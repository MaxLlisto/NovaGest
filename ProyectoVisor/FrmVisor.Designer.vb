<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmVisor
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
        Dim FormatterSettings2 As Gnostice.Documents.FormatterSettings = New Gnostice.Documents.FormatterSettings()
        Dim SpreadSheetFormatterSettings2 As Gnostice.Documents.Spreadsheet.SpreadSheetFormatterSettings = New Gnostice.Documents.Spreadsheet.SpreadSheetFormatterSettings()
        Dim PageSettings3 As Gnostice.Documents.PageSettings = New Gnostice.Documents.PageSettings()
        Dim Margins3 As Gnostice.Documents.Margins = New Gnostice.Documents.Margins()
        Dim SheetOptions3 As Gnostice.Documents.Spreadsheet.SheetOptions = New Gnostice.Documents.Spreadsheet.SheetOptions()
        Dim SheetOptions4 As Gnostice.Documents.Spreadsheet.SheetOptions = New Gnostice.Documents.Spreadsheet.SheetOptions()
        Dim TxtFormatterSettings2 As Gnostice.Documents.TXTFormatterSettings = New Gnostice.Documents.TXTFormatterSettings()
        Dim PageSettings4 As Gnostice.Documents.PageSettings = New Gnostice.Documents.PageSettings()
        Dim Margins4 As Gnostice.Documents.Margins = New Gnostice.Documents.Margins()
        Dim RenderingSettings2 As Gnostice.Graphics.RenderingSettings = New Gnostice.Graphics.RenderingSettings()
        Dim ImageRenderingSettings2 As Gnostice.Graphics.ImageRenderingSettings = New Gnostice.Graphics.ImageRenderingSettings()
        Dim LineArtRenderingSettings2 As Gnostice.Graphics.LineArtRenderingSettings = New Gnostice.Graphics.LineArtRenderingSettings()
        Dim ResolutionSettings2 As Gnostice.Graphics.ResolutionSettings = New Gnostice.Graphics.ResolutionSettings()
        Dim TextRenderingSettings2 As Gnostice.Graphics.TextRenderingSettings = New Gnostice.Graphics.TextRenderingSettings()
        Me.dV1 = New Gnostice.Documents.Controls.WinForms.DocumentViewer()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.entrada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.factura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fechafactura = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.imprimir = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Visor = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Situacion = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Directorio = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.archivo = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.cbcodigo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pmenos = New System.Windows.Forms.Button()
        Me.pmas = New System.Windows.Forms.Button()
        Me.gderecha = New System.Windows.Forms.Button()
        Me.gizquierda = New System.Windows.Forms.Button()
        Me.pultima = New System.Windows.Forms.Button()
        Me.pprimera = New System.Windows.Forms.Button()
        Me.pimprimir = New System.Windows.Forms.Button()
        Me.psiguiente = New System.Windows.Forms.Button()
        Me.panterior = New System.Windows.Forms.Button()
        Me.Buscar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DesdeFecha = New System.Windows.Forms.DateTimePicker()
        Me.HastaFecha = New System.Windows.Forms.DateTimePicker()
        Me.cbEmpresa = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dV1
        '
        Me.dV1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dV1.AutoSize = True
        Me.dV1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.dV1.BorderWidth = 10
        Me.dV1.CurrentPage = 0
        Me.dV1.HScrollBar.LargeChange = 40
        Me.dV1.HScrollBar.SmallChange = 20
        Me.dV1.HScrollBar.Value = 0
        Me.dV1.HScrollBar.Visibility = Gnostice.Documents.Controls.WinForms.ScrollBarVisibility.Always
        Me.dV1.Location = New System.Drawing.Point(571, 142)
        Me.dV1.MouseMode = Gnostice.DOM.CursorPreferences.AreaSelection
        Me.dV1.Name = "dV1"
        '
        '
        '
        Me.dV1.NavigationPane.ActivePage = Nothing
        Me.dV1.NavigationPane.Location = New System.Drawing.Point(0, 0)
        Me.dV1.NavigationPane.Name = ""
        Me.dV1.NavigationPane.TabIndex = 0
        Me.dV1.NavigationPane.Visibility = Gnostice.Documents.Controls.WinForms.Visibility.[Auto]
        Me.dV1.NavigationPane.WidthPercentage = 20
        Me.dV1.PageLayout = Nothing
        Me.dV1.PageRotation = Gnostice.Documents.Controls.WinForms.RotationAngle.Zero
        Me.dV1.Preferences.Cursor = Gnostice.DOM.CursorPreferences.Pan
        SpreadSheetFormatterSettings2.FormattingMode = Gnostice.DOM.FormattingMode.PreferDocumentSettings
        SpreadSheetFormatterSettings2.PageOrder = Gnostice.Documents.Spreadsheet.LayoutDirection.BackwardN
        PageSettings3.Height = 11.6929!
        Margins3.Bottom = 1.0!
        Margins3.Footer = 0!
        Margins3.Header = 0!
        Margins3.Left = 1.0!
        Margins3.Right = 1.0!
        Margins3.Top = 1.0!
        PageSettings3.Margin = Margins3
        PageSettings3.Orientation = Gnostice.Graphics.Orientation.Portrait
        PageSettings3.PageSize = Gnostice.Documents.PageSize.A4
        PageSettings3.Width = 8.2677!
        SpreadSheetFormatterSettings2.PageSettings = PageSettings3
        SheetOptions3.Print = False
        SheetOptions3.View = False
        SpreadSheetFormatterSettings2.ShowGridlines = SheetOptions3
        SheetOptions4.Print = False
        SheetOptions4.View = False
        SpreadSheetFormatterSettings2.ShowHeadings = SheetOptions4
        FormatterSettings2.SpreadSheet = SpreadSheetFormatterSettings2
        TxtFormatterSettings2.Font = New System.Drawing.Font("Calibri", 12.0!)
        PageSettings4.Height = 11.6929!
        Margins4.Bottom = 1.0!
        Margins4.Footer = 0!
        Margins4.Header = 0!
        Margins4.Left = 1.0!
        Margins4.Right = 1.0!
        Margins4.Top = 1.0!
        PageSettings4.Margin = Margins4
        PageSettings4.Orientation = Gnostice.Graphics.Orientation.Portrait
        PageSettings4.PageSize = Gnostice.Documents.PageSize.A4
        PageSettings4.Width = 8.2677!
        TxtFormatterSettings2.PageSettings = PageSettings4
        FormatterSettings2.TXT = TxtFormatterSettings2
        Me.dV1.Preferences.FormatterSettings = FormatterSettings2
        Me.dV1.Preferences.KeyNavigation = True
        ImageRenderingSettings2.CompositingMode = Gnostice.Graphics.CompositingMode.SourceOver
        ImageRenderingSettings2.CompositingQuality = Gnostice.Graphics.CompositingQuality.[Default]
        ImageRenderingSettings2.InterpolationMode = Gnostice.Graphics.InterpolationMode.Bilinear
        ImageRenderingSettings2.PixelOffsetMode = Gnostice.Graphics.PixelOffsetMode.[Default]
        RenderingSettings2.Image = ImageRenderingSettings2
        LineArtRenderingSettings2.SmoothingMode = Gnostice.Graphics.SmoothingMode.AntiAlias
        RenderingSettings2.LineArt = LineArtRenderingSettings2
        ResolutionSettings2.DpiX = 96.0!
        ResolutionSettings2.DpiY = 96.0!
        ResolutionSettings2.ResolutionMode = Gnostice.Graphics.ResolutionMode.UseSource
        RenderingSettings2.Resolution = ResolutionSettings2
        TextRenderingSettings2.TextContrast = 3
        TextRenderingSettings2.TextRenderingHint = Gnostice.Graphics.TextRenderingHint.AntiAlias
        RenderingSettings2.Text = TextRenderingSettings2
        Me.dV1.Preferences.RenderingSettings = RenderingSettings2
        Me.dV1.Preferences.Units = Gnostice.Graphics.MeasurementUnit.Inches
        Me.dV1.Size = New System.Drawing.Size(581, 735)
        Me.dV1.TabIndex = 6
        Me.dV1.VScrollBar.LargeChange = 40
        Me.dV1.VScrollBar.SmallChange = 20
        Me.dV1.VScrollBar.Value = 0
        Me.dV1.VScrollBar.Visibility = Gnostice.Documents.Controls.WinForms.ScrollBarVisibility.Always
        Me.dV1.Zoom.ZoomMode = Gnostice.Documents.Controls.WinForms.ZoomMode.ActualSize
        Me.dV1.Zoom.ZoomPercent = 100.0R
        '
        'dgv
        '
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.ColumnHeadersHeight = 40
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.entrada, Me.Fecha, Me.factura, Me.fechafactura, Me.imprimir, Me.Visor, Me.Situacion, Me.Directorio, Me.archivo})
        Me.dgv.Location = New System.Drawing.Point(12, 142)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.Size = New System.Drawing.Size(552, 643)
        Me.dgv.TabIndex = 7
        '
        'entrada
        '
        Me.entrada.HeaderText = "Cód.entrada"
        Me.entrada.Name = "entrada"
        Me.entrada.ReadOnly = True
        Me.entrada.Width = 90
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        Me.Fecha.Width = 90
        '
        'factura
        '
        Me.factura.HeaderText = "Factura"
        Me.factura.Name = "factura"
        Me.factura.ReadOnly = True
        '
        'fechafactura
        '
        Me.fechafactura.HeaderText = "Fecha Fac."
        Me.fechafactura.Name = "fechafactura"
        Me.fechafactura.ReadOnly = True
        '
        'imprimir
        '
        Me.imprimir.HeaderText = "Imprimir"
        Me.imprimir.Name = "imprimir"
        Me.imprimir.ReadOnly = True
        Me.imprimir.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.imprimir.ToolTipText = "Imprimir"
        Me.imprimir.Width = 36
        '
        'Visor
        '
        Me.Visor.HeaderText = "Visor"
        Me.Visor.Name = "Visor"
        Me.Visor.ReadOnly = True
        Me.Visor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Visor.Width = 36
        '
        'Situacion
        '
        Me.Situacion.HeaderText = "Situación"
        Me.Situacion.Name = "Situacion"
        Me.Situacion.ReadOnly = True
        Me.Situacion.Width = 40
        '
        'Directorio
        '
        Me.Directorio.HeaderText = "Directorio"
        Me.Directorio.Name = "Directorio"
        Me.Directorio.ReadOnly = True
        Me.Directorio.Width = 150
        '
        'archivo
        '
        Me.archivo.HeaderText = "archivo"
        Me.archivo.Name = "archivo"
        Me.archivo.ReadOnly = True
        '
        'cbcodigo
        '
        Me.cbcodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbcodigo.FormattingEnabled = True
        Me.cbcodigo.Location = New System.Drawing.Point(12, 25)
        Me.cbcodigo.Name = "cbcodigo"
        Me.cbcodigo.Size = New System.Drawing.Size(502, 32)
        Me.cbcodigo.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.AutoSize = True
        Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GroupBox1.Controls.Add(Me.pmenos)
        Me.GroupBox1.Controls.Add(Me.pmas)
        Me.GroupBox1.Controls.Add(Me.gderecha)
        Me.GroupBox1.Controls.Add(Me.gizquierda)
        Me.GroupBox1.Controls.Add(Me.pultima)
        Me.GroupBox1.Controls.Add(Me.pprimera)
        Me.GroupBox1.Controls.Add(Me.pimprimir)
        Me.GroupBox1.Controls.Add(Me.psiguiente)
        Me.GroupBox1.Controls.Add(Me.panterior)
        Me.GroupBox1.Location = New System.Drawing.Point(592, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(475, 81)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'pmenos
        '
        Me.pmenos.AutoSize = True
        Me.pmenos.Image = Global.VisorDocumentos.My.Resources.Resource1.menos
        Me.pmenos.Location = New System.Drawing.Point(126, 18)
        Me.pmenos.Name = "pmenos"
        Me.pmenos.Size = New System.Drawing.Size(42, 43)
        Me.pmenos.TabIndex = 8
        Me.pmenos.UseVisualStyleBackColor = True
        '
        'pmas
        '
        Me.pmas.AutoSize = True
        Me.pmas.Image = Global.VisorDocumentos.My.Resources.Resource1.mas
        Me.pmas.Location = New System.Drawing.Point(78, 17)
        Me.pmas.Name = "pmas"
        Me.pmas.Size = New System.Drawing.Size(42, 43)
        Me.pmas.TabIndex = 7
        Me.pmas.UseVisualStyleBackColor = True
        '
        'gderecha
        '
        Me.gderecha.AutoSize = True
        Me.gderecha.Image = Global.VisorDocumentos.My.Resources.Resource1.giroder
        Me.gderecha.Location = New System.Drawing.Point(427, 19)
        Me.gderecha.Name = "gderecha"
        Me.gderecha.Size = New System.Drawing.Size(42, 41)
        Me.gderecha.TabIndex = 6
        Me.gderecha.UseVisualStyleBackColor = True
        '
        'gizquierda
        '
        Me.gizquierda.AutoSize = True
        Me.gizquierda.Image = Global.VisorDocumentos.My.Resources.Resource1.giroizq
        Me.gizquierda.Location = New System.Drawing.Point(385, 19)
        Me.gizquierda.Name = "gizquierda"
        Me.gizquierda.Size = New System.Drawing.Size(42, 41)
        Me.gizquierda.TabIndex = 5
        Me.gizquierda.UseVisualStyleBackColor = True
        '
        'pultima
        '
        Me.pultima.AutoSize = True
        Me.pultima.Image = Global.VisorDocumentos.My.Resources.Resource1.ultima
        Me.pultima.Location = New System.Drawing.Point(329, 19)
        Me.pultima.Name = "pultima"
        Me.pultima.Size = New System.Drawing.Size(42, 43)
        Me.pultima.TabIndex = 4
        Me.pultima.UseVisualStyleBackColor = True
        '
        'pprimera
        '
        Me.pprimera.AutoSize = True
        Me.pprimera.Image = Global.VisorDocumentos.My.Resources.Resource1.primera
        Me.pprimera.Location = New System.Drawing.Point(203, 19)
        Me.pprimera.Name = "pprimera"
        Me.pprimera.Size = New System.Drawing.Size(42, 43)
        Me.pprimera.TabIndex = 3
        Me.pprimera.UseVisualStyleBackColor = True
        '
        'pimprimir
        '
        Me.pimprimir.AutoSize = True
        Me.pimprimir.Image = Global.VisorDocumentos.My.Resources.Resource1.print32
        Me.pimprimir.Location = New System.Drawing.Point(12, 19)
        Me.pimprimir.Name = "pimprimir"
        Me.pimprimir.Size = New System.Drawing.Size(42, 41)
        Me.pimprimir.TabIndex = 2
        Me.pimprimir.UseVisualStyleBackColor = True
        '
        'psiguiente
        '
        Me.psiguiente.AutoSize = True
        Me.psiguiente.Image = Global.VisorDocumentos.My.Resources.Resource1.siguiente
        Me.psiguiente.Location = New System.Drawing.Point(287, 19)
        Me.psiguiente.Name = "psiguiente"
        Me.psiguiente.Size = New System.Drawing.Size(42, 41)
        Me.psiguiente.TabIndex = 1
        Me.psiguiente.UseVisualStyleBackColor = True
        '
        'panterior
        '
        Me.panterior.AutoSize = True
        Me.panterior.Image = Global.VisorDocumentos.My.Resources.Resource1.anterior
        Me.panterior.Location = New System.Drawing.Point(245, 19)
        Me.panterior.Name = "panterior"
        Me.panterior.Size = New System.Drawing.Size(42, 41)
        Me.panterior.TabIndex = 0
        Me.panterior.UseVisualStyleBackColor = True
        '
        'Buscar
        '
        Me.Buscar.AutoSize = True
        Me.Buscar.Image = Global.VisorDocumentos.My.Resources.Resource1.lupa
        Me.Buscar.Location = New System.Drawing.Point(517, 20)
        Me.Buscar.Name = "Buscar"
        Me.Buscar.Size = New System.Drawing.Size(48, 42)
        Me.Buscar.TabIndex = 9
        Me.Buscar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Desde la fecha"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(220, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Hasta la fecha"
        '
        'DesdeFecha
        '
        Me.DesdeFecha.Checked = False
        Me.DesdeFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DesdeFecha.Location = New System.Drawing.Point(97, 64)
        Me.DesdeFecha.Name = "DesdeFecha"
        Me.DesdeFecha.Size = New System.Drawing.Size(94, 20)
        Me.DesdeFecha.TabIndex = 13
        Me.DesdeFecha.Value = New Date(2020, 12, 9, 0, 0, 0, 0)
        '
        'HastaFecha
        '
        Me.HastaFecha.Checked = False
        Me.HastaFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.HastaFecha.Location = New System.Drawing.Point(302, 64)
        Me.HastaFecha.Name = "HastaFecha"
        Me.HastaFecha.Size = New System.Drawing.Size(94, 20)
        Me.HastaFecha.TabIndex = 14
        Me.HastaFecha.Value = New Date(2020, 12, 9, 0, 0, 0, 0)
        '
        'cbEmpresa
        '
        Me.cbEmpresa.FormattingEnabled = True
        Me.cbEmpresa.Location = New System.Drawing.Point(66, 100)
        Me.cbEmpresa.Name = "cbEmpresa"
        Me.cbEmpresa.Size = New System.Drawing.Size(269, 21)
        Me.cbEmpresa.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Empresa"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1154, 889)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbEmpresa)
        Me.Controls.Add(Me.HastaFecha)
        Me.Controls.Add(Me.DesdeFecha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Buscar)
        Me.Controls.Add(Me.cbcodigo)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.dV1)
        Me.Name = "Form1"
        Me.Text = "Visor de documentos"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents dV1 As Gnostice.Documents.Controls.WinForms.DocumentViewer
    Friend WithEvents dgv As DataGridView
    Friend WithEvents cbcodigo As ComboBox
    Friend WithEvents Buscar As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents gderecha As Button
    Friend WithEvents gizquierda As Button
    Friend WithEvents pultima As Button
    Friend WithEvents pprimera As Button
    Friend WithEvents pimprimir As Button
    Friend WithEvents psiguiente As Button
    Friend WithEvents panterior As Button
    Friend WithEvents pmenos As Button
    Friend WithEvents pmas As Button
    Friend WithEvents entrada As DataGridViewTextBoxColumn
    Friend WithEvents Fecha As DataGridViewTextBoxColumn
    Friend WithEvents factura As DataGridViewTextBoxColumn
    Friend WithEvents fechafactura As DataGridViewTextBoxColumn
    Friend WithEvents imprimir As DataGridViewImageColumn
    Friend WithEvents Visor As DataGridViewImageColumn
    Friend WithEvents Situacion As DataGridViewButtonColumn
    Friend WithEvents Directorio As DataGridViewButtonColumn
    Friend WithEvents archivo As DataGridViewButtonColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DesdeFecha As DateTimePicker
    Friend WithEvents HastaFecha As DateTimePicker
    Friend WithEvents cbEmpresa As ComboBox
    Friend WithEvents Label3 As Label
End Class
