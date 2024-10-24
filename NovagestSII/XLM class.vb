'Imports interop_msxml
'Imports Microsoft.WindowsMediaServices.Interop

Imports System
Imports System.Xml
Imports Windows.Data.Xml.Dom


Public Class Clsxml
    Dim xDoc As XmlDocument
    Dim m_nodelist As XmlNodeList
    Dim m_node As XmlNode
    Dim xmlConfig As XmlDocument
    Public HayError As Boolean = False

    Public Function root() As XmlElement
        Return xmlConfig.DocumentElement
    End Function

    Public Sub New()
        xmlConfig = New XmlDocument()
    End Sub

    Public Sub New(ByVal oSubXML As XmlNode)
        xmlConfig = New XmlDocument()
        xmlConfig = oSubXML
    End Sub

    Public Sub New(ByVal cXml As String)
        xmlConfig = New XmlDocument()
        If IO.File.Exists(cXml) Then
            MsgBox("No encuentro el archivo solicitado")
            HayError = True
            Return
        End If
        xmlConfig.Load(cXml)

    End Sub

    Public Sub OpenXML(ByVal cXml As String)
        If IO.File.Exists(cXml) Then
            MsgBox("No encuentro el archivo solicitado")
            HayError = True
            Return
        End If

        xmlConfig.Load(cXml)
    End Sub

    Public Function ReadSingleNode(ByVal TxtNodo As String) As XmlNode
        Dim child As XmlNode
        child = xmlConfig.SelectSingleNode(TxtNodo)
        Return child
    End Function

    Public Function TxtReadSingleNode(ByVal TxtNodo As String) As String
        Dim child As XmlNode
        child = xmlConfig.SelectSingleNode(TxtNodo)
        Return child.InnerText
    End Function


    Public Function ReadNodes(ByVal TxtNodo As String) As XmlNodeList
        m_nodelist = xmlConfig.SelectNodes(TxtNodo)
        Return m_nodelist
    End Function

    Public Function NodeCount() As Integer
        Return m_nodelist.Count
    End Function

    Public Function NodeCount(xml As String) As Integer
        m_nodelist = xmlConfig.SelectNodes(xml)
        Return m_nodelist.Count
    End Function

    Public Function NodeList(cNombre) As XmlNodeList
        Return xmlConfig.GetElementsByTagName(cNombre)
    End Function

    Private Function NodeReader(child) As XmlNodeReader
        Return New XmlNodeReader(child)
    End Function

    Private Function NodeRead(ByVal cXML As XmlNodeReader) As Object ' 
        Return cXML.Read()
        'Select Case reader.NodeType
        '    Case XmlNodeType.Element
        '        'Console.Write("<{0}>", reader.Name)
        '    Case XmlNodeType.Text
        '        Console.Write(reader.Value)
        '    Case XmlNodeType.CDATA
        '        Console.Write(reader.Value)
        '    Case XmlNodeType.ProcessingInstruction
        '        Console.Write("<?{0} {1}?>", reader.Name, reader.Value)
        '    Case XmlNodeType.Comment
        '        Console.Write("<!--{0}-->", reader.Value)

        '    Case XmlNodeType.XmlDeclaration
        '        Console.Write("<?xml version='1.0'?>")
        '    Case XmlNodeType.Document
        '    Case XmlNodeType.EndElement
        '        Console.Write("</{0}>", reader.Name);
        'End Select
    End Function


    Public Function ListaValores(child) As List(Of Object)
        'Dim NameList As List(Of Object)
        'Dim nr As New XmlNodeReader(child)
        'While nr.Read()
        '    NameList.Items.Add(nr.Value)
        'End While
    End Function

    Public Function GetChildName(cXML, nnOrd) As String

    End Function

    Public Function GetChilValue(cNodoXML, nOrd) As String
        ' m_node.Attributes.GetNamedItem("id").Value
    End Function

End Class

'    oXML.OpenXML (cXML)

