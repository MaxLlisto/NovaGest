﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TwainTester
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
    Me.Button1 = New System.Windows.Forms.Button()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.Button3 = New System.Windows.Forms.Button()
    Me.Scaned = New System.Windows.Forms.PictureBox()
    CType(Me.Scaned, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(38, 31)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 0
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Button2
    '
    Me.Button2.Location = New System.Drawing.Point(38, 60)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(75, 23)
    Me.Button2.TabIndex = 1
    Me.Button2.Text = "Select"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'Button3
    '
    Me.Button3.Location = New System.Drawing.Point(38, 89)
    Me.Button3.Name = "Button3"
    Me.Button3.Size = New System.Drawing.Size(75, 23)
    Me.Button3.TabIndex = 2
    Me.Button3.Text = "Aquire"
    Me.Button3.UseVisualStyleBackColor = True
    '
    'Scaned
    '
    Me.Scaned.Location = New System.Drawing.Point(137, 31)
    Me.Scaned.Name = "Scaned"
    Me.Scaned.Size = New System.Drawing.Size(176, 91)
    Me.Scaned.TabIndex = 3
    Me.Scaned.TabStop = False
    '
    'TwainTester
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(458, 388)
    Me.Controls.Add(Me.Scaned)
    Me.Controls.Add(Me.Button3)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.Button1)
    Me.Name = "TwainTester"
    Me.Text = "Form1"
    CType(Me.Scaned, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents Button3 As System.Windows.Forms.Button
  Friend WithEvents Scaned As System.Windows.Forms.PictureBox
End Class
