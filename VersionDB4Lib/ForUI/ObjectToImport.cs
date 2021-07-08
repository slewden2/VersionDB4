using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;
using VersionDB4Lib.CRUD;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4Lib.ForUI
{
    public class ObjectToImport : Object
    {
        private EImportType importType = EImportType.Unkonw;
        private string internalObjectSql;

        public ObjectToImport() 
            => Columns = new List<ColumnDefinition>();

        public ObjectToImport(Object obj)
        {
            this.ObjectId = obj.ObjectId;
            this.VersionId = obj.VersionId;
            this.TypeObjectId = obj.TypeObjectId;
            this.ObjectSchema = obj.ObjectSchema;
            this.ObjectName = obj.ObjectName;
            this.ObjectColumn = obj.ObjectColumn;
            this.ObjectDeleted = obj.ObjectDeleted;
            this.ObjectEmpty = obj.ObjectEmpty;
            this.internalObjectSql = obj.ObjectSql;
            this.ObjectLockedBy = obj.ObjectLockedBy;
            this.ClientCodeId = obj.ClientCodeId;
        }

        public new string ObjectSql
        {
            get => internalObjectSql;
            set
            {
                internalObjectSql = value;
                if (this.GetTypeObject().TypeObjectNeedColumnDefinition)
                {
                    // TODO : analyse le texte pour en déduire les colonne leur type et l'info mandatory
                }
            }
        }

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


        public List<ColumnDefinition> Columns { get; set; }

    }
}
