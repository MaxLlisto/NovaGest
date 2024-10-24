'
' Created by SharpDevelop.
' User: Louis
' Date: 11/26/2016
' Time: 2:32 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Public Partial Class CustMsg
	Public OK As Boolean = False
	Public cancel As Boolean = False
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub Button1Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
		me.Close
	End Sub
	
	Sub Button2Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
		OK = true
		me.Close
	End Sub
	
	Sub Button3Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
		cancel = true
		me.Close
	End Sub
End Class
