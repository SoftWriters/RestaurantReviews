//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantReviews.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Restaurant
    {
        public Restaurant()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string Name { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
