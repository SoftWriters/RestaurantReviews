using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	/// <summary>
	/// The exception that is thrown when trying to set a rating outside the acceptable range <see cref="Review.MinRating"/> to <see cref="Review.MaxRating"/>.
	/// </summary>
	public class RatingOutOfRangeException : ApplicationException
	{
		private const string _messageFormat = "The rating specified {0}{1}is outside the acceptable range {2}-{3}";

		public RatingOutOfRangeException() : base(string.Format(_messageFormat, string.Empty, string.Empty, Review.MinRating, Review.MaxRating)) { }

		public RatingOutOfRangeException(int attemptedRating) : base(string.Format(_messageFormat, attemptedRating, " ", Review.MinRating, Review.MaxRating)) { }

		//...and the other expected overloads for flexibility
		public RatingOutOfRangeException(string message) : base(message) { }
		public RatingOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }
		public RatingOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