'Set RsEnviadas = New Recordset
'RsEnviadas.CursorLocation = adUseClient

'cCSV = oXML.ReadNode("/env:Envelope/env:Body/siiLR:RespuestaLRBajaBienesInversion/siiR:CSV")
'EstadoEnvio = oXML.ReadNode("/env:Envelope/env:Body/siiLR:RespuestaLRBajaBienesInversion/siiR:EstadoEnvio")

'nodos = oXML.NodeCount("/env:Envelope/env:Body/siiLR:RespuestaLRBajaBienesInversion")

'pbR.Min = 0
'pbR.max = nodos
'pbR.Visible = True
'pbR.value = 0

'For i = 0 To nodos - 1
'    pbR.value = i
'    DoEvents
'    If oXML.GetChildName("/env:Envelope/env:Body/siiLR:RespuestaLRBajaBienesInversion", i) = "siiR:RespuestaLinea" Then
'        SubXML = oXML.GetChildValue("/env:Envelope/env:Body/siiLR:RespuestaLRBajaBienesInversion", i)
'        oSubXML.OpenXML (SubXML)

'        Correcto_sn = oSubXML.ReadNode("/siiR:RespuestaLinea/siiR:EstadoRegistro")




'    Public Enum OpenXMLDoc
'        oxFile = 0
'        oxString = 1
'    End Enum

'    Public Enum DocInfoConst
'        diVERSION = 0
'        diENCODING = 1
'        diSTANDALONE = 2
'    End Enum

'    Const Invalid = "Invalid"
'    Const Element = "Element"
'    Const Attr = "Attribute"
'    Const Text = "Text"
'    Const CData = "CData Section"
'    Const Entity_Ref = "Entity Reference"
'    Const Entity = "Entity"
'    Const Processing = "Processing Instruction"
'    Const Comment = "Comment"
'    Const Document = "Document"
'    Const Doc_Type = "Document Type"
'    Const Doc_Fragment = "Document Fragment"
'    Const Notation = "Notation"

'    '---------------------------------------------------
'    ' Public Property Get
'    '   XMLDocumentInfo
'    ' Parameters
'    '   diDesiredInfo As DocInfoConst : Tells the property
'    '   what to look for
'    ' What it does
'    '   Gets the document info from the <?xml?> line
'    '   Not entirely sure of funcyionality here
'    '   No errors, just may change XPath a bit
'    '   (By the way you reference a node)
'    ' Author
'    '   Anthony (11.19.02)
'    ' Modified
'    '   (11.19.02)
'    '---------------------------------------------------
'    Public Function XMLDocumentInfo(diDesiredInfo As DocInfoConst) As Object
'        Dim strDesiredInfo As String

'        Select Case diDesiredInfo
'            Case diVERSION
'                strDesiredInfo = "version"
'            Case diENCODING
'                strDesiredInfo = "encoding"
'            Case diSTANDALONE
'                strDesiredInfo = "standalone"
'        End Select


'        Dim xNode As XmlNode
'        xNode = xDoc.ChildNodes(0).Attributes.GetNamedItem(strDesiredInfo)

'        XMLDocumentInfo = xNode.Text

'    End Function


'    Public Property Number() As Integer
'        Get
'            Return _count
'        End Get
'        Set(ByVal value As Integer)
'            _count = value
'        End Set
'    End Property


'    '---------------------------------------------------
'    ' Function
'    '   OpenXML
'    ' Parameters
'    '   strSource As String : The complete path and
'    '       file name of the xml file to open
'    '   Type As tOpenXML : Supplies to the function
'    '       wether it should load from a file or a string
'    ' What it does
'    '   Opens strSource as an xml document ready for
'    '   input (if oType is oxString then it loads from
'    '   a string specified by strSource)
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function OpenXML(strSource As String, Optional oType As OpenXMLDoc = OpenXMLDoc.oxString) As Boolean
'        Dim xDoc As XmlDocument = New XmlDocument()

