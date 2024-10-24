Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Data
Imports System.IO
Imports System.Drawing.Text
Imports System.ComponentModel

Partial Public Class Designer
    Dim RulerThickness As Integer = 20
    Dim RulerColor As Color = New Color().FromArgb(250, 250, 250) 'New Color().FromArgb(0, 60, 119)

    Dim SepHeight As Integer = 18

    Dim optionPanel As New DragOptions
    Dim shapePanel As New DragShapes

    Private oldx, oldy As Integer
    Dim DragMarginWidth As Integer = 15
    Dim OldRec As New Rectangle(0, 0, 1, 1)
    Dim oldLocation As New Point(0, 0)
    Dim OldP As New Point(0, 0)
    Dim WaitTime As Integer = 0
    Dim ControlSelected As Integer = 1
    Dim ResType As Integer = 0
    Dim ur As Integer = 0
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim cn As System.Data.SqlClient.SqlConnection
    Dim SecSelec As String
    Dim ScriptFile As String = ""

    Dim actionDone As Boolean = False
    Public savePending As Boolean = True
    Dim EssDrag As Boolean = True
    Dim DatosInforme As DataTable = New DataTable
    Public dtDatos As DataTable
    Public dtTablas As DataTable
    Public dtVariables As DataTable
    Dim orPunto As Point
    Dim seleccionando As Boolean = False
    Dim pn As Pen = New Pen(Color.Black, 2)
    Dim gg As Graphics
    Dim pph As New Drawing2D.GraphicsPath(Drawing2D.FillMode.Alternate)
    Dim nis As SelectedItems = New SelectedItems()
    Dim p1 As Point
    Dim p2 As Point
    Dim Parar As Boolean
    Dim MaxItems As Integer
    Dim pd As Propiedadesdocumento = New Propiedadesdocumento
    Dim oFmto As Formatos = New Formatos
    Dim Proceso As String = ""
    Dim conversion As Boolean = False
    Public ScriptsEventos() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
    Public Norefrescar As Boolean = False
    Public nuevoproceso As Boolean = False
    Public nuevodocumento As Boolean = False
    Public Formatonuevo As Boolean = False



    Public Sub New()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        '
        ' TODO : Add constructor code after InitializeComponents
        '
        Cargarcombofuentes()

        'ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
        'If ObjetoGlobal.Inicializar("") = False Then
        '    MsgBox("No se puede abrir la conexión a la BD")
        'End If
        panel1.AutoScroll = True
        panel1.VerticalScroll.Visible = False Or panel1.HorizontalScroll.Visible = False
        Combox_Ftes_size.Text = "10"
        oFmto.ObjetoGlobal = ObjetoGlobal

        construirTablas()

    End Sub

    Sub PictureBox3Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles pictureBox3.Paint
        Dim g As Graphics = e.Graphics
        g.CompositingQuality = CompositingQuality.HighQuality
        Dim pn As New Pen(Color.Silver, 1)
        Dim r As New Rectangle(0, 0, pictureBox3.Width - 1, pictureBox3.Height - 1)
        'g.DrawRectangle(pn, r)
        Dim SF As New StringFormat
        SF.Alignment = StringAlignment.Near
        SF.LineAlignment = StringAlignment.Center
        Dim y As Integer = 0
        'dim R as Rectangle
        Dim secn As Section
        Dim sel As Boolean = False
        Dim selSec As Integer = -1
        Dim selRec As Rectangle

        'ObjetoGlobal.cn.GetSchema()

        If nis.Seleccionados.Count > 0 Then
            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)
                For Each itm As Item In nis.Seleccionados

                    Dim t As Item
                    t = itm
                    selRec = t.Rec
                    Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                    If t.bw < 1 Then
                        handPad = 0
                    End If

                    ' Pinta el contorno del objeto seleccionado
                    Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)

                    g.DrawLine(Sel_pn, selRec.X - handPad, 0, selRec.X - handPad, 1000)
                    g.DrawLine(Sel_pn, selRec.X + selRec.Width + handPad, 0, selRec.X + selRec.Width + handPad, 1000)
                    g.DrawLine(Sel_pn, 0, y + selRec.Y - handPad, RP.w, y + selRec.Y - handPad)
                    g.DrawLine(Sel_pn, 0, y + selRec.Y + selRec.Height + handPad, RP.w, y + selRec.Y + selRec.Height + handPad)

                    Dim b As New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard, Color.FromArgb(130, 0, 0, 255), Color.Transparent)
                    Dim pn2 As New Pen(b, 8)
                    Dim nRec As New Rectangle(t.Rec.X, t.Rec.Y + y, t.Rec.Width, t.Rec.Height)
                    Dim R2 As New Rectangle(nRec.X - 5 - handPad, nRec.Y - 5 - handPad, nRec.Width + 10 + 2 * handPad, nRec.Height + 10 + 2 * handPad)
                    'g.DrawRectangle(pn2, R2)

                    Dim HW, HH As Integer
                    Dim Hx, Hy As Integer
                    If nRec.Width - 30 < 10 Then
                        HW = 10
                    Else
                        HW = nRec.Width - 30
                    End If
                    If nRec.Height - 30 < 10 Then
                        HH = 10
                    Else
                        HH = nRec.Height - 30
                    End If
                    Hx = nRec.X + (nRec.Width - HW) / 2
                    Hy = nRec.Y + (nRec.Height - HH) / 2
                    Dim HL As New Pen(Color.SlateBlue, 1)
                    Dim HF As New SolidBrush(Color.FromArgb(215, Color.SlateBlue))
                    R2 = New Rectangle(Hx, nRec.Y + nRec.Height + handPad, HW, 10)
                    g.FillPath(HF, GetPath(R2, 5))
                    'g.DrawPath(HL, GetPath(r2, 5))

                    R2 = New Rectangle(nRec.X + nRec.Width + handPad, Hy, 10, HH)
                    g.FillPath(HF, GetPath(R2, 5))
                    'g.DrawPath(HL, GetPath(r2, 5))

                    R2 = New Rectangle(nRec.X + nRec.Width - 1 + handPad, nRec.Y + nRec.Height - 1 + handPad, 10, 10)
                    g.FillPath(HF, GetPath(R2, 5))
                    g.DrawPath(HL, GetPath(R2, 5))
                Next
                If secn.state < 3 Then
                    r = New Rectangle(40, y, pictureBox3.Width + 4, SepHeight)
                    g.DrawString(secn.headerLabel, New Font("Arial", 9), Brushes.Black, r, SF)
                    y += SepHeight
                End If

                r = New Rectangle(0, y, pictureBox3.Width, secn.h)
                If secn.state <> 2 Then
                    If secn.state = 1 Then
                        g.DrawImage(CreateSection(secn), r)
                    End If
                    y += secn.h
                End If
            Next

            y = 0

            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)
                Dim state As Integer = secn.state

                r = New Rectangle(0, y, pictureBox3.Width, secn.h)
                If secn.state = 3 Then
                    g.DrawImage(CreateSection(secn), r)
                End If

                If secn.state < 3 Then
                    r = New Rectangle(-12, y, pictureBox3.Width + 24, SepHeight - 1)

                    Dim linGrBrush As New LinearGradientBrush(New Point(0, y), New Point(0, y + SepHeight), Color.Silver, Color.Gray)
                    Dim path As GraphicsPath
                    path = GetRoundedRectPath(r, 1)
                    g.FillPath(linGrBrush, path)
                    g.DrawPath(New Pen(Color.Gray, 1), path)
                End If


                r = New Rectangle(2, y + 2, SepHeight - 4, SepHeight - 5)
                g.FillRectangle(New SolidBrush(Color.FromArgb(55, 0, 0, 0)), r)
                g.DrawRectangle(New Pen(Color.FromArgb(155, 0, 0, 0), 1), r)
                Dim btnPad As Integer = 2
                Select Case state
                    Case 1
                        r = New Rectangle(2 + btnPad, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                        g.DrawRectangle(New Pen(Color.Black, 1), r)
                    Case 2
                        r = New Rectangle(2 + btnPad, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                        g.DrawRectangle(New Pen(Color.Black, 1), r)
                    Case 3
                        r = New Rectangle(2 + btnPad + SepHeight, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, 3)
                        g.FillRectangle(Brushes.Black, r)
                        r = New Rectangle(2 + btnPad + SepHeight, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                        g.DrawRectangle(New Pen(Color.Black, 1), r)

                        r = New Rectangle(2 + 4 + SepHeight - 4, y + 2, SepHeight - 4, SepHeight - 5)
                        g.FillRectangle(New SolidBrush(Color.FromArgb(55, 0, 0, 0)), r)
                        g.DrawRectangle(New Pen(Color.FromArgb(155, 0, 0, 0), 1), r)

                        r = New Rectangle(4 + btnPad, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                        Dim path As GraphicsPath = New GraphicsPath
                        path.AddLine(New Point(r.X, r.Y), New Point(r.X + r.Width, r.Y + r.Height / 2))
                        path.AddLine(New Point(r.X + r.Width, r.Y + r.Height / 2), New Point(r.X, r.Y + r.Height))
                        path.AddLine(New Point(r.X, r.Y), New Point(r.X, r.Y + r.Height))

                        g.FillPath(Brushes.Black, path)



                End Select

                If secn.state < 3 Then
                    r = New Rectangle(40, y, pictureBox3.Width + 4, SepHeight)
                    g.DrawString(secn.headerLabel, New Font("Arial", 9), Brushes.Black, r, SF)
                    y += SepHeight
                End If

                r = New Rectangle(0, y, pictureBox3.Width, secn.h)
                If secn.state <> 2 Then
                    If secn.state = 1 Then
                        g.DrawImage(CreateSection(secn), r)
                    End If
                    y += secn.h
                End If

            Next

            panel2.Height = y + 2 * RulerThickness
            panel2.Width = RP.w + 2 * RulerThickness
            y = 0
            y += SepHeight
            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)
                If secn.selected Then
                    For Each itm As Item In nis.Seleccionados
                        'y = 0
                        'y += SepHeight
                        Dim t As Item
                        t = itm
                        Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                        If t.bw < 1 Then
                            handPad = 0
                        End If

                        ' Pinta el contorno del objeto seleccionado
                        Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)

                        g.DrawLine(Sel_pn, selRec.X - handPad, 0, selRec.X - handPad, 1000)
                        g.DrawLine(Sel_pn, selRec.X + selRec.Width + handPad, 0, selRec.X + selRec.Width + handPad, 1000)
                        g.DrawLine(Sel_pn, 0, y + selRec.Y - handPad, RP.w, y + selRec.Y - handPad)
                        g.DrawLine(Sel_pn, 0, y + selRec.Y + selRec.Height + handPad, RP.w, y + selRec.Y + selRec.Height + handPad)
                        Dim b As New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard, Color.FromArgb(130, 0, 0, 255), Color.Transparent)
                        Dim pn2 As New Pen(b, 8)
                        Dim nRec As New Rectangle(t.Rec.X, t.Rec.Y + y, t.Rec.Width, t.Rec.Height)
                        Dim R2 As New Rectangle(nRec.X - 5 - handPad, nRec.Y - 5 - handPad, nRec.Width + 10 + 2 * handPad, nRec.Height + 10 + 2 * handPad)


                        Dim HW, HH As Integer
                        Dim Hx, Hy As Integer
                        If nRec.Width - 30 < 10 Then
                            HW = 10
                        Else
                            HW = nRec.Width - 30
                        End If
                        If nRec.Height - 30 < 10 Then
                            HH = 10
                        Else
                            HH = nRec.Height - 30
                        End If
                        Hx = nRec.X + (nRec.Width - HW) / 2
                        Hy = nRec.Y + (nRec.Height - HH) / 2
                        Dim HL As New Pen(Color.SlateBlue, 1)
                        Dim HF As New SolidBrush(Color.FromArgb(215, Color.SlateBlue))
                        R2 = New Rectangle(Hx, nRec.Y + nRec.Height + handPad, HW, 10)
                        g.FillPath(HF, GetPath(R2, 5))
                        'g.DrawPath(HL, GetPath(r2, 5))

                        R2 = New Rectangle(nRec.X + nRec.Width + handPad, Hy, 10, HH)
                        g.FillPath(HF, GetPath(R2, 5))
                        'g.DrawPath(HL, GetPath(r2, 5))

                        R2 = New Rectangle(nRec.X + nRec.Width - 1 + handPad, nRec.Y + nRec.Height - 1 + handPad, 10, 10)
                        g.FillPath(HF, GetPath(R2, 5))
                        g.DrawPath(HL, GetPath(R2, 5))
                    Next
                End If
                If secn.state < 3 Then
                    y += SepHeight
                End If
                If secn.state <> 2 Then
                    y += secn.h
                End If
            Next
            Return
        End If

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            Dim state As Integer = secn.state

            r = New Rectangle(0, y, pictureBox3.Width, secn.h)
            If secn.state = 3 Then
                g.DrawImage(CreateSection(secn), r)
            End If

            If secn.state < 3 Then
                r = New Rectangle(-12, y, pictureBox3.Width + 24, SepHeight - 1)

                Dim linGrBrush As New LinearGradientBrush(New Point(0, y), New Point(0, y + SepHeight), Color.Silver, Color.Gray)
                Dim path As GraphicsPath
                path = GetRoundedRectPath(r, 1)
                g.FillPath(linGrBrush, path)
                g.DrawPath(New Pen(Color.Gray, 1), path)
            End If


            r = New Rectangle(2, y + 2, SepHeight - 4, SepHeight - 5)
            g.FillRectangle(New SolidBrush(Color.FromArgb(55, 0, 0, 0)), r)
            g.DrawRectangle(New Pen(Color.FromArgb(155, 0, 0, 0), 1), r)
            Dim btnPad As Integer = 2
            Select Case state
                Case 1
                    r = New Rectangle(2 + btnPad, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                    g.DrawRectangle(New Pen(Color.Black, 1), r)
                Case 2
                    r = New Rectangle(2 + btnPad, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                    g.DrawRectangle(New Pen(Color.Black, 1), r)
                Case 3
                    r = New Rectangle(2 + btnPad + SepHeight, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, 3)
                    g.FillRectangle(Brushes.Black, r)
                    r = New Rectangle(2 + btnPad + SepHeight, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                    g.DrawRectangle(New Pen(Color.Black, 1), r)

                    r = New Rectangle(2 + 4 + SepHeight - 4, y + 2, SepHeight - 4, SepHeight - 5)
                    g.FillRectangle(New SolidBrush(Color.FromArgb(55, 0, 0, 0)), r)
                    g.DrawRectangle(New Pen(Color.FromArgb(155, 0, 0, 0), 1), r)

                    r = New Rectangle(4 + btnPad, y + 2 + btnPad, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                    Dim path As GraphicsPath = New GraphicsPath
                    path.AddLine(New Point(r.X, r.Y), New Point(r.X + r.Width, r.Y + r.Height / 2))
                    path.AddLine(New Point(r.X + r.Width, r.Y + r.Height / 2), New Point(r.X, r.Y + r.Height))
                    path.AddLine(New Point(r.X, r.Y), New Point(r.X, r.Y + r.Height))

                    g.FillPath(Brushes.Black, path)
            End Select

            If secn.state < 3 Then
                r = New Rectangle(40, y, pictureBox3.Width + 4, SepHeight)
                g.DrawString(secn.headerLabel, New Font("Arial", 9), Brushes.Black, r, SF)
                y += SepHeight
            End If

            r = New Rectangle(0, y, pictureBox3.Width, secn.h)
            If secn.state <> 2 Then
                If secn.state = 1 Then
                    g.DrawImage(CreateSection(secn), r)
                End If
                y += secn.h
            End If
            For k As Integer = 0 To secn.AR.Count - 1
                Dim t As Item
                t = secn.AR(k)
                If t.selected Then
                    selRec = t.Rec
                    sel = True
                    selSec = i
                End If
            Next
        Next
        If sel Then
            'g.DrawLine(Sel_pn, selRec.X, 0, selRec.X, 1000)
            'g.DrawLine(Sel_pn, selRec.X + selRec.Width, 0, selRec.X + selRec.Width, 1000)
        End If
        panel2.Height = y + 2 * RulerThickness
        panel2.Width = RP.w + 2 * RulerThickness

        If sel Then
            y = 0
            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)

                If secn.state < 3 Then
                    y += SepHeight
                End If
                If secn.state <> 2 Then
                    If selSec = i Then
                        Dim t As Item
                        t = si
                        If t Is Nothing Then
                            Exit For
                        End If
                        Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                        If t.bw < 1 Then
                            handPad = 0
                        End If

                        ' Pinta el contorno del objeto seleccionado
                        Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)

                        g.DrawLine(Sel_pn, selRec.X - handPad, 0, selRec.X - handPad, 1000)
                        g.DrawLine(Sel_pn, selRec.X + selRec.Width + handPad, 0, selRec.X + selRec.Width + handPad, 1000)
                        g.DrawLine(Sel_pn, 0, y + selRec.Y - handPad, RP.w, y + selRec.Y - handPad)
                        g.DrawLine(Sel_pn, 0, y + selRec.Y + selRec.Height + handPad, RP.w, y + selRec.Y + selRec.Height + handPad)
                        Dim b As New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard, Color.FromArgb(130, 0, 0, 255), Color.Transparent)
                        Dim pn2 As New Pen(b, 8)
                        Dim nRec As New Rectangle(t.Rec.X, t.Rec.Y + y, t.Rec.Width, t.Rec.Height)
                        Dim R2 As New Rectangle(nRec.X - 5 - handPad, nRec.Y - 5 - handPad, nRec.Width + 10 + 2 * handPad, nRec.Height + 10 + 2 * handPad)
                        'g.DrawRectangle(pn2, R2)

                        Dim HW, HH As Integer
                        Dim Hx, Hy As Integer
                        If nRec.Width - 30 < 10 Then
                            HW = 10
                        Else
                            HW = nRec.Width - 30
                        End If
                        If nRec.Height - 30 < 10 Then
                            HH = 10
                        Else
                            HH = nRec.Height - 30
                        End If
                        Hx = nRec.X + (nRec.Width - HW) / 2
                        Hy = nRec.Y + (nRec.Height - HH) / 2
                        Dim HL As New Pen(Color.SlateBlue, 1)
                        Dim HF As New SolidBrush(Color.FromArgb(215, Color.SlateBlue))
                        R2 = New Rectangle(Hx, nRec.Y + nRec.Height + handPad, HW, 10)
                        g.FillPath(HF, GetPath(R2, 5))
                        'g.DrawPath(HL, GetPath(r2, 5))

                        R2 = New Rectangle(nRec.X + nRec.Width + handPad, Hy, 10, HH)
                        g.FillPath(HF, GetPath(R2, 5))
                        'g.DrawPath(HL, GetPath(r2, 5))

                        R2 = New Rectangle(nRec.X + nRec.Width - 1 + handPad, nRec.Y + nRec.Height - 1 + handPad, 10, 10)
                        g.FillPath(HF, GetPath(R2, 5))
                        g.DrawPath(HL, GetPath(R2, 5))
                    End If
                    y += secn.h
                End If
            Next
        End If
        'End If

        If nis.visible Then
            nis.Draw()
        End If

        'ShowTree()
    End Sub

    Sub ShowOptions(Optional ByVal tools As Boolean = True)
        shapePanel.Visible = False
        optionPanel.Visible = True
        optionPanel.Show(tools)
        If tools Then
            optionPanel.label4.Text = si.nm
        End If
    End Sub

    Sub HideOptions(Optional ByVal tools As Boolean = True)
        shapePanel.Visible = True
        optionPanel.Visible = False
        shapePanel.shapes(tools)
    End Sub

    Sub PictureBox1Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles pictureBox1.Paint
        Dim g As Graphics = e.Graphics
        Dim pn As New Pen(Color.FromArgb(0, 0, 0), 1)
        Dim x As Integer = RulerThickness + 0
        Dim cnt As Integer = 0
        Dim r As Rectangle
        Dim SF As New StringFormat
        Dim drawBrush As New SolidBrush(Color.FromArgb(0, 0, 0))

        SF.Alignment = StringAlignment.Center
        Dim UFont As New Font("arial", 6)
        '8.25inches = 21cm
        '825 = 2100
        r = New Rectangle(1, 1, 20, 10)
        If unit = 40 Then
            g.DrawString("cm", UFont, drawBrush, r)
        Else
            g.DrawString("Inch", UFont, drawBrush, r)
        End If


        Dim bin As Integer = 0
        For i As Integer = 0 To panel2.Width + 200
            If unit = 40 Then
                If i Mod 4 = 0 Then
                    If cnt Mod 10 = 0 Then
                        'If bin Mod 2 = 0 Then
                        r = New Rectangle(x - 8, 8, 16, 10)
                        'g.DrawString(bin, UFont, Brushes.Black, r, SF)
                        g.DrawString(bin, UFont, drawBrush, r, SF)
                        'End If
                        g.DrawLine(pn, x, 0, x, 8)
                        bin += 1
                    ElseIf cnt Mod 5 = 0 And Not (cnt Mod 10 = 0) Then
                        g.DrawLine(pn, x, 0, x, 7)
                    Else
                        g.DrawLine(pn, x, 0, x, 4)
                    End If
                    cnt += 1
                    x += 4
                End If
            Else
                If i Mod 10 = 0 Then
                    If cnt Mod 5 = 0 Then
                        g.DrawLine(pn, x, 0, x, 8)
                        If i Mod 20 = 0 Then
                            r = New Rectangle(x - 8, 8, 16, 10)
                            g.DrawString(bin, UFont, Brushes.Black, r, SF)
                            g.DrawLine(pn, x, 0, x, 8)
                            bin += 1
                        End If
                    Else
                        g.DrawLine(pn, x, 0, x, 4)
                    End If
                    cnt += 1
                    x += 10
                End If
            End If
        Next
    End Sub

    Sub PictureBox2Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles pictureBox2.Paint
        Dim g As Graphics = e.Graphics
        'Dim pn As New Pen(Color.Black, 1)
        'Dim x As Integer = RulerThickness + 1 + SepHeight
        Dim cnt As Integer = 0
        Dim r As Rectangle
        Dim SF As New StringFormat
        SF.Alignment = StringAlignment.Center
        Dim UFont As New Font("arial", 6)
        Dim sh As Integer = 100
        Dim sy As Integer = SepHeight
        Dim secn As Section
        'Dim drawBrush As New SolidBrush(Color.FromArgb(255, 255, 255))
        Dim drawBrush As New SolidBrush(Color.FromArgb(0, 0, 0))
        'Dim pn As New Pen(Color.FromArgb(255, 255, 255), 1)
        Dim pn As New Pen(Color.FromArgb(0, 0, 0), 1)
        sy = 0
        For k As Integer = 0 To RP.Sections.Count - 1

            secn = RP.Sections(k)

            If secn.state < 3 Then
                sy += SepHeight
            End If
            sh = secn.h
            Dim bin As Integer = 0
            cnt = 0

            If secn.state < 3 Then
                r = New Rectangle(1, sy - SepHeight, RulerThickness + 10, SepHeight - 1)

                Dim linGrBrush As New LinearGradientBrush(New Point(0, sy - SepHeight), New Point(0, sy), Color.Silver, Color.Gray)
                Dim path As GraphicsPath
                path = GetRoundedRectPath(r, 1)
                g.FillPath(linGrBrush, path)
                g.DrawPath(New Pen(Color.Gray, 1), path)

                r = New Rectangle(4, sy + 2 - SepHeight, SepHeight - 4, SepHeight - 5)
                g.FillRectangle(New SolidBrush(Color.FromArgb(55, 0, 0, 0)), r)
                g.DrawRectangle(New Pen(Color.FromArgb(155, 0, 0, 0), 1), r)
                Dim btnPad As Integer = 2
                r = New Rectangle(4 + btnPad, sy + 2 + btnPad - SepHeight, SepHeight - 4 - 2 * btnPad, SepHeight - 5 - 2 * btnPad)
                ' If secn.state = 1 Then
                path = New GraphicsPath
                path.AddLine(New Point(r.X, r.Y), New Point(r.X + r.Width, r.Y))
                path.AddLine(New Point(r.X + r.Width, r.Y), New Point(r.X + r.Width / 2, r.Y + r.Height))
                path.AddLine(New Point(r.X, r.Y), New Point(r.X + r.Width / 2, r.Y + r.Height))

                g.FillPath(Brushes.Black, path)
                'g.DrawPath(pn, path)
                'Else
                '    If secn.state = 2 Then
                '        path = New GraphicsPath
                '        path.AddLine(New Point(r.X, r.Y), New Point(r.X + r.Width, r.Y + r.Height / 2))
                '        path.AddLine(New Point(r.X + r.Width, r.Y + r.Height / 2), New Point(r.X, r.Y + r.Height))
                '        path.AddLine(New Point(r.X, r.Y), New Point(r.X, r.Y + r.Height))

                '        g.FillPath(Brushes.Black, path)
                '    End If
                'End If
            End If

            If secn.state = 2 Then
                sh = 0
            End If
            For i As Integer = 0 To sh - 1
                If unit = 40 Then
                    If i Mod 4 = 0 Then
                        If cnt Mod 5 = 0 Then
                            g.DrawLine(pn, 0, sy, 8, sy)
                        End If
                        If cnt Mod 10 = 0 Then

                            'If bin Mod 2 = 0 Then
                            r = New Rectangle(1, sy, 16, 10)
                            g.DrawString(bin, UFont, drawBrush, r, SF)
                            'End If						
                            g.DrawLine(pn, 0, sy, 15, sy)
                            bin += 1
                        Else
                            g.DrawLine(pn, 0, sy, 3, sy)
                        End If
                        cnt += 1
                        'sy += 4
                    End If
                Else
                    If i Mod 10 = 0 Then
                        If cnt Mod 5 = 0 Then
                            g.DrawLine(pn, 0, sy, 5, sy)
                            If i Mod 20 = 0 Then
                                r = New Rectangle(1, sy, 16, 10)
                                g.DrawString(bin, UFont, drawBrush, r, SF)
                                g.DrawLine(pn, 0, sy, 10, sy)
                                bin += 1
                            End If
                        Else
                            g.DrawLine(pn, 0, sy, 2, sy)
                        End If
                        cnt += 1
                        'sy  += 10 
                    End If
                End If
                sy += 1
            Next

        Next
    End Sub

    Sub DesignerLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        _DF = Me
        pictureBox3.AllowDrop = True
        panel6.Controls.Add(optionPanel)
        panel6.Controls.Add(shapePanel)
        optionPanel.Dock = DockStyle.Fill
        shapePanel.Dock = DockStyle.Fill
        optionPanel.Visible = False


        Dim h As Integer = 10
        h = RulerThickness
        pictureBox1.Height = h
        pictureBox2.Width = h
        pictureBox4.Width = h
        pictureBox5.Height = h

        pictureBox1.BackColor = RulerColor
        pictureBox2.BackColor = RulerColor
        pictureBox4.BackColor = RulerColor
        pictureBox5.BackColor = RulerColor

        'panel1.Controls.Add(pictureBox1)
        'panel1.Controls.Add(pictureBox2)
        'panel1.Controls.Add(pictureBox4)
        'panel1.Controls.Add(pictureBox5)


        Dim shp As New shapeItem(100, 100, 100, 100)
        shp.RecSides = AnchorStyles.Bottom Or AnchorStyles.Right

        If RP Is Nothing Then
            RP = New Report
            RP.nm = "Nuevo informe 1"
            For i As Integer = 0 To 4
                Dim secn As Section
                Select Case i
                    Case 0
                        secn = New Section
                        secn.h = 120
                        secn.nm = "Cabecera0"
                        secn.headerLabel = "Cabecera"
                    Case 1
                        secn = New Section
                        secn.h = 100
                        secn.nm = "Cabecera1"
                        secn.headerLabel = "Cabecera dos"
                    Case 2
                        secn = New SectionDetail
                        secn.h = 80
                        secn.nm = "detalle"
                        secn.headerLabel = "Cuerpo básico"
                        'secn.bcolor = color.SkyBlue
                    Case 3
                        secn = New Section
                        secn.h = 100
                        secn.nm = "Pie1"
                        secn.headerLabel = "Píe dos"
                    Case 4
                        secn = New Section
                        secn.h = 120
                        secn.nm = "Pie0"
                        secn.headerLabel = "Píe"
                End Select
                secn.GroupID = i
                RP.Sections.Add(secn)
            Next

        End If


        ShowProperty(RP)

        ShowTree()
        Done()
        Reshape()

    End Sub

    Public Sub ShowTree()
        treeView1.Nodes.Clear()
        treeView1.Nodes.Add(RP.nm)
        Me.Text = "Diseño informe  -  " & RP.nm
        treeView1.Nodes(0).Tag = 0
        'treeview1.Nodes(0).Nodes(i).Nodes.Clear
        Dim secn As Section
        For i As Integer = 0 To RP.Sections.Count - 1
            'secn = sections(i)
            secn = RP.Sections(i)
            treeView1.Nodes(0).Nodes.Add(secn.nm)
            treeView1.Nodes(0).Nodes(i).Tag = 1
            If secn.GroupID > 4 Then
                treeView1.Nodes(0).Nodes(i).ImageIndex = 5
                treeView1.Nodes(0).Nodes(i).SelectedImageIndex = 5
            Else
                treeView1.Nodes(0).Nodes(i).ImageIndex = 1
                treeView1.Nodes(0).Nodes(i).SelectedImageIndex = 1
            End If

            For j As Integer = 0 To secn.AR.Count - 1
                Dim itm As Item = secn.AR(j)
                treeView1.Nodes(0).Nodes(i).Nodes.Add(itm.nm)
                treeView1.Nodes(0).Nodes(i).Nodes(j).Tag = 2
                Dim iind As Integer = 2
                Select Case itm.ItemType
                    Case "Shape" '
                        iind = 11
                    Case "Label"
                        iind = 13
                    Case "DataLabel" 'aaa
                        iind = 6
                    Case "Image"
                        iind = 12
                    Case "DataImage"
                        iind = 7
                    Case "Barcode"
                        iind = 16
                    Case "Fbarcode"
                        iind = 17

                End Select
                treeView1.Nodes(0).Nodes(i).Nodes(j).ImageIndex = iind
                treeView1.Nodes(0).Nodes(i).Nodes(j).SelectedImageIndex = iind
            Next
        Next
        treeView1.ExpandAll()
    End Sub

    Public Sub SelectNode(ByVal str As String)
        treeView1.Focus()
        For i As Integer = 0 To treeView1.Nodes(0).Nodes.Count - 1
            If treeView1.Nodes(0).Nodes(i).Text = str Then
                treeView1.SelectedNode = treeView1.Nodes(0).Nodes(i)
                Exit For
            End If
        Next
    End Sub

    Private Sub SelectNode(ByVal str As String, ByVal i As Integer)
        treeView1.Focus()
        For j As Integer = 0 To treeView1.Nodes(0).Nodes(i).Nodes.Count - 1
            If treeView1.Nodes(0).Nodes(i).Nodes(j).Text = str Then
                treeView1.SelectedNode = treeView1.Nodes(0).Nodes(i).Nodes(j)
                Exit For
            End If
        Next
    End Sub

    Private Function Selseccion(ByVal i As Integer) As String
        Return treeView1.Nodes(0).Nodes(i).Text
    End Function


    Public Sub Reshape()
        'printset.DefaultPageSettings.Landscape = True
        RP.Orn = printset.DefaultPageSettings.Landscape
        If RP.Orn Then
            RP.w = printset.DefaultPageSettings.PaperSize.Height - RP.leftm - RP.Rightm
        Else
            RP.w = printset.DefaultPageSettings.PaperSize.Width - RP.leftm - RP.Rightm
        End If
        'PictureBox1.Invalidate
        'PictureBox2.Invalidate
        'PictureBox3.Invalidate
        Me.Refresh()
        timer2.Enabled = True
    End Sub

    Public Sub ShowProperty(ByVal obj As Object)
        Try
            If obj Is Nothing Then
                label1.Text = ""
            Else
                label1.Text = obj.name
            End If
            propertyGrid1.SelectedObject = obj
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error mostrando las propiedades")
        End Try
    End Sub

    Sub PictureBox3Click(ByVal sender As Object, ByVal e As EventArgs) Handles pictureBox3.Click
        'panel2.Focus() = pictureBox3.PointToClient(Cursor.Position)
    End Sub

    Private Sub pictureBox3_MouseClick(sender As Object, e As MouseEventArgs) Handles pictureBox3.MouseClick
        'Dim Dentrodealgo As Boolean = False
        'Dim y As Integer = 0
        'Dim R As Rectangle
        'Dim secn As Section
        'Dim sel As Boolean = False
        'Dim sel_secn As Integer = -1
        'Dim sel_item As Integer = -1

        'If e.Button = Windows.Forms.MouseButtons.Left Then
        '    Dim p As Point = pictureBox3.PointToClient(Cursor.Position)
        '    OldP = p
        '    oldx = p.X
        '    oldy = p.Y

        '    For i As Integer = 0 To RP.Sections.Count - 1
        '        For j As Integer = 0 To RP.Sections(i).AR.Count - 1
        '            If RP.Sections(i).AR(j).selected Then
        '                sel_secn = i
        '                sel_item = j
        '            End If
        '            RP.Sections(i).AR(j).selected = False
        '        Next
        '    Next

        '    For i As Integer = 0 To RP.Sections.Count - 1
        '        For j As Integer = 0 To RP.Sections(i).AR.Count - 1

        '            Dim inum As Integer = secn.AR.Count - 1
        '            For k As Integer = 0 To inum
        '                If sel_secn = i And sel_item = (inum - k) Then
        '                    Dim t As Item = secn.AR(inum - k)
        '                    Dim rec As New Rectangle(t.Rec.Location, New Size(t.Rec.Width + DragMarginWidth, t.Rec.Height + DragMarginWidth))
        '                    If rec.Contains(p.X, p.Y - R.Y) Then
        '                        Dentrodealgo = True
        '                        Return
        '                    End If
        '                End If
        '            Next

        '        Next
        '    Next
        'End If
        'orPunto = pictureBox3.PointToClient(Cursor.Position)
        'seleccionando = True
    End Sub


    Sub PictureBox3MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox3.MouseDown
        Dim sel_secn As Integer = -1
        Dim sel_item As Integer = -1
        Dim fueraseleccion As Boolean

        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim p As Point = pictureBox3.PointToClient(Cursor.Position)
            OldP = p
            oldx = p.X
            oldy = p.Y

            Dim y As Integer = 0
            Dim R As Rectangle
            Dim secn As Section
            Dim sel As Boolean = False
            For i As Integer = 0 To RP.Sections.Count - 1
                RP.Sections(i).selected = False
                For j As Integer = 0 To RP.Sections(i).AR.Count - 1
                    'RP.Sections(i).selected = False
                    If RP.Sections(i).AR(j).selected Then
                        sel_secn = i
                        sel_item = j
                    End If
                    RP.Sections(i).AR(j).selected = False
                Next

            Next


            If Not ClicSobreUnObjeto(p, sel_secn, sel_item) Then
                If nis.Seleccionados.Count < 1 Then
                    ' Hice clic en ningún objeto
                    orPunto.X = oldx
                    orPunto.Y = oldy
                    seleccionando = True
                    nis.Rec = New Rectangle(orPunto.X, orPunto.Y, 1, 1)
                    nis.pn = New Pen(Color.Black, 2)
                    nis.g = pictureBox3.CreateGraphics
                    nis.pn.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
                    nis.visible = True
                    nis.rp = RP
                Else
                    seleccionando = False
                    nis.visible = False
                    nis.Limpiar()
                    pictureBox3.Invalidate() '.Refresh()
                    'If fueraseleccion Then
                    ' Hice clic en ningún objeto
                    orPunto.X = oldx
                    orPunto.Y = oldy
                    seleccionando = True
                    nis.Rec = New Rectangle(orPunto.X, orPunto.Y, 1, 1)
                    nis.pn = New Pen(Color.Black, 2)
                    nis.g = pictureBox3.CreateGraphics
                    nis.pn.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
                    nis.visible = True
                    nis.rp = RP
                End If
            End If

            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)

                Dim addh As Integer = 0
                For k As Integer = 0 To secn.AR.Count - 1
                    Dim t As Item
                    t = secn.AR(k)
                    If sel_secn = i Then
                        If sel_item = k Then
                            addh = SepHeight + 1
                        End If
                    End If
                Next

                If secn.state <> 2 Then
                    'Dim yy As Integer
                    Dim x As Integer = 0
                    'yy = y            			

                    If secn.state = 1 Then
                        R = New Rectangle(0, y + SepHeight, RP.w, secn.h + addh)
                    Else
                        R = New Rectangle(0, y, RP.w, secn.h + addh)
                    End If

                    Dim inum As Integer = secn.AR.Count - 1
                    For k As Integer = 0 To inum
                        If sel_secn = i And sel_item = (inum - k) Then
                            Dim t As Item = secn.AR(inum - k)
                            Dim rec As New Rectangle(t.Rec.Location, New Size(t.Rec.Width + DragMarginWidth, t.Rec.Height + DragMarginWidth))
                            If rec.Contains(p.X, p.Y - R.Y) Then
                                si = t
                                selsecn = i
                                t.selected = True
                                sel = True
                                ControlSelected = 3
                                OldRec.Location = New Point(p.X, p.Y - R.Y)
                                OldRec.Size = t.Rec.Size
                                oldLocation = t.Rec.Location
                                ShowProperty(t)
                                ShowOptions()
                                SelectNode(t.nm, i)

                                RP.Sections(i).selected = True

                                SecSelec = Selseccion(i)

                                If t.ItemType = "Image" Then
                                    If t.img Is Nothing Then
                                        removeImageToolStripMenuItem.Visible = False
                                    Else
                                        removeImageToolStripMenuItem.Visible = True
                                    End If
                                Else
                                    removeImageToolStripMenuItem.Visible = False
                                End If
                                GoTo theEnd
                                MsgBox("Arriba")
                                Exit For
                            End If
                        End If

                    Next

                    For k As Integer = 0 To inum
                        Dim t As Item = secn.AR(inum - k)
                        t.selected = False
                        Dim rec As Rectangle
                        Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                        If t.bw < 1 Then
                            handPad = 0
                        End If

                        If sel_secn = i And sel_item = (inum - k) Then
                            rec = New Rectangle(t.Rec.Location, New Size(t.Rec.Width + DragMarginWidth + 2, t.Rec.Height + DragMarginWidth + 2))
                        Else
                            rec = t.Rec
                        End If
                        'rec = New Rectangle(t.Rec.Location, New size(t.Rec.Width + DragMarginWidth + 2, t.Rec.Height + DragMarginWidth + 2))

                        If rec.Contains(p.X, p.Y - R.Y) Then
                            si = t
                            selsecn = i
                            t.selected = True
                            sel = True
                            ControlSelected = 3
                            OldRec.Location = New Point(p.X, p.Y - R.Y)
                            OldRec.Size = t.Rec.Size
                            oldLocation = t.Rec.Location
                            ShowProperty(t)
                            ShowOptions()


                            SelectNode(t.nm, i)
                            RP.Sections(i).selected = True

                            SecSelec = Selseccion(i)

                            If t.ItemType = "Image" Then
                                If t.img Is Nothing Then
                                    removeImageToolStripMenuItem.Visible = False
                                Else
                                    removeImageToolStripMenuItem.Visible = True
                                End If
                            Else
                                removeImageToolStripMenuItem.Visible = False
                            End If
                            GoTo theEnd
                        Else
                            t.selected = False
                        End If
                        x = p.X
                        'yy = p.y - r.Y
                    Next
                End If
                'exit for

                If secn.state < 3 Then
                    If secn.state = 2 Then
                        R = New Rectangle(0, y, RP.w, SepHeight)
                    Else
                        R = New Rectangle(0, y, RP.w, secn.h + SepHeight + addh)
                    End If
                Else
                    R = New Rectangle(0, y, RP.w, secn.h + addh)
                End If

                'if mouse is in section
                If R.Contains(New Point(oldx, oldy)) Then
                    ShowProperty(secn)
                    ControlSelected = 2
                    selsecn = i
                    If secn.GroupID > 4 Then
                        HideOptions(False)
                    Else
                        HideOptions(True)
                    End If

                    SecSelec = secn.Nombre

                    SelectNode(secn.nm)
                    RP.Sections(i).selected = True
                    R = New Rectangle(2, y + 2, SepHeight - 4, SepHeight - 5)
                    If R.Contains(New Point(oldx, oldy)) Then
                        If secn.state < 3 Then
                            secn.state = 3
                        Else
                            secn.state = 1
                        End If
                        Me.Refresh()
                        Exit Sub
                    End If
                    If secn.state = 3 Then
                        R = New Rectangle(2 + 4 + SepHeight - 4, y + 2, SepHeight - 4, SepHeight - 5)
                        If R.Contains(New Point(oldx, oldy)) Then
                            secn.state = 1
                            Me.Refresh()
                            Exit Sub
                        End If
                    End If
                End If

                If secn.state < 3 Then
                    y += SepHeight
                End If
                If secn.state <> 2 Then
                    y += secn.h
                End If
            Next
        Else      ' La tecla derecha del ratón.
            Dim y As Integer = 0
            For i As Integer = 0 To RP.Sections.Count - 1
                If RP.Sections(i).state < 3 Then
                    y += SepHeight
                End If
                For j As Integer = 0 To RP.Sections(i).AR.Count - 1
                    If RP.Sections(i).AR(j).selected Then
                        Dim rec As Rectangle
                        rec = RP.Sections(i).AR(j).Rec
                        rec.Y += y
                        If rec.Contains(e.Location) Then
                            Me.contextMenuStrip1.Items.Clear()
                            If nis.Seleccionados.Count > 0 Then
                                Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.removeToolStripMenuItem, Me.removeImageToolStripMenuItem, Me.duplicateToolStripMenuItem, Me.bringToFrontToolStripMenuItem, Me.sendToBackToolStripMenuItem, Me.SeparadorMenu1, Me.IzqToolStripMenuItem, Me.DerToolStripMenuItem, Me.ArrToolStripMenuItem, Me.AbaToolStripMenuItem, Me.SeparadorMenu, Me.HorToolStripMenuItem, Me.VerToolStripMenuItem})
                            Else
                                Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.removeToolStripMenuItem, Me.removeImageToolStripMenuItem, Me.duplicateToolStripMenuItem, Me.bringToFrontToolStripMenuItem, Me.sendToBackToolStripMenuItem})
                            End If
                            contextMenuStrip1.Show(pictureBox3, e.Location)
                        End If
                    End If
                Next
                If RP.Sections(i).state <> 2 Then
                    y += RP.Sections(i).h
                End If
            Next
        End If
