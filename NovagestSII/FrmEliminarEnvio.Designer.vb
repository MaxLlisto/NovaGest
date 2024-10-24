<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEliminarEnvio
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Opcion1 = New System.Windows.Forms.RadioButton()
        Me.Opcion2 = New System.Windows.Forms.RadioButton()
        Me.Opcion4 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtNumeroenvio = New System.Windows.Forms.TextBox()
        Me.CmdBorrar = New System.Windows.Forms.Button()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Opcion4)
        Me.GroupBox1.Controls.Add(Me.Opcion2)
        Me.GroupBox1.Controls.Add(Me.Opcion1)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(373, 69)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Opcion1
        '
        Me.Opcion1.AutoSize = True
        Me.Opcion1.Location = New System.Drawing.Point(16, 30)
        Me.Opcion1.Name = "Opcion1"
        Me.Opcion1.Size = New System.Drawing.Size(64, 17)
        Me.Opcion1.TabIndex = 0
        Me.Opcion1.TabStop = True
        Me.Opcion1.Text = "Emitidas"
        Me.Opcion1.UseVisualStyleBackColor = True
        '
        'Opcion2
        '
        Me.Opcion2.AutoSize = True
        Me.Opcion2.Location = New System.Drawing.Point(118, 30)
        Me.Opcion2.Name = "Opcion2"
        Me.Opcion2.Size = New System.Drawing.Size(72, 17)
        Me.Opcion2.TabIndex = 1
        Me.Opcion2.TabStop = True
        Me.Opcion2.Text = "Recibidas"
        Me.Opcion2.UseVisualStyleBackColor = True
        '
        'Opcion4
        '
        Me.Opcion4.AutoSize = True
        Me.Opcion4.Location = New System.Drawing.Point(239, 30)
        Me.Opcion4.Name = "Opcion4"
        Me.Opcion4.Size = New System.Drawing.Size(117, 17)
        Me.Opcion4.TabIndex = 2
        Me.Opcion4.TabStop = True
        Me.Opcion4.Text = "Bienes de inversión"
        Me.Opcion4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(76, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Número de envío"
        '
        'TxtNumeroenvio
        '
        Me.TxtNumeroenvio.Location = New System.Drawing.Point(172, 114)
        Me.TxtNumeroenvio.Name = "TxtNumeroenvio"
        Me.TxtNumeroenvio.Size = New System.Drawing.Size(100, 20)
        Me.TxtNumeroenvio.TabIndex = 2
        '
        'CmdBorrar
        '
        Me.CmdBorrar.BackgroundImage = Global.novagestSII.My.Resources.Resources.goma_de_borrar__2_
        Me.CmdBorrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBorrar.Location = New System.Drawing.Point(287, 104)
        Me.CmdBorrar.Name = "CmdBorrar"
        Me.CmdBorrar.Size = New System.Drawing.Size(40, 38)
        Me.CmdBorrar.TabIndex = 3
        Me.CmdBorrar.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = Global.novagestSII.My.Resources.Resources.salir1
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(401, 22)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(70, 60)
        Me.CmdSalir.TabIndex = 4
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'FrmEliminarEnvio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 190)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.CmdBorrar)
        Me.Controls.Add(Me.TxtNumeroenvio)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmEliminarEnvio"
        Me.Text = "Eliminar Envio"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Opcion4 As RadioButton
    Friend WithEvents Opcion2 As RadioButton
    Friend WithEvents Opcion1 As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtNumeroenvio As TextBox
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents CmdSalir As Button
End Class
