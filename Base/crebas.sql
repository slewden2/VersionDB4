/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     30/05/2021 19:29:13                          */
/*==============================================================*/


alter table dbo.Version
   add VersionIsLocked bit                  not null default 0
;

