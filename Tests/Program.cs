using System;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;
using Version = VersionDB4Lib.CRUD.Version;
using Object = VersionDB4Lib.CRUD.Object;


namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            using var conn = new DatabaseConnection();


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

            var objects = conn.Query<Object>(AnalyzeBase.SQLLoadObjects, new { VersionId = 3 });
            foreach (var o in objects)
            {
                //o.ObjectId = conn.ExecuteScalar(Object.SQLInsert, o);
                Console.WriteLine($"  Version {o}");
            }

        } 
    }
}
