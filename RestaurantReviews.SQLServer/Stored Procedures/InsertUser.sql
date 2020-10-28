CREATE PROCEDURE [dbo].[InsertUser]
	@id UNIQUEIDENTIFIER,
	@firstName NVARCHAR(20),
	@lastName NVARCHAR(20)
AS
	IF NOT EXISTS (SELECT 1
				   FROM [dbo].[User]
				   WHERE Id = @id)
	BEGIN
		INSERT INTO [dbo].[User] (Id, FirstName, LastName)
		VALUES (@id, @firstName, @lastName)
	END
RETURN 0
