/******************************************************************************
 * Name: APIServiceBase.cs
 * Purpose: Abstract base class for all API Service classes
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace RestaurantReviews.API.Service
{
    public abstract class APIServiceBase
    {
        protected void ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            if (validationResults.Count > 0) throw new ValidationException(validationResults.First().ErrorMessage);
        }
    }
}
