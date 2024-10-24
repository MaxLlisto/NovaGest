Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class FrmNEntradasNuevo20t
    Public ObjetoGlobal As Object
    Private aCalidades() As String
    Private aCalidadesDes() As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim Clasificacion(1, 12) As Long
    Dim MuestraNueva(12) As Single
    Dim PesoNuevo(12) As Long
    Dim FlagMensaje As Boolean
    Dim FlagGrabarPlagas As Boolean
    Dim QuePlagas(99) As Integer
    Dim PorcentajePlaga(99) As Single
    Dim QueTiposPlagas(99) As String
    Dim nPorcentaje_plagas As Double
    Dim CuantasPlagas As Integer
    Dim KilosN(12) As Double
    Dim nClasificaciones As Integer
    Dim PorcentajeFinal As Double
    Dim PorcentajeGrande As Double
    Dim PorcentajePequena As Double
    Dim PorcentajeRecoleccion As Double
    Dim cuantascalidades As Integer
    Dim LblProvisional(12) As Label
    Dim LblDefinitiva(12) As Label
    Dim LblEnvase(12) As Label
    Dim lblMuestra(12) As Label
    Dim LblNueva(12) As Label
    Dim OptAlbaran(4) As RadioButton
    Public oform As FrmEntradaAlbaranes
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN grabar la clasificación?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub

    Public Function CalcularPesos() As Boolean
        Dim i As Integer
        Dim Peso1 As Single
        Dim Peso2 As Single
        Dim Peso3 As Single
        Dim Proporcion As Single
        Dim Incremento As Integer
        Dim MejorFactor As Single
        Dim Factor As Single
        Dim Quien As Integer

        CalcularPesos = False
        Peso1 = Math.Round(RsEntrada!kg_entrada, 0)
        If Peso1 = 0 Then
            If FlagMensaje = True Then
                MsgBox("No existe peso en esta entrada.")
            End If
            Return False
        End If

        Peso2 = 0
        For i = 1 To 12 ' nClasificaciones
            Peso3 = 0
            If IsNumeric(lblMuestra(i).Text) Then Peso3 = Math.Round(CDbl(lblMuestra(i).Text), 2)
            MuestraNueva(i) = Peso3
            Peso2 = Peso2 + Peso3
        Next
        If Peso2 = 0 Then
            If FlagMensaje = True Then
                MsgBox("No se ha introducido peso para ninguna calidad.")
            End If
            Return False
        End If
        Proporcion = Peso1 / Peso2
        PesoNuevo(0) = 0
        For i = 1 To 12 'nClasificaciones
            PesoNuevo(i) = Math.Round(MuestraNueva(i) * Proporcion, 0)
            PesoNuevo(0) = PesoNuevo(0) + PesoNuevo(i)
        Next
        Do While PesoNuevo(0) <> Peso1
            Quien = -1
            If Peso1 > PesoNuevo(0) Then Incremento = 1 Else Incremento = -1
            For i = 1 To nClasificaciones
                If MuestraNueva(i) > 0 Then
                    Factor = ((PesoNuevo(i) + Incremento) / MuestraNueva(i)) - Proporcion
                    If Quien = -1 Or (Incremento > 0 And Factor < MejorFactor) Or (Incremento < 0 And Factor > MejorFactor) Then
                        Quien = i
                        MejorFactor = Factor
                    End If
                End If
            Next
            If Quien > 0 Then
                PesoNuevo(Quien) = PesoNuevo(Quien) + Incremento
                PesoNuevo(0) = PesoNuevo(0) + Incremento
            Else
                MsgBox("No se puede cuadrar el peso.")
                Return False
            End If
        Loop
        For i = 1 To 12 ' nClasificaciones
            LblNueva(i).Text = Format(PesoNuevo(i), "###,##0")
        Next
        LblKilosCalculados.Text = Format(PesoNuevo(0), "###,##0")
        CalcularPesos = True
    End Function
    Private Sub CmdGrabar_Click()
        FlagMensaje = True
        If CalcularPesos() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If
        FlagMensaje = False
    End Sub
    Private Function GrabarEntrada() As Boolean
        Dim i As Integer
        Dim RsAlbaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaranClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim RsPendiente As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction
        Dim Campos() As String, Valores() As String

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try

            'entradas_albaranes
            RsAlbaran.Open("SELECT * FROM ENTRADAS_ALBARANES E WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran), ObjetoGlobal.cn, True,,,,,, trans)
            RsAlbaran!kg_sin_clasif = 0
            RsAlbaran!porcentaje_plaga = PorcentajeFinal 'nPorcentaje_plagas
            RsAlbaran!porcentaje_grand = PorcentajeGrande
            RsAlbaran!porcentaje_peq = PorcentajePequena
            RsAlbaran!porcentaje_recol = PorcentajeRecoleccion
            RsAlbaran!porcentaje_recol_i = PorcentajeRecoleccion
            RsAlbaran.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsAlbaran, "U"
            RsAlbaran.Close()

            'entradas_clasificacion
            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn, True,,,,,, trans)
            While RsAlbaranClasificacion.RecordCount > 0
                RsAlbaranClasificacion.MoveFirst()
                'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "D"
                RsAlbaranClasificacion.Delete()
            End While
            RsAlbaranClasificacion.Close()
            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To nClasificaciones
                RsAlbaranClasificacion.AddNew()
                RsAlbaranClasificacion!empresa = Trim(ObjetoGlobal.EmpresaActual)
                RsAlbaranClasificacion!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                RsAlbaranClasificacion!serie_albaran = Trim(RsEntrada!serie_albaran)
                RsAlbaranClasificacion!numero_albaran = RsEntrada!numero_albaran
                RsAlbaranClasificacion!Tipo_clasificacion = "LIQ"
                RsAlbaranClasificacion!codigo_calidad = aCalidades(i) ' aaaa bbbb
                RsAlbaranClasificacion!codigo_variedad = Trim(RsEntrada!codigo_variedad)
                RsAlbaranClasificacion!kg_muestra = Math.Round(MuestraNueva(i), 2)
                RsAlbaranClasificacion!kg_calidad = Math.Round(PesoNuevo(i), 0)
                RsAlbaranClasificacion.Update()
                'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "I"
            Next
            RsAlbaranClasificacion.Close()

            'Plagas
            If FlagGrabarPlagas = True Then
                SQL = "SELECT * FROM ENTRADAS_PLAGAS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
                RsPlagas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                While RsPlagas.RecordCount > 0
                    RsPlagas.MoveFirst()
                    RsPlagas.Delete()
                End While
                RsPlagas.Close()
                SQL = "SELECT * FROM ENTRADAS_PLAGAS WHERE 1=0"
                RsPlagas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                For i = 1 To CuantasPlagas
                    RsPlagas.AddNew()
                    RsPlagas!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsPlagas!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    RsPlagas!serie_albaran = Trim(SerieSeleccionada)
                    RsPlagas!numero_albaran = AlbaranSeleccionado
                    RsPlagas!tipo_plaga = QueTiposPlagas(i)
                    RsPlagas!codigo_plaga = QuePlagas(i)
                    If PorcentajePlaga(i) < 100 Then RsPlagas!porcentaje = PorcentajePlaga(i)
                    RsPlagas.Update()
                Next
                RsPlagas.Close()
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()

            ReDim Campos(4), Valores(4)
            Campos(0) = "porcentaje_plaga" : Valores(0) = PorcentajeFinal
            Campos(1) = "porcentaje_recol" : Valores(1) = PorcentajeRecoleccion
            Campos(2) = "porcentaje_recol_i" : Valores(2) = PorcentajeRecoleccion
            Campos(3) = "porcentaje_grand" : Valores(3) = PorcentajeGrande
            Campos(4) = "porcentaje_peq" : Valores(4) = PorcentajePequena
            oform.AsignarValores(oform.CnTabla01, Campos, Valores)

            GrabarEntrada = True
            AlbaranSeleccionado = -1
            SerieSeleccionada = ""

            MsgBox("Anotada la clasificacion del albarán :" + CStr(RsEntrada!numero_albaran))
            ImprimeAlbaran3(RsEntrada!serie_albaran, RsEntrada!numero_albaran, RsEntrada!codigo_variedad, RsEntrada!Transportista_terc, CDbl("0" & RsEntrada!peso_campo))
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("Error en el albarán a modificar")
            Return False
        End Try

        '
    End Function
    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String

        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub
    Private Sub PresentarNuevaClasificacion(Rs As cnRecordset.CnRecordset)
        Dim i As Long, j As Long, SQL As String

        lblMuestra(1).Text = KilosN(1) 'CStr(Rs!porc_especial)
        lblMuestra(2).Text = KilosN(2) 'CStr(Rs!porc_primera)
        lblMuestra(3).Text = KilosN(3) 'CStr(Rs!porc_segunda)
        lblMuestra(4).Text = KilosN(4) 'CStr(Rs!porc_tercera)
        lblMuestra(5).Text = KilosN(5) 'CStr(Rs!porc_cuarta)
        lblMuestra(6).Text = KilosN(6) 'CStr(Rs!porc_destrio)
        lblMuestra(7).Text = 0
        lblMuestra(8).Text = 0
        lblMuestra(9).Text = 0
        lblMuestra(10).Text = 0
        lblMuestra(11).Text = 0
        lblMuestra(12).Text = 0
        CalcularPesos()
    End Sub
    Private Function TransformarDesdeHexa(Cadena As String) As Long
        Dim Valor As Double, Digitos(8) As String, Pesos(8) As Double, j As Integer

        Pesos(1) = 16
        Pesos(2) = 1
        Pesos(3) = 16 ^ 3
        Pesos(4) = 16 ^ 2
        Pesos(5) = 16 ^ 5
        Pesos(6) = 16 ^ 4
        Pesos(7) = 16 ^ 7
        Pesos(8) = 16 ^ 6
        Valor = 0
        For j = 1 To 8
            Digitos(j) = Ctransformar(Mid(Cadena, j, 1))
            Valor = Valor + Digitos(j) * Pesos(j)
        Next
        TransformarDesdeHexa = Valor
    End Function
    Private Function Ctransformar(xx As String) As Double
        If xx >= "0" And xx <= "9" Then
            Ctransformar = CDbl(xx)
        ElseIf xx = "A" Then
            Ctransformar = 10
        ElseIf xx = "B" Then
            Ctransformar = 11
        ElseIf xx = "C" Then
            Ctransformar = 12
        ElseIf xx = "D" Then
            Ctransformar = 13
        ElseIf xx = "E" Then
            Ctransformar = 14
        ElseIf xx = "F" Then
            Ctransformar = 15
        Else
            Ctransformar = 0
        End If
    End Function

    Private Sub RedistribuirAjustePlaga(KgDistribuir As Double)
        Dim sSql As String, i As Integer, Muestra(6) As Double, Ahora(6) As Double
        Dim KgAsignados As Integer, KgsTotales As Double, KgsFactor As Double
        Dim nActual As Double, lPartesIguales As Boolean, nTodosloskg As Long, nTotalesKgTotales As Double
        Dim PorcentajeInicial As Double, KilosInicial As Double, KilosFinal As Double

        lPartesIguales = False
        If KgDistribuir = 0 Then Exit Sub

        nTodosloskg = KilosN(1) + KilosN(2) + KilosN(3) + KilosN(4) + KilosN(5) + KilosN(6)
        KgsTotales = KilosN(1) + KilosN(2) + KilosN(3) + KilosN(4) + KilosN(5)
        nTotalesKgTotales = 100
        If KgsTotales = 0 Then lPartesIguales = True
        KgsFactor = KgDistribuir / KgsTotales
        For i = 1 To 5
            If Not lPartesIguales Then
                KgAsignados = Math.Round(KilosN(i) * KgsFactor, 0)
            Else
                KgAsignados = Math.Round(KgDistribuir / 5, 0)
            End If
            Ahora(i) = KilosN(i) + KgAsignados
        Next
        Ahora(6) = KilosN(6) - KgDistribuir
        For i = 1 To 6
            KilosN(i) = Ahora(i)
        Next

        KgsTotales = RsEntrada!kg_a_liquidar
        PorcentajeInicial = nPorcentaje_plagas
        KilosInicial = Math.Round(KgsTotales * PorcentajeInicial / 100, 0)
        KilosFinal = KilosInicial - KgDistribuir
        PorcentajeFinal = Math.Round(100 * PorcentajeFinal / KgsTotales, 2)

    End Sub
    Private Function ObtenerKgPlaga(plaga As Integer, descuento As Double, Rs As cnRecordset.CnRecordset) As Long
        Dim i As Integer, porcentaje As Double

        ObtenerKgPlaga = 0
        For i = 1 To 19
            If Rs("codigo_plaga" + CStr(i)) = plaga Then
                If descuento > 0 Then
                    If IsNumeric(Rs("porc_plaga" + CStr(i))) Then
                        porcentaje = CDbl(Rs("porc_plaga" + CStr(i)))
                        ObtenerKgPlaga = Math.Round(descuento * porcentaje * KilosN(6) / 10000.0#, 0)
                        Exit For
                    End If
                End If
            End If
        Next

    End Function


    Private Sub ImportaClasificacion()
        Dim SQL As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsGrabar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        ', Stream As ADODB.Stream
        Dim Variedad As String
        Dim Descripcion As String
        Dim Kilos As Long
        Dim Servicios As String
        Dim ReferenciaV As String
        Dim VariedadID As String
        Dim i As Long
        Dim j As Integer
        Dim Calibres(36) As String
        Dim AlbaranCG As Long, Referencia As Long
        Dim RsReferencia As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPrevio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CopiaAlbaran(3) As Long
        Dim FechaAlbaran As Date
        Dim Capataz As Long
        Dim CodigoSocio As Long
        Dim CodigoCampo As Long
        Dim NombreSocio As String
        Dim RsCal As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Pesos(36) As Double
        Dim Unidades(36) As Long
        Dim RsComercial As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsDestrio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsDefecto As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CadenaCalibre As String
        Dim CalibresLibres(12) As String
        Dim TotalPesoComercial As Double
        Dim TotalPesoNoComercial As Double
        Dim TotalPesoDefecto As Double
        Dim TotalPesoPlaga As Double
        Dim TotalMuestra As Double
        Dim TotalKilos As Double
        Dim KilosPorCalidad(12) As Long
        Dim PorcentajesPorCalidad(12) As Single
        Dim KilosAlbaran(12) As Long
        Dim Calidad As Integer
        Dim Cadena2 As String
        Dim TotalClasificacionAlbaran As Integer
        Dim Diferencia As Long
        Dim CalidadRedondeo As Integer
        Dim DiferenciaMaxima As Single
        Dim FlagMaximo As Boolean
        Dim IndiceClasif As Integer
        Dim KilosPorCalibre(20) As Long
        Dim PorcentajesPorCalibre(20) As Single
        Dim KilosAlbaranCalibre(20) As Long
        Dim PesoC As Double
        Dim KilosC As Double
        Dim TotalKilosCalibre As Long
        Dim DiferenciaKilosCalibre As Long
        Dim AsignarImporte As Long
        Dim ArrayCalibres(22) As String
        Dim OrdDiferencia As Long
        Dim MaxCalibre As Long
        Dim PesoPruebaComercial As Long
        Dim PesoPruebaNoComercial As Long
        Dim PesoPruebaDefecto As Long
        Dim rsIgnorar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim ProcesarAlbaran As Boolean
        Dim sPathBase As String
        Dim HayDatos As Boolean
        Dim AlbaranCalibradora As String
        Dim cnConexion As SqlConnection

        sPathBase = "\\Srvv02\Grupos\Calibradora naranja\lot.mdb"
        Dim sConnString As String = "Provider=Microsoft.Jet.OLEDB.3.51; Data Source=" & sPathBase & ";"

        Try

            cnConexion = New SqlConnection
            cnConexion.ConnectionString = sConnString
            cnConexion.Open()

            AlbaranCalibradora = 0
            For i = 0 To 4
                If OptAlbaran(i).Checked = True Then
                    AlbaranCalibradora = Trim(OptAlbaran(i).Text)
                    Exit For
                End If
            Next

            SQL = "SELECT * From Reference WHERE (([Numero de bon]) = '" & AlbaranCalibradora & "') order by number desc;"
            RsCal.Open(SQL, cnConexion)

            HayDatos = False
            If RsCal.RecordCount = 0 Then
                RsCal.Close()
                cnConexion.Close()
                MsgBox("No existe el albarán indicado de la calibradora (" + CStr(AlbaranCalibradora) + ")")
                Exit Sub
            Else
                If RsCal.RecordCount > 1 Then
                    MsgBox("Hay más de un albarán de calibradora con el número indicado (" + CStr(AlbaranCalibradora) + "),  se procesará el último")
                End If
                HayDatos = True
                RsGrabar.Open("SELECT * FROM calibrado_roda WHERE 1=0", ObjetoGlobal.cn)

                ' Debe de haber sólo un registro
                rsIgnorar.Open("SELECT * FROM proceso_calibradoa WHERE referencia=" & RsCal!Number, ObjetoGlobal.cn)

                ProcesarAlbaran = True
                If Not rsIgnorar.EOF Then
                    If rsIgnorar!ignora_sn = "S" Then
                        ProcesarAlbaran = False
                    End If
                End If

                If ProcesarAlbaran Then
                    TotalPesoComercial = RsCal("Poids qualite 1").Value
                    TotalPesoPlaga = RsCal("Poids qualite 2").Value
                    TotalPesoDefecto = RsCal("Poids qualite 3").Value
                    TotalPesoNoComercial = TotalPesoPlaga
                    TotalMuestra = TotalPesoComercial + TotalPesoNoComercial + TotalPesoDefecto

                    RsComercial.Open("SELECT * FROM Qualite_1 WHERE number=" & RsCal!Number, cnConexion)
                    RsDestrio.Open("SELECT * FROM Qualite_2 WHERE number=" & RsCal!Number, cnConexion)
                    RsDefecto.Open("SELECT * FROM Qualite_3 WHERE number=" & RsCal!Number, cnConexion)

                    ' Comprobamos si los pesos están cuadrados
                    PesoPruebaComercial = 0
                    PesoPruebaNoComercial = 0
                    PesoPruebaDefecto = 0

                    For j = 1 To 12
                        PesoPruebaComercial = PesoPruebaComercial + RsComercial(1 + (j * 6)).Value
                        PesoPruebaNoComercial = PesoPruebaNoComercial + RsDestrio(1 + (j * 6)).Value
                        PesoPruebaDefecto = PesoPruebaDefecto + RsDefecto(1 + (j * 6)).Value
                    Next

                    If (TotalPesoComercial <> PesoPruebaComercial) Or (TotalPesoNoComercial <> PesoPruebaNoComercial) Or (TotalPesoDefecto <> PesoPruebaDefecto) Then
                        MsgBox("Los pesos del desglose de calidades no corresponde con el total de pesos. Codigo:" & RsCal!Number & " albaran " & RsCal("Numero de bon").Value)
                        rsIgnorar.AddNew()
                        rsIgnorar!Referencia = RsCal!Number
                        rsIgnorar!numero_albaran = RsCal("Numero de bon").Value
                        rsIgnorar!ignora_sn = "S"
                        rsIgnorar.Update()
                        GoTo ignorar
                    End If

                    If TotalMuestra <> RsCal("Poids total").Value Then
                        MsgBox("Los pesos de las dos calidades no corresponde con el peso total indicado por la clasificadora. Codigo:" & RsCal!Number & " albaran " & RsCal("Numero de bon").Value)
                        rsIgnorar.AddNew()
                        rsIgnorar!Referencia = RsCal!Number
                        rsIgnorar!numero_albaran = RsCal("Numero de bon").Value
                        rsIgnorar!ignora_sn = "S"
                        rsIgnorar.Update()
                        GoTo ignorar
                    End If

                    Referencia = RsCal!Number
                    lblAlbaran.Text = CStr(Referencia)
                    lblAlbaran.Refresh()
                    'DoEvents
                    CopiaAlbaran(1) = 0 : CopiaAlbaran(2) = 0 : CopiaAlbaran(3) = 0
                    RsReferencia.Open("SELECT * FROM PROCESO_CALIBRADOA WHERE REFERENCIA = " + CStr(Referencia), ObjetoGlobal.cn)
                    If RsReferencia.RecordCount > 0 Then
                        If Trim(RsReferencia!actualizado_sn) = "S" Then
                            RsReferencia.Close()
                            GoTo ignorar
                        End If
                        If Trim(RsReferencia!ignora_sn) = "S" Or RsReferencia!numero_albaran = 0 Then
                            RsPrevio.Open("SELECT * FROM calibrado_roda WHERE REFERENCIA = " + CStr(Referencia), ObjetoGlobal.cn)
                            Do While RsPrevio.RecordCount > 0
                                RsPrevio.MoveFirst()
                                RsPrevio.Delete()
                            Loop
                            RsPrevio.Close()
                            RsReferencia!actualizado_sn = "S"
                            RsReferencia.Update()
                            RsReferencia.Close()
                            GoTo ignorar
                        End If
                        AlbaranCG = RsReferencia!numero_albaran
                        CopiaAlbaran(1) = RsReferencia!numero_albaran
                        If RsReferencia!numero_albaran2 > 0 Then
                            CopiaAlbaran(2) = RsReferencia!numero_albaran2
                            If RsReferencia!numero_albaran3 > 0 Then CopiaAlbaran(3) = RsReferencia!numero_albaran3
                        End If
                    Else
                        If RsCal("Numero de bon").Value < 20000 Then
                            AlbaranCG = RsCal("Numero de bon").Value
                        ElseIf RsCal("Numero de bon").Value >= 60001 And RsCal("Numero de bon").Value <= 69999 Then
                            AlbaranCG = RsCal("Numero de bon").Value - 60000
                        ElseIf RsCal("Numero de bon").Value >= 600001 And RsCal("Numero de bon").Value <= 609999 Then
                            AlbaranCG = RsCal("Numero de bon").Value - 600000
                        ElseIf RsCal("Numero de bon").Value >= 6000001 And RsCal("Numero de bon").Value <= 6009999 Then
                            AlbaranCG = RsCal("Numero de bon").Value - 6000000
                        ElseIf RsCal("Numero de bon").Value >= 60000001 And RsCal("Numero de bon").Value <= 60009999 Then
                            AlbaranCG = RsCal("Numero de bon").Value - 60000000
                        ElseIf RsCal("Numero de bon").Value >= 1000000 And RsCal("Numero de bon").Value <= 1009999 Then
                            AlbaranCG = 0
                        Else
                            AlbaranCG = 0
                        End If
                    End If

                    RsReferencia.Close()
                    RsPrevio.Open("SELECT * FROM calibrado_roda WHERE EMPRESA = '1' AND EJERCICIO = '" & ObjetoGlobal.EjercicioActual & "' AND SERIE_ALBARAN = '" & Trim(SerieSeleccionada) & "' AND NUMERO_ALBARAN = " + CStr(AlbaranCG), ObjetoGlobal.cn)
                    While RsPrevio.RecordCount > 0
                        RsPrevio.MoveFirst()
                        RsPrevio.Delete()
                    End While
                    RsPrevio.Close()

                    If AlbaranCG < 100000 Then
                        RsAlbaran.Open("SELECT * FROM ENTRADAS_ALBARANES JOIN VARIEDADES ON VARIEDADES.EMPRESA = ENTRADAS_ALBARANES.EMPRESA AND VARIEDADES.CODIGO_VARIEDAD = ENTRADAS_ALBARANES.CODIGO_VARIEDAD WHERE ENTRADAS_ALBARANES.EMPRESA = '1' AND ENTRADAS_ALBARANES.EJERCICIO = '" & ObjetoGlobal.EjercicioActual & "' AND SERIE_ALBARAN = '" & Trim(SerieSeleccionada) & "' AND NUMERO_ALBARAN = '" + CStr(AlbaranCG) + "'", ObjetoGlobal.cn)
                        If RsAlbaran.RecordCount = 0 Then
                            RsAlbaran.Close()
                            GoTo ignorar
                        End If
                        If Strings.Left(RsAlbaran!codigo_variedad, 2) <> "01" And Strings.Left(RsAlbaran!codigo_variedad, 2) <> "02" Then
                            RsAlbaran.Close()
                            GoTo ignorar
                        End If
                        Variedad = RsAlbaran!codigo_variedad
                        Descripcion = RsAlbaran!Descripcion
                        Kilos = RsAlbaran!kg_a_liquidar
                        'Servicios = Trim(RsAlbaran!activo_servicios)
                        FechaAlbaran = CDate(RsAlbaran!fecha_entrada)
                        '            Capataz = CLng(RsAlbaran!Capataz)
                        '            CodigoSocio = RsAlbaran!codigo_socio
                        '            NombreSocio = Trim("" & RsAlbaran!nombre_socio) + ", " + Trim("" & RsAlbaran!apellidos_socio)
                        '            CodigoCampo = RsAlbaran!codigo_campo
                        RsAlbaran.Close()
                    Else
                        Variedad = ""
                        Descripcion = ""
                        Kilos = 0
                        Servicios = "N"
                        FechaAlbaran = CDate("1/1/2000")
                        Capataz = 0
                        CodigoSocio = 0
                        NombreSocio = ""
                        CodigoCampo = 0
                    End If
                    VariedadID = Trim("" & RsCal("Zone 6").Value)
                    ReferenciaV = ""
                    For j = 1 To 36 : Calibres(j) = "" : Next

                    For j = 1 To 12
                        Calibres(j) = "" & RsCal(10 + j).Value
                        Pesos(j) = RsComercial(1 + (j * 6)).Value
                        Unidades(j) = RsComercial(73 + (j * 6)).Value
                        Calibres(j + 12) = "" & RsCal(10 + j).Value
                        Pesos(j + 12) = RsDestrio(1 + (j * 6)).Value
                        Unidades(j + 12) = RsDestrio(73 + (j * 6)).Value
                        ' Ver esto
                        Calibres(j + 24) = "" & RsCal(10 + j).Value
                        Pesos(j + 24) = RsDefecto(1 + (j * 6)).Value
                        Unidades(j + 24) = RsDefecto(73 + (j * 6)).Value
                    Next

                    RsGrabar.AddNew()
                    RsGrabar!empresa = "1"
                    RsGrabar!Ejercicio = ObjetoGlobal.EjercicioActual
                    RsGrabar!serie_albaran = Trim(SerieSeleccionada)
                    RsGrabar!numero_albaran = AlbaranCG
                    RsGrabar!Referencia = Referencia
                    RsGrabar!VariedadID = Strings.Left(Trim("" & RsCal("Zone 6").Value), 10)
                    RsGrabar!variedadref = Strings.Left(Trim("" & RsCal("Zone 6").Value), 10)
                    RsGrabar!codigo_variedad = Trim(Variedad)
                    RsGrabar!Descripcion = Trim(Descripcion)
                    RsGrabar!observaciones = Trim("" & RsCal("Commentaire").Value)
                    ProcesarPlagas(RsCal, RsGrabar)
                    RsGrabar!kilos_albaran = Kilos
                    'RsGrabar!activo_servicios = Trim(Servicios)
                    RsGrabar!fecha_albaran = CDate(FechaAlbaran)
                    '        RsGrabar!Capataz = Capataz
                    '        RsGrabar!codigo_socio = CodigoSocio
                    '        RsGrabar!nombre_socio = Trim(NombreSocio)
                    '        RsGrabar!codigo_campo = CodigoCampo

                    For j = 1 To 12
                        RsGrabar("categoria" + Format((j * 3) - 2, "00")).Value = Trim(Calibres(j))
                        RsGrabar("categoria" + Format((j * 3) - 1, "00")).Value = "DEST:" & Trim(Calibres(j + 12))
                        RsGrabar("categoria" + Format((j * 3), "00")).Value = "DEF:" & Trim(Calibres(j + 24))
                        RsGrabar("peso" + Format((j * 3) - 2, "00")).Value = Pesos(j)
                        RsGrabar("peso" + Format((j * 3) - 1, "00")).Value = Pesos(j + 12)
                        RsGrabar("peso" + Format((j * 3), "00")).Value = Pesos(j + 24)
                        RsGrabar("unidades" + Format((j * 3) - 2, "00")).Value = Unidades(j)
                        RsGrabar("unidades" + Format((j * 3) - 1, "00")).Value = Unidades(j + 12)
                        RsGrabar("unidades" + Format((j * 3), "00")).Value = Unidades(j + 24)
                    Next

                    'CALIDADES ALBARAN
                    TotalKilos = 0
                    For j = 0 To 10 : KilosPorCalidad(j) = 0 : PorcentajesPorCalidad(j) = 0 : KilosAlbaran(j) = 0 : Next

                    For j = 1 To 36 Step 3
                        TotalKilos = TotalKilos + Math.Round(RsGrabar("peso" + Format(j, "00")).Value, 0)
                        TotalKilos = TotalKilos + Math.Round(RsGrabar("peso" + Format(j + 1, "00")).Value, 0)
                        TotalKilos = TotalKilos + Math.Round(RsGrabar("peso" + Format(j + 2, "00")).Value, 0)
                    Next



                    For j = 1 To 36
                        If Math.Round(RsGrabar("peso" + Format(j, "00")), 0) > 0 Then
                            Calidad = 0
                            Cadena2 = UCase(Trim(RsGrabar("categoria" + Format(j, "00"))))
                            If Strings.Left(Cadena2, 4) = "DEST" Or Strings.Left(Cadena2, 3) = "DEF" Then
                                Calidad = cuantascalidades
                            Else
                                If Strings.Right(Cadena2, 2) = "PR" Or Strings.Right(Cadena2, 2) = "*1" Then
                                    Calidad = 1
                                ElseIf Strings.Right(Cadena2, 2) = "SE" Or Strings.Right(Cadena2, 2) = "*2" Then
                                    Calidad = 2
                                ElseIf Strings.Right(Cadena2, 2) = "TE" Or Strings.Right(Cadena2, 2) = "*3" Then
                                    Calidad = 3
                                ElseIf Strings.Right(Cadena2, 2) = "CU" Or Strings.Right(Cadena2, 2) = "*4" Then
                                    Calidad = 4
                                ElseIf Strings.Right(Cadena2, 2) = "NC" Then
                                    Calidad = cuantascalidades
                                ElseIf Mid(Strings.Right(Cadena2, 2), 1, 1) = "*" And Strings.Right(Cadena2, 1) >= "5" And Strings.Right(Cadena2, 1) <= "9" Then
                                    Calidad = CInt(Strings.Right(Cadena2, 1))
                                ElseIf Mid(Strings.Right(Cadena2, 2), 1, 1) = "&" And Strings.Right(Cadena2, 1) >= "0" And Strings.Right(Cadena2, 1) <= "2" Then
                                    Calidad = 10 + CInt(Strings.Right(Cadena2, 1))
                                End If
                            End If
                            If Calidad = 0 Then
                                '           msgbox( "Albaran sin clasificar: " + Trim(Rs!numero_albaran)
                            Else
                                KilosPorCalidad(Calidad) = KilosPorCalidad(Calidad) + Math.Round(RsGrabar("peso" + Format(j, "00")).Value, 0)
                            End If
                        End If
                    Next

                    If TotalKilos > 0 Then
                        For j = 1 To 12
                            PorcentajesPorCalidad(j) = KilosPorCalidad(j) * 100.0# / TotalKilos
                        Next
                    End If

                    RsGrabar!peso_01 = KilosPorCalidad(1)
                    RsGrabar!peso_02 = KilosPorCalidad(2)
                    RsGrabar!peso_03 = KilosPorCalidad(3)
                    RsGrabar!peso_04 = KilosPorCalidad(4)
                    RsGrabar!peso_05 = KilosPorCalidad(5)
                    RsGrabar!peso_06 = KilosPorCalidad(6)
                    ' Nuevo
                    RsGrabar!peso_07 = KilosPorCalidad(7)
                    RsGrabar!peso_08 = KilosPorCalidad(8)
                    RsGrabar!peso_09 = KilosPorCalidad(9)
                    RsGrabar!peso_10 = KilosPorCalidad(10)
                    RsGrabar!peso_11 = KilosPorCalidad(11)
                    RsGrabar!peso_12 = KilosPorCalidad(12)
                    ' fin de lo nuevo


                    RsGrabar!porc_01 = Math.Round(PorcentajesPorCalidad(1), 2)
                    RsGrabar!porc_02 = Math.Round(PorcentajesPorCalidad(2), 2)
                    RsGrabar!porc_03 = Math.Round(PorcentajesPorCalidad(3), 2)
                    RsGrabar!porc_04 = Math.Round(PorcentajesPorCalidad(4), 2)
                    RsGrabar!porc_05 = Math.Round(PorcentajesPorCalidad(5), 2)
                    RsGrabar!porc_06 = Math.Round(PorcentajesPorCalidad(6), 2)
                    'Nuevo
                    RsGrabar!porc_07 = Math.Round(PorcentajesPorCalidad(7), 2)
                    RsGrabar!porc_08 = Math.Round(PorcentajesPorCalidad(8), 2)
                    RsGrabar!porc_09 = Math.Round(PorcentajesPorCalidad(9), 2)
                    RsGrabar!porc_10 = Math.Round(PorcentajesPorCalidad(10), 2)
                    RsGrabar!porc_11 = Math.Round(PorcentajesPorCalidad(11), 2)
                    RsGrabar!porc_12 = Math.Round(PorcentajesPorCalidad(12), 2)


                    If RsGrabar!kilos_albaran > 0 Then
                        TotalClasificacionAlbaran = 0
                        For j = 1 To 6
                            KilosAlbaran(j) = Math.Round(PorcentajesPorCalidad(j) * RsGrabar!kilos_albaran / 100.0#, 0)
                            TotalClasificacionAlbaran = TotalClasificacionAlbaran + KilosAlbaran(j)
                        Next
                        Diferencia = RsGrabar!kilos_albaran - TotalClasificacionAlbaran
                        Do While Diferencia <> 0
                            CalidadRedondeo = -1
                            DiferenciaMaxima = 0
                            For j = 1 To 6
                                FlagMaximo = False
                                If CalidadRedondeo = -1 Then
                                    FlagMaximo = True
                                Else
                                    If Diferencia > 0 And KilosAlbaran(j) - (PorcentajesPorCalidad(j) * RsGrabar!kilos_albaran / 100.0#) < DiferenciaMaxima Then
                                        FlagMaximo = True
                                    ElseIf Diferencia < 0 And KilosAlbaran(j) - (PorcentajesPorCalidad(j) * RsGrabar!kilos_albaran / 100.0#) > DiferenciaMaxima Then
                                        FlagMaximo = True
                                    End If
                                End If
                                If FlagMaximo = True Then
                                    CalidadRedondeo = j
                                    DiferenciaMaxima = KilosAlbaran(j) - (PorcentajesPorCalidad(j) * RsGrabar!kilos_albaran / 100.0#)
                                End If
                            Next
                            If Diferencia > 0 Then
                                KilosAlbaran(CalidadRedondeo) = KilosAlbaran(CalidadRedondeo) + 1
                                Diferencia = Diferencia - 1
                            Else
                                KilosAlbaran(CalidadRedondeo) = KilosAlbaran(CalidadRedondeo) - 1
                                Diferencia = Diferencia + 1
                            End If
                        Loop
                        RsGrabar!kilos_01 = KilosAlbaran(1)
                        RsGrabar!kilos_02 = KilosAlbaran(2)
                        RsGrabar!kilos_03 = KilosAlbaran(3)
                        RsGrabar!kilos_04 = KilosAlbaran(4)
                        RsGrabar!kilos_05 = KilosAlbaran(5)
                        RsGrabar!kilos_06 = KilosAlbaran(6)
                        ' Nuevo
                        RsGrabar!kilos_07 = KilosAlbaran(7)
                        RsGrabar!kilos_08 = KilosAlbaran(8)
                        RsGrabar!kilos_09 = KilosAlbaran(9)
                        RsGrabar!kilos_10 = KilosAlbaran(10)
                        RsGrabar!kilos_11 = KilosAlbaran(11)
                        RsGrabar!kilos_12 = KilosAlbaran(12)
                    Else
                        RsGrabar!kilos_01 = 0
                        RsGrabar!kilos_02 = 0
                        RsGrabar!kilos_03 = 0
                        RsGrabar!kilos_04 = 0
                        RsGrabar!kilos_05 = 0
                        RsGrabar!kilos_06 = 0
                        ' Nuevo
                        RsGrabar!kilos_07 = 0
                        RsGrabar!kilos_08 = 0
                        RsGrabar!kilos_09 = 0
                        RsGrabar!kilos_10 = 0
                        RsGrabar!kilos_11 = 0
                        RsGrabar!kilos_12 = 0
                    End If
                    IndiceClasif = 0
                    For j = 1 To 12
                        IndiceClasif = IndiceClasif + (j - 1) * 1.0# * PorcentajesPorCalidad(j)
                    Next
                    IndiceClasif = IndiceClasif / 5
                    RsGrabar!indice_clasif = IndiceClasif

                    ' Calibre
                    For j = 1 To 12
                        CadenaCalibre = Replace(Trim(Calibres(j)), " ", "")
                        CadenaCalibre = Replace(CadenaCalibre, "NC", "")
                        CadenaCalibre = Replace(CadenaCalibre, "CU", "")
                        CadenaCalibre = Replace(CadenaCalibre, "TE", "")
                        CadenaCalibre = Replace(CadenaCalibre, "SE", "")
                        CadenaCalibre = Replace(CadenaCalibre, "PR", "")

                        Select Case LCase(CadenaCalibre)
                ' Naranjas
                            Case "8"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre8 = IIf(IsDBNull(RsGrabar!peso_calibre8), 0, RsGrabar!peso_calibre8) + PesoC
                                RsGrabar!porc_calibre8 = Math.Round(RsGrabar!peso_calibre8 * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre8 / 100, 2)
                                RsGrabar!kilos_calibre8 = KilosC

                            Case "7"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre7 = RsGrabar!peso_calibre7 + PesoC
                                RsGrabar!porc_calibre7 = Math.Round(RsGrabar!peso_calibre7 * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre7 / 100, 2)
                                RsGrabar!kilos_calibre7 = KilosC

                            Case "6"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre6 = RsGrabar!peso_calibre6 + PesoC
                                RsGrabar!porc_calibre6 = Math.Round(RsGrabar!peso_calibre6 * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre6 / 100, 2)
                                RsGrabar!kilos_calibre6 = KilosC

                            Case "5"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre5 = RsGrabar!peso_calibre5 + PesoC
                                RsGrabar!porc_calibre5 = Math.Round(RsGrabar!peso_calibre5 * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre5 / 100, 2)
                                RsGrabar!kilos_calibre5 = KilosC

                            Case "4"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre4 = RsGrabar!peso_calibre4 + PesoC
                                RsGrabar!porc_calibre4 = Math.Round(RsGrabar!peso_calibre4 * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre4 / 100, 2)
                                RsGrabar!kilos_calibre4 = KilosC

                            Case "3"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre3 = RsGrabar!peso_calibre3 + PesoC
                                RsGrabar!porc_calibre3 = Math.Round(RsGrabar!peso_calibre3 * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre3 / 100, 2)
                                RsGrabar!kilos_calibre3 = KilosC

                            Case "2"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre2 = RsGrabar!peso_calibre2 + PesoC
                                RsGrabar!porc_calibre2 = Math.Round((RsGrabar!peso_calibre2) * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre2 / 100, 2)
                                RsGrabar!kilos_calibre2 = KilosC

                            Case "1"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre1 = RsGrabar!peso_calibre1 + PesoC
                                RsGrabar!porc_calibre1 = Math.Round((RsGrabar!peso_calibre1) * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre1 / 100, 2)
                                RsGrabar!kilos_calibre1 = KilosC

                            Case "1x"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre1x = RsGrabar!peso_calibre1x + PesoC
                                RsGrabar!porc_calibre1x = Math.Round((RsGrabar!peso_calibre1x) * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre1x / 100, 2)
                                RsGrabar!kilos_calibre1x = KilosC

                            Case "1xx", "2x"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre1xx = RsGrabar!peso_calibre1xx + PesoC
                                RsGrabar!porc_calibre1xx = Math.Round((RsGrabar!peso_calibre1xx) * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre1xx / 100, 2)
                                RsGrabar!kilos_calibre1xx = KilosC

                            Case "1xxx", "3x"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre1xxx = RsGrabar!peso_calibre1xxx + PesoC
                                RsGrabar!porc_calibre1xxx = Math.Round((RsGrabar!peso_calibre1xxx) * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre1xxx / 100, 2)
                                RsGrabar!kilos_calibre1xxx = KilosC

                            Case "100"
                                PesoC = (Pesos(j) + Pesos(j + 12) + Pesos(j + 24))
                                RsGrabar!peso_calibre100 = RsGrabar!peso_calibre100 + PesoC
                                RsGrabar!porc_calibre100 = Math.Round((RsGrabar!peso_calibre100) * 100 / TotalMuestra, 2)
                                KilosC = Math.Round(Kilos * RsGrabar!porc_calibre100 / 100, 2)
                                RsGrabar!kilos_calibre100 = KilosC
                                RsGrabar!kilos_calibre100 = Math.Round(Kilos * RsGrabar!porc_calibre100 / 100, 2)
                        End Select
                    Next

                    For j = 1 To 12
                        CadenaCalibre = Replace(UCase(Trim(Calibres(j))), " ", "")
                        If Trim(CadenaCalibre) = "5NC" Then
                            RsGrabar!peso_nc_peq = RsGrabar!peso_nc_peq + Pesos(j)
                            RsGrabar!peso_nc_defecto = RsGrabar!peso_nc_defecto + Pesos(j + 12)
                            RsGrabar!peso_nc_recolec = RsGrabar!peso_nc_recolec + Pesos(j + 24)


                        ElseIf Trim(CadenaCalibre) = "8NC" Then
                            RsGrabar!peso_nc_peq = RsGrabar!peso_nc_peq + Pesos(j)
                            RsGrabar!peso_nc_defecto = RsGrabar!peso_nc_defecto + Pesos(j + 12)
                            RsGrabar!peso_nc_recolec = RsGrabar!peso_nc_recolec + Pesos(j + 24)

                        ElseIf Trim(CadenaCalibre) = "100NC" Then
                            RsGrabar!peso_nc_grande = RsGrabar!peso_nc_grande + Pesos(j)
                            RsGrabar!peso_nc_defecto = RsGrabar!peso_nc_defecto + Pesos(j + 12)
                            RsGrabar!peso_nc_recolec = RsGrabar!peso_nc_recolec + Pesos(j + 24)

                        ElseIf Trim(CadenaCalibre) = "3XNC" Then
                            RsGrabar!peso_nc_grande = RsGrabar!peso_nc_grande + Pesos(j)
                            RsGrabar!peso_nc_defecto = RsGrabar!peso_nc_defecto + Pesos(j + 12)
                            RsGrabar!peso_nc_recolec = RsGrabar!peso_nc_recolec + Pesos(j + 24)
                        Else
                            RsGrabar!peso_nc_defecto = RsGrabar!peso_nc_defecto + Pesos(j + 12)
                            RsGrabar!peso_nc_recolec = RsGrabar!peso_nc_recolec + Pesos(j + 24)
                        End If

                    Next

                    RsGrabar!porc_nc_peq = Math.Round(RsGrabar!peso_nc_peq * 100 / TotalMuestra, 2)
                    RsGrabar!porc_nc_grande = Math.Round(RsGrabar!peso_nc_grande * 100 / TotalMuestra, 2)
                    RsGrabar!kilos_nc_peq = Math.Round(Kilos * RsGrabar!porc_nc_peq / 100, 2)
                    RsGrabar!kilos_nc_grande = Math.Round(Kilos * RsGrabar!porc_nc_grande / 100, 2)
                    RsGrabar!porc_nc_defecto = Math.Round(RsGrabar!peso_nc_defecto * 100 / TotalMuestra, 2)
                    RsGrabar!porc_nc_recolec = Math.Round(RsGrabar!peso_nc_recolec * 100 / TotalMuestra, 2)
                    RsGrabar!kilos_nc_defecto = Math.Round(Kilos * RsGrabar!porc_nc_defecto / 100, 2)
                    RsGrabar!kilos_nc_recolec = Math.Round(Kilos * RsGrabar!porc_nc_recolec / 100, 2)

                    PorcentajeFinal = RsGrabar!porc_nc_defecto
                    PorcentajeGrande = RsGrabar!porc_nc_grande
                    PorcentajePequena = RsGrabar!porc_nc_peq
                    PorcentajeRecoleccion = RsGrabar!porc_nc_recolec

                    ' Redondeo y control
                    TotalKilosCalibre = 0
                    DiferenciaKilosCalibre = 0
                    AsignarImporte = 0
                    OrdDiferencia = 0
                    MaxCalibre = 0

                    ArrayCalibres(1) = "kilos_calibre1"
                    ArrayCalibres(2) = "kilos_calibre2"
                    ArrayCalibres(3) = "kilos_calibre3"
                    ArrayCalibres(4) = "kilos_calibre4"
                    ArrayCalibres(5) = "kilos_calibre5"
                    ArrayCalibres(6) = "kilos_calibre6"
                    ArrayCalibres(7) = "kilos_calibre7"
                    ArrayCalibres(8) = "kilos_calibre8"
                    ArrayCalibres(9) = "kilos_calibre9"
                    ArrayCalibres(10) = "kilos_calibre10"
                    ArrayCalibres(11) = "kilos_calibre1x"
                    ArrayCalibres(12) = "kilos_calibre1xx"
                    ArrayCalibres(13) = "kilos_calibre1xxx"
                    ArrayCalibres(14) = "kilos_calibre100"

                    For i = 1 To 14
                        TotalKilosCalibre = TotalKilosCalibre + RsGrabar(ArrayCalibres(i)).Value
                        MaxCalibre = Mayor(MaxCalibre, RsGrabar(ArrayCalibres(i)).Value)
                        If MaxCalibre = RsGrabar(ArrayCalibres(i)).Value Then
                            OrdDiferencia = i
                        End If
                    Next

                    DiferenciaKilosCalibre = RsGrabar!kilos_albaran - TotalKilosCalibre
                    If DiferenciaKilosCalibre > 10 Then
                        MsgBox("Error en el cálculo de Kilosgramos Calibre del albarán " & AlbaranCG)
                    End If
                    If DiferenciaKilosCalibre <> 0 Then
                        RsGrabar(ArrayCalibres(OrdDiferencia)).Value = RsGrabar(ArrayCalibres(OrdDiferencia)).Value + DiferenciaKilosCalibre
                    End If

                    RsGrabar.Update()

ignorar:
                    RsComercial.Close()
                    RsDestrio.Close()
                    RsDefecto.Close()
                End If
                rsIgnorar.Close()
                RsCal.MoveNext()
            End If
            RsGrabar.Close()
            cnConexion.Close()

            ' Ajuste traspaso de grande a otra categoría aaa
            AjustesPredeterminados(PorcentajesPorCalidad, KilosAlbaran)

            ' Clasificación
            For i = 1 To 12
                MuestraNueva(i) = Math.Round(PorcentajesPorCalidad(i), 2)
                PesoNuevo(i) = KilosAlbaran(i)
            Next
            For j = 1 To 12
                LblNueva(j).Text = KilosAlbaran(j)
                lblMuestra(j).Text = Math.Round(PorcentajesPorCalidad(j), 2)
            Next

        Catch ex As Exception
            MsgBox("Imposible establecer conexión con la clasificadora: " & ex.Message)
        End Try

        Return

        'malaconexion:
        '        Dim errLoop As error
        'Dim StrTmp As String
        '        Dim strError As String

        ' Set Errs1 = CNN.Errors
        ' i = 0
        '        For Each errLoop In Errs1
        '            With errLoop
        '                StrTmp = StrTmp & vbCrLf & "Error #" & i & ":"
        '                StrTmp = StrTmp & vbCrLf & "   ADO Error   #" & .Number
        '                StrTmp = StrTmp & vbCrLf & "   Description  " & .Description
        '                StrTmp = StrTmp & vbCrLf & "   Source       " & .Source
        '                i = i + 1
        '            End With
        '        Next

        '        MsgBox(StrTmp)

        '        msgbox( "Imposible establecer conexión con la clasificadora: " & Err.Description
    End Sub
    Private Function Mayor(ByVal a As Long, ByVal b As Long) As Long
        If a > b Then
            Mayor = a
        Else
            Mayor = b
        End If
    End Function



    Private Sub ProcesarPlagas(RsCal As cnRecordset.CnRecordset, RsGrabar As cnRecordset.CnRecordset)
        Dim Cadena As String
        Dim Cadena2 As String
        Dim CadP As String
        Dim CadN As String
        Dim Plagas(20) As String
        Dim DesPlagas(20) As String
        Dim Porcentajes(20) As Integer, PorcPlagas(20) As Integer, CodigosPlaga(20) As Integer
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim longitud As Integer
        Dim Puntero As Integer
        Dim FlagPrimeroNumeros As Boolean
        Dim TT As String
        Dim SQL As String
        Dim FlagExiste As Boolean
        Dim FlagTodas As Boolean
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim lNoSeguir As Boolean
        Dim ii As Integer
        Dim CadenaPlagas As String
        Dim CadenaRecoleccion As String
        Dim Pos As Integer
        Dim Orden As Integer
        Dim Incrementa As Boolean
        Dim TotalPlagas As Double
        Dim AjustaPlagas As Boolean
        Dim difPlagas As Double
        Dim OrdenMayor As Integer
        Dim MayorPlaga As Double

        For i = 1 To 20
            Plagas(i) = ""
            DesPlagas(i) = ""
            Porcentajes(i) = 0
            CodigosPlaga(i) = 0
        Next

        FlagTodas = True
        Cadena2 = Trim("" & RsCal("Commentaire").Value)

        Cadena2 = Replace(Cadena2, "'", "")
        Cadena2 = Replace(Cadena2, Chr(34), "")
        Cadena2 = Replace(Cadena2, "`", "")
        Cadena2 = Replace(Cadena2, "¨", "")
        Cadena2 = Replace(Cadena2, "´", "")
        Cadena2 = Replace(Cadena2, "^", "")

        Pos = InStr(1, Cadena2, "/")

        If Pos > 0 Then
            If Pos > 1 Then
                CadenaPlagas = Strings.Left(Cadena2, Pos - 1)
            Else
                CadenaPlagas = ""
            End If
            If Pos < Len(Trim(Cadena2)) Then
                CadenaRecoleccion = Mid(Cadena2, Pos + 1)
            Else
                CadenaRecoleccion = ""
            End If
        Else
            CadenaPlagas = Trim(Cadena2)
        End If

        If RsGrabar!numero_albaran < 99999 Then ' antes era: FlagTodas = True And ...
            SQL = "SELECT * FROM entradas_plagas_pro WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO = '" & ObjetoGlobal.EjercicioActual & "' AND SERIE_ALBARAN = '" & RsGrabar!serie_albaran & "' AND NUMERO_ALBARAN = " + CStr(RsGrabar!numero_albaran)
            RsPlagas.Open(SQL, ObjetoGlobal.cn, True)
            While RsPlagas.RecordCount > 0
                RsPlagas.MoveFirst()
                RsPlagas.Delete()
            End While
            RsPlagas.Close()
        End If

        If Trim(CadenaPlagas) > "" Then
            If Trim(CadenaPlagas) > "" Then
                Cadena2 = CadenaPlagas
                Cadena = ""
                For i = 1 To Len(Cadena2)
                    If Mid(Cadena2, i, 1) = "-" Or Mid(Cadena2, i, 1) = " " Or Mid(Cadena2, i, 1) = "%" Or Mid(Cadena2, i, 1) = "," Or Mid(Cadena2, i, 1) = "/" Then
                        Cadena2 = Cadena2
                    Else
                        Cadena = Cadena + Mid(Cadena2, i, 1)
                    End If
                Next
                If Strings.Left(Cadena, 1) >= "0" And Strings.Left(Cadena, 1) <= "9" Then
                    FlagPrimeroNumeros = True
                Else
                    FlagPrimeroNumeros = False
                End If
                Puntero = 1
                CadP = "" : CadN = ""
                If FlagPrimeroNumeros = True Then
                    For i = 1 To Len(Cadena)
                        If Mid(Cadena, i, 1) >= "0" And Mid(Cadena, i, 1) <= "9" Then
                            If Trim(CadP) > "" Then
                                lNoSeguir = False
                                For ii = 1 To Puntero - 1
                                    If Plagas(ii) = CadP Then
                                        lNoSeguir = True
                                    End If
                                Next
                                If Not lNoSeguir Then
                                    Plagas(Puntero) = CadP
                                    Porcentajes(Puntero) = CadN
                                    Puntero = Puntero + 1
                                End If
                                CadP = ""
                                CadN = CStr(Mid(Cadena, i, 1))
                            Else
                                CadN = Trim(CadN) + CStr(Mid(Cadena, i, 1))
                            End If
                        Else
                            CadP = Trim(CadP) + CStr(Mid(Cadena, i, 1))
                        End If
                    Next
                    If Trim(CadP) > "" Then
                        lNoSeguir = False
                        For ii = 1 To Puntero - 1
                            If Plagas(ii) = CadP Then
                                lNoSeguir = True
                            End If
                        Next
                        If Not lNoSeguir Then
                            Plagas(Puntero) = CadP
                            If Trim(CadN) = "" Then CadN = "0"
                            Porcentajes(Puntero) = CadN
                        End If
                    End If
                Else
                    For i = 1 To Len(Cadena)
                        If Not (Mid(Cadena, i, 1) >= "0" And Mid(Cadena, i, 1) <= "9") Then
                            If Trim(CadN) > "" Then
                                lNoSeguir = False
                                For ii = 1 To Puntero - 1
                                    If Plagas(ii) = CadP Then
                                        lNoSeguir = True
                                    End If
                                Next
                                If Not lNoSeguir Then
                                    Plagas(Puntero) = CadP
                                    Porcentajes(Puntero) = CadN
                                End If
                                Puntero = Puntero + 1
                                CadN = ""
                                CadP = CStr(Mid(Cadena, i, 1))
                            Else
                                CadP = Trim(CadP) + CStr(Mid(Cadena, i, 1))
                            End If
                        Else
                            CadN = Trim(CadN) + CStr(Mid(Cadena, i, 1))
                        End If
                    Next
                    If Trim(CadP) > "" Then
                        lNoSeguir = False
                        For ii = 1 To Puntero - 1
                            If Plagas(ii) = CadP Then
                                lNoSeguir = True
                            End If
                        Next
                        If Not lNoSeguir Then
                            Plagas(Puntero) = CadP
                            If Trim(CadN) = "" Then CadN = "0"
                            Porcentajes(Puntero) = CadN
                        End If
                    End If
                End If
            End If
            'TT = Trim(Cadena2)

            For i = 1 To Puntero
                Plagas(i) = LCase(Plagas(i))
            Next
            Orden = 0
            Incrementa = False
            For i = 1 To Puntero

                Orden = Orden + 1
                If Incrementa Then
                    Orden = Orden + 1
                End If
                Incrementa = False

                Cadena = "plaga" + CStr(Orden)
                FlagExiste = False
                SQL = "SELECT PROCESO_CALIBRADOT.PLAGA,PLAGAS.CODIGO_PLAGA,PLAGAS.DESCRIPCION,PLAGAS2.CODIGO_PLAGA as PLAGA2,PLAGAS2.DESCRIPCION AS DESCRIPCION2 FROM PROCESO_CALIBRADOT JOIN PLAGAS ON PLAGAS.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND PLAGAS.CODIGO_PLAGA = PROCESO_CALIBRADOT.PLAGA strings.Left JOIN PLAGAS PLAGAS2 ON PLAGAS2.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND PLAGAS2.CODIGO_PLAGA = PROCESO_CALIBRADOT.PLAGA2  WHERE CLAVE = '" + Trim(Plagas(i)) + "'"
                Rs2.Open(SQL, ObjetoGlobal.cn)
                If Rs2.RecordCount > 0 Then
                    FlagExiste = True
                    If Not IsDBNull(Rs2!codigo_plaga) Then
                        If Rs2!codigo_plaga > 0 Then
                            CodigosPlaga(Orden) = Rs2!codigo_plaga
                            DesPlagas(Orden) = Trim(Rs2!Descripcion)
                            PorcPlagas(Orden) = Porcentajes(i)
                        Else
                            CodigosPlaga(Orden) = 8 'plaga otros...
                            DesPlagas(Orden) = Trim(Rs2!Descripcion)
                            PorcPlagas(Orden) = Porcentajes(i)
                        End If
                    End If
                    If Not IsDBNull(Rs2!plaga2) Then
                        If Rs2!plaga2 > 0 Then
                            CodigosPlaga(Orden + 1) = Rs2!plaga2
                            DesPlagas(Orden + 1) = Trim(Rs2!Descripcion2)
                            PorcPlagas(Orden + 1) = Porcentajes(i)
                            Incrementa = True
                        End If
                    End If
                End If
                Rs2.Close()

                If Not FlagExiste Then
                    SQL = "SELECT PLAGAS.CODIGO_PLAGA,PLAGAS.DESCRIPCION FROM PLAGAS WHERE PLAGAS.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND PLAGAS.DESCRIPCION  = '" + Trim(Cadena) + "'"
                    Rs2.Open(SQL, ObjetoGlobal.cn)
                    If Rs2.RecordCount = 1 Then
                        FlagExiste = True
                        CodigosPlaga(Orden) = Rs2!codigo_plaga
                        DesPlagas(Orden) = Trim(Rs2!Descripcion)
                    Else
                        CodigosPlaga(Orden) = 8 ' otros
                    End If
                    Rs2.Close()
                End If
                If FlagExiste = True Then
                    If CodigosPlaga(i) > 0 Then
                        RsGrabar("plaga" + CStr(Orden)).Value = Trim(DesPlagas(Orden))
                        RsGrabar("porc_plaga" + CStr(Orden)).Value = CLng(PorcPlagas(Orden))
                        RsGrabar("codigo_plaga" + CStr(Orden)).Value = CLng(CodigosPlaga(Orden))
                    End If
                    If Incrementa Then
                        RsGrabar("plaga" + CStr(Orden + 1)).Value = Trim(DesPlagas(Orden + 1))
                        RsGrabar("porc_plaga" + CStr(Orden + 1)).Value = CLng(PorcPlagas(Orden + 1))
                        RsGrabar("codigo_plaga" + CStr(Orden + 1)).Value = CLng(CodigosPlaga(Orden + 1))
                    End If
                Else
                    RsGrabar("plaga" + CStr(Orden)).Value = LCase(Trim(Plagas(i)))
                    If Trim(Plagas(i)) = "" Then
                        RsGrabar("porc_plaga" + CStr(Orden)).Value = 0
                        RsGrabar("codigo_plaga" + CStr(Orden)).Value = 0
                    Else
                        RsGrabar("porc_plaga" + CStr(Orden)).Value = CLng(Porcentajes(i))
                        RsGrabar("codigo_plaga" + CStr(Orden)).Value = 8 ' antes -1
                    End If
                    FlagTodas = False
                End If
            Next
            If RsGrabar!numero_albaran < 99999 Then ' antes era: FlagTodas = True And ...

                TotalPlagas = 0
                AjustaPlagas = True
                For i = 1 To Puntero
                    TotalPlagas = TotalPlagas + PorcPlagas(i)
                    If PorcPlagas(i) = 0 Then
                        If CodigosPlaga(i) = 8 Then
                            AjustaPlagas = False
                            Exit For
                        End If
                    End If
                Next
                If TotalPlagas = 100 Then
                    AjustaPlagas = False
                End If
                If AjustaPlagas And TotalPlagas <> 0 Then
                    difPlagas = 100
                    OrdenMayor = 0
                    MayorPlaga = 0
                    For i = 1 To Puntero
                        If PorcPlagas(i) > 100 Then PorcPlagas(i) = 100
                        PorcPlagas(i) = Math.Round(PorcPlagas(i) * 100 / TotalPlagas, 2)
                        difPlagas = difPlagas - PorcPlagas(i)
                        If PorcPlagas(i) > MayorPlaga Then
                            MayorPlaga = PorcPlagas(i)
                            OrdenMayor = i
                        End If
                    Next
                    If difPlagas <> 0 Then
                        PorcPlagas(OrdenMayor) = PorcPlagas(OrdenMayor) + difPlagas
                    End If
                End If

                For i = 1 To Puntero
                    If CodigosPlaga(i) > 0 And PorcPlagas(i) > 0 And Trim(DesPlagas(i)) > "" Then
                        SQL = "SELECT * FROM entradas_plagas_pro WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO = '" & ObjetoGlobal.EjercicioActual & "' AND SERIE_ALBARAN = '" & Trim(RsGrabar!serie_albaran) & "' AND NUMERO_ALBARAN = " + CStr(RsGrabar!numero_albaran) + " AND CODIGO_PLAGA = " + CStr(CodigosPlaga(i))
                        RsPlagas.Open(SQL, ObjetoGlobal.cn, True)
                        If RsPlagas.RecordCount = 0 Then
                            RsPlagas.AddNew()
                            RsPlagas!empresa = "1"
                            RsPlagas!Ejercicio = ObjetoGlobal.EjercicioActual
                            RsPlagas!serie_albaran = RsGrabar!serie_albaran
                            RsPlagas!numero_albaran = RsGrabar!numero_albaran
                            RsPlagas!tipo_plaga = "D" ' Defectos
                            RsPlagas!codigo_plaga = CodigosPlaga(i)
                            If PorcPlagas(i) > 100 Then PorcPlagas(i) = 100
                            RsPlagas!porcentaje = PorcPlagas(i)
                        Else
                            RsPlagas!porcentaje = RsPlagas!porcentaje + PorcPlagas(i)
                            If RsPlagas!porcentaje > 100 Then RsPlagas!porcentaje = 100
                        End If
                        RsPlagas.Update()
                        RsPlagas.Close()
                    End If
                Next
            End If
        End If

        For i = 1 To 20 : Plagas(i) = "" : DesPlagas(i) = "" : Porcentajes(i) = 0 : CodigosPlaga(i) = 0 : Next

        If Trim(CadenaRecoleccion) > "" Then
            Cadena2 = CadenaRecoleccion
            Cadena = ""
            For i = 1 To Len(Cadena2)
                If Mid(Cadena2, i, 1) = "-" Or Mid(Cadena2, i, 1) = " " Or Mid(Cadena2, i, 1) = "%" Or Mid(Cadena2, i, 1) = "," Or Mid(Cadena2, i, 1) = "/" Then
                    Cadena2 = Cadena2
                Else
                    Cadena = Cadena + Mid(Cadena2, i, 1)
                End If
            Next
            If Strings.Left(Cadena, 1) >= "0" And Strings.Left(Cadena, 1) <= "9" Then
                FlagPrimeroNumeros = True
            Else
                FlagPrimeroNumeros = False
            End If
            Puntero = 1
            CadP = "" : CadN = ""
            If FlagPrimeroNumeros = True Then
                For i = 1 To Len(Cadena)
                    If Mid(Cadena, i, 1) >= "0" And Mid(Cadena, i, 1) <= "9" Then
                        If Trim(CadP) > "" Then
                            lNoSeguir = False
                            For ii = 1 To Puntero - 1
                                If Plagas(ii) = CadP Then
                                    lNoSeguir = True
                                End If
                            Next
                            If Not lNoSeguir Then
                                Plagas(Puntero) = CadP
                                Porcentajes(Puntero) = CadN
                                Puntero = Puntero + 1
                            End If
                            CadP = ""
                            CadN = CStr(Mid(Cadena, i, 1))
                        Else
                            CadN = Trim(CadN) + CStr(Mid(Cadena, i, 1))
                        End If
                    Else
                        CadP = Trim(CadP) + CStr(Mid(Cadena, i, 1))
                    End If
                Next
                If Trim(CadP) > "" Then
                    lNoSeguir = False
                    For ii = 1 To Puntero - 1
                        If Plagas(ii) = CadP Then
                            lNoSeguir = True
                        End If
                    Next
                    If Not lNoSeguir Then
                        Plagas(Puntero) = CadP
                        If Trim(CadN) = "" Then CadN = "0"
                        Porcentajes(Puntero) = CadN
                    End If
                End If
            Else
                For i = 1 To Len(Cadena)
                    If Not (Mid(Cadena, i, 1) >= "0" And Mid(Cadena, i, 1) <= "9") Then
                        If Trim(CadN) > "" Then
                            lNoSeguir = False
                            For ii = 1 To Puntero - 1
                                If Plagas(ii) = CadP Then
                                    lNoSeguir = True
                                End If
                            Next
                            If Not lNoSeguir Then
                                Plagas(Puntero) = CadP
                                Porcentajes(Puntero) = CadN
                            End If
                            Puntero = Puntero + 1
                            CadN = ""
                            CadP = CStr(Mid(Cadena, i, 1))
                        Else
                            CadP = Trim(CadP) + CStr(Mid(Cadena, i, 1))
                        End If
                    Else
                        CadN = Trim(CadN) + CStr(Mid(Cadena, i, 1))
                    End If
                Next
                If Trim(CadP) > "" Then
                    lNoSeguir = False
                    For ii = 1 To Puntero - 1
                        If Plagas(ii) = CadP Then
                            lNoSeguir = True
                        End If
                    Next
                    If Not lNoSeguir Then
                        Plagas(Puntero) = CadP
                        If Trim(CadN) = "" Then CadN = "0"
                        Porcentajes(Puntero) = CadN
                    End If
                End If
            End If

            'TT = Trim(Cadena2)

            For i = 1 To Puntero
                Plagas(i) = LCase(Plagas(i))
            Next
            Orden = 10
            Incrementa = False
            For i = 1 To Puntero
                Orden = Orden + 1
                If Incrementa Then
                    Orden = Orden + 1
                End If
                Incrementa = False

                Cadena = "plaga" + CStr(Orden)
                FlagExiste = False
                SQL = "SELECT PROCESO_CALIBRADOT.PLAGA,PLAGAS.CODIGO_PLAGA,PLAGAS.DESCRIPCION,PLAGAS2.CODIGO_PLAGA as PLAGA2,PLAGAS2.DESCRIPCION AS DESCRIPCION2 FROM PROCESO_CALIBRADOT JOIN PLAGAS ON PLAGAS.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND PLAGAS.CODIGO_PLAGA = PROCESO_CALIBRADOT.PLAGA strings.Left JOIN PLAGAS PLAGAS2 ON PLAGAS2.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND PLAGAS2.CODIGO_PLAGA = PROCESO_CALIBRADOT.PLAGA2  WHERE CLAVE = '" + Trim(Plagas(i)) + "'"
                Rs2.Open(SQL, ObjetoGlobal.cn)
                If Rs2.RecordCount > 0 Then
                    FlagExiste = True
                    If Not IsDBNull(Rs2!codigo_plaga) Then
                        If Rs2!codigo_plaga > 0 Then
                            CodigosPlaga(Orden) = Rs2!codigo_plaga
                            DesPlagas(Orden) = Trim(Rs2!Descripcion)
                            PorcPlagas(Orden) = Porcentajes(i)
                        Else
                            CodigosPlaga(Orden) = 8 'plaga otros...
                            DesPlagas(Orden) = Trim(Rs2!Descripcion)
                            PorcPlagas(Orden) = Porcentajes(i)
                        End If
                    End If
                    If Not IsDBNull(Rs2!plaga2) Then
                        If Rs2!plaga2 > 0 Then
                            CodigosPlaga(Orden + 1) = Rs2!plaga2
                            DesPlagas(Orden + 1) = Trim(Rs2!Descripcion2)
                            PorcPlagas(Orden + 1) = Porcentajes(i)
                            Incrementa = True
                        End If
                    End If
                End If
                Rs2.Close()

                If Not FlagExiste Then
                    SQL = "SELECT PLAGAS.CODIGO_PLAGA,PLAGAS.DESCRIPCION FROM PLAGAS WHERE PLAGAS.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND PLAGAS.DESCRIPCION  = '" + Trim(Cadena) + "'"
                    Rs2.Open(SQL, ObjetoGlobal.cn)
                    If Rs2.RecordCount = 1 Then
                        FlagExiste = True
                        CodigosPlaga(Orden) = Rs2!codigo_plaga
                        DesPlagas(Orden) = Trim(Rs2!Descripcion)
                    Else
                        CodigosPlaga(Orden) = 8 ' otros
                    End If
                    Rs2.Close()
                End If
                If FlagExiste = True Then
                    If CodigosPlaga(Orden) > 0 Then
                        RsGrabar("plaga" + CStr(Orden)).Value = Trim(DesPlagas(Orden))
                        RsGrabar("porc_plaga" + CStr(Orden)).Value = CLng(PorcPlagas(Orden))
                        RsGrabar("codigo_plaga" + CStr(Orden)).Value = CLng(CodigosPlaga(Orden))
                    End If
                    If Incrementa Then
                        RsGrabar("plaga" + CStr(Orden + 1)).Value = Trim(DesPlagas(Orden + 1))
                        RsGrabar("porc_plaga" + CStr(Orden + 1)).Value = CLng(PorcPlagas(Orden + 1))
                        RsGrabar("codigo_plaga" + CStr(Orden + 1)).Value = CLng(CodigosPlaga(Orden + 1))
                    End If
                Else
                    RsGrabar("plaga" + CStr(Orden)).Value = LCase(Trim(Plagas(i)))
                    If Trim(Plagas(i)) = "" Then
                        RsGrabar("porc_plaga" + CStr(Orden)).Value = 0
                        RsGrabar("codigo_plaga" + CStr(Orden)).Value = 0
                    Else
                        RsGrabar("porc_plaga" + CStr(Orden)).Value = CLng(Porcentajes(i))
                        RsGrabar("codigo_plaga" + CStr(Orden)).Value = 8 ' antes -1
                    End If
                    FlagTodas = False
                End If
                'TT = Trim(TT) + vbCrLf + Trim(Plagas(i)) + " / " + CStr(Porcentajes(i))
            Next
            If RsGrabar!numero_albaran < 99999 Then ' antes era: FlagTodas = True And ...

                TotalPlagas = 0
                AjustaPlagas = True
                For i = 1 To Puntero
                    TotalPlagas = TotalPlagas + PorcPlagas(i + 10)
                    If PorcPlagas(i + 10) = 0 Then
                        If CodigosPlaga(i + 10) = 8 Then
                            AjustaPlagas = False
                            Exit For
                        End If
                    End If
                Next
                If TotalPlagas = 100 Then
                    AjustaPlagas = False
                End If
                If AjustaPlagas And TotalPlagas <> 0 Then
                    difPlagas = 100
                    OrdenMayor = 0
                    MayorPlaga = 0
                    For i = 1 To Puntero
                        If PorcPlagas(i + 10) > 100 Then PorcPlagas(i + 10) = 100
                        PorcPlagas(i + 10) = Math.Round(PorcPlagas(i + 10) * 100 / TotalPlagas, 2)
                        difPlagas = difPlagas - PorcPlagas(i + 10)
                        If PorcPlagas(i + 10) > MayorPlaga Then
                            MayorPlaga = PorcPlagas(i + 10)
                            OrdenMayor = i + 10
                        End If
                    Next
                    If difPlagas <> 0 Then
                        PorcPlagas(OrdenMayor) = PorcPlagas(OrdenMayor) + difPlagas
                    End If
                End If

                For i = 1 To Puntero
                    If CodigosPlaga(i + 10) > 0 And PorcPlagas(i + 10) > 0 And Trim(DesPlagas(i + 10)) > "" Then
                        SQL = "SELECT * FROM entradas_plagas_pro WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO = '" & ObjetoGlobal.EjercicioActual & "' AND SERIE_ALBARAN = '" & RsGrabar!serie_albaran & "' AND NUMERO_ALBARAN = " + CStr(RsGrabar!numero_albaran) + " AND CODIGO_PLAGA = " + CStr(CodigosPlaga(i))
                        RsPlagas.Open(SQL, ObjetoGlobal.cn, True)
                        If RsPlagas.RecordCount = 0 Then
                            RsPlagas.AddNew()
                            RsPlagas!empresa = "1"
                            RsPlagas!Ejercicio = ObjetoGlobal.EjercicioActual
                            RsPlagas!serie_albaran = RsGrabar!serie_albaran
                            RsPlagas!numero_albaran = RsGrabar!numero_albaran
                            RsPlagas!tipo_plaga = "R" ' Defectos
                            RsPlagas!codigo_plaga = CodigosPlaga(i + 10)
                            If PorcPlagas(i + 10) > 100 Then PorcPlagas(i + 10) = 100
                            RsPlagas!porcentaje = PorcPlagas(i + 10)
                        Else
                            RsPlagas!porcentaje = RsPlagas!porcentaje + PorcPlagas(i + 10)
                            If RsPlagas!porcentaje > 100 Then RsPlagas!porcentaje = 100
                        End If
                        RsPlagas.Update()
                        RsPlagas.Close()
                    End If
                Next
            End If
        End If
    End Sub


    Private Sub AjustesPredeterminados(PorcentajesPorCalidad, KilosAlbaran)
        Dim RsPc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DifPor As Double
        Dim DifPor1 As Double
        Dim RsTemp As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim kg_muestra As Double
        Dim kg_calidad As Double
        Dim TotalMuestra As Double
        Dim TotalCalidad As Double
        Dim i As Integer


        For i = 1 To 12
            TotalMuestra = TotalMuestra + PorcentajesPorCalidad(i)
            TotalCalidad = TotalCalidad + KilosAlbaran(i)
        Next

        If PorcentajeGrande > 0 Then
            RsPc.Open("SELECT * FROM proceso_calibradov WHERE codigo_variedad='" & RsEntrada!codigo_variedad & "' AND plaga_dto = 100", ObjetoGlobal.cn)
            If Not RsPc.EOF Then
                DifPor = Math.Round(PorcentajeGrande * RsPc!Porcentaje_dto / 100, 2)
                PorcentajeGrande = Math.Round(PorcentajeGrande - DifPor, 2)
                For i = 1 To 12
                    If i = RsPc!calidad_final Then
                        kg_muestra = PorcentajesPorCalidad(i)
                        kg_calidad = KilosAlbaran(i)
                        DifPor1 = Math.Round(kg_muestra * 100 / TotalMuestra, 2) + DifPor
                        kg_muestra = Math.Round(TotalMuestra * DifPor1 / 100, 2)
                        DifPor1 = Math.Round(kg_calidad * 100 / TotalCalidad, 2) + DifPor
                        kg_calidad = Math.Round(TotalCalidad * DifPor1 / 100, 2)
                        PorcentajesPorCalidad(i) = kg_muestra
                        KilosAlbaran(i) = kg_calidad
                    End If
                    If i = 6 Then
                        kg_muestra = PorcentajesPorCalidad(i)
                        kg_calidad = KilosAlbaran(i)
                        DifPor1 = Math.Round(kg_muestra * 100 / TotalMuestra, 2) - DifPor
                        kg_muestra = Math.Round(TotalMuestra * DifPor1 / 100, 2)
                        DifPor1 = Math.Round(kg_calidad * 100 / TotalCalidad, 2) - DifPor
                        kg_calidad = Math.Round(TotalCalidad * DifPor1 / 100, 2)
                        PorcentajesPorCalidad(i) = kg_muestra
                        KilosAlbaran(i) = kg_calidad
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click

    End Sub

    Private Sub FrmNEntradasNuevo20_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim j As Integer
        Dim rsCalVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir()
        End If

        For i = 1 To 12
            LblProvisional(i) = DevuelveControl(Me, "lblProvisional" & Strings.Right("00" & i, 2))
            LblDefinitiva(i) = DevuelveControl(Me, "lblDefinitiva" & Strings.Right("00" & i, 2))
            LblEnvase(i) = DevuelveControl(Me, "lblEnvase" & Strings.Right("00" & i, 2))
            LblNueva(i) = DevuelveControl(Me, "lblNueva" & Strings.Right("00" & i, 2))
            lblMuestra(i) = DevuelveControl(Me, "lblMuestra" & Strings.Right("00" & i, 2))
        Next

        OptAlbaran(0) = OptAlbaran0
        OptAlbaran(1) = OptAlbaran1
        OptAlbaran(2) = OptAlbaran2
        OptAlbaran(3) = OptAlbaran3
        OptAlbaran(4) = OptAlbaran4

        SQL = "SELECT e.*, pr.razon_social, oc.nombre AS NOMBRE_CAPATAZ,ot.nombre AS NOMBRE_TRANSPORTISTA,c.situacion_campo AS DESCRIPCION_SITUACION,v.descripcion AS DESCRIPCION_VARIEDAD,p.descripcion_per AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM entradas_albaranes e JOIN proveedores_coop pr ON pr.codigo_proveedor = e.codigo_proveedor "
        SQL = Trim(SQL) + " JOIN campos_terceros c ON c.empresa = e.empresa AND c.ejercicio = e.ejercicio AND c.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
        SQL = Trim(SQL) + " JOIN personal_terceros oc ON oc.codigo = e.capataz_terc "
        SQL = Trim(SQL) + " JOIN personal_terceros ot ON ot.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " JOIN periodos_coop p ON p.empresa = e.empresa AND p.ejercicio = e.ejercicio AND p.codigo_variedad = e.codigo_variedad AND p.codigo_periodo = e.codigo_periodo "
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.serie_albaran = '" + Trim(SerieSeleccionada) + "' and numero_albaran = " + CStr(AlbaranSeleccionado)

        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado es inexistente", MsgBoxStyle.Critical)
            CmdSalir()
            Return
        End If

        rsCalVariedad.Open("SELECT MAX(codigo_calidad) AS CuantasCalidades FROM calidades_var_ej WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and EJERCICIO = '" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_VARIEDAD = '" & RsEntrada!codigo_variedad & "'", ObjetoGlobal.cn)
        cuantascalidades = rsCalVariedad!cuantascalidades
        rsCalVariedad.Close()

        SQL = "SELECT * FROM Calidades_Var_ej WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad = '" & RsEntrada!codigo_variedad & "' order by 1,2,3,4"
        rsCalVariedad.Open(SQL, ObjetoGlobal.cn)
        If rsCalVariedad.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("No existen calidades para esta variedad", MsgBoxStyle.Critical)
            CmdSalir()
            Return
        End If

        nClasificaciones = rsCalVariedad.RecordCount
        ReDim aCalidades(nClasificaciones)
        ReDim aCalidadesDes(nClasificaciones)

        For i = 1 To 12
            LblProvisional(i).Visible = False
            LblDefinitiva(i).Visible = False
            LblEnvase(i).Visible = False
            lblMuestra(i).Visible = False
            LblNueva(i).Visible = False
        Next

        i = 1
        While Not rsCalVariedad.EOF
            If i <= 12 Then
                aCalidades(i) = rsCalVariedad!codigo_calidad
                aCalidadesDes(i) = rsCalVariedad!Descripcion
                LblProvisional(i).Visible = True
                LblDefinitiva(i).Visible = True
                LblEnvase(i).Visible = True
                LblEnvase(i).Text = rsCalVariedad!Descripcion
                lblMuestra(i).Visible = True
                LblNueva(i).Visible = True
                i = i + 1
            End If
            rsCalVariedad.MoveNext()
        End While

        rsCalVariedad.Close()

        If RsEntrada!tarada_sn <> "S" Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado no ha sido tarado todavía.", MsgBoxStyle.Exclamation)
            CmdSalir()
            Return
        End If
        Me.KeyPreview = True
        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_proveedor) + " " + Trim("" & RsEntrada!razon_social)
        lblCampo.Text = CStr(RsEntrada!campo_terceros) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!capataz_terc) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista_terc) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)
        For i = 0 To 1 : For j = 0 To 12 : Clasificacion(i, j) = 0 : Next : Next
        RsClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " order by 1,2,3,4,5,6", ObjetoGlobal.cn)
        For i = 1 To RsClasificacion.RecordCount
            RsClasificacion.AbsolutePosition = i
            If Trim(RsClasificacion!Tipo_clasificacion) = "PRO" Then
                j = 0
            Else
                j = 1
            End If
            If Trim(RsClasificacion!codigo_calidad) > 0 And Trim(RsClasificacion!codigo_calidad) <= 12 Then
                Clasificacion(j, RsClasificacion!codigo_calidad) = RsClasificacion!kg_calidad
            End If
        Next
        RsClasificacion.Close()

        For i = 0 To 1
            For j = 1 To 12
                If i = 0 Then
                    LblProvisional(j).Text = Format(Clasificacion(i, j), "###,##0")
                Else
                    LblDefinitiva(j).Text = Format(Clasificacion(i, j), "###,##0")
                End If
            Next
        Next
        lblKilosfruta.Text = Format(RsEntrada!kg_entrada, "###,##0")
        OptAlbaran(0).Text = CStr(AlbaranSeleccionado)
        OptAlbaran(1).Text = CStr(60000 + AlbaranSeleccionado)
        OptAlbaran(2).Text = CStr(600000 + AlbaranSeleccionado)
        OptAlbaran(3).Text = CStr(6000000 + AlbaranSeleccionado)
        OptAlbaran(4).Text = CStr(60600000 + AlbaranSeleccionado)
        OptAlbaran(2).Checked = True
        FlagMensaje = False
        FlagGrabarPlagas = False

        PorcentajeGrande = 0
        PorcentajePequena = 0
        PorcentajeRecoleccion = 0

        If IsDBNull(RsEntrada!porcentaje_plaga) = True Then
            PorcentajeFinal = 0
        Else
            PorcentajeFinal = RsEntrada!porcentaje_plaga
        End If

    End Sub

    Private Sub CmdImportarClasificacion_Click(sender As Object, e As EventArgs) Handles CmdImportarClasificacion.Click
        ImportaClasificacion()
    End Sub
End Class