using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Service.Interface
{
    public interface IRestaurantService : IService<BL.Model.Restaurant, DAL.Entity.Restaurant>
    {
        IEnumerable<BL.Model.Restaurant> GetByCityId(int cityID);
    }
}
