<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Visor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Visor))
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.HideSelection = False
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(3, 12)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(264, 407)
        Me.ListView1.TabIndex = 129
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(128, 128)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Button4
        '
        Me.Button4.Image = Global.ReportBuilder2.My.Resources.Resources.grabarimagen
        Me.Button4.Location = New System.Drawing.Point(88, 424)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(65, 65)
        Me.Button4.TabIndex = 134
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.ReportBuilder2.My.Resources.Resources.addimagen
        Me.Button1.Location = New System.Drawing.Point(3, 424)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(65, 65)
        Me.Button1.TabIndex = 133
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.ReportBuilder2.My.Resources.Resources.salirseleccion
        Me.Button3.Location = New System.Drawing.Point(723, 424)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(65, 65)
        Me.Button3.TabIndex = 132
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Image = Global.ReportBuilder2.My.Resources.Resources.seleccionarimagen1
        Me.Button2.Location = New System.Drawing.Point(652, 424)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(65, 65)
        Me.Button2.TabIndex = 131
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(273, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(515, 407)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 128
        Me.PictureBox1.TabStop = False
        '
        'Visor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 490)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Visor"
        Me.Text = "Imágenes"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button4 As Button
End Class