theEnd:
        pictureBox3.Invalidate()
    End Sub


    Sub PictureBox2MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox2.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'dim p as Point = PictureBox3.PointToClient(Cursor.Position)
            oldx = e.X
            oldy = e.Y

            Dim y As Integer = 0
            Dim R As Rectangle
            Dim secn As Section
            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)
                R = New Rectangle(0, y, RP.w, secn.h + SepHeight)
                If R.Contains(New Point(oldx, oldy)) Then
                    ShowProperty(secn)
                    R = New Rectangle(4, y + 2, SepHeight - 4, SepHeight - 5)
                    If R.Contains(New Point(oldx, oldy)) Then
                        If secn.state = 1 Then
                            secn.state = 1 '2
                        Else
                            secn.state = 1
                        End If
                        Me.Refresh()
                        Exit For
                    End If
                End If
                If secn.state < 3 Then
                    y += SepHeight
                End If
                If secn.state <> 2 Then
                    y += secn.h
                End If
            Next
        End If
    End Sub

    Sub Panel2DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles panel2.DragEnter
        'e.Effect = DragDropEffects.Copy
    End Sub

    Sub Panel2DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles panel2.DragDrop

    End Sub

    Sub PictureBox3DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles pictureBox3.DragDrop
        Dim p As Point = pictureBox3.PointToClient(Cursor.Position)
        oldx = p.X
        oldy = p.Y

        Dim yy As Integer = 0
        Dim x As Integer = 0
        Dim R As Rectangle
        Dim secn As Section
        Dim ni As Object
        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            If secn.state <> 2 Then
                If secn.state = 1 Then
                    R = New Rectangle(0, yy + SepHeight, RP.w, secn.h)
                Else
                    R = New Rectangle(0, yy, RP.w, secn.h)
                End If
                Dim y As Integer = 0
                If R.Contains(New Point(oldx, oldy)) Then
                    x = p.X
                    y = p.Y - R.Y
                    'label3.Text = i & " - " & X & ", " & Y      
                    If y >= 0 Then
                        Dim itype As String = e.Data.GetData(DataFormats.Text).ToString
                        Select Case itype
                            Case "Label"
                                ni = New LabelItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Shape"
                                ni = New shapeItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Image"
                                ni = New ImageItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "DataLabel"
                                ni = New DataLabel(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "DataImage"
                                ni = New DataImage(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Barcode"
                                ni = New BarcodeItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Fbarcode"
                                ni = New BarcodeFieldItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                                'Case Else
                                '    ni = New DataLabel(x, y, 20, 15)
                                '    ni.nm = NewItem("DataLabel", secn.AR)
                                '    ni.itemtype = "DataLabel"
                                '    ni.DataField = itype
                                '    ni.name = itype



                        End Select
                        MaxItems = MaxItems + 1
                        ni.NumeroItem = MaxItems
                        secn.AR.Add(ni)
                        si = ni

                        QuitarSeleccionado()

                        si.selected = True ' a ver que pasa
                        ShowProperty(si)
                        ShowTree()

                        ShowOptions()
                        SelectNode(ni.nm, i)
                        SecSelec = Selseccion(i)

                        If itype = "Image" Then
                            Dim oFrm As Visor = New Visor
                            oFrm.ObjetoGlobal = Me.ObjetoGlobal
                            oFrm.cn = Me.ObjetoGlobal.cn
                            oFrm.ShowDialog()
                            If oFrm.resultado = DialogResult.OK Then
                                si.img = oFrm.img
                                si.codimg = oFrm.codigo
                            End If
                        End If

                        pictureBox3.Invalidate()
                        Done()
                        EssDrag = True
                    End If
                End If
            End If

            If secn.state < 3 Then
                yy += SepHeight
            End If
            If secn.state <> 2 Then
                yy += secn.h
            End If
        Next
    End Sub

    Sub PictureBox3DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles pictureBox3.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Sub PictureBox3MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox3.MouseUp
        pictureBox3.Cursor = Cursors.Default
        ResType = 0
        WaitTime = 0
        OldP = Nothing
        Parar = False
        If seleccionando Then
            nis.incluirItems()
            seleccionando = False
            pictureBox3.Refresh()
        End If

        If e.Button = MouseButtons.Left Then
            If actionDone Then
                Done()
                actionDone = False
            End If
        Else

        End If
    End Sub

    Sub PictureBox3MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox3.MouseMove
        Dim p As Point = pictureBox3.PointToClient(Cursor.Position)


        oldx = p.X
        oldy = p.Y

        Dim yy As Integer = 0
        Dim x As Integer = 0
        Dim R As Rectangle
        Dim secn As Section

        If seleccionando Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                nis.Rec.Width = oldx - orPunto.X
                nis.Rec.Height = oldy - orPunto.Y
                nis.Draw()
                pictureBox3.Invalidate()
                pictureBox3.Refresh()
                Return
            End If
        End If


        'If nis.Seleccionados.Count > 0 Then
        '    Dim it As Item
        '    For Each itm As Item In nis.Seleccionados
        '        it = itm
        '        If it.Rec.X = -1 Then
        '            Parar = True
        '        End If
        '        If si.Rec.Y = -1 Then
        '            Parar = True
        '        End If
        '    Next
        'End If

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            If secn.state <> 2 Then
                If secn.state = 1 Then
                    R = New Rectangle(0, yy + SepHeight, RP.w + 20, secn.h + SepHeight)
                Else
                    R = New Rectangle(0, yy, RP.w + 20, secn.h + SepHeight)
                End If
                Dim y As Integer = 0
                'Test if in section
                If R.Contains(New Point(oldx, oldy)) Then
                    x = p.X
                    y = p.Y - R.Y
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If Not (si Is Nothing) And selsecn = i Then
                            Dim ItemRec As Rectangle
                            ItemRec = New Rectangle(si.Rec.Location, si.Rec.Size)
                            If WaitTime > 0 Then
                                ItemRec.Height += 500
                                ItemRec.Width += 500
                            Else
                                ItemRec.Height += 12
                                ItemRec.Width += 12
                            End If
                            '
                            If 1 = 1 Then
                                WaitTime += 1
                                Select Case pictureBox3.Cursor
                                    Case Cursors.SizeNS
                                        si.Rec.Height = OldRec.Height + (p - OldP).Y '    new Point(oldrec.Width, oldrec.Height) + (new point(x, y) - oldrec.Location)
                                        If si.Rec.Height < 2 Then
                                            si.Rec.Height = 1
                                        End If
                                        'goto toend
                                    Case Cursors.SizeWE
                                        si.Rec.Width = OldRec.Width + (p - OldP).X    'new Point(oldrec.Width, oldrec.Height) + (new point(x, y) - oldrec.Location)
                                        If si.Rec.Width < 2 Then
                                            si.Rec.Width = 1
                                        End If
                                        'goto toend
                                    Case Cursors.SizeNWSE
                                        si.Rec.Size = New Point(OldRec.Width, OldRec.Height) + (p - OldP)
                                        If si.Rec.Height < 2 Then
                                            si.Rec.Height = 1
                                        End If
                                        If si.Rec.Width < 2 Then
                                            si.Rec.Width = 1
                                        End If
                                        'goto toend
                                    Case Cursors.Hand
                                        If Not Parar Then
                                            p1 = si.Rec.Location
                                            si.Rec.Location = oldLocation + (p - OldP)
                                            p2 = si.Rec.Location
                                            If si.Rec.X < -1 Then
                                                si.Rec.X = -1
                                            End If
                                            If si.Rec.Y < -1 Then
                                                si.Rec.Y = -1
                                            End If

                                            If nis.Seleccionados.Count > 0 Then

                                                For Each itm As Item In nis.Seleccionados
                                                    If Not Parar Then
                                                        Dim t As Item
                                                        Dim xxx As Integer = p2.X - p1.X
                                                        Dim yyy As Integer = p2.Y - p1.Y
                                                        t = itm
                                                        ' coordenadas.Text = "" & oldLocation.X & "," & oldLocation.Y & "(" & (t.Rec.Location.X + xxx) & "," & (t.Rec.Location.Y + yyy) & ")"
                                                        If t.Rec.Location <> si.Rec.Location Then
                                                            t.Rec.Location = New Point(t.Rec.Location.X + xxx, t.Rec.Location.Y + yyy)
                                                            If t.Rec.X < -1 Then
                                                                t.Rec.X = -1
                                                                Parar = True
                                                            End If
                                                            If t.Rec.Y < -1 Then
                                                                t.Rec.Y = -1
                                                                Parar = True
                                                            End If
                                                        End If
                                                    End If
                                                Next

                                            End If
                                        End If

                                        'goto toend
                                End Select
                                actionDone = True
                                pictureBox3.Refresh()
                                'goto toend
                            End If
                        End If
                    Else
                        For k As Integer = 0 To secn.AR.Count - 1
                            Dim t As Item
                            t = secn.AR(k)
                            If t.selected Then

                                Dim HW, HH As Integer
                                Dim Hx, Hy As Integer
                                If si.Rec.Width - 30 < 10 Then
                                    HW = 10
                                Else
                                    HW = si.Rec.Width - 30
                                End If
                                If si.Rec.Height - 30 < 10 Then
                                    HH = 10
                                Else
                                    HH = si.Rec.Height - 30
                                End If
                                Dim handPad As Integer = Math.Ceiling(si.bw / 2) - 1
                                If si.bw < 1 Then
                                    handPad = 0
                                End If
                                Hx = si.Rec.X + (si.Rec.Width - HW) / 2
                                Hy = si.Rec.Y + si._h + (si.Rec.Height - HH) / 2
                                Dim R1 As New Rectangle(Hx, si.Rec.Y + si.Rec.Height + si._h + handPad, HW, 10)
                                Dim R2 As New Rectangle(si.Rec.X + si.Rec.Width + handPad, Hy, 10, HH)
                                Dim R3 As New Rectangle(si.Rec.X + si.Rec.Width - 1 + handPad, si.Rec.Y + si.Rec.Height - 1 + si._h + handPad, 10, 10)

                                'dim r1 as New Rectangle(t.Rec.X + DragMarginWidth, t.Rec.Y + t.Rec.Height + DragMarginWidth, t.Rec.Width - 2*DragMarginWidth, DragMarginWidth)
                                'Dim r2 As New Rectangle(t.Rec.X + t.Rec.Width, t.Rec.Y + t.Rec.Height - 2*DragMarginWidth, DragMarginWidth, t.Rec.Height - 2*DragMarginWidth)
                                'Dim r3 As New Rectangle(t.Rec.X + t.Rec.Width, t.Rec.Y + t.Rec.Height, DragMarginWidth, DragMarginWidth)
                                If R1.Contains(New Point(x, y)) Then
                                    pictureBox3.Cursor = Cursors.SizeNS
                                    ResType = 1
                                ElseIf R2.Contains(New Point(x, y)) Then
                                    pictureBox3.Cursor = Cursors.SizeWE
                                    ResType = 2
                                ElseIf R3.Contains(New Point(x, y)) Then
                                    pictureBox3.Cursor = Cursors.SizeNWSE
                                    ResType = 3
                                ElseIf New Rectangle(si.Rec.X - handPad, si.Rec.Y - handPad, si.Rec.Width + 2 * handPad, si.Rec.Height + 2 * handPad).Contains(New Point(x, y)) Then
                                    pictureBox3.Cursor = Cursors.Hand
                                    ResType = 0
                                Else
                                    pictureBox3.Cursor = Cursors.Default
                                End If
                            End If

                        Next
                    End If
                    'label2.Text = X & ", " & Y        			
                End If
            End If

            If secn.state < 3 Then
                yy += SepHeight
            End If
            If secn.state <> 2 Then
                yy += secn.h
            End If
        Next
toend:
    End Sub

    Dim pgFocused As Boolean = False
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'detect up arrow key
        If ControlSelected = 3 And Not (si Is Nothing) And pgFocused = False Then
            If keyData = Keys.Up Then
                If si.selected Then
                    si.Rec.Y = si.Rec.Y - 1
                    ShowProperty(si)
                    Done()
                    pictureBox3.Invalidate()
                    Return True
                End If
            End If
            If keyData = Keys.Down Then
                If si.selected Then
                    si.Rec.Y = si.Rec.Y + 1
                    ShowProperty(si)
                    Done()
                    pictureBox3.Invalidate()
                    Return True
                End If
            End If
            If keyData = Keys.Left Then
                If si.selected Then
                    si.Rec.X = si.Rec.X - 1
                    ShowProperty(si)
                    Done()
                    pictureBox3.Invalidate()
                    Return True
                End If
            End If
            If keyData = Keys.Right Then
                If si.selected Then
                    si.Rec.X = si.Rec.X + 1
                    ShowProperty(si)
                    Done()
                    pictureBox3.Invalidate()
                    Return True
                End If
            End If
            If keyData = Keys.Delete Then
                If si.selected Then
                    Dim secn As Section
                    For j As Integer = 0 To RP.Sections.Count - 1
                        secn = RP.Sections(j)
                        For i As Integer = 0 To secn.AR.Count - 1
                            Dim itm As Item = secn.AR(i)
                            If itm.selected Then
                                secn.AR.Remove(itm)
                                ShowProperty(Nothing)
                                HideOptions()
                                ShowTree()
                                Done()
                                Reshape()
                                Exit For
                            End If
                        Next
                    Next

                    Return True
                End If
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Sub PropertyGrid1Enter(ByVal sender As Object, ByVal e As EventArgs) Handles propertyGrid1.Enter
        pgFocused = True
    End Sub

    Sub PropertyGrid1Leave(ByVal sender As Object, ByVal e As EventArgs) Handles propertyGrid1.Leave
        pgFocused = False
    End Sub

    Sub Button7Click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim pp As New PPreview
        'pp.ShowDialog
        'pp.Dispose
        'exit sub
        'Sample Items
        Dim detailData As New DataTable
        detailData.Columns.Add("id")
        detailData.Columns.Add("name")
        detailData.Columns.Add("qty")
        detailData.Columns.Add("image")
        For i As Integer = 0 To 100
            detailData.Rows.Add()
            detailData.Rows(i).Item("id") = i
            detailData.Rows(i).Item("name") = "Pitzza Hut " & i
            detailData.Rows(i).Item("qty") = 5
            ' detailData.Rows(i).Item("image") = ImageToBase64(picturebox6.Image, ImageFormat.Jpeg)
        Next

        Using p As New PrintDesign(RP)
            Dim secnx As Section = RP.Sections(2)
            secnx.dt = detailData
            p.PrintReport(True)
        End Using
    End Sub

    Public SaveToDB As Boolean = False

    Sub Button1Click(ByVal sender As Object, ByVal e As EventArgs) Handles bCargar.Click
        '    Dim oForm As AbrirInformes = New AbrirInformes

        '    'Save
        '    'If Not SaveToDB Then
        '    '    If ScriptFile = "" Then
        '    '        Dim fd As New SaveFileDialog
        '    '        fd.Filter = "LNSoft Report|*.LRPX"
        '    '        fd.Title = "Save a Design File"
        '    '        fd.FileName = "Informe 1"

        '    '        If fd.ShowDialog = DialogResult.OK Then
        '    '            ScriptFile = fd.FileName
        '    '            timer1.Enabled = True
        '    '            label3.Visible = True
        '    '            'createfile(scriptfile, GenStr())
        '    '        End If
        '    '    Else
        '    '        label3.Visible = True
        '    '        timer1.Enabled = True
        '    '    End If
        '    'End If
    End Sub


    Sub bGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bGuardar.Click
        'New
        If Not SaveToDB Then
            If savePending Then
                Dim f As New CustMsg
                f.msg.Text = "Quieres guardar las modificaciones realizadas a '" & RP.nm & "'?"
                f.button1.Text = "No"
                f.button2.Text = "Sí"
                f.button3.Visible = True
                f.ShowDialog()

                If f.OK Then
                    If Not savePending Then
                        Exit Sub
                    End If
                    Formatonuevo = False
                    GrabarInforme()
                    savePending = False
                Else
                    Exit Sub
                End If
                f.Dispose()
            End If
            ScriptFile = ""
        End If

    End Sub

    Sub Button3Click(ByVal sender As Object, ByVal e As EventArgs) Handles bCargar.Click
        Dim oForm As AbrirInformes = New AbrirInformes
        Dim aRet() As String
        Dim TRP As Report

        oForm.ObjetoGlobal = Me.ObjetoGlobal
        oForm.cn = Me.cn
        oForm.cargar_datos()
        If oForm.ShowDialog() = DialogResult.OK Then
            aRet = oForm.retorno
            'MsgBox(aRet(0) & "-" & aRet(1))
            treeView1.Nodes.Clear()
            If oForm.informes_antiguos Then
                TRP = CargarInformeAntiguos(aRet)
            Else
                TRP = CargarInforme(aRet)
            End If

            nuevoproceso = False
            nuevodocumento = False
            Formatonuevo = False
            RP = TRP
            Me.Refresh()
                ShowProperty(RP)
                ShowTree()
                savePending = False
                Me.Refresh()
                Done()
                Reshape()
            Else
                MsgBox("No hace nada")
        End If

        'Open
        'If Not SaveToDB Then
        '    If savePending Then
        '        Dim f As New CustMsg
        '        f.msg.Text = "Quieres guardar las modificaciones realizadas a '" & RP.nm & "'?"
        '        f.button1.Text = "No"
        '        f.button2.Text = "Sí"
        '        f.button3.Visible = True
        '        f.ShowDialog()

        '        If f.OK Then
        '            Button1Click(Nothing, Nothing)
        '            If savePending Then
        '                Exit Sub
        '            End If
        '        Else
        '            If f.cancel Then
        '                Exit Sub
        '            Else
        '                savePending = False
        '            End If
        '        End If
        '        f.Dispose()
        '    End If
        '    If savePending Then
        '        Exit Sub
        '    End If
        '    Me.SuspendLayout()
        '    Dim fd As New OpenFileDialog
        '    fd.Filter = "Info Report|*.LRPX"
        '    fd.Title = "Guardar archivo e. formato"
        '    If fd.ShowDialog = DialogResult.OK Then
        '        Try

        '            Try
        '                Me.SuspendLayout()
        '                Dim TRP As Report
        '                TRP = ReportFromFile(fd.FileName)
        '                RP = TRP
        '                ScriptFile = fd.FileName
        '                Me.ResumeLayout()

        '            Catch exx As Exception
        '                MessageBox.Show(vbCrLf & exx.Message & vbCrLf, "Informe leido falla el elemento")
        '            End Try
        '        Catch ex As Exception
        '            MessageBox.Show(vbCrLf & ex.Message & vbCrLf, "Informe cargado falla")
        '        End Try
        '        Me.ResumeLayout()
        '    End If
        '    Me.Refresh()
        '    ShowProperty(RP)
        '    ShowTree()
        '    savePending = False
        '    Me.Refresh()
        'End If

        'oForm.ObjetoGlobal = Me.ObjetoGlobal
        'oForm.cn = Me.cn
        'oForm.ShowDialog()


    End Sub

    'Private Function GenStr() As String
    '    Return ReportToString(RP)
    'End Function

    Private Sub ClearSel()
        For i As Integer = 0 To RP.Sections.Count - 1
            Dim secn As Section
            secn = RP.Sections(i)
            For j As Integer = 0 To secn.AR.Count - 1
                Dim t As Item
                t = secn.AR(j)
                t.selected = False
            Next
        Next
        ShowProperty(RP)
        ShowOptions(False)
    End Sub

    Private Sub ControlBtn()
        If actions.Count + ur - 1 < 1 Then
            bAnterior.Enabled = False
        Else
            bAnterior.Enabled = True
        End If
        If actions.Count + ur - 1 >= actions.Count - 1 Then
            bsiguiente.Enabled = False
        Else
            bsiguiente.Enabled = True
        End If
    End Sub

    Public Sub Done()
        actions.Add(RP.Clone(RP))
        actionIndex += 1
        ur = 0
        ControlBtn()
        savePending = True
    End Sub

    Sub Button4Click(ByVal sender As Object, ByVal e As EventArgs) Handles bAnterior.Click
        'Undo
        Try
            ur -= 1
            'msgbox(ur)
            Dim ni As Integer = actions.Count + ur - 1
            'msgbox(actions.Count)
            'msgbox(actions.Count - 1 & " - " & ni)
            If ni >= 0 And ni < actions.Count Then
                'msgbox(ni)
                Dim tmp As Report
                tmp = actions(ni)
                RP = tmp.Clone(tmp)
                ShowProperty(RP)
                ShowTree()
                ClearSel()
                Reshape()
                savePending = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ControlBtn()
    End Sub

    Sub Button5Click(ByVal sender As Object, ByVal e As EventArgs) Handles bsiguiente.Click
        'Redo
        Try

            Dim ni As Integer = actions.Count + ur - 1
            'msgbox(actions.Count)
            'msgbox(ni)
            If ni >= 0 And ni < actions.Count Then
                'msgbox(ni)
                Dim tmp As Report
                tmp = actions(ni)
                RP = tmp.Clone(tmp)
                ShowProperty(RP)
                ur += 1
                ShowTree()
                ClearSel()
                Reshape()
                savePending = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ControlBtn()
    End Sub

    Sub Button6Click(ByVal sender As Object, ByVal e As EventArgs)
        'Settings
        Dim Pd As New PrintDialog()
        Pd.PrinterSettings = printset
        If Pd.ShowDialog = DialogResult.OK Then
            printset = Pd.PrinterSettings
            RP.Orn = printset.DefaultPageSettings.Landscape
            If RP.Orn Then
                RP.w = printset.DefaultPageSettings.PaperSize.Height - RP.leftm - RP.Rightm
            Else
                RP.w = printset.DefaultPageSettings.PaperSize.Width - RP.leftm - RP.Rightm
            End If
            ShowProperty(RP)
            Me.Refresh()
        End If
    End Sub

    Public Sub createfile(ByVal fn As String, ByVal str As String)
        If File.Exists(fn) Then
            File.Delete(fn)
        End If
        Using sw As StreamWriter = File.CreateText(fn)
            sw.Write(str)
            sw.Close()
        End Using
    End Sub


    Sub TreeView1NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles treeView1.NodeMouseClick
        Dim secn As Section
        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            For j As Integer = 0 To secn.AR.Count - 1
                secn.AR(j).selected = False
            Next
        Next
        Select Case e.Node.Tag.ToString
            Case 0
                ShowProperty(RP)
                ShowOptions(False)
            Case 1
                Dim str As String = e.Node.Text
                'dim secn as Section
                For i As Integer = 0 To RP.Sections.Count - 1
                    secn = RP.Sections(i)
                    'msgbox(e.Node.Parent.Text)
                    If secn.nm = e.Node.Text Then
                        ShowProperty(secn)
                        If secn.GroupID > 4 Then
                            HideOptions(False)
                        Else
                            HideOptions(True)
                        End If

                        Exit For
                    End If
                Next
            Case 2
                Dim str As String = e.Node.Text
                'dim secn as Section
                For i As Integer = 0 To RP.Sections.Count - 1
                    secn = RP.Sections(i)
                    If secn.nm = e.Node.Parent.Text Then
                        For j As Integer = 0 To secn.AR.Count - 1
                            Dim itm As Object = secn.AR(j)
                            If itm.nm = str Then
                                si = itm
                                si.selected = True
                                'itemselected = True
                                ShowProperty(si)
                                ShowOptions()
                                Exit For
                            End If
                        Next
                    End If
                Next
        End Select
        Reshape()
    End Sub


    Sub Button8Click(ByVal sender As Object, ByVal e As EventArgs)
        'Help
        Dim f As New Help
        f.ShowDialog()
        f.Dispose()
    End Sub

    Sub Timer1Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
        'timer1.Enabled = False
        'createfile(ScriptFile, GenStr())
        'label3.Visible = False
        'savePending = False
    End Sub

    Sub DesignerFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not SaveToDB Then
            If savePending Then
                Dim f As New CustMsg
                f.msg.Text = "Quieres guardar las modificaciones realizadas a '" & RP.nm & "'?"
                f.button1.Text = "No"
                f.button2.Text = "Sí"
                f.button3.Visible = True
                f.ShowDialog()

                If f.OK Then
                    Button1Click(Nothing, Nothing)
                    If savePending Then
                        e.Cancel = False
                        SaveToDB = True
                        Me.Close()
                    End If
                Else
                    If f.cancel = True Then
                        e.Cancel = True
                        SaveToDB = True
                    Else
                        e.Cancel = False
                        SaveToDB = True
                        Me.Close()

                    End If
                End If
                f.Dispose()
            End If
        End If
        'Close()
    End Sub

    Sub Timer2Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer2.Tick
        timer2.Enabled = False
        Me.Refresh()
    End Sub

    Sub RemoveToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles removeToolStripMenuItem.Click
        optionPanel.nis = nis
        optionPanel.Label1Click(Nothing, Nothing)
    End Sub

    Sub DuplicateToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles duplicateToolStripMenuItem.Click
        optionPanel.nis = nis
        optionPanel.Label2Click(Nothing, Nothing)
    End Sub

    Sub BringToFrontToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles bringToFrontToolStripMenuItem.Click
        optionPanel.nis = nis
        optionPanel.Label5Click(Nothing, Nothing)
    End Sub

    Sub SendToBackToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles sendToBackToolStripMenuItem.Click
        optionPanel.nis = nis
        optionPanel.Label3Click(Nothing, Nothing)
    End Sub

    Sub IzqToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles IzqToolStripMenuItem.Click
        Dim t As Item
        Dim MasTop As Integer = 1000
        For Each itm As Item In nis.Seleccionados
            t = itm
            If t.Rec.X < MasTop Then
                MasTop = t.Rec.X
            End If
        Next

        For Each itm As Item In nis.Seleccionados
            t = itm
            t.Rec.X = MasTop
        Next
        Me.Refresh()


    End Sub
    Sub DerToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles DerToolStripMenuItem.Click
        Dim t As Item
        Dim MasTop As Integer = 0
        For Each itm As Item In nis.Seleccionados
            t = itm
            If t.Rec.X + t.Rec.Width > MasTop Then
                MasTop = t.Rec.X + t.Rec.Width
            End If
        Next

        For Each itm As Item In nis.Seleccionados
            t = itm
            t.Rec.X = MasTop - t.Rec.Width
        Next
        Me.Refresh()


    End Sub

    Sub ArrToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles ArrToolStripMenuItem.Click
        Dim t As Item
        Dim MasTop As Integer = 10000
        For Each itm As Item In nis.Seleccionados
            t = itm
            If t.Rec.Y < MasTop Then
                MasTop = t.Rec.Y
            End If
        Next

        For Each itm As Item In nis.Seleccionados
            t = itm
            t.Rec.Y = MasTop
        Next
        Me.Refresh()


    End Sub

    Sub AbaToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles AbaToolStripMenuItem.Click
        Dim t As Item
        Dim MasTop As Integer = 0
        For Each itm As Item In nis.Seleccionados
            t = itm
            If t.Rec.Y + t.Rec.Height > MasTop Then
                MasTop = t.Rec.Y + t.Rec.Height
            End If
        Next

        For Each itm As Item In nis.Seleccionados
            t = itm
            t.Rec.Y = MasTop - t.Rec.Height
        Next
        Me.Refresh()

    End Sub

    Sub HorToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles HorToolStripMenuItem.Click
        Dim t As Item
        Dim MasTop As Integer = 0
        Dim MenosTop As Integer = 99999
        Dim AnchoTotal As Integer = 0
        Dim anchodisponible As Integer = 0
        Dim dif As Integer = 0
        Dim xx As Integer

        nis.ordenar("X")

        For Each itm As Item In nis.Seleccionados
            t = itm
            If t.Rec.X < MenosTop Then
                MenosTop = t.Rec.X
            End If
            If t.Rec.X + t.Rec.Width > MasTop Then
                MasTop = t.Rec.X + t.Rec.Width
            End If
            AnchoTotal = AnchoTotal + t.Rec.Width
        Next
        anchodisponible = MasTop - MenosTop
        dif = CInt(Math.Round(((anchodisponible - AnchoTotal) / (nis.Seleccionados.Count - 1)), 0))

        xx = MenosTop

        For Each itm As Item In nis.Seleccionados
            t = itm
            t.Rec.X = xx
            xx = t.Rec.X + t.Rec.Width + dif
        Next
        Me.Refresh()

    End Sub

    Sub VerToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles VerToolStripMenuItem.Click
        Dim t As Item
        Dim MasTop As Integer = 0
        Dim MenosTop As Integer = 9999
        Dim altoTotal As Integer = 0
        Dim Altodisponible As Integer = 0
        Dim dif As Integer = 0
        Dim yy As Integer

        nis.ordenar("Y")

        For Each itm As Item In nis.Seleccionados
            t = itm
            If t.Rec.Y < MenosTop Then
                MenosTop = t.Rec.Y
            End If
            If t.Rec.Y + t.Rec.Height > MasTop Then
                MasTop = t.Rec.Y + t.Rec.Height
            End If
            altoTotal = altoTotal + t.Rec.Height
        Next
        Altodisponible = MasTop - MenosTop
        dif = CInt(Math.Round(((Altodisponible - altoTotal) / (nis.Seleccionados.Count - 1)), 0))

        yy = MenosTop

        For Each itm As Item In nis.Seleccionados
            t = itm
            t.Rec.Y = yy
            yy = t.Rec.Y + t.Rec.Height + dif
        Next
        Me.Refresh()

    End Sub


    Sub Panel1Click(ByVal sender As Object, ByVal e As EventArgs) Handles panel1.Click
        ClearSel()
        treeView1.Focus()
        treeView1.SelectedNode = treeView1.Nodes(0)
    End Sub

    Sub RemoveImageToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles removeImageToolStripMenuItem.Click
        optionPanel.RemoveImage()
    End Sub


    'Private Sub addlabel_MouseDown(sender As Object, e As MouseEventArgs) Handles addlabel.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("Label", DragDropEffects.Copy)
    'End Sub

    'Private Sub AddImage_MouseDown(sender As Object, e As EventArgs) Handles AddImage.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("Image", DragDropEffects.Copy)
    'End Sub

    'Private Sub Addcodebar_MouseDown(sender As Object, e As EventArgs) Handles Addcodebar.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("Barcode", DragDropEffects.Copy)
    'End Sub

    'Private Sub addfield_MouseDown(sender As Object, e As EventArgs) Handles addfield.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("DataLabel", DragDropEffects.Copy)
    'End Sub

    'Private Sub addFieldImage_MouseDown(sender As Object, e As EventArgs) Handles addFieldImage.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("DataImage", DragDropEffects.Copy)
    'End Sub

    'Private Sub AddFieldCodebar_MouseDown(sender As Object, e As EventArgs) Handles AddFieldCodebar.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("Fbarcode", DragDropEffects.Copy)
    'End Sub



    'Private Sub AddShape_MouseDown(sender As Object, e As MouseEventArgs) Handles AddShape.MouseDown
    '    Dim btnPic As Button = New Button
    '    btnPic.DoDragDrop("Shape", DragDropEffects.Copy)
    'End Sub

    Private Sub addlabel_MouseDown(sender As Object, e As MouseEventArgs) Handles btexto.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Label", DragDropEffects.Copy)
    End Sub

    Private Sub AddImage_MouseDown(sender As Object, e As EventArgs) Handles bimagen.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Image", DragDropEffects.Copy)
    End Sub

    Private Sub Addcodebar_MouseDown(sender As Object, e As EventArgs) Handles bcodigobarras.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Barcode", DragDropEffects.Copy)
    End Sub

    Private Sub addfield_MouseDown(sender As Object, e As EventArgs) Handles bcampo.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("DataLabel", DragDropEffects.Copy)
    End Sub

    Private Sub addFieldImage_MouseDown(sender As Object, e As EventArgs) Handles bImagencampo.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("DataImage", DragDropEffects.Copy)
    End Sub

    Private Sub AddFieldCodebar_MouseDown(sender As Object, e As EventArgs) Handles bcodigobarrast.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Fbarcode", DragDropEffects.Copy)
    End Sub

    Private Sub bacercade_Click(sender As Object, e As EventArgs) Handles bacercade.Click
        'Help
        Dim f As New Help
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub bConfiguracion_Click(sender As Object, e As EventArgs) Handles bConfiguracion.Click
        Dim oFrm As FrmPropiedadesdocumento = New FrmPropiedadesdocumento(oFmto)

        If oFrm.ShowDialog = DialogResult.OK Then
            oFmto = oFrm.propiedades
            If oFmto.a1.Trim = "apaisado" Then
                If Not printset.DefaultPageSettings.Landscape Then
                    printset.DefaultPageSettings.Landscape = True
                    Reshape()
                End If
            Else
                If printset.DefaultPageSettings.Landscape Then
                    printset.DefaultPageSettings.Landscape = False
                    Reshape()
                End If
            End If
        End If

    End Sub

    Private Sub bNuevo_Click(sender As Object, e As EventArgs) Handles bNuevo.Click
        Dim oFrm As FrmNuevoformato = New FrmNuevoformato
        Dim aRet() As String
        Dim TRP As Report

        oFrm.ObjetoGlobal = ObjetoGlobal
        oFrm.cargar_datos()
        If oFrm.ShowDialog = DialogResult.OK Then
            aRet = oFrm.retorno
            treeView1.Nodes.Clear()
            TRP = nuevoformato(aRet)
            RP = TRP
            Me.Refresh()
            ShowProperty(RP)
            ShowTree()
            savePending = False
            Me.Refresh()
            Done()
            Reshape()
            Formatonuevo = True
        Else
            MsgBox("No hace nada")
        End If

    End Sub

    Private Sub bprevia_Click(sender As Object, e As EventArgs) Handles bprevia.Click
        'Dim pp As New PPreview
        'pp.ShowDialog
        'pp.Dispose
        'exit sub
        'Sample Items
        'Dim detailData As New DataTable
        'detailData.Columns.Add("id")
        'detailData.Columns.Add("name")
        'detailData.Columns.Add("qty")
        'detailData.Columns.Add("image")
        'For i As Integer = 0 To 100
        '    detailData.Rows.Add()
        '    detailData.Rows(i).Item("id") = i
        '    detailData.Rows(i).Item("name") = "Pitzza Hut " & i
        '    detailData.Rows(i).Item("qty") = 5
        '    ' detailData.Rows(i).Item("image") = ImageToBase64(picturebox6.Image, ImageFormat.Jpeg)
        'Next

        Using p As New PrintDesign(RP)
            'Dim secnx As Section = RP.Sections(2)
            'secnx.dt = detailData
            p.PrintReport(True)
        End Using

    End Sub

    Private Sub AddShape_MouseDown(sender As Object, e As MouseEventArgs) Handles bfigura.MouseDown
        Dim btnPic As Button = New Button
        btnPic.DoDragDrop("Shape", DragDropEffects.Copy)
    End Sub

    Sub PictureBox2DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles pictureBox2.DragDrop
        Dim p As Point = pictureBox2.PointToClient(Cursor.Position)
        oldx = p.X
        oldy = p.Y

        Dim yy As Integer = 0
        Dim x As Integer = 0
        Dim R As Rectangle
        Dim secn As Section
        Dim ni As Object
        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            If secn.state <> 2 Then
                If secn.state = 1 Then
                    R = New Rectangle(0, yy + SepHeight, RP.w, secn.h)
                Else
                    R = New Rectangle(0, yy, RP.w, secn.h)
                End If
                Dim y As Integer = 0
                If R.Contains(New Point(oldx, oldy)) Then
                    x = p.X
                    y = p.Y - R.Y
                    If y >= 0 Then
                        Dim itype As String = e.Data.GetData(DataFormats.Text).ToString
                        Select Case itype
                            Case "Label"
                                ni = New LabelItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Shape"
                                ni = New shapeItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Image"
                                ni = New ImageItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "DataLabel"
                                ni = New DataLabel(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "DataImage"
                                ni = New DataImage(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Barcode"
                                ni = New BarcodeItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype
                            Case "Fbarcode"
                                ni = New BarcodeFieldItem(x, y, 20, 15)
                                ni.nm = NewItem(itype, secn.AR)
                                ni.itemtype = itype

                        End Select
                        secn.AR.Add(ni)
                        si = ni
                        ShowProperty(si)
                        ShowTree()
                        pictureBox2.Invalidate()
                        Done()
                    End If
                End If
            End If

            If secn.state < 3 Then
                yy += SepHeight
            End If
            If secn.state <> 2 Then
                yy += secn.h
            End If
        Next
    End Sub

    Sub PictureBox2DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles pictureBox2.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Sub PictureBox2MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox2.MouseUp
        pictureBox3.Cursor = Cursors.Default
        ResType = 0
        WaitTime = 0
        OldP = Nothing
        If e.Button = MouseButtons.Left Then
            If actionDone Then
                Done()
                actionDone = False
            End If
        Else

        End If
    End Sub

    Sub PictureBox2MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pictureBox2.MouseMove
        Dim p As Point = pictureBox2.PointToClient(Cursor.Position)
        Dim max As Integer = 0

        oldx = p.X
        oldy = p.Y

        'Dim yy As Integer = 0
        'Dim x As Integer = 0
        'Dim R As Rectangle
        'Dim secn As Section

        'For i As Integer = 0 To RP.Sections.Count - 1
        '    secn = RP.Sections(i)
        '    If secn.state <> 2 Then
        '        If secn.state = 1 Then
        '            R = New Rectangle(0, yy + SepHeight, RP.w + 20, secn.h + SepHeight)
        '        Else
        '            R = New Rectangle(0, yy, RP.w + 20, secn.h + SepHeight)
        '        End If
        '        Dim y As Integer = 0
        '        'Test if in section
        '        If R.Contains(New Point(oldx, oldy)) Then
        '            x = p.X
        '            y = p.Y - R.Y
        '            If e.Button = Windows.Forms.MouseButtons.Left Then
        '                If Not (si Is Nothing) And selsecn = i Then
        '                    Dim ItemRec As Rectangle
        '                    ItemRec = New Rectangle(si.Rec.Location, si.Rec.Size)
        '                    If WaitTime > 0 Then
        '                        ItemRec.Height += 500
        '                        ItemRec.Width += 500
        '                    Else
        '                        ItemRec.Height += 12
        '                        ItemRec.Width += 12
        '                    End If
        '                    '
        '                    If 1 = 1 Then
        '                        WaitTime += 1
        '                        If pictureBox2.Cursor = Cursors.Hand Then
        '                            'goto toend
        '                            si.Rec.Location = oldLocation + (p - OldP)
        '                            If si.Rec.X < -1 Then
        '                                si.Rec.X = -1
        '                            End If
        '                            If si.Rec.Y < -1 Then
        '                                si.Rec.Y = -1
        '                            End If
        '                            'goto toend
        '                        End If
        '                        actionDone = True
        '                        pictureBox2.Refresh()
        '                        pictureBox3.Refresh()
        '                        'goto toend
        '                    End If

        '                End If
        '            Else
        '                For k As Integer = 0 To secn.AR.Count - 1
        '                    Dim t As Item
        '                    t = secn.AR(k)
        '                    If t.selected Then

        '                        Dim HW, HH As Integer
        '                        Dim Hx, Hy As Integer
        '                        If si.Rec.Width - 30 < 10 Then
        '                            HW = 10
        '                        Else
        '                            HW = si.Rec.Width - 30
        '                        End If
        '                        If si.Rec.Height - 30 < 10 Then
        '                            HH = 10
        '                        Else
        '                            HH = si.Rec.Height - 30
        '                        End If
        '                        Dim handPad As Integer = Math.Ceiling(si.bw / 2) - 1
        '                        If si.bw < 1 Then
        '                            handPad = 0
        '                        End If
        '                        Hx = si.Rec.X + (si.Rec.Width - HW) / 2
        '                        Hy = si.Rec.Y + si._h + (si.Rec.Height - HH) / 2
        '                        Dim R1 As New Rectangle(Hx, si.Rec.Y + si.Rec.Height + si._h + handPad, HW, 10)
        '                        Dim R2 As New Rectangle(si.Rec.X + si.Rec.Width + handPad, Hy, 10, HH)
        '                        Dim R3 As New Rectangle(si.Rec.X + si.Rec.Width - 1 + handPad, si.Rec.Y + si.Rec.Height - 1 + si._h + handPad, 10, 10)

        '                        If New Rectangle(si.Rec.X - handPad, si.Rec.Y - handPad, si.Rec.Width + 2 * handPad, si.Rec.Height + 2 * handPad).Contains(New Point(x, y)) Then
        '                            pictureBox2.Cursor = Cursors.Hand
        '                            ResType = 0
        '                        Else
        '                            pictureBox2.Cursor = Cursors.Default
        '                        End If
        '                    End If
        '                Next
        '            End If
        '            'label2.Text = X & ", " & Y        			
        '        End If
        '    End If

        '    If secn.state < 3 Then
        '        yy += SepHeight
        '    End If
        '    If secn.state <> 2 Then
        '        yy += secn.h
        '    End If
        'Next
        '' Desde aquí
        'If e.Button = Windows.Forms.MouseButtons.Left Then
        '    'dim p as Point = PictureBox3.PointToClient(Cursor.Position)
        '    oldx = e.X
        '    oldy = e.Y

        Dim y As Integer = 0
        Dim R As Rectangle
        Dim secn As Section
        Dim hh As Integer = 0

        max = 0

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            R = New Rectangle(0, y, RP.w, secn.h + SepHeight)

            max += secn.h + SepHeight

            If R.Contains(New Point(oldx, oldy)) Then
                'ShowProperty(secn)
                R = New Rectangle(4, y + 2, SepHeight - 4, SepHeight - 5)
                If R.Contains(New Point(oldx, oldy)) Then
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If i > 0 Then
                            secn = RP.Sections(i - 1)
                            hh = secn.h - (oldLocation.Y - oldy)
                            If hh > 0 Then
                                secn.h = hh
                            End If
                            'ElseIf i < 2 And i < RP.Sections.Count Then
                            '    If hh < RP.Sections(i).h Then
                            '        secn.h = hh
                            '    End If
                            'ElseIf i > 2 And i >= RP.Sections.Count Then
                            '    If hh < RP.Sections(i).h Then
                            '        secn.h = hh
                            '    End If
                            'Else
                            '   secn.h = hh
                            '    End If

                            oldLocation.Y = oldy '(oldLocation.Y - oldy)
                            'secn.Alto += 0.5
                            coordenadas.Text = "" & oldLocation.X & "," & oldLocation.Y
                            Application.DoEvents()
                        End If
                    Else
                        pictureBox2.Cursor = Cursors.Hand
                        oldLocation = New Point(oldx, oldy)
                    End If
                    Me.Refresh()
                    Exit For
                End If
            Else
                pictureBox2.Cursor = Cursors.Default
            End If
            If secn.state < 3 Then
                y += SepHeight
            End If
            If secn.state <> 2 Then
                y += secn.h
            End If
        Next
toend:
    End Sub


    Private Sub Cargarcombofuentes()
        'Ancho por defecto para el ComboBox
        ComboBox_fuentes.Width = 200

        Dim font_family As FontFamily
        Dim installed_fonts As New InstalledFontCollection

        'Limpiamos el ComboBox en caso que tenga algo
        ComboBox_fuentes.Items.Clear()

        'cargamos las fuentes (como items)en el ComboBox
        For Each font_family In FontFamily.Families
            ComboBox_fuentes.Items.Add(font_family.Name)
        Next
        '************************************************

        'Fuente Seleccionada por Defecto (Opcional)
        ComboBox_fuentes.SelectedItem = "Microsoft Sans Serif"


    End Sub

    Private Sub AddImage_MouseDown(sender As Object, e As MouseEventArgs) Handles bimagen.MouseDown

    End Sub

    Private Sub Addcodebar_MouseDown(sender As Object, e As MouseEventArgs) Handles bcodigobarras.MouseDown

    End Sub

    Private Sub addfield_MouseDown(sender As Object, e As MouseEventArgs) Handles bcampo.MouseDown

    End Sub

    Private Sub addFieldImage_MouseDown(sender As Object, e As MouseEventArgs) Handles bImagencampo.MouseDown

    End Sub

    Private Sub AddFieldCodebar_MouseDown(sender As Object, e As MouseEventArgs) Handles bcodigobarrast.MouseDown

    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        Dim oDlg As SeleccionCampos
        Dim secn As Section
        Dim ni As Object

        If Trim(Nodoseleccionado()) = "" Then
            MsgBox("Selecciona una sección en el informe")
            Exit Sub
        End If


        oDlg = New SeleccionCampos
        oDlg.seccion = Nodoseleccionado()
        oDlg.og = ObjetoGlobal
        oDlg.datos = dtDatos
        oDlg.Tablas = dtTablas
        oDlg.cargartabla()
        oDlg.ShowDialog()

        If oDlg.Correcto Then
            dtDatos = oDlg.datos
            If oDlg.Resultado.Trim <> "" Then

                If MsgBox("Cambiar actualizar campo en el Item seleccionado", MsgBoxStyle.OkCancel And MsgBoxStyle.Question) = MsgBoxResult.Ok Then
                    CambiarenSeleccionado(oDlg.Resultado.Trim)
                    Return
                End If

                For i As Integer = 0 To RP.Sections.Count - 1
                    secn = RP.Sections(i)
                    If secn.selected Then
                        If NuevoItem(oDlg.Resultado.Trim, secn.AR) <> "" Then

                            If oDlg.Item = "im" Then
                                ni = New DataImage(5, 5, 100, 15)
                                ni.itemtype = "DataImage"
                            ElseIf oDlg.Item = "cb" Then
                                ni = New BarcodeFieldItem(5, 5, 100, 15)
                                ni.itemtype = "Fbarcode"
                            Else
                                ni = New DataLabel(5, 5, 100, 15)
                                ni.itemtype = "DataLabel"
                            End If

                            ni.nm = NewItem(oDlg.Resultado.Trim, secn.AR)

                            ni.DataField = oDlg.Resultado.Trim
                            'ni.name = oDlg.Resultado.Trim
                            MaxItems = MaxItems + 1
                            ni.NumeroItem = MaxItems
                            secn.AR.Add(ni)
                            ni.refresh()
                            si = ni
                            QuitarSeleccionado()
                            ShowProperty(si)
                            ShowTree()
                            ShowOptions()
                            SelectNode(ni.nm, i)
                            si.selected = True ' a ver que pasa
                            si.Refresh()
                            secn.Refresh()
                        End If
                    End If
                Next
            End If
        End If


    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        Dim oDlg As Variables
        Dim secn As Section
        Dim ni As Object

        If trim(Nodoseleccionado()) = "" Then
            MsgBox("Selecciona una sección en el informe")
            Exit Sub
        End If

        oDlg = New Variables
        oDlg.datos = dtVariables
        oDlg.seccion = Nodoseleccionado()
        oDlg.cargartabla()
        oDlg.ShowDialog()
        If oDlg.Resultado.Trim <> "" Then
            dtVariables = oDlg.datos
            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)
                If secn.selected Then
                    If NuevoItem(oDlg.Resultado.Trim, secn.AR) <> "" Then
                        If oDlg.Item = "im" Then
                            ni = New DataImage(5, 5, 100, 15)
                            ni.itemtype = "DataImage"
                        ElseIf oDlg.Item = "cb" Then
                            ni = New BarcodeFieldItem(5, 5, 100, 15)
                            ni.itemtype = "Fbarcode"
                        Else
                            ni = New DataLabel(5, 5, 100, 15)
                            ni.itemtype = "DataLabel"
                        End If
                        'ni.nm = NewItem(oDlg.Resultado.Trim, secn.AR)
                        ni.nm = oDlg.Resultado.Trim
                        'ni.itemtype = "DataLabel"
                        ni.DataField = oDlg.Resultado.Trim
                        'ni.name = oDlg.Resultado.Trim
                        MaxItems = MaxItems + 1
                        ni.NumeroItem = MaxItems
                        secn.AR.Add(ni)
                        ni.refresh()
                        si = ni
                        QuitarSeleccionado()
                        si.selected = True ' a ver que pasa
                        si.Refresh()
                        ShowProperty(si)
                        ShowTree()
                        ShowOptions()
                        SelectNode(ni.nm, i)
                        secn.Refresh()
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub ComboBox_fuentes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_fuentes.SelectedIndexChanged
        Dim nS As Integer
        Dim nt As Integer
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            If Not si Is Nothing Then
                If si.ItemType = "Label" Or si.ItemType = "DataLabel" Or si.ItemType = "Barcode" Or si.ItemType = "FBarcode" Then
                    nS = si.fnt.Size
                    nS = si.fnt.Style
                    si.fnt = New Font(ComboBox_fuentes.Text, si.fnt.Size, si.fnt.Style) ' = stringToFont(ComboBox_fuentes.SelectedItem)
                    si.Refresh()
                End If
            End If
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    oitm.Font = New Font(ComboBox_fuentes.Text, oitm.Font.Size, oitm.Font.Style)
                End If
            Next
        End If

    End Sub

    Public Function Inicializar(ByVal pProceso As String, ByVal pDocumento As String, ByVal pFormato As String, ByVal pModoSeleccionImpresora As Integer, ByVal pPrevisualizar As Boolean, ByVal cnn As System.Data.SqlClient.SqlConnection, ByRef XProceso As String, ByRef XDocumento As String, ByRef XFormato As String, ByRef DCT As Dictionary(Of String, Integer), ByVal pImpresoraSistema As String, Optional ByVal pEmpresa As String = "", Optional ByVal ImpresoraFija As String = "") As Boolean
        cn = cnn
    End Function

    Private Sub SeleccionarTablas_Click(sender As Object, e As EventArgs) Handles SeleccionarTablas.Click
        Dim oDlg As Selecciontablas
        If Trim(Nodoseleccionado()) = "" Then
            MsgBox("Selecciona una sección en el informe")
            Exit Sub
        End If
        oDlg = New Selecciontablas
        oDlg.seccion = Nodoseleccionado()
        oDlg.og = ObjetoGlobal
        oDlg.datos = dtTablas
        oDlg.cargartabla()
        If oDlg.ShowDialog() = DialogResult.OK Then
            dtTablas = oDlg.datos
        End If

    End Sub

    Private Function Nodoseleccionado() As String
        'Dim node As TreeNode = treeView1.SelectedNode

        'If (Not (node Is Nothing)) Then
        '    Dim text As String = node.Text
        '    Return text
        'End If
        'Return ""

        Return SecSelec

    End Function

    Private Sub QuitarSeleccionado()
        Dim secn As Section

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)

            For k As Integer = 0 To secn.AR.Count - 1
                Dim t As Item
                t = secn.AR(k)
                If t.selected Then
                    t.selected = False
                End If
            Next

        Next

    End Sub

    Private Sub CambiarenSeleccionado(cField)
        Dim secn As Section

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)

            For k As Integer = 0 To secn.AR.Count - 1
                Dim t As Item
                t = secn.AR(k)
                If t.selected Then
                    t.Dfield = cField
                End If
            Next

        Next

    End Sub

    Private Function CargarInforme(ByVal aRet() As String) As Report
        Dim TRP As New Report
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim cZona As String
        Dim cValor As String
        Dim t As Item
        Dim secciones() As String = {"cabecera0", "cabecera1", "cuerpo0", "cuerpo1", "pie0", "pie1"}
        Dim i As Integer
        Dim alinear As String
        Dim secn As Section
        Dim haySeccion As Boolean = False
        Dim numdato As Integer = 0
        Dim numerolinea As Integer
        Dim numeroimagen As Integer
        Dim nEstilo As Integer
        Dim nFontSize As Double

        dtDatos.Reset()

        dtDatos.Columns.Add(New DataColumn("valor", GetType(String)))
        dtDatos.Columns.Add(New DataColumn("seccion", GetType(String)))

        dtTablas.Reset()
        dtTablas.Columns.Add(New DataColumn("valor", GetType(String)))
        dtTablas.Columns.Add(New DataColumn("seccion", GetType(String)))

        dtVariables.Reset()
        dtVariables.Columns.Add(New DataColumn("valor", GetType(String)))
        dtVariables.Columns.Add(New DataColumn("seccion", GetType(String)))

        'dtVariables.Rows.Add("[fecha]")
        'dtVariables.Rows.Add("[fechalarga]")
        'dtVariables.Rows.Add("[pagina]")
        'dtVariables.Rows.Add("[paginas]")
        'dtVariables.Rows.Add("[registros()]")

        'oFmto.CargaFormato(Proceso, aRet(0), aRet(1))

        secciones = ObtenerSecciones(aRet)

        conversion = True

        Try
            If UBound(aRet) > 1 Then
                nuevoproceso = IIf(aRet(2) = "Sí", True, False)
                nuevodocumento = IIf(aRet(3) = "Sí", True, False)
            End If
            TRP.documento = aRet(0)
            TRP.formato = aRet(1)
            TRP.nm = aRet(1)
            oFmto.CargaFormato("", aRet(0), aRet(1))
            Proceso = oFmto.proceso
            savePending = True

            For i = 0 To UBound(secciones) '- 1 '5
                'If i = 0 Or i = 2 Or i = 4 Then
                cZona = secciones(i)
                dtVariables.Rows.Add("[fecha]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[fechalarga]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[pagina]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[paginas]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[registros()]", Strings.Left(cZona, cZona.Length - 1))
                'End If
            Next

            Norefrescar = True

            Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = 'BASICO' ORDER BY zona, valor"

            ' Cargamos primero las variables
            If Rs.Open(Sql, ObjetoGlobal.cn) Then
                While Not Rs.EOF
                    ' Ahora cargamos las tablas
                    If Trim(cZona) <> Trim(Rs!zona) Then
                        cZona = Trim(Rs!zona)
                        If InStr(Rs!valor, ".") = 0 And InStr(Rs!valor, "@") = 0 Then
                            cValor = "calculado." & Trim(Rs!valor)
                        ElseIf InStr(Rs!valor, "@") > 0 Then
                            cValor = "@"
                        Else
                            cValor = Trim(Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1))
                        End If

                        If Trim(cValor) = "calculado" Then
                            'dtVariables.Rows.Add(Rs!valor, cZona)
                            dtVariables.Rows.Add(Rs!valor, cZona)
                            cValor = ""
                        ElseIf cValor = "@" Then
                            dtVariables.Rows.Add(Rs!valor, cZona)
                            cValor = ""
                        Else
                            dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                            dtTablas.Rows.Add(cValor, cZona)
                        End If
                    Else
                        If InStr(Rs!valor, "@") > 0 Then
                            dtVariables.Rows.Add(Rs!valor, cZona)
                            cValor = ""
                        ElseIf Trim(cValor) <> Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1) Then
                            cValor = Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1)
                            If Trim(cValor) = "calculado" Then
                                dtVariables.Rows.Add(Rs!valor, cZona)
                                cValor = ""
                            Else
                                dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                                dtTablas.Rows.Add(cValor, cZona)
                            End If
                        Else
                            If Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1) = "calculado" Then
                                    dtVariables.Rows.Add(Rs!valor)
                                Else
                                    dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                                End If
                            End If
                        End If
                        Rs.MoveNext()
                End While
            End If
            Rs.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ' sacamos los scripts si los hubiere
        Try
            SacarScripts(aRet(0), aRet(1))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ' vemos las secciones
        Try
            'For i = 0 To 5
            For i = 0 To UBound(secciones) '- 1

                haySeccion = False
                cZona = secciones(i)

                Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo = 'banda'  ORDER BY 1,2,3,4,5"
                If Rs.Open(Sql, ObjetoGlobal.cn) Then



                    If Not Rs.EOF Then

                        If Trim("" & Rs!unidades) = "px" And conversion Then
                            conversion = False
                        End If

                        secn = New Section

                        If Strings.Left(cZona, cZona.Length - 1) = "cuerpo" Then
                            secn = New SectionDetail
                        Else
                            secn = New Section
                        End If
                        secn.nm = Strings.Left(cZona, cZona.Length - 1)
                        secn.headerLabel = Strings.Left(cZona, cZona.Length - 1)
                        secn.h = ConvertirPixelesPuntos(Rs!alto)
                        secn.indice = Rs!indice
                        secn.GroupID = i
                        haySeccion = True

                        TRP.Sections.Add(secn)
                    End If
                End If

                Rs.Close()
                If haySeccion Then
                    Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo <> 'banda'  ORDER BY 1,2,3,4,5"
                    If Rs.Open(Sql, ObjetoGlobal.cn) Then
                        While Not Rs.EOF

                            numdato = numdato + 1

                            Dim rec As New Rectangle(ConvertirPixelesPuntos(Rs!izquierda), ConvertirPixelesPuntos(Rs!tope), ConvertirPixelesPuntos(Rs!ancho), ConvertirPixelesPuntos(Rs!alto))
                            Select Case Trim(Rs!tipo)
                                Case "etiqueta"
                                    t = New LabelItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                Case "rectangulo"
                                    t = New shapeItem(rec)
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.FromArgb(255, 255, 255), Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea
                                    t.nm = "Rectangulo"

                                Case "linea"
                                    rec = New Rectangle(ConvertirPixelesPuntos(Rs!tope), ConvertirPixelesPuntos(Rs!alto), ConvertirPixelesPuntos(Rs!izquierda - Rs!tope), ConvertirPixelesPuntos(Rs!ancho - Rs!alto))

                                    t = New shapeItem(rec)

                                    If Rs!izquierda - Rs!tope = 0 Then
                                        t.tipo = 3
                                        't.Alto = 1
                                    ElseIf Rs!ancho - Rs!alto = 0 Then
                                        t.tipo = 2
                                        't.Ancho = 1
                                    ElseIf (Rs!tope - Rs!izquierda) < (Rs!ancho - Rs!alto) Then
                                        t.tipo = 4
                                        't.Ancho = 1
                                    Else
                                        t.tipo = 4
                                        't.Alto = 1
                                    End If

                                    t.BorderWidth = 1
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea

                                    numerolinea = numerolinea + 1
                                    t.nm = "Línea" & numerolinea
                                    t.txt = ""

                                Case "elipse"
                                    t = New shapeItem(rec)
                                    t.tipo = 1
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea
                                    t.nm = "Elipse"
                                    t.txt = ""

                                Case "dibujo"
                                    numeroimagen = numeroimagen + 1
                                    t = New ImageItem(rec)
                                    t.nm = If("" & Rs!valor = "", "Imagen" & numeroimagen, Rs!valor)
                                    t.Ancho_del_borde = 0.025
                                    t.txt = ""

                                Case "campo"

                                    If InStr("" & Rs!valor, "cb39_") Then
                                        t = New BarcodeFieldItem(rec)
                                        t.txt = "" & Rs!valor
                                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                        t.nm = "" & Rs!valor
                                        t.Ancho_del_borde = 0.025
                                        t.Drawbarcode = Item.SBarcode.Barcode39
                                    Else
                                        t = New DataLabel(rec)
                                        t.txt = "" '& Rs!valor
                                        t.fmt = If(IsDBNull(Rs!formatosal), "" & Rs!tag, Trim(Rs!formatosal))
                                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                        t.nm = "" & Rs!valor
                                        t.Ancho_del_borde = 0.025
                                    End If

                                Case "dimage"
                                    t = New DataImage(rec)
                                    t.txt = "" '& Rs!valor
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                Case "barcode"
                                    t = New BarcodeItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025
                                    Select Case Trim("" & Rs!cod_barras)
                                        Case "39"
                                            t.Drawbarcode = Item.SBarcode.Barcode39
                                        Case "QR"
                                            t.Drawbarcode = Item.SBarcode.BarcodeQR
                                        Case "128"
                                            t.Drawbarcode = Item.SBarcode.Barcode128
                                        Case "EAN13"
                                            t.Drawbarcode = Item.SBarcode.BarcodeEAN13
                                    End Select


                                Case "dbarcode"
                                    t = New BarcodeFieldItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                    Select Case Trim("" & Rs!cod_barras)
                                        Case "39"
                                            t.Drawbarcode = Item.SBarcode.Barcode39
                                        Case "QR"
                                            t.Drawbarcode = Item.SBarcode.BarcodeQR
                                        Case "128"
                                            t.Drawbarcode = Item.SBarcode.Barcode128
                                        Case "EAN13"
                                            t.Drawbarcode = Item.SBarcode.BarcodeEAN13
                                    End Select

                                Case Else
                                    t = New LabelItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025
                            End Select
                            ' "name"

                            t.TruncarTexto = False
                            If IsDBNull(Rs!autosize) Then
                                t.fitInBox = False
                            ElseIf Rs!autosize.trim = "T" Then
                                t.TruncarTexto = True
                            Else
                                t.fitInBox = IIf(Rs!autosize.trim = "N", False, True)
                            End If

                            't.nm = "" & Rs!valor
                            ' "text"

                            't.txt = "" & Rs!valor
                            ' -1;Bordercolor:-16777216;BorderRadius:0;BorderWidth:0
                            ' "backcolor"
                            t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                            ' "bordercolor"
                            t.linecolor = If(IsDBNull(Rs!color_borde), Color.FromArgb(0, 0, 0), Color.FromArgb(Rs!color_borde))
                            ' "borderradius"
                            t.BorderRad = If(IsDBNull(Rs!radio_borde), 0, ConvertirPixelesPuntos(Rs!radio_borde))
                            ' "borderwidth"
                            't.bw = If(IsDBNull(Rs!ancho_borde), 0.25, convertirPixelesPuntos(Rs!ancho_borde))
                            ' "borderside"
                            t.RecSides = If(IsDBNull(Rs!ancho_borde), CType(15, AnchorStyles), CType(ConvertirPixelesPuntos(Rs!borderside), AnchorStyles))
                            ' "font"
                            't.fnt = stringToFont(if(IsDBNull(Rs!fuente), "Arial; 8pt", Rs!fuente & "; " & If(IsDBNull(Rs!tamanof), 8, Rs!tamanof) & "pt"))
                            t.fnt = stringToFont(If(IsDBNull(Rs!fuente), "Arial", Rs!fuente))
                            nEstilo = 0
                            If Not IsDBNull(Rs!fuente) Then
                                nFontSize = If(IsDBNull(Rs!tamanof), 8.25, Convert.ToDecimal(Replace(Rs!tamanof, ".", ",")))
                                If "" & Rs!negrita <> "" Then
                                    If Rs!negrita = -1 Then
                                        nEstilo = 1
                                    End If
                                End If
                                If "" & Rs!cursiva <> "" Then
                                    If Rs!cursiva = -1 Then
                                        nEstilo = nEstilo + 2
                                    End If
                                End If
                                If "" & Rs!subrayado <> "" Then
                                    If Rs!subrayado = -1 Then
                                        nEstilo = nEstilo + 4
                                    End If
                                End If
                                'If nFontSize <> 0 Then
                                t.fnt = CreateFont(t.fnt.FontFamily.Name, Convert.ToSingle(nFontSize), nEstilo)
                                'End If
                            End If

                            ' "fontcolor"
                            t.fcolor = If(IsDBNull(Rs!color), Color.FromArgb(0, 0, 0), Color.FromArgb(Rs!color))

                            alinear = Trim(If(IsDBNull(Rs!justificado), "I", Rs!justificado))

                            ' "textalign"
                            Select Case alinear
                                Case "I"
                                    t.txtalign = ContentAlignment.MiddleLeft '= 16
                                Case "D"
                                    t.txtalign = ContentAlignment.MiddleRight '= 64
                                Case "C"
                                    t.txtalign = ContentAlignment.MiddleCenter '= 32
                                Case "1"
                                    t.txtalign = ContentAlignment.TopLeft '= 1
                                Case "2"
                                    t.txtalign = ContentAlignment.TopCenter '= 2
                                Case "3"
                                    t.txtalign = ContentAlignment.TopRight '= 4
                                Case "4"
                                    t.txtalign = ContentAlignment.MiddleLeft '= 16
                                Case "5"
                                    t.txtalign = ContentAlignment.MiddleCenter '= 32
                                Case "6"
                                    t.txtalign = ContentAlignment.MiddleRight '= 64
                                Case "7"
                                    t.txtalign = ContentAlignment.BottomLeft '= 256
                                Case "8"
                                    t.txtalign = ContentAlignment.BottomCenter '= 512
                                Case "9"
                                    t.txtalign = ContentAlignment.BottomRight '= 1024
                                Case Else
                                    t.txtalign = ContentAlignment.MiddleLeft '= 16
                            End Select
                            ' "datafield"
                            t.Dfield = "" & Rs!valor
                            t.DIndex = Rs!indice
                            If Trim("" & Rs!codigo_imagen) <> "" Then
                                t.img = ObtenImagen(Rs!codigo_imagen)
                                t.codimg = "" & Rs!codigo_imagen
                            Else
                                t.img = Nothing
                                t.codimg = ""
                            End If
                            t.NumeroItem = numdato
                            MaxItems = numdato
                            secn.AR.Add(t)

                            Rs.MoveNext()
                        End While
                    End If
                    Rs.Close()
                End If
            Next
            Norefrescar = False

            Return TRP

        Catch ex As Exception
            MsgBox(ex.Message)
            'Resume
        End Try

    End Function

    Private Sub construirTablas()

        ' Tablas
        dtTablas = New DataTable()
        dtTablas.Columns.Add("tabla")
        dtTablas.Columns.Add("seccion")

        ' Campos
        dtDatos = New DataTable()
        dtDatos.Columns.Add("nombre")
        dtDatos.Columns.Add("seccion")
        'dtDatos.Columns.Add("tipo")
        'dtDatos.Columns.Add("Longitud")
        'dtDatos.Columns.Add("decimales")
        'dtDatos.Columns.Add("mascara")

        ' Variables
        dtVariables = New DataTable()
        dtVariables.Columns.Add("nombre")
        'dtVariables.Columns.Add("seccion")

    End Sub


    'Public Sub VolcarAImpresion(NumeroDocumento As Long, Zona As Long, Indice As Long, Registro As Long, Campo As String, Linea As Long, Valor As String, Optional PuedeSerRectificacion As Boolean = False, Optional AnularOriginal As Boolean = False)

    'End Sub
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "cabeceras_salida.destinatario",       0, "", True, True
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "cabeceras_salida.nombre_dest",        0, "", True, True
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "cabeceras_salida.nif_dest",           0, "", True, True
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "cabeceras_salida.domicilio_dest",     0, "", True, True
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "cabeceras_salida.domicilio_dest_2",   0, "", True, True
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "cabeceras_salida.codigo_postal_dest", 0, "", True, True
    '    CINF.VolcarAImpresion 1, 0, -1, 0, "Calculado.textoprecio",               0, "Precio"
    ' Detalle
    '    CINF.VolcarAImpresion 1, 1, 0, RsLineas_temporal!numero_linea, "temp_salidas.importe" , 0, ""                , True, True
    '    CINF.VolcarAImpresion 1, 1, 1, Linea,                          "calculado.totalpalets", 0, CStr(TotalPalets)

    Private Function ObtenImagen(codigo_imagen) As Image
        Dim Rs As New cnRecordset.CnRecordset
        Dim Sql As String
        Dim ms As MemoryStream
        Dim arrImage() As Byte
        Dim img As Image = Nothing

        Sql = "SELECT * FROM zzImagenes_p WHERE codigo_imagen = '" & codigo_imagen & "'"
        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            arrImage = CType(Rs!imagen, Byte())
            ms = New MemoryStream(arrImage)
            img = Image.FromStream(ms)
        End If
        Return img

    End Function

    Private Function ObtenImagenCampo(campo_imagen) As Image
        Dim ms As MemoryStream
        Dim arrImage() As Byte
        Dim img As Image = Nothing

        arrImage = CType(campo_imagen, Byte())
        ms = New MemoryStream(arrImage)
        img = Image.FromStream(ms)

        Return img

    End Function

    Private Function ObtenImagenArchivo(path) As Image
        Dim img As Image = Nothing

        If File.Exists(path) Then
            img = Image.FromFile(path)
        End If

        Return img

    End Function

    Private Function ClicSobreUnObjeto(ByRef p As Point, ByVal sel_secn As Integer, ByVal sel_item As Integer)
        'Dim sel_secn As Integer = -1
        'Dim sel_item As Integer = -1
        Dim fueraseleccion As Boolean
        Dim secn As Section
        Dim y As Integer
        Dim R As Rectangle
        Dim x As Integer = 0

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)
            Dim addh As Integer = 0
            For k As Integer = 0 To secn.AR.Count - 1
                Dim t As Item
                t = secn.AR(k)
                If sel_secn = i Then
                    If sel_item = k Then
                        addh = SepHeight + 1
                    End If
                End If
            Next

            If secn.state <> 2 Then
                x = 0
                If secn.state = 1 Then
                    R = New Rectangle(0, y + SepHeight, RP.w, secn.h + addh)
                Else
                    R = New Rectangle(0, y, RP.w, secn.h + addh)
                End If

                Dim inum As Integer = secn.AR.Count - 1
                For k As Integer = 0 To inum
                    If sel_secn = i And sel_item = (inum - k) Then
                        Dim t As Item = secn.AR(inum - k)
                        Dim rec As New Rectangle(t.Rec.Location, New Size(t.Rec.Width + DragMarginWidth, t.Rec.Height + DragMarginWidth))
                        If rec.Contains(p.X, p.Y - R.Y) Then
                            Return True
                        End If
                    End If
                Next

                For k As Integer = 0 To inum
                    Dim t As Item = secn.AR(inum - k)
                    t.selected = False
                    Dim rec As Rectangle
                    Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                    If t.bw < 1 Then
                        handPad = 0
                    End If

                    If sel_secn = i And sel_item = (inum - k) Then
                        rec = New Rectangle(t.Rec.Location, New Size(t.Rec.Width + DragMarginWidth + 2, t.Rec.Height + DragMarginWidth + 2))
                    Else
                        rec = t.Rec
                    End If

                    If rec.Contains(p.X, p.Y - R.Y) Then
                        Return True
                    End If
                    x = p.X
                Next
            End If
            If secn.state < 3 Then
                y += SepHeight
            End If
            If secn.state <> 2 Then
                y += secn.h
            End If
        Next
        Return False


    End Function

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        ' Alinear izquierda
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            If itm.Alineación <= 4 Then ' Está en TOP
                                itm.Alineación = 1
                            ElseIf itm.Alineación <= 64 Then ' Está en el medio
                                itm.Alineación = 16
                            ElseIf itm.Alineación > 64 Then ' Está abajo
                                itm.Alineación = 256
                            End If
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    If oitm.Alineación <= 4 Then ' Está en TOP
                        oitm.Alineación = 1
                    ElseIf oitm.Alineación <= 64 Then ' Está en el medio
                        oitm.Alineación = 16
                    ElseIf oitm.Alineación > 64 Then ' Está abajo
                        oitm.Alineación = 256
                    End If
                End If
            Next
        End If

        '' Resumen:
        'ContentAlignment.TopLeft = 1
        'ContentAlignment.TopCenter = 2
        'ContentAlignment.TopRight = 4
        'ContentAlignment.MiddleLeft = 16
        'ContentAlignment.MiddleCenter = 32
        'ContentAlignment.MiddleRight = 64
        'ContentAlignment.BottomLeft = 256
        'ContentAlignment.BottomCenter = 512
        'ContentAlignment.BottomRight = 1024



    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        ' Alinear derecha
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            If itm.Alineación <= 4 Then ' Está en TOP
                                itm.Alineación = 4
                            ElseIf itm.Alineación <= 64 Then ' Está en el medio
                                itm.Alineación = 64
                            ElseIf itm.Alineación > 64 Then ' Está abajo
                                itm.Alineación = 1024
                            End If
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    If oitm.Alineación <= 4 Then ' Está en TOP
                        oitm.Alineación = 4
                    ElseIf oitm.Alineación <= 64 Then ' Está en el medio
                        oitm.Alineación = 64
                    ElseIf oitm.Alineación > 64 Then ' Está abajo
                        oitm.Alineación = 1024
                    End If
                End If
            Next
        End If

    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        'Centrar texto
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            If itm.Alineación <= 4 Then ' Está en TOP
                                itm.Alineación = 2
                            ElseIf itm.Alineación <= 64 Then ' Está en el medio
                                itm.Alineación = 32
                            ElseIf itm.Alineación > 64 Then ' Está abajo
                                itm.Alineación = 512
                            End If
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    If oitm.Alineación <= 4 Then ' Está en TOP
                        oitm.Alineación = 2
                    ElseIf oitm.Alineación <= 64 Then ' Está en el medio
                        oitm.Alineación = 32
                    ElseIf oitm.Alineación > 64 Then ' Está abajo
                        oitm.Alineación = 512
                    End If
                End If
            Next
        End If

        '' Resumen:
        'ContentAlignment.TopLeft = 1
        'ContentAlignment.TopCenter = 2
        'ContentAlignment.TopRight = 4
        'ContentAlignment.MiddleLeft = 16
        'ContentAlignment.MiddleCenter = 32
        'ContentAlignment.MiddleRight = 64
        'ContentAlignment.BottomLeft = 256
        'ContentAlignment.BottomCenter = 512
        'ContentAlignment.BottomRight = 1024


    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        ' Alinear arriba

        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            Select Case itm.Alineación
                                Case 1, 16, 256
                                    itm.Alineación = 1
                                Case 2, 32, 512
                                    oitm.Alineación = 2
                                Case 4, 64, 1024
                                    itm.Alineación = 4
                            End Select
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    Select Case oitm.Alineación
                        Case 1, 16, 256
                            oitm.Alineación = 1
                        Case 2, 32, 512
                            oitm.Alineación = 2
                        Case 4, 64, 1024
                            oitm.Alineación = 4
                    End Select
                End If
            Next
        End If
        '1,16,256
        '            2,32,512
        '            4,64, 1024
        '' Resumen:
        'ContentAlignment.TopLeft = 1
        'ContentAlignment.TopCenter = 2
        'ContentAlignment.TopRight = 4
        'ContentAlignment.MiddleLeft = 16
        'ContentAlignment.MiddleCenter = 32
        'ContentAlignment.MiddleRight = 64
        'ContentAlignment.BottomLeft = 256
        'ContentAlignment.BottomCenter = 512
        'ContentAlignment.BottomRight = 1024


    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        ' Alinear abajo
        ' Alinear arriba

        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            Select Case itm.Alineación
                                Case 1, 16, 256
                                    itm.Alineación = 256
                                Case 2, 32, 512
                                    oitm.Alineación = 512
                                Case 4, 64, 1024
                                    itm.Alineación = 1024
                            End Select
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    Select Case oitm.Alineación
                        Case 1, 16, 256
                            oitm.Alineación = 256
                        Case 2, 32, 512
                            oitm.Alineación = 512
                        Case 4, 64, 1024
                            oitm.Alineación = 1024
                    End Select
                End If
            Next
        End If
        '1,16,256
        '            2,32,512
        '            4,64, 1024
        '' Resumen:
        'ContentAlignment.TopLeft = 1
        'ContentAlignment.TopCenter = 2
        'ContentAlignment.TopRight = 4
        'ContentAlignment.MiddleLeft = 16
        'ContentAlignment.MiddleCenter = 32
        'ContentAlignment.MiddleRight = 64
        'ContentAlignment.BottomLeft = 256
        'ContentAlignment.BottomCenter = 512
        'ContentAlignment.BottomRight = 1024
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        'Alinear centro
        ' Alinear arriba

        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            Select Case itm.Alineación
                                Case 1, 16, 256
                                    itm.Alineación = 16
                                Case 2, 32, 512
                                    oitm.Alineación = 32
                                Case 4, 64, 1024
                                    itm.Alineación = 64
                            End Select
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    Select Case oitm.Alineación
                        Case 1, 16, 256
                            oitm.Alineación = 16
                        Case 2, 32, 512
                            oitm.Alineación = 32
                        Case 4, 64, 1024
                            oitm.Alineación = 64
                    End Select
                End If
            Next
        End If
        '1,16,256
        '            2,32,512
        '            4,64, 1024
        '' Resumen:
        'ContentAlignment.TopLeft = 1
        'ContentAlignment.TopCenter = 2
        'ContentAlignment.TopRight = 4
        'ContentAlignment.MiddleLeft = 16
        'ContentAlignment.MiddleCenter = 32
        'ContentAlignment.MiddleRight = 64
        'ContentAlignment.BottomLeft = 256
        'ContentAlignment.BottomCenter = 512
        'ContentAlignment.BottomRight = 1024

    End Sub

    Private Sub negrilla_Click(sender As Object, e As EventArgs) Handles negrilla.Click
        ' Alinear izquierda
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "B"))
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "B"))
                End If
            Next
        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        'subrayar
        ' Alinear izquierda
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "U"))
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "U"))
                End If
            Next
        End If
