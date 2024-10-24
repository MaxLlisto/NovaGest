'
' Created by SharpDevelop.
' User: Toshiba
' Date: 25-May-16
' Time: 11:26 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class PrintPreview
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
        Me.printPreviewControl1 = New System.Windows.Forms.PrintPreviewControl()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.button2 = New System.Windows.Forms.Button()
        Me.button1 = New System.Windows.Forms.Button()
        Me.numericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.numericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.numericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.numericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.panel1.SuspendLayout()
        Me.panel2.SuspendLayout()
        CType(Me.numericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'printPreviewControl1
        '
        Me.printPreviewControl1.Location = New System.Drawing.Point(183, 84)
        Me.printPreviewControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.printPreviewControl1.Name = "printPreviewControl1"
        Me.printPreviewControl1.Size = New System.Drawing.Size(440, 197)
        Me.printPreviewControl1.TabIndex = 0
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.printPreviewControl1)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel1.Location = New System.Drawing.Point(0, 38)
        Me.panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(757, 377)
        Me.panel1.TabIndex = 1
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.button2)
        Me.panel2.Controls.Add(Me.button1)
        Me.panel2.Controls.Add(Me.numericUpDown4)
        Me.panel2.Controls.Add(Me.numericUpDown3)
        Me.panel2.Controls.Add(Me.numericUpDown2)
        Me.panel2.Controls.Add(Me.numericUpDown1)
        Me.panel2.Controls.Add(Me.label8)
        Me.panel2.Controls.Add(Me.label7)
        Me.panel2.Controls.Add(Me.label6)
        Me.panel2.Controls.Add(Me.label5)
        Me.panel2.Controls.Add(Me.label4)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel2.Location = New System.Drawing.Point(0, 0)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(757, 38)
        Me.panel2.TabIndex = 2
        '
        'button2
        '
        Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button2.Location = New System.Drawing.Point(604, 5)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(73, 29)
        Me.button2.TabIndex = 12
        Me.button2.Text = "Cerrar"
        Me.button2.UseVisualStyleBackColor = False
        '
        'button1
        '
        Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button1.Location = New System.Drawing.Point(683, 5)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(71, 29)
        Me.button1.TabIndex = 12
        Me.button1.Text = "Imprimir"
        Me.button1.UseVisualStyleBackColor = False
        '
        'numericUpDown4
        '
        Me.numericUpDown4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numericUpDown4.Location = New System.Drawing.Point(449, 8)
        Me.numericUpDown4.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUpDown4.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUpDown4.Name = "numericUpDown4"
        Me.numericUpDown4.Size = New System.Drawing.Size(50, 24)
        Me.numericUpDown4.TabIndex = 9
        Me.numericUpDown4.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numericUpDown3
        '
        Me.numericUpDown3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numericUpDown3.Location = New System.Drawing.Point(325, 8)
        Me.numericUpDown3.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numericUpDown3.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUpDown3.Name = "numericUpDown3"
        Me.numericUpDown3.Size = New System.Drawing.Size(50, 24)
        Me.numericUpDown3.TabIndex = 8
        Me.numericUpDown3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numericUpDown2
        '
        Me.numericUpDown2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numericUpDown2.Location = New System.Drawing.Point(193, 8)
        Me.numericUpDown2.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUpDown2.Name = "numericUpDown2"
        Me.numericUpDown2.Size = New System.Drawing.Size(50, 24)
        Me.numericUpDown2.TabIndex = 11
        Me.numericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'numericUpDown1
        '
        Me.numericUpDown1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numericUpDown1.Location = New System.Drawing.Point(84, 8)
        Me.numericUpDown1.Maximum = New Decimal(New Integer() {400, 0, 0, 0})
        Me.numericUpDown1.Minimum = New Decimal(New Integer() {25, 0, 0, 0})
        Me.numericUpDown1.Name = "numericUpDown1"
        Me.numericUpDown1.Size = New System.Drawing.Size(50, 24)
        Me.numericUpDown1.TabIndex = 10
        Me.numericUpDown1.Value = New Decimal(New Integer() {101, 0, 0, 0})
        '
        'label8
        '
        Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.Location = New System.Drawing.Point(505, 10)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(101, 19)
        Me.label8.TabIndex = 4
        Me.label8.Text = "de 1 página"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label7
        '
        Me.label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(381, 10)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(62, 19)
        Me.label7.TabIndex = 3
        Me.label7.Text = "Página"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label6
        '
        Me.label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(249, 10)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(70, 19)
        Me.label6.TabIndex = 5
        Me.label6.Text = "Columnas"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label5
        '
        Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(140, 10)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(47, 19)
        Me.label5.TabIndex = 7
        Me.label5.Text = "Filas"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label4
        '
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(3, 10)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(75, 19)
        Me.label4.TabIndex = 6
        Me.label4.Text = "Zoom %"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PrintPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 415)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.panel2)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "PrintPreview"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vista previa"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel1.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        CType(Me.numericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents button2 As System.Windows.Forms.Button
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents numericUpDown1 As System.Windows.Forms.NumericUpDown
    Private WithEvents numericUpDown2 As System.Windows.Forms.NumericUpDown
    Private WithEvents numericUpDown3 As System.Windows.Forms.NumericUpDown
    Private WithEvents numericUpDown4 As System.Windows.Forms.NumericUpDown
    Private WithEvents button1 As System.Windows.Forms.Button
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents printPreviewControl1 As System.Windows.Forms.PrintPreviewControl
End Class