'        Select Case oType
'            Case OpenXMLDoc.oxFile
'                Return xDoc.Load(strSource)
'            Case OpenXMLDoc.oxString
'                OpenXML = xDoc.LoadXml(strSource)
'        End Select
'    End Function
'    '---------------------------------------------------
'    ' Function
'    '   ReadNode
'    ' Parameters
'    '   strQuery As String : The query string to be
'    '       executed on the xml file
'    ' What it does
'    '   Executes the query strQuery on the xml file
'    '   that has been opened and returns the value found
'    '   in the node
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function ReadNode(strQuery As String) As String
'        On Error GoTo ErrHandle

'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'ReadNode = "" & xNode.Text

'Set xNode = Nothing

'Exit Function
'ErrHandle:
''Resume
''ReadNode = Nothing
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   ReadNodeXML
'    ' Parameters
'    '   strQuery As String : The query string to be
'    '       executed on the xml file
'    ' What it does
'    '   Executes the query strQuery on the xml file
'    '   that has been opened and returns the full xml
'    '   code for the node
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function ReadNodeXML(strQuery As String) As String
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'ReadNodeXML = xNode.XML

'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Public Property Get
'    '   XML
'    ' Parameters
'    '   (None)
'    ' What it does
'    '   Reads all of the XML data from the xml file and
'    '   outputs as a string
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Property Get XML() As String
'XML = xDoc.XML
'End Property
'    '---------------------------------------------------
'    ' Function
'    '   WriteNode
'    ' Parameters
'    '   strQuery As String : The query used to access the
'    '       node
'    '   Value As Variant : The value to be written to
'    '       the node
'    ' What it does
'    '   Writes Value to the node specified by strQuery
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function WriteNode(strQuery As String, value As Variant)
'        On Error GoTo ErrHandle

'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'xNode.Text = value

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   ReadAttribute
'    ' Parameters
'    '   strQuery As String : The query used to access the
'    '       node
'    '   strName As String : The name of the attribute to
'    '       read
'    ' What it does
'    '   Returns the value of the attribute specified by
'    '   strName
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function ReadAttribute(strQuery As String, strName As String) As String
'        On Error GoTo ErrHandle

'        Dim xNode As MSXML2.IXMLDOMElement
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'ReadAttribute = xNode.getAttribute(strName)

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'        ReadAttribute = Null
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   WriteAttribute
'    ' Parameters
'    '   strQuery As String : The query used to access the
'    '       node
'    '   strName As String : The name of the attribute
'    '       to be written
'    '   Value As Variant : The value to be written to
'    '       the node attribute
'    ' What it does
'    '   Writes Value to the node attribute specified by
'    '   strName in a node specified by strQuery
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.25.02)
'    ' Fixed (0.03)
'    '   Added array support here also, so that more than
'    '   one attribute can be added at once
'    ' Fixed (0.04)
'    '   Array can start at any number at all :)
'    '---------------------------------------------------
'    Public Function WriteAttribute(strQuery As String, strNames() As String, strValues() As String)
'        On Error GoTo ErrHandle

'        Dim xNode As MSXML2.IXMLDOMElement, i As Integer
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'For i = LBound(strNames) To UBound(strNames)
'            If strNames(0) = "" Then GoTo ErrHandle : 
'            xNode.setAttribute strNames(i), strValues(i)
'Next i

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   NodeCount
'    ' Parameters
'    '   strQuery As String : The query used to access the
'    '       nodes
'    ' What it does
'    '   Returns the count of the nodes
'    '   Always use XML.NodeCount - 1 (in a for statement)
'    '   because it includes the closing node in the count
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.22.02)
'    ' Fixed
'    '   - Returns the correct node count now
'    '     instead of just 1
'    '---------------------------------------------------
'    Public Function NodeCount(strQuery As String) As Long
'        On Error GoTo ErrHandle

