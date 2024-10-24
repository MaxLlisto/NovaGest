Public Class FrmNEntradasNuevo15
    Public ObjetoGlobal As Object
    Public Campo As Long
    Public Variedad As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Public oform As FrmEntradaAlbaranes
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long


    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar pesos cuadrilla?", MsgBoxStyle.YesNo And MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Close()
            End If
        Else
            Close()
        End If
    End Sub

    Private Sub chPesoEspecialCuadrillas_Click()


        Try
            If chPesoEspecialCuadrillas.Checked Then
                TxtPorcentaje.Enabled = True
                TxtPesoAjustado.Enabled = True
                TxtPorcentaje.Focus()
            Else
                chPesoEspecialCuadrillas.Checked = False
                TxtPorcentaje.Enabled = False
                TxtPesoAjustado.Enabled = False
            End If
            Return
        Catch ex As Exception

        End Try


    End Sub


    Private Function Comprobacion() As Boolean

        If chPesoEspecialCuadrillas.Checked Then
            If TxtPesoAjustado.Text = 0 Then
                MsgBox("Debe de introducir un peso ajustado")
                Comprobacion = False
                TxtPesoAjustado.Focus()
                Return True
            End If
        End If
        Return True

    End Function

    Private Function GrabarEntrada() As Boolean

        Dim i As Integer
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim trans As SqlClient.SqlTransaction
        Dim Campos() As String, Valores() As String

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                Return False
            End If

            If chPesoEspecialCuadrillas.Checked Then
                RsEntradas!peso_especial_sn = "S"
                RsEntradas!peso_cuadrillas = CLng(TxtPesoAjustado.Text)
            Else
                RsEntradas!peso_especial_sn = "N"
                RsEntradas!peso_cuadrillas = RsEntradas!kg_a_liquidar
            End If
            If ChLiquidado.Checked Then
                RsEntradas!liquidada_c_sn = "S"
            Else
                RsEntradas!liquidada_c_sn = "N"
            End If
            RsEntradas.Update()
            trans.Commit()
            ObjetoGlobal.cn.Close()

            GrabarEntrada = True
            ReDim Campos(3), Valores(3)
            Campos(0) = "peso_especial_sn" : Valores(0) = RsEntradas!peso_especial_sn
            Campos(1) = "peso_cuadrillas" : Valores(1) = RsEntradas!peso_cuadrillas
            Campos(2) = "liquidada_c_sn" : Valores(2) = Trim(RsEntradas!liquidada_c_sn)
            RsEntradas.Close
            oform.AsignarValores(oform.CnTabla01, Campos, Valores)
            MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
            Return True

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Return False
        End Try

        Return True

    End Function
    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String

        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub



    Private Sub TxtPorcentaje_Validate(Cancel As Boolean)
        TxtPesoAjustado.Text = Math.Round(CLng(TxtPesoEntrado.Text) * (100 - CDbl(TxtPorcentaje.Text)) / 100, 2)
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If

    End Sub

    Private Sub FrmNEntradasNuevo15_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
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
            MsgBox("El albarán seleccionado es inexistente")
            CmdSalir()
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

        If Trim(UCase(RsEntrada!peso_especial_sn)) = "S" Then
            chPesoEspecialCuadrillas.Checked = True
        Else
            chPesoEspecialCuadrillas.Checked = False
        End If
        TxtPesoEntrado.Text = RsEntrada!kg_a_liquidar
        TxtPesoAjustado.Text = RsEntrada!peso_cuadrillas
        If Trim(UCase(RsEntrada!liquidada_c_sn)) = "S" Then
            ChLiquidado.Checked = True
        Else
            ChLiquidado.Checked = False
        End If

        chPesoEspecialCuadrillas_Click()

    End Sub
End Class