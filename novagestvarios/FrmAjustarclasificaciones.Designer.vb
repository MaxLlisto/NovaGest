<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAjustarClasificaciones
    Inherits libcomunes.FormGenerico 'System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAjustarClasificaciones))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCodigo_variedad = New Textboxbotoncontrol.TextBoxButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGClasificacion = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSegunda = New System.Windows.Forms.TextBox()
        Me.TxtNC = New System.Windows.Forms.TextBox()
        Me.Desdefecha = New System.Windows.Forms.DateTimePicker()
        Me.Hastalafecha = New System.Windows.Forms.DateTimePicker()
        Me.BtDeseleccionar = New System.Windows.Forms.Button()
        Me.BtSeleccionartodo = New System.Windows.Forms.Button()
        Me.BtGrabar = New System.Windows.Forms.Button()
        Me.BtReajustar = New System.Windows.Forms.Button()
        Me.Cargardatos = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.btExcel = New System.Windows.Forms.Button()
        Me.cbBloquear = New System.Windows.Forms.CheckBox()
        CType(Me.DGClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código de variedad"
        '
        'txtCodigo_variedad
        '
        Me.txtCodigo_variedad.ButtonImage = Nothing
        Me.txtCodigo_variedad.Location = New System.Drawing.Point(121, 30)
        Me.txtCodigo_variedad.Name = "txtCodigo_variedad"
        Me.txtCodigo_variedad.Size = New System.Drawing.Size(149, 20)
        Me.txtCodigo_variedad.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(347, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Entre las fecha:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(581, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "y"
        '
        'DGClasificacion
        '
        Me.DGClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGClasificacion.Location = New System.Drawing.Point(6, 77)
        Me.DGClasificacion.Name = "DGClasificacion"
        Me.DGClasificacion.Size = New System.Drawing.Size(1288, 652)
        Me.DGClasificacion.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 752)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Ajustar valores:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(117, 745)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 20)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Segunda:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(342, 745)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 20)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "NC:"
        '
        'TxtSegunda
        '
        Me.TxtSegunda.Location = New System.Drawing.Point(209, 745)
        Me.TxtSegunda.Name = "TxtSegunda"
        Me.TxtSegunda.Size = New System.Drawing.Size(100, 20)
        Me.TxtSegunda.TabIndex = 13
        Me.TxtSegunda.Text = "0"
        '
        'TxtNC
        '
        Me.TxtNC.Location = New System.Drawing.Point(386, 745)
        Me.TxtNC.Name = "TxtNC"
        Me.TxtNC.Size = New System.Drawing.Size(100, 20)
        Me.TxtNC.TabIndex = 14
        Me.TxtNC.Text = "0"
        '
        'Desdefecha
        '
        Me.Desdefecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Desdefecha.Location = New System.Drawing.Point(434, 30)
        Me.Desdefecha.Name = "Desdefecha"
        Me.Desdefecha.Size = New System.Drawing.Size(124, 20)
        Me.Desdefecha.TabIndex = 15
        '
        'Hastalafecha
        '
        Me.Hastalafecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Hastalafecha.Location = New System.Drawing.Point(614, 30)
        Me.Hastalafecha.Name = "Hastalafecha"
        Me.Hastalafecha.Size = New System.Drawing.Size(124, 20)
        Me.Hastalafecha.TabIndex = 16
        '
        'BtDeseleccionar
        '
        Me.BtDeseleccionar.Image = Global.Novagestvarios.My.Resources.Resources.descheque
        Me.BtDeseleccionar.Location = New System.Drawing.Point(866, 744)
        Me.BtDeseleccionar.Name = "BtDeseleccionar"
        Me.BtDeseleccionar.Size = New System.Drawing.Size(31, 29)
        Me.BtDeseleccionar.TabIndex = 20
        Me.BtDeseleccionar.UseVisualStyleBackColor = True
        '
        'BtSeleccionartodo
        '
        Me.BtSeleccionartodo.Image = Global.Novagestvarios.My.Resources.Resources.cheque
        Me.BtSeleccionartodo.Location = New System.Drawing.Point(815, 744)
        Me.BtSeleccionartodo.Name = "BtSeleccionartodo"
        Me.BtSeleccionartodo.Size = New System.Drawing.Size(45, 29)
        Me.BtSeleccionartodo.TabIndex = 19
        Me.BtSeleccionartodo.UseVisualStyleBackColor = True
        '
        'BtGrabar
        '
        Me.BtGrabar.Image = Global.Novagestvarios.My.Resources.Resources.subir_datos
        Me.BtGrabar.Location = New System.Drawing.Point(1225, 735)
        Me.BtGrabar.Name = "BtGrabar"
        Me.BtGrabar.Size = New System.Drawing.Size(75, 42)
        Me.BtGrabar.TabIndex = 18
        Me.BtGrabar.UseVisualStyleBackColor = True
        '
        'BtReajustar
        '
        Me.BtReajustar.Image = Global.Novagestvarios.My.Resources.Resources.cocinando
        Me.BtReajustar.Location = New System.Drawing.Point(509, 736)
        Me.BtReajustar.Name = "BtReajustar"
        Me.BtReajustar.Size = New System.Drawing.Size(62, 37)
        Me.BtReajustar.TabIndex = 17
        Me.BtReajustar.UseVisualStyleBackColor = True
        '
        'Cargardatos
        '
        Me.Cargardatos.Image = Global.Novagestvarios.My.Resources.Resources.maquina_elevadora
        Me.Cargardatos.Location = New System.Drawing.Point(786, 7)
        Me.Cargardatos.Name = "Cargardatos"
        Me.Cargardatos.Size = New System.Drawing.Size(74, 59)
        Me.Cargardatos.TabIndex = 9
        Me.Cargardatos.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.Image = Global.Novagestvarios.My.Resources.Resources.salir
        Me.BtSalir.Location = New System.Drawing.Point(1225, 4)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(75, 59)
        Me.BtSalir.TabIndex = 8
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'btExcel
        '
        Me.btExcel.Image = Global.Novagestvarios.My.Resources.Resources.excel1
        Me.btExcel.Location = New System.Drawing.Point(987, 7)
        Me.btExcel.Name = "btExcel"
        Me.btExcel.Size = New System.Drawing.Size(66, 56)
        Me.btExcel.TabIndex = 21
        Me.btExcel.UseVisualStyleBackColor = True
        '
        'cbBloquear
        '
        Me.cbBloquear.AutoSize = True
        Me.cbBloquear.Checked = True
        Me.cbBloquear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbBloquear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBloquear.ForeColor = System.Drawing.Color.Blue
        Me.cbBloquear.Location = New System.Drawing.Point(596, 747)
        Me.cbBloquear.Name = "cbBloquear"
        Me.cbBloquear.Size = New System.Drawing.Size(151, 17)
        Me.cbBloquear.TabIndex = 22
        Me.cbBloquear.Text = "Bloquear clasificación"
        Me.cbBloquear.UseVisualStyleBackColor = True
        '
        'FrmAjustarClasificaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1302, 794)
        Me.Controls.Add(Me.cbBloquear)
        Me.Controls.Add(Me.btExcel)
        Me.Controls.Add(Me.BtDeseleccionar)
        Me.Controls.Add(Me.BtSeleccionartodo)
        Me.Controls.Add(Me.BtGrabar)
        Me.Controls.Add(Me.BtReajustar)
        Me.Controls.Add(Me.Hastalafecha)
        Me.Controls.Add(Me.Desdefecha)
        Me.Controls.Add(Me.TxtNC)
        Me.Controls.Add(Me.TxtSegunda)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Cargardatos)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.DGClasificacion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCodigo_variedad)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmAjustarClasificaciones"
        Me.Text = resources.GetString("$this.Text")
        CType(Me.DGClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtCodigo_variedad As Textboxbotoncontrol.TextBoxButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents DGClasificacion As DataGridView
    Friend WithEvents BtSalir As Button
    Friend WithEvents Cargardatos As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSegunda As TextBox
    Friend WithEvents TxtNC As TextBox
    Friend WithEvents Desdefecha As DateTimePicker
    Friend WithEvents Hastalafecha As DateTimePicker
    Friend WithEvents BtReajustar As Button
    Friend WithEvents BtGrabar As Button
    Friend WithEvents BtSeleccionartodo As Button
    Friend WithEvents BtDeseleccionar As Button
    Friend WithEvents btExcel As Button
    Friend WithEvents cbBloquear As CheckBox
End Class
