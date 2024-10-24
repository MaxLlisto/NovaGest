'Imports System.Drawing.Printing
'Imports System.Collections.Specialized.StringDictionary


'Public Class InformesNuevo
'    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
'    Public INHaySeleccionFormato As Boolean
'    Public ImpresoraDefecto As String
'    Public printset As New Printing.PrinterSettings


'    Public Sub AbrirRecordsetsImpresion()
'        Dim i As Integer, Rs As cnRecordset.CnRecordset

'        Rs.Open("SELECT * FROM CONTADORES WHERE EMPRESA = 'T' AND EJERCICIO = 'T' AND NOMBRE = 'ultima_impresion' and serie = 'T'", ObjetoGlobal.cn, True)
'        If Rs.RecordCount = 0 Then
'            Rs.AddNew()
'            Rs!Empresa = "T"
'            Rs!Ejercicio = "T"
'            Rs!nombre = "ultima_impresion"
'            Rs!serie = "T"
'            Rs!contador = 0
'            Rs.Update()
'        End If
'        CodigoImpresion = Rs!contador + 1
'        Rs!contador = CodigoImpresion
'        Rs.Update()
'        Rs.Close()

'    End Sub
'    Public Function RetornaFormato(ByVal pProceso As String, ByVal pDocumento As String, ByVal pFormato As String) As Boolean
'        Dim SQL As String, i As Integer
'        Dim Defecto As Integer, ExisteImpresoraDefecto As Boolean
'        Dim Rs As cnRecordset.CnRecordset
'        Dim oFormatos As FrmSeleccionFormato = New FrmSeleccionFormato

'        RetornaFormato = False
'        SQL = "SELECT * FROM ZZFORMATOSDEFECTO WHERE"
'        If Trim(pProceso) > "" Then
'            SQL = SQL + " PROCESO = '" + Trim(pProceso) + "' "
'        End If
'        If Trim(pDocumento) > "" Then
'            If Right(SQL, 5) = "WHERE" Then
'                SQL = SQL + " DOCUMENTO = '" + Trim(pDocumento) + "' "
'            Else
'                SQL = SQL + " AND DOCUMENTO = '" + Trim(pDocumento) + "' "
'            End If
'        End If
'        If Trim(pFormato) > "" Then
'            If Right(SQL, 5) = "WHERE" Then
'                SQL = SQL + " FORMATO = '" + Trim(pFormato) + "' "
'            Else
'                SQL = SQL + " AND FORMATO = '" + Trim(pFormato) + "' "
'            End If
'        End If
'        If Right(SQL, 5) = "WHERE" Then
'            ' "Debe indicarse documento a imprimir.", "Retorna formato", "Informes nuevo", True
'            Return False
'        End If
'        Rs.Open(SQL, ObjetoGlobal.cn)
'        If Rs.RecordCount = 0 Then
'            ' "Debe indicarse formato de documento a imprimir (zzformatosdefecto)", "Retorna formato", "Informes nuevo", True
'            Return False
'        ElseIf Rs.RecordCount > 1 Then
'            Defecto = -1
'            Rs.MoveFirst()

'            For i = 0 To Rs.RecordCount - 1
'                oFormatos.Formatos = Rs!formato
'                If Trim("" & Rs!Defecto) <> "" Then If CBool(Rs!Defecto) Then Defecto = i
'                Rs.MoveNext()
'            Next

'            If Defecto > -1 Then
'                Rs.AbsolutePosition = i
'            Else
'                'FrmElegirFormatoNuevo.lblProceso = pProceso
'                Rs.MoveFirst()

'                For i = 0 To Rs.RecordCount - 1
'                    oFormatos.Formatos = Rs!formato
'                    Rs.MoveNext()
'                Next
'                oFormatos.ShowDialog()

'                If INHaySeleccionFormato = True Then
'                    Rs.AbsolutePosition = FrmSeleccionFormato.Seleccionado
'                Else
'                    Exit Function
'                End If
'            End If
'        End If
'        INApaisado = LCase(Trim("" & Rs!a1)) = "apaisado"
'        INCopiasImpresion = IIf(Not IsNothing(Rs!Copias), Rs!Copias, 1)
'        INImpresoraFormato = ""
'        If Trim("" & Rs!ImpresoraDefecto) <> "" And Trim("" & (Rs!ImpresoraDefecto)) <> "" Then
'            INImpresoraFormato = Trim(Rs!ImpresoraDefecto)
'        End If
'        If INModoSeleccionImpresora = 2 Then
'            If Trim(INImpresoraFormato) = "" Then
'                INImpresoraFormato = Trim(INImpresoraSistema)
'            End If
'            If Trim(INImpresoraFormato) = "" Then
'                ' "Se ha solicitado impresión en impresora no determinada.", "Retorna formato", "Informes nuevo", True
'                Return False
'            Else
'                ExisteImpresoraDefecto = False
'                For Each strPrinter As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
'                    If Trim(LCase(strPrinter)) = Trim(LCase(INImpresoraFormato)) Then
'                        ExisteImpresoraDefecto = True
'                        Return True
'                    End If
'                Next
'                If ExisteImpresoraDefecto = False Then
'                    ' "Se ha solicitado impresión en impresora inexistente.", "Retorna formato", "Informes nuevo", True
'                    Return False
'                End If
'            End If
'        End If
'        If Trim(INImpresoraFormato) = "" Then INImpresoraFormato = INImpresoraSistema
'        INProceso = Trim(Rs!proceso)
'        INDocumento = Trim(Rs!Documento)
'        INFormato = Trim(Rs!formato)
'        If Trim(Rs!imprimePDF) = "S" Then 'PDF
'            HayImpresionPDF = True 'PDF
'            NombreImpresoraPDF = Trim(Rs!impresoraPDF) 'PDF
'            CarpetaParaPDF = Trim(Rs!carpetapdf) 'PDF
'            If InStr(CarpetaParaPDF, "$0") > 0.5 Then 'PDF
'                CarpetaParaPDF = Replace(CarpetaParaPDF, "$0", Trim(INEmpresa)) 'PDF
'            End If 'PDF
'        Else 'PDF
'            HayImpresionPDF = False 'PDF
'            NombreImpresoraPDF = "" 'PDF
'            CarpetaParaPDF = "" 'PDF
'        End If 'PDF
'        RetornaFormato = True
'    End Function
'    Public Function DefinirDiccionarioCampos() As Dictionary(Of String, Long())
'        Dim Rs As cnRecordset.CnRecordset
'        Dim i As Integer
'        Dim DCT As Dictionary(Of String, Long()), Datos(3) As Long, Clave As String

