using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReviews.Data.QueryBuilder;
using RestaurantReviews.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviews.Data
{
    public static class Startup
    {
        /// <summary>
        /// Adds services necessary to use EFCore as the persistance layer
        /// </summary>
        /// <param name="services"></param>
        public static void UseRestaurantReviewsEFCore(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RestaurantContext>(p =>
            {
                p.UseSqlServer(connectionString);
            });
            services.AddTransient<IRestaurantLogic, RestaurantLogicEF>();
            services.AddTransient<IRestaurantQueryBuilder, RestaurantQueryBuilder>();
            services.AddTransient<IReviewQueryBuilder, ReviewQueryBuilder>();
            services.AddTransient<IUserQueryBuilder, UserQueryBuilder>();
        }
    }
}
