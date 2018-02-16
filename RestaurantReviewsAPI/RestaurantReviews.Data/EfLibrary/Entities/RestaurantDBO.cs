using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.EfLibrary.Entities
{
    [Table("Restaurants")]
    public class RestaurantDBO
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        public virtual StateDBO State { get; set; }
    }
}
