Some things to note about the RestaurantReviews solution provided:

1. The solution requires .NET version 4.5.1.

2. The solution assumes that there is a database named 'RestaurantReviews' in local SQL Server.       

3. An ASP.NET boilerplate was used to setup up this solution quickly with Entity Framework and AngularJS as selected technologies.
   You will find files of importance in the following locations:

	A. Entity objects are part of the core business layer and defined in the following folder:
	    '\RestaurantReviews\RestaurantReviews.Core\Entities\'
		
	B. Database sets are defined in:
	    '\RestaurantReviews\RestaurantReviews.EntityFramework\EntityFramework\RestaurantReviewsDbContext.cs'
		
	C. Connection strings for the database are defined in:
	    '\RestaurantReviews\RestaurantReviews.Web\Web.config'
		
	D. The database seed method can be found in:
	    '\RestaurantReviews\RestaurantReviews.EntityFramework\Migrations\Configuration.cs'
		
	E. The database migration file lives here:
	    '\RestaurantReviews\RestaurantReviews.EntityFramework\Migrations\201501252224191_InitialCreate.cs'
		
		- To populate database run the following commands from the package manager console:
		    PM> Add-Migration "InitialCreate"
            PM> Update-database
			
    F. Repository interfaces are also part of the core business layer and defined in the following folder:
	    '\RestaurantReviews\RestaurantReviews.Core\IRepositories\'
		
	G. The implementations of the above repository interfaces can be found in the RestaurantReviews.EntityFramework project:
	    '\RestaurantReviews\RestaurantReviews.EntityFramework\EntityFramework\Repositories\'
		
	H. The input and output datatype objects exposed in the application service can be found here:
	    '\RestaurantReviews\RestaurantReviews.Application\DtoInput.cs'
		'\RestaurantReviews\RestaurantReviews.Application\DtoOutput.cs'
		
	I. The application service API methods that satisfy the project requirements can be found here:
	    '\RestaurantReviews\RestaurantReviews.Application\RestaurantReviewsAppService.cs'
		NOTE: Read public method comments
		
NOTE: all text files will be checked in with linux line endings. Be advised if your git configuration
      is not set to convert back to Windows line endings.
