<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSeleccionLineaAlbaran
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
        Me.MSFLAlbaran = New System.Windows.Forms.DataGridView()
        Me.MSFLFactura = New System.Windows.Forms.DataGridView()
        Me.TxtFiltro = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Base4 = New System.Windows.Forms.Label()
        Me.base3 = New System.Windows.Forms.Label()
        Me.Base2 = New System.Windows.Forms.Label()
        Me.Base1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Tipo4 = New System.Windows.Forms.Label()
        Me.tipo3 = New System.Windows.Forms.Label()
        Me.tipo2 = New System.Windows.Forms.Label()
        Me.tipo1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Cuota4 = New System.Windows.Forms.Label()
        Me.cuota3 = New System.Windows.Forms.Label()
        Me.cuota2 = New System.Windows.Forms.Label()
        Me.cuota1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Total = New System.Windows.Forms.TextBox()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.aceptar = New System.Windows.Forms.Button()
        Me.CmdBuscar = New System.Windows.Forms.Button()
        Me.CargarAlbaranes = New System.Windows.Forms.Button()
        Me.cbProveedor = New System.Windows.Forms.ComboBox()
        CType(Me.MSFLAlbaran, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MSFLFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(198, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código del proveedor.:"
        '
        'MSFLAlbaran
        '
        Me.MSFLAlbaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MSFLAlbaran.Location = New System.Drawing.Point(16, 62)
        Me.MSFLAlbaran.Margin = New System.Windows.Forms.Padding(4)
        Me.MSFLAlbaran.MultiSelect = False
        Me.MSFLAlbaran.Name = "MSFLAlbaran"
        Me.MSFLAlbaran.ReadOnly = True
        Me.MSFLAlbaran.Size = New System.Drawing.Size(1176, 233)
        Me.MSFLAlbaran.TabIndex = 3
        '
        'MSFLFactura
        '
        Me.MSFLFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MSFLFactura.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.MSFLFactura.Location = New System.Drawing.Point(16, 302)
        Me.MSFLFactura.Margin = New System.Windows.Forms.Padding(4)
        Me.MSFLFactura.MultiSelect = False
        Me.MSFLFactura.Name = "MSFLFactura"
        Me.MSFLFactura.ReadOnly = True
        Me.MSFLFactura.Size = New System.Drawing.Size(1176, 233)
        Me.MSFLFactura.TabIndex = 4
        '
        'TxtFiltro
        '
        Me.TxtFiltro.Location = New System.Drawing.Point(16, 542)
        Me.TxtFiltro.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(149, 22)
        Me.TxtFiltro.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Base4)
        Me.GroupBox1.Controls.Add(Me.base3)
        Me.GroupBox1.Controls.Add(Me.Base2)
        Me.GroupBox1.Controls.Add(Me.Base1)
        Me.GroupBox1.Location = New System.Drawing.Point(233, 550)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(243, 145)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bases imponibles"
        '
        'Base4
        '
        Me.Base4.AutoSize = True
        Me.Base4.Location = New System.Drawing.Point(179, 110)
        Me.Base4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Base4.Name = "Base4"
        Me.Base4.Size = New System.Drawing.Size(36, 17)
        Me.Base4.TabIndex = 3
        Me.Base4.Text = "0.00"
        '
        'base3
        '
        Me.base3.AutoSize = True
        Me.base3.Location = New System.Drawing.Point(179, 82)
        Me.base3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.base3.Name = "base3"
        Me.base3.Size = New System.Drawing.Size(36, 17)
        Me.base3.TabIndex = 2
        Me.base3.Text = "0.00"
        '
        'Base2
        '
        Me.Base2.AutoSize = True
        Me.Base2.Location = New System.Drawing.Point(179, 54)
        Me.Base2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Base2.Name = "Base2"
        Me.Base2.Size = New System.Drawing.Size(36, 17)
        Me.Base2.TabIndex = 1
        Me.Base2.Text = "0.00"
        '
        'Base1
        '
        Me.Base1.AutoSize = True
        Me.Base1.Location = New System.Drawing.Point(179, 26)
        Me.Base1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Base1.Name = "Base1"
        Me.Base1.Size = New System.Drawing.Size(36, 17)
        Me.Base1.TabIndex = 0
        Me.Base1.Text = "0.00"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Tipo4)
        Me.GroupBox2.Controls.Add(Me.tipo3)
        Me.GroupBox2.Controls.Add(Me.tipo2)
        Me.GroupBox2.Controls.Add(Me.tipo1)
        Me.GroupBox2.Location = New System.Drawing.Point(484, 550)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(116, 145)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tipos IVA %"
        '
        'Tipo4
        '
        Me.Tipo4.AutoSize = True
        Me.Tipo4.Location = New System.Drawing.Point(31, 110)
        Me.Tipo4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Tipo4.Name = "Tipo4"
        Me.Tipo4.Size = New System.Drawing.Size(36, 17)
        Me.Tipo4.TabIndex = 3
        Me.Tipo4.Text = "0.00"
        '
        'tipo3
        '
        Me.tipo3.AutoSize = True
        Me.tipo3.Location = New System.Drawing.Point(31, 82)
        Me.tipo3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tipo3.Name = "tipo3"
        Me.tipo3.Size = New System.Drawing.Size(36, 17)
        Me.tipo3.TabIndex = 2
        Me.tipo3.Text = "0.00"
        '
        'tipo2
        '
        Me.tipo2.AutoSize = True
        Me.tipo2.Location = New System.Drawing.Point(31, 54)
        Me.tipo2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tipo2.Name = "tipo2"
        Me.tipo2.Size = New System.Drawing.Size(36, 17)
        Me.tipo2.TabIndex = 1
        Me.tipo2.Text = "0.00"
        '
        'tipo1
        '
        Me.tipo1.AutoSize = True
        Me.tipo1.Location = New System.Drawing.Point(31, 26)
        Me.tipo1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.tipo1.Name = "tipo1"
        Me.tipo1.Size = New System.Drawing.Size(36, 17)
        Me.tipo1.TabIndex = 0
        Me.tipo1.Text = "0.00"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Cuota4)
        Me.GroupBox3.Controls.Add(Me.cuota3)
        Me.GroupBox3.Controls.Add(Me.cuota2)
        Me.GroupBox3.Controls.Add(Me.cuota1)
        Me.GroupBox3.Location = New System.Drawing.Point(608, 550)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(243, 145)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cuotas IVA"
        '
        'Cuota4
        '
        Me.Cuota4.AutoSize = True
        Me.Cuota4.Location = New System.Drawing.Point(177, 110)
        Me.Cuota4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Cuota4.Name = "Cuota4"
        Me.Cuota4.Size = New System.Drawing.Size(36, 17)
        Me.Cuota4.TabIndex = 3
        Me.Cuota4.Text = "0.00"
        '
        'cuota3
        '
        Me.cuota3.AutoSize = True
        Me.cuota3.Location = New System.Drawing.Point(177, 80)
        Me.cuota3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cuota3.Name = "cuota3"
        Me.cuota3.Size = New System.Drawing.Size(36, 17)
        Me.cuota3.TabIndex = 2
        Me.cuota3.Text = "0.00"
        '
        'cuota2
        '
        Me.cuota2.AutoSize = True
        Me.cuota2.Location = New System.Drawing.Point(177, 50)
        Me.cuota2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cuota2.Name = "cuota2"
        Me.cuota2.Size = New System.Drawing.Size(36, 17)
        Me.cuota2.TabIndex = 1
        Me.cuota2.Text = "0.00"
        '
        'cuota1
        '
        Me.cuota1.AutoSize = True
        Me.cuota1.Location = New System.Drawing.Point(177, 20)
        Me.cuota1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cuota1.Name = "cuota1"
        Me.cuota1.Size = New System.Drawing.Size(36, 17)
        Me.cuota1.TabIndex = 0
        Me.cuota1.Text = "0.00"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(999, 565)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(179, 31)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Total factura"
        '
        'Total
        '
        Me.Total.Location = New System.Drawing.Point(1005, 624)
        Me.Total.Margin = New System.Windows.Forms.Padding(4)
        Me.Total.Name = "Total"
        Me.Total.Size = New System.Drawing.Size(185, 22)
        Me.Total.TabIndex = 13
        '
        'cancelar
        '
        Me.cancelar.BackgroundImage = Global.novacompras.My.Resources.Resources.cerrar
        Me.cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cancelar.Location = New System.Drawing.Point(85, 598)
        Me.cancelar.Margin = New System.Windows.Forms.Padding(4)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(52, 50)
        Me.cancelar.TabIndex = 8
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'aceptar
        '
        Me.aceptar.BackgroundImage = Global.novacompras.My.Resources.Resources.cheque__1_
        Me.aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.aceptar.Location = New System.Drawing.Point(27, 598)
        Me.aceptar.Margin = New System.Windows.Forms.Padding(4)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(51, 50)
        Me.aceptar.TabIndex = 7
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'CmdBuscar
        '
        Me.CmdBuscar.BackgroundImage = Global.novacompras.My.Resources.Resources._0404_09_search
        Me.CmdBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBuscar.Location = New System.Drawing.Point(175, 539)
        Me.CmdBuscar.Margin = New System.Windows.Forms.Padding(4)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.Size = New System.Drawing.Size(32, 28)
        Me.CmdBuscar.TabIndex = 6
        Me.CmdBuscar.UseVisualStyleBackColor = True
        '
        'CargarAlbaranes
        '
        Me.CargarAlbaranes.BackgroundImage = Global.novacompras.My.Resources.Resources.exportar1
        Me.CargarAlbaranes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CargarAlbaranes.Location = New System.Drawing.Point(764, 15)
        Me.CargarAlbaranes.Margin = New System.Windows.Forms.Padding(4)
        Me.CargarAlbaranes.Name = "CargarAlbaranes"
        Me.CargarAlbaranes.Size = New System.Drawing.Size(41, 37)
        Me.CargarAlbaranes.TabIndex = 2
        Me.CargarAlbaranes.UseVisualStyleBackColor = True
        '
        'cbProveedor
        '
        Me.cbProveedor.FormattingEnabled = True
        Me.cbProveedor.Location = New System.Drawing.Point(248, 23)
        Me.cbProveedor.Margin = New System.Windows.Forms.Padding(4)
        Me.cbProveedor.Name = "cbProveedor"
        Me.cbProveedor.Size = New System.Drawing.Size(507, 24)
        Me.cbProveedor.TabIndex = 14
        '
        'FrmSeleccionLineaAlbaran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 697)
        Me.Controls.Add(Me.cbProveedor)
        Me.Controls.Add(Me.Total)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cancelar)
        Me.Controls.Add(Me.aceptar)
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.TxtFiltro)
        Me.Controls.Add(Me.MSFLFactura)
        Me.Controls.Add(Me.MSFLAlbaran)
        Me.Controls.Add(Me.CargarAlbaranes)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmSeleccionLineaAlbaran"
        Me.Text = "Selección líneas albaran"
        CType(Me.MSFLAlbaran, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MSFLFactura, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents CargarAlbaranes As Button
    Friend WithEvents MSFLAlbaran As DataGridView
    Friend WithEvents MSFLFactura As DataGridView
    Friend WithEvents TxtFiltro As TextBox
    Friend WithEvents CmdBuscar As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents cancelar As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents base3 As Label
    Friend WithEvents Base2 As Label
    Friend WithEvents Base1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents tipo3 As Label
    Friend WithEvents tipo2 As Label
    Friend WithEvents tipo1 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents cuota3 As Label
    Friend WithEvents cuota2 As Label
    Friend WithEvents cuota1 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Total As TextBox
    Friend WithEvents cbProveedor As ComboBox
    Friend WithEvents Base4 As Label
    Friend WithEvents Tipo4 As Label
    Friend WithEvents Cuota4 As Label
End Class
