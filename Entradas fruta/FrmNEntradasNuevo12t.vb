Imports System.ComponentModel

Public Class FrmNEntradasNuevo12T
    Public ObjetoGlobal As Object
    Public Campo As Long
    Public Variedad As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Dim VariedadSeleccionada As String
    Public oform As FrmEntradaAlbaranes
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar periodo/ fecha/hora de entrada?", MsgBoxStyle.YesNo And MsgBoxStyle.Question) = MsgBoxStyle.YesNo Then
                Close()
            End If
        Else
            Close()
        End If
    End Sub
    Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
        'Dim SinUso As Boolean
        '
        If KeyCode = 123 Then 'F12 para grabar
            CmdGrabar_Click()
            KeyCode = 0
        ElseIf KeyCode = 118 Then 'F7 periodo
            If Trim(Variedad) > "" Then
                Dim oFrm As FrmNEntradasNuevo03 = New FrmNEntradasNuevo03
                oFrm.Variedad = Trim(Variedad)
                oFrm.oForm = Me
                oFrm.ShowDialog()
                TxtPeriodo.Focus()
            Else
                MsgBox("Debe seleccionar previamente campo  y variedad.")
            End If
        End If

    End Sub

    Private Sub Form_Load()
        Dim SQL As String

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir()
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
            MsgBox("El albarán seleccionado es inexistente", MsgBoxStyle.Critical)
            CmdSalir()
        End If
        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_proveedor) + " " + Trim("" & RsEntrada!razon_social)
        lblCampo.Text = CStr(RsEntrada!campo_terceros) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!capataz_terc) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista_terc) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)
        LblPeriodoN.Text = Trim(RsEntrada!descripcion_periodo)
        Campo = CStr(RsEntrada!campo_terceros)
        Variedad = CStr(RsEntrada!codigo_variedad)
        VariedadSeleccionada = CStr(RsEntrada!codigo_variedad)
        TxtPeriodo.Text = RsEntrada!codigo_periodo
        DTFecha.value = RsEntrada!fecha_entrada
        TxtHora.Text = RsEntrada!hora_entrada
        TxtPeriodo.Focus()
    End Sub

    Private Sub CmdGrabar_Click()
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If
    End Sub
    Private Function Comprobacion()
        Dim RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Cadena As String

        Comprobacion = True

        If Trim(TxtPeriodo.Text) = "" Then
            MsgBox("El periodo no puede permanecer vacio")
            Comprobacion = False
            Exit Function
        End If

        RsPeriodo.Open("SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_PERIODO = " + Trim(TxtPeriodo.Text) + " AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'", ObjetoGlobal.cn)
        If RsPeriodo.RecordCount = 0 Then
            MsgBox("Periodo inexistente")
            Return False
        End If

        Cadena = Format(CDate(DTFecha.value), "dd/mm/yyyy")
        RsFecha.Open("SELECT dbo.fn_que_ejercicio ('" + Trim(Cadena) + "') as ejercicio", ObjetoGlobal.cn)
        If Trim(RsFecha!Ejercicio) <> CStr(ObjetoGlobal.EjercicioActual) Then
            MsgBox("La fecha indicada no corresponde al ejercicio actual.")
            Return False
        End If
        Cadena = Trim(TxtHora.Text)
        If VerificarHora(Cadena) = False Then
            MsgBox("La hora indicada es incorrecta." + vbCrLf + " (Escriba la fecha en formato hh:mm:ss)")
            Return False
        End If
        TxtHora.Text = Trim(Cadena)
        Return True

    End Function
    Private Function GrabarEntrada() As Boolean
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim trans As SqlClient.SqlTransaction
        Dim Campos() As String, Valores() As String

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try

            'If ModoConexion = 1 Then CnLocal.BeginTrans

            'entradas_albaranes
            Sql = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                MsgBox("Error en el albarán a modificar")
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                Return False
            End If

            RsEntradas!fecha_entrada = CDate(DTFecha.value)
            RsEntradas!hora_entrada = Trim(TxtHora.Text)
            RsEntradas!codigo_periodo = CLng(TxtPeriodo.Text)
            RsEntradas.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "U"
            trans.Commit()
            ObjetoGlobal.cn.Close()
        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Return False
        End Try

        'If ModoConexion = 1 Then CnLocal.CommitTrans
        GrabarEntrada = True
        ReDim Campos(3), Valores(3)
        Campos(0) = "fecha_entrada" : Valores(0) = RsEntradas!fecha_entrada
        Campos(1) = "hora_entrada" : Valores(1) = RsEntradas!hora_entrada
        Campos(2) = "codigo_periodo " : Valores(2) = RsEntradas!codigo_periodo

        oform.AsignarValores(oform.CnTabla01, Campos, Valores)
        RsEntradas.Close()
        MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))

        Return True

    End Function
    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String

        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub

    Private Function VerificarHora(ByRef Hora As String) As Boolean
        Dim j1 As Integer, j2 As Integer
        Dim HH As Integer, MM As Integer, SS As Integer
        Dim HHc As String, MMc As String, SSc As String

        Dim Cadena As String
        j1 = InStr(1, Hora, ":")
        If j1 = 0 Then
            If Len(Hora) = 6 Then
                Hora = Mid(Hora, 1, 2) + ":" + Mid(Hora, 3, 2) + ":" + Mid(Hora, 5, 2)
            ElseIf Len(Hora) = 5 Then
                Hora = "0" + Mid(Hora, 1, 1) + ":" + Mid(Hora, 2, 2) + ":" + Mid(Hora, 4, 2)
            ElseIf Len(Hora) = 4 Then
                Hora = Mid(Hora, 1, 2) + ":" + Mid(Hora, 3, 2) + ":00"
            ElseIf Len(Hora) = 3 Then
                Hora = "0" + Mid(Hora, 1, 1) + ":" + Mid(Hora, 2, 2) + ":00"
            ElseIf Len(Hora) = 2 Then
                Hora = Trim(Hora) + ":00:00"
            ElseIf Len(Hora) = 1 Then
                Hora = "0" + Trim(Hora) + ":00:00"
            End If
            j1 = InStr(1, Hora, ":")
        End If
        If j1 = 0 Or j1 = Len(Hora) Then
            VerificarHora = False
            Exit Function
        End If
        j2 = InStr(j1 + 1, Hora, ":")
        If j2 = 0 Then
            If Len(Hora) = 5 Then
                Hora = Trim(Hora) + ":00"
            ElseIf Len(Hora) = 4 And j1 = 3 Then
                Hora = Mid(Hora, 1, 3) + "0" + Mid(Hora, 4, 1) + ":00"
            ElseIf Len(Hora) = 4 And j1 = 2 Then
                Hora = "0" + Mid(Hora, 1, 4) + ":00"
            End If
        End If
        j2 = InStr(j1 + 1, Hora, ":")
        If j2 = 0 Then
            VerificarHora = False
            Exit Function
        End If
        HHc = Mid(Hora, 1, j1 - 1)
        MMc = Mid(Hora, j1 + 1, j2 - j1 - 1)
        SSc = Mid(Hora, j2 + 1)
        If Not (IsNumeric(HHc)) Or Not (IsNumeric(MMc)) Or Not (IsNumeric(SSc)) Then
            VerificarHora = False
            Exit Function
        End If
        HH = CInt(HHc) : MM = CInt(MMc) : SS = CInt(SSc)
        If HH < 0 Or HH > 23 Then
            VerificarHora = False
            Exit Function
        End If
        If MM < 0 Or MM >= 60 Then
            VerificarHora = False
            Exit Function
        End If
        If SS < 0 Or SS >= 60 Then
            VerificarHora = False
            Exit Function
        End If
        Hora = Format(HH, "00") + ":" + Format(MM, "00") + ":" + Format(SS, "00")
        If Hora = "00:00:00" Then
            VerificarHora = False
            Exit Function
        End If
        VerificarHora = True
    End Function



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

    Private Sub TxtPeriodo_Validating(sender As Object, e As CancelEventArgs) Handles TxtPeriodo.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        LblPeriodoN.Text = ""
        If Trim(TxtPeriodo.Text) > "" And Variedad > "" Then
            Rs.Open("SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(Variedad) + "' AND CODIGO_PERIODO = " + CStr(TxtPeriodo.Text), ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Error. Periodo inexistente.")
                e.Cancel = True
            Else
                LblPeriodoN.Text = Trim(Rs!descripcion_per)
            End If
        End If

    End Sub
End Class