<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBalSituacion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBalSituacion))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.OptTodas2b = New System.Windows.Forms.RadioButton()
        Me.OptGrabadas2b = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.Cancelar = New System.Windows.Forms.Button()
        Me.OptTodas2a = New System.Windows.Forms.RadioButton()
        Me.OptConMov2a = New System.Windows.Forms.RadioButton()
        Me.OptConSaldo2a = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ChkDigitos1 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos2 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos3 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos4 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos5 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos6 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos7 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos8 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos9 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos10 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos11 = New System.Windows.Forms.CheckBox()
        Me.ChkDigitos12 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.OptTodas1b = New System.Windows.Forms.RadioButton()
        Me.OptGrabadas1b = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.OptTodas1a = New System.Windows.Forms.RadioButton()
        Me.OptConMov1a = New System.Windows.Forms.RadioButton()
        Me.OptConSaldo1a = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtHastacuenta = New System.Windows.Forms.TextBox()
        Me.TxtDesdecuenta = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtDigitos = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.TxtPeriodofin = New System.Windows.Forms.ComboBox()
        Me.TxtPeriodofinorigen = New System.Windows.Forms.ComboBox()
        Me.TxtPeriodoInicio = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.LblDiariosseleccionados = New System.Windows.Forms.Label()
        Me.TxtHastadiario = New System.Windows.Forms.ComboBox()
        Me.TxtDesdediario = New System.Windows.Forms.ComboBox()
        Me.hasta = New System.Windows.Forms.Label()
        Me.Desde = New System.Windows.Forms.Label()
        Me.TxtSelecciondiarios = New System.Windows.Forms.ComboBox()
        Me.BtExcel = New System.Windows.Forms.Button()
        Me.BtImprimir = New System.Windows.Forms.Button()
        Me.btVista = New System.Windows.Forms.Button()
        Me.BtPrevisualizar = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.TxtFecha_balance = New Textboxbotoncontrol.TextBoxCalendar()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.TxtFecha_balance)
        Me.GroupBox1.Controls.Add(Me.TxtDigitos)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(616, 303)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.GroupBox7)
        Me.GroupBox5.Controls.Add(Me.GroupBox6)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos1)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos2)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos3)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos4)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos5)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos6)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos7)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos8)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos9)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos10)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos11)
        Me.GroupBox5.Controls.Add(Me.ChkDigitos12)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Location = New System.Drawing.Point(19, 181)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(575, 112)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 16)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Incluir:"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.OptTodas2b)
        Me.GroupBox7.Controls.Add(Me.OptGrabadas2b)
        Me.GroupBox7.Location = New System.Drawing.Point(347, 61)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(207, 42)
        Me.GroupBox7.TabIndex = 6
        Me.GroupBox7.TabStop = False
        '
        'OptTodas2b
        '
        Me.OptTodas2b.AutoSize = True
        Me.OptTodas2b.Checked = True
        Me.OptTodas2b.Location = New System.Drawing.Point(128, 19)
        Me.OptTodas2b.Name = "OptTodas2b"
        Me.OptTodas2b.Size = New System.Drawing.Size(55, 17)
        Me.OptTodas2b.TabIndex = 42
        Me.OptTodas2b.TabStop = True
        Me.OptTodas2b.Text = "Todas"
        Me.OptTodas2b.UseVisualStyleBackColor = True
        '
        'OptGrabadas2b
        '
        Me.OptGrabadas2b.AutoSize = True
        Me.OptGrabadas2b.Location = New System.Drawing.Point(15, 19)
        Me.OptGrabadas2b.Name = "OptGrabadas2b"
        Me.OptGrabadas2b.Size = New System.Drawing.Size(71, 17)
        Me.OptGrabadas2b.TabIndex = 41
        Me.OptGrabadas2b.Text = "Grabadas"
        Me.OptGrabadas2b.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Panel1)
        Me.GroupBox6.Controls.Add(Me.OptTodas2a)
        Me.GroupBox6.Controls.Add(Me.OptConMov2a)
        Me.GroupBox6.Controls.Add(Me.OptConSaldo2a)
        Me.GroupBox6.Location = New System.Drawing.Point(16, 61)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(316, 42)
        Me.GroupBox6.TabIndex = 5
        Me.GroupBox6.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.pb)
        Me.Panel1.Controls.Add(Me.Cancelar)
        Me.Panel1.Location = New System.Drawing.Point(93, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(363, 85)
        Me.Panel1.TabIndex = 11
        Me.Panel1.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(131, 11)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(101, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Calculando balance"
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(2, 27)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(357, 16)
        Me.pb.TabIndex = 1
        '
        'Cancelar
        '
        Me.Cancelar.Location = New System.Drawing.Point(142, 51)
        Me.Cancelar.Name = "Cancelar"
        Me.Cancelar.Size = New System.Drawing.Size(75, 23)
        Me.Cancelar.TabIndex = 0
        Me.Cancelar.Text = "Cancelar"
        Me.Cancelar.UseVisualStyleBackColor = True
        '
        'OptTodas2a
        '
        Me.OptTodas2a.AutoSize = True
        Me.OptTodas2a.Checked = True
        Me.OptTodas2a.Location = New System.Drawing.Point(241, 19)
        Me.OptTodas2a.Name = "OptTodas2a"
        Me.OptTodas2a.Size = New System.Drawing.Size(55, 17)
        Me.OptTodas2a.TabIndex = 40
        Me.OptTodas2a.TabStop = True
        Me.OptTodas2a.Text = "Todas"
        Me.OptTodas2a.UseVisualStyleBackColor = True
        '
        'OptConMov2a
        '
        Me.OptConMov2a.AutoSize = True
        Me.OptConMov2a.Location = New System.Drawing.Point(108, 19)
        Me.OptConMov2a.Name = "OptConMov2a"
        Me.OptConMov2a.Size = New System.Drawing.Size(96, 17)
        Me.OptConMov2a.TabIndex = 39
        Me.OptConMov2a.Text = "C/Movimientos"
        Me.OptConMov2a.UseVisualStyleBackColor = True
        '
        'OptConSaldo2a
        '
        Me.OptConSaldo2a.AutoSize = True
        Me.OptConSaldo2a.Location = New System.Drawing.Point(7, 19)
        Me.OptConSaldo2a.Name = "OptConSaldo2a"
        Me.OptConSaldo2a.Size = New System.Drawing.Size(64, 17)
        Me.OptConSaldo2a.TabIndex = 38
        Me.OptConSaldo2a.Text = "C/Saldo"
        Me.OptConSaldo2a.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(133, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(382, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = " 1         2         3          4        5          6         7        8         " &
    "9       10       11        12"
        '
        'ChkDigitos1
        '
        Me.ChkDigitos1.AutoSize = True
        Me.ChkDigitos1.Location = New System.Drawing.Point(136, 27)
        Me.ChkDigitos1.Name = "ChkDigitos1"
        Me.ChkDigitos1.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos1.TabIndex = 26
        Me.ChkDigitos1.UseVisualStyleBackColor = True
        '
        'ChkDigitos2
        '
        Me.ChkDigitos2.AutoSize = True
        Me.ChkDigitos2.Location = New System.Drawing.Point(169, 27)
        Me.ChkDigitos2.Name = "ChkDigitos2"
        Me.ChkDigitos2.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos2.TabIndex = 27
        Me.ChkDigitos2.UseVisualStyleBackColor = True
        '
        'ChkDigitos3
        '
        Me.ChkDigitos3.AutoSize = True
        Me.ChkDigitos3.Location = New System.Drawing.Point(202, 27)
        Me.ChkDigitos3.Name = "ChkDigitos3"
        Me.ChkDigitos3.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos3.TabIndex = 28
        Me.ChkDigitos3.UseVisualStyleBackColor = True
        '
        'ChkDigitos4
        '
        Me.ChkDigitos4.AutoSize = True
        Me.ChkDigitos4.Location = New System.Drawing.Point(235, 27)
        Me.ChkDigitos4.Name = "ChkDigitos4"
        Me.ChkDigitos4.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos4.TabIndex = 29
        Me.ChkDigitos4.UseVisualStyleBackColor = True
        '
        'ChkDigitos5
        '
        Me.ChkDigitos5.AutoSize = True
        Me.ChkDigitos5.Location = New System.Drawing.Point(268, 27)
        Me.ChkDigitos5.Name = "ChkDigitos5"
        Me.ChkDigitos5.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos5.TabIndex = 30
        Me.ChkDigitos5.UseVisualStyleBackColor = True
        '
        'ChkDigitos6
        '
        Me.ChkDigitos6.AutoSize = True
        Me.ChkDigitos6.Location = New System.Drawing.Point(301, 27)
        Me.ChkDigitos6.Name = "ChkDigitos6"
        Me.ChkDigitos6.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos6.TabIndex = 31
        Me.ChkDigitos6.UseVisualStyleBackColor = True
        '
        'ChkDigitos7
        '
        Me.ChkDigitos7.AutoSize = True
        Me.ChkDigitos7.Location = New System.Drawing.Point(334, 27)
        Me.ChkDigitos7.Name = "ChkDigitos7"
        Me.ChkDigitos7.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos7.TabIndex = 32
        Me.ChkDigitos7.UseVisualStyleBackColor = True
        '
        'ChkDigitos8
        '
        Me.ChkDigitos8.AutoSize = True
        Me.ChkDigitos8.Location = New System.Drawing.Point(367, 27)
        Me.ChkDigitos8.Name = "ChkDigitos8"
        Me.ChkDigitos8.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos8.TabIndex = 33
        Me.ChkDigitos8.UseVisualStyleBackColor = True
        '
        'ChkDigitos9
        '
        Me.ChkDigitos9.AutoSize = True
        Me.ChkDigitos9.Location = New System.Drawing.Point(400, 27)
        Me.ChkDigitos9.Name = "ChkDigitos9"
        Me.ChkDigitos9.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos9.TabIndex = 34
        Me.ChkDigitos9.UseVisualStyleBackColor = True
        '
        'ChkDigitos10
        '
        Me.ChkDigitos10.AutoSize = True
        Me.ChkDigitos10.Location = New System.Drawing.Point(433, 27)
        Me.ChkDigitos10.Name = "ChkDigitos10"
        Me.ChkDigitos10.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos10.TabIndex = 35
        Me.ChkDigitos10.UseVisualStyleBackColor = True
        '
        'ChkDigitos11
        '
        Me.ChkDigitos11.AutoSize = True
        Me.ChkDigitos11.Location = New System.Drawing.Point(466, 27)
        Me.ChkDigitos11.Name = "ChkDigitos11"
        Me.ChkDigitos11.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos11.TabIndex = 36
        Me.ChkDigitos11.UseVisualStyleBackColor = True
        '
        'ChkDigitos12
        '
        Me.ChkDigitos12.AutoSize = True
        Me.ChkDigitos12.Location = New System.Drawing.Point(499, 27)
        Me.ChkDigitos12.Name = "ChkDigitos12"
        Me.ChkDigitos12.Size = New System.Drawing.Size(15, 14)
        Me.ChkDigitos12.TabIndex = 37
        Me.ChkDigitos12.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Otros niveles(dígitos)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TxtHastacuenta)
        Me.GroupBox2.Controls.Add(Me.TxtDesdecuenta)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 51)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(575, 124)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.OptTodas1b)
        Me.GroupBox4.Controls.Add(Me.OptGrabadas1b)
        Me.GroupBox4.Location = New System.Drawing.Point(347, 69)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(207, 42)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        '
        'OptTodas1b
        '
        Me.OptTodas1b.AutoSize = True
        Me.OptTodas1b.Checked = True
        Me.OptTodas1b.Location = New System.Drawing.Point(128, 19)
        Me.OptTodas1b.Name = "OptTodas1b"
        Me.OptTodas1b.Size = New System.Drawing.Size(55, 17)
        Me.OptTodas1b.TabIndex = 25
        Me.OptTodas1b.TabStop = True
        Me.OptTodas1b.Text = "Todas"
        Me.OptTodas1b.UseVisualStyleBackColor = True
        '
        'OptGrabadas1b
        '
        Me.OptGrabadas1b.AutoSize = True
        Me.OptGrabadas1b.Location = New System.Drawing.Point(15, 19)
        Me.OptGrabadas1b.Name = "OptGrabadas1b"
        Me.OptGrabadas1b.Size = New System.Drawing.Size(71, 17)
        Me.OptGrabadas1b.TabIndex = 24
        Me.OptGrabadas1b.Text = "Grabadas"
        Me.OptGrabadas1b.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.OptTodas1a)
        Me.GroupBox3.Controls.Add(Me.OptConMov1a)
        Me.GroupBox3.Controls.Add(Me.OptConSaldo1a)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 69)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(316, 42)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        '
        'OptTodas1a
        '
        Me.OptTodas1a.AutoSize = True
        Me.OptTodas1a.Checked = True
        Me.OptTodas1a.Location = New System.Drawing.Point(241, 19)
        Me.OptTodas1a.Name = "OptTodas1a"
        Me.OptTodas1a.Size = New System.Drawing.Size(55, 17)
        Me.OptTodas1a.TabIndex = 23
        Me.OptTodas1a.TabStop = True
        Me.OptTodas1a.Text = "Todas"
        Me.OptTodas1a.UseVisualStyleBackColor = True
        '
        'OptConMov1a
        '
        Me.OptConMov1a.AutoSize = True
        Me.OptConMov1a.Location = New System.Drawing.Point(108, 19)
        Me.OptConMov1a.Name = "OptConMov1a"
        Me.OptConMov1a.Size = New System.Drawing.Size(96, 17)
        Me.OptConMov1a.TabIndex = 22
        Me.OptConMov1a.Text = "C/Movimientos"
        Me.OptConMov1a.UseVisualStyleBackColor = True
        '
        'OptConSaldo1a
        '
        Me.OptConSaldo1a.AutoSize = True
        Me.OptConSaldo1a.Location = New System.Drawing.Point(7, 19)
        Me.OptConSaldo1a.Name = "OptConSaldo1a"
        Me.OptConSaldo1a.Size = New System.Drawing.Size(64, 17)
        Me.OptConSaldo1a.TabIndex = 21
        Me.OptConSaldo1a.Text = "C/Saldo"
        Me.OptConSaldo1a.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 16)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Incluir:"
        '
        'TxtHastacuenta
        '
        Me.TxtHastacuenta.Location = New System.Drawing.Point(351, 16)
        Me.TxtHastacuenta.Name = "TxtHastacuenta"
        Me.TxtHastacuenta.Size = New System.Drawing.Size(100, 20)
        Me.TxtHastacuenta.TabIndex = 4
        '
        'TxtDesdecuenta
        '
        Me.TxtDesdecuenta.Location = New System.Drawing.Point(100, 13)
        Me.TxtDesdecuenta.Name = "TxtDesdecuenta"
        Me.TxtDesdecuenta.Size = New System.Drawing.Size(100, 20)
        Me.TxtDesdecuenta.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(265, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Hasta cuenta"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Desde cuenta"
        '
        'TxtDigitos
        '
        Me.TxtDigitos.Location = New System.Drawing.Point(119, 25)
        Me.TxtDigitos.Name = "TxtDigitos"
        Me.TxtDigitos.Size = New System.Drawing.Size(100, 20)
        Me.TxtDigitos.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(275, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Fecha balance"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Dígitos balance"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.TxtPeriodofin)
        Me.GroupBox8.Controls.Add(Me.TxtPeriodofinorigen)
        Me.GroupBox8.Controls.Add(Me.TxtPeriodoInicio)
        Me.GroupBox8.Controls.Add(Me.Label11)
        Me.GroupBox8.Controls.Add(Me.Label10)
        Me.GroupBox8.Controls.Add(Me.Label9)
        Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.Location = New System.Drawing.Point(12, 321)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(616, 66)
        Me.GroupBox8.TabIndex = 7
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Selección de periodos"
        '
        'TxtPeriodofin
        '
        Me.TxtPeriodofin.FormattingEnabled = True
        Me.TxtPeriodofin.Location = New System.Drawing.Point(473, 25)
        Me.TxtPeriodofin.Name = "TxtPeriodofin"
        Me.TxtPeriodofin.Size = New System.Drawing.Size(137, 21)
        Me.TxtPeriodofin.TabIndex = 17
        '
        'TxtPeriodofinorigen
        '
        Me.TxtPeriodofinorigen.FormattingEnabled = True
        Me.TxtPeriodofinorigen.Location = New System.Drawing.Point(278, 25)
        Me.TxtPeriodofinorigen.Name = "TxtPeriodofinorigen"
        Me.TxtPeriodofinorigen.Size = New System.Drawing.Size(133, 21)
        Me.TxtPeriodofinorigen.TabIndex = 12
        '
        'TxtPeriodoInicio
        '
        Me.TxtPeriodoInicio.FormattingEnabled = True
        Me.TxtPeriodoInicio.Location = New System.Drawing.Point(42, 28)
        Me.TxtPeriodoInicio.Name = "TxtPeriodoInicio"
        Me.TxtPeriodoInicio.Size = New System.Drawing.Size(129, 21)
        Me.TxtPeriodoInicio.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(438, 28)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Final:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(204, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Último origen"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 28)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Inicio:"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.LblDiariosseleccionados)
        Me.GroupBox9.Controls.Add(Me.TxtHastadiario)
        Me.GroupBox9.Controls.Add(Me.TxtDesdediario)
        Me.GroupBox9.Controls.Add(Me.hasta)
        Me.GroupBox9.Controls.Add(Me.Desde)
        Me.GroupBox9.Controls.Add(Me.TxtSelecciondiarios)
        Me.GroupBox9.Location = New System.Drawing.Point(12, 406)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(616, 56)
        Me.GroupBox9.TabIndex = 8
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Selección diarios"
        '
        'LblDiariosseleccionados
        '
        Me.LblDiariosseleccionados.AutoSize = True
        Me.LblDiariosseleccionados.Location = New System.Drawing.Point(177, 22)
        Me.LblDiariosseleccionados.Name = "LblDiariosseleccionados"
        Me.LblDiariosseleccionados.Size = New System.Drawing.Size(10, 13)
        Me.LblDiariosseleccionados.TabIndex = 5
        Me.LblDiariosseleccionados.Text = "-"
        '
        'TxtHastadiario
        '
        Me.TxtHastadiario.FormattingEnabled = True
        Me.TxtHastadiario.Location = New System.Drawing.Point(494, 19)
        Me.TxtHastadiario.Name = "TxtHastadiario"
        Me.TxtHastadiario.Size = New System.Drawing.Size(98, 21)
        Me.TxtHastadiario.TabIndex = 16
        Me.TxtHastadiario.Visible = False
        '
        'TxtDesdediario
        '
        Me.TxtDesdediario.FormattingEnabled = True
        Me.TxtDesdediario.Location = New System.Drawing.Point(300, 19)
        Me.TxtDesdediario.Name = "TxtDesdediario"
        Me.TxtDesdediario.Size = New System.Drawing.Size(98, 21)
        Me.TxtDesdediario.TabIndex = 15
        Me.TxtDesdediario.Visible = False
        '
        'hasta
        '
        Me.hasta.AutoSize = True
        Me.hasta.Location = New System.Drawing.Point(454, 23)
        Me.hasta.Name = "hasta"
        Me.hasta.Size = New System.Drawing.Size(35, 13)
        Me.hasta.TabIndex = 2
        Me.hasta.Text = "Hasta"
        Me.hasta.Visible = False
        '
        'Desde
        '
        Me.Desde.AutoSize = True
        Me.Desde.Location = New System.Drawing.Point(256, 20)
        Me.Desde.Name = "Desde"
        Me.Desde.Size = New System.Drawing.Size(38, 13)
        Me.Desde.TabIndex = 1
        Me.Desde.Text = "Desde"
        Me.Desde.Visible = False
        '
        'TxtSelecciondiarios
        '
        Me.TxtSelecciondiarios.FormattingEnabled = True
        Me.TxtSelecciondiarios.Location = New System.Drawing.Point(21, 20)
        Me.TxtSelecciondiarios.Name = "TxtSelecciondiarios"
        Me.TxtSelecciondiarios.Size = New System.Drawing.Size(150, 21)
        Me.TxtSelecciondiarios.TabIndex = 14
        '
        'BtExcel
        '
        Me.BtExcel.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.excel1
        Me.BtExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtExcel.Location = New System.Drawing.Point(506, 480)
        Me.BtExcel.Name = "BtExcel"
        Me.BtExcel.Size = New System.Drawing.Size(50, 50)
        Me.BtExcel.TabIndex = 9
        Me.BtExcel.UseVisualStyleBackColor = True
        '
        'BtImprimir
        '
        Me.BtImprimir.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.impresora_ico1
        Me.BtImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtImprimir.Location = New System.Drawing.Point(447, 480)
        Me.BtImprimir.Name = "BtImprimir"
        Me.BtImprimir.Size = New System.Drawing.Size(50, 50)
        Me.BtImprimir.TabIndex = 18
        Me.BtImprimir.UseVisualStyleBackColor = True
        '
        'btVista
        '
        Me.btVista.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.textview_view_search_find_110401
        Me.btVista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btVista.Location = New System.Drawing.Point(391, 480)
        Me.btVista.Name = "btVista"
        Me.btVista.Size = New System.Drawing.Size(50, 50)
        Me.btVista.TabIndex = 7
        Me.btVista.UseVisualStyleBackColor = True
        '
        'BtPrevisualizar
        '
        Me.BtPrevisualizar.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.vprevia1
        Me.BtPrevisualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtPrevisualizar.Location = New System.Drawing.Point(330, 480)
        Me.BtPrevisualizar.Name = "BtPrevisualizar"
        Me.BtPrevisualizar.Size = New System.Drawing.Size(50, 50)
        Me.BtPrevisualizar.TabIndex = 17
        Me.BtPrevisualizar.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.Location = New System.Drawing.Point(94, 480)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(50, 50)
        Me.Button2.TabIndex = 5
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(21, 480)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(50, 50)
        Me.Button1.TabIndex = 4
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.BackgroundImage = CType(resources.GetObject("BtSalir.BackgroundImage"), System.Drawing.Image)
        Me.BtSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSalir.Location = New System.Drawing.Point(572, 480)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(50, 50)
        Me.BtSalir.TabIndex = 19
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'TxtFecha_balance
        '
        Me.TxtFecha_balance.ButtonImage = Nothing
        Me.TxtFecha_balance.FormatoFecha = "dd/MM/yyyy"
        Me.TxtFecha_balance.Location = New System.Drawing.Point(370, 25)
        Me.TxtFecha_balance.Name = "TxtFecha_balance"
        Me.TxtFecha_balance.Size = New System.Drawing.Size(129, 20)
        Me.TxtFecha_balance.TabIndex = 2
        Me.TxtFecha_balance.Text = "01/01/2022"
        Me.TxtFecha_balance.value = New Date(2022, 1, 1, 0, 0, 0, 0)
        '
        'FrmBalSituacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 531)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtExcel)
        Me.Controls.Add(Me.BtImprimir)
        Me.Controls.Add(Me.btVista)
        Me.Controls.Add(Me.BtPrevisualizar)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.GroupBox8)
        Me.Name = "FrmBalSituacion"
        Me.Text = "Balance de situación"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents OptTodas2b As RadioButton
    Friend WithEvents OptGrabadas2b As RadioButton
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents OptTodas2a As RadioButton
    Friend WithEvents OptConMov2a As RadioButton
    Friend WithEvents OptConSaldo2a As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents ChkDigitos1 As CheckBox
    Friend WithEvents ChkDigitos2 As CheckBox
    Friend WithEvents ChkDigitos3 As CheckBox
    Friend WithEvents ChkDigitos4 As CheckBox
    Friend WithEvents ChkDigitos5 As CheckBox
    Friend WithEvents ChkDigitos6 As CheckBox
    Friend WithEvents ChkDigitos7 As CheckBox
    Friend WithEvents ChkDigitos8 As CheckBox
    Friend WithEvents ChkDigitos9 As CheckBox
    Friend WithEvents ChkDigitos10 As CheckBox
    Friend WithEvents ChkDigitos11 As CheckBox
    Friend WithEvents ChkDigitos12 As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents OptTodas1b As RadioButton
    Friend WithEvents OptGrabadas1b As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents OptTodas1a As RadioButton
    Friend WithEvents OptConMov1a As RadioButton
    Friend WithEvents OptConSaldo1a As RadioButton
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtHastacuenta As TextBox
    Friend WithEvents TxtDesdecuenta As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtFecha_balance As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents TxtDigitos As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents TxtPeriodofin As ComboBox
    Friend WithEvents TxtPeriodofinorigen As ComboBox
    Friend WithEvents TxtPeriodoInicio As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents hasta As Label
    Friend WithEvents Desde As Label
    Friend WithEvents TxtSelecciondiarios As ComboBox
    Friend WithEvents TxtHastadiario As ComboBox
    Friend WithEvents TxtDesdediario As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents BtPrevisualizar As Button
    Friend WithEvents btVista As Button
    Friend WithEvents BtImprimir As Button
    Friend WithEvents BtExcel As Button
    Friend WithEvents BtSalir As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents pb As ProgressBar
    Friend WithEvents Cancelar As Button
    Friend WithEvents LblDiariosseleccionados As Label
End Class
