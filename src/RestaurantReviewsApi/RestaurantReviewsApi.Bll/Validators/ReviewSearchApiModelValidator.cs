using FluentValidation;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RestaurantReviewsApi.Bll.Validators
{
    public class ReviewSearchApiModelValidator : AbstractValidator<ReviewSearchApiModel>
    {
        public ReviewSearchApiModelValidator()
        {
            RuleFor(x => x).Custom((x, context) =>
             {
                 bool empty = true;
                 empty = x.UserName != null ? false : empty;
                 empty = x.RestaurantId != null ? false : empty;
                 if (empty)
                     context.AddFailure("At least one of the following fields must have a value: UserName, RestaurantId.");
             });

            RuleFor(m => m.UserName).MaximumLength(100)
                .WithMessage("The UserName field can only be up to 100 characters long.");         
        }
    }
}
