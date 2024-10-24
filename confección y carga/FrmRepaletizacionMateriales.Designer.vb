<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRepaletizacionMateriales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRepaletizacionMateriales))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.Salir = New System.Windows.Forms.Button()
        Me.cmdRepaletizar = New System.Windows.Forms.Button()
        Me.TxtPaletDestino = New System.Windows.Forms.TextBox()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(177, 397)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Indica el palet de destino"
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv.Location = New System.Drawing.Point(3, 1)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(698, 370)
        Me.dgv.TabIndex = 5
        '
        'Salir
        '
        Me.Salir.BackgroundImage = CType(resources.GetObject("Salir.BackgroundImage"), System.Drawing.Image)
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(643, 373)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(58, 55)
        Me.Salir.TabIndex = 9
        Me.Salir.UseVisualStyleBackColor = True
        '
        'cmdRepaletizar
        '
        Me.cmdRepaletizar.BackgroundImage = CType(resources.GetObject("cmdRepaletizar.BackgroundImage"), System.Drawing.Image)
        Me.cmdRepaletizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdRepaletizar.Location = New System.Drawing.Point(3, 373)
        Me.cmdRepaletizar.Name = "cmdRepaletizar"
        Me.cmdRepaletizar.Size = New System.Drawing.Size(62, 55)
        Me.cmdRepaletizar.TabIndex = 8
        Me.cmdRepaletizar.UseVisualStyleBackColor = True
        '
        'TxtPaletDestino
        '
        Me.TxtPaletDestino.Location = New System.Drawing.Point(318, 391)
        Me.TxtPaletDestino.Name = "TxtPaletDestino"
        Me.TxtPaletDestino.Size = New System.Drawing.Size(151, 20)
        Me.TxtPaletDestino.TabIndex = 10
        '
        'FrmRepaletizacionMateriales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 433)
        Me.Controls.Add(Me.TxtPaletDestino)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.cmdRepaletizar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgv)
        Me.Name = "FrmRepaletizacionMateriales"
        Me.Text = "Selección materiales reutilizable en repaletización"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Salir As Button
    Friend WithEvents cmdRepaletizar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents dgv As DataGridView
    Friend WithEvents TxtPaletDestino As TextBox
End Class
