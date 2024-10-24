<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Escaneadocumento
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
        Me.lblRazonSocial = New System.Windows.Forms.Label()
        Me.lblDirectorio = New System.Windows.Forms.Label()
        Me.lblNumeroEntrada = New System.Windows.Forms.Label()
        Me.lblEmpresa = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Refrescar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtArchivo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.OpFactura = New System.Windows.Forms.RadioButton()
        Me.OpAlbaran = New System.Windows.Forms.RadioButton()
        Me.OFDialog = New System.Windows.Forms.OpenFileDialog()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.BtGrabar = New System.Windows.Forms.Button()
        Me.BtSel = New System.Windows.Forms.Button()
        Me.BtEscanear = New System.Windows.Forms.Button()
        Me.BtSeleccionar = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblRazonSocial
        '
        Me.lblRazonSocial.AutoEllipsis = True
        Me.lblRazonSocial.BackColor = System.Drawing.Color.White
        Me.lblRazonSocial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRazonSocial.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblRazonSocial.Location = New System.Drawing.Point(108, 109)
        Me.lblRazonSocial.Name = "lblRazonSocial"
        Me.lblRazonSocial.Size = New System.Drawing.Size(314, 21)
        Me.lblRazonSocial.TabIndex = 9
        '
        'lblDirectorio
        '
        Me.lblDirectorio.AutoEllipsis = True
        Me.lblDirectorio.BackColor = System.Drawing.Color.White
        Me.lblDirectorio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDirectorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblDirectorio.Location = New System.Drawing.Point(85, 78)
        Me.lblDirectorio.Name = "lblDirectorio"
        Me.lblDirectorio.Size = New System.Drawing.Size(337, 21)
        Me.lblDirectorio.TabIndex = 8
        Me.lblDirectorio.Text = "D:\pruebas DF"
        '
        'lblNumeroEntrada
        '
        Me.lblNumeroEntrada.AutoEllipsis = True
        Me.lblNumeroEntrada.BackColor = System.Drawing.Color.White
        Me.lblNumeroEntrada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumeroEntrada.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblNumeroEntrada.Location = New System.Drawing.Point(131, 47)
        Me.lblNumeroEntrada.Name = "lblNumeroEntrada"
        Me.lblNumeroEntrada.Size = New System.Drawing.Size(213, 21)
        Me.lblNumeroEntrada.TabIndex = 7
        Me.lblNumeroEntrada.Text = "33"
        '
        'lblEmpresa
        '
        Me.lblEmpresa.AutoEllipsis = True
        Me.lblEmpresa.BackColor = System.Drawing.Color.White
        Me.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblEmpresa.Location = New System.Drawing.Point(85, 16)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(213, 21)
        Me.lblEmpresa.TabIndex = 5
        Me.lblEmpresa.Text = "1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Empresa:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Razón social:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(18, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Directorio:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Refrescar)
        Me.GroupBox2.Controls.Add(Me.lblRazonSocial)
        Me.GroupBox2.Controls.Add(Me.lblDirectorio)
        Me.GroupBox2.Controls.Add(Me.lblNumeroEntrada)
        Me.GroupBox2.Controls.Add(Me.lblEmpresa)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(192, 243)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(428, 145)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Documento"
        '
        'Refrescar
        '
        Me.Refrescar.BackgroundImage = Global.Escanerdocumentos.My.Resources.Resources.refrescar
        Me.Refrescar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Refrescar.Location = New System.Drawing.Point(354, 13)
        Me.Refrescar.Name = "Refrescar"
        Me.Refrescar.Size = New System.Drawing.Size(32, 31)
        Me.Refrescar.TabIndex = 10
        Me.Refrescar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Número de entrada:"
        '
        'TxtArchivo
        '
        Me.TxtArchivo.Location = New System.Drawing.Point(192, 214)
        Me.TxtArchivo.Name = "TxtArchivo"
        Me.TxtArchivo.Size = New System.Drawing.Size(520, 20)
        Me.TxtArchivo.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 221)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Seleccionar documento"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OpFactura)
        Me.GroupBox1.Controls.Add(Me.OpAlbaran)
        Me.GroupBox1.Location = New System.Drawing.Point(192, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(409, 160)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'OpFactura
        '
        Me.OpFactura.AutoSize = True
        Me.OpFactura.Image = Global.Escanerdocumentos.My.Resources.Resources.factura
        Me.OpFactura.Location = New System.Drawing.Point(229, 16)
        Me.OpFactura.Name = "OpFactura"
        Me.OpFactura.Size = New System.Drawing.Size(142, 128)
        Me.OpFactura.TabIndex = 1
        Me.OpFactura.UseVisualStyleBackColor = True
        '
        'OpAlbaran
        '
        Me.OpAlbaran.AutoSize = True
        Me.OpAlbaran.BackColor = System.Drawing.Color.Transparent
        Me.OpAlbaran.Checked = True
        Me.OpAlbaran.Image = Global.Escanerdocumentos.My.Resources.Resources.albaran
        Me.OpAlbaran.Location = New System.Drawing.Point(23, 16)
        Me.OpAlbaran.Name = "OpAlbaran"
        Me.OpAlbaran.Size = New System.Drawing.Size(142, 128)
        Me.OpAlbaran.TabIndex = 0
        Me.OpAlbaran.TabStop = True
        Me.OpAlbaran.UseVisualStyleBackColor = False
        '
        'BtSalir
        '
        Me.BtSalir.Image = Global.Escanerdocumentos.My.Resources.Resources.salir
        Me.BtSalir.Location = New System.Drawing.Point(636, 269)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(144, 112)
        Me.BtSalir.TabIndex = 17
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'BtGrabar
        '
        Me.BtGrabar.Image = Global.Escanerdocumentos.My.Resources.Resources.guardar
        Me.BtGrabar.Location = New System.Drawing.Point(24, 254)
        Me.BtGrabar.Name = "BtGrabar"
        Me.BtGrabar.Size = New System.Drawing.Size(142, 127)
        Me.BtGrabar.TabIndex = 16
        Me.BtGrabar.Text = "Button1"
        Me.BtGrabar.UseVisualStyleBackColor = True
        '
        'BtSel
        '
        Me.BtSel.BackgroundImage = Global.Escanerdocumentos.My.Resources.Resources.ico_folder2
        Me.BtSel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtSel.Location = New System.Drawing.Point(718, 208)
        Me.BtSel.Name = "BtSel"
        Me.BtSel.Size = New System.Drawing.Size(38, 31)
        Me.BtSel.TabIndex = 15
        Me.BtSel.UseVisualStyleBackColor = True
        '
        'BtEscanear
        '
        Me.BtEscanear.Image = Global.Escanerdocumentos.My.Resources.Resources.escaneando
        Me.BtEscanear.Location = New System.Drawing.Point(620, 48)
        Me.BtEscanear.Name = "BtEscanear"
        Me.BtEscanear.Size = New System.Drawing.Size(161, 149)
        Me.BtEscanear.TabIndex = 12
        Me.BtEscanear.UseVisualStyleBackColor = True
        '
        'BtSeleccionar
        '
        Me.BtSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtSeleccionar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtSeleccionar.ForeColor = System.Drawing.Color.Red
        Me.BtSeleccionar.Image = Global.Escanerdocumentos.My.Resources.Resources.scaner
        Me.BtSeleccionar.Location = New System.Drawing.Point(20, 43)
        Me.BtSeleccionar.Name = "BtSeleccionar"
        Me.BtSeleccionar.Size = New System.Drawing.Size(155, 159)
        Me.BtSeleccionar.TabIndex = 10
        Me.BtSeleccionar.Text = "Configurar escáner"
        Me.BtSeleccionar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.BtSeleccionar.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Escanear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 400)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtGrabar)
        Me.Controls.Add(Me.BtSel)
        Me.Controls.Add(Me.TxtArchivo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtEscanear)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtSeleccionar)
        Me.Name = "Escanear"
        Me.Text = "Escanear documentos de proveedores"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRazonSocial As Label
    Friend WithEvents lblDirectorio As Label
    Friend WithEvents lblNumeroEntrada As Label
    Friend WithEvents lblEmpresa As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents BtSalir As Button
    Friend WithEvents BtGrabar As Button
    Friend WithEvents BtSel As Button
    Friend WithEvents TxtArchivo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BtEscanear As Button
    Friend WithEvents OpAlbaran As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OpFactura As RadioButton
    Friend WithEvents BtSeleccionar As Button
    Friend WithEvents OFDialog As OpenFileDialog
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Refrescar As Button
End Class
