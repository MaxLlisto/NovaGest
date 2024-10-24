Public Class verificarSEPA

    Public Sub New()
    End Sub

    Public Function CheckIban(ByVal cIban As String)
        Dim cMod As String
        Dim cValor As String
        Dim IbanIban As String
        Dim cNum As String

        IbanIban = cIban
        cIban = Replace(cIban, " ", "")
        cIban = Replace(cIban, "-", "")
        cIban = Replace(cIban, "/", "")
        cIban = Replace(cIban, "\", "")
        cIban = Replace(cIban, ":", "")
        cIban = Replace(cIban, ",", "")
        cIban = Replace(cIban, ".", "")
        cIban = Replace(cIban, "*", "")

        cIban = cIban.Trim
        If cIban.Length < 24 Then
            Return False
        End If

        cNum = PasaraNumero(Left(cIban, 2))
        cIban = Mid(cIban, 5) + cNum + "00"
        cValor = cIban

        cMod = libcomunes.cadenas.padl(98 - CalculaModulo(cValor, 97), 2, "0")

        If Left(cIban, 2) + cMod + Mid(cIban, 5) <> IbanIban Then
            Return False
        End If

        Return True
    End Function

    Public Function PasaraNumero(ByVal cArg As String) As String
        Dim cValor As String
        Dim aLetra As String()
        Dim aNumero As String()
        Dim i As Integer

        aLetra = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        aNumero = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35"}

        cValor = Replace(cArg, " ", "")
        cValor = Replace(cValor, "-", "")
        cValor = Replace(cValor, "/", "")
        cValor = Replace(cValor, "_", "")
        cValor = Replace(cValor, "\", "")
        cValor = Replace(cValor, ":", "")
        cValor = cValor.Trim

        For i = 0 To aLetra.Length - 1
            cValor = Replace(cValor, aLetra(i), aNumero(i))
        Next
        cValor = cValor.Trim
        Return cValor

    End Function


    Function CalculaModulo(ByVal cNumero As String, ByVal nDivisor As Double) As Double
        Dim nLonDiv As Integer
        Dim nLonNum As Integer
        Dim nRet As Integer
        Dim aNumeros As String()
        Dim i As Integer
        Dim nDiv As Double
        Dim lNoSalir As Boolean

        nLonNum = cNumero.TrimEnd.TrimStart.Length
        cNumero = cNumero.TrimEnd.TrimStart
        nLonDiv = nDivisor.ToString.TrimEnd.TrimStart.Length
        ReDim aNumeros(nLonNum)

        For i = 1 To nLonNum
            aNumeros(i) = CInt(Val(Mid(cNumero, i, 1)))
        Next

        If cNumero.TrimEnd.TrimStart.Length < nLonDiv Then
            nRet = Val(cNumero.TrimEnd.TrimStart)
        Else
            nDiv = Math.Round(Val(Mid(cNumero, 1, nLonDiv)), 0)
            i = nLonDiv + 1
            lNoSalir = True
            While lNoSalir
                If nDiv < nDivisor Then
                    If i > nLonNum Then
                        nRet = nDiv
                        lNoSalir = False
                    Else
                        nDiv = (nDiv * 10) + aNumeros(i)
                        i = i + 1
                    End If
                Else
                    nDiv = Math.Round((nDiv Mod nDivisor), 0)
                End If
            End While
        End If

        Return nRet
    End Function

End Class
