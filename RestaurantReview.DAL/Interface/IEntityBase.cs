using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.DAL.Interface
{
   public interface IEntityBase<T>
    {
        int Id { get; set; }
    }
}
