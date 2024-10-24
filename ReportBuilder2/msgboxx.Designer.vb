'
' Created by SharpDevelop.
' User: Louis
' Date: 1/2/2017
' Time: 8:09 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class msgboxx
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
		Me.richTextBox1 = New System.Windows.Forms.RichTextBox
		Me.button1 = New System.Windows.Forms.Button
		Me.pictureBox1 = New System.Windows.Forms.PictureBox
		Me.button2 = New System.Windows.Forms.Button
		CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'richTextBox1
		'
		Me.richTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.richTextBox1.Location = New System.Drawing.Point(14, 14)
		Me.richTextBox1.Name = "richTextBox1"
		Me.richTextBox1.Size = New System.Drawing.Size(800, 200)
		Me.richTextBox1.TabIndex = 0
		Me.richTextBox1.Text = ""
		'
		'button1
		'
		Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button1.Location = New System.Drawing.Point(695, 269)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(117, 38)
		Me.button1.TabIndex = 1
		Me.button1.Text = "Close"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'pictureBox1
		'
		Me.pictureBox1.Location = New System.Drawing.Point(30, 220)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(129, 86)
		Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pictureBox1.TabIndex = 2
		Me.pictureBox1.TabStop = false
		'
		'button2
		'
		Me.button2.Location = New System.Drawing.Point(217, 248)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(92, 41)
		Me.button2.TabIndex = 3
		Me.button2.Text = "button2"
		Me.button2.UseVisualStyleBackColor = true
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'msgboxx
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(824, 314)
		Me.Controls.Add(Me.button2)
		Me.Controls.Add(Me.pictureBox1)
		Me.Controls.Add(Me.button1)
		Me.Controls.Add(Me.richTextBox1)
		Me.Name = "msgboxx"
		Me.Text = "msgboxx"
		CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
	End Sub
	Private button2 As System.Windows.Forms.Button
	Private pictureBox1 As System.Windows.Forms.PictureBox
	Private button1 As System.Windows.Forms.Button
	Public richTextBox1 As System.Windows.Forms.RichTextBox
End Class
