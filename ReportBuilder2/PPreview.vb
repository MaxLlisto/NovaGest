'
' Created by SharpDevelop.
' User: Louis
' Date: 1/25/2017
' Time: 1:10 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Partial Class PPreview
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub PPreviewLoad(ByVal sender As Object, ByVal e As EventArgs)
		Dim x, y As Integer
		x = 3
		y = 20
		For i As Integer = 0 To 3
			Dim pb As New PictureBox
			pb.Width = 800
			pb.Height = 1100
			Dim bm As New Bitmap(pb.Width, pb.Height)
			pb.Image = bm
			pb.BackColor = color.White
			pb.BorderStyle = borderstyle.FixedSingle
			panel1.Controls.Add(pb)
			x = (panel1.Width - pb.Width)/2
			pb.Location = New Point(x, y)
			pb.Anchor = anchorstyles.None or anchorstyles.Top
			y += pb.Height + 3
		Next
	End Sub
End Class
