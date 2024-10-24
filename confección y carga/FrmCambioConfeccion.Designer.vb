<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCambioConfeccion
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtCodigo_confeccion = New System.Windows.Forms.TextBox()
        Me.TxtPaletizacion = New System.Windows.Forms.TextBox()
        Me.TxtCajas = New System.Windows.Forms.TextBox()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Cambiar = New System.Windows.Forms.Button()
        Me.LblConfeccion = New System.Windows.Forms.Label()
        Me.lblPaletizacion = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Confección:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Paletización"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(73, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cajas:"
        '
        'TxtCodigo_confeccion
        '
        Me.TxtCodigo_confeccion.Location = New System.Drawing.Point(135, 33)
        Me.TxtCodigo_confeccion.Name = "TxtCodigo_confeccion"
        Me.TxtCodigo_confeccion.Size = New System.Drawing.Size(100, 20)
        Me.TxtCodigo_confeccion.TabIndex = 3
        '
        'TxtPaletizacion
        '
        Me.TxtPaletizacion.Location = New System.Drawing.Point(135, 74)
        Me.TxtPaletizacion.Name = "TxtPaletizacion"
        Me.TxtPaletizacion.Size = New System.Drawing.Size(100, 20)
        Me.TxtPaletizacion.TabIndex = 4
        '
        'TxtCajas
        '
        Me.TxtCajas.Location = New System.Drawing.Point(135, 106)
        Me.TxtCajas.Name = "TxtCajas"
        Me.TxtCajas.Size = New System.Drawing.Size(100, 20)
        Me.TxtCajas.TabIndex = 5
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novagestConfec.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(447, 154)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(43, 40)
        Me.Salir.TabIndex = 7
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Cambiar
        '
        Me.Cambiar.BackgroundImage = Global.novagestConfec.My.Resources.Resources.cambiar
        Me.Cambiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cambiar.Location = New System.Drawing.Point(135, 154)
        Me.Cambiar.Name = "Cambiar"
        Me.Cambiar.Size = New System.Drawing.Size(45, 40)
        Me.Cambiar.TabIndex = 6
        Me.Cambiar.UseVisualStyleBackColor = True
        '
        'LblConfeccion
        '
        Me.LblConfeccion.AutoSize = True
        Me.LblConfeccion.Location = New System.Drawing.Point(258, 39)
        Me.LblConfeccion.Name = "LblConfeccion"
        Me.LblConfeccion.Size = New System.Drawing.Size(10, 13)
        Me.LblConfeccion.TabIndex = 8
        Me.LblConfeccion.Text = "-"
        '
        'lblPaletizacion
        '
        Me.lblPaletizacion.AutoSize = True
        Me.lblPaletizacion.Location = New System.Drawing.Point(258, 77)
        Me.lblPaletizacion.Name = "lblPaletizacion"
        Me.lblPaletizacion.Size = New System.Drawing.Size(10, 13)
        Me.lblPaletizacion.TabIndex = 9
        Me.lblPaletizacion.Text = "-"
        '
        'FrmCambioConfeccion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 206)
        Me.Controls.Add(Me.lblPaletizacion)
        Me.Controls.Add(Me.LblConfeccion)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Cambiar)
        Me.Controls.Add(Me.TxtCajas)
        Me.Controls.Add(Me.TxtPaletizacion)
        Me.Controls.Add(Me.TxtCodigo_confeccion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmCambioConfeccion"
        Me.Text = "Cambios confección"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtCodigo_confeccion As TextBox
    Friend WithEvents TxtPaletizacion As TextBox
    Friend WithEvents TxtCajas As TextBox
    Friend WithEvents Cambiar As Button
    Friend WithEvents Salir As Button
    Friend WithEvents LblConfeccion As Label
    Friend WithEvents lblPaletizacion As Label
End Class
