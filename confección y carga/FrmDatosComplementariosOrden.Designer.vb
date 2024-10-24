<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDatosComplementariosOrden
    Inherits libcomunes.FormGenerico

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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtfecha_camion = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtHora_prevista = New System.Windows.Forms.TextBox()
        Me.TxtLote = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtMatricula1 = New System.Windows.Forms.TextBox()
        Me.Matriculas = New System.Windows.Forms.Label()
        Me.TxtMatricula2 = New System.Windows.Forms.TextBox()
        Me.TxtTransportista = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TxtTipo_Transporte = New System.Windows.Forms.Label()
        Me.Aceptar = New System.Windows.Forms.Button()
        Me.Cancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha camión:"
        '
        'txtfecha_camion
        '
        Me.txtfecha_camion.ButtonImage = Nothing
        Me.txtfecha_camion.FormatoFecha = "dd/MM/yyyy"
        Me.txtfecha_camion.Location = New System.Drawing.Point(124, 14)
        Me.txtfecha_camion.Name = "txtfecha_camion"
        Me.txtfecha_camion.Size = New System.Drawing.Size(100, 20)
        Me.txtfecha_camion.TabIndex = 2
        Me.txtfecha_camion.Text = "19/07/2023"
        Me.txtfecha_camion.value = New Date(2023, 7, 19, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Hora prevista.:"
        '
        'TxtHora_prevista
        '
        Me.TxtHora_prevista.Location = New System.Drawing.Point(124, 45)
        Me.TxtHora_prevista.Name = "TxtHora_prevista"
        Me.TxtHora_prevista.Size = New System.Drawing.Size(100, 20)
        Me.TxtHora_prevista.TabIndex = 4
        '
        'TxtLote
        '
        Me.TxtLote.Location = New System.Drawing.Point(124, 78)
        Me.TxtLote.Name = "TxtLote"
        Me.TxtLote.Size = New System.Drawing.Size(100, 20)
        Me.TxtLote.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Lote.:"
        '
        'TxtMatricula1
        '
        Me.TxtMatricula1.Location = New System.Drawing.Point(124, 110)
        Me.TxtMatricula1.Name = "TxtMatricula1"
        Me.TxtMatricula1.Size = New System.Drawing.Size(100, 20)
        Me.TxtMatricula1.TabIndex = 8
        '
        'Matriculas
        '
        Me.Matriculas.AutoSize = True
        Me.Matriculas.Location = New System.Drawing.Point(17, 116)
        Me.Matriculas.Name = "Matriculas"
        Me.Matriculas.Size = New System.Drawing.Size(63, 13)
        Me.Matriculas.TabIndex = 7
        Me.Matriculas.Text = "Matrículas.:"
        '
        'TxtMatricula2
        '
        Me.TxtMatricula2.Location = New System.Drawing.Point(244, 110)
        Me.TxtMatricula2.Name = "TxtMatricula2"
        Me.TxtMatricula2.Size = New System.Drawing.Size(100, 20)
        Me.TxtMatricula2.TabIndex = 9
        '
        'TxtTransportista
        '
        Me.TxtTransportista.Location = New System.Drawing.Point(124, 146)
        Me.TxtTransportista.Name = "TxtTransportista"
        Me.TxtTransportista.Size = New System.Drawing.Size(100, 20)
        Me.TxtTransportista.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Transportista.:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(124, 181)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(238, 20)
        Me.TextBox1.TabIndex = 13
        '
        'TxtTipo_Transporte
        '
        Me.TxtTipo_Transporte.AutoSize = True
        Me.TxtTipo_Transporte.Location = New System.Drawing.Point(17, 184)
        Me.TxtTipo_Transporte.Name = "TxtTipo_Transporte"
        Me.TxtTipo_Transporte.Size = New System.Drawing.Size(91, 13)
        Me.TxtTipo_Transporte.TabIndex = 12
        Me.TxtTipo_Transporte.Text = "Tipo_Transporte.:"
        '
        'Aceptar
        '
        Me.Aceptar.BackgroundImage = Global.novagestConfec.My.Resources.Resources.cheque__1_
        Me.Aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Aceptar.Location = New System.Drawing.Point(21, 236)
        Me.Aceptar.Name = "Aceptar"
        Me.Aceptar.Size = New System.Drawing.Size(59, 55)
        Me.Aceptar.TabIndex = 14
        Me.Aceptar.UseVisualStyleBackColor = True
        '
        'Cancelar
        '
        Me.Cancelar.BackgroundImage = Global.novagestConfec.My.Resources.Resources.cerrar
        Me.Cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Cancelar.Location = New System.Drawing.Point(99, 236)
        Me.Cancelar.Name = "Cancelar"
        Me.Cancelar.Size = New System.Drawing.Size(62, 55)
        Me.Cancelar.TabIndex = 15
        Me.Cancelar.UseVisualStyleBackColor = True
        '
        'FrmDatosComplementariosOrden
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 302)
        Me.Controls.Add(Me.Cancelar)
        Me.Controls.Add(Me.Aceptar)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TxtTipo_Transporte)
        Me.Controls.Add(Me.TxtTransportista)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtMatricula2)
        Me.Controls.Add(Me.TxtMatricula1)
        Me.Controls.Add(Me.Matriculas)
        Me.Controls.Add(Me.TxtLote)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtHora_prevista)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtfecha_camion)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmDatosComplementariosOrden"
        Me.Text = "Datos complementarios orden"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents txtfecha_camion As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtHora_prevista As TextBox
    Friend WithEvents TxtLote As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtMatricula1 As TextBox
    Friend WithEvents Matriculas As Label
    Friend WithEvents TxtMatricula2 As TextBox
    Friend WithEvents TxtTransportista As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TxtTipo_Transporte As Label
    Friend WithEvents Aceptar As Button
    Friend WithEvents Cancelar As Button
End Class
