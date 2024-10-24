Imports System.ComponentModel

Public Class FrmNEntradasNuevo10
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Public oForm As FrmEntradaAlbaranes
    Dim TxtEnvase(9) As TextBox
    Dim TxtTipoEnvase(9) As TextBox
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub FrmNEntradasNuevo10_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim Cajas As Integer

        For i = 1 To 9
            TxtEnvase(i) = DevuelveControl(Me, "TxtEnvase" & i, GroupBox1)
            TxtTipoEnvase(i) = DevuelveControl(Me, "TxtTipoEnvase" & Strings.Right("00" & i, 2), GroupBox1)
        Next

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán", MsgBoxStyle.Critical)
            FlagPreguntar = False
            CmdSalir()
        End If

        SQL = "SELECT E.*,S.*,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD,P.DESCRIPCION_PER AS DESCRIPCION_PERIODO "
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
            MsgBox("El albarán seleccionado es inexistente", MsgBoxStyle.Critical)
            CmdSalir()
        End If
        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_socio) + " " + Trim("" & RsEntrada!apellidos_socio) + ", " + Trim("" & RsEntrada!nombre_socio)
        lblCampo.Text = CStr(RsEntrada!codigo_campo) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!Capataz) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)

        RsEnvases.Open("SELECT * FROM ENTRADAS_ENVASES EE JOIN ENVASES E ON E.EMPRESA=EE.EMPRESA AND E.CODIGO_ENVASE = EE.CODIGO_ENVASE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and EE.NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ENTRADA_SALIDA = 'S' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
        For i = 1 To RsEnvases.RecordCount
            If i < 9 Then
                RsEnvases.AbsolutePosition = i
                TxtTipoEnvase(i).Text = Trim(RsEnvases!codigo_envase)
                TxtEnvase(i).Text = Trim(RsEnvases!numero_envases)
                TarasEnvases(i) = RsEnvases!peso
            End If
        Next
        TxtTaraCON.Text = Format(RsEntrada!tara_con_env, "###,##0")
        lblTaraSIN.Text = Format(RsEntrada!tara_sin_env, "###,##0")
        lblKilosbrutos.Text = Format(RsEntrada!bruto_sin_env, "###,##0")
        lblKilosfruta.Text = Format(RsEntrada!kg_a_liquidar, "###,##0")
        TxtTaraCON.Focus()
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            Me.Close()
        End If

    End Sub
    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar pesos/envases de entrada?", vbYesNo) = vbYes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar pesos/envases de entrada?", vbYesNo) = vbYes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If

    End Sub

    Private Function Comprobacion()
        Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, PesoSin As Long, Cajas As Integer, i As Integer

        Comprobacion = True
        'Tara
        CalcularPesos()

        If Not (IsNumeric(TxtTaraCON.Text)) Then
            If MsgBox("No se ha indicado tara con envases. ¿Desea continuar con tara 0?", vbQuestion + vbYesNo) = vbNo Then
                Comprobacion = False
                Exit Function
            Else
                TxtTaraCON.Text = "0"
                Exit Function
            End If
        End If
        If CLng(TxtTaraCON.Text) = 0 Then
            If MsgBox("No se ha especificado tara con envases. ¿Desea continuar con tara 0?", vbQuestion + vbYesNo) = vbNo Then
                Comprobacion = False
                Exit Function
            End If
        End If
        Comprobacion = True
    End Function
    Private Function GrabarEntrada() As Boolean
        Dim Mensaje As String
        Dim i As Integer
        Dim Cuantos As Long
        Dim Envase As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim RsPendiente As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction
        Dim PesoEntrada As Single
        Dim Calidad As Integer
        Dim Campos() As String, Valores() As String

        GrabarEntrada = False
        'On Local Error GoTo HacerRollBack

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try

            'If ModoConexion = 1 Then CnLocal.BeginTrans

            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                MsgBox("Error en el albarán a modificar")
                GoTo HacerRollBack
            End If

            RsEntradas!tara_con_env = CLng(TxtTaraCON.Text)
            RsEntradas!tara_sin_env = CLng(lblTaraSIN.Text)
            If RsEntradas!peso_campo > 0 Then
                RsEntradas!kg_entrada = RsEntradas!peso_campo
                RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                RsEntradas!peso_almacen = CLng(lblKilosfruta.Text)
            Else
                RsEntradas!kg_entrada = CLng(lblKilosfruta.Text)
                RsEntradas!kg_a_liquidar = CLng(lblKilosfruta.Text)
            End If
            RsEntradas!tarada_sn = "S"
            RsEntradas.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "U"
            PesoEntrada = RsEntradas!kg_a_liquidar

            'entradas_envases (ELIMINA TODOS LOS ENVASES DE SALIDA)
            RsEntradasEnvases.Open("SELECT * FROM ENTRADAS_ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ENTRADA_SALIDA = 'S'", ObjetoGlobal.cn, True,,,,,, trans)
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
                If Cuantos > 0 Then
                    RsEnvases.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(Envase) + "'", ObjetoGlobal.cn,,,,,,, trans)
                    If RsEnvases.RecordCount > 0 Then
                        RsEntradasEnvases.AddNew()
                        RsEntradasEnvases!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsEntradasEnvases!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsEntradasEnvases!serie_albaran = Trim(SerieSeleccionada)
                        RsEntradasEnvases!numero_albaran = AlbaranSeleccionado
                        RsEntradasEnvases!codigo_envase = Trim(Envase)
                        RsEntradasEnvases!entrada_salida = "S"
                        RsEntradasEnvases!numero_envases = Cuantos
                        RsEntradasEnvases!tara = RsEnvases!peso
                        RsEntradasEnvases.Update()
                    End If
                    RsEnvases.Close()
                End If
            Next
            'If ModoConexion = 1 Then GrabarEnLocal RsEntradasEnvases, "I"
            RsEntradasEnvases.Close()

            'clasificacion
            RsEntradas.Close()
            SQL = "SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6"
            RsClasif.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsClasif.RecordCount > 0 Then
                RecalculoClasificacion(RsClasif, PesoEntrada)
                For i = 1 To RsClasif.RecordCount
                    RsClasif.AbsolutePosition = i
                    Calidad = RsClasif!codigo_calidad
                    If Calidad >= 1 And Calidad <= 12 Then
                        RsClasif!kg_calidad = Math.Round(PesoNuevo(Calidad), 0)
                        'If ModoConexion = 1 Then GrabarEnLocal RsClasif, "U"
                        RsClasif.Update()
                    End If
                Next
                SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
                RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                RsEntradas!kg_sin_clasif = 0
                RsEntradas.Update()
            Else
                SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
                RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                RsEntradas!kg_sin_clasif = RsEntradas!kg_a_liquidar
                RsEntradas.Update()
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()

            GrabarEntrada = True
            ReDim Campos(5), Valores(5)
            Campos(0) = "tara_con_env" : Valores(0) = RsEntradas!tara_con_env
            Campos(1) = "tara_sin_env" : Valores(1) = RsEntradas!tara_sin_env
            Campos(2) = "kg_entrada" : Valores(2) = RsEntradas!kg_entrada
            Campos(3) = "kg_a_liquidar" : Valores(3) = RsEntradas!kg_a_liquidar
            Campos(4) = "kg_sin_clasif" : Valores(4) = RsEntradas!kg_sin_clasif
            Campos(5) = "tarada_sn" : Valores(5) = Trim(RsEntradas!tarada_sn)
            RsEntradas.Close()
            oForm.AsignarValores(oForm.CnTabla01, Campos, Valores)
            MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("No se puede grabar la tara del albarán de entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(ex.Message))
            Return False
        End Try
        '
HacerRollBack:
        'Resume
        trans.Rollback()
        ObjetoGlobal.cn.Close()

    End Function
    Public Sub CalcularPesos()
        Dim i As Integer, Peso1 As Single, Peso2 As Single, Peso3 As Single, Cajas As Integer, Bultos As Integer

        If IsNumeric(TxtTaraCON.Text) Then
            Peso1 = CLng(TxtTaraCON.Text)
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
            Next
            Peso1 = Math.Round(Peso1 - Peso2, 0)
            lblTaraSIN.Text = Format(Peso1, "##,##0")
            lblKilosfruta.Text = Format(RsEntrada!bruto_sin_env - Peso1, "##,##0")
        End If
    End Sub

    Private Sub TxtTipoenvase09_Validating(sender As Object, e As CancelEventArgs) Handles TxtTipoenvase01.Validating, TxtTipoenvase02.Validating, TxtTipoenvase03.Validating, TxtTipoenvase04.Validating, TxtTipoenvase05.Validating, TxtTipoenvase06.Validating, TxtTipoenvase07.Validating, TxtTipoenvase08.Validating, TxtTipoenvase09.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, i As Integer

        Dim tb = DirectCast(sender, TextBox)
        Dim Index = CInt(Strings.Right(tb.Name, 1))

        If tb.Text = "" Then
            tb.Text = ""
            TarasEnvases(Index) = 0
        Else
            If TxtTipoEnvase(Index).Text = "" Then
                TxtEnvase(Index).Text = ""
                TarasEnvases(Index) = 0
            Else
                Rs.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(TxtTipoEnvase(Index).Text) + "'", ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    MsgBox("Error. Tipo de envase inexistente.")
                    e.Cancel = True
                    Return
                End If
                For i = 1 To 9
                    If i <> Index Then
                        If Trim(TxtTipoEnvase(i).Text) = Trim(TxtTipoEnvase(Index).Text) Then
                            MsgBox("Error. Tipo de envase ya anotado.")
                            e.Cancel = True
                            Return
                        End If
                    End If
                Next
                TarasEnvases(Index) = Rs!peso
            End If
            CalcularPesos()
        End If


    End Sub

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



    Private Sub TxtEnvase_Validating(sender As Object, e As CancelEventArgs) Handles TxtEnvase1.Validating, TxtEnvase2.Validating, TxtEnvase3.Validating, TxtEnvase4.Validating, TxtEnvase5.Validating, TxtEnvase6.Validating, TxtEnvase7.Validating, TxtEnvase8.Validating, TxtEnvase9.Validating
        CalcularPesos()
    End Sub

    Private Sub lblKilosfruta_Click(sender As Object, e As EventArgs) Handles lblKilosfruta.Click

    End Sub

    Private Sub lblKilosbrutos_Click(sender As Object, e As EventArgs) Handles lblKilosbrutos.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub TxtTaraCON_TextChanged(sender As Object, e As EventArgs) Handles TxtTaraCON.TextChanged

    End Sub

    Private Sub lblTaraSIN_Click(sender As Object, e As EventArgs) Handles lblTaraSIN.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub lblTransportista_Click(sender As Object, e As EventArgs) Handles lblTransportista.Click

    End Sub

    Private Sub lblVariedad_Click(sender As Object, e As EventArgs) Handles lblVariedad.Click

    End Sub

    Private Sub lblPeriodo_Click(sender As Object, e As EventArgs) Handles lblPeriodo.Click

    End Sub

    Private Sub lblCapataz_Click(sender As Object, e As EventArgs) Handles lblCapataz.Click

    End Sub

    Private Sub lblCampo_Click(sender As Object, e As EventArgs) Handles lblCampo.Click

    End Sub

    Private Sub lblFecha_Click(sender As Object, e As EventArgs) Handles lblFecha.Click

    End Sub

    Private Sub lblAlbaran_Click(sender As Object, e As EventArgs) Handles lblAlbaran.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub lblSocio_Click(sender As Object, e As EventArgs) Handles lblSocio.Click

    End Sub
End Class