
-------------------------------------------------------------------------------
--- SLew 06/07/2021 20:04:57 : Crée tout le schéma
-------------------------------------------------------------------------------

/*==============================================================*/
/* Table: Base                                                  */
/*==============================================================*/
create table dbo.Base (
   BaseId               int                  identity,
   ClientCodeId         int                  not null,
   BaseName             varchar(50)          not null,
   BaseConnectionString varchar(100)         not null,
   constraint PK_BASE primary key (BaseId)
)
;

/*==============================================================*/
/* Table: Bloc                                                  */
/*==============================================================*/
create table dbo.Bloc (
   BlocId               int                  identity,
   ScriptId             int                  not null,
   SqlActionId          int                  not null,
   TypeObjectId         int                  not null,
   ClientCodeId         int                  null,
   BlocIndex            int                  not null,
   BlocLength           int                  not null,
   BlocDataBase         varchar(100)         not null,
   BlocSchema           varchar(20)          not null,
   BlocName             varchar(100)         not null,
   BlocExcludeFromResume bit                  not null,
   BlocColumn           varchar(100)         null,
   constraint PK_BLOC primary key (BlocId)
)
;

/*==============================================================*/
/* Table: ClientCode                                            */
/*==============================================================*/
create table dbo.ClientCode (
   ClientCodeId         int                  identity,
   ClientCodeName       varchar(100)         not null,
   constraint PK_CLIENTCODE primary key (ClientCodeId)
)
;

/*==============================================================*/
/* Table: "Column"                                              */
/*==============================================================*/
create table dbo."Column" (
   ColumnId             int                  identity,
   ObjectId             int                  not null,
   ColumnName           Varchar(100)         not null,
   ColumnType           Varchar(50)          not null,
   ColumnMandatory      bit                  not null,
   constraint PK_COLUMN primary key (ColumnId)
)
;

/*==============================================================*/
/* Table: DataBaseObject                                        */
/*==============================================================*/
create table dbo.DataBaseObject (
   DatabaseObjectId     int                  identity,
   ScriptId             int                  not null,
   TypeObjectId         int                  not null,
   DatabaseObjectDataBase varchar(100)         not null,
   DatabaseObjectSchema varchar(20)          not null,
   DatabaseObjectName   varchar(100)         not null,
   DatabaseObjectColumn varchar(100)         null,
   constraint PK_DATABASEOBJECT primary key (DatabaseObjectId)
)
;

/*==============================================================*/
/* Table: Object                                                */
/*==============================================================*/
create table dbo.Object (
   ObjectId             int                  identity,
   VersionId            int                  not null,
   TypeObjectId         int                  not null,
   ObjectSchema         varchar(20)          not null,
   ObjectName           varchar(100)         not null,
   ObjectDeleted        bit                  not null,
   ObjectEmpty          bit                  not null,
   ObjectSql            varchar(MAX)         not null,
   ObjectLockedBy       varchar(100)         null,
   ClientCodeId         int                  null,
   ObjectColumn         varchar(100)         null,
   constraint PK_OBJECT primary key (ObjectId)
)
;

/*==============================================================*/
/* Table: Project                                               */
/*==============================================================*/
create table dbo.Project (
   ProjectId            int                  identity,
   ProjectName          varchar(100)         not null,
   constraint PK_PROJECT primary key (ProjectId)
)
;

/*==============================================================*/
/* Table: Resume                                                */
/*==============================================================*/
create table dbo.Resume (
   ResumeId             int                  identity,
   ScriptId             int                  not null,
   SqlActionId          int                  not null,
   TypeObjectId         int                  not null,
   ResumeDatabase       varchar(100)         not null,
   ResumeSchema         varchar(20)          not null,
   ResumeName           varchar(100)         not null,
   ResumeColumn         varchar(100)         null,
   ResumeForOtherClients bit                  not null,
   ResumeManualValidationCode tinyInt              not null,
   constraint PK_RESUME primary key (ResumeId)
)
;

/*==============================================================*/
/* Table: Resume4Client                                         */
/*==============================================================*/
create table dbo.Resume4Client (
   ResumeId             int                  not null,
   ClientCodeId         int                  not null,
   constraint PK_RESUME4CLIENT primary key (ResumeId, ClientCodeId)
)
;

/*==============================================================*/
/* Table: Script                                                */
/*==============================================================*/
create table dbo.Script (
   ScriptId             int                  identity,
   VersionId            int                  not null,
   ScriptOrder          int                  not null,
   ScriptText           varchar(max)         not null,
   constraint PK_SCRIPT primary key (ScriptId)
)
;

/*==============================================================*/
/* Table: SqlAction                                             */
/*==============================================================*/
create table dbo.SqlAction (
   SqlActionId          int                  identity,
   SqlActionName        varchar(50)          not null,
   SqlActionIsForColumn bit                  not null,
   SqlActionIsForTable  bit                  not null,
   SqlActionIsForIndex  bit                  not null,
   SqlActionTitle       varchar(100)         not null,
   constraint PK_SQLACTION primary key (SqlActionId)
)
;

