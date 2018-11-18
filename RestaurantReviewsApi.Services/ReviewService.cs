using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Data.Interfaces;
using RestaurantReviews.Model;

namespace RestaurantReviews.Services
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Defines the contract for the ReviewService class </summary>
    ///
    /// <remarks>   This could be put in it's an file but I left it here for convenience</remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface IReviewService
    {
        List<Review> GetReviews(string name = null);
        Review GetReviewById(Guid id);
        List<Review> GetReviewsByUser(string username);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
        void SaveChanges();
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> ReviewService Constructor </summary>
    ///
    /// <param name="reviewsRepository"> An instance of a review repository. The Web ApI 2 framework 
    ///                                  uses dependency injection to supply the instance bewcause it was 
    ///                                  registered in the ConfigureServices method of the Startup class.
    ///                                  </param>
    ///-------------------------------------------------------------------------------------------------

    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewsRepository;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> RestaurantService Constructor </summary>
        ///
        /// <param name="reviewsRepository"> An instance of a restaurant repository. The framework uses
        ///                                  dependency injection to supply the instance bewcause
        ///                                  it was registered in the ConfigureServices method of the
        ///                                  Startup class.</param>
        ///-------------------------------------------------------------------------------------------------

        public ReviewService(IReviewRepository reviewsRepository)
        {
            this._reviewsRepository = reviewsRepository;
        }

        #region IReviewService Members

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets all reviews in the data store. </summary>
        ///
        /// <param name="username"> (Optional) The name of a single restaurant. </param>
        /// 
        /// <returns>   all reviews as a List of Review objects. </returns>
        ///-------------------------------------------------------------------------------------------------

        public List<Review> GetReviews(string username = null)
        {
            if (string.IsNullOrEmpty(username))
                return _reviewsRepository.GetAll().ToList();
            else
                return GetReviewsByUser(username).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a review as specified by a GUID identifier. </summary>
        ///
        /// <param name="id">   A Guid identifier. </param>
        ///
        /// <returns>   The review identified by the argument. </returns>
        /// 
        /// <remarks> An endpoint using this method is unused in this exercise.</remarks>
        ///-------------------------------------------------------------------------------------------------

        public Review GetReviewById(Guid id)
        {
            var review = _reviewsRepository.GetById(id);
            return review;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets reviews by user. </summary>
        ///
        /// <param name="username"> The username whose reviews we'll search for. </param>
        ///
        /// <returns>   The reviews written by a given user. </returns>
        ///-------------------------------------------------------------------------------------------------

        public List<Review> GetReviewsByUser(string username)
        {
            List<Review> reviews = _reviewsRepository.GetReviewsByUser(username).ToList();
            return reviews;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates a review object and adds it to the data store. </summary>
        ///
        /// <param name="review">   The review object from which the restaurant will be created.</param>
        ///-------------------------------------------------------------------------------------------------

        public void CreateReview(Review review)
        {
            _reviewsRepository.Add(review);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Updates the review in the data store described by the restaurant object. </summary>
        ///
        /// <param name="review">   The review object from which the restaurant will be updated.</param>
        ///-------------------------------------------------------------------------------------------------

        public void UpdateReview(Review review)
        {
            _reviewsRepository.Update(review);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Deletes the review in the data store  described by the restaurant object. </summary>
        ///
        /// <param name="review">   The review object that will be deleted from a data store. </param>
        ///-------------------------------------------------------------------------------------------------

        public void DeleteReview(Review review)
        {
            _reviewsRepository.Delete(review);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves the changes that we make when creating or updating a database. </summary>
        ///-------------------------------------------------------------------------------------------------

        public void SaveChanges()
        {
            _reviewsRepository.Save();
        }
        #endregion
    }
}