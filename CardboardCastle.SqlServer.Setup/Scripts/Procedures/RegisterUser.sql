USE [CardboardCastle];
GO

IF NOT EXISTS 
(
	SELECT
		1
	FROM sys.objects
	WHERE
		object_id = OBJECT_ID(N'[dbo].[RegisterUser]') AND
		type IN (N'P', N'PC')
)
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[RegisterUser] AS SET NOCOUNT ON;');
	PRINT 'RegisterUser procedure created';
END

GO

ALTER PROCEDURE [dbo].[RegisterUser]
(
	@FirstName VARCHAR(256),
	@LastName VARCHAR(256),
	@EmailAddress VARCHAR(256),
	@Password VARCHAR(MAX)
)
AS
BEGIN
	BEGIN TRANSACTION T1;
	BEGIN TRY

		DECLARE @ModUser VARCHAR(256) = 'System';

		IF EXISTS
		(
			SELECT 
				1 
			FROM [dbo].[User] WITH NOLOCK
			WHERE
				LTRIM(RTRIM(LOWER(@EmailAddress))) = LTRIM(RTRIM(LOWER(EmailAddress)))
		)
		BEGIN
			RETURN -409; --Conflict (Duplicate Users)
		END

		INSERT INTO [dbo].[User]
		(
			FirstName,
			LastName,
			EmailAddress,
			Password,
			CreatedBy,
			ModifiedBy
		)
		VALUES
		(
			@FirstName,
			@LastName,
			@EmailAddress,
			@Password,
			@ModUser,
			@ModUser
		)

		DECLARE @UserId INT = SCOPE_IDENTITY();

		COMMIT TRANSACTION T1;
		RETURN @UserId;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION T1;
		EXEC [dbo].[LogIssue] @LogMessage = ERROR_MESSAGE, @LogState = 'Error', @CreatedBy = ERROR_PROCEDURE;
		RETURN -500;
	END CATCH
END