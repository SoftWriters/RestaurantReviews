
#Region "Modification History"
' 07/22/2015 - rjg: class created
'                   used to define a Restaurant data template

#End Region

''' <summary>
''' ''' 07/22/2015 - rjg: used to define a Data Transfer Object (DTO) or set of aggregated data upon which a Restaurant is based
''' </summary>
''' <remarks></remarks>
Public Class RestaurantDTO

#Region "Properties"
    Public Property RestaurantID As Nullable(Of Integer)
    Public Property RestaurantName As String
    Public Property Street As String
    Public Property City As String
    Public Property State As String
    Public Property ZipCode As String

#End Region

End Class
