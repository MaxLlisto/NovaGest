'
' Created by SharpDevelop.
' User: Louis
' Date: 1/3/2017
' Time: 12:28 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.ComponentModel

Public Class PrintDesign

#Region "Print Preview"
    'Public Class PrintPrev
    Implements IDisposable

    Public ppIcon As Icon
    Dim printDocument1 As New PrintDocument
    Dim printDialog1 As New CoolPrintPreview_VB.CoolPrintPreviewDialog  ' PrintPreviewDialog
    'Dim printDialog1 As New PrintDialog
    Dim _Rpt As Report

    Dim page As Integer = 1
    Dim pages As Integer = 1
    Dim cdetail As Integer = 0
    Dim paginating As Boolean = False
    Dim Codigo_Informe As Long
    'Dim loScript As Westwind.wwScripting.wwScripting = New Westwind.wwScripting.wwScripting("VB")
    Public GrupoDetalle As Integer = 2
    Public Multidocumento As Boolean = False
    Public aVariables() 'As Variant
    Public oCv As libcomunes.ControlVariables = New libcomunes.ControlVariables

    Sub New(ByVal FileName As String)
        '_Rpt = ReportFromFile(FileName)
        'printDialog1 = New CoolPrintPreviewDialog()

    End Sub

    Sub New(ByVal Rpt As Report)
        _Rpt = Rpt
        oCv = Rpt.dtVariables
        'printDialog1 = New CoolPrintPreviewDialog()
    End Sub

    Public Sub PrintReport(Optional ByVal Preview As Boolean = False)

        'For i As Integer = 0 To RP.Sections.Count - 1
        '    Dim secn As Section
        '    secn = RP.Sections(i)
        '    GetData(secn.dt, secn._sql, secn._Constr, secn._Provider)
        'Next
        'PreparePrintDocument()
        'printDialog1.UseEXDialog = True
        printDocument1.PrinterSettings = printset
        'PreparePrintDocument()
        RenderPage()
        AddHandler printDocument1.PrintPage, AddressOf PrintPage
        'printDocument1.DefaultPageSettings.Landscape = true
        If Preview Then
            printDialog1.Document = printDocument1
            If ppIcon Is Nothing Then
                printDialog1.ShowIcon = False
            Else
                printDialog1.ShowIcon = True
                printDialog1.Icon = ppIcon
            End If
            printDialog1.WindowState = Windows.Forms.FormWindowState.Maximized
            printDialog1.ShowInTaskbar = True
            printDialog1.ShowDialog()
        Else
            printDocument1.Print()
        End If

    End Sub

    Public Sub PrintReportDocument(ByVal codInf As Long, ByVal Pag As Long, Optional ByVal Preview As Boolean = False)

        'loScript.AddAssembly("system.windows.forms.dll", "System.Windows.Forms")
        'loScript.AddAssembly("cnRecordset.dll")
        'loScript.AddAssembly("ObjetoGlobal.dll")
        'loScript.og = ObjetoGlobal
        'loScript.AddAssembly("system.web.dll","System.Web");
        'loScript.AddNamespace("System.Net");


        Codigo_Informe = codInf
        printDocument1.PrinterSettings = printset
        If Pag > 0 Then
            printDocument1.PrinterSettings.Copies = Pag
        End If

        RenderPage()
        paginating = False
        AddHandler printDocument1.PrintPage, New PrintPageEventHandler(AddressOf Me.PrinterPage)

        'printDocument1.DefaultPageSettings.Landscape = true
        If Preview Then
            printDialog1.Document = printDocument1
            If ppIcon Is Nothing Then
                printDialog1.ShowIcon = False
            Else
                printDialog1.ShowIcon = True
                printDialog1.Icon = ppIcon
            End If
            printDialog1.WindowState = Windows.Forms.FormWindowState.Maximized
            printDialog1.ShowInTaskbar = True
            printDialog1.ShowDialog()
        Else
            printDocument1.Print()
        End If

    End Sub
    Private Sub PreparePrintDocument()
        ' Make the PrintDocument object.
        pages = 1
        Dim ps As Printing.PageSettings = printset.DefaultPageSettings
        Dim pagebounds As Rectangle = ps.Bounds

        Dim g As Graphics
        g = Graphics.FromImage(New Bitmap(pagebounds.Width, pagebounds.Height))
        Dim ppa As New PrintPageEventArgs(g, pagebounds, ps.Bounds, ps)
        Do
            paginating = True
            PrintPage(Nothing, ppa)
            'pages += 1
            'msgbox("")
        Loop While ppa.HasMorePages
        paginating = False
    End Sub

    Private Function RenderPage() As Image
        Dim margY As Integer = 0
        margY = _Rpt.topm
        Dim secn As Section
        Dim Rec As Rectangle
        Dim h As Integer = 0 + margY
