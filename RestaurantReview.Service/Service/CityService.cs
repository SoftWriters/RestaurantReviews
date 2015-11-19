using RestaurantReview.BL.Base;
using RestaurantReview.Common;
using RestaurantReview.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.BL;
using RestaurantReview.Service.Interface;
using RestaurantReview.Service.Base;

namespace RestaurantReview.Service
{
    public class CityService : ServiceBase<BL.Model.City, DAL.Entity.City>, ICityService
    { 
        public CityService(IRRContext context)
            : base(context)
        {
        }

        public CityService()
            : base()
        {
        }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public new int Add(BL.Model.City city, int userID)
        {
            if (city == null)
            {
                throw new ArgumentNullException("city");
            }

            int ret = _context.CityAdd(city.CityName, city.State);

            if (ret < 0)
            {
                throw new Exception("Error adding record to database");
            }

            return ret;
        }
    }
}