Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

namespace TwainGui
    Friend Class InfoForm
        Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
        Public Sub New(ByVal bmi As BITMAPINFOHEADER)
            InitializeComponent()
            txtWidth.Text = bmi.biWidth.ToString()
            txtHeight.Text = bmi.biHeight.ToString()
            txtBitsPix.Text = bmi.biBitCount.ToString()
      Dim skb As Integer = CInt(bmi.biSizeImage / 2 ^ 10)
            txtSize.Text = skb.ToString()
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private components As System.ComponentModel.Container = Nothing
        Private label1 As System.Windows.Forms.Label
        Private txtWidth As System.Windows.Forms.TextBox
        Private txtHeight As System.Windows.Forms.TextBox
        Private label2 As System.Windows.Forms.Label
        Private txtBitsPix As System.Windows.Forms.TextBox
        Private label3 As System.Windows.Forms.Label
        Private txtSize As System.Windows.Forms.TextBox
        Private label4 As System.Windows.Forms.Label
        Private btnOK As System.Windows.Forms.Button

        Private Sub InitializeComponent()
            Me.btnOK = New System.Windows.Forms.Button()
            Me.label4 = New System.Windows.Forms.Label()
            Me.txtWidth = New System.Windows.Forms.TextBox()
            Me.txtHeight = New System.Windows.Forms.TextBox()
            Me.txtSize = New System.Windows.Forms.TextBox()
            Me.txtBitsPix = New System.Windows.Forms.TextBox()
            Me.label1 = New System.Windows.Forms.Label()
            Me.label2 = New System.Windows.Forms.Label()
            Me.label3 = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'btnOK
            '
            Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.btnOK.Location = New System.Drawing.Point(104, 136)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(96, 24)
            Me.btnOK.TabIndex = 2
            Me.btnOK.Text = "Correcto"
            '
            'label4
            '
            Me.label4.Location = New System.Drawing.Point(8, 107)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(88, 16)
            Me.label4.TabIndex = 0
            Me.label4.Text = "Tamaño KB"
            '
            'txtWidth
            '
            Me.txtWidth.Location = New System.Drawing.Point(104, 8)
            Me.txtWidth.Name = "txtWidth"
            Me.txtWidth.ReadOnly = True
            Me.txtWidth.Size = New System.Drawing.Size(160, 20)
            Me.txtWidth.TabIndex = 1
            '
            'txtHeight
            '
            Me.txtHeight.Location = New System.Drawing.Point(104, 40)
            Me.txtHeight.Name = "txtHeight"
            Me.txtHeight.ReadOnly = True
            Me.txtHeight.Size = New System.Drawing.Size(160, 20)
            Me.txtHeight.TabIndex = 1
            '
            'txtSize
            '
            Me.txtSize.Location = New System.Drawing.Point(104, 104)
            Me.txtSize.Name = "txtSize"
            Me.txtSize.ReadOnly = True
            Me.txtSize.Size = New System.Drawing.Size(160, 20)
            Me.txtSize.TabIndex = 1
            '
            'txtBitsPix
            '
            Me.txtBitsPix.Location = New System.Drawing.Point(104, 72)
            Me.txtBitsPix.Name = "txtBitsPix"
            Me.txtBitsPix.ReadOnly = True
            Me.txtBitsPix.Size = New System.Drawing.Size(160, 20)
            Me.txtBitsPix.TabIndex = 1
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(8, 13)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(88, 16)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Ancho"
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(8, 45)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(88, 16)
            Me.label2.TabIndex = 0
            Me.label2.Text = "Alto"
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(8, 76)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(88, 16)
            Me.label3.TabIndex = 0
            Me.label3.Text = "Bits/Pixel"
            '
            'InfoForm
            '
            Me.AcceptButton = Me.btnOK
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(274, 175)
            Me.Controls.Add(Me.btnOK)
            Me.Controls.Add(Me.txtWidth)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.txtHeight)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.txtBitsPix)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.txtSize)
            Me.Controls.Add(Me.label4)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "InfoForm"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.Text = "Información"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

    End Class
end namespace
