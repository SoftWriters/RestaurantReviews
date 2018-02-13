using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantReviews.Data.EfLibrary;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Domain.Service;

namespace RestaurantReviewsAPI.Services
{
    public class ServiceFactory
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ServiceFactory()
        {
            _unitOfWorkFactory = new UnitOfWorkFactory("DefaultConnection");
        }

        public RestaurantService RestaurantService
        {
            get
            {
                return new RestaurantService(_unitOfWorkFactory);
            }
        }
    }
}