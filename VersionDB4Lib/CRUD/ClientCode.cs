using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAndLogLibrary.DataBase;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Représente un objet pour identifier un code client
    /// </summary>
    public class ClientCode
    {
        private static List<ClientCode> list = null;

        public int ClientCodeId { get; set; }
        public string ClientCodeName { get; set; }

        public static string SQLSelect => @"
SELECT ClientCodeId, ClientCodeName FROM dbo.ClientCode
";

        public static List<ClientCode> List()
        {
            EnsureList();

            return list;
        }

        public static string Name(int clientCodeId)
        {
            EnsureList();

            var code = list.FirstOrDefault(x => x.ClientCodeId == clientCodeId);
            return code?.ClientCodeName ?? string.Empty;
        }

        private static void EnsureList()
        {
            if (list == null)
            {
                using var cnn = new DatabaseConnection();
                list = cnn.Query<ClientCode>(ClientCode.SQLSelect).ToList();
            }
        }
        public override string ToString() => ClientCodeName;

        public override int GetHashCode() => ClientCodeId.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is ClientCode cl)
            {
                return this.ClientCodeId == cl.ClientCodeId;
            }

            return false;
        }
    }
}
