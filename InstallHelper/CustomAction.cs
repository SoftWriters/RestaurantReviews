using System;
using System.IO;
using Microsoft.Deployment.WindowsInstaller;

namespace InstallHelper
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult UpdateDomainInConfigs(Session session)
        {
            session.Log("Begin UpdateDomainInConfigs");

            try
            {
                // need to pass the data to a deferred custom action as a single property
                // so here we are using an '*' to delimit the data
                string data = session.CustomActionData["DOMAINCONFIGDATA"];
                var items = data.Split('*');
                var installFolder = items[0];

                var domain = items[1];
                var serviceConfig = Path.Combine(installFolder, "Service", "SoftWriters.RestaurantReviews.Service.exe.config");
                var clientConfig = Path.Combine(installFolder, "TestClient", "ClientConsole.exe.config");

                UpdateAppConfigWithDomain(serviceConfig, domain);
                UpdateAppConfigWithDomain(clientConfig, domain);
            }
            catch (Exception e)
            {
                session.Log("Error updating app.config files with domain: " + e.Message);
                return ActionResult.Failure;
            }

            return ActionResult.Success;
        }
        
        private static void UpdateAppConfigWithDomain(string filename, string domain)
        {
            var contents = File.ReadAllText(filename);
            contents = contents.Replace("localhost", domain);
            File.WriteAllText(filename, contents);
        }
    }
}
