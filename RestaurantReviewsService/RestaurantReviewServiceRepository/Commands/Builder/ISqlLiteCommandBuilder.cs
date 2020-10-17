using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewService.Commands.Builder
{
    public interface ISqlLiteCommandBuilder<T>
    {
        T Build();
    }
}
