Public Class FrmNEntradasNuevo08
    Public ObjetoGlobal As Object
    Private aCalidades() As String
    Private aCalidadesDes() As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim Clasificacion(1, 12) As Long
    Dim MuestraNueva(12) As Single
    Dim PesoNuevo(12) As Long
    Dim PesosMuestras(1, 12) As Double
    Dim PorcentajesClasif(12) As Double
    Dim FlagMensaje As Boolean
    Dim FlagGrabarPlagas As Boolean
    Dim QuePlagas(99) As Integer
    Dim PorcentajePlaga(99) As Single
    Dim CuantasPlagas As Integer
    Dim KilosN(10) As Double
    Dim nClasificaciones As Integer
    Public oform As Form
    Dim inicial_recol_p As Double
    Dim inicial_recol_i As Double
    Dim SumaNC As Double
    Dim ValorNC As Double
    Dim ValorCom As Double
    Dim PorcentajeNC As Double
    Dim DiferenciaPorcentajes As Double
    Dim lblProvisional(12) As Label
    Dim lblDefinitiva(12) As Label
    Dim lblEnvase(12) As Label
    Dim txtMuestra(12) As TextBox
    Dim lblNueva(12) As Label
    Dim lblPorcentaje(12) As Label
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        FlagMensaje = True
        If CalcularPesos() Then
            If Math.Abs(DiferenciaPorcentajes) < 0.01 Or Strings.Left(RsEntrada!codigo_variedad, 2) > "02" Then
                GrabarEntrada()
                FlagPreguntar = False
                CmdSalir_Click()
            Else
                MsgBox("El porcentaje de No Comercial y la suma de plagas debe de ser iguales")
            End If
        End If
        FlagMensaje = False
    End Sub

    Private Sub CmdProv_def_Click()
        Dim i As Integer
        Dim TotalKilos As Double
        Dim Ord As Integer

        For i = 1 To 12
            If lblNueva(i).Visible Then
                lblNueva(i).Text = lblProvisional(i).Text
                TotalKilos = TotalKilos + CDbl(lblProvisional(i).Text)
                Ord = i
            End If
        Next
        For i = 1 To 12
            If lblNueva(i).Visible Then
                If TotalKilos > 0 Then
                    txtMuestra(i).Text = Math.Round((100 / TotalKilos) * CDbl(lblNueva(i).Text), 2)
                    lblPorcentaje(i).Text = txtMuestra(i).Text
                End If
            End If
        Next

        lblNC.Text = lblPorcentaje(Ord).Text
        Totalizar()

    End Sub

    Private Sub CmdSalir_Click()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN grabar la clasificación?", vbYesNo) = vbYes Then
                Me.Close()
            End If
        Else
            Me.Close()
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
            If FlagMensaje Then
                MsgBox("No existe peso en esta entrada.")
            End If
            Return False
        End If

        Peso2 = 0
        For i = 1 To 12
            Peso3 = 0
            If IsNumeric(txtMuestra(i).Text) Then Peso3 = Math.Round(CDbl(txtMuestra(i).Text), 2)
            MuestraNueva(i) = Peso3
            Peso2 = Peso2 + Peso3
        Next
        If Peso2 = 0 Then
            If FlagMensaje Then
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
        While PesoNuevo(0) <> Peso1
            Quien = -1
            If Peso1 > PesoNuevo(0) Then
                Incremento = 1
            Else
                Incremento = -1
            End If
            For i = 1 To nClasificaciones '7
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
        End While
        For i = 1 To 12 ' nClasificaciones (solo naranjas)
            lblNueva(i).Text = Format(PesoNuevo(i), "###,##0")
            lblPorcentaje(i).Text = Format(Math.Round(PesoNuevo(i) * 100 / Peso1, 2), "##0.00")
            'If i = 6 Then ' Ver solo cítricos aaa
            If i = nClasificaciones Then
                lblNC.Text = lblPorcentaje(i).Text
                lblDif.Text = Format((CDbl("0" & lblNC.Text) - CDbl("0" & LblSuma.Text)), "##0.00")
                DiferenciaPorcentajes = (CDbl("0" & lblNC.Text) - CDbl("0" & LblSuma.Text))
            End If
        Next
        LblKilosCalculados.Text = Format(PesoNuevo(0), "###,##0")
        Return True

    End Function

    Private Function GrabarEntrada() As Boolean
        Dim Mensaje As String
        Dim i As Integer
        Dim RsAlbaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaranClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPendiente As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CuantosBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try
            'entradas_albaranes
            RsAlbaran.Open("SELECT * FROM ENTRADAS_ALBARANES E WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran), ObjetoGlobal.cn, True,,,,,, trans)
            RsAlbaran!kg_sin_clasif = 0
            RsAlbaran!porcentaje_plaga = CDbl("0" & TxtPorcentaje_Plaga.Text)
            RsAlbaran!porcentaje_recol = CDbl("0" & TxtPorcentaje_recol.Text)
            RsAlbaran!porcentaje_recol_i = CDbl("0" & TxtPorcentaje_recol_i.Text)
            RsAlbaran!porcentaje_grand = CDbl("0" & txtPorcentaje_grand.Text)
            RsAlbaran!porcentaje_peq = CDbl("0" & txtPorcentaje_peq.Text)
            RsAlbaran.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsAlbaran, "U"
            RsAlbaran.Close()
            'entradas_clasificacion
            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn, True,,,,,, trans)
            Do While RsAlbaranClasificacion.RecordCount > 0
                RsAlbaranClasificacion.MoveFirst()
                'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "D"
                RsAlbaranClasificacion.Delete()
            Loop
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

            trans.Commit()
            ObjetoGlobal.cn.Close()

            GrabarEntrada = True
            AlbaranSeleccionado = -1
            SerieSeleccionada = ""
            MsgBox("Anotada la clasificacion del albarán :" + CStr(RsEntrada!numero_albaran))

            ImprimeAlbaran3(RsEntrada!serie_albaran, RsEntrada!numero_albaran, RsEntrada!codigo_variedad, RsEntrada!Transportista, CDbl("0" & RsEntrada!peso_campo))

            Return True
        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Mensaje = Trim(Err.Description)
            MsgBox("No se puede grabar la clasificación del albarán de entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(Mensaje))
            Return False
        End Try

    End Function
    'Private Sub PicF_Click(Index As Integer)
    '    Dim Cadena As String

    '    Cadena = "{F" + CStr(Index) + "}"
    '    SendKeys(Cadena)
    'End Sub
    Private Sub PresentarNuevaClasificacion(Rs As cnRecordset.CnRecordset)
        Dim i As Long
        Dim j As Long

        txtMuestra(1).Text = KilosN(1)
        txtMuestra(2).Text = KilosN(2)
        txtMuestra(3).Text = KilosN(3)
        txtMuestra(4).Text = KilosN(4)
        txtMuestra(5).Text = KilosN(5)
        txtMuestra(6).Text = KilosN(6)
        txtMuestra(7).Text = 0
        txtMuestra(8).Text = 0
        txtMuestra(9).Text = 0
        txtMuestra(10).Text = 0
        txtMuestra(11).Text = 0
        txtMuestra(12).Text = 0
        CalcularPesos()
    End Sub
    Private Sub GrabarValoresBlob(Cadena As String, Rs As cnRecordset.CnRecordset)
        Dim longitud As Long
        Dim CuantosDatos As Long
        Dim i As Long
        Dim j As Long
        Dim Cad1 As String
        Dim Cad2 As String
        Dim Cuantos As Long
        Dim Valor As Long
        Dim kk As Integer
        Dim TotalDatos As Integer
        Dim Puntero As Long
        Dim DatoS(30000) As Long
        Dim TotalKilos As Long
        Dim KilosPorCalidad(10) As Long
        Dim PorcentajesPorCalidad(10) As Single
        Dim KilosAlbaran(10) As Long
        Dim Cadena2 As String
        Dim Cadena3 As String
        Dim Calidad As Integer
        Dim TotalClasificacionAlbaran As Integer
        Dim Diferencia As Long
        Dim CalidadRedondeo As Integer
        Dim DiferenciaMaxima As Single
        Dim FlagMaximo As Boolean
        Dim IndiceClasif As Integer
        Dim KilosPorCalibre(20) As Long
        Dim PorcentajesPorCalibre(20) As Single
        Dim KilosAlbaranCalibre(20) As Long

        longitud = Len(Cadena)
        CuantosDatos = longitud / 16
        Puntero = 0
        TotalDatos = 0
        For i = 1 To CuantosDatos
            Cad1 = Mid(Cadena, 16 * i - 15, 8)
            Cad2 = Mid(Cadena, 16 * i - 7, 8)
            Cuantos = TransformarDesdeHexa(Cad1)
            Valor = TransformarDesdeHexa(Cad2)
            kk = kk + 1
            '    Rs.fields("Cuantos" + Format(kk, "00")).Value = Cuantos
            TotalDatos = TotalDatos + Cuantos
            '    Rs.fields("Valor" + Format(kk, "00")).Value = Valor
            For j = 1 To Cuantos
                Puntero = Puntero + 1
                DatoS(Puntero) = Valor
            Next
        Next
        Rs!total_datos = TotalDatos
        If Trim(Rs!VariedadID) = 75 Then
            For j = 19584 To 1153 Step -1
                DatoS(j) = DatoS(j - 1152)
            Next
            For j = 1 To 1152
                DatoS(j) = 0
            Next
        End If
        For j = 1 To 17
            Rs("peso" + Format(2 * j - 1, "00")) = DatoS(1152 * j - 1151)
            Rs("unidades" + Format(2 * j - 1, "00")) = DatoS(1152 * j - 1150)
            Rs("peso" + Format(2 * j, "00")) = DatoS(1152 * j - 1149)
            Rs("unidades" + Format(2 * j, "00")) = DatoS(1152 * j - 1148)
        Next
        'CALIDADES ALBARAN
        TotalKilos = 0
        For j = 0 To 10
            KilosPorCalidad(j) = 0
            PorcentajesPorCalidad(j) = 0
            KilosAlbaran(j) = 0
        Next
        For j = 1 To 34
            TotalKilos = TotalKilos + Math.Round(Rs("peso" + Format(j, "00")), 0)
        Next
        For j = 1 To 34
            If Math.Round(Rs("peso" + Format(j, "00")), 0) > 0 Then
                Calidad = 0
                Cadena2 = Trim(Rs("categoria" + Format(j, "00")))
                If Strings.Left(Cadena2, 4) = "DEST" Then
                    Calidad = 6
                Else
                    kk = InStr(1, Cadena2, ":")
                    If kk > 2 Then
                        If Mid(Cadena2, kk - 2, 2) = "ES" Then
                            Calidad = 1
                        ElseIf Mid(Cadena2, kk - 2, 2) = "PR" Then
                            Calidad = 2
                        ElseIf Mid(Cadena2, kk - 2, 2) = "SE" Then
                            Calidad = 3
                        ElseIf Mid(Cadena2, kk - 2, 2) = "TE" Then
                            Calidad = 4
                        ElseIf Mid(Cadena2, kk - 2, 2) = "CU" Then
                            Calidad = 5
                        ElseIf Mid(Cadena2, kk - 2, 2) = "NC" Then
                            Calidad = 6
                        End If
                    End If
                End If
                If Calidad = 0 Then
                    ' MsgBox "Albaran sin clasificar: " + Trim(Rs!numero_albaran)
                Else
                    KilosPorCalidad(Calidad) = KilosPorCalidad(Calidad) + Math.Round(Rs("peso" + Format(j, "00")), 0)
                End If
            End If
        Next
        If TotalKilos > 0 Then
            For j = 1 To 6
                PorcentajesPorCalidad(j) = KilosPorCalidad(j) * 100.0# / TotalKilos
            Next
        End If
        Rs!peso_especial = KilosPorCalidad(1)
        Rs!peso_primera = KilosPorCalidad(2)
        Rs!peso_segunda = KilosPorCalidad(3)
        Rs!peso_tercera = KilosPorCalidad(4)
        Rs!peso_cuarta = KilosPorCalidad(5)
        Rs!peso_destrio = KilosPorCalidad(6)
        Rs!porc_especial = Math.Round(PorcentajesPorCalidad(1), 2)
        Rs!porc_primera = Math.Round(PorcentajesPorCalidad(2), 2)
        Rs!porc_segunda = Math.Round(PorcentajesPorCalidad(3), 2)
        Rs!porc_tercera = Math.Round(PorcentajesPorCalidad(4), 2)
        Rs!porc_cuarta = Math.Round(PorcentajesPorCalidad(5), 2)
        Rs!porc_destrio = Math.Round(PorcentajesPorCalidad(6), 2)
        If Rs!kilos_albaran > 0 Then
            TotalClasificacionAlbaran = 0
            For j = 1 To 6
                KilosAlbaran(j) = Math.Round(PorcentajesPorCalidad(j) * Rs!kilos_albaran / 100.0#, 0)
                TotalClasificacionAlbaran = TotalClasificacionAlbaran + KilosAlbaran(j)
            Next
            Diferencia = Rs!kilos_albaran - TotalClasificacionAlbaran
            Do While Diferencia <> 0
                CalidadRedondeo = -1
                DiferenciaMaxima = 0
                For j = 1 To 6
                    FlagMaximo = False
                    If CalidadRedondeo = -1 Then
                        FlagMaximo = True
                    Else
                        If Diferencia > 0 And KilosAlbaran(j) - (PorcentajesPorCalidad(j) * Rs!kilos_albaran / 100.0#) < DiferenciaMaxima Then
                            FlagMaximo = True
                        ElseIf Diferencia < 0 And KilosAlbaran(j) - (PorcentajesPorCalidad(j) * Rs!kilos_albaran / 100.0#) > DiferenciaMaxima Then
                            FlagMaximo = True
                        End If
                    End If
                    If FlagMaximo = True Then
                        CalidadRedondeo = j
                        DiferenciaMaxima = KilosAlbaran(j) - (PorcentajesPorCalidad(j) * Rs!kilos_albaran / 100.0#)
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
            Rs!kilos_especial = KilosAlbaran(1)
            Rs!kilos_primera = KilosAlbaran(2)
            Rs!kilos_segunda = KilosAlbaran(3)
            Rs!kilos_tercera = KilosAlbaran(4)
            Rs!kilos_cuarta = KilosAlbaran(5)
            Rs!kilos_destrio = KilosAlbaran(6)
        Else
            Rs!kilos_especial = 0
            Rs!kilos_primera = 0
            Rs!kilos_segunda = 0
            Rs!kilos_tercera = 0
            Rs!kilos_cuarta = 0
            Rs!kilos_destrio = 0
        End If
        IndiceClasif = 0
        For j = 1 To 6
            IndiceClasif = IndiceClasif + (j - 1) * 1.0# * PorcentajesPorCalidad(j)
        Next
        IndiceClasif = IndiceClasif / 5
        Rs!indice_clasif = IndiceClasif

        KilosN(1) = Rs!kilos_especial
        KilosN(2) = Rs!kilos_primera
        KilosN(3) = Rs!kilos_segunda
        KilosN(4) = Rs!kilos_tercera
        KilosN(5) = Rs!kilos_cuarta
        KilosN(6) = Rs!kilos_destrio

        'CALIBRES
        For j = 0 To 20 : KilosPorCalibre(j) = 0 : PorcentajesPorCalibre(j) = 0 : KilosAlbaranCalibre(j) = 0 : Next

        For j = 1 To 34
            If Math.Round(Rs("peso" + Format(j, "00")), 0) > 0 Then
                Calidad = 0
                Cadena2 = Trim(Rs("categoria" + Format(j, "00")))
                If InStr(1, Cadena2, "(r)") > 0 Then
                    Calidad = 18
                ElseIf InStr(1, Cadena2, "DEST") > 0 Then
                    If j < 5 Then Calidad = 19 Else Calidad = 20
                ElseIf Strings.Left(Cadena2, 2) = "NC" Then
                    If j < 5 Then Calidad = 19 Else Calidad = 20
                Else
                    If Strings.Left(Cadena2, 3) <> "CAL" Then
                        MsgBox("error")
                    Else
                        Cadena3 = Mid(Cadena2, 4)
                        kk = InStr(1, Cadena3, " ")
                        If kk <= 0 Then
                            MsgBox("error 2")
                        Else
                            Cadena3 = Mid(Cadena3, 1, kk - 1)
                            If Len(Cadena3) = 1 And Cadena3 >= "1" And Cadena3 <= "9" Then
                                Calidad = CInt(Cadena3)
                            ElseIf Cadena3 = "1X" Then
                                Calidad = 11
                            ElseIf Cadena3 = "1XX" Then
                                Calidad = 12
                            ElseIf Cadena3 = "1XXX" Then
                                Calidad = 13
                            ElseIf Cadena3 = "11" Then
                                Calidad = 20
                            Else
                                MsgBox("error 3")
                            End If
                        End If
                    End If
                End If
                '       MsgBox Cadena2 + vbCrLf + CStr(Calidad)
                If Calidad = 0 Then
                    '            MsgBox "Albaran sin clasificar: " + Trim(Rs!numero_albaran)
                Else
                    KilosPorCalibre(Calidad) = KilosPorCalibre(Calidad) + Math.Round(Rs("peso" + Format(j, "00")), 0)
                End If
            End If
        Next
        If TotalKilos > 0 Then
            For j = 1 To 20
                PorcentajesPorCalibre(j) = KilosPorCalibre(j) * 100.0# / TotalKilos
            Next
        End If

        If Rs!kilos_albaran > 0 Then
            TotalClasificacionAlbaran = 0
            For j = 1 To 20
                KilosAlbaranCalibre(j) = Math.Round(PorcentajesPorCalibre(j) * Rs!kilos_albaran / 100.0#, 0)
                TotalClasificacionAlbaran = TotalClasificacionAlbaran + KilosAlbaranCalibre(j)
            Next
            Diferencia = Rs!kilos_albaran - TotalClasificacionAlbaran
            While Diferencia <> 0
                CalidadRedondeo = -1
                DiferenciaMaxima = 0
                For j = 1 To 20
                    FlagMaximo = False
                    If CalidadRedondeo = -1 Then
                        FlagMaximo = True
                    Else
                        If Diferencia > 0 And KilosAlbaranCalibre(j) - (PorcentajesPorCalibre(j) * Rs!kilos_albaran / 100.0#) < DiferenciaMaxima Then
                            FlagMaximo = True
                        ElseIf Diferencia < 0 And KilosAlbaranCalibre(j) - (PorcentajesPorCalibre(j) * Rs!kilos_albaran / 100.0#) > DiferenciaMaxima Then
                            FlagMaximo = True
                        End If
                    End If
                    If FlagMaximo = True Then
                        CalidadRedondeo = j
                        DiferenciaMaxima = KilosAlbaranCalibre(j) - (PorcentajesPorCalibre(j) * Rs!kilos_albaran / 100.0#)
                    End If
                Next
                If Diferencia > 0 Then
                    KilosAlbaranCalibre(CalidadRedondeo) = KilosAlbaranCalibre(CalidadRedondeo) + 1
                    Diferencia = Diferencia - 1
                Else
                    KilosAlbaranCalibre(CalidadRedondeo) = KilosAlbaranCalibre(CalidadRedondeo) - 1
                    Diferencia = Diferencia + 1
                End If
            End While
        End If

        For j = 1 To 9
            Rs("peso_calibre" + CStr(j)) = KilosPorCalibre(j)
            Rs("porc_calibre" + CStr(j)) = Math.Round(PorcentajesPorCalibre(j), 2)
            Rs("kilos_calibre" + CStr(j)) = KilosAlbaranCalibre(j)
        Next
        Rs!peso_calibre1x = KilosPorCalibre(11)
        Rs!porc_calibre1x = Math.Round(PorcentajesPorCalibre(11), 2)
        Rs!kilos_calibre1x = KilosAlbaranCalibre(11)
        Rs!peso_calibre1xx = KilosPorCalibre(12)
        Rs!porc_calibre1xx = Math.Round(PorcentajesPorCalibre(12), 2)
        Rs!kilos_calibre1xx = KilosAlbaranCalibre(12)
        Rs!peso_calibre1xxx = KilosPorCalibre(13)
        Rs!porc_calibre1xxx = Math.Round(PorcentajesPorCalibre(13), 2)
        Rs!kilos_calibre1xxx = KilosAlbaranCalibre(13)
        Rs!peso_nc_defecto = KilosPorCalibre(18)
        Rs!porc_nc_defecto = Math.Round(PorcentajesPorCalibre(18), 2)
        Rs!kilos_nc_defecto = KilosAlbaranCalibre(18)
        Rs!peso_nc_peq = KilosPorCalibre(19)
        Rs!porc_nc_peq = Math.Round(PorcentajesPorCalibre(19), 2)
        Rs!kilos_nc_peq = KilosAlbaranCalibre(19)
        Rs!peso_nc_grande = KilosPorCalibre(20)
        Rs!porc_nc_grande = Math.Round(PorcentajesPorCalibre(20), 2)
        Rs!kilos_nc_grande = KilosAlbaranCalibre(20)

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


    Private Sub txtPorcentaje_grand_Validate(Cancel As Boolean)
        Totalizar()
    End Sub


    Private Sub txtPorcentaje_peq_Validate(Cancel As Boolean)
        Totalizar()
    End Sub


    Private Sub TxtPorcentaje_Plaga_Validate(Cancel As Boolean)
        Totalizar()
    End Sub


    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir_Click()
    End Sub

    Private Sub TxtPorcentaje_recol_Validate(Cancel As Boolean)
        If inicial_recol_p = 0 And inicial_recol_i = 0 Then
            TxtPorcentaje_recol_i.Text = TxtPorcentaje_recol.Text
        ElseIf inicial_recol_p = 0 And inicial_recol_i > 0 Then
            If MsgBox("¿Debe actualizar el dato de recolección inicial?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo And MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes Then
                TxtPorcentaje_recol_i.Text = TxtPorcentaje_recol.Text
            End If
        End If
        Totalizar()
    End Sub

    Private Function Totalizar() As Boolean
        SumaNC = CDbl("0" & TxtPorcentaje_Plaga.Text) + CDbl("0" & TxtPorcentaje_recol.Text) + CDbl("0" & txtPorcentaje_grand.Text) + CDbl("0" & txtPorcentaje_peq.Text)
        Totalizar = True
        lblDif.Text = Format(CDbl("0" & lblNC.Text) - SumaNC, "###.##")
        DiferenciaPorcentajes = CDbl("0" & lblNC.Text) - SumaNC
        LblSuma.Text = Format(SumaNC, "###.##")
        If ValorNC - SumaNC <> 0 Then
            Totalizar = False
        End If
    End Function

    Private Sub FrmNEntradasNuevo08_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim j As Integer
        Dim rsCalVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim TotalPorc As Double
        Dim Max As Double
        Dim OrdMax As Integer
        Dim Porc As Double

        DiferenciaPorcentajes = 0

        For i = 1 To 12
            lblProvisional(i) = DevuelveControl(Me, "lblProvisional" & Strings.Right("00" & i, 2))
            lblDefinitiva(i) = DevuelveControl(Me, "lblDefinitiva" & Strings.Right("00" & i, 2))
            lblEnvase(i) = DevuelveControl(Me, "lblEnvase" & Strings.Right("00" & i, 2))
            lblNueva(i) = DevuelveControl(Me, "lblNueva" & Strings.Right("00" & i, 2))
            lblPorcentaje(i) = DevuelveControl(Me, "lblPorcentaje" & Strings.Right("00" & i, 2))
            txtMuestra(i) = DevuelveControl(Me, "txtMuestra" & Strings.Right("00" & i, 2))
        Next


        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir_Click()
            Exit Sub
        End If

        SQL = "SELECT E.*,S.*,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD,P.DESCRIPCION_PER AS DESCRIPCION_PERIODO,C.ACTIVO_SERVICIOS "
        SQL = Trim(SQL) + " FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado es inexistente")
            CmdSalir_Click()
            Exit Sub
        End If

        TxtPorcentaje_Plaga.Text = "" & RsEntrada!porcentaje_plaga
        TxtPorcentaje_recol.Text = "" & RsEntrada!porcentaje_recol
        TxtPorcentaje_recol_i.Text = "" & RsEntrada!porcentaje_recol_i
        txtPorcentaje_grand.Text = "" & RsEntrada!porcentaje_grand
        txtPorcentaje_peq.Text = "" & RsEntrada!porcentaje_peq
        inicial_recol_p = RsEntrada!porcentaje_recol
        inicial_recol_i = RsEntrada!porcentaje_recol_i

        SumaNC = RsEntrada!porcentaje_plaga + RsEntrada!porcentaje_recol + RsEntrada!porcentaje_grand + RsEntrada!porcentaje_peq
        LblSuma.Text = Format(SumaNC, "###.##")

        SQL = "SELECT * FROM Calidades_Var_ej  WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad = '" & RsEntrada!codigo_variedad & "' order by 1,2,3,4"
        rsCalVariedad.Open(SQL, ObjetoGlobal.cn)
        If rsCalVariedad.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("No existen calidades para esta variedad")
            CmdSalir_Click()
            Exit Sub
        End If

        nClasificaciones = rsCalVariedad.RecordCount
        ReDim aCalidades(nClasificaciones)
        ReDim aCalidadesDes(nClasificaciones)

        For i = 1 To 12
            lblProvisional(i).Visible = False
            lblDefinitiva(i).Visible = False
            LblEnvase(i).Visible = False
            txtMuestra(i).Visible = False
            lblNueva(i).Visible = False
            lblPorcentaje(i).Visible = False
        Next

        i = 1
        While Not rsCalVariedad.EOF
            If i <= 12 Then
                aCalidades(i) = rsCalVariedad!codigo_calidad
                aCalidadesDes(i) = rsCalVariedad!Descripcion
                lblProvisional(i).Visible = True
                lblDefinitiva(i).Visible = True
                LblEnvase(i).Visible = True
                LblEnvase(i).text = rsCalVariedad!Descripcion
                txtMuestra(i).Visible = True
                lblNueva(i).Visible = True
                lblPorcentaje(i).Visible = True
                i = i + 1
            End If
            rsCalVariedad.MoveNext
        End While

        rsCalVariedad.Close
        If RsEntrada!tarada_sn <> "S" Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado no ha sido tarado todavía.")
            CmdSalir_Click()
            Exit Sub
        End If
        Me.KeyPreview = True
        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/MM/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_socio) + " " + Trim("" & RsEntrada!apellidos_socio) + ", " + Trim("" & RsEntrada!nombre_socio)
        lblCampo.Text = CStr(RsEntrada!codigo_campo) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!Capataz) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)
        For i = 0 To 1
            For j = 0 To 12
                Clasificacion(i, j) = 0
            Next
        Next
        For i = 0 To 1
            For j = 0 To 12
                PesosMuestras(i, j) = 0#
            Next
        Next
        For j = 0 To 12
            PorcentajesClasif(j) = 0#
        Next

        RsClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " order by 1,2,3,4,5,6", ObjetoGlobal.cn)
        For i = 1 To RsClasificacion.RecordCount
            RsClasificacion.AbsolutePosition = i
            If Trim(RsClasificacion!Tipo_clasificacion) = "PRO" Then j = 0 Else j = 1
            If Trim(RsClasificacion!codigo_calidad) > 0 And Trim(RsClasificacion!codigo_calidad) <= 12 Then ' aaa
                Clasificacion(j, RsClasificacion!codigo_calidad) = RsClasificacion!kg_calidad
                PesosMuestras(j, RsClasificacion!codigo_calidad) = RsClasificacion!kg_muestra
            End If
        Next
        RsClasificacion.Close
        TotalPorc = 100
        For i = 0 To 1
            For j = 1 To 12
                If i = 0 Then
                    lblProvisional(j).Text = Format(Clasificacion(i, j), "###,##0")
                Else
                    lblDefinitiva(j).Text = Format(Clasificacion(i, j), "###,##0")
                    lblNueva(j).Text = Format(Clasificacion(i, j), "###,##0")
                    txtMuestra(j).Text = PesosMuestras(i, j)
                    Porc = Math.Round(Clasificacion(i, j) * 100 / RsEntrada!kg_entrada, 2)
                    TotalPorc = Math.Round(TotalPorc - Porc, 2)
                    If Porc > Max Then
                        Max = Porc
                        OrdMax = j
                    End If
                    PorcentajesClasif(j) = Porc
                End If
            Next
        Next
        If TotalPorc <> 0 Then
            PorcentajesClasif(OrdMax) = PorcentajesClasif(OrdMax) + TotalPorc
        End If
        For j = 1 To 12
            lblPorcentaje(j).Text = Format(PorcentajesClasif(j), "##0.00")
            If i = nClasificaciones Then
                PorcentajeNC = PorcentajesClasif(j)
                lblNC.Text = Format(PorcentajeNC, "###.##")
                lblDif.Text = Format((SumaNC - PorcentajeNC), "###.##")
                DiferenciaPorcentajes = Math.Round((SumaNC - PorcentajeNC), 2)
            End If
        Next

        lblKilosfruta.Text = Format(RsEntrada!kg_entrada, "###,##0")
        FlagMensaje = False
        FlagGrabarPlagas = False

    End Sub

    Private Sub TxtMuestra_Validated(sender As Object, e As EventArgs) Handles TxtMuestra01.Validated, TxtMuestra02.Validated, TxtMuestra03.Validated, TxtMuestra04.Validated, TxtMuestra05.Validated, TxtMuestra06.Validated, TxtMuestra07.Validated, TxtMuestra08.Validated, TxtMuestra09.Validated, TxtMuestra10.Validated, TxtMuestra11.Validated, TxtMuestra12.Validated
        CalcularPesos()
    End Sub


    Private Sub TxtPorcentaje_Plaga_Validated(sender As Object, e As EventArgs) Handles TxtPorcentaje_Plaga.Validated, txtPorcentaje_grand.Validated, txtPorcentaje_peq.Validated
        Totalizar()
    End Sub

    Private Sub TxtPorcentaje_recol_i_Validated(sender As Object, e As EventArgs) Handles TxtPorcentaje_recol_i.Validated
        If inicial_recol_p = 0 And inicial_recol_i = 0 Then
            TxtPorcentaje_recol_i.Text = TxtPorcentaje_recol.Text
        ElseIf inicial_recol_p = 0 And inicial_recol_i > 0 Then
            If MsgBox("¿Debe actualizar el dato de recolección inicial?", vbYesNo + vbCritical + vbDefaultButton1, "Cambio de porcentaje de recolección") = vbYes Then
                TxtPorcentaje_recol_i.Text = TxtPorcentaje_recol.Text
            End If
        End If
        Totalizar()
    End Sub
End Class