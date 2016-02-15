using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Data.Services
{
    public class ReviewService : IServiceBase<Review>
    {
        public void Delete(Guid id)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                var dbModel = context.Reviews.Find(id);

                context.Reviews.Remove(dbModel);
            }
        }

        public List<Review> GetAll()
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Reviews.ToList();
            }
        }

        public Review GetByID(Guid id)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Reviews.Find(id);
            }
        }
        
        public List<Review> GetByUserID(Guid userID)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Reviews.Where(m => m.UserId == userID).ToList();
            }
        }

        public void Save(Review t)
        {
            if (GetByID(t.Id) != null)
            {
                // Insert
                using (var context = new RestaurantReviewsEntities())
                {
                    var dbReview = new Review()
                    {
                        Contents = t.Contents,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
                        RestaurantId = t.RestaurantId,
                        Title = t.Title
                    };

                    context.Reviews.Add(dbReview);
                    context.SaveChanges();

                    t.Id = dbReview.Id;
                }
            }
            else
            {
                // Update
                using (var context = new RestaurantReviewsEntities())
                {
                    var dbModel = context.Reviews.Find(t.Id);

                    dbModel.Contents = t.Contents;
                    dbModel.CreatedBy = t.CreatedBy;
                    dbModel.CreatedOn = t.CreatedOn;
                    dbModel.RestaurantId = t.RestaurantId;
                    dbModel.Title = t.Title;

                    context.SaveChanges();
                }
            }
        }
    }
}