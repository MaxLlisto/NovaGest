<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Inicio
    Inherits libcomunes.FormGenerico
    'Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtUsuario = New System.Windows.Forms.TextBox()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.Entrar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbEmpresa = New System.Windows.Forms.ComboBox()
        Me.cbEjercicio = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.BtEntrar = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ver = New System.Windows.Forms.Button()
        Me.Nover = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 304)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario.:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 398)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(213, 37)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Contraseña.:"
        '
        'TxtUsuario
        '
        Me.TxtUsuario.Location = New System.Drawing.Point(256, 308)
        Me.TxtUsuario.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.TxtUsuario.Name = "TxtUsuario"
        Me.TxtUsuario.Size = New System.Drawing.Size(268, 31)
        Me.TxtUsuario.TabIndex = 2
        '
        'TxtPassword
        '
        Me.TxtPassword.Location = New System.Drawing.Point(256, 402)
        Me.TxtPassword.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.Size = New System.Drawing.Size(268, 31)
        Me.TxtPassword.TabIndex = 3
        Me.TxtPassword.UseSystemPasswordChar = True
        '
        'Entrar
        '
        Me.Entrar.Location = New System.Drawing.Point(266, 485)
        Me.Entrar.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Entrar.Name = "Entrar"
        Me.Entrar.Size = New System.Drawing.Size(198, 63)
        Me.Entrar.TabIndex = 7
        Me.Entrar.Text = "Entrar"
        Me.Entrar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(46, 263)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(172, 37)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Empresa.:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(52, 350)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(166, 37)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Ejercicio.:"
        '
        'cbEmpresa
        '
        Me.cbEmpresa.FormattingEnabled = True
        Me.cbEmpresa.Location = New System.Drawing.Point(238, 262)
        Me.cbEmpresa.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cbEmpresa.Name = "cbEmpresa"
        Me.cbEmpresa.Size = New System.Drawing.Size(534, 33)
        Me.cbEmpresa.TabIndex = 10
        '
        'cbEjercicio
        '
        Me.cbEjercicio.FormattingEnabled = True
        Me.cbEjercicio.Location = New System.Drawing.Point(238, 348)
        Me.cbEjercicio.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cbEjercicio.Name = "cbEjercicio"
        Me.cbEjercicio.Size = New System.Drawing.Size(268, 33)
        Me.cbEjercicio.TabIndex = 11
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
        Me.Panel1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 1238)
        Me.Panel1.TabIndex = 12
        Me.Panel1.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Menugeneral.My.Resources.Resources.logo_lliria
        Me.PictureBox2.Location = New System.Drawing.Point(238, 23)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(366, 177)
        Me.PictureBox2.TabIndex = 13
        Me.PictureBox2.TabStop = False
        '
        'BtEntrar
        '
        Me.BtEntrar.Enabled = False
        Me.BtEntrar.Location = New System.Drawing.Point(266, 452)
        Me.BtEntrar.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.BtEntrar.Name = "BtEntrar"
        Me.BtEntrar.Size = New System.Drawing.Size(280, 60)
        Me.BtEntrar.TabIndex = 12
        Me.BtEntrar.Text = "Entrar"
        Me.BtEntrar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Menugeneral.My.Resources.Resources.usuario
        Me.PictureBox1.Location = New System.Drawing.Point(256, 23)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(228, 212)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'ver
        '
        Me.ver.Image = Global.Menugeneral.My.Resources.Resources.icover
        Me.ver.Location = New System.Drawing.Point(540, 402)
        Me.ver.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ver.Name = "ver"
        Me.ver.Size = New System.Drawing.Size(64, 38)
        Me.ver.TabIndex = 5
        Me.ver.UseVisualStyleBackColor = True
        '
        'Nover
        '
        Me.Nover.Image = Global.Menugeneral.My.Resources.Resources.iconover
        Me.Nover.Location = New System.Drawing.Point(540, 402)
        Me.Nover.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Nover.Name = "Nover"
        Me.Nover.Size = New System.Drawing.Size(64, 38)
        Me.Nover.TabIndex = 4
        Me.Nover.UseVisualStyleBackColor = True
        Me.Nover.Visible = False
        '
        'Inicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1366, 1238)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Entrar)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ver)
        Me.Controls.Add(Me.Nover)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtUsuario)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "Inicio"
        Me.Text = "Inicio"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtUsuario As TextBox
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents Nover As Button
    Friend WithEvents ver As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Entrar As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cbEmpresa As ComboBox
    Friend WithEvents cbEjercicio As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BtEntrar As Button
    Friend WithEvents PictureBox2 As PictureBox
End Class
