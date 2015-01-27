using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;

namespace RestaurantReviews
{
    public class RestaurantReviewsAppService : ApplicationService, IRestaurantReviewsAppService
    {
        //These members set in constructor using dependency injection.
        private readonly IReviewRepository _reviewRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRepository<City, long> _cityRepository;
        private readonly IRepository<State> _stateRepository;
        private readonly IRepository<User, long> _userepository;

        public RestaurantReviewsAppService(IReviewRepository reviewRepository,
                                           IRestaurantRepository restaurantRepository,
                                           IRepository<City, long> cityRepository,
                                           IRepository<User, long> userRepository)
        {
            _reviewRepository = reviewRepository;
            _restaurantRepository = restaurantRepository;
            _cityRepository = cityRepository;
            _userepository = userRepository;
        }

        /// <summary>
        /// Not called out as a project requirement, but will be needed by app in
        /// order to pass back CityID as input when requesting restuarants by city
        /// or creating a new restaurant.
        /// </summary>
        /// <returns></returns>
        public GetCitiesOutput GetAllCities()
        {
            var cities = _cityRepository.GetAll();

            return new GetCitiesOutput
            {
                Cities = cities.ToList()
            };
        }

        /// <summary>
        /// Gets a list of Restaurants by City
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetRestaurantsOutput GetRestaurants(GetRestaurantsInput input)
        {
            var restaurants = _restaurantRepository.GetAllByCity(input.CityId);

            return new GetRestaurantsOutput
            {
                Restaurants = restaurants
            };
        }

        /// <summary>
        /// Gets a list of Reviews by User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetReviewsOutput GetReviews(GetReviewsInput input)
        {
            var reviews = _reviewRepository.GetAllByUser(input.UserId);

            return new GetReviewsOutput
            {
                Reviews = reviews
            };
        }

        /// <summary>
        /// Posts a new restaurant not in the database
        /// </summary>
        /// <param name="input">City ID and restaurant name are required.</param>
        public void PostRestaurant(CreateRestaurantInput input)
        {
            //Creating a new Restaurant entity with given input's properties
            var restaurant = new Restaurant
            {
                CityId = input.CityId,
                Name = input.Name
            };

            //Saving entity with standard Insert method of repositories.
            _restaurantRepository.Insert(restaurant);
        }

        /// <summary>
        /// Posts a new review for a restaurant.
        /// </summary>
        /// <param name="input"></param>
        public void PostReview(CreateReviewInput input)
        {
            //Creating a new Review entity with given input's properties
            var review = new Review { RestaurantId = input.RestaurantId,
                                      ReviewerId = input.UserId,
                                      Description = input.Description };

            //Saving entity with standard Insert method of repositories.
            _reviewRepository.Insert(review);
        }

        /// <summary>
        /// Deletes reviews.
        /// </summary>
        /// <remarks>
        /// If Creation DateTime is passed, only one review will be deleted.
        /// Otherwise, all reviews specific to passed User and Restaurant will be deleted.
        /// </remarks>
        /// <param name="input">UserID and RestaurantID are required</param>
        public void DeleteReview(DeleteReviewInput input)
        {
            //Deleting entity with linq expression syntax.
            var query = _reviewRepository.GetAllByUser(input.UserId).AsQueryable()
                        .Where(review => review.RestaurantId == input.RestaurantId);

            //If creation time is passed in input, delete only specific review,
            //Otherwise delete all reviews by user
            if (input.CreationTime.HasValue)
                query = query.Where(review => review.CreationTime == input.CreationTime);

            foreach (Review review in query)
            {
                _reviewRepository.Delete(review);
            }
        }
    }
}
