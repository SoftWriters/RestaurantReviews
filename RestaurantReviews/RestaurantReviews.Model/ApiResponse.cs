using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RestaurantReviews.Model
{
    /// <summary>
    /// "Envelope" for api responses to provide a consistent response schema regardless of whether the response is an error or success
    /// </summary>
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public T Payload { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }

    public static class ApiResponse
    {
        public static ApiResponse<T> Success<T>(T payload)
        {
            return new ApiResponse<T>
            {
                Status = (int)HttpStatusCode.OK,
                StatusMessage = HttpStatusCode.OK.ToString(),
                Payload = payload
            };
        }

        public static ApiResponse<T> BadRequest<T>()
        {
            return new ApiResponse<T>
            {
                Status = (int)HttpStatusCode.BadRequest,
                StatusMessage = HttpStatusCode.BadRequest.ToString(),
            };
        }
    }
}
