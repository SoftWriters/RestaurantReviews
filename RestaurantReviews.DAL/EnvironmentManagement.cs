using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace RestaurantReviews.DAL
{
    public static class EnvironmentManagement
    {
        public static string GetConnectionString()
        {
            switch (ConfigurationManager.AppSettings["Environment"])
            {
                case "Development":
                    return(ConfigurationManager.ConnectionStrings["DevelopmentConnection"].ConnectionString);
                    break;
                case "Test":
                    return (ConfigurationManager.ConnectionStrings["TestConnection"].ConnectionString);
                    break;
                case "Production":
                    return (ConfigurationManager.ConnectionStrings["ProductionConnection"].ConnectionString);
                    break;
            }
        }
    }
}
