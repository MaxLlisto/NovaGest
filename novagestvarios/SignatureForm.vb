'
' SignatureForm.vb
'
' Display signature input form on PC screen and on STU pad and process user input
'
' Copyright (c) 2015 Wacom GmbH. All rights reserved
'
Public Class SignatureForm

    Private m_tablet As wgssSTU.Tablet
    Private m_capability As wgssSTU.ICapability
    Private m_information As wgssSTU.IInformation

    ' In order to simulate buttons, we have our own Button class that stores the bounds and event handler.
    ' Using an array of these makes it easy to add or remove buttons as desired.
    Private Delegate Sub ButtonClick()
    Private Structure Button
        Public Bounds As Rectangle 'in Screen coordinates
        Public Text As String
        Public Click As EventHandler
        Public Sub PerformClick()
            Click(Me, Nothing)
        End Sub
    End Structure

    Private m_penInk As Pen   ' cached object.

    ' The isDown flag is used like this:
    ' 0 = up
    ' +ve = down, pressed on button number
    ' -1 = down, inking
    ' -2 = down, ignoring
    Private m_isDown As Int16

    Private m_penData As List(Of wgssSTU.IPenData) ' Array of data being stored. This can be subsequently used as desired.

    Private m_btns() As Button  ' The array of buttons that we are emulating.

    Private m_bitmap As Bitmap      ' This bitmap that we display on the screen.
    Private m_encodingMode As Byte  ' How we send the bitmap to the device.
    Private m_bitmapData() As Byte  ' This is the flattened data of the bitmap that we send to the device.

    Private m_ParentForm As Visitas ' link to calling form

    ' SignatureForm
    Public Sub New(ByVal ParentForm As Visitas, ByVal usbDevice As wgssSTU.IUsbDevice, ByVal otxt As TextBox)
        m_ParentForm = ParentForm

        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

        InitializeComponent() ' This call is required by the Windows Form Designer.

        m_penData = New List(Of wgssSTU.IPenData)

        m_tablet = New wgssSTU.Tablet()
        Dim protocolHelper = New wgssSTU.ProtocolHelper()

        Dim ec = m_tablet.usbConnect(usbDevice, True)
        If (ec.value = 0) Then
            m_capability = m_tablet.getCapability()
            m_information = m_tablet.getInformation()
            print("Connected " + m_information.modelName)
        Else
            Throw New Exception(ec.message)
        End If

        Me.SuspendLayout()
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi

        'Establecer el tamaño de la ventana del cliente como tamaño real,
        'Basado en el DPI reportado del monitor.

        Dim clientSize = New Size((m_capability.tabletMaxX / 2540.0F * 96.0F), (m_capability.tabletMaxY / 2540.0F * 96.0F))
        Me.ClientSize = clientSize
        Me.ResumeLayout()

        ReDim m_btns(3)

        If (usbDevice.idProduct <> &HA2) Then
            ' Place the buttons across the bottom of the screen.
            Dim w2 = m_capability.screenWidth / 3
            Dim w3 = m_capability.screenWidth / 3
            Dim w1 = m_capability.screenWidth - w2 - w3
            Dim y = m_capability.screenHeight * 6 / 7
            Dim h = m_capability.screenHeight - y

            m_btns(0).Bounds = New Rectangle(0, y, w1, h)
            m_btns(1).Bounds = New Rectangle(w1, y, w2, h)
            m_btns(2).Bounds = New Rectangle(w1 + w2, y, w3, h)

        Else
            'El STU-300 es muy superficial, por lo que es mejor utilizarlo
            'los botones al lado de la pantalla en su lugar.

            Dim x = m_capability.screenWidth * 3 / 4
            Dim w = m_capability.screenWidth - x

            Dim h2 = m_capability.screenHeight / 3
            Dim h3 = m_capability.screenHeight / 3
            Dim h1 = m_capability.screenHeight - h2 - h3

            m_btns(0).Bounds = New Rectangle(x, 0, w, h1)
            m_btns(1).Bounds = New Rectangle(x, h1, w, h2)
            m_btns(2).Bounds = New Rectangle(x, h1 + h2, w, h3)
        End If
        m_btns(0).Text = "OK"
        m_btns(1).Text = "Borrar"
        m_btns(2).Text = "Cancelar"
        m_btns(0).Click = New EventHandler(AddressOf btnOK_Click)
        m_btns(1).Click = New EventHandler(AddressOf btnClear_Click)
        m_btns(2).Click = New EventHandler(AddressOf btnCancel_Click)


        'Desactiva el color si el controlador masivo no está instalado.
        'Esto no es necesario, pero subiendo imágenes en color sin el controlador
        ' es muy lento.

        'Calcular el modo de codificación que se utilizará para actualizar la imagen

        Dim idP = m_tablet.getProductId()
        Dim encodingFlag = protocolHelper.simulateEncodingFlag(idP, 0)
        Dim useColor = False
        If (encodingFlag And (wgssSTU.encodingFlag.EncodingFlag_16bit Or wgssSTU.encodingFlag.EncodingFlag_24bit)) Then
            If (m_tablet.supportsWrite()) Then
                useColor = True
            End If
        End If
        If (encodingFlag And wgssSTU.encodingFlag.EncodingFlag_24bit) Then
            If (m_tablet.supportsWrite()) Then
                m_encodingMode = wgssSTU.encodingMode.EncodingMode_24bit_Bulk
            Else
                m_encodingMode = wgssSTU.encodingMode.EncodingMode_24bit
            End If

        ElseIf (encodingFlag And wgssSTU.encodingFlag.EncodingFlag_16bit) Then
            If (m_tablet.supportsWrite()) Then
                m_encodingMode = wgssSTU.encodingMode.EncodingMode_16bit_Bulk
            Else
                m_encodingMode = wgssSTU.encodingMode.EncodingMode_16bit
            End If
        Else
            ' assumes 1bit is available
            m_encodingMode = wgssSTU.encodingMode.EncodingMode_1bit
        End If

        'Tamaño del mapa de bits al tamaño de la pantalla LCD.
        'Esta aplicación utiliza el mismo mapa de bits tanto para la pantalla como para el cliente (ventana).
        'Sin embargo, con un alto DPI, este mapa de bits se estirará y sería mejor
        'crear mapas de bits individuales para la pantalla y el cliente en resoluciones nativas.

        'm_bitmap = New Bitmap(m_capability.screenWidth, m_capability.screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        ' aaa
        m_bitmap = DrawText(otxt.Text, otxt.Font, otxt.ForeColor, otxt.BackColor, m_capability.screenWidth, m_capability.screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)

        Dim gfx = Graphics.FromImage(m_bitmap)
        'gfx.Clear(Color.White)

        'Usa píxeles para las unidades, ya que DPI no será preciso para la tableta LCD.
        Dim font = New Font(FontFamily.GenericSansSerif, m_btns(0).Bounds.Height / 2.0F, GraphicsUnit.Pixel)
        Dim sf = New StringFormat()
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        If (useColor) Then
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit
        Else
            ' Anti-aliasing should be turned off for monochrome devices.
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel
        End If

        'Dibujar los botones
        For i As Integer = 0 To (m_btns.Length - 1)
            If (useColor) Then
                gfx.FillRectangle(Brushes.LightGray, m_btns(i).Bounds)
            End If
            gfx.DrawRectangle(Pens.Black, m_btns(i).Bounds)
            gfx.DrawString(m_btns(i).Text, font, Brushes.Black, m_btns(i).Bounds, sf)
        Next


        gfx.Dispose()
        font.Dispose()

        'Finalmente, use este mapa de bits para el fondo de la ventana.

        Me.BackgroundImage = m_bitmap
        Me.BackgroundImageLayout = ImageLayout.Stretch

        'Ahora que se ha creado el mapa de bits, se debe convertir a dispositivo nativo
        ' formato.

        'Lamentablemente, no es posible que el componente COM nativo
        'entender los mapas de bits .NET. Por lo tanto, hemos convertido el mapa de bits .NET
        'en un blob de memoria que será comprendido por COM.

        Dim stream = New System.IO.MemoryStream()
        m_bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
        m_bitmapData = protocolHelper.resizeAndFlatten(stream.ToArray(),
                            0, 0, CUInt(m_bitmap.Width), CUInt(m_bitmap.Height),
                            m_capability.screenWidth, m_capability.screenHeight,
                            CByte(m_encodingMode),
                            wgssSTU.Scale.Scale_Fit, 0, 0)

        protocolHelper = Nothing

        stream.Dispose()

        'Si desea optimizar aún más la transferencia de imágenes, puede comprimir la imagen usando
        'El algoritmo Zlib.

        Dim useZlibCompression = False
        If ((Not useColor) And useZlibCompression) Then
            ' m_bitmapData = compress_using_zlib(m_bitmapData); // insert compression here!
            m_encodingMode = m_encodingMode Or wgssSTU.encodingMode.EncodingMode_Zlib
        End If

        ' Calculate the size and cache the inking pen.

        Dim s = Me.AutoScaleDimensions
        Dim inkWidthMM = 0.7F
        m_penInk = New Pen(Color.DarkBlue, inkWidthMM / 25.4F * ((s.Width + s.Height) / 2.0F))
        m_penInk.StartCap = m_penInk.EndCap = System.Drawing.Drawing2D.LineCap.Round
        m_penInk.LineJoin = System.Drawing.Drawing2D.LineJoin.Round


        ' Add the delagate that receives pen data.
        AddHandler m_tablet.onPenData, New wgssSTU.ITabletEvents2_onPenDataEventHandler(AddressOf onPenData)
        AddHandler m_tablet.onGetReportException, New wgssSTU.ITabletEvents2_onGetReportExceptionEventHandler(AddressOf onGetReportException)

        ' Initialize the screen
        clearScreen()

        'Habilitar los datos del lápiz en la pantalla (si no lo está ya)
        m_tablet.setInkingMode(&H1)



    End Sub
    Private Function tabletToClient(ByVal penData As wgssSTU.IPenData)

        ' Client means the Windows Form coordinates.
        'Return New PointF(penData.x * Me.ClientSize.Width / m_capability.tabletMaxX, penData.y * Me.ClientSize.Height / m_capability.tabletMaxY)
        Return New PointF(CDbl(penData.x) * CDbl(Me.ClientSize.Width) / CDbl(m_capability.tabletMaxX), CDbl(penData.y) * CDbl(Me.ClientSize.Height) / CDbl(m_capability.tabletMaxY))
    End Function

    Private Function tabletToScreen(ByVal penData As wgssSTU.IPenData)
        ' Screen means LCD screen of the tablet.
        Return Point.Round(New PointF(CDbl(penData.x) * CDbl(m_capability.screenWidth) / CDbl(m_capability.tabletMaxX), CDbl(penData.y) * CDbl(m_capability.screenHeight) / CDbl(m_capability.tabletMaxY)))
    End Function

    Private Function clientToScreen(ByVal pt As Point)

        'client (window) coordinates to LCD screen coordinates. 
        ' This is needed for converting mouse coordinates into LCD bitmap coordinates as that's
        ' what this application uses as the coordinate space for buttons.
        Return Point.Round(New PointF(CDbl(pt.X) * CDbl(m_capability.screenWidth) / CDbl(Me.ClientSize.Width), CDbl(pt.Y) * CDbl(m_capability.screenHeight) / CDbl(Me.ClientSize.Height)))
    End Function

    Private Sub clearScreen()
        ' note: There is no need to clear the tablet screen prior to writing an image.
        m_tablet.writeImage(m_encodingMode, m_bitmapData)

        m_penData.Clear()
        m_isDown = 0
        Me.Invalidate()
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.m_penData.Clear()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (m_penData.Count <> 0) Then
            clearScreen()
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (m_penData.Count > 0) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub SignatureForm_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseClick
        ' Enable the mouse to click on the simulated buttons that we have displayed.

        ' Note that this can add some tricky logic into processing pen data
        ' if the pen was down at the time of this click, especially if the pen was logically
        ' also 'pressing' a button! This demo however ignores any that.

        Dim pt = clientToScreen(e.Location)
        For Each btn In m_btns
            If (btn.Bounds.Contains(pt)) Then
                btn.PerformClick()
                Exit For
            End If
        Next

    End Sub

    Private Sub SignatureForm_Closed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        ' Ensure that you correctly disconnect from the tablet, otherwise you are 
        ' likely to get errors when wanting to connect a second time.
        If (m_tablet IsNot Nothing) Then
            RemoveHandler m_tablet.onPenData, New wgssSTU.ITabletEvents2_onPenDataEventHandler(AddressOf onPenData)
            RemoveHandler m_tablet.onGetReportException, New wgssSTU.ITabletEvents2_onGetReportExceptionEventHandler(AddressOf onGetReportException)

            m_tablet.setInkingMode(&H0)
            m_tablet.setClearScreen()
            m_tablet.disconnect()
        End If

        m_penInk.Dispose()

    End Sub
    Private Sub onGetReportException(ByVal tabletEventsException As wgssSTU.ITabletEventsException)
        Try
            tabletEventsException.getException()
        Catch e As Exception
            print("Error: " + e.Message)
            MessageBox.Show("Error: " + e.Message)
            m_tablet.disconnect()
            m_tablet = Nothing
            m_penData = Nothing
            Me.Close()
        End Try
    End Sub
    Private Sub onPenData(ByVal penData As wgssSTU.IPenData) ' Process incoming pen data

        Dim pt = tabletToScreen(penData)

        Dim btn = 0 ' will be +ve if the pen is over a button.

        For i As Integer = 0 To (m_btns.Length - 1)
            If (m_btns(i).Bounds.Contains(pt)) Then
                btn = i + 1
                Exit For
            End If
        Next


        Dim isDown = (penData.sw <> 0)

        ' This code uses a model of four states the pen can be in:
        ' down or up, and whether this is the first sample of that state.

        If (isDown) Then

            If (m_isDown = 0) Then

                ' transition to down
                If (btn > 0) Then
                    ' We have put the pen down on a button.
                    ' Track the pen without inking on the client.
                    m_isDown = btn
                Else
                    ' We have put the pen down somewhere else.
                    ' Treat it as part of the signature.
                    m_isDown = -1
                End If
            Else
                ' already down, keep doing what we're doing!
            End If

            ' draw
            If (m_penData.Count <> 0 And m_isDown = -1) Then

                ' Draw a line from the previous down point to this down point.
                ' This is the simplist thing you can do; a more sophisticated program
                ' can perform higher quality rendering than this!

                Dim gfx = Me.CreateGraphics()
                gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality

                Dim prevPenData = m_penData(m_penData.Count - 1)

                Dim prev = tabletToClient(prevPenData)

                gfx.DrawLine(m_penInk, prev, tabletToClient(penData))
                gfx.Dispose()
            End If

            ' The pen is down, store it for use later.
            If (m_isDown = -1) Then
                m_penData.Add(penData)
            End If
        Else
            If (m_isDown <> 0) Then
                ' transition to up
                If (btn > 0) Then
                    ' The pen is over a button
                    If (btn = m_isDown) Then
                        ' The pen was pressed down over the same button as is was lifted now. 
                        ' Consider that as a click!
                        m_btns(btn - 1).PerformClick()
                    End If
                End If
                m_isDown = 0
            Else
                ' still up
            End If

            ' Add up data once we have collected some down data.
            If (m_penData.Count <> 0) Then
                m_penData.Add(penData)
            End If
        End If
    End Sub

    Private Sub SignatureForm_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        If (m_penData.Count <> 0) Then
            ' Redraw all the pen data up until now!

            Dim gfx = e.Graphics
            gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            Dim isDown = False
            Dim prev = New PointF()
            For i As Integer = 0 To (m_penData.Count - 1)
                If (m_penData(i).sw <> 0) Then
                    If (Not isDown) Then
                        isDown = True
                        prev = tabletToClient(m_penData(i))
                    Else
                        Dim curr = tabletToClient(m_penData(i))
                        gfx.DrawLine(m_penInk, prev, curr)
                        prev = curr
                    End If
                Else
                    If (isDown) Then
                        isDown = False
                    End If
                End If
            Next
        End If
    End Sub
    Public Function GetSigImage()
        Dim bitmap As Bitmap
        Dim brush As SolidBrush
        Dim rect As Rectangle
        Dim p1, p2 As Point

        rect.X = rect.Y = 0
        rect.Width = m_capability.screenWidth
        rect.Height = m_capability.screenHeight

        Try
            bitmap = New Bitmap(rect.Width, rect.Height)


            'bitmap = DrawText(oTxt.Text, oTxt.Font, oTxt.ForeColor, oTxt.BackColor)

            Dim gfx = Graphics.FromImage(bitmap)
            Dim s = Me.AutoScaleDimensions
            '            Dim inkWidthMM = 0.7F
            Dim inkWidthMM = 1.0F
            m_penInk = New Pen(Color.DarkBlue, inkWidthMM / 25.4F * ((s.Width + s.Height) / 2.0F))
            m_penInk.StartCap = m_penInk.EndCap = System.Drawing.Drawing2D.LineCap.Round
            m_penInk.LineJoin = System.Drawing.Drawing2D.LineJoin.Round

            gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality

            brush = New SolidBrush(Color.White)
            gfx.FillRectangle(brush, 0, 0, rect.Width, rect.Height)

            For i As Integer = 1 To (m_penData.Count - 1)
                p1 = tabletToScreen(m_penData(i - 1))
                p2 = tabletToScreen(m_penData(i))

                If (m_penData(i - 1).sw > 0 Or m_penData(i).sw > 0) Then
                    gfx.DrawLine(m_penInk, p1, p2)
                End If
            Next
        Catch ex As Exception
            print("Exception: " + ex.Message)
            MsgBox("Exception: " + ex.Message)
            bitmap = Nothing
        End Try
        Return bitmap
    End Function

    Private Function DrawText(ByVal text As String, ByVal font As Font, ByVal textColor As Color, ByVal backColor As Color, nWidth As Integer, nHeighr As Integer, fPixel As VariantType) As Image
        Dim img As Image = New Bitmap(1, 1)
        Dim drawing As Graphics = Graphics.FromImage(img)
        Dim textSize As SizeF = drawing.MeasureString(text, font)
        img.Dispose()
        drawing.Dispose()
        img = New Bitmap(nWidth, nHeighr, fPixel)
        drawing = Graphics.FromImage(img)
        drawing.Clear(backColor)
        Dim textBrush As Brush = New SolidBrush(textColor)
        drawing.DrawString(text, font, textBrush, 0, 0)
        drawing.Save()
        textBrush.Dispose()
        drawing.Dispose()
        Return img
    End Function
    Private Sub print(ByVal txt As String)
        m_ParentForm.print(txt)
    End Sub
End Class