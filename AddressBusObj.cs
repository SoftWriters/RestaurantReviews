using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReview
{
    /// <summary>
    /// A placeholder business object that would depend upon the data access framework in use.
    /// </summary>
    public class AddressBusObj
    {
        public Int32 AddressId { get; set;}
        public String LocationName { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
    }
}
