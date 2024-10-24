<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmguardarcomo
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
        Me.button2 = New System.Windows.Forms.Button()
        Me.button3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtFormato = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'button2
        '
        Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.button2.Location = New System.Drawing.Point(346, 77)
        Me.button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(91, 35)
        Me.button2.TabIndex = 1
        Me.button2.Text = "Correcto"
        Me.button2.UseVisualStyleBackColor = False
        '
        'button3
        '
        Me.button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.button3.Location = New System.Drawing.Point(451, 77)
        Me.button3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(92, 35)
        Me.button3.TabIndex = 2
        Me.button3.Text = "Cancelar"
        Me.button3.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(171, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nombre archivo: "
        '
        'TxtFormato
        '
        Me.TxtFormato.Location = New System.Drawing.Point(209, 40)
        Me.TxtFormato.MaxLength = 50
        Me.TxtFormato.Name = "TxtFormato"
        Me.TxtFormato.Size = New System.Drawing.Size(334, 20)
        Me.TxtFormato.TabIndex = 4
        '
        'Frmguardarcomo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 124)
        Me.Controls.Add(Me.TxtFormato)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button3)
        Me.Name = "Frmguardarcomo"
        Me.Text = "Guardar como"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents button2 As Button
    Public WithEvents button3 As Button
    Friend WithEvents Label1 As Label
    Public WithEvents TxtFormato As TextBox
End Class
