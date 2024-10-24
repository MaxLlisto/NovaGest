'
' Created by SharpDevelop.
' User: Toshiba
' Date: 12-Jun-16
' Time: 8:07 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Class Item
    Public txt As String = ""
    Public txtalign As ContentAlignment = ContentAlignment.TopLeft
    Public vh As Boolean = False
    Public di As Boolean = False
    Public fnt As Font = New Font("Arial", 8)
    Public fcolor As Color = color.Black
    Public bw As Integer = 1
    Public Rec As Rectangle
    Public selected As Boolean = False
    Public bcolor As Color = color.Transparent
    Public linecolor As Color = Color.Black ' Color.LightGray
    Public BorderRad As Integer = 0
    Public nm As String = ""
    Public ItemType As String = "Label"
    Public img As Image
    Public imgFile As String = ""
    Public Dfield As String = ""
    Public DIndex As Integer
    Public Drawshape As SType
    Public Drawbarcode As SBarcode
    Public Drawgrados As SGrados
    Public DrawImagen As SImagenes
    Public resizing As Integer = 0
    Public _h As Integer = 0
    Public RecSides As AnchorStyles = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Bottom
    Public pd As Padding
    Public fitInBox As Boolean = False
    Public TruncarTexto As Boolean = False
    Public fmt As String = ""
    Public dc As Boolean = True
    Public mt As Boolean = True
    Public ang As Integer
    Public estilo_linea As line_style = 0
    Public codimg As String = ""
    Public no As Integer
    Public secc As Section
    Public tipo_imagen As String
    Public NoRefrescar As Boolean = False

    'Dim unit As Integer = 1

    'Public Enum SType
    '    Rectangle = 0
    '    Ellipse = 1
    '    HorizontalLine = 2
    '    VerticalLine = 3
    'End Enum

    Public Enum SType
        Rectangulo = 0
        Elipse = 1
        Linea_horizontal = 2
        Linea_vertical = 3
        Linea_libre = 4
    End Enum

    Public Enum SImagenes
        Path = 1
        Binario = 2
        Codigo = 3
    End Enum


    Public Enum SBarcode
        Barcode39 = 0
        Barcode128 = 1
        BarcodeEAN13 = 2
        BarcodeQR = 3
    End Enum

    Public Enum SGrados
        _0_grados = 0
        _90_grados = 90
        _180_grados = 180
        _270_grados = 270
    End Enum

    Public Enum line_style
        Solido = System.Drawing.Drawing2D.DashStyle.Solid
        Guiones = System.Drawing.Drawing2D.DashStyle.Dash
        Puntos = System.Drawing.Drawing2D.DashStyle.Dot
        Guion_punto = System.Drawing.Drawing2D.DashStyle.DashDot
        Guion_2_puntos = System.Drawing.Drawing2D.DashStyle.DashDotDot
    End Enum

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
        rec = New Rectangle(x, y, w, h)
        pd.All = 0
    End Sub
    Public Sub New()
        rec = New Rectangle(10, 0, 20, 15)
        pd.All = 0
    End Sub
    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer, ByVal oSec As Section)
        Rec = New Rectangle(x, y, w, h)
        pd.All = 0
        secc = oSec
    End Sub


    <System.ComponentModel.Category("Diseño")>
    Public ReadOnly Property Nombre() As String
        Get
            Return nm
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return nm
        End Get
    End Property

    Public Property NumeroItem() As Integer
        Get
            Return no
        End Get
        Set(ByVal Value As Integer)
            no = Value
        End Set
    End Property


    <System.ComponentModel.Category("Localización")>
    Public Property X() As Decimal
        Get
            Return Rec.X / unit
        End Get
        Set(ByVal Value As Decimal)
            Rec.X = Value * unit
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Localización")>
    Public Property Y() As Decimal
        Get
            Return Rec.Y / unit
        End Get
        Set(ByVal Value As Decimal)
            Rec.Y = Value * unit
            Refresh()
        End Set
    End Property

    Public Property Height() As Decimal
        Get
            Return Rec.Height / unit
        End Get
        Set(ByVal Value As Decimal)
            Rec.Height = Value * unit
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Tamaño")>
    Public Property Alto() As Decimal
        'Public Property Height() As Decimal
        Get
            Return Rec.Height / unit
        End Get
        Set(ByVal Value As Decimal)
            Rec.Height = Value * unit
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Tamaño")>
    Public Property Ancho() As Decimal
        Get
            Return Rec.Width / unit
        End Get
        Set(ByVal Value As Decimal)
            Rec.Width = Value * unit
            Refresh()
        End Set
    End Property

    Public Property Width() As Decimal
        Get
            Return Rec.Width / unit
        End Get
        Set(ByVal Value As Decimal)
            Rec.Width = Value * unit
            Refresh()
        End Set
    End Property


    <System.ComponentModel.Category("Apariencia")>
    Public Property Color_fondo() As Color
        Get
            Return bcolor
        End Get
        Set(ByVal Value As Color)
            bcolor = Value
            Refresh()
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return bcolor
        End Get
        Set(ByVal Value As Color)
            bcolor = Value
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Apariencia")>
    Public Property Color_del_borde() As Color
        Get
            Return linecolor
        End Get
        Set(ByVal Value As Color)
            linecolor = Value
            Refresh()
        End Set
    End Property

    Public Property BorderColor() As Color
        Get
            Return linecolor
        End Get
        Set(ByVal Value As Color)
            linecolor = Value
            Refresh()
        End Set
    End Property


    <System.ComponentModel.Category("Apariencia")>
    Public Property Lados() As AnchorStyles
        Get
            Return RecSides
        End Get
        Set(ByVal Value As AnchorStyles)
            RecSides = Value
            Refresh()
        End Set
    End Property

    Public Property BorderSides() As AnchorStyles
        Get
            Return RecSides
        End Get
        Set(ByVal Value As AnchorStyles)
            RecSides = Value
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Apariencia"), System.ComponentModel.Description("Del borde de la forma")>
    Public Property Ancho_del_borde() As Decimal
        Get
            Return bw / unit
        End Get
        Set(ByVal Value As Decimal)
            bw = Value * unit
            Refresh()
        End Set
    End Property

    Public Property BorderWidth() As Decimal
        Get
            Return bw / unit
        End Get
        Set(ByVal Value As Decimal)
            bw = Value * unit
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Apariencia")>
    Public Property Radio_borde() As Decimal
        Get
            Return BorderRad / unit
        End Get
        Set(ByVal Value As Decimal)
            BorderRad = Value * unit
            Refresh()
        End Set
    End Property



    Public Property tipo() As SType
        Get
            Return Drawshape
        End Get
        Set(ByVal Value As SType)
            Drawshape = Value
        End Set

    End Property


    Public Property BorderRadius() As Decimal
        Get
            Return BorderRad / unit
        End Get
        Set(ByVal Value As Decimal)
            BorderRad = Value * unit
            Refresh()
        End Set
    End Property

    Public Sub Refresh()

        If NoRefrescar Then
            Return
        End If

        If Not _DF.Norefrescar Then
            _DF.Reshape()
            _DF.Done()
        End If

    End Sub

    Public Function clone() As item
        Return DirectCast(Me.MemberwiseClone(), item)
    End Function

End Class

Public Class ItemData
    Inherits LabelItem

    Public Sub New()
        MyBase.New()
    End Sub

    <System.ComponentModel.Category("Datos"), System.ComponentModel.TypeConverter(GetType(ComboVariables)), System.ComponentModel.Description("Selecciona un campo nuevo")>
    Public Property DataField() As String
        Get
            Return Dfield
        End Get
        Set(ByVal Value As String)
            Dfield = Value
        End Set
    End Property

    <System.ComponentModel.Category("Comportamiento")>
    Public Property Ajustar() As Boolean
        Get
            Return fitInBox
        End Get
        Set(ByVal Value As Boolean)
            fitInBox = Value
            Refresh()
        End Set
    End Property


End Class
