<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Accesospersonal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Accesospersonal))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Horainicial = New System.Windows.Forms.DateTimePicker()
        Me.DiaInicio = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Horafinal = New System.Windows.Forms.DateTimePicker()
        Me.Diafinal = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ReescribirFichadas = New System.Windows.Forms.CheckBox()
        Me.GrabarBiometricos = New System.Windows.Forms.CheckBox()
        Me.AjustesTarde = New System.Windows.Forms.CheckBox()
        Me.AjustesManana = New System.Windows.Forms.CheckBox()
        Me.Paradatarde = New System.Windows.Forms.CheckBox()
        Me.ParadaManana = New System.Windows.Forms.CheckBox()
        Me.Procesar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SoloOperario = New System.Windows.Forms.TextBox()
        Me.empresas = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Salir = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ProcesarVariosDias = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Horainicial)
        Me.GroupBox1.Controls.Add(Me.DiaInicio)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(495, 68)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Inicio turno"
        '
        'Horainicial
        '
        Me.Horainicial.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.Horainicial.Location = New System.Drawing.Point(370, 25)
        Me.Horainicial.Name = "Horainicial"
        Me.Horainicial.Size = New System.Drawing.Size(101, 20)
        Me.Horainicial.TabIndex = 3
        '
        'DiaInicio
        '
        Me.DiaInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DiaInicio.Location = New System.Drawing.Point(96, 23)
        Me.DiaInicio.Name = "DiaInicio"
        Me.DiaInicio.Size = New System.Drawing.Size(101, 20)
        Me.DiaInicio.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(293, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "desde la hora"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Desde el día"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Horafinal)
        Me.GroupBox2.Controls.Add(Me.Diafinal)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 84)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(495, 54)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Final de turno"
        '
        'Horafinal
        '
        Me.Horafinal.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.Horafinal.Location = New System.Drawing.Point(370, 22)
        Me.Horafinal.Name = "Horafinal"
        Me.Horafinal.Size = New System.Drawing.Size(101, 20)
        Me.Horafinal.TabIndex = 3
        '
        'Diafinal
        '
        Me.Diafinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Diafinal.Location = New System.Drawing.Point(96, 23)
        Me.Diafinal.Name = "Diafinal"
        Me.Diafinal.Size = New System.Drawing.Size(101, 20)
        Me.Diafinal.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(293, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "desde la hora"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 29)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Desde el día"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ReescribirFichadas)
        Me.GroupBox3.Controls.Add(Me.GrabarBiometricos)
        Me.GroupBox3.Controls.Add(Me.AjustesTarde)
        Me.GroupBox3.Controls.Add(Me.AjustesManana)
        Me.GroupBox3.Controls.Add(Me.Paradatarde)
        Me.GroupBox3.Controls.Add(Me.ParadaManana)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 144)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(495, 87)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Parada mañana"
        '
        'ReescribirFichadas
        '
        Me.ReescribirFichadas.AutoSize = True
        Me.ReescribirFichadas.Location = New System.Drawing.Point(299, 41)
        Me.ReescribirFichadas.Name = "ReescribirFichadas"
        Me.ReescribirFichadas.Size = New System.Drawing.Size(195, 17)
        Me.ReescribirFichadas.TabIndex = 5
        Me.ReescribirFichadas.Text = "Reescribir fichadas proceso anterior"
        Me.ReescribirFichadas.UseVisualStyleBackColor = True
        '
        'GrabarBiometricos
        '
        Me.GrabarBiometricos.AutoSize = True
        Me.GrabarBiometricos.Checked = True
        Me.GrabarBiometricos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.GrabarBiometricos.Location = New System.Drawing.Point(299, 19)
        Me.GrabarBiometricos.Name = "GrabarBiometricos"
        Me.GrabarBiometricos.Size = New System.Drawing.Size(114, 17)
        Me.GrabarBiometricos.TabIndex = 4
        Me.GrabarBiometricos.Text = "Grabar biométricos"
        Me.GrabarBiometricos.UseVisualStyleBackColor = True
        '
        'AjustesTarde
        '
        Me.AjustesTarde.AutoSize = True
        Me.AjustesTarde.Checked = True
        Me.AjustesTarde.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AjustesTarde.Location = New System.Drawing.Point(164, 41)
        Me.AjustesTarde.Name = "AjustesTarde"
        Me.AjustesTarde.Size = New System.Drawing.Size(90, 17)
        Me.AjustesTarde.TabIndex = 3
        Me.AjustesTarde.Text = "Ajustes salida"
        Me.AjustesTarde.UseVisualStyleBackColor = True
        '
        'AjustesManana
        '
        Me.AjustesManana.AutoSize = True
        Me.AjustesManana.Checked = True
        Me.AjustesManana.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AjustesManana.Location = New System.Drawing.Point(164, 19)
        Me.AjustesManana.Name = "AjustesManana"
        Me.AjustesManana.Size = New System.Drawing.Size(99, 17)
        Me.AjustesManana.TabIndex = 2
        Me.AjustesManana.Text = "Ajustes entrada"
        Me.AjustesManana.UseVisualStyleBackColor = True
        '
        'Paradatarde
        '
        Me.Paradatarde.AutoSize = True
        Me.Paradatarde.Location = New System.Drawing.Point(22, 41)
        Me.Paradatarde.Name = "Paradatarde"
        Me.Paradatarde.Size = New System.Drawing.Size(87, 17)
        Me.Paradatarde.TabIndex = 1
        Me.Paradatarde.Text = "Parada tarde"
        Me.Paradatarde.UseVisualStyleBackColor = True
        '
        'ParadaManana
        '
        Me.ParadaManana.AutoSize = True
        Me.ParadaManana.Checked = True
        Me.ParadaManana.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ParadaManana.Location = New System.Drawing.Point(22, 19)
        Me.ParadaManana.Name = "ParadaManana"
        Me.ParadaManana.Size = New System.Drawing.Size(101, 17)
        Me.ParadaManana.TabIndex = 0
        Me.ParadaManana.Text = "Parada mañana"
        Me.ParadaManana.UseVisualStyleBackColor = True
        '
        'Procesar
        '
        Me.Procesar.Image = CType(resources.GetObject("Procesar.Image"), System.Drawing.Image)
        Me.Procesar.Location = New System.Drawing.Point(123, 344)
        Me.Procesar.Name = "Procesar"
        Me.Procesar.Size = New System.Drawing.Size(77, 68)
        Me.Procesar.TabIndex = 4
        Me.Procesar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Solo del operario"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(215, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Procesar empresas"
        '
        'SoloOperario
        '
        Me.SoloOperario.Location = New System.Drawing.Point(117, 23)
        Me.SoloOperario.Name = "SoloOperario"
        Me.SoloOperario.Size = New System.Drawing.Size(77, 20)
        Me.SoloOperario.TabIndex = 2
        '
        'empresas
        '
        Me.empresas.Location = New System.Drawing.Point(310, 23)
        Me.empresas.Name = "empresas"
        Me.empresas.Size = New System.Drawing.Size(172, 20)
        Me.empresas.TabIndex = 3
        Me.empresas.Text = "1"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.empresas)
        Me.GroupBox4.Controls.Add(Me.SoloOperario)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Location = New System.Drawing.Point(10, 237)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(495, 58)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Filtros"
        '
        'Salir
        '
        Me.Salir.Image = CType(resources.GetObject("Salir.Image"), System.Drawing.Image)
        Me.Salir.Location = New System.Drawing.Point(260, 344)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(77, 68)
        Me.Salir.TabIndex = 6
        Me.Salir.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ProcesarVariosDias)
        Me.GroupBox5.Location = New System.Drawing.Point(14, 301)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(489, 37)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        '
        'ProcesarVariosDias
        '
        Me.ProcesarVariosDias.AutoSize = True
        Me.ProcesarVariosDias.Location = New System.Drawing.Point(6, 14)
        Me.ProcesarVariosDias.Name = "ProcesarVariosDias"
        Me.ProcesarVariosDias.Size = New System.Drawing.Size(142, 17)
        Me.ProcesarVariosDias.TabIndex = 0
        Me.ProcesarVariosDias.Text = "Procesar días en bloque"
        Me.ProcesarVariosDias.UseVisualStyleBackColor = True
        '
        'Accesospersonal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 418)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Procesar)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Accesospersonal"
        Me.Text = "Control de accesos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Horainicial As DateTimePicker
    Friend WithEvents DiaInicio As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Horafinal As DateTimePicker
    Friend WithEvents Diafinal As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents ReescribirFichadas As CheckBox
    Friend WithEvents GrabarBiometricos As CheckBox
    Friend WithEvents AjustesTarde As CheckBox
    Friend WithEvents AjustesManana As CheckBox
    Friend WithEvents Paradatarde As CheckBox
    Friend WithEvents ParadaManana As CheckBox
    Friend WithEvents Procesar As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents SoloOperario As TextBox
    Friend WithEvents empresas As TextBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Salir As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents ProcesarVariosDias As CheckBox
End Class
