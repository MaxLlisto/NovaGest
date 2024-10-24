Imports System
Imports System.IO
Imports System.Data

Public Class FrmRepaletizacionMateriales
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public Palet As String

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub FrmDestruirPalet_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        Dim i As Integer = 0
        Dim Row As DataGridViewRow
        Dim Cell As DataGridViewTextBoxCell
        Dim o As Integer = 0

        dgv().Columns().Clear()
        Dim column1 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column1.Name = "codigo"
        column1.HeaderText = "Código"
        column1.Width = 125
        dgv().Columns.Add(column1)

        Dim column2 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column2.Name = "articulo"
        column2.HeaderText = "Artículo"
        column2.Width = 100
        dgv().Columns.Add(column2)

        Dim column3 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column3.Name = "descripcion"
        column3.HeaderText = "Descripción"
        column3.Width = 100
        dgv().Columns.Add(column3)

        Dim column4 As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn()
        column4.Name = "unidades"
        column4.HeaderText = "Unidades"
        column4.Width = 100
        dgv().Columns.Add(column4)


        Dim column5 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column5.Name = "precio"
        column5.HeaderText = "Precio"
        column5.Width = 100
        dgv().Columns.Add(column5)

        Dim column6 As DataGridViewColumn = New DataGridViewTextBoxColumn()
        column6.Name = "sel"
        column6.HeaderText = ""
        column6.Width = 0
        dgv().Columns.Add(column6)

        sSql = "SELECT * FROM almacen" & Trim(ObjetoGlobal.EjercicioActual) & "_h "
        sSql = sSql + " WHERE tipo_documento = 'PAL' AND (tipo_movimiento = 'SC' OR tipo_movimiento = 'SPT') AND numero_documento ='" & Trim(Palet) & "'"
        Rs.Open(sSql, ObjetoGlobal.cn)

        While Not Rs.EOF
            i = i + 1
            Row = New DataGridViewRow
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "" & Rs!codigo_movimiento
            Row.Cells.Add(Cell)
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "" & Rs!articulo
            Row.Cells.Add(Cell)
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "" & Rs!Descripcion
            Row.Cells.Add(Cell)
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "" & Rs!unidades
            Row.Cells.Add(Cell)
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "" & Rs!Importe
            Row.Cells.Add(Cell)
            Cell = New DataGridViewTextBoxCell
            Cell.Value = "N"
            Row.Cells.Add(Cell)
            dgv.Rows.Add(Row)
            Rs.MoveNext()
        End While
        Rs.Close()
    End Sub
    Private Sub cmdRepaletizar_Click(sender As Object, e As EventArgs) Handles cmdRepaletizar.Click
        Dim nOrdenMax As Integer
        Dim i As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        Dim nOrd As Integer
        Dim nCajas As Integer

        If Trim(TxtPaletDestino.Text) = "" Then
            MsgBox("Es necesario indicar un palet de destino")
            Return
        End If

        nOrdenMax = 0

        sSql = "SELECT o.Cajas_palet FROM palets p LEFT JOIN ordenes_confeccion o ON o.empresa = p.empresa and o.numero_orden = p.orden_confeccion WHERE p.empresa ='" & ObjetoGlobal.EmpresaActual & "' AND codigo_palet='" & Trim(Palet) & "'"
        Rs.Open(sSql, ObjetoGlobal.cn)
        nCajas = Rs!Cajas_palet
        Rs.Close

        Rs.Open("SELECT * FROM repaletizacion_m WHERE 1=0", ObjetoGlobal.cn, True)
        nOrd = 0

        For i = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).Cells("sel").Value = "S" Then
                Rs.AddNew()
                nOrd = nOrd + 1
                Rs!empresa = ObjetoGlobal.EmpresaActual
                Rs!palet_inicial = Trim(Palet)
                Rs!palet_final = Trim(TxtPaletDestino.Text)
                Rs!Contador = nOrd
                Rs!codigo_movimiento = CLng(dgv.Rows(i).Cells("codigo").Value)
                Rs!articulo = dgv.Rows(i).Cells("articulo").Value
                Rs.Update()
            End If
        Next
        Rs.Close

        Rs.Open("SELECT * FROM repaletizacion_m WHERE 1=0", ObjetoGlobal.cn, True)
        Rs.AddNew
        Rs!empresa = ObjetoGlobal.empresaActual
        Rs!palet_inicial = Trim(Palet)
        Rs!palet_final = Trim(TxtPaletDestino.Text)
        Rs!Cajas = nCajas
        Rs!Actualizado = "N"
        Rs!cajas_eliminadas = 0
        Rs!tipo_repaletiz = "R"
        Rs.Update
        Rs.Close()
        Me.Close()

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim i As Integer

        dgv.Rows(e.RowIndex).Cells("sel").Value = If(dgv.Rows(e.RowIndex).Cells("sel").Value = "N", "S", "N")
        If dgv.Rows(e.RowIndex).Cells("sel").Value = "N" Then
            For i = 0 To 5
                dgv.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.White
            Next
        Else
            For i = 0 To 5
                dgv.Rows(e.RowIndex).Cells(i).Style.BackColor = Color.FromArgb(217, 255, 217)
            Next
        End If

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Me.Close()
    End Sub


    Private Sub TxtPaletDestino_Validated(sender As Object, e As EventArgs) Handles TxtPaletDestino.Validated
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String


        If Trim(TxtPaletDestino.Text) <> "" Then
            sSql = "SELECT * FROM palets WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND codigo_palet = '" & Trim(TxtPaletDestino.Text) & "'"
            Rs.Open(sSql, ObjetoGlobal.cn)
            If Rs.EOF Then
                TxtPaletDestino.Text = ""
                MsgBox("No existe referencia en palets con el código indicado")
            End If
            Rs.Close
        End If

    End Sub



End Class