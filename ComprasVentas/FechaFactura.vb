Public Class FechaFactura
    Public Fecha_factura As Date

    Private Sub Aceptar_Click(sender As Object, e As EventArgs) Handles Aceptar.Click
        Fecha_factura = dFecha.Text
        DialogResult = DialogResult.OK
    End Sub
    Private Sub Cancelar_Click(sender As Object, e As EventArgs) Handles Cancelar.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FechaFactura_Load(sender As Object, e As EventArgs) Handles Me.Load
        dFecha.Text = Date.Now
    End Sub
End Class