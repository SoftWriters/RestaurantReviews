Author: Eric Kepes (eric@kepes.net)

I built this using .Net Core on a Mac. To make it a bit easier, I wrote some bash shell scripts to set things up and run the service:

* startsql.sh - starts up SQL Server in a Docker Container and creates the initial database and user
* stopsql.sh - tears down the SQL Server container
* migrate.sh - runs the database migration scripts, which are embedded in the application via FluentMigrator
* run.sh - just a shortcut to start the app

The migration process deserves a little more discussion. The migration is run if the application is started with the command line parameter "migrate". This is in keeping in line with the principles of a 12-factor app, which I try to follow as much as makes sense.

None of the logins for SQL Server are secure - they are kept in plain text right in the repository. If this were a production app, I would not do it that way, but given there is no real risk, I left them there to facilitate easy running of the service by anyone reviewing it.

If you run the application and browser to the app url http://localhost:5000/swagger, you will be presented with a Swagger UI that you can use to explore the API. This is provided by NSwag.

There is no validation of users. Because it is not specified how to handle user accounts, I keep it simple and ignored the concept. Users are just email addresses.