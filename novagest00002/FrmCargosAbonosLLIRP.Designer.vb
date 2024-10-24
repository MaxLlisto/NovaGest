<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCargosAbonosLLIRP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCargosAbonosLLIRP))
        Me.TabPrincipal = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lblProgreso = New System.Windows.Forms.Label()
        Me.TxtCuantosCargos = New System.Windows.Forms.Label()
        Me.TxtCuantosAbonos = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.DTFechaEmision = New System.Windows.Forms.DateTimePicker()
        Me.DTFechaEnvio = New System.Windows.Forms.DateTimePicker()
        Me.CmdBorraRemesa = New System.Windows.Forms.Button()
        Me.CmdImprimirAbonos = New System.Windows.Forms.Button()
        Me.CmdGenerarsepa = New System.Windows.Forms.Button()
        Me.CmdImprimirCargos = New System.Windows.Forms.Button()
        Me.CmdSeleccionRemesa = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.CmdGuardarCargo = New System.Windows.Forms.Button()
        Me.TxtFicheroCargos = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CmdGuardarAbono = New System.Windows.Forms.Button()
        Me.TxtFicheroAbonos = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TxtCodCuenta2 = New System.Windows.Forms.TextBox()
        Me.TxtDC2 = New System.Windows.Forms.TextBox()
        Me.TxtOficina2 = New System.Windows.Forms.TextBox()
        Me.TxtEntidad2 = New System.Windows.Forms.TextBox()
        Me.TxtCuenta2 = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TxtNifCargo = New System.Windows.Forms.TextBox()
        Me.TxtPoblacionCargo = New System.Windows.Forms.TextBox()
        Me.TxtDomicilioCargo = New System.Windows.Forms.TextBox()
        Me.TxtNombreCargo = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtCodCuenta1 = New System.Windows.Forms.TextBox()
        Me.TxtDC1 = New System.Windows.Forms.TextBox()
        Me.TxtOficina1 = New System.Windows.Forms.TextBox()
        Me.TxtCuenta1 = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtEntidad1 = New System.Windows.Forms.TextBox()
        Me.TxtNifAbono = New System.Windows.Forms.TextBox()
        Me.TxtPoblacionAbono = New System.Windows.Forms.TextBox()
        Me.TxtDomicilioAbono = New System.Windows.Forms.TextBox()
        Me.TxtNombreAbono = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lvabonos = New System.Windows.Forms.ListView()
        Me.TxtTotalAbonos = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TxtTotalCargos = New System.Windows.Forms.TextBox()
        Me.LvCargos = New System.Windows.Forms.ListView()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TabPrincipal.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPrincipal
        '
        Me.TabPrincipal.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabPrincipal.Controls.Add(Me.TabPage1)
        Me.TabPrincipal.Controls.Add(Me.TabPage2)
        Me.TabPrincipal.Controls.Add(Me.TabPage3)
        Me.TabPrincipal.Location = New System.Drawing.Point(2, 12)
        Me.TabPrincipal.Name = "TabPrincipal"
        Me.TabPrincipal.SelectedIndex = 0
        Me.TabPrincipal.Size = New System.Drawing.Size(855, 456)
        Me.TabPrincipal.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblProgreso)
        Me.TabPage1.Controls.Add(Me.TxtCuantosCargos)
        Me.TabPage1.Controls.Add(Me.TxtCuantosAbonos)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.pb)
        Me.TabPage1.Controls.Add(Me.DTFechaEmision)
        Me.TabPage1.Controls.Add(Me.DTFechaEnvio)
        Me.TabPage1.Controls.Add(Me.CmdBorraRemesa)
        Me.TabPage1.Controls.Add(Me.CmdImprimirAbonos)
        Me.TabPage1.Controls.Add(Me.CmdGenerarsepa)
        Me.TabPage1.Controls.Add(Me.CmdImprimirCargos)
        Me.TabPage1.Controls.Add(Me.CmdSeleccionRemesa)
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(847, 430)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Ficheros"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lblProgreso
        '
        Me.lblProgreso.AutoSize = True
        Me.lblProgreso.Location = New System.Drawing.Point(413, 313)
        Me.lblProgreso.Name = "lblProgreso"
        Me.lblProgreso.Size = New System.Drawing.Size(0, 13)
        Me.lblProgreso.TabIndex = 17
        '
        'TxtCuantosCargos
        '
        Me.TxtCuantosCargos.BackColor = System.Drawing.Color.Linen
        Me.TxtCuantosCargos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCuantosCargos.Cursor = System.Windows.Forms.Cursors.Default
        Me.TxtCuantosCargos.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.TxtCuantosCargos.Location = New System.Drawing.Point(493, 41)
        Me.TxtCuantosCargos.Name = "TxtCuantosCargos"
        Me.TxtCuantosCargos.Size = New System.Drawing.Size(71, 20)
        Me.TxtCuantosCargos.TabIndex = 16
        Me.TxtCuantosCargos.Text = "0"
        Me.TxtCuantosCargos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtCuantosAbonos
        '
        Me.TxtCuantosAbonos.BackColor = System.Drawing.Color.Linen
        Me.TxtCuantosAbonos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCuantosAbonos.Cursor = System.Windows.Forms.Cursors.Default
        Me.TxtCuantosAbonos.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.TxtCuantosAbonos.Location = New System.Drawing.Point(311, 41)
        Me.TxtCuantosAbonos.Name = "TxtCuantosAbonos"
        Me.TxtCuantosAbonos.Size = New System.Drawing.Size(71, 20)
        Me.TxtCuantosAbonos.TabIndex = 15
        Me.TxtCuantosAbonos.Text = "0"
        Me.TxtCuantosAbonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(155, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(494, 20)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "La tabla contiene                     abonos y                      cargos."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(178, 384)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Fecha emisión:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(185, 349)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Fecha envío:"
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(6, 317)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(817, 10)
        Me.pb.TabIndex = 11
        '
        'DTFechaEmision
        '
        Me.DTFechaEmision.Checked = False
        Me.DTFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFechaEmision.Location = New System.Drawing.Point(262, 382)
        Me.DTFechaEmision.Name = "DTFechaEmision"
        Me.DTFechaEmision.Size = New System.Drawing.Size(90, 20)
        Me.DTFechaEmision.TabIndex = 10
        '
        'DTFechaEnvio
        '
        Me.DTFechaEnvio.Checked = False
        Me.DTFechaEnvio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFechaEnvio.Location = New System.Drawing.Point(262, 346)
        Me.DTFechaEnvio.Name = "DTFechaEnvio"
        Me.DTFechaEnvio.Size = New System.Drawing.Size(90, 20)
        Me.DTFechaEnvio.TabIndex = 9
        '
        'CmdBorraRemesa
        '
        Me.CmdBorraRemesa.BackgroundImage = CType(resources.GetObject("CmdBorraRemesa.BackgroundImage"), System.Drawing.Image)
        Me.CmdBorraRemesa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBorraRemesa.Location = New System.Drawing.Point(6, 333)
        Me.CmdBorraRemesa.Name = "CmdBorraRemesa"
        Me.CmdBorraRemesa.Size = New System.Drawing.Size(79, 91)
        Me.CmdBorraRemesa.TabIndex = 6
        Me.CmdBorraRemesa.UseVisualStyleBackColor = True
        '
        'CmdImprimirAbonos
        '
        Me.CmdImprimirAbonos.BackgroundImage = CType(resources.GetObject("CmdImprimirAbonos.BackgroundImage"), System.Drawing.Image)
        Me.CmdImprimirAbonos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdImprimirAbonos.Location = New System.Drawing.Point(563, 333)
        Me.CmdImprimirAbonos.Name = "CmdImprimirAbonos"
        Me.CmdImprimirAbonos.Size = New System.Drawing.Size(118, 91)
        Me.CmdImprimirAbonos.TabIndex = 8
        Me.CmdImprimirAbonos.UseVisualStyleBackColor = True
        '
        'CmdGenerarsepa
        '
        Me.CmdGenerarsepa.BackgroundImage = CType(resources.GetObject("CmdGenerarsepa.BackgroundImage"), System.Drawing.Image)
        Me.CmdGenerarsepa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdGenerarsepa.Location = New System.Drawing.Point(368, 333)
        Me.CmdGenerarsepa.Name = "CmdGenerarsepa"
        Me.CmdGenerarsepa.Size = New System.Drawing.Size(136, 91)
        Me.CmdGenerarsepa.TabIndex = 7
        Me.CmdGenerarsepa.UseVisualStyleBackColor = True
        '
        'CmdImprimirCargos
        '
        Me.CmdImprimirCargos.BackgroundImage = CType(resources.GetObject("CmdImprimirCargos.BackgroundImage"), System.Drawing.Image)
        Me.CmdImprimirCargos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdImprimirCargos.Location = New System.Drawing.Point(687, 333)
        Me.CmdImprimirCargos.Name = "CmdImprimirCargos"
        Me.CmdImprimirCargos.Size = New System.Drawing.Size(136, 91)
        Me.CmdImprimirCargos.TabIndex = 6
        Me.CmdImprimirCargos.UseVisualStyleBackColor = True
        '
        'CmdSeleccionRemesa
        '
        Me.CmdSeleccionRemesa.BackgroundImage = CType(resources.GetObject("CmdSeleccionRemesa.BackgroundImage"), System.Drawing.Image)
        Me.CmdSeleccionRemesa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSeleccionRemesa.Location = New System.Drawing.Point(91, 333)
        Me.CmdSeleccionRemesa.Name = "CmdSeleccionRemesa"
        Me.CmdSeleccionRemesa.Size = New System.Drawing.Size(81, 91)
        Me.CmdSeleccionRemesa.TabIndex = 5
        Me.CmdSeleccionRemesa.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.CmdGuardarCargo)
        Me.Panel4.Controls.Add(Me.TxtFicheroCargos)
        Me.Panel4.Location = New System.Drawing.Point(428, 271)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(395, 42)
        Me.Panel4.TabIndex = 4
        '
        'CmdGuardarCargo
        '
        Me.CmdGuardarCargo.BackgroundImage = CType(resources.GetObject("CmdGuardarCargo.BackgroundImage"), System.Drawing.Image)
        Me.CmdGuardarCargo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdGuardarCargo.Location = New System.Drawing.Point(347, 7)
        Me.CmdGuardarCargo.Name = "CmdGuardarCargo"
        Me.CmdGuardarCargo.Size = New System.Drawing.Size(33, 29)
        Me.CmdGuardarCargo.TabIndex = 3
        Me.CmdGuardarCargo.UseVisualStyleBackColor = True
        '
        'TxtFicheroCargos
        '
        Me.TxtFicheroCargos.Location = New System.Drawing.Point(15, 10)
        Me.TxtFicheroCargos.Name = "TxtFicheroCargos"
        Me.TxtFicheroCargos.Size = New System.Drawing.Size(326, 20)
        Me.TxtFicheroCargos.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.CmdGuardarAbono)
        Me.Panel3.Controls.Add(Me.TxtFicheroAbonos)
        Me.Panel3.Location = New System.Drawing.Point(6, 271)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(395, 42)
        Me.Panel3.TabIndex = 3
        '
        'CmdGuardarAbono
        '
        Me.CmdGuardarAbono.BackgroundImage = CType(resources.GetObject("CmdGuardarAbono.BackgroundImage"), System.Drawing.Image)
        Me.CmdGuardarAbono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdGuardarAbono.Location = New System.Drawing.Point(343, 10)
        Me.CmdGuardarAbono.Name = "CmdGuardarAbono"
        Me.CmdGuardarAbono.Size = New System.Drawing.Size(33, 29)
        Me.CmdGuardarAbono.TabIndex = 1
        Me.CmdGuardarAbono.UseVisualStyleBackColor = True
        '
        'TxtFicheroAbonos
        '
        Me.TxtFicheroAbonos.Location = New System.Drawing.Point(11, 13)
        Me.TxtFicheroAbonos.Name = "TxtFicheroAbonos"
        Me.TxtFicheroAbonos.Size = New System.Drawing.Size(326, 20)
        Me.TxtFicheroAbonos.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TxtCodCuenta2)
        Me.Panel2.Controls.Add(Me.TxtDC2)
        Me.Panel2.Controls.Add(Me.TxtOficina2)
        Me.Panel2.Controls.Add(Me.TxtEntidad2)
        Me.Panel2.Controls.Add(Me.TxtCuenta2)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.TxtNifCargo)
        Me.Panel2.Controls.Add(Me.TxtPoblacionCargo)
        Me.Panel2.Controls.Add(Me.TxtDomicilioCargo)
        Me.Panel2.Controls.Add(Me.TxtNombreCargo)
        Me.Panel2.Location = New System.Drawing.Point(428, 79)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(395, 186)
        Me.Panel2.TabIndex = 2
        '
        'TxtCodCuenta2
        '
        Me.TxtCodCuenta2.Location = New System.Drawing.Point(221, 158)
        Me.TxtCodCuenta2.Name = "TxtCodCuenta2"
        Me.TxtCodCuenta2.Size = New System.Drawing.Size(120, 20)
        Me.TxtCodCuenta2.TabIndex = 37
        '
        'TxtDC2
        '
        Me.TxtDC2.Location = New System.Drawing.Point(186, 158)
        Me.TxtDC2.Name = "TxtDC2"
        Me.TxtDC2.Size = New System.Drawing.Size(29, 20)
        Me.TxtDC2.TabIndex = 36
        '
        'TxtOficina2
        '
        Me.TxtOficina2.Location = New System.Drawing.Point(137, 158)
        Me.TxtOficina2.Name = "TxtOficina2"
        Me.TxtOficina2.Size = New System.Drawing.Size(43, 20)
        Me.TxtOficina2.TabIndex = 35
        '
        'TxtEntidad2
        '
        Me.TxtEntidad2.Location = New System.Drawing.Point(88, 158)
        Me.TxtEntidad2.Name = "TxtEntidad2"
        Me.TxtEntidad2.Size = New System.Drawing.Size(43, 20)
        Me.TxtEntidad2.TabIndex = 34
        '
        'TxtCuenta2
        '
        Me.TxtCuenta2.FormattingEnabled = True
        Me.TxtCuenta2.Location = New System.Drawing.Point(88, 28)
        Me.TxtCuenta2.Name = "TxtCuenta2"
        Me.TxtCuenta2.Size = New System.Drawing.Size(299, 21)
        Me.TxtCuenta2.TabIndex = 33
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(27, 6)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(61, 16)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "Cargos:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(39, 161)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "SEPA:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(50, 139)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(27, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "NIF:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(20, 113)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Población:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(25, 87)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 13)
        Me.Label14.TabIndex = 28
        Me.Label14.Text = "Domicilio:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(30, 61)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "Nombre:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(5, 31)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 13)
        Me.Label16.TabIndex = 26
        Me.Label16.Text = "Cuenta cargo:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtNifCargo
        '
        Me.TxtNifCargo.Location = New System.Drawing.Point(88, 132)
        Me.TxtNifCargo.Name = "TxtNifCargo"
        Me.TxtNifCargo.Size = New System.Drawing.Size(156, 20)
        Me.TxtNifCargo.TabIndex = 24
        '
        'TxtPoblacionCargo
        '
        Me.TxtPoblacionCargo.Location = New System.Drawing.Point(88, 106)
        Me.TxtPoblacionCargo.Name = "TxtPoblacionCargo"
        Me.TxtPoblacionCargo.Size = New System.Drawing.Size(299, 20)
        Me.TxtPoblacionCargo.TabIndex = 23
        '
        'TxtDomicilioCargo
        '
        Me.TxtDomicilioCargo.Location = New System.Drawing.Point(88, 80)
        Me.TxtDomicilioCargo.Name = "TxtDomicilioCargo"
        Me.TxtDomicilioCargo.Size = New System.Drawing.Size(299, 20)
        Me.TxtDomicilioCargo.TabIndex = 22
        '
        'TxtNombreCargo
        '
        Me.TxtNombreCargo.Location = New System.Drawing.Point(88, 54)
        Me.TxtNombreCargo.Name = "TxtNombreCargo"
        Me.TxtNombreCargo.Size = New System.Drawing.Size(299, 20)
        Me.TxtNombreCargo.TabIndex = 21
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtCodCuenta1)
        Me.Panel1.Controls.Add(Me.TxtDC1)
        Me.Panel1.Controls.Add(Me.TxtOficina1)
        Me.Panel1.Controls.Add(Me.TxtCuenta1)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.TxtEntidad1)
        Me.Panel1.Controls.Add(Me.TxtNifAbono)
        Me.Panel1.Controls.Add(Me.TxtPoblacionAbono)
        Me.Panel1.Controls.Add(Me.TxtDomicilioAbono)
        Me.Panel1.Controls.Add(Me.TxtNombreAbono)
        Me.Panel1.Location = New System.Drawing.Point(6, 79)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(395, 186)
        Me.Panel1.TabIndex = 1
        '
        'TxtCodCuenta1
        '
        Me.TxtCodCuenta1.Location = New System.Drawing.Point(227, 158)
        Me.TxtCodCuenta1.Name = "TxtCodCuenta1"
        Me.TxtCodCuenta1.Size = New System.Drawing.Size(120, 20)
        Me.TxtCodCuenta1.TabIndex = 23
        '
        'TxtDC1
        '
        Me.TxtDC1.Location = New System.Drawing.Point(192, 158)
        Me.TxtDC1.Name = "TxtDC1"
        Me.TxtDC1.Size = New System.Drawing.Size(29, 20)
        Me.TxtDC1.TabIndex = 22
        '
        'TxtOficina1
        '
        Me.TxtOficina1.Location = New System.Drawing.Point(143, 158)
        Me.TxtOficina1.Name = "TxtOficina1"
        Me.TxtOficina1.Size = New System.Drawing.Size(43, 20)
        Me.TxtOficina1.TabIndex = 21
        '
        'TxtCuenta1
        '
        Me.TxtCuenta1.FormattingEnabled = True
        Me.TxtCuenta1.Location = New System.Drawing.Point(94, 25)
        Me.TxtCuenta1.Name = "TxtCuenta1"
        Me.TxtCuenta1.Size = New System.Drawing.Size(282, 21)
        Me.TxtCuenta1.TabIndex = 20
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(8, 6)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 16)
        Me.Label17.TabIndex = 19
        Me.Label17.Text = "Abonos:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(45, 160)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "SEPA:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(56, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "NIF:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(26, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Población:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Domicilio:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Nombre:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Cuenta abono:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtEntidad1
        '
        Me.TxtEntidad1.Location = New System.Drawing.Point(94, 158)
        Me.TxtEntidad1.Name = "TxtEntidad1"
        Me.TxtEntidad1.Size = New System.Drawing.Size(43, 20)
        Me.TxtEntidad1.TabIndex = 7
        '
        'TxtNifAbono
        '
        Me.TxtNifAbono.Location = New System.Drawing.Point(94, 131)
        Me.TxtNifAbono.Name = "TxtNifAbono"
        Me.TxtNifAbono.Size = New System.Drawing.Size(156, 20)
        Me.TxtNifAbono.TabIndex = 6
        '
        'TxtPoblacionAbono
        '
        Me.TxtPoblacionAbono.Location = New System.Drawing.Point(94, 105)
        Me.TxtPoblacionAbono.Name = "TxtPoblacionAbono"
        Me.TxtPoblacionAbono.Size = New System.Drawing.Size(280, 20)
        Me.TxtPoblacionAbono.TabIndex = 5
        '
        'TxtDomicilioAbono
        '
        Me.TxtDomicilioAbono.Location = New System.Drawing.Point(94, 79)
        Me.TxtDomicilioAbono.Name = "TxtDomicilioAbono"
        Me.TxtDomicilioAbono.Size = New System.Drawing.Size(280, 20)
        Me.TxtDomicilioAbono.TabIndex = 4
        '
        'TxtNombreAbono
        '
        Me.TxtNombreAbono.Location = New System.Drawing.Point(94, 53)
        Me.TxtNombreAbono.Name = "TxtNombreAbono"
        Me.TxtNombreAbono.Size = New System.Drawing.Size(280, 20)
        Me.TxtNombreAbono.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(155, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(480, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "GRABACION DE FICHERO DE ABONOS/CARGOS"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lvabonos)
        Me.TabPage2.Controls.Add(Me.TxtTotalAbonos)
        Me.TabPage2.Controls.Add(Me.Label20)
        Me.TabPage2.Controls.Add(Me.Label19)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(847, 430)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Abonos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lvabonos
        '
        Me.lvabonos.HideSelection = False
        Me.lvabonos.Location = New System.Drawing.Point(19, 34)
        Me.lvabonos.Name = "lvabonos"
        Me.lvabonos.Size = New System.Drawing.Size(803, 350)
        Me.lvabonos.TabIndex = 22
        Me.lvabonos.UseCompatibleStateImageBehavior = False
        Me.lvabonos.View = System.Windows.Forms.View.Details
        '
        'TxtTotalAbonos
        '
        Me.TxtTotalAbonos.Location = New System.Drawing.Point(722, 390)
        Me.TxtTotalAbonos.Name = "TxtTotalAbonos"
        Me.TxtTotalAbonos.Size = New System.Drawing.Size(100, 20)
        Me.TxtTotalAbonos.TabIndex = 23
        Me.TxtTotalAbonos.Text = "0.00"
        Me.TxtTotalAbonos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(596, 394)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(119, 16)
        Me.Label20.TabIndex = 21
        Me.Label20.Text = "Importe abonos:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(16, 15)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 16)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Abonos:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TxtTotalCargos)
        Me.TabPage3.Controls.Add(Me.LvCargos)
        Me.TabPage3.Controls.Add(Me.Label21)
        Me.TabPage3.Controls.Add(Me.Label22)
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(847, 430)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Cargos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TxtTotalCargos
        '
        Me.TxtTotalCargos.Location = New System.Drawing.Point(726, 393)
        Me.TxtTotalCargos.Name = "TxtTotalCargos"
        Me.TxtTotalCargos.Size = New System.Drawing.Size(100, 20)
        Me.TxtTotalCargos.TabIndex = 27
        Me.TxtTotalCargos.Text = "0.00"
        Me.TxtTotalCargos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LvCargos
        '
        Me.LvCargos.HideSelection = False
        Me.LvCargos.Location = New System.Drawing.Point(23, 37)
        Me.LvCargos.Name = "LvCargos"
        Me.LvCargos.Size = New System.Drawing.Size(803, 350)
        Me.LvCargos.TabIndex = 26
        Me.LvCargos.UseCompatibleStateImageBehavior = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(600, 397)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(115, 16)
        Me.Label21.TabIndex = 25
        Me.Label21.Text = "Importe cargos:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(20, 18)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(61, 16)
        Me.Label22.TabIndex = 24
        Me.Label22.Text = "Cargos:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmCargosAbonosLLIRP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 477)
        Me.Controls.Add(Me.TabPrincipal)
        Me.Name = "FrmCargosAbonosLLIRP"
        Me.Text = "FrmCargosAbonosLLIRP"
        Me.TabPrincipal.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPrincipal As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents pb As ProgressBar
    Friend WithEvents DTFechaEmision As DateTimePicker
    Friend WithEvents DTFechaEnvio As DateTimePicker
    Friend WithEvents CmdBorraRemesa As Button
    Friend WithEvents CmdImprimirAbonos As Button
    Friend WithEvents CmdGenerarsepa As Button
    Friend WithEvents CmdImprimirCargos As Button
    Friend WithEvents CmdSeleccionRemesa As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TxtCuantosCargos As Label
    Friend WithEvents TxtCuantosAbonos As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents CmdGuardarCargo As Button
    Friend WithEvents TxtFicheroCargos As TextBox
    Friend WithEvents CmdGuardarAbono As Button
    Friend WithEvents TxtFicheroAbonos As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents TxtNifCargo As TextBox
    Friend WithEvents TxtPoblacionCargo As TextBox
    Friend WithEvents TxtDomicilioCargo As TextBox
    Friend WithEvents TxtNombreCargo As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtEntidad1 As TextBox
    Friend WithEvents TxtNifAbono As TextBox
    Friend WithEvents TxtPoblacionAbono As TextBox
    Friend WithEvents TxtDomicilioAbono As TextBox
    Friend WithEvents TxtNombreAbono As TextBox
    Friend WithEvents TxtTotalAbonos As TextBox
    Friend WithEvents lvabonos As ListView
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents TxtTotalCargos As TextBox
    Friend WithEvents LvCargos As ListView
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents TxtCuenta2 As ComboBox
    Friend WithEvents TxtCuenta1 As ComboBox
    Friend WithEvents lblProgreso As Label
    Friend WithEvents TxtCodCuenta1 As TextBox
    Friend WithEvents TxtDC1 As TextBox
    Friend WithEvents TxtOficina1 As TextBox
    Friend WithEvents TxtCodCuenta2 As TextBox
    Friend WithEvents TxtDC2 As TextBox
    Friend WithEvents TxtOficina2 As TextBox
    Friend WithEvents TxtEntidad2 As TextBox
End Class
