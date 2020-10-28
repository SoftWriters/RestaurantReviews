CREATE PROCEDURE [dbo].[InsertRestaurant]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(100),
	@city NVARCHAR(100)
AS
	IF NOT EXISTS (SELECT 1
				   FROM [dbo].[Restaurant]
				   WHERE Id = @id)
	BEGIN
		BEGIN TRAN T1
			DECLARE @locationId UNIQUEIDENTIFIER;
			SET @locationId = (SELECT Id 
							   FROM [dbo].[Location] 
							   WHERE City = @city);

			INSERT INTO Restaurant (Id, Name, LocationId)
			VALUES (@id, @name, @locationId)		
		COMMIT TRAN T1
	END
RETURN 0
