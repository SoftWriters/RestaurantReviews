namespace Softwriters.RestaurantReviews.Models.PrivateModels
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
