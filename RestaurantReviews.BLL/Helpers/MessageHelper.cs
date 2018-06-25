namespace RetaurantReviews.BLL.Helpers
{
    public static class MessageHelper
    {
        public static string GetMissingParameterErrorMessage(string parameterName)
        {
            return string.Format("The request does not have the \'{0}\' parameter.", parameterName);
        }
        
        public static string GetInvalidParameterErrorMessage(string parameterName, string extraMessage = null)
        {
            string errorMessage = string.Format("The request passes an invalid value for the \'{0}\' parameter.", parameterName);
            if (!string.IsNullOrEmpty(extraMessage))
            {
                errorMessage = string.Concat(errorMessage, " ", extraMessage);
            }
            return errorMessage;
        }
        
        public static string GetInternalServerErrorMessage(string extraMessage = null)
        {
            string errorMessage = "An unrecoverable server error has occured.";
            if (!string.IsNullOrEmpty(extraMessage))
            {
                errorMessage = string.Concat(errorMessage, " ", extraMessage);
            }
            return errorMessage;
        }

        public static string GetNotFoundErrorMessage(bool singular)
        {
            return singular ? "The requested API model doesn't exist in the server."
                            : "The requested API models don't exist in the server.";
        }
    }
}
