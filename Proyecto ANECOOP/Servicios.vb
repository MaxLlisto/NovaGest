



'Public Class Comunicaciones
'    Public Resultado As Boolean = False
'    Public aResult() As String 'JArray

'    '    'url=http//visualnacert.com/vnwebservices
'    '    'idUser=3369
'    '    'idOrganization=1072
'    '    'username=sync
'    '    'password=a47e82ff8d9f23327c2d89f0b6d01e2d0

'    Private Function Solicitudes(PteURL As String, cMetodo As String, cDatos As String) As String
'        'Se define la URL a donde se realizará la peticion de GetAuth
'        Dim URLPost As String = "http://ws.visualnacert.com/vnwebservices/user/3369/v3/"
'        Dim Fecha As String
'        Dim cadId As String
'        Dim Reply As String
'        Dim Clave As String = "Tyksol35vW543rt"
'        Dim oMyHmac As MyHmac
'        Dim cAut As String
'        Dim ByteReply() As Byte


'        Dim oHTTP As New ServerXMLHTTP40
'        Dim cPost As String
'        Dim cHost As String
'        Dim Cabecera As String
'        Dim cabecera1 As String
'        Dim oXML As New XMLDocument
'        Dim sessionkey As String
'        Dim cXML As String
'        Dim cCod As String
'        Dim nSeleccion As Integer
'        Dim objPeopleRoot As IXMLDOMElement
'        Dim objInicialRoot As IXMLDOMElement
'        Dim fso As Scripting.FileSystemObject
'        Dim fXML As TextStream
'        Dim sFichero As String



'        'DoEvents()

'        If Strings.Left(PteURL, 1) = "/" Then
'            PteURL = Strings.Mid(PteURL, 2)
'        End If

'        URLPost = URLPost & PteURL

'        Fecha = Format(DateTime.Now, "yyy/MM/ddHH:mm:ss")
'        cadId = "idorg=1072&user=sync&pwd=a47e82ff8d9f23327c2d89f0b6d01e2d0&date=" & Fecha

'        'Id user:   3369
'        'Id organización:   1072
'        'Username: sync

'        Application.DoEvents()

'        'ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
'        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


'        Using client As New MyWebClient
'            ' Set one of the headers.
'            client.Headers.Add("accept", "application/json, text/javascript, */*")
'            client.Headers.Add("Accept-Encoding", "gzip")
'            client.Headers.Add("accept-charset", "UTF-8")
'            client.Headers.Add("content-type", "application/json;charset=UTF-8")
'            oMyHmac = New MyHmac()
'            cAut = oMyHmac.CreateToken(cadId, Clave)
'            client.Headers.Add("authorization", cAut)
'            client.Headers.Add("datestring", Fecha)

'            If cMetodo = "POST" Then
'                Reply = client.UploadString(URLPost, "POST", cDatos)
'            ElseIf cMetodo = "DELETE" Then
'                Reply = client.UploadString(URLPost, "DELETE", "")
'            Else
'                ' Download data.
'                'Reply = client.DownloadString(URLPost)
'                ByteReply = client.DownloadData(URLPost)
'                Reply = UnicodeBytesToString(ByteReply)
'            End If
'        End Using

'        Application.DoEvents()
'        'return Encoding.UTF8.GetString(responseArray);
'        Return Reply

'    End Function

'    Public Function EnviarDatos(ByVal address As String, ByVal data As String) As JObject
'        Dim reply As String
'        Dim response As JObject
'        Dim Arr As JArray
'        Dim ojToken As JToken
'        Dim cjason As String

'        Application.DoEvents()

'        Resultado = True

'        reply = Solicitudes(address, "POST", data)

'        response = JObject.Parse(reply)

