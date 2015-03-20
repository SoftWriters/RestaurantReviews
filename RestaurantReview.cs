using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CustomerReview.ContractTypes;

namespace CustomerReview
{
    /// <summary>
    /// Host-independent WCF service RestaurantReview
    /// </summary>
    public class RestaurantReview : IRestaurantReview
    {
        /// <summary>
        /// Queries for a collection of restaurants based on the search criteria provided.
        /// </summary>
        /// <param name="getRestaurantRequest">Includes the search criteria for Restaurant Name, Address parts (City, State, Zip, etc.) and PhoneNumber.</param>
        /// <returns>A Collection of restaurants that meet the provided search criteria.</returns>
        public GetRestaurantResponseType GetRestaurant(GetRestaurantRequestType getRestaurantRequest)
        {
            GetRestaurantResponseType getRestaurantResponseType = new GetRestaurantResponseType();
            if (getRestaurantRequest != null)
            {
                //assume this would utilize a common data access framework and will assume working business objects RestaurantQueryObj, RestaurantBusObj and RestaurantBusObjCollection
                RestaurantQueryObj restaurantQueryObj = new RestaurantQueryObj();

                restaurantQueryObj.Name = getRestaurantRequest.RestaurantSearch.NameSearch;
                restaurantQueryObj.RestaurantId = getRestaurantRequest.RestaurantSearch.RestaurantId;
                restaurantQueryObj.PhoneNumber = getRestaurantRequest.RestaurantSearch.PhoneSearch;
                restaurantQueryObj.Address = new AddressBusObj();
                restaurantQueryObj.Address.AddressId = getRestaurantRequest.RestaurantSearch.AddressSearch.AddressId;
                restaurantQueryObj.Address.AddressLine1 = getRestaurantRequest.RestaurantSearch.AddressSearch.AddressLine1;
                restaurantQueryObj.Address.City = getRestaurantRequest.RestaurantSearch.AddressSearch.City;
                restaurantQueryObj.Address.State = getRestaurantRequest.RestaurantSearch.AddressSearch.State;
                restaurantQueryObj.Address.ZipCode = getRestaurantRequest.RestaurantSearch.AddressSearch.ZipCode;

                //load the data from the query object
                RestaurantCollection restaurants = restaurantQueryObj.LoadCollection();  //assume empty collection is returned if no results

                //translate returned results into the response object
                foreach (RestaurantBusObj restaurant in restaurants)
                {
                    RestaurantType restaurantType = new RestaurantType();

                    restaurantType.Name = restaurant.Name;
                    restaurantType.RestaurantId = restaurant.RestaurantId;
                    restaurantType.Address = new AddressType();
                    restaurantType.Address.AddressId = restaurant.Address.AddressId;
                    restaurantType.Address.AddressLine1 = restaurant.Address.AddressLine1;
                    restaurantType.Address.AddressLine2 = restaurant.Address.AddressLine2;
                    restaurantType.Address.City = restaurant.Address.City;
                    restaurantType.Address.State = restaurant.Address.State;
                    restaurantType.Address.ZipCode = restaurant.Address.ZipCode;
                    restaurantType.Phone = restaurant.PhoneNumber;

                    getRestaurantResponseType.Add(restaurantType);
                }
            }
            return getRestaurantResponseType;
        }

        /// <summary>
        /// Adds a restaurant.
        /// </summary>
        /// <param name="addRestaurantRequest">The restaurant object to add including name, address, and phone number.</param>
        /// <returns>A success or failure Acknowledgement.</returns>
        public AddRestaurantResponseType AddRestaurant(AddRestaurantRequestType addRestaurantRequest)
        {
            AddRestaurantResponseType addRestaurantResponseType = new AddRestaurantResponseType();
            if (addRestaurantRequest != null)
            {
                //this method would translate the request object into a RestaurantBusObj object and invoke a Save method to insert the data
                //assuming success:
                addRestaurantResponseType.Acknowledgement = new AcknowledgementType();
                addRestaurantResponseType.Acknowledgement.Status = "SUCCESS";
                addRestaurantResponseType.Acknowledgement.Message = "Restaurant was successfully added.";
            }
            return addRestaurantResponseType;
        }

