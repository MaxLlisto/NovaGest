<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo02
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
        Me.lstOperarios = New System.Windows.Forms.ListView()
        Me.nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.codigo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TxtNombre = New System.Windows.Forms.TextBox()
        Me.Salir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstOperarios
        '
        Me.lstOperarios.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lstOperarios.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.nombre, Me.codigo})
        Me.lstOperarios.FullRowSelect = True
        Me.lstOperarios.GridLines = True
        Me.lstOperarios.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstOperarios.HideSelection = False
        Me.lstOperarios.Location = New System.Drawing.Point(12, 43)
        Me.lstOperarios.MultiSelect = False
        Me.lstOperarios.Name = "lstOperarios"
        Me.lstOperarios.Size = New System.Drawing.Size(459, 317)
        Me.lstOperarios.TabIndex = 2
        Me.lstOperarios.UseCompatibleStateImageBehavior = False
        Me.lstOperarios.View = System.Windows.Forms.View.Details
        '
        'nombre
        '
        Me.nombre.Text = "Nombre"
        Me.nombre.Width = 300
        '
        'codigo
        '
        Me.codigo.Text = "Código"
        Me.codigo.Width = 150
        '
        'TxtNombre
        '
        Me.TxtNombre.Location = New System.Drawing.Point(12, 12)
        Me.TxtNombre.Name = "TxtNombre"
        Me.TxtNombre.Size = New System.Drawing.Size(459, 20)
        Me.TxtNombre.TabIndex = 1
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(488, 12)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(48, 49)
        Me.Salir.TabIndex = 2
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 372)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.TxtNombre)
        Me.Controls.Add(Me.lstOperarios)
        Me.Name = "FrmNEntradasNuevo02"
        Me.Text = "Selección de operario"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstOperarios As ListView
    Friend WithEvents TxtNombre As TextBox
    Friend WithEvents Salir As Button
    Friend WithEvents nombre As ColumnHeader
    Friend WithEvents codigo As ColumnHeader
End Class
