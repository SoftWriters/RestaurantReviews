using RestaurantReviews.ApiModels.ApiModelsV1;
using RestaurantReviews.BLL.ModelsV1;
using RestaurantReviews.EFRepository.Entities;
using RetaurantReviews.BLL.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.BLL.Managers
{
    public class RestaurantReviewsV1Manager
    {
        public ResponseBllModel PostRestaurant(CreateRestaurantApiModel createRestaurantApiModel, out int newRestaurantApiId)
        {
            newRestaurantApiId = 0;

            try
            {
                //Validation checks:
                string parameterName;
                if (createRestaurantApiModel == null)
                {                    
                    parameterName = "createRestaurantApiModel";                    
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.Name))
                {
                    parameterName = "createRestaurantApiModel.Name";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.AddressLine1))
                {
                    parameterName = "createRestaurantApiModel.AddressLine1";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.City))
                {
                    parameterName = "createRestaurantApiModel.City";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.PostalCode))
                {
                    parameterName = "createRestaurantApiModel.PostalCode";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.Country))
                {
                    parameterName = "createRestaurantApiModel.Country";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }

                //Now if the following 2 parameters are Null or whitespace, we then always store string.Empty instead (NOT NULL DB columns)
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.AddressLine2))
                {
                    createRestaurantApiModel.AddressLine2 = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(createRestaurantApiModel.StateProvince))
                {
                    createRestaurantApiModel.StateProvince = string.Empty;
                }

                using (var dbContext = new RestaurantReviewsDbEntities())
                {
                    //Validation check for a restaurant with duplicate address.
                    var duplicateRestaurant = (from r in dbContext.Restaurants
                                               where r.AddressLine1 == createRestaurantApiModel.AddressLine1
                                                  && r.AddressLine2 == createRestaurantApiModel.AddressLine2
                                                  && r.City == createRestaurantApiModel.City
                                                  && r.StateProvince == createRestaurantApiModel.StateProvince
                                                  && r.PostalCode == createRestaurantApiModel.PostalCode
                                                  && r.Country == createRestaurantApiModel.Country
                                               select new
                                               {
                                                   r.SystemId,
                                                   r.Name
                                               }).FirstOrDefault();
                    if (duplicateRestaurant != null)
                    {
                        return new ResponseBllModel
                        {
                            Result = ResultEnum.DuplicateRestaurantAddress,
                            ErrorMessage = string.Format("This address is already used by this restaurant: RestaurantApiId: {0}  Name:'{1}'",
                                                         duplicateRestaurant.SystemId, duplicateRestaurant.Name)
                        };
                    }

                    Restaurant newRestaurant = ModelMapper.GetEntity(createRestaurantApiModel);
                    dbContext.Restaurants.Add(newRestaurant);
                    dbContext.SaveChanges();

                    newRestaurantApiId = newRestaurant.SystemId;
                }

                return new ResponseBllModel
                {
                    Result = ResultEnum.Success
                };
            }
            catch (Exception ex)
            {
                return GetUnexpectedErrorResponseBllModel(ex);
            }
        }


        public ResponseBllModel SearchRestaurants(string city, string stateProvince, out IList<RestaurantApiModel> restaurantApiModels)
        {
            restaurantApiModels = new List<RestaurantApiModel>();

            try
            {
                //Validation checks:
                //We require at least the "city" search parameter.
                string parameterName;
                if (string.IsNullOrWhiteSpace(city))
                {
                    parameterName = "city";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }

                Restaurant[] restaurants;
                using (var dbContext = new RestaurantReviewsDbEntities())
                {
                    var restaurantsQueryable = from r in dbContext.Restaurants
                                               where r.City == city
                                               select r;
                    if (!string.IsNullOrWhiteSpace(stateProvince))
                    {
                        restaurantsQueryable = restaurantsQueryable.Where(r => r.StateProvince == stateProvince);
                    }

                    restaurants = restaurantsQueryable.ToArray();
                }

                foreach (Restaurant restaurant in restaurants)
                {
                    restaurantApiModels.Add(ModelMapper.GetApiModel(restaurant));
                }
                
                return new ResponseBllModel
                {
                    Result = ResultEnum.Success
                };
            }
            catch (Exception ex)
            {
                return GetUnexpectedErrorResponseBllModel(ex);
            }
        }


        public ResponseBllModel PostRestaurantReview(CreateRestaurantReviewApiModel createRestaurantReviewApiModel, out int newRestaurantReviewApiId)
        {
            newRestaurantReviewApiId = 0;

            try
            {
                //Validation checks:
                string parameterName;
                if (createRestaurantReviewApiModel == null)
                {
                    parameterName = "createRestaurantReviewApiModel";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (createRestaurantReviewApiModel.RestaurantApiId < 1)
                {
                    parameterName = "createRestaurantReviewApiModel.RestaurantApiId";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (string.IsNullOrWhiteSpace(createRestaurantReviewApiModel.ReviewerEmail))
                {
                    parameterName = "createRestaurantReviewApiModel.ReviewerEmail";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }
                if (createRestaurantReviewApiModel.NumberOfStars < 1 || createRestaurantReviewApiModel.NumberOfStars > 5)
                {
                    parameterName = "createRestaurantReviewApiModel.NumberOfStars";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.InvalidParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetInvalidParameterErrorMessage(parameterName, "Its valid values can only be 1 through 5.")
                    };
                }
                //The Text field is optional.

                using (var dbContext = new RestaurantReviewsDbEntities())
                {
                    //Validation check for the RestaurantApiId.
                    var foundRestaurant = (from r in dbContext.Restaurants
                                           where r.SystemId == createRestaurantReviewApiModel.RestaurantApiId
                                           select new
                                           {
                                               r.SystemId
                                           }).FirstOrDefault();
                    if (foundRestaurant == null)
                    {
                        parameterName = "createRestaurantReviewApiModel.RestaurantApiId";
                        return new ResponseBllModel
                        {
                            Result = ResultEnum.InvalidParameter,
                            ParameterName = parameterName,
                            ErrorMessage = MessageHelper.GetInvalidParameterErrorMessage(parameterName, string.Concat("The invalid value is: ", createRestaurantReviewApiModel.RestaurantApiId))
                        };
                    }

                    Reviewer reviewer = (from rv in dbContext.Reviewers
                                         where rv.Email == createRestaurantReviewApiModel.ReviewerEmail
                                         select rv).FirstOrDefault();

                    DateTime utcNow = DateTime.UtcNow;
                    if (reviewer == null)
                    {
                        reviewer = new Reviewer
                        {
                            Email = createRestaurantReviewApiModel.ReviewerEmail,
                            CreatedDate = utcNow,
                            ModifiedDate = utcNow
                        };

                        dbContext.Reviewers.Add(reviewer);
                    }

                    var restaurantReview = new RestaurantReview
                    {
                        RestaurantSystemId = createRestaurantReviewApiModel.RestaurantApiId,
                        ReviewerSystemId = reviewer.SystemId,
                        ReviewDate = utcNow,
                        NumberOfStars = createRestaurantReviewApiModel.NumberOfStars,
                        Text = createRestaurantReviewApiModel.Text,
                        CreatedDate = utcNow,
                        ModifiedDate = utcNow
                    };

                    dbContext.RestaurantReviews.Add(restaurantReview);
                    dbContext.SaveChanges();

                    newRestaurantReviewApiId = restaurantReview.SystemId;
                }

                return new ResponseBllModel
                {
                    Result = ResultEnum.Success
                };
            }
            catch (Exception ex)
            {
                return GetUnexpectedErrorResponseBllModel(ex);
            }
        }


        public ResponseBllModel SearchRestaurantReviews(string reviewerEmail, out IList<RestaurantReviewApiModel> restaurantReviewApiModels)
        {
            restaurantReviewApiModels = new List<RestaurantReviewApiModel>();

            try
            {
                //Validation checks:
                string parameterName;
                if (string.IsNullOrWhiteSpace(reviewerEmail))
                {
                    parameterName = "reviewerEmail";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }

                RestaurantReview[] restaurantReviews;
                using (var dbContext = new RestaurantReviewsDbEntities())
                {
                    restaurantReviews = (from rrv in dbContext.RestaurantReviews
                                         where rrv.Reviewer.Email == reviewerEmail
                                         select rrv).ToArray();
                }

                foreach (RestaurantReview restaurantReview in restaurantReviews)
                {
                    restaurantReviewApiModels.Add(ModelMapper.GetApiModel(restaurantReview));
                }

                return new ResponseBllModel
                {
                    Result = ResultEnum.Success
                };
            }
            catch (Exception ex)
            {
                return GetUnexpectedErrorResponseBllModel(ex);
            }
        }


        public ResponseBllModel DeleteRestaurantReview(int restaurantReviewApiId, out bool success)
        {
            success = false;

            try
            {
                //Validation checks:
                string parameterName;
                if (restaurantReviewApiId < 1)
                {
                    parameterName = "restaurantReviewApiId";
                    return new ResponseBllModel
                    {
                        Result = ResultEnum.MissingParameter,
                        ParameterName = parameterName,
                        ErrorMessage = MessageHelper.GetMissingParameterErrorMessage(parameterName)
                    };
                }

                using (var dbContext = new RestaurantReviewsDbEntities())
                {
                    //Validation check for the restaurantReviewApiId.
                    var restaurantReview = (from rrv in dbContext.RestaurantReviews
                                            where rrv.SystemId == restaurantReviewApiId
                                            select rrv).FirstOrDefault();
                    if (restaurantReview == null)
                    {
                        parameterName = "restaurantReviewApiId";
                        return new ResponseBllModel
                        {
                            Result = ResultEnum.InvalidParameter,
                            ParameterName = parameterName,
                            ErrorMessage = MessageHelper.GetInvalidParameterErrorMessage(parameterName, string.Concat("The invalid value is: ", restaurantReviewApiId))
                        };
                    }

                    dbContext.RestaurantReviews.Remove(restaurantReview);
                    dbContext.SaveChanges();
                    success = true;
                }

                return new ResponseBllModel
                {
                    Result = ResultEnum.Success
                };
            }
            catch (Exception ex)
            {
                return GetUnexpectedErrorResponseBllModel(ex);
            }
        }


        private ResponseBllModel GetUnexpectedErrorResponseBllModel(Exception exception)
        {
            string innerErrorMessage;
            string innerErrorStackTrace;
            if (exception.InnerException != null)
            {
                innerErrorMessage = exception.InnerException.Message;
                innerErrorStackTrace = exception.InnerException.StackTrace;
            }
            else
            {
                innerErrorMessage = null;
                innerErrorStackTrace = null;
            }
            return new ResponseBllModel
            {
                Result = ResultEnum.UnexpectedError,
                ErrorMessage = MessageHelper.GetInternalServerErrorMessage(string.Concat("Error: ", exception.Message)),
                ErrorStackTrace = exception.StackTrace,
                InnerErrorMessage = innerErrorMessage,
                InnerErrorStackTrace = innerErrorStackTrace
            };
        }
    }
}