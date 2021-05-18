USE [RestaurantReviews]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertUser]
	@id UNIQUEIDENTIFIER,
	@firstName NVARCHAR(20),
	@lastName NVARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[User] (Id, FirstName, LastName)
	VALUES (@id, @firstName, @lastName)
END
GO
