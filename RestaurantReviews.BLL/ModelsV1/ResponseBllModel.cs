using RestaurantReviews.ApiModels.ApiModelsV1;

namespace RestaurantReviews.BLL.ModelsV1
{
    public class ResponseBllModel
    {
        public ResultEnum Result { get; set; }
        public string ParameterName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
        public string InnerErrorMessage { get; set; }
        public string InnerErrorStackTrace { get; set; }
    }
}