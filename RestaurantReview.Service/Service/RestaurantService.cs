using RestaurantReview.BL.Base;
using RestaurantReview.Common;
using RestaurantReview.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.Service.Interface;
using RestaurantReview.Service.Base;

namespace RestaurantReview.Service
{
    public class RestaurantService : ServiceBase<BL.Model.Restaurant, DAL.Entity.Restaurant>, IRestaurantService
    { 
        public RestaurantService(IRRContext context)
            : base(context)
        {
        }

        public RestaurantService()
            : base()
        {
        }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public IEnumerable<BL.Model.Restaurant> GetByCityId(int cityID)
        {
           IEnumerable<DAL.Entity.Restaurant> restList = _context.RestaurantsGetByCity(cityID);

           IEnumerable<BL.Model.Restaurant> ret = Convert(restList);

           return ret;
        }  
    }
}