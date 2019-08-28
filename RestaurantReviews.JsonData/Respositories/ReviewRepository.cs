using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;

namespace RestaurantReviews.JsonData.Repositories
{
    public class ReviewRepository : RepositoryBase<IReview>, IReviewRepository
    {
        public ReviewRepository(IContext context) : base(context) { }

        public override IDataSet<IReview> GetDataSet()
        {
            return context.ReviewDataSet;
        }

        public override void Update(long id, IReview q)
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

        public ICollection<IReview> GetByUserId(int userId)
        {
            var contents = GetAll();
            return contents.Where(p => p.UserId == userId).ToList();
        }
    }
}