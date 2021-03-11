using System;

namespace SoftWriters.RestaurantReviews.WebApi
{
    public class SoftWriterException : Exception
    {
        public SoftWriterException()
        { }

        public SoftWriterException(string message) : base(message)
        { }

        public SoftWriterException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
