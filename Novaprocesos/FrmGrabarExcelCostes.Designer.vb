<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGrabarExcelCostes
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGrabarExcelCostes))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DTHastaFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DTDesdeFecha = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB5 = New System.Windows.Forms.RadioButton()
        Me.RB4 = New System.Windows.Forms.RadioButton()
        Me.RB3 = New System.Windows.Forms.RadioButton()
        Me.RB2 = New System.Windows.Forms.RadioButton()
        Me.RB1 = New System.Windows.Forms.RadioButton()
        Me.Grabar = New System.Windows.Forms.Button()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.LblEXCEL = New System.Windows.Forms.Label()
        Me.LblContador = New System.Windows.Forms.Label()
        Me.RB6 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(49, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Desde fecha:"
        '
        'DTHastaFecha
        '
        Me.DTHastaFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTHastaFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTHastaFecha.Location = New System.Drawing.Point(135, 49)
        Me.DTHastaFecha.Name = "DTHastaFecha"
        Me.DTHastaFecha.Size = New System.Drawing.Size(93, 20)
        Me.DTHastaFecha.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(49, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Hasta fecha:"
        '
        'DTDesdeFecha
        '
        Me.DTDesdeFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTDesdeFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTDesdeFecha.Location = New System.Drawing.Point(135, 22)
        Me.DTDesdeFecha.Name = "DTDesdeFecha"
        Me.DTDesdeFecha.Size = New System.Drawing.Size(93, 20)
        Me.DTDesdeFecha.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RB6)
        Me.GroupBox1.Controls.Add(Me.RB5)
        Me.GroupBox1.Controls.Add(Me.RB4)
        Me.GroupBox1.Controls.Add(Me.RB3)
        Me.GroupBox1.Controls.Add(Me.RB2)
        Me.GroupBox1.Controls.Add(Me.RB1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(24, 88)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(502, 42)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Familia:"
        '
        'RB5
        '
        Me.RB5.AutoSize = True
        Me.RB5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB5.Location = New System.Drawing.Point(300, 17)
        Me.RB5.Name = "RB5"
        Me.RB5.Size = New System.Drawing.Size(97, 17)
        Me.RB5.TabIndex = 17
        Me.RB5.Text = "Naranja blanca"
        Me.RB5.UseVisualStyleBackColor = True
        '
        'RB4
        '
        Me.RB4.AutoSize = True
        Me.RB4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB4.Location = New System.Drawing.Point(228, 17)
        Me.RB4.Name = "RB4"
        Me.RB4.Size = New System.Drawing.Size(66, 17)
        Me.RB4.TabIndex = 16
        Me.RB4.Text = "Satsuma"
        Me.RB4.UseVisualStyleBackColor = True
        '
        'RB3
        '
        Me.RB3.AutoSize = True
        Me.RB3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB3.Location = New System.Drawing.Point(159, 17)
        Me.RB3.Name = "RB3"
        Me.RB3.Size = New System.Drawing.Size(65, 17)
        Me.RB3.TabIndex = 15
        Me.RB3.Text = "Híbridos"
        Me.RB3.UseVisualStyleBackColor = True
        '
        'RB2
        '
        Me.RB2.AutoSize = True
        Me.RB2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB2.Location = New System.Drawing.Point(76, 17)
        Me.RB2.Name = "RB2"
        Me.RB2.Size = New System.Drawing.Size(77, 17)
        Me.RB2.TabIndex = 14
        Me.RB2.Text = "Clementina"
        Me.RB2.UseVisualStyleBackColor = True
        '
        'RB1
        '
        Me.RB1.AutoSize = True
        Me.RB1.Checked = True
        Me.RB1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB1.Location = New System.Drawing.Point(9, 17)
        Me.RB1.Name = "RB1"
        Me.RB1.Size = New System.Drawing.Size(62, 17)
        Me.RB1.TabIndex = 13
        Me.RB1.TabStop = True
        Me.RB1.Text = "Naranja"
        Me.RB1.UseVisualStyleBackColor = True
        '
        'Grabar
        '
        Me.Grabar.Image = CType(resources.GetObject("Grabar.Image"), System.Drawing.Image)
        Me.Grabar.Location = New System.Drawing.Point(165, 147)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(42, 39)
        Me.Grabar.TabIndex = 8
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.Image = CType(resources.GetObject("CmdSalir.Image"), System.Drawing.Image)
        Me.CmdSalir.Location = New System.Drawing.Point(861, 10)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(42, 40)
        Me.CmdSalir.TabIndex = 9
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'LblEXCEL
        '
        Me.LblEXCEL.AutoSize = True
        Me.LblEXCEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEXCEL.Location = New System.Drawing.Point(135, 259)
        Me.LblEXCEL.Name = "LblEXCEL"
        Me.LblEXCEL.Size = New System.Drawing.Size(238, 16)
        Me.LblEXCEL.TabIndex = 10
        Me.LblEXCEL.Text = "En proceso grabacion de EXCEL:"
        '
        'LblContador
        '
        Me.LblContador.AutoSize = True
        Me.LblContador.Location = New System.Drawing.Point(380, 259)
        Me.LblContador.Name = "LblContador"
        Me.LblContador.Size = New System.Drawing.Size(64, 13)
        Me.LblContador.TabIndex = 11
        Me.LblContador.Text = "LblContador"
        '
        'RB6
        '
        Me.RB6.AutoSize = True
        Me.RB6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RB6.Location = New System.Drawing.Point(403, 17)
        Me.RB6.Name = "RB6"
        Me.RB6.Size = New System.Drawing.Size(82, 17)
        Me.RB6.TabIndex = 18
        Me.RB6.Text = "Naranja roja"
        Me.RB6.UseVisualStyleBackColor = True
        '
        'FrmGrabarExcelCostes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 509)
        Me.Controls.Add(Me.LblContador)
        Me.Controls.Add(Me.LblEXCEL)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DTDesdeFecha)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DTHastaFecha)
        Me.Name = "FrmGrabarExcelCostes"
        Me.Text = "Grabación de EXCEL de costes"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents DTHastaFecha As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents DTDesdeFecha As DateTimePicker
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RB5 As RadioButton
    Friend WithEvents RB4 As RadioButton
    Friend WithEvents RB3 As RadioButton
    Friend WithEvents RB2 As RadioButton
    Friend WithEvents RB1 As RadioButton
    Friend WithEvents Grabar As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents LblEXCEL As Label
    Friend WithEvents LblContador As Label
    Friend WithEvents RB6 As RadioButton
End Class
