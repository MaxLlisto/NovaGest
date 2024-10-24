<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeleccionAsientos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SeleccionAsientos))
        Me.lv = New System.Windows.Forms.ListView()
        Me.Asto = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.fecha = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Diario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Debe = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.haber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BtDeseleccionar = New System.Windows.Forms.Button()
        Me.BtCancelar = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.BtSeleccionartodo = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lv
        '
        Me.lv.CheckBoxes = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Asto, Me.fecha, Me.Diario, Me.Debe, Me.haber})
        Me.lv.GridLines = True
        Me.lv.HideSelection = False
        Me.lv.Location = New System.Drawing.Point(8, 5)
        Me.lv.MultiSelect = False
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(623, 377)
        Me.lv.TabIndex = 0
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'Asto
        '
        Me.Asto.Text = "Asiento"
        Me.Asto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Asto.Width = 68
        '
        'fecha
        '
        Me.fecha.Text = "Fecha"
        Me.fecha.Width = 102
        '
        'Diario
        '
        Me.Diario.Text = "Diario"
        Me.Diario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Debe
        '
        Me.Debe.Text = "Importe debe"
        Me.Debe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Debe.Width = 138
        '
        'haber
        '
        Me.haber.Text = "Importe haber"
        Me.haber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.haber.Width = 134
        '
        'BtDeseleccionar
        '
        Me.BtDeseleccionar.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.descheque
        Me.BtDeseleccionar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtDeseleccionar.Location = New System.Drawing.Point(574, 395)
        Me.BtDeseleccionar.Name = "BtDeseleccionar"
        Me.BtDeseleccionar.Size = New System.Drawing.Size(42, 43)
        Me.BtDeseleccionar.TabIndex = 22
        Me.BtDeseleccionar.UseVisualStyleBackColor = True
        '
        'BtCancelar
        '
        Me.BtCancelar.BackgroundImage = CType(resources.GetObject("BtCancelar.BackgroundImage"), System.Drawing.Image)
        Me.BtCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtCancelar.Location = New System.Drawing.Point(81, 388)
        Me.BtCancelar.Name = "BtCancelar"
        Me.BtCancelar.Size = New System.Drawing.Size(50, 50)
        Me.BtCancelar.TabIndex = 24
        Me.BtCancelar.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.BackgroundImage = CType(resources.GetObject("BtSalir.BackgroundImage"), System.Drawing.Image)
        Me.BtSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSalir.Location = New System.Drawing.Point(8, 388)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(50, 50)
        Me.BtSalir.TabIndex = 23
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'BtSeleccionartodo
        '
        Me.BtSeleccionartodo.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.cheque
        Me.BtSeleccionartodo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSeleccionartodo.Location = New System.Drawing.Point(523, 395)
        Me.BtSeleccionartodo.Name = "BtSeleccionartodo"
        Me.BtSeleccionartodo.Size = New System.Drawing.Size(45, 43)
        Me.BtSeleccionartodo.TabIndex = 21
        Me.BtSeleccionartodo.UseVisualStyleBackColor = True
        '
        'SeleccionAsientos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 450)
        Me.Controls.Add(Me.BtCancelar)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtDeseleccionar)
        Me.Controls.Add(Me.BtSeleccionartodo)
        Me.Controls.Add(Me.lv)
        Me.Name = "SeleccionAsientos"
        Me.Text = "SeleccionAsientos"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lv As ListView
    Friend WithEvents fecha As ColumnHeader
    Friend WithEvents Asto As ColumnHeader
    Friend WithEvents Diario As ColumnHeader
    Friend WithEvents Debe As ColumnHeader
    Friend WithEvents haber As ColumnHeader
    Friend WithEvents BtDeseleccionar As Button
    Friend WithEvents BtSeleccionartodo As Button
    Friend WithEvents BtCancelar As Button
    Friend WithEvents BtSalir As Button
End Class
