using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.Business.SqlAnalyze
{
    /// <summary>
    /// Classe qui analyse un Script SQL Server et indique :
    ///  - Les blocs d'opérations élémentaires qui y sont faites
    ///  - les objets impactés
    ///  - Un résumé de l'objetif principal du script
    /// </summary>
    public class SqlAnalyzer
    {
        
        #region Membres privés
        /// <summary>
        /// La clé du script analysé
        /// </summary>
        private int ScriptId; 

        /// <summary>
        /// Résultat global (Variable de travail utilisée pour la compilation du résumé)
        /// </summary>
        private Resume identifiedScript;

        /// <summary>
        /// La liste des blocs trouvés
        /// </summary>
        private List<Bloc> myblocs;

        /// <summary>
        /// La liste des élémnets constituant le résumé
        /// </summary>
        private List<Resume> myresumes;

        /// <summary>
        /// La liste des objets impacté par le script
        /// </summary>
        private List<DataBaseObject> mysqlobjets;
        #endregion

        /// <summary>
        /// Empêche la création d'une instance par défaut de la classe<see cref="SqlAnalyzer" />.
        /// </summary>
        private SqlAnalyzer()
        { }

        #region Properties
        /// <summary>
        /// Obtient la liste détaillées des bloc trouvés
        /// </summary>
        public IEnumerable<Bloc> Blocs => myblocs;

        /// <summary>
        /// Obtient la liste des actions qui résume le script
        /// </summary>
        public IEnumerable<Resume> Resumes => myresumes;

        /// <summary>
        /// Obtient la liste des objets impactés par ce script
        /// </summary>
        public IEnumerable<DataBaseObject> SqlObjets => mysqlobjets;
        #endregion

        /// <summary>
        /// Initialise une analyse et renvoi l'objet instancié
        /// </summary>
        /// <param name="scriptId">La clé du script</param>
        /// <param name="text">Le contenu du fichier</param>
        /// <returns>L'objet instancié</returns>
        public static SqlAnalyzer Analyse(int scriptId, string text)
        {
            var sr = new SqlAnalyzer() { ScriptId = scriptId };
            sr.Initialize(text);
            return sr;
        }

        /// <summary>
        /// Load les infos d'analyse d'un script
        /// </summary>
        /// <param name="connection">La connexion à la base de données</param>
        /// <param name="scriptId">La clé du script</param>
        /// <returns></returns>
        public static SqlAnalyzer Load(DatabaseConnection connection, int scriptId)
        {
            string where = " WHERE ScriptId = @ScriptId";
            return new SqlAnalyzer()
            {
                ScriptId = scriptId,
                myblocs = connection.Query<Bloc>(Bloc.SQLSelect + where, new { ScriptId = scriptId }).ToList(),
                mysqlobjets = connection.Query<DataBaseObject>(DataBaseObject.SQLSelect + where, new { ScriptId = scriptId }).ToList(),
                myresumes = connection.Query<Resume>(Resume.SQLSelect + where, new { ScriptId = scriptId }).ToList(),
            };
        }

        /// <summary>
        /// Obtient le texte décripvant l'objet
        /// </summary>
        /// <returns>Le texte à afficher</returns>
        public override string ToString()
        {
            string nb = string.Empty;
            if (this.myblocs != null && this.myblocs.Any())
            {
                if (this.myblocs.Count > 1)
                {
                    nb = $"\n\n    {this.myblocs.Count} blocs trouvés";
                }
                else
                {
                    nb = $"\n\n    Un seul bloc trouvé";
                }
            }

            string nbr = string.Empty;
            if (this.myresumes != null && this.myresumes.Any())
            {
                if (this.myresumes.Count > 1)
                {
                    nbr = $"\n    {this.myresumes.Count} résumés";
                }
                else
                {
                    nbr = $"\n    Un seul résumé";
                }
            }

            string nbsqlo = string.Empty;
            if (this.mysqlobjets != null && this.mysqlobjets.Any())
            {
                if (this.mysqlobjets.Count > 1)
                {
                    nbsqlo = $"\n    {this.mysqlobjets.Count} objets Sql impacté";
                }
                else
                {
                    nbsqlo = $"\n    Un seul objet sql impacté";
                }
            }

            return  nb + nbr + nbsqlo;
        }

        public string ResumeText
            => string.Join(", ", this.Resumes);

        /// <summary>
        /// Enregistre l'objet en base
        /// </summary>
        /// <param name="connection">La connexion à la base de données</param>
        public void Save(DatabaseConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("La connextion n'est pas fournie");
            }

            if (this.Blocs.Any() || this.SqlObjets.Any() || this.Resumes.Any())
            { // y a quelque chose à sauver
                connection.BeginTransaction();
                try
                {
                    this.SaveBlocs(connection);
                    this.SaveDatabaseObjects(connection);
                    this.SaveResume(connection);
                    connection.CommitTransaction();
                }
                catch
                {
                    connection.RollBackTransaction();
                    throw;
                }
            }
        }

        #region Analyse du Texte SQL
        /// <summary>
        /// Initialize l'objet et le remplit
        /// </summary>
        /// <param name="text">Le contenu</param>
        private void Initialize(string text)
        {
            text = text.Replace("\r", string.Empty);
            this.myblocs = new List<Bloc>();
            this.myresumes = new List<Resume>();
            this.mysqlobjets = new List<DataBaseObject>();
            this.identifiedScript = null;

            if (!string.IsNullOrWhiteSpace(text))
            {
                this.FillBlocs(text);

                if (this.myblocs.Any())
                { // on traque les optimisations possibles
                    this.RemoveActionDouble();
                    this.RemoveActionInComment();

                    // on retire les appel aux procédures sytèmes 
                    // et aux EXEC (@sql); ==> appel à du script en texte (le texte du script est déjà parsé !)
                    this.RemoveSystemProcedureCall();

                    // Recherche des patterns de script connus
                    this.FindDBComprarerScript();
                    this.FindTableReCreation();

                    // compilation du résumé
                    this.CompileResume();

                    // compilation des SQL objets
                    this.mysqlobjets = this.myblocs.Where(x => x.SqlWhatId != SqlWhat.None && x.SqlActionId != SqlAction.DbComparer && x.SqlActionId != SqlAction.Comment).Select(x => x.GetDatabaseObject()).Distinct().ToList();
                }

                if (!this.Resumes.Any())
                { // Y a rien, on ajoute une ligne pour mémoriser que l'analyse a été faite
                    this.myresumes.Add(new Resume() 
                        { 
                            ScriptId = this.ScriptId, 
                            SqlActionId = SqlAction.Unknow, 
                            SqlWhatId = SqlWhat.None 
                        });
                }
            }
        }

        /// <summary>
        /// Remplit les "Blocs" d'éxécutions élémentaires trouvés dans le script
        /// </summary>
        /// <param name="text">Le texte SQL a analyser</param>
        private void FillBlocs(string text)
        {
            foreach (var e in RegexFounding.List)
            {
                this.FillBlocsExpression(text, e);
            }

            // Si on a des Add Columns alors on analyse des trucs en plus (Gain de temps sinon !)
            if (this.myblocs.Any(x => x.SqlActionId == SqlAction.AddColumn || x.SqlActionId == SqlAction.AddColumnNotNull))
            {
                foreach (var e in RegexFounding.ListAddColumn)
                {
                    this.FillBlocsExpression(text, e);
                }
            }
        }

        /// <summary>
        ///  Remplit les "Blocs" d'éxécutions élémentaires trouvés dans le script en fonction de l'expression
        /// </summary>
        /// <param name="text">Le texte SQL a analyser</param>
        /// <param name="e">L'expression à trouver</param>
        private void FillBlocsExpression(string text, RegexFounding e)
        {
            var ba = new BlocAnalyzer();
            ba.Analyze(e, this.ScriptId, text);
            myblocs.AddRange(ba.Blocs);
        }

        /// <summary>
        /// On a des "Add column" et des "add column Not null" pour une même info réelle ==> garder "add column Not null"
        /// On a aussi des DROP column avec et sans le nom de la colonne !
        /// On a aussi Drop Column qui est fait par un appel à une proc stoquée
        /// On a aussi Add column DEFAULT et modifie column au même index !
        /// </summary>
        private void RemoveActionDouble()
        {
            var lst = new List<Bloc>();
            foreach (var r in this.myblocs.OrderBy(x => x.BlocIndex).ThenBy(x => x.SqlActionId))
            { // le Tri assure que les Addcolumns arrivent avant les AddColumnNotNull
                if (r.SqlActionId == SqlAction.AddColumn)
                {
                    if (!this.myblocs.Any(x => x.SqlActionId == SqlAction.AddColumnNotNull && x.SqlWhatId == r.SqlWhatId && x.GetFullName() == r.GetFullName() && x.BlocIndex == r.BlocIndex))
                    { // on a pas trouvé un not null ==> c'est un add column qu'il faut : on le garde
                        if (r.BlocColumn.ToUpper() != "DEFAULT" || (r.BlocName.ToUpper() == "DEFAULT" && !this.myblocs.Any(x => x.SqlActionId == SqlAction.AlterColumn && x.SqlWhatId == r.SqlWhatId && x.BlocIndex == r.BlocIndex)))
                        {  // on est pas sur un add column défaut sans avoir de modifiy column
                            lst.Add(r);
                        }
                    }
                }
                else if (r.SqlActionId == SqlAction.DropColumn && string.IsNullOrWhiteSpace(r.BlocColumn) && r.BlocSchema == r.BlocName)
                { // au cas ou les 2 REGEX de drop column aient remontées la même info ?
                    if (!this.myblocs.Any(x => x.SqlActionId == SqlAction.DropColumn && x.SqlWhatId == r.SqlWhatId && x.BlocIndex == r.BlocIndex && !string.IsNullOrWhiteSpace(x.BlocColumn)))
                    { // on a pas trouvé un avec le nom ==> on le garde
                        lst.Add(r);
                    }
                }
                ////else if (r.SqlActionId == SqlAction.Execute && r.SqlWhatId == SqlWhat.Procedure && r.BlocSchema.ToLower() == "dbo" && r.BlocName.ToLower() == "flag_systemes_drop_column")
                ////{ // un appel a cette procédure est détecté s'il y a aussi un Supprime colonne pas loin (à 10 caractères près ) ==> on filtre l'appel
                ////    if (!this.myblocs.Any(x => x.SqlActionId == SqlAction.DropColumn && x.SqlWhatId == SqlWhat.Table && Math.Abs(r.BlocIndex - x.BlocIndex) < 10))
                ////    { // on a pas le drop column ==> on garde l'appel à la procédure
                ////        lst.Add(r);
                ////    }
                ////}
                else
                { // cas général
                    lst.Add(r);
                }
            }

            this.myblocs = lst;
        }

        /// <summary>
        /// Retire les actions à l'intérieur des commentaires
        /// </summary>
        private void RemoveActionInComment()
        {
            var comments = this.myblocs.Where(x => x.SqlActionId == SqlAction.Comment && x.SqlWhatId == SqlWhat.None);
            if (comments.Any())
            { // Y a des commentaires
                foreach (var res in comments.ToList())
                {
                    foreach (var act in this.myblocs.Where(x => res.IsIn(x) && x.SqlActionId != SqlAction.DbComparer && x.SqlActionId != SqlAction.Comment))
                    {
                        act.BlocExcludeFromResume = true;
                    }
                }

                this.myblocs.RemoveAll(x => x.BlocExcludeFromResume == true);
                this.myblocs.RemoveAll(x => x.SqlActionId == SqlAction.Comment);
            }
        }

        /// <summary>
        /// Retire les appels aux procédures système et les EXEC non pertinent (== ceux comme EXEC (@sql) ou @sql est une chaine que l'on a parsé juste avant ! )
        /// </summary>
        private void RemoveSystemProcedureCall()
        {
            // Supression des Exec (@sql) ou EXEC ('update...') ==> inutile pour l'analyse 
            this.myblocs.RemoveAll(x => x.SqlActionId == SqlAction.Execute && x.SqlWhatId == SqlWhat.None);
            foreach (var r in this.myblocs.Where(x => x.SqlWhatId == SqlWhat.Table && string.IsNullOrWhiteSpace(x.BlocSchema) && x.BlocName.StartsWith("#")))
            { // la manipulation de tables temporaire n'est pas dans le résumé
                r.BlocExcludeFromResume = true;
            }

            // Filtre des appels aux procédures SQL Server sytème (sp_xxx)
            foreach (var r in this.myblocs.Where(x => x.SqlActionId == SqlAction.Execute && x.SqlWhatId == SqlWhat.Procedure && x.BlocName.ToLowerInvariant().StartsWith("sp_")))
            {
                r.BlocExcludeFromResume = true;
            }

            // filtre des objet Temporaires
            foreach (var r in this.myblocs.Where(x => x.SqlWhatId == SqlWhat.Table && (x.BlocName.StartsWith("@") || x.BlocName.StartsWith("##") || x.BlocName.StartsWith("#"))))
            {
                r.BlocExcludeFromResume = true;
            }

            ////foreach (var r in this.myblocs.Where(x => x.SqlActionId == SqlAction.Execute && x.SqlWhatId == SqlWhat.Procedure && x.BlocName.ToUpperInvariant() == "FLAG_SYSTEMES_DROP_COLUMN"))
            ////{
            ////    r.BlocExcludeFromResume = true;
            ////}
        }

        /// <summary>
        /// Détecte les scripts de type DBComparer (Qui drop l'objet, puis le recrée avec éventuellement des versions différentes par clients)
        /// Les scripts "d'état perso" ne sont pas géré ici
        /// </summary>
        private void FindDBComprarerScript()
        {
            var dbcomparers = this.myblocs.Where(x => x.SqlActionId == SqlAction.DbComparer);

            foreach (var dbcomparer in dbcomparers)
            {
                if (dbcomparer.SqlWhatId == SqlWhat.None)
                {
                    if (!string.IsNullOrWhiteSpace(dbcomparer.BlocName))
                    { // spécifique pour les états personnalisés
                        this.identifiedScript = dbcomparer.GetResume(true);
                        return;
                    }
                }
                else
                { // uniquement si le commentaire DBComparer est trouvé
                    int theaction;
                    if (dbcomparer.SqlWhatId == SqlWhat.Schema)
                    { // pour les schéma il n'y a que des CREATE
                        var schema = this.myblocs.FirstOrDefault(x => x.SqlActionId == SqlAction.Create && x.SqlWhatId == SqlWhat.Schema);
                        if (schema != null)
                        {
                            this.identifiedScript = schema.GetResume();
                        }

                        return;
                    }

                    // on essaie les Create en premier
                    var name = this.myblocs.Where(x => x.SqlActionId == SqlAction.Drop && x.SqlWhatId == dbcomparer.SqlWhatId).Select(x => x.GetResume()).FirstOrDefault();
                    theaction = SqlAction.Create;
                    if (name == null)
                    { // ca marche pas on essaie les alter
                        name = this.myblocs.Where(x => x.SqlActionId == SqlAction.Alter && x.SqlWhatId == dbcomparer.SqlWhatId).Select(x => x.GetResume()).FirstOrDefault();
                        theaction = SqlAction.Alter;
                    }

                    if (name != null)
                    { // Y a tout pour dire que c'est un script DBComparer
                        int nbcreate = this.myblocs.Count(x => x.SqlActionId == theaction && x.SqlWhatId == dbcomparer.SqlWhatId && x.GetFullName() == name.GetFullName());
                        var clients = this.myblocs.Where(x => x.SqlActionId == SqlAction.CodeClient && x.ClientCodeId.HasValue && x.ClientCodeId > 0).Select(x => new ClientCode() { ClientCodeId = x.ClientCodeId.Value }).ToList();
                        int nbclient = clients.Count();
                        if (Math.Abs(nbcreate - nbclient) < 2)
                        { // l'écart entre les 2 semble ok
                            var act = nbcreate == 0 && theaction != SqlAction.Alter ? SqlAction.Drop : SqlAction.Alter;
                            this.identifiedScript = new Resume() 
                                { 
                                SqlActionId = act, 
                                SqlWhatId = dbcomparer.SqlWhatId, 
                                ScriptId =dbcomparer.ScriptId,
                                ResumeDatabase = name.ResumeDatabase,
                                ResumeSchema = name.ResumeSchema,
                                ResumeName = name.ResumeName,
                                ResumeColumn = name.ResumeColumn
                            };
                            if (clients.Any())
                            { // compil des codes clients impactés
                                this.identifiedScript.Clients = clients;
                                if (nbcreate > nbclient)
                                { // y a un créate de plus que des codes clients ==> il y a donc une clause Else == tous les autres client; elle est encodée avec un code = ALLOTHERCLIENTCODE (-1)
                                    this.identifiedScript.ResumeForOtherClients = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Identifie un script qui Drop une table et la recrée 
        /// (Dans certains cas la séquence est : 
        /// drop table temporaire, rename de la table en table temporaire, recréation de la table.
        /// Exemple FLAG_SYSTEMES_POUBELLE)
        /// Le script ne se préoccupe pas de la copie des données ==> ce sera vu comme un insert dans la table.
        /// </summary>
        private void FindTableReCreation()
        {
            var droptable = this.myblocs.Where(x => !x.BlocExcludeFromResume && x.SqlActionId == SqlAction.Drop && x.SqlWhatId == SqlWhat.Table);
            var createtable = this.myblocs.Where(x => !x.BlocExcludeFromResume && x.SqlActionId == SqlAction.Create && x.SqlWhatId == SqlWhat.Table);
            if (droptable.Any() && createtable.Any())
            { // Y a des drop et des créate on peut réfléchir...
                var tmps = new List<Resume>();
                Bloc create;
                Bloc rename;
                string tableName;
                foreach (var del in droptable)
                {
                    create = null;
                    rename = null;
                    if (del.BlocName.ToLowerInvariant().StartsWith("tmp_"))
                    { // on vient de trouver un drop d'une table tmp_xx : recherche de rename xx et Create xx
                        tableName = del.BlocName[4..]; // suppression de tmp_
                        rename = this.myblocs.FirstOrDefault(x => !x.BlocExcludeFromResume && x.SqlActionId == SqlAction.Rename && x.SqlWhatId == SqlWhat.Table && x.BlocDatabase == del.BlocDatabase && x.BlocSchema == del.BlocSchema && x.BlocName == tableName && x.BlocIndex > del.BlocIndex);
                        if (rename != null)
                        { // on peut continuer
                            create = createtable.FirstOrDefault(x => x.BlocSchema == del.BlocSchema && x.BlocName == tableName && x.BlocIndex > rename.BlocIndex);
                        }
                    }
                    else
                    { // drop et créate à l'identique (ca arrive quand on conserve pas les datas sur les tables pas encore en PROD)
                        tableName = del.BlocName;
                        create = createtable.FirstOrDefault(x => x.GetFullName() == del.GetFullName() && x.BlocIndex > del.BlocIndex);
                    }

                    if (create != null)
                    { // on a trouvé le create en face du drop ==> on mémorise l'action
                        tmps.Add(new Resume() { ScriptId = this.ScriptId, SqlActionId = SqlAction.Alter, SqlWhatId = SqlWhat.Table, ResumeDatabase = del.BlocDatabase, ResumeSchema = del.BlocSchema, ResumeName = tableName });

                        // les blocs intermédiares ne sont plus intéressant pour le résumé
                        del.BlocExcludeFromResume = true;
                        create.BlocExcludeFromResume = true;
                        if (rename != null)
                        {
                            rename.BlocExcludeFromResume = true;
                        }
                    }
                }

                if (tmps.Any())
                { // insertion dans le résumé avec dédoublonnage (C'est le cas quand on est dans une procédure qui recrée des table et qu'elle a du spécique pour plusieurs clients
                    this.myresumes.AddRange(tmps.Distinct());
                }
            }
        }

        /// <summary>
        /// Compile le résumé
        /// </summary>
        private void CompileResume()
        {
            if (this.identifiedScript != null)
            { // un template de script a été trouvé
                this.myresumes.Clear();  // on efface la collection ici car dans certains cas la procédure trouvée a déjà des résumés (issu de Drop & Create) qui ne sont pas intérresant car à l'intérieur de la proc

                var lst = this.identifiedScript.Clients.Where(x => x.ClientCodeId != 0);
                if (lst.Any())
                { // ajout des codes clients
                    var cclient = new Resume() { SqlActionId = SqlAction.CodeClient };
                    cclient.Clients = lst.ToList();
                    this.myresumes.Add(cclient);
                }

                // ajout du template
                this.myresumes.Add(this.identifiedScript);
            }
            else
            { // pas de template : on compile
              // 1 on regrouppe les codes clients en un élément
                var clients = this.myblocs.Where(x => !x.BlocExcludeFromResume && x.SqlActionId == SqlAction.CodeClient).Select(x => x.GetResume()).Distinct();
                if (clients.Any())
                {
                    var cclient = new Resume() { SqlActionId = SqlAction.CodeClient };
                    cclient.Clients = clients.SelectMany(c => c.Clients).Distinct().ToList();
                    this.myresumes.Add(cclient);
                }

                // 2 on ajoute les actions "pertinentes" en les dédoublonnant
                // tout ce qui n'est pas code client, ou execution en dehors des appels aux procédures stockées
                this.myresumes.AddRange(this.myblocs.Where(x => !x.BlocExcludeFromResume
                                                        && x.SqlActionId != SqlAction.CodeClient && x.SqlActionId != SqlAction.DbComparer
                                                        && ((x.SqlActionId == SqlAction.Execute && x.SqlWhatId == SqlWhat.Procedure)
                                                             || x.SqlActionId != SqlAction.Execute)).Select(x => x.GetResume()).Distinct());

                if (!this.myresumes.Any())
                { // A ce stade, on a pas obtenu de résumé : Ajout d'une ligne résumant les appels aux objets (Procédure, tables, ...) s'il y en a
                    this.myresumes.AddRange(this.myblocs.Where(x => !x.BlocExcludeFromResume
                                                          && x.SqlActionId != SqlAction.CodeClient && x.SqlActionId != SqlAction.DbComparer
                                                          && x.SqlActionId == SqlAction.Execute
                                                          && (x.SqlWhatId != SqlWhat.Table ||
                                                               (x.SqlWhatId == SqlWhat.Table && x.BlocSchema.ToLower() != "sys")))
                                                        .Select(x => x.GetResume(false))
                                                        .Distinct());
                }
            }
        }
        #endregion

        #region Save SQL
        /// <summary>
        /// Enregistre les blocs en BDD
        /// </summary>
        /// <param name="connection">La connexion à la base VersionDB</param>
        private void SaveBlocs(DatabaseConnection connection)
        {
            if (this.Blocs.Any())
            {
                var sqlStart = $@"
DECLARE @ScriptId INT = {SqlFormat.ForeignKey(this.ScriptId)}; 
DECLARE @tmpB TABLE(ScriptId INT, SqlActionId INT, SqlWhatId INT, ClientCodeId INT, BlocIndex INT, BlocLength INT, BlocDataBase VARCHAR(100), BlocSchema VARCHAR(20), BlocName VARCHAR(100), BlocExcludeFromResume BIT, BlocColumn VARCHAR(100));
";
                var sqlInsert = @"
INSERT INTO @tmpB(ScriptId, SqlActionId, SqlWhatId, ClientCodeId, BlocIndex, BlocLength, BlocDataBase, BlocSchema, BlocName, BlocExcludeFromResume, BlocColumn)
VALUES 
";
                var valuesTemplate = "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})";
                var sqlEnd = @"
MERGE dbo.Bloc AS t
   USING 
     (SELECT ScriptId, SqlActionId, SqlWhatId, ClientCodeId, BlocIndex, BlocLength, BlocDataBase, BlocSchema, BlocName, BlocExcludeFromResume, BlocColumn FROM @tmpB) 
       AS s 
   ON    t.SqlActionId = s.SqlActionId AND t.SqlWhatId = s.SqlWhatId AND ISNULL(t.ClientCodeId, 0) = ISNULL(s.ClientCodeId, 0) AND t.ScriptId = s.ScriptId
     AND t.BlocIndex = s.BlocIndex AND t.BlocLength = s.BlocLength 
     AND t.BlocDataBase = s.BlocDataBase AND t.BlocSchema = s.BlocSchema AND t.BlocName = s.BlocName
     AND t.BlocExcludeFromResume = s.BlocExcludeFromResume
     AND  ISNULL(t.BlocColumn, '') = ISNULL(s.BlocColumn, '')
  WHEN NOT MATCHED THEN 
    INSERT (ScriptId, SqlActionId, SqlWhatId, ClientCodeId, BlocIndex, BlocLength, BlocDataBase, BlocSchema, BlocName, BlocExcludeFromResume, BlocColumn)
    VALUES (ScriptId, SqlActionId, SqlWhatId, ClientCodeId, BlocIndex, BlocLength, BlocDataBase, BlocSchema, BlocName, BlocExcludeFromResume, BlocColumn)
  WHEN NOT MATCHED BY SOURCE AND t.ScriptId = @ScriptId THEN 
    DELETE
;
";
                StringBuilder sql = new StringBuilder(sqlStart);
                int number = (int)Math.Ceiling((double)Blocs.Count() / SqlFormat.SQLBULKINSERTSIZE);
                for(var i = 0; i < number; i++)
                {
                    var blocs = Blocs.Skip(i * SqlFormat.SQLBULKINSERTSIZE).Take(SqlFormat.SQLBULKINSERTSIZE);
                    var values = blocs.Select(x => string.Format(
                        valuesTemplate,
                        SqlFormat.ForeignKey(x.ScriptId),
                        SqlFormat.Integer(x.SqlActionId),
                        SqlFormat.Integer(x.SqlWhatId),
                        SqlFormat.ForeignKey(x.ClientCodeId),
                        SqlFormat.Integer(x.BlocIndex),
                        SqlFormat.Integer(x.BlocLength),
                        SqlFormat.String(x.BlocDatabase),
                        SqlFormat.String(x.BlocSchema),
                        SqlFormat.String(x.BlocName),
                        SqlFormat.Boolean(x.BlocExcludeFromResume),
                        SqlFormat.String(x.BlocColumn, true)));
                    sql.Append(sqlInsert + string.Join(", ", values) + ";");
                }

                sql.Append(sqlEnd);
                var sqlExecute = sql.ToString();
                connection.Execute(sqlExecute);
            }
        }

        /// <summary>
        /// Enregistre les objets sql
        /// </summary>
        /// <param name="connection">La connexion à la base VersionDB</param>
        private void SaveDatabaseObjects(DatabaseConnection connection)
        {
            if (this.SqlObjets.Any())
            {
                var sqlStart = $@"
DECLARE @ScriptId INT = {SqlFormat.ForeignKey(this.ScriptId)}; 
DECLARE @tmpO TABLE (ScriptId INT, SqlWhatId INT, DatabaseObjectDataBase VARCHAR(100), DatabaseObjectSchema VARCHAR(20), DatabaseObjectName VARCHAR(100));
";
                var sqlInsert = @"
INSERT INTO @tmpO (ScriptId, SqlWhatId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName)
VALUES 
";
                var valuesTemplate = " ({0}, {1}, {2}, {3}, {4})";
                var sqlEnd = @"
MERGE dbo.DataBaseObject AS t
   USING 
     (SELECT ScriptId, SqlWhatId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName FROM @tmpO) 
       AS s 
   ON    t.ScriptId = s.ScriptId AND t.SqlWhatId = s.SqlWhatId
     AND t.DatabaseObjectDataBase = s.DatabaseObjectDataBase AND t.DatabaseObjectSchema = s.DatabaseObjectSchema AND t.DatabaseObjectName = s.DatabaseObjectName
  WHEN NOT MATCHED THEN 
    INSERT (ScriptId, SqlWhatId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName)
    VALUES (ScriptId, SqlWhatId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName)
  WHEN NOT MATCHED BY SOURCE AND t.ScriptId = @ScriptId  THEN 
    DELETE
;
";
                StringBuilder sql = new StringBuilder(sqlStart);
                int number = (int)Math.Ceiling((double)SqlObjets.Count() / SqlFormat.SQLBULKINSERTSIZE);
                for (var i = 0; i < number; i++)
                {
                    var blocs = SqlObjets.Skip(i * SqlFormat.SQLBULKINSERTSIZE).Take(SqlFormat.SQLBULKINSERTSIZE);
                    var values = blocs.Select(x => string.Format(
                        valuesTemplate,
                        SqlFormat.ForeignKey(x.ScriptId),
                        SqlFormat.Integer(x.SqlWhatId),
                        SqlFormat.String(x.DatabaseObjectDatabase),
                        SqlFormat.String(x.DatabaseObjectSchema),
                        SqlFormat.String(x.DatabaseObjectName)));
                    sql.Append(sqlInsert + string.Join(", ", values) + ";");
                }

                sql.Append(sqlEnd);
                var sqlExecute = sql.ToString();
                connection.Execute(sqlExecute);
            }
        }

        /// <summary>
        /// Enregistre les résumés
        /// </summary>
        /// <param name="connection">La connexion à la base VersionDB</param>
        private void SaveResume(DatabaseConnection connection)
        {
            if (this.Resumes.Any())
            {
                var sqlStart = $@"
DECLARE @ScriptId INT = {SqlFormat.ForeignKey(this.ScriptId)}; 
DECLARE @tmpR TABLE (ScriptId INT, SqlActionId INT, SqlWhatId INT, ResumeDatabase VARCHAR(100), ResumeSchema VARCHAR(20), ResumeName VARCHAR(100), ResumeColumn VARCHAR(100), ResumeForOtherClients BIT, ResumeManualValidationCode TINYINT);
";
                var sqlInsert = @"
INSERT INTO @tmpR (ScriptId, SqlActionId, SqlWhatId, ResumeDatabase, ResumeSchema, ResumeName, ResumeColumn, ResumeForOtherClients, ResumeManualValidationCode)
VALUES 
";
                var valuesTemplate = "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})";
                var sqlEnd = @"
MERGE dbo.[Resume] AS t
   USING 
     (SELECT ScriptId, SqlActionId, SqlWhatId, ResumeDatabase, ResumeSchema, ResumeName, ResumeColumn, ResumeForOtherClients, ResumeManualValidationCode FROM @tmpR) 
       AS s 
   ON    t.SqlActionId = s.SqlActionId AND t.SqlWhatId = s.SqlWhatId AND t.ScriptId = s.ScriptId
     AND t.ResumeDatabase = s.ResumeDatabase AND t.ResumeSchema = s.ResumeSchema AND t.ResumeName = s.ResumeName
     AND  ISNULL(t.ResumeColumn, '') = ISNULL(s.ResumeColumn, '')
  WHEN MATCHED AND (t.ResumeForOtherClients != s.ResumeForOtherClients OR t.ResumeManualValidationCode != s.ResumeManualValidationCode) THEN
   UPDATE SET ResumeForOtherClients = s.ResumeForOtherClients
            , ResumeManualValidationCode = s.ResumeManualValidationCode
  WHEN NOT MATCHED THEN 
    INSERT (ScriptId, SqlActionId, SqlWhatId, ResumeDatabase, ResumeSchema, ResumeName, ResumeColumn, ResumeForOtherClients, ResumeManualValidationCode)
    VALUES (ScriptId, SqlActionId, SqlWhatId, ResumeDatabase, ResumeSchema, ResumeName, ResumeColumn, ResumeForOtherClients, ResumeManualValidationCode)
  WHEN NOT MATCHED BY SOURCE AND t.ScriptId = @ScriptId THEN 
    ---DELETE
    UPDATE SET ResumeManualValidationCode = 254
  OUTPUT deleted.ResumeId AS OldResumeId, deleted.ResumeForOtherClients AS OldResumeForOtherClients, deleted.ResumeManualValidationCode AS OldResumeManualValidationCode, $action As [SqlAction]
       , inserted.*
;
";
                StringBuilder sql = new StringBuilder(sqlStart);
                int number = (int)Math.Ceiling((double)Resumes.Count() / SqlFormat.SQLBULKINSERTSIZE);
                for (var i = 0; i < number; i++)
                {
                    var resume = Resumes.Skip(i * SqlFormat.SQLBULKINSERTSIZE).Take(SqlFormat.SQLBULKINSERTSIZE);
                    var values = resume.Select(x => string.Format(
                        valuesTemplate,
                        SqlFormat.ForeignKey(x.ScriptId),
                        SqlFormat.Integer(x.SqlActionId),
                        SqlFormat.Integer(x.SqlWhatId),
                        SqlFormat.String(x.ResumeDatabase),
                        SqlFormat.String(x.ResumeSchema),
                        SqlFormat.String(x.ResumeName),
                        SqlFormat.String(x.ResumeColumn, true),
                        SqlFormat.Boolean(x.ResumeForOtherClients),
                        SqlFormat.Integer(x.ResumeManualValidationCode)));
                    sql.Append(sqlInsert + string.Join(", ", values) + ";");
                }

                sql.Append(sqlEnd);
                var sqlExecute = sql.ToString();
                var resultats = connection.Query<ResumeMerge>(sqlExecute);
                foreach (var resumeMerge in resultats.Where(x => x.SqlAction == "INSERT" || x.SqlAction == "UPDATE"))
                {
                    if (resumeMerge.ResumeManualValidationCode == 254)
                    {  // A supprimer
                        var sql2 = @"
DELETE FROM dbo.Resume4Client WHERE ResumeId = @ResumeId;
DELETE FROM dbo.[Resume]      WHERE ResumeId = @ResumeId;
";
                        connection.Execute(sql2, new { resumeMerge.ResumeId });
                    }
                    else
                    {  // A synchroniser
                        var resume = this.Resumes.FirstOrDefault(x => x.IsSame(resumeMerge));
                        if (resume == null)
                        {
                            throw new ArgumentException($"Impossible de retrouver {resumeMerge}");
                        }

                        if (resume.Clients != null && resume.Clients.Any())
                        {

                            sqlStart = @"
DECLARE @tmp4 TABLE (ResumeId INT, ClientCodeId INT);
";
                            sqlInsert = @"
INSERT INTO @tmp4 (ResumeId, ClientCodeId)
VALUES 
";
                            valuesTemplate = "({0}, {1})";
                            sqlEnd = @"
MERGE dbo.Resume4Client AS t
   USING(SELECT ResumeId, ClientCodeId FROM @tmp4) AS s
   ON t.ResumeId = s.ResumeId
     AND t.ClientCodeId = s.ClientCodeId
  WHEN NOT MATCHED THEN
    INSERT (ResumeId, ClientCodeId) 
    VALUES (ResumeId, ClientCodeId)
  WHEN NOT MATCHED BY SOURCE THEN
    DELETE
  ;
                            ";

                            sql = new StringBuilder(sqlStart);
                            number = (int)Math.Ceiling((double)resume.Clients.Count() / SqlFormat.SQLBULKINSERTSIZE);
                            for (var i = 0; i < number; i++)
                            {
                                var client = resume.Clients.Skip(i * SqlFormat.SQLBULKINSERTSIZE).Take(SqlFormat.SQLBULKINSERTSIZE);
                                var values = client.Select(x => string.Format(
                                    valuesTemplate,
                                    SqlFormat.ForeignKey(resumeMerge.ResumeId),
                                    SqlFormat.ForeignKey(x.ClientCodeId)));
                                sql.Append(sqlInsert + string.Join(", ", values) + ";");
                            }

                            sql.Append(sqlEnd);
                            sqlExecute = sql.ToString();
                            connection.Execute(sqlExecute);
                        }
                    }
                }
            }
        }

        private class ResumeMerge : Resume
        {
            public int OldResumeId { get; set; }
            public bool OldResumeForOtherClients { get; set; }
            public byte OldResumeManualValidationCode { get; set; }

            public string SqlAction { get; set; }
        }
        #endregion
    }
}
