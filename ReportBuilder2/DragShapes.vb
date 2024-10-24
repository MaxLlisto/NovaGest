'
' Created by SharpDevelop.
' User: Toshiba
' Date: 03-Jun-16
' Time: 7:54 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Partial Class DragShapes
	Inherits System.Windows.Forms.UserControl
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		panel4.Height = 20
	End Sub

    Sub Label1MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox1.MouseDown, label1.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Label", DragDropEffects.Copy)
    End Sub

    Sub Label2MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox2.MouseDown, label2.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Shape", DragDropEffects.Copy)
    End Sub

    Sub Label5MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox3.MouseDown, label5.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Image", DragDropEffects.Copy)
    End Sub

    Sub Label3MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox4.MouseDown, label3.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("DataLabel", DragDropEffects.Copy)
    End Sub

    Sub Label4MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox5.MouseDown, label4.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("DataImage", DragDropEffects.Copy)
    End Sub

    Sub DragShapesLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub shapes(Optional ByVal Tools As Boolean = True)
        If Tools Then
            'msgbox("tools")
            panel5.Visible = False
        Else
            panel5.Visible = True
        End If
    End Sub


    Sub Label9Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox6.Click, label9.Click
        'Remove
        Dim secn As Section
        secn = _DF.propertyGrid1.SelectedObject
        Dim k As Integer = RP.Sections.Count - 1
        For i As Integer = 0 To RP.Sections.Count - 1
            If RP.Sections(k).GroupID = secn.GroupID Then
                RP.Sections.Remove(RP.Sections(k))
            End If
            k -= 1
        Next

        _DF.ShowProperty(Nothing)
        _DF.HideOptions()
        _DF.Reshape()
        _DF.ShowTree()
    End Sub

    Sub Label7Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox7.Click, label7.Click
        'Moveup
        Dim secn As Section
        secn = _DF.propertyGrid1.SelectedObject
        Dim ss As Integer = RP.Sections.IndexOf(secn)

        If ss > 2 Then
            RP.Sections(ss) = RP.Sections(ss - 1)
            RP.Sections(ss - 1) = secn
            _DF.Reshape()
            _DF.ShowProperty(secn)
            _DF.ShowTree()
            _DF.SelectNode(secn.nm)
        End If


    End Sub

    Sub Label8Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox8.Click, label8.Click
        'Move down
        Dim secn As Section
        secn = _DF.propertyGrid1.SelectedObject
        Dim ss As Integer = RP.Sections.IndexOf(secn)

        If ss < RP.Sections.Count - 3 Then
            RP.Sections(ss) = RP.Sections(ss + 1)
            RP.Sections(1 + ss) = secn
            _DF.Reshape()
            _DF.ShowProperty(secn)
            _DF.ShowTree()
            _DF.SelectNode(secn.nm)
        End If
    End Sub


End Class
