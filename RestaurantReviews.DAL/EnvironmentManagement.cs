using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace RestaurantReviews.DAL
{
    public static class EnvironmentManagement
    {
        private ConfigurationManager configuration;
        public static string GetConnectionString()
        {
            switch (configuration["Emvironment"])
            {
            }
        }
    }
}
