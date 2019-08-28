using FluentValidation;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;

namespace RestaurantReviews.Business.Validators
{
    public class ReviewModelValidator : FluentModelValidator<IReview>
    {
        public const string ContentRequiredErrorMessage = "Content is required";

        public ReviewModelValidator(IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            Validator.RuleFor(p => p.Content)
                .NotEmpty()
                .WithMessage(ContentRequiredErrorMessage);

            Validator.RuleFor(p => p.RestaurantId)
                .MustBeAnExistingRestaurantId<IReview, long>(restaurantRepository);

            Validator.RuleFor(p => p.UserId)
                .MustBeAnExistingUserId<IReview, long>(userRepository);
        }
    }
}
