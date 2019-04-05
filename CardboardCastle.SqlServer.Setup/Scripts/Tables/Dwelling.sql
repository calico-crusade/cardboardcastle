USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[Dwelling]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[Dwelling] (
		--Identity
		[DwellingId] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[Name] VARCHAR(256) NULL,
		[Street1] VARCHAR(MAX) NULL,
		[Street2] VARCHAR(MAX) NULL,
		[City] VARCHAR(256) NULL,
		[State] VARCHAR(256) NULL,
		[PostalCode] VARCHAR(256) NULL,
		[FriendlyName] VARCHAR(256) NOT NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Dwelling_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_Dwelling_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_Dwelling_Id PRIMARY KEY CLUSTERED ([DwellingId])
	)

	--Create any nonclustered indexs 

	PRINT 'Dwelling Table Created';
END

-- Modify the structure above for after-initial run