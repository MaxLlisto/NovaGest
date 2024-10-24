Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MyTreeView
    Inherits TreeView

    Private mImage As Image

    Public Property Image As Image
        Get
            Return mImage
        End Get
        Set(ByVal value As Image)
            mImage = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnAfterCollapse(ByVal e As TreeViewEventArgs)
        If mImage IsNot Nothing Then Invalidate()
        MyBase.OnAfterCollapse(e)
    End Sub

    Protected Overrides Sub OnAfterExpand(ByVal e As TreeViewEventArgs)
        If mImage IsNot Nothing Then Invalidate()
        MyBase.OnAfterExpand(e)
    End Sub

    'Protected Overrides Sub NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)
    '    MyBase.NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)
    '    MessageBox.Show("Mouse Doble Click.")
    'End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = &H14 AndAlso mImage IsNot Nothing Then
            Dim pt As Point
            Using gr = Graphics.FromHdc(m.WParam)
                pt.X = Me.Location.X + ((Me.Width - mImage.Width) / 2)
                pt.Y = Me.Location.Y + ((Me.Height - mImage.Height) / 4)
                gr.DrawImage(mImage, pt)
            End Using
        End If

    End Sub
End Class
