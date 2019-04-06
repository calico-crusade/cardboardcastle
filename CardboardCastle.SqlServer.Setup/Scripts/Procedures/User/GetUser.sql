USE [CardboardCastle];
GO

IF NOT EXISTS 
(
	SELECT
		1
	FROM sys.objects
	WHERE
		object_id = OBJECT_ID(N'[dbo].[GetUser]') AND
		type IN (N'P', N'PC')
)
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[GetUser] AS SET NOCOUNT ON;');
	PRINT 'GetUser procedure created';
END

GO

ALTER PROCEDURE [dbo].[GetUser]
(
	@UserId INT
)
AS
BEGIN
	BEGIN TRY

		SELECT
			*
		FROM [dbo].[User] WITH (NOLOCK)
		WHERE
			UserId = @UserId AND
			ObsoletedOn IS NULL

		RETURN -200;
	END TRY
	BEGIN CATCH
		EXEC [dbo].[LogIssue] @LogMessage = ERROR_MESSAGE, @LogState = 'Error', @CreatedBy = ERROR_PROCEDURE;
		RETURN -500;
	END CATCH
END