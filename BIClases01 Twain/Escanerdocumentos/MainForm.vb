Imports System.Windows.Forms

Imports TwainDotNet
Imports TwainDotNet.TwainNative
Imports TwainDotNet.WinFroms
Imports System.IO
Imports System.Xml



Public Class MainForm

    Dim areaSettings As New AreaSettings(Units.Centimeters, 0.1F, 5.7F, 0.1F + 2.6F, 5.7F + 2.6F)

    ''' <summary>
    ''' Twain scanning library
    ''' </summary>
    Dim twain As Twain

    ''' <summary>
    ''' The current scan settings.
    ''' </summary>
    Dim settings As ScanSettings

    ''' <summary>
    ''' The current list of images (only the latest displayed in the Form).
    ''' </summary>
    Dim images As List(Of System.Drawing.Bitmap)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Twain needs a hook into this Form's message loop to work:
        twain = New Twain(New WinFormsWindowMessageHook(Me))
        LeerConfig()

    End Sub

    Private Sub selectSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectSource.Click
        ' Show the "select scanning source" dialog
        Try
            twain.SelectSource()
        Catch ex As Exception
            MsgBox("Se ha producido un er ror, compruebe la disponibilidad del escáner")
            Close()
        End Try

    End Sub

    Private Sub saveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveButton.Click
        Grabarconfig()
        Me.Dispose()
    End Sub

    Private Sub diagnosticsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles diagnosticsButton.Click
        ' Dump out diagnostics from the current source
        Dim diagnostics As New Diagnostics(New WinFormsWindowMessageHook(Me))
    End Sub

    Private Sub LeerConfig()
        Dim linea As Integer
        Dim EnLectura As Boolean
        Dim nombrevar As String

        settings = New ScanSettings()
        settings.Rotation = New RotationSettings

        EnLectura = False
        Using reader As XmlReader = XmlReader.Create(My.Computer.FileSystem.CurrentDirectory & "\config.xml")
            linea = 0
            nombrevar = ""

            While reader.Read()
                If reader.IsStartElement() Then
                    If EnLectura Then
                        If Trim(UCase(reader.Name)) = "VALOR" Then
                            If reader.Read() Then
                                Select Case nombrevar

                                    Case "DefaultSourceName"
                                        twain.SelectSource(reader.Value.Trim())

                                    Case "UseDocumentFeeder"
                                        settings.UseDocumentFeeder = IIf(reader.Value.Trim() = "Sí", True, False)
                                        useAdfCheckBox.Checked = IIf(reader.Value.Trim() = "Sí", True, False)
                                    Case "ShowTwainUI"
                                        settings.ShowTwainUI = IIf(reader.Value.Trim() = "Sí", True, False)
                                        useUICheckBox.Checked = IIf(reader.Value.Trim() = "Sí", True, False)
                                    Case "ShowProgressIndicatorUI"
                                        settings.ShowProgressIndicatorUI = IIf(reader.Value.Trim() = "Sí", True, False)
                                        showProgressIndicatorUICheckBox.Checked = IIf(reader.Value.Trim() = "Sí", True, False)
                                    Case "UseDuplex"
                                        settings.UseDuplex = IIf(reader.Value.Trim() = "Sí", True, False)
                                        useDuplexCheckBox.Checked = IIf(reader.Value.Trim() = "Sí", True, False)
                                    Case "Resolution"
                                        settings.Resolution = IIf(reader.Value.Trim() = "B/N", ResolutionSettings.Fax, ResolutionSettings.ColourPhotocopier)
                                        blackAndWhiteCheckBox.Checked = IIf(reader.Value.Trim() = "B/N", True, False)
                                    Case "Area"
                                        settings.Area = IIf(reader.Value.Trim() = "No", Nothing, areaSettings)
                                        checkBoxArea.Checked = IIf(reader.Value.Trim() = "No", False, True)
                                    Case "ShouldTransferAllPages"
                                        settings.ShouldTransferAllPages = IIf(reader.Value.Trim() = "Sí", True, False)
                                    Case "AutomaticRotate"
                                        settings.Rotation.AutomaticRotate = IIf(reader.Value.Trim() = "Sí", True, False)
                                        autoRotateCheckBox.Checked = IIf(reader.Value.Trim() = "Sí", True, False)
                                    Case "AutomaticBorderDetection"
                                        settings.Rotation.AutomaticBorderDetection = IIf(reader.Value.Trim() = "Sí", True, False)
                                        autoDetectBorderCheckBox.Checked = IIf(reader.Value.Trim() = "Sí", True, False)
                                End Select
                                EnLectura = False
                            End If
                        End If
                    End If
                    If Trim(UCase(reader.Name)) = "VARIABLE" Then
                        If EnLectura Then
                            Exit While
                        End If
                        If Trim(UCase(reader("NOMBRE"))) <> "" Then
                            nombrevar = reader("NOMBRE")
                            EnLectura = True
                        End If
                    End If
                End If
            End While

        End Using
    End Sub
    Private Sub Grabarconfig()
        Dim writer As XmlWriter
        Dim settings As XmlWriterSettings = New XmlWriterSettings()

        settings.Indent = True
        settings.OmitXmlDeclaration = True
        settings.NewLineOnAttributes = True

        writer = XmlWriter.Create(My.Computer.FileSystem.CurrentDirectory & "\config.xml", settings)

        writer.WriteStartDocument()
        writer.WriteStartElement("config") ' Root.

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "DefaultSourceName")
        writer.WriteElementString("valor", twain.DefaultSourceName)
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "UseDocumentFeeder")
        writer.WriteElementString("valor", IIf(useAdfCheckBox.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "ShowTwainUI")
        writer.WriteElementString("valor", IIf(useUICheckBox.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "ShowProgressIndicatorUI")
        writer.WriteElementString("valor", IIf(showProgressIndicatorUICheckBox.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "UseDuplex")
        writer.WriteElementString("valor", IIf(useDuplexCheckBox.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "Resolution")
        writer.WriteElementString("valor", IIf(blackAndWhiteCheckBox.Checked, "B/N", "Color"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "Area")
        writer.WriteElementString("valor", IIf(checkBoxArea.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "ShouldTransferAllPages")
        writer.WriteElementString("valor", "Sí")
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "AutomaticRotate")
        writer.WriteElementString("valor", IIf(autoRotateCheckBox.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteStartElement("variable")
        writer.WriteAttributeString("NOMBRE", "AutomaticBorderDetection")
        writer.WriteElementString("valor", IIf(autoDetectBorderCheckBox.Checked, "Sí", "No"))
        writer.WriteEndElement()

        writer.WriteEndElement()
        writer.Flush()
        writer.Close()

    End Sub

End Class
