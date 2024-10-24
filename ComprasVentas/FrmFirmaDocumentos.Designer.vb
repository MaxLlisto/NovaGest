<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFirmaDocumentos
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
        Me.SigPlusNET1 = New Topaz.SigPlusNET()
        Me.Seguir = New System.Windows.Forms.Button()
        Me.BtSeguirSinFirmar = New System.Windows.Forms.Button()
        Me.BtCancelaFirma = New System.Windows.Forms.Button()
        Me.BtBorraPantalla = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'SigPlusNET1
        '
        Me.SigPlusNET1.BackColor = System.Drawing.Color.White
        Me.SigPlusNET1.ForeColor = System.Drawing.Color.Black
        Me.SigPlusNET1.Location = New System.Drawing.Point(12, 12)
        Me.SigPlusNET1.Name = "SigPlusNET1"
        Me.SigPlusNET1.Size = New System.Drawing.Size(624, 197)
        Me.SigPlusNET1.TabIndex = 0
        Me.SigPlusNET1.Text = "SigPlusNET1"
        '
        'Seguir
        '
        Me.Seguir.BackgroundImage = Global.novagestventas.My.Resources.Resources.firmadoyseguir
        Me.Seguir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Seguir.Location = New System.Drawing.Point(500, 219)
        Me.Seguir.Name = "Seguir"
        Me.Seguir.Size = New System.Drawing.Size(62, 61)
        Me.Seguir.TabIndex = 4
        Me.Seguir.UseVisualStyleBackColor = True
        '
        'BtSeguirSinFirmar
        '
        Me.BtSeguirSinFirmar.BackgroundImage = Global.novagestventas.My.Resources.Resources.seguirsinfirmar
        Me.BtSeguirSinFirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSeguirSinFirmar.Location = New System.Drawing.Point(340, 217)
        Me.BtSeguirSinFirmar.Name = "BtSeguirSinFirmar"
        Me.BtSeguirSinFirmar.Size = New System.Drawing.Size(80, 63)
        Me.BtSeguirSinFirmar.TabIndex = 3
        Me.BtSeguirSinFirmar.UseVisualStyleBackColor = True
        '
        'BtCancelaFirma
        '
        Me.BtCancelaFirma.BackgroundImage = Global.novagestventas.My.Resources.Resources.anularfirma
        Me.BtCancelaFirma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtCancelaFirma.Location = New System.Drawing.Point(189, 219)
        Me.BtCancelaFirma.Name = "BtCancelaFirma"
        Me.BtCancelaFirma.Size = New System.Drawing.Size(71, 61)
        Me.BtCancelaFirma.TabIndex = 2
        Me.BtCancelaFirma.UseVisualStyleBackColor = True
        '
        'BtBorraPantalla
        '
        Me.BtBorraPantalla.BackgroundImage = Global.novagestventas.My.Resources.Resources.BorraPantalla
        Me.BtBorraPantalla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtBorraPantalla.Location = New System.Drawing.Point(27, 219)
        Me.BtBorraPantalla.Name = "BtBorraPantalla"
        Me.BtBorraPantalla.Size = New System.Drawing.Size(82, 61)
        Me.BtBorraPantalla.TabIndex = 1
        Me.BtBorraPantalla.UseVisualStyleBackColor = True
        '
        'FrmFirmaDocumentos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 284)
        Me.Controls.Add(Me.Seguir)
        Me.Controls.Add(Me.BtSeguirSinFirmar)
        Me.Controls.Add(Me.BtCancelaFirma)
        Me.Controls.Add(Me.BtBorraPantalla)
        Me.Controls.Add(Me.SigPlusNET1)
        Me.Name = "FrmFirmaDocumentos"
        Me.Text = "Firma documentos"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SigPlusNET1 As Topaz.SigPlusNET
    Friend WithEvents BtBorraPantalla As Button
    Friend WithEvents BtCancelaFirma As Button
    Friend WithEvents BtSeguirSinFirmar As Button
    Friend WithEvents Seguir As Button
End Class
