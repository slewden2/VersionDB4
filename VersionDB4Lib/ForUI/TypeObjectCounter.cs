﻿using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.ForUI
{
    public class TypeObjectCounter : TypeObject
    {
        public int Count { get; set; }


        public static new string SQLSelect => @"
SELECT t.TypeObjectId, t.TypeObjectName, t.TypeObjectSqlServerCode, t.TypeObjectPlurial, t.TypeObjectPrestentOrder 
  , ox.nb AS [Count] 
FROM dbo.TypeObject t
LEFT JOIN (SELECT o.TypeObjectId, COUNT(*) AS nb 
           FROM dbo.Object o 
           WHERE o.VersionId = @VersionId
           GROUP BY o.TypeObjectId) ox ON t.TypeObjectId = ox.TypeObjectId
;
";
    }
}