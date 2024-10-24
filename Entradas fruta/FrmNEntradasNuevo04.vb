Public Class FrmNEntradasNuevo04
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public EnvaseSeleccionado As String = ""

    'Private Sub Form_Load()

    '    LstEnvases.FullRowSelect = True
    '    LstEnvases.MultiSelect = False
    '    Me.Text = "Selección de Envase"
    '    PoblarListaEnvases()

    'End Sub

    Private Sub LstEnvases_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LstEnvases.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            If Not LstEnvases.SelectedItems Is Nothing Then
                EnvaseSeleccionado = LstEnvases.SelectedItems(0).subItems(1).Text
                DialogResult = DialogResult.OK
            End If
        End If
    End Sub

    Private Sub PoblarListaEnvases()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String, i As Long
        Dim Item As ListViewItem

        LstEnvases.Items.Clear()

        SQL = "SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND DESCRIPCION > '' order by descripcion"
        Rs.Open(SQL, ObjetoGlobal.cn)

        If Rs.RecordCount > 0 Then
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                Item = New ListViewItem(Trim("" & Rs!Descripcion))
                Item.SubItems.Add(Rs!codigo_envase)
                LstEnvases.Items.Add(Item)
            Next
        End If
        Rs.Close()

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Me.Close()
    End Sub

    Private Sub FrmNEntradasNuevo04_Load(sender As Object, e As EventArgs) Handles Me.Load
        LstEnvases.FullRowSelect = True
        LstEnvases.MultiSelect = False
        Me.Text = "Selección de Envase"
        PoblarListaEnvases()
    End Sub
End Class