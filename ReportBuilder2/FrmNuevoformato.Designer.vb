<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNuevoformato
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
        Me.cbdocumento = New System.Windows.Forms.ComboBox()
        Me.Txtnuevodocumento = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Txtformato = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btcorrecto = New System.Windows.Forms.Button()
        Me.Btcancelar = New System.Windows.Forms.Button()
        Me.cbnuevo = New System.Windows.Forms.CheckBox()
        Me.TxtNuevoProceso = New System.Windows.Forms.TextBox()
        Me.CbNuevoProceso = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbProcesonuevo = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'cbdocumento
        '
        Me.cbdocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbdocumento.FormattingEnabled = True
        Me.cbdocumento.Location = New System.Drawing.Point(44, 68)
        Me.cbdocumento.Name = "cbdocumento"
        Me.cbdocumento.Size = New System.Drawing.Size(456, 21)
        Me.cbdocumento.TabIndex = 0
        '
        'Txtnuevodocumento
        '
        Me.Txtnuevodocumento.Enabled = False
        Me.Txtnuevodocumento.Location = New System.Drawing.Point(44, 131)
        Me.Txtnuevodocumento.MaxLength = 30
        Me.Txtnuevodocumento.Name = "Txtnuevodocumento"
        Me.Txtnuevodocumento.Size = New System.Drawing.Size(456, 20)
        Me.Txtnuevodocumento.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Selecciona un documento:"
        '
        'Txtformato
        '
        Me.Txtformato.Location = New System.Drawing.Point(45, 295)
        Me.Txtformato.MaxLength = 30
        Me.Txtformato.Name = "Txtformato"
        Me.Txtformato.Size = New System.Drawing.Size(456, 20)
        Me.Txtformato.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(42, 279)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Nombre del formato:"
        '
        'Btcorrecto
        '
        Me.Btcorrecto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btcorrecto.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Btcorrecto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btcorrecto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Btcorrecto.Location = New System.Drawing.Point(304, 384)
        Me.Btcorrecto.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btcorrecto.Name = "Btcorrecto"
        Me.Btcorrecto.Size = New System.Drawing.Size(91, 35)
        Me.Btcorrecto.TabIndex = 6
        Me.Btcorrecto.Text = "Correcto"
        Me.Btcorrecto.UseVisualStyleBackColor = False
        '
        'Btcancelar
        '
        Me.Btcancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btcancelar.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Btcancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btcancelar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Btcancelar.Location = New System.Drawing.Point(409, 384)
        Me.Btcancelar.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Btcancelar.Name = "Btcancelar"
        Me.Btcancelar.Size = New System.Drawing.Size(92, 35)
        Me.Btcancelar.TabIndex = 7
        Me.Btcancelar.Text = "Cancelar"
        Me.Btcancelar.UseVisualStyleBackColor = False
        '
        'cbnuevo
        '
        Me.cbnuevo.AutoSize = True
        Me.cbnuevo.Location = New System.Drawing.Point(44, 108)
        Me.cbnuevo.Name = "cbnuevo"
        Me.cbnuevo.Size = New System.Drawing.Size(170, 17)
        Me.cbnuevo.TabIndex = 8
        Me.cbnuevo.Text = "Nombre de documento nuevo:"
        Me.cbnuevo.UseVisualStyleBackColor = True
        '
        'TxtNuevoProceso
        '
        Me.TxtNuevoProceso.Enabled = False
        Me.TxtNuevoProceso.Location = New System.Drawing.Point(44, 239)
        Me.TxtNuevoProceso.MaxLength = 50
        Me.TxtNuevoProceso.Name = "TxtNuevoProceso"
        Me.TxtNuevoProceso.Size = New System.Drawing.Size(456, 20)
        Me.TxtNuevoProceso.TabIndex = 9
        Me.TxtNuevoProceso.Visible = False
        '
        'CbNuevoProceso
        '
        Me.CbNuevoProceso.AutoSize = True
        Me.CbNuevoProceso.Location = New System.Drawing.Point(44, 216)
        Me.CbNuevoProceso.Name = "CbNuevoProceso"
        Me.CbNuevoProceso.Size = New System.Drawing.Size(99, 17)
        Me.CbNuevoProceso.TabIndex = 10
        Me.CbNuevoProceso.Text = "Nuevo proceso"
        Me.CbNuevoProceso.UseVisualStyleBackColor = True
        Me.CbNuevoProceso.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(42, 163)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Selecciona un proceso"
        '
        'cbProcesonuevo
        '
        Me.cbProcesonuevo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProcesonuevo.Enabled = False
        Me.cbProcesonuevo.FormattingEnabled = True
        Me.cbProcesonuevo.Location = New System.Drawing.Point(45, 179)
        Me.cbProcesonuevo.Name = "cbProcesonuevo"
        Me.cbProcesonuevo.Size = New System.Drawing.Size(456, 21)
        Me.cbProcesonuevo.TabIndex = 11
        '
        'FrmNuevoformato
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 431)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbProcesonuevo)
        Me.Controls.Add(Me.CbNuevoProceso)
        Me.Controls.Add(Me.TxtNuevoProceso)
        Me.Controls.Add(Me.cbnuevo)
        Me.Controls.Add(Me.Btcorrecto)
        Me.Controls.Add(Me.Btcancelar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Txtformato)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Txtnuevodocumento)
        Me.Controls.Add(Me.cbdocumento)
        Me.Name = "FrmNuevoformato"
        Me.Text = "Nuevo formato"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cbdocumento As ComboBox
    Friend WithEvents Txtnuevodocumento As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Txtformato As TextBox
    Friend WithEvents Label3 As Label
    Public WithEvents Btcorrecto As Button
    Public WithEvents Btcancelar As Button
    Friend WithEvents cbnuevo As CheckBox
    Friend WithEvents TxtNuevoProceso As TextBox
    Friend WithEvents CbNuevoProceso As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbProcesonuevo As ComboBox
End Class
