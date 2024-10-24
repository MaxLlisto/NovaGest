Imports System.IO
Imports System
Imports System.Text
Imports System.Xml
Imports RestSharp
Imports RestSharp.Authenticators
Imports System.Collections
Imports System.Dynamic
Imports System.Threading.Tasks

Public Class FrmImportarexpedientes
    Inherits libcomunes.FormGenerico
    Dim nSeleccion As Integer
    Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim strStreamW As FileStream
    Dim strStreamWriter As StreamWriter
    Dim NombreArchivo As String
    Dim trans As SqlClient.SqlTransaction

    Public Sub DescargaExpedientes()
        Dim client As RestClient '= New RestClient("https://anecoopintegracion.anecoop.com/serviciosweb/login.asmx/CheckLogin?Usuario=adlliria&Empresa=46&Password=l82250a11")
        Dim request As RestRequest '= New RestRequest(Method.Get)
        Dim oHttp As XmlDocument = New XmlDocument()
        Dim result As RestResponse '= RestClient.Execute(request)
        Dim root As XmlNode
        Dim cPost As String = "/services/ConsultaExpediente.asmx"
        Dim cHost As String = "anecoopintegracion.anecoop.com"
        Dim response As RestResponse
        Dim txtXML As String
        Dim cXML As String
        Dim sessionkey As String
        Dim cCod As String
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        ObjetoGlobal.cn.Open()

        Try
            trans = ObjetoGlobal.cn.BeginTransaction()

            client = New RestClient("https://anecoopintegracion.anecoop.com/serviciosweb/login.asmx/CheckLogin?Usuario=adlliria&Empresa=46&Password=l82250a11")
            request = New RestRequest()

            lblTrabajos.Text = "Comunicando con Anecoop"
            Application.DoEvents()

            response = (client.Execute(request))

            oHttp.LoadXml(LimpiarRespuesta(response.Content))

            sessionkey = oHttp.GetElementsByTagName("SessionKey").Item(0).InnerText

            lblTrabajos.Text = "Anecoop nos ha contestado"
            Application.DoEvents()

            If Nuevos.Checked Then
                nSeleccion = 0
            ElseIf Descargados.Checked Then
                nSeleccion = 1
            Else
                nSeleccion = -1
            End If

            cCod = "46"
            cXML = DevuelveXML(cCod, sessionkey, TxtDesdelafecha.value, TxtHastalafecha.value, Nothing, Nothing, nSeleccion)

            lblTrabajos.Text = "Solicitando expedientes a Anecoop, esperando respuesta......"
            Application.DoEvents()

            client = New RestClient("https://anecoopintegracion.anecoop.com/ServiciosWeb/ConsultaExpediente.asmx")
            request = New RestRequest()
            request.AddHeader("Post", cPost)
            request.AddHeader("Host", cHost)
            request.AddHeader("Content-Type", "application/soap+xml; charset=utf-8")
            request.AddHeader("Content-length", CStr(Len(cXML)))
            request.Method = Method.Post
            request.AddBody(cXML)
            response = client.Execute(request)
            txtXML = response.Content

            lblTrabajos.Text = "Expedientes recibidos, procesamos...."
            Application.DoEvents()

            NombreArchivo = Trim(Now)
            NombreArchivo = Replace(NombreArchivo, "/", "")
            NombreArchivo = Replace(NombreArchivo, " ", "")
            NombreArchivo = Replace(NombreArchivo, ":", "")
            hacerfichero("IMP_ANE" & Trim(NombreArchivo) & ".xml")
            GrabarTextoArchivo(response.Content)
            oHttp.Load(NombreArchivo)

            ' Create an XmlNamespaceManager to resolve namespaces.
            Dim nt As NameTable = New NameTable()
            Dim nsmgr As New XmlNamespaceManager(nt)
            nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope")
            nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance")
            nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema")

            'm_nodelist = oHttp.SelectNodes("//<soap:Body/DescargaPedidosResponse/DescargaPedidosResult/Expediente", nsmgr)
            m_nodelist = oHttp.SelectNodes("//DescargaPedidosResult/Expediente", nsmgr)
            If m_nodelist.Count = 0 Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                lblTrabajos.Text = "¡¡No hay expedientes que procesar!!"
                Application.DoEvents()
                Return
            ElseIf Not ProcesarExpedientes(m_nodelist) Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                lblTrabajos.Text = "Se ha producido un error en la importación de expedientes"
                Application.DoEvents()
                Return
            Else
                m_nodelist = oHttp.SelectNodes("//DescargaPedidosResult/Expediente/EXPEDIENTE_PAGO", nsmgr)
                If Not ProcesarPagos(m_nodelist) Then
                        trans.Rollback()
                        ObjetoGlobal.cn.Close()
                        lblTrabajos.Text = "Se ha producido un error en la importación de expedientes de pago"
                        Application.DoEvents()
                        Return
                    End If
                m_nodelist = oHttp.SelectNodes("//DescargaPedidosResult/Expediente/EXPEDIENTE_COBRO", nsmgr)
                'If Not ProcesarCobros(m_nodelist) Then
                '    trans.Rollback()
                '    ObjetoGlobal.cn.Close()
                '    lblTrabajos.Text = "Se ha producido un error en la importación de expedientes de cobro"
                '    Application.DoEvents()
                '    Return
                'End If

                trans.Commit()
                ObjetoGlobal.cn.Close()
                lblTrabajos.Text = "Proceso concluido con éxito"
                Application.DoEvents()
                'MsgBox("Proceso concluido")
            End If
            ' trans.Commit()
            '  ObjetoGlobal.cn.Close()

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            lblTrabajos.Text = "Se ha producido un error en la importación de expedientes " & ex.Message & "->" & ex.ToString()
            Application.DoEvents()
            'MsgBox("Se ha producido un error en la importación de expedientes: " & ex.Message & "->" & ex.ToString())
        End Try


    End Sub
    Private Function ProcesarExpedientes(m_nodelist As XmlNodeList) As Boolean
        Dim aExpedientes As XmlDocument = New XmlDocument()
        Dim aPagos As XmlNodeList
        Dim aCobros As XmlNodeList
        Dim i As Integer
        Dim j As Integer
        Dim NodoCobro As XmlNode
        Dim NodoPago As XmlNode
        Dim NodosHijos As XmlNodeList

        Try

            If Not cbImportarExpedientes.Checked Then
                Return True
            End If

            If m_nodelist.Count = 0 Then
                lblTrabajos.Text = "¡¡No hay expedientes que procesar!!"
                Application.DoEvents()
            End If

            bp.Visible = True
            bp.Minimum = 1
            bp.Maximum = m_nodelist.Count
            bp.Value = 1
            bp.Step = 1

            For Each m_node In m_nodelist

                If cbImportarExpedientes.Checked Then
                    If Not RellenaExpediente(m_node) Then
                        Return False
                    End If
                End If

                bp.PerformStep()
                Application.DoEvents()
                'End If
            Next

            bp.Visible = False
            Return True

        Catch ex As Exception
            MsgBox("Error importando expedientes: " & ex.Message & " -> " & ex.ToString)
            bp.Visible = False
            Return False
        End Try

    End Function

    Private Function ProcesarPagos(m_nodelist As XmlNodeList) As Boolean
        Dim aExpedientes As XmlDocument = New XmlDocument()
        Dim aPagos As XmlNodeList
        Dim aCobros As XmlNodeList
        Dim i As Integer
        Dim j As Integer
        Dim NodoCobro As XmlNode
        Dim NodoPago As XmlNode
        Dim NodosHijos As XmlNodeList

        Try

            If m_nodelist.Count = 0 Then
                lblTrabajos.Text = "¡¡No hay pagos que procesar!!"
                Application.DoEvents()
            End If

            bp.Visible = True
            bp.Minimum = 1
            bp.Maximum = m_nodelist.Count
            bp.Value = 1
            bp.Step = 1

            For Each m_node In m_nodelist
                If Not VuelcaDatosPagos(m_node) Then
                    Return False
                End If
                bp.PerformStep()
                Application.DoEvents()
            Next

            bp.Visible = False
            Return True

        Catch ex As Exception
            MsgBox("Error importando expedientes: " & ex.Message & " -> " & ex.ToString)
            bp.Visible = False
            Return False
        End Try

    End Function

    Private Function ProcesarCobros(m_nodelist As XmlNodeList) As Boolean
        Dim aExpedientes As XmlDocument = New XmlDocument()
        Dim aPagos As XmlNodeList
        Dim aCobros As XmlNodeList
        Dim i As Integer
        Dim j As Integer
        Dim NodoCobro As XmlNode
        Dim NodoPago As XmlNode
        Dim NodosHijos As XmlNodeList

        Try

            If m_nodelist.Count = 0 Then
                lblTrabajos.Text = "¡¡No hay pagos que procesar!!"
                Application.DoEvents()
                Return True
            End If

            bp.Visible = True
            bp.Minimum = 1
            bp.Maximum = m_nodelist.Count
            bp.Value = 1
            bp.Step = 1

            For Each m_node In m_nodelist
                If Not VuelcaDatosCobros(m_node) Then
                    Return False
                End If
                'Next
                bp.PerformStep()
                Application.DoEvents()
            Next

            bp.Visible = False
            Return True

        Catch ex As Exception
            MsgBox("Error importando expedientes: " & ex.Message & " -> " & ex.ToString)
            bp.Visible = False
            Return False
        End Try

    End Function

    Private Function DevuelveXML(cCod, cSession, Optional dFechaIni = Nothing, Optional dFechaFin = Nothing, Optional cSalidacoop = Nothing, Optional ExpedId = Nothing, Optional Estado = Nothing)
        Dim cRet As String

        cRet = "<?xml version='1.0' encoding='utf-8'?>" ' + vbCrLf
        cRet = cRet + "<soap12:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap12='http://www.w3.org/2003/05/soap-envelope'>" ' + vbCrLf
        cRet = cRet + "<soap12:Body>" ' + vbCrLf
        cRet = cRet + "<DescargaPedidos xmlns='http://tempuri.org/'>" ' + vbCrLf
        cRet = cRet + "<usuarioCodigo>" & Trim(cCod) & "</usuarioCodigo>" ' + vbCrLf
        cRet = cRet + "<sessionKey>" & Trim(cSession) & "</sessionKey>" ' + vbCrLf
        If Not IsNothing(dFechaIni) Then
            cRet = cRet + "<fechaInicio>" & XMLFecha(dFechaIni) & "</fechaInicio>" ' + vbCrLf
        End If
        If Not IsNothing(dFechaFin) Then
            cRet = cRet + "<fechaFin>" & XMLFecha(dFechaFin) & "</fechaFin>" ' + vbCrLf
        End If
        If Not IsNothing(cSalidacoop) Then
            cRet = cRet + "<salidaCoopNumero>" & cSalidacoop & "</salidaCoopNumero>" ' + vbCrLf
        End If
        If Not IsNothing(ExpedId) Then
            cRet = cRet + "<expedienteID>" & ExpedId & "</expedienteID>" ' + vbCrLf
        End If
        If IsNothing(Estado) Then
            cRet = cRet + "<estado>0</estado>"
        ElseIf Estado = 0 Then
            cRet = cRet + "<estado>0</estado>"
        ElseIf Estado = 1 Then
            cRet = cRet + "<estado>1</estado>"
        End If
        cRet = cRet + "</DescargaPedidos>" ' + vbCrLf
        cRet = cRet + "</soap12:Body>" ' + vbCrLf
        cRet = cRet + "</soap12:Envelope>" ' + vbCrLf
        DevuelveXML = cRet

    End Function
    Private Function XMLFecha(ByVal dDate As Date)
        Dim cRet As String

        cRet = ""
        If IsDate(dDate) Then
            cRet = Format(dDate, "yyyy-MM-dd") + "T00:00:00"
        End If
        XMLFecha = cRet

    End Function
    Private Function DevuelveValor(Nodos As XmlNode, cNombreEt As String, cTipo As String, Optional Defecto As Object = "", Optional nDec As Integer = 0)
        Dim uRetorno As Object

        Try
            If Not IsDBNull(Nodos.SelectSingleNode(cNombreEt)) And Not IsNothing(Nodos.SelectSingleNode(cNombreEt)) Then
                uRetorno = Nodos.SelectSingleNode(cNombreEt).InnerText
                If Strings.UCase(uRetorno) = "NULL" Then
                    If IsNothing(Defecto) Then
                        Return ""
                    Else
                        Return RetornaValor(Defecto, cTipo)
                    End If
                End If

                If IsNothing(Defecto) Then
                    Return RetornaValor(Nodos.SelectSingleNode(cNombreEt).InnerText, cTipo, Defecto)
                Else
                    Return RetornaValor(Nodos.SelectSingleNode(cNombreEt).InnerText, cTipo)
                End If
            Else
                Return Defecto
            End If

        Catch ex As Exception
            Return Defecto
        End Try

    End Function
    Private Function RetornaValor(uValue As String, cTipo As String, Optional Defecto As Object = Nothing, Optional dec As Integer = 0)

        Try
            If IsNothing(uValue) Then
                Return Nothing
            End If

            If Not IsNothing(Defecto) Then
                If IsNothing(uValue) Then
                    'Return Defecto
                    uValue = Defecto
                End If
            End If
            If IsNothing(uValue) Then
                Return Nothing
            End If

            Select Case cTipo
                Case "C"
                    Return CStr(uValue)
                Case "S"
                    If Not IsNothing(uValue) Then
                        Return Trim(CStr(uValue))
                    Else
                        Return CStr(uValue)
                    End If
                Case "I"
                    Return CInt(uValue)
                Case "D"
                    Return CDbl(Replace(uValue, ".", ","))
                Case "L"
                    Return CLng(uValue)
                Case "B"
                    Return CBool(uValue)
                Case "F"
                    Return Format(AnotaFechaCorrecta(uValue), "dd/MM/yy")
                Case Else
                    Return CStr(uValue)
            End Select

        Catch ex As Exception
            Return Nothing
        End Try

        Return Nothing

    End Function


    Private Function RellenaExpediente(oNodo As XmlNode) As Boolean
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Añadiendo As Boolean
        Dim cExpediente
        Dim cCampanya
        Dim cDelegacion
        Dim cEnvio

        Try

            Añadiendo = False
            cExpediente = DevuelveValor(oNodo, "LINEA_EXPEDIENTE", "S")
            cCampanya = DevuelveValor(oNodo, "CODIGO_CAMPANYA", "S")
            cDelegacion = DevuelveValor(oNodo, "CODIGO_DELEGACION", "S", "1")
            cEnvio = DevuelveValor(oNodo, "EXPEDIENTE_ID", "S")
            cEnvio = CStr(Val(cEnvio))

            lblTrabajos.Text = "Procesando expedientes:" & vbCrLf & "Número: " & cExpediente & " Campaña: " & cCampanya & " Envío: " & cEnvio
            Application.DoEvents()

            If Trim(txtEjercicio.Text) <> "" Then
                If Trim(cCampanya) <> Trim(txtEjercicio.Text) Then
                    Return True
                End If
            End If

            rs.Open("SELECT * FROM EXPEDIENTES WHERE EJERCICIO_ANECOOP='" & Trim(cCampanya) & "' AND ltrim(DELEGACION)='" & Trim(cDelegacion) & "' AND ENVIO='" & Trim(cEnvio) & "' AND EXPEDIENTE='" & Trim(cExpediente) & "'", ObjetoGlobal.cn, True,,,,,, trans)
            If rs.RecordCount = 0 Then
                Añadiendo = True
                rs.AddNew()
            End If

            With rs
                !ejercicio_anecoop = cCampanya
                !delegacion = cDelegacion
                !envio = cEnvio
                !expediente = cExpediente
                !albaran_1 = DevuelveValor(oNodo, "NUMERO_SALIDA_COOPERATIVA", "S")
                !albaran_2 = DevuelveValor(oNodo, "N_PEDIDO_ANECOOP", "S")
                !albaran_3 = DevuelveValor(oNodo, "N_PEDIDO", "S")
                !Albaran_4 = DevuelveValor(oNodo, "N_LINEA", "S")
                !albaran_5 = DevuelveValor(oNodo, "NUMERO_SALIDA_ANECOOP", "S")
                !periodo_exp = DevuelveValor(oNodo, "PERIODO", "S")
                !fecha_salida = DevuelveValor(oNodo, "FECHA_SALIDA", "F")
                !matricula = DevuelveValor(oNodo, "MATRICULA", "S")
                !nombre_tipo_vehic = DevuelveValor(oNodo, "TIPO_VEHÍCULO", "S")
                !codigo_categoria = DevuelveValor(oNodo, "CATEGORÍA", "S")
                !nombre_categoria = DevuelveValor(oNodo, "NOMBRE_CATEGORIA", "S")
                !codigo_confeccion = DevuelveValor(oNodo, "CODIGO_CONFECCION", "S", "")
                !nombre_confeccion = DevuelveValor(oNodo, "NOMBRE_CONFECCION", "S")
                !kilos_piezas_litro = DevuelveValor(oNodo, "UNIDAD_PESO", "S")
                !codigo_envase = DevuelveValor(oNodo, "CODIGO_MODELO", "S", "")
                '!nombre_envase = DevuelveValor(oNodo, "NOMBRE_MODELO", "S")
                !codigo_embalaje = DevuelveValor(oNodo, "CODIGO_MATERIAL", "S")
                !nombre_embalaje = Strings.Left(DevuelveValor(oNodo, "NOMBRE_MATERIAL", "S"), 12)
                !codigo_marca = DevuelveValor(oNodo, "CODIGO_MARCA", "S", "")
                !nombre_marca = DevuelveValor(oNodo, "NOMBRE_MARCA", "S")
                !codigo_variedad = DevuelveValor(oNodo, "CODIGO_VARIEDAD", "S", "")
                !nombre_variedad = DevuelveValor(oNodo, "NOMBRE_VARIEDAD", "S")
                !codigo_cliente = DevuelveValor(oNodo, "CODIGO_CLIENTE", "S", "")
                !nombre_cliente = DevuelveValor(oNodo, "NOMBRECLIENTE", "S")
                !pais_cliente = DevuelveValor(oNodo, "CODIGO_PAIS", "S")
                !nombre_pais_ane = ""
                !codigo_moneda = DevuelveValor(oNodo, "CODIGO_DIVISA_CLIENTE", "S")
                !codigo_proveedor = DevuelveValor(oNodo, "CODIGO_COOPERATIVA_GESTORA", "D", 0)
                !nombre_proveedor = DevuelveValor(oNodo, "NOMBRE_GESTORA", "S", "")
                !punto_carga = DevuelveValor(oNodo, "PTO_CARGA", "S", "")
                !nombre_punto_carga = DevuelveValor(oNodo, "NOMBRE_PTO_CARGA", "S", "")
                If Añadiendo = True Then
                    !codigo_producto_ane = DevuelveValor(oNodo, "CODIGO_PRODUCTO", "S", "FIOR")
                ElseIf IsDBNull(!codigo_producto_ane) Then
                    !codigo_producto_ane = DevuelveValor(oNodo, "CODIGO_PRODUCTO", "S")
                ElseIf Trim(!codigo_producto_ane) = "" Then
                    !codigo_producto_ane = DevuelveValor(oNodo, "CODIGO_PRODUCTO", "S")
                End If
                !codigo_calibre = DevuelveValor(oNodo, "CODIGO_CALIBRE", "S")
                !nombre_calibre = DevuelveValor(oNodo, "NOMBRE_CALIBRE", "S")
                !numero_palets = DevuelveValor(oNodo, "NPALET", "D", 0)
                !tipo_palet = DevuelveValor(oNodo, "TIPO_PALET", "S")
                !nombre_tipo_palet = DevuelveValor(oNodo, "NOMBRE_TIPO_PALET", "S")
                If DevuelveValor(oNodo, "CODIGO_PRODUCTO", "S", "FIOR") = "FIOR" And DevuelveValor(oNodo, "NCAJAS", "D", 0) = 0 Then
                    !Bultos_envio = DevuelveValor(oNodo, "PESO_NETO", "D", 0)
                Else
                    !Bultos_envio = DevuelveValor(oNodo, "NCAJAS", "D", 0)
                End If
                !Kilos_envio = DevuelveValor(oNodo, "PESO_NETO", "D", 0)
                !precio_comercial = DevuelveValor(oNodo, "PRECIO_COMERCIAL", "D", 0, 5)
                !tipo_liquidacion = "S" 'DevuelveValor(oNodo, "LIQUIDADO_SON", "S")
                !divisa_p_comercial = DevuelveValor(oNodo, "CODIGO_DIVISA", "S")
                !num_liq_p = 0
                !fra_liq_p = System.DBNull.Value
                !carta_liq_p = System.DBNull.Value
                !registro_liq_p = System.DBNull.Value
                !importe_liq_p = 0
                !importe_iva_liq_p = 0
                !tipo_iva_liq_p = System.DBNull.Value
                !fecha_liq_P = System.DBNull.Value
                !CAMBIO_LIQ_P = 0
                If Añadiendo Then
                    '!num_liq_d = DevuelveValor(oNodo, "NUM_LIQ", "D", 0)
                    '!fra_liq_d = DevuelveValor(oNodo, "FRA_LIQ", "S")
                    '!carta_liq_d = DevuelveValor(oNodo, "CARTA_LIQ", "S")
                    '!registro_liq_d = DevuelveValor(oNodo, "REGISTRO_LIQ", "S")
                    '!importe_liq_d = DevuelveValor(oNodo, "IMPORTE_LIQ", "D", 0, 3)
                    '!importe_iva_liq_d = DevuelveValor(oNodo, "IMPORTE_IVA_LIQ", "D", 0, 3)
                    '!tipo_iva_liq_d = DevuelveValor(oNodo, "TIPO_IVA_LIQ", "S")
                    '!fecha_liq_d = DevuelveValor(oNodo, "FECHA_LIQ", "F")
                    '!fecha_cambio_liq_d = DevuelveValor(oNodo, "FECHA_CAMBIO_LIQ", "F")
                    '!cambio_liq_d = DevuelveValor(oNodo, "CAMBIO_LIQ", "D", 0)
                    If DevuelveValor(oNodo, "NUM_LIQ", "D", 0) <> 0 Then
                        !num_liq_d = DevuelveValor(oNodo, "NUM_LIQ", "D", 0)
                    End If
                    If DevuelveValor(oNodo, "FRA_LIQ", "S", "") <> "" Then
                        !fra_liq_d = DevuelveValor(oNodo, "FRA_LIQ", "S", "")
                    End If
                    If DevuelveValor(oNodo, "CARTA_LIQ", "S", "") <> "" Then
                        !carta_liq_d = DevuelveValor(oNodo, "CARTA_LIQ", "S", "")
                    End If
                    If DevuelveValor(oNodo, "REGISTRO_LIQ", "S", "") <> "" Then
                        !registro_liq_d = DevuelveValor(oNodo, "REGISTRO_LIQ", "S", "")
                    End If
                    If DevuelveValor(oNodo, "IMPORTE_LIQ", "D", 0, 3) <> 0 Then
                        !importe_liq_d = DevuelveValor(oNodo, "IMPORTE_LIQ", "D", 0, 3)
                    End If
                    If DevuelveValor(oNodo, "IMPORTE_IVA_LIQ", "D", 0, 3) <> 0 Then
                        !importe_iva_liq_d = DevuelveValor(oNodo, "IMPORTE_IVA_LIQ", "D", 0, 3)
                    End If
                    If DevuelveValor(oNodo, "TIPO_IVA_LIQ", "S", "") <> "" Then
                        !tipo_iva_liq_d = DevuelveValor(oNodo, "TIPO_IVA_LIQ", "S")
                    End If
                    If DevuelveValor(oNodo, "FECHA_LIQ", "F", "") <> "" Then
                        !fecha_liq_d = DevuelveValor(oNodo, "FECHA_LIQ", "F")
                    End If
                    If DevuelveValor(oNodo, "FECHA_CAMBIO_LIQ", "F", "") <> "" Then
                        !fecha_cambio_liq_d = DevuelveValor(oNodo, "FECHA_CAMBIO_LIQ", "F")
                    End If
                    If DevuelveValor(oNodo, "CAMBIO_LIQ", "D", 0) <> 0 Then
                        !cambio_liq_d = DevuelveValor(oNodo, "CAMBIO_LIQ", "D", 0)
                    End If
                Else
                    If DevuelveValor(oNodo, "NUM_LIQ", "D", 0) <> 0 Then
                        !num_liq_d = DevuelveValor(oNodo, "NUM_LIQ", "D", 0)
                    End If
                    If DevuelveValor(oNodo, "FRA_LIQ", "S", "") <> "" Then
                        !fra_liq_d = DevuelveValor(oNodo, "FRA_LIQ", "S", "")
                    End If
                    If DevuelveValor(oNodo, "CARTA_LIQ", "S", "") <> "" Then
                        !carta_liq_d = DevuelveValor(oNodo, "CARTA_LIQ", "S", "")
                    End If
                    If DevuelveValor(oNodo, "REGISTRO_LIQ", "S", "") <> "" Then
                        !registro_liq_d = DevuelveValor(oNodo, "REGISTRO_LIQ", "S", "")
                    End If
                    If DevuelveValor(oNodo, "IMPORTE_LIQ", "D", 0, 3) <> 0 Then
                        !importe_liq_d = DevuelveValor(oNodo, "IMPORTE_LIQ", "D", 0, 3)
                    End If
                    If DevuelveValor(oNodo, "IMPORTE_IVA_LIQ", "D", 0, 3) <> 0 Then
                        !importe_iva_liq_d = DevuelveValor(oNodo, "IMPORTE_IVA_LIQ", "D", 0, 3)
                    End If
                    If DevuelveValor(oNodo, "TIPO_IVA_LIQ", "S", "") <> "" Then
                        !tipo_iva_liq_d = DevuelveValor(oNodo, "TIPO_IVA_LIQ", "S")
                    End If
                    If DevuelveValor(oNodo, "FECHA_LIQ", "F", "") <> "" Then
                        !fecha_liq_d = DevuelveValor(oNodo, "FECHA_LIQ", "F")
                    End If
                    If DevuelveValor(oNodo, "FECHA_CAMBIO_LIQ", "F", "") <> "" Then
                        !fecha_cambio_liq_d = DevuelveValor(oNodo, "FECHA_CAMBIO_LIQ", "F")
                    End If
                    If DevuelveValor(oNodo, "CAMBIO_LIQ", "D", 0) <> 0 Then
                        !cambio_liq_d = DevuelveValor(oNodo, "CAMBIO_LIQ", "D", 0)
                    End If
                End If

                !valor_mercancia = DevuelveValor(oNodo, "VALOR_MERCANCIA", "D", 0, 3)
                !valor_confeccion = DevuelveValor(oNodo, "VALOR_CONFECCION", "D", 0, 3)

                !situacion_cobro = DevuelveValor(oNodo, "COBRADO_SON", "S")
                !grupo_sn = DevuelveValor(oNodo, "LIQUIDACION_AGRUPADA_SON", "S")
                !anticipado = DevuelveValor(oNodo, "PAGADO_SON", "S")
                !tipo_vehiculo = DevuelveValor(oNodo, "TIPO_VEHICULO", "S")
                !grupo_liquidacion = DevuelveValor(oNodo, "CODIGO_GRUPO_LIQUIDACION", "S", "")
                !situacion = DevuelveValor(oNodo, "ESTADO_COOP_EXPEDIENTE", "S", "")
                !porcent_iva_liq_d = DevuelveValor(oNodo, "PORCENT_IVA_LIQ", "D", 0, 2)
                !importe2_iva_liq_d = DevuelveValor(oNodo, "IMPORTE2_IVA_LIQ", "D", 0, 3)

                If Añadiendo = True Then
                    !enlace_prov = "N"
                    !enlace_def = "N"
                    !pagado = 0
                    !bloqueo = "N"
                    '!usucreacion = ObjetoGlobal.nombreusuario
                    !fechacreacion = Date.Now
                    !horacreacion = DateTime.Now.ToString("hh:mm:ss")
                Else
                    '!usumodificacion = ObjetoGlobal.nombreusuario
                    !fechamodificacion = Date.Now
                    !horamodificacion = DateTime.Now.ToString("hh:mm:ss")
                End If
            End With
            rs.Update()
            rs.Close()
            Return True
        Catch ex As Exception
            MsgBox("Error en la importación de expedientes: " & ex.Message & " -> " & ex.ToString)
            Return False
        End Try
        rs.Close()

    End Function

    Private Function VuelcaDatosCobros(oNodo) As Boolean
        Dim cExpediente As String
        Dim cCampanya As String
        Dim cDelegacion As String
        Dim cEnvio As String
        Dim cNumCobro As String
        Dim Añadiendo As Boolean
        Dim sSql As String
        Dim RsCobros_anecoop As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Try
            Añadiendo = False
            cExpediente = "1"
            cCampanya = DevuelveValor(oNodo, "CODIGO_CAMPANYA", "S")
            cDelegacion = "1"
            cEnvio = DevuelveValor(oNodo, "EXPEDIENTE_ID", "S")
            cEnvio = CStr(Val(cEnvio))
            cNumCobro = DevuelveValor(oNodo, "EXPEDIENTE_COBROID", "S")

            If Trim(txtEjercicio.Text) <> "" Then
                If Trim(cCampanya) <> Trim(txtEjercicio.Text) Then
                    Return True
                End If
            End If

            lblTrabajos.Text = "Procesando cobros:" & vbCrLf & "Número: " & cExpediente & " Campaña: " & cCampanya & " Envío: " & cEnvio
            Application.DoEvents()

            sSql = "SELECT * COBROS_ANECOOP WHERE ejercicio_anecoop ='" & cCampanya & "' AND"
            sSql = sSql + " delegacion ='" & cDelegacion & "' AND "
            sSql = sSql + " envio ='" & cEnvio & "' AND "
            sSql = sSql + " expediente ='" & cExpediente & "' AND "
            sSql = sSql + " numero_cobro =" & cNumCobro

            RsCobros_anecoop.Open(sSql, ObjetoGlobal.cn, True,,,,,, trans)
            If RsCobros_anecoop.RecordCount = 0 Then
                Añadiendo = True
                RsCobros_anecoop.AddNew()
            End If

            With RsCobros_anecoop
                !ejercicio_anecoop = cCampanya
                !delegacion = cDelegacion
                !envio = cEnvio
                !expediente = cExpediente
                !numero_cobro = CLng(cNumCobro)
                !numero_remesa = DevuelveValor(oNodo, "NUMERO_REMESA", "D", 0)
                !importe_cambio = DevuelveValor(oNodo, "IMPORTE_CAMBIO", "D", 1)
                !fecha_cambio = DevuelveValor(oNodo, "FECHA_CAMBIO", "F")
                !situacion = DevuelveValor(oNodo, "ESTADO", "S")
                !codigo_proveedor = DevuelveValor(oNodo, "CODIGO_COOPERATIVA_GESTORA", "D", 46)
                !importe_cobro = 0
                !situacion = DevuelveValor(oNodo, "ESTADO", "S")
            End With
            RsCobros_anecoop.Update()
            RsCobros_anecoop.Close()
            Return True

        Catch ex As Exception
            RsCobros_anecoop.Close()
            MsgBox("Error al volvar pagos " & ex.Message & " -> " & ex.ToString())
            Return False
        End Try

    End Function

    Private Function VuelcaDatosPagos(oNodo) As Boolean
        Dim cExpediente As String
        Dim cCampanya As String
        Dim cDelegacion As String
        Dim cEnvio As String
        Dim cNumPago As String
        Dim Añadiendo As Boolean
        Dim sSql As String
        Dim cTipoPago As String
        Dim RsPagos_anecoop As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Try
            Añadiendo = False
            cExpediente = "1"
            cCampanya = DevuelveValor(oNodo, "CODIGO_CAMPANYA", "S")
            cDelegacion = "1"
            cEnvio = DevuelveValor(oNodo, "EXPEDIENTE_ID", "S")
            cEnvio = CStr(Val(cEnvio))
            cNumPago = DevuelveValor(oNodo, "EXPEDIENTE_PAGOID", "S")

            If Trim(txtEjercicio.Text) <> "" Then
                If Trim(cCampanya) <> Trim(txtEjercicio.Text) Then
                    Return True
                End If
            End If

            lblTrabajos.Text = "Procesando pagos:" & vbCrLf & "Número: " & cExpediente & " Campaña: " & cCampanya & " Envío: " & cEnvio
            Application.DoEvents()

            sSql = "SELECT * FROM PAGOS_ANECOOP WHERE ejercicio_anecoop ='" & cCampanya & "' AND"
            'sSql = "SELECT * FROM pagos_ane_test WHERE ejercicio_anecoop ='" & cCampanya & "' AND"
            sSql = sSql + " delegacion ='" & cDelegacion & "' AND "
            sSql = sSql + " envio ='" & cEnvio & "' AND "
            sSql = sSql + " expediente ='" & cExpediente & "' AND "
            sSql = sSql + " numero_pago =" & cNumPago

            RsPagos_anecoop.Open(sSql, ObjetoGlobal.cn, True,,,,,, trans)

            If RsPagos_anecoop.RecordCount = 0 Then
                Añadiendo = True
                RsPagos_anecoop.AddNew()
                RsPagos_anecoop!ejercicio_anecoop = cCampanya
                RsPagos_anecoop!delegacion = cDelegacion
                RsPagos_anecoop!envio = cEnvio
                RsPagos_anecoop!expediente = cExpediente
                RsPagos_anecoop!numero_pago = CDbl(cNumPago)
                RsPagos_anecoop!enlazado = "N"
            End If

            With RsPagos_anecoop

                '!ejercicio_anecoop = cCampanya
                '!delegacion = cDelegacion
                '!envio = cEnvio
                '!expediente = cExpediente
                '!numero_pago = CDbl(cNumPago)
                !LIQUIDACION = DevuelveValor(oNodo, "NUM_LIQUIDACION", "D")
                !Importe = DevuelveValor(oNodo, "IMPORTE", "D", 0)

                cTipoPago = DevuelveValor(oNodo, "TIPO_PAGO", "S")
                If Trim(cTipoPago) = "P" Or Trim(cTipoPago) = "PL" Or Trim(cTipoPago) = "M" Then
                    !concepto_anecoop = "Mercancia"
                    !Tipo_iva = "S"
                    !situacion = "A"
                    !situacion2 = "P"

                ElseIf Trim(cTipoPago) = "I" Or Trim(cTipoPago) = "IM" Then
                    !concepto_anecoop = "Iva Definitivo"
                    !Tipo_iva = "I"
                    !situacion = "A"
                    !situacion2 = "P"
                End If
                !fecha_pago = DevuelveValor(oNodo, "FECHA_PAGO", "F")
                !Factura = damefactura(cCampanya, cDelegacion, cEnvio, cExpediente)
                !fecha_factura = DevuelveValor(oNodo, "FECHA_FACTURA", "F")
                !fecha_sc = DevuelveValor(oNodo, "FECHA_PAGO_SC", "F")
                !codigo_proveedor = DevuelveValor(oNodo, "CODIGO_COOPERATIVA_GESTORA", "D", 46)
                !fecha_act = Date.Now
                !referencia_pago = "1"
                !ejercicio_ref_pago = ""
            End With
            RsPagos_anecoop.Update()
            RsPagos_anecoop.Close()
            Return True
        Catch ex As Exception
            'RsPagos_anecoop.Close()
            MsgBox("Error al volvar pagos " & ex.Message & " -> " & ex.ToString())
            Return False
        End Try

    End Function


    Private Function AnotaFechaCorrecta(cCad)
        Dim cRet As String

        cRet = Mid(cCad, 9, 2) + "-" + Mid(cCad, 6, 2) + "-" + Mid(cCad, 1, 4)
        AnotaFechaCorrecta = CDate(cRet)

    End Function

    Private Function damefactura(cEjec, cdel, cenv, cexp)
        Dim RsExpedientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        RsExpedientes.Open("SELECT * FROM expedientes WHERE ejercicio_anecoop ='" & cEjec & "' AND delegacion='" & cdel & "' AND envio='" & cenv & "' AND expediente='" & cexp & "'", ObjetoGlobal.cn, False,,,,,, trans)
        If RsExpedientes.RecordCount > 0 Then
            damefactura = RsExpedientes!fra_liq_d
            Exit Function
        End If
        damefactura = ""

    End Function

    Private Function hacerfichero(ByVal fich As String) As Boolean
        Dim strStreamW As Stream
        Dim result = "c:\ficherosxml\" 'Path.GetTempPath()
        Dim PathFile As String = result + fich
        'Dim file As System.IO.StreamWriter
        'Dim fs As FileStream = Nothing

        If file.Exists(PathFile) Then
            file.Delete(PathFile)
        End If

        Try

            'Catch ex As Exception

            'End Try
            ''Se abre el archivo y si este no existe se crea
            'Using (StreamWriter sw = New StreamWriter(PathFile))
            '           sw.StreamWriter(

            '        String fileName = "test.txt";
            '    String textToAdd = "Example text in file";

            '    Try
            '    {
            '        fs = New FileStream(PathFile, FileMode.CreateNew);
            '        Using (StreamWriter writer = New StreamWriter(fs))
            '        {
            '            writer.Write(textToAdd);
            '        }
            '    }
            '    Finally
            '    {
            '        If (fs!= null) Then
            '                fs.Dispose();
            '   File = My.Computer.FileSystem.OpenTextFileWriter("c:\test.txt", True)

            strStreamW = File.OpenWrite(PathFile)
            strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.UTF8)
            NombreArchivo = PathFile
            Return True

        Catch ex As Exception
                MsgBox("Imposible abrir el archivo de los registros")
            Return False
        End Try


    End Function
    Private Function GrabarTextoArchivo(ByVal cArg As String) As Boolean
        Try
            strStreamWriter.Write(cArg)
            strStreamWriter.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub importar_Click(sender As Object, e As EventArgs) Handles importar.Click
        DescargaExpedientes()
    End Sub

    Private Function LimpiarRespuesta(ByVal CadXML As String) As String
        Dim RespuestaXML As String
        RespuestaXML = CadXML.Replace("" & vbCrLf & "", Nothing)
        RespuestaXML = FixXML(RespuestaXML)
        Return RespuestaXML

    End Function

    Private Function FixXML(ByVal sXML As String) As String
        Dim cRetorno As String
        Dim cCad As String
        Dim lPos As Long, i As Long
        Dim hay As Boolean = False

        If Len(sXML) = 0 Then
            Return ""
        End If

        For i = 1 To Len(sXML)
            cCad = Mid(sXML, i, 1)
            If cCad = """" Then
                ' If Not hay Then
                cRetorno = cRetorno + Chr(34)
                '    hay = True
                ' End If
            Else
                hay = False
                cRetorno = cRetorno + cCad
            End If
        Next
        Return cRetorno

    End Function

    Private Sub FrmImportarexpedientes_Load(sender As Object, e As EventArgs) Handles Me.Load

        txtEjercicio.Text = Mid(ObjetoGlobal.EjercicioActual, 3, 2)
        TxtDesdelafecha.value = CDate("01/07/20" + txtEjercicio.Text)
        TxtHastalafecha.value = Format(Date.Now, "dd/MM/yyyy")

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Me.Close()
    End Sub
End Class