'        If response.Item("resultado").ToString = "ERROR" Then
'            Resultado = False
'        Else
'            Resultado = response.TryGetValue("data", ojToken)
'            If Resultado Then
'                Arr = New JArray
'                Arr.Add(ojToken)
'                'Resultado = response.TryGetValue("data", ojToken.Values)
'                'Arr = CType(ojToken, JArray)
'                'Arr = JArray.Parse(ojToken)
'                'Arr = ojToken.ToArray
'                aResult = Arr
'            End If
'        End If

'        Application.DoEvents()

'        Return response

'    End Function

'    Public Function RecibirDatos(ByVal address As String) As JObject
'        Dim reply As String
'        Dim response As JObject
'        Dim Arr As JArray
'        'Dim Resultado As Boolean

'        Application.DoEvents()

'        Resultado = True

'        reply = Solicitudes(address, "GET", "")

'        response = JObject.Parse(reply)

'        If response.Item("resultado").ToString = "ERROR" Then
'            Resultado = False
'        Else
'            Resultado = response.TryGetValue("data", Arr)
'            If Resultado Then
'                aResult = Arr
'            End If
'        End If

'        Application.DoEvents()

'        Return response

'    End Function


'    Protected Overrides Sub Finalize()
'        MyBase.Finalize()
'    End Sub


'    'Private Sub SurroundingSub()
'    '    Dim response As JObject = JObject.Parse(retStr)
'    '    Dim student_ref As String = response("user").ToString()
'    'End Sub
'    'JObject response = JObject.Parse(retStr);
'    '        String student_ref = response["user"].ToString();

'    '        Return student_ref;

'    'String transResp = Encoding.UTF8.GetString(b_transResp);
'    '                JObject trObject = JObject.Parse(transResp);
'    '                transText = trObject["translation"].ToString().TrimStart();

'    'Public Function SubHMACSHA256(textos As String, key As String) As String
'    '    Dim getmessage As String
'    '    Dim getkey As String
'    '    Dim ret As String
'    '    Dim encoding As New System.Text.ASCIIEncoding()
'    '    Dim getkeyByte As Byte() = encoding.GetBytes(key)
'    '    Dim hashmessage As Byte()

'    '    getkey = key
'    '    getmessage = textos

'    '    'Dim gethmacmd5 As New HMACMD5(getkeyByte)
'    '    'Dim gethmacsha1 As New HMACSHA1(getkeyByte)
'    '    Dim gethmacsha256 As New HMACSHA256(getkeyByte)
'    '    'Dim gethmacsha384 As New HMACSHA384(getkeyByte)
'    '    'Dim gethmacsha512 As New HMACSHA512(getkeyByte)
'    '    Dim getmessageBytes As Byte() = encoding.GetBytes(getmessage)
'    '    ' hashmessage  = gethmacmd5.ComputeHash(getmessageBytes)
'    '    'Me.txt1.Text = ByteToString(hashmessage)
'    '    'hashmessage = gethmacsha1.ComputeHash(getmessageBytes)
'    '    'Me.txt2.Text = ByteToString(hashmessage)
'    '    hashmessage = gethmacsha256.ComputeHash(getmessageBytes)
'    '    ret = ByteToString(hashmessage)
'    '    'hashmessage = gethmacsha384.ComputeHash(getmessageBytes)
'    '    'Me.txt4.Text = ByteToString(hashmessage)
'    '    'hashmessage = gethmacsha512.ComputeHash(getmessageBytes)
'    '    'Me.txt5.Text = ByteToString(hashmessage)
'    '    Return ret

'    'End Function

'    'Private Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal cert As X509Certificate, ByVal chain As X509Chain, ByVal [error] As SslPolicyErrors) As Boolean

'    '    If [error] = System.Net.Security.SslPolicyErrors.None Then
'    '        Return True
'    '    End If

'    '    MsgBox("X509Certificate [{0}] Policy Error: " & cert.Subject & [error].ToString())
'    '    Return False
'    'End Function

