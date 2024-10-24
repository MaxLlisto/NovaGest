'
' Created by SharpDevelop.
' User: Louis
' Date: 12/26/2016
' Time: 1:15 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class Designer
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Designer))
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox5 = New System.Windows.Forms.PictureBox()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.coordenadas = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.bCargar = New System.Windows.Forms.ToolStripButton()
        Me.bGuardar = New System.Windows.Forms.ToolStripButton()
        Me.bGuardarcomo = New System.Windows.Forms.ToolStripButton()
        Me.linea111 = New System.Windows.Forms.ToolStripSeparator()
        Me.bNuevo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.bAnterior = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.bsiguiente = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btexto = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.bfigura = New System.Windows.Forms.ToolStripButton()
        Me.bimagen = New System.Windows.Forms.ToolStripButton()
        Me.bcodigobarras = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.bcampo = New System.Windows.Forms.ToolStripButton()
        Me.bImagencampo = New System.Windows.Forms.ToolStripButton()
        Me.bcodigobarrast = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
        Me.bSalir = New System.Windows.Forms.ToolStripButton()
        Me.bacercade = New System.Windows.Forms.ToolStripButton()
        Me.bConfiguracion = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.bprevia = New System.Windows.Forms.ToolStripButton()
        Me.ComboBox_fuentes = New System.Windows.Forms.ToolStripComboBox()
        Me.Combox_Ftes_size = New System.Windows.Forms.ToolStripComboBox()
        Me.negrilla = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.SeleccionarTablas = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.BtScripts = New System.Windows.Forms.ToolStripButton()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.treeView1 = New System.Windows.Forms.TreeView()
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.propertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.label3 = New System.Windows.Forms.Label()
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.removeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.removeImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.duplicateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bringToFrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.sendToBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IzqToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArrToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeparadorMenu = New System.Windows.Forms.ToolStripSeparator()
        Me.SeparadorMenu1 = New System.Windows.Forms.ToolStripSeparator()
        Me.panel1.SuspendLayout()
        Me.panel2.SuspendLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel3.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.panel5.SuspendLayout()
        Me.contextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel1
        '
        Me.panel1.AutoScroll = True
        Me.panel1.AutoScrollMargin = New System.Drawing.Size(2, 0)
        Me.panel1.AutoSize = True
        Me.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.panel1.BackColor = System.Drawing.Color.LightGray
        Me.panel1.Controls.Add(Me.panel2)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel1.Location = New System.Drawing.Point(329, 40)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(762, 396)
        Me.panel1.TabIndex = 0
        '
        'panel2
        '
        Me.panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panel2.Controls.Add(Me.pictureBox3)
        Me.panel2.Controls.Add(Me.pictureBox4)
        Me.panel2.Controls.Add(Me.pictureBox2)
        Me.panel2.Controls.Add(Me.pictureBox1)
        Me.panel2.Controls.Add(Me.pictureBox5)
        Me.panel2.Location = New System.Drawing.Point(1, 1)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(726, 333)
        Me.panel2.TabIndex = 0
        '
        'pictureBox3
        '
        Me.pictureBox3.BackColor = System.Drawing.Color.White
        Me.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pictureBox3.Location = New System.Drawing.Point(35, 35)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(656, 263)
        Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox3.TabIndex = 2
        Me.pictureBox3.TabStop = False
        '
        'pictureBox4
        '
        Me.pictureBox4.BackColor = System.Drawing.Color.White
        Me.pictureBox4.Dock = System.Windows.Forms.DockStyle.Right
        Me.pictureBox4.Location = New System.Drawing.Point(691, 35)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(35, 263)
        Me.pictureBox4.TabIndex = 3
        Me.pictureBox4.TabStop = False
        '
        'pictureBox2
        '
        Me.pictureBox2.BackColor = System.Drawing.Color.White
        Me.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.pictureBox2.Location = New System.Drawing.Point(0, 35)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(35, 263)
        Me.pictureBox2.TabIndex = 1
        Me.pictureBox2.TabStop = False
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.White
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(726, 35)
        Me.pictureBox1.TabIndex = 0
        Me.pictureBox1.TabStop = False
        '
        'pictureBox5
        '
        Me.pictureBox5.BackColor = System.Drawing.Color.White
        Me.pictureBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pictureBox5.Location = New System.Drawing.Point(0, 298)
        Me.pictureBox5.Name = "pictureBox5"
        Me.pictureBox5.Size = New System.Drawing.Size(726, 35)
        Me.pictureBox5.TabIndex = 4
        Me.pictureBox5.TabStop = False
        '
        'panel3
        '
        Me.panel3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel3.Controls.Add(Me.coordenadas)
        Me.panel3.Controls.Add(Me.ToolStrip1)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel3.Location = New System.Drawing.Point(0, 0)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(1435, 40)
        Me.panel3.TabIndex = 1
        '
        'coordenadas
        '
        Me.coordenadas.AutoSize = True
        Me.coordenadas.Location = New System.Drawing.Point(1221, 8)
        Me.coordenadas.Name = "coordenadas"
        Me.coordenadas.Size = New System.Drawing.Size(22, 13)
        Me.coordenadas.TabIndex = 9
        Me.coordenadas.Text = "0,0"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bCargar, Me.bGuardar, Me.bGuardarcomo, Me.linea111, Me.bNuevo, Me.ToolStripSeparator1, Me.bAnterior, Me.ToolStripLabel1, Me.bsiguiente, Me.ToolStripLabel2, Me.ToolStripSeparator2, Me.btexto, Me.ToolStripLabel3, Me.bfigura, Me.bimagen, Me.bcodigobarras, Me.ToolStripSeparator3, Me.bcampo, Me.bImagencampo, Me.bcodigobarrast, Me.ToolStripLabel4, Me.ToolStripSeparator4, Me.ToolStripLabel5, Me.bSalir, Me.bacercade, Me.bConfiguracion, Me.ToolStripSeparator5, Me.bprevia, Me.ComboBox_fuentes, Me.Combox_Ftes_size, Me.negrilla, Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator6, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripButton8, Me.ToolStripButton9, Me.ToolStripSeparator7, Me.SeleccionarTablas, Me.ToolStripButton10, Me.ToolStripButton11, Me.BtScripts})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.MinimumSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1433, 36)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'bCargar
        '
        Me.bCargar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bCargar.Image = CType(resources.GetObject("bCargar.Image"), System.Drawing.Image)
        Me.bCargar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bCargar.Name = "bCargar"
        Me.bCargar.Size = New System.Drawing.Size(28, 33)
        Me.bCargar.Text = "Cargar diseño"
        Me.bCargar.ToolTipText = "Cargar diseño"
        '
        'bGuardar
        '
        Me.bGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bGuardar.Image = CType(resources.GetObject("bGuardar.Image"), System.Drawing.Image)
        Me.bGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bGuardar.Name = "bGuardar"
        Me.bGuardar.Size = New System.Drawing.Size(28, 33)
        Me.bGuardar.Text = "Guargar"
        '
        'bGuardarcomo
        '
        Me.bGuardarcomo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bGuardarcomo.Image = CType(resources.GetObject("bGuardarcomo.Image"), System.Drawing.Image)
        Me.bGuardarcomo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bGuardarcomo.Name = "bGuardarcomo"
        Me.bGuardarcomo.Size = New System.Drawing.Size(28, 33)
        Me.bGuardarcomo.Text = "Guardar cómo"
        '
        'linea111
        '
        Me.linea111.Name = "linea111"
        Me.linea111.Size = New System.Drawing.Size(6, 36)
        '
        'bNuevo
        '
        Me.bNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bNuevo.Image = CType(resources.GetObject("bNuevo.Image"), System.Drawing.Image)
        Me.bNuevo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bNuevo.Name = "bNuevo"
        Me.bNuevo.Size = New System.Drawing.Size(28, 33)
        Me.bNuevo.Text = "Nuevo diseño"
        Me.bNuevo.ToolTipText = "Nuevo diseño"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 36)
        '
        'bAnterior
        '
        Me.bAnterior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bAnterior.Image = CType(resources.GetObject("bAnterior.Image"), System.Drawing.Image)
        Me.bAnterior.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bAnterior.Name = "bAnterior"
        Me.bAnterior.Size = New System.Drawing.Size(28, 33)
        Me.bAnterior.Text = "Objeto anterior"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(19, 33)
        Me.ToolStripLabel1.Text = "    "
        '
        'bsiguiente
        '
        Me.bsiguiente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bsiguiente.Image = CType(resources.GetObject("bsiguiente.Image"), System.Drawing.Image)
        Me.bsiguiente.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bsiguiente.Name = "bsiguiente"
        Me.bsiguiente.Size = New System.Drawing.Size(28, 33)
        Me.bsiguiente.Text = "Objeto siguiente"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(22, 33)
        Me.ToolStripLabel2.Text = "     "
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 36)
        '
        'btexto
        '
        Me.btexto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btexto.Image = CType(resources.GetObject("btexto.Image"), System.Drawing.Image)
        Me.btexto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btexto.Name = "btexto"
        Me.btexto.Size = New System.Drawing.Size(28, 33)
        Me.btexto.Text = "Añadir etiqueta de texto"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(22, 33)
        Me.ToolStripLabel3.Text = "     "
        '
        'bfigura
        '
        Me.bfigura.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bfigura.Image = CType(resources.GetObject("bfigura.Image"), System.Drawing.Image)
        Me.bfigura.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bfigura.Name = "bfigura"
        Me.bfigura.Size = New System.Drawing.Size(28, 33)
        Me.bfigura.Text = "Añadir figura geométrica"
        '
        'bimagen
        '
        Me.bimagen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bimagen.Image = CType(resources.GetObject("bimagen.Image"), System.Drawing.Image)
        Me.bimagen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bimagen.Name = "bimagen"
        Me.bimagen.Size = New System.Drawing.Size(28, 33)
        Me.bimagen.Text = "Añadir imagen"
        '
        'bcodigobarras
        '
        Me.bcodigobarras.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bcodigobarras.Image = CType(resources.GetObject("bcodigobarras.Image"), System.Drawing.Image)
        Me.bcodigobarras.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bcodigobarras.Name = "bcodigobarras"
        Me.bcodigobarras.Size = New System.Drawing.Size(28, 33)
        Me.bcodigobarras.Text = "Añadir código de barras"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 36)
        '
        'bcampo
        '
        Me.bcampo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bcampo.Image = CType(resources.GetObject("bcampo.Image"), System.Drawing.Image)
        Me.bcampo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bcampo.Name = "bcampo"
        Me.bcampo.Size = New System.Drawing.Size(28, 33)
        Me.bcampo.Text = "Añadir campo de datos"
        '
        'bImagencampo
        '
        Me.bImagencampo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bImagencampo.Image = CType(resources.GetObject("bImagencampo.Image"), System.Drawing.Image)
        Me.bImagencampo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bImagencampo.Name = "bImagencampo"
        Me.bImagencampo.Size = New System.Drawing.Size(28, 33)
        Me.bImagencampo.Text = "Añadir imagen desde tabla"
        '
        'bcodigobarrast
        '
        Me.bcodigobarrast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bcodigobarrast.Image = CType(resources.GetObject("bcodigobarrast.Image"), System.Drawing.Image)
        Me.bcodigobarrast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bcodigobarrast.Name = "bcodigobarrast"
        Me.bcodigobarrast.Size = New System.Drawing.Size(28, 33)
        Me.bcodigobarrast.Text = "Añadir código de barras desde tabla"
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(19, 33)
        Me.ToolStripLabel4.Text = "    "
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripLabel5
        '
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        Me.ToolStripLabel5.Size = New System.Drawing.Size(19, 33)
        Me.ToolStripLabel5.Text = "    "
        '
        'bSalir
        '
        Me.bSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.bSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bSalir.Image = CType(resources.GetObject("bSalir.Image"), System.Drawing.Image)
        Me.bSalir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bSalir.Name = "bSalir"
        Me.bSalir.Size = New System.Drawing.Size(28, 33)
        Me.bSalir.Text = "Salir"
        '
        'bacercade
        '
        Me.bacercade.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.bacercade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bacercade.Image = CType(resources.GetObject("bacercade.Image"), System.Drawing.Image)
        Me.bacercade.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bacercade.Name = "bacercade"
        Me.bacercade.Size = New System.Drawing.Size(28, 33)
        Me.bacercade.Text = "Acerca de..."
        '
        'bConfiguracion
        '
        Me.bConfiguracion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.bConfiguracion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bConfiguracion.Image = CType(resources.GetObject("bConfiguracion.Image"), System.Drawing.Image)
        Me.bConfiguracion.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bConfiguracion.Name = "bConfiguracion"
        Me.bConfiguracion.Size = New System.Drawing.Size(28, 33)
        Me.bConfiguracion.Text = "Configuración"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 36)
        '
        'bprevia
        '
        Me.bprevia.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.bprevia.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.bprevia.Image = CType(resources.GetObject("bprevia.Image"), System.Drawing.Image)
        Me.bprevia.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.bprevia.Name = "bprevia"
        Me.bprevia.Size = New System.Drawing.Size(28, 33)
        Me.bprevia.Text = "Vista previa"
        '
        'ComboBox_fuentes
        '
        Me.ComboBox_fuentes.AutoSize = False
        Me.ComboBox_fuentes.BackColor = System.Drawing.Color.White
        Me.ComboBox_fuentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_fuentes.ForeColor = System.Drawing.SystemColors.InfoText
        Me.ComboBox_fuentes.Name = "ComboBox_fuentes"
        Me.ComboBox_fuentes.Size = New System.Drawing.Size(160, 23)
        '
        'Combox_Ftes_size
        '
        Me.Combox_Ftes_size.AutoToolTip = True
        Me.Combox_Ftes_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Combox_Ftes_size.DropDownWidth = 16
        Me.Combox_Ftes_size.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.Combox_Ftes_size.Name = "Combox_Ftes_size"
        Me.Combox_Ftes_size.Size = New System.Drawing.Size(75, 36)
        '
        'negrilla
        '
        Me.negrilla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.negrilla.Image = CType(resources.GetObject("negrilla.Image"), System.Drawing.Image)
        Me.negrilla.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.negrilla.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.negrilla.Name = "negrilla"
        Me.negrilla.Size = New System.Drawing.Size(23, 33)
        Me.negrilla.Text = "ToolStripButton1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 36)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton4.Text = "ToolStripButton4"
        Me.ToolStripButton4.ToolTipText = "Alinear a la izquierda"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton5.Text = "ToolStripButton5"
        Me.ToolStripButton5.ToolTipText = "Centrar texto"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(23, 33)
        Me.ToolStripButton6.Text = "ToolStripButton6"
        Me.ToolStripButton6.ToolTipText = "Alinear a la derecha"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(28, 33)
        Me.ToolStripButton7.Text = "ToolStripButton7"
        Me.ToolStripButton7.ToolTipText = "Alinear arriba"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(28, 33)
        Me.ToolStripButton8.Text = "ToolStripButton8"
        Me.ToolStripButton8.ToolTipText = "Alinear abajo"
        '
        'ToolStripButton9
        '
        Me.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), System.Drawing.Image)
        Me.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton9.Name = "ToolStripButton9"
        Me.ToolStripButton9.Size = New System.Drawing.Size(28, 33)
        Me.ToolStripButton9.Text = "ToolStripButton9"
        Me.ToolStripButton9.ToolTipText = "Alinear en el centro"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 36)
        '
        'SeleccionarTablas
        '
        Me.SeleccionarTablas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SeleccionarTablas.Image = CType(resources.GetObject("SeleccionarTablas.Image"), System.Drawing.Image)
        Me.SeleccionarTablas.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SeleccionarTablas.Name = "SeleccionarTablas"
        Me.SeleccionarTablas.Size = New System.Drawing.Size(28, 33)
        Me.SeleccionarTablas.Text = "Seleccionar tablas"
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"), System.Drawing.Image)
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(28, 33)
        Me.ToolStripButton10.Text = "ToolStripButton10"
        Me.ToolStripButton10.ToolTipText = "Seleccionar campos informe"
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = CType(resources.GetObject("ToolStripButton11.Image"), System.Drawing.Image)
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(28, 33)
        Me.ToolStripButton11.Text = "ToolStripButton11"
        Me.ToolStripButton11.ToolTipText = "variables"
        '
        'BtScripts
        '
        Me.BtScripts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtScripts.Image = CType(resources.GetObject("BtScripts.Image"), System.Drawing.Image)
        Me.BtScripts.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtScripts.Name = "BtScripts"
        Me.BtScripts.Size = New System.Drawing.Size(28, 33)
        Me.BtScripts.Text = "ToolStripButton12"
        '
        'panel4
        '
        Me.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel4.Controls.Add(Me.panel6)
        Me.panel4.Controls.Add(Me.treeView1)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Location = New System.Drawing.Point(0, 40)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(329, 432)
        Me.panel4.TabIndex = 2
        '
        'panel6
        '
        Me.panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.panel6.Location = New System.Drawing.Point(3, 161)
        Me.panel6.Name = "panel6"
        Me.panel6.Size = New System.Drawing.Size(319, 266)
        Me.panel6.TabIndex = 1
        '
        'treeView1
        '
        Me.treeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.treeView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeView1.ImageIndex = 0
        Me.treeView1.ImageList = Me.imageList1
        Me.treeView1.Location = New System.Drawing.Point(1, 3)
        Me.treeView1.Name = "treeView1"
        Me.treeView1.SelectedImageIndex = 0
        Me.treeView1.Size = New System.Drawing.Size(323, 152)
        Me.treeView1.TabIndex = 0
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.imageList1.Images.SetKeyName(0, "282101.png")
        Me.imageList1.Images.SetKeyName(1, "148820.png")
        Me.imageList1.Images.SetKeyName(2, "137035.png")
        Me.imageList1.Images.SetKeyName(3, "186302 - Copy.png")
        Me.imageList1.Images.SetKeyName(4, "148713.png")
        Me.imageList1.Images.SetKeyName(5, "Move up.png")
        Me.imageList1.Images.SetKeyName(6, "data-management.png")
        Me.imageList1.Images.SetKeyName(7, "personal-information.png")
        Me.imageList1.Images.SetKeyName(8, "image-files (1).png")
        Me.imageList1.Images.SetKeyName(9, "image-file.png")
        Me.imageList1.Images.SetKeyName(10, "shapes (1).png")
        Me.imageList1.Images.SetKeyName(11, "shapes.png")
        Me.imageList1.Images.SetKeyName(12, "image-files.png")
        Me.imageList1.Images.SetKeyName(13, "product-development.png")
        Me.imageList1.Images.SetKeyName(14, "database.png")
        Me.imageList1.Images.SetKeyName(15, "row.png")
        Me.imageList1.Images.SetKeyName(16, "codigo-de-barras.png")
        Me.imageList1.Images.SetKeyName(17, "codebar-field.png")
        '
        'panel5
        '
        Me.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel5.Controls.Add(Me.label1)
        Me.panel5.Controls.Add(Me.propertyGrid1)
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Right
        Me.panel5.Location = New System.Drawing.Point(1091, 40)
        Me.panel5.Name = "panel5"
        Me.panel5.Size = New System.Drawing.Size(344, 432)
        Me.panel5.TabIndex = 3
        '
        'label1
        '
        Me.label1.AutoEllipsis = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(4, 6)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(327, 28)
        Me.label1.TabIndex = 1
        Me.label1.Text = "label1g"
        '
        'propertyGrid1
        '
        Me.propertyGrid1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.propertyGrid1.Location = New System.Drawing.Point(5, 38)
        Me.propertyGrid1.Name = "propertyGrid1"
        Me.propertyGrid1.Size = New System.Drawing.Size(334, 357)
        Me.propertyGrid1.TabIndex = 0
        '
        'label3
        '
        Me.label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label3.Font = New System.Drawing.Font("Segoe UI Light", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.Green
        Me.label3.Location = New System.Drawing.Point(329, 436)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(762, 36)
        Me.label3.TabIndex = 4
        Me.label3.Text = "Grabando..."
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.label3.Visible = False
        '
        'timer1
        '
        Me.timer1.Interval = 1000
        '
        'timer2
        '
        '
        'contextMenuStrip1
        '
        Me.contextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.removeToolStripMenuItem, Me.removeImageToolStripMenuItem, Me.duplicateToolStripMenuItem, Me.bringToFrontToolStripMenuItem, Me.sendToBackToolStripMenuItem, Me.IzqToolStripMenuItem, Me.DerToolStripMenuItem, Me.ArrToolStripMenuItem, Me.AbaToolStripMenuItem, Me.HorToolStripMenuItem, Me.VerToolStripMenuItem})
        Me.contextMenuStrip1.Name = "contextMenuStrip1"
        Me.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.contextMenuStrip1.Size = New System.Drawing.Size(200, 268)
        '
        'removeToolStripMenuItem
        '
        Me.removeToolStripMenuItem.Image = CType(resources.GetObject("removeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.removeToolStripMenuItem.Name = "removeToolStripMenuItem"
        Me.removeToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.removeToolStripMenuItem.Text = "Borrar objeto"
        '
        'removeImageToolStripMenuItem
        '
        Me.removeImageToolStripMenuItem.Image = CType(resources.GetObject("removeImageToolStripMenuItem.Image"), System.Drawing.Image)
        Me.removeImageToolStripMenuItem.Name = "removeImageToolStripMenuItem"
        Me.removeImageToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.removeImageToolStripMenuItem.Text = "Borrar imagen"
        '
        'duplicateToolStripMenuItem
        '
        Me.duplicateToolStripMenuItem.Image = CType(resources.GetObject("duplicateToolStripMenuItem.Image"), System.Drawing.Image)
        Me.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem"
        Me.duplicateToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.duplicateToolStripMenuItem.Text = "Duplicar"
        '
        'bringToFrontToolStripMenuItem
        '
        Me.bringToFrontToolStripMenuItem.Image = CType(resources.GetObject("bringToFrontToolStripMenuItem.Image"), System.Drawing.Image)
        Me.bringToFrontToolStripMenuItem.Name = "bringToFrontToolStripMenuItem"
        Me.bringToFrontToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.bringToFrontToolStripMenuItem.Text = "Llevar al frente"
        '
        'sendToBackToolStripMenuItem
        '
        Me.sendToBackToolStripMenuItem.Image = CType(resources.GetObject("sendToBackToolStripMenuItem.Image"), System.Drawing.Image)
        Me.sendToBackToolStripMenuItem.Name = "sendToBackToolStripMenuItem"
        Me.sendToBackToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.sendToBackToolStripMenuItem.Text = "Llevar abajo"
        '
        'IzqToolStripMenuItem
        '
        Me.IzqToolStripMenuItem.Image = Global.ReportBuilder2.My.Resources.Resource1.Alinear_izquierda
        Me.IzqToolStripMenuItem.Name = "IzqToolStripMenuItem"
        Me.IzqToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.IzqToolStripMenuItem.Text = "Alinear izquierda"
        '
        'DerToolStripMenuItem
        '
        Me.DerToolStripMenuItem.Image = Global.ReportBuilder2.My.Resources.Resource1.Alinear_derecha
        Me.DerToolStripMenuItem.Name = "DerToolStripMenuItem"
        Me.DerToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.DerToolStripMenuItem.Text = "Alinear derecha"
        '
        'ArrToolStripMenuItem
        '
        Me.ArrToolStripMenuItem.Image = Global.ReportBuilder2.My.Resources.Resource1.Alinear_arriba
        Me.ArrToolStripMenuItem.Name = "ArrToolStripMenuItem"
        Me.ArrToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.ArrToolStripMenuItem.Text = "Alinear arriba"
        '
        'AbaToolStripMenuItem
        '
        Me.AbaToolStripMenuItem.Image = Global.ReportBuilder2.My.Resources.Resource1.Alinear_abajo
        Me.AbaToolStripMenuItem.Name = "AbaToolStripMenuItem"
        Me.AbaToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.AbaToolStripMenuItem.Text = "Alinear abajo"
        '
        'HorToolStripMenuItem
        '
        Me.HorToolStripMenuItem.Image = Global.ReportBuilder2.My.Resources.Resource1.Org_horizontal
        Me.HorToolStripMenuItem.Name = "HorToolStripMenuItem"
        Me.HorToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.HorToolStripMenuItem.Text = "Distribuir horizontal"
        '
        'VerToolStripMenuItem
        '
        Me.VerToolStripMenuItem.Image = Global.ReportBuilder2.My.Resources.Resource1.Org_vertical
        Me.VerToolStripMenuItem.Name = "VerToolStripMenuItem"
        Me.VerToolStripMenuItem.Size = New System.Drawing.Size(199, 24)
        Me.VerToolStripMenuItem.Text = "Distribuir vertical"
        '
        'SeparadorMenu
        '
        Me.SeparadorMenu.Name = "SeparadorMenu"
        Me.SeparadorMenu.Size = New System.Drawing.Size(6, 6)
        '
        'SeparadorMenu1
        '
        Me.SeparadorMenu1.Name = "SeparadorMenu1"
        Me.SeparadorMenu1.Size = New System.Drawing.Size(6, 6)
        '
        'Designer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1435, 472)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.panel5)
        Me.Controls.Add(Me.panel4)
        Me.Controls.Add(Me.panel3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Designer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Diseño de informes"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel1.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel5.ResumeLayout(False)
        Me.contextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents removeImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents sendToBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents bringToFrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents duplicateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents removeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents IzqToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents DerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ArrToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents AbaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents HorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents VerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private SeparadorMenu As System.Windows.Forms.ToolStripSeparator
    Private SeparadorMenu1 As System.Windows.Forms.ToolStripSeparator

    Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    ' Private contextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Public WithEvents timer2 As System.Windows.Forms.Timer
    Private WithEvents timer1 As System.Windows.Forms.Timer
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents imageList1 As System.Windows.Forms.ImageList
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Private WithEvents treeView1 As System.Windows.Forms.TreeView
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents propertyGrid1 As System.Windows.Forms.PropertyGrid
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents pictureBox4 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox5 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents bGuardar As ToolStripButton
    Friend WithEvents bCargar As ToolStripButton
    Friend WithEvents linea111 As ToolStripSeparator
    Friend WithEvents bNuevo As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents bAnterior As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents bsiguiente As ToolStripButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents btexto As ToolStripButton
    Friend WithEvents bfigura As ToolStripButton
    Friend WithEvents bimagen As ToolStripButton
    Friend WithEvents bcodigobarras As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents bcampo As ToolStripButton
    Friend WithEvents bImagencampo As ToolStripButton
    Friend WithEvents bcodigobarrast As ToolStripButton
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents bprevia As ToolStripButton
    Friend WithEvents bConfiguracion As ToolStripButton
    Friend WithEvents bacercade As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents bSalir As ToolStripButton
    Friend WithEvents coordenadas As Label
    Friend WithEvents ComboBox_fuentes As ToolStripComboBox
    Friend WithEvents negrilla As ToolStripButton
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents ToolStripButton6 As ToolStripButton
    Friend WithEvents ToolStripButton7 As ToolStripButton
    Friend WithEvents ToolStripButton8 As ToolStripButton
    Friend WithEvents ToolStripButton9 As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripButton10 As ToolStripButton
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents Combox_Ftes_size As ToolStripComboBox
    Friend WithEvents SeleccionarTablas As ToolStripButton
    Friend WithEvents bGuardarcomo As ToolStripButton
    Friend WithEvents BtScripts As ToolStripButton
End Class
