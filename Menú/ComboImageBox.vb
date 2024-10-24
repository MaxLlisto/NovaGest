Imports System
Imports System.Windows.Forms
Imports System.Drawing

Namespace ComboImageBox
    Public Class ComboImageBox
        Inherits ComboBox

        Private ListPicture As New ImageList
        Private BrushFont As SolidBrush
        Private BrushBack As SolidBrush

        Public ComboFont As Font
        Public ComboItemText() As Object
        Public ComboItemIcon() As Integer
        Public ComboBackBrushes() As Color
        Public ComboFontBrushes() As Color
        Public ini As Integer = -1

        Public Property ImageList() As ImageList
            Get
                Return ListPicture
            End Get
            Set(ByVal ListaImagem As ImageList)
                ListPicture = ListaImagem
            End Set
        End Property

        Public Function ComboAddItem(ByVal ItemCombo As Object,
                                     ByVal ItemColor As Color,
                                     ByVal ItemIcono As Integer,
                                     ByVal ItemColorFondo As Color) As Boolean

            ini = ini + 1

            ReDim Preserve ComboItemText(ini)
            ReDim Preserve ComboFontBrushes(ini)
            ReDim Preserve ComboItemIcon(ini)
            ReDim Preserve ComboBackBrushes(ini)
            ComboItemText(ini) = ItemCombo
            ComboFontBrushes(ini) = ItemColor
            ComboItemIcon(ini) = ItemIcono
            ComboBackBrushes(ini) = ItemColorFondo

            load()
        End Function

        Private Sub load()
            On Error GoTo salir
            MyBase.Items.Clear()
            MyBase.Items.AddRange(ComboItemText)
            MyBase.DropDownStyle = ComboBoxStyle.DropDownList
            MyBase.DrawMode = DrawMode.OwnerDrawVariable
            MyBase.ItemHeight = Me.ListPicture.ImageSize.Height
            'MyBase.Width = Me.ListPicture.ImageSize.Width + 70
            MyBase.MaxDropDownItems = ini + 1
            MyBase.SelectedIndex = 0
salir:
            MyBase.Text = "Debe Cargar ImageList"
        End Sub



        Private Sub ComboBox_DrawItem(ByVal sender As Object,
        ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles MyBase.DrawItem
            If e.Index <> -1 Then
                BrushFont = New SolidBrush(ComboFontBrushes(e.Index))
                BrushBack = New SolidBrush(ComboBackBrushes(e.Index))
                Dim ancho As Integer = ListPicture.Images(ComboItemIcon(e.Index)).Width
                Dim alto As Integer = ListPicture.Images(ComboItemIcon(e.Index)).Height
                e.Graphics.FillRectangle(BrushBack, e.Bounds)
                'e.Graphics.DrawString(ComboItemText(e.Index), ComboFont, BrushFont, e.Bounds.Left + ancho + 2, e.Bounds.Top + alto / 4)
                'e.Graphics.DrawString(CStr(CallByName(ComboItemText(e.Index), Me.DisplayMember, Microsoft.VisualBasic.CallType.Method, Me.DisplayMember)), ComboFont, BrushFont, e.Bounds.Left + ancho + 2, e.Bounds.Top + alto / 4)
                e.Graphics.DrawString(ComboItemText(e.Index).itemtext, ComboFont, BrushFont, e.Bounds.Left + ancho + 2, e.Bounds.Top + alto / 4)
                e.Graphics.DrawImage(ListPicture.Images(ComboItemIcon(e.Index)),
                                     e.Bounds.Left, e.Bounds.Top)
            End If
        End Sub

        Private Sub ComboBox_MeasureItem(ByVal sender As Object,
        ByVal e As System.Windows.Forms.MeasureItemEventArgs) Handles MyBase.MeasureItem
            e.ItemHeight = ListPicture.ImageSize.Height
            e.ItemWidth = ListPicture.ImageSize.Width
        End Sub

    End Class

End Namespace
'============

Public Class ItemsMenu
    Private _itemtext As String
    Private _itemvalue As Integer
    Public Property ItemText() As String
        Get
            Return _itemtext
        End Get
        Set(ByVal value As String)
            _itemtext = value
        End Set
    End Property
    Public Property ItemValue() As Integer
        Get
            Return _itemvalue
        End Get
        Set(ByVal value As Integer)
            _itemvalue = value
        End Set
    End Property
    Public Sub New(ByVal displayText As String, ByVal value As Integer)
        _itemtext = displayText
        _itemvalue = value
    End Sub
    Public Overrides Function ToString() As String
        Return _itemvalue.ToString.Trim
    End Function

    'combobox.ComboAddItem(New ItemsMenu(Rs!texto_menu, Rs!numero_tw),Color.Red, 0, Color.Yellow)
    'combobox.DisplayMember = "ItemText"
    'combobox.ValueMember = "ItemValue"

End Class
