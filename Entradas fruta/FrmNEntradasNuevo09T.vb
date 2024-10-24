Imports System.ComponentModel

Public Class FrmNEntradasNuevo09T
    Public ModificaIndustria As Boolean
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Dim VariedadSeleccionada As String 'NOSTD
    Dim TxtEnvase(9) As TextBox
    Dim TxtTipoEnvase(9) As TextBox
    Dim Txtbulto(60) As TextBox
    Dim LblBulto(60) As Label
    Public oForm As FrmEntradaAlbaranes
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long


    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If ModificaIndustria = False Then
                If MsgBox("¿Desea salir SIN modificar pesos/envases de entrada?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            Else
                If MsgBox("El cambio de albarán a industria no se realizará a no ser que guarde previamente los cambios ¿continuar?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            End If
        Else
            Me.Close()
        End If
    End Sub


    Public Sub CalcularPesos()
        Dim i As Integer, Peso1 As Single, Peso2 As Single, Peso3 As Single, Cajas As Integer, Bultos As Integer

        If IsNumeric(TxtPesoBrutoCON.Text) Then
            Peso1 = CLng(TxtPesoBrutoCON.Text)
            Peso2 = 0
            For i = 1 To 9
                Peso3 = 0
                If Trim(TxtTipoEnvase(i).Text) = "" Then TxtEnvase(i).Text = ""
                If Trim(TxtTipoEnvase(i).Text) > "" Then
                    If TarasEnvases(i) <> 0 Then
                        If IsNumeric(TxtEnvase(i).Text) Then Peso3 = Math.Round(CDbl(TxtEnvase(i).Text) * CDbl(TarasEnvases(i)), 3)
                        Peso2 = Peso2 + Peso3
                    End If
                End If
                If IsNumeric(TxtEnvase(i).Text) Then
                    If Trim(TxtTipoEnvase(i).Text) = "CAJA" Or Trim(TxtTipoEnvase(i).Text) = "CAZUL" Or Trim(TxtTipoEnvase(i).Text) = "CCAFE" Or Trim(TxtTipoEnvase(i).Text) = "CCOL" Or Trim(TxtTipoEnvase(i).Text) = "CLIRIASP" Or Trim(TxtTipoEnvase(i).Text) = "CAJA." Then
                        If IsNumeric(TxtEnvase(i).Text) Then Cajas = Cajas + CLng(TxtEnvase(i).Text)
                    End If
                    If Trim(TxtTipoEnvase(i).Text) = "PALET" Or Trim(TxtTipoEnvase(i).Text) = "PV" Or Trim(TxtTipoEnvase(i).Text) = "PALET." Then
                        If IsNumeric(TxtEnvase(i).Text) Then Bultos = Bultos + CLng(TxtEnvase(i).Text)
                    End If
                End If
            Next
            Peso1 = Math.Round(Peso1 - Peso2, 0)
            TxtPesoBrutoSIN.Text = CStr(Peso1)
            TxtTotalCajas.Text = CStr(Cajas)
        End If
    End Sub
    '    Private Sub TxtEnvase_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
    '        Debug.Print "d", KeyCode

    'If KeyCode = 40 Then
    '            SendKeys vbTab
    '    KeyCode = 0
    '        ElseIf KeyCode = 38 Then
    '            KeyCode = 0
    '            CalcularPesos()

    '            If Index > 1 Then
    '                TxtEnvase(Index - 1).SetFocus
    '            Else
    '                TxtPesoBrutoCON.SetFocus
    '            End If
    '        ElseIf KeyCode = 39 Then
    '            KeyCode = 0
    '            CalcularPesos()
    '            TxtTotalbultos.SetFocus
    '        End If

    '    End Sub
    Private Sub TxtTipoEnvase_GotFocus(Index As Integer)
        EnvaseEnModificacion = Index
    End Sub

    Private Sub CmdRecalcularBultos_Click()
        Dim i As Integer
        Dim Cajas As Integer
        Dim SumaCajas As Integer
        Dim Bultos As Integer
        Dim CajasBulto As Integer

        Cajas = 0
        If IsNumeric(TxtTotalCajas.Text) Then
            Cajas = CLng(TxtTotalCajas.Text)
        End If

        Bultos = 0
        If IsNumeric(TxtTotalbultos.Text) Then
            Bultos = CLng(TxtTotalbultos.Text)
        End If
        CajasBulto = 0
        If IsNumeric(TxtCajasBulto.Text) Then
            CajasBulto = CLng(TxtCajasBulto.Text)
        End If
        If CajasBulto = 0 And Bultos > 0 Then
            CajasBulto = Cajas / Bultos
        End If
        SumaCajas = 0

        If Cajas > 0 And Bultos > 0 Then
            For i = 1 To Bultos - 1
                If SumaCajas + CajasBulto <= Cajas Then
                    Txtbulto(i).Text = CStr(CajasBulto)
                ElseIf SumaCajas < Cajas Then
                    Txtbulto(i).Text = CStr(Cajas - SumaCajas)
                Else
                    Txtbulto(i).Text = "0"
                End If
                SumaCajas = SumaCajas + CLng(Txtbulto(i).Text)
            Next
            If SumaCajas < Cajas Then Txtbulto(Bultos).Text = CStr(Cajas - SumaCajas) Else Txtbulto(Bultos).Text = "0"
        End If
    End Sub
    Private Function Comprobacion() As Boolean
        Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim PesoSin As Long
        Dim Cajas As Integer
        Dim i As Integer

        Comprobacion = True
        'Pesos y bultos
        If Not (IsNumeric(TxtPesoBrutoCON.Text)) Then
            MsgBox("No se ha indicado peso con envases.")
            Return False

        End If
        If CLng(TxtPesoBrutoCON.Text) = 0 Then
            MsgBox("No se ha especificado peso con envases.")
            Return False

        End If
        If Not (IsNumeric(TxtPesoBrutoSIN.Text)) Then
            MsgBox("No se ha indicado peso sin envases.")
            Return False
        End If
        If Strings.Left(VariedadSeleccionada, 2) <> "12" And Strings.Left(VariedadSeleccionada, 2) <> "11" Then
            If Not (IsNumeric(TxtTotalbultos.Text)) And Trim(RsEntrada!industria_sn) = "N" Then
                MsgBox("No se ha indicado número de bultos.")
                Return False

            End If
            If CInt(TxtTotalbultos.Text) <= 0 And Trim(RsEntrada!industria_sn) = "N" Then
                MsgBox("No se ha indicado número de bultos.")
                Return False
            End If
            Cajas = CLng(TxtTotalCajas.Text)
        End If

        PesoSin = CLng(TxtPesoBrutoSIN.Text)

        CalcularPesos()

        If PesoSin <> CLng(TxtPesoBrutoSIN.Text) Then
            MsgBox("El peso bruto sin envases se ha reclaculado.Compruebe los cambios.")
            Return False
        End If
        If Strings.Left(VariedadSeleccionada, 2) <> "12" And Strings.Left(VariedadSeleccionada, 2) <> "11" Then
            If Cajas <> CLng(TxtTotalCajas.Text) Then
                MsgBox("El número de cajas se ha reclaculado.Compruebe los cambios.")
                Comprobacion = False
                Exit Function
            End If
        End If
        Cajas = 0
        If Strings.Left(VariedadSeleccionada, 2) <> "12" And Strings.Left(VariedadSeleccionada, 2) <> "11" Then
            For i = 1 To 60
                If IsNumeric(Txtbulto(i).Text) Then
                    Cajas = Cajas + CLng(Txtbulto(i).Text)
                End If
            Next
            If Cajas <> CLng(TxtTotalCajas.Text) Then
                MsgBox("El total de cajas no coincide con las indicadas en los bultos.")
                Comprobacion = False
                Exit Function
            End If
        End If
        Comprobacion = True
    End Function
    Private Function GrabarEntrada() As Boolean
        Dim i As Integer
        Dim Cuantos As Long
        Dim Envase As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim CuantosBultos As Integer
        Dim PesoEntrada As Single
        Dim Calidad As Integer
        Dim Campos() As String
        Dim Valores() As String
        Dim ConBultosVolcados As Boolean
        Dim trans As SqlClient.SqlTransaction

        GrabarEntrada = False
        'On Local Error GoTo HacerRollBack


        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try

            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                trans.Rollback()
                MsgBox("Error en el albarán a modificar")
                Return False
            End If
            RsEntradas!peso_bruto_fruta = CLng(TxtPesoBrutoCON.Text)
            RsEntradas!bruto_sin_env = CLng(TxtPesoBrutoSIN.Text)
            RsEntradas!peso_campo = CLng("0" & TxtPesoCampo.Text)
            If Trim(RsEntradas!tarada_sn) = "N" Then
                RsEntradas!tara_con_env = 0
                RsEntradas!tara_sin_env = 0
                RsEntradas!kg_entrada = 0
                RsEntradas!kg_a_liquidar = 0
                RsEntradas!kg_sin_clasif = 0
            ElseIf RsEntradas!kg_sin_clasif > 0 Then
                If RsEntradas!peso_campo > 0 Then
                    RsEntradas!kg_entrada = RsEntradas!peso_campo
                    RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                    RsEntradas!kg_sin_clasif = RsEntradas!kg_entrada
                    RsEntradas!peso_almacen = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                Else
                    RsEntradas!kg_entrada = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                    RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                    RsEntradas!kg_sin_clasif = RsEntradas!kg_entrada
                End If
            Else
                If RsEntradas!peso_campo > 0 Then
                    RsEntradas!kg_entrada = RsEntradas!peso_campo
                    RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                    RsEntradas!kg_sin_clasif = RsEntradas!kg_entrada
                    RsEntradas!peso_almacen = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                Else
                    RsEntradas!kg_entrada = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                    RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                    RsEntradas!kg_sin_clasif = 0
                End If
            End If
            If ModificaIndustria = True Then
                RsEntradas!industria_sn = "N"
            End If
            PesoEntrada = RsEntradas!kg_entrada
            RsEntradas.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "U"

            'entradas_envases (ELIMINA TODOS LOS ENVASES DE ENTRADA)
            RsEntradasEnvases.Open("SELECT * FROM ENTRADAS_ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ENTRADA_SALIDA = 'E'", ObjetoGlobal.cn, True,,,,,, trans)
            While RsEntradasEnvases.RecordCount > 0
                RsEntradasEnvases.MoveFirst()
                'If ModoConexion = 1 Then GrabarEnLocal RsEntradasEnvases, "D"
                RsEntradasEnvases.Delete()
            End While
            For i = 1 To 9
                Cuantos = 0
                Envase = ""
                If IsNumeric(TxtEnvase(i).Text) And Trim(TxtTipoEnvase(i).Text) > "" Then
                    Cuantos = CLng(TxtEnvase(i).Text)
                    Envase = Trim(TxtTipoEnvase(i).Text)
                End If
                If Cuantos > 0 Then ' error
                    RsEnvases.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(Envase) + "'", ObjetoGlobal.cn, ,,,,,, trans)
                    If RsEnvases.RecordCount > 0 Then
                        RsEntradasEnvases.AddNew()
                        RsEntradasEnvases!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsEntradasEnvases!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsEntradasEnvases!serie_albaran = Trim(SerieSeleccionada)
                        RsEntradasEnvases!numero_albaran = AlbaranSeleccionado
                        RsEntradasEnvases!codigo_envase = Trim(Envase)
                        RsEntradasEnvases!entrada_salida = "E"
                        RsEntradasEnvases!numero_envases = Cuantos
                        RsEntradasEnvases!tara = RsEnvases!peso
                        RsEntradasEnvases.Update()
                    End If
                    RsEnvases.Close()
                End If
            Next
            'If ModoConexion = 1 Then GrabarEnLocal RsEntradasEnvases, "I"
            RsEntradasEnvases.Close()

            'NO SE MODIFICA SI ES UN ALBARAN CON BULTOS VOLCADOS
            ' Comprobamos
            ConBultosVolcados = False

            RsBultos.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND situacion = 'V'", ObjetoGlobal.cn, ,,,,,, trans)
            If RsBultos.RecordCount > 0 Then
                ConBultosVolcados = True ' Tiene bultos volcados
            End If
            RsBultos.Close()

            If Not ConBultosVolcados Then
                'entradas_bultos
                RsBultos.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
                While RsBultos.RecordCount > 0
                    RsBultos.MoveFirst()
                    'If ModoConexion = 1 Then GrabarEnLocal RsBultos, "D"
                    RsBultos.Delete()
                End While
                RsBultos.Close()
                CuantosBultos = CInt(TxtTotalbultos.Text)
                RsBultos.Open("SELECT * FROM ENTRADAS_BULTOS WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                'Bultos
                For i = 1 To CuantosBultos
                    RsBultos.AddNew()
                    RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    RsBultos!serie_albaran = Trim(SerieSeleccionada)
                    RsBultos!numero_albaran = AlbaranSeleccionado
                    RsBultos!Bulto = i
                    RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), AlbaranSeleccionado, i)
                    RsBultos!Cajas = CInt(Txtbulto(i).Text)
                    RsBultos.Update()
                Next
                'Cajas de clasificación (2)
                If Trim(RsEntradas!codigo_variedad) >= "01" And Trim(RsEntradas!codigo_variedad) <= "02z" Then
                    For i = 1 To 2
                        RsBultos.AddNew()
                        RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsBultos!serie_albaran = Trim(SerieSeleccionada)
                        RsBultos!numero_albaran = AlbaranSeleccionado
                        RsBultos!Bulto = 100 + i
                        RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), AlbaranSeleccionado, 100 + i)
                        RsBultos!Cajas = 1
                        RsBultos.Update()
                    Next
                    'Cajas de reclamación (1)
                    For i = 1 To 1
                        RsBultos.AddNew()
                        RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsBultos!serie_albaran = Trim(SerieSeleccionada)
                        RsBultos!numero_albaran = AlbaranSeleccionado
                        RsBultos!Bulto = 110 + i
                        RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), AlbaranSeleccionado, 110 + i)
                        RsBultos!Cajas = 1
                        RsBultos.Update()
                    Next
                    'Cajas de control recogida (2)
                    For i = 1 To 2
                        RsBultos.AddNew()
                        RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsBultos!serie_albaran = Trim(SerieSeleccionada)
                        RsBultos!numero_albaran = AlbaranSeleccionado
                        RsBultos!Bulto = 120 + i
                        RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), AlbaranSeleccionado, 120 + i)
                        RsBultos!Cajas = 1
                        RsBultos.Update()
                    Next
                    '    ElseIf (Trim(VariedadSeleccionada) >= "141" And Trim(VariedadSeleccionada) <= "147") Or Trim(VariedadSeleccionada) = "171" Then
                    '        CajasTotales = math.round(CInt("0" & TxtTotalBultos.Text) / 6, 0)
                    '        CajasTotales = CajasTotales + IIf((CajasTotales * 6) < CInt("0" & TxtTotalBultos.Text), 1, 0)
                    '        For i = 1 To 2
                    '            RsBultos.AddNew
                    '            RsBultos!EMPRESA = Trim(ObjetoGlobal.EmpresaActual)
                    '            RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    '            RsBultos!serie_albaran = Trim(SerieSeleccionada)
                    '            RsBultos!numero_albaran = AlbaranSeleccionado
                    '            RsBultos!Bulto = IIf(i = 1, 0, 999)
                    '            RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), AlbaranSeleccionado, IIf(i = 1, 0, 999))
                    '            RsBultos!Cajas = CajasTotales * 6
                    '            RsBultos.Update
                    '        Next
                End If
                'If ModoConexion = 1 Then GrabarEnLocal RsBultos, "I"
                RsBultos.Close()
            End If

            'clasificacion
            SQL = "SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6"
            RsClasif.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            RecalculoClasificacion(RsClasif, PesoEntrada)
            For i = 1 To RsClasif.RecordCount
                RsClasif.AbsolutePosition = i
                Calidad = RsClasif!codigo_calidad
                If Calidad >= 1 And Calidad <= 12 Then ' ver con Paco
                    RsClasif!kg_calidad = Math.Round(PesoNuevo(Calidad), 0)
                    'If ModoConexion = 1 Then GrabarEnLocal RsClasif, "U"
                    RsClasif.Update()
                End If
            Next
            ' Fin de la pregunta
            trans.Commit()
            ObjetoGlobal.cn.Close()

            'If ModoConexion = 1 Then CnLocal.CommitTrans
            GrabarEntrada = True
            ReDim Campos(5), Valores(5)
            Campos(0) = "peso_bruto_fruta" : Valores(0) = RsEntradas!peso_bruto_fruta
            Campos(1) = "bruto_sin_env" : Valores(1) = RsEntradas!bruto_sin_env
            Campos(2) = "kg_entrada" : Valores(2) = RsEntradas!kg_entrada
            Campos(3) = "kg_a_liquidar" : Valores(3) = RsEntradas!kg_a_liquidar
            Campos(4) = "kg_sin_clasif" : Valores(4) = RsEntradas!kg_sin_clasif
            Campos(5) = "peso_campo" : Valores(5) = "" & RsEntradas!peso_campo

            oForm.AsignarValores(oForm.CnTabla01, Campos, Valores)

            MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("No se puede grabar la entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(ex.Message))
            Return False
        End Try

    End Function
    Public Sub RecalculoClasificacion(Rs As cnRecordset.CnRecordset, PesoEntrada As Single)
        Dim i As Integer
        Dim Peso1 As Single
        Dim Peso2 As Single
        Dim Peso3 As Single
        Dim Proporcion As Single
        Dim Calidad As Integer
        Dim Muestra(12) As Single
        Dim Incremento As Integer
        Dim MejorFactor As Single
        Dim Factor As Single
        Dim Quien As Integer

        Peso1 = Math.Round(PesoEntrada, 0)
        If Peso1 = 0 Then Exit Sub
        Peso2 = 0
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            Calidad = Rs!codigo_calidad
            If Calidad >= 1 And Calidad <= 12 Then ' Ver con Paco
                Peso3 = Math.Round(Rs!kg_muestra, 2)
                Muestra(Calidad) = Peso3
                Peso2 = Peso2 + Peso3
            End If
        Next
        If Peso2 = 0 Then Exit Sub
        Proporcion = Peso1 / Peso2
        PesoNuevo(0) = 0
        For i = 1 To Rs.RecordCount
            PesoNuevo(i) = Math.Round(Muestra(i) * Proporcion, 0)
            PesoNuevo(0) = PesoNuevo(0) + PesoNuevo(i)
        Next
        Do While PesoNuevo(0) <> Peso1
            Quien = -1
            If Peso1 > PesoNuevo(0) Then Incremento = 1 Else Incremento = -1
            For i = 1 To Rs.RecordCount
                If Muestra(i) > 0 Then
                    Factor = ((PesoNuevo(i) + Incremento) / Muestra(i)) - Proporcion
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
                Exit Sub
            End If
        Loop
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If

    End Sub

    Private Sub FrmNEntradasNuevo09T_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, i As Integer, Cajas As Integer
        Dim NumeroBulto As Integer, Bultos As Integer

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir()
        End If

        For i = 1 To 9
            TxtEnvase(i) = DevuelveControl(Me, "TxtEnvase" & i, GroupBox1)
            TxtTipoEnvase(i) = DevuelveControl(Me, "TxtTipoEnvase" & Strings.Right("00" & i, 2), GroupBox1)
        Next
        For i = 1 To 60
            Txtbulto(i) = DevuelveControl(Me, "TxtBulto" & Strings.Right("00" & i, 2), GroupBox3)
            LblBulto(i) = DevuelveControl(Me, "LblBulto" & Strings.Right("00" & i, 2), GroupBox3)
        Next
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
        VariedadSeleccionada = CStr("" & RsEntrada!codigo_variedad)

        RsEnvases.Open("SELECT * FROM ENTRADAS_ENVASES EE JOIN ENVASES E ON E.EMPRESA=EE.EMPRESA AND E.CODIGO_ENVASE = EE.CODIGO_ENVASE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and EE.NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ENTRADA_SALIDA = 'E' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
        Cajas = 0
        For i = 1 To RsEnvases.RecordCount
            If i < 9 Then
                RsEnvases.AbsolutePosition = i
                TxtTipoEnvase(i).Text = Trim(RsEnvases!codigo_envase)
                TxtEnvase(i).Text = Trim(RsEnvases!numero_envases)
                Cajas = Cajas + RsEnvases!numero_envases
                TarasEnvases(i) = RsEnvases!peso
            End If
        Next
        TxtPesoCampo.Text = Format("0" & RsEntrada!peso_campo, "###,##0")
        TxtPesoBrutoCON.Text = Format(RsEntrada!peso_bruto_fruta, "###,##0")
        TxtPesoBrutoSIN.Text = Format(RsEntrada!bruto_sin_env, "###,##0")
        TxtTotalCajas.Text = Format(Cajas, "#,##0")

        RsBultos.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND bulto<=60 ORDER BY 1,2,3,4,5", ObjetoGlobal.cn)
        For i = 1 To RsBultos.RecordCount
            RsBultos.AbsolutePosition = i
            NumeroBulto = RsBultos!Bulto
            Txtbulto(NumeroBulto).Text = Trim(RsBultos!Cajas)
            Bultos = NumeroBulto
        Next
        For i = Bultos + 1 To 60
            Txtbulto(i).Visible = False
            LblBulto(i).Visible = False
        Next
        TxtTotalbultos.Text = CStr(Bultos)

    End Sub

    Private Sub TxtTotalbultos_Validating(sender As Object, e As CancelEventArgs) Handles TxtTotalbultos.Validating
        Dim i As Integer, Cuantos As Integer

        If IsNumeric(TxtTotalbultos.Text) Then
            Cuantos = TxtTotalbultos.Text
        Else
            Cuantos = 0
            TxtTotalbultos.Text = "0"
        End If
        If Cuantos > 60 Then
            MsgBox("El máximo número de bultos es 60.")
            e.Cancel = True
            Exit Sub
        End If
        For i = 1 To Cuantos
            LblBulto(i).Visible = True
            Txtbulto(i).Visible = True
        Next
        For i = Cuantos + 1 To 60
            Txtbulto(i).Text = ""
            LblBulto(i).Visible = False
            Txtbulto(i).Visible = False
        Next

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir()
    End Sub

    Private Sub TxtPesoBrutoCON_Validated(sender As Object, e As EventArgs) Handles TxtPesoBrutoCON.Validated
        CalcularPesos()
    End Sub

    Private Sub TxtTipoenvase01_TextChanged(sender As Object, e As EventArgs) Handles TxtTipoenvase01.TextChanged, TxtTipoenvase02.TextChanged, TxtTipoenvase03.TextChanged, TxtTipoenvase04.TextChanged, TxtTipoenvase05.TextChanged, TxtTipoenvase06.TextChanged, TxtTipoenvase07.TextChanged, TxtTipoenvase08.TextChanged, TxtTipoenvase09.TextChanged
        CalcularPesos()
    End Sub

    Private Sub TxtEnvase1_TextChanged(sender As Object, e As EventArgs) Handles TxtEnvase1.TextChanged

    End Sub

    Private Sub TxtEnvase1_Validated(sender As Object, e As EventArgs) Handles TxtEnvase1.Validated, TxtEnvase2.Validated, TxtEnvase3.Validated, TxtEnvase4.Validated, TxtEnvase5.Validated, TxtEnvase6.Validated, TxtEnvase7.Validated, TxtEnvase8.Validated, TxtEnvase9.Validated
        CalcularPesos()
    End Sub

    Private Sub TxtTipoenvase09_Validating(sender As Object, e As CancelEventArgs) Handles TxtTipoenvase01.Validating, TxtTipoenvase02.Validating, TxtTipoenvase03.Validating, TxtTipoenvase04.Validating, TxtTipoenvase05.Validating, TxtTipoenvase06.Validating, TxtTipoenvase07.Validating, TxtTipoenvase08.Validating, TxtTipoenvase09.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, i As Integer
        Dim tb = DirectCast(sender, TextBox)
        Dim Index = CInt(Strings.Right(tb.Name, 1))

        If tb.Text = "" Then
            tb.Text = ""
            TarasEnvases(Index) = 0
        Else
            Rs.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(tb.Text) + "'", ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Error. Tipo de envase inexistente.")
                e.Cancel = True
                Exit Sub
            End If
            For i = 1 To 9
                If i <> Index Then
                    If Trim(TxtTipoEnvase(i).Text) = Trim(TxtTipoEnvase(Index).Text) Then
                        MsgBox("Error. Tipo de envase ya anotado.")
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            Next
            TarasEnvases(Index) = Rs!peso
        End If
        CalcularPesos()

    End Sub
End Class
