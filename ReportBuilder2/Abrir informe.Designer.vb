<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AbrirInformes
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
        Me.TwLista = New System.Windows.Forms.TreeView()
        Me.btAbrir = New System.Windows.Forms.Button()
        Me.btCancelar = New System.Windows.Forms.Button()
        Me.cbInformesantiguos = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'TwLista
        '
        Me.TwLista.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TwLista.Location = New System.Drawing.Point(57, 12)
        Me.TwLista.Name = "TwLista"
        Me.TwLista.Size = New System.Drawing.Size(683, 631)
        Me.TwLista.TabIndex = 0
        '
        'btAbrir
        '
        Me.btAbrir.Location = New System.Drawing.Point(57, 649)
        Me.btAbrir.Name = "btAbrir"
        Me.btAbrir.Size = New System.Drawing.Size(146, 26)
        Me.btAbrir.TabIndex = 1
        Me.btAbrir.Text = "Abrir"
        Me.btAbrir.UseVisualStyleBackColor = True
        '
        'btCancelar
        '
        Me.btCancelar.Location = New System.Drawing.Point(209, 651)
        Me.btCancelar.Name = "btCancelar"
        Me.btCancelar.Size = New System.Drawing.Size(120, 23)
        Me.btCancelar.TabIndex = 2
        Me.btCancelar.Text = "Cancela"
        Me.btCancelar.UseVisualStyleBackColor = True
        '
        'cbInformesantiguos
        '
        Me.cbInformesantiguos.AutoSize = True
        Me.cbInformesantiguos.Location = New System.Drawing.Point(498, 650)
        Me.cbInformesantiguos.Name = "cbInformesantiguos"
        Me.cbInformesantiguos.Size = New System.Drawing.Size(142, 17)
        Me.cbInformesantiguos.TabIndex = 3
        Me.cbInformesantiguos.Text = "Cargar informes antiguos"
        Me.cbInformesantiguos.UseVisualStyleBackColor = True
        '
        'AbrirInformes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 686)
        Me.Controls.Add(Me.cbInformesantiguos)
        Me.Controls.Add(Me.btCancelar)
        Me.Controls.Add(Me.btAbrir)
        Me.Controls.Add(Me.TwLista)
        Me.Name = "AbrirInformes"
        Me.Text = "Abrir informe"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TwLista As TreeView
    Friend WithEvents btAbrir As Button
    Friend WithEvents btCancelar As Button
    Friend WithEvents cbInformesantiguos As CheckBox
End Class
