﻿using System;
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

        public static RestaurantService RestaurantService => new RestaurantService(UnitOfWorkFactory);
        public static UserAuthenticationService UserAuthenticationService => new UserAuthenticationService(UnitOfWorkFactory);
    }
}