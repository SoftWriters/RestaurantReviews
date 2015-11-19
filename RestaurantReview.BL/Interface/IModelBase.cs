using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.BL.Interface
{
   public interface IModelBase<T>
    {
        int Id { get; set; }
    }
}
