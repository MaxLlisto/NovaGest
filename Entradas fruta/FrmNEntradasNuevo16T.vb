Public Class FrmNEntradasNuevo16T
    Public ModificaIndustria As Boolean
    Public ObjetoGlobal As Object
    Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Dim TxtEnvase(9) As TextBox
    Dim TxtTipoEnvase(9) As TextBox
    Dim Txtbulto(60) As TextBox
    Dim LblBulto(60) As Label
    Dim ModificaPorcentaje As Boolean
    Public oForm As FrmEntradaAlbaranes
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long


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
        Public Sub CalcularPesos()
            Dim i As Integer, Peso1 As Single, Peso2 As Single, Peso3 As Single, Cajas As Integer, Bultos As Integer

            If IsNumeric(TxtPesoBrutoCON.Text) Then
                Peso1 = CLng(TxtPesoBrutoCON.Text)
                Peso2 = 0
                For i = 1 To 9
                    Peso3 = 0
                    If Trim(TxtTipoEnvase(i).Text) = "" Then TxtEnvase(i).Text = ""
                    If Trim(TxtTipoEnvase(i).Text) > "" Then
                        If TarasEnvases(i) <> 0 Then
                            If IsNumeric(TxtEnvase(i).Text) Then Peso3 = Math.Round(CDbl(TxtEnvase(i).Text) * CDbl(TarasEnvases(i)), 3)
                            Peso2 = Peso2 + Peso3
                        End If
                    End If
                    If IsNumeric(TxtEnvase(i).Text) Then
                        If Trim(TxtTipoEnvase(i).Text) = "CAJA" Or Trim(TxtTipoEnvase(i).Text) = "CAZUL" Or Trim(TxtTipoEnvase(i).Text) = "CCAFE" Or Trim(TxtTipoEnvase(i).Text) = "CCOL" Or Trim(TxtTipoEnvase(i).Text) = "CLIRIASP" Or Trim(TxtTipoEnvase(i).Text) = "CAJA." Then
                            If IsNumeric(TxtEnvase(i).Text) Then Cajas = Cajas + CLng(TxtEnvase(i).Text)
                        End If
                        If Trim(TxtTipoEnvase(i).Text) = "PALET" Or Trim(TxtTipoEnvase(i).Text) = "PV" Or Trim(TxtTipoEnvase(i).Text) = "PALET." Then
                            If IsNumeric(TxtEnvase(i).Text) Then Bultos = Bultos + CLng(TxtEnvase(i).Text)
                        End If
                    End If
                Next
                Peso1 = Math.Round(Peso1 - Peso2, 0)
                TxtPesoBrutoSIN.Text = CStr(Peso1)
                TxtTotalCajas.Text = CStr(Cajas)
            End If
        End Sub
        Private Sub CmdGrabar_Click()
            If Comprobacion() = True Then
                GrabarEntrada()
                FlagPreguntar = False
                CmdSalir()
            End If
        End Sub



        Private Sub TxtTipoEnvase_Validate(Index As Integer, Cancel As Boolean)
            Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, i As Integer

            If TxtTipoEnvase(Index).Text = "" Then
                TxtEnvase(Index).Text = ""
                TarasEnvases(Index) = 0
            Else

                Rs.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(TxtTipoEnvase(Index).Text) + "'", ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    MsgBox("Error. Tipo de envase inexistente.")
                    Cancel = True
                    Exit Sub
                End If
                For i = 1 To 9
                    If i <> Index Then
                        If Trim(TxtTipoEnvase(i).Text) = Trim(TxtTipoEnvase(Index).Text) Then
                            MsgBox("Error. Tipo de envase ya anotado.")
                            Cancel = True
                            Exit Sub
                        End If
                    End If
                Next
                TarasEnvases(Index) = Rs!peso
            End If
            CalcularPesos()
        End Sub

        Private Sub TxtTotalBultos_Validate(Cancel As Boolean)
            Dim i As Integer, Cuantos As Integer

            If IsNumeric(TxtTotalbultos.Text) Then
                Cuantos = TxtTotalbultos.Text
            Else
                Cuantos = 0
                TxtTotalbultos.Text = "0"
            End If
            If Cuantos > 60 Then
                MsgBox("El máximo número de bultos es 60.")
                Cancel = True
                Exit Sub
            End If
            For i = 1 To Cuantos
                LblBulto(i).Visible = True
                Txtbulto(i).Visible = True
            Next
            For i = Cuantos + 1 To 60
                Txtbulto(i).Text = ""
                LblBulto(i).Visible = False
                Txtbulto(i).Visible = False
            Next
        End Sub
        Private Sub CmdRecalcularBultos_Click()
            Dim i As Integer, Cajas As Integer, SumaCajas As Integer, Bultos As Integer, CajasBulto As Integer

            Cajas = 0 : If IsNumeric(TxtTotalCajas.Text) = True Then Cajas = CLng(TxtTotalCajas.Text)
            Bultos = 0 : If IsNumeric(TxtTotalbultos.Text) = True Then Bultos = CLng(TxtTotalbultos.Text)
            CajasBulto = 0 : If IsNumeric(TxtCajasBulto.Text) = True Then CajasBulto = CLng(TxtCajasBulto.Text)
            If CajasBulto = 0 And Bultos > 0 Then CajasBulto = Cajas / Bultos
            SumaCajas = 0

            If Cajas > 0 And Bultos > 0 Then
                For i = 1 To Bultos - 1
                    If SumaCajas + CajasBulto <= Cajas Then
                        Txtbulto(i).Text = CStr(CajasBulto)
                    ElseIf SumaCajas < Cajas Then
                        Txtbulto(i).Text = CStr(Cajas - SumaCajas)
                    Else
                        Txtbulto(i).Text = "0"
                    End If
                    SumaCajas = SumaCajas + CLng(Txtbulto(i).Text)
                Next
                If SumaCajas < Cajas Then
                    Txtbulto(Bultos).Text = CStr(Cajas - SumaCajas)
                Else
                    Txtbulto(Bultos).Text = "0"
                End If
            End If
        End Sub
        Private Function Comprobacion() As Boolean
            Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, PesoSin As Long, Cajas As Integer, i As Integer


            Comprobacion = True
            'Pesos y bultos
            If Not (IsNumeric(TxtPesoBrutoCON.Text)) Then
                MsgBox("No se ha indicado peso con envases.")
                Comprobacion = False
                Exit Function
            End If
            If CLng(TxtPesoBrutoCON.Text) = 0 Then
                MsgBox("No se ha especificado peso con envases.")
                Comprobacion = False
                Exit Function
            End If
            If Not (IsNumeric(TxtPesoBrutoSIN.Text)) Then
                MsgBox("No se ha indicado peso sin envases.")
                Comprobacion = False
                Exit Function
            End If

            If Not (IsNumeric(TxtTotalbultos.Text)) And Trim(RsEntrada!industria_sn) = "N" Then
                MsgBox("No se ha indicado número de bultos.")
                Comprobacion = False
                Exit Function
            End If
            If CInt(TxtTotalbultos.Text) <= 0 And Trim(RsEntrada!industria_sn) = "N" Then
                MsgBox("No se ha indicado número de bultos.")
                Comprobacion = False
                Exit Function
            End If
            PesoSin = CLng(TxtPesoBrutoSIN.Text)
            Cajas = CLng(TxtTotalCajas.Text)
            CalcularPesos()

            If PesoSin <> CLng(TxtPesoBrutoSIN.Text) Then
                MsgBox("El peso bruto sin envases se ha reclaculado.Compruebe los cambios.")
                Comprobacion = False
                Exit Function
            End If
            If Cajas <> CLng(TxtTotalCajas.Text) Then
                MsgBox("El número de cajas se ha reclaculado.Compruebe los cambios.")
                Comprobacion = False
                Exit Function
            End If
            Cajas = 0
            For i = 1 To 60
                If IsNumeric(Txtbulto(i).Text) Then
                    Cajas = Cajas + CLng(Txtbulto(i).Text)
                End If
            Next
            If Cajas <> CLng(TxtTotalCajas.Text) Then
                MsgBox("El total de cajas no coincide con las indicadas en los bultos.")
                Comprobacion = False
                Exit Function
            End If
            Comprobacion = True
        End Function
        Private Function GrabarEntrada() As Boolean
            Dim i As Integer
            Dim Cuantos As Long
            Dim Envase As String
            Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim PesoEntrada As Single
        Dim Calidad As Integer
            Dim trans As SqlClient.SqlTransaction
            Dim Campos() As String, Valores() As String

            GrabarEntrada = False


            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            Try

                'entradas_albaranes
                SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
                RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                If RsEntradas.RecordCount = 0 Then
                    trans.Rollback()
                    MsgBox("Error en el albarán a modificar")
                End If
                RsEntradas!peso_bruto_fruta = CLng(TxtPesoBrutoCON.Text)
                RsEntradas!bruto_sin_env = CLng(TxtPesoBrutoSIN.Text)
                If Trim(RsEntradas!tarada_sn) = "N" Then
                    RsEntradas!tara_con_env = 0
                    RsEntradas!tara_sin_env = 0
                    RsEntradas!kg_entrada = 0
                    RsEntradas!kg_a_liquidar = 0
                    RsEntradas!kg_sin_clasif = 0
                ElseIf RsEntradas!kg_sin_clasif > 0 Then
                    If RsEntradas!peso_campo > 0 Then
                        RsEntradas!kg_entrada = RsEntradas!peso_campo
                        RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                        RsEntradas!kg_sin_clasif = RsEntradas!kg_entrada
                        RsEntradas!peso_almacen = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                    Else
                        RsEntradas!kg_entrada = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                        RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                        RsEntradas!kg_sin_clasif = RsEntradas!kg_entrada
                    End If
                Else
                    If RsEntradas!peso_campo > 0 Then
                        RsEntradas!kg_entrada = RsEntradas!peso_campo
                        RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                        RsEntradas!kg_sin_clasif = RsEntradas!kg_entrada
                        RsEntradas!peso_almacen = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                    Else
                        RsEntradas!kg_entrada = RsEntradas!bruto_sin_env - RsEntradas!tara_sin_env
                        RsEntradas!kg_a_liquidar = RsEntradas!kg_entrada
                        RsEntradas!kg_sin_clasif = 0
                    End If
                End If

                If ModificaIndustria = True Then
                    RsEntradas!industria_sn = "N"
                End If
                PesoEntrada = RsEntradas!kg_entrada
                RsEntradas.Update()
                'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "U"

                'entradas_envases (ELIMINA TODOS LOS ENVASES DE ENTRADA)
                RsEntradasEnvases.Open("SELECT * FROM ENTRADAS_ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ENTRADA_SALIDA = 'E'", ObjetoGlobal.cn, True,,,,,, trans)
                While RsEntradasEnvases.RecordCount > 0
                    RsEntradasEnvases.MoveFirst()
                    'If ModoConexion = 1 Then GrabarEnLocal RsEntradasEnvases, "D"
                    RsEntradasEnvases.Delete()
                End While

                For i = 1 To 9
                    Cuantos = 0
                    Envase = ""
                    If IsNumeric(TxtEnvase(i).Text) And Trim(TxtTipoEnvase(i).Text) > "" Then
                        Cuantos = CLng(TxtEnvase(i).Text)
                        Envase = Trim(TxtTipoEnvase(i).Text)
                    End If
                    If Cuantos > 0 Then
                        RsEnvases.Open("SELECT * FROM ENVASES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_ENVASE = '" + Trim(Envase) + "'", ObjetoGlobal.cn)
                        If RsEnvases.RecordCount > 0 Then
                            RsEntradasEnvases.AddNew()
                            RsEntradasEnvases!empresa = Trim(ObjetoGlobal.EmpresaActual)
                            RsEntradasEnvases!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                            RsEntradasEnvases!serie_albaran = Trim(SerieSeleccionada)
                            RsEntradasEnvases!numero_albaran = AlbaranSeleccionado
                            RsEntradasEnvases!codigo_envase = Trim(Envase)
                            RsEntradasEnvases!entrada_salida = "E"
                            RsEntradasEnvases!numero_envases = Cuantos
                            RsEntradasEnvases!tara = RsEnvases!peso
                            RsEntradasEnvases.Update()
                        End If
                        RsEnvases.Close()
                    End If
                Next
                'If ModoConexion = 1 Then GrabarEnLocal RsEntradasEnvases, "I"
                RsEntradasEnvases.Close()


                'clasificacion
                SQL = "SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6"
                RsClasif.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                RecalculoClasificacion(RsClasif, PesoEntrada)
                For i = 1 To RsClasif.RecordCount
                    RsClasif.AbsolutePosition = i
                    Calidad = RsClasif!codigo_calidad
                    If Calidad >= 1 And Calidad <= 12 Then ' Ver con Paco
                        RsClasif!kg_calidad = Math.Round(PesoNuevo(Calidad), 0)
                        'If ModoConexion = 1 Then GrabarEnLocal RsClasif, "U"
                        RsClasif.Update()
                    End If
                Next
                trans.Commit()
                ObjetoGlobal.cn.Close()

                'If ModoConexion = 1 Then CnLocal.CommitTrans
                GrabarEntrada = True
                ReDim Campos(4), Valores(4)
                Campos(0) = "peso_bruto_fruta" : Valores(0) = RsEntradas!peso_bruto_fruta
                Campos(1) = "bruto_sin_env" : Valores(1) = RsEntradas!bruto_sin_env
                Campos(2) = "kg_entrada" : Valores(2) = RsEntradas!kg_entrada
                Campos(3) = "kg_a_liquidar" : Valores(3) = RsEntradas!kg_a_liquidar
                Campos(4) = "kg_sin_clasif" : Valores(4) = RsEntradas!kg_sin_clasif
                oForm.AsignarValores(oForm.CnTabla01, Campos, Valores)
                MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
                Return True

        Catch ex As Exception
                trans.Rollback()
                MsgBox("Error en el albarán a modificar")

            End Try

        End Function
        Public Sub RecalculoClasificacion(Rs As cnRecordset.CnRecordset, PesoEntrada As Single)
            Dim i As Integer
            Dim Peso1 As Single
            Dim Peso2 As Single
            Dim Peso3 As Single
            Dim Proporcion As Single
            Dim Calidad As Integer
            Dim Muestra(12) As Single
            Dim Incremento As Integer
            Dim MejorFactor As Single
            Dim Factor As Single
            Dim Quien As Integer

            Peso1 = Math.Round(PesoEntrada, 0)
            If Peso1 = 0 Then Exit Sub
            Peso2 = 0
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                Calidad = Rs!codigo_calidad
                If Calidad >= 1 And Calidad <= 12 Then ' Ver con Paco
                    Peso3 = Math.Round(Rs!kg_muestra, 2)
                    Muestra(Calidad) = Peso3
                    Peso2 = Peso2 + Peso3
                End If
            Next
            If Peso2 = 0 Then Exit Sub
            Proporcion = Peso1 / Peso2
            PesoNuevo(0) = 0
            For i = 1 To Rs.RecordCount
                PesoNuevo(i) = Math.Round(Muestra(i) * Proporcion, 0)
                PesoNuevo(0) = PesoNuevo(0) + PesoNuevo(i)
            Next
            While PesoNuevo(0) <> Peso1
                Quien = -1
                If Peso1 > PesoNuevo(0) Then Incremento = 1 Else Incremento = -1
                For i = 1 To Rs.RecordCount
                    If Muestra(i) > 0 Then
                        Factor = ((PesoNuevo(i) + Incremento) / Muestra(i)) - Proporcion
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
                    Return
                End If
            End While
            Return
        End Sub
        Private Sub PicF_Click(Index As Integer)
            Dim Cadena As String

            Cadena = "{F" + CStr(Index) + "}"
            SendKeys.Send(Cadena)

        End Sub

        Private Sub FrmNEntradasNuevo16_Load(sender As Object, e As EventArgs) Handles Me.Load
            Dim SQL As String
            Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            Dim i As Integer
            Dim Cajas As Integer
            Dim NumeroBulto As Integer
        Dim Bultos As Integer


        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            CmdSalir()
        End If

        For i = 1 To 9
            TxtEnvase(i) = DevuelveControl(Me, "TxtEnvase" & i, GroupBox1)
            TxtTipoEnvase(i) = DevuelveControl(Me, "TxtTipoEnvase" & Strings.Right("00" & i, 2), GroupBox1)
        Next
        For i = 1 To 60
            Txtbulto(i) = DevuelveControl(Me, "TxtBulto" & Strings.Right("00" & i, 2), GroupBox3)
            LblBulto(i) = DevuelveControl(Me, "LblBulto" & Strings.Right("00" & i, 2), GroupBox3)
        Next

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
        End If

        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/mm/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_proveedor) + " " + Trim("" & RsEntrada!razon_social)
        lblCampo.Text = CStr(RsEntrada!campo_terceros) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!capataz_terc) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista_terc) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)

        RsEnvases.Open("SELECT * FROM ENTRADAS_ENVASES EE JOIN ENVASES E ON E.EMPRESA=EE.EMPRESA AND E.CODIGO_ENVASE = EE.CODIGO_ENVASE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and EE.NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND ENTRADA_SALIDA = 'E' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
        Cajas = 0
        For i = 1 To RsEnvases.RecordCount
            If i < 9 Then
                RsEnvases.AbsolutePosition = i
                TxtTipoEnvase(i).Text = Trim(RsEnvases!codigo_envase)
                TxtEnvase(i).Text = Trim(RsEnvases!numero_envases)
                Cajas = Cajas + RsEnvases!numero_envases
                TarasEnvases(i) = RsEnvases!peso
            End If
        Next
        TxtPesoBrutoCON.Text = Format(RsEntrada!peso_bruto_fruta, "###,##0")
        TxtPesoBrutoSIN.Text = Format(RsEntrada!bruto_sin_env, "###,##0")
        TxtTotalCajas.Text = Format(Cajas, "#,##0")
        TxtPesoCampo.Text = Format("0" + RsEntrada!peso_campo, "###,##0")

        RsBultos.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND bulto<=60 ORDER BY 1,2,3,4,5", ObjetoGlobal.cn)
        For i = 1 To RsBultos.RecordCount
            RsBultos.AbsolutePosition = i
            NumeroBulto = RsBultos!Bulto
            Txtbulto(NumeroBulto).Text = Trim(RsBultos!Cajas)
            Bultos = NumeroBulto
        Next
        For i = Bultos + 1 To 60
            Txtbulto(i).Visible = False
            LblBulto(i).Visible = False
        Next
        TxtTotalbultos.Text = CStr(Bultos)

    End Sub

        Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
            CmdSalir()
        End Sub

        Private Sub TxtPesoBrutoCON_Validated(sender As Object, e As EventArgs) Handles TxtPesoBrutoCON.Validated
            CalcularPesos()
        End Sub

        Private Sub TxtTipoenvase_TextChanged(sender As Object, e As EventArgs) Handles TxtTipoenvase01.TextChanged, TxtTipoenvase02.TextChanged, TxtTipoenvase03.TextChanged, TxtTipoenvase04.TextChanged, TxtTipoenvase05.TextChanged, TxtTipoenvase06.TextChanged, TxtTipoenvase07.TextChanged, TxtTipoenvase08.TextChanged, TxtTipoenvase09.TextChanged
            CalcularPesos()
        End Sub


        Private Sub TxtTipoenvase_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtTipoenvase01.KeyDown, TxtTipoenvase02.KeyDown, TxtTipoenvase03.KeyDown, TxtTipoenvase04.KeyDown, TxtTipoenvase05.KeyDown, TxtTipoenvase06.KeyDown, TxtTipoenvase07.KeyDown, TxtTipoenvase08.KeyDown, TxtTipoenvase09.KeyDown
            Dim index As Integer
            Dim nombrecontrol As String

            nombrecontrol = sender.GetType.Name.Trim
            index = CInt(Strings.Right(nombrecontrol, 2))
            If e.KeyCode = Keys.Down Then ' 40
                SendKeys.Send(vbTab)
            ElseIf e.KeyCode = Keys.Up Then '38
                TxtEnvase(index - 1).Focus()
            ElseIf e.KeyCode = Keys.Right Then '39
                CalcularPesos()
                TxtTotalbultos.Focus()
            End If

        End Sub


        Private Sub TxtEnvase_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtEnvase1.KeyDown, TxtEnvase2.KeyDown, TxtEnvase3.KeyDown, TxtEnvase4.KeyDown, TxtEnvase5.KeyDown, TxtEnvase6.KeyDown, TxtEnvase7.KeyDown, TxtEnvase8.KeyDown, TxtEnvase9.KeyDown
            Dim index As Integer
            Dim nombrecontrol As String

            nombrecontrol = sender.GetType.Name.Trim
            index = CInt(Strings.Right(nombrecontrol, 1))
            If e.KeyCode = Keys.Down Then ' 40
                SendKeys.Send(vbTab)
            ElseIf e.KeyCode = Keys.Up Then '38
                CalcularPesos()
                If index > 1 Then
                    TxtEnvase(index - 1).Focus()
                Else
                    TxtPesoBrutoCON.Focus()
                End If

            ElseIf e.KeyCode = Keys.Right Then '39
                CalcularPesos()
                TxtTotalbultos.Focus()
            End If

        End Sub

        Private Sub TxtEnvase_Validated(sender As Object, e As EventArgs) Handles TxtEnvase1.Validated, TxtEnvase2.Validated, TxtEnvase3.Validated, TxtEnvase4.Validated, TxtEnvase5.Validated, TxtEnvase6.Validated, TxtEnvase7.Validated, TxtEnvase8.Validated, TxtEnvase9.Validated
            CalcularPesos()
        End Sub

        Private Sub TxtEnvase_GotFocus(sender As Object, e As EventArgs) Handles TxtEnvase1.GotFocus, TxtEnvase2.GotFocus, TxtEnvase3.GotFocus, TxtEnvase4.GotFocus, TxtEnvase5.GotFocus, TxtEnvase6.GotFocus, TxtEnvase7.GotFocus, TxtEnvase8.GotFocus, TxtEnvase9.GotFocus
            Dim nombrecontrol As String

            nombrecontrol = sender.GetType.Name.Trim
            EnvaseEnModificacion = CInt(Strings.Right(nombrecontrol, 1))


        End Sub

    End Class
