<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDuplicarOC
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
        Me.DTFechaOrden = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Aceptaryseguir = New System.Windows.Forms.Button()
        Me.Aceptaryver = New System.Windows.Forms.Button()
        Me.cmdSalir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'DTFechaOrden
        '
        Me.DTFechaOrden.ButtonImage = Nothing
        Me.DTFechaOrden.FormatoFecha = "dd/MM/yyyy"
        Me.DTFechaOrden.Location = New System.Drawing.Point(159, 9)
        Me.DTFechaOrden.Name = "DTFechaOrden"
        Me.DTFechaOrden.Size = New System.Drawing.Size(100, 20)
        Me.DTFechaOrden.TabIndex = 0
        Me.DTFechaOrden.Text = "18/07/2023"
        Me.DTFechaOrden.value = New Date(2023, 7, 18, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha nueva orden:"
        '
        'Aceptaryseguir
        '
        Me.Aceptaryseguir.Location = New System.Drawing.Point(13, 49)
        Me.Aceptaryseguir.Name = "Aceptaryseguir"
        Me.Aceptaryseguir.Size = New System.Drawing.Size(104, 41)
        Me.Aceptaryseguir.TabIndex = 2
        Me.Aceptaryseguir.Text = "Duplicar y seguir"
        Me.Aceptaryseguir.UseVisualStyleBackColor = True
        '
        'Aceptaryver
        '
        Me.Aceptaryver.Location = New System.Drawing.Point(123, 49)
        Me.Aceptaryver.Name = "Aceptaryver"
        Me.Aceptaryver.Size = New System.Drawing.Size(104, 41)
        Me.Aceptaryver.TabIndex = 3
        Me.Aceptaryver.Text = "Duplicar y ver"
        Me.Aceptaryver.UseVisualStyleBackColor = True
        '
        'cmdSalir
        '
        Me.cmdSalir.BackgroundImage = Global.novagestConfec.My.Resources.Resources.salirpersona1
        Me.cmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdSalir.Location = New System.Drawing.Point(281, 49)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(46, 41)
        Me.cmdSalir.TabIndex = 4
        Me.cmdSalir.UseVisualStyleBackColor = True
        '
        'FrmDuplicarOC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 94)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.Aceptaryver)
        Me.Controls.Add(Me.Aceptaryseguir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DTFechaOrden)
        Me.Name = "FrmDuplicarOC"
        Me.Text = "FrmDuplicarOC"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DTFechaOrden As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label1 As Label
    Friend WithEvents Aceptaryseguir As Button
    Friend WithEvents Aceptaryver As Button
    Friend WithEvents cmdSalir As Button
End Class
