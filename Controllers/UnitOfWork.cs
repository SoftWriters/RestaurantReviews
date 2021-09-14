
using System.Threading.Tasks;
using RestaurantReviews.Repositories;

namespace RestaurantReviews
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReviewContext _context;

        public IUserRepository Users { get; private set; }
        public IRestaurantRepository Restaurants { get; private set; }
        public IReviewRepository Reviews { get; private set; }

        public UnitOfWork(ReviewContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Restaurants = new RestaurantRepository(_context);
            Reviews = new ReviewRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
