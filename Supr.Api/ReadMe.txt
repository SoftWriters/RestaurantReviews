

I didn't get carried away with extending the base requirements beyond what you had listed. Instead I attempted to show
a full understanding of the development stack, WebAPI, authorization, isolation, separation of concerns, DRY, etc.

Projects are set up for IISExpress on port 8500.


Supr.Api		-	The core Api project; V1Controller is for Version 1. I left the OWIN OAuth components in place, along 
					with the Account controller so you can use the default uri's to create users

Supr.DB		-	Database project for my 2 additional tables; Restaurant and Review.

Supr.Model	-	Class library for application objects and lightweight repository.

Supr.Test	-	Test project with a few representative tests against the model and the api.



I hope to have represented enough proficiency to make it to the next step. Enjoy.

