using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;
using Version = VersionDB4Lib.CRUD.Version;

namespace VersionDB4Lib.ForUI
{
    public class VersionScriptCounter : Version, IPresentable, ICounter
    {
        public int Count { get; set; }

        public int CountObject { get; set; }
        public bool IsLastVersion { get; set; }


        public static new string SQLSelect => @"
SELECT v.VersionId, v.ProjectId, v.VersionPrincipal, v.VersionSecondary, v.VersionIsLocked
  , ISNULL(ox.nb, 0) AS [Count] 
  , ISNULL(oy.nb, 0) AS [CountObject] 
  , CASE WHEN v.VersionPrincipal * 100000 + v.VersionSecondary = (SELECT MAX(vx.VersionPrincipal * 100000 + vx.VersionSecondary) AS MaxVersion 
                                                                  FROM dbo.[Version] vx 
                                                                  WHERE vx.ProjectId = @ProjectId) THEN 1 ELSE 0 END AS IsLastVersion
FROM dbo.Version v
LEFT JOIN (SELECT s.VersionId, COUNT(*) AS nb 
           FROM dbo.Script s 
           GROUP BY s.VersionId) ox ON v.VersionId = ox.VersionId
LEFT JOIN (SELECT o.VersionId, COUNT(*) AS nb 
           FROM dbo.Object o 
           WHERE o.ObjectDeleted = 0
           GROUP BY o.VersionId) oy ON v.VersionId = oy.VersionId
WHERE v.ProjectId = @ProjectId
";

        public new ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.VersionScript;

    }
}
