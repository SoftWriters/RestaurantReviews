using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Review.ViewModels
{
    public class ReviewViewModel
    {
        [ScaffoldColumn(false)]
        public int IDReview { get; set; }
        [DisplayName("ID Restaurant")]
        public int IDRestaurant { get; set; }
        [DisplayName("Restaurant Name")]
        public string RestaurantName { get; set; }
        [ScaffoldColumn(false)]
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
        [ScaffoldColumn(false)]
        public string Line1 { get; set; }
        [ScaffoldColumn(false)]
        public string Line2 { get; set; }
        [ScaffoldColumn(false)]
        public string Line3 { get; set; }
        public string City { get; set; }
        [ScaffoldColumn(false)]
        public string StateCode { get; set; }
        [ScaffoldColumn(false)]
        public string ZipCode { get; set; }
        [DisplayName("Review Text")]
        public string ReviewText { get; set; }
        [ScaffoldColumn(false)]
        public string IDUser { get; set; }
        public string UserName { get; set; }
     
    }
}