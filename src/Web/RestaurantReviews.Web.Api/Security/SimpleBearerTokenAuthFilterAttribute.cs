using RestaurantReviews.Domain;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;


namespace RestaurantReviews.Web.Api.Security
{
    /// <summary>
    /// this class should never be used in production!!!
    /// it serves as an authentication mechanism very poorly but roughly approximates what happens with various bearer authentication
    /// models such as JWT.  For simplicity in using/testing this API project, I have done away with the nearly all of it
    /// including the header, claims, signature, and base64 encoding
    /// it simply takes the auth parameter at face value as the userid of the acting user
    /// for example, to mimic an authenticated and authorized user, simply pass Bearer [userId] in the Authentication header
    /// sample header: Authentication: Bearer 1
    /// </summary>
    public class SimpleBearerTokenAuthFilterAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 1. Look for Authorization header in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Authorization header", request);
                return;
            }

            //auth scheme must be Bearer
            if (authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Authorization scheme must be Bearer ex. \"Authorization: Bearer <userId>\"", request);
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials, header must be of form \"Authorization: Bearer <userId>\"", request);
                return;
            }

            var userId = ExtractUserId(authorization.Parameter);
            if (userId == 0)
            {
                context.ErrorResult = new AuthenticationFailureResult("Credentials are not an integer, header must be of form \"Authorization: Bearer <userId>\"", request);
            }

            var principal = await AuthenticateAsync(context, userId, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        private int ExtractUserId(string parameter)
        {
            int.TryParse(parameter, out int id);
            return id;
        }
        
        //compare the passed userid to users in the db, if found then return a generic principal for the user
        private async Task<IPrincipal> AuthenticateAsync(HttpAuthenticationContext context, int userId, CancellationToken token)
        {
            var resolver = context.ActionContext.RequestContext.Configuration.DependencyResolver;
            var userRepository = (IUserRepository)resolver.GetService(typeof(IUserRepository));
            if (await userRepository.UserExistsAsync(userId))
            {
                var identity = new GenericIdentity(userId.ToString());
                return new GenericPrincipal(identity, null);
            }
            else { return null; }
        }
    }
}