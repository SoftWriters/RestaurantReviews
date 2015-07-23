Imports System
Imports System.ServiceModel

#Region "Modification History"
' 07/22/2015 - rjg: interface to support a Restaurant service based on WCF

#End Region

<ServiceContract()>
Public Interface IRestaurantService

    <OperationContract()> _
    Function FetchByCity(city As String) As List(Of RestaurantDTO)

    <OperationContract()> _
    Function InsertRestaurant(restaurantDTO As RestaurantDTO) As Boolean

    <OperationContract()> _
    Function InsertRestaurantReview(restaurantId As Integer, userId As Integer, ratingId As Integer) As Boolean

    <OperationContract()> _
    Function FetchReviewsByUser() As List(Of RestaurantReviewDTO)

    <OperationContract()> _
    Function DeleteReview(reviewId As Integer) As Boolean

End Interface

