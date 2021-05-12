using DTOs;
using DTOs.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ORMLayer;
using ORMLayer.TableDefinitions;
using Repositories.Interfaces;
using Repositories.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
  public class ReviewRepoEF : IReviewRepo
  {
    private readonly string _connectionString;
    private readonly ITranslator<ReviewDTO,Review> _translator;

    public ReviewRepoEF(string connectionString)
    {
      _connectionString = connectionString;
      _translator = new ReviewTranslator();
    }
    public async Task AddReview(ReviewDTO newReview)
    {
      using (var context = getContext())
      {
        try
        {
          context.Reviews.Add(_translator.DtoToEntity(newReview));
          await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
          //If exception was caused by Foriegn Key Constraints or Unique Constraints
            // throw validation exception that will be proccessed to an HTTPResponse with clear failure reason in Abstract Controller
          if (!await context.Users.AnyAsync(u => u.Id == newReview.UserId))
            throw new ValidationException(String.Format("User with id {0} does not exist", newReview.UserId), ex);
          if (!await context.Restaurants.AnyAsync(r => r.Id == newReview.RestaurantId))
            throw new ValidationException(String.Format("Restaurant with id {0} does not exist", newReview.RestaurantId), ex);
          if (await context.Reviews.AnyAsync(r => r.RestaurantId == newReview.RestaurantId && r.UserId == newReview.UserId))
            throw new ValidationException(String.Format("User already wrote a review for this resturaunt.  Delete it before adding new review."));

          throw;
        }
      }
    }

    public async Task<bool> DeleteReview(long reviewId)
    {
      using(var context = getContext())
      {
        //Use SQL rather than EF to delete so only one sql statment is executed vs EF Retrieving Record then deleting it
        var deleteSql = @"DELETE FROM [Reviews] WHERE Id = @Id";
        var rowsEffected = await context.Database.ExecuteSqlRawAsync(deleteSql, new SqlParameter("@Id", reviewId));
        return rowsEffected > 0;
      }
    }

    public async Task<IEnumerable<ReviewDTO>> GetReviews(long? userId = null, long? restaurantId = null, bool sortLowToHigh = false)
    {
      if(!userId.HasValue && !restaurantId.HasValue)
        throw new ArgumentException("Either userId or restaurantId must be provided");

      using (var context = getContext())
      {
        IQueryable<Review> query = context.Reviews;
        query = userId.HasValue ? query.Where(r => r.UserId == userId.Value) : query; 
        query = restaurantId.HasValue ? query.Where(r => r.RestaurantId == restaurantId.Value) : query;

        return sortLowToHigh
          ? (await query.OrderBy(r => r.Stars).ToListAsync()).Select(_translator.EntityToDto)
          : (await query.OrderByDescending(r => r.Stars).ToListAsync()).Select(_translator.EntityToDto);
      }
    }

    private DatabaseContext getContext() { return new DatabaseContext(_connectionString); }
  }
}
