Imports System
Imports System.Windows.Forms

Public Class SalidasCooperativa
    Private Resultador As Integer = 0

    Private Sub SalidasCooperativa_Load(sender As Object, e As EventArgs) Handles Me.Load
        horasalida.Value = Now
    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Resultador = 1
        Me.Close()
    End Sub

    Private Sub Candelar_Click(sender As Object, e As EventArgs) Handles Candelar.Click
        Me.Close()
    End Sub

    Friend ReadOnly Property Resultado() As Boolean
        Get
            Return Resultador
        End Get
    End Property

    Friend ReadOnly Property Hora() As String
        Get
            Return horasalida.Value.ToString("HH:mm")
        End Get

    End Property

End Class