'        DCT = New Dictionary(Of String, Long())

'        Rs.Open("SELECT * FROM ZZZINFORMATOS WHERE DOCUMENTO='" & Trim(INDocumento) & "' AND FORMATO='" & Trim(INFormato) & "' AND TIPO='campo' ORDER BY ZONA,NUM_DATO", ObjetoGlobal.cn, True)
'        If Rs.RecordCount > 0 Then
'            Rs.MoveFirst()

'            For i = 0 To Rs.RecordCount - 1
'                If Trim(Rs!Valor) > "" Then
'                    If Trim(Rs!Zona) = "cabecera" Then
'                        Datos(0) = 0
'                    ElseIf Trim(Rs!Zona) = "cuerpo" Then
'                        Datos(0) = 1
'                    Else '"pie"
'                        Datos(0) = 2
'                    End If
'                    Datos(1) = Rs!num_dato
'                    Datos(2) = Rs!tope
'                    Datos(3) = Rs!izquierda
'                    Clave = Format(Trim(Rs!Zona), "!" & StrDup(10, "@")) + Format(LCase(Trim(Rs!Valor)), "!" & StrDup(60, "@")) + Format(Rs!Indice, "0000000000")
'                    '            MsgBox Clave
'                    DCT.Add(Trim(Clave), Datos)
'                End If
'                Rs.MoveNext()
'            Next i
'            Return DCT
'        Else
'            ' "El formato elegido no existe en la base de datos o no contiene campos", "Infexp", "InformesNuevo", True
'            '    Set DefinirDiccionarioCampos = Nothing
'            Return Nothing
'        End If

'    End Function
'    Private Sub AbrirRecordsetFormato()
'        INRsFormato.Open("SELECT * FROM ZZZINFORMATOS WHERE DOCUMENTO='" & Trim(INDocumento) & "' AND FORMATO='" & Trim(INFormato) & "'", ObjetoGlobal.cn, True)
'    End Sub



'    Private Function seleccionarImpresora() As Boolean
'        Dim prtDialog As New PrintDialog

'        With prtDialog
'            .AllowPrintToFile = False
'            .AllowSelection = False
'            .AllowSomePages = False
'            .PrintToFile = False
'            .ShowHelp = False
'            .ShowNetwork = True
'            If .ShowDialog() = DialogResult.OK Then
'                printset.PrinterName = .PrinterSettings.PrinterName
'            Else
'                Return False
'            End If
'        End With
'        Return True
'    End Function

'    Public Sub InicializaImpresion()
'        Dim i As Long, Clave2 As String, Clave3 As String
'        Dim Pagina As Long, ContadorPagina As Long
'        Dim DocumentoLeido As Long, DocumentoAnterior As Long, RegistroLeido As Long, FormatoLeido As Long
'        Dim AlturaPagina As Long, AlturaCuerpoActual As Long

'        INTotalPaginas = 0
'        INAltoPaginaReal = printset.DefaultPageSettings.PaperSize.Height
'        INAnchoPaginaReal = printset.DefaultPageSettings.PaperSize.Width

'        If INPrevisualizacion Then
'            'FrmPresFormato.Inicializar
'        End If

'        'NUEVO INDiccionarioPaginas.RemoveAll
'        INRsPaginas.Open("SELECT * FROM ZZZPAGINAS", ObjetoGlobal.cn, True)

'        While INRsPaginas.RecordCount > 0
'            INRsPaginas.MoveFirst()
'            INRsPaginas.Delete()
'        End While
'        INRsPaginas.Close()

'        INRsPaginas.Open("SELECT * FROM ZZZPAGINAS WHERE 1=0", ObjetoGlobal.cn, True)


'        AbrirRecordsetFormato()

'        For i = 0 To 10 : INAlturaCuerpo(i) = 0 : Next
'        INAlturaCab0 = 0
'        INAlturaCab1 = 0
'        INAlturaPie0 = 0
'        INAlturaPie1 = 0

'        While Not INRsFormato.EOF
'            If Trim(INRsFormato!tipo) = "banda" Then
'                If INRsFormato!alto > 15 Then
'                    Select Case Trim(INRsFormato!Zona)
'                        Case "cabecera"
'                            If INRsFormato!Indice = 0 Then
'                                INAlturaCab0 = INRsFormato!alto
'                            ElseIf INRsFormato!Indice = 1 Then
'                                INAlturaCab1 = INRsFormato!alto
'                            End If
'                        Case "cuerpo"
'                            If INRsFormato!Indice < 11 Then
'                                INAlturaCuerpo(INRsFormato!Indice) = INRsFormato!alto
'                            End If
'                        Case "pie"
'                            If INRsFormato!Indice = 0 Then
'                                INAlturaPie0 = INRsFormato!alto
'                            ElseIf INRsFormato!Indice = 1 Then
'                                INAlturaPie1 = INRsFormato!alto
'                            End If
'                    End Select
'                End If
'            End If
'            INRsFormato.MoveNext()
'        End While

