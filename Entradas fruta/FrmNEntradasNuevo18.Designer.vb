<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo18
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
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPorcentaje_peq = New System.Windows.Forms.TextBox()
        Me.txtPorcentaje_grand = New System.Windows.Forms.TextBox()
        Me.TxtPorcentaje_recol = New System.Windows.Forms.TextBox()
        Me.TxtPorcentaje_Plaga = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TxtPorcentaje_recol_i = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblSuma = New System.Windows.Forms.Label()
        Me.lblNC = New System.Windows.Forms.Label()
        Me.lblDif = New System.Windows.Forms.Label()
        Me.Grabar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(100, 92)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(127, 13)
        Me.Label17.TabIndex = 139
        Me.Label17.Text = "Porcentaje pequeñas"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(110, 65)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(117, 13)
        Me.Label18.TabIndex = 138
        Me.Label18.Text = "Porcentaje grandes"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(63, 39)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(164, 13)
        Me.Label16.TabIndex = 137
        Me.Label16.Text = "Porcentaje def. recolección"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(118, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 13)
        Me.Label15.TabIndex = 136
        Me.Label15.Text = "Porcentaje plagas"
        '
        'txtPorcentaje_peq
        '
        Me.txtPorcentaje_peq.Location = New System.Drawing.Point(242, 90)
        Me.txtPorcentaje_peq.Name = "txtPorcentaje_peq"
        Me.txtPorcentaje_peq.Size = New System.Drawing.Size(86, 20)
        Me.txtPorcentaje_peq.TabIndex = 135
        '
        'txtPorcentaje_grand
        '
        Me.txtPorcentaje_grand.Location = New System.Drawing.Point(242, 64)
        Me.txtPorcentaje_grand.Name = "txtPorcentaje_grand"
        Me.txtPorcentaje_grand.Size = New System.Drawing.Size(86, 20)
        Me.txtPorcentaje_grand.TabIndex = 134
        '
        'TxtPorcentaje_recol
        '
        Me.TxtPorcentaje_recol.Location = New System.Drawing.Point(242, 38)
        Me.TxtPorcentaje_recol.Name = "TxtPorcentaje_recol"
        Me.TxtPorcentaje_recol.Size = New System.Drawing.Size(86, 20)
        Me.TxtPorcentaje_recol.TabIndex = 133
        '
        'TxtPorcentaje_Plaga
        '
        Me.TxtPorcentaje_Plaga.Location = New System.Drawing.Point(242, 12)
        Me.TxtPorcentaje_Plaga.Name = "TxtPorcentaje_Plaga"
        Me.TxtPorcentaje_Plaga.Size = New System.Drawing.Size(86, 20)
        Me.TxtPorcentaje_Plaga.TabIndex = 132
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(22, 228)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(205, 13)
        Me.Label19.TabIndex = 141
        Me.Label19.Text = "Porcentaje def. recolección inicial:"
        '
        'TxtPorcentaje_recol_i
        '
        Me.TxtPorcentaje_recol_i.Location = New System.Drawing.Point(242, 228)
        Me.TxtPorcentaje_recol_i.Name = "TxtPorcentaje_recol_i"
        Me.TxtPorcentaje_recol_i.Size = New System.Drawing.Size(82, 20)
        Me.TxtPorcentaje_recol_i.TabIndex = 140
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(189, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 142
        Me.Label1.Text = "Suma"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(199, 159)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 143
        Me.Label2.Text = "NC."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(162, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 144
        Me.Label3.Text = "Diferencia"
        '
        'lblSuma
        '
        Me.lblSuma.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSuma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuma.Location = New System.Drawing.Point(242, 130)
        Me.lblSuma.Name = "lblSuma"
        Me.lblSuma.Size = New System.Drawing.Size(82, 23)
        Me.lblSuma.TabIndex = 145
        '
        'lblNC
        '
        Me.lblNC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblNC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNC.Location = New System.Drawing.Point(242, 161)
        Me.lblNC.Name = "lblNC"
        Me.lblNC.Size = New System.Drawing.Size(82, 23)
        Me.lblNC.TabIndex = 146
        '
        'lblDif
        '
        Me.lblDif.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblDif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDif.Location = New System.Drawing.Point(242, 190)
        Me.lblDif.Name = "lblDif"
        Me.lblDif.Size = New System.Drawing.Size(82, 23)
        Me.lblDif.TabIndex = 147
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.grabar1
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(336, 204)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(61, 44)
        Me.Grabar.TabIndex = 149
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(343, 10)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(54, 48)
        Me.Salir.TabIndex = 148
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo18
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 267)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.lblDif)
        Me.Controls.Add(Me.lblNC)
        Me.Controls.Add(Me.lblSuma)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.TxtPorcentaje_recol_i)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtPorcentaje_peq)
        Me.Controls.Add(Me.txtPorcentaje_grand)
        Me.Controls.Add(Me.TxtPorcentaje_recol)
        Me.Controls.Add(Me.TxtPorcentaje_Plaga)
        Me.Name = "FrmNEntradasNuevo18"
        Me.Text = "Modificación porcentaje defectos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents txtPorcentaje_peq As TextBox
    Friend WithEvents txtPorcentaje_grand As TextBox
    Friend WithEvents TxtPorcentaje_recol As TextBox
    Friend WithEvents TxtPorcentaje_Plaga As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents TxtPorcentaje_recol_i As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblSuma As Label
    Friend WithEvents lblNC As Label
    Friend WithEvents lblDif As Label
    Friend WithEvents Grabar As Button
    Friend WithEvents Salir As Button
End Class
