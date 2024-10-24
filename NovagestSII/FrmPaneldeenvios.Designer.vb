<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaneldeenvios
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
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lbldocumento1 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblcuota5 = New System.Windows.Forms.Label()
        Me.Msf = New System.Windows.Forms.DataGridView()
        Me.lblcuota4 = New System.Windows.Forms.Label()
        Me.lblcuota3 = New System.Windows.Forms.Label()
        Me.lblcuota2 = New System.Windows.Forms.Label()
        Me.lblcuota1 = New System.Windows.Forms.Label()
        Me.lbltipo5 = New System.Windows.Forms.Label()
        Me.lbltipo4 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lbltipo3 = New System.Windows.Forms.Label()
        Me.lbltipo2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbldocumento3 = New System.Windows.Forms.Label()
        Me.lbldocumento2 = New System.Windows.Forms.Label()
        Me.lbltipo1 = New System.Windows.Forms.Label()
        Me.lblbase5 = New System.Windows.Forms.Label()
        Me.lblbase4 = New System.Windows.Forms.Label()
        Me.lblbase3 = New System.Windows.Forms.Label()
        Me.lblbase2 = New System.Windows.Forms.Label()
        Me.lblbase1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbldocumento4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Txtperiodo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmdEmitir = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ckerroneas = New System.Windows.Forms.CheckBox()
        Me.ckconerrores = New System.Windows.Forms.CheckBox()
        Me.ckaceptadas = New System.Windows.Forms.CheckBox()
        Me.CkPendientes = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Option3 = New System.Windows.Forms.RadioButton()
        Me.Option2 = New System.Windows.Forms.RadioButton()
        Me.Option1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Openvio = New System.Windows.Forms.RadioButton()
        Me.Opfactura = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DTPDesdefecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.DTPHastafecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.cmdrecisarrecibidas = New System.Windows.Forms.Button()
        Me.cmdrevisaremitidas = New System.Windows.Forms.Button()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.CmdCargar = New System.Windows.Forms.Button()
        CType(Me.Msf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(18, 100)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(117, 13)
        Me.Label23.TabIndex = 2
        Me.Label23.Text = "Aceptados con errores:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(18, 67)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(123, 13)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "Documentos aceptados:"
        '
        'lbldocumento1
        '
        Me.lbldocumento1.Location = New System.Drawing.Point(146, 34)
        Me.lbldocumento1.Name = "lbldocumento1"
        Me.lbldocumento1.Size = New System.Drawing.Size(51, 13)
        Me.lbldocumento1.TabIndex = 31
        Me.lbldocumento1.Text = "-"
        Me.lbldocumento1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(18, 34)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(125, 13)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Documentos pendientes:"
        '
        'lblcuota5
        '
        Me.lblcuota5.Location = New System.Drawing.Point(409, 151)
        Me.lblcuota5.Name = "lblcuota5"
        Me.lblcuota5.Size = New System.Drawing.Size(77, 13)
        Me.lblcuota5.TabIndex = 30
        Me.lblcuota5.Text = "-"
        Me.lblcuota5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Msf
        '
        Me.Msf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Msf.Location = New System.Drawing.Point(21, 98)
        Me.Msf.Name = "Msf"
        Me.Msf.ReadOnly = True
        Me.Msf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Msf.Size = New System.Drawing.Size(1084, 324)
        Me.Msf.TabIndex = 23
        '
        'lblcuota4
        '
        Me.lblcuota4.Location = New System.Drawing.Point(409, 125)
        Me.lblcuota4.Name = "lblcuota4"
        Me.lblcuota4.Size = New System.Drawing.Size(77, 13)
        Me.lblcuota4.TabIndex = 29
        Me.lblcuota4.Text = "-"
        Me.lblcuota4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblcuota3
        '
        Me.lblcuota3.Location = New System.Drawing.Point(409, 99)
        Me.lblcuota3.Name = "lblcuota3"
        Me.lblcuota3.Size = New System.Drawing.Size(77, 13)
        Me.lblcuota3.TabIndex = 28
        Me.lblcuota3.Text = "-"
        Me.lblcuota3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblcuota2
        '
        Me.lblcuota2.Location = New System.Drawing.Point(409, 73)
        Me.lblcuota2.Name = "lblcuota2"
        Me.lblcuota2.Size = New System.Drawing.Size(77, 13)
        Me.lblcuota2.TabIndex = 27
        Me.lblcuota2.Text = "-"
        Me.lblcuota2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblcuota1
        '
        Me.lblcuota1.Location = New System.Drawing.Point(409, 47)
        Me.lblcuota1.Name = "lblcuota1"
        Me.lblcuota1.Size = New System.Drawing.Size(77, 13)
        Me.lblcuota1.TabIndex = 26
        Me.lblcuota1.Text = "-"
        Me.lblcuota1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbltipo5
        '
        Me.lbltipo5.Location = New System.Drawing.Point(196, 151)
        Me.lbltipo5.Name = "lbltipo5"
        Me.lbltipo5.Size = New System.Drawing.Size(77, 13)
        Me.lbltipo5.TabIndex = 25
        Me.lbltipo5.Text = "-"
        Me.lbltipo5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbltipo4
        '
        Me.lbltipo4.Location = New System.Drawing.Point(196, 125)
        Me.lbltipo4.Name = "lbltipo4"
        Me.lbltipo4.Size = New System.Drawing.Size(77, 13)
        Me.lbltipo4.TabIndex = 24
        Me.lbltipo4.Text = "-"
        Me.lbltipo4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(18, 133)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(117, 13)
        Me.Label24.TabIndex = 3
        Me.Label24.Text = "Documentos errróneas:"
        '
        'lbltipo3
        '
        Me.lbltipo3.Location = New System.Drawing.Point(196, 99)
        Me.lbltipo3.Name = "lbltipo3"
        Me.lbltipo3.Size = New System.Drawing.Size(77, 13)
        Me.lbltipo3.TabIndex = 23
        Me.lbltipo3.Text = "-"
        Me.lbltipo3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbltipo2
        '
        Me.lbltipo2.Location = New System.Drawing.Point(196, 73)
        Me.lbltipo2.Name = "lbltipo2"
        Me.lbltipo2.Size = New System.Drawing.Size(77, 13)
        Me.lbltipo2.TabIndex = 22
        Me.lbltipo2.Text = "-"
        Me.lbltipo2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(446, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Cuotas"
        '
        'lbldocumento3
        '
        Me.lbldocumento3.Location = New System.Drawing.Point(146, 100)
        Me.lbldocumento3.Name = "lbldocumento3"
        Me.lbldocumento3.Size = New System.Drawing.Size(51, 13)
        Me.lbldocumento3.TabIndex = 33
        Me.lbldocumento3.Text = "-"
        Me.lbldocumento3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbldocumento2
        '
        Me.lbldocumento2.Location = New System.Drawing.Point(146, 67)
        Me.lbldocumento2.Name = "lbldocumento2"
        Me.lbldocumento2.Size = New System.Drawing.Size(51, 13)
        Me.lbldocumento2.TabIndex = 32
        Me.lbldocumento2.Text = "-"
        Me.lbldocumento2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbltipo1
        '
        Me.lbltipo1.Location = New System.Drawing.Point(196, 47)
        Me.lbltipo1.Name = "lbltipo1"
        Me.lbltipo1.Size = New System.Drawing.Size(77, 13)
        Me.lbltipo1.TabIndex = 21
        Me.lbltipo1.Text = "-"
        Me.lbltipo1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblbase5
        '
        Me.lblbase5.Location = New System.Drawing.Point(13, 151)
        Me.lblbase5.Name = "lblbase5"
        Me.lblbase5.Size = New System.Drawing.Size(77, 13)
        Me.lblbase5.TabIndex = 20
        Me.lblbase5.Text = "-"
        Me.lblbase5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblbase4
        '
        Me.lblbase4.Location = New System.Drawing.Point(13, 125)
        Me.lblbase4.Name = "lblbase4"
        Me.lblbase4.Size = New System.Drawing.Size(77, 13)
        Me.lblbase4.TabIndex = 19
        Me.lblbase4.Text = "-"
        Me.lblbase4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblbase3
        '
        Me.lblbase3.Location = New System.Drawing.Point(13, 99)
        Me.lblbase3.Name = "lblbase3"
        Me.lblbase3.Size = New System.Drawing.Size(77, 13)
        Me.lblbase3.TabIndex = 18
        Me.lblbase3.Text = "-"
        Me.lblbase3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblbase2
        '
        Me.lblbase2.Location = New System.Drawing.Point(13, 73)
        Me.lblbase2.Name = "lblbase2"
        Me.lblbase2.Size = New System.Drawing.Size(77, 13)
        Me.lblbase2.TabIndex = 17
        Me.lblbase2.Text = "-"
        Me.lblbase2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblbase1
        '
        Me.lblbase1.Location = New System.Drawing.Point(13, 47)
        Me.lblbase1.Name = "lblbase1"
        Me.lblbase1.Size = New System.Drawing.Size(77, 13)
        Me.lblbase1.TabIndex = 16
        Me.lblbase1.Text = "-"
        Me.lblbase1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = " "
        '
        'lbldocumento4
        '
        Me.lbldocumento4.Location = New System.Drawing.Point(146, 133)
        Me.lbldocumento4.Name = "lbldocumento4"
        Me.lbldocumento4.Size = New System.Drawing.Size(51, 13)
        Me.lbldocumento4.TabIndex = 34
        Me.lbldocumento4.Text = "-"
        Me.lbldocumento4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(232, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Tipo%"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lbldocumento4)
        Me.GroupBox7.Controls.Add(Me.Label24)
        Me.GroupBox7.Controls.Add(Me.lbldocumento3)
        Me.GroupBox7.Controls.Add(Me.Label23)
        Me.GroupBox7.Controls.Add(Me.lbldocumento2)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.Controls.Add(Me.lbldocumento1)
        Me.GroupBox7.Controls.Add(Me.Label21)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox7.Location = New System.Drawing.Point(555, 428)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(541, 189)
        Me.GroupBox7.TabIndex = 18
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = " "
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblcuota5)
        Me.GroupBox6.Controls.Add(Me.lblcuota4)
        Me.GroupBox6.Controls.Add(Me.lblcuota3)
        Me.GroupBox6.Controls.Add(Me.lblcuota2)
        Me.GroupBox6.Controls.Add(Me.lblcuota1)
        Me.GroupBox6.Controls.Add(Me.lbltipo5)
        Me.GroupBox6.Controls.Add(Me.lbltipo4)
        Me.GroupBox6.Controls.Add(Me.lbltipo3)
        Me.GroupBox6.Controls.Add(Me.lbltipo2)
        Me.GroupBox6.Controls.Add(Me.lbltipo1)
        Me.GroupBox6.Controls.Add(Me.lblbase5)
        Me.GroupBox6.Controls.Add(Me.lblbase4)
        Me.GroupBox6.Controls.Add(Me.lblbase3)
        Me.GroupBox6.Controls.Add(Me.lblbase2)
        Me.GroupBox6.Controls.Add(Me.lblbase1)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.Label4)
        Me.GroupBox6.Controls.Add(Me.Label3)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox6.Location = New System.Drawing.Point(9, 428)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(540, 189)
        Me.GroupBox6.TabIndex = 17
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = " "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(53, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Base"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Txtperiodo)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.CmdEmitir)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox5.Location = New System.Drawing.Point(800, 7)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(104, 84)
        Me.GroupBox5.TabIndex = 16
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = " Presentadas"
        '
        'Txtperiodo
        '
        Me.Txtperiodo.Location = New System.Drawing.Point(6, 30)
        Me.Txtperiodo.Name = "Txtperiodo"
        Me.Txtperiodo.Size = New System.Drawing.Size(92, 20)
        Me.Txtperiodo.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Periodo"
        '
        'CmdEmitir
        '
        Me.CmdEmitir.Location = New System.Drawing.Point(6, 56)
        Me.CmdEmitir.Name = "CmdEmitir"
        Me.CmdEmitir.Size = New System.Drawing.Size(92, 22)
        Me.CmdEmitir.TabIndex = 8
        Me.CmdEmitir.Text = "Emitir"
        Me.CmdEmitir.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ckerroneas)
        Me.GroupBox4.Controls.Add(Me.ckconerrores)
        Me.GroupBox4.Controls.Add(Me.ckaceptadas)
        Me.GroupBox4.Controls.Add(Me.CkPendientes)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(268, 56)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(430, 39)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = " Situación"
        '
        'ckerroneas
        '
        Me.ckerroneas.AutoSize = True
        Me.ckerroneas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckerroneas.Location = New System.Drawing.Point(348, 13)
        Me.ckerroneas.Name = "ckerroneas"
        Me.ckerroneas.Size = New System.Drawing.Size(68, 17)
        Me.ckerroneas.TabIndex = 11
        Me.ckerroneas.Text = "Erróneas"
        Me.ckerroneas.UseVisualStyleBackColor = True
        '
        'ckconerrores
        '
        Me.ckconerrores.AutoSize = True
        Me.ckconerrores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckconerrores.Location = New System.Drawing.Point(190, 13)
        Me.ckconerrores.Name = "ckconerrores"
        Me.ckconerrores.Size = New System.Drawing.Size(133, 17)
        Me.ckconerrores.TabIndex = 11
        Me.ckconerrores.Text = "Aceptadas con errores"
        Me.ckconerrores.UseVisualStyleBackColor = True
        '
        'ckaceptadas
        '
        Me.ckaceptadas.AutoSize = True
        Me.ckaceptadas.Checked = True
        Me.ckaceptadas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckaceptadas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckaceptadas.Location = New System.Drawing.Point(100, 13)
        Me.ckaceptadas.Name = "ckaceptadas"
        Me.ckaceptadas.Size = New System.Drawing.Size(77, 17)
        Me.ckaceptadas.TabIndex = 1
        Me.ckaceptadas.Text = "Aceptadas"
        Me.ckaceptadas.UseVisualStyleBackColor = True
        '
        'CkPendientes
        '
        Me.CkPendientes.AutoSize = True
        Me.CkPendientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CkPendientes.Location = New System.Drawing.Point(7, 13)
        Me.CkPendientes.Name = "CkPendientes"
        Me.CkPendientes.Size = New System.Drawing.Size(79, 17)
        Me.CkPendientes.TabIndex = 0
        Me.CkPendientes.Text = "Pendientes"
        Me.CkPendientes.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Option3)
        Me.GroupBox3.Controls.Add(Me.Option2)
        Me.GroupBox3.Controls.Add(Me.Option1)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(268, 7)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(430, 46)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " Tipo factura"
        '
        'Option3
        '
        Me.Option3.AutoSize = True
        Me.Option3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option3.Location = New System.Drawing.Point(256, 20)
        Me.Option3.Name = "Option3"
        Me.Option3.Size = New System.Drawing.Size(117, 17)
        Me.Option3.TabIndex = 13
        Me.Option3.Text = "Bienes de inversión"
        Me.Option3.UseVisualStyleBackColor = True
        '
        'Option2
        '
        Me.Option2.AutoSize = True
        Me.Option2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option2.Location = New System.Drawing.Point(135, 20)
        Me.Option2.Name = "Option2"
        Me.Option2.Size = New System.Drawing.Size(72, 17)
        Me.Option2.TabIndex = 12
        Me.Option2.Text = "Recibidas"
        Me.Option2.UseVisualStyleBackColor = True
        '
        'Option1
        '
        Me.Option1.AutoSize = True
        Me.Option1.Checked = True
        Me.Option1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Option1.Location = New System.Drawing.Point(22, 20)
        Me.Option1.Name = "Option1"
        Me.Option1.Size = New System.Drawing.Size(64, 17)
        Me.Option1.TabIndex = 11
        Me.Option1.TabStop = True
        Me.Option1.Text = "Emitidas"
        Me.Option1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Openvio)
        Me.GroupBox2.Controls.Add(Me.Opfactura)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(9, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(253, 39)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Tipo de fecha"
        '
        'Openvio
        '
        Me.Openvio.AutoSize = True
        Me.Openvio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Openvio.Location = New System.Drawing.Point(134, 16)
        Me.Openvio.Name = "Openvio"
        Me.Openvio.Size = New System.Drawing.Size(86, 17)
        Me.Openvio.TabIndex = 11
        Me.Openvio.Text = "Fecha envío"
        Me.Openvio.UseVisualStyleBackColor = True
        '
        'Opfactura
        '
        Me.Opfactura.AutoSize = True
        Me.Opfactura.Checked = True
        Me.Opfactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opfactura.Location = New System.Drawing.Point(12, 19)
        Me.Opfactura.Name = "Opfactura"
        Me.Opfactura.Size = New System.Drawing.Size(91, 17)
        Me.Opfactura.TabIndex = 0
        Me.Opfactura.TabStop = True
        Me.Opfactura.Text = "Fecha factura"
        Me.Opfactura.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(9, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(253, 14)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Entre las fechas"
        '
        'DTPDesdefecha
        '
        Me.DTPDesdefecha.ButtonImage = Nothing
        Me.DTPDesdefecha.FormatoFecha = "dd/MM/yyyy"
        Me.DTPDesdefecha.Location = New System.Drawing.Point(21, 21)
        Me.DTPDesdefecha.Name = "DTPDesdefecha"
        Me.DTPDesdefecha.Size = New System.Drawing.Size(96, 20)
        Me.DTPDesdefecha.TabIndex = 0
        Me.DTPDesdefecha.Text = "02/05/2024"
        Me.DTPDesdefecha.value = New Date(2024, 5, 2, 0, 0, 0, 0)
        '
        'DTPHastafecha
        '
        Me.DTPHastafecha.ButtonImage = Nothing
        Me.DTPHastafecha.FormatoFecha = "dd/MM/yyyy"
        Me.DTPHastafecha.Location = New System.Drawing.Point(143, 21)
        Me.DTPHastafecha.Name = "DTPHastafecha"
        Me.DTPHastafecha.Size = New System.Drawing.Size(97, 20)
        Me.DTPHastafecha.TabIndex = 1
        Me.DTPHastafecha.Text = "02/05/2024"
        Me.DTPHastafecha.value = New Date(2024, 5, 2, 0, 0, 0, 0)
        '
        'cmdrecisarrecibidas
        '
        Me.cmdrecisarrecibidas.Location = New System.Drawing.Point(910, 52)
        Me.cmdrecisarrecibidas.Name = "cmdrecisarrecibidas"
        Me.cmdrecisarrecibidas.Size = New System.Drawing.Size(98, 39)
        Me.cmdrecisarrecibidas.TabIndex = 21
        Me.cmdrecisarrecibidas.Text = "Revisar recibidas"
        Me.cmdrecisarrecibidas.UseVisualStyleBackColor = True
        '
        'cmdrevisaremitidas
        '
        Me.cmdrevisaremitidas.Location = New System.Drawing.Point(910, 9)
        Me.cmdrevisaremitidas.Name = "cmdrevisaremitidas"
        Me.cmdrevisaremitidas.Size = New System.Drawing.Size(98, 39)
        Me.cmdrevisaremitidas.TabIndex = 20
        Me.cmdrevisaremitidas.Text = "Revisar emitidas"
        Me.cmdrevisaremitidas.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = Global.novagestSII.My.Resources.Resources.salir1
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(1014, 10)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(82, 79)
        Me.CmdSalir.TabIndex = 22
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'CmdCargar
        '
        Me.CmdCargar.BackgroundImage = Global.novagestSII.My.Resources.Resources.subir_registros
        Me.CmdCargar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdCargar.Location = New System.Drawing.Point(704, 11)
        Me.CmdCargar.Name = "CmdCargar"
        Me.CmdCargar.Size = New System.Drawing.Size(90, 84)
        Me.CmdCargar.TabIndex = 19
        Me.CmdCargar.UseVisualStyleBackColor = True
        '
        'FrmPaneldeenvios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1112, 622)
        Me.Controls.Add(Me.DTPHastafecha)
        Me.Controls.Add(Me.DTPDesdefecha)
        Me.Controls.Add(Me.Msf)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdrecisarrecibidas)
        Me.Controls.Add(Me.cmdrevisaremitidas)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.CmdCargar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmPaneldeenvios"
        Me.Text = "Panel de envios"
        CType(Me.Msf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label23 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents lbldocumento1 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents lblcuota5 As Label
    Friend WithEvents Msf As DataGridView
    Friend WithEvents lblcuota4 As Label
    Friend WithEvents lblcuota3 As Label
    Friend WithEvents lblcuota2 As Label
    Friend WithEvents lblcuota1 As Label
    Friend WithEvents lbltipo5 As Label
    Friend WithEvents lbltipo4 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents lbltipo3 As Label
    Friend WithEvents lbltipo2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbldocumento3 As Label
    Friend WithEvents lbldocumento2 As Label
    Friend WithEvents lbltipo1 As Label
    Friend WithEvents lblbase5 As Label
    Friend WithEvents lblbase4 As Label
    Friend WithEvents lblbase3 As Label
    Friend WithEvents lblbase2 As Label
    Friend WithEvents lblbase1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lbldocumento4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Txtperiodo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CmdEmitir As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ckerroneas As CheckBox
    Friend WithEvents ckconerrores As CheckBox
    Friend WithEvents ckaceptadas As CheckBox
    Friend WithEvents CkPendientes As CheckBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Option3 As RadioButton
    Friend WithEvents Option2 As RadioButton
    Friend WithEvents Option1 As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Openvio As RadioButton
    Friend WithEvents Opfactura As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DTPHastafecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents DTPDesdefecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents cmdrecisarrecibidas As Button
    Friend WithEvents cmdrevisaremitidas As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdCargar As Button
End Class
