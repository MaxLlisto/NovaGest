﻿Public Class FrmGrabacionCostes
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim FechaProceso As Date
    Dim Proceso As Integer
    Dim ContadorCRT As Integer
    Dim Industria(12, 1) As Double
    Dim Precalibrado(12, 1) As Double
    Dim Costes(60) As Double
    Dim Kilos(25) As Double
    Dim ContadorGeneral As Long
    Dim Familia As String
    Dim VariedadGeneral As String
    Dim Confecciones(1, 0) As String
    Dim CuantasConfecciones As Integer


    Private Sub GrabacionCostes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Clave As String, Fecha As Date

        Clave = InputBox("Introduzca clave de acceso:")
        If LCase(Trim(Clave)) <> "vercostes" Then
            Me.Close()
        End If
        LeerPesosTeoricos()
        Fecha = DateAdd("d", -1, Now)
        DTFecha.Value = Fecha

        InicializarControles()


    End Sub

    '    Dim CINF As Object
    '    Dim XProceso As String
    '    Dim XDocumento As String
    '    Dim XFormato As String
    '    Dim XDiccionario As Dictionary

    '    Dim Proceso As Long
    '    Dim Dias() As Date
    '    Dim VariedadesDia() As String
    '    Dim CuantosDias As Integer
    '    Dim CuantasColumnas As Long
    '    Dim NombreColumna() As String
    '    Dim CuantasFilas As Long
    '    Dim NombreFila() As String

    Dim PesoIndustriaClementinaPequeno As Long
    Dim PesoIndustriaNaranjaPequeno As Long
    Dim PesoIndustriaClementinaGrande As Long
    Dim PesoIndustriaNaranjaGrande As Long
    Dim PesoIndustriaSandia As Long
    Dim PesoVolcadoClementinaPalet As Long
    Dim PesoVolcadoNaranjaPalet As Long
    Dim PesoVolcadoClementinaPalot As Long
    Dim PesoVolcadoNaranjaPalot As Long
    Dim PesoCajaNaranja As Long
    Dim PesoCajaClementina As Long
    Dim PesoCajaNaranjaPrecalibre As Long
    Dim PesoCajaClementinaPrecalibre As Long
    Dim PesoVolcadoPalotOtros

    Private Sub CmdGrabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        Dim FlagGrabarDetalle As Boolean

        FechaProceso = CDate(Format(DTFecha.Value, "dd/MM/yyyy"))
        InicializarControles()

        'Procesos generales
        Chk01.Checked = True
        Proceso = ObtenerNumeroProceso()
        If Proceso <= 0 Then
            MsgBox("No es posible obtener en contador para el nuevo proceso")
            Exit Sub
        End If
        GrabarAuxiliar(Proceso, FechaProceso, "Previo")
        BorrarDatosProceso()

        'Palets confeccionados
        Lbl01.Visible = False
        Lbl02.Text = ""
        Lbl02.Visible = True
        Chk02.Checked = True
        ContadorCRT = 0

        GrabarAuxiliar(Proceso, FechaProceso, "GrabarPaletsConfeccionados")
        If GrabarPaletsConfeccionados() = False Then Exit Sub

        GrabarAuxiliar(Proceso, FechaProceso, "GrabarPaletsIndustria")
        If GrabarPaletsIndustria() = False Then Exit Sub

        GrabarAuxiliar(Proceso, FechaProceso, "GrabarPaletsPrecalibrado")
        If GrabarPaletsPrecalibrado() = False Then Exit Sub

        'Analisis volcado
        Chk03.Checked = True
        Lbl01.Visible = False
        Lbl02.Visible = False
        Lbl03.Text = ""
        Lbl03.Visible = True
        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "AnalisisVolcado")
        If AnalisisVolcado() = False Then Exit Sub

        'Costes Generales
        Chk04.Checked = True
        Lbl03.Visible = False
        Lbl04.Text = ""
        Lbl04.Visible = True
        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "CostesGenerales")
        If CostesGenerales() = False Then Exit Sub

        'Costes Volcado
        Chk05.Checked = True
        Lbl04.Visible = False
        Lbl05.Text = ""
        Lbl05.Visible = True

        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "CostesVolcado")
        If CostesVolcado() = False Then Exit Sub
        GrabarAuxiliar(Proceso, FechaProceso, "CostesIndustria")
        If CostesIndustria() = False Then Exit Sub
        GrabarAuxiliar(Proceso, FechaProceso, "CostesPrecalibrado")
        If CostesPrecalibrado() = False Then Exit Sub

        'Costes Paletizado
        Chk06.Checked = True
        Lbl05.Visible = False
        Lbl06.Text = ""
        Lbl06.Visible = True
        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "CostesPaletizado")
        If CostesPaletizado() = False Then Exit Sub

        'Costes Especiales
        Chk07.Checked = True
        Lbl06.Visible = False
        Lbl07.Text = ""
        Lbl07.Visible = True

        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "CostesEspeciales")
        If CostesEspeciales() = False Then Exit Sub

        'Reparto Volcado
        Chk08.Checked = True
        Lbl07.Visible = False
        Lbl08.Text = ""
        Lbl08.Visible = True
        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "RepartoVolcado")
        If RepartoVolcado() = False Then Exit Sub
        GrabarAuxiliar(Proceso, FechaProceso, "RepartoCostesGenerales")
        If RepartoCostesGenerales() = False Then Exit Sub

        'Reparto Paletizado
        Chk09.Checked = True
        Lbl08.Visible = False
        Lbl09.Text = ""
        Lbl09.Visible = True
        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "RepartoCostesPaletizado")
        If RepartoCostesPaletizado() = False Then Exit Sub

        'Grabación de costes palet
        Chk10.Checked = True
        Lbl09.Visible = False
        Lbl10.Text = ""
        Lbl10.Visible = True
        ContadorCRT = 0
        ContadorGeneral = 0
        GrabarAuxiliar(Proceso, FechaProceso, "IncrementoCostesPrecalibrado")
        If IncrementoCostesPrecalibrado() = False Then Exit Sub

        GrabarAuxiliar(Proceso, FechaProceso, "GrabacionCostesPalet")
        If GrabacionCostesPalet() = False Then Exit Sub
        Chk11.Checked = True
        Lbl10.Visible = False
        Lbl11.Text = ""
        Lbl11.Visible = True
        Lbl12.Text = ""
        Lbl12.Visible = True
        FlagGrabarDetalle = False
        ContadorCRT = 0
        GrabarAuxiliar(Proceso, FechaProceso, "LAZO HISTÓRICO")
        For i = 1 To 6
            CuantasConfecciones = -1
            ReDim Confecciones(1, 0)
            If i = 1 Then
                Familia = "NAR"
                VariedadGeneral = "022"
            ElseIf i = 2 Then
                Familia = "CLE"
                VariedadGeneral = "012"
            ElseIf i = 3 Then
                Familia = "HIB"
                VariedadGeneral = "017"
            ElseIf i = 4 Then
                Familia = "SAT"
                VariedadGeneral = "014"
            ElseIf i = 5 Then
                Familia = "NAB"
                VariedadGeneral = "024"
            Else
                Familia = "ROJ"
                VariedadGeneral = "021"
            End If
            GrabarAuxiliar(Proceso, FechaProceso, "LAZO HISTÓRICO + (" + Trim(Familia) + ")")
            Borraranteriores()

            GrabarAuxiliar(Proceso, FechaProceso, "CalcularCostes")
            If CalcularCostes() = True Then
                GrabarAuxiliar(Proceso, FechaProceso, "GrabarLineasInforme")
                If GrabarLineasInforme() = True Then
                    GrabarAuxiliar(Proceso, FechaProceso, "CalcularLineasInforme")
                    If CalcularLineasInforme() = False Then
                        MsgBox("Se ha cancelado el proceso")
                    End If
                End If
            End If
        Next

        'Grabamos la tabla h_costes_detalle
        If GrabaHistoricoDetalle() = False Then
            MsgBox("Se ha producido un error grabando el detalle de costes.")
        End If

        Lbl11.Text = ""
        Lbl11.Visible = False
        Lbl12.Text = ""
        Lbl12.Visible = False

        GrabarAuxiliar(Proceso, FechaProceso, "FIN:")

        MsgBox("El proceso de grabación ha concluido satisfactoriamente")
    End Sub

    Private Function ObtenerNumeroProceso()
        Dim RsAux As New cnRecordset.CnRecordset, SQL As String

        ObtenerNumeroProceso = -1
        SQL = "SELECT * FROM CONTADORES WHERE EMPRESA = 'T' AND EJERCICIO = 'T' AND NOMBRE = 'PROCESO' AND SERIE = 'T'"
        If RsAux.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla de contadores")
            Exit Function
        End If
        If RsAux.RecordCount = 0 Then
            RsAux.AddNew()
            RsAux!Empresa = "T"
            RsAux!Ejercicio = "T"
            RsAux!Nombre = "PROCESO"
            RsAux!Serie = "T"
            RsAux!Contador = 1
            RsAux.Update()
            Proceso = 1
        Else
            Proceso = IIf(IsDBNull(RsAux!Contador), 0, RsAux!Contador)
            Proceso = Proceso + 1
            RsAux!Contador = Proceso
            RsAux.Update()
        End If
        RsAux.Close()
        ObtenerNumeroProceso = Proceso
    End Function

    Private Sub GrabarAuxiliar(Proceso As Long, DiaProceso As Date, Paso As String)
        Dim Rs As New cnRecordset.CnRecordset, SQL As String, Cadena As String

        SQL = "SELECT * FROM TEMP_COSTESH WHERE 1=0"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes_h")
            Exit Sub
        End If

        Rs.AddNew()
        Rs!Proceso = Proceso
        Rs!dia_proceso = CDate(DiaProceso)
        Rs!Paso = Trim(Paso)
        Cadena = Format(Now, "dd/MM/yyy:mm:ss")
        Rs!fecha_hora = Now
        Rs.Update()
        Rs.Close()
    End Sub

    Private Sub BorrarDatosProceso()
        Dim Cadena As String

        Cadena = "SELECT * FROM TEMP_COSTES1 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES2 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES5 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES6 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

        Cadena = "SELECT * FROM TEMP_COSTES66 WHERE PROCESO =" + CStr(Proceso)
        BorrarDatos(Cadena)

    End Sub

    Private Function GrabarPaletsConfeccionados() As Boolean
        Dim SQL As String, RsPalets As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset
        Dim CuantosPalets As Integer
        Dim Palets(10, 0) As String
        Dim i As Integer, j As Integer
        Dim V() As String, FechaAnterior As Date
        Dim Hora As String, HoraAnterior As String, HoraActual As String

        GrabarPaletsConfeccionados = False
        SQL = "SELECT * FROM TEMP_COSTES1 WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        CuantosPalets = 0

        SQL = "SELECT p.*,v.* FROM palets P JOIN ORDENES_CONFECCION O on o.empresa = p.empresa and o.numero_orden = p.orden_confeccion JOIN VARIEDADES V ON V.EMPRESA = P.EMPRESA AND V.CODIGO_VARIEDAD = O.CODIGO_VARIEDAD WHERE  P.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and P.fecha_confeccion = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and o.codigo_variedad between '01' and '02z' and tipo_fabricacion = 'N' and (p.paletizador is null or p.paletizador<'0') "
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets (paletizador)")
            Exit Function
        End If
        If RsPalets.RecordCount > 0 Then
            MsgBox("No se ha asignado paletizador al palet: " + CStr(RsPalets!codigo_Palet) + "." + vbCrLf + "Se cancela el proceso.")
            Exit Function
        End If
        RsPalets.Close()

        SQL = "SELECT p.*,v.* FROM palets P JOIN ORDENES_CONFECCION O on o.empresa = p.empresa and o.numero_orden = p.orden_confeccion   JOIN VARIEDADES V ON V.EMPRESA = P.EMPRESA AND V.CODIGO_VARIEDAD = O.CODIGO_VARIEDAD WHERE P.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and P.fecha_confeccion = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and o.codigo_variedad between '01' and '02z'  and tipo_fabricacion = 'N' and p.peso_palet = 0 "
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets (peso)")
            Exit Function
        End If
        If RsPalets.RecordCount > 0 Then
            MsgBox("No se ha asignado peso al palet: " + CStr(RsPalets!codigo_Palet) + "." + vbCrLf + "Se cancela el proceso.")
            Exit Function
        End If
        RsPalets.Close()

        SQL = "SELECT p.*,v.*,o.codigo_confeccion FROM palets P JOIN ORDENES_CONFECCION O on o.empresa = p.empresa and o.numero_orden = p.orden_confeccion  JOIN VARIEDADES V ON V.EMPRESA = P.EMPRESA AND V.CODIGO_VARIEDAD = O.CODIGO_VARIEDAD WHERE  P.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and P.fecha_confeccion = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and o.codigo_variedad between '01' and '02z'  and tipo_fabricacion = 'N' ORDER BY p.fecha_confeccion, p.hora_confeccion"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets")
            Exit Function
        End If
        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl02.Text = CStr(ContadorCRT)
            Lbl02.Refresh()
            CuantosPalets = CuantosPalets + 1
            ReDim Preserve Palets(10, CuantosPalets)
            Palets(0, CuantosPalets) = Trim("" + RsPalets!Paletizador)
            Palets(1, CuantosPalets) = CStr(RsPalets!orden_confeccion)
            Palets(2, CuantosPalets) = Format(RsPalets!fecha_confeccion, "dd/MM/yyyy")
            Hora = Format(CDate(RsPalets!hora_confeccion), "HH:mm:ss")
            If Hora >= "09:30:00" And Hora < "10:00:00" Then
                Hora = "10:00:00"
            End If
            Palets(3, CuantosPalets) = Trim(Hora)
            Palets(4, CuantosPalets) = CStr(RsPalets!codigo_Palet)
            Palets(5, CuantosPalets) = ""
            Palets(6, CuantosPalets) = Trim(RsPalets!Codigo_Variedad)
            Palets(7, CuantosPalets) = CStr(RsPalets!peso_palet)
            Palets(8, CuantosPalets) = Trim(RsPalets!codigo_familia)
            If Not (IsDBNull(RsPalets!codigo_subfamilia)) Then
                If Trim(RsPalets!codigo_subfamilia) > "" Then
                    Palets(8, CuantosPalets) = Trim(RsPalets!codigo_subfamilia)
                End If
            End If
            V = Split(Trim("" + RsPalets!Paletizador), "/")
            For j = 0 To UBound(V)
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!codigo_Palet = Trim(RsPalets!codigo_Palet)
                RsTemp!Paletizador = V(j)
                RsTemp!hasta_fecha = RsPalets!fecha_confeccion
                RsTemp!hasta_hora = Trim(RsPalets!hora_confeccion)
                RsTemp!Codigo_Variedad = Trim(RsPalets!Codigo_Variedad)
                RsTemp!peso_palet = RsPalets!peso_palet
                RsTemp!familia_variedad = Trim(RsPalets!codigo_familia)
                If Not (IsDBNull(RsPalets!codigo_subfamilia)) Then
                    If Trim(RsPalets!codigo_subfamilia) > "" Then
                        RsTemp!familia_variedad = Trim(RsPalets!codigo_subfamilia)
                    End If
                End If
                RsTemp!codigo_confeccion = Trim(RsPalets!codigo_confeccion)
                RsTemp.Update()
            Next
        Next
        RsTemp.Close()
        For i = 1 To 25
            FechaAnterior = FechaProceso
            HoraAnterior = "00:00:00"

            SQL = "SELECT * FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + " AND PALETIZADOR = " + CStr(i) + " ORDER BY PROCESO,PALETIZADOR,HASTA_FECHA,HASTA_HORA"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes1")
                Exit Function
            End If
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                ContadorCRT = ContadorCRT + 1
                Lbl02.Text = CStr(ContadorCRT)
                Lbl02.Refresh()
                HoraActual = RsTemp!hasta_hora
                If FechaAnterior = RsTemp!hasta_fecha Then
                    If HoraAnterior < "09:30:00" And Trim(RsTemp!hasta_hora) >= "10:00:00" Then
                        If DateDiff("n", CDate(HoraAnterior), CDate(RsTemp!hasta_hora)) > 40 Then
                            HoraActual = Format(DateAdd("n", -5, CDate(RsTemp!hasta_hora)), "HH:mm:ss")
                        End If
                    Else
                        If DateDiff("n", CDate(HoraAnterior), CDate(RsTemp!hasta_hora)) > 10 Then
                            HoraActual = Format(DateAdd("n", -5, CDate(RsTemp!hasta_hora)), "HH:mm:ss")
                        End If
                    End If
                End If
                RsTemp!desde_fecha = FechaAnterior
                RsTemp!desde_hora = HoraAnterior
                RsTemp!hasta_hora = HoraActual
                FechaAnterior = RsTemp!hasta_fecha
                HoraAnterior = RsTemp!hasta_hora
                RsTemp.Update()
            Next
            RsTemp.Close()
        Next
        'GrabarPaletsConfeccionados = True
        Return True
    End Function
    Private Function GrabarPaletsIndustria() As Boolean
        Dim Cuantos As Double
        Dim SQL As String, RsPalets As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsTemp2 As New cnRecordset.CnRecordset ', Mensaje As String
        Dim i As Integer, j As Integer

        GrabarPaletsIndustria = False
        For i = 0 To 12 : For j = 0 To 1 : Industria(i, j) = 0 : Next : Next
        SQL = "SELECT clave_marcaje,left(codigo,2) as tipo,count(*) AS cuantos FROM hco_fichadas WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and fecha = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND clave_marcaje in (3714,6112,6116) GROUP BY clave_marcaje,left(codigo,2)"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla hco_fichadas")
            Exit Function
        End If

        For i = 1 To RsTemp.RecordCount
            RsTemp.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl02.Text = CStr(ContadorCRT)
            Lbl02.Refresh()
            If Not IsDBNull(RsTemp!Cuantos) Then
                Cuantos = RsTemp!Cuantos
                If Cuantos > 0 Then
                    If Trim(RsTemp!Tipo) = "NG" Then
                        Industria(1, 0) = Industria(1, 0) + Cuantos
                        Industria(1, 1) = Industria(1, 1) + PesoIndustriaNaranjaGrande * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "NQ" Then
                        Industria(2, 0) = Industria(2, 0) + Cuantos
                        Industria(2, 1) = Industria(2, 1) + PesoIndustriaNaranjaPequeno * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "MG" Then
                        Industria(3, 0) = Industria(3, 0) + Cuantos
                        Industria(3, 1) = Industria(3, 1) + PesoIndustriaClementinaGrande * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "MQ" Then
                        Industria(4, 0) = Industria(4, 0) + Cuantos
                        Industria(4, 1) = Industria(4, 1) + PesoIndustriaClementinaPequeno * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "HG" Then
                        Industria(5, 0) = Industria(5, 0) + Cuantos
                        Industria(5, 1) = Industria(5, 1) + PesoIndustriaClementinaGrande * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "HQ" Then
                        Industria(6, 0) = Industria(6, 0) + Cuantos
                        Industria(6, 1) = Industria(6, 1) + PesoIndustriaClementinaPequeno * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "SG" Then
                        Industria(7, 0) = Industria(7, 0) + Cuantos
                        Industria(7, 1) = Industria(7, 1) + PesoIndustriaClementinaGrande * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "SQ" Then
                        Industria(8, 0) = Industria(8, 0) + Cuantos
                        Industria(8, 1) = Industria(8, 1) + PesoIndustriaClementinaPequeno * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "BG" Then
                        Industria(9, 0) = Industria(9, 0) + Cuantos
                        Industria(9, 1) = Industria(9, 1) + PesoIndustriaNaranjaGrande * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "BQ" Then
                        Industria(10, 0) = Industria(10, 0) + Cuantos
                        Industria(10, 1) = Industria(10, 1) + PesoIndustriaNaranjaPequeno * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "RG" Then
                        Industria(11, 0) = Industria(11, 0) + Cuantos
                        Industria(11, 1) = Industria(11, 1) + PesoIndustriaNaranjaGrande * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "RQ" Then
                        Industria(12, 0) = Industria(12, 0) + Cuantos
                        Industria(12, 1) = Industria(12, 1) + PesoIndustriaNaranjaPequeno * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "SB" Then
                        '                PaletsISandiaBlanca = PaletsISandiaBlanca + Cuantos
                        '                KilosISandiaBlanca = KilosISandiaBlanca + PesoIndustriaSandia * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "SN" Then
                        '                PaletsISandiaNegra = PaletsISandiaNegra + Cuantos
                        '                KilosISandiaNegra = KilosISandiaNegra + PesoIndustriaSandia * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "SX" Then
                        '                PaletsISandiaBlanca = PaletsISandiaBlanca + Cuantos
                        '                KilosISandiaBlanca = KilosISandiaBlanca + PesoIndustriaSandia * Cuantos
                    ElseIf Trim(RsTemp!Tipo) = "SY" Then
                        '                PaletsISandiaNegra = PaletsISandiaNegra + Cuantos
                        '                KilosISandiaNegra = KilosISandiaNegra + PesoIndustriaSandia * Cuantos
                    Else
                        MsgBox("Anote marcaje de industria con clave_marcaje: " + CStr(RsTemp!clave_marcaje))
                    End If
                End If
            End If
        Next
        RsTemp.Close()

        SQL = "SELECT * FROM TEMP_COSTES1 WHERE 1=0"
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        If Industria(1, 1) + Industria(2, 1) > 0 Then
            RsTemp2.AddNew()
            RsTemp2!Proceso = Proceso
            RsTemp2!codigo_Palet = "1"
            RsTemp2!Paletizador = 30
            RsTemp2!Codigo_Variedad = ""
            RsTemp2!familia_variedad = "NAR"
            RsTemp2!peso_palet = Math.Round(Industria(1, 1) + Industria(2, 1), 0)
            RsTemp2!codigo_confeccion = "D"
            RsTemp2.Update()
        End If
        If Industria(3, 1) + Industria(4, 1) > 0 Then
            RsTemp2.AddNew()
            RsTemp2!Proceso = Proceso
            RsTemp2!codigo_Palet = "2"
            RsTemp2!Paletizador = 30
            RsTemp2!Codigo_Variedad = ""
            RsTemp2!familia_variedad = "CLE"
            RsTemp2!peso_palet = Math.Round(Industria(3, 1) + Industria(4, 1), 0)
            RsTemp2!codigo_confeccion = "D"
            RsTemp2.Update()
        End If
        If Industria(5, 1) + Industria(6, 1) > 0 Then
            RsTemp2.AddNew()
            RsTemp2!Proceso = Proceso
            RsTemp2!codigo_Palet = "3"
            RsTemp2!Paletizador = 30
            RsTemp2!Codigo_Variedad = ""
            RsTemp2!familia_variedad = "HIB"
            RsTemp2!peso_palet = Math.Round(Industria(5, 1) + Industria(6, 1), 0)
            RsTemp2!codigo_confeccion = "D"
            RsTemp2.Update()
        End If
        If Industria(7, 1) + Industria(8, 1) > 0 Then
            RsTemp2.AddNew()
            RsTemp2!Proceso = Proceso
            RsTemp2!codigo_Palet = "4"
            RsTemp2!Paletizador = 30
            RsTemp2!Codigo_Variedad = ""
            RsTemp2!familia_variedad = "SAT"
            RsTemp2!peso_palet = Math.Round(Industria(7, 1) + Industria(8, 1), 0)
            RsTemp2!codigo_confeccion = "D"
            RsTemp2.Update()
        End If
        If Industria(9, 1) + Industria(10, 1) > 0 Then
            RsTemp2.AddNew()
            RsTemp2!Proceso = Proceso
            RsTemp2!codigo_Palet = "5"
            RsTemp2!Paletizador = 30
            RsTemp2!Codigo_Variedad = ""
            RsTemp2!familia_variedad = "NAB"
            RsTemp2!peso_palet = Math.Round(Industria(9, 1) + Industria(10, 1), 0)
            RsTemp2!codigo_confeccion = "D"
            RsTemp2.Update()
        End If
        If Industria(11, 1) + Industria(12, 1) > 0 Then
            RsTemp2.AddNew()
            RsTemp2!Proceso = Proceso
            RsTemp2!codigo_Palet = "6"
            RsTemp2!Paletizador = 30
            RsTemp2!Codigo_Variedad = ""
            RsTemp2!familia_variedad = "ROJ"
            RsTemp2!peso_palet = Math.Round(Industria(11, 1) + Industria(12, 1), 0)
            RsTemp2!codigo_confeccion = "D"
            RsTemp2.Update()
        End If

        RsTemp2.Close()
        GrabarPaletsIndustria = True
    End Function

    Private Function GrabarPaletsPrecalibrado() As Boolean
        Dim Cajas As Integer, SubFamilia As String
        Dim SQL As String, RsPalets As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsTemp2 As New cnRecordset.CnRecordset ', Mensaje As String
        Dim i As Integer, j As Integer

        GrabarPaletsPrecalibrado = False
        For i = 0 To 12 : For j = 0 To 1 : Precalibrado(i, j) = 0 : Next : Next

        SQL = "SELECT * FROM palets_precalibre JOIN VARIEDADES ON VARIEDADES.EMPRESA = PALETS_PRECALIBRE.EMPRESA AND VARIEDADES.CODIGO_VARIEDAD = PALETS_PRECALIBRE.CODIGO_VARIEDAD WHERE PALETS_PRECALIBRE.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and PALETS_PRECALIBRE.fecha_confeccion = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets_precalibre")
            Exit Function
        End If

        SQL = "SELECT * FROM TEMP_COSTES1 WHERE 1=0"
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If

        For i = 1 To RsTemp.RecordCount
            RsTemp.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl02.Text = CStr(ContadorCRT)
            Lbl02.Refresh()

            SubFamilia = Trim(RsTemp!codigo_familia)
            If Not (IsDBNull(RsTemp!codigo_subfamilia)) Then
                If Trim(RsTemp!codigo_subfamilia) > "" Then
                    SubFamilia = Trim(RsTemp!codigo_subfamilia)
                End If
            End If

            If Not IsDBNull(RsTemp!Bultos) Then
                RsTemp2.AddNew()
                RsTemp2!Proceso = Proceso
                RsTemp2!codigo_Palet = RsTemp!codigo_Palet
                RsTemp2!Paletizador = 31
                RsTemp2!Codigo_Variedad = ""
                RsTemp2!codigo_confeccion = "PRECL"
                Cajas = RsTemp!Bultos
                If InStr(1, Trim(SubFamilia), "NAR") > 0 Then
                    Precalibrado(1, 0) = Precalibrado(1, 0) + 1
                    Precalibrado(1, 1) = Precalibrado(1, 1) + PesoCajaNaranjaPrecalibre * Cajas
                    RsTemp2!familia_variedad = "NAR"
                    RsTemp2!peso_palet = PesoCajaNaranjaPrecalibre * Cajas
                ElseIf InStr(1, Trim(SubFamilia), "CLE") > 0 Then
                    Precalibrado(3, 0) = Precalibrado(3, 0) + 1
                    Precalibrado(3, 1) = Precalibrado(3, 1) + PesoCajaClementinaPrecalibre * Cajas
                    RsTemp2!familia_variedad = "CLE"
                    RsTemp2!peso_palet = PesoCajaClementinaPrecalibre * Cajas
                ElseIf InStr(1, Trim(SubFamilia), "HIB") > 0 Then
                    Precalibrado(5, 0) = Precalibrado(5, 0) + 1
                    Precalibrado(5, 1) = Precalibrado(5, 1) + PesoCajaClementinaPrecalibre * Cajas
                    RsTemp2!familia_variedad = "HIB"
                    RsTemp2!peso_palet = PesoCajaClementinaPrecalibre * Cajas
                ElseIf InStr(1, Trim(SubFamilia), "SAT") > 0 Then
                    Precalibrado(7, 0) = Precalibrado(7, 0) + 1
                    Precalibrado(7, 1) = Precalibrado(7, 1) + PesoCajaClementinaPrecalibre * Cajas
                    RsTemp2!familia_variedad = "SAT"
                    RsTemp2!peso_palet = PesoCajaClementinaPrecalibre * Cajas
                ElseIf InStr(1, Trim(SubFamilia), "NAB") > 0 Then '
                    Precalibrado(9, 0) = Precalibrado(9, 0) + 1
                    Precalibrado(9, 1) = Precalibrado(9, 1) + PesoCajaNaranjaPrecalibre * Cajas
                    RsTemp2!familia_variedad = "NAB"
                    RsTemp2!peso_palet = PesoCajaNaranjaPrecalibre * Cajas
                ElseIf InStr(1, Trim(SubFamilia), "ROJ") > 0 Then '
                    Precalibrado(11, 0) = Precalibrado(11, 0) + 1
                    Precalibrado(11, 1) = Precalibrado(11, 1) + PesoCajaNaranjaPrecalibre * Cajas
                    RsTemp2!familia_variedad = "ROJ"
                    RsTemp2!peso_palet = PesoCajaNaranjaPrecalibre * Cajas
                    'ElseIf InStr(1, Trim(SubFamilia), "SAN") > 0 Then
                    '    Precalibrado(13, 0) = Precalibrado(13, 0) + 1
                    '    Precalibrado(13, 1) = Precalibrado(13, 1) + PesoVolcadoPalotOtros * Cajas
                    '    RsTemp2!familia_variedad = "SAND"
                    '    RsTemp2!peso_palet = PesoVolcadoPalotOtros * Cajas
                End If
                RsTemp2.Update()
            End If
        Next
        RsTemp.Close()
        RsTemp2.Close()
        GrabarPaletsPrecalibrado = True
    End Function

    Private Function AnalisisVolcado() As Boolean
        Dim Cajas As Integer, PesoCaja As Single, PesoBulto As Single
        Dim SQL As String, RsTemp As New cnRecordset.CnRecordset, RsFichadas As New cnRecordset.CnRecordset
        Dim i As Integer

        AnalisisVolcado = False
        SQL = "SELECT hco_fichadas.contador,hco_fichadas.cajas as cajas,palets_precalibre.codigo_variedad as codigo_variedad,isnull(variedades.codigo_subfamilia, variedades.codigo_familia) as vfamilia_variedad, hco_fichadas.clave_marcaje, 'PRE' as origen_marcado FROM hco_fichadas JOIN palets_precalibre ON palets_precalibre.empresa = hco_fichadas.empresa AND palets_precalibre.referencia = hco_fichadas.codigo"
        SQL = SQL + " JOIN VARIEDADES ON variedades.empresa = hco_fichadas.empresa AND variedades.codigo_variedad = palets_precalibre.codigo_variedad"
        SQL = SQL + " WHERE hco_fichadas.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND  hco_fichadas.clave_marcaje =8086 AND hco_fichadas.fecha = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        SQL = SQL + " UNION Select hco_fichadas.contador,hco_fichadas.cajas as cajas,entradas_albaranes.codigo_variedad as codigo_variedad ,isnull(variedades.codigo_subfamilia, variedades.codigo_familia) as vfamilia_variedad, hco_fichadas.clave_marcaje, 'ENT' as origen_marcado  FROM hco_fichadas JOIN entradas_albaranes ON entradas_albaranes.empresa = hco_fichadas.empresa and entradas_albaranes.ejercicio = hco_fichadas.ejercicio and entradas_albaranes.serie_albaran = hco_fichadas.serie_albaran and entradas_albaranes.numero_albaran = hco_fichadas.numero_albaran "
        SQL = SQL + " JOIN VARIEDADES ON variedades.empresa = hco_fichadas.empresa AND variedades.codigo_variedad = entradas_albaranes.codigo_variedad"
        SQL = SQL + " WHERE hco_fichadas.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND  hco_fichadas.accion in ('VOLCADO','VOLCADO_CAJA') AND hco_fichadas.fecha = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        If RsFichadas.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla hco_fichadas")
            Exit Function
        End If

        For i = 1 To RsFichadas.RecordCount
            RsFichadas.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl03.Text = CStr(ContadorCRT)
            Lbl03.Refresh()

            If Trim(RsFichadas!Codigo_Variedad) >= "01" And Trim(RsFichadas!Codigo_Variedad) <= "02z" Then
                If IsDBNull(RsFichadas!Cajas) Then
                    MsgBox("Marcaje de volcado que no indica cajas. Código = " + CStr(RsFichadas!Contador) + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If
                Cajas = RsFichadas!Cajas
                If Trim(RsFichadas!origen_marcado) = "PRE" Then
                    If Mid(RsFichadas!Codigo_Variedad, 1, 2) = "01" Then
                        PesoCaja = PesoCajaClementinaPrecalibre
                        If Cajas = 0 Then
                            Cajas = 1 : PesoCaja = PesoVolcadoClementinaPalot
                        End If
                    ElseIf Mid(RsFichadas!Codigo_Variedad, 1, 2) = "02" Then
                        PesoCaja = PesoCajaNaranjaPrecalibre
                        If Cajas = 0 Then
                            Cajas = 1 : PesoCaja = PesoVolcadoNaranjaPalot
                        End If
                    End If
                Else
                    If Mid(RsFichadas!Codigo_Variedad, 1, 2) = "01" Then
                        PesoCaja = PesoCajaClementina
                        If Cajas = 0 Then
                            Cajas = 1 : PesoCaja = PesoVolcadoClementinaPalot
                        End If
                    ElseIf Mid(RsFichadas!Codigo_Variedad, 1, 2) = "02" Then
                        PesoCaja = PesoCajaNaranja
                        If Cajas = 0 Then
                            Cajas = 1 : PesoCaja = PesoVolcadoNaranjaPalot
                        End If
                    End If
                End If
                PesoBulto = Math.Round(PesoCaja * Cajas, 0)
                SQL = "SELECT * FROM TEMP_COSTES2 WHERE PROCESO = " + CStr(Proceso) + " AND FAMILIA_VARIEDAD ='" + Trim(RsFichadas!vfamilia_variedad) + "'"
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes2")
                    Exit Function
                End If
                If RsTemp.RecordCount > 0 Then
                    RsTemp!peso_volcado = RsTemp!peso_volcado + PesoBulto
                Else
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!familia_variedad = Trim(RsFichadas!vfamilia_variedad)
                    RsTemp!peso_volcado = PesoBulto
                    RsTemp!importe1 = 0
                    RsTemp!importe2 = 0
                    RsTemp!importe3 = 0
                End If
                RsTemp.Update()
                RsTemp.Close()
            End If
        Next
        AnalisisVolcado = True
    End Function

    Private Function CostesGenerales() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset
        Dim i As Integer

        CostesGenerales = False

        SQL = "SELECT * FROM PERSONAL_ES where PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and not (puesto in ('018','105','101')) "
        SQL = Trim(SQL) + " AND NOT EXISTS (SELECT EMPRESA FROM PERSONAL_PUESTOS where personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = personal_es.puesto)"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es (puestos incorrectos)")
            Exit Function
        End If
        If Rs.RecordCount > 0 Then
            MsgBox("Existen costes del día indicado con puesto incorrecto (" + Trim(Rs!puesto) + ")" + vbCrLf + "Se interrumpe el proceso.")
            Exit Function
        End If
        Rs.Close()

        ' Quitamos los tipo puestos A e F
        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = personal_es.puesto WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and not (puesto in ('018','105','101')) AND TIPO_PUESTO IN('O','M','E','G') ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl04.Text = CStr(ContadorCRT)
            Lbl04.Refresh()

            ' Costes no productivos
            If Trim(Rs!tipo_puesto) = "O" Or Trim(Rs!tipo_puesto) = "M" Or Trim(Rs!tipo_puesto) = "F" Or Trim(Rs!tipo_puesto) = "A" Or Trim(Rs!tipo_puesto) = "E" Then
                SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "'"
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes3")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                    RsTemp!desde_variedad = ""
                    RsTemp!hasta_variedad = ""
                    RsTemp!apartado = 0
                    RsTemp!subapartado = 0
                    RsTemp!linea_informe = 0
                    RsTemp!importe_coste = 0
                End If
                RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                RsTemp.Update()
                RsTemp.Close()
            ElseIf Trim(Rs!tipo_puesto) = "G" Then
                'Coste general productivo
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    'Coste general NO productivo
                    SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO =" + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' AND apartado = 0 And subapartado = 0 And linea_informe = 0"
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = 0
                        RsTemp!subapartado = 0
                        RsTemp!linea_informe = 0
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                End If
            End If

            ' Ahora haremos lo mismo con Temp_costes33 que es lo mismo con usuario
            If Trim(Rs!tipo_puesto) = "O" Or Trim(Rs!tipo_puesto) = "M" Or Trim(Rs!tipo_puesto) = "E" Then
                SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "'"
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes3")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!Codigo_Operario = Rs!cod_operario
                    RsTemp!codigo_puesto = Rs!puesto
                    RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                    RsTemp!desde_variedad = ""
                    RsTemp!hasta_variedad = ""
                    RsTemp!apartado = 0
                    RsTemp!subapartado = 0
                    RsTemp!linea_informe = 0
                    RsTemp!importe_coste = 0
                End If
                RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                RsTemp.Update()
                RsTemp.Close()
            ElseIf Trim(Rs!tipo_puesto) = "G" Then
                'Coste general productivo
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!Codigo_Operario = Rs!cod_operario
                        RsTemp!codigo_puesto = Rs!puesto
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    'Coste general NO productivo
                    SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO =" + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' AND apartado = 0 And subapartado = 0 And linea_informe = 0"
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!Codigo_Operario = Rs!cod_operario
                        RsTemp!codigo_puesto = Rs!puesto
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = 0
                        RsTemp!subapartado = 0
                        RsTemp!linea_informe = 0
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                End If
            End If
            ' Fin de Temp_costes33
        Next
        CostesGenerales = True
    End Function
    Private Function CostesVolcado() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset
        Dim i As Integer

        CostesVolcado = False
        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = personal_es.puesto WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and not (puesto in ('018','105','101')) AND TIPO_PUESTO ='V' ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_puestos")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl05.Text = CStr(ContadorCRT)
            Lbl05.Refresh()

            ' Costes volcado
            If Trim(Rs!tipo_puesto) = "V" Then
                'Coste VOLCADO
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a volcado pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If

                ' Ahora temp_costes33
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes33")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!Codigo_Operario = Rs!cod_operario
                        RsTemp!codigo_puesto = Rs!puesto
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a volcado pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If
            End If
        Next
        CostesVolcado = True
    End Function
    Private Function CostesIndustria() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset
        Dim i As Integer

        CostesIndustria = False

        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = personal_es.puesto WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and not (puesto in ('018','105','101')) AND TIPO_PUESTO ='I' ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl05.Text = CStr(ContadorCRT)
            Lbl05.Refresh()

            ' Costes industria al 100%
            If Trim(Rs!tipo_puesto) = "I" Then
                'Coste industria
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a industria pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If

                ' Ahora la tabla TempCostes33
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes33")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!Codigo_Operario = Rs!cod_operario
                        RsTemp!codigo_puesto = Rs!puesto
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a industria pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If
            End If
        Next
        CostesIndustria = True

    End Function
    Private Function CostesPrecalibrado() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset
        Dim i As Integer

        CostesPrecalibrado = False

        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = personal_es.puesto WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and not (puesto in ('018','105','101')) AND TIPO_PUESTO ='R' ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl05.Text = CStr(ContadorCRT)
            Lbl05.Refresh()

            ' Costes precalibrado al 100%
            If Trim(Rs!tipo_puesto) = "R" Then
                'Coste industria
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes3")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a precalibrado pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If

                ' Ahora la tabla temp_costes33
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                    If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes33")
                        Exit Function
                    End If
                    If RsTemp.RecordCount = 0 Then
                        RsTemp.AddNew()
                        RsTemp!Proceso = Proceso
                        RsTemp!Codigo_Operario = Rs!cod_operario
                        RsTemp!codigo_puesto = Rs!puesto
                        RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                        RsTemp!desde_variedad = ""
                        RsTemp!hasta_variedad = ""
                        RsTemp!apartado = Rs!apartado
                        RsTemp!subapartado = Rs!subapartado
                        RsTemp!linea_informe = Rs!linea_informe
                        RsTemp!importe_coste = 0
                    End If
                    RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                    RsTemp.Update()
                    RsTemp.Close()
                Else
                    MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a precalibrado pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                    Exit Function
                End If

            End If
        Next
        CostesPrecalibrado = True
    End Function

    Private Function CostesPaletizado() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset ', RsFichadas As New cnRecordset.CnRecordset, Mensaje As String
        Dim i As Integer

        CostesPaletizado = False
        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = personal_es.puesto WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and not (puesto in ('018','105','101')) AND TIPO_PUESTO IN ('P','PI') ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl06.Text = CStr(ContadorCRT)
            Lbl06.Refresh()

            ' Costes paletizado
            If Trim(Rs!tipo_puesto) = "P" Or Trim(Rs!tipo_puesto) = "PI" Then
                'Coste PALETIZADO solo se graban ahora los que no están  asignados a ningún paletizador
                If Rs!paletizador01 = 0 And Rs!paletizador02 = 0 And Rs!paletizador03 = 0 And Rs!paletizador04 = 0 And Rs!paletizador05 = 0 And Rs!paletizador06 = 0 And Rs!paletizador07 = 0 And Rs!paletizador08 = 0 And Rs!paletizador09 = 0 And Rs!paletizador10 = 0 And Rs!paletizador11 = 0 And Rs!paletizador12 = 0 And Rs!paletizador13 = 0 And Rs!paletizador14 = 0 And Rs!paletizador15 = 0 And Rs!paletizador16 = 0 And Rs!paletizador17 = 0 And Rs!paletizador18 = 0 And Rs!paletizador19 = 0 And Rs!paletizador20 = 0 And Rs!paletizador21 = 0 And Rs!paletizador22 = 0 And Rs!paletizador23 = 0 And Rs!paletizador24 = 0 And Rs!paletizador25 = 0 Then
                    If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                        SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                            MsgBox("Error al abrir la tabla temp_costes3")
                            Exit Function
                        End If
                        If RsTemp.RecordCount = 0 Then
                            RsTemp.AddNew()
                            RsTemp!Proceso = Proceso
                            RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                            RsTemp!desde_variedad = ""
                            RsTemp!hasta_variedad = ""
                            RsTemp!apartado = Rs!apartado
                            RsTemp!subapartado = Rs!subapartado
                            RsTemp!linea_informe = Rs!linea_informe
                            RsTemp!importe_coste = 0
                        End If
                        RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                        RsTemp.Update()
                        RsTemp.Close()
                    Else
                        MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a paletizado pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                        Exit Function
                    End If
                End If

                ' Ahora la tabla temp_costes33
                If Rs!paletizador01 = 0 And Rs!paletizador02 = 0 And Rs!paletizador03 = 0 And Rs!paletizador04 = 0 And Rs!paletizador05 = 0 And Rs!paletizador06 = 0 And Rs!paletizador07 = 0 And Rs!paletizador08 = 0 And Rs!paletizador09 = 0 And Rs!paletizador10 = 0 And Rs!paletizador11 = 0 And Rs!paletizador12 = 0 And Rs!paletizador13 = 0 And Rs!paletizador14 = 0 And Rs!paletizador15 = 0 And Rs!paletizador16 = 0 And Rs!paletizador17 = 0 And Rs!paletizador18 = 0 And Rs!paletizador19 = 0 And Rs!paletizador20 = 0 And Rs!paletizador21 = 0 And Rs!paletizador22 = 0 And Rs!paletizador23 = 0 And Rs!paletizador24 = 0 And Rs!paletizador25 = 0 Then
                    If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                        SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                            MsgBox("Error al abrir la tabla temp_costes3")
                            Exit Function
                        End If
                        If RsTemp.RecordCount = 0 Then
                            RsTemp.AddNew()
                            RsTemp!Proceso = Proceso
                            RsTemp!Codigo_Operario = Rs!cod_operario
                            RsTemp!codigo_puesto = Rs!puesto
                            RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                            RsTemp!desde_variedad = ""
                            RsTemp!hasta_variedad = ""
                            RsTemp!apartado = Rs!apartado
                            RsTemp!subapartado = Rs!subapartado
                            RsTemp!linea_informe = Rs!linea_informe
                            RsTemp!importe_coste = 0
                        End If
                        RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                        RsTemp.Update()
                        RsTemp.Close()
                    Else
                        MsgBox("El puesto " + Trim(Rs!codigo_puesto) + " es un coste asociado a paletizado pero no tiene indicado apartado en el informe." + vbCrLf + "Se interrumpe el proceso.")
                        Exit Function
                    End If
                End If
            End If
        Next
        CostesPaletizado = True
    End Function

    Private Function CostesEspeciales() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim Importe1 As Double, Importe2 As Double, Importe3 As Double

        CostesEspeciales = False

        Lbl07.Text = "018"
        Lbl07.Refresh()

        'Costes especiales puesto 018 --> 313
        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = '313' WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and puesto='018' ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            'El 313 es un coste General productivo
            SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes3")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = Rs!apartado
                RsTemp!subapartado = Rs!subapartado
                RsTemp!linea_informe = Rs!linea_informe
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
            RsTemp.Update()
            RsTemp.Close()

            ' Ahora la tabla temp_costes33
            SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes33")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!Codigo_Operario = Rs!cod_operario
                RsTemp!codigo_puesto = Rs!puesto
                RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = Rs!apartado
                RsTemp!subapartado = Rs!subapartado
                RsTemp!linea_informe = Rs!linea_informe
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
            RsTemp.Update()
            RsTemp.Close()
        Next
        Rs.Close()

        Lbl07.Text = "105"
        Lbl07.Refresh()

        'Costes especiales puesto 105 --> 330
        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS on personal_puestos.empresa = personal_es.empresa and personal_puestos.codigo_puesto = '330' WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and puesto='105' ORDER BY TIPO_PUESTO,CODIGO_PUESTO,FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            'El 330 es un coste de paletizado
            If Rs!paletizador01 = 0 And Rs!paletizador02 = 0 And Rs!paletizador03 = 0 And Rs!paletizador04 = 0 And Rs!paletizador05 = 0 And Rs!paletizador06 = 0 And Rs!paletizador07 = 0 And Rs!paletizador08 = 0 And Rs!paletizador09 = 0 And Rs!paletizador10 = 0 And Rs!paletizador11 = 0 And Rs!paletizador12 = 0 And Rs!paletizador13 = 0 And Rs!paletizador14 = 0 And Rs!paletizador15 = 0 And Rs!paletizador16 = 0 And Rs!paletizador17 = 0 And Rs!paletizador18 = 0 And Rs!paletizador19 = 0 And Rs!paletizador20 = 0 And Rs!paletizador21 = 0 And Rs!paletizador22 = 0 And Rs!paletizador23 = 0 And Rs!paletizador24 = 0 And Rs!paletizador25 = 0 Then
                SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes3")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                    RsTemp!desde_variedad = ""
                    RsTemp!hasta_variedad = ""
                    RsTemp!apartado = Rs!apartado
                    RsTemp!subapartado = Rs!subapartado
                    RsTemp!linea_informe = Rs!linea_informe
                    RsTemp!importe_coste = 0
                End If
                RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                RsTemp.Update()
                RsTemp.Close()

                ' Ahorta tabla temp_costes33
                SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = '" + Trim(Rs!tipo_puesto) + "' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes33")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!Codigo_Operario = Rs!cod_operario
                    RsTemp!codigo_puesto = Rs!puesto
                    RsTemp!tipo_coste = Trim(Rs!tipo_puesto)
                    RsTemp!desde_variedad = ""
                    RsTemp!hasta_variedad = ""
                    RsTemp!apartado = Rs!apartado
                    RsTemp!subapartado = Rs!subapartado
                    RsTemp!linea_informe = Rs!linea_informe
                    RsTemp!importe_coste = 0
                End If
                RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                RsTemp.Update()
                RsTemp.Close()

            End If
        Next
        Rs.Close()

        Lbl07.Text = "101"
        Lbl07.Refresh()

        'Costes especiales puesto 101 a repartir entre los diferentes trabajos de carretilla
        SQL = "SELECT * FROM PERSONAL_ES WHERE PERSONAL_es.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA_ENTRADA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and puesto='101' ORDER BY FECHA_ENTRADA,HORA_ENTRADA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            Importe1 = Math.Round(Rs!importe_coste, 2) * 0.4
            Importe2 = Importe1
            Importe3 = Math.Round(Rs!importe_coste - Importe1 - Importe2, 2)

            SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = 'G' And apartado = 2 And subapartado = 1 And linea_informe = 1"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes3")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!tipo_coste = "G"
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = 2
                RsTemp!subapartado = 1
                RsTemp!linea_informe = 1
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Importe1
            RsTemp.Update()
            RsTemp.Close()

            SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = 'V' And apartado = 2 And subapartado = 1 And linea_informe = 1"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes3")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!tipo_coste = "V"
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = 2
                RsTemp!subapartado = 1
                RsTemp!linea_informe = 1
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Importe2
            RsTemp.Update()
            RsTemp.Close()

            SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = 'G' And apartado = 2 And subapartado = 1 And linea_informe = 2"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes3")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!tipo_coste = "G"
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = 2
                RsTemp!subapartado = 1
                RsTemp!linea_informe = 2
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Importe3
            RsTemp.Update()
            RsTemp.Close()

            ' Ahora lo mismo pero con la tabla temp_costes33
            SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = 'G' And apartado = 2 And subapartado = 1 And linea_informe = 1"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes33")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!Codigo_Operario = Rs!cod_operario
                RsTemp!codigo_puesto = Rs!puesto
                RsTemp!tipo_coste = "G"
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = 2
                RsTemp!subapartado = 1
                RsTemp!linea_informe = 1
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Importe1
            RsTemp.Update()
            RsTemp.Close()

            SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = 'V' And apartado = 2 And subapartado = 1 And linea_informe = 1"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes33")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!Codigo_Operario = Rs!cod_operario
                RsTemp!codigo_puesto = Rs!puesto
                RsTemp!tipo_coste = "V"
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = 2
                RsTemp!subapartado = 1
                RsTemp!linea_informe = 1
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Importe2
            RsTemp.Update()
            RsTemp.Close()

            SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = 'G' And apartado = 2 And subapartado = 1 And linea_informe = 2"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes33")
                Exit Function
            End If
            If RsTemp.RecordCount = 0 Then
                RsTemp.AddNew()
                RsTemp!Proceso = Proceso
                RsTemp!Codigo_Operario = Rs!cod_operario
                RsTemp!codigo_puesto = Rs!puesto
                RsTemp!tipo_coste = "G"
                RsTemp!desde_variedad = ""
                RsTemp!hasta_variedad = ""
                RsTemp!apartado = 2
                RsTemp!subapartado = 1
                RsTemp!linea_informe = 2
                RsTemp!importe_coste = 0
            End If
            RsTemp!importe_coste = RsTemp!importe_coste + Importe3
            RsTemp.Update()
            RsTemp.Close()
        Next
        Rs.Close()
        CostesEspeciales = True
    End Function

    Private Function RepartoVolcado() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsTemp2 As New cnRecordset.CnRecordset
        Dim i As Integer, j As Integer

        RepartoVolcado = False

        'Reparto de los costes de volcado por familia
        For i = 0 To 60 : Costes(i) = 0 : Next
        SQL = "SELECT * FROM TEMP_COSTES2 WHERE PROCESO = " + CStr(Proceso)
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes2")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            If Trim(Rs!familia_variedad) = "NAR" Then
                Costes(1) = Costes(1) + Rs!peso_volcado
            ElseIf Trim(Rs!familia_variedad) = "CLE" Then
                Costes(2) = Costes(2) + Rs!peso_volcado
            ElseIf Trim(Rs!familia_variedad) = "HIB" Then
                Costes(3) = Costes(3) + Rs!peso_volcado
            ElseIf Trim(Rs!familia_variedad) = "SAT" Then
                Costes(4) = Costes(4) + Rs!peso_volcado
            ElseIf Trim(Rs!familia_variedad) = "NAB" Then
                Costes(5) = Costes(5) + Rs!peso_volcado
            ElseIf Trim(Rs!familia_variedad) = "ROJ" Then
                Costes(6) = Costes(6) + Rs!peso_volcado
            Else
                '        MsgBox("volcado de familia no esperada: " + Trim(Rs!familia_variedad)
            End If
        Next
        Rs.Close()

        Costes(0) = Costes(1) * 15 + Costes(2) * 20 + Costes(3) * 20 + Costes(4) * 20 + Costes(5) * 15 + Costes(6) * 15
        If Costes(0) <> 0 Then
            For i = 1 To 6
                If i = 1 Or i >= 5 Then
                    Costes(10 + i) = Costes(i) * 15 / Costes(0)  'reparto de costes para cada familia
                Else
                    Costes(10 + i) = Costes(i) * 20 / Costes(0)   'reparto de costes para cada familia
                End If
            Next
        End If

        SQL = "SELECT proceso,codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  group by proceso,codigo_palet,familia_variedad,peso_palet"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount 'Kg fabricados de cada variedad
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            If Trim(Rs!familia_variedad) = "NAR" Then
                Costes(21) = Costes(21) + Rs!peso_palet
            ElseIf Trim(Rs!familia_variedad) = "CLE" Then
                Costes(22) = Costes(22) + Rs!peso_palet
            ElseIf Trim(Rs!familia_variedad) = "HIB" Then
                Costes(23) = Costes(23) + Rs!peso_palet
            ElseIf Trim(Rs!familia_variedad) = "SAT" Then
                Costes(24) = Costes(24) + Rs!peso_palet
            ElseIf Trim(Rs!familia_variedad) = "NAB" Then
                Costes(25) = Costes(25) + Rs!peso_palet
            ElseIf Trim(Rs!familia_variedad) = "ROJ" Then
                Costes(26) = Costes(26) + Rs!peso_palet
            Else
                If Trim(Rs!familia_variedad) <> "SAND" Then
                    MsgBox("Palet de familia no esperada: " + Trim(Rs!familia_variedad))
                End If
            End If
        Next
        For i = 1 To 6
            If Costes(20 + i) <> 0 Then
                Costes(10 + i) = Costes(10 + i) / Costes(20 + i)
            Else
                Costes(10 + i) = 0
            End If
        Next
        Costes(20) = Costes(21) + Costes(22) + Costes(23) + Costes(24) + Costes(25) + Costes(26)  ' Kilos confeccionados incluyendo precalibrado e industria
        Costes(31) = Math.Round(Industria(1, 1) + Industria(2, 1), 0)
        Costes(32) = Math.Round(Industria(3, 1) + Industria(4, 1), 0)
        Costes(33) = Math.Round(Industria(5, 1) + Industria(6, 1), 0)
        Costes(34) = Math.Round(Industria(7, 1) + Industria(8, 1), 0)
        Costes(35) = Math.Round(Industria(9, 1) + Industria(10, 1), 0)
        Costes(36) = Math.Round(Industria(11, 1) + Industria(12, 1), 0)

        Costes(37) = Math.Round(Precalibrado(1, 1), 0)
        Costes(38) = Math.Round(Precalibrado(3, 1), 0)
        Costes(39) = Math.Round(Precalibrado(5, 1), 0)
        Costes(40) = Math.Round(Precalibrado(7, 1), 0)
        Costes(41) = Math.Round(Precalibrado(9, 1), 0)
        Costes(42) = Math.Round(Precalibrado(11, 1), 0)

        Costes(60) = Costes(20) - Costes(37) - Costes(38) - Costes(39) - Costes(40) - Costes(41) - Costes(42)  'Kilos confeccionados sin precalibrado (incluye industria)
        Costes(50) = Costes(60) - Costes(31) - Costes(32) - Costes(33) - Costes(34) - Costes(35) - Costes(36) ' Kilos confeccionados sin industria ni precalibrado
        Costes(43) = Costes(31) + Costes(32) + Costes(33) + Costes(34) + Costes(35) + Costes(36) ' Kilos industria
        Costes(44) = Costes(37) + Costes(38) + Costes(39) + Costes(40) + Costes(41) + Costes(42) ' Kilos precalibrado
        If Costes(60) > 0 Then
            Costes(45) = Costes(50) / Costes(60) ' Proporción confeccionado sobre conf + industria
            Costes(46) = Costes(43) / Costes(60) ' Proporción industria  sobre conf + industria
        End If

        SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='V' order by apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes3")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j

                SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " AND CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes4")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                    'RsTemp2.Update()
                End If ' aaaa
                If Trim(Rs!familia_variedad) = "NAR" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(11) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "CLE" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(12) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "HIB" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(13) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "SAT" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(14) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "NAB" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(15) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "ROJ" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(16) * Rs!peso_palet))
                Else
                    If Trim(Rs!familia_variedad) <> "SAND" Then
                        MsgBox("Palet de familia no esperada: " + Trim(Rs!familia_variedad))
                    End If
                End If


                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        RsTemp.Close()

        SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes4")
            Exit Function
        End If
        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            RsTemp2!importe_volcado = Math.Round(RsTemp2!importe_volcado, 6)
            RsTemp2.Update()
        Next
        RsTemp2.Close()

        ' Ahora procesaremos la tabla Temp_costes33 y temp_costes44
        SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='V' order by codigo_operario, codigo_puesto, apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes33")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario =" + CStr(RsTemp!Codigo_Operario) + " AND codigo_puesto = '" + Trim(RsTemp!codigo_puesto) + "' AND CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes44")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp2!Proceso = Proceso
                    RsTemp2!Codigo_Operario = RsTemp!Codigo_Operario
                    RsTemp2!codigo_puesto = RsTemp!codigo_puesto
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                    RsTemp2!importe_total = 0
                    'RsTemp2.Update()
                End If
                If Trim(Rs!familia_variedad) = "NAR" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(11) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "CLE" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(12) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "HIB" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(13) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "SAT" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(14) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "NAB" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(15) * Rs!peso_palet))
                ElseIf Trim(Rs!familia_variedad) = "ROJ" Then
                    RsTemp2!importe_volcado = RsTemp2!importe_volcado + ((RsTemp!importe_coste * Costes(16) * Rs!peso_palet))
                Else
                    If Trim(Rs!familia_variedad) <> "SAND" Then
                        MsgBox("Palet de familia no esperada: " + Trim(Rs!familia_variedad))
                    End If
                End If
                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        RsTemp.Close()
        SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes44")
            Exit Function
        End If
        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            RsTemp2!importe_volcado = Math.Round(RsTemp2!importe_volcado, 6)
            RsTemp2.Update()
        Next
        RsTemp2.Close()
        Rs.Close()
        RepartoVolcado = True
    End Function
    Private Function RepartoCostesGenerales() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsTemp2 As New cnRecordset.CnRecordset ', Mensaje As String
        Dim i As Integer, j As Integer

        RepartoCostesGenerales = False

        'Reparto de los costes generales para cada palet
        SQL = "SELECT codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  AND codigo_confeccion<>'PRECL' group by codigo_palet,familia_variedad,peso_palet"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If

        SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='G' and apartado>0 and subapartado>0 and linea_informe>0 order by apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes3")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " and CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes4")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                    'RsTemp2.Update()
                End If
                If Costes(60) > 0 Then RsTemp2!importe_general = RsTemp2!importe_general + ((RsTemp!importe_coste * Rs!peso_palet) / Costes(60))


                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        RsTemp.Close()

        SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes4")
            Exit Function
        End If
        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            RsTemp2!importe_general = Math.Round(RsTemp2!importe_general, 6)
            RsTemp2.Update()
        Next
        RsTemp2.Close()

        ' Ahora con las tablas temp_costes33 y temp_costes44
        SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='G' and apartado>0 and subapartado>0 and linea_informe>0 order by codigo_operario, codigo_puesto, apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes33")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()

            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso) + " and CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND codigo_operario =" + CStr(RsTemp!Codigo_Operario) + " AND codigo_puesto = '" + Trim(RsTemp!codigo_puesto) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes44")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp2!Proceso = Proceso
                    RsTemp2!Codigo_Operario = RsTemp!Codigo_Operario
                    RsTemp2!codigo_puesto = RsTemp!codigo_puesto
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                End If

                'If Trim(RsTemp2!codigo_palet) = "301120501" And RsTemp!codigo_puesto = 324 And RsTemp2!apartado = 2 And RsTemp2!subapartado = 2 And RsTemp2!linea_informe = 1 And RsTemp2!codigo_operario = 2809 Then
                '    MsgBox("Anterior: " + CStr(RsTemp2!importe_general))
                'End If

                If Costes(60) > 0 Then RsTemp2!importe_general = RsTemp2!importe_general + ((RsTemp!importe_coste * Rs!peso_palet) / Costes(60))
                'If Trim(RsTemp2!codigo_palet) = "301120501" And RsTemp!codigo_puesto = 324 And RsTemp2!apartado = 2 And RsTemp2!subapartado = 2 And RsTemp2!linea_informe = 1 And RsTemp2!codigo_operario = 2809 Then
                '    MsgBox(CStr(RsTemp2!importe_general))
                'End If


                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        RsTemp.Close()
        SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes44")
            Exit Function
        End If
        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl08.Text = CStr(ContadorCRT)
            Lbl08.Refresh()
            RsTemp2!importe_general = Math.Round(RsTemp2!importe_general, 6)
            RsTemp2.Update()
        Next
        RsTemp2.Close()
        Rs.Close()
        RepartoCostesGenerales = True
    End Function
    Private Function RepartoCostesPaletizado() As Boolean
        Dim Hora As String, Fecha As Date, HoraFin As String
        Dim ImporteNoRepartido As Double, ImporteIndustria As Double, CosteMinuto As Double, Coste As Double, CosteTotal As Double
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsTemp2 As New cnRecordset.CnRecordset ', Mensaje As String
        Dim i As Integer, j As Integer, k As Integer, Paletizador As Long, FlagRepetido As Boolean
        '        'Dim Test(20) As Double


        RepartoCostesPaletizado = False

        GrabarAuxiliar(Proceso, FechaProceso, "  Graba 5")
        SQL = "SELECT * FROM TEMP_COSTES5 WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes5")
            Exit Function
        End If

        For i = 0 To 23
            For j = 0 To 59
                ContadorCRT = ContadorCRT + 1
                Lbl09.Text = CStr(ContadorCRT)
                Lbl09.Refresh()
                Hora = Format(i, "00") + ":" + Format(j, "00") + ":00"
                If Hora < "09:30:00" Or Hora >= "10:00:00" Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!Fecha = FechaProceso
                    RsTemp!Hora = Trim(Hora)
                    For k = 1 To 25
                        RsTemp("kilos_" & Format(k, "00")) = 0
                        RsTemp("palet_" & Format(k, "00")) = ""
                    Next
                    RsTemp.Update()
                End If
            Next
        Next
        RsTemp.Close()

        GrabarAuxiliar(Proceso, FechaProceso, "  Graba 5 (palets)")
        SQL = "SELECT * FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + " AND (PALETIZADOR<=25) ORDER BY HASTA_FECHA,HASTA_HORA"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            Fecha = CDate(Rs!DESDE_FECHA)
            Hora = Mid(Rs!desde_hora, 1, 6) + "00"
            HoraFin = Mid(Rs!hasta_hora, 1, 6) + "00"
            SQL = "SELECT * FROM TEMP_COSTES5 WHERE PROCESO = " + CStr(Proceso) + " AND FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "' AND HORA>='" + Trim(Hora) + "' AND HORA<'" + Trim(HoraFin) + "'"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes5")
                Exit Function
            End If
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                Paletizador = Rs!Paletizador
                RsTemp("kilos_" + Format(Paletizador, "00")) = RsTemp("kilos_" + Format(Paletizador, "00")) + Rs!peso_palet
                FlagRepetido = False
                If Not IsDBNull(RsTemp("palet_" + Format(Paletizador, "00"))) Then
                    If Trim(RsTemp("palet_" + Format(Paletizador, "00"))) > "" Then
                        MsgBox("Anote dos palets coincidentes.")
                        FlagRepetido = True
                    End If
                End If

                If FlagRepetido = False Then
                    RsTemp("palet_" + Format(Paletizador, "00")) = Rs!codigo_Palet
                End If

                RsTemp.Update()
            Next
            RsTemp.Close()
        Next
        Rs.Close()

        GrabarAuxiliar(Proceso, FechaProceso, "  LAZO PRINCIPAL")
        SQL = "SELECT * FROM PERSONAL_ES JOIN PERSONAL_PUESTOS ON PERSONAL_PUESTOS.EMPRESA = PERSONAL_ES.EMPRESA AND PERSONAL_PUESTOS.CODIGO_PUESTO = PERSONAL_ES.PUESTO "
        SQL = Trim(SQL) + " WHERE PERSONAL_ES.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND PERSONAL_ES.DIA_INICIAL_RED = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND PERSONAL_PUESTOS.TIPO_PUESTO IN('P','PI') AND apartado>0 AND subapartado>0 AND linea_informe>0 AND IMPORTE_COSTE>0"
        SQL = Trim(SQL) + " AND (PALETIZADOR01>0 OR PALETIZADOR02>0 OR PALETIZADOR03>0 OR PALETIZADOR04>0  OR PALETIZADOR05>0"
        SQL = Trim(SQL) + " OR PALETIZADOR06>0 OR PALETIZADOR07>0  OR PALETIZADOR08>0 OR PALETIZADOR08>0 OR PALETIZADOR10>0"
        SQL = Trim(SQL) + " OR PALETIZADOR11>0 OR PALETIZADOR12>0  OR PALETIZADOR13>0 OR PALETIZADOR14>0 OR PALETIZADOR15>0"
        SQL = Trim(SQL) + " OR PALETIZADOR16>0 OR PALETIZADOR17>0  OR PALETIZADOR18>0 OR PALETIZADOR19>0 OR PALETIZADOR20>0  OR PALETIZADOR21>0   OR PALETIZADOR22>0   OR PALETIZADOR23>0   OR PALETIZADOR24>0   OR PALETIZADOR25>0)"
        SQL = Trim(SQL) + " ORDER BY DIA_INICIAL_RED, HORA_INICIAL_RED"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()

            'GRABACION COSTES3 Y COSTES33 DE LOS COSTES DE PALETIZADOS ASIGNADOS A ALGÚN PALETIZADOR
            If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = 'PX' And apartado = " + CStr(Rs!apartado) + " And subapartado = " + CStr(Rs!subapartado) + " And linea_informe = " + CStr(Rs!linea_informe)
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes3")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!tipo_coste = "PX"
                    RsTemp!desde_variedad = ""
                    RsTemp!hasta_variedad = ""
                    RsTemp!apartado = Rs!apartado
                    RsTemp!subapartado = Rs!subapartado
                    RsTemp!linea_informe = Rs!linea_informe
                    RsTemp!importe_coste = 0
                End If
                RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                RsTemp.Update()
                RsTemp.Close()

                SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = " + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!puesto) + "' AND TIPO_COSTE = 'PX'"
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes33")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    RsTemp!Proceso = Proceso
                    RsTemp!Codigo_Operario = Rs!cod_operario
                    RsTemp!codigo_puesto = Rs!puesto
                    RsTemp!tipo_coste = "PX"
                    RsTemp!desde_variedad = ""
                    RsTemp!hasta_variedad = ""
                    RsTemp!apartado = Rs!apartado
                    RsTemp!subapartado = Rs!subapartado
                    RsTemp!linea_informe = Rs!linea_informe
                    RsTemp!importe_coste = 0
                End If
                RsTemp!importe_coste = RsTemp!importe_coste + Rs!importe_coste
                RsTemp.Update()
                RsTemp.Close()
            Else
                MsgBox("Se ha encontrado un coste de paletizado (Puesto:" + Trim(Rs!puesto) + ") no productivo. Se interrumpe el proceso.")
                Exit Function
            End If
        Next

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            Fecha = CDate(Rs!Dia_inicial_red)
            Hora = Mid(Rs!Hora_inicial_red, 1, 6) + "00"
            HoraFin = Mid(Rs!Hora_final_red, 1, 6) + "00"
            If Trim(Rs!tipo_puesto) = "P" Then
                ImporteNoRepartido = Rs!importe_coste
                ImporteIndustria = 0
            Else
                ImporteNoRepartido = Rs!importe_coste * Costes(45)
                ImporteIndustria = Rs!importe_coste - ImporteNoRepartido
            End If
            SQL = "SELECT * FROM TEMP_COSTES5 WHERE PROCESO = " + CStr(Proceso) + " AND FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "' AND HORA>='" + Trim(Hora) + "' AND HORA<'" + Trim(HoraFin) + "' order by FECHA,hora"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes5")
                Exit Function
            End If
            If RsTemp.RecordCount > 0 Then
                CosteMinuto = Rs!importe_coste / RsTemp.RecordCount
                If Trim(Rs!tipo_puesto) = "PI" Then
                    CosteMinuto = CosteMinuto * Costes(45)
                End If
                For j = 1 To RsTemp.RecordCount
                    RsTemp.AbsolutePosition = j
                    ContadorCRT = ContadorCRT + 1
                    Lbl09.Text = CStr(ContadorCRT)
                    Lbl09.Refresh()
                    For k = 0 To 25 : Kilos(k) = 0 : Next
                    For k = 1 To 25
                        If RsTemp("kilos_" + Format(k, "00")) > 0 And Rs("paletizador" + Format(k, "00")) > 0 Then
                            Kilos(k) = RsTemp("kilos_" + Format(k, "00"))
                            Kilos(0) = Kilos(0) + RsTemp("kilos_" + Format(k, "00"))
                        End If
                    Next
                    '            Test(1) = 0
                    '            Test(2) = 0
                    '            Test(3) = 0
                    '            Test(4) = 0
                    If Kilos(0) > 0 Then
                        SQL = "SELECT * FROM TEMP_COSTES6 WHERE PROCESO = " + CStr(Proceso) + " AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND HORA = '" + Format(CDate(RsTemp!Hora), "HH:mm:ss") + "' AND apartado = " + CStr(Rs!apartado) + " AND subapartado = " + CStr(Rs!subapartado) + " AND linea_informe = " + CStr(Rs!linea_informe)
                        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                            MsgBox("Error al abrir la tabla temp_costes6")
                            Exit Function
                        End If
                        If RsTemp2.RecordCount = 0 Then
                            RsTemp2.AddNew()
                            RsTemp2!Proceso = Proceso
                            RsTemp2!Fecha = FechaProceso
                            RsTemp2!Hora = Format(CDate(RsTemp!Hora), "HH:mm:ss")
                            RsTemp2!apartado = Rs!apartado
                            RsTemp2!subapartado = Rs!subapartado
                            RsTemp2!linea_informe = Rs!linea_informe
                            For k = 1 To 25
                                RsTemp2("coste_" + Format(k, "00")) = 0
                            Next
                        End If
                        For k = 1 To 25
                            If Kilos(k) > 0 Then
                                Coste = CosteMinuto * Kilos(k) / Kilos(0)
                                RsTemp2("coste_" + Format(k, "00")) = RsTemp2("coste_" + Format(k, "00")) + Coste
                                '                        Test(1) = Test(1) + Coste
                                '                        Test(3) = Test(3) + 1
                                ImporteNoRepartido = ImporteNoRepartido - Coste
                            End If
                        Next
                        RsTemp2.Update()
                        RsTemp2.Close()

                        '                        ' Ahora grabamos la tabla temp_costes66
                        SQL = "SELECT * FROM TEMP_COSTES66 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario =" + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!codigo_puesto) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND HORA = '" + Format(CDate(RsTemp!Hora), "HH:mm:ss") + "' AND apartado = " + CStr(Rs!apartado) + " AND subapartado = " + CStr(Rs!subapartado) + " AND linea_informe = " + CStr(Rs!linea_informe)
                        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                            MsgBox("Error al abrir la tabla temp_costes66")
                            Exit Function
                        End If
                        If RsTemp2.RecordCount = 0 Then
                            RsTemp2.AddNew()
                            RsTemp2!Proceso = Proceso
                            RsTemp2!Codigo_Operario = Rs!cod_operario
                            RsTemp2!codigo_puesto = Rs!codigo_puesto
                            RsTemp2!Fecha = FechaProceso
                            RsTemp2!Hora = Format(CDate(RsTemp!Hora), "HH:mm:ss")
                            RsTemp2!apartado = Rs!apartado
                            RsTemp2!subapartado = Rs!subapartado
                            RsTemp2!linea_informe = Rs!linea_informe
                            For k = 1 To 25
                                RsTemp2("coste_" + Format(k, "00")) = 0
                            Next
                        End If

                        For k = 1 To 25
                            If Kilos(k) > 0 Then
                                Coste = CosteMinuto * Kilos(k) / Kilos(0)
                                '                        Test(2) = Test(2) + Coste
                                '                        Test(4) = Test(4) + 1
                                RsTemp2("coste_" + Format(k, "00")) = RsTemp2("coste_" + Format(k, "00")) + Coste
                                '                        ImporteNoRepartido = ImporteNoRepartido - Coste
                            End If
                        Next
                        RsTemp2.Update()
                        RsTemp2.Close()

                        'SQL = "select isnull(cast(sum(coste_01+coste_02+coste_03+coste_04+coste_05+coste_06+coste_07+coste_08+coste_09+coste_10+"
                        'SQL = Trim(SQL) + "coste_11+coste_12+coste_13+coste_14+coste_15+coste_16+coste_17+coste_18+coste_19+coste_20+"
                        'SQL = Trim(SQL) + "coste_21+coste_22+coste_23+coste_24+coste_25) as decimal(13,2)),0) as xx"
                        'SQL = Trim(SQL) + " from temp_costes6  Where proceso = " + CStr(Proceso)
                        'If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        '    MsgBox("Error al abrir la tabla temp_costes6")
                        '    Exit Function
                        'End If
                        '                RsTemp2.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
                        '                Test(5) = RsTemp2!xx
                        '                RsTemp2.Close
                        '
                        '                SQL = "select isnull(cast(sum(coste_01+coste_02+coste_03+coste_04+coste_05+coste_06+coste_07+coste_08+coste_09+coste_10+"
                        '                SQL = Trim(SQL) + "coste_11+coste_12+coste_13+coste_14+coste_15+coste_16+coste_17+coste_18+coste_19+coste_20+"
                        '                SQL = Trim(SQL) + "coste_21+coste_22+coste_23+coste_24+coste_25) as decimal(13,2)),0) as xx"
                        '                SQL = Trim(SQL) + " from temp_costes66  Where proceso = " + CStr(proceso)
                        '                RsTemp2.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
                        '                Test(6) = RsTemp2!xx
                        '                RsTemp2.Close
                        '
                        '                If Abs(Test(5) - Test(6)) > 1 Then
                        '                    MsgBox("error"
                        '                End If
                        '                Print #1, i, j, Test(1), Test(2), Test(3), Test(4), Test(5), Test(6)

                        ' Fin grabación temp_costes66
                    End If
                Next
            Else
                '        MsgBox("Error en las horas"
            End If
            RsTemp.Close()
            If Math.Round(ImporteNoRepartido, 6) <> 0 Then
                SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = 'P' and apartado = " + CStr(Rs!apartado) + " AND subapartado = " + CStr(Rs!subapartado) + " AND linea_informe = " + CStr(Rs!linea_informe)
                'OJO: Tiene que ser 'P' aunque el coste original sea 'PI' porque esta es la parte de confección. La de industria ya la hemos restado
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes3")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!tipo_coste = "P"
                    RsTemp2!desde_variedad = ""
                    RsTemp2!hasta_variedad = ""
                    RsTemp2!apartado = Rs!apartado
                    RsTemp2!subapartado = Rs!subapartado
                    RsTemp2!linea_informe = Rs!linea_informe
                    RsTemp2!importe_coste = 0
                End If
                RsTemp2!importe_coste = RsTemp2!importe_coste + ImporteNoRepartido
                RsTemp2.Update()
                RsTemp2.Close()

                ' Ahora grabamos la tabla temp_costes33
                SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + "  AND codigo_operario =" + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!codigo_puesto) + "' AND TIPO_COSTE = 'P' and apartado = " + CStr(Rs!apartado) + " AND subapartado = " + CStr(Rs!subapartado) + " AND linea_informe = " + CStr(Rs!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes33")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!Codigo_Operario = Rs!cod_operario
                    RsTemp2!codigo_puesto = Rs!codigo_puesto
                    RsTemp2!tipo_coste = "P"
                    RsTemp2!desde_variedad = ""
                    RsTemp2!hasta_variedad = ""
                    RsTemp2!apartado = Rs!apartado
                    RsTemp2!subapartado = Rs!subapartado
                    RsTemp2!linea_informe = Rs!linea_informe
                    RsTemp2!importe_coste = 0
                End If
                RsTemp2!importe_coste = RsTemp2!importe_coste + ImporteNoRepartido
                RsTemp2.Update()
                RsTemp2.Close()

                ' Fin grabación temp_costes33
            End If

            If Math.Round(ImporteIndustria, 6) <> 0 Then
                SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE = 'I' and apartado = " + CStr(Rs!apartado) + " AND subapartado = " + CStr(Rs!subapartado) + " AND linea_informe = " + CStr(Rs!linea_informe)
                'OJO: Tiene que ser 'I' aunque el coste original sea 'PI' porque esta es la parte de industria. La de confección ya la hemos procesado
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes3")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!tipo_coste = "I"
                    RsTemp2!desde_variedad = ""
                    RsTemp2!hasta_variedad = ""
                    RsTemp2!apartado = Rs!apartado
                    RsTemp2!subapartado = Rs!subapartado
                    RsTemp2!linea_informe = Rs!linea_informe
                    RsTemp2!importe_coste = 0
                End If
                RsTemp2!importe_coste = RsTemp2!importe_coste + ImporteIndustria
                RsTemp2.Update()
                RsTemp2.Close()

                ' Ahora grabamos la tabla temp_costes33
                SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario =" + CStr(Rs!cod_operario) + " AND codigo_puesto = '" + Trim(Rs!codigo_puesto) + "' AND TIPO_COSTE = 'I' and apartado = " + CStr(Rs!apartado) + " AND subapartado = " + CStr(Rs!subapartado) + " AND linea_informe = " + CStr(Rs!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes33")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!Codigo_Operario = Rs!cod_operario
                    RsTemp2!codigo_puesto = Rs!codigo_puesto
                    RsTemp2!tipo_coste = "I"
                    RsTemp2!desde_variedad = ""
                    RsTemp2!hasta_variedad = ""
                    RsTemp2!apartado = Rs!apartado
                    RsTemp2!subapartado = Rs!subapartado
                    RsTemp2!linea_informe = Rs!linea_informe
                    RsTemp2!importe_coste = 0
                End If
                RsTemp2!importe_coste = RsTemp2!importe_coste + ImporteIndustria
                RsTemp2.Update()
                RsTemp2.Close()

                ' Fin grabación temp_costes33
            End If
        Next
        Rs.Close()

        GrabarAuxiliar(Proceso, FechaProceso, "  Graba 6 y 66")
        SQL = "SELECT * FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + " AND paletizador<=25 ORDER BY DESDE_FECHA,DESDE_HORA,PALETIZADOR"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        CosteTotal = 0

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            Fecha = CDate(Rs!desde_fecha)
            Hora = Mid(Rs!desde_hora, 1, 6) + "00"
            HoraFin = Mid(Rs!hasta_hora, 1, 6) + "00"
            Paletizador = Rs!paletizador

            SQL = "SELECT * FROM TEMP_COSTES6 WHERE PROCESO = " + CStr(Proceso) + " AND FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "' AND HORA>='" + Trim(Hora) + "' AND HORA<'" + Trim(HoraFin) + "' ORDER BY FECHA,HORA"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes6")
                Exit Function
            End If
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                ContadorCRT = ContadorCRT + 1
                Lbl09.Text = CStr(ContadorCRT)
                Lbl09.Refresh()

                If RsTemp("coste_" + Format(Paletizador, "00")) <= 0 Then
                    If RsTemp("coste_" + Format(Paletizador, "00")) < 0 Then
                        MsgBox("duplicado")
                    End If
                Else
                    Coste = RsTemp("coste_" + Format(Paletizador, "00"))
                    CosteTotal = CosteTotal + Coste
                    '           RsTemp.Fields("coste_" + Format(Paletizador, "00")) = -Coste
                    '           RsTemp.Update

                    SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " AND CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                    If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes4")
                        Exit Function
                    End If
                    If RsTemp2.RecordCount = 0 Then
                        RsTemp2.AddNew()
                        RsTemp2!Proceso = Proceso
                        RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                        RsTemp2!apartado = RsTemp!apartado
                        RsTemp2!subapartado = RsTemp!subapartado
                        RsTemp2!linea_informe = RsTemp!linea_informe
                        RsTemp2!importe_volcado = 0
                        RsTemp2!importe_general = 0
                        RsTemp2!importe_paletizado = 0
                        RsTemp2!importe_paletiz_g = 0
                        RsTemp2!importe_industria = 0
                        RsTemp2!importe_precalibre = 0
                    End If
                    RsTemp2!importe_paletizado = RsTemp2!importe_paletizado + Coste

                    'If Trim(RsTemp2!codigo_palet) = "301021608" And RsTemp2!apartado = 2 And RsTemp2!subapartado = 1 And RsTemp2!linea_informe = 2 Then
                    '    MsgBox(CStr(RsTemp2!importe_paletizado))
                    'End If


                    RsTemp2.Update()
                    RsTemp2.Close()
                End If
            Next
            RsTemp.Close()

            ' Ahora con las tablas temp_costes66 y temp_costes44
            SQL = "SELECT * FROM TEMP_COSTES66 WHERE PROCESO = " + CStr(Proceso) + " AND FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "' AND HORA>='" + Trim(Hora) + "' AND HORA<'" + Trim(HoraFin) + "' ORDER BY FECHA,HORA"
            If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla temp_costes66")
                Exit Function
            End If

            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                ContadorCRT = ContadorCRT + 1
                Lbl09.Text = CStr(ContadorCRT)
                Lbl09.Refresh()
                If RsTemp("coste_" + Format(Paletizador, "00")) <= 0 Then
                    If RsTemp("coste_" + Format(Paletizador, "00")) < 0 Then
                        MsgBox("duplicado")
                    End If
                Else
                    Coste = RsTemp("coste_" + Format(Paletizador, "00"))
                    CosteTotal = CosteTotal + Coste
                    '            RsTemp.Fields("coste_" + Format(Paletizador, "00")) = -Coste
                    '            RsTemp.Update

                    SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso) + "  AND codigo_operario =" + CStr(RsTemp!Codigo_Operario) + " AND codigo_puesto = '" + Trim(RsTemp!codigo_puesto) + "' AND CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                    If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes44")
                        Exit Function
                    End If
                    If RsTemp2.RecordCount = 0 Then
                        RsTemp2.AddNew()
                        RsTemp2!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsTemp2!Proceso = Proceso
                        RsTemp2!Codigo_Operario = RsTemp!Codigo_Operario
                        RsTemp2!codigo_puesto = RsTemp!codigo_puesto
                        RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                        RsTemp2!apartado = RsTemp!apartado
                        RsTemp2!subapartado = RsTemp!subapartado
                        RsTemp2!linea_informe = RsTemp!linea_informe
                        RsTemp2!importe_volcado = 0
                        RsTemp2!importe_general = 0
                        RsTemp2!importe_paletizado = 0
                        RsTemp2!importe_paletiz_g = 0
                        RsTemp2!importe_industria = 0
                        RsTemp2!importe_precalibre = 0
                        RsTemp2!importe_total = 0
                    End If
                    RsTemp2!importe_paletizado = RsTemp2!importe_paletizado + Coste
                    RsTemp2.Update()
                    RsTemp2.Close()
                End If
            Next
            RsTemp.Close()

            ' Fin de las tablas temp_costes66 y temp_costes44
        Next
        Rs.Close()
        GrabarAuxiliar(Proceso, FechaProceso, "  REDONDEO")
        SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes44")
            Exit Function
        End If
        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            RsTemp2!importe_paletizado = Math.Round(RsTemp2!importe_paletizado, 6)

            'If Trim(RsTemp2!codigo_palet) = "301021608" And RsTemp2!apartado = 2 And RsTemp2!subapartado = 1 And RsTemp2!linea_informe = 2 Then
            '    MsgBox(CStr(RsTemp2!importe_paletizado))
            'End If


            RsTemp2.Update()
        Next
        RsTemp2.Close()

        'Reparto de los costes de paletizado no asignados
        GrabarAuxiliar(Proceso, FechaProceso, "  LAZO NO ASIGNADOS")
        SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='P' and apartado>0 and subapartado>0 and linea_informe>0 order by apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes3")
            Exit Function
        End If

        SQL = "SELECT codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  AND paletizador<=25 group by codigo_palet,familia_variedad,peso_palet"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j

                SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " and CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes4")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                End If
                If Costes(50) > 0 Then RsTemp2!importe_paletiz_g = RsTemp2!importe_paletiz_g + ((RsTemp!importe_coste * Rs!peso_palet) / Costes(50))



                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        Rs.Close()
        RsTemp.Close()

        ' Ahora lo mismo pero de las tablas temp_costes33, temp_costes44
        GrabarAuxiliar(Proceso, FechaProceso, "  LAZO 33 44")
        SQL = "SELECT codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  AND paletizador<=25 group by codigo_palet,familia_variedad,peso_palet"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If

        SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='P' and apartado>0 and subapartado>0 and linea_informe>0 order by apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes33")
            Exit Function
        End If


        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario =" + CStr(RsTemp!Codigo_Operario) + " AND codigo_puesto = '" + Trim(RsTemp!codigo_puesto) + "' and CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes44")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp2!Proceso = Proceso
                    RsTemp2!Codigo_Operario = RsTemp!Codigo_Operario
                    RsTemp2!codigo_puesto = RsTemp!codigo_puesto
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                    RsTemp2!importe_total = 0
                End If
                If Costes(50) > 0 Then RsTemp2!importe_paletiz_g = RsTemp2!importe_paletiz_g + ((RsTemp!importe_coste * Rs!peso_palet) / Costes(50))
                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        Rs.Close()
        RsTemp.Close()
        ' Fin de las tablas temp_costes33, temp_costes44


        'Reparto de los costes de industria
        GrabarAuxiliar(Proceso, FechaProceso, "  INDUSTRIA")
        SQL = "SELECT * FROM TEMP_COSTES3 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='I' and apartado>0 and subapartado>0 and linea_informe>0 order by apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes3")
            Exit Function
        End If

        SQL = "SELECT codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  AND paletizador=30 group by codigo_palet,familia_variedad,peso_palet"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If

        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " and CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes4")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                End If
                If Costes(43) > 0 Then
                    RsTemp2!importe_industria = RsTemp2!importe_industria + ((RsTemp!importe_coste * Rs!peso_palet) / Costes(43))
                End If

                'If Trim(RsTemp2!codigo_palet) = "301021608" And RsTemp2!apartado = 2 And RsTemp2!subapartado = 1 And RsTemp2!linea_informe = 2 Then
                '    MsgBox(CStr(RsTemp2!importe_paletizado))
                'End If

                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        Rs.Close()
        RsTemp.Close()

        ' Ahora lo mismo pero de las tablas temp_costes33, temp_costes44
        GrabarAuxiliar(Proceso, FechaProceso, "  INDUSTRIA 33 44")
        SQL = "SELECT * FROM TEMP_COSTES33 WHERE PROCESO = " + CStr(Proceso) + " AND TIPO_COSTE='I' and apartado>0 and subapartado>0 and linea_informe>0 order by apartado,subapartado,linea_informe"
        If RsTemp.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes33")
            Exit Function
        End If
        SQL = "SELECT codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  AND paletizador=30 group by codigo_palet,familia_variedad,peso_palet"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            For j = 1 To RsTemp.RecordCount
                RsTemp.AbsolutePosition = j
                SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario =" + CStr(RsTemp!Codigo_Operario) + " AND codigo_puesto = '" + Trim(RsTemp!codigo_puesto) + "' and CODIGO_PALET ='" + Trim(Rs!codigo_Palet) + "' AND apartado = " + CStr(RsTemp!apartado) + " and subapartado = " + CStr(RsTemp!subapartado) + " and linea_informe = " + CStr(RsTemp!linea_informe)
                If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla temp_costes44")
                    Exit Function
                End If
                If RsTemp2.RecordCount = 0 Then
                    RsTemp2.AddNew()
                    RsTemp2!Proceso = Proceso
                    RsTemp2!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp2!Codigo_Operario = RsTemp!Codigo_Operario
                    RsTemp2!codigo_puesto = RsTemp!codigo_puesto
                    RsTemp2!codigo_Palet = Trim(Rs!codigo_Palet)
                    RsTemp2!apartado = RsTemp!apartado
                    RsTemp2!subapartado = RsTemp!subapartado
                    RsTemp2!linea_informe = RsTemp!linea_informe
                    RsTemp2!importe_volcado = 0
                    RsTemp2!importe_general = 0
                    RsTemp2!importe_paletizado = 0
                    RsTemp2!importe_paletiz_g = 0
                    RsTemp2!importe_industria = 0
                    RsTemp2!importe_precalibre = 0
                    RsTemp2!importe_total = 0
                End If
                If Costes(43) > 0 Then
                    RsTemp2!importe_industria = RsTemp2!importe_industria + ((RsTemp!importe_coste * Rs!peso_palet) / Costes(43))
                End If
                RsTemp2.Update()
                RsTemp2.Close()
            Next
        Next
        Rs.Close()
        RsTemp.Close()

        ' Fin tablas temp_costes33, temp_costes44
        GrabarAuxiliar(Proceso, FechaProceso, "  REDONDEO 2")
        SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes4")
            Exit Function
        End If

        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            RsTemp2!importe_paletiz_g = Math.Round(RsTemp2!importe_paletiz_g, 6)
            RsTemp2!importe_industria = Math.Round(RsTemp2!importe_industria, 6)
            RsTemp2.Update()
        Next
        RsTemp2.Close()

        ' Ahora lo mismo pero de las tabla temp_costes44
        SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso)
        If RsTemp2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_costes44")
            Exit Function
        End If
        For i = 1 To RsTemp2.RecordCount
            RsTemp2.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl09.Text = CStr(ContadorCRT)
            Lbl09.Refresh()
            RsTemp2!importe_paletiz_g = Math.Round(RsTemp2!importe_paletiz_g, 6)
            RsTemp2!importe_industria = Math.Round(RsTemp2!importe_industria, 6)
            RsTemp2.Update()
        Next
        RsTemp2.Close()
        ' Fin tabla temp_costes44

        RepartoCostesPaletizado = True
    End Function

    Private Function IncrementoCostesPrecalibrado() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsPalets As New cnRecordset.CnRecordset, RsCostes As New cnRecordset.CnRecordset
        Dim Mensaje As String
        Dim i As Integer, j As Integer, Importe As Double, PesoTotal As Double

        IncrementoCostesPrecalibrado = False
        Mensaje = Format(DateAdd("d", 1, FechaProceso), "dd/MM/yyyy")

        'Grabación previa de los costes de los bultos precalibrado fabricados hoy por si se volcaran también hoy
        SQL = "SELECT * FROM precalibre_costes pc WHERE  empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' and  exists (select empresa from palets_precalibre p where p.empresa = pc.empresa and P.EJERCICIO = pc.ejercicio and p.codigo_palet = pc.codigo_palet and p.fecha_confeccion = '" + Format(FechaProceso, "dd/MM/yyyy") + "')"
        BorrarDatos(SQL)

        SQL = "SELECT * FROM TEMP_COSTES4 JOIN TEMP_COSTES1 ON TEMP_COSTES1.PROCESO = TEMP_COSTES4.PROCESO AND TEMP_COSTES1.CODIGO_PALET = TEMP_COSTES4.CODIGO_PALET WHERE TEMP_COSTES4.PROCESO = " + CStr(Proceso) + " AND not TEMP_COSTES4.codigo_palet in ('1','2','3','4','5','6') AND  ({ fn LENGTH(temp_costes4.codigo_palet) } <= 7)"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes4")
            Exit Function
        End If

        SQL = "SELECT * FROM precalibre_COSTES WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla precalibre_costes")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            RsTemp.AddNew()
            RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
            RsTemp!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
            RsTemp!codigo_Palet = Trim(Rs!codigo_Palet)
            RsTemp!apartado = Rs!apartado
            RsTemp!subapartado = Rs!subapartado
            RsTemp!linea_informe = Rs!linea_informe
            RsTemp!importe_coste = Math.Round(Rs!importe_volcado + Rs!importe_general + Rs!importe_paletizado + Rs!importe_paletiz_g + Rs!importe_industria + Rs!importe_precalibre, 6)
            RsTemp.Update()
        Next
        RsTemp.Close()
        Rs.Close()


        'Incremento de los costes de los palets precalibrado volcados hoy sobre los palets comerciales confeccionados hoy
        SQL = "SELECT codigo_familia,apartado,subapartado,linea_informe,sum(importe_coste) as coste, isnull(v.codigo_subfamilia, v.codigo_familia) as vcodigo_familia "
        SQL = Trim(SQL) + " FROM PRECALIBRE_COSTES PC JOIN palets_precalibre P on p.empresa = pc.empresa and p.codigo_palet = pc.codigo_palet"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = P.EMPRESA AND V.CODIGO_VARIEDAD = P.CODIGO_VARIEDAD "
        SQL = Trim(SQL) + " WHERE PC.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "'"
        SQL = Trim(SQL) + " AND P.fecha_volcado BETWEEN '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND '" + Format(FechaProceso, "dd/MM/yyyy") + " 23:59:00'"
        SQL = Trim(SQL) + " GROUP BY v.codigo_subfamilia, v.codigo_familia, apartado,subapartado,linea_informe"
        If RsCostes.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla precalibre_costes")
            Exit Function
        End If

        For i = 1 To RsCostes.RecordCount
            RsCostes.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl10.Text = CStr(ContadorCRT)
            Lbl10.Refresh()
            SQL = "SELECT codigo_palet,familia_variedad,peso_palet FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + "  and familia_variedad = '" + Trim(RsCostes!vcodigo_familia) + "' group by codigo_palet,familia_variedad,peso_palet"
            If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla temp_costes1")
                Exit Function
            End If

            For j = 1 To RsPalets.RecordCount
                RsPalets.AbsolutePosition = j
                ContadorCRT = ContadorCRT + 1
                Lbl10.Text = CStr(ContadorCRT)
                Lbl10.Refresh()
                Importe = 0 : PesoTotal = 0
                If Trim(RsPalets!familia_variedad) = "NAR" Then
                    PesoTotal = Costes(21) - Costes(31) - Costes(37)
                ElseIf Trim(RsPalets!familia_variedad) = "CLE" Then
                    PesoTotal = Costes(22) - Costes(32) - Costes(38)
                ElseIf Trim(RsPalets!familia_variedad) = "HIB" Then
                    PesoTotal = Costes(23) - Costes(33) - Costes(39)
                ElseIf Trim(RsPalets!familia_variedad) = "SAT" Then
                    PesoTotal = Costes(24) - Costes(34) - Costes(40)
                ElseIf Trim(RsPalets!familia_variedad) = "NAB" Then
                    PesoTotal = Costes(25) - Costes(35) - Costes(41)
                ElseIf Trim(RsPalets!familia_variedad) = "ROJ" Then
                    PesoTotal = Costes(26) - Costes(36) - Costes(42)
                End If

                If PesoTotal > 0 Then
                    Importe = RsCostes!Coste * RsPalets!peso_palet / PesoTotal
                    SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + "  and codigo_palet = '" + Trim(RsPalets!codigo_Palet) + "' AND apartado = " + CStr(RsCostes!apartado) + " and subapartado = " + CStr(RsCostes!subapartado) + " and linea_informe = " + CStr(RsCostes!linea_informe)
                    If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes1")
                        Exit Function
                    End If
                    If Rs.RecordCount = 0 Then
                        Rs.AddNew()
                        Rs!Proceso = Proceso
                        Rs!codigo_Palet = Trim(RsPalets!codigo_Palet)
                        Rs!apartado = RsCostes!apartado
                        Rs!subapartado = RsCostes!subapartado
                        Rs!linea_informe = RsCostes!linea_informe
                        Rs!importe_volcado = 0
                        Rs!importe_general = 0
                        Rs!importe_paletizado = 0
                        Rs!importe_paletiz_g = 0
                        Rs!importe_industria = 0
                        Rs!importe_precalibre = Importe
                    Else
                        Rs!importe_precalibre = Rs!importe_precalibre + Importe
                    End If

                    'If Trim(Rs!codigo_palet) = "301021608" And Rs!apartado = 2 And Rs!subapartado = 1 And Rs!linea_informe = 2 Then
                    '    MsgBox(CStr(Rs!importe_paletizado))
                    'End If



                    Rs.Update()
                    Rs.Close()

                    ' Ahora la tabla temp_costes44
                    SQL = "SELECT * FROM TEMP_COSTES44 WHERE PROCESO = " + CStr(Proceso) + " AND codigo_operario = 99999 AND codigo_puesto = '999' and codigo_palet = '" + Trim(RsPalets!codigo_Palet) + "' AND apartado = " + CStr(RsCostes!apartado) + " and subapartado = " + CStr(RsCostes!subapartado) + " and linea_informe = " + CStr(RsCostes!linea_informe)
                    If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla temp_costes44")
                        Exit Function
                    End If
                    If Rs.RecordCount = 0 Then
                        Rs.AddNew()
                        Rs!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                        Rs!Proceso = Proceso
                        Rs!Codigo_Operario = 99999
                        Rs!codigo_puesto = "999"
                        Rs!codigo_Palet = Trim(RsPalets!codigo_Palet)
                        Rs!apartado = RsCostes!apartado
                        Rs!subapartado = RsCostes!subapartado
                        Rs!linea_informe = RsCostes!linea_informe
                        Rs!importe_volcado = 0
                        Rs!importe_general = 0
                        Rs!importe_paletizado = 0
                        Rs!importe_paletiz_g = 0
                        Rs!importe_industria = 0
                        Rs!importe_precalibre = Importe
                        Rs!importe_total = 0
                    Else
                        Rs!importe_precalibre = Rs!importe_precalibre + Importe
                    End If
                    Rs.Update()
                    Rs.Close()
                    ' Fin de temp_costes44
                End If
            Next
            RsPalets.Close()
        Next
        RsCostes.Close()

        IncrementoCostesPrecalibrado = True
    End Function

    Private Function GrabacionCostesPalet() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsPalets As New cnRecordset.CnRecordset ', Mensaje As String
        Dim i As Integer, j As Integer

        GrabacionCostesPalet = False

        'Palets confección
        SQL = "SELECT p.*,v.* FROM palets P JOIN ORDENES_CONFECCION O on o.empresa = p.empresa and o.numero_orden = p.orden_confeccion JOIN VARIEDADES V ON V.EMPRESA = P.EMPRESA AND V.CODIGO_VARIEDAD = O.CODIGO_VARIEDAD WHERE  P.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and P.fecha_confeccion = '" + Format(FechaProceso, "dd/MM/yyyy") + "' and o.codigo_variedad between '01' and '02z' and tipo_fabricacion = 'N'"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes44")
            Exit Function
        End If

        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl10.Text = CStr(ContadorCRT)
            Lbl10.Refresh()
            SQL = "SELECT * FROM PALETS_COSTES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_PALET = " + Trim(RsPalets!codigo_Palet)
            BorrarDatos(SQL)
        Next
        RsPalets.Close()

        SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " AND ({ fn LENGTH(codigo_palet) } > 7)"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes4")
            Exit Function
        End If

        SQL = "SELECT * FROM PALETS_COSTES WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla palets_costes")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl10.Text = CStr(ContadorCRT)
            Lbl10.Refresh()
            RsTemp.AddNew()
            RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
            RsTemp!codigo_Palet = Trim(Rs!codigo_Palet)
            RsTemp!apartado = Rs!apartado
            RsTemp!subapartado = Rs!subapartado
            RsTemp!linea_informe = Rs!linea_informe
            RsTemp!importe_coste = Math.Round(Rs!importe_volcado + Rs!importe_general + Rs!importe_paletizado + Rs!importe_paletiz_g + Rs!importe_industria + Rs!importe_precalibre, 6)
            RsTemp.Update()
        Next
        RsTemp.Close()
        Rs.Close()

        'Bultos industria
        SQL = "SELECT * FROM industria_costes WHERE  empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and fecha = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        BorrarDatos(SQL)

        SQL = "SELECT * FROM TEMP_COSTES4 JOIN TEMP_COSTES1 ON TEMP_COSTES1.PROCESO = TEMP_COSTES4.PROCESO AND TEMP_COSTES1.CODIGO_PALET = TEMP_COSTES4.CODIGO_PALET WHERE TEMP_COSTES4.PROCESO = " + CStr(Proceso) + " AND TEMP_COSTES4.codigo_palet in ('1','2','3','4','5','6')"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes4")
            Exit Function
        End If

        SQL = "SELECT * FROM INDUSTRIA_COSTES WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla industria_costes")
            Exit Function
        End If
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            RsTemp.AddNew()
            RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
            RsTemp!Fecha = FechaProceso
            '    RsTemp!codigo_palet = Trim(Rs!codigo_palet)
            RsTemp!familia_variedad = Trim(Rs!familia_variedad)
            RsTemp!apartado = Rs!apartado
            RsTemp!subapartado = Rs!subapartado
            RsTemp!linea_informe = Rs!linea_informe
            RsTemp!importe_coste = Math.Round(Rs!importe_volcado + Rs!importe_general + Rs!importe_paletizado + Rs!importe_paletiz_g + Rs!importe_industria + Rs!importe_precalibre, 2)
            RsTemp.Update()
        Next
        RsTemp.Close()
        Rs.Close()

        'Los costes de los bultos precalibrado confeccionados hoy se han grabado previamente
        'SQL = "SELECT * FROM precalibre_costes pc WHERE  empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' and  exists (select empresa from palets_precalibre p where p.empresa = pc.empresa and P.EJERCICIO = pc.ejercicio and p.codigo_palet = pc.codigo_palet and p.fecha_confeccion = '" + Format(Fechaproceso, "dd/MM/yyyy") + "')"
        'RsPalets.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockOptimistic
        'For i = 1 To RsPalets.RecordCount
        '    RsPalets.MoveFirst
        '    RsPalets.Delete
        'Next
        'RsPalets.Close
        '
        'SQL = "SELECT * FROM TEMP_COSTES4 JOIN TEMP_COSTES1 ON TEMP_COSTES1.PROCESO = TEMP_COSTES4.PROCESO AND TEMP_COSTES1.CODIGO_PALET = TEMP_COSTES4.CODIGO_PALET WHERE TEMP_COSTES4.PROCESO = " + CStr(proceso) + " AND not TEMP_COSTES4.codigo_palet in ('1','2','3','4','5','6') AND  ({ fn LENGTH(temp_costes4.codigo_palet) } <= 7)"
        'Rs.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        'SQL = "SELECT * FROM precalibre_COSTES WHERE 1=0"
        'RsTemp.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockOptimistic
        'For i = 1 To Rs.RecordCount
        '    Rs.AbsolutePosition = i
        '    RsTemp.AddNew
        '    RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
        '    RsTemp!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
        '    RsTemp!codigo_palet = Trim(Rs!codigo_palet)
        '    RsTemp!apartado = Rs!apartado
        '    RsTemp!subapartado = Rs!subapartado
        '    RsTemp!linea_informe = Rs!linea_informe
        '    RsTemp!importe_coste =math.round(Rs!importe_volcado + Rs!importe_general + Rs!importe_paletizado + Rs!importe_paletiz_g + Rs!importe_industria + Rs!importe_precalibre, 6)
        '    RsTemp.Update
        'Next
        'RsTemp.Close
        'Rs.Close

        GrabacionCostesPalet = True
    End Function

    Private Sub Borraranteriores()
        Dim SQL As String
        SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        BorrarDatos(SQL)

        SQL = "SELECT * FROM h_costes_DETALLE WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        BorrarDatos(SQL)
    End Sub

    Private Function CalcularCostes() As Boolean
        Dim RsPalets As New cnRecordset.CnRecordset, RsTemp As New cnRecordset.CnRecordset, RsCostes As New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Integer, j As Integer
        Dim Peso As Long, Fecha As Date
        Dim Cadena As String

        CalcularCostes = False
        SQL = "SELECT PALETS.*,ORDENES_CONFECCION.NUMERO_ORDEN,ORDENES_CONFECCION.CODIGO_CONFECCION,ORDENES_CONFECCION.PESO_PALET AS PESO_CONFECCION,ORDENES_CONFECCION.PALETIZACION,VARIEDADES.CODIGO_VARIEDAD,ISNULL(VARIEDADES.CODIGO_SUBFAMILIA,VARIEDADES.CODIGO_FAMILIA) as CODIGO_FAMILIA,SUBGRUPOS_CONFECCION.GRUPO_CONFECCION,S2.GRUPO_CONFECCION AS GRUPO_PALETIZACION FROM PALETS"
        SQL = Trim(SQL) + " LEFT JOIN ORDENES_CONFECCION ON ORDENES_CONFECCION.EMPRESA = PALETS.EMPRESA AND ORDENES_CONFECCION.NUMERO_ORDEN = PALETS.ORDEN_CONFECCION"
        SQL = Trim(SQL) + " LEFT JOIN VARIEDADES ON VARIEDADES.EMPRESA = ORDENES_CONFECCION.EMPRESA AND VARIEDADES.CODIGO_VARIEDAD = ORDENES_CONFECCION.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " LEFT JOIN CONFECCION ON CONFECCION.EMPRESA = ORDENES_CONFECCION.EMPRESA AND CONFECCION.CODIGO_CONFECCION = ORDENES_CONFECCION.CODIGO_CONFECCION"
        SQL = Trim(SQL) + " LEFT JOIN SUBGRUPOS_CONFECCION ON SUBGRUPOS_CONFECCION.EMPRESA = CONFECCION.EMPRESA AND SUBGRUPOS_CONFECCION.CODIGO_SUBGRUPO = CONFECCION.SUBGRUPO_CONFECCION"
        SQL = Trim(SQL) + " LEFT JOIN CONFECCION C2 ON C2.EMPRESA = ORDENES_CONFECCION.EMPRESA AND C2.CODIGO_CONFECCION = ORDENES_CONFECCION.PALETIZACION"
        SQL = Trim(SQL) + " LEFT JOIN SUBGRUPOS_CONFECCION S2 ON S2.EMPRESA = C2.EMPRESA AND S2.CODIGO_SUBGRUPO = C2.SUBGRUPO_CONFECCION"
        SQL = Trim(SQL) + " WHERE PALETS.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND PALETS.FECHA_CONFECCION = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND ISNULL(VARIEDADES.CODIGO_SUBFAMILIA,VARIEDADES.CODIGO_FAMILIA) IN ('" + Trim(Familia) + "','" + Trim(Familia) + "E') "
        SQL = Trim(SQL) + " AND (PALETS.TIPO_FABRICACION = 'N' OR EXISTS (SELECT REPALETIZACION.EMPRESA FROM REPALETIZACION WHERE REPALETIZACION.EMPRESA = PALETS.EMPRESA AND REPALETIZACION.PALET_FINAL = PALETS.CODIGO_PALET AND REPALETIZACION.TIPO_REPALETIZ = 'P'))"
        SQL = Trim(SQL) + " ORDER BY PALETS.FECHA_CONFECCION,SUBGRUPOS_CONFECCION.GRUPO_CONFECCION,PALETS.CODIGO_PALET"

        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets")
            Exit Function
        End If
        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()
            If IsDBNull(RsPalets!numero_orden) Then
                MsgBox("El palet " + Trim(RsPalets!codigo_Palet) + " no tiene orden de confección correcta." + vbCrLf + "Se interrumpe el proceso.")
                Exit Function
            End If
            If IsDBNull(RsPalets!grupo_confeccion) Or Trim(RsPalets!grupo_confeccion) = "" Then
                MsgBox("El palet " + Trim(RsPalets!codigo_Palet) + " no tiene asociado grupo de confección." + vbCrLf + "Se interrumpe el proceso.")
                Exit Function
            End If
            If IsDBNull(RsPalets!codigo_familia) Or Trim(RsPalets!codigo_familia) = "" Then
                MsgBox("El palet " + Trim(RsPalets!codigo_Palet) + " no tiene asociada familia para la variedad." + vbCrLf + "Se interrumpe el proceso.")
                Exit Function
            End If
            Peso = 0
            If Not IsDBNull(RsPalets!peso_palet) Then
                If RsPalets!peso_palet > 0 Then Peso = RsPalets!peso_palet
            End If
            If Peso = 0 Then
                If Not IsDBNull(RsPalets!peso_confeccion) Then
                    Peso = RsPalets!peso_confeccion
                End If
            End If
            If Peso = 0 Then
                MsgBox("El palet " + Trim(RsPalets!codigo_Palet) + " no tiene grabado peso (ni tampoco su orden de confección)." + vbCrLf + "Se interrumpe el proceso.")
                Exit Function
            End If
        Next

        SQL = "SELECT * FROM h_costes_FABRICACION WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_fabricacion")
            Exit Function
        End If

        RsTemp.AddNew()
        RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
        'RsTemp!proceso = proceso
        RsTemp!Contador = 0
        RsTemp!Fecha = FechaProceso
        RsTemp!familia_variedad = Trim(Familia)
        RsTemp!tipo_coste = "0"

        RsTemp!importe_fijo = 0
        RsTemp!kilos = 0
        RsTemp!importe_mo = 0
        RsTemp!importe_confeccion = 0
        RsTemp!importe_paletizacion = 0
        RsTemp!importe_envase = 0
        RsTemp!importe_total = 0
        RsTemp!centimos_kg = 0
        RsTemp!palets = 0
        RsTemp!suma_ptos_kilos = 0

        RsTemp.Update()
        RsTemp.Close()

        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            Peso = 0
            If Not IsDBNull(RsPalets!peso_palet) Then
                If RsPalets!peso_palet > 0 Then Peso = RsPalets!peso_palet
            End If
            If Peso = 0 Then
                If Not IsDBNull(RsPalets!peso_confeccion) Then
                    Peso = RsPalets!peso_confeccion
                End If
            End If
            IncluirPalet(RsPalets, Peso)
        Next
        RsPalets.Close()

        SQL = "SELECT * FROM h_costes_FABRICACION WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_confeccion")
            Exit Function
        End If

        SQL = "SELECT * FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + " AND PALETIZADOR = 30"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets")
            Exit Function
        End If
        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            If Trim(RsPalets!familia_variedad) = Trim(Familia) Then
                RsTemp.AddNew()
                RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                ContadorGeneral = ContadorGeneral + 1
                RsTemp!Contador = ContadorGeneral
                RsTemp!Fecha = FechaProceso
                RsTemp!familia_variedad = Trim(Familia)
                RsTemp!Codigo_Variedad = Trim(VariedadGeneral)
                RsTemp!grupo_confeccion = "D"
                RsTemp!codigo_confeccion = "0"
                RsTemp!tipo_coste = "1"
                RsTemp!Kilos = RsPalets!peso_palet

                RsTemp!importe_fijo = 0
                RsTemp!importe_mo = 0
                RsTemp!importe_confeccion = 0
                RsTemp!importe_paletizacion = 0
                RsTemp!importe_envase = 0
                RsTemp!importe_total = 0
                RsTemp!centimos_kg = 0
                RsTemp!palets = 0
                RsTemp!suma_ptos_kilos = 0

                RsTemp.Update()
                Cadena = Format(FechaProceso, "yyyyMMdd") + "0"
                CuantasConfecciones = CuantasConfecciones + 1
                ReDim Preserve Confecciones(1, CuantasConfecciones)
                Confecciones(0, CuantasConfecciones) = Trim(Cadena)
                Confecciones(1, CuantasConfecciones) = "0"
            End If
        Next
        RsTemp.Close()
        RsPalets.Close()

        SQL = "SELECT * FROM h_costes_FABRICACION WHERE 1=0"
        If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_confeccion")
            Exit Function
        End If

        SQL = "SELECT isnull(sum(peso_palet),0) as peso FROM TEMP_COSTES1 WHERE PROCESO = " + CStr(Proceso) + " AND PALETIZADOR = 31 and familia_variedad = '" + Trim(Familia) + "'"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If

        ContadorCRT = ContadorCRT + 1
        Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
        Lbl11.Refresh()

        RsTemp.AddNew()
        RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
        ContadorGeneral = ContadorGeneral + 1
        RsTemp!Contador = ContadorGeneral
        RsTemp!Fecha = FechaProceso
        RsTemp!familia_variedad = Trim(Familia)
        RsTemp!Codigo_Variedad = Trim(VariedadGeneral)
        RsTemp!grupo_confeccion = "PRECL"
        RsTemp!codigo_confeccion = "P"
        RsTemp!tipo_coste = "1"
        RsTemp!Kilos = RsPalets!Peso

        RsTemp!importe_fijo = 0
        RsTemp!importe_mo = 0
        RsTemp!importe_confeccion = 0
        RsTemp!importe_paletizacion = 0
        RsTemp!importe_envase = 0
        RsTemp!importe_total = 0
        RsTemp!centimos_kg = 0
        RsTemp!palets = 0
        RsTemp!suma_ptos_kilos = 0

        RsTemp.Update()
        Cadena = Format(Fecha, "yyyyMMdd") + "P"
        CuantasConfecciones = CuantasConfecciones + 1
        ReDim Preserve Confecciones(1, CuantasConfecciones)
        Confecciones(0, CuantasConfecciones) = Trim(Cadena)
        Confecciones(1, CuantasConfecciones) = "P"
        RsTemp.Close()
        RsPalets.Close()

        SQL = "SELECT PALETS.*,ORDENES_CONFECCION.NUMERO_ORDEN,ORDENES_CONFECCION.CODIGO_CONFECCION,ORDENES_CONFECCION.PESO_PALET AS PESO_CONFECCION,ORDENES_CONFECCION.PALETIZACION,VARIEDADES.CODIGO_VARIEDAD,ISNULL(VARIEDADES.CODIGO_SUBFAMILIA,VARIEDADES.CODIGO_FAMILIA) as codigo_familia,SUBGRUPOS_CONFECCION.GRUPO_CONFECCION,S2.GRUPO_CONFECCION AS GRUPO_PALETIZACION FROM PALETS"
        SQL = Trim(SQL) + " LEFT JOIN ORDENES_CONFECCION ON ORDENES_CONFECCION.EMPRESA = PALETS.EMPRESA AND ORDENES_CONFECCION.NUMERO_ORDEN = PALETS.ORDEN_CONFECCION"
        SQL = Trim(SQL) + " LEFT JOIN VARIEDADES ON VARIEDADES.EMPRESA = ORDENES_CONFECCION.EMPRESA AND VARIEDADES.CODIGO_VARIEDAD = ORDENES_CONFECCION.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " LEFT JOIN CONFECCION ON CONFECCION.EMPRESA = ORDENES_CONFECCION.EMPRESA AND CONFECCION.CODIGO_CONFECCION = ORDENES_CONFECCION.CODIGO_CONFECCION"
        SQL = Trim(SQL) + " LEFT JOIN SUBGRUPOS_CONFECCION ON SUBGRUPOS_CONFECCION.EMPRESA = CONFECCION.EMPRESA AND SUBGRUPOS_CONFECCION.CODIGO_SUBGRUPO = CONFECCION.SUBGRUPO_CONFECCION"
        SQL = Trim(SQL) + " LEFT JOIN CONFECCION C2 ON C2.EMPRESA = ORDENES_CONFECCION.EMPRESA AND C2.CODIGO_CONFECCION = ORDENES_CONFECCION.PALETIZACION"
        SQL = Trim(SQL) + " LEFT JOIN SUBGRUPOS_CONFECCION S2 ON S2.EMPRESA = C2.EMPRESA AND S2.CODIGO_SUBGRUPO = C2.SUBGRUPO_CONFECCION"
        SQL = Trim(SQL) + " WHERE PALETS.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND PALETS.FECHA_CONFECCION = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND ISNULL(VARIEDADES.CODIGO_SUBFAMILIA,VARIEDADES.CODIGO_FAMILIA) IN ('" + Trim(Familia) + "','" + Trim(Familia) + "E') "
        SQL = Trim(SQL) + " AND (PALETS.TIPO_FABRICACION = 'N' OR EXISTS (SELECT REPALETIZACION.EMPRESA FROM REPALETIZACION WHERE REPALETIZACION.EMPRESA = PALETS.EMPRESA AND REPALETIZACION.PALET_FINAL = PALETS.CODIGO_PALET AND REPALETIZACION.TIPO_REPALETIZ = 'P'))"
        SQL = Trim(SQL) + " ORDER BY PALETS.FECHA_CONFECCION,SUBGRUPOS_CONFECCION.GRUPO_CONFECCION,PALETS.CODIGO_PALET"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla palets")
            Exit Function
        End If
        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " AND CODIGO_PALET = '" + Trim(RsPalets!codigo_Palet) + "'"
            If RsCostes.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla temp_costes4")
                Exit Function
            End If
            For j = 1 To RsCostes.RecordCount
                RsCostes.AbsolutePosition = j
                SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND CODIGO_VARIEDAD = '" + Trim(RsPalets!Codigo_Variedad) + "' AND CODIGO_CONFECCION = '" + Trim(RsPalets!codigo_confeccion) + "' AND TIPO_COSTE = '2' AND APARTADO = " + CStr(RsCostes!apartado) + " AND SUBAPARTADO = " + CStr(RsCostes!subapartado) + " AND LINEA_INFORME = " + CStr(RsCostes!linea_informe)
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla h_costes_fabricacion")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    ContadorGeneral = ContadorGeneral + 1
                    RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp!Contador = ContadorGeneral
                    RsTemp!Fecha = FechaProceso
                    RsTemp!familia_variedad = Trim(Familia)
                    RsTemp!Codigo_Variedad = Trim(RsPalets!Codigo_Variedad)
                    RsTemp!grupo_confeccion = "" 'OJO
                    RsTemp!codigo_confeccion = RsPalets!codigo_confeccion
                    RsTemp!tipo_coste = "2"
                    RsTemp!apartado = RsCostes!apartado
                    RsTemp!subapartado = RsCostes!subapartado
                    RsTemp!linea_informe = RsCostes!linea_informe
                    RsTemp!importe_mo = RsCostes!importe_volcado + RsCostes!importe_general + RsCostes!importe_paletizado + RsCostes!importe_paletiz_g + RsCostes!importe_industria + RsCostes!importe_precalibre

                    RsTemp!importe_fijo = 0
                    RsTemp!kilos = 0
                    RsTemp!importe_confeccion = 0
                    RsTemp!importe_paletizacion = 0
                    RsTemp!importe_envase = 0
                    RsTemp!importe_total = 0
                    RsTemp!centimos_kg = 0
                    RsTemp!palets = 0
                    RsTemp!suma_ptos_kilos = 0

                Else
                    RsTemp!importe_mo = RsTemp!importe_mo + RsCostes!importe_volcado + RsCostes!importe_general + RsCostes!importe_paletizado + RsCostes!importe_paletiz_g + RsCostes!importe_industria + RsCostes!importe_precalibre
                End If
                RsTemp.Update()
                RsTemp.Close()
            Next
            RsCostes.Close()
        Next
        RsPalets.Close()

        SQL = "SELECT * FROM TEMP_COSTES1 WHERE TEMP_COSTES1.PROCESO = " + CStr(Proceso) + " AND TEMP_COSTES1.PALETIZADOR = 30 AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "'"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " AND CODIGO_PALET = '" + Trim(RsPalets!codigo_Palet) + "'"
            If RsCostes.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla temp_costes4")
                Exit Function
            End If
            For j = 1 To RsCostes.RecordCount
                RsCostes.AbsolutePosition = j
                SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND CODIGO_VARIEDAD = '" + Trim(VariedadGeneral) + "' AND CODIGO_CONFECCION = '" + Trim(RsPalets!codigo_confeccion) + "' AND TIPO_COSTE = '2' AND APARTADO = " + CStr(RsCostes!apartado) + " AND SUBAPARTADO = " + CStr(RsCostes!subapartado) + " AND LINEA_INFORME = " + CStr(RsCostes!linea_informe)
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla h_costes_fabricacion")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    ContadorGeneral = ContadorGeneral + 1
                    RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp!Contador = ContadorGeneral
                    RsTemp!Fecha = FechaProceso
                    RsTemp!familia_variedad = Trim(Familia)
                    RsTemp!Codigo_Variedad = Trim(VariedadGeneral)
                    RsTemp!grupo_confeccion = "" 'OJO
                    RsTemp!codigo_confeccion = "0"    'RsPalets!codigo_confeccion
                    RsTemp!tipo_coste = "2"
                    RsTemp!apartado = RsCostes!apartado
                    RsTemp!subapartado = RsCostes!subapartado
                    RsTemp!linea_informe = RsCostes!linea_informe
                    RsTemp!importe_mo = RsCostes!importe_volcado + RsCostes!importe_general + RsCostes!importe_paletizado + RsCostes!importe_paletiz_g + RsCostes!importe_industria + RsCostes!importe_precalibre

                    RsTemp!importe_fijo = 0
                    RsTemp!kilos = 0
                    RsTemp!importe_confeccion = 0
                    RsTemp!importe_paletizacion = 0
                    RsTemp!importe_envase = 0
                    RsTemp!importe_total = 0
                    RsTemp!centimos_kg = 0
                    RsTemp!palets = 0
                    RsTemp!suma_ptos_kilos = 0

                Else
                    RsTemp!importe_mo = RsTemp!importe_mo + RsCostes!importe_volcado + RsCostes!importe_general + RsCostes!importe_paletizado + RsCostes!importe_paletiz_g + RsCostes!importe_industria + RsCostes!importe_precalibre
                End If
                RsTemp.Update()
                RsTemp.Close()
            Next
            RsCostes.Close()
        Next
        RsPalets.Close()

        SQL = "SELECT * FROM TEMP_COSTES1 WHERE TEMP_COSTES1.PROCESO = " + CStr(Proceso) + " AND TEMP_COSTES1.PALETIZADOR = 31 AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "'"
        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes1")
            Exit Function
        End If
        For i = 1 To RsPalets.RecordCount
            RsPalets.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            SQL = "SELECT * FROM TEMP_COSTES4 WHERE PROCESO = " + CStr(Proceso) + " AND CODIGO_PALET = '" + Trim(RsPalets!codigo_Palet) + "'"
            If RsCostes.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla temp_costes4")
                Exit Function
            End If
            For j = 1 To RsCostes.RecordCount
                RsCostes.AbsolutePosition = j
                SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND CODIGO_VARIEDAD = '" + Trim(VariedadGeneral) + "' AND CODIGO_CONFECCION = '" + Trim(RsPalets!codigo_confeccion) + "' AND TIPO_COSTE = '2' AND APARTADO = " + CStr(RsCostes!apartado) + " AND SUBAPARTADO = " + CStr(RsCostes!subapartado) + " AND LINEA_INFORME = " + CStr(RsCostes!linea_informe)
                If RsTemp.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla h_costes_fabricacion")
                    Exit Function
                End If
                If RsTemp.RecordCount = 0 Then
                    RsTemp.AddNew()
                    ContadorGeneral = ContadorGeneral + 1
                    RsTemp!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsTemp!Contador = ContadorGeneral
                    RsTemp!Fecha = FechaProceso
                    RsTemp!familia_variedad = Trim(Familia)
                    RsTemp!Codigo_Variedad = Trim(VariedadGeneral)
                    RsTemp!grupo_confeccion = "" 'OJO
                    RsTemp!codigo_confeccion = "P"    'RsPalets!codigo_confeccion
                    RsTemp!tipo_coste = "2"
                    RsTemp!apartado = RsCostes!apartado
                    RsTemp!subapartado = RsCostes!subapartado
                    RsTemp!linea_informe = RsCostes!linea_informe
                    RsTemp!importe_mo = RsCostes!importe_volcado + RsCostes!importe_general + RsCostes!importe_paletizado + RsCostes!importe_paletiz_g + RsCostes!importe_industria + RsCostes!importe_precalibre

                    RsTemp!importe_fijo = 0
                    RsTemp!kilos = 0
                    RsTemp!importe_confeccion = 0
                    RsTemp!importe_paletizacion = 0
                    RsTemp!importe_envase = 0
                    RsTemp!importe_total = 0
                    RsTemp!centimos_kg = 0
                    RsTemp!palets = 0
                    RsTemp!suma_ptos_kilos = 0

                Else
                    RsTemp!importe_mo = RsTemp!importe_mo + RsCostes!importe_volcado + RsCostes!importe_general + RsCostes!importe_paletizado + RsCostes!importe_paletiz_g + RsCostes!importe_industria + RsCostes!importe_precalibre
                End If
                RsTemp.Update()
                RsTemp.Close()
            Next
            RsCostes.Close()
        Next
        CalcularCostes = True
    End Function

    Private Function GrabarLineasInforme() As Boolean
        Dim RsLineas As New cnRecordset.CnRecordset, RsTemporal As New cnRecordset.CnRecordset, RsAuxiliar As New cnRecordset.CnRecordset
        Dim SQL As String, i As Integer, j As Integer, Cadena As String, Cadena2 As String

        GrabarLineasInforme = False
        SQL = "SELECT * FROM GRUPOS_COSTE_INFORME WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND INFORME = 0 ORDER BY APARTADO,SUBAPARTADO,LINEA_INFORME"
        If RsLineas.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla grupos_coste_informe")
            Exit Function
        End If

        SQL = "SELECT * FROM h_costes_FABRICACION WHERE 1=0"
        If RsTemporal.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_fabricacion")
            Exit Function
        End If

        For i = 1 To RsLineas.RecordCount
            RsLineas.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            If Trim(RsLineas!solo_texto) <> "S" Then
                For j = 0 To CuantasConfecciones
                    Cadena = Mid(Confecciones(0, j), 1, 8)
                    Cadena2 = Trim(Mid(Confecciones(0, j), 9))
                    RsTemporal.AddNew()
                    RsTemporal!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    'RsTemporal!proceso = proceso
                    ContadorGeneral = ContadorGeneral + 1
                    RsTemporal!Contador = ContadorGeneral
                    RsTemporal!Fecha = FechaProceso
                    RsTemporal!familia_variedad = Trim(Familia)
                    RsTemporal!codigo_confeccion = Trim(Cadena2)
                    RsTemporal!grupo_confeccion = Trim(Confecciones(1, j))
                    RsTemporal!tipo_coste = "3"
                    RsTemporal!apartado = RsLineas!apartado
                    RsTemporal!subapartado = RsLineas!subapartado
                    RsTemporal!linea_informe = RsLineas!linea_informe
                    RsTemporal!grupo_coste = Trim(RsLineas!codigo_grupo)

                    RsTemporal!importe_fijo = 0
                    RsTemporal!kilos = 0
                    RsTemporal!importe_mo = 0
                    RsTemporal!importe_confeccion = 0
                    RsTemporal!importe_paletizacion = 0
                    RsTemporal!importe_envase = 0
                    RsTemporal!importe_total = 0
                    RsTemporal!centimos_kg = 0
                    RsTemporal!palets = 0
                    RsTemporal!suma_ptos_kilos = 0

                    RsTemporal.Update()
                Next
            End If
        Next
        GrabarLineasInforme = True
    End Function

    Private Function CalcularLineasInforme() As Boolean
        Dim RsLineas As New cnRecordset.CnRecordset, RsAuxiliar As New cnRecordset.CnRecordset, RsTemporal As New cnRecordset.CnRecordset
        Dim RsPuntos As New cnRecordset.CnRecordset
        Dim SQL As String, i As Integer, j As Integer, jj As Integer
        Dim Cadena As String ', Cadena2 As String
        Dim RsEspecial As New cnRecordset.CnRecordset ', Coeficiente As Double
        Dim Busquedas(6) As String, DatosTabla(5) As Double

        CalcularLineasInforme = False
        SQL = "SELECT * FROM TEMP_ERR_COSTES_FABRICACION WHERE 1=0"
        If RsEspecial.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_err_costes_fabricacion")
            Exit Function
        End If

        SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND TIPO_COSTE = '3' ORDER BY FECHA,APARTADO,SUBAPARTADO,LINEA_INFORME"
        If RsLineas.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_fabricacion")
            Exit Function
        End If

        For i = 1 To RsLineas.RecordCount
            RsLineas.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()
            SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND TIPO_COSTE = '1' AND FECHA = '" + Format(RsLineas!Fecha, "dd/MM/yyyy") + "' AND CODIGO_CONFECCION = '" + Trim(RsLineas!codigo_confeccion) + "' ORDER BY CODIGO_VARIEDAD"
            If RsAuxiliar.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla h_costes_fabricacion")
                Exit Function
            End If

            If RsAuxiliar.RecordCount = 0 Then
                MsgBox("No existen lineas de detalle para el día " + Format(RsLineas!Fecha, "dd/MM/yyyy") + " y la confeccion " + Trim(RsLineas!codigo_confeccion) + "." + vbCrLf + "Se cancela el proceso.")
                Exit Function
            End If
            For j = 1 To RsAuxiliar.RecordCount
                RsAuxiliar.AbsolutePosition = j
                Busquedas(0) = "SELECT * FROM GRUPOS_COSTE_TABLA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_GRUPO ='" + Trim(RsLineas!grupo_coste) + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "'"
                Busquedas(1) = Trim(Busquedas(0)) + " AND CODIGO_VARIEDAD ='" + Trim(RsAuxiliar!Codigo_Variedad) + "' AND GRUPO_CONFECCION = '" + Trim(RsAuxiliar!grupo_confeccion) + "' AND CODIGO_CONFECCION = '" + Trim(RsAuxiliar!codigo_confeccion) + "'"
                Busquedas(2) = Trim(Busquedas(0)) + " AND CODIGO_VARIEDAD ='" + Trim(RsAuxiliar!Codigo_Variedad) + "' AND GRUPO_CONFECCION = '" + Trim(RsAuxiliar!grupo_confeccion) + "' AND CODIGO_CONFECCION = '-'"
                Busquedas(3) = Trim(Busquedas(0)) + " AND CODIGO_VARIEDAD ='-' AND GRUPO_CONFECCION = '" + Trim(RsAuxiliar!grupo_confeccion) + "' AND CODIGO_CONFECCION = '" + Trim(RsAuxiliar!codigo_confeccion) + "'"
                Busquedas(4) = Trim(Busquedas(0)) + " AND CODIGO_VARIEDAD ='-' AND GRUPO_CONFECCION = '" + Trim(RsAuxiliar!grupo_confeccion) + "' AND CODIGO_CONFECCION = '-'"
                Busquedas(5) = Trim(Busquedas(0)) + " AND CODIGO_VARIEDAD ='" + Trim(RsAuxiliar!Codigo_Variedad) + "' AND GRUPO_CONFECCION = '-' AND CODIGO_CONFECCION = '-'"
                Busquedas(6) = Trim(Busquedas(0)) + " AND CODIGO_VARIEDAD ='-' AND GRUPO_CONFECCION = '-' AND CODIGO_CONFECCION = '-'"
                For jj = 1 To 5 : DatosTabla(jj) = 0 : Next
                For jj = 1 To 6
                    If RsTemporal.Open(Busquedas(jj), ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla grupos_coste_tabla (" + CStr(jj) + ")")
                        Exit Function
                    End If
                    If RsTemporal.RecordCount > 0 Then
                        DatosTabla(1) = RsTemporal!coste_kg
                        DatosTabla(2) = RsTemporal!puntos_kg
                        DatosTabla(3) = RsTemporal!porc_confeccion
                        DatosTabla(4) = RsTemporal!porc_paletizacion
                        DatosTabla(5) = RsTemporal!porc_envase
                        jj = 1000
                    End If
                    RsTemporal.Close()
                Next
                If jj < 100 Then
                    Cadena = "Los datos del día " + CStr(RsLineas!Fecha) + " no se pueden grabar correctamente:" + vbCrLf + "GRUPO COSTE:" + CStr(RsLineas!grupo_coste) + vbCrLf + "FAMILIA  . :" + Trim(Familia) + vbCrLf
                    Cadena = Trim(Cadena) + " VARIEDAD . :" + Trim(RsAuxiliar!Codigo_Variedad) + vbCrLf + "GRUPO CONF.:" + Trim(RsAuxiliar!grupo_confeccion) + vbCrLf + "CONFECCION :" + Trim(RsAuxiliar!codigo_confeccion) + vbCrLf
                    Cadena = Trim(Cadena) + " ¿Desea continuar?"
                    If MsgBox(Cadena, vbCritical + vbYesNo) = vbNo Then
                        Exit Function
                    End If

                    RsEspecial.AddNew()
                    RsEspecial!dia = RsLineas!Fecha
                    RsEspecial!grupo_coste = Trim(RsLineas!grupo_coste)
                    RsEspecial!familia_variedad = Trim(Familia)
                    RsEspecial!Codigo_Variedad = RsAuxiliar!Codigo_Variedad
                    'RsEspecial!grupo_confeccion = Trim(RsAuxiliar!grupo_confeccion)
                    RsEspecial!fecha_proceso = Now()
                    RsEspecial.Update()
                End If
                If IsDBNull(RsLineas!kilos) Then RsLineas!kilos = 0
                If IsDBNull(RsAuxiliar!kilos) Then RsAuxiliar!kilos = 0
                If IsDBNull(RsLineas!palets) Then RsLineas!palets = 0
                If IsDBNull(RsAuxiliar!palets) Then RsAuxiliar!palets = 0

                RsLineas!Kilos = Math.Round(RsLineas!kilos + RsAuxiliar!Kilos, 2)
                RsLineas!Palets = RsLineas!Palets + RsAuxiliar!Palets
                If Math.Round(DatosTabla(1), 6) <> 0 And Math.Round(RsAuxiliar!Kilos, 2) <> 0 Then
                    RsLineas!importe_fijo = Math.Round(RsLineas!importe_fijo + RsAuxiliar!Kilos * DatosTabla(1), 2)
                End If
                If Math.Round(DatosTabla(3), 2) <> 0 And Math.Round(RsAuxiliar!importe_confeccion, 2) <> 0 Then
                    RsLineas!importe_confeccion = Math.Round(RsLineas!importe_confeccion + RsAuxiliar!importe_confeccion * DatosTabla(3) / 100, 2)
                End If
                If Math.Round(DatosTabla(4), 2) <> 0 And Math.Round(RsAuxiliar!importe_paletizacion, 2) <> 0 Then
                    RsLineas!importe_paletizacion = Math.Round(RsLineas!importe_paletizacion + RsAuxiliar!importe_paletizacion * DatosTabla(4) / 100, 2) 'OJO
                End If
                If Math.Round(DatosTabla(5), 2) <> 0 And Math.Round(RsAuxiliar!importe_envase, 2) <> 0 Then
                    RsLineas!importe_envase = Math.Round(RsLineas!importe_envase + RsAuxiliar!importe_envase * DatosTabla(5) / 100, 2)
                End If
                RsLineas.Update()
            Next
            RsAuxiliar.Close()
        Next
        For i = 1 To RsLineas.RecordCount
            RsLineas.AbsolutePosition = i
            ContadorCRT = ContadorCRT + 1
            Lbl11.Text = Trim(Familia) + " " + CStr(ContadorCRT)
            Lbl11.Refresh()

            SQL = "SELECT ISNULL(sum(importe_mo),0) as mo FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND TIPO_COSTE = '2' AND FECHA = '" + Format(RsLineas!Fecha, "dd/MM/yyyy") + "' AND codigo_CONFECCION = '" + Trim(RsLineas!codigo_confeccion) + "' AND APARTADO = " + CStr(RsLineas!apartado) + " AND SUBAPARTADO = " + CStr(RsLineas!subapartado) + " AND LINEA_INFORME = " + CStr(RsLineas!linea_informe)
            If RsPuntos.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla h_costes_fabricacion")
                Exit Function
            End If
            If RsPuntos.RecordCount > 0 Then
                '        If RsLineas!Apartado = 2 And RsLineas!Subapartado = 3 And Trim(RsLineas!codigo_confeccion) = "0" Then
                '            MsgBox(""
                '        End If
                RsLineas!importe_mo = Math.Round(RsPuntos!mo, 2)
            End If
            RsPuntos.Close()
            RsLineas!importe_total = Math.Round(RsLineas!importe_mo + RsLineas!importe_confeccion + RsLineas!importe_envase + RsLineas!importe_paletizacion + RsLineas!importe_fijo, 2)
            RsLineas!centimos_kg = 0
            If RsLineas!Kilos <> 0 Then RsLineas!centimos_kg = Math.Round(RsLineas!importe_total * 100 / RsLineas!Kilos, 6)
            RsLineas.Update()
        Next
        RsLineas.Close()
        CalcularLineasInforme = True
    End Function


    Public Sub IncluirPalet(Rs As cnRecordset.CnRecordset, Peso As Long)
        Dim RsTemporal As New cnRecordset.CnRecordset
        Dim SQL As String, i As Integer, ImportePaletizacion As Double, ImporteConfeccion As Double, ImporteEnvase As Double
        Dim Cadena As String, Flaghay As Boolean

        ImporteConfeccion = 0
        ImportePaletizacion = 0
        ImporteEnvase = 0
        If Trim(Rs!Situacion) = "A" Then
            SQL = "SELECT dbo.fn_coste_escandallo_palet('" + Trim(Rs!Empresa) + "'," + CStr(Rs!codigo_Palet) + ",'SC') AS confeccion"
            SQL = Trim(SQL) + ", dbo.fn_coste_escandallo_palet('" + Trim(Rs!Empresa) + "'," + CStr(Rs!codigo_Palet) + ",'E') AS envase"
            SQL = Trim(SQL) + ", dbo.fn_coste_escandallo_palet('" + Trim(Rs!Empresa) + "'," + CStr(Rs!codigo_Palet) + ",'SPT') AS paletizacion"
            SQL = Trim(SQL) + ", dbo.fn_coste_escandallo_palet('" + Trim(Rs!Empresa) + "'," + CStr(Rs!codigo_Palet) + ",'P') AS palet"
            If RsTemporal.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir escandallos")
                Exit Sub
            End If
            ImporteConfeccion = Math.Round(RsTemporal!Confeccion, 2)
            ImporteEnvase = Math.Round(RsTemporal!envase, 2)
            ImportePaletizacion = Math.Round(RsTemporal!paletizacion, 2)
            ImporteConfeccion = Math.Round(ImporteConfeccion - ImporteEnvase, 2)
            RsTemporal.Close()
        Else
            ImporteConfeccion = Math.Round(Rs!coste_confeccion, 2)
            ImporteEnvase = Math.Round(Rs!coste_envase, 2)
            ImportePaletizacion = Math.Round(Rs!coste_paletizacion, 2)
            ImporteConfeccion = Math.Round(ImporteConfeccion - ImporteEnvase, 2)
        End If

        SQL = "SELECT * FROM h_costes_FABRICACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND FECHA = '" + Format(CDate(Rs!fecha_confeccion), "dd/MM/yyyy") + "' AND FAMILIA_VARIEDAD = '" + Trim(Rs!codigo_familia) + "' AND CODIGO_VARIEDAD = '" + Trim(Rs!Codigo_Variedad) + "' AND CODIGO_CONFECCION = '" + Trim(Rs!codigo_confeccion) + "' AND TIPO_COSTE = '1'"
        If RsTemporal.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir h_costes_fabricacion")
            Exit Sub
        End If
        If RsTemporal.RecordCount > 0 Then
            RsTemporal!Palets = RsTemporal!Palets + 1
            RsTemporal!Kilos = RsTemporal!Kilos + Peso
            RsTemporal!importe_confeccion = RsTemporal!importe_confeccion + ImporteConfeccion
            RsTemporal!importe_paletizacion = RsTemporal!importe_paletizacion + ImportePaletizacion
            RsTemporal!importe_envase = RsTemporal!importe_envase + ImporteEnvase
            RsTemporal.Update()
        Else
            RsTemporal.AddNew()
            RsTemporal!Empresa = Trim(ObjetoGlobal.EmpresaActual)
            'RsTemporal!proceso = proceso
            ContadorGeneral = ContadorGeneral + 1
            RsTemporal!Contador = ContadorGeneral
            RsTemporal!Fecha = CDate(Rs!fecha_confeccion)
            RsTemporal!familia_variedad = Trim(Familia)
            RsTemporal!Codigo_Variedad = Trim(Rs!Codigo_Variedad)
            RsTemporal!grupo_confeccion = Trim(Rs!grupo_confeccion)
            RsTemporal!codigo_confeccion = Trim(Rs!codigo_confeccion)
            RsTemporal!tipo_coste = "1"
            RsTemporal!Palets = 1
            RsTemporal!Kilos = Peso
            RsTemporal!importe_confeccion = ImporteConfeccion
            RsTemporal!importe_paletizacion = ImportePaletizacion
            RsTemporal!importe_envase = ImporteEnvase

            RsTemporal!importe_fijo = 0
            RsTemporal!importe_mo = 0
            RsTemporal!importe_total = 0
            RsTemporal!centimos_kg = 0
            RsTemporal!suma_ptos_kilos = 0


            RsTemporal.Update()
        End If
        RsTemporal.Close()
        Cadena = Format(CDate(Rs!fecha_confeccion), "yyyyMMdd") + Trim(Rs!codigo_confeccion)
        Flaghay = False
        For i = 0 To CuantasConfecciones
            If Cadena = Trim(Confecciones(0, i)) Then
                Flaghay = True
                Exit For
            End If
        Next
        If Flaghay = False Then
            CuantasConfecciones = CuantasConfecciones + 1
            ReDim Preserve Confecciones(1, CuantasConfecciones)
            Confecciones(0, CuantasConfecciones) = Trim(Cadena)
            Confecciones(1, CuantasConfecciones) = Trim(Rs!codigo_confeccion)
        End If
    End Sub

    Private Function GrabaHistoricoDetalle() As Boolean
        Dim SQL As String, Rs As New cnRecordset.CnRecordset, RsAux As New cnRecordset.CnRecordset, RsTemp44 As New cnRecordset.CnRecordset
        Dim VarInd() As String, DesVarInd() As String, FamVarInd() As String
        Dim i As Integer, j As Integer, k As Integer, Peso As Double
        Dim ContadorDetalle As Long
        Dim Total As Long, TotalPeso As Double
        Dim RsPalets As New cnRecordset.CnRecordset, RsVar As New cnRecordset.CnRecordset
        Dim PesoUnidadCol As Double, CosteVicente As Double, HayPesoDeCalculo As Integer

        GrabaHistoricoDetalle = False
        PesoUnidadCol = 1.6
        ContadorDetalle = 0

        ReDim VarInd(5)
        ReDim FamVarInd(5)
        ReDim DesVarInd(5)

        VarInd(0) = "" : VarInd(1) = "" : VarInd(2) = "" : VarInd(3) = "" : VarInd(4) = ""
        FamVarInd(0) = "NAR" : FamVarInd(1) = "CLE" : FamVarInd(2) = "HIB" : FamVarInd(3) = "SAT" : FamVarInd(4) = "NAB"
        DesVarInd(0) = "" : DesVarInd(1) = "" : DesVarInd(2) = "" : DesVarInd(3) = "" : DesVarInd(4) = ""

        For i = 0 To 4
            Peso = 0
            SQL = "SELECT codigo_variedad, familia_variedad, sum(peso_palet) AS Kg_variedad From temp_costes1 WHERE proceso=" & Proceso & " AND familia_variedad = '" & FamVarInd(i) & "' GROUP BY codigo_variedad, familia_variedad"
            If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla temp_costes1")
                Exit Function
            End If
            For j = 1 To Rs.RecordCount
                Rs.AbsolutePosition = j
                If Trim(Rs!codigo_Variedad) <> "" Then
                    If Peso < Rs!Kg_variedad Then
                        VarInd(i) = Rs!Codigo_Variedad
                    End If
                End If
            Next
            Rs.Close()
        Next

        For i = 0 To 4
            If Trim(VarInd(i)) <> "" Then
                SQL = "SELECT * FROM variedades WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_variedad = '" & VarInd(i) & "'"
                If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
                    MsgBox("Error al abrir la tabla variedades")
                    Exit Function
                End If
                DesVarInd(i) = RsAux!descripcion
                RsAux.Close()
            End If
        Next

        SQL = "SELECT * FROM h_costes_detalle WHERE 1=0"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_detalle")
            Exit Function
        End If

        SQL = "SELECT c.*, p.descripcion as des_puesto, o.nombre_operario, o.tipo_operario FROM temp_costes44 c LEFT JOIN personal_puestos p ON "
        SQL = SQL + " p.empresa = c.empresa AND p.codigo_puesto=c.codigo_puesto "
        SQL = SQL + " LEFT JOIN operarios_coop o ON o.codigo_operario = c.codigo_operario "
        SQL = SQL + " WHERE proceso=" & Proceso & " ORDER BY contador"
        If RsTemp44.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla temp_costes44")
            Exit Function
        End If

        For i = 1 To RsTemp44.RecordCount
            RsTemp44.AbsolutePosition = i
            Lbl12.Text = "Detalle histórico: " & CStr(Total - ContadorDetalle)
            Lbl12.Refresh()

            ContadorDetalle = ContadorDetalle + 1
            Rs.AddNew()
            Rs!Empresa = RsTemp44!Empresa
            Rs!Fecha = FechaProceso
            Rs!Contador = ContadorDetalle
            Rs!codigo_Palet = RsTemp44!codigo_Palet
            Rs!codigo_puesto = RsTemp44!codigo_puesto
            Rs!des_puesto = RsTemp44!des_puesto
            Rs!Codigo_Operario = RsTemp44!Codigo_Operario
            Rs!nombre_operario = RsTemp44!nombre_operario
            Rs!tipo_operario = IIf(InStr(1, UCase("" & RsTemp44!tipo_operario), "F") > 0, "F", "N")
            Rs!apartado = RsTemp44!apartado
            Rs!subapartado = RsTemp44!subapartado
            Rs!linea_informe = RsTemp44!linea_informe
            Rs!importe_volcado = RsTemp44!importe_volcado
            Rs!importe_general = RsTemp44!importe_general
            Rs!importe_paletizado = RsTemp44!importe_paletizado
            Rs!importe_paletiz_g = RsTemp44!importe_paletiz_g
            Rs!importe_industria = RsTemp44!importe_industria
            Rs!importe_precalibre = RsTemp44!importe_precalibre
            Rs!importe_total_c = (RsTemp44!importe_volcado + RsTemp44!importe_general + RsTemp44!importe_paletizado + RsTemp44!importe_paletiz_g + RsTemp44!importe_industria + RsTemp44!importe_precalibre)
            If Len(Trim(RsTemp44!codigo_Palet)) = 1 And Trim(RsTemp44!codigo_Palet) >= "1" And Trim(RsTemp44!codigo_Palet) <= "6" Then
                ' Se trata de un palet de industria
                Rs!Variedad = Trim(VarInd(CInt(Trim(RsTemp44!codigo_Palet)) - 1))
                Rs!des_variedad = Trim(DesVarInd(CInt(Trim(RsTemp44!codigo_Palet)) - 1))
                Rs!familia_variedad = Trim(FamVarInd(CInt(Trim(RsTemp44!codigo_Palet)) - 1))
            ElseIf Len(Trim(RsTemp44!codigo_Palet)) < 9 Then
                ' Se trata de un palet de precalibrado
                SQL = "SELECT p.*, v.descripcion as des_variedad, v.codigo_familia as familia_variedad FROM palets_precalibre p LEFT JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = p.codigo_variedad WHERE p.empresa='" & Trim(RsTemp44!Empresa) & "' AND "
                SQL = SQL + " p.ejercicio = '" & Trim(ObjetoGlobal.EjercicioActual) & "' AND p.codigo_palet='" & Trim(RsTemp44!codigo_Palet) & "'"
                If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
                    MsgBox("Error al abrir la tabla palets_precalibre")
                    Exit Function
                End If
                If RsAux.RecordCount <> 0 Then
                    Rs!Variedad = RsAux!Codigo_Variedad
                    Rs!des_variedad = RsAux!des_variedad
                    Rs!familia_variedad = RsAux!familia_variedad
                End If
                RsAux.Close()
            Else
                ' Se trata de un palet normal
                SQL = "SELECT p.*, v.codigo_variedad as variedad, v.descripcion as des_variedad, v.codigo_familia as familia_variedad FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
                SQL = SQL + "LEFT JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = o.codigo_variedad "
                SQL = SQL + "WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.codigo_palet = '" & Trim(RsTemp44!codigo_Palet) & "'"
                If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
                    MsgBox("Error al abrir la tabla palets")
                    Exit Function
                End If
                If RsAux.RecordCount <> 0 Then
                    Rs!Variedad = RsAux!Variedad
                    Rs!des_variedad = RsAux!des_variedad
                    Rs!familia_variedad = RsAux!familia_variedad
                End If
                RsAux.Close()
            End If
            Rs.Update()
        Next
        Rs.Close()


        SQL = "SELECT * FROM h_costes_detalle WHERE 1=0"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla h_costes_detalle")
            Exit Function
        End If

        ' Ahora Grabaremos el detalle de las verduras
        BorrarVerdurasAnteriores()

        SQL = "SELECT p.puesto, pp.descripcion, p.cod_operario, o.nombre_operario, o.tipo_operario, pp.apartado, pp.subapartado, pp.linea_informe, pp.desde_variedad, pp.hasta_variedad, sum(p.importe_coste) as coste_puesto FROM personal_es p LEFT JOIN personal_puestos pp ON pp.empresa=p.empresa AND pp.codigo_puesto = p.puesto "
        SQL = SQL + "LEFT JOIN operarios_coop o ON o.codigo_operario = p.cod_operario "
        SQL = SQL + "WHERE p.empresa= '" & ObjetoGlobal.EmpresaActual & "' AND (pp.tipo_puesto IN ('F', 'A')) AND p.fecha_entrada='" & Format(FechaProceso, "dd/MM/yyyy") & "' "
        SQL = SQL + "GROUP BY p.puesto, pp.descripcion, p.cod_operario, o.nombre_operario, o.tipo_operario, pp.apartado, pp.subapartado, pp.linea_informe, pp.desde_variedad, pp.hasta_variedad"
        If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla personal_es")
            Exit Function
        End If

        HayPesoDeCalculo = 0
        For i = 1 To RsAux.RecordCount
            RsAux.AbsolutePosition = i

            HayPesoDeCalculo = 1
            ' Averiguamos el total de Kg a repartir
            SQL = "SELECT h.empresa, h.codigo, h.fecha, h.clave_marcaje, h.accion, h.hora, h.contador_proceso, h.orden, h.tipomarca, o.codigo_variedad, p.peso_palet, p.codigo_palet, v.descripcion FROM HCO_FICHADAS h"
            SQL = SQL + " LEFT JOIN palets p ON p.empresa = h.empresa AND p.codigo_palet = h.codigo "
            SQL = SQL + " LEFT JOIN ordenes_confeccion o ON o.empresa = h.empresa AND o.numero_orden = p.orden_confeccion "
            SQL = SQL + " LEFT JOIN variedades v ON v.empresa = h.empresa AND v.codigo_variedad = o.codigo_variedad "
            SQL = SQL + " WHERE h.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND h.FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND (h.ACCION LIKE '%FABRICACION_P%') "
            If Not IsDBNull(RsAux!desde_variedad) And Not IsDBNull(RsAux!hasta_variedad) Then
                SQL = SQL + " AND  o.codigo_variedad between '" & Trim(RsAux!desde_variedad) & "' AND '" & Trim(RsAux!hasta_variedad) & "'"
            End If
            SQL = SQL + " ORDER BY h.empresa,o.codigo_variedad, h.FECHA, h.HORA"
            If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la hco_fichadas")
                Exit Function
            End If

            TotalPeso = 0
            For j = 1 To RsPalets.RecordCount
                RsPalets.AbsolutePosition = j
                TotalPeso = TotalPeso + RsPalets!peso_palet
            Next
            RsPalets.Close()

            If TotalPeso > 0 Then
                HayPesoDeCalculo = 2
                SQL = "SELECT h.empresa, h.codigo, h.fecha, h.clave_marcaje, h.accion, h.hora, h.contador_proceso, h.orden, h.tipomarca, o.codigo_variedad, p.peso_palet, p.codigo_palet, v.descripcion FROM HCO_FICHADAS h"
                SQL = SQL + " LEFT JOIN palets p ON p.empresa = h.empresa AND p.codigo_palet = h.codigo "
                SQL = SQL + " LEFT JOIN ordenes_confeccion o ON o.empresa = h.empresa AND o.numero_orden = p.orden_confeccion "
                SQL = SQL + " LEFT JOIN variedades v ON v.empresa = h.empresa AND v.codigo_variedad = o.codigo_variedad "
                SQL = SQL + " WHERE h.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND h.FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "' AND (h.ACCION LIKE '%FABRICACION_P%') "
                If Not IsDBNull(RsAux!desde_variedad) And Not IsDBNull(RsAux!hasta_variedad) Then
                    SQL = SQL + " AND  o.codigo_variedad between '" & RsAux!desde_variedad & "' AND '" & RsAux!hasta_variedad & "'"
                End If
                SQL = SQL + " ORDER BY o.codigo_variedad, h.FECHA, h.HORA"
                    If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la hco_fichadas")
                        Exit Function
                    End If

                    For j = 1 To RsPalets.RecordCount
                        RsPalets.AbsolutePosition = j
                        HayPesoDeCalculo = 3
                        ContadorDetalle = ContadorDetalle + 1
                        Lbl12.Text = "Verdura: " & ContadorDetalle
                        Lbl12.Refresh()
                        Rs.AddNew()
                        Rs!Empresa = RsPalets!Empresa
                        Rs!Fecha = FechaProceso
                        Rs!Contador = ContadorDetalle
                        Rs!codigo_Palet = RsPalets!codigo_Palet
                        Rs!codigo_puesto = RsAux!puesto
                        Rs!des_puesto = RsAux!descripcion
                        Rs!Codigo_Operario = RsAux!cod_operario
                        Rs!nombre_operario = RsAux!nombre_operario
                        Rs!tipo_operario = IIf(InStr(1, UCase("" & RsAux!tipo_operario), "F") > 0, "F", "N")
                        Rs!apartado = RsAux!apartado
                        Rs!subapartado = RsAux!subapartado
                        Rs!linea_informe = RsAux!linea_informe
                        Rs!importe_volcado = 0
                    Rs!importe_general = Math.Round(RsAux!coste_puesto * RsPalets!peso_palet / TotalPeso, 2)
                    Rs!importe_paletizado = 0
                        Rs!importe_paletiz_g = 0
                        Rs!importe_industria = 0
                        Rs!importe_precalibre = 0
                    Rs!importe_total_c = Math.Round(RsAux!coste_puesto * RsPalets!peso_palet / TotalPeso, 2)
                    SQL = "SELECT p.*, v.codigo_variedad as variedad, v.descripcion as des_variedad, v.codigo_familia as familia_variedad FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
                        SQL = SQL + "LEFT JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = o.codigo_variedad "
                        SQL = SQL + "WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.codigo_palet = '" & Trim(RsPalets!codigo_Palet) & "'"
                        If RsVar.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la palets")
                            Exit Function
                        End If
                        If RsVar.RecordCount > 0 Then
                            Rs!Variedad = RsVar!Variedad
                            Rs!des_variedad = RsVar!des_variedad
                            Rs!familia_variedad = RsVar!familia_variedad
                        End If
                        RsVar.Close()
                        Rs.Update()
                    Next

                    If HayPesoDeCalculo < 3 Then
                        MsgBox(" Puesto: " & RsAux!puesto & " operario: " & RsAux!cod_operario & " no tienen marcajes en el histórico pero tienen peso")
                    End If
                    RsPalets.Close()
                End If
                If HayPesoDeCalculo < 2 Then
                MsgBox(" Puesto: " & RsAux!puesto & " operario: " & RsAux!cod_operario & " no tienen marcajes en el histórico ni peso imputable")
            End If
        Next
        RsAux.Close()


        ' Ahora repartiremos los gastos de Vicente
        SQL = "select p.peso_palet as pesop,* from palets p left join ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
        SQL = SQL + "where p.empresa = '" & ObjetoGlobal.EmpresaActual & "' and o.codigo_variedad >= '180' "
        SQL = SQL + "and p.fecha_confeccion = '" & Format(FechaProceso, "dd/MM/yyyy") & "' "
        SQL = SQL + "and (o.codigo_variedad = '291' or "
        SQL = SQL + "exists (SELECT pe.empresa FROM personal_es pe "
        SQL = SQL + "left join personal_puestos pp ON pp.empresa=p.empresa AND pp.codigo_puesto = pe.puesto "
        SQL = SQL + "WHERE pe.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND pe.fecha_entrada = '" & Format(FechaProceso, "dd/MM/yyyy") & "' and o.codigo_variedad >= pp.desde_variedad and o.codigo_variedad <= pp.hasta_variedad)) "
        If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la palets")
            Exit Function
        End If
        TotalPeso = 0
        For j = 1 To RsAux.RecordCount
            RsAux.AbsolutePosition = j
            If Trim(RsAux!Codigo_Variedad) <> "291" Then
                TotalPeso = TotalPeso + RsAux!pesop
            Else ' Si es col serán unidades por el peso medio de 1.6 Kg por unidad
                TotalPeso = TotalPeso + (RsAux!pesop * PesoUnidadCol)
            End If
        Next
        RsAux.Close()

        'If TotalPeso = 0 Then
        '   MsgBox("No hay pesos en los palets para realizar el reparto de gastos"
        '   GrabaHistoricoDetalle = False
        '   Exit Function
        'End If

        ' Total peso de toda la verdura
        ' Ahora repartiremos los gastos de Vicente
        SQL = "select p.peso_palet as pesop,* from palets p left join ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
        SQL = SQL + "where p.empresa = '" & ObjetoGlobal.EmpresaActual & "' and o.codigo_variedad >= '180' "
        SQL = SQL + "and p.fecha_confeccion = '" & Format(FechaProceso, "dd/MM/yyyy") & "' "
        SQL = SQL + "and (o.codigo_variedad = '291' or "
        SQL = SQL + "exists (SELECT pe.empresa FROM personal_es pe "
        SQL = SQL + "left join personal_puestos pp ON pp.empresa=p.empresa AND pp.codigo_puesto = pe.puesto "
        SQL = SQL + "WHERE pe.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND pe.fecha_entrada = '" & Format(FechaProceso, "dd/MM/yyyy") & "' and o.codigo_variedad >= pp.desde_variedad and o.codigo_variedad <= pp.hasta_variedad)) "
        If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la palets")
            Exit Function
        End If
        TotalPeso = 0
        For j = 1 To RsAux.RecordCount
            RsAux.AbsolutePosition = j
            If Trim(RsAux!Codigo_Variedad) <> "291" Then
                TotalPeso = TotalPeso + RsAux!pesop
            Else ' Si es col serán unidades por el peso medio de 1.6 Kg por unidad
                TotalPeso = TotalPeso + (RsAux!pesop * PesoUnidadCol)
            End If
        Next
        RsAux.Close()

        ' Ahora calculamos el importe de coste de Vicente o similares
        SQL = "SELECT p.puesto, pp.descripcion, p.cod_operario, o.nombre_operario, o.tipo_operario, pp.apartado, pp.subapartado, pp.linea_informe, pp.desde_variedad, pp.hasta_variedad, sum(p.importe_coste) as coste_puesto FROM personal_es p LEFT JOIN personal_puestos pp ON pp.empresa=p.empresa AND pp.codigo_puesto = p.puesto "
        SQL = SQL + "LEFT JOIN operarios_coop o ON o.codigo_operario = p.cod_operario "
        SQL = SQL + "WHERE p.empresa= '" & ObjetoGlobal.EmpresaActual & "' AND pp.tipo_puesto ='Y'  AND p.fecha_entrada='" & Format(FechaProceso, "dd/MM/yyyy") & "' "
        SQL = SQL + "GROUP BY p.puesto, pp.descripcion, p.cod_operario, o.nombre_operario, o.tipo_operario, pp.apartado, pp.subapartado, pp.linea_informe, pp.desde_variedad, pp.hasta_variedad"
        If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la personal_es")
            Exit Function
        End If
        If RsAux.RecordCount > 0 Then
            For j = 1 To RsAux.RecordCount
                RsAux.AbsolutePosition = j
                CosteVicente = IIf(Not IsDBNull(RsAux!coste_puesto), RsAux!coste_puesto, 0)
                If CosteVicente > 0 Then 'Hay costes a repartir
                    ' Ahora repartiremos los gastos de Vicente
                    SQL = "select p.peso_palet as pesop,* from palets p left join ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
                    SQL = SQL + "where p.empresa = '" & ObjetoGlobal.EmpresaActual & "' and o.codigo_variedad >= '180' "
                    SQL = SQL + "and p.fecha_confeccion = '" & Format(FechaProceso, "dd/MM/yyyy") & "' "
                    SQL = SQL + "and (o.codigo_variedad = '291' or "
                    SQL = SQL + "exists (SELECT pe.empresa FROM personal_es pe "
                    SQL = SQL + "left join personal_puestos pp ON pp.empresa=p.empresa AND pp.codigo_puesto = pe.puesto "
                    SQL = SQL + "WHERE pe.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND pe.fecha_entrada = '" & Format(FechaProceso, "dd/MM/yyyy") & "' and o.codigo_variedad >= pp.desde_variedad and o.codigo_variedad <= pp.hasta_variedad)) "
                    If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la palets")
                        Exit Function
                    End If
                    For k = 1 To RsPalets.RecordCount
                        RsPalets.AbsolutePosition = k
                        If TotalPeso <> 0 And RsPalets!pesop <> 0 Then
                            ContadorDetalle = ContadorDetalle + 1
                            Rs.AddNew()
                            Rs!Empresa = RsPalets!Empresa
                            Rs!Fecha = FechaProceso
                            Rs!Contador = ContadorDetalle
                            Rs!codigo_Palet = RsPalets!codigo_Palet
                            Rs!codigo_puesto = RsAux!puesto
                            Rs!des_puesto = RsAux!descripcion
                            Rs!Codigo_Operario = RsAux!cod_operario
                            Rs!nombre_operario = RsAux!nombre_operario
                            Rs!tipo_operario = IIf(InStr(1, UCase("" & RsAux!tipo_operario), "F") > 0, "F", "N")
                            Rs!apartado = RsAux!apartado
                            Rs!subapartado = RsAux!subapartado
                            Rs!linea_informe = RsAux!linea_informe
                            Rs!importe_volcado = 0
                            If TotalPeso <> 0 And RsPalets!pesop <> 0 Then
                                Rs!importe_general = Math.Round(CosteVicente / TotalPeso * RsPalets!pesop, 2)
                                Rs!importe_total_c = Math.Round(CosteVicente / TotalPeso * RsPalets!pesop, 2)
                            Else
                                Rs!importe_general = 0
                                Rs!importe_total_c = 0
                            End If
                            Rs!importe_paletizado = 0
                            Rs!importe_paletiz_g = 0
                            Rs!importe_industria = 0
                            Rs!importe_precalibre = 0
                            SQL = "SELECT p.*, v.codigo_variedad as variedad, v.descripcion as des_variedad, v.codigo_familia as familia_variedad FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
                            SQL = SQL + "LEFT JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = o.codigo_variedad "
                            SQL = SQL + "WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.codigo_palet = '" & Trim(RsPalets!codigo_Palet) & "'"
                            If RsVar.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la palets")
                                Exit Function
                            End If
                            If RsVar.RecordCount <> 0 Then
                                Rs!Variedad = RsVar!Variedad
                                Rs!des_variedad = RsVar!des_variedad
                                Rs!familia_variedad = RsVar!familia_variedad
                            End If
                            RsVar.Close()
                            Rs.Update()
                        End If
                    Next
                    RsPalets.Close()
                End If
            Next
        End If
        RsAux.Close()

        '' Cálculo de gastos de la Sandía
        'SQL = "SELECT p.puesto, pp.descripcion, p.cod_operario, o.nombre_operario, o.tipo_operario, pp.apartado, pp.subapartado, pp.linea_informe, pp.desde_variedad, pp.hasta_variedad, sum(p.importe_coste) as coste_puesto FROM personal_es p LEFT JOIN personal_puestos pp ON pp.empresa=p.empresa AND pp.codigo_puesto = p.puesto "
        'SQL = SQL + "LEFT JOIN operarios_coop o ON o.codigo_operario = p.cod_operario "
        'SQL = SQL + "WHERE p.empresa= '" & ObjetoGlobal.EmpresaActual & "' AND left(pp.codigo_puesto,1) = '5' AND p.fecha_entrada='" &  Format(fechaproceso, "dd/MM/yyyy") & "' "
        'SQL = SQL + "GROUP BY p.puesto, pp.descripcion, p.cod_operario, o.nombre_operario, o.tipo_operario, pp.apartado, pp.subapartado, pp.linea_informe, pp.desde_variedad, pp.hasta_variedad"
        '
        'HayPesoDeCalculo = 0
        'RsAux.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        '
        'TotalPeso = 0
        'While Not RsAux.EOF
        '    HayPesoDeCalculo = 1
        '    ' Averiguamos el total de Kg a repartir
        '    sql = "SELECT h.empresa, h.codigo, h.fecha, h.clave_marcaje, h.accion, h.hora, h.contador_proceso, h.orden, h.tipomarca, o.codigo_variedad, p.peso_palet, p.codigo_palet, v.descripcion FROM HCO_FICHADAS h"
        '    sql = sql + " LEFT JOIN palets p ON p.empresa = h.empresa AND p.codigo_palet = h.codigo "
        '    sql = sql + " LEFT JOIN ordenes_confeccion o ON o.empresa = h.empresa AND o.numero_orden = p.orden_confeccion "
        '    sql = sql + " LEFT JOIN variedades v ON v.empresa = h.empresa AND v.codigo_variedad = o.codigo_variedad "
        '    sql = sql + " WHERE h.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND h.FECHA = '" + Format(Fechaproceso, "dd/MM/yyyy") + "' AND (h.ACCION LIKE '%FABRICACION_P%') "
        '    sql = sql + " AND  o.codigo_variedad between '" & Trim(RsAux!desde_variedad) & "' AND '" & Trim(RsAux!hasta_variedad) & "'"
        '    sql = sql + " ORDER BY h.empresa,o.codigo_variedad, h.FECHA, h.HORA"
        '    RsPalets.Open sql, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        '
        '    TotalPeso = 0
        '    While Not RsPalets.EOF
        '        TotalPeso = TotalPeso + RsPalets!peso_palet
        '        RsPalets.MoveNext
        '    Wend
        '    RsPalets.Close
        '
        '    If TotalPeso > 0 Then
        '        HayPesoDeCalculo = 2
        '        sql = "SELECT h.empresa, h.codigo, h.fecha, h.clave_marcaje, h.accion, h.hora, h.contador_proceso, h.orden, h.tipomarca, o.codigo_variedad, p.peso_palet, p.codigo_palet, v.descripcion FROM HCO_FICHADAS h"
        '        sql = sql + " LEFT JOIN palets p ON p.empresa = h.empresa AND p.codigo_palet = h.codigo "
        '        sql = sql + " LEFT JOIN ordenes_confeccion o ON o.empresa = h.empresa AND o.numero_orden = p.orden_confeccion "
        '        sql = sql + " LEFT JOIN variedades v ON v.empresa = h.empresa AND v.codigo_variedad = o.codigo_variedad "
        '        sql = sql + " WHERE h.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND h.FECHA = '" + Format(Fechaproceso, "dd/MM/yyyy") + "' AND (h.ACCION LIKE '%FABRICACION_P%') "
        '        sql = sql + " AND  o.codigo_variedad between '" & RsAux!desde_variedad & "' AND '" & RsAux!hasta_variedad & "'"
        '        sql = sql + " ORDER BY o.codigo_variedad, h.FECHA, h.HORA"
        '
        '        RsPalets.Open sql, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        '
        '        While Not RsPalets.EOF
        '            HayPesoDeCalculo = 3
        '            Contadordetalle = Contadordetalle + 1
        '            LblProceso(12).Caption = "Sandías: " & Contadordetalle
        '            LblProceso(12).Refresh
        '            DoEvents
        '            Rs.AddNew
        '            Rs!Empresa = RsPalets!Empresa
        '            Rs!Fecha = Fechaproceso
        '            Rs!Contador = Contadordetalle
        '            Rs!codigo_Palet = RsPalets!codigo_Palet
        '            Rs!codigo_puesto = RsAux!puesto
        '            Rs!des_puesto = RsAux!descripcion
        '            Rs!Codigo_Operario = RsAux!cod_operario
        '            Rs!nombre_operario = RsAux!nombre_operario
        '            Rs!tipo_operario = IIf(InStr(1, UCase("" & RsAux!tipo_operario), "F") > 0, "F", "N")
        '            Rs!apartado = RsAux!apartado
        '            Rs!subapartado = RsAux!subapartado
        '            Rs!linea_informe = RsAux!linea_informe
        '            Rs!importe_volcado = 0
        '            Rs!importe_general =math.round(RsAux!coste_puesto * RsPalets!peso_palet / TotalPeso, 2)
        '            Rs!importe_paletizado = 0
        '            Rs!importe_paletiz_g = 0
        '            Rs!importe_industria = 0
        '            Rs!importe_precalibre = 0
        '            Rs!importe_total_c =math.round(RsAux!coste_puesto * RsPalets!peso_palet / TotalPeso, 2)
        '            sql = "SELECT p.*, v.codigo_variedad as variedad, v.descripcion as des_variedad, v.codigo_familia as familia_variedad FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion "
        '            sql = sql + "LEFT JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = o.codigo_variedad "
        '            sql = sql + "WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.codigo_palet = '" & Trim(RsPalets!codigo_Palet) & "'"
        '            rsVar.Open sql, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        '            If rsVar.RecordCount <> 0 Then
        '               Rs!Variedad = rsVar!Variedad
        '               Rs!des_variedad = rsVar!des_variedad
        '               Rs!familia_variedad = rsVar!familia_variedad
        '            End If
        '            rsVar.Close
        '            Rs.Update
        '            RsPalets.MoveNext
        '        Wend
        '        If HayPesoDeCalculo < 3 Then
        '           MsgBox(" Puesto: " & RsAux!puesto & " operario: " & RsAux!cod_operario & " no tienen marcajes en el histórico pero tienen peso"
        '        End If
        '        RsPalets.Close
        '    End If
        '    If HayPesoDeCalculo < 2 Then
        '       MsgBox(" Puesto: " & RsAux!puesto & " operario: " & RsAux!cod_operario & " no tienen marcajes en el histórico ni peso imputable"
        '    End If
        '    RsAux.MoveNext
        'Wend
        'RsAux.Close
        GrabaHistoricoDetalle = True

    End Function

    Private Sub BorrarVerdurasAnteriores()
        Dim SQL As String

        SQL = "SELECT * FROM h_costes_DETALLE WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND NOT FAMILIA_VARIEDAD IN ('NAR','CLE', 'HIB','SAT','NAB') AND FECHA = '" + Format(FechaProceso, "dd/MM/yyyy") + "'"
        BorrarDatos(SQL)
    End Sub



    Private Function BorrarDatos(SQL As String) As Boolean
        Dim Rs As New cnRecordset.CnRecordset

        BorrarDatos = False

        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir el recordset de borrado" + vbCrLf + Trim(SQL))
            Exit Function
        End If
        Do While Rs.RecordCount > 0
            Rs.MoveFirst()
            Rs.Delete()
        Loop
        Rs.Close()
        BorrarDatos = True

    End Function


    Private Sub InicializarControles()
        Lbl01.Visible = False : Lbl01.Text = "" : Chk01.Checked = False
        Lbl02.Visible = False : Lbl02.Text = "" : Chk02.Checked = False
        Lbl03.Visible = False : Lbl03.Text = "" : Chk03.Checked = False
        Lbl04.Visible = False : Lbl04.Text = "" : Chk04.Checked = False
        Lbl05.Visible = False : Lbl05.Text = "" : Chk05.Checked = False
        Lbl06.Visible = False : Lbl06.Text = "" : Chk06.Checked = False
        Lbl07.Visible = False : Lbl07.Text = "" : Chk07.Checked = False
        Lbl08.Visible = False : Lbl08.Text = "" : Chk08.Checked = False
        Lbl09.Visible = False : Lbl09.Text = "" : Chk09.Checked = False
        Lbl10.Visible = False : Lbl10.Text = "" : Chk10.Checked = False
        Lbl11.Visible = False : Lbl11.Text = "" : Chk11.Checked = False
        Lbl12.Visible = False : Lbl12.Text = ""


    End Sub
    Private Sub LeerPesosTeoricos()
        Dim Rs As New cnRecordset.CnRecordset, SQL As String
        SQL = "SELECT * FROM PESOS_TEORICOS WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "'"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla de pesos_teoricos")
            Exit Sub
        End If
        If Rs.RecordCount = 0 Then
            PesoIndustriaClementinaPequeno = 225
            PesoIndustriaClementinaPequeno = 225
            PesoIndustriaNaranjaPequeno = 180
            PesoIndustriaNaranjaPequeno = 180
            PesoIndustriaClementinaGrande = 340
            PesoIndustriaClementinaGrande = 340
            PesoIndustriaNaranjaGrande = 295
            PesoIndustriaNaranjaGrande = 295
            PesoVolcadoClementinaPalet = 600
            PesoVolcadoClementinaPalet = 600
            PesoVolcadoNaranjaPalet = 600
            PesoVolcadoNaranjaPalet = 600
            PesoVolcadoClementinaPalot = 295
            PesoVolcadoClementinaPalot = 295
            PesoVolcadoNaranjaPalot = 295
            PesoVolcadoNaranjaPalot = 295
            PesoIndustriaSandia = 300
            PesoIndustriaSandia = 300
            PesoCajaNaranja = 19
            PesoCajaNaranja = 19
            PesoCajaClementina = 20
            PesoCajaClementina = 20
            PesoCajaNaranjaPrecalibre = 16
            PesoCajaClementinaPrecalibre = 18
            PesoVolcadoPalotOtros = 295
            PesoVolcadoPalotOtros = 295
        Else
            PesoIndustriaClementinaPequeno = Rs!industria_mand_peq
            PesoIndustriaClementinaPequeno = Rs!industria_mand_peq
            PesoIndustriaNaranjaPequeno = Rs!industria_nar_peq
            PesoIndustriaNaranjaPequeno = Rs!industria_nar_peq
            PesoIndustriaClementinaGrande = Rs!industria_mand_gr
            PesoIndustriaClementinaGrande = Rs!industria_mand_gr
            PesoIndustriaNaranjaGrande = Rs!industria_nar_gr
            PesoIndustriaNaranjaGrande = Rs!industria_nar_gr
            PesoIndustriaSandia = Rs!industria_sandia
            PesoIndustriaSandia = Rs!industria_sandia
            PesoVolcadoClementinaPalet = Rs!volcado_palet_mand
            PesoVolcadoClementinaPalet = Rs!volcado_palet_mand
            PesoVolcadoNaranjaPalet = Rs!volcado_palet_nar
            PesoVolcadoNaranjaPalet = Rs!volcado_palet_nar
            PesoVolcadoClementinaPalot = Rs!volcado_palot_mand
            PesoVolcadoClementinaPalot = Rs!volcado_palot_mand
            PesoVolcadoNaranjaPalot = Rs!volcado_palot_nar
            PesoVolcadoNaranjaPalot = Rs!volcado_palot_nar
            PesoCajaNaranja = Rs!caja_naranja
            PesoCajaNaranja = Rs!caja_naranja
            PesoCajaClementina = Rs!caja_mandarina
            PesoCajaClementina = Rs!caja_mandarina
            PesoCajaNaranjaPrecalibre = Rs!caja_naranja_pre
            PesoCajaClementinaPrecalibre = Rs!caja_mandarina_pre
            PesoVolcadoPalotOtros = Rs!volcado_palot_otros
            PesoVolcadoPalotOtros = Rs!volcado_palot_otros
        End If
        Rs.Close()
    End Sub


    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property


    '    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '        Dim MiExcel As LibreriaGeneral.EXCEL_VB
    '        Dim MiHojaExcel As Microsoft.Office.Interop.Excel.Workbook
    '        Dim MiPestañaExcel As Microsoft.Office.Interop.Excel.Worksheet
    '        Dim MiRangoExcel As Microsoft.Office.Interop.Excel.Range


    '        MiExcel = New LibreriaGeneral.EXCEL_VB(False)
    '        MiExcel.AbrirHojaExcel("C:\Users\paco\Desktop\Plantilla.xltx")
    '        MiExcel.Visible = True

    '        '       MiHojaExcel = MiExcel.ObjetoHoja() ' En principio, no hace falta manipular directamente el objeto
    '        '       MiPestañaExcel = MiExcel.ObjetoPestaña()
    '        'MiExcel.RangoActual = "B2:C5"

    '        '      MiRangoExcel = MiExcel.ObjetoRango()

    '        'AppExcel.Selection.NumberFormat = "@"

    '        'MiRangoExcel.Select()
    '        'MiRangoExcel.Font.Size = 5

    '    End Sub
End Class
