Public Class FrmBalSituacion
    Inherits libcomunes.FormGenerico
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim DigitosBalance As Integer
    Dim DesdeFrmPeriodos As Boolean
    Dim DesdeFrmDiarios As Boolean
    Dim rsFormato As cnRecordset.CnRecordset
    Dim BotonInforme As Boolean
    Dim cadenatotal As String
    Dim CuentasWhere As String
    Dim Iniciando As Boolean
    Dim tipocuenta1 As String
    Dim tipocuenta2 As String
    Dim grabadas1 As Boolean
    Dim grabadas2 As Boolean

    Dim numdigitos As Integer
    Dim TotDebeApertura As Double
    Dim TotHaberApertura As Double
    Dim TotDebeOrigen As Double
    Dim TotHaberOrigen As Double
    Dim TotDebePeriodo As Double
    Dim TotHaberPeriodo As Double
    Dim TotDebeTotal As Double
    Dim TotHaberTotal As Double
    Dim TotDebeTotSinAper As Double
    Dim TotHaberTotSinAper As Double
    Dim TotDebeOriSinAper As Double
    Dim TotHaberOriSinAper As Double
    Dim rsCuentasMayorNivel As cnRecordset.CnRecordset
    Dim rsCuentasMenorNivel As cnRecordset.CnRecordset

    Dim valido As Boolean
    Dim rsVolcado As cnRecordset.CnRecordset
    Dim valido_dato As Boolean

    Dim DiariosWhere As String
    Dim periodosWhere As String
    Dim CodigoPeriodoInicio As String
    Dim CodigoPeriodoFinOrigen As String
    Dim CodigoPeriodoFin As String
    Dim ChkDigitos(12) As CheckBox
    Dim bcancelar As Boolean = False

    Dim blnPrevisualizacion As Boolean = False '*************
    Dim blnImpresionCalculada As Boolean = False

    Dim proceso As String
    Dim documento As String
    Dim formato As String
    Dim oImp As ReportBuilder2.ClsImpresion
    Dim aCamposCabeceraInf(18) As String
    Dim aCamposDetalleInf(41) As String
    Dim aCamposPieInf(40) As String

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        TxtFecha_balance.value = Strings.Left(Date.Now(), 10)

    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean

        If (keyData = Keys.Return) Then

            ' Desplazar el foco entre los distintos controles 
            ' mediante la tecla Return. El código está basado en un 
            ' ejemplo de Francesco Balena. 
            ' 
            ' Iniciar todos los controles seleccionados actualmente. 
            ' 
            Dim ctrl As Control = Me.ActiveControl

            ' Si el control DataGridView contiene el foco
            ' abandonamos el procedimiento.
            '
            If (TypeOf ctrl Is DataGridView) Then
                Return MyBase.ProcessCmdKey(msg, keyData)
            End If

            Do
                ' Obtener el siguiente control hacia delante en el 
                ' orden de tabulación. 
                ctrl = Me.GetNextControl(ctrl, True)

                ' GetNextControl(ctrl, False) puede devolver Nothing si 
                ' es el primer control.
                '
                If (Not (ctrl Is Nothing) AndAlso (ctrl.CanFocus) AndAlso (ctrl.TabStop)) Then
                    ' Si el control puede recibir el foco, se lo doy. 
                    ctrl.Focus()
                    Text = ctrl.Name
                    Exit Do
                End If
            Loop

        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Private Sub VerNDigitos(ByVal n As Integer)
        Dim i As Integer
        Dim Incremento As Integer
        Dim TotalDigitos

        TotalDigitos = n
        Incremento = 300

        For i = 1 To TotalDigitos - 1
            ChkDigitos(i - 1).Visible = True
        Next
        For i = TotalDigitos To 12
            ChkDigitos(i - 1).Visible = False
        Next

    End Sub

    Private Sub FrmBalSituacion_Load(sender As Object, e As EventArgs) Handles Me.Load
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

                TxtPeriodoInicio.DataSource = dt
                TxtPeriodoInicio.ValueMember = "periodo"
                TxtPeriodoInicio.DisplayMember = "desc_periodo"

                TxtPeriodofinorigen.DataSource = dta
                TxtPeriodofinorigen.ValueMember = "periodo"
                TxtPeriodofinorigen.DisplayMember = "desc_periodo"

                TxtPeriodofin.DataSource = dtb
                TxtPeriodofin.ValueMember = "periodo"
                TxtPeriodofin.DisplayMember = "desc_periodo"
            End If
            rs.Close()

            sql = "SELECT * FROM diarios WHERE empresa =  '" & ObjetoGlobal.EmpresaActual & "' order by diario"

            ChkDigitos = {ChkDigitos1, ChkDigitos2, ChkDigitos3, ChkDigitos4, ChkDigitos5, ChkDigitos6, ChkDigitos7, ChkDigitos8, ChkDigitos9, ChkDigitos10, ChkDigitos11, ChkDigitos12}

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
            aCamposCabeceraInf = {" ",
                                    "calculado.empresa",
                                    "calculado.ejercicio",
                                    "calculado.codigo_cuenta",
                                    "calculado.diario",
                                    "calculado.periodo",
                                    "calculado.fecha_impresion",
                                    "calculado.codigo_divisa",
                                    "calculado.descripcion_cuenta",
                                    "calculado.usuario",
                                    "calculado.razon_social",
                                    "calculado.domicilio",
                                    "calculado.codigo_postal",
                                    "calculado.poblacion",
                                    "calculado.telefono",
                                    "calculado.fax",
                                    "calculado.correo_electronico",
                                    "calculado.pagina_web",
                                    "calculado.hora_impresión"
                                    }

            aCamposDetalleInf = {" ",
                                    "calculado.codigo_cuenta",
                                    "calculado.debe_periodo",
                                    "calculado.haber_periodo",
                                    "calculado.descripcion_cuenta",
                                    "calculado.observaciones",
                                    "calculado.digitos",
                                    "calculado.codigo_alternativo",
                                    "calculado.desc_alternativa",
                                    "calculado.analitica_sn",
                                    "calculado.analitica_defecto",
                                    "calculado.cod_divisa",
                                    "calculado.tipo_cuenta",
                                    "calculado.cta_numerica",
                                    "calculado.debe_origen",
                                    "calculado.haber_origen",
                                    "calculado.saldo_origen",
                                    "calculado.saldo_d_origen",
                                    "calculado.saldo_h_origen",
                                    "calculado.saldo_periodo",
                                    "calculado.saldo_d_periodo",
                                    "calculado.saldo_h_periodo",
                                    "calculado.debe_total",
                                    "calculado.haber_total",
                                    "calculado.saldo_total",
                                    "calculado.saldo_d_total",
                                    "calculado.saldo_h_total",
                                    "calculado.debe_apertura",
                                    "calculado.haber_apertura",
                                    "calculado.saldo_apertura",
                                    "calculado.saldo_d_apertur",
                                    "calculado.saldo_h_apertur",
                                    "calculado.debe_tot_s_aper",
                                    "calculado.haber_tot_s_aper",
                                    "calculado.saldo_tot_s_aper",
                                    "calculado.saldo_d_tot_s_a",
                                    "calculado.saldo_h_tot_s_a",
                                    "calculado.debe_ori_s_aper",
                                    "calculado.haber_ori_s_aper",
                                    "calculado.saldo_ori_s_aper",
                                    "calculado.saldo_d_ori_s_a",
                                    "calculado.saldo_h_ori_s_a"
                                }
            aCamposPieInf = {"",
                                    "calculado.fecha_impresion",
                                    "calculado.empresa",
                                    "calculado.razon_social",
                                    "calculado.domicilio",
                                    "calculado.poblacion",
                                    "calculado.telefono",
                                    "calculado.fax",
                                    "calculado.correo_electronico",
                                    "calculado.pagina_web",
                                    "calculado.codigo_postal",
                                    "calculado.TotDebeApertura",
                                    "calculado.TotHaberApertura",
                                    "calculado.TotSaldoApertura",
                                    "calculado.TotSaldoDebeApertura",
                                    "calculado.TotSaldoHaberApertura",
                                    "calculado.TotDebeOrigen",
                                    "calculado.TotHaberOrigen",
                                    "calculado.TotSaldoOrigen",
                                    "calculado.TotSaldoDebeOrigen",
                                    "calculado.TotSaldoHaberOrigen",
                                    "calculado.TotDebePeriodo",
                                    "calculado.TotHaberPeriodo",
                                    "calculado.TotSaldoPeriodo",
                                    "calculado.TotSaldoDebePeriodo",
                                    "calculado.TotSaldoHaberPeriodo",
                                    "calculado.TotDebeTotal",
                                    "calculado.TotHaberTotal",
                                    "calculado.TotSaldoTotal",
                                    "calculado.TotSaldoDebeTotal",
                                    "calculado.TotSaldoHaberTotal",
                                    "calculado.TotDebeTotSinAper",
                                    "calculado.TotHaberTotSinAper",
                                    "calculado.TotSaldoTotSinAper",
                                    "calculado.TotSaldoDebeTotSinAper",
                                    "calculado.TotSaldoHaberTotSinAper",
                                    "calculado.TotDebeOriSinAper",
                                    "calculado.TotHaberOriSinAper",
                                    "calculado.TotSaldoOriSinAper",
                                    "calculado.TotSaldoDebeOriSinAper",
                                    "calculado.TotSaldoHaberOriSinAper"
                                    }

            TxtDigitos.Text = ObjetoGlobal.NivelDeCuenta
            libcomunes.ObjetoGlobal = Me.ObjetoGlobal

            oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)
            CalculoNivelDeCuenta()

        Catch ex As Exception
            MsgBox("Problemas al inicial el proceso:" & sql)
            Return
        End Try

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Function CadenaTotalF() As String

        CadenaTotalF = ""
        If DiariosWhere <> "" Then
            CadenaTotalF = DiariosWhere
        End If
        If periodosWhere <> "" And periodosWhere <> "()" Then
            If CadenaTotalF = "" Then
                CadenaTotalF = periodosWhere
            Else
                CadenaTotalF = CadenaTotalF + " and " + periodosWhere
            End If
        End If
    End Function

    'Function ComprobarTodos() As Boolean

    'End Function

    Private Function EmitirporImpresora() As Boolean

        blnPrevisualizacion = False
        blnImpresionCalculada = False

        BotonInforme = True
        valido_dato = True

        If Not valido_dato Then Return False

        Select Case TxtSelecciondiarios.Text
            Case "Detallado"
                DiariosWhere = "(" + DiariosWhere + ")"
            Case "Todos"
                DiariosWhere = ""
            Case "Desde-Hasta"
                If TxtDesdediario.SelectedItem(0) <> "" And TxtHastadiario.SelectedItem(0) <> "" And Val(TxtDesdediario.SelectedItem(0)) <= Val(TxtHastadiario.SelectedItem(0)) Then
                    DiariosWhere = " (saldos.diario Between " & Val(TxtDesdediario.SelectedItem(0)) & " AND " & Val(TxtHastadiario.SelectedItem(0)) & ") "
                Else
                    MsgBox("Seleccione correctamente los diarios")
                    DiariosWhere = ""
                End If
        End Select
        periodosWhere = "(" + periodosWhere + ")"
        CuentasWhere = "(" + CuentasWhere + ")"


        cadenatotal = CadenaTotalF()

        If OptConSaldo1a.Checked Then
            tipocuenta1 = "Saldo"
        ElseIf OptConMov1a.Checked Then
            tipocuenta1 = "Movimientos"
        ElseIf OptTodas1a.Checked Then
            tipocuenta1 = "Todas"
        End If

        If OptConSaldo2a.Checked Then
            tipocuenta2 = "Saldo"
        ElseIf OptConMov2a.Checked Then
            tipocuenta2 = "Movimientos"
        ElseIf OptTodas2a.Checked Then
            tipocuenta2 = "Todas"
        End If

        If OptGrabadas1b.Checked Then
            grabadas1 = True
        ElseIf OptTodas1b.Checked Then
            grabadas1 = False
        End If

        If OptGrabadas2b.Checked Then
            grabadas2 = True
        ElseIf OptTodas2b.Checked Then
            grabadas2 = False
        End If

        Dim rsFormato As ReportBuilder2.FrmSelFormatoImpresion = New ReportBuilder2.FrmSelFormatoImpresion

        rsFormato.Proceso = "Edición de balance de saldos"
        rsFormato.ObjetoGlobal = Me.ObjetoGlobal

        If rsFormato.ShowDialog = DialogResult.OK Then
            documento = rsFormato.documento
            formato = rsFormato.formato

            oImp.Inicializar("Edición de balance de saldos", documento, formato, 1, 1, ObjetoGlobal.cn, "", "", "", "", "")

        Else
            Return False
        End If

        valido = True
        RellenarBalance(cadenatotal, CuentasWhere)
        If Not valido Then Return False

        '  If Not frmEstImp.blnCancelar Then
        '      If Not blnImpresionCalculada Then AbrirRecordsetFormato
        '      InicializarAcumuladosImpresion
        '      ImprimePaginas frmEstImp
        '      blnImpresionCalculada = True
        '  End If
        '  frmEstImp.blnCancelar = False
        '  frmEstImp.Hide '*************
        'Set frmEstImp = Nothing '*************
        Return True
    End Function


    Private Function Emitirporpantalla() As Boolean
        blnPrevisualizacion = False
        blnImpresionCalculada = False

        BotonInforme = True
        valido_dato = True

        If Not valido_dato Then Return False

        Select Case TxtSelecciondiarios.Text
            Case "Detallado"
                DiariosWhere = "(" + DiariosWhere + ")"
            Case "Todos"
                DiariosWhere = ""
            Case "Desde-Hasta"
                If TxtDesdediario.Text <> "" And TxtHastadiario.Text <> "" And Val(TxtDesdediario.Text) <= Val(TxtHastadiario.Text) Then
                    DiariosWhere = " (saldos.diario Between " & Val(TxtDesdediario.Text) & " AND " & Val(TxtHastadiario.Text) & ") " & ""
                Else
                    MsgBox("Seleccione correctamente los diarios")
                    DiariosWhere = ""
                End If
        End Select
        periodosWhere = "(" + periodosWhere + ")"
        CuentasWhere = "(" + CuentasWhere + ")"


        cadenatotal = CadenaTotalF()

        If OptConSaldo1a.Checked Then
            tipocuenta1 = "Saldo"
        ElseIf OptConMov1a.Checked Then
            tipocuenta1 = "Movimientos"
        ElseIf OptTodas1a.Checked Then
            tipocuenta1 = "Todas"
        End If

        If OptConSaldo2a.Checked Then
            tipocuenta2 = "Saldo"
        ElseIf OptConMov2a.Checked Then
            tipocuenta2 = "Movimientos"
        ElseIf OptTodas2a.Checked Then
            tipocuenta2 = "Todas"
        End If

        If OptGrabadas1b.Checked Then
            grabadas1 = True
        ElseIf OptTodas1b.Checked Then
            grabadas1 = False
        End If

        If OptGrabadas2b.Checked Then
            grabadas2 = True
        ElseIf OptTodas2b.Checked Then
            grabadas2 = False
        End If

        Dim rsFormato As ReportBuilder2.FrmSelFormatoImpresion = New ReportBuilder2.FrmSelFormatoImpresion

        rsFormato.Proceso = "Edición de balance de saldos"

        If rsFormato.ShowDialog = DialogResult.OK Then
            documento = rsFormato.documento
            formato = rsFormato.formato
        Else
            Return False
        End If

        valido = True
        RellenarBalance(cadenatotal, CuentasWhere)
        'If Not valido Then
        '    frmEstImp.blnCancelar = False
        '    frmEstImp.Hide '*************
        '    Set frmEstImp = Nothing '*************
        '    Exit Sub
        'End If

        'If Not frmEstImp.blnCancelar Then
        '    Set frmPrevisualizacion = CreateObject("css83imp.clsformatos").FormularioPres
        '    Set frmPrevisualizacion.ObjetoGlobal = ObjetoGlobal
        '    Set frmPrevisualizacion.frmLlamador = Me

        '    If Not blnImpresionCalculada Then AbrirRecordsetFormato

        '    Load frmPrevisualizacion '*************
        '    blnImpresionCalculada = True
        'Else
        '    frmEstImp.blnCancelar = False
        '    frmEstImp.Hide '*************
        '  Set frmEstImp = Nothing '*************
        'End If
        Return True

    End Function

    Function DigitosCuentas(ByVal dato As String, Optional Nivel As Integer = 3) As String
        Dim n As Integer
        Dim nCadena As String

        If Nivel = 0 Then
            n = ObjetoGlobal.NivelDeCuenta
            nCadena = ObjetoGlobal.CadenaNivelCuenta
        Else
            n = Nivel
            nCadena = Strings.Left(ObjetoGlobal.CadenaNivelCuenta, n)
        End If

        If InStr(1, dato, ".") <> 0 Then 'hay un punto en la cadena
            If Not dato = "." Then
                If Strings.Right(dato, 1) = "." And Len(dato) > 1 Then
                    dato = Strings.Left(Mid(dato, 1, InStr(1, dato, ".") - 1) & nCadena, Len(nCadena))
                Else
                    If Len(dato) > 1 Then
                        dato = Val(Strings.Left(Mid(dato, 1, InStr(1, dato, ".") - 1) & nCadena, Len(nCadena))) + Val(Mid(dato, InStr(1, dato, ".") + 1))
                    End If
                End If
            End If
        Else
            If Trim(dato) <> "" Then
                dato = Mid(dato & nCadena, 1, Len(nCadena))
            End If
        End If
        DigitosCuentas = dato
    End Function

    Sub RellenarBalance(cadenaWhere As String, CuentasWhere As String)
        Dim i As Integer

        Dim cuenta As String
        Dim cuentaant As String

        Dim signivel As Integer
        Dim MaximoNivel As Integer
        Dim cuentainicio As Double
        Dim cuentafin As Double

        'frmEstImp.Show vbModeless
        'frmEstImp.lbltextoimpresion = "Obteniendo información de cuentas . . ."
        'frmEstImp.pbrcalculo = 0
        'frmEstImp.pbrtotal = 0
        'DoEvents

        For i = 13 To 1 Step -1
            If ObjetoGlobal.NivelesDisponibles(i) = True Then
                MaximoNivel = i
                Exit For
            End If
        Next i
        numdigitos = Val(TxtDigitos.Text)

        TotDebeApertura = 0
        TotHaberApertura = 0
        TotDebeOrigen = 0
        TotHaberOrigen = 0
        TotDebePeriodo = 0
        TotHaberPeriodo = 0
        TotDebeTotal = 0
        TotHaberTotal = 0
        TotDebeTotSinAper = 0
        TotHaberTotSinAper = 0
        TotDebeOriSinAper = 0
        TotHaberOriSinAper = 0

        'Abro rs con todos los datos de las tablas cuentas y saldos sin ordenar
        rsCuentasMayorNivel = New cnRecordset.CnRecordset
        rsCuentasMenorNivel = New cnRecordset.CnRecordset

        If CuentasWhere <> "" And CuentasWhere <> "()" Then
            rsCuentasMayorNivel.Open("SELECT CODIGO_CUENTA, DESCRIPCION_CUENTA, OBSERVACIONES, DIGITOS, CODIGO_ALTERNATIVO, DESC_ALTERNATIVA, ANALITICA_SN, COD_DIVISA, ANALITICA_DEFECTO, TIPO_CUENTA, CTA_NUMERICA " &
                            "FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND " & CuentasWhere & " AND DIGITOS=" & numdigitos & "", ObjetoGlobal.cn)
        Else
            rsCuentasMayorNivel.Open("SELECT CODIGO_CUENTA, DESCRIPCION_CUENTA, OBSERVACIONES, DIGITOS, CODIGO_ALTERNATIVO, DESC_ALTERNATIVA, ANALITICA_SN, COD_DIVISA, ANALITICA_DEFECTO, TIPO_CUENTA, CTA_NUMERICA " &
                            "FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND DIGITOS=" & numdigitos & "", ObjetoGlobal.cn)
        End If

        rsCuentasMenorNivel.Open("SELECT CODIGO_CUENTA, DESCRIPCION_CUENTA, OBSERVACIONES, DIGITOS, CODIGO_ALTERNATIVO, DESC_ALTERNATIVA, ANALITICA_SN, COD_DIVISA, ANALITICA_DEFECTO, TIPO_CUENTA, CTA_NUMERICA " &
                            "FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND CODIGO_CUENTA>='" & Strings.Left(TxtDesdecuenta.Text, 1) & "' AND CODIGO_CUENTA<='" & Trim(TxtHastacuenta.Text) & "' " & cadenaWhereDigitos() & "", ObjetoGlobal.cn)

        'Compruebo el formato que voy imprimir y relleno con sus datos el array DatosFormato
        'rsFormato.Open("SELECT * FROM zzzinformatos_p WHERE DOCUMENTO='" & documento & "' AND FORMATO='" & formato & "' AND TIPO='campo' ORDER BY ZONA,NUM_DATO", ObjetoGlobal.cn)

        'If rsFormato.RecordCount > 0 Then
        '    rsFormato.MoveFirst()
        '    Dim datImp As Object
        '    Set datFmt = Nothing
        '    Set datFmt = New Collection
        '    For i = 0 To rsFormato.RecordCount - 1
        '        Set datImp = CreateObject("css80glbl.ClsDatosImpresion")
        '        datImp.zona = Trim(rsFormato!zona)
        '        datImp.num_dato = rsFormato!num_dato
        '        datImp.tope = rsFormato!tope
        '        datImp.izquierda = rsFormato!izquierda
        '        datFmt.Add datImp, Trim(ObjetoGlobal.rsFormato!zona) & "_" & Trim(ObjetoGlobal.rsFormato!num_dato) & "_" & Trim(ObjetoGlobal.rsFormato!Indice)
        '        ObjetoGlobal.rsFormato.MoveNext
        '    Next i
        'Else
        '    MsgBox("El formato elegido no existe en la base de datos")
        '    Exit Sub
        'End If

        VuelcaLineasCabeceraBalance()

        signivel = numdigitos
        signivel = CalculaSiguienteNivel(signivel)
        If rsCuentasMayorNivel.RecordCount > 0 And ObjetoGlobal.NivelesDisponibles(numdigitos) <> 1 Then
            rsCuentasMayorNivel.MoveFirst()
            cuentaant = Trim(rsCuentasMayorNivel!CODIGO_CUENTA)
            cuenta = Trim(rsCuentasMayorNivel!CODIGO_CUENTA)
            VuelcaLineasBalance(cuenta)
            For i = 1 To rsCuentasMayorNivel.RecordCount - 1
                rsCuentasMayorNivel.MoveNext()
                cuentaant = Trim(cuenta)
                cuenta = Trim(rsCuentasMayorNivel!CODIGO_CUENTA)
                'If frmEstImp.blnCancelar Then
                '    Exit Sub
                'End If
                'frmEstImp.pbrcalculo = i / (rsCuentasMayorNivel.RecordCount - 1) * 100
                'frmEstImp.pbrtotal = frmEstImp.pbrcalculo * 0.5
                'DoEvents
