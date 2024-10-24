'
' Created by SharpDevelop.
' User: Louis
' Date: 1/11/2017
' Time: 11:55 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class Help
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Help))
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.button1 = New System.Windows.Forms.Button()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(123, 98)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox1.TabIndex = 0
        Me.pictureBox1.TabStop = False
        '
        'label1
        '
        Me.label1.Font = New System.Drawing.Font("Segoe UI Light", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(152, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(275, 41)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Generador de informes"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(153, 53)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(252, 56)
        Me.label3.TabIndex = 3
        Me.label3.Text = "Adpatado a las necesidades de la aplicación de la Cooperativa Vinícola de Lliria"
        '
        'button1
        '
        Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button1.Location = New System.Drawing.Point(330, 112)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(97, 35)
        Me.button1.TabIndex = 4
        Me.button1.Text = "Cerrar"
        Me.button1.UseVisualStyleBackColor = True
        '
        'Help
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 159)
        Me.ControlBox = False
        Me.Controls.Add(Me.button1)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.pictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Help"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Acerca de Informes"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents button1 As System.Windows.Forms.Button
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
End Class
