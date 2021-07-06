using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.UI;

namespace VersionDB4Lib.ForUI
{
    public class ScriptWithStatus : Script
    {




        public new static string SQLSelect
    => @"
SELECT ScriptId, VersionId, ScriptOrder, ScriptText
FROM dbo.Script
";
    }
}
