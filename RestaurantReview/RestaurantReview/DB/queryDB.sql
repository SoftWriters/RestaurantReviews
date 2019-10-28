SELECT * FROM Restaurants
SELECT * FROM Users
SELECT * FROM Reviews

SELECT * FROM Reviews 
JOIN Restaurants on Reviews.RestaurantId = Restaurants.RestaurantId
JOIN Users on Reviews.UserId = Users.UserId
