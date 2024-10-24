'
' Created by SharpDevelop.
' User: Toshiba
' Date: 25-May-16
' Time: 11:26 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Drawing.Drawing2D
imports System.Data
Public Partial Class PrintPreview
	dim scns as new ArrayList()
	public pagewidth As Integer = 21
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		
	End Sub

    Sub PrintPreviewLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim print_document As New PrintDocument
        print_document.PrinterSettings = printset
        ' Install the PrintPage event handler.
        AddHandler print_document.PrintPage, AddressOf PrintPage
        printPreviewControl1 = New PrintPreviewControl
        printPreviewControl1.Document = print_document

        panel1.Controls.Clear()
        printPreviewControl1.Dock = DockStyle.Fill
        panel1.Controls.Add(printPreviewControl1)
        printPreviewControl1.Zoom = numericUpDown1.Value / 100
        printPreviewControl1.Rows = numericUpDown2.Value
        printPreviewControl1.Columns = numericUpDown3.Value

    End Sub

    Private Sub PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Try
            Dim g As Graphics = e.Graphics
            Dim x As Integer = 0
            Dim y As Integer = 0

            Dim rid As Integer = 1

            'scns = rp.Sections.Clone
            pagewidth = RP.w
            Dim h As Integer = 1
            Dim secn As Section
            Dim Rec As Rectangle
            For i As Integer = 0 To RP.Sections.Count - 1
                secn = RP.Sections(i)
                Rec = New Rectangle(0, h, RP.w, secn.h)

                'g.FillRectangle(brushes.Green, rec)
                Dim t As Item
                If secn.nm = "Detail" Then
                    For k As Integer = 0 To secn.DT.Rows.Count - 1
                        For j As Integer = 0 To secn.AR.Count - 1
                            t = secn.AR(j)
                            Rec = New Rectangle(0, h, RP.w, secn.h)
                            g.SetClip(Rec, CombineMode.Replace)
                            'g.Clear(secn.BackColor)
                            g.Clear(Color.White)

                            If secn.BorderWidth > 0 Then
                                Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                                If CInt(secn.BorderSides) = 15 Then
                                    g.DrawRectangle(New Pen(New SolidBrush(secn.BorderColor), secn.BorderWidth), New Rectangle(0, h, RP.w - 1, secn.h - 1))
                                Else
                                    If secn.BorderSides And AnchorStyles.Top Then
                                        g.DrawLine(spn, 0, h, RP.w - 1, 0)
                                    End If
                                    If secn.BorderSides And AnchorStyles.Right Then
                                        g.DrawLine(spn, RP.w - 1, h, RP.w - 1, secn.h - 1)
                                    End If

                                    If secn.BorderSides And AnchorStyles.Bottom Then
                                        g.DrawLine(spn, 0, h + secn.h - 1, RP.w - 1, secn.h - 1)
                                    End If

                                    If secn.BorderSides And AnchorStyles.Left Then
                                        g.DrawLine(spn, 0, h, 0, secn.h - 1)
                                    End If
                                End If
                            End If

                            Dim nRec As New Rectangle(t.Rec.X, t.Rec.Y + h, t.Rec.Width, t.Rec.Height)
                            Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                            If t.bw < 1 Then
                                handPad = 0
                            End If
                            Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)
                            Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)
                            'msgbox(t.ItemType)
                            Select Case t.ItemType
                                Case "Label"
                                    Dim Lrec As New Rectangle(nRec.X + handPad + radPad, nRec.Y + handPad + radPad, nRec.Width - 2 * (handPad + radPad), nRec.Height - 2 * (handPad + radPad))
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    If t.bw > 0 Then
                                        g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                        'g.DrawPath(pn, GetPath(lRec, 0))
                                    End If
                                    'gCanvas.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                    g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))
                                Case "Shape"
                                    pn.DashStyle = t.estilo_linea
                                    Select Case t.Drawshape
                                        Case shapeItem.SType.Rectangulo
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            If t.bw > 0 Then
                                                g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                            End If
                                        Case shapeItem.SType.Elipse
                                            g.FillEllipse(New SolidBrush(t.BackColor), nRec)
                                            If t.bw > 0 Then
                                                g.DrawEllipse(pn, nRec)
                                            End If
                                        Case shapeItem.SType.Linea_horizontal
                                            g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                            If t.bw > 0 Then
                                                g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y)
                                            End If
                                        Case shapeItem.SType.Linea_vertical
                                            g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                            If t.bw > 0 Then
                                                g.DrawLine(pn, nRec.X, nRec.Y, nRec.X, nRec.Y + nRec.Height)
                                            End If
                                    End Select
                                Case "Image"
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                    If t.bw > 0 Then
                                        g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                    End If
                                Case "DataLabel"
                                    'msgbox(radpad)
                                    Dim Lrec As New Rectangle(nRec.X + handPad + radPad, nRec.Y + handPad + radPad, nRec.Width - 2 * (handPad + radPad), nRec.Height - 2 * (handPad + radPad))
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    If t.bw > 0 Then
                                        g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                    End If
                                    'TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                    '  g.DrawString(TextFromDT(t.Dfield, k, secn), t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign), page, pages)

                                    'g.DrawString(t.ItemType & "[" & t.Dfield & "]", t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))

                                    Dim res As String = ""
                                    res = TextFromDTPrev(t.Dfield, k, secn.dt)
                                    If Trim(t.fmt) = "" Then

                                    Else
                                        Try
                                            Dim fo As Object
                                            If Not Decimal.TryParse(res, fo) Then
                                                Dim fo2 As Object
                                                If DateTime.TryParse(res, fo2) Then
                                                    res = Format(fo2, t.fmt)
                                                Else
                                                    res = "Formato desconocido"
                                                End If
                                            Else
                                                res = Format(fo, t.fmt)
                                            End If
                                        Catch ferr As Exception
                                            'res = TextFromDTPrev(t.Dfield, k, secn)

                                        End Try
                                    End If
                                    If t.fitInBox Then
                                        FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))
                                    Else
                                        g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))
                                    End If

                                Case "DataImage"
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    Dim img As Image = ImageFromDT(t.Dfield, k, secn.DT)
                                    g.DrawImage(RoundImageConers(img, nRec, t.BorderRad), nRec)
                                    If t.bw > 0 Then
                                        g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                    End If

                                Case "Barcode"
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                    getRecShape(g, t)
                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                    't.Lados = AnchorStyles.None
                                    cb.ShowString = t.mt
                                    cb.IncludeCheckSumDigit = t.dc
                                    cb.TextFont = t.fnt
                                    cb.TextColor = t.fcolor
                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.txt.Trim <> "", t.txt.Trim, "12345678")), nRec)

                                Case "Fbarcode"
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                    getRecShape(g, t)
                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                    't.Lados = AnchorStyles.None
                                    cb.ShowString = t.mt
                                    cb.IncludeCheckSumDigit = t.dc
                                    cb.TextFont = t.fnt
                                    cb.TextColor = t.fcolor
                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)


                            End Select
                        Next
                        h += secn.h
                    Next
                Else
                    Rec = New Rectangle(0, h, RP.w, secn.h)
                    g.SetClip(Rec, CombineMode.Replace)
                    g.Clear(secn.BackColor)

                    If secn.BorderWidth > 0 Then
                        Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                        If CInt(secn.BorderSides) = 15 Then
                            g.DrawRectangle(New Pen(New SolidBrush(secn.BorderColor), secn.BorderWidth), New Rectangle(0, h, RP.w - 1, secn.h - 1))
                        Else
                            If secn.BorderSides And AnchorStyles.Top Then
                                g.DrawLine(spn, 0, h, RP.w - 1, 0)
                            End If
                            If secn.BorderSides And AnchorStyles.Right Then
                                g.DrawLine(spn, RP.w - 1, h, RP.w - 1, secn.h - 1)
                            End If

                            If secn.BorderSides And AnchorStyles.Bottom Then
                                g.DrawLine(spn, 0, h + secn.h - 1, RP.w - 1, secn.h - 1)
                            End If

                            If secn.BorderSides And AnchorStyles.Left Then
                                g.DrawLine(spn, 0, h, 0, secn.h - 1)
                            End If
                        End If
                    End If

                    For j As Integer = 0 To secn.AR.Count - 1
                        t = secn.AR(j)
                        Dim nRec As New Rectangle(t.Rec.X, t.Rec.Y + h, t.Rec.Width, t.Rec.Height)
                        Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                        If t.bw < 1 Then
                            handPad = 0
                        End If
                        Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)
                        Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)
                        Select Case t.ItemType
                            Case "Label"
                                Dim Lrec As New Rectangle(nRec.X + handPad + radPad, nRec.Y + handPad + radPad, nRec.Width - 2 * (handPad + radPad), nRec.Height - 2 * (handPad + radPad))
                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                If t.bw > 0 Then
                                    g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                    'g.DrawPath(pn, GetPath(lRec, 0))
                                End If
                                'gCanvas.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))
                            Case "Shape"
                                Select Case t.Drawshape
                                    Case shapeItem.SType.Rectangulo
                                        g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                        If t.bw > 0 Then
                                            g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                        End If
                                    Case shapeItem.SType.Elipse
                                        g.FillEllipse(New SolidBrush(t.BackColor), nRec)
                                        If t.bw > 0 Then
                                            g.DrawEllipse(pn, nRec)
                                        End If
                                    Case shapeItem.SType.Linea_horizontal
                                        g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                        If t.bw > 0 Then
                                            g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y)
                                        End If
                                    Case shapeItem.SType.Linea_vertical
                                        g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                        If t.bw > 0 Then
                                            g.DrawLine(pn, nRec.X, nRec.Y, nRec.X, nRec.Y + nRec.Height)
                                        End If
                                End Select
                            Case "Image"
                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                If t.bw > 0 Then
                                    g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                End If
                            Case "DataLabel"
                                Dim Lrec As New Rectangle(nRec.X + handPad + radPad, nRec.Y + handPad + radPad, nRec.Width - 2 * (handPad + radPad), nRec.Height - 2 * (handPad + radPad))
                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                If t.bw > 0 Then
                                    g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                End If
                                'g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                g.DrawString(t.ItemType & "[" & t.Dfield & "]", t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))
                            Case "DataImage"
                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                If t.bw > 0 Then
                                    g.DrawPath(pn, GetPath(nRec, t.BorderRad))
                                End If
                            Case "Barcode"
                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                'g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                'getRecShape(g, t)
                                Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                't.Lados = AnchorStyles.None
                                cb.ShowString = t.mt
                                cb.IncludeCheckSumDigit = t.dc
                                cb.TextFont = t.fnt
                                cb.TextColor = t.fcolor
                                g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.txt.Trim <> "", t.txt.Trim, "12345678")), nRec)
                            Case "Fbarcode"
                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                getRecShape(g, t)
                                Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                't.Lados = AnchorStyles.None
                                cb.ShowString = t.mt
                                cb.IncludeCheckSumDigit = t.dc
                                cb.TextFont = t.fnt
                                cb.TextColor = t.fcolor
                                g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)


                        End Select
                    Next
                    h += secn.h
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Sub NumericUpDown2ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numericUpDown2.ValueChanged
        printPreviewControl1.Rows = numericUpDown2.Value
    End Sub

    Sub NumericUpDown3ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numericUpDown3.ValueChanged
        printPreviewControl1.Columns = numericUpDown3.Value
    End Sub

    Sub NumericUpDown1ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numericUpDown1.ValueChanged
        printPreviewControl1.Zoom = numericUpDown1.Value / 100
    End Sub

    Sub Button1Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
        printPreviewControl1.Document.Print()
    End Sub

    Sub Button2Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
        Me.Close()
    End Sub
End Class
