/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     16/05/2021 19:19:26                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Script') and o.name = 'FK_SCRIPT_REFERENCE_VERSION')
alter table dbo.Script
   drop constraint FK_SCRIPT_REFERENCE_VERSION
;

alter table dbo.Object
   add ScriptId int                  null
;

alter table dbo.Object
   add constraint FK_OBJECT_HAS_A_SCRIPT foreign key (ScriptId)
      references dbo.Script (ScriptId)
;

alter table dbo.Script
   add constraint FK_SCRIPT_4A_VERSION foreign key (VersionId)
      references dbo.Version (VersionId)
;

