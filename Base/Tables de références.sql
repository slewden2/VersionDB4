
--- référentiel des types a gérer


INSERT INTO dbo.TypeObject (TypeObjectSqlServerCode, TypeObjectName, TypeObjectPlurial, TypeObjectPrestentOrder) VALUES 
  ('F', 'Référence', 'Les références', 8)
, ('FN', 'Fonction scalaire', 'Les fonctions scalaires', 4)
, ('IF', 'Fonction table en ligne', 'les fonctions tables (en ligne)', 5)
, ('P', 'Procédure stockée', 'Les procédures stockées', 3)
, ('U', 'Table', 'Les tables', 1)
, ('V', 'Vue', 'Les vues', 2)
, ('TF', 'fonction table', 'Les fonctions table (instructions multiples)', 6)
, ('TT', 'Type de table', 'Les types de données Table', 7)
, ('IDX', 'Index', 'Les index', 9)
, ('TR', 'Trigger', 'Les triggers', 10)
, ('SCH', 'Schema', 'Les schémas', 11)


---UPDATE dbo.TypeObject  SET TypeObjectSqlServerCode = 'TR' WHERE TypeObjectName = 'Trigger'

--INSERT INTO dbo.CodeClient (CodeClientName) VALUES ('[Interne]'), ('CNAF'), ('Pôle Emploie'), ('Bddf')
--SELECT * FROM dbo.CodeClient
--INSERT INTO dbo.project (projectName) VALUES ('interact')

SELECt * From dbo.Project

--INSERT INTO dbo.Version (projectId, VersionPrincipal, VersionSecondary) VALUES (1, 4, 1), (1, 4, 2), (1, 5, 0)



SELECT * FROM dbo.SqlWhat

--SET IDENTITY_INSERT dbo.SqlWhat ON;
--INSERT INTO dbo.SqlWhat(SqlWhatId, SqlWhatName, SqlWhatTitle) VALUES 
--    (1, 'Procedure', 'les procédures'), (2, 'Function', 'les fonctions') , (3, 'View', 'les vues'), (4, 'Trigger', 'les triggers')
--  , (5, 'Index', 'les index'), (6, 'Schema', 'les schémas'), (7, 'Table', 'les tables'), (8, 'Type', 'les types'), (9, 'Constraint', 'les contraintes');
--SET IDENTITY_INSERT dbo.SqlWhat OFF;

SET IDENTITY_INSERT dbo.SqlWhat ON;
INSERT INTO dbo.SqlWhat(SqlWhatId, SqlWhatName, SqlWhatTitle) VALUES (0, 'Aucun', '')
SET IDENTITY_INSERT dbo.SqlWhat OFF;


SELECt * FRom dbo.SqlAction

--SET IDENTITY_INSERT dbo.SqlAction ON;
--INSERT INTO dbo.SqlAction(SqlActionId, SqlActionName, SqlActionIsForColumn, SqlActionIsForTable, SqlActionIsForIndex, SqlActionTitle) 
--   VALUES (0, 'UnKnow', 0, 0, 0, 'Rien à faire'), (1, 'DBComparer', 0, 0, 0, 'Script créé par DBComparer'), (2, 'CodeClient', 0, 0, 0, 'Code spécifique à un client')
--        , (3, 'Comment', 0, 0, 0, 'Commentaire'), (4, 'Create', 0, 0, 1, 'Création de'), (5, 'Alter', 0, 0, 1, 'Modification de')
--        , (6, 'Drop', 0, 0, 1, 'Suppression de'), (7, 'Rename', 0, 0, 0, 'Changement de nom de'), (8, 'AddColumn', 1, 1, 0, 'Ajout de colonne à')
--        , (9, 'AlterColumn', 1, 1, 0, 'Modification d''une colonne de'), (10, 'DropColumn', 1, 1, 0, 'Suppression d''une colonne dans')
--        , (11, 'RenameColumn', 1, 1, 0, 'Changement de nom d''une colonne de'), (12, 'Update', 0, 1, 0, 'Mise à jour de données dans'), (13, 'Insert', 0, 1, 0, 'Insersion de données dans')
--        , (14, 'Delete', 0, 1, 0, 'Suppression de données dans'), (15, 'Execute', 0, 0, 0, 'Execution de')
--        , (16, 'AddColumNotNull', 1, 1, 0, 'Ajout de colonne non nulle à'), (17, 'RaiseError', 0, 0, 0, 'Erreur programmée'), (18, 'Print', 0, 0, 0, 'Affichage d''un message');
--SET IDENTITY_INSERT dbo.SqlAction OFF;
