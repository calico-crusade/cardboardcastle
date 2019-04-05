USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[Utility]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[Utility] (
		--Identity
		[UtilityId] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[DwellingId] INT NOT NULL,
		[Name] VARCHAR(256) NOT NULL,
		[Amount] MONEY NOT NULL,
		[Type] INT NOT NULL,
		[Pattern] VARCHAR(256) NULL,
		[PaymentDate] DATETIME NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Utility_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_Utility_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_Utility_Id PRIMARY KEY CLUSTERED ([UtilityId])
	)

	--Create any nonclustered indexs 
	CREATE NONCLUSTERED INDEX PK_Utility_DwellingId ON [dbo].[Utility] (DwellingId);

	PRINT 'Utility Table Created';
END

-- Modify the structure above for after-initial run