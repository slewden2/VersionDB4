using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    public class Object : IPresentable
    {
        public int ObjectId { get; set; }
        public int VersionId { get; set; }
        public int TypeObjectId { get; set; }
        public string ObjectSchema { get; set; }
        public string ObjectName { get; set; }
        public string ObjectColumn { get; set; }  // nom utilisé pour les colonnes, les index et les contraintes
        public bool ObjectDeleted { get; set; }
        public bool ObjectEmpty { get; set; }
        public string ObjectSql { get; set; }
        public string ObjectLockedBy { get; set; }
        public int? ClientCodeId { get; set; }

        public string TypeObjectName() => TypeObject.List().First(x => x.TypeObjectId == TypeObjectId).TypeObjectName;

        public ObjectIdentifier Identifier => new ObjectIdentifier(ObjectName) { Schema = ObjectSchema, Column = ObjectColumn };


        public override string ToString()
        {
            if (TypeObjectId == TypeObject.Schema)
            {
                return ObjectName;
            }
            else if (TypeObjectId == TypeObject.Index || TypeObjectId == TypeObject.ForeignKey || TypeObjectId == TypeObject.Constraint)
            {
                string sch = string.IsNullOrWhiteSpace(ObjectSchema) ? string.Empty : $"{ObjectSchema}.";
                return $"{ObjectColumn} de la table {sch}{ObjectName}";
            }
            else
            {
                string sch = string.IsNullOrWhiteSpace(ObjectSchema) ? string.Empty : $"{ObjectSchema}.";
                return $"{sch}{ObjectName}";
            }
        }

        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.SqlObject;

        public static string SQLSelect => @"
SELECT ObjectId, VersionId, TypeObjectId, ObjectSchema, ObjectName, ObjectDeleted, ObjectEmpty, ObjectSql, ObjectLockedBy, ClientCodeId, ObjectColumn
FROM dbo.Object o
WHERE VersionId = @VersionId
  AND o.ObjectDeleted = 0
";

        public static string SQlSelectWithVersionAndType => SQLSelect + " AND TypeObjectId = @TypeObjectId";
        public static string SQLInsert => @"
INSERT INTO dbo.Object (VersionId, TypeObjectId, ObjectSchema, ObjectName, ObjectDeleted, ObjectEmpty, ObjectSql, ObjectLockedBy, ClientCodeId, ObjectColumn
) VALUES (
@VersionId, @TypeObjectId, @ObjectSchema, @ObjectName, @ObjectDeleted, @ObjectEmpty, @ObjectSql, @ObjectLockedBy, @ClientCodeId, @ObjectColumn
);
SELECT TOP 1 COALESCE(SCOPE_IDENTITY(), @@IDENTITY) AS [Key];
";
        public static string SQLUpdate => @"
UPDATE dbo.Object 
SET ObjectDeleted = @ObjectDeleted
  , ObjectEmpty   = @ObjectEmpty
  , ObjectSql     = @ObjectSql
  , ClientCodeId  = @ClientCodeId
WHERE ObjectId = @ObjectId
";

        public static string SQLDelete => @"
UPDATE dbo.Object 
SET ObjectDeleted = 1
WHERE ObjectId = @ObjectId
";
    }
}
