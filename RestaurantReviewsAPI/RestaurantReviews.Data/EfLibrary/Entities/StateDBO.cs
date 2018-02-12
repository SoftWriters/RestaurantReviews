using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.EfLibrary.Entities
{
    [Table("States")]
    public class StateDBO
    {
        public long Id { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
