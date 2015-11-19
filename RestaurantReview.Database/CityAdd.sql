CREATE PROCEDURE [dbo].[CityAdd]
	@City varchar(2),
	@State varchar(50)
AS
	IF EXISTS(SELECT Id FROM Cities WHERE CityName=@City AND [State] = @State)
		BEGIN
			SELECT -1;
			RETURN;
		END
	INSERT INTO Cities (CityName, [State])
	VALUES(@City, @State)
	SELECT 0;