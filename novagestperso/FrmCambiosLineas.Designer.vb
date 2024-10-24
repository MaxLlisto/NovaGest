<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCambiosLineas

Inherits LibreriaModeloMantenimiento.ModeloMantenimiento

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
 Me.components = New System.ComponentModel.Container()
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCambiosLineas))
 Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
 Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
 Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
Me.PanelSuperior = New System.Windows.Forms.Panel()
Me.CmdSalir = New System.Windows.Forms.Button()
Me.PanelCentral = New System.Windows.Forms.Panel()
Me.TabCabecera = New System.Windows.Forms.TabControl()
Me.TP00 = New System.Windows.Forms.TabPage()
Me.TP01 = New System.Windows.Forms.TabPage()
Me.TP02 = New System.Windows.Forms.TabPage()
Me.TP03 = New System.Windows.Forms.TabPage()
Me.TP04 = New System.Windows.Forms.TabPage()
Me.TP05 = New System.Windows.Forms.TabPage()
Me.TP06 = New System.Windows.Forms.TabPage()
Me.TP07 = New System.Windows.Forms.TabPage()
Me.TP08 = New System.Windows.Forms.TabPage()
Me.TP09 = New System.Windows.Forms.TabPage()
Me.CnTabla01 = New CnTabla.CnTabla()
Me.GridTabla01 = New System.Windows.Forms.DataGridView()
Me.CnEdicion001 = New CnEdicion.CnEdicion()
Me.CnEdicion002 = New CnEdicion.CnEdicion()
Me.CnEdicion003 = New CnEdicion.CnEdicion()
Me.CnEdicion004 = New CnEdicion.CnEdicion()
Me.TabGeneral = New System.Windows.Forms.TabControl()
Me.TabPage00 = New System.Windows.Forms.TabPage()
Me.TabPage01 = New System.Windows.Forms.TabPage()
Me.TabPage02 = New System.Windows.Forms.TabPage()
Me.TabPage03 = New System.Windows.Forms.TabPage()
Me.TabPage04 = New System.Windows.Forms.TabPage()
Me.TabPage05 = New System.Windows.Forms.TabPage()
Me.TabPage06 = New System.Windows.Forms.TabPage()
Me.TabPage07 = New System.Windows.Forms.TabPage()
Me.TabPage08 = New System.Windows.Forms.TabPage()
Me.TabPage09 = New System.Windows.Forms.TabPage()
 Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
