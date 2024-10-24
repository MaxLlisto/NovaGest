Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Data
Imports System.IO
Imports System.Drawing.Text
Imports System.ComponentModel
Imports System.Windows.Documents

Public Class frmScrippts
    Inherits System.Windows.Forms.Form
    Dim Nosalir As Boolean = False
    Dim nocerrar = False
    Dim enedicion As Integer = 0

    Public ScriptsEventos() As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub


    Enum event_numero
        inicio_informe = 1
        Inicio_docmento = 2
        Fin_documento = 3
        Cabecera_inicial = 4
        Siguientes_cabeceras = 5
        Pie_final = 6
        Pies_parciales = 7
        Lineas_documento = 8
        Seccion = 9
    End Enum


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents rtbMain As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents cmdSchema As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScrippts))
        Me.rtbMain = New System.Windows.Forms.RichTextBox()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.cmdSchema = New System.Windows.Forms.Button()
        Me.cbSeleccionScript = New System.Windows.Forms.ComboBox()
        Me.BtCargar = New System.Windows.Forms.Button()
        Me.BtSalir = New System.Windows.Forms.Button()
        Me.BtGrabar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbMain
        '
        Me.rtbMain.AcceptsTab = True
        Me.rtbMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbMain.Location = New System.Drawing.Point(7, 46)
        Me.rtbMain.Name = "rtbMain"
        Me.rtbMain.Size = New System.Drawing.Size(833, 505)
        Me.rtbMain.TabIndex = 0
        Me.rtbMain.Text = ""
        Me.rtbMain.WordWrap = False
        '
        'lblTime
        '
        Me.lblTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTime.Location = New System.Drawing.Point(8, 581)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(154, 16)
        Me.lblTime.TabIndex = 1
        '
        'cmdSchema
        '
        Me.cmdSchema.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSchema.BackgroundImage = Global.ReportBuilder2.My.Resources.Resources.boton_color
        Me.cmdSchema.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdSchema.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSchema.Location = New System.Drawing.Point(374, 557)
        Me.cmdSchema.Name = "cmdSchema"
        Me.cmdSchema.Size = New System.Drawing.Size(59, 55)
        Me.cmdSchema.TabIndex = 5
        '
        'cbSeleccionScript
        '
        Me.cbSeleccionScript.FormattingEnabled = True
        Me.cbSeleccionScript.Location = New System.Drawing.Point(7, 12)
        Me.cbSeleccionScript.Name = "cbSeleccionScript"
        Me.cbSeleccionScript.Size = New System.Drawing.Size(774, 21)
        Me.cbSeleccionScript.TabIndex = 7
        '
        'BtCargar
        '
        Me.BtCargar.BackgroundImage = CType(resources.GetObject("BtCargar.BackgroundImage"), System.Drawing.Image)
        Me.BtCargar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtCargar.Location = New System.Drawing.Point(787, 4)
        Me.BtCargar.Name = "BtCargar"
        Me.BtCargar.Size = New System.Drawing.Size(33, 34)
        Me.BtCargar.TabIndex = 8
        Me.BtCargar.UseVisualStyleBackColor = True
        '
        'BtSalir
        '
        Me.BtSalir.BackgroundImage = CType(resources.GetObject("BtSalir.BackgroundImage"), System.Drawing.Image)
        Me.BtSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtSalir.Location = New System.Drawing.Point(782, 557)
        Me.BtSalir.Name = "BtSalir"
        Me.BtSalir.Size = New System.Drawing.Size(58, 55)
        Me.BtSalir.TabIndex = 9
        Me.BtSalir.UseVisualStyleBackColor = True
        '
        'BtGrabar
        '
        Me.BtGrabar.BackgroundImage = CType(resources.GetObject("BtGrabar.BackgroundImage"), System.Drawing.Image)
        Me.BtGrabar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtGrabar.Location = New System.Drawing.Point(21, 557)
        Me.BtGrabar.Name = "BtGrabar"
        Me.BtGrabar.Size = New System.Drawing.Size(57, 55)
        Me.BtGrabar.TabIndex = 10
        Me.BtGrabar.UseVisualStyleBackColor = True
        '
        'frmScrippts
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(848, 610)
        Me.Controls.Add(Me.BtGrabar)
        Me.Controls.Add(Me.BtSalir)
        Me.Controls.Add(Me.BtCargar)
        Me.Controls.Add(Me.cbSeleccionScript)
        Me.Controls.Add(Me.cmdSchema)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.rtbMain)
        Me.Name = "frmScrippts"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Editor de scripts"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private m_ColorRTB As ColorRichTextBox.clsColorRichTextBox

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As DataTable = New DataTable("Tabla")
        m_ColorRTB = New ColorRichTextBox.clsColorRichTextBox(rtbMain)


        dt = New DataTable("Tabla")

        dt.Columns.Add("Codigo")
        dt.Columns.Add("evento")

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Codigo") = event_numero.inicio_informe
        dr("evento") = "inicio informe"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Inicio_docmento
        dr("evento") = "Inicio docmento"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Fin_documento
        dr("evento") = "Fin documento"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Cabecera_inicial
        dr("evento") = "Cabecera inicial"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Siguientes_cabeceras
        dr("evento") = "Siguientes cabeceras"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Pie_final
        dr("evento") = "Pie final"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Pies_parciales
        dr("evento") = "Pies parciales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Codigo") = event_numero.Lineas_documento
        dr("evento") = "Líneas documento"
        dt.Rows.Add(dr)

        cbSeleccionScript.DataSource = dt
        cbSeleccionScript.ValueMember = "Codigo"
        cbSeleccionScript.DisplayMember = "evento"

        Nosalir = True

    End Sub

    Private Sub cmdLoadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = IO.Directory.GetCurrentDirectory
        ofd.Filter = "VB.NET files (*.vb)|*.vb"
        If ofd.ShowDialog = DialogResult.OK Then
            Dim sr As New IO.StreamReader(ofd.FileName)
            m_ColorRTB.ColorCode = False
            rtbMain.Text = sr.ReadToEnd
            m_ColorRTB.ColorCode = True
            Dim StartTime As Long = Now.Ticks
            m_ColorRTB.RecolorEntireText()
            Dim ts As New TimeSpan(Now.Ticks - StartTime)
            lblTime.Text = "Carga archivo: " & ts.TotalMilliseconds / 1000 & " sec."
            sr.Close()
        End If
    End Sub

    Private Sub cmdColorCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        m_ColorRTB.ColorCode = True
        Dim StartTime As Long = Now.Ticks
        m_ColorRTB.RecolorEntireText()
        Dim ts As New TimeSpan(Now.Ticks - StartTime)
        lblTime.Text = "Color Code: " & ts.TotalMilliseconds / 1000 & " sec."
    End Sub

    Private Sub cmdUncolor_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim StartTime As Long = Now.Ticks
        m_ColorRTB.ColorCode = False
        Dim ts As New TimeSpan(Now.Ticks - StartTime)
        lblTime.Text = "Stop Color: " & ts.TotalMilliseconds / 1000 & " sec."
    End Sub

    Private Sub cmdSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSchema.Click
        Dim SchemaDlg As New frmSchema(m_ColorRTB.ColorSchema)

        If SchemaDlg.ShowDialog() = DialogResult.OK Then
            m_ColorRTB.ColorSchema = New ColorRichTextBox.clsColorRichTextBoxSchema(SchemaDlg.m_Schema)
        End If
        nocerrar = True

    End Sub


    Friend WithEvents cbSeleccionScript As ComboBox
    Friend WithEvents BtCargar As Button
    Friend WithEvents BtSalir As Button
    Friend WithEvents BtGrabar As Button

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        nocerrar = False
        Me.Close()
    End Sub

    Private Sub BtCargar_Click(sender As Object, e As EventArgs) Handles BtCargar.Click

    End Sub

    Private Sub cbSeleccionScript_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSeleccionScript.SelectedIndexChanged

        'cbSeleccionScript.SelectedItem.ToString()
        'ScriptsEventos()
        If Nosalir Then
            If Not cbSeleccionScript.SelectedItem Is Nothing Then
                Select Case cbSeleccionScript.SelectedValue.ToString
                    Case "inicio_informe"
                        m_ColorRTB.ColorCode = False
                        If ScriptsEventos(1).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(1).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 1
                        'm_ColorRTB.ColorCode = True
                        'm_ColorRTB.RecolorEntireText()

                    Case "Inicio_docmento"
                        If ScriptsEventos(2).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable informe " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(2).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If

                        enedicion = 2
                    Case "Fin_documento"
                        If ScriptsEventos(3).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable informe " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(3).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 3

                    Case "Cabecera_inicial"
                        If ScriptsEventos(4).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(4).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 4

                    Case "Siguientes_cabeceras"
                        If ScriptsEventos(5).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(5).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 5

                    Case "Pie_final"
                        If ScriptsEventos(6).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(6).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 6

                    Case "Pies_parciales"
                        If ScriptsEventos(7).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(7).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 7

                    Case "Lineas_documento"
                        If ScriptsEventos(8).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'p(3) = MiDataRow de las líneas Ejemplo p(3).importe_debe " & vbCrLf &
                            "'p(4) = número de línea " & vbCrLf &
                            "'Ejemplo: p(2)!?TotalDebe = p(2)!?TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(8).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 8

                    Case "Seccion"
                        If ScriptsEventos(9).Trim = "" Then
                            rtbMain.Text = "' Empieza informe:" & vbCrLf & "'event_numero.inicio_informe " & vbCrLf &
                            "'p(0) = clase report " & vbCrLf &
                            "'p(1) = Datatable sección " & vbCrLf &
                            "'p(2) = clase variables " & vbCrLf &
                            "'Ejemplo: p(2)!TotalDebe = p(2)!TotalDebe + p(1)!Debe "
                        Else
                            m_ColorRTB.ColorCode = False
                            rtbMain.Text = ScriptsEventos(9).Trim
                            m_ColorRTB.ColorCode = True
                            Dim StartTime As Long = Now.Ticks
                            m_ColorRTB.RecolorEntireText()
                        End If
                        enedicion = 9

                End Select
            End If
        End If

    End Sub

    Private Sub frmScrippts_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If nocerrar Then
            e.Cancel = True
        End If
        nocerrar = False
    End Sub

    Private Sub BtGrabar_Click(sender As Object, e As EventArgs) Handles BtGrabar.Click
        If enedicion > 0 Then
            ScriptsEventos(enedicion) = rtbMain.Text
        End If
    End Sub

End Class
