USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[Residency]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[Residency] (
		--Identity
		[ResidencyId] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[ResidentId] INT NOT NULL,
		[DwellingId] INT NOT NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Residency_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_Residency_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_Residency_Id PRIMARY KEY CLUSTERED ([ResidencyId])
	)

	--Create any nonclustered indexs 
	CREATE NONCLUSTERED INDEX PK_Residency_ResidentId ON [dbo].[Residency] (ResidentId);
	CREATE NONCLUSTERED INDEX PK_Residency_DwellingId ON [dbo].[Residency] (DwellingId);

	PRINT 'Residency Table Created';
END

-- Modify the structure above for after-initial run