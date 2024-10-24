<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frmusossigpac

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmusossigpac))
        Me.PanelSuperior = New System.Windows.Forms.Panel()
        Me.CnTabla01 = New CnTabla.CnTabla()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.PanelCentral = New System.Windows.Forms.Panel()
        Me.TabCabecera = New System.Windows.Forms.TabControl()
        Me.TP00 = New System.Windows.Forms.TabPage()
        Me.CnEdicion004 = New CnEdicion.CnEdicion()
        Me.TxtDatos004 = New System.Windows.Forms.TextBox()
        Me.Lbl004 = New System.Windows.Forms.Label()
        Me.CnEdicion003 = New CnEdicion.CnEdicion()
        Me.TxtDatos003 = New System.Windows.Forms.TextBox()
        Me.Lbl003 = New System.Windows.Forms.Label()
        Me.CnEdicion002 = New CnEdicion.CnEdicion()
        Me.TxtDatos002 = New System.Windows.Forms.TextBox()
        Me.Lbl002 = New System.Windows.Forms.Label()
        Me.TxtLookup0011 = New System.Windows.Forms.TextBox()
        Me.CmdGrid001 = New System.Windows.Forms.Button()
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
        Me.CnTabla01.SeleccionAdicional = Nothing
        Me.CnTabla01.Size = New System.Drawing.Size(550, 52)
        Me.CnTabla01.TabIndex = 10000
        Me.CnTabla01.Tabla = "usos_sigpac"
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
        Me.PanelCentral.Size = New System.Drawing.Size(1184, 114)
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
        Me.TabCabecera.Size = New System.Drawing.Size(1180, 83)
        Me.TabCabecera.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabCabecera.TabIndex = 9999
        '
        'TP00
        '
        Me.TP00.CausesValidation = False
        Me.TP00.Controls.Add(Me.CnEdicion004)
        Me.TP00.Controls.Add(Me.TxtDatos004)
        Me.TP00.Controls.Add(Me.Lbl004)
        Me.TP00.Controls.Add(Me.CnEdicion003)
        Me.TP00.Controls.Add(Me.TxtDatos003)
        Me.TP00.Controls.Add(Me.Lbl003)
        Me.TP00.Controls.Add(Me.CnEdicion002)
        Me.TP00.Controls.Add(Me.TxtDatos002)
        Me.TP00.Controls.Add(Me.Lbl002)
        Me.TP00.Controls.Add(Me.TxtLookup0011)
        Me.TP00.Controls.Add(Me.CmdGrid001)
        Me.TP00.Controls.Add(Me.CnEdicion001)
        Me.TP00.Controls.Add(Me.TxtDatos001)
        Me.TP00.Controls.Add(Me.Lbl001)
        Me.TP00.Location = New System.Drawing.Point(4, 4)
        Me.TP00.Margin = New System.Windows.Forms.Padding(0)
        Me.TP00.Name = "TP00"
        Me.TP00.Size = New System.Drawing.Size(1092, 75)
        Me.TP00.TabIndex = 1
        Me.TP00.Text = "usos_sigpac"
        Me.TP00.UseVisualStyleBackColor = True
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
        Me.CnEdicion004.Campo = "declaracion_sn"
        Me.CnEdicion004.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion004.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion004.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion004.CampoTablaPadre = Nothing
        Me.CnEdicion004.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
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
        Me.CnEdicion004.HayValorDefecto = True
        Me.CnEdicion004.HayValorFijo = False
        Me.CnEdicion004.HayValorFijoCreacion = False
        Me.CnEdicion004.Identidad = False
        Me.CnEdicion004.Location = New System.Drawing.Point(118, 50)
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
        Me.CnEdicion004.Tabla = "usos_sigpac"
        Me.CnEdicion004.TablaEnlacePrincipal = Nothing
        Me.CnEdicion004.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto
        Me.CnEdicion004.TituloGridEnlaceLookup1 = Nothing
        Me.CnEdicion004.TituloGridEnlaceLookup2 = Nothing
        Me.CnEdicion004.TituloGridEnlaceLookup3 = Nothing
        Me.CnEdicion004.TituloParaGrid = Nothing
        Me.CnEdicion004.TTEdicion = Nothing
        Me.CnEdicion004.TxtDatos = Nothing
        Me.CnEdicion004.ValorDefecto = "N"
        Me.CnEdicion004.ValorFijo = ""
        Me.CnEdicion004.ValorFijoCreacion = ""
        '
        'TxtDatos004
        '
        Me.TxtDatos004.Location = New System.Drawing.Point(114, 50)
        Me.TxtDatos004.Name = "TxtDatos004"
        Me.TxtDatos004.ReadOnly = True
        Me.TxtDatos004.Size = New System.Drawing.Size(20, 21)
        Me.TxtDatos004.TabIndex = 9999
        '
        'Lbl004
        '
        Me.Lbl004.AutoSize = True
        Me.Lbl004.Location = New System.Drawing.Point(32, 52)
        Me.Lbl004.Name = "Lbl004"
        Me.Lbl004.Size = New System.Drawing.Size(81, 13)
        Me.Lbl004.TabIndex = 9999
        Me.Lbl004.Text = "Declaracion Sn:"
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
        Me.CnEdicion003.Campo = "descripcion"
        Me.CnEdicion003.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion003.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion003.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion003.CampoTablaPadre = Nothing
        Me.CnEdicion003.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
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
        Me.CnEdicion003.Location = New System.Drawing.Point(498, 29)
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
        Me.CnEdicion003.Tabla = "usos_sigpac"
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
        Me.TxtDatos003.Location = New System.Drawing.Point(494, 29)
        Me.TxtDatos003.Name = "TxtDatos003"
        Me.TxtDatos003.ReadOnly = True
        Me.TxtDatos003.Size = New System.Drawing.Size(391, 21)
        Me.TxtDatos003.TabIndex = 9999
        '
        'Lbl003
        '
        Me.Lbl003.AutoSize = True
        Me.Lbl003.Location = New System.Drawing.Point(428, 31)
        Me.Lbl003.Name = "Lbl003"
        Me.Lbl003.Size = New System.Drawing.Size(65, 13)
        Me.Lbl003.TabIndex = 9999
        Me.Lbl003.Text = "Descripción:"
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
        Me.CnEdicion002.Campo = "uso_sigpac"
        Me.CnEdicion002.CampoEnlacesLookup1 = Nothing
        Me.CnEdicion002.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion002.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion002.CampoTablaPadre = Nothing
        Me.CnEdicion002.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
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
        Me.CnEdicion002.Location = New System.Drawing.Point(118, 29)
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
        Me.CnEdicion002.Tabla = "usos_sigpac"
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
        Me.TxtDatos002.Location = New System.Drawing.Point(114, 29)
        Me.TxtDatos002.Name = "TxtDatos002"
        Me.TxtDatos002.ReadOnly = True
        Me.TxtDatos002.Size = New System.Drawing.Size(20, 21)
        Me.TxtDatos002.TabIndex = 9999
        '
        'Lbl002
        '
        Me.Lbl002.AutoSize = True
        Me.Lbl002.Location = New System.Drawing.Point(50, 31)
        Me.Lbl002.Name = "Lbl002"
        Me.Lbl002.Size = New System.Drawing.Size(63, 13)
        Me.Lbl002.TabIndex = 9999
        Me.Lbl002.Text = "Uso Sigpac:"
        '
        'TxtLookup0011
        '
        Me.TxtLookup0011.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtLookup0011.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLookup0011.Location = New System.Drawing.Point(309, 11)
        Me.TxtLookup0011.Name = "TxtLookup0011"
        Me.TxtLookup0011.ReadOnly = True
        Me.TxtLookup0011.Size = New System.Drawing.Size(250, 14)
        Me.TxtLookup0011.TabIndex = 9999
        '
        'CmdGrid001
        '
        Me.CmdGrid001.Image = CType(resources.GetObject("CmdGrid001.Image"), System.Drawing.Image)
        Me.CmdGrid001.Location = New System.Drawing.Point(275, 8)
        Me.CmdGrid001.Name = "CmdGrid001"
        Me.CmdGrid001.Size = New System.Drawing.Size(24, 22)
        Me.CmdGrid001.TabIndex = 9999
        Me.CmdGrid001.UseVisualStyleBackColor = True
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
        Me.CnEdicion001.Campo = "empresa"
        Me.CnEdicion001.CampoEnlacesLookup1 = "razon_social"
        Me.CnEdicion001.CampoEnlacesLookup2 = Nothing
        Me.CnEdicion001.CampoEnlacesLookup3 = Nothing
        Me.CnEdicion001.CampoTablaPadre = Nothing
        Me.CnEdicion001.CaracteresAceptables = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*"
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
        Me.CnEdicion001.EnlacesLookup1 = 33245
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
        Me.CnEdicion001.HayValorFijo = True
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
        Me.CnEdicion001.NumeroParametroValorFijo = 1
        Me.CnEdicion001.NumeroParametroValorFijoCreacion = 0
        Me.CnEdicion001.NumeroTablaFormulario = -1
        Me.CnEdicion001.Requerido = False
        Me.CnEdicion001.Restriccion = ""
        Me.CnEdicion001.SiempreOculto = False
        Me.CnEdicion001.SiempreSoloLectura = True
        Me.CnEdicion001.Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado
        Me.CnEdicion001.Size = New System.Drawing.Size(30, 20)
        Me.CnEdicion001.TabIndex = 9999
        Me.CnEdicion001.Tabla = "usos_sigpac"
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
        Me.TxtDatos001.Size = New System.Drawing.Size(160, 21)
        Me.TxtDatos001.TabIndex = 9999
        '
        'Lbl001
        '
        Me.Lbl001.AutoSize = True
        Me.Lbl001.Location = New System.Drawing.Point(61, 10)
        Me.Lbl001.Name = "Lbl001"
        Me.Lbl001.Size = New System.Drawing.Size(52, 13)
        Me.Lbl001.TabIndex = 9999
        Me.Lbl001.Text = "Empresa:"
        '
        'TP01
        '
        Me.TP01.CausesValidation = False
        Me.TP01.Location = New System.Drawing.Point(4, 4)
        Me.TP01.Name = "TP01"
        Me.TP01.Size = New System.Drawing.Size(1092, 75)
        Me.TP01.TabIndex = 9
        Me.TP01.Text = "1"
        Me.TP01.UseVisualStyleBackColor = True
        '
        'TP02
        '
        Me.TP02.CausesValidation = False
        Me.TP02.Location = New System.Drawing.Point(4, 4)
        Me.TP02.Name = "TP02"
        Me.TP02.Size = New System.Drawing.Size(1092, 75)
        Me.TP02.TabIndex = 2
        Me.TP02.Text = "2"
        Me.TP02.UseVisualStyleBackColor = True
        '
        'TP03
        '
        Me.TP03.CausesValidation = False
        Me.TP03.Location = New System.Drawing.Point(4, 4)
        Me.TP03.Name = "TP03"
        Me.TP03.Size = New System.Drawing.Size(1092, 75)
        Me.TP03.TabIndex = 10
        Me.TP03.Text = "3"
        Me.TP03.UseVisualStyleBackColor = True
        '
        'TP04
        '
        Me.TP04.CausesValidation = False
        Me.TP04.Location = New System.Drawing.Point(4, 4)
        Me.TP04.Name = "TP04"
        Me.TP04.Size = New System.Drawing.Size(1092, 75)
        Me.TP04.TabIndex = 3
        Me.TP04.Text = "4"
        Me.TP04.UseVisualStyleBackColor = True
        '
        'TP05
        '
        Me.TP05.CausesValidation = False
        Me.TP05.Location = New System.Drawing.Point(4, 4)
        Me.TP05.Name = "TP05"
        Me.TP05.Size = New System.Drawing.Size(1092, 75)
        Me.TP05.TabIndex = 4
        Me.TP05.Text = "5"
        Me.TP05.UseVisualStyleBackColor = True
        '
        'TP06
        '
        Me.TP06.CausesValidation = False
        Me.TP06.Location = New System.Drawing.Point(4, 4)
        Me.TP06.Name = "TP06"
        Me.TP06.Size = New System.Drawing.Size(1092, 75)
        Me.TP06.TabIndex = 5
        Me.TP06.Text = "6"
        Me.TP06.UseVisualStyleBackColor = True
        '
        'TP07
        '
        Me.TP07.CausesValidation = False
        Me.TP07.Location = New System.Drawing.Point(4, 4)
        Me.TP07.Name = "TP07"
        Me.TP07.Size = New System.Drawing.Size(1092, 75)
        Me.TP07.TabIndex = 6
        Me.TP07.Text = "7"
        Me.TP07.UseVisualStyleBackColor = True
        '
        'TP08
        '
        Me.TP08.CausesValidation = False
        Me.TP08.Location = New System.Drawing.Point(4, 4)
        Me.TP08.Name = "TP08"
        Me.TP08.Size = New System.Drawing.Size(1092, 75)
        Me.TP08.TabIndex = 7
        Me.TP08.Text = "8"
        Me.TP08.UseVisualStyleBackColor = True
        '
        'TP09
        '
        Me.TP09.CausesValidation = False
        Me.TP09.Location = New System.Drawing.Point(4, 4)
        Me.TP09.Name = "TP09"
        Me.TP09.Size = New System.Drawing.Size(1092, 75)
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
        Me.TabGeneral.Location = New System.Drawing.Point(2, 85)
        Me.TabGeneral.Margin = New System.Windows.Forms.Padding(0)
        Me.TabGeneral.Name = "TabGeneral"
        Me.TabGeneral.SelectedIndex = 0
        Me.TabGeneral.Size = New System.Drawing.Size(1184, 27)
        Me.TabGeneral.TabIndex = 0
        '
        'TabPage00
        '
        Me.TabPage00.CausesValidation = False
        Me.TabPage00.Location = New System.Drawing.Point(4, 4)
        Me.TabPage00.Name = "TabPage00"
        Me.TabPage00.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage00.Size = New System.Drawing.Size(1176, 1)
        Me.TabPage00.TabIndex = 0
        Me.TabPage00.Text = "usos_sigpac"
        Me.TabPage00.UseVisualStyleBackColor = True
        '
        'TabPage01
        '
        Me.TabPage01.CausesValidation = False
        Me.TabPage01.Location = New System.Drawing.Point(4, 4)
        Me.TabPage01.Name = "TabPage01"
        Me.TabPage01.Size = New System.Drawing.Size(1176, 756)
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
        Me.TabPage02.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage02.TabIndex = 1
        Me.TabPage02.Text = "2"
        Me.TabPage02.UseVisualStyleBackColor = True
        '
        'TabPage03
        '
        Me.TabPage03.CausesValidation = False
        Me.TabPage03.Location = New System.Drawing.Point(4, 4)
        Me.TabPage03.Name = "TabPage03"
        Me.TabPage03.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage03.TabIndex = 2
        Me.TabPage03.Text = "3"
        Me.TabPage03.UseVisualStyleBackColor = True
        '
        'TabPage04
        '
        Me.TabPage04.CausesValidation = False
        Me.TabPage04.Location = New System.Drawing.Point(4, 4)
        Me.TabPage04.Name = "TabPage04"
        Me.TabPage04.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage04.TabIndex = 3
        Me.TabPage04.Text = "4"
        Me.TabPage04.UseVisualStyleBackColor = True
        '
        'TabPage05
        '
        Me.TabPage05.CausesValidation = False
        Me.TabPage05.Location = New System.Drawing.Point(4, 4)
        Me.TabPage05.Name = "TabPage05"
        Me.TabPage05.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage05.TabIndex = 4
        Me.TabPage05.Text = "5"
        Me.TabPage05.UseVisualStyleBackColor = True
        '
        'TabPage06
        '
        Me.TabPage06.CausesValidation = False
        Me.TabPage06.Location = New System.Drawing.Point(4, 4)
        Me.TabPage06.Name = "TabPage06"
        Me.TabPage06.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage06.TabIndex = 5
        Me.TabPage06.Text = "6"
        Me.TabPage06.UseVisualStyleBackColor = True
        '
        'TabPage07
        '
        Me.TabPage07.CausesValidation = False
        Me.TabPage07.Location = New System.Drawing.Point(4, 4)
        Me.TabPage07.Name = "TabPage07"
        Me.TabPage07.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage07.TabIndex = 6
        Me.TabPage07.Text = "7"
        Me.TabPage07.UseVisualStyleBackColor = True
        '
        'TabPage08
        '
        Me.TabPage08.CausesValidation = False
        Me.TabPage08.Location = New System.Drawing.Point(4, 4)
        Me.TabPage08.Name = "TabPage08"
        Me.TabPage08.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage08.TabIndex = 7
        Me.TabPage08.Text = "8"
        Me.TabPage08.UseVisualStyleBackColor = True
        '
        'TabPage09
        '
        Me.TabPage09.CausesValidation = False
        Me.TabPage09.Location = New System.Drawing.Point(4, 4)
        Me.TabPage09.Name = "TabPage09"
        Me.TabPage09.Size = New System.Drawing.Size(1176, 756)
        Me.TabPage09.TabIndex = 8
        Me.TabPage09.Text = "9"
        Me.TabPage09.UseVisualStyleBackColor = True
        '
        'PanelInferior
        '
        Me.PanelInferior.BackColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.CausesValidation = False
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 178)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(1184, 28)
        Me.PanelInferior.TabIndex = 9999
        '
        'Frmusossigpac
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1184, 206)
        Me.Controls.Add(Me.PanelSuperior)
        Me.Controls.Add(Me.PanelCentral)
        Me.Controls.Add(Me.PanelInferior)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Frmusossigpac"
        Me.Text = "Usos SIGPAC"
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
Friend WithEvents CmdGrid001 As Button
Friend WithEvents TxtLookup0011 As TextBox
Friend WithEvents Lbl002 As Label
Friend WithEvents TxtDatos002 As TextBox
Friend WithEvents CnEdicion002 As CnEdicion.CnEdicion
Friend WithEvents Lbl003 As Label
Friend WithEvents TxtDatos003 As TextBox
Friend WithEvents CnEdicion003 As CnEdicion.CnEdicion
Friend WithEvents Lbl004 As Label
Friend WithEvents TxtDatos004 As TextBox
Friend WithEvents CnEdicion004 As CnEdicion.CnEdicion
End Class
