--Creating database only if database is not created yet
IF DB_ID('Nedeljni_III_Arnautovic_Prpos_DDL') IS NULL
CREATE DATABASE Nedeljni_III_Arnautovic_Prpos_DDL
GO
USE Nedeljni_III_Arnautovic_Prpos_DDL;



if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblComponents')
drop table tblComponents;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblRecept')
drop table tblRecept;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser;

if OBJECT_ID('vwRecept','v') IS NOT NULL DROP VIEW vwRecept;

create table tblUser (
UserId int identity(1,1) primary key,
FullName nvarchar (50) not null,
Username nvarchar (50) unique not null,
Pasword nvarchar (50) not null 
)

create table tblRecept (
ReceptID int identity(1,1) primary key,
UserID int not null,
ReceptName nvarchar(100) not null,
ReceptType nvarchar (20) not null,
PersonNumber int not null,
Author nvarchar(100) not null,
ReceptText nvarchar(100) not null,
CreationDate date not null
)

create table tblComponents(
ComponentID int identity (1,1) primary key,
ReceptID int not null,
ComponentName nvarchar(50) not null,
ComponentAmount int not null
)

Insert into tblUser values ('Admin Name','Admin','Admin123');

Alter Table tblRecept
Add foreign key (UserID) references tblUser(UserID);

Alter Table tblComponents
Add foreign key (ReceptID) references tblRecept(ReceptID);


GO
CREATE VIEW vwRecept AS
	SELECT	tblRecept.*,
			tblComponents.ComponentAmount,tblComponents.ComponentName,tblComponents.ComponentID
	From tblRecept,tblComponents
	WHERE	tblRecept.ReceptID = tblComponents.ReceptID



	

