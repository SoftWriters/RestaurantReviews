using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsService.ModelBuilders
{
    public interface IModelBuilder<T,P>
    {
        T Build(P p);
    }
}
