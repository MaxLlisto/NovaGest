Imports System.ComponentModel

Public Class FrmNEntradasNuevo18
    Public ModificaPorcentaje As Boolean
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Dim SumaNC As Double
    Dim ValorNC As Double
    Dim ValorCom As Double
    Dim Porc_Recoleccion As Double
    Public oform As FrmEntradaAlbaranes
    Dim EsNuevoRegistro As Boolean
    Dim inicial_recol_p As Double
    Dim inicial_recol_i As Double
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir()
    End Sub

    Private Sub CmdSalir()
        If FlagPreguntar = True Then
            If ModificaPorcentaje = False Then
                If MsgBox("¿Desea salir SIN modificar el porcentaje?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            Else
                If MsgBox("El cambio de porcentaje no se realizará a no ser que guarde previamente los cambios ¿continuar?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.Close()
                End If
            End If
        Else
            Me.Close()
        End If
    End Sub
    Private Sub CmdGrabar_Click()
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If
    End Sub

    Private Function GrabarEntrada() As Boolean
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim rsForm As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NuevoPorcNC As Double
        Dim DifValor As Double
        Dim Porcentajes(12) As Double
        Dim Muestras(12) As Double
        Dim KilosCalidad(12) As Double
        Dim i As Integer
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Kilos As Double
        Dim Calidad As Integer
        Dim TotalComercial As Double
        Dim MaxOrd As Integer
        Dim Max As Double
        Dim DifPor As Double
        Dim TotalMuestras As Double
        Dim TotalKilos As Long
        Dim trans As SqlClient.SqlTransaction
        Dim Campos() As String, Valores() As String

        For i = 1 To 10
            Porcentajes(i) = 0
            Muestras(i) = 0
            KilosCalidad(i) = 0
        Next


        GrabarEntrada = False


        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()

        Try
            'If ModoConexion = 1 Then CnLocal.BeginTrans

            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                trans.Rollback()
                MsgBox("Error en el albarán a modificar")
                Return False
            End If

            RsEntradas!porcentaje_plaga = CDbl("0" & TxtPorcentaje_Plaga.Text)
            RsEntradas!porcentaje_recol = CDbl("0" & TxtPorcentaje_recol.Text)
            RsEntradas!porcentaje_recol_i = CDbl("0" & TxtPorcentaje_recol_i.Text)
            RsEntradas!porcentaje_grand = CDbl("0" & txtPorcentaje_grand.Text)
            RsEntradas!porcentaje_peq = CDbl("0" & txtPorcentaje_peq.Text)
            RsEntradas.Update()

            NuevoPorcNC = CDbl("0" & TxtPorcentaje_Plaga.Text) + CDbl("0" & TxtPorcentaje_recol.Text) + CDbl("0" & txtPorcentaje_grand.Text) + CDbl("0" & txtPorcentaje_peq.Text)
            DifValor = ValorNC - NuevoPorcNC

            ' Si el porcentaje dela No Comercial varía y Porc_Recoleccion es mayor que 0
            If (DifValor <> 0) And Porc_Recoleccion > 0 Then
                ' Obtenemos la clasificación actual
                RsClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "'  AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn, True,,,,,, trans)
                TotalMuestras = 0
                TotalComercial = 0
                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Calidad = RsClasificacion!codigo_calidad ' - 1
                    Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
                    Porcentajes(Calidad) = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                    Muestras(Calidad) = RsClasificacion!kg_muestra
                    TotalMuestras = TotalMuestras + RsClasificacion!kg_muestra
                    KilosCalidad(Calidad) = RsClasificacion!kg_calidad
                    If Calidad <> 6 Then
                        TotalComercial = TotalComercial + Porcentajes(Calidad)
                    End If
                Next
                DifPor = 100
                Max = 0
                For i = 1 To 10
                    If i = 6 Then
                        Porcentajes(i) = NuevoPorcNC
                    Else
                        Porcentajes(i) = Porcentajes(i) + (DifValor / TotalComercial * Porcentajes(i))
                        If Porcentajes(i) > Max Then
                            Max = Porcentajes(i)
                            MaxOrd = i
                        End If
                    End If
                    DifPor = DifPor - Porcentajes(i)
                Next
                'Si no suma 100 añadimos la diferencia al de mayor cantidad
                If DifPor <> 0 Then
                    Porcentajes(MaxOrd) = Porcentajes(i) + DifPor
                End If

                RsClasificacion.MoveFirst()
                TotalKilos = RsEntradas!kg_entrada
                MaxOrd = 0
                Max = 0
                ' Calculamos los arrays con los nuevos valores
                For i = 1 To 10
                    Muestras(i) = Math.Round(TotalMuestras * Porcentajes(i) / 100, 2)
                    KilosCalidad(i) = Math.Round(RsEntradas!kg_entrada * Porcentajes(i) / 100, 0)
                    If KilosCalidad(i) > Max Then
                        Max = KilosCalidad(i)
                        MaxOrd = i
                    End If
                    'Sumamos el total de kilos calculados
                    TotalKilos = TotalKilos - KilosCalidad(i)
                Next
                If TotalKilos <> 0 Then
                    ' Si los kilos calculados no suman los kg. entrada sumamos la diferencia al de mayor peso
                    KilosCalidad(MaxOrd) = KilosCalidad(MaxOrd) + TotalKilos
                End If
                ' Grabamos los porcentajes a las tablas
                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Calidad = RsClasificacion!codigo_calidad ' - 1
                    RsClasificacion!kg_muestra = Muestras(Calidad)
                    RsClasificacion!kg_calidad = KilosCalidad(Calidad)
                    RsClasificacion.Update()
                Next
                RsClasificacion.Close()
            End If

            'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "U"

            trans.Commit()
            ObjetoGlobal.cn.Close()

            'If ModoConexion = 1 Then CnLocal.CommitTrans


            If Trim(oform.CnTabla01.ValorCampo("serie_albaran")) = Trim(SerieSeleccionada) And oform.CnTabla01.ValorCampo("numero_albaran") = AlbaranSeleccionado Then
                ReDim Campos(3), Valores(3)
                Campos(0) = "porcentaje_plaga" : Valores(0) = TxtPorcentaje_Plaga.Text
                Campos(1) = "porcentaje_recol" : Valores(1) = TxtPorcentaje_recol.Text
                Campos(2) = "porcentaje_grand" : Valores(2) = txtPorcentaje_grand.Text
                Campos(3) = "porcentaje_peq" : Valores(3) = txtPorcentaje_peq.Text
                oform.AsignarValores(oform.CnTabla01, Campos, Valores)
                MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
            End If

            RsEntradas.Close()
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("Error en el albarán a modificar")
            Return False
        End Try

    End Function

    Private Function Comprobacion() As Boolean
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nPorcentajeNoComercial As Double
        Dim porcentaje As Double
        Dim Calidad As Integer
        Dim Kilos As Long
        Dim SQL As String
        Dim i As Integer
        Dim TotalPorcNC As Double

        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount = 0 Then
            MsgBox("Error en el albarán a modificar")
            Return False
        End If

        RsClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "'  AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
        For i = 1 To RsClasificacion.RecordCount
            RsClasificacion.AbsolutePosition = i
            Calidad = RsClasificacion!codigo_calidad ' - 1
            Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
            If Calidad = 6 Then
                porcentaje = 0
                If RsEntradas!kg_entrada <> 0 Then
                    porcentaje = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                End If
                nPorcentajeNoComercial = porcentaje
            End If
        Next
        RsClasificacion.Close()
        RsEntradas.Close()

        TotalPorcNC = CDbl("0" & TxtPorcentaje_Plaga.Text) + CDbl("0" & TxtPorcentaje_recol.Text) + CDbl("0" & txtPorcentaje_grand.Text) + CDbl("0" & txtPorcentaje_peq.Text)
        If Math.Abs(nPorcentajeNoComercial - TotalPorcNC) > 0.01 And (CDbl("0" & TxtPorcentaje_recol.Text) = Porc_Recoleccion) Then
            MsgBox("Los porcentajes indicados no coinciden con el porcentaje de NC.")
            Comprobacion = False
        End If

    End Function


    Private Function Totalizar() As Boolean
        SumaNC = CDbl("0" & TxtPorcentaje_Plaga.Text) + CDbl("0" & TxtPorcentaje_recol.Text) + CDbl("0" & txtPorcentaje_grand.Text) + CDbl("0" & txtPorcentaje_peq.Text)
        Totalizar = True
        lblDif.Text = Format(ValorNC - SumaNC, "###.##")
        lblSuma.Text = Format(SumaNC, "###.##")
        If ValorNC - SumaNC <> 0 Then
            Totalizar = False
        End If
    End Function

    Private Function DevuelveNoComercial() As Double
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Calidad As Integer
        Dim porcentaje As Double
        Dim SQL As String
        Dim Kilos As Double
        Dim i As Integer

        DevuelveNoComercial = 0
        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount = 0 Then
            MsgBox("Error en el albarán a modificar")
            RsEntradas.Close()
            Return 0
        End If

        RsClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "'  AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
        For i = 1 To RsClasificacion.RecordCount
            RsClasificacion.AbsolutePosition = i
            Calidad = RsClasificacion!codigo_calidad ' - 1
            Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
            If Calidad = 6 Then
                porcentaje = 0
                If RsEntradas!kg_entrada <> 0 Then
                    porcentaje = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                End If
                DevuelveNoComercial = porcentaje
            End If
        Next
        If RsClasificacion.RecordCount = 0 Then
            EsNuevoRegistro = True
        Else
            EsNuevoRegistro = False
        End If

        RsClasificacion.Close()
        RsEntradas.Close()

        Return DevuelveNoComercial

    End Function

    Private Sub FrmNEntradasNuevo18_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String

        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado es inexistente")
            CmdSalir()
        End If

        TxtPorcentaje_Plaga.Text = "" & RsEntrada!porcentaje_plaga
        TxtPorcentaje_recol.Text = "" & RsEntrada!porcentaje_recol
        TxtPorcentaje_recol_i.Text = "" & RsEntrada!porcentaje_recol_i
        txtPorcentaje_grand.Text = "" & RsEntrada!porcentaje_grand
        txtPorcentaje_peq.Text = "" & RsEntrada!porcentaje_peq
        inicial_recol_p = RsEntrada!porcentaje_recol
        inicial_recol_i = RsEntrada!porcentaje_recol_i

        Porc_Recoleccion = RsEntrada!porcentaje_recol

        SumaNC = RsEntrada!porcentaje_plaga + RsEntrada!porcentaje_recol + RsEntrada!porcentaje_grand + RsEntrada!porcentaje_peq
        lblSuma.Text = Format(SumaNC, "###.##")

        ValorNC = DevuelveNoComercial()
        ValorCom = 100 - ValorNC
        lblNC.Text = Format(ValorNC, "###.##")
        lblDif.Text = Format(ValorNC - SumaNC, "###.##")
        RsEntrada.Close()

    End Sub

    Private Sub TxtPorcentaje_Plaga_Validated(sender As Object, e As EventArgs) Handles TxtPorcentaje_Plaga.Validated
        Totalizar()
    End Sub


    Private Sub txtPorcentaje_grand_Validated(sender As Object, e As EventArgs) Handles txtPorcentaje_grand.Validated
        Totalizar()
    End Sub

    Private Sub txtPorcentaje_peq_Validated(sender As Object, e As EventArgs) Handles txtPorcentaje_peq.Validated
        Totalizar()
    End Sub

    Private Sub TxtPorcentaje_recol_Validated(sender As Object, e As EventArgs) Handles TxtPorcentaje_recol.Validated
        If inicial_recol_p = 0 And inicial_recol_i = 0 Then
            TxtPorcentaje_recol_i.Text = TxtPorcentaje_recol.Text
            'ElseIf inicial_recol_p > 0 And inicial_recol_i > 0 Then
        ElseIf inicial_recol_p = 0 And inicial_recol_i > 0 Then
            If MsgBox("¿Debe actualizar el dato de recolección inicial?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                TxtPorcentaje_recol_i.Text = TxtPorcentaje_recol.Text
            End If
        End If
        Totalizar()
    End Sub
End Class