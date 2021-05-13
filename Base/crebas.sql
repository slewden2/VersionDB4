/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     13/05/2021 15:55:44                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Base') and o.name = 'FK_BASE_IN_VERSION')
alter table dbo.Base
   drop constraint FK_BASE_IN_VERSION
;

alter table dbo.Base
   drop column VersionId
;

