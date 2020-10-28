CREATE PROCEDURE [dbo].[InsertReview]
	@id UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER,
	@restaurantId UNIQUEIDENTIFIER,
	@rating INT,
	@reviewText NVARCHAR(500)
AS
	BEGIN TRAN T1
		INSERT INTO [dbo].[Review] (Id, UserId, RestaurantId, Rating, ReviewText)
		VALUES (@id, @userId, @restaurantId, @rating, @reviewText)
	COMMIT TRAN T1
RETURN 0
