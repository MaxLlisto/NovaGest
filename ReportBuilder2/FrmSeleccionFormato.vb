Public Class FrmSeleccionFormato
    Dim FormSeleccionado As String
    Dim ReturnValue As Boolean = False


    Property Formatos() As String
        Get
            Return Me.cboFormatos.Items.ToString
        End Get
        Set(ByVal value As String)
            cboFormatos.Items.Add(value)
            Refresh()
        End Set
    End Property

    Property Seleccionado() As String
        Get
            Return FormSeleccionado
        End Get
        Set(ByVal value As String)
            FormSeleccionado = value
            Refresh()
        End Set
    End Property

    Private Sub cboFormatos_TextChanged(sender As Object, e As EventArgs) Handles cboFormatos.TextChanged
        FormSeleccionado = cboFormatos.SelectedValue
    End Sub

    Private Sub ok_Click(sender As Object, e As EventArgs) Handles ok.Click
        ReturnValue = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ReturnValue = False
        Me.Close()
    End Sub
End Class