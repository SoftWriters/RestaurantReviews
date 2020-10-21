using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Services
{
    public class ReviewService: IReviewService
    {
        private RestaurantContext _context;
        public ReviewService(RestaurantContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetAllByUserId(string userId)
        {
            return await _context.Reviews.Where(_ => _.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetAllByRestaurant(int restaurantId)
        {
            return await _context.Reviews.Where(_ => _.RestaurantId == restaurantId).ToListAsync();
        }
        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public void UpdateReview(int id, Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
        }
        public void CreateReview(Review review)
        {
            _context.Reviews.Add(review);
        }
        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
        }
        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

    }

}
