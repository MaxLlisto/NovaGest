Public Class FormGenerico
    Inherits System.Windows.Forms.Form

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean

        If (keyData = Keys.Return) Then

            ' Desplazar el foco entre los distintos controles 
            ' mediante la tecla Return. El código está basado en un 
            ' ejemplo de Francesco Balena. 
            ' 
            ' Iniciar todos los controles seleccionados actualmente. 
            ' 
            Dim ctrl As Control = Me.ActiveControl

            ' Si el control DataGridView contiene el foco
            ' abandonamos el procedimiento.
            '
            If (TypeOf ctrl Is DataGridView) Then
                Return MyBase.ProcessCmdKey(msg, keyData)
            ElseIf (TypeOf ctrl Is ListView) Then
                Return MyBase.ProcessCmdKey(msg, keyData)
            End If

            Do
                ' Obtener el siguiente control hacia delante en el 
                ' orden de tabulación. 
                ctrl = Me.GetNextControl(ctrl, True)

                ' GetNextControl(ctrl, False) puede devolver Nothing si 
                ' es el primer control.
                '
                If (Not (ctrl Is Nothing) AndAlso (ctrl.CanFocus) AndAlso (ctrl.TabStop)) Then
                    ' Si el control puede recibir el foco, se lo doy. 
                    ctrl.Focus()
                    Text = ctrl.Name
                    Exit Do
                End If
            Loop

        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function
End Class