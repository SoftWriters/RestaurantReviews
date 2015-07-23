#Region "Modification History"
' 07/22/2015 - rjg: class created
'                   used to define a Restaurant Review data template

#End Region

''' <summary>
''' 07/22/2015 - rjg: used to define a Data Transfer Object (DTO) or set of aggregated data upon which a Restaurant Review is based
''' </summary>
''' <value></value>
''' <returns></returns>
''' <remarks></remarks>
Public Class RestaurantReviewDTO

#Region "Properties"
    Public Property ReviewID As Nullable(Of Integer)
    Public Property RestaurantName As String
    Public Property City As String
    Public Property UserName As String
    Public Property Rating As String
#End Region

End Class
