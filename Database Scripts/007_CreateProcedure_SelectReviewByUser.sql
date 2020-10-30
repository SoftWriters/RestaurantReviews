USE [RestaurantReviews]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SelectReviewsByUser]
	@id UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT review.[Id], review.[RestaurantId], [Rating], [ReviewText]
	FROM [dbo].[Review] review
	WHERE UserId = @id AND IsDeleted = 0
END
GO


