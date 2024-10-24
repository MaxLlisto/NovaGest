Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class FrmNEntradasNuevo22t
    Public ObjetoGlobal As Object
    Private aCalidades() As String
    Private aCalidadesDes() As String
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim Clasificacion(1, 12) As Long
    Dim MuestraNueva(12) As Single
    Dim PesoNuevo(12) As Long
    Dim PesosMuestras(1, 12) As Double
    Dim PorcentajesClasif(12) As Double
    Dim cc As SqlConnection
    Dim FlagMensaje As Boolean
    Dim FlagGrabarPlagas As Boolean
    Dim QuePlagas(99) As Integer
    Dim PorcentajePlaga(99) As Single
    Dim CuantasPlagas As Integer
    Dim KilosN(10) As Double
    Dim nClasificaciones As Integer
    Public oForm As FrmEntradaAlbaranes
    Dim inicial_recol_p As Double
    Dim inicial_recol_i As Double
    Dim SumaNC As Double
    Dim ValorNC As Double
    Dim ValorCom As Double
    Dim PorcentajeNC As Double
    Dim DiferenciaPorcentajes As Double
    Dim Pasaraprovisional As Boolean
    Dim Pasaradefinitiva As Boolean
    Dim LblDefinitiva(12) As Label
    Dim LblProvisional(12) As Label
    Dim LblEstimada(12) As Label
    Dim LblEstimadaPorc(12) As Label
    Dim LblEnvase(12) As Label
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long

    Private Sub pPasaraDefinitiva()
        Dim i As Integer

        pPasaraProvisional()

        For i = 1 To 12
            LblDefinitiva(i).Text = LblProvisional(i).Text
        Next i
        Pasaradefinitiva = True
        Return

    End Sub

    Private Sub pPasaraProvisional()
        Dim i As Integer

        For i = 1 To 12
            LblProvisional(i).Text = LblEstimada(i).Text
        Next i
        Pasaraprovisional = True
        Return

    End Sub

    Private Sub CmdSalir()
        If Not Pasaraprovisional And Not Pasaradefinitiva Then
            Close()
        ElseIf FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN grabar la clasificación?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Close()
        End If
    End Sub

    Public Function CalcularPesos(Valores) As Boolean
        Dim i As Integer
        Dim Peso1 As Single
        Dim Peso2 As Single
        Dim Peso3 As Single
        Dim Proporcion As Single
        Dim Incremento As Integer
        Dim MejorFactor As Single
        Dim Factor As Single
        Dim Quien As Integer

        CalcularPesos = False
        Peso1 = Math.Round(RsEntrada!kg_entrada, 0)
        If Peso1 = 0 Then
            If FlagMensaje = True Then MsgBox("No existe peso en esta entrada.")
            Return False
        End If

        Peso2 = 0
        For i = 1 To 12
            Peso3 = 0
            If IsNumeric(Valores(i)) Then Peso3 = Math.Round(CDbl(Valores(i)), 2)
            MuestraNueva(i) = Peso3
            Peso2 = Peso2 + Peso3
        Next
        If Peso2 = 0 Then
            If FlagMensaje Then
                MsgBox("No se ha introducido peso para ninguna calidad.")
                Return False
            End If
        End If

        Proporcion = Peso1 / Peso2
        PesoNuevo(0) = 0
        For i = 1 To 12
            PesoNuevo(i) = Math.Round(MuestraNueva(i) * Proporcion, 0)
            PesoNuevo(0) = PesoNuevo(0) + PesoNuevo(i)
        Next
        While PesoNuevo(0) <> Peso1
            Quien = -1
            If Peso1 > PesoNuevo(0) Then Incremento = 1 Else Incremento = -1
            For i = 1 To nClasificaciones '7
                If MuestraNueva(i) > 0 Then
                    Factor = ((PesoNuevo(i) + Incremento) / MuestraNueva(i)) - Proporcion
                    If Quien = -1 Or (Incremento > 0 And Factor < MejorFactor) Or (Incremento < 0 And Factor > MejorFactor) Then
                        Quien = i
                        MejorFactor = Factor
                    End If
                End If
            Next
            If Quien > 0 Then
                PesoNuevo(Quien) = PesoNuevo(Quien) + Incremento
                PesoNuevo(0) = PesoNuevo(0) + Incremento
            Else
                MsgBox("No se puede cuadrar el peso.")
                Exit Function
            End If
        End While

        For i = 1 To 12 ' nClasificaciones (solo naranjas)
            LblEstimada(i).Text = Format(PesoNuevo(i), "###,##0")
            LblEstimadaPorc(i).Text = Format(Math.Round(PesoNuevo(i) * 100 / Peso1, 2), "##0.00")
        Next
        LblKilosCalculados.Text = Format(PesoNuevo(0), "###,##0")
        CalcularPesos = True
    End Function
    Private Sub CmdGrabar_Click()
        'FlagMensaje = True

        'If Not Pasaraprovisional And Not Pasaradefinitiva Then
        '    MsgBox("No se ha grabado ninguna clasificación")
        '    'GrabarEntrada
        '    FlagPreguntar = False
        '    CmdSalir()
        'ElseIf Pasaradefinitiva Then
        '    GrabarEntrada()
        '    FlagPreguntar = False
        '    CmdSalir()
        'ElseIf Pasaraprovisional Then
        '    GrabarEntrada()
        '    FlagPreguntar = False
        '    CmdSalir()
        'End If
        'FlagMensaje = False

    End Sub
    Private Function GrabarEntrada() As Boolean
        Dim i As Integer
        Dim RsAlbaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlbaranClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPendiente As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            'entradas_albaranes
            RsAlbaran.Open("SELECT * FROM ENTRADAS_ALBARANES E WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran), ObjetoGlobal.cn, True,,,,,, trans)
            RsAlbaran!kg_sin_clasif = 0
            RsAlbaran.Update()
            'If ModoConexion = 1 Then GrabarEnLocal RsAlbaran, "U"
            RsAlbaran.Close()

            'entradas_clasificacion
            If Pasaraprovisional Then
                RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran) + " AND TIPO_CLASIFICACION = 'PRO'", ObjetoGlobal.cn, True,,,,,, trans)
                While RsAlbaranClasificacion.RecordCount > 0
                    RsAlbaranClasificacion.MoveFirst()
                    'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "D"
                    RsAlbaranClasificacion.Delete()
                End While
                RsAlbaranClasificacion.Close()
                RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                For i = 1 To nClasificaciones
                    RsAlbaranClasificacion.AddNew()
                    RsAlbaranClasificacion!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsAlbaranClasificacion!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    RsAlbaranClasificacion!serie_albaran = Trim(RsEntrada!serie_albaran)
                    RsAlbaranClasificacion!numero_albaran = RsEntrada!numero_albaran
                    RsAlbaranClasificacion!Tipo_clasificacion = "PRO"
                    RsAlbaranClasificacion!codigo_calidad = aCalidades(i) ' aaaa bbbb
                    RsAlbaranClasificacion!codigo_variedad = Trim(RsEntrada!codigo_variedad)
                    RsAlbaranClasificacion!kg_muestra = Math.Round(CDbl(LblEstimadaPorc(i).Text), 2)
                    RsAlbaranClasificacion!kg_calidad = Math.Round(CDbl(LblProvisional(i).Text), 0)
                    RsAlbaranClasificacion.Update()
                    'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "I"
                Next
                RsAlbaranClasificacion.Close()
            End If

            If Pasaradefinitiva Then
                RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn, True,,,,,, trans)
                While RsAlbaranClasificacion.RecordCount > 0
                    RsAlbaranClasificacion.MoveFirst()
                    'if ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "D"
                    RsAlbaranClasificacion.Delete()
                End While
                RsAlbaranClasificacion.Close()
                RsAlbaranClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                For i = 1 To nClasificaciones
                    RsAlbaranClasificacion.AddNew()
                    RsAlbaranClasificacion!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsAlbaranClasificacion!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    RsAlbaranClasificacion!serie_albaran = Trim(RsEntrada!serie_albaran)
                    RsAlbaranClasificacion!numero_albaran = RsEntrada!numero_albaran
                    RsAlbaranClasificacion!Tipo_clasificacion = "LIQ"
                    RsAlbaranClasificacion!codigo_calidad = aCalidades(i)
                    RsAlbaranClasificacion!codigo_variedad = Trim(RsEntrada!codigo_variedad)
                    RsAlbaranClasificacion!kg_muestra = Math.Round(CDbl(LblEstimadaPorc(i).Text), 2)
                    RsAlbaranClasificacion!kg_calidad = Math.Round(CDbl(LblDefinitiva(i).Text), 0)
                    RsAlbaranClasificacion.Update()
                    'If ModoConexion = 1 Then GrabarEnLocal RsAlbaranClasificacion, "I"
                Next
                RsAlbaranClasificacion.Close()
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()

            GrabarEntrada = True
            AlbaranSeleccionado = -1
            SerieSeleccionada = ""
            MsgBox("Anotada la clasificacion del albarán :" + CStr(RsEntrada!numero_albaran))
            If Pasaradefinitiva Then
                'ImprimeAlbaran3(RsEntrada!serie_albaran, RsEntrada!numero_albaran, RsEntrada!codigo_variedad, RsEntrada!Transportista, CDbl("0" & RsEntrada!peso_campo))
                ImprimeAlbaran3(RsEntrada!serie_albaran, RsEntrada!numero_albaran, RsEntrada!codigo_variedad, RsEntrada!Transportista_terc, CDbl("0" & RsEntrada!peso_campo))
            End If
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("Error en el albarán a modificar " & ex.Message)
            Return False
        End Try

    End Function


    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String

        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        FlagMensaje = True

        If Not Pasaraprovisional And Not Pasaradefinitiva Then
            MsgBox("No se ha grabado ninguna clasificación")
            'GrabarEntrada
            FlagPreguntar = False
            CmdSalir()
        ElseIf Pasaradefinitiva Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        ElseIf Pasaraprovisional Then
            GrabarEntrada()
            FlagPreguntar = False
            CmdSalir()
        End If
        FlagMensaje = False

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        CmdSalir()
    End Sub

    Private Sub CmdPasaraProvisional_Click(sender As Object, e As EventArgs) Handles CmdPasaraProvisional.Click
        pPasaraProvisional()
    End Sub

    Private Sub CmdPasaraDefinitiva_Click(sender As Object, e As EventArgs) Handles CmdPasaraDefinitiva.Click
        pPasaraDefinitiva()
    End Sub

    Private Sub FrmNEntradasNuevo22_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, i As Integer, j As Integer
        Dim rsCalVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim TotalPorc As Double
        Dim Max As Double
        Dim OrdMax As Integer
        Dim Porc As Double
        Dim RsBtoClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBtoCalibr As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CalidadesBultos(12) As Double
        Dim CalidadesEstimadas(12) As Double
        Dim PesoVolcado As Double
        Dim CoefVolcado As Double
        Dim Item As ListViewItem
        Dim RsEntradaBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim peso_bultoE() As Double
        Dim peso_bultoP() As Double
        Dim Bultos_entrados As Integer
        Dim RsPesoBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim PesoBulto As Double

        For i = 1 To 12
            LblProvisional(i) = DevuelveControl(Me, "lblProvisional" & Strings.Right("00" & i, 2))
            LblDefinitiva(i) = DevuelveControl(Me, "lblDefinitiva" & Strings.Right("00" & i, 2))
            LblEnvase(i) = DevuelveControl(Me, "lblEnvase" & Strings.Right("00" & i, 2))
            LblEstimada(i) = DevuelveControl(Me, "LblEstimada" & Strings.Right("00" & i, 2))
            LblEstimadaPorc(i) = DevuelveControl(Me, "lblPorcentaje" & Strings.Right("00" & i, 2))
        Next

        DiferenciaPorcentajes = 0

        Pasaraprovisional = False
        Pasaradefinitiva = False

        CmdPasaraProvisional.Visible = False
        CmdPasaraDefinitiva.Visible = False

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir()
            Exit Sub
        End If

        SQL = "SELECT e.*, pr.razon_social, oc.nombre AS NOMBRE_CAPATAZ,ot.nombre AS NOMBRE_TRANSPORTISTA,c.situacion_campo AS DESCRIPCION_SITUACION,v.descripcion AS DESCRIPCION_VARIEDAD,p.descripcion_per AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM entradas_albaranes e JOIN proveedores_coop pr ON pr.codigo_proveedor = e.codigo_proveedor "
        SQL = Trim(SQL) + " JOIN campos_terceros c ON c.empresa = e.empresa AND c.ejercicio = e.ejercicio AND c.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
        SQL = Trim(SQL) + " JOIN personal_terceros oc ON oc.codigo = e.capataz_terc "
        SQL = Trim(SQL) + " JOIN personal_terceros ot ON ot.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " JOIN periodos_coop p ON p.empresa = e.empresa AND p.ejercicio = e.ejercicio AND p.codigo_variedad = e.codigo_variedad AND p.codigo_periodo = e.codigo_periodo "
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.serie_albaran = '" + Trim(SerieSeleccionada) + "' and numero_albaran = " + CStr(AlbaranSeleccionado)

        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado es inexistente", MsgBoxStyle.Critical)
            CmdSalir()
            Exit Sub
        End If
        lblKilosfruta.Text = Format(RsEntrada!kg_entrada, "###,##0")

        RsEntradaBultos.Open("SELECT * FROM entradas_bultos WHERE empresa ='" & RsEntrada!empresa & "' AND ejercicio='" & RsEntrada!Ejercicio & "' AND serie_albaran = '" + Trim(SerieSeleccionada) + "' AND numero_albaran = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn)

        Bultos_entrados = RsEntradaBultos.RecordCount
        ReDim peso_bultoE(Bultos_entrados + 1)
        ReDim peso_bultoP(Bultos_entrados + 1)

        For i = 1 To Bultos_entrados
            RsPesoBultos.Open("SELECT dbo.fn_peso_bulto('" & RsEntrada!empresa & "','" & RsEntrada!Ejercicio & "','" + Trim(SerieSeleccionada) + "'," & CStr(AlbaranSeleccionado) & "," & CStr(i) & " ) as peso", ObjetoGlobal.cn)
            peso_bultoE(i) = peso_bultoE(i) + RsPesoBultos!peso
            RsPesoBultos.Close()
        Next

        inicial_recol_p = RsEntrada!porcentaje_recol
        inicial_recol_i = RsEntrada!porcentaje_recol_i

        SQL = "SELECT * FROM Calidades_Var_ej  WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad = '" & RsEntrada!codigo_variedad & "' order by 1,2,3,4"
        rsCalVariedad.Open(SQL, ObjetoGlobal.cn)
        If rsCalVariedad.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("No existen calidades para esta variedad", MsgBoxStyle.Critical)
            CmdSalir()
            Return
        End If


        nClasificaciones = rsCalVariedad.RecordCount
        ReDim aCalidades(nClasificaciones)
        ReDim aCalidadesDes(nClasificaciones)

        For i = 1 To 12
            LblEstimadaPorc(i).Visible = False
            LblEstimada(i).Visible = False
            LblProvisional(i).Visible = False
            LblDefinitiva(i).Visible = False
            LblEnvase(i).Visible = False
        Next

        i = 1
        While Not rsCalVariedad.EOF
            If i <= 12 Then
                aCalidades(i) = rsCalVariedad!codigo_calidad
                aCalidadesDes(i) = rsCalVariedad!Descripcion
                LblEstimadaPorc(i).Visible = True
                LblEstimada(i).Visible = True
                LblProvisional(i).Visible = True
                LblDefinitiva(i).Visible = True
                LblEnvase(i).Visible = True
                LblEnvase(i).Text = Trim("" & rsCalVariedad!Descripcion)
                i = i + 1
            End If
            rsCalVariedad.MoveNext()
        End While
        rsCalVariedad.Close()

        If RsEntrada!tarada_sn <> "S" Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado no ha sido tarado todavía.", MsgBoxStyle.Exclamation)
            CmdSalir()
            Return
        End If

        ' Obtenemos el peso de los cartones volcados
        SQL = "SELECT DISTINCT bulto FROM entradas_clasif_bt "
        SQL = Trim(SQL) + " WHERE Tipo_clasificacion = 'PRO' AND EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsBtoClasif.Open(SQL, ObjetoGlobal.cn)

        LblBultosVolcados.Text = "" & RsBtoClasif.RecordCount
        RsBtoClasif.Close()

        SQL = "SELECT e.*, c.descripcion FROM entradas_clasif_bt e LEFT JOIN calidades_var_ej c ON c.empresa =e.empresa AND c.ejercicio=e.ejercicio AND c.codigo_variedad=e.codigo_variedad AND c.codigo_calidad = e.codigo_calidad "
        SQL = Trim(SQL) + " WHERE e.Tipo_clasificacion = 'PRO' AND e.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and e.NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        SQL = Trim(SQL) + " ORDER BY 1,2,3,4,5"
        RsBtoClasif.Open(SQL, ObjetoGlobal.cn)

        If RsBtoClasif.RecordCount = 0 Then
            RsBtoClasif.Close()
            FlagPreguntar = False
            MsgBox("No se ha volcado ninguna caja del albarán solicitado.", MsgBoxStyle.Exclamation)
            CmdSalir()
            Return
        End If

        lvBultos.Items.Clear()
        PesoVolcado = 0

        While Not RsBtoClasif.EOF
            peso_bultoP(RsBtoClasif!Bulto) = peso_bultoP(RsBtoClasif!Bulto) + RsBtoClasif!kg_muestra
            RsBtoClasif.MoveNext()
        End While

        RsBtoClasif.MoveFirst()

        While Not RsBtoClasif.EOF
            Item = lvBultos.Items.Add(Trim(RsBtoClasif!Bulto))
            Item.SubItems.Add("" & RsBtoClasif!Descripcion)
            Item.SubItems.Add("" & Format(RsBtoClasif!kg_muestra, "##,###,##0.00"))
            PesoBulto = 0
            If peso_bultoP(RsBtoClasif!Bulto) <> 0 Then
                PesoBulto = Math.Round((peso_bultoE(RsBtoClasif!Bulto) / peso_bultoP(RsBtoClasif!Bulto)) * RsBtoClasif!kg_muestra, 2)
            End If
            Item.SubItems.Add("" & Format(PesoBulto, "##,###,##0.00"))
            PesoVolcado = PesoVolcado + RsBtoClasif!kg_calidad
            For i = 1 To 12
                If aCalidades(i) = RsBtoClasif!codigo_calidad Then
                    CalidadesBultos(i) = CalidadesBultos(i) + PesoBulto
                    Exit For
                End If
            Next
            RsBtoClasif.MoveNext()
        End While

        lblKgVolcadosFruta.Text = Format(PesoVolcado, "##,###,##0")
        CoefVolcado = CDbl("0" & lblKilosfruta.Text) / PesoVolcado

        ' Control de pesos
        For i = 1 To 12
            CalidadesEstimadas(i) = Math.Round(CalidadesBultos(i) * CoefVolcado, 2)
        Next
        CalcularPesos(CalidadesEstimadas)


        SQL = "SELECT * FROM entradas_calibr_bt "
        SQL = Trim(SQL) + " WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        SQL = Trim(SQL) + " ORDER BY 1,2,3,4,5,6"
        RsBtoCalibr.Open(SQL, ObjetoGlobal.cn)

        lvNombres.Items.Clear()

        While Not RsBtoCalibr.EOF
            Item = lvNombres.Items.Add(Trim(RsBtoCalibr!Bulto))
            Item.SubItems.Add("" & Trim(RsBtoCalibr!Orden))
            Item.SubItems.Add("" & Trim(RsBtoCalibr!nombre_calidad))
            Item.SubItems.Add("" & Format(RsBtoCalibr!kg_calidad, "##,###,##0"))
            RsBtoCalibr.MoveNext()
        End While


        If PesoVolcado > 0 Then
            CmdPasaraProvisional.Visible = True
        End If

        RsClasificacion.Open("SELECT * FROM entradas_clasif WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran) + " AND TIPO_CLASIFICACION = 'PRO'", ObjetoGlobal.cn)
        If RsClasificacion.RecordCount = 0 Then
            CmdPasaraProvisional.Visible = True
        End If
        RsClasificacion.Close()

        RsClasificacion.Open("SELECT * FROM entradas_clasif WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(RsEntrada!serie_albaran) + "' and NUMERO_ALBARAN = " + CStr(RsEntrada!numero_albaran) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn)
        If RsClasificacion.RecordCount = 0 And Not CmdPasaraProvisional.Visible Then
            CmdPasaraDefinitiva.Visible = True
        End If
        RsClasificacion.Close()

        Me.KeyPreview = True
        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_proveedor) + " " + Trim("" & RsEntrada!razon_social)
        lblCampo.Text = CStr(RsEntrada!campo_terceros) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!capataz_terc) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista_terc) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)

        For i = 0 To 1
            For j = 0 To 12
                Clasificacion(i, j) = 0
            Next
        Next
        For i = 0 To 1
            For j = 0 To 12
                PesosMuestras(i, j) = 0#
            Next
        Next
        For j = 0 To 12
            PorcentajesClasif(j) = 0#
        Next

        RsClasificacion.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " order by 1,2,3,4,5,6", ObjetoGlobal.cn)
        For i = 1 To RsClasificacion.RecordCount
            RsClasificacion.AbsolutePosition = i
            If Trim(RsClasificacion!Tipo_clasificacion) = "PRO" Then
                j = 0
            Else
                j = 1
            End If
            If Trim(RsClasificacion!codigo_calidad) > 0 And Trim(RsClasificacion!codigo_calidad) <= 12 Then
                CmdPasaraDefinitiva.Visible = True
                Clasificacion(j, RsClasificacion!codigo_calidad) = RsClasificacion!kg_calidad
                PesosMuestras(j, RsClasificacion!codigo_calidad) = RsClasificacion!kg_muestra
            End If
        Next
        RsClasificacion.Close()
        TotalPorc = 100
        For i = 0 To 1
            For j = 1 To 12
                If i = 0 Then
                    LblProvisional(j).Text = Format(Clasificacion(i, j), "###,##0")
                Else
                    LblDefinitiva(j).Text = Format(Clasificacion(i, j), "###,##0")
                    Porc = Math.Round(Clasificacion(i, j) * 100 / RsEntrada!kg_entrada, 2)
                    TotalPorc = Math.Round(TotalPorc - Porc, 2)
                    If Porc > Max Then
                        Max = Porc
                        OrdMax = j
                    End If
                    PorcentajesClasif(j) = Porc
                End If
            Next
        Next
        If TotalPorc <> 0 Then
            PorcentajesClasif(OrdMax) = PorcentajesClasif(OrdMax) + TotalPorc
        End If
        For j = 1 To 12
            If j = 6 Then ' Ver solo cítricos
                PorcentajeNC = PorcentajesClasif(j)
                DiferenciaPorcentajes = Math.Round((SumaNC - PorcentajeNC), 2)
            End If
        Next

        FlagMensaje = False
        FlagGrabarPlagas = False
    End Sub


End Class