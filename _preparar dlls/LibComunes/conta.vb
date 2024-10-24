Public Module conta
    Public Function DigitosCuentas(dato As String, Optional Nivel As Integer = 0, Optional caracter As String = "0") As String
        Dim n As Integer
        Dim ret As String = ""

        If Nivel = 0 Then
            n = ObjetoGlobal.NivelDeCuenta
        Else
            n = Nivel
        End If

        If InStr(1, dato, ".") <> 0 Then
            If Not dato = "." Then
                ret = padc(dato, n, ".", caracter)
            End If
        Else
            If Trim(dato) <> "" Then
                ret = padr(dato, n, caracter)
            End If
        End If
        Return ret

    End Function

    Sub CalculoNivelDeCuenta()
        Dim RsNivelCuenta As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer

        If RsNivelCuenta.Open("SELECT * FROM NIVELES_CONTABLES WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual & "'", ObjetoGlobal.cn) Then
            If RsNivelCuenta.RecordCount > 0 Then
                For i = 13 To 1 Step -1
                    If UCase(RsNivelCuenta("nivel_" & i)) = "S" Then
                        ObjetoGlobal.NivelDeCuenta = i
                        Exit For
                    End If
                Next
                ObjetoGlobal.NiveldeSaldos = RsNivelCuenta!nivel_saldos
                ObjetoGlobal.NivelesDisponibles = New List(Of Integer)

                ObjetoGlobal.NivelesDisponibles.Clear()

                ObjetoGlobal.NivelesDisponibles.Add(False)
                For i = 1 To 13
                    ObjetoGlobal.NivelesDisponibles.Add(RsNivelCuenta("nivel_" & i) = "S")
                    If RsNivelCuenta("nivel_" & i) = "O" Then
                        ObjetoGlobal.NivelesDisponibles.Remove(ObjetoGlobal.NivelesDisponibles.Count)
                        ObjetoGlobal.NivelesDisponibles.Add(True)
                    End If
                Next
                ObjetoGlobal.CadenaNivelCuenta = ""
                For i = 1 To ObjetoGlobal.NivelDeCuenta
                    ObjetoGlobal.CadenaNivelCuenta = ObjetoGlobal.CadenaNivelCuenta & "0"
                Next
            End If
        End If
        RsNivelCuenta.Close()

    End Sub

    Public Function DameFecha(Optional d As String = "") As Date
        Dim dd As Integer
        Dim mm As Integer
        Dim aa As Integer
        Dim fecha As Date = Now

        If Not IsNumeric(d) Then
            If InStr(d, "/") <> 0 Then
                d = aCadena(d, "/", 2)
            ElseIf InStr(d, "-") <> 0 Then
                d = aCadena(d, "-", 2)
            ElseIf InStr(d, ".") <> 0 Then
                d = aCadena(d, ".", 2)
            ElseIf InStr(Trim(d), " ") <> 0 Then
                d = aCadena(d, "-", 2)
            Else
                Return ""
            End If
        End If

        'dd = Day(Now)
        mm = Month(Now)
        aa = Year(Now)

        d = Trim(d)
        Select Case Len(d)
            Case 1
                dd = padl(d, 2, "0")
                Return Format(dd & "/" & padl(mm, 2, "0") & "/" & aa, vbGeneralDate)

            Case 2
                If d <= 31 And Inlist(mm, 1, 3, 5, 7, 8, 10, 12) Then
                    Return Format(dd & "/" & padl(mm, 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf d <= 30 And Inlist(mm, 4, 6, 7, 11) Then
                    dd = d
                    Return Format(dd & "/" & padl(mm, 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf d <= 29 And mm = 2 And (((aa Mod 4) = 0 And (aa Mod 100) <> 0) Or ((aa Mod 4) = 0 And (aa Mod 400) = 0)) Then
                    dd = d
                    Return Format(dd & "/" & padl(mm, 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf d <= 28 And mm = 2 Then
                    dd = d
                    Return Format(dd & "/" & padl(mm, 2, "0") & "/" & aa, vbGeneralDate)

                Else
                    dd = d
                    Return Format(padl(Left(d, 1), 2, "0") & "/" & padl(Right(d, 1), 2, "0") & "/" & aa, vbGeneralDate)

                End If
            Case 3
                If (Left(d, 2) = "31" And InStr(Right(d, 1), "13578") > 0) Then
                    Return Format(padl(Left(d, 2), 2, "0") & "/" & padl(Right(d, 1), 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf (Left(d, 2) = "30" And InStr(Right(d, 1), "469") > 0) Then
                    Return Format(padl(Left(d, 2), 2, "0") & "/" & padl(Right(d, 1), 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf Left(d, 2) = "29" And Right(d, 1) = 2 And (((aa Mod 4) = 0 And (aa Mod 100) <> 0) Or ((aa Mod 400) = 0)) Then
                    Return Format(padl(Left(d, 2), 2, "0") & "/" & padl(Right(d, 1), 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf Left(d, 2) = "28" And Right(d, 1) = 2 Then
                    Return Format(padl(Left(d, 2), 2, "0") & "/" & padl(Right(d, 1), 2, "0") & "/" & aa, vbGeneralDate)

                ElseIf Left(d, 2) > "31" Then
                    If CInt(Right(d, 2)) <= 12 Then
                        Return Format(padl(Left(d, 1), 2, "0") & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                    Else
                        Return ""
                    End If
                ElseIf Left(d, 2) >= "30" And InStr(Right(d, 1), "2469") > 0 Then
                    If CInt(Right(d, 2)) <= 12 Then
                        Return Format(padl(Left(d, 1), 2, "0") & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                    Else
                        Return ""
                    End If
                ElseIf Left(d, 2) > "28" And Right(d, 1) = 2 And Not (((aa Mod 4) = 0 And (aa Mod 100) <> 0) Or ((aa Mod 400) = 0)) Then
                    If CInt(Right(d, 2)) <= 12 Then
                        Return Format(padl(Left(d, 1), 2, "0") & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                    Else
                        Return ""
                    End If
                Else
                    Return Format(Left(d, 2) & "/" & padl(Right(d, 1), 2, "0") & "/" & aa, vbGeneralDate)
                End If
            Case 4
                If Left(d, 2) <= "31" And Inlist(Right(d, 2), "01", "03", "05", "07", "08", "10", "12") Then
                    Return Format(Left(d, 2) & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                ElseIf Left(d, 2) <= "30" And Inlist(Right(d, 2), "04", "06", "09", "11") Then
                    Return Format(Left(d, 2) & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                ElseIf Left(d, 2) <= "28" And Right(d, 2) = "02" Then
                    Return Format(Left(d, 2) & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                ElseIf Left(d, 2) = "29" And Right(d, 2) = "02" And (((aa Mod 4) = 0 And (aa Mod 100) <> 0) Or ((aa Mod 400) = 0)) Then
                    Return Format(Left(d, 2) & "/" & Right(d, 2) & "/" & aa, vbGeneralDate)
                End If
            Case 6
                If Left(d, 2) <= "31" And Inlist(Mid(dd, 3, 2), "01", "03", "05", "07", "08", "10", "12") Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & "20" & Right(d, 2), vbGeneralDate)
                ElseIf Left(d, 2) <= "30" And Inlist(Mid(dd, 3, 2), "04", "04", "09", "11") Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & "20" & Right(d, 2), vbGeneralDate)
                ElseIf Left(d, 2) <= "29" And Mid(dd, 3, 2) = "02" And (((aa Mod 4) = 0 And (aa Mod 100) <> 0) Or ((aa Mod 400) = 0)) Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & "20" & Right(d, 2), vbGeneralDate)
                ElseIf Left(d, 2) <= "28" And Mid(dd, 3, 2) = "02" Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & "20" & Right(d, 2), vbGeneralDate)
                Else
                    Return ""
                End If
            Case 8
                If Left(d, 2) <= "31" And Inlist(Mid(dd, 3, 2), "01", "03", "05", "07", "08", "10", "12") And Inlist(Mid(dd, 5, 2), "19", "20") Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & Right(d, 4), vbGeneralDate)
                ElseIf Left(d, 2) <= "30" And Inlist(Mid(dd, 3, 2), "04", "04", "09", "11") And Inlist(Mid(dd, 5, 2), "19", "20") Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & Right(d, 4), vbGeneralDate)
                ElseIf Left(d, 2) <= "29" And Mid(dd, 3, 2) = "02" And (((aa Mod 4) = 0 And (aa Mod 100) <> 0) Or ((aa Mod 400) = 0)) And Inlist(Mid(dd, 5, 2), "19", "20") Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & Right(d, 4), vbGeneralDate)
                ElseIf Left(d, 2) <= "28" And Mid(dd, 3, 2) = "02" And Inlist(Mid(dd, 5, 2), "19", "20") Then
                    Return Format(Left(d, 2) & "/" & Mid(dd, 3, 2) & "/" & Right(d, 4), vbGeneralDate)
                Else
                    Return ""
                End If
            Case Else
                Return ""
        End Select

        Return ""

    End Function



End Module
