'
' Created by SharpDevelop.
' User: Louis
' Date: 12/27/2016
' Time: 10:05 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Drawing
imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.IO
imports System.Data

Public Module Module1

    Public _DF As Designer
    Public RP As Report
    Public printset As New Printing.PrinterSettings
    Public si As Item
    Public selsecn As Integer = -1
    'Dim unit As Integer = 100
    Public unit As Integer = 40 '40 '100
    Public Sel_pn As New Pen(Color.FromArgb(50, 10, 10, 255), 1)
    Public actions As New List(Of report)
    Public actionIndex As Long = -1
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public PPIX As Double
    Public PPIY As Double
    Public PPCMX As Double
    Public PPCMY As Double
    Public MaxItems As Long
    Public Norefrescar As Boolean = False
    Public ScriptsEventos() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
    Dim conversion As Boolean = True
    'Public dtVariables As ControlVariables = New ControlVariables

    Dim cn As System.Data.SqlClient.SqlConnection

    Function GetRoundedRectPath(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
        Dim diameter As Integer = 2 * radius
        Dim arcRect As Rectangle =
          New Rectangle(rect.Location, New Size(diameter, diameter))
        Dim path As GraphicsPath = New GraphicsPath()

        ' top left
        Path.AddArc(arcRect, 180, 90)

        ' top right
        arcRect.X = rect.Right - diameter
        Path.AddArc(arcRect, 270, 90)

        ' bottom right
        arcRect.Y = rect.Bottom - diameter
        Path.AddArc(arcRect, 0, 90)

        ' bottom left
        arcRect.X = rect.Left
        Path.AddArc(arcRect, 90, 90)

        Path.CloseFigure()
        Return Path
    End Function

    Function GetRoundedRectPath(ByVal rect As Rectangle, ByVal radius1 As Integer, ByVal radius2 As Integer) As GraphicsPath
        Dim diameter1 As Integer = 2 * radius1
        Dim diameter2 As Integer = 2 * radius2
        Dim arcRect As Rectangle =
          New Rectangle(rect.Location, New Size(diameter1, diameter1))
        Dim path As GraphicsPath = New GraphicsPath()

        ' top left
        Path.AddArc(arcRect, 180, 90)

        ' top right
        arcRect.X = rect.Right - diameter1
        Path.AddArc(arcRect, 270, 90)

        ' bottom right
        arcRect = New Rectangle(rect.Location, New Size(diameter2, diameter2))
        arcRect.X = rect.Right - diameter2
        arcRect.Y = rect.Bottom - diameter2
        Path.AddArc(arcRect, 0, 90)

        ' bottom left
        arcRect.X = rect.Left
        Path.AddArc(arcRect, 90, 90)

        Path.CloseFigure()
        Return Path
    End Function

    Function GetRoundedRectPath(ByVal rect As Rectangle, ByVal radius1 As Integer, ByVal radius2 As Integer, ByVal radius3 As Integer, ByVal radius4 As Integer) As GraphicsPath
        Dim diameter1 As Integer = 2 * radius1
        Dim diameter2 As Integer = 2 * radius2
        Dim diameter3 As Integer = 2 * radius3
        Dim diameter4 As Integer = 2 * radius4
        Dim arcRect As Rectangle =
          New Rectangle(rect.Location, New Size(diameter1, diameter1))
        Dim path As GraphicsPath = New GraphicsPath()

        ' top left
        Path.AddArc(arcRect, 180, 90)

        ' top right
        arcRect = New Rectangle(rect.Location, New Size(diameter2, diameter2))
        arcRect.X = rect.Right - diameter2
        Path.AddArc(arcRect, 270, 90)

        ' bottom right
        arcRect = New Rectangle(rect.Location, New Size(diameter3, diameter3))
        arcRect.X = rect.Right - diameter3
        arcRect.Y = rect.Bottom - diameter3
        Path.AddArc(arcRect, 0, 90)

        ' bottom left
        arcRect = New Rectangle(rect.Location, New Size(diameter4, diameter4))
        arcRect.Y = rect.Bottom - diameter4
        Path.AddArc(arcRect, 90, 90)

        Path.CloseFigure()
        Return Path
    End Function

    Public Enum BorderLineType
        SimpleLine = 0
        DoubleLine = 1
        ThinThick = 2
        ThickThin = 3
        Triple = 4
    End Enum

    Public Function CreateSection(ByVal secn As Section) As Bitmap
        Dim ang As Integer = 45
        Dim xx As Integer

        Try
            If secn.h > 0 Then
                Dim bmp As New Bitmap(RP.w, secn.h)
                Dim g As Graphics
                g = Graphics.FromImage(bmp)
                g.Clear(secn.bcolor)
                If Not (secn.img Is Nothing) Then
                    g.DrawImage(secn.img, New Rectangle(0, 0, RP.w, secn.h))
                End If

                'g.DrawLine(New Pen(color.Silver, 1), 0, 0, RP.w, 0)
                'g.DrawPath(new Pen(new SolidBrush(secn.BorderColor), secn.BorderWidth), GetPath(new Rectangle(0 , 0, RP.w, secn.h), secn.BorderRad))
                If secn.BorderWidth > 0 Then
                    Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                    If CInt(secn.BorderSides) = 15 Then
                        g.DrawRectangle(New Pen(New SolidBrush(secn.Bordercolor), secn.BorderWidth), New Rectangle(0, 0, RP.w - 1, secn.h - 1))
                    Else
                        If secn.BorderSides And AnchorStyles.Top Then
                            g.DrawLine(spn, 0, 0, RP.w - 1, 0)
                        End If
                        If secn.BorderSides And AnchorStyles.Right Then
                            g.DrawLine(spn, RP.w - 1, 0, RP.w - 1, secn.h - 1)
                        End If

                        If secn.BorderSides And AnchorStyles.Bottom Then
                            g.DrawLine(spn, 0, secn.h - 1, RP.w - 1, secn.h - 1)
                        End If

                        If secn.BorderSides And AnchorStyles.Left Then
                            g.DrawLine(spn, 0, 0, 0, secn.h - 1)
                        End If
                    End If
                End If

                Dim t As Item
                'Dim nRec As Rectangle
                Dim sel As Boolean = False
                Dim selRec As Rectangle
                For j As Integer = 0 To secn.AR.Count - 1
                    t = secn.AR(j)
                    If t.selected Then
                        sel = True
                        selRec = t.Rec
                    End If
                    Dim nRec As New Rectangle(t.Rec.X, t.Rec.Y, t.Rec.Width, t.Rec.Height)
                    Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)

                    'pn.Alignment = PenAlignment.Inset
                    Select Case t.ItemType
                        Case "Label"
                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                            getRecShape(g, t)

                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                            If t.vh Then
                                nflag += StringFormatFlags.DirectionVertical
                            End If
                            If t.di Then
                                nflag += StringFormatFlags.DirectionRightToLeft
                            End If

                            Select Case t.Drawgrados
                                Case 0
                                    ang = 0
                                Case 90
                                    ang = 90
                                Case 180
                                    ang = 180
                                Case 270
                                    ang = 270
                            End Select

                            If ang = 0 Then
                                If t.fitInBox Then
                                    FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                Else
                                    g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                End If

                            Else

                                Dim ls As SizeF
                                Dim ntras As Integer
                                ls = LongitudString(g, t.fnt, "=" & t.Dfield)

                                If ang = 90 Then
                                    ntras = Math.Abs(CInt(System.Math.Cos(ang * (Math.PI / 180)) * ls.ToSize.Width))
                                    g.TranslateTransform(nRec.X + t.pd.Left + ls.Height, nRec.Y + t.pd.Top)
                                ElseIf ang = 180 Then
                                    ntras = Math.Abs(Truncar(System.Math.Cos(ang * (Math.PI / 180)) * ls.ToSize.Width))
                                    g.TranslateTransform(nRec.X + ls.ToSize.Width * 3 / 4, nRec.Y + t.pd.Top + ls.ToSize.Height)
                                ElseIf ang = 270 Then
                                    ntras = Math.Abs(Truncar(System.Math.Sin(ang * (Math.PI / 180)) * ls.ToSize.Width / 2))
                                    g.TranslateTransform(nRec.X, nRec.Y + (ls.Width / 2) + ls.ToSize.Height)
                                Else
                                    g.TranslateTransform(nRec.X + t.pd.Left, nRec.Y + ls.ToSize.Width / 2)
                                End If
                                g.RotateTransform(ang)



                                If ang = 90 Then
                                    ntras = Math.Abs(CInt(System.Math.Cos(ang * (Math.PI / 180)) * ls.ToSize.Width))
                                    g.TranslateTransform(nRec.X + t.pd.Left + ls.Height, nRec.Y + t.pd.Top)
                                    'g.TranslateTransform(nRec.X + t.pd.Left, nRec.Y + t.pd.Top)
                                ElseIf ang = 180 Then
                                    ntras = Math.Abs(Truncar(System.Math.Cos(ang * (Math.PI / 180)) * ls.ToSize.Width))
                                    'g.TranslateTransform(nRec.X + ntras + ls.ToSize.Height, nRec.Y + t.pd.Top + ls.ToSize.Height)
                                    g.TranslateTransform(nRec.X + ls.ToSize.Width * 3 / 4, nRec.Y + t.pd.Top + ls.ToSize.Height)
                                ElseIf ang = 270 Then
                                    ntras = Math.Abs(Truncar(System.Math.Sin(ang * (Math.PI / 180)) * ls.ToSize.Width / 2))
                                    'g.TranslateTransform(nRec.X, nRec.Y + ntras + ls.ToSize.Height)
                                    g.TranslateTransform(nRec.X, nRec.Y + (ls.Width / 2) + ls.ToSize.Height)
                                Else
                                    g.TranslateTransform(nRec.X + t.pd.Left, nRec.Y + ls.ToSize.Width / 2)
                                End If


                                g.RotateTransform(ang)

                                'gCanvas.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                If t.fitInBox Then
                                    FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                Else
                                    'g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign))
                                    'RectangleF.op_Implicit(Label1.ClientRectangle))
                                    'g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    g.DrawString(t.txt.Trim, t.fnt, New SolidBrush(t.fcolor), New Rectangle(0, 0, nRec.Height + ls.Width, nRec.Width), CA2SF(t.txtalign, nflag))
                                End If
                                g.ResetTransform()
                            End If

                            'g.ResetTransform()

                        Case "Shape"
                            pn.DashStyle = t.estilo_linea
                            pn.Width = t.bw
                            Select Case t.Drawshape
                                Case shapeItem.SType.Rectangulo
                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                    getRecShape(g, t)
                                Case shapeItem.SType.Elipse
                                    g.FillEllipse(New SolidBrush(t.BackColor), nRec)
                                    g.DrawEllipse(pn, nRec) 'aaaa
                                Case shapeItem.SType.Linea_horizontal
                                    pn.DashStyle = t.estilo_linea
                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                    g.FillRectangle(New SolidBrush(t.BorderColor), New Rectangle(nRec.X, nRec.Y, nRec.Width, t.BorderWidth))
                                    g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y)

                                Case shapeItem.SType.Linea_vertical
                                    pn.DashStyle = t.estilo_linea
                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                    g.FillRectangle(New SolidBrush(t.BorderColor), New Rectangle(nRec.X, nRec.Y, t.BorderWidth, nRec.Height))
                                    g.DrawLine(pn, nRec.X, nRec.Y, nRec.X, nRec.Y + nRec.Height)

                                Case shapeItem.SType.Linea_libre
                                    pn.DashStyle = t.estilo_linea
                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                    g.FillRectangle(New SolidBrush(t.BorderColor), New Rectangle(nRec.X, nRec.Y, t.BorderWidth, nRec.Height))
                                    'g.DrawLine(pn, nRec.X, nRec.Y, nRec.Width, nRec.Height)
                                    If Math.Abs(nRec.Width) > Math.Abs(nRec.Height) Then
                                        'g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y)
                                        xx = CInt(Math.Sqrt(nRec.Width ^ 2 - nRec.Height ^ 2))
                                        g.DrawLine(pn, nRec.X, nRec.Y, (xx), nRec.Y + nRec.Height)
                                    Else
                                        xx = CInt(Math.Sqrt(nRec.Height ^ 2 - nRec.Width ^ 2))
                                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.Width, xx)
                                        'g.DrawLine(pn, nRec.X, nRec.Y, nRec.X, nRec.Y + nRec.Height)
                                    End If

                                    g.DrawLine(pn, New PointF(nRec.X, nRec.Y), New PointF(nRec.X + nRec.Width, nRec.Y + nRec.Height))


                            End Select
                        Case "Image"
                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                            If Not (t.img Is Nothing) Then
                                g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                            End If
                            getRecShape(g, t)

                        Case "DataLabel"
                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                            getRecShape(g, t)

                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                            If t.vh Then
                                nflag += StringFormatFlags.DirectionVertical
                            End If
                            If t.di Then
                                nflag += StringFormatFlags.DirectionRightToLeft
                            End If
                            'StringFormatFlags.FitBlackBox

                            Select Case t.Drawgrados
                                Case 0
                                    ang = 0
                                Case 90
                                    ang = 90
                                Case 180
                                    ang = 180
                                Case 270
                                    ang = 270
                            End Select

                            If ang = 0 Then
                                If t.fitInBox Then
                                    FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                Else
                                    g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                End If

                                'g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                If Trim(t.Dfield) = "" Then
                                    If t.fitInBox Then
                                        FitText(g, "<Sin_mombre_de_campo>", t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    Else
                                        g.DrawString("<Sin_mombre_de_campo>", t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    End If
                                Else
                                    If t.fitInBox Then
                                        FitText(g, "=" & t.Dfield, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    Else
                                        g.DrawString("=" & t.Dfield, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    End If
                                End If
                            Else
                                Dim ls As SizeF
                                Dim ntras As Integer
                                ls = LongitudString(g, t.fnt, "=" & t.Dfield)

                                If ang = 90 Then
                                    ntras = Math.Abs(CInt(System.Math.Cos(ang * (Math.PI / 180)) * ls.ToSize.Width))
                                    g.TranslateTransform(nRec.X + t.pd.Left + ls.Height, nRec.Y + t.pd.Top)
                                ElseIf ang = 180 Then
                                    ntras = Math.Abs(Truncar(System.Math.Cos(ang * (Math.PI / 180)) * ls.ToSize.Width))
                                    g.TranslateTransform(nRec.X + ls.ToSize.Width * 3 / 4, nRec.Y + t.pd.Top + ls.ToSize.Height)
                                ElseIf ang = 270 Then
                                    ntras = Math.Abs(Truncar(System.Math.Sin(ang * (Math.PI / 180)) * ls.ToSize.Width / 2))
                                    g.TranslateTransform(nRec.X, nRec.Y + (ls.Width / 2) + ls.ToSize.Height)
                                Else
                                    g.TranslateTransform(nRec.X + t.pd.Left, nRec.Y + ls.ToSize.Width / 2)
                                End If
                                g.RotateTransform(ang)

                                If t.fitInBox Then
                                    FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                Else
                                    g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                End If

                                'g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
                                If Trim(t.Dfield) = "" Then
                                    If t.fitInBox Then
                                        FitText(g, "", t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    Else
                                        g.DrawString("", t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    End If
                                Else
                                    If t.fitInBox Then
                                        FitText(g, "=" & t.Dfield, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    Else
                                        g.DrawString("=" & t.Dfield, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                    End If
                                End If
                            End If


                        Case "DataImage"
                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                            g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                            getRecShape(g, t)
                            Dim sf As New StringFormat
                            sf.Alignment = StringAlignment.Center
                            sf.LineAlignment = StringAlignment.Center
                            If Trim(t.Dfield) = "" Then
                                FitText(g, "<Sin_imagen>", t.fnt, Brushes.Black, nRec, sf)
                            Else
                                FitText(g, "=" & t.Dfield, t.fnt, Brushes.Black, nRec, sf)
                            End If

                        Case "Barcode"
                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                            g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                            getRecShape(g, t)
                            Dim cb As Object

                            If t.Drawbarcode = Item.SBarcode.Barcode39 Then
                                cb = New Barcodes.Barcode39
                            ElseIf t.Drawbarcode = Item.SBarcode.Barcode128 Then
                                cb = New Barcodes.Barcode128
                            ElseIf t.Drawbarcode = Item.SBarcode.BarcodeEAN13 Then
                                cb = New Barcodes.Barcodeean13
                            ElseIf t.Drawbarcode = Item.SBarcode.BarcodeQR Then
                                cb = New Barcodes.BarcodeQR
                            End If

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
                            'Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39

                            Dim cb As Object

                            If t.Drawbarcode = Item.SBarcode.Barcode39 Then
                                cb = New Barcodes.Barcode39
                            ElseIf t.Drawbarcode = Item.SBarcode.Barcode128 Then
                                cb = New Barcodes.Barcode128
                            ElseIf t.Drawbarcode = Item.SBarcode.BarcodeEAN13 Then
                                cb = New Barcodes.Barcodeean13
                            ElseIf t.Drawbarcode = Item.SBarcode.BarcodeQR Then
                                cb = New Barcodes.BarcodeQR
                            End If
                            't.Lados = AnchorStyles.None
                            cb.ShowString = t.mt
                            cb.IncludeCheckSumDigit = t.dc
                            cb.TextFont = t.fnt
                            cb.TextColor = t.fcolor
                            g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)

                    End Select
                Next

                Return bmp
                g.Dispose()
            Else
                Return New Bitmap(1, 1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Sección de informe de errores")
            Return New Bitmap(1, 1)
        End Try

    End Function

    Private Function NewMethod(g As Graphics, t As Item, ls As SizeF) As SizeF
        ls = LongitudString(g, t.fnt, t.txt)
        Return ls
    End Function

    Private Function MeasureString(g As Graphics, fnt As Font, txt As String) As Integer
        Throw New NotImplementedException()
    End Function

    Public Sub getRecShape(ByVal g As Graphics, ByVal t As Item, Optional ByVal x As Integer = 0, Optional ByVal y As Integer = 0, Optional ByVal Design As Boolean = True)
        Dim nRec As New Rectangle(x + t.Rec.X, y + t.Rec.Y, t.Rec.Width, t.Rec.Height)
        Dim pn As New Pen(New SolidBrush(IIf(Design, Color.FromArgb(192, 192, 192), Color.Transparent)), t.bw)
        'Dim pn As New Pen(New SolidBrush(Color.Transparent), t.bw)

        pn.DashStyle = t.estilo_linea

        If t.bw > 0 Then
            If t.BorderRad > 0 Then
                g.DrawPath(pn, GetPath(nRec, t.BorderRad))
            Else
                If CInt(t.BorderSides) = 15 Then
                    g.DrawRectangle(pn, nRec)
                Else
                    pn.LineJoin = LineJoin.MiterClipped
                    If t.BorderSides And AnchorStyles.Top Then
                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y)
                        'path.AddLine(new Point(nRec.X, nRec.Y), new Point(nRec.X + nRec.Width, nrec.Y))
                    End If
                    If t.BorderSides And AnchorStyles.Right Then
                        g.DrawLine(pn, nRec.X + nRec.Width, nRec.Y, nRec.X + nRec.Width, nRec.Y + nRec.Height)
                        'path.AddLine(new Point(nRec.X + nRec.Width, nRec.Y), new Point(nRec.X + nRec.Width, nrec.Y + nrec.Height))
                    End If

                    If t.BorderSides And AnchorStyles.Bottom Then
                        g.DrawLine(pn, nRec.X, nRec.Y + nRec.Height, nRec.X + nRec.Width, nRec.Y + nRec.Height)
                        'path.AddLine(new Point(nRec.X + nRec.Width, nrec.Y + nrec.Height), new Point(nRec.X, nRec.Y + nrec.Height))
                    End If

                    If t.BorderSides And AnchorStyles.Left Then
                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.X, nRec.Y + nRec.Height)
                        'path.AddLine(new Point(nRec.X, nrec.Y + nrec.Height), new Point(nRec.X, nRec.Y))
                    End If

                    If t.BorderSides And AnchorStyles.Top Then
                        If t.BorderSides And AnchorStyles.Right Then
                            Dim path As New GraphicsPath()
                            path.StartFigure()
                            path.AddLine(New Point(nRec.X, nRec.Y), New Point(nRec.X + nRec.Width, nRec.Y))
                            path.AddLine(New Point(nRec.X + nRec.Width, nRec.Y), New Point(nRec.X + nRec.Width, nRec.Y + nRec.Height))
                            g.DrawPath(pn, path)
                        End If
                    End If

                    If t.BorderSides And AnchorStyles.Right Then
                        If t.BorderSides And AnchorStyles.Bottom Then
                            Dim path As New GraphicsPath()
                            path.StartFigure()
                            path.AddLine(New Point(nRec.X + nRec.Width, nRec.Y), New Point(nRec.X + nRec.Width, nRec.Y + nRec.Height))
                            path.AddLine(New Point(nRec.X + nRec.Width, nRec.Y + nRec.Height), New Point(nRec.X, nRec.Y + nRec.Height))
                            g.DrawPath(pn, path)
                        End If
                    End If

                    If t.BorderSides And AnchorStyles.Bottom Then
                        If t.BorderSides And AnchorStyles.Left Then
                            Dim path As New GraphicsPath()
                            path.StartFigure()
                            path.AddLine(New Point(nRec.X + nRec.Width, nRec.Y + nRec.Height), New Point(nRec.X, nRec.Y + nRec.Height))
                            path.AddLine(New Point(nRec.X, nRec.Y + nRec.Height), New Point(nRec.X, nRec.Y))
                            g.DrawPath(pn, path)
                        End If
                    End If

                    If t.BorderSides And AnchorStyles.Top Then
                        If t.BorderSides And AnchorStyles.Left Then
                            Dim path As New GraphicsPath()
                            path.StartFigure()
                            path.AddLine(New Point(nRec.X, nRec.Y + nRec.Height), New Point(nRec.X, nRec.Y))
                            path.AddLine(New Point(nRec.X, nRec.Y), New Point(nRec.X + nRec.Width, nRec.Y))
                            g.DrawPath(pn, path)
                        End If
                    End If
                End If
            End If
            'g.DrawPath(pn, GetPath(lRec, 0))
        End If
    End Sub

    Public Function CA2SF(ByVal ca As ContentAlignment, Optional sff As Integer = 0) As StringFormat
        Dim sf As New StringFormat
        Select Case ca
            Case ContentAlignment.TopLeft
                sf.Alignment = StringAlignment.Near
                sf.LineAlignment = StringAlignment.Near
            Case ContentAlignment.TopCenter
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Near
            Case ContentAlignment.TopRight
                sf.Alignment = StringAlignment.Far
                sf.LineAlignment = StringAlignment.Near
            Case ContentAlignment.MiddleLeft
                sf.Alignment = StringAlignment.Near
                sf.LineAlignment = StringAlignment.Center
            Case ContentAlignment.MiddleCenter
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Center
            Case ContentAlignment.MiddleRight
                sf.Alignment = StringAlignment.Far
                sf.LineAlignment = StringAlignment.Center
            Case ContentAlignment.BottomLeft
                sf.Alignment = StringAlignment.Near
                sf.LineAlignment = StringAlignment.Far
            Case ContentAlignment.BottomCenter
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Far
            Case ContentAlignment.BottomRight
                sf.Alignment = StringAlignment.Far
                sf.LineAlignment = StringAlignment.Far
        End Select
        sf.FormatFlags = sff
        Return sf


    End Function

    Public Function TextFromDTx(ByVal key As String, ByVal ind As Integer, ByVal DT As DataTable, ByVal page As Integer, ByVal pages As Integer) As String
        Dim dl As String = ""
        Dim opst As String = ". + - % * / ( ) ! ^ & < >"
        Try
            If key <> "" Then
                For i As Integer = 0 To DT.Columns.Count - 1
                    If key.IndexOf(DT.Columns(i).ColumnName, stringcomparison.OrdinalIgnoreCase) > -1 Then
                        Dim fstr As String = ""
                        Dim str As String = ""
                        Dim substr As String = ""
                        Dim close As Boolean = True
                        Dim si As Integer = 0
                        Dim ei As Integer = 0
                        For j As Integer = 0 To key.Length - 1
                            If key(j) = "'" Or key(j) = Chr(34) Then
                                If close = False Then
                                    If j > si + 1 Then
                                        str = key.Substring(si + 1, j - si - 1).ToLower
                                        fstr &= strings.Replace(str, DT.Columns(i).ColumnName.ToLower, DT.Rows(ind).Item(key),,, CompareMethod.Text)
                                        close = True
                                    End If
                                Else
                                    fstr &= key.Substring(si, j - si)
                                    si = j
                                    close = False
                                End If
                            End If
                        Next
                        dl = fstr
                    End If
                Next
                'dl = DT.Rows(ind).Item(key)
                'If IsNumeric(DT.Rows(ind).Item(key)) And Not IsEmpty(DT.Rows(ind).Item(key)) Then

                'Else

                'End If
            End If
        Catch ex As Exception
            'messagebox.Show(ex.Message, "Data label Error")
            dl = ex.Message & ind
        End Try
        Try
            Dim ev As New Evaluator
            dl = ev.Eval(dl)
        Catch ex As Exception
            dl = ex.Message
        End Try
        Return dl
    End Function

    Public Function TextFromDTPrev(ByVal key As String, ByVal ind As Integer, ByVal DT As DataTable) As String
        Dim dl As String = ""
        Dim opst As String = ". + - % * / ( ) ! ^ & < >"
        Try
            If key <> "" Then
                For i As Integer = 0 To DT.Columns.Count - 1
                    If key.IndexOf(DT.Columns(i).ColumnName, StringComparison.OrdinalIgnoreCase) > -1 Then
                        Dim fstr As String = ""
                        Dim str As String = ""
                        Dim substr As String = ""
                        Dim close As Boolean = True
                        Dim si As Integer = 0
                        Dim ei As Integer = 0
                        For j As Integer = 0 To key.Length - 1
                            If key(j) = "'" Or key(j) = Chr(34) Then
                                If close = False Then
                                    If j > si + 1 Then
                                        str = key.Substring(si + 1, j - si - 1).ToLower
                                        fstr &= Strings.Replace(str, DT.Columns(i).ColumnName.ToLower, DT.Rows(ind).Item(key),,, CompareMethod.Text)
                                        close = True
                                    End If
                                Else
                                    fstr &= key.Substring(si, j - si)
                                    si = j
                                    close = False
                                End If
                            End If
                        Next
                        dl = fstr
                    End If
                Next
            End If
        Catch ex As Exception
            'messagebox.Show(ex.Message, "Data label Error")
            dl = ex.Message & ind
        End Try
        'Try
        '    Dim ev As New Evaluator
        '    dl = ev.Eval(dl)
        'Catch ex As Exception
        '    dl = ex.Message
        'End Try
        Return dl
    End Function

    Public Function TextFromDT(ByVal key As String, ByVal ind As Integer, ByVal secn As Section, ByVal page As Integer, ByVal pages As Integer, ByRef oCv As libcomunes.ControlVariables) As String
        Dim dl As String = ""

        'Dim opst As String = ". + - % * / ( ) ! ^ & < >"

        Try
            If key <> "" Then
                dl = Strings.Replace(key, "[fecha]", "'" & DateTime.Now.ToShortDateString & "'",,, CompareMethod.Text)
                dl = Strings.Replace(dl, "[fechalarga]", "'" & DateTime.Now.ToLongDateString & "'",,, CompareMethod.Text)
                dl = Strings.Replace(dl, "[hora]", "'" & DateTime.Now.ToShortTimeString & "'",,, CompareMethod.Text)
                dl = Strings.Replace(dl, "[horalarga]", "'" & DateTime.Now.ToLongTimeString & "'",,, CompareMethod.Text)
                dl = Strings.Replace(dl, "[paginas]", pages,,, CompareMethod.Text)
                dl = Strings.Replace(dl, "[pagina]", page,,, CompareMethod.Text)
                If Left(key.Trim, 1) = "[" And Right(key.Trim, 1) = "]" Then
                    dl = Strings.Replace(dl, key.Trim, ObtenerValorVar(key.Trim),,, CompareMethod.Text)
                    Return dl
                End If
                If Left(key.Trim, 1) = "@" Then
                    dl = Strings.Replace(dl, key.Trim, oCv(key.Trim),,, CompareMethod.Text) ' aaa 
                    Return dl
                End If
                Dim DT As DataTable = secn.dt

                dl = Strings.Replace(dl, "Registros()", DT.Rows.Count,,, CompareMethod.Text)

                Dim Linea As String = DT.Rows(ind).Item("num_linea").ToString
                Dim RsCtasRows() As DataRow
                RsCtasRows = secn.dt.Select("num_linea = " & Linea & " AND cod_variable = '" & key & "'")

                If RsCtasRows.Length > 0 Then
                    Dim str As String = RsCtasRows(0)("valor_dato").ToString.Trim
                    If IsNumeric(str) Then
                        dl = Strings.Replace(dl, key, str,,, CompareMethod.Text)
                    Else
                        'dl = Strings.Replace(dl, key, """" & str & """",,, CompareMethod.Text)
                        dl = Strings.Replace(dl, key, "" & str,,, CompareMethod.Text)
                    End If
                Else
                    'dl = Strings.Replace(dl, key, 0,,, CompareMethod.Text)
                    dl = Strings.Replace(dl, key, "",,, CompareMethod.Text)
                End If
                'For j As Integer = 0 To DT.Columns(ind).Count - 1
                '    Dim str As String = DT.Rows(ind).Item(DT.Columns(j).ColumnName).ToString
                '    If Not String.IsNullOrEmpty(str) Then
                '        If IsNumeric(str) Then
                '            dl = Strings.Replace(dl, "[" & DT.Columns(j).ColumnName & "]", str,,, CompareMethod.Text)
                '        Else
                '            dl = Strings.Replace(dl, "[" & DT.Columns(j).ColumnName & "]", """" & str & """",,, CompareMethod.Text)
                '        End If
                '    End If
                'Next

            End If
        Catch ex As Exception
            'messagebox.Show(ex.Message, "Data label Error")
            dl = "'" & ex.Message & key & "'"
        End Try
        'msgbox(dl)
        'Try
        '    Dim ev As New Evaluator
        '    dl = ev.Eval(dl)
        'Catch ex As Exception
        '    dl = dl & ex.Message
        'End Try
        Return dl
    End Function

    Public Function ImageFromDT(ByVal key As String, ByVal ind As Integer, ByVal DT As DataTable) As Image
        Dim dl As Image = Nothing
        Dim EsPath As Boolean

        Try
            Dim Linea As String = DT.Rows(ind).Item("num_linea").ToString
            Dim RsCtasRows() As DataRow
            Dim Path As String
            Dim Imagen As Image

            RsCtasRows = DT.Select("num_linea = " & Linea & " AND cod_variable = '" & key & "'")
            If RsCtasRows.Length > 0 Then
                If RsCtasRows(0)("valor_dato").ToString.Trim.ToLower = "imagen" Then
                    Path = RsCtasRows(0)("path").ToString.Trim.ToLower
                    Try 'aaa
                        If File.Exists(Path) Then
                            dl = Image.FromFile(Path)
                        End If
                        Return dl
                        'Using fs As New System.IO.FileStream(Path, IO.FileMode.Open)
                        '    Imagen = New Bitmap(Image.FromStream(fs))
                        '    Return Imagen
                        'End Using
                    Catch ex As Exception
                        Dim msg As String = "Archivo: " & Path & Environment.NewLine & Environment.NewLine & "Error: " & ex.ToString
                        MessageBox.Show(msg, "Error abriendo archivo")
                    End Try
                End If
            Else
                Return Nothing
            End If

            'RsCtasRows = DT.Select("num_linea = " & Linea & " AND cod_variable = '" & key & "'")
            'If RsCtasRows.Length > 0 Then
            '    Dim str As String = RsCtasRows(0)("valor_dato").ToString.Trim
            '    If IsNumeric(str) Then
            '        dl = Strings.Replace(dl, key, str,,, CompareMethod.Text)
            '    Else
            '        'dl = Strings.Replace(dl, key, """" & str & """",,, CompareMethod.Text)
            '        dl = Strings.Replace(dl, key, "" & str,,, CompareMethod.Text)
            '    End If
            'Else
            '    'dl = Strings.Replace(dl, key, 0,,, CompareMethod.Text)
            '    dl = Strings.Replace(dl, key, "",,, CompareMethod.Text)
            'End If


            'If key <> "" Then
            '    For j As Integer = 0 To DT.Columns.Count - 1
            '        If key.ToLower.Contains("[" & DT.Columns(j).ColumnName & "]") Then
            '            'msgbox(DT.Rows(ind).Item(Dt.Columns(j).ColumnName).tostring)
            '            dl = Base64ToImage(DT.Rows(ind).Item(DT.Columns(j).ColumnName).ToString)
            '            Exit For
            '        End If
            '    Next
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Data Image Error")
            Return Nothing
        End Try
        'Return dl
    End Function

    Public Function obtenerImagen(rutaOrden As String) As String

        Try
            Dim bytesImagen As Byte() = System.IO.File.ReadAllBytes(rutaOrden)
            Dim imagenBase64 As String = Convert.ToBase64String(bytesImagen)

            Return imagenBase64

            'Dim tipoContenido As String

            'Select Case Path.GetExtension(rutaOrden)
            '    Case ".jpg"
            '        tipoContenido = "image/jpg"
            '    Case ".gif"
            '        tipoContenido = "image/gif"
            '    Case ".png"
            '        tipoContenido = "image/png"
            '    Case ".bmp"
            '        tipoContenido = "image/bmp"

            '    Case Else
            '        Return Nothing

            'End Select

            'Return String.Format("data:{0};base64,{1}", tipoContenido, imagenBase64)

        Catch ex As Exception
            MsgBox("Error cargando imagen " & ex.Message & " (" & ex.ToString & " )")
            Return ""
        End Try

    End Function

    Public Function ByteToImage(ByVal b As Object) As Image
        Dim im As Image = Nothing
        Try
            Dim ImageByte As Byte()
            ImageByte = TryCast(b, Byte())
            If ImageByte IsNot Nothing Then
                Using productImageStream As Stream = New System.IO.MemoryStream(ImageByte)
                    im = Image.FromStream(productImageStream)
                End Using
            End If
        Catch ex As Exception
            msgbox(ex.Message)
        End Try

        Return im
    End Function

    Public Function GetPath(ByVal rc As Rectangle, ByVal r As Int32) As GraphicsPath
        Dim x As Int32 = rc.X, y As Int32 = rc.Y, w As Int32 = rc.Width, h As Int32 = rc.Height
        r = r << 1
        Dim path As GraphicsPath = New GraphicsPath()
        If r > 0 Then
            If (r > h) Then r = h
            If (r > w) Then r = w
            path.AddArc(x, y, r, r, 180, 90)
            path.AddArc(x + w - r, y, r, r, 270, 90)
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90)
            path.AddArc(x, y + h - r, r, r, 90, 90)
            path.CloseFigure()
        Else
            path.AddRectangle(rc)
        End If
        Return path
    End Function

    Public Function RoundImageConers(ByVal img As Image, ByVal rec As Rectangle, ByVal rad As Integer) As Image
        Try
            Dim newImg As New Bitmap(rec.Width, rec.Height)
            Dim nRec As New Rectangle(0, 0, rec.Width, rec.Height)
            Dim g As Graphics = graphics.FromImage(newImg)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            'g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.SetClip(getpath(nrec, rad), CombineMode.Replace)
            If Not (img Is Nothing) Then
                g.DrawImage(img, nrec)
            End If
            g.Dispose
            Return newImg
        Catch ex As Exception
            Return New Bitmap(1, 1)
        End Try

    End Function

    Public Sub FitText(ByVal g As Graphics, ByVal str As String, ByVal f As Font, ByVal br As Brush, ByVal rec As Rectangle, ByVal sf As StringFormat)
        Dim s As Integer = f.size
        Dim f2 As New Font(f.name, s, f.style)

        If str.Length > 0 Then
            While g.MeasureString(str, f2, New SizeF(rec.Width, rec.Height * 100), sf).Height > rec.Height And f2.Size > 3
                s = s - 1
                f2 = New Font(f.Name, s, f.Style)
            End While
        End If
        'g.DrawString(str, f2, br, rec, sf)
        g.DrawString(str, f2, br, rec.X, rec.Y, sf)

    End Sub

    Public Sub FitTextCodebar(ByVal g As Graphics, ByVal str As String, ByVal f As Font, ByVal br As Brush, ByVal rec As Rectangle, ByVal sf As StringFormat)
        Dim s As Integer = f.Size
        Dim f2 As New Font(f.Name, s, f.Style)
        If str.Length > 0 Then
            While g.MeasureString(str, f2, New SizeF(rec.Width, rec.Height * 1000), sf).Height > rec.Height And f2.Size > 3
                s = s - 1
                f2 = New Font(f.Name, s, f.Style)
            End While
        End If
        g.DrawString(str, f2, br, rec, sf)
    End Sub


    Public Function NewItem(ByVal nm As String, ByVal AR As List(Of Item)) As String
        Dim i As Integer
        Dim found As Boolean = True
        Dim str As String
        Do
            str = nm & i + 1
            found = False
            For j As Integer = 0 To AR.Count - 1
                Dim t As Item = AR(j)
                If str = t.nm Then
                    found = True
                End If
            Next
            i += 1
            'msgbox(found.ToString)
        Loop While (found)
        Return str
    End Function

    Public Function NuevoItem(ByVal nm As String, ByVal AR As List(Of Item)) As String
        Dim i As Integer
        Dim found As Boolean = True
        Dim str As String
        Do
            str = nm '& i + 1
            found = False
            For j As Integer = 0 To AR.Count - 1
                Dim t As Item = AR(j)
                If str = t.nm Then
                    found = True
                    Return ""
                End If
            Next
            i += 1
            'msgbox(found.ToString)
        Loop While (found)
        Return str
    End Function

    Public Function ImageToBase64(ByVal img As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            'Convert Image to byte()
            img.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()
            'Convert byte() to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function

    Public Function Base64ToImage(ByVal base64String As String) As Image
        If base64String = "" Then
            Return Nothing
        Else
            'Convert Base64 String to byte()
            Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
            Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

            'Convert byte() to Image
            ms.Write(imageBytes, 0, imageBytes.Length)
            Dim img As Image = Image.FromStream(ms, True)
            Return img
        End If
    End Function


    Public Function String2Base64(ByVal str As String) As String
        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(str)
        ' convert the byte array to a Base64 string
        Return Convert.ToBase64String(byt)
    End Function

    Public Function Base642String(ByVal str As String) As String
        If str = "" Then
            Return ""
        Else
            Try
                Dim b As Byte() = Convert.FromBase64String(str)
                Return System.Text.Encoding.UTF8.GetString(b)
            Catch ex As Exception
                Return ""
            End Try
        End If
    End Function

    Public Function Number2string(ByVal num As Decimal) As String
        Dim str As String = num.ToString
        Return strings.Replace(str, ",", ".")
    End Function


    Public Function fontToString(ByVal font As System.Drawing.Font) As String
        Dim tc As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
        Return tc.ConvertToString(font)
    End Function

    Public Function stringToFont(ByVal str As String) As System.Drawing.Font
        Dim tc As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
        Return CType(tc.ConvertFromString(str), Font)
    End Function

    'Public Function ReportToString(ByVal rpt As Report) As String
    '    Dim str As String = "LNSoft Report Design version=1.1"
    '    Try
    '        str &= VBCRLF & "Report@Name:" & string2base64(Rpt.nm)
    '        str &= ";Width:" & Rpt.w
    '        str &= ";TopMargin:" & Rpt.topm
    '        str &= ";LeftMargin:" & Rpt.leftm
    '        str &= ";BottomMargin:" & Rpt.bottomm
    '        str &= ";Landscape:" & Rpt.Orn
    '        str &= ";MultiColumn:" & Rpt.mcol
    '        For i As Integer = 0 To Rpt.Sections.Count - 1
    '            Dim scn As Section = Rpt.Sections(i)
    '            'str &= VBCRLF & VBTAB & "Section@Name=" & string2base64(scn.nm)
    '            str &= VBCRLF & VBTAB & "Section@type:" & scn.GroupID
    '            str &= ";Name:" & scn.nm
    '            str &= ";Label:" & string2base64(scn.headerLabel)
    '            str &= ";Height:" & scn.h
    '            str &= ";Backcolor:" & scn.BackColor.ToArgb.tostring
    '            str &= ";Bordercolor:" & scn.BorderColor.ToArgb.tostring
    '            str &= ";BorderRadius:" & scn.BorderRad
    '            str &= ";BorderWidth:" & scn.BorderWidth
    '            str &= ";Provider:" & CInt(scn._Provider)
    '            str &= ";ConnectionString:" & string2base64(scn.ConnectionString)
    '            str &= ";SQL:" & string2base64(scn.SQL)
    '            If scn.img Is Nothing Then
    '                str &= ";Image:00"
    '            Else
    '                str &= ";Image:" & ImageToBase64(scn.img, system.Drawing.Imaging.ImageFormat.Png)
    '            End If
    '            str &= ";AltBackcolor:" & scn.abcolor.ToArgb.tostring
    '            If scn.aimg Is Nothing Then
    '                str &= ";AltImage:00"
    '            Else
    '                str &= ";AltImage:" & ImageToBase64(scn.aimg, system.Drawing.Imaging.ImageFormat.Png)
    '            End If

    '            For j As Integer = 0 To scn.AR.Count - 1
    '                Dim itm As Item = scn.AR(j)
    '                str &= VBCRLF & VBTAB & VBTAB & "Control@Type:" & itm.ItemType
    '                str &= ";Text:" & string2base64(itm.txt).ToString
    '                str &= ";Font:" & fontToString(itm.fnt)
    '                str &= ";FontColor:" & itm.fcolor.ToArgb.tostring
    '                str &= ";TextAlign:" & CInt(itm.txtalign)
    '                str &= ";BackColor:" & itm.BackColor.ToArgb.tostring
    '                str &= ";BorderWidth:" & itm.bw
    '                str &= ";BorderColor:" & itm.BorderColor.ToArgb.tostring
    '                str &= ";BorderRadius:" & itm.BorderRad
    '                str &= ";BorderSide:" & CInt(itm.BorderSides)
    '                Dim rc As New RectangleConverter
    '                str &= ";Rectangle:" & rc.ConvertToString(itm.Rec)
    '                str &= ";Name:" & string2base64(itm.nm)
    '                If itm.img Is Nothing Then
    '                    str &= ";Image:00"
    '                Else
    '                    str &= ";Image:" & ImageToBase64(itm.img, system.Drawing.Imaging.ImageFormat.Png)
    '                End If

    '                str &= ";Shape:" & CInt(itm.Drawshape)
    '                str &= ";DataField:" & string2base64(itm.Dfield)
    '                str &= ";DataIndex:" & itm.DIndex.ToString
    '                Dim pc As New PaddingConverter
    '                str &= ";Padding:" & pc.ConvertToString(itm.pd)
    '                str &= ";AutoSize:" & itm.fitInBox
    '                str &= ";Format:" & String2Base64(itm.fmt)
    '                str &= ";dc:" & itm.dc
    '                str &= ";mt:" & itm.mt
    '            Next
    '        Next
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error procesando el informe")
    '    End Try
    '    Return str
    'End Function

    'Public Function ReportFromFile(ByVal fn As String) As Report
    '    Dim Rpt As Report
    '    Try
    '        Dim stringReader As String
    '        stringReader = My.Computer.FileSystem.ReadAllText(fn)
    '        rpt = ReportFromString(stringReader)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "No puedo abrir el archivo.")
    '    End Try
    '    Return rpt
    'End Function

    'Public Function ReportFromString(ByVal design As String) As Report
    '    Dim TRP As New Report
    '    Try
    '        Dim stringReader As String = strings.Replace(design, VBTAB, "")
    '        Dim lines() As String
    '        lines = strings.Split(stringReader, CChar(VBCRLF))
    '        If lines.Length > 6 Then
    '            If lines(0).Split("=")(0) = "Diseño de informularios e informes" Then
    '                If lines(0).Split("=")(1) <= "1.1" Then
    '                    'msgbox("Rsections")									
    '                    Dim RPstr() As String
    '                    RPstr = lines(1).Split("@")(1).Split(";")
    '                    For k As Integer = 0 To RPstr.Length - 1
    '                        Dim data() As String
    '                        data = RPstr(k).Split(":")
    '                        'msgbox(data(0))
    '                        Select Case data(0).ToLower
    '                            Case "Name".ToLower
    '                                TRP.nm = Base642String(data(1))
    '                            Case "Width".ToLower
    '                                TRP.w = Val(data(1))
    '                            Case "Landscape".ToLower
    '                                TRP.Orn = CBool(data(1))
    '                                printset.DefaultPageSettings.Landscape = TRP.Orn
    '                            Case "topmargin".ToLower
    '                                TRP.topm = Val(data(1))
    '                            Case "leftmargin".ToLower
    '                                TRP.leftm = Val(data(1))
    '                            Case "bottommargin".ToLower
    '                                TRP.bottomm = Val(data(1))
    '                            Case "multicolumn".ToLower
    '                                TRP.mcol = CBool(data(1))
    '                        End Select

    '                    Next
    '                    'msgbox("sections")							
    '                    For i As Integer = 1 To lines.Length - 1
    '                        lines(i) = Strings.Replace(lines(i), vbLf, "")
    '                        Dim secn As Section
    '                        RPstr = lines(i).Split("@")(1).Split(";")
    '                        'msgbox(lines(i).Split("@")(0))
    '                        Dim disc As String = LCase(Trim(lines(i).Split("@")(0)))
    '                        'msgbox(lines(i).Split("@")(1))
    '                        Select Case LCase(disc)
    '                            Case "report"

    '                            Case "section"
    '                                For k As Integer = 0 To RPstr.Length - 1
    '                                    Dim data() As String
    '                                    data = RPstr(k).Split(":")
    '                                    Dim comp As String = LCase(Trim(data(0)))
    '                                    'msgbox(data(1) & data(1).Length)
    '                                    Try
    '                                        'msgbox(comp)
    '                                        Select Case comp
    '                                            Case "type"
    '                                                'msgbox(data(1))
    '                                                If CInt(data(1)) > 4 Then
    '                                                    secn = New SectionGroup
    '                                                    secn.GroupID = CInt(data(1))
    '                                                Else
    '                                                    If CInt(data(1)) = 2 Then
    '                                                        secn = New SectionDetail
    '                                                    Else
    '                                                        secn = New Section
    '                                                    End If
    '                                                    secn.GroupID = CInt(data(1))
    '                                                End If
    '                                            Case "name"
    '                                                secn.nm = data(1)
    '                                            Case "label"
    '                                                secn.headerLabel = Base642String(data(1))
    '                                            Case "height"
    '                                                secn.h = Val(data(1))
    '                                            Case "borderradius"
    '                                                secn.BorderRad = Val(data(1))
    '                                            Case "borderwidth"
    '                                                secn.bw = Val(data(1))
    '                                            Case "bordercolor"
    '                                                secn.linecolor = Color.FromArgb(data(1))
    '                                            Case "backcolor"
    '                                                secn.bcolor = Color.FromArgb(data(1))
    '                                            Case "connectionstring"
    '                                                secn._Constr = Base642String(data(1))
    '                                            Case "sql"
    '                                                secn._sql = Base642String(data(1))
    '                                            Case "provider"
    '                                                secn._Provider = CType(Val(data(1)), Section.DataProv)
    '                                                    'msgbox(secn._Provider.ToString)
    '                                            Case "image"
    '                                                If data(1).Length > 5 Then
    '                                                    secn.img = Base64ToImage(data(1))
    '                                                Else
    '                                                    secn.img = Nothing
    '                                                End If
    '                                            Case "altbackcolor"
    '                                                secn.abcolor = Color.FromArgb(data(1))
    '                                            Case "altimage"
    '                                                If data(1).Length > 5 Then
    '                                                    secn.aimg = Base64ToImage(data(1))
    '                                                Else
    '                                                    secn.aimg = Nothing
    '                                                End If

    '                                        End Select
    '                                    Catch exx As Exception
    '                                        MessageBox.Show(exx.Message, "Error decodificando informe")
    '                                    End Try
    '                                Next
    '                                'msgbox(secn.Height)
    '                                TRP.Sections.Add(secn)
    '                            Case "control"
    '                                Dim t As Item
    '                                For k As Integer = 0 To RPstr.Length - 1
    '                                    Dim data() As String
    '                                    data = RPstr(k).Split(":")
    '                                    Dim comp As String = LCase(Trim(data(0)))

    '                                    Try
    '                                        Select Case comp
    '                                            Case "type"
    '                                                Dim rec As New Rectangle(0, 0, 50, 50)
    '                                                Dim type As String = LCase(Trim(data(1)))
    '                                                'msgbox(type)
    '                                                Select Case type
    '                                                    Case "label"
    '                                                        t = New LabelItem()
    '                                                    Case "shape"
    '                                                        t = New shapeItem(rec)
    '                                                    Case "image"
    '                                                        t = New ImageItem(rec)
    '                                                    Case "datalabel"
    '                                                        t = New DataLabel(rec)
    '                                                    Case "dataimage"
    '                                                        t = New DataImage(rec)
    '                                                    Case "barcode"
    '                                                        t = New BarcodeItem(rec)
    '                                                    Case "fbarcode"
    '                                                        t = New BarcodeFieldItem(rec)
    '                                                        't = New BarcodeItem(rec)
    '                                                    Case Else
    '                                                        t = New LabelItem()
    '                                                End Select
    '                                            Case "name"
    '                                                t.nm = Base642String(data(1))
    '                                            Case "text"
    '                                                t.txt = Base642String(data(1))
    '                                            Case "rectangle"
    '                                                Dim rc As New RectangleConverter
    '                                                t.Rec = rc.ConvertFromString(data(1))
    '                                            Case "backcolor"
    '                                                t.bcolor = Color.FromArgb(data(1))
    '                                            Case "bordercolor"
    '                                                t.linecolor = Color.FromArgb(data(1))
    '                                            Case "borderradius"
    '                                                t.BorderRad = Val(data(1))
    '                                            Case "borderwidth"
    '                                                t.bw = Val(data(1))
    '                                            Case "borderside"
    '                                                t.RecSides = CType(Val(data(1)), AnchorStyles)
    '                                            Case "font"
    '                                                t.fnt = stringToFont(data(1))
    '                                            Case "fontcolor"
    '                                                t.fcolor = Color.FromArgb(data(1))
    '                                            Case "textalign"
    '                                                t.txtalign = CType(Val(data(1)), ContentAlignment)
    '                                            Case "datafield"
    '                                                t.Dfield = Base642String(data(1))
    '                                            Case "dataindex"
    '                                                t.DIndex = Val(data(1))
    '                                            Case "image"
    '                                                If data(1).Length > 5 Then
    '                                                    t.img = Base64ToImage(data(1))
    '                                                Else
    '                                                    t.img = Nothing
    '                                                End If
    '                                            Case "padding"
    '                                                Dim pc As New PaddingConverter
    '                                                t.pd = pc.ConvertFromString(data(1))
    '                                            Case "autosize"
    '                                                t.fitInBox = CBool(data(1))
    '                                            Case "format"
    '                                                t.fmt = Base642String(data(1))
    '                                            Case "digito_control"
    '                                                t.dc = CBool(data(1))
    '                                            Case "ver_texto"
    '                                                t.mt = CBool(data(1))

    '                                        End Select
    '                                    Catch exx As Exception
    '                                        MessageBox.Show(exx.Message, "Error decodificando el contenido")
    '                                    End Try
    '                                Next
    '                                'msgbox(secn.Height)
    '                                secn.AR.Add(t)
    '                        End Select

    '                    Next

    '                Else
    '                    MsgBox("Versión inválida")
    '                End If
    '            Else
    '                MsgBox("Archivo de informe no válido")
    '            End If
    '        Else
    '            MsgBox("Archivo incompleto o dañado")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Imposible abrir file2.")
    '    End Try
    '    Return TRP
    'End Function

    Public Sub GetData(ByVal DT As datatable, ByVal sql As String, ByVal connstr As String, ByVal prov As Section.DataProv)
        Dim x As Object
        Select Case prov
            Case section.DataProv.Microsoft_Access_Database, section.DataProv.Microsoft_SQL_Server
                x = New MSConn(DT, sql, connstr)
            Case section.DataProv.MySQL_Database
                x = New MySqlClass(DT, sql, connstr)
            Case section.DataProv.Microsoft_ODBC
                x = New ODBCConn(DT, sql, connstr)
            Case Else

        End Select
    End Sub

    Public Function LongitudString(ByVal g As Graphics, ByVal ofont As Font, ByVal c As String) As SizeF
        Dim SILen As SizeF = New SizeF(0, 0)
        Dim rsize As SizeF
        Dim i As Integer

        rsize = g.MeasureString(c.ToString(), ofont)
        Return rsize

        'For i = 1 To c.Length
        '    rsize = MeasureCharacter(g, ofont, Mid(c, i, 1))
        '    SILen.Width += rsize.Width
        '    If rsize.Height > SILen.Height Then
        '        SILen.Height = rsize.Height
        '    End If
        'Next
        'Return SILen

    End Function


    Private Function MeasureCharacter(ByVal g As Graphics, ByVal font As Font, ByVal c As Char) As SizeF
        Return g.MeasureString(c.ToString(), font)

    End Function

    Private Sub DeReportaDb(ByVal rpt As Report)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sql As String
        Dim i As Integer = 0
        Dim scn As Section
        Dim nombre_seccion As String
        Dim rc As New RectangleConverter
        Dim rec_str As String
        Dim aFont() As String
        Dim StrFont As String


        sql = "SELECT * from zzzinformatos_p where documento = '" & rpt.documento & "' AND formato='" & rpt.formato & "'"

        If Rs.Open(sql, ObjetoGlobal.cn) Then
            While Rs.RecordCount > 0
                Rs.Delete()
                Rs.MoveFirst()
            End While
        End If
        Rs.Close()
        sql = "SELECT * from zzzinformatos_p where 1=0"
        If Rs.Open(sql, ObjetoGlobal.cn) Then
            Try
                For i = 0 To rpt.Sections.Count - 1
                    scn = rpt.Sections(i)

                    Rs.AddNew()
                    Rs!documento = rpt.documento
                    Rs!formato = rpt.formato
                    Rs!zona = scn.nm
                    nombre_seccion = scn.nm
                    Rs!tipo = "banda"
                    Rs!indice = 9
                    Rs!num_dato = ++i
                    Rs!valor = String2Base64(scn.headerLabel)
                    Rs!tope = 0
                    Rs!alto = scn.h
                Next
                For j As Integer = 0 To scn.AR.Count - 1
                    Dim itm As Item = scn.AR(j)
                    Rs!documento = rpt.documento
                    Rs!formato = rpt.formato
                    Rs!zona = itm.nm
                    nombre_seccion = nombre_seccion
                    Rs!tipo = "banda"
                    Rs!indice = 9
                    Rs!num_dato = ++i
                    Rs!valor = String2Base64(scn.headerLabel)
                    rec_str = rc.ConvertToString(itm.Rec).ToString()
                    Dim aRec() As String = Split(rec_str, ";")
                    Rs!izquierda = CInt(aRec(0))
                    Rs!tope = CInt(aRec(1))
                    Rs!ancho = CInt(aRec(2))
                    Rs!alto = CInt(aRec(3))
                    StrFont = fontToString(itm.fnt)
                    aFont = Split(StrFont, ";")
                    Rs!fuente = aFont(0)
                    Rs!tamanof = Trim(Replace(aFont(1), "pt", ""))
                    Rs!color = itm.fcolor.ToArgb.ToString
                    Rs!fondo = itm.BackColor.ToArgb.ToString
                    Rs!angulo = itm.ang
                    Rs!tipo_cb = itm.Drawbarcode
                    If InStr(StrFont, "style=Bold") <> 0 Then
                        Rs!negrita = 1
                    End If
                    If InStr(StrFont, "Underline") <> 0 Then
                        Rs!subrayado = 1
                    End If
                    If InStr(StrFont, "Italic") <> 0 Then
                        Rs!cursiva = 1
                    End If

                    If CInt(itm.txtalign) = 1 Or CInt(itm.txtalign) = 16 Or CInt(itm.txtalign) = 256 Then
                        Rs!justificado = "I"
                    ElseIf CInt(itm.txtalign) = 2 Or CInt(itm.txtalign) = 32 Or CInt(itm.txtalign) = 512 Then
                        Rs!justificado = "C"
                    Else
                        Rs!justificado = "D"
                    End If
                    If CInt(itm.txtalign) = 1 Or CInt(itm.txtalign) = 2 Or CInt(itm.txtalign) = 4 Then
                        Rs!justificadov = "T"
                    ElseIf CInt(itm.txtalign) = 16 Or CInt(itm.txtalign) = 32 Or CInt(itm.txtalign) = 64 Then
                        Rs!justificadov = "C"
                    Else
                        Rs!justificadov = "B"
                    End If
                    Rs!tag = String2Base64(itm.fmt)
                    Rs!estilolinea = itm.estilo_linea
                    Rs!grosorlinea = itm.bw
                    'Rs!acumulado =

                    'Rs!acotarh =
                    'Rs!acotarv =
                    'Rs!numero_anterior =
                    'Rs!valor_anterior =
                    'Rs!tipo_imagen =
                    'Rs!codigo_imagen =
                Next


            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error procesando el informe")
            End Try
            '      Return Str()



        End If

    End Sub

    'Public Sub ROTACION()

    '    Using font As System.Drawing.Font = New Font("Tahoma", 8, FontStyle.Regular)
    '        Using brush As System.Drawing.SolidBrush = New SolidBrush(Color.Gray)
    '            Using format As System.Drawing.StringFormat = New StringFormat(StringFormatFlags.DirectionVertical)
    '                Dim location As System.Drawing.Point = New Point(ClientSize.Width \ 2, ClientSize.Height \ 2)
    '                Dim transform As System.Drawing.Drawing2D.Matrix = e.Graphics.Transform
    '                transform.RotateAt(180, location)
    '                e.Graphics.Transform = transform
    '                e.Graphics.DrawString("This is a test vertical string", font, brush, location, format)
    '            End Using
    '        End Using
    '    End Using
    'End Sub

    'Public Sub ROTACION(ByVal m_dAngle As Integer)
    '    Dim dAngle As Double = m_dAngle * Math.PI / 180

    '    dc.RotateTransform(-m_dAngle)

    '    Dim dXTrans As Double = ((m_dX - dXmin) / dScale)
    '    Dim dYTrans As Double = (-(m_dY - dYmax) / dScale)

    '    pt.X = (dXTrans * Math.Cos(dAngle) - dYTrans * Math.Sin(dAngle))
    '    pt.Y = ((dXTrans * Math.Sin(dAngle) + dYTrans * Math.Cos(dAngle))
    '    Dim sb As SolidBrush = New SolidBrush(m_Color), pt.X, pt.Y)

    '    dc.DrawString(m_sText, thisFont, sb, pt.X, pt.Y)
    '    dc.ResetTransform()

    'End Sub

    'Private Sub Label2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Label2.Paint
    '    '  Draw text vertically, from bottom to top
    '    '   The text to be drawn on the label
    '    Dim strText As String = "Bottom to Top"
    '    '  The Graphics object for this label
    '    Dim g2 As Graphics = e.Graphics
    '    '  The font, size and style to draw text 
    '    Dim fnt As Font = New Font("Arial", 12, FontStyle.Bold)
    '    '   Move origin point of text down to bottom of label
    '    g2.TranslateTransform(13, Label2.Height - 25)
    '    '  Rotate the text the required number of degrees
    '    g2.RotateTransform(270)
    '    '  Optional TextRenderingHint
    '    g2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit
    '    '  Finally we draw the string
    '    g2.DrawString(strText, fnt, Brushes.White, 0, 0)
    'End Sub
    Public Function Truncar(ByVal Numero As Double, Optional ByVal Decimales As Byte = 0) As Double
        Dim lngPotencia As Long
        lngPotencia = 10 ^ Decimales
        Numero = Int(Numero * lngPotencia)
        Truncar = Numero / lngPotencia
    End Function

    Public Function Intersect(ByVal A As Point, ByVal B As Point, ByVal C As Point, ByVal D As Point) As Boolean
        If (CCW(A, C, D) = CCW(B, C, D)) Then
            Return False
        End If
        If (CCW(A, B, C) = CCW(A, B, D)) Then
            Return False
        End If
        Return True
    End Function

    Public Function LineIntersect(ByVal CurPoint1 As Point, ByVal R As Rectangle) As Boolean
        Dim num2 As Double = (CDbl(R.Height) / CDbl(R.Width))
        Dim num As Double = (R.Y - (num2 * R.X))
        Dim num3 As Double = ((num2 * CurPoint1.X) + num)
        Return ((((Math.Abs(CDbl((CurPoint1.Y - num3))) < 5) And (CurPoint1.X > Math.Min(R.X, (R.X + R.Width)))) And (CurPoint1.X < Math.Max(R.X, (R.X + R.Width)))) OrElse (((R.X = R.Right) AndAlso ((CurPoint1.Y > Math.Min(R.Y, (R.Y + R.Height))) And (CurPoint1.Y < Math.Max(R.Y, (R.Y + R.Height))))) AndAlso (Math.Abs(CInt((CurPoint1.X - R.X))) < 5)))
    End Function

    Public Function CCW(ByVal A As Point, ByVal B As Point, ByVal C As Point) As Boolean
        Return (((C.Y - A.Y) * (B.X - A.X)) > ((B.Y - A.Y) * (C.X - A.X)))
    End Function

    'Public Function CargarInforme(ByVal aRet() As String) As Report
    '    Dim TRP As New Report
    '    Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    '    Dim Sql As String
    '    Dim cZona As String
    '    Dim cValor As String
    '    Dim t As Item
    '    Dim secciones() As String '= {"cabecera0", "cabecera1", "cuerpo0", "cuerpo1", "pie0", "pie1"}
    '    Dim i As Integer
    '    Dim alinear As String
    '    Dim secn As Section
    '    Dim haySeccion As Boolean = False
    '    Dim numdato As Integer = 0
    '    Dim numerolinea As Integer
    '    Dim numeroimagen As Integer
    '    Dim nEstilo As Integer
    '    Dim nFontSize As Double


    '    Try

    '        Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and tipo = 'banda' ORDER BY zona, indice"
    '        If Rs.Open(Sql, ObjetoGlobal.cn) Then
    '            ReDim secciones(Rs.RecordCount)
    '            i = 0
    '            While Not Rs.EOF
    '                secciones(i) = Rs!zona & Rs!indice
    '                i = i + 1
    '                Rs.MoveNext()
    '            End While
    '        End If

    '        For i = 0 To UBound(secciones) - 1

    '            haySeccion = False
    '            cZona = secciones(i)

    '            Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo = 'banda'  ORDER BY valor"
    '            If Rs.Open(Sql, ObjetoGlobal.cn) Then
    '                If Not Rs.EOF Then

    '                    secn = New Section

    '                    If Strings.Left(cZona, cZona.Length - 1) = "cuerpo" Then
    '                        secn = New SectionDetail
    '                    Else
    '                        secn = New Section
    '                    End If
    '                    secn.nm = Strings.Left(cZona, cZona.Length - 1)
    '                    secn.headerLabel = Strings.Left(cZona, cZona.Length - 1)
    '                    secn.h = convertirPixelesPuntos(Rs!alto)
    '                    secn.GroupID = i
    '                    haySeccion = True
    '                    TRP.Sections.Add(secn)
    '                End If
    '            End If

    '            Rs.Close()
    '            If haySeccion Then
    '                Sql = "select * FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = '" & aRet(1) & "' and zona = '" & Strings.Left(cZona, cZona.Length - 1) & "' AND indice =" & Strings.Right(cZona, 1) & " AND tipo <> 'banda'  ORDER BY valor"
    '                If Rs.Open(Sql, ObjetoGlobal.cn) Then
    '                    While Not Rs.EOF

    '                        numdato = numdato + 1

    '                        Dim rec As New Rectangle(convertirPixelesPuntos(Rs!izquierda), convertirPixelesPuntos(Rs!tope), convertirPixelesPuntos(Rs!ancho), convertirPixelesPuntos(Rs!alto))
    '                        Select Case Trim(Rs!tipo)
    '                            Case "etiqueta"
    '                                t = New LabelItem(rec)
    '                                t.txt = "" & Rs!valor
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                t.nm = "" & Rs!valor
    '                                t.Ancho_del_borde = 0.025

    '                            Case "rectangulo"
    '                                t = New shapeItem(rec)
    '                                t.estilo_linea = IIf(IsDBNull(Rs!estilo), 0, Rs!estilo)
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.FromArgb(255, 255, 255), Color.FromArgb(Rs!backcolor))
    '                                t.Ancho_del_borde = 0.025 * Rs!grosorlinea
    '                                t.nm = "Rectangulo"

    '                            Case "linea"
    '                                rec = New Rectangle(convertirPixelesPuntos(Rs!tope), convertirPixelesPuntos(Rs!alto), convertirPixelesPuntos(Rs!izquierda - Rs!tope), convertirPixelesPuntos(Rs!ancho - Rs!alto))

    '                                t = New shapeItem(rec)

    '                                If Rs!izquierda - Rs!tope = 0 Then
    '                                    t.tipo = 3
    '                                    't.Alto = 1
    '                                ElseIf Rs!ancho - Rs!alto = 0 Then
    '                                    t.tipo = 2
    '                                    't.Ancho = 1
    '                                ElseIf (Rs!tope - Rs!izquierda) < (Rs!ancho - Rs!alto) Then
    '                                    t.tipo = 4
    '                                    't.Ancho = 1
    '                                Else
    '                                    t.tipo = 4
    '                                    't.Alto = 1
    '                                End If

    '                                t.BorderWidth = 1
    '                                t.estilo_linea = IIf(IsDBNull(Rs!estilolinea), 0, Rs!estilolinea - 1)
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                t.Ancho_del_borde = 0.025 * Rs!grosorlinea

    '                                numerolinea = numerolinea + 1
    '                                t.nm = "Línea" & numerolinea
    '                                t.txt = ""

    '                            Case "elipse"
    '                                t = New shapeItem(rec)
    '                                t.tipo = 1
    '                                t.estilo_linea = IIf(IsDBNull(Rs!estilo), 0, Rs!estilo)
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                t.Ancho_del_borde = 0.025 * Rs!grosorlinea
    '                                t.nm = "Elipse"
    '                                t.txt = ""

    '                            Case "dibujo"
    '                                numeroimagen = numeroimagen + 1
    '                                t = New ImageItem(rec)
    '                                t.nm = If("" & Rs!valor = "", "Imagen" & numeroimagen, Rs!valor)
    '                                t.Ancho_del_borde = 0.025
    '                                t.txt = ""

    '                            Case "campo"

    '                                If InStr("" & Rs!valor, "cb39_") Then
    '                                    t = New BarcodeFieldItem(rec)
    '                                    t.txt = "" & Rs!valor
    '                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                    t.nm = "" & Rs!valor
    '                                    t.Ancho_del_borde = 0.025
    '                                    t.Drawbarcode = Item.SBarcode.Barcode39
    '                                Else
    '                                    t = New DataLabel(rec)
    '                                    t.txt = "" '& Rs!valor
    '                                    t.fmt = If("" & Rs!formatosal = "", Rs!tag, "" & Rs!formatosal)
    '                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                    t.nm = "" & Rs!valor
    '                                    t.Ancho_del_borde = 0.025
    '                                End If

    '                            Case "dimage"
    '                                t = New DataImage(rec)
    '                                t.txt = "" '& Rs!valor
    '                                t.nm = "" & Rs!valor
    '                                t.Ancho_del_borde = 0.025

    '                            Case "barcode"
    '                                t = New BarcodeItem(rec)
    '                                t.txt = "" & Rs!valor
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                t.nm = "" & Rs!valor
    '                                t.Ancho_del_borde = 0.025

    '                            Case "dbarcode"
    '                                t = New BarcodeFieldItem(rec)
    '                                t.txt = "" & Rs!valor
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                t.nm = "" & Rs!valor
    '                                t.Ancho_del_borde = 0.025

    '                            Case Else
    '                                t = New LabelItem(rec)
    '                                t.txt = "" & Rs!valor
    '                                t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                                t.nm = "" & Rs!valor
    '                                t.Ancho_del_borde = 0.025
    '                        End Select
    '                        ' "name"

    '                        't.nm = "" & Rs!valor
    '                        ' "text"

    '                        't.txt = "" & Rs!valor
    '                        ' -1;Bordercolor:-16777216;BorderRadius:0;BorderWidth:0
    '                        ' "backcolor"
    '                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
    '                        ' "bordercolor"
    '                        t.linecolor = If(IsDBNull(Rs!color_borde), Color.FromArgb(0, 0, 0), Color.FromArgb(Rs!color_borde))
    '                        ' "borderradius"
    '                        t.BorderRad = If(IsDBNull(Rs!radio_borde), 0, convertirPixelesPuntos(Rs!radio_borde))
    '                        ' "borderwidth"
    '                        't.bw = If(IsDBNull(Rs!ancho_borde), 0.25, convertirPixelesPuntos(Rs!ancho_borde))
    '                        ' "borderside"
    '                        t.RecSides = If(IsDBNull(Rs!ancho_borde), CType(15, AnchorStyles), CType(convertirPixelesPuntos(Rs!borderside), AnchorStyles))
    '                        ' "font"
    '                        't.fnt = stringToFont(If(IsDBNull(Rs!fuente), "Arial; 8pt", Rs!fuente & "; " & If(IsDBNull(Rs!tamanof), 8, Rs!tamanof) & "pt"))
    '                        t.fnt = stringToFont(If(IsDBNull(Rs!fuente), "Arial", Rs!fuente))
    '                        nEstilo = 0
    '                        If Not IsDBNull(Rs!fuente) Then
    '                            nFontSize = If(IsDBNull(Rs!tamanof), 8.25, Convert.ToDecimal(Replace(Rs!tamanof, ".", ",")))
    '                            If "" & Rs!negrita <> "" Then
    '                                If Rs!negrita = -1 Then
    '                                    nEstilo = 1
    '                                End If
    '                            End If
    '                            If "" & Rs!cursiva <> "" Then
    '                                If Rs!cursiva = -1 Then
    '                                    nEstilo = nEstilo + 2
    '                                End If
    '                            End If
    '                            If "" & Rs!subrayado <> "" Then
    '                                If Rs!subrayado = -1 Then
    '                                    nEstilo = nEstilo + 4
    '                                End If
    '                            End If
    '                            'If nFontSize <> 0 Then
    '                            t.fnt = CreateFont(t.fnt.FontFamily.Name, Convert.ToSingle(nFontSize), nEstilo)
    '                            'End If
    '                        End If

    '                        ' "fontcolor"
    '                        t.fcolor = If(IsDBNull(Rs!color), Color.FromArgb(0, 0, 0), Color.FromArgb(Rs!color))

    '                        alinear = Trim(If(IsDBNull(Rs!justificado), "I", Rs!justificado))

    '                        ' "textalign"
    '                        Select Case alinear
    '                            Case "I"
    '                                t.txtalign = ContentAlignment.MiddleLeft = 16
    '                            Case "D"
    '                                t.txtalign = ContentAlignment.MiddleRight = 64
    '                            Case "C"
    '                                t.txtalign = ContentAlignment.MiddleCenter = 32
    '                            Case "1"
    '                                t.txtalign = ContentAlignment.TopLeft = 1
    '                            Case "2"
    '                                t.txtalign = ContentAlignment.TopCenter = 2
    '                            Case "3"
    '                                t.txtalign = ContentAlignment.TopRight = 4
    '                            Case "4"
    '                                t.txtalign = ContentAlignment.MiddleLeft = 16
    '                            Case "5"
    '                                t.txtalign = ContentAlignment.MiddleCenter = 32
    '                            Case "6"
    '                                t.txtalign = ContentAlignment.MiddleRight = 64
    '                            Case "7"
    '                                t.txtalign = ContentAlignment.BottomLeft = 256
    '                            Case "8"
    '                                t.txtalign = ContentAlignment.BottomCenter = 512
    '                            Case "9"
    '                                t.txtalign = ContentAlignment.BottomRight = 1024
    '                            Case Else
    '                                t.txtalign = ContentAlignment.MiddleLeft = 16
    '                        End Select
    '                        ' "datafield"
    '                        t.Dfield = "" & Rs!valor
    '                        ' "dataindex"
    '                        t.DIndex = Rs!indice
    '                        ' "image"
    '                        If Trim("" & Rs!codigo_imagen) <> "" Then
    '                            t.img = ObtenImagen(Rs!codigo_imagen)
    '                            't.codimg = "" & Rs!codigo_imagen
    '                        Else
    '                            t.img = Nothing
    '                            t.codimg = ""
    '                        End If
    '                        't.Ancho_del_borde = 0.025
    '                        ' "padding"
    '                        ' Dim pc As New PaddingConverter
    '                        't.pd = pc.ConvertFromString(Data(1))
    '                        ' "autosize"
    '                        '                        t.fitInBox = Rs!autosize

    '                        ' "format"
    '                        't.fmt = "" & Rs!formatosal

    '                        Select Case "" & Rs!cod_barras
    '                            Case "39"
    '                                t.Drawbarcode = Item.SBarcode.Barcode39
    '                            Case "QR"
    '                                t.Drawbarcode = Item.SBarcode.BarcodeQR
    '                            Case "128"
    '                                t.Drawbarcode = Item.SBarcode.Barcode128
    '                            Case "EAN13"
    '                                t.Drawbarcode = Item.SBarcode.BarcodeEAN13
    '                        End Select
    '                        'If Trim(Rs!tipo) <> "dibujo" Then
    '                        t.NumeroItem = numdato
    '                        MaxItems = numdato
    '                        secn.AR.Add(t)
    '                        'End If

    '                        ' "digito_control"
    '                        't.dc = "" & Rs!cb_dc

    '                        ' "ver_texto"
    '                        't.mt = "" & Rs!cb_text
    '                        Rs.MoveNext()
    '                    End While
    '                End If
    '                Rs.Close()
    '            End If
    '        Next

    '        Return TRP

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Function

    Private Function SacarVariable(ByVal aRet() As String) As libcomunes.ControlVariables
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim cZona As String
        Dim cValor As String
        Dim dtVariables As libcomunes.ControlVariables = New libcomunes.ControlVariables

        'dtVariables.Reset()
        'dtVariables.Columns.Add(New DataColumn("valor", GetType(String)))
        'dtVariables.Columns.Add(New DataColumn("seccion", GetType(String)))

        Sql = "SELECT DISTINCT valor FROM zzzinformatos_p WHERE documento = '" & aRet(0) & "' and FORMATO = 'BASICO' and tipo = 'campo' AND left(valor, 1) = '@' ORDER BY 1"

        If Rs.Open(Sql, ObjetoGlobal.cn) Then
            While Not Rs.EOF
                dtVariables.add(Rs!valor.trim, "0")
                Rs.MoveNext()
            End While
        End If
        Rs.Close()
        Return dtVariables


    End Function

    Public Function CargarInforme(ByVal aRet() As String) As Report
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

        secciones = ObtenerSecciones(aRet)

        Try

            SacarScripts(aRet(0), aRet(1))

            TRP.dtVariables = SacarVariable(aRet)

            'For i = 0 To 5
            For i = 0 To UBound(secciones)

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
                                    t.NoRefrescar = True
                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                Case "rectangulo"
                                    t = New shapeItem(rec)
                                    t.NoRefrescar = True
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.FromArgb(255, 255, 255), Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea
                                    t.nm = "Rectangulo"

                                Case "linea"
                                    rec = New Rectangle(ConvertirPixelesPuntos(Rs!tope), ConvertirPixelesPuntos(Rs!alto), ConvertirPixelesPuntos(Rs!izquierda - Rs!tope), ConvertirPixelesPuntos(Rs!ancho - Rs!alto))

                                    t = New shapeItem(rec)
                                    t.NoRefrescar = True

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
                                    t.NoRefrescar = True

                                    t.tipo = 1
                                    t.estilo_linea = DevuelveEstilo(If(IsDBNull(Rs!estilo), "", Rs!estilo))
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.Ancho_del_borde = 0.025 * Rs!grosorlinea
                                    t.nm = "Elipse"
                                    t.txt = ""

                                Case "dibujo"
                                    numeroimagen = numeroimagen + 1
                                    t = New ImageItem(rec)
                                    t.NoRefrescar = True

                                    t.nm = If("" & Rs!valor = "", "Imagen" & numeroimagen, Rs!valor)
                                    t.Ancho_del_borde = 0.025
                                    t.txt = ""

                                Case "campo"

                                    If InStr("" & Rs!valor, "cb39_") Then
                                        t = New BarcodeFieldItem(rec)
                                        t.NoRefrescar = True

                                        t.txt = "" & Rs!valor
                                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                        t.nm = "" & Rs!valor
                                        t.Ancho_del_borde = 0.025
                                        t.Drawbarcode = Item.SBarcode.Barcode39
                                    Else
                                        t = New DataLabel(rec)
                                        t.NoRefrescar = True

                                        t.txt = "" '& Rs!valor
                                        t.fmt = If(IsDBNull(Rs!formatosal), "" & Rs!tag, Trim(Rs!formatosal))
                                        t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                        t.nm = "" & Rs!valor
                                        t.Ancho_del_borde = 0.025
                                    End If

                                Case "dimage"
                                    t = New DataImage(rec)
                                    t.NoRefrescar = True

                                    t.txt = "" '& Rs!valor
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025

                                Case "barcode"
                                    t = New BarcodeItem(rec)
                                    t.NoRefrescar = True

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
                                    t.NoRefrescar = True

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
                                    t.NoRefrescar = True

                                    t.txt = "" & Rs!valor
                                    t.bcolor = If(IsDBNull(Rs!backcolor), Color.Transparent, Color.FromArgb(Rs!backcolor))
                                    t.nm = "" & Rs!valor
                                    t.Ancho_del_borde = 0.025
                            End Select
                            ' "name"

                            't.nm = "" & Rs!valor
                            ' "text"
                            'If Rs!autosize = "T" Then
                            '    t.TruncarTexto = True
                            'End If
                            'If IsDBNull(Rs!autosize) Then
                            '    t.fitInBox = False
                            'Else
                            '    t.fitInBox = IIf(Rs!autosize = "S", True, False)
                            'End If

                            t.TruncarTexto = False
                            If IsDBNull(Rs!autosize) Then
                                t.fitInBox = True
                            ElseIf Rs!autosize = "T" Then
                                t.TruncarTexto = True
                            Else
                                t.fitInBox = IIf(Rs!autosize = "N", False, True)
                            End If


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

    Private Sub SacarVariables(aRet() As String)
        Throw New NotImplementedException()
    End Sub

    Public Function ReportFromDb(ByVal design As String) As Report
        Dim TRP As New Report
        Return TRP
    End Function

    'Private Function SacaVariables(design)

    'End Function

    'Private Function SacaCampos(design)

    'End Function

    Private Function ConvertirPixelesPuntos(ByVal px As Int32) As Double
        Return IIf(conversion, (px * 0.0666666667), px)
    End Function

    Public Function PxaPul(ByVal px As Int32) As Double
        Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        Dim dpi As Double = (intX ^ 2 + intY ^ 2) ^ 0.5

        Return (px / dpi)

    End Function

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


    Public Function CreateFont(ByVal fontName As String, ByVal fontSize As Single, ByVal estilo As FontStyle) As Drawing.Font
        Dim newFont As Drawing.Font = New Drawing.Font(fontName, fontSize, estilo)
        Return newFont

    End Function


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
    Public Function ObtenerValorVar(key)

    End Function

    Private Function SacarScripts(doc, fmto)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        Try

            ScriptsEventos = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}

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

End Module
