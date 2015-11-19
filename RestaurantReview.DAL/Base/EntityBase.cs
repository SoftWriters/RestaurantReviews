using RestaurantReview.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.DAL.Base
{
    public abstract class EntityBase
    {
        public abstract class Entity<T> : EntityBase, IEntityBase<T>
        {
            public virtual int Id { get; set; }
        }
    }
}
