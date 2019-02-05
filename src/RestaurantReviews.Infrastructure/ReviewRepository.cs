using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReviews.Domain;

namespace RestaurantReviews.Infrastructure
{
    /// <summary>
    /// In the real world, this class should be wired up to the database using any ORM tools such as entity framework or NHibernate or the ado.net code
    /// for the purpose of a demo, a 
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        private static ConcurrentDictionary<Guid, Review> _reviewRepository; //key: review id, value: review
        private static ConcurrentDictionary<Guid, User> _userRepository; //key: userid, value: User

        static ReviewRepository()

        {
            _reviewRepository = new ConcurrentDictionary<Guid, Review>();
            _userRepository = new ConcurrentDictionary<Guid, User>();

            //Adding the bootstrap data
            List<User> users = new List<User>(new User[]{
                new User{UserId=new Guid("9A172816-751A-4947-943B-755B0EF3BD9A"), FullName="John Smith"},
                new User{UserId=new Guid("1EA27E80-11D2-437C-BAF4-12152D02B1E8"), FullName="Jane Smith"},
                new User{UserId=new Guid("4B61528A-DE26-4E91-9420-F65EBE581CA3"), FullName="John Doe"},
                new User{UserId=new Guid("16C81D20-3BC7-4D4B-BFED-09868501E5E8"), FullName="Jennifer Doe"},
                new User{UserId=new Guid("1841B436-63CE-4EF3-B5DB-AC20AA69125A"), FullName="Steve King"},
            });
            users.ForEach(u => _userRepository.GetOrAdd(u.UserId, u));


        }

        public async Task<Review> AddAsync(Review review)
        {
            return await Task.Run<Review>(() =>
            {
                return _reviewRepository.GetOrAdd(review.ReviewId, review);
            });
        }

        public async Task DeleteAsync(Guid reviewId)
        {
            await Task.Run(() =>
            {
                _reviewRepository.Remove(reviewId, out Review review);
            });
        }

        public async Task<Review> FindAsync(Guid reviewId)
        {
            return await Task.Run<Review>(() =>
            {
                _reviewRepository.TryGetValue(reviewId, out Review ret);
                return ret;
            });
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserAsync(Guid userId)
        {
            return await Task.Run<IEnumerable<Review>>(() =>
            {
                return (new List<Review>(_reviewRepository.Values)).FindAll(a =>
                a.CreatedUserId == userId);
            });
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
        {
            return await Task.Run<IEnumerable<User>>(() =>
            {
                return (new List<User>(_userRepository.Values)).FindAll(a =>
                a.FullName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
            });
        }

        public async Task<User> FindUserAsync(Guid userId)
        {
            return await Task.Run<User>(() =>
            {
                _userRepository.TryGetValue(userId, out User user);
                return user;
            });
        }
    }
}
