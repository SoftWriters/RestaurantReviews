using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Logic.Model
{
    public interface IApiResponse
    {
        string Status { get; }
        string Message { get; }
        IEnumerable<ValidationResult> ValidationErrors { get; }
    }
}
