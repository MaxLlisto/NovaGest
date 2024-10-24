<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmarqueocaja
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtCaja = New System.Windows.Forms.TextBox()
        Me.TxtFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.BtCalcular = New System.Windows.Forms.Button()
        Me.Line1 = New AwesomeShapeControl.Line()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblContado = New System.Windows.Forms.Label()
        Me.LblCredito = New System.Windows.Forms.Label()
        Me.LblTarjeta = New System.Windows.Forms.Label()
        Me.LblDiferido = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Line2 = New AwesomeShapeControl.Line()
        Me.BtImprimir = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Caja"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(157, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha"
        '
        'TxtCaja
        '
        Me.TxtCaja.Location = New System.Drawing.Point(12, 31)
        Me.TxtCaja.Name = "TxtCaja"
        Me.TxtCaja.Size = New System.Drawing.Size(81, 20)
        Me.TxtCaja.TabIndex = 2
        '
        'TxtFecha
        '
        Me.TxtFecha.ButtonImage = Nothing
        Me.TxtFecha.FormatoFecha = "dd/MM/yyyy"
        Me.TxtFecha.Location = New System.Drawing.Point(122, 31)
        Me.TxtFecha.Name = "TxtFecha"
        Me.TxtFecha.Size = New System.Drawing.Size(129, 20)
        Me.TxtFecha.TabIndex = 3
        Me.TxtFecha.Text = "08/08/2023"
        Me.TxtFecha.value = New Date(2023, 8, 8, 0, 0, 0, 0)
        '
        'BtCalcular
        '
        Me.BtCalcular.BackgroundImage = Global.novagestventas.My.Resources.Resources.calcular
        Me.BtCalcular.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtCalcular.Location = New System.Drawing.Point(285, 21)
        Me.BtCalcular.Name = "BtCalcular"
        Me.BtCalcular.Size = New System.Drawing.Size(45, 39)
        Me.BtCalcular.TabIndex = 4
        Me.BtCalcular.UseVisualStyleBackColor = True
        '
        'Line1
        '
        Me.Line1.EndPoint = New System.Drawing.Point(332, 6)
        Me.Line1.LineColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Line1.LineWidth = 2.0!
        Me.Line1.Location = New System.Drawing.Point(12, 57)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(337, 11)
        Me.Line1.StartPoint = New System.Drawing.Point(5, 5)
        Me.Line1.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Green
        Me.Label3.Location = New System.Drawing.Point(31, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Contado :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Green
        Me.Label4.Location = New System.Drawing.Point(39, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Crédito :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(39, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Tarjeta :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Green
        Me.Label6.Location = New System.Drawing.Point(34, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 16)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Diferido :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Green
        Me.Label7.Location = New System.Drawing.Point(51, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 20)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "TOTAL: "
        '
        'lblContado
        '
        Me.lblContado.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblContado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblContado.Location = New System.Drawing.Point(140, 96)
        Me.lblContado.Name = "lblContado"
        Me.lblContado.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblContado.Size = New System.Drawing.Size(136, 23)
        Me.lblContado.TabIndex = 11
        Me.lblContado.Text = "0.00"
        '
        'LblCredito
        '
        Me.LblCredito.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.LblCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCredito.Location = New System.Drawing.Point(140, 126)
        Me.LblCredito.Name = "LblCredito"
        Me.LblCredito.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LblCredito.Size = New System.Drawing.Size(136, 23)
        Me.LblCredito.TabIndex = 12
        Me.LblCredito.Text = "0.00"
        '
        'LblTarjeta
        '
        Me.LblTarjeta.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.LblTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTarjeta.Location = New System.Drawing.Point(140, 163)
        Me.LblTarjeta.Name = "LblTarjeta"
        Me.LblTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LblTarjeta.Size = New System.Drawing.Size(136, 23)
        Me.LblTarjeta.TabIndex = 13
        Me.LblTarjeta.Text = "0.00"
        '
        'LblDiferido
        '
        Me.LblDiferido.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.LblDiferido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDiferido.Location = New System.Drawing.Point(140, 200)
        Me.LblDiferido.Name = "LblDiferido"
        Me.LblDiferido.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LblDiferido.Size = New System.Drawing.Size(136, 23)
        Me.LblDiferido.TabIndex = 14
        Me.LblDiferido.Text = "0.00"
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotal.Location = New System.Drawing.Point(140, 263)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotal.Size = New System.Drawing.Size(136, 23)
        Me.lblTotal.TabIndex = 15
        Me.lblTotal.Text = "0.00"
        '
        'Line2
        '
        Me.Line2.EndPoint = New System.Drawing.Point(229, 5)
        Me.Line2.LineColor = System.Drawing.Color.Teal
        Me.Line2.LineWidth = 2.0!
        Me.Line2.Location = New System.Drawing.Point(42, 237)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(234, 11)
        Me.Line2.StartPoint = New System.Drawing.Point(5, 6)
        Me.Line2.TabIndex = 16
        '
        'BtImprimir
        '
        Me.BtImprimir.BackgroundImage = Global.novagestventas.My.Resources.Resources.impresorafa
        Me.BtImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtImprimir.Location = New System.Drawing.Point(34, 322)
        Me.BtImprimir.Name = "BtImprimir"
        Me.BtImprimir.Size = New System.Drawing.Size(45, 39)
        Me.BtImprimir.TabIndex = 17
        Me.BtImprimir.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.BackgroundImage = Global.novagestventas.My.Resources.Resources.salir1
        Me.BtSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSalir.Location = New System.Drawing.Point(285, 322)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(45, 39)
        Me.BtSalir.TabIndex = 18
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'Frmarqueocaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 372)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtImprimir)
        Me.Controls.Add(Me.Line2)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.LblDiferido)
        Me.Controls.Add(Me.LblTarjeta)
        Me.Controls.Add(Me.LblCredito)
        Me.Controls.Add(Me.lblContado)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Line1)
        Me.Controls.Add(Me.BtCalcular)
        Me.Controls.Add(Me.TxtFecha)
        Me.Controls.Add(Me.TxtCaja)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frmarqueocaja"
        Me.Text = "Arqueo de caja"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtCaja As TextBox
    Friend WithEvents TxtFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents BtCalcular As Button
    Friend WithEvents Line1 As AwesomeShapeControl.Line
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblContado As Label
    Friend WithEvents LblCredito As Label
    Friend WithEvents LblTarjeta As Label
    Friend WithEvents LblDiferido As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents Line2 As AwesomeShapeControl.Line
    Friend WithEvents BtImprimir As Button
    Friend WithEvents BtSalir As Button
End Class
