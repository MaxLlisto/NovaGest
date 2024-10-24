﻿Imports System
Imports System.IO
Imports System.Collections
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms

Namespace GdiPlusLib


  Public Class Gdip
    Private Shared codecs As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()

    Private Shared Function GetCodecClsid(ByVal filename As String, ByRef clsid As Guid) As Boolean
      clsid = Guid.Empty
      Dim ext As String = Path.GetExtension(filename)
      If ext Is Nothing Then
        Return False
      End If
      ext = "*" & ext.ToUpper()
      For Each codec As ImageCodecInfo In codecs
        If codec.FilenameExtension.IndexOf(ext) >= 0 Then
          clsid = codec.Clsid
          Return True
        End If
      Next
      Return False
    End Function

        Public Shared Function SaveBMPTofile(ByVal picname As String, ByVal bminfo As IntPtr, ByVal pixdat As IntPtr) As Boolean
            Dim sd As New SaveFileDialog()

            Dim clsid As Guid
            If Not GetCodecClsid(picname, clsid) Then
                MessageBox.Show("Extensión de archivo desconocida " & Path.GetExtension(sd.FileName), "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            Dim img As IntPtr = IntPtr.Zero
            Dim st As Integer = GdipCreateBitmapFromGdiDib(bminfo, pixdat, img)
            If (st <> 0) OrElse (img = IntPtr.Zero) Then
                Return False
            End If

            st = GdipSaveImageToFile(img, picname, clsid, IntPtr.Zero)
            GdipDisposeImage(img)
            Return st = 0
        End Function

        Public Shared Function SaveDIBAs(ByVal picname As String, ByVal bminfo As IntPtr, ByVal pixdat As IntPtr) As Boolean
      Dim sd As New SaveFileDialog()

      sd.FileName = picname
      sd.Title = "Save bitmap as..."
      sd.Filter = "Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|JPEG file (*.jpg)|*.jpg|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*"
      sd.FilterIndex = 1
      If sd.ShowDialog() <> DialogResult.OK Then
        Return False
      End If

      Dim clsid As Guid
      If Not GetCodecClsid(sd.FileName, clsid) Then
        MessageBox.Show("Unknown picture format for extension " & Path.GetExtension(sd.FileName), "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False
      End If

      Dim img As IntPtr = IntPtr.Zero
      Dim st As Integer = GdipCreateBitmapFromGdiDib(bminfo, pixdat, img)
      If (st <> 0) OrElse (img = IntPtr.Zero) Then
        Return False
      End If

      st = GdipSaveImageToFile(img, sd.FileName, clsid, IntPtr.Zero)
      GdipDisposeImage(img)
      Return st = 0
    End Function

    <DllImport("gdiplus.dll", ExactSpelling:=True)> _
    Friend Shared Function GdipCreateBitmapFromGdiDib(ByVal bminfo As IntPtr, ByVal pixdat As IntPtr, ByRef image As IntPtr) As Integer
    End Function

    <DllImport("gdiplus.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)> _
    Friend Shared Function GdipSaveImageToFile(ByVal image As IntPtr, ByVal filename As String, <[In]()> ByRef clsid As Guid, ByVal encparams As IntPtr) As Integer
    End Function

    <DllImport("gdiplus.dll", ExactSpelling:=True)> _
    Friend Shared Function GdipDisposeImage(ByVal image As IntPtr) As Integer
    End Function

  End Class

End Namespace
' namespace GdiPlusLib 
