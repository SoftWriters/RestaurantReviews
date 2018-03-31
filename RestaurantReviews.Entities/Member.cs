namespace RestaurantReviews.Entities
{
    /// <summary>
    /// Exposes properties related to a member.
    /// </summary>
    public class Member
    {
        #region fields
        private long _id = -1;
        #endregion

        #region properties
        /// <summary>
        /// Gets the unique identifier for the associated member.
        /// </summary>
        public long Id {
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
        /// Gets or sets the user name of the associated member.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name of this member.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of this member.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of this member.
        /// </summary>
        public string Email { get; set; }
        #endregion
    }
}
