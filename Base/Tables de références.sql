
--- r�f�rentiel des types a g�rer

--DELETE FROM dbo.Object
--DELETE FROM dbo.TypeObject

SET IDENTITY_INSERT dbo.TypeObject ON;
INSERT INTO dbo.TypeObject (TypeObjectId, TypeObjectSqlServerCode, TypeObjectName, TypeObjectPlurial, TypeObjectPrestentOrder, TypeObjectTilte, TypeObjectNeedColumnDefinition) VALUES 
  (0, '', 'Aucun', '', 255, '', 0) 
, (1, 'P', 'Proc�dure stock�e', 'Les proc�dures stock�es', 3, 'la proc�dure', 0)
, (2, 'FN', 'Fonction scalaire', 'Les fonctions scalaires', 4, 'la fonction scalaire', 0)
, (3, 'IF', 'Fonction table en ligne', 'Les fonctions tables (en ligne)', 5, 'la fonction table en ligne', 0)
, (4, 'TF', 'Fonction table', 'Les fonctions tables (instructions multiples)', 6, 'la fonction table � instructions multiples', 0)
, (5, 'V', 'Vue', 'Les vues', 2, 'la vue', 1)
, (6, 'TR', 'D�clencheur', 'Les d�clencheurs', 12, 'le d�clencheur', 0)
, (7, 'IDX', 'Index', 'Les index', 11, 'l''index', 1)
, (8, 'SCH', 'Schema', 'Les sch�mas', 13, 'le sch�ma', 0)
, (9, 'U', 'Table', 'Les tables', 1, 'la table', 1)
, (10, 'TT', 'Type de table', 'Les types de donn�es table', 7, 'le type de donn�es table', 1)
, (11, 'TD', 'Type de donn�es', 'Les types de donn�es', 8, 'le type de donn�es �l�mentaire', 0)
, (12, 'F', 'R�f�rence', 'Les r�f�rences', 9, 'la r�f�rence', 0)
, (13, 'C', 'Contrainte', 'Les contraintes', 10, 'la contrainte', 0)
;

SET IDENTITY_INSERT dbo.TypeObject OFF;

---UPDATE dbo.TypeObject SET TypeObjectPlurial = 'Les contraintes' WHERE TypeObjectId = 13
---UPDATE dbo.TypeObject SET TypeObjectSqlServerCode = 'C' WHERE TypeObjectId = 13
---UPDATE dbo.TypeObject SET TypeObjectName = 'D�clencheur', TypeObjectPlurial = 'Les d�clencheurs' WHERE TypeObjectId = 6

SELECT *  From TypeObject ORDER BY TypeObjectPrestentOrder

---UPDATE dbo.TypeObject  SET TypeObjectSqlServerCode = 'TR' WHERE TypeObjectName = 'Trigger'

--INSERT INTO dbo.CodeClient (CodeClientName) VALUES ('[Interne]'), ('CNAF'), ('P�le Emploie'), ('Bddf')
--SELECT * FROM dbo.CodeClient
--INSERT INTO dbo.project (projectName) VALUES ('interact')

SELECt * From dbo.Project

--INSERT INTO dbo.Version (projectId, VersionPrincipal, VersionSecondary) VALUES (1, 4, 1), (1, 4, 2), (1, 5, 0)





--SET IDENTITY_INSERT dbo.SqlWhat ON;
--INSERT INTO dbo.SqlWhat(SqlWhatId, SqlWhatName, SqlWhatTitle) VALUES 
--    (1, 'Procedure', 'les proc�dures'), (2, 'Function', 'les fonctions') , (3, 'View', 'les vues'), (4, 'Trigger', 'les triggers')
--  , (5, 'Index', 'les index'), (6, 'Schema', 'les sch�mas'), (7, 'Table', 'les tables'), (8, 'Type', 'les types'), (9, 'Constraint', 'les contraintes');
--SET IDENTITY_INSERT dbo.SqlWhat OFF;

SET IDENTITY_INSERT dbo.SqlWhat ON;
INSERT INTO dbo.SqlWhat(SqlWhatId, SqlWhatName, SqlWhatTitle) VALUES (0, 'Aucun', '')
SET IDENTITY_INSERT dbo.SqlWhat OFF;


SELECt * FRom dbo.SqlAction

--SET IDENTITY_INSERT dbo.SqlAction ON;
--INSERT INTO dbo.SqlAction(SqlActionId, SqlActionName, SqlActionIsForColumn, SqlActionIsForTable, SqlActionIsForIndex, SqlActionTitle) 
--   VALUES (0, 'UnKnow', 0, 0, 0, 'Rien � faire'), (1, 'DBComparer', 0, 0, 0, 'Script cr�� par DBComparer'), (2, 'CodeClient', 0, 0, 0, 'Code sp�cifique � un client')
--        , (3, 'Comment', 0, 0, 0, 'Commentaire'), (4, 'Create', 0, 0, 1, 'Cr�ation de'), (5, 'Alter', 0, 0, 1, 'Modification de')
--        , (6, 'Drop', 0, 0, 1, 'Suppression de'), (7, 'Rename', 0, 0, 0, 'Changement de nom de'), (8, 'AddColumn', 1, 1, 0, 'Ajout de colonne �')
--        , (9, 'AlterColumn', 1, 1, 0, 'Modification d''une colonne de'), (10, 'DropColumn', 1, 1, 0, 'Suppression d''une colonne dans')
--        , (11, 'RenameColumn', 1, 1, 0, 'Changement de nom d''une colonne de'), (12, 'Update', 0, 1, 0, 'Mise � jour de donn�es dans'), (13, 'Insert', 0, 1, 0, 'Insersion de donn�es dans')
--        , (14, 'Delete', 0, 1, 0, 'Suppression de donn�es dans'), (15, 'Execute', 0, 0, 0, 'Execution de')
--        , (16, 'AddColumNotNull', 1, 1, 0, 'Ajout de colonne non nulle �'), (17, 'RaiseError', 0, 0, 0, 'Erreur programm�e'), (18, 'Print', 0, 0, 0, 'Affichage d''un message');
--SET IDENTITY_INSERT dbo.SqlAction OFF;

