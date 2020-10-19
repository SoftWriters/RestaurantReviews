using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantReviewsApi.Bll.Common
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, string displayMessage = null) : base(message)
        {
            DisplayMessage = displayMessage;
        }

        public string DisplayMessage { get; }
    }
}
