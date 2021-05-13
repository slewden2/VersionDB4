SELECT
SCHEMA_NAME(tt2.schema_id) AS [Schema], tt2.name AS [Name],
 tt2.system_type_id, tt2.user_type_id

, REPLACE('CREATE TYPE ' +  QUOTENAME(SCHEMA_NAME(tt2.schema_id)) + '.' + QUOTENAME(tt2.name) + ' AS TABLE(' + CHAR(13) + '  ' 
 + SUBSTRING((SELECT ', ' + QUOTENAME(clmns.name) + ' '
        + CASE WHEN clmns.is_computed = 1 THEN '  AS ' + ISNULL(cc.definition,N'') + CASE WHEN cc.is_persisted= 1 THEN ' PERSISTED' ELSE '' END
                ELSE QUOTENAME(COALESCE(baset.name, usrt.name)) 
                    + CASE WHEN UPPER(baset.name) IN ('CHAR',   'VARCHAR') AND clmns.max_length <> -1 THEN '(' + CONVERT(VARCHAR(20), clmns.max_length) + ')' 
                            WHEN UPPER(baset.name) IN ('NCHAR', 'NVARCHAR') AND clmns.max_length <> -1 THEN '(' + CONVERT(VARCHAR(20), clmns.max_length /2 ) +')' 
                            WHEN UPPER(baset.name) IN ('CHAR', 'VARCHAR', 'NCHAR', 'NVARCHAR') AND clmns.max_length = -1 THEN '(max)' 
                            ELSE '' END + CASE WHEN clmns.collation_name IS NOT NULL AND UPPER(baset.name) IN ('NCHAR', 'NVARCHAR') THEN ' COLLATE ' + clmns.collation_name ELSE '' END 
                    + CASE WHEN clmns.is_identity = 1 THEN ' IDENTITY(' + CONVERT(VARCHAR(20), ISNULL(ic.seed_value,0)) + ',' + CONVERT(VARCHAR(20), ISNULL(ic.increment_value,0)) + ')' ELSE '' END
                    + CASE WHEN clmns.is_rowguidcol = 1 THEN ' ROWGUIDCOL ' ELSE '' END
          END
        + CASE WHEN clmns.is_nullable = 1 THEN ' NULL' ELSE ' NOT NULL' END

        +  CASE WHEN ISNULL(clmns.is_computed, 0) = 0 AND dc.Name IS NOT NULL THEN ' DEFAULT ' + dc.definition ELSE '' END 
        AS [text()] 
    FROM       sys.table_types            AS tt
    INNER JOIN sys.all_columns            AS clmns ON clmns.object_id=tt.type_table_object_id
     LEFT JOIN sys.computed_columns       AS cc ON cc.object_id = clmns.object_id and cc.column_id = clmns.column_id
     LEFT JOIN sys.types                  AS usrt ON usrt.user_type_id = clmns.user_type_id
     LEFT JOIN sys.objects                AS d ON d.object_id = clmns.default_object_id
     LEFT JOIN sys.default_constraints    AS dc ON clmns.default_object_id = dc.object_id
     LEFT JOIN sys.identity_columns       AS ic ON ic.object_id = clmns.object_id and ic.column_id = clmns.column_id
     LEFT JOIN sys.types                  AS baset ON (baset.user_type_id = clmns.system_type_id and baset.user_type_id = baset.system_type_id) or ((baset.system_type_id = clmns.system_type_id) and (baset.user_type_id = clmns.user_type_id) and (baset.is_user_defined = 0) and (baset.is_assembly_type = 1)) 
    WHERE tt.system_type_id = tt2.system_type_id AND tt.user_type_id = tt2.user_type_id 
    ORDER BY clmns.column_id ASC
    FOR XML PATH('')), 3, 999999) 
    + CHAR(13) + ')'
    + CASE WHEN tt2.is_memory_optimized = 1 THEN CHAR(13) + 'WITH ( MEMORY_OPTIMIZED = ON)' ELSE '' END
    , ', ', ',' + CHAR(13) + '  ') AS [sql]

FROM
sys.table_types AS tt2
INNER JOIN sys.schemas AS stt2 ON stt2.schema_id = tt2.schema_id


