using System;
using System.Linq;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
     /// <summary>
    /// Un objet point� dans un script
    /// </summary>
    public class DataBaseObject
    {
        /// <summary>
        /// Cl� de l'objet d'un base de donn�es
        /// </summary>
        public int DatabaseObjectId { get; set; }

        /// <summary>
        /// La cl� du script associ�
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// La cl� du type d'objet
        /// </summary>
        public int SqlWhatId { get; set; }

        /// <summary>
        /// La base de donn�es cocern�e par cet objet
        /// </summary>
        public string DatabaseObjectDatabase { get; set; }

        /// <summary>
        /// Le sch�ma de l'objet
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