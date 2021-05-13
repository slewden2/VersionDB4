using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    public class TypeObject : IPresentable
    {
        public int TypeObjectId { get; set; }
        public string TypeObjectName { get; set; }
        public string TypeObjectSqlServerCode { get; set; }
        public string TypeObjectPlurial { get; set; }
        public int TypeObjectPrestentOrder { get; set; }

        public static string SQLSelect => @"
SELECT TypeObjectId, TypeObjectName, TypeObjectSqlServerCode, TypeObjectPlurial, TypeObjectPrestentOrder FROM dbo.TypeObject
";
        public override string ToString() => TypeObjectPlurial;
        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.SqlGroup;
    }
}
