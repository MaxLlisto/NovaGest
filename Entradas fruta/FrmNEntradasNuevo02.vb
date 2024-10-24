Public Class FrmNEntradasNuevo02
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public CapatazTransportista As String
    Public pform As Form
    Public OperarioSeleccionado As Long
    'Public codigo As String
    'Public nombre As String

    Private Sub FrmNEntradasNuevo02_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CapatazTransportista = "C" Then
            Me.Text = "Selección de CAPATAZ"
        Else
            Me.Text = "Selección de TRANSPORTISTA"
        End If
    End Sub


    Private Sub lstOperarios_DoubleClick(sender As Object, e As EventArgs) Handles lstOperarios.DoubleClick

        If Not lstOperarios.SelectedItems Is Nothing Then
            OperarioSeleccionado = lstOperarios.SelectedItems(0).SubItems(1).Text
            DialogResult = DialogResult.OK
        Else
                TxtNombre.Focus()
        End If

    End Sub
    Private Sub PoblarListaOperarios(Cadena As String)
        Dim i As Long, j As Long, P1 As Long, P2 As Long, PM As Long
        Dim Aceptable As Boolean, Primero As Boolean, Item As ListViewItem

        lstOperarios.Items.Clear()
        If Trim(Cadena) = "" Then Exit Sub
        If CuantosOperarios = 0 Then Exit Sub
        P1 = 0 : P2 = CuantosOperarios

        Do While (P1 <= P2 And Primero = False)
            PM = Fix((P1 + P2) / 2)
            Aceptable = False : Primero = False
            If Comparar(Strings.Left(UCase(ReferenciaOperario(PM)), Len(Cadena)), UCase(Cadena)) = 0 Then
                Aceptable = True
                If PM = 1 Then
                    Primero = True
                Else
                    If Comparar(Strings.Left(UCase(ReferenciaOperario(PM - 1)), Len(Cadena)), UCase(Cadena)) <> 0 Then Primero = True
                End If
            End If
            If Aceptable = True And Primero = True Then
                P1 = PM : P2 = PM
            Else
                If Comparar(Strings.Left(UCase(ReferenciaOperario(PM)), Len(Cadena)), UCase(Cadena)) >= 0 Then
                    P2 = PM - 1
                Else
                    P1 = PM + 1
                End If
            End If
        Loop

        If Primero = True Then
            For j = PM To CuantosOperarios
                If Comparar(Strings.Left(UCase(ReferenciaOperario(j)), Len(Cadena)), UCase(Cadena)) = 0 Then
                    Item = lstOperarios.Items.Add(Trim(Strings.Left(ReferenciaOperario(j), 40)) + " " + Trim(Mid(ReferenciaOperario(j), 41, 40)))
                    Item.SubItems.Add(CStr(CLng(Strings.Mid(ReferenciaOperario(j), 81))))
                Else
                    Exit For
                End If
            Next
            lstOperarios.Items(1).Selected = True
        End If

    End Sub
    Private Function Comparar(Cadena1 As String, Cadena2 As String) As Integer
        Dim C1 As String, C2 As String, j1 As Integer, j2 As Integer

        C1 = Cadena1
        j1 = InStr(1, C1, Chr(209))
        Do While j1 > 0
            If j1 = 1 Then
                C1 = "Nz" + Mid(C1, 2)
            ElseIf j1 = Len(C1) Then
                C1 = Strings.Left(C1, j1 - 1) + "Nz"
            Else
                C1 = Strings.Left(C1, j1 - 1) + "Nz" + Mid(C1, j1 + 1)
            End If
            j1 = InStr(1, C1, Chr(209))
        Loop
        C2 = Cadena2
        j2 = InStr(1, C2, Chr(209))
        Do While j2 > 0
            If j2 = 1 Then
                C2 = "Nz" + Mid(C2, 2)
            ElseIf j2 = Len(C2) Then
                C2 = Strings.Left(C2, j2 - 1) + "Nz"
            Else
                C2 = Strings.Left(C2, j2 - 1) + "Nz" + Mid(C2, j2 + 1)
            End If
            j2 = InStr(1, C2, Chr(209))
        Loop
        If C1 < C2 Then
            Comparar = -1
        ElseIf C1 = C2 Then
            Comparar = 0
        Else
            Comparar = 1
        End If
    End Function
    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        'DialogResult = DialogResult.OK


    End Sub

    Private Sub TxtNombre_TextChanged(sender As Object, e As EventArgs) Handles TxtNombre.TextChanged
        OperarioSeleccionado = -1
        PoblarListaOperarios(Trim(TxtNombre.Text))
        If OperarioSeleccionado > -1 Then
            DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub lstOperarios_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lstOperarios.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            If Not lstOperarios.SelectedItems Is Nothing Then
                OperarioSeleccionado = lstOperarios.SelectedItems(0).SubItems(1).Text
                If Not lstOperarios.SelectedItems Is Nothing Then
                    OperarioSeleccionado = lstOperarios.SelectedItems(0).SubItems(1).Text
                    DialogResult = DialogResult.OK
                Else
                    TxtNombre.Focus()
                End If
                DialogResult = DialogResult.OK
            Else
                TxtNombre.Focus()
            End If
        End If
    End Sub

    Private Sub lstOperarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstOperarios.SelectedIndexChanged

    End Sub
End Class