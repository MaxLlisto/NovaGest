'
' Created by SharpDevelop.
' User: Toshiba
' Date: 12-Jun-16
' Time: 8:10 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing.Design

Public Class LabelItem
    Inherits item

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal w As Integer, ByVal h As Integer)
        ItemType = "Label"
        nm = "Label"
        Rec = New Rectangle(x, y, w, h)
        Drawgrados = SGrados._0_grados

    End Sub

    Public Sub New()
        ItemType = "Label"
        nm = "Label"
        Rec = New Rectangle(10, 10, 50, 50)
        Drawgrados = SGrados._0_grados
    End Sub

    Public Sub New(ByVal R As Rectangle)
        ItemType = "Label"
        nm = "Label"
        Rec = R
        Drawgrados = SGrados._0_grados
    End Sub

    Public Sub New(ByVal x As Integer, ByVal y As Integer)
        ItemType = "Label"
        nm = "Label"
        rec = New Rectangle(x, y, 50, 50)
    End Sub

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
        Get
            Return pd
        End Get

        Set(ByVal value As Padding)
            pd = value
            Refresh()
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
    'Property Truncar() As Boolean
    '    Get
    '        Return TruncarTexto
    '    End Get

    '    Set(ByVal value As Boolean)
    '        TruncarTexto = value
    '        Refresh()
    '    End Set
    'End Property

End Class
