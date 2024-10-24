<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Variables
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
        Me.LvVariables = New System.Windows.Forms.ListView()
        Me.Nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nombre_variable = New System.Windows.Forms.TextBox()
        Me.eliminar = New System.Windows.Forms.Button()
        Me.add_variable = New System.Windows.Forms.Button()
        Me.salir = New System.Windows.Forms.Button()
        Me.cancelar = New System.Windows.Forms.Button()
        Me.Incluir = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ca = New System.Windows.Forms.RadioButton()
        Me.im = New System.Windows.Forms.RadioButton()
        Me.cb = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LvVariables
        '
        Me.LvVariables.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre})
        Me.LvVariables.FullRowSelect = True
        Me.LvVariables.GridLines = True
        Me.LvVariables.HideSelection = False
        Me.LvVariables.Location = New System.Drawing.Point(12, 65)
        Me.LvVariables.MultiSelect = False
        Me.LvVariables.Name = "LvVariables"
        Me.LvVariables.Size = New System.Drawing.Size(328, 342)
        Me.LvVariables.TabIndex = 0
        Me.LvVariables.UseCompatibleStateImageBehavior = False
        Me.LvVariables.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Nombre"
        Me.Nombre.Width = 320
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(74, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Nombre:"
        '
        'nombre_variable
        '
        Me.nombre_variable.Location = New System.Drawing.Point(128, 22)
        Me.nombre_variable.Name = "nombre_variable"
        Me.nombre_variable.Size = New System.Drawing.Size(130, 20)
        Me.nombre_variable.TabIndex = 6
        '
        'eliminar
        '
        Me.eliminar.Image = Global.ReportBuilder2.My.Resources.Resources.eliminar__1_
        Me.eliminar.Location = New System.Drawing.Point(364, 135)
        Me.eliminar.Name = "eliminar"
        Me.eliminar.Size = New System.Drawing.Size(55, 55)
        Me.eliminar.TabIndex = 10
        Me.eliminar.UseVisualStyleBackColor = True
        '
        'add_variable
        '
        Me.add_variable.Image = Global.ReportBuilder2.My.Resources.Resources.mas_variable2
        Me.add_variable.Location = New System.Drawing.Point(12, 5)
        Me.add_variable.Name = "add_variable"
        Me.add_variable.Size = New System.Drawing.Size(55, 55)
        Me.add_variable.TabIndex = 9
        Me.add_variable.UseVisualStyleBackColor = True
        '
        'salir
        '
        Me.salir.Image = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.salir.Location = New System.Drawing.Point(364, 196)
        Me.salir.Name = "salir"
        Me.salir.Size = New System.Drawing.Size(55, 55)
        Me.salir.TabIndex = 8
        Me.salir.UseVisualStyleBackColor = True
        '
        'cancelar
        '
        Me.cancelar.Image = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable
        Me.cancelar.Location = New System.Drawing.Point(362, 257)
        Me.cancelar.Name = "cancelar"
        Me.cancelar.Size = New System.Drawing.Size(55, 55)
        Me.cancelar.TabIndex = 2
        Me.cancelar.UseVisualStyleBackColor = True
        '
        'Incluir
        '
        Me.Incluir.Image = Global.ReportBuilder2.My.Resources.Resources.incluir_variable
        Me.Incluir.Location = New System.Drawing.Point(362, 65)
        Me.Incluir.Name = "Incluir"
        Me.Incluir.Size = New System.Drawing.Size(55, 55)
        Me.Incluir.TabIndex = 1
        Me.Incluir.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cb)
        Me.GroupBox1.Controls.Add(Me.im)
        Me.GroupBox1.Controls.Add(Me.ca)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 414)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(327, 56)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Objeto"
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
        'im
        '
        Me.im.AutoSize = True
        Me.im.Location = New System.Drawing.Point(116, 20)
        Me.im.Name = "im"
        Me.im.Size = New System.Drawing.Size(60, 17)
        Me.im.TabIndex = 1
        Me.im.TabStop = True
        Me.im.Text = "Imagen"
        Me.im.UseVisualStyleBackColor = True
        '
        'cb
        '
        Me.cb.AutoSize = True
        Me.cb.Location = New System.Drawing.Point(216, 19)
        Me.cb.Name = "cb"
        Me.cb.Size = New System.Drawing.Size(91, 17)
        Me.cb.TabIndex = 2
        Me.cb.TabStop = True
        Me.cb.Text = "Código Barras"
        Me.cb.UseVisualStyleBackColor = True
        '
        'Variables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 470)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.eliminar)
        Me.Controls.Add(Me.add_variable)
        Me.Controls.Add(Me.salir)
        Me.Controls.Add(Me.nombre_variable)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cancelar)
        Me.Controls.Add(Me.Incluir)
        Me.Controls.Add(Me.LvVariables)
        Me.Name = "Variables"
        Me.Text = "Gestión de variables"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LvVariables As ListView
    Friend WithEvents Incluir As Button
    Friend WithEvents cancelar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents nombre_variable As TextBox
    Friend WithEvents salir As Button
    Friend WithEvents Nombre As ColumnHeader
    Friend WithEvents add_variable As Button
    Friend WithEvents eliminar As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cb As RadioButton
    Friend WithEvents im As RadioButton
    Friend WithEvents ca As RadioButton
End Class
