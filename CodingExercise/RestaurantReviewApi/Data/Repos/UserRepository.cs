using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User ReadUser(int id)
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                User user = conn.Query<User>(                   
                        "SELECT Id, Name " +
                        "FROM User " +
                        "WHERE Id= @id",
                        new { id }).FirstOrDefault();

                user.Reviews = conn.Query<Review>(
                    "SELECT rv.Id, rv.CityId, ct.Name as CityName, rv.UserId, ur.Name as UserName, rv.ReviewDate, rv.ReviewText, rv.Rating " +
                    "FROM Review rv " +
                    "   INNER JOIN City ct on rv.CityId = ct.Id " +
                    "   INNER JOIN User ur on rv.UserId = ur.Id " +
                    "WHERE rv.UserId = @id", new { id }).ToList();

                return user;
            }
        }

        public IList<User> ReadAllUsers()
        {
            using (var conn = ResturauntDbConnection())
            {
                conn.Open();
                IList<User> user = conn.Query<User>(
                        "SELECT Id, Name " +
                        "FROM User "
                        ).ToList();

                return user;
            }
        }

    }
}
