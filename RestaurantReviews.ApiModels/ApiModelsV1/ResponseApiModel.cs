namespace RestaurantReviews.ApiModels.ApiModelsV1
{
    public class ResponseApiModel
    {
        public ResultEnum Result { get; set; }
        public string ParameterName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
