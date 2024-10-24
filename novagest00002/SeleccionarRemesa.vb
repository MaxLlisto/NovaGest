Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class SeleccionarRemesa
    Public ObjetoGlobal As Object
    Public RemesaAsociada As String
    Dim aAsociadas() As String

    Private Sub CmdAsociar_Click(sender As Object, e As EventArgs) Handles CmdAsociar.Click
        Dim numasociadas As Integer
        Dim i As Integer

        Try

            numasociadas = 0

            If lwAsociados.SelectedItems.Count = 0 And lwAsociados.CheckedItems.Count = 0 Then
                Return
            End If

            For i = 0 To lwAsociados.Items.Count - 1
                If lwAsociados.Items.Item(i).Checked Then
                    numasociadas = numasociadas + 1
                End If
            Next i

            If numasociadas = 0 Then
                If lwAsociados.SelectedItems.Count = 1 Then
                    ReDim aAsociadas(0)
                    aAsociadas(0) = lwAsociados.SelectedItems.Item(0).Tag
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                    Return
                End If
            End If

            If numasociadas = 1 Then
                ReDim aAsociadas(0)
                aAsociadas(0) = lwAsociados.CheckedItems.Item(0).Tag 'lwAsociados.SelectedItems.Item(0).Tag    'lwAsociados.SelectedItem.Tag
                Me.DialogResult = DialogResult.OK
                Me.Close()
            ElseIf numasociadas > 1 Then
                ReDim aAsociadas(numasociadas - 1)
                RemesaAsociada = "VARIAS"
                numasociadas = 0
                For i = 0 To lwAsociados.CheckedItems.Count - 1
                    aAsociadas(numasociadas) = lwAsociados.CheckedItems.Item(i).Tag
                    numasociadas = numasociadas + 1
                Next
                Me.DialogResult = DialogResult.OK
                Me.Close()
                Return
            Else
                RemesaAsociada = "NINGUNA"
                CargarItems()
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
                MsgBox("No has seleccionado ninguna remesa")
                Return
            End If
        Catch ex As Exception
            MsgBox("Se ha producido un error en la selección: " & ex.Message.Trim & "-" & ex.ToString)
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Try

    End Sub
    Public Function ListaRemesas() As String
        Dim i As Integer
        Dim Ret As String = ""

        If aAsociadas.Length = 1 Then
            Ret = "'" & CStr(aAsociadas(0)) & "'"
        Else
            For i = 0 To aAsociadas.Length - 1
                If Ret = "" Then
                    Ret = "'" & CStr(aAsociadas(0)) & "'"
                Else
                    Ret = Ret + "," + "'" & CStr(aAsociadas(i)) & "'"
                End If
            Next
        end if
    return Ret
End Function

    Private Sub CargarItems()
        Dim X
        Dim RsDocsAso As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSQL As String
        Dim i As Integer

        sSQL = "Select proceso, count(*) as NumRegistros, sum(importe) as total FROM TEMP_CARGOS_ABONOS group by proceso order by proceso desc"

        lwAsociados.FullRowSelect = True

        lwAsociados.Items.Clear()
        RsDocsAso.Open(sSQL, ObjetoGlobal.cn)

        Dim arrLVItem(RsDocsAso.RecordCount) As ListViewItem
        Dim items As New List(Of ListViewItem)

        i = 0
        While Not RsDocsAso.EOF
            arrLVItem(i) = New ListViewItem
            arrLVItem(i).SubItems(0).Text = RsDocsAso!Proceso
            arrLVItem(i).SubItems.Add("" & RsDocsAso!NumRegistros)
            arrLVItem(i).SubItems.Add("" & RsDocsAso!Total)
            arrLVItem(i).Tag = RsDocsAso!Proceso
            lwAsociados.Items.Add(arrLVItem(i))
            RsDocsAso.MoveNext()
            i = i + 1
        End While
        If i > 0 Then
            lwAsociados.Items.Item(1).Focused = True
        End If

        'lwAsociados.Items.AddRange(arrLVItem)
        RsDocsAso.Close()
        'lwAsociados.Items.Item(lwAsociados.Items.Count - 1).Focused = True
        'lwAsociados.Items.Item(lwAsociados.Items.Count - 1).Selected = True 'SelectedItems.Item(lwAsociados.Items.Count - 1)
        'lwAsociados.FocusedItem = lwAsociados.SelectedItems(0)

    End Sub


    Public Function DevolverRelacion()
        DevolverRelacion = aAsociadas
    End Function

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub SeleccionarRemesa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarItems()
    End Sub

    Private Sub cbVarias_CheckedChanged(sender As Object, e As EventArgs) Handles cbVarias.CheckedChanged
        If lwAsociados.CheckBoxes Then
            lwAsociados.CheckBoxes = False
        Else
            lwAsociados.CheckBoxes = True
        End If
    End Sub


End Class