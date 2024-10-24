<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSII
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
        Me.DPDesde = New Textboxbotoncontrol.TextBoxCalendar()
        Me.DPHasta = New Textboxbotoncontrol.TextBoxCalendar()
        Me.CmdFacturasEmitidas = New System.Windows.Forms.Button()
        Me.CmdFacturasRecibidas = New System.Windows.Forms.Button()
        Me.CmdFacturasInversion = New System.Windows.Forms.Button()
        Me.TxtEnvioE = New System.Windows.Forms.TextBox()
        Me.TxtEnvioR = New System.Windows.Forms.TextBox()
        Me.TxtEnvioBI = New System.Windows.Forms.TextBox()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.TxtNombreXML = New System.Windows.Forms.TextBox()
        Me.PB1 = New System.Windows.Forms.ProgressBar()
        Me.PB2 = New System.Windows.Forms.ProgressBar()
        Me.PB4 = New System.Windows.Forms.ProgressBar()
        Me.PBR = New System.Windows.Forms.ProgressBar()
        Me.CkEmi = New System.Windows.Forms.CheckBox()
        Me.CkReci = New System.Windows.Forms.CheckBox()
        Me.CkInv = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar4 = New System.Windows.Forms.ProgressBar()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.CmdBuscarXML = New System.Windows.Forms.Button()
        Me.CmdProcesar = New System.Windows.Forms.Button()
        Me.CmdClipBoard = New System.Windows.Forms.Button()
        Me.Line1 = New AwesomeShapeControl.Line()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DPDesde
        '
        Me.DPDesde.ButtonImage = Nothing
        Me.DPDesde.FormatoFecha = "dd/MM/yyyy"
        Me.DPDesde.Location = New System.Drawing.Point(152, 16)
        Me.DPDesde.Name = "DPDesde"
        Me.DPDesde.Size = New System.Drawing.Size(133, 20)
        Me.DPDesde.TabIndex = 0
        Me.DPDesde.Text = "22/04/2024"
        Me.DPDesde.value = New Date(2024, 4, 22, 0, 0, 0, 0)
        '
        'DPHasta
        '
        Me.DPHasta.ButtonImage = Nothing
        Me.DPHasta.FormatoFecha = "dd/MM/yyyy"
        Me.DPHasta.Location = New System.Drawing.Point(470, 16)
        Me.DPHasta.Name = "DPHasta"
        Me.DPHasta.Size = New System.Drawing.Size(133, 20)
        Me.DPHasta.TabIndex = 1
        Me.DPHasta.Text = "22/04/2024"
        Me.DPHasta.value = New Date(2024, 4, 22, 0, 0, 0, 0)
        '
        'CmdFacturasEmitidas
        '
        Me.CmdFacturasEmitidas.Location = New System.Drawing.Point(12, 82)
        Me.CmdFacturasEmitidas.Name = "CmdFacturasEmitidas"
        Me.CmdFacturasEmitidas.Size = New System.Drawing.Size(75, 23)
        Me.CmdFacturasEmitidas.TabIndex = 2
        Me.CmdFacturasEmitidas.Text = "Emitidas"
        Me.CmdFacturasEmitidas.UseVisualStyleBackColor = True
        '
        'CmdFacturasRecibidas
        '
        Me.CmdFacturasRecibidas.Location = New System.Drawing.Point(10, 122)
        Me.CmdFacturasRecibidas.Name = "CmdFacturasRecibidas"
        Me.CmdFacturasRecibidas.Size = New System.Drawing.Size(75, 23)
        Me.CmdFacturasRecibidas.TabIndex = 3
        Me.CmdFacturasRecibidas.Text = "Recibidas"
        Me.CmdFacturasRecibidas.UseVisualStyleBackColor = True
        '
        'CmdFacturasInversion
        '
        Me.CmdFacturasInversion.Location = New System.Drawing.Point(10, 160)
        Me.CmdFacturasInversion.Name = "CmdFacturasInversion"
        Me.CmdFacturasInversion.Size = New System.Drawing.Size(75, 23)
        Me.CmdFacturasInversion.TabIndex = 4
        Me.CmdFacturasInversion.Text = "B inversión"
        Me.CmdFacturasInversion.UseVisualStyleBackColor = True
        '
        'TxtEnvioE
        '
        Me.TxtEnvioE.Location = New System.Drawing.Point(91, 82)
        Me.TxtEnvioE.Name = "TxtEnvioE"
        Me.TxtEnvioE.Size = New System.Drawing.Size(67, 20)
        Me.TxtEnvioE.TabIndex = 5
        '
        'TxtEnvioR
        '
        Me.TxtEnvioR.Location = New System.Drawing.Point(91, 122)
        Me.TxtEnvioR.Name = "TxtEnvioR"
        Me.TxtEnvioR.Size = New System.Drawing.Size(67, 20)
        Me.TxtEnvioR.TabIndex = 6
        '
        'TxtEnvioBI
        '
        Me.TxtEnvioBI.Location = New System.Drawing.Point(91, 162)
        Me.TxtEnvioBI.Name = "TxtEnvioBI"
        Me.TxtEnvioBI.Size = New System.Drawing.Size(67, 20)
        Me.TxtEnvioBI.TabIndex = 7
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(91, 229)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(566, 20)
        Me.txtPath.TabIndex = 8
        '
        'TxtNombreXML
        '
        Me.TxtNombreXML.Location = New System.Drawing.Point(91, 283)
        Me.TxtNombreXML.Name = "TxtNombreXML"
        Me.TxtNombreXML.Size = New System.Drawing.Size(566, 20)
        Me.TxtNombreXML.TabIndex = 9
        '
        'PB1
        '
        Me.PB1.Location = New System.Drawing.Point(165, 82)
        Me.PB1.Name = "PB1"
        Me.PB1.Size = New System.Drawing.Size(493, 23)
        Me.PB1.TabIndex = 12
        '
        'PB2
        '
        Me.PB2.Location = New System.Drawing.Point(165, 119)
        Me.PB2.Name = "PB2"
        Me.PB2.Size = New System.Drawing.Size(493, 23)
        Me.PB2.TabIndex = 13
        '
        'PB4
        '
        Me.PB4.Location = New System.Drawing.Point(165, 160)
        Me.PB4.Name = "PB4"
        Me.PB4.Size = New System.Drawing.Size(493, 23)
        Me.PB4.TabIndex = 14
        '
        'PBR
        '
        Me.PBR.Location = New System.Drawing.Point(92, 309)
        Me.PBR.Name = "PBR"
        Me.PBR.Size = New System.Drawing.Size(566, 10)
        Me.PBR.TabIndex = 15
        '
        'CkEmi
        '
        Me.CkEmi.AutoSize = True
        Me.CkEmi.Location = New System.Drawing.Point(663, 85)
        Me.CkEmi.Name = "CkEmi"
        Me.CkEmi.Size = New System.Drawing.Size(15, 14)
        Me.CkEmi.TabIndex = 16
        Me.CkEmi.UseVisualStyleBackColor = True
        '
        'CkReci
        '
        Me.CkReci.AutoSize = True
        Me.CkReci.Location = New System.Drawing.Point(663, 125)
        Me.CkReci.Name = "CkReci"
        Me.CkReci.Size = New System.Drawing.Size(15, 14)
        Me.CkReci.TabIndex = 17
        Me.CkReci.UseVisualStyleBackColor = True
        '
        'CkInv
        '
        Me.CkInv.AutoSize = True
        Me.CkInv.Location = New System.Drawing.Point(663, 165)
        Me.CkInv.Name = "CkInv"
        Me.CkInv.Size = New System.Drawing.Size(15, 14)
        Me.CkInv.TabIndex = 18
        Me.CkInv.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(70, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Fecha inicial"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(395, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Fecha final"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(107, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Envío"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(660, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Trasmitir"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 234)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Ruta AEAT"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 290)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Respuesta"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(647, 53)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Tipo facturas"
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(165, 119)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(493, 23)
        Me.ProgressBar2.TabIndex = 13
        '
        'ProgressBar4
        '
        Me.ProgressBar4.Location = New System.Drawing.Point(92, 309)
        Me.ProgressBar4.Name = "ProgressBar4"
        Me.ProgressBar4.Size = New System.Drawing.Size(566, 10)
        Me.ProgressBar4.TabIndex = 15
        '
        'CmdSalir
        '
        Me.CmdSalir.BackgroundImage = Global.novagestSII.My.Resources.Resources.salir1
        Me.CmdSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdSalir.Location = New System.Drawing.Point(680, 1)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(50, 52)
        Me.CmdSalir.TabIndex = 19
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'CmdBuscarXML
        '
        Me.CmdBuscarXML.BackgroundImage = Global.novagestSII.My.Resources.Resources.buscar
        Me.CmdBuscarXML.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdBuscarXML.Location = New System.Drawing.Point(661, 277)
        Me.CmdBuscarXML.Name = "CmdBuscarXML"
        Me.CmdBuscarXML.Size = New System.Drawing.Size(29, 30)
        Me.CmdBuscarXML.TabIndex = 29
        Me.CmdBuscarXML.UseVisualStyleBackColor = True
        '
        'CmdProcesar
        '
        Me.CmdProcesar.BackgroundImage = Global.novagestSII.My.Resources.Resources.procesarfichero
        Me.CmdProcesar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdProcesar.Location = New System.Drawing.Point(696, 273)
        Me.CmdProcesar.Name = "CmdProcesar"
        Me.CmdProcesar.Size = New System.Drawing.Size(38, 38)
        Me.CmdProcesar.TabIndex = 11
        Me.CmdProcesar.UseVisualStyleBackColor = True
        '
        'CmdClipBoard
        '
        Me.CmdClipBoard.BackgroundImage = Global.novagestSII.My.Resources.Resources.copiar
        Me.CmdClipBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CmdClipBoard.Location = New System.Drawing.Point(663, 221)
        Me.CmdClipBoard.Name = "CmdClipBoard"
        Me.CmdClipBoard.Size = New System.Drawing.Size(27, 28)
        Me.CmdClipBoard.TabIndex = 10
        Me.CmdClipBoard.UseVisualStyleBackColor = True
        '
        'Line1
        '
        Me.Line1.EndPoint = New System.Drawing.Point(689, 5)
        Me.Line1.Location = New System.Drawing.Point(10, 259)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(694, 12)
        Me.Line1.StartPoint = New System.Drawing.Point(5, 7)
        Me.Line1.TabIndex = 28
        '
        'FrmSII
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 333)
        Me.Controls.Add(Me.CmdSalir)
        Me.Controls.Add(Me.CmdBuscarXML)
        Me.Controls.Add(Me.Line1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CkInv)
        Me.Controls.Add(Me.CkReci)
        Me.Controls.Add(Me.CkEmi)
        Me.Controls.Add(Me.PBR)
        Me.Controls.Add(Me.PB4)
        Me.Controls.Add(Me.PB2)
        Me.Controls.Add(Me.PB1)
        Me.Controls.Add(Me.CmdProcesar)
        Me.Controls.Add(Me.CmdClipBoard)
        Me.Controls.Add(Me.TxtNombreXML)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.TxtEnvioBI)
        Me.Controls.Add(Me.TxtEnvioR)
        Me.Controls.Add(Me.TxtEnvioE)
        Me.Controls.Add(Me.CmdFacturasInversion)
        Me.Controls.Add(Me.CmdFacturasRecibidas)
        Me.Controls.Add(Me.CmdFacturasEmitidas)
        Me.Controls.Add(Me.DPHasta)
        Me.Controls.Add(Me.DPDesde)
        Me.Controls.Add(Me.GroupBox1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Name = "FrmSII"
        Me.Text = "Suministro inmediato de información(SII)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DPDesde As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents DPHasta As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents CmdFacturasEmitidas As Button
    Friend WithEvents CmdFacturasRecibidas As Button
    Friend WithEvents CmdFacturasInversion As Button
    Friend WithEvents TxtEnvioE As TextBox
    Friend WithEvents TxtEnvioR As TextBox
    Friend WithEvents TxtEnvioBI As TextBox
    Friend WithEvents txtPath As TextBox
    Friend WithEvents TxtNombreXML As TextBox
    Friend WithEvents CmdClipBoard As Button
    Friend WithEvents CmdProcesar As Button
    Friend WithEvents PB1 As ProgressBar
    Friend WithEvents PB2 As ProgressBar
    Friend WithEvents PB4 As ProgressBar
    Friend WithEvents PBR As ProgressBar
    Friend WithEvents CkEmi As CheckBox
    Friend WithEvents CkReci As CheckBox
    Friend WithEvents CkInv As CheckBox
    Friend WithEvents CmdSalir As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Line1 As AwesomeShapeControl.Line
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents ProgressBar4 As ProgressBar
    Friend WithEvents CmdBuscarXML As Button
End Class