Me.PanelInferior = New System.Windows.Forms.Panel()
Me.PanelSuperior.SuspendLayout()
Me.PanelCentral.SuspendLayout()
Me.TabCabecera.SuspendLayout()
Me.TabGeneral.SuspendLayout()
Me.TabPage00.SuspendLayout()
CType(Me.GridTabla01, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'PanelSuperior
'
Me.PanelSuperior.Controls.Add(Me.CnTabla01)
Me.PanelSuperior.BackColor = System.Drawing.SystemColors.Control
Me.PanelSuperior.Controls.Add(Me.CmdSalir)
Me.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top
Me.PanelSuperior.Location = New System.Drawing.Point(0, 0)
Me.PanelSuperior.Name = "PanelSuperior"
Me.PanelSuperior.Size = New System.Drawing.Size(1184, 55)
Me.PanelSuperior.TabIndex = 9999
Me.PanelSuperior.CausesValidation = False
'
'CnTabla01
'
Me.CnTabla01.Autosize = True
Me.CnTabla01.BackColor = System.Drawing.SystemColors.Control
Me.CnTabla01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.CnTabla01.CargaAlInicio = True
Me.CnTabla01.CausesValidation = False
Me.CnTabla01.EnlaceATablaPadre = -1
Me.CnTabla01.Estado = CnTabla.CnTabla.EstadoCnTabla.Inactivo
Me.CnTabla01.Formato = CnTabla.CnTabla.FormatoCnTabla.TablaPrincipalSuperior
Me.CnTabla01.HayBorrado = True
Me.CnTabla01.HayCreacion = True
Me.CnTabla01.HayDesplegar = True
Me.CnTabla01.HayModificacion = True
Me.CnTabla01.HaySeleccion = True
Me.CnTabla01.HaySiguienteAnterior = True
Me.CnTabla01.Location = New System.Drawing.Point(3,0)
Me.CnTabla01.Margin = New System.Windows.Forms.Padding(0)
Me.CnTabla01.Name = "CnTabla01"
Me.CnTabla01.Size = New System.Drawing.Size(550, 48)
Me.CnTabla01.TabIndex = 10000
Me.CnTabla01.TabStop = False
Me.CnTabla01.Tabla = "lineas"
'
'CmdSalir
'
Me.CmdSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.CmdSalir.Image = CType(resources.GetObject("CmdSalir.Image"), System.Drawing.Image)
Me.CmdSalir.Location = New System.Drawing.Point(1126, 6)
Me.CmdSalir.Name = "CmdSalir"
Me.CmdSalir.Size = New System.Drawing.Size(54, 48)
Me.CmdSalir.TabIndex = 9999
Me.CmdSalir.TabStop = False
Me.CmdSalir.UseVisualStyleBackColor = True
Me.CmdSalir.CausesValidation = False
'
'PanelCentral
'
Me.PanelCentral.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.PanelCentral.BackColor = System.Drawing.SystemColors.Control
Me.PanelCentral.Controls.Add(Me.TabCabecera)
Me.PanelCentral.Controls.Add(Me.TabGeneral)
Me.PanelCentral.Location = New System.Drawing.Point(0, 59)
Me.PanelCentral.Name = "PanelCentral"
Me.PanelCentral.Size = New System.Drawing.Size(1184, 869)
Me.PanelCentral.TabIndex = 9999
Me.PanelCentral.CausesValidation = False
'
'TabCabecera
'
Me.TabCabecera.Alignment = System.Windows.Forms.TabAlignment.Right
Me.TabCabecera.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.TabCabecera.Controls.Add(Me.TP00)
Me.TabCabecera.Controls.Add(Me.TP01)
Me.TabCabecera.Controls.Add(Me.TP02)
Me.TabCabecera.Controls.Add(Me.TP03)
Me.TabCabecera.Controls.Add(Me.TP04)
Me.TabCabecera.Controls.Add(Me.TP05)
Me.TabCabecera.Controls.Add(Me.TP06)
Me.TabCabecera.Controls.Add(Me.TP07)
Me.TabCabecera.Controls.Add(Me.TP08)
Me.TabCabecera.Controls.Add(Me.TP09)
Me.TabCabecera.ItemSize = New System.Drawing.Size(20, 20) '0, 1
Me.TabCabecera.Location = New System.Drawing.Point(2, 0)
Me.TabCabecera.Margin = New System.Windows.Forms.Padding(0)
Me.TabCabecera.Name = "TabCabecera"
Me.TabCabecera.SelectedIndex = 0
Me.TabCabecera.Size = New System.Drawing.Size(1180, 20)
Me.TabCabecera.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
Me.TabCabecera.TabIndex = 9999
Me.TabCabecera.CausesValidation = False
'
'TP00
'
Me.TP00.Location = New System.Drawing.Point(4, 5)
Me.TP00.Margin = New System.Windows.Forms.Padding(0)
Me.TP00.Name = "TP00"
Me.TP00.Size = New System.Drawing.Size(1172, 69)
Me.TP00.TabIndex = 1
Me.TP00.Text = "0"
Me.TP00.UseVisualStyleBackColor = True
Me.TP00.CausesValidation = False
Me.TP00.Text = "lineas"
'
'TP01
'
Me.TP01.Location = New System.Drawing.Point(4, 5)
Me.TP01.Name = "TP01"
Me.TP01.Size = New System.Drawing.Size(1172, 69)
Me.TP01.TabIndex = 9
Me.TP01.Text = "1"
Me.TP01.UseVisualStyleBackColor = True
Me.TP01.CausesValidation = False
'
'TP02
'
Me.TP02.Location = New System.Drawing.Point(4, 5)
Me.TP02.Name = "TP02"
Me.TP02.Size = New System.Drawing.Size(1172, 69)
Me.TP02.TabIndex = 2
Me.TP02.Text = "2"
Me.TP02.UseVisualStyleBackColor = True
Me.TP02.CausesValidation = False
'
'TP03
'
Me.TP03.Location = New System.Drawing.Point(4, 5)
Me.TP03.Name = "TP03"
Me.TP03.Size = New System.Drawing.Size(1172, 69)
Me.TP03.TabIndex = 10
Me.TP03.Text = "3"
Me.TP03.UseVisualStyleBackColor = True
Me.TP03.CausesValidation = False
'
'TP04
'
Me.TP04.Location = New System.Drawing.Point(4, 5)
Me.TP04.Name = "TP04"
Me.TP04.Size = New System.Drawing.Size(1172, 69)
Me.TP04.TabIndex = 3
Me.TP04.Text = "4"
Me.TP04.UseVisualStyleBackColor = True
Me.TP04.CausesValidation = False
'
'TP05
'
Me.TP05.Location = New System.Drawing.Point(4, 5)
Me.TP05.Name = "TP05"
Me.TP05.Size = New System.Drawing.Size(1172, 69)
Me.TP05.TabIndex = 4
Me.TP05.Text = "5"
Me.TP05.UseVisualStyleBackColor = True
Me.TP05.CausesValidation = False
'
'TP06
'
Me.TP06.Location = New System.Drawing.Point(4, 5)
Me.TP06.Name = "TP06"
Me.TP06.Size = New System.Drawing.Size(1172, 69)
Me.TP06.TabIndex = 5
Me.TP06.Text = "6"
Me.TP06.UseVisualStyleBackColor = True
Me.TP06.CausesValidation = False
'
'TP07
'
Me.TP07.Location = New System.Drawing.Point(4, 5)
Me.TP07.Name = "TP07"
Me.TP07.Size = New System.Drawing.Size(1172, 69)
Me.TP07.TabIndex = 6
Me.TP07.Text = "7"
Me.TP07.UseVisualStyleBackColor = True
Me.TP07.CausesValidation = False
'
'TP08
'
Me.TP08.Location = New System.Drawing.Point(4, 5)
Me.TP08.Name = "TP08"
Me.TP08.Size = New System.Drawing.Size(1172, 69)
Me.TP08.TabIndex = 7
Me.TP08.Text = "8"
Me.TP08.UseVisualStyleBackColor = True
Me.TP08.CausesValidation = False
'
'TP09
'
Me.TP09.Location = New System.Drawing.Point(4, 5)
Me.TP09.Name = "TP09"
Me.TP09.Size = New System.Drawing.Size(1172, 69)
Me.TP09.TabIndex = 8
Me.TP09.Text = "9"
Me.TP09.UseVisualStyleBackColor = True
Me.TP09.CausesValidation = False
'
'TabGeneral
'
Me.TabGeneral.Alignment = System.Windows.Forms.TabAlignment.Bottom
Me.TabGeneral.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
Or System.Windows.Forms.AnchorStyles.Left) _
Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.TabGeneral.CausesValidation = False
Me.TabGeneral.Controls.Add(Me.TabPage00)
Me.TabGeneral.Controls.Add(Me.TabPage01)
Me.TabGeneral.Controls.Add(Me.TabPage02)
Me.TabGeneral.Controls.Add(Me.TabPage03)
Me.TabGeneral.Controls.Add(Me.TabPage04)
Me.TabGeneral.Controls.Add(Me.TabPage05)
Me.TabGeneral.Controls.Add(Me.TabPage06)
Me.TabGeneral.Controls.Add(Me.TabPage07)
Me.TabGeneral.Controls.Add(Me.TabPage08)
Me.TabGeneral.Controls.Add(Me.TabPage09)
Me.TabGeneral.Location = New System.Drawing.Point(2, 22)
Me.TabGeneral.Margin = New System.Windows.Forms.Padding(0)
Me.TabGeneral.Name = "TabGeneral"
Me.TabGeneral.SelectedIndex = 0
Me.TabGeneral.Size = New System.Drawing.Size(1184, 845)
Me.TabGeneral.TabIndex = 0
Me.TabGeneral.CausesValidation = False
'
'TabPage00
'
Me.TabPage00.Controls.Add(Me.CnEdicion004)
Me.TabPage00.Controls.Add(Me.CnEdicion003)
Me.TabPage00.Controls.Add(Me.CnEdicion002)
Me.TabPage00.Controls.Add(Me.CnEdicion001)
Me.TabPage00.Controls.Add(Me.GridTabla01)
Me.TabPage00.Location = New System.Drawing.Point(4, 4)
Me.TabPage00.Name = "TabPage00"
Me.TabPage00.Padding = New System.Windows.Forms.Padding(3)
Me.TabPage00.Size = New System.Drawing.Size(1172, 762)
Me.TabPage00.TabIndex = 0
Me.TabPage00.Text = "0"
Me.TabPage00.UseVisualStyleBackColor = True
Me.Tabpage00.CausesValidation = False
'
'GridTabla01
'
Me.GridTabla01.Size = New System.Drawing.Size(1160, 656)
Me.GridTabla01.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
Me.GridTabla01.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
Me.GridTabla01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
Me.GridTabla01.DefaultCellStyle = DataGridViewCellStyle2
Me.GridTabla01.Location = New System.Drawing.Point(2, 12)
Me.GridTabla01.MultiSelect = False
Me.GridTabla01.Name = "GridTabla01"
Me.GridTabla01.ReadOnly = True
DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
Me.GridTabla01.TabIndex = 9999
Me.GridTabla01.TabStop = False
'
'CnEdicion001
'
Me.CnEdicion001.AceptaEspacios = True
Me.CnEdicion001.AceptaMayusculas = True
Me.CnEdicion001.AceptaMayusculasAcentuadas = False
Me.CnEdicion001.AceptaMinusculas = False
Me.CnEdicion001.AceptaMinusculasAcentuadas = False
Me.CnEdicion001.AceptaNumeros = True
Me.CnEdicion001.AceptaSimbolos = True
Me.CnEdicion001.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion001.ConvierteAMayusculas = True
Me.CnEdicion001.ConvierteAMinusculas = False
Me.CnEdicion001.EsFechaHoraCreacion = False
Me.CnEdicion001.EsFechaHoraModificacion = False
Me.CnEdicion001.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion001.ColorFondo = System.Drawing.Color.White
Me.CnEdicion001.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion001.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion001.Campo = "empresa"
Me.CnEdicion001.CampoEnlacesLookup1 = Nothing
Me.CnEdicion001.CampoEnlacesLookup2 = Nothing
Me.CnEdicion001.CampoEnlacesLookup3 = Nothing
Me.CnEdicion001.EdicionEnGrid = True
Me.CnEdicion001.TituloParaGrid = "Empresa"
Me.CnEdicion001.AnchoColumnaGrid = 160
Me.CnEdicion001.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion001.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion001.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion001.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion001.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion001.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion001.EnCreacionSoloLectura = False
Me.CnEdicion001.EnCreacionOculto = False
Me.CnEdicion001.SiempreSoloLectura = False
Me.CnEdicion001.SiempreOculto = False
Me.CnEdicion001.EnlacesLookup1 = 0
Me.CnEdicion001.EnlacesLookup2 = 0
Me.CnEdicion001.EnlacesLookup3 = 0
Me.CnEdicion001.EnModificacionSoloLectura = False
Me.CnEdicion001.EnModificacionOculto = False
Me.CnEdicion001.Fuente = Nothing
Me.CnEdicion001.HayMascaraEspecial = False
Me.CnEdicion001.HayValorDefecto = False
Me.CnEdicion001.NumeroParametroValorDefecto = 0
Me.CnEdicion001.ValorDefecto = ""
Me.CnEdicion001.HayValorFijo = True
Me.CnEdicion001.NumeroParametroValorFijo = 1
Me.CnEdicion001.ValorFijo = ""
Me.CnEdicion001.HayValorFijoCreacion = False
Me.CnEdicion001.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion001.ValorFijoCreacion = ""
Me.CnEdicion001.Location = New System.Drawing.Point(5,98)
Me.CnEdicion001.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion001.MascaraEspecial = ""
Me.CnEdicion001.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion001.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion001.MaximoNumero = 999999999999999.0R
Me.CnEdicion001.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion001.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion001.MinimoNumero = -999999999999999.0R
Me.CnEdicion001.Name = "CnEdicion001"
Me.CnEdicion001.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion001.TabIndex = 9999
Me.CnEdicion001.Tabla = "lineas"
'
'CnEdicion002
'
Me.CnEdicion002.AceptaEspacios = True
Me.CnEdicion002.AceptaMayusculas = True
Me.CnEdicion002.AceptaMayusculasAcentuadas = True
Me.CnEdicion002.AceptaMinusculas = True
Me.CnEdicion002.AceptaMinusculasAcentuadas = True
Me.CnEdicion002.AceptaNumeros = True
Me.CnEdicion002.AceptaSimbolos = False
Me.CnEdicion002.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion002.ConvierteAMayusculas = False
Me.CnEdicion002.ConvierteAMinusculas = False
Me.CnEdicion002.EsFechaHoraCreacion = False
Me.CnEdicion002.EsFechaHoraModificacion = False
Me.CnEdicion002.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion002.ColorFondo = System.Drawing.Color.White
Me.CnEdicion002.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion002.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion002.Campo = "codigo_linea"
Me.CnEdicion002.CampoEnlacesLookup1 = Nothing
Me.CnEdicion002.CampoEnlacesLookup2 = Nothing
Me.CnEdicion002.CampoEnlacesLookup3 = Nothing
Me.CnEdicion002.EdicionEnGrid = True
Me.CnEdicion002.TituloParaGrid = "Codigo linea"
Me.CnEdicion002.AnchoColumnaGrid = 115
Me.CnEdicion002.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion002.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion002.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion002.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion002.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion002.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion002.EnCreacionSoloLectura = False
Me.CnEdicion002.EnCreacionOculto = False
Me.CnEdicion002.SiempreSoloLectura = False
Me.CnEdicion002.SiempreOculto = False
Me.CnEdicion002.EnlacesLookup1 = 0
Me.CnEdicion002.EnlacesLookup2 = 0
Me.CnEdicion002.EnlacesLookup3 = 0
Me.CnEdicion002.EnModificacionSoloLectura = False
Me.CnEdicion002.EnModificacionOculto = False
Me.CnEdicion002.Fuente = Nothing
Me.CnEdicion002.HayMascaraEspecial = False
Me.CnEdicion002.HayValorDefecto = False
Me.CnEdicion002.NumeroParametroValorDefecto = 0
Me.CnEdicion002.ValorDefecto = ""
Me.CnEdicion002.HayValorFijo = False
Me.CnEdicion002.NumeroParametroValorFijo = 0
Me.CnEdicion002.ValorFijo = ""
Me.CnEdicion002.HayValorFijoCreacion = False
Me.CnEdicion002.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion002.ValorFijoCreacion = ""
Me.CnEdicion002.Location = New System.Drawing.Point(40,98)
Me.CnEdicion002.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion002.MascaraEspecial = ""
Me.CnEdicion002.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion002.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion002.MaximoNumero = 999999999999999.0R
Me.CnEdicion002.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion002.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion002.MinimoNumero = -999999999999999.0R
Me.CnEdicion002.Name = "CnEdicion002"
Me.CnEdicion002.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion002.TabIndex = 9999
Me.CnEdicion002.Tabla = "lineas"
'
'CnEdicion003
'
Me.CnEdicion003.AceptaEspacios = True
Me.CnEdicion003.AceptaMayusculas = True
Me.CnEdicion003.AceptaMayusculasAcentuadas = False
Me.CnEdicion003.AceptaMinusculas = False
Me.CnEdicion003.AceptaMinusculasAcentuadas = False
Me.CnEdicion003.AceptaNumeros = True
Me.CnEdicion003.AceptaSimbolos = True
Me.CnEdicion003.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion003.ConvierteAMayusculas = True
Me.CnEdicion003.ConvierteAMinusculas = False
Me.CnEdicion003.EsFechaHoraCreacion = False
Me.CnEdicion003.EsFechaHoraModificacion = False
Me.CnEdicion003.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion003.ColorFondo = System.Drawing.Color.White
Me.CnEdicion003.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion003.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion003.Campo = "descripcion"
Me.CnEdicion003.CampoEnlacesLookup1 = Nothing
Me.CnEdicion003.CampoEnlacesLookup2 = Nothing
Me.CnEdicion003.CampoEnlacesLookup3 = Nothing
Me.CnEdicion003.EdicionEnGrid = True
Me.CnEdicion003.TituloParaGrid = "Descripción"
Me.CnEdicion003.AnchoColumnaGrid = 250
Me.CnEdicion003.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion003.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion003.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion003.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion003.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion003.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion003.EnCreacionSoloLectura = False
Me.CnEdicion003.EnCreacionOculto = False
Me.CnEdicion003.SiempreSoloLectura = False
Me.CnEdicion003.SiempreOculto = False
Me.CnEdicion003.EnlacesLookup1 = 0
Me.CnEdicion003.EnlacesLookup2 = 0
Me.CnEdicion003.EnlacesLookup3 = 0
Me.CnEdicion003.EnModificacionSoloLectura = False
Me.CnEdicion003.EnModificacionOculto = False
Me.CnEdicion003.Fuente = Nothing
Me.CnEdicion003.HayMascaraEspecial = False
Me.CnEdicion003.HayValorDefecto = False
Me.CnEdicion003.NumeroParametroValorDefecto = 0
Me.CnEdicion003.ValorDefecto = ""
Me.CnEdicion003.HayValorFijo = False
Me.CnEdicion003.NumeroParametroValorFijo = 0
Me.CnEdicion003.ValorFijo = ""
Me.CnEdicion003.HayValorFijoCreacion = False
Me.CnEdicion003.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion003.ValorFijoCreacion = ""
Me.CnEdicion003.Location = New System.Drawing.Point(75,98)
Me.CnEdicion003.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion003.MascaraEspecial = ""
Me.CnEdicion003.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion003.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion003.MaximoNumero = 999999999999999.0R
Me.CnEdicion003.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion003.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion003.MinimoNumero = -999999999999999.0R
Me.CnEdicion003.Name = "CnEdicion003"
Me.CnEdicion003.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion003.TabIndex = 9999
Me.CnEdicion003.Tabla = "lineas"
'
'CnEdicion004
'
Me.CnEdicion004.AceptaEspacios = True
Me.CnEdicion004.AceptaMayusculas = True
Me.CnEdicion004.AceptaMayusculasAcentuadas = False
Me.CnEdicion004.AceptaMinusculas = False
Me.CnEdicion004.AceptaMinusculasAcentuadas = False
Me.CnEdicion004.AceptaNumeros = True
Me.CnEdicion004.AceptaSimbolos = True
Me.CnEdicion004.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion004.ConvierteAMayusculas = True
Me.CnEdicion004.ConvierteAMinusculas = False
Me.CnEdicion004.EsFechaHoraCreacion = False
Me.CnEdicion004.EsFechaHoraModificacion = False
Me.CnEdicion004.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion004.ColorFondo = System.Drawing.Color.White
Me.CnEdicion004.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion004.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion004.Campo = "coste_fijo_sn"
Me.CnEdicion004.CampoEnlacesLookup1 = Nothing
Me.CnEdicion004.CampoEnlacesLookup2 = Nothing
Me.CnEdicion004.CampoEnlacesLookup3 = Nothing
Me.CnEdicion004.EdicionEnGrid = True
Me.CnEdicion004.TituloParaGrid = "Coste Fijo Sn"
Me.CnEdicion004.AnchoColumnaGrid = 125
Me.CnEdicion004.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion004.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion004.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion004.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion004.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion004.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion004.EnCreacionSoloLectura = False
Me.CnEdicion004.EnCreacionOculto = False
Me.CnEdicion004.SiempreSoloLectura = False
Me.CnEdicion004.SiempreOculto = False
Me.CnEdicion004.EnlacesLookup1 = 0
Me.CnEdicion004.EnlacesLookup2 = 0
Me.CnEdicion004.EnlacesLookup3 = 0
Me.CnEdicion004.EnModificacionSoloLectura = False
Me.CnEdicion004.EnModificacionOculto = False
Me.CnEdicion004.Fuente = Nothing
Me.CnEdicion004.HayMascaraEspecial = False
Me.CnEdicion004.HayValorDefecto = True
Me.CnEdicion004.NumeroParametroValorDefecto = 0
Me.CnEdicion004.ValorDefecto = "N"
Me.CnEdicion004.HayValorFijo = False
Me.CnEdicion004.NumeroParametroValorFijo = 0
Me.CnEdicion004.ValorFijo = ""
Me.CnEdicion004.HayValorFijoCreacion = False
Me.CnEdicion004.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion004.ValorFijoCreacion = ""
Me.CnEdicion004.Location = New System.Drawing.Point(110,98)
Me.CnEdicion004.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion004.MascaraEspecial = ""
Me.CnEdicion004.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion004.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion004.MaximoNumero = 999999999999999.0R
Me.CnEdicion004.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion004.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion004.MinimoNumero = -999999999999999.0R
Me.CnEdicion004.Name = "CnEdicion004"
Me.CnEdicion004.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion004.TabIndex = 9999
Me.CnEdicion004.Tabla = "lineas"
Me.TabPage00.Text = "lineas"
'
'TabPage01
'
Me.TabPage01.Location = New System.Drawing.Point(4, 4)
Me.TabPage01.Name = "TabPage01"
Me.TabPage01.Size = New System.Drawing.Size(1172, 765)
Me.TabPage01.TabIndex = 9
Me.TabPage01.Text = "1"
Me.TabPage01.UseVisualStyleBackColor = True
Me.Tabpage01.CausesValidation = False
'
'TabPage02
'
Me.TabPage02.Location = New System.Drawing.Point(4, 4)
Me.TabPage02.Name = "TabPage02"
Me.TabPage02.Padding = New System.Windows.Forms.Padding(3)
Me.TabPage02.Size = New System.Drawing.Size(1172, 765)
Me.TabPage02.TabIndex = 1
Me.TabPage02.Text = "2"
Me.TabPage02.UseVisualStyleBackColor = True
Me.Tabpage02.CausesValidation = False
'
'TabPage03
'
Me.TabPage03.Location = New System.Drawing.Point(4, 4)
Me.TabPage03.Name = "TabPage03"
Me.TabPage03.Size = New System.Drawing.Size(1172, 765)
Me.TabPage03.TabIndex = 2
Me.TabPage03.Text = "3"
Me.TabPage03.UseVisualStyleBackColor = True
Me.Tabpage03.CausesValidation = False
'
'TabPage04
'
Me.TabPage04.Location = New System.Drawing.Point(4, 4)
Me.TabPage04.Name = "TabPage04"
Me.TabPage04.Size = New System.Drawing.Size(1172, 765)
Me.TabPage04.TabIndex = 3
Me.TabPage04.Text = "4"
Me.TabPage04.UseVisualStyleBackColor = True
Me.Tabpage04.CausesValidation = False
'
'TabPage05
'
Me.TabPage05.Location = New System.Drawing.Point(4, 4)
Me.TabPage05.Name = "TabPage05"
Me.TabPage05.Size = New System.Drawing.Size(1172, 765)
Me.TabPage05.TabIndex = 4
Me.TabPage05.Text = "5"
Me.TabPage05.UseVisualStyleBackColor = True
Me.Tabpage05.CausesValidation = False
'
'TabPage06
'
Me.TabPage06.Location = New System.Drawing.Point(4, 4)
Me.TabPage06.Name = "TabPage06"
Me.TabPage06.Size = New System.Drawing.Size(1172, 765)
Me.TabPage06.TabIndex = 5
Me.TabPage06.Text = "6"
Me.TabPage06.UseVisualStyleBackColor = True
Me.Tabpage06.CausesValidation = False
'
'TabPage07
'
Me.TabPage07.Location = New System.Drawing.Point(4, 4)
Me.TabPage07.Name = "TabPage07"
Me.TabPage07.Size = New System.Drawing.Size(1172, 765)
Me.TabPage07.TabIndex = 6
Me.TabPage07.Text = "7"
Me.TabPage07.UseVisualStyleBackColor = True
Me.Tabpage07.CausesValidation = False
'
'TabPage08
'
Me.TabPage08.Location = New System.Drawing.Point(4, 4)
Me.TabPage08.Name = "TabPage08"
Me.TabPage08.Size = New System.Drawing.Size(1172, 765)
Me.TabPage08.TabIndex = 7
Me.TabPage08.Text = "8"
Me.TabPage08.UseVisualStyleBackColor = True
Me.Tabpage08.CausesValidation = False
'
'TabPage09
'
Me.TabPage09.Location = New System.Drawing.Point(4, 4)
Me.TabPage09.Name = "TabPage09"
Me.TabPage09.Size = New System.Drawing.Size(1172, 765)
Me.TabPage09.TabIndex = 8
Me.TabPage09.Text = "9"
Me.TabPage09.UseVisualStyleBackColor = True
Me.Tabpage09.CausesValidation = False
'
'PanelInferior
'
Me.PanelInferior.BackColor = System.Drawing.SystemColors.Control
Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
Me.PanelInferior.Location = New System.Drawing.Point(0, 933)
Me.PanelInferior.Name = "PanelInferior"
Me.PanelInferior.Size = New System.Drawing.Size(1184, 28)
Me.PanelInferior.TabIndex = 9999
Me.PanelInferior.CausesValidation = False
'
'FrmCambiosLineas
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.CausesValidation = False
Me.ClientSize = New System.Drawing.Size(1184, 961)
Me.Controls.Add(Me.PanelSuperior)
Me.Controls.Add(Me.PanelCentral)
Me.Controls.Add(Me.PanelInferior)
Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Name = "FrmCambiosLineas"
Me.Text = "FrmCambiosLineas"
Me.PanelSuperior.ResumeLayout(False)
Me.PanelSuperior.PerformLayout()
Me.PanelCentral.ResumeLayout(False)
Me.TabCabecera.ResumeLayout(False)
Me.TabGeneral.ResumeLayout(False)
Me.TabPage00.ResumeLayout(False)
Me.TabPage00.PerformLayout()
Me.ResumeLayout(False)
End Sub

