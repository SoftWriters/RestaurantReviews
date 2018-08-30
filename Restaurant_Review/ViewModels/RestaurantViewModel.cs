using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Review.ViewModels
{
    public class RestaurantViewModel
    {
        [ScaffoldColumn(false)]
        public int IDRestaurant { get; set; }
        [DisplayName("Name")]
        public string RestaurantName { get; set; }
        [DisplayName("Description")]
        public string RestaurantDescription { get; set; }
        [ScaffoldColumn(false)]
        public bool IsActive { get; set; }
        [ScaffoldColumn(false)]
        public bool Deleted { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }
        [ScaffoldColumn(false)]
        public string IDUserCreated { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateUpdated { get; set; }
        [ScaffoldColumn(false)]
        public string IDUserUpdated { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        [DisplayName("State Code")]
        public string StateCode { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
    }
   
}