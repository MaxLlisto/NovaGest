'
' Created by SharpDevelop.
' User: Toshiba
' Date: 30-May-16
' Time: 8:06 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.ComponentModel
Public Class DataImage
    Inherits ItemData

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
		ItemType = "DataImage"
        Drawshape = SType.Rectangulo
        Rec = new Rectangle(x, y, w, h)
	End Sub
    Public Sub New(ByVal R As Rectangle)
        ItemType = "DataImage"
        Drawshape = SType.Rectangulo
        Rec = R
    End Sub

    <System.ComponentModel.Category("Fuente de la imagen")>
    Public Property Tipo_Imagen() As SImagenes
        Get
            Return Tipo_Imagen
        End Get
        Set(ByVal Value As SImagenes)
            Tipo_Imagen = Value
            Refresh()
        End Set
    End Property


End Class
