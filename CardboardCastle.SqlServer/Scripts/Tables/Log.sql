USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[Log]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[Log] (
		--Identity
		[Id] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[LogMessage] VARCHAR(MAX) NOT NULL,
		[LogState] VARCHAR(MAX) NOT NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Log_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_Log_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_Log_Id PRIMARY KEY CLUSTERED ([Id])
	)

	--Create any nonclustered indexs 

	PRINT 'Log Table Created';
END

-- Modify the structure above for after-initial run