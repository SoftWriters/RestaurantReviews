SET IDENTITY_INSERT dbo.Restaurants ON;

MERGE dbo.Restaurants AS TARGET
USING (VALUES 
	(1, 'Chipotle Mexican Grill', '3009 Washington Pike', 'Bridgeville', 'PA', '15017'),
	(2, 'Blaze Pizza', '160 Millers Run Road', 'Bridgeville', 'PA', '15017'),
	(3, 'The Original Hot Dog Shop', '3901 Forbes Avenue', 'Pittsburgh', 'PA', '15213'),
	(4, 'Chipotle Mexican Grill', '3615 Forbes Avenue', 'Pittsburgh', 'PA', '15213')
) AS SOURCE (RestaurantId, [Name], [Address], City, [State], ZipCode)
ON (TARGET.RestaurantId = SOURCE.RestaurantId)
WHEN NOT MATCHED BY TARGET THEN
	INSERT (RestaurantId, [Name], [Address], City, [State], ZipCode)
	VALUES (SOURCE.RestaurantId, SOURCE.[Name], SOURCE.[Address], SOURCE.City, SOURCE.[State], SOURCE.ZipCode);

SET IDENTITY_INSERT dbo.Restaurants OFF;

SET IDENTITY_INSERT dbo.Users ON;

MERGE dbo.Users AS TARGET
USING (VALUES 
	(1, 'testuser1', 'Test', 'User'),
	(2, 'testuser2', 'Test', 'User')
) AS SOURCE (UserId, Username, FirstName, LastName)
ON (TARGET.UserId = SOURCE.UserId)
WHEN NOT MATCHED BY TARGET THEN
	INSERT (UserId, Username, FirstName, LastName)
	VALUES (SOURCE.UserId, SOURCE.Username, SOURCE.FirstName, SOURCE.LastName);

SET IDENTITY_INSERT dbo.Users OFF;

SET IDENTITY_INSERT dbo.Reviews ON;

MERGE dbo.Reviews AS TARGET
USING (VALUES
	(1, 1, 1, 'Delicious'),
	(2, 2, 1, 'Yummy'),
	(3, 3, 2, 'Best fries in Pittsburgh')
) AS SOURCE (ReviewId, RestaurantId, UserId, Content)
ON (TARGET.ReviewId = SOURCE.ReviewId)
WHEN NOT MATCHED BY TARGET THEN
	INSERT (ReviewId, RestaurantId, UserId, Content)
	VALUES (SOURCE.ReviewId, SOURCE.RestaurantId, SOURCE.UserId, SOURCE.Content);

SET IDENTITY_INSERT dbo.Reviews OFF;
