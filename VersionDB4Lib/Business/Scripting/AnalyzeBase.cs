using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.Business.Scripting
{
    public class AnalyzeBase
    {
        public static string SQLImportObject(int typeObjectId)
            => typeObjectId switch
            {
                1 => @"   --- Procédures
SELECT @VersionId AS VersionId, 1 AS TypeObjectId, OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.[name] AS ObjectName, ISNULL(sm.[definition], ss.[definition]) AS ObjectSql
FROM sys.objects AS sp
 LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
 LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.[type] = 'p' 
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
  AND sm.execute_as_principal_id IS NULL
;
",
                2 => @"   --- Fonctions scalaire
SELECT @VersionId AS VersionId, 2 AS TypeObjectId, OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.[name] AS ObjectName, ISNULL(sm.[definition], ss.[definition]) AS ObjectSql
FROM sys.objects AS sp
 LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
 LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.[type] = 'FN' 
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
  AND sm.execute_as_principal_id IS NULL
;
",
                3 => @"   --- Fonctions table en ligne
SELECT @VersionId AS VersionId, 3 AS TypeObjectId, OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.[name] AS ObjectName, ISNULL(sm.[definition], ss.[definition]) AS ObjectSql
FROM sys.objects AS sp
 LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
 LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.[type] = 'IF' 
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
  AND sm.execute_as_principal_id IS NULL
;
",          
                4 => @"   --- Fonction Table
SELECT @VersionId AS VersionId, 4 AS TypeObjectId, OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.[name] AS ObjectName, ISNULL(sm.[definition], ss.[definition]) AS ObjectSql
FROM sys.objects AS sp
 LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
 LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.[type] = 'TF' 
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
  AND sm.execute_as_principal_id IS NULL
;
",
                5 => @"   --- Vue
SELECT @VersionId AS VersionId, 5 AS TypeObjectId, OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.[name] AS ObjectName, ISNULL(sm.[definition], ss.[definition]) AS ObjectSql
FROM sys.objects AS sp
 LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
 LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.[type] = 'V' 
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
  AND sm.execute_as_principal_id IS NULL
;
",
                6 => @"   --- Trigger
SELECT @VersionId AS VersionId, 6 AS TypeObjectId, OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.[name] AS ObjectName, ISNULL(sm.[definition], ss.[definition]) AS ObjectSql
FROM sys.objects AS sp
 LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
 LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.[type] = 'TR' 
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
  AND sm.execute_as_principal_id IS NULL
;",
                7 => @"   --- Index (TODO A compléter)
SELECT @VersionId AS VersionId, 7 AS TypeObjectId, i.[name] AS ObjectColumn, t.[name] AS ObjectName, OBJECT_SCHEMA_NAME(i.object_id) AS ObjectSchema
 ,  CASE WHEN i.is_unique = 1 THEN 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(i.object_id)) +'.' + QUOTENAME(t.[name]) + ' ADD ' 
                                    + CASE WHEN i.is_primary_key = 1 THEN ' PRIMARY KEY '
                                           ELSE ' CONSTRAINT ' + QUOTENAME(i.[name]) 
                                      END
                                    + CASE i.[type] WHEN 1 THEN ' CLUSTERED ' 
                                                    WHEN 2 THEN ' NONCLUSTERED ' 
                                      END 
        ELSE 'CREATE INDEX ' + QUOTENAME(i.[name]) + ' ON ' + QUOTENAME(OBJECT_SCHEMA_NAME(i.object_id)) +'.' + QUOTENAME(t.[name])
    END + '(' +  
    SUBSTRING( (SELECT ', ' + co.[name] +  CASE WHEN cl.is_descending_key = 1 THEN ' DESC ' ELSE ' ASC ' END AS [text()]
      FROM sys.index_columns cl
      INNER JOIN sys.columns co ON cl.object_id = co.object_id AND cl.column_id = co.column_id
      WHERE cl.is_included_column = 0
        AND cl.object_id = i.object_id
        AND cl.column_id > 0
      ORDER BY cl.index_column_id
      FOR XML PATH('')
    ), 3, 99999)
    + ')' 
    + CASE WHEN (SELECT COUNT(*) FROM sys.index_columns cl2 WHERE cl2.is_included_column = 1 AND cl2.object_id = i.object_id ) > 0 THEN 
          ' INCLUDE ('
    + SUBSTRING( (SELECT ', ' + co.[name] AS [text()]
            FROM sys.index_columns cl
            INNER JOIN sys.columns co ON cl.object_id = co.object_id AND cl.column_id = co.column_id
            WHERE cl.is_included_column = 1
              AND cl.object_id = i.object_id
            ORDER BY cl.object_id, cl.index_column_id
            FOR XML PATH('') ), 3, 9999)  + ')' 
            ELSE '' END
      + ';' AS ObjectSql
FROM sys.indexes i
INNER JOIN sys.tables t ON i.object_id = t.object_id
WHERE i.is_hypothetical = 0 AND i.index_id > 0 
;",
                8 => @"   --- Schema
SELECT @VersionId AS VersionId,  8 AS TypeObjectId, '' AS ObjectSchema, s.[name] AS ObjectName, 'CREATE SCHEMA ' + QUOTENAME(s.[name]) + ';' AS ObjectSql
FROM sys.schemas s
WHERE s.principal_id = 1
;
",
                9 => @"   --- Table (TODO A compléter avec les index, constrainte, colonne calculée, ...)
SELECT @VersionId AS VersionId, 9 AS TypeObjectId,  OBJECT_SCHEMA_NAME(t.object_id) AS ObjectSchema, t.[name] AS ObjectName
 , 'CREATE TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(t.object_id)) + '.' + QUOTENAME(t.[name]) 
   + '(' + SUBSTRING ( ( SELECT ', ' + cl.[name] + ' ' + ty.[name] 
                              + CASE WHEN ty.[name] IN ('varchar', 'char')   THEN '(' + CASE WHEN cl.max_length > 0 THEN FORMAT(cl.max_length, '')     ELSE 'MAX' END + ')'
                                     WHEN ty.[name] IN ('nvarchar', 'nchar') THEN '(' + CASE WHEN cl.max_length > 0 THEN FORMAT(cl.max_length / 2, '') ELSE 'MAX' END + ')'
                                     WHEN ty.[name] = 'numeric' THEN '(' + FORMAT(cl.[precision], '') + ', ' + FORMAT(cl.scale, '') + ')'
                                     ELSE '' 
                                  END
                              + CASE WHEN cl.is_identity = 1 THEN ' IDENTITY ' 
                                     WHEN cl.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' 
                                  END As [text()]
                          FROM sys.columns cl
                          INNER JOIN sys.types ty ON cl.system_type_id = ty.system_type_id AND cl.user_type_id = ty.user_type_id
                          WHERE cl.object_id = t.object_id
                          ORDER BY cl.object_id, cl.column_id
                          FOR XML PATH('')) , 3, 999999)
    + ')' AS ObjectSql      --- TODO SQL d'une table !
FROM sys.tables t
;
",              
                10 => @"   --- type table
SELECt @versionId AS VersionId , 10 as TypeObjectId, s.[name] AS ObjectSchema, tt.[name] AS ObjectName
 , 'CREATE TYPE ' + QUOTENAME(s.[name]) + '.' + QUOTENAME(tt.[name]) + ' AS TABLE '
   + '(' + SUBSTRING ( ( SELECT ', ' + cl.[name] + ' ' + ty.[name] 
                              + CASE WHEN ty.[name] IN ('varchar', 'char')   THEN '(' + CASE WHEN cl.max_length > 0 THEN FORMAT(cl.max_length, '')     ELSE 'MAX' END + ')'
                                     WHEN ty.[name] IN ('nvarchar', 'nchar') THEN '(' + CASE WHEN cl.max_length > 0 THEN FORMAT(cl.max_length / 2, '') ELSE 'MAX' END + ')'
                                     WHEN ty.[name] = 'numeric' THEN '(' + FORMAT(cl.[precision], '') + ', ' + FORMAT(cl.scale, '') + ')'
                                     ELSE '' 
                                  END
                              + CASE WHEN cl.is_identity = 1 THEN ' IDENTITY ' 
                                     WHEN cl.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' 
                                  END As [text()]
                          FROM sys.columns cl
                          INNER JOIN sys.types ty ON cl.system_type_id = ty.system_type_id AND cl.user_type_id = ty.user_type_id
                          WHERE cl.object_id = tt.type_table_object_id
                          ORDER BY cl.object_id, cl.column_id
                          FOR XML PATH('')) , 3, 999999)
    + ')' AS ObjectSql
FROM sys.table_types tt
INNER JOIN sys.schemas s on tt.schema_id = s.schema_id
;
",
                11 => @"   --- Type élementaire
SELECt @versionId AS VersionId , 11 as TypeObjectId, s.[name] AS ObjectSchema, tt.[name] AS ObjectName
  , 'CREATE TYPE ' + QUOTENAME(s.[name]) + '.' + QUOTENAME(tt.[name]) + ' FROM ' + QUOTENAME(ty.[name])
    + CASE WHEN ty.[name] IN ('varchar', 'char')   THEN '(' + CASE WHEN tt.max_length > 0 THEN FORMAT(tt.max_length, '')     ELSE 'MAX' END + ')'
            WHEN ty.[name] IN ('nvarchar', 'nchar') THEN '(' + CASE WHEN tt.max_length > 0 THEN FORMAT(tt.max_length / 2, '') ELSE 'MAX' END + ')'
            WHEN ty.[name] = 'numeric' THEN '(' + FORMAT(tt.[precision], '') + ', ' + FORMAT(tt.scale, '') + ')'
            ELSE '' 
        END
    + CASE WHEN tt.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' END
    + ';' AS ObjectSql
FROM sys.types tt
INNER JOIN sys.schemas s on tt.schema_id = s.schema_id
INNER JOIN sys.types ty ON tt.system_type_id = ty.system_type_id AND ty.is_user_defined = 0
WHERE tt.is_user_defined = 1 
  AND tt.is_table_type = 0
;
",
                12 => @"   --- Foreign key
SELECT @VersionId AS VersionId, 12 AS TypeObjectId, OBJECT_SCHEMA_NAME(cstr.object_id) AS ObjectSchema, tbl1.[name] AS ObjectName, cstr.[name] AS ObjectColumn
,  'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(tbl1.schema_id)) + '.' + QUOTENAME(tbl1.[name]) + 
  CASE WHEN cstr.is_not_trusted = 0 THEN '  WITH CHECK ' ELSE '  WITH NOCHECK ' END + ' ADD  CONSTRAINT ' + QUOTENAME(cstr.[name]) + ' FOREIGN KEY(' +
  SUBSTRING((SELECT ', ' + QUOTENAME(cfk.[name])  as [text()] 
             FROM sys.foreign_key_columns AS fk
             INNER JOIN sys.columns       AS cfk ON fk.parent_column_id = cfk.column_id AND fk.parent_object_id = cfk.object_id
             WHERE fk.constraint_object_id = cstr.object_id
             ORDER BY cfk.column_id 
             FOR XML PATH('')), 3, 999999) +
  ')'+ CHAR(13) + 'REFERENCES ' + QUOTENAME(SCHEMA_NAME(tbl2.schema_id)) + '.' + QUOTENAME(tbl2.[name]) + ' (' +
  SUBSTRING((SELECT ', ' + QUOTENAME(cfk2.[name])  as [text()] 
             FROM sys.foreign_key_columns AS fk2 
             INNER JOIN sys.columns       AS cfk2 ON fk2.referenced_column_id = cfk2.column_id AND fk2.referenced_object_id = cfk2.object_id
             WHERE fk2.constraint_object_id = cstr.object_id
             ORDER BY cfk2.column_id 
             FOR XML PATH('')), 3, 999999) +
  ')' + CASE cstr.update_referential_action WHEN 1 THEN CHAR(13) + 'ON UPDATE CASCADE' WHEN 2 THEN CHAR(13) + 'ON UPDATE SET NULL' WHEN 3 THEN CHAR(13) + 'ON UPDATE SET DEFAULT' ELSE '' END
      + CASE cstr.delete_referential_action WHEN 1 THEN CHAR(13) + 'ON DELETE CASCADE' WHEN 2 THEN CHAR(13) + 'ON DELETE SET NULL' WHEN 3 THEN CHAR(13) + 'ON DELETE SET DEFAULT' ELSE '' END
      + CASE WHEN cstr.is_not_for_replication = 1  THEN CHAR(13) + 'NOT FOR REPLICATION' ELSE '' END
      + CHAR(13) + 'GO' + CHAR(10) + CHAR(13) + 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(tbl1.schema_id)) + '.' + QUOTENAME(tbl1.[name]) 
      + CASE WHEN cstr.is_disabled = 1 THEN ' NOCHECK' ELSE ' CHECK' END + ' CONSTRAINT ' + QUOTENAME(cstr.name)  + CHAR(13) + CHAR(10) 
      + ';'  AS ObjectSql
FROM sys.foreign_keys AS cstr
INNER JOIN sys.tables AS tbl1 ON cstr.parent_object_id     = tbl1.object_id
INNER JOIN sys.tables AS tbl2 ON cstr.referenced_object_id = tbl2.object_id
;
",
                13 => @"   --- TODO contraite
SELECT @VersionId AS VersionId, 13 AS TypeObjectId, OBJECT_SCHEMA_NAME(t.object_id) AS ObjectSchema, t.[name] AS ObjectName, cstr.[name] AS ObjectColumn
,  'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(t.schema_id)) + '.' + QUOTENAME(t.[name]) 
     + CASE WHEN cstr.is_not_trusted = 0 THEN '  WITH CHECK ' ELSE '  WITH NOCHECK ' END + ' ADD  CONSTRAINT ' + QUOTENAME(cstr.[name]) + ' CHECK (' +
    cstr.[definition] + '; ' + CHAR(13) + CHAR(10)
     + 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(t.schema_id)) + '.' + QUOTENAME(t.[name]) 
     + CASE WHEN cstr.is_disabled = 1 THEN ' NOCHECK' ELSE 'CHECK' END + ' CONSTRAINT ' + QUOTENAME(cstr.[name])  + ';' AS ObjectSql
