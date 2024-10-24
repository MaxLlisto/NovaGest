<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCarnetManipulador

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCarnetManipulador))
        Me.PanelSuperior = New System.Windows.Forms.Panel()
        Me.CnTabla01 = New CnTabla.CnTabla()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.PanelCentral = New System.Windows.Forms.Panel()
        Me.TabCabecera = New System.Windows.Forms.TabControl()
        Me.TP00 = New System.Windows.Forms.TabPage()
        Me.CmdFecha010 = New System.Windows.Forms.Button()
        Me.CnEdicion010 = New CnEdicion.CnEdicion()
        Me.TxtDatos010 = New System.Windows.Forms.TextBox()
        Me.Lbl010 = New System.Windows.Forms.Label()
        Me.CnEdicion009 = New CnEdicion.CnEdicion()
        Me.TxtDatos009 = New System.Windows.Forms.TextBox()
        Me.Lbl009 = New System.Windows.Forms.Label()
        Me.CmdFecha008 = New System.Windows.Forms.Button()
        Me.CnEdicion008 = New CnEdicion.CnEdicion()
        Me.TxtDatos008 = New System.Windows.Forms.TextBox()
        Me.Lbl008 = New System.Windows.Forms.Label()
        Me.CnEdicion007 = New CnEdicion.CnEdicion()
        Me.TxtDatos007 = New System.Windows.Forms.TextBox()
        Me.Lbl007 = New System.Windows.Forms.Label()
        Me.TxtLookup0061 = New System.Windows.Forms.TextBox()
        Me.CmdGrid006 = New System.Windows.Forms.Button()
        Me.CnEdicion006 = New CnEdicion.CnEdicion()
        Me.TxtDatos006 = New System.Windows.Forms.TextBox()
        Me.Lbl006 = New System.Windows.Forms.Label()
        Me.CnEdicion005 = New CnEdicion.CnEdicion()
        Me.TxtDatos005 = New System.Windows.Forms.TextBox()
        Me.Lbl005 = New System.Windows.Forms.Label()
        Me.CnEdicion004 = New CnEdicion.CnEdicion()
        Me.TxtDatos004 = New System.Windows.Forms.TextBox()
        Me.Lbl004 = New System.Windows.Forms.Label()
        Me.CnEdicion003 = New CnEdicion.CnEdicion()
        Me.TxtDatos003 = New System.Windows.Forms.TextBox()
        Me.Lbl003 = New System.Windows.Forms.Label()
        Me.CnEdicion002 = New CnEdicion.CnEdicion()
        Me.TxtDatos002 = New System.Windows.Forms.TextBox()
        Me.Lbl002 = New System.Windows.Forms.Label()
        Me.CnEdicion001 = New CnEdicion.CnEdicion()
        Me.TxtDatos001 = New System.Windows.Forms.TextBox()
        Me.Lbl001 = New System.Windows.Forms.Label()
        Me.TP01 = New System.Windows.Forms.TabPage()
        Me.TP02 = New System.Windows.Forms.TabPage()
        Me.TP03 = New System.Windows.Forms.TabPage()
        Me.TP04 = New System.Windows.Forms.TabPage()
        Me.TP05 = New System.Windows.Forms.TabPage()
        Me.TP06 = New System.Windows.Forms.TabPage()
        Me.TP07 = New System.Windows.Forms.TabPage()
        Me.TP08 = New System.Windows.Forms.TabPage()
        Me.TP09 = New System.Windows.Forms.TabPage()
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
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.BackColor = System.Drawing.SystemColors.Control
        Me.PanelSuperior.CausesValidation = False
        Me.PanelSuperior.Controls.Add(Me.CnTabla01)
        Me.PanelSuperior.Controls.Add(Me.CmdSalir)
        Me.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSuperior.Location = New System.Drawing.Point(0, 0)
        Me.PanelSuperior.Name = "PanelSuperior"
        Me.PanelSuperior.Size = New System.Drawing.Size(1184, 55)
        Me.PanelSuperior.TabIndex = 9999
        '
        'CnTabla01
        '
        Me.CnTabla01.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnTabla01.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnTabla01.AutoSize = True
        Me.CnTabla01.BackColor = System.Drawing.SystemColors.Control
        Me.CnTabla01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CnTabla01.CargaAlInicio = True
        Me.CnTabla01.CausesValidation = False
        Me.CnTabla01.Contenedor = Nothing
        Me.CnTabla01.ContextMenuStrip = Nothing
        Me.CnTabla01.Enabled = False
        Me.CnTabla01.EnlaceATablaPadre = -1
        Me.CnTabla01.Estado = CnTabla.CnTabla.EstadoCnTabla.Inactivo
        Me.CnTabla01.Filtro = Nothing
        Me.CnTabla01.FiltroActivado = False
        Me.CnTabla01.FiltroExiste = False
        Me.CnTabla01.FiltroNoExiste = False
        Me.CnTabla01.FiltroTodas = False
        Me.CnTabla01.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CnTabla01.Formato = CnTabla.CnTabla.FormatoCnTabla.TablaPrincipalSuperior
        Me.CnTabla01.FormularioLlamador = Nothing
        Me.CnTabla01.Grid = Nothing
        Me.CnTabla01.HayBorrado = True
        Me.CnTabla01.HayCreacion = True
        Me.CnTabla01.HayDesplegar = True
        Me.CnTabla01.HayGrid = False
        Me.CnTabla01.HayModificacion = True
        Me.CnTabla01.HaySeleccion = True
        Me.CnTabla01.HaySiguienteAnterior = True
        Me.CnTabla01.Location = New System.Drawing.Point(3, 0)
        Me.CnTabla01.Margin = New System.Windows.Forms.Padding(0)
        Me.CnTabla01.Name = "CnTabla01"
        Me.CnTabla01.NumeroTabpageGrid = -1
        Me.CnTabla01.NumTablaFormulario = -1
        Me.CnTabla01.NumTablaPadreFormulario = -1
        Me.CnTabla01.OrdenacionDefecto = Nothing
        Me.CnTabla01.OrdenacionFiltro = Nothing
        Me.CnTabla01.SeleccionAdicional = Nothing
        Me.CnTabla01.Size = New System.Drawing.Size(550, 52)
        Me.CnTabla01.TabIndex = 10000
        Me.CnTabla01.Tabla = "carnet_manipulador"
        Me.CnTabla01.TablaPadre = Nothing
        Me.CnTabla01.TabStop = False
        '
        'CmdSalir
        '
        Me.CmdSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdSalir.CausesValidation = False
        Me.CmdSalir.Image = CType(resources.GetObject("CmdSalir.Image"), System.Drawing.Image)
        Me.CmdSalir.Location = New System.Drawing.Point(1126, 6)
        Me.CmdSalir.Name = "CmdSalir"
        Me.CmdSalir.Size = New System.Drawing.Size(54, 48)
        Me.CmdSalir.TabIndex = 9999
        Me.CmdSalir.TabStop = False
        Me.CmdSalir.UseVisualStyleBackColor = True
        '
        'PanelCentral
        '
        Me.PanelCentral.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelCentral.BackColor = System.Drawing.SystemColors.Control
        Me.PanelCentral.CausesValidation = False
        Me.PanelCentral.Controls.Add(Me.TabCabecera)
        Me.PanelCentral.Controls.Add(Me.TabGeneral)
        Me.PanelCentral.Location = New System.Drawing.Point(0, 59)
        Me.PanelCentral.Name = "PanelCentral"
        Me.PanelCentral.Size = New System.Drawing.Size(1184, 178)
        Me.PanelCentral.TabIndex = 9999
        '
        'TabCabecera
        '
        Me.TabCabecera.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.TabCabecera.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabCabecera.CausesValidation = False
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
        Me.TabCabecera.ItemSize = New System.Drawing.Size(20, 20)
        Me.TabCabecera.Location = New System.Drawing.Point(2, 0)
        Me.TabCabecera.Margin = New System.Windows.Forms.Padding(0)
        Me.TabCabecera.Multiline = True
        Me.TabCabecera.Name = "TabCabecera"
        Me.TabCabecera.SelectedIndex = 0
        Me.TabCabecera.Size = New System.Drawing.Size(1180, 146)
        Me.TabCabecera.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabCabecera.TabIndex = 9999
        '
        'TP00
        '
        Me.TP00.CausesValidation = False
        Me.TP00.Controls.Add(Me.CmdFecha010)
        Me.TP00.Controls.Add(Me.CnEdicion010)
        Me.TP00.Controls.Add(Me.TxtDatos010)
        Me.TP00.Controls.Add(Me.Lbl010)
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
        Me.TP00.Controls.Add(Me.CnEdicion005)
        Me.TP00.Controls.Add(Me.TxtDatos005)
        Me.TP00.Controls.Add(Me.Lbl005)
        Me.TP00.Controls.Add(Me.CnEdicion004)
        Me.TP00.Controls.Add(Me.TxtDatos004)
        Me.TP00.Controls.Add(Me.Lbl004)
        Me.TP00.Controls.Add(Me.CnEdicion003)
        Me.TP00.Controls.Add(Me.TxtDatos003)
        Me.TP00.Controls.Add(Me.Lbl003)
        Me.TP00.Controls.Add(Me.CnEdicion002)
        Me.TP00.Controls.Add(Me.TxtDatos002)
        Me.TP00.Controls.Add(Me.Lbl002)
        Me.TP00.Controls.Add(Me.CnEdicion001)
        Me.TP00.Controls.Add(Me.TxtDatos001)
        Me.TP00.Controls.Add(Me.Lbl001)
        Me.TP00.Location = New System.Drawing.Point(4, 4)
        Me.TP00.Margin = New System.Windows.Forms.Padding(0)
        Me.TP00.Name = "TP00"
        Me.TP00.Size = New System.Drawing.Size(1132, 138)
        Me.TP00.TabIndex = 1
        Me.TP00.Text = "carnet_manipulador"
        Me.TP00.UseVisualStyleBackColor = True
        '
        'CmdFecha010
        '
        Me.CmdFecha010.Image = CType(resources.GetObject("CmdFecha010.Image"), System.Drawing.Image)
        Me.CmdFecha010.Location = New System.Drawing.Point(595, 113)
        Me.CmdFecha010.Name = "CmdFecha010"
        Me.CmdFecha010.Size = New System.Drawing.Size(24, 22)
        Me.CmdFecha010.TabIndex = 9999
        Me.CmdFecha010.UseVisualStyleBackColor = True
        '
        'CnEdicion010
        '
        Me.CnEdicion010.AceptaEspacios = True
        Me.CnEdicion010.AceptaMayusculas = True
        Me.CnEdicion010.AceptaMayusculasAcentuadas = True
        Me.CnEdicion010.AceptaMinusculas = True
        Me.CnEdicion010.AceptaMinusculasAcentuadas = True
        Me.CnEdicion010.AceptaNumeros = True
        Me.CnEdicion010.AceptaSimbolos = False
        Me.CnEdicion010.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion010.AnchoColumnaGrid = 0
        Me.CnEdicion010.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion010.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion010.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion010.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion010.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion010.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion010.Campo = "fecha_vto_ropo"
        Me.CnEdicion010.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion010.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion010.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion010.CampoTablaPadre = Nothing
        Me.CnEdicion010.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
        Me.CnEdicion010.Clave = False
        Me.CnEdicion010.CmdCombo = Nothing
        Me.CnEdicion010.CmdFecha = Nothing
        Me.CnEdicion010.CmdGrid = Nothing
        Me.CnEdicion010.CmdMantenimiento = Nothing
        Me.CnEdicion010.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion010.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion010.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion010.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion010.Contenedor = Nothing
        Me.CnEdicion010.ContextMenuStrip = Nothing
        Me.CnEdicion010.ConvierteAMayusculas = False
        Me.CnEdicion010.ConvierteAMinusculas = False
        Me.CnEdicion010.CuantosEnlacesCampo = 0
        Me.CnEdicion010.Decimales = 0
        Me.CnEdicion010.EdicionEnCombo = False
        Me.CnEdicion010.EdicionEnGrid = False
        Me.CnEdicion010.EnCreacionOculto = False
        Me.CnEdicion010.EnCreacionSoloLectura = False
        Me.CnEdicion010.EnlacesLookup1 = 0
        Me.CnEdicion010.EnlacesLookup2 = 0
        Me.CnEdicion010.EnlacesLookup3 = 0
        Me.CnEdicion010.EnModificacionOculto = False
        Me.CnEdicion010.EnModificacionSoloLectura = False
        Me.CnEdicion010.EsFechaHoraCreacion = False
        Me.CnEdicion010.EsFechaHoraModificacion = False
        Me.CnEdicion010.Etiqueta = Nothing
        Me.CnEdicion010.Fuente = Nothing
        Me.CnEdicion010.HayMascaraEspecial = False
        Me.CnEdicion010.HayValorDefecto = False
        Me.CnEdicion010.HayValorFijo = False
        Me.CnEdicion010.HayValorFijoCreacion = False
        Me.CnEdicion010.Identidad = False
        Me.CnEdicion010.Location = New System.Drawing.Point(498, 113)
        Me.CnEdicion010.Longitud = 90
        Me.CnEdicion010.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion010.MascaraEspecial = ""
        Me.CnEdicion010.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion010.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion010.MaximoNumero = 999999999999999.0R
        Me.CnEdicion010.MaxLength = 0
        Me.CnEdicion010.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion010.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion010.MinimoNumero = -999999999999999.0R
        Me.CnEdicion010.Name = "CnEdicion010"
        Me.CnEdicion010.NumeroCampo = -1
        Me.CnEdicion010.NumeroParametroValorDefecto = 0
        Me.CnEdicion010.NumeroParametroValorFijo = 0
        Me.CnEdicion010.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion010.NumeroTablaFormulario = -1
        Me.CnEdicion010.Requerido = False
        Me.CnEdicion010.Restriccion = ""
        Me.CnEdicion010.SiempreOculto = False
        Me.CnEdicion010.SiempreSoloLectura = False
        Me.CnEdicion010.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion010.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion010.TabIndex = 9999
        Me.CnEdicion010.Tabla = "carnet_manipulador"
        Me.CnEdicion010.TablaEnlacePrincipal = Nothing
        Me.CnEdicion010.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion010.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion010.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion010.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion010.TituloParaGrid = Nothing
        Me.CnEdicion010.TTEdicion = Nothing
        Me.CnEdicion010.TxtDatos = Nothing
        Me.CnEdicion010.ValorDefecto = ""
        Me.CnEdicion010.ValorFijo = ""
        Me.CnEdicion010.ValorFijoCreacion = ""
        '
        'TxtDatos010
        '
        Me.TxtDatos010.Location = New System.Drawing.Point(494, 113)
        Me.TxtDatos010.Name = "TxtDatos010"
        Me.TxtDatos010.ReadOnly = True
        Me.TxtDatos010.Size = New System.Drawing.Size(100, 21)
        Me.TxtDatos010.TabIndex = 9999
        '
        'Lbl010
        '
        Me.Lbl010.AutoSize = True
        Me.Lbl010.Location = New System.Drawing.Point(406, 115)
        Me.Lbl010.Name = "Lbl010"
        Me.Lbl010.Size = New System.Drawing.Size(87, 13)
        Me.Lbl010.TabIndex = 9999
        Me.Lbl010.Text = "Fecha Vto Ropo:"
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
        Me.CnEdicion009.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion009.AnchoColumnaGrid = 0
        Me.CnEdicion009.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion009.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion009.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion009.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion009.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion009.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion009.Campo = "ropo"
        Me.CnEdicion009.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion009.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion009.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion009.CampoTablaPadre = Nothing
        Me.CnEdicion009.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion009.Clave = False
        Me.CnEdicion009.CmdCombo = Nothing
        Me.CnEdicion009.CmdFecha = Nothing
        Me.CnEdicion009.CmdGrid = Nothing
        Me.CnEdicion009.CmdMantenimiento = Nothing
        Me.CnEdicion009.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion009.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion009.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion009.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion009.Contenedor = Nothing
        Me.CnEdicion009.ContextMenuStrip = Nothing
        Me.CnEdicion009.ConvierteAMayusculas = True
        Me.CnEdicion009.ConvierteAMinusculas = False
        Me.CnEdicion009.CuantosEnlacesCampo = 0
        Me.CnEdicion009.Decimales = 0
        Me.CnEdicion009.EdicionEnCombo = False
        Me.CnEdicion009.EdicionEnGrid = False
        Me.CnEdicion009.EnCreacionOculto = False
        Me.CnEdicion009.EnCreacionSoloLectura = False
        Me.CnEdicion009.EnlacesLookup1 = 0
        Me.CnEdicion009.EnlacesLookup2 = 0
        Me.CnEdicion009.EnlacesLookup3 = 0
        Me.CnEdicion009.EnModificacionOculto = False
        Me.CnEdicion009.EnModificacionSoloLectura = False
        Me.CnEdicion009.EsFechaHoraCreacion = False
        Me.CnEdicion009.EsFechaHoraModificacion = False
        Me.CnEdicion009.Etiqueta = Nothing
        Me.CnEdicion009.Fuente = Nothing
        Me.CnEdicion009.HayMascaraEspecial = False
        Me.CnEdicion009.HayValorDefecto = False
        Me.CnEdicion009.HayValorFijo = False
        Me.CnEdicion009.HayValorFijoCreacion = False
        Me.CnEdicion009.Identidad = False
        Me.CnEdicion009.Location = New System.Drawing.Point(118, 113)
        Me.CnEdicion009.Longitud = 90
        Me.CnEdicion009.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion009.MascaraEspecial = ""
        Me.CnEdicion009.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion009.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion009.MaximoNumero = 999999999999999.0R
        Me.CnEdicion009.MaxLength = 0
        Me.CnEdicion009.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion009.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion009.MinimoNumero = -999999999999999.0R
        Me.CnEdicion009.Name = "CnEdicion009"
        Me.CnEdicion009.NumeroCampo = -1
        Me.CnEdicion009.NumeroParametroValorDefecto = 0
        Me.CnEdicion009.NumeroParametroValorFijo = 0
        Me.CnEdicion009.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion009.NumeroTablaFormulario = -1
        Me.CnEdicion009.Requerido = False
        Me.CnEdicion009.Restriccion = ""
        Me.CnEdicion009.SiempreOculto = False
        Me.CnEdicion009.SiempreSoloLectura = False
        Me.CnEdicion009.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion009.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion009.TabIndex = 9999
        Me.CnEdicion009.Tabla = "carnet_manipulador"
        Me.CnEdicion009.TablaEnlacePrincipal = Nothing
        Me.CnEdicion009.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion009.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion009.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion009.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion009.TituloParaGrid = Nothing
        Me.CnEdicion009.TTEdicion = Nothing
        Me.CnEdicion009.TxtDatos = Nothing
        Me.CnEdicion009.ValorDefecto = ""
        Me.CnEdicion009.ValorFijo = ""
        Me.CnEdicion009.ValorFijoCreacion = ""
        '
        'TxtDatos009
        '
        Me.TxtDatos009.Location = New System.Drawing.Point(114, 113)
        Me.TxtDatos009.Name = "TxtDatos009"
        Me.TxtDatos009.ReadOnly = True
        Me.TxtDatos009.Size = New System.Drawing.Size(160, 21)
        Me.TxtDatos009.TabIndex = 9999
        '
        'Lbl009
        '
        Me.Lbl009.AutoSize = True
        Me.Lbl009.Location = New System.Drawing.Point(77, 115)
        Me.Lbl009.Name = "Lbl009"
        Me.Lbl009.Size = New System.Drawing.Size(36, 13)
        Me.Lbl009.TabIndex = 9999
        Me.Lbl009.Text = "Ropo:"
        '
        'CmdFecha008
        '
        Me.CmdFecha008.Image = CType(resources.GetObject("CmdFecha008.Image"), System.Drawing.Image)
        Me.CmdFecha008.Location = New System.Drawing.Point(595, 92)
        Me.CmdFecha008.Name = "CmdFecha008"
        Me.CmdFecha008.Size = New System.Drawing.Size(24, 22)
        Me.CmdFecha008.TabIndex = 9999
        Me.CmdFecha008.UseVisualStyleBackColor = True
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
        Me.CnEdicion008.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion008.AnchoColumnaGrid = 0
        Me.CnEdicion008.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion008.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion008.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion008.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion008.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion008.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion008.Campo = "valido_hasta"
        Me.CnEdicion008.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion008.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion008.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion008.CampoTablaPadre = Nothing
        Me.CnEdicion008.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ"
        Me.CnEdicion008.Clave = False
        Me.CnEdicion008.CmdCombo = Nothing
        Me.CnEdicion008.CmdFecha = Nothing
        Me.CnEdicion008.CmdGrid = Nothing
        Me.CnEdicion008.CmdMantenimiento = Nothing
        Me.CnEdicion008.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion008.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion008.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion008.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion008.Contenedor = Nothing
        Me.CnEdicion008.ContextMenuStrip = Nothing
        Me.CnEdicion008.ConvierteAMayusculas = False
        Me.CnEdicion008.ConvierteAMinusculas = False
        Me.CnEdicion008.CuantosEnlacesCampo = 0
        Me.CnEdicion008.Decimales = 0
        Me.CnEdicion008.EdicionEnCombo = False
        Me.CnEdicion008.EdicionEnGrid = False
        Me.CnEdicion008.EnCreacionOculto = False
        Me.CnEdicion008.EnCreacionSoloLectura = False
        Me.CnEdicion008.EnlacesLookup1 = 0
        Me.CnEdicion008.EnlacesLookup2 = 0
        Me.CnEdicion008.EnlacesLookup3 = 0
        Me.CnEdicion008.EnModificacionOculto = False
        Me.CnEdicion008.EnModificacionSoloLectura = False
        Me.CnEdicion008.EsFechaHoraCreacion = False
        Me.CnEdicion008.EsFechaHoraModificacion = False
        Me.CnEdicion008.Etiqueta = Nothing
        Me.CnEdicion008.Fuente = Nothing
        Me.CnEdicion008.HayMascaraEspecial = False
        Me.CnEdicion008.HayValorDefecto = False
        Me.CnEdicion008.HayValorFijo = False
        Me.CnEdicion008.HayValorFijoCreacion = False
        Me.CnEdicion008.Identidad = False
        Me.CnEdicion008.Location = New System.Drawing.Point(498, 92)
        Me.CnEdicion008.Longitud = 90
        Me.CnEdicion008.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion008.MascaraEspecial = ""
        Me.CnEdicion008.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion008.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion008.MaximoNumero = 999999999999999.0R
        Me.CnEdicion008.MaxLength = 0
        Me.CnEdicion008.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion008.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion008.MinimoNumero = -999999999999999.0R
        Me.CnEdicion008.Name = "CnEdicion008"
        Me.CnEdicion008.NumeroCampo = -1
        Me.CnEdicion008.NumeroParametroValorDefecto = 0
        Me.CnEdicion008.NumeroParametroValorFijo = 0
        Me.CnEdicion008.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion008.NumeroTablaFormulario = -1
        Me.CnEdicion008.Requerido = False
        Me.CnEdicion008.Restriccion = ""
        Me.CnEdicion008.SiempreOculto = False
        Me.CnEdicion008.SiempreSoloLectura = False
        Me.CnEdicion008.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion008.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion008.TabIndex = 9999
        Me.CnEdicion008.Tabla = "carnet_manipulador"
        Me.CnEdicion008.TablaEnlacePrincipal = Nothing
        Me.CnEdicion008.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion008.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion008.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion008.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion008.TituloParaGrid = Nothing
        Me.CnEdicion008.TTEdicion = Nothing
        Me.CnEdicion008.TxtDatos = Nothing
        Me.CnEdicion008.ValorDefecto = ""
        Me.CnEdicion008.ValorFijo = ""
        Me.CnEdicion008.ValorFijoCreacion = ""
        '
        'TxtDatos008
        '
        Me.TxtDatos008.Location = New System.Drawing.Point(494, 92)
        Me.TxtDatos008.Name = "TxtDatos008"
        Me.TxtDatos008.ReadOnly = True
        Me.TxtDatos008.Size = New System.Drawing.Size(100, 21)
        Me.TxtDatos008.TabIndex = 9999
        '
        'Lbl008
        '
        Me.Lbl008.AutoSize = True
        Me.Lbl008.Location = New System.Drawing.Point(423, 94)
        Me.Lbl008.Name = "Lbl008"
        Me.Lbl008.Size = New System.Drawing.Size(70, 13)
        Me.Lbl008.TabIndex = 9999
        Me.Lbl008.Text = "Valido Hasta:"
        '
        'CnEdicion007
        '
        Me.CnEdicion007.AceptaEspacios = True
        Me.CnEdicion007.AceptaMayusculas = True
        Me.CnEdicion007.AceptaMayusculasAcentuadas = False
        Me.CnEdicion007.AceptaMinusculas = False
        Me.CnEdicion007.AceptaMinusculasAcentuadas = False
        Me.CnEdicion007.AceptaNumeros = True
        Me.CnEdicion007.AceptaSimbolos = True
        Me.CnEdicion007.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion007.AnchoColumnaGrid = 0
        Me.CnEdicion007.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion007.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion007.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion007.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion007.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion007.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion007.Campo = "nivel"
        Me.CnEdicion007.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion007.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion007.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion007.CampoTablaPadre = Nothing
        Me.CnEdicion007.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion007.Clave = False
        Me.CnEdicion007.CmdCombo = Nothing
        Me.CnEdicion007.CmdFecha = Nothing
        Me.CnEdicion007.CmdGrid = Nothing
        Me.CnEdicion007.CmdMantenimiento = Nothing
        Me.CnEdicion007.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion007.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion007.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion007.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion007.Contenedor = Nothing
        Me.CnEdicion007.ContextMenuStrip = Nothing
        Me.CnEdicion007.ConvierteAMayusculas = True
        Me.CnEdicion007.ConvierteAMinusculas = False
        Me.CnEdicion007.CuantosEnlacesCampo = 0
        Me.CnEdicion007.Decimales = 0
        Me.CnEdicion007.EdicionEnCombo = False
        Me.CnEdicion007.EdicionEnGrid = False
        Me.CnEdicion007.EnCreacionOculto = False
        Me.CnEdicion007.EnCreacionSoloLectura = False
        Me.CnEdicion007.EnlacesLookup1 = 0
        Me.CnEdicion007.EnlacesLookup2 = 0
        Me.CnEdicion007.EnlacesLookup3 = 0
        Me.CnEdicion007.EnModificacionOculto = False
        Me.CnEdicion007.EnModificacionSoloLectura = False
        Me.CnEdicion007.EsFechaHoraCreacion = False
        Me.CnEdicion007.EsFechaHoraModificacion = False
        Me.CnEdicion007.Etiqueta = Nothing
        Me.CnEdicion007.Fuente = Nothing
        Me.CnEdicion007.HayMascaraEspecial = False
        Me.CnEdicion007.HayValorDefecto = False
        Me.CnEdicion007.HayValorFijo = False
        Me.CnEdicion007.HayValorFijoCreacion = False
        Me.CnEdicion007.Identidad = False
        Me.CnEdicion007.Location = New System.Drawing.Point(118, 92)
        Me.CnEdicion007.Longitud = 90
        Me.CnEdicion007.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion007.MascaraEspecial = ""
        Me.CnEdicion007.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion007.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion007.MaximoNumero = 999999999999999.0R
        Me.CnEdicion007.MaxLength = 0
        Me.CnEdicion007.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion007.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion007.MinimoNumero = -999999999999999.0R
        Me.CnEdicion007.Name = "CnEdicion007"
        Me.CnEdicion007.NumeroCampo = -1
        Me.CnEdicion007.NumeroParametroValorDefecto = 0
        Me.CnEdicion007.NumeroParametroValorFijo = 0
        Me.CnEdicion007.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion007.NumeroTablaFormulario = -1
        Me.CnEdicion007.Requerido = False
        Me.CnEdicion007.Restriccion = ""
        Me.CnEdicion007.SiempreOculto = False
        Me.CnEdicion007.SiempreSoloLectura = False
        Me.CnEdicion007.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion007.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion007.TabIndex = 9999
        Me.CnEdicion007.Tabla = "carnet_manipulador"
        Me.CnEdicion007.TablaEnlacePrincipal = Nothing
        Me.CnEdicion007.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion007.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion007.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion007.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion007.TituloParaGrid = Nothing
        Me.CnEdicion007.TTEdicion = Nothing
        Me.CnEdicion007.TxtDatos = Nothing
        Me.CnEdicion007.ValorDefecto = ""
        Me.CnEdicion007.ValorFijo = ""
        Me.CnEdicion007.ValorFijoCreacion = ""
        '
        'TxtDatos007
        '
        Me.TxtDatos007.Location = New System.Drawing.Point(114, 92)
        Me.TxtDatos007.Name = "TxtDatos007"
        Me.TxtDatos007.ReadOnly = True
        Me.TxtDatos007.Size = New System.Drawing.Size(20, 21)
        Me.TxtDatos007.TabIndex = 9999
        '
        'Lbl007
        '
        Me.Lbl007.AutoSize = True
        Me.Lbl007.Location = New System.Drawing.Point(79, 94)
        Me.Lbl007.Name = "Lbl007"
        Me.Lbl007.Size = New System.Drawing.Size(34, 13)
        Me.Lbl007.TabIndex = 9999
        Me.Lbl007.Text = "Nivel:"
        '
        'TxtLookup0061
        '
        Me.TxtLookup0061.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtLookup0061.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLookup0061.Location = New System.Drawing.Point(189, 74)
        Me.TxtLookup0061.Name = "TxtLookup0061"
        Me.TxtLookup0061.ReadOnly = True
        Me.TxtLookup0061.Size = New System.Drawing.Size(250, 14)
        Me.TxtLookup0061.TabIndex = 9999
        '
        'CmdGrid006
        '
        Me.CmdGrid006.Image = CType(resources.GetObject("CmdGrid006.Image"), System.Drawing.Image)
        Me.CmdGrid006.Location = New System.Drawing.Point(155, 71)
        Me.CmdGrid006.Name = "CmdGrid006"
        Me.CmdGrid006.Size = New System.Drawing.Size(24, 22)
        Me.CmdGrid006.TabIndex = 9999
        Me.CmdGrid006.UseVisualStyleBackColor = True
        '
        'CnEdicion006
        '
        Me.CnEdicion006.AceptaEspacios = True
        Me.CnEdicion006.AceptaMayusculas = True
        Me.CnEdicion006.AceptaMayusculasAcentuadas = False
        Me.CnEdicion006.AceptaMinusculas = False
        Me.CnEdicion006.AceptaMinusculasAcentuadas = False
        Me.CnEdicion006.AceptaNumeros = True
        Me.CnEdicion006.AceptaSimbolos = True
        Me.CnEdicion006.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion006.AnchoColumnaGrid = 0
        Me.CnEdicion006.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion006.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion006.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion006.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion006.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion006.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion006.Campo = "provincia"
        Me.CnEdicion006.CampoEnlacesLookup1 = "nombre_provincia"
        Me.CnEdicion006.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion006.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion006.CampoTablaPadre = Nothing
        Me.CnEdicion006.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion006.Clave = False
        Me.CnEdicion006.CmdCombo = Nothing
        Me.CnEdicion006.CmdFecha = Nothing
        Me.CnEdicion006.CmdGrid = Nothing
        Me.CnEdicion006.CmdMantenimiento = Nothing
        Me.CnEdicion006.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion006.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion006.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion006.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion006.Contenedor = Nothing
        Me.CnEdicion006.ContextMenuStrip = Nothing
        Me.CnEdicion006.ConvierteAMayusculas = True
        Me.CnEdicion006.ConvierteAMinusculas = False
        Me.CnEdicion006.CuantosEnlacesCampo = 0
        Me.CnEdicion006.Decimales = 0
        Me.CnEdicion006.EdicionEnCombo = False
        Me.CnEdicion006.EdicionEnGrid = False
        Me.CnEdicion006.EnCreacionOculto = False
        Me.CnEdicion006.EnCreacionSoloLectura = False
        Me.CnEdicion006.EnlacesLookup1 = 33101
        Me.CnEdicion006.EnlacesLookup2 = 0
        Me.CnEdicion006.EnlacesLookup3 = 0
        Me.CnEdicion006.EnModificacionOculto = False
        Me.CnEdicion006.EnModificacionSoloLectura = False
        Me.CnEdicion006.EsFechaHoraCreacion = False
        Me.CnEdicion006.EsFechaHoraModificacion = False
        Me.CnEdicion006.Etiqueta = Nothing
        Me.CnEdicion006.Fuente = Nothing
        Me.CnEdicion006.HayMascaraEspecial = False
        Me.CnEdicion006.HayValorDefecto = False
        Me.CnEdicion006.HayValorFijo = False
        Me.CnEdicion006.HayValorFijoCreacion = False
        Me.CnEdicion006.Identidad = False
        Me.CnEdicion006.Location = New System.Drawing.Point(118, 71)
        Me.CnEdicion006.Longitud = 90
        Me.CnEdicion006.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion006.MascaraEspecial = ""
        Me.CnEdicion006.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion006.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion006.MaximoNumero = 999999999999999.0R
        Me.CnEdicion006.MaxLength = 0
        Me.CnEdicion006.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion006.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion006.MinimoNumero = -999999999999999.0R
        Me.CnEdicion006.Name = "CnEdicion006"
        Me.CnEdicion006.NumeroCampo = -1
        Me.CnEdicion006.NumeroParametroValorDefecto = 0
        Me.CnEdicion006.NumeroParametroValorFijo = 0
        Me.CnEdicion006.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion006.NumeroTablaFormulario = -1
        Me.CnEdicion006.Requerido = False
        Me.CnEdicion006.Restriccion = ""
        Me.CnEdicion006.SiempreOculto = False
        Me.CnEdicion006.SiempreSoloLectura = False
        Me.CnEdicion006.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion006.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion006.TabIndex = 9999
        Me.CnEdicion006.Tabla = "carnet_manipulador"
        Me.CnEdicion006.TablaEnlacePrincipal = Nothing
        Me.CnEdicion006.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion006.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion006.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion006.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion006.TituloParaGrid = Nothing
        Me.CnEdicion006.TTEdicion = Nothing
        Me.CnEdicion006.TxtDatos = Nothing
        Me.CnEdicion006.ValorDefecto = ""
        Me.CnEdicion006.ValorFijo = ""
        Me.CnEdicion006.ValorFijoCreacion = ""
        '
        'TxtDatos006
        '
        Me.TxtDatos006.Location = New System.Drawing.Point(114, 71)
        Me.TxtDatos006.Name = "TxtDatos006"
        Me.TxtDatos006.ReadOnly = True
        Me.TxtDatos006.Size = New System.Drawing.Size(40, 21)
        Me.TxtDatos006.TabIndex = 9999
        '
        'Lbl006
        '
        Me.Lbl006.AutoSize = True
        Me.Lbl006.Location = New System.Drawing.Point(59, 73)
        Me.Lbl006.Name = "Lbl006"
        Me.Lbl006.Size = New System.Drawing.Size(54, 13)
        Me.Lbl006.TabIndex = 9999
        Me.Lbl006.Text = "Provincia:"
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
        Me.CnEdicion005.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion005.AnchoColumnaGrid = 0
        Me.CnEdicion005.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion005.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion005.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion005.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion005.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion005.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion005.Campo = "poblacion"
        Me.CnEdicion005.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion005.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion005.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion005.CampoTablaPadre = Nothing
        Me.CnEdicion005.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion005.Clave = False
        Me.CnEdicion005.CmdCombo = Nothing
        Me.CnEdicion005.CmdFecha = Nothing
        Me.CnEdicion005.CmdGrid = Nothing
        Me.CnEdicion005.CmdMantenimiento = Nothing
        Me.CnEdicion005.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion005.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion005.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion005.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion005.Contenedor = Nothing
        Me.CnEdicion005.ContextMenuStrip = Nothing
        Me.CnEdicion005.ConvierteAMayusculas = True
        Me.CnEdicion005.ConvierteAMinusculas = False
        Me.CnEdicion005.CuantosEnlacesCampo = 0
        Me.CnEdicion005.Decimales = 0
        Me.CnEdicion005.EdicionEnCombo = False
        Me.CnEdicion005.EdicionEnGrid = False
        Me.CnEdicion005.EnCreacionOculto = False
        Me.CnEdicion005.EnCreacionSoloLectura = False
        Me.CnEdicion005.EnlacesLookup1 = 0
        Me.CnEdicion005.EnlacesLookup2 = 0
        Me.CnEdicion005.EnlacesLookup3 = 0
        Me.CnEdicion005.EnModificacionOculto = False
        Me.CnEdicion005.EnModificacionSoloLectura = False
        Me.CnEdicion005.EsFechaHoraCreacion = False
        Me.CnEdicion005.EsFechaHoraModificacion = False
        Me.CnEdicion005.Etiqueta = Nothing
        Me.CnEdicion005.Fuente = Nothing
        Me.CnEdicion005.HayMascaraEspecial = False
        Me.CnEdicion005.HayValorDefecto = False
        Me.CnEdicion005.HayValorFijo = False
        Me.CnEdicion005.HayValorFijoCreacion = False
        Me.CnEdicion005.Identidad = False
        Me.CnEdicion005.Location = New System.Drawing.Point(118, 50)
        Me.CnEdicion005.Longitud = 90
        Me.CnEdicion005.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion005.MascaraEspecial = ""
        Me.CnEdicion005.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion005.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion005.MaximoNumero = 999999999999999.0R
        Me.CnEdicion005.MaxLength = 0
        Me.CnEdicion005.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion005.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion005.MinimoNumero = -999999999999999.0R
        Me.CnEdicion005.Name = "CnEdicion005"
        Me.CnEdicion005.NumeroCampo = -1
        Me.CnEdicion005.NumeroParametroValorDefecto = 0
        Me.CnEdicion005.NumeroParametroValorFijo = 0
        Me.CnEdicion005.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion005.NumeroTablaFormulario = -1
        Me.CnEdicion005.Requerido = False
        Me.CnEdicion005.Restriccion = ""
        Me.CnEdicion005.SiempreOculto = False
        Me.CnEdicion005.SiempreSoloLectura = False
        Me.CnEdicion005.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion005.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion005.TabIndex = 9999
        Me.CnEdicion005.Tabla = "carnet_manipulador"
        Me.CnEdicion005.TablaEnlacePrincipal = Nothing
        Me.CnEdicion005.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion005.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion005.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion005.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion005.TituloParaGrid = Nothing
        Me.CnEdicion005.TTEdicion = Nothing
        Me.CnEdicion005.TxtDatos = Nothing
        Me.CnEdicion005.ValorDefecto = ""
        Me.CnEdicion005.ValorFijo = ""
        Me.CnEdicion005.ValorFijoCreacion = ""
        '
        'TxtDatos005
        '
        Me.TxtDatos005.Location = New System.Drawing.Point(114, 50)
        Me.TxtDatos005.Name = "TxtDatos005"
        Me.TxtDatos005.ReadOnly = True
        Me.TxtDatos005.Size = New System.Drawing.Size(250, 21)
        Me.TxtDatos005.TabIndex = 9999
        '
        'Lbl005
        '
        Me.Lbl005.AutoSize = True
        Me.Lbl005.Location = New System.Drawing.Point(57, 52)
        Me.Lbl005.Name = "Lbl005"
        Me.Lbl005.Size = New System.Drawing.Size(56, 13)
        Me.Lbl005.TabIndex = 9999
        Me.Lbl005.Text = "Población:"
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
        Me.CnEdicion004.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion004.AnchoColumnaGrid = 0
        Me.CnEdicion004.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion004.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion004.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion004.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion004.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion004.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion004.Campo = "codigo_postal"
        Me.CnEdicion004.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion004.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion004.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion004.CampoTablaPadre = Nothing
        Me.CnEdicion004.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion004.Clave = False
        Me.CnEdicion004.CmdCombo = Nothing
        Me.CnEdicion004.CmdFecha = Nothing
        Me.CnEdicion004.CmdGrid = Nothing
        Me.CnEdicion004.CmdMantenimiento = Nothing
        Me.CnEdicion004.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion004.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion004.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion004.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion004.Contenedor = Nothing
        Me.CnEdicion004.ContextMenuStrip = Nothing
        Me.CnEdicion004.ConvierteAMayusculas = True
        Me.CnEdicion004.ConvierteAMinusculas = False
        Me.CnEdicion004.CuantosEnlacesCampo = 0
        Me.CnEdicion004.Decimales = 0
        Me.CnEdicion004.EdicionEnCombo = False
        Me.CnEdicion004.EdicionEnGrid = False
        Me.CnEdicion004.EnCreacionOculto = False
        Me.CnEdicion004.EnCreacionSoloLectura = False
        Me.CnEdicion004.EnlacesLookup1 = 0
        Me.CnEdicion004.EnlacesLookup2 = 0
        Me.CnEdicion004.EnlacesLookup3 = 0
        Me.CnEdicion004.EnModificacionOculto = False
        Me.CnEdicion004.EnModificacionSoloLectura = False
        Me.CnEdicion004.EsFechaHoraCreacion = False
        Me.CnEdicion004.EsFechaHoraModificacion = False
        Me.CnEdicion004.Etiqueta = Nothing
        Me.CnEdicion004.Fuente = Nothing
        Me.CnEdicion004.HayMascaraEspecial = False
        Me.CnEdicion004.HayValorDefecto = False
        Me.CnEdicion004.HayValorFijo = False
        Me.CnEdicion004.HayValorFijoCreacion = False
        Me.CnEdicion004.Identidad = False
        Me.CnEdicion004.Location = New System.Drawing.Point(498, 29)
        Me.CnEdicion004.Longitud = 90
        Me.CnEdicion004.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion004.MascaraEspecial = ""
        Me.CnEdicion004.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion004.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion004.MaximoNumero = 999999999999999.0R
        Me.CnEdicion004.MaxLength = 0
        Me.CnEdicion004.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion004.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion004.MinimoNumero = -999999999999999.0R
        Me.CnEdicion004.Name = "CnEdicion004"
        Me.CnEdicion004.NumeroCampo = -1
        Me.CnEdicion004.NumeroParametroValorDefecto = 0
        Me.CnEdicion004.NumeroParametroValorFijo = 0
        Me.CnEdicion004.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion004.NumeroTablaFormulario = -1
        Me.CnEdicion004.Requerido = False
        Me.CnEdicion004.Restriccion = ""
        Me.CnEdicion004.SiempreOculto = False
        Me.CnEdicion004.SiempreSoloLectura = False
        Me.CnEdicion004.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion004.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion004.TabIndex = 9999
        Me.CnEdicion004.Tabla = "carnet_manipulador"
        Me.CnEdicion004.TablaEnlacePrincipal = Nothing
        Me.CnEdicion004.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion004.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion004.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion004.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion004.TituloParaGrid = Nothing
        Me.CnEdicion004.TTEdicion = Nothing
        Me.CnEdicion004.TxtDatos = Nothing
        Me.CnEdicion004.ValorDefecto = ""
        Me.CnEdicion004.ValorFijo = ""
        Me.CnEdicion004.ValorFijoCreacion = ""
        '
        'TxtDatos004
        '
        Me.TxtDatos004.Location = New System.Drawing.Point(494, 29)
        Me.TxtDatos004.Name = "TxtDatos004"
        Me.TxtDatos004.ReadOnly = True
        Me.TxtDatos004.Size = New System.Drawing.Size(50, 21)
        Me.TxtDatos004.TabIndex = 9999
        '
        'Lbl004
        '
        Me.Lbl004.AutoSize = True
        Me.Lbl004.Location = New System.Drawing.Point(417, 31)
        Me.Lbl004.Name = "Lbl004"
        Me.Lbl004.Size = New System.Drawing.Size(76, 13)
        Me.Lbl004.TabIndex = 9999
        Me.Lbl004.Text = "Código postal:"
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
        Me.CnEdicion003.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion003.AnchoColumnaGrid = 0
        Me.CnEdicion003.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion003.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion003.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion003.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion003.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion003.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion003.Campo = "domicilio"
        Me.CnEdicion003.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion003.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion003.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion003.CampoTablaPadre = Nothing
        Me.CnEdicion003.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion003.Clave = False
        Me.CnEdicion003.CmdCombo = Nothing
        Me.CnEdicion003.CmdFecha = Nothing
        Me.CnEdicion003.CmdGrid = Nothing
        Me.CnEdicion003.CmdMantenimiento = Nothing
        Me.CnEdicion003.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion003.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion003.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion003.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion003.Contenedor = Nothing
        Me.CnEdicion003.ContextMenuStrip = Nothing
        Me.CnEdicion003.ConvierteAMayusculas = True
        Me.CnEdicion003.ConvierteAMinusculas = False
        Me.CnEdicion003.CuantosEnlacesCampo = 0
        Me.CnEdicion003.Decimales = 0
        Me.CnEdicion003.EdicionEnCombo = False
        Me.CnEdicion003.EdicionEnGrid = False
        Me.CnEdicion003.EnCreacionOculto = False
        Me.CnEdicion003.EnCreacionSoloLectura = False
        Me.CnEdicion003.EnlacesLookup1 = 0
        Me.CnEdicion003.EnlacesLookup2 = 0
        Me.CnEdicion003.EnlacesLookup3 = 0
        Me.CnEdicion003.EnModificacionOculto = False
        Me.CnEdicion003.EnModificacionSoloLectura = False
        Me.CnEdicion003.EsFechaHoraCreacion = False
        Me.CnEdicion003.EsFechaHoraModificacion = False
        Me.CnEdicion003.Etiqueta = Nothing
        Me.CnEdicion003.Fuente = Nothing
        Me.CnEdicion003.HayMascaraEspecial = False
        Me.CnEdicion003.HayValorDefecto = False
        Me.CnEdicion003.HayValorFijo = False
        Me.CnEdicion003.HayValorFijoCreacion = False
        Me.CnEdicion003.Identidad = False
        Me.CnEdicion003.Location = New System.Drawing.Point(118, 29)
        Me.CnEdicion003.Longitud = 90
        Me.CnEdicion003.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion003.MascaraEspecial = ""
        Me.CnEdicion003.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion003.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion003.MaximoNumero = 999999999999999.0R
        Me.CnEdicion003.MaxLength = 0
        Me.CnEdicion003.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion003.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion003.MinimoNumero = -999999999999999.0R
        Me.CnEdicion003.Name = "CnEdicion003"
        Me.CnEdicion003.NumeroCampo = -1
        Me.CnEdicion003.NumeroParametroValorDefecto = 0
        Me.CnEdicion003.NumeroParametroValorFijo = 0
        Me.CnEdicion003.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion003.NumeroTablaFormulario = -1
        Me.CnEdicion003.Requerido = False
        Me.CnEdicion003.Restriccion = ""
        Me.CnEdicion003.SiempreOculto = False
        Me.CnEdicion003.SiempreSoloLectura = False
        Me.CnEdicion003.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion003.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion003.TabIndex = 9999
        Me.CnEdicion003.Tabla = "carnet_manipulador"
        Me.CnEdicion003.TablaEnlacePrincipal = Nothing
        Me.CnEdicion003.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion003.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion003.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion003.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion003.TituloParaGrid = Nothing
        Me.CnEdicion003.TTEdicion = Nothing
        Me.CnEdicion003.TxtDatos = Nothing
        Me.CnEdicion003.ValorDefecto = ""
        Me.CnEdicion003.ValorFijo = ""
        Me.CnEdicion003.ValorFijoCreacion = ""
        '
        'TxtDatos003
        '
        Me.TxtDatos003.Location = New System.Drawing.Point(114, 29)
        Me.TxtDatos003.Name = "TxtDatos003"
        Me.TxtDatos003.ReadOnly = True
        Me.TxtDatos003.Size = New System.Drawing.Size(250, 21)
        Me.TxtDatos003.TabIndex = 9999
        '
        'Lbl003
        '
        Me.Lbl003.AutoSize = True
        Me.Lbl003.Location = New System.Drawing.Point(62, 31)
        Me.Lbl003.Name = "Lbl003"
        Me.Lbl003.Size = New System.Drawing.Size(51, 13)
        Me.Lbl003.TabIndex = 9999
        Me.Lbl003.Text = "Domicilio:"
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
        Me.CnEdicion002.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion002.AnchoColumnaGrid = 0
        Me.CnEdicion002.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion002.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion002.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion002.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion002.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion002.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion002.Campo = "nombre"
        Me.CnEdicion002.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion002.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion002.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion002.CampoTablaPadre = Nothing
        Me.CnEdicion002.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion002.Clave = False
        Me.CnEdicion002.CmdCombo = Nothing
        Me.CnEdicion002.CmdFecha = Nothing
        Me.CnEdicion002.CmdGrid = Nothing
        Me.CnEdicion002.CmdMantenimiento = Nothing
        Me.CnEdicion002.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion002.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion002.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion002.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion002.Contenedor = Nothing
        Me.CnEdicion002.ContextMenuStrip = Nothing
        Me.CnEdicion002.ConvierteAMayusculas = True
        Me.CnEdicion002.ConvierteAMinusculas = False
        Me.CnEdicion002.CuantosEnlacesCampo = 0
        Me.CnEdicion002.Decimales = 0
        Me.CnEdicion002.EdicionEnCombo = False
        Me.CnEdicion002.EdicionEnGrid = False
        Me.CnEdicion002.EnCreacionOculto = False
        Me.CnEdicion002.EnCreacionSoloLectura = False
        Me.CnEdicion002.EnlacesLookup1 = 0
        Me.CnEdicion002.EnlacesLookup2 = 0
        Me.CnEdicion002.EnlacesLookup3 = 0
        Me.CnEdicion002.EnModificacionOculto = False
        Me.CnEdicion002.EnModificacionSoloLectura = False
        Me.CnEdicion002.EsFechaHoraCreacion = False
        Me.CnEdicion002.EsFechaHoraModificacion = False
        Me.CnEdicion002.Etiqueta = Nothing
        Me.CnEdicion002.Fuente = Nothing
        Me.CnEdicion002.HayMascaraEspecial = False
        Me.CnEdicion002.HayValorDefecto = False
        Me.CnEdicion002.HayValorFijo = False
        Me.CnEdicion002.HayValorFijoCreacion = False
        Me.CnEdicion002.Identidad = False
        Me.CnEdicion002.Location = New System.Drawing.Point(498, 8)
        Me.CnEdicion002.Longitud = 90
        Me.CnEdicion002.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion002.MascaraEspecial = ""
        Me.CnEdicion002.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion002.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion002.MaximoNumero = 999999999999999.0R
        Me.CnEdicion002.MaxLength = 0
        Me.CnEdicion002.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion002.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion002.MinimoNumero = -999999999999999.0R
        Me.CnEdicion002.Name = "CnEdicion002"
        Me.CnEdicion002.NumeroCampo = -1
        Me.CnEdicion002.NumeroParametroValorDefecto = 0
        Me.CnEdicion002.NumeroParametroValorFijo = 0
        Me.CnEdicion002.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion002.NumeroTablaFormulario = -1
        Me.CnEdicion002.Requerido = False
        Me.CnEdicion002.Restriccion = ""
        Me.CnEdicion002.SiempreOculto = False
        Me.CnEdicion002.SiempreSoloLectura = False
        Me.CnEdicion002.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion002.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion002.TabIndex = 9999
        Me.CnEdicion002.Tabla = "carnet_manipulador"
        Me.CnEdicion002.TablaEnlacePrincipal = Nothing
        Me.CnEdicion002.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion002.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion002.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion002.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion002.TituloParaGrid = Nothing
        Me.CnEdicion002.TTEdicion = Nothing
        Me.CnEdicion002.TxtDatos = Nothing
        Me.CnEdicion002.ValorDefecto = ""
        Me.CnEdicion002.ValorFijo = ""
        Me.CnEdicion002.ValorFijoCreacion = ""
        '
        'TxtDatos002
        '
        Me.TxtDatos002.Location = New System.Drawing.Point(494, 8)
        Me.TxtDatos002.Name = "TxtDatos002"
        Me.TxtDatos002.ReadOnly = True
        Me.TxtDatos002.Size = New System.Drawing.Size(250, 21)
        Me.TxtDatos002.TabIndex = 9999
        '
        'Lbl002
        '
        Me.Lbl002.AutoSize = True
        Me.Lbl002.Location = New System.Drawing.Point(446, 10)
        Me.Lbl002.Name = "Lbl002"
        Me.Lbl002.Size = New System.Drawing.Size(47, 13)
        Me.Lbl002.TabIndex = 9999
        Me.Lbl002.Text = "nombre:"
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
        Me.CnEdicion001.Alineacion = System.Windows.Forms.HorizontalAlignment.Left
        Me.CnEdicion001.AnchoColumnaGrid = 0
        Me.CnEdicion001.AnchoColumnaGridEnlaceLookup1 = 0
        Me.CnEdicion001.AnchoColumnaGridEnlaceLookup2 = 0
        Me.CnEdicion001.AnchoColumnaGridEnlaceLookup3 = 0
        Me.CnEdicion001.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.CnEdicion001.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.CnEdicion001.BackColor = System.Drawing.Color.Yellow
        Me.CnEdicion001.Campo = "dni"
        Me.CnEdicion001.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion001.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion001.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion001.CampoTablaPadre = Nothing
        Me.CnEdicion001.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.:;<>{}!¡?¿@#$%&/\()[]=+*"
        Me.CnEdicion001.Clave = False
        Me.CnEdicion001.CmdCombo = Nothing
        Me.CnEdicion001.CmdFecha = Nothing
        Me.CnEdicion001.CmdGrid = Nothing
        Me.CnEdicion001.CmdMantenimiento = Nothing
        Me.CnEdicion001.ColorFondo = System.Drawing.Color.White
        Me.CnEdicion001.ColorFondoControl = System.Drawing.Color.Yellow
        Me.CnEdicion001.ColorFondoRequerido = System.Drawing.Color.Yellow
        Me.CnEdicion001.ColorTexto = System.Drawing.SystemColors.ControlText
        Me.CnEdicion001.Contenedor = Nothing
        Me.CnEdicion001.ContextMenuStrip = Nothing
        Me.CnEdicion001.ConvierteAMayusculas = True
        Me.CnEdicion001.ConvierteAMinusculas = False
        Me.CnEdicion001.CuantosEnlacesCampo = 0
        Me.CnEdicion001.Decimales = 0
        Me.CnEdicion001.EdicionEnCombo = False
        Me.CnEdicion001.EdicionEnGrid = False
        Me.CnEdicion001.EnCreacionOculto = False
        Me.CnEdicion001.EnCreacionSoloLectura = False
        Me.CnEdicion001.EnlacesLookup1 = 0
        Me.CnEdicion001.EnlacesLookup2 = 0
        Me.CnEdicion001.EnlacesLookup3 = 0
        Me.CnEdicion001.EnModificacionOculto = False
        Me.CnEdicion001.EnModificacionSoloLectura = False
        Me.CnEdicion001.EsFechaHoraCreacion = False
        Me.CnEdicion001.EsFechaHoraModificacion = False
        Me.CnEdicion001.Etiqueta = Nothing
        Me.CnEdicion001.Fuente = Nothing
        Me.CnEdicion001.HayMascaraEspecial = False
        Me.CnEdicion001.HayValorDefecto = False
        Me.CnEdicion001.HayValorFijo = False
        Me.CnEdicion001.HayValorFijoCreacion = False
        Me.CnEdicion001.Identidad = False
        Me.CnEdicion001.Location = New System.Drawing.Point(118, 8)
        Me.CnEdicion001.Longitud = 90
        Me.CnEdicion001.Margin = New System.Windows.Forms.Padding(0)
        Me.CnEdicion001.MascaraEspecial = ""
        Me.CnEdicion001.MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.CnEdicion001.MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)
        Me.CnEdicion001.MaximoNumero = 999999999999999.0R
        Me.CnEdicion001.MaxLength = 0
        Me.CnEdicion001.MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.CnEdicion001.MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)
        Me.CnEdicion001.MinimoNumero = -999999999999999.0R
        Me.CnEdicion001.Name = "CnEdicion001"
        Me.CnEdicion001.NumeroCampo = -1
        Me.CnEdicion001.NumeroParametroValorDefecto = 0
        Me.CnEdicion001.NumeroParametroValorFijo = 0
        Me.CnEdicion001.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion001.NumeroTablaFormulario = -1
        Me.CnEdicion001.Requerido = False
        Me.CnEdicion001.Restriccion = ""
        Me.CnEdicion001.SiempreOculto = False
        Me.CnEdicion001.SiempreSoloLectura = False
        Me.CnEdicion001.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion001.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion001.TabIndex = 9999
        Me.CnEdicion001.Tabla = "carnet_manipulador"
        Me.CnEdicion001.TablaEnlacePrincipal = Nothing
        Me.CnEdicion001.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion001.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion001.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion001.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion001.TituloParaGrid = Nothing
        Me.CnEdicion001.TTEdicion = Nothing
        Me.CnEdicion001.TxtDatos = Nothing
        Me.CnEdicion001.ValorDefecto = ""
        Me.CnEdicion001.ValorFijo = ""
        Me.CnEdicion001.ValorFijoCreacion = ""
        '
        'TxtDatos001
        '
        Me.TxtDatos001.Location = New System.Drawing.Point(114, 8)
        Me.TxtDatos001.Name = "TxtDatos001"
        Me.TxtDatos001.ReadOnly = True
        Me.TxtDatos001.Size = New System.Drawing.Size(120, 21)
        Me.TxtDatos001.TabIndex = 9999
        '
        'Lbl001
        '
        Me.Lbl001.AutoSize = True
        Me.Lbl001.Location = New System.Drawing.Point(87, 10)
        Me.Lbl001.Name = "Lbl001"
        Me.Lbl001.Size = New System.Drawing.Size(26, 13)
        Me.Lbl001.TabIndex = 9999
        Me.Lbl001.Text = "Dni:"
        '
        'TP01
        '
        Me.TP01.CausesValidation = False
        Me.TP01.Location = New System.Drawing.Point(4, 4)
        Me.TP01.Name = "TP01"
        Me.TP01.Size = New System.Drawing.Size(1132, 138)
        Me.TP01.TabIndex = 9
        Me.TP01.Text = "1"
        Me.TP01.UseVisualStyleBackColor = True
        '
        'TP02
        '
        Me.TP02.CausesValidation = False
        Me.TP02.Location = New System.Drawing.Point(4, 4)
        Me.TP02.Name = "TP02"
        Me.TP02.Size = New System.Drawing.Size(1132, 138)
        Me.TP02.TabIndex = 2
        Me.TP02.Text = "2"
        Me.TP02.UseVisualStyleBackColor = True
        '
        'TP03
        '
        Me.TP03.CausesValidation = False
        Me.TP03.Location = New System.Drawing.Point(4, 4)
        Me.TP03.Name = "TP03"
        Me.TP03.Size = New System.Drawing.Size(1132, 138)
        Me.TP03.TabIndex = 10
        Me.TP03.Text = "3"
        Me.TP03.UseVisualStyleBackColor = True
        '
        'TP04
        '
        Me.TP04.CausesValidation = False
        Me.TP04.Location = New System.Drawing.Point(4, 4)
        Me.TP04.Name = "TP04"
        Me.TP04.Size = New System.Drawing.Size(1132, 138)
        Me.TP04.TabIndex = 3
        Me.TP04.Text = "4"
        Me.TP04.UseVisualStyleBackColor = True
        '
        'TP05
        '
        Me.TP05.CausesValidation = False
        Me.TP05.Location = New System.Drawing.Point(4, 4)
        Me.TP05.Name = "TP05"
        Me.TP05.Size = New System.Drawing.Size(1132, 138)
        Me.TP05.TabIndex = 4
        Me.TP05.Text = "5"
        Me.TP05.UseVisualStyleBackColor = True
        '
        'TP06
        '
        Me.TP06.CausesValidation = False
        Me.TP06.Location = New System.Drawing.Point(4, 4)
        Me.TP06.Name = "TP06"
        Me.TP06.Size = New System.Drawing.Size(1132, 138)
        Me.TP06.TabIndex = 5
        Me.TP06.Text = "6"
        Me.TP06.UseVisualStyleBackColor = True
        '
        'TP07
        '
        Me.TP07.CausesValidation = False
        Me.TP07.Location = New System.Drawing.Point(4, 4)
        Me.TP07.Name = "TP07"
        Me.TP07.Size = New System.Drawing.Size(1132, 138)
        Me.TP07.TabIndex = 6
        Me.TP07.Text = "7"
        Me.TP07.UseVisualStyleBackColor = True
        '
        'TP08
        '
        Me.TP08.CausesValidation = False
        Me.TP08.Location = New System.Drawing.Point(4, 4)
        Me.TP08.Name = "TP08"
        Me.TP08.Size = New System.Drawing.Size(1132, 138)
        Me.TP08.TabIndex = 7
        Me.TP08.Text = "8"
        Me.TP08.UseVisualStyleBackColor = True
        '
        'TP09
        '
        Me.TP09.CausesValidation = False
        Me.TP09.Location = New System.Drawing.Point(4, 4)
        Me.TP09.Name = "TP09"
        Me.TP09.Size = New System.Drawing.Size(1132, 138)
        Me.TP09.TabIndex = 8
        Me.TP09.Text = "9"
        Me.TP09.UseVisualStyleBackColor = True
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
        Me.TabGeneral.Location = New System.Drawing.Point(2, 148)
        Me.TabGeneral.Margin = New System.Windows.Forms.Padding(0)
        Me.TabGeneral.Name = "TabGeneral"
        Me.TabGeneral.SelectedIndex = 0
        Me.TabGeneral.Size = New System.Drawing.Size(1184, 28)
        Me.TabGeneral.TabIndex = 0
        '
        'TabPage00
        '
        Me.TabPage00.CausesValidation = False
        Me.TabPage00.Location = New System.Drawing.Point(4, 4)
        Me.TabPage00.Name = "TabPage00"
        Me.TabPage00.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage00.Size = New System.Drawing.Size(1176, 2)
        Me.TabPage00.TabIndex = 0
        Me.TabPage00.Text = "carnet_manipulador"
        Me.TabPage00.UseVisualStyleBackColor = True
        '
        'TabPage01
        '
        Me.TabPage01.CausesValidation = False
        Me.TabPage01.Location = New System.Drawing.Point(4, 4)
        Me.TabPage01.Name = "TabPage01"
        Me.TabPage01.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage01.TabIndex = 9
        Me.TabPage01.Text = "1"
        Me.TabPage01.UseVisualStyleBackColor = True
        '
        'TabPage02
        '
        Me.TabPage02.CausesValidation = False
        Me.TabPage02.Location = New System.Drawing.Point(4, 4)
        Me.TabPage02.Name = "TabPage02"
        Me.TabPage02.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage02.Size = New System.Drawing.Size(1176, 2)
        Me.TabPage02.TabIndex = 1
        Me.TabPage02.Text = "2"
        Me.TabPage02.UseVisualStyleBackColor = True
        '
        'TabPage03
        '
        Me.TabPage03.CausesValidation = False
        Me.TabPage03.Location = New System.Drawing.Point(4, 4)
        Me.TabPage03.Name = "TabPage03"
        Me.TabPage03.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage03.TabIndex = 2
        Me.TabPage03.Text = "3"
        Me.TabPage03.UseVisualStyleBackColor = True
        '
        'TabPage04
        '
        Me.TabPage04.CausesValidation = False
        Me.TabPage04.Location = New System.Drawing.Point(4, 4)
        Me.TabPage04.Name = "TabPage04"
        Me.TabPage04.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage04.TabIndex = 3
        Me.TabPage04.Text = "4"
        Me.TabPage04.UseVisualStyleBackColor = True
        '
        'TabPage05
        '
        Me.TabPage05.CausesValidation = False
        Me.TabPage05.Location = New System.Drawing.Point(4, 4)
        Me.TabPage05.Name = "TabPage05"
        Me.TabPage05.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage05.TabIndex = 4
        Me.TabPage05.Text = "5"
        Me.TabPage05.UseVisualStyleBackColor = True
        '
        'TabPage06
        '
        Me.TabPage06.CausesValidation = False
        Me.TabPage06.Location = New System.Drawing.Point(4, 4)
        Me.TabPage06.Name = "TabPage06"
        Me.TabPage06.Size = New System.Drawing.Size(1176, 2)
        Me.TabPage06.TabIndex = 5
        Me.TabPage06.Text = "6"
        Me.TabPage06.UseVisualStyleBackColor = True
        '
        'TabPage07
        '
        Me.TabPage07.CausesValidation = False
        Me.TabPage07.Location = New System.Drawing.Point(4, 4)
        Me.TabPage07.Name = "TabPage07"
        Me.TabPage07.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage07.TabIndex = 6
        Me.TabPage07.Text = "7"
        Me.TabPage07.UseVisualStyleBackColor = True
        '
        'TabPage08
        '
        Me.TabPage08.CausesValidation = False
        Me.TabPage08.Location = New System.Drawing.Point(4, 4)
        Me.TabPage08.Name = "TabPage08"
        Me.TabPage08.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage08.TabIndex = 7
        Me.TabPage08.Text = "8"
        Me.TabPage08.UseVisualStyleBackColor = True
        '
        'TabPage09
        '
        Me.TabPage09.CausesValidation = False
        Me.TabPage09.Location = New System.Drawing.Point(4, 4)
        Me.TabPage09.Name = "TabPage09"
        Me.TabPage09.Size = New System.Drawing.Size(1176, 693)
        Me.TabPage09.TabIndex = 8
        Me.TabPage09.Text = "9"
        Me.TabPage09.UseVisualStyleBackColor = True
        '
        'PanelInferior
        '
        Me.PanelInferior.BackColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.CausesValidation = False
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 242)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(1184, 28)
        Me.PanelInferior.TabIndex = 9999
        '
        'FrmCarnetManipulador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1184, 270)
        Me.Controls.Add(Me.PanelSuperior)
        Me.Controls.Add(Me.PanelCentral)
        Me.Controls.Add(Me.PanelInferior)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmCarnetManipulador"
        Me.Text = "Carnets manipuladores"
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelSuperior.PerformLayout()
        Me.PanelCentral.ResumeLayout(False)
        Me.TabCabecera.ResumeLayout(False)
        Me.TP00.ResumeLayout(False)
        Me.TP00.PerformLayout()
        Me.TabGeneral.ResumeLayout(False)
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
Friend WithEvents Lbl003 As Label
Friend WithEvents TxtDatos003 As TextBox
Friend WithEvents CnEdicion003 As CnEdicion.CnEdicion
Friend WithEvents Lbl004 As Label
Friend WithEvents TxtDatos004 As TextBox
Friend WithEvents CnEdicion004 As CnEdicion.CnEdicion
Friend WithEvents Lbl005 As Label
Friend WithEvents TxtDatos005 As TextBox
Friend WithEvents CnEdicion005 As CnEdicion.CnEdicion
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
Friend WithEvents Lbl010 As Label
Friend WithEvents TxtDatos010 As TextBox
Friend WithEvents CnEdicion010 As CnEdicion.CnEdicion
Friend WithEvents CmdFecha010 As Button
End Class
