Imports System.ComponentModel

Public Class FrmNEntradasNuevo01
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim FlagCambioEnCodigo As Boolean
    Dim SocioSeleccionado As Long
    Dim CampoSeleccionado As Long
    Dim VariedadSeleccionada As String
    Dim TarasEnvases(9) As Single
    Dim SerieAlbaran As String
    Dim NumeroAlbaran As Long
    Dim Transportista As Integer
    Dim FlagYaSeleccionado As Boolean
    'Dim CuantosSocios As Long
    Public oForm As Form

    ' Nuevo
    Dim TxtBulto(60) As TextBox
    Dim TxtEnvase(9) As TextBox
    Dim LblEnvase(8) As Label
    Dim labelb(60) As Label

    'Dim ReferenciaCampo() As String ' CodigoSocio (10) + Variedad(10) + CodigoCampo(10)
    'Dim DatosCampo() As String 'Situación(10) + Descripcion(60) + Hanegadas(##.####)(7)+ DESCRIPCION_VARIEDAD (30)
    'Dim CuantosCampos As Long
    'Dim ReferenciaSocio() As String ' Apellidos + Nombre(80) + CodigoSocio(10)

    Private Sub FrmNEntradasNuevo01_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim i As Integer
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        cabeceras()

        ' Ponemos los controles en un array por compatiblidad
        For i = 1 To 60
            TxtBulto(i) = DevuelveControl(Me, "TxtBulto" & Strings.Right("00" & i, 2), GroupBox3)
            Labelb(i) = DevuelveControl(Me, "label0" & Strings.Right("00" & i, 2), GroupBox3)
        Next

        For i = 1 To 60
            Labelb(i).Visible = True
            TxtBulto(i).Visible = True
        Next

        For i = 1 To 9
            TxtEnvase(i) = DevuelveControl(Me, "TxtEnvase" & i, GroupBox1)
        Next
        LblEnvase(1) = LblEnvase01
        LblEnvase(2) = LblEnvase02
        LblEnvase(3) = LblEnvase03
        LblEnvase(4) = LblEnvase04
        LblEnvase(5) = LblEnvase05
        LblEnvase(6) = LblEnvase06
        LblEnvase(7) = LblEnvase07
        LblEnvase(8) = LblEnvase08

        RsEnvases.Open("SELECT * FROM ENTRADAS_ENVASES_U EU JOIN ENVASES E ON E.EMPRESA=EU.EMPRESA AND E.CODIGO_ENVASE = EU.CODIGO_ENVASE WHERE EU.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' ORDER BY EU.EMPRESA,EU.CONTADOR", ObjetoGlobal.cn)
        For i = 1 To RsEnvases.RecordCount
            If i < 9 Then
                RsEnvases.AbsolutePosition = i
                LblEnvase(i).Text = Trim(RsEnvases!codigo_envase)
                TarasEnvases(i) = RsEnvases!peso
            End If
        Next
        For i = 1 To 60
            labelb(i).Visible = True
            TxtBulto(i).Visible = True
        Next
        DtFecha.value = Now.Date
        TxtHora.Text = Format(Now.Date, "hh:mm:ss")
        CampoSeleccionado = 0
        SocioSeleccionado = 0
        VariedadSeleccionada = ""

        funciones.ObjetoGlobal = ObjetoGlobal

        Me.KeyPreview = True

        'MsgBox("ya")

        TxtCapataz.Focus()

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        If MsgBox("¿Desea salir SIN grabar el albarán de entrada?", vbYesNo) = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub TxtCapataz_Validating(sender As Object, e As CancelEventArgs) Handles TxtCapataz.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String

        If Trim(TxtCapataz.Text) = "" Then
            lblCapataz.Text = ""
        Else
            SQL = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtCapataz.Text)
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                lblCapataz.Text = ""
                'MsgBox("Capataz inexistente.")
                Me.ErrorProvider1.SetError(TxtCapataz, "Capataz inexistente")
                e.Cancel = True

            ElseIf IsDBNull(Rs!referencia_gd) Or Strings.Left("" & Rs!referencia_gd, 1) = "-" Then
                lblCapataz.Text = ""
                'MsgBox("No se puede indicar compo capataz un operario no activo")
                Me.ErrorProvider1.SetError(TxtCapataz, "No se puede indicar compo capataz un operario no activo")
                e.Cancel = True
            ElseIf Rs!codigo_operario >= 20000 Then
                lblCapataz.Text = ""
                'MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo capataz")
                Me.ErrorProvider1.SetError(TxtCapataz, "Los códigos de operario superiores a 20000 no se puede indicar compo capataz")
                e.Cancel = True
            Else
                lblCapataz.Text = "" & Trim(Rs!nombre_operario)
            End If
        End If
    End Sub

    Private Sub TxtTransportista_Validating(sender As Object, e As CancelEventArgs) Handles TxtTransportista.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String

        If Trim(TxtTransportista.Text) = "" Then
            LblTransportista.Text = ""
        Else
            SQL = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtTransportista.Text)
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                LblTransportista.Text = ""
                'MsgBox("Capataz inexistente.")
                Me.ErrorProvider1.SetError(TxtTransportista, "Transportista inexistente")
                e.Cancel = True

            ElseIf IsDBNull(Rs!referencia_gd) Or Strings.Left("" & Rs!referencia_gd, 1) = "-" Then
                lblCapataz.Text = ""
                'MsgBox("No se puede indicar compo capataz un operario no activo")
                Me.ErrorProvider1.SetError(TxtCapataz, "No se puede indicar compo transportista un operario no activo")
                e.Cancel = True
            ElseIf Rs!codigo_operario >= 20000 Then
                LblTransportista.Text = ""
                'MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo capataz")
                Me.ErrorProvider1.SetError(TxtCapataz, "Los códigos de operario superiores a 20000 no se puede indicar compo transportistas")
                e.Cancel = True
            Else
                LblTransportista.Text = "" & Trim(Rs!nombre_operario)
            End If
        End If
    End Sub


    Private Sub TxtCodigosocio_Validating(sender As Object, e As CancelEventArgs) Handles TxtCodigosocio.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If FlagCambioEnCodigo Then
            If Trim(TxtCodigosocio.Text) = "" Then
                Txtnombre.Text = ""
                'Me.ErrorProvider1.SetError(TxtCodigosocio, "El código de socio no puede permanecer vacio")
                'e.Cancel = True
            Else
                Rs.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(TxtCodigosocio.Text), ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    Txtnombre.Text = ""
                    Me.ErrorProvider1.SetError(TxtCodigosocio, "El código de socio indicado es incorrecto")
                    e.Cancel = True
                Else
                    Txtnombre.Text = Trim("" & Rs!apellidos_socio) + " " + Trim("" & Rs!nombre_socio)
                End If
                Rs.Close()
            End If
        End If
        If SocioSeleccionado > -1 Then
            BorrarDatos()
        End If

    End Sub
    Private Function ControlAgendayplazo(CodCampo, CodVariedad)
        Dim RsCult As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim plazo As Long
        Dim FechaLimite As Date

        If Strings.Left(CodVariedad, 2) = "01" Or
           Strings.Left(CodVariedad, 2) = "02" Or
           Strings.Left(CodVariedad, 2) = "40" Or
           Strings.Left(CodVariedad, 3) = "291" Or
           Strings.Left(CodVariedad, 3) = "171" Or
           (Strings.Left(CodVariedad, 3) >= "141" And Strings.Left(CodVariedad, 3) <= "144") Or
            Strings.Left(CodVariedad, 2) = "09" Then

            If Trim(CodCampo) > "" And Trim(CodVariedad) > "" Then

                RsCult.Open("SELECT kg_entrados,hectareas,agenda,fecha_plazo_seg FROM CULTIVOS WHERE " &
                " EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND " &
                " EJERCICIO = '" & Trim(ObjetoGlobal.EjercicioActual) & "' AND " &
                " CODIGO_CAMPO = " & CodCampo & " AND " &
                " CODIGO_VARIEDAD = '" & Trim(CodVariedad) & "' ", ObjetoGlobal.cn)
                If RsCult!agenda <> "S" Then
                    MsgBox("No tiene agenda")
                    Return False
                End If
                If Not IsDBNull(RsCult!fecha_plazo_seg) Then
                    If IsDate(RsCult!fecha_plazo_seg) Then
                        If RsCult!fecha_plazo_seg > CDate(DtFecha.value) Then
                            MsgBox("Problemas con el plazo de seguridad")
                            Return False
                        End If
                    End If
                End If
                If Strings.Left(CodVariedad, 2) = "40" Then

                    SQL = "SELECT PARTES_SERVICIOS.SITUACION,PARTES_SERVICIOS.FECHA_FIN_PARTE,PARTES_TRATAMIENTOS.*,TARIFAS_ARTICULO.PLAZO_SEGURIDAD,SERVICIOS.* FROM PARTES_TRATAMIENTOS LEFT JOIN PARTES_SERVICIOS ON PARTES_SERVICIOS.EMPRESA = PARTES_TRATAMIENTOS.EMPRESA AND PARTES_SERVICIOS.CODIGO_PARTE = PARTES_TRATAMIENTOS.CODIGO_PARTE"
                    SQL = Trim(SQL) + " LEFT JOIN TARIFAS_ARTICULO ON PARTES_TRATAMIENTOS.EMPRESA = TARIFAS_ARTICULO.EMPRESA AND PARTES_TRATAMIENTOS.CODIGO_ARTICULO = TARIFAS_ARTICULO.CODIGO_ARTICULO"
                    SQL = Trim(SQL) + " LEFT JOIN SERVICIOS ON PARTES_SERVICIOS.EMPRESA = SERVICIOS.EMPRESA AND PARTES_SERVICIOS.CODIGO_SERVICIO = SERVICIOS.CODIGO_SERVICIO"
                    SQL = Trim(SQL) + " WHERE PARTES_TRATAMIENTOS.EMPRESA = '4' AND SERVICIOS.EMPRESA_SOCIO = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERVICIOS.CODIGO_CAMPO = " + Trim(CodCampo) + " AND SERVICIOS.CODIGO_VARIEDAD = '" + Trim(CodVariedad) + "'"
                    RsPartes.Open(SQL, ObjetoGlobal.cn)
                    If RsPartes.RecordCount > 0 Then
                        RsPartes.MoveFirst()

                        Do While Not RsPartes.EOF
                            If IsDBNull(RsPartes!plazo_seguridad) Then
                                plazo = 0
                            ElseIf Not IsNumeric(RsPartes!plazo_seguridad) Then
                                plazo = 0
                            Else
                                plazo = RsPartes!plazo_seguridad
                            End If
                            If Trim(RsPartes!Situacion) = "R" Or Trim(RsPartes!Situacion) = "A" Then
                                If plazo > 0 Then
                                    MsgBox("El cultivo tiene un parte abierto (Cod.:" + CStr(RsPartes!codigo_parte) + ")")
                                    Return False
                                End If
                            ElseIf Trim(RsPartes!Situacion) = "T" Or Trim(RsPartes!Situacion) = "C" Then
                                If plazo > 0 Then
                                    If IsDBNull(RsPartes!fecha_fin_parte) Then
                                        FechaLimite = DateAndTime.DateAdd("d", plazo, RsPartes!fecha_parte)
                                    Else
                                        FechaLimite = DateAndTime.DateAdd("d", plazo, RsPartes!fecha_fin_parte)
                                    End If
                                    If CDate(DtFecha.value) <= CDate(FechaLimite) Then
                                        MsgBox("El cultivo tiene un parte que incumple plazo de seguridad (Cod.:" + CStr(RsPartes!codigo_parte) + ")")
                                        Return False
                                    End If
                                End If
                            End If
                            RsPartes.MoveNext()
                        Loop
                    End If
                    RsPartes.Close()
                End If
                RsCult.Close()
            End If
        End If
        Return True
    End Function

    Private Sub CmdRecalcularBultos_Click(sender As Object, e As EventArgs) Handles CmdRecalcularBultos.Click
        Dim i As Integer, Cajas As Integer, SumaCajas As Integer, Bultos As Integer, CajasBulto As Integer

        Cajas = 0
        If IsNumeric(TxtTotalCajas.Text) Then
            Cajas = CLng(TxtTotalCajas.Text)
        End If
        Bultos = 0
        If IsNumeric(TxtTotalbultos.Text) Then
            Bultos = CLng(TxtTotalbultos.Text)
        End If
        CajasBulto = 0
        If IsNumeric(TxtCajasBulto.Text) = True Then
            CajasBulto = CLng(TxtCajasBulto.Text)
        End If
        If CajasBulto = 0 And Bultos > 0 Then
            CajasBulto = Cajas / Bultos
        End If

        SumaCajas = 0

        If Cajas > 0 And Bultos > 0 Then
            For i = 1 To Bultos - 1
                If SumaCajas + CajasBulto <= Cajas Then
                    TxtBulto(i).Text = CStr(CajasBulto)
                ElseIf SumaCajas < Cajas Then
                    TxtBulto(i).Text = CStr(Cajas - SumaCajas)
                Else
                    TxtBulto(i).Text = "0"
                End If
                SumaCajas = SumaCajas + CLng(TxtBulto(i).Text)
            Next
            If SumaCajas < Cajas Then
                TxtBulto(Bultos).Text = CStr(Cajas - SumaCajas)
            Else
                TxtBulto(Bultos).Text = "0"
            End If
        End If

    End Sub

    Public Sub CalcularPesos(RehaceBultos As Boolean)
        Dim i As Integer, Peso1 As Single, Peso2 As Single, Peso3 As Single, Cajas As Integer, Bultos As Integer

        If IsNumeric(TxtPesoBrutoCON.Text) Then
            Peso1 = CLng(TxtPesoBrutoCON.Text)
            Peso2 = 0
            Cajas = 0
            Bultos = 0
            For i = 1 To 8
                Peso3 = 0
                If IsNumeric(TxtEnvase(i).Text) Then
                    Peso3 = Math.Round(CDbl(TxtEnvase(i).Text) * CDbl(TarasEnvases(i)), 3)
                    Peso2 = Peso2 + Peso3
                    If Trim(LblEnvase(i).Text) = "CAJA" Or Trim(LblEnvase(i).Text) = "CAZUL" Or Trim(LblEnvase(i).Text) = "CCAFE" Or Trim(LblEnvase(i).Text) = "CCOL" Or Trim(LblEnvase(i).Text) = "CLIRIASP" Or Trim(LblEnvase(i).Text) = "CAJA." Then
                        Cajas = Cajas + CLng(TxtEnvase(i).Text)
                    End If
                    If Trim(LblEnvase(i).Text) = "PALET" Or Trim(LblEnvase(i).Text) = "PV" Or Trim(LblEnvase(i).Text) = "PALET." Then
                        Bultos = Bultos + CLng(TxtEnvase(i).Text)
                    End If
                End If
            Next
            For i = 9 To 9
                If Trim(TxtTipoenvase.Text) = "" Then TxtEnvase(i).Text = ""
                If Trim(TxtTipoenvase.Text) > "" Then
                    If TarasEnvases(i) <> 0 Then
                        If IsNumeric(TxtEnvase(i).Text) Then Peso3 = Math.Round(CDbl(TxtEnvase(i).Text) * CDbl(TarasEnvases(i)), 3)
                        Peso2 = Peso2 + Peso3
                    End If
                End If
                If IsNumeric(TxtEnvase(i).Text) Then
                    If Trim(TxtTipoenvase.Text) = "CAJA" Or Trim(TxtTipoenvase.Text) = "CAZUL" Or Trim(TxtTipoenvase.Text) = "CCAFE" Or Trim(TxtTipoenvase.Text) = "CCOL" Or Trim(TxtTipoenvase.Text) = "CLIRIASP" Or Trim(TxtTipoenvase.Text) = "CAJA." Then
                        Cajas = Cajas + CLng(TxtEnvase(i).Text)
                    End If
                End If
                If Trim(TxtTipoenvase.Text) = "PALET" Or Trim(TxtTipoenvase.Text) = "PV" Or Trim(TxtTipoenvase.Text) = "PALET." Then
                    If IsNumeric(TxtEnvase(i).Text) Then Bultos = Bultos + CLng(TxtEnvase(i).Text)
                End If
            Next
            Peso1 = Math.Round(Peso1 - Peso2, 0)
            TxtPesoBrutoSIN.Text = CStr(Peso1)
            TxtTotalCajas.Text = CStr(Cajas)

            If RehaceBultos = True Then
                TxtTotalbultos.Text = CStr(Bultos)
            End If

        End If
    End Sub

    Private Function VerTramitesPrevios()
        Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim PesoSin As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsOr As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCult As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim plazo As Long
        Dim FechaLimite As Date
        Dim SQL As String
        Dim MensajeError As String = ""


        RsOr.Open("SELECT empresa FROM Ordenes_recoleccion WHERE empresa='" &
                    Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "' AND " &
                    " codigo_campo=" & Trim(CampoSeleccionado) & " AND variedad='" & Trim(VariedadSeleccionada) & "'", ObjetoGlobal.cn)
        If RsOr.RecordCount = 0 Then
            MensajeError = MensajeError + "No tiene orden de recolección" + Chr(13)
        End If
        RsOr.Close()

        ' Agenda y plazo de seguridad
        If Strings.Left(VariedadSeleccionada, 2) = "01" Or Strings.Left(VariedadSeleccionada, 2) = "02" Or Strings.Left(VariedadSeleccionada, 2) = "40" Or Strings.Left(VariedadSeleccionada, 3) = "291" Then
            If Trim(CampoSeleccionado) > "" And Trim(VariedadSeleccionada) > "" Then
                RsCult.Open("SELECT kg_entrados,hectareas,agenda,fecha_plazo_seg FROM CULTIVOS WHERE " &
            " EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND " &
            " EJERCICIO = '" & Trim(ObjetoGlobal.EjercicioActual) & "' AND " &
            " CODIGO_CAMPO = " & CampoSeleccionado & " AND " &
            " CODIGO_VARIEDAD = '" & Trim(VariedadSeleccionada) & "' ", ObjetoGlobal.cn)
                If RsCult!agenda <> "S" Then
                    MensajeError = MensajeError + "No tiene agenda" + Chr(13)
                End If
                If Not IsDBNull(RsCult!fecha_plazo_seg) Then
                    If IsDate(RsCult!fecha_plazo_seg) Then
                        If RsCult!fecha_plazo_seg > CDate(DtFecha.value) Then
                            MensajeError = MensajeError + "Problemas con el plazo de seguridad" + Chr(13)
                        End If
                    End If
                End If
                If Strings.Left(VariedadSeleccionada, 2) = "40" Then
                    SQL = "SELECT PARTES_SERVICIOS.SITUACION,PARTES_SERVICIOS.FECHA_FIN_PARTE,PARTES_TRATAMIENTOS.*,TARIFAS_ARTICULO.PLAZO_SEGURIDAD,SERVICIOS.* FROM PARTES_TRATAMIENTOS LEFT JOIN PARTES_SERVICIOS ON PARTES_SERVICIOS.EMPRESA = PARTES_TRATAMIENTOS.EMPRESA AND PARTES_SERVICIOS.CODIGO_PARTE = PARTES_TRATAMIENTOS.CODIGO_PARTE"
                    SQL = Trim(SQL) + " LEFT JOIN TARIFAS_ARTICULO ON PARTES_TRATAMIENTOS.EMPRESA = TARIFAS_ARTICULO.EMPRESA AND PARTES_TRATAMIENTOS.CODIGO_ARTICULO = TARIFAS_ARTICULO.CODIGO_ARTICULO"
                    SQL = Trim(SQL) + " LEFT JOIN SERVICIOS ON PARTES_SERVICIOS.EMPRESA = SERVICIOS.EMPRESA AND PARTES_SERVICIOS.CODIGO_SERVICIO = SERVICIOS.CODIGO_SERVICIO"
                    SQL = Trim(SQL) + " WHERE PARTES_TRATAMIENTOS.EMPRESA = '4' AND SERVICIOS.EMPRESA_SOCIO = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERVICIOS.CODIGO_CAMPO = " + Trim(CampoSeleccionado) + " AND SERVICIOS.CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'"
                    RsPartes.Open(SQL, ObjetoGlobal.cn)
                    If RsPartes.RecordCount > 0 Then
                        RsPartes.MoveFirst()

                        While Not RsPartes.EOF
                            If IsDBNull(RsPartes!plazo_seguridad) Then
                                plazo = 0
                            ElseIf Not IsNumeric(RsPartes!plazo_seguridad) Then
                                plazo = 0
                            Else
                                plazo = RsPartes!plazo_seguridad
                            End If
                            If Trim(RsPartes!Situacion) = "R" Or Trim(RsPartes!Situacion) = "A" Then
                                If plazo > 0 Then
                                    MensajeError = MensajeError + "El cultivo tiene un parte abierto (Cod.:" + CStr(RsPartes!codigo_parte) + ")" + Chr(13)
                                    VerTramitesPrevios = False
                                End If
                            ElseIf Trim(RsPartes!Situacion) = "T" Or Trim(RsPartes!Situacion) = "C" Then
                                If plazo > 0 Then
                                    If IsDBNull(RsPartes!fecha_fin_parte) Then
                                        FechaLimite = DateAdd("d", plazo, RsPartes!fecha_parte)
                                    Else
                                        FechaLimite = DateAdd("d", plazo, RsPartes!fecha_fin_parte)
                                    End If
                                    If CDate(DtFecha.value) <= CDate(FechaLimite) Then
                                        MensajeError = MensajeError + "El cultivo tiene un parte que incumple plazo de seguridad (Cod.:" + CStr(RsPartes!codigo_parte) + ")" + Chr(13)
                                    End If
                                End If
                            End If
                            RsPartes.MoveNext()
                        End While
                    End If
                    RsPartes.Close()
                End If
                RsCult.Close()
            End If
        End If
        If MensajeError.Trim <> "" Then
            MsgBox(MensajeError)
            Return False
        End If
        Return True
    End Function


    Private Sub LstCampos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LstCampos.KeyPress
        Static cBusqueda As String = ""
        Static nhora As Long
        Dim foundItem As ListViewItem

        If LstCampos.Items.Count = 0 Then
            Return
        End If

        If DateDiff("s", Date.Now, CDate(Format(Date.Now, "dd/MM/yyyy") & " 00:01")) > nhora + 5 Then
            nhora = DateDiff("s", Date.Now, CDate(Format(Date.Now, "dd/MM/yyyy") & " 00:01"))
            cBusqueda = ""
        End If
        cBusqueda = cBusqueda + e.KeyChar

        foundItem = LstCampos.FindItemWithText(cBusqueda, False, 0, True)

        If foundItem IsNot Nothing Then
            LstCampos.TopItem = foundItem
        End If

        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            If (Not lstsocios.SelectedItems Is Nothing) And (Not LstCampos.SelectedItems Is Nothing) Then

                SocioSeleccionado = lstsocios.SelectedItems(0).SubItems(1).Text
                CampoSeleccionado = LstCampos.SelectedItems(0).Text
                VariedadSeleccionada = LstCampos.SelectedItems(0).SubItems(4).Text
                If Not ControlAgendayplazo(CampoSeleccionado, VariedadSeleccionada) Then
                    Exit Sub
                End If

                ActualizarSeleccion()
                VerTramitesPrevios()
                TxtPeriodo.Focus()
            End If
        End If


    End Sub

    Private Sub PoblarListaSocios(Cadena As String)
        Dim j As Long
        Dim P1 As Long
        Dim P2 As Long
        Dim PM As Long
        Dim Aceptable As Boolean
        Dim Primero As Boolean
        Dim Item As ListViewItem

        lstsocios.Items.Clear()
        LstCampos.Items.Clear()
        LblDatosSeleccionado.Text = ""
        LstProceso.Items.Clear()

        If Trim(Cadena) = "" Then
            Return
        End If
        If CuantosSocios = 0 Then
            Return
        End If

        P1 = 0 : P2 = CuantosSocios
        LstProceso.Items.Add("busco: " + Trim(Txtnombre.Text))

        While (P1 <= P2 And Primero = False)
            PM = Fix((P1 + P2) / 2)
            LstProceso.Items.Add(CStr(P1) + " " + CStr(P2) + " " + CStr(PM))
            LstProceso.Items.Add(UCase(ReferenciaSocio(PM)))
            Aceptable = False : Primero = False
            If Comparar(Strings.Left(UCase(ReferenciaSocio(PM)), Len(Cadena)), UCase(Cadena)) = 0 Then
                Aceptable = True
                If PM = 1 Then
                    Primero = True
                Else
                    If Comparar(Strings.Left(UCase(ReferenciaSocio(PM - 1)), Len(Cadena)), UCase(Cadena)) <> 0 Then Primero = True
                End If
            End If
            If Aceptable = True And Primero = True Then
                P1 = PM : P2 = PM
            Else
                If Comparar(Strings.Left(UCase(ReferenciaSocio(PM)), Len(Cadena)), UCase(Cadena)) >= 0 Then
                    P2 = PM - 1
                Else
                    P1 = PM + 1
                End If
            End If
            LstProceso.Items.Add("Aceptable = " + CStr(Aceptable) + " Primero= " + CStr(Primero))
            LstProceso.Items.Add("")
        End While

        LstProceso.Items.Add(CStr(PM) + " PRIMERO = " + CStr(Primero))
        LstProceso.Items.Add(ReferenciaSocio(PM))

        If PM > 1 Then LstProceso.Items.Add(ReferenciaSocio(PM - 1))

        If Primero = True Then
            For j = PM To CuantosSocios
                If Comparar(Strings.Left(UCase(ReferenciaSocio(j)), Len(Cadena)), UCase(Cadena)) = 0 Then
                    'Item = LstSocios.Items.Add(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
                    'Item.SubItems.Add(CStr(CLng(Strings.Mid(ReferenciaSocio(j), 81))))
                    'Item = LstSocios.Items.Add(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
                    Item = New ListViewItem(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
                    Item.SubItems.Add(CStr(CLng(Strings.Mid(ReferenciaSocio(j), 81))))
                    lstsocios.Items.Add(Item)
                Else
                    Exit For
                End If
            Next
            lstsocios.Items(1).Selected = True
            PoblarListaCampos(CLng(Strings.Mid(ReferenciaSocio(PM), 81)), Trim(Strings.Left(ReferenciaSocio(PM), 80)), "")
        End If

    End Sub

    Private Sub Txtnombre_TextChanged(sender As Object, e As EventArgs) Handles Txtnombre.TextChanged

        If Trim(TxtCodigosocio.Text) > "" And FlagCambioEnCodigo = False Then
            TxtCodigosocio.Text = ""
        End If
        PoblarListaSocios(Trim(Txtnombre.Text))
        If SocioSeleccionado > -1 Then
            BorrarDatos()
        End If

    End Sub
    Private Sub BorrarDatos()
        SocioSeleccionado = -1
        CampoSeleccionado = -1
        VariedadSeleccionada = ""
        LblSocioSeleccionado.Text = ""
        LblCampoVariedadSeleccionado.Text = ""
    End Sub

    Private Function Comparar(Cadena1 As String, Cadena2 As String) As Integer
        Dim C1 As String, C2 As String, j1 As Integer, j2 As Integer

        C1 = Cadena1
        j1 = InStr(1, C1, Chr(209))

        Do While j1 > 0
            If j1 = 1 Then
                C1 = "Nz" + Mid(C1, 2)
            ElseIf j1 = Len(C1) Then
                C1 = Strings.Left(C1, j1 - 1) + "Nz"
            Else
                C1 = Strings.Left(C1, j1 - 1) + "Nz" + Strings.Mid(C1, j1 + 1)
            End If
            j1 = InStr(1, C1, Chr(209))
        Loop

        C2 = Cadena2
        j2 = InStr(1, C2, Chr(209))

        Do While j2 > 0
            If j2 = 1 Then
                C2 = "Nz" + Mid(C2, 2)
            ElseIf j2 = Len(C2) Then
                C2 = Strings.Left(C2, j2 - 1) + "Nz"
            Else
                C2 = Strings.Left(C2, j2 - 1) + "Nz" + Strings.Mid(C2, j2 + 1)
            End If
            j2 = InStr(1, C2, Chr(209))
        Loop
        If C1 < C2 Then

            Return -1
        ElseIf C1 = C2 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Private Sub PoblarListaCampos(Socio As Long, Nombre As String, Variedad As String)
        Dim i As Long, j As Long, P1 As Long, P2 As Long, PM As Long, Cadena As String
        Dim Aceptable As Boolean, Primero As Boolean, Item As ListViewItem

        LstCampos.Items.Clear()
        LstProceso.Items.Clear()

        If Socio <= 0 Then Exit Sub

        LblDatosSeleccionado.Text = "Socio:" + CStr(Socio) + " - " + Trim(Nombre)
        P1 = 0
        P2 = CuantosCampos
        Cadena = Format(Socio, "0000000000")
        LstProceso.Items.Add("busco: " + CStr(Socio) + " " + Trim(Variedad))

        Do While (P1 <= P2 And Primero = False)
            PM = Fix((P1 + P2) / 2)
            LstProceso.Items.Add(CStr(P1) + " " + CStr(P2) + " " + CStr(PM))
            LstProceso.Items.Add(UCase(ReferenciaCampo(PM)))
            Aceptable = False : Primero = False
            If Comparar(Strings.Left(UCase(ReferenciaCampo(PM)), 10), UCase(Cadena)) = 0 Then
                Aceptable = True
                If PM = 1 Then
                    Primero = True
                Else
                    If Comparar(Strings.Left(UCase(ReferenciaCampo(PM - 1)), 10), UCase(Cadena)) <> 0 Then Primero = True
                End If
            End If
            If Aceptable = True And Primero = True Then
                P1 = PM : P2 = PM
            Else
                If Comparar(Strings.Left(UCase(ReferenciaCampo(PM)), 10), UCase(Cadena)) >= 0 Then
                    P2 = PM - 1
                Else
                    P1 = PM + 1
                End If
            End If
            LstProceso.Items.Add("Aceptable = " + CStr(Aceptable) + " Primero= " + CStr(Primero))
            LstProceso.Items.Add("")
        Loop

        LstProceso.Items.Add(CStr(PM) + " PRIMERO = " + CStr(Primero))
        LstProceso.Items.Add(ReferenciaCampo(PM))
        If PM > 1 Then
            LstProceso.Items.Add(ReferenciaCampo(PM - 1))
        End If

        If Primero = True Then
            For j = PM To CuantosCampos
                If Comparar(Strings.Left(UCase(ReferenciaCampo(j)), 10), UCase(Cadena)) = 0 Then
                    Item = New ListViewItem(CStr(CLng(Mid(ReferenciaCampo(j), 21))))
                    Item.SubItems.Add(Mid(ReferenciaCampo(j), 11, 10) + " " + Trim(Mid(DatosCampo(j), 78)))
                    Item.SubItems.Add(Trim(Mid(DatosCampo(j), 11, 60)))
                    Item.SubItems.Add(CStr(CDbl(Mid(DatosCampo(j), 71, 7))))
                    Item.SubItems.Add(Trim(Mid(ReferenciaCampo(j), 11, 10)))
                    LstCampos.Items.Add(Item)
                Else
                    Exit For
                End If
            Next
        End If
    End Sub


    Private Sub TxtTipoenvase_Validating(sender As Object, e As CancelEventArgs)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer

        If TxtTipoenvase.Text = "" Then
            TxtEnvase(9).Text = ""
            TarasEnvases(9) = 0
        Else
            Rs.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(TxtTipoenvase.Text) + "'", ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Error. Tipo de envase inexistente.")
                e.Cancel = True
                Return
            End If
            For i = 1 To 8
                If Trim(LblEnvase(i).Text) = Trim(TxtTipoenvase.Text) Then
                    MsgBox("Error. Tipo de envase ya anotado.")
                    e.Cancel = True
                    Return
                End If
            Next

            TarasEnvases(9) = Rs!peso

        End If
        CalcularPesos(True)
    End Sub

    Private Sub ActualizarSeleccion()
        Dim RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String

        RsSocio.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(SocioSeleccionado), ObjetoGlobal.cn)
        If RsSocio.RecordCount = 0 Then
            MsgBox("Socio seleccionado inexistente")
            BorrarDatos()
            Return
        End If
        LblSocioSeleccionado.Text = CStr(RsSocio!codigo_socio) + " - " + Trim("" & RsSocio!apellidos_socio) + ", " + Trim("" & RsSocio!nombre_socio)

        SQL = "SELECT CU.ALTA_BAJA,CU.CODIGO_CAMPO,CA.CODIGO_SOCIO,CA.ALTA_BAJA AS ALTA_BAJA_CAMPO,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD,CU.HANEGADAS"
        SQL = Trim(SQL) + " FROM CULTIVOS CU JOIN CAMPOS CA ON CA.EMPRESA = CU.EMPRESA  AND CA.CODIGO_CAMPO = CU.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = CU.EMPRESA AND V.CODIGO_VARIEDAD = CU.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = CU.EMPRESA AND SC.CODIGO_SITUACION = CA.SITUACION_CAMPO"
        SQL = Trim(SQL) + " WHERE CU.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "'AND CU.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CU.CODIGO_CAMPO = " + CStr(CampoSeleccionado) + " AND CU.CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'"
        RsCampo.Open(SQL, ObjetoGlobal.cn)
        If RsCampo.RecordCount = 0 Then
            MsgBox("Campo seleccionado inexistente")
            BorrarDatos()
            Return
        End If
        If RsCampo!codigo_socio <> SocioSeleccionado Then
            MsgBox("Error en la asociación socio-campo en campo seleccionado.")
            BorrarDatos()
            Return
        End If
        If RsCampo!alta_baja <> "A" Then
            MsgBox("Campo (cultivo) seleccionado en situacion de BAJA.")
            BorrarDatos()
            Return
        End If
        If RsCampo!alta_baja_Campo <> "A" Then
            MsgBox("Campo seleccionado en situacion de BAJA.")
            BorrarDatos()
            Return
        End If

        If Strings.Left(VariedadSeleccionada, 2) = "40" Or Strings.Left(VariedadSeleccionada, 3) = "291" Then
            MsgBox("No olvides marcar el campo fin de recolección.")
        End If

        LblCampoVariedadSeleccionado.Text = CStr(RsCampo!codigo_campo) + " - " + Trim("" & RsCampo!descripcion_situacion) + " (" + Trim("" & RsCampo!Hanegadas) + " Haneg.) " + Trim(VariedadSeleccionada) + " - " + Trim(RsCampo!descripcion_variedad)

        lstsocios.Items.Clear()
        LstCampos.Items.Clear()

    End Sub

    Private Sub TxtPeriodo_Validating(sender As Object, e As CancelEventArgs) Handles TxtPeriodo.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        LblPeriodo.Text = ""
        If Trim(TxtPeriodo.Text) > "" And VariedadSeleccionada > "" Then
            Rs.Open("SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "' AND CODIGO_PERIODO = " + CStr(TxtPeriodo.Text), ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Error. Periodo inexistente.")
                e.Cancel = True
            Else
                LblPeriodo.Text = Trim(Rs!descripcion_per)
            End If
        End If

        TxtEnvase1.Focus()


    End Sub

    Private Sub TxtPesoBrutoCON_Validating(sender As Object, e As CancelEventArgs) Handles TxtPesoBrutoCON.Validating
        CalcularPesos(True)
    End Sub

    Private Sub Txtnombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txtnombre.KeyPress
        FlagCambioEnCodigo = False
    End Sub

    Private Sub TxtCodigosocio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCodigosocio.KeyPress
        FlagCambioEnCodigo = True
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
            TxtTotalbultos.Text = 0
            Return
        End If
        For i = 1 To Cuantos
            labelb(i).Visible = True
            TxtBulto(i).Visible = True
        Next

        For i = Cuantos + 1 To 60
            TxtBulto(i).Text = ""
            labelb(i).Visible = False
            TxtBulto(i).Visible = False
        Next
    End Sub

    Private Function VerificarHora(ByRef Hora As String) As Boolean
        Dim j1 As Integer, j2 As Integer
        Dim HH As Integer, MM As Integer, SS As Integer
        Dim HHc As String, MMc As String, SSc As String

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
        Dim RsRegepa As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsOr As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsMsj As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCult As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Cadena As String

        'Capataz
        If Trim(TxtCapataz.Text) = "" Then
            MsgBox("No se ha indicado capataz.")
            Return False
        End If

        RsOperario.Open("SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtCapataz.Text), ObjetoGlobal.cn)
        If RsOperario.RecordCount = 0 Then
            MsgBox("Capataz inexistente")
            Return False
        ElseIf IsDBNull(RsOperario!referencia_gd) Or Strings.Left("" & RsOperario!referencia_gd, 1) = "-" Then
            MsgBox("No se puede indicar compo capataz un operario no activo")
            Return False
        ElseIf RsOperario!codigo_operario >= 20000 Then
            MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo capataz")
            Return False
        End If
        RsOperario.Close()

        'Transportista
        If Trim(TxtTransportista.Text) = "" Then
            MsgBox("No se ha indicado transportista.")
            Return False
        End If
        RsOperario.Open("SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + Trim(TxtTransportista.Text), ObjetoGlobal.cn)
        If RsOperario.RecordCount = 0 Then
            MsgBox("Transportista inexistente")
            Return False
        ElseIf IsDBNull(RsOperario!referencia_gd) Or Strings.Left("" & RsOperario!referencia_gd, 1) = "-" Then
            MsgBox("No se puede indicar compo transportista un operario no activo")
            Return False
        ElseIf RsOperario!codigo_operario >= 20000 Then
            MsgBox("Los códigos de operario superiores a 20000 no se puede indicar compo transportista")
            Return False
        End If
        RsOperario.Close()

        'Socio
        If SocioSeleccionado <= 0 Then
            MsgBox("No se ha indicado socio.")
            Return False
        End If

        RsSocio.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(SocioSeleccionado), ObjetoGlobal.cn)
        If RsSocio.RecordCount = 0 Then
            MsgBox("Socio inexistente")
            Return False
        End If

        RsRegepa.Open("SELECT * FROM socios_regepa WHERE codigo_socio = " + CStr(SocioSeleccionado) & " AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "'", ObjetoGlobal.cn)
        If RsRegepa.RecordCount = 0 Then
            MsgBox("Este socio no tiene REGEPA anotado en su ficha")
        Else
            If RsRegepa!regepa = "N" Then
                MsgBox("Este socio no tiene REGEPA anotado en su ficha")
            End If
        End If
        RsRegepa.Close()

        If Trim("" & RsSocio!doc1) <> "S" Then
            MsgBox("Este socio no ha firmado la Ley de la cadena alimentaria", MsgBoxStyle.Critical)
        End If

        'Campo
        If CampoSeleccionado <= 0 Then
            MsgBox("No se ha indicado campo.")
            Return False
        End If
        RsCampo.Open("SELECT * FROM CAMPOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CAMPO = " + CStr(CampoSeleccionado), ObjetoGlobal.cn)
        If RsCampo.RecordCount = 0 Then
            MsgBox("Campo inexistente")
            Return False
        End If
        If RsCampo!alta_baja <> "A" Then
            MsgBox("El campo seleccionado no está dado de alta.")
            Return False
        End If
        If RsCampo!codigo_socio <> SocioSeleccionado Then
            MsgBox("El campo seleccionado no corresponde al proveedor indicado.")
            Return False
        End If
        RsCampo.Close()
        'Variedad
        If Trim(VariedadSeleccionada) = "" Then
            MsgBox("No se ha indicado variedad.")
            Return False
        End If
        RsVariedad.Open("SELECT * FROM VARIEDADES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'", ObjetoGlobal.cn)
        If RsVariedad.RecordCount = 0 Then
            MsgBox("Variedad inexistente")
            Return False
        End If
        RsVariedad.Close()
        'Cultivo
        RsCultivo.Open("SELECT * FROM CULTIVOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_CAMPO = " + CStr(CampoSeleccionado) + " AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'", ObjetoGlobal.cn)
        If RsCultivo.RecordCount = 0 Then
            MsgBox("Cultivo inexistente")
            Return False
        Else
            If (RsCultivo!codigo_variedad >= "001" And RsCultivo!codigo_variedad <= "02z") Or Strings.Left(RsCultivo!codigo_variedad, 2) = "17" Or Strings.Left(RsCultivo!codigo_variedad, 2) = "14" Then
                If RsCultivo!kg_previstos = 0 And RsCultivo!kg_asegurados_ca = 0 Then
                    MsgBox("Este cultivo no tiene anotado los kg. previstos ni los asegurados")
                End If
            End If
        End If
        If RsCultivo!alta_baja <> "A" Then
            MsgBox("El cultivo seleccionado no está dado de alta.")
            Return False
        End If
        RsCultivo.Close()
        'Periodo
        If Trim(TxtPeriodo.Text) = "" Then
            MsgBox("No se ha indicado periodo.")
            Return False
        End If
        RsPeriodo.Open("SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_PERIODO = " + Trim(TxtPeriodo.Text) + " AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'", ObjetoGlobal.cn)
        If RsPeriodo.RecordCount = 0 Then
            MsgBox("Periodo inexistente")
            Return False
        End If

        ' Orden de recolección
        RsOr.Open("SELECT empresa FROM Ordenes_recoleccion WHERE empresa='" &
                    Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "' AND " &
                    " codigo_campo=" & Trim(CampoSeleccionado) & " AND variedad='" & Trim(VariedadSeleccionada) & "'", ObjetoGlobal.cn)

        If RsOr.RecordCount = 0 Then
            MsgBox("No tiene orden de recolección")
            Return False
        End If
        RsOr.Close()

        If Not ControlAgendayplazo(CampoSeleccionado, VariedadSeleccionada) Then
            Return False
        End If

        If Strings.Left(VariedadSeleccionada, 2) <> "12" And Strings.Left(VariedadSeleccionada, 2) <> "11" Then ' La almendra y la algarroba no tiene bultos
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

            If Not (IsNumeric(TxtTotalbultos.Text)) And Not ChkID.Checked Then
                MsgBox("No se ha indicado número de bultos.")
                Return False
            End If
            If CInt(TxtTotalbultos.Text) <= 0 And Not ChkID.Checked Then
                MsgBox("No se ha indicado número de bultos.")
                Return False
                Exit Function
            End If
            PesoSin = CLng(TxtPesoBrutoSIN.Text)
            Cajas = CLng(TxtTotalCajas.Text)
            CalcularPesos(False)
            If PesoSin <> CLng(TxtPesoBrutoSIN.Text) Then
                MsgBox("El peso bruto sin envases se ha reclaculado.Compruebe los cambios.")
                Return False
                Exit Function
            End If
            If Cajas <> CLng(TxtTotalCajas.Text) Then
                MsgBox("El número de cajas se ha reclaculado.Compruebe los cambios.")
                Return False
            End If
            Cajas = 0
            For i = 1 To 60
                If IsNumeric("0" & Trim(TxtBulto(i).Text)) Then
                    Cajas = Cajas + CLng("0" & Trim(TxtBulto(i).Text))
                End If
            Next
            If Cajas <> CLng(TxtTotalCajas.Text) And Not ChkID.Checked Then
                MsgBox("El total de cajas no coincide con las indicadas en los bultos.")
                Return False
            End If
        End If
        Cadena = Format(CDate(DtFecha.value), "dd/mm/yyyy")
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

        RsMsj.Open("SELECT * FROM entradas_mensaje WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_campo = " & CampoSeleccionado & " AND codigo_variedad = '" & VariedadSeleccionada & "'", ObjetoGlobal.cn)
        If Not RsMsj.EOF Then
            MsgBox("AVISO: Lavado de cajas.")
        End If
        RsMsj.Close()
        Comprobacion = True
    End Function

    Private Function ObtenerNumeroDeEntrada(Serie As String) As Long
        Dim RsPendientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsContadores As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaranes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSerie As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim Contador As Long
        Dim FlagExiste As Boolean
        Dim FlagCambiado As Boolean
        Dim Mensaje As String
        Dim FlagGrabado As Boolean
        Dim RsError As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        Contador = -1

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try
            Contador = -1
            If Trim(Serie) = "" Then
                SQL = "SELECT * FROM SERIES_ENTRADAS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "'"
                RsSerie.Open(SQL, ObjetoGlobal.cn,,,,,,, trans)
                If RsSerie.RecordCount > 0 Then Serie = "" & Trim(RsSerie!Serie)
                RsSerie.Close()
            End If
            If Trim(Serie) = "" Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                MsgBox("No se puede determinar serie de albaranes para la entrada.")
                Return -1
            End If

            SQL = "SELECT * FROM CONTADORES_PEND WHERE EMPRESA ='" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = 'T' AND NOMBRE = 'ultima_entrada' AND SERIE = '" + Trim(Serie) + "' ORDER BY 1,2,3,4,5"
            RsPendientes.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsPendientes.RecordCount > 0 Then
                While Contador < 0 And Not RsPendientes.EOF
                    If RsPendientes!Contador >= 0 Then Contador = RsPendientes!Contador
                    'If ModoConexion = 1 Then GrabarEnLocal RsPendientes, "D"
                    RsPendientes.Delete()
                End While
            End If
            RsPendientes.Close()
            RsContadores.Open("SELECT * FROM CONTADORES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = 'T' AND NOMBRE = 'ultima_entrada'  AND SERIE = '" + Trim(Serie) + "'", ObjetoGlobal.cn, True,,,,,, trans)
            If RsContadores.RecordCount <= 0 Then
                If Contador < 0 Then Contador = 1
                RsContadores.AddNew()
                RsContadores!empresa = Trim(ObjetoGlobal.EmpresaActual)
                RsContadores!Ejercicio = "T"
                RsContadores!Nombre = "ultima_entrada"
                RsContadores!Serie = Trim(Serie)
                RsContadores!Contador = Contador
                RsContadores.Update()
                'If ModoConexion = 1 Then GrabarEnLocal RsContadores, "I"
            Else
                If Contador < 0 Then Contador = RsContadores!Contador + 1
                RsContadores!Contador = Contador
                RsContadores.Update()
                'If ModoConexion = 1 Then GrabarEnLocal RsContadores, "U"
            End If
            FlagGrabado = True
            FlagExiste = True
            FlagCambiado = False
            Do While FlagExiste = True
                FlagExiste = False
                RsAlbaranes.Open("SELECT EMPRESA FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERIE_ALBARAN= '" + Trim(Serie) + "' AND NUMERO_ALBARAN = " + CStr(Contador), ObjetoGlobal.cn, ,,,,,, trans)
                If RsAlbaranes.RecordCount > 0 Then
                    Contador = Contador + 1
                    FlagExiste = True
                    FlagCambiado = True
                End If
                RsAlbaranes.Close()
            Loop
            If FlagCambiado = True Then
                RsContadores!Contador = Contador
                RsContadores.Update()
                'If ModoConexion = 1 Then GrabarEnLocal RsContadores, "U"
            End If
            RsContadores.Close()
            SerieAlbaran = Trim(Serie)

            trans.Commit()
            ObjetoGlobal.cn.Close()
            'ObtenerNumeroDeEntrada = Contador
            Return Contador

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()

            Mensaje = Trim(Err.Description)
            If Contador > 0 And FlagGrabado = True Then
                SQL = "SELECT * FROM CONTADORES_PEND WHERE 1=0"
                RsError.Open(SQL, ObjetoGlobal.cn, True)
                RsError.AddNew()
                RsError!empresa = Trim(ObjetoGlobal.EmpresaActual)
                RsError!Ejercicio = "T"
                RsError!Nombre = "ultima_entrada"
                RsError!Serie = Trim(Serie)
                RsError!Contador = Contador
                RsError.Update()
                'If ModoConexion = 1 Then GrabarEnLocal RsError, "I"
                RsError.Close()
            End If
            MsgBox("No se puede obtener el contador de albaranes entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(Mensaje))
            Return -1
        End Try

    End Function

    Private Function GrabarEntrada() As Boolean
        Dim Mensaje As String, i As Integer, Cuantos As Long, Envase As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim PesoCampo As Double
        Dim SQL As String
        Dim RsPendiente As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CuantosBultos As Integer
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        GrabarEntrada = False
        SerieAlbaran = ""

        'If Mid(VariedadSeleccionada, 1, 2) = "11" Then
        '    SerieAlbaran = "N1"
        'ElseIf Mid(VariedadSeleccionada, 1, 3) >= "121" And Mid(VariedadSeleccionada, 1, 3) <= "129" Then
        '    SerieAlbaran = "N0"
        'End If

        NumeroAlbaran = ObtenerNumeroDeEntrada(SerieAlbaran)
        If NumeroAlbaran <= -1 Then
            MsgBox("No se puede grabar el albarán.")
            Return False
        End If


        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try
            'entradas_albaranes
            RsEntradas.Open("SELECT * FROM ENTRADAS_ALBARANES WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            RsEntradas.AddNew()
            RsEntradas!empresa = Trim(ObjetoGlobal.EmpresaActual)
            RsEntradas!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
            RsEntradas!serie_albaran = Trim(SerieAlbaran)
            RsEntradas!numero_albaran = NumeroAlbaran
            RsEntradas!fecha_entrada = CDate(DtFecha.value)
            RsEntradas!hora_entrada = Trim(TxtHora.Text)
            RsEntradas!Capataz = CLng(TxtCapataz.Text)
            RsEntradas!Transportista = CLng(TxtTransportista.Text)
            Transportista = CLng(TxtTransportista.Text)
            RsEntradas!codigo_campo = CampoSeleccionado
            RsEntradas!codigo_variedad = VariedadSeleccionada
            RsEntradas!codigo_periodo = CLng(TxtPeriodo.Text)
            RsEntradas!codigo_socio = SocioSeleccionado
            RsEntradas!peso_bruto_fruta = CLng(TxtPesoBrutoCON.Text)
            RsEntradas!bruto_sin_env = CLng(TxtPesoBrutoSIN.Text)
            ' Por terceros
            TipoDeAlbaran = "S" ' Entrada de socios
            RsEntradas!Tipo_entrada = TipoDeAlbaran
            PesoCampo = 0
            If Trim(TxtPesoCampo.Text) > "" Then
                RsEntradas!peso_campo = CLng(TxtPesoCampo.Text)
                PesoCampo = RsEntradas!peso_campo
            End If
            RsEntradas!observaciones = ""
            If ChkID.Checked Then
                RsEntradas!industria_sn = "S"
            Else
                RsEntradas!industria_sn = "N"
            End If
            RsEntradas.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "I"
            RsEntradas.Close()
            'entradas_envases
            RsEntradasEnvases.Open("SELECT * FROM ENTRADAS_ENVASES WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To 9
                Cuantos = 0
                Envase = ""
                If i <= 8 Then
                    If IsNumeric(TxtEnvase(i).Text) And Trim(LblEnvase(i).Text) > "" Then
                        Cuantos = CLng(TxtEnvase(i).Text)
                        Envase = Trim(LblEnvase(i).Text)
                    End If
                Else
                    If IsNumeric(TxtEnvase(i).Text) And Trim(TxtTipoenvase.Text) > "" Then
                        Cuantos = CLng(TxtEnvase(i).Text)
                        Envase = Trim(TxtTipoenvase.Text)
                    End If
                End If
                If Cuantos > 0 Then
                    RsEnvases.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(Envase) + "'", ObjetoGlobal.cn, True,,,,,, trans)
                    If RsEnvases.RecordCount > 0 Then
                        RsEntradasEnvases.AddNew()
                        RsEntradasEnvases!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsEntradasEnvases!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsEntradasEnvases!serie_albaran = Trim(SerieAlbaran)
                        RsEntradasEnvases!numero_albaran = NumeroAlbaran
                        RsEntradasEnvases!codigo_envase = Trim(Envase)
                        RsEntradasEnvases!entrada_salida = "E"
                        RsEntradasEnvases!numero_envases = Cuantos
                        RsEntradasEnvases!tara = RsEnvases!peso
                        RsEntradasEnvases.Update()
                        'If ModoConexion = 1 Then GrabarEnLocal RsEntradasEnvases, "I"
                    End If
                    RsEnvases.Close()
                End If
            Next
            RsEntradasEnvases.Close()
            'entradas_bultos
            If Not ChkID.Checked Then
                CuantosBultos = CInt(TxtTotalbultos.Text)
                RsBultos.Open("SELECT * FROM ENTRADAS_BULTOS WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                'Bultos
                For i = 1 To CuantosBultos
                    RsBultos.AddNew()
                    RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    RsBultos!serie_albaran = Trim(SerieAlbaran)
                    RsBultos!numero_albaran = NumeroAlbaran
                    RsBultos!Bulto = i
                    RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), NumeroAlbaran, i)
                    RsBultos!Cajas = CInt("0" & TxtBulto(i).Text)
                    RsBultos.Update()
                    'If ModoConexion = 1 Then GrabarEnLocal RsBultos, "I"
                Next

                'Cajas de clasificación (2)

                ' Desde aquí no poner en caso de sandia aaaa
                If Trim(VariedadSeleccionada) >= "01" And Trim(VariedadSeleccionada) <= "02z" Then
                    For i = 1 To 2
                        RsBultos.AddNew()
                        RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsBultos!serie_albaran = Trim(SerieAlbaran)
                        RsBultos!numero_albaran = NumeroAlbaran
                        RsBultos!Bulto = 100 + i
                        RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), NumeroAlbaran, 100 + i)
                        RsBultos!Cajas = 1
                        RsBultos.Update()
                        'If ModoConexion = 1 Then GrabarEnLocal RsBultos, "I"
                    Next
                    'Cajas de reclamación (1)
                    For i = 1 To 1
                        RsBultos.AddNew()
                        RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsBultos!serie_albaran = Trim(SerieAlbaran)
                        RsBultos!numero_albaran = NumeroAlbaran
                        RsBultos!Bulto = 110 + i
                        RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), NumeroAlbaran, 110 + i)
                        RsBultos!Cajas = 1
                        RsBultos.Update()
                        'If ModoConexion = 1 Then GrabarEnLocal RsBultos, "I"
                    Next
                    'Cajas de control recogida (2)
                    For i = 1 To 2
                        RsBultos.AddNew()
                        RsBultos!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsBultos!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsBultos!serie_albaran = Trim(SerieAlbaran)
                        RsBultos!numero_albaran = NumeroAlbaran
                        RsBultos!Bulto = 120 + i
                        RsBultos!Referencia = DevuelveEtiqueta(Trim(ObjetoGlobal.EjercicioActual), NumeroAlbaran, 120 + i)
                        RsBultos!Cajas = 1
                        RsBultos.Update()
                        'If ModoConexion = 1 Then GrabarEnLocal RsBultos, "I"
                    Next
                End If
                RsBultos.Close()
            End If

            ' Pregunta si ha terminado la recolección de sandía o coliflor para poner agenda NO
            If Strings.Left(VariedadSeleccionada, 2) = "40" Or Strings.Left(VariedadSeleccionada, 3) = "291" Then
                If CDFR.Checked Then
                    SQL = "SELECT * FROM CULTIVOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO='" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_CAMPO = " + Trim(CampoSeleccionado) + " AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'"
                    RsCultivo.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                    If RsCultivo.RecordCount > 0 Then
                        RsCultivo!agenda = "N"
                        RsCultivo!fecha_entrega = Nothing
                        RsCultivo.Update()
                        RsCultivo.Close()
                    End If
                End If
            End If

            ' Fin de la pregunta
            trans.Commit()
            ObjetoGlobal.cn.Close()

            'If ModoConexion = 1 Then CnLocal.CommitTrans
            GrabarEntrada = True
            MsgBox("Grabada la entrada:" + CStr(NumeroAlbaran))


            ImprimeAlbaran1(SerieAlbaran, NumeroAlbaran, VariedadSeleccionada, Transportista, PesoCampo)
            If Strings.Left(VariedadSeleccionada, 2) <> "12" And Strings.Left(VariedadSeleccionada, 2) <> "11" Then
                ImprimeEtiquetas(SerieAlbaran, NumeroAlbaran)
            End If
            Return True

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            'If ModoConexion = 1 Then CnLocal.RollbackTrans
            If NumeroAlbaran > 0 And Trim(SerieAlbaran) > "" Then
                RsPendiente.Open("SELECT * FROM CONTADORES_PEND WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                RsPendiente.AddNew()
                RsPendiente!empresa = Trim(ObjetoGlobal.EmpresaActual)
                RsPendiente!Ejercicio = "T"
                RsPendiente!Nombre = "ultima_entrada"
                RsPendiente!Serie = Trim(SerieAlbaran)
                RsPendiente!Contador = NumeroAlbaran
                RsPendiente.Update()
                RsPendiente.Close()
                'If ModoConexion = 1 Then GrabarEnLocal RsPendiente, "I"
            End If
            Mensaje = Trim(Err.Description)
            MsgBox("No se puede grabar la entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(Mensaje))
            Return False
        End Try

    End Function
    Private Function cabeceras()

        AlbaranSeleccionado = -1
        'lstAlbaranes.ColumnHeader.Clear
        lstsocios.View = View.Details

        Dim columnHeader1 As ColumnHeader = New ColumnHeader()
        columnHeader1.Text = "Nombre"
        columnHeader1.TextAlign = HorizontalAlignment.Left
        columnHeader1.Width = 400
        lstsocios.Columns.Add(columnHeader1)

        Dim columnHeader2 As ColumnHeader = New ColumnHeader()
        columnHeader2.Text = "Código"
        columnHeader2.TextAlign = HorizontalAlignment.Left
        columnHeader2.Width = 100
        lstsocios.Columns.Add(columnHeader2)
        lstsocios.FullRowSelect = True
        lstsocios.MultiSelect = False


        Dim columnHeader3 As ColumnHeader = New ColumnHeader()
        columnHeader3.Text = "Código"
        columnHeader3.TextAlign = HorizontalAlignment.Left
        columnHeader3.Width = 100
        LstCampos.Columns.Add(columnHeader3)

        Dim columnHeader4 As ColumnHeader = New ColumnHeader()
        columnHeader4.Text = "Variedad"
        columnHeader4.TextAlign = HorizontalAlignment.Left
        columnHeader4.Width = 100
        LstCampos.Columns.Add(columnHeader4)

        Dim columnHeader5 As ColumnHeader = New ColumnHeader()
        columnHeader5.Text = "Situación"
        columnHeader5.TextAlign = HorizontalAlignment.Left
        columnHeader5.Width = 100
        LstCampos.Columns.Add(columnHeader5)

        Dim columnHeader6 As ColumnHeader = New ColumnHeader()
        columnHeader6.Text = "Hanegadas"
        columnHeader6.TextAlign = HorizontalAlignment.Left
        columnHeader6.Width = 100
        LstCampos.Columns.Add(columnHeader6)

        Dim columnHeader7 As ColumnHeader = New ColumnHeader()
        columnHeader7.Text = "CodigoVariedad"
        columnHeader7.TextAlign = HorizontalAlignment.Left
        columnHeader7.Width = 100
        LstCampos.Columns.Add(columnHeader7)
        LstCampos.FullRowSelect = True
        LstCampos.MultiSelect = False
        Return True

    End Function

    Private Sub LstSocios_MouseClick(sender As Object, e As MouseEventArgs) Handles LstSocios.MouseClick

        If Not LstSocios.SelectedItems Is Nothing Then
            PoblarListaCampos(CLng(LstSocios.SelectedItems(0).SubItems(1).Text), LstSocios.SelectedItems(0).Text, "")
        End If

    End Sub


    Private Sub LstSocios_Click(sender As Object, e As EventArgs) Handles LstSocios.Click
        If Not LstSocios.SelectedItems Is Nothing Then
            PoblarListaCampos(CLng(LstSocios.SelectedItems(0).SubItems(1).Text), LstSocios.SelectedItems(0).Text, "")
        End If
    End Sub

    Private Sub LstCampos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstCampos.SelectedIndexChanged

    End Sub

    Private Sub TxtCapataz_TextChanged(sender As Object, e As EventArgs) Handles TxtCapataz.TextChanged

    End Sub

    Private Sub FrmNEntradasNuevo01_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F12 ' Grabar
                If Comprobacion() = True Then
                    GrabarEntrada()
                End If
            Case Keys.F5 ' Capataz
                Dim oFrm As FrmNEntradasNuevo02 = New FrmNEntradasNuevo02
                oFrm.pform = Me
                If oFrm.ShowDialog() = DialogResult.OK Then
                    TxtCapataz.Text = oFrm.OperarioSeleccionado
                End If
            Case Keys.F6 ' transportista
                Dim oFrm As FrmNEntradasNuevo02 = New FrmNEntradasNuevo02
                oFrm.pform = Me
                If oFrm.ShowDialog() = DialogResult.OK Then
                    TxtTransportista.Text = oFrm.OperarioSeleccionado
                End If
            Case Keys.F7 ' periodo
                Dim oFrm As FrmNEntradasNuevo03 = New FrmNEntradasNuevo03
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                If VariedadSeleccionada.Trim <> "" Then
                    oFrm.Variedad = Trim(VariedadSeleccionada)
                End If
                If oFrm.ShowDialog() = DialogResult.OK Then
                    TxtPeriodo.Text = oFrm.PeriodoSeleccionado
                End If
            Case Keys.F8 ' Envase
                Dim oFrm As FrmNEntradasNuevo04 = New FrmNEntradasNuevo04
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                If oFrm.ShowDialog() = DialogResult.OK Then
                    If Trim(TxtTipoenvase.Text) = "" Then
                        TxtTipoenvase.Text = Trim(EnvaseSeleccionado)
                        TxtEnvase(9).Focus()
                    End If
                End If
        End Select

    End Sub

    Private Sub LstSocios_KeyDown(sender As Object, e As KeyEventArgs) Handles LstSocios.KeyDown

        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Right Then
            If LstCampos.Items.Count = 0 Then
                Return
            End If
            LstCampos.Focus()
            LstCampos.FocusedItem = LstCampos.Items(0)
            LstCampos.Items(0).Selected = True
            LstCampos.Items(0).Focused = True
        ElseIf e.KeyCode = Keys.Tab Then
            If LstCampos.Items.Count = 0 Then
                Return
            End If
            LstCampos.Focus()
            LstCampos.FocusedItem = LstCampos.Items(0)
            LstCampos.Items(0).Selected = True
            LstCampos.Items(0).Focused = True
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.Home Then
            Actualizarcampos()
        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.End Then
            Actualizarcampos()
        End If

    End Sub

    Private Sub LstSocios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstSocios.SelectedIndexChanged
        Actualizarcampos()
    End Sub

    Private Sub Actualizarcampos()
        If lstsocios.Items.Count > 0 Then
            If Not LstSocios.SelectedItems Is Nothing And LstSocios.SelectedItems.Count > 0 Then
                'PoblarListaCampos(CLng(LstSocios.Items(1).SubItems(1)), LstSocios.Items(1), "")
                PoblarListaCampos(CLng(LstSocios.SelectedItems(0).SubItems(1).Text), LstSocios.SelectedItems(0).Text, "")
            End If
        End If

    End Sub

    Private Sub LstSocios_GotFocus(sender As Object, e As EventArgs) Handles LstSocios.GotFocus
        Actualizarcampos()
    End Sub

    Private Sub LstSocios_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LstSocios.ItemSelectionChanged
        Actualizarcampos()
    End Sub

    Private Sub TxtHora_Validated(sender As Object, e As EventArgs) Handles TxtHora.Validated
        If LstSocios.Items.Count = 0 Then
            Return
        End If
        LstSocios.FocusedItem = LstSocios.Items(0)
    End Sub

    Private Sub TxtPeriodo_TextChanged(sender As Object, e As EventArgs) Handles TxtPeriodo.TextChanged

    End Sub

    Private Sub TxtEnvase1_TextChanged(sender As Object, e As EventArgs) Handles TxtEnvase1.TextChanged

    End Sub

    Private Sub TxtEnvase9_TextChanged(sender As Object, e As EventArgs) Handles TxtEnvase9.TextChanged

    End Sub

    Private Sub TxtEnvase9_Validating(sender As Object, e As CancelEventArgs) Handles TxtEnvase9.Validating
        TxtPesoCampo.Focus()
    End Sub

    Private Sub Txtnombre_KeyDown(sender As Object, e As KeyEventArgs) Handles Txtnombre.KeyDown

        If e.KeyCode = Keys.Down Then
            If LstSocios.Items.Count = 0 Then
                Return
            End If
            LstSocios.Focus()
            LstSocios.FocusedItem = LstSocios.Items(0)
            LstSocios.Items(0).Selected = True
            LstSocios.Items(0).Focused = True
        End If

    End Sub

    Private Sub TxtTransportista_TextChanged(sender As Object, e As EventArgs) Handles TxtTransportista.TextChanged

    End Sub
End Class