USE [CardboardCastle];
GO

IF NOT EXISTS 
(
	SELECT
		1
	FROM sys.objects
	WHERE
		object_id = OBJECT_ID(N'[dbo].[<ProcName>]') AND
		type IN (N'P', N'PC')
)
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[<ProcName>] AS SET NOCOUNT ON;');
	PRINT '<ProcName> procedure created';
END

GO

ALTER PROCEDURE [dbo].[<ProcName>]
(
	@Parameter INT
)
AS
BEGIN
	BEGIN TRANSACTION T1;
	BEGIN TRY

		PRINT 'Do something';

		COMMIT TRANSACTION T1;
		RETURN -200;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION T1;
		EXEC [dbo].[LogIssue] @LogMessage = ERROR_MESSAGE, @LogState = 'Error', @CreatedBy = ERROR_PROCEDURE;
		RETURN -500;
	END CATCH
END