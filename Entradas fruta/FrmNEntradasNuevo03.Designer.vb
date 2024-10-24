<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo03
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
        Me.LstPeriodos = New System.Windows.Forms.ListView()
        Me.Nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.codigo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CmdGrabar = New System.Windows.Forms.Button()
        Me.TxtDescripcion = New System.Windows.Forms.TextBox()
        Me.TxtCodPeriodo = New System.Windows.Forms.TextBox()
        Me.Salir = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LstPeriodos
        '
        Me.LstPeriodos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre, Me.codigo})
        Me.LstPeriodos.HideSelection = False
        Me.LstPeriodos.Location = New System.Drawing.Point(12, 3)
        Me.LstPeriodos.Name = "LstPeriodos"
        Me.LstPeriodos.Size = New System.Drawing.Size(371, 474)
        Me.LstPeriodos.TabIndex = 0
        Me.LstPeriodos.UseCompatibleStateImageBehavior = False
        Me.LstPeriodos.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Nombre periodo"
        Me.Nombre.Width = 150
        '
        'codigo
        '
        Me.codigo.Text = "Código"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmdGrabar)
        Me.GroupBox1.Controls.Add(Me.TxtDescripcion)
        Me.GroupBox1.Controls.Add(Me.TxtCodPeriodo)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 484)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(428, 52)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Nuevo periodo"
        '
        'CmdGrabar
        '
        Me.CmdGrabar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.grabar1
        Me.CmdGrabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdGrabar.Location = New System.Drawing.Point(377, 12)
        Me.CmdGrabar.Name = "CmdGrabar"
        Me.CmdGrabar.Size = New System.Drawing.Size(40, 34)
        Me.CmdGrabar.TabIndex = 2
        Me.CmdGrabar.UseVisualStyleBackColor = True
        '
        'TxtDescripcion
        '
        Me.TxtDescripcion.Location = New System.Drawing.Point(66, 20)
        Me.TxtDescripcion.Name = "TxtDescripcion"
        Me.TxtDescripcion.Size = New System.Drawing.Size(284, 20)
        Me.TxtDescripcion.TabIndex = 1
        '
        'TxtCodPeriodo
        '
        Me.TxtCodPeriodo.Location = New System.Drawing.Point(7, 20)
        Me.TxtCodPeriodo.Name = "TxtCodPeriodo"
        Me.TxtCodPeriodo.Size = New System.Drawing.Size(53, 20)
        Me.TxtCodPeriodo.TabIndex = 0
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(389, 12)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(41, 34)
        Me.Salir.TabIndex = 2
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(441, 540)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LstPeriodos)
        Me.Name = "FrmNEntradasNuevo03"
        Me.Text = "Selección de periodo"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LstPeriodos As ListView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CmdGrabar As Button
    Friend WithEvents TxtDescripcion As TextBox
    Friend WithEvents TxtCodPeriodo As TextBox
    Friend WithEvents Salir As Button
    Friend WithEvents Nombre As ColumnHeader
    Friend WithEvents codigo As ColumnHeader
End Class