1:              If Mid(cuenta, 1, signivel) <> Mid(cuentaant, 1, signivel) Then
                    VuelcaLineasBalance(Mid(cuentaant, 1, signivel))
                    signivel = CalculaSiguienteNivel(signivel)
                    If signivel > 0 Then
                        GoTo 1
                    Else
                        VuelcaLineasBalance(cuenta)
                        signivel = CalculaSiguienteNivel(numdigitos)
                    End If
                Else
                    VuelcaLineasBalance(cuenta)
                    signivel = CalculaSiguienteNivel(numdigitos)
                End If
            Next
            While signivel > 0
                VuelcaLineasBalance(Mid(cuenta, 1, signivel))
                signivel = CalculaSiguienteNivel(signivel)
            End While
        Else
            cuentainicio = Val((TxtDesdecuenta.Text) & Mid("0000000000000", 1, MaximoNivel - numdigitos))
            cuentafin = Val((TxtHastacuenta.Text) & Mid("9999999999999", 1, MaximoNivel - numdigitos))

            rsCuentasMayorNivel.Open("SELECT DISTINCT LEFT(CODIGO_CUENTA, " & numdigitos & ") AS CODIGO_CUENTA FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND CODIGO_CUENTA>='" & cuentainicio & "' AND CODIGO_CUENTA<='" & cuentafin & "' AND DIGITOS=" & MaximoNivel & " AND (COD_DIVISA='EUR' OR COD_DIVISA IS NULL)", ObjetoGlobal.cn)

            If rsCuentasMayorNivel.RecordCount > 0 Then
                'Abro cuentas menor nivel
                rsCuentasMayorNivel.MoveFirst()
                cuentaant = rsCuentasMayorNivel!CODIGO_CUENTA
                rsCuentasMayorNivel.MoveLast()
                cuenta = rsCuentasMayorNivel!CODIGO_CUENTA
                rsCuentasMenorNivel.Open("SELECT CODIGO_CUENTA, DESCRIPCION_CUENTA, OBSERVACIONES, DIGITOS, CODIGO_ALTERNATIVO, DESC_ALTERNATIVA, ANALITICA_SN, COD_DIVISA, ANALITICA_DEFECTO, TIPO_CUENTA, CTA_NUMERICA " &
                                          "FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND CODIGO_CUENTA>='" & Strings.Left(cuentaant, 1) & "' AND CODIGO_CUENTA<='" & Trim(cuenta) & "' " & cadenaWhereDigitos() & " AND (COD_DIVISA='EUR' OR COD_DIVISA='' OR COD_DIVISA IS NULL)", ObjetoGlobal.cn)

                rsCuentasMayorNivel.MoveFirst()
                cuentaant = Trim(rsCuentasMayorNivel!CODIGO_CUENTA)
                cuenta = Trim(rsCuentasMayorNivel!CODIGO_CUENTA)
                VuelcaLineasBalance(cuenta)
                For i = 1 To rsCuentasMayorNivel.RecordCount - 1
                    rsCuentasMayorNivel.MoveNext()
                    cuentaant = Trim(cuenta)
                    cuenta = Trim(rsCuentasMayorNivel!CODIGO_CUENTA)
                    'If frmEstImp.blnCancelar Then
                    '    Exit Sub
                    'End If
                    'frmEstImp.pbrcalculo = i / (rsCuentasMayorNivel.RecordCount - 1) * 100
                    'frmEstImp.pbrtotal = frmEstImp.pbrcalculo * 0.5

