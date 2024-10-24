<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmErrores
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
        Me.TxtErrores = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TxtErrores
        '
        Me.TxtErrores.Location = New System.Drawing.Point(12, 12)
        Me.TxtErrores.Multiline = True
        Me.TxtErrores.Name = "TxtErrores"
        Me.TxtErrores.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtErrores.Size = New System.Drawing.Size(776, 426)
        Me.TxtErrores.TabIndex = 0
        '
        'FrmErrores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TxtErrores)
        Me.Name = "FrmErrores"
        Me.Text = "Errores"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtErrores As TextBox
End Class
