using FluentValidation;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Validators
{
    public class ReviewApiModelValidator : AbstractValidator<ReviewApiModel>
    {
        public ReviewApiModelValidator()
        {
            RuleFor(m => m.RestaurantId).NotNull()
                .WithMessage("RestaurantId is required.");

            RuleFor(m => m.UserName).NotEmpty()
                .DependentRules(() => RuleFor(m => m.UserName)
                .MaximumLength(100)
                .WithMessage("The UserName field can only be up to 100 characters long."))
            .WithMessage("UserName is required.");

            RuleFor(m => m.Rating).NotNull()
                .DependentRules(() => RuleFor(m => m.Rating)
                .InclusiveBetween(1, 10)
                .WithMessage("The Rating field must be between 1 and 10."))
            .WithMessage("Rating is required.");

            RuleFor(m => m.Details).MaximumLength(4000)
                .WithMessage("The Details field can only be up to 4000 characters long.");
        }
    }
}
