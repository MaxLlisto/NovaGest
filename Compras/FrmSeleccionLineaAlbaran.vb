Public Class FrmSeleccionLineaAlbaran

    Public ObjetoGlobal As Object
    Public codigo_proveedor As Integer
    Dim nTotal As Double
    Dim aBaseIva() As Double
    Dim aTipoIva() As Integer
    Dim aCuotaIva() As Double
    Dim seleccionSQL As String
    Public acepto As Boolean
    Public TotalSeleccion As Double


    Private Sub CargarAlbaran(SqlArg)
        Dim rsAlbaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, Sql As String, i As Long, nFila As Long, Fila As Long
        Dim Row As DataGridViewRow

        MSFLAlbaran().Columns().Clear()
        MSFLFactura().SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MSFLAlbaran().SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "numero_entrada"
        column1.HeaderText = "Núm. entrada"
        'column1.Width = 0
        column1.Visible = False
        MSFLAlbaran().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "linea_albaran"
        column2.HeaderText = "Línea albarán"
        'column2.Width = 0
        column1.Visible = False
        MSFLAlbaran().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "serie"
        column3.HeaderText = "Serie"
        column3.Width = 50
        MSFLAlbaran().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "numero"
        column4.HeaderText = "Número"
        column4.Width = 65
        MSFLAlbaran().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "articulo"
        column5.HeaderText = "Artículo"
        column5.Width = 75
        MSFLAlbaran().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "descripcion"
        column6.HeaderText = "Descripción"
        column6.Width = 200
        MSFLAlbaran().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "ref_proveedor"
        column7.HeaderText = "Ref. proveedor"
        column7.Width = 50
        MSFLAlbaran().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "unidades"
        column8.HeaderText = "Unidades"
        column8.Width = 65
        column8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column8.DefaultCellStyle.Format = "c"
        MSFLAlbaran().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "precio"
        column9.HeaderText = "Precio"
        column9.Width = 65
        column9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column8.DefaultCellStyle.Format = "c"
        MSFLAlbaran().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "importe"
        column10.HeaderText = "Importe"
        column10.Width = 65
        column10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        column8.DefaultCellStyle.Format = "c"
        MSFLAlbaran().Columns.Add(column10)

        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "tipo_iva"
        column11.HeaderText = "Ttipo iva"
        column11.Width = 65
        column11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        MSFLAlbaran().Columns.Add(column11)

        Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column12.Name = "línea_albaran"
        column12.HeaderText = "Línea albarán"
        column12.Width = 65
        MSFLAlbaran().Columns.Add(column12)

        MSFLFactura().Columns().Clear()
        Dim fcolumn1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn1.Name = "numero_entrada"
        fcolumn1.HeaderText = "Núm. entrada"
        fcolumn1.Width = 0
        fcolumn1.Visible = False
        MSFLFactura().Columns.Add(fcolumn1)

        Dim fcolumn2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn2.Name = "linea_albaran"
        fcolumn2.HeaderText = "Línea albarán"
        fcolumn2.Width = 0
        fcolumn2.Visible = False
        MSFLFactura().Columns.Add(fcolumn2)

        Dim fcolumn3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn3.Name = "serie"
        fcolumn3.HeaderText = "Serie"
        fcolumn3.Width = 50
        MSFLFactura().Columns.Add(fcolumn3)

        Dim fcolumn4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn4.Name = "numero"
        fcolumn4.HeaderText = "Número"
        fcolumn4.Width = 65
        MSFLFactura().Columns.Add(fcolumn4)

        Dim fcolumn5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn5.Name = "articulo"
        fcolumn5.HeaderText = "Artículo"
        fcolumn5.Width = 75
        MSFLFactura().Columns.Add(fcolumn5)

        Dim fcolumn6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn6.Name = "descripcion"
        fcolumn6.HeaderText = "Descripción"
        fcolumn6.Width = 200
        MSFLFactura().Columns.Add(fcolumn6)

        Dim fcolumn7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn7.Name = "ref_proveedor"
        fcolumn7.HeaderText = "Ref. proveedor"
        fcolumn7.Width = 65
        MSFLFactura().Columns.Add(fcolumn7)

        Dim fcolumn8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn8.Name = "unidades"
        fcolumn8.HeaderText = "Unidades"
        fcolumn8.Width = 65
        fcolumn8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        fcolumn8.DefaultCellStyle.Format = "c"
        MSFLFactura().Columns.Add(fcolumn8)

        Dim fcolumn9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn9.Name = "precio"
        fcolumn9.HeaderText = "Precio"
        fcolumn9.Width = 65
        fcolumn9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        fcolumn9.DefaultCellStyle.Format = "c"
        MSFLFactura().Columns.Add(fcolumn9)

        Dim fcolumn10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn10.Name = "importe"
        fcolumn10.HeaderText = "Importe"
        fcolumn10.Width = 65
        fcolumn10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        fcolumn10.DefaultCellStyle.Format = "c"

        MSFLFactura().Columns.Add(fcolumn10)

        Dim fcolumn11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn11.Name = "tipo_iva"
        fcolumn11.HeaderText = "Ttipo iva"
        fcolumn11.Width = 50
        fcolumn11.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        MSFLFactura().Columns.Add(fcolumn11)

        Dim fcolumn12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        fcolumn12.Name = "línea_albaran"
        fcolumn12.HeaderText = "Línea albarán"
        fcolumn12.Width = 50
        fcolumn12.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        MSFLFactura().Columns.Add(fcolumn12)

        If codigo_proveedor = 0 Then
            MsgBox("No se indicó para que proveedor")
            Return
        End If
        If SqlArg = "" Then
            Sql = "SELECT albaran_com_c.*, albaran_com_l.linea_albaran, albaran_com_l.articulo,  albaran_com_l.descripcion, albaran_com_l.referencia_prov, albaran_com_l.unidades, albaran_com_l.precio, albaran_com_l.importe, albaran_com_l.tipo_iva FROM albaran_com_c, albaran_com_l WHERE albaran_com_l.empresa=albaran_com_c.empresa and albaran_com_l.numero_entrada=albaran_com_c.numero_entrada and albaran_com_c.EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' and albaran_com_l.situacion = 'N' AND albaran_com_c.codigo_proveedor =" & codigo_proveedor & " order by serie_albaran_p, numero_albaran_p, linea_albaran"
        Else
            Sql = "SELECT albaran_com_c.*, albaran_com_l.linea_albaran, albaran_com_l.articulo,  albaran_com_l.descripcion, albaran_com_l.referencia_prov, albaran_com_l.unidades, albaran_com_l.precio, albaran_com_l.importe, albaran_com_l.tipo_iva FROM albaran_com_c, albaran_com_l WHERE albaran_com_l.empresa=albaran_com_c.empresa and albaran_com_l.numero_entrada=albaran_com_c.numero_entrada and albaran_com_c.EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' and albaran_com_l.situacion = 'N' AND " & SqlArg & " order by serie_albaran_p, numero_albaran_p, linea_albaran"
        End If
        rsAlbaran.Open(Sql, ObjetoGlobal.cn)

        If rsAlbaran.RecordCount = 0 Then
            MsgBox("No hay albaranes pendientes")
            Return
        End If
        nFila = 0
        Dim Cell = New DataGridViewTextBoxCell
        While Not rsAlbaran.EOF
            Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = rsAlbaran!numero_entrada
                Row.Cells.Add(Cell)

            'Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "M"
                Row.Cells.Add(Cell)

            '   Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & rsAlbaran!serie_albaran_p)
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & rsAlbaran!numero_albaran_p)
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim("" & rsAlbaran!articulo)
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & rsAlbaran!Descripcion
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & rsAlbaran!referencia_prov
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & rsAlbaran!unidades
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & rsAlbaran!Precio
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & rsAlbaran!Importe
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & rsAlbaran!tipo_iva
                Row.Cells.Add(Cell)

            '    Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "" & rsAlbaran!Linea_albaran
            Row.Cells.Add(Cell)

            MSFLAlbaran().Rows.Add(Row)
                rsAlbaran.MoveNext()
            End While
            rsAlbaran.Close()

    End Sub

    Private Function SumaIVAS(nBase As Double, nTipo As Integer) As Boolean
        Dim i As Integer
        Dim bSumado As Boolean

        For i = 0 To 3
            If aTipoIva(i) = nTipo Then
                aBaseIva(i) = aBaseIva(i) + nBase
                aCuotaIva(i) = Math.Round((aBaseIva(i) * (aTipoIva(i))) / 100, 2)
                bSumado = True
                Exit For
            End If
        Next
        If Not bSumado Then
            For i = 0 To 3
                If aTipoIva(i) = -1 Then
                    aTipoIva(i) = nTipo
                    aBaseIva(i) = nBase
                    aCuotaIva(i) = Math.Round((aBaseIva(i) * (aTipoIva(i))) / 100, 2)
                    Exit For
                End If
            Next
        End If
        PonTotales()
        SumaIVAS = True

    End Function

    Private Function RestaIVAS(nBase As Double, nTipo As Integer) As Boolean
        Dim i As Integer
        Dim bSumado As Boolean

        For i = 0 To 3
            If aTipoIva(i) = nTipo Then
                aBaseIva(i) = aBaseIva(i) - nBase
                aCuotaIva(i) = Math.Round((aBaseIva(i) * (aTipoIva(i))) / 100, 2)
                bSumado = True
                Exit For
            End If
        Next
        RestaIVAS = True
        PonTotales()

    End Function

    Private Sub PonTotales()
        Dim i As Integer

        If aTipoIva(0) <> -1 Then
            Base1.Text = Format(aBaseIva(0), "###,###,##0.00")
            'Base1.TextAlign = ContentAlignment.MiddleRight
            tipo1.Text = Format(aTipoIva(0), "###,###,##0.00")
            'tipo1.TextAlign = ContentAlignment.MiddleRight
            cuota1.Text = Format(aCuotaIva(0), "###,###,##0.00")
            'cuota1.TextAlign = ContentAlignment.MiddleRight
        End If
        If aTipoIva(1) <> -1 Then
            Base2.Text = Format(aBaseIva(1), "###,###,##0.00")
            'Base2.TextAlign = ContentAlignment.MiddleRight
            tipo2.Text = Format(aTipoIva(1), "###,###,##0.00")
            'tipo2.TextAlign = ContentAlignment.MiddleRight
            cuota2.Text = Format(aCuotaIva(1), "###,###,##0.00")
            'cuota2.TextAlign = ContentAlignment.MiddleRight
        End If
        If aTipoIva(2) <> -1 Then
            base3.Text = Format(aBaseIva(2), "###,###,##0.00")
            'base3.TextAlign = ContentAlignment.MiddleRight
            tipo3.Text = Format(aTipoIva(2), "###,###,##0.00")
            'tipo3.TextAlign = ContentAlignment.MiddleRight
            cuota3.Text = Format(aCuotaIva(2), "###,###,##0.00")
            'cuota3.TextAlign = ContentAlignment.MiddleRight
        End If
        If aTipoIva(3) <> -1 Then
            Base4.Text = Format(aBaseIva(3), "###,###,##0.00")
            'Base4.TextAlign = ContentAlignment.MiddleRight
            Tipo4.Text = Format(aTipoIva(3), "###,###,##0.00")
            'Tipo4.TextAlign = ContentAlignment.MiddleRight
            Cuota4.Text = Format(aCuotaIva(3), "###,###,##0.00")
            'Cuota4.TextAlign = ContentAlignment.MiddleRight
        End If
        nTotal = 0
        For i = 0 To 3
            If aTipoIva(i) <> -1 Then
                nTotal = nTotal + aBaseIva(i) + aCuotaIva(i)
            End If
        Next
        TotalSeleccion = nTotal

        Total.Text = Format(nTotal, "###,###,###.##")

    End Sub

    Private Sub CargarAlbaranes_Click(sender As Object, e As EventArgs) Handles CargarAlbaranes.Click
        Dim RSGrabarProceso As New cnRecordset.CnRecordset
        Dim dt As DataTable = New DataTable("Tabla")
        Dim dr As DataRow
        Dim sql As String

        If Trim(cbProveedor.Text) <> "" Then
            If IsNumeric(cbProveedor.Text.Trim) Then
                sql = "SELECT codigo_proveedor, razon_social From proveedores_coop WHERE (razon_social Like '%" & UCase(Trim(cbProveedor.Text.Trim)) & "%' or codigo_proveedor = " & cbProveedor.Text.Trim & ") ORDER BY codigo_proveedor"
            Else
                sql = "SELECT codigo_proveedor, razon_social From proveedores_coop WHERE (razon_social Like '%" & UCase(Trim(cbProveedor.Text.Trim)) & "%') ORDER BY codigo_proveedor"
            End If
            If RSGrabarProceso.Open(sql, ObjetoGlobal.cn) Then
                dt.Columns.Add("codigo")
                dt.Columns.Add("razon_social")
                If RSGrabarProceso.RecordCount > 1 Then
                    For i = 1 To RSGrabarProceso.RecordCount
                        RSGrabarProceso.AbsolutePosition = i
                        dr = dt.NewRow()
                        dr("Codigo") = RSGrabarProceso!codigo_proveedor
                        dr("razon_social") = RSGrabarProceso!razon_social
                        dt.Rows.Add(dr)
                    Next
                    cbProveedor.DataSource = dt
                    cbProveedor.ValueMember = "codigo"
                    cbProveedor.DisplayMember = "razon_social"
                End If
            Else
                cbProveedor.ValueMember = RSGrabarProceso!codigo_proveedor
                cbProveedor.DisplayMember = RSGrabarProceso!razon_social
                CargarAlbaran("")
            End If
        End If

    End Sub

    Private Sub cancelar_Click(sender As Object, e As EventArgs) Handles cancelar.Click
        acepto = False
        Xnumero_lineas = 0
        DialogResult = DialogResult.Cancel
    End Sub

    'Private Sub MSFLAlbaran_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles MSFLAlbaran.CellContentDoubleClick
    '    Dim nFila As Integer
    '    Dim nCol As Integer
    '    Dim nEntrada As Long
    '    Dim i As Integer
    '    Dim ii As Integer
    '    Dim nFilaFac As Integer
    '    Dim oFrm As FrmModificacionLineasAlbaran = New FrmModificacionLineasAlbaran

    '    nFila = MSFLAlbaran.SelectedRows(0).Index
    '    nCol = MSFLAlbaran.SelectedColumns(0).Index

    '    If nFila = 0 Then
    '        Exit Sub
    '    End If

    '    ' Si hacen clic sobre la serie o el número de albarán bajará todo el albarán
    '    ' en otro caso solo bajará esa línea
    '    nFilaFac = MSFLFactura.CurrentRow.Index
    '    nEntrada = MSFLFactura.Rows(nFila).DataBoundItem("numero_entrada").ToString

    '    If nCol = 2 Then
    '        oFrm.ObjetoGlobal = Me.ObjetoGlobal
    '        oFrm.NumeroEntrada = nEntrada
    '        If oFrm.ShowDialog() = DialogResult.Yes Then
    '            ReDim aBaseIva(3)
    '            ReDim aTipoIva(3)
    '            ReDim aCuotaIva(3)
    '            aTipoIva(0) = -1
    '            aTipoIva(1) = -1
    '            aTipoIva(2) = -1
    '            aTipoIva(3) = -1
    '            CargarAlbaran(seleccionSQL) ' Carga albaranes del proveedor
    '        End If

    '    ElseIf nCol > 3 Then
    '        MSFLAlbaran.Rows.Add()
    '        nFilaFac = nFilaFac + 1
    '        For ii = 0 To 11
    '            MSFLAlbaran.Rows(nFilaFac).Cells(ii).Value = MSFLFactura.Rows(nFilaFac).Cells(ii).Value
    '        Next
    '        RestaIVAS(CDbl(MSFLFactura.Rows(nFila).Cells(9).Value), CInt(MSFLFactura.Rows(nFila).Cells(10).Value))

    '        MSFLFactura.Rows.Remove(MSFLFactura.Rows(nFila))
    '    Else
    '        For i = (MSFLFactura.Rows.Count - 1) To 1 Step -1
    '            If MSFLFactura.Rows(i).Cells("numero_entrada").Value = nEntrada Then  ' Es del mismo albarán
    '                MSFLAlbaran.Rows.Add()
    '                nFilaFac = nFilaFac + 1
    '                For ii = 0 To 11
    '                    MSFLAlbaran.Rows(nFilaFac).Cells(ii).Value = MSFLFactura.Rows(i).Cells(ii).Value
    '                Next
    '                RestaIVAS(CDbl(MSFLFactura.Rows(i).Cells(9).Value), CInt(MSFLFactura.Rows(i).Cells(10).Value))
    '                MSFLFactura.Rows.Remove(MSFLFactura.Rows(i))
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub aceptar_Click(sender As Object, e As EventArgs) Handles aceptar.Click
        Dim i As Integer
        acepto = True
        Xnumero_lineas = (MSFLFactura.Rows.Count - 1)
        If Xnumero_lineas = 2 Then
            If Trim(MSFLFactura.Rows(i).Cells("numero_entrada").Value) = "" Then
                Me.DialogResult = DialogResult.Cancel
            End If
        End If
        ReDim Xdetalle_factura(Xnumero_lineas)
        For i = 1 To Xnumero_lineas
            Xdetalle_factura(i) = Format(CLng(MSFLFactura.Rows(i).Cells("numero_entrada").Value), "0000000000") + Format(CLng(MSFLFactura.Rows(i).Cells("linea_albaran").Value), "00000")
        Next
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub FrmSeleccionLineaAlbaran_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text + "  Empresa: " + Trim(ObjetoGlobal.empresaactual) + " Ejercicio: " + Trim(ObjetoGlobal.EjercicioActual)

        ReDim aBaseIva(3)
        ReDim aTipoIva(3)
        ReDim aCuotaIva(3)

        aTipoIva(0) = -1
        aTipoIva(1) = -1
        aTipoIva(2) = -1

        acepto = False
        Xnumero_lineas = 0
        If CStr(codigo_proveedor).Trim <> "" Then
            CargarAlbaran("")
        End If

    End Sub

    Private Sub cbProveedor_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbProveedor.SelectionChangeCommitted
        If cbProveedor.SelectedValue.trim <> "" Then
            CargarAlbaran("")
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim nf As Long

        For nf = 1 To MSFLFactura.Rows.Count - 1
            If Trim(MSFLFactura.Rows(nf).Cells(3).Value) = Trim(TxtFiltro.Text) Then
                'MSFLFactura.CurrentCell = MSFLFactura.Item(0, nf)
                MSFLFactura.Rows(nf).Selected = True
                MSFLFactura.CurrentCell = MSFLFactura.Rows(nf).Cells(3)
                Return
            End If
        Next

    End Sub

    'Private Sub MSFLAlbaran_DoubleClick(sender As Object, e As EventArgs) Handles MSFLAlbaran.DoubleClick
    '    Dim nFila As Integer
    '    Dim nCol As Integer
    '    Dim nEntrada As Long
    '    Dim i As Integer
    '    Dim ii As Integer
    '    Dim nFilaFac As Integer
    '    Dim oFrm As FrmModificacionLineasAlbaran = New FrmModificacionLineasAlbaran

    '    nFila = MSFLAlbaran.SelectedRows(0).Index
    '    nCol = MSFLAlbaran.SelectedColumns(0).Index
    '    If nFila = 0 Then
    '        Exit Sub
    '    End If

    '    ' Si hacen clic sobre la serie o el número de albarán bajará todo el albarán
    '    ' en otro caso solo bajará esa línea
    '    nFilaFac = MSFLFactura.CurrentRow.Index
    '    nEntrada = MSFLFactura.Rows(nFila).DataBoundItem("numero_entrada").ToString

    '    If nCol = 2 Then
    '        oFrm.ObjetoGlobal = Me.ObjetoGlobal
    '        oFrm.NumeroEntrada = nEntrada
    '        If oFrm.ShowDialog() = DialogResult.Yes Then
    '            ReDim aBaseIva(3)
    '            ReDim aTipoIva(3)
    '            ReDim aCuotaIva(3)
    '            aTipoIva(0) = -1
    '            aTipoIva(1) = -1
    '            aTipoIva(2) = -1
    '            CargarAlbaran(seleccionSQL) ' Carga albaranes del proveedor
    '        End If

    '    ElseIf nCol > 3 Then
    '        MSFLAlbaran.Rows.Add()
    '        nFilaFac = nFilaFac + 1
    '        For ii = 0 To 11
    '            MSFLAlbaran.Rows(nFilaFac).Cells(ii).Value = MSFLFactura.Rows(nFilaFac).Cells(ii).Value
    '        Next
    '        RestaIVAS(CDbl(MSFLFactura.Rows(nFila).Cells(9).Value), CInt(MSFLFactura.Rows(nFila).Cells(10).Value))

    '        MSFLFactura.Rows.Remove(MSFLFactura.Rows(nFila))
    '    Else
    '        For i = (MSFLFactura.Rows.Count - 1) To 1 Step -1
    '            If MSFLFactura.Rows(i).Cells("numero_entrada").Value = nEntrada Then  ' Es del mismo albarán
    '                MSFLAlbaran.Rows.Add()
    '                nFilaFac = nFilaFac + 1
    '                For ii = 0 To 11
    '                    MSFLAlbaran.Rows(nFilaFac).Cells(ii).Value = MSFLFactura.Rows(i).Cells(ii).Value
    '                Next
    '                RestaIVAS(CDbl(MSFLFactura.Rows(i).Cells(9).Value), CInt(MSFLFactura.Rows(i).Cells(10).Value))
    '                MSFLFactura.Rows.Remove(MSFLFactura.Rows(i))
    '            End If
    '        Next
    '    End If
    'End Sub

    Private Sub cbProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProveedor.SelectedIndexChanged

    End Sub

    'Private Sub MSFLAlbaran_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MSFLAlbaran.CellContentClick
    '    Dim nFila As Integer
    '    Dim nCol As Integer
    '    Dim nEntrada As Long
    '    Dim i As Integer
    '    Dim ii As Integer
    '    Dim nFilaFac As Integer
    '    Dim oFrm As FrmModificacionLineasAlbaran = New FrmModificacionLineasAlbaran

    '    nFila = e.RowIndex
    '    nCol = e.ColumnIndex

    '    'nFila = MSFLAlbaran.SelectedCells(0).RowIndex

    '    'If nFila = 0 Then
    '    '    Return
    '    'End If

    '    ' Si hacen clic sobre la serie o el número de albarán bajará todo el albarán
    '    ' en otro caso solo bajará esa línea
    '    nFilaFac = MSFLFactura.Rows.Count - 1
    '    nEntrada = CDbl(MSFLAlbaran.Item(0, nFila).Value) '.Rows(nFila).DataBoundItem("numero_entrada").ToString

    '    If nCol = 2 Then
    '        oFrm.ObjetoGlobal = Me.ObjetoGlobal
    '        oFrm.NumeroEntrada = nEntrada
    '        If oFrm.ShowDialog() = DialogResult.Yes Then
    '            ReDim aBaseIva(3)
    '            ReDim aTipoIva(3)
    '            ReDim aCuotaIva(3)
    '            aTipoIva(0) = -1
    '            aTipoIva(1) = -1
    '            aTipoIva(2) = -1
    '            CargarAlbaran(seleccionSQL) ' Carga albaranes del proveedor
    '        End If
    '    ElseIf nCol > 3 Then

    '        If (MSFLFactura.Rows.Count > 0) Then
    '            Dim FilasFact As Integer

    '            FilasFact = MSFLFactura.Rows.Count

    '            For i = 0 To MSFLAlbaran.Rows.Count - 1
    '                Dim j As Integer
    '                FilasFact = FilasFact + 2
    '                For j = i To MSFLAlbaran.ColumnCount - 1
    '                    'MSFLFactura.Rows(j).Cells(1).Value = MSFLAlbaran.Rows(0).Cells(i).Value
    '                    MSFLFactura.Item(j, FilasFact).Value = MSFLAlbaran.Item(j, FilasFact).Value
    '                Next
    '            Next

    '        End If
    '    Else
    '        'MSFLAlbaran.Rows(nFila).Cells.CopyTo(MSFLFactura.DA, MSFLFactura.Rows.Count + 1)

    '        'MSFLFactura.Rows.Insert(MSFLFactura.Rows.Count + 1
    '        nFilaFac = nFilaFac + 1
    '        '        For ii = 0 To 11
    '        '            MSFLFactura.TextMatrix(nFilaFac, ii) = MSFLAlbaran.TextMatrix(nFila, ii)
    '        '        Next
    '        '        SumaIVAS CDbl(MSFLFactura.TextMatrix(nFilaFac, 9)), CInt(MSFLFactura.TextMatrix(nFilaFac, 10))
    '        'MSFLAlbaran.RemoveItem(nFila)
    '        '    Else
    '        '        For i = (MSFLAlbaran.Rows - 1) To 1 Step -1
    '        '            If MSFLAlbaran.TextMatrix(i, 0) = nEntrada Then  ' Es del mismo albarán
    '        '                MSFLFactura.Rows = MSFLFactura.Rows + 1
    '        '                nFilaFac = nFilaFac + 1
    '        '                For ii = 0 To 11
    '        '                    MSFLFactura.TextMatrix(nFilaFac, ii) = MSFLAlbaran.TextMatrix(i, ii)
    '        '                Next
    '        '                SumaIVAS CDbl(MSFLFactura.TextMatrix(nFilaFac, 9)), CInt(MSFLFactura.TextMatrix(nFilaFac, 10))
    '        '       MSFLAlbaran.RemoveItem(i)
    '        '            End If
    '        '        Next
    '        '    End If

    '        '    DataGridView31.ColumnCount = 2
    '        '    DataGridView31.RowCount = 81


    '        '    If (DataGridView31.Rows.Count > 0) Then

    '        '        For i = 0 To DataGridView25.ColumnCount - 1
    '        '            'If DataGridView25.Columns(i).Visible = True Then
    '        '            Dim j As Integer


    '        '            For j = i To DataGridView25.ColumnCount - 1
    '        '                DataGridView31.Rows(j).Cells(0).Value = DataGridView25.Columns(i).HeaderText
    '        '            Next


    '        '            For j = i To DataGridView25.ColumnCount - 1
    '        '                DataGridView31.Rows(j).Cells(1).Value = DataGridView25.Rows(0).Cells(i).Value
    '        '            Next
    '        '        Next

    '    End If
    '    '    En línea


    'End Sub

    Private Sub MSFLAlbaran_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles MSFLAlbaran.CellDoubleClick
        Dim nFila As Integer
        Dim nCol As Integer
        Dim nEntrada As Long
        Dim i As Integer
        Dim ii As Integer
        Dim nFilaFac As Integer
        Dim oFrm As FrmModificacionLineasAlbaran = New FrmModificacionLineasAlbaran

        Try

            nFila = e.RowIndex
            nCol = e.ColumnIndex

            nFila = MSFLAlbaran.SelectedCells(0).RowIndex

            'If nFila = 0 Then
            '    Exit Sub
            'End If

            ' Si hacen clic sobre la serie o el número de albarán bajará todo el albarán
            ' en otro caso solo bajará esa línea
            'nFilaFac = MSFLFactura.SelectedRows(0).Index
            nEntrada = CDbl(MSFLAlbaran.Item(0, nFila).Value) '.Rows(nFila).DataBoundItem("numero_entrada").ToString

            If nCol = 2 Then
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.NumeroEntrada = nEntrada
                If oFrm.ShowDialog() = DialogResult.Yes Then
                    ReDim aBaseIva(3)
                    ReDim aTipoIva(3)
                    ReDim aCuotaIva(3)
                    aTipoIva(0) = -1
                    aTipoIva(1) = -1
                    aTipoIva(2) = -1
                    aTipoIva(3) = -1
                    CargarAlbaran(seleccionSQL) ' Carga albaranes del proveedor
                End If

            ElseIf nCol > 3 Then
                MSFLFactura.Rows.Insert(0)
                nFilaFac = 0 'MSFLFactura.Rows().Count - 1

                For ii = 0 To 11
                    MSFLFactura.Rows(nFilaFac).Cells(ii).Value = MSFLAlbaran.Rows(nFila).Cells(ii).Value
                Next
                SumaIVAS(CDbl(MSFLFactura.Rows(nFilaFac).Cells(9).Value), CInt(MSFLFactura.Rows(nFilaFac).Cells(10).Value))
                MSFLAlbaran.Rows.Remove(MSFLAlbaran.Rows(nFila))
            Else
                For i = (MSFLAlbaran.Rows.Count - 1) To 0 Step -1
                    If MSFLAlbaran.Rows(i).Cells(0).Value = nEntrada Then  ' Es del mismo albarán
                        MSFLFactura.Rows.Insert(0)
                        nFilaFac = 0
                        For ii = 0 To 11
                            MSFLFactura.Rows(nFilaFac).Cells(ii).Value = MSFLAlbaran.Rows(i).Cells(ii).Value
                        Next
                        SumaIVAS(CDbl(MSFLFactura.Rows(nFilaFac).Cells(9).Value), CInt(MSFLFactura.Rows(nFilaFac).Cells(10).Value))
                        MSFLAlbaran.Rows.Remove(MSFLAlbaran.Rows(i))
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ex.Source)
        End Try

    End Sub



    Private Sub MSFLFactura_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles MSFLFactura.CellDoubleClick
        Dim nFila As Integer
        Dim nCol As Integer
        Dim nEntrada As Long
        Dim i As Integer
        Dim ii As Integer
        Dim nFilaFac As Integer
        Dim oFrm As FrmModificacionLineasAlbaran = New FrmModificacionLineasAlbaran

        Try
            nFila = e.RowIndex
            nCol = e.ColumnIndex

            ' Si hacen clic sobre la serie o el número de albarán bajará todo el albarán
            ' en otro caso solo bajará esa línea
            nFilaFac = MSFLFactura.Rows.Count - 1
            nEntrada = CDbl(MSFLFactura.Item(0, nFila).Value)
            nFilaFac = MSFLFactura.SelectedCells(0).RowIndex

            If nCol = 2 Then
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.NumeroEntrada = nEntrada
                If oFrm.ShowDialog() = DialogResult.Yes Then
                    ReDim aBaseIva(3)
                    ReDim aTipoIva(3)
                    ReDim aCuotaIva(3)
                    aTipoIva(0) = -1
                    aTipoIva(1) = -1
                    aTipoIva(2) = -1
                    aTipoIva(3) = -1
                    CargarAlbaran(seleccionSQL) ' Carga albaranes del proveedor
                End If

            ElseIf nCol > 3 Then
                MSFLAlbaran.Rows.Insert(0)
                nFilaFac = 0

                For ii = 0 To 11
                    MSFLAlbaran.Rows(nFilaFac).Cells(ii).Value = MSFLFactura.Rows(nFila).Cells(ii).Value
                Next
                RestaIVAS(CDbl(MSFLFactura.Rows(nFila).Cells(9).Value), CInt(MSFLFactura.Rows(nFila).Cells(10).Value))
                MSFLFactura.Rows.Remove(MSFLFactura.Rows(nFila))

            Else
                For i = (MSFLFactura.Rows.Count - 1) To 0 Step -1
                    If MSFLFactura.Rows(i).Cells(0).Value = nEntrada Then  ' Es del mismo albarán
                        MSFLAlbaran.Rows.Insert(0)
                        nFilaFac = 0

                        For ii = 0 To 11
                            MSFLAlbaran.Rows(nFilaFac).Cells(ii).Value = MSFLFactura.Rows(i).Cells(ii).Value
                        Next
                        RestaIVAS(CDbl(MSFLFactura.Rows(i).Cells(9).Value), CInt(MSFLFactura.Rows(i).Cells(10).Value))
                        MSFLFactura.Rows.Remove(MSFLFactura.Rows(i))
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ex.Source)
        End Try


    End Sub


End Class