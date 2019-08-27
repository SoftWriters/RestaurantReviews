using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Repository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(IContext context) : base(context) { }

        public override IDataSet<Review> GetDataSet()
        {
            return context.ReviewDataSet;
        }

        public override void Update(long id, Review q)
        {
            var contents = GetAll();
            var found = contents.FirstOrDefault(x => x.Id == id);
            if (found == null)
                return;
            found.Content = q.Content;
            found.RestaurantId = q.RestaurantId;
            found.UserId = q.UserId;
            context.ReviewDataSet.Save(contents);
        }

        public IList<Review> GetByUserId(int userId)
        {
            var contents = GetAll();
            return contents.Where(p => p.UserId == userId).ToList();
        }
    }
}