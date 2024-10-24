Imports System.IO
Imports System.Drawing

Public Class FrmFirmaDocumentos
    Private sign, ok, clear, please As Bitmap
    Private lcdX, lcdY, screen As Integer
    Private lcdSize As UInteger
    Private data, data2 As String
    Public TextoAlbaran As String
    Public Importes As Double
    Public path As String

    Private Sub BtBorraPantalla_Click(sender As Object, e As EventArgs) Handles BtBorraPantalla.Click
        SigPlusNET1.ClearTablet()
    End Sub

    Private Sub Seguir_Click(sender As Object, e As EventArgs) Handles Seguir.Click
        Dim NombreArchivo As String
        Dim myimage As Image

        SigPlusNET1.SetTabletState(1)
        NombreArchivo = path.Trim & Trim("\IM" + Replace(TextoAlbaran, " ", "")) + ".bmp"
        SigPlusNET1.SetImageXSize(400)
        SigPlusNET1.SetImageYSize(100)
        SigPlusNET1.SetJustifyMode(5)

        myimage = SigPlusNET1.GetSigImage()
        myimage.Save(NombreArchivo, System.Drawing.Imaging.ImageFormat.Bmp)
        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub BtSeguirSinFirmar_Click(sender As Object, e As EventArgs) Handles BtSeguirSinFirmar.Click
        Dim NombreArchivo As String

        SigPlusNET1.SetTabletState(1)
        NombreArchivo = Trim("IM" + Replace(TextoAlbaran, " ", "")) + ".bmp"
        If File.Exists(Trim(path) + "\" + NombreArchivo) Then
            MsgBox("Este albarán no ha sido firmado nunca")
        End If
        'NombreArchivo = Trim(path) + "\" + NombreArchivo
        'SigPlus1.WriteImageFile NombreArchivo
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtCancelaFirma_Click(sender As Object, e As EventArgs) Handles BtCancelaFirma.Click
        'SigPlusNET1.SetTabletState(1)
        'SigPlusNET1.ClearTablet() 'erases ink and annotation from display
        'SigPlusNET1.LCDRefresh(0, 0, 0, 320, 240) 'Clears entire LCD screen
        'SigPlusNET1.SetLCDCaptureMode(1) 'Resets regular auto-clear inking
        'SigPlusNET1.SetTabletState(0)
        SigPlusNET1.ClearTablet()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Public HaFirmado As Boolean

    Private Sub TextosVentana()
        Dim f As Font = New Drawing.Font("Arial", 9.0F, Drawing.FontStyle.Regular)

        SigPlusNET1.SetTabletState(1) 'Turns tablet on to collect signature
        SigPlusNET1.LCDRefresh(0, 0, 0, 240, 64)
        SigPlusNET1.SetTranslateBitmapEnable(False)

        'Get LCD size in pixels.
        lcdSize = SigPlusNET1.LCDGetLCDSize()
        lcdX = CInt(lcdSize And &HFFFF)
        lcdY = CInt(lcdSize >> 16 And &HFFFF)
        SigPlusNET1.LCDWriteString(0, 2, 15, 60, f, TextoAlbaran)
        SigPlusNET1.LCDWriteString(0, 2, 160, 60, f, Format(Importes, "#,###,##0.00") + " €")
        SigPlusNET1.LCDWriteString(0, 2, 10, 60, f, "-------------------------------------------------")


    End Sub


    Private Sub FrmFirmaDocumentos_Load(sender As Object, e As EventArgs) Handles Me.Load
        SigPlusNET1.Width = 400
        SigPlusNET1.Height = 100
        HaFirmado = False
        TextosVentana()

    End Sub
End Class