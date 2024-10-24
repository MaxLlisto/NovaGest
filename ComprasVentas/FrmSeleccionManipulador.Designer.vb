<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSeleccionManipulador
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
        Me.lv = New System.Windows.Forms.ListView()
        Me.Cancelar = New System.Windows.Forms.Button()
        Me.GrabarCredito = New System.Windows.Forms.Button()
        Me.GrabarContado = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lv
        '
        Me.lv.HideSelection = False
        Me.lv.Location = New System.Drawing.Point(11, 7)
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(779, 287)
        Me.lv.TabIndex = 0
        Me.lv.UseCompatibleStateImageBehavior = False
        '
        'Cancelar
        '
        Me.Cancelar.BackgroundImage = Global.novagestventas.My.Resources.Resources.cerrar
        Me.Cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancelar.Location = New System.Drawing.Point(733, 300)
        Me.Cancelar.Name = "Cancelar"
        Me.Cancelar.Size = New System.Drawing.Size(55, 57)
        Me.Cancelar.TabIndex = 3
        Me.Cancelar.UseVisualStyleBackColor = True
        '
        'GrabarCredito
        '
        Me.GrabarCredito.BackgroundImage = Global.novagestventas.My.Resources.Resources.GrabarCredito
        Me.GrabarCredito.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GrabarCredito.Location = New System.Drawing.Point(75, 300)
        Me.GrabarCredito.Name = "GrabarCredito"
        Me.GrabarCredito.Size = New System.Drawing.Size(57, 57)
        Me.GrabarCredito.TabIndex = 2
        Me.GrabarCredito.UseVisualStyleBackColor = True
        '
        'GrabarContado
        '
        Me.GrabarContado.BackgroundImage = Global.novagestventas.My.Resources.Resources.Grabarcontado
        Me.GrabarContado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GrabarContado.Location = New System.Drawing.Point(12, 300)
        Me.GrabarContado.Name = "GrabarContado"
        Me.GrabarContado.Size = New System.Drawing.Size(57, 57)
        Me.GrabarContado.TabIndex = 1
        Me.GrabarContado.UseVisualStyleBackColor = True
        '
        'FrmSeleccionManipulador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 363)
        Me.Controls.Add(Me.Cancelar)
        Me.Controls.Add(Me.GrabarCredito)
        Me.Controls.Add(Me.GrabarContado)
        Me.Controls.Add(Me.lv)
        Me.Name = "FrmSeleccionManipulador"
        Me.Text = "Selección manipulador"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lv As ListView
    Friend WithEvents GrabarContado As Button
    Friend WithEvents GrabarCredito As Button
    Friend WithEvents Cancelar As Button
End Class
