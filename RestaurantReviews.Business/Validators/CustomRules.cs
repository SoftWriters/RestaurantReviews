using FluentValidation;
using RestaurantReviews.Interfaces.Repositories;

namespace RestaurantReviews.Business.Validators
{
    internal static class CustomRules
    {
        public const string InvalidRestaurantIdErrorMessage = "RestaurantId must be associated with an existing restaurant";
        public const string InvalidUserIdErrorMessage = "UserId must be associated with an existing user";

        internal static IRuleBuilderOptions<T, long> MustBeAnExistingRestaurantId<T, TElement>(
            this IRuleBuilder<T, long> ruleBuilder, 
            IRestaurantRepository restaurantRepository)
        {
            return ruleBuilder
                .Must(restaurantId => restaurantRepository.GetById(restaurantId) != null)
                .WithMessage(InvalidRestaurantIdErrorMessage);
        }

        internal static IRuleBuilderOptions<T, long> MustBeAnExistingUserId<T, TElement>(
            this IRuleBuilder<T, long> ruleBuilder,
            IUserRepository userRepository)
        {
            return ruleBuilder
                .Must(userId => userRepository.GetById(userId) != null)
                .WithMessage(InvalidUserIdErrorMessage);
        }
    }
}