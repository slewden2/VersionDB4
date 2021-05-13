using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.Business.SqlAnalyze
{
    /// <summary>
    /// Classe qui contient l'ensemble des expressions permettant de trouver les actions dans un script
    /// </summary>
    public class RegexFounding
    {
        /// <summary>
        /// Obtient la liste publique des éléments à chercher
        /// </summary>
        private static readonly List<RegexFounding> THELIST = new List<RegexFounding>()
        {
            // TODO SQLWHAT = Constraint = 9
            // TODO SQL Action PRINT = 18 = identifier RaiseError du print no wait


            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\s+procédure", SqlAction.DbComparer, SqlWhat.Procedure),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\s+fonction", SqlAction.DbComparer, SqlWhat.Function),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\s+vue", SqlAction.DbComparer, SqlWhat.View),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?le\s+déclencheur", SqlAction.DbComparer, SqlWhat.Trigger),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?l'index", SqlAction.DbComparer, SqlWhat.Index),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?le\s+schéma", SqlAction.DbComparer, SqlWhat.Schema),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?L'état\s+perso\s+(\S+\s+)*?etli_nom\s+=\s+''(?<name>(\S+?\s*?)+?)''", SqlAction.DbComparer, SqlWhat.None), 
            //// ici on ne gère pas les scripts d'état perso (inutile pour le projet)

            new RegexFounding(@"CODECLIENTIS\((?<codeClient>\d+)\)", SqlAction.CodeClient, SqlWhat.None),
            //// IF\s+\(\s*EXISTS\s*\(\s*SELECT\s+(\S+?\s*?)+?FROM\s+DBO\.CODECLIENTIS\((?<codeClient>\d+)\)\s*?\)\s*?\)\s*?(--(.*?)\n)*BEGIN(\s+?\S*?)*?END
            new RegexFounding(@"(?m)--(.*?)\n", SqlAction.Comment, SqlWhat.None),
            new RegexFounding(@"/\*+([^*]|[\r\n]| \*([^/]|[\r\n]))*?\*+/", SqlAction.Comment, SqlWhat.None),

            new RegexFounding(@"RAISERROR\s*\(", SqlAction.RaiseError, SqlWhat.None),

            new RegexFounding(@"CREATE\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s*\(", SqlAction.Create, SqlWhat.Table),
            new RegexFounding(@"SELECT\s+(\S+\s+(?!(FROM|CREATE|BEGIN|INSERT)))*?INTO\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s", SqlAction.Create, SqlWhat.Table),
            new RegexFounding(@"DROP\s+Table\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)(\s|;|$)", SqlAction.Drop, SqlWhat.Table),
            new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s+(?<col>\w+?)\s+?(\S+?\s+?)NOT\s+NULL", SqlAction.AddColumnNotNull, SqlWhat.Table),
            new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s+(?!CONSTRAINT)(?<col>\w+?)\s", SqlAction.AddColumn, SqlWhat.Table),

            new RegexFounding(@"ALTER\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+ADD\s+(?!CONSTRAINT)(\s+?\S*?)*FOR\s+(?<col>\S+)\s", SqlAction.AlterColumn, SqlWhat.Table),    // cas particulier pour gérer l'ajout de contraintes par défaut ALETER TABLE ADD DEFAULT
            new RegexFounding(@"ALTER\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+ALTER\s+COLUMN\s+(?<col>\S+?)\s", SqlAction.AlterColumn, SqlWhat.Table),
            //new RegexFounding(@"FLAG_SYSTEMES_DROP_COLUMN\s+N?'(?<shema>\S*\.)?(?<name>\S+)'\s*,\s*'(?<col>\S+)'\s*[^,]", SqlAction.DropColumn, SqlWhat.Table),
            //new RegexFounding(@"FLAG_SYSTEMES_DROP_COLUMN\s+(N?'(?<shema>\S+)'\s*,\s*)?(?<name>\S+)'\s*,\s*'(?<col>\S+)'", SqlAction.DropColumn, SqlWhat.Table),
            //new RegexFounding(@"FLAG_SYSTEMES_DROP_COLUMN\s+(@schema\s*=\s*N?'(?<schema>\S+)'\s*,)?@NomTable\s*=\s*N?'(?<name>\S+)'\s*,\s*@NomColonne\s*=\s*N?'(?<col>\S+)'", SqlAction.DropColumn, SqlWhat.Table),
            new RegexFounding(@"ALTER\s+TABLE\s+((?<schema>\S+)\.)?(?<name>\S+)\s+DROP\s+(?!CONSTRAINT)(COLUMN\s+)?(?<col>\S+)\s", SqlAction.DropColumn, SqlWhat.Table),
            new RegexFounding(@"SP_RENAME\s+(@\S+\s+=\s+)?N?['""]?((?<schema>\S+?)\.)?(?<name>\S+)['""\s]?", SqlAction.Rename, SqlWhat.Table),
            new RegexFounding(@"SP_RENAME\s+(@\S+\s+=\s+)?N?['""]?(?<schema>\S+?)\.(?<name>\S+)\.(?<col>\S+)['""\s]?", SqlAction.RenameColumn, SqlWhat.Table),

            new RegexFounding(@"INSERT\s+(INTO\s+)?(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s*(\(|SELECT)", SqlAction.Insert, SqlWhat.Table),
            new RegexFounding(@"(UPDATE\s+(?<prefix>\S+)\s+SET[\s\S]*?(FROM|JOIN)\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+\k<prefix>\s?)|(UPDATE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+SET)", SqlAction.Update, SqlWhat.Table),
            new RegexFounding(@"DELETE\s+(?<prefix>\S+)\s+(\S+\s+)*?FROM\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+\k<prefix>", SqlAction.Delete, SqlWhat.Table),
            new RegexFounding(@"DELETE\s+(?<prefix>\S+)\s+(\S+\s+)*?FROM\s+(\S+\s+)*JOIN\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+\k<prefix>", SqlAction.Delete, SqlWhat.Table),
            new RegexFounding(@"DELETE\s+FROM\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)", SqlAction.Delete, SqlWhat.Table),
            new RegexFounding(@"DELETE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+WHERE", SqlAction.Delete, SqlWhat.Table),
            new RegexFounding(@"TRUNCATE\s+TABLE\s+(?<schema>[a-z]\w*\.)?(?<name>[a-z]\w*)", SqlAction.Delete, SqlWhat.Table),

            new RegexFounding(@"CREATE\s+PROCEDURE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Create, SqlWhat.Procedure),
            new RegexFounding(@"ALTER\s+PROCEDURE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, SqlWhat.Procedure),
            new RegexFounding(@"DROP\s+PROCEDURE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, SqlWhat.Procedure),
            new RegexFounding(@"\s(EXEC|EXECUTE)\s+(@\S+\s+=\s+)?((?<shema>\S+)\.)?\[?(?<name>\w+)", SqlAction.Execute, SqlWhat.Procedure),

            new RegexFounding(@"CREATE\s+FUNCTION\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\S+)\s*\(", SqlAction.Create, SqlWhat.Function),
            new RegexFounding(@"ALTER\s+FUNCTION\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\S+)\s*\(", SqlAction.Alter, SqlWhat.Function),
            new RegexFounding(@"DROP\s+FUNCTION\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, SqlWhat.Function),

            new RegexFounding(@"CREATE\s+VIEW\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Create, SqlWhat.View),
            new RegexFounding(@"ALTER\s+VIEW\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, SqlWhat.View),
            new RegexFounding(@"DROP\s+VIEW\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, SqlWhat.View),
            new RegexFounding(@"THEN\s+'ALTER'\s+ELSE\s+'CREATE'\s+END\s+\+\s+'\s+VIEW\s+((?<database>\S+?)\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, SqlWhat.View),

            new RegexFounding(@"CREATE\s+TRIGGER\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Create, SqlWhat.Trigger),
            new RegexFounding(@"ALTER\s+TRIGGER\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, SqlWhat.Trigger),
            new RegexFounding(@"DROP\s+TRIGGER\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, SqlWhat.Trigger),

            new RegexFounding(@"CREATE\s+\S*\s+INDEX\s+(?<col>\S+)\s+ON\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+", SqlAction.Create, SqlWhat.Index),
            new RegexFounding(@"ALTER\s+\S*\s+INDEX\s+(?<col>\S+)\s+ON\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+", SqlAction.Alter, SqlWhat.Index),
            new RegexFounding(@"DROP\s+INDEX\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)\.\[?(?<col>\S+)\s+", SqlAction.Drop, SqlWhat.Index),
            new RegexFounding(@"DROP\s+INDEX\s+\[?(?<col>\S+)\s+ON\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, SqlWhat.Index),

            new RegexFounding(@"CREATE\s+SCHEMA\s+\[?(?<name>[a-z_][\w]*)", SqlAction.Create, SqlWhat.Schema),
            new RegexFounding(@"ALTER\s+SCHEMA\s+\[?(?<name>[a-z_][\w]*)TRANSFERT\s", SqlAction.Alter, SqlWhat.Schema),
            new RegexFounding(@"DROP\s+SCHEMA\s+(IF\s+EXISTS\s+)?\[?(?<name>[a-z_][\w]*)", SqlAction.Drop, SqlWhat.Schema),

            new RegexFounding(@"CREATE\s+TYPE\s+?((?<schema>\S+?)\.)?(?<name>\S+)\s+?AS", SqlAction.Create, SqlWhat.Type),
            new RegexFounding(@"DROP\s+TYPE\s+?((?<schema>\S+?)\.)?(?<name>\S+)\s+?", SqlAction.Drop, SqlWhat.Type),

            new RegexFounding(@"(FROM\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s*\()|(JOIN\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s*\()", SqlAction.Execute, SqlWhat.Function),
            new RegexFounding(@"(FROM\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s)|(JOIN\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s)", SqlAction.Execute, SqlWhat.Table), // VA y avoir des ambiguités !!
        };

        /// <summary>
        /// Obtient la liste publique des éléments à chercher quand on a déjà trouvé un ADD COLUMN
        /// </summary>
        private static readonly List<RegexFounding> THELISTCOLUMN = new List<RegexFounding>()
        {
          // add column deuxième colonne de l'instruction
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s((?!ALTER)[\S\s])+?,\s*(?<col>\w+)\s((?!ALTER).)+?NOT\s+NULL", SqlAction.AddColumnNotNull, SqlWhat.Table),
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s(\S+?\s+?(?!ALTER|;))*?(([\S\s](?!ALTER|;))+?),\s*?(?<col>\w+?)\s", SqlAction.AddColumn, SqlWhat.Table),   
      
          // add column troisième colonne de l'instruction
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s((?!ALTER)[\S\s])+?,((?!ALTER)[\S\s])+?,\s*(?<col>\w+)\s((?!ALTER).)+?NOT\s+NULL", SqlAction.AddColumnNotNull, SqlWhat.Table),
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s(\S+?\s+?(?!ALTER|;))*?(([\S\s](?!ALTER|;))+?),\s*?(\S+?\s+?(?!ALTER|;))*?(([\S\s](?!ALTER|;))+?),\s*?(?<col>\w+?)\s", SqlAction.AddColumn, SqlWhat.Table),
        };

        /// <summary>
        /// Initialise les membres statiques de la classe <see cref="RegexFounding" />.
        /// Pour ajuster la taille du cache des expressions régulière (Utile car on en utilise au moins 40 ici)
        /// </summary>
        static RegexFounding()
            => Regex.CacheSize = 50;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RegexFounding"/>
        /// </summary>
        /// <param name="pattern">Le pattern de recherche</param>
        /// <param name="action">L'action correspondante</param>
        /// <param name="apply">L'objet sur lequel s'applique l'action</param>
        private RegexFounding(string pattern, int action, int apply)
        {
            this.Expression = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace, new TimeSpan(0, 0, 5));
            this.Action = action;
            this.ApplyOn = apply;
        }

        /// <summary>
        /// Obtient la liste publique des éléments à chercher
        /// </summary>
        public static IEnumerable<RegexFounding> List => THELIST;

        /// <summary>
        /// Obtient la liste publique des éléments à chercher quand on a déjà un add column
        /// </summary>
        public static IEnumerable<RegexFounding> ListAddColumn => THELISTCOLUMN;

        /// <summary>
        /// Obtient l'expression régulière à utiliser
        /// </summary>
        public Regex Expression { get; private set; }

        /// <summary>
        /// Obtient l'action associée
        /// </summary>
        public int Action { get; private set; }

        /// <summary>
        /// Obtient l'objet concerné
        /// </summary>
        public int ApplyOn { get; private set; }

        /// <summary>
        /// Retire du texte tout ce qui ne doit pas y être pour un nom d'objet SQL Server
        /// (., les [], ...) cette méthode vient en complément des expressions régulières. Grace à elle, cela permet de les simplifier. 
        /// Les élements remontés sont finis d'être nettoyé grace à cette méthode
        /// </summary>
        /// <param name="name">Le nom à filtrer</param>
        /// <returns>Le nom filtré</returns>
        public static string Filtre(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }
            else
            {
                return name.Replace(".", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("\"", string.Empty).Replace(";", string.Empty).Replace("'", string.Empty);
            }
        }
    }
}
