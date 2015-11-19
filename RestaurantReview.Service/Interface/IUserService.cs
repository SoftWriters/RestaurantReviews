using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Service.Interface
{
    public interface IUserService : IService<BL.Model.User, DAL.Entity.User>
    {
    }
}
