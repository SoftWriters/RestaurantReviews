using RestaurantReviews.Data;
using RestaurantReviews.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Service
{
    public class UserService : IUserService
    {
        private readonly Data.Services.UserServicecs _UserServicecs;

        public UserService()
        {
            _UserServicecs = new Data.Services.UserServicecs();
        }

        public User GetByID(Guid id)
        {
            return _UserServicecs.GetByID(id);
        }

        public List<User> GetAll()
        {
            return _UserServicecs.GetAll();
        }

        public void Delete(Guid id)
        {
            _UserServicecs.Delete(id);
        }

        public void Save(User t)
        {
            _UserServicecs.Save(t);
        }
    }
}