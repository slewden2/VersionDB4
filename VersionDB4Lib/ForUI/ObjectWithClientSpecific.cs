using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4Lib.ForUI
{
    public class ObjectWithClientSpecific : Object
    {
        private List<ObjectIsSpecificClient> clients = null;

        public int NumberOfClientImplementation { get; set; }

        public List<ObjectIsSpecificClient> ObjectSpecificClientList(bool forceRefill = false)
        {
            if (clients == null || forceRefill)
            {
                using var cnn = new DatabaseConnection();
                clients = cnn.Query<ObjectIsSpecificClient>(ObjectIsSpecificClient.SQLSelect, new { ObjectId }).OrderBy(x => x.ClientCodeName).ToList();
            }

            return clients;
        }

        public IEnumerable<ClientCode> ClientCodeList()
        {
            if (clients == null)
            {
                using var cnn = new DatabaseConnection();
                clients = cnn.Query<ObjectIsSpecificClient>(ObjectIsSpecificClient.SQLSelect, new { ObjectId }).OrderBy(x => x.ClientCodeName).ToList();
            }

            return clients.Where(x => x.ClientCodeId.HasValue && x.ClientCodeId> 0)
                .Select(x => new ClientCode() { ClientCodeId =  x.ClientCodeId ?? 0, ClientCodeName = x.ClientCodeName});
        }

        public static new string SQLSelect => @"
SELECT o.ObjectId, o.VersionId, o.TypeObjectId, o.ObjectSchema, o.ObjectName, o.ObjectDeleted, o.ObjectEmpty, o.ObjectSql, o.ObjectLockedBy, o.ClientCodeId, o.ObjectColumn
 , o2.NumberOfClientImplementation
FROM dbo.[Object] o
 LEFT JOIN (SELECT ox.ObjectSchema, ox.ObjectName, ox.ObjectColumn, COUNT(*) AS NumberOfClientImplementation
            FROM dbo.[Object] ox
            WHERE ox.ClientCodeId IS NOT NULL
              AND ox.VersionId = @VersionId
            GROUP BY ox.ObjectSchema, ox.ObjectName, ox.ObjectColumn
           ) o2 ON o.ObjectSchema = o2.ObjectSchema AND o.ObjectName = o2.ObjectName AND ISNULL(o.ObjectColumn, 0) = ISNULL(o2.ObjectColumn, 0)
WHERE o.ObjectDeleted = 0
  AND o.ClientCodeId IS NULL
  AND VersionId = @VersionId
  AND TypeObjectId = @TypeObjectId
;
";

        public static new string SQLDelete => @"
UPDATE dbo.Object 
SET ObjectDeleted = 1
WHERE ObjectId = @ObjectId

";
    }
}
