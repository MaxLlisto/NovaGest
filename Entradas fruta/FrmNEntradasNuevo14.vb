Imports System.ComponentModel

Public Class FrmNEntradasNuevo14
    Public ObjetoGlobal As Object
    Public Campo As Long
    Public Variedad As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Public oForm As FrmEntradaAlbaranes
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub CmdSalir_Click()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar capataz/transportista en esta entrada?", vbYesNo) = vbYes Then
                Close()
            End If
        Else
            Close()
        End If
    End Sub
    Private Sub Form_Load()
        Dim SQL As String

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán", MsgBoxStyle.Critical)
            FlagPreguntar = False
            CmdSalir_Click()
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
            CmdSalir_Click()
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
        Campo = CStr(RsEntrada!codigo_campo)
        Variedad = CStr(RsEntrada!codigo_variedad)
        TxtCapataz.Text = RsEntrada!Capataz
        TxtTransportista.Text = RsEntrada!Transportista
        TxtCapataz.Text = Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportistaN.Text = Trim(RsEntrada!nombre_transportista)
        Me.Show()
        TxtCapataz.Focus()
    End Sub


    Private Function Comprobacion() As Boolean
        Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, PesoSin As Long, Cajas As Integer, i As Integer

        Comprobacion = True
        'Capataz
        If Trim(TxtCapataz.Text) = "" Then
            MsgBox("No se ha indicado capataz.", MsgBoxStyle.Critical)
            Return False
        End If

        RsOperario.Open("SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtCapataz.Text), ObjetoGlobal.cn)
        If RsOperario.RecordCount = 0 Then
            MsgBox("Capataz inexistente", MsgBoxStyle.Critical)
            Return False
        ElseIf IsDBNull(RsOperario!referencia_gd) Or Strings.Left("" & RsOperario!referencia_gd, 1) = "-" Then
            MsgBox("No se puede indicar compo capataz un operario no activo", MsgBoxStyle.Critical)
            Return False
        ElseIf RsOperario!codigo_operario >= 20000 Then
            MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo capataz", MsgBoxStyle.Critical)
            Return False
        End If
        'Transportista
        If Trim(TxtTransportista.Text) = "" Then
            MsgBox("No se ha indicado transportista.")
            Return False
        End If
        RsOperario.Close()
        RsOperario.Open("SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtTransportista.Text), ObjetoGlobal.cn)
        If RsOperario.RecordCount = 0 Then
            MsgBox("Transportista inexistente", MsgBoxStyle.Critical)
            Return False
        ElseIf IsDBNull(RsOperario!referencia_gd) Or Strings.Left("" & RsOperario!referencia_gd, 1) = "-" Then
            MsgBox("No se puede indicar compo transportista un operario no activo", MsgBoxStyle.Critical)
            Return False
        ElseIf RsOperario!codigo_operario >= 20000 Then
            MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo transportista", MsgBoxStyle.Critical)
            Return False
        End If
        Return True
    End Function

    Private Function GrabarEntrada() As Boolean
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim trans As SqlClient.SqlTransaction
        Dim Campos() As String, Valores() As String

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try

            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                MsgBox("Error en el albarán a modificar", MsgBoxStyle.Critical)
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                Return False
            End If

            RsEntradas!Capataz = CLng(TxtCapataz.Text)
            RsEntradas!Transportista = CLng(TxtTransportista.Text)
            RsEntradas.Update()

            trans.Commit()
            ObjetoGlobal.cn.Close()

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Return False
        End Try

        GrabarEntrada = True
        ReDim Campos(2), Valores(2)
        Campos(0) = "capataz" : Valores(0) = RsEntradas!Capataz
        Campos(1) = "transportista" : Valores(1) = RsEntradas!Transportista
        RsEntradas.Close()
        oForm.AsignarValores(oForm.CnTabla01, Campos, Valores)
        MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado), MsgBoxStyle.Information)
        Return True

    End Function
    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String
        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub

    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar periodo/ fecha/hora de entrada?", MsgBoxStyle.YesNo And MsgBoxStyle.Question) = MsgBoxStyle.YesNo Then
                Close()
            End If
        Else
            Close()
        End If
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If
    End Sub

    Private Sub TxtTransportista_TextChanged(sender As Object, e As EventArgs) Handles TxtTransportista.TextChanged

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir()
    End Sub

    Private Sub TxtCapataz_Validating(sender As Object, e As CancelEventArgs) Handles TxtCapataz.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, SQL As String

        If Trim(TxtCapataz.Text) = "" Then
            lblCapataz.Text = ""
        Else
            SQL = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtCapataz.Text)
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Capataz inexistente.")
                e.Cancel = True
                lblCapataz.Text = ""
            ElseIf IsDBNull(Rs!referencia_gd) Or Strings.Left("" & Rs!referencia_gd, 1) = "-" Then
                MsgBox("No se puede indicar compo capataz un operario no activo")
                e.Cancel = True
                lblCapataz.Text = ""
            ElseIf Rs!codigo_operario >= 20000 Then
                MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo capataz")
                e.Cancel = True
                lblCapataz.Text = ""
            Else
                lblCapataz.Text = "" & Trim(Rs!nombre_operario)
            End If
        End If
    End Sub

    Private Sub TxtTransportista_Validating(sender As Object, e As CancelEventArgs) Handles TxtTransportista.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, SQL As String, Fecha As Date

        If Trim(TxtTransportista.Text) = "" Then
            lblTransportista.Text = ""
        Else
            SQL = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtTransportista.Text)
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Transportista inexistente.")
                e.Cancel = True
                lblTransportista.Text = ""
            ElseIf IsDBNull(Rs!referencia_gd) Or Strings.Left("" & Rs!referencia_gd, 1) = "-" Then
                MsgBox("No se puede indicar compo transportista un operario no activo")
                e.Cancel = True
                lblCapataz.Text = ""
            ElseIf Rs!codigo_operario >= 20000 Then
                MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo transportista")
                e.Cancel = True
                lblCapataz.Text = ""
            Else
                lblTransportista.Text = "" & Trim(Rs!nombre_operario)
            End If
        End If
    End Sub
End Class