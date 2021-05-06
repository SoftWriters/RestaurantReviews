T.J. Pipon
tjpipon@gmail.com
724-516-1804

I have built a RESTful Web Api with C# in .NET 5.0. 

* JWT Authentication (uid=TestUser, pwd=Password!123)
* Secure, HTTPS enforced
* EFCore with Identity via in-memory database
* Seeded with minimal test data (UserId=[1,2], RestaurantId=[1,2], CityId=[1,2, 3])
* Using Data-Transfer-objects (ViewModel) to only expose small subsets of data
* Async design with LINQ-to-Objects, Lambda Expressions
* Intense data validation and return codes
* Logging with Serilog 
* Swagger UI at launch for full API documentation

For Unit Tests, I used a combination of Postman and Swagger.

Thanks that was super fun!
TJ
