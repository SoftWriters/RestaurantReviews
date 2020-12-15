using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data.QueryBuilder;
using RestaurantReviews.Logic;
using RestaurantReviews.Logic.Model;
using RestaurantReviews.Logic.Model.Restaurant.Create;
using RestaurantReviews.Logic.Model.Restaurant.Search;
using RestaurantReviews.Logic.Model.Review.Create;
using RestaurantReviews.Logic.Model.Review.Search;
using RestaurantReviews.Logic.Model.User.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    public class RestaurantLogicEF : IRestaurantLogic
    {
        private readonly RestaurantContext context;
        private readonly IRestaurantQueryBuilder restaurantQueryBuilder;
        private readonly IReviewQueryBuilder reviewQueryBuilder;
        private readonly IUserQueryBuilder userQueryBuilder;

        public RestaurantLogicEF(
            RestaurantContext context,
            IRestaurantQueryBuilder restaurantQueryBuilder,
            IReviewQueryBuilder reviewQueryBuilder,
            IUserQueryBuilder userQueryBuilder)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.restaurantQueryBuilder = restaurantQueryBuilder ?? throw new ArgumentNullException(nameof(restaurantQueryBuilder));
            this.reviewQueryBuilder = reviewQueryBuilder ?? throw new ArgumentNullException(nameof(reviewQueryBuilder));
            this.userQueryBuilder = userQueryBuilder ?? throw new ArgumentNullException(nameof(userQueryBuilder));
        }

        public Task<CreateResponse> CreateRestaurant(CreateRestaurantRequest request)
        {
            return Create(restaurantQueryBuilder, request, p => p.Restaurants);
        }

        public Task<CreateResponse> CreateReview(CreateReviewRequest request)
        {
            return Create(reviewQueryBuilder, request, p => p.Reviews);
        }

        public Task<SearchResponse<SearchRestaurant>> SearchRestaurants(SearchRestaurantRequest request)
        {
            return Search(restaurantQueryBuilder, request, p => p.Restaurants);
        }

        public Task<SearchResponse<SearchReview>> SearchReviews(SearchReviewRequest request)
        {
            return Search(reviewQueryBuilder, request, p => p.Reviews);
        }

        public Task<SearchResponse<SearchUser>> SearchUsers(SearchUserRequest request)
        {
            return Search(userQueryBuilder, request, p => p.Users);
        }

        /// <summary>
        /// Generic search implementation that first validates any request model requirements and wraps the response into an "envelope"
        /// </summary>
        private async Task<SearchResponse<T>> Search<TEntity, TRequest, T>(
            IQueryBuilderSearch<TEntity, TRequest, T> queryBuilder,
            TRequest request,
            Func<RestaurantContext, DbSet<TEntity>> getSet)
            where TEntity : class
        {
            if (SearchResponse.HasValidationErrors<T>(request, out var response))
            {
                return response;
            }
            else
            {
                try
                {
                    var results = await queryBuilder.BuildSearchQuery(getSet(context), request)
                        .ToListAsync();
                    return SearchResponse.Success(results.Select(p => queryBuilder.BuildSearchEntity(p)));
                }
                catch (Exception ex)
                {
                    return SearchResponse.Exception<T>(ex);
                }
            }
        }

        /// <summary>
        /// Generic create implementation that first validates any request model requirements and wraps the response into an "envelope"
        /// </summary>
        private async Task<CreateResponse> Create<TEntity, TRequest>(
            IQueryBuilderUpsert<TEntity, TRequest> queryBuilder,
            TRequest request,
            Func<RestaurantContext, DbSet<TEntity>> getSet)
            where TEntity : class, IEntity
        {
            var set = getSet(context);
            var query = queryBuilder.BuildUpsertQuery(set, request);
            var existing = await query.ToListAsync();
            if (CreateResponse.HasValidationErrors(request, out var errorResponse))
            {
                return errorResponse;
            }
            else if (existing.Any())
            {
                return CreateResponse.Duplicate(existing.First().Id.ToString());
            }
            else
            {
                var entity = queryBuilder.BuildUpsertEntity(request);
                try
                {
                    set.Add(entity);
                    await context.SaveChangesAsync();
                    return CreateResponse.Success(entity.Id.ToString());
                }
                catch (Exception ex)
                {
                    return CreateResponse.Exception(ex);
                }
            }
        }
    }
}
