<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmClasificacionfruta
    Inherits libcomunes.FormGenerico 'System.Windows.Forms.Form

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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblFechaEntrada = New System.Windows.Forms.Label()
        Me.Cargardatos = New System.Windows.Forms.Button()
        Me.TxtNumero = New System.Windows.Forms.TextBox()
        Me.TxtSerie = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGClasificacion = New System.Windows.Forms.DataGridView()
        Me.DGSumas = New System.Windows.Forms.DataGridView()
        Me.DGDefinitivos = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LblNombreSocio = New System.Windows.Forms.Label()
        Me.lblCodigo_campo = New System.Windows.Forms.Label()
        Me.lblVariedad = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTotalPorc = New System.Windows.Forms.Label()
        Me.btExcel = New System.Windows.Forms.Button()
        Me.BorrarSumas = New System.Windows.Forms.Button()
        Me.Pasaraclasificacion = New System.Windows.Forms.Button()
        Me.Moveradefinitiva = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cbBloquear = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGSumas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGDefinitivos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblFechaEntrada)
        Me.GroupBox1.Controls.Add(Me.Cargardatos)
        Me.GroupBox1.Controls.Add(Me.TxtNumero)
        Me.GroupBox1.Controls.Add(Me.TxtSerie)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(483, 51)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Documento"
        '
        'lblFechaEntrada
        '
        Me.lblFechaEntrada.AutoSize = True
        Me.lblFechaEntrada.Location = New System.Drawing.Point(313, 20)
        Me.lblFechaEntrada.Name = "lblFechaEntrada"
        Me.lblFechaEntrada.Size = New System.Drawing.Size(0, 13)
        Me.lblFechaEntrada.TabIndex = 5
        '
        'Cargardatos
        '
        Me.Cargardatos.Image = Global.Novagestvarios.My.Resources.Resources.maquina_elevadora
        Me.Cargardatos.Location = New System.Drawing.Point(403, 7)
        Me.Cargardatos.Name = "Cargardatos"
        Me.Cargardatos.Size = New System.Drawing.Size(74, 42)
        Me.Cargardatos.TabIndex = 4
        Me.Cargardatos.UseVisualStyleBackColor = True
        '
        'TxtNumero
        '
        Me.TxtNumero.Location = New System.Drawing.Point(166, 22)
        Me.TxtNumero.Name = "TxtNumero"
        Me.TxtNumero.Size = New System.Drawing.Size(113, 20)
        Me.TxtNumero.TabIndex = 3
        '
        'TxtSerie
        '
        Me.TxtSerie.Location = New System.Drawing.Point(49, 22)
        Me.TxtSerie.Name = "TxtSerie"
        Me.TxtSerie.Size = New System.Drawing.Size(49, 20)
        Me.TxtSerie.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(107, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Número"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Serie"
        '
        'DGClasificacion
        '
        Me.DGClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGClasificacion.Location = New System.Drawing.Point(13, 69)
        Me.DGClasificacion.Name = "DGClasificacion"
        Me.DGClasificacion.Size = New System.Drawing.Size(1072, 375)
        Me.DGClasificacion.TabIndex = 1
        '
        'DGSumas
        '
        Me.DGSumas.AllowUserToAddRows = False
        Me.DGSumas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGSumas.Location = New System.Drawing.Point(12, 450)
        Me.DGSumas.Name = "DGSumas"
        Me.DGSumas.Size = New System.Drawing.Size(1072, 61)
        Me.DGSumas.TabIndex = 6
        '
        'DGDefinitivos
        '
        Me.DGDefinitivos.AllowUserToAddRows = False
        Me.DGDefinitivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDefinitivos.Location = New System.Drawing.Point(12, 574)
        Me.DGDefinitivos.Name = "DGDefinitivos"
        Me.DGDefinitivos.Size = New System.Drawing.Size(1072, 61)
        Me.DGDefinitivos.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(527, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Socio:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(527, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Código de campo:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(733, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Variedad:"
        '
        'LblNombreSocio
        '
        Me.LblNombreSocio.AutoSize = True
        Me.LblNombreSocio.Location = New System.Drawing.Point(577, 18)
        Me.LblNombreSocio.Name = "LblNombreSocio"
        Me.LblNombreSocio.Size = New System.Drawing.Size(10, 13)
        Me.LblNombreSocio.TabIndex = 14
        Me.LblNombreSocio.Text = "-"
        '
        'lblCodigo_campo
        '
        Me.lblCodigo_campo.AutoSize = True
        Me.lblCodigo_campo.Location = New System.Drawing.Point(642, 41)
        Me.lblCodigo_campo.Name = "lblCodigo_campo"
        Me.lblCodigo_campo.Size = New System.Drawing.Size(10, 13)
        Me.lblCodigo_campo.TabIndex = 15
        Me.lblCodigo_campo.Text = "-"
        '
        'lblVariedad
        '
        Me.lblVariedad.AutoSize = True
        Me.lblVariedad.Location = New System.Drawing.Point(810, 41)
        Me.lblVariedad.Name = "lblVariedad"
        Me.lblVariedad.Size = New System.Drawing.Size(10, 13)
        Me.lblVariedad.TabIndex = 16
        Me.lblVariedad.Text = "-"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(855, 656)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Total porcentajes % :"
        '
        'lblTotalPorc
        '
        Me.lblTotalPorc.AutoSize = True
        Me.lblTotalPorc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPorc.Location = New System.Drawing.Point(970, 656)
        Me.lblTotalPorc.Name = "lblTotalPorc"
        Me.lblTotalPorc.Size = New System.Drawing.Size(32, 13)
        Me.lblTotalPorc.TabIndex = 18
        Me.lblTotalPorc.Text = "0.00"
        '
        'btExcel
        '
        Me.btExcel.Image = Global.Novagestvarios.My.Resources.Resources.excel1
        Me.btExcel.Location = New System.Drawing.Point(89, 643)
        Me.btExcel.Name = "btExcel"
        Me.btExcel.Size = New System.Drawing.Size(55, 44)
        Me.btExcel.TabIndex = 19
        Me.btExcel.UseVisualStyleBackColor = True
        '
        'BorrarSumas
        '
        Me.BorrarSumas.Image = Global.Novagestvarios.My.Resources.Resources.eliminar
        Me.BorrarSumas.Location = New System.Drawing.Point(1010, 517)
        Me.BorrarSumas.Name = "BorrarSumas"
        Me.BorrarSumas.Size = New System.Drawing.Size(73, 51)
        Me.BorrarSumas.TabIndex = 10
        Me.BorrarSumas.UseVisualStyleBackColor = True
        '
        'Pasaraclasificacion
        '
        Me.Pasaraclasificacion.Image = Global.Novagestvarios.My.Resources.Resources.mover_datos
        Me.Pasaraclasificacion.Location = New System.Drawing.Point(12, 641)
        Me.Pasaraclasificacion.Name = "Pasaraclasificacion"
        Me.Pasaraclasificacion.Size = New System.Drawing.Size(53, 46)
        Me.Pasaraclasificacion.TabIndex = 9
        Me.Pasaraclasificacion.UseVisualStyleBackColor = True
        '
        'Moveradefinitiva
        '
        Me.Moveradefinitiva.Image = Global.Novagestvarios.My.Resources.Resources.mover_datos
        Me.Moveradefinitiva.Location = New System.Drawing.Point(13, 517)
        Me.Moveradefinitiva.Name = "Moveradefinitiva"
        Me.Moveradefinitiva.Size = New System.Drawing.Size(52, 51)
        Me.Moveradefinitiva.TabIndex = 8
        Me.Moveradefinitiva.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.Novagestvarios.My.Resources.Resources.salir
        Me.Button1.Location = New System.Drawing.Point(1010, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 59)
        Me.Button1.TabIndex = 5
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cbBloquear
        '
        Me.cbBloquear.AutoSize = True
        Me.cbBloquear.Checked = True
        Me.cbBloquear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbBloquear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBloquear.ForeColor = System.Drawing.Color.Blue
        Me.cbBloquear.Location = New System.Drawing.Point(188, 656)
        Me.cbBloquear.Name = "cbBloquear"
        Me.cbBloquear.Size = New System.Drawing.Size(151, 17)
        Me.cbBloquear.TabIndex = 20
        Me.cbBloquear.Text = "Bloquear clasificación"
        Me.cbBloquear.UseVisualStyleBackColor = True
        '
        'FrmClasificacionfruta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1097, 693)
        Me.Controls.Add(Me.cbBloquear)
        Me.Controls.Add(Me.btExcel)
        Me.Controls.Add(Me.lblTotalPorc)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblVariedad)
        Me.Controls.Add(Me.lblCodigo_campo)
        Me.Controls.Add(Me.LblNombreSocio)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BorrarSumas)
        Me.Controls.Add(Me.Pasaraclasificacion)
        Me.Controls.Add(Me.Moveradefinitiva)
        Me.Controls.Add(Me.DGDefinitivos)
        Me.Controls.Add(Me.DGSumas)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DGClasificacion)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmClasificacionfruta"
        Me.Text = "Clasificación de la fruta"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DGClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGSumas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGDefinitivos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Cargardatos As Button
    Friend WithEvents TxtNumero As TextBox
    Friend WithEvents TxtSerie As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DGClasificacion As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents DGSumas As DataGridView
    Friend WithEvents DGDefinitivos As DataGridView
    Friend WithEvents Moveradefinitiva As Button
    Friend WithEvents Pasaraclasificacion As Button
    Friend WithEvents BorrarSumas As Button
    Friend WithEvents lblFechaEntrada As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents LblNombreSocio As Label
    Friend WithEvents lblCodigo_campo As Label
    Friend WithEvents lblVariedad As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblTotalPorc As Label
    Friend WithEvents btExcel As Button
    Friend WithEvents cbBloquear As CheckBox
End Class
