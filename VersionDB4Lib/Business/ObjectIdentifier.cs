using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib.Business
{
    public class ObjectIdentifier
    {
        public ObjectIdentifier(string name) 
            => this.Name = name;

        public string DataBase { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Column { get; set; }


        public override string ToString()
        {
            string db = string.IsNullOrWhiteSpace(DataBase) ? string.Empty : $"{DataBase}.";
            string sh = string.IsNullOrWhiteSpace(Schema) ? string.Empty : $"{Schema}.";
            string col = string.IsNullOrWhiteSpace(Column) ? string.Empty : $".{Column}";
            return $"{db}{sh}{Name}{col}";
        }

        public override int GetHashCode()
            => HashCode.Combine(Schema, Name, Column);

        public override bool Equals(object obj)
        {
            if (obj is ObjectIdentifier other)
            {
                return ((string.IsNullOrWhiteSpace(other.DataBase) && string.IsNullOrWhiteSpace(this.DataBase)) || other.DataBase == this.DataBase)
                      && ((string.IsNullOrWhiteSpace(other.Schema) && string.IsNullOrWhiteSpace(this.Schema)) || other.Schema == this.Schema)
                      && other.Name == this.Name
                      && ((string.IsNullOrWhiteSpace(other.Column) && string.IsNullOrWhiteSpace(this.Column)) || other.Column == this.Column);
            }

            return false;
        }
    }
}
