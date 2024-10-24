<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEdicionFicha
    Inherits System.Windows.Forms.Form

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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtEmpresa = New System.Windows.Forms.TextBox()
        Me.TxtEjercicio = New System.Windows.Forms.TextBox()
        Me.lblNombreEmpresa = New System.Windows.Forms.Label()
        Me.LblNombreEjercicio = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtCopias = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblHastaNombresocio = New System.Windows.Forms.Label()
        Me.lblNombreSocio = New System.Windows.Forms.Label()
        Me.TxtPlantilla = New System.Windows.Forms.TextBox()
        Me.TxtHastaSocio = New System.Windows.Forms.TextBox()
        Me.TxtCodigoSocio = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.frCartel = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FrmDatos = New System.Windows.Forms.GroupBox()
        Me.ChIncluirCamposBaja = New System.Windows.Forms.CheckBox()
        Me.ChSoloCamposCitricos = New System.Windows.Forms.CheckBox()
        Me.ChkAlta = New System.Windows.Forms.CheckBox()
        Me.FrmTiposFicha = New System.Windows.Forms.GroupBox()
        Me.OpcFichaSeguro = New System.Windows.Forms.RadioButton()
        Me.OpcDeclaracion = New System.Windows.Forms.RadioButton()
        Me.OpcNormal = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.OpcCS = New System.Windows.Forms.RadioButton()
        Me.OpcCA = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PrbProgreso = New System.Windows.Forms.ProgressBar()
        Me.pantalla = New System.Windows.Forms.RadioButton()
        Me.Impresora = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CmdBuscarPlantilla = New System.Windows.Forms.Button()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.frCartel.SuspendLayout()
        Me.FrmDatos.SuspendLayout()
        Me.FrmTiposFicha.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.CmdSalir)
        Me.Panel1.Controls.Add(Me.CmdCancelar)
        Me.Panel1.Controls.Add(Me.CmdImprimir)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(1, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(591, 44)
        Me.Panel1.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(258, 31)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Edición ficha socio"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Empresa:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(42, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Ejercicio:"
        '
        'TxtEmpresa
        '
        Me.TxtEmpresa.Location = New System.Drawing.Point(99, 56)
        Me.TxtEmpresa.Name = "TxtEmpresa"
        Me.TxtEmpresa.Size = New System.Drawing.Size(100, 20)
        Me.TxtEmpresa.TabIndex = 3
        '
        'TxtEjercicio
        '
        Me.TxtEjercicio.Location = New System.Drawing.Point(99, 82)
        Me.TxtEjercicio.Name = "TxtEjercicio"
        Me.TxtEjercicio.Size = New System.Drawing.Size(100, 20)
        Me.TxtEjercicio.TabIndex = 4
        '
        'lblNombreEmpresa
        '
        Me.lblNombreEmpresa.AutoSize = True
        Me.lblNombreEmpresa.Location = New System.Drawing.Point(205, 59)
        Me.lblNombreEmpresa.Name = "lblNombreEmpresa"
        Me.lblNombreEmpresa.Size = New System.Drawing.Size(10, 13)
        Me.lblNombreEmpresa.TabIndex = 5
        Me.lblNombreEmpresa.Text = "-"
        '
        'LblNombreEjercicio
        '
        Me.LblNombreEjercicio.AutoSize = True
        Me.LblNombreEjercicio.Location = New System.Drawing.Point(205, 85)
        Me.LblNombreEjercicio.Name = "LblNombreEjercicio"
        Me.LblNombreEjercicio.Size = New System.Drawing.Size(10, 13)
        Me.LblNombreEjercicio.TabIndex = 6
        Me.LblNombreEjercicio.Text = "-"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmdBuscarPlantilla)
        Me.GroupBox1.Controls.Add(Me.TxtCopias)
        Me.GroupBox1.Controls.Add(Me.frCartel)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.lblHastaNombresocio)
        Me.GroupBox1.Controls.Add(Me.lblNombreSocio)
        Me.GroupBox1.Controls.Add(Me.TxtPlantilla)
        Me.GroupBox1.Controls.Add(Me.TxtHastaSocio)
        Me.GroupBox1.Controls.Add(Me.TxtCodigoSocio)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 117)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(581, 136)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Formato ficha"
        '
        'TxtCopias
        '
        Me.TxtCopias.Location = New System.Drawing.Point(113, 108)
        Me.TxtCopias.Name = "TxtCopias"
        Me.TxtCopias.Size = New System.Drawing.Size(42, 20)
        Me.TxtCopias.TabIndex = 17
        Me.TxtCopias.Text = "1"
        Me.TxtCopias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(64, 111)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Copias:"
        '
        'lblHastaNombresocio
        '
        Me.lblHastaNombresocio.AutoSize = True
        Me.lblHastaNombresocio.Location = New System.Drawing.Point(219, 46)
        Me.lblHastaNombresocio.Name = "lblHastaNombresocio"
        Me.lblHastaNombresocio.Size = New System.Drawing.Size(10, 13)
        Me.lblHastaNombresocio.TabIndex = 15
        Me.lblHastaNombresocio.Text = "-"
        '
        'lblNombreSocio
        '
        Me.lblNombreSocio.AutoSize = True
        Me.lblNombreSocio.Location = New System.Drawing.Point(219, 21)
        Me.lblNombreSocio.Name = "lblNombreSocio"
        Me.lblNombreSocio.Size = New System.Drawing.Size(10, 13)
        Me.lblNombreSocio.TabIndex = 14
        Me.lblNombreSocio.Text = "-"
        '
        'TxtPlantilla
        '
        Me.TxtPlantilla.Location = New System.Drawing.Point(113, 78)
        Me.TxtPlantilla.Name = "TxtPlantilla"
        Me.TxtPlantilla.Size = New System.Drawing.Size(426, 20)
        Me.TxtPlantilla.TabIndex = 7
        '
        'TxtHastaSocio
        '
        Me.TxtHastaSocio.Location = New System.Drawing.Point(113, 48)
        Me.TxtHastaSocio.Name = "TxtHastaSocio"
        Me.TxtHastaSocio.Size = New System.Drawing.Size(100, 20)
        Me.TxtHastaSocio.TabIndex = 6
        '
        'TxtCodigoSocio
        '
        Me.TxtCodigoSocio.Location = New System.Drawing.Point(113, 18)
        Me.TxtCodigoSocio.Name = "TxtCodigoSocio"
        Me.TxtCodigoSocio.Size = New System.Drawing.Size(100, 20)
        Me.TxtCodigoSocio.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(60, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Plantilla:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Hasta el socio:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Desde el socio:"
        '
        'frCartel
        '
        Me.frCartel.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.frCartel.Controls.Add(Me.Label8)
        Me.frCartel.Controls.Add(Me.Label7)
        Me.frCartel.Controls.Add(Me.Button1)
        Me.frCartel.Location = New System.Drawing.Point(118, 69)
        Me.frCartel.Name = "frCartel"
        Me.frCartel.Size = New System.Drawing.Size(292, 59)
        Me.frCartel.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(65, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(219, 16)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Imprimiendo la ficha solicitada"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(118, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 16)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Un momento"
        '
        'FrmDatos
        '
        Me.FrmDatos.Controls.Add(Me.ChIncluirCamposBaja)
        Me.FrmDatos.Controls.Add(Me.ChSoloCamposCitricos)
        Me.FrmDatos.Controls.Add(Me.ChkAlta)
        Me.FrmDatos.Location = New System.Drawing.Point(12, 259)
        Me.FrmDatos.Name = "FrmDatos"
        Me.FrmDatos.Size = New System.Drawing.Size(264, 100)
        Me.FrmDatos.TabIndex = 8
        Me.FrmDatos.TabStop = False
        Me.FrmDatos.Text = "Opciones"
        '
        'ChIncluirCamposBaja
        '
        Me.ChIncluirCamposBaja.AutoSize = True
        Me.ChIncluirCamposBaja.Location = New System.Drawing.Point(19, 66)
        Me.ChIncluirCamposBaja.Name = "ChIncluirCamposBaja"
        Me.ChIncluirCamposBaja.Size = New System.Drawing.Size(203, 17)
        Me.ChIncluirCamposBaja.TabIndex = 2
        Me.ChIncluirCamposBaja.Text = "Incluir campos con fecha de baja hoy"
        Me.ChIncluirCamposBaja.UseVisualStyleBackColor = True
        '
        'ChSoloCamposCitricos
        '
        Me.ChSoloCamposCitricos.AutoSize = True
        Me.ChSoloCamposCitricos.Location = New System.Drawing.Point(19, 43)
        Me.ChSoloCamposCitricos.Name = "ChSoloCamposCitricos"
        Me.ChSoloCamposCitricos.Size = New System.Drawing.Size(194, 17)
        Me.ChSoloCamposCitricos.TabIndex = 1
        Me.ChSoloCamposCitricos.Text = "Sólo socios con campos de cítricos"
        Me.ChSoloCamposCitricos.UseVisualStyleBackColor = True
        '
        'ChkAlta
        '
        Me.ChkAlta.AutoSize = True
        Me.ChkAlta.Checked = True
        Me.ChkAlta.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkAlta.Location = New System.Drawing.Point(19, 20)
        Me.ChkAlta.Name = "ChkAlta"
        Me.ChkAlta.Size = New System.Drawing.Size(115, 17)
        Me.ChkAlta.TabIndex = 0
        Me.ChkAlta.Text = "Sólo socios de alta"
        Me.ChkAlta.UseVisualStyleBackColor = True
        '
        'FrmTiposFicha
        '
        Me.FrmTiposFicha.Controls.Add(Me.OpcFichaSeguro)
        Me.FrmTiposFicha.Controls.Add(Me.OpcDeclaracion)
        Me.FrmTiposFicha.Controls.Add(Me.OpcNormal)
        Me.FrmTiposFicha.Location = New System.Drawing.Point(282, 259)
        Me.FrmTiposFicha.Name = "FrmTiposFicha"
        Me.FrmTiposFicha.Size = New System.Drawing.Size(300, 100)
        Me.FrmTiposFicha.TabIndex = 9
        Me.FrmTiposFicha.TabStop = False
        Me.FrmTiposFicha.Text = "Formato"
        '
        'OpcFichaSeguro
        '
        Me.OpcFichaSeguro.AutoSize = True
        Me.OpcFichaSeguro.Location = New System.Drawing.Point(26, 66)
        Me.OpcFichaSeguro.Name = "OpcFichaSeguro"
        Me.OpcFichaSeguro.Size = New System.Drawing.Size(86, 17)
        Me.OpcFichaSeguro.TabIndex = 2
        Me.OpcFichaSeguro.TabStop = True
        Me.OpcFichaSeguro.Text = "Ficha seguro"
        Me.OpcFichaSeguro.UseVisualStyleBackColor = True
        '
        'OpcDeclaracion
        '
        Me.OpcDeclaracion.AutoSize = True
        Me.OpcDeclaracion.Location = New System.Drawing.Point(26, 43)
        Me.OpcDeclaracion.Name = "OpcDeclaracion"
        Me.OpcDeclaracion.Size = New System.Drawing.Size(109, 17)
        Me.OpcDeclaracion.TabIndex = 1
        Me.OpcDeclaracion.TabStop = True
        Me.OpcDeclaracion.Text = "Ficha declaración"
        Me.OpcDeclaracion.UseVisualStyleBackColor = True
        '
        'OpcNormal
        '
        Me.OpcNormal.AutoSize = True
        Me.OpcNormal.Checked = True
        Me.OpcNormal.Location = New System.Drawing.Point(26, 20)
        Me.OpcNormal.Name = "OpcNormal"
        Me.OpcNormal.Size = New System.Drawing.Size(79, 17)
        Me.OpcNormal.TabIndex = 0
        Me.OpcNormal.TabStop = True
        Me.OpcNormal.Text = "Ficha socio"
        Me.OpcNormal.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OpcCS)
        Me.GroupBox2.Controls.Add(Me.OpcCA)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 366)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(569, 40)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        '
        'OpcCS
        '
        Me.OpcCS.AutoSize = True
        Me.OpcCS.Location = New System.Drawing.Point(283, 17)
        Me.OpcCS.Name = "OpcCS"
        Me.OpcCS.Size = New System.Drawing.Size(115, 17)
        Me.OpcCS.TabIndex = 2
        Me.OpcCS.TabStop = True
        Me.OpcCS.Text = "Campaña siguiente"
        Me.OpcCS.UseVisualStyleBackColor = True
        '
        'OpcCA
        '
        Me.OpcCA.AutoSize = True
        Me.OpcCA.Checked = True
        Me.OpcCA.Location = New System.Drawing.Point(19, 17)
        Me.OpcCA.Name = "OpcCA"
        Me.OpcCA.Size = New System.Drawing.Size(102, 17)
        Me.OpcCA.TabIndex = 1
        Me.OpcCA.TabStop = True
        Me.OpcCA.Text = "Campaña actual"
        Me.OpcCA.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.pantalla)
        Me.GroupBox4.Controls.Add(Me.Impresora)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 406)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(569, 66)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        '
        'PrbProgreso
        '
        Me.PrbProgreso.Location = New System.Drawing.Point(14, 478)
        Me.PrbProgreso.Name = "PrbProgreso"
        Me.PrbProgreso.Size = New System.Drawing.Size(568, 11)
        Me.PrbProgreso.TabIndex = 12
        '
        'pantalla
        '
        Me.pantalla.Appearance = System.Windows.Forms.Appearance.Button
        Me.pantalla.BackgroundImage = Global.Proysocios.My.Resources.Resources.pantalla_del_monitor
        Me.pantalla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pantalla.Location = New System.Drawing.Point(411, 14)
        Me.pantalla.Name = "pantalla"
        Me.pantalla.Size = New System.Drawing.Size(47, 46)
        Me.pantalla.TabIndex = 3
        Me.pantalla.UseVisualStyleBackColor = True
        '
        'Impresora
        '
        Me.Impresora.BackgroundImage = Global.Proysocios.My.Resources.Resources.impresora_ico
        Me.Impresora.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Impresora.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Impresora.Checked = True
        Me.Impresora.Location = New System.Drawing.Point(48, 13)
        Me.Impresora.Name = "Impresora"
        Me.Impresora.Size = New System.Drawing.Size(53, 41)
        Me.Impresora.TabIndex = 2
        Me.Impresora.TabStop = True
        Me.Impresora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Impresora.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.Proysocios.My.Resources.Resources.palabra
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(15, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(44, 42)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CmdBuscarPlantilla
        '
        Me.CmdBuscarPlantilla.BackgroundImage = Global.Proysocios.My.Resources.Resources.buscararchivo
        Me.CmdBuscarPlantilla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBuscarPlantilla.Location = New System.Drawing.Point(538, 76)
        Me.CmdBuscarPlantilla.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarPlantilla.Name = "CmdBuscarPlantilla"
        Me.CmdBuscarPlantilla.Size = New System.Drawing.Size(22, 22)
        Me.CmdBuscarPlantilla.TabIndex = 18
        Me.CmdBuscarPlantilla.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = Global.Proysocios.My.Resources.Resources.salir1
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(525, 0)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(56, 44)
        Me.CmdSalir.TabIndex = 5
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackgroundImage = Global.Proysocios.My.Resources.Resources.editar1
        Me.CmdCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdCancelar.Location = New System.Drawing.Point(426, 1)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.Size = New System.Drawing.Size(44, 44)
        Me.CmdCancelar.TabIndex = 4
        Me.CmdCancelar.UseVisualStyleBackColor = True
        '
        'CmdImprimir
        '
        Me.CmdImprimir.BackgroundImage = Global.Proysocios.My.Resources.Resources.impresora_ico
        Me.CmdImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdImprimir.Location = New System.Drawing.Point(355, 2)
        Me.CmdImprimir.Name = "CmdImprimir"
        Me.CmdImprimir.Size = New System.Drawing.Size(55, 41)
        Me.CmdImprimir.TabIndex = 3
        Me.CmdImprimir.UseVisualStyleBackColor = True
        '
        'FrmEdicionFicha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 494)
        Me.Controls.Add(Me.PrbProgreso)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.FrmTiposFicha)
        Me.Controls.Add(Me.FrmDatos)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LblNombreEjercicio)
        Me.Controls.Add(Me.lblNombreEmpresa)
        Me.Controls.Add(Me.TxtEjercicio)
        Me.Controls.Add(Me.TxtEmpresa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmEdicionFicha"
        Me.Text = "Edición ficha socios"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.frCartel.ResumeLayout(False)
        Me.frCartel.PerformLayout()
        Me.FrmDatos.ResumeLayout(False)
        Me.FrmDatos.PerformLayout()
        Me.FrmTiposFicha.ResumeLayout(False)
        Me.FrmTiposFicha.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdImprimir As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtEmpresa As TextBox
    Friend WithEvents TxtEjercicio As TextBox
    Friend WithEvents lblNombreEmpresa As Label
    Friend WithEvents LblNombreEjercicio As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TxtPlantilla As TextBox
    Friend WithEvents TxtHastaSocio As TextBox
    Friend WithEvents TxtCodigoSocio As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents FrmDatos As GroupBox
    Friend WithEvents ChIncluirCamposBaja As CheckBox
    Friend WithEvents ChSoloCamposCitricos As CheckBox
    Friend WithEvents ChkAlta As CheckBox
    Friend WithEvents FrmTiposFicha As GroupBox
    Friend WithEvents OpcFichaSeguro As RadioButton
    Friend WithEvents OpcDeclaracion As RadioButton
    Friend WithEvents OpcNormal As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents OpcCS As RadioButton
    Friend WithEvents OpcCA As RadioButton
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents pantalla As RadioButton
    Friend WithEvents Impresora As RadioButton
    Friend WithEvents PrbProgreso As ProgressBar
    Friend WithEvents frCartel As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblHastaNombresocio As Label
    Friend WithEvents lblNombreSocio As Label
    Friend WithEvents TxtCopias As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CmdBuscarPlantilla As Button
End Class
