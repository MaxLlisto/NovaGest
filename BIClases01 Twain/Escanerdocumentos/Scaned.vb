Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Imports TwainScanner.GdiPlusLib
Imports TwainScanner.TwainLib

Public Class Scaned
  Inherits System.Windows.Forms.Form

  <DllImport("gdi32.dll", ExactSpelling:=True)> Friend Shared Function SetDIBitsToDevice(ByVal hdc As IntPtr, ByVal xdst As Integer, ByVal ydst As Integer, ByVal width As Integer, ByVal height As Integer, ByVal xsrc As Integer, ByVal ysrc As Integer, ByVal start As Integer, ByVal lines As Integer, ByVal bitsptr As IntPtr, ByVal bmiptr As IntPtr, ByVal color As Integer) As Integer
  End Function
  <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalLock(ByVal handle As IntPtr) As IntPtr
  End Function
  <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalFree(ByVal handle As IntPtr) As IntPtr
  End Function
  <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> Public Shared Sub OutputDebugString(ByVal outstr As String)
  End Sub

  Dim bmi As BITMAPINFOHEADER
  Dim bmprect As Rectangle
  Dim dibhand As IntPtr
  Dim bmpptr As IntPtr
  Dim pixptr As IntPtr

  Public Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

  Public Sub New(ByVal dibhandp As IntPtr)
    InitializeComponent()

    SetStyle(ControlStyles.DoubleBuffer, False)
    SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    SetStyle(ControlStyles.Opaque, True)
    SetStyle(ControlStyles.ResizeRedraw, True)
    SetStyle(ControlStyles.UserPaint, True)

    bmprect = New Rectangle(0, 0, 0, 0)
    dibhand = dibhandp
    bmpptr = GlobalLock(dibhand)
    pixptr = GetPixelInfo(bmpptr)

    Me.AutoScrollMinSize = New System.Drawing.Size(bmprect.Width, bmprect.Height)
  End Sub

  Protected Function GetPixelInfo(ByVal bmpptr As IntPtr) As IntPtr
    bmi = New BITMAPINFOHEADER()
    Marshal.PtrToStructure(bmpptr, bmi)

    bmprect.X = 0
    bmprect.Y = 0
    bmprect.Width = bmi.biWidth
    bmprect.Height = bmi.biHeight

    If (bmi.biSizeImage = 0) Then
      bmi.biSizeImage = CInt((((bmi.biWidth * bmi.biBitCount) + 31) And CInt(Hex(Not (31)))) / 2 ^ 3) * bmi.biHeight
    End If

    Dim p As Integer = bmi.biClrUsed
    If ((p = 0) And (bmi.biBitCount <= 8)) Then
      p = CInt(1 * 2 ^ bmi.biBitCount)
    End If
    p = (p * 4) + bmi.biSize + CType(bmpptr.ToInt32, Integer)
    Return New IntPtr(p)
  End Function


End Class