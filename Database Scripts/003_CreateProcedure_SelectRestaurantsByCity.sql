USE [RestaurantReviews]
GO

/****** Object:  StoredProcedure [dbo].[SelectRestaurantsByCity]    Script Date: 10/30/2020 10:52:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectRestaurantsByCity]
	@city NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Id], [Name]
	FROM Restaurant
	WHERE City = @city
END
GO


