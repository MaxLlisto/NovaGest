Public Class SeleccionAsientos
    Dim objetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public Desdelafecha As Date
    Public Hastalafecha As Date
    Public whereAsientos As String
    Public ordenasientos As String = ""
    Public rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Public hayseleccion As Boolean = False
    Public dsSel As DataSet
    Public dtSel As DataTable


    Private Sub SeleccionAsientos_Load(sender As Object, e As EventArgs) Handles Me.Load
        'e.DialogResult = DialogResult.Cancel
        LlenarVista()
    End Sub

    Private Sub BtSeleccionartodo_Click(sender As Object, e As EventArgs) Handles BtSeleccionartodo.Click
        'Dim check As CheckBox = DirectCast(sender, CheckBox)

        For Each item As ListViewItem In lv.Items
            item.Checked = True 'check.Checked
        Next

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return objetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            objetoGlobal = value
        End Set
    End Property

    Private Sub BtDeseleccionar_Click(sender As Object, e As EventArgs) Handles BtDeseleccionar.Click
        'Dim check As CheckBox = DirectCast(sender, CheckBox)

        For Each item As ListViewItem In lv.Items
            item.Checked = False 'Not check.Checked
        Next

    End Sub

    Public Function ListaAsientos() As DataTable
        Dim aRet() As Integer
        Dim j As Integer = 0
        Dim dr As DataRow

        rs.MoveFirst()
        For i = 0 To rs.RecordCount - 1
            If lv.Items(i).Checked Then
                dr = dtSel.NewRow()
                dr("empresa") = rs("empresa")
                dr("ejercicio") = rs("ejercicio")
                dr("fecha_asiento") = rs("fecha_asiento")
                dr("diario") = rs("diario")
                dr("numero_asiento") = rs("numero_asiento")
                dr("importe_debe") = rs("importe_debe")
                dr("importe_haber") = rs("importe_haber")
                dr("periodo") = rs("periodo")
                dtSel.Rows.Add(dr)
            End If
            rs.MoveNext()
        Next
        Return dtSel

    End Function

    Private Sub LlenarVista()
        Dim item As ListViewItem

        lv.Items.Clear()
        ' Asignar algunos datos aleatorios
        rs.Open("SELECT * FROM CABECERAS_ASIENTO WHERE EMPRESA='" & objetoGlobal.EmpresaActual & "' AND EJERCICIO='" & objetoGlobal.EjercicioActual & "' AND " & whereAsientos & If(ordenasientos <> "", " ORDER BY " & ordenasientos, ""), objetoGlobal.cn)
        While Not rs.EOF
            item = New ListViewItem(Format(rs!numero_asiento, "####0"))
            item.SubItems.Add(Format(rs!fecha_asiento, "dd/MM/yyyy"))
            'item.SubItems.Add(Format(rs!numero_asiento, "####0"))
            item.SubItems.Add(Format(rs!Diario, "####0"))
            item.SubItems.Add(Format(rs!importe_debe, "#####,###,##0.00"))
            item.SubItems.Add(Format(rs!importe_haber, "#####,###,##0.00"))
            lv.Items.Add(item)
            rs.MoveNext()
        End While
        dsSel = rs.cnDataSet.Clone()
        dtSel = dsSel.Tables(0)

    End Sub

    Public Function Chequeada(i) As Boolean
        Return lv.Items.Item(i).Checked
    End Function

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtCancelar_Click(sender As Object, e As EventArgs) Handles BtCancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class