'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'NodeCount = xNode.childNodes.length

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'        NodeCount = -1
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   Save
'    ' Parameters
'    '   strFileName As String : Full path to the save
'    '       destination
'    ' What it does
'    '   Saves the XML file to strFileName
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function Save(strFileName As String)
'        xDoc.Save strFileName
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   DeleteNode
'    ' Parameters
'    '   strQuery As String : Query to find the node
'    ' What it does
'    '   Deletes a child node specified by strQuery
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (11.18.02)
'    '---------------------------------------------------
'    Public Function DeleteNode(strQuery As String)
'        On Error GoTo ErrHandle

'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'xNode.parentNode.removeChild xNode

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   MakeNode
'    ' Parameters
'    '   strQuery As String : Query to find the parent
'    '       node
'    '   strName As String : Name of the new node
'    '   Optional Value As Variant : Value of the node
'    ' What it does
'    '   Creates a new node in the XML file
'    ' Author
'    '   Anthony (11.18.02)
'    ' Modified
'    '   (12.03.02)
'    ' Fixed (0.03)
'    '   Accepts an array for the attribute list, allowing
'    '   for multiple attributes, as opposed to the method
'    '   used in 0.01 and 0.02, which only accepted two
'    '   attributes at once.  Attribute arrays are
'    '   required, but just enter an empty string if you
'    '   dont want any attributes
'    ' Fixed (0.04)
'    '   Accepts an array starting at any number at all :)
'    '---------------------------------------------------
'    Public Function MakeNode(strQuery As String, strName As String, strAttrNames() As String, strAttrValues() As String, Optional NodeValue As Variant)
'        Dim xPNode As MSXML2.IXMLDOMNode
'        Dim xCNode As MSXML2.IXMLDOMElement
'        Dim i As Integer
'        Dim cname As String
'        Dim cvalue As String
'Set xPNode = xDoc.documentElement.selectSingleNode(strQuery)
'Set xCNode = xDoc.createElement(strName)

'If Not IsMissing(NodeValue) Then
'            xCNode.Text = NodeValue
'        End If

'        xPNode.appendChild xCNode

'For i = LBound(strAttrNames) To UBound(strAttrNames)
'            If strAttrNames(0) = "" Then GoTo EndFunc : 
'            cname = strAttrNames(i)
'            cvalue = strAttrValues(i)
'            'WriteAttribute strQuery & "/" & strName, cname, cvalue
'        Next i

'EndFunc:
'    End Function
'    '---------------------------------------------------
'    ' Function
'    '   DeleteAttribute
'    ' Parameters
'    '   strQuery As String : Query to find the parent
'    '       node
'    '   strName As String : Name of the attribute to be
'    '       deleted
'    ' What it does
'    '   Deletes strAttributeName from the node specified
'    '   by strQuery
'    ' Author
'    '   Anthony (11.19.02)
'    ' Modified
'    '   (11.19.02)
'    '---------------------------------------------------
'    Public Function DeleteAttribute(strQuery As String, strAttributeName As String)
'        Dim xNode As MSXML2.IXMLDOMElement
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'xNode.Attributes.removeNamedItem strAttributeName

'Set xNode = Nothing
'End Function

'    '---------------------------------------------------
'    ' Function
'    '   GetColorValue
'    ' Parameters
'    '   strQuery : Query used to locate the (x, x, x) node
'    ' What it does
'    '   Parses a node found in the format (x, x, x) to be
'    '   used as an rgb value
'    '   Set the object to be colored equal to this
'    '   EX: frmMain.BackColor = XML.GetColorValue("query")
'    ' Author
'    '   Anthony (11.20.02)
'    ' Modified
'    '   (11.20.02)
'    '---------------------------------------------------
'    Public Function GetColorValue(strQuery As String) As Long
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'Dim Red As Integer, Green As Integer, Blue As Integer
'        Dim Data() As String
'        Exit Function
'        Data = Split(xNode.Text, ", ")

