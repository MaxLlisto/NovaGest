Imports cnRecordset.CnRecordset
Imports System.ComponentModel
Imports System.Data.SqlClient


Public Class Accesospersonal
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim aNoProcesar As New List(Of Integer)
    Dim Dia_inicial_red As Date
    Dim Hora_inicial_red As String
    Dim Dia_Final_red As Date
    Dim Hora_Final_red As String

    'Para tomar de la tabla parámetros (antigua 'basico_reloj')
    Dim red_entrada_1_d As String
    Dim red_entrada_1_h As String
    Dim red_entrada_1 As String
    Dim red_entrada_2_d As String
    Dim red_entrada_2_h As String
    Dim red_entrada_2 As String
    Dim red_entrada_3_d As String
    Dim red_entrada_3_h As String
    Dim red_entrada_3 As String

    Dim red_salida_1_d As String
    Dim red_salida_1_h As String
    Dim red_salida_1 As String
    Dim red_salida_2_d As String
    Dim red_salida_2_h As String
    Dim red_salida_2 As String
    Dim red_salida_3_d As String
    Dim red_salida_3_h As String
    Dim red_salida_3 As String

    Dim strhora_dto_1 As String
    Dim hora_dto_1 As String
    Dim minutos_dto_1 As Integer
    Dim strhora_dto_2 As String
    Dim hora_dto_2 As String
    Dim minutos_dto_2 As Integer

    Dim strentrada_dto_2 As String
    Dim entrada_dto_2 As String

    Dim parada_m_desde_h As String
    Dim parada_t_desde_h As String
    Dim costes_en_empresas As String
    Dim empresas_costes() As String

    Dim horas_jornada As Double
    Dim RedondeoActual As Integer
    Dim cHoraRedondeada As String
    Dim nNumOperario As Long
    Dim EmpresaProceso As String


    Public Sub New()
        Dim aCal(0) As Array
        Dim aCab(0) As String
        Dim rs As New cnRecordset.CnRecordset
        Dim fechadehoy As String

        fechadehoy = Format(DateTime.Today, "dd/MM/yyyy")

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        DiaInicio.Value = DateAdd("d", -1, CDate(fechadehoy))
        Diafinal.Value = CDate(fechadehoy)
        Horainicial.Value = CDate(Format(DiaInicio.Value, "dd/MM/yyyy") & " 03:59:59")
        Horafinal.Value = CDate(Format(Diafinal.Value, "dd/MM/yyyy") & " 04:00:00")

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Dim Msg As MsgBoxResult
        Msg = MsgBox("Cerrar el modulo, ¿Desea salir?", vbYesNo, "Salir del Modulo")
        If Msg = MsgBoxResult.Yes Then
            'Application.ExitThread()
            Close()
        Else
            Exit Sub
        End If
    End Sub


    Private Sub redondeo_entrada(ByVal Hora_inicial As String, ByVal Dia_inicial As Date)
        Dim HH As Integer
        Dim mm As Integer
        Dim ss As Integer

        'HH = CInt(Left(Hora_inicial, 2))
        'mm = CInt(Mid(Hora_inicial, 4, 2))

        HH = CInt(Strings.Left(Format(CDate(Hora_inicial), "HH:mm:ss"), 2))
        mm = CInt(Mid(Format(CDate(Hora_inicial), "HH:mm:ss"), 4, 2))

        ss = 0 'CInt(Right(Hora_inicial, 2))

        Dia_inicial_red = Dia_inicial

        If (red_entrada_1_d <> "") And (red_entrada_1_h <> "") And (red_entrada_1 <> "") And (Valor(red_entrada_1_d) <= Valor(red_entrada_1_h)) And (Valor(red_entrada_1_d) <= mm) And (mm <= Valor(red_entrada_1_h)) Then
            mm = Valor(red_entrada_1)
        End If
        If (red_entrada_2_d <> "") And (red_entrada_2_h <> "") And (red_entrada_2 <> "") And (Valor(red_entrada_2_d) <= Valor(red_entrada_2_h)) And (Valor(red_entrada_2_d) <= mm) And (mm <= Valor(red_entrada_2_h)) Then
            mm = Valor(red_entrada_2)
        End If
        If (red_entrada_3_d <> "") And (red_entrada_3_h <> "") And (red_entrada_3 <> "") And (Valor(red_entrada_3_d) <= Valor(red_entrada_3_h)) And (Valor(red_entrada_3_d) <= mm) And (mm <= Valor(red_entrada_3_h)) Then
            mm = Valor(red_entrada_3)
        End If

        While (mm >= 60)
            mm = mm - 60
            HH = HH + 1
        End While

        While (HH >= 24)
            HH = HH - 24
            'Dia_inicial_red = Dia_inicial_red + 1
            Dia_inicial_red = DateAdd("d", 1, Dia_inicial_red)
        End While

        Hora_inicial_red = Format(TimeSerial(HH, mm, ss), "HH:mm:ss")

    End Sub

    Private Sub redondeo_salida(Hora_final As String, Dia_final As Date)
        Dim HH As Integer
        Dim mm As Integer
        Dim ss As Integer

        HH = Strings.Left(Format(CDate(Hora_final), "HH:mm:ss"), 2)
        mm = Mid(Format(CDate(Hora_final), "HH:mm:ss"), 4, 2)

        ss = 0  'Right(Hora_final, 2)

        Dia_Final_red = Dia_final

        If (red_salida_1_d <> "") And (red_salida_1_h <> "") And (red_salida_1 <> "") And (Valor(red_salida_1_d) <= Valor(red_salida_1_h)) And (Valor(red_salida_1_d) <= mm) And (mm <= Valor(red_salida_1_h)) Then
            mm = Valor(red_salida_1)
        End If
        If (red_salida_2_d <> "") And (red_salida_2_h <> "") And (red_salida_2 <> "") And (Valor(red_salida_2_d) <= Valor(red_salida_2_h)) And (Valor(red_salida_2_d) <= mm) And (mm <= Valor(red_salida_2_h)) Then
            mm = Valor(red_salida_2)
        End If
        If (red_salida_3_d <> "") And (red_salida_3_h <> "") And (red_salida_3 <> "") And (Valor(red_salida_3_d) <= Valor(red_salida_3_h)) And (Valor(red_salida_3_d) <= mm) And (mm <= Valor(red_salida_3_h)) Then
            mm = Valor(red_salida_3)
        End If

        While (mm >= 60)
            mm = mm - 60
            HH = HH + 1
        End While

        While (HH >= 24)
            HH = HH - 24
            'Dia_Final_red = Dia_Final_red + 1
            Dia_Final_red = DateAdd("d", 1, Dia_Final_red)
        End While

        Hora_Final_red = Format(TimeSerial(HH, mm, ss), "HH:mm:ss")

    End Sub

    Private Sub GrabaPar(cEmp, Rs, nCont, cOper, dFec_entrada, cHora_entrada, dFec_Salida, cHora_salida, cPuesto, sRE, sRS)
        Dim rsOper As New cnRecordset.CnRecordset
        Dim sSql As String

        sSql = "SELECT * FROM Operarios_coop WHERE codigo_operario = " & cOper

        rsOper.Open(sSql, ObjetoGlobal.cn)
        cEmp = PonEmpresa(Trim(rsOper!tipo_operario))
        nCont = PonContador(Trim(cEmp))

        Rs.AddNew
        Rs!Empresa = cEmp 'IIf(Trim(rsOper!tipo_operario) = "L14", "22", TxtEmpresa.Text)
        Rs!Contador = nCont
        Rs!cod_operario = cOper
        Rs!fecha_entrada = dFec_entrada
        Rs!Hora_entrada = cHora_entrada
        Rs!fecha_salida = dFec_Salida
        Rs!Hora_salida = cHora_salida

        Rs!redondeo_entrada = sRE
        Rs!redondeo_salida = sRS

        'parada_m_desde_h = TablaParametros("Parada mañana antes de hora", "parada_m_desde_h", "08:30")
        'parada_t_desde_h = TablaParametros("Parada tarde antes de hora", "parada_t_desde_h", "15:30")

        If ("" & rsOper!parada_almuerzo_sn) = "N" Then
            Rs!Parada_1_sn = "N"
        ElseIf Not ParadaManana.Checked Then
            Rs!Parada_1_sn = "N"
        ElseIf ParadaManana.Checked And ("" & rsOper!parada_almuerzo_sn) = "S" Then
            Rs!Parada_1_sn = "S"
            'ElseIf ParadaManana.Checked And ("" & rsOper!parada_almuerzo_sn) <> "N" Then
            '    If DifTime(parada_m_desde_h, cHora_entrada) > 0 Then 'aaa
            '        Rs!Parada_1_sn = "S"
            '    Else
            '        Rs!Parada_1_sn = "N"
            '    End If
        End If

        If Paradatarde.Checked And ("" & rsOper!parada_tarde_sn) = "N" Then
            Rs!parada_2_sn = "N"
        ElseIf Paradatarde.Checked And ("" & rsOper!parada_tarde_sn) <> "N" Then
            Rs!parada_2_sn = "S"
        Else
            Rs!parada_2_sn = "N"
        End If

        Rs!Parada_1_sn = IIf(ParadaManana.Checked, "S", "N")
        Rs!parada_2_sn = IIf(Paradatarde.Checked, "S", "N")
        Rs!puesto = cPuesto

        Rs.Update

        rsOper.Close()

    End Sub

    Private Sub Ajustarhorario(Empresa)
        Dim q1 As Double
        Dim q2 As Double
        Dim Rs As New cnRecordset.CnRecordset
        Dim RsDes As New cnRecordset.CnRecordset
        Dim sSql As String = ""
        Dim horas_dto As Double
        Dim horas_ajustadas As Double
        Dim cHora_salida As String = ""
        Dim FechaSalida As Date
        Dim nTotal As Double
        Dim nDescuadre As Double

        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If nNumOperario > 0 Then
            sSql = sSql + " AND cod_operario=" & nNumOperario
        End If

        sSql = sSql + " ORDER BY cod_operario, fecha_entrada, hora_entrada"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        ' Primer paso, redondeos
        While Not Rs.EOF
            Rs!Dia_inicial_red = Rs!fecha_entrada 'valores iniciales
            Rs!Hora_inicial_red = Rs!Hora_entrada 'valores iniciales
            If Rs!redondeo_entrada = "S" Then
                redondeo_entrada(Rs!Hora_entrada, Rs!fecha_entrada)
                Rs!Dia_inicial_red = Dia_inicial_red 'valores que devuelve redondeo_entrada
                Rs!Hora_inicial_red = Hora_inicial_red 'valores que devuelve redondeo_entrada
                cHoraRedondeada = Hora_inicial_red
            Else
                If Rs!Hora_inicial_red < cHoraRedondeada Then
                    Rs!Hora_inicial_red = cHoraRedondeada
                End If
            End If

            'Datos salida ********************************************
            Rs!Dia_final_red = Rs!fecha_salida 'valores iniciales
            Rs!Hora_final_red = Rs!Hora_salida 'valores iniciales
            If Rs!Hora_final_red < Rs!Hora_inicial_red Then
                Rs!Hora_final_red = Rs!Hora_inicial_red
            End If
            If Rs!redondeo_salida = "S" Then
                redondeo_salida(Rs!Hora_salida, Rs!fecha_salida)
                Rs!Dia_final_red = Dia_Final_red 'valores que devuelve redondeo_salida
                Rs!Hora_final_red = Hora_Final_red 'valores que devuelve redondeo_salida
                If Rs!Hora_inicial_red > Rs!Hora_final_red And Rs!Dia_inicial_red = Rs!Dia_final_red Then
                    Rs!Hora_inicial_red = Rs!Hora_final_red
                End If
            End If

            If (Rs!Dia_final_red < Rs!Dia_inicial_red) Or (Rs!Dia_final_red = Rs!Dia_inicial_red And Rs!Hora_inicial_red >= Rs!Hora_final_red) Then
                Rs!horas = 0
            Else
                q1 = (Rs!Dia_final_red) - (Rs!Dia_inicial_red)
                q2 = ((Hour(CDate(Rs!Hora_final_red)) * 3600) + (Minute(CDate(Rs!Hora_final_red)) * 60) + (Second(CDate(Rs!Hora_final_red))) - ((Hour(CDate(Rs!Hora_inicial_red)) * 3600 + Minute(CDate(Rs!Hora_inicial_red)) * 60 + Second((Rs!Hora_inicial_red)))))
                Rs!horas = Math.Round(((((24.0# * q1) * 3600) + q2) / 3600.0#), 2)
            End If
            Rs.Update()
            Rs.MoveNext()
        End While
        Rs.Close()

        ' Ahora controlaremos las horas de salida
        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If nNumOperario > 0 Then
            sSql = sSql + " AND cod_operario=" & nNumOperario
        End If

        sSql = sSql + " ORDER BY cod_operario, fecha_entrada, hora_entrada DESC"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        While Not Rs.EOF
            If Rs!redondeo_salida = "S" Then
                cHora_salida = Rs!Hora_final_red
                FechaSalida = Rs!fecha_salida
            Else
                If Rs!Hora_final_red > cHora_salida And FechaSalida = Rs!fecha_salida Then
                    Rs!Hora_final_red = cHora_salida
                    If Rs!Hora_inicial_red > Rs!Hora_final_red And Rs!Dia_inicial_red = Rs!Dia_final_red Then
                        Rs!Hora_inicial_red = Rs!Hora_final_red
                    End If
                End If
            End If
            If (Rs!Dia_final_red < Rs!Dia_inicial_red) Or (Rs!Dia_final_red = Rs!Dia_inicial_red And Rs!Hora_inicial_red >= Rs!Hora_final_red) Then
                Rs!horas = 0
            Else
                q1 = (Rs!Dia_final_red) - (Rs!Dia_inicial_red)
                q2 = ((Hour(CDate(Rs!Hora_final_red)) * 3600) + (Minute(CDate(Rs!Hora_final_red)) * 60) + (Second(CDate(Rs!Hora_final_red))) - ((Hour(CDate(Rs!Hora_inicial_red)) * 3600 + Minute(CDate(Rs!Hora_inicial_red)) * 60 + Second((Rs!Hora_inicial_red)))))
                Rs!horas = Math.Round(((((24.0# * q1) * 3600) + q2) / 3600.0#), 2)
            End If
            Rs.Update()
            Rs.MoveNext()
        End While
        Rs.Close()

        ' Segundo paso, horas ajustadas
        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If nNumOperario > 0 Then
            sSql = sSql + " AND cod_operario=" & nNumOperario
        End If

        sSql = sSql + " ORDER BY cod_operario, fecha_entrada, hora_entrada"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        While Not Rs.EOF

            If Rs!redondeo_entrada = "S" Then
                nTotal = 0
            End If

            'Calculo dtos**********************************************
            horas_dto = 0
            horas_ajustadas = 0

            calculo_dto(Rs!cod_operario, (Rs!Dia_inicial_red), (Rs!Dia_final_red), (Rs!Hora_inicial_red), (Rs!Hora_final_red), (Rs!horas), Rs!Parada_1_sn, Rs!parada_2_sn, horas_dto, horas_ajustadas)
            Rs!horas_dto = horas_dto
            Rs!horas_ajustadas = horas_ajustadas

            nTotal = nTotal + Rs!horas_ajustadas

            'Calculo coste*********************************************
            Rs!importe_coste = 0 'Se actualiza en función
            '**********************************************************

            If Rs!redondeo_salida = "S" Then
                nDescuadre = Math.Round(nTotal - Int(nTotal), 2)
                If nDescuadre <> 0 And Math.Abs(nDescuadre) <> 0.5 Then
                    If nDescuadre < 0.25 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas - nDescuadre
                    ElseIf nDescuadre >= 0.26 And nDescuadre <= 0.49 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas + 0.5 - nDescuadre
                    ElseIf nDescuadre >= 0.51 And nDescuadre <= 0.75 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas - (nDescuadre - 0.5)
                    ElseIf nDescuadre >= 0.76 And nDescuadre <= 0.99 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas + (1 - nDescuadre)
                    End If
                End If
            End If

            Rs.Update()
            Rs.MoveNext()
        End While

        ' Ahora se calcula el importe de los costes
        IgualaCostesOperDia(Empresa)
    End Sub



    Private Sub Procesar_Click(sender As Object, e As EventArgs) Handles Procesar.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim sSql As String
        Dim rsPer As New cnRecordset.CnRecordset
        Dim sSqlPer As String
        Dim nContador As Long
        Dim rsPar As New cnRecordset.CnRecordset
        Dim nOper As Long
        Dim dFec_entrada As Date
        Dim cHora_entrada As String = ""
        Dim dFec_Salida As Date
        Dim cHora_salida As String = ""
        Dim nOperario As Long
        Dim cTipoMar As String
        Dim cPuesto As String = ""
        Dim sRE As String = ""
        Dim sRS As String = ""
        Dim bPrimero As Boolean
        Dim rsOper As New cnRecordset.CnRecordset
        Dim aEmpresas() As String
        Dim i As Integer
        Dim ii As Integer
        Dim PrimerDia As Date
        Dim UltimoDia As Date
        Dim dias As Long
        Dim j As Integer

        aEmpresas = Split(empresas.Text, ",")

        If GrabarBiometricos.Checked Then ' Importamos datos de MySql
            If Not ImportarRelojesBiometricos() Then
                MsgBox("Algunos relojes biométricos no se han importado corectamente, se suspende la importación de esos relojes")
            End If
        End If

        If Trim(SoloOperario.Text) <> "" Then
            sSql = "SELECT * FROM Operarios_coop WHERE codigo_operario = " & SoloOperario.Text
            rsOper.Open(sSql, ObjetoGlobal.cn)
            If rsOper.RecordCount = 0 Then
                MsgBox("No existe el operario indicado")
                Return
            End If
            nNumOperario = CLng(Trim(SoloOperario.Text))
        End If

        dias = 1

        If Format(Horainicial.Value, "HH:mm:ss") > "08:30:00" And ParadaManana.Checked Then
            If MsgBox("Está marcada la opción de parada mañana, ¿está correcta?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Return
            End If
        End If

        PrimerDia = Format(CDate(DiaInicio.Value), "dd/MM/yy")
        UltimoDia = Format(CDate(Diafinal.Value), "dd/MM/yy")

        'If ProcesarVariosDias.Checked Then
        '    dias = DateDiff("d", PrimerDia, UltimoDia)
        'End If

        For j = 0 To dias - 1

            DiaInicio.Value = PrimerDia.AddDays(j)
            Diafinal.Value = PrimerDia.AddDays(j + 1)

            For ii = 0 To UBound(aEmpresas)

                EmpresaProceso = aEmpresas(ii)
                AbreParametros()

                If Not VerificarDatos(aEmpresas(ii)) Then
                    Exit Sub
                End If
                If ReescribirFichadas.Checked Then
                    ' Anulamos pares fichadas anteriores
                    BorrarProcesoAnterior(aEmpresas(ii))
                End If

                nContador = 1

                sSqlPer = "SELECT * FROM personal_es WHERE 1=0"
                rsPer.Open(sSqlPer, ObjetoGlobal.cn, True)

                sSql = "SELECT * FROM personal_marcajes WHERE empresa = '" & Trim(aEmpresas(ii)) & "'"
                sSql = sSql + " AND (fecha_marcaje > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_marcaje = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_marcaje >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
                sSql = sSql + " AND (fecha_marcaje < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_marcaje = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_marcaje <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

                If nNumOperario > 0 Then
                    sSql = sSql + " AND cod_operario=" & nNumOperario
                End If

                If Not ReescribirFichadas.Checked Then
                    sSql = sSql + " AND actualizado <> 'S'"
                End If
                sSql = sSql + " ORDER BY cod_operario, fecha_marcaje, hora_marcaje"

                nOperario = 0
                cTipoMar = ""

                Rs.Open(sSql, ObjetoGlobal.cn, True)


                sRE = "N"
                sRS = "N"

                While Not Rs.EOF

                    If Not NoProcesarOperario(Rs!cod_operario) Then
                        If nOperario <> Rs!cod_operario Then
                            nOperario = Rs!cod_operario
                            nOper = Rs!cod_operario
                            dFec_entrada = Rs!fecha_marcaje
                            cHora_entrada = Rs!hora_marcaje
                            cPuesto = Rs!puesto
                            cTipoMar = Trim(UCase(Rs!tipo_marcaje))
                            bPrimero = True
                        Else
                            If cTipoMar = Trim(UCase(Rs!tipo_marcaje)) Then
                                If Trim(UCase(Rs!tipo_marcaje)) = "E" Then
                                    dFec_Salida = Rs!fecha_marcaje
                                    cHora_salida = Rs!hora_marcaje
                                    nContador = nContador + 1

                                    If bPrimero Then
                                        sRE = IIf(AjustesManana.Checked, "S", "N")
                                    End If
                                    GrabaPar(EmpresaProceso, rsPer, nContador, nOper, dFec_entrada, cHora_entrada, dFec_Salida, cHora_salida, cPuesto, sRE, sRS)
                                    sRE = "N"
                                    sRS = "N"
                                    bPrimero = False
                                    dFec_entrada = Rs!fecha_marcaje
                                    cHora_entrada = Rs!hora_marcaje
                                    cTipoMar = Trim(UCase(Rs!tipo_marcaje))
                                    cPuesto = Rs!puesto
                                End If
                            Else
                                If Trim(UCase(Rs!tipo_marcaje)) = "S" Then
                                    dFec_Salida = Rs!fecha_marcaje
                                    cHora_salida = Rs!hora_marcaje
                                    nContador = nContador + 1

                                    If bPrimero Then
                                        sRE = IIf(AjustesManana.Checked, "S", "N")
                                    End If
                                    sRS = IIf(AjustesTarde.Checked, "S", "N")
                                    GrabaPar(EmpresaProceso, rsPer, nContador, nOper, dFec_entrada, cHora_entrada, dFec_Salida, cHora_salida, cPuesto, sRE, sRS)
                                    cTipoMar = Trim(UCase(Rs!tipo_marcaje))
                                    sRE = "N"
                                    sRS = "N"
                                    bPrimero = False

                                ElseIf Trim(UCase(Rs!tipo_marcaje)) = "E" Then
                                    dFec_entrada = Rs!fecha_marcaje
                                    cHora_entrada = Rs!hora_marcaje
                                    cTipoMar = Trim(UCase(Rs!tipo_marcaje))
                                    cPuesto = Rs!puesto
                                    bPrimero = True
                                End If
                            End If
                        End If
                        Rs!actualizado = "S"
                        Rs.Update()
                    End If
                    Rs.MoveNext()
                End While
                Rs.Close()
                rsPer.Close()
                GrabarCostes(aEmpresas(i))
            Next
        Next

        MsgBox("Proceso concluido")


    End Sub

    Private Sub GrabarCostes(Empresa)
        Dim q1 As Double, q2 As Double
        Dim Rs As New cnRecordset.CnRecordset
        Dim RsDes As New cnRecordset.CnRecordset
        Dim sSql As String
        Dim horas_dto As Double
        Dim horas_ajustadas As Double
        Dim cHora_salida As String = ""
        Dim FechaSalida As Date
        Dim nTotal As Double
        Dim nDescuadre As Double


        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If nNumOperario > 0 Then
            sSql = sSql + " AND cod_operario=" & nNumOperario
        End If

        sSql = sSql + " ORDER BY cod_operario, fecha_entrada, hora_entrada"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        ' Primer paso, redondeos

        While Not Rs.EOF

            Rs!Dia_inicial_red = Rs!fecha_entrada 'valores iniciales
            Rs!Hora_inicial_red = Rs!Hora_entrada 'valores iniciales
            If Rs!redondeo_entrada = "S" Then
                redondeo_entrada(Rs!Hora_entrada, Rs!fecha_entrada)
                Rs!Dia_inicial_red = Dia_inicial_red 'valores que devuelve redondeo_entrada
                Rs!Hora_inicial_red = Hora_inicial_red 'valores que devuelve redondeo_entrada
                cHoraRedondeada = Hora_inicial_red
            Else
                If Rs!Hora_inicial_red < cHoraRedondeada Then
                    Rs!Hora_inicial_red = cHoraRedondeada
                End If
            End If

            'Datos salida ********************************************
            Rs!Dia_final_red = Rs!fecha_salida 'valores iniciales
            Rs!Hora_final_red = Rs!Hora_salida 'valores iniciales
            If Rs!Hora_final_red < Rs!Hora_inicial_red Then
                Rs!Hora_final_red = Rs!Hora_inicial_red
            End If
            If Rs!redondeo_salida = "S" Then
                redondeo_salida(Rs!Hora_salida, Rs!fecha_salida)
                Rs!Dia_final_red = Dia_Final_red 'valores que devuelve redondeo_salida
                Rs!Hora_final_red = Hora_Final_red 'valores que devuelve redondeo_salida
                If Rs!Hora_inicial_red > Rs!Hora_final_red And Rs!Dia_inicial_red = Rs!Dia_final_red Then
                    Rs!Hora_inicial_red = Rs!Hora_final_red
                End If
            End If

            If (Rs!Dia_final_red < Rs!Dia_inicial_red) Or (Rs!Dia_final_red = Rs!Dia_inicial_red And Rs!Hora_inicial_red >= Rs!Hora_final_red) Then
                Rs!horas = 0
            Else
                ' q1 = (Rs!Dia_final_red) - (Rs!Dia_inicial_red)
                q1 = DateDiff("d", Rs!Dia_inicial_red, Rs!Dia_final_red)
                q2 = ((Hour(CDate(Rs!Hora_final_red)) * 3600) + (Minute(CDate(Rs!Hora_final_red)) * 60) + (Second(CDate(Rs!Hora_final_red))) - ((Hour(CDate(Rs!Hora_inicial_red)) * 3600 + Minute(CDate(Rs!Hora_inicial_red)) * 60 + Second((Rs!Hora_inicial_red)))))
                Rs!horas = Math.Round(((((24.0# * q1) * 3600) + q2) / 3600.0#), 2)
            End If
            Rs.Update()
            Rs.MoveNext()
        End While
        Rs.Close()

        ' Ahora controlaremos las horas de salida

        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If nNumOperario > 0 Then
            sSql = sSql + " AND cod_operario=" & nNumOperario
        End If

        sSql = sSql + " ORDER BY cod_operario, fecha_entrada, hora_entrada DESC"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        While Not Rs.EOF
            If Rs!redondeo_salida = "S" Then
                cHora_salida = Rs!Hora_final_red
                FechaSalida = Rs!fecha_salida
            Else
                If Rs!Hora_final_red > cHora_salida And FechaSalida = Rs!fecha_salida Then
                    Rs!Hora_final_red = cHora_salida
                    If Rs!Hora_inicial_red > Rs!Hora_final_red And Rs!Dia_inicial_red = Rs!Dia_final_red Then
                        Rs!Hora_inicial_red = Rs!Hora_final_red
                    End If
                End If
            End If
            If (Rs!Dia_final_red < Rs!Dia_inicial_red) Or (Rs!Dia_final_red = Rs!Dia_inicial_red And Rs!Hora_inicial_red >= Rs!Hora_final_red) Then
                Rs!horas = 0
            Else
                'q1 = (Rs!Dia_final_red) - (Rs!Dia_inicial_red)
                q1 = DateDiff("d", Rs!Dia_inicial_red, Rs!Dia_final_red)
                q2 = ((Hour(CDate(Rs!Hora_final_red)) * 3600) + (Minute(CDate(Rs!Hora_final_red)) * 60) + (Second(CDate(Rs!Hora_final_red))) - ((Hour(CDate(Rs!Hora_inicial_red)) * 3600 + Minute(CDate(Rs!Hora_inicial_red)) * 60 + Second((Rs!Hora_inicial_red)))))
                Rs!horas = Math.Round(((((24.0# * q1) * 3600) + q2) / 3600.0#), 2)
            End If
            Rs.Update()
            Rs.MoveNext()
        End While
        Rs.Close()

        ' Segundo paso, horas ajustadas
        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If nNumOperario > 0 Then
            sSql = sSql + " AND cod_operario=" & nNumOperario
        End If

        sSql = sSql + " ORDER BY cod_operario, fecha_entrada, hora_entrada"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        While Not Rs.EOF

            If Rs!redondeo_entrada = "S" Then
                nTotal = 0
            End If

            'Calculo dtos**********************************************
            horas_dto = 0
            horas_ajustadas = 0
            calculo_dto(Rs!cod_operario, Rs!Dia_inicial_red, Rs!Dia_final_red, Rs!Hora_inicial_red, Rs!Hora_final_red, Rs!horas, Rs!Parada_1_sn, Rs!parada_2_sn, horas_dto, horas_ajustadas)
            Rs!horas_dto = horas_dto
            Rs!horas_ajustadas = horas_ajustadas

            nTotal = nTotal + Rs!horas_ajustadas

            'Calculo coste*********************************************
            Rs!importe_coste = 0 'Se actualiza en función
            '**********************************************************

            If Rs!redondeo_salida = "S" Then
                nDescuadre = Math.Round(nTotal - Int(nTotal), 2)
                If nDescuadre <> 0 And Math.Abs(nDescuadre) <> 0.5 Then
                    If nDescuadre < 0.25 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas - nDescuadre
                    ElseIf nDescuadre >= 0.26 And nDescuadre <= 0.49 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas + 0.5 - nDescuadre
                    ElseIf nDescuadre >= 0.51 And nDescuadre <= 0.75 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas - (nDescuadre - 0.5)
                    ElseIf nDescuadre >= 0.76 And nDescuadre <= 0.99 Then
                        Rs!horas_ajustadas = Rs!horas_ajustadas + (1 - nDescuadre)
                    End If
                End If
            End If

            Rs.Update()
            Rs.MoveNext()
        End While

        ' Ahora se calcula el importe de los costes
        IgualaCostesOperDia(Empresa)


    End Sub


    Private Function ObtenFecha(Numero As Long) As Date
        ObtenFecha = DateAdd("s", Numero, "01/01/1970 00:00:00")
    End Function

    Private Function deFechaaNumero(Fecha As Date) As Long
        deFechaaNumero = DateDiff("s", "01/01/1970", Fecha)
    End Function

    Private Function PonContador(cEmpresa) As Long
        Dim sSqlPer As String
        Dim rsPer As New cnRecordset.CnRecordset
        Dim nContador As Long

        sSqlPer = "SELECT max(contador) as cont FROM personal_es WHERE Empresa = '" & Trim(cEmpresa) & "'"
        rsPer.Open(sSqlPer, ObjetoGlobal.cn)
        If rsPer.RecordCount > 0 Then
            If Not (DBNull.Value.Equals(rsPer!cont)) Then
                nContador = rsPer!cont + 1
            Else
                nContador = 1
            End If
        Else
            nContador = 1
        End If
        rsPer.Close()
        Return nContador

    End Function

    Private Function VerificarDatos(num_empresa) As Boolean
        Dim Rs As New cnRecordset.CnRecordset
        Dim sSql As String
        Dim cTipoMar As String
        Dim nOperario As Long
        Dim bError As Boolean = False
        Dim sMensajesError As String
        Dim oFrm As New FrmErroresMarcajes
        Dim lHayAlgunRegistros As Boolean
        Dim cOperacion As String = ""
        Dim cNomOperario As String = ""
        Dim cPuesto As String = ""
        Dim cHoraInicial As String = ""
        Dim cHoraFinal As String = ""

        VerificarDatos = True

        sSql = "SELECT p.*, o.nombre_operario, o.situacion, o.fecha_alta_2, o.fecha_alta_3 FROM personal_marcajes p JOIN operarios_coop o ON codigo_operario = p.cod_operario WHERE p.empresa = '" & Trim(num_empresa) & "'"
        sSql = sSql + " AND (p.fecha_marcaje > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (p.fecha_marcaje = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND p.hora_marcaje >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (p.fecha_marcaje < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (p.fecha_marcaje = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND p.hora_marcaje <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "
        If SoloOperario.Text.Trim <> "" Then
            sSql = sSql + " AND p.cod_operario=" & SoloOperario.Text
        End If

        If Not ReescribirFichadas.Checked Then
            sSql = sSql + " AND p.actualizado <> 'S'"
        End If
        sSql = sSql + " ORDER BY p.cod_operario, p.fecha_marcaje, p.hora_marcaje"

        nOperario = 0
        cTipoMar = ""
        sMensajesError = ""

        aNoProcesar.Clear()

        Rs.Open(sSql, ObjetoGlobal.cn)


        If Rs.RecordCount = 0 Then
            MsgBox("No hay registros que procesar")
            VerificarDatos = False
            Rs.Close()
            Return False
        End If

        lHayAlgunRegistros = IIf(Rs.RecordCount > 0, True, False)

        Try

            While Not Rs.EOF

                If Trim(Rs!situacion) <> "AC" Then
                    If nOperario <> Trim(UCase(Rs!cod_operario)) Then
                        sMensajesError = sMensajesError + " Error: " & Rs!Nombre_Operario & " No se encuentra activo " & Chr(13) + Chr(10) ' ->" & cOperacion + Chr(13) + Chr(10)
                        AddNoProcesar(Rs!cod_operario)
                        nOperario = Rs!cod_operario
                    End If
                Else
                    If Rs!cod_operario <> 0 And Rs!tipo_marcaje = "E" And IsDBNull(Rs!puesto) Then
                        sMensajesError = sMensajesError + " Error: " & Rs!cod_operario & " falta puesto de entrada " + Chr(13) + Chr(10)
                        AddNoProcesar(Rs!cod_operario)
                        VerificarDatos = False
                    End If
                    If nOperario <> Trim(UCase(Rs!cod_operario)) Then
                        If nOperario <> 0 And (cTipoMar = "E" Or Trim(cTipoMar) = "") Then
                            ' Error, falta la salida del operario nOperario
                            sMensajesError = sMensajesError + " Error: " & cNomOperario & " falta salida ->" & cOperacion + Chr(13) + Chr(10)
                            AddNoProcesar(nOperario)
                            VerificarDatos = False
                        End If
                        'If 
                        nOperario = Rs!cod_operario
                        cNomOperario = Trim(nOperario & " " & Strings.Left(Rs!nombre_operario, 30))
                        cPuesto = "" & Rs!puesto
                        cHoraInicial = Format(CDate(Rs!hora_marcaje), "HH:mm:ss")
                        If Trim(UCase(Rs!tipo_marcaje)) <> "E" Then
                            cOperacion = "Marca: " & Trim(UCase(Rs!tipo_marcaje)) & " Op:" & nOperario & " fecha " & Format(Rs!fecha_marcaje, "dd/MM/yyyy") & " Hora " & Format(CDate(Rs!hora_marcaje), "HH:mm:ss")
                            sMensajesError = sMensajesError + " Error: " & cNomOperario & " falta entrada ->" & cOperacion + Chr(13) + Chr(10)
                            AddNoProcesar(nOperario)
                            VerificarDatos = False
                        End If
                        cTipoMar = Trim(UCase(Rs!tipo_marcaje))
                        cOperacion = "Marca: " & cTipoMar & " Op:" & nOperario & " fecha " & Format(Rs!fecha_marcaje, "dd/MM/yyyy") & " Hora " & Rs!hora_marcaje
                    Else
                        If cTipoMar = Trim(UCase(Rs!tipo_marcaje)) Then
                            If cTipoMar = "S" Then
                                ' Error, falta la entrada del operario nOperario
                                sMensajesError = sMensajesError & " Error: " & Strings.Trim(Rs!cod_operario) & " " & Strings.Left(Rs!nombre_operario, 30) & " faltan entradas ->" & cOperacion & Chr(13) & Chr(10)
                                AddNoProcesar(nOperario)
                                VerificarDatos = False
                            ElseIf Trim(cPuesto) = "" Then
                                sMensajesError = sMensajesError + " Error: " & cNomOperario & " Falta puesto ->" & cOperacion + Chr(13) + Chr(10)
                                AddNoProcesar(nOperario)
                                VerificarDatos = False
                                cHoraInicial = Format(CDate(Rs!hora_marcaje), "HH:mm:ss")
                                'ElseIf Trim(cPuesto) = Trim(Rs!puesto) Then
                                '    sMensajesError = sMensajesError + " Error: " & cNomOperario & " Falta salida o la entrada está duplicada ->" & cOperacion + Chr(13) + Chr(10)
                                '    AddNoProcesar(nOperario)
                                '    VerificarDatos = False
                                '    cHoraInicial = Rs!hora_marcaje
                            Else
                                If Trim(cHoraInicial) <> "" Then
                                    If AlertaPorMargenHorario(cHoraInicial, Format(CDate(Rs!hora_marcaje), "HH:mm:ss")) Then
                                        sMensajesError = sMensajesError + " ALERTA: " & cNomOperario & " Comprobar hora de entrada y salida: " & "Entra: " + cHoraInicial + " sale " & Rs!hora_marcaje + Chr(13) + Chr(10)
                                        VerificarDatos = False
                                    End If
                                End If
                                If Trim(Rs!tipo_marcaje) = "E" Then
                                    cHoraInicial = Format(CDate(Rs!hora_marcaje), "HH:mm:ss")
                                Else
                                    cHoraInicial = ""
                                End If
                            End If
                        Else
                            If Trim(cHoraInicial) <> "" Then
                                If AlertaPorMargenHorario(cHoraInicial, Format(CDate(Rs!hora_marcaje), "HH:mm:ss")) Then
                                    sMensajesError = sMensajesError + " ALERTA: " & cNomOperario & " Comprobar hora de entrada y salida: " & "Entra: " + cHoraInicial + " sale " & Rs!hora_marcaje + Chr(13) + Chr(10)
                                    VerificarDatos = False
                                End If
                            End If
                            If Trim(Rs!tipo_marcaje) = "E" Then
                                cHoraInicial = Format(CDate(Rs!hora_marcaje), "HH:mm:ss")
                            Else
                                cHoraInicial = ""
                            End If
                        End If
                        cPuesto = "" & Rs!puesto
                        cTipoMar = Trim(UCase(Rs!tipo_marcaje))
                        cOperacion = "Marca: " & cTipoMar & " Op: " & nOperario & " fecha " & Format(Rs!fecha_marcaje, "dd/MM/yyyy") & " Hora " & Format(CDate(Rs!hora_marcaje), "HH:mm:ss")
                    End If
                End If
                Rs.MoveNext()
            End While
        Catch ex As Exception
            MsgBox("Se ha producido un error no controlado en algún dato de los operarios " & nOperario & " o " & Trim(UCase("" & Rs!cod_operario)))
            Return False
        End Try

        If nOperario <> 0 And (cTipoMar = "E" Or Trim(cTipoMar) = "") Then
            ' Error, falta la salida del operario nOperario
            sMensajesError = sMensajesError + " Error: " & cNomOperario & " falta entrada " + Chr(13) + Chr(10)
            AddNoProcesar(nOperario)
            VerificarDatos = False
        Else
            ''**************** ver ***************
            'If AlertaPorMargenHorario(cHoraInicial, hora_ultimo_marcaje) Then
            '    sMensajesError = sMensajesError + " ALERTA: " & cNomOperario & " Comprobar hora de entrada y salida: " & "Entra: " + cHoraInicial + " sale " & hora_ultimo_marcaje + Chr(13) + Chr(10)
            'End If
            ''**************** hasta aquí ***************
        End If
        Rs.Close()

        If Not lHayAlgunRegistros Then
            MsgBox("No hay registros que procesar")
            VerificarDatos = False
            Return False
        End If

        If Not VerificarDatos Then
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.FechaInicial = DiaInicio.Value
            oFrm.HoraInicial = Format(CDate(Horainicial.Value), "HH:mm:ss")
            oFrm.Fechafinal = Diafinal.Value
            oFrm.HoraFinal = Format(CDate(Horafinal.Value), "HH:mm:ss")
            oFrm.Mensaje(sMensajesError)
            oFrm.ShowDialog()
            If oFrm.Resultado = "P" Then
                VerificarDatos = True
                Return True
            Else
                VerificarDatos = False
                MsgBox("Solucione los errores y vuelva a intentarlo")
                Return False
            End If
        Else
            Return True
        End If

    End Function

    Private Function AlertaPorMargenHorario(cHoraInicial, cHoraFinal) As Boolean
        Dim chh1 As String
        Dim cmm1 As String
        Dim chh2 As String
        Dim cmm2 As String

        chh1 = Strings.Left(cHoraInicial, 2)
        cmm1 = Mid(cHoraInicial, 4, 2)

        chh2 = Strings.Left(cHoraFinal, 2)
        cmm2 = Mid(cHoraFinal, 4, 2)

        If chh1 <= "12" Or (chh1 = "13" And cmm1 <= "40") Then
            If chh2 >= "15" Or (chh2 = "14" And cmm2 >= "40") Then
                Return True
            End If
        End If
        If Val(chh2) - Val(chh1) >= 8 Then
            If Val(chh2) > 4 Then
                Return True
            End If
        End If
        Return False

    End Function
    Private Sub AddNoProcesar(ByVal nUsu As Integer)
        Dim lEstaYa As Boolean

        For Each operario As Integer In aNoProcesar
            If operario = nUsu Then
                lEstaYa = True
            End If
        Next
        If Not lEstaYa Then
            aNoProcesar.Add(nUsu)
        End If

    End Sub

    Private Function NoProcesarOperario(ByVal nUsu As Integer) As Boolean

        For Each operario As Integer In aNoProcesar
            If operario = nUsu Then
                Return True
            End If
        Next
        Return False

    End Function

    Private Function ImportarRelojesBiometricos() As Boolean
        Dim cc As SqlConnection
        Dim CadenaConexion As String
        Dim SQL As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim rsPer As New cnRecordset.CnRecordset
        Dim rsCont As New cnRecordset.CnRecordset
        Dim Desde_Segundos As Long
        Dim Hasta_Segundos As Long
        Dim fecha_hora As String
        Dim UltimoRegistroLeido As Long
        Dim Empresa As String
        Dim rsOper As New cnRecordset.CnRecordset
        Dim tipo_operario As String

        fecha_hora = Format(CDate(DiaInicio.Value.ToString("dd/MM/yyyy") & " " & Horainicial.Value.ToString("HH:mm:ss")), "dd/MM/yyyy HH:mm:ss")
        Desde_Segundos = deFechaaNumero(CDate(fecha_hora))

        fecha_hora = Format(CDate(Diafinal.Value.ToString("dd/MM/yyyy") & " " & Horafinal.Value.ToString("HH:mm:ss")), "dd/MM/yyyy HH:mm:ss")
        Hasta_Segundos = deFechaaNumero(CDate(fecha_hora))

        rsCont.Open("SELECT * FROM contadores_pend WHERE ejercicio = 'T' AND nombre = 'ultimo_reg_importado' AND serie = 'T'", ObjetoGlobal.cn, True)
        If rsCont.EOF Then
            rsCont.AddNew()
            rsCont!Empresa = ""
            rsCont!Ejercicio = "T"
            rsCont!Nombre = "ultimo_reg_importado"
            rsCont!Serie = "T"
            rsCont!Contador = 1
        End If

        SQL = "SELECT * FROM tb_event_log WHERE nEventIdn = 58 and nTNAEvent <= 1 AND nEventLogIdn > " & rsCont!Contador & " order by nEventLogIdn "

        Try
            CadenaConexion = "Data Source=Srvv01;Initial Catalog=biostar;Persist Security Info=True;User ID=sa;Password=horto"
            cc = New Data.SqlClient.SqlConnection(CadenaConexion)
            cc.Open()
        Catch e As Exception
            MsgBox("Error abriendo la conexión")
            Return False
        End Try

        Rs.Open(SQL, cc)

        SQL = "SELECT * FROM personal_marcajes WHERE 1=0"
        rsPer.Open(SQL, ObjetoGlobal.cn, True)

        While Not Rs.EOF
            Empresa = "1"
            If ObtenFecha(Rs!nDateTime) >= CDate("13/05/2019 00:00:01") Then
                rsPer.AddNew()

                If Rs!nUserId >= 20000 Then
                    rsPer!cod_operario = Rs!nUserId
                Else
                    rsPer!cod_operario = (8000 + Rs!nUserId)
                End If
                tipo_operario = ""
                rsOper.Open("SELECT * FROM Operarios_coop WHERE codigo_operario = " & rsPer!cod_operario, ObjetoGlobal.cn)
                If rsOper.RecordCount = 0 Then
                    MsgBox("No existe el operario indicado: " & rsPer!cod_operario)
                    Return False
                End If
                tipo_operario = Trim(rsOper!tipo_operario)
                rsOper.Close()
                rsPer!Empresa = "1" 'IIf(tipo_operario = "L14", "1", Empresa)
                fecha_hora = ObtenFecha(Rs!nDateTime).ToString("dd/MM/yyyy HH:mm:ss")
                'fecha_hora = Format(fecha_hora, "dd/MM/yyyy HH:mm:ss")
                rsPer!fecha_marcaje = Strings.Left(fecha_hora, 10)
                rsPer!hora_marcaje = Mid(fecha_hora, 12)
                rsPer!tipo_marcaje = IIf(Rs!nTNAEvent = 0, "E", "S")
                rsPer!puesto = CStr(Rs!nReaderIdn)
                rsPer!actualizado = "N"
                rsPer!Terminal = Rs!nReaderIdn
                rsPer.Update()
            End If
            UltimoRegistroLeido = Rs!nEventLogIdn
            rsCont!Contador = UltimoRegistroLeido
            rsCont.Update()
            Rs.MoveNext()
        End While
        rsCont.Close()
        Return True

    End Function

    Private Sub IgualaCostesOperDia(Empresa)
        Dim precio_hora_extra As Double
        Dim precio_hora_normal As Double
        Dim rsOperarios As New cnRecordset.CnRecordset
        Dim rsTiposOper As New cnRecordset.CnRecordset
        Dim Rs As New cnRecordset.CnRecordset
        Dim nHorasJornada As Double
        Dim sSql As String
        Dim nHora_normal As Double
        Dim nHoraExtra As Double
        Dim nOperario As Long = 0

        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Value), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(Horainicial.Value, "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Value), "dd/MM/yy") & "' AND hora_salida <= '" & Format(Horafinal.Value, "HH:mm:ss") & "')) "

        If Trim(SoloOperario.Text) <> "" Then
            sSql = sSql + " AND cod_operario=" & SoloOperario.Text
        End If

        sSql = sSql + " ORDER BY Cod_operario, fecha_entrada, hora_entrada"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        nHorasJornada = 0

        While Not Rs.EOF

            nHora_normal = 0
            nHoraExtra = 0
            If nOperario <> Rs!cod_operario Then
                nOperario = Rs!cod_operario
                nHorasJornada = 0
                rsOperarios.Open("SELECT tipo_operario FROM Operarios_coop WHERE Codigo_operario=" & Rs!cod_operario, ObjetoGlobal.cn)
                If rsOperarios.RecordCount > 0 Then
                    rsTiposOper.Open("SELECT Precio_hora_normal,Precio_hora_extra FROM Tipos_operarios WHERE TIPO_OPERARIO='" & Trim(rsOperarios!tipo_operario) & "'", ObjetoGlobal.cn)
                    If rsTiposOper.RecordCount > 0 Then
                        precio_hora_extra = Valor(rsTiposOper!precio_hora_extra)
                        precio_hora_normal = Valor(rsTiposOper!precio_hora_normal)
                    Else
                        MsgBox("No se ha encontrado el operario con código " & Rs!cod_operario & ". No se calculará su coste.")
                        nOperario = 0
                    End If
                    rsTiposOper.Close()
                Else
                    MsgBox("No se ha encontrado el operario con código " & Rs!cod_operario & ". No se calculará su coste.")
                    nOperario = 0
                End If
                rsOperarios.Close()
            End If
            If nOperario > 0 Then
                If nHorasJornada >= horas_jornada Then
                    nHoraExtra = Rs!horas_ajustadas
                ElseIf (nHorasJornada + Rs!horas_ajustadas) >= horas_jornada Then
                    nHoraExtra = (nHorasJornada + Rs!horas_ajustadas) - horas_jornada
                    nHora_normal = horas_jornada - nHorasJornada
                Else
                    nHora_normal = Rs!horas_ajustadas
                End If
                nHorasJornada = nHorasJornada + Rs!horas_ajustadas
                Rs!importe_coste = Math.Round((nHora_normal * precio_hora_normal) + (nHoraExtra * precio_hora_extra), 2)
                Rs.Update()
            End If
            Rs.MoveNext()
        End While
        Rs.Close()

    End Sub

    Private Sub calculo_dto(ByVal operario As Long, ByVal dia_inicial_r As Date, ByVal dia_final_r As Date, ByVal hora_inicial_r As String, ByVal hora_final_r As String, horas As Double, ByVal sDto_1 As String, ByVal sDto_2 As String, ByRef horas_dto As Double, ByRef horas_ajustadas As Double)
        Dim dt As Double
        Dim j1 As Integer
        Dim jj As Integer

        dt = 0
        If dia_inicial_r = dia_final_r Then
            dt = calculo_dto2(operario, dia_inicial_r, hora_inicial_r, hora_final_r, sDto_1, sDto_2)
        Else
            'j1 = dia_final_r - dia_inicial_r + 1
            j1 = DateDiff("d", dia_inicial_r, dia_final_r) + 1

            For jj = 1 To j1
                If jj = 1 Then
                    dt = dt + calculo_dto2(operario, dia_inicial_r, hora_inicial_r, "23:59:00", sDto_1, sDto_2)
                ElseIf jj = j1 Then
                    dt = dt + calculo_dto2(operario, dia_final_r, "00:00:00", hora_final_r, sDto_1, sDto_2)
                Else
                    dt = dt + calculo_dto2(operario, DateAdd("d", jj - 1, dia_inicial_r), "00:00:00", "23:29:00", sDto_1, sDto_2)
                End If
            Next jj
        End If
        horas_dto = dt
        horas_ajustadas = horas - horas_dto

    End Sub

    Private Function calculo_dto2(ByVal operario As Long, ByVal dia_inicial_r As Date, ByVal hora_inicial_r As String, ByVal hora_final_r As String, ByVal sDto_1 As String, ByVal sDto_2 As String) As Double
        Dim h1 As Date 'Hora de comienzo de la jornada
        Dim h2 As Date 'Hora de finalización de la jornada
        Dim h3 As Date 'Aux.Hora comienza descanso (o tiempo de descuento)
        Dim h4 As Date 'Aux.Hora finaliza descanso (o tiempo de descuento)
        Dim th As Double
        Dim t1 As Double
        Dim rsQDtos As New cnRecordset.CnRecordset
        Dim horaauxH As Integer
        Dim minutosaux As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs2 As New cnRecordset.CnRecordset
        Dim SQL As String
        Dim FlagDescuento2 As Boolean

        calculo_dto2 = 0
        th = 0
        h1 = CDate(hora_inicial_r)
        h2 = CDate(hora_final_r)

        'Intervalo de dto. 1
        If strhora_dto_1 <> "" And sDto_1 = "S" Then ' Si hay hora de descuento y el operario descansa
            h3 = CDate(hora_dto_1)
            If (Hour(h3) * 3600 + Minute(h3) * 60 + Second(h3)) + (minutos_dto_1 * 60) >= 86400 Then '@@
                minutos_dto_1 = (86399 - (Hour(h3) * 3600 + Minute(h3) * 60 + Second(h3))) / 60
                h4 = TimeSerial(23, 59, 59)
            Else
                minutosaux = minutos_dto_1
                If (minutosaux / 60) >= 1 Then
                    horaauxH = Int(minutosaux / 60)
                    minutosaux = minutosaux - (60 * horaauxH)
                Else
                    horaauxH = 0
                End If
                minutosaux = Int(minutosaux)
                'h4 = CDate(hora_dto_1) + TimeSerial(horaauxH, minutosaux, 0)
                h4 = SumarHoras(hora_dto_1, TimeSerial(horaauxH, minutosaux, 0).ToString)
            End If

            If h3 <= h1 And h4 >= h1 And h4 <= h2 Then
                t1 = DateDiff("s", h1, h4) ''Hour(h4 - h1) * 3600 + Minute(h4 - h1) * 60 + Second(h4 - h1)
                th = th + (t1 / 3600.0#)
            ElseIf h3 <= h1 And h4 >= h2 Then
                t1 = DateDiff("s", h1, h2) 'Hour(h2 - h1) * 3600 + Minute(h2 - h1) * 60 + Second(h2 - h1)
                th = th + (t1 / 3600.0#)
            ElseIf h3 >= h1 And h3 <= h2 And h4 <= h2 Then
                t1 = DateDiff("s", h3, h4) 'Hour(h4 - h3) * 3600 + Minute(h4 - h3) * 60 + Second(h4 - h3)
                th = th + (t1 / 3600.0#)
            ElseIf h3 >= h1 And h3 <= h2 And h4 >= h2 Then
                t1 = DateDiff("s", h3, h2) 'Hour(h2 - h3) * 3600 + Minute(h2 - h3) * 60 + Second(h2 - h3)
                th = th + (t1 / 3600.0#)
            End If 'el resto de casos no descontaría horas (tiempo de dto.fuera de horario del trabajo del operario)
        End If

        'Intervalo de dto. 2
        FlagDescuento2 = False
        If strhora_dto_2 <> "" And sDto_2 = "S" Then  ' Si hay hora de descuento tarde y el operario descansa
            If entrada_dto_2 = "" Then
                FlagDescuento2 = True
            Else
                If hora_inicial_r = entrada_dto_2 Then
                    FlagDescuento2 = True
                Else
                    SQL = "SELECT * FROM personal_es WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND cod_operario = " + CStr(operario) + " AND dia_inicial_red = '" + Format(CDate(dia_inicial_r), "dd/MM/yyyy") + "' and hora_inicial_red = '" & Format(CDate(entrada_dto_2), "HH:mm:ss") + "'"
                    Rs2.Open(SQL, ObjetoGlobal.cn)
                    If Rs2.RecordCount > 0 Then
                        FlagDescuento2 = True
                    End If
                    Rs2.Close()
                End If
            End If
        End If
        If FlagDescuento2 = True Then
            h3 = CDate(hora_dto_2)
            If (Hour(h3) * 3600 + Minute(h3) * 60 + Second(h3)) + (minutos_dto_2 * 60) >= 86400 Then
                minutos_dto_2 = (86399 - (Hour(h3) * 3600 + Minute(h3) * 60 + Second(h3))) / 60
                h4 = TimeSerial(23, 59, 59)
            Else
                minutosaux = minutos_dto_2
                If (minutosaux / 60) >= 1 Then
                    horaauxH = Int(minutosaux / 60)
                    minutosaux = minutosaux - (60 * horaauxH)
                Else
                    horaauxH = 0
                End If
                minutosaux = Int(minutosaux)
                h4 = CDate(hora_dto_2) + TimeSerial(horaauxH, minutosaux, 0)
            End If

            If h3 <= h1 And h4 >= h1 And h4 <= h2 Then
                t1 = DateDiff("s", h1, h4) 'Hour(h4 - h1) * 3600 + Minute(h4 - h1) * 60 + Second(h4 - h1)
                th = th + (t1 / 3600.0#)
            ElseIf h3 <= h1 And h4 >= h2 Then
                t1 = DateDiff("s", h1, h2) 'Hour(h2 - h1) * 3600 + Minute(h2 - h1) * 60 + Second(h2 - h1)
                th = th + (t1 / 3600.0#)
            ElseIf h3 >= h1 And h3 <= h2 And h4 <= h2 Then
                t1 = DateDiff("s", h3, h4) 'Hour(h4 - h3) * 3600 + Minute(h4 - h3) * 60 + Second(h4 - h3)
                th = th + (t1 / 3600.0#)
            ElseIf h3 >= h1 And h3 <= h2 And h4 >= h2 Then
                t1 = DateDiff("s", h3, h2) 'Hour(h2 - h3) * 3600 + Minute(h2 - h3) * 60 + Second(h2 - h3)
                th = th + (t1 / 3600.0#)
            End If 'el resto de casos no descontaría horas (tiempo de dto.fuera de horario del trabajo del operario)
        End If


        calculo_dto2 = th

    End Function
    Private Sub BorrarProcesoAnterior(Empresa)
        Dim Rs As New cnRecordset.CnRecordset
        Dim sSql As String


        sSql = "SELECT * FROM personal_es WHERE empresa = '" & Trim(Empresa) & "'"
        sSql = sSql + " AND (fecha_entrada > '" & Format(CDate(DiaInicio.Text), "dd/MM/yy") & "' OR (fecha_entrada = '" & Format(CDate(DiaInicio.Text), "dd/MM/yy") & "' AND hora_entrada >= '" & Format(CDate(Horainicial.Text), "HH:mm:ss") & "')) "
        sSql = sSql + " AND (fecha_salida < '" & Format(CDate(Diafinal.Text), "dd/MM/yy") & "' OR (fecha_salida = '" & Format(CDate(Diafinal.Text), "dd/MM/yy") & "' AND hora_salida <= '" & Format(CDate(Horafinal.Text), "HH:mm:ss") & "')) "

        If SoloOperario.Text.Trim <> "" Then
            sSql = sSql + " AND cod_operario=" & SoloOperario.Text
        End If

        Rs.Open(sSql, ObjetoGlobal.cn, True)

        While Rs.RecordCount > 0
            Rs.MoveFirst()
            Rs.Delete()
        End While

        Return

    End Sub

    Function TablaParametros(Proceso As String, Campo As String, Optional valor_defecto As String = "")
        Dim Rs As New cnRecordset.CnRecordset

        TablaParametros = valor_defecto
        Rs.Open("SELECT VALOR_DEFECTO FROM TABLA_PARAMETROS WHERE EMPRESA='" & EmpresaProceso & "' AND PROCESO='" & Proceso & "' AND CAMPO='" & Campo & "'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then TablaParametros = "" & Trim(Rs!valor_defecto)

        Return TablaParametros


    End Function

    Private Sub AbreParametros()

        horas_jornada = TablaParametros("Grabacion costes", "horas_jornada", 8)
        red_entrada_1_d = TablaParametros("Grabacion costes", "red_entrada_1_d", "")
        red_entrada_1_h = TablaParametros("Grabacion costes", "red_entrada_1_h", "")
        red_entrada_1 = TablaParametros("Grabacion costes", "red_entrada_1", "")
        red_entrada_2_d = TablaParametros("Grabacion costes", "red_entrada_2_d", "")
        red_entrada_2_h = TablaParametros("Grabacion costes", "red_entrada_2_h", "")
        red_entrada_2 = TablaParametros("Grabacion costes", "red_entrada_2", "")
        red_entrada_3_d = TablaParametros("Grabacion costes", "red_entrada_3_d", "")
        red_entrada_3_h = TablaParametros("Grabacion costes", "red_entrada_3_h", "")
        red_entrada_3 = TablaParametros("Grabacion costes", "red_entrada_3", "")

        red_salida_1_d = TablaParametros("Grabacion costes", "red_salida_1_d", "")
        red_salida_1_h = TablaParametros("Grabacion costes", "red_salida_1_h", "")
        red_salida_1 = TablaParametros("Grabacion costes", "red_salida_1", "")
        red_salida_2_d = TablaParametros("Grabacion costes", "red_salida_2_d", "")
        red_salida_2_h = TablaParametros("Grabacion costes", "red_salida_2_h", "")
        red_salida_2 = TablaParametros("Grabacion costes", "red_salida_2", "")
        red_salida_3_d = TablaParametros("Grabacion costes", "red_salida_3_d", "")
        red_salida_3_h = TablaParametros("Grabacion costes", "red_salida_3_h", "")
        red_salida_3 = TablaParametros("Grabacion costes", "red_salida_3", "")
        parada_m_desde_h = TablaParametros("Parada mañana antes de hora", "parada_m_desde_h", "08:30")
        parada_t_desde_h = TablaParametros("Parada tarde antes de hora", "parada_t_desde_h", "15:30")
        costes_en_empresas = TablaParametros("Calcular costes en empresas", "costes_en_empresas", "1")

        empresas_costes = Split(costes_en_empresas, ",")

        strhora_dto_1 = TablaParametros("Grabacion costes", "hora_dto_1", "")
        If strhora_dto_1 <> "" Then hora_dto_1 = Format(CDate(strhora_dto_1), "HH:mm:ss")
        minutos_dto_1 = Valor(TablaParametros("Grabacion costes", "minutos_dto_1", 0))
        strhora_dto_2 = TablaParametros("Grabacion costes", "hora_dto_2", "")
        If strhora_dto_2 <> "" Then hora_dto_2 = Format(CDate(strhora_dto_2), "HH:mm:ss")
        minutos_dto_2 = Valor(TablaParametros("Grabacion costes", "minutos_dto_2", 0))

        strentrada_dto_2 = TablaParametros("Grabacion costes", "entrada_dto_2", "14:30:00")
        If strentrada_dto_2 <> "" Then entrada_dto_2 = Format(CDate(strentrada_dto_2), "HH:mm:ss")

        parada_m_desde_h = TablaParametros("Parada mañana antes de hora", "parada_m_desde_h", "08:40")
        parada_t_desde_h = TablaParametros("Parada tarde antes de hora", "parada_t_desde_h", "15:30")

    End Sub


    Public Function Valor(n)
        If Trim("" & n) = "" Then
            Return 0
        Else
            Return CDbl(n)
        End If
    End Function

    Private Function DifTime(ByVal hora1 As String, ByVal hora2 As String) As Long
        Dim aHora1() As String
        Dim aHora2() As String
        Dim nHora1 As Long = 0
        Dim nHora2 As Long = 0
        Dim i As Integer

        hora1 = Format("HH:mm", hora1)
        hora2 = Format("HH:mm", hora2)

        aHora1 = Split(hora1, ":")
        aHora2 = Split(hora2, ":")

        ' Ahora pasamos a segundos

        For i = 0 To 1
            Select Case i
                Case 0
                    nHora1 = nHora1 + (aHora1(i) * 3600)
                    nHora2 = nHora2 + (aHora2(i) * 3600)
                Case 1
                    nHora1 = nHora1 + (aHora1(i) * 60)
                    nHora2 = nHora2 + (aHora2(i) * 60)
                Case 2
                    nHora1 = nHora1 + aHora1(i)
                    nHora2 = nHora2 + aHora2(i)
            End Select
        Next
        Return nHora1 - nHora2


    End Function

    Function SumarHoras(hora1, hora2) As String
        Dim aHora1() As String
        Dim aHora2() As String
        Dim i As Integer
        Dim hh As Integer = 0
        Dim mm As Integer = 0
        Dim ss As Integer = 0


        aHora1 = Split(Mid(hora1, Len(Trim(hora1)) - 7, 8), ":")
        aHora2 = Split(Mid(hora2, Len(Trim(hora2)) - 7, 8), ":")

        For i = 2 To 0 Step -1
            Select Case i
                Case 2
                    ss = CInt(aHora1(i)) + CInt(aHora2(i))
                    If ss > 59 Then
                        ss = ss - 60
                        mm = 1
                    End If
                Case 1
                    mm = mm + CInt(aHora1(i)) + CInt(aHora2(i))
                    If mm > 59 Then
                        mm = mm - 60
                        hh = 1
                    End If
                Case 0
                    hh = hh + CInt(aHora1(i)) + CInt(aHora2(i))
                    If hh > 24 Then
                        hh = hh - 24
                    End If
            End Select
        Next
        Return TimeSerial(hh, mm, ss)

    End Function

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Function PonEmpresa(cArg) As String
        Dim cEmpresa As String

        If Trim(cArg) = "PEON" Then
            PonEmpresa = "11"
        ElseIf Trim(cArg) = "TIENDA" Then
            PonEmpresa = "11"
        ElseIf Trim(cArg) = "ALMAZARA" Then
            PonEmpresa = "11"
        ElseIf Trim(cArg) = "TRACTORISTA" Then
            PonEmpresa = "11"
        ElseIf Trim(cArg) = "TECNICO" Then
            PonEmpresa = "11"
        ElseIf Trim(cArg) = "CONACOOP" Then
            PonEmpresa = "1"
        Else
            PonEmpresa = "1"
        End If

    End Function


End Class
