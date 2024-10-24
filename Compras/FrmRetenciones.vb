Public Class FrmRetenciones
    Public BaseRetencion As Double
    Public TipoRetencíon As Double
    Public pform As FrmFacturasProveedor

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        BaseRetencion = CDbl(TxtBase.Text)
        TipoRetencíon = CDbl(TxtTipo.Text)
        DialogResult = DialogResult.Yes
    End Sub

    Private Sub FrmRetenciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sql As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTP As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        ' Tipo línea total factura IRP descrición IRPF cuota en importe cargo

        sql = "select * temp_factura_compra WHERE empresa = '" & ObjetoGlobal.empresaactual & "' AND proceso =" & pform.CnTabla02.ValorCampo("proceso") & " order by 1,2,3"
        Rs.Open(Sql, ObjetoGlobal.cn)

        While Not Rs.EOF
            If Not IsDBNull(Rs!Importe) Then
                BaseRetencion += Math.Round(Rs!Importe, 2)
            End If
            Rs.MoveNext()
        End While

        'Datos apunte proveedor
        RsTP.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = 'ANOTACION FACTURA COMPRA' AND CAMPO = 'TIPO_RETENCION'", ObjetoGlobal.cn)
        TipoRetencíon = CDbl(RsTP!valor_defecto)
        TxtBase.Text = BaseRetencion
        TxtTipo.Text = TipoRetencíon
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        DialogResult = DialogResult.No
    End Sub
End Class