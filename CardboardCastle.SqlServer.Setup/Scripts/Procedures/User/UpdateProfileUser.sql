USE [CardboardCastle];
GO

IF NOT EXISTS 
(
	SELECT
		1
	FROM sys.objects
	WHERE
		object_id = OBJECT_ID(N'[dbo].[UpdateProfileUser]') AND
		type IN (N'P', N'PC')
)
BEGIN
	EXEC('CREATE PROCEDURE [dbo].[UpdateProfileUser] AS SET NOCOUNT ON;');
	PRINT 'UpdateProfileUser procedure created';
END

GO

ALTER PROCEDURE [dbo].[UpdateProfileUser]
(
	@UserId INT,
	@EmailAddress VARCHAR(256),
	@FirstName VARCHAR(256),
	@LastName VARCHAR(256)
)
AS
BEGIN
	BEGIN TRANSACTION T1;
	BEGIN TRY

		UPDATE [dbo].[User]
		SET
			EmailAddress = @EmailAddress,
			FirstName = @FirstName,
			LastName = @LastName,
			ModifiedOn = GETDATE(),
			ModifiedBy = 'Update Proc'
		WHERE
			UserId = @UserId

		COMMIT TRANSACTION T1;
		RETURN -200;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION T1;
		EXEC [dbo].[LogIssue] @LogMessage = ERROR_MESSAGE, @LogState = 'Error', @CreatedBy = ERROR_PROCEDURE;
		RETURN -500;
	END CATCH
END