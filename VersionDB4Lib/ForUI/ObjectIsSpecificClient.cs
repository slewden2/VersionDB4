using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4Lib.ForUI
{
    public class ObjectIsSpecificClient : Object, IPresentable
    {
        public string ClientCodeName { get; set; }

        public override string ToString()
        {
            if (ClientCodeId.HasValue && ClientCodeId.Value > 0)
            {
                string sch = string.IsNullOrWhiteSpace(ObjectSchema) ? string.Empty : $"{ObjectSchema}.";
                return $"Implémentation pour {ClientCodeName} de {sch}{ObjectName}";
            }
            else
            {
                return base.ToString();
            }
        }

        public override ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.SQlObjectCustomClient;

        public static new string SQLSelect => @"
SELECT o2.[ObjectId], o2.[VersionId], o2.[TypeObjectId], o2.[ObjectSchema], o2.[ObjectName]
  , o2.[ObjectDeleted], o2.[ObjectEmpty], o2.[ObjectSql], o2.[ObjectLockedBy], o2.[ClientCodeId], o2.[ObjectColumn]
  , o2.ClientCodeId, cc.ClientCodeName 
FROM dbo.[Object] o
INNER JOIN dbo.[Object] o2 ON o.ObjectSchema = o2.ObjectSchema AND o.ObjectName = o2.ObjectName AND ISNULL(o.ObjectColumn, 0) = ISNULL(o2.ObjectColumn, 0) AND o.VersionId = o2.VersionId
INNER JOIN dbo.ClientCode cc ON o2.ClientCodeId = cc.ClientCodeId
WHERE o.ObjectId = @objectId
  AND o2.ObjectDeleted = 0
;
";
    }
}
