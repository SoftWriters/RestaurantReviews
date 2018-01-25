using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Data.Services
{
    public class UserServicecs : IServiceBase<User>
    {
        public User GetByID(Guid id)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Users.Find(id);
            }
        }

        public List<User> GetAll()
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Users.ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                var dbModel = context.Users.Find(id);

                context.Users.Remove(dbModel);
            }
        }

        public void Save(User t)
        {
            if (GetByID(t.Id) != null)
            {
                // Insert
                using (var context = new RestaurantReviewsEntities())
                {
                    var dbUser = new User()
                    {
                        IsActive = t.IsActive,
                        Name = t.Name
                    };

                    context.Users.Add(dbUser);
                    context.SaveChanges();

                    t.Id = dbUser.Id;
                }
            }
            else
            {
                // Update
                using (var context = new RestaurantReviewsEntities())
                {
                    var dbModel = context.Users.Find(t.Id);

                    dbModel.Name = t.Name;
                    dbModel.IsActive = t.IsActive;

                    context.SaveChanges();
                }
            }
        } 
    }
}