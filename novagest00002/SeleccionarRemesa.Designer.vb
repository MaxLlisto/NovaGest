<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeleccionarRemesa
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeleccionarRemesa))
        Me.lwAsociados = New System.Windows.Forms.ListView()
        Me.Proceso = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Recibos = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Importe = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CmdAsociar = New System.Windows.Forms.Button()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.cbVarias = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lwAsociados
        '
        Me.lwAsociados.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Proceso, Me.Recibos, Me.Importe})
        Me.lwAsociados.GridLines = True
        Me.lwAsociados.HideSelection = False
        Me.lwAsociados.Location = New System.Drawing.Point(12, 0)
        Me.lwAsociados.MultiSelect = False
        Me.lwAsociados.Name = "lwAsociados"
        Me.lwAsociados.Size = New System.Drawing.Size(489, 208)
        Me.lwAsociados.TabIndex = 0
        Me.lwAsociados.UseCompatibleStateImageBehavior = False
        Me.lwAsociados.View = System.Windows.Forms.View.Details
        '
        'Proceso
        '
        Me.Proceso.Text = "Proceso"
        Me.Proceso.Width = 130
        '
        'Recibos
        '
        Me.Recibos.Text = "Núm. recibos"
        Me.Recibos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Recibos.Width = 131
        '
        'Importe
        '
        Me.Importe.Text = "Importe"
        Me.Importe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Importe.Width = 206
        '
        'CmdAsociar
        '
        Me.CmdAsociar.BackgroundImage = CType(resources.GetObject("CmdAsociar.BackgroundImage"), System.Drawing.Image)
        Me.CmdAsociar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdAsociar.Location = New System.Drawing.Point(12, 214)
        Me.CmdAsociar.Name = "CmdAsociar"
        Me.CmdAsociar.Size = New System.Drawing.Size(69, 62)
        Me.CmdAsociar.TabIndex = 1
        Me.CmdAsociar.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = CType(resources.GetObject("CmdSalir.BackgroundImage"), System.Drawing.Image)
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(408, 214)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(77, 62)
        Me.CmdSalir.TabIndex = 2
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'cbVarias
        '
        Me.cbVarias.AutoSize = True
        Me.cbVarias.Location = New System.Drawing.Point(159, 223)
        Me.cbVarias.Name = "cbVarias"
        Me.cbVarias.Size = New System.Drawing.Size(155, 17)
        Me.cbVarias.TabIndex = 3
        Me.cbVarias.Text = "Seleccionar varias remesas"
        Me.cbVarias.UseVisualStyleBackColor = True
        '
        'SeleccionarRemesa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 281)
        Me.Controls.Add(Me.cbVarias)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.CmdAsociar)
        Me.Controls.Add(Me.lwAsociados)
        Me.Name = "SeleccionarRemesa"
        Me.Text = "Seleccionar Remesa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lwAsociados As ListView
    Friend WithEvents Proceso As ColumnHeader
    Friend WithEvents Recibos As ColumnHeader
    Friend WithEvents Importe As ColumnHeader
    Friend WithEvents CmdAsociar As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents cbVarias As CheckBox
End Class
