Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.IO

Class FirmaFectuas
    Dim doc As XmlDocument = New XmlDocument()

    Public Function GetCertificado() As X509Certificate2
        Dim store As X509Store = New X509Store
        store.Open((OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly))
        Dim collection As X509Certificate2Collection = CType(store.Certificates, X509Certificate2Collection)
        Dim fcollection As X509Certificate2Collection = CType(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, False), X509Certificate2Collection)
        Dim scollection As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(fcollection, "Certificados", "Seleccione un certificado", X509SelectionFlag.SingleSelection)
        If (scollection.Count = 0) Then
            Return Nothing
        End If
        Return scollection(0)

    End Function

    Public Function Sign(cert As X509Certificate2, route As String)
        doc.LoadXml(File.ReadAllText(route))

        Dim signedxml As SignedXml = New SignedXml(doc)
        Dim reference As Reference = New Reference()
        reference.Uri = ""
        Dim env As XmlDsigEnvelopedSignatureTransform = New XmlDsigEnvelopedSignatureTransform()
        reference.AddTransform(env)
        signedxml.AddReference(reference)

        Dim KeyInfo As KeyInfo = New KeyInfo()
        KeyInfo.AddClause(New KeyInfoX509Data(cert))
        signedxml.KeyInfo = KeyInfo
        signedxml.SigningKeyName = cert.Subject

        signedxml.SigningKey = cert.PrivateKey '<--- origen de error'
        'signedxml.SigningKey = cert.GetRSAPrivateKey <--- Para firmar con certificados no exportables

        signedxml.ComputeSignature()
        Dim xmlsig As XmlElement = signedxml.GetXml()
        doc.DocumentElement.AppendChild(doc.ImportNode(xmlsig, True))

    End Function

    Public Sub GrabarXMLFirmado(Ruta)
        doc.Save(Ruta)
    End Sub

    Public Shared Function VerifyXmlFile(Name As String) As Boolean

        ' Check the arguments.
        If Equals(Name, Nothing) Then Throw New ArgumentNullException("Name")

        ' Create a new XML document.
        Dim xmlDocument As XmlDocument = New XmlDocument()

        ' Format using white spaces.
        xmlDocument.PreserveWhitespace = True

        ' Load the passed XML file into the document.
        xmlDocument.Load(Name)

        ' Create a new SignedXml object and pass it
        ' the XML document class.
        Dim signedXml As SignedXml = New SignedXml(xmlDocument)

        ' Find the "Signature" node and create a new
        ' XmlNodeList object.
        Dim nodeList As XmlNodeList = xmlDocument.GetElementsByTagName("Signature")

        ' Load the signature node.
        signedXml.LoadXml(CType(nodeList(0), XmlElement))

        ' Check the signature and return the result.
        Return signedXml.CheckSignature()
    End Function


End Class