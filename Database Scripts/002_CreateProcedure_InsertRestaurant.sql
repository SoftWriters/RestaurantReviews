USE [RestaurantReviews]
GO

/****** Object:  StoredProcedure [dbo].[InsertRestaurant]    Script Date: 10/30/2020 10:02:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertRestaurant]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(100),
	@city NVARCHAR(100)
AS
	IF NOT EXISTS (SELECT 1
				   FROM [dbo].[Restaurant]
				   WHERE Name = @name AND City = @city)
	
	INSERT INTO Restaurant (Id, Name, City)
	VALUES (@id, @name, @city)		
	
RETURN 0
GO