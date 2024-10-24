'
' Created by SharpDevelop.
' User: Louis
' Date: 1/25/2017
' Time: 1:10 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class PPreview
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.panel1 = New System.Windows.Forms.Panel
		Me.SuspendLayout
		'
		'panel1
		'
		Me.panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.panel1.AutoScroll = true
		Me.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark
		Me.panel1.Location = New System.Drawing.Point(9, 35)
		Me.panel1.Margin = New System.Windows.Forms.Padding(10)
		Me.panel1.Name = "panel1"
		Me.panel1.Padding = New System.Windows.Forms.Padding(10)
		Me.panel1.Size = New System.Drawing.Size(782, 214)
		Me.panel1.TabIndex = 0
		'
		'PPreview
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(801, 261)
		Me.Controls.Add(Me.panel1)
		Me.Name = "PPreview"
        Me.Text = "Vista previa"
        AddHandler Load, AddressOf Me.PPreviewLoad
		Me.ResumeLayout(false)
	End Sub
	Private panel1 As System.Windows.Forms.Panel
End Class
