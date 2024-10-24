'
' Created by SharpDevelop.
' User: Toshiba
' Date: 30-May-16
' Time: 8:06 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.ComponentModel
Public Class DataLabel
    Inherits ItemData
    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
		ItemType = "DataLabel"
        Drawshape = SType.Rectangulo
        Rec = New Rectangle(x, y, w, h)
        Drawgrados = 0
    End Sub
	Public Sub New(ByVal R As Rectangle)
		ItemType = "DataLabel"
        Drawshape = SType.Rectangulo
        Rec = R
        Drawgrados = 0
    End Sub
    '<System.ComponentModel.Category("Data")>
    '<Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))>
    '[System.ComponentModel.Browsable(False)]
    'Public System.Windows.Forms.ComboBox ComboBox { Get; }

    '<System.ComponentModel.Category("Datos")>
    'Public Property Datafield() As String
    '    Get
    '        Return Dfield
    '    End Get
    '    Set(ByVal Value As String)
    '        Dfield = Value
    '        Refresh()
    '    End Set
    'End Property

    '<System.ComponentModel.Category("Appearance")> _
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
    '<System.ComponentModel.Category("Appearance")> _
    <System.ComponentModel.Category("Apariencia")>
    Property FontColor() As color
		Get
			return fcolor
		End Get
		
		Set(ByVal value As color)
			fColor = value
			refresh()
		End Set
	End Property
    '<System.ComponentModel.Category("Appearance")> _
    <System.ComponentModel.Category("Apariencia")>
    Property TextAlignment() As ContentAlignment
		Get
			return txtalign
		End Get
		
		Set(ByVal value As ContentAlignment)
			txtalign = value
			refresh()
		End Set
	End Property
    '<System.ComponentModel.Category("Appearance")> _
    <System.ComponentModel.Category("Apariencia")>
    Property TextPadding() As Padding
        Get
            Return pd
        End Get

        Set(ByVal value As Padding)
            pd = value
            refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("Apariencia")>
    'Property vertical() As Boolean
    '    Get
    '        Return vh
    '    End Get

    '    Set(ByVal value As Boolean)
    '        vh = value
    '        Refresh()
    '    End Set
    'End Property

    <System.ComponentModel.Category("Apariencia")>
    Property derecha_a_izquierda() As Boolean
        Get
            Return di
        End Get

        Set(ByVal value As Boolean)
            di = value
            Refresh()
        End Set
    End Property
    <System.ComponentModel.Category("Apariencia")>
    Property angulo() As SGrados
        Get
            Return Drawgrados
        End Get

        Set(ByVal value As SGrados)
            Drawgrados = value
            Refresh()
        End Set
    End Property
    '<System.ComponentModel.Category("Apariencia")>
    'Property angulo() As Integer
    '    Get
    '        Return ang
    '    End Get

    '    Set(ByVal value As Integer)
    '        ang = value
    '        Refresh()
    '    End Set
    'End Property

    '<System.ComponentModel.Category("Behaviour")> _
    <System.ComponentModel.Category("Comportamiento")>
    Public Property ShrinkTextToFit as Boolean
        Get
            Return fitInBox
        End Get
        Set(ByVal Value As Boolean)
            fitInBox = Value
            refresh()
        End Set
    End Property

    '<System.ComponentModel.Category("Behaviour")> _
    <System.ComponentModel.Category("Comportamiento")>
    Public Property Format() As String
        Get
            Return fmt
        End Get
        Set(ByVal Value As String)
            fmt = Value
            refresh()
        End Set
    End Property

    <System.ComponentModel.Category("Comportamiento")>
    Property Truncar() As Boolean
        Get
            Return TruncarTexto
        End Get

        Set(ByVal value As Boolean)
            TruncarTexto = value
            Refresh()
        End Set
    End Property


End Class
