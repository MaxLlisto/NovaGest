Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Data
Imports System.IO
Imports System.Drawing.Text



Public Class FrmNuevoformato
        Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
        Public cn As System.Data.SqlClient.SqlConnection
    Public retorno(4) As String

    Public Sub New()
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    Private Sub Btcorrecto_Click(sender As Object, e As EventArgs) Handles Btcorrecto.Click
        If Txtnuevodocumento.Text.Trim.Length > 30 Then
            MsgBox("Por favor, el nombre de documento como máximo debe tener 30 caracteres")
            Return
        End If
        If Txtformato.Text.Trim.Length > 30 Then
            MsgBox("Por favor, el nombre de formato como máximo debe tener 30 caracteres")
            Return
        End If

        If ((Txtnuevodocumento.Text.Trim <> "" And (TxtNuevoProceso.Text.Trim <> "" Or cbProcesonuevo.Text.Trim <> "")) _
            Or (cbdocumento.Text.Trim <> "")) And Txtformato.Text.Trim <> "" Then
            retorno(0) = If(cbnuevo.Checked, Txtnuevodocumento.Text, cbdocumento.Text)
            retorno(1) = Txtformato.Text
            retorno(2) = If(cbnuevo.Checked, "Sí", "No")
            retorno(3) = If(cbnuevo.Checked And CbNuevoProceso.Checked, "Sí", "No")
            retorno(4) = If(Not CbNuevoProceso.Checked And cbProcesonuevo.Text.Trim <> "", cbProcesonuevo.Text.Trim, TxtNuevoProceso.Text)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MsgBox("Por favor, selecciona un formato o un documento o en su caso un proceso válido")
            Exit Sub
        End If
    End Sub

    Public Sub cargar_datos()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sql As String = "select DISTINCT documento FROM zzzinformatos_p where FORMATO = 'BASICO' ORDER BY 1"
        Dim documentos As String
        Dim procesos As String
        Dim sql1 As String = "Select DISTINCT proceso FROM zzformatosdefecto_n ORDER BY 1"

        cbdocumento.Items.Clear()
        cbProcesonuevo.Items.Clear()

        If Rs.Open(sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                If Rs!documento <> documentos Then
                    cbdocumento.Items.Add(Rs!documento)
                    documentos = Rs!documento
                End If
                Rs.MoveNext()
            End While
        End If
        Rs.Close()

        If Rs.Open(sql1, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                If Rs!proceso <> procesos Then
                    cbProcesonuevo.Items.Add(Rs!proceso)
                    procesos = Rs!proceso
                End If
                Rs.MoveNext()
            End While
        End If
        Rs.Close()

    End Sub

    Private Sub Btcancelar_Click(sender As Object, e As EventArgs) Handles Btcancelar.Click
        Me.DialogResult = DialogResult.No
    End Sub

    Private Sub cbnuevo_CheckedChanged(sender As Object, e As EventArgs) Handles cbnuevo.CheckedChanged
        If cbnuevo.Checked Then
            Txtnuevodocumento.Enabled = True
            cbdocumento.Enabled = False
            CbNuevoProceso.Visible = True
            TxtNuevoProceso.Visible = True
            cbProcesonuevo.Enabled = True
        Else
            Txtnuevodocumento.Enabled = False
            cbdocumento.Enabled = True
            CbNuevoProceso.Visible = False
            TxtNuevoProceso.Visible = False
            cbProcesonuevo.Enabled = False
        End If

    End Sub

    Private Sub CbNuevoProceso_CheckedChanged(sender As Object, e As EventArgs) Handles CbNuevoProceso.CheckedChanged
        If CbNuevoProceso.Checked Then
            TxtNuevoProceso.Enabled = True
            cbProcesonuevo.Enabled = False
            cbProcesonuevo.Text = ""
        Else
            TxtNuevoProceso.Enabled = False
            cbProcesonuevo.Enabled = True
        End If
    End Sub
End Class