<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo13t
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmNEntradasNuevo13t))
        Me.TxtObservaciones = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.OpSituacion04 = New System.Windows.Forms.RadioButton()
        Me.OpSituacion03 = New System.Windows.Forms.RadioButton()
        Me.OpSituacion02 = New System.Windows.Forms.RadioButton()
        Me.OpSituacion01 = New System.Windows.Forms.RadioButton()
        Me.OpSituacion00 = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtConclusion = New System.Windows.Forms.TextBox()
        Me.TxtHistorial = New System.Windows.Forms.TextBox()
        Me.Grabar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.lblPeriodo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTransportista = New System.Windows.Forms.Label()
        Me.lblVariedad = New System.Windows.Forms.Label()
        Me.lblSocio = New System.Windows.Forms.Label()
        Me.lblCapataz = New System.Windows.Forms.Label()
        Me.lblCampo = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblAlbaran = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtObservaciones
        '
        Me.TxtObservaciones.Location = New System.Drawing.Point(442, 509)
        Me.TxtObservaciones.Name = "TxtObservaciones"
        Me.TxtObservaciones.Size = New System.Drawing.Size(411, 20)
        Me.TxtObservaciones.TabIndex = 145
        '
        'Label10
        '
        Me.Label10.AutoEllipsis = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(299, 510)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 19)
        Me.Label10.TabIndex = 144
        Me.Label10.Text = "Observaciones:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OpSituacion04)
        Me.GroupBox1.Controls.Add(Me.OpSituacion03)
        Me.GroupBox1.Controls.Add(Me.OpSituacion02)
        Me.GroupBox1.Controls.Add(Me.OpSituacion01)
        Me.GroupBox1.Controls.Add(Me.OpSituacion00)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(859, 191)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(136, 216)
        Me.GroupBox1.TabIndex = 143
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Situación del aviso"
        '
        'OpSituacion04
        '
        Me.OpSituacion04.AutoSize = True
        Me.OpSituacion04.Location = New System.Drawing.Point(21, 177)
        Me.OpSituacion04.Name = "OpSituacion04"
        Me.OpSituacion04.Size = New System.Drawing.Size(69, 17)
        Me.OpSituacion04.TabIndex = 4
        Me.OpSituacion04.TabStop = True
        Me.OpSituacion04.Text = "Cerrado"
        Me.OpSituacion04.UseVisualStyleBackColor = True
        '
        'OpSituacion03
        '
        Me.OpSituacion03.AutoSize = True
        Me.OpSituacion03.Location = New System.Drawing.Point(21, 141)
        Me.OpSituacion03.Name = "OpSituacion03"
        Me.OpSituacion03.Size = New System.Drawing.Size(70, 17)
        Me.OpSituacion03.TabIndex = 3
        Me.OpSituacion03.TabStop = True
        Me.OpSituacion03.Text = "Avisado"
        Me.OpSituacion03.UseVisualStyleBackColor = True
        '
        'OpSituacion02
        '
        Me.OpSituacion02.AutoSize = True
        Me.OpSituacion02.Location = New System.Drawing.Point(21, 105)
        Me.OpSituacion02.Name = "OpSituacion02"
        Me.OpSituacion02.Size = New System.Drawing.Size(86, 17)
        Me.OpSituacion02.TabIndex = 2
        Me.OpSituacion02.TabStop = True
        Me.OpSituacion02.Text = "Intentando"
        Me.OpSituacion02.UseVisualStyleBackColor = True
        '
        'OpSituacion01
        '
        Me.OpSituacion01.AutoSize = True
        Me.OpSituacion01.Location = New System.Drawing.Point(21, 69)
        Me.OpSituacion01.Name = "OpSituacion01"
        Me.OpSituacion01.Size = New System.Drawing.Size(82, 17)
        Me.OpSituacion01.TabIndex = 1
        Me.OpSituacion01.TabStop = True
        Me.OpSituacion01.Text = "Pendiente"
        Me.OpSituacion01.UseVisualStyleBackColor = True
        '
        'OpSituacion00
        '
        Me.OpSituacion00.AutoSize = True
        Me.OpSituacion00.Location = New System.Drawing.Point(21, 33)
        Me.OpSituacion00.Name = "OpSituacion00"
        Me.OpSituacion00.Size = New System.Drawing.Size(89, 17)
        Me.OpSituacion00.TabIndex = 0
        Me.OpSituacion00.TabStop = True
        Me.OpSituacion00.Text = "No avisado"
        Me.OpSituacion00.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoEllipsis = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(564, 157)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(151, 19)
        Me.Label9.TabIndex = 142
        Me.Label9.Text = "Conclusión avisos"
        '
        'Label8
        '
        Me.Label8.AutoEllipsis = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(115, 157)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(128, 19)
        Me.Label8.TabIndex = 141
        Me.Label8.Text = "Aviso historial"
        '
        'TxtConclusion
        '
        Me.TxtConclusion.AcceptsReturn = True
        Me.TxtConclusion.AcceptsTab = True
        Me.TxtConclusion.Location = New System.Drawing.Point(442, 191)
        Me.TxtConclusion.Multiline = True
        Me.TxtConclusion.Name = "TxtConclusion"
        Me.TxtConclusion.Size = New System.Drawing.Size(411, 312)
        Me.TxtConclusion.TabIndex = 140
        '
        'TxtHistorial
        '
        Me.TxtHistorial.AcceptsReturn = True
        Me.TxtHistorial.AcceptsTab = True
        Me.TxtHistorial.Location = New System.Drawing.Point(16, 191)
        Me.TxtHistorial.Multiline = True
        Me.TxtHistorial.Name = "TxtHistorial"
        Me.TxtHistorial.Size = New System.Drawing.Size(411, 312)
        Me.TxtHistorial.TabIndex = 139
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = CType(resources.GetObject("Grabar.BackgroundImage"), System.Drawing.Image)
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(934, 485)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(61, 44)
        Me.Grabar.TabIndex = 138
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = CType(resources.GetObject("Salir.BackgroundImage"), System.Drawing.Image)
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(941, 5)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(54, 48)
        Me.Salir.TabIndex = 137
        Me.Salir.UseVisualStyleBackColor = True
        '
        'lblPeriodo
        '
        Me.lblPeriodo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPeriodo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPeriodo.Location = New System.Drawing.Point(118, 106)
        Me.lblPeriodo.Name = "lblPeriodo"
        Me.lblPeriodo.Size = New System.Drawing.Size(135, 23)
        Me.lblPeriodo.TabIndex = 133
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 19)
        Me.Label4.TabIndex = 125
        Me.Label4.Text = "Periodo:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTransportista
        '
        Me.lblTransportista.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblTransportista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTransportista.Location = New System.Drawing.Point(558, 74)
        Me.lblTransportista.Name = "lblTransportista"
        Me.lblTransportista.Size = New System.Drawing.Size(350, 23)
        Me.lblTransportista.TabIndex = 227
        '
        'lblVariedad
        '
        Me.lblVariedad.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblVariedad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblVariedad.Location = New System.Drawing.Point(558, 40)
        Me.lblVariedad.Name = "lblVariedad"
        Me.lblVariedad.Size = New System.Drawing.Size(192, 23)
        Me.lblVariedad.TabIndex = 226
        '
        'lblSocio
        '
        Me.lblSocio.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSocio.Location = New System.Drawing.Point(558, 7)
        Me.lblSocio.Name = "lblSocio"
        Me.lblSocio.Size = New System.Drawing.Size(350, 23)
        Me.lblSocio.TabIndex = 225
        '
        'lblCapataz
        '
        Me.lblCapataz.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCapataz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCapataz.Location = New System.Drawing.Point(118, 73)
        Me.lblCapataz.Name = "lblCapataz"
        Me.lblCapataz.Size = New System.Drawing.Size(135, 23)
        Me.lblCapataz.TabIndex = 224
        '
        'lblCampo
        '
        Me.lblCampo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblCampo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCampo.Location = New System.Drawing.Point(118, 40)
        Me.lblCampo.Name = "lblCampo"
        Me.lblCampo.Size = New System.Drawing.Size(135, 23)
        Me.lblCampo.TabIndex = 223
        '
        'lblFecha
        '
        Me.lblFecha.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFecha.Location = New System.Drawing.Point(275, 7)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(135, 23)
        Me.lblFecha.TabIndex = 222
        '
        'lblAlbaran
        '
        Me.lblAlbaran.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblAlbaran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAlbaran.Location = New System.Drawing.Point(118, 7)
        Me.lblAlbaran.Name = "lblAlbaran"
        Me.lblAlbaran.Size = New System.Drawing.Size(135, 23)
        Me.lblAlbaran.TabIndex = 221
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(378, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(171, 19)
        Me.Label7.TabIndex = 220
        Me.Label7.Text = "Transportista terceros:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoEllipsis = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(449, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 19)
        Me.Label6.TabIndex = 219
        Me.Label6.Text = "Variedad:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoEllipsis = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(449, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 19)
        Me.Label5.TabIndex = 218
        Me.Label5.Text = "Proveedor:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 19)
        Me.Label3.TabIndex = 217
        Me.Label3.Text = "Capataz terc:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 19)
        Me.Label2.TabIndex = 216
        Me.Label2.Text = "Campo terc:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 19)
        Me.Label1.TabIndex = 215
        Me.Label1.Text = "Albarán:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmNEntradasNuevo13t
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 538)
        Me.Controls.Add(Me.lblTransportista)
        Me.Controls.Add(Me.lblVariedad)
        Me.Controls.Add(Me.lblSocio)
        Me.Controls.Add(Me.lblCapataz)
        Me.Controls.Add(Me.lblCampo)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.lblAlbaran)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtObservaciones)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtConclusion)
        Me.Controls.Add(Me.TxtHistorial)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.lblPeriodo)
        Me.Controls.Add(Me.Label4)
        Me.Name = "FrmNEntradasNuevo13t"
        Me.Text = "Gestión de avisos / observaciones"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtObservaciones As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OpSituacion04 As RadioButton
    Friend WithEvents OpSituacion03 As RadioButton
    Friend WithEvents OpSituacion02 As RadioButton
    Friend WithEvents OpSituacion01 As RadioButton
    Friend WithEvents OpSituacion00 As RadioButton
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtConclusion As TextBox
    Friend WithEvents TxtHistorial As TextBox
    Friend WithEvents Grabar As Button
    Friend WithEvents Salir As Button
    Friend WithEvents lblPeriodo As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblTransportista As Label
    Friend WithEvents lblVariedad As Label
    Friend WithEvents lblSocio As Label
    Friend WithEvents lblCapataz As Label
    Friend WithEvents lblCampo As Label
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblAlbaran As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
