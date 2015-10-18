using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	public class Review : PersistedObject<Review>
	{
		/// <summary>
		/// The restaurant for which this review was submitted.
		/// </summary>
		public Restaurant Restaurant { get; internal set; }

		/// <summary>
		/// The lowest rating on our scale: 1.
		/// </summary>
		public const int MinRating = 1;

		/// <summary>
		/// The highest rating on our scale: 5.
		/// </summary>
		public const int MaxRating = 5;

		private int _rating;
		/// <summary>
		/// The rating associated with this review, on a scale from 1 (lowest) to 5 (highest).
		/// </summary>
		public int Rating
		{
			get
			{
				return _rating;
			}
			set
			{
				if (value < MinRating || value > MaxRating)
					throw new RatingOutOfRangeException(value);
				_rating = value;
			}
		}

		/// <summary>
		/// Tally of people who agree with this review.
		/// </summary>
		public int UpVotes { get; internal set; }
		/// <summary>
		/// Votes this review up.
		/// </summary>
		public void VoteUp()
		{
			UpVotes++;
		}

		/// <summary>
		/// Tally of people who disagree with this review.
		/// </summary>
		public int DownVotes { get; internal set; }
		/// <summary>
		/// Votes this review down.
		/// </summary>
		public void VoteDown()
		{
			DownVotes++;
		}

		public AuthenticatedUser SubmittedBy { get; set; }

		public static Review Add(Restaurant restaurant, int rating, AuthenticatedUser submittedBy)
		{
			Review newReview = new Review
			{
				Restaurant = restaurant,
				Rating = rating,
				SubmittedBy = submittedBy
			};

			//TODO: Persist via separate layer or ORM attributes (EntityFramework, NHibernate, etc)

			return newReview;
		}

		public static List<Review> FindAllByRestaurant(Restaurant restaurant)
		{
			//TODO: Need persistence layer for lookup
			throw new NotImplementedException();
		}

		public static List<Review> FindAllBySubmitter(string userName)
		{
			//TODO: Need persistence layer for lookup
			throw new NotImplementedException();
		}
	}
}