2:                  If Mid(cuenta, 1, signivel) <> Mid(cuentaant, 1, signivel) Then
                        VuelcaLineasBalance(Mid(cuentaant, 1, signivel))
                        signivel = CalculaSiguienteNivel(signivel)
                        If signivel > 0 Then
                            GoTo 2
                        Else
                            VuelcaLineasBalance(cuenta)
                            signivel = CalculaSiguienteNivel(numdigitos)
                        End If
                    Else
                        VuelcaLineasBalance(cuenta)
                        signivel = CalculaSiguienteNivel(numdigitos)
                    End If
                Next i
                While signivel > 0
                    VuelcaLineasBalance(Mid(cuenta, 1, signivel))
                    signivel = CalculaSiguienteNivel(signivel)
                End While
            Else
                MsgBox("No existen cuentas para la selección realizada")
                valido = False
                Exit Sub
            End If

        End If
        VuelcaLineasPiesBalance()
    End Sub

    Sub VuelcaLineasBalance(cuenta As String)
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rscal As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsSaldos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rscuentas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DebeApertura As Double, HaberApertura As Double, DebeOrigen As Double, HaberOrigen As Double
        Dim DebePeriodo As Double, HaberPeriodo As Double
        Dim numerodigitos As Integer, i As Integer
        Static Cont As Integer
        Dim siguientenivel As Integer
        Dim cuentainicio As String, cuentafin As String
        Dim nivelopcional As Boolean
        Dim y As Integer

        If BotonInforme = True Then
            Cont = 1
            BotonInforme = False
        End If


        'numdigitos es el numero de digitos pedido sacado de txtdigitos
        numerodigitos = Len(Trim(cuenta))
        nivelopcional = False

        '   ********************
        '    If rsCuentasMenorNivel.RecordCount > 0 Then
        '        Set rscuentas = IIf(numerodigitos = numdigitos, rsCuentasMayorNivel, rsCuentasMenorNivel) 'OJO
        '    Else
        '        Set rscuentas = rsCuentasMayorNivel
        '    End If
        '    rscuentas.MoveFirst
        '    rscuentas.Find "CODIGO_CUENTA='" & Trim(cuenta) & "'", 0, adSearchForward, rscuentas.Bookmark
        '    If rscuentas.RecordCount > 0 Then
        '        rscuentas.MoveFirst
        '    End If
        '   *********************
        Rscuentas = rsCuentasMenorNivel

        ' Rscuentas.Filter = "CODIGO_CUENTA='" & cuenta & "'"
        Dim RsCtasRows() As DataRow
        Dim rsCtas As DataTable

        rsCtas = Rscuentas.cnDataSet.Tables(0)
        RsCtasRows = rsCtas.Select("CODIGO_CUENTA='" & cuenta & "'")
        'Rscuentas.Close()

        If ObjetoGlobal.NivelesDisponibles(numerodigitos) = 1 Then 'nivel opcional
            If RsCtasRows.Count > 0 Then
                ''If Not Rscuentas.EOF Then
                nivelopcional = True
            Else
                nivelopcional = False
                If numerodigitos = TxtDigitos.Text And grabadas1 = True Then
                    Exit Sub
                End If
                If numerodigitos <= TxtDigitos.Text And grabadas2 = True Then
                    Exit Sub
                End If
            End If
        End If

        If ObjetoGlobal.NivelesDisponibles(numerodigitos) = False Then
            If numerodigitos = TxtDigitos.Text And grabadas1 = True Then Exit Sub
            If numerodigitos <= TxtDigitos.Text And grabadas2 = True Then Exit Sub
        End If
        '**La cuenta tiene un nivel que existe****************************************
        If ObjetoGlobal.NivelesDisponibles(numerodigitos) = True Or nivelopcional = True Then 'La cuenta tiene un nivel con el que sí trabajamos
            '**La cuenta tiene un nivel que existe y es mayor que nivel de saldos*********
            If numerodigitos >= ObjetoGlobal.NiveldeSaldos Then 'Es un nivel con el que trabajo
                If cadenatotal <> "" And cadenatotal <> "()" Then
                    rs.Open("SELECT * FROM SALDOS WHERE EMPRESA ='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO ='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & cuenta & "' AND " & cadenatotal & " AND (CODIGO_DIVISA='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                Else
                    rs.Open("SELECT * FROM SALDOS WHERE EMPRESA ='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO ='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & cuenta & "' AND (CODIGO_DIVISA ='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                End If
                '**La cuenta tiene un nivel que existe, mayor que nivel de saldos y tiene datos grabados en saldos***
                If rs.RecordCount > 0 Then 'Además tiene datos grabados en saldos

                    rscal = New cnRecordset.CnRecordset

                    If cadenatotal <> "" And cadenatotal <> "()" Then
                        rscal.Open("SELECT SUM(DEBE_PERIODO) AS DEBEAPERTURA, SUM(HABER_PERIODO) AS HABERAPERTURA FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "' AND " & cadenatotal & " AND PERIODO=" & CodigoPeriodoInicio & " AND (CODIGO_DIVISA ='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                    Else
                        rscal.Open("SELECT SUM(DEBE_PERIODO) AS DEBEAPERTURA, SUM(HABER_PERIODO) AS HABERAPERTURA FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "' AND PERIODO=" & CodigoPeriodoInicio & " AND (CODIGO_DIVISA ='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                    End If
                    If Not IsDBNull(rscal!DebeApertura) Then
                        DebeApertura = rscal!DebeApertura
                    Else
                        DebeApertura = 0
                    End If
                    If Not IsDBNull(rscal!HaberApertura) Then
                        HaberApertura = rscal!HaberApertura
                    Else
                        HaberApertura = 0
                    End If

                    rscal = New cnRecordset.CnRecordset

                    If cadenatotal <> "" And cadenatotal <> "()" Then
                        '27/06/00
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEORIGEN,SUM (HABER_PERIODO) AS HABERORIGEN FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "' AND " & cadenatotal & " AND PERIODO>" & CodigoPeriodoInicio & " AND PERIODO<=" & CodigoPeriodoFinOrigen & " AND (CODIGO_DIVISA='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                    Else
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEORIGEN,SUM (HABER_PERIODO) AS HABERORIGEN FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "' AND PERIODO>" & CodigoPeriodoInicio & " AND PERIODO<=" & CodigoPeriodoFinOrigen & " AND (CODIGO_DIVISA='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                    End If
                    If Not IsDBNull(rscal!DebeOrigen) Then
                        DebeOrigen = rscal!DebeOrigen
                    Else
                        DebeOrigen = 0
                    End If
                    If Not IsDBNull(rscal!HaberOrigen) Then
                        HaberOrigen = rscal!HaberOrigen
                    Else
                        HaberOrigen = 0
                    End If
                    rscal = New cnRecordset.CnRecordset
                    If cadenatotal <> "" And cadenatotal <> "()" Then
                        rscal.Open("SELECT SUM(DEBE_PERIODO) AS DEBEPERIODO,SUM (HABER_PERIODO) AS HABERPERIODO FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "' AND " & cadenatotal & " AND PERIODO>" & CodigoPeriodoFinOrigen & " AND PERIODO<=" & CodigoPeriodoFin & " AND (CODIGO_DIVISA='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                    Else
                        rscal.Open("SELECT SUM(DEBE_PERIODO) AS DEBEPERIODO,SUM (HABER_PERIODO) AS HABERPERIODO FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "' AND PERIODO > " & CodigoPeriodoFinOrigen & " AND PERIODO <= " & CodigoPeriodoFin & " AND (CODIGO_DIVISA='EUR' OR CODIGO_DIVISA='' OR CODIGO_DIVISA IS NULL)", ObjetoGlobal.cn)
                    End If
                    If Not IsDBNull(rscal!DebePeriodo) Then
                        DebePeriodo = rscal!DebePeriodo
                    Else
                        DebePeriodo = 0
                    End If
                    If Not IsDBNull(rscal!HaberPeriodo) Then
                        HaberPeriodo = rscal!HaberPeriodo
                    Else
                        HaberPeriodo = 0
                    End If
                    If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                        If numerodigitos = Val(TxtDigitos.Text) And tipocuenta1 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If

                        If numerodigitos < Val(TxtDigitos.Text) And tipocuenta2 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If
                    Else
                        If numerodigitos = Val(TxtDigitos.Text) And tipocuenta1 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebeOrigen) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberOrigen) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If

                        If numerodigitos < Val(TxtDigitos.Text) And tipocuenta2 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebeOrigen) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberOrigen) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If
                    End If
                    rscal.Close()
                    vuelcadato(1, "cuerpo", Cont, "calculado.codigo_cuenta", cuenta, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_periodo", DebePeriodo, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_periodo", HaberPeriodo, 1)
                    If numerodigitos = numdigitos Then
                        TotDebePeriodo = TotDebePeriodo + DebePeriodo
                        TotHaberPeriodo = TotHaberPeriodo + HaberPeriodo
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.descripcion_cuenta", "" & RsCtasRows(0)("descripcion_cuenta"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.observaciones", "" & RsCtasRows(0)("observaciones"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.digitos", "" & RsCtasRows(0)("digitos"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.codigo_alternativo", "" & RsCtasRows(0)("codigo_alternativo"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.DESC_ALTERNATIVA", "" & RsCtasRows(0)("DESC_ALTERNATIVA"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.analitica_sn", "" & RsCtasRows(0)("analitica_sn"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.analitica_defecto", "" & RsCtasRows(0)("analitica_defecto"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.cod_divisa", "" & RsCtasRows(0)("cod_divisa"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.tipo_cuenta", "" & RsCtasRows(0)("tipo_cuenta"), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.cta_numerica", "" & RsCtasRows(0)("cta_numerica"), 1)
                    If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.Debe_Apertura", DebeApertura, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_Apertura", HaberApertura, 1)
                        If numerodigitos = numdigitos Then
                            TotDebeOrigen = TotDebeOrigen + DebeApertura
                            TotHaberOrigen = TotHaberOrigen + HaberApertura
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_apertura", DebeApertura - HaberApertura, 1)
                        If (DebeApertura - HaberApertura > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                        End If
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_origen", (DebeApertura + DebeOrigen), 1) 'rs.Fields(2), 1
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_origen", (HaberApertura + HaberOrigen), 1) 'rs.Fields(2), 1
                        If numerodigitos = numdigitos Then
                            TotDebeOrigen = TotDebeOrigen + (DebeApertura + DebeOrigen)
                            TotHaberOrigen = TotHaberOrigen + (HaberApertura + HaberOrigen)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_origen", ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1) 'rs.Fields(2), 1
                        If ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)) > 0 Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1) 'rs.Fields(2), 1
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", 0, 1) 'rs.Fields(2), 1
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", 0, 1) 'rs.Fields(2), 1
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1) 'rs.Fields(2), 1
                        End If
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_periodo", (DebePeriodo - HaberPeriodo), 1) 'rs.Fields(2), 1
                    If (DebePeriodo - HaberPeriodo > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_periodo", Math.Abs(DebePeriodo - HaberPeriodo), 1) 'rs.Fields(2), 1
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_periodo", 0, 1) 'rs.Fields(2), 1
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_periodo", 0, 1) 'rs.Fields(2), 1
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_periodo", Math.Abs(DebePeriodo - HaberPeriodo), 1) 'rs.Fields(2), 1
                    End If
                    If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", (DebeApertura + DebePeriodo), 1) 'rs.Fields(2), 1
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_total", (HaberApertura + HaberPeriodo), 1) 'rs.Fields(2), 1
                        If numerodigitos = numdigitos Then
                            TotDebeTotal = TotDebeTotal + (DebeApertura + DebePeriodo)
                            TotHaberTotal = TotHaberTotal + (HaberApertura + HaberPeriodo)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_total", ((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1) 'rs.Fields(2), 1
                        If (((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1) 'rs.Fields(2), 1
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", 0, 1) 'rs.Fields(2), 1
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", 0, 1) 'rs.Fields(2), 1
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1) 'rs.Fields(2), 1
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_apertura", DebeApertura, 1) 'rs.Fields(2), 1
                        If numerodigitos = numdigitos Then TotDebeApertura = TotDebeApertura + DebeApertura
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", (DebeApertura + DebeOrigen + DebePeriodo), 1) 'rs.Fields(2), 1
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_total", (HaberApertura + HaberOrigen + HaberPeriodo), 1) 'rs.Fields(2), 1
                        If numerodigitos = numdigitos Then
                            TotDebeTotal = TotDebeTotal + (DebeApertura + DebeOrigen + DebePeriodo)
                            TotHaberTotal = TotHaberTotal + (HaberApertura + HaberOrigen + HaberPeriodo)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_total", ((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1) 'rs.Fields(2), 1
                        If (((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1) 'rs.Fields(2), 1
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", 0, 1) 'rs.Fields(2), 1
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", 0, 1) 'rs.Fields(2), 1
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1) 'rs.Fields(2), 1
                        End If
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_apertura", DebeApertura, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_apertura", HaberApertura, 1)
                    If numerodigitos = numdigitos Then
                        TotDebeApertura = TotDebeApertura + DebeApertura
                        TotHaberApertura = TotHaberApertura + HaberApertura
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_apertura", (DebeApertura - HaberApertura), 1)
                    If ((DebeApertura - HaberApertura) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_tot_s_aper", (DebeOrigen + DebePeriodo), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_tot_s_aper", (HaberOrigen + HaberPeriodo), 1)
                    If numerodigitos = numdigitos Then
                        TotDebeTotSinAper = TotDebeTotSinAper + (DebeOrigen + DebePeriodo)
                        TotHaberTotSinAper = TotHaberTotSinAper + (HaberOrigen + HaberPeriodo)
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_tot_s_aper", ((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                    If (((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_tot_s_a", Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_tot_s_a", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_tot_s_a", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_tot_s_a", Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_ori_s_aper", DebeOrigen, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_ori_s_aper", HaberOrigen, 1)
                    If numerodigitos = numdigitos Then
                        TotDebeOriSinAper = TotDebeOriSinAper + DebeOrigen
                        TotHaberOriSinAper = TotHaberOriSinAper + HaberOrigen
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculo.saldo_ori_s_aper", (DebeOrigen - HaberOrigen), 1)
                    If ((DebeOrigen - HaberOrigen) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_ori_s_a", Math.Abs(DebeOrigen - HaberOrigen), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_ori_s_a", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_ori_s_a", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_ori_s_a", Math.Abs(DebeOrigen - HaberOrigen), 1)
                    End If
                    Cont = Cont + 1
                    '**La cuenta tiene un nivel que existe,mayor que nivel de saldos y no tiene datos grabados en saldos******
                Else 'La cuenta tiene un nivel que existe, es mayor que ObjetoGlobal.NivelDeSaldos, pero no tiene nada grabado en SALDOS->No tiene mvtos.y por tanto todo 0´s
                    If numerodigitos = Val(TxtDigitos.Text) And (tipocuenta1 = "Movimientos" Or tipocuenta1 = "Saldo") Then Exit Sub
                    If (numerodigitos < Val(TxtDigitos.Text)) And (tipocuenta2 = "Movimientos" Or tipocuenta2 = "Saldo") Then Exit Sub

                    'If Not Rscuentas.EOF Then
                    If RsCtasRows.Count > 0 Then
                        DebeApertura = 0
                        HaberApertura = 0
                        DebeOrigen = 0
                        HaberOrigen = 0
                        DebePeriodo = 0
                        HaberPeriodo = 0
                        vuelcadato(1, "cuerpo", Cont, "calculado.codigo_cuenta", RsCtasRows(0)("CODIGO_CUENTA"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_periodo", DebePeriodo, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_periodo", HaberPeriodo, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.descripcion_cuenta", "" & RsCtasRows(0)("descripcion_cuenta"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.observaciones", "" & RsCtasRows(0)("observaciones"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.digitos", "" & RsCtasRows(0)("digitos"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.codigo_alternativo", "" & RsCtasRows(0)("codigo_alternativo"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.desc_alternativa", "" & RsCtasRows(0)("DESC_ALTERNATIVA"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.analitica_sn", "" & RsCtasRows(0)("analitica_sn"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.analitica_defecto", "" & RsCtasRows(0)("analitica_defecto"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.cod_divisa", "" & RsCtasRows(0)("cod_divisa"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.tipo_cuenta", "" & RsCtasRows(0)("tipo_cuenta"), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.cta_numerica", "" & RsCtasRows(0)("cta_numerica"), 1)
                        If numerodigitos = numdigitos Then
                            TotDebePeriodo = TotDebePeriodo + DebePeriodo
                            TotHaberPeriodo = TotHaberPeriodo + HaberPeriodo
                        End If
                        If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.debe_origen", DebeApertura, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.haber_origen", HaberApertura, 1)
                            If numerodigitos = numdigitos Then
                                TotDebeOrigen = TotDebeOrigen + DebeApertura
                                TotHaberOrigen = TotHaberOrigen + HaberApertura
                            End If
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_origen", DebeApertura - HaberApertura, 1)
                            If (DebeApertura - HaberApertura > 0) Then
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", Math.Abs(DebeApertura - HaberApertura), 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", 0, 1)
                            Else
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", 0, 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", Math.Abs(DebeApertura - HaberApertura), 1)
                            End If
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.debe_origen", (DebeApertura + DebeOrigen), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.haber_origen", (HaberApertura + HaberOrigen), 1)
                            If numerodigitos = numdigitos Then
                                TotDebeOrigen = TotDebeOrigen + (DebeApertura + DebeOrigen)
                                TotHaberOrigen = TotHaberOrigen + (HaberApertura + HaberOrigen)
                            End If
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_origen", ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                            If ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)) > 0 Then
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", 0, 1)
                            Else
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", 0, 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                            End If
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_periodo", (DebePeriodo - HaberPeriodo), 1)
                        If (DebePeriodo - HaberPeriodo > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_periodo", Math.Abs(DebePeriodo - HaberPeriodo), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_periodo", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_periodo", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_periodo", Math.Abs(DebePeriodo - HaberPeriodo), 1)
                        End If
                        If CodigoPeriodoInicio = CodigoPeriodoFin Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", (DebeApertura + DebePeriodo), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.haber_total", (HaberApertura + HaberPeriodo), 1)
                            If numerodigitos = numdigitos Then
                                TotDebeTotal = TotDebeTotal + (DebeApertura + DebePeriodo)
                                TotHaberTotal = TotHaberTotal + (HaberApertura + HaberPeriodo)
                            End If
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_total", ((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                            If (((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)) > 0) Then
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", 0, 1)
                            Else
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", 0, 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                            End If
                            vuelcadato(1, "cuerpo", Cont, "calculado.debe_apertura", DebeApertura, 1)
                            If numerodigitos = numdigitos Then TotDebeApertura = TotDebeApertura + DebeApertura
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", (DebeApertura + DebeOrigen + DebePeriodo), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.haber_total", (HaberApertura + HaberOrigen + HaberPeriodo), 1)
                            If numerodigitos = numdigitos Then
                                TotDebeTotal = TotDebeTotal + (DebeApertura + DebeOrigen + DebePeriodo)
                                TotHaberTotal = TotHaberTotal + (HaberApertura + HaberOrigen + HaberPeriodo)
                            End If
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_total", ((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                            If (((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)) > 0) Then
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", 0, 1)
                            Else
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", 0, 1)
                                vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                            End If
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_apertura", DebeApertura, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_apertura", HaberApertura, 1)
                        If numerodigitos = numdigitos Then
                            TotDebeApertura = TotDebeApertura + DebeApertura
                            TotHaberApertura = TotHaberApertura + HaberApertura
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_apertura", (DebeApertura - HaberApertura), 1)
                        If ((DebeApertura - HaberApertura) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_tot_s_aper", (DebeOrigen + DebePeriodo), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_tot_s_aper", (HaberOrigen + HaberPeriodo), 1)
                        If numerodigitos = numdigitos Then
                            TotDebeTotSinAper = TotDebeTotSinAper + (DebeOrigen + DebePeriodo)
                            TotHaberTotSinAper = TotHaberTotSinAper + (HaberOrigen + HaberPeriodo)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_tot_s_aper", ((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                        If (((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_tot_s_a", Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_tot_s_a", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_tot_s_a", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_tot_s_a", Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_ori_s_aper", DebeOrigen, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_ori_s_aper", HaberOrigen, 1)
                        If numerodigitos = numdigitos Then
                            TotDebeOriSinAper = TotDebeOriSinAper + DebeOrigen
                            TotHaberOriSinAper = TotHaberOriSinAper + HaberOrigen
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_ori_s_aper", (DebeOrigen - HaberOrigen), 1)
                        If ((DebeOrigen - HaberOrigen) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_ori_s_a", Math.Abs(DebeOrigen - HaberOrigen), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_ori_s_a", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_ori_s_a", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_ori_s_a", Math.Abs(DebeOrigen - HaberOrigen), 1)
                        End If
                        Cont = Cont + 1
                    End If
                End If
                '**La cuenta tiene un nivel que existe y es menor que nivel de saldos*********
            Else ' La cuenta tiene un nivel al que trabajamos pero es menor que ObjetoGlobal.NivelDeSaldos
                siguientenivel = ObjetoGlobal.NiveldeSaldos
                cuentainicio = cuenta & Mid("0000000000000", 1, siguientenivel - numerodigitos)
                cuentafin = cuenta & Mid("9999999999999", 1, siguientenivel - numerodigitos)

                rsSaldos = New cnRecordset.CnRecordset

                If cadenatotal <> "" And cadenatotal <> "()" Then
                    '28/06/00
                    rsSaldos.Open("Select * FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)

                Else
                    rsSaldos.Open("SELECT * FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                End If
                '**La cuenta tiene un nivel que existe, menor que nivel de saldos y hay datos
                'en saldos de las subcuentas grabada de la cuenta*******************************
                If rsSaldos.RecordCount > 0 Then 'Si hay datos para la cuentas que empiezan por cuenta y tienen NivelDeSaldo dígitos

                    rscal = New cnRecordset.CnRecordset

                    If cadenatotal <> "" And cadenatotal <> "()" Then
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEAPERTURA,SUM (HABER_PERIODO) AS HABERAPERTURA FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND PERIODO=" & CodigoPeriodoInicio & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                    Else
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEAPERTURA,SUM (HABER_PERIODO) AS HABERAPERTURA FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO=" & CodigoPeriodoInicio & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                    End If
                    If rscal.RecordCount > 0 And Not IsDBNull(rscal!DebeApertura) Then
                        DebeApertura = rscal!DebeApertura
                    Else
                        DebeApertura = 0
                    End If
                    If rscal.RecordCount > 0 And Not IsDBNull(rscal!HaberApertura) Then
                        HaberApertura = rscal!HaberApertura
                    Else
                        HaberApertura = 0
                    End If

                    rscal = New cnRecordset.CnRecordset

                    If cadenatotal <> "" And cadenatotal <> "()" Then
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEORIGEN,SUM (HABER_PERIODO) AS HABERORIGEN FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND PERIODO>" & CodigoPeriodoInicio & " AND PERIODO<=" & CodigoPeriodoFinOrigen & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                    Else
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEORIGEN,SUM (HABER_PERIODO) AS HABERORIGEN FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO>" & CodigoPeriodoInicio & " AND PERIODO<=" & CodigoPeriodoFinOrigen & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                    End If
                    If rscal.RecordCount > 0 And Not IsDBNull(rscal!DebeOrigen) Then
                        DebeOrigen = rscal!DebeOrigen
                    Else
                        DebeOrigen = 0
                    End If
                    If Not IsDBNull(rscal!HaberOrigen) Then
                        HaberOrigen = rscal!HaberOrigen
                    Else
                        HaberOrigen = 0
                    End If

                    rscal = New cnRecordset.CnRecordset

                    If cadenatotal <> "" And cadenatotal <> "()" Then
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEPERIODO,SUM (HABER_PERIODO) AS HABERPERIODO FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND PERIODO>" & CodigoPeriodoFinOrigen & " AND PERIODO<=" & CodigoPeriodoFin & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                    Else
                        rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEPERIODO,SUM (HABER_PERIODO) AS HABERPERIODO FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO>" & CodigoPeriodoFinOrigen & " AND PERIODO<=" & CodigoPeriodoFin & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                    End If
                    If Not IsDBNull(rscal!DebePeriodo) Then
                        DebePeriodo = rscal!DebePeriodo
                    Else
                        DebePeriodo = 0
                    End If
                    If rscal.RecordCount > 0 And Not IsDBNull(rscal!HaberPeriodo) Then
                        HaberPeriodo = rscal!HaberPeriodo
                    Else
                        HaberPeriodo = 0
                    End If
                    If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                        If numerodigitos = Val(TxtDigitos.Text) And tipocuenta1 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If

                        If numerodigitos < Val(TxtDigitos.Text) And tipocuenta2 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If
                    Else
                        If numerodigitos = Val(TxtDigitos.Text) And tipocuenta1 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebeOrigen) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberOrigen) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If

                        If numerodigitos < Val(TxtDigitos.Text) And tipocuenta2 = "Saldo" Then
                            If ((CSng(DebeApertura) + CSng(DebeOrigen) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberOrigen) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                        End If
                    End If

                    Rscuentas = New cnRecordset.CnRecordset

                    Rscuentas.Open("SELECT * FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "'", ObjetoGlobal.cn)

                    vuelcadato(1, "cuerpo", Cont, "calculado.codigo_cuenta", cuenta, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_periodo", DebePeriodo, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_periodo", HaberPeriodo, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.descripcion_cuenta", "" & Rscuentas!descripcion_cuenta, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.observaciones", "" & Rscuentas!observaciones, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.digitos", "" & Rscuentas!digitos, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.codigo_alternativo", "" & Rscuentas!codigo_alternativo, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.desc_alternativa", "" & Rscuentas!DESC_ALTERNATIVA, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.analitica_sn", "" & Rscuentas!analitica_sn, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.analitica_defecto", "" & Rscuentas!analitica_defecto, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.cod_divisa", "" & Rscuentas!cod_divisa, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.tipo_cuenta", "" & Rscuentas!tipo_cuenta, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.cta_numerica", "" & Rscuentas!cta_numerica, 1)

                    If numerodigitos = numdigitos Then
                        TotDebePeriodo = TotDebePeriodo + DebePeriodo
                        TotHaberPeriodo = TotHaberPeriodo + HaberPeriodo
                    End If
                    If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_origen", DebeApertura, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_origen", HaberApertura, 1)
                        If numerodigitos = numdigitos Then
                            TotDebeOrigen = TotDebeOrigen + DebeApertura
                            TotHaberOrigen = TotHaberOrigen + HaberApertura
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_origen", DebeApertura - HaberApertura, 1)
                        If (DebeApertura - HaberApertura > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", Math.Abs(DebeApertura - HaberApertura), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", Math.Abs(DebeApertura - HaberApertura), 1)
                        End If
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_origen", (DebeApertura + DebeOrigen), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_origen", (HaberApertura + HaberOrigen), 1)
                        If numerodigitos = numdigitos Then
                            TotDebeOrigen = TotDebeOrigen + (DebeApertura + DebeOrigen)
                            TotHaberOrigen = TotHaberOrigen + (HaberApertura + HaberOrigen)
                        End If

                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_origen", ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                        If ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)) > 0 Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_origen", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_origen", Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                        End If
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_periodo", (DebePeriodo - HaberPeriodo), 1)
                    If (DebePeriodo - HaberPeriodo > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_periodo", Math.Abs(DebePeriodo - HaberPeriodo), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_periodo", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_periodo", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_periodo", Math.Abs(DebePeriodo - HaberPeriodo), 1)
                    End If
                    If CodigoPeriodoInicio = CodigoPeriodoFin Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", (DebeApertura + DebePeriodo), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_total", (HaberApertura + HaberPeriodo), 1)
                        If numerodigitos = numdigitos Then
                            TotDebeTotal = TotDebeTotal + (DebeApertura + DebePeriodo)
                            TotHaberTotal = TotHaberTotal + (HaberApertura + HaberPeriodo)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_total", ((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                        If (((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", DebeApertura, 1)
                        If numerodigitos = numdigitos Then TotDebeApertura = TotDebeApertura + DebeApertura
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_total", (DebeApertura + DebeOrigen + DebePeriodo), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_total", (HaberApertura + HaberOrigen + HaberPeriodo), 1)
                        If numerodigitos = numdigitos Then
                            TotDebeTotal = TotDebeTotal + (DebeApertura + DebeOrigen + DebePeriodo)
                            TotHaberTotal = TotHaberTotal + (HaberApertura + HaberOrigen + HaberPeriodo)
                        End If
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_total", ((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                        If (((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)) > 0) Then
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", 0, 1)
                        Else
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_total", 0, 1)
                            vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_total", Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                        End If
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_apertura", DebeApertura, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_apertura", HaberApertura, 1)
                    If numerodigitos = numdigitos Then
                        TotDebeApertura = TotDebeApertura + DebeApertura
                        TotHaberApertura = TotHaberApertura + HaberApertura
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_apertura", (DebeApertura - HaberApertura), 1)
                    If ((DebeApertura - HaberApertura) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_apertur", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_apertur", Math.Abs(DebeApertura - HaberApertura), 1)
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_tot_s_aper", (DebeOrigen + DebePeriodo), 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_tot_s_aper", (HaberOrigen + HaberPeriodo), 1)
                    If numerodigitos = numdigitos Then
                        TotDebeTotSinAper = TotDebeTotSinAper + (DebeOrigen + DebePeriodo)
                        TotHaberTotSinAper = TotHaberTotSinAper + (HaberOrigen + HaberPeriodo)
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_tot_s_aper", ((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                    If (((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_tot_s_a", Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_tot_s_a", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_tot_s_a", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_tot_s_a", Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.debe_ori_s_aper", DebeOrigen, 1)
                    vuelcadato(1, "cuerpo", Cont, "calculado.haber_ori_s_aper", HaberOrigen, 1)
                    If numerodigitos = numdigitos Then
                        TotDebeOriSinAper = TotDebeOriSinAper + DebeOrigen
                        TotHaberOriSinAper = TotHaberOriSinAper + HaberOrigen
                    End If
                    vuelcadato(1, "cuerpo", Cont, "calculado.saldo_ori_s_aper", (DebeOrigen - HaberOrigen), 1)
                    If ((DebeOrigen - HaberOrigen) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_ori_s_a", Math.Abs(DebeOrigen - HaberOrigen), 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_ori_s_a", 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_d_ori_s_a", 0, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.saldo_h_ori_s_a", Math.Abs(DebeOrigen - HaberOrigen), 1)
                    End If
                    Cont = Cont + 1
                    Rscuentas.Close()
                    '**La cuenta tiene un nivel que existe, menor que nivel de saldos y no hay datos
                    'en saldos de las subcuentas grabada de la cuenta*******************************
                Else 'No hay datos en saldos para cuentas que empiezan por cuenta y tienen ObjetoGlobal.NivelDeSaldos dígitos-> Vuelco 0`s
                    If numerodigitos = Val(TxtDigitos.Text) And (tipocuenta1 = "Movimientos" Or tipocuenta1 = "Saldo") Then Exit Sub
                    If numerodigitos < Val(TxtDigitos.Text) And (tipocuenta2 = "Movimientos" Or tipocuenta2 = "Saldo") Then Exit Sub
                    '                                Set rscuentas = New Recordset
                    '                                rscuentas.CursorLocation = adUseClient
                    '                                rscuentas.Open( "SELECT * FROM CUENTAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND CODIGO_CUENTA='" & Trim(cuenta) & "'", ObjetoGlobal.cn)
                    'If Not Rscuentas.EOF Then
                    If RsCtasRows.Count > 0 Then
                        DebeApertura = 0
                        HaberApertura = 0
                        DebeOrigen = 0
                        HaberOrigen = 0
                        DebePeriodo = 0
                        HaberPeriodo = 0

                        'vuelcadato(1, "cuerpo", Cont, "calculado.codigo_cuenta", Rscuentas!CODIGO_CUENTA, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.debe_periodo", DebePeriodo, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.haber_periodo", HaberPeriodo, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.descripcion_cuenta", "" & Rscuentas!descripcion_cuenta, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.observaciones", "" & Rscuentas!observaciones, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.digitos", "" & Rscuentas!digitos, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.codigo_alternativo", "" & Rscuentas!codigo_alternativo, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.desc_alternativa", "" & Rscuentas!DESC_ALTERNATIVA, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.analitica_sn", "" & Rscuentas!analitica_sn, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.analitica_defecto", "" & Rscuentas!analitica_defecto, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.cod_divisa", "" & Rscuentas!cod_divisa, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.tipo_cuenta", "" & Rscuentas!tipo_cuenta, 1)
                        'vuelcadato(1, "cuerpo", Cont, "calculado.cta_numerica", "" & Rscuentas!cta_numerica, 1)

                        vuelcadato(1, "cuerpo", Cont, "calculado.codigo_cuenta", RsCtasRows(0)!CODIGO_CUENTA, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.debe_periodo", DebePeriodo, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.haber_periodo", HaberPeriodo, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.descripcion_cuenta", "" & RsCtasRows(0)!descripcion_cuenta, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.observaciones", "" & RsCtasRows(0)!observaciones, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.digitos", "" & RsCtasRows(0)!digitos, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.codigo_alternativo", "" & RsCtasRows(0)!codigo_alternativo, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.desc_alternativa", "" & RsCtasRows(0)!DESC_ALTERNATIVA, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.analitica_sn", "" & RsCtasRows(0)!analitica_sn, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.analitica_defecto", "" & RsCtasRows(0)!analitica_defecto, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.cod_divisa", "" & RsCtasRows(0)!cod_divisa, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.tipo_cuenta", "" & RsCtasRows(0)!tipo_cuenta, 1)
                        vuelcadato(1, "cuerpo", Cont, "calculado.cta_numerica", "" & RsCtasRows(0)!cta_numerica, 1)

                        For y = 14 To 41
                            vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(y), 0, 1)
                        Next y
                        Cont = Cont + 1
                    End If
                End If
            End If
            'La cuenta tiene un nivel que no existe***********************************************
        Else 'La cuenta tiene un nivel al que no trabajamos
            For i = numerodigitos + 1 To 13
                If ObjetoGlobal.NivelesDisponibles(i) = True And i >= ObjetoGlobal.NiveldeSaldos Then
                    Exit For
                End If
            Next i
            siguientenivel = i
            cuentainicio = cuenta & Mid("0000000000000", 1, siguientenivel - numerodigitos)
            cuentafin = cuenta & Mid("9999999999999", 1, siguientenivel - numerodigitos)
            If cadenatotal <> "()" And cadenatotal <> "" Then
                rsSaldos.Open("SELECT * FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & cuentainicio & " AND CTA_NUMERICA<=" & cuentafin, ObjetoGlobal.cn)
            Else
                rsSaldos.Open("SELECT * FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & cuentainicio & " AND CTA_NUMERICA<=" & cuentafin, ObjetoGlobal.cn)
            End If
            'La cuenta tiene un nivel que no existe,y las subcuentas grabadas tienen datos en saldos*
            If rsSaldos.RecordCount > 0 Then
                If cadenatotal <> "()" And cadenatotal <> "" Then
                    rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEAPERTURA,SUM (HABER_PERIODO) AS HABERAPERTURA FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND PERIODO=" & CodigoPeriodoInicio & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                Else
                    rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEAPERTURA,SUM (HABER_PERIODO) AS HABERAPERTURA FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO=" & CodigoPeriodoInicio & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                End If
                If rscal.RecordCount > 0 And Not IsDBNull(rscal!DebeApertura) Then
                    DebeApertura = rscal!DebeApertura
                Else
                    DebeApertura = 0
                End If
                If rscal.RecordCount > 0 And Not IsDBNull(rscal!HaberApertura) Then
                    HaberApertura = rscal!HaberApertura
                Else
                    HaberApertura = 0
                End If
                rscal.Close()

                If cadenatotal <> "" And cadenatotal <> "()" Then
                    rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEORIGEN,SUM (HABER_PERIODO) AS HABERORIGEN FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND PERIODO>" & CodigoPeriodoInicio & " AND PERIODO<=" & CodigoPeriodoFinOrigen & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                Else
                    rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEORIGEN,SUM (HABER_PERIODO) AS HABERORIGEN FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO>" & CodigoPeriodoInicio & " AND PERIODO<=" & CodigoPeriodoFinOrigen & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                End If
                If rscal.RecordCount > 0 And Not IsDBNull(rscal!DebeOrigen) Then
                    DebeOrigen = rscal!DebeOrigen
                Else
                    DebeOrigen = 0
                End If
                If rscal.RecordCount > 0 And Not IsDBNull(rscal!HaberOrigen) Then
                    HaberOrigen = rscal!HaberOrigen
                Else
                    HaberOrigen = 0
                End If
                rscal.Close()

                If cadenatotal <> "" And cadenatotal <> "()" Then
                    rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEPERIODO,SUM (HABER_PERIODO) AS HABERPERIODO FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND " & cadenatotal & " AND PERIODO>" & CodigoPeriodoFinOrigen & " AND PERIODO<=" & CodigoPeriodoFin & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                Else
                    rscal.Open("SELECT SUM (DEBE_PERIODO) AS DEBEPERIODO,SUM (HABER_PERIODO) AS HABERPERIODO FROM SALDOS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "' AND EJERCICIO='" & ObjetoGlobal.EjercicioActual & "' AND PERIODO>" & CodigoPeriodoFinOrigen & " AND PERIODO<=" & CodigoPeriodoFin & " AND CODIGO_DIVISA='EUR' AND CTA_NUMERICA>=" & CDbl(cuentainicio) & " AND CTA_NUMERICA<=" & CDbl(cuentafin), ObjetoGlobal.cn)
                End If
                If rscal.RecordCount > 0 And Not IsDBNull(rscal!DebePeriodo) Then
                    DebePeriodo = rscal!DebePeriodo
                Else
                    DebePeriodo = 0
                End If
                If rscal.RecordCount > 0 And Not IsDBNull(rscal!HaberPeriodo) Then
                    HaberPeriodo = rscal!HaberPeriodo
                Else
                    HaberPeriodo = 0
                End If
                If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                    If numerodigitos = Val(TxtDigitos.Text) And tipocuenta1 = "Saldo" Then
                        If ((CSng(DebeApertura) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                    End If

                    If numerodigitos < Val(TxtDigitos.Text) And tipocuenta2 = "Saldo" Then
                        If ((CSng(DebeApertura) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                    End If
                Else
                    If numerodigitos = Val(TxtDigitos.Text) And tipocuenta1 = "Saldo" Then
                        If ((CSng(DebeApertura) + CSng(DebeOrigen) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberOrigen) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                    End If

                    If numerodigitos < Val(TxtDigitos.Text) And tipocuenta2 = "Saldo" Then
                        If ((CSng(DebeApertura) + CSng(DebeOrigen) + CSng(DebePeriodo)) - (CSng(HaberApertura) + CSng(HaberOrigen) + CSng(HaberPeriodo))) = 0 Then Exit Sub
                    End If
                End If
                rscal.Close()
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(1), cuenta, 1) 'rs.Fields(2), 1
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(2), DebePeriodo, 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(3), HaberPeriodo, 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(6), "" & numerodigitos, 1) 'rs.Fields(9), 1
                If numerodigitos = numdigitos Then
                    TotDebePeriodo = TotDebePeriodo + DebePeriodo
                    TotHaberPeriodo = TotHaberPeriodo + HaberPeriodo
                End If
                If CodigoPeriodoInicio = CodigoPeriodoFinOrigen Then
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(14), DebeApertura, 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(15), HaberApertura, 1)
                    If numerodigitos = numdigitos Then
                        TotDebeOrigen = TotDebeOrigen + DebeApertura
                        TotHaberOrigen = TotHaberOrigen + HaberApertura
                    End If
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(16), DebeApertura - HaberApertura, 1)
                    If (DebeApertura - HaberApertura > 0) Then
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(17), Math.Abs(DebeApertura - HaberApertura), 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(18), 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(17), 0, 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(18), Math.Abs(DebeApertura - HaberApertura), 1)
                    End If
                Else
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(14), (DebeApertura + DebeOrigen), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(15), (HaberApertura + HaberOrigen), 1)
                    If numerodigitos = numdigitos Then
                        TotDebeOrigen = TotDebeOrigen + (DebeApertura + DebeOrigen)
                        TotHaberOrigen = TotHaberOrigen + (HaberApertura + HaberOrigen)
                    End If
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(16), ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                    If ((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)) > 0 Then
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(17), Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(18), 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(17), 0, 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(18), Math.Abs((DebeApertura + DebeOrigen) - (HaberApertura + HaberOrigen)), 1)
                    End If
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(19), (DebePeriodo - HaberPeriodo), 1)
                If (DebePeriodo - HaberPeriodo > 0) Then
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(20), Math.Abs(DebePeriodo - HaberPeriodo), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(21), 0, 1)
                Else
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(20), 0, 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(21), Math.Abs(DebePeriodo - HaberPeriodo), 1)
                End If
                If CodigoPeriodoInicio = CodigoPeriodoFin Then
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(22), (DebeApertura + DebePeriodo), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(23), (HaberApertura + HaberPeriodo), 1)
                    If numerodigitos = numdigitos Then
                        TotDebeTotal = TotDebeTotal + (DebeApertura + DebePeriodo)
                        TotHaberTotal = TotHaberTotal + (HaberApertura + HaberPeriodo)
                    End If
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(24), ((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                    If (((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(25), Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(26), 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(25), 0, 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(26), Math.Abs((DebeApertura + DebePeriodo) - (HaberApertura + HaberPeriodo)), 1)
                    End If
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(27), DebeApertura, 1)
                    If numerodigitos = numdigitos Then TotDebeApertura = TotDebeApertura + DebeApertura
                Else
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(22), (DebeApertura + DebeOrigen + DebePeriodo), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(23), (HaberApertura + HaberOrigen + HaberPeriodo), 1)
                    If numerodigitos = numdigitos Then
                        TotDebeTotal = TotDebeTotal + (DebeApertura + DebeOrigen + DebePeriodo)
                        TotHaberTotal = TotHaberTotal + (DebeApertura + DebeOrigen + DebePeriodo)
                    End If
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(24), ((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                    If (((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)) > 0) Then
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(25), Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(26), 0, 1)
                    Else
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(25), 0, 1)
                        vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(26), Math.Abs((DebeApertura + DebeOrigen + DebePeriodo) - (HaberApertura + HaberOrigen + HaberPeriodo)), 1)
                    End If
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(27), DebeApertura, 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(28), HaberApertura, 1)
                If numerodigitos = numdigitos Then
                    TotDebeApertura = TotDebeApertura + DebeApertura
                    TotHaberApertura = TotHaberApertura + HaberApertura
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(29), (DebeApertura - HaberApertura), 1)
                If ((DebeApertura - HaberApertura) > 0) Then
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(30), Math.Abs(DebeApertura - HaberApertura), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(31), 0, 1)
                Else
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(30), 0, 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(31), Math.Abs(DebeApertura - HaberApertura), 1)
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(32), (DebeOrigen + DebePeriodo), 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(33), (HaberOrigen + HaberPeriodo), 1)
                If numerodigitos = numdigitos Then
                    TotDebeTotSinAper = TotDebeTotSinAper + (DebeOrigen + DebePeriodo)
                    TotHaberTotSinAper = TotHaberTotSinAper + (HaberOrigen + HaberPeriodo)
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(34), ((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                If (((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)) > 0) Then
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(35), Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(36), 0, 1)
                Else
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(35), 0, 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(36), Math.Abs((DebeOrigen + DebePeriodo) - (HaberOrigen + HaberPeriodo)), 1)
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(37), DebeOrigen, 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(38), HaberOrigen, 1)
                If numerodigitos = numdigitos Then
                    TotDebeOriSinAper = TotDebeOriSinAper + DebeOrigen
                    TotHaberOriSinAper = TotHaberOriSinAper + HaberOrigen
                End If
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(39), (DebeOrigen - HaberOrigen), 1)
                If ((DebeOrigen - HaberOrigen) > 0) Then
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(40), Math.Abs(DebeOrigen - HaberOrigen), 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(41), 0, 1)
                Else
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(40), 0, 1)
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(41), Math.Abs(DebeOrigen - HaberOrigen), 1)
                End If
                Cont = Cont + 1
                'La cuenta tiene un nivel que no existe,y las subcuentas grabadas no
                'tienen datos en saldos*
            Else ' No trabajamos a ese nivel y no hay datos grabados para las cuentas que empiezan por cuenta y tienen siguientenivel dígitos-> vuelco 0´s
                If numerodigitos = Val(TxtDigitos.Text) And (tipocuenta1 = "Movimientos" Or tipocuenta1 = "Saldo") Then Exit Sub
                If numerodigitos < Val(TxtDigitos.Text) And (tipocuenta2 = "Movimientos" Or tipocuenta2 = "Saldo") Then Exit Sub
                DebeApertura = 0
                HaberApertura = 0
                DebeOrigen = 0
                HaberOrigen = 0
                DebePeriodo = 0
                HaberPeriodo = 0
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(1), "" & cuenta, 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(2), 0, 1)
                vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(3), 0, 1)
                For y = 14 To 41
                    vuelcadato(1, "cuerpo", Cont, aCamposDetalleInf(y), 0, 1)
                Next y
                Cont = Cont + 1
            End If
        End If
    End Sub


    '****************************************************************
    Sub VuelcaLineasCabeceraBalance()
        'Dim rsVolcado As Recordset
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        rs.Open("SELECT EMPRESAS.* FROM EMPRESAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "'", ObjetoGlobal.cn)

        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(1), ObjetoGlobal.EmpresaActual, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(2), ObjetoGlobal.DescripcionEjercicio, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(2), ObjetoGlobal.DescripcionEjercicio, 2)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(3), "Desde cuenta: " & TxtDesdecuenta.Text & " Hasta cuenta: " & TxtHastacuenta.Text, 1)
        If TxtSelecciondiarios.Text = "Detallado" Then
            vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(4), LblDiariosseleccionados.Text, 1)
            vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(4), LblDiariosseleccionados.Text, 2)
        ElseIf TxtSelecciondiarios.Text = "Desde-Hasta" Then
            vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(4), "Desde diario: " & TxtDesdediario.Text & "  Hasta diario: " & TxtHastadiario.Text, 1)
            vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(4), "Desde diario: " & TxtDesdediario.Text & "  Hasta diario: " & TxtHastadiario.Text, 2)
        ElseIf TxtSelecciondiarios.Text = "Todos" Then
            vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(4), "Todos", 1)
            vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(4), "Todos", 2)
        End If
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(5), "Periodo Inicio:  " & TxtPeriodoInicio.Text.Trim & "      Periodo Fin Origen:  " & TxtPeriodofinorigen.Text.Trim & "      Periodo Fin:  " & TxtPeriodofin.Text.Trim, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(5), "Periodo Inicio:  " & TxtPeriodoInicio.Text.Trim & "      Periodo Fin Origen:  " & TxtPeriodofinorigen.Text.Trim & "      Periodo Fin:  " & TxtPeriodofin.Text.Trim, 2)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(6), "" & TxtFecha_balance.value, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(6), "" & TxtFecha_balance.value, 2)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(18), DateTime.Now.ToString("HH:mm tt"), 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(18), DateTime.Now.ToString("HH:mm tt"), 2)

        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(7), "EUR", 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(8), "Desde cuenta: " & TxtDesdecuenta.Text.Trim & "   Hasta cuenta: " & TxtHastacuenta.Text.Trim, 1)
        'vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(9), ObjetoGlobal.NombreUSUario, 1)
        'vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(9), ObjetoGlobal.NombreUSUario, 2)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(10), "" & ObjetoGlobal.EmpresaRazonSocial, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(10), "" & ObjetoGlobal.EmpresaRazonSocial, 2)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(11), "" & rs!domicilio, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(12), "" & rs!codigo_postal, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(13), "" & rs!poblacion, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(14), "" & rs!telefono, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(15), "" & rs!fax, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(16), "" & rs!correo_electronico, 1)
        vuelcadato(1, "cabecera", 1, aCamposCabeceraInf(17), "" & rs!pagina_web, 1)
        If rs.RecordCount <> -1 Then rs.Close()

    End Sub
    '***************************************************************
    Sub VuelcaLineasPiesBalance()
        'Dim rsVolcado As Recordset
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        rs.Open("SELECT EMPRESAS.* FROM EMPRESAS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "'", ObjetoGlobal.cn)

        vuelcadato(1, "pie", 1, aCamposPieInf(1), Now, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(2), ObjetoGlobal.EmpresaActual, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(3), "" & ObjetoGlobal.EmpresaRazonSocial, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(4), "" & rs!domicilio, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(10), "" & rs!codigo_postal, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(5), "" & rs!poblacion, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(6), "" & rs!telefono, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(7), "" & rs!fax, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(8), "" & rs!correo_electronico, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(9), "" & rs!pagina_web, 2)

        vuelcadato(1, "pie", 1, aCamposPieInf(11), TotDebeApertura, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(12), TotHaberApertura, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(13), (TotDebeApertura - TotHaberApertura), 2)
        If (TotDebeApertura - TotHaberApertura) >= 0 Then
            vuelcadato(1, "pie", 1, aCamposPieInf(14), Math.Abs(TotDebeApertura - TotHaberApertura), 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(15), 0, 2)
        Else
            vuelcadato(1, "pie", 1, aCamposPieInf(14), 0, 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(15), Math.Abs(TotDebeApertura - TotHaberApertura), 2)
        End If
        vuelcadato(1, "pie", 1, aCamposPieInf(16), TotDebeOrigen, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(17), TotHaberOrigen, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(18), (TotDebeOrigen - TotHaberOrigen), 2)

        If (TotDebeOrigen - TotHaberOrigen) >= 0 Then
            vuelcadato(1, "pie", 1, aCamposPieInf(19), Math.Abs(TotDebeOrigen - TotHaberOrigen), 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(20), 0, 2)
        Else
            vuelcadato(1, "pie", 1, aCamposPieInf(19), 0, 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(20), Math.Abs(TotDebeOrigen - TotHaberOrigen), 2)
        End If
        vuelcadato(1, "pie", 1, aCamposPieInf(21), TotDebePeriodo, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(22), TotHaberPeriodo, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(23), (TotDebePeriodo - TotHaberPeriodo), 2)
        If (TotDebePeriodo - TotHaberPeriodo) >= 0 Then
            vuelcadato(1, "pie", 1, aCamposPieInf(24), Math.Abs(TotDebePeriodo - TotHaberPeriodo), 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(25), 0, 2)
        Else
            vuelcadato(1, "pie", 1, aCamposPieInf(24), 0, 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(25), Math.Abs(TotDebePeriodo - TotHaberPeriodo), 2)
        End If
        vuelcadato(1, "pie", 1, aCamposPieInf(26), TotDebeTotal, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(27), TotHaberTotal, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(28), (TotDebeTotal - TotHaberTotal), 2)
        If (TotDebeTotal - TotHaberTotal) >= 0 Then
            vuelcadato(1, "pie", 1, aCamposPieInf(29), Math.Abs(TotDebeTotal - TotHaberTotal), 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(30), 0, 2)
        Else
            vuelcadato(1, "pie", 1, aCamposPieInf(14), 0, 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(15), Math.Abs(TotDebeTotal - TotHaberTotal), 2)
        End If
        vuelcadato(1, "pie", 1, aCamposPieInf(31), TotDebeTotSinAper, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(32), TotHaberTotSinAper, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(33), (TotDebeTotSinAper - TotHaberTotSinAper), 2)
        If (TotDebeTotSinAper - TotHaberTotSinAper) >= 0 Then
            vuelcadato(1, "pie", 1, aCamposPieInf(34), Math.Abs(TotDebeTotSinAper - TotHaberTotSinAper), 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(35), 0, 2)
        Else
            vuelcadato(1, "pie", 1, aCamposPieInf(34), 0, 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(35), Math.Abs(TotDebeTotSinAper - TotHaberTotSinAper), 2)
        End If
        vuelcadato(1, "pie", 1, aCamposPieInf(36), TotDebeOriSinAper, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(37), TotHaberOriSinAper, 2)
        vuelcadato(1, "pie", 1, aCamposPieInf(38), (TotDebeOriSinAper - TotHaberOriSinAper), 2)
        If (TotDebeOriSinAper - TotHaberOriSinAper) >= 0 Then
            vuelcadato(1, "pie", 1, aCamposPieInf(39), Math.Abs(TotDebeOriSinAper - TotHaberOriSinAper), 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(40), 0, 2)
        Else
            vuelcadato(1, "pie", 1, aCamposPieInf(39), 0, 2)
            vuelcadato(1, "pie", 1, aCamposPieInf(40), Math.Abs(TotDebeOriSinAper - TotHaberOriSinAper), 2)
        End If
        If rs.RecordCount <> -1 Then
            rs.Close()
        End If
    End Sub

    '***************************************************************
    Function CalculaSiguienteNivel(signivel As Integer)
        Dim i As Integer
        Dim Nivel As Integer

        For i = signivel - 1 To 1 Step -1
            If ChkDigitos(i - 1).Checked Then
                Nivel = i
                Return i
            End If
        Next
        Return i
    End Function
    '***************************************************************

    Function cadenaWhereDigitos() As String
        Dim i As Integer, str As String

        str = "DIGITOS=" & numdigitos & " OR "
        For i = 1 To 12
            If ChkDigitos(i - 1).Checked Then str = str & "DIGITOS=" & i & " OR "
        Next
        If str <> "" Then
            cadenaWhereDigitos = " AND (" & Strings.Left(str, Len(str) - 4) & ")"
        Else
            cadenaWhereDigitos = " AND EMPRESA= '-1'"
        End If
        Return cadenaWhereDigitos

    End Function

    Private Sub Cancelar_Click(sender As Object, e As EventArgs) Handles Cancelar.Click
        bcancelar = True
        Me.Close()
    End Sub

    Private Function vuelcadato(pagina, Czona, Cont, Campo, valor, o)
        'Dim Indice As Integer = 0
        Dim Registro As Integer = 0
        Dim zona As Integer = 1

        Select Case Trim(UCase(Czona))
            Case "CABECERA"
                zona = 0
            Case "PIE"
                zona = 2
            Case Else
                zona = 1
        End Select

        oImp.VolcarAImpresion(pagina, zona, o - 1, Registro, Campo, Cont, valor) ', Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)
        Return ""
    End Function
    Private Sub CadenasWhere()

        'periodosWhere = "periodo >=" & Trim(TxtPeriodoInicio.Text) & " AND periodo <=" & TxtPeriodofin.Text
        'CodigoPeriodoInicio = TxtPeriodoInicio.Text
        'CodigoPeriodoFin = TxtPeriodofin.Text
        'CodigoPeriodoFinOrigen = TxtPeriodofinorigen.Text

        periodosWhere = "periodo >=" & Trim(TxtPeriodoInicio.SelectedValue) & " AND periodo <=" & TxtPeriodofin.SelectedValue
        CodigoPeriodoInicio = TxtPeriodoInicio.SelectedValue
        CodigoPeriodoFin = TxtPeriodofin.SelectedValue
        CodigoPeriodoFinOrigen = TxtPeriodofinorigen.SelectedValue

    End Sub
    Private Sub BtPrevisualizar_Click(sender As Object, e As EventArgs) Handles BtPrevisualizar.Click
        CadenasWhere()
        If Emitirporpantalla() Then
            oImp.Previsualizar()
        End If

    End Sub

    Private Sub BtImprimir_Click(sender As Object, e As EventArgs) Handles BtImprimir.Click
        CadenasWhere()
        If EmitirporImpresora() Then
            oImp.Imprimir()
        End If

    End Sub

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

    Private Sub TxtDesdecuenta_Validated(sender As Object, e As EventArgs) Handles TxtDesdecuenta.Validated
        TxtDesdecuenta.Text = libcomunes.DigitosCuentas(TxtDesdecuenta.Text, TxtDigitos.Text)
    End Sub

    Private Sub TxtHastacuenta_Validated(sender As Object, e As EventArgs) Handles TxtHastacuenta.Validated
        TxtHastacuenta.Text = libcomunes.DigitosCuentas(TxtHastacuenta.Text, TxtDigitos.Text, "9")
        CuentasWhere = "codigo_cuenta>='" & Val(TxtDesdecuenta.Text) & "' and codigo_cuenta<='" & Val(TxtHastacuenta.Text) & "'"
    End Sub

    Private Sub TxtDigitos_Validated(sender As Object, e As EventArgs) Handles TxtDigitos.Validated
        VerNDigitos(CInt(TxtDigitos.Text))
    End Sub

    Sub CalculoNivelDeCuenta()
        Dim RsNivelCuenta As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer

        ObjetoGlobal.NivelesDisponibles.Clear()

        While ObjetoGlobal.NivelesDisponibles.Count > 0
            ObjetoGlobal.NivelesDisponibles.Remove(ObjetoGlobal.NivelesDisponibles.Count)
        End While

        RsNivelCuenta.Open("SELECT * FROM NIVELES_CONTABLES WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "'", ObjetoGlobal.cn)
        If RsNivelCuenta.RecordCount > 0 Then
            For i = 13 To 1 Step -1
                If UCase(RsNivelCuenta("nivel_" & i)) = "S" Then
                    ObjetoGlobal.NivelDeCuenta = i
                    Exit For
                End If
            Next
            ObjetoGlobal.NiveldeSaldos = RsNivelCuenta!nivel_saldos

            ObjetoGlobal.NivelesDisponibles.Add(False)
            For i = 1 To 13
                ObjetoGlobal.NivelesDisponibles.Add(RsNivelCuenta("nivel_" & i) = "S")
                'If RsNivelCuenta("nivel_" & i) = "O" Then
                '    'NivelesDisponibles(i) = 1
                '    ObjetoGlobal.NivelesDisponibles.Remove(ObjetoGlobal.NivelesDisponibles.Count)
                '    ObjetoGlobal.NivelesDisponibles.Add(1)
                'End If
            Next
            ObjetoGlobal.CadenaNivelCuenta = ""
            For i = 1 To ObjetoGlobal.NivelDeCuenta
                ObjetoGlobal.CadenaNivelCuenta = ObjetoGlobal.CadenaNivelCuenta & "0"
            Next

        End If
        RsNivelCuenta.Close()
    End Sub

    Private Sub TxtDigitos_TextChanged(sender As Object, e As EventArgs) Handles TxtDigitos.TextChanged

    End Sub

    Private Sub TxtDesdecuenta_TextChanged(sender As Object, e As EventArgs) Handles TxtDesdecuenta.TextChanged

    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub
End Class