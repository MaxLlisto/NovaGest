Imports System.Drawing.KnownColor

Public Class ElementoEnvio
    Public orden As Integer = 0
    Public Em As String
    Public En As String
    Public Se As String
    Public Nu As String
    Public Fe As String
    Public Ev As String
    Public CP As String
    Public CI As String
    Public Ra As String
    Public Ba As String
    Public Ti As String
    Public Cu As String
    Public Si As String
    Public CV As String
    Public Er As String
    Public Co As String
    Public Situacion As Color
    Public ColorFondo As Color


    Public Sub New()
        orden = orden + 1
        Em = ""
        En = ""
        Se = ""
        Nu = ""
        Fe = ""
        Ev = ""
        CP = ""
        CI = ""
        Ra = ""
        Ba = ""
        Ti = ""
        Cu = ""
        Si = ""
        CV = ""
        Er = ""
        Co = ""
        Situacion = New Color().FromName(Green)
        ColorFondo = New Color().FromName(White)
    End Sub
    Public Sub New(a1 As String, a2 As String, a3 As String, a4 As String, a5 As String, a6 As String, a7 As String, a8 As String, a9 As String, a10 As String, a11 As String, a12 As String, a13 As String, a14 As String, a15 As String, a16 As Color, a17 As Color)
        orden = orden + 1
        Em = a1
        En = a2
        Se = a3
        Nu = a4
        Fe = a5
        Ev = a6
        CP = a7
        CI = a8
        Ra = a9
        Ba = a10
        Ti = a11
        Cu = a12
        Si = a13
        CV = a14
        Er = a15
        'Co = a16
        Situacion = a16
        ColorFondo = a17
    End Sub
    Public Sub New(a1 As String, a2 As String, a3 As String, sit As Color, fon As Color)
        Em = ""
        En = ""
        Se = ""
        Nu = ""
        Fe = ""
        Ev = ""
        CP = ""
        CI = ""
        Ra = ""
        Ba = a1
        Ti = a2
        Cu = a3
        Si = ""
        CV = ""
        Er = ""
        Co = ""
        Situacion = sit
        ColorFondo = fon
    End Sub
End Class
