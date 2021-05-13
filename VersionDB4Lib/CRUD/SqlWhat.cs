using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAndLogLibrary.DataBase;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Les types d'objets SQL gérés
    /// 
    /// 0 = None
    /// 1 = Procedure
    /// 2 = Function
    /// 3 = View
    /// 4 = Trigger
    /// 5 = Index
    /// 6 = Schema
    /// 7 = Table
    /// 8 = Type
    /// 9 = Constraint
    /// </summary>
    public class SqlWhat
    {
        private static List<SqlWhat> list = null;

        /// <summary>
        /// Clé du types d'objets SQL
        /// </summary>
        public int SqlWhatId { get; set; }

        /// <summary>
        /// Nom du type d'objet SQL
        /// </summary>
        public string SqlWhatName { get; set; }

        /// <summary>
        /// Titlre du type d'objet (Genre : 'Les fonctions'
        /// </summary>
        public string SqlWhatTitle { get; set; }

        public override string ToString() => SqlWhatName;

        public static string SQLSelect
            => @"
SELECT SqlWhatId, SqlWhatName, SqlWhatTitle 
FROM dbo.SqlWhat
";

        public static int None => 0;
        public static int Procedure => 1;
        public static int Function => 2;
        public static int View => 3;
        public static int Trigger => 4;
        public static int Index => 5;

        public static int Schema => 6;
        public static int Table => 7;
        public static int Type => 8;
        public static int Constraint => 9;

        public static List<SqlWhat> List()
        {
            if (list == null)
            {
                using var cnn = new DatabaseConnection();
                list = cnn.Query<SqlWhat>(SqlWhat.SQLSelect).ToList();
            }

            return list;
        }
    }
}