Imports System.ComponentModel

Public Class CuadrillasCitricos
    Inherits libcomunes.FormGenerico
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim RsCampos As cnRecordset.CnRecordset
    Dim RsOperarios As cnRecordset.CnRecordset
    Dim FilaCapataz As Integer = -1
    Private WithEvents CeldaT As DataGridViewTextBoxEditingControl


    Private Sub Cuadrillas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '       Me.OJ = ModuloControl.ObjetoGlobal
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property


    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Try
            RsCampos.Close()
        Catch ex As Exception
        End Try
        Try
            RsOperarios.Close()
        Catch ex As Exception
        End Try

        Me.Close()
    End Sub

    Private Sub TxtCapataz_Validating(sender As Object, e As CancelEventArgs) Handles TxtCapataz.Validating

        Dim Sql As String, Rs As New cnRecordset.CnRecordset, Codigo As Integer
        e.Cancel = True

        If Trim(TxtCapataz.Text) = "" Then
            LblNombreCapataz.Text = ""
            e.Cancel = False
        Else
            Codigo = -1
            If IsNumeric(TxtCapataz.Text) Then Codigo = CInt(TxtCapataz.Text)
            If Codigo = -1 Then
                LblNombreCapataz.Text = ""
                MsgBox("No existe el operario indicado.")
            Else
                Sql = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + CStr(Codigo)
                Rs.Open(Sql, ObjetoGlobal.cn)
                If Rs.Recordcount = 0 Then
                    LblNombreCapataz.Text = ""
                    MsgBox("No existe el capataz indicado.")
                Else
                    LblNombreCapataz.Text = Trim(Rs!nombre_operario)
                    e.Cancel = False
                End If
                Rs.Close()
            End If
        End If
    End Sub





    Private Sub CmdProcesarDia_Click(sender As Object, e As EventArgs) Handles CmdProcesarDia.Click
        'Dim Rs2 As Recordset, Sql As String, i As Long, j As Long, Columna As Long, Fila As Long
        Dim RsDatos As cnRecordset.CnRecordset
        Dim Codigo As Integer, i As Long, Fila As Integer, Columna As Integer
        Dim Sql As String, Cadena As String, CadenaAlbaranes As String
        Dim Cajas As Long, Palots As Integer
        Dim FlagError As Boolean, MensajeError As String
        Dim Celda As DataGridViewCell

        If Trim(TxtCapataz.Text) = "" Then
            MsgBox("Debe indicar capataz.")
            Exit Sub
        End If
        Codigo = -1
        If IsNumeric(TxtCapataz.Text) Then
            Codigo = CInt(TxtCapataz.Text)
        End If
        If Codigo = -1 Then
            MsgBox("Debe indicar un código válido de capataz.")
            Exit Sub
        End If

        RsCampos = New cnRecordset.CnRecordset
        Sql = "SELECT ENTRADAS_ALBARANES.CODIGO_SOCIO,SOCIOS_COOP.APELLIDOS_SOCIO,SOCIOS_COOP.NOMBRE_SOCIO,ENTRADAS_ALBARANES.CODIGO_CAMPO,ENTRADAS_ALBARANES.CODIGO_VARIEDAD,VARIEDADES.DESCRIPCION,SITUACION_CAMPOS.DESCRIPCION AS SITUACIONC,ENTRADAS_ALBARANES.CAPATAZ,sum(CASE WHEN UPPER(PESO_ESPECIAL_SN)='S' THEN ROUND((1 - ISNULL(PORCENTAJE_RECOL_I,0)/ 100) * PESO_CUADRILLAS,0) ELSE ROUND((1 - ISNULL(PORCENTAJE_RECOL_I,0)/ 100) * KG_A_LIQUIDAR,0)  END) AS KILOS,"
        Sql = Trim(Sql) + " SUM(CASE WHEN UPPER(PESO_ESPECIAL_SN)='S' THEN ROUND((ISNULL(PORCENTAJE_RECOL_I,0)/ 100) * PESO_CUADRILLAS,0) ELSE ROUND((ISNULL(PORCENTAJE_RECOL_I,0)/ 100) * KG_A_LIQUIDAR,0)  END) AS KILOS_NO FROM ENTRADAS_ALBARANES"
        Sql = Trim(Sql) + " JOIN VARIEDADES ON VARIEDADES.EMPRESA = ENTRADAS_ALBARANES.EMPRESA AND VARIEDADES.CODIGO_VARIEDAD= ENTRADAS_ALBARANES.CODIGO_VARIEDAD "
        Sql = Trim(Sql) + " JOIN SOCIOS_COOP ON SOCIOS_COOP.CODIGO_SOCIO= ENTRADAS_ALBARANES.CODIGO_SOCIO "
        Sql = Trim(Sql) + " JOIN CAMPOS ON CAMPOS.EMPRESA= ENTRADAS_ALBARANES.EMPRESA AND CAMPOS.CODIGO_CAMPO = ENTRADAS_ALBARANES.CODIGO_CAMPO"
        Sql = Trim(Sql) + " JOIN SITUACION_CAMPOS ON SITUACION_CAMPOS.EMPRESA= CAMPOS.EMPRESA AND SITUACION_CAMPOS.CODIGO_SITUACION = CAMPOS.SITUACION_CAMPO"
        Sql = Trim(Sql) + " WHERE ENTRADAS_ALBARANES.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ENTRADAS_ALBARANES.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ENTRADAS_ALBARANES.FECHA_ENTRADA = '" + Format(CDate(DTFechaEntrada.Value), "dd/MM/yyyy") + "' AND ENTRADAS_ALBARANES.CAPATAZ = " + CStr(TxtCapataz.Text) + " AND ENTRADAS_ALBARANES.LIQUIDADA_C_SN<>'S'"
        Sql = Trim(Sql) + " AND ENTRADAS_ALBARANES.CODIGO_VARIEDAD BETWEEN '01' AND '02z' AND NOT( ENTRADAS_ALBARANES.OBSERVACIONES LIKE '%*KILOS*%')"
        Sql = Trim(Sql) + " GROUP BY ENTRADAS_ALBARANES.CODIGO_SOCIO,SOCIOS_COOP.APELLIDOS_SOCIO,SOCIOS_COOP.NOMBRE_SOCIO,ENTRADAS_ALBARANES.CODIGO_CAMPO,ENTRADAS_ALBARANES.CODIGO_VARIEDAD,VARIEDADES.DESCRIPCION,SITUACION_CAMPOS.DESCRIPCION,ENTRADAS_ALBARANES.CAPATAZ"
        Sql = Trim(Sql) + " ORDER BY ENTRADAS_ALBARANES.CODIGO_SOCIO,SOCIOS_COOP.APELLIDOS_SOCIO,SOCIOS_COOP.NOMBRE_SOCIO,ENTRADAS_ALBARANES.CODIGO_CAMPO,ENTRADAS_ALBARANES.CODIGO_VARIEDAD,VARIEDADES.DESCRIPCION,SITUACION_CAMPOS.DESCRIPCION,ENTRADAS_ALBARANES.CAPATAZ"
        RsCampos.Open(Sql, ObjetoGlobal.cn)
        If RsCampos.RecordCount = 0 Then
            RsCampos.Close()
            MsgBox("No existen entradas correspondientes a ese capataz y fecha.")
            Exit Sub
        End If
        Sql = "SELECT * FROM OPERARIOS_COOP WHERE CUADRILLA = '" + CStr(TxtCapataz.Text) + "'"
        Sql = Trim(Sql) + " ORDER BY CODIGO_OPERARIO"
        RsOperarios = New cnRecordset.CnRecordset
        RsOperarios.Open(Sql, ObjetoGlobal.cn)
        If RsOperarios.RecordCount = 0 Then
            MsgBox("No existen operarios correspondientes a esa cuadrilla.")
            RsOperarios.Close()
            Sql = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + CStr(TxtCapataz.Text)
            RsOperarios.Open(Sql, ObjetoGlobal.cn)
        End If
        InicializarGrid()

        FlagError = False
        MensajeError = ""
        For i = 1 To RsCampos.RecordCount
            RsCampos.AbsolutePosition = i
            Cajas = 0 : Palots = 0 : CadenaAlbaranes = "Alb.:"
            Sql = "SELECT B.*,E.kg_sin_clasif, E.industria_sn FROM ENTRADAS_ENVASES B JOIN ENTRADAS_ALBARANES E ON E.EMPRESA = B.EMPRESA and E.EJERCICIO = B.EJERCICIO AND E.SERIE_ALBARAN = B.SERIE_ALBARAN AND E.NUMERO_ALBARAN = B.NUMERO_ALBARAN"
            Sql = Trim(Sql) + " WHERE B.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND B.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.FECHA_ENTRADA = '" + Format(CDate(DTFechaEntrada.Value), "dd/MM/yyyy") + "' AND E.CAPATAZ = " + CStr(TxtCapataz.Text) + " AND E.LIQUIDADA_C_SN<>'S'"
            Sql = Trim(Sql) + " AND E.CODIGO_VARIEDAD BETWEEN '01' AND '02z' AND NOT( E.OBSERVACIONES LIKE '%*KILOS*%')"
            Sql = Trim(Sql) + " AND E.CODIGO_CAMPO = " + CStr(RsCampos!codigo_campo) + " AND E.CODIGO_VARIEDAD = '" + Trim(RsCampos!codigo_variedad) + "' AND E.CAPATAZ = " + CStr(TxtCapataz.Text) + " AND B.ENTRADA_SALIDA = 'E' AND B.CODIGO_ENVASE IN ('CAJA.','PV')"
            'aaa
            RsDatos = New cnRecordset.CnRecordset
            RsDatos.Open(Sql, ObjetoGlobal.cn)
            For j = 1 To RsDatos.RecordCount
                RsDatos.AbsolutePosition = j
                'If RsDatos!kg_sin_clasif > 0 Then
                '    FlagError = True
                '    MensajeError = "El albarán " + CStr(RsDatos!numero_albaran) + " no está clasificado."
                'End If
                If Trim(RsDatos!codigo_envase) = "CAJA." Then
                    Cajas = Cajas + RsDatos!numero_envases
                ElseIf Trim(RsDatos!codigo_envase) = "PV" Then
                    Palots = Palots + RsDatos!numero_envases
                End If
                CadenaAlbaranes = Trim(CadenaAlbaranes) & " " & CStr(RsDatos!numero_albaran)
            Next

            RsDatos.Close()
            Columna = i + 2
            Cadena = CStr(RsCampos!codigo_campo) & " / " & Trim("" & RsCampos!situacionc)
            Cadena = Cadena + vbNewLine & CStr(RsCampos!codigo_socio) & " / " & Trim("" & RsCampos!apellidos_socio) & " " & Trim("" & RsCampos!nombre_socio)
            Cadena = Cadena + vbNewLine & Trim(RsCampos!Descripcion)
            Cadena = Cadena + vbNewLine & Format(RsCampos!Kilos, "#,#") + "+" + Format(RsCampos!Kilos_no, "#,#") + " / " + CStr(Cajas) + " / " + CStr(Palots)
            Cadena = Cadena + vbNewLine & Trim(CadenaAlbaranes)
            DGHoras.Columns(Columna).HeaderCell.Value = Trim(Cadena)
        Next

        FilaCapataz = -1
        For i = 1 To RsOperarios.RecordCount
            RsOperarios.AbsolutePosition = i
            Fila = i
            Cadena = CStr(RsOperarios!codigo_operario) & " - " & Trim("" & RsOperarios!nombre_operario)
            If CInt(TxtCapataz.Text) = RsOperarios!codigo_operario Then
                FilaCapataz = Fila
                Celda = DGHoras.Rows(FilaCapataz).Cells(0)
                Celda.Style.ForeColor = Color.Red
            End If
            DGHoras.Rows(Fila).Cells(0).Value = Trim(Cadena)
        Next
        If FilaCapataz = -1 Then
            MsgBox("El capataz no está incluido en la cuadrilla")
        End If
        VerCuantosOperarios()

        DGHoras.Sort(DGHoras.Columns(0), ListSortDirection.Ascending)



        'InicializarGrid()

        'If FlagError = True Then
        '    MsgBox(MensajeError)
        '    Exit Sub
        'End If

        'For i = 1 To Rs2.RecordCount
        '    Rs2.AbsolutePosition = i
        '    Fila = i + 4
        '    If CInt(Rs2!codigo_operario) = CInt(TxtCapataz.Text) Then
        '        DGHoras.Row = Fila
        '        DGHoras.Col = 0
        '        DGHoras.CellBackColor = RGB(200, 200, 200)
        '        DGHoras.Row = Fila
        '        DGHoras.Col = 1
        '        DGHoras.CellBackColor = RGB(200, 200, 200)
        '    Else
        '        DGHoras.Row = Fila
        '        DGHoras.Col = 0
        '        DGHoras.CellBackColor = 0
        '        DGHoras.Row = Fila
        '        DGHoras.Col = 1
        '        DGHoras.CellBackColor = 0
        '    End If
        '    DGHoras.TextMatrix(Fila, 0) = CStr(Rs2!codigo_operario)
        '    DGHoras.TextMatrix(Fila, 1) = CStr(Rs2!nombre_operario)
        'Next

        'VerCuantosOperariosH 

    End Sub


    Private Sub InicializarGrid()
        Dim OFila As DataGridViewRow, OColumna As DataGridViewColumn
        Dim ColumnaBoton As New DataGridViewButtonColumn
        Dim Celda As DataGridViewCell

        'DESCRIPCIÓN DATAGRIDVIEW
        'Columna 0 ---> Nombres de operrio
        'Columna 1 ---> Botones de eliminar operario
        'Columna 2 ---> Operarios transporte
        'Columnas 3 a DWHoras.Rowcount -1  ---> Campos 

        '                                        0              1                   2            3  a DWHoras.Rowcount -1
        'Fila Header                         "Operarios" |    Nada           | "Transp." |  datos campos...
        'Fila 0                          "Horas campo->" |Botón sin uso      |    Nada   |  Horas genéricas campo (todos los operarios)
        'Fila 1 a Num.operarios+1     Código y nombre op.|Botón eliminar op. | Transp.op.|  Horas operarios campo


        DGHoras.Rows.Clear()
        DGHoras.AllowUserToAddRows = False
        DGHoras.RowHeadersVisible = False
        DGHoras.AllowUserToResizeColumns = False
        DGHoras.AllowUserToResizeRows = False

        DGHoras.Columns.Add("", "Operarios")
        ColumnaBoton.HeaderText = ""
        DGHoras.Columns.Add(ColumnaBoton)

        DGHoras.ColumnCount = RsCampos.RecordCount + 3
        DGHoras.RowCount = RsOperarios.RecordCount + 1
        'DGHoras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'DGHoras.RowHeadersWidth = 200
        'DGHoras.ColumnHeadersHeight = 400
        For Each OFila In DGHoras.Rows : OFila.Height = 18 : Next
        For Each OColumna In DGHoras.Columns
            OColumna.Width = 200
            OColumna.SortMode = DataGridViewColumnSortMode.NotSortable
            If OColumna.Index > 1 Then OColumna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next

        DGHoras.Columns(0).Width = 200
        DGHoras.Columns(0).ReadOnly = True
        DGHoras.Columns(1).Width = 20
        DGHoras.Columns(2).Width = 70

        DGHoras.EnableHeadersVisualStyles = False

        Celda = DGHoras.Columns(0).HeaderCell
        Celda.Style.Font = New Font(DGHoras.DefaultCellStyle.Font.Name, 16, FontStyle.Bold)

        DGHoras.Rows(0).Cells(0).ReadOnly = True
        DGHoras.Rows(0).Cells(0).Value = " Horas campo->"
        DGHoras.Rows(0).Cells(0).Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGHoras.Rows(0).Cells(0).Style.Font = New Font(DGHoras.DefaultCellStyle.Font.Name, 10, FontStyle.Bold)

        DGHoras.Rows(0).Cells(1).ReadOnly = True

        DGHoras.Rows(0).Cells(2).ReadOnly = True
        DGHoras.Columns(2).HeaderCell.Value = "Transp."
        DGHoras.Columns(2).HeaderCell.Style.Font = New Font(DGHoras.DefaultCellStyle.Font.Name, 10, FontStyle.Bold)


    End Sub

    Private Sub DGHoras_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DGHoras.CellPainting
        Dim Celda As DataGridViewButtonCell, Icono As Icon

        If (e.ColumnIndex = 1 And e.RowIndex >= 1) Then
            e.Paint(e.CellBounds, DataGridViewPaintParts.All)

            Celda = sender.Rows(e.RowIndex).Cells(e.ColumnIndex)
            '.Value = Descripcion
            'Celda = sender.Rows(e.RowIndex, e.ColumnIndex)
            Icono = New Icon(Environment.CurrentDirectory + "\Cancel16.ico")
            'MsgBox(Environment.CurrentDirectory.ToString)
            e.Graphics.DrawIcon(Icono, e.CellBounds.Left + 3, e.CellBounds.Top + 3)

            '    this.dataGridView1.Rows[e.RowIndex].Height = icoAtomico.Height + 10
            'this.dataGridView1.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10

            e.Handled = True
        End If

    End Sub


    Private Sub DGHoras_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGHoras.CellContentClick
        Dim Grid = DirectCast(sender, DataGridView)

        If TypeOf Grid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso e.RowIndex >= 1 And e.ColumnIndex = 1 Then
            BorraOperario(e.RowIndex)
        End If
    End Sub

    Private Sub CmdAceptaOperario_Click(sender As Object, e As EventArgs) Handles CmdAceptaOperario.Click
        Dim Sql As String, Rs As New cnRecordset.CnRecordset, Codigo As Integer, Cadena As String
        If Trim(TxtOperario.Text) = "" Then
            MsgBox("Debe indicar operario a añadir")
        Else
            Codigo = -1
            If IsNumeric(TxtOperario.Text) Then Codigo = CInt(TxtOperario.Text)
            If Codigo = -1 Then
                LblNombreOperario.Text = ""
                MsgBox("Debe indicar código de operario.")
            Else
                Sql = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + CStr(Codigo)
                Rs.Open(Sql, ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    LblNombreOperario.Text = ""
                    MsgBox("No existe el operario indicado.")
                ElseIf YaIncluido(Codigo) = True Then
                    LblNombreOperario.Text = Trim(Rs!nombre_operario)
                    MsgBox("El operario indicado ya está incluido en la lista.")
                Else
                    Cadena = CStr(Rs!codigo_operario) & " - " & Trim("" & Rs!nombre_operario)
                    IncluyeOperario(Codigo, Cadena)
                    Me.ActiveControl = TxtOperario
                End If
                Rs.Close()
            End If
        End If
    End Sub

    Private Sub BorraOperario(Fila)
        Dim jj As Integer, Cadena As String

        Cadena = DGHoras.Rows(Fila).Cells(0).Value
        jj = InStr(Cadena, " -")
        If jj > 1 Then Cadena = Microsoft.VisualBasic.Left(Cadena, jj - 1)
        If CLng(Cadena) = CLng(TxtCapataz.Text) Then
            MsgBox("El capataz no puede ser eliminado de la lista de operarios")
            Exit Sub
        Else
            DGHoras.Rows.RemoveAt(Fila)
            VerCuantosOperarios()
        End If

    End Sub

    'Private Sub DTFechaEntrada_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then
    '        SendKeys.Send("{TAB}")
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub VerCuantosOperarios()
        Dim Cuantos As Integer

        Cuantos = DGHoras.Rows.Count - 1
        LblCuantos.Text = CStr(Cuantos)
    End Sub

    Private Sub TxtOperario_Validating(sender As Object, e As CancelEventArgs) Handles TxtOperario.Validating
        Dim Sql As String, Rs As New cnRecordset.CnRecordset, Codigo As Integer
        e.Cancel = True

        If Trim(TxtOperario.Text) = "" Then
            LblNombreOperario.Text = ""
            e.Cancel = False
        Else
            Codigo = -1
            If IsNumeric(TxtOperario.Text) Then Codigo = CInt(TxtOperario.Text)
            If Codigo = -1 Then
                LblNombreOperario.Text = ""
                MsgBox("Debe indicar código de operario.")
            Else
                Sql = "SELECT * FROM OPERARIOS_COOP WHERE CODIGO_OPERARIO = " + CStr(Codigo)
                Rs.Open(Sql, ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    LblNombreOperario.Text = ""
                    MsgBox("No existe el operario indicado.")
                Else
                    LblNombreOperario.Text = Trim(Rs!nombre_operario)
                    If YaIncluido(Codigo) = True Then
                        MsgBox("El operario indicado ya está incluido en la lista.")
                    Else
                        e.Cancel = False
                    End If
                End If
                Rs.Close()
            End If
        End If
    End Sub



    Private Function YaIncluido(Codigo As Long) As Boolean
        Dim jj As Integer, Cadena As String

        YaIncluido = False
        For i = 1 To DGHoras.Rows.Count - 1
            Cadena = DGHoras.Rows(i).Cells(0).Value
            jj = InStr(Cadena, " -")
            If jj > 1 Then Cadena = Microsoft.VisualBasic.Left(Cadena, jj - 1)
            If CLng(Cadena) = CLng(Codigo) Then
                YaIncluido = True
                Exit For
            End If
        Next
    End Function

    Private Sub IncluyeOperario(Codigo As Long, Nombre As String)
        DGHoras.Rows.Add()
        DGHoras.Rows(DGHoras.Rows.Count - 1).Height = 18
        DGHoras.CurrentCell = DGHoras.Rows(DGHoras.Rows.Count - 1).Cells(0)
        DGHoras.Rows(DGHoras.Rows.Count - 1).Cells(0).Value = Nombre
        DGHoras.Sort(DGHoras.Columns(0), ListSortDirection.Ascending)
        VerCuantosOperarios()
    End Sub



    Private Sub CeldaT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CeldaT.KeyPress
        Dim Fila As Integer, Columna As Integer

        Fila = DGHoras.CurrentCell.RowIndex
        Columna = DGHoras.CurrentCell.ColumnIndex
        If Columna = 2 Then
            e.KeyChar = ControlaTecla(sender, e.KeyChar, 0, False, 2)
        Else
            e.KeyChar = ControlaTecla(sender, e.KeyChar, 2, False, 5)
        End If
        If e.KeyChar = Chr(0) Then e.Handled = True
    End Sub

    Private Sub DGHoras_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGHoras.EditingControlShowing
        CeldaT = TryCast(e.Control, DataGridViewTextBoxEditingControl)
    End Sub

    Private Sub CmdGrabar_Click(sender As Object, e As EventArgs) Handles CmdGrabar.Click
        Dim i As Integer, j As Long, k As Long, l As Long, kk As Integer, jj As Integer

        Dim SQL As String
        Dim RsP As cnRecordset.CnRecordset, Rs As cnRecordset.CnRecordset, RsGrabar As cnRecordset.CnRecordset
        Dim Fila As Integer, Columna As Integer
        Dim HorasL As Double, Diferencia As Double
        Dim TotalTransporte As Integer, PrecioTransporte As Double, PrecioTransporte2 As Double, Transporte As Integer
        'Dim Importes(50, 5) As Double

        Dim Cadena As String, Codigo As Long
        Dim PrecioCapataz As Double, PrecioOperario As Double
        Dim HorasDefecto(100) As Double, HorasT As Double
        Dim Kilos5 As Double, Kilos As Double, Proporcion As Double, K2 As Double
        Dim DatosCampo(100, 4) As Double, Ajuste As Double
        Dim HorasAlbaran(100, 100) As Double
        Dim Transaction As Data.SqlClient.SqlTransaction, HayTransaccion As Boolean

        HayTransaccion = False
        Try
            If RsCampos.RecordCount = 0 Then
                MsgBox("No existen campos a liquidar")
                Exit Sub
            End If
            If RsOperarios.RecordCount = 0 Then
                MsgBox("No existen operarios a liquidar")
                Exit Sub
            End If
        Catch ex As Exception
            Exit Sub
        End Try

        RsP = New cnRecordset.CnRecordset
        SQL = "SELECT * FROM kilos_recol_var WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = 'T'"
        RsP.Open(SQL, ObjetoGlobal.cn)
        If RsP.RecordCount = 0 Then
            PrecioOperario = 0
            PrecioCapataz = 0
        Else
            PrecioOperario = Math.Round(RsP!precio_hora_op, 4)
            PrecioCapataz = Math.Round(RsP!precio_hora_cap, 4)
        End If
        RsP.Close()

        'Transporte
        TotalTransporte = 0
        For i = 1 To DGHoras.RowCount - 1
            Fila = i
            Transporte = 0
            If Trim(DGHoras.Rows(Fila).Cells(2).Value) > "" Then
                If IsNumeric(DGHoras.Rows(Fila).Cells(2).Value) Then
                    Transporte = CInt(DGHoras.Rows(Fila).Cells(2).Value)
                End If
            End If
            TotalTransporte = TotalTransporte + Transporte
        Next
        If TotalTransporte > 0 Then
            If TotalTransporte <> DGHoras.RowCount - 1 Then
                MsgBox("Número incorrecto de personas en transporte.")
                Exit Sub
            End If
        End If
        PrecioTransporte = 0
        If Trim(TxtTransporte.Text) > "" Then
            If IsNumeric(TxtTransporte.Text) Then
                PrecioTransporte = Math.Round(CDbl(TxtTransporte.Text), 2)
            End If
        End If
        PrecioTransporte2 = 0
        If Trim(TxtTransporte2.Text) > "" Then
            If IsNumeric(TxtTransporte2.Text) Then
                PrecioTransporte2 = Math.Round(CDbl(TxtTransporte2.Text), 2)
            End If
        End If
        If TotalTransporte > 0 And PrecioTransporte = 0 And PrecioTransporte2 = 0 Then
            MsgBox("Debe indicar precio de transporte.")
            Exit Sub
        End If

        'Comprobación de partes liquidados

        Rs = New cnRecordset.CnRecordset

        For i = 1 To RsCampos.RecordCount
            RsCampos.AbsolutePosition = i
            SQL = "SELECT * FROM ENTRADAS_RECOL WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_CAMPO = " + CStr(RsCampos!codigo_campo) + " AND FECHA = '" + Format(CDate(DTFechaEntrada.Value), "dd/MM/yyyy") + "' AND CODIGO_VARIEDAD = '" + Trim(RsCampos!codigo_variedad) + "' AND CAPATAZ = " + CStr(RsCampos!Capataz) + " ORDER BY EMPRESA,EJERCICIO,SERIE_ALBARAN,NUMERO_ALBARAN"
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Rs.RecordCount > 0 Then
                If MsgBox("ATENCION: Ya existen partes grabados correspondientes al capataz/dia." & vbNewLine + "¿Desea continuar?", vbExclamation + vbYesNo) = vbNo Then
                    Rs.Close()
                    Exit Sub
                End If
            End If
            Rs.Close()
        Next

        'Comprobación de productividad de cada campo

        'DatosCampo(i, 0) = Horas totales imputadas
        'DatosCampo(i, 1) = Kilos mínimos para 5 horas
        'DatosCampo(i, 2) = Kilos recolectados
        'DatosCampo(i, 3) = Kilos necesarios
        'Los acumulados estén en DatosCampo(0,?)

        For i = 0 To 100 : For j = 0 To 4 : DatosCampo(i, j) = 0 : Next : Next
        For i = 1 To RsCampos.RecordCount
            RsCampos.AbsolutePosition = i

            If Trim(DGHoras.Rows(0).Cells(2 + i).Value) >= "0" Then
                If IsNumeric(DGHoras.Rows(0).Cells(2 + i).Value) Then
                    HorasDefecto(i) = Math.Round(CDbl(DGHoras.Rows(0).Cells(2 + i).Value), 2)
                End If
            End If
            Kilos5 = 0
            SQL = "SELECT * FROM kilos_recol_var WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(RsCampos!codigo_variedad) + "'"
            RsP.Open(SQL, ObjetoGlobal.cn)
            If RsP.RecordCount > 0 Then
                Kilos5 = Math.Round(RsP!kilos_5, 0)
            End If
            RsP.Close()

            HorasT = 0
            For j = 1 To DGHoras.RowCount - 1
                Fila = j
                If Trim(DGHoras.Rows(Fila).Cells(2 + i).Value) = "" Then
                    HorasT = HorasT + HorasDefecto(i)
                ElseIf Trim(DGHoras.Rows(Fila).Cells(2 + i).Value) >= "0" Then
                    If IsNumeric(DGHoras.Rows(Fila).Cells(2 + i).Value) Then
                        HorasT = HorasT + Math.Round(CDbl(DGHoras.Rows(Fila).Cells(2 + i).Value), 2)
                    Else
                        MsgBox("Horas incorrectas para el campo " + CStr(RsCampos!codigo_campo) + " y el operario " + Trim(DGHoras.Rows(Fila).Cells(0).Value))
                        Exit Sub
                    End If
                End If

            Next
            If HorasT = 0 Then
                MsgBox("No hay horas en el campo " + CStr(RsCampos!codigo_campo))
                Exit Sub
            End If
            If Math.Round(HorasT * Kilos5 / 5.0#, 2) > RsCampos!Kilos Then
                MsgBox("Kilos insuficientes en campo " + CStr(RsCampos!codigo_campo) + vbNewLine + "Kilos recolectados: " + Format(Math.Round(RsCampos!Kilos, 2), "#,#" + vbNewLine + "Kilos necesarios. . : " + Format((Math.Round(HorasT * Kilos5 / 5.0#, 2)), "#,#")))
            End If
            DatosCampo(i, 0) = HorasT : DatosCampo(i, 1) = Kilos5 : DatosCampo(i, 2) = RsCampos!Kilos : DatosCampo(i, 3) = Math.Round(HorasT * Kilos5 / 5.0#, 2)
            For j = 0 To 4 : DatosCampo(0, j) = DatosCampo(0, j) + DatosCampo(i, j) : Next
        Next

        'Ajuste de horas 
        Ajuste = 0
        If DatosCampo(0, 2) < DatosCampo(0, 3) Then
            Ajuste = Math.Round(100 * (1 - (DatosCampo(0, 2) / DatosCampo(0, 3))), 2)
            If MsgBox("Kilos recolectados insuficientes en el TOTAL DEL DIA." + vbNewLine + "Kilos recolectados: " + Format((Math.Round(DatosCampo(0, 2), 0)), "#,#") + vbNewLine + "Kilos necesarios. . : " + Format((Math.Round(DatosCampo(0, 3), 0)), "#,#") + vbNewLine + "¿Desea grabar la liquidación aplicando una corrección del " + CStr(Ajuste) + "%?", vbCritical + vbYesNo) = vbNo Then
                Exit Sub
            End If
        End If
        If Ajuste <> 0 Then
            For i = 0 To 100 : For j = 0 To 4 : DatosCampo(i, j) = 0 : Next : Next
            For i = 1 To RsCampos.RecordCount
                RsCampos.AbsolutePosition = i
                Kilos5 = 0
                SQL = "SELECT * FROM kilos_recol_var WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(RsCampos!codigo_variedad) + "'"
                RsP.Open(SQL, ObjetoGlobal.cn)
                If RsP.RecordCount > 0 Then
                    Kilos5 = Math.Round(RsP!kilos_5, 0)
                End If
                RsP.Close()

                HorasT = 0
                For j = 1 To DGHoras.RowCount - 1
                    Fila = j
                    If Trim(DGHoras.Rows(Fila).Cells(2 + i).Value) = "" Then
                        HorasT = HorasT + HorasDefecto(i)
                    ElseIf Trim(DGHoras.Rows(Fila).Cells(2 + i).Value) >= "0" Then
                        If IsNumeric(DGHoras.Rows(Fila).Cells(2 + i).Value) Then
                            HorasT = HorasT + Math.Round(CDbl(DGHoras.Rows(Fila).Cells(2 + i).Value), 2)
                        Else
                            MsgBox("Horas incorrectas para el campo " + CStr(RsCampos!codigo_campo) + " y el operario " + Trim(DGHoras.Rows(Fila).Cells(0).Value))
                            Exit Sub
                        End If
                    End If
                Next
                If HorasT = 0 Then
                    MsgBox("No hay horas en el campo " + CStr(RsCampos!codigo_campo))
                    Exit Sub
                End If
                DatosCampo(i, 0) = HorasT : DatosCampo(i, 1) = Kilos5 : DatosCampo(i, 2) = RsCampos!Kilos : DatosCampo(i, 3) = Math.Round(HorasT * Kilos5 / 5.0#, 2)
                For j = 0 To 4 : DatosCampo(0, j) = DatosCampo(0, j) + DatosCampo(i, j) : Next
            Next
        End If

        'Grabación
        'Transaction = ObjetoGlobal.cn.BeginTransaction("Entradas horas")
        'HayTransaccion = True

        For i = 1 To RsCampos.RecordCount
            RsCampos.AbsolutePosition = i
            For k = 0 To 100 : For l = 0 To 100 : HorasAlbaran(k, l) = 0 : Next : Next
            Kilos5 = 0
            SQL = "SELECT * FROM kilos_recol_var WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(RsCampos!codigo_variedad) + "'"
            RsP.Open(SQL, ObjetoGlobal.cn)
            If RsP.RecordCount > 0 Then
                Kilos5 = Math.Round(RsP!kilos_5, 0)
            End If
            RsP.Close()

            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_CAMPO = " + CStr(RsCampos!codigo_campo) + " AND CODIGO_VARIEDAD ='" + Trim(RsCampos!codigo_variedad) + "'  AND FECHA_ENTRADA = '" + Format(CDate(DTFechaEntrada.Value), "dd/MM/yyyy") + "' AND CAPATAZ = " + Trim(TxtCapataz.Text) + " AND ENTRADAS_ALBARANES.LIQUIDADA_C_SN<>'S' AND NOT(ENTRADAS_ALBARANES.OBSERVACIONES LIKE '%*HORAS*%') ORDER BY EMPRESA,EJERCICIO,SERIE_ALBARAN,NUMERO_ALBARAN"
            Rs.Open(SQL, ObjetoGlobal.cn, True)
            If Rs.Recordcount = 0 Then
                Rs.Close()
                MsgBox("No se puede actualizar. No existen entradas para el campo = " + CStr(RsCampos!codigo_campo))
                'Transaction.Rollback()
                Exit Sub
            End If

            Kilos = 0
            For j = 1 To Rs.Recordcount
                Rs.AbsolutePosition = j
                If Trim(Rs!peso_especial_sn) = "S" Then
                    K2 = Rs!peso_cuadrillas
                Else
                    K2 = Rs!kg_a_liquidar
                End If
                If Rs!porcentaje_recol_i > 0 Then K2 = Math.Round(K2 * (1 - Rs!porcentaje_recol_i / 100.0#), 0)
                Kilos = Kilos + K2
            Next
            If Kilos = 0 Then
                Rs.Close()
                MsgBox("No se puede actualizar. No existen kilos para el campo = " + CStr(RsCampos!codigo_campo))
                'Transaction.Rollback()
                Exit Sub
            End If

            Columna = 2 + i

            For j = 1 To Rs.Recordcount
                Rs.AbsolutePosition = j
                If Trim(Rs!peso_especial_sn) = "S" Then
                    K2 = Rs!peso_cuadrillas
                Else
                    K2 = Rs!kg_a_liquidar
                End If
                If Rs!porcentaje_recol_i > 0 Then K2 = Math.Round(K2 * (1 - Rs!porcentaje_recol_i / 100.0#), 0)
                Proporcion = K2 / Kilos
                HorasT = 0
                For k = 1 To DGHoras.RowCount - 1
                    Fila = k
                    If Trim(DGHoras.Rows(Fila).Cells(Columna).Value) = "" Then
                        HorasT = HorasT + HorasDefecto(i)
                    ElseIf Trim(DGHoras.Rows(Fila).Cells(Columna).Value) >= "0" Then
                        HorasT = HorasT + Math.Round(CDbl(DGHoras.Rows(Fila).Cells(Columna).Value), 2)
                    End If
                Next

                For k = 1 To DGHoras.RowCount - 1
                    Fila = k
                    HorasL = 0
                    If Trim(DGHoras.Rows(Fila).Cells(Columna).Value) = "" Then
                        HorasL = HorasDefecto(i)
                    Else
                        If Trim(DGHoras.Rows(Fila).Cells(Columna).Value) >= "0" Then
                            HorasL = Math.Round(CDbl(DGHoras.Rows(Fila).Cells(Columna).Value), 2)
                        End If
                    End If
                    HorasAlbaran(Fila, j) = Math.Round(HorasL * Proporcion, 2)
                    HorasAlbaran(Fila, 0) = HorasAlbaran(Fila, 0) + HorasAlbaran(Fila, j)
                Next
            Next

            For k = 1 To DGHoras.RowCount - 1
                Fila = k
                If Trim(DGHoras.Rows(Fila).Cells(Columna).Value) = "" Then
                    HorasL = HorasDefecto(i)
                ElseIf Trim(DGHoras.Rows(Fila).Cells(Columna).Value) >= "0" Then
                    HorasL = Math.Round(CDbl(DGHoras.Rows(Fila).Cells(Columna).Value), 2)
                End If
                If Math.Round(HorasAlbaran(Fila, 0), 2) <> Math.Round(HorasL, 2) Then
                    Diferencia = Math.Round(Math.Round(HorasL, 2) - Math.Round(HorasAlbaran(Fila, 0), 2), 2)
                    For j = 1 To Rs.Recordcount
                        Rs.AbsolutePosition = j
                        If Trim(Rs!peso_especial_sn) = "S" Then
                            K2 = Rs!peso_cuadrillas
                        Else
                            K2 = Rs!kg_a_liquidar
                        End If
                        If Rs!PORCENTAJE_RECOL_I > 0 Then K2 = Math.Round(K2 * (1 - Rs!PORCENTAJE_RECOL_I / 100.0#), 0)
                        If K2 > 0 Then
                            HorasAlbaran(Fila, j) = Math.Round(HorasAlbaran(Fila, j) + Diferencia, 2)
                            Exit For
                        End If
                    Next
                End If
            Next

            RsGrabar = New cnRecordset.CnRecordset
            SQL = "SELECT * FROM ENTRADAS_RECOL WHERE 1=0"
            RsGrabar.Open(SQL, ObjetoGlobal.cn, True)
            For j = 1 To Rs.Recordcount
                Rs.AbsolutePosition = j
                For k = 1 To DGHoras.RowCount - 1
                    Fila = k
                    HorasL = Math.Round(HorasAlbaran(Fila, j), 2)
                    Transporte = 0
                    If Trim(DGHoras.Rows(Fila).Cells(2).Value) > "" Then
                        If IsNumeric(DGHoras.Rows(Fila).Cells(2).Value) Then
                            Transporte = CInt(DGHoras.Rows(Fila).Cells(2).Value)
                        End If
                    End If
                    If HorasL <> 0 Or Transporte <> 0 Then
                        RsGrabar.AddNew()
                        RsGrabar!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsGrabar!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsGrabar!serie_albaran = Trim(Rs!serie_albaran)
                        RsGrabar!numero_albaran = CLng(Rs!numero_albaran)
                        Cadena = DGHoras.Rows(Fila).Cells(0).Value
                        jj = InStr(Cadena, " -")
                        If jj > 1 Then Cadena = Microsoft.VisualBasic.Left(Cadena, jj - 1)
                        Codigo = CLng(Cadena)
                        RsGrabar!codigo_operario = Codigo
                        RsGrabar!codigo_campo = RsCampos!codigo_campo
                        RsGrabar!Fecha = CDate(Rs!fecha_entrada)
                        RsGrabar!codigo_variedad = Trim(RsCampos!codigo_variedad)
                        RsGrabar!Capataz = CLng(TxtCapataz.Text)
                        RsGrabar!horas = Math.Round(HorasL * (100.0 - Ajuste) / 100.0, 2)
                        If Codigo = CLng(TxtCapataz.Text) Then
                            RsGrabar!Precio = Math.Round(PrecioCapataz, 2)
                        Else
                            RsGrabar!Precio = Math.Round(PrecioOperario, 2)
                        End If
                        RsGrabar!Importe = Math.Round(RsGrabar!Precio * RsGrabar!horas, 2)
                        If i = 1 And j = 1 Then
                            RsGrabar!Transporte = Transporte
                            RsGrabar!precio_transporte = PrecioTransporte
                            RsGrabar!precio_transporte2 = PrecioTransporte2
                        Else
                            RsGrabar!Transporte = 0
                            RsGrabar!precio_transporte = 0
                            RsGrabar!precio_transporte2 = 0
                        End If
                        RsGrabar!Situacion = "N"
                        RsGrabar!tipo_liquidacion = "H"
                        RsGrabar!precio_liquidado_t = 0
                        RsGrabar!importe_liquidado_t = 0
                        RsGrabar!kilos_liquidados_t = 0
                        RsGrabar!kilos = 0
                        RsGrabar!importe_total = 0
                        RsGrabar!dto_palot = 0
                        RsGrabar!importe_dto_palot = 0
                        RsGrabar.Update()
                    End If
                Next
                Rs!LIQUIDADA_C_SN = "S"
                Rs.Update()
            Next
            Rs.Close()
            RsGrabar.Close()
        Next

        'Transaction.Commit()
        MsgBox("Se ha grabado correctamente la tabla en horas")
    End Sub

    Private Function ControlaTecla(C As TextBox, TeclaPulsada As Char, Optional CuantosDecimales As Integer = 2, Optional PermiteNegativos As Boolean = True, Optional LongitudMaxima As Integer = 0)
        Dim PosicionComa As Integer

        ControlaTecla = Chr(0)
        If TeclaPulsada = Chr(45) Then 'Guión
            If PermiteNegativos = True And (LongitudMaxima = 0 Or Microsoft.VisualBasic.Len(C.Text) < LongitudMaxima) Then
                If C.Text = "" Or (C.SelectionStart = 0 And InStr(C.Text, "-") = 0) Then
                    ControlaTecla = TeclaPulsada
                End If
            End If
        ElseIf (TeclaPulsada = Chr(46) Or TeclaPulsada = Chr(44)) And (LongitudMaxima = 0 Or Microsoft.VisualBasic.Len(C.Text) < LongitudMaxima) Then   'Coma y punto
            If CuantosDecimales > 0 Then
                If (InStr(C.Text, ",") <= 0) And (Len(Trim(C.Text)) - C.SelectionStart <= CuantosDecimales) Then
                    ControlaTecla = Chr(44)
                End If
            End If
        ElseIf TeclaPulsada >= Chr(48) And TeclaPulsada <= Chr(57) And (LongitudMaxima = 0 Or Microsoft.VisualBasic.Len(C.Text) < LongitudMaxima) Then ' 0 a 9
            PosicionComa = InStr(C.Text, ",")
            If PosicionComa <= 0 Or C.SelectionStart < PosicionComa Then
                ControlaTecla = TeclaPulsada
            ElseIf Len(Strings.Right(C.Text, Len(C.Text) - PosicionComa)) < CuantosDecimales Then
                ControlaTecla = TeclaPulsada
            End If
        End If
    End Function


End Class
