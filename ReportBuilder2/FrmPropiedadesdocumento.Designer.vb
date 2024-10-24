<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPropiedadesdocumento
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbvertical = New System.Windows.Forms.RadioButton()
        Me.RbHorizontal = New System.Windows.Forms.RadioButton()
        Me.Copias = New System.Windows.Forms.NumericUpDown()
        Me.TxtImpresora = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.rbPredefinida = New System.Windows.Forms.RadioButton()
        Me.RbPredeterminada = New System.Windows.Forms.RadioButton()
        Me.BtGrabar = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BtCancelar = New System.Windows.Forms.Button()
        Me.cbDocumentoDefecto = New System.Windows.Forms.CheckBox()
        Me.cbPapel = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.Copias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Número de copias"
        '
        'rbvertical
        '
        Me.rbvertical.AutoSize = True
        Me.rbvertical.Checked = True
        Me.rbvertical.Location = New System.Drawing.Point(33, 231)
        Me.rbvertical.Name = "rbvertical"
        Me.rbvertical.Size = New System.Drawing.Size(14, 13)
        Me.rbvertical.TabIndex = 3
        Me.rbvertical.TabStop = True
        Me.rbvertical.UseVisualStyleBackColor = True
        '
        'RbHorizontal
        '
        Me.RbHorizontal.AutoSize = True
        Me.RbHorizontal.Location = New System.Drawing.Point(96, 281)
        Me.RbHorizontal.Name = "RbHorizontal"
        Me.RbHorizontal.Size = New System.Drawing.Size(14, 13)
        Me.RbHorizontal.TabIndex = 4
        Me.RbHorizontal.UseVisualStyleBackColor = True
        '
        'Copias
        '
        Me.Copias.Location = New System.Drawing.Point(119, 12)
        Me.Copias.Name = "Copias"
        Me.Copias.Size = New System.Drawing.Size(37, 20)
        Me.Copias.TabIndex = 5
        Me.Copias.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TxtImpresora
        '
        Me.TxtImpresora.Enabled = False
        Me.TxtImpresora.Location = New System.Drawing.Point(358, 41)
        Me.TxtImpresora.Name = "TxtImpresora"
        Me.TxtImpresora.Size = New System.Drawing.Size(126, 20)
        Me.TxtImpresora.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(20, 204)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(141, 127)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Posición papel"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PictureBox2)
        Me.GroupBox2.Controls.Add(Me.rbPredefinida)
        Me.GroupBox2.Controls.Add(Me.RbPredeterminada)
        Me.GroupBox2.Controls.Add(Me.TxtImpresora)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 59)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(497, 108)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.ReportBuilder2.My.Resources.Resources.impresora_ico
        Me.PictureBox2.Location = New System.Drawing.Point(12, 11)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(78, 97)
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'rbPredefinida
        '
        Me.rbPredefinida.AutoSize = True
        Me.rbPredefinida.Location = New System.Drawing.Point(226, 41)
        Me.rbPredefinida.Name = "rbPredefinida"
        Me.rbPredefinida.Size = New System.Drawing.Size(126, 17)
        Me.rbPredefinida.TabIndex = 8
        Me.rbPredefinida.Text = "Impresora predefinida"
        Me.rbPredefinida.UseVisualStyleBackColor = True
        '
        'RbPredeterminada
        '
        Me.RbPredeterminada.AutoSize = True
        Me.RbPredeterminada.Checked = True
        Me.RbPredeterminada.Location = New System.Drawing.Point(110, 40)
        Me.RbPredeterminada.Name = "RbPredeterminada"
        Me.RbPredeterminada.Size = New System.Drawing.Size(99, 17)
        Me.RbPredeterminada.TabIndex = 7
        Me.RbPredeterminada.TabStop = True
        Me.RbPredeterminada.Text = "Predeterminada"
        Me.RbPredeterminada.UseVisualStyleBackColor = True
        '
        'BtGrabar
        '
        Me.BtGrabar.Image = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.BtGrabar.Location = New System.Drawing.Point(356, 260)
        Me.BtGrabar.Name = "BtGrabar"
        Me.BtGrabar.Size = New System.Drawing.Size(64, 71)
        Me.BtGrabar.TabIndex = 9
        Me.BtGrabar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ReportBuilder2.My.Resources.Resources.posicionpapel11
        Me.PictureBox1.Location = New System.Drawing.Point(20, 222)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(108, 97)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'BtCancelar
        '
        Me.BtCancelar.Image = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable
        Me.BtCancelar.Location = New System.Drawing.Point(465, 260)
        Me.BtCancelar.Name = "BtCancelar"
        Me.BtCancelar.Size = New System.Drawing.Size(64, 71)
        Me.BtCancelar.TabIndex = 10
        Me.BtCancelar.UseVisualStyleBackColor = True
        '
        'cbDocumentoDefecto
        '
        Me.cbDocumentoDefecto.AutoSize = True
        Me.cbDocumentoDefecto.Location = New System.Drawing.Point(409, 15)
        Me.cbDocumentoDefecto.Name = "cbDocumentoDefecto"
        Me.cbDocumentoDefecto.Size = New System.Drawing.Size(120, 17)
        Me.cbDocumentoDefecto.TabIndex = 11
        Me.cbDocumentoDefecto.Text = "Documento defecto"
        Me.cbDocumentoDefecto.UseVisualStyleBackColor = True
        '
        'cbPapel
        '
        Me.cbPapel.FormattingEnabled = True
        Me.cbPapel.Location = New System.Drawing.Point(235, 11)
        Me.cbPapel.Name = "cbPapel"
        Me.cbPapel.Size = New System.Drawing.Size(142, 21)
        Me.cbPapel.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(183, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Tamaño"
        '
        'FrmPropiedadesdocumento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 346)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbPapel)
        Me.Controls.Add(Me.cbDocumentoDefecto)
        Me.Controls.Add(Me.BtCancelar)
        Me.Controls.Add(Me.BtGrabar)
        Me.Controls.Add(Me.Copias)
        Me.Controls.Add(Me.RbHorizontal)
        Me.Controls.Add(Me.rbvertical)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "FrmPropiedadesdocumento"
        Me.Text = "FrmPropiedadesinforme"
        CType(Me.Copias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents rbvertical As RadioButton
    Friend WithEvents RbHorizontal As RadioButton
    Friend WithEvents Copias As NumericUpDown
    Friend WithEvents TxtImpresora As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents rbPredefinida As RadioButton
    Friend WithEvents RbPredeterminada As RadioButton
    Friend WithEvents BtGrabar As Button
    Friend WithEvents BtCancelar As Button
    Friend WithEvents cbDocumentoDefecto As CheckBox
    Friend WithEvents cbPapel As ComboBox
    Friend WithEvents Label2 As Label
End Class
