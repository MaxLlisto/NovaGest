<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDestruirPalet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDestruirPalet))
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.dFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdDestruir = New System.Windows.Forms.Button()
        Me.Salir = New System.Windows.Forms.Button()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv.Location = New System.Drawing.Point(3, 2)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(698, 370)
        Me.dgv.TabIndex = 0
        '
        'dFecha
        '
        Me.dFecha.ButtonImage = Nothing
        Me.dFecha.FormatoFecha = "dd/MM/yyyy"
        Me.dFecha.Location = New System.Drawing.Point(304, 395)
        Me.dFecha.Name = "dFecha"
        Me.dFecha.Size = New System.Drawing.Size(109, 20)
        Me.dFecha.TabIndex = 1
        Me.dFecha.Text = "12/07/2023"
        Me.dFecha.value = New Date(2023, 7, 12, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(177, 398)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Fecha de la destrucción"
        '
        'cmdDestruir
        '
        Me.cmdDestruir.BackgroundImage = CType(resources.GetObject("cmdDestruir.BackgroundImage"), System.Drawing.Image)
        Me.cmdDestruir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdDestruir.Location = New System.Drawing.Point(3, 374)
        Me.cmdDestruir.Name = "cmdDestruir"
        Me.cmdDestruir.Size = New System.Drawing.Size(62, 55)
        Me.cmdDestruir.TabIndex = 3
        Me.cmdDestruir.UseVisualStyleBackColor = True
        '
        'Salir
        '
        Me.Salir.BackgroundImage = CType(resources.GetObject("Salir.BackgroundImage"), System.Drawing.Image)
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(643, 374)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(58, 55)
        Me.Salir.TabIndex = 4
        Me.Salir.UseVisualStyleBackColor = True
        '
        'FrmDestruirPalet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 436)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.cmdDestruir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dFecha)
        Me.Controls.Add(Me.dgv)
        Me.Name = "FrmDestruirPalet"
        Me.Text = "Selección de materiales reutilizables"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgv As DataGridView
    Friend WithEvents dFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label1 As Label
    Friend WithEvents cmdDestruir As Button
    Friend WithEvents Salir As Button


End Class
