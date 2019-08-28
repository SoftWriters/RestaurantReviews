using FluentValidation;
using RestaurantReviews.Interfaces.Business;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Business.Validators
{
    public abstract class FluentModelValidator<T> : IModelValidator<T>
    {
        protected readonly AbstractValidator<T> Validator;

        protected FluentModelValidator()
        {
            Validator = new InlineValidator<T>();
        }

        public ICollection<string> Validate(T item)
        {
            var validationResults = Validator.Validate(item);
            var errors = validationResults.Errors.Select(p => $"{p.PropertyName}: {p.ErrorMessage}").ToArray();
            return errors;
        }
    }
}
