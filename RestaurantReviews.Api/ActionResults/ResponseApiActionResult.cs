using RestaurantReviews.ApiModels.ApiModelsV1;

using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantReviews.Api.ActionResults
{
    public class ResponseApiActionResult : IHttpActionResult
    {
        private readonly HttpRequestMessage HttpRequestMessage;
        private readonly ResponseApiModel ResponseApiModel;

        public ResponseApiActionResult(HttpRequestMessage httpRequestMessage, ResponseApiModel responseApiModel)
        {
            HttpRequestMessage = httpRequestMessage;
            ResponseApiModel = responseApiModel;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent<ResponseApiModel>(ResponseApiModel, new JsonMediaTypeFormatter(), "application/json"),
                RequestMessage = HttpRequestMessage
            };

            switch (ResponseApiModel.Result)
            {
                case ResultEnum.Success:
                {
                    response.StatusCode = HttpStatusCode.OK;
                    break;
                }
                case ResultEnum.UnexpectedError:
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    break;
                }
                //Business Logic Layer (BLL) errors:
                case ResultEnum.NotFound:
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        break;
                    }
                case ResultEnum.MissingParameter:
                case ResultEnum.InvalidParameter:
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    break;
                }
                case ResultEnum.DuplicateRestaurantAddress:
                {
                    response.StatusCode = HttpStatusCode.Conflict;
                    break;
                }
            }

            return Task.FromResult(response);
        }
    }
}