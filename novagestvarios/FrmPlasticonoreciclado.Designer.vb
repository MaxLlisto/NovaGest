<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPlasticonoreciclado
    Inherits libcomunes.FormGenerico

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
        Dim AbrirDesglose As System.Windows.Forms.Button
        Me.Hastalafecha = New System.Windows.Forms.DateTimePicker()
        Me.Desdefecha = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCodigo_variedad = New Textboxbotoncontrol.TextBoxButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGClasificacion = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtCliente = New Textboxbotoncontrol.TextBoxButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTasa = New Textboxbotoncontrol.TextBoxButton()
        Me.TxtArchivoDetalle = New System.Windows.Forms.TextBox()
        Me.FileDialogo = New System.Windows.Forms.SaveFileDialog()
        Me.BtFicheroDetalle = New System.Windows.Forms.Button()
        Me.PasarExcel = New System.Windows.Forms.Button()
        Me.BtCalcularinforme = New System.Windows.Forms.Button()
        Me.cerrar = New System.Windows.Forms.Button()
        Me.btExcel = New System.Windows.Forms.Button()
        Me.Cargardatos = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxCalendar1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxCalendar2 = New System.Windows.Forms.DateTimePicker()
        AbrirDesglose = New System.Windows.Forms.Button()
        CType(Me.DGClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AbrirDesglose
        '
        AbrirDesglose.BackgroundImage = Global.Novagestvarios.My.Resources.Resources.desglose
        AbrirDesglose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        AbrirDesglose.Location = New System.Drawing.Point(1441, 11)
        AbrirDesglose.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        AbrirDesglose.Name = "AbrirDesglose"
        AbrirDesglose.Size = New System.Drawing.Size(88, 69)
        AbrirDesglose.TabIndex = 53
        AbrirDesglose.UseVisualStyleBackColor = True
        AddHandler AbrirDesglose.Click, AddressOf Me.AbrirDesglose_Click
        '
        'Hastalafecha
        '
        Me.Hastalafecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Hastalafecha.Location = New System.Drawing.Point(713, -112)
        Me.Hastalafecha.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Hastalafecha.Name = "Hastalafecha"
        Me.Hastalafecha.Size = New System.Drawing.Size(13, 22)
        Me.Hastalafecha.TabIndex = 30
        '
        'Desdefecha
        '
        Me.Desdefecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Desdefecha.Location = New System.Drawing.Point(473, -112)
        Me.Desdefecha.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Desdefecha.Name = "Desdefecha"
        Me.Desdefecha.Size = New System.Drawing.Size(13, 22)
        Me.Desdefecha.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(669, -112)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 17)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "y"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(357, -108)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 17)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Entre las fecha:"
        '
        'txtCodigo_variedad
        '
        Me.txtCodigo_variedad.ButtonImage = Nothing
        Me.txtCodigo_variedad.Location = New System.Drawing.Point(56, -112)
        Me.txtCodigo_variedad.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCodigo_variedad.Name = "txtCodigo_variedad"
        Me.txtCodigo_variedad.Size = New System.Drawing.Size(47, 22)
        Me.txtCodigo_variedad.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-84, -108)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 17)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Código de variedad"
        '
        'DGClasificacion
        '
        Me.DGClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGClasificacion.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DGClasificacion.Location = New System.Drawing.Point(16, 96)
        Me.DGClasificacion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DGClasificacion.Name = "DGClasificacion"
        Me.DGClasificacion.Size = New System.Drawing.Size(1717, 795)
        Me.DGClasificacion.TabIndex = 36
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(886, 27)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 17)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "y"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(596, 30)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 17)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Entre las fecha:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(31, 31)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 17)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Código cliente"
        '
        'TxtCliente
        '
        Me.TxtCliente.ButtonImage = Nothing
        Me.TxtCliente.Location = New System.Drawing.Point(137, 27)
        Me.TxtCliente.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtCliente.Name = "TxtCliente"
        Me.TxtCliente.Size = New System.Drawing.Size(196, 22)
        Me.TxtCliente.TabIndex = 44
        Me.TxtCliente.Text = "200"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(393, 31)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 17)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Tasa"
        '
        'txtTasa
        '
        Me.txtTasa.ButtonImage = Nothing
        Me.txtTasa.Location = New System.Drawing.Point(443, 27)
        Me.txtTasa.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTasa.Name = "txtTasa"
        Me.txtTasa.Size = New System.Drawing.Size(132, 22)
        Me.txtTasa.TabIndex = 46
        Me.txtTasa.Text = "0,45"
        '
        'TxtArchivoDetalle
        '
        Me.TxtArchivoDetalle.Location = New System.Drawing.Point(137, 60)
        Me.TxtArchivoDetalle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TxtArchivoDetalle.Name = "TxtArchivoDetalle"
        Me.TxtArchivoDetalle.Size = New System.Drawing.Size(945, 22)
        Me.TxtArchivoDetalle.TabIndex = 49
        Me.TxtArchivoDetalle.Text = "c:\temp\detallelog.csv"
        '
        'FileDialogo
        '
        Me.FileDialogo.DefaultExt = "csv"
        '
        'BtFicheroDetalle
        '
        Me.BtFicheroDetalle.BackgroundImage = Global.Novagestvarios.My.Resources.Resources.ext_csv_ico
        Me.BtFicheroDetalle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtFicheroDetalle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtFicheroDetalle.Location = New System.Drawing.Point(1092, 54)
        Me.BtFicheroDetalle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtFicheroDetalle.Name = "BtFicheroDetalle"
        Me.BtFicheroDetalle.Size = New System.Drawing.Size(32, 33)
        Me.BtFicheroDetalle.TabIndex = 50
        Me.BtFicheroDetalle.UseVisualStyleBackColor = True
        '
        'PasarExcel
        '
        Me.PasarExcel.Image = Global.Novagestvarios.My.Resources.Resources.excel1
        Me.PasarExcel.Location = New System.Drawing.Point(1319, 11)
        Me.PasarExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PasarExcel.Name = "PasarExcel"
        Me.PasarExcel.Size = New System.Drawing.Size(88, 69)
        Me.PasarExcel.TabIndex = 52
        Me.PasarExcel.UseVisualStyleBackColor = True
        '
        'BtCalcularinforme
        '
        Me.BtCalcularinforme.BackgroundImage = Global.Novagestvarios.My.Resources.Resources.contenedor_plasticos
        Me.BtCalcularinforme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtCalcularinforme.Location = New System.Drawing.Point(1152, 11)
        Me.BtCalcularinforme.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtCalcularinforme.Name = "BtCalcularinforme"
        Me.BtCalcularinforme.Size = New System.Drawing.Size(109, 73)
        Me.BtCalcularinforme.TabIndex = 51
        Me.BtCalcularinforme.UseVisualStyleBackColor = True
        '
        'cerrar
        '
        Me.cerrar.Image = Global.Novagestvarios.My.Resources.Resources.salir
        Me.cerrar.Location = New System.Drawing.Point(1633, -1)
        Me.cerrar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cerrar.Name = "cerrar"
        Me.cerrar.Size = New System.Drawing.Size(100, 89)
        Me.cerrar.TabIndex = 54
        Me.cerrar.UseVisualStyleBackColor = True
        '
        'btExcel
        '
        Me.btExcel.Image = Global.Novagestvarios.My.Resources.Resources.excel1
        Me.btExcel.Location = New System.Drawing.Point(1211, -140)
        Me.btExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btExcel.Name = "btExcel"
        Me.btExcel.Size = New System.Drawing.Size(13, 12)
        Me.btExcel.TabIndex = 31
        Me.btExcel.UseVisualStyleBackColor = True
        '
        'Cargardatos
        '
        Me.Cargardatos.Image = Global.Novagestvarios.My.Resources.Resources.maquina_elevadora
        Me.Cargardatos.Location = New System.Drawing.Point(943, -140)
        Me.Cargardatos.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Cargardatos.Name = "Cargardatos"
        Me.Cargardatos.Size = New System.Drawing.Size(13, 12)
        Me.Cargardatos.TabIndex = 28
        Me.Cargardatos.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.Image = Global.Novagestvarios.My.Resources.Resources.salir
        Me.BtSalir.Location = New System.Drawing.Point(1528, -144)
        Me.BtSalir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(13, 12)
        Me.BtSalir.TabIndex = 27
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(31, 65)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 17)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Desglose"
        '
        'TextBoxCalendar1
        '
        Me.TextBoxCalendar1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.TextBoxCalendar1.Location = New System.Drawing.Point(710, 27)
        Me.TextBoxCalendar1.Name = "TextBoxCalendar1"
        Me.TextBoxCalendar1.Size = New System.Drawing.Size(137, 22)
        Me.TextBoxCalendar1.TabIndex = 47
        '
        'TextBoxCalendar2
        '
        Me.TextBoxCalendar2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.TextBoxCalendar2.Location = New System.Drawing.Point(945, 25)
        Me.TextBoxCalendar2.Name = "TextBoxCalendar2"
        Me.TextBoxCalendar2.Size = New System.Drawing.Size(137, 22)
        Me.TextBoxCalendar2.TabIndex = 48
        '
        'FrmPlasticonoreciclado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1749, 905)
        Me.Controls.Add(Me.TextBoxCalendar2)
        Me.Controls.Add(Me.TextBoxCalendar1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(AbrirDesglose)
        Me.Controls.Add(Me.BtFicheroDetalle)
        Me.Controls.Add(Me.TxtArchivoDetalle)
        Me.Controls.Add(Me.txtTasa)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtCliente)
        Me.Controls.Add(Me.PasarExcel)
        Me.Controls.Add(Me.BtCalcularinforme)
        Me.Controls.Add(Me.cerrar)
        Me.Controls.Add(Me.DGClasificacion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btExcel)
        Me.Controls.Add(Me.Hastalafecha)
        Me.Controls.Add(Me.Desdefecha)
        Me.Controls.Add(Me.Cargardatos)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCodigo_variedad)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmPlasticonoreciclado"
        Me.Text = "Informe plástico no reciclado"
        CType(Me.DGClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btExcel As Button
    Friend WithEvents Hastalafecha As DateTimePicker
    Friend WithEvents Desdefecha As DateTimePicker
    Friend WithEvents Cargardatos As Button
    Friend WithEvents BtSalir As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtCodigo_variedad As Textboxbotoncontrol.TextBoxButton
    Friend WithEvents Label1 As Label
    Friend WithEvents PasarExcel As Button
    Friend WithEvents BtCalcularinforme As Button
    Friend WithEvents cerrar As Button
    Friend WithEvents DGClasificacion As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtCliente As Textboxbotoncontrol.TextBoxButton
    Friend WithEvents Label7 As Label
    Friend WithEvents txtTasa As Textboxbotoncontrol.TextBoxButton
    Friend WithEvents TxtArchivoDetalle As TextBox
    Friend WithEvents BtFicheroDetalle As Button
    Friend WithEvents FileDialogo As SaveFileDialog
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxCalendar1 As DateTimePicker
    Friend WithEvents TextBoxCalendar2 As DateTimePicker
End Class
