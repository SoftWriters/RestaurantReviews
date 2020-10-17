namespace RestaurantReviewService.Strategy
{
    public interface IRepositoryStrategy<P,T>
    {
        T Execute(P p);
    }
}
