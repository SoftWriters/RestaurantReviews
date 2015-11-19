using RestaurantReview.BL.Base;
using RestaurantReview.Common;
using RestaurantReview.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.BL;
using RestaurantReview.Service.Interface;
using RestaurantReview.Service.Base;

namespace RestaurantReview.Service
{
    public class ReviewService : ServiceBase<BL.Model.Review, DAL.Entity.Review>, IReviewService
    { 
        public ReviewService(IRRContext context)
            : base(context)
        {
        }

        public ReviewService()
            : base()
        {
        }

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        public IEnumerable<BL.Model.Review> GetByUserId(int userID)
        {
            IEnumerable<DAL.Entity.Review> reviewList = _context.ReviewsGetByUser(userID);

            IEnumerable<BL.Model.Review> ret = Convert(reviewList);

            return ret;
        }

        public new int Add(BL.Model.Review review, int userID)
        {
            if (review == null)
            {
                throw new ArgumentNullException("review");
            }

            int ret = _context.ReviewAdd(review.RestaurantID, review.UserID, review.Rating, review.Comments);

            if (ret < 0)
            {
                throw new Exception("Error adding record to database");
            }

            return ret;
        }

        public new void Delete(BL.Model.Review model, int userID)
        {
            DAL.Entity.Review rEntity = Get(model.Id, userID);

            _dbset.Remove(rEntity);
            _context.SaveChanges();
        }

        private DAL.Entity.Review Get(int iD, int userID)
        {
            var query = from r in _context.Reviews
                        where r.Id == iD
                        select r;

            return query.Single();
        }
    }
}