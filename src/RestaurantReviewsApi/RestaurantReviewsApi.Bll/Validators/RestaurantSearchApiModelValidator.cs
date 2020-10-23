using FluentValidation;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RestaurantReviewsApi.Bll.Validators
{
    public class RestaurantSearchApiModelValidator : AbstractValidator<RestaurantSearchApiModel>
    {
        public RestaurantSearchApiModelValidator()
        {
            RuleFor(x => x).Custom((x, context) =>
             {
                 bool empty = true;
                 empty = x.Name != null ? false : empty;
                 empty = x.City != null ? false : empty;
                 empty = x.State != null ? false : empty;
                 empty = x.ZipCode != null ? false : empty;
                 if (empty)
                     context.AddFailure("At least one of the following fields must have a value: Name, AddressLine1, City, State, ZipCode.");
             });

            RuleFor(m => m.Name).MaximumLength(100)
                .WithMessage("The Name field can only be up to 100 characters long.");

            RuleFor(m => m.City).MaximumLength(100)
                .WithMessage("The City field can only be up to 100 characters long.");

            RuleFor(m => m.State).Must(ValidationHelper.ValidState)
                .When(m => !string.IsNullOrEmpty(m.State))
                .WithMessage("The State field be a valid value.");

            RuleFor(m => m.ZipCode).Must(ValidationHelper.ValidZipCode)
                .When(m => !string.IsNullOrEmpty(m.ZipCode))
                .WithMessage("The ZipCode field be a valid value.");
        }
    }
}
