<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label5 = New System.Windows.Forms.Label()
        Me.autoRotateCheckBox = New System.Windows.Forms.CheckBox()
        Me.autoDetectBorderCheckBox = New System.Windows.Forms.CheckBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.useDuplexCheckBox = New System.Windows.Forms.CheckBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.showProgressIndicatorUICheckBox = New System.Windows.Forms.CheckBox()
        Me.checkBoxArea = New System.Windows.Forms.CheckBox()
        Me.diagnosticsButton = New System.Windows.Forms.Button()
        Me.heightLabel = New System.Windows.Forms.Label()
        Me.widthLabel = New System.Windows.Forms.Label()
        Me.blackAndWhiteCheckBox = New System.Windows.Forms.CheckBox()
        Me.saveButton = New System.Windows.Forms.Button()
        Me.useUICheckBox = New System.Windows.Forms.CheckBox()
        Me.useAdfCheckBox = New System.Windows.Forms.CheckBox()
        Me.selectSource = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'label5
        '
        Me.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.label5.Location = New System.Drawing.Point(19, 296)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(120, 2)
        Me.label5.TabIndex = 39
        '
        'autoRotateCheckBox
        '
        Me.autoRotateCheckBox.AutoSize = True
        Me.autoRotateCheckBox.Location = New System.Drawing.Point(19, 276)
        Me.autoRotateCheckBox.Name = "autoRotateCheckBox"
        Me.autoRotateCheckBox.Size = New System.Drawing.Size(94, 17)
        Me.autoRotateCheckBox.TabIndex = 38
        Me.autoRotateCheckBox.Text = "Auto Rotación"
        Me.autoRotateCheckBox.UseVisualStyleBackColor = True
        '
        'autoDetectBorderCheckBox
        '
        Me.autoDetectBorderCheckBox.AutoSize = True
        Me.autoDetectBorderCheckBox.Location = New System.Drawing.Point(19, 253)
        Me.autoDetectBorderCheckBox.Name = "autoDetectBorderCheckBox"
        Me.autoDetectBorderCheckBox.Size = New System.Drawing.Size(128, 17)
        Me.autoDetectBorderCheckBox.TabIndex = 37
        Me.autoDetectBorderCheckBox.Text = "Auto detección borde"
        Me.autoDetectBorderCheckBox.UseVisualStyleBackColor = True
        '
        'label4
        '
        Me.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.label4.Location = New System.Drawing.Point(19, 248)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(120, 2)
        Me.label4.TabIndex = 36
        '
        'label3
        '
        Me.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.label3.Location = New System.Drawing.Point(19, 159)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(120, 2)
        Me.label3.TabIndex = 35
        '
        'label2
        '
        Me.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.label2.Location = New System.Drawing.Point(19, 111)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(120, 2)
        Me.label2.TabIndex = 34
        '
        'useDuplexCheckBox
        '
        Me.useDuplexCheckBox.AutoSize = True
        Me.useDuplexCheckBox.Location = New System.Drawing.Point(19, 91)
        Me.useDuplexCheckBox.Name = "useDuplexCheckBox"
        Me.useDuplexCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.useDuplexCheckBox.TabIndex = 33
        Me.useDuplexCheckBox.Text = "Usar Duplex"
        Me.useDuplexCheckBox.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.label1.Location = New System.Drawing.Point(19, 63)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(120, 2)
        Me.label1.TabIndex = 32
        '
        'showProgressIndicatorUICheckBox
        '
        Me.showProgressIndicatorUICheckBox.AutoSize = True
        Me.showProgressIndicatorUICheckBox.Location = New System.Drawing.Point(19, 139)
        Me.showProgressIndicatorUICheckBox.Name = "showProgressIndicatorUICheckBox"
        Me.showProgressIndicatorUICheckBox.Size = New System.Drawing.Size(105, 17)
        Me.showProgressIndicatorUICheckBox.TabIndex = 31
        Me.showProgressIndicatorUICheckBox.Text = "Mostrar progreso"
        Me.showProgressIndicatorUICheckBox.UseVisualStyleBackColor = True
        '
        'checkBoxArea
        '
        Me.checkBoxArea.AutoSize = True
        Me.checkBoxArea.Location = New System.Drawing.Point(19, 187)
        Me.checkBoxArea.Name = "checkBoxArea"
        Me.checkBoxArea.Size = New System.Drawing.Size(73, 17)
        Me.checkBoxArea.TabIndex = 30
        Me.checkBoxArea.Text = "Grab área"
        Me.checkBoxArea.UseVisualStyleBackColor = True
        '
        'diagnosticsButton
        '
        Me.diagnosticsButton.Location = New System.Drawing.Point(19, 369)
        Me.diagnosticsButton.Name = "diagnosticsButton"
        Me.diagnosticsButton.Size = New System.Drawing.Size(117, 40)
        Me.diagnosticsButton.TabIndex = 24
        Me.diagnosticsButton.Text = "Diagnóstico"
        Me.diagnosticsButton.UseVisualStyleBackColor = True
        '
        'heightLabel
        '
        Me.heightLabel.AutoSize = True
        Me.heightLabel.Location = New System.Drawing.Point(19, 225)
        Me.heightLabel.Name = "heightLabel"
        Me.heightLabel.Size = New System.Drawing.Size(25, 13)
        Me.heightLabel.TabIndex = 29
        Me.heightLabel.Text = "Alto"
        '
        'widthLabel
        '
        Me.widthLabel.AutoSize = True
        Me.widthLabel.Location = New System.Drawing.Point(19, 212)
        Me.widthLabel.Name = "widthLabel"
        Me.widthLabel.Size = New System.Drawing.Size(38, 13)
        Me.widthLabel.TabIndex = 28
        Me.widthLabel.Text = "Ancho"
        '
        'blackAndWhiteCheckBox
        '
        Me.blackAndWhiteCheckBox.AutoSize = True
        Me.blackAndWhiteCheckBox.Location = New System.Drawing.Point(19, 164)
        Me.blackAndWhiteCheckBox.Name = "blackAndWhiteCheckBox"
        Me.blackAndWhiteCheckBox.Size = New System.Drawing.Size(53, 17)
        Me.blackAndWhiteCheckBox.TabIndex = 27
        Me.blackAndWhiteCheckBox.Text = "B && N"
        Me.blackAndWhiteCheckBox.UseVisualStyleBackColor = True
        '
        'saveButton
        '
        Me.saveButton.Location = New System.Drawing.Point(19, 310)
        Me.saveButton.Name = "saveButton"
        Me.saveButton.Size = New System.Drawing.Size(117, 53)
        Me.saveButton.TabIndex = 22
        Me.saveButton.Text = "Grabar"
        Me.saveButton.UseVisualStyleBackColor = True
        '
        'useUICheckBox
        '
        Me.useUICheckBox.AutoSize = True
        Me.useUICheckBox.Location = New System.Drawing.Point(19, 116)
        Me.useUICheckBox.Name = "useUICheckBox"
        Me.useUICheckBox.Size = New System.Drawing.Size(62, 17)
        Me.useUICheckBox.TabIndex = 26
        Me.useUICheckBox.Text = "Usar UI"
        Me.useUICheckBox.UseVisualStyleBackColor = True
        '
        'useAdfCheckBox
        '
        Me.useAdfCheckBox.AutoSize = True
        Me.useAdfCheckBox.Location = New System.Drawing.Point(19, 68)
        Me.useAdfCheckBox.Name = "useAdfCheckBox"
        Me.useAdfCheckBox.Size = New System.Drawing.Size(72, 17)
        Me.useAdfCheckBox.TabIndex = 25
        Me.useAdfCheckBox.Text = "Usar ADF"
        Me.useAdfCheckBox.UseVisualStyleBackColor = True
        '
        'selectSource
        '
        Me.selectSource.Location = New System.Drawing.Point(19, 12)
        Me.selectSource.Name = "selectSource"
        Me.selectSource.Size = New System.Drawing.Size(117, 48)
        Me.selectSource.TabIndex = 20
        Me.selectSource.Text = "Seleccionar fuente"
        Me.selectSource.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(164, 414)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.autoRotateCheckBox)
        Me.Controls.Add(Me.autoDetectBorderCheckBox)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.useDuplexCheckBox)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.showProgressIndicatorUICheckBox)
        Me.Controls.Add(Me.checkBoxArea)
        Me.Controls.Add(Me.diagnosticsButton)
        Me.Controls.Add(Me.heightLabel)
        Me.Controls.Add(Me.widthLabel)
        Me.Controls.Add(Me.blackAndWhiteCheckBox)
        Me.Controls.Add(Me.saveButton)
        Me.Controls.Add(Me.useUICheckBox)
        Me.Controls.Add(Me.useAdfCheckBox)
        Me.Controls.Add(Me.selectSource)
        Me.Name = "MainForm"
        Me.Text = "Configuración Twain"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents autoRotateCheckBox As System.Windows.Forms.CheckBox
    Private WithEvents autoDetectBorderCheckBox As System.Windows.Forms.CheckBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents useDuplexCheckBox As System.Windows.Forms.CheckBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents showProgressIndicatorUICheckBox As System.Windows.Forms.CheckBox
    Private WithEvents checkBoxArea As System.Windows.Forms.CheckBox
    Private WithEvents diagnosticsButton As System.Windows.Forms.Button
    Private WithEvents heightLabel As System.Windows.Forms.Label
    Private WithEvents widthLabel As System.Windows.Forms.Label
    Private WithEvents blackAndWhiteCheckBox As System.Windows.Forms.CheckBox
    Private WithEvents saveButton As System.Windows.Forms.Button
    Private WithEvents useUICheckBox As System.Windows.Forms.CheckBox
    Private WithEvents useAdfCheckBox As System.Windows.Forms.CheckBox
    Private WithEvents selectSource As System.Windows.Forms.Button

End Class
