using FluentValidation;
using RestaurantReviews.Interfaces.Models;

namespace RestaurantReviews.Business.Validators
{
    public class RestaurantModelValidator : FluentModelValidator<IRestaurant>
    {
        public const string CityRequiredErrorMessage = "City is required";

        public RestaurantModelValidator()
        {
            Validator.RuleFor(p => p.City)
                .NotEmpty()
                .WithMessage(CityRequiredErrorMessage);
        }
    }
}
