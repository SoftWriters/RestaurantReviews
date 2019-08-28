using RestaurantReviews.Interfaces.Models;
using System.Collections.Generic;

namespace RestaurantUsers.Interfaces.Business
{
    public interface IUserManager
    {
        ICollection<IUser> GetAll();
        IUser GetById(long id);
        void Create(IUser user);
    }
}
