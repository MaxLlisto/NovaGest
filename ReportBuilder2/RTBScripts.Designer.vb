<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.RtbScript = New System.Windows.Forms.RichTextBox()
        Me.Guardar = New System.Windows.Forms.Button()
        Me.Cancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'RtbScript
        '
        Me.RtbScript.AcceptsTab = True
        Me.RtbScript.Location = New System.Drawing.Point(12, 45)
        Me.RtbScript.Name = "RtbScript"
        Me.RtbScript.Size = New System.Drawing.Size(776, 336)
        Me.RtbScript.TabIndex = 1
        Me.RtbScript.Text = ""
        '
        'Guardar
        '
        Me.Guardar.BackgroundImage = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.Guardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guardar.Location = New System.Drawing.Point(678, 393)
        Me.Guardar.Name = "Guardar"
        Me.Guardar.Size = New System.Drawing.Size(50, 47)
        Me.Guardar.TabIndex = 2
        Me.Guardar.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.Guardar.UseVisualStyleBackColor = True
        '
        'Cancelar
        '
        Me.Cancelar.BackgroundImage = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable
        Me.Cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancelar.Location = New System.Drawing.Point(743, 393)
        Me.Cancelar.Name = "Cancelar"
        Me.Cancelar.Size = New System.Drawing.Size(45, 47)
        Me.Cancelar.TabIndex = 3
        Me.Cancelar.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Cancelar)
        Me.Controls.Add(Me.Guardar)
        Me.Controls.Add(Me.RtbScript)
        Me.Name = "Form1"
        Me.Text = "Editor de Scripts"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RtbScript As RichTextBox
    Friend WithEvents Guardar As Button
    Friend WithEvents Cancelar As Button

    Private Sub Guardar_Click(sender As Object, e As EventArgs) Handles Guardar.Click

    End Sub
End Class
