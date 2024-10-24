'Imports System
'Imports System.Runtime.Versioning
'    Imports System.Security.Cryptography
'    Imports System.Security.Cryptography.Xml
'    Imports System.Xml


'Public Class SignXML
'        Public Shared Sub Main(args As String())
'            Try
'            ' Create a new CspParameters object to specify
'            ' a key container.
'            Dim cspParams As New CspParameters

'            With cspParams
'                .KeyContainerName = "XML_DSIG_RSA_KEY"
'            End With

'            ' Create a new RSA signing key and save it in the container.
'            Dim rsaKey As New RSACryptoServiceProvider(cspParams)

'            ' Create a new XML document.
'            Dim xmlDoc As New XmlDocument
'            With xmlDoc
'                ' Load an XML file into the XmlDocument object.
'                .PreserveWhitespace = True
'            End With

'            xmlDoc.Load("test.xml")

'            ' Sign the XML document.
'            SignXml(xmlDoc, rsaKey)

'                Console.WriteLine("XML file signed.")

'                ' Save the document.
'                xmlDoc.Save("test.xml")
'            Catch e As Exception
'                Console.WriteLine(e.Message)
'            End Try
'        End Sub

'        ' Sign an XML file.
'        ' This document cannot be verified unless the verifying
'        ' code has the key with which it was signed.
'        Public Shared Sub SignXml(ByVal xmlDoc As XmlDocument, ByVal rsaKey As RSA)
'            ' Check arguments.
'            If xmlDoc Is Nothing Then
'                Throw New ArgumentException(Nothing, NameOf(xmlDoc))
'            End If
'            If rsaKey Is Nothing Then
'                Throw New ArgumentException(Nothing, NameOf(rsaKey))
'            End If

'        ' Create a SignedXml object.
'        Dim signedXml As New SignedXml(xmlDoc)
'        With signedXml
'            ' Add the key to the SignedXml document.
'            .SigningKey = rsaKey
'        End With

'        ' Create a reference to be signed.
'        Dim reference As New Reference

'        With reference
'            .Uri = ""
'        End With

'        ' Add an enveloped transformation to the reference.
'        Dim env As New XmlDsigEnvelopedSignatureTransform()
'            reference.AddTransform(env)

'            ' Add the reference to the SignedXml object.
'            signedXml.AddReference(reference)

'            ' Compute the signature.
'            signedXml.ComputeSignature()

'            ' Get the XML representation of the signature and save
'            ' it to an XmlElement object.
'            Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()

'            ' Append the element to the XML document.
'            xmlDoc.DocumentElement?.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
'        End Sub
'    End Class




