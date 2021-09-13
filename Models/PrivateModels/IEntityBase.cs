namespace Softwriters.RestaurantReviews.Models.PrivateModels
{
    public interface IEntityBase
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
