<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSeleccionFormato
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
        Me.cboFormatos = New System.Windows.Forms.ComboBox()
        Me.ok = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cboFormatos
        '
        Me.cboFormatos.FormattingEnabled = True
        Me.cboFormatos.Location = New System.Drawing.Point(12, 12)
        Me.cboFormatos.Name = "cboFormatos"
        Me.cboFormatos.Size = New System.Drawing.Size(417, 21)
        Me.cboFormatos.TabIndex = 0
        '
        'ok
        '
        Me.ok.Image = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.ok.Location = New System.Drawing.Point(435, 10)
        Me.ok.Name = "ok"
        Me.ok.Size = New System.Drawing.Size(61, 56)
        Me.ok.TabIndex = 1
        Me.ok.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable
        Me.Button1.Location = New System.Drawing.Point(435, 72)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 56)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FrmSeleccionFormato
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 227)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ok)
        Me.Controls.Add(Me.cboFormatos)
        Me.Name = "FrmSeleccionFormato"
        Me.Text = "Selección de formato"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cboFormatos As ComboBox
    Friend WithEvents ok As Button
    Friend WithEvents Button1 As Button
End Class
