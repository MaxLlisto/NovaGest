<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClonarBulto
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
        Me.TxtBulto = New System.Windows.Forms.TextBox()
        Me.PsSalir = New System.Windows.Forms.Button()
        Me.PsClonar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(360, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Clonar en esta línea la clasificación el bulto:"
        '
        'TxtBulto
        '
        Me.TxtBulto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBulto.Location = New System.Drawing.Point(378, 29)
        Me.TxtBulto.Name = "TxtBulto"
        Me.TxtBulto.Size = New System.Drawing.Size(87, 26)
        Me.TxtBulto.TabIndex = 1
        '
        'PsSalir
        '
        Me.PsSalir.Image = Global.Novagestvarios.My.Resources.Resources.salirpersona
        Me.PsSalir.Location = New System.Drawing.Point(585, 21)
        Me.PsSalir.Name = "PsSalir"
        Me.PsSalir.Size = New System.Drawing.Size(43, 45)
        Me.PsSalir.TabIndex = 3
        Me.PsSalir.UseVisualStyleBackColor = True
        '
        'PsClonar
        '
        Me.PsClonar.Image = Global.Novagestvarios.My.Resources.Resources.clonar
        Me.PsClonar.Location = New System.Drawing.Point(496, 21)
        Me.PsClonar.Name = "PsClonar"
        Me.PsClonar.Size = New System.Drawing.Size(56, 45)
        Me.PsClonar.TabIndex = 2
        Me.PsClonar.UseVisualStyleBackColor = True
        '
        'ClonarBulto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 81)
        Me.Controls.Add(Me.PsSalir)
        Me.Controls.Add(Me.PsClonar)
        Me.Controls.Add(Me.TxtBulto)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ClonarBulto"
        Me.Text = "Clonar Bulto"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TxtBulto As TextBox
    Friend WithEvents PsClonar As Button
    Friend WithEvents PsSalir As Button
End Class
