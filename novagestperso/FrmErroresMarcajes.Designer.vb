<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmErroresMarcajes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmErroresMarcajes))
        Me.Procesar = New System.Windows.Forms.Button()
        Me.Cancelar = New System.Windows.Forms.Button()
        Me.AExcel = New System.Windows.Forms.Button()
        Me.TxtMsg = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'Procesar
        '
        Me.Procesar.Image = CType(resources.GetObject("Procesar.Image"), System.Drawing.Image)
        Me.Procesar.Location = New System.Drawing.Point(3, 385)
        Me.Procesar.Name = "Procesar"
        Me.Procesar.Size = New System.Drawing.Size(134, 106)
        Me.Procesar.TabIndex = 1
        Me.Procesar.UseVisualStyleBackColor = True
        '
        'Cancelar
        '
        Me.Cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancelar.Image = CType(resources.GetObject("Cancelar.Image"), System.Drawing.Image)
        Me.Cancelar.Location = New System.Drawing.Point(143, 385)
        Me.Cancelar.Name = "Cancelar"
        Me.Cancelar.Size = New System.Drawing.Size(142, 106)
        Me.Cancelar.TabIndex = 2
        Me.Cancelar.UseVisualStyleBackColor = True
        '
        'AExcel
        '
        Me.AExcel.Image = CType(resources.GetObject("AExcel.Image"), System.Drawing.Image)
        Me.AExcel.Location = New System.Drawing.Point(680, 385)
        Me.AExcel.Name = "AExcel"
        Me.AExcel.Size = New System.Drawing.Size(115, 106)
        Me.AExcel.TabIndex = 3
        Me.AExcel.UseVisualStyleBackColor = True
        '
        'TxtMsg
        '
        Me.TxtMsg.Location = New System.Drawing.Point(3, 1)
        Me.TxtMsg.Name = "TxtMsg"
        Me.TxtMsg.Size = New System.Drawing.Size(792, 378)
        Me.TxtMsg.TabIndex = 4
        Me.TxtMsg.Text = ""
        '
        'FrmErroresMarcajes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 493)
        Me.Controls.Add(Me.TxtMsg)
        Me.Controls.Add(Me.AExcel)
        Me.Controls.Add(Me.Cancelar)
        Me.Controls.Add(Me.Procesar)
        Me.Name = "FrmErroresMarcajes"
        Me.Text = "Errores en fichadas"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Procesar As Button
    Friend WithEvents Cancelar As Button
    Friend WithEvents AExcel As Button
    Friend WithEvents TxtMsg As RichTextBox
End Class
