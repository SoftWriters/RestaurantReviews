using System.Web.Http.ExceptionHandling;

namespace RestaurantReviews.Web.Api.Logging
{
    /// <summary>
    /// Logs unhandled exceptions to whatever outside service is responsible for aggregating them
    /// </summary>
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var logEntry = context.Exception.ToString();
            //here we would call whatever logging service is setup for the system as a whole!
        }
    }
}