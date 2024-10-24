<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FechaFactura
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
        Me.dFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Aceptar = New System.Windows.Forms.Button()
        Me.Cancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dFecha
        '
        Me.dFecha.ButtonImage = Nothing
        Me.dFecha.FormatoFecha = "dd/MM/yyyy"
        Me.dFecha.Location = New System.Drawing.Point(49, 45)
        Me.dFecha.Name = "dFecha"
        Me.dFecha.Size = New System.Drawing.Size(144, 20)
        Me.dFecha.TabIndex = 0
        Me.dFecha.Text = "11/08/2023"
        Me.dFecha.value = New Date(2023, 8, 11, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha factura"
        '
        'Aceptar
        '
        Me.Aceptar.BackgroundImage = Global.novagestventas.My.Resources.Resources.garrapata
        Me.Aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Aceptar.Location = New System.Drawing.Point(215, 32)
        Me.Aceptar.Name = "Aceptar"
        Me.Aceptar.Size = New System.Drawing.Size(41, 41)
        Me.Aceptar.TabIndex = 2
        Me.Aceptar.UseVisualStyleBackColor = True
        '
        'Cancelar
        '
        Me.Cancelar.BackgroundImage = Global.novagestventas.My.Resources.Resources.salir11
        Me.Cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancelar.Location = New System.Drawing.Point(296, 32)
        Me.Cancelar.Name = "Cancelar"
        Me.Cancelar.Size = New System.Drawing.Size(47, 41)
        Me.Cancelar.TabIndex = 4
        Me.Cancelar.UseVisualStyleBackColor = True
        '
        'FechaFactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 116)
        Me.Controls.Add(Me.Cancelar)
        Me.Controls.Add(Me.Aceptar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dFecha)
        Me.Name = "FechaFactura"
        Me.Text = "Fecha facturación"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label1 As Label
    Friend WithEvents Aceptar As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Cancelar As Button
End Class
