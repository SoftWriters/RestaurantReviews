This is a MS Windows Communication Foundation (WCF) service library 
to provide an API to other components that serve platforms sharing
information regarding customer reviews of restaurants.

The intent is to capture or store the data into a MS SQL Server database.

It is composed of an interface class defining the service and operation contracts
used to provide the following data:

Get a list of restaurants by city - returns list of restaurants for a specific city
Post a restaurant that is not in the database
Post a review for a restaurant
Get of a list of reviews by user - returns list of all reviews ordered by user
Delete a review