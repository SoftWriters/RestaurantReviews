In my zip file, you will find 4 projects:

* There are some fairly simple instructions needed to get this solution working.
1) You should be able to start the API solution up and see the Swagger UI without a backing database.
2) There is folder in the RestaurantReviewData project called DatabaseFile that contains an .mdf file.
3) That database file will need to be attached to an instance of SQL Server.
4) Then you will need to update RestaurantReviewConntectionString in the appsetting.json file with an appropriate connection string.

* RestaurantReview
	Most of the code in this project is auto-generated.
	The one exception to that is the file RestaurantReviewController.Extensions.cs
	I explicitly separated this controller into 2 partial classes to segregate my code.
	Everything in RestaurantReviewController.Extensions.cs was written by me.

* RestaurantReviewBusiness
	All of the code in this project was written by me as well.

* RestaurantReviewData
	This is all entity framework core and it was auto-generated.

* RestaurantReviewUnitTests
	All of the unit tests were authored by me as well.