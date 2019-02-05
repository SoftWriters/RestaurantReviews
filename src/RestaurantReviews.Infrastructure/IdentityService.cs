using System;
using RestaurantReviews.Domain;
using Microsoft.AspNetCore.Http;

namespace RestaurantReviews.Infrastructure
{

    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Guid GetUserIdentity()
        {
            return new Guid(_context.HttpContext.User.FindFirst("sub").Value);
        }
    }
}
