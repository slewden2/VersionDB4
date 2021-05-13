using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAndLogLibrary.DataBase;

namespace VersionDB4Lib.CRUD
{
    public class ClientCode
    {
        private static List<ClientCode> list = null;

        public int ClientCodeId { get; set; }
        public string ClientCodeName { get; set; }

        public static string SQLSelect => @"
SELECT ClientCodeId, ClientCodeName FROM dbo.ClientCode
";
        public override string ToString() => ClientCodeName;


        public static List<ClientCode> List()
        {
            if (list == null)
            {
                using var cnn = new DatabaseConnection();
                list = cnn.Query<ClientCode>(ClientCode.SQLSelect).ToList();
            }

            return list;
        }
    }

}
