CREATE PROCEDURE [dbo].[ReviewsGetByUser]
	@UserID int
AS
	SELECT  Id, RestaurantId, UserId, Rating, Comments, DateCreated
	FROM	Reviews
	WHERE	UserID = @UserID