Public Class ClonarBulto
    Public Resultado As Integer = 0

    Private Sub PsClonar_Click(sender As Object, e As EventArgs) Handles PsClonar.Click

        If IsNumeric(TxtBulto.Text) Then
            If CInt(TxtBulto.Text) Then
                Resultado = CInt(TxtBulto.Text)
            End If
        Else
            MsgBox("Tienes que indicar un número válido")
            Resultado = 0
        End If
        Me.Close()

    End Sub

    Private Sub PsSalir_Click(sender As Object, e As EventArgs) Handles PsSalir.Click
        Me.Close()
    End Sub
End Class