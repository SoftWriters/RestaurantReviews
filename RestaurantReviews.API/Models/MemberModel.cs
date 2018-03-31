using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.API.Models
{
    /// <summary>
    /// A proxy model for RestaurantReviews.Entities.Member
    /// </summary>
    public class MemberModel
    {
        /// <summary>
        /// The ID of the Member instance.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The user name of the Member.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The first name of the Member.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the Member.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email of the Member.
        /// </summary>
        public string Email { get; set; }
    }
}