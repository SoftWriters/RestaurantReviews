using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.API.Models
{
    /// <summary>
    /// A proxy model for RestaurantReviews.Entities.Review
    /// </summary>
    public class ReviewModel
    {
        /// <summary>
        /// The ID of the Review
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The ID of the Member who created the Review
        /// </summary>
        public long MemberId { get; set; }

        /// <summary>
        /// The ID of the Restaurant this review corresponds to.
        /// </summary>
        public long RestaurantId { get; set; }

        /// <summary>
        /// The review content.
        /// </summary>
        public string Body{ get; set; }
    }
}