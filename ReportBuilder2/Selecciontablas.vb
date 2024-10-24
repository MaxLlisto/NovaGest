Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.ComponentModel
Imports CustomListView
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Collections.Specialized

Public Class Selecciontablas
    Dim cTabla As String
    Dim nSec As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public datos As DataTable
    Public datosInf As DataTable

    Private Structure MyCell
        Dim Row As Integer
        Dim Col As Integer
    End Structure

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Campos_tabla.AllowDrop = True
        Campos_report.AllowDrop = True
        'cargartabla()

    End Sub

    Public Property og As Object
        Get
            Return ObjetoGlobal
        End Get
        Set(valor As Object)
            ObjetoGlobal = valor
        End Set
    End Property

    Public Property Tabla As String
        Get
            Return cTabla
        End Get
        Set(valor As String)
            cTabla = valor
        End Set
    End Property

    Public Property seccion As String
        Get
            Return nSec
        End Get
        Set(valor As String)
            nSec = valor
            lblSeccion.Text = nSec
        End Set

    End Property

    Public Sub cargartabla()
        Dim lvItem As ListViewItem
        Dim i As Integer
        Dim Midatarow() As DataRow
        Dim Midatos As String
        Dim nombre_tabla As String
        Dim Sql As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        Sql = "SELECT *  FROM sys.objects WHERE (type = 'U' OR type = 'V' ) ORDER BY NAME"

        rs.Open(Sql, ObjetoGlobal.cn)

        While Not rs.EOF
            If Not (Strings.Left(CStr(rs!name), 2) = "zz" Or Strings.Left(CStr(rs!name), 2) = "za" Or Strings.Left(CStr(rs!name), 2) = "zb" Or Strings.Left(CStr(rs!name), 2) = "zp" Or Strings.Left(CStr(rs!name), 2) = "zs" Or Strings.Left(CStr(rs!name), 2) = "ZZ" Or Strings.Left(CStr(rs!name), 1) = "X") Then
                lvItem = New ListViewItem(CStr(rs!name)) 'Campos_tabla.Items.Count + 1)
                lvItem.SubItems.Add(CStr(rs!name))
                Campos_tabla.Items.Add(lvItem)
            End If
            rs.MoveNext()
        End While

        Midatarow = datos.Select("seccion = '" & nSec & "'") 'aaa
        For Each MiData As DataRow In Midatarow
            Midatos = MiData(0)
            lvItem = New ListViewItem(Midatos)
            lvItem.SubItems.Add(MiData(0))
            Campos_report.Items.Add(lvItem)
            'Campos_report.Items.Add(lvItem)
        Next

    End Sub
    Private Sub ListView_ItemDrag(
        ByVal sender As Object, ByVal e As ItemDragEventArgs) _
        Handles Campos_tabla.ItemDrag, Campos_report.ItemDrag

        Dim myItem As ListViewItem
        Dim myItems(sender.SelectedItems.Count - 1) As ListViewItem
        Dim i As Integer = 0

        ' Loop though the SelectedItems collection for the source.
        For Each myItem In sender.SelectedItems
            ' Add the ListViewItem to the array of ListViewItems.
            myItems(i) = myItem
            i = i + 1
        Next
        ' Create a DataObject containg the array of ListViewItems.
        sender.DoDragDrop(New DataObject("System.Windows.Forms.ListViewItem()", myItems), DragDropEffects.Move)

    End Sub

    Private Sub ListView_DragEnter(
        ByVal sender As Object, ByVal e As DragEventArgs) _
        Handles Campos_tabla.DragEnter, Campos_report.DragEnter
        ' Check for the custom DataFormat ListViewItem array.
        If e.Data.GetDataPresent("System.Windows.Forms.ListViewItem()") Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ListView_DragDrop(
        ByVal sender As Object, ByVal e As DragEventArgs) _
        Handles Campos_tabla.DragDrop, Campos_report.DragDrop
        Dim myItem As ListViewItem
        Dim myItems() As ListViewItem = e.Data.GetData("System.Windows.Forms.ListViewItem()")
        Dim i As Integer = 0
        Try
            For Each myItem In myItems
                ' Add the item to the target list.
                sender.Items.Add(myItems(i).Text)
                ' Remove the item from the source list.
                If sender Is Campos_tabla Then
                    Campos_report.Items.Remove(Campos_report.SelectedItems.Item(0))
                Else
                    Campos_tabla.Items.Remove(Campos_tabla.SelectedItems.Item(0))
                End If
                i = i + 1
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Function WhichCell(ByVal lvw As ListView, ByVal X As Integer, ByVal Y As Integer) As MyCell
        Dim colstart As Integer = 0
        Dim colend As Integer = 0
        Dim xCol As Integer

        If lvw.Items.Count > 0 Then
            If Not IsNothing(lvw.FocusedItem.Index) Then
                For xCol = 0 To (lvw.Columns.Count - 1)
                    colend = colend + lvw.Columns(xCol).Width
                    If colstart <= X And X <= colend Then
                        WhichCell.Col = xCol + 1
                        Exit For
                    End If
                    colstart = colstart + lvw.Columns(xCol).Width
                Next
                WhichCell.Row = lvw.FocusedItem.Index
            End If
        End If
        Return WhichCell

    End Function

    Private Sub Campos_tabla_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Campos_tabla.MouseDoubleClick
        Dim item As ListViewItem '= LVSeleccionados.SelectedItems(0)

        If Campos_tabla.Items.Count > 0 Then
            item = Campos_tabla.SelectedItems(0)
            Campos_report.Items.Add(item.Text)
            Campos_tabla.Items.Remove(Campos_tabla.SelectedItems.Item(0))
        End If

    End Sub

    Private Sub Campos_report_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Campos_report.MouseDoubleClick
        Dim item As ListViewItem '= LVSeleccionados.SelectedItems(0)

        If Campos_report.Items.Count > 0 Then
            item = Campos_report.SelectedItems(0)
            Campos_tabla.Items.Add(item.Text)
            Campos_report.Items.Remove(Campos_report.SelectedItems.Item(0))
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim item As ListViewItem '= LVSeleccionados.SelectedItems(0)
        Dim i As Integer

        If Campos_tabla.Items.Count > 0 Then
            If Campos_tabla.SelectedItems.Count > 0 Then
                For i = Campos_tabla.SelectedItems.Count - 1 To 0 Step -1
                    item = Campos_tabla.SelectedItems(i)
                    Campos_report.Items.Add(item.Text)
                    Campos_tabla.Items.Remove(Campos_tabla.SelectedItems.Item(i))
                Next
            End If
        End If

    End Sub

    Private Sub devolver_Click(sender As Object, e As EventArgs) Handles devolver.Click
        Dim item As ListViewItem '= LVSeleccionados.SelectedItems(0)
        Dim i As Integer

        If Campos_report.Items.Count > 0 Then
            If Campos_report.SelectedItems.Count > 0 Then
                For i = Campos_report.SelectedItems.Count - 1 To 0 Step -1
                    item = Campos_report.SelectedItems(i)
                    Campos_tabla.Items.Add(item.Text)
                    Campos_report.Items.Remove(Campos_report.SelectedItems.Item(i))
                Next
            End If
        End If
    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click
        Dim i As Integer

        If nSec.Trim <> "" Then
            'datos.Reset()
            datos.Clear()
            For i = 0 To Campos_report.Items.Count - 1
                datos.Rows.Add(Campos_report.Items.Item(i).SubItems(0).Text, nSec)
            Next
            DialogResult = DialogResult.OK
        End If
        DialogResult = DialogResult.Cancel


    End Sub

    'Private Sub Campos_tabla_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Campos_tabla.SelectedIndexChanged
    '    GrabarTablas
    '    Close()
    'End Sub

    Private Sub GrabarTablas()

    End Sub

    Private Sub cancelar_Click(sender As Object, e As EventArgs) Handles cancelar.Click
        Close()
    End Sub

    Private Sub Campos_report_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Campos_report.SelectedIndexChanged

    End Sub
End Class
