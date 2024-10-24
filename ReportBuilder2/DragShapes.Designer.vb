'
' Created by SharpDevelop.
' User: Toshiba
' Date: 03-Jun-16
' Time: 7:54 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class DragShapes
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the control.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DragShapes))
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label6 = New System.Windows.Forms.Label()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.pictureBox5 = New System.Windows.Forms.PictureBox()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.label9 = New System.Windows.Forms.Label()
        Me.pictureBox6 = New System.Windows.Forms.PictureBox()
        Me.pictureBox7 = New System.Windows.Forms.PictureBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.pictureBox8 = New System.Windows.Forms.PictureBox()
        Me.panel1.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel2.SuspendLayout()
        Me.panel3.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.panel5.SuspendLayout()
        CType(Me.pictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.Navy
        Me.label1.Location = New System.Drawing.Point(37, 6)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(124, 20)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Texto fijo"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label2
        '
        Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.Navy
        Me.label2.Location = New System.Drawing.Point(37, 31)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(124, 20)
        Me.label2.TabIndex = 0
        Me.label2.Text = "Figura geométrica"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label3
        '
        Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.Navy
        Me.label3.Location = New System.Drawing.Point(37, 91)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(142, 20)
        Me.label3.TabIndex = 0
        Me.label3.Text = "Campo alfanumérico"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label4
        '
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.Color.Navy
        Me.label4.Location = New System.Drawing.Point(37, 117)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(124, 20)
        Me.label4.TabIndex = 0
        Me.label4.Text = "Campo de imagen"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label5
        '
        Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.Color.Navy
        Me.label5.Location = New System.Drawing.Point(37, 57)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(124, 20)
        Me.label5.TabIndex = 0
        Me.label5.Text = "Imagen"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.panel1.Controls.Add(Me.label6)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(198, 34)
        Me.panel1.TabIndex = 5
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(35, 7)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(141, 18)
        Me.label6.TabIndex = 0
        Me.label6.Text = "Insertar controles"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pictureBox1
        '
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(11, 7)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox1.TabIndex = 6
        Me.pictureBox1.TabStop = False
        '
        'pictureBox2
        '
        Me.pictureBox2.Image = CType(resources.GetObject("pictureBox2.Image"), System.Drawing.Image)
        Me.pictureBox2.Location = New System.Drawing.Point(11, 32)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox2.TabIndex = 6
        Me.pictureBox2.TabStop = False
        '
        'pictureBox3
        '
        Me.pictureBox3.Image = CType(resources.GetObject("pictureBox3.Image"), System.Drawing.Image)
        Me.pictureBox3.Location = New System.Drawing.Point(11, 57)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox3.TabIndex = 6
        Me.pictureBox3.TabStop = False
        '
        'pictureBox4
        '
        Me.pictureBox4.Image = CType(resources.GetObject("pictureBox4.Image"), System.Drawing.Image)
        Me.pictureBox4.Location = New System.Drawing.Point(11, 91)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox4.TabIndex = 6
        Me.pictureBox4.TabStop = False
        '
        'pictureBox5
        '
        Me.pictureBox5.Image = CType(resources.GetObject("pictureBox5.Image"), System.Drawing.Image)
        Me.pictureBox5.Location = New System.Drawing.Point(11, 118)
        Me.pictureBox5.Name = "pictureBox5"
        Me.pictureBox5.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox5.TabIndex = 6
        Me.pictureBox5.TabStop = False
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.label1)
        Me.panel2.Controls.Add(Me.pictureBox5)
        Me.panel2.Controls.Add(Me.label3)
        Me.panel2.Controls.Add(Me.pictureBox4)
        Me.panel2.Controls.Add(Me.label5)
        Me.panel2.Controls.Add(Me.pictureBox3)
        Me.panel2.Controls.Add(Me.label2)
        Me.panel2.Controls.Add(Me.pictureBox2)
        Me.panel2.Controls.Add(Me.label4)
        Me.panel2.Controls.Add(Me.pictureBox1)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel2.Location = New System.Drawing.Point(0, 118)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(198, 144)
        Me.panel2.TabIndex = 7
        '
        'panel3
        '
        Me.panel3.AutoScroll = True
        Me.panel3.Controls.Add(Me.panel4)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel3.Location = New System.Drawing.Point(0, 0)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(198, 295)
        Me.panel3.TabIndex = 8
        '
        'panel4
        '
        Me.panel4.AutoSize = True
        Me.panel4.Controls.Add(Me.panel2)
        Me.panel4.Controls.Add(Me.panel5)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel4.Location = New System.Drawing.Point(0, 0)
        Me.panel4.Name = "panel4"
        Me.panel4.Padding = New System.Windows.Forms.Padding(0, 35, 0, 0)
        Me.panel4.Size = New System.Drawing.Size(198, 262)
        Me.panel4.TabIndex = 9
        '
        'panel5
        '
        Me.panel5.Controls.Add(Me.label9)
        Me.panel5.Controls.Add(Me.pictureBox6)
        Me.panel5.Controls.Add(Me.pictureBox7)
        Me.panel5.Controls.Add(Me.label7)
        Me.panel5.Controls.Add(Me.label8)
        Me.panel5.Controls.Add(Me.pictureBox8)
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel5.Location = New System.Drawing.Point(0, 35)
        Me.panel5.Name = "panel5"
        Me.panel5.Size = New System.Drawing.Size(198, 83)
        Me.panel5.TabIndex = 8
        Me.panel5.Visible = False
        '
        'label9
        '
        Me.label9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.Color.Navy
        Me.label9.Location = New System.Drawing.Point(37, 5)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(124, 20)
        Me.label9.TabIndex = 0
        Me.label9.Text = "Borrar"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pictureBox6
        '
        Me.pictureBox6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox6.Image = CType(resources.GetObject("pictureBox6.Image"), System.Drawing.Image)
        Me.pictureBox6.Location = New System.Drawing.Point(11, 6)
        Me.pictureBox6.Name = "pictureBox6"
        Me.pictureBox6.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox6.TabIndex = 6
        Me.pictureBox6.TabStop = False
        '
        'pictureBox7
        '
        Me.pictureBox7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox7.Image = CType(resources.GetObject("pictureBox7.Image"), System.Drawing.Image)
        Me.pictureBox7.Location = New System.Drawing.Point(11, 31)
        Me.pictureBox7.Name = "pictureBox7"
        Me.pictureBox7.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox7.TabIndex = 6
        Me.pictureBox7.TabStop = False
        '
        'label7
        '
        Me.label7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.Navy
        Me.label7.Location = New System.Drawing.Point(37, 30)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(124, 20)
        Me.label7.TabIndex = 0
        Me.label7.Text = "Subir"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label8
        '
        Me.label8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.Color.Navy
        Me.label8.Location = New System.Drawing.Point(37, 56)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(124, 20)
        Me.label8.TabIndex = 0
        Me.label8.Text = "Bajar"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pictureBox8
        '
        Me.pictureBox8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox8.Image = CType(resources.GetObject("pictureBox8.Image"), System.Drawing.Image)
        Me.pictureBox8.Location = New System.Drawing.Point(11, 56)
        Me.pictureBox8.Name = "pictureBox8"
        Me.pictureBox8.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox8.TabIndex = 6
        Me.pictureBox8.TabStop = False
        '
        'DragShapes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.panel3)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "DragShapes"
        Me.Size = New System.Drawing.Size(198, 295)
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel2.ResumeLayout(False)
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel5.ResumeLayout(False)
        CType(Me.pictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pictureBox8 As System.Windows.Forms.PictureBox
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents pictureBox7 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox6 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents pictureBox5 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox4 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label

End Class
