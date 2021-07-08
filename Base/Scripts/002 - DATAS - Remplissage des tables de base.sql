
-------------------------------------------------------------------------------
--- SLew 06/07/2021 19:53:23 : Cr�e Les donn�es de bases
-------------------------------------------------------------------------------

IF (NOT EXISTS (SELECT 1 FROM TypeObject WHERE TypeObjectId = 13))
BEGIN
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
END

IF (NOT EXISTS (SELECT 1 FROM dbo.sqlAction WHERE SqlActionId = 18))
BEGIN
  SET IDENTITY_INSERT dbo.SqlAction ON;
  INSERT INTO dbo.SqlAction(SqlActionId, SqlActionName, SqlActionIsForColumn, SqlActionIsForTable, SqlActionIsForIndex, SqlActionTitle) 
     VALUES (0, 'UnKnow', 0, 0, 0, 'Rien � faire'), (1, 'DBComparer', 0, 0, 0, 'Script cr�� par DBComparer'), (2, 'CodeClient', 0, 0, 0, 'Code sp�cifique � un client')
          , (3, 'Comment', 0, 0, 0, 'Commentaire'), (4, 'Create', 0, 0, 1, 'Cr�ation de'), (5, 'Alter', 0, 0, 1, 'Modification de')
          , (6, 'Drop', 0, 0, 1, 'Suppression de'), (7, 'Rename', 0, 0, 0, 'Changement de nom de'), (8, 'AddColumn', 1, 1, 0, 'Ajout de colonne �')
          , (9, 'AlterColumn', 1, 1, 0, 'Modification d''une colonne de'), (10, 'DropColumn', 1, 1, 0, 'Suppression d''une colonne dans')
          , (11, 'RenameColumn', 1, 1, 0, 'Changement de nom d''une colonne de'), (12, 'Update', 0, 1, 0, 'Mise � jour de donn�es dans'), (13, 'Insert', 0, 1, 0, 'Insersion de donn�es dans')
          , (14, 'Delete', 0, 1, 0, 'Suppression de donn�es dans'), (15, 'Execute', 0, 0, 0, 'Execution de')
          , (16, 'AddColumNotNull', 1, 1, 0, 'Ajout de colonne non nulle �'), (17, 'RaiseError', 0, 0, 0, 'Erreur programm�e'), (18, 'Print', 0, 0, 0, 'Affichage d''un message');
  SET IDENTITY_INSERT dbo.SqlAction OFF;
END

IF (NOT EXISTS (SELECT 1 FROM dbo.Project WHERE ProjectId = 1))
BEGIN
  SET IDENTITY_INSERT dbo.Project ON;
  INSERT INTO dbo.Project (ProjectId, ProjectName) VALUES (1, 'Database');
  SET IDENTITY_INSERT dbo.Project OFF;
END

IF (NOT EXISTS (SELECT 1 FROM dbo.Version))
BEGIN
  INSERT INTO dbo.[Version] (ProjectId, VersionPrincipal, VersionSecondary) VALUES (1, 1, 0);
END