'    Public Function ByteToString(buff As Byte()) As String
'        Dim getbinary As String = ""
'        For i As Integer = 0 To buff.Length - 1
'            getbinary += buff(i).ToString("X2")
'        Next
'        Return (getbinary)
'    End Function
'    Private Function UnicodeBytesToString(ByVal bytes() As Byte) As String

'        Return System.Text.Encoding.UTF8.GetString(bytes)
'        System.Windows.Forms.Application.DoEvents()

'    End Function



'End Class

'Dim oHTTP As New ServerXMLHTTP40
'Dim cPost As String
'Dim cHost As String
'Dim Cabecera As String
'Dim cabecera1 As String
'Dim oXML As New DOMDocument
'Dim sessionkey As String
'Dim cXML As String
'Dim cCod As String
'Dim nSeleccion As Integer
'Dim objPeopleRoot As IXMLDOMElement
'Dim objInicialRoot As IXMLDOMElement
'Dim fso As Scripting.FileSystemObject
'Dim fXML As TextStream
'Dim sFichero As String

'Set fso = New Scripting.FileSystemObject

''Open "c:\temp\envio.xml" For Binary As 1
'On Error GoTo Errorenconexion

'cPost = "/services/ConsultaExpediente.asmx" ' HTTP/1.1"
''cHost = "integracionproveedores.anecoop.com"
'cHost = "anecoopintegracion.anecoop.com"

'oHTTP.Open "GET", "https://anecoopintegracion.anecoop.com/serviciosweb/login.asmx/CheckLogin?Usuario=adlliria&Empresa=46&Password=l82250a11"
''oHTTP.open "GET", "http://integracionproveedores.anecoop.com/serviciosweb/login.asmx/CheckLogin?Usuario=adlliria&Empresa=46&Password=l82250a11"
'oHTTP.send
'If oXML.loadXML(oHTTP.responseText) Then
'If Nuevos.Value Then
'nSeleccion = 0
'ElseIf Descargados.Value Then
'nSeleccion = 1
'Else
'nSeleccion = -1
'End If
'sessionkey = oXML.documentElement.childNodes(1).Text
'cCod = "46"
'cXML = DevuelveXML(cCod, sessionkey, DTPDesdelafecha.Value, DTPHastalaFecha.Value, Null, Null, nSeleccion)
''oHTTP.setTimeouts 9999999, 9999999, 9999999, 9999999 ' 0, 0, 0, 0
'oHTTP.Open "POST", "https://anecoopintegracion.anecoop.com/ServiciosWeb/ConsultaExpediente.asmx", False
'    'oHTTP.Open "POST", "http://" + cHost + cPost, False
'oHTTP.setRequestHeader "Post", cPost
'    oHTTP.setRequestHeader "Host", cHost
'    oHTTP.setRequestHeader "Content-Type", "application/soap+xml; charset=utf-8"
'    oHTTP.setRequestHeader "Content-length", CStr(Len(cXML))
'    oHTTP.send cXML
'    Text2.Text = oHTTP.Status & "  :  " & oHTTP.StatusText
''Put #1, , oHTTP.responseText
''Text1.Text = "ya está" 'oHTTP.responseText
''Close #1
'sFichero = Trim(Now)
'sFichero = Replace(sFichero, "/", "")
'sFichero = Replace(sFichero, " ", "")
'sFichero = Replace(sFichero, ":", "")


'Set fXML = fso.CreateTextFile("c:\" & "IMP_ANE" & Trim(sFichero) & ".xml", True)
'    fXML.Write(oHTTP.responseText)
'fXML.Close

'oXML.loadXML(oHTTP.responseText)

'Set objPeopleRoot = oXML.documentElement

'    'Set objInicialRoot = RetornaNodoInicial(objPeopleRoot.childNodes)
'    If RetornaNodoInicial(objPeopleRoot.childNodes, "DescargaPedidosResult", objInicialRoot) = 1 Then
'If objPeopleRoot.hasChildNodes Then
'If Ejercicio.Text <> "" Then
'ImportarExpedientes.Ejercicio = Ejercicio.Text
'End If
'          ProcesaExpedientes objInicialRoot.childNodes
'          MsgBox "Proceso concluido"
'       End If
'End If
'Else
'MsgBox "Mala conexion"
'End If
'Exit Sub
'Errorenconexion:
'MsgBox "Error en conexión, intentalo más tarde(" & Trim(Err.Description) & ")"

