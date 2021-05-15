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
        public int SqlWhatId { get; set; }
        public int Count { get; set; }

        public SqlAction GetAction() => SqlAction.List().FirstOrDefault(x => x.SqlActionId == SqlActionId);

        public SqlWhat GetWhat() => SqlWhat.List().FirstOrDefault(x => x.SqlWhatId == SqlWhatId);

        public override string ToString() => $"{GetAction().SqlActionTitle} {GetWhat().SqlWhatName}";

        public static string SQLSelect => @"
SELECT r.SqlActionId, r.SqlWhatId, COUNT(*) AS [Count]
FROM dbo.[Resume] r 
INNER JOIN dbo.script s ON s.ScriptId = r.ScriptId
WHERE r.SqlActionId > 0
  AND s.VersionId = @VersionId
GROUP BY r.SqlActionId, r.SqlWhatId
;
";
    }
}
