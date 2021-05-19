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

        public static string SQLSelect
            => @"
SELECT DatabaseObjectId, ScriptId, TypeObjectId, DatabaseObjectDataBase, DatabaseObjectSchema, DatabaseObjectName, DatabaseObjectColumn
FROM dbo.DatabaseObject
";


        public TypeObject GetWhat() => TypeObject.List().FirstOrDefault(x => x.TypeObjectId == TypeObjectId);
        public void SetWhat(TypeObject value) => TypeObjectId = (value == null ? 0 : value.TypeObjectId);

        public override string ToString() 
            => $"{GetWhat().TypeObjectName} {EnumHelper.ToString(DatabaseObjectDatabase, DatabaseObjectSchema, DatabaseObjectName)}";


        public override int GetHashCode()
            => HashCode.Combine(ScriptId, TypeObjectId, DatabaseObjectDatabase, DatabaseObjectSchema, DatabaseObjectName, DatabaseObjectColumn);

        public override bool Equals(object obj)
        {
            if (obj is DataBaseObject bl)
            {
                return this.ScriptId == bl.ScriptId
                    && this.TypeObjectId == bl.TypeObjectId
                    && (this.DatabaseObjectDatabase == bl.DatabaseObjectDatabase || (string.IsNullOrWhiteSpace(this.DatabaseObjectDatabase) && string.IsNullOrWhiteSpace(bl.DatabaseObjectDatabase)))
                    && (this.DatabaseObjectSchema == bl.DatabaseObjectSchema || (string.IsNullOrWhiteSpace(this.DatabaseObjectSchema) && string.IsNullOrWhiteSpace(bl.DatabaseObjectSchema)))
                    && (this.DatabaseObjectName == bl.DatabaseObjectName || (string.IsNullOrWhiteSpace(this.DatabaseObjectName) && string.IsNullOrWhiteSpace(bl.DatabaseObjectName)))
                    && (this.DatabaseObjectColumn == bl.DatabaseObjectColumn || (string.IsNullOrWhiteSpace(this.DatabaseObjectColumn) && string.IsNullOrWhiteSpace(bl.DatabaseObjectColumn)));
            }

            return false;
        }


    }
}