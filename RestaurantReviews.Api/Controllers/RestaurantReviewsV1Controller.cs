using RestaurantReviews.Api.ActionResults;
using RestaurantReviews.ApiModels.ApiModelsV1;
using RestaurantReviews.BLL.Managers;
using RestaurantReviews.BLL.ModelsV1;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestaurantReviews.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class RestaurantReviewsV1Controller : ApiController
    {
        [Route("restaurant")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult PostRestaurant([FromBody]CreateRestaurantApiModel createRestaurantApiModel)
        {
            var restaurantReviewsV1Manager = new RestaurantReviewsV1Manager();

            int newRestaurantApiId;
            ResponseBllModel responseBllModel = restaurantReviewsV1Manager.PostRestaurant(createRestaurantApiModel, out newRestaurantApiId);

            if (responseBllModel.Result == ResultEnum.Success)
                return Ok(newRestaurantApiId);
            else
                return GetResponseApiActionResult(responseBllModel);
        }


        [Route("restaurant")]
        [HttpGet]
        [ResponseType(typeof(IList<RestaurantApiModel>))]
        public IHttpActionResult SearchRestaurants(string city = null, string stateProvince = null)
        {
            var restaurantReviewsV1Manager = new RestaurantReviewsV1Manager();

            IList<RestaurantApiModel> restaurantApiModels;
            ResponseBllModel responseBllModel = restaurantReviewsV1Manager.SearchRestaurants(city, stateProvince, out restaurantApiModels);

            if (responseBllModel.Result == ResultEnum.Success)
                return Ok(restaurantApiModels);
            else
                return GetResponseApiActionResult(responseBllModel);
        }


        [Route("restaurantreview")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult PostReview([FromBody]CreateRestaurantReviewApiModel createRestaurantReviewApiModel)
        {
            var restaurantReviewsV1Manager = new RestaurantReviewsV1Manager();

            int newRestaurantReviewApiId;
            ResponseBllModel responseBllModel = restaurantReviewsV1Manager.PostRestaurantReview(createRestaurantReviewApiModel, out newRestaurantReviewApiId);

            if (responseBllModel.Result == ResultEnum.Success)
                return Ok(newRestaurantReviewApiId);
            else
                return GetResponseApiActionResult(responseBllModel);
        }


        [Route("restaurantreview")]
        [HttpGet]
        [ResponseType(typeof(IList<RestaurantReviewApiModel>))]
        public IHttpActionResult SearchRestaurantReviews(string reviewerEmail = null)
        {
            var restaurantReviewsV1Manager = new RestaurantReviewsV1Manager();

            IList<RestaurantReviewApiModel> restaurantReviewApiModels;
            ResponseBllModel responseBllModel = restaurantReviewsV1Manager.SearchRestaurantReviews(reviewerEmail, out restaurantReviewApiModels);

            if (responseBllModel.Result == ResultEnum.Success)
                return Ok(restaurantReviewApiModels);
            else
                return GetResponseApiActionResult(responseBllModel);
        }


        [Route("restaurantreview/{restaurantReviewApiId:int}")]
        [HttpDelete]
        [ResponseType(typeof(bool))]
        public IHttpActionResult DeleteRestaurantReviews(int restaurantReviewApiId)
        {
            var restaurantReviewsV1Manager = new RestaurantReviewsV1Manager();

            bool success;
            ResponseBllModel responseBllModel = restaurantReviewsV1Manager.DeleteRestaurantReview(restaurantReviewApiId, out success);

            if (responseBllModel.Result == ResultEnum.Success)
                return Ok(success);
            else
                return GetResponseApiActionResult(responseBllModel);
        }


        private ResponseApiActionResult GetResponseApiActionResult(ResponseBllModel responseBllModel)
        {
            var responseApiModel = new ResponseApiModel
            {
                Result = responseBllModel.Result,
                ParameterName = responseBllModel.ParameterName,
                ErrorMessage = responseBllModel.ErrorMessage
            };

            return new ResponseApiActionResult(Request, responseApiModel);
        }
    }
}