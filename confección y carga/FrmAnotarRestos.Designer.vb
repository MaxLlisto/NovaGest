<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAnotarRestos
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
        Me.lblOrden = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtNumeroCajas = New System.Windows.Forms.TextBox()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.AnotarRestos = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Orden de confección.:"
        '
        'lblOrden
        '
        Me.lblOrden.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblOrden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrden.Location = New System.Drawing.Point(130, 9)
        Me.lblOrden.Name = "lblOrden"
        Me.lblOrden.Size = New System.Drawing.Size(156, 23)
        Me.lblOrden.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Número de cajas:"
        '
        'TxtNumeroCajas
        '
        Me.TxtNumeroCajas.Location = New System.Drawing.Point(130, 51)
        Me.TxtNumeroCajas.Name = "TxtNumeroCajas"
        Me.TxtNumeroCajas.Size = New System.Drawing.Size(100, 20)
        Me.TxtNumeroCajas.TabIndex = 3
        '
        'BtSalir
        '
        Me.BtSalir.BackgroundImage = Global.novagestConfec.My.Resources.Resources.salirpersona
        Me.BtSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSalir.Location = New System.Drawing.Point(239, 98)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(47, 46)
        Me.BtSalir.TabIndex = 4
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'AnotarRestos
        '
        Me.AnotarRestos.BackgroundImage = Global.novagestConfec.My.Resources.Resources.palet
        Me.AnotarRestos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AnotarRestos.Location = New System.Drawing.Point(176, 98)
        Me.AnotarRestos.Name = "AnotarRestos"
        Me.AnotarRestos.Size = New System.Drawing.Size(47, 46)
        Me.AnotarRestos.TabIndex = 5
        Me.AnotarRestos.UseVisualStyleBackColor = True
        '
        'FrmAnotarRestos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 156)
        Me.Controls.Add(Me.AnotarRestos)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.TxtNumeroCajas)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblOrden)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmAnotarRestos"
        Me.Text = "Anotar restos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lblOrden As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtNumeroCajas As TextBox
    Friend WithEvents BtSalir As Button
    Friend WithEvents AnotarRestos As Button
End Class
