<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeleccionCampos
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
        Me.Campos_tabla = New System.Windows.Forms.ListView()
        Me.Campo0 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Campos_report = New System.Windows.Forms.ListView()
        Me.CampoInf = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.aceptar = New System.Windows.Forms.Button()
        Me.devolver = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.lblSeccion = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cb = New System.Windows.Forms.RadioButton()
        Me.im = New System.Windows.Forms.RadioButton()
        Me.ca = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Campos_tabla
        '
        Me.Campos_tabla.AllowDrop = True
        Me.Campos_tabla.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Campo0})
        Me.Campos_tabla.FullRowSelect = True
        Me.Campos_tabla.GridLines = True
        Me.Campos_tabla.HideSelection = False
        Me.Campos_tabla.Location = New System.Drawing.Point(12, 37)
        Me.Campos_tabla.Name = "Campos_tabla"
        Me.Campos_tabla.Size = New System.Drawing.Size(262, 546)
        Me.Campos_tabla.TabIndex = 6
        Me.Campos_tabla.UseCompatibleStateImageBehavior = False
        Me.Campos_tabla.View = System.Windows.Forms.View.Details
        '
        'Campo0
        '
        Me.Campo0.Text = "Campo"
        Me.Campo0.Width = 230
        '
        'Campos_report
        '
        Me.Campos_report.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.CampoInf})
        Me.Campos_report.GridLines = True
        Me.Campos_report.HideSelection = False
        Me.Campos_report.Location = New System.Drawing.Point(349, 32)
        Me.Campos_report.Name = "Campos_report"
        Me.Campos_report.Size = New System.Drawing.Size(244, 546)
        Me.Campos_report.TabIndex = 7
        Me.Campos_report.UseCompatibleStateImageBehavior = False
        Me.Campos_report.View = System.Windows.Forms.View.Details
        '
        'CampoInf
        '
        Me.CampoInf.Text = "Campo"
        Me.CampoInf.Width = 230
        '
        'aceptar
        '
        Me.aceptar.Image = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.aceptar.Location = New System.Drawing.Point(414, 595)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(80, 43)
        Me.aceptar.TabIndex = 5
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'devolver
        '
        Me.devolver.Image = Global.ReportBuilder2.My.Resources.Resources.dedo_izquiera
        Me.devolver.Location = New System.Drawing.Point(281, 152)
        Me.devolver.Name = "devolver"
        Me.devolver.Size = New System.Drawing.Size(62, 47)
        Me.devolver.TabIndex = 4
        Me.devolver.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.ReportBuilder2.My.Resources.Resources.dedo_derecha1
        Me.Button2.Location = New System.Drawing.Point(281, 99)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(62, 47)
        Me.Button2.TabIndex = 3
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cancelar
        '
        Me.cancelar.Image = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable1
        Me.cancelar.Location = New System.Drawing.Point(500, 595)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(92, 43)
        Me.cancelar.TabIndex = 2
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'lblSeccion
        '
        Me.lblSeccion.AutoSize = True
        Me.lblSeccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeccion.Location = New System.Drawing.Point(252, 9)
        Me.lblSeccion.Name = "lblSeccion"
        Me.lblSeccion.Size = New System.Drawing.Size(139, 20)
        Me.lblSeccion.TabIndex = 17
        Me.lblSeccion.Text = "<nombre sección>"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(168, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Sección:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cb)
        Me.GroupBox1.Controls.Add(Me.im)
        Me.GroupBox1.Controls.Add(Me.ca)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 589)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(262, 56)
        Me.GroupBox1.TabIndex = 136
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Objeto"
        '
        'cb
        '
        Me.cb.AutoSize = True
        Me.cb.Location = New System.Drawing.Point(161, 19)
        Me.cb.Name = "cb"
        Me.cb.Size = New System.Drawing.Size(91, 17)
        Me.cb.TabIndex = 2
        Me.cb.Text = "Código Barras"
        Me.cb.UseVisualStyleBackColor = True
        '
        'im
        '
        Me.im.AutoSize = True
        Me.im.Location = New System.Drawing.Point(82, 19)
        Me.im.Name = "im"
        Me.im.Size = New System.Drawing.Size(60, 17)
        Me.im.TabIndex = 1
        Me.im.Text = "Imagen"
        Me.im.UseVisualStyleBackColor = True
        '
        'ca
        '
        Me.ca.AutoSize = True
        Me.ca.Checked = True
        Me.ca.Location = New System.Drawing.Point(18, 20)
        Me.ca.Name = "ca"
        Me.ca.Size = New System.Drawing.Size(58, 17)
        Me.ca.TabIndex = 0
        Me.ca.TabStop = True
        Me.ca.Text = "Campo"
        Me.ca.UseVisualStyleBackColor = True
        '
        'SeleccionCampos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 648)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblSeccion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Campos_report)
        Me.Controls.Add(Me.Campos_tabla)
        Me.Controls.Add(Me.aceptar)
        Me.Controls.Add(Me.devolver)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cancelar)
        Me.Name = "SeleccionCampos"
        Me.Text = "Selección de campos del informe"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cancelar As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents devolver As Button
    Friend WithEvents aceptar As Button
    Friend WithEvents Campos_tabla As ListView
    Friend WithEvents Campos_report As ListView
    Friend WithEvents lblSeccion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Campo0 As ColumnHeader
    Friend WithEvents CampoInf As ColumnHeader
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cb As RadioButton
    Friend WithEvents im As RadioButton
    Friend WithEvents ca As RadioButton
End Class
