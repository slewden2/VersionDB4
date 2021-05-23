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
            // TODO TypeObject = Constraint = 9
            // TODO SQL Action PRINT = 18 = identifier RaiseError du print no wait


            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\sprocédure", SqlAction.DbComparer, TypeObject.Procedure),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\sfonction\sscalaire", SqlAction.DbComparer, TypeObject.FunctionScalaire),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\sfonction\stable\sen\sligne", SqlAction.DbComparer, TypeObject.FunctionTableEnligne),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\sfonction\stable\sà\sinstructions\smultiples", SqlAction.DbComparer, TypeObject.FunctionTable),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\svue", SqlAction.DbComparer, TypeObject.View),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?le\sdéclencheur", SqlAction.DbComparer, TypeObject.Trigger),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?l'index", SqlAction.DbComparer, TypeObject.Index),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?le\sschéma", SqlAction.DbComparer, TypeObject.Schema),
            new RegexFounding(@"\s*---\sDBComparer\s((?!référence|l'index|contrainte)(\S+\s+))*?la\stable", SqlAction.DbComparer, TypeObject.Table),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?le\stype\sde\sdonnées\stable", SqlAction.DbComparer, TypeObject.TypeTable),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?le\stype\sde\sdonnées\sélémentaire", SqlAction.DbComparer, TypeObject.TypeElementaire),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\sréférence", SqlAction.DbComparer, TypeObject.ForeignKey),
            new RegexFounding(@"\s*---\sDBComparer\s+(\S+\s+)*?la\scontrainte", SqlAction.DbComparer, TypeObject.Constraint),

            new RegexFounding(@"CodeClientIs\((?<codeClient>\d+)\)", SqlAction.CodeClient, TypeObject.None),
            //// IF\s+\(\s*EXISTS\s*\(\s*SELECT\s+(\S+?\s*?)+?FROM\s+DBO\.CodeClientIs\((?<codeClient>\d+)\)\s*?\)\s*?\)\s*?(--(.*?)\n)*BEGIN(\s+?\S*?)*?END
            new RegexFounding(@"(?m)--(.*?)(\n|$)", SqlAction.Comment, TypeObject.None),
            new RegexFounding(@"/\*+([^*]|[\r\n]|\*[^/])*? \*+/(([^*/]*)\*+/)*", SqlAction.Comment, TypeObject.None),

            new RegexFounding(@"RAISERROR\s*\( ('.*?')+\s*,\s*1[12345678]\s*", SqlAction.RaiseError, TypeObject.None),

            new RegexFounding(@"CREATE\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s*\(", SqlAction.Create, TypeObject.Table),
            new RegexFounding(@"SELECT\s+(\S+\s+(?!(FROM|CREATE|BEGIN|INSERT)))*?INTO\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s", SqlAction.Create, TypeObject.Table),
            new RegexFounding(@"DROP\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)(\s|;|$)", SqlAction.Drop, TypeObject.Table),
            new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s+(?<col>\w+?)\s+?(\S+?\s+?)NOT\s+NULL", SqlAction.AddColumnNotNull, TypeObject.Table),
            new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s+(?!CONSTRAINT)(?<col>\w+?)\s", SqlAction.AddColumn, TypeObject.Table),

            new RegexFounding(@"ALTER\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+ADD\s+(?!CONSTRAINT)(\s+?\S*?)*FOR\s+(?<col>\S+)\s", SqlAction.AlterColumn, TypeObject.Table),    // cas particulier pour gérer l'ajout de contraintes par défaut ALETER TABLE ADD DEFAULT
            new RegexFounding(@"ALTER\s+TABLE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+ALTER\s+COLUMN\s+(?<col>\S+?)\s", SqlAction.AlterColumn, TypeObject.Table),
            //new RegexFounding(@"FLAG_SYSTEMES_DROP_COLUMN\s+N?'(?<shema>\S*\.)?(?<name>\S+)'\s*,\s*'(?<col>\S+)'\s*[^,]", SqlAction.DropColumn, TypeObject.Table),
            //new RegexFounding(@"FLAG_SYSTEMES_DROP_COLUMN\s+(N?'(?<shema>\S+)'\s*,\s*)?(?<name>\S+)'\s*,\s*'(?<col>\S+)'", SqlAction.DropColumn, TypeObject.Table),
            //new RegexFounding(@"FLAG_SYSTEMES_DROP_COLUMN\s+(@schema\s*=\s*N?'(?<schema>\S+)'\s*,)?@NomTable\s*=\s*N?'(?<name>\S+)'\s*,\s*@NomColonne\s*=\s*N?'(?<col>\S+)'", SqlAction.DropColumn, TypeObject.Table),
            new RegexFounding(@"ALTER\s+TABLE\s+((?<schema>\S+)\.)?(?<name>\S+)\s+DROP\s+(?!CONSTRAINT)(COLUMN\s+)?(?<col>\S+)\s", SqlAction.DropColumn, TypeObject.Table),
            new RegexFounding(@"SP_RENAME\s+(@\S+\s+=\s+)?N?['""]?((?<schema>\S+?)\.)?(?<name>\S+)['""\s]?", SqlAction.Rename, TypeObject.Table),
            new RegexFounding(@"SP_RENAME\s+(@\S+\s+=\s+)?N?['""]?(?<schema>\S+?)\.(?<name>\S+)\.(?<col>\S+)['""\s]?", SqlAction.RenameColumn, TypeObject.Table),

            new RegexFounding(@"INSERT\s+(INTO\s+)?(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s*(\(|SELECT)", SqlAction.Insert, TypeObject.Table),
            new RegexFounding(@"(UPDATE\s+(?<prefix>\S+)\s+SET[\s\S]*?(FROM|JOIN)\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+\k<prefix>\s?)|(UPDATE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+SET)", SqlAction.Update, TypeObject.Table),
            new RegexFounding(@"DELETE\s+(?<prefix>\S+)\s+(\S+\s+)*?FROM\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+\k<prefix>", SqlAction.Delete, TypeObject.Table),
            new RegexFounding(@"DELETE\s+(?<prefix>\S+)\s+(\S+\s+)*?FROM\s+(\S+\s+)*JOIN\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+\k<prefix>", SqlAction.Delete, TypeObject.Table),
            new RegexFounding(@"DELETE\s+FROM\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)", SqlAction.Delete, TypeObject.Table),
            new RegexFounding(@"DELETE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+WHERE", SqlAction.Delete, TypeObject.Table),
            new RegexFounding(@"TRUNCATE\s+TABLE\s+(?<schema>[a-z]\w*\.)?(?<name>[a-z]\w*)", SqlAction.Delete, TypeObject.Table),

            new RegexFounding(@"CREATE\s+PROCEDURE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Create, TypeObject.Procedure),
            new RegexFounding(@"ALTER\s+PROCEDURE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, TypeObject.Procedure),
            new RegexFounding(@"DROP\s+PROCEDURE\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, TypeObject.Procedure),
            new RegexFounding(@"\s(EXEC|EXECUTE)\s+(@\S+\s+=\s+)?((?<shema>\S+)\.)?\[?(?<name>\w+)", SqlAction.Execute, TypeObject.Procedure),

            new RegexFounding(@"CREATE\s+FUNCTION\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\S+)\s*\(", SqlAction.Create, TypeObject.FunctionTable),
            new RegexFounding(@"ALTER\s+FUNCTION\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\S+)\s*\(", SqlAction.Alter, TypeObject.FunctionTable),
            new RegexFounding(@"DROP\s+FUNCTION\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, TypeObject.FunctionTable),

            new RegexFounding(@"CREATE\s+VIEW\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Create, TypeObject.View),
            new RegexFounding(@"ALTER\s+VIEW\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, TypeObject.View),
            new RegexFounding(@"DROP\s+VIEW\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, TypeObject.View),
            new RegexFounding(@"THEN\s+'ALTER'\s+ELSE\s+'CREATE'\s+END\s+\+\s+'\s+VIEW\s+((?<database>\S+?)\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, TypeObject.View),

            new RegexFounding(@"CREATE\s+TRIGGER\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Create, TypeObject.Trigger),
            new RegexFounding(@"ALTER\s+TRIGGER\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Alter, TypeObject.Trigger),
            new RegexFounding(@"DROP\s+TRIGGER\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, TypeObject.Trigger),

            new RegexFounding(@"CREATE\s+(\S*\s+)?INDEX\s+(?<col>\S+)\s+ON\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+", SqlAction.Create, TypeObject.Index),
            new RegexFounding(@"ALTER\s+\S*\s+INDEX\s+(?<col>\S+)\s+ON\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?(?<name>\S+)\s+", SqlAction.Alter, TypeObject.Index),
            new RegexFounding(@"DROP\s+INDEX\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)\.\[?(?<col>\S+)\s+", SqlAction.Drop, TypeObject.Index),
            new RegexFounding(@"DROP\s+INDEX\s+\[?(?<col>\S+)\s+ON\s+(?<database>\S+?\.)?(?<schema>\S+?\.)?\[?(?<name>\w+)", SqlAction.Drop, TypeObject.Index),

            new RegexFounding(@"CREATE\s+SCHEMA\s+\[?(?<name>[a-z_][\w]*)", SqlAction.Create, TypeObject.Schema),
            new RegexFounding(@"ALTER\s+SCHEMA\s+\[?(?<name>[a-z_][\w]*)TRANSFERT\s", SqlAction.Alter, TypeObject.Schema),
            new RegexFounding(@"DROP\s+SCHEMA\s+(IF\s+EXISTS\s+)?\[?(?<name>[a-z_][\w]*)", SqlAction.Drop, TypeObject.Schema),

            new RegexFounding(@"CREATE\s+TYPE\s+?((?<schema>\S+?)\.)?(?<name>\S+)\s+?AS", SqlAction.Create, TypeObject.TypeTable),
            new RegexFounding(@"DROP\s+TYPE\s+?((?<schema>\S+?)\.)?(?<name>\S+)\s+?", SqlAction.Drop, TypeObject.TypeTable),

            new RegexFounding(@"(FROM\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s*\()|(JOIN\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s*\()", SqlAction.Execute, TypeObject.FunctionTable),
            new RegexFounding(@"(FROM\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s)|(JOIN\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)(?<name>\w+)\s)", SqlAction.Execute, TypeObject.Table), // VA y avoir des ambiguités !!
        };

        /// <summary>
        /// Obtient la liste publique des éléments à chercher quand on a déjà trouvé un ADD COLUMN
        /// </summary>
        private static readonly List<RegexFounding> THELISTCOLUMN = new List<RegexFounding>()
        {
          // add column deuxième colonne de l'instruction
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s((?!ALTER)[\S\s])+?,\s*(?<col>\w+)\s((?!ALTER).)+?NOT\s+NULL", SqlAction.AddColumnNotNull, TypeObject.Table),
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s(\S+?\s+?(?!ALTER|;))*?(([\S\s](?!ALTER|;))+?),\s*?(?<col>\w+?)\s", SqlAction.AddColumn, TypeObject.Table),   
      
          // add column troisième colonne de l'instruction
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s((?!ALTER)[\S\s])+?,((?!ALTER)[\S\s])+?,\s*(?<col>\w+)\s((?!ALTER).)+?NOT\s+NULL", SqlAction.AddColumnNotNull, TypeObject.Table),
          new RegexFounding(@"ALTER\s+TABLE\s+((?<database>\S+?)\.)?((?<schema>\S+?)\.)?(?<name>\S+)\s+ADD\s(\S+?\s+?(?!ALTER|;))*?(([\S\s](?!ALTER|;))+?),\s*?(\S+?\s+?(?!ALTER|;))*?(([\S\s](?!ALTER|;))+?),\s*?(?<col>\w+?)\s", SqlAction.AddColumn, TypeObject.Table),
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
                return name.Replace(".", string.Empty)
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Replace("\"", string.Empty)
                    .Replace(";", string.Empty)
                    .Replace("'", string.Empty);
            }
        }
    }
}
