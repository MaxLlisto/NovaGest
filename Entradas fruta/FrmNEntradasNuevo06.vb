Imports System.ComponentModel

Public Class FrmNEntradasNuevo06
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim lblEnvase(6) As Label
    Dim TxtEnvase(9) As TextBox
    Dim TxtTipoEnvase(9) As TextBox
    Public oform As Form

    Public Sub CalcularPesos()
        Dim i As Integer, Peso1 As Single, Peso2 As Single, Peso3 As Single, Cajas As Integer, Bultos As Integer

        If IsNumeric(TxtTaraCON.Text) Then
            Peso1 = CLng(TxtTaraCON.Text)
            Peso2 = 0
            For i = 1 To 6
                Peso3 = 0
                If IsNumeric(TxtEnvase(i).Text) Then
                    Peso3 = Math.Round(CDbl(TxtEnvase(i).Text) * CDbl(TarasEnvases(i)), 3)
                    Peso2 = Peso2 + Peso3
                End If
            Next
            For i = 7 To 9
                Peso3 = 0
                If Trim(TxtTipoEnvase7.Text) = "" Then TxtEnvase(i).Text = ""
                If Trim(TxtTipoEnvase8.Text) > "" Then
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
    Private Sub CmdGrabar_Click()
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            Me.Close()
        End If
    End Sub
    Private Function Comprobacion() As Boolean
        Dim RsOperario As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsSocio As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsCampo As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsPeriodo As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsFecha As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsCultivo As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsVariedad As cnRecordset.cnRecordset = New cnRecordset.cnRecordset

        CalcularPesos()

        If Not (IsNumeric(TxtTaraCON.Text)) Then
            If MsgBox("No se ha indicado tara con envases. ¿Desea continuar con tara 0?", vbQuestion + vbYesNo) = vbNo Then
                Return False
            Else
                TxtTaraCON.Text = "0"
                Return True
            End If
        End If
        If CLng(TxtTaraCON.Text) = 0 Then
            If MsgBox("No se ha especificado tara con envases. ¿Desea continuar con tara 0?", vbQuestion + vbYesNo) = vbNo Then
                Return False
            End If
        End If
        Return True

    End Function
    Private Function GrabarEntrada() As Boolean
        Dim RsAlbaran As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsEnvases As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsAlbaranEnvases As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsBultos As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsPendiente As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim RsCultivo As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim Mensaje As String
        Dim i As Integer
        Dim Cuantos As Long
        Dim Envase As String
        Dim trans As SqlClient.SqlTransaction

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try
            'entradas_albaranes
            RsAlbaran.Open("SELECT * FROM ENTRADAS_ALBARANES E WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran), ObjetoGlobal.cn, True,,,,,, trans)
            RsAlbaran!tara_con_env = CLng(TxtTaraCON.Text)
            RsAlbaran!tara_sin_env = CLng(lblTaraSIN.Text)
            RsAlbaran!kg_entrada = CLng(lblKilosfruta.Text)
            RsAlbaran!kg_a_liquidar = CLng(lblKilosfruta.Text)
            RsAlbaran!kg_sin_clasif = CLng(lblKilosfruta.Text)
            If Not (IsDBNull(RsAlbaran!peso_campo)) Then
                If IsNumeric(RsAlbaran!peso_campo) Then
                    If RsAlbaran!peso_campo > 0 Then
                        RsAlbaran!kg_entrada = RsAlbaran!peso_campo
                        RsAlbaran!kg_a_liquidar = RsAlbaran!peso_campo
                        RsAlbaran!kg_sin_clasif = RsAlbaran!peso_campo
                        RsAlbaran!peso_almacen = CLng(lblKilosfruta.Text)
                    End If
                End If
            End If
            RsAlbaran!tarada_sn = "S"
            RsAlbaran.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsAlbaran, "U"
            RsAlbaran.Close()
            'entradas_envases
            RsAlbaranEnvases.Open("SELECT * FROM ENTRADAS_ENVASES WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To 9
                Cuantos = 0
                Envase = ""
                If i <= 6 Then
                    If IsNumeric(TxtEnvase(i).Text) And Trim(lblEnvase(i).Text) > "" Then
                        Cuantos = CLng(TxtEnvase(i).Text)
                        Envase = Trim(lblEnvase(i).Text)
                    End If
                Else
                    If IsNumeric(TxtEnvase(i).Text) And Trim(TxtTipoEnvase(i).Text) > "" Then
                        Cuantos = CLng(TxtEnvase(i).Text)
                        Envase = Trim(TxtTipoEnvase(i).Text)
                    End If
                End If
                If Cuantos > 0 Then
                    RsEnvases.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(Envase) + "'", ObjetoGlobal.cn, ,,,,,, trans)
                    If RsEnvases.RecordCount > 0 Then
                        RsAlbaranEnvases.AddNew
                        RsAlbaranEnvases!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsAlbaranEnvases!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsAlbaranEnvases!serie_albaran = Trim(RsEntrada!serie_albaran)
                        RsAlbaranEnvases!numero_albaran = RsEntrada!numero_albaran
                        RsAlbaranEnvases!codigo_envase = Trim(Envase)
                        RsAlbaranEnvases!entrada_salida = "S"
                        RsAlbaranEnvases!numero_envases = Cuantos
                        RsAlbaranEnvases!tara = RsEnvases!peso
                        RsAlbaranEnvases.Update
                        'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranEnvases, "I"
                    End If
                    RsEnvases.Close
                End If
            Next
            RsAlbaranEnvases.Close
            trans.Commit()
            ObjetoGlobal.cn.Close()
            MsgBox("Anotada la tara del albarán :" + CStr(RsEntrada!numero_albaran))
            ImprimeAlbaran2(RsEntrada!serie_albaran, RsEntrada!numero_albaran, RsEntrada!codigo_variedad, RsEntrada!Transportista, CDbl("0" & RsEntrada!peso_campo))
            Return True

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Mensaje = Trim(ex.Message)
            MsgBox("No se puede grabar la tara del albarán de entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(Mensaje))
            Return False
        End Try

        Return False
    End Function
    '    Private Sub PicF_Click(Index As Integer)
    '        Dim Cadena As String

    '        Cadena = "{F" + CStr(Index) + "}"
    '        SendKeys Cadena
    'End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        If MsgBox("¿Desea salir SIN grabar la tara?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmNEntradasNuevo06_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsEnvases As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim i As Integer

        lblEnvase(1) = LblEnvase01
        lblEnvase(2) = LblEnvase02
        lblEnvase(3) = LblEnvase03
        lblEnvase(4) = LblEnvase04
        lblEnvase(5) = LblEnvase05
        lblEnvase(6) = LblEnvase06

        TxtEnvase(1) = TxtEnvase1
        TxtEnvase(2) = TxtEnvase2
        TxtEnvase(3) = TxtEnvase3
        TxtEnvase(4) = TxtEnvase4
        TxtEnvase(5) = TxtEnvase5
        TxtEnvase(6) = TxtEnvase6
        TxtEnvase(7) = TxtEnvase7
        TxtEnvase(8) = TxtEnvase8
        TxtEnvase(9) = TxtEnvase9

        TxtTipoEnvase(7) = TxtTipoEnvase7
        TxtTipoEnvase(8) = TxtTipoEnvase8
        TxtTipoEnvase(9) = TxtTipoEnvase9

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            Me.Close()
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
            MsgBox("El albarán seleccionado es inexistente")
            Me.Close()
        End If

        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_socio) + " " + Trim("" & RsEntrada!apellidos_socio) + ", " + Trim("" & RsEntrada!nombre_socio)
        lblCampo.Text = CStr(RsEntrada!codigo_campo) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!Capataz) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)
        lblPesoCampo.Text = Format("" & RsEntrada!peso_campo, "###,###")

        RsEnvases.Open("SELECT * FROM ENTRADAS_ENVASES_U EU JOIN ENVASES E ON E.EMPRESA=EU.EMPRESA AND E.CODIGO_ENVASE = EU.CODIGO_ENVASE WHERE EU.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' ORDER BY EU.EMPRESA,EU.CONTADOR", ObjetoGlobal.cn)
        For i = 1 To RsEnvases.RecordCount
            If i < 7 Then
                RsEnvases.AbsolutePosition = i
                lblEnvase(i).Text = Trim(RsEnvases!codigo_envase)
                TarasEnvases(i) = RsEnvases!peso
            End If
        Next
        TxtTaraCON.Text = "0"
        lblKilosbrutos.Text = Format(RsEntrada!bruto_sin_env, "###,##0")
        CalcularPesos()

    End Sub

    Private Sub TxtTipoEnvase_Validating(sender As Object, e As CancelEventArgs) Handles TxtTipoEnvase7.Validating, TxtTipoEnvase8.Validating, TxtTipoEnvase9.Validating
        'CalcularPesos()
        Dim Rs As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim i As Integer

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

            For i = 1 To 6
                If Trim(lblEnvase(i).Text) = Trim(tb.Text) Then
                    MsgBox("Error. Tipo de envase ya anotado.")
                    e.Cancel = True
                    Exit Sub
                End If
            Next

            For i = 7 To 9
                If i <> Index Then
                    If Trim(TxtTipoEnvase(i).Text) = Trim(tb.Text) Then
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


    Private Sub TxtTipoEnvase_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TxtTipoEnvase7.MouseDoubleClick, TxtTipoEnvase8.MouseDoubleClick, TxtTipoEnvase9.MouseDoubleClick
        Dim tb = DirectCast(sender, TextBox)
        Dim Index = CInt(Strings.Right(tb.Name, 1))
        Dim oFrm As FrmNEntradasNuevo04

        EnvaseSeleccionado = ""
        oFrm = New FrmNEntradasNuevo04
        oFrm.ObjetoGlobal = ObjetoGlobal
        If oFrm.ShowDialog = DialogResult.OK Then
            TxtTipoEnvase(Index).Text = Trim(EnvaseSeleccionado)
            TxtEnvase(Index).Focus()
        End If

    End Sub

    Private Sub TxtEnvase_Validated(sender As Object, e As EventArgs) Handles TxtEnvase1.Validated, TxtEnvase2.Validated, TxtEnvase3.Validated, TxtEnvase4.Validated, TxtEnvase5.Validated, TxtEnvase6.Validated, TxtEnvase7.Validated, TxtEnvase8.Validated, TxtEnvase9.Validated
        CalcularPesos()
    End Sub

    Private Sub TxtTaraCON_Validated(sender As Object, e As EventArgs) Handles TxtTaraCON.Validated
        CalcularPesos()
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        CmdGrabar_Click()
    End Sub
End Class