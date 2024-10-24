Imports System.Drawing.Printing
Public Class FrmErroresMarcajes
    Public Resultado As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public FechaInicial As Date
    Public HoraInicial As String
    Public Fechafinal As Date
    Public HoraFinal As String
    Dim WithEvents PD As New PrintDocument()
    Dim WithEvents PPD As New PrintPreviewDialog
    Public mystring As String = ""

    Public Sub DocPrint(ByVal mystring As String)

        PPD.Document = PD

        PPD.SetDesktopBounds(0, 0, My.Computer.Screen.WorkingArea.Width, My.Computer.Screen.WorkingArea.Height)
        PPD.ShowDialog()
        PPD.Close()
    End Sub

    Private Sub PD_PrintPages(ByVal Sender As Object, ByVal e As PrintPageEventArgs)

        e.Graphics.DrawString(mystring, New Font("Arial", 12, FontStyle.Regular), Brushes.Black, 100, 100)

    End Sub

    Public Sub Mensaje(cMsg)
        TxtMsg.Text = cMsg
    End Sub

    Private Sub Procesar_Click(sender As Object, e As EventArgs) Handles Procesar.Click
        Resultado = "P"
        Close()
    End Sub

    Private Sub Cancelar_Click(sender As Object, e As EventArgs) Handles Cancelar.Click
        Resultado = "C"
        Close()
    End Sub

    Private Sub AExcel_Click(sender As Object, e As EventArgs) Handles AExcel.Click
        mystring = TxtMsg.Text
        DocPrint(mystring)
    End Sub
End Class