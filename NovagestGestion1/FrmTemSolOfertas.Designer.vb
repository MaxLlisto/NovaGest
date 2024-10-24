<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTemSolOfertas

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
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTemSolOfertas))
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
Me.Lbl001 = New System.Windows.Forms.Label()
Me.TxtDatos001 = New System.Windows.Forms.TextBox()
Me.CnEdicion001 = New CnEdicion.CnEdicion()
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
Me.Lbl007 = New System.Windows.Forms.Label()
Me.TxtDatos007 = New System.Windows.Forms.TextBox()
Me.CnEdicion007 = New CnEdicion.CnEdicion()
Me.Lbl008 = New System.Windows.Forms.Label()
Me.TxtDatos008 = New System.Windows.Forms.TextBox()
Me.CnEdicion008 = New CnEdicion.CnEdicion()
Me.Lbl009 = New System.Windows.Forms.Label()
Me.TxtDatos009 = New System.Windows.Forms.TextBox()
Me.CnEdicion009 = New CnEdicion.CnEdicion()
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
Me.PanelInferior = New System.Windows.Forms.Panel()
Me.PanelSuperior.SuspendLayout()
Me.PanelCentral.SuspendLayout()
Me.TabCabecera.SuspendLayout()
Me.TP00.SuspendLayout()
Me.TabGeneral.SuspendLayout()
Me.TabPage00.SuspendLayout()
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
Me.CnTabla01.Tabla = "tem_sol_ofertas"
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
Me.TabCabecera.Size = New System.Drawing.Size(1180, 83)
Me.TabCabecera.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
Me.TabCabecera.TabIndex = 9999
Me.TabCabecera.CausesValidation = False
'
'TP00
'
Me.TP00.Controls.Add(Me.CnEdicion003)
Me.TP00.Controls.Add(Me.TxtDatos003)
Me.TP00.Controls.Add(Me.Lbl003)
Me.TP00.Controls.Add(Me.TxtLookup0021)
Me.TP00.Controls.Add(Me.CmdGrid002)
Me.TP00.Controls.Add(Me.CnEdicion002)
Me.TP00.Controls.Add(Me.TxtDatos002)
Me.TP00.Controls.Add(Me.Lbl002)
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
Me.CnEdicion001.CampoEnlacesLookup1 = Nothing
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
Me.CnEdicion001.Tabla = "tem_sol_ofertas"
'
'Lbl002
'
Me.Lbl002.AutoSize = True
Me.Lbl002.Location = New System.Drawing.Point(66,31)
Me.Lbl002.Name = "Lbl002"
Me.Lbl002.Size = New System.Drawing.Size(41, 13)
Me.Lbl002.Tabindex = 9999
Me.Lbl002.Text = "Artículo:"
'
'TxtDatos002
'
Me.TxtDatos002.Location = New System.Drawing.Point(114,29)
Me.TxtDatos002.Name = "TxtDatos002"
Me.TxtDatos002.ReadOnly = True
Me.TxtDatos002.Size = New System.Drawing.Size(160, 20)
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
Me.CnEdicion002.Campo = "articulo"
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
Me.CnEdicion002.EnlacesLookup1 = 33028
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
Me.CnEdicion002.Tabla = "tem_sol_ofertas"
'
'CmdGrid002
'
Me.CmdGrid002.Image = CType(Resources.GetObject("CmdGrid002.Image"), System.Drawing.Image)
Me.CmdGrid002.Location = New System.Drawing.Point(275,29)
Me.CmdGrid002.Name = "CmdGrid002"
Me.CmdGrid002.Size = New System.Drawing.Size(24,22)
Me.CmdGrid002.UseVisualStyleBackColor = True
Me.CmdGrid002.Tabindex = 9999
'
'TxtLookup0021
'
Me.TxtLookup0021.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
Me.TxtLookup0021.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TxtLookup0021.Location = New System.Drawing.Point(309,32)
Me.TxtLookup0021.Name = "TxtLookup0021"
Me.TxtLookup0021.ReadOnly = True
Me.TxtLookup0021.Size = New System.Drawing.Size(250, 13)
Me.TxtLookup0021.Tabindex = 9999
'
'Lbl003
'
Me.Lbl003.AutoSize = True
Me.Lbl003.Location = New System.Drawing.Point(57,52)
Me.Lbl003.Name = "Lbl003"
Me.Lbl003.Size = New System.Drawing.Size(41, 13)
Me.Lbl003.Tabindex = 9999
Me.Lbl003.Text = "Contador:"
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
Me.CnEdicion003.Campo = "contador"
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
Me.CnEdicion003.Tabla = "tem_sol_ofertas"
Me.TP00.Text = "tem_sol_ofertas"
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
Me.TabGeneral.Location = New System.Drawing.Point(2, 85)
Me.TabGeneral.Margin = New System.Windows.Forms.Padding(0)
Me.TabGeneral.Name = "TabGeneral"
Me.TabGeneral.SelectedIndex = 0
Me.TabGeneral.Size = New System.Drawing.Size(1184, 782)
Me.TabGeneral.TabIndex = 0
Me.TabGeneral.CausesValidation = False
'
'TabPage00
'
Me.TabPage00.Controls.Add(Me.CnEdicion009)
Me.TabPage00.Controls.Add(Me.TxtDatos009)
Me.TabPage00.Controls.Add(Me.Lbl009)
Me.TabPage00.Controls.Add(Me.CnEdicion008)
Me.TabPage00.Controls.Add(Me.TxtDatos008)
Me.TabPage00.Controls.Add(Me.Lbl008)
Me.TabPage00.Controls.Add(Me.CnEdicion007)
Me.TabPage00.Controls.Add(Me.TxtDatos007)
Me.TabPage00.Controls.Add(Me.Lbl007)
Me.TabPage00.Controls.Add(Me.CnEdicion006)
Me.TabPage00.Controls.Add(Me.TxtDatos006)
Me.TabPage00.Controls.Add(Me.Lbl006)
Me.TabPage00.Controls.Add(Me.TxtLookup0051)
Me.TabPage00.Controls.Add(Me.CmdGrid005)
Me.TabPage00.Controls.Add(Me.CnEdicion005)
Me.TabPage00.Controls.Add(Me.TxtDatos005)
Me.TabPage00.Controls.Add(Me.Lbl005)
Me.TabPage00.Controls.Add(Me.CmdFecha004)
Me.TabPage00.Controls.Add(Me.CnEdicion004)
Me.TabPage00.Controls.Add(Me.TxtDatos004)
Me.TabPage00.Controls.Add(Me.Lbl004)
Me.TabPage00.Location = New System.Drawing.Point(4, 4)
Me.TabPage00.Name = "TabPage00"
Me.TabPage00.Padding = New System.Windows.Forms.Padding(3)
Me.TabPage00.Size = New System.Drawing.Size(1172, 762)
Me.TabPage00.TabIndex = 0
Me.TabPage00.Text = "0"
Me.TabPage00.UseVisualStyleBackColor = True
Me.Tabpage00.CausesValidation = False
'
'Lbl004
'
Me.Lbl004.AutoSize = True
Me.Lbl004.Location = New System.Drawing.Point(22,10)
Me.Lbl004.Name = "Lbl004"
Me.Lbl004.Size = New System.Drawing.Size(41, 13)
Me.Lbl004.Tabindex = 9999
Me.Lbl004.Text = "Fecha Incidencia:"
'
'TxtDatos004
'
Me.TxtDatos004.Location = New System.Drawing.Point(114,8)
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
Me.CnEdicion004.Location = New System.Drawing.Point(118,8)
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
Me.CnEdicion004.Tabla = "tem_sol_ofertas"
'
'CmdFecha004
'
Me.CmdFecha004.Image = CType(Resources.GetObject("CmdFecha004.Image"), System.Drawing.Image)
Me.CmdFecha004.Location = New System.Drawing.Point(215,8)
Me.CmdFecha004.Name = "CmdFecha004"
Me.CmdFecha004.Size = New System.Drawing.Size(24,22)
Me.CmdFecha004.UseVisualStyleBackColor = True
Me.CmdFecha004.Tabindex = 9999
'
'Lbl005
'
Me.Lbl005.AutoSize = True
Me.Lbl005.Location = New System.Drawing.Point(16,31)
Me.Lbl005.Name = "Lbl005"
Me.Lbl005.Size = New System.Drawing.Size(41, 13)
Me.Lbl005.Tabindex = 9999
Me.Lbl005.Text = "Código proveedor:"
'
'TxtDatos005
'
Me.TxtDatos005.Location = New System.Drawing.Point(114,29)
Me.TxtDatos005.Name = "TxtDatos005"
Me.TxtDatos005.ReadOnly = True
Me.TxtDatos005.Size = New System.Drawing.Size(100, 20)
Me.TxtDatos005.Tabindex = 9999
'
'CnEdicion005
'
Me.CnEdicion005.AceptaEspacios = True
Me.CnEdicion005.AceptaMayusculas = True
Me.CnEdicion005.AceptaMayusculasAcentuadas = True
Me.CnEdicion005.AceptaMinusculas = True
Me.CnEdicion005.AceptaMinusculasAcentuadas = True
Me.CnEdicion005.AceptaNumeros = True
Me.CnEdicion005.AceptaSimbolos = False
Me.CnEdicion005.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
Me.CnEdicion005.ConvierteAMayusculas = False
Me.CnEdicion005.ConvierteAMinusculas = False
Me.CnEdicion005.EsFechaHoraCreacion = False
Me.CnEdicion005.EsFechaHoraModificacion = False
Me.CnEdicion005.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion005.ColorFondo = System.Drawing.Color.White
Me.CnEdicion005.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion005.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion005.Campo = "codigo_proveedor"
Me.CnEdicion005.CampoEnlacesLookup1 = "razon_social"
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
Me.CnEdicion005.EnlacesLookup1 = 33029
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
Me.CnEdicion005.Location = New System.Drawing.Point(118,29)
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
Me.CnEdicion005.Tabla = "tem_sol_ofertas"
'
'CmdGrid005
'
Me.CmdGrid005.Image = CType(Resources.GetObject("CmdGrid005.Image"), System.Drawing.Image)
Me.CmdGrid005.Location = New System.Drawing.Point(215,29)
Me.CmdGrid005.Name = "CmdGrid005"
Me.CmdGrid005.Size = New System.Drawing.Size(24,22)
Me.CmdGrid005.UseVisualStyleBackColor = True
Me.CmdGrid005.Tabindex = 9999
'
'TxtLookup0051
'
Me.TxtLookup0051.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
Me.TxtLookup0051.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.TxtLookup0051.Location = New System.Drawing.Point(249,32)
Me.TxtLookup0051.Name = "TxtLookup0051"
Me.TxtLookup0051.ReadOnly = True
Me.TxtLookup0051.Size = New System.Drawing.Size(250, 13)
Me.TxtLookup0051.Tabindex = 9999
'
'Lbl006
'
Me.Lbl006.AutoSize = True
Me.Lbl006.Location = New System.Drawing.Point(59,52)
Me.Lbl006.Name = "Lbl006"
Me.Lbl006.Size = New System.Drawing.Size(41, 13)
Me.Lbl006.Tabindex = 9999
Me.Lbl006.Text = "Cantidad:"
'
'TxtDatos006
'
Me.TxtDatos006.Location = New System.Drawing.Point(114,50)
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
Me.CnEdicion006.Campo = "cantidad"
Me.CnEdicion006.CampoEnlacesLookup1 = Nothing
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
Me.CnEdicion006.EnlacesLookup1 = 0
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
Me.CnEdicion006.Location = New System.Drawing.Point(118,50)
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
Me.CnEdicion006.Tabla = "tem_sol_ofertas"
'
'Lbl007
'
Me.Lbl007.AutoSize = True
Me.Lbl007.Location = New System.Drawing.Point(64,73)
Me.Lbl007.Name = "Lbl007"
Me.Lbl007.Size = New System.Drawing.Size(41, 13)
Me.Lbl007.Tabindex = 9999
Me.Lbl007.Text = "Importe:"
'
'TxtDatos007
'
Me.TxtDatos007.Location = New System.Drawing.Point(114,71)
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
Me.CnEdicion007.Campo = "importe"
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
Me.CnEdicion007.Location = New System.Drawing.Point(118,71)
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
Me.CnEdicion007.Tabla = "tem_sol_ofertas"
'
'Lbl008
'
Me.Lbl008.AutoSize = True
Me.Lbl008.Location = New System.Drawing.Point(31,94)
Me.Lbl008.Name = "Lbl008"
Me.Lbl008.Size = New System.Drawing.Size(41, 13)
Me.Lbl008.Tabindex = 9999
Me.Lbl008.Text = "Observaciones:"
'
'TxtDatos008
'
Me.TxtDatos008.Location = New System.Drawing.Point(114,92)
Me.TxtDatos008.Name = "TxtDatos008"
Me.TxtDatos008.ReadOnly = True
Me.TxtDatos008.Size = New System.Drawing.Size(250, 20)
Me.TxtDatos008.Tabindex = 9999
'
'CnEdicion008
'
Me.CnEdicion008.AceptaEspacios = True
Me.CnEdicion008.AceptaMayusculas = True
Me.CnEdicion008.AceptaMayusculasAcentuadas = False
Me.CnEdicion008.AceptaMinusculas = False
Me.CnEdicion008.AceptaMinusculasAcentuadas = False
Me.CnEdicion008.AceptaNumeros = True
Me.CnEdicion008.AceptaSimbolos = True
Me.CnEdicion008.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion008.ConvierteAMayusculas = True
Me.CnEdicion008.ConvierteAMinusculas = False
Me.CnEdicion008.EsFechaHoraCreacion = False
Me.CnEdicion008.EsFechaHoraModificacion = False
Me.CnEdicion008.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion008.ColorFondo = System.Drawing.Color.White
Me.CnEdicion008.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion008.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion008.Campo = "observaciones"
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
Me.CnEdicion008.Location = New System.Drawing.Point(118,92)
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
Me.CnEdicion008.Tabla = "tem_sol_ofertas"
'
'Lbl009
'
Me.Lbl009.AutoSize = True
Me.Lbl009.Location = New System.Drawing.Point(34,115)
Me.Lbl009.Name = "Lbl009"
Me.Lbl009.Size = New System.Drawing.Size(41, 13)
Me.Lbl009.Tabindex = 9999
Me.Lbl009.Text = "Art Proveedor:"
'
'TxtDatos009
'
Me.TxtDatos009.Location = New System.Drawing.Point(114,113)
Me.TxtDatos009.Name = "TxtDatos009"
Me.TxtDatos009.ReadOnly = True
Me.TxtDatos009.Size = New System.Drawing.Size(160, 20)
Me.TxtDatos009.Tabindex = 9999
'
'CnEdicion009
'
Me.CnEdicion009.AceptaEspacios = True
Me.CnEdicion009.AceptaMayusculas = True
Me.CnEdicion009.AceptaMayusculasAcentuadas = False
Me.CnEdicion009.AceptaMinusculas = False
Me.CnEdicion009.AceptaMinusculasAcentuadas = False
Me.CnEdicion009.AceptaNumeros = True
Me.CnEdicion009.AceptaSimbolos = True
Me.CnEdicion009.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
Me.CnEdicion009.ConvierteAMayusculas = True
Me.CnEdicion009.ConvierteAMinusculas = False
Me.CnEdicion009.EsFechaHoraCreacion = False
Me.CnEdicion009.EsFechaHoraModificacion = False
Me.CnEdicion009.ColorFondoControl = System.Drawing.Color.Yellow
Me.CnEdicion009.ColorFondo = System.Drawing.Color.White
Me.CnEdicion009.ColorFondoRequerido = System.Drawing.Color.Yellow
Me.CnEdicion009.BackColor = System.Drawing.Color.Yellow
Me.CnEdicion009.Campo = "art_proveedor"
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
Me.CnEdicion009.Location = New System.Drawing.Point(118,113)
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
Me.CnEdicion009.Tabla = "tem_sol_ofertas"
Me.TabPage00.Text = "tem_sol_ofertas"
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
'FrmTemSolOfertas
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.CausesValidation = False
Me.ClientSize = New System.Drawing.Size(1184, 961)
Me.Controls.Add(Me.PanelSuperior)
Me.Controls.Add(Me.PanelCentral)
Me.Controls.Add(Me.PanelInferior)
Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.Name = "FrmTemSolOfertas"
Me.Text = "FrmTemSolOfertas"
Me.PanelSuperior.ResumeLayout(False)
Me.PanelSuperior.PerformLayout()
Me.PanelCentral.ResumeLayout(False)
Me.TabCabecera.ResumeLayout(False)
Me.TabGeneral.ResumeLayout(False)
Me.TP00.ResumeLayout(False)
Me.TP00.PerformLayout()
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
Friend WithEvents Lbl001 As Label
Friend WithEvents TxtDatos001 As TextBox
Friend WithEvents CnEdicion001 As CnEdicion.CnEdicion
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
Friend WithEvents Lbl007 As Label
Friend WithEvents TxtDatos007 As TextBox
Friend WithEvents CnEdicion007 As CnEdicion.CnEdicion
Friend WithEvents Lbl008 As Label
Friend WithEvents TxtDatos008 As TextBox
Friend WithEvents CnEdicion008 As CnEdicion.CnEdicion
Friend WithEvents Lbl009 As Label
Friend WithEvents TxtDatos009 As TextBox
Friend WithEvents CnEdicion009 As CnEdicion.CnEdicion
End Class
