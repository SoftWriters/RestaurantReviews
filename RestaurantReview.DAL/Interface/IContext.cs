using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.DAL.Interface
{
    public interface IRRContext
    {
        IDbSet<Entity.Review> Reviews { get; set; }
        IDbSet<Entity.User> Users { get; set; }
        IDbSet<Entity.Restaurant> Restaurants { get; set; }
        IDbSet<Entity.City> Cities { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int CityAdd(string City, string State);
        int RestaurantAdd(int CityID, string Name);
        IEnumerable<Entity.Restaurant> RestaurantsGetByCity(int CityID);
        int ReviewAdd(int RestaurantID, int UserID, int Rating, string Comments);
        IEnumerable<Entity.Review> ReviewsGetByUser(int UserID);

        int SaveChanges();
    }
}
