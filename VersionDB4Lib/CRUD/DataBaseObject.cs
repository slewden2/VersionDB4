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
        public int TypeObjectId { get; set; }

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