<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNEntradasNuevo07
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
        Me.TxtAlbaran = New System.Windows.Forms.TextBox()
        Me.lstAlbaranes = New System.Windows.Forms.ListView()
        Me.CmdBuscar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(314, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Albaranes pendientes de clasificación"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Número albarán:"
        '
        'TxtAlbaran
        '
        Me.TxtAlbaran.Location = New System.Drawing.Point(104, 13)
        Me.TxtAlbaran.Name = "TxtAlbaran"
        Me.TxtAlbaran.Size = New System.Drawing.Size(126, 20)
        Me.TxtAlbaran.TabIndex = 2
        '
        'lstAlbaranes
        '
        Me.lstAlbaranes.HideSelection = False
        Me.lstAlbaranes.Location = New System.Drawing.Point(15, 50)
        Me.lstAlbaranes.Name = "lstAlbaranes"
        Me.lstAlbaranes.Size = New System.Drawing.Size(1047, 423)
        Me.lstAlbaranes.TabIndex = 4
        Me.lstAlbaranes.UseCompatibleStateImageBehavior = False
        '
        'CmdBuscar
        '
        Me.CmdBuscar.BackgroundImage = Global.novaEntradas.My.Resources.Resources.buscar01
        Me.CmdBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBuscar.Location = New System.Drawing.Point(763, 4)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.Size = New System.Drawing.Size(48, 42)
        Me.CmdBuscar.TabIndex = 3
        Me.CmdBuscar.UseVisualStyleBackColor = True
        '
        'FrmNEntradasNuevo07
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1071, 485)
        Me.Controls.Add(Me.lstAlbaranes)
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.TxtAlbaran)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmNEntradasNuevo07"
        Me.Text = "Albaranes pendientes de clasificación"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAlbaran As TextBox
    Friend WithEvents CmdBuscar As Button
    Friend WithEvents lstAlbaranes As ListView
End Class
