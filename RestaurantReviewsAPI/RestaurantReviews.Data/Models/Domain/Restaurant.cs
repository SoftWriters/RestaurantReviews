using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestaurantReviews.Data.Models.Validation;

namespace RestaurantReviews.Data.Models.Domain
{
    public class Restaurant
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string StateCode { get; set; }

        public List<ValidationError> Validate()
        {
            var validationErrors = new List<ValidationError>();

            if(string.IsNullOrWhiteSpace(Name))
                validationErrors.Add(new ValidationError
                {
                    Reference = "Name",
                    Message = "Name is required."
                });

            if(string.IsNullOrWhiteSpace(City))
                validationErrors.Add(new ValidationError
                {
                    Reference = "City",
                    Message = "City is required."
                });

            if(string.IsNullOrWhiteSpace(StateCode))
                validationErrors.Add(new ValidationError
                {
                    Reference = "StateCode",
                    Message = "StateCode is required."
                });

            return validationErrors;
        }
    }
}