Restart:
        For i As Integer = 0 To _Rpt.Sections.Count - 1
            If i = _Rpt.Sections.Count - 1 Then
                secn = _Rpt.Sections(i - 1)
            ElseIf i = _Rpt.Sections.Count - 2 Then
                secn = _Rpt.Sections(i + 1)
            Else
                secn = _Rpt.Sections(i)
            End If
            If secn.Visible Then
                If secn.GroupID = GrupoDetalle Then 'aaa
                    For j As Integer = cdetail To secn.AR.Count - 1
                        h += secn.h
                    Next
                Else
                    h += secn.h
                End If
            End If
        Next
    End Function

    Private Sub RenderSection(ByVal g As Graphics, ByVal secn As Section)

    End Sub

    Sub PrinterPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Static rsC As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        'Dim rsD As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Static rsDD As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Static Primero As Boolean = True
        Dim i As Integer
        Dim j As Integer
        Dim g As Graphics = e.Graphics
        Dim PA As Rectangle
        Dim a As Integer = 0
        Dim b As Integer = 2
        Static UltimoBloque As Boolean = False
        Static OtroRegistro As Boolean = True
        PA = e.PageBounds
        PA = New Rectangle(PA.X, PA.Y, PA.Width, PA.Height)


        If Primero Then
            rsC.Open("Select * FROM zzz_documentosnv WHERE codigo_impresion = " & Codigo_Informe & " ORDER BY num_documento", ObjetoGlobal.cn)
            EmpiezaInforme(rsC)
            Primero = False
        End If
        'If rsC.Open("Select * FROM zzz_documentosnv WHERE codigo_impresion = " & Codigo_Informe & " ORDER BY num_documento", ObjetoGlobal.cn) Then
        If OtroRegistro Then
            OtroRegistro = False
            cdetail = 0
            While Not rsC.EOF
                EmpiezaDocumento(rsC)
                i = 0
                If GrupoDetalle <> 2 And page > 1 And Not Multidocumento Then
                    a = 1
                    b = 1
                    i = 2
                Else
                    a = 0
                    b = 2
                End If
                For j = a To b
                    rsDD.Open("SELECT * FROM zzz_documentos_detnv WHERE codigo_impresion = " & Codigo_Informe & " AND pagina_documento = " & rsC!num_documento & " AND zona =" & j & " AND indice = 0 order by num_linea, num_registro", ObjetoGlobal.cn)
                    _Rpt.Sections(i).dt = rsDD.cnDataSet.Tables(0).Copy
                    rsDD.Close()
                    i = i + 1
                    rsDD.Open("SELECT * FROM zzz_documentos_detnv WHERE codigo_impresion = " & Codigo_Informe & " AND pagina_documento = " & rsC!num_documento & " AND zona =" & j & " AND indice = 1 order by num_linea, num_registro", ObjetoGlobal.cn)
                    If Not rsDD.EOF Then
                        _Rpt.Sections(i).dt = rsDD.cnDataSet.Tables(0).Copy
                        i = i + 1
                    End If
                    rsDD.Close()
                    rsDD.Open("SELECT * FROM zzz_documentos_detnv WHERE codigo_impresion = " & Codigo_Informe & " AND pagina_documento = " & rsC!num_documento & " AND zona =" & j & " AND indice = 2 order by num_linea, num_registro", ObjetoGlobal.cn)
                    If Not rsDD.EOF Then
                        _Rpt.Sections(i).dt = rsDD.cnDataSet.Tables(0).Copy
                        i = i + 1
                    End If
                    rsDD.Close()

                Next
                rsC.MoveNext()
                UltimoBloque = False
                If rsC.EOF Then
                    UltimoBloque = True
                End If
                Exit While
            End While
        End If

        ImprimirPaginas(sender, e, g, PA, UltimoBloque)

        'cdetail = cdetail + 1
        If Not e.HasMorePages And Not rsC.EOF Then
            OtroRegistro = True
            e.HasMorePages = True
            TerminaDocumento(rsC)
        Else
            If rsC.EOF And Not e.HasMorePages Then
                e.HasMorePages = False
                PieFinal(rsC)
            Else
                e.HasMorePages = True
                PieParciales(rsC)
            End If
            Return
        End If




    End Sub


    Sub ImprimirPaginas(ByVal sender As Object, ByVal e As PrintPageEventArgs, ByRef g As Graphics, ByRef pa As Rectangle, Optional ByVal UltimoBloque As Boolean = False)

        Try
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim margX As Integer = 0
            Dim margY As Integer = 0
            Dim margB As Integer = 0
            Dim morepages As Boolean = False

            margX = _Rpt.leftm
            margY = _Rpt.topm
            margB = _Rpt.bottomm

            Dim rid As Integer = 1

            Dim h As Integer = 0 + margY
            Dim secn As Section
            Dim Rec As Rectangle
            Dim Brec As Rectangle = pa
            'PA = New Rectangle(PA.X, PA.Y, PA.Width, PA.Height)

            Select Case _Rpt.linetype
                Case 0
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Black, _Rpt.bw), Rec)
                Case 1
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 3
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - bt / 2, _Rpt.topm + Brec.Y - bt / 2, _Rpt.w + bt, Brec.Height - margB - margY + bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), Rec)

                    Rec = New Rectangle(_Rpt.leftm + Brec.X - bt / 2 - 2 * bt, _Rpt.topm + Brec.Y - bt / 2 - 2 * bt, _Rpt.w + bt + 4 * bt, Brec.Height - margB - margY + bt + 4 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), Rec)
                Case 2
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 4
                    Dim rec1 As New Rectangle(Rec.X + 3 * bt / 2, Rec.Y + 3 * bt / 2, Rec.Width - 3 * bt, Rec.Height - 3 * bt)
                    Dim rec2 As New Rectangle(Rec.X - 1 * bt, Rec.Y - 1 * bt, Rec.Width + 2 * bt, Rec.Height + 2 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec1)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), 2 * bt), rec2)
                Case 3
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 4
                    Dim rec1 As New Rectangle(Rec.X - 3 * bt / 2, Rec.Y - 3 * bt / 2, Rec.Width + 3 * bt, Rec.Height + 3 * bt)
                    Dim rec2 As New Rectangle(Rec.X + 1 * bt, Rec.Y + 1 * bt, Rec.Width - 2 * bt, Rec.Height - 2 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec1)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), 2 * bt), rec2)
                Case 4
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 5
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), Rec)
                    Dim rec1 As New Rectangle(Rec.X - 2 * bt, Rec.Y - 2 * bt, Rec.Width + 4 * bt, Rec.Height + 4 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec1)
                    Dim rec2 As New Rectangle(Rec.X + 2 * bt, Rec.Y + 2 * bt, Rec.Width - 4 * bt, Rec.Height - 4 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec2)
            End Select

