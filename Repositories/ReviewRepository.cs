
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ReviewContext context) : base(context) { }

        public override async Task<List<Review>> ListAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public override async Task<Review> CreateAsync(Object dto)
        {
            var reviewDto = (ReviewDTO)dto;
            var review = new Review()
            {
                RestaurantID = reviewDto.RestaurantID,
                UserID = reviewDto.UserID,
                Text = reviewDto.Text,
                Rating = reviewDto.Rating,
                Timestamp = DateTime.Now

            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public override async Task<Review> ReadAsync(long id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public override Task<Review> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<Review> DeleteAsync(long id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<List<Review>> ListReviewsByUserAsync(long userId)
        {
            return await _context.Reviews
                .Where(review => review.UserID == userId)
                .ToListAsync();
        }
    }
}
