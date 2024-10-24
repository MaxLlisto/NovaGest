Imports System.IO

Public Class reto
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim MinAlbaran As Long
    Dim MaxAlbaran As Long
    Dim aDefectos() As Double
    Dim aTitulos() As String
    Dim DictNCAlbaran As New Dictionary(Of String, Integer)
    Dim cAlbaranesVolcados As String
    Dim ruta As String
    Dim archivo As String
    Dim ropo_coop_d As String = "104601920SS"
    Dim fcaducidad_d As Date = "25/06/2023"
    Dim ropo_coop_t As String = "104601606ST"
    Dim fcaducidad_t As Date = "11/02/2024"
    Dim nif_coop As String = "F46026068"
    Dim MsgErrores As String
    Dim aErrores(0) As String

    Private Const Sep As String = ";"

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
        'ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
        'If ObjetoGlobal.Inicializar("") = False Then
        '    MsgBox("No se puede abrir la conexión a la BD")
        'End If

        'SQL = "SELECT dbo.fn_que_ejercicio('" & fechadehoy & "') as ejercicio"
        'If rs.Open(SQL, ObjetoGlobal.cn) Then
        '    EjercicioActual = rs("ejercicio").ToString.Trim
        'End If

        'ObjetoGlobal.EjercicioActual = "2021" 'EjercicioActual

        Cabeceras(DGVCompras)
        Cabeceras(DGVVentas)
        Cabeceras(DGVAdquisiciones)
        Cabeceras(DGVAplicaciones)

    End Sub

    Private Sub BtBuscar_Click(sender As Object, e As EventArgs) Handles BtBuscar.Click
        ReDim aErrores(0)
        MsgErrores = ""
        VerErrores.Enabled = False
        Cabeceras(DGVCompras)
        Cabeceras(DGVVentas)
        Cabeceras(DGVAdquisiciones)
        Cabeceras(DGVAplicaciones)

        compras()
        Ventas()
        adquisiciones()
        aplicaciones()
        If MsgErrores <> "" Then
            VerErrores.Enabled = True
        End If

    End Sub

    Private Sub Cabeceras(ByRef DGV As DataGridView)

        DGV.Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "tipo"
        column1.HeaderText = "Tipo"
        column1.Width = 100
        DGV.Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "fecha"
        column2.HeaderText = "Fecha"
        column2.Width = 100
        DGV.Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "nifd"
        column3.HeaderText = "NIF dest."
        column3.Width = 100
        DGV.Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "ropod"
        column4.HeaderText = "ROPO dest."
        column4.Width = 100
        DGV.Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "entidad"
        column5.HeaderText = "Entidad"
        column5.Width = 100
        DGV.Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "cod"
        column6.HeaderText = "Código"

        If DGV.Name = "DGVVentas" Then
            column6.Width = 100
        Else
            column6.Width = 10
        End If
        DGV.Columns.Add(column6)

        'Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column7.Name = "nifa"
        'column7.HeaderText = "NIF autor."
        'column7.Width = 0
        'DGV.Columns.Add(column7)

        'Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column8.Name = "nombreaut"
        'column8.HeaderText = ""
        'column8.Width = 0
        'DGV.Columns.Add(column8)

        'Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column9.Name = "apellidoa"
        'column9.HeaderText = ""
        'column9.Width = 0
        'DGV.Columns.Add(column9)

        'Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column10.Name = "entidad"
        'column10.HeaderText = ""
        'column10.Width = 0
        'DGV.Columns.Add(column10)

        'Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column11.Name = "email"
        'column11.HeaderText = ""
        'column11.Width = 0
        'DGV.Columns.Add(column11)

        'Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column12.Name = "telefono"
        'column12.HeaderText = ""
        'column12.Width = 0
        'DGV.Columns.Add(column12)

        'Dim column13 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column13.Name = "direcciond"
        'column13.HeaderText = ""
        'column13.Width = 0
        'DGV.Columns.Add(column13)

        'Dim column14 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column14.Name = "cpd"
        'column14.HeaderText = ""
        'column14.Width = 0
        'DGV.Columns.Add(column14)

        'Dim column15 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        'column15.Name = "entidada"
        'column15.HeaderText = ""
        'column15.Width = 0
        'DGV.Columns.Add(column15)

        Dim column16 As DataGridViewColumn = New DataGridViewTextBoxColumn() '7
        column16.Name = "registro"
        column16.HeaderText = "Núm. registro"
        column16.Width = 120
        DGV.Columns.Add(column16)

        Dim column17 As DataGridViewColumn = New DataGridViewTextBoxColumn() '8
        column17.Name = "nombrecom"
        column17.HeaderText = "Nombre comerc."
        column17.Width = 200
        DGV.Columns.Add(column17)

        Dim column18 As DataGridViewColumn = New DataGridViewTextBoxColumn() '9
        column18.Name = "lote"
        column18.HeaderText = "Lote"
        column18.Width = 100
        DGV.Columns.Add(column18)

        Dim column19 As DataGridViewColumn = New DataGridViewTextBoxColumn() '10
        column19.Name = "unidad"
        column19.HeaderText = "Unidad"
        column19.Width = 100
        DGV.Columns.Add(column19)

        Dim column20 As DataGridViewColumn = New DataGridViewTextBoxColumn() '11
        column20.Name = "volumen"
        column20.HeaderText = "Volumen"
        column20.Width = 100
        DGV.Columns.Add(column20)

        Dim column21 As DataGridViewColumn = New DataGridViewTextBoxColumn() '12
        column21.Name = "capacidad"
        column21.HeaderText = "Capacidad"
        column21.Width = 100
        DGV.Columns.Add(column21)

        Dim column22 As DataGridViewColumn = New DataGridViewTextBoxColumn() '13
        column22.Name = "envases"
        column22.HeaderText = "Envases"
        column22.Width = 100
        DGV.Columns.Add(column22)

        Dim column23 As DataGridViewColumn = New DataGridViewTextBoxColumn() '14
        column23.Name = "total"
        column23.HeaderText = "Vol. total"
        column23.Width = 100
        DGV.Columns.Add(column23)

        Dim column24 As DataGridViewColumn = New DataGridViewTextBoxColumn() '15
        column24.Name = "vto"
        column24.Width = 0
        DGV.Columns.Add(column24)

        Dim column25 As DataGridViewColumn = New DataGridViewTextBoxColumn() '16
        column25.Name = "direccion"
        column25.Width = 0
        DGV.Columns.Add(column25)

        Dim column26 As DataGridViewColumn = New DataGridViewTextBoxColumn() '17
        column26.Name = "autorizado"
        column26.Width = 0
        DGV.Columns.Add(column26)

        Dim column27 As DataGridViewColumn = New DataGridViewTextBoxColumn() '18
        column27.Name = "Objeto del tratamiento"
        If DGV.Name = DGVAplicaciones.Name Then
            column27.Width = 300
        Else
            column27.Width = 0
        End If

        DGV.Columns.Add(column27)

        Dim column28 As DataGridViewColumn = New DataGridViewTextBoxColumn() '19
        column28.Name = "Denom.común"
        column28.Width = 200
        DGV.Columns.Add(column28)

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Dim Msg As MsgBoxResult
        Msg = MsgBox("Cerrar el modulo, ¿Desea salir?", vbYesNo, "Salir del Modulo")
        If Msg = MsgBoxResult.Yes Then
            Close()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Dim DGV As DataGridView
        Dim tit As String


        If TabControl1.SelectedTab.Text = TbCompras.Text Then
            DGV = DGVCompras
            tit = "Compras"
        ElseIf TabControl1.SelectedTab.Text = TbVentas.Text Then
            DGV = DGVVentas
            tit = "Ventas"
        ElseIf TabControl1.SelectedTab.Text = TbAdquiciones.Text Then
            DGV = DGVAdquisiciones
            tit = "Adquisiciones"
        ElseIf TabControl1.SelectedTab.Text = TbAplicaciones.Text Then
            DGV = DGVAplicaciones
            tit = "Aplicaciones"
        End If
        ExportarDatosExcel(DGV, tit, 1)

    End Sub

    Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal titulo As String, ByVal libro As Integer)
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

    Public Sub compras()
        Dim sql As String
        Dim adatos(26) As String
        Dim rs As New cnRecordset.CnRecordset
        Dim rsdp As New cnRecordset.CnRecordset
        Dim Cargardireccion As Boolean

        ' Compras a distribuidores
        sql = "Select l.empresa, l.numero_entrada, l.articulo, l.unidades_stock, l.observaciones, c.fecha_albaran, c.codigo_proveedor, p.cif, t.numero_registro, "
        sql = sql + "a.descripcion, a.unidad_volumen, a.volumen, a.unidad_peso, a.peso_bruto, a.peso_neto,"
        sql = sql + "p.ropo, p.fecha_vto_ropo, p.razon_social, isnull(t.d_comun, 'N') as dc "
        sql = sql + "From albaran_com_l l "
        sql = sql + "Left Join albaran_com_c c ON c.empresa = l.empresa And c.numero_entrada = l.numero_entrada "
        sql = sql + "Left Join tarifas_articulo t ON  t.empresa = l.empresa And t.codigo_articulo=l.articulo "
        sql = sql + "Left Join articulos a ON  a.empresa = l.empresa And a.codigo_articulo=l.articulo "
        sql = sql + "Left Join proveedores_coop p ON p.codigo_proveedor = c.codigo_proveedor "
        sql = sql + "WHERE l.empresa = '11' AND l.unidades_stock >= 0 AND c.fecha_albaran BETWEEN '" & Format(DPDesdeFecha.Value, "dd/MM/yyyy") & "' AND '" & Format(DPHastaFecha.Value, "dd/MM/yyyy") & "' And (Not t.numero_registro Is Null and t.numero_registro<>'')"
        If rs.Open(sql, ObjetoGlobal.cn) Then
            While Not rs.EOF
                ReDim adatos(28)
                Cargardireccion = False
                adatos(1) = "11"
                adatos(2) = rs!fecha_albaran
                adatos(3) = "" & rs!cif
                adatos(4) = "" & rs!ropo
                adatos(5) = rs!razon_social
                adatos(6) = rs!codigo_proveedor
                adatos(7) = "" : adatos(8) = "" : adatos(9) = "" : adatos(10) = "" : adatos(11) = "" : adatos(12) = "" : adatos(13) = "" : adatos(14) = "" : adatos(15) = ""
                adatos(16) = "" & rs!numero_registro
                adatos(17) = "" & LimpiarNombreComercial("" & rs!descripcion, rs!numero_registro, rs!dc)
                adatos(18) = "" & rs!observaciones
                adatos(28) = "" & rs!dc

                If Trim("" & rs!unidad_volumen) <> "" Then

                    Select Case UCase(Trim(rs!unidad_volumen))
                        Case "L"
                            adatos(19) = "3"
                        Case "CC"
                            adatos(19) = "6"
                        Case Else
                            MyMsgBox("Error en unidad de volumen artículo: " & rs!articulo)

                    End Select
                    If rs!volumen > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!volumen, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(rs!unidades_stock, 0), "#####0")
                    ElseIf rs!volumen = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades_stock, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de volumen artículo: " & rs!articulo)
                    End If
                ElseIf Trim("" & rs!unidad_peso) <> "" Then

                    If UCase(Trim(rs!unidad_peso)) = "GR" Then
                        adatos(19) = "1"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "KG" Or UCase(Trim(rs!unidad_peso)) = "K" Then
                        adatos(19) = "2"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "MG" Or UCase(Trim(rs!unidad_peso)) = "MG/UD" Then
                        adatos(19) = "4"
                    Else
                        MyMsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    End If

                    'Select Case UCase(Trim(rs!unidad_volumen))
                    '    Case "GR"
                    '        adatos(19) = "1"
                    '    Case "KG" Or "K"
                    '        adatos(19) = "2"
                    '    Case "MG" Or "MG/UD"
                    '        adatos(19) = "4"
                    '    Case Else
                    '        MsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    'End Select
                    If rs!peso_neto > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!peso_neto, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(rs!unidades_stock, 0), "#####0")
                    ElseIf rs!peso_neto = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades_stock, 2), "####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de peso artículo: " & rs!articulo)
                    End If
                Else
                    MyMsgBox("Debe de existir una unidad de volumen o peso en el artículo: " & rs!articulo)
                End If
                If Trim("" & rs!fecha_vto_ropo) <> "" Then
                    adatos(24) = rs!fecha_vto_ropo
                    If rs!fecha_vto_ropo < rs!fecha_albaran Then
                        Cargardireccion = True
                    End If
                Else
                    adatos(24) = ""
                    If Trim(adatos(4)) = "" Then
                        Cargardireccion = True
                    Else
                        MyMsgBox("Falta la fecha de caducidad del proveedor: " & rs!codigo_proveedor)
                    End If
                End If
                If Cargardireccion Then
                    adatos(25) = True
                Else
                    adatos(25) = False
                End If
                adatos(26) = ""
                adatos(27) = ""
                Cargar(DGVCompras, adatos)
                rs.MoveNext()
            End While


        End If
        rs.Close()
        ' Devoluciones a distribuidores
        sql = "Select l.empresa, l.numero_entrada, l.articulo, l.unidades_stock, l.observaciones, c.fecha_albaran, c.codigo_proveedor, p.cif, t.numero_registro, "
        sql = sql + "a.descripcion, a.unidad_volumen, a.volumen, a.unidad_peso, a.peso_bruto, a.peso_neto,"
        sql = sql + "p.ropo, p.fecha_vto_ropo, p.razon_social, isnull(t.d_comun, 'N') as dc "
        sql = sql + "From albaran_com_l l "
        sql = sql + "Left Join albaran_com_c c ON c.empresa = l.empresa And c.numero_entrada = l.numero_entrada "
        sql = sql + "Left Join tarifas_articulo t ON  t.empresa = l.empresa And t.codigo_articulo=l.articulo "
        sql = sql + "Left Join articulos a ON  a.empresa = l.empresa And a.codigo_articulo=l.articulo "
        sql = sql + "Left Join proveedores_coop p ON p.codigo_proveedor = c.codigo_proveedor "
        sql = sql + "WHERE l.empresa = '11' AND l.unidades_stock < 0 AND c.fecha_albaran BETWEEN '" & Format(DPDesdeFecha.Value, "dd/MM/yyyy") & "' AND '" & Format(DPHastaFecha.Value, "dd/MM/yyyy") & "' And (Not t.numero_registro Is Null and t.numero_registro<>'')"
        If rs.Open(sql, ObjetoGlobal.cn) Then
            While Not rs.EOF
                ReDim adatos(28)
                Cargardireccion = False
                adatos(1) = "12"
                adatos(2) = rs!fecha_albaran
                adatos(3) = "" & rs!cif
                adatos(4) = "" & rs!ropo
                adatos(5) = rs!razon_social
                adatos(6) = rs!codigo_proveedor
                adatos(7) = "" : adatos(8) = "" : adatos(9) = "" : adatos(10) = "" : adatos(11) = "" : adatos(12) = "" : adatos(13) = "" : adatos(14) = "" : adatos(15) = ""
                adatos(16) = "" & rs!numero_registro
                adatos(17) = "" & LimpiarNombreComercial("" & rs!descripcion, rs!numero_registro, rs!dc)
                adatos(18) = "" & rs!observaciones
                adatos(28) = "" & rs!dc
                If Trim("" & rs!unidad_volumen) <> "" Then

                    Select Case UCase(Trim(rs!unidad_volumen))
                        Case "L"
                            adatos(19) = "3"
                        Case "CC"
                            adatos(19) = "6"
                        Case Else
                            MyMsgBox("Error en unidad de volumen artículo: " & rs!articulo)

                    End Select
                    If rs!volumen > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!volumen, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(-1 * rs!unidades_stock, 0), "#####0")
                    ElseIf rs!volumen = 1 Then
                        adatos(23) = "" & Format(Math.Round(-1 * rs!unidades_stock, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de volumen artículo: " & rs!articulo)
                    End If
                ElseIf Trim("" & rs!unidad_peso) <> "" Then

                    If UCase(Trim(rs!unidad_peso)) = "GR" Then
                        adatos(19) = "1"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "KG" Or UCase(Trim(rs!unidad_peso)) = "K" Then
                        adatos(19) = "2"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "MG" Or UCase(Trim(rs!unidad_peso)) = "MG/UD" Then
                        adatos(19) = "4"
                    Else
                        MyMsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    End If

                    'Select Case UCase(Trim(rs!unidad_volumen))
                    '    Case "GR"
                    '        adatos(19) = "1"
                    '    Case "KG" Or "K"
                    '        adatos(19) = "2"
                    '    Case "MG" Or "MG/UD"
                    '        adatos(19) = "4"
                    '    Case Else
                    '        MsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    'End Select
                    If rs!peso_neto > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!peso_neto, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(-1 * rs!unidades_stock, 0), "#####0")
                    ElseIf rs!peso_neto = 1 Then
                        adatos(23) = "" & Format(Math.Round(-1 * rs!unidades_stock, 2), "####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de peso artículo: " & rs!articulo)
                    End If
                Else
                    MyMsgBox("Debe de existir una unidad de volumen o peso en el artículo: " & rs!articulo)
                End If
                If Trim("" & rs!fecha_vto_ropo) <> "" Then
                    adatos(24) = rs!fecha_vto_ropo
                    If rs!fecha_vto_ropo < rs!fecha_albaran Then
                        Cargardireccion = True
                    End If
                Else
                    adatos(24) = ""
                    If Trim(adatos(4)) = "" Then
                        Cargardireccion = True
                    Else
                        MyMsgBox("Falta la fecha de caducidad del proveedor: " & rs!codigo_proveedor)
                    End If
                End If
                If Cargardireccion Then
                    adatos(25) = True
                Else
                    adatos(25) = False
                End If
                adatos(26) = ""
                adatos(27) = ""
                Cargar(DGVCompras, adatos)
                rs.MoveNext()
            End While


        End If


    End Sub

    Public Sub Ventas()
        Dim sql As String
        Dim adatos(26) As String
        Dim rs As New cnRecordset.CnRecordset
        Dim rsdp As New cnRecordset.CnRecordset
        Dim Cargardireccion As Boolean

        sql = "Select l.empresa, l.serie_albaran, l.numero_albaran, l.articulo, l.unidades, l.lote, c.fecha_albaran, c.codigo_cliente, c.cif_cliente, c.modo_envio, t.numero_registro, "
        sql = sql + "a.descripcion, a.unidad_volumen, a.volumen, a.unidad_peso, a.peso_bruto, a.peso_neto,"
        sql = sql + "cl.ropo, cl.fecha_vto_ropo, cl.razon_social, isnull(t.d_comun, 'N') as dc "
        sql = sql + "From lineas_albaran l "
        sql = sql + "Left Join cabeceras_albaran c ON c.empresa = l.empresa And c.serie_albaran = l.serie_albaran and c.numero_albaran = l.numero_albaran "
        sql = sql + "Left Join tarifas_articulo t ON  t.empresa = l.empresa And t.codigo_articulo=l.articulo "
        sql = sql + "Left Join articulos a ON  a.empresa = l.empresa And a.codigo_articulo=l.articulo "
        sql = sql + "Left Join clientes_coop cl ON cl.codigo_cliente = c.codigo_cliente "
        sql = sql + "WHERE l.empresa = '11' AND c.fecha_albaran BETWEEN '" & Format(DPDesdeFecha.Value, "dd/MM/yyyy") & "' AND '" & Format(DPHastaFecha.Value, "dd/MM/yyyy") & "' And (Not t.numero_registro Is Null and t.numero_registro<>'')"
        If rs.Open(sql, ObjetoGlobal.cn) Then
            While Not rs.EOF
                ReDim adatos(28)
                Cargardireccion = False
                adatos(1) = "2"
                adatos(2) = rs!fecha_albaran
                If Trim(rs!cif_cliente) = "F46026068" Then
                    adatos(3) = rs!cif_cliente
                    adatos(4) = ropo_coop_t
                    adatos(24) = fcaducidad_t
                Else
                    adatos(3) = rs!cif_cliente
                    adatos(4) = "" & rs!ropo
                End If
                adatos(5) = "" & rs!razon_social
                adatos(6) = "" & rs!codigo_cliente
                adatos(7) = "" : adatos(8) = "" : adatos(9) = "" : adatos(10) = "" : adatos(11) = "" : adatos(12) = "" : adatos(13) = "" : adatos(14) = "" : adatos(15) = ""
                adatos(16) = "" & rs!numero_registro
                adatos(17) = "" & LimpiarNombreComercial("" & rs!descripcion, rs!numero_registro, rs!dc)
                adatos(18) = "" & rs!lote
                adatos(28) = "" & rs!dc
                If Trim("" & rs!unidad_volumen) <> "" Then

                    If UCase(Trim(rs!unidad_volumen)) = "L" Then
                        adatos(19) = "3"
                    ElseIf UCase(Trim(rs!unidad_volumen)) = "CC" Then
                        adatos(19) = "6"
                    ElseIf UCase(Trim(rs!unidad_volumen)) = "ML" Then
                        adatos(19) = "5"
                    Else
                        MyMsgBox("Error en unidad de volumen artículo: " & rs!articulo)
                    End If

                    If rs!volumen > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!volumen, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(rs!unidades, 0), "#####0")
                    ElseIf rs!volumen = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de volumen artículo: " & rs!articulo)
                    End If
                ElseIf Trim("" & rs!unidad_peso) <> "" Then

                    If UCase(Trim(rs!unidad_peso)) = "GR" Then
                        adatos(19) = "1"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "KG" Or UCase(Trim(rs!unidad_peso)) = "K" Then
                        adatos(19) = "2"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "MG" Or UCase(Trim(rs!unidad_peso)) = "MG/UD" Then
                        adatos(19) = "4"
                    Else
                        MyMsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    End If

                    'Select Case UCase(Trim(rs!unidad_volumen))
                    '    Case "GR"
                    '        adatos(19) = "1"
                    '    Case "KG" Or "K"
                    '        adatos(18) = "2"
                    '    Case "MG" Or "MG/UD"
                    '        adatos(19) = "4"
                    '    Case Else
                    '        MsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    'End Select
                    If rs!peso_neto > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!peso_neto, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(rs!unidades, 0), "#####0")
                    ElseIf rs!peso_neto = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de peso artículo: " & rs!articulo)
                    End If
                Else
                    MyMsgBox("Debe de existir una unidad de volumen o peso en el artículo: " & rs!articulo)
                End If
                If Trim("" & rs!fecha_vto_ropo) <> "" Then
                    adatos(24) = rs!fecha_vto_ropo
                    If rs!fecha_vto_ropo < rs!fecha_albaran Then
                        Cargardireccion = True
                    End If
                ElseIf Trim(rs!cif_cliente) <> "F46026068" Then
                    adatos(24) = ""
                    If Trim(adatos(4)) = "" Then
                        Cargardireccion = True
                    Else
                        MyMsgBox("Falta la fecha de caducidad del cliente: " & rs!codigo_cliente)
                    End If
                End If
                If Trim(adatos(4)) = "" Then
                    adatos(26) = rs!modo_envio
                End If
                If Cargardireccion Then
                    adatos(25) = False
                Else
                    adatos(25) = False
                End If
                Cargar(DGVVentas, adatos)
                rs.MoveNext()
            End While
        End If

    End Sub

    Public Sub adquisiciones()
        Dim sql As String
        Dim adatos(28) As String
        Dim rs As New cnRecordset.CnRecordset
        Dim rsdp As New cnRecordset.CnRecordset
        Dim Cargardireccion As Boolean

        sql = "Select l.empresa, l.serie_albaran, l.numero_albaran, l.articulo, l.unidades, l.lote, c.fecha_albaran, c.codigo_cliente, c.cif_cliente, t.numero_registro, "
        sql = sql + "a.descripcion, a.unidad_volumen, a.volumen, a.unidad_peso, a.peso_bruto, a.peso_neto,"
        sql = sql + "cl.razon_social, cl.ropo, cl.fecha_vto_ropo, isnull(t.d_comun, 'N') as dc "
        sql = sql + "From lineas_albaran l "
        sql = sql + "Left Join cabeceras_albaran c ON c.empresa = l.empresa And c.serie_albaran = l.serie_albaran and c.numero_albaran = l.numero_albaran "
        sql = sql + "Left Join tarifas_articulo t ON  t.empresa = l.empresa And t.codigo_articulo=l.articulo "
        sql = sql + "Left Join articulos a ON  a.empresa = l.empresa And a.codigo_articulo=l.articulo "
        sql = sql + "Left Join clientes_coop cl ON cl.codigo_cliente = c.codigo_cliente "
        sql = sql + "WHERE l.empresa = '11' AND c.fecha_albaran BETWEEN '" & Format(DPDesdeFecha.Value, "dd/MM/yyyy") & "' AND '" & Format(DPHastaFecha.Value, "dd/MM/yyyy") & "' AND c.cif_cliente = 'F46026068' And t.numero_registro > '1'"
        If rs.Open(sql, ObjetoGlobal.cn) Then
            While Not rs.EOF
                ReDim adatos(28)
                Cargardireccion = False
                adatos(1) = "7"
                adatos(2) = rs!fecha_albaran
                adatos(3) = "" & rs!cif_cliente
                adatos(4) = "" & ropo_coop_d
                adatos(5) = "" & rs!razon_social
                adatos(6) = rs!codigo_cliente
                adatos(7) = "" : adatos(8) = "" : adatos(9) = "" : adatos(10) = "" : adatos(11) = "" : adatos(12) = "" : adatos(13) = "" : adatos(14) = "" : adatos(15) = ""
                adatos(16) = "" & rs!numero_registro
                adatos(17) = "" & LimpiarNombreComercial("" & rs!descripcion, rs!numero_registro, rs!dc)
                adatos(18) = "" & rs!lote
                adatos(28) = "" & rs!dc
                If Trim("" & rs!unidad_volumen) <> "" Then


                    If UCase(Trim(rs!unidad_volumen)) = "L" Then
                        adatos(19) = "3"
                    ElseIf UCase(Trim(rs!unidad_volumen)) = "CC" Then
                        adatos(19) = "6"
                    ElseIf UCase(Trim(rs!unidad_volumen)) = "ML" Then
                        adatos(19) = "5"
                    Else
                        MyMsgBox("Error en unidad de volumen artículo: " & rs!articulo)
                    End If

                    If rs!volumen > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!volumen, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(rs!unidades, 0), "#####0")
                    ElseIf rs!volumen = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de volumen artículo: " & rs!articulo)
                    End If
                ElseIf Trim("" & rs!unidad_peso) <> "" Then

                    If UCase(Trim(rs!unidad_peso)) = "GR" Then
                        adatos(19) = "1"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "KG" Or UCase(Trim(rs!unidad_peso)) = "K" Then
                        adatos(19) = "2"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "MG" Or UCase(Trim(rs!unidad_peso)) = "MG/UD" Then
                        adatos(19) = "4"
                    Else
                        MyMsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    End If

                    If rs!peso_neto > 1 Then
                        adatos(21) = "" & Format(Math.Round(rs!peso_neto, 2), "#####0.00")
                        adatos(22) = "" & Format(Math.Round(rs!unidades, 0), "#####0")
                    ElseIf rs!peso_neto = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de peso artículo: " & rs!articulo)

                    End If
                Else
                    MyMsgBox("Debe de existir una unidad de volumen o peso en el artículo: " & rs!articulo)
                End If
                If Trim("" & rs!fecha_vto_ropo) <> "" Then
                    adatos(24) = rs!fecha_vto_ropo
                    If rs!fecha_vto_ropo < rs!fecha_albaran Then
                        Cargardireccion = True
                    End If
                ElseIf Trim(rs!cif_cliente) <> "F46026068" Then
                    adatos(24) = ""
                    If Trim(adatos(4)) = "" Then
                        Cargardireccion = True
                    Else
                        MyMsgBox("Falta la fecha de caducidad del proveedor: " & rs!codigo_cliente)
                    End If
                End If
                If Cargardireccion Then
                    adatos(25) = True
                Else
                    adatos(25) = False
                End If
                adatos(26) = ""
                adatos(27) = ""
                Cargar(DGVAdquisiciones, adatos)
                rs.MoveNext()
            End While


        End If


    End Sub
    Public Sub aplicaciones()
        Dim sql As String
        Dim adatos(27) As String
        Dim rs As New cnRecordset.CnRecordset
        Dim rsdp As New cnRecordset.CnRecordset
        Dim Cargardireccion As Boolean
        'rs.Close()

        sql = "Select pt.empresa, pt.codigo_articulo, pt.unidades_utilizadas, pt.fecha_utilizacion, pt.lote, ps.fecha_parte, so.nif_socio, "
        sql = sql + "t.numero_registro, a.descripcion, a.unidad_volumen, a.volumen, a.unidad_peso, a.peso_bruto, a.peso_neto, so.apellidos_socio, so.nombre_socio, so.codigo_socio, ps.observaciones , isnull(t.d_comun, 'N') as dc "
        sql = sql + "From partes_tratamientos pt "
        sql = sql + "Left Join partes_servicios ps ON ps.empresa = pt.empresa And ps.codigo_parte = pt.codigo_parte "
        sql = sql + "Left Join servicios s ON s.empresa = ps.empresa And s.codigo_servicio = ps.codigo_servicio "
        sql = sql + "Left Join socios_coop so ON so.codigo_socio = s.codigo_socio "
        sql = sql + "Left Join articulos a ON  a.empresa = '11' And a.codigo_articulo=pt.codigo_articulo "
        sql = sql + "Left Join tarifas_articulo t ON  t.empresa = a.empresa And t.codigo_articulo=a.codigo_articulo "
        sql = sql + "WHERE pt.empresa = '4' AND ps.fecha_parte BETWEEN '" & Format(DPDesdeFecha.Value, "dd/MM/yyyy") & "' AND '" & Format(DPHastaFecha.Value, "dd/MM/yyyy") & "' AND (ps.situacion <> 'A' and  ps.situacion <> 'X')  AND so.nif_socio <> 'F46026068' And (Not t.numero_registro Is Null and t.numero_registro<>'') and s.tipo_servicio <> 'RECOMENDACIONES' and ps.tipo_servicio <> 'RECOMENDACIONES'"
        If rs.Open(sql, ObjetoGlobal.cn) Then
            While Not rs.EOF
                ReDim adatos(28)
                Cargardireccion = False
                adatos(1) = "8"
                adatos(2) = rs!fecha_parte
                adatos(3) = rs!nif_socio
                adatos(4) = ""
                adatos(5) = Trim(rs!apellidos_socio) & "," & Trim(rs!nombre_socio)
                adatos(6) = rs!codigo_socio
                adatos(7) = "" : adatos(8) = "" : adatos(9) = "" : adatos(10) = "" : adatos(11) = "" : adatos(12) = "" : adatos(13) = "" : adatos(14) = "" : adatos(15) = ""

                adatos(16) = "" & rs!numero_registro
                adatos(17) = "" & LimpiarNombreComercial("" & rs!descripcion, rs!numero_registro, rs!dc)
                adatos(18) = "" & rs!lote
                adatos(27) = "" & rs!observaciones
                adatos(28) = "" & rs!dc

                If Trim("" & rs!unidad_volumen) <> "" Then

                    If UCase(Trim(rs!unidad_volumen)) = "L" Then
                        adatos(19) = "3"
                    ElseIf UCase(Trim(rs!unidad_volumen)) = "CC" Then
                        adatos(19) = "6"
                    ElseIf UCase(Trim(rs!unidad_volumen)) = "ML" Then
                        adatos(19) = "5"
                    Else
                        MyMsgBox("Error en unidad de volumen artículo: " & rs!codigo_articulo)
                    End If

                    'Select Case UCase(Trim(rs!unidad_volumen))
                    '    Case "L"
                    '        adatos(19) = "3"
                    '    Case "CC" Or "ML"
                    '        adatos(19) = "5"
                    '    Case Else
                    '        MsgBox("Error en unidad de volumen artículo: " & rs!articulo)
                    'End Select
                    If rs!volumen > 1 Then
                        adatos(21) = "" '& Format(Math.Round(rs!volumen, 2), "#####0.00")
                        adatos(22) = "" '& Format(Math.Round(rs!unidades_utilizadas, 0), "#####0")
                        adatos(23) = "" & Format(Math.Round(rs!unidades_utilizadas * rs!volumen, 2), "#####0.00")
                        '      adatos(23) = ""
                    ElseIf rs!volumen = 1 Then
                        adatos(21) = ""
                        adatos(22) = ""
                        adatos(23) = "" & Format(Math.Round(rs!unidades_utilizadas, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de volumen artículo: " & rs!codigo_articulo)
                    End If
                ElseIf Trim("" & rs!unidad_peso) <> "" Then

                    If UCase(Trim(rs!unidad_peso)) = "GR" Then
                        adatos(19) = "1"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "KG" Or UCase(Trim(rs!unidad_peso)) = "K" Then
                        adatos(19) = "2"
                    ElseIf UCase(Trim(rs!unidad_peso)) = "MG" Or UCase(Trim(rs!unidad_peso)) = "MG/UD" Then
                        adatos(19) = "4"
                    Else
                        MyMsgBox("Error en unidad de volumen artículo: " & rs!codigo_articulo)
                    End If

                    'Select Case UCase(Trim(rs!unidad_volumen))
                    '    Case "GR"
                    '        adatos(19) = "1"
                    '    Case "KG" Or "K"
                    '        adatos(19) = "2"
                    '    Case "MG" Or "MG/UD"
                    '        adatos(19) = "4"
                    '    Case Else
                    '        MsgBox("Error en unidad de peso artículo: " & rs!articulo)
                    'End Select

                    If rs!peso_neto > 1 Then
                        adatos(21) = "" '& Format(Math.Round(rs!peso_neto, 2), "#####0.00")
                        adatos(22) = "" '& Format(Math.Round(rs!unidades_utilizadas, 0), "#####0")
                        adatos(23) = "" & Format(Math.Round(rs!unidades_utilizadas * rs!peso_neto, 2), "#####0.00")
                    ElseIf rs!peso_neto = 1 Then
                        adatos(23) = "" & Format(Math.Round(rs!unidades_utilizadas, 2), "#####0.00")
                    Else
                        MyMsgBox("Error en unidad de unidades de peso artículo: " & rs!codigo_articulo)
                    End If
                Else
                    MyMsgBox("Debe de existir una unidad de volumen o peso en el artículo: " & rs!codigo_articulo)
                End If
                'If Trim("" & rs!fecha_vto_ropo) <> "" Then
                '    adatos(24) = rs!fecha_vto_ropo
                '    If rs!fecha_vto_ropo < rs!fecha_albaran Then
                '        Cargardireccion = True
                '    End If
                'Else
                '    adatos(24) = ""
                '    If Trim(adatos(4)) = "" Then
                '        Cargardireccion = True
                '    Else
                '        MyMsgBox("Falta la fecha de caducidad del proveedor: " & rs!codigo_cliente)
                '    End If
                'End If

                adatos(25) = True ' En socios siempre cargamos la dirección

                Cargar(DGVAplicaciones, adatos)
                rs.MoveNext()
            End While
            DGVAplicaciones.Columns(18).Width = 300


        End If


    End Sub

    Public Sub Cargar(ByRef oDGV As DataGridView, ByRef datos() As String)
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim i As Integer

        Row = New DataGridViewRow
        For i = 1 To UBound(datos)
            If i < 7 Or i > 15 Then
                Cell = New DataGridViewTextBoxCell
                Cell.Value = datos(i)
                Row.Cells.Add(Cell)
            End If
        Next
        oDGV.Rows.Add(Row)

    End Sub

    Public Sub hacerfichero()
        Dim fs As FileStream

        ':::Validamos si la carpeta de ruta existe, si no existe la creamos
        Try
            If File.Exists(ruta) Then
                ':::Si la carpeta existe creamos o sobreescribios el archivo txt
                fs = File.Create(ruta & archivo)
                fs.Close()
            Else
                ':::Si la carpeta no existe la creamos
                Directory.CreateDirectory(ruta)
                ':::Una vez creada la carpeta creamos o sobreescribios el archivo txt
                fs = File.Create(ruta & archivo)
                fs.Close()
            End If

        Catch ex As Exception
            MsgBox("Se presento un problema al momento de crear el archivo: " & ex.Message, MsgBoxStyle.Critical, "RETO")
        End Try

        Try
            Dim writeFile As System.IO.TextWriter = New StreamWriter(ruta & archivo)
            writeFile.WriteLine("vb.net-informations.com")
            writeFile.Flush()
            writeFile.Close()
            writeFile = Nothing
        Catch ex As IOException
            MsgBox(ex.ToString)
        End Try

    End Sub
    Public Function escribir(archivo) As Boolean
        Dim writeFile As System.IO.StreamWriter
        Dim hayerrores As Boolean = False

        Try

            writeFile = My.Computer.FileSystem.OpenTextFileWriter(archivo, False)
            ' Escribimos la cabecera
            writeFile.WriteLine("Tipo;Fecha;NIFResponsable;ROPOResponsable;Operacion;NIFDestino;ROPODestino;EntidadDestino;CorreoElectronicoDestino;TelefonoDestino;FaxDestino;DireccionDestino;CodPostalDestino;PaisDestino;ProvinciaDestino;LocalidadDestino;NIFPersonaAutorizada;NombrePersonaAutorizada;PrimerApellidoPersonaAutorizada;SegundoApellidoPersonaAutorizada;EmpresaExplotacionUsuarioProfesional")
            If escribirfichero(DGVCompras, "1", writeFile) Then
                hayerrores = True
            End If
            If escribirfichero(DGVVentas, "2", writeFile) Then
                hayerrores = True
            End If
            If escribirfichero(DGVAdquisiciones, "7", writeFile) Then
                hayerrores = True
            End If
            If escribirfichero(DGVAplicaciones, "8", writeFile) Then
                hayerrores = True
            End If
            writeFile.Flush()
            writeFile.Close()
            writeFile = Nothing
            If hayerrores Then
                MsgBox("Se ha producido un error en la emisión del fichero, puedes revisarlos en la ventana de errores")
                VerErrores.Enabled = True
            End If
            Return hayerrores
        Catch ex As Exception
            MsgBox("Problemas al escribir el archivo" + ex.Message)
            writeFile.Close()
            writeFile = Nothing
            VerErrores.Enabled = True
            Return True
        End Try

    End Function
    Public Function escribirfichero(ByRef oDGV As DataGridView, ByVal tipo As String, ByRef oFsw As System.IO.StreamWriter) As Boolean
        Dim i As Integer
        Dim linea As String
        Dim codigo As String
        Dim sql As String
        Dim rs As New cnRecordset.CnRecordset
        Dim rsIne As New cnRecordset.CnRecordset
        Dim email As String
        Dim telefono As String
        Dim fax As String
        Dim movil As String
        Dim direccion As String
        Dim cp As String
        Dim pais As String
        Dim provincia As String
        Dim ine As String
        Dim hayerrores As Boolean = False

        For i = 0 To oDGV.Rows.Count - 2
            linea = "1" & Sep & Trim(oDGV.Rows(i).Cells(1).Value) & Sep & Trim(nif_coop) & Sep
            If tipo < 7 Then
                linea = linea & Trim(ropo_coop_d) & Sep
            Else
                linea = linea & Trim(ropo_coop_t) & Sep
            End If
            'If tipo = 1 Then
            '    linea = linea & oDGV.Rows(i).Cells(0).Value & Sep
            'Else
            '    linea = linea & tipo & Sep
            'End If
            linea = linea & oDGV.Rows(i).Cells(0).Value & Sep
            'aaa
            If tipo = 2 Then
                If Trim(oDGV.Rows(i).Cells(3).Value) = "" Then
                    If Trim(oDGV.Rows(i).Cells(16).Value) <> "" And Trim(oDGV.Rows(i).Cells(16).Value) <> Trim(oDGV.Rows(i).Cells(2).Value) Then
                        sql = "SELECT * FROM carnet_manipulador WHERE dni='" & Trim(oDGV.Rows(i).Cells(16).Value) & "'"
                        If rsIne.Open(sql, ObjetoGlobal.cn) Then
                            linea = linea & Trim("" & rsIne!dni) & Sep
                            linea = linea & Trim("" & rsIne!ropo) & Sep
                            linea = linea & Trim("" & rsIne!nombre) & Sep
                            If Trim("" & rsIne!ropo) = "" Then
                                MyMsgBox("La persona autorizada con nif " & Trim(oDGV.Rows(i).Cells(16).Value) & " no tiene número de ropo.")
                                hayerrores = True
                            End If
                        Else
                            linea = linea & Trim(oDGV.Rows(i).Cells(2).Value) & Sep
                            linea = linea & Trim(oDGV.Rows(i).Cells(3).Value) & Sep
                            linea = linea & Trim(oDGV.Rows(i).Cells(4).Value) & Sep
                            MyMsgBox("El cliente con nif " & Trim(oDGV.Rows(i).Cells(2).Value) & " no tiene número de ropo ni persona autorizada localizable")
                            hayerrores = True
                        End If
                        rsIne.Close()
                    Else
                        linea = linea & Trim(oDGV.Rows(i).Cells(2).Value) & Sep
                        linea = linea & Trim(oDGV.Rows(i).Cells(3).Value) & Sep
                        linea = linea & Trim(oDGV.Rows(i).Cells(4).Value) & Sep
                        MyMsgBox("El cliente con nif " & Trim(oDGV.Rows(i).Cells(2).Value) & " no tiene número de ropo ni se indica persona autorizada")
                        hayerrores = True
                    End If
                Else
                    linea = linea & Trim(oDGV.Rows(i).Cells(2).Value) & Sep
                    linea = linea & Trim(oDGV.Rows(i).Cells(3).Value) & Sep
                    linea = linea & Trim(oDGV.Rows(i).Cells(4).Value) & Sep
                End If
            Else
                linea = linea & Trim(oDGV.Rows(i).Cells(2).Value) & Sep
                linea = linea & Trim(oDGV.Rows(i).Cells(3).Value) & Sep
                linea = linea & Trim(oDGV.Rows(i).Cells(4).Value) & Sep
            End If

            email = ""
            telefono = ""
            fax = ""
            direccion = ""
            cp = ""
            pais = ""
            provincia = ""
            ine = ""
            movil = ""

            If CBool(oDGV.Rows(i).Cells(15).Value) Then
                codigo = Trim(oDGV.Rows(i).Cells(5).Value)
                If Trim(tipo) = "11" Or Trim(tipo) = "12" Then
                    ' Proveedores
                    sql = "SELECT * FROM direccion_prov WHERE codigo_proveedor =" & codigo & " AND contador = 1"
                    If rs.Open(sql, ObjetoGlobal.cn) Then
                        email = "" & rs!correo_electronico
                        telefono = "" & rs!telefono
                        fax = "" & rs!fax
                        direccion = "" & Replace(rs!domicilio, ";", ",")
                        cp = "" & rs!codigo_postal
                        provincia = "" & rs!provincia
                        pais = "ES"
                        If Trim(email) = "" And Trim(telefono) = "" And Trim(fax) = "" Then
                            MyMsgBox("El proveedor " & codigo & " debe de tener algún campo de los siguientes: email o teléfono o fax ")
                            hayerrores = True
                        End If
                        If Trim(direccion) = "" Or Trim(cp) = "" Or Trim(provincia) = "" Then
                            MyMsgBox("El proveedor " & codigo & " debe de tener la dirección completa ")
                            hayerrores = True
                        End If
                        If Trim(cp) <> "" Then
                            sql = "SELECT * FROM cod_postales_coop where codigo_postal=" & cp
                            If rsIne.Open(sql, ObjetoGlobal.cn) Then
                                ine = rsIne!codigo_ine
                            Else
                                MyMsgBox("Error código postal " & cp & " del cliente " & codigo)
                                hayerrores = True
                            End If
                            rsIne.Close()
                        Else
                            MyMsgBox("Error en código postal del cliente " & codigo & " es un dato requerido")
                            hayerrores = True
                        End If
                    Else
                        MyMsgBox("No encuentro la dirección del proveedor " & codigo)
                        hayerrores = True
                    End If
                    rs.Close()

                ElseIf Trim(tipo) = "2" Then
                    ' Clientes
                    ' Proveedores
                    sql = "SELECT c.*, cp.codigo_ine FROM direccion_cliente c left join cod_postales_coop cp ON cp.codigo_postal = c.codigo_postal WHERE codigo_cliente =" & codigo & " AND contador = 1 "
                    If rs.Open(sql, ObjetoGlobal.cn) Then
                        email = "" & rs!correo_electronico
                        telefono = "" & rs!telefono
                        fax = "" & rs!fax
                        direccion = "" & Replace(rs!domicilio, ";", ",")
                        cp = "" & rs!codigo_postal
                        provincia = "" & rs!provincia
                        pais = "ES"
                        If Trim(email) = "" And Trim(telefono) = "" And Trim(fax) = "" Then
                            MyMsgBox("El cliente " & codigo & " debe de tener algún campo de los siguientes: email o teléfono o fax ")
                            hayerrores = True
                        End If
                        If Trim(direccion) = "" Or Trim(cp) = "" Or Trim(provincia) = "" Then
                            MyMsgBox("El cliente " & codigo & "  debe de tener la dirección completa ")
                            hayerrores = True
                        End If
                        If Trim(cp) <> "" Then
                            sql = "SELECT * FROM cod_postales_coop where codigo_postal=" & cp
                            If rsIne.Open(sql, ObjetoGlobal.cn) Then
                                ine = rsIne!codigo_ine
                            Else
                                MyMsgBox("Error código postal " & cp & " del cliente " & codigo)
                                hayerrores = True
                            End If
                            rsIne.Close()
                        Else
                            MyMsgBox("Error en código postal del cliente " & codigo & " es un dato requerido")
                            hayerrores = True
                        End If
                    Else
                        MyMsgBox("No encuentro la dirección del cliente " & codigo)
                        hayerrores = True
                    End If
                    rs.Close()

                ElseIf Trim(tipo) = "7" Then
                    ' De cooperativa SS a Cooperativa ST

                ElseIf Trim(tipo) = "8" Then
                    ' Socios
                    sql = "SELECT * FROM socios_coop WHERE codigo_socio =" & codigo
                    If rs.Open(sql, ObjetoGlobal.cn) Then
                        email = "" & rs!e_mail
                        telefono = IIf(Trim("" & rs!telefono) <> "", Trim("" & rs!telefono), Trim("" & rs!movil_socio))
                        movil = "" & rs!movil_socio
                        fax = "" & rs!fax
                        direccion = Trim("" & rs!calle_plaza) & " " & Trim("" & rs!domicilio_socio) & " " & Trim("" & rs!numero) & " " & Trim("" & rs!puerta)
                        direccion = Replace(direccion, ";", ",")

                        cp = Trim("" & rs!codigo_postal)
                        provincia = Trim("" & rs!provincia)
                        pais = "ES"

                        If Trim(cp) <> "" Then
                            sql = "SELECT * FROM cod_postales_coop where codigo_postal=" & cp
                            If rsIne.Open(sql, ObjetoGlobal.cn) Then
                                ine = rsIne!codigo_ine
                            Else
                                MyMsgBox("Error código postal " & cp & " del cliente " & codigo)
                                hayerrores = True
                            End If
                            rsIne.Close()
                        Else
                            MyMsgBox("Error en código postal del cliente " & codigo & " es un dato requerido")
                            hayerrores = True
                        End If

                        If Trim("" & email) = "" And Trim("" & telefono) = "" And Trim("" & fax) = "" Then
                            MyMsgBox("El socio " & codigo & " debe de tener algún campo de los siguientes: email o teléfono o fax ")
                            hayerrores = True
                        End If
                        If Trim(direccion) = "" Or Trim(cp) = "" Or Trim(provincia) = "" Then
                            MyMsgBox("El socio " & codigo & "  debe de tener la dirección completa ")
                            hayerrores = True
                        End If
                    End If
                    rs.Close()
                End If
            End If

            If Trim(oDGV.Rows(i).Cells(11).Value) = "" And Trim(oDGV.Rows(i).Cells(12).Value) = "" And Trim(oDGV.Rows(i).Cells(13).Value) = "" Then
                MyMsgBox("El producto de " & Trim(oDGV.Rows(i).Cells(4).Value) & " con registro " & Trim(oDGV.Rows(i).Cells(6).Value) & " nombre " & Trim(oDGV.Rows(i).Cells(7).Value) & " y lote " & Trim(oDGV.Rows(i).Cells(8).Value) & " no tiene ningún tipo de unidad ")
                hayerrores = True
            End If

            linea = linea & Trim(email) & Sep & Trim(telefono) & Sep & Trim(fax) & Sep & Trim(direccion) & Sep & Trim(cp) & Sep & Trim(pais) & Sep & (provincia) & Sep & (ine) & Sep
            linea = linea & Sep & Sep & Sep & Sep & Sep
            oFsw.WriteLine(linea)
            linea = ""
            linea = linea & "2" & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(6).Value) & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(7).Value) & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(8).Value) & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(11).Value) & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(9).Value) & Sep
            'linea = linea & oDGV.Rows(i).Cells(19).Value & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(12).Value) & Sep
            linea = linea & Trim(oDGV.Rows(i).Cells(13).Value) & Sep
            If Trim(tipo) <> "8" Then
                linea = linea & Sep & Trim(oDGV.Rows(i).Cells(18).Value) & Sep
            Else
                linea = linea & Sep & Trim(oDGV.Rows(i).Cells(18).Value) & Sep & Trim(oDGV.Rows(i).Cells(17).Value)
            End If

            If Trim(linea) <> "" Then
                linea = linea '& vbCrLf
                oFsw.WriteLine(linea)
            End If
        Next
        Return hayerrores
    End Function

    Private Sub Exportar_Click(sender As Object, e As EventArgs) Handles Exportar.Click
        Dim archivo As String

        archivo = FicheroaExportar()
        If Trim(archivo) <> "" Then
            If Not escribir(archivo) Then
                MsgBox("Fichero generado correctamente")
            Else
                MsgBox("Fichero generado con errores")
            End If

            'Catch ex As Exception
            '    MsgBox("Error escribiendo el archivo")
            'End Try
        End If

    End Sub

    Private Function FicheroaExportar()
        SaveFileDialog1.Filter = "csv Files (*.csv*)|*.csv"

        SaveFileDialog1.Title = "Exportar archivo RETO"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            'If SaveFileDialog1.CheckPathExists And SaveFileDialog1.CheckFileExists Then
            Return SaveFileDialog1.FileName
            'Else
            'Return ""
            'End If
        End If

    End Function

    Private Function MyMsgBox(cArg)
        Dim i As Integer
        Dim YaEsta As Boolean = False

        For i = 0 To UBound(aErrores) '- 1
            If Trim(aErrores(i)) = Trim(cArg) Then
                YaEsta = True
            End If
        Next
        If Not YaEsta Then
            ReDim Preserve aErrores(UBound(aErrores) + 1)
            aErrores(UBound(aErrores)) = cArg
            MsgErrores = MsgErrores + (cArg + vbCrLf)
        End If

    End Function

    Private Sub VerErrores_Click(sender As Object, e As EventArgs) Handles VerErrores.Click
        Dim Err As FrmErrores = New FrmErrores(MsgErrores)
        Err.ShowDialog()
        'VerErrores.Enabled = False
    End Sub


    Private Sub borrar_Click(sender As Object, e As EventArgs) Handles borrar.Click
        Dim DGV As DataGridView

        If TabControl1.SelectedTab.Text = TbCompras.Text Then
            DGV = DGVCompras
        ElseIf TabControl1.SelectedTab.Text = TbVentas.Text Then
            DGV = DGVVentas
        ElseIf TabControl1.SelectedTab.Text = TbAdquiciones.Text Then
            DGV = DGVAdquisiciones
        ElseIf TabControl1.SelectedTab.Text = TbAplicaciones.Text Then
            DGV = DGVAplicaciones
        End If

        Try
            If DGV.SelectedRows Is Nothing Then Exit Sub

            Dim selectedRowCount As Integer = DGV.Rows.GetRowCount(DataGridViewElementStates.Selected)
            For I As Integer = 0 To selectedRowCount - 1
                DGV.Rows.Remove(DGV.SelectedRows(0))
            Next
        Catch ex As Exception
            MessageBox.Show("No hay elementos para eliminar....!", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Function nombreyapellidos(cCad) As String()
        Dim no As Long
        Dim cad1 As String
        Dim cad2 As String
        Dim aCad() As String
        Dim aRet() As String

        cCad = Trim(cCad)

        ' Si tiene una coma
        If InStr(cCad, ",") > 0 Then
            ReDim aCad(2)
            no = InStr(cCad, ",")
            aCad(1) = Trim(Strings.Left(cCad, no - 1))
            aCad(2) = Trim(Strings.Mid(cCad, no + 1))
        Else
            no = CountCharacter(cCad, " ")
            aRet = Split(cCad, " ")
            Select Case no
                Case 1
                    ReDim aCad(2)
                    aCad(1) = aRet(1)
                    aCad(2) = aRet(2)
                Case 2
                    ReDim aCad(3)
                    aCad(1) = aRet(1)
                    aCad(2) = aRet(2)
                    aCad(3) = aRet(3)
                Case 3
                    ReDim aCad(3)
                    aCad(1) = aRet(1) & " " & aRet(2)
                    aCad(2) = aRet(3)
                    aCad(3) = aRet(4)
                Case 4
                    ReDim aCad(3)
                    aCad(1) = aRet(1) & " " & aRet(2)
                    aCad(2) = aRet(3) & " " & aRet(4)
                    aCad(3) = aRet(5)
            End Select
        End If
        Return aCad

    End Function

    Public Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In value
            If c = ch Then
                cnt += 1
            End If
        Next
        Return cnt
    End Function

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property
    Private Function LimpiarNombreComercial(nombre, reg, dc)
        Dim ret As String
        Dim no As Integer
        Dim rs As New cnRecordset.CnRecordset

        no = Strings.InStr(Strings.UCase(nombre), "REG")
        If no <> 0 Then
            ret = Strings.Left(nombre, no - 1)
        Else
            ret = nombre
        End If
        no = Strings.InStr(Strings.UCase(ret), "1 L")
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "1L")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "2 L")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "2L")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "1K")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "1 K")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "2K")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "2 K")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "5 L")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "5L")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "5 K")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "5K")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "100 C")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "500 C")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "100 G")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "500 G")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "200 C")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "200C")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "200 G")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "200G")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "20L")
        End If
        If no = 0 Then
            no = Strings.InStr(Strings.UCase(ret), "20 L")
        End If
        If no <> 0 Then
            ret = Strings.Left(ret, no - 1)
        End If

        If Strings.Trim(dc) <> "S" Then
            If rs.Open("SELECT * FROM productos_aut WHERE registro ='" & Strings.Trim(reg) & "' and orden = 1", ObjetoGlobal.cn) Then
                If Not rs.EOF() Then
                    If Not ret.Trim = Strings.Trim(rs!descripcion) Then
                        ret = Strings.Trim(rs!descripcion)
                    End If
                End If
            End If
        End If

        'ret = Replace(Strings.UCase(ret), "1 LTR", "")
        'ret = Replace(Strings.UCase(ret), "1 LTS", "")
        'ret = Replace(Strings.UCase(ret), "1 LTS", "")
        'ret = Replace(Strings.UCase(ret), "1 L", "")
        'ret = Replace(Strings.UCase(ret), "2 L", "")
        'ret = Replace(Strings.UCase(ret), "1L", "")
        'ret = Replace(Strings.UCase(ret), "2L", "")
        'ret = Replace(Strings.UCase(ret), "5 L", "")
        'ret = Replace(Strings.UCase(ret), "5L", "")
        Return ret
    End Function

    Private Sub SurroundingSub()
        Dim i_StandardHeight = 768 'Developer Desktop Height Where the Form is Designed
        Dim i_StandardWidth = 1024 'Developer Desktop Width Where the Form is Designed
        Dim i_PresentHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Dim i_PresentWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim f_HeightRatio As Single = New Single()
        Dim f_WidthRatio As Single = New Single()
        f_HeightRatio = i_PresentHeight / CSng(i_StandardHeight)
        f_WidthRatio = i_PresentWidth / CSng(i_StandardWidth)
        For Each c As Control In Me.Controls
            If c.[GetType]().ToString() Is "System.Windows.Forms.Button" Then
                Dim obtn As Button = CType(c, Button)
                obtn.TextAlign = ContentAlignment.MiddleCenter
            End If
            If c.HasChildren Then
                For Each cChildren As Control In c.Controls
                    cChildren.SetBounds(Convert.ToInt32(cChildren.Bounds.X * f_WidthRatio), Convert.ToInt32(cChildren.Bounds.Y * f_WidthRatio), Convert.ToInt32(cChildren.Bounds.Width * f_WidthRatio), Convert.ToInt32(cChildren.Bounds.Height * f_HeightRatio))
                    'cChildren.Font = new Font(cChildren.Font.FontFamily, cChildren.Font.Size * f_HeightRatio, cChildren.Font.Style, cChildren.Font.Unit, ((byte)(0)));
                Next
                ' c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, ((byte)(0)));
                c.SetBounds(Convert.ToInt32(c.Bounds.X * f_WidthRatio), Convert.ToInt32(c.Bounds.Y * f_WidthRatio), Convert.ToInt32(c.Bounds.Width * f_WidthRatio), Convert.ToInt32(c.Bounds.Height * f_HeightRatio))
            Else
                c.SetBounds(Convert.ToInt32(c.Bounds.X * f_WidthRatio), Convert.ToInt32(c.Bounds.Y * f_WidthRatio), Convert.ToInt32(c.Bounds.Width * f_WidthRatio), Convert.ToInt32(c.Bounds.Height * f_HeightRatio))
                ' c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, ((byte)(0)));
            End If
        Next
        Me.Height = Convert.ToInt32(i_StandardHeight * f_HeightRatio)
        Me.Width = Convert.ToInt32(i_StandardWidth * f_WidthRatio)
    End Sub

    Private Sub reto_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' SurroundingSub()
    End Sub
End Class

