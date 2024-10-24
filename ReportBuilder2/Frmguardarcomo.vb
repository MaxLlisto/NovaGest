Public Class Frmguardarcomo
    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        If TxtFormato.Text.Trim = "" Then
            Exit Sub
        End If
        If TxtFormato.Text.Trim.Length > 30 Then
            MsgBox("Por favor, el nombre del formato como máximo debe tener 30 caracteres")
            Return
        End If
        DialogResult = DialogResult.Yes
        Close()
    End Sub

    Private Sub button3_Click(sender As Object, e As EventArgs) Handles button3.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Public Property formato As String
        Get
            Return TxtFormato.Text
        End Get
        Set(valor As String)
            TxtFormato.Text = valor
        End Set
    End Property

End Class