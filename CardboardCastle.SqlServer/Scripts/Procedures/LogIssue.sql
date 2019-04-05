USE [CardboardCastle];
GO

IF NOT EXISTS 
(
	SELECT
		1
	FROM sys.objects
	WHERE
		object_id = OBJECT_ID(N'[dbo].[LogIssue]') AND
		type IN (N'P', N'PC')
)
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[LogIssue] AS SET NOCOUNT ON;');
	PRINT 'LogIssue procedure created';
END

GO

ALTER PROCEDURE [dbo].[LogIssue]
(
	@LogMessage VARCHAR(MAX),
	@LogState VARCHAR(MAX),
	@CreatedBy VARCHAR(256)
)
AS
BEGIN
	BEGIN TRANSACTION T1;
	BEGIN TRY

		INSERT INTO [dbo].[Log]
		(
			LogMessage,
			LogState,
			CreatedBy,
			ModifiedBy
		)
		VALUES
		(
			@LogMessage,
			@LogState,
			@CreatedBy,
			'Issue Logging'
		)

		COMMIT TRANSACTION T1;
        RETURN 200;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION T1;
        RETURN 500;
	END CATCH
END