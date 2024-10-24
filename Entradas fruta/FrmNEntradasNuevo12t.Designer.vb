<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo12T
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
        Me.TxtHora = New System.Windows.Forms.TextBox()
        Me.DTFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LblPeriodoN = New System.Windows.Forms.Label()
        Me.TxtPeriodo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
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
        Me.Grabar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtHora
        '
        Me.TxtHora.Location = New System.Drawing.Point(713, 152)
        Me.TxtHora.Name = "TxtHora"
        Me.TxtHora.Size = New System.Drawing.Size(75, 20)
        Me.TxtHora.TabIndex = 120
        '
        'DTFecha
        '
        Me.DTFecha.ButtonImage = Nothing
        Me.DTFecha.FormatoFecha = "dd/MM/yyyy"
        Me.DTFecha.Location = New System.Drawing.Point(583, 152)
        Me.DTFecha.Name = "DTFecha"
        Me.DTFecha.Size = New System.Drawing.Size(124, 20)
        Me.DTFecha.TabIndex = 119
        Me.DTFecha.Text = "25/09/2023"
        Me.DTFecha.value = New Date(2023, 9, 25, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(500, 158)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "Fecha/Hora:"
        '
        'LblPeriodoN
        '
        Me.LblPeriodoN.Location = New System.Drawing.Point(128, 155)
        Me.LblPeriodoN.Name = "LblPeriodoN"
        Me.LblPeriodoN.Size = New System.Drawing.Size(285, 23)
        Me.LblPeriodoN.TabIndex = 117
        Me.LblPeriodoN.Text = "-"
        '
        'TxtPeriodo
        '
        Me.TxtPeriodo.Location = New System.Drawing.Point(52, 155)
        Me.TxtPeriodo.Name = "TxtPeriodo"
        Me.TxtPeriodo.Size = New System.Drawing.Size(70, 20)
        Me.TxtPeriodo.TabIndex = 116
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(-1, 155)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 115
        Me.Label9.Text = "Periodo:"
        '
        'lblTransportista
        '
        Me.lblTransportista.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblTransportista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTransportista.Location = New System.Drawing.Point(552, 72)
        Me.lblTransportista.Name = "lblTransportista"
        Me.lblTransportista.Size = New System.Drawing.Size(350, 23)
        Me.lblTransportista.TabIndex = 112
        '
        'lblVariedad
        '
        Me.lblVariedad.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblVariedad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblVariedad.Location = New System.Drawing.Point(552, 38)
        Me.lblVariedad.Name = "lblVariedad"
        Me.lblVariedad.Size = New System.Drawing.Size(192, 23)
        Me.lblVariedad.TabIndex = 111
        '
        'lblSocio
        '
        Me.lblSocio.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSocio.Location = New System.Drawing.Point(552, 5)
        Me.lblSocio.Name = "lblSocio"
        Me.lblSocio.Size = New System.Drawing.Size(350, 23)
        Me.lblSocio.TabIndex = 110
        '
        'lblPeriodo
        '
        Me.lblPeriodo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPeriodo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPeriodo.Location = New System.Drawing.Point(112, 108)
        Me.lblPeriodo.Name = "lblPeriodo"
        Me.lblPeriodo.Size = New System.Drawing.Size(135, 23)
        Me.lblPeriodo.TabIndex = 109
        '
        'lblCapataz
        '
        Me.lblCapataz.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCapataz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCapataz.Location = New System.Drawing.Point(112, 77)
        Me.lblCapataz.Name = "lblCapataz"
        Me.lblCapataz.Size = New System.Drawing.Size(135, 23)
        Me.lblCapataz.TabIndex = 108
        '
        'lblCampo
        '
        Me.lblCampo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCampo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCampo.Location = New System.Drawing.Point(112, 43)
        Me.lblCampo.Name = "lblCampo"
        Me.lblCampo.Size = New System.Drawing.Size(135, 23)
        Me.lblCampo.TabIndex = 107
        '
        'lblFecha
        '
        Me.lblFecha.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFecha.Location = New System.Drawing.Point(269, 5)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(135, 23)
        Me.lblFecha.TabIndex = 106
        '
        'lblAlbaran
        '
        Me.lblAlbaran.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAlbaran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAlbaran.Location = New System.Drawing.Point(112, 5)
        Me.lblAlbaran.Name = "lblAlbaran"
        Me.lblAlbaran.Size = New System.Drawing.Size(135, 23)
        Me.lblAlbaran.TabIndex = 105
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(446, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 19)
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "Transportista:"
        '
        'Label6
        '
        Me.Label6.AutoEllipsis = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(446, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 19)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "Variedad:"
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(446, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 19)
        Me.Label5.TabIndex = 102
        Me.Label5.Text = "Código socio:"
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 19)
        Me.Label4.TabIndex = 101
        Me.Label4.Text = "Periodo:"
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 19)
        Me.Label3.TabIndex = 100
        Me.Label3.Text = "Capataz:"
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 19)
        Me.Label2.TabIndex = 99
        Me.Label2.Text = "Campo:"
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 19)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Albarán:"
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.grabar1
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(950, 142)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(61, 44)
        Me.Grabar.TabIndex = 114
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(957, 8)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(54, 48)
        Me.Salir.TabIndex = 113
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo12t
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 194)
        Me.Controls.Add(Me.TxtHora)
        Me.Controls.Add(Me.DTFecha)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.LblPeriodoN)
        Me.Controls.Add(Me.TxtPeriodo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Grabar)
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
        Me.Name = "FrmNEntradasNuevo12t"
        Me.Text = "Modificación periodo fecha y hora(terceros)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtHora As TextBox
    Friend WithEvents DTFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label10 As Label
    Friend WithEvents LblPeriodoN As Label
    Friend WithEvents TxtPeriodo As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Grabar As Button
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
End Class
