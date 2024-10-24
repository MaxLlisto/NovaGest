Public Class FrmModificacionLineasAlbaran
    Public ObjetoGlobal As Object
    Public NumeroEntrada As Long
    Dim bImpresasEtCB As Boolean
    Dim bImpresasEtPr As Boolean
    Dim bCambioPVP As Boolean
    Dim nLimiteMargen As Double
    Dim nFilaEditada As Integer
    Dim bEsunAlta As Boolean
    Dim ClaveAjenaArticulo As Boolean
    Dim BusquedaArticulo As Boolean
    Dim TipoIva As Integer
    Dim TablaIVA As String
    Dim cAlmacen As String
    Dim nProveedor As Double
    Dim cCodigo_tarifa As String
    Dim FormularioGrid As Boolean
    Dim CtrlPulsado As Boolean
    Dim dFechaAlbaran As Date
    Dim cSerie_albaran_p As String
    Dim cNumero_albaran_p As String
    Dim dFecha_movimiento As Date
    Dim hora_movimiento As String
    Dim cUnidad_stock As String
    Dim nClave_doc As Integer
    Dim ArticuloInicial As String
    Dim PrecioInicial As Double
    Dim CantidadInicial As Double
    Dim precio_ref_inicial As Double
    Dim Stock_inicial As Double
    Dim precio_anterior As Double
    Dim precio_posterior As Double
    Dim HayModificaciones As Boolean
    Dim EnEdicion As Boolean

    Private Sub CmdCancelar_Click()
        nFilaEditada = 0
        CmdAceptar.Visible = False
        CmdCancelar.Visible = False
        TxtPrecio.Visible = False
        TxtCantidad.Visible = False
        TxtImporte.Visible = False
        '    TxtArticulo.Visible = False
        TxtDetalle.Visible = False
        CmdEliminarLinea.Visible = True
        Salir.Enabled = True
        'Grabar.Enabled = True
        CmdModificarDetalle.Visible = True
        'CmdAddLinea.Visible = True
        'CmdGridcodigo_articulo.Visible = False

    End Sub

    Private Sub RellenaGrid()
        Dim sSql As String
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsExi As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSqlalb As String
        Dim RsCond As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsDocumentos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Row As DataGridViewRow

        MSProcesos().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "articulo"
        column1.HeaderText = "Artículo"
        column1.Width = 25
        MSProcesos().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "descripcion"
        column2.HeaderText = "Descripción"
        column2.Width = 200
        MSProcesos().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "cant"
        column3.HeaderText = "Cant."
        column3.Width = 25
        MSProcesos().Columns.Add(column3)

        Dim column4 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column4.Name = "precio"
        column4.HeaderText = "Precio"
        column4.Width = 50
        MSProcesos().Columns.Add(column4)

        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "importe_linea"
        column5.HeaderText = "Importe línea"
        column5.Width = 50
        MSProcesos().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "almacen"
        column6.HeaderText = "Almacén"
        column6.Width = 50
        MSProcesos().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "iva"
        column7.HeaderText = "IVA"
        column7.Width = 50
        MSProcesos().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "linea_albaran"
        column8.HeaderText = "linea albarán"
        column8.Width = 50
        MSProcesos().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "articulo_inicial"
        column9.HeaderText = "Artículo inicial"
        column9.Width = 50
        MSProcesos().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "cantidad_inicial"
        column10.HeaderText = "cantidad_inicial"
        column10.Width = 50
        MSProcesos().Columns.Add(column10)

        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "importe_inicial"
        column11.HeaderText = "importe_inicial"
        column11.Width = 50
        MSProcesos().Columns.Add(column11)

        Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column12.Name = "total_inicial"
        column12.HeaderText = "total_inicial"
        column12.Width = 50
        MSProcesos().Columns.Add(column12)

        Dim column13 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column13.Name = "codigo_inventario"
        column13.HeaderText = "codigo_inventario"
        column13.Width = 50
        MSProcesos().Columns.Add(column13)

        Dim column14 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column14.Name = "codigo_unidad"
        column14.HeaderText = "codigo_unidad"
        column14.Width = 50
        MSProcesos().Columns.Add(column14)

        Dim column15 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column15.Name = "nada"
        column15.HeaderText = "nada"
        column15.Width = 0
        MSProcesos().Columns.Add(column15)

        sSqlalb = "SELECT * from Albaran_com_c WHERE empresa ='" & Trim(ObjetoGlobal.empresaactual) & "' AND numero_entrada = " & NumeroEntrada
        RsCAlb.Open(sSqlalb, ObjetoGlobal.cn)
        If RsCAlb.RecordCount > 0 Then

            cAlmacen = RsCAlb!Almacen
            nProveedor = RsCAlb!codigo_proveedor
            dFechaAlbaran = RsCAlb!fecha_albaran
            cAlmacen = RsCAlb!Almacen
            nProveedor = RsCAlb!codigo_proveedor
            dFechaAlbaran = RsCAlb!fecha_albaran
            cSerie_albaran_p = RsCAlb!serie_albaran_p
            cNumero_albaran_p = RsCAlb!numero_albaran_p
            dFecha_movimiento = IIf(IsDBNull(RsCAlb!fecha_entrada), RsCAlb!fecha_albaran, RsCAlb!fecha_entrada)
            hora_movimiento = RsCAlb!Hora_entrada

            TablaIVA = RsCAlb!tabla_iva_prov

            RsCond.Open("SELECT codigo_tarifa FROM Condiciones_compra WHERE empresa='" & Trim(ObjetoGlobal.empresaactual) & "' and codigo_proveedor = " & nProveedor & " and tipo_compra = 'C' and fecha_inicio <= '" & dFechaAlbaran & "' AND fecha_final >= '" & dFechaAlbaran & "' and codigo_tarifa > '10'", ObjetoGlobal.cn)
            If RsCond.RecordCount > 0 Then
                cCodigo_tarifa = RsCond!codigo_tarifa
            End If

            sSql = "SELECT * FROM Albaran_com_l WHERE empresa ='" & Trim(ObjetoGlobal.empresaactual) & "' and NUMERO_ENTRADA=" & NumeroEntrada

            RsAlb.Open(sSql, ObjetoGlobal.cn)

            Dim Cell = New DataGridViewTextBoxCell
            While Not RsAlb.EOF
                Row = New DataGridViewRow
                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!articulo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!Descripcion
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!unidades
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!Precio
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!Importe
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!Almacen
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!tipo_iva
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = RsAlb!Linea_albaran
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = Trim(RsAlb!articulo)
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & RsAlb!unidades
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & RsAlb!Precio
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & RsAlb!Importe
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & RsAlb!cod_movimiento
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = "" & RsAlb!unidad_entrada
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = ""
                Row.Cells.Add(Cell)

                MSProcesos().Rows.Add(Row)
                RsAlb.MoveNext()
            End While
            RsAlb.Close()
        End If
        RsCAlb.Close()

    End Sub

    Private Sub TxtCantidad_LostFocus()
        If TxtPrecio.Text <> "" And TxtCantidad.Text <> "" Then
            TxtImporte.Text = Math.Round(TxtPrecio.Text * TxtCantidad.Text, 2)
        End If
    End Sub


    Private Sub TxtImporte_LostFocus()
        If TxtPrecio.Text <> "" And TxtCantidad.Text <> "" Then
            TxtImporte.Text = Math.Round(TxtPrecio.Text * TxtCantidad.Text, 2)
        End If
    End Sub

    Private Sub Poncontroles()

        If CStr(MSProcesos.Rows(nFilaEditada).Cells("iva").Value) <> "" Then
            TipoIva = MSProcesos.Rows(nFilaEditada).Cells("iva").Value
        End If
        cUnidad_stock = MSProcesos.Rows(nFilaEditada).Cells("codigo_unidad").Value

        TxtDetalle.Visible = True
        TxtDetalle.Text = MSProcesos.Rows(nFilaEditada).Cells("descripcion").Value
        TxtCantidad.Visible = True
        TxtCantidad.Text = MSProcesos.Rows(nFilaEditada).Cells("cant").Value
        TxtPrecio.Visible = True
        TxtPrecio.Text = MSProcesos.Rows(nFilaEditada).Cells("precio").Value
        TxtImporte.Visible = True
        TxtImporte.Text = MSProcesos.Rows(nFilaEditada).Cells("importe_linea").Value

    End Sub

    Private Sub Grabacontroles()
        'MSProcesos.TextMatrix(nFilaEditada, 0) = TxtArticulo.Text
        MSProcesos.Rows(nFilaEditada).Cells("descripcion").Value = TxtDetalle.Text
        MSProcesos.Rows(nFilaEditada).Cells("cantidad").Value = TxtCantidad.Text
        MSProcesos.Rows(nFilaEditada).Cells("precio").Value = TxtPrecio.Text
        MSProcesos.Rows(nFilaEditada).Cells("importe_linea").Value = TxtImporte.Text
        MSProcesos.Rows(nFilaEditada).Cells("almacen").Value = cAlmacen
        MSProcesos.Rows(nFilaEditada).Cells("iva").Value = "" & TipoIva
        MSProcesos.Rows(nFilaEditada).Cells("codigo_unidad").Value = "" & cUnidad_stock
    End Sub

    Private Sub HacerInventario()
    End Sub

    Private Sub txtPrecio_LostFocus()
        If TxtPrecio.Text <> "" And TxtCantidad.Text <> "" Then
            TxtImporte.Text = Math.Round(TxtPrecio.Text * TxtCantidad.Text, 2)
        End If
    End Sub

    Private Function GrabarLinea() As Boolean
        Dim RsHtco_Almacen As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsIva As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        Dim Tipo As String
        Dim nLinea As Integer
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbP As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        ' Tipo -> A Nueva línea  M Línea existente
        Try

            nLinea = MSProcesos.Rows(nFilaEditada).Cells("numero_linea").Value
            Tipo = "M"

            sSql = "SELECT * FROM Albaran_com_l WHERE empresa ='" & Trim(ObjetoGlobal.empresaactual) & "' and NUMERO_ENTRADA=" & NumeroEntrada & " and linea_albaran=" & nLinea
            RsAlb.Open(sSql, ObjetoGlobal.cn, True)

            If RsAlb.RecordCount = 1 Then
                RsAlb!unidades = CDbl(MSProcesos.Rows(nFilaEditada).Cells("unidades").Value)
                RsAlb!unidades_stock = CDbl(MSProcesos.Rows(nFilaEditada).Cells("unidades_stock").Value)
                RsAlb!Precio = CDbl(MSProcesos.Rows(nFilaEditada).Cells("Precio").Value)
                RsAlb!Importe = CDbl(MSProcesos.Rows(nFilaEditada).Cells("importe_linea").Value)
                RsAlb.Update()
            Else
                MsgBox("Error modificando la línea de albarán")
            End If
            '       ModificaMovimiento RsAlb!cod_movimiento
            RsAlb.Close()

            MSProcesos.Rows(nFilaEditada).Cells("articulo_inicial").Value = MSProcesos.Rows(nFilaEditada).Cells("articulo").Value
            MSProcesos.Rows(nFilaEditada).Cells("cantidad_inicial").Value = MSProcesos.Rows(nFilaEditada).Cells("cant").Value
            MSProcesos.Rows(nFilaEditada).Cells("importe_inicial").Value = MSProcesos.Rows(nFilaEditada).Cells("precio").Value
            MSProcesos.Rows(nFilaEditada).Cells("total_inicial").Value = MSProcesos.Rows(nFilaEditada).Cells("importe_linea").Value
            Return True
        Catch ex As Exception
            MsgBox("Imposible modificar esta línea del albarán")
            Return False
        End Try

    End Function

    Private Function BorrarLinea(ByVal fi As Integer) As Boolean
        Dim sSql As String
        Dim nLinea As Integer
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Try
            ' Tipo -> A Nueva línea  M Línea existente
            nLinea = MSProcesos.Rows(fi).Cells("numero_linea").Value

            ' Grabaremos la línea del albaran
            sSql = "SELECT * FROM Albaran_com_l WHERE empresa ='" & Trim(ObjetoGlobal.empresaactual) & "' and NUMERO_ENTRADA=" & NumeroEntrada & " and linea_albaran=" & nLinea
            RsAlb.Open(sSql, ObjetoGlobal.cn, True)

            If RsAlb.RecordCount = 1 Then
                RsAlb.Delete()
            End If
            RsAlb.Close()
            Return True

        Catch ex As Exception
            MsgBox("Imposible borrar esta línea del albarán")
            Return False
        End Try

    End Function


    Private Sub ModificaMovimiento(cod_movimiento)
        Dim sSql As String
        Dim RsHtco_Almacen As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        sSql = "SELECT * FROM Almacen" + Trim(ObjetoGlobal.EjercicioActual) + "_h WHERE empresa='" & Trim(ObjetoGlobal.empresaactual) & "' AND codigo_movimiento =" & cod_movimiento
        RsHtco_Almacen.Open(sSql, ObjetoGlobal.cn, True)

        If RsHtco_Almacen.RecordCount = 1 Then
            RsHtco_Almacen!articulo = "" & MSProcesos.Rows(nFilaEditada).Cells("articulo").Value
            RsHtco_Almacen!Descripcion = "" & MSProcesos.Rows(nFilaEditada).Cells("descripcion").Value
            RsHtco_Almacen!unidades = CDbl(MSProcesos.Rows(nFilaEditada).Cells("cant").Value)
            RsHtco_Almacen!unidades_stock = CDbl(MSProcesos.Rows(nFilaEditada).Cells("cant").Value)
            RsHtco_Almacen!Precio = CDbl(CDbl(MSProcesos.Rows(nFilaEditada).Cells("precio").Value))
            RsHtco_Almacen!Importe = CDbl(CDbl(MSProcesos.Rows(nFilaEditada).Cells("importe_linea").Value))
            RsHtco_Almacen.Update()
        Else
            'Error
        End If
        RsHtco_Almacen.Close()
    End Sub

    Private Sub CalcularPrecioRef(codigo_articulo As String, numero_entrada As Long, Linea_albaran As Integer, AD As String)
        Dim SACT As Double ' Saldo posterior a la entrada
        Dim Sql As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Sql = "SELECT l.articulo, l.unidades_stock, l.precio, l.importe, c.fecha_entrada, c.hora_entrada, c.almacen, precio_anterior FROM albaran_com_l l LEFT JOIN albaran_com_c c ON c.empresa = l.empresa AND c.numero_entrada = l.numero_entrada WHERE l.empresa = '" & ObjetoGlobal.empresaactual & "' AND l.articulo = '" & codigo_articulo & "' AND l.numero_entrada = " & numero_entrada & " AND l.linea_albaran = " & Linea_albaran

        Rs.Open(Sql, ObjetoGlobal.cn)

        SACT = ObtenerStockArticulo(Rs!articulo, Rs!fecha_entrada, Rs!Hora_entrada, Rs!Almacen)
        Stock_inicial = SACT - Rs!unidades_stock
        precio_ref_inicial = Rs!precio_anterior

    End Sub
    Private Function ObtenerStockArticulo(cArticulo As String, fecha_entrada As Date, Hora_entrada As String, Almacen As String) As Double
        Dim RsInv As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        Dim cHoraFecha As String

        ObtenerStockArticulo = 0

        cHoraFecha = CStr(Format(fecha_entrada, "dd/mm/yyyy") & " " & Format(Hora_entrada, "hh:mm:ss"))
        sSql = "select dbo.fn_existencia_a_fecha('" & Trim(ObjetoGlobal.empresaactual) & "', '" & Trim(ObjetoGlobal.EjercicioActual) & "', '" & cArticulo & "', '" & cHoraFecha & "', 'N', '" & Almacen & "')"
        RsInv.Open(sSql, ObjetoGlobal.cn)
        If Not RsInv.EOF Then
            ObtenerStockArticulo = RsInv(0)
        End If


    End Function

    Private Sub RegeneraPrecioReferencia(articulo As String, codigo_movimiento As Long, Stock_ini As Double, Precio_ref_ini As Double)
        Dim sSql As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsInv As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsAlba As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nStock As Double
        Dim rsLineas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nPrecioRef As Double
        Dim nTotalImporteRef As Double
        Dim EjercicioActual As String
        Dim fecha_hora As Date

        EjercicioActual = ObjetoGlobal.EjercicioActual

        sSql = "SELECT * FROM articulos WHERE empresa = '" & Trim(ObjetoGlobal.empresaactual) & "' and codigo_articulo = '" & articulo & "'"
        Rs.Open(sSql, ObjetoGlobal.cn, True)

        If Not Rs.EOF Then
            ' Precio inicial
            nPrecioRef = Precio_ref_ini
            nStock = Stock_ini

            nTotalImporteRef = Math.Round(nPrecioRef * nStock, 4)

            sSql = "SELECT * FROM almacen" & Trim(ObjetoGlobal.EjercicioActual) & "_h WHERE "
            sSql = sSql + " empresa = '" & Trim(ObjetoGlobal.empresaactual) & "' and codigo_movimiento = " & codigo_movimiento
            rsLineas.Open(sSql, ObjetoGlobal.cn)
            If Not rsLineas.EOF Then
                fecha_hora = rsLineas!fecha_hora
            End If

            sSql = "SELECT * FROM almacen" & Trim(ObjetoGlobal.EjercicioActual) & "_h WHERE "
            sSql = sSql + " empresa = '" & Trim(ObjetoGlobal.empresaactual) & "' and codigo_movimiento > " & codigo_movimiento & " and fecha_hora >='" & fecha_hora & "' AND articulo = '" & Rs!codigo_articulo & "' "
            sSql = sSql + " ORDER BY fecha_hora "

            rsLineas.Open(sSql, ObjetoGlobal.cn)

            While Not rsLineas.EOF
                If Trim(rsLineas!tipo_movimiento) = "E" Then ' Es una entrada
                    nTotalImporteRef = Math.Round(nPrecioRef * nStock, 4)
                    nStock = nStock + rsLineas!unidades_stock
                    nTotalImporteRef = nTotalImporteRef + rsLineas!Importe
                    rsAlba.Open("SELECT * FROM albaran_com_l WHERE empresa = '" & Trim(ObjetoGlobal.empresaactual) & "' AND cod_movimiento = " & rsLineas!codigo_movimiento, ObjetoGlobal.cn, True)
                    If rsAlba.RecordCount = 1 Then
                        rsAlba!precio_anterior = nPrecioRef
                        nPrecioRef = Math.Round(nTotalImporteRef / nStock, 4)
                        rsAlba!precio_posterior = nPrecioRef
                    End If

                ElseIf Trim(rsLineas!tipo_movimiento) = "S" Then ' Es una salida
                    rsAlba.Open("SELECT * FROM lineas_albaran WHERE empresa = '" & Trim(ObjetoGlobal.empresaactual) & "' AND serie_albaran = '" & Trim(rsLineas!serie_documento) & "' And numero_albaran = " & rsLineas!numero_documento & " AND linea_albaran=" & rsLineas!linea_documento, ObjetoGlobal.cn, True)
                    If rsAlba.RecordCount = 1 Then
                        rsAlba!total_coste = Math.Round(rsAlba!unidades_stock * nPrecioRef, 4) + Math.Round(rsAlba!unidades_stock * rsAlba!coste_adicional, 4)
                        rsAlba!prec_coste = nPrecioRef
                        rsAlba!total_margen = rsAlba!total_venta - rsAlba!total_coste
                        If rsAlba!total_venta <> 0 Then
                            rsAlba!porc_margen = Math.Round((rsAlba!total_margen * 100) / rsAlba!total_venta, 2)
                        End If
                        rsAlba.Update
                    Else
                        'Error
                    End If
                    nStock = nStock - rsLineas!unidades_stock
                    rsAlba.Close
                Else
                    ' Error
                End If
                rsLineas.MoveNext
            End While
            rsLineas.Close
            Rs!precio_ref = nPrecioRef
            Rs.Update
        End If
        Rs.Close

    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Grabacontroles()
        EnEdicion = False
        If GrabarLinea() Then
            CmdEliminarLinea.Visible = True
            CmdModificarDetalle.Visible = True
            CmdAceptar.Visible = False
            CmdCancelar.Visible = False
            TxtPrecio.Visible = False
            TxtCantidad.Visible = False
            TxtImporte.Visible = False
            TxtDetalle.Visible = False
            CmdModificarDetalle.Visible = True
            Salir.Enabled = True
            CmdModificarDetalle.Visible = True
            nFilaEditada = 0
            HayModificaciones = True
        End If

    End Sub

    Private Sub FrmModificacionLineasAlbaran_Load(sender As Object, e As EventArgs) Handles Me.Load
        RellenaGrid()
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles TxtCantidad.Validated
        If TxtPrecio.Text <> "" And TxtCantidad.Text <> "" Then
            TxtImporte.Text = Math.Round(TxtPrecio.Text * TxtCantidad.Text, 2)
        End If
    End Sub

    Private Sub TxtImporte_Validated(sender As Object, e As EventArgs) Handles TxtImporte.Validated
        If TxtPrecio.Text <> "" And TxtCantidad.Text <> "" Then
            TxtImporte.Text = Math.Round(TxtPrecio.Text * TxtCantidad.Text, 2)
        End If
    End Sub

    Private Sub TxtPrecio_Validated(sender As Object, e As EventArgs) Handles TxtPrecio.Validated
        If TxtPrecio.Text <> "" And TxtCantidad.Text <> "" Then
            TxtImporte.Text = Math.Round(TxtPrecio.Text * TxtCantidad.Text, 2)
        End If
    End Sub

    Private Sub CmdModificarDetalle_Click(sender As Object, e As EventArgs) Handles CmdModificarDetalle.Click
        If MSProcesos.Rows.Count > 0 Then
            Poncontroles()
            Salir.Enabled = False
            CmdAceptar.Visible = True
            CmdCancelar.Visible = True
            CmdEliminarLinea.Visible = False
            CmdModificarDetalle.Visible = False
            nFilaEditada = MSProcesos.CurrentRow.Index
            EnEdicion = True
        End If
    End Sub

    Private Sub CmdEliminarLinea_Click(sender As Object, e As EventArgs) Handles CmdEliminarLinea.Click
        Dim FilaSeleccionada As Integer = MSProcesos.CurrentRow.Index

        If BorrarLinea(FilaSeleccionada) Then
            MSProcesos.Rows.Remove(MSProcesos.Rows(FilaSeleccionada))
            HayModificaciones = True
        End If

    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        nFilaEditada = 0
        CmdAceptar.Visible = False
        CmdCancelar.Visible = False
        TxtPrecio.Visible = False
        TxtCantidad.Visible = False
        TxtImporte.Visible = False
        TxtDetalle.Visible = False
        CmdEliminarLinea.Visible = True
        Salir.Enabled = True
        CmdModificarDetalle.Visible = True
        EnEdicion = False
    End Sub

    Private Sub MSProcesos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MSProcesos.CellContentClick

    End Sub

    Private Sub MSProcesos_KeyDown(sender As Object, e As KeyEventArgs) Handles MSProcesos.KeyDown
        If EnEdicion Then
            e.SuppressKeyPress = True
        Else
            e.SuppressKeyPress = False
        End If
    End Sub
End Class