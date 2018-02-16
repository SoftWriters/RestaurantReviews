using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantReviews.Data.EfLibrary;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Domain.Service;

namespace RestaurantReviewsAPI.Services
{
    public static class ServiceFactory
    {
        private static IUnitOfWorkFactory UnitOfWorkFactory => new UnitOfWorkFactory("DefaultConnection");

        public static RestaurantService RestaurantService(long currentUserId)
        {
            return new RestaurantService(UnitOfWorkFactory, currentUserId);
        }

        public static ReviewService ReviewService(long currentUserId)
        {
            return new ReviewService(UnitOfWorkFactory, currentUserId);
        }

        public static UserService UserService(long currentUserId)
        {
            return new UserService(UnitOfWorkFactory, currentUserId);
        }

        public static AuthenticationService UserAuthenticationService => new AuthenticationService(UnitOfWorkFactory);
    }
}