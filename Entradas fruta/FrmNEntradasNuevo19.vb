Public Class FrmNEntradasNuevo19
    Public ModificaPorcentaje As Boolean
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Public oform As FrmEntradaAlbaranes
    Public nOldCalidad As Double
    Dim LblClasificacion(6) As Label
    Dim lblPorcentaje(6) As Label
    Dim lblEnvase(6) As Label
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub CmdGrabar_Click()
        If Not CmdGrabarModificacion() Then
            MsgBox("No se puedo grabar la modificación de la helada")
        End If
    End Sub
    Private Function CmdGrabarModificacion() As Boolean
        Dim RsAlbaranClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nTotalClasif As Double
        Dim Clasificacion(10) As Double
        Dim Calidad(10) As Integer
        Dim Kilogramos_calidad(10) As Double
        Dim nRestoPor As Double
        Dim nRestoKgs As Double
        Dim SQL As String
        Dim nKg_entrada As Double
        Dim nPorc_Defecto As Double
        Dim MaxPorc_Defecto As Double
        Dim i As Integer
        Dim Campos() As String, Valores() As String
        Dim trans As SqlClient.SqlTransaction


        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try

            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, ,,,,,, trans)
            If RsEntradas.RecordCount > 0 Then
                nKg_entrada = CDbl("0" & RsEntradas!kg_a_liquidar)
                nPorc_Defecto = CDbl("0" & RsEntradas!porcentaje_plaga)
            Else
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                MsgBox("Error, no se encuentra el albarán solicitado")
                Return False
            End If

            RsEntradas.Close()

            If nOldCalidad = 0 And CDbl(TxtPorcentaje_helada.Text) = 0 Then
                ' No hay nada que cambiar
                Return False
            End If

            CmdGrabarModificacion = True

            For i = 1 To 10
                Clasificacion(i) = 0#
                Calidad(i) = 0
            Next
            nTotalClasif = 0

            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY codigo_calidad", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To RsAlbaranClasificacion.RecordCount
                RsAlbaranClasificacion.AbsolutePosition = i
                Kilogramos_calidad(i) = RsAlbaranClasificacion!kg_calidad
                Calidad(i) = RsAlbaranClasificacion!codigo_calidad
            Next i

            ' Restauramos las anteriores
            If nOldCalidad > 0 Then
                For i = 1 To RsAlbaranClasificacion.RecordCount
                    If Calidad(i) = 5 Then
                        Kilogramos_calidad(i) = 0
                    Else
                        Kilogramos_calidad(i) = Math.Round(100 * Kilogramos_calidad(i) / (100 - nOldCalidad), 0)
                    End If
                Next
                nPorc_Defecto = Math.Round(100 * nPorc_Defecto / (100 - nOldCalidad), 2)
            End If

            ' Calculamos las nuevas
            nRestoPor = 100
            nRestoKgs = nKg_entrada

            'If CDbl(TxtPorcentaje_helada.Text) <> 0 Then
            For i = 1 To RsAlbaranClasificacion.RecordCount
                If Calidad(i) = 5 Then
                    Clasificacion(i) = CDbl(TxtPorcentaje_helada.Text)
                    Kilogramos_calidad(i) = Math.Round(nKg_entrada * Clasificacion(i) / 100, 0)
                Else
                    Kilogramos_calidad(i) = Math.Round(Kilogramos_calidad(i) * (1 - (CDbl(TxtPorcentaje_helada.Text) / 100)), 0)
                    Clasificacion(i) = Math.Round(Kilogramos_calidad(i) * 100 / nKg_entrada, 2)
                End If
                If Calidad(i) = 6 Then
                    MaxPorc_Defecto = Clasificacion(i)
                End If
                nRestoPor = nRestoPor - Math.Round(Clasificacion(i), 2)
                nRestoKgs = nRestoKgs - Math.Round(Kilogramos_calidad(i), 0)
            Next

            nRestoPor = Math.Round(nRestoPor, 2)
            nRestoKgs = Math.Round(nRestoKgs, 0)

            nPorc_Defecto = Math.Round(nPorc_Defecto * (1 - (CDbl(TxtPorcentaje_helada.Text) / 100)), 2)

            If MaxPorc_Defecto < nPorc_Defecto Then
                nPorc_Defecto = MaxPorc_Defecto
            End If

            'End If

            If nRestoPor <> 0 Then
                Clasificacion(1) = Clasificacion(1) + nRestoPor
            End If
            If nRestoKgs <> 0 Then
                Kilogramos_calidad(1) = Kilogramos_calidad(1) + nRestoKgs
            End If

            RsAlbaranClasificacion.Close()

            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY codigo_calidad", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To RsAlbaranClasificacion.RecordCount
                RsAlbaranClasificacion.AbsolutePosition = i
                RsAlbaranClasificacion!kg_muestra = Clasificacion(i)
                RsAlbaranClasificacion!kg_calidad = Kilogramos_calidad(i)
                RsAlbaranClasificacion.Update()
            Next
            RsAlbaranClasificacion.Close()

            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount > 0 Then
                RsEntradas!porcentaje_plaga = nPorc_Defecto
                RsEntradas.Update()
            End If
            RsEntradas.Close()

            trans.Commit()
            ObjetoGlobal.cn.Close()

            ReDim Campos(0), Valores(0)
            Campos(0) = "porcentaje_plaga" : Valores(0) = CStr(nPorc_Defecto)
            oform.AsignarValores(oform.CnTabla01, Campos, Valores)
            MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
            Return True

        Catch ex As Exception
            MsgBox("Error grabando albarán " & ex.Message)
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            Return False
        End Try
        Return True

    End Function



    Private Sub AnotarClasificaciones()
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim SQL As String
        Dim Clasificacion(10) As Long
        Dim porcentaje As Double

        'entradas_albaranes
        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            lblDefectos.Text = Format(RsEntradas!porcentaje_plaga, "##0.00")
            RsClasificacion.Open("SELECT ec.*, cv.descripcion FROM ENTRADAS_CLASIF ec JOIN calidades_var_ej cv ON cv.empresa = ec.empresa AND cv.ejercicio=ec.ejercicio AND cv.codigo_variedad = ec.codigo_variedad AND cv.codigo_calidad = ec.codigo_calidad WHERE ec.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ec.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ec.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and ec.NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ec.TIPO_CLASIFICACION = 'LIQ' order by 1,2,3,4,5,6", ObjetoGlobal.cn)
            For i = 1 To RsClasificacion.RecordCount
                RsClasificacion.AbsolutePosition = i
                lblEnvase(i).Text = RsClasificacion!Descripcion
                If Trim(RsClasificacion!codigo_calidad) > 0 And Trim(RsClasificacion!codigo_calidad) <= 9 Then ' ver con Paco
                    If Trim(RsClasificacion!codigo_calidad) = "5" Then
                        nOldCalidad = RsClasificacion!kg_muestra
                    End If
                    Clasificacion(i) = RsClasificacion!kg_calidad
                    porcentaje = 0
                    If oform.CnTabla01.ValorCampo("kg_entrada") <> 0 Then
                        porcentaje = Math.Round(Clasificacion(i) * 100 / oform.CnTabla01.ValorCampo("kg_entrada"), 2)
                    End If
                    LblClasificacion(i).Text = Format(Clasificacion(i), "###,##0")
                    lblPorcentaje(i).Text = Format(porcentaje, "##0.#0")
                End If
            Next
            RsClasificacion.Close()
        Else
            MsgBox("No encuentro el albarán")
        End If
        RsEntradas.Close

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

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir()
    End Sub

    Private Sub FrmNEntradasNuevo19_Load(sender As Object, e As EventArgs) Handles Me.Load

        For i = 1 To 6
            LblClasificacion(i) = DevuelveControl(Me, "LblClasificacion" & Strings.Right("00" & i, 2), Nothing)
            lblPorcentaje(i) = DevuelveControl(Me, "lblPorcentaje" & Strings.Right("00" & i, 2), Nothing)
            lblEnvase(i) = DevuelveControl(Me, "lblEnvase" & Strings.Right("00" & i, 2), Nothing)
        Next
        AnotarClasificaciones()
        TxtPorcentaje_helada.Text = nOldCalidad

    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If Not CmdGrabarModificacion() Then
            MsgBox("No se puedo grabar la modificación de la helada")
        End If
    End Sub
End Class