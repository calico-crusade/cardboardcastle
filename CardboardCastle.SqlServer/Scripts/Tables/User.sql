--Replace User with the name of the table

USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[User]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[User] (
		--Identity
		[UserId] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[FirstName] VARCHAR(256) NOT NULL,
		[LastName] VARCHAR(256) NOT NULL,
		[EmailAddress] VARCHAR(256) NOT NULL,
		[Password] VARCHAR(MAX) NOT NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_User_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_User_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_User_Id PRIMARY KEY CLUSTERED ([UserId])
	)

	--Create any nonclustered indexs 

	PRINT 'User Table Created';
END

-- Modify the structure above for after-initial run