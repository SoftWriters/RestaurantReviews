using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviewService.Entities;
using System.Data.SQLite;

namespace RestaurantReviewService.Abstract
{
    public interface ISqlLiteDbRepository : IDbRepository<EntityModelBase, object> { }
    
}
