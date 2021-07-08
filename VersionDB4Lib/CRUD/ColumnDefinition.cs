using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib.CRUD
{
    public class ColumnDefinition
    {
        public int ColumnId { get; set; }
        public int ObjectId { get; set; }
        public string ColumnName { get; set; }

        public string ColumnType { get; set; }
        public bool ColumnMandatory { get; set; }

        public static string SQLDelete
            => "DELETE FROM dbo.[Column] WHERE ObjectId = @ObjectId";

        public static string SQLInsert
            => "INSERT INTO dbo.[column] (ObjectId, ColumnName, ColumnType, ColumnMandatory) VALUES (@ObjectId, @ColumnName, @ColumnType, @ColumnMandatory);";
    }
}
