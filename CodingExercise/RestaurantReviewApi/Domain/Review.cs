using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
