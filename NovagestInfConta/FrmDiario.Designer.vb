<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDiario
    Inherits libcomunes.FormGenerico

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDiario))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.SeleccionarAstos = New System.Windows.Forms.Button()
        Me.Numeración = New System.Windows.Forms.Label()
        Me.TxtNumeracion = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtOrdenacion = New System.Windows.Forms.ComboBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.LblDiariosseleccionados = New System.Windows.Forms.Label()
        Me.TxtHastadiario = New System.Windows.Forms.ComboBox()
        Me.TxtDesdediario = New System.Windows.Forms.ComboBox()
        Me.hasta = New System.Windows.Forms.Label()
        Me.Desde = New System.Windows.Forms.Label()
        Me.TxtSelecciondiarios = New System.Windows.Forms.ComboBox()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.BtExcel = New System.Windows.Forms.Button()
        Me.BtImprimir = New System.Windows.Forms.Button()
        Me.btVista = New System.Windows.Forms.Button()
        Me.BtPrevisualizar = New System.Windows.Forms.Button()
        Me.gbcont = New System.Windows.Forms.GroupBox()
        Me.Continuacion = New System.Windows.Forms.CheckBox()
        Me.TxtAcumuladohaber = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtAcumuladodebe = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtPaginaInicial = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtPrimerasiento = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtHastaPeriodo = New System.Windows.Forms.ComboBox()
        Me.TxtDesdePeriodo = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtHastaFechaAsiento = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDesdeFechaAsiento = New Textboxbotoncontrol.TextBoxCalendar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.gbcont.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(14, 194)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(616, 10)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.SeleccionarAstos)
        Me.GroupBox4.Controls.Add(Me.Numeración)
        Me.GroupBox4.Controls.Add(Me.TxtNumeracion)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.TxtOrdenacion)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 228)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(616, 63)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Ordenación"
        '
        'SeleccionarAstos
        '
        Me.SeleccionarAstos.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.empleados
        Me.SeleccionarAstos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SeleccionarAstos.Location = New System.Drawing.Point(541, 10)
        Me.SeleccionarAstos.Name = "SeleccionarAstos"
        Me.SeleccionarAstos.Size = New System.Drawing.Size(51, 47)
        Me.SeleccionarAstos.TabIndex = 7
        Me.SeleccionarAstos.UseVisualStyleBackColor = True
        '
        'Numeración
        '
        Me.Numeración.AutoSize = True
        Me.Numeración.Location = New System.Drawing.Point(282, 22)
        Me.Numeración.Name = "Numeración"
        Me.Numeración.Size = New System.Drawing.Size(64, 13)
        Me.Numeración.TabIndex = 8
        Me.Numeración.Text = "Numeración"
        '
        'TxtNumeracion
        '
        Me.TxtNumeracion.FormattingEnabled = True
        Me.TxtNumeracion.Location = New System.Drawing.Point(350, 19)
        Me.TxtNumeracion.Name = "TxtNumeracion"
        Me.TxtNumeracion.Size = New System.Drawing.Size(133, 21)
        Me.TxtNumeracion.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Ordenación"
        '
        'TxtOrdenacion
        '
        Me.TxtOrdenacion.FormattingEnabled = True
        Me.TxtOrdenacion.Location = New System.Drawing.Point(112, 19)
        Me.TxtOrdenacion.Name = "TxtOrdenacion"
        Me.TxtOrdenacion.Size = New System.Drawing.Size(133, 21)
        Me.TxtOrdenacion.TabIndex = 5
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.LblDiariosseleccionados)
        Me.GroupBox9.Controls.Add(Me.TxtHastadiario)
        Me.GroupBox9.Controls.Add(Me.TxtDesdediario)
        Me.GroupBox9.Controls.Add(Me.hasta)
        Me.GroupBox9.Controls.Add(Me.Desde)
        Me.GroupBox9.Controls.Add(Me.TxtSelecciondiarios)
        Me.GroupBox9.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(616, 56)
        Me.GroupBox9.TabIndex = 16
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Selección diarios"
        '
        'LblDiariosseleccionados
        '
        Me.LblDiariosseleccionados.AutoSize = True
        Me.LblDiariosseleccionados.Location = New System.Drawing.Point(177, 22)
        Me.LblDiariosseleccionados.Name = "LblDiariosseleccionados"
        Me.LblDiariosseleccionados.Size = New System.Drawing.Size(10, 13)
        Me.LblDiariosseleccionados.TabIndex = 5
        Me.LblDiariosseleccionados.Text = "-"
        '
        'TxtHastadiario
        '
        Me.TxtHastadiario.FormattingEnabled = True
        Me.TxtHastadiario.Location = New System.Drawing.Point(494, 19)
        Me.TxtHastadiario.Name = "TxtHastadiario"
        Me.TxtHastadiario.Size = New System.Drawing.Size(98, 21)
        Me.TxtHastadiario.TabIndex = 2
        Me.TxtHastadiario.Visible = False
        '
        'TxtDesdediario
        '
        Me.TxtDesdediario.FormattingEnabled = True
        Me.TxtDesdediario.Location = New System.Drawing.Point(300, 19)
        Me.TxtDesdediario.Name = "TxtDesdediario"
        Me.TxtDesdediario.Size = New System.Drawing.Size(98, 21)
        Me.TxtDesdediario.TabIndex = 1
        Me.TxtDesdediario.Visible = False
        '
        'hasta
        '
        Me.hasta.AutoSize = True
        Me.hasta.Location = New System.Drawing.Point(454, 23)
        Me.hasta.Name = "hasta"
        Me.hasta.Size = New System.Drawing.Size(35, 13)
        Me.hasta.TabIndex = 2
        Me.hasta.Text = "Hasta"
        Me.hasta.Visible = False
        '
        'Desde
        '
        Me.Desde.AutoSize = True
        Me.Desde.Location = New System.Drawing.Point(256, 20)
        Me.Desde.Name = "Desde"
        Me.Desde.Size = New System.Drawing.Size(38, 13)
        Me.Desde.TabIndex = 1
        Me.Desde.Text = "Desde"
        Me.Desde.Visible = False
        '
        'TxtSelecciondiarios
        '
        Me.TxtSelecciondiarios.FormattingEnabled = True
        Me.TxtSelecciondiarios.Location = New System.Drawing.Point(21, 20)
        Me.TxtSelecciondiarios.Name = "TxtSelecciondiarios"
        Me.TxtSelecciondiarios.Size = New System.Drawing.Size(150, 21)
        Me.TxtSelecciondiarios.TabIndex = 0
        '
        'BtSalir
        '
        Me.BtSalir.BackgroundImage = CType(resources.GetObject("BtSalir.BackgroundImage"), System.Drawing.Image)
        Me.BtSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSalir.Location = New System.Drawing.Point(580, 397)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(50, 50)
        Me.BtSalir.TabIndex = 15
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'BtExcel
        '
        Me.BtExcel.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.excel1
        Me.BtExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtExcel.Location = New System.Drawing.Point(514, 397)
        Me.BtExcel.Name = "BtExcel"
        Me.BtExcel.Size = New System.Drawing.Size(50, 50)
        Me.BtExcel.TabIndex = 14
        Me.BtExcel.UseVisualStyleBackColor = True
        '
        'BtImprimir
        '
        Me.BtImprimir.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.impresora_ico1
        Me.BtImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtImprimir.Location = New System.Drawing.Point(455, 397)
        Me.BtImprimir.Name = "BtImprimir"
        Me.BtImprimir.Size = New System.Drawing.Size(50, 50)
        Me.BtImprimir.TabIndex = 14
        Me.BtImprimir.UseVisualStyleBackColor = True
        '
        'btVista
        '
        Me.btVista.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.textview_view_search_find_110401
        Me.btVista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btVista.Location = New System.Drawing.Point(399, 397)
        Me.btVista.Name = "btVista"
        Me.btVista.Size = New System.Drawing.Size(50, 50)
        Me.btVista.TabIndex = 12
        Me.btVista.UseVisualStyleBackColor = True
        '
        'BtPrevisualizar
        '
        Me.BtPrevisualizar.BackgroundImage = Global.novagestinfconta.My.Resources.Resources.vprevia1
        Me.BtPrevisualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtPrevisualizar.Location = New System.Drawing.Point(338, 397)
        Me.BtPrevisualizar.Name = "BtPrevisualizar"
        Me.BtPrevisualizar.Size = New System.Drawing.Size(50, 50)
        Me.BtPrevisualizar.TabIndex = 13
        Me.BtPrevisualizar.UseVisualStyleBackColor = True
        '
        'gbcont
        '
        Me.gbcont.Controls.Add(Me.Continuacion)
        Me.gbcont.Controls.Add(Me.TxtAcumuladohaber)
        Me.gbcont.Controls.Add(Me.Label7)
        Me.gbcont.Controls.Add(Me.TxtAcumuladodebe)
        Me.gbcont.Controls.Add(Me.Label6)
        Me.gbcont.Controls.Add(Me.TxtPaginaInicial)
        Me.gbcont.Controls.Add(Me.Label5)
        Me.gbcont.Controls.Add(Me.TxtPrimerasiento)
        Me.gbcont.Controls.Add(Me.Label4)
        Me.gbcont.Location = New System.Drawing.Point(12, 297)
        Me.gbcont.Name = "gbcont"
        Me.gbcont.Size = New System.Drawing.Size(616, 94)
        Me.gbcont.TabIndex = 18
        Me.gbcont.TabStop = False
        Me.gbcont.Text = "Continuación"
        '
        'Continuacion
        '
        Me.Continuacion.AutoSize = True
        Me.Continuacion.Location = New System.Drawing.Point(78, 0)
        Me.Continuacion.Name = "Continuacion"
        Me.Continuacion.Size = New System.Drawing.Size(15, 14)
        Me.Continuacion.TabIndex = 8
        Me.Continuacion.UseVisualStyleBackColor = True
        '
        'TxtAcumuladohaber
        '
        Me.TxtAcumuladohaber.Location = New System.Drawing.Point(441, 52)
        Me.TxtAcumuladohaber.Name = "TxtAcumuladohaber"
        Me.TxtAcumuladohaber.Size = New System.Drawing.Size(101, 20)
        Me.TxtAcumuladohaber.TabIndex = 12
        Me.TxtAcumuladohaber.Text = "0.00"
        Me.TxtAcumuladohaber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(349, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Acumulado haber"
        '
        'TxtAcumuladodebe
        '
        Me.TxtAcumuladodebe.Location = New System.Drawing.Point(441, 26)
        Me.TxtAcumuladodebe.Name = "TxtAcumuladodebe"
        Me.TxtAcumuladodebe.Size = New System.Drawing.Size(101, 20)
        Me.TxtAcumuladodebe.TabIndex = 11
        Me.TxtAcumuladodebe.Text = "0.00"
        Me.TxtAcumuladodebe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(349, 29)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Acumulado debe"
        '
        'TxtPaginaInicial
        '
        Me.TxtPaginaInicial.Location = New System.Drawing.Point(112, 48)
        Me.TxtPaginaInicial.Name = "TxtPaginaInicial"
        Me.TxtPaginaInicial.Size = New System.Drawing.Size(54, 20)
        Me.TxtPaginaInicial.TabIndex = 10
        Me.TxtPaginaInicial.Text = "1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Página inicial"
        '
        'TxtPrimerasiento
        '
        Me.TxtPrimerasiento.Location = New System.Drawing.Point(112, 22)
        Me.TxtPrimerasiento.Name = "TxtPrimerasiento"
        Me.TxtPrimerasiento.Size = New System.Drawing.Size(100, 20)
        Me.TxtPrimerasiento.TabIndex = 9
        Me.TxtPrimerasiento.Text = "1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Primer asiento"
        '
        'TxtHastaPeriodo
        '
        Me.TxtHastaPeriodo.FormattingEnabled = True
        Me.TxtHastaPeriodo.Location = New System.Drawing.Point(417, 99)
        Me.TxtHastaPeriodo.Name = "TxtHastaPeriodo"
        Me.TxtHastaPeriodo.Size = New System.Drawing.Size(137, 21)
        Me.TxtHastaPeriodo.TabIndex = 4
        '
        'TxtDesdePeriodo
        '
        Me.TxtDesdePeriodo.FormattingEnabled = True
        Me.TxtDesdePeriodo.Location = New System.Drawing.Point(124, 99)
        Me.TxtDesdePeriodo.Name = "TxtDesdePeriodo"
        Me.TxtDesdePeriodo.Size = New System.Drawing.Size(133, 21)
        Me.TxtDesdePeriodo.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(322, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Hasta el periodo:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(30, 102)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Desde el periodo"
        '
        'TxtHastaFechaAsiento
        '
        Me.TxtHastaFechaAsiento.ButtonImage = Nothing
        Me.TxtHastaFechaAsiento.FormatoFecha = "dd/MM/yyyy"
        Me.TxtHastaFechaAsiento.Location = New System.Drawing.Point(373, 161)
        Me.TxtHastaFechaAsiento.Name = "TxtHastaFechaAsiento"
        Me.TxtHastaFechaAsiento.Size = New System.Drawing.Size(129, 20)
        Me.TxtHastaFechaAsiento.TabIndex = 27
        Me.TxtHastaFechaAsiento.Text = "13/03/2023"
        Me.TxtHastaFechaAsiento.value = New Date(2023, 3, 13, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(276, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Fecha balance"
        '
        'TxtDesdeFechaAsiento
        '
        Me.TxtDesdeFechaAsiento.ButtonImage = Nothing
        Me.TxtDesdeFechaAsiento.FormatoFecha = "dd/MM/yyyy"
        Me.TxtDesdeFechaAsiento.Location = New System.Drawing.Point(128, 161)
        Me.TxtDesdeFechaAsiento.Name = "TxtDesdeFechaAsiento"
        Me.TxtDesdeFechaAsiento.Size = New System.Drawing.Size(129, 20)
        Me.TxtDesdeFechaAsiento.TabIndex = 25
        Me.TxtDesdeFechaAsiento.Text = "13/03/2023"
        Me.TxtDesdeFechaAsiento.value = New Date(2023, 3, 13, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 164)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Desde la fecha"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(12, 136)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(616, 10)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        '
        'FrmDiario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 459)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TxtHastaFechaAsiento)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDesdeFechaAsiento)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtHastaPeriodo)
        Me.Controls.Add(Me.TxtDesdePeriodo)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.gbcont)
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtExcel)
        Me.Controls.Add(Me.BtImprimir)
        Me.Controls.Add(Me.btVista)
        Me.Controls.Add(Me.BtPrevisualizar)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "FrmDiario"
        Me.Text = "Libro de diario"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.gbcont.ResumeLayout(False)
        Me.gbcont.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents BtSalir As Button
    Friend WithEvents BtExcel As Button
    Friend WithEvents BtImprimir As Button
    Friend WithEvents btVista As Button
    Friend WithEvents BtPrevisualizar As Button
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents LblDiariosseleccionados As Label
    Friend WithEvents TxtHastadiario As ComboBox
    Friend WithEvents TxtDesdediario As ComboBox
    Friend WithEvents hasta As Label
    Friend WithEvents Desde As Label
    Friend WithEvents TxtSelecciondiarios As ComboBox
    Friend WithEvents Numeración As Label
    Friend WithEvents TxtNumeracion As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtOrdenacion As ComboBox
    Friend WithEvents SeleccionarAstos As Button
    Friend WithEvents gbcont As GroupBox
    Friend WithEvents TxtPrimerasiento As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtAcumuladohaber As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtAcumuladodebe As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtPaginaInicial As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Continuacion As CheckBox
    Friend WithEvents TxtHastaPeriodo As ComboBox
    Friend WithEvents TxtDesdePeriodo As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtHastaFechaAsiento As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtDesdeFechaAsiento As Textboxbotoncontrol.TextBoxCalendar
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
End Class
