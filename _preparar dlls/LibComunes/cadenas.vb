Public Module cadenas
    Public Function padl(x As String, y As Integer, Optional z As Char = " ") As String
        Dim ret As String = ""

        x = Trim(x)
        For i As Integer = 1 To y - Len(x)
            ret += z
        Next
        ret += x
        Return ret

    End Function

    Public Function padr(x As String, y As Integer, Optional z As Char = " ") As String
        Dim ret As String = ""

        ret = Trim(x)
        For i As Integer = 1 To y - Len(x)
            ret += z
        Next
        Return ret
    End Function

    Public Function padc(x As String, y As Integer, c As Char, Optional z As Char = " ") As String
        Dim ret As String = ""
        Dim pos As Integer
        Dim cini As String
        Dim cfin As String
        Dim nRelleno As Integer

        x = Trim(x)
        pos = InStr(x, c)
        If pos > 1 Then
            cini = Left(x, pos - 1)
        Else
            cini = ""
        End If
        If pos < Len(x) Then
            cfin = Mid(x, pos + 1)
        Else
            cfin = ""
        End If

        nRelleno = y - Len(cini) - Len(cfin)

        ret = Trim(cini)
        For i As Integer = 1 To nRelleno
            ret += z
        Next
        ret += cfin

        Return ret
    End Function

    Public Function Inlist(x As VariantType, ByVal ParamArray args() As VariantType) As Boolean

        If args.Length <= 0 Then
            Return False
        End If
        For i As Integer = 0 To UBound(args, 1)
            If x = args(i) Then
                Return True
            End If
        Next
        Return False

    End Function

    Public Function aCadena(ByVal a As String, ByVal b As String, Optional ByVal c As Integer = 0) As String
        Dim lista() As String
        Dim cad As String = ""

        a = Trim(a)
        lista = Split(a, b)
        For i As Integer = 0 To UBound(lista) - 1
            If c = 0 Then
                cad += Trim(lista(i))
            Else
                cad += padl(Trim(lista(i)), c, "0")
            End If
        Next

        Return cad

    End Function
End Module
