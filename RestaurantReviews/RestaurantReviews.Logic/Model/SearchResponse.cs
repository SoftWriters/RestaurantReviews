using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Logic.Model
{
    public class SearchResponse<T> : IApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// The validation errors that have occurred <see cref="CreateResponseStatus.ValidationError"/>
        /// </summary>
        public IEnumerable<ValidationResult> ValidationErrors { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

    public static class SearchResponse
    {
        public static SearchResponse<T> Success<T>(IEnumerable<T> results)
        {
            return new SearchResponse<T>
            {
                Status = SearchResponseStatus.Success.ToString(),
                Message = "Success",
                Results = results
            };
        }

        public static SearchResponse<T> Exception<T>(Exception ex)
        {
            return new SearchResponse<T>
            {
                Status = CreateResponseStatus.Failure.ToString(),
                Message = Debugger.IsAttached ? ex.ToString() : "Internal error"
            };
        }

        public static bool HasValidationErrors<T>(object request, out SearchResponse<T> response)
        {
            var results = new List<ValidationResult>();
            response = Success(Enumerable.Empty<T>());
            if (Validator.TryValidateObject(request, new ValidationContext(request), results))
            {
                return false;
            }

            response.Status = CreateResponseStatus.ValidationError.ToString();
            response.Message = "One or more validation errors occurred";
            response.ValidationErrors = results;
            return true;
        }
    }

    public enum SearchResponseStatus
    {
        Unknown,
        Success,
        ValidationError,
        Failure
    }
}
