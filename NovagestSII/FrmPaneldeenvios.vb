Imports System.Drawing.KnownColor

Public Class FrmPaneldeenvios
    Inherits libcomunes.FormGenerico
    Public objetoGlobal As ObjetoGlobal.ObjetoGlobal

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub CmdCargar_Click(sender As Object, e As EventArgs) Handles CmdCargar.Click

        If "" & DTPDesdefecha.Text.Trim = "" Or "" & DTPHastafecha.Text.Trim = "" Then
            MsgBox("Hay que indicar la fecha de inicio y de final")
            Return
        End If
        Cabecera()
        CargarLineas()
    End Sub

    Private Sub Cabecera()


        Msf().Columns().Clear()

        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "empresa"
        column1.HeaderText = "Empresa"
        column1.Width = 50
        Msf().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "entrada"
        column2.HeaderText = "Num. entrada"
        column2.Width = 60
        Msf().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "serie"
        column3.HeaderText = "Serie"
        column3.Width = 60
        Msf().Columns.Add(column3)


        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "numero"
        column4.HeaderText = "Número"
        column4.Width = 75
        Msf().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "fechaf"
        column5.HeaderText = "Fecha factura"
        column5.Width = 75
        Msf().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "envio"
        column6.HeaderText = "Envío"
        column6.Width = 60
        Msf().Columns.Add(column6)

        Dim column7 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column7.Name = "CliPro"
        column7.HeaderText = "Clie/Prov"
        column7.Width = 65
        Msf().Columns.Add(column7)

        Dim column8 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column8.Name = "nif"
        column8.HeaderText = "NIF"
        column8.Width = 50

        Msf().Columns.Add(column8)

        Dim column9 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column9.Name = "nombre"
        column9.HeaderText = "Nombre/Razón"
        column9.Width = 265
        Msf().Columns.Add(column9)

        Dim column10 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column10.Name = "base"
        column10.HeaderText = "Base"
        column10.Width = 70
        Msf().Columns.Add(column10)

        Dim column11 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column11.Name = "tipo"
        column11.HeaderText = "Tipo %"
        column11.Width = 10
        Msf().Columns.Add(column11)

        Dim column12 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column12.Name = "cuota"
        column12.HeaderText = "Cuota"
        column12.Width = 65
        Msf().Columns.Add(column12)

        Dim column13 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column13.Name = "situacion"
        column13.HeaderText = "Situación"
        column13.Width = 65
        Msf().Columns.Add(column13)

        Dim column14 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column14.Name = "csv"
        column14.HeaderText = "CSV"
        column14.Width = 65
        Msf().Columns.Add(column14)

        Dim column15 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column15.Name = "error"
        column15.HeaderText = "Descripción error"
        column15.Width = 65
        Msf().Columns.Add(column15)


    End Sub
    Public Sub CargarGrid(ByRef oDGV As DataGridView, ByRef datos() As String)
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

    Private Sub CargarLineas()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim no As Integer
        Dim Bases(5) As Double
        Dim tipos(5) As Double
        Dim Cuotas(5) As Double
        Dim i As Integer
        Dim n As Integer
        Dim max As Integer
        Dim Esta As Boolean
        Dim docs(3) As Integer
        Dim nfila As Long
        Dim situaciones As Boolean
        Dim HayTipoCero As Boolean
        Dim lblBase(5) As Object
        Dim lbltipo(5) As Object
        Dim lblCuota(5) As Object
        Dim lblDocumentos(4) As Object
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim Datos As List(Of ElementoEnvio) = New List(Of ElementoEnvio)
        Dim LiColor As Color
        Dim FoColor As Color
        Dim fi As Integer = 0

        HayTipoCero = False

        For i = 0 To 5
            Bases(i) = 0 : tipos(i) = 0 : Cuotas(i) = 0
        Next

        For i = 0 To 3
            docs(i) = 0
        Next

        lblBase(0) = lblbase1
        lblBase(1) = lblbase2
        lblBase(2) = lblbase3
        lblBase(3) = lblbase4
        lblBase(4) = lblbase5

        lbltipo(0) = lbltipo1
        lbltipo(1) = lbltipo2
        lbltipo(2) = lbltipo3
        lbltipo(3) = lbltipo4
        lbltipo(4) = lbltipo5

        lblCuota(0) = lblcuota1
        lblCuota(1) = lblcuota2
        lblCuota(2) = lblcuota3
        lblCuota(3) = lblcuota4
        lblCuota(4) = lblcuota5

        lblDocumentos(0) = lbldocumento1
        lblDocumentos(1) = lbldocumento2
        lblDocumentos(2) = lbldocumento3
        lblDocumentos(3) = lbldocumento4

        For i = 0 To 4
            lblBase(i).Visible = False : lbltipo(i).Visible = False : lblCuota(i).Visible = False
            lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
        Next

        If Option1.Checked = True Then
            If Openvio.Checked Then
                Sql = "SELECT * FROM factura_vta_aeat WHERE fecha_envio BETWEEN '" & CDate(DTPDesdefecha.Text) & "' AND '" & CDate(DTPHastafecha.Text) & "' "
            Else
                Sql = "SELECT * FROM factura_vta_aeat WHERE fecha_factura BETWEEN '" & CDate(DTPDesdefecha.Text) & "' AND '" & CDate(DTPHastafecha.Text) & "' "
            End If
            If CkPendientes.Checked Then
                Sql = Sql + " AND ( situacion = 'P'"
                situaciones = True
            End If
            If ckaceptadas.Checked Then
                Sql = Sql + IIf(situaciones, " OR ", " AND ( ") & "situacion = 'C'"
                situaciones = True
            End If
            If ckconerrores.Checked Then
                Sql = Sql + IIf(situaciones, " OR ", " AND ( ") & "situacion = 'A'"
                situaciones = True
            End If
            If ckerroneas.Checked Then
                Sql = Sql + IIf(situaciones, " OR ", " AND ( ") & "situacion = 'E'"
                situaciones = True
            End If
            Sql = Sql + IIf(situaciones, " )", "")
            Sql = Sql + " ORDER BY fecha_factura "
        Else
            If Openvio.Checked Then
                Sql = "SELECT * FROM factura_com_aeat WHERE fecha_envio BETWEEN '" & CDate(DTPDesdefecha.Text) & "' AND '" & CDate(DTPHastafecha.Text) & "' "
            Else
                Sql = "SELECT * FROM factura_com_aeat WHERE fecha_registro BETWEEN '" & CDate(DTPDesdefecha.Text) & "' AND '" & CDate(DTPHastafecha.Text) & "' "
            End If

            If CkPendientes.Checked Then
                Sql = Sql + " AND ( situacion = 'P'"
                situaciones = True
            End If
            If ckaceptadas.Checked Then
                Sql = Sql + IIf(situaciones, " OR ", " AND ( ") & "situacion = 'C'"
                situaciones = True
            End If
            If ckconerrores.Checked Then
                Sql = Sql + IIf(situaciones, " OR ", " AND ( ") & "situacion = 'A'"
                situaciones = True
            End If
            If ckerroneas.Checked Then
                Sql = Sql + IIf(situaciones, " OR ", " AND ( ") & "situacion = 'E'"
                situaciones = True
            End If
            Sql = Sql + IIf(situaciones, " )", "")
            Sql = Sql + " ORDER BY fecha_registro "
        End If
        Rs.Open(Sql, objetoGlobal.cn)



        If Option1.Checked Then
            max = -1
            While Not Rs.EOF

                fi = IIf(fi = 0, 1, 0)

                If Rs!Situacion = "P" Then
                    docs(0) = docs(0) + 1
                ElseIf Rs!Situacion = "C" Then
                    docs(1) = docs(1) + 1
                ElseIf Rs!Situacion = "A" Then
                    docs(2) = docs(2) + 1
                ElseIf Rs!Situacion = "E" Then
                    docs(3) = docs(3) + 1
                End If

                LiColor = New Color()
                FoColor = New Color()
                FoColor = (IIf(fi = 0, Color.White, Color.WhiteSmoke))

                For i = 0 To 14
                    If Rs!Situacion = "C" Then
                        LiColor = Color.Green
                    ElseIf Rs!Situacion = "E" Then
                        LiColor = Color.Red
                    ElseIf Rs!Situacion = "A" Then
                        LiColor = Color.Gold
                    Else
                        LiColor = Color.WhiteSmoke
                    End If
                Next

                Sql = "SELECT c.empresa, c.serie_factura_vta, c.numero_factura, c.razon_social, t.tipo_linea, t.base, t.porcentaje, t.importe_cargo FROM factura_vta_c c "
                Sql = Sql + " LEFT JOIN factura_vta_c_tot t ON t.empresa = c.empresa AND c.serie_factura_vta = t.serie_factura_vta AND c.numero_factura = t.numero_factura "
                Sql = Sql + " WHERE c.empresa = '" & Rs!empresa & "' AND c.serie_factura_vta ='" & Rs!serie_factura_vta & "' AND c.numero_factura=" & Rs!numero_factura & " AND t.tipo_linea = 'I'"
                rs1.Open(Sql, objetoGlobal.cn)

                no = 0
                While Not rs1.EOF
                    no = no + 1
                    If no > 1 Then
                        Datos.Add(New ElementoEnvio(rs1!base, rs1!porcentaje, rs1!importe_cargo, LiColor, FoColor))
                    Else
                        Datos.Add(New ElementoEnvio(Trim(Rs!empresa), "", Trim("" & Rs!serie_factura_vta), Trim("" & Rs!numero_factura), Trim("" & Rs!fecha_factura), Trim("" & Rs!numero_envio),
                                                    Trim("" & Rs!codigo_cliente), Trim("" & Rs!cif), Trim("" & rs1!razon_social), rs1!base, rs1!porcentaje, rs1!importe_cargo,
                                                    Trim("" & Rs!Situacion), Trim("" & Rs!csv), "" & Rs!cod_error & " - " & Rs!des_error, LiColor, FoColor))
                    End If

                    Esta = False
                    For i = 0 To 4
                        If tipos(i) = rs1!porcentaje Then
                            If rs1!porcentaje = 0 And rs1!Base <> 0 And Not HayTipoCero Then
                                HayTipoCero = True
                                max = max + 1
                            End If
                            Bases(i) = Bases(i) + rs1!Base
                            Cuotas(i) = Cuotas(i) + rs1!importe_cargo
                            Esta = True
                            Exit For
                        End If
                    Next
                    If Not Esta Then
                        max = max + 1
                        Bases(max) = rs1!Base
                        tipos(max) = rs1!porcentaje
                        Cuotas(max) = rs1!importe_cargo
                    End If
                    rs1.MoveNext()
                End While
                rs1.Close()

                Rs.MoveNext()
            End While
        Else
            max = -1
            While Not Rs.EOF

                fi = IIf(fi = 0, 1, 0)

                If Rs!Situacion = "P" Then
                    docs(0) = docs(0) + 1
                ElseIf Rs!Situacion = "C" Then
                    docs(1) = docs(1) + 1
                ElseIf Rs!Situacion = "A" Then
                    docs(2) = docs(2) + 1
                ElseIf Rs!Situacion = "E" Then
                    docs(3) = docs(3) + 1
                End If

                FoColor = New Color()
                FoColor = IIf(fi = 0, Color.White, Color.WhiteSmoke)


                For i = 0 To 14
                    If Rs!Situacion = "C" Then
                        LiColor = Color.Green
                    ElseIf Rs!Situacion = "E" Then
                        LiColor = Color.Red
                    ElseIf Rs!Situacion = "A" Then
                        LiColor = Color.Gold
                    Else
                        LiColor = Color.Black
                    End If
                Next

                Sql = "SELECT c.empresa, c.numero_entrada_fra, c.razon_social, t.tipo_linea, t.base, t.porcentaje, t.importe_cargo FROM factura_com_c c "
                Sql = Sql + " LEFT JOIN factura_com_c_tot t ON t.empresa = c.empresa AND c.numero_entrada_fra = t.numero_entrada_fra "
                Sql = Sql + " WHERE c.empresa = '" & Rs!empresa & "' AND c.numero_entrada_fra =" & Rs!numero_entrada & " AND t.tipo_linea = 'I'"
                rs1.Open(Sql, objetoGlobal.cn)

                no = 0
                While Not rs1.EOF
                    no = no + 1
                    If no > 1 Then
                        Datos.Add(New ElementoEnvio(rs1!base, rs1!porcentaje, rs1!importe_cargo, LiColor, FoColor))
                    Else
                        Datos.Add(New ElementoEnvio(Trim(Rs!empresa), "", Trim("" & Rs!serie_factura_com), Trim("" & Rs!numero_fra_com), Trim("" & Rs!fecha_factura), Trim("" & Rs!numero_envio),
                                                    Trim("" & Rs!codigo_proveedor), Trim("" & Rs!cif_prov), Trim("" & rs1!razon_social), rs1!base, rs1!porcentaje, rs1!importe_cargo,
                                                    Trim("" & Rs!Situacion), Trim("" & Rs!csv), "" & Rs!cod_error & " - " & Rs!des_error, LiColor, FoColor))
                    End If

                    Esta = False
                    For i = 0 To 4
                        If tipos(i) = rs1!porcentaje Then
                            If rs1!porcentaje = 0 And rs1!Base <> 0 And Not HayTipoCero Then
                                HayTipoCero = True
                                max = max + 1
                            End If
                            Bases(i) = Bases(i) + rs1!Base
                            Cuotas(i) = Cuotas(i) + rs1!importe_cargo
                            Esta = True
                            Exit For
                        End If
                    Next
                    If Not Esta Then
                        max = max + 1
                        Bases(max) = rs1!Base
                        tipos(max) = rs1!porcentaje
                        Cuotas(max) = rs1!importe_cargo
                    End If
                    rs1.MoveNext()
                End While
                rs1.Close()
                Rs.MoveNext()
            End While
        End If

        For i = 0 To 3
            lblDocumentos(i).text = docs(i).ToString("##,###")
        Next

        If max >= 0 Then
            For i = 0 To max
                lblBase(i).Visible = True : lbltipo(i).Visible = True : lblCuota(i).Visible = True
                lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
            Next
        End If

        i = 0

        For Each OEE As ElementoEnvio In Datos

            i = i + 1

            Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Em
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo

            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.En
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Se
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Nu
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Fe
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ev
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.CP
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.CI
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ra
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ba
            Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo

            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ti
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Cu
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.CI
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.CV
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo

            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Er
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Row.DefaultCellStyle.BackColor = OEE.ColorFondo
            Row.DefaultCellStyle.ForeColor = OEE.Situacion
            Msf.Rows.Add(Row)

        Next

    End Sub

    Private Sub cmdrevisaremitidas_Click(sender As Object, e As EventArgs) Handles cmdrevisaremitidas.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim no As Integer
        Dim Bases(5) As Double
        Dim tipos(5) As Double
        Dim Cuotas(5) As Double
        Dim i As Integer
        Dim n As Integer
        Dim max As Integer
        Dim Esta As Boolean
        Dim docs(3) As Integer
        Dim HayTipoCero As Boolean
        Dim lblBase(5) As Object
        Dim lbltipo(5) As Object
        Dim lblCuota(5) As Object
        Dim lblDocumentos(4) As Object
        Dim Datos As List(Of ElementoEnvio) = New List(Of ElementoEnvio)
        Dim LiColor As Color
        Dim ficolor As Color
        Dim fi As Integer = 0

        If "" & DTPDesdefecha.Text.Trim = "" Or "" & DTPHastafecha.Text.Trim = "" Then
            MsgBox("Hay que indicar la fecha de inicio y de final")
            Return
        End If


        HayTipoCero = False

        For i = 0 To 5
            Bases(i) = 0 : tipos(i) = 0 : Cuotas(i) = 0
        Next

        For i = 0 To 3
            docs(i) = 0
        Next

        Cabecera()

        lblBase(0) = lblbase1
        lblBase(1) = lblbase2
        lblBase(2) = lblbase3
        lblBase(3) = lblbase4
        lblBase(4) = lblbase5

        lbltipo(0) = lbltipo1
        lbltipo(1) = lbltipo2
        lbltipo(2) = lbltipo3
        lbltipo(3) = lbltipo4
        lbltipo(4) = lbltipo5

        lblCuota(0) = lblcuota1
        lblCuota(1) = lblcuota2
        lblCuota(2) = lblcuota3
        lblCuota(3) = lblcuota4
        lblCuota(4) = lblcuota5

        lblDocumentos(0) = lbldocumento1
        lblDocumentos(1) = lbldocumento2
        lblDocumentos(2) = lbldocumento3
        lblDocumentos(3) = lbldocumento4

        For i = 0 To 4
            lblBase(i).Visible = False : lbltipo(i).Visible = False : lblCuota(i).Visible = False
            lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
        Next

        Sql = "SELECT * FROM Factura_vta_c WHERE fecha_factura between '" & CDate(DTPDesdefecha.value) & "' AND '" & CDate(DTPHastafecha.value) & "' AND CIF <> 'F46026068' AND "
        Sql = Sql + "NOT EXISTS (select * FROM factura_vta_aeat WHERE factura_vta_aeat.empresa = Factura_vta_c.empresa AND factura_vta_aeat.serie_factura_vta = Factura_vta_c.serie_factura_vta AND "
        Sql = Sql + "factura_vta_aeat.numero_factura = Factura_vta_c.numero_factura) ORDER BY fecha_factura"
        Rs.Open(Sql, objetoGlobal.cn)
        max = -1

        LiColor = New Color()
        LiColor = Color.Black

        ficolor = New Color()
        ficolor = IIf(fi = 0, Color.White, Color.WhiteSmoke)

        While Not Rs.EOF

            Sql = "SELECT c.empresa, c.serie_factura_vta, c.numero_factura, c.razon_social, t.tipo_linea, t.base, t.porcentaje, t.importe_cargo FROM factura_vta_c c "
            Sql = Sql + " LEFT JOIN factura_vta_c_tot t ON t.empresa = c.empresa AND c.serie_factura_vta = t.serie_factura_vta AND c.numero_factura = t.numero_factura "
            Sql = Sql + " WHERE c.empresa = '" & Rs!empresa & "' AND c.serie_factura_vta ='" & Rs!serie_factura_vta & "' AND c.numero_factura=" & Rs!numero_factura & " AND t.tipo_linea = 'I'"
            rs1.Open(Sql, objetoGlobal.cn)
            no = 0

            fi = IIf(fi = 0, 1, 0)
            ficolor = IIf(fi = 0, Color.White, Color.WhiteSmoke)

            While Not rs1.EOF
                no = no + 1
                If no > 1 Then
                    Datos.Add(New ElementoEnvio(rs1!base, rs1!porcentaje, rs1!importe_cargo, LiColor, ficolor))
                Else
                    Datos.Add(New ElementoEnvio(Trim(Rs!empresa), "", Trim("" & Rs!serie_factura_vta), Trim("" & Rs!numero_factura), Trim("" & Rs!fecha_factura), "",
                                                    Trim("" & Rs!codigo_cliente), Trim("" & Rs!cif), Trim("" & rs1!razon_social), rs1!base, rs1!porcentaje, rs1!importe_cargo,
                                                    "", "", "", LiColor, ficolor))
                End If
                Esta = False
                For i = 0 To 4
                    If tipos(i) = rs1!porcentaje Then
                        If rs1!porcentaje = 0 And rs1!Base <> 0 And Not HayTipoCero Then
                            HayTipoCero = True
                            max = max + 1
                        End If
                        Bases(i) = Bases(i) + rs1!Base
                        Cuotas(i) = Cuotas(i) + rs1!importe_cargo
                        Esta = True
                        Exit For
                    End If
                Next
                If Not Esta Then
                    max = max + 1
                    Bases(max) = rs1!Base
                    tipos(max) = rs1!porcentaje
                    Cuotas(max) = rs1!importe_cargo
                End If
                rs1.MoveNext()
            End While
            rs1.Close()
            Rs.MoveNext()
        End While

        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell

        For Each OEE As ElementoEnvio In Datos

                i = i + 1

                Row = New DataGridViewRow
                Cell = New DataGridViewTextBoxCell

                Cell.Value = OEE.Em
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo

                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.En
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Se
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Nu
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Fe
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ev
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.CP
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.CI
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ra
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ba
                Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo

                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ti
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Cu
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Row.Cells.Add(Cell)

            Row.DefaultCellStyle.BackColor = OEE.ColorFondo
            Row.DefaultCellStyle.ForeColor = OEE.Situacion
            Msf.Rows.Add(Row)

        Next
        Rs.Close()

        If max >= 0 Then
            For i = 0 To max
                lblBase(i).Visible = True : lbltipo(i).Visible = True : lblCuota(i).Visible = True
                lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
            Next
        End If

    End Sub

    Private Sub cmdrecisarrecibidas_Click(sender As Object, e As EventArgs) Handles cmdrecisarrecibidas.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim no As Integer
        Dim Bases(5) As Double
        Dim tipos(5) As Double
        Dim Cuotas(5) As Double
        Dim i As Integer
        Dim n As Integer
        Dim max As Integer
        Dim Esta As Boolean
        Dim docs(3) As Integer
        Dim nfila As Long
        Dim situaciones As Boolean
        Dim HayTipoCero As Boolean
        Dim lblBase(5) As Object
        Dim lbltipo(5) As Object
        Dim lblCuota(5) As Object
        Dim lblDocumentos(4) As Object
        Dim Datos As List(Of ElementoEnvio) = New List(Of ElementoEnvio)
        Dim LiColor As Color
        Dim fiColor As Color
        Dim fi As Integer = 0

        If "" & DTPDesdefecha.Text.Trim = "" Or "" & DTPHastafecha.Text.Trim = "" Then
            MsgBox("Hay que indicar la fecha de inicio y de final")
            Return
        End If

        For i = 0 To 5
            Bases(i) = 0 : tipos(i) = 0 : Cuotas(i) = 0
        Next

        For i = 0 To 3
            docs(i) = 0
        Next

        Cabecera()

        lblBase(0) = lblbase1
        lblBase(1) = lblbase2
        lblBase(2) = lblbase3
        lblBase(3) = lblbase4
        lblBase(4) = lblbase5

        lbltipo(0) = lbltipo1
        lbltipo(1) = lbltipo2
        lbltipo(2) = lbltipo3
        lbltipo(3) = lbltipo4
        lbltipo(4) = lbltipo5

        lblCuota(0) = lblcuota1
        lblCuota(1) = lblcuota2
        lblCuota(2) = lblcuota3
        lblCuota(3) = lblcuota4
        lblCuota(4) = lblcuota5

        lblDocumentos(0) = lbldocumento1
        lblDocumentos(1) = lbldocumento2
        lblDocumentos(2) = lbldocumento3
        lblDocumentos(3) = lbldocumento4

        For i = 0 To 4
            lblBase(i).Visible = False : lbltipo(i).Visible = False : lblCuota(i).Visible = False
            lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
        Next

        Sql = "SELECT * FROM factura_com_c WHERE fecha_registro between '" & CDate(DTPDesdefecha.value) & "' AND '" & CDate(DTPHastafecha.value) & "' AND cif_prov <> 'F46026068' AND "
        Sql = Sql + "NOT EXISTS (select * FROM factura_com_aeat WHERE factura_com_aeat.empresa = Factura_com_c.empresa AND factura_com_aeat.numero_entrada = Factura_com_c.numero_entrada_fra ) ORDER BY fecha_registro"

        Rs.Open(Sql, objetoGlobal.cn)

        LiColor = New Color()
        LiColor = Color.Black

        fiColor = New Color()

        max = -1
        While Not Rs.EOF
            no = 0
            Sql = "SELECT c.empresa, c.numero_entrada_fra, c.serie_factura_com, c.numero_fra_com, c.fecha_factura, c.codigo_proveedor, c.razon_social, c.cif_prov, t.tipo_linea, t.base, t.porcentaje, t.importe_cargo FROM factura_com_c c "
            Sql = Sql + " LEFT JOIN factura_com_c_tot t ON t.empresa = c.empresa AND c.numero_entrada_fra = t.numero_entrada_fra "
            Sql = Sql + " WHERE c.empresa = '" & Rs!empresa & "' AND c.numero_entrada_fra =" & Rs!numero_entrada_fra & " AND t.tipo_linea = 'I'"
            rs1.Open(Sql, objetoGlobal.cn)

            fi = IIf(fi = 0, 1, 0)
            fiColor = IIf(fi = 0, Color.White, Color.WhiteSmoke)

            While Not rs1.EOF

                no = no + 1
                If no > 1 Then
                    Datos.Add(New ElementoEnvio(rs1!base, rs1!porcentaje, rs1!importe_cargo, LiColor, fiColor))
                Else
                    Datos.Add(New ElementoEnvio(Trim(Rs!empresa), Rs!numero_entrada_fra, Trim("" & Rs!serie_factura_com), Trim("" & Rs!numero_fra_com), Trim("" & Rs!fecha_factura), "",
                                                    Trim("" & Rs!codigo_proveedor), Trim("" & Rs!cif_prov), Trim("" & rs1!razon_social), rs1!base, rs1!porcentaje, rs1!importe_cargo,
                                                    "", "", "", LiColor, fiColor))
                End If
                Esta = False

                For i = 0 To 4
                    If tipos(i) = rs1!porcentaje Then
                        If rs1!porcentaje = 0 And rs1!Base <> 0 And Not HayTipoCero Then
                            HayTipoCero = True
                            max = max + 1
                        End If
                        Bases(i) = Bases(i) + rs1!Base
                        Cuotas(i) = Cuotas(i) + rs1!importe_cargo
                        Esta = True
                        Exit For
                    End If
                Next
                If Not Esta Then
                    max = max + 1
                    Bases(max) = rs1!Base
                    tipos(max) = rs1!porcentaje
                    Cuotas(max) = rs1!importe_cargo
                End If
                rs1.MoveNext()
            End While
            rs1.Close()
            Rs.MoveNext()
        End While

        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell

        For Each OEE As ElementoEnvio In Datos

            i = i + 1

            Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell

            Cell.Value = OEE.Em
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo

            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.En
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Se
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Nu
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Fe
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ev
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.CP
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.CI
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ra
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ba
            Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo

            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Ti
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Row.Cells.Add(Cell)

            Cell = New DataGridViewTextBoxCell
            Cell.Value = OEE.Cu
            Cell.Style.ForeColor = OEE.Situacion
            Cell.Style.BackColor = OEE.ColorFondo
            Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            Row.Cells.Add(Cell)

            Row.DefaultCellStyle.BackColor = OEE.ColorFondo
            Row.DefaultCellStyle.ForeColor = OEE.Situacion
            Msf.Rows.Add(Row)

        Next


        If max >= 0 Then
            For i = 0 To max
                lblBase(i).Visible = True : lbltipo(i).Visible = True : lblCuota(i).Visible = True
                lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
            Next
        End If


    End Sub



    Private Sub EmitirRecibidas()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim no As Integer
        Dim Bases(5) As Double
        Dim tipos(5) As Double
        Dim Cuotas(5) As Double
        Dim i As Integer
        Dim n As Integer
        Dim max As Integer
        Dim Esta As Boolean
        Dim docs(3) As Integer
        Dim nfila As Long
        Dim HayTipoCero As Boolean
        Dim lblBase(5) As Object
        Dim lbltipo(5) As Object
        Dim lblCuota(5) As Object
        Dim lblDocumentos(4) As Object
        Dim Datos As List(Of ElementoEnvio) = New List(Of ElementoEnvio)
        Dim LiColor As Color
        Dim FiColor As Color
        Dim fi As Integer = 0

        Cabecera()

        For i = 0 To 5
            Bases(i) = 0 : tipos(i) = 0 : Cuotas(i) = 0
        Next

        For i = 0 To 3
            docs(i) = 0
        Next

        Cabecera()

        lblBase(0) = lblbase1
        lblBase(1) = lblbase2
        lblBase(2) = lblbase3
        lblBase(3) = lblbase4
        lblBase(4) = lblbase5

        lbltipo(0) = lbltipo1
        lbltipo(1) = lbltipo2
        lbltipo(2) = lbltipo3
        lbltipo(3) = lbltipo4
        lbltipo(4) = lbltipo5

        lblCuota(0) = lblcuota1
        lblCuota(1) = lblcuota2
        lblCuota(2) = lblcuota3
        lblCuota(3) = lblcuota4
        lblCuota(4) = lblcuota5

        lblDocumentos(0) = lbldocumento1
        lblDocumentos(1) = lbldocumento2
        lblDocumentos(2) = lbldocumento3
        lblDocumentos(3) = lbldocumento4

        For i = 0 To 5
            lblBase(i).Visible = False : lbltipo(i).Visible = False : lblCuota(i).Visible = False
            lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
        Next

        Sql = "SELECT * FROM factura_com_c WHERE bloque_liquid = '" & Trim(Txtperiodo.Text) & "' AND cif_prov <> 'F46026068' AND "
        Sql = Sql + "EXISTS (select * FROM factura_com_aeat WHERE factura_com_aeat.empresa = Factura_com_c.empresa AND factura_com_aeat.numero_entrada = Factura_com_c.numero_entrada_fra ) ORDER BY fecha_registro"

        Rs.Open(Sql, objetoGlobal.cn)

        LiColor = New Color()
        LiColor = Color.Black

        FiColor = New Color()

        Rs.Open(Sql, objetoGlobal.cn)

        max = -1
        While Not Rs.EOF
            no = 0
            Sql = "SELECT c.empresa, c.numero_entrada_fra, c.serie_factura_com, c.numero_fra_com, c.fecha_factura, c.codigo_proveedor, c.razon_social, c.cif_prov, t.tipo_linea, t.base, t.porcentaje, t.importe_cargo FROM factura_com_c c "
            Sql = Sql + " LEFT JOIN factura_com_c_tot t ON t.empresa = c.empresa AND c.numero_entrada_fra = t.numero_entrada_fra "
            Sql = Sql + " WHERE c.empresa = '" & Rs!empresa & "' AND c.numero_entrada_fra =" & Rs!numero_entrada_fra & " AND t.tipo_linea = 'I'"
            rs1.Open(Sql, objetoGlobal.cn)

            fi = IIf(fi = 0, 1, 0)
            FiColor = IIf(fi = 0, Color.White, Color.WhiteSmoke)

            no = 0
            While Not rs1.EOF

                no = no + 1
                If no > 1 Then
                    Datos.Add(New ElementoEnvio(rs1!base, rs1!porcentaje, rs1!importe_cargo, LiColor, FiColor))
                Else
                    Datos.Add(New ElementoEnvio(Trim(Rs!empresa), "", Trim("" & Rs!serie_factura_com), Trim("" & Rs!numero_fra_com), Trim("" & Rs!fecha_factura), Trim("" & Rs!numero_envio),
                                                    Trim("" & Rs!codigo_proveedor), Trim("" & Rs!cif_prov), Trim("" & rs1!razon_social), rs1!base, rs1!porcentaje, rs1!importe_cargo,
                                                    Trim("" & Rs!Situacion), Trim("" & Rs!csv), "" & Rs!cod_error & " - " & Rs!des_error, LiColor, FiColor))
                End If

                For i = 0 To 4
                    If tipos(i) = rs1!porcentaje Then
                        If rs1!porcentaje = 0 And rs1!Base <> 0 And Not HayTipoCero Then
                            HayTipoCero = True
                            max = max + 1
                        End If
                        Bases(i) = Bases(i) + rs1!Base
                        Cuotas(i) = Cuotas(i) + rs1!importe_cargo
                        Esta = True
                        Exit For
                    End If
                Next
                If Not Esta Then
                    max = max + 1
                    Bases(max) = rs1!Base
                    tipos(max) = rs1!porcentaje
                    Cuotas(max) = rs1!importe_cargo
                End If
                rs1.MoveNext()
            End While

            rs1.Close()

            Dim Row As DataGridViewRow
            Dim Cell As DataGridViewTextBoxCell

            For Each OEE As ElementoEnvio In Datos

                i = i + 1

                Row = New DataGridViewRow
                Cell = New DataGridViewTextBoxCell

                Cell.Value = OEE.Em
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo

                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.En
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Se
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Nu
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Fe
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ev
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.CP
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.CI
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ra
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ba
                Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo

                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Ti
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Cu
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Cell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.CI
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.CV
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo

                Row.Cells.Add(Cell)

                Cell = New DataGridViewTextBoxCell
                Cell.Value = OEE.Er
                Cell.Style.ForeColor = OEE.Situacion
                Cell.Style.BackColor = OEE.ColorFondo
                Row.Cells.Add(Cell)

                Row.DefaultCellStyle.BackColor = OEE.ColorFondo
                Row.DefaultCellStyle.ForeColor = OEE.Situacion
                Msf.Rows.Add(Row)
            Next
            Rs.MoveNext()
        End While

        If max >= 0 Then
            For i = 0 To max
                lblBase(i).Visible = True : lbltipo(i).Visible = True : lblCuota(i).Visible = True
                lblBase(i).text = Bases(i).ToString("###,###,##0.00") : lbltipo(i).text = tipos(i) : lblCuota(i).text = Cuotas(i).ToString("###,###,##0.00")
            Next
        End If


    End Sub

    Private Sub CmdEmitir_Click(sender As Object, e As EventArgs) Handles CmdEmitir.Click
        If Trim(Txtperiodo.Text) = "" Then Exit Sub
        EmitirRecibidas()
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return objetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            objetoGlobal = value
        End Set
    End Property

End Class


