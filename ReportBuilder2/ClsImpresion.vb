Public Class ClsImpresion
    Public CINFImpresora As String
    Public CINFApaisado As String
    Public INModoSeleccionImpresora As Integer
    Public INPrevisualizacion As Boolean
    Public INImpresoraSistema As String
    Public printset As New Printing.PrinterSettings
    Public rsc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Public rsd As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public oRpt As Report
    Public CodigoInforme As Long
    Public NumeroObjeto As Long = 0
    Public PaginasInforme As Integer = 0
    Public GrupoDetalle As Integer = 2
    Dim documento_f As String
    Dim informe_f As String
    Dim proceso_f As String
    'Dim ConsScrips As


    Public Sub New(ByRef og As ObjetoGlobal.ObjetoGlobal)
        ObjetoGlobal = og
    End Sub

    Public Sub EstadObjetoImpresionesion_Inicializar(Texto As String, ContadorParcial As Long, ContadorTotal As Long)
        '        'FrmEstadoImpresionNuevo.Show vbModal
        '        'FrmEstadoImpresionNuevo.lblTextObjetoImpresionesion = Trim(Texto)
        '        'FrmEstadoImpresionNuevo.pbrCalculo = ContadorParcial
        '        'FrmEstadoImpresionNuevo.pbrTotal = ContadorTotal
    End Sub
    Public Function Inicializar(ByVal pProceso As String, ByVal pDocumento As String, ByVal pFormato As String, ByVal pModoSeleccionImpresora As Integer, ByVal pPrevisualizar As Boolean, ByVal cnn As Data.SqlClient.SqlConnection, ByRef XProceso As String, ByRef XDocumento As String, ByRef XFormato As String, ByVal log As String, ByVal pImpresoraSistema As String, Optional ByVal pEmpresa As String = "1", Optional ByVal ImpresoraFija As String = "") As Boolean
        Dim aRet(1) As String

        '        pModoSeleccionImpresora
        '        ' 0 = Impresora en zz formatosdocumento + ImpresoraPredeterminada sin preguntar
        '        ' 1 = Siempre pregunta
        '        ' 2 = ImpresoraPorDefecto (solamente)
        '        'MsgBox("Inicializar (comienzo):" + CStr(pModoSeleccionImpresora) + "|" + Trim(pImpresoraSistema) + "\" + Trim(INImpresoraSistema)
        '        Inicializar = False

        INModoSeleccionImpresora = pModoSeleccionImpresora
        INPrevisualizacion = pPrevisualizar

        If Trim(pImpresoraSistema) = "" Then
            INImpresoraSistema = pImpresoraSistema
        Else
            INImpresoraSistema = printset.PrinterName
        End If

        If Trim(ImpresoraFija) <> "" Then
            INImpresoraSistema = ImpresoraFija
        End If
        Inicializar = True

        If rsc.Open("SELECT max(codigo_impresion) AS cod FROM zzz_documentosnv", ObjetoGlobal.cn) Then
            If Not rsc.EOF Then
                If Not IsDBNull(rsc!cod) Then
                    CodigoInforme = rsc!cod + 1
                Else
                    CodigoInforme = 1
                End If
            End If
        Else
            MsgBox("No se puede abrir la tabla de informes")
            Return False
        End If
        rsc.Close()

        If Not rsd.Open("SELECT * FROM zzz_documentos_detnv WHERE  1=0", ObjetoGlobal.cn, True) Then
            MsgBox("No se puede abrir la tabla de informes")
            rsd.Close()
            Return False
        End If

        proceso_f = pProceso.Trim
        documento_f = pDocumento.Trim
        informe_f = pFormato.Trim

        If rsc.Open("SELECT * FROM zzz_documentosnv WHERE  1=0", ObjetoGlobal.cn, True) Then
            rsc.AddNew()
            rsc!codigo_impresion = CodigoInforme
            rsc!num_documento = 1
            rsc!proceso = pProceso.Trim
            rsc!documento = pDocumento.Trim
            rsc!formato = pFormato.Trim
            rsc!impresora = INImpresoraSistema
            rsc!apaisado = False
            rsc.Update()
        End If

        aRet(0) = pDocumento
        aRet(1) = pFormato
        Module1.ObjetoGlobal = ObjetoGlobal
        oRpt = Module1.CargarInforme(aRet)

        'rsc.Close()
        'rsd.Close()
    End Function
    Public Sub VolcarAImpresion(NumeroDocumento As Long, Zona As Long, Indice As Long, Registro As Long, Campo As String, Linea As Long, Valor As String, Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
        Dim Datos(3) As Long
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim ThisMoment As Date = Now
        Dim LiteralZona As String
        Dim k1 As Integer
        Dim k2 As Integer
        Dim i As Integer
        Dim sql As String

        sql = "SELECT * FROM zzz_documentosnv WHERE CODIGO_IMPRESION = " + CStr(CodigoInforme) + " AND NUM_DOCUMENTO = " + CStr(NumeroDocumento)
        If Rs.Open(sql, ObjetoGlobal.cn, True) Then
            If Rs.EOF Then
                Rs.AddNew()
                Rs!codigo_impresion = CodigoInforme
                Rs!num_documento = NumeroDocumento
                Rs!proceso = proceso_f
                Rs!documento = documento_f
                Rs!formato = informe_f
                Rs!impresora = INImpresoraSistema
                Rs!apaisado = False
                Rs.Update()
            End If
            Rs.Close()
        End If

        If Zona = 0 Then
            LiteralZona = "cabecera"
            If Indice > 1 Then
                MsgBox("Los índices de cabecera en la impresión pueden ser 0 o 1." + vbCrLf + "Se ignorará el índice " + CStr(Indice) + " en el dato:'" + Trim(Campo) + "'")
                Exit Sub
            ElseIf Indice < 0 Then
                k1 = 0 : k2 = 1
            Else
                k1 = Indice : k2 = Indice
            End If
        ElseIf Zona = 1 Then
            LiteralZona = "cuerpo"
            If Indice > 10 Then
                MsgBox("Los formatos de cuerpo en la impresion (indice) pueden ser de 0 a 10." + vbCrLf + "Se ignorará el formato " + CStr(Indice) + " en el dato:'" + Trim(Campo) + "'")
                Exit Sub
            ElseIf Indice <= 0 Then
                k1 = 0 : k2 = 0
            Else
                k1 = Indice : k2 = Indice
            End If
        Else
            LiteralZona = "pie"
            If Indice > 1 Then
                MsgBox("Los índices de pie en la impresión pueden ser 0 o 1." + vbCrLf + "Se ignorará el índice " + CStr(Indice) + " en el dato:'" + Trim(Campo) + "'")
                Exit Sub
            ElseIf Indice < 0 Then
                k1 = 0 : k2 = 1
            Else
                k1 = Indice : k2 = Indice
            End If
        End If

        For i = k1 To k2
            NumeroObjeto = NumeroObjeto + 1
            rsd.AddNew()
            rsd!codigo_impresion = CodigoInforme
            rsd!pagina_documento = NumeroDocumento
            rsd!zona = Zona
            rsd!indice = i
            rsd!num_registro = NumeroObjeto
            rsd!cod_variable = Campo
            rsd!num_linea = Linea
            rsd!valor_dato = Valor
            rsd!literalzona = LiteralZona & i
            rsd.Update()
        Next

        'PaginasInforme = If(NumeroDocumento > PaginasInforme, NumeroDocumento, PaginasInforme)

    End Sub

    Public Sub VolcarImagenAImpresion(NumeroDocumento As Long, Zona As Long, Indice As Long, Registro As Long, Campo As String, Linea As Long, Valor As String, Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
        'Public Sub VolcarImagenAImpresion(NumeroDocumento As Long, Zona As Long, Indice As Long, Registro As Long, Campo As String, Linea As Long, Valor As Image, Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
        Dim Datos(3) As Long
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim ThisMoment As Date = Now
        Dim LiteralZona As String
        Dim k1 As Integer
        Dim k2 As Integer
        Dim i As Integer
        Dim sql As String


        sql = "SELECT * FROM zzz_documentosnv WHERE CODIGO_IMPRESION = " + CStr(CodigoInforme) + " AND NUM_DOCUMENTO = " + CStr(NumeroDocumento)
        If Rs.Open(sql, ObjetoGlobal.cn, True) Then
            If Rs.EOF Then
                Rs.AddNew()
                Rs!codigo_impresion = CodigoInforme
                Rs!num_documento = NumeroDocumento
                Rs!proceso = proceso_f
                Rs!documento = documento_f
                Rs!formato = informe_f
                Rs!impresora = INImpresoraSistema
                Rs!apaisado = False
                Rs.Update()
            End If
            Rs.Close()
        End If

        If Zona = 0 Then
            LiteralZona = "cabecera"
            If Indice > 1 Then
                MsgBox("Los índices de cabecera en la impresión pueden ser 0 o 1." + vbCrLf + "Se ignorará el índice " + CStr(Indice) + " en el dato:'" + Trim(Campo) + "'")
                Exit Sub
            ElseIf Indice < 0 Then
                k1 = 0 : k2 = 1
            Else
                k1 = Indice : k2 = Indice
            End If
        ElseIf Zona = 1 Then
            LiteralZona = "cuerpo"
            If Indice > 10 Then
                MsgBox("Los formatos de cuerpo en la impresion (indice) pueden ser de 0 a 10." + vbCrLf + "Se ignorará el formato " + CStr(Indice) + " en el dato:'" + Trim(Campo) + "'")
                Exit Sub
            ElseIf Indice <= 0 Then
                k1 = 0 : k2 = 0
            Else
                k1 = Indice : k2 = Indice
            End If
        Else
            LiteralZona = "pie"
            If Indice > 1 Then
                MsgBox("Los índices de pie en la impresión pueden ser 0 o 1." + vbCrLf + "Se ignorará el índice " + CStr(Indice) + " en el dato:'" + Trim(Campo) + "'")
                Exit Sub
            ElseIf Indice < 0 Then
                k1 = 0 : k2 = 1
            Else
                k1 = Indice : k2 = Indice
            End If
        End If

        For i = k1 To k2
            NumeroObjeto = NumeroObjeto + 1
            rsd.AddNew()
            rsd!codigo_impresion = CodigoInforme
            rsd!pagina_documento = NumeroDocumento
            rsd!zona = Zona
            rsd!indice = i
            rsd!num_registro = NumeroObjeto
            rsd!cod_variable = Campo
            rsd!num_linea = Linea
            rsd!valor_dato = "imagen"
            rsd!literalzona = LiteralZona & i
            rsd!path = Valor.Trim
            rsd.Update()
        Next

        PaginasInforme = If(NumeroDocumento > PaginasInforme, NumeroDocumento, PaginasInforme)

    End Sub

    Public Sub VolcarNumeroDocumento(NumeroDocumento As Long, NombreFicheroPDF As String)
        '        Dim Clave As String, Dato As String

        '        Clave = Format(NumeroDocumento, "0000000000")
        '        If HayImpresionPDF = True Then
        '            If INDiccionarioDocumentos.ContainsKey(Clave) Then
        '                Dato = Trim(NombreFicheroPDF)
        '                INDiccionarioDocumentos.Item(Clave) = Dato
        '            Else
        '                'MsgBox("No se puede grabar el fichero PDF asociado al documento:" + Trim(NombreFicheroPDF)
        '            End If
        '        End If
    End Sub
    Public Sub Previsualizar()

        Using p As New PrintDesign(oRpt)
            p.GrupoDetalle = GrupoDetalle
            p.PrintReportDocument(CodigoInforme, PaginasInforme, True)
        End Using
        rsc.Close()
        rsd.Close()

    End Sub
    Public Sub Imprimir()

        Using p As New PrintDesign(oRpt)
            p.GrupoDetalle = GrupoDetalle
            p.PrintReportDocument(CodigoInforme, PaginasInforme, False)
        End Using
        rsc.Close()
        rsd.Close()

    End Sub
    Public Property FuerzaCopias() As Integer
        Get
            Return PaginasInforme
        End Get
        Set(ByVal Value As Integer)
            PaginasInforme = Value
        End Set
    End Property


End Class
