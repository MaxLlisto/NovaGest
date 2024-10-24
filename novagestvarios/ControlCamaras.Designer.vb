<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlCamaras
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.cbSel = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPaletsCamaraT = New System.Windows.Forms.Label()
        Me.lblKgsCamara4 = New System.Windows.Forms.Label()
        Me.lblPaletsCamara4 = New System.Windows.Forms.Label()
        Me.lblKgsCamara3 = New System.Windows.Forms.Label()
        Me.lblPaletsCamara3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Total = New System.Windows.Forms.Label()
        Me.lblKgsCamara2 = New System.Windows.Forms.Label()
        Me.lblPaletsCamara2 = New System.Windows.Forms.Label()
        Me.lblKilosCamaraT = New System.Windows.Forms.Label()
        Me.lblKgsCamara1 = New System.Windows.Forms.Label()
        Me.lblPaletsCamara1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExpExcel = New System.Windows.Forms.Button()
        Me.Vaciarlacamara = New System.Windows.Forms.Button()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(12, 66)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersWidth = 35
        Me.dgv.Size = New System.Drawing.Size(1345, 421)
        Me.dgv.TabIndex = 1
        '
        'cbSel
        '
        Me.cbSel.FormattingEnabled = True
        Me.cbSel.Items.AddRange(New Object() {"Todos     ", "Cámara 1", "Cámara 2", "Cámara 3", "Cámara 4"})
        Me.cbSel.Location = New System.Drawing.Point(28, 21)
        Me.cbSel.Name = "cbSel"
        Me.cbSel.Size = New System.Drawing.Size(251, 21)
        Me.cbSel.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblPaletsCamaraT)
        Me.GroupBox1.Controls.Add(Me.lblKgsCamara4)
        Me.GroupBox1.Controls.Add(Me.lblPaletsCamara4)
        Me.GroupBox1.Controls.Add(Me.lblKgsCamara3)
        Me.GroupBox1.Controls.Add(Me.lblPaletsCamara3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Total)
        Me.GroupBox1.Controls.Add(Me.lblKgsCamara2)
        Me.GroupBox1.Controls.Add(Me.lblPaletsCamara2)
        Me.GroupBox1.Controls.Add(Me.lblKilosCamaraT)
        Me.GroupBox1.Controls.Add(Me.lblKgsCamara1)
        Me.GroupBox1.Controls.Add(Me.lblPaletsCamara1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 493)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1109, 91)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(768, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Total palets"
        '
        'lblPaletsCamaraT
        '
        Me.lblPaletsCamaraT.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblPaletsCamaraT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaletsCamaraT.Location = New System.Drawing.Point(862, 35)
        Me.lblPaletsCamaraT.Name = "lblPaletsCamaraT"
        Me.lblPaletsCamaraT.Size = New System.Drawing.Size(79, 13)
        Me.lblPaletsCamaraT.TabIndex = 31
        Me.lblPaletsCamaraT.Text = "0"
        Me.lblPaletsCamaraT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKgsCamara4
        '
        Me.lblKgsCamara4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblKgsCamara4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKgsCamara4.Location = New System.Drawing.Point(483, 63)
        Me.lblKgsCamara4.Name = "lblKgsCamara4"
        Me.lblKgsCamara4.Size = New System.Drawing.Size(79, 13)
        Me.lblKgsCamara4.TabIndex = 30
        Me.lblKgsCamara4.Text = "0"
        Me.lblKgsCamara4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPaletsCamara4
        '
        Me.lblPaletsCamara4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblPaletsCamara4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaletsCamara4.Location = New System.Drawing.Point(483, 35)
        Me.lblPaletsCamara4.Name = "lblPaletsCamara4"
        Me.lblPaletsCamara4.Size = New System.Drawing.Size(79, 13)
        Me.lblPaletsCamara4.TabIndex = 29
        Me.lblPaletsCamara4.Text = "0"
        Me.lblPaletsCamara4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKgsCamara3
        '
        Me.lblKgsCamara3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblKgsCamara3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKgsCamara3.Location = New System.Drawing.Point(361, 63)
        Me.lblKgsCamara3.Name = "lblKgsCamara3"
        Me.lblKgsCamara3.Size = New System.Drawing.Size(79, 13)
        Me.lblKgsCamara3.TabIndex = 27
        Me.lblKgsCamara3.Text = "0"
        Me.lblKgsCamara3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPaletsCamara3
        '
        Me.lblPaletsCamara3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblPaletsCamara3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaletsCamara3.Location = New System.Drawing.Point(361, 35)
        Me.lblPaletsCamara3.Name = "lblPaletsCamara3"
        Me.lblPaletsCamara3.Size = New System.Drawing.Size(79, 13)
        Me.lblPaletsCamara3.TabIndex = 26
        Me.lblPaletsCamara3.Text = "0"
        Me.lblPaletsCamara3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(510, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Cámara 4"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(392, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Cámara 3"
        '
        'Total
        '
        Me.Total.AutoSize = True
        Me.Total.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total.Location = New System.Drawing.Point(768, 16)
        Me.Total.Name = "Total"
        Me.Total.Size = New System.Drawing.Size(67, 13)
        Me.Total.TabIndex = 23
        Me.Total.Text = "Total Kilos"
        '
        'lblKgsCamara2
        '
        Me.lblKgsCamara2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblKgsCamara2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKgsCamara2.Location = New System.Drawing.Point(239, 63)
        Me.lblKgsCamara2.Name = "lblKgsCamara2"
        Me.lblKgsCamara2.Size = New System.Drawing.Size(79, 13)
        Me.lblKgsCamara2.TabIndex = 21
        Me.lblKgsCamara2.Text = "0"
        Me.lblKgsCamara2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPaletsCamara2
        '
        Me.lblPaletsCamara2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblPaletsCamara2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaletsCamara2.Location = New System.Drawing.Point(239, 35)
        Me.lblPaletsCamara2.Name = "lblPaletsCamara2"
        Me.lblPaletsCamara2.Size = New System.Drawing.Size(79, 13)
        Me.lblPaletsCamara2.TabIndex = 20
        Me.lblPaletsCamara2.Text = "0"
        Me.lblPaletsCamara2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKilosCamaraT
        '
        Me.lblKilosCamaraT.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblKilosCamaraT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKilosCamaraT.Location = New System.Drawing.Point(862, 16)
        Me.lblKilosCamaraT.Name = "lblKilosCamaraT"
        Me.lblKilosCamaraT.Size = New System.Drawing.Size(79, 13)
        Me.lblKilosCamaraT.TabIndex = 19
        Me.lblKilosCamaraT.Text = "0"
        Me.lblKilosCamaraT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKgsCamara1
        '
        Me.lblKgsCamara1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblKgsCamara1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKgsCamara1.Location = New System.Drawing.Point(117, 63)
        Me.lblKgsCamara1.Name = "lblKgsCamara1"
        Me.lblKgsCamara1.Size = New System.Drawing.Size(79, 13)
        Me.lblKgsCamara1.TabIndex = 18
        Me.lblKgsCamara1.Text = "0"
        Me.lblKgsCamara1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPaletsCamara1
        '
        Me.lblPaletsCamara1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblPaletsCamara1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPaletsCamara1.Location = New System.Drawing.Point(117, 35)
        Me.lblPaletsCamara1.Name = "lblPaletsCamara1"
        Me.lblPaletsCamara1.Size = New System.Drawing.Size(79, 13)
        Me.lblPaletsCamara1.TabIndex = 17
        Me.lblPaletsCamara1.Text = "0"
        Me.lblPaletsCamara1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Kgs."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Palets"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(262, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Cámara 2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(144, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Cámara 1"
        '
        'ExpExcel
        '
        Me.ExpExcel.BackgroundImage = Global.Novagestvarios.My.Resources.Resources.excel1
        Me.ExpExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExpExcel.Location = New System.Drawing.Point(1282, 500)
        Me.ExpExcel.Name = "ExpExcel"
        Me.ExpExcel.Size = New System.Drawing.Size(75, 69)
        Me.ExpExcel.TabIndex = 15
        Me.ExpExcel.UseVisualStyleBackColor = True
        '
        'Vaciarlacamara
        '
        Me.Vaciarlacamara.BackgroundImage = Global.Novagestvarios.My.Resources.Resources.camaravacia
        Me.Vaciarlacamara.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Vaciarlacamara.Location = New System.Drawing.Point(505, 1)
        Me.Vaciarlacamara.Name = "Vaciarlacamara"
        Me.Vaciarlacamara.Size = New System.Drawing.Size(59, 59)
        Me.Vaciarlacamara.TabIndex = 14
        Me.Vaciarlacamara.UseVisualStyleBackColor = True
        '
        'ControlCamaras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1371, 590)
        Me.Controls.Add(Me.ExpExcel)
        Me.Controls.Add(Me.Vaciarlacamara)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cbSel)
        Me.Controls.Add(Me.dgv)
        Me.Name = "ControlCamaras"
        Me.Text = "Inventario de palets en cámaras"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents cbSel As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblKgsCamara2 As Label
    Friend WithEvents lblPaletsCamara2 As Label
    Friend WithEvents lblKilosCamaraT As Label
    Friend WithEvents lblKgsCamara1 As Label
    Friend WithEvents lblPaletsCamara1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Vaciarlacamara As Button
    Friend WithEvents ExpExcel As Button
    Friend WithEvents lblKgsCamara4 As Label
    Friend WithEvents lblPaletsCamara4 As Label
    Friend WithEvents lblKgsCamara3 As Label
    Friend WithEvents lblPaletsCamara3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Total As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblPaletsCamaraT As Label
End Class