Restart:
            For i As Integer = 0 To _Rpt.Sections.Count - 1
                secn = _Rpt.Sections(i)

                Rec = New Rectangle(margX, h, _Rpt.w, secn.h)
                'msgbox(cdetail)
                'If i = _Rpt.Sections.Count - 1 Then
                '    MsgBox(cdetail)
                'End If

                Dim t As Item
                If secn.GroupID = GrupoDetalle Then
                    Dim lin As Long = 0
                    If secn.Visible Then
                        If cdetail > secn.dt.Rows.Count - 1 Then
                            GoTo Footer
                        End If
                        Dim alt As Boolean = False
                        Dim ContadorLin As Integer = 0

                        For k As Integer = cdetail To secn.dt.Rows.Count - 1

                            ContadorLin = ContadorLin + 1

                            If lin = 0 Then
                                lin = secn.dt.Rows(k)("num_linea")
                            ElseIf lin <> secn.dt.Rows(k)("num_linea") Then
                                lin = secn.dt.Rows(k)("num_linea")
                                h += secn.h
                            End If

                            LineasDocumento(secn.dt, lin)

                            For j As Integer = 0 To secn.AR.Count - 1
                                If Not paginating Then
                                    t = secn.AR(j)
                                    If j = 0 Then
                                        Rec = New Rectangle(margX - 0, h, _Rpt.w + 0, secn.h)
                                        g.SetClip(Rec, CombineMode.Replace)
                                        Rec = New Rectangle(margX, h, _Rpt.w, secn.h)
                                        g.Clear(Color.White)
                                        If alt Then
                                            g.FillRectangle(New SolidBrush(secn.abcolor), Rec)
                                            If Not (secn.aimg Is Nothing) Then
                                                g.DrawImage(secn.aimg, Rec)
                                            End If
                                            alt = False
                                        Else
                                            g.FillRectangle(New SolidBrush(secn.bcolor), Rec)
                                            If Not (secn.img Is Nothing) Then
                                                g.DrawImage(secn.img, Rec)
                                            End If
                                            alt = True
                                        End If

                                    End If


                                    If secn.BorderWidth > 0 Then
                                        Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                                        If CInt(secn.BorderSides) = 15 Then
                                            g.DrawRectangle(New Pen(New SolidBrush(secn.Bordercolor), secn.BorderWidth), New Rectangle(margX, h, _Rpt.w, secn.h))
                                        Else
                                            If secn.BorderSides And AnchorStyles.Top Then
                                                g.DrawLine(spn, margX, h, margX + _Rpt.w, h)
                                            End If
                                            If secn.BorderSides And AnchorStyles.Right Then
                                                g.DrawLine(spn, margX + _Rpt.w, h, margX + _Rpt.w, h + secn.h)
                                            End If

                                            If secn.BorderSides And AnchorStyles.Bottom Then
                                                g.DrawLine(spn, margX, h + secn.h, margX + _Rpt.w, h + secn.h)
                                            End If

                                            If secn.BorderSides And AnchorStyles.Left Then
                                                g.DrawLine(spn, margX, h, margX, h + secn.h)
                                            End If
                                        End If
                                    End If

                                    Dim nRec As New Rectangle(margX + t.Rec.X, t.Rec.Y + h, t.Rec.Width, t.Rec.Height)
                                    Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                                    If t.bw < 1 Then
                                        handPad = 0
                                    End If
                                    Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)
                                    Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)
                                    'msgbox(t.ItemType)
                                    Select Case LCase(t.ItemType)
                                        Case LCase("Label")
                                            'Dim Lrec As New Rectangle(margX + nrec.X + handpad + radPad + t.pd.Left, nrec.Y + handpad + radPad + t.pd.Top, nrec.Width - 2*(handpad + radPad) - t.pd.Left - t.pd.Right, nrec.Height - 2*(handpad + radPad) - t.pd.Top - t.pd.Bottom)
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)


                                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                            If t.vh Then
                                                nflag += StringFormatFlags.DirectionVertical
                                            End If
                                            If t.di Then
                                                nflag += StringFormatFlags.DirectionRightToLeft
                                            End If


                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)
                                            If t.fitInBox Then
                                                FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            Else
                                                g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            End If
                                        Case LCase("Shape")
                                            Select Case t.Drawshape
                                                Case shapeItem.SType.Rectangulo
                                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                                    getRecShape(g, t, margX, h, False)
                                                Case shapeItem.SType.Elipse
                                                    g.FillEllipse(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawEllipse(pn, nRec)
                                                    End If
                                                Case shapeItem.SType.Linea_horizontal
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, margX + nRec.X, h + nRec.Y, margX + nRec.X + nRec.Width, nRec.Y)
                                                    End If
                                                Case shapeItem.SType.Linea_vertical
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, margX + nRec.X, h + nRec.Y, margX + nRec.X, nRec.Y + nRec.Height)
                                                    End If
                                                Case shapeItem.SType.Linea_libre
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y - nRec.Height)
                                                    End If

                                            End Select
                                        Case LCase("Barcode")
                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39()
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor
                                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, t.txt), nRec)

                                            End Select

                                        Case "Fbarcode"
                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor

                                                    Dim res As String = ""
                                                    res = TextFromDT(t.Dfield.Trim, k, secn, page, pages, oCv)

                                                    If res.Trim <> "" Then
                                                        'g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)
                                                        g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, res.Trim), nRec)
                                                    End If

                                            End Select

                                        Case LCase("Image")
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            If Not (t.img Is Nothing) Then
                                                g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                            End If
                                            getRecShape(g, t, margX, h, False)

                                        Case LCase("DataLabel")
                                            'msgbox(radpad)
                                            'dim Lrec as New Rectangle(margX + nrec.X + handpad + radPad, h + nrec.Y + handpad + radPad, nrec.Width - 2*(handpad + radPad), nrec.Height - 2*(handpad + radPad))
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                                            'g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            g.FillPath(New SolidBrush(Color.Transparent), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)

                                            Dim res As String = ""

                                            res = TextFromDT(t.Dfield.Trim, k, secn, page, pages, oCv)
                                            If res.Trim <> "" Then
                                                Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                                If t.vh Then
                                                    nflag += StringFormatFlags.DirectionVertical
                                                End If
                                                If t.di Then
                                                    nflag += StringFormatFlags.DirectionRightToLeft
                                                End If

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
                                                        'res = TextFromDT(t.Dfield, k, secn, page, pages)
                                                    End Try
                                                End If
                                                'If t.fitInBox Then
                                                '    FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                'Else
                                                '    'g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                '    g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Rec.X, Rec.Y, CA2SF(t.txtalign, nflag))

                                                '    'DrawString(s As String, font As Font, brush As Brush, x As Single, y As Single, format As StringFormat)

                                                'End If
                                                If t.TruncarTexto Then
                                                    'res = recortarcadena(g, t.fnt, res, Lrec.Width)
                                                    Dim ls As SizeF
                                                    ls = Module1.LongitudString(g, t.fnt, res & " ----")
                                                    If Lrec.Width < ls.Width Then
                                                        res = Strings.Left(res, CInt(Len(res) * Lrec.Width / ls.Width) - 2) & ".."
                                                    End If
                                                End If
                                                If t.fitInBox Then
                                                    FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                Else
                                                    g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                End If
                                            End If

                                        Case LCase("DataImage")
                                            'g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            Dim Imagen As Image
                                            Imagen = ImageFromDT(t.Dfield, k, secn.dt)

                                            If Not IsDBNull(Imagen) Then
                                                '    g.DrawImage(RoundImageConers(img, nRec, t.BorderRad), nRec)
                                                'End If
                                                'getRecShape(g, t, margX, h, False)
                                                'End If

                                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                                If Not (t.img Is Nothing) Then
                                                    g.DrawImage(RoundImageConers(Imagen, nRec, t.BorderRad), nRec)
                                                End If
                                                getRecShape(g, t, margX, h, False)
                                            End If
                                    End Select
                                End If
                            Next

                            'h += secn.h
                            Dim xx As Integer = _Rpt.Sections.Count - 2
                            Dim newh As Integer = 0
                            If _Rpt.Sections(xx).Visible Then
                                newh += CType(_Rpt.Sections(xx), Section).h
                            End If

                            'cdetail = k + 1

                            While lin = secn.dt.Rows(k)("num_linea")
                                'cdetail = k + 1
                                k = k + 1
                                If k >= secn.dt.Rows.Count - 1 Then
                                    h += secn.h
                                    Exit While
                                End If
                            End While

                            If pa.Height - margB - h - secn.h - newh < 0 Then
                                morepages = True
                                'exit for
                                cdetail = k
                                GoTo Footer
                            End If
                        Next
                        morepages = False
                    End If
