Imports System.Windows.Forms
Imports TwainDotNet
Imports TwainDotNet.TwainNative
Imports TwainDotNet.WinFroms

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Security.Permissions
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Environment
Imports System.Diagnostics
Imports System.Threading
Imports System.Text
Imports System.Security
Imports System.Xml
Imports PDFjet.NET



Public Class Escaneadocumento
    Public msgfilter As Boolean
    Private picnumber As Integer = 0
    Public carpeta_gd As String
    Public numero_entrada As Long
    Public PathArchivoPdf As String
    Public   ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim bmi As BitmapInfoHeader
    Dim bmprect As Rectangle
    Dim Tipodoc As String = "A"
    Private aTitulos() As String = {"NUMERO_ENTRADA", "SERIE_FACTURA", "NUMERO_FACTURA", "FECHA_FACTURA", "CODIGO_PROVEEDOR", "NOMBRE_PROVEEDOR", "CIF_PROVEEDOR", "TOTAL_FACTURA", "BASE IMPONIBLE1", "TIPO1", "CUOTA1", "BASE IMPONIBLE2", "TIPO2", "CUOTA2", "BASE IMPONIBLE3", "TIPO3", "CUOTA3"}
    Private aValores(16) As String
    Private aFields() As String = {"numero_entrada_fra", "serie_factura_com", "numero_fra_com", "fecha_factura", "codigo_proveedor", "razon_social", "cif_prov", "importe_cargo-importe_abono", "Base", "Porcentaje", "importe_cargo-importe_abono", "Base", "Porcentaje", "importe_cargo-importe_abono", "Base", "Porcentaje", "importe_cargo-importe_abono"}
    Private aTipo() As String = {"L", "S", "S", "D", "L", "S", "S", "D", "D", "D", "D", "D", "D", "D", "D", "D", "D"}
    Public Const WM_COPYDATA As Integer = &H4A
    'Public logger As StreamWriter = New StreamWriter("logger.txt")
    Public Trabajando As Boolean = False

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
        'ObtenerInformacion()

        'Timer1.Interval = 2000
        'Timer1.Enabled = True

        ' Twain needs a hook into this Form's message loop to work:
        twain = New Twain(New WinFormsWindowMessageHook(Me))

        'LeerConfiguracion()

        ' Add a handler to grab each image as it comes off the scanner
        AddHandler twain.TransferImage,
            Sub(sender As Object, args As TwainDotNet.TransferImageEventArgs)
                If (Not (args.Image Is Nothing)) Then
                    '            pictureBox1.Image = args.Image

                    images.Add(args.Image)

                    ' widthLabel.Text = String.Format("Width: {0}", pictureBox1.Image.Width)
                    'heightLabel.Text = String.Format("Height: {0}", pictureBox1.Image.Height)
                End If
            End Sub

        ' Re-enable the form after scanning completes
        AddHandler twain.ScanningComplete,
            Sub(sender As Object, e As TwainDotNet.ScanningCompleteEventArgs)
                'e.Exception.Message
                If images.Count > 0 Then
                    Enabled = True
                    PasaraPDF()
                    AGestor()
                Else
                    MsgBox("No hay imágenes escaneadas")
                    Enabled = True
                End If
                Trabajando = False
            End Sub
    End Sub

    <DllImport("gdi32.dll", ExactSpelling:=True)> Friend Shared Function SetDIBitsToDevice(ByVal hdc As IntPtr, ByVal xdst As Integer, ByVal ydst As Integer, ByVal width As Integer, ByVal height As Integer, ByVal xsrc As Integer, ByVal ysrc As Integer, ByVal start As Integer, ByVal lines As Integer, ByVal bitsptr As IntPtr, ByVal bmiptr As IntPtr, ByVal color As Integer) As Integer
    End Function
    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalLock(ByVal handle As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalFree(ByVal handle As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> Public Shared Sub OutputDebugString(ByVal outstr As String)
    End Sub

    Private Function PasaraPDF() _
           As Boolean
        Dim FOS As FileStream
        Dim imagen As PDFjet.NET.Image
        Dim pics As List(Of PDFjet.NET.Image)
        Dim FPDF As PDF
        Dim page As Page
        Dim sTemp As String
        Dim i As Integer
        Dim PathPdf As String = System.IO.Path.GetRandomFileName()
        Dim PathBMP As String = System.IO.Path.GetTempPath()
        Dim NomhBMP As String = System.IO.Path.GetRandomFileName()
        Dim lon As Integer
        Dim proporcionAncho As Double
        Dim proporcionAlto As Double
        Dim ancho As Integer
        Dim alto As Integer
        Dim empresa As String

        Try

            sTemp = System.IO.Path.Combine(PathBMP, NomhBMP)
            lon = sTemp.Length
            sTemp = sTemp.Substring(0, lon - 3) & "png"

            'empresa = lblEmpresa.Text
            'numero_entrada = lblNumeroEntrada.Text
            carpeta_gd = lblDirectorio.Text

            PathPdf = PathBMP & "\" & Tipodoc & empresa & "_" & Format(numero_entrada, "0000000") & ".pdf"
            PathArchivoPdf = PathPdf

            Dim stream As IO.FileStream = New IO.FileStream(PathPdf, IO.FileMode.Create)
            Dim bos As BufferedStream = New BufferedStream(stream)

            pics = New List(Of PDFjet.NET.Image)

            FPDF = New PDF(bos)

            For i = 0 To images.Count - 1

                images(i).Save(sTemp, Imaging.ImageFormat.Png)

                FOS = New FileStream(sTemp, FileMode.Open, FileAccess.Read)

                imagen = New PDFjet.NET.Image(FPDF, FOS, ImageType.PNG)
                'imagen = New PDFjet.NET.PNGImage(FOS)
                ancho = imagen.GetWidth()
                alto = imagen.GetHeight()
                pics.Add(imagen)

                proporcionAncho = Math.Round((571 / ancho), 1)
                proporcionAlto = Math.Round((807 / alto), 1)

                page = New Page(FPDF, Letter.PORTRAIT)
                pics(i).SetPosition(0.0F, 0.0F)
                pics(i).ScaleBy(proporcionAncho, proporcionAlto)
                pics(i).DrawOn(page)
                FOS.Close()
                File.Delete(sTemp)
            Next
            FPDF.Flush()
            stream.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return False
    End Function


    Private Sub BtSeleccionar_Click(sender As Object, e As EventArgs) Handles BtSeleccionar.Click
        Dim oFrm As MainForm = New MainForm
        oFrm.ShowDialog()
        'LeerConfiguracion()
    End Sub

    Private Sub BtEscanear_Click(sender As Object, e As EventArgs) Handles BtEscanear.Click
        ' Disable the Form until scanning completes
        Enabled = False

        ' Clear off any images from the last run
        images = New List(Of System.Drawing.Bitmap)

        Try
            ' Start scanning. Depending on the settings above dialogs from the scanner driver may be displayed.
            Trabajando = True
            LeerConfiguracion()
            'settings.Area = areaSettings
            twain.StartScanning(settings)

        Catch ex As TwainException
            MessageBox.Show(ex.Message)
            Enabled = True
            Trabajando = False
        End Try

    End Sub

    'Private Sub diagnosticsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles diagnosticsButton.Click
    '    ' Dump out diagnostics from the current source
    '    Dim diagnostics As New Diagnostics(New WinFormsWindowMessageHook(Me))
    'End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub

    Public WriteOnly Property empresa() As String

        Set(ByVal value As String)
            lblEmpresa.Text = Trim(value)
        End Set

    End Property

    Public WriteOnly Property directorio() As String

        Set(ByVal value As String)
            lblDirectorio.Text = Trim(value)
        End Set

    End Property

    Public WriteOnly Property Numeroentrada() As Long

        Set(ByVal value As Long)
            lblNumeroEntrada.Text = CStr(value)
        End Set

    End Property

    Public WriteOnly Property Razonsocial() As Long

        Set(ByVal value As Long)
            lblRazonSocial.Text = CStr(value)
        End Set

    End Property


    'Friend WithEvents CbDialogEscaner As Windows.Forms.CheckBox
    Public Function ChangeWindowMessageFilter(ByVal message As Long, ByVal dwFlag As Long) As Boolean
        Return False
    End Function

    Protected Function GetPixelInfo(ByVal bmpptr As IntPtr) As IntPtr
        bmi = New BitmapInfoHeader()

        Marshal.PtrToStructure(bmpptr, bmi)

        bmprect.X = 0
        bmprect.Y = 0
        bmprect.Width = bmi.biWidth
        bmprect.Height = bmi.biHeight

        If (bmi.biSizeImage = 0) Then
            bmi.biSizeImage = CInt((((bmi.biWidth * bmi.biBitCount) + 31) And CInt(Hex(Not (31)))) / 2 ^ 3) * bmi.biHeight
        End If

        Dim p As Integer = bmi.biClrUsed
        If ((p = 0) And (bmi.biBitCount <= 8)) Then
            p = CInt(1 * 2 ^ bmi.biBitCount)
        End If
        p = (p * 4) + bmi.biSize + CType(bmpptr.ToInt32, Integer)
        Return New IntPtr(p)
    End Function

    Private Sub OpAlbaran_CheckedChanged(sender As Object, e As EventArgs) Handles OpAlbaran.CheckedChanged
        If OpAlbaran.Checked Then
            Tipodoc = "A"
        Else
            Tipodoc = "F"
        End If
    End Sub

    Private Sub OpFactura_CheckedChanged(sender As Object, e As EventArgs) Handles OpFactura.CheckedChanged
        If OpFactura.Checked Then
            Tipodoc = "F"
        Else
            Tipodoc = "A"
        End If
    End Sub

    Private Function ObtenerInformacion() As Boolean
        Dim objStreamReader As StreamReader
        Dim strLine As String
        Dim words As String()
        Dim empresa As String
        Dim entrada As String
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim RsProv As New cnRecordset.CnRecordset
        Dim RsTot As New cnRecordset.CnRecordset
        Dim lReturn As Boolean = False
        Dim PathDoc As String
        Dim ord As Integer
        Dim ind As Integer
        Dim i As Integer

        Try
            'ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
            'If ObjetoGlobal.Inicializar("") = False Then
            '    MsgBox("No se puede abrir la conexión a la BD")
            'End If

            'If (Environment.GetCommandLineArgs().Length > 1) Then
            '    empresa = Environment.GetCommandLineArgs(1)
            '    entrada = Environment.GetCommandLineArgs(2)
            'Else
            '    'Environment.GetCommandLineArgs()[0]
            '    'Pass the file path and the file name to the StreamReader constructor.
            '    objStreamReader = New StreamReader("c:\sdc\datos_factura.txt")

            '    'Read the first line of text.
            '    strLine = objStreamReader.ReadLine

            '    'Close the file.
            '    objStreamReader.Close()
            '    words = strLine.Split(New Char() {","c})
            '    empresa = words(0)
            '    entrada = words(1)
            'End If

            'If Trim(lblEmpresa.Text) = Trim(empresa) And Trim(lblNumeroEntrada.Text) = entrada Then
            '    Return True
            'End If


            Sql = "SELECT * FROM factura_com_c WHERE empresa ='" & ObjetoGlobal.EmpresaActual & "' AND numero_entrada_fra = " & numero_entrada

            If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
                Sql = "SELECT * FROM proveedores_coop WHERE codigo_proveedor=" & Rs!codigo_proveedor
                If RsProv.Open(Sql, ObjetoGlobal.cn, True) Then
#Disable Warning BC42104 ' La variable 'words' se usa antes de que se le haya asignado un valor. Podría darse una excepción de referencia NULL en tiempo de ejecución.
                    lblEmpresa.Text = ObjetoGlobal.EmpresaActual
#Enable Warning BC42104 ' La variable 'words' se usa antes de que se le haya asignado un valor. Podría darse una excepción de referencia NULL en tiempo de ejecución.
                    lblNumeroEntrada.Text = numero_entrada
                    PathDoc = RsProv!carpeta_gd
                    PathDoc = PathDoc.Replace("Ntvirtualdoc", "101.101.100.22\horto_doc")
                    lblDirectorio.Text = PathDoc
                    lblRazonSocial.Text = Rs!razon_social
                    carpeta_gd = RsProv!carpeta_gd
                    aValores(0) = numero_entrada
                    For i = 1 To 6
                        aValores(i) = CStr(Rs(i))
                    Next
                    lReturn = True
                    ord = 0
                    ind = 1
                    RsProv.Close()
                End If
            End If
            Rs.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return lReturn

    End Function

    Private Function AGestor() As Boolean
        Dim oDt As BlClases01.DFFTools
        Dim namefile As String
        Dim PathFile As String
        Dim ficDir As String

        Try
            If File.Exists(PathArchivoPdf) Then
                namefile = IO.Path.GetFileName(PathArchivoPdf)
                PathFile = IO.Path.GetDirectoryName(PathArchivoPdf)

                oDt = New BlClases01.DFFTools(PathFile, namefile)

                ficDir = IO.Path.Combine(lblDirectorio.Text, namefile)
                If oDt.EstaConectado Then
                    oDt.CopiarArchivo(lblDirectorio.Text)
                    If Not GrabarIndices(oDt, ficDir) Then
                        MsgBox(oDt.DescripcionError)
                    End If
                    If oDt.HayError Then
                        MsgBox(oDt.DescripcionError)
                        Return False
                    End If
                    Return True
                Else
                    MsgBox("No ha sido posible establecer la conexión")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub LeerConfiguracion()
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
                'If reader.IsStartElement() Then
                If EnLectura Then
                    If Trim(UCase(reader.Name)) = "VALOR" Then
                        If reader.Read() Then
                            Select Case nombrevar
                                Case "DefaultSourceName"
                                    twain.SelectSource(reader.Value.Trim())
                                Case "UseDocumentFeeder"
                                    settings.UseDocumentFeeder = IIf(reader.Value.Trim() = "Sí", True, False)
                                Case "ShowTwainUI"
                                    settings.ShowTwainUI = IIf(reader.Value.Trim() = "Sí", True, False)
                                Case "ShowProgressIndicatorUI"
                                    settings.ShowProgressIndicatorUI = IIf(reader.Value.Trim() = "Sí", True, False)
                                Case "UseDuplex"
                                    settings.UseDuplex = IIf(reader.Value.Trim() = "No", False, True)
                                Case "Resolution"
                                    settings.Resolution = IIf(reader.Value.Trim() = "B/N", ResolutionSettings.Fax, ResolutionSettings.ColourPhotocopier)
                                Case "Area"
                                        'settings.Area = IIf(reader.Value.Trim() = "Sí", Nothing, areaSettings)
                                Case "ShouldTransferAllPages"
                                    settings.ShouldTransferAllPages = IIf(reader.Value.Trim() = "Sí", True, False)
                                Case "AutomaticRotate"
                                    settings.Rotation.AutomaticRotate = IIf(reader.Value.Trim() = "Sí", True, False)
                                Case "AutomaticBorderDetection"
                                    settings.Rotation.AutomaticBorderDetection = IIf(reader.Value.Trim() = "Sí", True, False)
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
            End While
        End Using

    End Sub

    Private Sub BtSel_Click(sender As Object, e As EventArgs) Handles BtSel.Click
        If OFDialog.ShowDialog() <> DialogResult.OK Then Return
        TxtArchivo.Text = OFDialog.FileName
    End Sub

    Private Sub BtGrabar_Click(sender As Object, e As EventArgs) Handles BtGrabar.Click
        Dim oDt As BlClases01.DFFTools
        Dim namefile As String
        Dim PathFile As String
        Dim Exte As String
        Dim OnlyName As String
        Dim ficDir As String

        Try
            If File.Exists(TxtArchivo.Text) Then
                namefile = IO.Path.GetFileName(TxtArchivo.Text)
                PathFile = IO.Path.GetDirectoryName(TxtArchivo.Text)
                Exte = IO.Path.GetExtension(TxtArchivo.Text)

                OnlyName = Tipodoc & Trim(lblEmpresa.Text) & "_" & Format(CLng(lblNumeroEntrada.Text), "0000000") & Exte

                ficDir = IO.Path.Combine(Trim(lblDirectorio.Text), OnlyName)

                oDt = New BlClases01.DFFTools(PathFile, namefile)
                If oDt.EstaConectado Then
                    oDt.CopiarArchivo(lblDirectorio.Text, OnlyName)
                    If Not GrabarIndices(oDt, ficDir) Then
                        MsgBox(oDt.DescripcionError)
                    End If
                Else
                    MsgBox("No ha sido posible establecer la conexión")
                End If
            End If
        Catch ex As Exception
            MsgBox("6")
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function GrabarIndices(oDt, ruta) As Boolean
        Dim RSGrabarProceso As New cnRecordset.CnRecordset
        'Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

        Try
            'For i = 1 To 16
            '    If i = 8 And aValores(i) = "" And aValores(i + 1) = "" And aValores(i + 2) = "" Then
            '        Exit For
            '    End If
            '    If i = 11 And aValores(i) = "" And aValores(i + 1) = "" And aValores(i + 2) = "" Then
            '        Exit For
            '    End If
            '    If i = 14 And aValores(i) = "" And aValores(i + 1) = "" And aValores(i + 2) = "" Then
            '        Exit For
            '    End If
            '    Select Case aTipo(i)
            '        Case "D"
            '            respuesta = oDt.EscribeIndice(ruta, aTitulos(i), CDbl(Trim(aValores(i))))
            '        Case "F"
            '            respuesta = oDt.EscribeIndice(ruta, aTitulos(i), CDate(Trim(aValores(i))))
            '        Case "C"
            '            respuesta = oDt.EscribeIndice(ruta, aTitulos(i), Trim(aValores(i)))
            '        Case Else
            '            respuesta = ""
            '    End Select
            '    If Trim(respuesta) <> "OK" Then
            '        MsgBox("No se ha podido grabar el campo " & aTitulos(i) & " de la factura en gestión documental")
            '    End If
            'Next

            If RSGrabarProceso.Open("SELECT * FROM GD_PROCESOS WHERE 1=0", ObjetoGlobal.cn, True) Then
                RSGrabarProceso.AddNew()
                RSGrabarProceso!Tipo_Documento = IIf(OpAlbaran.Checked, "Albaran compra", "factura_compra")
                RSGrabarProceso!empresa = Trim(lblEmpresa.Text)
                RSGrabarProceso!numero_entrada = CLng(aValores(0))
                RSGrabarProceso!codigo_proveedor = CLng(aValores(4))
                RSGrabarProceso!carpeta = Trim(IO.Path.GetDirectoryName(ruta))
                RSGrabarProceso!fichero = Trim(IO.Path.GetFileName(ruta))
                RSGrabarProceso!Situacion = "N"
                RSGrabarProceso!proceso_ant = 0
                RSGrabarProceso.Update()
                RSGrabarProceso.Close()
                Return True
            End If

        Catch ex As Exception
            MsgBox("" & ex.Message)
            Return False
        End Try
        Return False

    End Function

    Private Sub Escaneadocumento_Load(sender As Object, e As EventArgs) Handles Me.Load
        ObtenerInformacion()
    End Sub

    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    If Not Trabajando Then
    '        Trabajando = True
    '        ObtenerInformacion()
    '        Trabajando = False
    '    End If

    'End Sub

    'Private Sub Refrescar_Click(sender As Object, e As EventArgs) Handles Refrescar.Click
    '    If Not Trabajando Then
    '        Trabajando = True
    '        ObtenerInformacion()
    '        Trabajando = False
    '    End If
    'End Sub


End Class
