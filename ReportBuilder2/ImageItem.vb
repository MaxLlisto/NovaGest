'
' Created by SharpDevelop.
' User: Toshiba
' Date: 30-May-16
' Time: 8:05 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Class ImageItem
	Inherits item
	Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
		ItemType = "Image"
        Drawshape = SType.Rectangulo
        Rec = New Rectangle(x, y, w, h)
		img = nothing
	End Sub
	
	Public Sub New(ByVal R As Rectangle)
		ItemType = "Image"
        Drawshape = SType.Rectangulo
        Rec = R
		img = nothing
	End Sub

    '<System.ComponentModel.Category("Image")> _
    <System.ComponentModel.Category("Imagen")>
    Public Property Image() As Image
        Get
            Return img
        End Get
        Set(ByVal Value As Image)
            img = Value
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Imagen")>
    Public Property codigo_imagen() As String
        Get
            Return codimg
        End Get
        Set(ByVal Value As String)
            codimg = Value
        End Set
    End Property




End Class
