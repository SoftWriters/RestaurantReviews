namespace RestaurantReviews.ApiModels.ApiModelsV1
{
    public enum ResultEnum : int
    {
        Success = 0,
        UnexpectedError,
        //Business Logic Layer (BLL) errors:
        NotFound,
        MissingParameter,
        InvalidParameter,
        DuplicateRestaurantAddress
    }
}