Footer:
                Else
                    'msgbox(i)
                    If i >= _Rpt.Sections.Count - 2 Then
                        h = pa.Height
                        h += -margB
                    Else
                        If Not morepages = True Then
                            If CType(_Rpt.Sections(_Rpt.Sections.Count - 2), Section).Visible Then
                                If h + secn.h > pa.Height - margB - CType(_Rpt.Sections(_Rpt.Sections.Count - 2), Section).h Then
                                    morepages = True
                                End If
                            End If
                        End If
                    End If

                    If secn.GroupID = 5 Then
                        'MsgBox("")
                    End If

                    If secn.Visible Then
                        Dim show As Boolean = True
                        If page > 1 Then
                            If secn.GroupID = 0 Then
                                show = False
                            End If
                            If secn.GroupID = 1 Then
                                SiguientesCabeceras(secn.dt)
                            End If
                        Else
                            If secn.GroupID = 1 Then
                                show = False
                                PrimeraCabecera(secn.dt)
                            End If
                        End If
                        'If morepages Then
                        '    If secn.GroupID = 5 Then
                        '        show = False
                        '    End If
                        'Else
                        '    If secn.GroupID = 4 Then
                        '        show = False
                        '    End If
                        'End If

                        If morepages Or Not UltimoBloque Then
                            If secn.GroupID = _Rpt.Sections.Count - 1 Then
                                show = False
                                PieParciales(secn.dt)
                            End If
                        Else
                            If secn.GroupID = _Rpt.Sections.Count - 2 Then
                                show = False
                            End If
                            PieFinal(secn.dt)
                        End If

                        If i = _Rpt.Sections.Count - 2 Then
                            'show = true
                        End If
                        'msgbox(h)
                        If show Then
                            If i >= _Rpt.Sections.Count - 2 Then
                                h -= secn.h
                            End If

                            If Not paginating Then
                                Rec = New Rectangle(margX - 0, h, _Rpt.w + 0, secn.h)
                                g.SetClip(Rec, CombineMode.Replace)
                                g.Clear(Color.White)
                                Rec = New Rectangle(margX, h, _Rpt.w, secn.h)
                                g.FillRectangle(New SolidBrush(secn.BackColor), Rec)
                                If Not (secn.img Is Nothing) Then
                                    g.DrawImage(secn.img, Rec)
                                End If

                                If secn.BorderWidth > 0 Then
                                    Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                                    If CInt(secn.BorderSides) = 15 Then
                                        g.DrawRectangle(New Pen(New SolidBrush(secn.Bordercolor), secn.BorderWidth), New Rectangle(margX, h, _Rpt.w, secn.h))
                                    Else
                                        If secn.BorderSides And AnchorStyles.Top Then
                                            g.DrawLine(spn, margX, h, margX + _Rpt.w, h)
                                        End If
                                        If secn.BorderSides And AnchorStyles.Right Then
                                            g.DrawLine(spn, margX + _Rpt.w, h, margX + _Rpt.w, h + secn.h)
                                        End If

                                        If secn.BorderSides And AnchorStyles.Bottom Then
                                            g.DrawLine(spn, margX, h + secn.h, margX + _Rpt.w, h + secn.h)
                                        End If

                                        If secn.BorderSides And AnchorStyles.Left Then
                                            g.DrawLine(spn, margX, h, margX, h + secn.h)
                                        End If
                                    End If
                                End If
                                For j As Integer = 0 To secn.AR.Count - 1
                                    t = secn.AR(j)
                                    Dim nRec As New Rectangle(margX + t.Rec.X, t.Rec.Y + h, t.Rec.Width, t.Rec.Height)
                                    Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                                    If t.bw < 1 Then
                                        handPad = 0
                                    End If
                                    Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)
                                    Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)
                                    Select Case t.ItemType
                                        Case "Label"
                                            'Dim Lrec As New Rectangle(nrec.X + handpad + radPad, nrec.Y + handpad + radPad, nrec.Width - 2*(handpad + radPad), nrec.Height - 2*(handpad + radPad))
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)
                                            'gCanvas.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit

                                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                            If t.vh Then
                                                nflag += StringFormatFlags.DirectionVertical
                                            End If
                                            If t.di Then
                                                nflag += StringFormatFlags.DirectionRightToLeft
                                            End If


                                            If t.fitInBox Then
                                                FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            Else
                                                g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            End If

                                        Case "Shape"
                                            Select Case t.Drawshape
                                                Case shapeItem.SType.Rectangulo
                                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                                    getRecShape(g, t, margX, h, False)
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
                                                Case shapeItem.SType.Linea_libre
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y - nRec.Height)
                                                    End If

                                            End Select
                                        Case "Image"
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                            getRecShape(g, t, margX, h, False)
                                        Case "DataLabel"
                                            'dim Lrec as New Rectangle(nrec.X + handpad + radPad, nrec.Y + handpad + radPad, nrec.Width - 2*(handpad + radPad), nrec.Height - 2*(handpad + radPad))
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)
                                            Dim res As String = ""
                                            res = TextFromDT(t.Dfield, 0, secn, page, pages, oCv)
                                            If res.Trim <> "" Then
                                                Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                                If t.vh Then
                                                    nflag += StringFormatFlags.DirectionVertical
                                                End If
                                                If t.di Then
                                                    nflag += StringFormatFlags.DirectionRightToLeft
                                                End If

                                                If Trim(t.fmt) = "" Then

                                                Else
                                                    Try
                                                        Dim fo As Object
                                                        If Not Decimal.TryParse(res, fo) Then
                                                            Dim fo2 As Object
                                                            If DateTime.TryParse(res, fo2) Then
                                                                res = Format(fo2, t.fmt)
                                                                'Else
                                                                '    res = ""
                                                            End If
                                                        Else
                                                            res = Format(fo, t.fmt)
                                                        End If
                                                    Catch ferr As Exception
                                                        'res = TextFromDT(t.Dfield, k, secn, page, pages)
                                                        'res = ferr.Message
                                                    End Try
                                                End If
                                                'If t.fitInBox Then
                                                '    FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                'Else
                                                '    g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                'End If

                                                If t.TruncarTexto Then
                                                    Dim ls As SizeF
                                                    ls = Module1.LongitudString(g, t.fnt, res & " ----")
                                                    If Lrec.Width < ls.Width Then
                                                        res = Strings.Left(res, CInt(res.Trim.Length * Lrec.Width / ls.Width) - 2) & ".."
                                                    End If
                                                End If
                                                If t.Dfield.Trim <> "" Then
                                                    If t.fitInBox Then
                                                        FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                    Else
                                                        g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                                    End If
                                                End If
                                            End If



                                        Case "DataImage"
                                            'g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            'Dim img As Image

                                            'img = ImageFromDT(t.Dfield, 0, secn.dt)
                                            'If Not (img Is Nothing) Then
                                            '    g.DrawImage(RoundImageConers(img, nRec, t.BorderRad), nRec)
                                            'End If
                                            ''g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                            'getRecShape(g, t, margX, h, False)

                                            Dim Imagen As Image
                                            Imagen = ImageFromDT(t.Dfield, 0, secn.dt)

                                            If Not IsDBNull(Imagen) Then
                                                '    g.DrawImage(RoundImageConers(img, nRec, t.BorderRad), nRec)
                                                'End If
                                                'getRecShape(g, t, margX, h, False)
                                                'End If

                                                g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                                If Not (Imagen Is Nothing) Then
                                                    g.DrawImage(RoundImageConers(Imagen, nRec, t.BorderRad), nRec)
                                                End If
                                                getRecShape(g, t, margX, h, False)
                                            End If

                                        Case "Barcode"
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))

                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39()
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor
                                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, t.txt), nRec)

                                            End Select

                                        Case "Fbarcode"
                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39

                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor

                                                    Dim res As String = ""
                                                    res = TextFromDT(t.Dfield.Trim, 0, secn, page, pages, oCv)

                                                    If res.Trim <> "" Then
                                                        'g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)
                                                        g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, res.Trim), nRec)
                                                    End If

                                                    'If t.Dfield.Trim <> "" Then
                                                    '    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                                    '    cb.ShowString = t.mt
                                                    '    cb.IncludeCheckSumDigit = t.dc
                                                    '    cb.TextFont = t.fnt
                                                    '    cb.TextColor = t.fcolor
                                                    '    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)
                                                    'End If
                                            End Select

                                    End Select
                                Next
                            End If
                            If i < _Rpt.Sections.Count - 2 Then
                                h += secn.h
                            End If
                        End If
                    End If


                    If i = _Rpt.Sections.Count - 3 Then
                        h = pa.Height
                        If CType(_Rpt.Sections(i + 1), Section).Visible Then
                            h += -CType(_Rpt.Sections(i + 1), Section).h
                        End If
                        If CType(_Rpt.Sections(i + 2), Section).Visible Then
                            h += -CType(_Rpt.Sections(i + 2), Section).h
                        End If
                        h += -margB
                    End If
                End If

            Next
            page += 1
            If morepages Then
                If _Rpt.mcol Then
                    margX += _Rpt.w + _Rpt.leftm / 2
                    If margX + _Rpt.w + _Rpt.leftm < pa.Width Then
                        h = _Rpt.topm
                        GoTo Restart
                    End If
                End If
                'page += 1
                e.HasMorePages = True
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If Multidocumento Then
            e.HasMorePages = False
            pages = page
            page = 1
            cdetail = 0
        End If

    End Sub

    Sub Printpage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Try
            Dim g As Graphics = e.Graphics
            'g.InterpolationMode = InterpolationMode.HighQualityBilinear
            'g.FillRectangle(brushes.SaddleBrown, PA)
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim margX As Integer = 0
            Dim margY As Integer = 0
            Dim margB As Integer = 0
            Dim morepages As Boolean = False

            Dim PA As Rectangle
            PA = e.PageBounds

            margX = _Rpt.leftm
            margY = _Rpt.topm
            margB = _Rpt.bottomm

            Dim rid As Integer = 1

            'scns = _Rpt.Sections.Clone

            Dim h As Integer = 0 + margY
            Dim secn As Section
            Dim Rec As Rectangle
            Dim Brec As Rectangle = PA
            PA = New Rectangle(PA.X, PA.Y, PA.Width, PA.Height)

            Select Case _Rpt.linetype
                Case 0
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Black, _Rpt.bw), Rec)
                Case 1
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 3
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - bt / 2, _Rpt.topm + Brec.Y - bt / 2, _Rpt.w + bt, Brec.Height - margB - margY + bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), Rec)

                    Rec = New Rectangle(_Rpt.leftm + Brec.X - bt / 2 - 2 * bt, _Rpt.topm + Brec.Y - bt / 2 - 2 * bt, _Rpt.w + bt + 4 * bt, Brec.Height - margB - margY + bt + 4 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), Rec)
                Case 2
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 4
                    Dim rec1 As New Rectangle(Rec.X + 3 * bt / 2, Rec.Y + 3 * bt / 2, Rec.Width - 3 * bt, Rec.Height - 3 * bt)
                    Dim rec2 As New Rectangle(Rec.X - 1 * bt, Rec.Y - 1 * bt, Rec.Width + 2 * bt, Rec.Height + 2 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec1)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), 2 * bt), rec2)
                Case 3
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 4
                    Dim rec1 As New Rectangle(Rec.X - 3 * bt / 2, Rec.Y - 3 * bt / 2, Rec.Width + 3 * bt, Rec.Height + 3 * bt)
                    Dim rec2 As New Rectangle(Rec.X + 1 * bt, Rec.Y + 1 * bt, Rec.Width - 2 * bt, Rec.Height - 2 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec1)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), 2 * bt), rec2)
                Case 4
                    Rec = New Rectangle(_Rpt.leftm + Brec.X - _Rpt.bw / 2, _Rpt.topm + Brec.Y - _Rpt.bw / 2, _Rpt.w + _Rpt.bw, Brec.Height - margB - margY + _Rpt.bw)
                    g.DrawRectangle(New Pen(Brushes.Red, _Rpt.bw), Rec)
                    Dim bt As Integer = 0
                    bt = _Rpt.bw / 5
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), Rec)
                    Dim rec1 As New Rectangle(Rec.X - 2 * bt, Rec.Y - 2 * bt, Rec.Width + 4 * bt, Rec.Height + 4 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec1)
                    Dim rec2 As New Rectangle(Rec.X + 2 * bt, Rec.Y + 2 * bt, Rec.Width - 4 * bt, Rec.Height - 4 * bt)
                    g.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(100, 0, 0, 0)), bt), rec2)
            End Select

