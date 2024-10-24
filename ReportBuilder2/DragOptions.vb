'
' Created by SharpDevelop.
' User: Toshiba
' Date: 03-Jun-16
' Time: 7:55 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Partial Class DragOptions
    Inherits System.Windows.Forms.UserControl
    Public nis As SelectedItems

    Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Public Sub Show(Optional ByVal tools As Boolean = True)
		If tools Then
			panel2.Visible = True
			label1.Text = "Remove"
			picturebox1.Image = picturebox5.Image
		Else
			panel2.Visible = false
			label1.Text = "Add Group"
			picturebox1.Image = picturebox6.Image
			label4.Text = RP.Name
		End If
	End Sub

    Public Sub Label1Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox1.Click, label1.Click


        If nis.Seleccionados.Count = 0 Then
            If panel2.Visible = True Then
                'Remove
                Dim secn As Section
                For j As Integer = 0 To RP.Sections.Count - 1
                    secn = RP.Sections(j)
                    For i As Integer = 0 To secn.AR.Count - 1
                        Dim itm As Item = secn.AR(i)
                        If itm.selected Then
                            secn.AR.Remove(itm)
                            _DF.Reshape()
                            _DF.ShowTree()
                            _DF.HideOptions()
                            _DF.ShowProperty(Nothing)
                            Exit For
                        End If
                    Next
                Next
            Else
                Dim str As String = "GroupHeader"
                Dim i As Integer = 0
                Dim found As Boolean = False
                Do
                    i += 1
                    found = False
                    For j As Integer = 0 To RP.Sections.Count - 1
                        If LCase(RP.Sections(j).nm) = LCase(str & i) Then
                            'msgbox(i)
                            found = True
                        End If
                    Next
                Loop While found


                'msgbox(i)
                Dim secn As New SectionGroup
                Dim grpID As Integer = RP.Sections.Count
                secn.nm = "GroupFooter" & i
                secn.headerLabel = secn.nm & " <Unbound>"
                secn.GroupID = grpID
                secn.h = 40
                RP.Sections.Insert(RP.Sections.Count - 2, secn)

                secn = New SectionGroup
                secn.nm = "GroupHeader" & i
                secn.headerLabel = secn.nm & " <Unbound>"
                secn.GroupID = grpID
                secn.h = 40
                RP.Sections.Insert(2, secn)

                _DF.Reshape()
                _DF.ShowTree()

            End If
        Else
            For i As Integer = 0 To RP.Sections.Count - 1
                Dim secn As Section
                secn = RP.Sections(i)
                If secn.selected Then
                    For Each itm As Item In nis.Seleccionados
                        secn.AR.Remove(itm)
                    Next
                    _DF.Reshape()
                    _DF.ShowTree()
                    _DF.HideOptions()
                    _DF.ShowProperty(Nothing)
                    nis.Limpiar()
                    Exit For
                End If
            Next
        End If

    End Sub

    Public Sub Label2Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox2.Click, label2.Click
        'Duplicate
        Dim secn As Section
        For j As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(j)
            For i As Integer = 0 To secn.AR.Count - 1
                Dim itm As Item = secn.AR(i)
                If itm.selected Then
                    Dim ni As Item
                    ni = itm.clone()
                    ni.nm = NewItem(ni.ItemType, secn.AR)
                    ni.Rec.X += 5
                    ni.Rec.Y += 5
                    secn.AR.Add(ni)
                    _DF.ShowTree()
                    _DF.Reshape()
                    _DF.HideOptions()
                    Exit For
                End If
            Next
        Next
    End Sub

    Public Sub Label5Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox3.Click, label5.Click
        'Bring to front
        'Dim tmp As Object
        Dim secn As Section
        For j As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(j)
            For i As Integer = 0 To secn.AR.Count - 1
                Dim itm As Item = secn.AR(i)
                If itm.selected And i < secn.AR.Count - 1 Then
                    secn.AR(i) = secn.AR(i + 1)
                    secn.AR(i + 1) = itm
                    _DF.ShowTree()
                    _DF.Reshape()
                    Exit For
                End If
            Next
        Next
    End Sub

    Public Sub Label0Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox3.Click, label5.Click
        Dim secn As Section

        If nis.Seleccionados.Count < 1 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    Dim itm As Item = secn.AR(i)
                    If itm.selected Then
                        itm.img = Nothing
                        _DF.Reshape()
                        Exit For
                    End If
                Next
            Next
        Else
            For Each itm As Item In nis.Seleccionados
                Dim t As Item
                t = itm
                t.img = Nothing
                _DF.Reshape()
            Next
        End If


    End Sub

    Public Sub RemoveImage()
        Dim secn As Section

        If nis.Seleccionados.Count < 1 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    Dim itm As Item = secn.AR(i)
                    If itm.selected Then
                        itm.img = Nothing
                        _DF.Reshape()
                        Exit For
                    End If
                Next
            Next
        Else
            For Each itm As Item In nis.Seleccionados
                Dim t As Item
                t = itm
                t.img = Nothing
                _DF.Reshape()
            Next
        End If


    End Sub

    Public Sub Label3Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox4.Click, label3.Click
        'Send to back
        Dim secn As Section
        For j As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(j)
            For i As Integer = 0 To secn.AR.Count - 1
                Dim itm As Item = secn.AR(i)
                If itm.selected And i > 0 Then
                    secn.AR(i) = secn.AR(i - 1)
                    secn.AR(i - 1) = itm
                    _DF.ShowTree()
                    _DF.Reshape()
                    Exit For
                End If
            Next
        Next

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub
End Class
