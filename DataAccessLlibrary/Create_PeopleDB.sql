CREATE DATABASE PeopleDB
GO

USE [PeopleDB]
GO
CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(50)  NULL
)
GO
/****** Object:  StoredProcedure [dbo].[spPerson_Delete]    Script Date: 12/9/2021 8:13:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPerson_Delete]
	@Id int
AS
begin
	Delete
	From	[dbo].[Person]
	Where	Id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[spPerson_Get]    Script Date: 12/9/2021 8:13:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPerson_Get]
	@Id int
AS
begin
	Select  
			Id,
			FirstName,
			LastName,
			Email
	From 
			[dbo].[Person]
	Where
			id = @Id;
end
GO
/****** Object:  StoredProcedure [dbo].[spPerson_GetAll]    Script Date: 12/9/2021 8:13:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPerson_GetAll]
AS
begin
	Select  
			Id,
			FirstName,
			LastName,
			Email
	From 
			[dbo].[Person];
end
GO
/****** Object:  StoredProcedure [dbo].[spPerson_Insert]    Script Date: 12/9/2021 8:13:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPerson_Insert]
	@Id			int output,
	@FirstName	nvarchar(50) , 
    @LastName	nvarchar(50) ,
	@Email		nvarchar(50) 
AS
begin
	Insert Into [dbo].[Person](FirstName,LastName,Email)
	values (@FirstName, @LastName, @Email);
	set @Id = @@IDENTITY
end
GO
/****** Object:  StoredProcedure [dbo].[spPerson_Update]    Script Date: 12/9/2021 8:13:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPerson_Update]
	@Id				int,
	@FirstName		nvarchar(50),
	@LastName		nvarchar(50),
	@Email			nvarchar(50)
AS
begin
	Update  [dbo].[Person]
	Set
		FirstName	=	@FirstName
		,LastName	=	@LastName
		,Email		=	@Email
	Where
		Id = @Id;
end
GO
