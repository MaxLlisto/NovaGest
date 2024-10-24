Public Class BalanceMasas
    Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim MinAlbaran As Long
    Dim MaxAlbaran As Long
    Dim aDefectos() As Double
    Dim aTitulos() As String
    Dim DictNCAlbaran As New Dictionary(Of String, Integer)
    Dim cAlbaranesVolcados As String

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        CabeceraDetallado()
        CabeceraResumido()

    End Sub
    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Dim DGV As DataGridView = DGResumido
        Dim tit As String = "Informe resumido"


        If DGInformes.SelectedTab.Text = Detallado.Text Then
            DGV = DGDetallado
            tit = "Informe detallado por palets"
        Else 'DGInformes.SelectedTab.Text = resumido.Text Then
            DGV = DGResumido
            tit = "Informe resumido"
        End If
        ExportarDatosExcel(DGV, tit)


    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Dim Msg As MsgBoxResult
        Msg = MsgBox("Cerrar el modulo, ¿Desea salir?", vbYesNo, "Salir del Modulo")
        If Msg = MsgBoxResult.Yes Then
            Application.ExitThread()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub BtBuscar_Click(sender As Object, e As EventArgs) Handles BtBuscar.Click
        Dim nOrd As Integer
        Dim aCal(0) As Array
        Dim aCad(0) As String

        If OpcionVolcados.Checked Then
            nOrd = 0
        ElseIf OpcionVolcados1.Checked Then
            nOrd = 1
        End If

        If nOrd > 0 And TxtVariedad.Text.Trim.Length = 0 Then
            nOrd = 0
        End If

        'espera.Visible = True

        CabeceraDetallado()
        CabeceraResumido()

        MostrarDetallados()
        MostrarResumidos()
        If TxtVariedad.Text.Trim() <> "" Then
        End If

        ' espera.Visible = False

    End Sub
    Private Sub MostrarDetallados()
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs1 As New cnRecordset.CnRecordset
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim PbEurep As Integer
        Dim pbNoeurep As Integer
        Dim numero_albaran As String
        Dim vuelta As Integer

        Sql = "SELECT c.*, l.codigo_variedad, l.numero_linea, l.palets, l.bultos, l.bruto, l.neto, v.codigo_familia from cabeceras_salida c LEFT JOIN lineas_salida l ON l.empresa = c.empresa AND l.ejercicio = c.ejercicio AND l.serie = c.serie AND l.tipo_documento = c.tipo_documento AND "
        Sql = Sql + " l.numero_documento = c.numero_documento "
        Sql = Sql + " LEFT JOIN variedades v ON v.empresa = c.empresa AND v.codigo_variedad = l.codigo_variedad "
        Sql = Sql + " WHERE c.empresa ='" & Trim(ObjetoGlobal.EmpresaActual) & "' and c.ejercicio ='" & Trim(ObjetoGlobal.EjercicioActual) & "' and c.fecha_documento between '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        If Trim(TxtVariedad.Text) > "" Then
            Sql = Trim(Sql) + " AND v.codigo_familia = '" + Trim(TxtVariedad.Text) + "'"
        End If
        Sql = Sql + " AND exists (SELECT p.empresa FROM palets_salidas p WHERE p.empresa = c.empresa AND p.ejercicio = c.ejercicio AND p.serie = c.serie AND p.tipo_documento = c.tipo_documento AND p.numero_documento = c.numero_documento)"
        Sql = Sql + " order by c.empresa, c.ejercicio, c.serie, c.tipo_documento, c.numero_documento"

        numero_albaran = ""

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF

                Row = New DataGridViewRow
                If numero_albaran <> Rs!numero_documento Then
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Rs!numero_documento
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Rs!fecha_documento
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Trim("" & Rs!codigo_cliente) + " " + Trim("" & Rs!nombre_cliente)
                    Row.Cells.Add(Cell)
                Else
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = ""
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = ""
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = ""
                    Row.Cells.Add(Cell)
                End If

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & Rs!numero_linea)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & Rs!codigo_variedad)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & Rs!palets)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & Rs!bultos)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & Rs!bruto)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & Rs!neto)
                Row.Cells.Add(Cell)

                Sql = "select s.*, p.peso_bruto FROM palets_salidas s LEFT JOIN palets p ON p.empresa = s.empresa AND p.codigo_palet = s.codigo_palet WHERE s.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and s.ejercicio ='" & Trim(ObjetoGlobal.EjercicioActual) & "' AND s.serie = '" & Trim(Rs!serie) & "' AND s.tipo_documento = '" & Trim(Rs!tipo_documento) & "' AND s.numero_documento = '" & Trim(Rs!numero_documento) & "' and s.numero_linea =" & Rs!numero_linea

                vuelta = 1

                If Rs1.Open(Sql, ObjetoGlobal.cn) Then
                    While Not Rs1.EOF

                        If vuelta > 1 Then
                            Row = New DataGridViewRow

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)
                        End If

                        vuelta = vuelta + 1

                        PbEurep = 0
                        pbNoeurep = 0

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & Rs1!codigo_palet)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & Rs1!peso_bruto)
                        Row.Cells.Add(Cell)


                        DevuelveBalance(Rs1!codigo_palet, Rs1!ejercicio, PbEurep, pbNoeurep)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & PbEurep)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & pbNoeurep)
                        Row.Cells.Add(Cell)
                        DGDetallado.Rows.Add(Row)
                        Rs1.MoveNext()

                    End While
                End If
                Rs1.Close()
                'DGDetallado.Rows.Add(Row)
                Rs.MoveNext()
            End While
        End If




    End Sub
    Private Sub MostrarResumidos()
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs1 As New cnRecordset.CnRecordset
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim PbEurep As Integer
        Dim pbNoeurep As Integer
        Dim numero_albaran As String

        Dim nPalets As Integer
        Dim nBultos As Integer
        Dim nBruto As Double
        Dim nNeto As Double


        Sql = "SELECT c.*, l.codigo_variedad, l.numero_linea, l.palets, l.bultos, l.bruto, l.neto, v.codigo_familia from cabeceras_salida c LEFT JOIN lineas_salida l ON l.empresa = c.empresa AND l.ejercicio = c.ejercicio AND l.serie = c.serie AND l.tipo_documento = c.tipo_documento AND "
        Sql = Sql + " l.numero_documento = c.numero_documento "
        Sql = Sql + " LEFT JOIN variedades v ON v.empresa = c.empresa AND v.codigo_variedad = l.codigo_variedad "
        Sql = Sql + " WHERE c.empresa ='" & Trim(ObjetoGlobal.EmpresaActual) & "' and c.ejercicio ='" & Trim(ObjetoGlobal.EjercicioActual) & "' and c.fecha_documento between '" + Format(CDate(DPFechaVolcado.Value), "dd/MM/yyyy") + "' AND '" + Format(CDate(DPHastaFecha.Value), "dd/MM/yyyy") + "'"
        If Trim(TxtVariedad.Text) > "" Then
            Sql = Trim(Sql) + " AND v.codigo_familia = '" + Trim(TxtVariedad.Text) + "'"
        End If
        Sql = Sql + " AND exists (SELECT p.empresa FROM palets_salidas p WHERE p.empresa = c.empresa AND p.ejercicio = c.ejercicio AND p.serie = c.serie AND p.tipo_documento = c.tipo_documento AND p.numero_documento = c.numero_documento)"
        Sql = Sql + " order by c.empresa, c.ejercicio, c.serie, c.tipo_documento, c.numero_documento"

        numero_albaran = ""

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF

                If numero_albaran <> Rs!numero_documento Then

                    Row = New DataGridViewRow

                    If Trim(numero_albaran) <> "" Then
                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & nPalets)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & nBultos)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & nBruto)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & nNeto)
                        Row.Cells.Add(Cell)


                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & PbEurep)
                        Row.Cells.Add(Cell)

                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = Trim("" & pbNoeurep)
                        Row.Cells.Add(Cell)

                        DGResumido.Rows.Add(Row)

                        nPalets = 0
                        nBultos = 0
                        nBruto = 0
                        nNeto = 0
                        PbEurep = 0
                        pbNoeurep = 0

                    End If

                    numero_albaran = Rs!numero_documento

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Rs!numero_documento
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Rs!fecha_documento
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Trim("" & Rs!codigo_cliente) + " " + Trim("" & Rs!nombre_cliente)
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Trim("" & Rs!codigo_variedad)
                    Row.Cells.Add(Cell)

                End If

                nPalets = nPalets + Rs!palets
                nBultos = nBultos + Rs!bultos
                nBruto = nBruto + Rs!bruto
                nNeto = nNeto + Rs!neto

                Sql = "select s.*, p.peso_bruto FROM palets_salidas s LEFT JOIN palets p ON p.empresa = s.empresa AND p.codigo_palet = s.codigo_palet WHERE s.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and s.ejercicio ='" & Trim(ObjetoGlobal.EjercicioActual) & "' AND s.serie = '" & Trim(Rs!serie) & "' AND s.tipo_documento = '" & Trim(Rs!tipo_documento) & "' AND s.numero_documento = '" & Trim(Rs!numero_documento) & "' and s.numero_linea =" & Rs!numero_linea

                If Rs1.Open(Sql, ObjetoGlobal.cn) Then
                    While Not Rs1.EOF
                        DevuelveBalance(Rs1!codigo_palet, Rs1!ejercicio, PbEurep, pbNoeurep)
                        Rs1.MoveNext()
                    End While
                End If
                Rs1.Close()
                Rs.MoveNext()
            End While

            If Trim(numero_albaran) <> "" Then

                Row = New DataGridViewRow

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & nPalets)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & nBultos)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & nBruto)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & nNeto)
                Row.Cells.Add(Cell)


                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & PbEurep)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & pbNoeurep)
                Row.Cells.Add(Cell)

                DGResumido.Rows.Add(Row)

            End If
        End If


    End Sub

    Private Sub DevuelveBalance(ByVal palet As String, ByVal ejercicio As String, ByRef PbEurep As Double, ByRef pbNoeurep As Double)
        Dim Rs As New cnRecordset.CnRecordset
        Dim Sql As String

        Sql = "SELECT p.*, c.eurep FROM palets_trazab p LEFT JOIN entradas_albaranes a ON a.empresa = p.empresa AND a.ejercicio = p.ejercicio AND a.serie_albaran = p.serie_albaran AND "
        Sql = Sql + " a.numero_albaran = p.numero_albaran LEFT JOIN cultivos c ON c.empresa = p.empresa AND c.ejercicio = p.ejercicio AND c.codigo_campo = a.codigo_campo AND "
        Sql = Sql + " c.codigo_variedad = a.codigo_variedad WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.ejercicio = '" & ejercicio & "' AND p.codigo_palet = '" & palet & "'"
        Sql = Sql + " and tipo_entrada = 'S'"

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                If "" & Rs!eurep = "S" Then
                    PbEurep = PbEurep + 1
                Else
                    pbNoeurep = pbNoeurep + 1
                End If
                Rs.MoveNext()
            End While
        End If
        Rs.Close()

        Sql = "SELECT p.*, c.eurep FROM palets_trazab p LEFT JOIN entradas_albaranes a ON a.empresa = p.empresa AND a.ejercicio = p.ejercicio AND a.serie_albaran = p.serie_albaran AND "
        Sql = Sql + " a.numero_albaran = p.numero_albaran LEFT JOIN campos_terceros c ON c.empresa = p.empresa AND c.ejercicio = p.ejercicio AND c.codigo_campo = a.codigo_campo AND "
        Sql = Sql + " c.codigo_variedad = a.codigo_variedad WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.ejercicio = '" & ejercicio & "' AND p.codigo_palet = '" & palet & "'"
        Sql = Sql + " and tipo_entrada = 'T'"

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                If "" & Rs!eurep = "S" Then
                    PbEurep = PbEurep + 1
                Else
                    pbNoeurep = pbNoeurep + 1
                End If
                Rs.MoveNext()
            End While
        End If
        Rs.Close()
        If PbEurep = 0 And pbNoeurep = 0 Then
            'MsgBox("veamos")
        End If

    End Sub


    Private Sub CabeceraDetallado()

        DGDetallado().Columns().Clear()

        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "albarán"
        column1.HeaderText = "Albarán"
        column1.Width = 75
        DGDetallado().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "fecha"
        column2.HeaderText = "Fecha"
        column2.Width = 100
        DGDetallado().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "cliente"
        column3.HeaderText = "Cliente"
        column3.Width = 275
        DGDetallado().Columns.Add(column3)


        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "linea"
        column4.HeaderText = "Línea"
        column4.Width = 60
        DGDetallado().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "variedad"
        column5.HeaderText = "Variedad"
        column5.Width = 60
        DGDetallado().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "palest"
        column6.HeaderText = "Palets"
        column6.Width = 60
        DGDetallado().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "bultos"
        column7.HeaderText = "Bultos"
        column7.Width = 65
        DGDetallado().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "pbto"
        column8.HeaderText = "Peso"
        column8.Width = 65
        DGDetallado().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "pneto"
        column9.HeaderText = "Neto"
        column9.Width = 65
        DGDetallado().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "palet"
        column10.HeaderText = "Código palet"
        column10.Width = 100
        DGDetallado().Columns.Add(column10)

        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "peso"
        column11.HeaderText = "Peso palet"
        column11.Width = 65
        DGDetallado().Columns.Add(column11)

        Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column12.Name = "pglobal"
        column12.HeaderText = "Global"
        column12.Width = 50
        DGDetallado().Columns.Add(column12)

        Dim column13 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column13.Name = "pnoglobal"
        column13.HeaderText = "No global"
        column13.Width = 50
        DGDetallado().Columns.Add(column13)

    End Sub

    Private Sub CabeceraResumido()

        DGResumido().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "albarán"
        column1.HeaderText = "Albarán"
        column1.Width = 80
        DGResumido().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "fecha"
        column2.HeaderText = "Fecha"
        column2.Width = 100
        DGResumido().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "cliente"
        column3.HeaderText = "Cliente"
        column3.Width = 300
        DGResumido().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "variedad"
        column4.HeaderText = "Variedad"
        column4.Width = 80
        DGResumido().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "palets"
        column5.HeaderText = "Palets"
        column5.Width = 80
        DGResumido().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "bultos"
        column6.HeaderText = "Bultos"
        column6.Width = 80
        DGResumido().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "pbto"
        column7.HeaderText = "Bruto"
        column7.Width = 80
        DGResumido().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "pneto"
        column8.HeaderText = "Neto"
        column8.Width = 80
        DGResumido().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "pglobal"
        column9.HeaderText = "Global"
        column9.Width = 80
        DGResumido().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "pnoglobal"
        column10.HeaderText = "No global"
        column10.Width = 80
        DGResumido().Columns.Add(column10)

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
        End With

        m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
    End Sub


    Private Sub DPFechaVolcado_ValueChanged(sender As Object, e As EventArgs) Handles DPFechaVolcado.ValueChanged
        DPHastaFecha.Value = DPFechaVolcado.Value
    End Sub

    Private Sub TxtVariedad_TextChanged(sender As Object, e As EventArgs) Handles TxtVariedad.TextChanged
        If TxtVariedad.Text.Trim() <> "" Then
            OpcionVolcados1.Checked = True
        End If
    End Sub
    Public Property og() As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get

        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property



End Class