/*

SELECT
i.name AS [Name],
CAST(ISNULL(si.bounding_box_xmax,0) AS float(53)) AS [BoundingBoxXMax],
CAST(ISNULL(si.bounding_box_xmin,0) AS float(53)) AS [BoundingBoxXMin],
CAST(ISNULL(si.bounding_box_ymax,0) AS float(53)) AS [BoundingBoxYMax],
CAST(ISNULL(si.bounding_box_ymin,0) AS float(53)) AS [BoundingBoxYMin],
CAST(case when (i.type=7) then hi.bucket_count else 0 end AS int) AS [BucketCount],
CAST(ISNULL(si.cells_per_object,0) AS int) AS [CellsPerObject],
CAST(i.compression_delay AS int) AS [CompressionDelay],
~i.allow_page_locks AS [DisallowPageLocks],
~i.allow_row_locks AS [DisallowRowLocks],

        CASE WHEN ((SELECT tbli.is_memory_optimized FROM sys.tables tbli WHERE tbli.object_id = i.object_id)=1 or
        (SELECT tti.is_memory_optimized FROM sys.table_types tti WHERE tti.type_table_object_id = i.object_id)=1)
        THEN ISNULL((SELECT ds.name FROM sys.data_spaces AS ds WHERE ds.type='FX'), N'')
        ELSE CASE WHEN 'FG'=dsi.type THEN dsi.name ELSE N'' END
        END
       AS [FileGroup],
CASE WHEN 'FD'=dstbl.type THEN dstbl.name ELSE N'' END AS [FileStreamFileGroup],
CASE WHEN 'PS'=dstbl.type THEN dstbl.name ELSE N'' END AS [FileStreamPartitionScheme],
i.fill_factor AS [FillFactor],
ISNULL(i.filter_definition, N'') AS [FilterDefinition],
i.ignore_dup_key AS [IgnoreDuplicateKeys],

        ISNULL(indexedpaths.name, N'')
       AS [IndexedXmlPathName],
i.is_primary_key + 2*i.is_unique_constraint AS [IndexKeyType],
CAST(
          CASE i.type WHEN 1 THEN 0 WHEN 4 THEN 4
                      WHEN 3 THEN CASE xi.xml_index_type WHEN 0 THEN 2 WHEN 1 THEN 3 WHEN 2 THEN 7 WHEN 3 THEN 8 END
                      WHEN 4 THEN 4 WHEN 6 THEN 5 WHEN 7 THEN 6 WHEN 5 THEN 9 ELSE 1 END
        AS tinyint) AS [IndexType],
CAST(CASE i.index_id WHEN 1 THEN 1 ELSE 0 END AS bit) AS [IsClustered],
i.is_disabled AS [IsDisabled],
CAST(CASE WHEN filetableobj.object_id IS NULL THEN 0 ELSE 1 END AS bit) AS [IsFileTableDefined],
CAST(ISNULL(k.is_system_named, 0) AS bit) AS [IsSystemNamed],
CAST(OBJECTPROPERTY(i.object_id,N'IsMSShipped') AS bit) AS [IsSystemObject],
i.is_unique AS [IsUnique],
CAST(ISNULL(si.level_1_grid,0) AS smallint) AS [Level1Grid],
CAST(ISNULL(si.level_2_grid,0) AS smallint) AS [Level2Grid],
CAST(ISNULL(si.level_3_grid,0) AS smallint) AS [Level3Grid],
CAST(ISNULL(si.level_4_grid,0) AS smallint) AS [Level4Grid],
ISNULL(s.no_recompute,0) AS [NoAutomaticRecomputation],
CAST(ISNULL(INDEXPROPERTY(i.object_id, i.name, N'IsPadIndex'), 0) AS bit) AS [PadIndex],
ISNULL(xi2.name, N'') AS [ParentXmlIndex],
CASE WHEN 'PS'=dsi.type THEN dsi.name ELSE N'' END AS [PartitionScheme],
case UPPER(ISNULL(xi.secondary_type,'')) when 'P' then 1 when 'V' then 2 when 'R' then 3 else 0 end AS [SecondaryXmlIndexType],
CAST(ISNULL(spi.spatial_index_type,0) AS tinyint) AS [SpatialIndexType],
CAST(ISNULL(INDEXPROPERTY(i.object_id, i.name, N'IsOptimizedForSequentialKey'), 0) AS bit) AS [IsOptimizedForSequentialKey]
FROM
sys.table_types AS tt
INNER JOIN sys.schemas AS stt ON stt.schema_id = tt.schema_id
INNER JOIN sys.indexes AS i ON (i.object_id=tt.type_table_object_id) -- (i.index_id > @_msparam_0 and i.is_hypothetical = @_msparam_1) AND 
LEFT OUTER JOIN sys.spatial_index_tessellations as si ON i.object_id = si.object_id and i.index_id = si.index_id
LEFT OUTER JOIN sys.hash_indexes AS hi ON i.object_id = hi.object_id AND i.index_id = hi.index_id
LEFT OUTER JOIN sys.data_spaces AS dsi ON dsi.data_space_id = i.data_space_id
LEFT OUTER JOIN sys.tables AS t ON t.object_id = i.object_id
LEFT OUTER JOIN sys.data_spaces AS dstbl ON dstbl.data_space_id = t.Filestream_data_space_id and (i.index_id < 2 or (i.type = 7 and i.index_id < 3))
LEFT OUTER JOIN sys.xml_indexes AS xi ON xi.object_id = i.object_id AND xi.index_id = i.index_id
LEFT OUTER JOIN sys.selective_xml_index_paths AS indexedpaths ON xi.object_id = indexedpaths.object_id AND xi.using_xml_index_id = indexedpaths.index_id AND xi.path_id = indexedpaths.path_id
LEFT OUTER JOIN sys.filetable_system_defined_objects AS filetableobj ON i.object_id = filetableobj.object_id
LEFT OUTER JOIN sys.key_constraints AS k ON k.parent_object_id = i.object_id AND k.unique_index_id = i.index_id
LEFT OUTER JOIN sys.stats AS s ON s.stats_id = i.index_id AND s.object_id = i.object_id
LEFT OUTER JOIN sys.xml_indexes AS xi2 ON xi2.object_id = xi.object_id AND xi2.index_id = xi.using_xml_index_id
LEFT OUTER JOIN sys.spatial_indexes AS spi ON i.object_id = spi.object_id and i.index_id = spi.index_id


*/