using System;
using System.Collections.Generic;
using System.Text;
using Version = VersionDB4Lib.CRUD.Version;

namespace VersionDB4Lib.ForUI
{
    public class VersionObjectCounter : Version, ICounter
    {
        public int Count { get; set; }

        public static new string SQLSelect => @"
SELECT v.VersionId, v.ProjectId, v.VersionPrincipal, v.VersionSecondary
  , ISNULL(ox.nb, 0) AS [Count] 
FROM dbo.[Version] v
LEFT JOIN (SELECT o.VersionId, COUNT(*) AS nb 
           FROM dbo.Object o 
           GROUP BY o.VersionId) ox ON v.VersionId = ox.VersionId
WHERE v.ProjectId = @ProjectId
";
    }
}
