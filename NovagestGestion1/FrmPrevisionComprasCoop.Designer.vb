<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrevisionComprasCoop

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
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPrevisionComprasCoop))
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
Me.CnTabla02 = New CnTabla.CnTabla()
Me.GridTabla02 = New System.Windows.Forms.DataGridView()
Me.Lbl001 = New System.Windows.Forms.Label()
Me.TxtDatos001 = New System.Windows.Forms.TextBox()
Me.CnEdicion001 = New CnEdicion.CnEdicion()
Me.CmdGrid001 = New System.Windows.Forms.Button()
Me.TxtLookup0011 = New System.Windows.Forms.TextBox()
Me.Lbl002 = New System.Windows.Forms.Label()
Me.TxtDatos002 = New System.Windows.Forms.TextBox()
Me.CnEdicion002 = New CnEdicion.CnEdicion()
Me.CmdGrid002 = New System.Windows.Forms.Button()
Me.TxtLookup0021 = New System.Windows.Forms.TextBox()
Me.Lbl003 = New System.Windows.Forms.Label()
Me.TxtDatos003 = New System.Windows.Forms.TextBox()
Me.CnEdicion003 = New CnEdicion.CnEdicion()
Me.Lbl004 = New System.Windows.Forms.Label()
Me.TxtDatos004 = New System.Windows.Forms.TextBox()
Me.CnEdicion004 = New CnEdicion.CnEdicion()
Me.CmdFecha004 = New System.Windows.Forms.Button()
Me.Lbl005 = New System.Windows.Forms.Label()
Me.TxtDatos005 = New System.Windows.Forms.TextBox()
Me.CnEdicion005 = New CnEdicion.CnEdicion()
Me.CmdGrid005 = New System.Windows.Forms.Button()
Me.TxtLookup0051 = New System.Windows.Forms.TextBox()
Me.Lbl006 = New System.Windows.Forms.Label()
Me.TxtDatos006 = New System.Windows.Forms.TextBox()
Me.CnEdicion006 = New CnEdicion.CnEdicion()
Me.CmdGrid006 = New System.Windows.Forms.Button()
Me.TxtLookup0061 = New System.Windows.Forms.TextBox()
Me.Lbl007 = New System.Windows.Forms.Label()
Me.TxtDatos007 = New System.Windows.Forms.TextBox()
Me.CnEdicion007 = New CnEdicion.CnEdicion()
Me.Lbl008 = New System.Windows.Forms.Label()
Me.TxtDatos008 = New System.Windows.Forms.TextBox()
Me.CnEdicion008 = New CnEdicion.CnEdicion()
Me.CmdFecha008 = New System.Windows.Forms.Button()
Me.Lbl009 = New System.Windows.Forms.Label()
Me.TxtDatos009 = New System.Windows.Forms.TextBox()
Me.CnEdicion009 = New CnEdicion.CnEdicion()
Me.CmdFecha009 = New System.Windows.Forms.Button()
Me.Lbl010 = New System.Windows.Forms.Label()
Me.TxtDatos010 = New System.Windows.Forms.TextBox()
Me.CnEdicion010 = New CnEdicion.CnEdicion()
Me.CnEdicion011 = New CnEdicion.CnEdicion()
Me.CnEdicion012 = New CnEdicion.CnEdicion()
Me.CnEdicion013 = New CnEdicion.CnEdicion()
Me.CnEdicion014 = New CnEdicion.CnEdicion()
Me.CnEdicion015 = New CnEdicion.CnEdicion()
Me.CnEdicion016 = New CnEdicion.CnEdicion()
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
Me.TP00.SuspendLayout()
Me.TabGeneral.SuspendLayout()
Me.TabPage01.SuspendLayout()
CType(Me.GridTabla02, System.ComponentModel.ISupportInitialize).BeginInit()
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
Me.CnTabla01.Tabla = "prevision_compras"
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
Me.TabCabecera.Size = New System.Drawing.Size(1180, 167)
Me.TabCabecera.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
Me.TabCabecera.TabIndex = 9999
Me.TabCabecera.CausesValidation = False
'
'TP00
'
Me.TP00.Controls.Add(Me.CnEdicion010)
Me.TP00.Controls.Add(Me.TxtDatos010)
Me.TP00.Controls.Add(Me.Lbl010)
Me.TP00.Controls.Add(Me.CmdFecha009)
Me.TP00.Controls.Add(Me.CnEdicion009)
Me.TP00.Controls.Add(Me.TxtDatos009)
Me.TP00.Controls.Add(Me.Lbl009)
Me.TP00.Controls.Add(Me.CmdFecha008)
Me.TP00.Controls.Add(Me.CnEdicion008)
Me.TP00.Controls.Add(Me.TxtDatos008)
Me.TP00.Controls.Add(Me.Lbl008)
Me.TP00.Controls.Add(Me.CnEdicion007)
Me.TP00.Controls.Add(Me.TxtDatos007)
Me.TP00.Controls.Add(Me.Lbl007)
Me.TP00.Controls.Add(Me.TxtLookup0061)
Me.TP00.Controls.Add(Me.CmdGrid006)
Me.TP00.Controls.Add(Me.CnEdicion006)
Me.TP00.Controls.Add(Me.TxtDatos006)
Me.TP00.Controls.Add(Me.Lbl006)
Me.TP00.Controls.Add(Me.TxtLookup0051)
Me.TP00.Controls.Add(Me.CmdGrid005)
Me.TP00.Controls.Add(Me.CnEdicion005)
Me.TP00.Controls.Add(Me.TxtDatos005)
Me.TP00.Controls.Add(Me.Lbl005)
Me.TP00.Controls.Add(Me.CmdFecha004)
Me.TP00.Controls.Add(Me.CnEdicion004)
Me.TP00.Controls.Add(Me.TxtDatos004)
Me.TP00.Controls.Add(Me.Lbl004)
Me.TP00.Controls.Add(Me.CnEdicion003)
Me.TP00.Controls.Add(Me.TxtDatos003)
Me.TP00.Controls.Add(Me.Lbl003)
Me.TP00.Controls.Add(Me.TxtLookup0021)
Me.TP00.Controls.Add(Me.CmdGrid002)
Me.TP00.Controls.Add(Me.CnEdicion002)
Me.TP00.Controls.Add(Me.TxtDatos002)
Me.TP00.Controls.Add(Me.Lbl002)
Me.TP00.Controls.Add(Me.TxtLookup0011)
Me.TP00.Controls.Add(Me.CmdGrid001)
Me.TP00.Controls.Add(Me.CnEdicion001)
Me.TP00.Controls.Add(Me.TxtDatos001)
Me.TP00.Controls.Add(Me.Lbl001)
Me.TP00.Location = New System.Drawing.Point(4, 5)
Me.TP00.Margin = New System.Windows.Forms.Padding(0)
Me.TP00.Name = "TP00"
Me.TP00.Size = New System.Drawing.Size(1172, 69)
Me.TP00.TabIndex = 1
Me.TP00.Text = "0"
Me.TP00.UseVisualStyleBackColor = True
Me.TP00.CausesValidation = False
'
'Lbl001
'
Me.Lbl001.AutoSize = True
Me.Lbl001.Location = New System.Drawing.Point(61,10)
Me.Lbl001.Name = "Lbl001"
Me.Lbl001.Size = New System.Drawing.Size(41, 13)
Me.Lbl001.Tabindex = 9999
Me.Lbl001.Text = "Empresa:"
'
'TxtDatos001
'
Me.TxtDatos001.Location = New System.Drawing.Point(114,8)
Me.TxtDatos001.Name = "TxtDatos001"
Me.TxtDatos001.ReadOnly = True
Me.TxtDatos001.Size = New System.Drawing.Size(160, 20)
Me.TxtDatos001.Tabindex = 9999
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
Me.CnEdicion001.CampoEnlacesLookup1 = "razon_social"
Me.CnEdicion001.CampoEnlacesLookup2 = Nothing
Me.CnEdicion001.CampoEnlacesLookup3 = Nothing
Me.CnEdicion001.EdicionEnGrid = False
Me.CnEdicion001.TituloParaGrid = Nothing
Me.CnEdicion001.AnchoColumnaGrid = 0
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
Me.CnEdicion001.EnlacesLookup1 = 33183
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
Me.CnEdicion001.Location = New System.Drawing.Point(118,8)
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
Me.CnEdicion001.Tabla = "prevision_compras"
'
'CmdGrid001
'
Me.CmdGrid001.Image = CType(Resources.GetObject("CmdGrid001.Image"), System.Drawing.Image)
Me.CmdGrid001.Location = New System.Drawing.Point(275,8)
Me.CmdGrid001.Name = "CmdGrid001"
Me.CmdGrid001.Size = New System.Drawing.Size(24,22)
Me.CmdGrid001.UseVisualStyleBackColor = True
Me.CmdGrid001.Tabindex = 9999
'
'TxtLookup0011
'
Me.TxtLookup0011.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
Me.TxtLookup0011.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TxtLookup0011.Location = New System.Drawing.Point(309,11)
Me.TxtLookup0011.Name = "TxtLookup0011"
Me.TxtLookup0011.ReadOnly = True
Me.TxtLookup0011.Size = New System.Drawing.Size(250, 13)
Me.TxtLookup0011.Tabindex = 9999
'
'Lbl002
'
Me.Lbl002.AutoSize = True
Me.Lbl002.Location = New System.Drawing.Point(63,31)
Me.Lbl002.Name = "Lbl002"
Me.Lbl002.Size = New System.Drawing.Size(41, 13)
Me.Lbl002.Tabindex = 9999
Me.Lbl002.Text = "Ejercicio:"
'
'TxtDatos002
'
Me.TxtDatos002.Location = New System.Drawing.Point(114,29)
Me.TxtDatos002.Name = "TxtDatos002"
Me.TxtDatos002.ReadOnly = True
Me.TxtDatos002.Size = New System.Drawing.Size(120, 20)
Me.TxtDatos002.Tabindex = 9999
'
'CnEdicion002
'
Me.CnEdicion002.AceptaEspacios = True
Me.CnEdicion002.AceptaMayusculas = True
Me.CnEdicion002.AceptaMayusculasAcentuadas = False
Me.CnEdicion002.AceptaMinusculas = False
Me.CnEdicion002.AceptaMinusculasAcentuadas = False
Me.CnEdicion002.AceptaNumeros = True
Me.CnEdicion002.AceptaSimbolos = True
Me.CnEdicion002.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion002.ConvierteAMayusculas = True
Me.CnEdicion002.ConvierteAMinusculas = False
Me.CnEdicion002.EsFechaHoraCreacion = False
Me.CnEdicion002.EsFechaHoraModificacion = False
Me.CnEdicion002.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion002.ColorFondo = System.Drawing.Color.White
Me.CnEdicion002.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion002.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion002.Campo = "ejercicio"
Me.CnEdicion002.CampoEnlacesLookup1 = "descripcion"
Me.CnEdicion002.CampoEnlacesLookup2 = Nothing
Me.CnEdicion002.CampoEnlacesLookup3 = Nothing
Me.CnEdicion002.EdicionEnGrid = False
Me.CnEdicion002.TituloParaGrid = Nothing
Me.CnEdicion002.AnchoColumnaGrid = 0
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
Me.CnEdicion002.EnlacesLookup1 = 33184
Me.CnEdicion002.EnlacesLookup2 = 0
Me.CnEdicion002.EnlacesLookup3 = 0
Me.CnEdicion002.EnModificacionSoloLectura = False
Me.CnEdicion002.EnModificacionOculto = False
Me.CnEdicion002.Fuente = Nothing
Me.CnEdicion002.HayMascaraEspecial = False
Me.CnEdicion002.HayValorDefecto = False
Me.CnEdicion002.NumeroParametroValorDefecto = 0
Me.CnEdicion002.ValorDefecto = ""
Me.CnEdicion002.HayValorFijo = True
Me.CnEdicion002.NumeroParametroValorFijo = 3
Me.CnEdicion002.ValorFijo = ""
Me.CnEdicion002.HayValorFijoCreacion = False
Me.CnEdicion002.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion002.ValorFijoCreacion = ""
Me.CnEdicion002.Location = New System.Drawing.Point(118,29)
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
Me.CnEdicion002.Tabla = "prevision_compras"
'
'CmdGrid002
'
Me.CmdGrid002.Image = CType(Resources.GetObject("CmdGrid002.Image"), System.Drawing.Image)
Me.CmdGrid002.Location = New System.Drawing.Point(235,29)
Me.CmdGrid002.Name = "CmdGrid002"
Me.CmdGrid002.Size = New System.Drawing.Size(24,22)
Me.CmdGrid002.UseVisualStyleBackColor = True
Me.CmdGrid002.Tabindex = 9999
'
'TxtLookup0021
'
Me.TxtLookup0021.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
Me.TxtLookup0021.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TxtLookup0021.Location = New System.Drawing.Point(269,32)
Me.TxtLookup0021.Name = "TxtLookup0021"
Me.TxtLookup0021.ReadOnly = True
Me.TxtLookup0021.Size = New System.Drawing.Size(250, 13)
Me.TxtLookup0021.Tabindex = 9999
'
'Lbl003
'
Me.Lbl003.AutoSize = True
Me.Lbl003.Location = New System.Drawing.Point(69,52)
Me.Lbl003.Name = "Lbl003"
Me.Lbl003.Size = New System.Drawing.Size(41, 13)
Me.Lbl003.Tabindex = 9999
Me.Lbl003.Text = "Codigo:"
'
'TxtDatos003
'
Me.TxtDatos003.Location = New System.Drawing.Point(114,50)
Me.TxtDatos003.Name = "TxtDatos003"
Me.TxtDatos003.ReadOnly = True
Me.TxtDatos003.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos003.Tabindex = 9999
'
'CnEdicion003
'
Me.CnEdicion003.AceptaEspacios = True
Me.CnEdicion003.AceptaMayusculas = True
Me.CnEdicion003.AceptaMayusculasAcentuadas = True
Me.CnEdicion003.AceptaMinusculas = True
Me.CnEdicion003.AceptaMinusculasAcentuadas = True
Me.CnEdicion003.AceptaNumeros = True
Me.CnEdicion003.AceptaSimbolos = False
Me.CnEdicion003.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion003.ConvierteAMayusculas = False
Me.CnEdicion003.ConvierteAMinusculas = False
Me.CnEdicion003.EsFechaHoraCreacion = False
Me.CnEdicion003.EsFechaHoraModificacion = False
Me.CnEdicion003.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion003.ColorFondo = System.Drawing.Color.White
Me.CnEdicion003.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion003.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion003.Campo = "codigo"
Me.CnEdicion003.CampoEnlacesLookup1 = Nothing
Me.CnEdicion003.CampoEnlacesLookup2 = Nothing
Me.CnEdicion003.CampoEnlacesLookup3 = Nothing
Me.CnEdicion003.EdicionEnGrid = False
Me.CnEdicion003.TituloParaGrid = Nothing
Me.CnEdicion003.AnchoColumnaGrid = 0
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
Me.CnEdicion003.Location = New System.Drawing.Point(118,50)
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
Me.CnEdicion003.Tabla = "prevision_compras"
'
'Lbl004
'
Me.Lbl004.AutoSize = True
Me.Lbl004.Location = New System.Drawing.Point(402,52)
Me.Lbl004.Name = "Lbl004"
Me.Lbl004.Size = New System.Drawing.Size(41, 13)
Me.Lbl004.Tabindex = 9999
Me.Lbl004.Text = "Fecha Incidencia:"
'
'TxtDatos004
'
Me.TxtDatos004.Location = New System.Drawing.Point(494,50)
Me.TxtDatos004.Name = "TxtDatos004"
Me.TxtDatos004.ReadOnly = True
Me.TxtDatos004.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos004.Tabindex = 9999
'
'CnEdicion004
'
Me.CnEdicion004.AceptaEspacios = True
Me.CnEdicion004.AceptaMayusculas = True
Me.CnEdicion004.AceptaMayusculasAcentuadas = True
Me.CnEdicion004.AceptaMinusculas = True
Me.CnEdicion004.AceptaMinusculasAcentuadas = True
Me.CnEdicion004.AceptaNumeros = True
Me.CnEdicion004.AceptaSimbolos = False
Me.CnEdicion004.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion004.ConvierteAMayusculas = False
Me.CnEdicion004.ConvierteAMinusculas = False
Me.CnEdicion004.EsFechaHoraCreacion = False
Me.CnEdicion004.EsFechaHoraModificacion = False
Me.CnEdicion004.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion004.ColorFondo = System.Drawing.Color.White
Me.CnEdicion004.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion004.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion004.Campo = "fecha"
Me.CnEdicion004.CampoEnlacesLookup1 = Nothing
Me.CnEdicion004.CampoEnlacesLookup2 = Nothing
Me.CnEdicion004.CampoEnlacesLookup3 = Nothing
Me.CnEdicion004.EdicionEnGrid = False
Me.CnEdicion004.TituloParaGrid = Nothing
Me.CnEdicion004.AnchoColumnaGrid = 0
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
Me.CnEdicion004.HayValorDefecto = False
Me.CnEdicion004.NumeroParametroValorDefecto = 0
Me.CnEdicion004.ValorDefecto = ""
Me.CnEdicion004.HayValorFijo = False
Me.CnEdicion004.NumeroParametroValorFijo = 0
Me.CnEdicion004.ValorFijo = ""
Me.CnEdicion004.HayValorFijoCreacion = False
Me.CnEdicion004.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion004.ValorFijoCreacion = ""
Me.CnEdicion004.Location = New System.Drawing.Point(498,50)
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
Me.CnEdicion004.Tabla = "prevision_compras"
'
'CmdFecha004
'
Me.CmdFecha004.Image = CType(Resources.GetObject("CmdFecha004.Image"), System.Drawing.Image)
Me.CmdFecha004.Location = New System.Drawing.Point(595,50)
Me.CmdFecha004.Name = "CmdFecha004"
Me.CmdFecha004.Size = New System.Drawing.Size(24,22)
Me.CmdFecha004.UseVisualStyleBackColor = True
Me.CmdFecha004.Tabindex = 9999
'
'Lbl005
'
Me.Lbl005.AutoSize = True
Me.Lbl005.Location = New System.Drawing.Point(24,73)
Me.Lbl005.Name = "Lbl005"
Me.Lbl005.Size = New System.Drawing.Size(41, 13)
Me.Lbl005.Tabindex = 9999
Me.Lbl005.Text = "Código variedad:"
'
'TxtDatos005
'
Me.TxtDatos005.Location = New System.Drawing.Point(114,71)
Me.TxtDatos005.Name = "TxtDatos005"
Me.TxtDatos005.ReadOnly = True
Me.TxtDatos005.Size = New System.Drawing.Size(64, 20)
Me.TxtDatos005.Tabindex = 9999
'
'CnEdicion005
'
Me.CnEdicion005.AceptaEspacios = True
Me.CnEdicion005.AceptaMayusculas = True
Me.CnEdicion005.AceptaMayusculasAcentuadas = False
Me.CnEdicion005.AceptaMinusculas = False
Me.CnEdicion005.AceptaMinusculasAcentuadas = False
Me.CnEdicion005.AceptaNumeros = True
Me.CnEdicion005.AceptaSimbolos = True
Me.CnEdicion005.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion005.ConvierteAMayusculas = True
Me.CnEdicion005.ConvierteAMinusculas = False
Me.CnEdicion005.EsFechaHoraCreacion = False
Me.CnEdicion005.EsFechaHoraModificacion = False
Me.CnEdicion005.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion005.ColorFondo = System.Drawing.Color.White
Me.CnEdicion005.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion005.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion005.Campo = "codigo_variedad"
Me.CnEdicion005.CampoEnlacesLookup1 = "descripcion"
Me.CnEdicion005.CampoEnlacesLookup2 = Nothing
Me.CnEdicion005.CampoEnlacesLookup3 = Nothing
Me.CnEdicion005.EdicionEnGrid = False
Me.CnEdicion005.TituloParaGrid = Nothing
Me.CnEdicion005.AnchoColumnaGrid = 0
Me.CnEdicion005.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion005.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion005.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion005.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion005.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion005.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion005.EnCreacionSoloLectura = False
Me.CnEdicion005.EnCreacionOculto = False
Me.CnEdicion005.SiempreSoloLectura = False
Me.CnEdicion005.SiempreOculto = False
Me.CnEdicion005.EnlacesLookup1 = 33185
Me.CnEdicion005.EnlacesLookup2 = 0
Me.CnEdicion005.EnlacesLookup3 = 0
Me.CnEdicion005.EnModificacionSoloLectura = False
Me.CnEdicion005.EnModificacionOculto = False
Me.CnEdicion005.Fuente = Nothing
Me.CnEdicion005.HayMascaraEspecial = False
Me.CnEdicion005.HayValorDefecto = False
Me.CnEdicion005.NumeroParametroValorDefecto = 0
Me.CnEdicion005.ValorDefecto = ""
Me.CnEdicion005.HayValorFijo = False
Me.CnEdicion005.NumeroParametroValorFijo = 0
Me.CnEdicion005.ValorFijo = ""
Me.CnEdicion005.HayValorFijoCreacion = False
Me.CnEdicion005.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion005.ValorFijoCreacion = ""
Me.CnEdicion005.Location = New System.Drawing.Point(118,71)
Me.CnEdicion005.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion005.MascaraEspecial = ""
Me.CnEdicion005.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion005.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion005.MaximoNumero = 999999999999999.0R
Me.CnEdicion005.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion005.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion005.MinimoNumero = -999999999999999.0R
Me.CnEdicion005.Name = "CnEdicion005"
Me.CnEdicion005.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion005.TabIndex = 9999
Me.CnEdicion005.Tabla = "prevision_compras"
'
'CmdGrid005
'
Me.CmdGrid005.Image = CType(Resources.GetObject("CmdGrid005.Image"), System.Drawing.Image)
Me.CmdGrid005.Location = New System.Drawing.Point(179,71)
Me.CmdGrid005.Name = "CmdGrid005"
Me.CmdGrid005.Size = New System.Drawing.Size(24,22)
Me.CmdGrid005.UseVisualStyleBackColor = True
Me.CmdGrid005.Tabindex = 9999
'
'TxtLookup0051
'
Me.TxtLookup0051.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
Me.TxtLookup0051.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TxtLookup0051.Location = New System.Drawing.Point(213,74)
Me.TxtLookup0051.Name = "TxtLookup0051"
Me.TxtLookup0051.ReadOnly = True
Me.TxtLookup0051.Size = New System.Drawing.Size(250, 13)
Me.TxtLookup0051.Tabindex = 9999
'
'Lbl006
'
Me.Lbl006.AutoSize = True
Me.Lbl006.Location = New System.Drawing.Point(30,94)
Me.Lbl006.Name = "Lbl006"
Me.Lbl006.Size = New System.Drawing.Size(41, 13)
Me.Lbl006.Tabindex = 9999
Me.Lbl006.Text = "Cod Proveedor:"
'
'TxtDatos006
'
Me.TxtDatos006.Location = New System.Drawing.Point(114,92)
Me.TxtDatos006.Name = "TxtDatos006"
Me.TxtDatos006.ReadOnly = True
Me.TxtDatos006.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos006.Tabindex = 9999
'
'CnEdicion006
'
Me.CnEdicion006.AceptaEspacios = True
Me.CnEdicion006.AceptaMayusculas = True
Me.CnEdicion006.AceptaMayusculasAcentuadas = True
Me.CnEdicion006.AceptaMinusculas = True
Me.CnEdicion006.AceptaMinusculasAcentuadas = True
Me.CnEdicion006.AceptaNumeros = True
Me.CnEdicion006.AceptaSimbolos = False
Me.CnEdicion006.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion006.ConvierteAMayusculas = False
Me.CnEdicion006.ConvierteAMinusculas = False
Me.CnEdicion006.EsFechaHoraCreacion = False
Me.CnEdicion006.EsFechaHoraModificacion = False
Me.CnEdicion006.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion006.ColorFondo = System.Drawing.Color.White
Me.CnEdicion006.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion006.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion006.Campo = "cod_proveedor"
Me.CnEdicion006.CampoEnlacesLookup1 = "razon_social"
Me.CnEdicion006.CampoEnlacesLookup2 = Nothing
Me.CnEdicion006.CampoEnlacesLookup3 = Nothing
Me.CnEdicion006.EdicionEnGrid = False
Me.CnEdicion006.TituloParaGrid = Nothing
Me.CnEdicion006.AnchoColumnaGrid = 0
Me.CnEdicion006.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion006.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion006.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion006.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion006.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion006.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion006.EnCreacionSoloLectura = False
Me.CnEdicion006.EnCreacionOculto = False
Me.CnEdicion006.SiempreSoloLectura = False
Me.CnEdicion006.SiempreOculto = False
Me.CnEdicion006.EnlacesLookup1 = 33186
Me.CnEdicion006.EnlacesLookup2 = 0
Me.CnEdicion006.EnlacesLookup3 = 0
Me.CnEdicion006.EnModificacionSoloLectura = False
Me.CnEdicion006.EnModificacionOculto = False
Me.CnEdicion006.Fuente = Nothing
Me.CnEdicion006.HayMascaraEspecial = False
Me.CnEdicion006.HayValorDefecto = False
Me.CnEdicion006.NumeroParametroValorDefecto = 0
Me.CnEdicion006.ValorDefecto = ""
Me.CnEdicion006.HayValorFijo = False
Me.CnEdicion006.NumeroParametroValorFijo = 0
Me.CnEdicion006.ValorFijo = ""
Me.CnEdicion006.HayValorFijoCreacion = False
Me.CnEdicion006.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion006.ValorFijoCreacion = ""
Me.CnEdicion006.Location = New System.Drawing.Point(118,92)
Me.CnEdicion006.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion006.MascaraEspecial = ""
Me.CnEdicion006.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion006.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion006.MaximoNumero = 999999999999999.0R
Me.CnEdicion006.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion006.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion006.MinimoNumero = -999999999999999.0R
Me.CnEdicion006.Name = "CnEdicion006"
Me.CnEdicion006.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion006.TabIndex = 9999
Me.CnEdicion006.Tabla = "prevision_compras"
'
'CmdGrid006
'
Me.CmdGrid006.Image = CType(Resources.GetObject("CmdGrid006.Image"), System.Drawing.Image)
Me.CmdGrid006.Location = New System.Drawing.Point(215,92)
Me.CmdGrid006.Name = "CmdGrid006"
Me.CmdGrid006.Size = New System.Drawing.Size(24,22)
Me.CmdGrid006.UseVisualStyleBackColor = True
Me.CmdGrid006.Tabindex = 9999
'
'TxtLookup0061
'
Me.TxtLookup0061.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
Me.TxtLookup0061.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TxtLookup0061.Location = New System.Drawing.Point(249,95)
Me.TxtLookup0061.Name = "TxtLookup0061"
Me.TxtLookup0061.ReadOnly = True
Me.TxtLookup0061.Size = New System.Drawing.Size(250, 13)
Me.TxtLookup0061.Tabindex = 9999
'
'Lbl007
'
Me.Lbl007.AutoSize = True
Me.Lbl007.Location = New System.Drawing.Point(79,115)
Me.Lbl007.Name = "Lbl007"
Me.Lbl007.Size = New System.Drawing.Size(41, 13)
Me.Lbl007.Tabindex = 9999
Me.Lbl007.Text = "Peso:"
'
'TxtDatos007
'
Me.TxtDatos007.Location = New System.Drawing.Point(114,113)
Me.TxtDatos007.Name = "TxtDatos007"
Me.TxtDatos007.ReadOnly = True
Me.TxtDatos007.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos007.Tabindex = 9999
'
'CnEdicion007
'
Me.CnEdicion007.AceptaEspacios = True
Me.CnEdicion007.AceptaMayusculas = True
Me.CnEdicion007.AceptaMayusculasAcentuadas = True
Me.CnEdicion007.AceptaMinusculas = True
Me.CnEdicion007.AceptaMinusculasAcentuadas = True
Me.CnEdicion007.AceptaNumeros = True
Me.CnEdicion007.AceptaSimbolos = False
Me.CnEdicion007.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion007.ConvierteAMayusculas = False
Me.CnEdicion007.ConvierteAMinusculas = False
Me.CnEdicion007.EsFechaHoraCreacion = False
Me.CnEdicion007.EsFechaHoraModificacion = False
Me.CnEdicion007.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion007.ColorFondo = System.Drawing.Color.White
Me.CnEdicion007.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion007.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion007.Campo = "peso"
Me.CnEdicion007.CampoEnlacesLookup1 = Nothing
Me.CnEdicion007.CampoEnlacesLookup2 = Nothing
Me.CnEdicion007.CampoEnlacesLookup3 = Nothing
Me.CnEdicion007.EdicionEnGrid = False
Me.CnEdicion007.TituloParaGrid = Nothing
Me.CnEdicion007.AnchoColumnaGrid = 0
Me.CnEdicion007.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion007.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion007.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion007.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion007.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion007.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion007.EnCreacionSoloLectura = False
Me.CnEdicion007.EnCreacionOculto = False
Me.CnEdicion007.SiempreSoloLectura = False
Me.CnEdicion007.SiempreOculto = False
Me.CnEdicion007.EnlacesLookup1 = 0
Me.CnEdicion007.EnlacesLookup2 = 0
Me.CnEdicion007.EnlacesLookup3 = 0
Me.CnEdicion007.EnModificacionSoloLectura = False
Me.CnEdicion007.EnModificacionOculto = False
Me.CnEdicion007.Fuente = Nothing
Me.CnEdicion007.HayMascaraEspecial = False
Me.CnEdicion007.HayValorDefecto = False
Me.CnEdicion007.NumeroParametroValorDefecto = 0
Me.CnEdicion007.ValorDefecto = ""
Me.CnEdicion007.HayValorFijo = False
Me.CnEdicion007.NumeroParametroValorFijo = 0
Me.CnEdicion007.ValorFijo = ""
Me.CnEdicion007.HayValorFijoCreacion = False
Me.CnEdicion007.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion007.ValorFijoCreacion = ""
Me.CnEdicion007.Location = New System.Drawing.Point(118,113)
Me.CnEdicion007.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion007.MascaraEspecial = ""
Me.CnEdicion007.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion007.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion007.MaximoNumero = 999999999999999.0R
Me.CnEdicion007.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion007.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion007.MinimoNumero = -999999999999999.0R
Me.CnEdicion007.Name = "CnEdicion007"
Me.CnEdicion007.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion007.TabIndex = 9999
Me.CnEdicion007.Tabla = "prevision_compras"
'
'Lbl008
'
Me.Lbl008.AutoSize = True
Me.Lbl008.Location = New System.Drawing.Point(381,115)
Me.Lbl008.Name = "Lbl008"
Me.Lbl008.Size = New System.Drawing.Size(41, 13)
Me.Lbl008.Tabindex = 9999
Me.Lbl008.Text = "Fecha prevista servir:"
'
'TxtDatos008
'
Me.TxtDatos008.Location = New System.Drawing.Point(494,113)
Me.TxtDatos008.Name = "TxtDatos008"
Me.TxtDatos008.ReadOnly = True
Me.TxtDatos008.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos008.Tabindex = 9999
'
'CnEdicion008
'
Me.CnEdicion008.AceptaEspacios = True
Me.CnEdicion008.AceptaMayusculas = True
Me.CnEdicion008.AceptaMayusculasAcentuadas = True
Me.CnEdicion008.AceptaMinusculas = True
Me.CnEdicion008.AceptaMinusculasAcentuadas = True
Me.CnEdicion008.AceptaNumeros = True
Me.CnEdicion008.AceptaSimbolos = False
Me.CnEdicion008.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion008.ConvierteAMayusculas = False
Me.CnEdicion008.ConvierteAMinusculas = False
Me.CnEdicion008.EsFechaHoraCreacion = False
Me.CnEdicion008.EsFechaHoraModificacion = False
Me.CnEdicion008.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion008.ColorFondo = System.Drawing.Color.White
Me.CnEdicion008.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion008.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion008.Campo = "fecha_prevista"
Me.CnEdicion008.CampoEnlacesLookup1 = Nothing
Me.CnEdicion008.CampoEnlacesLookup2 = Nothing
Me.CnEdicion008.CampoEnlacesLookup3 = Nothing
Me.CnEdicion008.EdicionEnGrid = False
Me.CnEdicion008.TituloParaGrid = Nothing
Me.CnEdicion008.AnchoColumnaGrid = 0
Me.CnEdicion008.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion008.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion008.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion008.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion008.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion008.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion008.EnCreacionSoloLectura = False
Me.CnEdicion008.EnCreacionOculto = False
Me.CnEdicion008.SiempreSoloLectura = False
Me.CnEdicion008.SiempreOculto = False
Me.CnEdicion008.EnlacesLookup1 = 0
Me.CnEdicion008.EnlacesLookup2 = 0
Me.CnEdicion008.EnlacesLookup3 = 0
Me.CnEdicion008.EnModificacionSoloLectura = False
Me.CnEdicion008.EnModificacionOculto = False
Me.CnEdicion008.Fuente = Nothing
Me.CnEdicion008.HayMascaraEspecial = False
Me.CnEdicion008.HayValorDefecto = False
Me.CnEdicion008.NumeroParametroValorDefecto = 0
Me.CnEdicion008.ValorDefecto = ""
Me.CnEdicion008.HayValorFijo = False
Me.CnEdicion008.NumeroParametroValorFijo = 0
Me.CnEdicion008.ValorFijo = ""
Me.CnEdicion008.HayValorFijoCreacion = False
Me.CnEdicion008.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion008.ValorFijoCreacion = ""
Me.CnEdicion008.Location = New System.Drawing.Point(498,113)
Me.CnEdicion008.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion008.MascaraEspecial = ""
Me.CnEdicion008.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion008.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion008.MaximoNumero = 999999999999999.0R
Me.CnEdicion008.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion008.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion008.MinimoNumero = -999999999999999.0R
Me.CnEdicion008.Name = "CnEdicion008"
Me.CnEdicion008.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion008.TabIndex = 9999
Me.CnEdicion008.Tabla = "prevision_compras"
'
'CmdFecha008
'
Me.CmdFecha008.Image = CType(Resources.GetObject("CmdFecha008.Image"), System.Drawing.Image)
Me.CmdFecha008.Location = New System.Drawing.Point(595,113)
Me.CmdFecha008.Name = "CmdFecha008"
Me.CmdFecha008.Size = New System.Drawing.Size(24,22)
Me.CmdFecha008.UseVisualStyleBackColor = True
Me.CmdFecha008.Tabindex = 9999
'
'Lbl009
'
Me.Lbl009.AutoSize = True
Me.Lbl009.Location = New System.Drawing.Point(33,136)
Me.Lbl009.Name = "Lbl009"
Me.Lbl009.Size = New System.Drawing.Size(41, 13)
Me.Lbl009.Tabindex = 9999
Me.Lbl009.Text = "Fecha Llegada:"
'
'TxtDatos009
'
Me.TxtDatos009.Location = New System.Drawing.Point(114,134)
Me.TxtDatos009.Name = "TxtDatos009"
Me.TxtDatos009.ReadOnly = True
Me.TxtDatos009.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos009.Tabindex = 9999
'
'CnEdicion009
'
Me.CnEdicion009.AceptaEspacios = True
Me.CnEdicion009.AceptaMayusculas = True
Me.CnEdicion009.AceptaMayusculasAcentuadas = True
Me.CnEdicion009.AceptaMinusculas = True
Me.CnEdicion009.AceptaMinusculasAcentuadas = True
Me.CnEdicion009.AceptaNumeros = True
Me.CnEdicion009.AceptaSimbolos = False
Me.CnEdicion009.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion009.ConvierteAMayusculas = False
Me.CnEdicion009.ConvierteAMinusculas = False
Me.CnEdicion009.EsFechaHoraCreacion = False
Me.CnEdicion009.EsFechaHoraModificacion = False
Me.CnEdicion009.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion009.ColorFondo = System.Drawing.Color.White
Me.CnEdicion009.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion009.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion009.Campo = "fecha_llegada"
Me.CnEdicion009.CampoEnlacesLookup1 = Nothing
Me.CnEdicion009.CampoEnlacesLookup2 = Nothing
Me.CnEdicion009.CampoEnlacesLookup3 = Nothing
Me.CnEdicion009.EdicionEnGrid = False
Me.CnEdicion009.TituloParaGrid = Nothing
Me.CnEdicion009.AnchoColumnaGrid = 0
Me.CnEdicion009.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion009.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion009.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion009.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion009.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion009.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion009.EnCreacionSoloLectura = False
Me.CnEdicion009.EnCreacionOculto = False
Me.CnEdicion009.SiempreSoloLectura = False
Me.CnEdicion009.SiempreOculto = False
Me.CnEdicion009.EnlacesLookup1 = 0
Me.CnEdicion009.EnlacesLookup2 = 0
Me.CnEdicion009.EnlacesLookup3 = 0
Me.CnEdicion009.EnModificacionSoloLectura = False
Me.CnEdicion009.EnModificacionOculto = False
Me.CnEdicion009.Fuente = Nothing
Me.CnEdicion009.HayMascaraEspecial = False
Me.CnEdicion009.HayValorDefecto = False
Me.CnEdicion009.NumeroParametroValorDefecto = 0
Me.CnEdicion009.ValorDefecto = ""
Me.CnEdicion009.HayValorFijo = False
Me.CnEdicion009.NumeroParametroValorFijo = 0
Me.CnEdicion009.ValorFijo = ""
Me.CnEdicion009.HayValorFijoCreacion = False
Me.CnEdicion009.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion009.ValorFijoCreacion = ""
Me.CnEdicion009.Location = New System.Drawing.Point(118,134)
Me.CnEdicion009.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion009.MascaraEspecial = ""
Me.CnEdicion009.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion009.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion009.MaximoNumero = 999999999999999.0R
Me.CnEdicion009.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion009.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion009.MinimoNumero = -999999999999999.0R
Me.CnEdicion009.Name = "CnEdicion009"
Me.CnEdicion009.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion009.TabIndex = 9999
Me.CnEdicion009.Tabla = "prevision_compras"
'
'CmdFecha009
'
Me.CmdFecha009.Image = CType(Resources.GetObject("CmdFecha009.Image"), System.Drawing.Image)
Me.CmdFecha009.Location = New System.Drawing.Point(215,134)
Me.CmdFecha009.Name = "CmdFecha009"
Me.CmdFecha009.Size = New System.Drawing.Size(24,22)
Me.CmdFecha009.UseVisualStyleBackColor = True
Me.CmdFecha009.Tabindex = 9999
'
'Lbl010
'
Me.Lbl010.AutoSize = True
Me.Lbl010.Location = New System.Drawing.Point(439,136)
Me.Lbl010.Name = "Lbl010"
Me.Lbl010.Size = New System.Drawing.Size(41, 13)
Me.Lbl010.Tabindex = 9999
Me.Lbl010.Text = "Situación:"
'
'TxtDatos010
'
Me.TxtDatos010.Location = New System.Drawing.Point(494,134)
Me.TxtDatos010.Name = "TxtDatos010"
Me.TxtDatos010.ReadOnly = True
Me.TxtDatos010.Size = New System.Drawing.Size(20, 20)
Me.TxtDatos010.Tabindex = 9999
'
'CnEdicion010
'
Me.CnEdicion010.AceptaEspacios = True
Me.CnEdicion010.AceptaMayusculas = True
Me.CnEdicion010.AceptaMayusculasAcentuadas = False
Me.CnEdicion010.AceptaMinusculas = False
Me.CnEdicion010.AceptaMinusculasAcentuadas = False
Me.CnEdicion010.AceptaNumeros = True
Me.CnEdicion010.AceptaSimbolos = True
Me.CnEdicion010.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion010.ConvierteAMayusculas = True
Me.CnEdicion010.ConvierteAMinusculas = False
Me.CnEdicion010.EsFechaHoraCreacion = False
Me.CnEdicion010.EsFechaHoraModificacion = False
Me.CnEdicion010.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion010.ColorFondo = System.Drawing.Color.White
Me.CnEdicion010.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion010.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion010.Campo = "situacion"
Me.CnEdicion010.CampoEnlacesLookup1 = Nothing
Me.CnEdicion010.CampoEnlacesLookup2 = Nothing
Me.CnEdicion010.CampoEnlacesLookup3 = Nothing
Me.CnEdicion010.EdicionEnGrid = False
Me.CnEdicion010.TituloParaGrid = Nothing
Me.CnEdicion010.AnchoColumnaGrid = 0
Me.CnEdicion010.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion010.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion010.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion010.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion010.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion010.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion010.EnCreacionSoloLectura = False
Me.CnEdicion010.EnCreacionOculto = False
Me.CnEdicion010.SiempreSoloLectura = False
Me.CnEdicion010.SiempreOculto = False
Me.CnEdicion010.EnlacesLookup1 = 0
Me.CnEdicion010.EnlacesLookup2 = 0
Me.CnEdicion010.EnlacesLookup3 = 0
Me.CnEdicion010.EnModificacionSoloLectura = False
Me.CnEdicion010.EnModificacionOculto = False
Me.CnEdicion010.Fuente = Nothing
Me.CnEdicion010.HayMascaraEspecial = False
Me.CnEdicion010.HayValorDefecto = True
Me.CnEdicion010.NumeroParametroValorDefecto = 0
Me.CnEdicion010.ValorDefecto = "N"
Me.CnEdicion010.HayValorFijo = False
Me.CnEdicion010.NumeroParametroValorFijo = 0
Me.CnEdicion010.ValorFijo = ""
Me.CnEdicion010.HayValorFijoCreacion = False
Me.CnEdicion010.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion010.ValorFijoCreacion = ""
Me.CnEdicion010.Location = New System.Drawing.Point(498,134)
Me.CnEdicion010.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion010.MascaraEspecial = ""
Me.CnEdicion010.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion010.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion010.MaximoNumero = 999999999999999.0R
Me.CnEdicion010.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion010.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion010.MinimoNumero = -999999999999999.0R
Me.CnEdicion010.Name = "CnEdicion010"
Me.CnEdicion010.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion010.TabIndex = 9999
Me.CnEdicion010.Tabla = "prevision_compras"
Me.TP00.Text = "prevision_compras"
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
Me.TabGeneral.Location = New System.Drawing.Point(2, 169)
Me.TabGeneral.Margin = New System.Windows.Forms.Padding(0)
Me.TabGeneral.Name = "TabGeneral"
Me.TabGeneral.SelectedIndex = 0
Me.TabGeneral.Size = New System.Drawing.Size(1184, 698)
Me.TabGeneral.TabIndex = 0
Me.TabGeneral.CausesValidation = False
'
'TabPage00
'
Me.TabPage00.Location = New System.Drawing.Point(4, 4)
Me.TabPage00.Name = "TabPage00"
Me.TabPage00.Padding = New System.Windows.Forms.Padding(3)
Me.TabPage00.Size = New System.Drawing.Size(1172, 762)
Me.TabPage00.TabIndex = 0
Me.TabPage00.Text = "0"
Me.TabPage00.UseVisualStyleBackColor = True
Me.Tabpage00.CausesValidation = False
Me.TabPage00.Text = "prevision_compras"
'
'TabPage01
'
Me.TabPage01.Controls.Add(Me.CnEdicion016)
Me.TabPage01.Controls.Add(Me.CnEdicion015)
Me.TabPage01.Controls.Add(Me.CnEdicion014)
Me.TabPage01.Controls.Add(Me.CnEdicion013)
Me.TabPage01.Controls.Add(Me.CnEdicion012)
Me.TabPage01.Controls.Add(Me.CnEdicion011)
Me.TabPage01.Controls.Add(Me.GridTabla02)
Me.TabPage01.Controls.Add(Me.CnTabla02)
Me.TabPage01.Location = New System.Drawing.Point(4, 4)
Me.TabPage01.Name = "TabPage01"
Me.TabPage01.Size = New System.Drawing.Size(1172, 765)
Me.TabPage01.TabIndex = 9
Me.TabPage01.Text = "1"
Me.TabPage01.UseVisualStyleBackColor = True
Me.Tabpage01.CausesValidation = False
'
'CnTabla02
'
Me.CnTabla02.Autosize = True
Me.CnTabla02.BackColor = System.Drawing.SystemColors.Control
Me.CnTabla02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
Me.CnTabla02.CargaAlInicio = True
Me.CnTabla02.CausesValidation = False
Me.CnTabla02.EnlaceATablaPadre = 33189
Me.CnTabla02.Estado = CnTabla.CnTabla.EstadoCnTabla.Inactivo
Me.CnTabla02.Formato = CnTabla.CnTabla.FormatoCnTabla.TablaSecundariaSuperior
Me.CnTabla02.HayBorrado = True
Me.CnTabla02.HayCreacion = True
Me.CnTabla02.HayDesplegar = True
Me.CnTabla02.HayModificacion = True
Me.CnTabla02.HaySeleccion = True
Me.CnTabla02.HaySiguienteAnterior = True
Me.CnTabla02.Location = New System.Drawing.Point(116,0)
Me.CnTabla02.Margin = New System.Windows.Forms.Padding(0)
Me.CnTabla02.Name = "CnTabla02"
Me.CnTabla02.Size = New System.Drawing.Size(550, 48)
Me.CnTabla02.TabIndex = 10000
Me.CnTabla02.TabStop = False
Me.CnTabla02.Tabla = "prev_compras_clasif"
'
'GridTabla02
'
Me.GridTabla02.Size = New System.Drawing.Size(1160, 425)
Me.GridTabla02.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
Me.GridTabla02.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
Me.GridTabla02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
Me.GridTabla02.DefaultCellStyle = DataGridViewCellStyle2
Me.GridTabla02.Location = New System.Drawing.Point(2, 75)
Me.GridTabla02.MultiSelect = False
Me.GridTabla02.Name = "GridTabla02"
Me.GridTabla02.ReadOnly = True
DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
Me.GridTabla02.TabIndex = 9999
Me.GridTabla02.TabStop = False
'
'CnEdicion011
'
Me.CnEdicion011.AceptaEspacios = True
Me.CnEdicion011.AceptaMayusculas = True
Me.CnEdicion011.AceptaMayusculasAcentuadas = False
Me.CnEdicion011.AceptaMinusculas = False
Me.CnEdicion011.AceptaMinusculasAcentuadas = False
Me.CnEdicion011.AceptaNumeros = True
Me.CnEdicion011.AceptaSimbolos = True
Me.CnEdicion011.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion011.ConvierteAMayusculas = True
Me.CnEdicion011.ConvierteAMinusculas = False
Me.CnEdicion011.EsFechaHoraCreacion = False
Me.CnEdicion011.EsFechaHoraModificacion = False
Me.CnEdicion011.ColorFondoControl = System.Drawing.Color.Blue
Me.CnEdicion011.ColorFondo = System.Drawing.Color.SkyBlue
Me.CnEdicion011.ColorFondoRequerido = System.Drawing.Color.SkyBlue
Me.CnEdicion011.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion011.Campo = "empresa"
Me.CnEdicion011.CampoEnlacesLookup1 = Nothing
Me.CnEdicion011.CampoEnlacesLookup2 = Nothing
Me.CnEdicion011.CampoEnlacesLookup3 = Nothing
Me.CnEdicion011.EdicionEnGrid = True
Me.CnEdicion011.TituloParaGrid = "Empresa"
Me.CnEdicion011.AnchoColumnaGrid = 58
Me.CnEdicion011.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion011.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion011.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion011.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion011.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion011.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion011.EnCreacionSoloLectura = False
Me.CnEdicion011.EnCreacionOculto = False
Me.CnEdicion011.SiempreSoloLectura = False
Me.CnEdicion011.SiempreOculto = False
Me.CnEdicion011.EnlacesLookup1 = 0
Me.CnEdicion011.EnlacesLookup2 = 0
Me.CnEdicion011.EnlacesLookup3 = 0
Me.CnEdicion011.EnModificacionSoloLectura = False
Me.CnEdicion011.EnModificacionOculto = False
Me.CnEdicion011.Fuente = Nothing
Me.CnEdicion011.HayMascaraEspecial = False
Me.CnEdicion011.HayValorDefecto = False
Me.CnEdicion011.NumeroParametroValorDefecto = 0
Me.CnEdicion011.ValorDefecto = ""
Me.CnEdicion011.HayValorFijo = True
Me.CnEdicion011.NumeroParametroValorFijo = 1
Me.CnEdicion011.ValorFijo = ""
Me.CnEdicion011.HayValorFijoCreacion = False
Me.CnEdicion011.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion011.ValorFijoCreacion = ""
Me.CnEdicion011.Location = New System.Drawing.Point(880,54)
Me.CnEdicion011.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion011.MascaraEspecial = ""
Me.CnEdicion011.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion011.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion011.MaximoNumero = 999999999999999.0R
Me.CnEdicion011.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion011.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion011.MinimoNumero = -999999999999999.0R
Me.CnEdicion011.Name = "CnEdicion011"
Me.CnEdicion011.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion011.TabIndex = 9999
Me.CnEdicion011.Tabla = "prev_compras_clasif"
'
'CnEdicion012
'
Me.CnEdicion012.AceptaEspacios = True
Me.CnEdicion012.AceptaMayusculas = True
Me.CnEdicion012.AceptaMayusculasAcentuadas = False
Me.CnEdicion012.AceptaMinusculas = False
Me.CnEdicion012.AceptaMinusculasAcentuadas = False
Me.CnEdicion012.AceptaNumeros = True
Me.CnEdicion012.AceptaSimbolos = True
Me.CnEdicion012.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion012.ConvierteAMayusculas = True
Me.CnEdicion012.ConvierteAMinusculas = False
Me.CnEdicion012.EsFechaHoraCreacion = False
Me.CnEdicion012.EsFechaHoraModificacion = False
Me.CnEdicion012.ColorFondoControl = System.Drawing.Color.Blue
Me.CnEdicion012.ColorFondo = System.Drawing.Color.SkyBlue
Me.CnEdicion012.ColorFondoRequerido = System.Drawing.Color.SkyBlue
Me.CnEdicion012.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion012.Campo = "ejercicio"
Me.CnEdicion012.CampoEnlacesLookup1 = Nothing
Me.CnEdicion012.CampoEnlacesLookup2 = Nothing
Me.CnEdicion012.CampoEnlacesLookup3 = Nothing
Me.CnEdicion012.EdicionEnGrid = True
Me.CnEdicion012.TituloParaGrid = "Ejercicio"
Me.CnEdicion012.AnchoColumnaGrid = 55
Me.CnEdicion012.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion012.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion012.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion012.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion012.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion012.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion012.EnCreacionSoloLectura = False
Me.CnEdicion012.EnCreacionOculto = False
Me.CnEdicion012.SiempreSoloLectura = False
Me.CnEdicion012.SiempreOculto = False
Me.CnEdicion012.EnlacesLookup1 = 0
Me.CnEdicion012.EnlacesLookup2 = 0
Me.CnEdicion012.EnlacesLookup3 = 0
Me.CnEdicion012.EnModificacionSoloLectura = False
Me.CnEdicion012.EnModificacionOculto = False
Me.CnEdicion012.Fuente = Nothing
Me.CnEdicion012.HayMascaraEspecial = False
Me.CnEdicion012.HayValorDefecto = False
Me.CnEdicion012.NumeroParametroValorDefecto = 0
Me.CnEdicion012.ValorDefecto = ""
Me.CnEdicion012.HayValorFijo = True
Me.CnEdicion012.NumeroParametroValorFijo = 3
Me.CnEdicion012.ValorFijo = ""
Me.CnEdicion012.HayValorFijoCreacion = False
Me.CnEdicion012.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion012.ValorFijoCreacion = ""
Me.CnEdicion012.Location = New System.Drawing.Point(930,54)
Me.CnEdicion012.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion012.MascaraEspecial = ""
Me.CnEdicion012.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion012.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion012.MaximoNumero = 999999999999999.0R
Me.CnEdicion012.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion012.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion012.MinimoNumero = -999999999999999.0R
Me.CnEdicion012.Name = "CnEdicion012"
Me.CnEdicion012.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion012.TabIndex = 9999
Me.CnEdicion012.Tabla = "prev_compras_clasif"
'
'CnEdicion013
'
Me.CnEdicion013.AceptaEspacios = True
Me.CnEdicion013.AceptaMayusculas = True
Me.CnEdicion013.AceptaMayusculasAcentuadas = True
Me.CnEdicion013.AceptaMinusculas = True
Me.CnEdicion013.AceptaMinusculasAcentuadas = True
Me.CnEdicion013.AceptaNumeros = True
Me.CnEdicion013.AceptaSimbolos = False
Me.CnEdicion013.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion013.ConvierteAMayusculas = False
Me.CnEdicion013.ConvierteAMinusculas = False
Me.CnEdicion013.EsFechaHoraCreacion = False
Me.CnEdicion013.EsFechaHoraModificacion = False
Me.CnEdicion013.ColorFondoControl = System.Drawing.Color.Blue
Me.CnEdicion013.ColorFondo = System.Drawing.Color.SkyBlue
Me.CnEdicion013.ColorFondoRequerido = System.Drawing.Color.SkyBlue
Me.CnEdicion013.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion013.Campo = "codigo"
Me.CnEdicion013.CampoEnlacesLookup1 = Nothing
Me.CnEdicion013.CampoEnlacesLookup2 = Nothing
Me.CnEdicion013.CampoEnlacesLookup3 = Nothing
Me.CnEdicion013.EdicionEnGrid = True
Me.CnEdicion013.TituloParaGrid = "Codigo"
Me.CnEdicion013.AnchoColumnaGrid = 48
Me.CnEdicion013.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion013.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion013.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion013.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion013.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion013.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion013.EnCreacionSoloLectura = False
Me.CnEdicion013.EnCreacionOculto = False
Me.CnEdicion013.SiempreSoloLectura = False
Me.CnEdicion013.SiempreOculto = False
Me.CnEdicion013.EnlacesLookup1 = 0
Me.CnEdicion013.EnlacesLookup2 = 0
Me.CnEdicion013.EnlacesLookup3 = 0
Me.CnEdicion013.EnModificacionSoloLectura = False
Me.CnEdicion013.EnModificacionOculto = False
Me.CnEdicion013.Fuente = Nothing
Me.CnEdicion013.HayMascaraEspecial = False
Me.CnEdicion013.HayValorDefecto = False
Me.CnEdicion013.NumeroParametroValorDefecto = 0
Me.CnEdicion013.ValorDefecto = ""
Me.CnEdicion013.HayValorFijo = False
Me.CnEdicion013.NumeroParametroValorFijo = 0
Me.CnEdicion013.ValorFijo = ""
Me.CnEdicion013.HayValorFijoCreacion = False
Me.CnEdicion013.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion013.ValorFijoCreacion = ""
Me.CnEdicion013.Location = New System.Drawing.Point(980,54)
Me.CnEdicion013.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion013.MascaraEspecial = ""
Me.CnEdicion013.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion013.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion013.MaximoNumero = 999999999999999.0R
Me.CnEdicion013.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion013.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion013.MinimoNumero = -999999999999999.0R
Me.CnEdicion013.Name = "CnEdicion013"
Me.CnEdicion013.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion013.TabIndex = 9999
Me.CnEdicion013.Tabla = "prev_compras_clasif"
'
'CnEdicion014
'
Me.CnEdicion014.AceptaEspacios = True
Me.CnEdicion014.AceptaMayusculas = True
Me.CnEdicion014.AceptaMayusculasAcentuadas = True
Me.CnEdicion014.AceptaMinusculas = True
Me.CnEdicion014.AceptaMinusculasAcentuadas = True
Me.CnEdicion014.AceptaNumeros = True
Me.CnEdicion014.AceptaSimbolos = False
Me.CnEdicion014.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion014.ConvierteAMayusculas = False
Me.CnEdicion014.ConvierteAMinusculas = False
Me.CnEdicion014.EsFechaHoraCreacion = False
Me.CnEdicion014.EsFechaHoraModificacion = False
Me.CnEdicion014.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion014.ColorFondo = System.Drawing.Color.White
Me.CnEdicion014.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion014.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion014.Campo = "codigo_calidad"
Me.CnEdicion014.CampoEnlacesLookup1 = "descripcion"
Me.CnEdicion014.CampoEnlacesLookup2 = Nothing
Me.CnEdicion014.CampoEnlacesLookup3 = Nothing
Me.CnEdicion014.EdicionEnGrid = True
Me.CnEdicion014.TituloParaGrid = "Código calidad"
Me.CnEdicion014.AnchoColumnaGrid = 134
Me.CnEdicion014.TituloGridEnlaceLookup1 = "Descripción"
Me.CnEdicion014.AnchoColumnaGridEnlaceLookup1 = 250
Me.CnEdicion014.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion014.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion014.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion014.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion014.EnCreacionSoloLectura = False
Me.CnEdicion014.EnCreacionOculto = False
Me.CnEdicion014.SiempreSoloLectura = False
Me.CnEdicion014.SiempreOculto = False
Me.CnEdicion014.EnlacesLookup1 = 33191
Me.CnEdicion014.EnlacesLookup2 = 0
Me.CnEdicion014.EnlacesLookup3 = 0
Me.CnEdicion014.EnModificacionSoloLectura = False
Me.CnEdicion014.EnModificacionOculto = False
Me.CnEdicion014.Fuente = Nothing
Me.CnEdicion014.HayMascaraEspecial = False
Me.CnEdicion014.HayValorDefecto = False
Me.CnEdicion014.NumeroParametroValorDefecto = 0
Me.CnEdicion014.ValorDefecto = ""
Me.CnEdicion014.HayValorFijo = False
Me.CnEdicion014.NumeroParametroValorFijo = 0
Me.CnEdicion014.ValorFijo = ""
Me.CnEdicion014.HayValorFijoCreacion = False
Me.CnEdicion014.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion014.ValorFijoCreacion = ""
Me.CnEdicion014.Location = New System.Drawing.Point(5,98)
Me.CnEdicion014.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion014.MascaraEspecial = ""
Me.CnEdicion014.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion014.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion014.MaximoNumero = 999999999999999.0R
Me.CnEdicion014.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion014.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion014.MinimoNumero = -999999999999999.0R
Me.CnEdicion014.Name = "CnEdicion014"
Me.CnEdicion014.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion014.TabIndex = 9999
Me.CnEdicion014.Tabla = "prev_compras_clasif"
'
'CnEdicion015
'
Me.CnEdicion015.AceptaEspacios = True
Me.CnEdicion015.AceptaMayusculas = True
Me.CnEdicion015.AceptaMayusculasAcentuadas = False
Me.CnEdicion015.AceptaMinusculas = False
Me.CnEdicion015.AceptaMinusculasAcentuadas = False
Me.CnEdicion015.AceptaNumeros = True
Me.CnEdicion015.AceptaSimbolos = True
Me.CnEdicion015.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion015.ConvierteAMayusculas = True
Me.CnEdicion015.ConvierteAMinusculas = False
Me.CnEdicion015.EsFechaHoraCreacion = False
Me.CnEdicion015.EsFechaHoraModificacion = False
Me.CnEdicion015.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion015.ColorFondo = System.Drawing.Color.White
Me.CnEdicion015.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion015.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion015.Campo = "codigo_variedad"
Me.CnEdicion015.CampoEnlacesLookup1 = "descripcion"
Me.CnEdicion015.CampoEnlacesLookup2 = Nothing
Me.CnEdicion015.CampoEnlacesLookup3 = Nothing
Me.CnEdicion015.EdicionEnGrid = True
Me.CnEdicion015.TituloParaGrid = "Código variedad"
Me.CnEdicion015.AnchoColumnaGrid = 144
Me.CnEdicion015.TituloGridEnlaceLookup1 = "Descripción"
Me.CnEdicion015.AnchoColumnaGridEnlaceLookup1 = 250
Me.CnEdicion015.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion015.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion015.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion015.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion015.EnCreacionSoloLectura = False
Me.CnEdicion015.EnCreacionOculto = False
Me.CnEdicion015.SiempreSoloLectura = False
Me.CnEdicion015.SiempreOculto = False
Me.CnEdicion015.EnlacesLookup1 = 33190
Me.CnEdicion015.EnlacesLookup2 = 0
Me.CnEdicion015.EnlacesLookup3 = 0
Me.CnEdicion015.EnModificacionSoloLectura = False
Me.CnEdicion015.EnModificacionOculto = False
Me.CnEdicion015.Fuente = Nothing
Me.CnEdicion015.HayMascaraEspecial = False
Me.CnEdicion015.HayValorDefecto = False
Me.CnEdicion015.NumeroParametroValorDefecto = 0
Me.CnEdicion015.ValorDefecto = ""
Me.CnEdicion015.HayValorFijo = False
Me.CnEdicion015.NumeroParametroValorFijo = 0
Me.CnEdicion015.ValorFijo = ""
Me.CnEdicion015.HayValorFijoCreacion = False
Me.CnEdicion015.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion015.ValorFijoCreacion = ""
Me.CnEdicion015.Location = New System.Drawing.Point(40,98)
Me.CnEdicion015.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion015.MascaraEspecial = ""
Me.CnEdicion015.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion015.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion015.MaximoNumero = 999999999999999.0R
Me.CnEdicion015.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion015.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion015.MinimoNumero = -999999999999999.0R
Me.CnEdicion015.Name = "CnEdicion015"
Me.CnEdicion015.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion015.TabIndex = 9999
Me.CnEdicion015.Tabla = "prev_compras_clasif"
'
'CnEdicion016
'
Me.CnEdicion016.AceptaEspacios = True
Me.CnEdicion016.AceptaMayusculas = True
Me.CnEdicion016.AceptaMayusculasAcentuadas = True
Me.CnEdicion016.AceptaMinusculas = True
Me.CnEdicion016.AceptaMinusculasAcentuadas = True
Me.CnEdicion016.AceptaNumeros = True
Me.CnEdicion016.AceptaSimbolos = False
Me.CnEdicion016.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion016.ConvierteAMayusculas = False
Me.CnEdicion016.ConvierteAMinusculas = False
Me.CnEdicion016.EsFechaHoraCreacion = False
Me.CnEdicion016.EsFechaHoraModificacion = False
Me.CnEdicion016.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion016.ColorFondo = System.Drawing.Color.White
Me.CnEdicion016.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion016.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion016.Campo = "kg_calidad"
Me.CnEdicion016.CampoEnlacesLookup1 = Nothing
Me.CnEdicion016.CampoEnlacesLookup2 = Nothing
Me.CnEdicion016.CampoEnlacesLookup3 = Nothing
Me.CnEdicion016.EdicionEnGrid = True
Me.CnEdicion016.TituloParaGrid = "Kg. calidad"
Me.CnEdicion016.AnchoColumnaGrid = 106
Me.CnEdicion016.TituloGridEnlaceLookup1 = Nothing
Me.CnEdicion016.AnchoColumnaGridEnlaceLookup1 = 0
Me.CnEdicion016.TituloGridEnlaceLookup2 = Nothing
Me.CnEdicion016.AnchoColumnaGridEnlaceLookup2 = 0
Me.CnEdicion016.TituloGridEnlaceLookup3 = Nothing
Me.CnEdicion016.AnchoColumnaGridEnlaceLookup3 = 0
Me.CnEdicion016.EnCreacionSoloLectura = False
Me.CnEdicion016.EnCreacionOculto = False
Me.CnEdicion016.SiempreSoloLectura = False
Me.CnEdicion016.SiempreOculto = False
Me.CnEdicion016.EnlacesLookup1 = 0
Me.CnEdicion016.EnlacesLookup2 = 0
Me.CnEdicion016.EnlacesLookup3 = 0
Me.CnEdicion016.EnModificacionSoloLectura = False
Me.CnEdicion016.EnModificacionOculto = False
Me.CnEdicion016.Fuente = Nothing
Me.CnEdicion016.HayMascaraEspecial = False
Me.CnEdicion016.HayValorDefecto = False
Me.CnEdicion016.NumeroParametroValorDefecto = 0
Me.CnEdicion016.ValorDefecto = ""
Me.CnEdicion016.HayValorFijo = False
Me.CnEdicion016.NumeroParametroValorFijo = 0
Me.CnEdicion016.ValorFijo = ""
Me.CnEdicion016.HayValorFijoCreacion = False
Me.CnEdicion016.NumeroParametroValorFijoCreacion = 0
Me.CnEdicion016.ValorFijoCreacion = ""
Me.CnEdicion016.Location = New System.Drawing.Point(75,98)
Me.CnEdicion016.Margin = New System.Windows.Forms.Padding(0)
Me.CnEdicion016.MascaraEspecial = ""
Me.CnEdicion016.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
Me.CnEdicion016.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
Me.CnEdicion016.MaximoNumero = 999999999999999.0R
Me.CnEdicion016.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
Me.CnEdicion016.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
Me.CnEdicion016.MinimoNumero = -999999999999999.0R
Me.CnEdicion016.Name = "CnEdicion016"
Me.CnEdicion016.Size = New System.Drawing.Size(30, 20)
Me.CnEdicion016.TabIndex = 9999
Me.CnEdicion016.Tabla = "prev_compras_clasif"
Me.TabPage01.Text = "prev_compras_clasif"
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
'FrmPrevisionComprasCoop
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.CausesValidation = False
Me.ClientSize = New System.Drawing.Size(1184, 961)
Me.Controls.Add(Me.PanelSuperior)
Me.Controls.Add(Me.PanelCentral)
Me.Controls.Add(Me.PanelInferior)
Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Name = "FrmPrevisionComprasCoop"
Me.Text = "FrmPrevisionComprasCoop"
Me.PanelSuperior.ResumeLayout(False)
Me.PanelSuperior.PerformLayout()
Me.PanelCentral.ResumeLayout(False)
Me.TabCabecera.ResumeLayout(False)
Me.TabGeneral.ResumeLayout(False)
Me.TabPage01.ResumeLayout(False)
Me.TabPage01.PerformLayout()
Me.TP00.ResumeLayout(False)
Me.TP00.PerformLayout()
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
Friend WithEvents CnTabla02 As CnTabla.CnTabla
Friend WithEvents GridTabla02 As DataGridView
Friend WithEvents Lbl001 As Label
Friend WithEvents TxtDatos001 As TextBox
Friend WithEvents CnEdicion001 As CnEdicion.CnEdicion
Friend WithEvents CmdGrid001 As Button
Friend WithEvents TxtLookup0011 As TextBox
Friend WithEvents Lbl002 As Label
Friend WithEvents TxtDatos002 As TextBox
Friend WithEvents CnEdicion002 As CnEdicion.CnEdicion
Friend WithEvents CmdGrid002 As Button
Friend WithEvents TxtLookup0021 As TextBox
Friend WithEvents Lbl003 As Label
Friend WithEvents TxtDatos003 As TextBox
Friend WithEvents CnEdicion003 As CnEdicion.CnEdicion
Friend WithEvents Lbl004 As Label
Friend WithEvents TxtDatos004 As TextBox
Friend WithEvents CnEdicion004 As CnEdicion.CnEdicion
Friend WithEvents CmdFecha004 As Button
Friend WithEvents Lbl005 As Label
Friend WithEvents TxtDatos005 As TextBox
Friend WithEvents CnEdicion005 As CnEdicion.CnEdicion
Friend WithEvents CmdGrid005 As Button
Friend WithEvents TxtLookup0051 As TextBox
Friend WithEvents Lbl006 As Label
Friend WithEvents TxtDatos006 As TextBox
Friend WithEvents CnEdicion006 As CnEdicion.CnEdicion
Friend WithEvents CmdGrid006 As Button
Friend WithEvents TxtLookup0061 As TextBox
Friend WithEvents Lbl007 As Label
Friend WithEvents TxtDatos007 As TextBox
Friend WithEvents CnEdicion007 As CnEdicion.CnEdicion
Friend WithEvents Lbl008 As Label
Friend WithEvents TxtDatos008 As TextBox
Friend WithEvents CnEdicion008 As CnEdicion.CnEdicion
Friend WithEvents CmdFecha008 As Button
Friend WithEvents Lbl009 As Label
Friend WithEvents TxtDatos009 As TextBox
Friend WithEvents CnEdicion009 As CnEdicion.CnEdicion
Friend WithEvents CmdFecha009 As Button
Friend WithEvents Lbl010 As Label
Friend WithEvents TxtDatos010 As TextBox
Friend WithEvents CnEdicion010 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion011 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion012 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion013 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion014 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion015 As CnEdicion.CnEdicion
Friend WithEvents CnEdicion016 As CnEdicion.CnEdicion
 Friend WithEvents Timer1 As Timer
End Class
