-- create test user id
DECLARE @testUserId NVARCHAR(MAX) = N'00000000-0000-0000-0000-000000000000';
SELECT @testUserId, CONVERT(uniqueidentifier, @testUserId);

-- create test review id
DECLARE @testReviewId NVARCHAR(MAX) = N'00000000-0000-0000-0000-000000000001';
SELECT @testReviewId, CONVERT(uniqueidentifier, @testReviewId);

-- create test restaurant id
DECLARE @testRestaurantId NVARCHAR(MAX) = N'00000000-0000-0000-0000-000000000002';
SELECT @testRestaurantId, CONVERT(uniqueidentifier, @testRestaurantId);

-- create test location id
DECLARE @testLocationId NVARCHAR(MAX) = N'00000000-0000-0000-0000-000000000003';
SELECT @testLocationId, CONVERT(uniqueidentifier, @testLocationId);


-- insert test user
EXEC InsertUser @testUserId, 'Test', 'User';

-- insert test location
EXEC InsertLocation @testLocationId, 'TestCity';

-- insert test restaurant
EXEC InsertRestaurant @testRestaurantId, 'TestRestaurant', 'TestCity'

-- insert test review
EXEC InsertReview @testReviewId, @testUserId, @testRestaurantId, 5, 'Fantastic!'