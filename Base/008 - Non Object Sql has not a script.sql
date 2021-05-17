/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     17/05/2021 23:33:55                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Object') and o.name = 'FK_OBJECT_HAS_A_SCRIPT')
alter table dbo.Object
   drop constraint FK_OBJECT_HAS_A_SCRIPT
;

alter table dbo.Object
   drop column ScriptId
;

