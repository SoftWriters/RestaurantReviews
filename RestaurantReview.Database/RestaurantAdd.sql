CREATE PROCEDURE [dbo].[RestaurantAdd]
	@CityID int,
	@Name varchar(50)
AS
	IF EXISTS(SELECT Id FROM Restaurants WHERE CityID=@CityID AND Name = @Name)
		BEGIN
			SELECT -1;
			RETURN;
		END
	INSERT INTO Restaurants (CityID, Name)
	VALUES(@CityID, @Name)
	SELECT 0;