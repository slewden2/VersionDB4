using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace DatabaseAndLogLibrary.DataBase
{
    public static class SqlFormat
    {
        public const int SQLBULKINSERTSIZE = 1000;

        public static string String(string text, bool nullable = false)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return nullable ? "NULL": "''";
            }

            return $"'{text.Replace("'", "''")}'";
        }
        public static string ForeignKey(int n)   => (n <= 0) ? "NULL" : n.ToString();
        public static string ForeignKey(int? n)  => (!n.HasValue || n <= 0) ? "NULL" : n.ToString();
        public static string Integer(int n) => n.ToString();
        public static string Integer(int? n) => n.HasValue ? n.ToString() : "NULL";
        public static string Boolean(bool n) => n ? "1" : "0";
        public static string Number(double n) => n.ToString().Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, string.Empty).Replace(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
        public static string Number(decimal n) => n.ToString().Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, string.Empty).Replace(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
        public static string Number(float n) => n.ToString().Replace(NumberFormatInfo.CurrentInfo.NumberGroupSeparator, string.Empty).Replace(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
    }
}
