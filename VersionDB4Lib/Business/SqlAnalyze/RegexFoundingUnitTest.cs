using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace VersionDB4Lib.Business.SqlAnalyze
{
    public class RegexFoundingUnitTest
    {
        public int SqlActionId { get; set; }

        public int SqlWhatId { get; set; }

        public string Text { get; set; }

        public bool Result { get; set; }


        public static IEnumerable<RegexFoundingUnitTest> Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                string fileContent = File.ReadAllText(fileName);
                var obj = JArray.Parse(fileContent);
                return obj.ToObject<IEnumerable<RegexFoundingUnitTest>>();
            }
            else
            {
                return new List<RegexFoundingUnitTest>();
            }
        }

        public static void Save(string fileName, IEnumerable<RegexFoundingUnitTest> list)
        {
            var json = JArray.FromObject(list);
            File.WriteAllText(fileName, json.ToString());
        }
    }
}
