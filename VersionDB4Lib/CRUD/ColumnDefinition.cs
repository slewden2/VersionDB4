using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib.CRUD
{
    public class ColumnDefinition
    {
        public int CoolumnsId { get; set; }
        public int ObjectId { get; set; }
        public string ColmnName { get; set; }

        public string ColumnType { get; set; }
        public bool ColumnMandatory { get; set; }

        public static string SQLDelete
            => "DELETE FROM dbo.ColumnDefinition WHERE ObjectId = @ObjectId";
    }
}
