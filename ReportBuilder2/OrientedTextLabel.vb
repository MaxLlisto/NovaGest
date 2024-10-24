Imports System
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Namespace CustomControl.OrientAbleTextControls
    Public Enum Orientation
        Circle
        Arc
        Rotate
    End Enum

    Public Enum Direction
        Clockwise
        AntiClockwise
    End Enum

    Public Class OrientedTextLabel
        Inherits System.Windows.Forms.Label

        Private rotAngle As Double
        Private texto As String
        Private textOrient As Orientation
        Private textDirect As Direction

        Public Sub New()
            rotAngle = 0R
            textOrient = Orientation.Rotate
            Me.Size = New Size(105, 12)
        End Sub

        <Description("Rotation Angle"), Category("Appearance")>
        Public Property RotationAngle As Double
            Get
                Return rotAngle
            End Get
            Set(ByVal value As Double)
                rotAngle = value
                Me.Invalidate()
            End Set
        End Property

        <Description("Kind of Text Orientation"), Category("Appearance")>
        Public Property TextOrientation As Orientation
            Get
                Return textOrient
            End Get
            Set(ByVal value As Orientation)
                textOrient = value
                Me.Invalidate()
            End Set
        End Property

        <Description("Direction of the Text"), Category("Appearance")>
        Public Property TextDirection As Direction
            Get
                Return textDirect
            End Get
            Set(ByVal value As Direction)
                textDirect = value
                Me.Invalidate()
            End Set
        End Property

        <Description("Display Text"), Category("Appearance")>
        Public Overrides Property Text As String
            Get
                Return texto
            End Get
            Set(ByVal value As String)
                texto = value
                Me.Invalidate()
            End Set
        End Property

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim graphics As Graphics = e.Graphics
            Dim stringFormat As StringFormat = New StringFormat()
            stringFormat.Alignment = StringAlignment.Center
            stringFormat.Trimming = StringTrimming.None
            Dim textBrush As Brush = New SolidBrush(Me.ForeColor)
            Dim width As Single = graphics.MeasureString(text, Me.Font).Width
            Dim height As Single = graphics.MeasureString(text, Me.Font).Height
            Dim radius As Single = 0F

            If ClientRectangle.Width < ClientRectangle.Height Then
                radius = ClientRectangle.Width * 0.9F / 2
            Else
                radius = ClientRectangle.Height * 0.9F / 2
            End If

            Select Case textOrientation
                Case Orientation.Arc
                    Dim arcAngle As Single = (2 * width / radius) / text.Length

                    If textDirection = Direction.Clockwise Then

                        For i As Integer = 0 To text.Length - 1
                            graphics.TranslateTransform(CSng((radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI)))), CSng((radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI)))))
                            graphics.RotateTransform((-90 + CSng(rotationAngle) + 180 * arcAngle * i / CSng(Math.PI)))
                            graphics.DrawString(text(i).ToString(), Me.Font, textBrush, 0, 0)
                            graphics.ResetTransform()
                        Next
                    Else

                        For i As Integer = 0 To text.Length - 1
                            graphics.TranslateTransform(CSng((radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI)))), CSng((radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI)))))
                            graphics.RotateTransform((-90 - CSng(rotationAngle) - 180 * arcAngle * i / CSng(Math.PI)))
                            graphics.DrawString(text(i).ToString(), Me.Font, textBrush, 0, 0)
                            graphics.ResetTransform()
                        Next
                    End If

                    Exit Select
                Case Orientation.Circle

                    If textDirection = Direction.Clockwise Then

                        For i As Integer = 0 To text.Length - 1
                            graphics.TranslateTransform(CSng((radius * (1 - Math.Cos((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI)))), CSng((radius * (1 - Math.Sin((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI)))))
                            graphics.RotateTransform(-90 + CSng(rotationAngle) + (360 / text.Length) * i)
                            graphics.DrawString(text(i).ToString(), Me.Font, textBrush, 0, 0)
                            graphics.ResetTransform()
                        Next
                    Else

                        For i As Integer = 0 To text.Length - 1
                            graphics.TranslateTransform(CSng((radius * (1 - Math.Cos((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI)))), CSng((radius * (1 + Math.Sin((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI)))))
                            graphics.RotateTransform(-90 - CSng(rotationAngle) - (360 / text.Length) * i)
                            graphics.DrawString(text(i).ToString(), Me.Font, textBrush, 0, 0)
                            graphics.ResetTransform()
                        Next
                    End If

                    Exit Select
                Case Orientation.Rotate
                    Dim angle As Double = (rotationAngle / 180) * Math.PI
                    graphics.TranslateTransform((ClientRectangle.Width + CSng((height * Math.Sin(angle))) - CSng((width * Math.Cos(angle)))) / 2, (ClientRectangle.Height - CSng((height * Math.Cos(angle))) - CSng((width * Math.Sin(angle)))) / 2)
                    graphics.RotateTransform(CSng(rotationAngle))
                    graphics.DrawString(text, Me.Font, textBrush, 0, 0)
                    graphics.ResetTransform()
                    Exit Select
            End Select
        End Sub
    End Class
End Namespace

'Private Sub btnDrawText_Click(ByVal sender As _
'    System.Object, ByVal e As System.EventArgs) Handles _
'    btnDrawText.Click
'    Static angle As Long    ' Angle in degrees.

'    ' Make a GraphicsPath that draws the text
'    ' at (150, 150).
'    Dim graphics_path As New _
'        GraphicsPath(Drawing.Drawing2D.FillMode.Winding)
'    graphics_path.AddString("Hello",
'        New FontFamily("Times New Roman"),
'        FontStyle.Bold, 40,
'        New Point(150, 150),
'        StringFormat.GenericDefault)

'    ' Make a rotation matrix representing 
'    ' rotation around the point (150, 150).
'    Dim rotation_matrix As New Matrix()
'    angle += 20
'    rotation_matrix.RotateAt(angle, New PointF(150, 150))

'    ' Transform the GraphicsPath.
'    graphics_path.Transform(rotation_matrix)

'    ' Draw the text.
'    With Me.CreateGraphics
'        .Clear(Me.BackColor)
'        .FillPath(Brushes.Black, graphics_path)
'    End With
'End Sub

'Private Sub Label1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Label1.Paint
'    Label1.Text = ""
'    e.Graphics.TranslateTransform(Label1.ClientSize.Width, Label1.ClientSize.Height)
'    e.Graphics.RotateTransform(180)
'    e.Graphics.DrawString("Hello", Label1.Font, Brushes.Black, RectangleF.op_Implicit(Label1.ClientRectangle))
'End Sub

