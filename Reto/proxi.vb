'Imports System.Web
'Namespace Services

'    <System.CodeDom.Compiler.GeneratedCodeAttribute("WsdlReader", "1.0.0.0"), _
'System.Diagnostics.DebuggerStepThroughAttribute(), _
'System.ComponentModel.DesignerCategoryAttribute("code"), _
'System.Web.Services.WebServiceBindingAttribute(Name:="RETOWSSoap", [Namespace]:="http://mapama.es/services/w
'sreto")> _
'Partial Public Class WSRETO
' Inherits Microsoft.Web.Services3.WebServicesClientProtocol

' Public Sub New()
' MyBase.New
' Me.Url = "https://servicio.mapama.gob.es/wsreto/services/wsreto.asmx"
' End Sub

' <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mapama.es/services/wsreto/Save_Transac
'cion", RequestNamespace:="http://mapama.es/services/wsreto", ResponseNamespace:="http://mapama.es/services/ws
'reto", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Proto
'cols.SoapParameterStyle.Wrapped)> _
' Public Function Save_Transaccion(ByVal data As String) As String
' Dim results() As Object = Me.Invoke("Save_Transaccion", New Object() {data})
' Return CType(results(0),String)
' End Function

' <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mapama.es/services/wsreto/Update_Trans
'accion", RequestNamespace:="http://mapama.es/services/wsreto", ResponseNamespace:="http://mapama.es/services/
'wsreto", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Pro
'tocols.SoapParameterStyle.Wrapped)> _
' Public Function Update_Transaccion(ByVal identificador As Integer, ByVal data As String) As String
' Dim results() As Object = Me.Invoke("Update_Transaccion", New Object() {identificador, data})
' Return CType(results(0),String)
' End Function

' <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mapama.es/services/wsreto/Delete_Trans
'accion", RequestNamespace:="http://mapama.es/services/wsreto", ResponseNamespace:="http://mapama.es/services/
'wsreto", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Pro
'tocols.SoapParameterStyle.Wrapped)> _
' Public Function Delete_Transaccion(ByVal identificador As Integer) As String
' Dim results() As Object = Me.Invoke("Delete_Transaccion", New Object() {identificador})
' Return CType(results(0),String)
' End Function

' <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mapama.es/services/wsreto/GetItem_Tran
'saccion", RequestNamespace:="http://mapama.es/services/wsreto", ResponseNamespace:="http://mapama.es/services
'/wsreto", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Pr
'otocols.SoapParameterStyle.Wrapped)> _
' Public Function GetItem_Transaccion(ByVal identificador As Integer) As String
' Dim results() As Object = Me.Invoke("GetItem_Transaccion", New Object() {identificador})
' Return CType(results(0),String)
' End Function

' <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mapama.es/services/wsreto/GetItems_Tra
'nsaccion", RequestNamespace:="http://mapama.es/services/wsreto", ResponseNamespace:="http://mapama.es/service
's/wsreto", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.P
'rotocols.SoapParameterStyle.Wrapped)> _
' Public Function GetItems_Transaccion() As String
' Dim results() As Object = Me.Invoke("GetItems_Transaccion", New Object(-1) {})
' Return CType(results(0),String)
' End Function

' End Class
'End Namespace