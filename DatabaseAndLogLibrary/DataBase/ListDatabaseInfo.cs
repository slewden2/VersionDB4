using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAndLogLibrary.DataBase
{
    public class ListDatabaseInfo
    {
        public string DataBaseName { get; set; }


        public static string SQLSelect => @"
SELECT name as DataBaseName FRom sys.databases
;
";
        public override string ToString() => DataBaseName;


        public static IEnumerable<ListDatabaseInfo> List(string connexionString)
        {
            var cnn = new DatabaseConnection(connexionString);
            return cnn.Query<ListDatabaseInfo>(ListDatabaseInfo.SQLSelect);
        }

    }
}
