
SELECT * FROM dbo.TypeObject

SELECT * From Sys.systypes WHERE name = 'toto'


SELECT
st.name AS [Name],
sst.name AS [Schema],
'Server[@Name=' + quotename(CAST(
        serverproperty(N'Servername')
       AS sysname),'''') + ']' + '/Database[@Name=' + quotename(db_name(),'''') + ']' + '/UserDefinedDataType[@Name=' + quotename(st.name,'''') + ' and @Schema=' + quotename(sst.name,'''') + ']' AS [Urn],
baset.name AS [SystemType],
CAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND st.max_length <> -1 THEN st.max_length/2 ELSE st.max_length END AS int) AS [Length],
CAST(st.precision AS int) AS [NumericPrecision],
CAST(st.scale AS int) AS [NumericScale],
st.is_nullable AS [Nullable]
FROM
sys.types AS st
INNER JOIN sys.schemas AS sst ON sst.schema_id = st.schema_id
LEFT OUTER JOIN sys.types AS baset ON (baset.user_type_id = st.system_type_id and baset.user_type_id = baset.system_type_id) or ((baset.system_type_id = st.system_type_id) and (baset.user_type_id = st.user_type_id) and (baset.is_user_defined = 0) and (baset.is_assembly_type = 1)) 
WHERE
(st.schema_id!=4 and st.system_type_id!=240 and st.user_type_id != st.system_type_id and st.is_table_type != 1)
ORDER BY
[Schema] ASC,[Name] ASC
