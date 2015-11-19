CREATE PROCEDURE [dbo].[ReviewAdd]
	@RestaurantID int,
	@UserID int,
	@Rating int,
	@Comments text
AS
	IF EXISTS(SELECT Id FROM Reviews WHERE RestaurantID=@RestaurantID AND UserID = @UserID)
		BEGIN
			SELECT -1;
			RETURN;
		END
	INSERT INTO Reviews (RestaurantID, UserID, Rating, Comments, DateCreated)
	VALUES(@RestaurantID, @UserID, @Rating, @Comments, GetDate())
	SELECT 0;
