using System;
using System.Collections.Generic;
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

        public bool ObjectDeleted { get; set; }
        public bool ObjectEmpty { get; set; }
        public string ObjectSql { get; set; }

        public string ObjectLockedBy { get; set; }

        public int? ClientCodeId { get; set; }

        public int? ScriptId { get; set; }

        public override string ToString()
            => TypeObjectId == 11 ? ObjectName : $"{ObjectSchema}.{ObjectName}";  // un schéma (11) a juste un nom !
        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.SqlObject;

        public static string SQLSelect => @"
SELECT ObjectId, VersionId, TypeObjectId, ObjectSchema, ObjectName, ObjectDeleted, ObjectEmpty, ObjectSql, ObjectLockedBy, ClientCodeId, ScriptId
FROM dbo.Object o
WHERE VersionId = @VersionId
  AND o.ObjectDeleted = 0
";

        public static string SQlSelectWithVersionAndType => SQLSelect + " AND TypeObjectId = @TypeObjectId";
        public static string SQLInsert => @"
INSERT INTO dbo.Object (VersionId, TypeObjectId, ObjectSchema, ObjectName, ObjectDeleted, ObjectEmpty, ObjectSql, ObjectLockedBy, ClientCodeId, ScriptId
) VALUES (
@VersionId, @TypeObjectId, @ObjectSchema, @ObjectName, @ObjectDeleted, @ObjectEmpty, @ObjectSql, @ObjectLockedBy, @ClientCodeId, @ScriptId
);
SELECT TOP 1 COALESCE(SCOPE_IDENTITY(), @@IDENTITY) AS [Key];
";

        public static string SQLDelete => @"
UPDATE dbo.Object 
SET ObjectDeleted = 1
WHERE ObjectId = @ObjectId
";

    }
}
