<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcomprobacionretenciones
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
        Me.cbEmpresa = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtCargar = New System.Windows.Forms.Button()
        Me.DGComprobaciones = New System.Windows.Forms.DataGridView()
        Me.Salir = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbRetenciones = New System.Windows.Forms.RadioButton()
        Me.rbAportaciones = New System.Windows.Forms.RadioButton()
        CType(Me.DGComprobaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbEmpresa
        '
        Me.cbEmpresa.FormattingEnabled = True
        Me.cbEmpresa.Location = New System.Drawing.Point(108, 8)
        Me.cbEmpresa.Name = "cbEmpresa"
        Me.cbEmpresa.Size = New System.Drawing.Size(279, 21)
        Me.cbEmpresa.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 20)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Empresa.:"
        '
        'BtCargar
        '
        Me.BtCargar.Location = New System.Drawing.Point(393, 6)
        Me.BtCargar.Name = "BtCargar"
        Me.BtCargar.Size = New System.Drawing.Size(113, 23)
        Me.BtCargar.TabIndex = 13
        Me.BtCargar.Text = "Cargar"
        Me.BtCargar.UseVisualStyleBackColor = True
        '
        'DGComprobaciones
        '
        Me.DGComprobaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGComprobaciones.Location = New System.Drawing.Point(16, 47)
        Me.DGComprobaciones.Name = "DGComprobaciones"
        Me.DGComprobaciones.Size = New System.Drawing.Size(772, 325)
        Me.DGComprobaciones.TabIndex = 14
        '
        'Salir
        '
        Me.Salir.Image = Global.Novagestvarios.My.Resources.Resources.salir
        Me.Salir.Location = New System.Drawing.Point(714, 378)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(74, 65)
        Me.Salir.TabIndex = 15
        Me.Salir.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbAportaciones)
        Me.GroupBox1.Controls.Add(Me.rbRetenciones)
        Me.GroupBox1.Location = New System.Drawing.Point(512, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(276, 31)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        '
        'rbRetenciones
        '
        Me.rbRetenciones.AutoSize = True
        Me.rbRetenciones.Checked = True
        Me.rbRetenciones.Location = New System.Drawing.Point(18, 11)
        Me.rbRetenciones.Name = "rbRetenciones"
        Me.rbRetenciones.Size = New System.Drawing.Size(85, 17)
        Me.rbRetenciones.TabIndex = 0
        Me.rbRetenciones.TabStop = True
        Me.rbRetenciones.Text = "Retenciones"
        Me.rbRetenciones.UseVisualStyleBackColor = True
        '
        'rbAportaciones
        '
        Me.rbAportaciones.AutoSize = True
        Me.rbAportaciones.Location = New System.Drawing.Point(157, 11)
        Me.rbAportaciones.Name = "rbAportaciones"
        Me.rbAportaciones.Size = New System.Drawing.Size(87, 17)
        Me.rbAportaciones.TabIndex = 1
        Me.rbAportaciones.Text = "Aportaciones"
        Me.rbAportaciones.UseVisualStyleBackColor = True
        '
        'frmcomprobacionretenciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.DGComprobaciones)
        Me.Controls.Add(Me.BtCargar)
        Me.Controls.Add(Me.cbEmpresa)
        Me.Controls.Add(Me.Label3)
        Me.Name = "frmcomprobacionretenciones"
        Me.Text = "frmcomprobacionretenciones"
        CType(Me.DGComprobaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbEmpresa As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents BtCargar As Button
    Friend WithEvents DGComprobaciones As DataGridView
    Friend WithEvents Salir As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbAportaciones As RadioButton
    Friend WithEvents rbRetenciones As RadioButton
End Class
