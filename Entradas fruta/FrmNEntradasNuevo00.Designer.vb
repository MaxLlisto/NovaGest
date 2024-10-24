<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo00
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAlbaran = New System.Windows.Forms.Label()
        Me.lblSocio = New System.Windows.Forms.Label()
        Me.lblCampo = New System.Windows.Forms.Label()
        Me.lblVariedad = New System.Windows.Forms.Label()
        Me.lblKillos = New System.Windows.Forms.Label()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Grabar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Albarán:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Socio:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(31, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Campo:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(19, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Variedad:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(42, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Kilos:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(246, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Fecha:"
        '
        'lblAlbaran
        '
        Me.lblAlbaran.Location = New System.Drawing.Point(88, 16)
        Me.lblAlbaran.Name = "lblAlbaran"
        Me.lblAlbaran.Size = New System.Drawing.Size(140, 25)
        Me.lblAlbaran.TabIndex = 8
        '
        'lblSocio
        '
        Me.lblSocio.Location = New System.Drawing.Point(88, 56)
        Me.lblSocio.Name = "lblSocio"
        Me.lblSocio.Size = New System.Drawing.Size(325, 24)
        Me.lblSocio.TabIndex = 9
        '
        'lblCampo
        '
        Me.lblCampo.Location = New System.Drawing.Point(88, 93)
        Me.lblCampo.Name = "lblCampo"
        Me.lblCampo.Size = New System.Drawing.Size(204, 23)
        Me.lblCampo.TabIndex = 10
        '
        'lblVariedad
        '
        Me.lblVariedad.Location = New System.Drawing.Point(86, 124)
        Me.lblVariedad.Name = "lblVariedad"
        Me.lblVariedad.Size = New System.Drawing.Size(354, 29)
        Me.lblVariedad.TabIndex = 11
        '
        'lblKillos
        '
        Me.lblKillos.Location = New System.Drawing.Point(86, 162)
        Me.lblKillos.Name = "lblKillos"
        Me.lblKillos.Size = New System.Drawing.Size(154, 31)
        Me.lblKillos.TabIndex = 12
        '
        'lblFecha
        '
        Me.lblFecha.Location = New System.Drawing.Point(299, 20)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(218, 20)
        Me.lblFecha.TabIndex = 13
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.novaEntradas.My.Resources.Resources.cerrar
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(282, 199)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(43, 43)
        Me.Button1.TabIndex = 7
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.cheque__1_
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(154, 199)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(43, 43)
        Me.Grabar.TabIndex = 6
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo00
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 254)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.lblKillos)
        Me.Controls.Add(Me.lblVariedad)
        Me.Controls.Add(Me.lblCampo)
        Me.Controls.Add(Me.lblSocio)
        Me.Controls.Add(Me.lblAlbaran)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmNEntradasNuevo00"
        Me.Text = "Albarán previo coincidente en capataz y transportista"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Grabar As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents lblAlbaran As Label
    Friend WithEvents lblSocio As Label
    Friend WithEvents lblCampo As Label
    Friend WithEvents lblVariedad As Label
    Friend WithEvents lblKillos As Label
    Friend WithEvents lblFecha As Label
End Class
