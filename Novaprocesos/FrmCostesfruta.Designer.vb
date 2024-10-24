<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostesfruta
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Fechavolcado = New Textboxbotoncontrol.TextBoxCalendar()
        Me.lblProgreso = New System.Windows.Forms.Label()
        Me.pb = New System.Windows.Forms.PictureBox()
        Me.Grabar = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        CType(Me.pb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Costes del día.:"
        '
        'Fechavolcado
        '
        Me.Fechavolcado.ButtonImage = Nothing
        Me.Fechavolcado.FormatoFecha = "dd/MM/yyyy"
        Me.Fechavolcado.Location = New System.Drawing.Point(113, 15)
        Me.Fechavolcado.Name = "Fechavolcado"
        Me.Fechavolcado.Size = New System.Drawing.Size(144, 20)
        Me.Fechavolcado.TabIndex = 1
        Me.Fechavolcado.Text = "26/09/2023"
        Me.Fechavolcado.value = New Date(2023, 9, 26, 0, 0, 0, 0)
        '
        'lblProgreso
        '
        Me.lblProgreso.Location = New System.Drawing.Point(27, 52)
        Me.lblProgreso.Name = "lblProgreso"
        Me.lblProgreso.Size = New System.Drawing.Size(248, 30)
        Me.lblProgreso.TabIndex = 126
        '
        'pb
        '
        Me.pb.Image = Global.novaprocesos.My.Resources.Resources.update_6894_128
        Me.pb.Location = New System.Drawing.Point(83, 52)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(128, 121)
        Me.pb.TabIndex = 127
        Me.pb.TabStop = False
        Me.pb.Visible = False
        '
        'Grabar
        '
        Me.Grabar.BackgroundImage = Global.novaprocesos.My.Resources.Resources.grabar
        Me.Grabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Grabar.Location = New System.Drawing.Point(292, 15)
        Me.Grabar.Name = "Grabar"
        Me.Grabar.Size = New System.Drawing.Size(51, 50)
        Me.Grabar.TabIndex = 125
        Me.Grabar.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaprocesos.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(292, 97)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(47, 48)
        Me.Salir.TabIndex = 124
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmCostesfruta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 185)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.lblProgreso)
        Me.Controls.Add(Me.Grabar)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Fechavolcado)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmCostesfruta"
        Me.Text = "Costes línea fruta"
        CType(Me.pb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Fechavolcado As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Grabar As Button
    Friend WithEvents Salir As Button
    Friend WithEvents lblProgreso As Label
    Friend WithEvents pb As PictureBox
End Class
