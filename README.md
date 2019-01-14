RestaurantReviews
=================

The Problem
--------------
We are in the midst of building a mobile application that will let restaurant patrons rate the restaurant in which they are eating. As part of the build, we need to develop a web API that will accept and store the ratings and other sundry data from a publicly accessible interface. 

For this project, we would like you to build this API. Feel free to add your own twists and ideas to what type of data we should collect and return, but at a minimum your API should be able to:

1. Get a list of restaurants by city
2. Post a restaurant that is not in the database
3. Post a review for a restaurant
4. Get of a list of reviews by user
5. Delete a review

The Fine Print
--------------
Please use whatever techniques you feel are applicable to solve the problem. We suggest that you approach this exercise as if this code was part of a larger system. The end result should be representative of your abilities and style.  We prefer that you submit your solution in a language targeting the .NET Framework to help us better evaluate your code.

Please fork this repository. If your solution involves code auto generated by a development  tool, please commit it separately from your own work.  When you have completed your solution, please issue a pull request to notify us that you are ready.

Have fun.

-----------------------------------------------------------------------------------------------
To Run the solution
1. run the Setup.ps1 powershell script to create the schema (requires SQL Server or SQL Express with Integrated Security enabled)
   --If the server is different than local, you will need to modify config.json and change the 'dbServer' value
   --If Integrated Security is not enabled you will need to modify the connection string at the top of Setup.ps1
   --If all else fails, the RestaurantReviews database can be created manually in SMSS, and create scripts (located under scripts\table\) can be opened and run in SMSS
2. Compile the solution in Visual Studio
3. Modify the connection string in Web.config if necessary