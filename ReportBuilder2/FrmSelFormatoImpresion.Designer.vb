<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSelFormatoImpresion
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
        Me.lvFormatos = New System.Windows.Forms.ListView()
        Me.Nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.copias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btCancelar = New System.Windows.Forms.Button()
        Me.BtSeleccionar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvFormatos
        '
        Me.lvFormatos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre, Me.copias, Me.ColumnHeader2, Me.ColumnHeader1})
        Me.lvFormatos.GridLines = True
        Me.lvFormatos.HideSelection = False
        Me.lvFormatos.Location = New System.Drawing.Point(12, 12)
        Me.lvFormatos.Name = "lvFormatos"
        Me.lvFormatos.Size = New System.Drawing.Size(546, 159)
        Me.lvFormatos.TabIndex = 0
        Me.lvFormatos.UseCompatibleStateImageBehavior = False
        Me.lvFormatos.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Nombre formato"
        '
        'copias
        '
        Me.copias.Text = "Copias"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Impresota defecto"
        Me.ColumnHeader2.Width = 165
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Documento"
        '
        'btCancelar
        '
        Me.btCancelar.BackgroundImage = Global.ReportBuilder2.My.Resources.Resources.eliminar_variable
        Me.btCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btCancelar.Location = New System.Drawing.Point(67, 177)
        Me.btCancelar.Name = "btCancelar"
        Me.btCancelar.Size = New System.Drawing.Size(49, 47)
        Me.btCancelar.TabIndex = 2
        Me.btCancelar.UseVisualStyleBackColor = True
        '
        'BtSeleccionar
        '
        Me.BtSeleccionar.BackgroundImage = Global.ReportBuilder2.My.Resources.Resources.check_ok_accept_apply_1582
        Me.BtSeleccionar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSeleccionar.Location = New System.Drawing.Point(12, 177)
        Me.BtSeleccionar.Name = "BtSeleccionar"
        Me.BtSeleccionar.Size = New System.Drawing.Size(49, 47)
        Me.BtSeleccionar.TabIndex = 1
        Me.BtSeleccionar.UseVisualStyleBackColor = True
        '
        'FrmSelFormatoImpresion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 229)
        Me.Controls.Add(Me.btCancelar)
        Me.Controls.Add(Me.BtSeleccionar)
        Me.Controls.Add(Me.lvFormatos)
        Me.Name = "FrmSelFormatoImpresion"
        Me.Text = "Selección del formato de impresión"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvFormatos As ListView
    Friend WithEvents BtSeleccionar As Button
    Friend WithEvents btCancelar As Button
    Friend WithEvents Nombre As ColumnHeader
    Friend WithEvents copias As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
End Class
