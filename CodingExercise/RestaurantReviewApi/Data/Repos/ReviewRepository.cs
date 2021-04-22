using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public void CreateReview(Review review)
        {
            review.DateSubmitted = DateTime.Now;
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                review.Id = conn.Query<int>(
                    $"INSERT INTO Review (RestaurantId, UserId, DateSubmitted, Text, Rating) " +
                    $"VALUES (@RestaurantId, @UserId, @DateSubmitted, @Text, @Rating); " +
                    $"SELECT last_insert_rowid() ", 
                    review).First();
            }
        }

        public void DeleteReview(int id)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM Review WHERE Id = @id", new { id });
            }
        }

        public IList<Review> ReadReviewsByUser(int userId)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                var reviews = conn.Query<Review>(
                    "SELECT rv.Id, rv.RestaurantId, rs.Name as RestaurantName, rv.UserId, ur.Name as UserName, rv.DateSubmitted, rv.Text, rv.Rating " +
                    "FROM Review rv " +
                    "   INNER JOIN Restaurant rs on rv.RestaurantId = rs.Id " +
                    "   INNER JOIN User ur on rv.UserId = ur.Id " +
                    "WHERE rv.UserId = @userId"
                    , new { userId }).ToList();

                return reviews;
            }
        }

        public IList<Review> ReadAllReviews()
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                var reviews = conn.Query<Review>(
                    "SELECT rv.Id, rv.RestaurantId, rs.Name as RestaurantName, rv.UserId, ur.Name as UserName, rv.DateSubmitted, rv.Text, rv.Rating " +
                    "FROM Review rv " +
                    "   INNER JOIN Restaurant rs on rv.RestaurantId = rs.Id " +
                    "   INNER JOIN User ur on rv.UserId = ur.Id "
                    ).ToList();

                return reviews;
            }
        }

    }
}
