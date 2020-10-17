using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewService.Operations
{
    public interface IRepositoryOperation<T,P>
    {
        T Execute(P p);
    }
}
