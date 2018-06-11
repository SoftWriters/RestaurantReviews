/******************************************************************************
 * Name: SqlReviewRepository.cs
 * Purpose: Review Repository that uses EF to perform Repo operations
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Data.SqlServer.DataModel;

namespace RestaurantReviews.API.Data.SqlServer
{
    public class SqlReviewRepository : IReviewRepository
    {
        public ReviewModelList GetReviewsByRestaurant(int restaurant_id)
        {
            ReviewModelList ret = new ReviewModelList();
            ret.ReviewList = new List<ReviewModelDTO>();

            using (var db = new RestaurantReviewsContext())
            {
                var reviews = from r in db.TblReview
                              where r.RestaurantId == restaurant_id
                              select r;

                foreach(TblReview review in reviews)
                {
                    ret.ReviewList.Add(ModelFactory.Create(review));
                }
            }

            return ret;
        }

        public ReviewModelList GetReviewsByReviewer(int reviewer_id)
        {
            ReviewModelList ret = new ReviewModelList();
            ret.ReviewList = new List<ReviewModelDTO>();

            using (var db = new RestaurantReviewsContext())
            {
                var reviews = from r in db.TblReview
                              where r.ReviewerId == reviewer_id
                              select r;

                foreach (TblReview review in reviews)
                {
                    ret.ReviewList.Add(ModelFactory.Create(review));
                }
            }

            return ret;
        }

        public ReviewModelDTO GetReviewById(int id)
        {
            ReviewModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                var review = db.TblReview.Find(id);
                if(review != null) ret = ModelFactory.Create(review);
            }

            return ret;
        }

        public ReviewModelDTO AddReview(ReviewModelDTO newReview)
        {
            ReviewModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                TblReview review = ModelFactory.Create(newReview);
                db.TblReview.Add(review);
                db.SaveChanges();
                ret = ModelFactory.Create(review);
            }

            return ret;
        }

        public ReviewModelDTO DeleteReview(int id)
        {
            ReviewModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                TblReview review = db.TblReview.Find(id);
                if(review != null)
                {
                    db.TblReview.Remove(review);
                    db.SaveChanges();
                    ret = ModelFactory.Create(review);
                }
            }

            return ret;
        }
    }
}
