using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Service.Interface
{
    public interface IReviewService : IService<BL.Model.Review, DAL.Entity.Review>
    {
        IEnumerable<BL.Model.Review> GetByUserId(int userID);
    }
}
