Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.ComponentModel
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices



Public Class ControlCamaras
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Private aCampos() As String = {"codigo_palet", "peso_palet", "orden_confeccion", "fecha_confeccion", "calibre_tecnico", "ubicacion", "descripcion", "cliente", "nombre_cliente", "destinatario", "nombre_destinatario"}
    Private aAnchos() As Integer = {75, 70, 70, 150, 70, 70, 150, 80, 225, 80, 225}
    Private aAlineamiento() As String = {"C", "D", "D", "C", "C", "C", "I", "D", "I", "D", "I"}
    Private bCargandoGrid As Boolean

    Public Sub New()
        InitializeComponent()
        ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
        If ObjetoGlobal.Inicializar("") = False Then
            MsgBox("No se puede abrir la conexión a la BD")
        End If
        CargarGrid()

    End Sub

    Private Sub CargarGrid()
        Dim rs As New cnRecordset.CnRecordset
        Dim sQL As String
        Dim Row As DataGridViewRow
        Dim i As Integer
        Dim o As Integer
        Dim totalp As Double
        Dim totalp1 As Double
        Dim totalp2 As Double
        Dim totalp3 As Double
        Dim totalp4 As Double
        Dim totalb As Long
        Dim totalb1 As Long
        Dim totalb2 As Long
        Dim totalb3 As Long
        Dim totalb4 As Long

        If bCargandoGrid Then
            Return
        End If
        bCargandoGrid = True
        cbSel.Enabled = False

        'Dim dt As DataTable = New DataTable("Tabla")
        'Dim dr As DataRow
        sQL = RetornaSQL()

        dgv.Rows.Clear()
        CabeceraGrid()

        If rs.Open(sQL, ObjetoGlobal.cn) Then
            If rs.RecordCount > 0 Then

                For i = 0 To rs.RecordCount + 1
                    Row = New DataGridViewRow
                    Row.Height = 36
                    dgv.Rows.Add(Row)
                Next

                rs.MoveFirst()

                i = -1
                While Not rs.EOF
                    Application.DoEvents()
                    i = i + 1
                    For o = 0 To UBound(aCampos)
                        dgv.Rows(i).Cells.Item(aCampos(o)).Value = Trim(IIf(IsDBNull(rs(aCampos(o))), "", rs(aCampos(o)).ToString))
                    Next
                    If rs!ubicacion = 10 Then
                        totalp1 = totalp1 + rs!peso_palet
                        totalb1 = totalb1 + 1
                    ElseIf rs!ubicacion = 11 Then
                        totalp2 = totalp2 + rs!peso_palet
                        totalb2 = totalb2 + 1
                    ElseIf rs!ubicacion = 12 Then
                        totalp3 = totalp3 + rs!peso_palet
                        totalb3 = totalb3 + 1
                    ElseIf rs!ubicacion = 13 Then
                        totalp4 = totalp4 + rs!peso_palet
                        totalb4 = totalb4 + 1
                    End If
                    rs.MoveNext()
                End While

                totalp = totalp1 + totalp2
                totalb = totalb1 + totalb2
                lblPaletsCamara1.Text = totalb1.ToString
                lblPaletsCamara2.Text = totalb2.ToString
                lblPaletsCamara3.Text = totalb3.ToString
                lblPaletsCamara4.Text = totalb4.ToString

                lblKgsCamara1.Text = totalp1.ToString
                lblKgsCamara2.Text = totalp2.ToString
                lblKgsCamara3.Text = totalp3.ToString
                lblKgsCamara4.Text = totalp4.ToString

                lblPaletsCamaraT.Text = totalb.ToString
                lblKilosCamaraT.Text = totalp.ToString
            End If
        End If
        rs.Close()
        bCargandoGrid = False
        cbSel.Enabled = True

    End Sub
    Private Sub CabeceraGrid()
        Dim i As Integer

        dgv.AutoGenerateColumns = False

        dgv.Columns().Clear()

        'Set Columns Count 
        dgv.ColumnCount = UBound(aCampos) + 1

        For i = 0 To UBound(aCampos)
            dgv.Columns(i).Name = aCampos(i)
            dgv.Columns(i).HeaderText = Replace(aCampos(i), "_", " ")
            dgv.Columns(i).DataPropertyName = aCampos(i)
            'dgv.Columns(i).Width = aAnchos(i)
            Select Case aAlineamiento(i)
                Case "I"
                    dgv.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                Case "D"
                    dgv.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Case "C"
                    dgv.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End Select
        Next
        'Add Columns

        'dgv.Columns(1).Name = "peso_palet"
        '    dgv.Columns(1).HeaderText = "Peso"
        '    dgv.Columns(1).DataPropertyName = "peso_palet"

        '    dgv.Columns(2).Name = "orden_confeccion"
        '    dgv.Columns(2).HeaderText = "Orden confeccion"
        '    dgv.Columns(2).DataPropertyName = "orden_confeccion"

        '    dgv.Columns(3).Name = "fecha_confeccion"
        '    dgv.Columns(3).HeaderText = "fecha confeccion"
        '    dgv.Columns(3).DataPropertyName = "fecha_confeccion"

        '    dgv.Columns(3).Name = "fecha_confeccion"
        '    dgv.Columns(3).HeaderText = "fecha confeccion"
        '    dgv.Columns(3).DataPropertyName = "fecha_confeccion"

        '    dgv.Columns(4).Name = "Calibre"
        '    dgv.Columns(4).HeaderText = "Calibre"
        '    dgv.Columns(4).DataPropertyName = "Calibre"
    End Sub
    Private Function RetornaSQL()
        Dim Sql As String

        Sql = "Select p.empresa,p.codigo_palet,p.orden_confeccion, p.peso_palet,p.fecha_confeccion,p.hora_confeccion, p.ubicacion, u.descripcion, "
        Sql = Sql + "o.codigo_cliente as cliente, c.razon_social as nombre_cliente,  o.codigo_destinatario as destinatario, d.razon_social as nombre_destinatario, o.calibre_tecnico from palets p "
        Sql = Sql + "LEFT JOIN ordenes_confeccion o ON p.empresa=o.empresa AND o.numero_orden = p.orden_confeccion left join ubicaciones_prod u ON u.ubicacion=p.ubicacion "
        Sql = Sql + "LEFT JOIN clientes_coop c ON c.codigo_cliente = o.codigo_cliente LEFT JOIN clientes_coop d ON d.codigo_cliente = o.codigo_destinatario "
        Sql = Sql + "WHERE p.empresa = '1' and fecha_confeccion >='01/07/2022'" ' & ObjetoGlobal.EmpresaActual & "'"

        '{"codigo_palet", "peso_palet", "orden_confeccion", "fecha_confeccion", "Calibre_tecnico", "ubicacion", "descripcion", "codigo_cliente", "nombre_cliente", "codigo_destinatario", "nombre_destinatario"}

        Select Case Strings.Right(Trim(cbSel.Text), 1)
            Case ""
                Sql = Sql + " and p.ubicacion between 10 and 13"
            Case "1"
                Sql = Sql + " and p.ubicacion = 10"
            Case "2"
                Sql = Sql + " and p.ubicacion = 11"
            Case "3"
                Sql = Sql + " and p.ubicacion = 12"
            Case "4"
                Sql = Sql + " and p.ubicacion = 13"
            Case Else
                Sql = Sql + " and p.ubicacion between 10 and 13"
        End Select

        Return Sql

    End Function

    Private Sub cbSel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSel.SelectedIndexChanged
        CargarGrid()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub Vaciarlacamara_Click(sender As Object, e As EventArgs) Handles Vaciarlacamara.Click
        Dim sql As String
        Dim rs As New cnRecordset.CnRecordset
        Dim camara As Integer
        Dim messageBoxText As String
        Dim caption As String

        caption = "Vaciado de cámara"
        messageBoxText = "Esta acción marcará como salidos de la cámara todos los palets"

        Dim result As Integer = MessageBox.Show(messageBoxText, caption, MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            Return
        ElseIf result = DialogResult.Yes Then
            'MessageBox.Show("Yes pressed")
        End If

        'Return

        Select Case Strings.Right(Trim(cbSel.Text), 1)
            Case ""
                camara = 0
            Case "1"
                camara = 10
            Case "2"
                camara = 11
            Case "3"
                camara = 12
            Case "4"
                camara = 13
            Case Else
                camara = 0
        End Select
        If camara = 0 Then
            MsgBox("Por favor, selecciona una cámara para vaciar")
            Return
        End If
        If camara > 0 Then
            sql = "Select * FROM palets WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ubicacion=" & camara
            If rs.Open(sql, ObjetoGlobal.cn, True) Then
                For i = 1 To rs.RecordCount
                    rs.AbsolutePosition = i
                    rs!ubicacion = 0
                    rs.Update()
                Next
                rs.Close()
            End If
        Else
            MsgBox("Hay que indicar el almacen que se ha vaciado")
        End If
    End Sub

    Public Function WriteArray(Sheet As String, ByRef ObjectArray As Object(,)) As String


        Try
            Dim xl As Excel.Application = New Excel.Application
            Dim wb As Excel.Workbook = xl.Workbooks.Add()
            Dim ws As Excel.Worksheet = wb.Worksheets.Add()
            Dim ruta As String = Path.GetTempPath()
            Dim tempfile As String = Path.GetRandomFileName()
            Dim file As String = tempfile.Substring(1, tempfile.IndexOf(".")) & "xlsx"

            ws.Name = Sheet
            Dim range As Excel.Range = ws.Range("A1").Resize(ObjectArray.GetLength(0), ObjectArray.GetLength(1))
            range.Value = ObjectArray

            range = ws.Range("A1").Resize(1, ObjectArray.GetLength(1) - 1)

            range.Interior.Color = RGB(0, 70, 132)  'Con-way Blue
            range.Font.Color = RGB(Drawing.Color.White.R, Drawing.Color.White.G, Drawing.Color.White.B)
            range.Font.Bold = True
            range.WrapText = True

            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter

            range.Application.ActiveWindow.SplitColumn = 0
            range.Application.ActiveWindow.SplitRow = 1
            range.Application.ActiveWindow.FreezePanes = True

            wb.SaveAs(ruta & file)
            wb.Close()
            xl.Quit()
            xl = Nothing
            wb = Nothing
            ws = Nothing
            range = Nothing
            Marshal.ReleaseComObject(xl)
            Marshal.ReleaseComObject(wb)
            Marshal.ReleaseComObject(ws)
            Marshal.ReleaseComObject(range)
            Return ""
        Catch ex As Exception
            Return "WriteArray()" & Environment.NewLine & Environment.NewLine & ex.Message
        End Try
    End Function

    Public Function WriteDataGrid(SheetName As String, ByRef dt As DataGridView) As String
        Try
            Dim l(dt.Rows.Count + 1, dt.Columns.Count) As Object
            For c As Integer = 0 To dt.Columns.Count - 1
                l(0, c) = dt.Columns(c).HeaderText
            Next

            For r As Integer = 1 To dt.Rows.Count
                For c As Integer = 0 To dt.Columns.Count - 1
                    l(r, c) = dt.Rows(r - 1).Cells(c)
                Next
            Next

            Dim errors As String = WriteArray(SheetName, l)
            If errors <> "" Then
                Return errors
            End If

            Return ""
        Catch ex As Exception
            Return "WriteDataGrid()" & Environment.NewLine & Environment.NewLine & ex.Message
        End Try
    End Function


    Public Function WriteDataTable(SheetName As String, ByRef dt As DataTable) As String
        Try
            Dim l(dt.Rows.Count + 1, dt.Columns.Count) As Object
            For c As Integer = 0 To dt.Columns.Count - 1
                l(0, c) = dt.Columns(c).ColumnName
            Next

            For r As Integer = 1 To dt.Rows.Count
                For c As Integer = 0 To dt.Columns.Count - 1
                    l(r, c) = dt.Rows(r - 1).Item(c)
                Next
            Next

            Dim errors As String = WriteArray(SheetName, l)
            If errors <> "" Then
                Return errors
            End If

            Return ""
        Catch ex As Exception
            Return "WriteDataTable()" & Environment.NewLine & Environment.NewLine & ex.Message
        End Try
    End Function

    Private Sub ExpExcel_Click(sender As Object, e As EventArgs) Handles ExpExcel.Click
        ExportarDatosExcel(dgv, "Palets cámara")
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
            Dim objRango As Microsoft.Office.Interop.Excel.Range = .Range(primeraLetra + primerNumero.ToString, UltimaLetraIzq + UltimaLetra + (UltimoNumero - 1).ToString)
            objRango.Select()
            objRango.Columns.AutoFit()
            objRango.Columns.BorderAround(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium)
        End With

        m_Excel.Cursor = Microsoft.Office.Interop.Excel.XlMousePointer.xlDefault

    End Sub

End Class
