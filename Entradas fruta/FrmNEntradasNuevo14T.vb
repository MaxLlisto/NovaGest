Imports System.ComponentModel

Public Class FrmNEntradasNuevo14T
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
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir_Click()
        End If

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
            CmdSalir_Click()
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

        Campo = CStr(RsEntrada!campo_terceros)
        Variedad = CStr(RsEntrada!codigo_variedad)
        TxtCapataz.Text = RsEntrada!capataz_terc
        TxtTransportista.Text = RsEntrada!Transportista_terc
        LblCapatazN.Text = Trim(RsEntrada!NOMBRE_CAPATAZ)
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


        Comprobacion = True
        'Capataz
        RsOperario.Open("SELECT * FROM personal_terceros WHERE codigo = " + Trim(TxtCapataz.Text), ObjetoGlobal.cn)
        If RsOperario.RecordCount = 0 Then
            MsgBox("Capataz terceros inexistente", MsgBoxStyle.Critical)
            Return False
        End If
        'Transportista
        If Trim(TxtTransportista.Text) = "" Then
            MsgBox("No se ha indicado transportista.", MsgBoxStyle.Critical)
            Comprobacion = False
            Exit Function
        End If
        RsOperario.Close()

        RsOperario.Open("SELECT * FROM personal_terceros WHERE codigo = " + Trim(TxtTransportista.Text), ObjetoGlobal.cn)
        If RsOperario.RecordCount = 0 Then
            MsgBox("Transportista terceros inexistente", MsgBoxStyle.Critical)
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
                MsgBox("Error en el albarán a modificar")
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                Return False
            End If

            RsEntradas!capataz_terc = CLng(TxtCapataz.Text)
            RsEntradas!Transportista_terc = CLng(TxtTransportista.Text)
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
        Campos(0) = "capataz_terc" : Valores(0) = RsEntradas!capataz_terc
        Campos(1) = "transportista_terc" : Valores(1) = RsEntradas!Transportista_terc
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

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir()
    End Sub

    Private Sub TxtCapataz_Validating(sender As Object, e As CancelEventArgs) Handles TxtCapataz.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String

        If Trim(TxtTransportista.Text) = "" Then
            lblTransportista.Text = ""
        Else
            SQL = "SELECT * FROM personal_terceros WHERE codigo = " + Trim(TxtCapataz.Text)
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Capataz terceros inexistente.", MsgBoxStyle.Critical)
                e.Cancel = True
                lblCapataz.Text = ""
            Else
                lblTransportista.Text = "" & Trim(Rs!nombre_operario)
            End If
        End If
    End Sub

    Private Sub TxtTransportista_Validating(sender As Object, e As CancelEventArgs) Handles TxtTransportista.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, SQL As String, Fecha As Date

        If Trim(TxtTransportista.Text) = "" Then
            lblTransportista.Text = ""
        Else
            SQL = "SELECT * FROM personal_terceros WHERE codigo = " + Trim(TxtTransportista.Text)
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Transportista terceros inexistente.", MsgBoxStyle.Critical)
                e.Cancel = True
                lblTransportista.Text = ""
            Else
                lblTransportista.Text = "" & Trim(Rs!Nombre)
            End If
        End If
    End Sub

End Class