CREATE PROCEDURE [dbo].[InsertLocation]
	@id UNIQUEIDENTIFIER,
	@city NVARCHAR(100)
AS
	IF NOT EXISTS (SELECT 1 FROM [dbo].[Location]
					WHERE Id = @id
					OR City = @city)
	BEGIN
 		INSERT INTO [dbo].[Location] (Id, City)
		VALUES (@id, @city)
	END
RETURN 0
