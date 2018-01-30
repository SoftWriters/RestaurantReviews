using RestaurantReview.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.BusinessLogic.Models
{
    public class Review : ReviewContext
    {
        public Review (ReviewContext context)
        {
            id = context.id;
            restaurantID = context.restaurantID;
            rating = context.rating;
            comments = context.comments;
            userName = context.userName;
        }
    }
}
