/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     16/05/2021 19:12:40                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Bloc') and o.name = 'FK_BLOC_4A_SQLACTION')
alter table dbo.Bloc
   drop constraint FK_BLOC_4A_SQLACTION
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Bloc') and o.name = 'FK_BLOC_4A_SQLSCRIPT')
alter table dbo.Bloc
   drop constraint FK_BLOC_4A_SQLSCRIPT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Bloc') and o.name = 'FK_BLOC_4A_SQLWHAT')
alter table dbo.Bloc
   drop constraint FK_BLOC_4A_SQLWHAT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Bloc') and o.name = 'FK_BLOC_IS_SPECIFIC_CODECLIENT')
alter table dbo.Bloc
   drop constraint FK_BLOC_IS_SPECIFIC_CODECLIENT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DataBaseObject') and o.name = 'FK_DATABASEOBJECT_4A_SQLSCRIPT')
alter table dbo.DataBaseObject
   drop constraint FK_DATABASEOBJECT_4A_SQLSCRIPT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DataBaseObject') and o.name = 'FK_DATABASEOBJECT_4A_SQLWHAT')
alter table dbo.DataBaseObject
   drop constraint FK_DATABASEOBJECT_4A_SQLWHAT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Resume') and o.name = 'FK_RESUME_4A_SQLACTION')
alter table dbo.Resume
   drop constraint FK_RESUME_4A_SQLACTION
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Resume') and o.name = 'FK_RESUME_4A_SQLSCRIPT')
alter table dbo.Resume
   drop constraint FK_RESUME_4A_SQLSCRIPT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Resume') and o.name = 'FK_RESUME_4A_SQLWHAT')
alter table dbo.Resume
   drop constraint FK_RESUME_4A_SQLWHAT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Resume4Client') and o.name = 'FK_RESUME4CLIENT_4A_RESUME')
alter table dbo.Resume4Client
   drop constraint FK_RESUME4CLIENT_4A_RESUME
;

alter table dbo.Bloc
   drop constraint PK_BLOC
;

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Bloc')
            and   type = 'U')
   drop table dbo.Bloc
;



alter table dbo.DataBaseObject
   drop constraint PK_DATABASEOBJECT
;

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.DataBaseObject')
            and   type = 'U')
   drop table dbo.DataBaseObject
;


;

alter table dbo.Resume
   drop constraint PK_RESUME
;

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Resume')
            and   type = 'U')
   drop table dbo.Resume
;



if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SqlWhat')
            and   type = 'U')
   drop table dbo.SqlWhat
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
/* Table: DataBaseObject                                        */
/*==============================================================*/
create table dbo.DataBaseObject (
   DatabaseObjectId     int                  identity,
   ScriptId             int                  not null,
   TypeObjectId         int                  not null,
   DatabaseObjectDataBase varchar(100)         not null,
   DatabaseObjectSchema varchar(20)          not null,
   DatabaseObjectName   varchar(100)         not null,
   constraint PK_DATABASEOBJECT primary key (DatabaseObjectId)
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

alter table dbo.DataBaseObject
   add constraint FK_DATABASEOBJECT_4A_SQLSCRIPT foreign key (ScriptId)
      references dbo.Script (ScriptId)
;

alter table dbo.DataBaseObject
   add constraint FK_DATABASEOBJECT_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
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
   add constraint FK_RESUME4CLIENT_4A_RESUME foreign key (ResumeId)
      references dbo.Resume (ResumeId)
;

