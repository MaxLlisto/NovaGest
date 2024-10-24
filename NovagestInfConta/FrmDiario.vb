Public Class FrmDiario
    Inherits libcomunes.FormGenerico
    Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim blnPrevisualizacion As Boolean = False '*************
    Dim blnImpresionCalculada As Boolean = False
    Dim proceso As String
    Dim documento As String
    Dim formato As String
    Dim campoCab(16) As String
    Dim campoInf(54) As String
    Dim campoPie(2) As String
    Dim oImp As ReportBuilder2.ClsImpresion
    Dim txtEmpresa As String
    Dim rsFormato As cnRecordset.CnRecordset
    Dim DiariosWhere As String
    Dim periodoswhere As String
    Dim FechasWhere As String
    Dim SeleccionDiarios As String
    Dim seleccionasientos As Boolean = False
    Dim listaasientos As String
    Dim primerasiento As Long
    Dim rsCabAsto As DataTable '= New cnRecordset.CnRecordset
    Dim PrimerDiario As Boolean, PrimeraFecha As Boolean

    'variables continuacion
    Public primasiento As String
    Public PaginaAnterior As Integer
    Public AcumuladoDebe As Double
    Public AcumuladoHaber As Double
    Public SumaDebeAsiento As Double
    Public SumaHaberAsiento As Double

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        TxtDesdeFechaAsiento.value = Strings.Left(Date.Now(), 10)
        TxtHastaFechaAsiento.value = Strings.Left(Date.Now(), 10)

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub FrmDiario_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sql As String = ""
        Dim dt As DataTable = New DataTable("Tabla")
        Dim dta As DataTable = New DataTable("Tabla")
        Dim dtb As DataTable = New DataTable("Tabla")
        Dim dr As DataRow
        Dim dra As DataRow
        Dim drb As DataRow
        Dim dt1 As DataTable = New DataTable("Tabla")
        Dim dt1a As DataTable = New DataTable("Tabla")
        Dim dt1b As DataTable = New DataTable("Tabla")

        dt.Columns.Add("periodo")
        dt.Columns.Add("desc_periodo")

        dta.Columns.Add("periodo")
        dta.Columns.Add("desc_periodo")

        dtb.Columns.Add("periodo")
        dtb.Columns.Add("desc_periodo")

        dt1.Columns.Add("diario")
        dt1.Columns.Add("nombre_diario")

        dt1a.Columns.Add("diario")
        dt1a.Columns.Add("nombre_diario")

        dt1b.Columns.Add("diario")
        dt1b.Columns.Add("nombre_diario")

        Try
            sql = "SELECT * FROM periodos WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' order by periodo"
            If rs.Open(sql, ObjetoGlobal.cn) Then
                While Not rs.EOF
                    dr = dt.NewRow()
                    dr("periodo") = rs!periodo
                    dr("desc_periodo") = Trim(rs!desc_periodo)
                    dra = dta.NewRow()
                    dra("periodo") = rs!periodo
                    dra("desc_periodo") = Trim(rs!desc_periodo)
                    drb = dtb.NewRow()
                    drb("periodo") = rs!periodo
                    drb("desc_periodo") = Trim(rs!desc_periodo)

                    dt.Rows.Add(dr)
                    dta.Rows.Add(dra)
                    dtb.Rows.Add(drb)
                    rs.MoveNext()
                End While

                TxtDesdePeriodo.DataSource = dta
                TxtDesdePeriodo.ValueMember = "periodo"
                TxtDesdePeriodo.DisplayMember = "desc_periodo"

                TxtHastaPeriodo.DataSource = dtb
                TxtHastaPeriodo.ValueMember = "periodo"
                TxtHastaPeriodo.DisplayMember = "desc_periodo"
            End If
            rs.Close()

            sql = "SELECT * FROM diarios WHERE empresa =  '" & ObjetoGlobal.EmpresaActual & "' order by diario"

            If rs.Open(sql, ObjetoGlobal.cn) Then

                While Not rs.EOF
                    dr = dt1.NewRow()
                    dr("diario") = rs!diario
                    dr("nombre_diario") = Trim(rs!nombre_diario)
                    dra = dt1a.NewRow()
                    dra("diario") = rs!diario
                    dra("nombre_diario") = Trim(rs!nombre_diario)
                    drb = dt1b.NewRow()
                    drb("diario") = rs!diario
                    drb("nombre_diario") = Trim(rs!nombre_diario)

                    dt1.Rows.Add(dr)
                    dt1a.Rows.Add(dra)
                    dt1b.Rows.Add(drb)
                    rs.MoveNext()
                End While

                TxtSelecciondiarios.Items.Add("Todos")
                TxtSelecciondiarios.Items.Add("Desde-Hasta")
                'TxtSelecciondiarios.Items.Add("Detallado")

                TxtDesdediario.DataSource = dt1a
                TxtDesdediario.ValueMember = "diario"
                TxtDesdediario.DisplayMember = "nombre_diario"

                TxtHastadiario.DataSource = dt1b
                TxtHastadiario.ValueMember = "diario"
                TxtHastadiario.DisplayMember = "nombre_diario"
            End If
            rs.Close()

            TxtOrdenacion.Items.Add("Fechas")
            TxtOrdenacion.Items.Add("Diario")

            TxtNumeracion.Items.Add("Normal")
            TxtNumeracion.Items.Add("Correlativa")

            campoCab(1) = "calculado.empresa"
            campoCab(2) = "calculado.ejercicio"
            campoCab(3) = "calculado.fecha_asiento"
            campoCab(4) = "calculado.diario"
            campoCab(5) = "calculado.numero_asiento"
            campoCab(6) = "calculado.importe_debe"
            campoCab(7) = "calculado.importe_haber"
            campoCab(8) = "calculado.periodo"
            campoCab(9) = "calculado.periodos_seleccionados"
            campoCab(10) = "calculado.diarios_seleccionados"
            campoCab(11) = "calculado.tipo_ordenacion"
            campoCab(12) = "calculado.fechas_selección"
            campoCab(13) = "calculado.fecha_actual"
            campoCab(14) = "calculado.usuario"
            campoCab(15) = "calculado.hora"
            campoCab(16) = "calculado.empresa_razonsocial"


            campoInf(1) = "calculado.empresa"
            campoInf(2) = "calculado.ejercicio"
            campoInf(3) = "calculado.fecha_asiento"
            campoInf(4) = "calculado.diario"
            campoInf(5) = "calculado.numero_asiento"
            campoInf(6) = "calculado.posicion"
            campoInf(7) = "calculado.periodo"
            campoInf(8) = "calculado.codigo_cuenta"
            campoInf(9) = "calculado.concepto"
            campoInf(10) = "calculado.descripcion_apunte"
            campoInf(11) = "calculado.importe_debe"
            campoInf(12) = "calculado.importe_haber"
            campoInf(13) = "calculado.clave_apunte"
            campoInf(14) = "calculado.fecha_act"
            campoInf(15) = "calculado.usuario_act"
            campoInf(16) = "calculado.tipo_act"
            campoInf(17) = "calculado.punteado"
            campoInf(18) = "calculado.punteado2"
            campoInf(19) = "calculado.punteado3"
            campoInf(20) = "calculado.anot_divisa"
            campoInf(21) = "calculado.cod_divisa"
            campoInf(22) = "calculado.debe_divisa"
            campoInf(23) = "calculado.haber_divisa"
            campoInf(24) = "calculado.cambio_divisa"
            campoInf(25) = "calculado.anot_contravalor"
            campoInf(26) = "calculado.contravalor_debe"
            campoInf(27) = "calculado.contravalor_haber"
            campoInf(28) = "calculado.compra_vta"
            campoInf(29) = "calculado.enviado_concili"
            campoInf(30) = "calculado.f_envio_concili"
            campoInf(31) = "calculado.enviado_tesoreria"
            campoInf(32) = "calculado.f_envio_tesoreria"
            campoInf(33) = "calculado.contrapartida"
            campoInf(34) = "calculado.fecha_valor"
            campoInf(35) = "calculado.descripcion_cuenta"
            campoInf(36) = "calculado.observaciones"
            campoInf(37) = "calculado.digitos"
            campoInf(38) = "calculado.codigo_alternativo"
            campoInf(39) = "calculado.desc_alternativa"
            campoInf(40) = "calculado.analitica_sn"
            campoInf(41) = "calculado.cod_divisa"
            campoInf(42) = "calculado.analitica_defecto"
            campoInf(43) = "calculado.tipo_cuenta"
            campoInf(44) = "calculado.cta_numerica"
            campoInf(45) = "calculado.columnaD/H"
            campoInf(46) = "calculado.importe"
            campoInf(47) = "calculado.nombre_divisa"
            campoInf(48) = "calculado.cambio"
            campoInf(49) = "calculado.unidades_cambio"
            campoInf(50) = "calculado.abreviatura"
            campoInf(51) = "calculado.decimales_precios"
            campoInf(52) = "calculado.decimales_importe"
            campoInf(53) = "calculado.suma_debe_asiento"
            campoInf(54) = "calculado.suma_haber_asiento"

            campoPie(1) = "calculado.importe_debe"
            campoPie(2) = "calculado.importe_haber"

            libcomunes.ObjetoGlobal = Me.ObjetoGlobal

            oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)

        Catch ex As Exception
            MsgBox("Problemas al inicial el proceso:" & sql)
            Return
        End Try
    End Sub

    Private Function vuelcadato(pagina, Czona, Cont, Campo, valor, o)
        'Dim Indice As Integer = 0
        Dim Registro As Integer = 0
        Dim zona As Integer = 1

        Select Case Trim(UCase(Czona))
            Case "CABECERA"
                zona = 0
                oImp.VolcarAImpresion(pagina, zona, o - 1, Registro, campoCab(Campo), Cont, valor) ', Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
            Case "PIE"
                zona = 2
                oImp.VolcarAImpresion(pagina, zona, o - 1, Registro, campoPie(Campo), Cont, valor) ', Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
            Case Else
                zona = 1
                oImp.VolcarAImpresion(pagina, zona, o - 1, Registro, campoInf(Campo), Cont, valor) ', Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
        End Select

        'oImp.VolcarAImpresion(pagina, zona, o - 1, Registro, campoInf(Campo), Cont, valor) ', Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
        Return ""
    End Function

    Private Function EmitirPorPantalla() As Boolean
        Dim rsFormato As ReportBuilder2.FrmSelFormatoImpresion = New ReportBuilder2.FrmSelFormatoImpresion
        Dim cadenawhere As String = ""

        'rsFormato.Proceso = "Edición de diario"

        'If rsFormato.ShowDialog = DialogResult.OK Then
        '    documento = rsFormato.documento
        '    formato = rsFormato.formato
        'Else
        '    Return False
        'End If
        'cadenawhere = ConstruyeWhereGeneralDiarios()
        'RellenaDiario(cadenawhere, TxtOrdenacion.Text, TxtNumeracion.Text, CInt(TxtPrimerasiento.Text))


        cadenawhere = ConstruyeWhereGeneralDiarios()

        rsFormato.Proceso = "Edición de diario"
        rsFormato.ObjetoGlobal = Me.ObjetoGlobal

        If rsFormato.ShowDialog = DialogResult.OK Then
            documento = rsFormato.documento
            formato = rsFormato.formato
            oImp.Inicializar("Edición de diario", documento, formato, 1, 1, ObjetoGlobal.cn, "", "", "", "", "")
            oImp.GrupoDetalle = 3
        Else
            Return False
        End If

        cadenawhere = ConstruyeWhereGeneralDiarios()

        RellenaDiario(cadenawhere, TxtOrdenacion.Text, TxtNumeracion.Text, CInt(TxtPrimerasiento.Text))
        ' If Not valido Then Return False

        Return True
    End Function

    Private Function EmitirporImpresora() As Boolean
        Dim cadenawhere As String = ""

        blnPrevisualizacion = False
        blnImpresionCalculada = False


        'Select Case TxtSelecciondiarios.Text
        '    Case "Detallado"
        '        DiariosWhere = "(" + DiariosWhere + ")"
        '    Case "Todos"
        '        DiariosWhere = ""
        '    Case "Desde-Hasta"
        '        If TxtDesdediario.SelectedItem(0) <> "" And TxtHastadiario.SelectedItem(0) <> "" And Val(TxtDesdediario.SelectedItem(0)) <= Val(TxtHastadiario.SelectedItem(0)) Then
        '            DiariosWhere = " (saldos.diario Between " & Val(TxtDesdediario.SelectedItem(0)) & " AND " & Val(TxtHastadiario.SelectedItem(0)) & ") "
        '        Else
        '            MsgBox("Seleccione correctamente los diarios")
        '            DiariosWhere = ""
        '        End If
        'End Select
        'periodoswhere = "(" + periodoswhere + ")"
        ''CuentasWhere = "(" + CuentasWhere + ")"


        Dim rsFormato As ReportBuilder2.FrmSelFormatoImpresion = New ReportBuilder2.FrmSelFormatoImpresion

        cadenawhere = ConstruyeWhereGeneralDiarios()

        rsFormato.Proceso = "Edición de diario"
        rsFormato.ObjetoGlobal = Me.ObjetoGlobal

        If rsFormato.ShowDialog = DialogResult.OK Then
            documento = rsFormato.documento
            formato = rsFormato.formato
            oImp.Inicializar("Edición de diario", documento, formato, 1, 1, ObjetoGlobal.cn, "", "", "", "", "")
        Else
            Return False
        End If

        cadenawhere = ConstruyeWhereGeneralDiarios()

        RellenaDiario(cadenawhere, TxtOrdenacion.Text, TxtNumeracion.Text, CInt(TxtPrimerasiento.Text))
        ' If Not valido Then Return False

        Return True
    End Function

    Private Sub TxtSelecciondiarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TxtSelecciondiarios.SelectedIndexChanged
        Select Case TxtSelecciondiarios.Text.Trim

            Case "Desde-Hasta"
                TxtDesdediario.Visible = True
                TxtHastadiario.Visible = True
                Desde.Visible = True
                hasta.Visible = True

            Case "Todo"
                TxtDesdediario.Visible = False
                TxtHastadiario.Visible = False
                Desde.Visible = False
                hasta.Visible = False

            Case "Detallado"
                TxtDesdediario.Visible = False
                TxtHastadiario.Visible = False

        End Select
    End Sub

    Private Sub BtPrevisualizar_Click(sender As Object, e As EventArgs) Handles BtPrevisualizar.Click

        'CadenasWhere()
        If EmitirPorPantalla() Then
            oImp.GrupoDetalle = 3
            oImp.Previsualizar()
        End If

    End Sub

    Private Sub BtImprimir_Click(sender As Object, e As EventArgs) Handles BtImprimir.Click
        'CadenasWhere()
        If EmitirporImpresora() Then
            oImp.GrupoDetalle = 3
            oImp.Imprimir()
        End If
    End Sub

    Sub RellenaDiario(cadenaWhere As String, orden As String, numeracion As String, primasiento As Integer)
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCuentas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cadenaOrder As String
        Dim numcorr As Integer
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim numasiento As Integer
        Dim numasientoant As Integer
        Dim Fecha As Date
        Dim fechaAnt As Date
        Dim Diario As Integer
        Dim diarioAnt As Integer
        Dim rsSumas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cadenafiltro As String
        Dim hora As String = Now.ToString("HH:mm:ss")
        Dim fechaSys As String = Now.ToString("dd:MM:yyyy")
        Dim CadenaSQL As String
        Dim rsCabecera As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim doc As Integer = 1



        numcorr = 1 + primasiento
        If orden = "Diarios" Then
            cadenaOrder = " ORDER BY diario, fecha_asiento, numero_asiento"
        Else
            cadenaOrder = " ORDER BY fecha_asiento, diario, numero_asiento"
        End If

        '**********

        If Not seleccionasientos Then
            If rsCabecera.Open("SELECT * FROM CABECERAS_ASIENTO WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual &
        "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' " & ConstruyeWhereGeneralDiarios() & cadenaOrder, ObjetoGlobal.cn) Then
                rsCabAsto = rsCabecera.cnDataSet.Tables(0)
            Else
                MsgBox("Error obteniendo Long datos")
                Return
            End If
        End If

        '**********

        vuelcadato(1, "cabecera", 1, 1, ObjetoGlobal.EmpresaActual, 1)
        vuelcadato(1, "cabecera", 1, 1, ObjetoGlobal.EmpresaActual, 2)
        vuelcadato(1, "cabecera", 1, 16, "" & ObjetoGlobal.EmpresaRazonSocial, 1)
        vuelcadato(1, "cabecera", 1, 16, "" & ObjetoGlobal.EmpresaRazonSocial, 2)
        vuelcadato(1, "cabecera", 1, 2, ObjetoGlobal.EjercicioActual, 1)
        vuelcadato(1, "cabecera", 1, 2, ObjetoGlobal.EjercicioActual, 2)

        'nuevo resto datos
        If Trim(TxtDesdePeriodo.Text) = "" Then
            vuelcadato(1, "cabecera", 1, 9, "Todos los periodos", 1)
        Else
            vuelcadato(1, "cabecera", 1, 9, "Periodos: Desde " & Trim(TxtDesdePeriodo.Text) & " hasta " & Trim(TxtHastaPeriodo.Text), 1)
        End If
        If TxtDesdediario.Visible Then
            vuelcadato(1, "cabecera", 1, 10, "Desde el diario " & TxtDesdediario.Text & " hasta el diario " & TxtHastadiario.Text, 1)
        Else
            If TxtSelecciondiarios.Text = "Todos" Then
                vuelcadato(1, "cabecera", 1, 10, "Todos los diarios", 1)
            Else
                vuelcadato(1, "cabecera", 1, 10, SeleccionDiarios, 1)
            End If
        End If
        vuelcadato(1, "cabecera", 1, 11, TxtOrdenacion.Text, 1)

        vuelcadato(1, "cabecera", 1, 13, "" & fechaSys, 1)
        vuelcadato(1, "cabecera", 1, 14, ObjetoGlobal.UsuarioActual, 1)
        vuelcadato(1, "cabecera", 1, 15, hora, 1)

        If Trim(TxtDesdePeriodo.Text) = "" Then
            vuelcadato(1, "cabecera", 1, 9, "Todos los periodos", 2)
        Else
            vuelcadato(1, "cabecera", 1, 9, "Periodos:  desde " & Trim(TxtDesdePeriodo.Text) & "   hasta " & Trim(TxtHastaPeriodo.Text), 2)
        End If
        If TxtDesdediario.Visible Then
            vuelcadato(1, "cabecera", 1, 10, "Desde el diario " & TxtDesdediario.Text & "   hasta el diario " & TxtHastadiario.Text, 2)
        Else
            If TxtSelecciondiarios.Text = "Todos" Then
                vuelcadato(1, "cabecera", 1, 10, "Todos los diarios", 2)
            Else
                vuelcadato(1, "cabecera", 1, 10, SeleccionDiarios, 2)
            End If
        End If
        vuelcadato(1, "cabecera", 1, 11, TxtOrdenacion.Text, 2)
        vuelcadato(1, "cabecera", 1, 13, "" & fechaSys, 2)
        vuelcadato(1, "cabecera", 1, 14, ObjetoGlobal.UsuarioActual, 2)
        vuelcadato(1, "cabecera", 1, 15, hora, 2)

        'A partir de aquí
        i = 1
        For Each MiDataRow As DataRow In rsCabAsto.Rows


            cadenafiltro = "asientos.empresa='" & Trim(MiDataRow!Empresa) & "' and asientos.ejercicio='" & Trim(MiDataRow!EJERCICIO) & "' and asientos.fecha_asiento='" & MiDataRow!fecha_asiento & "' and asientos.diario=" & MiDataRow!Diario & " and asientos.numero_asiento=" & CInt(MiDataRow!numero_asiento)

            rs.Open("SELECT asientos.empresa, asientos.periodo, asientos.ejercicio, asientos.fecha_asiento, asientos.diario, asientos.numero_asiento, cuentas.digitos, " &
                    "cuentas.observaciones, cuentas.codigo_alternativo, cuentas.desc_alternativa, cuentas.analitica_sn, cuentas.cod_divisa, cuentas.analitica_defecto, cuentas.tipo_cuenta, cuentas.cta_numerica, " &
                    "cast(asientos.posicion as int) as posicion1, asientos.codigo_cuenta, asientos.concepto, asientos.descripcion_apunte, " &
                    "asientos.importe_debe, asientos.importe_haber, asientos.clave_apunte, asientos.fecha_act, " &
                    "asientos.usuario_act, asientos.tipo_act, asientos.punteado, asientos.punteado2, " &
                    "asientos.punteado3, asientos.anot_divisa, asientos.cod_divisa, asientos.debe_divisa, " &
                    "asientos.haber_divisa, asientos.cambio_divisa, asientos.anot_contravalor, asientos.contravalor_debe, " &
                    "asientos.contravalor_haber, asientos.compra_vta, asientos.enviado_concili, asientos.f_envio_concili, " &
                    "asientos.enviado_tesoreria, asientos.f_envio_tesoreria, asientos.contrapartida, asientos.fecha_valor, " &
                    "cuentas.descripcion_cuenta  " &
                    "FROM cuentas INNER JOIN asientos ON cuentas.empresa = asientos.empresa AND " &
                    "cuentas.codigo_cuenta = asientos.codigo_cuenta " &
                    "WHERE " & cadenafiltro & " ORDER BY POSICION", ObjetoGlobal.cn)

            vuelcadato(doc, "cuerpo", i, 1, "" & MiDataRow!Empresa, 1)
            vuelcadato(doc, "cuerpo", i, 2, "" & MiDataRow!EJERCICIO, 1)
            vuelcadato(doc, "cuerpo", i, 3, "" & MiDataRow!fecha_asiento, 1)
            vuelcadato(doc, "cuerpo", i, 4, "" & MiDataRow!Diario, 1)
            If numeracion = "Correlativa" Then
                vuelcadato(doc, "cuerpo", i, 5, "" & numcorr, 1)
            Else
                vuelcadato(doc, "cuerpo", i, 5, "" & CInt(MiDataRow!numero_asiento), 1)
            End If
            vuelcadato(doc, "cuerpo", i, 7, "" & rs!periodo, 1)
            vuelcadato(doc, "cuerpo", i, 1, "" & rs!Empresa, 1)
            vuelcadato(doc, "cuerpo", i, 11, Valor(rs!importe_debe), 1)
            vuelcadato(doc, "cuerpo", i, 12, Valor(rs!importe_haber), 1)
            i = i + 1
            'CUERPO 2 lineas de apuntes del asiento
            If rs.RecordCount > 0 Then 'Hay apuntes para el asiento actual
                rs.MoveFirst
                For k = 1 To rs.RecordCount 'Para cada apunte del asiento actual
                    'Tabla asientos
                    vuelcadato(doc, "cuerpo", i, 1, "" & rs!Empresa, 2)
                    vuelcadato(doc, "cuerpo", i, 2, "" & rs!EJERCICIO, 2)
                    vuelcadato(doc, "cuerpo", i, 3, "" & rs!fecha_asiento, 2)
                    vuelcadato(doc, "cuerpo", i, 4, "" & rs!Diario, 2)
                    vuelcadato(doc, "cuerpo", i, 5, IIf(numeracion = "Correlativa", "" & numcorr, "" & rs!numero_asiento), 2)
                    vuelcadato(doc, "cuerpo", i, 6, "" & rs!Posicion1, 2)
                    vuelcadato(doc, "cuerpo", i, 7, "" & rs!periodo, 2)
                    vuelcadato(doc, "cuerpo", i, 8, "" & rs!CODIGO_CUENTA, 2)
                    vuelcadato(doc, "cuerpo", i, 9, "" & rs!concepto, 2)
                    vuelcadato(doc, "cuerpo", i, 10, "" & rs!descripcion_apunte, 2)
                    vuelcadato(doc, "cuerpo", i, 11, Valor(rs!importe_debe), 2)
                    vuelcadato(doc, "cuerpo", i, 12, Valor(rs!importe_haber), 2)
                    vuelcadato(doc, "cuerpo", i, 13, "" & rs!CLAVE_APUNTE, 2)
                    vuelcadato(doc, "cuerpo", i, 14, "" & rs!fecha_act, 2)
                    vuelcadato(doc, "cuerpo", i, 15, "" & rs!usuario_act, 2)
                    vuelcadato(doc, "cuerpo", i, 16, "" & rs!tipo_act, 2)
                    vuelcadato(doc, "cuerpo", i, 17, "" & rs!punteado, 2)
                    vuelcadato(doc, "cuerpo", i, 18, "" & rs!punteado2, 2)
                    vuelcadato(doc, "cuerpo", i, 19, "" & rs!punteado3, 2)
                    vuelcadato(doc, "cuerpo", i, 20, "" & rs!anot_divisa, 2)
                    vuelcadato(doc, "cuerpo", i, 21, "" & rs!cod_divisa, 2)
                    vuelcadato(doc, "cuerpo", i, 22, Valor(rs!debe_divisa), 2) 'decimales
                    vuelcadato(doc, "cuerpo", i, 23, Valor(rs!haber_divisa), 2) 'decimales
                    vuelcadato(doc, "cuerpo", i, 24, Valor(rs!cambio_divisa), 2) 'decimales
                    vuelcadato(doc, "cuerpo", i, 25, "" & rs!anot_contravalor, 1)
                    vuelcadato(doc, "cuerpo", i, 26, Valor(rs!contravalor_debe), 2) 'decimales
                    vuelcadato(doc, "cuerpo", i, 27, Valor(rs!contravalor_haber), 2) 'decimales
                    vuelcadato(doc, "cuerpo", i, 28, "" & rs!compra_vta, 2)
                    vuelcadato(doc, "cuerpo", i, 29, "" & rs!enviado_concili, 2)
                    vuelcadato(doc, "cuerpo", i, 30, "" & rs!f_envio_concili, 2)
                    vuelcadato(doc, "cuerpo", i, 31, "" & rs!enviado_tesoreria, 2)
                    vuelcadato(doc, "cuerpo", i, 32, "" & rs!f_envio_tesoreria, 2)
                    vuelcadato(doc, "cuerpo", i, 33, "" & rs!contrapartida, 2)
                    vuelcadato(doc, "cuerpo", i, 34, "" & rs!fecha_valor, 2)
                    vuelcadato(doc, "cuerpo", i, 35, "" & rs!descripcion_cuenta, 2)
                    'vuelcadato(1, "cuerpo", i, 36, "" & rs!observaciones, 2)
                    vuelcadato(doc, "cuerpo", i, 37, "" & rs!digitos, 2)
                    'vuelcadato(1, "cuerpo", i, 38, "" & rs!codigo_alternativo, 2)
                    vuelcadato(doc, "cuerpo", i, 39, "" & rs!DESC_ALTERNATIVA, 2)
                    vuelcadato(doc, "cuerpo", i, 40, "" & rs!analitica_sn, 2)
                    'vuelcadato(1, "cuerpo", i, 41, "" & rs!cod_divisa, 2)
                    vuelcadato(doc, "cuerpo", i, 42, "" & rs!analitica_defecto, 2)
                    vuelcadato(doc, "cuerpo", i, 43, "" & rs!tipo_cuenta, 2)
                    vuelcadato(doc, "cuerpo", i, 44, "" & rs!cta_numerica, 2)
                    If (rs!importe_debe) > 0 Then
                        vuelcadato(doc, "cuerpo", i, 45, "D", 2)
                        vuelcadato(doc, "cuerpo", i, 46, Valor(rs!importe_debe), 2)
                    Else
                        vuelcadato(doc, "cuerpo", i, 45, "H", 2)
                        vuelcadato(doc, "cuerpo", i, 46, Valor(rs!importe_haber), 2)
                    End If
                    i = i + 1
                    rs.MoveNext
                Next k
                'vuelcadato( 1, "cuerpo", i, 11, "", 3 'poner 1 sobra
                vuelcadato(doc, "cuerpo", i, 53, Valor(MiDataRow!importe_debe), 3)
                vuelcadato(doc, "cuerpo", i, 54, Valor(MiDataRow!importe_haber), 3)
                i = i + 1
            End If
            numcorr = numcorr + 1
            rs.Close()
            i = i + 1
            doc = doc + 1
        Next

        vuelcadato(doc - 1, "pie", i, 1, "", 2)
        vuelcadato(doc - 1, "pie", i, 1, "", 1)

        Exit Sub
    End Sub

    Private Function ConstruyeWhereGeneralDiarios() As String
        Static primera As Integer
        Dim cadenatotal As String

        primera = primera + 1

        Select Case TxtSelecciondiarios.Text
            Case "Detallado"
                DiariosWhere = "(" + DiariosWhere + ")"
            Case "Todos"
                DiariosWhere = ""
            Case "Desde-Hasta"
                If TxtDesdediario.Text <> "" And TxtHastadiario.Text <> "" And Val(TxtDesdediario.Text) <= Val(TxtHastadiario.Text) Then
                    DiariosWhere = "(diario between " & Val(TxtDesdediario.Text) & " AND " & Val(TxtHastadiario.Text) & ")" & ""
                Else
                    MsgBox("Seleccione correctamente los diarios")
                    DiariosWhere = ""
                    Return ""
                End If
        End Select

        If TxtHastaFechaAsiento.Text < TxtDesdeFechaAsiento.Text Then
            MsgBox("Selecciones correctamente las fechas de asientos")
            FechasWhere = ""
            Return ""
        Else
            FechasWhere = "fecha_asiento between " & TxtDesdeFechaAsiento.Text & " AND " & TxtHastaFechaAsiento.Text
        End If
        cadenatotal = FechasWhere

        If TxtDesdePeriodo.SelectedValue.ToString.Trim <> "" And TxtHastaPeriodo.SelectedValue.ToString.Trim <> "" Then
            periodoswhere = "periodo >='" & Trim(TxtDesdePeriodo.SelectedValue) & "' AND periodo <='" & TxtHastaPeriodo.SelectedValue & "'"
        ElseIf TxtDesdePeriodo.SelectedValue.ToString.Trim <> "" Then
            periodoswhere = "periodo >= '" & Trim(TxtDesdePeriodo.SelectedValue) & "'"
        ElseIf TxtHastaPeriodo.SelectedValue.ToString.Trim <> "" Then
            periodoswhere = "periodo <= '" & TxtHastaPeriodo.SelectedValue & "'"
        Else
            periodoswhere = ""
        End If

        If DiariosWhere <> "" Then
            If cadenatotal <> "" Then
                cadenatotal = cadenatotal & " AND " & DiariosWhere
            End If
        End If
        If periodoswhere <> "" Then
            If cadenatotal <> "" Then
                cadenatotal = cadenatotal & " AND  " & periodoswhere
            End If
        End If
        'If listaasientos.Trim <> "" Then
        '    listaasientos = "IN LIST (" & listaasientos.Trim & ") "
        '    If cadenatotal <> "" Then
        '        cadenatotal = cadenatotal & " AND  " & listaasientos
        '    Else
        '        cadenatotal = listaasientos
        '    End If
        'End If

        Return IIf(cadenatotal <> "", " AND ", "") & cadenatotal

    End Function

    Public Function Valor(n)
        If Trim("" & n) = "" Then Valor = 0 Else Valor = CDbl(n)
    End Function

    Private Sub Continuacion_CheckedChanged(sender As Object, e As EventArgs) Handles Continuacion.CheckedChanged
        If Continuacion.Checked Then
            gbcont.Enabled = True
        Else
            gbcont.Enabled = False
        End If
    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub

    Private Sub SeleccionarAstos_Click(sender As Object, e As EventArgs) Handles SeleccionarAstos.Click
        Dim ofrm As SeleccionAsientos = New SeleccionAsientos
        Dim FechasWhere As String = ""

        If TxtHastaFechaAsiento.Text < TxtDesdeFechaAsiento.Text Then
            MsgBox("Selecciones correctamente las fechas de asientos")
            FechasWhere = ""
        Else
            FechasWhere = "fecha_asiento between '" & TxtDesdeFechaAsiento.Text & "' AND '" & TxtHastaFechaAsiento.Text & " ' "
        End If

        If TxtOrdenacion.SelectedText = "Diarios" Then
            ofrm.ordenasientos = "diario,fecha_asiento,numero_asiento"
        Else
            ofrm.ordenasientos = "fecha_asiento,diario,numero_asiento"
        End If

        ofrm.og = Me.ObjetoGlobal

        seleccionasientos = False

        ofrm.whereAsientos = FechasWhere
        If ofrm.ShowDialog = DialogResult.OK Then
            seleccionasientos = True
            rsCabAsto = ofrm.ListaAsientos()
        End If

    End Sub
End Class