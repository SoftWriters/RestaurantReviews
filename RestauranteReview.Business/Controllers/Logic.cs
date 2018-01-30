using System;
using System.Net;

namespace RestaurantReview.BusinessLogic
{
    public class Logic
    {
        public bool TotalFailureMessage(Exception ex, out HttpStatusCode suggestedStatusCode, out string errorMessage)
        {
            suggestedStatusCode = HttpStatusCode.InternalServerError;
            errorMessage = "Application error has occured. Please email \"correalf01@gmail.com\" with this request time for further support."; //ex.ToString();
            return false;
        }

        public string CleanRestaurantName(string name)
        {
            return name.ToLower().Replace(" ", "");
        }
    }
}
