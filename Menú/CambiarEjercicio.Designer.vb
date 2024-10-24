<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CambiarEjercicio
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtEntrar = New System.Windows.Forms.Button()
        Me.cbEjercicio = New System.Windows.Forms.ComboBox()
        Me.cbEmpresa = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.BtEntrar)
        Me.Panel1.Controls.Add(Me.cbEjercicio)
        Me.Panel1.Controls.Add(Me.cbEmpresa)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(437, 290)
        Me.Panel1.TabIndex = 13
        Me.Panel1.Visible = False
        '
        'BtEntrar
        '
        Me.BtEntrar.Location = New System.Drawing.Point(133, 235)
        Me.BtEntrar.Name = "BtEntrar"
        Me.BtEntrar.Size = New System.Drawing.Size(140, 31)
        Me.BtEntrar.TabIndex = 12
        Me.BtEntrar.Text = "Entrar"
        Me.BtEntrar.UseVisualStyleBackColor = True
        '
        'cbEjercicio
        '
        Me.cbEjercicio.FormattingEnabled = True
        Me.cbEjercicio.Location = New System.Drawing.Point(119, 181)
        Me.cbEjercicio.Name = "cbEjercicio"
        Me.cbEjercicio.Size = New System.Drawing.Size(136, 21)
        Me.cbEjercicio.TabIndex = 11
        '
        'cbEmpresa
        '
        Me.cbEmpresa.FormattingEnabled = True
        Me.cbEmpresa.Location = New System.Drawing.Point(119, 136)
        Me.cbEmpresa.Name = "cbEmpresa"
        Me.cbEmpresa.Size = New System.Drawing.Size(269, 21)
        Me.cbEmpresa.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 20)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Ejercicio.:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 137)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Empresa.:"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Menugeneral.My.Resources.Resources.logo_lliria
        Me.PictureBox2.Location = New System.Drawing.Point(119, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(183, 92)
        Me.PictureBox2.TabIndex = 13
        Me.PictureBox2.TabStop = False
        '
        'CambiarEjercicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 290)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "CambiarEjercicio"
        Me.Text = "Cambio de empresa/Ejercicio"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents BtEntrar As Button
    Friend WithEvents cbEjercicio As ComboBox
    Friend WithEvents cbEmpresa As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
End Class
