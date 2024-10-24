'Module Module1
'<?xml version="1.0" encoding="UTF-8"?>
'<family>
'    <name gender = "Male" >
'    <firstname>Tom</firstname>
'    <lastname> Smith</lastname>
'  </name>
'  <name gender = "Female" >
'    <firstname>Dale</firstname>
'    <lastname> Smith</lastname>
'  </name>
'</family>

'    ' Dos metodos para extraer XML

'    Imports System.IO
'    Imports System.Xml
'    Module ParsingUsingXmlTextReader
'        Sub Main()
'            Dim m_xmlr As XmlTextReader
'            'Create the XML Reader
'            m_xmlr = New XmlTextReader("C:\Personal\family.xml")
'            'Disable whitespace so that you don't have to read over whitespaces
'            m_xmlr.WhiteSpaceHandling = WhiteSpaceHandling.NONE
'            'read the xml declaration and advance to family tag
'            m_xmlr.Read()
'            'read the family tag
'            m_xmlr.Read()
'            'Load the Loop
'            While Not m_xmlr.EOF
'                'Go to the name tag
'                m_xmlr.Read()
'                'if not start element exit while loop
'                If Not m_xmlr.IsStartElement() Then
'                    Exit While
'                End If
'                'Get the Gender Attribute Value
'                Dim genderAttribute = m_xmlr.GetAttribute("gender")
'                'Read elements firstname and lastname
'                m_xmlr.Read()
'                'Get the firstName Element Value
'                Dim firstNameValue = m_xmlr.ReadElementString("firstname")
'                'Get the lastName Element Value
'                Dim lastNameValue = m_xmlr.ReadElementString("lastname")
'                'Write Result to the Console
'                Console.WriteLine("Gender: " & genderAttribute _
'                  & " FirstName: " & firstNameValue & " LastName: " _
'                  & lastNameValue)
'                Console.Write(vbCrLf)
'            End While
'            'close the reader
'            m_xmlr.Close()
'        End Sub

'        ' Otro método
'        Imports System.IO
'        Imports System.Xml
'        Module ParsingUsingXmlDocument
'            Sub Main()
'                Try
'                    Dim m_xmld As XmlDocument
'                    Dim m_nodelist As XmlNodeList
'                    Dim m_node As XmlNode
'                    'Create the XML Document
'                    m_xmld = New XmlDocument()
'                    'Load the Xml file
'                    m_xmld.Load("C:\CMS\Personal\family.xml")
'                    'Get the list of name nodes 
'                    m_nodelist = m_xmld.SelectNodes("/family/name")
'                    'Loop through the nodes
'                    For Each m_node In m_nodelist
'                        'Get the Gender Attribute Value
'                        Dim genderAttribute = m_node.Attributes.GetNamedItem("gender").Value
'                        'Get the firstName Element Value
'                        Dim firstNameValue = m_node.ChildNodes.Item(0).InnerText
'                        'Get the lastName Element Value
'                        Dim lastNameValue = m_node.ChildNodes.Item(1).InnerText
'                        'Write Result to the Console
'                        Console.Write("Gender: " & genderAttribute _
'                          & " FirstName: " & firstNameValue & " LastName: " _
'                          & lastNameValue)
'                        Console.Write(vbCrLf)
'                    Next
'                Catch errorVariable As Exception
'                    'Error trapping
'                    Console.Write(errorVariable.ToString())
'                End Try
'            End Sub
'        End Module

'    End Module

'Imports System
'Imports System.Web
'Imports System.IO
'Imports System.Net

'Partial Public Class _Default
'    Inherits System.Web.UI.Page

'    Private Function HttpGet(ByVal uri As String, ByVal username As String, ByVal password As String) As String
'        Dim stream As Stream
'        Dim reader As StreamReader
'        Dim response As String = Nothing
'        Dim webClient As WebClient = New WebClient()

'        Using webClient

'            If Not String.IsNullOrEmpty(username) AndAlso Not String.IsNullOrEmpty(password) Then
'                webClient.Credentials = New NetworkCredential(username, password)
'            End If

'            Try
'                stream = webClient.OpenRead(uri)
'                reader = New StreamReader(stream)
'                response = reader.ReadToEnd()
'            Catch ex As WebException

'                If TypeOf ex.Response Is HttpWebResponse Then

'                    Select Case (CType(ex.Response, HttpWebResponse)).StatusCode
'                        Case HttpStatusCode.NotFound, HttpStatusCode.Unauthorized
'                            response = Nothing
'                        Case Else
'                            Throw ex
'                    End Select
'                End If
'            End Try
'        End Using

'        Return response
'    End Function

'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
'        Dim uri As String = "http://twitter.com/statuses/user_timeline.xml"
'        Dim username As String = "YOUR TWITTER USERNAME"
'        Dim password As String = "YOUR TWITTER PASSWORD"
'        litResponse.Text = HttpGet(uri, username, password)
'    End Sub
'End Class
