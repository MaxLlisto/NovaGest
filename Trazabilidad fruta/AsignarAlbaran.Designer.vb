<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AsignarAlbaran
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
        Me.lblLote = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.OpReasignar = New System.Windows.Forms.RadioButton()
        Me.OpPrecalibrado = New System.Windows.Forms.RadioButton()
        Me.OpIgnorar = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TxtAlbaran = New System.Windows.Forms.TextBox()
        Me.TxtBulto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Asignar = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblLote)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(128, 126)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Lote"
        '
        'lblLote
        '
        Me.lblLote.AutoSize = True
        Me.lblLote.ForeColor = System.Drawing.Color.Blue
        Me.lblLote.Location = New System.Drawing.Point(22, 56)
        Me.lblLote.Name = "lblLote"
        Me.lblLote.Size = New System.Drawing.Size(19, 20)
        Me.lblLote.TabIndex = 1
        Me.lblLote.Text = "0"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OpIgnorar)
        Me.GroupBox2.Controls.Add(Me.OpPrecalibrado)
        Me.GroupBox2.Controls.Add(Me.OpReasignar)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(146, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(207, 126)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Asignar"
        '
        'OpReasignar
        '
        Me.OpReasignar.AutoSize = True
        Me.OpReasignar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpReasignar.Location = New System.Drawing.Point(21, 25)
        Me.OpReasignar.Name = "OpReasignar"
        Me.OpReasignar.Size = New System.Drawing.Size(73, 20)
        Me.OpReasignar.TabIndex = 0
        Me.OpReasignar.TabStop = True
        Me.OpReasignar.Text = "Albaran"
        Me.OpReasignar.UseVisualStyleBackColor = True
        '
        'OpPrecalibrado
        '
        Me.OpPrecalibrado.AutoSize = True
        Me.OpPrecalibrado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpPrecalibrado.Location = New System.Drawing.Point(21, 53)
        Me.OpPrecalibrado.Name = "OpPrecalibrado"
        Me.OpPrecalibrado.Size = New System.Drawing.Size(104, 20)
        Me.OpPrecalibrado.TabIndex = 1
        Me.OpPrecalibrado.TabStop = True
        Me.OpPrecalibrado.Text = "Precalibrado"
        Me.OpPrecalibrado.UseVisualStyleBackColor = True
        '
        'OpIgnorar
        '
        Me.OpIgnorar.AutoSize = True
        Me.OpIgnorar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpIgnorar.Location = New System.Drawing.Point(21, 81)
        Me.OpIgnorar.Name = "OpIgnorar"
        Me.OpIgnorar.Size = New System.Drawing.Size(68, 20)
        Me.OpIgnorar.TabIndex = 2
        Me.OpIgnorar.TabStop = True
        Me.OpIgnorar.Text = "Ignorar"
        Me.OpIgnorar.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TxtBulto)
        Me.GroupBox3.Controls.Add(Me.TxtAlbaran)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(359, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(264, 126)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Documentos"
        '
        'TxtAlbaran
        '
        Me.TxtAlbaran.Location = New System.Drawing.Point(92, 24)
        Me.TxtAlbaran.Name = "TxtAlbaran"
        Me.TxtAlbaran.Size = New System.Drawing.Size(149, 26)
        Me.TxtAlbaran.TabIndex = 0
        '
        'TxtBulto
        '
        Me.TxtBulto.Location = New System.Drawing.Point(92, 56)
        Me.TxtBulto.Name = "TxtBulto"
        Me.TxtBulto.Size = New System.Drawing.Size(91, 26)
        Me.TxtBulto.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Albarán"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Bulto"
        '
        'Salir
        '
        Me.Salir.Image = Global.Controlfruta.My.Resources.Resources.puerta3
        Me.Salir.Location = New System.Drawing.Point(740, 18)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(105, 115)
        Me.Salir.TabIndex = 5
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Asignar
        '
        Me.Asignar.Image = Global.Controlfruta.My.Resources.Resources.grabar
        Me.Asignar.Location = New System.Drawing.Point(629, 18)
        Me.Asignar.Name = "Asignar"
        Me.Asignar.Size = New System.Drawing.Size(105, 115)
        Me.Asignar.TabIndex = 4
        Me.Asignar.UseVisualStyleBackColor = True
        '
        'AsignarAlbaran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(858, 150)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Asignar)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "AsignarAlbaran"
        Me.Text = "Asignar albarán"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblLote As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents OpIgnorar As RadioButton
    Friend WithEvents OpPrecalibrado As RadioButton
    Friend WithEvents OpReasignar As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtBulto As TextBox
    Friend WithEvents TxtAlbaran As TextBox
    Friend WithEvents Asignar As Button
    Friend WithEvents Salir As Button
End Class
