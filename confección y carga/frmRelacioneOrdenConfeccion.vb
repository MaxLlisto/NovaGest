Public Class frmRelacioneOrdenConfeccion
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public OrdenAsociada As String

    Private Sub frmRelacioneOrdenConfeccion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarItems()
    End Sub

    Private Sub CmdSalir_Click()
        OrdenAsociada = ""
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub CargarItems()
        Dim RsDocsAso As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        Dim i As Integer
        Dim item As ListViewItem

        sSql = "SELECT ordenes_confeccion.*, isnull((SELECT sum(carga_confeccion.numero_palets) from carga_confeccion where carga_confeccion.empresa=ordenes_confeccion.empresa and carga_confeccion.orden_confeccion=ordenes_confeccion.numero_orden),0) as Consumidos,isnull((SELECT count(*) from palets where palets.empresa ='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND  palets.orden_confeccion = ordenes_confeccion.numero_orden),0) as fabricados FROM ordenes_confeccion WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' and ( numero_palets - isnull((SELECT sum(carga_confeccion.numero_palets) from carga_confeccion where carga_confeccion.empresa=ordenes_confeccion.empresa and carga_confeccion.orden_confeccion=ordenes_confeccion.numero_orden),0) ) > 0"
        sSql = sSql + " and (ordenes_confeccion.estado <> 'A' and not (ordenes_confeccion.estado >= '8')) order by numero_orden "

        lwAsociados.Items.Clear()
        RsDocsAso.Open(sSql, ObjetoGlobal.cn)
        i = 0
        While Not RsDocsAso.EOF
            item = New ListViewItem(RsDocsAso!numero_orden.ToString)
            item.SubItems.Add(RsDocsAso!fecha_orden)
            item.SubItems.Add(RsDocsAso!codigo_cliente)
            item.SubItems.Add(RsDocsAso!codigo_destinatario)
            item.SubItems.Add(RsDocsAso!codigo_confeccion)
            item.SubItems.Add(RsDocsAso!codigo_variedad)
            item.SubItems.Add(RsDocsAso!codigo_marca)
            item.SubItems.Add(RsDocsAso!numero_palets)
            item.SubItems.Add(RsDocsAso!Consumidos)
            item.SubItems.Add((RsDocsAso!numero_palets - RsDocsAso!Consumidos))
            item.SubItems.Add((RsDocsAso!fabricados))
            lwAsociados.Items.Add(item)
            RsDocsAso.MoveNext()
        End While

        RsDocsAso.Close

    End Sub

    Private Sub Seleccionar_Click(sender As Object, e As EventArgs) Handles Seleccionar.Click
        OrdenAsociada = lwAsociados.SelectedItems.ToString
        DialogResult = DialogResult.OK
    End Sub
End Class