using RestaurantReviews.Entities;
using RestaurantReviews.WebServices.Models;

namespace RestaurantReviews.WebServices.Helpers
{
    public static class Parser
    {
        public static Restaurant ModelToEntity(RestaurantModel model)
        {
            return
                new Restaurant
                {
                    Id = model.Id,
                    Name = model.Name,
                    Website = model.Website,
                    PhoneNumber = model.PhoneNumber,
                    RestaurantLocation = model.RestaurantLocation
                };
        }

        public static RestaurantModel EntityToModel(Restaurant entity)
        {
            return
                new RestaurantModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Website = entity.Website,
                    PhoneNumber = entity.PhoneNumber,
                    RestaurantLocation = entity.RestaurantLocation
                };
        }

        public static Review ModelToEntity(ReviewModel model)
        {
            return
                new Review
                {
                    Id = model.Id,
                    RestaurantId = model.RestaurantId,
                    UserId = model.UserId,
                    FoodGrade = model.FoodGrade,
                    ServiceGrade = model.ServiceGrade,
                    LookFeelGrade = model.LookFeelGrade,
                    Text = model.Text
                };
        }

        public static ReviewModel EntityToModel(Review entity)
        {
            return
                new ReviewModel
                {
                    Id = entity.Id,
                    RestaurantId = entity.RestaurantId,
                    UserId = entity.UserId,
                    FoodGrade = entity.FoodGrade,
                    ServiceGrade = entity.ServiceGrade,
                    LookFeelGrade = entity.LookFeelGrade,
                    Text = entity.Text,
                    Restaurant = Parser.EntityToModel(entity.Restaurant),
                    User = Parser.EntityToModel(entity.User),
                    DateCreated = entity.DateCreated
                };
        }

        public static User ModelToEntity(UserModel model)
        {
            return
                new User
                {
                    Id = model.Id,
                    Username = model.Username,
                    Email = model.Email
                };
        }

        public static UserModel EntityToModel(User entity)
        {
            return
                new UserModel
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email
                };
        }
    }
}