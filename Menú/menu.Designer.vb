Imports ComboImageBox.ComboImageBox

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class menu
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.lblEjercicio = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbSecciones = New Menugeneral.ComboImageBox.ComboImageBox()
        Me.tvmenu = New Menugeneral.MyTreeView()
        Me.SuspendLayout()
        '
        'lblEmpresa
        '
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmpresa.Location = New System.Drawing.Point(72, 486)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(428, 23)
        Me.lblEmpresa.TabIndex = 2
        '
        'lblUsuario
        '
        Me.lblUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUsuario.Location = New System.Drawing.Point(72, 532)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(428, 23)
        Me.lblUsuario.TabIndex = 3
        '
        'lblEjercicio
        '
        Me.lblEjercicio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEjercicio.Location = New System.Drawing.Point(72, 509)
        Me.lblEjercicio.Name = "lblEjercicio"
        Me.lblEjercicio.Size = New System.Drawing.Size(428, 23)
        Me.lblEjercicio.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(12, 486)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 23)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Empresa:"
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Location = New System.Drawing.Point(12, 509)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Ejercicio:"
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Location = New System.Drawing.Point(12, 532)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 23)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Usuario:"
        '
        'cbSecciones
        '
        Me.cbSecciones.FormattingEnabled = True
        Me.cbSecciones.Location = New System.Drawing.Point(12, 12)
        Me.cbSecciones.Name = "cbSecciones"
        Me.cbSecciones.Size = New System.Drawing.Size(488, 21)
        Me.cbSecciones.TabIndex = 1
        '
        'tvmenu
        '
        Me.tvmenu.Image = Global.Menugeneral.My.Resources.Resources.logo
        Me.tvmenu.Location = New System.Drawing.Point(12, 62)
        Me.tvmenu.Name = "tvmenu"
        Me.tvmenu.Size = New System.Drawing.Size(488, 421)
        Me.tvmenu.TabIndex = 0
        '
        'menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 564)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblEjercicio)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.lblEmpresa)
        Me.Controls.Add(Me.cbSecciones)
        Me.Controls.Add(Me.tvmenu)
        Me.Name = "menu"
        Me.Text = "Menú"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tvmenu As MyTreeView
    Private cbSecciones As ComboImageBox.ComboImageBox
    'Friend WithEvents tvmenu As MyTreeView
    Friend WithEvents lblEmpresa As Label
    Friend WithEvents lblUsuario As Label
    Friend WithEvents lblEjercicio As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
