using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.ForUI
{
    public class TypeObjectCounter : TypeObject, ICounter
    {
        public int Count { get; set; }


        public static string SQLSelect => @"
SELECT t.TypeObjectId, t.TypeObjectName, t.TypeObjectSqlServerCode, t.TypeObjectPlurial, t.TypeObjectPrestentOrder 
  , oy.nb AS [Count] 
FROM dbo.TypeObject t
LEFT JOIN (SELECT o.TypeObjectId, COUNT(*) AS nb 
           FROM (SELECT DISTINCT ox.TypeObjectId, ox.ObjectSchema, ox.ObjectName, ox.ObjectColumn 
                 FROM dbo.[Object] ox
                 WHERE ox.VersionId = @VersionId
                   AND ox.ObjectDeleted = 0
                   AND ClientCodeId IS NULL
                 ) o 
           GROUP BY o.TypeObjectId) oy ON t.TypeObjectId = oy.TypeObjectId
WHERE t.TypeObjectId > 0
;
";
    }
}
