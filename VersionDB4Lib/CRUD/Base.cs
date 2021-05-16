using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    public class Base : IPresentable
    {
        public int BaseId { get; set; }
        public int ClientCodeId { get; set; }
 
        public string BaseName { get; set; }
        public string BaseConnectionString { get; set; }

        public override string ToString()
        {
            var cni = new ConnectionStringInfo(BaseConnectionString);
            return cni.Base;
        }

        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.Client;

        public static string SQLSelect
            => @"
SELECT b.BaseId, b.ClientCodeId, b.BaseName, b.BaseConnectionString 
FROM dbo.Base b
";

        public static string SQLInsert => @"
INSERT INTO dbo.Base (
ClientCodeId, BaseName, BaseConnectionString
) VALUES (
@ClientCodeId, @BaseName, @BaseConnectionString
);
SELECT TOP 1 COALESCE(SCOPE_IDENTITY(), @@IDENTITY) AS [Key];
";
        public static string SQLUpdate => @"
UPDATE dbo.Base 
SET 
  ClientCodeId = @ClientCodeId, 
  BaseName     = @BaseName, 
  BaseConnectionString = @BaseConnectionString
WHERE BaseId = @BaseId
;
";
        public static string SQLDelete => @"
DELETE FROM dbo.Base 
WHERE BaseId = @BaseId
;
";
    }
}
