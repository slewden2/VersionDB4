

IF (NOT EXISTS (SELECT 1 FROM dbo.Project p WHERE p.ProjectName = 'Tests Analyzeur de script'))
BEGIN
  INSERT INTO dbo.Project (ProjectName) VALUES ('Tests Analyzeur de script')
  ;
END

SELECT * FROM dbo.Project WHERE ProjectName = 'Tests Analyzeur de script';


IF (NOT EXISTS (SELECT 1 FROM dbo.[Version] v 
                         INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
                         WHERE p.ProjectName = 'Tests Analyzeur de script'
                           AND v.VersionPrincipal = 1
                           AND v.VersionSecondary = 1))
BEGIN
  INSERT INTO dbo.[Version] (ProjectId, VersionPrincipal, VersionSecondary) 
  SELECT p.ProjectId, 1, 1
  FROM dbo.Project p
  WHERE p.ProjectName = 'Tests Analyzeur de script'
  ;
END

SELECT * FROM dbo.[Version] v INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
                         WHERE p.ProjectName = 'Tests Analyzeur de script'
                           AND v.VersionPrincipal = 1
                           AND v.VersionSecondary = 1;

---- on efface tous les scripts
DELETE r4 
FROM dbo.Resume4Client r4 
INNER JOIN dbo.[Resume] r ON r4.ResumeId = r.ResumeId
INNER JOIN dbo.script s   ON r.ScriptId = s.ScriptId
INNER JOIN  dbo.[Version] v ON s.VersionId = v.VersionId
INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
WHERE p.ProjectName = 'Tests Analyzeur de script'
  AND v.VersionPrincipal = 1
  AND v.VersionSecondary = 1
;

DELETE r 
FROM dbo.[Resume] r
INNER JOIN dbo.script s   ON r.ScriptId = s.ScriptId
INNER JOIN  dbo.[Version] v ON s.VersionId = v.VersionId
INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
WHERE p.ProjectName = 'Tests Analyzeur de script'
  AND v.VersionPrincipal = 1
  AND v.VersionSecondary = 1
;

DELETE d 
FROM dbo.DataBaseObject d
INNER JOIN dbo.script s   ON d.ScriptId = s.ScriptId
INNER JOIN  dbo.[Version] v ON s.VersionId = v.VersionId
INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
WHERE p.ProjectName = 'Tests Analyzeur de script'
  AND v.VersionPrincipal = 1
  AND v.VersionSecondary = 1
;
DELETE b 
FROM dbo.DataBaseObject b
INNER JOIN dbo.script s   ON b.ScriptId = s.ScriptId
INNER JOIN  dbo.[Version] v ON s.VersionId = v.VersionId
INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
WHERE p.ProjectName = 'Tests Analyzeur de script'
  AND v.VersionPrincipal = 1
  AND v.VersionSecondary = 1
;

DELETE s
FROM dbo.script s 
INNER JOIN  dbo.[Version] v ON s.VersionId = v.VersionId
INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
WHERE p.ProjectName = 'Tests Analyzeur de script'
  AND v.VersionPrincipal = 1
  AND v.VersionSecondary = 1
;

SELECT v.VersionId
FROM dbo.[Version] v 
INNER JOIN dbo.Project p ON p.ProjectId = v.ProjectId 
WHERE p.ProjectName = 'Tests Analyzeur de script'
  AND v.VersionPrincipal = 1
  AND v.VersionSecondary = 1
;


INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
--- DBComparer Création de la procédure base.dbo.Procedure2test
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
--- DBComparer la fonction base.dbo.Function2test
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
--- DBComparer la vue base.dbo.view
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
--- DBComparer le déclencheur base.dbo.Trigger2Test
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
--- DBComparer l''index index2Test
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
--- DBComparer le schéma schema2Test
');

INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
 RAISERROR(''Message'', 1, 42);
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
 RAISERROR(''Message'', 16, 42);
');

INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
 CREATE TABLE base.schema.table1 (col1 int)
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
 CREATE TABLE schema.table2 (col1 int)
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, 'CREATE     TABLE     table3 (col1 int)
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
SELECT * INTO database.schema.table4 
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
SELECT INTO schema.table5 
');
INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
SELECT INTO table6
');

INSERT INTO dbo.Script (VersionId, ScriptOrder, ScriptText) VALUES (30, 1, '
DROP TABLE database.schema.table4');



SELECt * FROM dbo.Script WHERe VersionId = 30;