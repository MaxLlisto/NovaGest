Public Class FrmEliminarEnvio
    Public Objetoglobal As ObjetoGlobal.ObjetoGlobal

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        If Opcion1.Checked Then
            Sql = "SELECT * FROM factura_vta_aeat WHERE numero_envio = " & TxtNumeroenvio.Text & " AND situacion <> 'P' "
            Rs.Open(Sql, Objetoglobal.cn)
            If Rs.RecordCount > 0 Then
                MsgBox("Este número de envío tiene registro ya procesados")
                TxtNumeroenvio.Text = "0"
                Return
            End If
            Rs.Close()
            Sql = "SELECT * FROM factura_vta_aeat WHERE numero_envio = " & TxtNumeroenvio.Text & " AND situacion = 'P' "
            Rs.Open(Sql, Objetoglobal.cn, True)
            While Rs.RecordCount > 0
                Rs.MoveFirst()
                Rs.Delete()
            End While
        ElseIf Opcion2.Checked Then
            Sql = "SELECT * FROM factura_com_aeat WHERE numero_envio = " & txtNumeroEnvio.Text & " AND situacion <> 'P' "
            Rs.Open(Sql, Objetoglobal.cn)
            If Rs.RecordCount > 0 Then
                MsgBox("Este número de envío tiene registro ya procesados")
                TxtNumeroenvio.Text = "0"
                Return
            End If
            Rs.Close
            Sql = "SELECT * FROM factura_com_aeat WHERE numero_envio = " & txtNumeroEnvio.Text & " AND situacion = 'P' "
            Rs.Open(Sql, Objetoglobal.cn, True)
            While Rs.RecordCount > 0
                Rs.MoveFirst
                Rs.Delete
            End While
        End If
        Rs.Close
        MsgBox("Proceso de borrado concluido")

    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return Objetoglobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            Objetoglobal = value
        End Set
    End Property

End Class