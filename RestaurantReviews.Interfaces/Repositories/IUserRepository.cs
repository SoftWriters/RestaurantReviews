using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Interfaces.Repository
{
    public interface IUserRepository
    {
        ICollection<IUser> GetAll();
        IUser GetById(long id);
        long Create(IUser user);
    }
}
