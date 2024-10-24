<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo21
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
        Me.Grabar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtPorcentaje_Plaga = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.grabar1
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(448, 14)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(61, 44)
        Me.Grabar.TabIndex = 151
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(529, 12)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(54, 48)
        Me.Salir.TabIndex = 150
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(209, 20)
        Me.Label1.TabIndex = 152
        Me.Label1.Text = "Nuevo porcentaje clareta"
        '
        'TxtPorcentaje_Plaga
        '
        Me.TxtPorcentaje_Plaga.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPorcentaje_Plaga.Location = New System.Drawing.Point(257, 30)
        Me.TxtPorcentaje_Plaga.Name = "TxtPorcentaje_Plaga"
        Me.TxtPorcentaje_Plaga.Size = New System.Drawing.Size(100, 26)
        Me.TxtPorcentaje_Plaga.TabIndex = 153
        Me.TxtPorcentaje_Plaga.Text = "0,00"
        Me.TxtPorcentaje_Plaga.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FrmNEntradasNuevo21
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 86)
        Me.Controls.Add(Me.TxtPorcentaje_Plaga)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.Salir)
        Me.Name = "FrmNEntradasNuevo21"
        Me.Text = "Modificación defecto clareta"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Grabar As Button
    Friend WithEvents Salir As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtPorcentaje_Plaga As TextBox
End Class