        /// <summary>
        /// Adds a restaurant review.
        /// </summary>
        /// <param name="addRestaurantReviewRequest">The restaurant review to add including key restaurant data, the user, and the text message.</param>
        /// <returns>A success or failure Acknowledgement.</returns>
        public AddRestaurantReviewResponseType AddRestaurantReview(AddRestaurantReviewRequestType addRestaurantReviewRequest)
        {
            AddRestaurantReviewResponseType addRestaurantReviewResponseType = new AddRestaurantReviewResponseType();
            if (addRestaurantReviewRequest != null)
            {
                //this method would translate the request object into a RestaurantReviewBusObj (not implemented for this test) object and invoke a Save method to insert the data
                //assuming success:
                addRestaurantReviewResponseType.Acknowledgement = new AcknowledgementType();
                addRestaurantReviewResponseType.Acknowledgement.Status = "SUCCESS";
                addRestaurantReviewResponseType.Acknowledgement.Message = "Restaurant Review was successfully added.";
            }
            return addRestaurantReviewResponseType;
        }

        /// <summary>
        /// Queries for a collection of restaurant reviews based on the search criteria provided.
        /// </summary>
        /// <param name="getRestaurantReviewRequest">Includes the search criteria for the restaurant review including user, and other restaurant criteria.</param>
        /// <returns>A Collection of restaurant reviews that meet the provided search criteria.</returns>
        public GetRestaurantReviewResponseType GetRestaurantReview(GetRestaurantReviewRequestType getRestaurantReviewRequest)
        {
            GetRestaurantReviewResponseType getRestaurantReviewResponseType = new GetRestaurantReviewResponseType();
            if (getRestaurantReviewRequest != null)
            {
                //this method would return the reviews based on the search criteria provided in the input parameter
                //for testing purposes, add a response manually:
                RestaurantReviewType restaurantReview = new RestaurantReviewType();
                restaurantReview.Message = "This is the greatest diner in the world.";
                restaurantReview.UserId = "customer";
                restaurantReview.StarsCode = "FIVE";
                restaurantReview.RestaurantReviewed = new RestaurantType();
                restaurantReview.RestaurantReviewed.Name = "Corner Diner";
                restaurantReview.RestaurantReviewed.RestaurantId = 1;
                restaurantReview.RestaurantReviewed.Address = new AddressType();
                restaurantReview.RestaurantReviewed.Address.AddressId = 2;
                restaurantReview.RestaurantReviewed.Address.AddressLine1 = "100 Main Street";
                restaurantReview.RestaurantReviewed.Address.City = "Pittsburgh";
                restaurantReview.RestaurantReviewed.Address.State = "PA";
                restaurantReview.RestaurantReviewed.Address.ZipCode = "15228";
                restaurantReview.RestaurantReviewed.Phone = "4125551111";
                getRestaurantReviewResponseType.Add(restaurantReview);
            }
            return getRestaurantReviewResponseType;
        }

        /// <summary>
        /// Deletes a restaurant review.
        /// </summary>
        /// <param name="deleteRestaurantReviewRequest">The restaurant review to delete.</param>
        /// <returns>A success or failure Acknowledgement.</returns>
        public DeleteRestaurantReviewResponseType DeleteRestaurantReview(DeleteRestaurantReviewRequestType deleteRestaurantReviewRequest)
        {
            DeleteRestaurantReviewResponseType deleteRestaurantReviewResponseType = new DeleteRestaurantReviewResponseType();
            if (deleteRestaurantReviewRequest != null)
            {
                //this method would translate the request object into a RestaurantBusObj object and invoke a Delete method to delete the data (or mark inactive)
                //assuming success:
                deleteRestaurantReviewResponseType.Acknowledgement = new AcknowledgementType();
                deleteRestaurantReviewResponseType.Acknowledgement.Status = "SUCCESS";
                deleteRestaurantReviewResponseType.Acknowledgement.Message = "Restaurant Review was successfully deleted.";
            }
            return deleteRestaurantReviewResponseType;
        }
    }
}
