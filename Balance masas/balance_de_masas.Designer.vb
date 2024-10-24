Imports libcomunes

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BalanceMasas
    Inherits FormGenerico

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
        Me.resumido = New System.Windows.Forms.TabPage()
        Me.DGResumido = New System.Windows.Forms.DataGridView()
        Me.DGInformes = New System.Windows.Forms.TabControl()
        Me.Detallado = New System.Windows.Forms.TabPage()
        Me.DGDetallado = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DPFechaVolcado = New System.Windows.Forms.DateTimePicker()
        Me.DPHastaFecha = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.OpcionVolcados1 = New System.Windows.Forms.RadioButton()
        Me.OpcionVolcados = New System.Windows.Forms.RadioButton()
        Me.TxtVariedad = New System.Windows.Forms.TextBox()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Excel = New System.Windows.Forms.Button()
        Me.BtBuscar = New System.Windows.Forms.Button()
        Me.resumido.SuspendLayout()
        CType(Me.DGResumido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DGInformes.SuspendLayout()
        Me.Detallado.SuspendLayout()
        CType(Me.DGDetallado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'resumido
        '
        Me.resumido.Controls.Add(Me.DGResumido)
        Me.resumido.Location = New System.Drawing.Point(4, 22)
        Me.resumido.Name = "resumido"
        Me.resumido.Padding = New System.Windows.Forms.Padding(3)
        Me.resumido.Size = New System.Drawing.Size(1168, 568)
        Me.resumido.TabIndex = 3
        Me.resumido.Text = "Informe por albarán de salida"
        Me.resumido.UseVisualStyleBackColor = True
        '
        'DGResumido
        '
        Me.DGResumido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGResumido.Location = New System.Drawing.Point(9, 3)
        Me.DGResumido.Name = "DGResumido"
        Me.DGResumido.ReadOnly = True
        Me.DGResumido.Size = New System.Drawing.Size(1156, 559)
        Me.DGResumido.TabIndex = 1
        '
        'DGInformes
        '
        Me.DGInformes.Controls.Add(Me.Detallado)
        Me.DGInformes.Controls.Add(Me.resumido)
        Me.DGInformes.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.DGInformes.Location = New System.Drawing.Point(12, 92)
        Me.DGInformes.Name = "DGInformes"
        Me.DGInformes.SelectedIndex = 0
        Me.DGInformes.Size = New System.Drawing.Size(1176, 594)
        Me.DGInformes.TabIndex = 0
        '
        'Detallado
        '
        Me.Detallado.Controls.Add(Me.DGDetallado)
        Me.Detallado.Location = New System.Drawing.Point(4, 22)
        Me.Detallado.Name = "Detallado"
        Me.Detallado.Padding = New System.Windows.Forms.Padding(3)
        Me.Detallado.Size = New System.Drawing.Size(1168, 568)
        Me.Detallado.TabIndex = 1
        Me.Detallado.Text = "Informe detallado"
        Me.Detallado.UseVisualStyleBackColor = True
        '
        'DGDetallado
        '
        Me.DGDetallado.AllowUserToAddRows = False
        Me.DGDetallado.AllowUserToDeleteRows = False
        Me.DGDetallado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetallado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGDetallado.Location = New System.Drawing.Point(3, 3)
        Me.DGDetallado.Name = "DGDetallado"
        Me.DGDetallado.ReadOnly = True
        Me.DGDetallado.Size = New System.Drawing.Size(1162, 562)
        Me.DGDetallado.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha consulta"
        '
        'DPFechaVolcado
        '
        Me.DPFechaVolcado.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DPFechaVolcado.Location = New System.Drawing.Point(12, 26)
        Me.DPFechaVolcado.Name = "DPFechaVolcado"
        Me.DPFechaVolcado.Size = New System.Drawing.Size(103, 20)
        Me.DPFechaVolcado.TabIndex = 2
        '
        'DPHastaFecha
        '
        Me.DPHastaFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DPHastaFecha.Location = New System.Drawing.Point(12, 52)
        Me.DPHastaFecha.Name = "DPHastaFecha"
        Me.DPHastaFecha.Size = New System.Drawing.Size(103, 20)
        Me.DPHastaFecha.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.OpcionVolcados1)
        Me.GroupBox1.Controls.Add(Me.OpcionVolcados)
        Me.GroupBox1.Controls.Add(Me.TxtVariedad)
        Me.GroupBox1.Location = New System.Drawing.Point(134, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(308, 69)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Código de cultivo"
        '
        'OpcionVolcados1
        '
        Me.OpcionVolcados1.AutoSize = True
        Me.OpcionVolcados1.Location = New System.Drawing.Point(126, 34)
        Me.OpcionVolcados1.Name = "OpcionVolcados1"
        Me.OpcionVolcados1.Size = New System.Drawing.Size(123, 17)
        Me.OpcionVolcados1.TabIndex = 2
        Me.OpcionVolcados1.TabStop = True
        Me.OpcionVolcados1.Text = "Cultivo seleccionado"
        Me.OpcionVolcados1.UseVisualStyleBackColor = True
        '
        'OpcionVolcados
        '
        Me.OpcionVolcados.AutoSize = True
        Me.OpcionVolcados.Location = New System.Drawing.Point(126, 11)
        Me.OpcionVolcados.Name = "OpcionVolcados"
        Me.OpcionVolcados.Size = New System.Drawing.Size(55, 17)
        Me.OpcionVolcados.TabIndex = 1
        Me.OpcionVolcados.TabStop = True
        Me.OpcionVolcados.Text = "Todos"
        Me.OpcionVolcados.UseVisualStyleBackColor = True
        '
        'TxtVariedad
        '
        Me.TxtVariedad.Location = New System.Drawing.Point(6, 40)
        Me.TxtVariedad.Name = "TxtVariedad"
        Me.TxtVariedad.Size = New System.Drawing.Size(99, 20)
        Me.TxtVariedad.TabIndex = 0
        '
        'Salir
        '
        Me.Salir.Image = Global.NovaBalMasas.My.Resources.Resources.puerta3
        Me.Salir.Location = New System.Drawing.Point(1071, 20)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(71, 66)
        Me.Salir.TabIndex = 8
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Excel
        '
        Me.Excel.Image = Global.NovaBalMasas.My.Resources.Resources.icons8_ms_excel_64
        Me.Excel.Location = New System.Drawing.Point(650, 22)
        Me.Excel.Name = "Excel"
        Me.Excel.Size = New System.Drawing.Size(72, 64)
        Me.Excel.TabIndex = 7
        Me.Excel.UseVisualStyleBackColor = True
        '
        'BtBuscar
        '
        Me.BtBuscar.Image = Global.NovaBalMasas.My.Resources.Resources.icons8_buscar_chat_50
        Me.BtBuscar.Location = New System.Drawing.Point(448, 17)
        Me.BtBuscar.Name = "BtBuscar"
        Me.BtBuscar.Size = New System.Drawing.Size(97, 69)
        Me.BtBuscar.TabIndex = 6
        Me.BtBuscar.UseVisualStyleBackColor = True
        '
        'BalanceMasas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 689)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Excel)
        Me.Controls.Add(Me.BtBuscar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DPHastaFecha)
        Me.Controls.Add(Me.DPFechaVolcado)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGInformes)
        Me.Name = "BalanceMasas"
        Me.Text = "Balance de masas"
        Me.resumido.ResumeLayout(False)
        CType(Me.DGResumido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DGInformes.ResumeLayout(False)
        Me.Detallado.ResumeLayout(False)
        CType(Me.DGDetallado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents resumido As TabPage
    Friend WithEvents DGResumido As DataGridView
    Friend WithEvents DGInformes As TabControl
    Friend WithEvents Label1 As Label
    Friend WithEvents DPFechaVolcado As DateTimePicker
    Friend WithEvents DPHastaFecha As DateTimePicker
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents OpcionVolcados1 As RadioButton
    Friend WithEvents OpcionVolcados As RadioButton
    Friend WithEvents TxtVariedad As TextBox
    Friend WithEvents BtBuscar As Button
    Friend WithEvents Excel As Button
    Friend WithEvents Salir As Button
    Friend WithEvents Detallado As TabPage
    Friend WithEvents DGDetallado As DataGridView
End Class
