using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Data.Services
{
    public class RestaurantService : IServiceBase<Restaurant>
    {
        public void Delete(Guid id)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                var dbModel = context.Restaurants.Find(id);

                context.Restaurants.Remove(dbModel);
            }
        }

        public List<Restaurant> GetAll()
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Restaurants.ToList();
            }
        }

        public Restaurant GetByID(Guid id)
        {
            using(var context = new RestaurantReviewsEntities())
            {
                return context.Restaurants.Find(id);
            }
        }

        public List<Restaurant> GetByCity(string city)
        {
            using (var context = new RestaurantReviewsEntities())
            {
                return context.Restaurants.Where(m => m.City == city).ToList();
            }
        }

        public void Save(Restaurant t)
        {
            if(GetByID(t.Id) != null)
            {
                // Insert
                using (var context = new RestaurantReviewsEntities())
                {
                    var dbResteraunt = new Restaurant()
                    {
                        City = t.City,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
                        Name = t.Name,
                        State = t.State
                    };

                    context.Restaurants.Add(dbResteraunt);
                    context.SaveChanges();

                    t.Id = dbResteraunt.Id;
                }
            }
            else
            {
                // Update
                using (var context = new RestaurantReviewsEntities())
                {
                    var dbModel = context.Restaurants.Find(t.Id);

                    dbModel.City = t.City;
                    dbModel.Name = t.Name;
                    dbModel.State = t.State;

                    context.SaveChanges();
                }
            }
        }
    }
}