Restart:
            For i As Integer = 0 To _Rpt.Sections.Count - 1
                If i = _Rpt.Sections.Count - 1 Then
                    secn = _Rpt.Sections(i - 1)
                ElseIf i = _Rpt.Sections.Count - 2 Then
                    secn = _Rpt.Sections(i + 1)
                Else
                    secn = _Rpt.Sections(i)
                End If

                Rec = New Rectangle(margX, h, _Rpt.w, secn.h)
                'MsgBox(cdetail)
                'g.FillRectangle(brushes.Green, rec)
                Dim t As Item
                If secn.GroupID = GrupoDetalle Then
                    If secn.Visible Then
                        If cdetail > secn.dt.Rows.Count - 1 Then
                            GoTo Footer
                        End If
                        Dim alt As Boolean = False
                        Dim contador As Integer = 0
                        For k As Integer = cdetail To secn.dt.Rows.Count - 1
                            contador = contador + 1
                            For j As Integer = 0 To secn.AR.Count - 1
                                If Not paginating Then
                                    t = secn.AR(j)
                                    If j = 0 Then
                                        Rec = New Rectangle(margX - 0, h, _Rpt.w + 0, secn.h)
                                        g.SetClip(Rec, CombineMode.Replace)
                                        Rec = New Rectangle(margX, h, _Rpt.w, secn.h)
                                        g.Clear(Color.White)
                                        If alt Then
                                            g.FillRectangle(New SolidBrush(secn.abcolor), Rec)
                                            If Not (secn.aimg Is Nothing) Then
                                                g.DrawImage(secn.aimg, Rec)
                                            End If
                                            alt = False
                                        Else
                                            g.FillRectangle(New SolidBrush(secn.bcolor), Rec)
                                            If Not (secn.img Is Nothing) Then
                                                g.DrawImage(secn.img, Rec)
                                            End If
                                            alt = True
                                        End If

                                    End If


                                    If secn.BorderWidth > 0 Then
                                        Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                                        If CInt(secn.BorderSides) = 15 Then
                                            g.DrawRectangle(New Pen(New SolidBrush(secn.Bordercolor), secn.BorderWidth), New Rectangle(margX, h, _Rpt.w, secn.h))
                                        Else
                                            If secn.BorderSides And AnchorStyles.Top Then
                                                g.DrawLine(spn, margX, h, margX + _Rpt.w, h)
                                            End If
                                            If secn.BorderSides And AnchorStyles.Right Then
                                                g.DrawLine(spn, margX + _Rpt.w, h, margX + _Rpt.w, h + secn.h)
                                            End If

                                            If secn.BorderSides And AnchorStyles.Bottom Then
                                                g.DrawLine(spn, margX, h + secn.h, margX + _Rpt.w, h + secn.h)
                                            End If

                                            If secn.BorderSides And AnchorStyles.Left Then
                                                g.DrawLine(spn, margX, h, margX, h + secn.h)
                                            End If
                                        End If
                                    End If

                                    Dim nRec As New Rectangle(margX + t.Rec.X, t.Rec.Y + h, t.Rec.Width, t.Rec.Height)
                                    Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                                    If t.bw < 1 Then
                                        handPad = 0
                                    End If
                                    Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)
                                    Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)
                                    'msgbox(t.ItemType)
                                    Select Case LCase(t.ItemType)
                                        Case LCase("Label")
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)
                                            'Dim Lrec As New Rectangle(margX + nrec.X + handpad + radPad + t.pd.Left, nrec.Y + handpad + radPad + t.pd.Top, nrec.Width - 2*(handpad + radPad) - t.pd.Left - t.pd.Right, nrec.Height - 2*(handpad + radPad) - t.pd.Top - t.pd.Bottom)
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)


                                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                            If t.vh Then
                                                nflag += StringFormatFlags.DirectionVertical
                                            End If
                                            If t.di Then
                                                nflag += StringFormatFlags.DirectionRightToLeft
                                            End If


                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)
                                            If t.fitInBox Then
                                                FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            Else
                                                g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            End If
                                        Case LCase("Shape")
                                            Select Case t.Drawshape
                                                Case shapeItem.SType.Rectangulo
                                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                                    getRecShape(g, t, margX, h, False)
                                                Case shapeItem.SType.Elipse
                                                    g.FillEllipse(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawEllipse(pn, nRec)
                                                    End If
                                                Case shapeItem.SType.Linea_horizontal
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, margX + nRec.X, h + nRec.Y, margX + nRec.X + nRec.Width, nRec.Y)
                                                    End If
                                                Case shapeItem.SType.Linea_vertical
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, margX + nRec.X, h + nRec.Y, margX + nRec.X, nRec.Y + nRec.Height)
                                                    End If
                                                Case shapeItem.SType.Linea_libre
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y - nRec.Height)
                                                    End If

                                            End Select
                                        Case LCase("Barcode")
                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39()
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor
                                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, t.txt), nRec)

                                            End Select

                                        Case "Fbarcode"
                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor
                                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)
                                            End Select

                                        Case LCase("Image")
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            If Not (t.img Is Nothing) Then
                                                g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                            End If
                                            getRecShape(g, t, margX, h, False)
                                        Case LCase("DataLabel")
                                            'msgbox(radpad)
                                            'dim Lrec as New Rectangle(margX + nrec.X + handpad + radPad, h + nrec.Y + handpad + radPad, nrec.Width - 2*(handpad + radPad), nrec.Height - 2*(handpad + radPad))
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)

                                            Dim res As String = ""
                                            res = TextFromDT(t.Dfield.Trim, k, secn, page, pages, oCv)

                                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                            If t.vh Then
                                                nflag += StringFormatFlags.DirectionVertical
                                            End If
                                            If t.di Then
                                                nflag += StringFormatFlags.DirectionRightToLeft
                                            End If

                                            If Trim(t.fmt) = "" Then

                                            Else
                                                Try
                                                    Dim fo As Object
                                                    If Not Decimal.TryParse(res, fo) Then
                                                        Dim fo2 As Object
                                                        If IsNumeric(res) Then
                                                            res = Format(fo, t.fmt)
                                                        ElseIf DateTime.TryParse(res, fo2) Then
                                                            res = Format(fo2, t.fmt)
                                                        Else
                                                            res = "Formato desconocido"
                                                        End If
                                                    Else
                                                        res = Format(fo, t.fmt)
                                                    End If
                                                Catch ferr As Exception
                                                    'res = TextFromDT(t.Dfield, k, secn, page, pages)
                                                End Try
                                            End If

                                            If t.TruncarTexto Then
                                                Dim ls As SizeF
                                                ls = Module1.LongitudString(g, t.fnt, res)
                                                If Lrec.Width < ls.Width Then
                                                    res = Strings.Left(res, CInt(res.Trim.Length * ls.Width / Lrec.Width) - 2) & ".."
                                                End If
                                            End If

                                            If t.fitInBox Then
                                                FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            Else
                                                g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            End If

                                        Case LCase("DataImage")
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            Dim img As Image
                                            img = ImageFromDT(t.Dfield, k, secn.dt)
                                            If Not (img Is Nothing) Then
                                                g.DrawImage(RoundImageConers(img, nRec, t.BorderRad), nRec)
                                            End If
                                            getRecShape(g, t, margX, h, False)
                                    End Select
                                End If
                            Next


                            h += secn.h
                            Dim xx As Integer = _Rpt.Sections.Count - 2
                            Dim newh As Integer = 0
                            If _Rpt.Sections(xx).Visible Then
                                newh += CType(_Rpt.Sections(xx), Section).h
                            End If
                            cdetail = k + 1
                            If PA.Height - margB - h - secn.h - newh < 0 Then
                                morepages = True
                                'exit for
                                GoTo Footer
                            End If
                        Next
                        morepages = False
                    End If
