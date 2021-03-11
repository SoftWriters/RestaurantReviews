ORGANIZATION *********************************************
SoftWriters.RestaurantReviews.WebApi
- This is the actual api and implementation

SoftWriters.RestarantReviews.DataLibrary
- This contains my data objects as well as my implementation for my disk based data stores.

SoftWriters.RestaurantReviews.Service
- Service to host the web api

Test
- Directory that contains the following:
	- ServiceConsole: Service console, similar to my service, but runs in a console for ease of testing
	- ClientConsole: A console app with a client implementation. Good for testing a client, similar to what would
	be used in a mobile app.
	- SoftWriters.RestaurantReviews.WebApi.Tests: My api unit tests
	- CreateTestCerts.ps1: A powershell script to create self-signed test certs, needed for TLS\SSL configuration

Installer
- Contains a project for the msi and a custom action that updates the app.config files with the appropriate domain (passed via command line along with install folder)
- To run installer: msiexec /i RestaurantReviewsServerMsi.msi DOMAIN=<yourDomain> InstallerFolder=C:\some\Path /lvoicewarmupx installerlog.txt

SETUP *********************************************
- In order to run the test console apps, you need to Run As Administrator
- If using TLS\SSL, you need to create the certificates, and the certificate must have the host name as the service
- In the console client, just comment/uncomment the appropriate lines to toggle between http and https
- NOTE: In the configs, I only have https endpoints commented out, because I wanted a working solution with an installer,
and so I didn't want to have to worry about certificates. But I tested with https endpoints and it should be all good, if running the ServiceConsole

DEPENDENCIES ******************************************
- You will need to restore nuget packages to get the MSTest packages
- For the installer, I have WiX v3.11 installed, and so if you don't have that installed, or the WiX VS extension 
just unload the projects under Installer and forget about them

AREAS OF IMPROVEMENT *********************************************
- I tried to make my api agnostic to the data source, and rather than connect to a database, for my testing, I created
a disk based data store. However, there could probably be  improvements. My datastore isn't very efficient. Also, if
connecting to a proper database, my api could be tweaked a little, such as using ints for ids rather than guids.

- My app doesn't really have a concept of a logged in user. Some calls I pass in a user id, and some I make an assumption
about who the user is.

- I created unit tests for my api, but not my xml data stores. As I said, if I were going via a database, I wouldn't be using 
these data stores. But, if I were going with the xml data stores in the long run, then I should probably have some unit tests
created for those as well.

- Once I got my API working, I thought I would be fancy and include and installer project, complete with install bootstrapper. I didn't create the installer UI
because that would take a lot of time to do right and I wouldn't want to do it if it wasn't right. But if you're going to have the installer, it would be good 
to have a bootstrapper that defaults the domain to that of the target machine.

- I targeted .NET 4.6.1, because I worked on a web api project recently that also targeted that .NET version, and so was most comfortable with that version. 
I believe I had read that in more recent .NET versions, they handle certain things a little differently. I can't remember what that is, but I would assume that
if targeting a recent version of .NET Framework, one would have to do at least some minimal rework to get everything running properly.