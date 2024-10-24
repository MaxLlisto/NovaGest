Imports System
Imports System.Xml
Imports Windows.Data.Xml.Dom

Public Class ClsContestacionEmitidas
    Dim cPost As String
    Dim cHost As String
    Dim Cabecera As String
    Dim cabecera1 As String
    Dim sessionkey As String
    Dim cXML As String
    Dim cCod As String
    Dim nSeleccion As Integer
    Dim sFichero As String
    Dim oXML As New Clsxml
    Public ObjetoGlobal As Object

    Public Sub CargaDocumento(cXML, pbR)
        Dim cCSV As String
        Dim EstadoEnvio As String
        Dim a() As Object
        Dim i As Long
        Dim serieNumero As String
        Dim Correcto_sn As String
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nodos As Integer
        Dim SubXML As String
        Dim oNodo As Clsxml
        Dim FechaEmision As String
        Dim FechaFactura As String
        Dim nif_emisor As String
        Dim codigo_error As String
        Dim Descripcion_error As String
        Dim oListadeNodos As XmlNodeList

        oXML.OpenXML(cXML)

        cCSV = oXML.TxtReadSingleNode("/env:Envelope/env:Body/siiR:RespuestaLRFacturasEmitidas/siiR:CSV")
        EstadoEnvio = oXML.TxtReadSingleNode("/env:Envelope/env:Body/siiR:RespuestaLRFacturasEmitidas/siiR:EstadoEnvio")
        nodos = oXML.NodeCount("/env:Envelope/env:Body/siiR:RespuestaLRFacturasEmitidas/siiR:RespuestaLinea")
        oListadeNodos = oXML.ReadNodes("/env:Envelope/env:Body/siiR:RespuestaLRFacturasEmitidas/siiR:RespuestaLinea")

        pbR.Min = 0
        pbR.max = nodos
        pbR.Visible = True
        pbR.value = 0

        For Each oSubXML As XmlNode In oListadeNodos

            i += 1
            pbR.value = i
            Application.DoEvents()

            oNodo = New Clsxml(oSubXML)

            Correcto_sn = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:EstadoRegistro").Trim

            If Correcto_sn = "Correcto" Then
                serieNumero = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:NumSerieFacturaEmisor")
                nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:NIF")
                FechaFactura = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:FechaExpedicionFacturaEmisor")
                FechaEmision = Replace(FechaFactura, "-", "/")
                RsEnviadas.Open("Select * FROM factura_vta_aeat where situacion = 'P' AND Serie_numero = '" & Trim(serieNumero) & "' and fecha_factura BETWEEN '" & Format(FechaFactura, "dd/mm/yyyy 00:00:00") & "' and '" & Format(FechaFactura, "dd/mm/yyyy 23:59:59") & "'", ObjetoGlobal.cn, True)
                If RsEnviadas.RecordCount > 0 Then
                    RsEnviadas!Situacion = "C"
                    RsEnviadas!csv = cCSV
                    RsEnviadas.Update()
                End If
                RsEnviadas.Close()
            ElseIf Correcto_sn = "AceptadoConErrores" Then
                serieNumero = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:NumSerieFacturaEmisor")
                nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:NIF")
                FechaFactura = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:FechaExpedicionFacturaEmisor")
                codigo_error = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:CodigoErrorRegistro")
                Descripcion_error = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:DescripcionErrorRegistro")

                RsEnviadas.Open("Select * FROM factura_vta_aeat where situacion <> 'P' AND Serie_numero = '" & Trim(serieNumero) & "' and fecha_factura = '" & Format(FechaFactura, "dd/mm/yyyy 00:00:00") & "'", ObjetoGlobal.cn, True)
                If RsEnviadas.RecordCount > 0 Then
                    RsEnviadas!Situacion = "A"
                    RsEnviadas!csv = cCSV
                    RsEnviadas!cod_error = codigo_error
                    RsEnviadas!des_error = Strings.Left(Descripcion_error, 150)
                    RsEnviadas.Update()
                End If
                RsEnviadas.Close()
            Else
                serieNumero = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:NumSerieFacturaEmisor")
                nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:NIF")
                FechaFactura = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:FechaExpedicionFacturaEmisor")
                codigo_error = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:CodigoErrorRegistro")
                Descripcion_error = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:DescripcionErrorRegistro")
                RsEnviadas.Open("Select * FROM factura_vta_aeat where situacion = 'P' AND Serie_numero = '" & Trim(serieNumero) & "' and fecha_factura = '" & Format(FechaFactura, "dd/mm/yyyy 00:00:00") & "'", ObjetoGlobal.cn, True)
                If RsEnviadas.RecordCount = 1 Then
                    RsEnviadas!Situacion = "E"
                    RsEnviadas!csv = ""
                    RsEnviadas!cod_error = codigo_error
                    RsEnviadas!des_error = Left(Descripcion_error, 150)
                    RsEnviadas.Update()
                End If
                RsEnviadas.Close()
            End If
        Next

    End Sub

End Class
