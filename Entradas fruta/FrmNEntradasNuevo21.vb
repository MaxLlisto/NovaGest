Public Class FrmNEntradasNuevo21
    Public ModificaPorcentaje As Boolean
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Public oForm As FrmEntradaAlbaranes
    Public nOldClareta As Double
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub CmdSalir_Click()
        If FlagPreguntar = True Then
            If ModificaPorcentaje = False Then
                If MsgBox("¿Desea salir SIN modificar el porcentaje?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Close()
                End If
            Else
                If MsgBox("El cambio de porcentaje no se realizará a no ser que guarde previamente los cambios ¿continuar?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Close()
                End If
            End If
        Else
            Close()
        End If
    End Sub

    Private Sub Form_Load()
        Dim SQL As String
        Dim nPorc_plaga As Double
        Dim nPorc_clareta As Double

        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado es inexistente")
            CmdSalir_Click()
        End If
        nPorc_plaga = "" & RsEntrada!porcentaje_plaga
        RsEntrada.Close

        SQL = "SELECT * FROM entradas_plagas WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND codigo_plaga = 4"
        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount <> 0 Then
            nPorc_clareta = "" & RsEntrada!porcentaje
            TxtPorcentaje_Plaga.Text = Math.Round(nPorc_plaga * nPorc_clareta / 100, 2)
            nOldClareta = Math.Round(nPorc_plaga * nPorc_clareta / 100, 2)
        Else
            TxtPorcentaje_Plaga.Text = "0"
            nOldClareta = 0
        End If

        Me.KeyPreview = True

    End Sub


    Private Function GrabarEntrada() As Boolean
        Dim i As Integer
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaranClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaranPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CodigoPlaga(15) As Integer
        Dim PorcentajePlaga(15) As Double
        Dim nKg_a_liquidar As Double
        Dim nKg_a_liquidar_nc As Double
        Dim nPorc_Defecto As Double
        Dim nKg_ClaretaAntes As Double
        Dim nKg_ClaretaDespues As Double
        Dim Kg_Defecto As Double
        Dim nKg_comerciales As Double
        Dim nRelacion As Double
        Dim Calidad(10) As Integer
        Dim Kilogramos_calidad(10) As Double
        Dim Kg_Plaga(15) As Double
        Dim Clasificacion(10) As Double
        Dim SQL As String
        Dim nTotalPorcNoClareta As Double
        Dim nResto As Double
        Dim nOrdClareta As Integer
        Dim nTotalClasif As Double
        Dim trans As SqlClient.SqlTransaction
        Dim Campos() As String, Valores() As String

        For i = 1 To 15
            CodigoPlaga(i) = 0
            PorcentajePlaga(i) = 0#
            Kg_Plaga(i) = 0#
        Next

        For i = 1 To 10
            Kilogramos_calidad(i) = 0
            Clasificacion(i) = 0#
            Calidad(i) = 0
        Next

        nTotalClasif = 0
        nKg_a_liquidar_nc = 0
        nTotalPorcNoClareta = 0

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try

            RsAlbaranPlagas.Open("SELECT * FROM ENTRADAS_PLAGAS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " ORDER BY codigo_plaga ", ObjetoGlobal.cn,,,,,,, trans)
            For i = 1 To RsAlbaranPlagas.RecordCount
                RsAlbaranPlagas.AbsolutePosition = i
                CodigoPlaga(i) = RsAlbaranPlagas!codigo_plaga
                PorcentajePlaga(i) = RsAlbaranPlagas!porcentaje
                If RsAlbaranPlagas!codigo_plaga = 4 Then
                    nOrdClareta = i
                End If
            Next i

            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY codigo_calidad", ObjetoGlobal.cn,,,,,,, trans)
            For i = 1 To RsAlbaranClasificacion.RecordCount
                RsAlbaranClasificacion.AbsolutePosition = i
                Kilogramos_calidad(i) = RsAlbaranClasificacion!kg_calidad
                Calidad(i) = RsAlbaranClasificacion!codigo_calidad
                If RsAlbaranClasificacion!codigo_calidad <= 4 Then
                    nKg_comerciales = nKg_comerciales + RsAlbaranClasificacion!kg_calidad
                End If
                If RsAlbaranClasificacion!codigo_calidad = 6 Then
                    nKg_a_liquidar_nc = RsAlbaranClasificacion!kg_calidad
                End If
            Next i


            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn,,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                trans.Rollback()
                MsgBox("Error en el albarán a modificar")
                Return False
            End If
            nPorc_Defecto = RsEntradas!porcentaje_plaga
            nKg_a_liquidar = RsEntradas!kg_a_liquidar

            Kg_Defecto = Math.Round(nKg_a_liquidar * nPorc_Defecto / 100, 2)

            RsEntradas.Close()

            nKg_ClaretaAntes = Math.Round(nKg_a_liquidar * nOldClareta / 100, 2)
            nKg_ClaretaDespues = Math.Round(nKg_a_liquidar * CDbl(TxtPorcentaje_Plaga.Text) / 100, 2)

            nRelacion = (nKg_ClaretaAntes - nKg_ClaretaDespues) / nKg_comerciales

            For i = 1 To RsAlbaranPlagas.RecordCount
                RsAlbaranPlagas.AbsolutePosition = i
                If CodigoPlaga(i) = 4 Then
                    Kg_Plaga(i) = nKg_ClaretaDespues
                Else
                    Kg_Plaga(i) = Math.Round(Kg_Defecto * PorcentajePlaga(i) / 100, 2)
                End If
            Next

            Kg_Defecto = Kg_Defecto - (nKg_ClaretaAntes - nKg_ClaretaDespues)

            nResto = nKg_a_liquidar

            For i = 1 To 4
                Kilogramos_calidad(i) = Math.Round(Kilogramos_calidad(i) + (Kilogramos_calidad(i) * nRelacion), 0)
                nResto = nResto - Kilogramos_calidad(i)
            Next

            Kilogramos_calidad(6) = Math.Round(Kilogramos_calidad(6) - (nKg_ClaretaAntes - nKg_ClaretaDespues), 0)

            nResto = nResto - Kilogramos_calidad(6)
            If nResto <> 0 Then
                Kilogramos_calidad(6) = Kilogramos_calidad(6) + nResto
            End If

            nResto = 100
            For i = 1 To RsAlbaranPlagas.RecordCount
                PorcentajePlaga(i) = Math.Round(Kg_Plaga(i) * 100 / Kg_Defecto, 2)
                nResto = nResto - PorcentajePlaga(i)
            Next
            If nResto <> 0 And RsAlbaranPlagas.RecordCount > 0 Then
                PorcentajePlaga(1) = PorcentajePlaga(1) + nResto
            End If

            nResto = 100
            For i = 1 To RsAlbaranClasificacion.RecordCount
                If i <> 5 Then
                    Clasificacion(i) = Math.Round(Kilogramos_calidad(i) * 100 / nKg_a_liquidar, 2)
                    nResto = nResto - Clasificacion(i)
                End If
            Next
            If nResto <> 0 And RsAlbaranClasificacion.RecordCount > 0 Then
                Clasificacion(1) = Clasificacion(1) + nResto
            End If

            nPorc_Defecto = Math.Round(nPorc_Defecto - (nOldClareta - CDbl(TxtPorcentaje_Plaga.Text)), 2)

            RsAlbaranPlagas.Close()
            RsAlbaranClasificacion.Close()

            ' Cálculos realizados, traspasamos los datos
            RsAlbaranPlagas.Open("SELECT * FROM ENTRADAS_PLAGAS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " ORDER BY codigo_plaga ", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To RsAlbaranPlagas.RecordCount
                RsAlbaranPlagas.AbsolutePosition = i
                RsAlbaranPlagas!porcentaje = PorcentajePlaga(i)
                RsAlbaranPlagas.Update()
            Next i

            RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY codigo_calidad", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To RsAlbaranClasificacion.RecordCount
                RsAlbaranClasificacion.AbsolutePosition = i
                RsAlbaranClasificacion!kg_calidad = Kilogramos_calidad(i)
                RsAlbaranClasificacion!kg_muestra = Clasificacion(i)
                RsAlbaranClasificacion.Update()
            Next i

            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                trans.Rollback()
                MsgBox("Error en el albarán a modificar")
                Return False
            End If
            RsEntradas!porcentaje_plaga = nPorc_Defecto
            RsEntradas.Update()

            trans.Commit()
            ObjetoGlobal.cn.Close()

            ReDim Campos(0), Valores(0)
            Campos(0) = "porcentaje_plaga" : Valores(0) = nPorc_Defecto
            oForm.AsignarValores(oForm.CnTabla01, Campos, Valores)
            MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))

            RsAlbaranClasificacion.Close()
            RsAlbaranPlagas.Close()
            RsEntradas.Close()
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("Error en el albarán a modificar")
            Return False
        End Try

    End Function


    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If GrabarEntrada() Then
            FlagPreguntar = False
            CmdSalir_Click()
        End If
    End Sub
End Class