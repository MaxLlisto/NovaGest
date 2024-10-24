Public Class FrmNEntradasNuevo13
    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdGrabar_Click()
    End Sub

    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public Campo As Long
    Public Variedad As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Public oform As FrmEntradaAlbaranes
    Public OpSituacion(6) As RadioButton
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long


    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar datos de aviso / observaciones en esta entrada?", MsgBoxStyle.YesNo And MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub CmdGrabar_Click()
        GrabarEntrada()
        FlagPreguntar = False
        CmdSalir()
    End Sub
    Private Function Comprobacion() As Boolean
        Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, PesoSin As Long, Cajas As Integer, i As Integer

        Comprobacion = True
    End Function

    Private Function GrabarEntrada() As Boolean
        Dim Mensaje As String
        Dim i As Integer
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim Campos() As String, Valores() As String
        Dim trans As SqlClient.SqlTransaction

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try

            OpSituacion(0) = OpSituacion00
            OpSituacion(1) = OpSituacion01
            OpSituacion(2) = OpSituacion02
            OpSituacion(3) = OpSituacion03
            OpSituacion(4) = OpSituacion04

            'Entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                MsgBox("Error en el albarán a modificar")
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                Return False
            End If

            RsEntradas!historial_aviso = Trim(TxtHistorial.Text)
            RsEntradas!conclusion_aviso = Trim(TxtConclusion.Text)
            RsEntradas!observaciones = Trim(TxtObservaciones.Text)

            If OpSituacion(0).Checked Then
                RsEntradas!situacion_aviso = "N"
            ElseIf OpSituacion(1).Checked = True Then
                RsEntradas!situacion_aviso = "P"
            ElseIf OpSituacion(2).Checked = True Then
                RsEntradas!situacion_aviso = "I"
            ElseIf OpSituacion(3).Checked = True Then
                RsEntradas!situacion_aviso = "A"
            ElseIf OpSituacion(4).Checked = True Then
                RsEntradas!situacion_aviso = "C"
            Else
                RsEntradas!situacion_aviso = "N"
            End If
            RsEntradas.Update()

            trans.Commit()
            ObjetoGlobal.cn.Close()


        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Mensaje = Trim(ex.Message)
            Return False
        End Try

        ReDim Campos(3), Valores(3)
        Campos(0) = "situacion_aviso" : Valores(0) = Trim(RsEntradas!situacion_aviso)
        Campos(1) = "conclusion_aviso" : Valores(1) = Trim(RsEntradas!conclusion_aviso)
        Campos(2) = "historial_aviso" : Valores(2) = Trim(RsEntradas!historial_aviso)
        Campos(3) = "observaciones" : Valores(3) = Trim(RsEntradas!observaciones)
        RsEntradas.Close()
        oform.AsignarValores(oform.CnTabla01, Campos, Valores)

        MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
        Return True

    End Function
    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String

        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub

    Private Sub FrmNEntradasNuevo13_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim i As Integer, Cajas As Integer

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir()
        End If

        SQL = "SELECT E.observaciones as observaciones_entrada,E.*,S.*,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD,P.DESCRIPCION_PER AS DESCRIPCION_PERIODO "
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
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_socio) + " " + Trim("" & RsEntrada!apellidos_socio) + ", " + Trim("" & RsEntrada!nombre_socio)
        lblCampo.Text = CStr(RsEntrada!codigo_campo) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!Capataz) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)
        Campo = CStr(RsEntrada!codigo_campo)
        Variedad = CStr(RsEntrada!codigo_variedad)
        TxtHistorial.Text = Trim("" & RsEntrada!historial_aviso)
        TxtConclusion.Text = Trim("" & RsEntrada!conclusion_aviso)
        TxtObservaciones.Text = Trim("" & RsEntrada!observaciones_entrada)
        If Trim("" & RsEntrada!situacion_aviso) = "N" Then
            OpSituacion(0).Checked = True
        ElseIf Trim(RsEntrada!situacion_aviso) = "P" Then
            OpSituacion(1).Checked = True
        ElseIf Trim(RsEntrada!situacion_aviso) = "I" Then
            OpSituacion(2).Checked = True
        ElseIf Trim(RsEntrada!situacion_aviso) = "A" Then
            OpSituacion(3).Checked = True
        ElseIf Trim(RsEntrada!situacion_aviso) = "C" Then
            OpSituacion(4).Checked = True
        Else
            MsgBox("Anote situación de aviso no definida")
            OpSituacion(0).Checked = True
        End If
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        CmdSalir()
    End Sub
End Class
