Imports System
Imports System.Xml
Imports Windows.Data.Xml.Dom

Public Class ClsContestacionRecibidas
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

        cCSV = oXML.TxtReadSingleNode("/env:Envelope/env:Body/siiR:RespuestaLRFacturasRecibidas/siiR:CSV")
        EstadoEnvio = oXML.TxtReadSingleNode("/env:Envelope/env:Body/siiR:RespuestaLRFacturasRecibidas/siiR:EstadoEnvio")
        nodos = oXML.NodeCount("/env:Envelope/env:Body/siiR:RespuestaLRFacturasRecibidas")
        oListadeNodos = oXML.ReadNodes("/env:Envelope/env:Body/siiR:RespuestaLRFacturasRecibidas")

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

                PrintLine(1, "", "Correcto")

                If Trim("" & nif_emisor) = "" Then
                    PrintLine(1, "", "cee")
                    nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:IDOtro/sii:ID")
                Else
                    PrintLine(1, "", "No cee")
                End If

                PrintLine(1, "", "NIF " & nif_emisor)
                PrintLine(1, "", "Fecha " & FechaEmision)

                RsEnviadas.Open("Select * FROM factura_com_aeat where situacion = 'P' AND Serie_numero = '" & Trim(serieNumero) & "' and fecha_factura = '" & FechaEmision & "' AND CIF_prov = '" & nif_emisor & "'", ObjetoGlobal.cn, True)
                PrintLine(1, "", "Registros " & RsEnviadas.RecordCount)

                If RsEnviadas.RecordCount > 0 Then
                    RsEnviadas!Situacion = "C"
                    RsEnviadas!csv = cCSV
                    RsEnviadas.Update()
                End If
                RsEnviadas.Close()

            ElseIf Correcto_sn = "AceptadoConErrores" Then

                PrintLine(1, "", "Aceptado con errores")
                serieNumero = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:NumSerieFacturaEmisor")
                nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:NIF")
                FechaFactura = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:FechaExpedicionFacturaEmisor")
                FechaEmision = Replace(FechaFactura, "-", "/")
                codigo_error = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:CodigoErrorRegistro")
                Descripcion_error = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:DescripcionErrorRegistro")

                If Trim("" & nif_emisor) = "" Then
                    PrintLine(1, "", "cee")
                    nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:IDOtro/sii:ID")
                Else
                    PrintLine(1, "", "No cee")
                End If

                RsEnviadas.Open("Select * FROM factura_com_aeat where situacion = 'P' AND Serie_numero = '" & Trim(serieNumero) & "' and fecha_factura = '" & FechaFactura & "' AND CIF = '" & nif_emisor & "'", ObjetoGlobal.cn, True)
                PrintLine(1, "", "Registros " & RsEnviadas.RecordCount)

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

                If Trim("" & nif_emisor) = "" Then
                    PrintLine(1, "", "cee")
                    nif_emisor = oNodo.TxtReadSingleNode("/siiR:RespuestaLinea/siiR:IDFactura/sii:IDEmisorFactura/sii:IDOtro/sii:ID")
                Else
                    PrintLine(1, "", "No cee")
                End If

                RsEnviadas.Open("Select * FROM factura_com_aeat where situacion = 'P' AND Serie_numero = '" & Trim(serieNumero) & "' and fecha_factura = '" & FechaFactura & "' AND CIF_prov = '" & nif_emisor & "'", ObjetoGlobal.cn, True)
                If RsEnviadas.RecordCount > 0 Then
                    RsEnviadas!Situacion = "E"
                    RsEnviadas!csv = ""
                    RsEnviadas!cod_error = codigo_error
                    RsEnviadas!des_error = Descripcion_error
                    RsEnviadas.Update()
                End If
                RsEnviadas.Close()
            End If
        Next

    End Sub


    '    FileOpen(1, "c:\trash.txt", OpenMode.Output)   ' Open file for output.
    'Print(1, "This is a test.")   ' Print text to file.
    'PrintLine(1)   ' Print blank line to file.
    'PrintLine(1, "Zone 1", TAB(), "Zone 2")   ' Print in two print zones.
    'PrintLine(1, "Hello", "World")   ' Separate strings with a tab.
    'PrintLine(1, SPC(5), "5 leading spaces ")   ' Print five leading spaces.
    'PrintLine(1, TAB(10), "Hello")   ' Print word at column 10.

    '' Assign Boolean, Date, and Error values.
    'Dim aBool As Boolean
    '    Dim aDate As DateTime
    'aBool = False
    'aDate = DateTime.Parse("February 12, 1969")

    '' Dates and booleans are translated using locale settings of your system.
    'PrintLine(1, aBool, " is a Boolean value")
    'PrintLine(1, aDate, " is a date")
    'FileClose(1)   ' Close file.

End Class
