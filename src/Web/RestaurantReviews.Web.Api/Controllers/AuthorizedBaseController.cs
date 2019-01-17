using RestaurantReviews.Web.Api.ExceptionHandling;
using RestaurantReviews.Web.Api.Security;
using RestaurantReviews.Web.Api.Validation;
using System.Web.Http;

namespace RestaurantReviews.Web.Api.Controllers
{
    [SimpleBearerTokenAuthFilter]
    [ModelValidationFilter]
    [DuplicateKeyExceptionFilter]
    public class AuthorizedBaseController : ApiController
    {
    }
}
