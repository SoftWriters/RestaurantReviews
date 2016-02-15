using RestaurantReviews.Data;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Service.Interfaces
{
    public interface IUserService
    {
        User GetByID(Guid id);
        List<User> GetAll();
        void Delete(Guid id);
        void Save(User t);
    }
}