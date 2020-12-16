using RestaurantReviews.Logic.Model.Restaurant.Create;
using RestaurantReviews.Logic.Model.Restaurant.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.QueryBuilder
{
    public interface IRestaurantQueryBuilder :
        IQueryBuilderSearch<Restaurant, SearchRestaurantRequest, SearchRestaurant>,
        IQueryBuilderUpsert<Restaurant, CreateRestaurantRequest>
    { }


    public class RestaurantQueryBuilder : IRestaurantQueryBuilder
    {
        public IQueryable<Restaurant> BuildSearchQuery(IQueryable<Restaurant> dbSet, SearchRestaurantRequest request)
        {
            IQueryable<Restaurant> query = dbSet;
            if (request?.State != null)
            {
                query = query.Where(p => p.State.ToLower() == request.State.ToLower());
            }
            if (request?.Cities != null)
            {
                var loweredCities = request.Cities.Select(p => p?.ToLower());
                query = query.Where(p => loweredCities.Contains(p.City.ToLower()));
            }

            return query;
        }

        public SearchRestaurant BuildSearchResponse(Restaurant entity)
        {
            return new SearchRestaurant()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                City = entity.City,
                State = entity.State,
                Zip = entity.ZipCode
            };
        }

        public Restaurant BuildEntityUpsert(CreateRestaurantRequest request)
        {
            return new Restaurant()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                City = request.City,
                State = request.State,
                ZipCode = request.Zip
            };
        }

        public IQueryable<Restaurant> BuildQuerySingle(IQueryable<Restaurant> dbSet, CreateRestaurantRequest request)
        {
            return dbSet.Where(p => p.Name.ToLower() == request.Name.ToLower() &&
                p.City.ToLower() == request.City.ToLower() &&
                p.State.ToLower() == request.State.ToLower() &&
                p.ZipCode == request.Zip.ToLower());
        }
    }
}
