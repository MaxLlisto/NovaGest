<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo05
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
        Me.LstAlbaranes = New System.Windows.Forms.ListView()
        Me.Salir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(307, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(385, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Albaranes pendientes de tarado"
        '
        'LstAlbaranes
        '
        Me.LstAlbaranes.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.LstAlbaranes.FullRowSelect = True
        Me.LstAlbaranes.GridLines = True
        Me.LstAlbaranes.HideSelection = False
        Me.LstAlbaranes.LabelEdit = True
        Me.LstAlbaranes.Location = New System.Drawing.Point(12, 58)
        Me.LstAlbaranes.Name = "LstAlbaranes"
        Me.LstAlbaranes.Size = New System.Drawing.Size(1048, 380)
        Me.LstAlbaranes.TabIndex = 1
        Me.LstAlbaranes.UseCompatibleStateImageBehavior = False
        Me.LstAlbaranes.View = System.Windows.Forms.View.Details
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novaEntradas.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(1009, 6)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(51, 43)
        Me.Salir.TabIndex = 2
        Me.Salir.Text = "Button1"
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo05
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 450)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.LstAlbaranes)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmNEntradasNuevo05"
        Me.Text = "Albaranes pendientes de tarado"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents LstAlbaranes As ListView
    Friend WithEvents Salir As Button
End Class
