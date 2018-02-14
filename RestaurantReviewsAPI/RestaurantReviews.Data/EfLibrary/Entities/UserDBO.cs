using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.EfLibrary.Entities
{
    [Table("Users")]
    public class UserDBO
    {
        public long Id { get; set; }
        [MaxLength(250)]
        [Index("IX_Username", IsUnique = true)]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
