CREATE PROCEDURE [dbo].[RestaurantsGetByCity]
	@CityID int
AS
	SELECT	Id, CityId, Name
	FROM	Restaurants
	WHERE	CityID = @CityID
