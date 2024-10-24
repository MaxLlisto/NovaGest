Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections
Imports System.Reflection

Public Class FrmAjustarClasificaciones
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public NumeroCalidades As Integer

    Private Sub FrmAjustarClasificaciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cabeceras()
    End Sub
    'Private Sub New()
    '    MyBase.New()
    '    ' Esta llamada es exigida por el diseñador.
    '    InitializeComponent()

    '    ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    '    Cabeceras()
    'End Sub
    Private Sub Cabeceras()

        DGClasificacion().Columns().Clear()

        Dim column0 As DataGridViewColumn = New DataGridViewCheckBoxColumn
        column0.Name = "sel"
        column0.HeaderText = "Sel"
        column0.Width = 30
        DGClasificacion().Columns.Add(column0)

        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "albaran"
        column1.HeaderText = "Albarán"
        column1.Width = 60
        DGClasificacion().Columns.Add(column1)

        Dim column2 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column2.Name = "fecha"
        column2.HeaderText = "Fecha albarán"
        column2.Width = 90
        column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "variedad"
        column3.HeaderText = "Variedad"
        column3.Width = 60
        DGClasificacion().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "kilos"
        column4.HeaderText = "Kgs.a liquidar"
        column4.Width = 60
        column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "codigo_socio"
        column5.HeaderText = "codigo_socio"
        column5.Width = 75
        DGClasificacion().Columns.Add(column5)


    End Sub

    Private Function RellenaRestoCabeceras(var) As String
        Dim SQL2 As String
        Dim Calidades(12) As String
        Dim i As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim CuantasCalidades As Integer
        Dim column As DataGridViewColumn

        SQL2 = "SELECT * from calidades_var_ej WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "'"
        SQL2 = Trim(SQL2) + " AND codigo_variedad = '" + var + "'"
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

        For i = 1 To CuantasCalidades
            column = New DataGridViewTextBoxColumn()
            column.Name = Calidades(i)
            column.HeaderText = Calidades(i)
            column.Width = 75
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGClasificacion().Columns.Add(column)
        Next

        For i = CuantasCalidades + 1 To 12
            column = New DataGridViewTextBoxColumn()
            column.Name = ""
            column.HeaderText = ""
            column.Width = 0
            DGClasificacion().Columns.Add(column)
        Next

        Return CuantasCalidades

    End Function

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub BtGrabar_Click(sender As Object, e As EventArgs) Handles BtGrabar.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim RsC As New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Integer
        Dim Totalkilos As Double

        For Each Fila As DataGridViewRow In DGClasificacion.Rows
            If Not Fila Is Nothing Then
                '//Puedes hacer una validación con el nombre de la columna
                If Fila.Cells("sel").Value Then
                    SQL = "SELECT * FROM entradas_albaranes WHERE empresa = '1' and ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND numero_albaran =" & Fila.Cells("albaran").Value & " and fecha_entrada='" & Fila.Cells("fecha").Value & "'"
                    If Rs.Open(SQL, ObjetoGlobal.cn) Then
                        If Not Rs.EOF Then
                            For i = 1 To NumeroCalidades
                                SQL = "Select * from entradas_clasif WHERE empresa = '" & Rs!empresa & "' AND ejercicio = '" & Rs!ejercicio & "' and serie_albaran='" & Rs!serie_albaran & "' AND numero_albaran =" & Rs!numero_albaran & " AND codigo_calidad=" & i
                                If RsC.Open(SQL, ObjetoGlobal.cn, True) Then
                                    If Not RsC.EOF Then
                                        '    RsC.AddNew()
                                        '    RsC!empresa = Rs!empresa
                                        '    RsC!ejercicio = Rs!ejercicio
                                        '    RsC!serie_albaran = Rs!serie_albaran
                                        '    RsC!numero_albaran = Rs!numero_albaran
                                        '    RsC!tipo_clasificacion = 'LIQ'
                                        '    RsC!codigo_calidad = i
                                        '    RsC!codigo_variedad = Rs!codigo_variedad
                                        'End If
                                        RsC!kg_muestra = Fila.Cells(5 + i).Value
                                        RsC!kg_calidad = Math.Round(Rs!kg_a_liquidar * RsC!kg_muestra / 100, 2)
                                        RsC.Update()
                                        RsC.Close()
                                    End If
                                End If
                            Next
                        End If
                    End If
                    Rs.Close()
                End If
            End If
        Next

    End Sub

    Private Sub Cargardatos_Click(sender As Object, e As EventArgs) Handles Cargardatos.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim RsC As New cnRecordset.CnRecordset
        Dim SQL As String
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim CellC As DataGridViewCheckBoxCell
        Dim clasif(NumeroCalidades) As Double

        If txtCodigo_variedad.Text.Trim = "" Then
            MsgBox("Debe de indicar un código de calidad")
            Return
        End If

        Cabeceras()
        NumeroCalidades = RellenaRestoCabeceras(txtCodigo_variedad.Text)

        SQL = "SELECT * from entradas_albaranes WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad = '" & txtCodigo_variedad.Text & "' AND fecha_entrada BETWEEN '" & CStr(Desdefecha.Value) & "' AND '" & CStr(Hastalafecha.Value) & "'"
        SQL = SQL + " And exists (SELECT * FROM entradas_clasif WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and ejercicio ='" & ObjetoGlobal.EjercicioActual & "' and  entradas_clasif.serie_albaran = entradas_albaranes.serie_albaran and entradas_clasif.numero_albaran = entradas_albaranes.numero_albaran)"
        If Rs.Open(SQL, ObjetoGlobal.cn) Then
            While Not Rs.EOF

                Row = New DataGridViewRow

                CellC = New DataGridViewCheckBoxCell
                CellC.Value = False
                Row.Cells.Add(CellC)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Rs!numero_albaran
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Rs!fecha_entrada
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Rs!codigo_variedad
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Rs!Kg_a_liquidar
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                If "" & Rs!codigo_socio = "" Then
                    Cell.Value = "Pro." & Rs!codigo_proveedor
                Else
                    Cell.Value = "Soc." & Rs!codigo_socio
                End If
                Row.Cells.Add(Cell)

                clasif = ObtenerClasificacion(Rs!serie_albaran, Rs!numero_albaran, Rs!kg_a_liquidar)

                For i = 1 To NumeroCalidades
                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = clasif(i)
                    Row.Cells.Add(Cell)
                Next
                DGClasificacion().Rows.Add(Row)
                Rs.MoveNext()
            End While
            Rs.Close()
        End If
        colorearFilas()

    End Sub

    Private Sub BtSeleccionartodo_Click(sender As Object, e As EventArgs) Handles BtSeleccionartodo.Click

        If DGClasificacion.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In DGClasificacion.Rows
                If Not Fila Is Nothing Then
                    '//Puedes hacer una validación con el nombre de la columna
                    Fila.Cells("sel").Value = True
                End If
            Next
        End If
    End Sub

    Private Sub BtDeseleccionar_Click(sender As Object, e As EventArgs) Handles BtDeseleccionar.Click
        If DGClasificacion.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In DGClasificacion.Rows
                If Not Fila Is Nothing Then
                    '//Puedes hacer una validación con el nombre de la columna
                    Fila.Cells("sel").Value = False
                End If
            Next
        End If
    End Sub

    Private Sub BtReajustar_Click(sender As Object, e As EventArgs) Handles BtReajustar.Click
        Dim SumaBuenas As Double
        Dim SumaBuenasNuevas As Double
        Dim SumaMalas As Double
        Dim Sumastotales As Double
        Dim i As Integer
        Dim Segunda As Double
        Dim NC As Double
        Dim totallinea As Double

        If TxtNC.Text.Trim = "" Then
            TxtNC.Text = "0"
        End If

        If TxtSegunda.Text.Trim = "" Then
            TxtSegunda.Text = "0"
        End If

        If DGClasificacion.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In DGClasificacion.Rows
                If Not Fila Is Nothing Then
                    '//Puedes hacer una validación con el nombre de la columna
                    If Fila.Cells("sel").Value Then
                        If Strings.Left(txtCodigo_variedad.Text, 2) = "01" Or Strings.Left(txtCodigo_variedad.Text, 2) = "02" Then
                            ' Se ajusta
                            SumaBuenas = 0
                            SumaMalas = 0
                            For i = 1 To NumeroCalidades - 1
                                SumaBuenas += Fila.Cells(5 + i).Value
                            Next
                            NC = Fila.Cells(5 + NumeroCalidades).Value
                            SumaMalas = NC
                            Sumastotales = SumaBuenas + SumaMalas

                            NC = Math.Round(NC * ((CDbl(TxtNC.Text) + 100) / 100), 2)
                            SumaBuenasNuevas = Sumastotales - NC
                            totallinea = Sumastotales - NC

                            For i = 1 To NumeroCalidades - 1
                                If SumaBuenas <> 0 Then
                                    Fila.Cells(5 + i).Value = Math.Round(Fila.Cells(5 + i).Value * SumaBuenasNuevas / SumaBuenas, 2)
                                Else
                                    Fila.Cells(5 + i).Value = 0
                                End If
                                totallinea -= Fila.Cells(5 + i).Value
                            Next
                            Fila.Cells(5 + NumeroCalidades).Value = Math.Round((NC + totallinea), 2)
                        Else
                            ' Se ajusta
                            SumaBuenas = 0
                            SumaMalas = 0
                            For i = 1 To NumeroCalidades - 2
                                SumaBuenas += Fila.Cells(5 + i).Value
                            Next
                            Segunda = Fila.Cells(5 + NumeroCalidades - 1).Value
                            NC = Fila.Cells(5 + NumeroCalidades).Value
                            SumaMalas = Segunda + NC
                            Sumastotales = SumaBuenas + SumaMalas

                            Segunda = Math.Round(Segunda * ((CDbl(TxtSegunda.Text) + 100) / 100), 2)

                            NC = Math.Round(NC * ((CDbl(TxtNC.Text) + 100) / 100), 2)
                            SumaBuenasNuevas = Sumastotales - (Segunda + NC)
                            totallinea = Sumastotales - (Segunda + NC)

                            For i = 1 To NumeroCalidades - 2
                                If SumaBuenas <> 0 Then
                                    Fila.Cells(5 + i).Value = Math.Round(Fila.Cells(5 + i).Value * SumaBuenasNuevas / SumaBuenas, 2)
                                Else
                                    Fila.Cells(5 + i).Value = 0
                                End If
                                totallinea -= Fila.Cells(5 + i).Value

                            Next
                            Fila.Cells(5 + NumeroCalidades - 1).Value = Segunda
                            Fila.Cells(5 + NumeroCalidades).Value = Math.Round((NC + totallinea), 2)
                        End If

                    End If
                    End If
            Next
        End If

    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub

    Private Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ExportarDatosExcel(DGClasificacion, "Clasificaciones")
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
                If reg.Cells("sel").Value Then
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
                End If

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
    Private Function ObtenerClasificacion(ser, num, Kg_totales) As Double()
        Dim Sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim sumas(NumeroCalidades) As Double
        Dim Porc(NumeroCalidades) As Double
        Dim SubtotalB As Double = 0
        Dim SubtotalM As Double = 0
        Dim totales As Double
        Dim TotalMalo As Double
        Dim dif As Double = 100
        Dim TotalKgClasif As Double


        Sql = "SELECT * FROM entradas_clasif WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
        Sql = Sql + " serie_albaran = '" & ser & "' AND numero_albaran =" & num & " and tipo_clasificacion = 'LIQ' ORDER BY codigo_calidad"
        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            If Not Rs.EOF Then

                'TotalKgClasif = ObtenerPesos(ser, num)

                While Not Rs.EOF
                    sumas(Rs!codigo_calidad) = Rs!kg_muestra
                    Rs.MoveNext()
                End While

                'For i = 1 To NumeroCalidades - 2
                '    SubtotalB += sumas(i)
                'Next
                'SubtotalM = TotalKgClasif - SubtotalB

                'TotalMalo = sumas(NumeroCalidades - 1) + sumas(NumeroCalidades)

                'For i = NumeroCalidades - 1 To NumeroCalidades
                '    sumas(i) = Math.Round(sumas(i) * SubtotalM / TotalMalo, 2)
                'Next

                'For i = 1 To NumeroCalidades
                '    Porc(i) = Math.Round(sumas(i) * 100 / TotalKgClasif, 2)
                '    dif -= Porc(i)
                'Next
                ''For i = NumeroCalidades - 1 To NumeroCalidades
                ''    Porc(i) = Math.Round(sumas(i) * 100 / TotalKgClasif , 2)
                ''    dif -= Porc(i)
                ''Next
                'Porc(NumeroCalidades) += Math.Round(dif, 2)
            Else
                For i = 1 To NumeroCalidades
                    sumas(i) = 0
                Next
            End If

        End If


        Return sumas


    End Function

    Private Function ObtenerPesos(ser, Alb) As Double
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rs1 As New cnRecordset.CnRecordset
        Dim PesoClasificado As Double = 0


        sql = "SELECT DISTINCT bulto, empresa, ejercicio, serie_albaran, numero_albaran  FROM entradas_lotes_test WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & Trim(ObjetoGlobal.EjercicioActual) & "' and serie_albaran ='" & Trim(ser) & "' and numero_albaran= " & Alb

        If Rs.Open(sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                sql = "select CAST(ISNULL(dbo.fn_peso_bulto('" & Trim(Rs!empresa) & "','" & Trim(Rs!ejercicio) & "','" & Trim(Rs!serie_albaran) & "', " & Rs!numero_albaran & ", " & Rs!bulto & "),0) As Integer) As peso_bultos"
                If Rs1.Open(sql, ObjetoGlobal.cn) Then
                    PesoClasificado += Rs1!peso_bultos
                End If
                Rs1.Close()
                Rs.MoveNext()
            End While
        End If
        Rs.Close()

        Return PesoClasificado

    End Function

    Private Sub colorearFilas()

        For Each fila As DataGridViewRow In DGClasificacion.Rows
            If Not fila.Cells("codigo_socio").Value Is Nothing Then
                Select Case Strings.Left(fila.Cells("codigo_socio").Value.ToString(), 3)
                    Case "Soc"
                        fila.DefaultCellStyle.BackColor = Color.LightGreen
                    Case "Pro"
                        fila.DefaultCellStyle.BackColor = Color.Orange 
                End Select
            End If
        Next

    End Sub

End Class