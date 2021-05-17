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
        public int TypeObjectId { get; set; }

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

        /// <summary>
        /// Le nom de l'objet pour les colonne, index et contraintes
        /// </summary>
        public string DatabaseObjectColumn { get; set; }


        public TypeObject GetWhat() => TypeObject.List().FirstOrDefault(x => x.TypeObjectId == TypeObjectId);
        public void SetWhat(TypeObject value) => TypeObjectId = (value == null ? 0 : value.TypeObjectId);

        public override string ToString() 
            => $"{GetWhat().TypeObjectName} {EnumHelper.ToString(DatabaseObjectDatabase, DatabaseObjectSchema, DatabaseObjectName)}";

        public static string SQLSelect
            => @"
SELECT DatabaseObjectId, ScriptId, TypeObjectId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName, DatabaseObjectColumn
FROM dbo.DatabaseObject
";


    }
}