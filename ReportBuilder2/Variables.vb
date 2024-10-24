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

Public Class Variables
    Public datos As DataTable
    Dim plvItem As ListViewItem
    Public Resultado As String = ""
    Public nombre_de_variable As String = ""
    Public Correcto As Boolean = False
    Dim nSec As String = ""
    Public Item As String


    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        nombre_variable.Text = ""
        'tipo_variable.Text = "caracter"
        'longitud_variable.Text = 15

    End Sub
    Public Property seccion As String
        Get
            Return nSec
        End Get
        Set(valor As String)
            nSec = valor
            Me.Text = Me.Text.Trim & "(" & nSec & ")"
        End Set
    End Property

    Public Sub cargartabla()
        Dim lvItem As ListViewItem
        Dim i As Integer
        Dim Midatarow() As DataRow
        Dim ctextos As String


        Midatarow = datos.Select("seccion = '" & nSec & "'")

        LvVariables.AllowDrop = True

        For Each MiData As DataRow In Midatarow
            ctextos = MiData("valor")
            lvItem = New ListViewItem(ctextos)
            lvItem.SubItems.Add(MiData("valor"))
            LvVariables.Items.Add(lvItem)
        Next

    End Sub

    Private Sub Incluir_Click(sender As Object, e As EventArgs) Handles Incluir.Click
        Dim i As Integer

        If nombre_variable.Text.Trim = "" Then 'Or tipo_variable.Text.Trim = "" Or longitud_variable.Text.Trim = "" Then
            Exit Sub
        End If
        'If Not IsNumeric(longitud_variable.Text.Trim) Then
        '    MsgBox("La longitud debe de tener un valor numérico")
        '    Exit Sub
        'End If

        If InStr(nombre_variable.Text.Trim, "calculado.") = 0 And InStr(nombre_variable.Text.Trim, "@") = 0 Then
            nombre_variable.Text = "calculado." & nombre_variable.Text.Trim
        End If

        plvItem = Nothing

        For i = 0 To LvVariables.Items.Count - 1
            If UCase(nombre_variable.Text.Trim) = UCase(LvVariables.Items.Item(i).SubItems(0).Text.Trim) Then
                MsgBox("Esta variable ya existe")
                Exit Sub
            End If
        Next

        LvVariables.Items.Add(nombre_variable.Text)
        LvVariables.Refresh()

        ' LvVariables.Items(LvVariables.Items.Count - 1).SubItems.Add(tipo_variable.Text)
        'LvVariables.Items(LvVariables.Items.Count - 1).SubItems.Add(longitud_variable.Text)

        nombre_variable.Text = ""
        'tipo_variable.Text = "caracter"
        'longitud_variable.Text = 15

    End Sub

    'Private Sub LvVariables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvVariables.SelectedIndexChanged

    '    For Each lvItem As ListViewItem In LvVariables.Items
    '        datos.Rows.Add(lvItem.SubItems(0).Text.Trim, nSec)
    '    Next
    '    DialogResult = DialogResult.OK


    '    For Each lvItem As ListViewItem In LvVariables.SelectedItems
    '        plvItem = lvItem
    '        nombre_variable.Text = lvItem.SubItems(0).Text.Trim
    '        '  tipo_variable.Text = lvItem.SubItems(1).Text
    '        '  longitud_variable.Text = lvItem.SubItems(2).Text
    '    Next

    'End Sub

    'Private Sub nombre_variable_TextChanged(sender As Object, e As EventArgs) Handles nombre_variable.TextChanged

    '    If Not plvItem Is Nothing Then
    '        If plvItem.SubItems(1).Text.Trim <> nombre_variable.Text.Trim Then
    '            plvItem = Nothing
    '            'plvItem.SubItems(0).Text = nombre_variable.Text
    '        End If
    '    End If

    'End Sub

    'Private Sub tipo_variable_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If Not plvItem Is Nothing Then
    '        plvItem.SubItems(1).Text = tipo_variable.Text
    '    End If
    'End Sub

    'Private Sub longitud_variable_TextChanged(sender As Object, e As EventArgs)
    '    If Not plvItem Is Nothing Then
    '        plvItem.SubItems(2).Text = longitud_variable.Text
    '    End If
    'End Sub

    Private Sub add_variable_Click(sender As Object, e As EventArgs) Handles add_variable.Click

        plvItem = Nothing
        nombre_variable.Text = ""
        'tipo_variable.Text = "caracter"
        'longitud_variable.Text = 15

    End Sub

    Private Sub cancelar_Click(sender As Object, e As EventArgs) Handles cancelar.Click

        DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub

    Private Sub salir_Click(sender As Object, e As EventArgs) Handles salir.Click

        If LvVariables.SelectedItems.Count > 0 Then
            Resultado = LvVariables.SelectedItems.Item(0).SubItems(1).Text.Trim()
            nombre_de_variable = nombre_variable.Text
        End If

        Correcto = True

        datos.Clear()
        For Each lvItem As ListViewItem In LvVariables.Items
            datos.Rows.Add(lvItem.SubItems(0).Text.Trim, nSec)
        Next

        If cb.Checked Then
            Item = "cb"
        ElseIf im.Checked Then
            Item = "im"
        Else
            Item = "ca"
        End If

            DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub LvVariables_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles LvVariables.ItemDrag
        LvVariables.DoDragDrop(LvVariables.SelectedItems.ToString, DragDropEffects.Copy)
    End Sub

    Private Sub LvVariables_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LvVariables.MouseDoubleClick
        'Resultado = LvVariables.SelectedItems.ToString

        If LvVariables.SelectedItems.Count > 0 Then
            Resultado = LvVariables.SelectedItems.Item(0).SubItems(0).Text.Trim()
            nombre_de_variable = nombre_variable.Text
            Correcto = True
            datos.Clear()
            For Each lvItem As ListViewItem In LvVariables.Items
                datos.Rows.Add(lvItem.SubItems(0).Text.Trim, nSec)
            Next
            If cb.Checked Then
                Item = "cb"
            ElseIf im.Checked Then
                Item = "im"
            Else
                Item = "ca"
            End If
            DialogResult = DialogResult.OK
            Me.Close()
        End If
        Exit Sub



    End Sub

    Private Sub eliminar_Click_1(sender As Object, e As EventArgs) Handles eliminar.Click
        If LvVariables.SelectedItems.Count > 0 Then
            LvVariables.Items.Remove(LvVariables.SelectedItems(0))
        End If
    End Sub

    ''for setting 1 row to the DragDrop data.
    ''You will need to come up with a mechanism for selecting more than one row
    'Private Sub dgvOriginating_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvOriginating.MouseDown
    '    DataGridView1.DoDragDrop(DataGridView1.SelectedRows, DragDropEffects.Copy)
    'End Sub

    ''shows acceptance of data
    'Private Sub LvVariables_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles LvVariables.DragEnter
    '    Dim i As Integer
    '    For i = 0 To e.Data.GetFormats().Length - 1
    '        If e.Data.GetFormats()(i).Equals("System.Windows.Forms.DataGridViewSelectedRowCollection") Then
    '            'The data from the drag source is moved to the target.
    '            e.Effect = DragDropEffects.Copy
    '        End If
    '    Next


    'End Sub
    'put data into DGV
    '    Private Sub LvVariables_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles LvVariables.DragDrop
    '   End Sub

End Class