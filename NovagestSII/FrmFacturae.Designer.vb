<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFacturae
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtAlias = New System.Windows.Forms.TextBox()
        Me.TxtSerie = New System.Windows.Forms.TextBox()
        Me.TxtDesdefactura = New System.Windows.Forms.TextBox()
        Me.TxtHastaFactura = New System.Windows.Forms.TextBox()
        Me.TxtCliente = New System.Windows.Forms.TextBox()
        Me.TxtNIF = New System.Windows.Forms.TextBox()
        Me.DTDesdefecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.DTHastaFecha = New Textboxbotoncontrol.TextBoxCalendar()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TxtFactaFactura = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBoxCalendar2 = New Textboxbotoncontrol.TextBoxCalendar()
        Me.TextBoxCalendar3 = New Textboxbotoncontrol.TextBoxCalendar()
        Me.CBFirmaFactura = New System.Windows.Forms.CheckBox()
        Me.CmdProcesar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Opc2 = New System.Windows.Forms.RadioButton()
        Me.Opc1 = New System.Windows.Forms.RadioButton()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(24, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(298, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Emisión factura electrónica"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(330, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Alias certificado:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Serie factura:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(249, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Entre los números"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Código cliente"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(243, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "NIF"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 174)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Entre las fechas:"
        '
        'TxtAlias
        '
        Me.TxtAlias.Location = New System.Drawing.Point(429, 18)
        Me.TxtAlias.Name = "TxtAlias"
        Me.TxtAlias.Size = New System.Drawing.Size(100, 20)
        Me.TxtAlias.TabIndex = 7
        '
        'TxtSerie
        '
        Me.TxtSerie.Location = New System.Drawing.Point(115, 76)
        Me.TxtSerie.Name = "TxtSerie"
        Me.TxtSerie.Size = New System.Drawing.Size(100, 20)
        Me.TxtSerie.TabIndex = 8
        '
        'TxtDesdefactura
        '
        Me.TxtDesdefactura.Location = New System.Drawing.Point(356, 76)
        Me.TxtDesdefactura.Name = "TxtDesdefactura"
        Me.TxtDesdefactura.Size = New System.Drawing.Size(100, 20)
        Me.TxtDesdefactura.TabIndex = 9
        '
        'TxtHastaFactura
        '
        Me.TxtHastaFactura.Location = New System.Drawing.Point(471, 76)
        Me.TxtHastaFactura.Name = "TxtHastaFactura"
        Me.TxtHastaFactura.Size = New System.Drawing.Size(100, 20)
        Me.TxtHastaFactura.TabIndex = 10
        '
        'TxtCliente
        '
        Me.TxtCliente.Location = New System.Drawing.Point(115, 123)
        Me.TxtCliente.Name = "TxtCliente"
        Me.TxtCliente.Size = New System.Drawing.Size(100, 20)
        Me.TxtCliente.TabIndex = 11
        '
        'TxtNIF
        '
        Me.TxtNIF.Location = New System.Drawing.Point(281, 122)
        Me.TxtNIF.Name = "TxtNIF"
        Me.TxtNIF.Size = New System.Drawing.Size(100, 20)
        Me.TxtNIF.TabIndex = 12
        '
        'DTDesdefecha
        '
        Me.DTDesdefecha.ButtonImage = Nothing
        Me.DTDesdefecha.FormatoFecha = "dd/MM/yyyy"
        Me.DTDesdefecha.Location = New System.Drawing.Point(115, 167)
        Me.DTDesdefecha.Name = "DTDesdefecha"
        Me.DTDesdefecha.Size = New System.Drawing.Size(100, 20)
        Me.DTDesdefecha.TabIndex = 14
        Me.DTDesdefecha.Text = "30/04/2024"
        Me.DTDesdefecha.value = New Date(2024, 4, 30, 0, 0, 0, 0)
        '
        'DTHastaFecha
        '
        Me.DTHastaFecha.ButtonImage = Nothing
        Me.DTHastaFecha.FormatoFecha = "dd/MM/yyyy"
        Me.DTHastaFecha.Location = New System.Drawing.Point(281, 167)
        Me.DTHastaFecha.Name = "DTHastaFecha"
        Me.DTHastaFecha.Size = New System.Drawing.Size(100, 20)
        Me.DTHastaFecha.TabIndex = 15
        Me.DTHastaFecha.Text = "30/04/2024"
        Me.DTHastaFecha.value = New Date(2024, 4, 30, 0, 0, 0, 0)
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(115, 76)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 8
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(356, 76)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 9
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(471, 76)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(100, 20)
        Me.TextBox4.TabIndex = 10
        '
        'TxtFactaFactura
        '
        Me.TxtFactaFactura.Location = New System.Drawing.Point(471, 76)
        Me.TxtFactaFactura.Name = "TxtFactaFactura"
        Me.TxtFactaFactura.Size = New System.Drawing.Size(100, 20)
        Me.TxtFactaFactura.TabIndex = 10
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(115, 123)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 11
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(281, 122)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(100, 20)
        Me.TextBox6.TabIndex = 12
        '
        'TextBoxCalendar2
        '
        Me.TextBoxCalendar2.ButtonImage = Nothing
        Me.TextBoxCalendar2.FormatoFecha = "dd/MM/yyyy"
        Me.TextBoxCalendar2.Location = New System.Drawing.Point(115, 167)
        Me.TextBoxCalendar2.Name = "TextBoxCalendar2"
        Me.TextBoxCalendar2.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxCalendar2.TabIndex = 14
        Me.TextBoxCalendar2.Text = "30/04/2024"
        Me.TextBoxCalendar2.value = New Date(2024, 4, 30, 0, 0, 0, 0)
        '
        'TextBoxCalendar3
        '
        Me.TextBoxCalendar3.ButtonImage = Nothing
        Me.TextBoxCalendar3.FormatoFecha = "dd/MM/yyyy"
        Me.TextBoxCalendar3.Location = New System.Drawing.Point(281, 167)
        Me.TextBoxCalendar3.Name = "TextBoxCalendar3"
        Me.TextBoxCalendar3.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxCalendar3.TabIndex = 15
        Me.TextBoxCalendar3.Text = "30/04/2024"
        Me.TextBoxCalendar3.value = New Date(2024, 4, 30, 0, 0, 0, 0)
        '
        'CBFirmaFactura
        '
        Me.CBFirmaFactura.AutoSize = True
        Me.CBFirmaFactura.Location = New System.Drawing.Point(445, 170)
        Me.CBFirmaFactura.Name = "CBFirmaFactura"
        Me.CBFirmaFactura.Size = New System.Drawing.Size(101, 17)
        Me.CBFirmaFactura.TabIndex = 16
        Me.CBFirmaFactura.Text = "Firmar la factura"
        Me.CBFirmaFactura.UseVisualStyleBackColor = True
        '
        'CmdProcesar
        '
        Me.CmdProcesar.BackgroundImage = Global.novagestSII.My.Resources.Resources.gestion_de_proyectos
        Me.CmdProcesar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdProcesar.Location = New System.Drawing.Point(29, 222)
        Me.CmdProcesar.Name = "CmdProcesar"
        Me.CmdProcesar.Size = New System.Drawing.Size(63, 58)
        Me.CmdProcesar.TabIndex = 17
        Me.CmdProcesar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Opc2)
        Me.GroupBox1.Controls.Add(Me.Opc1)
        Me.GroupBox1.Location = New System.Drawing.Point(140, 234)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(448, 46)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Modo de presentación"
        '
        'Opc2
        '
        Me.Opc2.AutoSize = True
        Me.Opc2.Location = New System.Drawing.Point(252, 19)
        Me.Opc2.Name = "Opc2"
        Me.Opc2.Size = New System.Drawing.Size(118, 17)
        Me.Opc2.TabIndex = 1
        Me.Opc2.TabStop = True
        Me.Opc2.Text = "Registros conjuntos"
        Me.Opc2.UseVisualStyleBackColor = True
        '
        'Opc1
        '
        Me.Opc1.AutoSize = True
        Me.Opc1.Location = New System.Drawing.Point(20, 19)
        Me.Opc1.Name = "Opc1"
        Me.Opc1.Size = New System.Drawing.Size(127, 17)
        Me.Opc1.TabIndex = 0
        Me.Opc1.TabStop = True
        Me.Opc1.Text = "Registros individuales"
        Me.Opc1.UseVisualStyleBackColor = True
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = Global.novagestSII.My.Resources.Resources.salir1
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(535, 7)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(58, 52)
        Me.CmdSalir.TabIndex = 19
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'FrmFacturae
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 301)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CmdProcesar)
        Me.Controls.Add(Me.CBFirmaFactura)
        Me.Controls.Add(Me.DTHastaFecha)
        Me.Controls.Add(Me.DTDesdefecha)
        Me.Controls.Add(Me.TxtNIF)
        Me.Controls.Add(Me.TxtCliente)
        Me.Controls.Add(Me.TxtHastaFactura)
        Me.Controls.Add(Me.TxtDesdefactura)
        Me.Controls.Add(Me.TxtSerie)
        Me.Controls.Add(Me.TxtAlias)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmFacturae"
        Me.Text = "Factura electrónica"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtAlias As TextBox
    Friend WithEvents TxtSerie As TextBox
    Friend WithEvents TxtDesdefactura As TextBox
    Friend WithEvents TxtHastaFactura As TextBox
    Friend WithEvents TxtCliente As TextBox
    Friend WithEvents TxtNIF As TextBox
    Friend WithEvents DTDesdefecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents DTHastaFecha As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TxtFactaFactura As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents TextBoxCalendar2 As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents TextBoxCalendar3 As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents CBFirmaFactura As CheckBox
    Friend WithEvents CmdProcesar As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Opc2 As RadioButton
    Friend WithEvents Opc1 As RadioButton
    Friend WithEvents CmdSalir As Button
End Class
