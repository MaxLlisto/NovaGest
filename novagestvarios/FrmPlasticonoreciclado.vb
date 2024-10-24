Imports System.IO

Public Class FrmPlasticonoreciclado
    Inherits libcomunes.FormGenerico
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim strStreamW As Stream
    Dim strStreamWriter As StreamWriter


    Private Sub BtCalcularinforme_Click(sender As Object, e As EventArgs) Handles BtCalcularinforme.Click
        Dim sql As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim datos(10)
        Dim cantidad As Double = 0.000000
        Dim importe As Double = 0.000000
        Dim lineaLog As String = ""
        Dim SubCantidad As Double = 0.000000
        Dim CadMat As String = ""
        Dim TieneAlguno As Boolean = False
        Dim ejeAnecoop As String
        Dim PedidoAnec As String


        If Not GeneraArchivoLogs() Then
            Return
        End If

        CabeceraVolcados()

        If TxtCliente.Text <> 0 Then
            sql = "SELECT distinct c.numero_documento, c.empresa, c.ejercicio ,c.serie, c.tipo_documento, c.fecha_documento, c.matricula, o.no_pedido_cliente, o.fecha_camion, c.pais_dest "
            sql = sql + " FROM cabeceras_salida c LEFT JOIN ordenes_carga o ON o.empresa= c.empresa AND o.numero_orden = c.orden_carga "
            sql = sql + " LEFT JOIN lineas_salida l ON l.empresa = c.empresa AND l.ejercicio = c.ejercicio AND l.serie = c.serie AND l.tipo_documento = c.tipo_documento AND l.numero_documento = c.numero_documento "
            sql = sql + " LEFT JOIN materiales_confec m ON m.empresa = c.empresa AND m.codigo_confeccion = l.codigo_confeccion "
            sql = sql + " LEFT JOIN materiales_confec p ON m.empresa = c.empresa AND m.codigo_confeccion = l.tipo_palet_sal "
            sql = sql + " WHERE c.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND c.fecha_documento BETWEEN '" & TextBoxCalendar1.Text & "' AND '" & TextBoxCalendar2.Text & "' AND pais_dest <> 'ES' "
            sql = sql + " AND codigo_cliente = " & TxtCliente.Text & " and EXISTS (SELECT codigo_articulo FROM peso_no_reciclado n WHERE n.empresa = c.empresa AND (n.codigo_articulo = m.codigo_material or n.codigo_articulo = p.codigo_material )) "
            sql = sql + " ORDER BY c.empresa, c.ejercicio, c.serie, c.tipo_documento, c.numero_documento, c.fecha_documento "
        Else
            sql = "SELECT distinct c.numero_documento, c.empresa, c.ejercicio ,c.serie, c.tipo_documento, c.fecha_documento, c.matricula, o.no_pedido_cliente, o.fecha_camion, c.pais_dest "
            sql = sql + " FROM cabeceras_salida c LEFT JOIN ordenes_carga o ON o.empresa= c.empresa AND o.numero_orden = c.orden_carga "
            sql = sql + " LEFT JOIN lineas_salida l ON l.empresa = c.empresa AND l.ejercicio = c.ejercicio AND l.serie = c.serie AND l.tipo_documento = c.tipo_documento AND l.numero_documento = c.numero_documento "
            sql = sql + " LEFT JOIN materiales_confec m ON m.empresa = c.empresa AND m.codigo_confeccion = l.codigo_confeccion "
            sql = sql + " LEFT JOIN materiales_confec p ON m.empresa = c.empresa AND m.codigo_confeccion = l.tipo_palet_sal "
            sql = sql + " WHERE c.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND c.fecha_documento BETWEEN '" & TextBoxCalendar1.Text & "' AND '" & TextBoxCalendar2.Text & "' AND pais_dest = 'ES' "
            sql = sql + " and EXISTS (SELECT codigo_articulo FROM peso_no_reciclado n WHERE n.empresa = c.empresa AND (n.codigo_articulo = m.codigo_material or n.codigo_articulo = p.codigo_material )) "
            sql = sql + " ORDER BY c.empresa, c.ejercicio, c.serie, c.tipo_documento, c.numero_documento, c.fecha_documento "
        End If

        If rs.Open(sql, ObjetoGlobal.cn) Then

            While Not rs.EOF

                lineaLog = "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & "" & vbCrLf
                'GrabarLineaLog(lineaLog)

                'If rs!numero_documento.trim = "00092334" Then
                '    MsgBox("aaaaa")
                'End If

                TieneAlguno = False

                cantidad = 0.000000
                importe = 0.000000

                CadMat = ""

                sql = "SELECT l.empresa, l.ejercicio, l.serie, l.tipo_documento, l.numero_documento, l.numero_linea, n.desde_fecha, n.hasta_fecha, n.peso_unidad, l.bultos, l.palets, m.codigo_material, m.unidades_material, ma.tipo_articulo as descripcion, l.codigo_variedad FROM lineas_salida l "
                sql = sql + " LEFT JOIN materiales_confec m ON m.empresa = l.empresa AND m.codigo_confeccion = l.codigo_confeccion "
                sql = sql + " LEFT JOIN materiales ma ON ma.empresa = l.empresa AND ma.codigo_articulo = m.codigo_material "
                sql = sql + " LEFT JOIN peso_no_reciclado n ON n.empresa = l.empresa And n.codigo_articulo = m.codigo_material "
                sql = sql + " LEFT JOIN variedades v ON v.empresa = l.empresa And v.codigo_variedad = l.codigo_variedad "
                sql = sql + " WHERE l.empresa = '" & rs!empresa & "' AND l.ejercicio = '" & rs!ejercicio & "' AND l.serie = '" & rs!serie & "' AND "
                sql = sql + " l.tipo_documento = '" & rs!tipo_documento & "' AND l.numero_documento = '" & rs!numero_documento & "' and not n.desde_fecha is null"

                If Mid(CStr("" & rs!no_pedido_cliente).Trim, 3, 1) = "/" Then
                    ejeAnecoop = Strings.Left(CStr("" & rs!no_pedido_cliente).Trim, 2)
                    PedidoAnec = Strings.Mid(CStr("" & rs!no_pedido_cliente).Trim, 4)
                    If PedidoAnec.Trim.Length > 3 Then
                        If Mid(PedidoAnec.Trim, PedidoAnec.Trim.Length - 1, 1) = "/" Then
                            PedidoAnec = Mid(PedidoAnec.Trim, 1, PedidoAnec.Trim.Length - 2)
                        End If
                    End If
                End If


                datos(1) = ejeAnecoop 'Strings.Right(CStr(Year(rs!fecha_documento)).Trim, 2)
                datos(2) = PedidoAnec 'CStr("" & rs!no_pedido_cliente).Trim
                datos(3) = rs!numero_documento.trim
                datos(8) = CStr("" & rs!fecha_camion)
                datos(9) = rs!matricula
                datos(10) = rs!pais_dest

                'lineaLog = "Serie;Tipo Doc;Número;Fecha;Pedido;Fecha camión;matrícula,país, palets"
                'GrabarLineaLog(lineaLog)
                'lineaLog = "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & ";" & CStr("" & rs!no_pedido_cliente).Trim & ";" & CStr("" & rs!fecha_camion) & ";" & rs!matricula & ";" & rs!pais_dest
                'GrabarLineaLog(lineaLog)

                If rs1.Open(sql, ObjetoGlobal.cn) Then
                    If rs1.RecordCount > 0 Then
                        'GrabarLineaLog("")
                        lineaLog = lineaLog & vbCrLf & "Serie;Tipo Doc;Número;Fecha;Pedido;Fecha camión;matrícula;país ;palets" & vbCrLf
                        'GrabarLineaLog(lineaLog)
                        lineaLog = lineaLog & "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & ";" & CStr("" & rs!no_pedido_cliente).Trim & ";" & CStr("" & rs!fecha_camion) & ";" & rs!matricula & ";" & rs!pais_dest & ";" & CuantosPalets(rs!empresa, rs!ejercicio, rs!serie, rs!tipo_documento, rs!numero_documento) & vbCrLf
                        'GrabarLineaLog(lineaLog)
                        lineaLog = lineaLog & vbCrLf & "Serie;Tipo Doc;Número;Fecha;Pedido;Fecha camión;matrícula;país;material;descripción;desde fecha;hasta fecha;cantidad;peso unidad; unidades material;bultos palet;palet" & vbCrLf
                        'GrabarLineaLog(lineaLog)
                    End If

                    While Not rs1.EOF

                        'sql = "SELECT p.* FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion WHERE p.empresa = '" & rs!empresa & "' AND p.ejercicio = '" & rs!ejercicio & "' AND p.serie = '" & rs!serie & "' AND "
                        'sql = sql + " p.tipo_documento = '" & rs!tipo_documento & "' AND p.numero_documento = '" & rs!numero_documento & "' AND o.codigo_variedad ='" & rs1!codigo_variedad & "'"

                        sql = "SELECT p.* FROM palets_salidas s LEFT JOIN palets p ON p.empresa = s.empresa AND s.codigo_palet = p.codigo_palet LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion WHERE s.empresa = '" & rs!empresa & "' AND s.ejercicio = '" & rs!ejercicio & "' AND s.serie = '" & rs1!serie & "' AND "
                        sql = sql + " s.tipo_documento = '" & rs1!tipo_documento & "' AND s.numero_documento = '" & rs1!numero_documento & "' AND s.numero_linea = " & rs1!numero_linea & " AND o.codigo_variedad ='" & rs1!codigo_variedad & "'"

                        If rs2.Open(sql, ObjetoGlobal.cn) Then
                            While Not rs2.EOF
                                If IsDBNull(rs1!hasta_Fecha) Then
                                    'If rs!numero_documento.trim = "00093052" And rs1!desde_Fecha >= CDate("01/04/2023") Then
                                    '    MsgBox(rs!numero_documento)
                                    'End If
                                    If rs2!Fecha_confeccion >= rs1!desde_Fecha Then 'Or (rs2!Fecha_confeccion < rs1!desde_Fecha And rs2!Fecha_confeccion < "01/01/2023") Then

                                        CadMat = CadMat + IIf(InStr(CadMat, Trim("" & rs1!descripcion)) <> 0, "", " [" & Trim("" & rs1!descripcion & "]"))
                                        datos(7) = CadMat

                                        cantidad = cantidad + CDbl((rs1!peso_unidad * rs1!unidades_material * (rs1!bultos / rs1!palets)))
                                        SubCantidad = CDbl((rs1!peso_unidad * rs1!unidades_material * (rs1!bultos / rs1!palets)))
                                        lineaLog = lineaLog & "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & ";" & CStr("" & rs!no_pedido_cliente).Trim & ";" & CStr("" & rs!fecha_camion) & ";" & rs!matricula & ";" & rs!pais_dest & ";" & rs1!codigo_material & ";" & ("" & rs1!descripcion).trim & ";" & rs1!desde_Fecha & ";" & rs1!hasta_Fecha & ";" & SubCantidad & ";" & rs1!peso_unidad & ";" & rs1!unidades_material & ";" & (rs1!bultos / rs1!palets) & ";" & rs2!codigo_palet & vbCrLf
                                        'GrabarLineaLog(lineaLog)
                                        TieneAlguno = True
                                    End If
                                Else
                                    If rs2!Fecha_confeccion <= rs1!hasta_Fecha And rs2!Fecha_confeccion >= rs1!desde_Fecha Then 'Or (rs2!Fecha_confeccion < rs1!desde_Fecha And rs2!Fecha_confeccion < "01/01/2023") Then

                                        'CadMat = CadMat + IIf(InStr(CadMat, "" & rs1!descripcion.trim) <> 0, "", " [" & "" & rs1!descripcion.trim & "]")
                                        CadMat = CadMat + IIf(InStr(CadMat, Trim("" & rs1!descripcion)) <> 0, "", " [" & Trim("" & rs1!descripcion & "]"))
                                        datos(7) = CadMat

                                        cantidad = cantidad + CDbl((rs1!peso_unidad * rs1!unidades_material * (rs1!bultos / rs1!palets)))
                                        SubCantidad = CDbl((rs1!peso_unidad * rs1!unidades_material * (rs1!bultos / rs1!palets)))
                                        lineaLog = lineaLog & "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & ";" & CStr("" & rs!no_pedido_cliente).Trim & ";" & CStr("" & rs!fecha_camion) & ";" & rs!matricula & ";" & rs!pais_dest & ";" & rs1!codigo_material & ";" & ("" & rs1!descripcion).trim & ";" & rs1!desde_Fecha & ";" & rs1!hasta_Fecha & ";" & SubCantidad & ";" & rs1!peso_unidad & ";" & rs1!unidades_material & ";" & (rs1!bultos / rs1!palets) & ";" & rs2!codigo_palet & vbCrLf
                                        'GrabarLineaLog(lineaLog)
                                        TieneAlguno = True
                                    End If
                                End If
                                rs2.MoveNext()
                            End While
                        End If
                        rs2.Close()
                        rs1.MoveNext()
                    End While
                End If
                rs1.Close()
                'l.empresa, l.ejercicio, l.serie, l.tipo_documento, l.numero_documento, l.numero_linea, n.desde_fecha, n.hasta_fecha, n.peso_unidad, l.bultos, l.palets, m.codigo_material, m.unidades_material, ma.tipo_articulo As descripcion, l.codigo_variedad FROM lineas_salida l
                sql = "SELECT l.empresa, l.ejercicio, l.serie, l.tipo_documento, l.numero_documento, l.numero_linea, n.desde_fecha, n.hasta_fecha, n.peso_unidad, l.bultos, l.palets, m.codigo_material, m.unidades_material, ma.tipo_articulo As descripcion, l.codigo_variedad FROM lineas_salida l "
                sql = sql + " LEFT JOIN materiales_confec m ON m.empresa = l.empresa AND m.codigo_confeccion = l.tipo_palet_sal "
                sql = sql + " LEFT JOIN materiales ma ON ma.empresa = l.empresa AND ma.codigo_articulo = m.codigo_material "
                sql = sql + " LEFT JOIN peso_no_reciclado n ON n.empresa = l.empresa And n.codigo_articulo = m.codigo_material "
                sql = sql + " LEFT JOIN variedades v ON v.empresa = l.empresa And v.codigo_variedad = l.codigo_variedad "
                sql = sql + " WHERE l.empresa = '" & rs!empresa & "' AND l.ejercicio = '" & rs!ejercicio & "' AND l.serie = '" & rs!serie & "' AND "
                sql = sql + " l.tipo_documento = '" & rs!tipo_documento & "' AND l.numero_documento = '" & rs!numero_documento & "' and not n.desde_fecha is null"

                If rs1.Open(sql, ObjetoGlobal.cn) Then

                    If rs1.RecordCount > 0 Then
                        'GrabarLineaLog("")
                        lineaLog = lineaLog & vbCrLf & "Serie;Tipo Doc;Número;Fecha;Pedido;Fecha camión;matrícula;país;matrerial;descripción;desde fecha;hasta fecha;cantidad;peso unidad; unidades material;bultos palet;palet" & vbCrLf
                        'GrabarLineaLog(lineaLog)
                    End If

                    While Not rs1.EOF
                        'CadMat = CadMat + IIf(InStr(CadMat, rs1!descripcion.trim) <> 0, "", " [" & rs1!descripcion.trim & "]")
                        'datos(7) = CadMat
                        sql = "SELECT p.* FROM palets_salidas s LEFT JOIN palets p ON p.empresa = s.empresa AND s.codigo_palet = p.codigo_palet LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa AND o.numero_orden = p.orden_confeccion WHERE s.empresa = '" & rs!empresa & "' AND s.ejercicio = '" & rs!ejercicio & "' AND s.serie = '" & rs!serie & "' AND "
                        sql = sql + " s.tipo_documento = '" & rs!tipo_documento & "' AND s.numero_documento = '" & rs!numero_documento & "' AND s.numero_linea = " & rs1!numero_linea & " AND o.codigo_variedad ='" & rs1!codigo_variedad & "'"
                        If rs2.Open(sql, ObjetoGlobal.cn) Then
                            While Not rs2.EOF
                                If IsDBNull(rs1!hasta_Fecha) Then
                                    'If rs!numero_documento.trim = "00093052" Then
                                    '    MsgBox(rs!numero_documento)
                                    'End If
                                    If rs2!Fecha_confeccion >= rs1!desde_Fecha Then 'Or (rs2!Fecha_confeccion < rs1!desde_Fecha And rs2!Fecha_confeccion < "01/01/2023") Then
                                        CadMat = CadMat + IIf(InStr(CadMat, rs1!descripcion.trim) <> 0, "", " [" & rs1!descripcion.trim & "]")
                                        datos(7) = CadMat
                                        cantidad = cantidad + CDbl((rs1!peso_unidad * rs1!unidades_material * rs1!palets))
                                        SubCantidad = CDbl((rs1!peso_unidad * rs1!unidades_material * rs1!palets))
                                        lineaLog = lineaLog & "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & ";" & CStr("" & rs!no_pedido_cliente).Trim & ";" & CStr("" & rs!fecha_camion) & ";" & rs!matricula & ";" & rs!pais_dest & ";" & rs1!codigo_material & ";" & rs1!descripcion.trim & ";" & rs1!desde_Fecha & ";" & rs1!hasta_Fecha & ";" & SubCantidad & ";" & rs1!peso_unidad & ";" & rs1!unidades_material & ";0;" & rs2!codigo_palet & vbCrLf
                                        'GrabarLineaLog(lineaLog)
                                        TieneAlguno = True
                                    End If
                                Else
                                    If rs2!Fecha_confeccion <= rs1!hasta_Fecha And rs2!Fecha_confeccion >= rs1!desde_Fecha Then 'Or (rs2!Fecha_confeccion < rs1!desde_Fecha And rs2!Fecha_confeccion < "01/01/2023") Then
                                        CadMat = CadMat + IIf(InStr(CadMat, rs1!descripcion.trim) <> 0, "", " [" & rs1!descripcion.trim & "]")
                                        datos(7) = CadMat
                                        cantidad = cantidad + ((rs1!peso_unidad * rs1!unidades_material * rs1!palets))
                                        SubCantidad = CDbl((rs1!peso_unidad * rs1!unidades_material * rs1!palets))
                                        lineaLog = lineaLog & "" & rs!serie & ";" & rs!tipo_documento & ";" & rs!numero_documento & ";" & rs!fecha_documento & ";" & CStr("" & rs!no_pedido_cliente).Trim & ";" & CStr("" & rs!fecha_camion) & ";" & rs!matricula & ";" & rs!pais_dest & ";" & rs1!codigo_material & ";" & rs1!descripcion.trim & ";" & rs1!desde_Fecha & ";" & rs1!hasta_Fecha & ";" & SubCantidad & ";" & rs1!peso_unidad & ";" & rs1!unidades_material & ";0;" & rs2!codigo_palet & vbCrLf
                                        GrabarLineaLog(lineaLog)
                                        TieneAlguno = True
                                    End If
                                End If
                                rs2.MoveNext()
                            End While
                        End If
                        rs2.Close()
                        rs1.MoveNext()
                    End While
                End If
                rs1.Close()
                datos(4) = Replace(Format(Math.Round(cantidad, 3), "######0.000"), ".", ",")
                datos(5) = Replace(Format(Math.Round(cantidad * CDbl(txtTasa.Text), 3), "######0.000"), ".", ",")
                datos(6) = ""
                If TieneAlguno Then
                    Row = New DataGridViewRow
                    Row.Height = 36
                    lineaLog = lineaLog & vbCrLf
                    GrabarLineaLog("")
                    For i = 1 To 10
                        Cell = New DataGridViewTextBoxCell
                        Cell.Value = datos(i)
                        Row.Cells.Add(Cell)
                        lineaLog = lineaLog + "" & datos(i) & ";"
                    Next
                    DGClasificacion.Rows.Add(Row)
                    GrabarLineaLog(lineaLog)
                    lineaLog = ""
                End If
                rs.MoveNext()
            End While
        End If
        CerrarArchivologs()

    End Sub

    Private Sub CabeceraVolcados()

        DGClasificacion().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "campana"
        column1.HeaderText = "Campaña"
        column1.Width = 80
        DGClasificacion().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "pedido"
        column2.HeaderText = "Pedido  Anecoop"
        column2.Width = 120
        column2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGClasificacion().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "albaran"
        column3.HeaderText = "Albarán"
        column3.Width = 150
        column3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGClasificacion().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "cantidad"
        column4.HeaderText = "Cantidad"
        column4.Width = 100
        column4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "importe"
        column5.HeaderText = "Importe"
        column5.Width = 100
        column5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGClasificacion().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "exento"
        column6.HeaderText = "Exento"
        column6.Width = 150
        DGClasificacion().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "descripcion"
        column7.HeaderText = "Descripción"
        column7.Width = 200
        DGClasificacion().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "fecha"
        column8.HeaderText = "Fecha carga"
        column8.Width = 100
        column8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGClasificacion().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "matricula"
        column9.HeaderText = "Matricula"
        column9.Width = 120
        DGClasificacion().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "pais"
        column10.HeaderText = "pais"
        column10.Width = 100
        DGClasificacion().Columns.Add(column10)

    End Sub

    Private Sub FrmPlasticonoreciclado_Load(sender As Object, e As EventArgs) Handles Me.Load
        CabeceraVolcados()
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub PasarExcel_Click(sender As Object, e As EventArgs) Handles PasarExcel.Click
        ExportarDatosExcel(DGClasificacion, "Kilos plasticos no reciblados")
    End Sub




    Public Sub ExportarDatosExcel(ByVal DataGridView1 As DataGridView, ByVal titulo As String)
        Dim m_Excel As New Microsoft.Office.Interop.Excel.Application
        Dim imps As Double
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
                    objCelda.EntireColumn.NumberFormat = c.DefaultCellStyle.Format
                    If c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) Then
                        objCelda.EntireColumn.NumberFormat = "###.##0,000"
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
                        If IsDate(reg.Cells(c.Index).Value) AndAlso reg.Cells(c.Index).Value.ToString.Length >= 8 Then
                            .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", "'" & reg.Cells(c.Index).Value)
                        ElseIf isnumeric(reg.Cells(c.Index).Value) And instr(reg.Cells(c.Index).Value, ",") > 0 Then
                            imps = CDbl(reg.Cells(c.Index).Value) * 1000
                            .Cells(i, strColumna) = "=" & imps & "/1000"
                        Else
                            .Cells(i, strColumna) = IIf(IsDBNull(reg.ToString), "", reg.Cells(c.Index).Value)
                        End If

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

    Private Sub cerrar_Click(sender As Object, e As EventArgs) Handles cerrar.Click
        Me.Close()
    End Sub

    Private Function GeneraArchivoLogs() As Boolean
        '    'Variables para abrir el archivo en modo de escritura

        Try

            If System.IO.File.Exists(TxtArchivoDetalle.Text) Then
                File.Delete(TxtArchivoDetalle.Text)
            End If

            'Se abre el archivo y si este no existe se crea
            strStreamW = File.OpenWrite(TxtArchivoDetalle.Text)
            strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.UTF8)
            Return True

        Catch ex As Exception
            MsgBox("Imposible abrir el archivo de los registros")
            Return False
        End Try

    End Function

    Private Function GrabarLineaLog(cArg) As Boolean

        'Escribimos la línea en el achivo de texto
        strStreamWriter.WriteLine(cArg)
        Return True

    End Function


    Private Function CerrarArchivologs() As Boolean

        Try
            strStreamWriter.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function CuantosPalets(emp, eje, ser, tip, num) As Integer
        Dim sql As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Palets As Integer = 0
        Try
            sql = "SELECT DISTINCT l.numero_linea, l.palets  FROM lineas_salida l "
            sql = Sql + " LEFT JOIN materiales_confec m ON m.empresa = l.empresa AND m.codigo_confeccion = l.codigo_confeccion "
            Sql = Sql + " LEFT JOIN peso_no_reciclado n ON n.empresa = l.empresa And n.codigo_articulo = m.codigo_material "
            sql = sql + " WHERE l.empresa = '" & emp & "' AND l.ejercicio = '" & eje & "' AND l.serie = '" & ser & "' AND "
                            Sql = sql + " l.tipo_documento = '" & tip & "' AND l.numero_documento = '" & num & "' and not n.desde_fecha is null"

            If rs.Open(sql, ObjetoGlobal.cn) Then
                If rs.RecordCount > 0 Then
                    While Not rs.EOF
                        If Not IsDBNull(rs!palets) Then
                            Palets = Palets + rs!palets
                        End If
                        rs.MoveNext()
                    End While
                End If
            End If
            rs.Close()
        Catch ex As Exception

        End Try

        Return Palets

    End Function

    Private Sub BtFicheroDetalle_Click(sender As Object, e As EventArgs) Handles BtFicheroDetalle.Click
        Dim FileDialogo As New SaveFileDialog

        FileDialogo.FileName = TxtArchivoDetalle.Text
        FileDialogo.InitialDirectory = TxtArchivoDetalle.Text
        If FileDialogo.ShowDialog = Windows.Forms.DialogResult.OK Then
            TxtArchivoDetalle.Text = FileDialogo.FileName
        End If
        Return

    End Sub

    Private Sub AbrirDesglose_Click(sender As Object, e As EventArgs)
        Try
            Dim proces As New Process()
            proces.StartInfo.FileName = TxtArchivoDetalle.Text
            proces.Start()

        Catch ex As Exception
            MsgBox("Imposible abrir archivo " & TxtArchivoDetalle.Text.Trim)
        End Try

    End Sub




End Class