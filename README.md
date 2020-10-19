RestaurantReviewsApi
=================

Solution
--------------
This solution consists of a .NET Core 3.1 web api with SQL Server backend.
This api is complete with swagger documentation, validation, authentication, authorization, and unit tests. 

Usage
--------------
To Run
1. Open the solution: src\RestaurantReviewsApi\RestaurantReviewsApi.sln
2. Publish the Database Project: RestaurantReviewsApi.Database.sqlproj
3. Update the AppSettings.json of the Api to point to newly published database. (Data:DefaultConnection:ConnectionString)
4. Run RestaurantReviewsApi.csproj

Authentication
--------------
All endpoints with the exception of debug and auth require a JWT bearer token.
Calling auth/{username} will provide the caller with an accesstoken.
For the purpose of this demo, ALL usernames are valid. 
Entering a username that contains "Admin" will also grant the user ADMIN scope, allowing the following endpoints to be hit:
- PostRestaurant
- PatchRestaurant
- DeleteRestaurant

To authenticate requests, add the following header:
- {"Authorization": "bearer {access_token}"}
- Note, swagger will add authorization headers using the green authorize button on the right hand side of the swagger ui.