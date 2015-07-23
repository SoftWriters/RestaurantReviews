Imports System
Imports System.ServiceModel
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Text

#Region "Modification History"
' 07/22/2015 - rjg: class created

#End Region

''' <summary>
''' 07/22/2015 - rjg: class implements methods as part of a Windows Communication Foundation (WCF) service
''' </summary>
''' <remarks> all database operations in the service methods are designed to support a MS SQL Server database.
''' ADO.NET commands used with embedded SQL and can also be implemented as stored procedures as demonstrated
''' in method DeleteReview below
''' </remarks>
Public Class RestaurantService
    Implements IRestaurantService
    ''' <summary>
    ''' 07/22/2015 - rjg: Function retrieves a list of restaurants for a selected city
    ''' </summary>
    ''' <param name="city"></param>
    ''' <returns>list of restaurants</returns>
    ''' <remarks></remarks>
    Public Function FetchByCity(city As String) As List(Of RestaurantDTO) Implements IRestaurantService.FetchByCity

        Dim restaurantList As New List(Of RestaurantDTO)
 
        Using cn As SqlConnection = ConnectionManager.GetConnection()

            ' define command to retrieve list of restaurants
            Dim cmd As SqlCommand = New SqlCommand

            With cmd

                .CommandText = "select name, street, city, state, zipcode from restaurant where city = @prmCity"
                .CommandType = CommandType.Text
                .Parameters.Add("@prmCity", SqlDbType.Int).Value = city

            End With

            ' capture all restaurants for a specific city
            Using reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleResult Or CommandBehavior.CloseConnection)

                While (reader.Read())

                    ' create a restaurant object for each restaurant and add it to the list object
                    Dim restaurantDTO As New RestaurantDTO

                    With restaurantDTO

                        .RestaurantName = reader.GetString("name")
                        .Street = reader.GetString("street")
                        .City = reader.GetString("city")
                        .ZipCode = reader.GetString("zipcode")

                    End With

                    ' add each restaurant object to the list
                    restaurantList.Add(restaurantDTO)

                End While

            End Using

            Return restaurantList

        End Using

    End Function
    ''' <summary>
    ''' 07/22/2015 - rjg: Function to create a new restaurant
    ''' </summary>
    ''' <param name="restaurantDTO"></param>
    ''' <returns>boolean indicating success or failure upon creating new restaurant</returns>
    ''' <remarks></remarks>
    Public Function InsertRestaurant(restaurantDTO As RestaurantDTO) As Boolean Implements IRestaurantService.InsertRestaurant

        Dim sb As New StringBuilder
        Dim success As Boolean = False

        Dim cmd As SqlCommand = New SqlCommand

        If isNewRestaurant(restaurantDTO.RestaurantID) Then

            sb.Append("insert into Restaurant values (@prmRestaurantName, @prmStreet, @prmCity, @prmState, @prmZipcode)")

            Using cn As SqlConnection = ConnectionManager.GetConnection()

                With cmd

                    .CommandText = sb.ToString
                    .CommandType = CommandType.Text

                    .Parameters.Add("@prmRestaurantName", SqlDbType.Int).Value = restaurantDTO.RestaurantName
                    .Parameters.Add("@prmStreet", SqlDbType.Int).Value = restaurantDTO.Street
                    .Parameters.Add("@prmCity", SqlDbType.Int).Value = restaurantDTO.City
                    .Parameters.Add("@prmState", SqlDbType.Int).Value = restaurantDTO.State
                    .Parameters.Add("@prmZipcode", SqlDbType.Int).Value = restaurantDTO.ZipCode

                End With

                ' insert of restaurant assumes that RestaurantId is an "identity" column in table Restaurant and will be assigned upon row addition
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If (rowsAffected = 1) Then
                    success = True
                End If

            End Using

        End If

        Return success

    End Function
    ''' <summary>
    ''' 07/22/2015 - rjg: Function that creates a new restaurant review and customer rating for a specific restaurant
    ''' </summary>
    ''' <param name="restaurantId"></param>
    ''' <param name="userId"></param>
    ''' <param name="ratingId"></param>
    ''' <returns>boolean indicating success or failure upon creating new user review</returns>
    ''' <remarks></remarks>
    Public Function InsertRestaurantReview(restaurantId As Integer, userId As Integer, ratingId As Integer) As Boolean Implements IRestaurantService.InsertRestaurantReview

        Dim success As Boolean = False

        Using cn As SqlConnection = ConnectionManager.GetConnection()

            ' insert of restaurant assumes that ReviewId is an "identity" column in table UserReview and will be assigned upon row addition
            Dim cmd As SqlCommand = New SqlCommand("insert UserReview values (@prmRestaurantId, @prmUserId, @prmRatingId)", cn)

            With cmd

                .CommandType = CommandType.Text
                .Parameters.Add("@prmRestaurantId", SqlDbType.Int).Value = restaurantId
                .Parameters.Add("@prmUserId", SqlDbType.Int).Value = userId
                .Parameters.Add("@prmRatingId", SqlDbType.Int).Value = ratingId

            End With

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If (rowsAffected = 1) Then
                success = True
            End If

        End Using

        Return success

    End Function
    ''' <summary>
    ''' 07/22/2015 - rjg: Function retrieves a list of customer reviews ordered by customer and restaurant, respectively
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns>list of all customer reviews ordered by customer name and restaurant name</returns>
    ''' <remarks></remarks>
    Public Function FetchReviewsByUser() As List(Of RestaurantReviewDTO) Implements IRestaurantService.FetchReviewsByUser

        Dim restaurantReviewList As New List(Of RestaurantReviewDTO)

        Using cn As SqlConnection = ConnectionManager.GetConnection()

            ' define command to retrieve list of restaurant reviews
            Dim cmd As SqlCommand = New SqlCommand

            With cmd

                .Connection = cn
                .CommandText = "select r.restaurantname, r.city, u.username, rt.rating from Restaurant r, CustomerReview cr, Rating rt, User u " + _
                                " where cr.ratingId = rt.ratingId " + _
                                "   and cr.userId = u.userId " + _
                                "   and cr.restaurantId = r.restaurantId " + _
                                "   order by ur.username, r.restaurantname"

                .CommandType = CommandType.Text

            End With

            ' retrieve all reviews for a s
            Using reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleResult Or CommandBehavior.CloseConnection)

                While (reader.Read())

                    ' For each restaurant, create a restaurant object and add it to the list
                    Dim restaurantReviewDTO As New RestaurantReviewDTO

                    With restaurantReviewDTO

                        .RestaurantName = reader.GetString("restaurantname")
                        .City = reader.GetString("city")
                        .UserName = reader.GetString("username")
                        .Rating = reader.GetString("rating")

                    End With

                    restaurantReviewList.Add(restaurantReviewDTO)

                End While

            End Using

            Return restaurantReviewList

        End Using

    End Function
    ''' <summary>
    ''' 07/22/2015 - rjg: function to delete an individual customer review based on a selected ReviewId 
    ''' </summary>
    ''' <param name="reviewId"></param>
    ''' <returns>boolean indicating success or failure of operation to delete a customer review</returns>
    ''' <remarks>function expects the ReviewId of the customer review to be removed</remarks>
    Public Function DeleteReview(ByVal reviewId As Integer) As Boolean Implements IRestaurantService.DeleteReview

        Dim success As Boolean = False


        ' define command to delete single restaurant
        Using cmd As New SqlCommand("spDeleteCustomerReview", ConnectionManager.GetConnection())


            With cmd

                .CommandText = "delete from RestaurantReview where reviewId = @prmReviewId"
                .CommandType = CommandType.Text
                .Parameters.Add("@prmReviewId", SqlDbType.Int).Value = reviewId

            End With

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If (rowsAffected = 1) Then
                success = True
            End If

        End Using

        Return success

    End Function
    ''' <summary>
    ''' 07/22/2015 - rjg: function that detects existing Restaurant based on restaurant name
    ''' </summary>
    ''' <param name="restaurantName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function isNewRestaurant(restaurantName As String) As Boolean

        Dim success As Boolean = False
        Dim dbValue As Object

        Using cn As SqlConnection = ConnectionManager.GetConnection()

            ' define command to detect if restaurant already exists
            Dim cmd As SqlCommand = New SqlCommand()

            With cmd

                .CommandText = "select count(*) from Restaurant where restaurantname like '''%''' + @prmName + '''%''' "
                .CommandType = CommandType.Text
                .Parameters.Add("@prmName", SqlDbType.Int).Value = restaurantName

            End With

            dbValue = cmd.ExecuteScalar()

            If CInt(dbValue) = 0 Then
                success = True
            End If

        End Using

        Return success

    End Function

End Class
