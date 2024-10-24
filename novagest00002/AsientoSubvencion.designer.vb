<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AsientoSubvencion
    Inherits libcomunes.FormGenerico

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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtEmpresa = New Textboxbotoncontrol.TextBoxButton()
        Me.bAsiento = New System.Windows.Forms.Button()
        Me.TxtEjercicio = New Textboxbotoncontrol.TextBoxButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtDiario = New System.Windows.Forms.ComboBox()
        Me.TxtPeriodo = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.lblEjercicio = New System.Windows.Forms.Label()
        Me.TxtDesdeFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.TxtHastaFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.TxtFecha_asiento = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Txtbien = New Textboxbotoncontrol.TextBoxButton()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Empresa"
        '
        'TxtEmpresa
        '
        Me.TxtEmpresa.ButtonImage = Nothing
        Me.TxtEmpresa.Location = New System.Drawing.Point(169, 30)
        Me.TxtEmpresa.Name = "TxtEmpresa"
        Me.TxtEmpresa.Size = New System.Drawing.Size(92, 20)
        Me.TxtEmpresa.TabIndex = 1
        '
        'bAsiento
        '
        Me.bAsiento.Location = New System.Drawing.Point(9, 365)
        Me.bAsiento.Name = "bAsiento"
        Me.bAsiento.Size = New System.Drawing.Size(161, 23)
        Me.bAsiento.TabIndex = 9
        Me.bAsiento.Text = "Hacer asiento"
        Me.bAsiento.UseVisualStyleBackColor = True
        '
        'TxtEjercicio
        '
        Me.TxtEjercicio.ButtonImage = Nothing
        Me.TxtEjercicio.Location = New System.Drawing.Point(169, 71)
        Me.TxtEjercicio.Name = "TxtEjercicio"
        Me.TxtEjercicio.Size = New System.Drawing.Size(92, 20)
        Me.TxtEjercicio.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Ejercicio"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Desde la fecha"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Hasta la fecha"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 190)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Fecha del asiento"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 283)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Diario"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 323)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Periodo"
        '
        'TxtDiario
        '
        Me.TxtDiario.FormattingEnabled = True
        Me.TxtDiario.Location = New System.Drawing.Point(169, 274)
        Me.TxtDiario.Name = "TxtDiario"
        Me.TxtDiario.Size = New System.Drawing.Size(228, 21)
        Me.TxtDiario.TabIndex = 7
        '
        'TxtPeriodo
        '
        Me.TxtPeriodo.FormattingEnabled = True
        Me.TxtPeriodo.Location = New System.Drawing.Point(169, 315)
        Me.TxtPeriodo.Name = "TxtPeriodo"
        Me.TxtPeriodo.Size = New System.Drawing.Size(228, 21)
        Me.TxtPeriodo.TabIndex = 8
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(200, 365)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Salir"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AutoSize = True
        Me.lblEmpresa.Location = New System.Drawing.Point(267, 37)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(10, 13)
        Me.lblEmpresa.TabIndex = 11
        Me.lblEmpresa.Text = "-"
        '
        'lblEjercicio
        '
        Me.lblEjercicio.AutoSize = True
        Me.lblEjercicio.Location = New System.Drawing.Point(267, 71)
        Me.lblEjercicio.Name = "lblEjercicio"
        Me.lblEjercicio.Size = New System.Drawing.Size(10, 13)
        Me.lblEjercicio.TabIndex = 12
        Me.lblEjercicio.Text = "-"
        '
        'TxtDesdeFecha
        '
        Me.TxtDesdeFecha.ButtonImage = Nothing
        Me.TxtDesdeFecha.FormatoFecha = "dd/MM/yyyy"
        Me.TxtDesdeFecha.Location = New System.Drawing.Point(169, 107)
        Me.TxtDesdeFecha.Name = "TxtDesdeFecha"
        Me.TxtDesdeFecha.Size = New System.Drawing.Size(100, 20)
        Me.TxtDesdeFecha.TabIndex = 3
        Me.TxtDesdeFecha.Text = "26/09/2022"
        Me.TxtDesdeFecha.value = New Date(2022, 9, 26, 0, 0, 0, 0)
        '
        'TxtHastaFecha
        '
        Me.TxtHastaFecha.ButtonImage = Nothing
        Me.TxtHastaFecha.FormatoFecha = "dd/MM/yyyy"
        Me.TxtHastaFecha.Location = New System.Drawing.Point(169, 142)
        Me.TxtHastaFecha.Name = "TxtHastaFecha"
        Me.TxtHastaFecha.Size = New System.Drawing.Size(100, 20)
        Me.TxtHastaFecha.TabIndex = 4
        Me.TxtHastaFecha.Text = "26/09/2022"
        Me.TxtHastaFecha.value = New Date(2022, 9, 26, 0, 0, 0, 0)
        '
        'TxtFecha_asiento
        '
        Me.TxtFecha_asiento.ButtonImage = Nothing
        Me.TxtFecha_asiento.FormatoFecha = "dd/MM/yyyy"
        Me.TxtFecha_asiento.Location = New System.Drawing.Point(169, 182)
        Me.TxtFecha_asiento.Name = "TxtFecha_asiento"
        Me.TxtFecha_asiento.Size = New System.Drawing.Size(100, 20)
        Me.TxtFecha_asiento.TabIndex = 5
        Me.TxtFecha_asiento.Text = "26/09/2022"
        Me.TxtFecha_asiento.value = New Date(2022, 9, 26, 0, 0, 0, 0)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(27, 236)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Código bien"
        '
        'Txtbien
        '
        Me.Txtbien.ButtonImage = Nothing
        Me.Txtbien.Location = New System.Drawing.Point(169, 229)
        Me.Txtbien.Name = "Txtbien"
        Me.Txtbien.Size = New System.Drawing.Size(92, 20)
        Me.Txtbien.TabIndex = 6
        '
        'AsientoSubvencion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 400)
        Me.Controls.Add(Me.Txtbien)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtFecha_asiento)
        Me.Controls.Add(Me.TxtHastaFecha)
        Me.Controls.Add(Me.TxtDesdeFecha)
        Me.Controls.Add(Me.lblEjercicio)
        Me.Controls.Add(Me.lblEmpresa)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TxtPeriodo)
        Me.Controls.Add(Me.TxtDiario)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtEjercicio)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.bAsiento)
        Me.Controls.Add(Me.TxtEmpresa)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AsientoSubvencion"
        Me.Text = "Asiento de amortizaciones"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TxtEmpresa As Textboxbotoncontrol.TextBoxButton
    Friend WithEvents bAsiento As Button
    Friend WithEvents TxtEjercicio As Textboxbotoncontrol.TextBoxButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtDiario As ComboBox
    Friend WithEvents TxtPeriodo As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents lblEmpresa As Label
    Friend WithEvents lblEjercicio As Label
    Friend WithEvents TxtDesdeFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents TxtHastaFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents TxtFecha_asiento As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label8 As Label
    Friend WithEvents Txtbien As Textboxbotoncontrol.TextBoxButton
End Class
