using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VersionDB4Lib.Business;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.ForUI
{
    public class ResumeVersionActions 
    {
        public int SqlActionId { get; set; }
        public int TypeObjectId { get; set; }
        public int Count { get; set; }

        public SqlAction GetAction() => SqlAction.List().FirstOrDefault(x => x.SqlActionId == SqlActionId);

        public TypeObject GetWhat() => TypeObject.List().FirstOrDefault(x => x.TypeObjectId == TypeObjectId);

        public override string ToString() => $"{GetAction().SqlActionTitle} {GetWhat().TypeObjectName.ToLower()}";

        public static string SQLSelect => @"
SELECT r.SqlActionId, r.TypeObjectId, COUNT(*) AS [Count]
FROM dbo.[Resume] r 
INNER JOIN dbo.script s ON s.ScriptId = r.ScriptId
WHERE r.SqlActionId > 0
  AND s.VersionId = @VersionId
GROUP BY r.SqlActionId, r.TypeObjectId
;
";
    }
}
