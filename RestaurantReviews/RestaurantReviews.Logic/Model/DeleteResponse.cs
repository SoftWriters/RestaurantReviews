using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace RestaurantReviews.Logic.Model
{
    public class DeleteResponse : IApiResponse
    {
        public DeleteResponseStatus Status { get; set; }

        string IApiResponse.Status => Status.ToString();

        public string Message { get; set; }

        public IEnumerable<ValidationResult> ValidationErrors { get; set; }

        public static DeleteResponse Success()
        {
            return new DeleteResponse
            {
                Status = DeleteResponseStatus.Success,
                Message = "Deleted successfully"
            };
        }

        public static DeleteResponse NotFound()
        {
            return new DeleteResponse
            {
                Status = DeleteResponseStatus.NotFound,
                Message = "The specified record was not found"
            };
        }

        public static DeleteResponse Exception(Exception ex)
        {
            return new DeleteResponse
            {
                Status = DeleteResponseStatus.Failure,
                Message = Debugger.IsAttached ? ex.ToString() : "Internal error"
            };
        }

        public static bool HasValidationErrors(object request, out DeleteResponse response)
        {
            var results = new List<ValidationResult>();
            response = Success();
            if (Validator.TryValidateObject(request, new ValidationContext(request), results, true))
            {
                return false;
            }

            response.Status = DeleteResponseStatus.ValidationError;
            response.Message = "One or more validation errors occurred";
            response.ValidationErrors = results;
            return true;
        }
    }

    public enum DeleteResponseStatus
    {
        Unknown,
        Success,
        NotFound,
        ValidationError,
        Failure
    }
}
