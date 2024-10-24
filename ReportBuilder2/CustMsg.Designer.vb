'
' Created by SharpDevelop.
' User: Louis
' Date: 11/26/2016
' Time: 2:32 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class CustMsg
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
        Me.button1 = New System.Windows.Forms.Button()
        Me.button2 = New System.Windows.Forms.Button()
        Me.msg = New System.Windows.Forms.Label()
        Me.button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'button1
        '
        Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.button1.Location = New System.Drawing.Point(444, 111)
        Me.button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(78, 35)
        Me.button1.TabIndex = 0
        Me.button1.Text = "Cerrar"
        Me.button1.UseVisualStyleBackColor = False
        '
        'button2
        '
        Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.button2.Location = New System.Drawing.Point(222, 111)
        Me.button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(91, 35)
        Me.button2.TabIndex = 0
        Me.button2.Text = "Correcto"
        Me.button2.UseVisualStyleBackColor = False
        '
        'msg
        '
        Me.msg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.msg.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.msg.ForeColor = System.Drawing.SystemColors.MenuText
        Me.msg.Location = New System.Drawing.Point(28, 9)
        Me.msg.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.msg.Name = "msg"
        Me.msg.Size = New System.Drawing.Size(654, 88)
        Me.msg.TabIndex = 1
        Me.msg.Text = "label1"
        Me.msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'button3
        '
        Me.button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.button3.Location = New System.Drawing.Point(332, 111)
        Me.button3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(92, 35)
        Me.button3.TabIndex = 0
        Me.button3.Text = "Cancelar"
        Me.button3.UseVisualStyleBackColor = False
        Me.button3.Visible = False
        '
        'CustMsg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(714, 158)
        Me.Controls.Add(Me.msg)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button3)
        Me.Controls.Add(Me.button1)
        Me.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Name = "CustMsg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Aviso"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents button3 As System.Windows.Forms.Button
    Public WithEvents msg As System.Windows.Forms.Label
    Public WithEvents button2 As System.Windows.Forms.Button
    Public WithEvents button1 As System.Windows.Forms.Button
End Class
