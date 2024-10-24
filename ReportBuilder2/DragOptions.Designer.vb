'
' Created by SharpDevelop.
' User: Toshiba
' Date: 03-Jun-16
' Time: 7:55 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class DragOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DragOptions))
        Me.label2 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label4 = New System.Windows.Forms.Label()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.pictureBox5 = New System.Windows.Forms.PictureBox()
        Me.pictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.panel1.SuspendLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel2.SuspendLayout()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.Navy
        Me.label2.Location = New System.Drawing.Point(39, 5)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(103, 23)
        Me.label2.TabIndex = 3
        Me.label2.Text = "Duplicar"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label5
        '
        Me.label5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.Color.Navy
        Me.label5.Location = New System.Drawing.Point(39, 28)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(103, 23)
        Me.label5.TabIndex = 2
        Me.label5.Text = "Traer al frente"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label1
        '
        Me.label1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.Navy
        Me.label1.Location = New System.Drawing.Point(45, 40)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(112, 23)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Borrar"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label3
        '
        Me.label3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.Navy
        Me.label3.Location = New System.Drawing.Point(39, 51)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(103, 23)
        Me.label3.TabIndex = 2
        Me.label3.Text = "Llevar abajo"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.panel1.Controls.Add(Me.label4)
        Me.panel1.Location = New System.Drawing.Point(3, 3)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(196, 34)
        Me.panel1.TabIndex = 4
        '
        'label4
        '
        Me.label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label4.AutoEllipsis = True
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(16, 4)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(176, 27)
        Me.label4.TabIndex = 0
        Me.label4.Text = "label4"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pictureBox3
        '
        Me.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox3.Image = CType(resources.GetObject("pictureBox3.Image"), System.Drawing.Image)
        Me.pictureBox3.Location = New System.Drawing.Point(11, 29)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(23, 22)
        Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox3.TabIndex = 9
        Me.pictureBox3.TabStop = False
        '
        'pictureBox2
        '
        Me.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox2.Image = CType(resources.GetObject("pictureBox2.Image"), System.Drawing.Image)
        Me.pictureBox2.Location = New System.Drawing.Point(13, 8)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox2.TabIndex = 8
        Me.pictureBox2.TabStop = False
        '
        'pictureBox1
        '
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(19, 42)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(20, 18)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox1.TabIndex = 7
        Me.pictureBox1.TabStop = False
        '
        'pictureBox4
        '
        Me.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox4.Image = CType(resources.GetObject("pictureBox4.Image"), System.Drawing.Image)
        Me.pictureBox4.Location = New System.Drawing.Point(12, 52)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(23, 22)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox4.TabIndex = 9
        Me.pictureBox4.TabStop = False
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.pictureBox4)
        Me.panel2.Controls.Add(Me.pictureBox3)
        Me.panel2.Controls.Add(Me.pictureBox2)
        Me.panel2.Controls.Add(Me.label2)
        Me.panel2.Controls.Add(Me.label3)
        Me.panel2.Controls.Add(Me.label5)
        Me.panel2.Location = New System.Drawing.Point(6, 66)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(151, 79)
        Me.panel2.TabIndex = 10
        '
        'pictureBox5
        '
        Me.pictureBox5.Image = CType(resources.GetObject("pictureBox5.Image"), System.Drawing.Image)
        Me.pictureBox5.Location = New System.Drawing.Point(161, 127)
        Me.pictureBox5.Name = "pictureBox5"
        Me.pictureBox5.Size = New System.Drawing.Size(35, 29)
        Me.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox5.TabIndex = 11
        Me.pictureBox5.TabStop = False
        Me.pictureBox5.Visible = False
        '
        'pictureBox6
        '
        Me.pictureBox6.Image = CType(resources.GetObject("pictureBox6.Image"), System.Drawing.Image)
        Me.pictureBox6.Location = New System.Drawing.Point(160, 162)
        Me.pictureBox6.Name = "pictureBox6"
        Me.pictureBox6.Size = New System.Drawing.Size(35, 29)
        Me.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pictureBox6.TabIndex = 11
        Me.pictureBox6.TabStop = False
        Me.pictureBox6.Visible = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(18, 149)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox7.TabIndex = 13
        Me.PictureBox7.TabStop = False
        '
        'Label6
        '
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(45, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 23)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Llevar abajo"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox8
        '
        Me.PictureBox8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(18, 172)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox8.TabIndex = 15
        Me.PictureBox8.TabStop = False
        '
        'Label7
        '
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(45, 171)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 23)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Llevar abajo"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox9
        '
        Me.PictureBox9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(18, 195)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox9.TabIndex = 11
        Me.PictureBox9.TabStop = False
        '
        'Label8
        '
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(45, 194)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 23)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Llevar abajo"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox10
        '
        Me.PictureBox10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox10.Image = CType(resources.GetObject("PictureBox10.Image"), System.Drawing.Image)
        Me.PictureBox10.Location = New System.Drawing.Point(18, 221)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox10.TabIndex = 11
        Me.PictureBox10.TabStop = False
        '
        'Label9
        '
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(45, 220)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 23)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Llevar abajo"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox11
        '
        Me.PictureBox11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox11.Image = CType(resources.GetObject("PictureBox11.Image"), System.Drawing.Image)
        Me.PictureBox11.Location = New System.Drawing.Point(18, 244)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox11.TabIndex = 17
        Me.PictureBox11.TabStop = False
        '
        'Label10
        '
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Navy
        Me.Label10.Location = New System.Drawing.Point(45, 243)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(103, 23)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Llevar abajo"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox12
        '
        Me.PictureBox12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox12.Image = CType(resources.GetObject("PictureBox12.Image"), System.Drawing.Image)
        Me.PictureBox12.Location = New System.Drawing.Point(18, 267)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox12.TabIndex = 19
        Me.PictureBox12.TabStop = False
        '
        'Label11
        '
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Navy
        Me.Label11.Location = New System.Drawing.Point(45, 266)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 23)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Llevar abajo"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DragOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.PictureBox12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.PictureBox11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.PictureBox10)
        Me.Controls.Add(Me.PictureBox9)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.PictureBox7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.pictureBox6)
        Me.Controls.Add(Me.pictureBox5)
        Me.Controls.Add(Me.panel2)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "DragOptions"
        Me.Size = New System.Drawing.Size(198, 294)
        Me.panel1.ResumeLayout(False)
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel2.ResumeLayout(False)
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pictureBox6 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox5 As System.Windows.Forms.PictureBox
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents pictureBox4 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox3 As System.Windows.Forms.PictureBox
    Public WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents PictureBox7 As PictureBox
    Private WithEvents Label6 As Label
    Private WithEvents PictureBox8 As PictureBox
    Private WithEvents Label7 As Label
    Private WithEvents PictureBox9 As PictureBox
    Private WithEvents Label8 As Label
    Private WithEvents PictureBox10 As PictureBox
    Private WithEvents Label9 As Label
    Private WithEvents PictureBox11 As PictureBox
    Private WithEvents Label10 As Label
    Private WithEvents PictureBox12 As PictureBox
    Private WithEvents Label11 As Label
End Class
