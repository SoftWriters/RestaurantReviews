using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities
{
    /// <summary>
    /// Exposes properties related to a review.
    /// </summary>
    public class Review
    {
        #region fields
        private long _id = -1;
        private long _memberid = -1;
        private long _restaurantid = -1;
        #endregion

        #region properties
        /// <summary>
        /// Gets the unique identifier for this review instance.
        /// </summary>
        public long Id
        {
            get
            {
                return this._id;
            }
            internal set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// Gets or sets the member id associated with this review.
        /// </summary>
        public long MemberId
        {
            get
            {
                return this._memberid;
            }
            internal set
            {
                this._memberid = value;
            }
        }

        /// <summary>
        /// Gets or sets the restaurant id associated with this review.
        /// </summary>
        public long RestaurantId
        {
            get
            {
                return this._restaurantid;
            }
            internal set
            {
                this._restaurantid = value;
            }
        }

        /// <summary>
        /// Gets or sets the review content.
        /// </summary>
        public string Body { get; set; }
        #endregion
    }
}
