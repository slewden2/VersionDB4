using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.ForUI
{
    public class ResumeVersionValidation
    {
        public byte ResumeManualValidationCode { get; set; }

        public int Count { get; set; }

        public EValidation GetResumeManualValidationCode() => (EValidation)ResumeManualValidationCode;

        public Color Color => GetResumeManualValidationCode().GetColor();

        public override string ToString() => GetResumeManualValidationCode().Libelle();


        public static string SQLSelect => @"
SELECT r.ResumeManualValidationCode, COUNT(*) AS [Count]
FROM dbo.[Resume] r 
INNER JOIN dbo.script s ON s.ScriptId = r.ScriptId
WHERE r.SqlActionId > 0
  AND s.VersionId = @VersionId
GROUP BY r.ResumeManualValidationCode
;";
    }
}
