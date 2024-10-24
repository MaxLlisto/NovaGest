Public Class AsientoSubvencion

    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        'Dim RsDiario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        'Dim RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        'Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        'If RsDiario.Open("SELECT * FROM diarios WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' ORDER BY diario", ObjetoGlobal.cn) Then
        '    TxtDiario.DataSource = RsDiario.cnDataSet.tables(0)
        '    TxtDiario.DisplayMember = "nombre_diario"
        '    TxtDiario.ValueMember = "diario"
        'End If

        'If RsPeriodo.Open("SELECT * FROM periodos WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and ejercicio = '" & ObjetoGlobal.EjercicioActual & "'ORDER BY periodo", ObjetoGlobal.cn) Then
        '    TxtPeriodo.DataSource = RsPeriodo.cnDataSet.Tables(0)
        '    TxtPeriodo.DisplayMember = "desc_periodo"
        '    TxtPeriodo.ValueMember = "periodo"
        'End If
        'TxtEmpresa.Text = ObjetoGlobal.EmpresaActual
        'TxtEjercicio.Text = ObjetoGlobal.EjercicioActual

        'If Rs.Open("Select * from empresas WHERE empresa = '" & TxtEmpresa.Text & "'", ObjetoGlobal.cn) Then
        '    lblEmpresa.Text = Rs!razon_social
        'End If
        'Rs.Close()
        'If Rs.Open("Select * from ejercicios_contab WHERE empresa = '" & TxtEmpresa.Text & "' and ejercicio = '" & ObjetoGlobal.EjercicioActual, ObjetoGlobal.cn) Then
        '    lblEjercicio.Text = Rs!descripcion
        'End If
        'Rs.Close()


        'RsDiario.Close()
        'RsPeriodo.Close()

    End Sub
    Private Sub OnLoad(e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles bAsiento.Click
        Dim ImporteAmortizado As Double
        Dim RsAmort = New cnRecordset.CnRecordset
        Dim rsHtco = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim numasiento As Double
        Dim DescripcionApunte As String
        Dim aDebeCta(4) As String
        Dim aImpoDeb(4) As Double
        Dim aHaberCta(4) As String
        Dim aImpoHab(4) As Double
        Dim RsAux = New cnRecordset.CnRecordset
        Dim RsPorcentajes = New cnRecordset.CnRecordset
        Dim RsAnterior = New cnRecordset.CnRecordset
        Dim ImporteAnterior As Double
        Dim TotalSubvencion As Double
        Dim Factor As Double
        Dim cuota As Double
        Dim cuota20 As Double
        Dim cuota80 As Double
        Dim i As Integer, j As Integer
        Dim trans As SqlClient.SqlTransaction
        Dim Ejercicios() As Integer
        Dim Porc() As Double
        Dim Cuotas() As Double
        Dim Ctas() As String
        Dim Periodos_sn As String = "N"

        'Dim ErrorVuelca As Integer

        'Datos requeridos
        If Strings.Trim(TxtDiario.Text) = "" Then
            MsgBox("Debe introducir un diario.", vbInformation, "")
            TxtDiario.Focus()
            Exit Sub
        End If
        If Trim(TxtPeriodo.Text) = "" Then
            MsgBox("Debe introducir un periodo.", vbInformation, "")
            TxtPeriodo.Focus()
            Exit Sub
        End If

        'Comprobamos compatibilidad fecha/periodo.
        Sql = "SELECT FECHA_INICIO,FECHA_FIN FROM PERIODOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO=" & (TxtPeriodo.Text)
        If RsAux.Open(Sql, ObjetoGlobal.cn) Then
            If RsAux.RecordCount > 0 Then
                If Not ((RsAux!fecha_inicio) <= (TxtFecha_asiento.value) And (RsAux!fecha_fin) >= (TxtFecha_asiento.value)) Then
                    MsgBox("La fecha seleccionada no pertenece al periodo seleccionado.", vbInformation, "")
                    TxtFecha_asiento.value = (RsAux!fecha_inicio)
                    Exit Sub
                End If
            Else
                MsgBox("No existe el periodo introducido para el ejercicio actual.", vbInformation, "")
                TxtPeriodo.Text = ""
                TxtPeriodo.Focus()
                Exit Sub
            End If
        End If

        Try
            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()
            Sql = "SELECT * FROM bienes WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND (porc_subvencion1 <> 0 OR porc_subvencion2 <> 0 OR porc_subvencion3 <> 0 OR porc_subvencion4 <> 0)"

            If Txtbien.Text.Trim <> "" Then
                Sql = Sql + " AND codigo_bien = '" & Txtbien.Text.Trim & "'"
            End If

            RsAmort.Open(Sql, ObjetoGlobal.cn,,,,,,, trans)
            numasiento = -1
            For i = 1 To RsAmort.RecordCount
                RsAmort.AbsolutePosition = i
                Sql = "SELECT * FROM htco_amortizacion  WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_bien='" & RsAmort!codigo_bien & "' AND Fecha_amortizacion BETWEEN '" & TxtDesdeFecha.value & "' AND '" & TxtHastaFecha.value & "' order by fecha_amortizacion"
                rsHtco.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans)
                For j = 1 To rsHtco.RecordCount
                    rsHtco.AbsolutePosition = j
                    ImporteAmortizado = rsHtco!importe_amort
                    Sql = "SELECT dbo.fn_porcentaje_para_subvencion('" + Trim(ObjetoGlobal.EmpresaActual) + "','" + Trim(RsAmort!codigo_bien) + "') as ps,"
                    Sql = Trim(Sql) + "dbo.fn_porcentaje_amortizacion('" + Trim(ObjetoGlobal.EmpresaActual) + "','" + Trim(RsAmort!codigo_bien) + "') as pa"
                    RsPorcentajes.Open(Sql, ObjetoGlobal.cn,,,,,,, trans)
                    Factor = 1
                    If RsPorcentajes!ps <> 0 And RsPorcentajes!pa <> 0 Then Factor = RsPorcentajes!ps / RsPorcentajes!pa
                    RsPorcentajes.Close()

                    If RsAmort!porc_subvencion1 <> 0 Then
                        If IsDBNull(RsAmort!period_sn) Then
                            Periodos_sn = "N"
                        Else
                            Periodos_sn = RsAmort!period_sn
                        End If
                        If Trim(Periodos_sn) = "N" Then
                            cuota = Math.Round(Factor * ImporteAmortizado * RsAmort!porc_subvencion1 / 100, 2)
                            ImporteAnterior = 0
                            Sql = "SELECT Isdbnull(SUM(subvencion_1),0) as yasubvencionado from htco_amortizacion where empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND codigo_bien = '" + Trim(RsAmort!codigo_bien) + "'"
                            Sql = Trim(Sql) + " AND fecha_amortizacion< '" + Format(rsHtco!Fecha_amortizacion, "dd/MM/yyyy") + "'"
                            RsAnterior.Open(Sql, ObjetoGlobal.cn,,,,,,, trans)
                            If RsAnterior.RecordCount > 0 Then ImporteAnterior = Math.Round(RsAnterior!yasubvencionado, 2)
                            RsAnterior.Close()
                            TotalSubvencion = Math.Round(RsAmort!valor_a_amortizar * RsAmort!porc_subvencion1 / 100, 2)
                            If Math.Abs(ImporteAnterior) >= Math.Abs(TotalSubvencion) Then
                                cuota = 0
                            ElseIf Math.Abs(cuota) > Math.Abs(TotalSubvencion - ImporteAnterior) Then
                                cuota = TotalSubvencion - ImporteAnterior
                            End If
                            If Math.Round(cuota, 2) <> 0 Then
                                DescripcionApunte = "TRASP. A INGRESOS SUBV. CAPITAL(" & RsAmort!codigo_bien & ")"
                                aDebeCta(0) = "840000000"
                                aDebeCta(1) = "479000000"
                                aDebeCta(2) = "830100000"
                                aDebeCta(3) = ""
                                aHaberCta(0) = "746000000"
                                aHaberCta(1) = "830100000"
                                aHaberCta(2) = "840000000"
                                cuota20 = Math.Round(cuota * 0.2, 2)
                                cuota80 = cuota - cuota20
                                aDebeCta(3) = RsAmort!cta_subvencion1
                                aImpoDeb(0) = cuota
                                aImpoDeb(1) = cuota20
                                aImpoDeb(2) = cuota20
                                aImpoDeb(3) = cuota80
                                aImpoHab(0) = cuota
                                aImpoHab(1) = cuota20
                                aImpoHab(2) = cuota
                                ' Al debe
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(0), "---", DescripcionApunte, aImpoDeb(0), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(0), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(1), "---", DescripcionApunte, aImpoDeb(1), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(1), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(2), "---", DescripcionApunte, aImpoDeb(2), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(2), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(3), "---", DescripcionApunte, aImpoDeb(3), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(3), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                ' Al haber
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(0)), "---", DescripcionApunte, 0, aImpoHab(0), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(0), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(1)), "---", DescripcionApunte, 0, aImpoHab(1), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(1), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(2)), "---", DescripcionApunte, 0, aImpoHab(2), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(2), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores

                                rsHtco!subvencion_1 = Math.Round(cuota, 2)
                                rsHtco.Update()
                            End If
                        Else
                            Dim nEjercicioInicial As Integer = CInt(RsAmort!ejercicio1)
                            Dim nEjercicioFinal As Integer = CInt(RsAmort!ejercicio1 + RsAmort!anno_periodos - 1)

                            ReDim Ejercicios(RsAmort!anno_periodos)
                            ReDim Porc(RsAmort!anno_periodos)
                            ReDim Ctas(RsAmort!anno_periodos)
                            ReDim Cuotas(RsAmort!anno_periodos)
                            Dim Ejer As Integer
                            Dim UltimaPorc As Double
                            Dim EstaDentroMargen As Boolean = False

                            cuota = 0

                            Dim oo As Integer
                            For oo = 0 To RsAmort!anno_periodos - 1
                                Ejercicios(oo) = nEjercicioInicial + oo
                            Next
                            'If CInt(ObjetoGlobal.EjercicioActual) >= nEjercicioInicial And CInt(ObjetoGlobal.EjercicioActual) <= nEjercicioFinal Then
                            oo = 0
                            While Ejercicios(oo) <= CInt(ObjetoGlobal.EjercicioActual)
                                If oo + 1 <= RsAmort!anno_periodos Then
                                    Ejer = CInt(0 & RsAmort("ejercicio" & (oo + 1)))
                                    If Ejer > 0 Then
                                        Porc(oo + 1) = CDbl(0 & RsAmort("porc" & (oo + 1)))
                                        Ctas(oo + 1) = "" & RsAmort("CtaCble" & (oo + 1))
                                        If Ejer = CInt(ObjetoGlobal.EjercicioActual) Then
                                            Cuotas(oo + 1) = Math.Round(((ImporteAmortizado * Porc(oo + 1)) / 100) * (oo + 1), 2)
                                            EstaDentroMargen = True
                                        ElseIf Ejer < CInt(ObjetoGlobal.EjercicioActual) Then
                                            Cuotas(oo + 1) = Math.Round(((ImporteAmortizado * Porc(oo + 1)) / 100), 2)
                                        End If
                                        UltimaPorc = Cuotas(oo + 1)
                                        cuota = cuota + Cuotas(oo + 1)
                                    End If
                                End If
                                oo = oo + 1
                                If oo + 1 > RsAmort!anno_periodos Then
                                    Exit While
                                End If
                            End While
                            For oo = 1 To UBound(Ctas)
                                If Math.Round(Cuotas(oo), 2) <> 0 Then
                                    DescripcionApunte = "TRASP. A INGRESOS SUBV. CAPITAL(" & RsAmort!codigo_bien & ")"
                                    aDebeCta(0) = "840000000"
                                    aDebeCta(1) = "479000000"
                                    aDebeCta(2) = "830100000"
                                    aDebeCta(3) = ""
                                    aHaberCta(0) = "746000000"
                                    aHaberCta(1) = "830100000"
                                    aHaberCta(2) = "840000000"
                                    cuota20 = Math.Round(Cuotas(oo) * 0.2, 2)
                                    cuota80 = Cuotas(oo) - cuota20
                                    aDebeCta(3) = Ctas(oo)
                                    aImpoDeb(0) = Cuotas(oo)
                                    aImpoDeb(1) = cuota20
                                    aImpoDeb(2) = cuota20
                                    aImpoDeb(3) = cuota80
                                    aImpoHab(0) = Cuotas(oo)
                                    aImpoHab(1) = cuota20
                                    aImpoHab(2) = Cuotas(oo)
                                    ' Al debe
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(0), "---", DescripcionApunte, aImpoDeb(0), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(0), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(1), "---", DescripcionApunte, aImpoDeb(1), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(1), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(2), "---", DescripcionApunte, aImpoDeb(2), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(2), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(3), "---", DescripcionApunte, aImpoDeb(3), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(3), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                    ' Al haber
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(0)), "---", DescripcionApunte, 0, aImpoHab(0), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(0), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(1)), "---", DescripcionApunte, 0, aImpoHab(1), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(1), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                    If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(2)), "---", DescripcionApunte, 0, aImpoHab(2), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(2), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                End If
                            Next
                            'End If
                            rsHtco!subvencion_1 = Math.Round(cuota, 2)
                            rsHtco.Update()
                        End If
                    End If

                    If Not IsDBNull(RsAmort!porc_subvencion2) Then
                        If RsAmort!porc_subvencion2 <> 0 Then
                            cuota = Math.Round(Factor * ImporteAmortizado * RsAmort!porc_subvencion2 / 100, 2)
                            ImporteAnterior = 0
                            Sql = "SELECT Isdbnull(SUM(subvencion_2),0) as yasubvencionado from htco_amortizacion where empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND codigo_bien = '" + Trim(RsAmort!codigo_bien) + "'"
                            Sql = Trim(Sql) + " AND fecha_amortizacion < '" + Format(rsHtco!Fecha_amortizacion, "dd/MM/yyyy") + "'"
                            RsAnterior.Open(Sql, ObjetoGlobal.cn,,,,,,, trans)
                            If RsAnterior.RecordCount > 0 Then ImporteAnterior = Math.Round(RsAnterior!yasubvencionado, 2)
                            RsAnterior.Close()
                            TotalSubvencion = Math.Round(RsAmort!valor_a_amortizar * RsAmort!porc_subvencion2 / 100, 2)
                            If Math.Abs(ImporteAnterior) >= Math.Abs(TotalSubvencion) Then
                                cuota = 0
                            ElseIf Math.Abs(cuota) > Math.Abs(TotalSubvencion - ImporteAnterior) Then
                                cuota = TotalSubvencion - ImporteAnterior
                            End If
                            If Math.Round(cuota, 2) <> 0 Then
                                DescripcionApunte = "TRASP. A INGRESOS SUBV. CAPITAL(" & RsAmort!codigo_bien & ")"
                                aDebeCta(0) = "840000000"
                                aDebeCta(1) = "479000000"
                                aDebeCta(2) = "830100000"
                                aDebeCta(3) = ""
                                aHaberCta(0) = "746000000"
                                aHaberCta(1) = "830100000"
                                aHaberCta(2) = "840000000"
                                cuota20 = Math.Round(cuota * 0.2, 2)
                                cuota80 = cuota - cuota20
                                aDebeCta(3) = RsAmort!cta_subvencion2
                                aImpoDeb(0) = cuota
                                aImpoDeb(1) = cuota20
                                aImpoDeb(2) = cuota20
                                aImpoDeb(3) = cuota80
                                aImpoHab(0) = cuota
                                aImpoHab(1) = cuota20
                                aImpoHab(2) = cuota
                                ' Al debe
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(0), "---", DescripcionApunte, aImpoDeb(0), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(0), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(1), "---", DescripcionApunte, aImpoDeb(1), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(1), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(2), "---", DescripcionApunte, aImpoDeb(2), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(2), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(3), "---", DescripcionApunte, aImpoDeb(3), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(3), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                ' Al haber
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(0)), "---", DescripcionApunte, 0, aImpoHab(0), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(0), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(1)), "---", DescripcionApunte, 0, aImpoHab(1), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(1), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(2)), "---", DescripcionApunte, 0, aImpoHab(2), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(2), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores

                                rsHtco!subvencion_2 = Math.Round(cuota, 2)
                                rsHtco.Update()
                            End If
                        End If
                    End If

                    If Not IsDBNull(RsAmort!porc_subvencion3) Then
                        If RsAmort!porc_subvencion3 <> 0 Then
                            cuota = Math.Round(Factor * ImporteAmortizado * RsAmort!porc_subvencion3 / 100, 2)
                            ImporteAnterior = 0
                            Sql = "SELECT Isdbnull(SUM(subvencion_3),0) as yasubvencionado from htco_amortizacion where empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND codigo_bien = '" + Trim(RsAmort!codigo_bien) + "'"
                            Sql = Trim(Sql) + " AND fecha_amortizacion< '" + Format(rsHtco!Fecha_amortizacion, "dd/MM/yyyy") + "'"
                            RsAnterior.Open(Sql, ObjetoGlobal.cn,,,,,,, trans)
                            If RsAnterior.RecordCount > 0 Then ImporteAnterior = Math.Round(RsAnterior!yasubvencionado, 2)
                            RsAnterior.Close()
                            TotalSubvencion = Math.Round(RsAmort!valor_a_amortizar * RsAmort!porc_subvencion3 / 100, 2)
                            If Math.Abs(ImporteAnterior) >= Math.Abs(TotalSubvencion) Then
                                cuota = 0
                            ElseIf Math.Abs(cuota) > Math.Abs(TotalSubvencion - ImporteAnterior) Then
                                cuota = TotalSubvencion - ImporteAnterior
                            End If
                            If Math.Round(cuota, 2) <> 0 Then
                                DescripcionApunte = "TRASP. A INGRESOS SUBV. CAPITAL(" & RsAmort!codigo_bien & ")"
                                aDebeCta(0) = "840000000"
                                aDebeCta(1) = "479000000"
                                aDebeCta(2) = "830100000"
                                aDebeCta(3) = ""
                                aHaberCta(0) = "746000000"
                                aHaberCta(1) = "830100000"
                                aHaberCta(2) = "840000000"
                                cuota20 = Math.Round(cuota * 0.2, 2)
                                cuota80 = cuota - cuota20
                                aDebeCta(3) = RsAmort!cta_subvencion3
                                aImpoDeb(0) = cuota
                                aImpoDeb(1) = cuota20
                                aImpoDeb(2) = cuota20
                                aImpoDeb(3) = cuota80
                                aImpoHab(0) = cuota
                                aImpoHab(1) = cuota20
                                aImpoHab(2) = cuota
                                ' Al debe
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(0), "---", DescripcionApunte, aImpoDeb(0), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(0), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(1), "---", DescripcionApunte, aImpoDeb(1), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(1), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(2), "---", DescripcionApunte, aImpoDeb(2), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(2), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(3), "---", DescripcionApunte, aImpoDeb(3), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(3), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                ' Al haber
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(0)), "---", DescripcionApunte, 0, aImpoHab(0), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(0), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(1)), "---", DescripcionApunte, 0, aImpoHab(1), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(1), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(2)), "---", DescripcionApunte, 0, aImpoHab(2), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(2), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores

                                rsHtco!subvencion_3 = Math.Round(cuota, 2)
                                rsHtco.Update()
                            End If
                        End If
                    End If

                    If Not IsDBNull(RsAmort!porc_subvencion4) Then
                        If RsAmort!porc_subvencion4 <> 0 Then
                            cuota = Math.Round(Factor * ImporteAmortizado * RsAmort!porc_subvencion4 / 100, 2)
                            ImporteAnterior = 0
                            Sql = "SELECT Isdbnull(SUM(subvencion_4),0) as yasubvencionado from htco_amortizacion where empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND codigo_bien = '" + Trim(RsAmort!codigo_bien) + "'"
                            Sql = Trim(Sql) + " AND fecha_amortizacion< '" + Format(rsHtco!Fecha_amortizacion, "dd/MM/yyyy") + "'"
                            RsAnterior.Open(Sql, ObjetoGlobal.cn,,,,,,, trans)
                            If RsAnterior.RecordCount > 0 Then ImporteAnterior = Math.Round(RsAnterior!yasubvencionado, 2)
                            RsAnterior.Close()
                            TotalSubvencion = Math.Round(RsAmort!valor_a_amortizar * RsAmort!porc_subvencion4 / 100, 2)
                            If Math.Abs(ImporteAnterior) >= Math.Abs(TotalSubvencion) Then
                                cuota = 0
                            ElseIf Math.Abs(cuota) > Math.Abs(TotalSubvencion - ImporteAnterior) Then
                                cuota = TotalSubvencion - ImporteAnterior
                            End If
                            If Math.Round(cuota, 2) <> 0 Then
                                DescripcionApunte = "TRASP. A INGRESOS SUBV. CAPITAL(" & RsAmort!codigo_bien & ")"
                                aDebeCta(0) = "840000000"
                                aDebeCta(1) = "479000000"
                                aDebeCta(2) = "830100000"
                                aDebeCta(3) = ""
                                aHaberCta(0) = "746000000"
                                aHaberCta(1) = "830100000"
                                aHaberCta(2) = "840000000"
                                cuota20 = Math.Round(cuota * 0.2, 2)
                                cuota80 = cuota - cuota20
                                aDebeCta(3) = RsAmort!cta_subvencion4
                                aImpoDeb(0) = cuota
                                aImpoDeb(1) = cuota20
                                aImpoDeb(2) = cuota20
                                aImpoDeb(3) = cuota80
                                aImpoHab(0) = cuota
                                aImpoHab(1) = cuota20
                                aImpoHab(2) = cuota
                                ' Al debe
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(0), "---", DescripcionApunte, aImpoDeb(0), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(0), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(1), "---", DescripcionApunte, aImpoDeb(1), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(1), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(2), "---", DescripcionApunte, aImpoDeb(2), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(2), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & aDebeCta(3), "---", DescripcionApunte, aImpoDeb(3), 0, "N", "N", "N", "" & Trim(RsAmort!Divisa), aImpoDeb(3), 0, 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                ' Al haber
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(0)), "---", DescripcionApunte, 0, aImpoHab(0), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(0), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(1)), "---", DescripcionApunte, 0, aImpoHab(1), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(1), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores
                                If VuelcaApunteContable(ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual, TxtFecha_asiento.value, TxtDiario.Text, numasiento, -1, TxtPeriodo.Text, "" & Trim(aHaberCta(2)), "---", DescripcionApunte, 0, aImpoHab(2), "N", "N", "N", "" & Trim(RsAmort!Divisa), 0, aImpoHab(2), 1, "N", 0, 0, "", "" & TxtFecha_asiento.value, True,,, trans) Then GoTo errores

                                rsHtco!subvencion_4 = Math.Round(cuota, 2)
                                rsHtco.Update()
                            End If
                        End If
                    End If
                Next
                rsHtco.Close()
            Next
            trans.Commit()
            ObjetoGlobal.cn.Close()

            MsgBox("Proceso del asiento de traspaso a ingresos subv. de capital realizado.", vbInformation, "")
            Exit Sub

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("Se ha producido un error al grabar los datos. Error :" & Err.Description, vbInformation, "")
            Return
        End Try

