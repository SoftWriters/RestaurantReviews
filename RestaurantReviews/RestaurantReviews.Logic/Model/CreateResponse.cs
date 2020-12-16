using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model
{
    public class CreateResponse : IApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// The id of the resource that was created, or the id of the existing
        /// resource if the status is <see cref="CreateResponseStatus.Duplicate"/>
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The validation errors that have occurred <see cref="CreateResponseStatus.ValidationError"/>
        /// </summary>
        public IEnumerable<ValidationResult> ValidationErrors { get; set; }

        public static CreateResponse Success(string id)
        {
            return new CreateResponse
            {
                Status = CreateResponseStatus.Success.ToString(),
                Message = "Created successfully",
                Id = id
            };
        }

        public static CreateResponse Duplicate(string id)
        {
            return new CreateResponse
            {
                Status = CreateResponseStatus.Duplicate.ToString(),
                Message = "This entity already exists",
                Id = id
            };
        }

        public static CreateResponse Exception(Exception ex)
        {
            return new CreateResponse
            {
                Status = CreateResponseStatus.Failure.ToString(),
                Message = Debugger.IsAttached ? ex.ToString() : "Internal error"
            };
        }

        public static bool HasValidationErrors(object request, out CreateResponse response)
        {
            var results = new List<ValidationResult>();
            response = Success(null);
            if (Validator.TryValidateObject(request, new ValidationContext(request), results, true))
            {
                return false;
            }

            response.Status = CreateResponseStatus.ValidationError.ToString();
            response.Message = "One or more validation errors occurred";
            response.ValidationErrors = results;
            return true;
        }
    }

    public enum CreateResponseStatus
    {
        Unknown,
        Success,
        Duplicate,
        ValidationError,
        Failure
    }
}
