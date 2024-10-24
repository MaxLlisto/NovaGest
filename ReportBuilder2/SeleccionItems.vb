Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing.Design

Public Class SelectedItems
    Public Seleccionados As New List(Of Item)
    Public Rec As Rectangle
    Public g As Graphics
    Public pn As Pen
    Public visible As Boolean = False
    Public RulerThickness As Integer = 20
    Public SepHeight As Integer = 18
    Public rp As Report

    Public Sub add(it As Item)
        Seleccionados.add(it)
    End Sub

    Public Sub New()
        visible = False
    End Sub

    Public Sub New(ByVal R As Rectangle)
        Rec = R
    End Sub

    Public Sub Limpiar()
        For Each itm As Item In Seleccionados
            itm.selected = False
        Next
        Seleccionados.Clear()
    End Sub

    Public Function rectangulo()
        Return Rec
    End Function

    Public Sub Draw()
        g.DrawRectangle(pn, Rec)
    End Sub

    Public Sub ordenar(ByVal a As String)
        If UCase(a) = "X" Then
            Seleccionados.Sort(Function(it1 As Item, it2 As Item) it1.X.CompareTo(it2.X))
        Else
            Seleccionados.Sort(Function(it1 As Item, it2 As Item) it1.Y.CompareTo(it2.Y))
        End If
    End Sub


    Public Sub incluirItems()
        Dim secn As Section
        Dim sel_secn As Integer = -1
        Dim sel_item As Integer = -1
        Dim R As Rectangle
        Dim y As Integer = 0
        Dim sel As Boolean = False

        For i As Integer = 0 To RP.Sections.Count - 1
            secn = RP.Sections(i)

            'If secn.selected Then
            Dim addh As Integer = 0
                For k As Integer = 0 To secn.AR.Count - 1
                    Dim t As Item
                    t = secn.AR(k)
                    If sel_secn = i Then
                        If sel_item = k Then
                            addh = SepHeight + 1
                        End If
                    End If
                Next

            If secn.state <> 2 And secn.selected Then
                Dim x As Integer = 0

                If secn.state = 1 Then
                    R = New Rectangle(0, y + SepHeight, rp.w, secn.h + addh)
                Else
                    R = New Rectangle(0, y, rp.w, secn.h + addh)
                End If

                Dim inum As Integer = secn.AR.Count - 1
                For k As Integer = 0 To inum
                    'If sel_secn = i And sel_item = (inum - k) Then
                    Dim t As Item = secn.AR(inum - k)
                    If Rec.Contains(t.Rec.X, t.Rec.Y + y) Or
                           Rec.Contains(t.Rec.X + t.Rec.Width, t.Rec.Y + y) Or
                           Rec.Contains(t.Rec.X + t.Rec.Width, t.Rec.Y + t.Rec.Height + y) Or
                           Rec.Contains(t.Rec.X, t.Rec.Y + t.Rec.Height + y) Then
                        t.selected = True
                        add(t)
                    End If
                    'End If
                Next
            End If
            If secn.state < 3 Then
                    y += SepHeight
                End If
                If secn.state <> 2 Then
                    y += secn.h
                End If
            'End If
        Next
    End Sub

End Class
