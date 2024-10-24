<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo11
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
        Me.Salir = New System.Windows.Forms.Button()
        Me.lblTransportista = New System.Windows.Forms.Label()
        Me.lblVariedad = New System.Windows.Forms.Label()
        Me.lblSocio = New System.Windows.Forms.Label()
        Me.lblPeriodo = New System.Windows.Forms.Label()
        Me.lblCapataz = New System.Windows.Forms.Label()
        Me.lblCampo = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblAlbaran = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblCampoVariedadSeleccionado = New System.Windows.Forms.Label()
        Me.LblSocioSeleccionado = New System.Windows.Forms.Label()
        Me.LblDatosSeleccionado = New System.Windows.Forms.Label()
        Me.LstCampos = New System.Windows.Forms.ListView()
        Me.LstSocios = New System.Windows.Forms.ListView()
        Me.LstProceso = New System.Windows.Forms.ListBox()
        Me.Txtnombre = New System.Windows.Forms.TextBox()
        Me.TxtCodigosocio = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Grabar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(927, 9)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(54, 48)
        Me.Salir.TabIndex = 74
        Me.Salir.UseVisualStyleBackColor = True
        '
        'lblTransportista
        '
        Me.lblTransportista.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblTransportista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTransportista.Location = New System.Drawing.Point(554, 76)
        Me.lblTransportista.Name = "lblTransportista"
        Me.lblTransportista.Size = New System.Drawing.Size(350, 23)
        Me.lblTransportista.TabIndex = 73
        '
        'lblVariedad
        '
        Me.lblVariedad.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblVariedad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblVariedad.Location = New System.Drawing.Point(554, 42)
        Me.lblVariedad.Name = "lblVariedad"
        Me.lblVariedad.Size = New System.Drawing.Size(192, 23)
        Me.lblVariedad.TabIndex = 72
        '
        'lblSocio
        '
        Me.lblSocio.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSocio.Location = New System.Drawing.Point(554, 9)
        Me.lblSocio.Name = "lblSocio"
        Me.lblSocio.Size = New System.Drawing.Size(350, 23)
        Me.lblSocio.TabIndex = 71
        '
        'lblPeriodo
        '
        Me.lblPeriodo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPeriodo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPeriodo.Location = New System.Drawing.Point(114, 112)
        Me.lblPeriodo.Name = "lblPeriodo"
        Me.lblPeriodo.Size = New System.Drawing.Size(135, 23)
        Me.lblPeriodo.TabIndex = 70
        '
        'lblCapataz
        '
        Me.lblCapataz.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCapataz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCapataz.Location = New System.Drawing.Point(114, 81)
        Me.lblCapataz.Name = "lblCapataz"
        Me.lblCapataz.Size = New System.Drawing.Size(135, 23)
        Me.lblCapataz.TabIndex = 69
        '
        'lblCampo
        '
        Me.lblCampo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCampo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCampo.Location = New System.Drawing.Point(114, 47)
        Me.lblCampo.Name = "lblCampo"
        Me.lblCampo.Size = New System.Drawing.Size(135, 23)
        Me.lblCampo.TabIndex = 68
        '
        'lblFecha
        '
        Me.lblFecha.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFecha.Location = New System.Drawing.Point(271, 9)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(135, 23)
        Me.lblFecha.TabIndex = 67
        '
        'lblAlbaran
        '
        Me.lblAlbaran.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAlbaran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAlbaran.Location = New System.Drawing.Point(114, 9)
        Me.lblAlbaran.Name = "lblAlbaran"
        Me.lblAlbaran.Size = New System.Drawing.Size(135, 23)
        Me.lblAlbaran.TabIndex = 66
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(448, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 19)
        Me.Label7.TabIndex = 65
        Me.Label7.Text = "Transportista:"
        '
        'Label6
        '
        Me.Label6.AutoEllipsis = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(448, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 19)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Variedad:"
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(448, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 19)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Código socio:"
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 19)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Periodo:"
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 19)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Capataz:"
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 19)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Campo:"
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 19)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Albarán:"
        '
        'LblCampoVariedadSeleccionado
        '
        Me.LblCampoVariedadSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCampoVariedadSeleccionado.Location = New System.Drawing.Point(454, 557)
        Me.LblCampoVariedadSeleccionado.Name = "LblCampoVariedadSeleccionado"
        Me.LblCampoVariedadSeleccionado.Size = New System.Drawing.Size(514, 23)
        Me.LblCampoVariedadSeleccionado.TabIndex = 79
        Me.LblCampoVariedadSeleccionado.Text = "-"
        '
        'LblSocioSeleccionado
        '
        Me.LblSocioSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblSocioSeleccionado.Location = New System.Drawing.Point(15, 557)
        Me.LblSocioSeleccionado.Name = "LblSocioSeleccionado"
        Me.LblSocioSeleccionado.Size = New System.Drawing.Size(427, 23)
        Me.LblSocioSeleccionado.TabIndex = 78
        Me.LblSocioSeleccionado.Text = "-"
        '
        'LblDatosSeleccionado
        '
        Me.LblDatosSeleccionado.Location = New System.Drawing.Point(457, 199)
        Me.LblDatosSeleccionado.Name = "LblDatosSeleccionado"
        Me.LblDatosSeleccionado.Size = New System.Drawing.Size(511, 23)
        Me.LblDatosSeleccionado.TabIndex = 77
        Me.LblDatosSeleccionado.Text = "-"
        '
        'LstCampos
        '
        Me.LstCampos.FullRowSelect = True
        Me.LstCampos.GridLines = True
        Me.LstCampos.HideSelection = False
        Me.LstCampos.Location = New System.Drawing.Point(451, 226)
        Me.LstCampos.Name = "LstCampos"
        Me.LstCampos.Size = New System.Drawing.Size(517, 328)
        Me.LstCampos.TabIndex = 76
        Me.LstCampos.UseCompatibleStateImageBehavior = False
        Me.LstCampos.View = System.Windows.Forms.View.Details
        '
        'LstSocios
        '
        Me.LstSocios.FullRowSelect = True
        Me.LstSocios.GridLines = True
        Me.LstSocios.HideSelection = False
        Me.LstSocios.Location = New System.Drawing.Point(15, 199)
        Me.LstSocios.Name = "LstSocios"
        Me.LstSocios.Size = New System.Drawing.Size(430, 355)
        Me.LstSocios.TabIndex = 75
        Me.LstSocios.UseCompatibleStateImageBehavior = False
        Me.LstSocios.View = System.Windows.Forms.View.Details
        '
        'LstProceso
        '
        Me.LstProceso.FormattingEnabled = True
        Me.LstProceso.Location = New System.Drawing.Point(908, 462)
        Me.LstProceso.Name = "LstProceso"
        Me.LstProceso.Size = New System.Drawing.Size(71, 30)
        Me.LstProceso.TabIndex = 80
        '
        'Txtnombre
        '
        Me.Txtnombre.Location = New System.Drawing.Point(187, 159)
        Me.Txtnombre.Name = "Txtnombre"
        Me.Txtnombre.Size = New System.Drawing.Size(278, 20)
        Me.Txtnombre.TabIndex = 83
        '
        'TxtCodigosocio
        '
        Me.TxtCodigosocio.Location = New System.Drawing.Point(112, 158)
        Me.TxtCodigosocio.Name = "TxtCodigosocio"
        Me.TxtCodigosocio.Size = New System.Drawing.Size(69, 20)
        Me.TxtCodigosocio.TabIndex = 82
        '
        'Label8
        '
        Me.Label8.AutoEllipsis = True
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 16)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "Código socio:"
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.grabar1
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(917, 560)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(51, 55)
        Me.Grabar.TabIndex = 123
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo11
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(989, 627)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.Txtnombre)
        Me.Controls.Add(Me.TxtCodigosocio)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.LblCampoVariedadSeleccionado)
        Me.Controls.Add(Me.LblSocioSeleccionado)
        Me.Controls.Add(Me.LblDatosSeleccionado)
        Me.Controls.Add(Me.LstCampos)
        Me.Controls.Add(Me.LstSocios)
        Me.Controls.Add(Me.LstProceso)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.lblTransportista)
        Me.Controls.Add(Me.lblVariedad)
        Me.Controls.Add(Me.lblSocio)
        Me.Controls.Add(Me.lblPeriodo)
        Me.Controls.Add(Me.lblCapataz)
        Me.Controls.Add(Me.lblCampo)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.lblAlbaran)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmNEntradasNuevo11"
        Me.Text = "Modificación Socio / Campo / Cultivo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Salir As Button
    Friend WithEvents lblTransportista As Label
    Friend WithEvents lblVariedad As Label
    Friend WithEvents lblSocio As Label
    Friend WithEvents lblPeriodo As Label
    Friend WithEvents lblCapataz As Label
    Friend WithEvents lblCampo As Label
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblAlbaran As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LblCampoVariedadSeleccionado As Label
    Friend WithEvents LblSocioSeleccionado As Label
    Friend WithEvents LblDatosSeleccionado As Label
    Friend WithEvents LstCampos As ListView
    Friend WithEvents LstSocios As ListView
    Friend WithEvents LstProceso As ListBox
    Friend WithEvents Txtnombre As TextBox
    Friend WithEvents TxtCodigosocio As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Grabar As Button
End Class
