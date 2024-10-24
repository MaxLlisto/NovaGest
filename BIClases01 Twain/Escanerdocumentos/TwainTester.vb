Imports System.Math
Imports TwainScanner.TwainLib

Public Class TwainTester
  Implements IMessageFilter
  Private msgfilter As Boolean
  Private tw As Twain
  Private picnumber As Integer = 0

  Private Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
    Dim cmd As TwainCommand = tw.PassMessage(m)
    If cmd = TwainCommand.[Not] Then
      Return False
    End If

    Select Case cmd
      Case TwainCommand.CloseRequest
        If True Then
          EndingScan()
          tw.CloseSrc()
          Exit Select
        End If
      Case TwainCommand.CloseOk
        If True Then
          EndingScan()
          tw.CloseSrc()
          Exit Select
        End If
      Case TwainCommand.DeviceEvent
        If True Then
          Exit Select
        End If
      Case TwainCommand.Failure
        EndingScan()
        tw.CloseSrc()
        Return False
      Case TwainCommand.TransferReady
        If True Then
          Dim pics As ArrayList = tw.TransferPictures()
          EndingScan()
          tw.CloseSrc()
          picnumber += 1
          For i As Integer = 0 To pics.Count - 1
            Dim img As IntPtr = DirectCast(pics(i), IntPtr)
            Dim newpic As New scaned(img)
            newpic.MdiParent = Me
            Dim picnum As Integer = i + 1
            'PictureBox2.Image = newpic.BackgroundImage
            newpic.Text = ("ScanPass" & picnumber.ToString() & "_Pic") + picnum.ToString()
            newpic.Show()
          Next
          Exit Select
        End If
    End Select

    Return True
  End Function

  Private Sub EndingScan()
    If msgfilter Then
      Application.RemoveMessageFilter(Me)
      msgfilter = False
      Me.Enabled = True
      Me.Activate()
    End If
  End Sub

  <STAThread()> _
  Private Shared Sub Main()
    If Twain.ScreenBitDepth < 15 Then
      MessageBox.Show("Need high/true-color video mode!", "Screen Bit Depth", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Exit Sub
    End If
  End Sub

  Private Sub tester_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    tw = New Twain()
    tw.Init(Me.Handle)
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    tw.[Select]()
  End Sub

  Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    If Not msgfilter Then
      Me.Enabled = False
      msgfilter = True
      Application.AddMessageFilter(Me)
    End If
    tw.Acquire()
  End Sub

End Class