/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     18/05/2021 01:22:08                          */
/*==============================================================*/


alter table dbo.DataBaseObject
   add DatabaseObjectColumn varchar(100)         null
;

alter table dbo.Object
   add ObjectColumn varchar(100)         null
;

