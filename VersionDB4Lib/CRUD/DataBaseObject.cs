using System;
using System.Linq;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
     /// <summary>
    /// Un objet pointé dans un script
    /// </summary>
    public class DataBaseObject
    {
        /// <summary>
        /// Clé de l'objet d'un base de données
        /// </summary>
        public int DatabaseObjectId { get; set; }

        /// <summary>
        /// La clé du script associé
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// La clé du type d'objet
        /// </summary>
        public int SqlWhatId { get; set; }

        /// <summary>
        /// La base de données cocernée par cet objet
        /// </summary>
        public string DatabaseObjectDatabase { get; set; }

        /// <summary>
        /// Le schéma de l'objet
        /// </summary>
        public string DatabaseObjectSchema { get; set; }

        /// <summary>
        /// Le nom de l'objet
        /// </summary>
        public string DatabaseObjectName { get; set; }


        public SqlWhat GetWhat() => SqlWhat.List().FirstOrDefault(x => x.SqlWhatId == SqlWhatId);
        public void SetWhat(SqlWhat value) => SqlWhatId = (value == null ? 0 : value.SqlWhatId);

        public override string ToString() => $"{GetWhat().SqlWhatName} {EnumHelper.ToString(DatabaseObjectDatabase, DatabaseObjectSchema, DatabaseObjectName)}";

        public static string SQLSelect
            => @"
SELECT DatabaseObjectId, ScriptId, SqlWhatId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName
FROM dbo.DatabaseObject
";


    }
}