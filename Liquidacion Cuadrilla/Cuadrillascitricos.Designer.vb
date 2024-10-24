<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CuadrillasCitricos
    Inherits libcomunes.FormGenerico

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LblDia = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtCapataz = New System.Windows.Forms.TextBox()
        Me.LblNombreCapataz = New System.Windows.Forms.Label()
        Me.LblTransporte = New System.Windows.Forms.Label()
        Me.TxtTransporte2 = New System.Windows.Forms.TextBox()
        Me.TxtTransporte = New System.Windows.Forms.TextBox()
        Me.DGHoras = New System.Windows.Forms.DataGridView()
        Me.LblNombreOperario = New System.Windows.Forms.Label()
        Me.TxtOperario = New System.Windows.Forms.TextBox()
        Me.LblOperario = New System.Windows.Forms.Label()
        Me.CmdAceptaOperario = New System.Windows.Forms.Button()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.CmdGrabar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblCuantos = New System.Windows.Forms.Label()
        Me.CmdProcesarDia = New System.Windows.Forms.Button()
        Me.DTFechaEntrada = New System.Windows.Forms.DateTimePicker()
        CType(Me.DGHoras, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblDia
        '
        Me.LblDia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDia.Location = New System.Drawing.Point(17, 12)
        Me.LblDia.Name = "LblDia"
        Me.LblDia.Size = New System.Drawing.Size(51, 25)
        Me.LblDia.TabIndex = 1
        Me.LblDia.Text = "Día:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(216, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Capataz:"
        '
        'TxtCapataz
        '
        Me.TxtCapataz.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCapataz.Location = New System.Drawing.Point(279, 9)
        Me.TxtCapataz.Name = "TxtCapataz"
        Me.TxtCapataz.Size = New System.Drawing.Size(56, 20)
        Me.TxtCapataz.TabIndex = 1
        '
        'LblNombreCapataz
        '
        Me.LblNombreCapataz.AutoSize = True
        Me.LblNombreCapataz.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNombreCapataz.Location = New System.Drawing.Point(372, 11)
        Me.LblNombreCapataz.Name = "LblNombreCapataz"
        Me.LblNombreCapataz.Size = New System.Drawing.Size(0, 13)
        Me.LblNombreCapataz.TabIndex = 4
        '
        'LblTransporte
        '
        Me.LblTransporte.AutoSize = True
        Me.LblTransporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTransporte.Location = New System.Drawing.Point(16, 42)
        Me.LblTransporte.Name = "LblTransporte"
        Me.LblTransporte.Size = New System.Drawing.Size(72, 13)
        Me.LblTransporte.TabIndex = 5
        Me.LblTransporte.Text = "Transporte:"
        '
        'TxtTransporte2
        '
        Me.TxtTransporte2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporte2.Location = New System.Drawing.Point(156, 42)
        Me.TxtTransporte2.Name = "TxtTransporte2"
        Me.TxtTransporte2.Size = New System.Drawing.Size(55, 20)
        Me.TxtTransporte2.TabIndex = 3
        '
        'TxtTransporte
        '
        Me.TxtTransporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporte.Location = New System.Drawing.Point(101, 41)
        Me.TxtTransporte.Name = "TxtTransporte"
        Me.TxtTransporte.Size = New System.Drawing.Size(50, 20)
        Me.TxtTransporte.TabIndex = 2
        '
        'DGHoras
        '
        Me.DGHoras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGHoras.Location = New System.Drawing.Point(9, 73)
        Me.DGHoras.Name = "DGHoras"
        Me.DGHoras.RowTemplate.Height = 28
        Me.DGHoras.Size = New System.Drawing.Size(982, 572)
        Me.DGHoras.TabIndex = 9
        '
        'LblNombreOperario
        '
        Me.LblNombreOperario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNombreOperario.Location = New System.Drawing.Point(217, 651)
        Me.LblNombreOperario.Name = "LblNombreOperario"
        Me.LblNombreOperario.Size = New System.Drawing.Size(290, 23)
        Me.LblNombreOperario.TabIndex = 12
        '
        'TxtOperario
        '
        Me.TxtOperario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOperario.Location = New System.Drawing.Point(100, 651)
        Me.TxtOperario.Name = "TxtOperario"
        Me.TxtOperario.Size = New System.Drawing.Size(111, 20)
        Me.TxtOperario.TabIndex = 11
        '
        'LblOperario
        '
        Me.LblOperario.AutoSize = True
        Me.LblOperario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOperario.Location = New System.Drawing.Point(17, 654)
        Me.LblOperario.Name = "LblOperario"
        Me.LblOperario.Size = New System.Drawing.Size(59, 13)
        Me.LblOperario.TabIndex = 10
        Me.LblOperario.Text = "Operario:"
        '
        'CmdAceptaOperario
        '
        Me.CmdAceptaOperario.BackgroundImage = Global.novagestLiqCuad.My.Resources.Resources.check_ok_accept_apply_1582
        Me.CmdAceptaOperario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdAceptaOperario.Location = New System.Drawing.Point(539, 657)
        Me.CmdAceptaOperario.Name = "CmdAceptaOperario"
        Me.CmdAceptaOperario.Size = New System.Drawing.Size(38, 35)
        Me.CmdAceptaOperario.TabIndex = 13
        Me.CmdAceptaOperario.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = Global.novagestLiqCuad.My.Resources.Resources.salir
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(933, 14)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(53, 50)
        Me.CmdSalir.TabIndex = 14
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'CmdGrabar
        '
        Me.CmdGrabar.BackgroundImage = Global.novagestLiqCuad.My.Resources.Resources.grabar
        Me.CmdGrabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdGrabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdGrabar.Location = New System.Drawing.Point(918, 657)
        Me.CmdGrabar.Name = "CmdGrabar"
        Me.CmdGrabar.Size = New System.Drawing.Size(49, 44)
        Me.CmdGrabar.TabIndex = 15
        Me.CmdGrabar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(595, 662)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Núm. operarios:"
        '
        'LblCuantos
        '
        Me.LblCuantos.AutoSize = True
        Me.LblCuantos.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCuantos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCuantos.Location = New System.Drawing.Point(753, 662)
        Me.LblCuantos.Name = "LblCuantos"
        Me.LblCuantos.Size = New System.Drawing.Size(0, 13)
        Me.LblCuantos.TabIndex = 17
        Me.LblCuantos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmdProcesarDia
        '
        Me.CmdProcesarDia.BackgroundImage = Global.novagestLiqCuad.My.Resources.Resources.desglose
        Me.CmdProcesarDia.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdProcesarDia.Location = New System.Drawing.Point(779, 12)
        Me.CmdProcesarDia.Name = "CmdProcesarDia"
        Me.CmdProcesarDia.Size = New System.Drawing.Size(53, 53)
        Me.CmdProcesarDia.TabIndex = 4
        Me.CmdProcesarDia.UseVisualStyleBackColor = True
        '
        'DTFechaEntrada
        '
        Me.DTFechaEntrada.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFechaEntrada.Location = New System.Drawing.Point(50, 9)
        Me.DTFechaEntrada.Name = "DTFechaEntrada"
        Me.DTFechaEntrada.Size = New System.Drawing.Size(139, 20)
        Me.DTFechaEntrada.TabIndex = 19
        '
        'CuadrillasCitricos
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(998, 709)
        Me.Controls.Add(Me.DTFechaEntrada)
        Me.Controls.Add(Me.LblCuantos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmdGrabar)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.CmdAceptaOperario)
        Me.Controls.Add(Me.LblNombreOperario)
        Me.Controls.Add(Me.TxtOperario)
        Me.Controls.Add(Me.LblOperario)
        Me.Controls.Add(Me.DGHoras)
        Me.Controls.Add(Me.CmdProcesarDia)
        Me.Controls.Add(Me.TxtTransporte)
        Me.Controls.Add(Me.TxtTransporte2)
        Me.Controls.Add(Me.LblTransporte)
        Me.Controls.Add(Me.LblNombreCapataz)
        Me.Controls.Add(Me.TxtCapataz)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblDia)
        Me.Name = "CuadrillasCitricos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuadrillas cítricos"
        CType(Me.DGHoras, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblDia As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtCapataz As TextBox
    Friend WithEvents LblNombreCapataz As Label
    Friend WithEvents LblTransporte As Label
    Friend WithEvents TxtTransporte2 As TextBox
    Friend WithEvents TxtTransporte As TextBox
    Friend WithEvents CmdProcesarDia As Button
    Friend WithEvents DGHoras As DataGridView
    Friend WithEvents LblNombreOperario As Label
    Friend WithEvents TxtOperario As TextBox
    Friend WithEvents LblOperario As Label
    Friend WithEvents CmdAceptaOperario As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents LblCuantos As Label
    Public WithEvents CmdGrabar As Button
    Friend WithEvents DTFechaEntrada As DateTimePicker
End Class
