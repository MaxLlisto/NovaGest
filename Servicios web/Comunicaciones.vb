Imports System.Net
Imports Newtonsoft.Json.Linq
    'Imports System.Text
    Imports System
    Imports System.Text
    Imports System.IO
Imports System.Security.Cryptography
Imports System.Windows.Forms


'Public Class Comunicaciones
'    Public Resultado As Boolean = False
'    Public aResult As JArray

'    'url=http//visualnacert.com/vnwebservices
'    'idUser=3369
'    'idOrganization=1072
'    'username=sync
'    'password=a47e82ff8d9f23327c2d89f0b6d01e2d0

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


'    Public Function EliminarDatos(ByVal address As String) As JObject
'        Dim reply As String
'        Dim response As JObject
'        Dim Arr As JArray

'        Resultado = True

'        System.Windows.Forms.Application.DoEvents()

'        reply = Solicitudes(address, "DELETE", "")

'        response = JObject.Parse(reply)

'        If response.Item("resultado").ToString = "ERROR" Then
'            Resultado = False
'        Else
'            Resultado = True
'            'Resultado = response.TryGetValue("data", Arr)
'            'If Resultado Then
'            '    aResult = Arr
'            'End If
'        End If

'        System.Windows.Forms.Application.DoEvents()

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
