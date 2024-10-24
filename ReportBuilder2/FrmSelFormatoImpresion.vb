Public Class FrmSelFormatoImpresion
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public Proceso As String = ""
    Public documento As String = ""
    Public formato As String = ""

    Public Function RetornaFormato()
        Dim rsFmt As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim item As ListViewItem
        Dim sql As String

        sql = "SELECT * FROM zzformatosdefecto_n WHERE Proceso = '" & Proceso & "'"
        If documento.Trim <> "" Then
            sql = sql + " AND documento = '" & documento & "'"
        End If
        If rsFmt.Open(sql, ObjetoGlobal.cn) Then
            If rsFmt.RecordCount > 1 Then
                While Not rsFmt.EOF
                    item = New ListViewItem(rsFmt!formato.ToString)
                    item.SubItems.Add(rsFmt!copias)
                    item.SubItems.Add("" & rsFmt!impresoradefecto)
                    item.SubItems.Add(rsFmt!documento)
                    lvFormatos.Items.Add(item)
                    rsFmt.MoveNext()
                End While
            ElseIf rsFmt.RecordCount = 1 Then
                documento = rsFmt!documento.ToString
                formato = rsFmt!formato.ToString
                Me.DialogResult = DialogResult.OK
                Me.Close()
            ElseIf rsFmt.RecordCount = 0 Then
                MsgBox(" No encuentro formatos para el proceso " & Proceso.Trim)
                Me.DialogResult = DialogResult.Cancel
            End If
        End If
        rsFmt.Close()

    End Function

    Private Sub BtSeleccionar_Click(sender As Object, e As EventArgs) Handles BtSeleccionar.Click
        Me.DialogResult = DialogResult.OK
        formato = lvFormatos.SelectedItems(0).SubItems(0).Text
        documento = lvFormatos.SelectedItems(0).SubItems(3).Text
        Me.Close()
    End Sub

    Private Sub FrmSelFormatoImpresion_Load(sender As Object, e As EventArgs) Handles Me.Load
        RetornaFormato()
    End Sub

    Private Sub btCancelar_Click(sender As Object, e As EventArgs) Handles btCancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lvFormatos_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvFormatos.MouseDoubleClick
        Me.DialogResult = DialogResult.OK
        formato = lvFormatos.SelectedItems(0).SubItems(0).Text
        documento = lvFormatos.SelectedItems(0).SubItems(3).Text
        Me.Close()
    End Sub
End Class