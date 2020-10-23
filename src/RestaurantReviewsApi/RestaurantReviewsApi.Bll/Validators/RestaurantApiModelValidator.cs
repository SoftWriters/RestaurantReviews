using FluentValidation;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Validators
{
    public class RestaurantApiModelValidator : AbstractValidator<RestaurantApiModel>
    {
        public RestaurantApiModelValidator()
        {
            //At the bare minimum lets have Name, City, and State be required.
            RuleFor(m => m.Name).NotEmpty()
                .DependentRules(() => RuleFor(m => m.Name)
                .MaximumLength(100)
                .WithMessage("The Name field can only be up to 100 characters long."))
            .WithMessage("Name is required.");

            RuleFor(m => m.AddressLine1).MaximumLength(100)
                .WithMessage("The AddressLine1 field can only be up to 100 characters long.");

            RuleFor(m => m.AddressLine2).MaximumLength(100)
                .WithMessage("The AddressLine2 field can only be up to 100 characters long.");

            RuleFor(m => m.City).NotEmpty()
                .DependentRules(() => RuleFor(m => m.City)
                .MaximumLength(100)
                .WithMessage("The City field can only be up to 100 characters long."))
            .WithMessage("City is required.");

            RuleFor(m => m.State).NotEmpty()
                .DependentRules(() => RuleFor(m => m.State)
                .Must(ValidationHelper.ValidState)
                .WithMessage("The State field be a valid value."))
            .WithMessage("State is required.");

            RuleFor(m => m.ZipCode).Must(ValidationHelper.ValidZipCode)
                .When(m => !string.IsNullOrEmpty(m.ZipCode))
                .WithMessage("The ZipCode field be a valid value.");

            RuleFor(m => m.Phone).Must(ValidationHelper.ValidPhoneNumber)
                .When(m => !string.IsNullOrEmpty(m.Phone))
                .WithMessage("The Phone field be a valid value.");

            //TODO Validate Website Format
            RuleFor(m => m.Website).MaximumLength(100)
                .WithMessage("The Website field can only be up to 100 characters long.");

            RuleFor(m => m.Email).EmailAddress()
                .When(m => !string.IsNullOrEmpty(m.Email))
            .WithMessage("The Email field be a valid value.");

            RuleFor(m => m.Description).MaximumLength(500)
                .WithMessage("The Description field can only be up to 500 characters long.");
        }
    }
}
