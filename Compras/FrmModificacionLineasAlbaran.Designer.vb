<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmModificacionLineasAlbaran
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
        Me.MSProcesos = New System.Windows.Forms.DataGridView()
        Me.TxtDetalle = New System.Windows.Forms.TextBox()
        Me.TxtCantidad = New System.Windows.Forms.TextBox()
        Me.TxtPrecio = New System.Windows.Forms.TextBox()
        Me.TxtImporte = New System.Windows.Forms.TextBox()
        Me.Salir = New System.Windows.Forms.Button()
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.CmdEliminarLinea = New System.Windows.Forms.Button()
        Me.CmdModificarDetalle = New System.Windows.Forms.Button()
        CType(Me.MSProcesos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MSProcesos
        '
        Me.MSProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MSProcesos.Location = New System.Drawing.Point(12, 64)
        Me.MSProcesos.Name = "MSProcesos"
        Me.MSProcesos.Size = New System.Drawing.Size(741, 313)
        Me.MSProcesos.TabIndex = 0
        '
        'TxtDetalle
        '
        Me.TxtDetalle.Location = New System.Drawing.Point(13, 384)
        Me.TxtDetalle.Name = "TxtDetalle"
        Me.TxtDetalle.Size = New System.Drawing.Size(388, 20)
        Me.TxtDetalle.TabIndex = 1
        '
        'TxtCantidad
        '
        Me.TxtCantidad.Location = New System.Drawing.Point(418, 383)
        Me.TxtCantidad.Name = "TxtCantidad"
        Me.TxtCantidad.Size = New System.Drawing.Size(100, 20)
        Me.TxtCantidad.TabIndex = 2
        '
        'TxtPrecio
        '
        Me.TxtPrecio.Location = New System.Drawing.Point(535, 383)
        Me.TxtPrecio.Name = "TxtPrecio"
        Me.TxtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.TxtPrecio.TabIndex = 3
        '
        'TxtImporte
        '
        Me.TxtImporte.Location = New System.Drawing.Point(652, 383)
        Me.TxtImporte.Name = "TxtImporte"
        Me.TxtImporte.Size = New System.Drawing.Size(100, 20)
        Me.TxtImporte.TabIndex = 4
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novacompras.My.Resources.Resources.salir
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(760, 12)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(43, 40)
        Me.Salir.TabIndex = 9
        Me.Salir.UseVisualStyleBackColor = True
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackgroundImage = Global.novacompras.My.Resources.Resources.cancelar_cambio
        Me.CmdCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdCancelar.Location = New System.Drawing.Point(760, 394)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.Size = New System.Drawing.Size(43, 40)
        Me.CmdCancelar.TabIndex = 8
        Me.CmdCancelar.UseVisualStyleBackColor = True
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackgroundImage = Global.novacompras.My.Resources.Resources.correo_cambio
        Me.CmdAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdAceptar.Location = New System.Drawing.Point(760, 348)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.Size = New System.Drawing.Size(43, 40)
        Me.CmdAceptar.TabIndex = 7
        Me.CmdAceptar.UseVisualStyleBackColor = True
        '
        'CmdEliminarLinea
        '
        Me.CmdEliminarLinea.BackgroundImage = Global.novacompras.My.Resources.Resources.eliminar
        Me.CmdEliminarLinea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdEliminarLinea.Location = New System.Drawing.Point(760, 110)
        Me.CmdEliminarLinea.Name = "CmdEliminarLinea"
        Me.CmdEliminarLinea.Size = New System.Drawing.Size(43, 40)
        Me.CmdEliminarLinea.TabIndex = 6
        Me.CmdEliminarLinea.UseVisualStyleBackColor = True
        '
        'CmdModificarDetalle
        '
        Me.CmdModificarDetalle.BackgroundImage = Global.novacompras.My.Resources.Resources.editar
        Me.CmdModificarDetalle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdModificarDetalle.Location = New System.Drawing.Point(760, 64)
        Me.CmdModificarDetalle.Name = "CmdModificarDetalle"
        Me.CmdModificarDetalle.Size = New System.Drawing.Size(43, 40)
        Me.CmdModificarDetalle.TabIndex = 5
        Me.CmdModificarDetalle.UseVisualStyleBackColor = True
        '
        'FrmModificacionLineasAlbaran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 450)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Controls.Add(Me.CmdEliminarLinea)
        Me.Controls.Add(Me.CmdModificarDetalle)
        Me.Controls.Add(Me.TxtImporte)
        Me.Controls.Add(Me.TxtPrecio)
        Me.Controls.Add(Me.TxtCantidad)
        Me.Controls.Add(Me.TxtDetalle)
        Me.Controls.Add(Me.MSProcesos)
        Me.Name = "FrmModificacionLineasAlbaran"
        Me.Text = "Modificar líneas albarán"
        CType(Me.MSProcesos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MSProcesos As DataGridView
    Friend WithEvents TxtDetalle As TextBox
    Friend WithEvents TxtCantidad As TextBox
    Friend WithEvents TxtPrecio As TextBox
    Friend WithEvents TxtImporte As TextBox
    Friend WithEvents CmdModificarDetalle As Button
    Friend WithEvents CmdEliminarLinea As Button
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents Salir As Button
End Class