'        If INAlturaPie1 > INAlturaPie0 Then
'            INAlturaMaximoPie = INAlturaPie1
'        Else
'            INAlturaMaximoPie = INAlturaPie0
'        End If

'        Pagina = 0 : DocumentoLeido = -1 : DocumentoAnterior = -1 : AlturaPagina = 0 : ContadorPagina = -1
'        'For i = 0 To INDiccionarioDocumentos.Count - 1

'        For Each Clave2 In INDiccionarioDocumentos.Keys
'            RegistroLeido = -1 : FormatoLeido = -1
'            'Clave2 = INDiccionarioDocumentos.Keys(i)
'            DocumentoLeido = CLng(Left(Clave2, 10))
'            If Len(Clave2) > 10 Then
'                RegistroLeido = CLng(Mid(Clave2, 11, 10))
'                FormatoLeido = CLng(Mid(Clave2, 21, 10))
'            End If
'            If DocumentoLeido <> DocumentoAnterior Then
'                If Pagina > 0 Then
'                    ContadorPagina = ContadorPagina + 1
'                    Clave3 = Format(Pagina, "0000000000") + Format(ContadorPagina, "0000000000") + Format(DocumentoAnterior, "0000000000") + "2010000000000" 'pie final del documento
'                    'NUEVO INDiccionarioPaginas.Add Clave3, ""
'                    INRsPaginas.AddNew() 'NUEVO
'                    INRsPaginas!Pagina = Pagina 'NUEVO
'                    INRsPaginas!Clave = Trim(Clave3) 'NUEVO
'                    INRsPaginas!Dato = "" 'NUEVO
'                    INRsPaginas.Update() 'NUEVO
'                    AlturaPagina = 0
'                End If
'                Pagina = Pagina + 1
'                ContadorPagina = 1
'                Clave3 = Format(Pagina, "0000000000") + Format(ContadorPagina, "0000000000") + Format(DocumentoLeido, "0000000000") + "0000000000000" 'cabecera inicial del documento
'                'NUEVO INDiccionarioPaginas.Add Clave3, ""
'                INRsPaginas.AddNew() 'NUEVO
'                INRsPaginas!Pagina = Pagina 'NUEVO
'                INRsPaginas!Clave = Trim(Clave3) 'NUEVO
'                INRsPaginas!Dato = "" 'NUEVO
'                INRsPaginas.Update() 'NUEVO
'                AlturaPagina = INAlturaCab0
'            End If
'            If FormatoLeido >= 0 Then
'                AlturaCuerpoActual = INAlturaCuerpo(FormatoLeido)
'                If AlturaPagina + AlturaCuerpoActual + INAlturaMaximoPie > INAltoPaginaReal Then
'                    ContadorPagina = ContadorPagina + 1
'                    Clave3 = Format(Pagina, "0000000000") + Format(ContadorPagina, "0000000000") + Format(DocumentoLeido, "0000000000") + "2000000000000" 'pie parcial
'                    'NUEVO INDiccionarioPaginas.Add Clave3, ""
'                    INRsPaginas.AddNew() 'NUEVO
'                    INRsPaginas!Pagina = Pagina 'NUEVO
'                    INRsPaginas!Clave = Trim(Clave3) 'NUEVO
'                    INRsPaginas!Dato = "" 'NUEVO
'                    INRsPaginas.Update() 'NUEVO
'                    AlturaPagina = 0
'                    Pagina = Pagina + 1
'                    ContadorPagina = 1
'                    Clave3 = Format(Pagina, "0000000000") + Format(ContadorPagina, "0000000000") + Format(DocumentoLeido, "0000000000") + "0010000000000" 'cabecera continuacion
'                    'NUEVO INDiccionarioPaginas.Add Clave3, ""
'                    INRsPaginas.AddNew() 'NUEVO
'                    INRsPaginas!Pagina = Pagina 'NUEVO
'                    INRsPaginas!Clave = Trim(Clave3) 'NUEVO
'                    INRsPaginas!Dato = "" 'NUEVO
'                    INRsPaginas.Update() 'NUEVO
'                    AlturaPagina = INAlturaCab1
'                End If
'                ContadorPagina = ContadorPagina + 1
'                Clave3 = Format(Pagina, "0000000000") + Format(ContadorPagina, "0000000000") + Format(DocumentoLeido, "0000000000") + "1" + Format(FormatoLeido, "00") + Format(RegistroLeido, "0000000000") 'cuerpo
'                'NUEVO INDiccionarioPaginas.Add Clave3, ""
'                INRsPaginas.AddNew() 'NUEVO
'                INRsPaginas!Pagina = Pagina 'NUEVO
'                INRsPaginas!Clave = Trim(Clave3) 'NUEVO
'                INRsPaginas!Dato = "" 'NUEVO
'                INRsPaginas.Update() 'NUEVO
'                AlturaPagina = AlturaPagina + INAlturaCuerpo(FormatoLeido)
'            End If
'            DocumentoAnterior = DocumentoLeido
'        Next
'        If Pagina > 0 Then
'            ContadorPagina = ContadorPagina + 1
'            Clave3 = Format(Pagina, "0000000000") + Format(ContadorPagina, "0000000000") + Format(DocumentoAnterior, "0000000000") + "2010000000000" 'pie final del documento
'            'NUEVO INDiccionarioPaginas.Add Clave3, ""
'            INRsPaginas.AddNew() 'NUEVO
'            INRsPaginas!Pagina = Pagina 'NUEVO
'            INRsPaginas!Clave = Trim(Clave3) 'NUEVO
'            INRsPaginas!Dato = "" 'NUEVO
'            INRsPaginas.Update() 'NUEVO
'        End If
'        INTotalPaginas = Pagina
'        'MsgBox("Inicializa impresion fin" 'NUEVO 21/12
'    End Sub
'    Public Sub ImprimePaginas(DesdePagina As Long, HastaPagina As Long)
'        Dim i As Long, MaxRegistro As Long
'        Dim nCopia As Integer, HastaCopias As Integer, NPagina As Long
'        Dim PaginaLeida As Long, DocumentoLeido As Long, DocumentoAnterior As Long, ZonaLeida As String, RegistroLeido As Long, FormatoLeido As String, TipoLineaLeido As Integer
'        Dim Clave2 As String, Clave3 As String, Dato3 As String, FlagEncontrado As Boolean, NombreF As String
'        Dim ExisteLaImpresoraPDF As Boolean, ContadorPDF As Long

'        'MsgBox("Imprime paginas empiezo" 'NUEVO 21/12
'        If DesdePagina < 1 Then DesdePagina = 1
'        If DesdePagina > INTotalPaginas Then DesdePagina = INTotalPaginas
'        If HastaPagina < DesdePagina Then HastaPagina = DesdePagina
'        If HastaPagina > INTotalPaginas Then HastaPagina = INTotalPaginas

'        If INPrevisualizacion = True Then
'            HastaCopias = 1
'            'FrmPresFormato.picSeccion.Cls
'            HastaPagina = DesdePagina
'        Else
'            If FuerzaCop > 0 Then
'                HastaCopias = FuerzaCop
'                FuerzaCop = 0
'            Else
'                HastaCopias = INCopiasImpresion
'            End If
'        End If
'        If HayImpresionPDF = True Then HastaCopias = HastaCopias + 1 'PDF
'        For nCopia = 1 To HastaCopias
'            If HayImpresionPDF = True And nCopia = HastaCopias Then 'PDF
'                ImpresoraAnterior = Trim(System.Drawing.Printing.PrinterSettings.) 'PDF
'                ExisteLaImpresoraPDF = False 'PDF

'                For Each printerName As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters()
'                    If Trim(LCase(NombreImpresoraPDF)) = printerName Then
'                        ExisteLaImpresoraPDF = True
'                        Exit For
'                    End If
'                Next

'                If Not ExisteLaImpresoraPDF Then 'PDF
'                    For Each printerName As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters()
'                        If Trim(LCase(printerName)) = Trim(LCase(NombreImpresoraPDF)) + " local" Then 'PDF
'                            ''''''''''''''''''Set Printer = OBJPrinter 'PDF
'                            ExisteLaImpresoraPDF = True 'PDF
'                            Exit For 'PDF
'                        End If 'PDF
'                    Next 'PDF
'                    If ExisteLaImpresoraPDF = False Then 'PDF
'                        ' "El documento se imprimirá normalmente, pero no se puede generar archivo histórico.", vbInformation 'PDF
'                        HayImpresionPDF = False 'PDF
'                    End If 'PDF
'                End If 'PDF
'            End If 'PDF
'            DocumentoAnterior = -9999
'            For NPagina = DesdePagina To HastaPagina
'                FlagEncontrado = False
'                INAlturaActual = 0

'                Dim foundRows As MiDatatable = New MiDatatable

'                foundRows = INRsPaginas.cnDataSet.Tables(0)
'                Dim DsFiltrada As Data.DataTable

'                DsFiltrada = foundRows.Filter("pagina = " + CStr(NPagina), "clave")

'                For i = 1 To DsFiltrada.Rows.Count
'                    Clave2 = Trim(DsFiltrada.Rows(i)("clave"))
'                    FlagEncontrado = True
'                    DocumentoLeido = CLng(Mid(Clave2, 21, 10))
'                    FormatoLeido = Mid(Clave2, 31, 3)
'                    RegistroLeido = CLng(Mid(Clave2, 34, 10))
'                    If HayImpresionPDF = True And DocumentoLeido <> DocumentoAnterior And nCopia = HastaCopias Then 'PDF
'                        If DocumentoAnterior <> -9999 Then Printer.EndDoc
'                        Clave3 = Format(DocumentoLeido, "0000000000") 'PDF
'                        If Not INDiccionarioDocumentos.ContainsKey(Clave3) Then 'PDF
'                            MsgBox("No se puede grabar el fichero PDF asociado al documento num.:" + CStr(DocumentoLeido))
'                        Else 'PDF
'                            Dato3 = INDiccionarioDocumentos.Item(Clave3) 'PDF
'                            NombreF = Trim(CarpetaParaPDF) + Trim(Dato3) + ".ps" 'PDF
'                            ContadorPDF = 0 'PDF
'                            Do While Dir(NombreF) > "" 'PDF
'                                ContadorPDF = ContadorPDF + 1 'PDF
'                                NombreF = Trim(CarpetaParaPDF) + Trim(Dato3) + "_" + Format(ContadorPDF, "0000") + ".ps" 'PDF
'                            Loop 'PDF
'                            '''''''''''''''''''Printer.Print 'PDF
'                        End If 'PDF
'                    End If 'PDF
'                    DocumentoAnterior = DocumentoLeido 'PDF
'                    If Left(FormatoLeido, 3) = "000" Then
'                        ImprimeZona("cabecera", 0, DocumentoLeido, NPagina, nCopia)
'                        INAlturaActual = INAlturaActual + INAlturaCab0
'                    ElseIf Left(FormatoLeido, 3) = "001" Then
'                        ImprimeZona("cabecera", 1, DocumentoLeido, NPagina, nCopia)
'                        INAlturaActual = INAlturaActual + INAlturaCab1
'                    ElseIf Left(FormatoLeido, 1) = "1" Then
'                        TipoLineaLeido = CInt(Mid(FormatoLeido, 2, 2))
'                        ImprimeCuerpo(DocumentoLeido, TipoLineaLeido, RegistroLeido, nCopia)
'                        INAlturaActual = INAlturaActual + INAlturaCuerpo(TipoLineaLeido)
'                    ElseIf Left(FormatoLeido, 3) = "200" Then
'                        INAlturaActual = INAltoPaginaReal - INAlturaPie1
'                        ImprimeZona("pie", 1, DocumentoLeido, NPagina, nCopia)
'                        INAlturaActual = INAlturaActual + INAlturaPie1
'                    ElseIf Left(FormatoLeido, 3) = "201" Then
'                        INAlturaActual = INAltoPaginaReal - INAlturaPie0
'                        ImprimeZona("pie", 0, DocumentoLeido, NPagina, nCopia)
'                        INAlturaActual = INAlturaActual + INAlturaPie0
'                    End If
'                    'NUEVO            End If
'                Next
'                If Not INPrevisualizacion Then Printer.NewPage
'            Next
'            If INPrevisualizacion Then
'                FrmPresFormato.TxtNPagina.Text = CStr(DesdePagina)
'                FrmPresFormato.TxtTotalPaginas.Text = CStr(INTotalPaginas)
'                On Error Resume Next
'                FrmPresFormato.ShowModal
'                On Error GoTo 0
'            Else
'                Printer.EndDoc
'            End If
'        Next
'        If HayImpresionPDF = True Then
'            Printer.EndDoc
'            For Each OBJPrinter In Printers 'PDF
'                If Trim(LCase(OBJPrinter.DeviceName)) = Trim(LCase(ImpresoraAnterior)) Then 'PDF
'            Set Printer = OBJPrinter 'PDF
'            Exit For 'PDF
'                End If 'PDF
'            Next 'PDF
'        End If 'PDF
'        'MsgBox("Imprime paginas fin" 'NUEVO 21/12
'    End Sub
'    Private Sub ImprimeZona(Zona As String, IndiceZona As Integer, Documento As Long, Pagina As Long, NumeroDeCopia As Integer)
'        Dim kk As Integer, i As Long
'        Dim objPrinter_Preliminar As Object
'        Dim Contenido As String, Longitud As Integer, Acotarh As Boolean
'        Dim AlturaProvisional As Integer, AlturaDefinitiva As Integer
'        Dim TipoImagen As String, CodigoImagen As String

'        kk = IIf(Zona = "cabecera", 0, 2)
'        If INPrevisualizacion = True Then
'    Set objPrinter_Preliminar = FrmPresFormato.picSeccion
'Else
'    Set objPrinter_Preliminar = Printer
'    objPrinter_Preliminar.ScaleMode = 1  'Twips
'        End If

'        Dim foundRows As MiDatatable = New MiDatatable

'        foundRows = INRsFormato.cnDataSet.Tables(0)
'        Dim DsFiltrada As Data.DataTable

'        DsFiltrada = foundRows.Filter("zona='" & Zona & "' and indice=" & IndiceZona & " and tipo<>'banda' and copia" & Format(NumeroDeCopia, "00") & "='S'", "tope, izquierda")

'        With objPrinter_Preliminar
'            For i = 1 To DsFiltrada.Rows.Count
'                Select Case Trim(INRsFormato!tipo)
'                    Case "etiqueta"
'                        'Debug.Print DsFiltrada.Rows(i)("Valor")
'                        .CurrentX = DsFiltrada.Rows(i)("izquierda")
'                        .CurrentY = INAlturaActual + DsFiltrada.Rows(i)("tope")
'                        .FontName = Trim(DsFiltrada.Rows(i)("fuente"))
'                        .FontBold = Trim(DsFiltrada.Rows(i)("negrita"))
'                        .FontItalic = Trim(DsFiltrada.Rows(i)("cursiva"))
'                        .FontUnderline = INRsFormato!subrayado
'                        .FontSize = Val(DsFiltrada.Rows(i)("tamanof"))
'                        If Trim(DsFiltrada.Rows(i)("Valor")) = "Nº de Página" Then
'                            Contenido = CStr(Pagina)
'                        ElseIf Trim(DsFiltrada.Rows(i)("Valor")) = "Total Páginas" Then
'                            Contenido = INTotalPaginas
'                        Else
'                            Contenido = Trim("" & DsFiltrada.Rows(i)("Valor"))
'                        End If
'                        Longitud = .TextWidth("" & DsFiltrada.Rows(i)("Contenido"))
'                        If Not IsNothing(DsFiltrada.Rows(i)("Justificado")) Then
'                            Select Case DsFiltrada.Rows(i)("Justificado")
'                                Case "I"

'                                Case "D"
'                                    .CurrentX = DsFiltrada.Rows(i)("izquierda") + DsFiltrada.Rows(i)("Ancho") - Longitud
'                                Case "C"
'                                    .CurrentX = (DsFiltrada.Rows(i)("izquierda") + (DsFiltrada.Rows(i)("Ancho") - Longitud) / 2)
'                            End Select
'                        End If
'                        Acotarh = (UCase("" & DsFiltrada.Rows(i)("Acotarh") = "S") Or (IsNothing(DsFiltrada.Rows(i)("Acotarh")))
'                            objPrinter_Preliminar.Print TrozoTexto(RTrim("" & Contenido), DsFiltrada.Rows(i)("Ancho"), objPrinter_Preliminar, Acotarh)
'    Case "campo"
'                        INRsDatos(kk).Filter = "codigo_impresion =" & CStr(CodigoImpresion) & " and num_documento=" & Documento & " and zona=" & kk & " and indice=" & IndiceZona & " and num_registro = 0 and num_dato=" & INRsFormato!num_dato & " and num_linea = 0"
'                        If INRsDatos(kk).RecordCount > 0 Then
'                            If Left(DsFiltrada.Rows(i)("Valor"), 14) <> "calculado.cb39" And Left(DsFiltrada.Rows(i)("Valor"), 16) <> "calculado.imagen" Then
'                                .CurrentX = DsFiltrada.Rows(i)("Valor")DsFiltrada.Rows(i)("izquierda")
'                                    .CurrentY = INAlturaActual + DsFiltrada.Rows(i)("tope")
'                                .FontName = Trim(DsFiltrada.Rows(i)("fuente")
'                                    .FontBold = DsFiltrada.Rows(i)("negrita")
'                                .FontItalic = DsFiltrada.Rows(i)("cursiva")
'                                .FontUnderline = DsFiltrada.Rows(i)("subrayado")
'                                .FontSize = Val(DsFiltrada.Rows(i)("tamanof"))
'                                If IsNothing(INRsDatos(IndiceZona)!valor_dato) = False Then
'                                    Contenido = RTrim(INRsDatos(kk)!valor_dato)  ' OJO
'                                    If IsNothing(INRsFormato!Tag) = False Then
'                                        If Trim(Contenido) > "" And IsNumeric(Contenido) = True And Trim(INRsFormato!Tag) > "" Then
'                                            Contenido = Format(Contenido, Trim(INRsFormato!Tag))
'                                        End If
'                                    End If
'                                End If
'                                Longitud = .TextWidth("" & Contenido)
'                                If Not IsNothing(INRsFormato!Justificado) Then
'                                    Select Case INRsFormato!Justificado
'                                        Case "I"
'                            'No hace nada
'                                        Case "D"
'                                            .CurrentX = INRsFormato!izquierda + INRsFormato!Ancho - Longitud
'                                        Case "C"
'                                            .CurrentX = (INRsFormato!izquierda + (INRsFormato!Ancho - Longitud) / 2)
'                                    End Select
'                                End If
'                                Acotarh = (UCase("" & INRsFormato!Acotarh) = "S") Or (IsNothing(INRsFormato!Acotarh))
'                                objPrinter_Preliminar.Print TrozoTexto(RTrim("" & Contenido), INRsFormato!Ancho, objPrinter_Preliminar, Acotarh) 'OJO
'                            ElseIf Left(INRsFormato!Valor, 16) = "calculado.imagen" Then
'                                'InicializarIMAGEN objPrinter_Preliminar
'                                If Dir(Trim(INRsDatos(IndiceZona)!valor_dato)) <> "" Then
'                                    If Not IsNothing(INRsFormato!alto) And Not IsNothing(INRsFormato!Ancho) Then
'                                        objPrinter_Preliminar.PaintPicture LoadPicture(Trim(INRsDatos(IndiceZona)!valor_dato)), INRsFormato!izquierda, INAlturaActual + INRsFormato!tope, INRsFormato!Ancho, INRsFormato!alto
'                   Else
'                                        objPrinter_Preliminar.PaintPicture LoadPicture(Trim(INRsDatos(IndiceZona)!valor_dato)), INRsFormato!izquierda, INAlturaActual + INRsFormato!tope, 0, 0
'                   End If
'                                End If
'                            Else
'                                If Formato39(1) = "" Then InicializarCodigoDeBarras39()
'                                .CurrentX = INRsFormato!izquierda
'                                .CurrentY = INAlturaActual + INRsFormato!tope
'                                .FontSize = Val(INRsFormato!tamanof)
'                                DibujaCodigo39 RTrim(INRsDatos(kk)!valor_dato), objPrinter_Preliminar, .CurrentX, .CurrentY, INRsFormato!alto, .FontSize
'            End If
'                        End If
'                    Case "dibujo"
'                        InicializarIMAGEN objPrinter_Preliminar
'        If IsNothing(INRsFormato!tipo_imagen) = False And IsNothing(INRsFormato!Codigo_imagen) = False Then
'                            If Trim(INRsFormato!tipo_imagen) > "" And Trim(INRsFormato!Codigo_imagen) > "" Then
'                                If Trim(INRsFormato!Acotarh) = "S" And Not IsNothing(INRsFormato!alto) And Not IsNothing(INRsFormato!Ancho) Then
'                                    CImagen.PintaImagen INRsFormato!tipo_imagen, INRsFormato!Codigo_imagen, INRsFormato!izquierda, INAlturaActual + INRsFormato!tope, INRsFormato!Ancho, INRsFormato!alto
'                Else
'                                    CImagen.PintaImagen INRsFormato!tipo_imagen, INRsFormato!Codigo_imagen, INRsFormato!izquierda, INAlturaActual + INRsFormato!tope, 0, 0
'                End If
'                            End If
'                        End If
'                    Case "linea"
'                        objPrinter_Preliminar.DrawStyle = (INRsFormato!estilolinea) - 1
'                        If (INRsFormato!grosorlinea) < 1 Then
'                            objPrinter_Preliminar.DrawWidth = 1
'                        Else
'                            objPrinter_Preliminar.DrawWidth = (INRsFormato!grosorlinea)
'                        End If
'                        objPrinter_Preliminar.Line(INRsFormato!izquierda, INAlturaActual + INRsFormato!Ancho)-(INRsFormato!tope, INAlturaActual + INRsFormato!alto)
'    End Select
'                INRsFormato.MoveNext()
'            Next
'        End With
'    End Sub
'    Private Sub ImprimeCuerpo(Documento As Long, TipoLinea As Integer, NumRegistro As Long, NumeroDeCopia As Integer)
'        Dim i As Long, j As Long
'        Dim Contenido As String, Longitud As Integer, Acotarh As Boolean
'        Dim AlturaProvisional As Integer, AlturaDefinitiva As Integer
'        Dim objPrinter_Preliminar As Object

'        If INPrevisualizacion = True Then
'    Set objPrinter_Preliminar = FrmPresFormato.picSeccion
'Else
'    Set objPrinter_Preliminar = Printer
'End If
'        INRsFormato.Filter = "zona='cuerpo' and indice=" & TipoLinea & " and tipo<>'banda' and copia" & Format(NumeroDeCopia, "00") & "='S'"
'        With objPrinter_Preliminar
'            For i = 0 To INRsFormato.RecordCount - 1
'                Select Case Trim(INRsFormato!tipo)
'                    Case "campo"
'                        .CurrentX = INRsFormato!izquierda
'                        .CurrentY = (INAlturaActual + INRsFormato!tope)
'                        .FontName = Trim(INRsFormato!fuente)
'                        .FontBold = INRsFormato!negrita
'                        .FontItalic = INRsFormato!cursiva
'                        .FontUnderline = INRsFormato!subrayado
'                        .FontSize = Val(INRsFormato!tamanof)
'                        Contenido = ""
'                        INRsDatos(1).Filter = "codigo_impresion = " & CStr(CodigoImpresion) & " and num_documento=" & Documento & " and zona=1 and indice=" & TipoLinea & " and num_registro = " & NumRegistro & " and num_dato=" & INRsFormato!num_dato & " and num_linea = 0"
'                        If INRsDatos(1).RecordCount > 0 Then
'                            If IsNothing(INRsDatos(1)!valor_dato) = False Then
'                                Contenido = RTrim(INRsDatos(1)!valor_dato)  ' OJO
'                                If IsNothing(INRsFormato!Tag) = False Then
'                                    If Trim(Contenido) > "" And IsNumeric(Contenido) = True And Trim(INRsFormato!Tag) > "" Then
'                                        Contenido = Format(Contenido, Trim(INRsFormato!Tag))
'                                    End If
'                                End If
'                            End If
'                        End If
'                        Longitud = .TextWidth("" & Contenido)
'                        If Not IsNothing(INRsFormato!Justificado) Then
'                            Select Case INRsFormato!Justificado
'                                Case "I" ' Nada
'                                Case "D"
'                                    .CurrentX = (INRsFormato!izquierda + INRsFormato!Ancho - Longitud)
'                                Case "C"
'                                    .CurrentX = (INRsFormato!izquierda + (INRsFormato!Ancho - Longitud) / 2)
'                            End Select
'                        End If
'                        Acotarh = (UCase("" & INRsFormato!Acotarh) = "S") Or (IsNothing(INRsFormato!Acotarh))
'                        objPrinter_Preliminar.Print TrozoTexto(RTrim("" & Contenido), INRsFormato!Ancho, objPrinter_Preliminar, Acotarh) 'OJO
'                    Case "etiqueta"
'                        .CurrentX = INRsFormato!izquierda
'                        .CurrentY = (INAlturaActual + INRsFormato!tope)
'                        .FontName = Trim(INRsFormato!fuente)
'                        .FontBold = INRsFormato!negrita
'                        .FontItalic = INRsFormato!cursiva
'                        .FontUnderline = INRsFormato!subrayado
'                        .FontSize = Val(INRsFormato!tamanof)
'                        Contenido = RTrim(INRsFormato!Valor)
'                        Longitud = .TextWidth("" & Contenido)
'                        If Not IsNothing(INRsFormato!Justificado) Then
'                            Select Case INRsFormato!Justificado
'                                Case "I"
'                    'No hacemos nada
'                                Case "D"
'                                    .CurrentX = INRsFormato!izquierda + INRsFormato!Ancho - Longitud
'                                Case "C"
'                                    .CurrentX = (INRsFormato!izquierda + (INRsFormato!Ancho - Longitud) / 2)
'                            End Select
'                        End If
'                        Acotarh = (UCase("" & INRsFormato!Acotarh) = "S") Or (IsNothing(INRsFormato!Acotarh))
'                        objPrinter_Preliminar.Print TrozoTexto(RTrim("" & Contenido), INRsFormato!Ancho, objPrinter_Preliminar, Acotarh) 'OJO
'                    Case "dibujo"
'                        InicializarIMAGEN objPrinter_Preliminar
'        If IsNothing(INRsFormato!tipo_imagen) = False And IsNothing(INRsFormato!Codigo_imagen) = False Then
'                            If Trim(INRsFormato!tipo_imagen) > "" And Trim(INRsFormato!Codigo_imagen) > "" Then
'                                If Trim(INRsFormato!Acotarh) = "S" And Not IsNothing(INRsFormato!alto) And Not IsNothing(INRsFormato!Ancho) Then
'                                    CImagen.PintaImagen INRsFormato!tipo_imagen, INRsFormato!Codigo_imagen, INRsFormato!izquierda, INAlturaActual + INRsFormato!tope, INRsFormato!Ancho, INRsFormato!alto
'                Else
'                                    CImagen.PintaImagen INRsFormato!tipo_imagen, INRsFormato!Codigo_imagen, INRsFormato!izquierda, INAlturaActual + INRsFormato!tope, 0, 0
'                End If
'                            End If
'                        End If
'                    Case "linea"
'                        objPrinter_Preliminar.DrawStyle = (INRsFormato!estilolinea) - 1
'                        If (INRsFormato!grosorlinea) < 1 Then
'                            objPrinter_Preliminar.DrawWidth = 1
'                        Else
'                            objPrinter_Preliminar.DrawWidth = (INRsFormato!grosorlinea)
'                        End If
'                        objPrinter_Preliminar.Line(INRsFormato!izquierda, INAlturaActual + INRsFormato!Ancho)-(INRsFormato!tope, INAlturaActual + INRsFormato!alto)
'    End Select
'                INRsFormato.MoveNext()
'            Next
'        End With
'    End Sub
'    Function TrozoTexto(Cadena As String, Longitud As Single, C As Control, Acotarh As Boolean) As String
'        Dim i As Integer

'        If Acotarh Then
'            For i = Len(Cadena) To 1 Step -1
'                If C.TextWidth(Left(Cadena, i)) < Longitud Then
'                    Exit For
'                End If
'            Next
'            TrozoTexto = Left(Cadena, i)
'        Else
'            TrozoTexto = Cadena
'        End If
'    End Function
'    Public Function ConfiguracionDeImpresora(frmOwner As Form) As Boolean
'        Dim PrintDlg As PRINTDLG_TYPE
'        Dim DevMode As DEVMODE_TYPE
'        Dim DevName As DEVNAMES_TYPE
'        Dim lpDevMode As Long, lpDevName As Long
'        Dim bReturn As Integer
'        Dim OBJPrinter As Printer, NewPrinterName As String
'        Dim strSetting As String
'        Dim PrintFlags As Long

'        PrintFlags = PD_PRINTSETUP
'        PrintDlg.lStructSize = Len(PrintDlg)
'        PrintDlg.hwndOwner = frmOwner.hWnd
'        PrintDlg.Flags = PrintFlags
'        DevMode.dmDeviceName = Printer.DeviceName
'        DevMode.dmSize = Len(DevMode)

'        If INIntercalarPaginas Then
'            DevMode.dmCollate = 1
'        Else
'            DevMode.dmCollate = 0
'        End If
'        DevMode.dmOrientation = Printer.Orientation
'        DevMode.dmCopies = Printer.Copies
'        On Error Resume Next
'        DevMode.dmDuplex = Printer.Duplex
'        On Error GoTo 0

'        PrintDlg.hDevMode = GlobalAlloc(GMEM_MOVEABLE Or GMEM_ZEROINIT, Len(DevMode))
'        lpDevMode = GlobalLock(PrintDlg.hDevMode)
'        If lpDevMode > 0 Then
'            CopyMemory ByVal lpDevMode, DevMode, Len(DevMode)
'    bReturn = GlobalUnlock(PrintDlg.hDevMode)
'        End If
'        With DevName
'            .wDriverOffset = 8
'            .wDeviceOffset = .wDriverOffset + 1 + Len(Printer.DriverName)
'            .wOutputOffset = .wDeviceOffset + 1 + Len(Printer.Port)
'            .wDefault = 0
'        End With
'        With Printer
'            DevName.extra = .DriverName & Chr(0) & .DeviceName & Chr(0) & .Port & Chr(0)
'        End With
'        PrintDlg.hDevNames = GlobalAlloc(GMEM_MOVEABLE Or GMEM_ZEROINIT, Len(DevName))
'        lpDevName = GlobalLock(PrintDlg.hDevNames)
'        If lpDevName > 0 Then
'            CopyMemory ByVal lpDevName, DevName, Len(DevName)
'    bReturn = GlobalUnlock(lpDevName)
'        End If
'        If PrintDialog(PrintDlg) Then
'            lpDevName = GlobalLock(PrintDlg.hDevNames)
'            CopyMemory DevName, ByVal lpDevName, 45
'    bReturn = GlobalUnlock(lpDevName)
'            GlobalFree PrintDlg.hDevNames
'    lpDevMode = GlobalLock(PrintDlg.hDevMode)
'            CopyMemory DevMode, ByVal lpDevMode, Len(DevMode)
'    bReturn = GlobalUnlock(PrintDlg.hDevMode)
'            GlobalFree PrintDlg.hDevMode
'    NewPrinterName = UCase$(Left(DevMode.dmDeviceName, InStr(DevMode.dmDeviceName, Chr$(0)) - 1))
'            If Printer.DeviceName <> NewPrinterName Then
'                For Each OBJPrinter In Printers
'                    If UCase$(OBJPrinter.DeviceName) = NewPrinterName Then
'                Set Printer = OBJPrinter
'           End If
'                Next
'            End If
'            On Error Resume Next
'            DoEvents
'            With Printer
'                .Copies = DevMode.dmCopies
'                .Duplex = DevMode.dmDuplex
'                .Orientation = DevMode.dmOrientation
'            End With
'            On Error GoTo 0
'            ConfiguracionDeImpresora = True
'        Else
'            ConfiguracionDeImpresora = False
'        End If
'    End Function
'    Private Sub SurroundingSub()
'        Dim foundRows As DataRow()
'        foundRows = dataset.Tables(0).Select("ASESOR = '" & asesor.Text & "' AND MES = " & Convert.ToString("junio") & " ")
'        For Each row As DataRow In foundRows
'            'aqui se recorre el filtro y el valor se optiene: row["nombrecolumna"]
'        Next
'    End Sub

'End Class

