/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     26/06/2021 11:18:54                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Bloc') and o.name = 'FK_BLOC_4A_TYPEOBJECT')
alter table dbo.Bloc
   drop constraint FK_BLOC_4A_TYPEOBJECT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.DataBaseObject') and o.name = 'FK_DATABASEOBJECT_4A_TYPEOBJECT')
alter table dbo.DataBaseObject
   drop constraint FK_DATABASEOBJECT_4A_TYPEOBJECT
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Object') and o.name = 'FK_OBJECT_AS_A_TYPE')
alter table dbo.Object
   drop constraint FK_OBJECT_AS_A_TYPE
;

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Resume') and o.name = 'FK_RESUME_4A_TYPEOBJECT')
alter table dbo.Resume
   drop constraint FK_RESUME_4A_TYPEOBJECT
;

alter table dbo.TypeObject
   drop constraint PK_TYPEOBJECT
;

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.tmp_TypeObject')
            and   type = 'U')
   drop table dbo.tmp_TypeObject
;

execute sp_rename TypeObject, tmp_TypeObject
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

set identity_insert dbo.TypeObject on
;

insert into dbo.TypeObject (TypeObjectId, TypeObjectName, TypeObjectSqlServerCode, TypeObjectPlurial, TypeObjectPrestentOrder, TypeObjectTilte, TypeObjectNeedColumnDefinition)
select TypeObjectId, TypeObjectName, TypeObjectSqlServerCode, TypeObjectPlurial, TypeObjectPrestentOrder, TypeObjectTilte, TypeObjectNeedColumnDefinition
from dbo.tmp_TypeObject
;

set identity_insert dbo.TypeObject off
;

alter table dbo.Bloc
   add constraint FK_BLOC_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.DataBaseObject
   add constraint FK_DATABASEOBJECT_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.Object
   add constraint FK_OBJECT_AS_A_TYPE foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

alter table dbo.Resume
   add constraint FK_RESUME_4A_TYPEOBJECT foreign key (TypeObjectId)
      references dbo.TypeObject (TypeObjectId)
;

