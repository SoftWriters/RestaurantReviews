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
    public class UserService : ServiceBase<BL.Model.User, DAL.Entity.User>, IUserService
    { 
        public UserService(IRRContext context)
            : base(context)
        {
        }

        public UserService()
            : base()
        {
        }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }
    }
}