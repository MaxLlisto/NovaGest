Imports ob
Public Class FrutaTrazab
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim MinAlbaran As Long
    Dim MaxAlbaran As Long
    Dim aDefectos() As Double
    Dim aTitulos() As String
    Dim DictNCAlbaran As New Dictionary(Of String, Integer)
    Dim cAlbaranesVolcados As String
    Dim Cargadedatos As Boolean = False


    Public Sub New()
        Dim aCal(0) As Array
        Dim aCab(0) As String
        Dim rs As New cnRecordset.CnRecordset
        Dim fechadehoy As String
        Dim SQL As String
        Dim EjercicioActual As String

        fechadehoy = Format(DateTime.Today, "dd/MM/yyyy")

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
        If ObjetoGlobal.Inicializar("") = False Then
            MsgBox("No se puede abrir la conexión a la BD")
        End If

        SQL = "SELECT dbo.fn_que_ejercicio('" & fechadehoy & "') as ejercicio"
        If rs.Open(SQL, ObjetoGlobal.cn) Then
            EjercicioActual = rs("ejercicio").ToString.Trim
        End If

        ObjetoGlobal.EjercicioActual = EjercicioActual

        CabeceraVolcados()
        CabeceraFabricados(aCal)
        CabeceraSegunda()
        CabeceraTrazabilidad()
        CabeceraTrazaSegunda()
        CabeceraRestos()
        CabeceraSeguimiento()
        CabeceraFabricadoCestas()
        CabeceraVolcadoCestas()
        CabeceraPrecalibrado()
        CabeceraDefectosLinea(aCab)
        CabeceraPalotsDestrio()
        OpcionVolcados.Checked = True

    End Sub



    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click

        If OpVolcado.Checked Then
            ExportarDatosExcel(DGVolcado, "Volcados")
        ElseIf OpFabricado.Checked Then
            ExportarDatosExcel(DGFabricados, "palets fabricados")
        ElseIf OpSegunda.Checked Then
            ExportarDatosExcel(DGPaletsSegunda, "Palets segunda")
        ElseIf OpVolcadoCestas.Checked Then
            ExportarDatosExcel(DGVolcadosCestas, "Volcado cestas")
        ElseIf OpFabricadoCestas.Checked Then
            ExportarDatosExcel(DGFabricadoCestas, "Cestas fabricadas")
        ElseIf OpPrecalibrado.Checked Then
            ExportarDatosExcel(DGPrecalibrados, "Precalibrado")
        ElseIf OpSeguimiento.Checked Then
            ExportarDatosExcel(DGSeguimiento, "Seguimiento campaña")
        ElseIf Opdefectos.Checked Then
            ExportarDatosExcel(DSDefectosLinea, "Defectos de albaranes")
        ElseIf OpcPalotDestrio.Checked Then
            ExportarDatosExcel(PalotsDetrio, "Palots destrio")
        ElseIf opRestos.Checked Then
            ExportarDatosExcel(DGView, "Restos")
        Else
            ExportarDatosExcel(DGTrazabilidad, "Trazabilidad")
        End If

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        'Dim Msg As MsgBoxResult
        'Msg = MsgBox("Cerrar el modulo, ¿Desea salir?", vbYesNo, "Salir del Modulo")
        Me.Close()

        'If Msg = MsgBoxResult.Yes Then
        '    Application.ExitThread()
        'Else
        '    Exit Sub
        'End If
    End Sub

    Private Sub BtBuscar_Click(sender As Object, e As EventArgs) Handles BtBuscar.Click
        Dim nOrd As Integer
        Dim aCal(0) As Array
        Dim aCad(0) As String

        If OpcionVolcados.Checked Then
            nOrd = 0
        ElseIf OpcionVolcados1.Checked Then
            nOrd = 1
        ElseIf OpcionVolcados2.Checked Then
            nOrd = 2
        Else
            nOrd = 3
        End If

        If nOrd > 0 And TxtVariedad.Text.Trim.Length = 0 Then
            nOrd = 0
        End If

        'espera.Visible = True

        CabeceraVolcados()
        CabeceraFabricados(aCal)
        CabeceraSegunda()
        CabeceraTrazabilidad()
        CabeceraSeguimiento()
        CabeceraFabricadoCestas()
        CabeceraRestos()
        CabeceraVolcadoCestas()
        CabeceraPrecalibrado()
        CabeceraDefectosLinea(aCad)
        CabeceraPalotsDestrio()

        MostrarVolcados(nOrd)
        MostrarFabricados(nOrd)
        MostrarSegunda(nOrd)
        MostrarCestasVolcadas(nOrd)
        Mostrarrestos()
        MostrarCestasFabricadas(nOrd)
        MostrarPrecalibrados(nOrd)
        MostrarPalotsDestrio(nOrd)
        If TxtVariedad.Text.Trim() <> "" Then
            Mostrarseguimiento(nOrd)
            MostrarDefectosLinea()
            'trazabilidad2()
        End If

        ' espera.Visible = False

    End Sub
    Private Sub MostrarVolcados(nOrd)
        Dim Sql As String, SQL2 As String, Calidades(12) As String, i As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim CuantasCalidades As Integer, Cadena As String
        Dim column As DataGridViewColumn

        cpb.Visible = True
        cpb.Maximum = 10000
        cpb.Minimum = 0
        cpb.ResetText()
        cpb.Text = "Calculando..."
        cpb.Value = 0
        cpb.Refresh()
        cpb.Show()


        Sql = "SELECT t.lote, t.hora_volcado, t.numero_albaran, t.bulto, e.codigo_variedad,"
        Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE (select CAST(ISNULL(SUM(kg_calidad),0) AS INTEGER) from entradas_clasif_bt b"
        Sql = Trim(Sql) + " WHERE b.empresa = t.empresa AND b.ejercicio= t.ejercicio AND b.serie_albaran = t.serie_albaran AND b.numero_albaran = t.numero_albaran AND b.bulto = t.bulto AND b.tipo_clasificacion = 'PRO') END,"
        'Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE (select CAST(ISNULL(dbo.fn_peso_bulto(t.empresa,t.ejercicio,t.serie_albaran,t.numero_albaran,t.bulto),0) AS INTEGER)) END,"
        Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE '0' END,"
        Sql = Trim(Sql) + " t.codigo_barras,"
        Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then 'E' ELSE '' END,"

        If Trim(TxtVariedad.Text) > "" And (nOrd = 1 Or nOrd = 2) Then

            SQL2 = "SELECT * from calidades_var_ej WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "'"
            SQL2 = Trim(SQL2) + " AND codigo_variedad = '" + Trim(TxtVariedad.Text) + "'"
            Rs.Open(SQL2, ObjetoGlobal.cn)
            For i = 1 To Rs.RecordCount
                If i <= 12 Then
                    Rs.AbsolutePosition = i
                    Calidades(i) = Trim(Rs!descripcion_abrev)
                End If
            Next
            CuantasCalidades = Rs.RecordCount
            If CuantasCalidades > 12 Then CuantasCalidades = 12
            Rs.Close()

            Cadena = ""
            For i = 1 To 12
                If i <= CuantasCalidades Then
                    Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE (select CAST(ISNULL(SUM(kg_calidad),0) AS INTEGER) from entradas_clasif_bt b"
                    Sql = Trim(Sql) + " WHERE b.empresa = t.empresa AND b.ejercicio= t.ejercicio AND b.serie_albaran = t.serie_albaran AND b.numero_albaran = t.numero_albaran AND b.bulto = t.bulto AND b.tipo_clasificacion = 'PRO' AND codigo_calidad = " + CStr(i) + " ) END"
                Else
                    Sql = Trim(Sql) + "0"
                End If
                If i < 12 Then Sql = Trim(Sql) + ","
            Next
        Else
            Sql = Trim(Sql) + " 0,0,0,0,0,0,0,0,0,0,0,0"
        End If
        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " , c.eurep, s.grasp, t.fecha_volcado, t.empresa, t.ejercicio, t.serie_albaran FROM entradas_lotes_test t "
        Else
            Sql = Trim(Sql) + " , c.eurep, s.grasp, t.fecha_volcado, t.empresa, t.ejercicio, t.serie_albaran FROM entradas_lotes_test t "
        End If

        Sql = Trim(Sql) + " LEFT JOIN entradas_albaranes e ON e.empresa=t.empresa and e.ejercicio = t.ejercicio and e.serie_albaran = t.serie_albaran and t.numero_albaran = e.numero_albaran"
        Sql = Trim(Sql) + " LEFT JOIN cultivos c ON c.empresa=t.empresa and c.ejercicio = t.ejercicio and c.codigo_campo = e.codigo_campo and c.codigo_variedad = e.codigo_variedad "
        Sql = Trim(Sql) + " LEFT JOIN socios_coop s ON s.codigo_socio=e.codigo_socio "
        Sql = Trim(Sql) + " WHERE t.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND t.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "'"
        If nOrd = 3 Then
            Sql = Trim(Sql) + " AND t.numero_albaran is NULL"
        Else
            If Trim(TxtVariedad.Text) > "" Then
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " AND e.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
                ElseIf nOrd = 2 = True Then
                    Sql = Trim(Sql) + " AND e.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
                End If
            End If
        End If
        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        Else
            Sql = Trim(Sql) + " And t.fecha_volcado='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"
        End If
        Sql = Trim(Sql) + " and not t.numero_albaran is null "
        Sql = Trim(Sql) + " UNION "
        Sql = Trim(Sql) + " Select t.lote, t.hora_volcado, 0, 1, pp.codigo_variedad, t.kg_calibrados, t.kg_calibrados, pp.codigo_palet, '', "
        Sql = Trim(Sql) + " kg_01, kg_02,kg_03,kg_04,kg_05,kg_06,kg_07,kg_08,kg_09,kg_10,kg_11,0, '','', '', '','','' "
        Sql = Trim(Sql) + " From entradas_lotes_test t Left join palets_precal_fru pp on pp.empresa = t.empresa And pp.ejercicio = t.ejercicio And pp.codigo_palet = t.codigo_barras and pp.fecha_volcado = t.fecha_volcado"
        Sql = Trim(Sql) + " WHERE t.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND t.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' "
        If Trim(TxtVariedad.Text) > "" Then
            If nOrd = 1 Then
                Sql = Trim(Sql) + " And pp.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
            ElseIf nOrd = 2 = True Then
                Sql = Trim(Sql) + " And pp.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
            End If
        End If
        Sql = Trim(Sql) + " AND t.numero_albaran is NULL  "
        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        Else
            Sql = Trim(Sql) + " And t.fecha_volcado='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"
        End If
        Sql = Trim(Sql) + " and t.numero_albaran is null "
        Sql = Trim(Sql) + " UNION "
        Sql = Trim(Sql) + " Select t.lote, t.hora_volcado, 0, 1, ps.codigo_variedad, t.kg_calibrados, t.kg_calibrados, ps.codigo_barras, '', "
        Sql = Trim(Sql) + " kg_01, kg_02,kg_03,kg_04,kg_05,kg_06,kg_07,kg_08,kg_09,kg_10,kg_11,0, '','', '', '','','' "
        Sql = Trim(Sql) + " From entradas_lotes_test t Left join palets_segunda ps on ps.ejercicio = t.ejercicio And ps.codigo_barras = t.codigo_barras and ps.fecha_volcado = t.fecha_volcado"
        Sql = Trim(Sql) + " WHERE t.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND t.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' "
        If Trim(TxtVariedad.Text) > "" Then
            If nOrd = 1 Then
                Sql = Trim(Sql) + " And ps.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
            ElseIf nOrd = 2 = True Then
                Sql = Trim(Sql) + " And ps.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
            End If
        End If
        Sql = Trim(Sql) + " AND t.numero_albaran is NULL  "
        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        Else
            Sql = Trim(Sql) + " And t.fecha_volcado='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"
        End If
        Sql = Trim(Sql) + " and t.numero_albaran is null "
        Sql = Trim(Sql) + " order by 1"

        If Trim(TxtVariedad.Text) > "" And (nOrd = 1 Or nOrd = 2) Then
            For i = 1 To CuantasCalidades
                column = New DataGridViewTextBoxColumn()
                column.Name = Calidades(i)
                column.HeaderText = Calidades(i)
                column.Width = 100
                DGVolcado().Columns.Add(column)
            Next
            For i = CuantasCalidades + 1 To 12
                column = New DataGridViewTextBoxColumn()
                column.Name = ""
                column.HeaderText =
                column.Width = 0
                DGVolcado().Columns.Add(column)
            Next
        Else
            For i = 1 To 12
                column = New DataGridViewTextBoxColumn()
                column.Name = ""
                column.HeaderText =
                column.Width = 0
                DGVolcado().Columns.Add(column)
            Next
        End If
        column = New DataGridViewTextBoxColumn()
        column.Name = "eurep_cs"
        column.HeaderText = "G.GAP"
        column.Width = 100
        DGVolcado().Columns.Add(column)

        column = New DataGridViewTextBoxColumn()
        column.Name = "grasp"
        column.HeaderText = "GRASP"
        column.Width = 100
        DGVolcado().Columns.Add(column)

        'If DPFechaVolcado.Value < DPHastaFecha.Value Then
        column = New DataGridViewTextBoxColumn()
        column.Name = "Fechav"
        column.HeaderText = "Fecha volcado"
        column.Width = 100
        DGVolcado().Columns.Add(column)
        'End If

        column = New DataGridViewTextBoxColumn()
        column.Name = "control"
        column.HeaderText = "Cr. defec."
        column.Width = 100
        DGVolcado().Columns.Add(column)

        CargarVolcados(Sql)

    End Sub


    Private Sub MostrarFabricados(nOrd)
        Dim Sql As String

        Sql = "SELECT p.codigo_palet, peso_palet = CASE WHEN isnull(p.peso_real,0) > 0 THEN p.peso_real ELSE isnull(o.peso_palet,0) END, p.hora_confeccion as hora, o.codigo_variedad, (rtrim(o.codigo_confeccion) + ' - ' + co.descripcion ) as desconfeccion, (rtrim(o.paletizacion) + ' - ' + pa.descripcion ) as despaletizacion, (CONVERT(varchar(10), o.codigo_cliente) + ' - ' + c.razon_social ) as descliente, o.codigo_destinatario, o.calibre_tecnico, o.calibre_comercial, p.fecha_confeccion AS fecha, p.orden_confeccion, p.tipo_fabricacion FROM palets AS p LEFT JOIN ordenes_confeccion o "
        Sql = Sql + " ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion LEFT JOIN variedades v ON v.empresa=p.empresa AND v.codigo_variedad = o.codigo_variedad "
        Sql = Sql + " LEFT JOIN clientes_coop c ON c.codigo_cliente = o.codigo_cliente "
        Sql = Sql + " LEFT JOIN confeccion co ON co.empresa = p.empresa and co.codigo_confeccion = o.codigo_confeccion "
        Sql = Sql + " LEFT JOIN confeccion pa ON pa.empresa = p.empresa and pa.codigo_confeccion = o.paletizacion "

        If nOrd = 0 Then
            Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND v.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT', 'ALBA') "
        Else
            If Trim(TxtVariedad.Text) > "" Then
                Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'"
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " AND (o.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                ElseIf nOrd = 2 Then
                    Sql = Trim(Sql) + " AND (o.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                End If
            Else
                Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND v.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT', 'ALBA') "
            End If
        End If


        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND p.fecha_confeccion BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "' AND linea_fabricacion IS NULL"
        Else
            Sql = Trim(Sql) + " AND p.fecha_confeccion='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND (linea_fabricacion IS NULL OR linea_fabricacion='')"
        End If

        Sql = Sql + " UNION "
        Sql = Sql + " SELECT CONVERT(varchar(12), pp.codigo_palet), peso_palet = CASE WHEN isnull(pp.kgs,0) > 0 THEN (pp.bultos * pp.kgs) WHEN left(pp.codigo_variedad, 2) = '17' then (pp.bultos * 12)
                                                                                 WHEN left(pp.codigo_variedad, 2) = '14' then (pp.bultos * 18) WHEN left(pp.codigo_variedad, 2) = '09' then (pp.bultos * 6)
                                                                                 ELSE (pp.bultos * 12) END, pp.hora_confeccion as hora, pp.codigo_variedad,  '' as desconfeccion, '' as despaletizacion, '' as descliente, 0 as codigo_destinatario, pp.codigo_calibre as calibre_tecnico, '' as calibre_comercial, pp.fecha_confeccion as fecha, 0, 'N' AS tipo_fabricacion "
        Sql = Sql + " FROM palets_precal_fru pp "
        Sql = Sql + " LEFT JOIN variedades vv ON vv.empresa=pp.empresa AND vv.codigo_variedad = pp.codigo_variedad "

        If nOrd = 0 = True Then
            Sql = Sql + " WHERE pp.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND vv.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT', 'ALBA') "
        Else
            If Trim(TxtVariedad.Text) > "" Then
                Sql = Sql + " WHERE pp.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'"
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " AND (pp.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                ElseIf nOrd = 2 Then
                    Sql = Trim(Sql) + " AND (pp.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                End If
            Else
                Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND v.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT', 'ALBA') "
            End If
        End If
        'Sql = Trim(Sql) + " AND pp.fecha_confeccion='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"

        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND pp.fecha_confeccion BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        Else
            Sql = Trim(Sql) + " And pp.fecha_confeccion='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"
        End If

        '' Volcado de segunda
        'Sql = Sql + " UNION "
        'Sql = Sql + " SELECT CONVERT(varchar(12), ps.codigo_palet), (ps.bultos * 19) as peso_palet, ps.hora_confeccion as hora, ps.codigo_variedad,  '' as desconfeccion, '' as despaletizacion, '' as descliente, 0 as codigo_destinatario, 'seg' as calibre_tecnico, '' as calibre_comercial, ps.fecha_confeccion as fecha, 0 "
        'Sql = Sql + " FROM palets_segunda ps "
        'Sql = Sql + " LEFT JOIN variedades vs ON vs.empresa= '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND vs.codigo_variedad = ps.codigo_variedad "

        'If nOrd = 0 = True Then
        '    Sql = Sql + " WHERE ps.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND vs.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT', 'ALBA') "
        'Else
        '    If Trim(TxtVariedad.Text) > "" Then
        '        Sql = Sql + " WHERE ps.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'"
        '        If nOrd = 1 Then
        '            Sql = Trim(Sql) + " AND (ps.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
        '        ElseIf nOrd = 2 Then
        '            Sql = Trim(Sql) + " AND (ps.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
        '        End If
        '    Else
        '        Sql = Sql + " WHERE ps.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND vs.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT', 'ALBA') "
        '    End If
        'End If
        ''Sql = Trim(Sql) + " AND pp.fecha_confeccion='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"

        'If DPFechaVolcado.Value < DPHastaFecha.Value Then
        '    Sql = Trim(Sql) + " AND ps.fecha_confeccion BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        'Else
        '    Sql = Trim(Sql) + " And ps.fecha_confeccion='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"
        'End If

        '' Fin del volcado de segunda



        Sql = Trim(Sql) + " ORDER BY fecha, hora"

        'CancelarRestos(Sql)
        CargarFabricados(Sql)

    End Sub
    Private Sub MostrarCestasVolcadas(nOrd)
        Dim Sql As String

        Sql = Sql + " SELECT CONVERT(varchar(12), pp.codigo_palet), (pp.bultos * 14) as peso_palet, pp.hora_confeccion, pp.codigo_variedad,  '' as desconfeccion, '' as despaletizacion, '' as descliente, 0 as codigo_destinatario, '' as calibre_tecnico, pp.codigo_calibre as calibre_comercial "
        Sql = Sql + " FROM palets_precal_fru pp "
        Sql = Sql + " LEFT JOIN variedades vv ON vv.empresa=pp.empresa AND vv.codigo_variedad = pp.codigo_variedad "

        If nOrd = 0 Then
            Sql = Sql + " WHERE pp.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND vv.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT') "
        Else
            If Trim(TxtVariedad.Text) > "" Then
                Sql = Sql + " WHERE pp.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'"
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " AND (pp.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                ElseIf nOrd = 2 Then
                    Sql = Trim(Sql) + " AND (pp.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                End If
            End If
        End If

        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND pp.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy 00:00:01") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Else
            Sql = Trim(Sql) + " AND pp.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy 00:00:01") + "' AND '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy 23:59:59") + "'"
        End If

        'Sql = Trim(Sql) + " AND pp.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Trim(Sql) + " ORDER BY 3"


        CargarCestasVolcadas(Sql)

    End Sub

    Private Sub MostrarPrecalibrados(nOrd)
        Dim Sql As String
        Dim desdefecha As Date

        Sql = " SELECT p.codigo_palet, p.referencia, p.codigo_variedad, p.codigo_calibre,  p.fecha_confeccion, p.hora_confeccion, p.salida, p.bultos, p.calidad, isnull(p.kgs,0) as peso "
        Sql = Sql + " FROM palets_precal_fru p "
        Sql = Sql + " LEFT JOIN variedades v ON v.empresa=p.empresa AND v.codigo_variedad = p.codigo_variedad "

        If nOrd = 0 Then
            Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND v.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT') "
        Else
            If Trim(TxtVariedad.Text) > "" Then
                Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'"
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " AND (p.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                ElseIf nOrd = 2 Then
                    Sql = Trim(Sql) + " AND (p.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                End If
            End If
        End If

        desdefecha = DPFechaVolcado.Value.AddDays(-1 * dias.Value)

        Sql = Trim(Sql) + " AND p.fecha_confeccion BETWEEN '" + Format(CDate(desdefecha), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "' AND SITUACION = 'N'"

        If rbPendientes.Checked Then
            Sql = Sql + " AND NOT EXISTS (SELECT lote FROM entradas_lotes_test elt WHERE elt.empresa = p.empresa AND elt.ejercicio = p.ejercicio AND elt.codigo_barras= p.codigo_palet) "
        ElseIf rbVolcados.Checked Then
            Sql = Sql + " And EXISTS(SELECT lote FROM entradas_lotes_test elt WHERE elt.empresa = p.empresa And elt.ejercicio = p.ejercicio And elt.codigo_barras= p.codigo_palet) "
        End If

        Sql = Trim(Sql) + " ORDER BY 3"


        CargarPrecalibrado(Sql)

    End Sub

    Private Sub MostrarSegunda(nOrd)
        Dim Sql As String

        Sql = " SELECT p.codigo_barras, p.bultos, (p.bultos*19) as peso, p.fecha_confeccion, p.hora_confeccion, p.codigo_variedad, p.situacion "
        Sql = Sql + " FROM palets_segunda p "
        Sql = Sql + " LEFT JOIN variedades v ON v.empresa= '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND v.codigo_variedad = p.codigo_variedad "

        If nOrd = 0 Then
            Sql = Sql + " WHERE v.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT') "
        Else
            If Trim(TxtVariedad.Text) > "" Then
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " WHERE  (p.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                ElseIf nOrd = 2 Then
                    Sql = Trim(Sql) + " WHERE  (p.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                End If
            End If
        End If

        Sql = Trim(Sql) + " AND p.fecha_confeccion BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'" ' ND SITUACION = 'N'"
        Sql = Trim(Sql) + " ORDER BY 4"

        CargarSegunda(Sql)

    End Sub
    Private Sub MostrarCestasFabricadas(nOrd)
        Dim Sql As String
        Sql = "Select p.codigo_palet, peso_palet = Case When isnull(p.peso_palet,0) > 0 Then p.peso_palet Else isnull(o.peso_palet,0) End, p.hora_confeccion, o.codigo_variedad, (rtrim(o.codigo_confeccion) + ' - ' + co.descripcion ) as desconfeccion, (rtrim(o.paletizacion) + ' - ' + pa.descripcion ) as despaletizacion, (CONVERT(varchar(10), o.codigo_cliente) + ' - ' + c.razon_social ) as descliente, o.codigo_destinatario, o.calibre_tecnico, o.calibre_comercial FROM palets AS p LEFT JOIN ordenes_confeccion o "
        Sql = Sql + " ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion LEFT JOIN variedades v ON v.empresa=p.empresa AND v.codigo_variedad = o.codigo_variedad "
        Sql = Sql + " LEFT JOIN clientes_coop c ON c.codigo_cliente = o.codigo_cliente "
        Sql = Sql + " LEFT JOIN confeccion co ON co.empresa = p.empresa and co.codigo_confeccion = o.codigo_confeccion "
        Sql = Sql + " LEFT JOIN confeccion pa ON pa.empresa = p.empresa and pa.codigo_confeccion = o.paletizacion "

        If nOrd = 0 Then
            Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND v.codigo_familia IN ('MEO', 'GRAN', 'KAKI', 'KIWI', 'NECT') "
        Else
            If Trim(TxtVariedad.Text) > "" Then
                Sql = Sql + " WHERE p.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "'"
                If nOrd = 1 Then
                    Sql = Trim(Sql) + " AND (o.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                ElseIf nOrd = 2 Then
                    Sql = Trim(Sql) + " AND (o.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' )"
                End If
            End If
        End If
        'Sql = Trim(Sql) + " AND p.fecha_confeccion='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"

        If DPFechaVolcado.Value < DPHastaFecha.Value Then
            Sql = Trim(Sql) + " AND p.fecha_confeccion BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        Else
            Sql = Trim(Sql) + " And p.fecha_confeccion ='" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "'"
        End If

        Sql = Trim(Sql) + " AND p.linea_fabricacion = 'CESTAS'"
        Sql = Trim(Sql) + " ORDER BY 3"

        CargarCestasFabricadas(Sql)

    End Sub

    Private Sub Mostrarseguimiento(nOrd)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim rs1 As New cnRecordset.CnRecordset
        Dim valores(10) As Double
        Dim i As Integer
        Dim a5 As Double
        Dim a6 As Double
        Dim a7 As Double
        Dim a8 As Double
        Dim a9 As Double
        Dim a10 As Double
        Dim dif As Double
        Dim HayDatos As Boolean
        Dim KgsVolcados As Double
        Dim KgPaletsSt As Double
        Dim DifPrimera As Double
        Dim imp1 As Double
        Dim imp2 As Double
        Dim imp3 As Double
        Dim Pesoconfeccionado As Double
        Dim mermas As Double
        Dim Precalibrado As Double
        Dim PesoPrimera As Double
        Dim pesodestrio As Double
        Dim PorPrimera As Double
        Dim PorSegunda As Double
        Dim PorDestrio As Double

        If TxtVariedad.Text.Trim = "" Then
            Return
        End If


        KgsVolcados = 0
        For i = 0 To DGVolcado.Rows.Count - 1
            If Not (CDbl(DGVolcado.Rows(i).Cells(7).Value) >= 200000 And CDbl(DGVolcado.Rows(i).Cells(7).Value) <= 299999) Then
                KgsVolcados = KgsVolcados + CDbl(DGVolcado.Rows(i).Cells(6).Value)
            End If
        Next

        KgPaletsSt = DevuelveSegundadelDia(DPFechaVolcado.Value, DPHastaFecha.Value)
        Pesoconfeccionado = KgPaletsSt
        Sql = "SELECT SUM(isnull(p.peso_real, p.peso_palet)) AS peso FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion LEFT JOIN confeccion c ON c.empresa = p.empresa AND c.codigo_confeccion = o.codigo_confeccion "
        Sql = Sql + " LEFT JOIN subgrupos_confeccion sc ON sc.empresa = p.empresa AND sc.codigo_subgrupo = c.subgrupo_confeccion LEFT JOIN grupos_confeccion gc ON gc.empresa = p.empresa AND gc.grupo_confeccion = sc.grupo_confeccion  "
        Sql = Sql + " WHERE  p.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND o.codigo_variedad = '" + Trim(TxtVariedad.Text) + "' AND "
        Sql = Sql + " p.fecha_confeccion  BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Sql + " and not gc.grupo_confeccion IN ('F6', 'F7', 'F8', 'F9', 'F14') AND o.categoria <> 'RE' AND categoria <> 'ST' "

        mermas = 0
        PesoPrimera = 0
        Rs.Open(Sql, ObjetoGlobal.cn)
        If Not Rs.EOF And Not IsDBNull(Rs!peso) Then
            Pesoconfeccionado = Pesoconfeccionado + Rs!peso
            PesoPrimera = Rs!peso
        End If

        Rs.Close()
        Precalibrado = 0
        Sql = "SELECT SUM(bultos) AS total_cajas, sum(bultos * isnull(kgs,0)) as total_kilos FROM palets_precal_fru WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND "
        Sql = Sql + " codigo_variedad = '" + Trim(TxtVariedad.Text) + "' AND fecha_confeccion  BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        'Sql = Sql + " AND (NOT fecha_volcado  BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "' OR fecha_volcado IS NULL)"

        Rs.Open(Sql, ObjetoGlobal.cn)
        If Not Rs.EOF And Not IsDBNull(Rs!total_cajas) Then
            If Strings.Left(TxtVariedad.Text, 2) = "17" Then
                Pesoconfeccionado = Pesoconfeccionado + (Rs!total_cajas * 12)
                Precalibrado = Precalibrado + (Rs!total_cajas * 12)
            ElseIf Strings.Left(TxtVariedad.Text, 2) = "14" Then
                Pesoconfeccionado = Pesoconfeccionado + (Rs!total_cajas * 18)
                Precalibrado = Precalibrado + (Rs!total_cajas * 18)
            ElseIf Strings.Left(TxtVariedad.Text, 2) = "09" Then
                If Rs!total_kilos > 0 Then
                    Pesoconfeccionado = Pesoconfeccionado + Rs!total_kilos
                    Precalibrado = Precalibrado + Rs!total_kilos
                Else
                    Pesoconfeccionado = Pesoconfeccionado + (Rs!total_cajas * 6)
                    Precalibrado = Precalibrado + (Rs!total_cajas * 6)
                End If
            End If
        End If


        Rs.Close()

        Sql = "SELECT isnull(sum(kg_a_liquidar),0) as Kg "
        Sql = Sql + " FROM entradas_albaranes WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND fecha_entrada BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "' and codigo_variedad='" & Trim(TxtVariedad.Text) & "'"

        For i = 1 To 10
            valores(i) = 0#
        Next

        HayDatos = False

        Rs.Open(Sql, ObjetoGlobal.cn)

        If Not Rs.EOF And Not IsDBNull(Rs!kg) Then
            valores(1) = Rs!kg
        End If


        valores(2) = KgsVolcados

        'rs1.Close()

        Sql = "SELECT isnull(SUM(bt.kg_calidad),0) as Kg FROM entradas_clasif_bt bt LEFT JOIN entradas_lotes_test t ON t.empresa=bt.empresa AND t.ejercicio=bt.ejercicio AND "
        Sql = Sql + " t.serie_albaran=bt.serie_albaran AND t.numero_albaran=bt.numero_albaran AND t.bulto=bt.bulto"
        Sql = Sql + " where bt.empresa='" & ObjetoGlobal.EmpresaActual & "' AND bt.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND "
        Sql = Sql + " t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Sql + " and bt.codigo_variedad='" & Trim(TxtVariedad.Text) & "'"
        Sql = Sql + " AND bt.codigo_calidad BETWEEN 1 AND 2 "
        rs1.Open(Sql, ObjetoGlobal.cn)

        If Not rs1.EOF And Not IsDBNull(rs1!kg) Then
            valores(3) = rs1!kg
        End If

        rs1.Close()
        Sql = "SELECT isnull(sum(bt.kg_calidad),0) as Kg FROM entradas_clasif_bt bt LEFT JOIN entradas_lotes_test t ON t.empresa=bt.empresa AND t.ejercicio=bt.ejercicio AND "
        Sql = Sql + " t.serie_albaran=bt.serie_albaran AND t.numero_albaran=bt.numero_albaran AND t.bulto=bt.bulto"
        Sql = Sql + " where bt.empresa='" & ObjetoGlobal.EmpresaActual & "' AND bt.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND "
        Sql = Sql + " t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Sql + " and bt.codigo_variedad='" & Trim(TxtVariedad.Text) & "'"
        Sql = Sql + " AND bt.codigo_calidad BETWEEN 3 AND 4 "
        rs1.Open(Sql, ObjetoGlobal.cn)

        If Not rs1.EOF And Not IsDBNull(rs1!kg) Then
            valores(4) = rs1!kg
        End If

        rs1.Close()
        Sql = "SELECT isnull(sum(bt.kg_calidad),0) as Kg FROM entradas_clasif_bt bt LEFT JOIN entradas_lotes_test t ON t.empresa=bt.empresa AND t.ejercicio=bt.ejercicio AND "
        Sql = Sql + " t.serie_albaran=bt.serie_albaran AND t.numero_albaran=bt.numero_albaran AND t.bulto=bt.bulto"
        Sql = Sql + " where bt.empresa='" & ObjetoGlobal.EmpresaActual & "' AND bt.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND "
        Sql = Sql + " t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Sql + " and bt.codigo_variedad='" & Trim(TxtVariedad.Text) & "'"
        Sql = Sql + " AND bt.codigo_calidad BETWEEN 5 AND 8 "
        rs1.Open(Sql, ObjetoGlobal.cn)

        If Not rs1.EOF And Not IsDBNull(rs1!kg) Then
            valores(5) = rs1!kg
        End If

        rs1.Close()
        Sql = "SELECT isnull(sum(bt.kg_calidad),0) as Kg FROM entradas_clasif_bt bt LEFT JOIN entradas_lotes_test t ON t.empresa=bt.empresa AND t.ejercicio=bt.ejercicio AND "
        Sql = Sql + " t.serie_albaran=bt.serie_albaran AND t.numero_albaran=bt.numero_albaran AND t.bulto=bt.bulto"
        Sql = Sql + " where bt.empresa='" & ObjetoGlobal.EmpresaActual & "' AND bt.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND "
        Sql = Sql + " t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Sql + " and bt.codigo_variedad='" & Trim(TxtVariedad.Text) & "'"
        Sql = Sql + " AND bt.codigo_calidad = 10"
        rs1.Open(Sql, ObjetoGlobal.cn)

        If Not rs1.EOF And Not IsDBNull(rs1!kg) Then
            valores(9) = rs1!kg
        End If

        rs1.Close()
        Sql = "SELECT isnull(sum(bt.kg_calidad),0) as Kg FROM entradas_clasif_bt bt LEFT JOIN entradas_lotes_test t ON t.empresa=bt.empresa AND t.ejercicio=bt.ejercicio AND "
        Sql = Sql + " t.serie_albaran=bt.serie_albaran AND t.numero_albaran=bt.numero_albaran AND t.bulto=bt.bulto"
        Sql = Sql + " where bt.empresa='" & ObjetoGlobal.EmpresaActual & "' AND bt.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND "
        Sql = Sql + " t.fecha_volcado BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        Sql = Sql + " and bt.codigo_variedad='" & Trim(TxtVariedad.Text) & "'"
        Sql = Sql + " AND bt.codigo_calidad = 11"
        rs1.Open(Sql, ObjetoGlobal.cn)

        If Not rs1.EOF And Not IsDBNull(rs1!kg) Then
            valores(10) = rs1!kg
        End If

        rs1.Close()
        Sql = " SELECT sum (CASE WHEN (grande_peq = 'G' or grande_peq = 'R' or grande_peq = 'A') then 410 ELSE 250 END) AS Kilos"
        Sql = Trim(Sql) + " FROM palets_destrio "
        Sql = Trim(Sql) + " WHERE codigo_variedad = '" & Trim(TxtVariedad.Text) & "'"
        Sql = Trim(Sql) + " AND fecha BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "'"
        rs1.Open(Sql, ObjetoGlobal.cn)
        If Not rs1.EOF And Not IsDBNull(rs1!Kilos) Then
            valores(7) = rs1!Kilos
        End If
        rs1.Close()
        HayDatos = True

        pesodestrio = KgsVolcados - Pesoconfeccionado '(DifPrimera, Segundadel y precalibrado)

        mermas = 0

        DifPrimera = PesoPrimera 'Pesoconfeccionado

        PorPrimera = Math.Round((DifPrimera + Precalibrado) * 100 / KgsVolcados, 2)
        PorSegunda = Math.Round(KgPaletsSt * 100 / KgsVolcados, 2)
        PorDestrio = Math.Round(pesodestrio * 100 / KgsVolcados, 2)

        'valores(7) = valores(7) + mermas

        Rs.Close()

        Sql = "SELECT isnull(sum(p.peso_palet),0) as peso FROM palets p LEFT JOIN ordenes_confeccion o ON p.empresa=o.empresa AND p.orden_confeccion = o.numero_orden WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.fecha_carga BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy 23:59:59") + "' AND "
        Sql = Sql + " o.codigo_variedad='" & Trim(TxtVariedad.Text) & "' and p.linea_fabricacion IS NULL"
        rs1.Open(Sql, ObjetoGlobal.cn)
        If HayDatos And valores(2) > 0 Then

            ' Dto5Porc = Math.Round((valores(3) + valores(4) + valores(5)) * 5 / 100, 2)

            valores(6) = KgPaletsSt
            'valores(7) = valores(2) - (valores(3) + valores(4) + valores(5) + valores(6) - Dto5Porc)
            '(valores(3) + valores(4) + valores(5)
            'a5 = Math.Round((valores(3) + valores(4) + valores(5)) * 100 / valores(2), 2)
            'a6 = Math.Round(valores(6) * 100 / valores(2), 2)
            'a7 = Math.Round(valores(7) * 100 / valores(2), 2)

            valores(8) = (valores(3) + valores(4) + valores(5))

            imp1 = (DifPrimera * valores(3)) / (valores(3) + valores(4) + valores(5))
            imp2 = (DifPrimera * valores(4)) / (valores(3) + valores(4) + valores(5))
            imp3 = (DifPrimera * valores(5)) / (valores(3) + valores(4) + valores(5))

            valores(3) = Math.Round(imp1, 2)
            valores(4) = Math.Round(imp2, 2)
            valores(5) = Math.Round(imp3, 2)

            'DifPrimera = KgsVolcados - KgPaletsSt - valores(7)

            a5 = Math.Round((valores(3) + valores(4) + valores(5)) * 100 / (valores(3) + valores(4) + valores(5) + valores(6) + pesodestrio), 2)
            a6 = Math.Round(valores(6) * 100 / (valores(3) + valores(4) + valores(5) + valores(6) + pesodestrio), 2)
            a7 = Math.Round(pesodestrio * 100 / (valores(3) + valores(4) + valores(5) + valores(6) + pesodestrio), 2)

            dif = 100 - (a5 + a6 + a7)
            a7 = Math.Round(a7 + dif, 2)

            'a7 = 100 - a5 - a6
            a8 = Math.Round(valores(3) * 100 / (valores(3) + valores(4) + valores(5)), 2)
            a9 = Math.Round(valores(4) * 100 / (valores(3) + valores(4) + valores(5)), 2)
            a10 = Math.Round(valores(5) * 100 / (valores(3) + valores(4) + valores(5)), 2)
            dif = 100 - (a8 + a9 + a10)
            a10 = Math.Round(a10 + dif, 2)
            'a10 = 100 - a7 - a8

            ' Kg. entrados
            Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Math.Round(valores(1) / 1000, 0)
            Row.Cells.Add(Cell)

            ' Volcados
            Cell = New DataGridViewTextBoxCell
            Cell.Value = valores(2) 'Math.Round(valores(2) / 1000, 0)
            Row.Cells.Add(Cell)

            ' Tm. cámara
            Cell = New DataGridViewTextBoxCell
            Cell.Value = 0
            Row.Cells.Add(Cell)

            ' Tm cargado
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Math.Round(rs1!peso / 1000, 0)
            Row.Cells.Add(Cell)

            ' Desde aquí
            ' Kg. primera
            Cell = New DataGridViewTextBoxCell
            Cell.Value = PesoPrimera '(valores(3) + valores(4) + valores(5)) '- Dto5Porc
            Row.Cells.Add(Cell)

            ' Kg. Segunda
            Cell = New DataGridViewTextBoxCell
            Cell.Value = KgPaletsSt
            Row.Cells.Add(Cell)


            ' Kg. Precalibrado
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Precalibrado
            Row.Cells.Add(Cell)

            ' Kg. N/C
            Cell = New DataGridViewTextBoxCell
            Cell.Value = pesodestrio
            Row.Cells.Add(Cell)

            ' Hasta aquí
            ' Kg. 1a y extra
            Cell = New DataGridViewTextBoxCell
            Cell.Value = PorPrimera
            Row.Cells.Add(Cell)

            ' Kg. segunda
            Cell = New DataGridViewTextBoxCell
            Cell.Value = PorSegunda
            Row.Cells.Add(Cell)

            ' Kg. N/C
            Cell = New DataGridViewTextBoxCell
            Cell.Value = PorDestrio
            Row.Cells.Add(Cell)

            ' 2/3
            Cell = New DataGridViewTextBoxCell
            Cell.Value = a8
            Row.Cells.Add(Cell)

            '4/5
            Cell = New DataGridViewTextBoxCell
            Cell.Value = a9
            Row.Cells.Add(Cell)

            ' 6/9
            Cell = New DataGridViewTextBoxCell
            Cell.Value = a10 'Math.Round(valores(5) * 100 / (valores(3) + valores(4) + valores(5)), 2)
            Row.Cells.Add(Cell)

            ' 1a aut
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Math.Round(valores(8), 0)
            Row.Cells.Add(Cell)

            ' 2a aut
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Math.Round(valores(9), 0)
            Row.Cells.Add(Cell)

            ' NC Aut
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Math.Round(valores(10), 0)
            Row.Cells.Add(Cell)

            ' Mermas
            Cell = New DataGridViewTextBoxCell
            Cell.Value = Math.Round(mermas, 0)
            Row.Cells.Add(Cell)

            DGSeguimiento.Rows.Add(Row)
        End If

    End Sub

    Private Sub CabeceraPalotsDestrio()

        PalotsDetrio().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "CB"
        column1.HeaderText = "Código de barras"
        column1.Width = 125
        PalotsDetrio().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "VARIEDAD"
        column2.HeaderText = "Variedad"
        column2.Width = 125
        PalotsDetrio().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "FECHA"
        column3.HeaderText = "Fecha"
        column3.Width = 125
        PalotsDetrio().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "TAMANO"
        column4.HeaderText = "Tamaño palet"
        column4.Width = 125
        PalotsDetrio().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "PESO"
        column5.HeaderText = "Peso"
        column5.Width = 125
        PalotsDetrio().Columns.Add(column5)


    End Sub

    Private Sub CabeceraVolcados()

        DGVolcado().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "lote"
        column1.HeaderText = "Lote"
        column1.Width = 125
        DGVolcado().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "hora"
        column2.HeaderText = "Hora"
        column2.Width = 100
        DGVolcado().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "albaran"
        column3.HeaderText = "Albarán"
        column3.Width = 100
        DGVolcado().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "bulto"
        column4.HeaderText = "Bulto"
        column4.Width = 100
        DGVolcado().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "variedad"
        column5.HeaderText = "Variedad"
        column5.Width = 100
        DGVolcado().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "calibre"
        column6.HeaderText = "Kgs.calibre"
        column6.Width = 100
        DGVolcado().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "kbulto"
        column7.HeaderText = "Kgs.bulto"
        column7.Width = 100
        DGVolcado().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "cbarras"
        column8.HeaderText = "Cod. barras"
        column8.Width = 100
        DGVolcado().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "b"
        column9.HeaderText = "b"
        column9.Width = 20
        DGVolcado().Columns.Add(column9)

    End Sub


    Private Sub CabeceraSegunda()
        Dim aColumns(7) As DataGridViewColumn

        DGPaletsSegunda().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "palet"
        column1.HeaderText = "Palet"
        column1.Width = 300
        DGPaletsSegunda().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "Cajas"
        column2.HeaderText = "cajas"
        column2.Width = 150
        DGPaletsSegunda().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "peso"
        column3.HeaderText = "Peso"
        column3.Width = 100
        DGPaletsSegunda().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "fecha"
        column4.HeaderText = "fecha confección"
        column4.Width = 100
        DGPaletsSegunda().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "hora"
        column5.HeaderText = "Hora confección"
        column5.Width = 100
        DGPaletsSegunda().Columns.Add(column5)

        Dim column6 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column6.Name = "variedad"
        column6.HeaderText = "Variedad"
        column6.Width = 125
        DGPaletsSegunda().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "situacion"
        column7.HeaderText = "Situación"
        column7.Width = 150
        DGPaletsSegunda().Columns.Add(column7)

    End Sub


    Private Sub CabeceraFabricados(aCal As Array)
        Dim aColumns(12) As DataGridViewColumn

        DGFabricados().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "palet"
        column1.HeaderText = "Palet"
        column1.Width = 300
        DGFabricados().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "peso"
        column2.HeaderText = "Peso"
        column2.Width = 100
        DGFabricados().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "hora"
        column3.HeaderText = "Hora"
        column3.Width = 100
        DGFabricados().Columns.Add(column3)


        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "variedad"
        column4.HeaderText = "Variedad"
        column4.Width = 125
        DGFabricados().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "confeccion"
        column5.HeaderText = "Confección"
        column5.Width = 150
        DGFabricados().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "paletizacion"
        column6.HeaderText = "Paletizacion"
        column6.Width = 150
        DGFabricados().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "cliente"
        column7.HeaderText = "Cliente"
        column7.Width = 150
        DGFabricados().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "destinatario"
        column8.HeaderText = "Destinatario"
        column8.Width = 150
        DGFabricados().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "Caltecnico"
        column9.HeaderText = "Cal.técnico"
        column9.Width = 150
        DGFabricados().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "Calcomercial"
        column10.HeaderText = "Cal.comercial"
        column10.Width = 150
        DGFabricados().Columns.Add(column10)


        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "fecha_confeccion"
        column11.HeaderText = "Fecha confeccion"
        column11.Width = 150
        DGFabricados().Columns.Add(column11)

        Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column12.Name = "orden_confeccion"
        column12.HeaderText = "Orden confeccion"
        column12.Width = 150
        DGFabricados().Columns.Add(column12)

        'For i = 0 To UBound(aCal)
        '    aColumns(i) = New DataGridViewTextBoxColumn()
        '    aColumns(i).Name = "cal" & i
        '    aColumns(i).HeaderText = aCal(i)
        '    aColumns(i).Width = 125
        '    DGFabricados().Columns.Add(aColumns(i))
        'Next

        Dim column99 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column99.Name = "tipo_fabricacion"
        column99.HeaderText = "tipo_fabricacion"
        column99.Width = 50
        DGFabricados().Columns.Add(column99)

    End Sub


    Private Sub CabeceraPrecalibrado()

        DGPrecalibrados().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "palet"
        column1.HeaderText = "Palet"
        column1.Width = 100
        DGPrecalibrados().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "referencia"
        column2.HeaderText = "Referencia"
        column2.Width = 75
        DGPrecalibrados().Columns.Add(column2)

        Dim column3 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column3.Name = "codigo_variedad"
        column3.HeaderText = "Variedad"
        column3.Width = 75
        DGPrecalibrados().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "codigo_calibre"
        column4.HeaderText = "Calibre"
        column4.Width = 75
        DGPrecalibrados().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "fecha_confeccion"
        column5.HeaderText = "Fecha confeccion"
        column5.Width = 75
        DGPrecalibrados().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "hora_confeccion"
        column6.HeaderText = "Hora confeccion"
        column6.Width = 75
        DGPrecalibrados().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "salida"
        column7.HeaderText = "Salida"
        column7.Width = 50
        DGPrecalibrados().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "bultos"
        column8.HeaderText = "Bultos"
        column8.Width = 50
        DGPrecalibrados().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "cantidad"
        column9.HeaderText = "Cantidad"
        column9.Width = 50
        DGPrecalibrados().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "Kg"
        column10.HeaderText = "kilos"
        column10.Width = 35
        DGPrecalibrados().Columns.Add(column10)

    End Sub

    Private Sub CabeceraVolcadoCestas()

        DGVolcadosCestas().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "palet"
        column1.HeaderText = "Palet"
        column1.Width = 100
        DGVolcadosCestas().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "peso"
        column2.HeaderText = "Peso"
        column2.Width = 100
        DGVolcadosCestas().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "hora"
        column3.HeaderText = "Hora"
        column3.Width = 100
        DGVolcadosCestas().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "variedad"
        column4.HeaderText = "Variedad"
        column4.Width = 100
        DGVolcadosCestas().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "confeccion"
        column5.HeaderText = "Confección"
        column5.Width = 100
        DGVolcadosCestas().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "paletizacion"
        column6.HeaderText = "Paletizacion"
        column6.Width = 100
        DGVolcadosCestas().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "cliente"
        column7.HeaderText = "Cliente"
        column7.Width = 100
        DGVolcadosCestas().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "destinatario"
        column8.HeaderText = "Destinatario"
        column8.Width = 100
        DGVolcadosCestas().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "Caltecnico"
        column9.HeaderText = "Cal.técnico"
        column9.Width = 100
        DGVolcadosCestas().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "Calcomercial"
        column10.HeaderText = "Cal.comercial"
        column10.Width = 100
        DGVolcadosCestas().Columns.Add(column10)
    End Sub


    Private Sub CabeceraFabricadoCestas()

        DGFabricadoCestas().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "palet"
        column1.HeaderText = "Palet"
        column1.Width = 100
        DGFabricadoCestas().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "peso"
        column2.HeaderText = "Peso"
        column2.Width = 100
        DGFabricadoCestas().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "hora"
        column3.HeaderText = "Hora"
        column3.Width = 100
        DGFabricadoCestas().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "variedad"
        column4.HeaderText = "Variedad"
        column4.Width = 100
        DGFabricadoCestas().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "confeccion"
        column5.HeaderText = "Confección"
        column5.Width = 200
        DGFabricadoCestas().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "paletizacion"
        column6.HeaderText = "Paletizacion"
        column6.Width = 200
        DGFabricadoCestas().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "cliente"
        column7.HeaderText = "Cliente"
        column7.Width = 200
        DGFabricadoCestas().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "destinatario"
        column8.HeaderText = "Destinatario"
        column8.Width = 200
        DGFabricadoCestas().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "Caltecnico"
        column9.HeaderText = "Cal.técnico"
        column9.Width = 100
        DGFabricadoCestas().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "Calcomercial"
        column10.HeaderText = "Cal.comercial"
        column10.Width = 100
        DGFabricadoCestas().Columns.Add(column10)
    End Sub

    Private Sub CabeceraSeguimiento()

        DGSeguimiento().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "kgentrada"
        column1.HeaderText = "Kg. entrada"
        column1.Width = 300
        DGSeguimiento().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "tmvolcadas"
        column2.HeaderText = "Kg. volcados"
        column2.Width = 125
        DGSeguimiento().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "tmacámara"
        column3.HeaderText = "Tm. a cámara"
        column3.Width = 300
        DGSeguimiento().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "tmcargado"
        column4.HeaderText = "Tm. cargado"
        column4.Width = 125
        DGSeguimiento().Columns.Add(column4)

        Dim column5 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column5.Name = "kg1"
        column5.HeaderText = "Kg. Primera"
        column5.Width = 125
        DGSeguimiento().Columns.Add(column5)

        Dim column6 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column6.Name = "kg2"
        column6.HeaderText = "Kg. estandar"
        column6.Width = 125
        DGSeguimiento().Columns.Add(column6)


        Dim column7 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column7.Name = "precalibrado"
        column7.HeaderText = "Precalibrado"
        column7.Width = 125
        DGSeguimiento().Columns.Add(column7)

        Dim column8 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column8.Name = "kgNC"
        column8.HeaderText = "Kg. NC"
        column8.Width = 125
        DGSeguimiento().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "1yextra"
        column9.HeaderText = "1ª y extra"
        column9.Width = 150
        DGSeguimiento().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "segunda"
        column10.HeaderText = "Segunda"
        column10.Width = 150
        DGSeguimiento().Columns.Add(column10)

        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "nocomercial"
        column11.HeaderText = "No comercial"
        column11.Width = 150
        DGSeguimiento().Columns.Add(column11)

        Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column12.Name = "23"
        column12.HeaderText = "2/3"
        column12.Width = 150
        DGSeguimiento().Columns.Add(column12)

        Dim column13 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column13.Name = "45"
        column13.HeaderText = "4/5"
        column13.Width = 150
        DGSeguimiento().Columns.Add(column13)

        Dim column14 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column14.Name = "6"
        column14.HeaderText = "6/9"
        column14.Width = 150
        DGSeguimiento().Columns.Add(column14)

        Dim column15 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column15.Name = "PRIMERA"
        column15.HeaderText = "1ª Aut."
        column15.Width = 150
        DGSeguimiento().Columns.Add(column15)

        Dim column16 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column16.Name = "SEGUNDA"
        column16.HeaderText = "2ª Aut."
        column16.Width = 150
        DGSeguimiento().Columns.Add(column16)

        Dim column17 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column17.Name = "NOCOMERCIAL"
        column17.HeaderText = "NC Aut."
        column17.Width = 150
        DGSeguimiento().Columns.Add(column17)

        Dim column18 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column18.Name = "MERMAS"
        column18.HeaderText = "Mermas"
        column18.Width = 150
        DGSeguimiento().Columns.Add(column18)

    End Sub

    Private Sub CabeceraTrazabilidad()

        DGTrazabilidad().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "hora"
        column1.HeaderText = "Hora"
        column1.Width = 125
        DGTrazabilidad().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "palet"
        column2.HeaderText = "Palet"
        column2.Width = 125
        DGTrazabilidad().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "kilos"
        column3.HeaderText = "Kilos"
        column3.Width = 100
        DGTrazabilidad().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "vacio"
        column4.HeaderText = " ->"
        column4.Width = 30
        DGTrazabilidad().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "albaran"
        column5.HeaderText = "Albarán"
        column5.Width = 125
        DGTrazabilidad().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "bulto"
        column6.HeaderText = "Bulto"
        column6.Width = 100
        DGTrazabilidad().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "kilos"
        column7.HeaderText = "Kilos"
        column7.Width = 100
        DGTrazabilidad().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "hora"
        column8.HeaderText = "Hora"
        column8.Width = 100
        DGTrazabilidad().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "eurep_cs"
        column9.HeaderText = "Global Gap"
        column9.Width = 150
        DGTrazabilidad().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "grasp"
        column10.HeaderText = "grasp"
        column10.Width = 150
        DGTrazabilidad().Columns.Add(column10)

    End Sub
    Private Sub CabeceraTrazaSegunda()

        DGTrazaSegunda().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "hora"
        column1.HeaderText = "Hora"
        column1.Width = 125
        DGTrazaSegunda().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "palet"
        column2.HeaderText = "Palet"
        column2.Width = 125
        DGTrazaSegunda().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "kilos"
        column3.HeaderText = "Kilos"
        column3.Width = 100
        DGTrazaSegunda().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "vacio"
        column4.HeaderText = " ->"
        column4.Width = 30
        DGTrazaSegunda().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "albaran"
        column5.HeaderText = "Albarán"
        column5.Width = 125
        DGTrazaSegunda().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "bulto"
        column6.HeaderText = "Bulto"
        column6.Width = 100
        DGTrazaSegunda().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "kilos"
        column7.HeaderText = "Kilos"
        column7.Width = 100
        DGTrazaSegunda().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "hora"
        column8.HeaderText = "Hora"
        column8.Width = 100
        DGTrazaSegunda().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "eurep_cs"
        column9.HeaderText = "Global Gap"
        column9.Width = 150
        DGTrazaSegunda().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "grasp"
        column10.HeaderText = "grasp"
        column10.Width = 150
        DGTrazaSegunda().Columns.Add(column10)

    End Sub
    Private Sub CargarVolcados(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        Dim Rsp As New cnRecordset.CnRecordset
        ' Dim ord As Integer
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()
        Dim cClave As String
        Dim Defectos As Double
        Dim nColNC As Integer
        Dim u As Integer
        Dim rss As New cnRecordset.CnRecordset
        Dim sSql As String
        Dim porc As Decimal


        'MinAlbaran = 999999
        'MaxAlbaran = 0

        nColNC = DondeNC()

        cAlbaranesVolcados = ""

        i = 0

        If RS.Open(Sql, ObjetoGlobal.cn,,,,,, 150) Then
            cpb.Maximum = RS.RecordCount
            cpb.Minimum = 0
            'cpb.Increment(0)
            cpb.Value = 0
            cpb.Step = 1
            While Not RS.EOF
                i = i + 1
                cpb.Text = "Procesado " & Format((100 / cpb.Maximum * i), "#0.00")
                cpb.PerformStep()
                If i > 1 Then
                    u = InStr(1, cAlbaranesVolcados, "" & RS!numero_albaran)
                Else
                    u = 0
                End If
                If u = 0 Then
                    If cAlbaranesVolcados = "" Then
                        cAlbaranesVolcados = "" & RS!numero_albaran
                    Else
                        cAlbaranesVolcados = cAlbaranesVolcados + "," & RS!numero_albaran
                    End If
                End If

                cClave = Strings.Right("0000000" & RS!numero_albaran, 7) & Strings.Right("000" & RS!bulto, 3)
                'If RS!numero_albaran < MinAlbaran Then
                '    MinAlbaran = RS!numero_albaran
                'End If
                'If RS!numero_albaran > MaxAlbaran Then
                '    MaxAlbaran = RS!numero_albaran
                'End If

                Row = New DataGridViewRow
                Defectos = 0
                For o = 0 To RS.CuantosCampos - 4 '1
                    Cell = New DataGridViewTextBoxCell
                    If o = 6 Then
                        If Trim(RS!empresa) = "" And ("" & RS!numero_albaran) = "" Then
                            Cell.Value = RS.Valor(RS.NombreCampo(o))
                        Else
                            sSql = "select CAST(ISNULL(dbo.fn_peso_bulto('" & Trim(RS!empresa) & "','" & Trim(RS!ejercicio) & "','" & Trim(RS!serie_albaran) & "', " & RS!numero_albaran & ", " & RS!bulto & "),0) As Integer) As peso_bultos"
                            If Rsp.Open(sSql, ObjetoGlobal.cn) Then
                                Cell.Value = Rsp!peso_bultos

                                If (DictNCAlbaran.ContainsKey(cClave)) Then
                                    ' DictNCAlbaran(cClave) = DictNCAlbaran(cClave) + Defectos
                                Else
                                    DictNCAlbaran.Add(cClave, Rsp!peso_bultos)
                                End If

                            Else
                                Cell.Value = 0
                            End If
                            Rsp.Close()
                        End If
                    Else
                        Cell.Value = RS.Valor(RS.NombreCampo(o))
                    End If
                    Row.Cells.Add(Cell)
                    If nColNC > 0 Then
                        If o = (nColNC - 1) Or o = nColNC Then
                            Defectos = Defectos + CLng("0" & RS.Valor(RS.NombreCampo(o)))
                        End If
                    End If
                Next
                If Not IsDBNull(RS!numero_albaran) And Not IsDBNull(RS!bulto) Then
                    sSql = "Select * FROM defectos_linea where ejercicio = '" & ObjetoGlobal.EjercicioActual & " ' and numero_albaran = " & RS!numero_albaran & " AND caja=" & RS!bulto
                    If rss.Open(sSql, ObjetoGlobal.cn) Then
                        If rss.RecordCount > 0 Then
                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = "Sí"
                            Row.Cells.Add(Cell)
                        Else
                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = "No"
                            Row.Cells.Add(Cell)
                        End If
                    Else
                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = "No"
                        Row.Cells.Add(Cell)
                    End If
                    rss.Close()
                End If

                'If (DictNCAlbaran.ContainsKey(cClave)) Then
                '    DictNCAlbaran(cClave) = DictNCAlbaran(cClave) + Defectos
                'Else
                '    DictNCAlbaran.Add(cClave, Defectos)
                'End If
                DGVolcado.Rows.Add(Row)
                RS.MoveNext()
            End While
        End If
        cpb.Visible = False
        cpb.Hide()


    End Sub

    Private Sub CargarFabricados(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()

        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                For o = 0 To RS.CuantosCampos - 1
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                DGFabricados.Rows.Add(Row)
            Next
        End If
    End Sub

    Private Sub CargarPrecalibrado(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()
        Dim peso As Double

        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                For o = 0 To 9
                    Cell = New DataGridViewTextBoxCell
                    If o < 9 Then
                        Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Else
                        If Strings.Left(TxtVariedad.Text, 2) = "17" Then
                            If RS!peso > 0 Then
                                peso = (RS!bultos * RS!peso)
                            Else
                                peso = (RS!bultos * 12)
                            End If
                            Cell.Value = peso
                        ElseIf Strings.Left(TxtVariedad.Text, 2) = "14" Then
                            If RS!peso > 0 Then
                                peso = (RS!bultos * RS!peso)
                            Else
                                peso = (RS!bultos * 12)
                            End If
                            Cell.Value = peso
                        ElseIf Strings.Left(TxtVariedad.Text, 2) = "09" Then
                            If RS!peso > 0 Then
                                peso = (RS!bultos * RS!peso)
                            Else
                                peso = (RS!bultos * 6)
                            End If
                            Cell.Value = peso
                        Else
                            If RS!peso > 0 Then
                                peso = (RS!bultos * RS!peso)
                            Else
                                peso = (RS!bultos * 12)
                            End If
                            Cell.Value = peso
                        End If
                    End If
                    Row.Cells.Add(Cell)
                Next
                DGPrecalibrados.Rows.Add(Row)
            Next
        End If
    End Sub

    Private Sub CargarSegunda(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()

        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                For o = 0 To 6
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                DGPaletsSegunda.Rows.Add(Row)
            Next
        End If
    End Sub

    Private Sub CargarCestasVolcadas(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        Dim ord As Integer
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()


        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                Row.Height = 36
                ord = DGVolcadosCestas.Rows.Count - 2
                For o = 0 To RS.CuantosCampos - 1
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                DGVolcadosCestas.Rows.Add(Row)
            Next
        End If
    End Sub
    Private Sub CargarCestasFabricadas(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        ' Dim ord As Integer
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()
        'AAAA

        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                Row.Height = 36
                For o = 0 To RS.CuantosCampos - 1
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                DGFabricadoCestas.Rows.Add(Row)
            Next
        End If
    End Sub
    Private Sub CargarSeguimiento(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        '   Dim ord As Integer
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()


        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                Row.Height = 36
                For o = 0 To RS.CuantosCampos - 1
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                DGSeguimiento.Rows.Add(Row)
            Next
        End If
    End Sub
    Private Sub CargarTrazabilidad(Sql)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        ' Dim ord As Integer
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()


        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                Row.Height = 36
                For o = 0 To RS.CuantosCampos - 1
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RS.Valor(RS.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                DGTrazabilidad.Rows.Add(Row)
            Next
        End If
    End Sub

    'Private Sub trazabilidad()
    '    Dim Palet As String
    '    Dim calibre As String
    '    Dim ColCal As Integer
    '    Dim Rs As New cnRecordset.CnRecordset
    '    Dim FilaInit As Integer
    '    Dim NoSalir As Boolean
    '    Dim TotalKilos As Double
    '    Dim aAlba() As String
    '    Dim aBulto() As String
    '    Dim aHora() As String
    '    Dim aPeso() As Double
    '    Dim Orden As Integer
    '    Dim Fila As Integer
    '    Dim Row As Integer
    '    Dim PesoFabricado As Double
    '    Dim FilaInicial As Integer
    '    Dim FFinal As Integer
    '    Dim Sql As String
    '    Dim PesosGastados() As Double
    '    Dim PesoActual As Double

    '    ReDim PesosGastados(DGVolcado.ColumnCount)
    '    For i = 0 To DGVolcado.ColumnCount
    '        PesosGastados(i) = 0
    '    Next

    '    For i = 1 To DGFabricados.Rows.Count - 1

    '        Palet = DGFabricados.Rows(i).Cells(0).Value

    '        If CLng(Palet) > 1000000 Then
    '            Sql = "SELECT p.*, o.calibre_tecnico, o.calibre_comercial from Palets p LEFT JOIN ordenes_confeccion o ON o.empresa=p.empresa AND o.numero_orden = p.orden_confeccion WHERE p.empresa='" & ObjetoGlobal.EmpresaActual & "' AND p.ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND p.codigo_palet='" & Palet & "'"
    '        Else
    '            Sql = "SELECT codigo_calibre as calibre_tecnico from palets_precal_fru WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' AND ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND codigo_palet='" & Palet & "'"
    '        End If

    '        Rs.Open(Sql, ObjetoGlobal.cn)

    '        If Not Rs.EOF Then
    '            ColCal = ColumnaCalibre(Rs!calibre_tecnico)
    '            'FilaInit = FilaInicio(DGFabricados.Rows(i).Cells(2).Value) '.TextMatrix(MSHFabricados.RowSel, 2))
    '            FFinal = FilaFinal(DGFabricados.Rows(i).Cells(2).Value) '.TextMatrix(MSHFabricados.RowSel, 2))

    '            NoSalir = True
    '            TotalKilos = CDbl(DGFabricados.Rows(i).Cells(1).Value)
    '            PesoFabricado = TotalKilos
    '            Orden = 0
    '            ReDim aAlba(0)
    '            ReDim aBulto(0)
    '            ReDim aPeso(0)
    '            ReDim aHora(0)

    '            FilaInicial = FilaInit

    '            While NoSalir
    '                Orden = Orden + 1
    '                If (CDbl(PesoFabricado) - CDbl(DGFabricados.Rows(i).Cells(ColCal).Value)) < 0 Then
    '                    NoSalir = False
    '                Else
    '                    PesoFabricado = PesoFabricado - CDbl(DGFabricados.Rows(i).Cells(ColCal).Value)
    '                End If
    '                FilaInit = FilaInit - 1
    '                If FilaInit < 2 Then
    '                    NoSalir = False
    '                End If
    '            End While
    '            NoSalir = True
    '            Orden = 0
    '            While NoSalir
    '                'Orden = Orden + 1
    '                If CDbl(DGFabricados.Rows(i).Cells(ColCal).Value) <> 0 Then
    '                    ReDim Preserve aAlba(Orden)
    '                    ReDim Preserve aBulto(Orden)
    '                    ReDim Preserve aPeso(Orden)
    '                    ReDim Preserve aHora(Orden)

    '                    aAlba(Orden) = DGVolcado.Rows(FilaInit).Cells(2).Value
    '                    aBulto(Orden) = DGVolcado.Rows(FilaInit).Cells(3).Value
    '                    aHora(Orden) = DGVolcado.Rows(FilaInit).Cells(1).Value

    '                    If (CDbl(TotalKilos) - CDbl(DGVolcado.Rows(FilaInit).Cells(ColCal).Value)) < 0 Then
    '                        aPeso(Orden) = TotalKilos
    '                        NoSalir = False
    '                    Else
    '                        aPeso(Orden) = CDbl(DGVolcado.Rows(FilaInit).Cells(ColCal).Value)
    '                        TotalKilos = TotalKilos - CDbl(DGVolcado.Rows(FilaInit).Cells(ColCal).Value)
    '                    End If
    '                    FilaInit = FilaInit + 1
    '                    If FilaInit > FilaInicial Then
    '                        NoSalir = False
    '                    End If
    '                    Orden = Orden + 1
    '                End If
    '                If Orden > 720 Then
    '                    NoSalir = False
    '                End If
    '            End While
    '            If Not NoSalir Then
    '                PintarTrazabilidad(Palet, DGFabricados.Rows(i).Cells(2).Value, CDbl(DGFabricados.Rows(i).Cells(1).Value), aAlba, aBulto, aPeso, aHora, Row)
    '            End If
    '        End If
    '        Rs.Close()
    '    Next

    'End Sub

    Private Function ColumnaCalibre(cal) As Integer
        Dim i As Integer
        Dim aCali() As String

        ReDim aCali(0)
        aCali = Split(Trim(cal), "/")

        For i = 10 To DGVolcado.ColumnCount - 1
            If Trim(DGVolcado.Columns(i).HeaderText.Replace("Cal", "")) = Trim(aCali(0)) Then
                Return i
            End If
        Next

    End Function

    Private Function FilaInicio(Hora) As Integer
        Dim i As Integer
        Dim Fila As Integer
        Dim msg As String

        For i = 1 To DGVolcado.ColumnCount
            If aMinutos(Trim(Mid(DGVolcado.Rows(i).Cells(1).Value, 1))) > aMinutos(Hora) Then
                msg = msg + Trim(Mid(DGVolcado.Rows(i).Cells(1).Value, 1)) + "   " + Hora + Chr(13) + Chr(10)
            Else
                Fila = i
                msg = msg + Trim(Mid(DGVolcado.Rows(i).Cells(1).Value, 1)) + "   " + Hora + Chr(13) + Chr(10)
            End If
        Next
        MsgBox(msg)
        Return i

    End Function

    Private Function FilaFinal(Hora) As Integer
        Dim i As Integer
        ' Dim Fila As Integer
        ' Dim msg As String

        For i = 0 To DGVolcado.Rows.Count - 2
            If aMinutos(Trim(Mid(DGVolcado.Rows(i).Cells(1).Value, 1))) > aMinutos(Hora) Then
                Return i - 1
                'msg = msg + Trim(Mid(DGVolcado.Rows(i).Cells(1).Value, 1)) + "   " + Hora + Chr(13) + Chr(10)
            Else
                'Fila = i
                'msg = msg + Trim(Mid(DGVolcado.Rows(i).Cells(1).Value, 1)) + "   " + Hora + Chr(13) + Chr(10)
            End If
        Next
        'MsgBox(msg)
        Return i

    End Function

    Private Function aMinutos(ByVal Hora As String) As Long
        Dim sminutos As String
        Dim aHoras() As String
        If Trim(Hora) = "" Then
            Return 0
        End If
        aHoras = Hora.Split(":")
        'Hora.ToString("HH:mm")
        sminutos = (CInt(aHoras(0)) * 60) + CInt(aHoras(1))
        Return sminutos
    End Function

    Private Sub PintarTrazabilidad(ByVal Palet As String, ByVal HoraSalida As String, ByVal PesoPalet As String, ByVal aAlba() As String, ByVal aBulto() As String, ByVal aPeso() As Double, ByVal Hora() As String, ByRef Row As Integer, ByRef aGlobal() As String, ByRef aGrasp() As String, ByVal aPalets() As String)
        Dim Rowt As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim i As Integer
        Dim sql As String
        Dim RS As New cnRecordset.CnRecordset
        Dim o As Integer

        'Rowt = New DataGridViewRow
        For i = 1 To UBound(aAlba)
            If aAlba(i) > 0 Then
                Rowt = New DataGridViewRow
                If i = 1 Then
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = HoraSalida
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Palet
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = PesoPalet
                    Rowt.Cells.Add(Cell)
                Else
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = " "
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = " "
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = " "
                    Rowt.Cells.Add(Cell)
                End If
                Cell = New DataGridViewTextBoxCell
                Cell.Value = " "
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aAlba(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aBulto(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aPeso(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = Hora(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aGlobal(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aGrasp(i)
                Rowt.Cells.Add(Cell)
                DGTrazabilidad.Rows.Add(Rowt)
                'Else
                '    If Trim(aPalets(i)) <> "" Then
                '        sql = "SELECT DISTINCT  numero_albaran FROM palets_precal_f_t WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "'  AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' and codigo_palet = '" & aPalets(i) & "'"
                '        If RS.Open(sql, ObjetoGlobal.cn) Then
                '            For o = 1 To RS.RecordCount
                '                RS.AbsolutePosition = o
                '                Rowt = New DataGridViewRow
                '                If i = 1 And o = 1 Then
                '                    Cell = New DataGridViewTextBoxCell
                '                    Cell.Value = HoraSalida
                '                    Rowt.Cells.Add(Cell)
                '                    Cell = New DataGridViewTextBoxCell
                '                    Cell.Value = Palet
                '                    Rowt.Cells.Add(Cell)
                '                    Cell = New DataGridViewTextBoxCell
                '                    Cell.Value = PesoPalet
                '                    Rowt.Cells.Add(Cell)
                '                Else
                '                    Cell = New DataGridViewTextBoxCell
                '                    Cell.Value = " "
                '                    Rowt.Cells.Add(Cell)
                '                    Cell = New DataGridViewTextBoxCell
                '                    Cell.Value = " "
                '                    Rowt.Cells.Add(Cell)
                '                    Cell = New DataGridViewTextBoxCell
                '                    Cell.Value = " "
                '                    Rowt.Cells.Add(Cell)
                '                End If
                '                Cell = New DataGridViewTextBoxCell
                '                Cell.Value = " "
                '                Rowt.Cells.Add(Cell)
                '                Cell = New DataGridViewTextBoxCell
                '                Cell.Value = RS!numero_albaran
                '                Rowt.Cells.Add(Cell)
                '                Cell = New DataGridViewTextBoxCell
                '                Cell.Value = aBulto(i)
                '                Rowt.Cells.Add(Cell)
                '                Cell = New DataGridViewTextBoxCell
                '                If o = 1 Then
                '                    Cell.Value = aPeso(i)
                '                Else
                '                    Cell.Value = 0
                '                End If
                '                Rowt.Cells.Add(Cell)
                '                Cell = New DataGridViewTextBoxCell
                '                Cell.Value = Hora(i)
                '                Rowt.Cells.Add(Cell)
                '                Cell = New DataGridViewTextBoxCell
                '                Cell.Value = aGlobal(i)
                '                Rowt.Cells.Add(Cell)
                '                Cell = New DataGridViewTextBoxCell
                '                Cell.Value = aGrasp(i)
                '                Rowt.Cells.Add(Cell)
                '                DGTrazabilidad.Rows.Add(Rowt)
                '            Next
                '        End If
                '        RS.Close()

                'End If
            End If
        Next
    End Sub
    Private Sub trazabilidad1()
        Dim Palet As String
        ' Dim calibre As String
        Dim ColCal As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim FilaInit As Integer
        Dim NoSalir As Boolean
        Dim TotalKilos As Double
        Dim aAlba() As String
        Dim aBulto() As String
        Dim aHora() As String
        Dim aPeso() As Double
        Dim aPalets() As String
        Dim Orden As Integer
        '  Dim Fila As Integer
        Dim Row As Integer
        Dim PesoFabricado As Double
        Dim FilaInicial As Integer
        Dim FFinal As Integer
        Dim Sql As String
        Dim PesosGastados() As Double
        Dim PesoActual As Double
        Dim DiferenciaAlbaran As Double
        Dim i As Integer
        Dim o As Integer
        Dim EsPrecalibrado As Boolean
        Dim aGlobal() As String
        Dim aGrasp() As String
        Dim Confec As String

        ReDim PesosGastados(DGVolcado.ColumnCount)
        For i = 0 To DGVolcado.ColumnCount
            PesosGastados(i) = 0
        Next

        For i = 1 To DGFabricados.Rows.Count - 1

            Palet = DGFabricados.Rows(i).Cells(0).Value
            Confec = DGFabricados.Rows(i).Cells(4).Value


            If CLng(Palet) > 1000000 Then
                EsPrecalibrado = False
                Sql = "SELECT p.*, o.calibre_tecnico, o.calibre_comercial, o.codigo_variedad from Palets p LEFT JOIN ordenes_confeccion o ON o.empresa=p.empresa AND o.numero_orden = p.orden_confeccion WHERE p.empresa='" & ObjetoGlobal.EmpresaActual & "' AND  p.codigo_palet='" & Palet & "'"
            Else
                EsPrecalibrado = True
                Sql = "SELECT codigo_calibre as calibre_comercial, codigo_variedad from palets_precal_fru WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' AND codigo_palet='" & Palet & "'"
            End If

            Rs.Open(Sql, ObjetoGlobal.cn)

            If Not Rs.EOF Then
                If EsPrecalibrado Then
                    ColCal = ObtenColumnaPreCalibre(Rs!codigo_variedad, Rs!calibre_tecnico, Confec)
                Else
                    ColCal = ObtenColumnaCalibre(Rs!codigo_variedad, Rs!calibre_comercial, Confec)
                End If
                FFinal = FilaFinal(DGFabricados.Rows(i).Cells(2).Value) '.TextMatrix(MSHFabricados.RowSel, 2))

                NoSalir = True
                TotalKilos = CDbl(DGFabricados.Rows(i).Cells(1).Value)
                PesoFabricado = TotalKilos
                Orden = 0
                ReDim aAlba(0)
                ReDim aBulto(0)
                ReDim aPeso(0)
                ReDim aHora(0)
                ReDim aGlobal(0)
                ReDim aGrasp(0)
                ReDim aPalets(0)

                FilaInicial = FilaInit
                PesoActual = 0
                DiferenciaAlbaran = -1
                For o = 1 To FFinal
                    'Orden = Orden + 1

                    If CDbl(DGVolcado.Rows(o).Cells(ColCal).Value) <> 0 Then
                        PesoActual = PesoActual + CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value))
                        If (PesosGastados(ColCal) <= (PesoActual - CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value)))) Then
                            If DiferenciaAlbaran = -1 Then
                                DiferenciaAlbaran = (PesoActual - CDbl(CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value)))) - PesosGastados(ColCal)
                            Else
                                DiferenciaAlbaran = 0
                            End If

                            ReDim Preserve aAlba(Orden)
                            ReDim Preserve aBulto(Orden)
                            ReDim Preserve aPeso(Orden)
                            ReDim Preserve aHora(Orden)
                            ReDim Preserve aGlobal(Orden)
                            ReDim Preserve aGrasp(Orden)
                            ReDim Preserve aPalets(Orden)

                            aAlba(Orden) = DGVolcado.Rows(o).Cells(2).Value
                            aBulto(Orden) = DGVolcado.Rows(o).Cells(3).Value
                            aHora(Orden) = DGVolcado.Rows(o).Cells(1).Value
                            aGlobal(Orden) = DGVolcado.Rows(o).Cells(21).Value
                            aGrasp(Orden) = DGVolcado.Rows(o).Cells(22).Value
                            aPalets(Orden) = DGVolcado.Rows(o).Cells(8).Value

                            If DiferenciaAlbaran > 0 Then
                                aPeso(Orden) = DiferenciaAlbaran
                                TotalKilos = TotalKilos - DiferenciaAlbaran
                                DiferenciaAlbaran = 0
                            ElseIf (CDbl(TotalKilos) - CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value))) < 0 Then
                                aPeso(Orden) = TotalKilos
                                TotalKilos = 0
                                NoSalir = False
                                Exit For
                            Else
                                aPeso(Orden) = CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value))
                                TotalKilos = TotalKilos - CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value))
                            End If
                            PesosGastados(ColCal) = PesosGastados(ColCal) + aPeso(Orden)
                            If aPeso(Orden) > 0 Then
                                NoSalir = False
                            End If
                            Orden = Orden + 1
                        End If
                    End If
                Next
                If Not NoSalir Then
                    PintarTrazabilidad(Palet, DGFabricados.Rows(i).Cells(2).Value, CDbl(DGFabricados.Rows(i).Cells(1).Value), aAlba, aBulto, aPeso, aHora, Row, aGlobal, aGrasp, aPalets)
                End If
            End If
            Rs.Close()
        Next

    End Sub
    Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal titulo As String)
        Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
        m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
        With objHojaExcel
            .Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible

            .Activate()
            'Encabezado  
            .Range("A1:L1").Merge()
            .Range("A1:L1").Value = titulo
            .Range("A1:L1").Font.Bold = True
            .Range("A1:L1").Font.Size = 15
            'Copete  
            .Range("A2:L2").Merge()
            .Range("A2:L2").Value = titulo
            .Range("A2:L2").Font.Bold = True
            .Range("A2:L2").Font.Size = 12

            Const primeraLetra As Char = "A"
            Const primerNumero As Short = 3
            Dim Letra As Char, UltimaLetra As Char
            Dim Numero As Integer, UltimoNumero As Integer
            Dim cod_letra As Byte = Asc(primeraLetra) - 1
            Dim sepDec As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
            Dim sepMil As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator
            'Establecer formatos de las columnas de la hija de cálculo  
            Dim strColumna As String = ""
            Dim LetraIzq As String = ""
            Dim cod_LetraIzq As Byte = Asc(primeraLetra) - 1
            Letra = primeraLetra
            Numero = primerNumero
            Dim objCelda As Microsoft.Office.Interop.Excel.Range
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        cod_LetraIzq += 1
                        LetraIzq = Chr(cod_LetraIzq)
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                    strColumna = LetraIzq + Letra + Numero.ToString
                    objCelda = .Range(strColumna, Type.Missing)
                    objCelda.Value = c.HeaderText
                    objCelda.EntireColumn.Font.Size = 8
                    'objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format  
                    If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        objCelda.EntireColumn.NumberFormat = "#" + sepMil + "0" + sepDec + "00"
                    End If
                End If
            Next

            Dim objRangoEncab As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + Numero.ToString, LetraIzq + Letra + Numero.ToString)
            objRangoEncab.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
            UltimaLetra = Letra
            Dim UltimaLetraIzq As String = LetraIzq

            'CARGA DE DATOS  
            Dim i As Integer = Numero + 1

            For Each reg As DataGridViewRow In DataGridView1.Rows
                LetraIzq = ""
                cod_LetraIzq = Asc(primeraLetra) - 1
                Letra = primeraLetra
                cod_letra = Asc(primeraLetra) - 1
                For Each c As DataGridViewColumn In DataGridView1.Columns
                    If c.Visible Then
                        If Letra = "Z" Then
                            Letra = primeraLetra
                            cod_letra = Asc(primeraLetra)
                            cod_LetraIzq += 1
                            LetraIzq = Chr(cod_LetraIzq)
                        Else
                            cod_letra += 1
                            Letra = Chr(cod_letra)
                        End If
                        strColumna = LetraIzq + Letra
                        ' acá debería realizarse la carga  
                        .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                        '.Cells(i, strColumna) = IIf(IsDBNull(reg.(c.DataPropertyName)), c.DefaultCellStyle.NullValue, reg(c.DataPropertyName))  
                        '.Range(strColumna + i, strColumna + i).In()  

                    End If
                Next
                Dim objRangoReg As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + i.ToString, strColumna + i.ToString)
                objRangoReg.Rows.BorderAround()
                objRangoReg.Select()
                i += 1
            Next
            UltimoNumero = i

            'Dibujar las líneas de las columnas  
            LetraIzq = ""
            cod_LetraIzq = Asc("A")
            cod_letra = Asc(primeraLetra)
            Letra = primeraLetra
            For Each c As DataGridViewColumn In DataGridView1.Columns
                If c.Visible Then
                    objCelda = .Range(LetraIzq + Letra + primerNumero.ToString, LetraIzq + Letra + (UltimoNumero - 1).ToString)
                    objCelda.BorderAround()
                    If Letra = "Z" Then
                        Letra = primeraLetra
                        cod_letra = Asc(primeraLetra)
                        LetraIzq = Chr(cod_LetraIzq)
                        cod_LetraIzq += 1
                    Else
                        cod_letra += 1
                        Letra = Chr(cod_letra)
                    End If
                End If
            Next

            'Dibujar el border exterior grueso  
            'Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            'objRango.Select()
            'objRango.Columns.AutoFit()
            'objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
        End With

        m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
    End Sub

    Private Sub trazabilidad2()
        Dim Palet As String
        'Dim calibre As String
        Dim ColCal As Integer()
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs1 As New cnRecordset.CnRecordset
        Dim RsP As New cnRecordset.CnRecordset
        Dim RsR As New cnRecordset.CnRecordset
        Dim RsTP As New cnRecordset.CnRecordset
        Dim RsRep As New cnRecordset.CnRecordset
        Dim FilaInit As Integer
        Dim NoSalir As Boolean
        Dim TotalKilos As Double
        Dim aAlba() As String
        Dim aBulto() As String
        Dim aHora() As String
        Dim aPeso() As Double
        Dim Palets() As String
        Dim Orden As Integer
        'Dim Fila As Integer
        Dim Row As Integer
        Dim PesoFabricado As Double
        Dim FilaInicial As Integer
        Dim FFinal As Integer
        Dim Sql As String
        Dim PesosGastados() As Double
        Dim PesoActual As Double
        Dim DiferenciaAlbaran As Double
        Dim i As Integer
        Dim o As Integer
        Dim t As Integer
        Dim Confec As String
        Dim EsPrecalibrado As Boolean
        Dim aGlobal() As String
        Dim aGrasp() As String
        Dim aRestos(,) As String
        Dim PesoRestos As Double
        Dim PesoInicial As Double
        Dim ultimo As Boolean
        Dim no As Integer
        Dim HayRestros As Boolean
        Dim sConfeccion As String
        Dim sCodigo_variedad As String
        Dim Resto_orden_confeccion As Long
        Dim Resto_contador As Integer
        Dim j As Integer
        Dim TieneItem As Double
        Dim peso_calidad As Double
        Dim esrepaletizado As Boolean
        Dim Palet_origen As String
        Dim EsSegunda As Boolean = False

        CabeceraTrazabilidad()

        ReDim PesosGastados(DGVolcado.ColumnCount)
        For i = 0 To DGVolcado.ColumnCount
            PesosGastados(i) = 0
        Next

        For i = 0 To DGFabricados.Rows.Count - 1
            esrepaletizado = False
            Palet_origen = ""
            EsSegunda = False

            Palet = DGFabricados.Rows(i).Cells(0).Value
            Confec = DGFabricados.Rows(i).Cells(4).Value
            If Trim(DGFabricados.Rows(i).Cells(12).Value) = "R" Then
                esrepaletizado = True
                If RsRep.Open("SELECT * FROM repaletizacion WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and palet_final = '" & Palet & "'", ObjetoGlobal.cn) Then
                    Palet_origen = RsRep!palet_inicial
                Else
                    esrepaletizado = False
                End If
                RsRep.Close()
            End If
            PesoRestos = 0
            PesoInicial = 0
            'If Trim(Palet) = "111191204" Then
            ' MsgBox("parar")
            'End If
            If CLng(Palet) > 1000000 Then
                Sql = "SELECT p.*, o.calibre_tecnico, o.calibre_comercial, o.codigo_variedad, o.categoria from Palets p LEFT JOIN ordenes_confeccion o ON o.empresa=p.empresa AND o.numero_orden = p.orden_confeccion WHERE p.empresa='" & ObjetoGlobal.EmpresaActual & "' AND  p.codigo_palet='" & Palet & "'"
                EsPrecalibrado = False
                If Trim(Palet) = "111171501" Then
                    MsgBox("palet encontrado")
                End If
            Else
                Sql = "SELECT codigo_calibre as calibre_tecnico, codigo_variedad from palets_precal_fru WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_palet='" & Palet & "'"
                EsPrecalibrado = True
            End If

            Rs.Open(Sql, ObjetoGlobal.cn)


            If Rs.RecordCount > 0 Then

                If CLng(Palet) > 1000000 Then
                    If Rs!categoria = "ST" Then
                        EsSegunda = True
                    End If
                End If

                Orden = 0
                ReDim aAlba(0)
                ReDim aBulto(0)
                ReDim aPeso(0)
                ReDim aHora(0)
                ReDim aGlobal(0)
                ReDim aGrasp(0)
                ReDim Palets(0)

                PesoRestos = 0

                ' Ver si hay restos
                HayRestros = False
                Resto_orden_confeccion = 0
                Resto_contador = 0

                If Not EsPrecalibrado And Not EsSegunda Then
                    Sql = "SELECT p.orden_confeccion, o.cajas_palet, o.peso_palet, o.fecha_orden FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa and o.numero_orden = p.orden_confeccion WHERE p.empresa='" & ObjetoGlobal.EmpresaActual & "' and p.codigo_palet = '" & Palet & "'"
                    RsP.Open(Sql, ObjetoGlobal.cn)
                    If RsP.RecordCount > 0 Then
                        Sql = "SELECT * FROM ord_confec_restos WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' AND palet = '" & Palet & "' order by contador"
                        If RsR.Open(Sql, ObjetoGlobal.cn) Then ' Tiene restos 
                            If RsR.RecordCount > 0 Then
                                Resto_orden_confeccion = RsR!numero_orden
                                Resto_contador = RsR!contador
                                HayRestros = True
                            End If
                        End If
                        If HayRestros Then
                            PesoRestos = Math.Round((RsP!peso_palet / RsP!cajas_palet) * RsR!cajas, 0)
                            aRestos = CargarArrayRestos(RsP!fecha_orden)
                            ColCal = ObtenColumnaCalibre(Rs!codigo_variedad, Rs!calibre_comercial, Confec)
                            'FFinal = FilaFinal(DGFabricados.Rows(i).Cells(2).Value)
                            PesoInicial = PesoRestos

                            For no = 1 To UBound(ColCal)

                                For o = UBound(aRestos) To 1 Step -1
                                    If CDbl("0" & aRestos(o, ColCal(no))) > 0 Then
                                        If CDbl("0" & aRestos(o, ColCal(no))) <> 0 Or Trim(Palet) <> "" Then
                                            PesoActual = PesoActual + CDbl("0" & aRestos(o, ColCal(no)))
                                            'If (PesoInicial - CDbl("0" & aRestos(o, ColCal(no)))) <= 0 Then
                                            '    ultimo = True
                                            '    DiferenciaAlbaran = PesoInicial
                                            'Else
                                            '    DiferenciaAlbaran = DiferenciaAlbaran - CDbl("0" & aRestos(o, ColCal(no)))
                                            'End If
                                            'PesoInicial = PesoInicial - CDbl("0" & aRestos(o, ColCal(no)))
                                            'DiferenciaAlbaran = 0

                                            TieneItem = False
                                            For j = 1 To UBound(aAlba)
                                                If aAlba(j) = aRestos(o, 2) And aBulto(j) = aRestos(o, 3) Then
                                                    TieneItem = True
                                                    Exit For
                                                End If
                                            Next
                                            If Not TieneItem Then
                                                Orden = Orden + 1
                                                j = Orden
                                                ReDim Preserve aAlba(Orden)
                                                ReDim Preserve aBulto(Orden)
                                                ReDim Preserve aPeso(Orden)
                                                ReDim Preserve aHora(Orden)
                                                ReDim Preserve aGlobal(Orden)
                                                ReDim Preserve aGrasp(Orden)
                                                ReDim Preserve Palets(Orden)
                                            End If

                                            aAlba(j) = aRestos(o, 2)
                                            aBulto(j) = aRestos(o, 3)
                                            aHora(j) = aRestos(o, 1)
                                            Palets(j) = aRestos(o, 7)


                                            'aGlobal(Orden) = aRestos(o, 21)
                                            'aGrasp(Orden) = aRestos(o, 22)
                                            'Palets(Orden) = aRestos(o, 23)

                                            peso_calidad = CDbl("0" & aRestos(o, ColCal(no)))

                                            ultimo = False

                                            If DiferenciaAlbaran > 0 Then
                                                aPeso(j) = DiferenciaAlbaran
                                                TotalKilos = TotalKilos - DiferenciaAlbaran
                                                DiferenciaAlbaran = 0
                                            ElseIf (CDbl(PesoInicial) - CDbl("0" & aRestos(o, ColCal(no)))) < 0 Then
                                                'aPeso(Orden) = TotalKilos
                                                aPeso(j) = PesoInicial
                                                PesoInicial = 0
                                                ultimo = True
                                            Else
                                                aPeso(j) = aPeso(j) + CDbl("0" & aRestos(o, ColCal(no)))
                                                PesoInicial = PesoInicial - CDbl("0" & aRestos(o, ColCal(no)))
                                            End If
                                            '   PesosGastados(ColCal) = C(ColCal) + aPeso(Orden)
                                            'If aPeso(Orden) > 0 Then
                                            'If aPeso(j) > 0 Then
                                            'NoSalir = False
                                            'End If
                                            'Orden = Orden + 1
                                            If ultimo Then
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next
                                If ultimo Then
                                    Exit For
                                End If
                            Next
                        End If
                        RsR.Close()
                    End If
                    RsP.Close()
                End If

                ' Hasta aquí los restos

                If EsPrecalibrado Then
                    ColCal = ObtenColumnaPreCalibre(Rs!codigo_variedad, Rs!calibre_tecnico, Confec)
                ElseIf EsSegunda Then
                    ReDim ColCal(1)
                ElseIf esrepaletizado Then
                    ReDim ColCal(0)
                Else
                    ColCal = ObtenColumnaCalibre(Rs!codigo_variedad, Rs!calibre_comercial, Confec)
                End If
                FFinal = FilaFinal(DGFabricados.Rows(i).Cells(2).Value) '.TextMatrix(MSHFabricados.RowSel, 2))

                NoSalir = True
                TotalKilos = CDbl(DGFabricados.Rows(i).Cells(1).Value) - PesoRestos + PesoInicial
                PesoFabricado = TotalKilos '- PesoRestos

                FilaInicial = FilaInit
                PesoActual = 0
                PesoInicial = 0
                DiferenciaAlbaran = -1

                For no = 1 To UBound(ColCal)

                    For o = FFinal To 0 Step -1
                        'Orden = Orden + 1
                        If CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal(no)).Value)) > 0 Then

                            If CDbl("0" & DGVolcado.Rows(o).Cells(ColCal(no)).Value) <> 0 Or Trim(Palet) <> "" Then
                                PesoActual = PesoActual + CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal(no)).Value))
                                ' If (PesosGastados(ColCal) <= (PesoActual - CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value)))) Then
                                'If DiferenciaAlbaran = -1 Then
                                'DiferenciaAlbaran = (PesoActual - CDbl(CDbl("0" & Trim(DGVolcado.Rows(o).Cells(ColCal).Value)))) - PesosGastados(ColCal)
                                'Else
                                DiferenciaAlbaran = 0
                                '       End If

                                TieneItem = False
                                For j = 1 To UBound(aAlba)
                                    If aAlba(j) = DGVolcado.Rows(o).Cells(2).Value And aBulto(j) = DGVolcado.Rows(o).Cells(3).Value Then
                                        TieneItem = True
                                        Exit For
                                    End If
                                Next

                                If Not TieneItem Then
                                    Orden = Orden + 1
                                    j = Orden
                                    ReDim Preserve aAlba(Orden)
                                    ReDim Preserve aBulto(Orden)
                                    ReDim Preserve aPeso(Orden)
                                    ReDim Preserve aHora(Orden)
                                    ReDim Preserve aGlobal(Orden)
                                    ReDim Preserve aGrasp(Orden)
                                    ReDim Preserve Palets(Orden)
                                    aPeso(Orden) = 0
                                End If

                                aAlba(j) = DGVolcado.Rows(o).Cells(2).Value
                                aBulto(j) = DGVolcado.Rows(o).Cells(3).Value
                                aGlobal(j) = "" & DGVolcado.Rows(o).Cells(21).Value
                                aGrasp(j) = "" & DGVolcado.Rows(o).Cells(22).Value
                                aHora(j) = DGVolcado.Rows(o).Cells(1).Value
                                Palets(j) = DGVolcado.Rows(o).Cells(7).Value

                                peso_calidad = CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal(no)).Value)) + aPeso(j)

                                If peso_calidad = 0 Then
                                    MsgBox("peso 0")
                                End If

                                If DiferenciaAlbaran > 0 Then
                                    aPeso(j) = DiferenciaAlbaran
                                    TotalKilos = TotalKilos - DiferenciaAlbaran
                                    DiferenciaAlbaran = 0
                                ElseIf (CDbl(TotalKilos) - CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal(no)).Value))) < 0 Then
                                    aPeso(j) = TotalKilos
                                    TotalKilos = 0
                                    NoSalir = False
                                Else
                                    aPeso(j) = aPeso(j) + CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal(no)).Value))
                                    TotalKilos = TotalKilos - CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal(no)).Value))
                                End If
                                '   PesosGastados(ColCal) = PesosGastados(ColCal) + aPeso(Orden)
                                If aPeso(j) > 0 Then
                                    'NoSalir = False
                                End If

                                Orden = j

                                If aAlba(j) = 0 And (DGVolcado.Rows(o).Cells(7).Value <= 4000 Or (DGVolcado.Rows(o).Cells(7).Value > 200000 And DGVolcado.Rows(o).Cells(7).Value <= 249000)) And Not esrepaletizado And Not EsSegunda Then
                                    RsTP.Open("SELECT DISTINCT numero_albaran FROM palets_precal_f_t WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND codigo_palet = " & Trim(DGVolcado.Rows(o).Cells(7).Value), ObjetoGlobal.cn)

                                    While Not RsTP.EOF
                                        If Orden <> j Then
                                            ReDim Preserve aAlba(Orden)
                                            ReDim Preserve aBulto(Orden)
                                            ReDim Preserve aPeso(Orden)
                                            ReDim Preserve aHora(Orden)
                                            ReDim Preserve aGlobal(Orden)
                                            ReDim Preserve aGrasp(Orden)
                                            ReDim Preserve Palets(Orden)
                                        End If
                                        aAlba(Orden) = RsTP!numero_albaran
                                        Orden = Orden + 1
                                        RsTP.MoveNext()
                                    End While
                                    RsTP.Close()

                                ElseIf aAlba(j) = 0 And (DGVolcado.Rows(o).Cells(7).Value >= 250000 And DGVolcado.Rows(o).Cells(7).Value <= 299999) And EsSegunda Then
                                    RsTP.Open("SELECT DISTINCT numero_albaran FROM palets_segunda_t WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND codigo_palet = " & Trim(DGVolcado.Rows(o).Cells(7).Value), ObjetoGlobal.cn)

                                    While Not RsTP.EOF
                                        If Orden <> j Then
                                            ReDim Preserve aAlba(Orden)
                                            ReDim Preserve aBulto(Orden)
                                            ReDim Preserve aPeso(Orden)
                                            ReDim Preserve aHora(Orden)
                                            ReDim Preserve aGlobal(Orden)
                                            ReDim Preserve aGrasp(Orden)
                                            ReDim Preserve Palets(Orden)
                                        End If
                                        aAlba(Orden) = RsTP!numero_albaran
                                        Orden = Orden + 1
                                        RsTP.MoveNext()
                                    End While
                                    RsTP.Close()
                                End If
                            End If
                        End If
                        If Not NoSalir Then
                            Exit For
                        End If
                    Next
                    If Not NoSalir And TotalKilos < 1 Then
                        Exit For
                    End If
                Next

                If esrepaletizado Then
                    If RsRep.Open("SELECT DISTINCT numero_albaran FROM palets_trazab WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and codigo_palet = '" & Palet_origen & "'", ObjetoGlobal.cn) Then
                        Orden = 1
                        While Not RsRep.EOF
                            If Orden > 0 Then
                                ReDim Preserve aAlba(Orden)
                                ReDim Preserve aBulto(Orden)
                                ReDim Preserve aPeso(Orden)
                                ReDim Preserve aHora(Orden)
                                ReDim Preserve aGlobal(Orden)
                                ReDim Preserve aGrasp(Orden)
                                ReDim Preserve Palets(Orden)
                            End If
                            aAlba(Orden) = RsRep!numero_albaran
                            Orden = Orden + 1
                            RsRep.MoveNext()
                        End While
                    End If
                    RsRep.Close()
                End If

                'If Not NoSalir Then
                PintarTrazabilidad(Palet, DGFabricados.Rows(i).Cells(2).Value, CDbl(DGFabricados.Rows(i).Cells(1).Value), aAlba, aBulto, aPeso, aHora, Row, aGlobal, aGrasp, Palets)
                If GrabarTra.Checked Then
                    GrabarTrazabilidad(Palet, DGFabricados.Rows(i).Cells(2).Value, CDbl(DGFabricados.Rows(i).Cells(1).Value), aAlba, aBulto, aPeso, aHora, Row)
                End If
                'End If
            End If
            Rs.Close()
        Next

    End Sub
    Private Sub GrabarTrazabilidad(ByVal Palet As String, ByVal HoraSalida As String, ByVal PesoPalet As String, ByVal aAlba() As String, ByVal aBulto() As String, ByVal aPeso() As Double, ByVal Hora() As String, ByRef Row As Integer)
        Dim i As Integer
        Dim sql As String
        Dim RsT As New cnRecordset.CnRecordset
        Dim RsP As New cnRecordset.CnRecordset
        Dim cont As Integer

        If CDbl(Palet) > 1000000 Then ' Palet normal
            sql = "SELECT * FROM palets_trazab WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual.Trim & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual.Trim & "' and codigo_palet='" & Palet & "'"
            RsT.Open(sql, ObjetoGlobal.cn, True)
            While RsT.RecordCount > 0
                RsT.MoveFirst()
                RsT.Delete()
            End While
            RsT.Close()
        Else
            sql = "SELECT * FROM palets_precal_f_t WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual.Trim & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual.Trim & "' and codigo_palet='" & Palet & "'"
            RsP.Open(sql, ObjetoGlobal.cn, True)
            While RsP.RecordCount > 0
                RsP.MoveFirst()
                RsP.Delete()
            End While
            RsP.Close()
        End If

        sql = "SELECT * FROM palets_trazab WHERE 1=0"
        RsT.Open(sql, ObjetoGlobal.cn, True)
        sql = "SELECT * FROM palets_precal_f_t WHERE 1=0"
        RsP.Open(sql, ObjetoGlobal.cn, True)
        cont = 0
        For i = 1 To UBound(aAlba)
            If CDbl(Palet) > 1000000 Then ' Palet normal
                cont = cont + 1
                RsT.AddNew()
                RsT!empresa = ObjetoGlobal.EmpresaActual
                RsT!codigo_palet = Palet
                RsT!contador = cont
                RsT!ejercicio = ObjetoGlobal.EjercicioActual
                RsT!serie_albaran = "N" + Strings.Right(ObjetoGlobal.EjercicioActual.Trim, 2)
                RsT!numero_albaran = aAlba(i)
                'RsT!referencia
                'RsT!ref_precalibre
                RsT.Update()
            Else ' Precalibre

                cont = cont + 1
                RsP.AddNew()
                RsP!empresa = ObjetoGlobal.EmpresaActual
                RsP!codigo_palet = Palet
                RsP!contador = cont
                RsP!ejercicio = ObjetoGlobal.EjercicioActual
                RsP!serie_albaran = "N" + Strings.Right(ObjetoGlobal.EjercicioActual.Trim, 2)
                RsP!numero_albaran = aAlba(i)
                'RsP!referencia
                'RsP!ref_precalibre
                RsP.Update()
            End If
        Next

    End Sub

    Private Sub Bttrazabilidad_Click(sender As Object, e As EventArgs) Handles Bttrazabilidad.Click

        If TxtVariedad.Text.Trim() <> "" Then
            trazabilidadSeg()
            trazabilidad2()
        End If

    End Sub

    Private Sub DPFechaVolcado_ValueChanged(sender As Object, e As EventArgs) Handles DPFechaVolcado.ValueChanged
        DPHastaFecha.Value = DPFechaVolcado.Value
    End Sub

    Private Sub TxtVariedad_TextChanged(sender As Object, e As EventArgs) Handles TxtVariedad.TextChanged
        If TxtVariedad.Text.Trim() <> "" Then
            OpcionVolcados1.Checked = True
        End If
    End Sub

    Private Sub DGVolcado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVolcado.CellContentClick
        'Dim ofrm As New AsignarAlbaran

        'ofrm.PonLote = DGVolcado.Rows(e.RowIndex).Cells("lote").Value
        'ofrm.NoBulto = DGVolcado.Rows(e.RowIndex).Cells("bulto").Value
        'ofrm.Documento = DGVolcado.Rows(e.RowIndex).Cells("albaran").Value
        'ofrm.ObjetoGlobal = ObjetoGlobal
        'ofrm.ShowDialog()

        'If ofrm.resultado = 1 Then
        '    DGVolcado.Rows(e.RowIndex).Cells("albaran").Value = ofrm.Documento
        '    DGVolcado.Rows(e.RowIndex).Cells("bulto").Value = ofrm.NoBulto
        'ElseIf ofrm.resultado = 2 Then
        '    DGVolcado.Rows(e.RowIndex).Cells("albaran").Value = ofrm.Documento
        'ElseIf ofrm.resultado = 3 Then
        '    DGVolcado.Rows(e.RowIndex).Cells("albaran").Value = ofrm.Documento
        '    DGVolcado.Rows(e.RowIndex).Cells("bulto").Value = ofrm.NoBulto
        'End If

    End Sub

    Private Function ObtenColumnaCalibre(var, cal, conf)
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim aCali As String()
        Dim subsql As String
        Dim aRet As Integer()
        Dim ord As Integer
        Dim jj As Integer
        Dim YaEsta As Boolean


        If cal.indexof("-") > 0 Then
            aCali = Split(Trim(cal), "-")
            aCali(0) = aCali(0).Trim
            aCali(1) = aCali(1).Trim
            If aCali(0).Length < aCali(1).Length Then
                aCali(0) = " " & aCali(0)
            End If
            subsql = "calibre between '" & aCali(0) & "' and  '" & aCali(1) & "' "
        Else
            aCali = Split(Trim(cal), "/")
            If aCali.Length > 1 Then
                aCali(0) = aCali(0).Trim
                aCali(1) = aCali(1).Trim
                If aCali(0).Length < aCali(1).Length Then
                    aCali(0) = " " & aCali(0)
                End If
                subsql = "calibre between '" & aCali(0) & "' and  '" & aCali(1) & "' "
            Else
                subsql = "calibre = '" & Trim(aCali(0)) & "' "
            End If
        End If

        ReDim Preserve aRet(0)
        ord = 0

        sql = "SELECT * FROM calidades_calib_fr WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' and codigo_variedad = '" & var & "' AND " & subsql & " and (CHARINDEX(UPPER(trim(caja)), '" & Strings.UCase(Strings.Trim(conf)) & "') > 0 OR UPPER(trim(caja)) = 'INDIFERENTE')"
        Rs.Open(sql, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            Rs.Close()

            sql = "SELECT * FROM calidades_calib_fr WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' and codigo_variedad = '" & var & "' AND " & subsql & " and (CHARINDEX(UPPER(trim(caja)), '" & Strings.UCase(Strings.Trim(conf)) & "') > 0 OR UPPER(trim(caja)) = 'INDIFERENTE')"
            Rs.Open(sql, ObjetoGlobal.cn)

            If Rs.RecordCount > 0 Then
                While Not Rs.EOF
                    For i = 1 To 15
                        If Rs("cal" & i) = "S" Then
                            'Rs.Close()
                            'Return i + 9
                            ord = ord + 1
                            ReDim Preserve aRet(ord)
                            aRet(ord) = i + 4
                        End If
                    Next
                    Rs.MoveNext()
                End While
                '   For i = 1 To 15
                'If Rs("cal" & i) = "O" Then
                ' Rs.Close()
                'Return i + 9
                'End If
                '  Next
            End If
            Rs.MoveFirst()
            If Rs.RecordCount > 0 Then
                While Not Rs.EOF
                    For i = 1 To 15
                        If Rs("cal" & i) = "O" Then
                            'Rs.Close()
                            'Return i + 9
                            YaEsta = False
                            For jj = 0 To UBound(aRet)
                                If aRet(jj) = i + 4 Then
                                    YaEsta = True
                                End If
                            Next
                            If Not YaEsta Then
                                ord = ord + 1
                                ReDim Preserve aRet(ord)
                                aRet(ord) = i + 9
                            End If
                        End If
                    Next
                    Rs.MoveNext()
                End While
                '   For i = 1 To 15
                'If Rs("cal" & i) = "O" Then
                ' Rs.Close()
                'Return i + 9
                'End If
                '  Next
            End If

            'Else
            '    For i = 4 To 18
            '        If Rs("cal" & (i - 3)) = "S" Then
            '            Rs.Close()
            '            Return i
            '        End If
            '    Next
            '    For i = 4 To 18
            '        If Rs("cal" & (i - 3)) = "O" Then
            '            Rs.Close()
            '            Return i
            '        End If
            '    Next
        End If

        '    ,[codigo_variedad]
        '    ,[calibre]
        '    ,[cal1]
        '    ,[cal2]
        '    ,[cal3]
        '    ,[cal4]
        '    ,[cal5]
        '    ,[cal6]
        '    ,[cal7]
        '    ,[cal8]
        '    ,[cal9]
        '    ,[cal10]
        '    ,[cal11]
        '    ,[cal12]
        '    ,[cal13]
        '    ,[cal14]
        '    ,[cal15]
        'From [Compugest].[dbo].[calidades_calib_fr]
        Rs.Close()
        Return aRet


    End Function


    Private Sub HayRestos(palet)
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim RsP As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim PesoRestos As Double
        Dim aRestos(,) As String
        Dim ColCal As Integer
        Dim FFinal As Integer
        Dim Confec As String

        palet = DGFabricados.Rows(i).Cells(0).Value
        Confec = DGFabricados.Rows(i).Cells(4).Value

        sql = "SELECT p.orden_confeccion, o.cajas_palet, o.peso_palet FROM palets p LEFT JOIN orden_confeccion o ON o.empresa = p.empresa and o.numero_orden = p.orden_confeccion WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' and codigo_palet = '" & palet & "'"
        RsP.Open(sql, ObjetoGlobal.cn, True)
        If RsP.RecordCount > 0 Then
            sql = "SELECT * FROM ord_confec_restos WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' and numero_orden = '" & RsP!orden_confeccion & "' AND situacion ='N' order by contador"
            Rs.Open(sql, ObjetoGlobal.cn, True)
            PesoRestos = Math.Round((RsP!peso_palet / RsP!cajas_palet) * Rs!cajas, 0)
            aRestos = CargarArrayRestos(RsP!fecha_orden)

            ColCal = ObtenColumnaCalibre(Rs!codigo_variedad, Rs!calibre_comercial, Confec)
            FFinal = FilaFinal(DGFabricados.Rows(i).Cells(2).Value)

        End If

    End Sub

    Private Function CargarArrayRestos(aFecha) As String(,)
        Dim Sql As String, SQL2 As String, Calidades(12) As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim CuantasCalidades As Integer
        Dim aDatos(,) As String
        Dim i As Integer
        Dim o As Integer

        Sql = "SELECT lote, hora_volcado, t.numero_albaran, bulto, codigo_variedad,"
        Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE (select CAST(ISNULL(SUM(kg_calidad),0) AS INTEGER) from entradas_clasif_bt b"
        Sql = Trim(Sql) + " WHERE b.empresa = t.empresa AND b.ejercicio= t.ejercicio AND b.serie_albaran = t.serie_albaran AND b.numero_albaran = t.numero_albaran AND b.bulto = t.bulto AND b.tipo_clasificacion = 'PRO') END,"
        Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE (select CAST(ISNULL(dbo.fn_peso_bulto(t.empresa,t.ejercicio,t.serie_albaran,t.numero_albaran,t.bulto),0) AS INTEGER)) END,"
        Sql = Trim(Sql) + " t.codigo_barras,"
        Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then 'E' ELSE '' END,"


        SQL2 = "SELECT * from calidades_var_ej WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "'"
        SQL2 = Trim(SQL2) + " AND codigo_variedad = '" + Trim(TxtVariedad.Text) + "'"


        Rs.Open(SQL2, ObjetoGlobal.cn)
        For i = 1 To Rs.RecordCount
            If i <= 12 Then
                Rs.AbsolutePosition = i
                Calidades(i) = Trim(Rs!descripcion_abrev)
            End If
        Next
        CuantasCalidades = Rs.RecordCount

        If CuantasCalidades > 12 Then CuantasCalidades = 12
        Rs.Close()

        For i = 1 To 12
            If i <= CuantasCalidades Then
                Sql = Trim(Sql) + " CASE WHEN (t.ignorar_sn = 'S' or t.numero_albaran is NULL) then '0' ELSE (select CAST(ISNULL(SUM(kg_calidad),0) AS INTEGER) from entradas_clasif_bt b"
                Sql = Trim(Sql) + " WHERE b.empresa = t.empresa AND b.ejercicio= t.ejercicio AND b.serie_albaran = t.serie_albaran AND b.numero_albaran = t.numero_albaran AND b.bulto = t.bulto AND b.tipo_clasificacion = 'PRO' AND codigo_calidad = " + CStr(i) + " ) END"
            Else
                Sql = Trim(Sql) + "0"
            End If
            If i < 12 Then Sql = Trim(Sql) + ","
        Next

        Sql = Trim(Sql) + " , codigo_barras FROM entradas_lotes_test t "
        Sql = Trim(Sql) + " LEFT JOIN entradas_albaranes e ON e.empresa=t.empresa and e.ejercicio = t.ejercicio and e.serie_albaran = t.serie_albaran and t.numero_albaran = e.numero_albaran"
        Sql = Trim(Sql) + " WHERE t.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND t.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "'"
        Sql = Trim(Sql) + " AND e.CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "'"
        Sql = Trim(Sql) + " AND fecha_volcado='" + Format(CDate(aFecha), "dd/MM/yyyy") + "' "
        Sql = Trim(Sql) + " ORDER BY lote desc"

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            ReDim Preserve aDatos(Rs.RecordCount, Rs.CuantosCampos)
            For i = 1 To Rs.RecordCount
                'ReDim Preserve aDatos(CuantasCalidades, i)
                Rs.AbsolutePosition = i
                For o = 0 To Rs.CuantosCampos - 1
                    If IsNumeric(Rs.Valor(Rs.NombreCampo(o))) Then
                        aDatos(i, o) = CStr(Rs.Valor(Rs.NombreCampo(o)))
                    Else
                        aDatos(i, o) = "" & Rs.Valor(Rs.NombreCampo(o))
                    End If
                Next
            Next
        End If
        Rs.Close()

        Return aDatos

    End Function
    Private Function ObtenColumnaPreCalibre(var, cal, conf)
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim aCali As String()
        Dim subsql As String
        Dim aRet As Integer()
        Dim no As Integer
        Dim YaEsta As Boolean
        Dim ORD As Integer



        subsql = "calibre = '" & Trim(cal) & "' AND (caja='PRECAL' OR caja='INDIFERENTE')"

        no = 0
        ReDim aRet(0)


        sql = "SELECT * FROM calidades_calib_fr WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' and codigo_variedad = '" & var & "' AND " & subsql
        Rs.Open(sql, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            Rs.Close()

            sql = "Select * FROM calidades_calib_fr WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' and codigo_variedad = '" & var & "' AND " & subsql
            Rs.Open(sql, ObjetoGlobal.cn)
            If Rs.RecordCount > 0 Then
                While Not Rs.EOF
                    For i = 1 To 15
                        If Rs("cal" & i) = "S" Then
                            ORD = ORD + 1
                            ReDim Preserve aRet(ORD)
                            aRet(ORD) = i + 9
                        End If
                    Next
                    Rs.MoveNext()
                End While
            End If
            Rs.MoveFirst()
            If Rs.RecordCount > 0 Then
                While Not Rs.EOF
                    For i = 1 To 15
                        If Rs("cal" & i) = "O" Then
                            YaEsta = False
                            For jj = 0 To UBound(aRet)
                                If aRet(jj) = i + 9 Then
                                    YaEsta = True
                                End If
                            Next
                            If Not YaEsta Then
                                ORD = ORD + 1
                                ReDim Preserve aRet(ORD)
                                aRet(ORD) = i + 9
                            End If
                        End If
                    Next
                    Rs.MoveNext()
                End While
            End If

            'While Not Rs.EOF
            '    For i = 1 To 15
            '        If Rs("cal" & i) = "S" Then
            '            'Rs.Close()
            '            no = no + 1
            '            ReDim Preserve aRet(no)
            '            'Return i + 9
            '            aRet(no) = i + 9
            '        End If
            '    Next
            '    For i = 1 To 15
            '        If Rs("cal" & i) = "O" Then
            '            'Rs.Close()
            '            'Return i + 9
            '            no = no + 1
            '            ReDim Preserve aRet(no)
            '            'Return i + 9
            '            aRet(no) = i + 9
            '        End If
            '    Next
            '    Rs.MoveNext()
            'End While
        End If
        Rs.Close()

        Return aRet

    End Function
    Function DevuelveSegundadelDia(dFecha1, dFecha2) As Double
        Dim ret As Double = 0
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim i As Integer

        'For i = 0 To DGPaletsSegunda.Rows.Count - 1
        '    ret += CDbl(DGPaletsSegunda.Rows(i).Cells(2).Value)
        'Next

        If ret = 0 Then
            Sql = "SELECT isnull(sum(p.peso_palet),0) as peso FROM palets p LEFT JOIN ordenes_confeccion o on o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion"
            Sql = Sql + " WHERE p.empresa='" & ObjetoGlobal.EmpresaActual & "' and P.fecha_confeccion between '" & Format(dFecha1, "dd/MM/yyyy") & "' and '" & Format(dFecha2, "dd/MM/yyyy") & "' and o.codigo_variedad = '" & Trim(TxtVariedad.Text) & "' AND o.categoria = 'ST'"

            If Rs.Open(Sql, ObjetoGlobal.cn) Then
                ret = Rs!peso
            End If
            Rs.Close()
        End If

        Return ret

    End Function

    Private Sub CabeceraRestos()

        DGView().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "numero_orden"
        column1.HeaderText = "Orden confección"
        column1.Width = 125
        DGView().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "orden"
        column2.HeaderText = "Orden"
        column2.Width = 125
        DGView().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "fecha"
        column3.HeaderText = "Fecha"
        column3.Width = 125
        DGView().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "cajas"
        column4.HeaderText = "Cajas"
        column4.Width = 125
        DGView().Columns.Add(column4)

        Dim column5 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column5.Name = "calibre"
        column5.HeaderText = "Calibre"
        column5.Width = 125
        DGView().Columns.Add(column5)

        Dim column6 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column6.Name = "variedad"
        column6.HeaderText = "Variedad"
        column6.Width = 125
        DGView().Columns.Add(column6)

        Dim column7 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column7.Name = "confeccion"
        column7.HeaderText = "Confección"
        column7.Width = 125
        DGView().Columns.Add(column7)

        Dim column8 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column8.Name = "borrar"
        column8.HeaderText = "Eliminar"
        column8.Width = 100
        DGView().Columns.Add(column8)

    End Sub
    Private Sub Mostrarrestos()
        Dim Sql As String
        Dim i As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell

        Sql = "SELECT r.numero_orden, r.contador, r.fecha_orden, r.cajas, o.calibre_tecnico, o.codigo_variedad, o.codigo_confeccion FROM ord_confec_restos r LEFT JOIN ordenes_confeccion o ON o.numero_orden = r.numero_orden WHERE r.palet is null  And r.situacion = 'N' AND o.codigo_variedad = '" & TxtVariedad.Text & "'"

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                Row = New DataGridViewRow
                For o = 0 To Rs.CuantosCampos - 1
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Rs.Valor(Rs.NombreCampo(o))
                    Row.Cells.Add(Cell)
                Next
                Cell = New DataGridViewTextBoxCell
                Cell.Value = "  Eliminar palet   "
                Row.Cells.Add(Cell)
                DGView.Rows.Add(Row)
            Next
        End If

    End Sub

    Private Sub DGView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGView.CellContentClick
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset

        If DGView.CurrentCell.ColumnIndex = 7 And e.RowIndex >= 0 Then
            If MessageBox.Show("Confirma eliminar el resto seleccionado: " & DGView.Rows(e.RowIndex).Cells("numero_orden").Value & "-" & DGView.Rows(e.RowIndex).Cells("orden").Value, "Borrar palets restos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                Sql = "SELECT * FROM ord_confec_restos WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND numero_orden=" & DGView.Rows(e.RowIndex).Cells("numero_orden").Value & " AND contador = " & DGView.Rows(e.RowIndex).Cells("orden").Value
                If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
                    If Rs.RecordCount = 1 Then
                        Rs!situacion = "B"
                        Rs.Update()
                    End If
                    CabeceraRestos()
                    Mostrarrestos()
                End If
                Rs.Close()
            End If
        End If

    End Sub
    Private Sub CabeceraDefectosLinea(ByRef aCab() As String)
        Dim i As Integer
        Dim aColumns(24) As DataGridViewColumn

        DSDefectosLinea().Columns().Clear()
        'Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column1.Name = "mas"
        'column1.HeaderText = " + "
        'column1.Width = 25
        'DSDefectosLinea().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "serie"
        column2.HeaderText = "Serie"
        column2.Width = 50
        column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DSDefectosLinea().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "numero_albaran"
        column3.HeaderText = "Albarán"
        column3.Width = 75
        column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DSDefectosLinea().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "fecha_entrada"
        column4.HeaderText = "Fecha entrada"
        column4.Width = 100
        column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DSDefectosLinea().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "kilos"
        column5.HeaderText = "Kgs. entrada"
        column5.Width = 100
        column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DSDefectosLinea().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "codigo_socio"
        column6.HeaderText = "Socio"
        column6.Width = 75
        DSDefectosLinea().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "Razon"
        column7.HeaderText = "Razón social"
        column7.Width = 225
        DSDefectosLinea().Columns.Add(column7)

        If Not DefectosHortizontal.Checked Then
            Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column8.Name = "defecto"
            column8.HeaderText = "Defecto"
            column8.Width = 125
            DSDefectosLinea().Columns.Add(column8)

            Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column9.Name = "cantidad"
            column9.HeaderText = "cantidad"
            column9.Width = 100
            column9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DSDefectosLinea().Columns.Add(column9)

            Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
            column10.Name = "porc"
            column10.HeaderText = "% Def"
            column10.Width = 50
            column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DSDefectosLinea().Columns.Add(column10)
        End If

        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "pesd"
        column11.HeaderText = "Peso volcado"
        column11.Width = 75
        column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DSDefectosLinea().Columns.Add(column11)

        'Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column11.Name = "pesv"
        'column11.HeaderText = "Peso volcado"
        'column11.Width = 75
        'column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DSDefectosLinea().Columns.Add(column12)

        For i = 1 To UBound(aCab)
            aColumns(i) = New DataGridViewTextBoxColumn()
            aColumns(i).Name = aCab(i)
            aColumns(i).HeaderText = aCab(i)
            aColumns(i).Width = 75
            DSDefectosLinea().Columns.Add(aColumns(i))
        Next

    End Sub
    Private Sub MostrarDefectosLinea()
        Dim Sql As String
        Dim i As Integer
        Dim ii As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs1 As New cnRecordset.CnRecordset
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim campos() As String = {"serie_albaran", "numero_albaran", "fecha_entrada", "kg_entrada", "codigo_socio", "razon"}
        Dim campos1() As String = {"defecto", "cant"}
        Dim suma As Double
        Dim sumadef As Double
        Dim defecto As String
        Dim EsLineaalbaran As Boolean
        Dim rsProv As New cnRecordset.CnRecordset
        Dim iii As Integer
        Dim oo As Integer
        Dim CabDefectos(0) As String
        Dim nOrd As Integer
        Dim Ser As String
        Dim KgAlbaran As Double

        Ser = "N" & Strings.Right(Strings.Trim(ObjetoGlobal.EjercicioActual), 2)

        If cAlbaranesVolcados.Trim = "" Then
            Exit Sub
        End If

        If DefectosHortizontal.Checked = False Then

            CabeceraDefectosLinea(CabDefectos)

            Sql = "SELECT d.serie_albaran, d.numero_albaran, e.fecha_entrada, e.kg_entrada, e.codigo_socio, (trim(s.nombre_socio) + ' ' + trim(s.apellidos_socio)) as razon FROM defectos_linea d LEFT JOIN entradas_albaranes e ON e.empresa = d.empresa AND d.serie_albaran = e.serie_albaran AND "
            Sql = Sql + " e.numero_albaran = d.numero_albaran LEFT JOIN socios_coop s ON s.codigo_socio = e.codigo_socio "
            Sql = Sql + " WHERE e.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND e.serie_albaran = '" & Ser & "' AND e.numero_albaran IN (" & cAlbaranesVolcados & ")"
            Sql = Sql + " GROUP BY d.serie_albaran, d.numero_albaran, e.fecha_entrada, e.kg_entrada, e.codigo_socio, (trim(s.nombre_socio) + ' ' + trim(s.apellidos_socio)) "
            Sql = Sql + " ORDER BY d.serie_albaran, d.numero_albaran"

            If Rs.Open(Sql, ObjetoGlobal.cn) Then

                For i = 1 To Rs.RecordCount
                    Rs.AbsolutePosition = i
                    Row = New DataGridViewRow
                    For o = 0 To Rs.CuantosCampos - 2
                        If campos(o) = "codigo_socio" Then
                            If IsDBNull(Rs.Valor(campos(o))) Then
                                Sql = "SELECT e.codigo_proveedor, p.razon_social FROM entradas_albaranes e LEFT JOIN proveedores_coop p ON p.codigo_proveedor = e.codigo_proveedor "
                                Sql = Sql + " WHERE e.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND e.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND e.serie_albaran ='" & Rs!serie_albaran & "' AND e.numero_albaran =" & Rs!numero_albaran
                                If rsProv.Open(Sql, ObjetoGlobal.cn) Then
                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = rsProv!codigo_proveedor
                                    Row.Cells.Add(Cell)
                                    o = o + 1
                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = rsProv!razon_social
                                    Row.Cells.Add(Cell)
                                Else
                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = "-"
                                    Row.Cells.Add(Cell)

                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = "-"
                                    Row.Cells.Add(Cell)
                                    o = o + 1
                                End If
                                rsProv.Close()
                            Else
                                Cell = New DataGridViewTextBoxCell
                                Cell.Value = Rs.Valor(campos(o))
                                Row.Cells.Add(Cell)
                                Cell = New DataGridViewTextBoxCell
                                Cell.Value = Rs.Valor(campos(o + 1))
                                Row.Cells.Add(Cell)
                                o = o + 1
                            End If
                        Else
                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = Rs.Valor(campos(o))
                            Row.Cells.Add(Cell)
                        End If

                        EsLineaalbaran = True
                    Next

                    ReDim aDefectos(0)
                    ReDim aTitulos(0)

                    KgAlbaran = PesosDefectos(Rs!numero_albaran)

                    suma = KgAlbaran
                    sumadef = 0

                    For iii = 0 To UBound(aDefectos) - 1
                        'suma = suma + aDefectos(iii)
                        If aTitulos(iii).Substring(0, 8) <> "CORRECTO" Then
                            sumadef = sumadef + aDefectos(iii)
                        End If
                    Next

                    For iii = 0 To UBound(aDefectos) - 1
                        If Not EsLineaalbaran Then
                            Row = New DataGridViewRow
                            For o = 0 To Rs.CuantosCampos - 1
                                Cell = New DataGridViewTextBoxCell
                                Cell.Value = ""
                                Row.Cells.Add(Cell)
                            Next
                        Else
                            EsLineaalbaran = False
                        End If

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = aTitulos(iii)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = aDefectos(iii)
                        Row.Cells.Add(Cell)


                        Cell = New DataGridViewTextBoxCell
                        If suma <> 0 Then
                            Cell.Value = Math.Round(aDefectos(iii) * 100 / sumadef, 2)
                        Else
                            Cell.Value = "-"
                        End If

                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        If iii = 0 Then
                            Cell.Value = Math.Round(suma, 2)
                        Else
                            Cell.Value = ""
                        End If
                        Row.Cells.Add(Cell)

                        'If aTitulos(iii).Substring(0, 8) <> "CORRECTO" Then
                        '    Cell = New DataGridViewTextBoxCell
                        '    Cell.Value = Math.Round(aDefectos(iii) * 100 / sumadef, 2)
                        'Else
                        '    Cell = New DataGridViewTextBoxCell
                        '    Cell.Value = ""
                        'End If
                        'Row.Cells.Add(Cell)
                        DSDefectosLinea.Rows.Add(Row)
                    Next

                Next
                Rs.Close()
            End If
        Else
            CabDefectos = CabecerasDefectos()

            CabeceraDefectosLinea(CabDefectos)

            Sql = "SELECT d.serie_albaran, d.numero_albaran, e.fecha_entrada, e.kg_entrada, e.codigo_socio, (trim(s.nombre_socio) + ' ' + trim(s.apellidos_socio)) as razon FROM defectos_linea d LEFT JOIN entradas_albaranes e ON e.empresa = d.empresa AND d.serie_albaran = e.serie_albaran AND "
            Sql = Sql + " e.numero_albaran = d.numero_albaran LEFT JOIN socios_coop s ON s.codigo_socio = e.codigo_socio "
            Sql = Sql + " WHERE e.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND e.serie_albaran = '" & Ser & "' AND e.numero_albaran IN (" & cAlbaranesVolcados & ")"
            Sql = Sql + " GROUP BY d.serie_albaran, d.numero_albaran, e.fecha_entrada, e.fecha_entrada, e.kg_entrada, e.codigo_socio, (trim(s.nombre_socio) + ' ' + trim(s.apellidos_socio)) "
            Sql = Sql + " ORDER BY d.serie_albaran, d.numero_albaran"

            If Rs.Open(Sql, ObjetoGlobal.cn) Then

                For i = 1 To Rs.RecordCount
                    Rs.AbsolutePosition = i
                    Row = New DataGridViewRow
                    For o = 0 To Rs.CuantosCampos - 2
                        If campos(o) = "codigo_socio" Then
                            If IsDBNull(Rs.Valor(campos(o))) Then
                                Sql = "SELECT e.codigo_proveedor, p.razon_social FROM entradas_albaranes e LEFT JOIN proveedores_coop p ON p.codigo_proveedor = e.codigo_proveedor "
                                Sql = Sql + " WHERE e.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND e.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND e.serie_albaran ='" & Rs!serie_albaran & "' AND e.numero_albaran =" & Rs!numero_albaran
                                If rsProv.Open(Sql, ObjetoGlobal.cn) Then
                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = rsProv!codigo_proveedor
                                    Row.Cells.Add(Cell)
                                    o = o + 1
                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = rsProv!razon_social
                                    Row.Cells.Add(Cell)
                                Else
                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = "-"
                                    Row.Cells.Add(Cell)

                                    Cell = New DataGridViewTextBoxCell
                                    Cell.Value = "-"
                                    Row.Cells.Add(Cell)
                                    o = o + 1
                                End If
                                rsProv.Close()
                            Else
                                Cell = New DataGridViewTextBoxCell
                                Cell.Value = Rs.Valor(campos(o))
                                Row.Cells.Add(Cell)
                                Cell = New DataGridViewTextBoxCell
                                Cell.Value = Rs.Valor(campos(o + 1))
                                Row.Cells.Add(Cell)
                                o = o + 1
                            End If
                        Else
                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = Rs.Valor(campos(o))
                            Row.Cells.Add(Cell)
                        End If
                    Next

                    ReDim aDefectos(0)
                    ReDim aTitulos(0)

                    suma = PesosDefectos(Rs!numero_albaran)

                    'suma = 0
                    sumadef = 0

                    For iii = 0 To UBound(aDefectos) - 1
                        'suma = suma + aDefectos(iii)
                        If aTitulos(iii).Substring(0, 8) <> "CORRECTO" Then
                            sumadef = sumadef + aDefectos(iii)
                        End If
                    Next

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Math.Round(suma, 2)
                    Row.Cells.Add(Cell)

                    For iii = 1 To UBound(CabDefectos)
                        nOrd = OrdenArray(aTitulos, CabDefectos(iii))
                        If nOrd > 1000 Then
                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)
                        Else
                            Cell = New DataGridViewTextBoxCell
                            If suma <> 0 Then
                                Cell.Value = Math.Round(aDefectos(nOrd) * 100 / sumadef, 2)
                            Else
                                Cell.Value = "-"
                            End If
                            Row.Cells.Add(Cell)
                        End If
                    Next
                    DSDefectosLinea.Rows.Add(Row)
                Next
                Rs.Close()
            End If

        End If

        Cargadedatos = True


    End Sub


    'Private Sub CabeceraDetalleDefectosLinea()

    '    DSDefectosLineaDetalle().Columns().Clear()
    '    Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column1.Name = "mas"
    '    column1.HeaderText = " + "
    '    column1.Width = 25
    '    DSDefectosLineaDetalle().Columns.Add(column1)

    '    Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column2.Name = "serie"
    '    column2.HeaderText = "Serie"
    '    column2.Width = 50
    '    DSDefectosLineaDetalle().Columns.Add(column2)

    '    Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column3.Name = "numero_albaran"
    '    column3.HeaderText = "Albarán"
    '    column3.Width = 75
    '    DSDefectosLineaDetalle().Columns.Add(column3)

    '    Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
    '    column4.Name = "defecto"
    '    column4.HeaderText = "Defecto"
    '    column4.Width = 100
    '    DSDefectosLineaDetalle().Columns.Add(column4)

    '    Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column5.Name = "cantidad"
    '    column5.HeaderText = "Cantidad"
    '    column5.Width = 75
    '    DSDefectosLineaDetalle().Columns.Add(column5)

    '    Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column6.Name = "porc"
    '    column6.HeaderText = "%"
    '    column6.Width = 50
    '    DSDefectosLineaDetalle().Columns.Add(column6)


    'End Sub

    'Private Sub DSDefectosLinea_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DSDefectosLinea.CellContentClick
    '    Dim Sql As String
    '    Dim i As Integer
    '    Dim Rs As New cnRecordset.CnRecordset
    '    Dim Row As DataGridViewRow
    '    Dim Cell As DataGridViewTextBoxCell
    '    Dim suma As Double

    '    Dim campos() As String = {"serie_albaran", "numero_albaran", "defecto", "cant"}

    '    If DSDefectosLinea.CurrentCell.ColumnIndex < 3 Then
    '        CabeceraDetalleDefectosLinea()
    '        Sql = "SELECT serie_albaran, numero_albaran, defecto, sum(cantidad) as cant FROM defectos_linea WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
    '        Sql = Sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND serie_albaran = '" & DSDefectosLinea.Rows(e.RowIndex).Cells("serie").Value & "' AND "
    '        Sql = Sql + " numero_albaran =" & DSDefectosLinea.Rows(e.RowIndex).Cells("numero_albaran").Value
    '        Sql = Sql + " GROUP BY serie_albaran, numero_albaran, defecto"

    '        If Rs.Open(Sql, ObjetoGlobal.cn) Then
    '            suma = 0
    '            For i = 1 To Rs.RecordCount
    '                Rs.AbsolutePosition = i
    '                suma = suma + Rs!cant
    '            Next

    '            For i = 1 To Rs.RecordCount
    '                Rs.AbsolutePosition = i
    '                Row = New DataGridViewRow
    '                For o = 0 To Rs.CuantosCampos - 1
    '                    If o = 0 Then
    '                        Cell = New DataGridViewTextBoxCell
    '                        Cell.Value = "+"
    '                        Row.Cells.Add(Cell)
    '                    End If
    '                    Cell = New DataGridViewTextBoxCell
    '                    Cell.Value = Rs.Valor(campos(o))
    '                    Row.Cells.Add(Cell)
    '                    If Trim(campos(i)) = "cant" Then
    '                        Cell = New DataGridViewTextBoxCell
    '                        Cell.Value = Math.Round(Rs.Valor(campos(o)) * 100 / suma, 2)
    '                        Row.Cells.Add(Cell)
    '                    End If

    '                Next
    '                DSDefectosLineaDetalle.Rows.Add(Row)
    '            Next
    '        End If
    '        DetalleCajas.Visible = False
    '        DSDefectosLineaDetalle.Visible = True
    '        DSDefectosLineaDetalle.Location = DSDefectosLinea.Location + DSDefectosLinea.GetCellDisplayRectangle(3, DSDefectosLinea.CurrentRow.Index, False).Location + New Point(0, DSDefectosLinea.CurrentRow.Height)
    '    Else
    '        DSDefectosLineaDetalle.Visible = False
    '        DetalleCajas.Visible = False
    '    End If

    'End Sub

    'Private Sub CabeceraDetalleCajas()

    '    DetalleCajas().Columns().Clear()

    '    Dim column0 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column0.Name = "vacia"
    '    column0.HeaderText = ""
    '    column0.Width = 75
    '    DetalleCajas().Columns.Add(column0)

    '    Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column1.Name = "caja"
    '    column1.HeaderText = "Caja"
    '    column1.Width = 50
    '    DetalleCajas().Columns.Add(column1)

    '    Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
    '    column2.Name = "cant"
    '    column2.HeaderText = "Cantidad"
    '    column2.Width = 50
    '    DetalleCajas().Columns.Add(column2)


    'End Sub
    'Private Sub DSDefectosLineaDetalle_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DSDefectosLineaDetalle.CellContentClick
    '    Dim Sql As String
    '    Dim i As Integer
    '    Dim Rs As New cnRecordset.CnRecordset
    '    Dim Row As DataGridViewRow
    '    Dim Cell As DataGridViewTextBoxCell
    '    Dim campos() As String = {"caja", "cantidad"}

    '    If DSDefectosLineaDetalle.CurrentCell.ColumnIndex < 2 Then
    '        CabeceraDetalleCajas()
    '        Sql = "SELECT caja, cantidad FROM defectos_linea WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
    '        Sql = Sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND serie_albaran = '" & DSDefectosLineaDetalle.Rows(e.RowIndex).Cells("serie").Value & "' AND "

    '        Sql = Sql + " numero_albaran =" & DSDefectosLineaDetalle.Rows(e.RowIndex).Cells("numero_albaran").Value & " AND defecto ='" & DSDefectosLineaDetalle.Rows(e.RowIndex).Cells("defecto").Value & "'"
    '        Sql = Sql + " ORDER BY caja"

    '        If Rs.Open(Sql, ObjetoGlobal.cn) Then
    '            For i = 1 To Rs.RecordCount
    '                Rs.AbsolutePosition = i
    '                Row = New DataGridViewRow
    '                For o = 0 To Rs.CuantosCampos - 1
    '                    If o = 0 Then
    '                        Cell = New DataGridViewTextBoxCell
    '                        Cell.Value = Space(50)
    '                        Row.Cells.Add(Cell)
    '                    End If
    '                    Cell = New DataGridViewTextBoxCell
    '                    Cell.Value = Rs.Valor(campos(o))
    '                    Row.Cells.Add(Cell)
    '                Next
    '                DetalleCajas.Rows.Add(Row)
    '            Next
    '        End If
    '        DetalleCajas.Visible = True
    '        DetalleCajas.Location = DSDefectosLineaDetalle.Location + DSDefectosLineaDetalle.GetCellDisplayRectangle(4, DSDefectosLineaDetalle.CurrentRow.Index, False).Location + New Point(0, DSDefectosLineaDetalle.CurrentRow.Height)
    '    Else
    '        DetalleCajas.Visible = False
    '    End If

    'End Sub

    Function PesoBulto(albaran, bulto)
        Dim Sql As String
        Dim i As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim totalcajas As Integer
        Dim total_kg As Double
        Dim peso_bulto As Double = 0

        Sql = "SELECT SUM(CAJAS) AS tcajas FROM  entradas_bultos where empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
        Sql = Sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND numero_albaran = " & albaran
        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            totalcajas = Rs!tcajas
            Rs.Close()
            Sql = "SELECT kg_entrada FROM  entradas_albaranes where empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
            Sql = Sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND numero_albaran = " & albaran
            If Rs.Open(Sql, ObjetoGlobal.cn) Then
                total_kg = Rs!kg_entrada
                Rs.Close()
                Sql = "SELECT cajas FROM entradas_bultos where empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
                Sql = Sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND numero_albaran = " & albaran & " AND bulto=" & bulto
                If Rs.Open(Sql, ObjetoGlobal.cn) Then
                    peso_bulto = Math.Round((total_kg / totalcajas) * Rs!cajas)
                End If
            End If
        End If
        Rs.Close()

        Return peso_bulto


    End Function

    Function PesosDefectos(albaran)
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim no As Integer
        Dim PesoBto As Double
        Dim cClave As String
        Dim ControlCajas As List(Of Integer) = New List(Of Integer)
        Dim PesoTotal As Double

        sql = "SELECT defecto FROM defectos_linea WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
        sql = sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "'  AND "
        sql = sql + " numero_albaran =" & albaran & " AND left(defecto,8) <> 'CORRECTO' "
        sql = sql + " GROUP BY defecto"

        If Rs.Open(sql, ObjetoGlobal.cn) Then
            ReDim aTitulos(Rs.RecordCount)
            ReDim aDefectos(Rs.RecordCount)
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                aTitulos(i - 1) = Rs!defecto
                aDefectos(i - 1) = 0
            Next
        End If
        Rs.Close()

        sql = "SELECT numero_albaran, caja, defecto, cantidad FROM defectos_linea WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND "
        sql = sql + " ejercicio='" & ObjetoGlobal.EjercicioActual & "'  AND "
        sql = sql + " numero_albaran =" & albaran & " AND left(defecto,8) <> 'CORRECTO' "

        If Rs.Open(sql, ObjetoGlobal.cn) Then

            For i = 1 To Rs.RecordCount

                Rs.AbsolutePosition = i

                no = OrdenArray(aTitulos, Rs!defecto)

                cClave = Strings.Right("0000000" & albaran, 7) & Strings.Right("000" & Rs!caja, 3)


                If no < 10000 Then
                    PesoBto = 0
                    If (DictNCAlbaran.ContainsKey(cClave)) Then
                        PesoBto = DictNCAlbaran(cClave)
                    End If
                    'If Not ControlCajas.Contains(Rs!Caja) Then
                    '    ControlCajas.Add(Rs!Caja)
                    '    PesoTotal = PesoTotal + PesoBto
                    'End If
                    PesoTotal = PesoTotal + PesoBto
                    aDefectos(no) = aDefectos(no) + (PesoBto * Rs!cantidad / 100)
                End If
            Next
        End If

        Return PesoTotal

    End Function
    Function OrdenArray(ByRef arr() As String, ByVal val As String)
        Dim no As Integer
        Dim i As Integer

        no = 10000
        For i = 0 To UBound(arr) - 1
            If Trim(arr(i)) = Trim(val) Then
                Return i
            End If
        Next

        Return i

    End Function
    Function DondeNC()
        Dim i As Integer

        For i = 0 To DGVolcado.Columns.Count - 1
            If Trim(DGVolcado.Columns.Item(i).HeaderText) = "NC" Then
                Return i
            End If
        Next

        Return 0

    End Function
    Function CabecerasDefectos() As String()
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim aRetorno() As String

        sql = "SELECT Defecto FROM defectos_linea d LEFT JOIN entradas_albaranes e ON e.empresa = d.empresa AND e.ejercicio = d.ejercicio AND e.serie_albaran=d.serie_albaran AND e.numero_albaran = d.numero_albaran "
        sql = sql + "WHERE d.empresa = '1' and d.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND e.codigo_variedad ='" & TxtVariedad.Text & "' AND LEFT(defecto,8) <> 'CORRECTO' GROUP BY defecto ORDER BY defecto"

        If Rs.Open(sql, ObjetoGlobal.cn) Then
            ReDim aRetorno(Rs.RecordCount)
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                aRetorno(i) = Rs!defecto
            Next
        End If

        Return aRetorno

    End Function

    Private Sub MostrarPalotsDestrio(nOrd)
        Dim Sql As String
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim RS As New cnRecordset.CnRecordset
        Dim i As Integer
        Dim o As Integer
        Dim d As DateTime = Now()

        CabeceraPalotsDestrio()

        If nOrd = 1 Then
            Sql = " SELECT codigo_barras, codigo_variedad, fecha, grande_peq, 0 AS peso "
            Sql = Sql + " FROM palets_destrio WHERE CODIGO_VARIEDAD = '" + Trim(TxtVariedad.Text) + "' AND fecha BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        Else
            Sql = " SELECT codigo_barras, codigo_variedad, fecha, grande_peq, 0 AS peso "
            Sql = Sql + " FROM palets_destrio WHERE fecha BETWEEN '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        End If

        If RS.Open(Sql, ObjetoGlobal.cn) Then
            For i = 1 To RS.RecordCount
                RS.AbsolutePosition = i
                Row = New DataGridViewRow
                For o = 0 To 4
                    Cell = New DataGridViewTextBoxCell
                    If UCase(Trim(RS.NombreCampo(o))) = "GRANDE_PEQ" Then
                        Select Case UCase(Trim(RS.Valor(RS.NombreCampo(o))))
                            Case "G"
                                Cell.Value = "Industria Grande"
                            Case "P"
                                Cell.Value = "Industria Pequeño"
                            Case "R"
                                Cell.Value = "Retría Grande"
                            Case "I"
                                Cell.Value = "Retría Pequeño"
                            Case "S"
                                Cell.Value = "Segunda grande"
                            Case "X"
                                Cell.Value = "Segunda pequeño"
                            Case Else
                                Cell.Value = "No comercial"
                        End Select
                    ElseIf UCase(Trim(RS.NombreCampo(o))) = "PESO" Then
                        Select Case UCase(Trim(RS.Valor(RS.NombreCampo(o - 1))))
                            Case "G", "S"
                                If Strings.Left(RS!codigo_variedad, 2) = "09" Then
                                    Cell.Value = 430
                                ElseIf Strings.Left(RS!codigo_variedad, 2) = "14" Then
                                    Cell.Value = 340
                                Else
                                    Cell.Value = 400
                                End If
                            Case "P" ,"X"
                                Cell.Value = 250

                            Case "R"
                                If Strings.Left(RS!codigo_variedad, 2) = "14" Then
                                    Cell.Value = 340
                                Else
                                    Cell.Value = 400
                                End If

                            Case "I"
                                Cell.Value = 250
                            Case Else
                                If Strings.Left(RS!codigo_variedad, 2) = "14" Then
                                    Cell.Value = 340
                                Else
                                    Cell.Value = 400
                                End If
                        End Select
                    Else
                        Cell.Value = RS.Valor(RS.NombreCampo(o))
                    End If
                    Row.Cells.Add(Cell)
                Next
                PalotsDetrio.Rows.Add(Row)
            Next
        End If
    End Sub

    Private Sub rbPendientes_CheckedChanged(sender As Object, e As EventArgs) Handles rbPendientes.CheckedChanged, rbVolcados.CheckedChanged, RbTodos.CheckedChanged
        Dim nOrd As Integer

        If OpcionVolcados.Checked Then
            nOrd = 0
        ElseIf OpcionVolcados1.Checked Then
            nOrd = 1
        ElseIf OpcionVolcados2.Checked Then
            nOrd = 2
        Else
            nOrd = 3
        End If

        If nOrd > 0 And TxtVariedad.Text.Trim.Length = 0 Then
            nOrd = 0
        End If

        If Not IsNothing(ObjetoGlobal) Then
            CabeceraPrecalibrado()
            MostrarPrecalibrados(nOrd)
        End If

    End Sub



    'Private Sub CancelarRestos(Sql)
    '    Dim RS As New cnRecordset.CnRecordset
    '    Dim RSr As New cnRecordset.CnRecordset
    '    Dim i As Integer
    '    Dim o As Integer
    '    Dim d As DateTime = Now()
    '    Dim SqlR As String

    '    If RS.Open(Sql, ObjetoGlobal.cn) Then
    '        For i = 1 To RS.RecordCount
    '            RS.AbsolutePosition = i
    '            If RS!orden_confeccion <> 0 Then
    '                SqlR = "SELECT * FROM ord_confec_restos where empresa = '" & RS!empresa & "' AND numero_orden =" & RS!orden_confeccion & " And palet Is Null"
    '                If RSr.Open(SqlR, ObjetoGlobal.cn) Then
    '                    RSr!palet = RS!codigo_palet
    '                    RSr!situacion = 'X'
    '                    RSr.Update
    '                End If
    '                RSr.Close()
    '            End If
    '        Next
    '    End If
    '    RS.Close()

    'End Sub

    Private Sub trazabilidadSeg()
        Dim Palet As String
        'Dim calibre As String
        Dim ColCal As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs1 As New cnRecordset.CnRecordset
        Dim RsP As New cnRecordset.CnRecordset
        Dim RsR As New cnRecordset.CnRecordset
        Dim RsTP As New cnRecordset.CnRecordset
        Dim RsRep As New cnRecordset.CnRecordset
        Dim FilaInit As Integer
        Dim NoSalir As Boolean
        Dim TotalKilos As Double
        Dim aAlba() As String
        Dim aBulto() As String
        Dim aHora() As String
        Dim aPeso() As Double
        Dim Palets() As String
        Dim Orden As Integer
        'Dim Fila As Integer
        Dim Row As Integer
        Dim PesoFabricado As Double
        Dim FilaInicial As Integer
        Dim FFinal As Integer
        Dim Sql As String
        Dim PesosGastados() As Double
        Dim PesoActual As Double
        Dim DiferenciaAlbaran As Double
        Dim i As Integer
        Dim o As Integer
        Dim t As Integer
        Dim aGlobal() As String
        Dim aGrasp() As String
        Dim PesoInicial As Double
        Dim j As Integer
        Dim TieneItem As Double
        Dim peso_calidad As Double

        CabeceraTrazaSegunda()

        ReDim PesosGastados(DGVolcado.ColumnCount)
        For i = 0 To DGVolcado.ColumnCount
            PesosGastados(i) = 0
        Next

        For i = 0 To DGPaletsSegunda.Rows.Count - 1

            Palet = DGPaletsSegunda.Rows(i).Cells(0).Value

            Orden = 0
            ReDim aAlba(0)
            ReDim aBulto(0)
            ReDim aPeso(0)
            ReDim aHora(0)
            ReDim aGlobal(0)
            ReDim aGrasp(0)
            ReDim Palets(0)

            ColCal = ObtenColumnaSegunda()
            FFinal = FilaFinal(DGPaletsSegunda.Rows(i).Cells(4).Value)

            NoSalir = True
            TotalKilos = CDbl(DGPaletsSegunda.Rows(i).Cells(2).Value)
            PesoFabricado = TotalKilos

            FilaInicial = FilaInit
            PesoActual = 0
            PesoInicial = 0
            DiferenciaAlbaran = -1

            For o = FFinal To 0 Step -1
                'Orden = Orden + 1
                If CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal).Value)) > 0 Then

                    If CDbl("0" & DGVolcado.Rows(o).Cells(ColCal).Value) <> 0 Or Trim(Palet) <> "" Then
                        PesoActual = PesoActual + CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal).Value))

                        DiferenciaAlbaran = 0

                        TieneItem = False
                        For j = 1 To UBound(aAlba)
                            If aAlba(j) = DGVolcado.Rows(o).Cells(2).Value And aBulto(j) = DGVolcado.Rows(o).Cells(3).Value Then
                                TieneItem = True
                                Exit For
                            End If
                        Next

                        If Not TieneItem Then
                            Orden = Orden + 1
                            j = Orden
                            ReDim Preserve aAlba(Orden)
                            ReDim Preserve aBulto(Orden)
                            ReDim Preserve aPeso(Orden)
                            ReDim Preserve aHora(Orden)
                            ReDim Preserve aGlobal(Orden)
                            ReDim Preserve aGrasp(Orden)
                            ReDim Preserve Palets(Orden)
                            aPeso(Orden) = 0
                        End If

                        aAlba(j) = DGVolcado.Rows(o).Cells(2).Value
                        aBulto(j) = DGVolcado.Rows(o).Cells(3).Value
                        aGlobal(j) = "" & DGVolcado.Rows(o).Cells(21).Value
                        aGrasp(j) = "" & DGVolcado.Rows(o).Cells(22).Value
                        aHora(j) = DGVolcado.Rows(o).Cells(1).Value
                        Palets(j) = DGVolcado.Rows(o).Cells(7).Value

                        peso_calidad = CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal).Value)) + aPeso(j)

                        If peso_calidad = 0 Then
                            MsgBox("peso 0")
                        End If

                        If DiferenciaAlbaran > 0 Then
                            aPeso(j) = DiferenciaAlbaran
                            TotalKilos = TotalKilos - DiferenciaAlbaran
                            DiferenciaAlbaran = 0
                        ElseIf (CDbl(TotalKilos) - CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal).Value))) < 0 Then
                            aPeso(j) = TotalKilos
                            TotalKilos = 0
                            NoSalir = False
                        Else
                            aPeso(j) = aPeso(j) + CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal).Value))
                            TotalKilos = TotalKilos - CDbl("0" & Trim("" & DGVolcado.Rows(o).Cells(ColCal).Value))
                        End If
                        '   PesosGastados(ColCal) = PesosGastados(ColCal) + aPeso(Orden)
                        If aPeso(j) > 0 Then
                            'NoSalir = False
                        End If
                        'Orden = Orden + 1
                        Orden = j
                    End If
                End If
                If Not NoSalir Then
                    Exit For
                End If

                If Not NoSalir And TotalKilos < 1 Then
                    Exit For
                End If
            Next
            'If Not NoSalir Then
            PintarTrazabSegunda(Palet, DGPaletsSegunda.Rows(i).Cells(4).Value, CDbl(DGPaletsSegunda.Rows(i).Cells(2).Value), aAlba, aBulto, aPeso, aHora, Row, aGlobal, aGrasp, Palets)
            'If GrabarTra.Checked Then
            GrabarTrazaSeg(Palet, DGPaletsSegunda.Rows(i).Cells(4).Value, CDbl(DGPaletsSegunda.Rows(i).Cells(2).Value), aAlba, aBulto, aPeso, aHora, Row)
            'End If
            'End If
        Next

    End Sub
    Private Sub GrabarTrazaSeg(ByVal Palet As String, ByVal HoraSalida As String, ByVal PesoPalet As String, ByVal aAlba() As String, ByVal aBulto() As String, ByVal aPeso() As Double, ByVal Hora() As String, ByRef Row As Integer)
        Dim i As Integer
        Dim sql As String
        Dim RsT As New cnRecordset.CnRecordset
        Dim RsP As New cnRecordset.CnRecordset
        Dim cont As Integer

        sql = "SELECT * FROM palets_segunda_t WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual.Trim & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual.Trim & "' and codigo_palet='" & Palet & "'"
        RsP.Open(sql, ObjetoGlobal.cn, True)
        While RsP.RecordCount > 0
            RsP.MoveFirst()
            RsP.Delete()
        End While
        RsP.Close()

        sql = "SELECT * FROM palets_segunda_t WHERE 1=0"
        RsT.Open(sql, ObjetoGlobal.cn, True)
        cont = 0
        For i = 1 To UBound(aAlba)
            If aAlba(i) > 0 Then
                cont = cont + 1
                RsT.AddNew()
                RsT!empresa = ObjetoGlobal.EmpresaActual
                RsT!codigo_palet = Palet
                RsT!contador = cont
                RsT!ejercicio = ObjetoGlobal.EjercicioActual
                RsT!serie_albaran = "N" + Strings.Right(ObjetoGlobal.EjercicioActual.Trim, 2)
                RsT!numero_albaran = aAlba(i)
                'RsT!referencia
                'RsT!ref_precalibre
                RsT.Update()
            End If
        Next

    End Sub


    Private Sub PintarTrazabSegunda(ByVal Palet As String, ByVal HoraSalida As String, ByVal PesoPalet As String, ByVal aAlba() As String, ByVal aBulto() As String, ByVal aPeso() As Double, ByVal Hora() As String, ByRef Row As Integer, ByRef aGlobal() As String, ByRef aGrasp() As String, ByVal aPalets() As String)
        Dim Rowt As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim i As Integer
        Dim sql As String
        Dim RS As New cnRecordset.CnRecordset
        Dim o As Integer


        'Rowt = New DataGridViewRow
        For i = 1 To UBound(aAlba)
            If aAlba(i) > 0 Then
                Rowt = New DataGridViewRow
                If i = 1 Then
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = HoraSalida
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Palet
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = PesoPalet
                    Rowt.Cells.Add(Cell)
                Else
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = " "
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = " "
                    Rowt.Cells.Add(Cell)
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = " "
                    Rowt.Cells.Add(Cell)
                End If
                Cell = New DataGridViewTextBoxCell
                Cell.Value = " "
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aAlba(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aBulto(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aPeso(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = Hora(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aGlobal(i)
                Rowt.Cells.Add(Cell)
                Cell = New DataGridViewTextBoxCell
                Cell.Value = aGrasp(i)
                Rowt.Cells.Add(Cell)
                DGTrazaSegunda.Rows.Add(Rowt)
            End If
        Next
    End Sub

    Private Function ObtenColumnaSegunda()
        Dim i As Integer = 0

        For i = 0 To DGVolcado.ColumnCount - 1
            If Strings.Left(Trim(DGVolcado.Columns(i).HeaderText), 2) = "SE" Then
                Return i
            End If
        Next
    End Function

    Private Sub DGVolcado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVolcado.CellClick
        Dim ofrm As New AsignarAlbaran

        ofrm.PonLote = DGVolcado.Rows(e.RowIndex).Cells("lote").Value
        ofrm.NoBulto = DGVolcado.Rows(e.RowIndex).Cells("bulto").Value
        ofrm.Documento = DGVolcado.Rows(e.RowIndex).Cells("albaran").Value
        ofrm.ObjetoGlobal = ObjetoGlobal
        ofrm.ShowDialog()

        If ofrm.resultado = 1 Then
            DGVolcado.Rows(e.RowIndex).Cells("albaran").Value = ofrm.Documento
            DGVolcado.Rows(e.RowIndex).Cells("bulto").Value = ofrm.NoBulto
        ElseIf ofrm.resultado = 2 Then
            DGVolcado.Rows(e.RowIndex).Cells("albaran").Value = ofrm.Documento
        ElseIf ofrm.resultado = 3 Then
            DGVolcado.Rows(e.RowIndex).Cells("albaran").Value = ofrm.Documento
            DGVolcado.Rows(e.RowIndex).Cells("bulto").Value = ofrm.NoBulto
        End If
    End Sub

    Private Sub DefectosHortizontal_CheckedChanged(sender As Object, e As EventArgs) Handles DefectosHortizontal.CheckedChanged
        If Cargadedatos Then
            MostrarDefectosLinea()
        End If

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

End Class

