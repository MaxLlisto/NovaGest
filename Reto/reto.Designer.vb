<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reto
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TbCompras = New System.Windows.Forms.TabPage()
        Me.DGVCompras = New System.Windows.Forms.DataGridView()
        Me.TbVentas = New System.Windows.Forms.TabPage()
        Me.DGVVentas = New System.Windows.Forms.DataGridView()
        Me.TbAdquiciones = New System.Windows.Forms.TabPage()
        Me.DGVAdquisiciones = New System.Windows.Forms.DataGridView()
        Me.TbAplicaciones = New System.Windows.Forms.TabPage()
        Me.DGVAplicaciones = New System.Windows.Forms.DataGridView()
        Me.DPDesdeFecha = New System.Windows.Forms.DateTimePicker()
        Me.DPHastaFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.borrar = New System.Windows.Forms.Button()
        Me.VerErrores = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Exportar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Excel = New System.Windows.Forms.Button()
        Me.BtBuscar = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TbCompras.SuspendLayout()
        CType(Me.DGVCompras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbVentas.SuspendLayout()
        CType(Me.DGVVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbAdquiciones.SuspendLayout()
        CType(Me.DGVAdquisiciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TbAplicaciones.SuspendLayout()
        CType(Me.DGVAplicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TbCompras)
        Me.TabControl1.Controls.Add(Me.TbVentas)
        Me.TabControl1.Controls.Add(Me.TbAdquiciones)
        Me.TabControl1.Controls.Add(Me.TbAplicaciones)
        Me.TabControl1.Location = New System.Drawing.Point(-4, 97)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1001, 463)
        Me.TabControl1.TabIndex = 0
        '
        'TbCompras
        '
        Me.TbCompras.Controls.Add(Me.DGVCompras)
        Me.TbCompras.Location = New System.Drawing.Point(4, 22)
        Me.TbCompras.Name = "TbCompras"
        Me.TbCompras.Padding = New System.Windows.Forms.Padding(3)
        Me.TbCompras.Size = New System.Drawing.Size(993, 437)
        Me.TbCompras.TabIndex = 0
        Me.TbCompras.Text = "Compras"
        Me.TbCompras.UseVisualStyleBackColor = True
        '
        'DGVCompras
        '
        Me.DGVCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVCompras.Location = New System.Drawing.Point(3, 6)
        Me.DGVCompras.Name = "DGVCompras"
        Me.DGVCompras.Size = New System.Drawing.Size(984, 425)
        Me.DGVCompras.TabIndex = 0
        '
        'TbVentas
        '
        Me.TbVentas.Controls.Add(Me.DGVVentas)
        Me.TbVentas.Location = New System.Drawing.Point(4, 22)
        Me.TbVentas.Name = "TbVentas"
        Me.TbVentas.Padding = New System.Windows.Forms.Padding(3)
        Me.TbVentas.Size = New System.Drawing.Size(993, 437)
        Me.TbVentas.TabIndex = 1
        Me.TbVentas.Text = "Ventas"
        Me.TbVentas.UseVisualStyleBackColor = True
        '
        'DGVVentas
        '
        Me.DGVVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVVentas.Location = New System.Drawing.Point(4, 6)
        Me.DGVVentas.Name = "DGVVentas"
        Me.DGVVentas.Size = New System.Drawing.Size(984, 425)
        Me.DGVVentas.TabIndex = 1
        '
        'TbAdquiciones
        '
        Me.TbAdquiciones.Controls.Add(Me.DGVAdquisiciones)
        Me.TbAdquiciones.Location = New System.Drawing.Point(4, 22)
        Me.TbAdquiciones.Name = "TbAdquiciones"
        Me.TbAdquiciones.Size = New System.Drawing.Size(993, 437)
        Me.TbAdquiciones.TabIndex = 2
        Me.TbAdquiciones.Text = "Adquisiciones"
        Me.TbAdquiciones.UseVisualStyleBackColor = True
        '
        'DGVAdquisiciones
        '
        Me.DGVAdquisiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVAdquisiciones.Location = New System.Drawing.Point(4, 6)
        Me.DGVAdquisiciones.Name = "DGVAdquisiciones"
        Me.DGVAdquisiciones.Size = New System.Drawing.Size(984, 425)
        Me.DGVAdquisiciones.TabIndex = 1
        '
        'TbAplicaciones
        '
        Me.TbAplicaciones.Controls.Add(Me.DGVAplicaciones)
        Me.TbAplicaciones.Location = New System.Drawing.Point(4, 22)
        Me.TbAplicaciones.Name = "TbAplicaciones"
        Me.TbAplicaciones.Size = New System.Drawing.Size(993, 437)
        Me.TbAplicaciones.TabIndex = 3
        Me.TbAplicaciones.Text = "Aplicaciones"
        Me.TbAplicaciones.UseVisualStyleBackColor = True
        '
        'DGVAplicaciones
        '
        Me.DGVAplicaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVAplicaciones.Location = New System.Drawing.Point(4, 6)
        Me.DGVAplicaciones.Name = "DGVAplicaciones"
        Me.DGVAplicaciones.Size = New System.Drawing.Size(984, 425)
        Me.DGVAplicaciones.TabIndex = 1
        '
        'DPDesdeFecha
        '
        Me.DPDesdeFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DPDesdeFecha.Location = New System.Drawing.Point(156, 37)
        Me.DPDesdeFecha.Name = "DPDesdeFecha"
        Me.DPDesdeFecha.Size = New System.Drawing.Size(103, 20)
        Me.DPDesdeFecha.TabIndex = 14
        '
        'DPHastaFecha
        '
        Me.DPHastaFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DPHastaFecha.Location = New System.Drawing.Point(156, 63)
        Me.DPHastaFecha.Name = "DPHastaFecha"
        Me.DPHastaFecha.Size = New System.Drawing.Size(103, 20)
        Me.DPHastaFecha.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(153, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Entre las fechas:"
        '
        'borrar
        '
        Me.borrar.Image = Global.reto.My.Resources.Resources.papelera
        Me.borrar.Location = New System.Drawing.Point(644, 9)
        Me.borrar.Name = "borrar"
        Me.borrar.Size = New System.Drawing.Size(78, 76)
        Me.borrar.TabIndex = 20
        Me.borrar.UseVisualStyleBackColor = True
        '
        'VerErrores
        '
        Me.VerErrores.Image = Global.reto.My.Resources.Resources.mensaje_de_error11
        Me.VerErrores.Location = New System.Drawing.Point(389, 7)
        Me.VerErrores.Name = "VerErrores"
        Me.VerErrores.Size = New System.Drawing.Size(83, 78)
        Me.VerErrores.TabIndex = 19
        Me.VerErrores.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.reto.My.Resources.Resources.RETOS
        Me.PictureBox1.Location = New System.Drawing.Point(12, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(90, 80)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 18
        Me.PictureBox1.TabStop = False
        '
        'Exportar
        '
        Me.Exportar.Image = Global.reto.My.Resources.Resources.exportar
        Me.Exportar.Location = New System.Drawing.Point(522, 9)
        Me.Exportar.Name = "Exportar"
        Me.Exportar.Size = New System.Drawing.Size(75, 77)
        Me.Exportar.TabIndex = 17
        Me.Exportar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.Image = Global.reto.My.Resources.Resources.puerta3
        Me.Salir.Location = New System.Drawing.Point(896, 12)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(77, 72)
        Me.Salir.TabIndex = 13
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Excel
        '
        Me.Excel.Image = Global.reto.My.Resources.Resources.icons8_ms_excel_64
        Me.Excel.Location = New System.Drawing.Point(802, 12)
        Me.Excel.Name = "Excel"
        Me.Excel.Size = New System.Drawing.Size(81, 72)
        Me.Excel.TabIndex = 12
        Me.Excel.UseVisualStyleBackColor = True
        '
        'BtBuscar
        '
        Me.BtBuscar.Image = Global.reto.My.Resources.Resources.icons8_buscar_chat_50
        Me.BtBuscar.Location = New System.Drawing.Point(277, 4)
        Me.BtBuscar.Name = "BtBuscar"
        Me.BtBuscar.Size = New System.Drawing.Size(92, 79)
        Me.BtBuscar.TabIndex = 11
        Me.BtBuscar.UseVisualStyleBackColor = True
        '
        'reto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(999, 564)
        Me.Controls.Add(Me.borrar)
        Me.Controls.Add(Me.VerErrores)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Exportar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DPHastaFecha)
        Me.Controls.Add(Me.DPDesdeFecha)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Excel)
        Me.Controls.Add(Me.BtBuscar)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "reto"
        Me.Text = "Informe RETO"
        Me.TabControl1.ResumeLayout(False)
        Me.TbCompras.ResumeLayout(False)
        CType(Me.DGVCompras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbVentas.ResumeLayout(False)
        CType(Me.DGVVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbAdquiciones.ResumeLayout(False)
        CType(Me.DGVAdquisiciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TbAplicaciones.ResumeLayout(False)
        CType(Me.DGVAplicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TbCompras As TabPage
    Friend WithEvents TbVentas As TabPage
    Friend WithEvents Salir As Button
    Friend WithEvents Excel As Button
    Friend WithEvents BtBuscar As Button
    Friend WithEvents DPDesdeFecha As DateTimePicker
    Friend WithEvents DPHastaFecha As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents DGVCompras As DataGridView
    Friend WithEvents DGVVentas As DataGridView
    Friend WithEvents TbAdquiciones As TabPage
    Friend WithEvents DGVAdquisiciones As DataGridView
    Friend WithEvents TbAplicaciones As TabPage
    Friend WithEvents DGVAplicaciones As DataGridView
    Friend WithEvents Exportar As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents VerErrores As Button
    Friend WithEvents borrar As Button
End Class
