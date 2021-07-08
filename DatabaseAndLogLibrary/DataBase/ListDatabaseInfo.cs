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

        public override int GetHashCode() => DataBaseName.ToLowerInvariant().GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is ListDatabaseInfo db)
            {
                return this.DataBaseName.ToLowerInvariant().Equals(db.DataBaseName.ToLowerInvariant());
            }

            return this.DataBaseName.ToLowerInvariant().Equals(obj.ToString().ToLowerInvariant());
        }

        public static IEnumerable<ListDatabaseInfo> List(string connexionString)
        {
            var cnn = new DatabaseConnection(connexionString);
            return cnn.Query<ListDatabaseInfo>(ListDatabaseInfo.SQLSelect);
        }

        public static implicit operator string(ListDatabaseInfo db)  => db.ToString();
        public static implicit operator ListDatabaseInfo(string dbName) => new ListDatabaseInfo() { DataBaseName = dbName};

    }
}
