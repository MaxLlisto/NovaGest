<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCargosAbonosLLIRP
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
        Me.TabPrincipal = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CmdImprimirCargos = New System.Windows.Forms.Button()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtCuantosabonos = New System.Windows.Forms.TextBox()
        Me.TxtCuantosCargos = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DTFechaEnvio = New Textboxbotoncontrol.TextBoxCalendar()
        Me.DTFechaEmision = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TabPrincipal.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPrincipal
        '
        Me.TabPrincipal.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabPrincipal.Controls.Add(Me.TabPage1)
        Me.TabPrincipal.Controls.Add(Me.TabPage2)
        Me.TabPrincipal.Controls.Add(Me.TabPage3)
        Me.TabPrincipal.Location = New System.Drawing.Point(12, 1)
        Me.TabPrincipal.Name = "TabPrincipal"
        Me.TabPrincipal.SelectedIndex = 0
        Me.TabPrincipal.Size = New System.Drawing.Size(945, 705)
        Me.TabPrincipal.TabIndex = 10
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button4)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.DTFechaEmision)
        Me.TabPage1.Controls.Add(Me.DTFechaEnvio)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TxtCuantosabonos)
        Me.TabPage1.Controls.Add(Me.TxtCuantosCargos)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.pb)
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.CmdImprimirCargos)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(937, 679)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Datos ficheros"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(192, 74)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Cargos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(514, 144)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Abonos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.novagest00002.My.Resources.Resources.impresoraabonos
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(798, 450)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(126, 98)
        Me.Button1.TabIndex = 11
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CmdImprimirCargos
        '
        Me.CmdImprimirCargos.BackgroundImage = Global.novagest00002.My.Resources.Resources.impresoraalcargos
        Me.CmdImprimirCargos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdImprimirCargos.Location = New System.Drawing.Point(666, 450)
        Me.CmdImprimirCargos.Name = "CmdImprimirCargos"
        Me.CmdImprimirCargos.Size = New System.Drawing.Size(126, 98)
        Me.CmdImprimirCargos.TabIndex = 10
        Me.CmdImprimirCargos.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(12, 428)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(912, 16)
        Me.pb.TabIndex = 16
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(477, 356)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(447, 68)
        Me.Panel4.TabIndex = 15
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(12, 354)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(447, 68)
        Me.Panel3.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(477, 87)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(447, 263)
        Me.Panel2.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(12, 87)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(447, 261)
        Me.Panel1.TabIndex = 12
        '
        'TxtCuantosabonos
        '
        Me.TxtCuantosabonos.Location = New System.Drawing.Point(542, 55)
        Me.TxtCuantosabonos.Name = "TxtCuantosabonos"
        Me.TxtCuantosabonos.Size = New System.Drawing.Size(100, 20)
        Me.TxtCuantosabonos.TabIndex = 20
        '
        'TxtCuantosCargos
        '
        Me.TxtCuantosCargos.Location = New System.Drawing.Point(357, 53)
        Me.TxtCuantosCargos.Name = "TxtCuantosCargos"
        Me.TxtCuantosCargos.Size = New System.Drawing.Size(100, 20)
        Me.TxtCuantosCargos.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(218, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(499, 20)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "La tabla contiene                              cargos y                          " &
    "       abonos."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(248, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(458, 24)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "GRABACIÓN FICHERO DE VARGOS / ABONOS"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(387, 453)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Fecha de envío"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(387, 486)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Fecha de emisión"
        '
        'DTFechaEnvio
        '
        Me.DTFechaEnvio.ButtonImage = Nothing
        Me.DTFechaEnvio.FormatoFecha = "dd/MM/yyyy"
        Me.DTFechaEnvio.Location = New System.Drawing.Point(501, 450)
        Me.DTFechaEnvio.Name = "DTFechaEnvio"
        Me.DTFechaEnvio.Size = New System.Drawing.Size(141, 20)
        Me.DTFechaEnvio.TabIndex = 23
        '
        'DTFechaEmision
        '
        Me.DTFechaEmision.ButtonImage = Nothing
        Me.DTFechaEmision.FormatoFecha = "dd/MM/yyyy"
        Me.DTFechaEmision.Location = New System.Drawing.Point(501, 483)
        Me.DTFechaEmision.Name = "DTFechaEmision"
        Me.DTFechaEmision.Size = New System.Drawing.Size(141, 20)
        Me.DTFechaEmision.TabIndex = 24
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(28, 453)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 25
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(28, 498)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 26
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.BackgroundImage = Global.novagest00002.My.Resources.Resources.sEPAXML1
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Location = New System.Drawing.Point(390, 522)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(126, 119)
        Me.Button4.TabIndex = 27
        Me.Button4.UseVisualStyleBackColor = True
        '
        'FrmCargosAbonosLLIRP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 711)
        Me.Controls.Add(Me.TabPrincipal)
        Me.Name = "FrmCargosAbonosLLIRP"
        Me.Text = "Emisión de remesas de cargo y de abono"
        Me.TabPrincipal.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPrincipal As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents DTFechaEmision As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents DTFechaEnvio As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtCuantosabonos As TextBox
    Friend WithEvents TxtCuantosCargos As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents pb As ProgressBar
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents CmdImprimirCargos As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
End Class
