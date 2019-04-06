USE [CardboardCastle];
GO

IF NOT EXISTS 
(
	SELECT
		1
	FROM sys.objects
	WHERE
		object_id = OBJECT_ID(N'[dbo].[FetchUser]') AND
		type IN (N'P', N'PC')
)
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[FetchUser] AS SET NOCOUNT ON;');
	PRINT 'FetchUser procedure created';
END

GO

ALTER PROCEDURE [dbo].[FetchUser]
(
	@Email VARCHAR(256)
)
AS
BEGIN
	BEGIN TRY

		SELECT
			*
		FROM [dbo].[User] WITH (NOLOCK)
		WHERE
			LOWER(RTRIM(LTRIM(@Email))) = LOWER(RTRIM(LTRIM(EmailAddress))) AND
			ObsoletedOn IS NULL

		RETURN -200;
	END TRY
	BEGIN CATCH
		EXEC [dbo].[LogIssue] @LogMessage = ERROR_MESSAGE, @LogState = 'Error', @CreatedBy = ERROR_PROCEDURE;
		RETURN -500;
	END CATCH
END