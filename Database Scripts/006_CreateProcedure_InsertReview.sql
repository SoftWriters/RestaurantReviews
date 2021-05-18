USE [RestaurantReviews]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertReview]
	@id UNIQUEIDENTIFIER,
	@userId UNIQUEIDENTIFIER,
	@restaurantId UNIQUEIDENTIFIER,
	@rating INT,
	@reviewText NVARCHAR(500)
AS
	INSERT INTO Review (Id, UserId, RestaurantId, Rating, ReviewText, IsDeleted)
	VALUES (@id, @userId, @restaurantId, @rating, @reviewText, 0)	
RETURN 0
GO


