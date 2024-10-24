'
' Created by SharpDevelop.
' User: Louis
' Date: 1/2/2017
' Time: 8:09 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Partial Class msgboxx
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub Button1Click(ByVal sender As Object, ByVal e As EventArgs)
		me.Close
	End Sub
	
	Sub Button2Click(ByVal sender As Object, ByVal e As EventArgs)
		picturebox1.Image = Base64ToImage(richtextbox1.Text)
	End Sub
End Class
