using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews
{
    public class CreateRestaurantInput : IInputDto
    {
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class CreateReviewInput : IInputDto
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class DeleteReviewInput : IInputDto
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int UserId { get; set; }

        public DateTime? CreationTime { get; set; }
    }

    public class GetRestaurantsInput : IInputDto
    {
        [Required]
        public int CityId { get; set; }
    }

    public class GetReviewsInput : IInputDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
