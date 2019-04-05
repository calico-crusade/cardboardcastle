USE [CardboardCastle];
GO

--Check if table exists, if it doesn't, create it.
IF NOT EXISTS
(
	SELECT
		1
	FROM dbo.sysobjects
	WHERE
		Id = object_id(N'[dbo].[PaymentArrangement]') AND
		OBJECTPROPERTY(id, N'IsTable') = 1
)
BEGIN
	--Create table
	CREATE TABLE [dbo].[PaymentArrangement] (
		--Identity
		[PaymentArrangementId] INT IDENTITY(1,1) NOT NULL,
		--Custom Columns
		[UtilityId] INT NOT NULL,
		[ResidencyId] INT NOT NULL,
		[Amount] MONEY NOT NULL,
		[Type] INT NOT NULL,
		--Audit Trail
		[CreatedBy] VARCHAR(256) NOT NULL,
		[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_PaymentArrangement_CreatedOn DEFAULT GETDATE(),
		[ModifiedBy] VARCHAR(256) NOT NULL,
		[ModifiedOn] DATETIME NOT NULL  CONSTRAINT DF_PaymentArrangement_ModifiedOn DEFAULT GETDATE(),
		[ObsoletedBy] VARCHAR(256) NULL,
		[ObsoletedOn] DATETIME NULL,
		CONSTRAINT PK_PaymentArrangement_Id PRIMARY KEY CLUSTERED ([PaymentArrangementId])
	)

	--Create any nonclustered indexs 
	CREATE NONCLUSTERED INDEX PK_PaymentArrangement_UtilityId ON [dbo].[PaymentArrangement] (UtilityId);
	CREATE NONCLUSTERED INDEX PK_PaymentArrangement_ResidencyId ON [dbo].[PaymentArrangement] (ResidencyId);

	PRINT 'PaymentArrangement Table Created';
END

-- Modify the structure above for after-initial run