using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.Business.Managers
{
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IModelValidator<IReview> _reviewValidator;

        public ReviewManager(IReviewRepository reviewRepository, IUserRepository userRepository, IModelValidator<IReview> reviewValidator)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _reviewValidator = reviewValidator;
        }

        public ICollection<IReview> GetAll()
        {
            return _reviewRepository.GetAll();
        }

        public IReview GetById(long id)
        {
            return _reviewRepository.GetById(id);
        }

        public ICollection<IReview> GetByUserId(int userId)
        {
            if (_userRepository.GetById(userId) == null)
                throw new ArgumentException($"UserId {userId} does not exist");

            return _reviewRepository.GetByUserId(userId);
        }

        public void Create(IReview review)
        {
            if (review == null)
                throw new ArgumentNullException("review");

            var errors = _reviewValidator.Validate(review);
            if (errors?.Any() == true)
                throw new Exception(string.Join(Environment.NewLine, errors));

            _reviewRepository.Create(review);
        }
    }
}
