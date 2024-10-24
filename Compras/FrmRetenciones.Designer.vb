<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRetenciones
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
        Me.TxtBase = New System.Windows.Forms.TextBox()
        Me.TxtTipo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtBase
        '
        Me.TxtBase.Location = New System.Drawing.Point(115, 12)
        Me.TxtBase.Name = "TxtBase"
        Me.TxtBase.Size = New System.Drawing.Size(100, 20)
        Me.TxtBase.TabIndex = 0
        '
        'TxtTipo
        '
        Me.TxtTipo.Location = New System.Drawing.Point(115, 56)
        Me.TxtTipo.Name = "TxtTipo"
        Me.TxtTipo.Size = New System.Drawing.Size(39, 20)
        Me.TxtTipo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Base a retener"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tipo reención"
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackgroundImage = Global.novacompras.My.Resources.Resources.cancelar_cambio
        Me.CmdCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdCancelar.Location = New System.Drawing.Point(84, 104)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.Size = New System.Drawing.Size(43, 40)
        Me.CmdCancelar.TabIndex = 10
        Me.CmdCancelar.UseVisualStyleBackColor = True
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackgroundImage = Global.novacompras.My.Resources.Resources.correo_cambio
        Me.CmdAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdAceptar.Location = New System.Drawing.Point(35, 104)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.Size = New System.Drawing.Size(43, 40)
        Me.CmdAceptar.TabIndex = 9
        Me.CmdAceptar.UseVisualStyleBackColor = True
        '
        'FrmRetenciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(266, 155)
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtTipo)
        Me.Controls.Add(Me.TxtBase)
        Me.Name = "FrmRetenciones"
        Me.Text = "Retenciones"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtBase As TextBox
    Friend WithEvents TxtTipo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdAceptar As Button
End Class
