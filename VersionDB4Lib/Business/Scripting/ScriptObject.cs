using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4Lib.Business.Scripting
{
    /// <summary>
    /// Fait un script SQL de mise à jour d'un objet du référentiel
    /// </summary>
    public class ScriptObject
    {
        private readonly Object myobject;
        private readonly SqlAction myAction;
        public ScriptObject(Object obj, SqlAction act)
        {
            myobject = obj;
            myAction = act;

            if (myAction.SqlActionId != SqlAction.Delete && myAction.SqlActionId != SqlAction.Alter && myAction.SqlActionId != SqlAction.Create)
            {
                throw new ArgumentException("Action is only in CUD values");
            }
        }


        public override string ToString()
        {
            if (myobject.TypeObjectId == TypeObject.None)
            { // pas de bras pas de chocolat
                return string.Empty;
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine();
            sql.AppendLine("-------------------------------------------------------------------------------");
            sql.AppendLine($"--- DBComparer {Environment.UserName} le {DateTime.Now:dd/MM/yyyy} à {DateTime.Now:HH:mm} {TypeObject.FileEntete(myobject.TypeObjectId)} {myobject}");
            sql.AppendLine("-------------------------------------------------------------------------------");

            string sepi = string.Empty;
            if (myobject.ClientCodeId > 0)
            {
                sql.AppendLine($"IF (EXISTS (SELECT 1 FROM dbo.CodeClientId({myobject.ClientCodeId})))");
                sql.AppendLine("BEGIN");
                sepi = "    ";
            }

            if (myobject.ObjectDeleted || myAction.SqlActionId == SqlAction.Delete || myAction.SqlActionId == SqlAction.Alter)
            {
                sql.AppendLine(sepi + SQLIfExists());
                sql.AppendLine(sepi + "BEGIN");
                sql.AppendLine(sepi + "    " + SQLDrop());
                sql.AppendLine(sepi + "END");
            }
            
            if (myAction.SqlActionId != SqlAction.Delete)
            {
                sql.AppendLine();
                sql.AppendLine(sepi + "DECLARE @sql VARCHAR(MAX);");
                sql.AppendLine(sepi + $"SELECT @sql = {SqlFormat.String(myobject.ObjectSql)}");
                sql.AppendLine(sepi + "IF (ISNULL(@sql, '') <> '')");
                sql.AppendLine(sepi + "BEGIN");
                sql.AppendLine(sepi + "    EXEC(@sql);");
                sql.AppendLine(sepi + "END");
            }

            if (myobject.ClientCodeId > 0)
            {
                sql.AppendLine("END");
            }

            sql.AppendLine("-------------------------------------------------------------------------------");
            return sql.ToString();
        }


        private string SQLIfExists()
            => myobject.TypeObjectId switch
            {
                1 => $"IF (EXISTS (SELECT 1 FROM sys.procedures p WHERE p.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(p.schema_id) = '{myobject.ObjectSchema}'))",
                2 => $"IF (EXISTS (SELECT 1 FROM sys.objects f WHERE f.[type] = 'FN' AND f.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(f.schema_id) = '{myobject.ObjectSchema}'))",
                3 => $"IF (EXISTS (SELECT 1 FROM sys.objects f WHERE f.[type] = 'IF' AND f.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(f.schema_id) = '{myobject.ObjectSchema}'))",
                4 => $"IF (EXISTS (SELECT 1 FROM sys.objects f WHERE f.[type] = 'TF' AND f.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(f.schema_id) = '{myobject.ObjectSchema}'))",
                5 => $"IF (EXISTS (SELECT 1 FROM sys.views v WHERE v.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(v.schema_id) = '{myobject.ObjectSchema}'))",
                6 => $"IF (EXISTS (SELECT 1 FROM sys.triggers t WHERE t.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(t.schema_id) = '{myobject.ObjectSchema}'))",
                7 => $"IF (EXISTS (SELECT 1 FROM sys.indexes i WHERE i.[name] = '{myobject.ObjectColumn}' AND OBJECT_NAME(i.object_id) = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(i.schema_id) = '{myobject.ObjectSchema}'))",
                8 => $"IF (EXISTS (SELECT 1 FROM sys.schemas s WHERE s.[name] = '{myobject.ObjectName}'))",
                9 => $"IF (EXISTS (SELECT 1 FROM sys.tables t WHERE t.[name] = '{myobject.ObjectName}'  AND OBJECT_SCHEMA_NAME(t.schema_id) = '{myobject.ObjectSchema}'))",
                10 => $"IF (EXISTS (SELECT 1 FROM sys.table_types tt INNER JOIN sys.schemas AS s ON tt.schema_id = s.schema_id WHERE tt.[name] = '{myobject.ObjectName}' AND s.[name] = '{myobject.ObjectSchema}'))",
                11 => $"IF (EXISTS (SELECT 1 FROM sys.types t INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id WHERE t.[name] = '{myobject.ObjectName}' AND s.[name] = '{myobject.ObjectSchema}' AND t.is_user_defined = 1 AND t.is_table_type = 0))",
                12 => $"IF (EXISTS (SELECT 1 FROM sys.foreign_keys f WHERE f.[name] = '{myobject.ObjectName}'  AND OBJECT_SCHEMA_NAME(f.schema_id) = '{myobject.ObjectSchema}'))",
                13 => $"IF (EXISTS (SELECT 1 FROM sys.objects c WHERE c.[type] = 'C' AND c.[name] = '{myobject.ObjectName}' AND OBJECT_SCHEMA_NAME(f.schema_id) = '{myobject.ObjectSchema}'))",
                _ => string.Empty
            };
        private string SQLDrop()
            => myobject.TypeObjectId switch
            {
                1 => $"DROP PROCEDURE [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                2 => $"DROP FUNCTION [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                3 => $"DROP FUNCTION [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                4 => $"DROP FUNCTION [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                5 => $"DROP VIEW [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                6 => $"DROP TRIGGER [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                7 => $"DROP INDEX [{myobject.ObjectColumn}] ON  [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                8 => $"DROP SCHEMA [{myobject.ObjectName}];",
                9 => $"DROP TABLE [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                10 => $"DROP TYPE [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                11 => $"DROP TYPE [{myobject.ObjectSchema}].[{myobject.ObjectName}];",
                12 => $"ALTER TABLE [{myobject.ObjectSchema}].[{myobject.ObjectName}] DROP CONSTRAINT [{myobject.ObjectColumn}];",
                13 => $"ALTER TABLE [{myobject.ObjectSchema}].[{myobject.ObjectName}] DROP CONSTRAINT [{myobject.ObjectColumn}];",
                _ => string.Empty
            };
    }
}
