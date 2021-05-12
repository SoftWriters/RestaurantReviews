See some documentation around the different endpoints and their failure conditions in the DocsAndResources folder.  I ran the project through visual studio and used Postman to test it.

I used an SqlExpress connection with EntityFramework for my database. I provided a .bak file in DocsAndResources that is the demo database I was using to test my endpoints.  The connection string needs to be defined in appsettings.Development.json/appsettings.json in RestaurantReviewsAPI when running the project

You can also generate an empty database if you follow the instructions in the parameter-less constructor in DatabaseContext.cs.

Things to note:

- Used a combination of Linq and raw Sql to interact with the database depending on what made sense for that query
- Added some unique constraints on the Reviews and Restaurants Table (see DatabaseContext.cs ln 36-41)
- Abstracted away EntityFramework from the WebApi through Repository interfaces and classes.  See comments in Startup.cs on ln 42 for more reasoning behind that choice
- Injected Loggers and Repos into controllers using Asp.net Core DI
- Used Nlog for logging errors to 'trace.log' in the bin
- Created translators between EntityFramework Entities and simple DTOs
- Because this project seemed to be more about data storage/retrieval and architecture, I found it difficult to find many places to add unit testing.  I put some simple unit tests in around one of the translators just as an example.
- Attempted to give informative error messages to the API user through custom exception pathways
- Had hoped to add some user Management and Authentication but I ran out of time

