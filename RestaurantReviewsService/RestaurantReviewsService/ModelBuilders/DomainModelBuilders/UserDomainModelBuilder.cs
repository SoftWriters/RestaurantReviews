using RestaurantReviewService.Entities;
using RestaurantReviewsService.DomainModels;

namespace RestaurantReviewsService.ModelBuilders.DomainModelBuilders
{
    public sealed class UserDomainModelBuilder : IModelBuilder<UserDM, User>
    {
        UserDM IModelBuilder<UserDM, User>.Build(User p)
        {
            return new UserDM
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Alias = p.Alias,
                City = p.City,
                State = p.State,
                StateIdRef = p.StateIdRef
            };
        }
    }
}