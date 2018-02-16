using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Routing;
using RestaurantReviews.Data.Models;
using RestaurantReviewsAPI.Services;

namespace RestaurantReviewsAPI.Authorization
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var authService = ServiceFactory.UserAuthenticationService;

            var authenticatedUser = await authService
                    .AuthenticateUser(userName, password);

            if (authenticatedUser == null)
            {
                // No user with userName/password exists.
                return null;
            }

            // Create a ClaimsIdentity with all the claims for this user.
            Claim nameClaim = new Claim(ClaimTypes.Name, userName);
            Claim idClaim = new Claim(ClaimTypes.Sid, authenticatedUser.Id.ToString());
            List<Claim> claims = new List<Claim> { nameClaim, idClaim };

            // important to set the identity this way, otherwise IsAuthenticated will be false
            // see: http://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Basic");
            
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

    }
}