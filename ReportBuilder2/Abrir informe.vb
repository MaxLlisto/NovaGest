Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Data
Imports System.IO
Imports System.Drawing.Text

Public Class AbrirInformes
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public cn As System.Data.SqlClient.SqlConnection
    Public retorno() As String
    Public informes_antiguos As Boolean

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Public Sub cargar_datos()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sql As String = "select DISTINCT  documento, formato FROM zzzinformatos_p where FORMATO <> 'BASICO' ORDER BY 1,2"
        Dim documentos As String
        Dim tNode As TreeNode

        TwLista.Nodes.Clear()

        informes_antiguos = False
        If cbInformesantiguos.Checked Then
            sql = "select DISTINCT documento, formato FROM zzzimpformato_n where FORMATO <> 'BASICO' ORDER BY 1,2"
            informes_antiguos = True
        End If

        If Rs.Open(sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                If Rs!documento <> documentos Then
                    tNode = TwLista.Nodes.Add(Rs!documento)
                    tNode.ForeColor = Color.Black
                    documentos = Rs!documento
                End If
                tNode.Nodes.Add(Rs!formato)
                tNode.ForeColor = Color.Blue
                Rs.MoveNext()
            End While
        End If

    End Sub

    Private Sub btAbrir_Click(sender As Object, e As EventArgs) Handles btAbrir.Click
        Dim SelNode As TreeNode
        Dim path As String
        Dim aitems() As String

        SelNode = TwLista.SelectedNode

        path = SelNode.FullPath
        'MsgBox(path)

        If InStr(path, "\") <> 0 Then
            aitems = Split(path, "\")
            retorno = aitems
            'MsgBox(aitems(0))
            'MsgBox(aitems(1))
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            aitems = Split(path, "\")
            MsgBox("Por favor, selecciona un formato")
            Exit Sub
            MsgBox(aitems(0))
        End If


    End Sub



    Private Sub Ejecutarseleccion(path As String)
        Dim aitems() As String

        If InStr(path, "\") <> 0 Then
            aitems = Split(path, "\")
            'MsgBox(aitems(0))
            'MsgBox(aitems(1))
        Else
            aitems = Split(path, "\")
            'MsgBox(aitems(0))
        End If

    End Sub

    Private Sub TwLista_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TwLista.MouseDoubleClick
        Dim item As System.Windows.Forms.TreeNode = TwLista.GetNodeAt(e.Location)
        Dim SelNode As TreeNode
        Dim path As String
        Dim aitems() As String

        'If item IsNot Nothing Then
        '    Ejecutarseleccion(item.FullPath)
        'End If

        SelNode = TwLista.SelectedNode

        Path = SelNode.FullPath
        'MsgBox(path)

        If InStr(Path, "\") <> 0 Then
            aitems = Split(Path, "\")
            retorno = aitems
            'MsgBox(aitems(0))
            'MsgBox(aitems(1))
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            aitems = Split(Path, "\")
            'MsgBox("Por favor, selecciona un formato")
            Exit Sub
            'MsgBox(aitems(0))
        End If

    End Sub

    Private Sub btCancelar_Click(sender As Object, e As EventArgs) Handles btCancelar.Click
        Me.DialogResult = DialogResult.No
    End Sub

    Private Sub cbInformesantiguos_CheckedChanged(sender As Object, e As EventArgs) Handles cbInformesantiguos.CheckedChanged
        cargar_datos()
    End Sub
End Class