FROM sys.check_constraints cstr
INNER JOIN sys.tables t on cstr.parent_object_id = t.object_id
;
",
                _ => string.Empty
            };

        /*
            
            
            
            @"
--- tables
SELECT t.object_id AS [ObjectId], @VersionId AS VersionId
 , 5 AS TypeObjectId   --- Table
 , OBJECT_SCHEMA_NAME(t.object_id) AS ObjectSchema, t.name AS ObjectName
 , 0 AS ObjectDeleted, 0 AS ObjectEmpty
 , 'CREATE TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(t.object_id)) + '.' + QUOTENAME(t.name) AS ObjectSql      --- TODO SQL d'une table !
FROM sys.tables t

UNION 
--- Les foreign keys
SELECT cstr.object_id AS [ObjectId], @VersionId AS VersionId
 , 1 AS TypeObjectId  --- FK
  , OBJECT_SCHEMA_NAME(cstr.object_id) AS ObjectSchema, cstr.name AS ObjectName
  , 0 AS ObjectDeleted, 0 AS ObjectEmpty
,  'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(tbl1.schema_id)) + '.' + QUOTENAME(tbl1.name) + 
  CASE WHEN cstr.is_not_trusted = 0 THEN '  WITH CHECK ' ELSE '  WITH NOCHECK ' END + 'ADD  CONSTRAINT ' + QUOTENAME(cstr.name) + ' FOREIGN KEY(' +
  SUBSTRING((SELECT ', ' + QUOTENAME(cfk.name)  as [text()] 
             FROM sys.foreign_key_columns AS fk
             INNER JOIN sys.columns       AS cfk ON fk.parent_column_id = cfk.column_id and fk.parent_object_id = cfk.object_id
             WHERE fk.constraint_object_id = cstr.object_id
             ORDER BY cfk.column_id 
             FOR XML PATH('')), 3, 999999) +
  ')'+ CHAR(13) + 'REFERENCES ' + QUOTENAME(SCHEMA_NAME(tbl2.schema_id)) + '.' + QUOTENAME(tbl2.name) + ' (' +
  SUBSTRING((SELECT ', ' + QUOTENAME(cfk2.name)  as [text()] 
             FROM sys.foreign_key_columns AS fk2 
             INNER JOIN sys.columns       AS cfk2 ON fk2.referenced_column_id = cfk2.column_id and fk2.referenced_object_id = cfk2.object_id
             WHERE fk2.constraint_object_id = cstr.object_id
             ORDER BY cfk2.column_id 
             FOR XML PATH('')), 3, 999999) +
  ')' + CASE cstr.update_referential_action WHEN 1 THEN CHAR(13) + 'ON UPDATE CASCADE' WHEN 2 THEN CHAR(13) + 'ON UPDATE SET NULL' WHEN 3 THEN CHAR(13) + 'ON UPDATE SET DEFAULT' ELSE '' END
      + CASE cstr.delete_referential_action WHEN 1 THEN CHAR(13) + 'ON DELETE CASCADE' WHEN 2 THEN CHAR(13) + 'ON DELETE SET NULL' WHEN 3 THEN CHAR(13) + 'ON DELETE SET DEFAULT' ELSE '' END
      + CASE WHEN cstr.is_not_for_replication = 1  THEN CHAR(13) + 'NOT FOR REPLICATION' ELSE '' END
      + CHAR(13) + 'GO' + CHAR(10) + CHAR(13) + 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(tbl1.schema_id)) + '.' + QUOTENAME(tbl1.name) 
      + CASE WHEN cstr.is_disabled = 1 THEN ' NOCHECK' ELSE ' CHECK' END + ' CONSTRAINT ' + QUOTENAME(cstr.name)  + CHAR(13) + 'GO'
  AS ObjectSql
FROM sys.foreign_keys AS cstr
INNER JOIN sys.tables AS tbl1 ON cstr.parent_object_id     = tbl1.object_id
INNER JOIN sys.tables AS tbl2 ON cstr.referenced_object_id = tbl2.object_id

UNION 
-- les FUNC & PROC & VIEW & TRIGGER de la base client
SELECT sp.object_id AS [ObjectId], @VersionId AS VersionId
 , CASE sp.[type] WHEN 'FN' THEN 2 WHEN 'TF' THEN 7 WHEN 'IF' THEN 3 WHEN 'P' THEN 4 WHEN 'SP' THEN 4 WHEN 'V' THEN 6 WHEN 'TR' THEN 10 WHEN 'TA' THEN 10 END AS TypeObjectId 
 , OBJECT_SCHEMA_NAME(sp.object_id) AS ObjectSchema, sp.name AS ObjectName
 , 0 AS ObjectDeleted, 0 AS ObjectEmpty
 , ISNULL(sm.definition, ss.definition) AS ObjectSql
FROM sys.objects AS sp
LEFT JOIN sys.sql_modules        AS sm ON sm.object_id = sp.object_id
LEFT JOIN sys.system_sql_modules AS ss ON ss.object_id = sp.object_id
WHERE sp.type IN (N'FN', N'TF', N'IF', N'P', N'PC', N'V', N'TR', N'TA') -- FUNC & PROC & VIEW & TRIGGER
  AND CONVERT(BIT, CASE WHEN sp.is_ms_shipped = 1 THEN 1
                        WHEN (SELECT major_id FROM sys.extended_properties WHERE major_id = sp.object_id AND minor_id = 0 AND class = 1 AND name = N'microsoft_database_tools_support') IS NOT NULL THEN 1
						            ELSE 0 END) = 0
 AND sm.execute_as_principal_id IS NULL

 UNION 
-- les schemas de la base client
SELECT s.schema_id AS [ObjectId], @VersionId AS VersionId,  11 AS TypeObjectId 
 , '' AS ObjectSchema, s.name AS ObjectName
 , 0 AS ObjectDeleted, 0 AS ObjectEmpty
 , 'CREATE SCHEMA ' + QUOTENAME(s.name)  + CHAR(13) + 'GO' AS ObjectSql
FROM sys.schemas s
WHERE s.principal_id = 1

;
";
        */
    }
}