Footer:
                Else
                    'msgbox(i)
                    If i = _Rpt.Sections.Count - 1 Then
                        h = pa.Height
                        h += -margB
                    Else
                        If Not morepages = True Then
                            If CType(_Rpt.Sections(_Rpt.Sections.Count - 2), Section).Visible Then
                                If h + secn.h > pa.Height - margB - CType(_Rpt.Sections(_Rpt.Sections.Count - 2), Section).h Then
                                    morepages = True
                                End If
                            End If
                        End If
                    End If

                    If secn.Visible Then
                        Dim show As Boolean = True

                        If secn.GroupID = 5 Then

                        End If

                        If page > 1 Then
                            If secn.GroupID = 0 Then
                                show = False
                            End If
                        End If
                        If morepages Then
                            If secn.GroupID = 4 Then
                                show = False
                            End If
                        End If

                        If i = _Rpt.Sections.Count - 2 Then
                            'show = true
                        End If
                        'msgbox(h)
                        If show Then
                            If i > _Rpt.Sections.Count - 2 Then
                                h -= secn.h
                            End If

                            If Not paginating Then
                                Rec = New Rectangle(margX - 0, h, _Rpt.w + 0, secn.h)
                                g.SetClip(Rec, CombineMode.Replace)
                                g.Clear(Color.White)
                                Rec = New Rectangle(margX, h, _Rpt.w, secn.h)
                                g.FillRectangle(New SolidBrush(secn.BackColor), Rec)
                                If Not (secn.img Is Nothing) Then
                                    g.DrawImage(secn.img, Rec)
                                End If

                                If secn.BorderWidth > 0 Then
                                    Dim spn As New Pen(New SolidBrush(secn.linecolor), secn.BorderWidth)

                                    If CInt(secn.BorderSides) = 15 Then
                                        g.DrawRectangle(New Pen(New SolidBrush(secn.Bordercolor), secn.BorderWidth), New Rectangle(margX, h, _Rpt.w, secn.h))
                                    Else
                                        If secn.BorderSides And AnchorStyles.Top Then
                                            g.DrawLine(spn, margX, h, margX + _Rpt.w, h)
                                        End If
                                        If secn.BorderSides And AnchorStyles.Right Then
                                            g.DrawLine(spn, margX + _Rpt.w, h, margX + _Rpt.w, h + secn.h)
                                        End If

                                        If secn.BorderSides And AnchorStyles.Bottom Then
                                            g.DrawLine(spn, margX, h + secn.h, margX + _Rpt.w, h + secn.h)
                                        End If

                                        If secn.BorderSides And AnchorStyles.Left Then
                                            g.DrawLine(spn, margX, h, margX, h + secn.h)
                                        End If
                                    End If
                                End If
                                For j As Integer = 0 To secn.AR.Count - 1
                                    t = secn.AR(j)
                                    Dim nRec As New Rectangle(margX + t.Rec.X, t.Rec.Y + h, t.Rec.Width, t.Rec.Height)
                                    Dim handPad As Integer = Math.Ceiling(t.bw / 2) - 1
                                    If t.bw < 1 Then
                                        handPad = 0
                                    End If
                                    Dim radPad As Integer = t.BorderRad / Math.Sqrt(2)
                                    Dim pn As New Pen(New SolidBrush(t.linecolor), t.bw)
                                    Select Case t.ItemType
                                        Case "Label"
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)

                                            'Dim Lrec As New Rectangle(nrec.X + handpad + radPad, nrec.Y + handpad + radPad, nrec.Width - 2*(handpad + radPad), nrec.Height - 2*(handpad + radPad))
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)
                                            'gCanvas.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit

                                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                            If t.vh Then
                                                nflag += StringFormatFlags.DirectionVertical
                                            End If
                                            If t.di Then
                                                nflag += StringFormatFlags.DirectionRightToLeft
                                            End If


                                            If t.fitInBox Then
                                                FitText(g, t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            Else
                                                g.DrawString(t.txt, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            End If

                                        Case "Shape"
                                            Select Case t.Drawshape
                                                Case shapeItem.SType.Rectangulo
                                                    g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                                    getRecShape(g, t, margX, h, False)
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
                                                Case shapeItem.SType.Linea_libre
                                                    g.FillRectangle(New SolidBrush(t.BackColor), nRec)
                                                    If t.bw > 0 Then
                                                        g.DrawLine(pn, nRec.X, nRec.Y, nRec.X + nRec.Width, nRec.Y - nRec.Height)
                                                    End If

                                            End Select
                                        Case "Image"
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                            getRecShape(g, t, margX, h, False)
                                        Case "DataLabel"
                                            pn = New Pen(New SolidBrush(Color.Transparent), t.bw)
                                            'dim Lrec as New Rectangle(nrec.X + handpad + radPad, nrec.Y + handpad + radPad, nrec.Width - 2*(handpad + radPad), nrec.Height - 2*(handpad + radPad))
                                            Dim Lrec As New Rectangle(nRec.X + t.pd.Left, nRec.Y + t.pd.Top, nRec.Width - t.pd.Left - t.pd.Right, nRec.Height - t.pd.Top - t.pd.Bottom)
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            getRecShape(g, t, margX, h, False)
                                            Dim res As String = ""
                                            res = TextFromDT(t.Dfield, 0, secn, page, pages, oCv)

                                            Dim nflag As Integer = 0 'StringFormatFlags.FitBlackBox

                                            If t.vh Then
                                                nflag += StringFormatFlags.DirectionVertical
                                            End If
                                            If t.di Then
                                                nflag += StringFormatFlags.DirectionRightToLeft
                                            End If

                                            If Trim(t.fmt) = "" Then

                                            Else
                                                Try
                                                    Dim fo As Object
                                                    If Not Decimal.TryParse(res, fo) Then

                                                        Dim fo2 As Object
                                                        If IsNumeric(res) Then
                                                            res = Format(fo, t.fmt)
                                                        ElseIf DateTime.TryParse(res, fo2) Then
                                                            res = Format(fo2, t.fmt)
                                                        Else
                                                            res = "formato desconocido"
                                                        End If
                                                    Else
                                                        res = Format(fo, t.fmt)
                                                    End If
                                                Catch ferr As Exception
                                                    'res = TextFromDT(t.Dfield, k, secn, page, pages)
                                                    'res = ferr.Message
                                                End Try
                                            End If

                                            If t.TruncarTexto Then
                                                Dim ls As SizeF
                                                ls = Module1.LongitudString(g, t.fnt, res)
                                                If Lrec.Width < ls.Width Then
                                                    res = Strings.Left(res, CInt(res.Trim.Length * ls.Width / Lrec.Width) - 2) & ".."
                                                End If
                                            End If

                                            If t.fitInBox Then
                                                FitText(g, res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            Else
                                                g.DrawString(res, t.fnt, New SolidBrush(t.fcolor), Lrec, CA2SF(t.txtalign, nflag))
                                            End If
                                        Case "DataImage"
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))
                                            Dim img As Image
                                            img = ImageFromDT(t.Dfield, 0, secn.dt)
                                            If Not (img Is Nothing) Then
                                                g.DrawImage(RoundImageConers(img, nRec, t.BorderRad), nRec)
                                            End If
                                            'g.DrawImage(RoundImageConers(t.img, nRec, t.BorderRad), nRec)
                                            getRecShape(g, t, margX, h, False)

                                        Case "Barcode"
                                            g.FillPath(New SolidBrush(t.BackColor), GetPath(nRec, t.BorderRad))

                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39()
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor
                                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, t.txt), nRec)

                                            End Select

                                        Case "Fbarcode"
                                            Select Case t.Drawbarcode
                                                Case shapeItem.SBarcode.Barcode39
                                                    Dim cb As Barcodes.Barcode39 = New Barcodes.Barcode39
                                                    cb.ShowString = t.mt
                                                    cb.IncludeCheckSumDigit = t.dc
                                                    cb.TextFont = t.fnt
                                                    cb.TextColor = t.fcolor
                                                    g.DrawImage(cb.GenerateBarcodeImage(t.Rec.Width, t.Rec.Height, IIf(t.Dfield.Trim <> "", t.Dfield.Trim, "0")), nRec)
                                            End Select

                                    End Select
                                Next
                            End If



                            If i < _Rpt.Sections.Count - 1 Then
                                h += secn.h
                            End If
                        End If
                    End If


                    If i = _Rpt.Sections.Count - 3 Then
                        h = pa.Height
                        If CType(_Rpt.Sections(i + 1), Section).Visible Then
                            h += -CType(_Rpt.Sections(i + 1), Section).h
                        End If

                        If CType(_Rpt.Sections(i + 2), Section).Visible Then
                            h += -CType(_Rpt.Sections(i + 2), Section).h
                        End If
                        h += -margB
                    End If
                End If

            Next

            If morepages Then
                If RP.mcol Then
                    margX += RP.w + RP.leftm / 2
                    If margX + RP.w + RP.leftm < PA.Width Then
                        h = RP.topm
                        GoTo Restart
                    End If
                End If
                page += 1
                e.HasMorePages = True
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        e.HasMorePages = False
        pages = page
        page = 1
        cdetail = 0
    End Sub


    Private disposedValue As Boolean = False        ' To detect redundant calls



    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.

            RemoveHandler printDocument1.PrintPage, AddressOf PrintPage
        End If
        Me.disposedValue = True
    End Sub

    Public Sub PrintReportGestion(ByVal codInf As Long, Optional ByVal Pag As Integer = 1, Optional ByVal Preview As Boolean = False)

        For i As Integer = 0 To RP.Sections.Count - 1
            Dim secn As Section
            secn = RP.Sections(i)
            GetData(secn.dt, secn._sql, secn._Constr, secn._Provider)
        Next
        'PreparePrintDocument()
        'printDialog1.UseEXDialog = True
        printDocument1.PrinterSettings = printset
        PreparePrintDocumentGestion(codInf, Pag)
        RenderPage()
        AddHandler printDocument1.PrintPage, AddressOf PrintPage
        'printDocument1.DefaultPageSettings.Landscape = true
        If Preview Then
            printDialog1.Document = printDocument1
            If ppIcon Is Nothing Then
                printDialog1.ShowIcon = False
            Else
                printDialog1.ShowIcon = True
                printDialog1.Icon = ppIcon
            End If
            printDialog1.WindowState = Windows.Forms.FormWindowState.Maximized
            printDialog1.ShowInTaskbar = True
            printDialog1.ShowDialog()
        Else
            printDocument1.Print()
        End If

    End Sub

    Private Sub PreparePrintDocumentGestion(ByVal CodInf As Long, ByVal Pag As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim rsinf As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        pages = 1
        Dim ps As Printing.PageSettings = printset.DefaultPageSettings
        Dim pagebounds As Rectangle = ps.Bounds

        Dim g As Graphics
        g = Graphics.FromImage(New Bitmap(pagebounds.Width, pagebounds.Height))
        Dim ppa As New PrintPageEventArgs(g, pagebounds, ps.Bounds, ps)
        'Do
        '    paginating = True
        '    PrintPage(Nothing, ppa)
        '    'pages += 1
        '    'msgbox("")
        'Loop While ppa.HasMorePages

        'For j = 0 To 2
        '    i = IIf(j = 0, 1, IIf(j = 1, 3, 5))
        '    'If i > 0 1 Then ' Primera pagina
        '    If rsinf.Open("SELECT * FROM zzz_documentos_detnv WHERE  codigo_impresion =" & CodInf & " AND pagina_documento = '1' and zona = " & i & " and indice = 1 order by 1,2,3,4,5", ObjetoGlobal.cn) Then
        '        If Not rsinf.EOF Then
        '            _Rpt.Sections(i).dt = rsinf.cnDataSet.Tables(0)
        '        End If
        '    End If
        '    rsinf.Close()
        '    If rsinf.Open("SELECT * FROM zzz_documentos_detnv WHERE  codigo_impresion =" & CodInf & " AND pagina_documento = " & "1" & " and zona = 0 and indice = 0 order by 1,2,3,4,5", ObjetoGlobal.cn) Then
        '        If Not rsinf.EOF Then
        '            _Rpt.Sections(i - 1).dt = rsinf.cnDataSet.Tables(0)
        '        End If
        '    End If
        '    rsinf.Close()
        '    'End If
        'Next

        'If i = Pag Then ' última página
        '    '    If rsinf.Open("SELECT * FROM zzz_documentos_detnv WHERE  codigo_impresion =" & CodInf & " AND pagina_documento = " & Pag & " and zona = 2 and indice = 1 order by 1,2,3,4,5", ObjetoGlobal.cn) Then
        '    '        If Not rsinf.EOF Then
        '    '            _Rpt.Sections(_Rpt.Sections.Count).dt = rsinf.cnDataSet.Tables(0)
        '    '        Else
        '    '            rsinf.Close()
        '    '            If rsinf.Open("SELECT * FROM zzz_documentos_detnv WHERE  codigo_impresion =" & CodInf & " AND pagina_documento = " & Pag & " and zona = 2 and indice = 0 order by 1,2,3,4,5", ObjetoGlobal.cn) Then
        '    '                _Rpt.Sections(_Rpt.Sections.Count - 1).dt = rsinf.cnDataSet.Tables(0)
        '    '            End If
        '    '        End If
        '    '        rsinf.Close()
        '    '    End If
        '    'Else ' Las demás
        '    '    If rsinf.Open("SELECT * FROM zzz_documentos_detnv WHERE  codigo_impresion =" & CodInf & " AND pagina_documento = " & Pag & " and zona = 1 order by 1,2,3,4,5", ObjetoGlobal.cn) Then
        '    '        If Not rsinf.EOF Then
        '    '            _Rpt.Sections(1 + rsinf!indice).dt = rsinf.cnDataSet.Tables(0)
        '    '        End If
        '    '        rsinf.Close()
        '    '    End If
        '    'End If
        'Next
        'If Not rsinf.EOF Then
        'PrintPage(Nothing, ppa)
        'End If

        'Do
        '    paginating = True
        '    PrintPage(Nothing, ppa)
        '    'pages += 1
        '    'msgbox("")
        'Loop While ppa.HasMorePages

        'Next

        paginating = True
    End Sub
    Private Function recortarcadena(ByVal g As Graphics, ByVal fnt As Font, ByVal cad As String, ByVal ancho As Integer)
        Dim loncad As SizeF

        If cad.Trim = "" Then
            Return cad
        End If
        loncad = Module1.LongitudString(g, fnt, cad)
        If loncad.Width > ancho Then
            While loncad.Width > ancho
                cad = Strings.Left(cad, Strings.Left(cad, Len(cad) - 1))
                loncad = Module1.LongitudString(g, fnt, cad)
            End While
            cad = Left(cad, Strings.Left(cad, Len(cad) - 2)) & ".."
            Return cad
        Else
            Return cad
        End If

    End Function
    Public Function Codigo_eventos(k, cod)
        ScriptsEventos(k) = cod
    End Function

#Region " Eventos informe "
    Public Enum event_numero
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
    Function EmpiezaInforme(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.inicio_informe)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' Empieza informe:
        'event_numero.inicio_informe
        'p(0) = clase report
        'p(1) = Datatable sección
        'p(2) = clase variables




    End Function
    Function EmpiezaDocumento(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.Inicio_docmento)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' Empieza informe:
        'event_numero.Inicio_docmento
        'p(0) = clase report
        'p(1) = Datatable informe
        'p(2) = clase variables

    End Function

    Function TerminaDocumento(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.Fin_documento)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' fin documento
        'event_numero.Fin_documento
        'p(0) = clase report
        'p(1) = Datatable informe
        'p(2) = clase variables

    End Function

    Function PrimeraCabecera(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.Cabecera_inicial)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' Cabecera inicial informe:
        'event_numero.Cabecera_inicial
        'p(0) = clase report
        'p(1) = Datatable sección
        'p(2) = clase variables

    End Function

    Function SiguientesCabeceras(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.Siguientes_cabeceras)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' cebeceras siguientes informe:
        'event_numero.Siguientes_cabeceras
        'p(0) = clase report
        'p(1) = Datatable sección
        'p(2) = clase variables


    End Function

    Function PieFinal(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.Pie_final)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' Pie final informe:
        'event_numero.Pie_final
        'p(0) = clase report
        'p(1) = Datatable sección
        'p(2) = clase variables


    End Function

    Function PieParciales(ByVal odt As Object)
        Dim lcCode = ScriptsEventos(event_numero.Pies_parciales)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' pçíes parciales informe:
        'event_numero.Pies_parciales
        'p(0) = clase report
        'p(1) = Datatable sección
        'p(2) = clase variables


    End Function

    Function LineasDocumento(ByVal odt As Data.DataTable, ByVal lin As Long)
        Dim lcCode = ScriptsEventos(event_numero.Lineas_documento)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        If lcCode.trim = "" Then Return ""

        Dim Drl As libcomunes.MiDataRow = New libcomunes.MiDataRow
        Try
            Drl.rows = odt.Select("num_linea = " & lin)
            Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv, Drl, lin))
            'MsgBox(oCv("@importe_debe") & "  " & oCv("@importe_haber"))
        Catch ex As Exception

        End Try

        ' Líneas documento informe:
        'p(0) = clase report
        'p(1) = Datatable sección
        'p(2) = clase variables
        'p(3) = Midatarow de las líneas Ejemplo p(3).importe_debe
        'p(4) = número de línea
        ' Ejemplo: p(2).totalDebe = p(2).totalDebe + p(3).importe_debe

    End Function

    Function SeccionPersonalizada(ByVal odt As Object, ByVal seccion As Integer)
        Dim lcCode = ScriptsEventos(event_numero.Seccion)
        Dim loScript As Westwind.wwScripting.wwScripting
        If lcCode.trim = "" Then Return ""
        loScript = AbrirScripts()
        Return ""
        If lcCode.trim = "" Then Return ""
        Dim lcResult = CStr(loScript.ExecuteCode(lcCode, Me, odt, oCv))

        ' Sección informe:
        ' event_numero.Seccion
        ' p(0) = clase report
        ' p(1) = Datatable sección
        ' p(2) = clase variables

    End Function

    Private Function AbrirScripts() As Westwind.wwScripting.wwScripting
        Dim loScript As Westwind.wwScripting.wwScripting = New Westwind.wwScripting.wwScripting("VB")
        loScript.lSaveSourceCode = True

        ' loScript.CreateAppDomain("WestWind");  // force into AppDomain
        loScript.AddAssembly("system.windows.forms.dll", "System.Windows.Forms")
        loScript.AddAssembly("System.Data.Common.dll", "System.Data")
        loScript.AddAssembly("cnRecordset.dll")
        loScript.AddAssembly("ObjetoGlobal.dll")
        loScript.og = ObjetoGlobal

        Return loScript

    End Function
#End Region

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
	'End Class
	#End Region
	
	
	
End Class
