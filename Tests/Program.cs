using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business.Scripting;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using Object = VersionDB4Lib.CRUD.Object;

namespace Tests
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");


            using var conn = new DatabaseConnection();
            string sql = @"
SELECT ObjectId, VersionId, TypeObjectId, ObjectSchema, ObjectName, ObjectDeleted, ObjectEmpty, ObjectSql, ObjectLockedBy, ClientCodeId, ObjectColumn
FROM dbo.[object] o 
INNER JOIN TypeObject t ON o.TypeObjectId = t.TypeObjectId 
WHERE t.TypeObjectId = 9  --- t.TypeObjectNeedColumnDefinition = 1
";
            List<ColumnDefinition> colums = new List<ColumnDefinition>();

            Regex reg = new Regex(RegexFounding.REGEXPCOLUMNLIST);
            var tables = conn.Query<Object>(sql);
            foreach(var obj in tables)
            {
                var mcll = reg.Matches(obj.ObjectSql);
                if (mcll != null && mcll.Count > 0)
                {
                    foreach (Match match in mcll)
                    {
                        var column = new ColumnDefinition()
                        {
                            ColmnName = match.Groups["column"]?.Value,
                            ColumnType = match.Groups["type"]?.Value,
                            ColumnMandatory = ConvertMandatorySqlToBool(match.Groups["mandatory"]?.Value),
                            ObjectId = obj.ObjectId
                        };
                }
                }
            }



            ////var codes = conn.Query<CodeClient>(CodeClient.SQLSelect);
            ////foreach (var c in codes)
            ////{
            ////    Console.WriteLine($"Code Client {c}");
            ////}

            ////var types = conn.Query<TypeObject>(TypeObject.SQLSelect);
            ////foreach (var t in types)
            ////{
            ////    Console.WriteLine($"Type {t}");
            ////}

            ////var projects = conn.Query<Project>(Project.SQLSelect);
            ////Version maxV = Version.Empty;
            ////foreach (var p in projects)
            ////{
            ////    maxV = Version.Empty;

            ////    Console.WriteLine($"Project {p}");
            ////    var versions = conn.Query<Version>(Version.SQLSelect, new { ProjectId = p.ProjectId });
            ////    foreach (var v in versions)
            ////    {
            ////        Console.WriteLine($"  Version {v}");
            ////        if (v> maxV)
            ////        {
            ////            maxV = v;
            ////        }
            ////    }

            ////    ////if (p.ProjectId == 2 && maxV != Version.Empty)
            ////    ////{
            ////    ////    maxV++;
            ////    ////    maxV.VersionId = conn.ExecuteScalar(Version.SQLInsert, maxV);
            ////    ////    Console.WriteLine($"  Version {maxV} id={maxV.VersionId}");
            ////    ////}
            ////}

            ////////var baseClient = new Base()
            ////////{
            ////////    ClientCodeId = 1,
            ////////    VersionId = maxV.VersionId,
            ////////    BaseConnectionString = "Data Source=localhost;Initial Catalog=VersionDB4;integrated security=SSPI"
            ////////};
            ////////baseClient.BaseId = conn.ExecuteScalar(Base.SQLInsert, baseClient);
            ////////Console.WriteLine($"Client  {baseClient} id={baseClient.BaseId}");

            ////var objects = conn.Query<Object>(AnalyzeBase.SQLLoadObjects, new { VersionId = 3 });
            ////foreach (var o in objects)
            ////{
            ////    //o.ObjectId = conn.ExecuteScalar(Object.SQLInsert, o);
            ////    Console.WriteLine($"  Version {o}");
            ////}

        }


        private static bool ConvertMandatorySqlToBool(string mandatoryText)
        {
            if (string.IsNullOrWhiteSpace(mandatoryText))
            { // pas de text donc ce n'est pas obligatoire
                return false;
            }

            if (mandatoryText.ToLowerInvariant() == "identity")
            { // identity implique que la colonne est obligatoire
                return true;
            }

            if (mandatoryText.ToLowerInvariant() == "null")
            { // null tout seul implique que la colonne n'est pas obligatoire
                return false;
            }

            // ici on est forcement dans le cas "NOT NULL" (avec autant d'espaces que voulu entre les mots)
            return true;
        }
    }
}