RETURN

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        'Tachar
        'subrayar
        ' Alinear izquierda
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "S"))
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "S"))
                End If
            Next
        End If
        Return

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        'Itálica
        'subrayar
        ' Alinear izquierda
        Dim secn As Section
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            For j As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(j)
                For i As Integer = 0 To secn.AR.Count - 1
                    If TypeOf secn.AR(i) Is LabelItem Then
                        Dim itm As LabelItem = secn.AR(i)
                        If itm.selected Then
                            itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "I"))
                            Return
                        End If
                    End If
                Next
            Next
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "I"))
                End If
            Next
        End If
        Return

    End Sub

    Private Sub ComboBox_fuentes_Click(sender As Object, e As EventArgs) Handles ComboBox_fuentes.Click
        'Tachar
        'subrayar
        ' Alinear izquierda
        'Dim secn As Section
        'Dim oitm As LabelItem

        'If nis.Seleccionados.Count = 0 Then
        '    For j As Integer = 0 To RP.Sections.Count - 1
        '        secn = RP.Sections(j)
        '        For i As Integer = 0 To secn.AR.Count - 1
        '            If TypeOf secn.AR(i) Is LabelItem Then
        '                Dim itm As LabelItem = secn.AR(i)
        '                If itm.selected Then
        '                    itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "S"))
        '                    Return
        '                End If
        '            End If
        '        Next
        '    Next
        'Else
        '    For Each it As Item In nis.Seleccionados
        '        If TypeOf it Is LabelItem Then
        '            oitm = it
        '            oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "S"))
        '        End If
        '    Next
        'End If
        Return
    End Sub

    'Private Sub pictureBox3_Move(sender As Object, e As EventArgs) Handles pictureBox3.Move
    '    'Tachar
    '    'subrayar
    '    ' Alinear izquierda
    '    Dim secn As Section
    '    Dim oitm As LabelItem

    '    If nis.Seleccionados.Count = 0 Then
    '        For j As Integer = 0 To RP.Sections.Count - 1
    '            secn = RP.Sections(j)
    '            For i As Integer = 0 To secn.AR.Count - 1
    '                If TypeOf secn.AR(i) Is LabelItem Then
    '                    Dim itm As LabelItem = secn.AR(i)
    '                    If itm.selected Then
    '                        itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "S"))
    '                        Return
    '                    End If
    '                End If
    '            Next
    '        Next
    '    Else
    '        For Each it As Item In nis.Seleccionados
    '            If TypeOf it Is LabelItem Then
    '                oitm = it
    '                oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "S"))
    '            End If
    '        Next
    '    End If
    '    Return
    'End Sub

    Private Sub Combox_Ftes_size_Click(sender As Object, e As EventArgs) Handles Combox_Ftes_size.Click
        'Tachar
        'subrayar
        ' Alinear izquierda
        'Dim secn As Section
        'Dim oitm As LabelItem

        'If nis.Seleccionados.Count = 0 Then
        '    For j As Integer = 0 To RP.Sections.Count - 1
        '        secn = RP.Sections(j)
        '        For i As Integer = 0 To secn.AR.Count - 1
        '            If TypeOf secn.AR(i) Is LabelItem Then
        '                Dim itm As LabelItem = secn.AR(i)
        '                If itm.selected Then
        '                    itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "S"))
        '                    Return
        '                End If
        '            End If
        '        Next
        '    Next
        'Else
        '    For Each it As Item In nis.Seleccionados
        '        If TypeOf it Is LabelItem Then
        '            oitm = it
        '            oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "S"))
        '        End If
        '    Next
        'End If
        'Return
    End Sub

    Private Sub ComboBox_fuentes_TextChanged(sender As Object, e As EventArgs) Handles ComboBox_fuentes.TextChanged
        'Tachar
        'subrayar
        ' Alinear izquierda
        'Dim secn As Section
        'Dim oitm As LabelItem

        'If nis.Seleccionados.Count = 0 Then
        '    For j As Integer = 0 To RP.Sections.Count - 1
        '        secn = RP.Sections(j)
        '        For i As Integer = 0 To secn.AR.Count - 1
        '            If TypeOf secn.AR(i) Is LabelItem Then
        '                Dim itm As LabelItem = secn.AR(i)
        '                If itm.selected Then
        '                    itm.Font = New Font(itm.Font, CambiarStilo(itm.Font, "S"))
        '                    Return
        '                End If
        '            End If
        '        Next
        '    Next
        'Else
        '    For Each it As Item In nis.Seleccionados
        '        If TypeOf it Is LabelItem Then
        '            oitm = it
        '            oitm.Font = New Font(oitm.Font, CambiarStiloBloque(oitm.Font, "S"))
        '        End If
        '    Next
        'End If
        'Return
    End Sub

    Private Sub Combox_Ftes_size_TextChanged(sender As Object, e As EventArgs) Handles Combox_Ftes_size.TextChanged

    End Sub
    Private Function CambiarStilo(oFnt, ne) As Integer
        Dim ret As Integer = 0

        Select Case ne
            Case "B"
                If oFnt.Bold Then
                    ret = 0
                Else
                    ret = 1
                End If
                ret += If(oFnt.Bold, 0, 0) + If(oFnt.Italic, 2, 0) + If(oFnt.Underline, 4, 0) + If(oFnt.Strikeout, 8, 0)
            Case "I"
                If oFnt.Italic Then
                    ret = 0
                Else
                    ret = 2
                End If
                ret += If(oFnt.Bold, 1, 0) + If(oFnt.Italic, 0, 0) + If(oFnt.Underline, 4, 0) + If(oFnt.Strikeout, 8, 0)
            Case "U"
                If oFnt.Underline Then
                    ret = 0
                Else
                    ret = 4
                End If
                ret += If(oFnt.Bold, 1, 0) + If(oFnt.Italic, 2, 0) + If(oFnt.Underline, 0, 0) + If(oFnt.Strikeout, 8, 0)
            Case "S"
                If oFnt.Strikeout Then
                    ret = 0
                Else
                    ret = 8
                End If
                ret += If(oFnt.Bold, 1, 0) + If(oFnt.Italic, 2, 0) + If(oFnt.Underline, 4, 0) + If(oFnt.Strikeout, 0, 0)
        End Select
        Return ret

    End Function

    Private Function CambiarStiloBloque(oFnt, ne) As String
        Dim ret As Integer = 0

        Select Case ne
            Case "B"
                ret = 1
                ret += If(oFnt.Bold, 0, 0) + If(oFnt.Italic, 2, 0) + If(oFnt.Underline, 4, 0) + If(oFnt.Strikeout, 8, 0)
            Case "I"
                ret = 2
                ret += If(oFnt.Bold, 1, 0) + If(oFnt.Italic, 0, 0) + If(oFnt.Underline, 4, 0) + If(oFnt.Strikeout, 8, 0)
            Case "U"
                ret = 4
                ret += If(oFnt.Bold, 1, 0) + If(oFnt.Italic, 2, 0) + If(oFnt.Underline, 0, 0) + If(oFnt.Strikeout, 8, 0)
            Case "S"
                ret = 8
                ret += If(oFnt.Bold, 1, 0) + If(oFnt.Italic, 2, 0) + If(oFnt.Underline, 4, 0) + If(oFnt.Strikeout, 0, 0)
        End Select
        Return ret

    End Function

    Private Sub ComboBox_fuentes_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBox_fuentes.KeyUp

        'Dim secn As Section
        'Dim oitm As LabelItem

        'If nis.Seleccionados.Count = 0 Then
        '    For j As Integer = 0 To RP.Sections.Count - 1
        '        secn = RP.Sections(j)
        '        For i As Integer = 0 To secn.AR.Count - 1
        '            If TypeOf secn.AR(i) Is LabelItem Then
        '                Dim itm As LabelItem = secn.AR(i)
        '                If itm.selected Then
        '                    itm.Font = New Font(ComboBox_fuentes.Text, itm.Font.Size, itm.Font.Style)
        '                    Return
        '                End If
        '            End If
        '        Next
        '    Next
        'Else
        '    For Each it As Item In nis.Seleccionados
        '        If TypeOf it Is LabelItem Then
        '            oitm = it
        '            oitm.Font = New Font(ComboBox_fuentes.Text, oitm.Font.Size, oitm.Font.Style)
        '        End If
        '    Next
        'End If
        'Return
    End Sub

    Private Sub bGuardarcomo_Click(sender As Object, e As EventArgs) Handles bGuardarcomo.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim oFrm As Frmguardarcomo = New Frmguardarcomo
        Dim Formato As String
        Dim proceso As String
        Dim sql As String

        oFrm.formato = RP.formato
        If oFrm.ShowDialog() = DialogResult.Yes Then
            Formato = oFrm.formato
            sql = "SELECT * FROM zzzinformatos_p WHERE documento = '" & RP.documento & "' AND formato = '" & Formato & "'"
            If Rs.Open(sql, ObjetoGlobal.cn) Then
                If Rs.RecordCount > 0 Then
                    Dim f As New CustMsg
                    f.msg.Text = "El formato indicado ya existe, ¿quieres sustituirlo?"
                    f.button1.Text = "No"
                    f.button2.Text = "Sí"
                    f.button3.Visible = True
                    f.ShowDialog()

                    If f.OK Then
                        RP.formato = Formato
                        oFmto.formato = Formato
                        Formatonuevo = False
                        GrabarInforme()
                        If savePending Then
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                    f.Dispose()
                Else
                    'oFmto.proceso = RP.proceso
                    'oFmto.documento = RP.documento
                    oFmto.formato = Formato
                    Formatonuevo = True
                    GrabarInforme()
                End If
            End If
        End If
        Rs.Close()

    End Sub

    Private Function nuevoformato(ByVal aRet() As String) As Report
        Dim TRP As New Report
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim cZona As String
        Dim cValor As String
        Dim t As Item
        Dim secciones() As String = {"cabecera0", "cabecera1", "cuerpo0", "cuerpo1", "pie0", "pie1"}
        Dim i As Integer
        Dim alinear As String
        Dim secn As Section
        Dim haySeccion As Boolean = False
        Dim numdato As Integer = 0
        Dim numerolinea As Integer
        Dim numeroimagen As Integer
        Dim nEstilo As Integer
        Dim nFontSize As Double

        dtDatos.Reset()

        dtDatos.Columns.Add(New DataColumn("valor", GetType(String)))
        dtDatos.Columns.Add(New DataColumn("seccion", GetType(String)))

        dtTablas.Reset()
        dtTablas.Columns.Add(New DataColumn("valor", GetType(String)))
        dtTablas.Columns.Add(New DataColumn("seccion", GetType(String)))

        dtVariables.Reset()
        dtVariables.Columns.Add(New DataColumn("valor", GetType(String)))
        dtVariables.Columns.Add(New DataColumn("seccion", GetType(String)))

        'dtVariables.Rows.Add("[fecha]")
        'dtVariables.Rows.Add("[fechalarga]")
        'dtVariables.Rows.Add("[pagina]")
        'dtVariables.Rows.Add("[paginas]")
        'dtVariables.Rows.Add("[registros()]")

        oFmto.proceso = aRet(4)
        oFmto.documento = aRet(0)
        oFmto.formato = aRet(1)
        oFmto.copias = 1
        oFmto.papel = "A4"
        oFmto.impresoraPDF = "N"

        Try
            TRP.documento = aRet(0)
            TRP.formato = aRet(1)
            TRP.nm = aRet(1)

            For i = 0 To 5
                If i = 0 Or i = 2 Or i = 4 Then
                    cZona = secciones(i)
                    dtVariables.Rows.Add("[fecha]", Strings.Left(cZona, cZona.Length - 1))
                    dtVariables.Rows.Add("[fechalarga]", Strings.Left(cZona, cZona.Length - 1))
                    dtVariables.Rows.Add("[pagina]", Strings.Left(cZona, cZona.Length - 1))
                    dtVariables.Rows.Add("[paginas]", Strings.Left(cZona, cZona.Length - 1))
                    dtVariables.Rows.Add("[registros()]", Strings.Left(cZona, cZona.Length - 1))
                End If
            Next

            ScriptsEventos = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}

            Norefrescar = True

            Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = 'BASICO' ORDER BY zona, valor"

            ' Cargamos primero las variables
            If Rs.Open(Sql, ObjetoGlobal.cn) Then
                While Not Rs.EOF
                    ' Ahora cargamos las tablas
                    If Trim(cZona) <> Trim(Rs!zona) Then
                        cZona = Trim(Rs!zona)
                        cValor = Trim(Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1))
                        If Trim(cValor) = "calculado" Then
                            'dtVariables.Rows.Add(Rs!valor, cZona)
                            dtVariables.Rows.Add(Rs!valor, cZona)
                            cValor = ""
                        Else
                            dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                            dtTablas.Rows.Add(cValor, cZona)
                        End If
                    Else
                        If Trim(cValor) <> Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1) Then
                            cValor = Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1)
                            If Trim(cValor) = "calculado" Then
                                dtVariables.Rows.Add(Rs!valor, cZona)
                                cValor = ""
                            Else
                                dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                                dtTablas.Rows.Add(cValor, cZona)
                            End If
                        Else
                            If Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1) = "calculado" Then
                                dtVariables.Rows.Add(Rs!valor)
                            Else
                                dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                            End If
                        End If
                    End If
                    Rs.MoveNext()
                End While
            End If
            Rs.Close()

            If aRet(2) = False Then

                For i = 0 To 5
                    haySeccion = False
                    cZona = secciones(i)

                    Sql = "select TOP 1 * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo = 'banda'  ORDER BY 1,2,3,4,5"
                    If Rs.Open(Sql, ObjetoGlobal.cn) Then
                        If Not Rs.EOF Then
                            secn = New Section
                            If Strings.Left(cZona, cZona.Length - 1) = "cuerpo" Then
                                secn = New SectionDetail
                            Else
                                secn = New Section
                            End If
                            secn.nm = Strings.Left(cZona, cZona.Length - 1)
                            secn.headerLabel = Strings.Left(cZona, cZona.Length - 1)
                            secn.h = convertirPixelesPuntos(Rs!alto)
                            secn.GroupID = i
                            haySeccion = True
                            TRP.Sections.Add(secn)
                        End If
                    End If
                    Rs.Close()
                Next
                Return TRP
            Else
                LlenarVacio(TRP, aRet)
                Return TRP
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Function GrabarInforme()
        Dim SQL As String
        Dim RS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            If Not Formatonuevo Then
                SQL = "SELECT * FROM zzzinformatos_p WHERE documento = '" & RP.documento & "' AND formato = '" & RP.formato & "'"
                If RS.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans) Then
                    While RS.RecordCount > 0
                        RS.MoveFirst()
                        RS.Delete()
                    End While
                End If
            End If
            If Not oFmto.GrabaFormato(trans) Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                MsgBox("Error en la grabación de formatos, se suspende la grabación.", vbInformation, "")
                Return False
            End If
            If Not GrabarBasico(trans) Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                MsgBox("Error en la grabación de formatos, se suspende la grabación.", vbInformation, "")
                Return False
            End If
            If Not GrabarItems(trans) Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                MsgBox("Error en la grabación de formatos, se suspende la grabación.", vbInformation, "")
                Return False
            End If
            If Not GrabarScripts(trans) Then
                trans.Rollback()
                ObjetoGlobal.cn.Close()
                MsgBox("Error en la grabación de formatos, se suspende la grabación.", vbInformation, "")
                Return False
            End If
            savePending = False
            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return True

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("Error en la grabación de formatos, se suspende la grabación.", vbInformation, "")
            Return False
        End Try


    End Function

    Private Function GrabarItems(ByRef trans As SqlClient.SqlTransaction)
        Dim SQL As String
        Dim RS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        SQL = "SELECT * FROM zzzinformatos_p WHERE 1=0"
        If Not RS.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans) Then
            MsgBox("Peroblemas abriendo la base de datos")
            Return False
        End If

        For j As Integer = 0 To RP.Sections.Count - 1

            For Each t As Item In RP.Sections(j).AR

                RS.AddNew()
                RS!documento = Trim(oFmto.documento)
                RS!formato = Trim(oFmto.formato)
                RS!zona = RP.Sections(j).nm
                RS!indice = RP.Sections(j).indice 't.no
                RS!valor = Trim(t.nm)


                RS!tope = t.Rec.Y
                RS!alto = t.Rec.Height
                RS!izquierda = t.Rec.X
                RS!ancho = t.Rec.Width

                RS!num_dato = t.NumeroItem

                Select Case t.ItemType
                    Case "Label"
                        RS!tipo = "etiqueta"
                        RS!fuente = t.fnt.FontFamily
                        RS!tamanof = t.fnt.Size
                        RS!negrita = If(t.fnt.Style = 1 Or t.fnt.Style = 3 Or t.fnt.Style = 5 Or t.fnt.Style = 7, -1, 0)
                        RS!cursiva = If(t.fnt.Style = 2 Or t.fnt.Style = 3 Or t.fnt.Style = 6 Or t.fnt.Style = 7, -1, 0)
                        RS!subrayado = If(t.fnt.Style = 4 Or t.fnt.Style = 5 Or t.fnt.Style = 6 Or t.fnt.Style = 7, -1, 0)
                        RS!color = t.fcolor.ToArgb
                        RS!fontcolor = t.fcolor.ToArgb
                        RS!angulo = t.Drawgrados
                        RS!valor = Trim(t.txt)


                    Case "DataLabel"
                        RS!tipo = "campo"
                        RS!fuente = t.fnt.FontFamily
                        RS!tamanof = t.fnt.Size
                        RS!negrita = If(t.fnt.Style = 1 Or t.fnt.Style = 3 Or t.fnt.Style = 5 Or t.fnt.Style = 7, -1, 0)
                        RS!cursiva = If(t.fnt.Style = 2 Or t.fnt.Style = 3 Or t.fnt.Style = 6 Or t.fnt.Style = 7, -1, 0)
                        RS!subrayado = If(t.fnt.Style = 4 Or t.fnt.Style = 5 Or t.fnt.Style = 6 Or t.fnt.Style = 7, -1, 0)
                        RS!angulo = t.Drawgrados
                        RS!color = t.fcolor.ToArgb
                        RS!fontcolor = t.fcolor.ToArgb
                        RS!formatosal = t.fmt

                    Case "Shape"
                        If t.tipo = t.SType.Rectangulo Then
                            RS!tipo = "rectangulo"
                        ElseIf t.tipo = t.SType.Elipse Then
                            RS!tipo = "elipse"
                        ElseIf t.tipo = t.SType.Linea_horizontal Or t.tipo = t.SType.Linea_vertical Or t.tipo = t.SType.Linea_libre Then
                            RS!tope = t.Rec.X
                            RS!alto = t.Rec.Y
                            RS!izquierda = t.Rec.Width + t.Rec.X
                            RS!ancho = t.Rec.Height + t.Rec.Y
                            RS!tipo = "linea"
                        Else
                            RS!tipo = "rectangulo"
                        End If
                        RS!estilo = t.estilo_linea
                        RS!grosorlinea = Math.Round(t.Ancho_del_borde / 0.025, 0)
                        RS!color = t.BorderColor.ToArgb

                    Case "Image"
                        RS!tipo = "dibujo"
                        RS!codigo_imagen = t.codimg
                        RS!color = t.BorderColor.ToArgb

                    Case "Fbarcode"
                        RS!tipo = "dbarcode"
                        If t.Drawbarcode = Item.SBarcode.Barcode39 Then
                            RS!cod_barras = "39"
                        ElseIf t.Drawbarcode = Item.SBarcode.BarcodeQR Then
                            RS!cod_barras = "QR"
                        ElseIf t.Drawbarcode = Item.SBarcode.Barcode128 Then
                            RS!cod_barras = "128"
                        ElseIf t.Drawbarcode = Item.SBarcode.BarcodeEAN13 Then
                            RS!cod_barras = "EAN13"
                        End If
                        RS!color = t.fcolor.ToArgb


                    Case "Barcode"
                        RS!tipo = "barcode"
                        If t.Drawbarcode = Item.SBarcode.Barcode39 Then
                            RS!cod_barras = "39"
                        ElseIf t.Drawbarcode = Item.SBarcode.BarcodeQR Then
                            RS!cod_barras = "QR"
                        ElseIf t.Drawbarcode = Item.SBarcode.Barcode128 Then
                            RS!cod_barras = "128"
                        ElseIf t.Drawbarcode = Item.SBarcode.BarcodeEAN13 Then
                            RS!cod_barras = "EAN13"
                        End If
                        RS!color = t.fcolor.ToArgb
                        RS!valor = Trim(t.txt)

                    Case "DataImage"
                        RS!tipo = "dimage"
                        RS!codigo_imagen = t.codimg
                        RS!color = t.BorderColor.ToArgb
                    Case Else
                        MsgBox("Tipo " & t.ItemType & " desconocido")
                End Select

                RS!backcolor = t.bcolor.ToArgb
                RS!color_borde = t.linecolor.ToArgb
                RS!radio_borde = t.BorderRad
                RS!ancho_borde = t.bw
                RS!borderside = t.RecSides
                RS!angulo = t.ang
                RS!autosize = IIf(t.fitInBox, "S", "N")
                If t.TruncarTexto Then
                    RS!autosize = "T"
                End If

                Select Case t.txtalign
                    Case ContentAlignment.MiddleLeft
                        RS!justificado = "I"
                    Case ContentAlignment.MiddleRight
                        RS!justificado = "D"
                    Case ContentAlignment.MiddleCenter
                        RS!justificado = "C"
                    Case ContentAlignment.TopLeft
                        RS!justificado = "1"
                    Case ContentAlignment.TopCenter
                        RS!justificado = "2"
                    Case ContentAlignment.TopRight
                        RS!justificado = "3"
                    Case ContentAlignment.MiddleLeft
                        RS!justificado = "4"
                    Case ContentAlignment.MiddleCenter
                        RS!justificado = "5"
                    Case ContentAlignment.MiddleRight
                        RS!justificado = "6"
                    Case ContentAlignment.BottomLeft
                        RS!justificado = "7"
                    Case ContentAlignment.BottomCenter
                        RS!justificado = "8"
                    Case ContentAlignment.BottomRight
                        RS!justificado = "9"
                    Case Else
                        RS!justificado = "I"
                End Select
                RS!unidades = "px"
                RS.Update()
            Next
        Next

        Return True


    End Function

    Private Function GrabarBasico(ByRef trans As SqlClient.SqlTransaction)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim numdato As Integer

        Try
            For i As Integer = 0 To RP.Sections.Count - 1
                Dim tsecn As New Section
                Dim secn As Section
                secn = RP.Sections(i)

                Sql = "SELECT * FROM zzzinformatos_p WHERE documento = '" & Trim(oFmto.documento) & "' AND formato = '" & Trim(oFmto.formato) & "' and zona = '" & secn.nm & "' AND tipo ='banda' AND indice=" & secn.indice
                If Rs.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans) Then
                    If Rs.EOF Then
                        Rs.AddNew()
                    End If
                    Rs!documento = Trim(oFmto.documento)
                    Rs!formato = Trim(oFmto.formato)
                    Rs!backcolor = secn.bcolor.ToArgb
                    Rs!unidades = "px"
                    Rs!Alto = secn.h
                    Rs!zona = secn.nm
                    Rs!num_dato = 0
                    Rs!tipo = "banda"
                    Rs!indice = secn.indice
                    Rs.Update()
                End If
                Rs.Close()
            Next

            Rs.Open("SELECT max(num_dato) as numero FROM zzzinformatos_p WHERE documento = '" & Trim(RP.documento) & "' AND formato = 'BASICO'", ObjetoGlobal.cn, ,,,,,, trans)

            If Rs.RecordCount > 0 Then
                If Not IsDBNull(Rs!numero) Then
                    numdato = Rs!numero
                Else
                    numdato = 0
                End If
            Else
                numdato = 0
            End If
            Rs.Close()

            For Each MiDataRow As DataRow In dtVariables.Rows
                Sql = "SELECT * FROM zzzinformatos_p WHERE documento = '" & Trim(RP.documento) & "' AND formato = 'BASICO' and zona = '" & MiDataRow("seccion") & "' AND valor ='" & MiDataRow("valor") & "'"
                If Rs.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans) Then
                    If Rs.EOF Then
                        ' Añadimos, no quitamos ningún registro por si acaso otro formato lo utiliza
                        If (Trim(MiDataRow("valor")) <> "[fecha]" And
                       Trim(MiDataRow("valor")) <> "[fechalarga]" And
                       Trim(MiDataRow("valor")) <> "[pagina]" And
                       Trim(MiDataRow("valor")) <> "[paginas]" And
                       Trim(MiDataRow("valor")) <> "[registros()]") Then
                            numdato = numdato + 1
                            Rs.AddNew()
                            Rs!documento = Trim(oFmto.documento)
                            Rs!formato = "BASICO"
                            Rs!zona = MiDataRow("seccion")
                            Rs!tipo = "campo"
                            Rs!indice = 1
                            Rs!num_dato = numdato
                            Rs!valor = MiDataRow("valor")
                            Rs.Update()
                        End If
                    End If
                End If
                Rs.Close()
            Next

            For Each MiDataRow As DataRow In dtDatos.Rows
                Sql = "SELECT * FROM zzzinformatos_p WHERE documento = '" & Trim(RP.documento) & "' AND formato = 'BASICO' and zona = '" & MiDataRow("seccion") & "' AND valor ='" & MiDataRow("valor") & "'"
                If Rs.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans) Then
                    If Rs.EOF Then
                        ' Añadimos, no quitamos ningún registro por si acaso otro formato lo utiliza
                        numdato = numdato + 1
                        Rs.AddNew()
                        Rs!documento = Trim(oFmto.documento)
                        Rs!formato = "BASICO"
                        Rs!zona = MiDataRow("seccion")
                        Rs!tipo = "campo"
                        Rs!indice = 1
                        Rs!num_dato = numdato
                        Rs!valor = MiDataRow("valor")
                        Rs.Update()
                    End If
                End If
                Rs.Close()
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error en grabación de formato")
            Return False
        End Try


    End Function

    Private Sub LlenarVacio(ByRef RP As Report, ByVal aRet As String())


        If Not RP Is Nothing Then
            RP.nm = aRet(1)
            For i As Integer = 0 To 4
                Dim secn As Section
                Select Case i
                    Case 0
                        secn = New Section
                        secn.h = 120
                        secn.nm = "Cabecera0"
                        secn.headerLabel = "Cabecera"
                    Case 1
                        secn = New Section
                        secn.h = 100
                        secn.nm = "Cabecera1"
                        secn.headerLabel = "Cabecera dos"
                    Case 2
                        secn = New SectionDetail
                        secn.h = 80
                        secn.nm = "detalle"
                        secn.headerLabel = "Cuerpo básico"
                        'secn.bcolor = color.SkyBlue
                    Case 3
                        secn = New Section
                        secn.h = 100
                        secn.nm = "Pie1"
                        secn.headerLabel = "Píe dos"
                    Case 4
                        secn = New Section
                        secn.h = 120
                        secn.nm = "Pie0"
                        secn.headerLabel = "Píe"
                End Select
                secn.GroupID = i
                RP.Sections.Add(secn)
            Next

        End If


        ShowProperty(RP)

        ShowTree()
        Done()
        Reshape()

    End Sub
    Private Function DevuelveEstilo(Optional a As String = "Solido") As Integer
        Select Case Trim(a)
            Case "Solido"
                Return System.Drawing.Drawing2D.DashStyle.Solid
            Case "Guiones"
                Return System.Drawing.Drawing2D.DashStyle.Dash
            Case "Puntos"
                Return System.Drawing.Drawing2D.DashStyle.Dot
            Case "Guion_punto"
                Return System.Drawing.Drawing2D.DashStyle.DashDot
            Case "Guion_2_puntos"
                Return System.Drawing.Drawing2D.DashStyle.DashDotDot
        End Select

    End Function
    Private Sub Combox_Ftes_size_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combox_Ftes_size.SelectedIndexChanged
        Dim nS As Integer
        Dim nt As Integer
        Dim oitm As LabelItem

        If nis.Seleccionados.Count = 0 Then
            If Not si Is Nothing Then
                If si.ItemType = "Label" Or si.ItemType = "DataLabel" Or si.ItemType = "Barcode" Or si.ItemType = "FBarcode" Then
                    nS = si.fnt.Size
                    nS = si.fnt.Style
                    si.fnt = New Font(si.fnt.FontFamily, CInt(Combox_Ftes_size.Text), si.fnt.Style) ' = stringToFont(ComboBox_fuentes.SelectedItem)
                    si.Refresh()
                End If
            End If
        Else
            For Each it As Item In nis.Seleccionados
                If TypeOf it Is LabelItem Then
                    oitm = it
                    oitm.Font = New Font(oitm.Font.FontFamily, CInt(Combox_Ftes_size.Text), oitm.Font.Style)
                End If
            Next
        End If
    End Sub
    Private Function ConvertirPixelesPuntos(ByVal px As Int32) As Double
        Return IIf(conversion, (px * 0.0666666667), px)
    End Function

    Private Function CargarInformeAntiguos(ByVal aRet() As String) As Report
        Dim TRP As New Report
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim cZona As String
        Dim cValor As String
        Dim t As Item
        Dim secciones() As String = {"cabecera0", "cabecera1", "cuerpo0", "cuerpo1", "pie0", "pie1"}
        Dim i As Integer
        Dim alinear As String
        Dim secn As Section
        Dim haySeccion As Boolean = False
        Dim numdato As Integer = 0
        Dim numerolinea As Integer
        Dim numeroimagen As Integer
        Dim nEstilo As Integer
        Dim nFontSize As Double

        dtDatos.Reset()

        dtDatos.Columns.Add(New DataColumn("valor", GetType(String)))
        dtDatos.Columns.Add(New DataColumn("seccion", GetType(String)))

        dtTablas.Reset()
        dtTablas.Columns.Add(New DataColumn("valor", GetType(String)))
        dtTablas.Columns.Add(New DataColumn("seccion", GetType(String)))

        dtVariables.Reset()
        dtVariables.Columns.Add(New DataColumn("valor", GetType(String)))
        dtVariables.Columns.Add(New DataColumn("seccion", GetType(String)))

        'dtVariables.Rows.Add("[fecha]")
        'dtVariables.Rows.Add("[fechalarga]")
        'dtVariables.Rows.Add("[pagina]")
        'dtVariables.Rows.Add("[paginas]")
        'dtVariables.Rows.Add("[registros()]")

        'oFmto.CargaFormato(Proceso, aRet(0), aRet(1))
        conversion = True

        secciones = ObtenerSecciones(aRet)

        Try
            If UBound(aRet) > 1 Then
                nuevoproceso = IIf(aRet(2) = "Sí", True, False)
                nuevodocumento = IIf(aRet(3) = "Sí", True, False)
            End If
            TRP.documento = aRet(0)
            TRP.formato = aRet(1)
            TRP.nm = aRet(1)
            oFmto.CargaFormato("", aRet(0), aRet(1))
            savePending = True

            'For i = 0 To 5
            For i = 0 To UBound(secciones) '- 1
                'If i = 0 Or i = 2 Or i = 4 Then
                cZona = secciones(i)
                dtVariables.Rows.Add("[fecha]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[fechalarga]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[pagina]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[paginas]", Strings.Left(cZona, cZona.Length - 1))
                dtVariables.Rows.Add("[registros()]", Strings.Left(cZona, cZona.Length - 1))
                'End If
            Next

            Norefrescar = True

            Sql = "select * FROM zzzimpformato_n WHERE documento = '" & aRet(0) & "' and FORMATO = 'BASICO' ORDER BY zona, valor"

            ' Cargamos primero las variables
            If Rs.Open(Sql, ObjetoGlobal.cn) Then
                While Not Rs.EOF
                    ' Ahora cargamos las tablas
                    If InStr(Rs!valor, ".") > 0 Then
                        If Trim(cZona) <> Trim(Rs!zona) Then
                            cZona = Trim(Rs!zona)
                            cValor = Trim(Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1))
                            If Trim(cValor) = "calculado" Then
                                'dtVariables.Rows.Add(Rs!valor, cZona)
                                dtVariables.Rows.Add(Rs!valor, cZona)
                                cValor = ""
                            Else
                                dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                                dtTablas.Rows.Add(cValor, cZona)
                            End If
                        Else
                            If Trim(cValor) <> Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1) Then
                                cValor = Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1)
                                If Trim(cValor) = "calculado" Then
                                    dtVariables.Rows.Add(Rs!valor, cZona)
                                    cValor = ""
                                Else
                                    dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                                    dtTablas.Rows.Add(cValor, cZona)
                                End If
                            Else
                                If Strings.Left(Rs!valor, InStr(Rs!valor, ".") - 1) = "calculado" Then
                                    dtVariables.Rows.Add(Rs!valor)
                                Else
                                    dtDatos.Rows.Add(Rs!valor, Rs!zona) ', "C", 0, 0, "")
                                End If
                            End If
                        End If
                    Else
                        If Trim(cZona) <> Trim(Rs!zona) Then
                            cZona = Trim(Rs!zona)
                            'dtVariables.Rows.Add(Rs!valor, cZona)
                            dtVariables.Rows.Add("calculado." & Trim("" & Rs!valor), cZona)
                            cValor = ""
                        Else
                            dtVariables.Rows.Add("calculado." & Trim("" & Rs!valor), cZona)
                            cValor = ""
                        End If
                    End If
                    Rs.MoveNext()
                End While
            End If
            Rs.Close()

            ' vemos las secciones
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            'For i = 0 To 5
            For i = 0 To UBound(secciones) '- 1

                haySeccion = False
                cZona = secciones(i)

                Sql = "select * FROM zzzimpformato_n WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo = 'banda'  ORDER BY 1,2,3,4,5"
                If Rs.Open(Sql, ObjetoGlobal.cn) Then



                    If Not Rs.EOF Then

                        If Trim("" & Rs!unidades) = "px" And conversion Then
                            conversion = False
                        End If

                        secn = New Section

                        If Strings.Left(cZona, cZona.Length - 1) = "cuerpo" Then
                            secn = New SectionDetail
                        Else
                            secn = New Section
                        End If
                        secn.nm = Strings.Left(cZona, cZona.Length - 1)
                        secn.headerLabel = Strings.Left(cZona, cZona.Length - 1)
                        secn.h = ConvertirPixelesPuntos(Rs!alto)
                        secn.indice = Rs!indice
                        secn.GroupID = i
                        haySeccion = True

                        TRP.Sections.Add(secn)
                    End If
                End If

                Rs.Close()
                If haySeccion Then
                    Sql = "select * FROM zzzimpformato_n WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo <> 'banda'  ORDER BY 1,2,3,4,5"
                    If Rs.Open(Sql, ObjetoGlobal.cn) Then
                        While Not Rs.EOF

                            numdato = numdato + 1

                            Dim rec As New Rectangle(ConvertirPixelesPuntos(Rs!izquierda), ConvertirPixelesPuntos(Rs!tope), ConvertirPixelesPuntos(Rs!ancho), ConvertirPixelesPuntos(Rs!alto))
                            Select Case Trim(Rs!tipo)
                                Case "etiqueta"
                                    t = New LabelItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                Case "rectangulo"
                                    t = New shapeItem(rec)
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.FromArgb(255, 255, 255), Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea
                                    t.nm = "Rectangulo"

                                Case "linea"
                                    rec = New Rectangle(ConvertirPixelesPuntos(Rs!tope), ConvertirPixelesPuntos(Rs!alto), ConvertirPixelesPuntos(Rs!izquierda - Rs!tope), ConvertirPixelesPuntos(Rs!ancho - Rs!alto))

                                    t = New shapeItem(rec)

                                    If Rs!izquierda - Rs!tope = 0 Then
                                        t.tipo = 3
                                        't.Alto = 1
                                    ElseIf Rs!ancho - Rs!alto = 0 Then
                                        t.tipo = 2
                                        't.Ancho = 1
                                    ElseIf (Rs!tope - Rs!izquierda) < (Rs!ancho - Rs!alto) Then
                                        t.tipo = 4
                                        't.Ancho = 1
                                    Else
                                        t.tipo = 4
                                        't.Alto = 1
                                    End If

                                    t.BorderWidth = 1
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea

                                    numerolinea = numerolinea + 1
                                    t.nm = "Línea" & numerolinea
                                    t.txt = ""

                                Case "elipse"
                                    t = New shapeItem(rec)
                                    t.tipo = 1
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea
                                    t.nm = "Elipse"
                                    t.txt = ""

                                Case "dibujo"
                                    numeroimagen = numeroimagen + 1
                                    t = New ImageItem(rec)
                                    t.nm = If("" & Rs!valor = "", "Imagen" & numeroimagen, Rs!valor)
                                    t.Ancho_del_borde = 0.025
                                    t.txt = ""

                                Case "campo"

                                    If InStr("" & Rs!valor, "cb39_") Then
                                        t = New BarcodeFieldItem(rec)
                                        If InStr("" & Rs!valor, ".") = 0 Then
                                            t.txt = "calculado." & Rs!valor
                                            t.nm = "calculado." & Rs!valor
                                        Else
                                            t.txt = "" & Rs!valor
                                            t.nm = "" & Rs!valor
                                        End If
                                        't.txt = "" & Rs!valor
                                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                        't.nm = "" & Rs!valor
                                        t.Ancho_del_borde = 0.025
                                        t.Drawbarcode = Item.SBarcode.Barcode39
                                    Else
                                        t = New DataLabel(rec)
                                        If InStr("" & Rs!valor, ".") = 0 Then
                                            t.txt = "calculado." & Rs!valor
                                            t.nm = "calculado." & Rs!valor
                                        Else
                                            t.txt = "" & Rs!valor
                                            t.nm = "" & Rs!valor
                                        End If
                                        't.txt = "" '& Rs!valor
                                        t.fmt = If(IsDBNull(Rs!formatosal), "" & Rs!tag, Trim(Rs!formatosal))
                                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                        't.nm = "" & Rs!valor
                                        t.Ancho_del_borde = 0.025
                                    End If

                                Case "dimage"
                                    t = New DataImage(rec)
                                    t.txt = "" '& Rs!valor
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                Case "barcode"
                                    t = New BarcodeItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025
                                    Select Case Trim("" & Rs!cod_barras)
                                        Case "39"
                                            t.Drawbarcode = Item.SBarcode.Barcode39
                                        Case "QR"
                                            t.Drawbarcode = Item.SBarcode.BarcodeQR
                                        Case "128"
                                            t.Drawbarcode = Item.SBarcode.Barcode128
                                        Case "EAN13"
                                            t.Drawbarcode = Item.SBarcode.BarcodeEAN13
                                    End Select


                                Case "dbarcode"
                                    t = New BarcodeFieldItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                    Select Case Trim("" & Rs!cod_barras)
                                        Case "39"
                                            t.Drawbarcode = Item.SBarcode.Barcode39
                                        Case "QR"
                                            t.Drawbarcode = Item.SBarcode.BarcodeQR
                                        Case "128"
                                            t.Drawbarcode = Item.SBarcode.Barcode128
                                        Case "EAN13"
                                            t.Drawbarcode = Item.SBarcode.BarcodeEAN13
                                    End Select

                                Case Else
                                    t = New LabelItem(rec)
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025
                            End Select
                            ' "name"

                            't.nm = "" & Rs!valor
                            ' "text"

                            't.txt = "" & Rs!valor
                            ' -1;Bordercolor:-16777216;BorderRadius:0;BorderWidth:0
                            ' "backcolor"
                            t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                            ' "bordercolor"
                            t.linecolor = If(IsDBNull(Rs!color_borde), Color.FromArgb(0, 0, 0), Color.FromArgb(Rs!color_borde))
                            ' "borderradius"
                            t.BorderRad = If(IsDBNull(Rs!radio_borde), 0, ConvertirPixelesPuntos(Rs!radio_borde))
                            ' "borderwidth"
                            't.bw = If(IsDBNull(Rs!ancho_borde), 0.25, convertirPixelesPuntos(Rs!ancho_borde))
                            ' "borderside"
                            t.RecSides = If(IsDBNull(Rs!ancho_borde), CType(15, AnchorStyles), CType(ConvertirPixelesPuntos(Rs!borderside), AnchorStyles))
                            ' "font"
                            't.fnt = stringToFont(if(IsDBNull(Rs!fuente), "Arial; 8pt", Rs!fuente & "; " & If(IsDBNull(Rs!tamanof), 8, Rs!tamanof) & "pt"))
                            t.fnt = stringToFont(If(IsDBNull(Rs!fuente), "Arial", Rs!fuente))
                            nEstilo = 0
                            If Not IsDBNull(Rs!fuente) Then
                                nFontSize = If(IsDBNull(Rs!tamanof), 8.25, Convert.ToDecimal(Replace(Rs!tamanof, ".", ",")))
                                If "" & Rs!negrita <> "" Then
                                    If Rs!negrita = -1 Then
                                        nEstilo = 1
                                    End If
                                End If
                                If "" & Rs!cursiva <> "" Then
                                    If Rs!cursiva = -1 Then
                                        nEstilo = nEstilo + 2
                                    End If
                                End If
                                If "" & Rs!subrayado <> "" Then
                                    If Rs!subrayado = -1 Then
                                        nEstilo = nEstilo + 4
                                    End If
                                End If
                                'If nFontSize <> 0 Then
                                t.fnt = CreateFont(t.fnt.FontFamily.Name, Convert.ToSingle(nFontSize), nEstilo)
                                'End If
                            End If

                            ' "fontcolor"
                            t.fcolor = If(IsDBNull(Rs!color), Color.FromArgb(0, 0, 0), Color.FromArgb(Rs!color))

                            alinear = Trim(If(IsDBNull(Rs!justificado), "I", Rs!justificado))

                            ' "textalign"
                            Select Case alinear
                                Case "I"
                                    t.txtalign = ContentAlignment.MiddleLeft = 16
                                Case "D"
                                    t.txtalign = ContentAlignment.MiddleRight = 64
                                Case "C"
                                    t.txtalign = ContentAlignment.MiddleCenter = 32
                                Case "1"
                                    t.txtalign = ContentAlignment.TopLeft = 1
                                Case "2"
                                    t.txtalign = ContentAlignment.TopCenter = 2
                                Case "3"
                                    t.txtalign = ContentAlignment.TopRight = 4
                                Case "4"
                                    t.txtalign = ContentAlignment.MiddleLeft = 16
                                Case "5"
                                    t.txtalign = ContentAlignment.MiddleCenter = 32
                                Case "6"
                                    t.txtalign = ContentAlignment.MiddleRight = 64
                                Case "7"
                                    t.txtalign = ContentAlignment.BottomLeft = 256
                                Case "8"
                                    t.txtalign = ContentAlignment.BottomCenter = 512
                                Case "9"
                                    t.txtalign = ContentAlignment.BottomRight = 1024
                                Case Else
                                    t.txtalign = ContentAlignment.MiddleLeft = 16
                            End Select

                            ' "datafield"
                            If InStr("" & Rs!valor, ".") = 0 Then
                                t.Dfield = "calculado." & Rs!valor
                            Else
                                t.Dfield = "" & Rs!valor
                            End If

                            ' "dataindex"
                            t.DIndex = Rs!indice
                            ' "image"
                            'If Rs!tipo_imagen = "campo" Then
                            '    't.img = ObtenImagenCampo()
                            '    t.codimg = "campo"
                            'ElseIf Rs!tipo_imagen = "path" Then
                            '    't.img = ObtenImagenArchivo()
                            '    t.codimg = "archivo"
                            If Trim("" & Rs!codigo_imagen) <> "" Then
                                t.img = ObtenImagen(Rs!codigo_imagen)
                                t.codimg = "" & Rs!codigo_imagen
                            Else
                                t.img = Nothing
                                t.codimg = ""
                            End If
                            't.Ancho_del_borde = 0.025
                            ' "padding"
                            ' Dim pc As New PaddingConverter
                            't.pd = pc.ConvertFromString(Data(1))
                            ' "autosize"
                            '                        t.fitInBox = Rs!autosize

                            ' "format"
                            't.fmt = "" & Rs!formatosal

                            'Select Case "" & Rs!cod_barras
                            '    Case "39"
                            '        t.Drawbarcode = Item.SBarcode.Barcode39
                            '    Case "QR"
                            '        t.Drawbarcode = Item.SBarcode.BarcodeQR
                            '    Case "128"
                            '        t.Drawbarcode = Item.SBarcode.Barcode128
                            '    Case "EAN13"
                            '        t.Drawbarcode = Item.SBarcode.BarcodeEAN13
                            'End Select
                            'If Trim(Rs!tipo) <> "dibujo" Then
                            t.NumeroItem = numdato
                            MaxItems = numdato
                            secn.AR.Add(t)
                            'End If

                            ' "digito_control"
                            't.dc = "" & Rs!cb_dc

                            ' "ver_texto"
                            't.mt = "" & Rs!cb_text
                            Rs.MoveNext()
                        End While
                    End If
                    Rs.Close()
                End If
            Next
            Norefrescar = False

            Return TRP

        Catch ex As Exception
            MsgBox(ex.Message)
            'Resume
        End Try

    End Function

    Private Sub bSalir_Click(sender As Object, e As EventArgs) Handles bSalir.Click

        If Not SaveToDB Then
            If savePending Then
                Dim f As New CustMsg
                f.msg.Text = "Quieres guardar las modificaciones realizadas a '" & RP.nm & "'?"
                f.button1.Text = "No"
                f.button2.Text = "Sí"
                f.button3.Visible = True
                f.ShowDialog()

                If f.OK Then
                    If Not savePending Then
                        Close()
                    End If
                    Formatonuevo = False
                    GrabarInforme()
                    savePending = False
                    Close()
                Else
                    Close()
                End If
                f.Dispose()
            End If
            Close()
            ScriptFile = ""
        End If

    End Sub

    Private Sub bcodigobarrast_Click(sender As Object, e As EventArgs) Handles bcodigobarrast.Click

    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
            oFmto.ObjetoGlobal = ObjetoGlobal
        End Set
    End Property

    Private Sub BtScripts_Click(sender As Object, e As EventArgs) Handles BtScripts.Click
        Dim oFrm As frmScrippts = New frmScrippts
        oFrm.ScriptsEventos = ScriptsEventos
        oFrm.ShowDialog()
        ScriptsEventos = oFrm.ScriptsEventos

    End Sub

    Private Function ObtenerSecciones(aRet()) As String()
        Dim aSol() As String = {"cabecera0", "cabecera1"}
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim num_cuerpos As Integer = 2
        Dim i As Integer

        Sql = "select COUNT(*) AS num FROM zzzimpformato_n WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = 'cuerpo' GROUP BY indice"
        If rs.Open(Sql, ObjetoGlobal.cn) Then
            If Not rs.EOF Then
                If Not IsDBNull(rs!num) Then
                    num_cuerpos = rs.RecordCount
                End If
            End If
            For i = 1 To num_cuerpos
                ReDim Preserve aSol(1 + i)
                aSol(1 + i) = "cuerpo" & (i - 1)
            Next
        End If
        ReDim Preserve aSol(UBound(aSol) + 1)
        aSol(UBound(aSol)) = "pie0"
        ReDim Preserve aSol(UBound(aSol) + 1)
        aSol(UBound(aSol)) = "pie1"

        Return aSol

    End Function

    Private Function SacarScripts(doc, fmto)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        Try
            For i As Integer = 1 To (ScriptsEventos.Length - 1)
                Sql = "SELECT * FROM zzscriptsformato WHERE documento = '" & Trim(doc) & "' AND formato = '" & Trim(fmto) & "' and seccion = '" & i & "' and zona = 'todas' "
                If Rs.Open(Sql, ObjetoGlobal.cn) Then
                    If Rs.EOF Then
                        ScriptsEventos(i) = ""
                    Else
                        ScriptsEventos(i) = Trim(Rs!script)
                    End If
                End If
                Rs.Close()
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error obteniendo scripts del formato")
            Return False
        End Try

    End Function

    Private Function GrabarScripts(ByRef trans As SqlClient.SqlTransaction)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        Try
            For i As Integer = 1 To ScriptsEventos.Length - 1
                Sql = "SELECT * FROM zzscriptsformato WHERE documento = '" & Trim(oFmto.documento) & "' AND formato = '" & Trim(oFmto.formato) & "' and seccion = '" & i & "' and zona = 'todas' "
                If Rs.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans) Then
                    If Rs.EOF Then
                        Rs.AddNew()
                    End If
                    Rs!documento = Trim(oFmto.documento)
                    Rs!formato = Trim(oFmto.formato)
                    Rs!seccion = i
                    Rs!zona = "todas"
                    Rs!ent_sal = "T"
                    Rs!script = ScriptsEventos(i)
                    Rs.Update()
                End If
                Rs.Close()
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error en grabación de formato")
            Return False
        End Try

    End Function

End Class
