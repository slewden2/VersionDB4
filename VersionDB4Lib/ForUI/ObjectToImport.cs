using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4Lib.ForUI
{
    public class ObjectToImport : Object
    {
        private EImportType importType = EImportType.Unkonw;

        public Object ReferencedObject { get; set; }

        public EImportType OriginalStatus { get; private set; } = EImportType.Unkonw;

        public EImportType Status
        {
            get
            {
                if (importType == EImportType.Unkonw)
                {
                    importType = ReferencedObject == null ? EImportType.New : IsMatch(ReferencedObject) ? ReferencedObject.ObjectSql == this.ObjectSql ? EImportType.Equal : EImportType.Different : EImportType.New;
                    OriginalStatus = importType;
                }

                return importType;
            }
            set => importType = value;
        }


        public bool IsMatch(Object obj)
           => obj != null && this.Identifier.Equals(obj.Identifier);

    }
}
