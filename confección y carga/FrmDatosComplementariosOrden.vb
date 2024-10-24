Public Class FrmDatosComplementariosOrden
    Inherits libcomunes.FormGenerico
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public fecha_camion As Date
    Public tipo_transporte As String
    Public transportista As String
    Public hora_prevista As String
    Public lote As String
    Public matricula1 As String
    Public matricula2 As String
    Public EsCorrecto As Boolean

    Private Sub Aceptar_Click(sender As Object, e As EventArgs) Handles Aceptar.Click
        fecha_camion = txtfecha_camion.value
        tipo_transporte = "" & TxtTipo_Transporte.Text
        transportista = "" & TxtTransportista.Text
        hora_prevista = "" & TxtHora_prevista.Text
        lote = "" & TxtLote.Text
        matricula1 = "" & TxtMatricula1.Text
        matricula2 = "" & TxtMatricula2.Text
        DialogResult = DialogResult.OK

    End Sub

    Private Sub Cancelar_Click(sender As Object, e As EventArgs) Handles Cancelar.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FrmDatosComplementariosOrden_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtfecha_camion.value = fecha_camion
    End Sub
End Class