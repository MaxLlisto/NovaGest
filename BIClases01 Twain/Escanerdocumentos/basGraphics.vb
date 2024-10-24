Imports Microsoft.VisualBasic
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Reflection


'DIB structure:
'1) BITMAPINFOHEADER
'2) ColorTable or Bitfields array, if any(i.e, predicated on bpp of image)
'3) Bitmap's bits

Module basGraphics

    Private Declare Function GdipCreateBitmapFromGdiDib Lib "GdiPlus.dll" (
    ByVal pBIH As IntPtr,
    ByVal pPix As IntPtr,
    ByRef pBitmap As IntPtr) As Integer


    <StructLayout(LayoutKind.Sequential, Pack:=2)> Public Class BitmapInfoHeader '40 bytes
        Public biSize As Integer                'Size of the header structure to identify to the API which Bitmap header type we’re passing it.
        Public biWidth As Integer               'Width of the Bitmap we wish to create
        Public biHeight As Integer              'Height of the Bitmap we wish to create
        Public biPlanes As Short                'Colour planes of the Bitmap we wish to create (always 1)
        Public biBitCount As Short              'Colour depth of the Bitmap we wish to create
        Public biCompression As Integer         'Compression method used to store the Bitmap data
        Public biSizeImage As Integer           'Size (in bytes) of the image data
        Public biXPelsPerMeter As Integer       'Number of horizontal pixels per meter on the source device
        Public biYPelsPerMeter As Integer       'Number of vertical pixels per meter on the source device
        Public biClrUsed As Integer             'Number of colours used from palette
        Public biClrImportant As Integer        'Number of colours from palette that are absolutely required for proper display (seldom used any more)
    End Class


    Public Function BitmapFromDIB(ByVal hDIB As Integer) As Bitmap

        Dim pDIB As IntPtr


        'Identify the memory pointer to the DIB Handler (hDIB)
        pDIB = New IntPtr(hDIB)

        Return BitmapFromDIB(pDIB)

    End Function

    Public Function BitmapFromDIB(ByVal pDIB As IntPtr) As Bitmap

        Dim intStatus As Integer
        Dim pBmp As IntPtr
        Dim pPix As IntPtr
        Dim mi As MethodInfo

        'Call external GDI method
        ' mi = GetType(Bitmap).GetMethod("FromGDIplus", BindingFlags.[Static] Or BindingFlags.NonPublic)
        'If mi Is Nothing Then
        'Return Nothing
        'End If

        'Get pointer to bitmap header info
        pPix = GetPixelInfo(pDIB)

        'Initialize memory pointer where Bitmap will be saved
        pBmp = IntPtr.Zero

        'Call external methosd that saves bitmap into pointer
        intStatus = GdipCreateBitmapFromGdiDib(pDIB, pPix, pBmp)

        'If success return bitmap, if failed return null
        If (intStatus = 0) AndAlso (pBmp <> IntPtr.Zero) Then
#Disable Warning BC42104 ' La variable 'mi' se usa antes de que se le haya asignado un valor. Podría darse una excepción de referencia NULL en tiempo de ejecución.
            Return DirectCast(mi.Invoke(Nothing, New Object() {pBmp}), Bitmap)
#Enable Warning BC42104 ' La variable 'mi' se usa antes de que se le haya asignado un valor. Podría darse una excepción de referencia NULL en tiempo de ejecución.
        Else
            Return Nothing
        End If

    End Function

    Public Sub SavehDibToTiff(ByVal hDIB As Integer, ByVal strFileName As String, ByVal intXRes As Integer, ByVal intTRes As Integer)

        Dim pDIB As IntPtr


        'Identify the memory pointer to the DIB Handler (hDIB)
        pDIB = New IntPtr(hDIB)

        SavehDibToTiff(pDIB, strFileName, intXRes, intTRes)

    End Sub

    Public Sub SavehDibToTiff(ByVal pDIB As IntPtr, ByVal strFileName As String, ByVal intXRes As Integer, ByVal intTRes As Integer)

        Dim lngEV As Long
        Dim NewBitmap As Bitmap
        Dim ici As ImageCodecInfo


        'Save the contents of DIB pointer into bitmap object
        NewBitmap = BitmapFromDIB(pDIB)

        'Set resolution if needed
        If intXRes > 0 AndAlso intTRes > 0 Then
            NewBitmap.SetResolution(intXRes, intTRes)
        End If

        'Create an instance of the windows TIFF encoder
        ici = GetEncoderInfo("image/tiff")

        'Define encoder parameters
        Dim eps As New EncoderParameters(1)

        'Only one parameter in this case (compression)
        'Create an Encoder Value for TIFF compression Group 4
        lngEV = CLng(EncoderValue.CompressionCCITT4)
        eps.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Compression, lngEV)

        'Save file
        NewBitmap.Save(strFileName, ici, eps)

    End Sub

    Private Function GetPixelInfo(ByVal pBMP As IntPtr) As IntPtr

        Dim p As Integer
        Dim bmi As BitmapInfoHeader
        Dim bmprect As Rectangle

        bmi = New BitmapInfoHeader()
        Marshal.PtrToStructure(pBMP, bmi)

        bmprect.X = bmprect.Y = 0
        bmprect.Width = bmi.biWidth
        bmprect.Height = bmi.biHeight

        If (bmi.biSizeImage = 0) Then
            On Error Resume Next
            bmi.biSizeImage = Int((((bmi.biWidth * bmi.biBitCount) + 31) & Hex(Not (31))) / 2 ^ 3) * bmi.biHeight
        End If

        p = bmi.biClrUsed
        If ((p = 0) And (bmi.biBitCount <= 8)) Then
            p = Int(1 * 2 ^ bmi.biBitCount)
        End If
        p = (p * 4) + bmi.biSize + CType(pBMP.ToInt32, Integer)

        Return New IntPtr(p)

    End Function

    Private Function GetEncoderInfo(ByVal strMimeType As String) As ImageCodecInfo

        Dim r As Integer
        Dim encoders As ImageCodecInfo()


        encoders = ImageCodecInfo.GetImageEncoders()
        For r = 0 To encoders.Length - 1
            If encoders(r).MimeType = strMimeType Then
                Return encoders(r)
            End If
        Next

        Return Nothing

    End Function

End Module
