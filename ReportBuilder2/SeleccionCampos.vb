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


Public Class SeleccionCampos
    Dim cTabla As String
    Dim nSec As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public datos As DataTable
    Public Tablas As DataTable
    Public Resultado As String = ""
    Public nombre_de_variable As String = ""
    Public Correcto As Boolean = False
    Public Item As String


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

    Public Function cargartabla()
        Dim lvItem As ListViewItem
        Dim i As Integer
        Dim Midatarow() As DataRow
        Dim ValorDato As String
        Dim Mistablas() As DataRow
        Dim SQL As String
        Dim RS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cCampo As String
        Dim MiLista As List(Of String) = New List(Of String)()

        ' Select Case*
        'From syscolumns
        'Where id = OBJECT_ID('tu_tabla')  
        'ORDER BY colorder

        Midatarow = datos.Select("seccion = '" & nSec & "'")
        Mistablas = Tablas.Select("seccion = '" & nSec & "'")

        '' Valores tabla
        'For Each MiData As DataRow In Midatarow
        '    ValorDato = "" & MiData(0)
        '    lvItem = New ListViewItem(ValorDato) 'Campos_tabla.Items.Count + 1)
        '    lvItem.SubItems.Add(MiData(0))
        '    Campos_tabla.Items.Add(lvItem)
        '    'Campos_report.Items.Add(lvItem)
        'Next

        ' Valores report
        For Each MiData As DataRow In Midatarow
            ValorDato = "" & MiData(0)
            lvItem = New ListViewItem(ValorDato) 'Campos_tabla.Items.Count + 1)
            lvItem.SubItems.Add(MiData(0))
            Campos_report.Items.Add(lvItem)
            MiLista.Add(ValorDato)
        Next

        For Each MiData As DataRow In Mistablas
            ValorDato = "" & MiData(0)
            SQL = " Select *  From syscolumns Where id = OBJECT_ID('" & ValorDato & "')  ORDER BY colorder"
            If RS.Open(SQL, ObjetoGlobal.cn) Then
                While Not RS.EOF
                    cCampo = ValorDato.Trim & "." & RS!name
                    If Not MiLista.Contains(cCampo) Then
                        lvItem = New ListViewItem(cCampo)
                        lvItem.SubItems.Add(cCampo)
                        Campos_tabla.Items.Add(lvItem)
                    End If
                    RS.MoveNext()
                End While
            End If
            RS.Close()
        Next


    End Function



    'Sql = "Select * FROM zzestructura where TABLA = '" & cTabla & "' and n9 = 1 order by nColumna"

    'rs.Open(Sql, ObjetoGlobal.cn)
    'While Not rs.EOF
    '    Campos_tabla.Items.Add(rs!campo)
    '    rs.MoveNext()
    'End While
    'rs.Close()

    'rs = ObjetoGlobal.cn.OpenSchema(adSchemaTables)
    'hay = rs.RecordCount
    'If Hay <= 0 Then
    '    MsgBox("No existen tablas en la BD")
    'Else
    '    rs.MoveFirst()
    '    While Not rs.EOF
    '        If rs!table_type = "TABLE" Or LCase(Strings.Left(rs!table_name, 5)) = "vista" And LCase(Strings.Left(rs!table_name, 2)) <> "zz" Then
    '            Campos_tabla.Items.Add(rs!table_name)
    '        End If
    '        rs.MoveNext()
    '    End While
    'End If

    'End Sub

    'Private Sub lst_MouseDown(sender As Object, e As MouseEventArgs) Handles Campos_tabla.MouseDown, Campos_report.MouseDown
    '    Dim lst As ListBox = DirectCast(sender, ListBox)

    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        Dim index As Integer = Campos_tabla.IndexFromPoint(e.X, e.Y)
    '        If index <> ListBox.NoMatches Then
    '            Dim item As String = lst.Items(index)
    '            Dim drop_effect As DragDropEffects = lst.DoDragDrop(lst.Items(index), DragDropEffects.Move Or DragDropEffects.Copy)
    '            ' If it was moved, remove the item from this
    '            ' list.
    '            If drop_effect = DragDropEffects.Move Then
    '                ' See if the user dropped the item in this
    '                ' ListBox
    '                ' at a higher position.
    '                If lst.Items(index) = item Then
    '                    ' The item has not moved.
    '                    lst.Items.RemoveAt(index)
    '                Else
    '                    ' The item has moved.
    '                    lst.Items.RemoveAt(index + 1)
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub Campos_tabla_DragOver(sender As Object, e As DragEventArgs) Handles Campos_tabla.DragDrop, Campos_report.DragDrop
    '    Const KEY_CTRL As Integer = 8

    '    If Not (e.Data.GetDataPresent(GetType(System.String))) Then
    '        e.Effect = DragDropEffects.None
    '    ElseIf (e.KeyState And KEY_CTRL) And (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy Then
    '        ' Copy.
    '        e.Effect = DragDropEffects.Copy
    '    ElseIf (e.AllowedEffect And DragDropEffects.Move) = DragDropEffects.Move Then
    '        ' Move.
    '        e.Effect = DragDropEffects.Move
    '    End If
    'End Sub

    'Private Sub Campos_tabla_DragDrop(sender As Object, e As DragEventArgs) Handles Campos_tabla.DragDrop, Campos_report.DragDrop
    '    If e.Data.GetDataPresent(GetType(System.String)) Then
    '        If (e.Effect = DragDropEffects.Copy) Or (e.Effect = DragDropEffects.Move) Then
    '            Dim lst As ListBox = DirectCast(sender, ListBox)
    '            Dim item As Object = CType(e.Data.GetData(GetType(System.String)), System.Object)
    '            Dim pt As Point = lst.PointToClient(New Point(e.X, e.Y))
    '            Dim index As Integer = lst.IndexFromPoint(pt.X, pt.Y)
    '            If index = ListBox.NoMatches Then
    '                lst.Items.Add(item)
    '            Else
    '                lst.Items.Insert(index, item)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cancelar.Click
    '    Dim ret As System.Data.DataTable = New DataTable()
    '    Dim Sql As String
    '    Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    '    Dim i As Integer
    '    Dim Total As Integer = Campos_report.Items.Count
    '    Dim Valor As Integer = 0

    '    ret.Columns.Add("campo")
    '    ret.Columns.Add("tabla")
    '    ret.Columns.Add("tipo")
    '    ret.Columns.Add("longitud", GetType(Integer))
    '    ret.Columns.Add("decimales", GetType(Integer))
    '    ret.Columns.Add("mascara")
    '    ret.Columns.Add("etiqueta")
    '    ret.Columns.Add("seccion")

    '    ':::Iniciamos nuestro ciclo for
    '    For i = 0 To Total - 1
    '        Sql = "SELECT * FROM zzestructura where TABLA = '" & cTabla & "' and campo = '" & Campos_report.Items(i).ToString & "'"
    '        rs.Open(Sql, ObjetoGlobal.cn)
    '        If Not rs.EOF Then
    '            ret.Rows.Add(rs!campo, rs!tabla, rs!tipo, rs!longitud, rs!decimales, rs!mascara, rs!etiqueta)
    '        End If
    '        rs.Close()
    '    Next

    'End Sub

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
        sender.DoDragDrop(New _
        DataObject("System.Windows.Forms.ListViewItem()", myItems),
        DragDropEffects.Move)
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

        Try
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
        Catch ex As Exception

        End Try


    End Function

    Private Sub Campos_tabla_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Campos_tabla.MouseDoubleClick
        Dim item As ListViewItem '= LVSeleccionados.SelectedItems(0)

        Try
            If Campos_tabla.Items.Count > 0 Then
                item = Campos_tabla.SelectedItems(0)
                Campos_report.Items.Add(item.Text)
                Campos_tabla.Items.Remove(Campos_tabla.SelectedItems.Item(0))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Campos_report_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Campos_report.MouseDoubleClick
        'Dim item As ListViewItem '= LVSeleccionados.SelectedItems(0)

        'If Campos_report.Items.Count > 0 Then
        '    item = Campos_report.SelectedItems(0)
        '    Campos_tabla.Items.Add(item.Text)
        '    Campos_report.Items.Remove(Campos_report.SelectedItems.Item(0))
        'End If
        Try
            Resultado = Campos_report.SelectedItems.Item(0).SubItems(0).Text.Trim()
            Correcto = True
            If cb.Checked Then
                Item = "cb"
            ElseIf im.Checked Then
                Item = "im"
            Else
                Item = "ca"
            End If
            DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception

        End Try
        DialogResult = DialogResult.Cancel
        Me.Close()

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

        Try
            If Campos_report.Items.Count > 0 Then
                If Campos_report.SelectedItems.Count > 0 Then
                    For i = Campos_report.SelectedItems.Count - 1 To 0 Step -1
                        item = Campos_report.SelectedItems(i)
                        Campos_tabla.Items.Add(item.Text)
                        Campos_report.Items.Remove(Campos_report.SelectedItems.Item(i))
                    Next
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Campos_report_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Campos_report.SelectedIndexChanged

    End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click
        Dim i As Integer

        Try
            If Campos_report.SelectedItems.Count > 0 Then
                Resultado = Campos_report.SelectedItems.Item(0).SubItems(0).Text.Trim()
            Else
                Resultado = ""
            End If
            Correcto = True

            If nSec.Trim <> "" Then
                'datos.Reset()
                datos.Clear()
                For i = 0 To Campos_report.Items.Count - 1
                    datos.Rows.Add(Campos_report.Items.Item(i).SubItems(0).Text, nSec)
                Next
                If cb.Checked Then
                    Item = "cb"
                ElseIf im.Checked Then
                    Item = "im"
                Else
                    Item = "ca"
                End If
                DialogResult = DialogResult.OK
            End If
            DialogResult = DialogResult.Cancel

            Me.Close()
        Catch ex As Exception
            Correcto = False
        End Try
        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cancelar_Click(sender As Object, e As EventArgs) Handles cancelar.Click
        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class