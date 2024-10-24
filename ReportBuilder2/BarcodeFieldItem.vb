Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing.Design

Public Class BarcodeFieldItem
    Inherits ItemData

    Public Sub New()
        ItemType = "Fbarcode"
        nm = "Fbarcode"
        Drawbarcode = SBarcode.Barcode39
        Rec = New Rectangle(10, 10, 50, 50)
    End Sub

    Public Sub New(ByVal R As Rectangle)
        ItemType = "Fbarcode"
        nm = "Fbarcode"
        Drawbarcode = SBarcode.Barcode39
        Rec = R
    End Sub

    Public Sub New(ByVal x As Integer, ByVal y As Integer)
        ItemType = "Fbarcode"
        nm = "Fbarcode"
        Drawbarcode = SBarcode.Barcode39
        Rec = New Rectangle(x, y, 50, 50)
    End Sub

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
        ItemType = "Fbarcode"
        Drawbarcode = SBarcode.Barcode39
        Rec = New Rectangle(x, y, w, h)
    End Sub

    <System.ComponentModel.Category("Diseño")>
    Public Property Ver_texto() As Boolean
        Get
            Return mt
        End Get
        Set(ByVal Value As Boolean)
            mt = Value
            Refresh()
        End Set
    End Property


    <System.ComponentModel.Category("Diseño")>
    Public Property Digito_control() As Boolean
        Get
            Return dc
        End Get
        Set(ByVal Value As Boolean)
            dc = Value
            Refresh()
        End Set
    End Property


    <System.ComponentModel.Category("Apariencia")>
    Public Property Tipo() As SBarcode
        Get
            Return Drawbarcode
        End Get
        Set(ByVal Value As SBarcode)
            Drawbarcode = Value
            Refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("Datos")>
    '<Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))>
    'Public Property Datafield() As String
    '    Get
    '        Return Dfield
    '    End Get
    '    Set(ByVal Value As String)
    '        Dfield = Value
    '        Refresh()
    '    End Set
    'End Property


    <System.ComponentModel.Category("Apariencia")>
    <Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))>
    Public Property Texto() As String
        Get
            Return txt
        End Get
        Set(ByVal Value As String)
            txt = Value
            Refresh()
        End Set
    End Property
    <System.ComponentModel.Category("Apariencia")>
    Property Font() As Font
        Get
            Return fnt
        End Get

        Set(ByVal value As Font)
            fnt = value
            Refresh()
        End Set
    End Property
    <System.ComponentModel.Category("Apariencia")>
    Property FontColor() As Color
        Get
            Return fcolor
        End Get

        Set(ByVal value As Color)
            fcolor = value
            Refresh()
        End Set
    End Property
    <System.ComponentModel.Category("Apariencia")>
    Property Alineación() As ContentAlignment
        'Property TextAlignment() As ContentAlignment
        Get
            Return txtalign
        End Get

        Set(ByVal value As ContentAlignment)
            txtalign = value
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Apariencia")>
    Property Relleno() As Padding
        'Property TextPadding() As Padding
        Get
            Return pd
        End Get

        Set(ByVal value As Padding)
            pd = value
            Refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Comportamiento")>
    Public Property Ajustar() As Boolean
        'Public Property ShrinkTextToFit As Boolean
        Get
            Return fitInBox
        End Get
        Set(ByVal Value As Boolean)
            fitInBox = Value
            Refresh()
        End Set
    End Property
End Class