errores:
        trans.Rollback()
        ObjetoGlobal.cn.Close()
        MsgBox("Se ha producido un error al grabar los datos. Error :" & Err.Description, vbInformation, "")

    End Sub

    Public Function VuelcaApunteContable(ByVal Empresa As String, ByVal Ejercicio As String, ByVal fecha_asiento As Date, ByVal diario As Integer, ByRef numero_asiento As Double, ByRef Posicion As Double, ByVal periodo As Integer, ByVal codigo_cuenta As String, ByVal concepto As String, ByVal descripcion_apunte As String, ByVal importe_debe As Double, ByVal importe_haber As Double, ByVal punteado As String, ByVal punteado2 As String, ByVal punteado3 As String, ByVal cod_divisa As String, ByVal debe_divisa As Double, ByVal haber_divisa As Double, ByVal cambio_divisa As Double, ByVal anot_contravalor As String, ByVal contravalor_debe As Double, ByVal contravalor_haber As Double, ByVal contrapartida As String, ByVal fecha_valor As String, ByVal Optimista As Boolean, Optional rsCabecera As cnRecordset.CnRecordset = Nothing, Optional FacturaClaveDocumento As Double = 0, Optional trans As SqlClient.SqlTransaction = Nothing) As Integer
        Dim RsApunte = New cnRecordset.CnRecordset
        Dim Rs = New cnRecordset.CnRecordset
        Dim ValorClaveApunte As Long
        Dim CompraVenta As String
        Dim MaxNumeroAsiento As Long
        Dim MaxPosicion As Long

        'Si la cuenta está en divisa, debo pasar los datos:
        'codigo_divisa,cambio_divisa,debe_divisa,haber_divisa,anot_contravalor="S",contravalor_debe,contravalor_haber
        'Si la cuenta no está en divisa, pero se le pasa el contravalor de la divisa secundaria:
        'anot_contravalor = "S",contravalor_debe,contravalor_haber
        'anot_contravalor = "N" y todo lo demás a 0.
        'Si la llamada se hace Optimista=False, no hace falta pasar estos datos,
        'dentro de la función se calcularán.

        'Si Optimista=true, supongo que no tengo que comprobar que la cuenta, la contrapartida,
        'el diario y el periodo son correctos.

        'Si en la tabla cabeceras_asiento no existe el asiento, en numero_asiento paso -1,
        'de esa forma creará el asiento. Las siguientes veces que se llame a la función,
        'habrá que pasarle el numero_asiento que se crea aquí.
        'Puedo pasar o no la cabecera, pero si numero_asiento=-1,
        'la cabecera se creará aquí dentro.

        'Si la posición es -1, busco una posición para grabar. Si la posición es distinta
        'de -1, buscaré la que hayan pasado, y en caso de que ya exista, aumentaré en uno
        'su valor (hasta que encuentre uno que no exista). Este caso me servirá sólo para
        'intercalar, si no intercalo paso siempre -1, así me aseguro de que no hay
        'conflictos entre usuarios


        Try



            importe_debe = Math.Round(importe_debe, 2)
            importe_haber = Math.Round(importe_haber, 2)

            contravalor_debe = Math.Round(contravalor_debe, 2)
            contravalor_haber = Math.Round(contravalor_haber, 2)
            'comprobaciones obligadas.......
            If importe_debe = 0 And importe_haber = 0 Then
                'MsgBox ("No se permite introducir apuntes con debe=0 y haber=0", vbOKOnly, "Error al grabar apuntes"
                Return 0
            End If

            If Trim(descripcion_apunte) = "" Then
                MsgBox("No se permite introducir apuntes sin descripción", vbOKOnly, "Error al grabar apuntes")
                VuelcaApunteContable = 2 'Error previsto: descripcion=""
                Return 2
            End If
            descripcion_apunte = Strings.Left(Trim(descripcion_apunte), 60)


            'Calculo Clave Apunte
            Rs.Open("SELECT MAX(CLAVE_APUNTE) as ultimo_asiento FROM ASIENTOS WHERE EMPRESA='" & Trim(Empresa) & "'", ObjetoGlobal.cn,,,,,,, trans)
            ValorClaveApunte = Rs!ultimo_asiento + 1
            Rs.Close()

            'Calculo compraventa
            Rs.Open("SELECT COMPRA_VTA FROM CONCEPTOS WHERE EMPRESA='" & Trim(Empresa) & "' AND codigo_concepto='" & Trim(concepto) & "'", ObjetoGlobal.cn,,,,,,, trans)
            If Rs.RecordCount <= 0 Then
                MsgBox("No existe el concepto" & Trim(concepto), vbOKOnly, "Error al grabar apuntes")
                VuelcaApunteContable = 3 'Error previsto: No existe el concepto.
                Return 3
            Else
                CompraVenta = IIf("S" = "" & Rs!compra_vta, "S", "N")
            End If
            Rs.Close()

            'Comprobaciones opcionales........
            If Not Optimista Then
                'Si no es optimista, no me fío de los datos que me pasan,
                'y hago las comprobaciones necesarias... cuentas, periodo y fecha_asiento, diario
                If Len(Trim(codigo_cuenta)) <> ObjetoGlobal.NivelDeCuenta Then
                    MsgBox("El número de dígitos de la cuenta: " & codigo_cuenta & " no coincide con el nivel contable", vbOKOnly, "Error al grabar apuntes")
                    VuelcaApunteContable = 10 'Error previsto: No existe la cuenta
                    Return 10
                End If
                Rs.Open("SELECT EMPRESA, COD_DIVISA FROM CUENTAS WHERE EMPRESA='" & Trim(Empresa) & "' AND CODIGO_CUENTA='" & Trim(codigo_cuenta) & "'", ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount <= 0 Then
                    MsgBox("No existe la cuenta: " & codigo_cuenta & vbCr & "Descripción: " & descripcion_apunte, vbOKOnly, "Error al grabar apuntes")
                    VuelcaApunteContable = 10 'Error previsto: No existe la cuenta
                    Return 10
                Else
                    'En caso de que la llamada se haga desde fuera de apuntes, aquí se calculan
                    'los cambios y las anotaciones de divisa y de contravalor...
                    If Not IsDBNull(Rs!cod_divisa) Then 'And (cod_divisa = "" Or cambio_divisa = 0 Or (debe_divisa = 0 And haber_divisa = 0) Or anot_contravalor <> "S" Or (contravalor_debe = 0 And contravalor_haber = 0)) Then
                        '              MsgBox ("Para grabar asientos en esta cuenta son necesarios importes en divisa y en contravalor", vbOKOnly, "Error al grabar apuntes"
                        '              VuelcaApunteContable = 11 'Error previsto: Algún dato de divisa o contravalor no se ha pasao correctamente
                        '              Exit Function
                        'anot_divisa = "S"
                        cod_divisa = Trim(Rs!cod_divisa)
                        cambio_divisa = 1 'CalculaCambioDivisa(ObjetoGlobal.DivisaActual, cod_divisa, fecha_asiento)
                        debe_divisa = importe_debe 'DevuelveCambioDivisaGenerico(importe_debe, ObjetoGlobal.DivisaActual, cod_divisa)
                        haber_divisa = importe_haber 'DevuelveCambioDivisaGenerico(importe_haber, ObjetoGlobal.DivisaActual, cod_divisa)
                        anot_contravalor = "S"
                        contravalor_debe = importe_debe 'DevuelveCambioDivisaGenerico(importe_debe, ObjetoGlobal.DivisaActual, ObjetoGlobal.divisasecundaria)
                        contravalor_haber = importe_haber 'DevuelveCambioDivisaGenerico(importe_haber, ObjetoGlobal.DivisaActual, ObjetoGlobal.divisasecundaria)
                    End If
                End If
                Rs.Close()

                If Len(Trim(contrapartida)) <> ObjetoGlobal.NivelDeCuenta And Trim(contrapartida) <> "" Then
                    MsgBox("El número de dígitos de la cuenta de contrapartida: " & contrapartida & " no coincide con el nivel contable", vbOKOnly, "Error al grabar apuntes")
                    VuelcaApunteContable = 10 'Error previsto: No existe la cuenta
                    Return 10
                End If
                If Trim(contrapartida) <> "" Then
                    Rs.Open("SELECT EMPRESA FROM CUENTAS WHERE EMPRESA='" & Trim(Empresa) & "' AND CODIGO_CUENTA='" & Trim(contrapartida) & "'", ObjetoGlobal.cn,,,,,,, trans)
                    If Rs.RecordCount <= 0 Then
                        MsgBox("No existe la cuenta de contrapartida: " & contrapartida, vbOKOnly, "Error al grabar apuntes")
                        VuelcaApunteContable = 4 'Error previsto: No existe la cuenta de contrapartida
                        Return 4
                    End If
                    Rs.Close()
                End If

                Rs.Open("SELECT EMPRESA FROM DIARIOS WHERE EMPRESA='" & Trim(Empresa) & "' AND DIARIO=" & diario, ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount <= 0 Then
                    MsgBox("No existe el diario especificado: " & diario, vbOKOnly, "Error al grabar apuntes")
                    VuelcaApunteContable = 5 'Error previsto: No existe el diario
                    Return 5
                End If
                Rs.Close()

                Rs.Open("SELECT EMPRESA FROM PERIODOS WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND PERIODO=" & periodo & " AND fecha_inicio <= '" & Format(fecha_asiento, "dd/MM/yyyy") & "' AND fecha_fin>='" & Format(fecha_asiento, "dd/MM/yyyy") & "'", ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount <= 0 Then
                    MsgBox("No existe el periodo: " & periodo & ", o la fecha " & Format(fecha_asiento, "dd/MM/yyyy") & " no pertenece al periodo: " & periodo, vbOKOnly, "Error al grabar apuntes")
                    VuelcaApunteContable = 6 'Error previsto: No existe el periodo y la fecha no pertenece al ejercicio
                    Return 6
                End If
                Rs.Close()
            End If 'Optimista

            'If Trim(cod_divisa) = Trim(ObjetoGlobal.DivisaActual) Then cod_divisa = ""
            'If Trim(cod_divisa) = Trim(ObjetoGlobal.divisasecundaria) Then cod_divisa = ""
            cod_divisa = ""


            'Graba cabeceras_asiento
            'Compruebo el valor del número de asiento,
            'para saber si debo crear el asiento o si sólo debo actualizarlo...
            If numero_asiento = -1 Then
                'Si numero_asiento=-1, no existe la cabecera y hay que crearla.

                If rsCabecera Is Nothing Then
                    rsCabecera = New cnRecordset.CnRecordset
                End If

                'Busco el siguiente número de asiento.
                Rs.Open("SELECT MAX(NUMERO_ASIENTO) as ultimo_asiento FROM CABECERAS_ASIENTO WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(fecha_asiento, "dd/MM/yyyy") & "' AND DIARIO= " & diario, ObjetoGlobal.cn,,,,,,, trans)
                MaxNumeroAsiento = Rs!ultimo_asiento + 1
                Rs.Close()
                numero_asiento = MaxNumeroAsiento

                'Me dispongo a grabar la cabecera
                rsCabecera.Open("SELECT * FROM CABECERAS_ASIENTO WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                rsCabecera.AddNew()
                rsCabecera!Empresa = Trim(Empresa)
                rsCabecera!Ejercicio = Trim(Ejercicio)
                rsCabecera!fecha_asiento = fecha_asiento
                rsCabecera!diario = diario
                rsCabecera!numero_asiento = numero_asiento
                rsCabecera!importe_debe = (importe_debe)
                rsCabecera!importe_haber = (importe_haber)
                rsCabecera!periodo = periodo
                rsCabecera!usucreacion = ObjetoGlobal.UsuarioActual
                rsCabecera!fechacreacion = DateTime.Now.Date
                rsCabecera!horacreacion = DateTime.Now.ToString("HH:mm:ss")
                rsCabecera.Update()
            Else 'nos pasan un número de asiento
                If rsCabecera Is Nothing Then
                    'pero no he pasado la cabecera como variable.
                    'Tengo que abrirla para modificar los importes...
                    rsCabecera = New cnRecordset.CnRecordset
                    rsCabecera.Open("SELECT * FROM CABECERAS_ASIENTO WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(fecha_asiento, "dd/MM/yyyy") & "' AND DIARIO= " & diario & " AND NUMERO_ASIENTO=" & numero_asiento, ObjetoGlobal.cn, True,,,,,, trans)
                    If rsCabecera.RecordCount > 0 Then
                        'si existe la modifico
                        rsCabecera!importe_debe = Math.Round(rsCabecera!importe_debe, 2) + importe_debe '*
                        rsCabecera!importe_haber = Math.Round(rsCabecera!importe_haber, 2) + importe_haber '*
                        'rsCabecera!usumodificacion = ObjetoGlobal.NombreUSUario
                        rsCabecera!fechacreacion = DateTime.Now.Date
                        rsCabecera!horacreacion = DateTime.Now.ToString("HH:mm:ss")
                        rsCabecera.Update()
                    Else 'si no existe, la grabo...
                        rsCabecera.AddNew()
                        rsCabecera!Empresa = Trim(Empresa)
                        rsCabecera!Ejercicio = Trim(Ejercicio)
                        rsCabecera!fecha_asiento = fecha_asiento
                        rsCabecera!diario = diario
                        rsCabecera!numero_asiento = numero_asiento
                        rsCabecera!importe_debe = (importe_debe)
                        rsCabecera!importe_haber = (importe_haber)
                        rsCabecera!periodo = periodo
                        rsCabecera!usucreacion = ObjetoGlobal.UsuarioActual
                        rsCabecera!fechacreacion = DateTime.Now.Date
                        rsCabecera!horacreacion = DateTime.Now.ToString("HH:mm:ss")
                        rsCabecera.Update()
                    End If 'end recordcount
                    'Else 'no is missing
                    '    If rsCabecera.RecordCount > 0 Then
                    '        rsCabecera!importe_debe = Math.Round(rsCabecera!importe_de, 2) + importe_debe '*
                    '        rsCabecera!importe_haber = Math.Round(rsCabecera!importe_haber, 2) + importe_haber '*
                    '        'rsCabecera!usumodificacion = ObjetoGlobal.NombreUSUario
                    '        rsCabecera!fechacreacion = DateTime.Now.Date
                    '        rsCabecera!horacreacion = DateTime.Now.ToString("HH:mm:ss")
                    '        rsCabecera.Update()
                    '    Else 'si no existe, grabo
                    '        rsCabecera.AddNew()
                    '        rsCabecera!Empresa = Trim(Empresa)
                    '        rsCabecera!Ejercicio = Trim(Ejercicio)
                    '        rsCabecera!fecha_asiento = fecha_asiento
                    '        rsCabecera!diario = diario
                    '        rsCabecera!numero_asiento = numero_asiento
                    '        rsCabecera!importe_debe = (importe_debe)
                    '        rsCabecera!importe_haber = (importe_haber)
                    '        rsCabecera!periodo = periodo
                    '        rsCabecera!usucreacion = ObjetoGlobal.UsuarioActual
                    '        rsCabecera!fechacreacion = DateTime.Now.Date
                    '        rsCabecera!horacreacion = DateTime.Now.ToString("HH:mm:ss")
                    '        rsCabecera.Update()
                    '    End If 'recordcount
                End If 'is missing
            End If 'numero_asiento
            'busco la posición del apunte
            If Posicion = -1 Then
                Rs.Open("SELECT MAX(POSICION) AS ultima_posicion FROM ASIENTOS WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(fecha_asiento, "dd/MM/yyyy") & "' AND DIARIO= " & diario & " AND NUMERO_ASIENTO=" & numero_asiento, ObjetoGlobal.cn,,,,,,, trans)
                If IsDBNull(Rs!ultima_posicion) Then
                    MaxPosicion = 10
                Else
                    MaxPosicion = Rs!ultima_posicion + 10
                End If
                Rs.Close()
                Posicion = MaxPosicion
            Else
                Rs.Open("SELECT POSICION FROM ASIENTOS WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(fecha_asiento, "dd/MM/yyyy") & "' AND DIARIO= " & diario & " AND NUMERO_ASIENTO=" & numero_asiento & " AND POSICION=" & Posicion, ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount > 0 Then 'como ya existe la posición, lo pondré al final
                    Rs.Close()
                    Rs.Open("SELECT MAX(POSICION) as ultima_posicion FROM ASIENTOS WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(fecha_asiento, "dd/MM/yyyy") & "' AND DIARIO= " & diario & " AND NUMERO_ASIENTO=" & numero_asiento, ObjetoGlobal.cn,,,,,,, trans)
                    MaxPosicion = Rs!ultima_posicion + 10
                    Rs.Close()
                    Posicion = MaxPosicion
                End If
            End If
            'grabar apunte...
            'AKIIII

            'Calculo Clave Apunte
            Rs.Open("SELECT MAX(CLAVE_APUNTE) as ultimo_apunte FROM ASIENTOS WHERE EMPRESA='" & Trim(Empresa) & "'", ObjetoGlobal.cn,,,,,,, trans)
            'If rs.RecordCount > 0 Then
            ValorClaveApunte = Rs!ultimo_apunte + 1
            'End If
            Rs.Close()


            RsApunte.Open("SELECT * FROM ASIENTOS WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            RsApunte.AddNew()
            RsApunte!Empresa = Trim(Empresa)
            RsApunte!Ejercicio = Trim(Ejercicio)
            RsApunte!fecha_asiento = (fecha_asiento)
            RsApunte!diario = Val(diario)
            RsApunte!numero_asiento = numero_asiento
            RsApunte!Posicion = Posicion
            RsApunte!periodo = Val(periodo)
            RsApunte!codigo_cuenta = Val(Trim(codigo_cuenta))
            RsApunte!concepto = Trim(concepto)
            RsApunte!descripcion_apunte = Trim(descripcion_apunte)
            RsApunte!importe_debe = (importe_debe)
            RsApunte!importe_haber = (importe_haber)
            RsApunte!clave_apunte = ValorClaveApunte
            RsApunte!fecha_act = DateTime.Now.Date
            RsApunte!usuario_act = ObjetoGlobal.UsuarioActual
            RsApunte!tipo_act = "C"
            RsApunte!punteado = "N"
            RsApunte!punteado2 = "N"
            RsApunte!punteado3 = "N"
            If Trim(punteado) = "S" Then
                RsApunte!punteado = Trim(punteado)
            End If
            If Trim(punteado2) = "S" Then
                RsApunte!punteado2 = Trim(punteado2)
            End If
            If Trim(punteado3) = "S" Then
                RsApunte!punteado3 = Trim(punteado3)
            End If
            If Trim(cod_divisa) <> "" Then
                RsApunte!anot_divisa = "S"
                RsApunte!cod_divisa = Trim(cod_divisa)
                RsApunte!debe_divisa = debe_divisa
                RsApunte!haber_divisa = haber_divisa
                RsApunte!cambio_divisa = cambio_divisa
            Else
                RsApunte!anot_divisa = "N"
                RsApunte!debe_divisa = 0
                RsApunte!haber_divisa = 0
                RsApunte!cod_divisa = ""
                RsApunte!cambio_divisa = 0
            End If
            RsApunte!anot_contravalor = "N" '*
            If Trim(anot_contravalor) = "S" Then
                RsApunte!anot_contravalor = "S"
                RsApunte!contravalor_debe = contravalor_debe
                RsApunte!contravalor_haber = contravalor_haber
            Else
                RsApunte!contravalor_debe = 0
                RsApunte!contravalor_haber = 0
            End If
            RsApunte!compra_vta = "N"
            If CompraVenta = "S" Then
                RsApunte!compra_vta = "S"
            Else
                RsApunte!compra_vta = "N"
            End If
            If Trim(contrapartida) <> "" Then RsApunte!contrapartida = Trim(contrapartida)
            If Trim(fecha_valor) <> "" Then RsApunte!fecha_valor = (fecha_valor)
            RsApunte!enviado_concili = "N"
            'RsApunte!f_envio_concili = Constants.vbNull
            RsApunte!enviado_tesoreria = "N"
            'RsApunte!f_envio_tesoreria = Constants.vbNull
            RsApunte!usucreacion = ObjetoGlobal.UsuarioActual
            RsApunte!fechacreacion = DateTime.Now.Date
            RsApunte!horacreacion = DateTime.Now.ToString("HH:mm:ss")
            RsApunte.Update()

            Dim RsDocumentos = New cnRecordset.CnRecordset

            If FacturaClaveDocumento > 0 Then
                RsDocumentos.Open("SELECT * FROM DOCUMENTOS_APUNTES WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                RsDocumentos.AddNew()
                RsDocumentos!Empresa = (RsApunte!Empresa)
                RsDocumentos!Ejercicio = (RsApunte!Ejercicio)
                RsDocumentos!clave_apunt = ValorClaveApunte
                RsDocumentos!contador = 10
                RsDocumentos!clave_docum = FacturaClaveDocumento
                RsDocumentos.Update()
            End If

            'GRABA SALDOS
            If RsApunte!anot_divisa = "S" Then
                If VuelcaDatosSaldos(RsApunte!Empresa, RsApunte!Ejercicio, RsApunte!codigo_cuenta, RsApunte!diario, RsApunte!periodo, RsApunte!debe_divisa, RsApunte!haber_divisa, False, "" & RsApunte!cod_divisa, False, trans) = 1000 Then
                    MsgBox("No se han grabado correctamente los saldos", vbOKOnly, "Error saldos")
                    VuelcaApunteContable = 8 'Error previsto: saldos en divisa mal grabados
                    RsApunte.Close()
                    Return 8
                End If
            End If
            If VuelcaDatosSaldos(RsApunte!Empresa, RsApunte!Ejercicio, RsApunte!codigo_cuenta, RsApunte!diario, RsApunte!periodo, RsApunte!importe_debe, RsApunte!importe_haber, IIf(RsApunte!compra_vta = "S", True, False), "EUR", True, trans) = 1000 Then
                MsgBox("No se han grabado correctamente los saldos", vbOKOnly, "Error saldos")
                VuelcaApunteContable = 9 'Error previsto: saldos mal grabados
                RsApunte.Close()
                Return 9
            End If

            RsApunte.Close()
            Return 0

        Catch ex As Exception
            RsApunte.CancelUpdate()
            Dim ErrorApunte
            ErrorApunte = Err.Number & ": " & Err.Description
            MsgBox(ErrorApunte, vbOKOnly, "Error al grabar apuntes")
            Return 1000
        End Try


    End Function

    Public Function VuelcaDatosSaldos(Empresa As String, Ejercicio As String, cuenta As String, diario As Integer, periodo As Integer, ImporteDebe As Double, ImporteHaber As Double, CompraVenta As Boolean, Divisa As String, GrabaMayores As Boolean, Optional trans As SqlClient.SqlTransaction = Nothing) As Integer
        Dim i As Integer
        Dim Nivel As Integer
        Dim NivelAComprobar As Integer
        Dim CuentaABuscar As String
        Dim rsSaldos = New cnRecordset.CnRecordset

        Try
            NivelAComprobar = 0
            Nivel = Len(Trim(cuenta))
            For i = Nivel To ObjetoGlobal.niveldesaldos Step -1
                If ObjetoGlobal.nivelesdisponibles(i) Then
                    NivelAComprobar = i
                    CuentaABuscar = Strings.Left(Trim(cuenta), NivelAComprobar)
                    rsSaldos.Open("SELECT * FROM SALDOS WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND CODIGO_CUENTA='" & Trim(CuentaABuscar) & "' AND DIARIO=" & Trim(diario) & " AND PERIODO=" & Trim(periodo) & " AND CODIGO_DIVISA='" & Trim(Divisa) & "'", ObjetoGlobal.cn, True,,,,,, trans)
                    If rsSaldos.RecordCount > 0 Then

                        'ObjetoGlobal.cn.Execute("UPDATE SALDOS SET DEBE_PERIODO=" & (rsSaldos!debe_periodo + ImporteDebe) & ", HABER_PERIODO=" & (rsSaldos!haber_periodo + ImporteHaber) & " WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND CODIGO_CUENTA='" & Trim(CuentaABuscar) & "' AND DIARIO=" & Trim(diario) & " AND PERIODO=" & Trim(periodo) & " AND CODIGO_DIVISA='" & Trim(Divisa) & "'")
                        rsSaldos!debe_periodo = rsSaldos!debe_periodo + ImporteDebe
                        rsSaldos!haber_periodo = rsSaldos!haber_periodo + ImporteHaber

                        If CompraVenta And i = ObjetoGlobal.NivelDeCuenta Then
                            'ObjetoGlobal.cn.Execute("UPDATE SALDOS SET SALDO_CV_PERIODO=" & (rsSaldos!saldo_cv_periodo + ImporteDebe - ImporteHaber) & " WHERE EMPRESA='" & Trim(Empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND CODIGO_CUENTA='" & Trim(CuentaABuscar) & "' AND DIARIO=" & Trim(diario) & " AND PERIODO=" & Trim(periodo) & " AND CODIGO_DIVISA='" & Trim(Divisa) & "'")
                            rsSaldos!saldo_cv_periodo = (rsSaldos!saldo_cv_periodo + ImporteDebe - ImporteHaber)
                        End If

                        rsSaldos.Update()
                    Else
                        If CompraVenta And i = ObjetoGlobal.NivelDeCuenta Then
                            rsSaldos.AddNew()
                            rsSaldos!Empresa = Trim(Empresa)
                            rsSaldos!Ejercicio = Trim(Ejercicio)
                            rsSaldos!CODIGO_CUENTA = Trim(CuentaABuscar)
                            rsSaldos!diario = Val(diario)
                            rsSaldos!periodo = Val(periodo)
                            rsSaldos!CODIGO_DIVISA = Trim(Divisa)
                            rsSaldos!DEBE_PERIODO = (ImporteDebe)
                            rsSaldos!HABER_PERIODO = (ImporteHaber)
                            rsSaldos!DIGITOS = NivelAComprobar
                            rsSaldos!CTA_NUMERICA = Val(Strings.Trim(CuentaABuscar))
                            rsSaldos!SALDO_CV_PERIODO = (ImporteDebe - ImporteHaber)
                            'ObjetoGlobal.cn.Execute("INSERT INTO SALDOS (EMPRESA, EJERCICIO, CODIGO_CUENTA, DIARIO, PERIODO, CODIGO_DIVISA, DEBE_PERIODO, HABER_PERIODO, DIGITOS, CTA_NUMERICA," & "SALDO_CV_PERIODO) VALUES('" & Trim(Empresa) & "', '" & Trim(Ejercicio) & "', '" & Trim(CuentaABuscar) & "', " & Val(diario) & ", " & Val(periodo) & ", '" & Trim(Divisa) & "', " & (ImporteDebe) & ", " & (ImporteHaber) & ", " & NivelAComprobar & ", " & Val(Strings.Trim(CuentaABuscar)) & ", " & (ImporteDebe - ImporteHaber) & ")"
                        Else
                            'ObjetoGlobal.cn.Execute("INSERT INTO SALDOS (EMPRESA, EJERCICIO, CODIGO_CUENTA, DIARIO, PERIODO, CODIGO_DIVISA, DEBE_PERIODO, HABER_PERIODO, DIGITOS, CTA_NUMERICA," &
                            '"SALDO_CV_PERIODO) VALUES('" & Trim(Empresa) & "', '" & Trim(Ejercicio) & "', '" & Trim(CuentaABuscar) & "', " & Val(diario) & ", " & Val(periodo) &
                            '", '" & Trim(Divisa) & "', " & (ImporteDebe) & ", " & (ImporteHaber) & ", " & NivelAComprobar &
                            '", " & Val(Trim(CuentaABuscar)) & ", 0)")
                            rsSaldos.AddNew()
                            rsSaldos!Empresa = Trim(Empresa)
                            rsSaldos!Ejercicio = Trim(Ejercicio)
                            rsSaldos!CODIGO_CUENTA = Trim(CuentaABuscar)
                            rsSaldos!diario = Val(diario)
                            rsSaldos!periodo = Val(periodo)
                            rsSaldos!CODIGO_DIVISA = Trim(Divisa)
                            rsSaldos!DEBE_PERIODO = (ImporteDebe)
                            rsSaldos!HABER_PERIODO = (ImporteHaber)
                            rsSaldos!DIGITOS = NivelAComprobar
                            rsSaldos!CTA_NUMERICA = Val(Strings.Trim(CuentaABuscar))
                            rsSaldos!SALDO_CV_PERIODO = 0
                        End If
                        rsSaldos.Update()
                    End If
                    rsSaldos.Close()
                End If
                If Not GrabaMayores Then Exit For
            Next
            Return 0

        Catch ex As Exception
            Return 1000
        End Try
    End Function

    Private Sub AsientoSubvencion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim RsDiario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If RsDiario.Open("SELECT * FROM diarios WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' ORDER BY diario", ObjetoGlobal.cn) Then
            TxtDiario.DataSource = RsDiario.cnDataSet.Tables(0)
            TxtDiario.DisplayMember = "nombre_diario"
            TxtDiario.ValueMember = "diario"
        End If

        If RsPeriodo.Open("SELECT * FROM periodos WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and ejercicio = '" & ObjetoGlobal.EjercicioActual & "'ORDER BY periodo", ObjetoGlobal.cn) Then
            TxtPeriodo.DataSource = RsPeriodo.cnDataSet.Tables(0)
            TxtPeriodo.DisplayMember = "desc_periodo"
            TxtPeriodo.ValueMember = "periodo"
        End If
        TxtEmpresa.Text = ObjetoGlobal.EmpresaActual
        TxtEjercicio.Text = ObjetoGlobal.EjercicioActual

        If Rs.Open("Select * from empresas WHERE empresa = '" & TxtEmpresa.Text & "'", ObjetoGlobal.cn) Then
            lblEmpresa.Text = Rs!razon_social
        End If
        Rs.Close()
        If Rs.Open("Select * from ejercicios_contab WHERE empresa = '" & TxtEmpresa.Text & "' and ejercicio = '" & ObjetoGlobal.EjercicioActual & "'", ObjetoGlobal.cn) Then
            lblEjercicio.Text = Rs!descripcion
        End If
        Rs.Close()

        RsDiario.Close()
        RsPeriodo.Close()

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class


