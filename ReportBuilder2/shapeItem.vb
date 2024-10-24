'
' Created by SharpDevelop.
' User: Toshiba
' Date: 01-Jun-16
' Time: 7:06 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Class shapeItem
	Inherits item
	Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
		ItemType = "Shape"
        Drawshape = SType.Rectangulo
        Rec = new Rectangle(x, y, w, h)
	End Sub



    Public Sub New(ByVal R As Rectangle)
		ItemType = "Shape"
        Drawshape = SType.Rectangulo
        Rec = R
	End Sub
    '<System.ComponentModel.Category("Appearance")> _
    <System.ComponentModel.Category("Apariencia")>
    Public Property Tipo() As SType
        'Public Property ShapeType() As SType
        Get
            Return Drawshape
        End Get
        Set(ByVal Value As SType)
            Drawshape = Value
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Apariencia")>
    Public Property Estilo() As line_style
        'Public Property ShapeType() As SType
        Get
            Return estilo_linea
        End Get
        Set(ByVal Value As line_style)
            estilo_linea = Value
            Refresh()
        End Set
    End Property


End Class
