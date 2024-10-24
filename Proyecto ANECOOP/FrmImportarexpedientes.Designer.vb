<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImportarexpedientes
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
        Me.txtEjercicio = New System.Windows.Forms.TextBox()
        Me.TxtDesdelafecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.TxtHastalafecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.lblTrabajos = New System.Windows.Forms.Label()
        Me.Acciones = New System.Windows.Forms.GroupBox()
        Me.Todos = New System.Windows.Forms.RadioButton()
        Me.Descargados = New System.Windows.Forms.RadioButton()
        Me.Nuevos = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.importar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.bp = New System.Windows.Forms.ProgressBar()
        Me.cbImportarExpedientes = New System.Windows.Forms.CheckBox()
        Me.cbImportarCobrosypagos = New System.Windows.Forms.CheckBox()
        Me.Acciones.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtEjercicio
        '
        Me.txtEjercicio.Location = New System.Drawing.Point(129, 12)
        Me.txtEjercicio.Name = "txtEjercicio"
        Me.txtEjercicio.Size = New System.Drawing.Size(100, 20)
        Me.txtEjercicio.TabIndex = 0
        '
        'TxtDesdelafecha
        '
        Me.TxtDesdelafecha.ButtonImage = Nothing
        Me.TxtDesdelafecha.FormatoFecha = "dd/MM/yyyy"
        Me.TxtDesdelafecha.Location = New System.Drawing.Point(129, 50)
        Me.TxtDesdelafecha.Name = "TxtDesdelafecha"
        Me.TxtDesdelafecha.Size = New System.Drawing.Size(100, 20)
        Me.TxtDesdelafecha.TabIndex = 2
        Me.TxtDesdelafecha.Text = "10/05/2024"
        Me.TxtDesdelafecha.value = New Date(2024, 5, 10, 0, 0, 0, 0)
        '
        'TxtHastalafecha
        '
        Me.TxtHastalafecha.ButtonImage = Nothing
        Me.TxtHastalafecha.FormatoFecha = "dd/MM/yyyy"
        Me.TxtHastalafecha.Location = New System.Drawing.Point(129, 88)
        Me.TxtHastalafecha.Name = "TxtHastalafecha"
        Me.TxtHastalafecha.Size = New System.Drawing.Size(100, 20)
        Me.TxtHastalafecha.TabIndex = 3
        Me.TxtHastalafecha.Text = "10/05/2024"
        Me.TxtHastalafecha.value = New Date(2024, 5, 10, 0, 0, 0, 0)
        '
        'lblTrabajos
        '
        Me.lblTrabajos.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblTrabajos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTrabajos.Location = New System.Drawing.Point(12, 122)
        Me.lblTrabajos.Name = "lblTrabajos"
        Me.lblTrabajos.Size = New System.Drawing.Size(425, 63)
        Me.lblTrabajos.TabIndex = 4
        Me.lblTrabajos.Text = "Sin actividad"
        '
        'Acciones
        '
        Me.Acciones.Controls.Add(Me.Todos)
        Me.Acciones.Controls.Add(Me.Descargados)
        Me.Acciones.Controls.Add(Me.Nuevos)
        Me.Acciones.Location = New System.Drawing.Point(12, 256)
        Me.Acciones.Name = "Acciones"
        Me.Acciones.Size = New System.Drawing.Size(425, 51)
        Me.Acciones.TabIndex = 5
        Me.Acciones.TabStop = False
        Me.Acciones.Text = "Selección"
        '
        'Todos
        '
        Me.Todos.AutoSize = True
        Me.Todos.Checked = True
        Me.Todos.Location = New System.Drawing.Point(325, 19)
        Me.Todos.Name = "Todos"
        Me.Todos.Size = New System.Drawing.Size(55, 17)
        Me.Todos.TabIndex = 2
        Me.Todos.TabStop = True
        Me.Todos.Text = "Todos"
        Me.Todos.UseVisualStyleBackColor = True
        '
        'Descargados
        '
        Me.Descargados.AutoSize = True
        Me.Descargados.Location = New System.Drawing.Point(165, 19)
        Me.Descargados.Name = "Descargados"
        Me.Descargados.Size = New System.Drawing.Size(88, 17)
        Me.Descargados.TabIndex = 1
        Me.Descargados.Text = "Descargados"
        Me.Descargados.UseVisualStyleBackColor = True
        '
        'Nuevos
        '
        Me.Nuevos.AutoSize = True
        Me.Nuevos.Location = New System.Drawing.Point(31, 19)
        Me.Nuevos.Name = "Nuevos"
        Me.Nuevos.Size = New System.Drawing.Size(62, 17)
        Me.Nuevos.TabIndex = 0
        Me.Nuevos.Text = "Nuevos"
        Me.Nuevos.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(76, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Ejercicio"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Desde la fecha"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(47, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Hasta la fecha"
        '
        'importar
        '
        Me.importar.BackgroundImage = Global.Proyanecoop.My.Resources.Resources.Importar_expedientes
        Me.importar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.importar.Location = New System.Drawing.Point(54, 324)
        Me.importar.Name = "importar"
        Me.importar.Size = New System.Drawing.Size(75, 70)
        Me.importar.TabIndex = 9
        Me.importar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.Proyanecoop.My.Resources.Resources.salir1
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(316, 324)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(75, 70)
        Me.Salir.TabIndex = 10
        Me.Salir.UseVisualStyleBackColor = True
        '
        'bp
        '
        Me.bp.Location = New System.Drawing.Point(12, 188)
        Me.bp.Name = "bp"
        Me.bp.Size = New System.Drawing.Size(425, 10)
        Me.bp.TabIndex = 11
        Me.bp.Visible = False
        '
        'cbImportarExpedientes
        '
        Me.cbImportarExpedientes.AutoSize = True
        Me.cbImportarExpedientes.Checked = True
        Me.cbImportarExpedientes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbImportarExpedientes.Location = New System.Drawing.Point(25, 216)
        Me.cbImportarExpedientes.Name = "cbImportarExpedientes"
        Me.cbImportarExpedientes.Size = New System.Drawing.Size(124, 17)
        Me.cbImportarExpedientes.TabIndex = 12
        Me.cbImportarExpedientes.Text = "Importar expedientes"
        Me.cbImportarExpedientes.UseVisualStyleBackColor = True
        '
        'cbImportarCobrosypagos
        '
        Me.cbImportarCobrosypagos.AutoSize = True
        Me.cbImportarCobrosypagos.Checked = True
        Me.cbImportarCobrosypagos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbImportarCobrosypagos.Location = New System.Drawing.Point(239, 216)
        Me.cbImportarCobrosypagos.Name = "cbImportarCobrosypagos"
        Me.cbImportarCobrosypagos.Size = New System.Drawing.Size(139, 17)
        Me.cbImportarCobrosypagos.TabIndex = 13
        Me.cbImportarCobrosypagos.Text = "Importar cobros y pagos"
        Me.cbImportarCobrosypagos.UseVisualStyleBackColor = True
        '
        'FrmImportarexpedientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 397)
        Me.Controls.Add(Me.cbImportarCobrosypagos)
        Me.Controls.Add(Me.cbImportarExpedientes)
        Me.Controls.Add(Me.bp)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.importar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Acciones)
        Me.Controls.Add(Me.lblTrabajos)
        Me.Controls.Add(Me.TxtHastalafecha)
        Me.Controls.Add(Me.TxtDesdelafecha)
        Me.Controls.Add(Me.txtEjercicio)
        Me.Name = "FrmImportarexpedientes"
        Me.Text = "Importar expedientes anecoop"
        Me.Acciones.ResumeLayout(False)
        Me.Acciones.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtEjercicio As TextBox
    Friend WithEvents TxtDesdelafecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents TxtHastalafecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents lblTrabajos As Label
    Friend WithEvents Acciones As GroupBox
    Friend WithEvents Todos As RadioButton
    Friend WithEvents Descargados As RadioButton
    Friend WithEvents Nuevos As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents importar As Button
    Friend WithEvents Salir As Button
    Friend WithEvents bp As ProgressBar
    Friend WithEvents cbImportarExpedientes As CheckBox
    Friend WithEvents cbImportarCobrosypagos As CheckBox
End Class
