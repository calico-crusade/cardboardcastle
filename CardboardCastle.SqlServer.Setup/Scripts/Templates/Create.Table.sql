--Replace <TableName> with the name of the table

USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[<TableName>]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[<TableName>] (
		--Identity
		[<TableName>Id] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns

		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_<TableName>_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_<TableName>_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_<TableName>_Id PRIMARY KEY CLUSTERED ([<TableName>Id])
	)

	--Create any nonclustered indexs 

	PRINT '<TableName> Table Created';
END

-- Modify the structure above for after-initial run