<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo04
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
        Me.LstEnvases = New System.Windows.Forms.ListView()
        Me.Salir = New System.Windows.Forms.Button()
        Me.descripcion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.codigo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'LstEnvases
        '
        Me.LstEnvases.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.descripcion, Me.codigo})
        Me.LstEnvases.HideSelection = False
        Me.LstEnvases.Location = New System.Drawing.Point(13, 13)
        Me.LstEnvases.Name = "LstEnvases"
        Me.LstEnvases.Size = New System.Drawing.Size(400, 501)
        Me.LstEnvases.TabIndex = 0
        Me.LstEnvases.UseCompatibleStateImageBehavior = False
        Me.LstEnvases.View = System.Windows.Forms.View.Details
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(419, 13)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(44, 41)
        Me.Salir.TabIndex = 1
        Me.Salir.UseVisualStyleBackColor = True
        '
        'descripcion
        '
        Me.descripcion.Text = "Descripción envase"
        Me.descripcion.Width = 150
        '
        'codigo
        '
        Me.codigo.Text = "Código"
        '
        'FrmNEntradasNuevo04
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 526)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.LstEnvases)
        Me.Name = "FrmNEntradasNuevo04"
        Me.Text = "Selección de envases"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LstEnvases As ListView
    Friend WithEvents Salir As Button
    Friend WithEvents descripcion As ColumnHeader
    Friend WithEvents codigo As ColumnHeader
End Class
