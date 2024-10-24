'Imports Microsoft.Web.Services3
'Namespace Cliente
'    Public Class App
'        Public Sub InvocarServicioWeb()
'            With New Services.WSRETO()
'                ' ==================================================================================================
'                ' Establecer los valores de la seguridad
'                ' ==================================================================================================
'                .SetClientCredential(New Security.Tokens.UsernameToken("Usuario", "contraseña", Security.Tokens.PasswordOption.SendPlainText))
'                Dim policy As Microsoft.Web.Services3.Design.Policy = New Microsoft.Web.Services3.Design.Policy()
'                policy.Assertions.Add(New Microsoft.Web.Services3.Design.UsernameOverTransportAssertion())
'                policy.Assertions.Add(New Microsoft.Web.Services3.Design.RequireActionHeaderAssertion())
'                .SetPolicy(policy)
'                ' ==================================================================================================
'                ' Realizar la llamada al método del servicio web
'                ' ==================================================================================================
'                Dim res As String = .GetItem_Transaccion(30)
'                ' ==================================================================================================
'                ' Procesar la respuesta
'                ' ==================================================================================================
'                Dim doc As New System.Xml.XmlDocument()
'                doc.LoadXml(res)
'                Select Case doc.SelectSingleNode("Response/Code/text()").Value
'                    Case "SUCCESS"
'                        System.Diagnostics.Debug.WriteLine("Datos grabados correctamente")
'                    Case "FAIL"
'                        Dim mensaje As String = doc.SelectSingleNode("Response/Message/text()").Value
'                        Dim causas As String = doc.SelectSingleNode("Response/Errores/Error/Causes/text()").Value
'                        Dim errores As String() = doc.SelectSingleNode("Response/Errores/Error/Description/text()") _
'                        .Value.Split("-")
'                        System.Diagnostics.Debug.WriteLine(mensaje)
'                    Case "ERROR"
'                        Dim mensaje As String = doc.SelectSingleNode("Response/Message/text()").Value
'                        System.Diagnostics.Debug.WriteLine(mensaje)
'                End Select
'            End With
'        End Sub
'    End Class
'End Namespace