<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEdicionContrato
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblNombreEjercicio = New System.Windows.Forms.Label()
        Me.lblNombreEmpresa = New System.Windows.Forms.Label()
        Me.TxtEjercicio = New System.Windows.Forms.TextBox()
        Me.TxtEmpresa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNombreSocio = New System.Windows.Forms.Label()
        Me.TxtCodigoSocio = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.frCartel = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtCopias = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmdBuscaFicha = New System.Windows.Forms.Button()
        Me.TxtPlantillaficha = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmdBuscarPlantilla = New System.Windows.Forms.Button()
        Me.TxtPlantilla = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtFirmadopor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtFechaEdicion = New Textboxbotoncontrol.TextBoxCalendar()
        Me.PrbProgreso = New System.Windows.Forms.ProgressBar()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.frCartel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.CmdSalir)
        Me.Panel1.Controls.Add(Me.CmdCancelar)
        Me.Panel1.Controls.Add(Me.CmdImprimir)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(591, 44)
        Me.Panel1.TabIndex = 1
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(302, 31)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Edición contrato socio"
        '
        'LblNombreEjercicio
        '
        Me.LblNombreEjercicio.AutoSize = True
        Me.LblNombreEjercicio.Location = New System.Drawing.Point(190, 85)
        Me.LblNombreEjercicio.Name = "LblNombreEjercicio"
        Me.LblNombreEjercicio.Size = New System.Drawing.Size(10, 13)
        Me.LblNombreEjercicio.TabIndex = 12
        Me.LblNombreEjercicio.Text = "-"
        '
        'lblNombreEmpresa
        '
        Me.lblNombreEmpresa.AutoSize = True
        Me.lblNombreEmpresa.Location = New System.Drawing.Point(190, 59)
        Me.lblNombreEmpresa.Name = "lblNombreEmpresa"
        Me.lblNombreEmpresa.Size = New System.Drawing.Size(10, 13)
        Me.lblNombreEmpresa.TabIndex = 11
        Me.lblNombreEmpresa.Text = "-"
        '
        'TxtEjercicio
        '
        Me.TxtEjercicio.Location = New System.Drawing.Point(84, 82)
        Me.TxtEjercicio.Name = "TxtEjercicio"
        Me.TxtEjercicio.Size = New System.Drawing.Size(100, 20)
        Me.TxtEjercicio.TabIndex = 10
        '
        'TxtEmpresa
        '
        Me.TxtEmpresa.Location = New System.Drawing.Point(84, 56)
        Me.TxtEmpresa.Name = "TxtEmpresa"
        Me.TxtEmpresa.Size = New System.Drawing.Size(100, 20)
        Me.TxtEmpresa.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Ejercicio:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Empresa:"
        '
        'lblNombreSocio
        '
        Me.lblNombreSocio.AutoSize = True
        Me.lblNombreSocio.Location = New System.Drawing.Point(220, 26)
        Me.lblNombreSocio.Name = "lblNombreSocio"
        Me.lblNombreSocio.Size = New System.Drawing.Size(10, 13)
        Me.lblNombreSocio.TabIndex = 17
        Me.lblNombreSocio.Text = "-"
        '
        'TxtCodigoSocio
        '
        Me.TxtCodigoSocio.Location = New System.Drawing.Point(114, 23)
        Me.TxtCodigoSocio.Name = "TxtCodigoSocio"
        Me.TxtCodigoSocio.Size = New System.Drawing.Size(100, 20)
        Me.TxtCodigoSocio.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Código socio:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.frCartel)
        Me.GroupBox1.Controls.Add(Me.txtCopias)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.CmdBuscaFicha)
        Me.GroupBox1.Controls.Add(Me.TxtPlantillaficha)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.CmdBuscarPlantilla)
        Me.GroupBox1.Controls.Add(Me.TxtPlantilla)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TxtFirmadopor)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TxtFechaEdicion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblNombreSocio)
        Me.GroupBox1.Controls.Add(Me.TxtCodigoSocio)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 117)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(591, 192)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Edición ficha"
        '
        'frCartel
        '
        Me.frCartel.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.frCartel.Controls.Add(Me.Label10)
        Me.frCartel.Controls.Add(Me.Label11)
        Me.frCartel.Controls.Add(Me.Button1)
        Me.frCartel.Location = New System.Drawing.Point(143, 37)
        Me.frCartel.Name = "frCartel"
        Me.frCartel.Size = New System.Drawing.Size(292, 59)
        Me.frCartel.TabIndex = 29
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(65, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(219, 16)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Imprimiendo la ficha solicitada"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(118, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(94, 16)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Un momento"
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
        'txtCopias
        '
        Me.txtCopias.Location = New System.Drawing.Point(114, 152)
        Me.txtCopias.Name = "txtCopias"
        Me.txtCopias.Size = New System.Drawing.Size(100, 20)
        Me.txtCopias.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(67, 155)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Copias:"
        '
        'CmdBuscaFicha
        '
        Me.CmdBuscaFicha.BackgroundImage = Global.Proysocios.My.Resources.Resources.buscararchivo
        Me.CmdBuscaFicha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBuscaFicha.Location = New System.Drawing.Point(539, 126)
        Me.CmdBuscaFicha.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscaFicha.Name = "CmdBuscaFicha"
        Me.CmdBuscaFicha.Size = New System.Drawing.Size(22, 22)
        Me.CmdBuscaFicha.TabIndex = 27
        Me.CmdBuscaFicha.UseVisualStyleBackColor = True
        '
        'TxtPlantillaficha
        '
        Me.TxtPlantillaficha.Location = New System.Drawing.Point(114, 128)
        Me.TxtPlantillaficha.Name = "TxtPlantillaficha"
        Me.TxtPlantillaficha.Size = New System.Drawing.Size(426, 20)
        Me.TxtPlantillaficha.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(32, 131)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Plantilla ficha:"
        '
        'CmdBuscarPlantilla
        '
        Me.CmdBuscarPlantilla.BackgroundImage = Global.Proysocios.My.Resources.Resources.buscararchivo
        Me.CmdBuscarPlantilla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBuscarPlantilla.Location = New System.Drawing.Point(539, 100)
        Me.CmdBuscarPlantilla.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarPlantilla.Name = "CmdBuscarPlantilla"
        Me.CmdBuscarPlantilla.Size = New System.Drawing.Size(22, 22)
        Me.CmdBuscarPlantilla.TabIndex = 24
        Me.CmdBuscarPlantilla.UseVisualStyleBackColor = True
        '
        'TxtPlantilla
        '
        Me.TxtPlantilla.Location = New System.Drawing.Point(114, 102)
        Me.TxtPlantilla.Name = "TxtPlantilla"
        Me.TxtPlantilla.Size = New System.Drawing.Size(426, 20)
        Me.TxtPlantilla.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 105)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Plantilla contrato:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(39, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Firmado por:"
        '
        'TxtFirmadopor
        '
        Me.TxtFirmadopor.Location = New System.Drawing.Point(114, 76)
        Me.TxtFirmadopor.Name = "TxtFirmadopor"
        Me.TxtFirmadopor.Size = New System.Drawing.Size(412, 20)
        Me.TxtFirmadopor.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Fecha edición:"
        '
        'TxtFechaEdicion
        '
        Me.TxtFechaEdicion.ButtonImage = Nothing
        Me.TxtFechaEdicion.FormatoFecha = "dd/MM/yyyy"
        Me.TxtFechaEdicion.Location = New System.Drawing.Point(114, 50)
        Me.TxtFechaEdicion.Name = "TxtFechaEdicion"
        Me.TxtFechaEdicion.Size = New System.Drawing.Size(100, 20)
        Me.TxtFechaEdicion.TabIndex = 18
        Me.TxtFechaEdicion.Text = "17/04/2024"
        Me.TxtFechaEdicion.value = New Date(2024, 4, 17, 0, 0, 0, 0)
        '
        'PrbProgreso
        '
        Me.PrbProgreso.Location = New System.Drawing.Point(0, 315)
        Me.PrbProgreso.Name = "PrbProgreso"
        Me.PrbProgreso.Size = New System.Drawing.Size(591, 11)
        Me.PrbProgreso.TabIndex = 19
        '
        'FrmEdicionContrato
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 325)
        Me.Controls.Add(Me.PrbProgreso)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LblNombreEjercicio)
        Me.Controls.Add(Me.lblNombreEmpresa)
        Me.Controls.Add(Me.TxtEjercicio)
        Me.Controls.Add(Me.TxtEmpresa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmEdicionContrato"
        Me.Text = "Edición contrato socio"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.frCartel.ResumeLayout(False)
        Me.frCartel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdImprimir As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents LblNombreEjercicio As Label
    Friend WithEvents lblNombreEmpresa As Label
    Friend WithEvents TxtEjercicio As TextBox
    Friend WithEvents TxtEmpresa As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblNombreSocio As Label
    Friend WithEvents TxtCodigoSocio As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtFirmadopor As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtFechaEdicion As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents txtCopias As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CmdBuscaFicha As Button
    Friend WithEvents TxtPlantillaficha As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CmdBuscarPlantilla As Button
    Friend WithEvents TxtPlantilla As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents PrbProgreso As ProgressBar
    Friend WithEvents frCartel As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Button1 As Button
End Class
