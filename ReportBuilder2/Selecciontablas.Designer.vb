<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Selecciontablas
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
        Me.Campos_report = New System.Windows.Forms.ListView()
        Me.TablaInf = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Campos_tabla = New System.Windows.Forms.ListView()
        Me.Tablas = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.aceptar = New System.Windows.Forms.Button()
        Me.devolver = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSeccion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Campos_report
        '
        Me.Campos_report.AllowDrop = True
        Me.Campos_report.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TablaInf})
        Me.Campos_report.FullRowSelect = True
        Me.Campos_report.GridLines = True
        Me.Campos_report.HideSelection = False
        Me.Campos_report.Location = New System.Drawing.Point(316, 34)
        Me.Campos_report.Name = "Campos_report"
        Me.Campos_report.Size = New System.Drawing.Size(253, 571)
        Me.Campos_report.TabIndex = 13
        Me.Campos_report.UseCompatibleStateImageBehavior = False
        Me.Campos_report.View = System.Windows.Forms.View.Details
        '
        'TablaInf
        '
        Me.TablaInf.Text = "Tabla informe"
        Me.TablaInf.Width = 262
        '
        'Campos_tabla
        '
        Me.Campos_tabla.AllowDrop = True
        Me.Campos_tabla.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Tablas})
        Me.Campos_tabla.FullRowSelect = True
        Me.Campos_tabla.GridLines = True
        Me.Campos_tabla.HideSelection = False
        Me.Campos_tabla.Location = New System.Drawing.Point(15, 34)
        Me.Campos_tabla.Name = "Campos_tabla"
        Me.Campos_tabla.Size = New System.Drawing.Size(227, 571)
        Me.Campos_tabla.TabIndex = 12
        Me.Campos_tabla.UseCompatibleStateImageBehavior = False
        Me.Campos_tabla.View = System.Windows.Forms.View.Details
        '
        'Tablas
        '
        Me.Tablas.Text = "Tablas"
        Me.Tablas.Width = 225
        '
        'aceptar
        '
        Me.aceptar.Image = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.aceptar.Location = New System.Drawing.Point(316, 611)
        Me.aceptar.Name = "aceptar"
        Me.aceptar.Size = New System.Drawing.Size(116, 43)
        Me.aceptar.TabIndex = 11
        Me.aceptar.UseVisualStyleBackColor = True
        '
        'devolver
        '
        Me.devolver.Image = Global.ReportBuilder2.My.Resources.Resources.dedo_izquiera
        Me.devolver.Location = New System.Drawing.Point(248, 174)
        Me.devolver.Name = "devolver"
        Me.devolver.Size = New System.Drawing.Size(62, 47)
        Me.devolver.TabIndex = 10
        Me.devolver.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.ReportBuilder2.My.Resources.Resources.dedo_derecha1
        Me.Button2.Location = New System.Drawing.Point(248, 121)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(62, 47)
        Me.Button2.TabIndex = 9
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cancelar
        '
        Me.cancelar.Image = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable1
        Me.cancelar.Location = New System.Drawing.Point(438, 611)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(122, 43)
        Me.cancelar.TabIndex = 8
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(164, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Sección:"
        '
        'lblSeccion
        '
        Me.lblSeccion.AutoSize = True
        Me.lblSeccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeccion.Location = New System.Drawing.Point(248, 9)
        Me.lblSeccion.Name = "lblSeccion"
        Me.lblSeccion.Size = New System.Drawing.Size(139, 20)
        Me.lblSeccion.TabIndex = 15
        Me.lblSeccion.Text = "<nombre sección>"
        '
        'Selecciontablas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 662)
        Me.Controls.Add(Me.lblSeccion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Campos_report)
        Me.Controls.Add(Me.Campos_tabla)
        Me.Controls.Add(Me.aceptar)
        Me.Controls.Add(Me.devolver)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cancelar)
        Me.Name = "Selecciontablas"
        Me.Text = "Selección tablas"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Campos_report As ListView
    Friend WithEvents Campos_tabla As ListView
    Friend WithEvents aceptar As Button
    Friend WithEvents devolver As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents cancelar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lblSeccion As Label
    Friend WithEvents TablaInf As ColumnHeader
    Friend WithEvents Tablas As ColumnHeader
End Class
