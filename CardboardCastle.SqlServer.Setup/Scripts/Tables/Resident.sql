USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[Resident]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[Resident] (
		--Identity
		[ResidentId] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[UserId] INT NULL,
		[Nickname] VARCHAR(256) NOT NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Resident_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_Resident_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_Resident_Id PRIMARY KEY CLUSTERED ([ResidentId])
	)

	--Create any nonclustered indexs 

	PRINT 'Resident Table Created';
END

-- Modify the structure above for after-initial run