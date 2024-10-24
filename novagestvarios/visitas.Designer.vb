<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Visitas
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
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.empresa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nif = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.salirsalir = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Entrada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cbcodigo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GrabarModif = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Firmar = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtNumeroEntrada = New System.Windows.Forms.TextBox()
        Me.TxtEntrada = New System.Windows.Forms.Label()
        Me.TxtHoraSalida = New System.Windows.Forms.DateTimePicker()
        Me.TxtHora_entrada = New System.Windows.Forms.DateTimePicker()
        Me.txtFecha = New System.Windows.Forms.DateTimePicker()
        Me.TxtTarjeta = New System.Windows.Forms.TextBox()
        Me.TxtMotivo = New System.Windows.Forms.TextBox()
        Me.TxtMovil = New System.Windows.Forms.TextBox()
        Me.TxtNIF = New System.Windows.Forms.TextBox()
        Me.TxtNombre = New System.Windows.Forms.TextBox()
        Me.TxtEmpresa = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDisplay = New System.Windows.Forms.TextBox()
        Me.TxtTextoFirma = New System.Windows.Forms.TextBox()
        Me.ModoAuditoria = New System.Windows.Forms.CheckBox()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.EntraPersona = New System.Windows.Forms.Button()
        Me.Buscar = New System.Windows.Forms.Button()
        Me.Nuevo = New System.Windows.Forms.Button()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nombre, Me.empresa, Me.nif, Me.salirsalir, Me.Entrada})
        Me.dgv.Location = New System.Drawing.Point(12, 147)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(950, 292)
        Me.dgv.TabIndex = 0
        '
        'nombre
        '
        Me.nombre.HeaderText = "Nombre y Apellidos"
        Me.nombre.Name = "nombre"
        Me.nombre.Width = 300
        '
        'empresa
        '
        Me.empresa.HeaderText = "Empresa"
        Me.empresa.Name = "empresa"
        Me.empresa.Width = 300
        '
        'nif
        '
        Me.nif.HeaderText = "NIF"
        Me.nif.Name = "nif"
        Me.nif.Width = 125
        '
        'salirsalir
        '
        Me.salirsalir.HeaderText = ""
        Me.salirsalir.Image = Global.Novagestvarios.My.Resources.Resources.salirpersona
        Me.salirsalir.Name = "salirsalir"
        '
        'Entrada
        '
        Me.Entrada.HeaderText = "Entrada"
        Me.Entrada.Name = "Entrada"
        '
        'cbcodigo
        '
        Me.cbcodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcodigo.FormattingEnabled = True
        Me.cbcodigo.Location = New System.Drawing.Point(13, 101)
        Me.cbcodigo.Name = "cbcodigo"
        Me.cbcodigo.Size = New System.Drawing.Size(892, 32)
        Me.cbcodigo.TabIndex = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GrabarModif)
        Me.GroupBox1.Controls.Add(Me.Salir)
        Me.GroupBox1.Controls.Add(Me.Firmar)
        Me.GroupBox1.Location = New System.Drawing.Point(875, 445)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(87, 255)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'GrabarModif
        '
        Me.GrabarModif.Enabled = False
        Me.GrabarModif.Image = Global.Novagestvarios.My.Resources.Resources.grabar
        Me.GrabarModif.Location = New System.Drawing.Point(5, 104)
        Me.GrabarModif.Name = "GrabarModif"
        Me.GrabarModif.Size = New System.Drawing.Size(75, 73)
        Me.GrabarModif.TabIndex = 6
        Me.GrabarModif.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.Image = Global.Novagestvarios.My.Resources.Resources.salir
        Me.Salir.Location = New System.Drawing.Point(6, 183)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(74, 65)
        Me.Salir.TabIndex = 5
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Firmar
        '
        Me.Firmar.Image = Global.Novagestvarios.My.Resources.Resources.firmar
        Me.Firmar.Location = New System.Drawing.Point(6, 19)
        Me.Firmar.Name = "Firmar"
        Me.Firmar.Size = New System.Drawing.Size(77, 79)
        Me.Firmar.TabIndex = 3
        Me.Firmar.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtNumeroEntrada)
        Me.GroupBox2.Controls.Add(Me.TxtEntrada)
        Me.GroupBox2.Controls.Add(Me.TxtHoraSalida)
        Me.GroupBox2.Controls.Add(Me.TxtHora_entrada)
        Me.GroupBox2.Controls.Add(Me.txtFecha)
        Me.GroupBox2.Controls.Add(Me.TxtTarjeta)
        Me.GroupBox2.Controls.Add(Me.TxtMotivo)
        Me.GroupBox2.Controls.Add(Me.TxtMovil)
        Me.GroupBox2.Controls.Add(Me.TxtNIF)
        Me.GroupBox2.Controls.Add(Me.TxtNombre)
        Me.GroupBox2.Controls.Add(Me.TxtEmpresa)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 445)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(862, 248)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        'TxtNumeroEntrada
        '
        Me.TxtNumeroEntrada.Location = New System.Drawing.Point(611, 49)
        Me.TxtNumeroEntrada.Name = "TxtNumeroEntrada"
        Me.TxtNumeroEntrada.ReadOnly = True
        Me.TxtNumeroEntrada.Size = New System.Drawing.Size(137, 20)
        Me.TxtNumeroEntrada.TabIndex = 19
        '
        'TxtEntrada
        '
        Me.TxtEntrada.AutoSize = True
        Me.TxtEntrada.Location = New System.Drawing.Point(521, 52)
        Me.TxtEntrada.Name = "TxtEntrada"
        Me.TxtEntrada.Size = New System.Drawing.Size(83, 13)
        Me.TxtEntrada.TabIndex = 18
        Me.TxtEntrada.Text = "Número entrada"
        '
        'TxtHoraSalida
        '
        Me.TxtHoraSalida.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.TxtHoraSalida.Location = New System.Drawing.Point(613, 182)
        Me.TxtHoraSalida.Name = "TxtHoraSalida"
        Me.TxtHoraSalida.Size = New System.Drawing.Size(113, 20)
        Me.TxtHoraSalida.TabIndex = 17
        '
        'TxtHora_entrada
        '
        Me.TxtHora_entrada.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.TxtHora_entrada.Location = New System.Drawing.Point(103, 182)
        Me.TxtHora_entrada.Name = "TxtHora_entrada"
        Me.TxtHora_entrada.Size = New System.Drawing.Size(113, 20)
        Me.TxtHora_entrada.TabIndex = 16
        '
        'txtFecha
        '
        Me.txtFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFecha.Location = New System.Drawing.Point(613, 20)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New System.Drawing.Size(107, 20)
        Me.txtFecha.TabIndex = 15
        '
        'TxtTarjeta
        '
        Me.TxtTarjeta.Location = New System.Drawing.Point(611, 82)
        Me.TxtTarjeta.Name = "TxtTarjeta"
        Me.TxtTarjeta.Size = New System.Drawing.Size(137, 20)
        Me.TxtTarjeta.TabIndex = 14
        '
        'TxtMotivo
        '
        Me.TxtMotivo.Location = New System.Drawing.Point(111, 152)
        Me.TxtMotivo.Name = "TxtMotivo"
        Me.TxtMotivo.Size = New System.Drawing.Size(640, 20)
        Me.TxtMotivo.TabIndex = 13
        '
        'TxtMovil
        '
        Me.TxtMovil.Location = New System.Drawing.Point(95, 119)
        Me.TxtMovil.Name = "TxtMovil"
        Me.TxtMovil.Size = New System.Drawing.Size(213, 20)
        Me.TxtMovil.TabIndex = 12
        '
        'TxtNIF
        '
        Me.TxtNIF.Location = New System.Drawing.Point(55, 86)
        Me.TxtNIF.Name = "TxtNIF"
        Me.TxtNIF.Size = New System.Drawing.Size(187, 20)
        Me.TxtNIF.TabIndex = 11
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(115, 53)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(392, 20)
        Me.TxtNombre.TabIndex = 10
        '
        'TxtEmpresa
        '
        Me.TxtEmpresa.Location = New System.Drawing.Point(76, 23)
        Me.TxtEmpresa.Name = "TxtEmpresa"
        Me.TxtEmpresa.Size = New System.Drawing.Size(431, 20)
        Me.TxtEmpresa.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(560, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Fecha"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(532, 188)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Hora de salida"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 188)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Hora de entrada"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(521, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Número tarjeta"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Motivo de la visita"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Teléfono móvil"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "NIF"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nombre y apellidos"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Empresa"
        '
        'txtDisplay
        '
        Me.txtDisplay.Location = New System.Drawing.Point(13, 699)
        Me.txtDisplay.Multiline = True
        Me.txtDisplay.Name = "txtDisplay"
        Me.txtDisplay.Size = New System.Drawing.Size(949, 81)
        Me.txtDisplay.TabIndex = 15
        '
        'TxtTextoFirma
        '
        Me.TxtTextoFirma.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTextoFirma.Location = New System.Drawing.Point(64, 723)
        Me.TxtTextoFirma.Multiline = True
        Me.TxtTextoFirma.Name = "TxtTextoFirma"
        Me.TxtTextoFirma.Size = New System.Drawing.Size(100, 20)
        Me.TxtTextoFirma.TabIndex = 16
        Me.TxtTextoFirma.Visible = False
        '
        'ModoAuditoria
        '
        Me.ModoAuditoria.AutoSize = True
        Me.ModoAuditoria.Location = New System.Drawing.Point(807, 78)
        Me.ModoAuditoria.Name = "ModoAuditoria"
        Me.ModoAuditoria.Size = New System.Drawing.Size(98, 17)
        Me.ModoAuditoria.TabIndex = 17
        Me.ModoAuditoria.Text = "Modo auditoría"
        Me.ModoAuditoria.UseVisualStyleBackColor = True
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = Global.Novagestvarios.My.Resources.Resources.salirpersona
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        '
        'EntraPersona
        '
        Me.EntraPersona.Image = Global.Novagestvarios.My.Resources.Resources.entrapersona
        Me.EntraPersona.Location = New System.Drawing.Point(170, 12)
        Me.EntraPersona.Name = "EntraPersona"
        Me.EntraPersona.Size = New System.Drawing.Size(85, 74)
        Me.EntraPersona.TabIndex = 14
        Me.EntraPersona.UseVisualStyleBackColor = True
        '
        'Buscar
        '
        Me.Buscar.AutoSize = True
        Me.Buscar.Image = Global.Novagestvarios.My.Resources.Resources.buscar
        Me.Buscar.Location = New System.Drawing.Point(911, 95)
        Me.Buscar.Name = "Buscar"
        Me.Buscar.Size = New System.Drawing.Size(44, 38)
        Me.Buscar.TabIndex = 11
        Me.Buscar.UseVisualStyleBackColor = True
        '
        'Nuevo
        '
        Me.Nuevo.Image = Global.Novagestvarios.My.Resources.Resources.addpersona
        Me.Nuevo.Location = New System.Drawing.Point(13, 12)
        Me.Nuevo.Name = "Nuevo"
        Me.Nuevo.Size = New System.Drawing.Size(85, 74)
        Me.Nuevo.TabIndex = 0
        Me.Nuevo.UseVisualStyleBackColor = True
        '
        'Visitas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 784)
        Me.Controls.Add(Me.ModoAuditoria)
        Me.Controls.Add(Me.TxtTextoFirma)
        Me.Controls.Add(Me.txtDisplay)
        Me.Controls.Add(Me.EntraPersona)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Buscar)
        Me.Controls.Add(Me.cbcodigo)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.Nuevo)
        Me.Name = "Visitas"
        Me.Text = "Control de visitas"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgv As DataGridView
    Friend WithEvents Buscar As Button
    Friend WithEvents cbcodigo As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Salir As Button
    Friend WithEvents Firmar As Button
    Friend WithEvents Nuevo As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TxtHora_entrada As DateTimePicker
    Friend WithEvents txtFecha As DateTimePicker
    Friend WithEvents TxtTarjeta As TextBox
    Friend WithEvents TxtMotivo As TextBox
    Friend WithEvents TxtMovil As TextBox
    Friend WithEvents TxtNIF As TextBox
    Friend WithEvents TxtNombre As TextBox
    Friend WithEvents TxtEmpresa As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtHoraSalida As DateTimePicker
    Friend WithEvents EntraPersona As Button
    Friend WithEvents TxtNumeroEntrada As TextBox
    Friend WithEvents TxtEntrada As Label
    Friend WithEvents txtDisplay As TextBox
    Friend WithEvents TxtTextoFirma As TextBox
    Friend WithEvents nombre As DataGridViewTextBoxColumn
    Friend WithEvents empresa As DataGridViewTextBoxColumn
    Friend WithEvents nif As DataGridViewTextBoxColumn
    Friend WithEvents salirsalir As DataGridViewImageColumn
    Friend WithEvents Entrada As DataGridViewTextBoxColumn
    Friend WithEvents ModoAuditoria As CheckBox
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents GrabarModif As Button
End Class
