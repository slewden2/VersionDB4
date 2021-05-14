using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;
using Version = VersionDB4Lib.CRUD.Version;

namespace VersionDB4Lib.ForUI
{
    public class VersionScriptCounter : Version, IPresentable
    {
        public int Count { get; set; }
       
        public static new string SQLSelect => @"
SELECT v.VersionId, v.ProjectId, v.VersionPrincipal, v.VersionSecondary
  , ISNULL(ox.nb, 0) AS [Count] 
FROM dbo.[Version] v
LEFT JOIN (SELECT s.VersionId, COUNT(*) AS nb 
           FROM dbo.Script s 
           GROUP BY s.VersionId) ox ON v.VersionId = ox.VersionId
WHERE v.ProjectId = @ProjectId
";

        public override string ToString()
        {
            string tit = base.ToString();
            if (Count > 0)
            {
                tit += $" ({Count})";
            }

            return tit;
        }

        public new ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.VersionScript;

    }
}