/*==============================================================*/
/* Table: TypeObject                                            */
/*==============================================================*/
create table dbo.TypeObject (
   TypeObjectId         int                  identity,
   TypeObjectName       varchar(50)          not null,
   TypeObjectSqlServerCode varchar(5)           not null,
   TypeObjectPlurial    varchar(50)          not null,
   TypeObjectPrestentOrder tinyint              not null,
   TypeObjectTilte      varchar(50)          not null,
   TypeObjectNeedColumnDefinition bit                  not null,
   constraint PK_TYPEOBJECT primary key (TypeObjectId)
)
;

/*==============================================================*/
/* Table: Version                                               */
/*==============================================================*/
create table dbo.Version (
   VersionId            int                  identity,
   ProjectId            int                  not null,
   VersionPrincipal     int                  not null,
   VersionSecondary     int                  not null,
   VersionIsLocked      bit                  not null default 0,
   constraint PK_VERSION primary key (VersionId)
)
;

/*==============================================================*/
/* Table: WorkItem                                              */
/*==============================================================*/
create table dbo.WorkItem (
   WorkItemId           int                  identity,
   WorkItemExternalCode Varchar(20)          not null,
   WorkItemName         Varchar(50)          not null,
   constraint PK_WORKITEM primary key (WorkItemId)
)
;

/*==============================================================*/
/* Table: WorkItemObject                                        */
/*==============================================================*/
create table dbo.WorkItemObject (
   WorkItemId           int                  not null,
   ObjectId             int                  not null,
   constraint PK_WORKITEMOBJECT primary key (WorkItemId, ObjectId)
)
;

/*==============================================================*/
/* Table: WorkItemScript                                        */
/*==============================================================*/
create table dbo.WorkItemScript (
   WorkItemId           int                  not null,
   ScriptId             int                  not null,
   constraint PK_WORKITEMSCRIPT primary key (WorkItemId, ScriptId)
)
;

alter table dbo.Base
   add constraint FK_BASE_41_CODECLIENT foreign key (ClientCodeId)
      references dbo.ClientCode (ClientCodeId)
;

alter table dbo.Bloc
   add constraint FK_BLOC_4A_SQLACTION foreign key (SqlActionId)
      references dbo.SqlAction (SqlActionId)
;

alter table dbo.Bloc
   add constraint FK_BLOC_4A_SQLSCRIPT foreign key (ScriptId)
      references dbo.Script (ScriptId)
;

alter table dbo.Bloc
   add constraint FK_BLOC_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.Bloc
   add constraint FK_BLOC_IS_SPECIFIC_CODECLIENT foreign key (ClientCodeId)
      references dbo.ClientCode (ClientCodeId)
;

alter table dbo."Column"
   add constraint FK_COLUMN_IS4_OBJECT foreign key (ObjectId)
      references dbo.Object (ObjectId)
;

alter table dbo.DataBaseObject
   add constraint FK_DATABASEOBJECT_4A_SQLSCRIPT foreign key (ScriptId)
      references dbo.Script (ScriptId)
;

alter table dbo.DataBaseObject
   add constraint FK_DATABASEOBJECT_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.Object
   add constraint FK_OBJECT_AS_A_TYPE foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.Object
   add constraint FK_OBJECT_IN_VERSION foreign key (VersionId)
      references dbo.Version (VersionId)
;

alter table dbo.Object
   add constraint FK_OBJECT_IS4A_CODECLIENT foreign key (ClientCodeId)
      references dbo.ClientCode (ClientCodeId)
;

alter table dbo.Resume
   add constraint FK_RESUME_4A_SQLACTION foreign key (SqlActionId)
      references dbo.SqlAction (SqlActionId)
;

alter table dbo.Resume
   add constraint FK_RESUME_4A_SQLSCRIPT foreign key (ScriptId)
      references dbo.Script (ScriptId)
;

alter table dbo.Resume
   add constraint FK_RESUME_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.Resume4Client
   add constraint FK_RESUME4CLIENT_4A_CODECLIENT foreign key (ClientCodeId)
      references dbo.ClientCode (ClientCodeId)
;

alter table dbo.Resume4Client
   add constraint FK_RESUME4CLIENT_4A_RESUME foreign key (ResumeId)
      references dbo.Resume (ResumeId)
;

alter table dbo.Script
   add constraint FK_SCRIPT_4A_VERSION foreign key (VersionId)
      references dbo.Version (VersionId)
;

alter table dbo.Version
   add constraint FK_VERSION_4A_PROJECT foreign key (ProjectId)
      references dbo.Project (ProjectId)
;

alter table dbo.WorkItemObject
   add constraint FK_WORKITEM_41_OBJECT_1 foreign key (ObjectId)
      references dbo.Object (ObjectId)
;

alter table dbo.WorkItemObject
   add constraint FK_WORKITEM_41_OBJECT_2 foreign key (WorkItemId)
      references dbo.WorkItem (WorkItemId)
;

alter table dbo.WorkItemScript
   add constraint FK_WORKITEM_41_SCRIPT_1 foreign key (WorkItemId)
      references dbo.WorkItem (WorkItemId)
;

alter table dbo.WorkItemScript
   add constraint FK_WORKITEM_41_SCRIPT_2 foreign key (ScriptId)
      references dbo.Script (ScriptId)
;

