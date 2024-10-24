Public Class frmSchema
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal ColorSchema As ColorRichTextBox.clsColorRichTextBoxSchema)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        m_Schema = ColorSchema
    End Sub

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
    Friend WithEvents lstKeywords As System.Windows.Forms.ListBox
    Friend WithEvents lblKeywords As System.Windows.Forms.Label
    Friend WithEvents cmdAddKeyword As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteKeyword As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdKeywordFont As System.Windows.Forms.Button
    Friend WithEvents lblKeywordFont As System.Windows.Forms.Label
    Friend WithEvents lblLineCommentFont As System.Windows.Forms.Label
    Friend WithEvents cmdLineCommentFont As System.Windows.Forms.Button
    Friend WithEvents lblStringFont As System.Windows.Forms.Label
    Friend WithEvents cmdStringFont As System.Windows.Forms.Button
    Friend WithEvents cmdNormalTextFont As System.Windows.Forms.Button
    Friend WithEvents lblNormalTextFont As System.Windows.Forms.Label
    Friend WithEvents cmdNormalTextColor As System.Windows.Forms.Button
    Friend WithEvents cmdStringColor As System.Windows.Forms.Button
    Friend WithEvents cmdLineCommentColor As System.Windows.Forms.Button
    Friend WithEvents cmdKeywordColor As System.Windows.Forms.Button
    Friend WithEvents lblNormalTextColor As System.Windows.Forms.Label
    Friend WithEvents lblStringColor As System.Windows.Forms.Label
    Friend WithEvents lblLineCommentColor As System.Windows.Forms.Label
    Friend WithEvents lblKeywordColor As System.Windows.Forms.Label
    Friend WithEvents chkCaseSensitive As System.Windows.Forms.CheckBox
    Friend WithEvents txtLineCommentString As System.Windows.Forms.TextBox
    Friend WithEvents lblLineCommentString As System.Windows.Forms.Label
    Friend WithEvents cmdSaveSchema As System.Windows.Forms.Button
    Friend WithEvents cmdLoadSchema As System.Windows.Forms.Button
    Friend WithEvents cmdVBNET As System.Windows.Forms.Button
    Friend WithEvents chkFormatKeyword As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstKeywords = New System.Windows.Forms.ListBox()
        Me.lblKeywords = New System.Windows.Forms.Label()
        Me.cmdAddKeyword = New System.Windows.Forms.Button()
        Me.cmdDeleteKeyword = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdKeywordFont = New System.Windows.Forms.Button()
        Me.lblKeywordFont = New System.Windows.Forms.Label()
        Me.lblLineCommentFont = New System.Windows.Forms.Label()
        Me.cmdLineCommentFont = New System.Windows.Forms.Button()
        Me.lblStringFont = New System.Windows.Forms.Label()
        Me.cmdStringFont = New System.Windows.Forms.Button()
        Me.lblNormalTextFont = New System.Windows.Forms.Label()
        Me.cmdNormalTextFont = New System.Windows.Forms.Button()
        Me.cmdNormalTextColor = New System.Windows.Forms.Button()
        Me.cmdStringColor = New System.Windows.Forms.Button()
        Me.cmdLineCommentColor = New System.Windows.Forms.Button()
        Me.cmdKeywordColor = New System.Windows.Forms.Button()
        Me.lblNormalTextColor = New System.Windows.Forms.Label()
        Me.lblStringColor = New System.Windows.Forms.Label()
        Me.lblLineCommentColor = New System.Windows.Forms.Label()
        Me.lblKeywordColor = New System.Windows.Forms.Label()
        Me.chkCaseSensitive = New System.Windows.Forms.CheckBox()
        Me.chkFormatKeyword = New System.Windows.Forms.CheckBox()
        Me.txtLineCommentString = New System.Windows.Forms.TextBox()
        Me.lblLineCommentString = New System.Windows.Forms.Label()
        Me.cmdSaveSchema = New System.Windows.Forms.Button()
        Me.cmdLoadSchema = New System.Windows.Forms.Button()
        Me.cmdVBNET = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstKeywords
        '
        Me.lstKeywords.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lstKeywords.ItemHeight = 16
        Me.lstKeywords.Location = New System.Drawing.Point(6, 24)
        Me.lstKeywords.Name = "lstKeywords"
        Me.lstKeywords.Size = New System.Drawing.Size(174, 356)
        Me.lstKeywords.TabIndex = 1
        '
        'lblKeywords
        '
        Me.lblKeywords.AutoSize = True
        Me.lblKeywords.Location = New System.Drawing.Point(6, 6)
        Me.lblKeywords.Name = "lblKeywords"
        Me.lblKeywords.Size = New System.Drawing.Size(80, 13)
        Me.lblKeywords.TabIndex = 0
        Me.lblKeywords.Text = "Palabras clave:"
        '
        'cmdAddKeyword
        '
        Me.cmdAddKeyword.Location = New System.Drawing.Point(6, 386)
        Me.cmdAddKeyword.Name = "cmdAddKeyword"
        Me.cmdAddKeyword.Size = New System.Drawing.Size(84, 23)
        Me.cmdAddKeyword.TabIndex = 2
        Me.cmdAddKeyword.Text = "&Añadir"
        '
        'cmdDeleteKeyword
        '
        Me.cmdDeleteKeyword.Location = New System.Drawing.Point(96, 386)
        Me.cmdDeleteKeyword.Name = "cmdDeleteKeyword"
        Me.cmdDeleteKeyword.Size = New System.Drawing.Size(84, 23)
        Me.cmdDeleteKeyword.TabIndex = 3
        Me.cmdDeleteKeyword.Text = "&Borrar"
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(554, 386)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(84, 23)
        Me.cmdClose.TabIndex = 27
        Me.cmdClose.Text = "Cerrar"
        '
        'cmdKeywordFont
        '
        Me.cmdKeywordFont.Location = New System.Drawing.Point(480, 24)
        Me.cmdKeywordFont.Name = "cmdKeywordFont"
        Me.cmdKeywordFont.Size = New System.Drawing.Size(158, 23)
        Me.cmdKeywordFont.TabIndex = 5
        Me.cmdKeywordFont.Text = "Claves Font"
        '
        'lblKeywordFont
        '
        Me.lblKeywordFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKeywordFont.Location = New System.Drawing.Point(188, 27)
        Me.lblKeywordFont.Name = "lblKeywordFont"
        Me.lblKeywordFont.Size = New System.Drawing.Size(280, 18)
        Me.lblKeywordFont.TabIndex = 4
        Me.lblKeywordFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLineCommentFont
        '
        Me.lblLineCommentFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLineCommentFont.Location = New System.Drawing.Point(188, 59)
        Me.lblLineCommentFont.Name = "lblLineCommentFont"
        Me.lblLineCommentFont.Size = New System.Drawing.Size(280, 18)
        Me.lblLineCommentFont.TabIndex = 6
        Me.lblLineCommentFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdLineCommentFont
        '
        Me.cmdLineCommentFont.Location = New System.Drawing.Point(480, 56)
        Me.cmdLineCommentFont.Name = "cmdLineCommentFont"
        Me.cmdLineCommentFont.Size = New System.Drawing.Size(158, 23)
        Me.cmdLineCommentFont.TabIndex = 7
        Me.cmdLineCommentFont.Text = "Líneas de comentario Font"
        '
        'lblStringFont
        '
        Me.lblStringFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStringFont.Location = New System.Drawing.Point(188, 91)
        Me.lblStringFont.Name = "lblStringFont"
        Me.lblStringFont.Size = New System.Drawing.Size(280, 18)
        Me.lblStringFont.TabIndex = 8
        Me.lblStringFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdStringFont
        '
        Me.cmdStringFont.Location = New System.Drawing.Point(480, 88)
        Me.cmdStringFont.Name = "cmdStringFont"
        Me.cmdStringFont.Size = New System.Drawing.Size(158, 23)
        Me.cmdStringFont.TabIndex = 9
        Me.cmdStringFont.Text = "Cadena Font"
        '
        'lblNormalTextFont
        '
        Me.lblNormalTextFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNormalTextFont.Location = New System.Drawing.Point(188, 123)
        Me.lblNormalTextFont.Name = "lblNormalTextFont"
        Me.lblNormalTextFont.Size = New System.Drawing.Size(280, 18)
        Me.lblNormalTextFont.TabIndex = 10
        Me.lblNormalTextFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdNormalTextFont
        '
        Me.cmdNormalTextFont.Location = New System.Drawing.Point(480, 120)
        Me.cmdNormalTextFont.Name = "cmdNormalTextFont"
        Me.cmdNormalTextFont.Size = New System.Drawing.Size(158, 23)
        Me.cmdNormalTextFont.TabIndex = 11
        Me.cmdNormalTextFont.Text = "Texto normal Font"
        '
        'cmdNormalTextColor
        '
        Me.cmdNormalTextColor.Location = New System.Drawing.Point(480, 248)
        Me.cmdNormalTextColor.Name = "cmdNormalTextColor"
        Me.cmdNormalTextColor.Size = New System.Drawing.Size(158, 23)
        Me.cmdNormalTextColor.TabIndex = 19
        Me.cmdNormalTextColor.Text = "Texto normal Color"
        '
        'cmdStringColor
        '
        Me.cmdStringColor.Location = New System.Drawing.Point(480, 216)
        Me.cmdStringColor.Name = "cmdStringColor"
        Me.cmdStringColor.Size = New System.Drawing.Size(158, 23)
        Me.cmdStringColor.TabIndex = 17
        Me.cmdStringColor.Text = "Cadena Color"
        '
        'cmdLineCommentColor
        '
        Me.cmdLineCommentColor.Location = New System.Drawing.Point(480, 184)
        Me.cmdLineCommentColor.Name = "cmdLineCommentColor"
        Me.cmdLineCommentColor.Size = New System.Drawing.Size(158, 23)
        Me.cmdLineCommentColor.TabIndex = 15
        Me.cmdLineCommentColor.Text = "Líneas de comentario Color"
        '
        'cmdKeywordColor
        '
        Me.cmdKeywordColor.Location = New System.Drawing.Point(480, 152)
        Me.cmdKeywordColor.Name = "cmdKeywordColor"
        Me.cmdKeywordColor.Size = New System.Drawing.Size(158, 23)
        Me.cmdKeywordColor.TabIndex = 13
        Me.cmdKeywordColor.Text = "Claves Color"
        '
        'lblNormalTextColor
        '
        Me.lblNormalTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNormalTextColor.Location = New System.Drawing.Point(188, 250)
        Me.lblNormalTextColor.Name = "lblNormalTextColor"
        Me.lblNormalTextColor.Size = New System.Drawing.Size(280, 18)
        Me.lblNormalTextColor.TabIndex = 18
        Me.lblNormalTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStringColor
        '
        Me.lblStringColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStringColor.Location = New System.Drawing.Point(188, 218)
        Me.lblStringColor.Name = "lblStringColor"
        Me.lblStringColor.Size = New System.Drawing.Size(280, 18)
        Me.lblStringColor.TabIndex = 16
        Me.lblStringColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLineCommentColor
        '
        Me.lblLineCommentColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLineCommentColor.Location = New System.Drawing.Point(188, 186)
        Me.lblLineCommentColor.Name = "lblLineCommentColor"
        Me.lblLineCommentColor.Size = New System.Drawing.Size(280, 18)
        Me.lblLineCommentColor.TabIndex = 14
        Me.lblLineCommentColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKeywordColor
        '
        Me.lblKeywordColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKeywordColor.Location = New System.Drawing.Point(188, 154)
        Me.lblKeywordColor.Name = "lblKeywordColor"
        Me.lblKeywordColor.Size = New System.Drawing.Size(280, 18)
        Me.lblKeywordColor.TabIndex = 12
        Me.lblKeywordColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkCaseSensitive
        '
        Me.chkCaseSensitive.Location = New System.Drawing.Point(188, 284)
        Me.chkCaseSensitive.Name = "chkCaseSensitive"
        Me.chkCaseSensitive.Size = New System.Drawing.Size(448, 24)
        Me.chkCaseSensitive.TabIndex = 20
        Me.chkCaseSensitive.Text = "Distinguir entre mayúsculas y minúsculas"
        '
        'chkFormatKeyword
        '
        Me.chkFormatKeyword.Location = New System.Drawing.Point(188, 322)
        Me.chkFormatKeyword.Name = "chkFormatKeyword"
        Me.chkFormatKeyword.Size = New System.Drawing.Size(448, 24)
        Me.chkFormatKeyword.TabIndex = 21
        Me.chkFormatKeyword.Text = "Formato clave"
        '
        'txtLineCommentString
        '
        Me.txtLineCommentString.Location = New System.Drawing.Point(188, 360)
        Me.txtLineCommentString.Name = "txtLineCommentString"
        Me.txtLineCommentString.Size = New System.Drawing.Size(74, 20)
        Me.txtLineCommentString.TabIndex = 22
        '
        'lblLineCommentString
        '
        Me.lblLineCommentString.AutoSize = True
        Me.lblLineCommentString.Location = New System.Drawing.Point(269, 364)
        Me.lblLineCommentString.Name = "lblLineCommentString"
        Me.lblLineCommentString.Size = New System.Drawing.Size(146, 13)
        Me.lblLineCommentString.TabIndex = 23
        Me.lblLineCommentString.Text = "Cadena línea de comentarios"
        '
        'cmdSaveSchema
        '
        Me.cmdSaveSchema.Location = New System.Drawing.Point(278, 386)
        Me.cmdSaveSchema.Name = "cmdSaveSchema"
        Me.cmdSaveSchema.Size = New System.Drawing.Size(84, 23)
        Me.cmdSaveSchema.TabIndex = 24
        Me.cmdSaveSchema.Text = "Guardar"
        '
        'cmdLoadSchema
        '
        Me.cmdLoadSchema.Location = New System.Drawing.Point(369, 386)
        Me.cmdLoadSchema.Name = "cmdLoadSchema"
        Me.cmdLoadSchema.Size = New System.Drawing.Size(84, 23)
        Me.cmdLoadSchema.TabIndex = 25
        Me.cmdLoadSchema.Text = "Cargar"
        '
        'cmdVBNET
        '
        Me.cmdVBNET.Location = New System.Drawing.Point(461, 386)
        Me.cmdVBNET.Name = "cmdVBNET"
        Me.cmdVBNET.Size = New System.Drawing.Size(84, 23)
        Me.cmdVBNET.TabIndex = 26
        Me.cmdVBNET.Text = "Defecto"
        '
        'frmSchema
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(644, 414)
        Me.Controls.Add(Me.cmdVBNET)
        Me.Controls.Add(Me.cmdLoadSchema)
        Me.Controls.Add(Me.cmdSaveSchema)
        Me.Controls.Add(Me.lblLineCommentString)
        Me.Controls.Add(Me.lblKeywords)
        Me.Controls.Add(Me.txtLineCommentString)
        Me.Controls.Add(Me.chkFormatKeyword)
        Me.Controls.Add(Me.chkCaseSensitive)
        Me.Controls.Add(Me.lblNormalTextColor)
        Me.Controls.Add(Me.lblStringColor)
        Me.Controls.Add(Me.lblLineCommentColor)
        Me.Controls.Add(Me.lblKeywordColor)
        Me.Controls.Add(Me.cmdNormalTextColor)
        Me.Controls.Add(Me.cmdStringColor)
        Me.Controls.Add(Me.cmdLineCommentColor)
        Me.Controls.Add(Me.cmdKeywordColor)
        Me.Controls.Add(Me.lblNormalTextFont)
        Me.Controls.Add(Me.cmdNormalTextFont)
        Me.Controls.Add(Me.lblStringFont)
        Me.Controls.Add(Me.cmdStringFont)
        Me.Controls.Add(Me.lblLineCommentFont)
        Me.Controls.Add(Me.cmdLineCommentFont)
        Me.Controls.Add(Me.lblKeywordFont)
        Me.Controls.Add(Me.cmdKeywordFont)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdDeleteKeyword)
        Me.Controls.Add(Me.cmdAddKeyword)
        Me.Controls.Add(Me.lstKeywords)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmSchema"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selección de patrones de colores para edición de scripts"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public m_Schema As ColorRichTextBox.clsColorRichTextBoxSchema

    Private Sub frmSchema_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSchema()
    End Sub

    Private Sub cmdAddKeyword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddKeyword.Click
        Dim sReturn As String = InputBox("Please, add new keyword", "New keyword")
        If Not sReturn = String.Empty Then
            lstKeywords.SelectedIndex = lstKeywords.Items.Add(sReturn)
            Dim iCount As Integer
            Dim aKeywords(lstKeywords.Items.Count - 1) As String
            For iCount = 0 To lstKeywords.Items.Count - 1
                aKeywords(iCount) = CType(lstKeywords.Items.Item(iCount), String)
            Next iCount
            m_Schema.Keywords = aKeywords
        End If
    End Sub

    Private Sub cmdDeleteKeyword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteKeyword.Click
        If Not lstKeywords.SelectedItem Is Nothing Then
            lstKeywords.Items.Remove(lstKeywords.SelectedItem)
            Dim iCount As Integer
            Dim aKeywords(lstKeywords.Items.Count - 1) As String
            For iCount = 0 To lstKeywords.Items.Count - 1
                aKeywords(iCount) = CType(lstKeywords.Items.Item(iCount), String)
            Next iCount
            m_Schema.Keywords = aKeywords
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        DialogResult = DialogResult.OK
        'Me.Close()
    End Sub

    Private Sub cmdFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKeywordFont.Click, cmdLineCommentFont.Click, cmdStringFont.Click, cmdNormalTextFont.Click
        Dim fd As New FontDialog()
        If fd.ShowDialog = DialogResult.OK Then
            Select Case CType(sender, Button).Name
                Case "cmdKeywordFont"
                    m_Schema.KeywordFont = fd.Font
                    lblKeywordFont.Font = fd.Font
                    lblKeywordFont.Text = fd.Font.Name
                Case "cmdLineCommentFont"
                    m_Schema.LineCommentFont = fd.Font
                    lblLineCommentFont.Font = fd.Font
                    lblLineCommentFont.Text = fd.Font.Name
                Case "cmdStringFont"
                    m_Schema.StringFont = fd.Font
                    lblStringFont.Font = fd.Font
                    lblStringFont.Text = fd.Font.Name
                Case "cmdNormalTextFont"
                    m_Schema.NormalFont = fd.Font
                    lblNormalTextFont.Font = fd.Font
                    lblNormalTextFont.Text = fd.Font.Name
            End Select
        End If
    End Sub

    Private Sub cmdColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKeywordColor.Click, cmdLineCommentColor.Click, cmdStringColor.Click, cmdNormalTextColor.Click
        Dim cd As New ColorDialog()
        If cd.ShowDialog = DialogResult.OK Then
            Select Case CType(sender, Button).Name
                Case "cmdKeywordColor"
                    m_Schema.KeywordColor = cd.Color
                    lblKeywordColor.BackColor = cd.Color
                    lblKeywordFont.ForeColor = cd.Color
                Case "cmdLineCommentColor"
                    m_Schema.LineCommentColor = cd.Color
                    lblLineCommentColor.BackColor = cd.Color
                    lblLineCommentFont.ForeColor = cd.Color
                Case "cmdStringColor"
                    m_Schema.StringColor = cd.Color
                    lblStringColor.BackColor = cd.Color
                    lblStringFont.ForeColor = cd.Color
                Case "cmdNormalTextColor"
                    m_Schema.NormalColor = cd.Color
                    lblNormalTextColor.BackColor = cd.Color
                    lblNormalTextFont.ForeColor = cd.Color
            End Select
        End If
    End Sub

    Private Sub chkCaseSensitive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCaseSensitive.CheckedChanged
        m_Schema.CaseSensitive = chkCaseSensitive.Checked
    End Sub

    Private Sub chkFormatKeyword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFormatKeyword.CheckedChanged
        m_Schema.FormatKeyword = chkFormatKeyword.Checked
    End Sub

    Private Sub txtLineCommentString_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLineCommentString.TextChanged
        If Not txtLineCommentString.Text = String.Empty Then
            m_Schema.LineComment = txtLineCommentString.Text
        Else
            txtLineCommentString.Text = m_Schema.LineComment
        End If
    End Sub

    Private Sub cmdSaveSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveSchema.Click
        Dim sfd As New SaveFileDialog()
        If sfd.ShowDialog = DialogResult.OK Then
            m_Schema.SaveSchema(sfd.FileName)
        End If
        DialogResult = DialogResult.OK
    End Sub

    Private Sub cmdLoadSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadSchema.Click
        Dim ofd As New OpenFileDialog()
        If ofd.ShowDialog = DialogResult.OK Then
            Try
                m_Schema = New ColorRichTextBox.clsColorRichTextBoxSchema(m_Schema.LoadSchema(ofd.FileName))
                LoadSchema()
            Catch ex As Exception
                MsgBox("Error while loading schema: " & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Sub

    Private Sub LoadSchema()
        lstKeywords.Items.Clear()
        Dim iCount As Integer
        For iCount = 0 To m_Schema.Keywords.Length - 1
            lstKeywords.Items.Add(m_Schema.Keywords(iCount))
        Next iCount
        If Not lstKeywords.Items.Count = 0 Then lstKeywords.SelectedIndex = 0

        lblKeywordFont.Font = m_Schema.KeywordFont
        lblKeywordFont.Text = m_Schema.KeywordFont.Name

        lblLineCommentFont.Font = m_Schema.LineCommentFont
        lblLineCommentFont.Text = m_Schema.LineCommentFont.Name

        lblStringFont.Font = m_Schema.StringFont
        lblStringFont.Text = m_Schema.StringFont.Name

        lblNormalTextFont.Font = m_Schema.NormalFont
        lblNormalTextFont.Text = m_Schema.NormalFont.Name

        lblKeywordColor.BackColor = m_Schema.KeywordColor
        lblKeywordFont.ForeColor = m_Schema.KeywordColor

        lblLineCommentColor.BackColor = m_Schema.LineCommentColor
        lblLineCommentFont.ForeColor = m_Schema.LineCommentColor

        lblStringColor.BackColor = m_Schema.StringColor
        lblStringFont.ForeColor = m_Schema.StringColor

        lblNormalTextColor.BackColor = m_Schema.NormalColor
        lblNormalTextFont.ForeColor = m_Schema.NormalColor

        chkCaseSensitive.Checked = m_Schema.CaseSensitive
        chkFormatKeyword.Checked = m_Schema.FormatKeyword
        txtLineCommentString.Text = m_Schema.LineComment
    End Sub

    Private Sub cmdVBNET_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVBNET.Click
        m_Schema = New ColorRichTextBox.clsColorRichTextBoxSchema(ColorRichTextBox.clsColorRichTextBoxSchema.enColorSchemaType.VBNET)
        LoadSchema()
    End Sub

End Class