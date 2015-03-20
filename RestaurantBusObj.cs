using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReview
{
    /// <summary>
    /// A placeholder business object that would depend upon the data access framework in use.
    /// </summary>
    public class RestaurantBusObj
    {
        public Int32 RestaurantId { get; set; }
        public String Name { get; set; }
        public AddressBusObj Address { get; set; }
        public String PhoneNumber { get; set; }
    }
}
