--SELECT * FRom dbo.CodeClient
--SELECT * FRom dbo.Version
--SELECt * From Base
--SELECt * From TypeObject

CREATE PROCEDURE dbo.Analyse

DECLARE @versionId INT = 5

--- tables
SELECT t.object_id AS [ObjectId], @versionId AS VersionId
 , 5 AS TypeObjectId   --- Table
 , OBJECT_SCHEMA_NAME(t.object_id) AS ObjectSchema, t.name AS ObjectName
 , 0 AS ObjectDeleted, 0 AS ObjectEmpty
 , NULL AS ObjectSql      --- TODO SQL d'une table !
FROM sys.tables t

UNION 
--- Les foreign keys
SELECT cstr.object_id AS [ObjectId], @versionId AS VersionId
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
-- les functions de la base client
SELECT sp.object_id AS [ObjectId], @versionId AS VersionId
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
SELECT s.schema_id AS [ObjectId], @versionId AS VersionId,  11 AS TypeObjectId 
 , '' AS ObjectSchema, s.name AS ObjectName
 , 0 AS ObjectDeleted, 0 AS ObjectEmpty
 , 'CREATE SCHEMA ' + QUOTENAME(s.name)  + CHAR(13) + 'GO' AS ObjectSql
FROM sys.schemas s
WHERE s.principal_id = 1