'        Red = Val(Right(Data(0), Len(Data(0)) - 1))
'        Green = Val(Data(1))
'        Blue = Val(Left(Data(2), Len(Data(2)) - 1))

'        GetColorValue = RGB(Red, Green, Blue)
'    End Function
'    '---------------------------------------------------
'    ' Function
'    '   SetColorValue
'    ' Parameters
'    '   strQuery : Query used to locate the (x, x, x) node
'    '   Red : Red color value
'    '   Green : Green color value
'    '   Blue : Blue color value
'    ' What it does
'    '   Writes the color data with format (Red, Green, Blue)
'    '   To a specified node, allowing for easy skinning
'    ' Author
'    '   Anthony (11.20.02)
'    ' Modified
'    '   (11.20.02)
'    '---------------------------------------------------
'    Public Function SetColorValue(strQuery As String, Red As Integer, Green As Integer, Blue As Integer)
'        Dim strData As String
'        strData = "(" & Red & ", " & Green & ", " & Blue & ")"

'        WriteNode strQuery, strData
'End Function

'    '---------------------------------------------------
'    ' Function
'    '   GetChildValue
'    ' Parameters
'    '   strQuery : Query used to locate the parent node
'    '   Index : Count of the node (first = 1, etc...)
'    ' What it does
'    '   Allows easy child access for parent nodes
'    '   (If multiple children exist under a parent)
'    '   Only returns the value of the node
'    '   Other functions are used to get to
'    '   Attributes of Child Nodes
'    '
'    '   Also allows for loading of XML files where the
'    '   tags or tag count is not known
'    ' Author
'    '   Anthony (11.22.02)
'    ' Modified
'    '   (11.22.02)
'    '---------------------------------------------------
'    Public Function GetChildValue(strQuery As String, Index As Long) As String
'        On Error GoTo ErrHandle
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'GetChildValue = xNode.childNodes(Index).XML
''.Text

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'        GetChildValue = ""
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   GetChildAttribute
'    ' Parameters
'    '   strQuery : Query used to locate the parent node
'    '   strName : Name of the attribute to read
'    '   Index : Count of the node (first = 1, etc...)
'    ' What it does
'    '   Allows you to access the attribute of a child
'    '   node easily
'    ' Author
'    '   Anthony (11.22.02)
'    ' Modified
'    '   (11.22.02)
'    '---------------------------------------------------
'    Public Function GetChildAttribute(strQuery As String, strName As String, Index As Long) As String
'        On Error GoTo ErrHandle
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'GetChildAttribute = xNode.childNodes(Index).Attributes.getNamedItem(strName)

'Set xNode = Nothing

'Exit Function
'ErrHandle:
'        GetChildAttribute = ""
'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   GetChildName
'    ' Parameters
'    '   strQuery : Query used to locate the parent node
'    '   Index : Count of the node (first = 1, etc...)
'    ' What it does
'    '   Returns the name of the node
'    '   EX: <hello> (name: hello)
'    ' Author
'    '   Anthony (11.23.02)
'    ' Modified
'    '   (11.23.02)
'    '---------------------------------------------------
'    Public Function GetChildName(strQuery As String, Index As Long) As String
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'GetChildName = xNode.childNodes(Index).nodeName

'Set xNode = Nothing
'End Function
'    '---------------------------------------------------
'    ' Function
'    '   WriteChildAttr
'    ' Parameters
'    '   strQuery : Query used to locate the parent node
'    '   Index : Count of the node (first = 1, etc...)
'    '   strNames() : Array of attribute names
'    '   strValues() : Array of attribute values
'    ' What it does
'    '   Writes an attribute (or multiple attributes) to
'    '   a child node specified by Index.
'    '   Make sure the values of strNames and strValues
'    '   match up correctly (and start at zero)
'    ' Author
'    '   Anthony (11.25.02)
'    ' Modified
'    '   (12.03.02)
'    '---------------------------------------------------
'    Public Function WriteChildAttr(strQuery As String, Index As Long, strAttrNames() As String, strAttrValues As String)
'        Dim xNode As MSXML2.IXMLDOMNode, i As Integer
'Set xNode = xDoc.documentElement.selectSingleNode(strQuery)

