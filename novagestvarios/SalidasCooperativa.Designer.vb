<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalidasCooperativa
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
        Me.horasalida = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Candelar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'horasalida
        '
        Me.horasalida.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.horasalida.Location = New System.Drawing.Point(123, 27)
        Me.horasalida.Name = "horasalida"
        Me.horasalida.Size = New System.Drawing.Size(100, 20)
        Me.horasalida.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Hora de salida"
        '
        'Candelar
        '
        Me.Candelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Candelar.Image = My.Resources.Resources.salir
        Me.Candelar.Location = New System.Drawing.Point(166, 88)
        Me.Candelar.Name = "Candelar"
        Me.Candelar.Size = New System.Drawing.Size(74, 56)
        Me.Candelar.TabIndex = 3
        Me.Candelar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Salir.Image = My.Resources.Resources.salirpersona
        Me.Salir.Location = New System.Drawing.Point(29, 88)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(74, 56)
        Me.Salir.TabIndex = 2
        Me.Salir.UseVisualStyleBackColor = True
        '
        'SalidasCooperativa
        '
        Me.AcceptButton = Me.Salir
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 165)
        Me.Controls.Add(Me.Candelar)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.horasalida)
        Me.Name = "SalidasCooperativa"
        Me.Text = "Salidas Cooperativa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents horasalida As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Salir As Button
    Friend WithEvents Candelar As Button
End Class
