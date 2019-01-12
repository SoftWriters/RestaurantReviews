using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Web.Api.App_Start
{
    public interface IConfigurationProvider
    {
        string ConnnectionString { get; set; }
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        public string ConnnectionString { get; set; }
        public ConfigurationProvider()
        {
            ConnnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RestaurantReviews"].ConnectionString;
        }


    }
}