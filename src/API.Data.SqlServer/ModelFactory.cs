/******************************************************************************
 * Name: ModelFactory.cs
 * Purpose: Model Factory class with static methods to convert DTO objects to
 *           Entity Framework objects and vice-versa
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Data.SqlServer.DataModel;

namespace RestaurantReviews.API.Data.SqlServer
{
    public class ModelFactory
    {
        public static RestaurantModelDTO Create(TblRestaurant restaurant)
        {
            return new RestaurantModelDTO()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                City = restaurant.City
            };
        }

        public static ReviewModelDTO Create(TblReview review)
        {
            return new ReviewModelDTO()
            {
                Id = review.Id,
                Reviewer = Create(review.Reviewer),
                Restaurant = Create(review.Restaurant),
                ReviewedDateTime = review.ReviewDateTime,
                Rating = review.Rating,
                ReviewText = review.ReviewText
            };
        }

        public static TblRestaurant Create(RestaurantModelDTO restaurant)
        {
            return new TblRestaurant()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                City = restaurant.City
            };
        }

        public static TblReview Create(ReviewModelDTO review)
        {
            return new TblReview()
            {
                Id = review.Id,
                Reviewer = Create(review.Reviewer),
                Restaurant = Create(review.Restaurant),
                ReviewDateTime = review.ReviewedDateTime,
                Rating = review.Rating,
                ReviewText = review.ReviewText
            };
        }

        public static TblReviewer Create(ReviewerModelDTO reviewer)
        {
            return new TblReviewer()
            {
                Id = reviewer.Id,
                Name = reviewer.Name
            };
        }

        public static ReviewerModelDTO Create(TblReviewer reviewer)
        {
            return new ReviewerModelDTO()
            {
                Id = reviewer.Id,
                Name = reviewer.Name
            };
        }
    }
}
