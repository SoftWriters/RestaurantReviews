using System.Collections.Generic;
using RestaurantReviews.Data.Models.Validation;
using RestaurantReviews.Domain.Codes;

namespace RestaurantReviews.Domain.Models
{
    public class OperationResponse
    {
        public OpCodes OpCode { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
