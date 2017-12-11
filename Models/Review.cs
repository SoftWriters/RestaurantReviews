using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RestaurantReviews.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReviewId {get; set;}
        public string UserName {get; set;}
        public int Rating {get; set;}
        public string Description {get; set;}
        public virtual long RestaurantId {get; set;}

        [JsonIgnore] 
        public virtual Restaurant Restaurant {get; set;}
    }
}