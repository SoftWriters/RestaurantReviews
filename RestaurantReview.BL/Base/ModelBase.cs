using RestaurantReview.BL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.BL.Base
{
    public abstract class ModelBase
    {
        public abstract class Model<T> : ModelBase, IModelBase<T>
        {
            public virtual int Id { get; set; }
        }
    }
}
