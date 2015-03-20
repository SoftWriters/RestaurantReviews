using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CustomerReview.ContractTypes;

namespace CustomerReview
{
    [ServiceContract]
    public interface IRestaurantReview
    {
        [OperationContract]
        [WebGet]
        GetRestaurantResponseType GetRestaurant(GetRestaurantRequestType getRestaurantRequest);

        [OperationContract]
        [WebInvoke]
        AddRestaurantResponseType AddRestaurant(AddRestaurantRequestType addRestaurantRequest);

        [OperationContract]
        [WebInvoke]
        AddRestaurantReviewResponseType AddRestaurantReview(AddRestaurantReviewRequestType addRestaurantReviewRequest);

        [OperationContract]
        [WebGet]
        GetRestaurantReviewResponseType GetRestaurantReview(GetRestaurantReviewRequestType getRestaurantReviewRequest);

        [OperationContract]
        [WebInvoke]
        DeleteRestaurantReviewResponseType DeleteRestaurantReview(DeleteRestaurantReviewRequestType deleteRestaurantReviewRequest);
    }
}
