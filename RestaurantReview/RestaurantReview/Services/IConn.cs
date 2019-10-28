namespace RestaurantReview.Services
{
    public interface IConn
    {
        string connstring();

        string AWSconnstring();
    }
}