Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections
Imports System.Reflection
Imports System.ComponentModel

Public Class FrmClasificacionfruta
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public NumeroCalidades As Integer
    Public BuenosMalos(2) As Double
    Public Clasificacion(12) As Double

    Private Sub Cargardatos_Click(sender As Object, e As EventArgs) Handles Cargardatos.Click
        CabeceraVolcados()
        MostrarVolcados()
    End Sub

    Private Sub CabeceraVolcados()

        DGClasificacion().Columns().Clear()
        DGSumas.Columns().Clear()
        DGDefinitivos.Columns().Clear()

        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "albaran"
        column1.HeaderText = "Albarán"
        column1.Width = 50
        DGClasificacion().Columns.Add(column1)

        Dim column2 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column2.Name = "bulto"
        column2.HeaderText = "Bulto"
        column2.Width = 40
        column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "variedad"
        column3.HeaderText = "Variedad"
        column3.Width = 0
        DGClasificacion().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "kbulto"
        column4.HeaderText = "Kg bulto"
        column4.Width = 45
        column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "cajas"
        column5.HeaderText = "Cajas"
        column5.Width = 40
        DGClasificacion().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "lote"
        column6.HeaderText = "Lote"
        column6.Width = 45
        DGClasificacion().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "Fecha"
        column7.HeaderText = "Fecha volcado"
        column7.Width = 75
        DGClasificacion().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "hora"
        column8.HeaderText = "Hora"
        column8.Width = 60
        DGClasificacion().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "calibre"
        column9.HeaderText = "Kgs.calibre"
        column9.Width = 50
        column9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "cbarras"
        column10.HeaderText = "Cod. barras"
        column10.Width = 50
        DGClasificacion().Columns.Add(column10)


        Dim column5S As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5S.Name = "kbulto"
        column5S.HeaderText = "Kgs.bulto"
        column5S.Width = 75
        column5S.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGSumas().Columns.Add(column5S)

        Dim column5D As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5D.Name = "kbulto"
        column5D.HeaderText = "Kgs.bulto"
        column5D.Width = 75
        column5D.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDefinitivos().Columns.Add(column5D)

        Dim column4S As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4S.Name = "cajas"
        column4S.HeaderText = "Cajas"
        column4S.Width = 50

        DGSumas().Columns.Add(column4S)

        Dim column4D As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4D.Name = "cajas"
        column4D.HeaderText = "Cajas"
        column4D.Width = 50
        DGDefinitivos().Columns.Add(column4D)

        Dim column9S As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9S.Name = "calibre"
        column9S.HeaderText = "Kgs.calibre"
        column9S.Width = 75
        column9S.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGSumas().Columns.Add(column9S)

        Dim column9D As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9D.Name = "calibre"
        column9D.HeaderText = "Kgs.calibre"
        column9D.Width = 75
        column9D.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDefinitivos().Columns.Add(column9D)


    End Sub

    Private Function RellenaRestoCabeceras(var) As String
        Dim Sql As String, SQL2 As String, Calidades(12) As String, i As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim CuantasCalidades As Integer, Cadena As String
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
            column.Width = 50
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGClasificacion().Columns.Add(column)
        Next
        For i = 1 To CuantasCalidades
            column = New DataGridViewTextBoxColumn()
            column.Name = Calidades(i)
            column.HeaderText = Calidades(i)
            column.Width = 75
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGSumas().Columns.Add(column)
        Next
        For i = 1 To CuantasCalidades
            column = New DataGridViewTextBoxColumn()
            column.Name = Calidades(i)
            column.HeaderText = Calidades(i)
            column.Width = 75
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGDefinitivos().Columns.Add(column)
        Next

        For i = CuantasCalidades + 1 To 12
            column = New DataGridViewTextBoxColumn()
            column.Name = ""
            column.HeaderText = ""
            column.Width = 0
            DGClasificacion().Columns.Add(column)
        Next
        For i = CuantasCalidades + 1 To 12
            column = New DataGridViewTextBoxColumn()
            column.Name = ""
            column.HeaderText = ""
            column.Width = 0
            DGSumas().Columns.Add(column)
        Next
        For i = CuantasCalidades + 1 To 12
            column = New DataGridViewTextBoxColumn()
            column.Name = ""
            column.HeaderText = ""
            column.Width = 0
            DGDefinitivos().Columns.Add(column)
        Next

        Return CuantasCalidades

        'column = New DataGridViewTextBoxColumn()
        'column.Name = "eurep_cs"
        'column.HeaderText = "G.GAP"
        'column.Width = 100
        'DGClasificacion().Columns.Add(column)

        'column = New DataGridViewTextBoxColumn()
        'column.Name = "grasp"
        'column.HeaderText = "GRASP"
        'column.Width = 100
        'DGClasificacion().Columns.Add(column)

    End Function

    Private Sub MostrarVolcados()
        Dim Sql As String, Calidades(12) As String, i As Integer
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rsp As New cnRecordset.CnRecordset
        Dim Rspr As New cnRecordset.CnRecordset
        Dim RsLin As New cnRecordset.CnRecordset
        Dim RsCart As New cnRecordset.CnRecordset
        Dim PesoSe As Double
        Dim PesoNC As Double
        Dim TotalNC As Double
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim cuantascalidades As Integer
        Dim PesoComercial As Double
        Dim PesoTeorico As Double
        Dim coef As Double

        Sql = "SELECT entradas_albaranes.*, socios_coop.nombre_socio, socios_coop.apellidos_socio FROM entradas_albaranes LEFT JOIN socios_coop ON entradas_albaranes.codigo_socio = socios_coop.codigo_socio WHERE entradas_albaranes.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND entradas_albaranes.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' "
        Sql = Sql + "AND entradas_albaranes.serie_albaran='" & TxtSerie.Text & "' AND entradas_albaranes.numero_albaran =" & TxtNumero.Text
        If Rs.Open(Sql, ObjetoGlobal.cn) Then

            If Rs.EOF Then
                MsgBox("Albarán de entrada no encontrado")
                Return
            End If

            lblFechaEntrada.Text = Rs!fecha_entrada
            lblCodigo_campo.Text = If(Not IsDBNull(Rs!codigo_campo), Rs!codigo_campo, Rs!campo_terceros)
            lblVariedad.Text = Rs!Codigo_variedad
            If IsDBNull(Rs!codigo_socio) Then
                If Rspr.Open("SELECT * FROM proveedores_coop WHERE codigo_proveedor = " & Rs!codigo_proveedor, ObjetoGlobal.cn) Then
                    LblNombreSocio.Text = "" & Rs!codigo_proveedor & " - " + Trim(Rspr!razon_social)
                End If
                Rspr.Close()
            ElseIf Not IsDBNull(Rs!codigo_socio) Then
                LblNombreSocio.Text = "" & Rs!codigo_socio & " - " + Trim(Trim(Rs!nombre_socio) & " " & Trim(Rs!apellidos_socio))
            End If

            cuantascalidades = RellenaRestoCabeceras(Rs!codigo_variedad)
            NumeroCalidades = cuantascalidades

            Sql = "SELECT b.empresa, b.ejercicio, b.serie_albaran, b.numero_albaran, b.bulto, b.cajas FROM entradas_bultos b WHERE b.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND b.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' "
            Sql = Sql + " AND b.serie_albaran ='" & TxtSerie.Text & "' AND b.numero_albaran =" & TxtNumero.Text & " ORDER BY 1,2,3,4,5"
            If RsCart.Open(Sql, ObjetoGlobal.cn) Then
                While Not RsCart.EOF

                    Row = New DataGridViewRow

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RsCart!numero_albaran
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RsCart!bulto
                    Row.Cells.Add(Cell)


                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = Rs!codigo_variedad
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell

                    Sql = "select CAST(ISNULL(dbo.fn_peso_bulto('" & Trim(Rs!empresa) & "','" & Trim(Rs!ejercicio) & "','" & Trim(Rs!serie_albaran) & "', " & Rs!numero_albaran & ", " & RsCart!bulto & "),0) As Integer) As peso_bultos"
                    If Rsp.Open(Sql, ObjetoGlobal.cn) And Not Rsp.EOF Then
                        Cell.Value = Rsp!peso_bultos
                        PesoTeorico = Rsp!peso_bultos
                    Else
                        Cell.Value = 0
                        PesoTeorico = 0
                    End If
                    Row.Cells.Add(Cell)

                    Cell = New DataGridViewTextBoxCell
                    Cell.Value = RsCart!cajas
                    Row.Cells.Add(Cell)

                    PesoComercial = 0
                    Rsp.Close()

                    Sql = "SELECT * FROM entradas_lotes_test WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' "
                    Sql = Sql + " and serie_albaran='" & RsCart!serie_albaran & "' AND numero_albaran =" & TxtNumero.Text & " AND bulto =" & RsCart!bulto
                    If RsLin.Open(Sql, ObjetoGlobal.cn) Then
                        If Not RsLin.EOF Then
                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = RsLin!lote
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = RsLin!fecha_volcado
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = RsLin!hora_volcado
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = RsLin!kg_calibrados
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = ""
                            Row.Cells.Add(Cell)

                            For i = 1 To cuantascalidades - 2
                                Cell = New DataGridViewTextBoxCell
                                Cell.Value = IIf(IsDBNull(RsLin("Kg_" & Strings.Right(("00" & i), 2))), 0, RsLin("Kg_" & Strings.Right(("00" & i), 2)))
                                PesoComercial += IIf(IsDBNull(RsLin("Kg_" & Strings.Right(("00" & i), 2))), 0, RsLin("Kg_" & Strings.Right(("00" & i), 2)))
                                Row.Cells.Add(Cell)
                            Next

                            PesoSe = 0
                            PesoNC = 0
                            TotalNC = 0

                            For i = cuantascalidades - 1 To cuantascalidades
                                'Cell = New DataGridViewTextBoxCell
                                'Cell.Value = IIf(IsDBNull(RsLin("Kg_" & Strings.Right(("00" & i), 2))), 0, RsLin("Kg_" & Strings.Right(("00" & i), 2)))
                                If i = cuantascalidades Then
                                    PesoNC = IIf(IsDBNull(RsLin("Kg_" & Strings.Right(("00" & i), 2))), 0, RsLin("Kg_" & Strings.Right(("00" & i), 2)))
                                Else
                                    PesoSe = IIf(IsDBNull(RsLin("Kg_" & Strings.Right(("00" & i), 2))), 0, RsLin("Kg_" & Strings.Right(("00" & i), 2)))
                                End If
                                TotalNC = PesoSe + PesoNC
                                'PesoComercial += IIf(IsDBNull(RsLin("Kg_" & Strings.Right(("00" & i), 2))), 0, RsLin("Kg_" & Strings.Right(("00" & i), 2)))
                                'Row.Cells.Add(Cell)
                            Next
                            coef = ((PesoTeorico - PesoComercial) / TotalNC)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = Math.Round(coef * PesoSe, 0)
                            Row.Cells.Add(Cell)

                            Cell = New DataGridViewTextBoxCell
                            Cell.Value = Math.Round((PesoTeorico - PesoComercial) - Math.Round(coef * PesoSe, 0), 2)
                            Row.Cells.Add(Cell)
                        End If

                    End If
                    DGClasificacion().Rows.Add(Row)
                    RsLin.Close()
                    RsCart.MoveNext()
                End While
                RsCart.Close()
            End If
        End If
        SumasColumnas()


    End Sub


    Private Sub SumasColumnas()
        Dim Sumas(22) As Double
        Dim i As Integer
        Dim Row As DataGridViewRow
        Dim Row1 As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim totales As Double
        Dim dif As Double = 100

        For i = 0 To 21
            Sumas(i) = 0
        Next

        For Each fila As DataGridViewRow In DGClasificacion.Rows
            For i = 0 To 21
                If i = 3 Or i = 4 Or i = 8 Or (i >= 10 And i <= NumeroCalidades + 9) Then
                    If IsNumeric(fila.Cells(i).Value) Then
                        Sumas(i) += CDbl(fila.Cells(i).Value)
                    Else
                        Sumas(i) += 0
                    End If
                Else
                    Sumas(i) += 0
                End If
            Next
        Next

        If DGSumas.Rows.Count > 0 Then
            DGSumas.Rows.Remove(DGSumas.Rows(0))
        End If

        Row = New DataGridViewRow
        For i = 0 To 21
            If i = 3 Or i = 4 Or i = 8 Or (i >= 10 And i <= NumeroCalidades + 9) Then
                Cell = New DataGridViewTextBoxCell
                Cell.Value = Sumas(i)
                Row.Cells.Add(Cell)
            End If

        Next
        DGSumas().Rows.Add(Row)

        'For i = 3 To 14
        '    DGDefinitivos.Rows(0).Cells(i).Value = " "
        'Next

        'For i = 3 To 14
        '    totales += DGSumas.Rows(0).Cells(i).Value
        '    DGDefinitivos.Rows(0).Cells(i).Value = DGSumas.Rows(0).Cells(i).Value
        'Next

        For i = 3 To NumeroCalidades + 2
            If IsNumeric(DGSumas.Rows(0).Cells(i).Value) Then
                totales += CDbl(DGSumas.Rows(0).Cells(i).Value)
            End If

        Next

        If DGDefinitivos.Rows.Count > 0 Then
            DGDefinitivos.Rows.Remove(DGDefinitivos.Rows(0))
        End If

        Row1 = New DataGridViewRow
        For i = 0 To 21
            If i = 3 Or i = 4 Or i = 8 Or i >= 10 Then
                Cell = New DataGridViewTextBoxCell
                Cell.Value = 0 ' Sumas(i)
                Row1.Cells.Add(Cell)
            End If
        Next
        DGDefinitivos.Rows.Add(Row1)

        Adefinitiva()

        'For i = 3 To NumeroCalidades + 2
        '    DGDefinitivos.Rows(0).Cells(i).Value = Math.Round((CDbl("0" & DGSumas.Rows(0).Cells(i).Value) * 100) / totales, 2)
        '    dif -= Math.Round((CDbl("0" & DGSumas.Rows(0).Cells(i).Value) * 100) * totales, 2)
        'Next
        'DGDefinitivos.Rows(0).Cells(i - 1).Value -= dif

        'totalizarporc()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub Moveradefinitiva_Click(sender As Object, e As EventArgs) Handles Moveradefinitiva.Click

        Adefinitiva()

    End Sub
    Private Sub Adefinitiva()
        Dim i As Integer
        Dim sumas As Double
        Dim dif As Double = 100

        sumas = 0
        For i = 3 To NumeroCalidades + 2
            DGDefinitivos.Rows(0).Cells(i).Value = " "
        Next

        For i = 3 To NumeroCalidades + 2
            sumas += DGSumas.Rows(0).Cells(i).Value
        Next

        For i = 0 To 2
            If IsNumeric(DGSumas.Rows(0).Cells(i).Value) Then
                DGDefinitivos.Rows(0).Cells(i).Value = CDbl(DGSumas.Rows(0).Cells(i).Value)
            Else
                DGDefinitivos.Rows(0).Cells(i).Value = 0
            End If
        Next

        ReDim Clasificacion(NumeroCalidades + 2)

        For i = 3 To NumeroCalidades + 2
            If IsNumeric(DGSumas.Rows(0).Cells(i).Value) Then
                DGDefinitivos.Rows(0).Cells(i).Value = Math.Round((CDbl(DGSumas.Rows(0).Cells(i).Value) * 100) / sumas, 2)
                dif -= Math.Round((CDbl(DGSumas.Rows(0).Cells(i).Value) * 100) / sumas, 2)
                Clasificacion(i) = Math.Round((CDbl(DGSumas.Rows(0).Cells(i).Value) * 100) / sumas, 2)
            Else
                Clasificacion(i) = 0
            End If
        Next
        DGDefinitivos.Rows(0).Cells(i - 1).Value += dif
        Clasificacion(i - 1) += dif

        totalizarporc()
    End Sub
    Private Sub DGClasificacion_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGClasificacion.CellContentClick
        Dim oFrm As ClonarBulto = New ClonarBulto
        Dim Filaaclonar As Integer = e.RowIndex
        Dim FilaClonada As Integer = 0
        Dim fila As Integer = 0
        Dim columna As Integer = 0

        oFrm.ShowDialog()
        If oFrm.Resultado <> 0 Then
            ' Ahora clonaremos la línea
            For fila = 0 To DGClasificacion.Rows.Count
                If CInt(DGClasificacion.Rows(fila).Cells("bulto").Value) = oFrm.Resultado Then
                    FilaClonada = fila
                    Exit For
                End If
            Next
            If Filaaclonar <> FilaClonada Then
                For columna = 6 To 21
                    DGClasificacion.Rows(e.RowIndex).Cells(columna).Value = DGClasificacion.Rows(FilaClonada).Cells(columna).Value
                Next
                SumasColumnas()
            End If
        End If


    End Sub

    Private Sub BorrarSumas_Click(sender As Object, e As EventArgs) Handles BorrarSumas.Click
        For i = 0 To 14
            DGDefinitivos.Rows(0).Cells(i).Value = " "
        Next
    End Sub

    Private Sub DGDefinitivos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDefinitivos.CellContentClick

    End Sub

    Private Sub totalizarporc()
        Dim sumas As Double = 0
        Dim i As Integer

        lblTotalPorc.Text = 0
        BuenosMalos(1) = 0
        BuenosMalos(2) = 0

        For i = 3 To NumeroCalidades + 2
            If IsNumeric(DGDefinitivos.Rows(0).Cells(i).Value) Then
                sumas += CDbl(DGDefinitivos.Rows(0).Cells(i).Value)
                If i <= NumeroCalidades Then
                    BuenosMalos(1) += CDbl(DGDefinitivos.Rows(0).Cells(i).Value)
                Else
                    BuenosMalos(2) += CDbl(DGDefinitivos.Rows(0).Cells(i).Value)
                End If
            End If
        Next
        lblTotalPorc.Text = Math.Round(sumas, 2)

    End Sub


    Public Sub ExportarDatosExcel(ByVal titulo As String)
        Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
        m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlWait
        m_Excel.Visible = True
        Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook = m_Excel.Workbooks.Add
        Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet = objLibroExcel.Worksheets(1)
        Dim DataGridView1 As DataGridView
        Dim DGV() As DataGridView = {DGClasificacion, DGSumas, DGDefinitivos}

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
            Dim jj As Integer

            For jj = 0 To 2
                DataGridView1 = DGV(jj)

                strColumna = ""
                LetraIzq = ""
                cod_LetraIzq = Asc(primeraLetra) - 1
                Letra = primeraLetra
                cod_letra = Asc(primeraLetra) - 1

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
                Numero = UltimoNumero + 2
            Next

            'Dibujar el border exterior grueso  
            'Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            'objRango.Select()
            'objRango.Columns.AutoFit()
            'objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
        End With

        m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault
    End Sub

    Private Sub btExcel_Click(sender As Object, e As EventArgs) Handles btExcel.Click
        ExportarDatosExcel("Clasificaciones")
    End Sub

    Private Sub DGClasificacion_Validating(sender As Object, e As CancelEventArgs) Handles DGClasificacion.Validating
        'totalizarporc()
        'If e.ColumnIndex > NumeroCalidades - 2 Then

        'End If
    End Sub

    Private Sub DGDefinitivos_Validating(sender As Object, e As CancelEventArgs) Handles DGDefinitivos.Validating
        totalizarporc()
        'If e.ColumnIndex > NumeroCalidades - 2 Then

        'End If
    End Sub

    Private Sub DGDefinitivos_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGDefinitivos.CellValueChanged
        Dim grid As DataGridView = sender

        'If grid.Columns Then[e.ColumnIndex].Name 

        totalizarporc()
        'If e.ColumnIndex > NumeroCalidades - 2 Then

        'End If

        'BuenosMalos(1) += CDbl(DGDefinitivos.Rows(0).Cells(e.ColumnIndex).Value)


    End Sub

    Private Sub DGClasificacion_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGClasificacion.CellValueChanged
        SumasColumnas()
    End Sub

    Private Sub DGDefinitivos_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGDefinitivos.CellEndEdit
        Dim nuevosmalos As Double = 0
        Dim nuevosbuenos As Double = 0
        Dim dif As Double = 0
        Dim Coef As Double = 0
        Dim celda As Double = 0
        Dim i As Integer

        If e.ColumnIndex = NumeroCalidades + 1 Or e.ColumnIndex = NumeroCalidades + 2 Then
            nuevosmalos = CDbl(DGDefinitivos.Rows(0).Cells(NumeroCalidades + 1).Value) + CDbl(DGDefinitivos.Rows(0).Cells(NumeroCalidades + 2).Value)
            nuevosbuenos = 100 - nuevosmalos
            dif = nuevosbuenos
            Coef = nuevosbuenos / BuenosMalos(1)
            For i = 3 To NumeroCalidades
                DGDefinitivos.Rows(0).Cells(i).Value = Math.Round(DGDefinitivos.Rows(0).Cells(i).Value * Coef, 2)
                dif -= DGDefinitivos.Rows(0).Cells(i).Value
            Next
            DGDefinitivos.Rows(0).Cells(3).Value = Math.Round(DGDefinitivos.Rows(0).Cells(3).Value + dif, 2)
        End If
        totalizarporc()

    End Sub

    Private Sub Pasaraclasificacion_Click(sender As Object, e As EventArgs) Handles Pasaraclasificacion.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim Rsc As New cnRecordset.CnRecordset
        Dim sql As String
        Dim i As Integer


        sql = "SELECT * FROM entradas_albaranes WHERE empresa = '1' and ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND serie_albaran ='" & TxtSerie.Text & "' AND numero_albaran =" & TxtNumero.Text

        If Rs.Open(sql, ObjetoGlobal.cn, True) Then
            If Rs.RecordCount > 0 Then
                For i = 1 To NumeroCalidades
                    sql = "Select * from entradas_clasif WHERE empresa = '" & Rs!empresa & "' AND ejercicio = '" & Rs!ejercicio & "' and serie_albaran='" & Rs!serie_albaran & "' AND numero_albaran =" & Rs!numero_albaran & " AND codigo_calidad=" & i & " and tipo_clasificacion = 'PRO'"
                    If Rsc.Open(sql, ObjetoGlobal.cn, True) Then
                        If Rsc.EOF Then
                            Rsc.AddNew()
                            Rsc!empresa = Rs!empresa
                            Rsc!ejercicio = Rs!ejercicio
                            Rsc!serie_albaran = Rs!serie_albaran
                            Rsc!numero_albaran = Rs!numero_albaran
                            Rsc!tipo_clasificacion = "PRO"
                            Rsc!codigo_calidad = i
                            Rsc!codigo_variedad = Rs!codigo_variedad
                        End If
                    End If
                    Rsc!kg_muestra = DGDefinitivos.Rows(0).Cells(i + 2).Value
                    Rsc!kg_calidad = Math.Round(Rs!kg_a_liquidar * Rsc!kg_muestra / 100, 2)
                    Rsc.Update()
                    Rsc.Close()
                Next
                For i = 1 To NumeroCalidades
                    sql = "Select * from entradas_clasif WHERE empresa = '" & Rs!empresa & "' AND ejercicio = '" & Rs!ejercicio & "' and serie_albaran='" & Rs!serie_albaran & "' AND numero_albaran =" & Rs!numero_albaran & " AND codigo_calidad=" & i & " and tipo_clasificacion = 'LIQ'"
                    If Rsc.Open(sql, ObjetoGlobal.cn, True) Then
                        If Rsc.EOF Then
                            Rsc.AddNew()
                            Rsc!empresa = Rs!empresa
                            Rsc!ejercicio = Rs!ejercicio
                            Rsc!serie_albaran = Rs!serie_albaran
                            Rsc!numero_albaran = Rs!numero_albaran
                            Rsc!tipo_clasificacion = "LIQ"
                            Rsc!codigo_calidad = i
                            Rsc!codigo_variedad = Rs!codigo_variedad
                        End If
                    End If
                    Rsc!kg_muestra = DGDefinitivos.Rows(0).Cells(i + 2).Value
                    Rsc!kg_calidad = Math.Round(Rs!kg_a_liquidar * Rsc!kg_muestra / 100, 2)
                    Rsc.Update()
                    Rsc.Close()
                Next
                If cbBloquear.Checked Then
                    Rs!entregada_sn = "B"
                    Rs.Update()
                End If
                Rs.Close()
            End If
        End If


    End Sub
End Class