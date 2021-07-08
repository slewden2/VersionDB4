using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DatabaseAndLogLibrary.DataBase;

namespace VersionDB4Lib.Business.Scripting
{
    public class ScriptToDisk
    {
        public ScriptToDisk(int versionId, string folderFullPath)
        {
            VersionId = versionId;
            FullPath = folderFullPath;
        }

        public int VersionId { get; private set; }
        public string FullPath { get; private set; }

        public void WriteScripts()
        {
            using var cnn = new DatabaseConnection();
            var lst = cnn.Query<ScriptDisk>(ScriptDisk.SQLSelect, new { VersionId });
            if (Directory.Exists(FullPath))
            {
                Directory.Delete(FullPath);
            }

            Directory.CreateDirectory(FullPath);
            foreach(var script in lst)
            {
                script.ToDisk(FullPath);
            }
        }


        private class ScriptDisk
        {
            public string FileName { get; set; }
            public string ScriptText { get; set; }

            public static string SQLSelect
                => @"
SELECT 'Script V' + FORMAT(v.VersionPrincipal, '0') + '.' +  FORMAT(v.VersionSecondary, '00') + '.' +  FORMAT(s.ScriptOrder, '000') + '.sql' AS [FileName], s.ScriptText
FROM dbo.Script s
INNER JOIN dbo.Version v on s.VersionId = v.VersionId
WHERE s.VersionId = @VersionId
ORDER BY ScriptOrder
;
";
            public void ToDisk(string folder)
            {
                string fullFileName = Path.Combine(folder, FileName);
                File.WriteAllText(fullFileName, ScriptText, Encoding.UTF8);
            }
        }
    }
}