'End Sub

'Private Function DevuelveXML(cCod, cSession, Optional dFechaIni = Null, Optional dFechaFin = Null, Optional cSalidacoop = Null, Optional ExpedId = Null, Optional Estado)
'    Dim cRet As String

'    cRet = "<?xml version='1.0' encoding='utf-8'?>" ' + vbCrLf
'    cRet = cRet + "<soap12:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap12='http://www.w3.org/2003/05/soap-envelope'>" ' + vbCrLf
'    cRet = cRet + "<soap12:Body>" ' + vbCrLf
'    cRet = cRet + "<DescargaPedidos xmlns='http://tempuri.org/'>" ' + vbCrLf
'    cRet = cRet + "<usuarioCodigo>" & Trim(cCod) & "</usuarioCodigo>" ' + vbCrLf
'    cRet = cRet + "<sessionKey>" & Trim(cSession) & "</sessionKey>" ' + vbCrLf
'    If Not IsNull(dFechaIni) Then
'        cRet = cRet + "<fechaInicio>" & XMLFecha(dFechaIni) & "</fechaInicio>" ' + vbCrLf
'    Else
'        '    cRet = cRet + "<fechaInicio></fechaInicio>" ' + vbCrLf
'        'cRet = cRet + "<fechaInicio>10/01/07</fechaInicio>" ' + vbCrLf
'    End If
'    If Not IsNull(dFechaFin) Then
'        cRet = cRet + "<fechaFin>" & XMLFecha(dFechaFin) & "</fechaFin>" ' + vbCrLf
'    Else
'        '    cRet = cRet + "<fechaFin></fechaFin>" ' + vbCrLf
'        'cRet = cRet + "<fechaFin>10/31/07</fechaFin>" ' + vbCrLf
'    End If
'    If Not IsNull(cSalidacoop) Then
'        cRet = cRet + "<salidaCoopNumero>" & cSalidacoop & "</salidaCoopNumero>" ' + vbCrLf
'    Else
'        '   cRet = cRet + "<salidaCoopNumero></salidaCoopNumero>" ' + vbCrLf
'    End If
'    If Not IsNull(ExpedId) Then
'        cRet = cRet + "<expedienteID>" & ExpedId & "</expedienteID>" ' + vbCrLf
'    Else
'        '   cRet = cRet + "<expedienteID></expedienteID>" ' + vbCrLf
'    End If
'    If Estado = 0 Then
'        cRet = cRet + "<estado>0</estado>"
'    ElseIf Estado = 1 Then
'        cRet = cRet + "<estado>1</estado>"
'    End If
'    cRet = cRet + "</DescargaPedidos>" ' + vbCrLf
'    cRet = cRet + "</soap12:Body>" ' + vbCrLf
'    cRet = cRet + "</soap12:Envelope>" ' + vbCrLf
'    DevuelveXML = cRet

'End Function
'Private Function XMLFecha(ByVal dDate As Date)
'    Dim cRet As String

'    cRet = ""
'    If IsDate(dDate) Then
'        cRet = Format(dDate, "yyyy-mm-dd") + "T00:00:00"
'    End If
'    XMLFecha = cRet

'End Function


'Private Sub Form_Load()
'Set ImportarExpedientes.ObjetoGlobal = Me.ObjetoGlobal
'Ejercicio.Text = Mid(ObjetoGlobal.ejercicioActual, 3, 2)

'    DTPDesdelafecha.Value = CDate("01/10/20" + Ejercicio.Text)
'    DTPHastalaFecha.Value = Date
'    Todos.Value = True

'End Sub

