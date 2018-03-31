using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities
{
    /// <summary>
    /// Exposes properties related to a restaurant.
    /// </summary>
    public class Restaurant
    {
        #region fields
        private long _id = -1;
        #endregion

        #region properties
        /// <summary>
        /// Gets the unique identifier for this restaurant instance.
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
        /// Gets or sets the name of this restaurant instance.
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}
