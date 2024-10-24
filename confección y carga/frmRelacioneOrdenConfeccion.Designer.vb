<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRelacioneOrdenConfeccion
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
        Me.lwAsociados = New System.Windows.Forms.ListView()
        Me.Salir = New System.Windows.Forms.Button()
        Me.Seleccionar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lwAsociados
        '
        Me.lwAsociados.HideSelection = False
        Me.lwAsociados.Location = New System.Drawing.Point(5, 2)
        Me.lwAsociados.Name = "lwAsociados"
        Me.lwAsociados.Size = New System.Drawing.Size(783, 318)
        Me.lwAsociados.TabIndex = 0
        Me.lwAsociados.UseCompatibleStateImageBehavior = False
        '
        'Salir
        '
        Me.Salir.BackgroundImage = Global.novagestConfec.My.Resources.Resources.salirpersona
        Me.Salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Salir.Location = New System.Drawing.Point(727, 326)
        Me.Salir.Name = "Salir"
        Me.Salir.Size = New System.Drawing.Size(61, 50)
        Me.Salir.TabIndex = 2
        Me.Salir.UseVisualStyleBackColor = True
        '
        'Seleccionar
        '
        Me.Seleccionar.BackgroundImage = Global.novagestConfec.My.Resources.Resources.Oklista
        Me.Seleccionar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Seleccionar.Location = New System.Drawing.Point(5, 328)
        Me.Seleccionar.Name = "Seleccionar"
        Me.Seleccionar.Size = New System.Drawing.Size(61, 50)
        Me.Seleccionar.TabIndex = 1
        Me.Seleccionar.UseVisualStyleBackColor = True
        '
        'frmRelacioneOrdenConfeccion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 383)
        Me.Controls.Add(Me.Salir)
        Me.Controls.Add(Me.Seleccionar)
        Me.Controls.Add(Me.lwAsociados)
        Me.Name = "frmRelacioneOrdenConfeccion"
        Me.Text = "frmRelacioneOrdenConfeccion"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lwAsociados As ListView
    Friend WithEvents Seleccionar As Button
    Friend WithEvents Salir As Button
End Class