'For i = LBound(strAttrNames) To UBound(strAttrNames)
'            If strAttrNames(0) = "" Then GoTo EndFunc : 
'            'WriteAttribute strQuery & "/" & GetChildName(strQuery, Index), strAttrNames(i), strAttrValues(i)
'        Next i

'EndFunc:
'Set xNode = Nothing
'End Function

'    Public Function EncryptNode(strQuery As String)
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.selectSingleNode(strQuery)

'xNode.Text = ENCRYPT(xNode.Text, Len(xNode.Text))
'    End Function

'    Public Function DecryptNode(strQuery As String)
'        Dim xNode As MSXML2.IXMLDOMNode
'Set xNode = xDoc.selectSingleNode(strQuery)

'xNode.Text = DECRYPT(xNode.Text, Len(xNode.Text))
'    End Function

'    ' The encryption sub
'    ' USAGE:sVariable = ENCRYPT(sYourString,
'    '     lLengthOfString)

'    Private Function ENCRYPT(sString As String, lLen As Long) As String
'        'Just declaring variables
'        Dim i As Long
'        Dim NewChar As Long
'        i = 1 'can't start a String at 0 :-)
'        ' Go through each character in the strin
'        '     g and convert
'        ' it to an ASCII value, add the number d
'        '     esired (here, 13), and
'        ' then place it into a new string


'        Do Until i = lLen + 1
'            NewChar = Asc(Mid(sString, i, 1)) + 13
'            ENCRYPT = ENCRYPT + Chr(NewChar)
'            i = i + 1
'        Loop
'    End Function

'    'Decryption sub
'    'USAGE: sVariable = DECRYPT(sYourstring,
'    '     lLengthOfString)

'    Private Function DECRYPT(sString As String, lLen As Long) As String
'        Dim i As Long
'        Dim NewChar As Long
'        i = 1
'        'Doing the reverse of the encryption sub
'        '     !


'        Do Until i = lLen + 1
'            NewChar = Asc(Mid(sString, i, 1)) - 13
'            DECRYPT = DECRYPT + Chr(NewChar)
'            i = i + 1
'        Loop
'    End Function


'End Class

'Private Sub LeerArchivoXML(ByVal ruta As String)

'    Try

'        Dim xmlConfig As XmlDocument

'        Dim m_nodelist As XmlNodeList

'        Dim m_node As XmlNode

'        'Creamos el "XML Document"

'        xmlConfig = New XmlDocument()

'        'Cargamos el archivo

'        xmlConfig.Load(ruta)

'        'Obtenemos la lista de los nodos "id"

'        m_nodelist = xmlConfig.SelectNodes("/Configuracion/name")

'        Dim indice As Integer = 0

'        'Iniciamos el ciclo de lectura

'        For Each m_node In m_nodelist

'            'Obtenemos el atributo del codigo

'            Dim mCodigo As String = m_node.Attributes.GetNamedItem("id").Value

'            'Obtenemos el Elemento Hora

'            Dim mHora As Integer = m_node.ChildNodes.Item(0).InnerText

'            'Obtenemos el Elemento Minuto

'            Dim mMinuto As Integer = m_node.ChildNodes.Item(1).InnerText

'            If indice = 0 Then

'                MessageBox.Show("La primera hora es: " & mHora.ToString("00") & ":" & mMinuto.ToString("00"))

'                indice += 1

'            Else

'                MessageBox.Show("La segunda hora es: " & mHora.ToString("00") & ":" & mMinuto.ToString("00"))

'            End If

'        Next

'    Catch ex As Exception


'    End Try

'  End Sub