<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frmramos

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmramos))
        Me.PanelSuperior = New System.Windows.Forms.Panel()
        Me.CnTabla01 = New CnTabla.CnTabla()
        Me.CmdSalir = New System.Windows.Forms.Button()
        Me.PanelCentral = New System.Windows.Forms.Panel()
        Me.TabCabecera = New System.Windows.Forms.TabControl()
        Me.TP00 = New System.Windows.Forms.TabPage()
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
        Me.CnTabla01.Tabla = "segur_ramos"
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
        Me.PanelCentral.Size = New System.Drawing.Size(1184, 45)
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
        Me.TabCabecera.Size = New System.Drawing.Size(1180, 41)
        Me.TabCabecera.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabCabecera.TabIndex = 9999
        '
        'TP00
        '
        Me.TP00.CausesValidation = False
        Me.TP00.Controls.Add(Me.CnEdicion002)
        Me.TP00.Controls.Add(Me.TxtDatos002)
        Me.TP00.Controls.Add(Me.Lbl002)
        Me.TP00.Controls.Add(Me.CnEdicion001)
        Me.TP00.Controls.Add(Me.TxtDatos001)
        Me.TP00.Controls.Add(Me.Lbl001)
        Me.TP00.Location = New System.Drawing.Point(4, 4)
        Me.TP00.Margin = New System.Windows.Forms.Padding(0)
        Me.TP00.Name = "TP00"
        Me.TP00.Size = New System.Drawing.Size(972, 33)
        Me.TP00.TabIndex = 1
        Me.TP00.Text = "segur_ramos"
        Me.TP00.UseVisualStyleBackColor = True
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
        Me.CnEdicion002.Campo = "descripcion"
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
        Me.CnEdicion002.Tabla = "segur_ramos"
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
        Me.Lbl002.Location = New System.Drawing.Point(428, 10)
        Me.Lbl002.Name = "Lbl002"
        Me.Lbl002.Size = New System.Drawing.Size(65, 13)
        Me.Lbl002.TabIndex = 9999
        Me.Lbl002.Text = "Descripción:"
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
        Me.CnEdicion001.Campo = "codigo_ramo"
        Me.CnEdicion001.CampoEnlacesLookup1 = Nothing
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
        Me.CnEdicion001.Tabla = "segur_ramos"
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
        Me.TxtDatos001.Size = New System.Drawing.Size(250, 21)
        Me.TxtDatos001.TabIndex = 9999
        '
        'Lbl001
        '
        Me.Lbl001.AutoSize = True
        Me.Lbl001.Location = New System.Drawing.Point(39, 10)
        Me.Lbl001.Name = "Lbl001"
        Me.Lbl001.Size = New System.Drawing.Size(74, 13)
        Me.Lbl001.TabIndex = 9999
        Me.Lbl001.Text = "Codigo Ramo:"
        '
        'TP01
        '
        Me.TP01.CausesValidation = False
        Me.TP01.Location = New System.Drawing.Point(4, 4)
        Me.TP01.Name = "TP01"
        Me.TP01.Size = New System.Drawing.Size(972, 33)
        Me.TP01.TabIndex = 9
        Me.TP01.Text = "1"
        Me.TP01.UseVisualStyleBackColor = True
        '
        'TP02
        '
        Me.TP02.CausesValidation = False
        Me.TP02.Location = New System.Drawing.Point(4, 4)
        Me.TP02.Name = "TP02"
        Me.TP02.Size = New System.Drawing.Size(972, 33)
        Me.TP02.TabIndex = 2
        Me.TP02.Text = "2"
        Me.TP02.UseVisualStyleBackColor = True
        '
        'TP03
        '
        Me.TP03.CausesValidation = False
        Me.TP03.Location = New System.Drawing.Point(4, 4)
        Me.TP03.Name = "TP03"
        Me.TP03.Size = New System.Drawing.Size(972, 33)
        Me.TP03.TabIndex = 10
        Me.TP03.Text = "3"
        Me.TP03.UseVisualStyleBackColor = True
        '
        'TP04
        '
        Me.TP04.CausesValidation = False
        Me.TP04.Location = New System.Drawing.Point(4, 4)
        Me.TP04.Name = "TP04"
        Me.TP04.Size = New System.Drawing.Size(972, 33)
        Me.TP04.TabIndex = 3
        Me.TP04.Text = "4"
        Me.TP04.UseVisualStyleBackColor = True
        '
        'TP05
        '
        Me.TP05.CausesValidation = False
        Me.TP05.Location = New System.Drawing.Point(4, 4)
        Me.TP05.Name = "TP05"
        Me.TP05.Size = New System.Drawing.Size(972, 33)
        Me.TP05.TabIndex = 4
        Me.TP05.Text = "5"
        Me.TP05.UseVisualStyleBackColor = True
        '
        'TP06
        '
        Me.TP06.CausesValidation = False
        Me.TP06.Location = New System.Drawing.Point(4, 4)
        Me.TP06.Name = "TP06"
        Me.TP06.Size = New System.Drawing.Size(972, 33)
        Me.TP06.TabIndex = 5
        Me.TP06.Text = "6"
        Me.TP06.UseVisualStyleBackColor = True
        '
        'TP07
        '
        Me.TP07.CausesValidation = False
        Me.TP07.Location = New System.Drawing.Point(4, 4)
        Me.TP07.Name = "TP07"
        Me.TP07.Size = New System.Drawing.Size(972, 33)
        Me.TP07.TabIndex = 6
        Me.TP07.Text = "7"
        Me.TP07.UseVisualStyleBackColor = True
        '
        'TP08
        '
        Me.TP08.CausesValidation = False
        Me.TP08.Location = New System.Drawing.Point(4, 4)
        Me.TP08.Name = "TP08"
        Me.TP08.Size = New System.Drawing.Size(972, 33)
        Me.TP08.TabIndex = 7
        Me.TP08.Text = "8"
        Me.TP08.UseVisualStyleBackColor = True
        '
        'TP09
        '
        Me.TP09.CausesValidation = False
        Me.TP09.Location = New System.Drawing.Point(4, 4)
        Me.TP09.Name = "TP09"
        Me.TP09.Size = New System.Drawing.Size(972, 33)
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
        Me.TabGeneral.Location = New System.Drawing.Point(2, 43)
        Me.TabGeneral.Margin = New System.Windows.Forms.Padding(0)
        Me.TabGeneral.Name = "TabGeneral"
        Me.TabGeneral.SelectedIndex = 0
        Me.TabGeneral.Size = New System.Drawing.Size(1184, 0)
        Me.TabGeneral.TabIndex = 0
        '
        'TabPage00
        '
        Me.TabPage00.CausesValidation = False
        Me.TabPage00.Location = New System.Drawing.Point(4, 4)
        Me.TabPage00.Name = "TabPage00"
        Me.TabPage00.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage00.Size = New System.Drawing.Size(1176, 0)
        Me.TabPage00.TabIndex = 0
        Me.TabPage00.Text = "segur_ramos"
        Me.TabPage00.UseVisualStyleBackColor = True
        '
        'TabPage01
        '
        Me.TabPage01.CausesValidation = False
        Me.TabPage01.Location = New System.Drawing.Point(4, 4)
        Me.TabPage01.Name = "TabPage01"
        Me.TabPage01.Size = New System.Drawing.Size(1176, 798)
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
        Me.TabPage02.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage02.TabIndex = 1
        Me.TabPage02.Text = "2"
        Me.TabPage02.UseVisualStyleBackColor = True
        '
        'TabPage03
        '
        Me.TabPage03.CausesValidation = False
        Me.TabPage03.Location = New System.Drawing.Point(4, 4)
        Me.TabPage03.Name = "TabPage03"
        Me.TabPage03.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage03.TabIndex = 2
        Me.TabPage03.Text = "3"
        Me.TabPage03.UseVisualStyleBackColor = True
        '
        'TabPage04
        '
        Me.TabPage04.CausesValidation = False
        Me.TabPage04.Location = New System.Drawing.Point(4, 4)
        Me.TabPage04.Name = "TabPage04"
        Me.TabPage04.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage04.TabIndex = 3
        Me.TabPage04.Text = "4"
        Me.TabPage04.UseVisualStyleBackColor = True
        '
        'TabPage05
        '
        Me.TabPage05.CausesValidation = False
        Me.TabPage05.Location = New System.Drawing.Point(4, 4)
        Me.TabPage05.Name = "TabPage05"
        Me.TabPage05.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage05.TabIndex = 4
        Me.TabPage05.Text = "5"
        Me.TabPage05.UseVisualStyleBackColor = True
        '
        'TabPage06
        '
        Me.TabPage06.CausesValidation = False
        Me.TabPage06.Location = New System.Drawing.Point(4, 4)
        Me.TabPage06.Name = "TabPage06"
        Me.TabPage06.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage06.TabIndex = 5
        Me.TabPage06.Text = "6"
        Me.TabPage06.UseVisualStyleBackColor = True
        '
        'TabPage07
        '
        Me.TabPage07.CausesValidation = False
        Me.TabPage07.Location = New System.Drawing.Point(4, 4)
        Me.TabPage07.Name = "TabPage07"
        Me.TabPage07.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage07.TabIndex = 6
        Me.TabPage07.Text = "7"
        Me.TabPage07.UseVisualStyleBackColor = True
        '
        'TabPage08
        '
        Me.TabPage08.CausesValidation = False
        Me.TabPage08.Location = New System.Drawing.Point(4, 4)
        Me.TabPage08.Name = "TabPage08"
        Me.TabPage08.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage08.TabIndex = 7
        Me.TabPage08.Text = "8"
        Me.TabPage08.UseVisualStyleBackColor = True
        '
        'TabPage09
        '
        Me.TabPage09.CausesValidation = False
        Me.TabPage09.Location = New System.Drawing.Point(4, 4)
        Me.TabPage09.Name = "TabPage09"
        Me.TabPage09.Size = New System.Drawing.Size(1176, 798)
        Me.TabPage09.TabIndex = 8
        Me.TabPage09.Text = "9"
        Me.TabPage09.UseVisualStyleBackColor = True
        '
        'PanelInferior
        '
        Me.PanelInferior.BackColor = System.Drawing.SystemColors.Control
        Me.PanelInferior.CausesValidation = False
        Me.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelInferior.Location = New System.Drawing.Point(0, 109)
        Me.PanelInferior.Name = "PanelInferior"
        Me.PanelInferior.Size = New System.Drawing.Size(1184, 28)
        Me.PanelInferior.TabIndex = 9999
        '
        'Frmramos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1184, 137)
        Me.Controls.Add(Me.PanelSuperior)
        Me.Controls.Add(Me.PanelCentral)
        Me.Controls.Add(Me.PanelInferior)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Frmramos"
        Me.Text = "Frmramos"
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
End Class
