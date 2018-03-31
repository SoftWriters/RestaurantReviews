using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities
{
    /// <summary>
    /// Exposes properties related to a restaurant address.
    /// </summary>
    public class RestaurantAddress
    {
        #region fields
        private long _id = -1;
        private long _restaurantId = -1;
        #endregion

        #region properties
        /// <summary>
        /// Gets the unique identifier for the associated restaurant address.
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
        /// Gets or sets the associated restaurant unique identifier.
        /// </summary>
        public long RestaurantId
        {
            get
            {
                return this._restaurantId;
            }
            set
            {
                this._restaurantId = value;
            }
        }

        /// <summary>
        /// Gets or sets the primary street address of the associated restaurant address.
        /// </summary>
        public string Street1 { get; set; }

        /// <summary>
        /// Gets or sets the secondary street address of the associated restaurant address.
        /// </summary>
        public string Street2 { get; set; }

        /// <summary>
        /// Gets or sets the city of the associated restaurant address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region of the associated restaurant address.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the associated restaurant address.
        /// </summary>
        public string PostalCode { get; set; }
        #endregion
    }
}
