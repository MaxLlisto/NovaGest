Public Class FrmCostesfruta
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    'Public miExcel As excel.Application
    'Public miDocExcel As excel.Workbook

    Private Sub CmdGrabar()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsP As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim cv_actual As String
        Dim Orden As Integer
        Dim Orden2 As Integer
        Dim codigo_puesto As String
        Dim Codigo_Operario() As Integer
        Dim Hora_entrada() As Date
        Dim Hora_salida() As Date
        Dim Total_horas() As Double
        Dim Total_coste() As Double
        Dim Hora_inicio() As Date
        Dim Hora_fin() As Date
        Dim Codigo_Variedad() As String
        Dim i As Integer, j As Integer
        Dim horas As Double
        Dim Coste As Double

        codigo_puesto = "523" ' Puesto genérico fruta

        SQL = "SELECT * FROM personal_es WHERE dia_inicial_red ='" & CStr(Fechavolcado.value) & "' AND puesto='" & codigo_puesto & "' AND hora_inicial_red<>hora_final_red ORDER BY hora_inicial_red"
        RsP.Open(SQL, ObjetoGlobal.cn)

        Orden = 0
        For i = 1 To RsP.RecordCount
            RsP.AbsolutePosition = i
            If lblProgreso.Text = "...................." Then
                lblProgreso.Text = ""
            Else
                lblProgreso.Text = lblProgreso.Text & "."
            End If

            Orden = Orden + 1
            ReDim Preserve Codigo_Operario(Orden)
            ReDim Preserve Hora_entrada(Orden)
            ReDim Preserve Hora_salida(Orden)
            ReDim Preserve Total_horas(Orden)
            ReDim Preserve Total_coste(Orden)
            Codigo_Operario(Orden) = RsP!cod_operario
            Hora_entrada(Orden) = CDate(RsP!Hora_inicial_red)
            Hora_salida(Orden) = CDate(RsP!Hora_final_red)
            Total_horas(Orden) = Math.Round(DateDiff("n", Hora_entrada(Orden), Hora_salida(Orden)) / 60, 4)
            Total_coste(Orden) = Math.Round(RsP!importe_coste, 2)

            If CDate(RsP!Hora_inicial_red) <= CDate("09:30:00") And CDate(RsP!Hora_final_red) <= CDate("09:30:00") Then
                'Nada
            ElseIf CDate(RsP!Hora_inicial_red) <= CDate("09:30:00") And CDate(RsP!Hora_final_red) >= CDate("09:30:00") And CDate(RsP!Hora_final_red) <= CDate("10:00:00") Then
                Hora_salida(Orden) = CDate("09:30:00")
                Total_horas(Orden) = Math.Round(DateDiff("n", Hora_entrada(Orden), Hora_salida(Orden)) / 60, 4)
            ElseIf CDate(RsP!Hora_inicial_red) <= CDate("09:30:00") And CDate(RsP!Hora_final_red) > CDate("10:00:00") Then
                Hora_salida(Orden) = CDate("09:30:00")
                Total_horas(Orden) = Math.Round(DateDiff("n", Hora_entrada(Orden), Hora_salida(Orden)) / 60, 4)
                Total_coste(Orden) = Math.Round((RsP!importe_coste / RsP!horas_ajustadas) * Total_horas(Orden), 2)

                Orden = Orden + 1
                ReDim Preserve Codigo_Operario(Orden)
                ReDim Preserve Hora_entrada(Orden)
                ReDim Preserve Hora_salida(Orden)
                ReDim Preserve Total_horas(Orden)
                ReDim Preserve Total_coste(Orden)
                Codigo_Operario(Orden) = RsP!cod_operario
                Hora_entrada(Orden) = CDate("10:00:00")
                Hora_salida(Orden) = CDate(RsP!Hora_final_red)
                Total_horas(Orden) = Math.Round(DateDiff("n", Hora_entrada(Orden), Hora_salida(Orden)) / 60, 4)
                Total_coste(Orden) = (RsP!importe_coste / RsP!horas_ajustadas) * Total_horas(Orden)
            ElseIf CDate(RsP!Hora_inicial_red) >= CDate("09:30:00") And CDate(RsP!Hora_final_red) <= CDate("10:00:00") Then
                Codigo_Operario(Orden) = -1
                MsgBox("Marcaje en la pausa de almuerzo", MsgBoxStyle.Information)
            ElseIf CDate(RsP!Hora_inicial_red) >= CDate("09:30:00") And CDate(RsP!Hora_inicial_red) <= CDate("10:00:00") And CDate(RsP!Hora_final_red) > CDate("10:00:00") Then
                Hora_entrada(Orden) = CDate("10:00:00")
                Total_horas(Orden) = Math.Round(DateDiff("n", Hora_entrada(Orden), Hora_salida(Orden)) / 60, 4)
            ElseIf CDate(RsP!Hora_inicial_red) >= CDate("10:00:00") Then
                'Nada
            Else
                MsgBox("No sé lo que pasa con este marcaje", MsgBoxStyle.Exclamation)
            End If
        Next
        RsP.Close()

        SQL = "SELECT b.*, e.codigo_variedad FROM entradas_bultos b LEFT JOIN entradas_albaranes e ON e.empresa = b.empresa AND e.ejercicio = b.ejercicio AND "
        SQL = SQL + "e.serie_albaran = b.serie_albaran AND e.numero_albaran = b.numero_albaran "
        SQL = SQL + "WHERE b.empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND "
        SQL = SQL + "b.ejercicio = dbo.fn_que_ejercicio('" & Fechavolcado.Text & "') AND b.linea_volcado = 'FRUTA' AND "
        SQL = SQL + "b.fecha_volcado BETWEEN '" & Fechavolcado.Text & " 00:00:00" & "' AND '" & Fechavolcado.Text & " 23:59:59" & "' "
        SQL = SQL + "ORDER BY b.fecha_volcado"

        Orden2 = 0
        cv_actual = "x"
        Rs.Open(SQL, ObjetoGlobal.cn)
        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            If Trim(cv_actual) = Trim(Rs!Codigo_Variedad) Then
                Hora_fin(Orden2) = CDate(Format(Rs!fecha_volcado, "hh:MM:ss"))
            Else
                Orden2 = Orden2 + 1
                ReDim Preserve Hora_inicio(Orden2)
                ReDim Preserve Hora_fin(Orden2)
                ReDim Preserve Codigo_Variedad(Orden2)

                cv_actual = Trim(Rs!Codigo_Variedad)
                Codigo_Variedad(Orden2) = Trim(Rs!Codigo_Variedad)

                If Orden2 = 1 Then
                    Hora_inicio(Orden2) = CDate("00:00:01")
                Else
                    Hora_inicio(Orden2) = CDate(Format(Rs!fecha_volcado, "hh:MM:ss"))
                    Hora_fin(Orden2 - 1) = DateAdd("s", -1, Hora_inicio(Orden2))
                End If
            End If
        Next
        If Orden2 > 0 Then Hora_fin(Orden2) = CDate("23:59:59")
        Rs.Close()

        Rs2.Open("SELECT * FROM costes_linea_fruta WHERE fecha = '" + Fechavolcado.Text + "'", ObjetoGlobal.cn, True)
        Do While Rs2.RecordCount > 0
            Rs2.Delete()
            Rs2.MoveFirst()
        Loop
        Rs2.Close()
        Rs2.Open("SELECT * FROM costes_linea_horas WHERE fecha = '" + Fechavolcado.value + "'", ObjetoGlobal.cn, True)
        While Rs2.RecordCount > 0
            Rs2.Delete()
            Rs2.MoveFirst()
        End While
        Rs2.Close()

        For i = 1 To Orden
            For j = 1 To Orden2
                If CDate(Hora_fin(j)) <= CDate(Hora_entrada(i)) Then
                    'Nada
                ElseIf CDate(Hora_inicio(j)) <= CDate(Hora_entrada(i)) And CDate(Hora_fin(j)) <= CDate(Hora_salida(i)) Then
                    horas = Math.Round(DateDiff("n", CDate(Hora_entrada(i)), CDate(Hora_fin(j))), 2) / 60
                    If Total_horas(i) = 0 Then
                        Coste = 0
                    Else
                        Coste = Math.Round(Total_coste(i) * horas / Total_horas(i), 2)
                    End If
                    GrabarCoste(CDate(Fechavolcado.value), Codigo_Variedad(j), Codigo_Operario(i), horas, Coste, CDate(Hora_entrada(i)), CDate(Hora_fin(j)))
                ElseIf CDate(Hora_inicio(j)) <= CDate(Hora_entrada(i)) And CDate(Hora_fin(j)) >= CDate(Hora_salida(i)) Then
                    horas = Math.Round(Total_horas(i), 2)
                    Coste = Math.Round(Total_coste(i), 2)
                    GrabarCoste(CDate(Fechavolcado.value), Codigo_Variedad(j), Codigo_Operario(i), horas, Coste, CDate(Hora_entrada(i)), CDate(Hora_salida(i)))
                ElseIf CDate(Hora_inicio(j)) >= CDate(Hora_entrada(i)) And CDate(Hora_fin(j)) <= CDate(Hora_salida(i)) Then
                    horas = Math.Round(DateDiff("n", CDate(Hora_inicio(j)), CDate(Hora_fin(j))), 2) / 60
                    If Total_horas(i) = 0 Then
                        Coste = 0
                    Else
                        Coste = Math.Round(Total_coste(i) * horas / Total_horas(i), 2)
                    End If
                    GrabarCoste(CDate(Fechavolcado.value), Codigo_Variedad(j), Codigo_Operario(i), horas, Coste, CDate(Hora_inicio(j)), CDate(Hora_fin(j)))
                ElseIf CDate(Hora_inicio(j)) >= CDate(Hora_entrada(i)) And CDate(Hora_inicio(j)) <= CDate(Hora_salida(i)) And CDate(Hora_fin(j)) > CDate(Hora_salida(i)) Then
                    horas = Math.Round(DateDiff("n", CDate(Hora_inicio(j)), CDate(Hora_salida(i))), 2) / 60
                    If Total_horas(i) = 0 Then
                        Coste = 0
                    Else
                        Coste = Math.Round(Total_coste(i) * horas / Total_horas(i), 2)
                    End If
                    GrabarCoste(CDate(Fechavolcado.value), Codigo_Variedad(j), Codigo_Operario(i), horas, Coste, CDate(Hora_inicio(j)), CDate(Hora_salida(i)))
                ElseIf CDate(Hora_salida(i)) <= CDate(Hora_inicio(j)) Then
                    'Nada
                Else
                    MsgBox("Marcaje con horas incorrectas", MsgBoxStyle.Critical)
                End If
            Next
        Next

        MsgBox("Proceso concluido correctamente", MsgBoxStyle.Information)

    End Sub

    Private Sub GrabarCoste(Fecha As Date, Variedad As String, operario As Integer, horas As Double, Coste As Double, Hora1 As Date, Hora2 As Date)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim Periodos(13, 1) As String
        Dim i As Integer
        Dim t1 As Double
        Dim T2 As Double
        Dim T3 As Double
        Dim Seg1 As Double
        Dim Seg2 As Double

        Periodos(1, 0) = "00:00:01" : Periodos(1, 1) = "08:00:00"
        Periodos(2, 0) = "08:00:00" : Periodos(2, 1) = "09:00:00"
        Periodos(3, 0) = "09:00:00" : Periodos(3, 1) = "10:00:00"
        Periodos(4, 0) = "10:00:00" : Periodos(4, 1) = "11:00:00"
        Periodos(5, 0) = "11:00:00" : Periodos(5, 1) = "12:00:00"
        Periodos(6, 0) = "12:00:00" : Periodos(6, 1) = "13:00:00"
        Periodos(7, 0) = "13:00:00" : Periodos(7, 1) = "15:00:00"
        Periodos(8, 0) = "15:00:00" : Periodos(8, 1) = "16:00:00"
        Periodos(9, 0) = "16:00:00" : Periodos(9, 1) = "17:00:00"
        Periodos(10, 0) = "17:00:00" : Periodos(10, 1) = "18:00:00"
        Periodos(11, 0) = "18:00:00" : Periodos(11, 1) = "19:00:00"
        Periodos(12, 0) = "19:00:00" : Periodos(12, 1) = "20:00:00"
        Periodos(13, 0) = "20:00:00" : Periodos(13, 1) = "23:59:59"

        t1 = 0
        Sql = "SELECT * FROM costes_linea_fruta WHERE fecha='" + Format(Fechavolcado.value, "dd/MM/yyyy") + "' AND variedad = '" + Trim(Variedad) + "' AND operario = " + CStr(operario)

        Rs.Open(Sql, ObjetoGlobal.cn, True)
        If Rs.RecordCount = 0 Then
            Rs.AddNew()
            Sql = "SELECT MAX(orden) as numorden FROM costes_linea_fruta"
            Rs2.Open(Sql, ObjetoGlobal.cn, True)
            If IsDBNull(Rs2!numorden) Then
                Rs!Orden = 1
            Else
                Rs!Orden = Rs2!numorden + 1
            End If
            Rs2.Close()
            Rs!Fecha = CDate(Fecha)
            Rs!Variedad = Trim(Variedad)
            Rs!operario = operario
            Rs!horas = 0
            Rs!Coste = 0
        End If
        Rs!horas = Math.Round(Rs!horas + horas, 2)
        Rs!Coste = Math.Round(Rs!Coste + Coste, 2)
        t1 = t1 + Coste
        Rs.Update()
        Rs.Close()

        T3 = 0
        Seg1 = DateDiff("s", Hora1, Hora2)
        For i = 1 To 13
            T2 = 0
            If CDate(Periodos(i, 1)) < CDate(Hora1) Then
                'Nada
            ElseIf CDate(Periodos(i, 0)) >= CDate(Hora2) Then
                'Nada
            ElseIf CDate(Periodos(i, 0)) <= Hora1 And CDate(Periodos(i, 1)) <= Hora2 Then
                Seg2 = DateDiff("s", Hora1, CDate(Periodos(i, 1)))
                If Seg1 = 0 Then
                    T2 = 0
                Else
                    T2 = Math.Round(Coste * Seg2 / Seg1, 2)
                End If
                'Hora1 a Periodos(i, 1)
            ElseIf CDate(Periodos(i, 0)) <= Hora1 And CDate(Periodos(i, 1)) > Hora2 Then
                'Hora1 a Hora2
                T2 = Coste
            ElseIf CDate(Periodos(i, 0)) >= Hora1 And CDate(Periodos(i, 1)) <= Hora2 Then
                'Periodos(i,0) a Periodos(i,1)
                Seg2 = DateDiff("s", CDate(Periodos(i, 0)), CDate(Periodos(i, 1)))
                If Seg1 = 0 Then
                    T2 = 0
                Else
                    T2 = Math.Round(Coste * Seg2 / Seg1, 2)
                End If
            ElseIf CDate(Periodos(i, 0)) >= Hora1 And CDate(Periodos(i, 1)) > Hora2 Then
                'Periodos(i,0) a Hora2
                Seg2 = DateDiff("s", CDate(Periodos(i, 0)), Hora2)
                If Seg1 = 0 Then
                    T2 = 0
                Else
                    T2 = Math.Round(Coste * Seg2 / Seg1, 2)
                End If
            ElseIf CDate(Periodos(i, 0)) >= Hora2 Then
                'Nada
            Else
                MsgBox("No sé qué hago aquí", MsgBoxStyle.Question)
            End If

            If T2 <> 0 Then
                Sql = "SELECT * FROM costes_linea_horas WHERE fecha='" + Format(Fechavolcado.value, "dd/MM/yyyy") + "' AND hora_inicio = '" + Format(Periodos(i, 0), "hh:nn:ss") + "' AND variedad = '" + Trim(Variedad) + "' AND operario = " + CStr(operario)
                Rs.Open(Sql, ObjetoGlobal.cn, True)
                If Rs.RecordCount = 0 Then
                    Rs.AddNew()
                    Sql = "SELECT MAX(orden) as numorden FROM costes_linea_horas"
                    Rs2.Open(Sql, ObjetoGlobal.cn, True)
                    If IsDBNull(Rs2!numorden) Then
                        Rs!Orden = 1
                    Else
                        Rs!Orden = Rs2!numorden + 1
                    End If
                    Rs2.Close()
                    Rs!Fecha = CDate(Fecha)
                    Rs!Hora_inicio = Format(CDate(Periodos(i, 0)), "hh:mm:ss")
                    Rs!Hora_fin = Format(CDate(Periodos(i, 1)), "hh:mm:ss")
                    Rs!Variedad = Trim(Variedad)
                    Rs!operario = operario
                    Rs!Coste = 0
                End If
                Rs!Coste = Math.Round(Rs!Coste + T2, 2)
                Rs.Update()
                Rs.Close()
                T3 = T3 + T2
            End If
        Next

    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        pb.Visible = True
        CmdGrabar()
        pb.Visible = False
    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Me.Close()
    End Sub
    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

End Class