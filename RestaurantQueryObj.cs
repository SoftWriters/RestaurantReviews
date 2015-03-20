using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerReview
{
    /// <summary>
    /// A placeholder business query object that would depend upon the data access framework in use.
    /// </summary>
    public class RestaurantQueryObj
    {
        private Int32 _restaurantId = Int32.MinValue;
        private String _name = String.Empty;
        private AddressBusObj _address = null;
        private String _phoneNumber = String.Empty;

        #region [-- PROPERTIES --]

        public Int32 RestaurantId
        {
            get
            {
                return _restaurantId;
            }
            set
            {
                _restaurantId = value;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public AddressBusObj Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public String PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }

        #endregion [-- PROPERTIES --]

        #region [-- PUBLIC METHODS --]

        public RestaurantCollection LoadCollection()
        {
            RestaurantCollection restaurants = new RestaurantCollection();

            //load the collection based on the provided properties in the query object using existing data access methods
            //for testing, will load manually
            RestaurantBusObj restaurant = new RestaurantBusObj();
            restaurant.Name = "Corner Diner";
            restaurant.RestaurantId = 1;
            restaurant.Address = new AddressBusObj();
            restaurant.Address.AddressId = 2;
            restaurant.Address.AddressLine1 = "100 Main Street";
            restaurant.Address.City = "Pittsburgh";
            restaurant.Address.State = "PA";
            restaurant.Address.ZipCode = "15228";
            restaurant.PhoneNumber = "4125551111";

            restaurants.Add(restaurant);
            return restaurants;
        }

        #endregion [-- PUBLIC METHODS --]
    }
}
