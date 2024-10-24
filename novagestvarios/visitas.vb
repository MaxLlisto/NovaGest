Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.ComponentModel
Imports System.Runtime
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
Imports PdfSharp.Drawing.Layout

Public Class Visitas
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public aConfig(0, 1) As String
    Dim tablet As wgssSTU.Tablet
    Dim capability As wgssSTU.ICapability
    Dim capability2 As wgssSTU.ICapability2
    Dim information As wgssSTU.IInformation


    Private Sub Visitas_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal

        'If ObjetoGlobal.Inicializar("") = False Then
        'MsgBox("No se puede abrir la conexión a la BD")
        'End If
        LeerConfiguracion()
        CargarVisitas()
    End Sub

    Private Sub Buscar_Click(sender As Object, e As EventArgs) Handles Buscar.Click
        Dim RS As New cnRecordset.CnRecordset
        Dim RS1 As New cnRecordset.CnRecordset
        Dim dt As DataTable = New DataTable("Tabla")
        Dim dr As DataRow
        Dim sql As String
        Dim i As Integer
        Dim j As Integer
        Dim Row As DataGridViewRow
        Dim ord As Integer

        If ModoAuditoria.Checked Then
            CabeceraDGV(1)
            If Trim(cbcodigo.Text) <> "" Then
                sql = "SELECT DISTINCT nif FROM visitas_cooperativa WHERE nif LIKE '%" & UCase(Trim(cbcodigo.Text)) & "%'"
                If RS1.Open(sql, ObjetoGlobal.cn) Then
                    dt.Columns.Add("Nombre")
                    dt.Columns.Add("numero_entrada")

                    For j = 1 To RS1.RecordCount
                        RS1.AbsolutePosition = j
                        sql = "SELECT numero_entrada, Fecha, nombre, empresa_visita, nif, hora_entrada, hora_salida, motivo, ruta_pdf, fecha FROM visitas_cooperativa WHERE NIF = '" & RS1("NIF") & "' order by numero_entrada DESC"
                        If RS.Open(sql, ObjetoGlobal.cn) Then
                            For i = 1 To RS.RecordCount
                                RS.AbsolutePosition = i
                                'aaa
                                Row = New DataGridViewRow
                                Row.Height = 36
                                dgv.Rows.Add(Row)
                                ord = dgv.Rows.Count - 2
                                'dgv.Rows(ord).Cells.Item("entrada").Value = RS("NUMERO_ENTRADA")
                                dgv.Rows(ord).Cells.Item("nombre").Value = RS("nombre")
                                dgv.Rows(ord).Cells.Item("empresa").Value = RS("empresa_visita")
                                dgv.Rows(ord).Cells.Item("nif").Value = RS("nif")
                                dgv.Rows(ord).Cells.Item("fecha").Value = RS("fecha")
                                dgv.Rows(ord).Cells.Item("hentrada").Value = RS("hora_entrada")
                                dgv.Rows(ord).Cells.Item("hsalida").Value = RS("hora_salida")
                                dgv.Rows(ord).Cells.Item("path").Value = RS("ruta_pdf")
                            Next
                            RS.Close()
                        End If
                    Next
                    RS1.Close()
                    cbcodigo.DataSource = dt
                    cbcodigo.ValueMember = "numero_entrada"
                    cbcodigo.DisplayMember = "nombre"
                End If
            End If
        Else
            CabeceraDGV(0)
            If Trim(cbcodigo.Text) <> "" Then
                sql = "SELECT DISTINCT nif FROM visitas_cooperativa WHERE nif LIKE '%" & UCase(Trim(cbcodigo.Text)) & "%'"
                If RS1.Open(sql, ObjetoGlobal.cn) Then
                    dt.Columns.Add("Nombre")
                    dt.Columns.Add("numero_entrada")
                    For j = 1 To RS1.RecordCount
                        RS1.AbsolutePosition = j
                        sql = "SELECT TOP 1 nombre, numero_entrada FROM visitas_cooperativa WHERE NIF = '" & RS1("NIF") & "' order by numero_entrada DESC"
                        If RS.Open(sql, ObjetoGlobal.cn) Then
                            For i = 1 To RS.RecordCount
                                RS.AbsolutePosition = i
                                dr = dt.NewRow()
                                dr("numero_entrada") = RS("NUMERO_ENTRADA")
                                dr("nombre") = RS("nombre")
                                dt.Rows.Add(dr)
                            Next
                            RS.Close()
                        End If
                    Next
                    RS1.Close()
                    cbcodigo.DataSource = dt
                    cbcodigo.ValueMember = "numero_entrada"
                    cbcodigo.DisplayMember = "nombre"
                End If
            End If
        End If
    End Sub

    Private Sub CargarGRID(numero_entrada)
        Dim Row As DataGridViewRow
        Dim RS As New cnRecordset.CnRecordset
        Dim ord As Integer

        If RS.Open("SELECT * FROM visitas_cooperativa WHERE numero_entrada = " & numero_entrada, ObjetoGlobal.cn) Then
            Row = New DataGridViewRow
            Row.Height = 36
            dgv.Rows.Add(Row)
            ord = dgv.Rows.Count - 2
            dgv.Rows(ord).Cells.Item("nombre").Value = RS("nombre")
            dgv.Rows(ord).Cells.Item("empresa").Value = RS("empresa_visita")
            dgv.Rows(ord).Cells.Item("nif").Value = RS("nif")
            'dgv.Rows(ord).Cells.Item("salirsalir").Value = WindowsApp7.My.Resources.Resources.salirpersona 'My.Resources.Resource1.GetObject("salirpersona.png")
            dgv.Rows(ord).Cells.Item("entrada").Value = numero_entrada.ToString().Trim()
        End If

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim Ruta As String
        'Dim result As DialogResult
        Dim oFrm As SalidasCooperativa
        Dim Resultado As Integer = 0


        If ModoAuditoria.Checked Then
            Ruta = "" & dgv.Rows(e.RowIndex).Cells("path").Value
            If Ruta.Trim <> "" Then
                Try
                    Process.Start(Ruta)
                Catch ex As Exception
                    MessageBox.Show("Error localizando el archivo: " & ex.Message)
                End Try
            End If
        Else
            If Not IsNothing(dgv.Rows(e.RowIndex).Cells("nif").Value) And Not IsNothing(dgv.Rows(e.RowIndex).Cells("nombre").Value) Then
                MuestraEntrada(dgv.Rows(e.RowIndex).Cells("entrada").Value)
                If e.ColumnIndex = 3 Then ' Salir
                    oFrm = New SalidasCooperativa
                    Resultado = oFrm.ShowDialog()
                    If Resultado = 1 Then
                        SalirDelRecinto(dgv.Rows(e.RowIndex).Cells("entrada").Value, oFrm.Hora)
                        CargarVisitas()
                    End If
                End If
            End If
        End If


    End Sub

    Private Sub GrabarEntrada()
        Dim Rs As New cnRecordset.CnRecordset
        Dim nNumero_entrada As Long
        Dim path_pdf As String = NombrePdf()

        If Rs.Open("SELECT max(numero_entrada) as numero FROM visitas_cooperativa where empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'", ObjetoGlobal.cn) Then
            If Not Rs("Numero") Is Nothing Then
                nNumero_entrada = Rs("Numero") + 1
            Else
                nNumero_entrada = 1
            End If
        End If
        Rs.Close()

        If Rs.Open("SELECT * FROM visitas_cooperativa where 1=0", ObjetoGlobal.cn, True) Then
            Rs.AddNew()
            Rs("Empresa") = Trim(ObjetoGlobal.EmpresaActual)
            Rs("empresa_visita") = TxtEmpresa.Text.Trim()
            Rs("numero_entrada") = nNumero_entrada
            Rs("Fecha") = txtFecha.Text
            Rs("nombre") = TxtNombre.Text.Trim()
            Rs("nif") = TxtNIF.Text
            Rs("tarjeta") = TxtTarjeta.Text
            Rs("hora_entrada") = TxtHora_entrada.Value.ToString("HH:mm")
            Rs("motivo") = TxtMotivo.Text.Trim()
            Rs("telefono") = TxtMovil.Text.Trim()
            Rs("ruta_pdf") = Trim(path_pdf)
            'Rs("hora_salida") = DPHoraEntrada.Value
            Rs.Update()
            Rs.Close()
            CargarVisitas()
        End If
        GrabarModif.Enabled = False

    End Sub
    Private Sub SalirDelRecinto(entrada, hora)
        Dim Rs As New cnRecordset.CnRecordset
        If Rs.Open("SELECT * FROM visitas_cooperativa where numero_entrada=" & entrada, ObjetoGlobal.cn, True) Then
            Rs("hora_salida") = hora
            Rs.Update()
            Rs.Close()
        End If
    End Sub

    Private Sub EntraPersona_Click(sender As Object, e As EventArgs) Handles EntraPersona.Click
        Dim RS As New cnRecordset.CnRecordset
        Dim nNumero_entrada As Long

        If Not cbcodigo.SelectedValue Is Nothing Then

            If RS.Open("SELECT max(numero_entrada) as numero FROM visitas_cooperativa where empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'", ObjetoGlobal.cn) Then
                If Not RS("Numero") Is Nothing Then
                    nNumero_entrada = RS("Numero") + 1
                Else
                    nNumero_entrada = 1
                End If
            End If
            RS.Close()

            If RS.Open("SELECT * FROM visitas_cooperativa WHERE numero_entrada = " & UCase(Trim(cbcodigo.SelectedValue)), ObjetoGlobal.cn) Then
                'RS("Empresa") = Trim(ObjetoGlobal.EmpresaActual)
                TxtNumeroEntrada.ReadOnly = False
                TxtEmpresa.Text = RS("empresa_visita")
                txtFecha.Text = Now.ToShortDateString()
                TxtNombre.Text = RS("nombre")
                TxtNIF.Text = RS("nif")
                TxtTarjeta.Text = "" 'RS("tarjeta")
                TxtHora_entrada.Text = Now.ToLocalTime()
                TxtNumeroEntrada.Text = nNumero_entrada.ToString()
                TxtMotivo.Text = RS("motivo")
                TxtNumeroEntrada.ReadOnly = True
                RS.Close()
            End If
        Else
            MsgBox("Seleccione una persona")
        End If
        GrabarModif.Enabled = False

    End Sub

    Private Sub Firmar_Click(sender As Object, e As EventArgs) Handles Firmar.Click
        Dim usbDevices = New wgssSTU.UsbDevices()
        If (usbDevices.Count <> 0) Then
            Try
                print("Encontrado " + usbDevices.Count.ToString() + " dispositivo")
                Dim usbDevice = usbDevices(0)
                Dim demo = New SignatureForm(Me, usbDevice, TextoAFirmar())
                Dim res = demo.ShowDialog()
                print("Firma devuelta: " + res.ToString())
                If (res = DialogResult.OK) Then
                    DisplaySignature(demo)
                    GrabarEntrada()
                Else
                    MsgBox("Problemas al registrar la entrada")
                End If


            Catch ex As Exception
                MsgBox("Error: " + ex.Message)
            End Try

        Else
            print("No se han encontrado dispositivos")
        End If

    End Sub


    Public Sub print(ByVal txt As String)
        txtDisplay.Text += txt + vbCrLf
        txtDisplay.SelectionStart = txtDisplay.Text.Length ' scroll to end
        txtDisplay.ScrollToCaret()
    End Sub

    Private Sub DisplaySignature(ByVal demo As SignatureForm)
        Dim bitmap As Bitmap
        Dim PathBMP As String = System.IO.Path.GetTempPath()
        Dim NomhBMP As String = System.IO.Path.GetRandomFileName()
        Dim sTemp As String
        Dim lon As Integer

        sTemp = System.IO.Path.Combine(PathBMP, NomhBMP)
        lon = sTemp.Length
        sTemp = sTemp.Substring(0, lon - 3) & "png"

        ' Este módulo hay que arreglarlo para que guarde un PDF con texto y firma 

        bitmap = demo.GetSigImage()
        'bitmap = demo.BackgroundImage()
        'bitmap = demo.BackgroundImageLayout()


        'bitmap.Save("d:\temp\sig.png", Imaging.ImageFormat.Png) ' Guarda la firma en el disco
        ' resize the image to fit the screen
        Dim scale = 2       ' halve or quarter the image size
        If bitmap.Width > 400 Then
            scale = 4
        End If

        bitmap.Save(sTemp, Imaging.ImageFormat.Png) ' Guarda la firma en el disco
        HacerPdf(sTemp)

    End Sub

    Function TextoAFirmar() As TextBox
        Dim text As String
        Dim tHora As String

        tHora = TxtHora_entrada.Value.ToString("HH:mm") ' .GetDateTimeFormats("hh:mm:ss")

        text = "El que firma : " & TxtNombre.Text.Trim() & vbCrLf & "con el NIF : " & TxtNIF.Text.Trim() & vbCrLf
        text = text + "trabajador de la empresa : " & TxtEmpresa.Text.Trim() & vbCrLf
        text = text + "entra en el recinto el día : " & txtFecha.Text.Trim() & " a las " & tHora & vbCrLf
        text = text + "para : " & TxtMotivo.Text.Trim() & vbCrLf
        text = text + "Firma  : "


        TxtTextoFirma.Text = text

        Return TxtTextoFirma

    End Function

    Function TextoAFirmarPDF() As String
        Dim text As String
        Dim tHora As String

        tHora = TxtHora_entrada.Value.ToString("HH:mm") ' .GetDateTimeFormats("hh:mm:ss")

        text = "El que firma : " & TxtNombre.Text.Trim() & vbCrLf
        text = text + "trabajador de la empresa : " & TxtEmpresa.Text.Trim() & vbCrLf
        text = text + "con el NIF : " & TxtNIF.Text.Trim() & vbCrLf
        text = text + "entra en el recinto el día : " & txtFecha.Text.Trim() & " a las " & tHora & vbCrLf
        text = text + "para : " & TxtMotivo.Text.Trim() & vbCrLf
        text = text + "con tarjeta de visitante número : " & TxtTarjeta.Text.Trim() & vbCrLf
        text = text + "Y para que así conste firma  : "

        Return text

    End Function

    Sub HacerPdf(cPathImagen)
        Dim Documento As PdfDocument = New PdfDocument                       ' Crea el documento Pdf
        Dim pagina As PdfPage = Documento.AddPage                        ' Crea una pagina vacia
        Dim FteNormal As XFont = New XFont("Arial", 10, XFontStyle.Regular)  ' Crea la fuente
        Dim FteNegrilla As XFont = New XFont("Arial", 15, XFontStyle.Bold)   ' Crea la fuente 
        Dim pgfx As XGraphics = XGraphics.FromPdfPage(pagina)           ' Crea un Objeto XGraphics 
        Dim tf As XTextFormatter                                            ' Objeto para formatear texto
        'Dim NombrePdf As String
        Dim TextoPdf As String
        Dim sf As XStringFormat = New XStringFormat()

        Dim Archivo As String = NombrePdf()

        pagina.Size = PageSize.Letter
        pagina.Orientation = PageOrientation.Portrait

        'Escribimos el titulo
        Dim rect As New XRect(15, 15, 585, 20)
        pgfx.DrawString("Control de visitas de la Cooperativa Vínicola de Llíria", FteNegrilla, XBrushes.Black, rect, XStringFormat.Center)

        'escribimos el texto
        TextoPdf = TextoAFirmarPDF()
        tf = New XTextFormatter(pgfx)
        tf.Alignment = XParagraphAlignment.Justify
        rect = New XRect(15, 45, 585, 60)
        tf.DrawString(TextoPdf, FteNormal, XBrushes.Black, rect)

        'pgfx.DrawString(TextoPdf, FteNormal, XBrushes.Black, 150, 150)

        'Insertamos la imagen en una hoja nueva
        If File.Exists(cPathImagen) Then
            Dim imagen As XImage = XImage.FromFile(cPathImagen)
            pgfx.DrawImage(imagen, 50, 150, 500, 375)
        End If

        Try
            'si el archivo esta abierto da error
            'para que averiguen como saber si ela rchivo esta abierto y cerrarlo
            ' antes de grabar uno nuevo
            Documento.Save(Archivo)
        Catch ex As Exception
        End Try
    End Sub
    Private Function NombrePdf()
        Dim cpath As String
        cpath = Retornaconf("PATH IMAGENES")

        cpath = cpath + TxtNIF.Text.Trim() & "_" & txtFecha.Value.ToString("yyyymmdd") & TxtHora_entrada.Value.ToString("mmss") & ".pdf"
        Return cpath

    End Function

    Private Sub LeerConfiguracion()
        Dim linea As Integer
        Dim EnLectura As Boolean

        ReDim aConfig(1, 0)

        EnLectura = False
        Using reader As XmlReader = XmlReader.Create(My.Computer.FileSystem.CurrentDirectory & "\config.xml")
            linea = 0

            While reader.Read()
                If reader.IsStartElement() Then
                    If EnLectura Then
                        If Trim(UCase(reader.Name)) = "VALOR" Then
                            If reader.Read() Then
                                aConfig(1, linea) = reader.Value.Trim()
                                EnLectura = False
                            End If
                        End If
                    End If
                    If Trim(UCase(reader.Name)) = "VARIABLE" Then
                        If EnLectura Then
                            Exit While
                        End If
                        'If reader.Read() Then
                        If Trim(UCase(reader("NOMBRE"))) <> "" Then
                            linea = linea + 1
                            ReDim Preserve aConfig(1, linea)
                            'Array.Resize(aConfig, aConfig.Length + 1)
                            aConfig(0, linea) = reader("NOMBRE")
                            EnLectura = True
                        End If
                        'End If
                    End If
                End If
            End While
        End Using
    End Sub
    Private Function Retornaconf(ByVal arg As String) As String
        Dim i As Integer
        Dim Ret As String


        For i = 0 To aConfig.GetLength(1) - 1
            If UCase(Trim(aConfig(0, i))) = UCase(Trim(arg)) Then
                Ret = aConfig(1, i)
                Return Replace(Ret, Chr(34), "")
            End If
        Next
        Return ""

    End Function

    Private Sub CabeceraDGV(n)

        If n = 0 Then
            dgv().Columns().Clear()
            Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column1.Name = "nombre"
            column1.HeaderText = "Nombre y apellidos"
            column1.Width = 300
            dgv().Columns.Add(column1)

            Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column2.Name = "empresa"
            column2.HeaderText = "Empresa del visitante"
            column2.Width = 300
            dgv().Columns.Add(column2)

            Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column3.Name = "nif"
            column3.HeaderText = "NIF"
            column3.Width = 125
            dgv().Columns.Add(column3)

            Dim column4 As DataGridViewImageColumn = New DataGridViewImageColumn()
            column4.Name = "salir"
            column4.HeaderText = ""
            'column4.Width = 150
            'column4.DefaultCellStyle = New DataGridViewCellStyle() ' {NullValue = System.Drawing.Bitmap, Alignment = MiddleCenter}
            column4.Image = My.Resources.Resources.salirpersona
            dgv().Columns.Add(column4)


            Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column5.Name = "Entrada"
            column5.HeaderText = "Entrada"
            column5.Width = 150
            dgv().Columns.Add(column5)
        Else
            dgv().Columns().Clear()
            Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column1.Name = "nombre"
            column1.HeaderText = "Nombre y apellidos"
            column1.Width = 300
            dgv().Columns.Add(column1)

            Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column2.Name = "empresa"
            column2.HeaderText = "Empresa del visitante"
            column2.Width = 300
            dgv().Columns.Add(column2)

            Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column3.Name = "nif"
            column3.HeaderText = "NIF"
            column3.Width = 125
            dgv().Columns.Add(column3)

            Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column4.Name = "fecha"
            column4.HeaderText = "fecha entrada"
            column4.Width = 125
            dgv().Columns.Add(column4)

            Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column5.Name = "hentrada"
            column5.HeaderText = "Hora entrada"
            column5.Width = 125
            dgv().Columns.Add(column5)

            Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column6.Name = "hsalida"
            column6.HeaderText = "Hora salida"
            column6.Width = 125
            dgv().Columns.Add(column6)

            Dim column7 As DataGridViewImageColumn = New DataGridViewImageColumn()
            column7.Name = "ver"
            column7.HeaderText = "ver"
            column7.Width = 125
            column7.Image = My.Resources.Resources.FIRMADO
            dgv().Columns.Add(column7)

            Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column8.Name = "path"
            column8.HeaderText = ""
            column8.Width = 0
            dgv().Columns.Add(column8)


        End If

    End Sub
    Private Sub CargarVisitas()
        Dim Row As DataGridViewRow
        Dim RS As New cnRecordset.CnRecordset
        Dim ord As Integer
        Dim i As Integer
        Dim d As DateTime = Now()


        If RS.Open("SELECT * FROM visitas_cooperativa WHERE empresa = '" & ObjetoGlobal.EmpresaActual.Trim() & "' and fecha >= '" & d.AddDays(-15).ToString("dd/MM/yyyy") & "' and hora_salida IS NULL", ObjetoGlobal.cn) Then
            CabeceraDGV(0)
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                Row.Height = 36
                dgv.Rows.Add(Row)
                ord = dgv.Rows.Count - 2
                dgv.Rows(ord).Cells.Item("nombre").Value = RS("nombre")
                dgv.Rows(ord).Cells.Item("empresa").Value = RS("empresa_visita")
                dgv.Rows(ord).Cells.Item("nif").Value = RS("nif")
                'dgv.Rows(ord).Cells.Item("salirsalir").Value = WindowsApp7.My.Resources.Resources.salirpersona 'My.Resources.Resource1.GetObject("salirpersona.png")
                dgv.Rows(ord).Cells.Item("entrada").Value = RS("numero_entrada").ToString()
            Next i
        End If

    End Sub

    Private Sub ModoAuditoria_CheckedChanged(sender As Object, e As EventArgs) Handles ModoAuditoria.CheckedChanged
        If ModoAuditoria.Checked Then
            CabeceraDGV(1)
        Else
            CabeceraDGV(0)
            CargarVisitas()
        End If

    End Sub

    Private Sub ModoAuditoria_CheckStateChanged(sender As Object, e As EventArgs) Handles ModoAuditoria.CheckStateChanged

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Dim Msg As MsgBoxResult
        'Msg = MsgBox("Cerrar el control de visitas, ¿Desea salir?", vbYesNo, "Salir")
        'If Msg = MsgBoxResult.Yes Then
        'Application.ExitThread()
        Me.Close()
        'Else
        'Exit Sub
        'End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        GrabarEntrada()
    End Sub

    Private Sub MuestraEntrada(numero_entrada)
        Dim RS As New cnRecordset.CnRecordset

        If numero_entrada <> 0 Then
            If RS.Open("SELECT * FROM visitas_cooperativa WHERE numero_entrada = " & numero_entrada.ToString.Trim, ObjetoGlobal.cn) Then
                TxtNumeroEntrada.ReadOnly = False
                TxtEmpresa.Text = RS("empresa_visita")
                txtFecha.Text = RS("fecha")
                TxtNombre.Text = RS("nombre")
                TxtNIF.Text = RS("nif")
                TxtTarjeta.Text = RS("tarjeta")
                TxtHora_entrada.Text = RS("hora_entrada") ' Now.ToLocalTime()
                TxtNumeroEntrada.Text = numero_entrada.ToString().Trim()
                TxtMotivo.Text = RS("motivo")
                RS.Close()
                TxtNumeroEntrada.ReadOnly = True
            End If
        End If


    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        GrabarEntrada()
    End Sub

    Private Sub aSololectura(larg)
        TxtEmpresa.ReadOnly = larg
        'txtFecha.ReadOnly = larg
        TxtNombre.ReadOnly = larg
        TxtNIF.ReadOnly = larg
        TxtTarjeta.ReadOnly = larg
        'TxtHora_entrada.ReadOnly = larg
        TxtNumeroEntrada.ReadOnly = larg
        TxtMotivo.ReadOnly = larg
    End Sub

    Private Sub tTextChanged(sender As Object, e As EventArgs) Handles TxtEmpresa.TextChanged, TxtHoraSalida.ValueChanged, TxtTarjeta.TextChanged, TxtHora_entrada.ValueChanged, TxtMotivo.TextChanged, TxtMovil.TextChanged, TxtNIF.TextChanged, TxtNombre.TextChanged
        If TxtNumeroEntrada.Text.Trim <> "" Then
            GrabarModif.Enabled = True
        End If

    End Sub

    Private Sub tKeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtEmpresa.KeyPress, TxtHoraSalida.KeyPress, TxtTarjeta.KeyPress, TxtHora_entrada.KeyPress, TxtMotivo.KeyPress, TxtMovil.KeyPress, TxtNIF.KeyPress, TxtNombre.KeyPress
        If TxtNumeroEntrada.Text.Trim <> "" Then
            GrabarModif.Enabled = True
        End If
    End Sub

    Private Sub GrabarModif_Click(sender As Object, e As EventArgs) Handles GrabarModif.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim numero_entrada As String

        If TxtNumeroEntrada.Text.Trim = "" Then
            GrabarModif.Enabled = False
            Return
        End If

        numero_entrada = TxtNumeroEntrada.Text
        If Rs.Open("SELECT * FROM visitas_cooperativa WHERE numero_entrada = " & numero_entrada.ToString.Trim, ObjetoGlobal.cn, True) Then
            Rs("Empresa") = Trim(ObjetoGlobal.EmpresaActual)
            Rs("empresa_visita") = TxtEmpresa.Text.Trim()
            Rs("Fecha") = txtFecha.Text
            Rs("nombre") = TxtNombre.Text.Trim()
            Rs("nif") = TxtNIF.Text.Trim()
            Rs("tarjeta") = TxtTarjeta.Text.Trim
            Rs("hora_entrada") = TxtHora_entrada.Value.ToString("HH:mm").Trim()
            Rs("motivo") = TxtMotivo.Text.Trim()
            Rs("telefono") = TxtMovil.Text.Trim()
            Rs.Update()
            Rs.Close()
            CargarVisitas()
        End If
        GrabarModif.Enabled = False

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub Nuevo_Click(sender As Object, e As EventArgs) Handles Nuevo.Click

    End Sub
End Class