Friend WithEvents PanelSuperior As Panel
Friend WithEvents CmdSalir As Button
Friend WithEvents PanelCentral As Panel
Friend WithEvents PanelInferior As Panel
Friend WithEvents TabCabecera As TabControl
Friend WithEvents TP00 As TabPage
Friend WithEvents TP01 As TabPage
Friend WithEvents TP02 As TabPage
Friend WithEvents TP03 As TabPage
Friend WithEvents TP04 As TabPage
Friend WithEvents TP05 As TabPage
Friend WithEvents TP06 As TabPage
Friend WithEvents TP07 As TabPage
Friend WithEvents TP08 As TabPage
Friend WithEvents TP09 As TabPage
Friend WithEvents TabGeneral As TabControl
Friend WithEvents TabPage00 As TabPage
Friend WithEvents TabPage01 As TabPage
Friend WithEvents TabPage02 As TabPage
Friend WithEvents TabPage03 As TabPage
Friend WithEvents TabPage04 As TabPage
Friend WithEvents TabPage05 As TabPage
Friend WithEvents TabPage06 As TabPage
Friend WithEvents TabPage07 As TabPage
Friend WithEvents TabPage08 As TabPage
Friend WithEvents TabPage09 As TabPage
Friend WithEvents CnTabla01 As CnTabla.CnTabla
Friend WithEvents GridTabla01 As DataGridView
Friend WithEvents CnEdicion001 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion002 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion003 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion004 As CnEdicion.CnEdicion
 Friend WithEvents Timer1 As Timer
End Class
