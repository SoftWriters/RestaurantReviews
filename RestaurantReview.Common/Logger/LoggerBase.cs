using System;

namespace RestaurantReview.Common.Logger
{
	/// <summary>
	/// This is the base class for loggin purposes. 
	/// </summary>
	public abstract class LoggerBase
	{
        //TODO: Insert logger package here

		protected abstract System.Type LogPrefix
		{
			get;
		}

        private static bool isConfigured = false;

		public LoggerBase()
		{
			// initiate logging class           
            if (!isConfigured)
            {
               //TODO:Initiate Logger
            }
            //TODO: Get Logger
		}

		/// <summary>
		/// Information level messages are logged to the logger.
		/// </summary>
		/// <param name="message">String that needs to be logged.</param>
		protected void LogInfo(string message)
		{
            //TODO: Log info
        }

        /// <summary>
        /// Warning level messages are logged to the logger.
        /// </summary>
        /// <param name="message">String that needs to be logged.</param>
        protected void LogWarn(string message)
		{
            //TODO: Log warn
        }

        /// <summary>
        /// Error level messages are logged to the logger.
        /// </summary>
        /// <param name="message">String that needs to be logged.</param>
        /// <param name="e">The exception that needs to be logged.</param>
        protected void LogError(string message, Exception e)
		{
            //TODO: Log error
        }

        /// <summary>
        /// Debug level messages are logged to the logger.
        /// </summary>
        /// <param name="message">String that needs to be logged</param>
        protected void LogDebug(string message)
		{
            //TODO: Log debug
        }

        /// <summary>
        /// Fatal level messages are logged to the logger.
        /// </summary>
        /// <param name="message">String that needs to be logged</param>
        /// <param name="e">The exception that needs to be logged</param>
        protected void LogFatal(string message, Exception e)
		{
			//TODO: Log fatal error
		}
	}
}
