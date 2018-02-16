using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace RestaurantReviewsAPI.Authorization
{
    public static class IIdentityExtensions
    {
        public static long? GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;

            Claim claim = claimsIdentity.FindFirst(ClaimTypes.Sid);

            if (claim == null)
                return null;

            long id;

            if (!long.TryParse(claim.Value, out id))
                return null;

            return id;
        }
    }
}