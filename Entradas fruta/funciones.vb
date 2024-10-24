Module funciones
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public TipoDeAlbaran As String ' S Socios T terceros
    Public ReferenciaCampoTerceros() As String ' CodigoSocio (10) + Variedad(10) + CodigoCampo(10)
    Public DatosCampoTerceros() As String 'Situación(10) + Descripcion(60) + Hanegadas(##.####)(7)+ DESCRIPCION_VARIEDAD (30)
    Public CuantosCamposTerceros As Long
    Public ReferenciaSocioTerceros() As String ' Apellidos + Nombre(80) + CodigoSocio(10)
    Public CuantosSociosTerceros As Long
    Public ReferenciaOperarioTerceros() As String ' Apellidos + Nombre(80) + CodigoOperario(10)
    Public CuantosOperariosTerceros As Long

    Public ReferenciaCampo() As String ' CodigoSocio (10) + Variedad(10) + CodigoCampo(10)
    Public DatosCampo() As String 'Situación(10) + Descripcion(60) + Hanegadas(##.####)(7)+ DESCRIPCION_VARIEDAD (30)
    Public CuantosCampos As Long
    Public ReferenciaSocio() As String ' Apellidos + Nombre(80) + CodigoSocio(10)
    Public CuantosSocios As Long
    Public ReferenciaOperario() As String ' Apellidos + Nombre(80) + CodigoOperario(10)
    Public CuantosOperarios As Long
    Public EnvaseSeleccionado As String
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long
    Public oImpAlbaran1 As Object
    Public oImpEtiquetas As Object
    Public XProceso1 As String
    Public XDocumento1 As String
    Public XFormato1 As String
    Public XProceso2 As String
    Public XDocumento2 As String
    Public XFormato2 As String
    Public XProceso3 As String
    Public XDocumento3 As String
    Public XFormato3 As String
    Public XProceso4 As String
    Public XDocumento4 As String
    Public XFormato4 As String
    Public XProcesoE As String
    Public XDocumentoE As String
    Public XFormatoE As String
    Public HayImpresoraEtiquetas As Boolean
    Public HayImpresoraAlbaranes As Boolean
    Public NombreImpresoraAlbaranes As String
    Public ModoConexion As String
    '0 = Solo conexión Ntliria
    '1 = Ambas
    '2 = Solo conexión local
    '3 = Ambas. Se ha recuperado la conexión remota y se intenta el proceso de actualizacion
    '10 = SIN NINGUNA CONEXION
    Public TextoEtiquetas(100, 4) As String
    Public CuantasEtiquetas As Integer
    Dim Caracteres39(39) As String

    Public Function DevuelveEtiqueta(Ejercicio As String, Albaran As Long, Bulto As Integer) As String
        Dim etiqueta As String, Bulto2 As Integer
        Dim C1 As String, C2 As String, c3 As String
        Dim i1 As Integer, i2 As Integer, r1 As Integer, r2 As Integer

        'Acepta como parametros los datos del bulto y devuelve la referencia del codigo de barras correspondiente. Si se produce un error, se devuelve ''
        'Las cajas de clasificación entran como bulto 101,102,103 ... (en principio 2 cajas)
        'Las cajas de reclamación como 111,112,113... (en principio 1 caja)
        'Las cajas de control colla como bultos 121,122,123,... (en principio 2 cajas)
        'La referencia se compone de tres partes:
        'Caracter 1
        'Indica el ejercicio
        '9 = 2009, 0 = 2010, 1 = 2011, ... 8 = 2018
        'Pero, si lel número de bulto es del 41 al 80, se utiliza
        '    2009 = J
        '    2010 = A
        '    2011 = B
        '    2012 = C
        '    2013 = D
        '    2014 = E
        '    2015  = F
        '    2016 = G
        '    2017 = H
        '    2018 = I
        'Si se trata de cajas, se utiliza
        '    2009 = T
        '    2010 = K
        '    2011 = L
        '    2012 = M
        '    2013 = N
        '    2014 = O
        '    2015 = P
        '    2016 = Q
        '    2017 = R
        '    2018 = S
        '
        'CARACTERES 2, 3, 4
        'El número de albarán
        '
        'Caracter 5
        'El numero de bulto, según la siguiente tabla:
        '
        'Bulto   Ascii  Carácter
        '1   49  1
        '2   50  2
        '3   51  3
        '4   52  4
        '5   53  5
        '6   54  6
        '7   55  7
        '8   56  8
        '9   57  9
        '10  36  $
        '11  37  %
        '12  43  +
        '13  47  /
        '14  65  A
        '15  66  B
        '16  67  C
        '17  68  D
        '18  69  E
        '19  70  F
        '20  71  G
        '21  72  H
        '22  73  I
        '23  74  J
        '24  75  K
        '25  76  L
        '26  77  M
        '27  78  N
        '28  79  O
        '29  80  P
        '30  81  Q
        '31  82  R
        '32  83  S
        '33  84  T
        '34  85  U
        '35  86  V
        '36  87  W
        '37  88  X
        '38  89  Y
        '39  90  Z
        '40  48  0

        Ejercicio = Trim(Ejercicio)

        DevuelveEtiqueta = ""
        C1 = ""
        If Bulto > 0 And Bulto <= 40 Then
            If Ejercicio = "2010" Then
                C1 = "0"
            ElseIf Ejercicio = "2021" Then
                C1 = "1"
            ElseIf Ejercicio = "2022" Then
                C1 = "2"
            ElseIf Ejercicio = "2023" Then
                C1 = "3"
            ElseIf Ejercicio = "2024" Then
                C1 = "4"
            ElseIf Ejercicio = "2015" Then
                C1 = "5"
            ElseIf Ejercicio = "2016" Then
                C1 = "6"
            ElseIf Ejercicio = "2017" Then
                C1 = "7"
            ElseIf Ejercicio = "2018" Then
                C1 = "8"
            ElseIf Ejercicio = "2019" Then
                C1 = "9"
            ElseIf Ejercicio = "2020" Then
                C1 = "0"
            End If
        ElseIf Bulto > 40 And Bulto <= 80 Then
            If Ejercicio = "2010" Then
                C1 = "A"
            ElseIf Ejercicio = "2021" Then
                C1 = "B"
            ElseIf Ejercicio = "2022" Then
                C1 = "C"
            ElseIf Ejercicio = "2023" Then
                C1 = "D"
            ElseIf Ejercicio = "2024" Then
                C1 = "E"
            ElseIf Ejercicio = "2015" Then
                C1 = "F"
            ElseIf Ejercicio = "2016" Then
                C1 = "G"
            ElseIf Ejercicio = "2017" Then
                C1 = "H"
            ElseIf Ejercicio = "2018" Then
                C1 = "I"
            ElseIf Ejercicio = "2019" Then
                C1 = "J"
            ElseIf Ejercicio = "2020" Then
                C1 = "U"
            End If
        ElseIf Bulto > 100 Then
            If Ejercicio = "2010" Then
                C1 = "K"
            ElseIf Ejercicio = "2021" Then
                C1 = "L"
            ElseIf Ejercicio = "2022" Then
                C1 = "M"
            ElseIf Ejercicio = "2023" Then
                C1 = "N"
            ElseIf Ejercicio = "2024" Then
                C1 = "O"
            ElseIf Ejercicio = "2015" Then
                C1 = "P"
            ElseIf Ejercicio = "2016" Then
                C1 = "Q"
            ElseIf Ejercicio = "2017" Then
                C1 = "R"
            ElseIf Ejercicio = "2018" Then
                C1 = "S"
            ElseIf Ejercicio = "2019" Then
                C1 = "T"
            ElseIf Ejercicio = "2020" Then
                C1 = "V"
            End If
        End If
        C2 = ""
        If C1 > "" Then
            i1 = Albaran \ 1600
            r1 = Albaran - (1600 * i1)
            i2 = r1 \ 40
            r2 = r1 - (40 * i2)
            If i1 >= 0 And i1 <= 39 And i2 >= 0 And i2 <= 39 And r2 >= 0 And r2 <= 39 Then
                C2 = DevuelveCaracter39(i1) + DevuelveCaracter39(i2) + DevuelveCaracter39(r2)
            End If
        End If
        c3 = ""
        If C1 > "" And C2 > "" Then
            If Bulto < 100 Then Bulto2 = Bulto Else Bulto2 = Bulto - 100
            If Bulto > 40 And Bulto <= 80 Then Bulto2 = Bulto - 40
            If Bulto2 = 40 Then
                c3 = "0"
            Else
                If Bulto2 > 0 And Bulto2 < 40 Then c3 = DevuelveCaracter39(Bulto2)
            End If
        End If
        etiqueta = Trim(C1) + Trim(C2) + Trim(c3)
        Return Trim(etiqueta)
    End Function

    Private Function DevuelveCaracter39(Valor As Integer) As String

        DevuelveCaracter39 = ""
        If Caracteres39(0) = "" Then
            Caracteres39(0) = "0"
            Caracteres39(1) = "1"
            Caracteres39(2) = "2"
            Caracteres39(3) = "3"
            Caracteres39(4) = "4"
            Caracteres39(5) = "5"
            Caracteres39(6) = "6"
            Caracteres39(7) = "7"
            Caracteres39(8) = "8"
            Caracteres39(9) = "9"
            Caracteres39(10) = "$"
            Caracteres39(11) = "%"
            Caracteres39(12) = "+"
            Caracteres39(13) = "/"
            Caracteres39(14) = "A"
            Caracteres39(15) = "B"
            Caracteres39(16) = "C"
            Caracteres39(17) = "D"
            Caracteres39(18) = "E"
            Caracteres39(19) = "F"
            Caracteres39(20) = "G"
            Caracteres39(21) = "H"
            Caracteres39(22) = "I"
            Caracteres39(23) = "J"
            Caracteres39(24) = "K"
            Caracteres39(25) = "L"
            Caracteres39(26) = "M"
            Caracteres39(27) = "N"
            Caracteres39(28) = "O"
            Caracteres39(29) = "P"
            Caracteres39(30) = "Q"
            Caracteres39(31) = "R"
            Caracteres39(32) = "S"
            Caracteres39(33) = "T"
            Caracteres39(34) = "U"
            Caracteres39(35) = "V"
            Caracteres39(36) = "W"
            Caracteres39(37) = "X"
            Caracteres39(38) = "Y"
            Caracteres39(39) = "Z"
        End If
        If Valor >= 0 And Valor <= 39 Then
            Return Caracteres39(Valor)
        End If
    End Function

    Public Sub ImprimeAlbaran1(SerieAlbaran As String, NumeroAlbaran As Long, Variedad As String, Transportista As Integer, PesoCampo As Double)
        Dim i As Integer, FormatoImpresion As String, FlagPrevisualizar As Boolean
        Dim cDocumentoImpresion As String
        Dim oImp As ReportBuilder2.ClsImpresion

        oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)

        FlagPrevisualizar = False
        cDocumentoImpresion = "EntradasA5_documento1"
        If Left(Variedad, 3) = "401" Then
            FormatoImpresion = "entradas_lliriaA5_sandiaN1"
        ElseIf Left(Variedad, 3) = "409" Or Left(Variedad, 3) = "408" Then
            FormatoImpresion = "entradas_lliriaA5_sandiaM1"
        ElseIf Left(Variedad, 3) = "405" Then
            FormatoImpresion = "entradas_lliriaA5_sandia1_4"
        ElseIf Left(Variedad, 2) = "40" Then
            FormatoImpresion = "entradas_lliriaA5_sandia1"
        ElseIf Left(Variedad, 3) = "110" Then
            FormatoImpresion = "entradas_lliriaA5_algarroba"
        ElseIf Left(Variedad, 4) = "0125" Or Left(Variedad, 4) = "0110" Then
            FormatoImpresion = "entradas_lliriaA5_documento1_n"
        ElseIf Left(Variedad, 2) = "14" Then
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento1gt"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento1_g"
            End If
        ElseIf Left(Variedad, 2) = "17" Or Left(Variedad, 2) = "09" Or Left(Variedad, 2) = "03" Or Left(Variedad, 2) = "08" Or Left(Variedad, 2) = "46" Or Left(Variedad, 2) = "48" Or Left(Variedad, 4) = "0127" Then
            FormatoImpresion = "entradas_lliriaA5_documento1_k" '
        ElseIf Left(Variedad, 2) = "12" Then
            FormatoImpresion = "entradas_lliriaA5_documento1_4"
            cDocumentoImpresion = "EntradasA5_Almendra1"
        ElseIf PesoCampo <> 0 Then
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_peso_cam1_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_peso_cam1_4"
            End If
        Else
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento1_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento1_4"
            End If
        End If
        If HayImpresoraAlbaranes = True Then
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 2, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Return
            End If
        Else
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 1, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Return
            End If
        End If

        'oImp.oImpEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'oImp.oImpEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'oImp.oImpTipoDocumento = "Albaran de entrada (1)"
        'oImp.oImpSerie = Trim(SerieAlbaran)
        'oImp.oImpNumeroDocumento = CStr(NumeroAlbaran)
        'oImp.oImpUsuario = Trim(ObjetoGlobal.NombreUsuario)
        If Trim(TipoDeAlbaran) = "T" Then
            oImp.FuerzaCopias = 2
            'VuelcaAlbaran1T(oImp, SerieAlbaran, NumeroAlbaran)
        Else
            If Transportista = 1 Then
                oImp.FuerzaCopias = 2
            Else
                oImp.FuerzaCopias = 1
            End If
            VuelcaAlbaran1(oImp, SerieAlbaran, NumeroAlbaran)
        End If
        oImp.Imprimir()
    End Sub

    Public Sub VuelcaAlbaran1(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer, SQL As String, Cadena As String, Cadena2 As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String
        Dim MensajeEurep As String
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Palet(2) As Integer
        Dim Columna(2) As Integer
        Dim Bultos As Integer
        Dim Altura As String
        Dim Altura2 As String
        Dim ValorAltura As Double
        Dim ValorAltura2 As Double

        SQL = "SELECT E.*,S.*,CU.EUREP,CU.NATURANE_SN,CU.AG_ECOLOGICA,cu.prod_integrada, P.DESCRIPCION_PER,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND E.NUMERO_ALBARAN = " + CStr(NumeroAlbaran)

        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then

            SQL = "SELECT Isnull(count(*),0) as bultos FROM entradas_bultos WHERE empresa = '" + Trim(RsEntradas!empresa) + "'"
            SQL = Trim(SQL) + " AND ejercicio = '" + Trim(RsEntradas!Ejercicio) + "'"
            SQL = Trim(SQL) + " AND serie_albaran = '" + Trim(RsEntradas!serie_albaran) + "'"
            SQL = Trim(SQL) + " AND numero_albaran = " + CStr(RsEntradas!numero_albaran)
            SQL = Trim(SQL) + " AND bulto <100"

            RsBultos.Open(SQL, ObjetoGlobal.cn)
            Bultos = RsBultos!Bultos
            RsBultos.Close()

            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If IsDBNull(RsEntradas(i)) Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                Else
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr(RsEntradas(i)))
                End If
            Next

            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_per))
            Cadena = Trim("" & RsEntradas!apellidos_socio) + ", " + Trim("" & RsEntradas!nombre_socio)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim(Cadena))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            Cadena = Trim("" & RsEntradas!NOMBRE_CAPATAZ)
            If Trim(RsEntradas!codigo_variedad) >= "01" And Trim(RsEntradas!codigo_variedad) <= "02z" Then
                Randomize()
                Palet(1) = 1 + Int(Rnd() * Bultos) : Columna(1) = 1 + Int(Rnd() * 6)
                Palet(2) = 0
                While Palet(2) = 0 Or (Palet(2) = Palet(1) And Bultos > 1)
                    Palet(2) = 1 + Int(Rnd() * Bultos) : Columna(2) = 1 + Int(Rnd() * 6)
                End While
                ValorAltura = Rnd()

                If ValorAltura < 0.33 Then
                    Altura = "A"
                ElseIf ValorAltura < 0.66 Then
                    Altura = "B"
                Else
                    Altura = "C"
                End If
                ValorAltura2 = Rnd()

                If ValorAltura2 < 0.33 Then
                    Altura2 = "A"
                ElseIf ValorAltura2 < 0.66 Then
                    Altura2 = "B"
                Else
                    Altura2 = "C"
                End If

                Cadena2 = "P:" + CStr(Palet(1)) + "/" + CStr(Columna(1)) + Trim(Altura) + "  P:" + CStr(Palet(2)) + "/" + CStr(Columna(2)) + Trim(Altura2)
                Cadena = Trim(Cadena) + Space(15) + Trim(Cadena2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim(Cadena))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))

            MensajeEurep = ""
            If Trim("" & RsEntradas!prod_integrada) = "S" Then
                MensajeEurep = "SPRING"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.ECOLÓG./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" Then
                MensajeEurep = "PROD.ECOLÓGICA"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.RECONV./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" Then
                MensajeEurep = "PROD.RECONVERSIÓN"
            ElseIf Trim("" & RsEntradas!eurep) = "S" Then
                'MensajeEurep = "GLOBALG.A.P."
                If IsDBNull(RsEntradas!grasp) Then
                    MensajeEurep = "GLOBALG.A.P."
                Else
                    If Trim(RsEntradas!grasp) = "S" Then
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    Else
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    End If
                End If

            ElseIf Trim("" & RsEntradas!naturane_sn) = "S" Then
                MensajeEurep = "TRAT.NATURANE"
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.eurep", 0, Trim(MensajeEurep))

            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then
                EnvasesEntrados = Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            ' Pintamos las cabeceras de las calidades
            SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

            RsCalVar.Open(SQL, ObjetoGlobal.cn)
            For i = 1 To RsCalVar.RecordCount
                RsCalVar.AbsolutePosition = i
                oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (CInt("0" & RsCalVar!codigo_calidad) - 1), 0, Trim("" & RsCalVar!descripcion_abrev))
            Next
            RsCalVar.Close()
            RsEntradas.Close()
        End If
    End Sub

    Public Sub ImprimeAlbaran2(SerieAlbaran As String, NumeroAlbaran As Long, Variedad As String, Transportista As Integer, PesoCampo As Double)
        Dim i As Integer, FormatoImpresion As String, FlagPrevisualizar As Boolean
        Dim cDocumentoImpresion As String
        Dim oImp As ReportBuilder2.ClsImpresion

        FlagPrevisualizar = False
        oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)

        cDocumentoImpresion = "EntradasA5_documento2"
        If Left(Variedad, 3) = "401" Then
            FormatoImpresion = "entradas_lliriaA5_sandiaN2"
        ElseIf Left(Variedad, 4) = "0125" Or Left(Variedad, 4) = "0110" Then
            FormatoImpresion = "entradas_lliriaA5_documento2_n"
        ElseIf Left(Variedad, 3) = "409" Or Left(Variedad, 3) = "408" Then
            FormatoImpresion = "entradas_lliriaA5_sandiaM2"
        ElseIf Left(Variedad, 3) = "405" Then
            FormatoImpresion = "entradas_lliriaA5_sandia2_4"
        ElseIf Left(Variedad, 2) = "14" Then
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento2gt"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento2_g" '"entradas_lliriaA5_caqui2"
            End If
        ElseIf Left(Variedad, 2) = "17" Or Left(Variedad, 2) = "09" Or Left(Variedad, 2) = "03" Or Left(Variedad, 2) = "08" Or Left(Variedad, 2) = "46" Or Left(Variedad, 2) = "48" Or Left(Variedad, 4) = "0127" Then
            FormatoImpresion = "entradas_lliriaA5_documento2_k" '"entradas_lliriaA5_caqui2"
        ElseIf Left(Variedad, 2) = "40" Then
            FormatoImpresion = "entradas_lliriaA5_sandia2"
        ElseIf Left(Variedad, 2) = "12" Then
            FormatoImpresion = "entradas_lliriaA5_documento2_4"
            cDocumentoImpresion = "EntradasA5_Almendra2"
        ElseIf Left(Variedad, 3) = "110" Then
            Exit Sub
        ElseIf PesoCampo > 0 Then
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_peso_cam2_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_peso_cam2_4"
            End If
        Else
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento2_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento2_4"
            End If
        End If
        If HayImpresoraAlbaranes = True Then
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 2, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Exit Sub
            End If
        Else
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 1, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Exit Sub
            End If
        End If

        'oImp.oImpEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'oImp.oImpEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'oImp.oImpTipoDocumento = "Albaran de entrada (2)"
        'oImp.oImpSerie = Trim(SerieAlbaran)
        'oImp.oImpNumeroDocumento = CStr(NumeroAlbaran)
        'oImp.oImpUsuario = Trim(ObjetoGlobal.NombreUsuario)

        If Trim(TipoDeAlbaran) = "T" Then
            oImp.FuerzaCopias = 3
            VuelcaAlbaran2T(oImp, SerieAlbaran, NumeroAlbaran)
        Else
            If Transportista = 1 Then
                oImp.FuerzaCopias = 2
            Else
                oImp.FuerzaCopias = 1
            End If
            VuelcaAlbaran2(oImp, SerieAlbaran, NumeroAlbaran)
        End If
        oImp.Imprimir()
    End Sub
    Public Sub VuelcaAlbaran2(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer, SQL As String, Cadena As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String, EnvasesSalidos As String, MensajeEurep As String
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        SQL = "SELECT E.*,S.*,CU.EUREP,CU.NATURANE_SN,CU.AG_ECOLOGICA,cu.prod_integrada, P.DESCRIPCION_PER,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND E.NUMERO_ALBARAN = " + CStr(NumeroAlbaran)

        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If IsDBNull(RsEntradas(i)) Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                Else
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr(RsEntradas(i)))
                End If
            Next
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_per))
            Cadena = Trim("" & RsEntradas!apellidos_socio) + ", " + Trim("" & RsEntradas!nombre_socio)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim(Cadena))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim("" & RsEntradas!NOMBRE_CAPATAZ))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))

            MensajeEurep = ""

            If Trim("" & RsEntradas!prod_integrada) = "S" Then
                MensajeEurep = "SPRING"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.ECOLÓG./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" Then
                MensajeEurep = "PROD.ECOLÓGICA"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.RECONV./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" Then
                MensajeEurep = "PROD.RECONVERSIÓN"
            ElseIf Trim("" & RsEntradas!eurep) = "S" Then
                'MensajeEurep = "GLOBALG.A.P."
                If IsDBNull(RsEntradas!grasp) Then
                    MensajeEurep = "GLOBALG.A.P."
                Else
                    If Trim(RsEntradas!grasp) = "S" Then
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    Else
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    End If
                End If

            ElseIf Trim("" & RsEntradas!naturane_sn) = "S" Then
                MensajeEurep = "TRAT.NATURANE"
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.eurep", 0, Trim(MensajeEurep))

            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then
                EnvasesEntrados = Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            RsEnvases.Close()
            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'S'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesSalidos = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesSalidos = EnvasesSalidos + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesSalidos) > 2 Then
                EnvasesSalidos = Left(EnvasesSalidos, Len(EnvasesSalidos) - 2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_salidos", 0, Trim(EnvasesSalidos))

            SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

            RsCalVar.Open(SQL, ObjetoGlobal.cn)
            For i = 1 To RsCalVar.RecordCount
                RsCalVar.AbsolutePosition = i
                oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (CInt("0" & RsCalVar!codigo_calidad) - 1), 0, Trim("" & RsCalVar!descripcion_abrev))
            Next
            RsEntradas.Close()
            RsCalVar.Close()

        End If
    End Sub
    Public Sub ImprimeAlbaran3(SerieAlbaran As String, NumeroAlbaran As Long, Variedad As String, Transportista As Integer, PesoCampo As Double)
        Dim i As Integer, FormatoImpresion As String, FlagPrevisualizar As Boolean
        Dim cDocumentoImpresion As String
        Dim oImp As ReportBuilder2.ClsImpresion

        FlagPrevisualizar = False
        oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)
        cDocumentoImpresion = "EntradasA5_documento3"
        If Left(Variedad, 3) = "401" Then
            FormatoImpresion = "entradas_lliriaA5_sandiaN3"
        ElseIf Left(Variedad, 4) = "0125" Or Left(Variedad, 4) = "0110" Then
            FormatoImpresion = "entradas_lliriaA5_documento3_n"
        ElseIf Left(Variedad, 3) = "409" Or Left(Variedad, 3) = "408" Then
            FormatoImpresion = "entradas_lliriaA5_sandiaM3"
        ElseIf Left(Variedad, 3) = "405" Then
            FormatoImpresion = "entradas_lliriaA5_sandia3_4"
        ElseIf Left(Variedad, 2) = "40" Then
            FormatoImpresion = "entradas_lliriaA5_sandia3"
        ElseIf Left(Variedad, 2) = "14" Then
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento3gt" '"entradas_lliriaA5_caqui3"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento3_q" '"entradas_lliriaA5_caqui3"
            End If
        ElseIf Left(Variedad, 2) = "17" Or Left(Variedad, 2) = "09" Or Left(Variedad, 2) = "03" Or Left(Variedad, 2) = "08" Or Left(Variedad, 2) = "46" Or Left(Variedad, 2) = "48" Or Left(Variedad, 4) = "0127" Then
            FormatoImpresion = "entradas_lliriaA5_documento3_q" '"entradas_lliriaA5_caqui3"
        ElseIf Left(Variedad, 2) = "12" Then
            FormatoImpresion = "entradas_lliriaA5_documento3_4"
            cDocumentoImpresion = "EntradasA5_Almendra3"
        ElseIf PesoCampo > 0 Then
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_peso_cam3_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_peso_cam3_4"
            End If
        Else
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento3_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento3_4"
            End If
        End If
        If HayImpresoraAlbaranes = True Then
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 2, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Exit Sub
            End If
        Else
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 1, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Exit Sub
            End If
        End If

        'oImp.oImpEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'oImp.oImpEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'oImp.oImpTipoDocumento = "Albaran entrada (3)"
        'oImp.oImpSerie = Trim(SerieAlbaran)
        'oImp.oImpNumeroDocumento = CStr(NumeroAlbaran)
        'oImp.oImpUsuario = Trim(ObjetoGlobal.NombreUsuario)

        If Trim(TipoDeAlbaran) = "T" Then
            VuelcaAlbaran3T(oImp, SerieAlbaran, NumeroAlbaran)
        Else
            VuelcaAlbaran3(oImp, SerieAlbaran, NumeroAlbaran)
        End If
        oImp.FuerzaCopias = 1
        oImp.Imprimir()
    End Sub
    Public Sub ImprimeDesglosePlagas(SerieAlbaran As String, NumeroAlbaran As Long, Variedad As String)
        Dim i As Integer, FormatoImpresion As String, FlagPrevisualizar As Boolean
        Dim cDocumentoImpresion As String
        Dim oImp As ReportBuilder2.ClsImpresion

        FlagPrevisualizar = False
        oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)
        cDocumentoImpresion = "EntradasA5_documento4"

        If Left(Variedad, 4) = "0125" Or Left(Variedad, 4) = "0110" Then
            FormatoImpresion = "entradas_lliriaA5_documento4_n"
        Else
            If Trim(TipoDeAlbaran) = "T" Then
                FormatoImpresion = "entradas_lliriaA5_documento4_t"
            Else
                FormatoImpresion = "entradas_lliriaA5_documento4_4"
            End If
        End If
        If HayImpresoraAlbaranes = True Then
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 2, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Exit Sub
            End If
        Else
            If Not oImp.Inicializar("Albaranes de entrada", cDocumentoImpresion, FormatoImpresion, 1, FlagPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Exit Sub
            End If
        End If
        'oImp.oImpEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'oImp.oImpEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'oImp.oImpTipoDocumento = "Albaran entrada (plagas)"
        'oImp.oImpSerie = Trim(SerieAlbaran)
        'oImp.oImpNumeroDocumento = CStr(NumeroAlbaran)
        'oImp.oImpUsuario = Trim(ObjetoGlobal.NombreUsuario)
        If Trim(TipoDeAlbaran) = "T" Then
            VuelcaDesglosePlagasT(oImp, SerieAlbaran, NumeroAlbaran)
        Else
            VuelcaDesglosePlagas(oImp, SerieAlbaran, NumeroAlbaran)
        End If
        oImp.Imprimir()
    End Sub

    Public Sub VuelcaAlbaran3(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer, SQL As String, Cadena As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String, EnvasesSalidos As String, MensajeEurep As String
        Dim porcentaje As Single, Calidad As Integer, Kilos As Long
        Dim nPorc_nc As Single
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        'Dim Cadena As String

        SQL = "SELECT E.*,S.*,CU.EUREP,CU.NATURANE_SN,CU.AG_ECOLOGICA,cu.prod_integrada, P.DESCRIPCION_PER,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND E.NUMERO_ALBARAN = " + CStr(NumeroAlbaran)

        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If IsDBNull(RsEntradas(i)) Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                Else
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr(RsEntradas(i)))
                End If
            Next
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_per))
            Cadena = Trim("" & RsEntradas!apellidos_socio) + ", " + Trim("" & RsEntradas!nombre_socio)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim(Cadena))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim("" & RsEntradas!NOMBRE_CAPATAZ))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))

            MensajeEurep = ""

            If Trim("" & RsEntradas!prod_integrada) = "S" Then
                MensajeEurep = "SPRING"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.ECOLÓG./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" Then
                MensajeEurep = "PROD.ECOLÓGICA"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.RECONV./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" Then
                MensajeEurep = "PROD.RECONVERSIÓN"
            ElseIf Trim("" & RsEntradas!eurep) = "S" Then
                'MensajeEurep = "GLOBALG.A.P."
                If IsDBNull(RsEntradas!grasp) Then
                    MensajeEurep = "GLOBALG.A.P."
                Else
                    If Trim(RsEntradas!grasp) = "S" Then
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    Else
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    End If
                End If

            ElseIf Trim("" & RsEntradas!naturane_sn) = "S" Then
                MensajeEurep = "TRAT.NATURANE"
            End If

            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.eurep", 0, Trim(MensajeEurep))

            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then
                EnvasesEntrados = Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            RsEnvases.Close()
            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'S'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesSalidos = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesSalidos = EnvasesSalidos + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesSalidos) > 2 Then
                EnvasesSalidos = Left(EnvasesSalidos, Len(EnvasesSalidos) - 2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_salidos", 0, Trim(EnvasesSalidos))

            RsClasificacion.Open("SELECT ec.*, cv.* FROM ENTRADAS_CLASIF ec LEFT JOIN calidades_var_ej cv ON cv.empresa=ec.empresa AND cv.ejercicio = ec.ejercicio AND cv.codigo_variedad = ec.codigo_variedad AND cv.codigo_calidad=ec.codigo_calidad WHERE ec.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ec.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ec.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND ec.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ec.TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
            If RsClasificacion.RecordCount > 0 Then
                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Calidad = CInt("0" & RsClasificacion!codigo_calidad) - 1
                    Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
                    If Calidad >= 0 And Calidad <= 12 Then
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & CStr(Calidad), 0, Trim("" & RsClasificacion!descripcion_abrev))
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidadx_" & CStr(Calidad), 0, Trim("" & RsClasificacion!descripcion_docum))
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.kilos_calidad_" & CStr(Calidad), 0, Format(Kilos, "###,##0"))
                        If RsEntradas!kg_entrada <> 0 Then
                            porcentaje = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_calidad_" & CStr(Calidad), 0, CStr(porcentaje))
                        End If
                        If Calidad = 5 Then
                            nPorc_nc = porcentaje
                        End If
                    End If
                Next
            Else
                SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

                RsCalVar.Open(SQL, ObjetoGlobal.cn)
                For i = 1 To RsCalVar.RecordCount
                    RsCalVar.AbsolutePosition = i
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (RsCalVar!codigo_calidad - 1), 0, Trim("" & RsCalVar!descripcion_abrev))
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidadx_" & (RsCalVar!codigo_calidad - 1), 0, Trim("" & RsCalVar!descripcion_docum))
                Next
                RsCalVar.Close()
            End If
            RsClasificacion.Close()

            'Ahora imprimimor la distrtibución del no comercial
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_defecto", 0, Format((RsEntradas!porcentaje_plaga + RsEntradas!porcentaje_recol), "#0.00"))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_tamano", 0, Format((RsEntradas!porcentaje_grand + RsEntradas!porcentaje_peq), "#0.00"))
            RsEntradas.Close()

        End If
    End Sub
    Public Sub VuelcaDesglosePlagas(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer, SQL As String, Cadena As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String
        Dim EnvasesSalidos As String
        Dim MensajeEurep As String
        Dim porcentaje As Single, Calidad As Integer, Kilos As Long
        Dim nPorc_nc As Single
        Dim nTotalPlagas As Double
        Dim nTotalRecoleccion As Double
        Dim sSqlPlagas As String
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        SQL = "SELECT E.*,S.*,CU.EUREP,CU.NATURANE_SN,CU.AG_ECOLOGICA,cu.prod_integrada, P.DESCRIPCION_PER,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND E.NUMERO_ALBARAN = " + CStr(NumeroAlbaran)

        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If IsDBNull(RsEntradas(i).text) Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                Else
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr(RsEntradas(i)))
                End If
            Next
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_per))
            Cadena = Trim("" & RsEntradas!apellidos_socio) + ", " + Trim("" & RsEntradas!nombre_socio)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim(Cadena))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim("" & RsEntradas!NOMBRE_CAPATAZ))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))

            MensajeEurep = ""

            If Trim("" & RsEntradas!prod_integrada) = "S" Then
                MensajeEurep = "SPRING"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.ECOLÓG./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" Then
                MensajeEurep = "PROD.ECOLÓGICA"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" And Trim("" & RsEntradas!eurep) = "S" Then
                MensajeEurep = "PROD.RECONV./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" Then
                MensajeEurep = "PROD.RECONVERSIÓN"
            ElseIf Trim("" & RsEntradas!eurep) = "S" Then
                'MensajeEurep = "GLOBALG.A.P."
                If IsDBNull(RsEntradas!grasp) Then
                    MensajeEurep = "GLOBALG.A.P."
                Else
                    If Trim(RsEntradas!grasp) = "S" Then
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    Else
                        MensajeEurep = "GLOBALG.A.P./GRASP"
                    End If
                End If

            ElseIf Trim("" & RsEntradas!naturane_sn) = "S" Then
                MensajeEurep = "TRAT.NATURANE"
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.eurep", 0, Trim(MensajeEurep))

            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then EnvasesEntrados = Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            RsEnvases.Close()
            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'S'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesSalidos = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesSalidos = EnvasesSalidos + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesSalidos) > 2 Then EnvasesSalidos = Left(EnvasesSalidos, Len(EnvasesSalidos) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_salidos", 0, Trim(EnvasesSalidos))

            RsClasificacion.Open("SELECT ec.*, cv.descripcion_docum FROM ENTRADAS_CLASIF ec LEFT JOIN calidades_var_ej cv ON cv.empresa=ec.empresa AND cv.ejercicio = ec.ejercicio AND cv.codigo_variedad = ec.codigo_variedad  AND cv.codigo_calidad=ec.codigo_calidad WHERE ec.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ec.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ec.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND ec.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ec.TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
            If RsClasificacion.RecordCount > 0 Then
                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Calidad = RsClasificacion!codigo_calidad - 1
                    Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
                    'If Calidad >= 0 And Calidad <= 7 Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & CStr(Calidad), 0, Trim(RsClasificacion!descripcion_docum))
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.kilos_calidad_" & CStr(Calidad), 0, Format(Kilos, "###,##0"))
                    If RsEntradas!kg_entrada <> 0 Then
                        porcentaje = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_calidad_" & CStr(Calidad), 0, CStr(porcentaje))
                    End If
                    If i = RsClasificacion.RecordCount Then
                        nPorc_nc = porcentaje
                    End If
                    'End If
                Next
            Else
                SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

                RsCalVar.Open(SQL, ObjetoGlobal.cn)
                For i = 1 To RsCalVar.RecordCount
                    RsCalVar.AbsolutePosition = i
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (RsCalVar!codigo_calidad - 1), 0, Trim(RsCalVar!descripcion_docum))
                Next
                RsCalVar.Close()
            End If
            RsClasificacion.Close()

            ' Ahora imprimimos la distrtibución del no comercial
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_defecto", 0, Format((RsEntradas!porcentaje_plaga + IIf(IsDBNull(RsEntradas!porcentaje_recol), 0, RsEntradas!porcentaje_recol)), "#0.00"))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_tamano", 0, Format((IIf(IsDBNull(RsEntradas!porcentaje_grand), 0, RsEntradas!porcentaje_grand) + IIf(IsDBNull(RsEntradas!porcentaje_peq), 0, RsEntradas!porcentaje_peq)), "#0.00"))

            sSqlPlagas = "SELECT e.codigo_plaga, e.porcentaje, p.descripcion, e.tipo_plaga FROM entradas_plagas e LEFT JOIN plagas p ON p.empresa =e.empresa AND p.codigo_plaga= e.codigo_plaga "
            sSqlPlagas = sSqlPlagas + "WHERE e.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND e.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) & " ORDER BY e.codigo_plaga"
            RsPlagas.Open(sSqlPlagas, ObjetoGlobal.cn)
            nTotalPlagas = 0
            While Not RsPlagas.EOF
                If RsPlagas!tipo_plaga = "D" Then
                    If RsPlagas!porcentaje > 0 Then nTotalPlagas = nTotalPlagas + RsPlagas!porcentaje
                Else
                    If RsPlagas!porcentaje > 0 Then nTotalRecoleccion = nTotalRecoleccion + RsPlagas!porcentaje
                End If
                RsPlagas.MoveNext()
            End While
            If RsPlagas.RecordCount > 0 Then
                RsPlagas.MoveFirst()
                i = 0
                If nTotalPlagas <> 0 Then
                    While Not RsPlagas.EOF
                        i = i + 1
                        If RsPlagas!tipo_plaga = "D" Then
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.texto_plaga" & i, 0, Trim(RsPlagas!Descripcion))
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_plaga" & i, 0, Format(Math.Round(((RsEntradas!porcentaje_plaga / nTotalPlagas) * RsPlagas!porcentaje), 2), "#0.00"))
                        ElseIf RsPlagas!tipo_plaga = "R" Then
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.texto_plaga" & i, 0, Trim(RsPlagas!Descripcion))
                            If RsPlagas!porcentaje > 0 Then
                                oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_plaga" & i, 0, Format(Math.Round(((RsEntradas!porcentaje_recol / nTotalRecoleccion) * RsPlagas!porcentaje), 2), "#0.00"))
                            End If
                        Else
                            MsgBox("Tipo de plaga no reconocido")

                        End If
                        RsPlagas.MoveNext()
                    End While
                End If
            End If
            RsPlagas.Close()
            RsEntradas.Close()
        End If
    End Sub

    Public Sub ImprimeEtiquetas(SerieAlbaran As String, NumeroAlbaran As Long)
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim proceso As String
        Dim formato As String
        Dim Sql As String
        Dim oImpEtiquetas As ReportBuilder2.ClsImpresion

        formato = "Etiqueta_entrada_v6c_X"
        proceso = formato

        oImpEtiquetas = New ReportBuilder2.ClsImpresion(ObjetoGlobal)
        If HayImpresoraEtiquetas Then
            If Not oImpEtiquetas.Inicializar(proceso, "Etiquetas_entrada", formato, 2, False, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición para etiquetas.")
                Exit Sub
            End If
        Else
            If Not oImpEtiquetas.Inicializar(proceso, "Etiquetas_entrada", formato, 1, False, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición para etiquetas.")
                Exit Sub
            End If
        End If

        'oImpEtiquetas.oImpEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'oImpEtiquetas.oImpEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'oImpEtiquetas.oImpTipoDocumento = "Etiquetas entrada"
        'oImpEtiquetas.oImpSerie = Trim(SerieAlbaran)
        'oImpEtiquetas.oImpNumeroDocumento = CStr(NumeroAlbaran)
        'oImpEtiquetas.oImpUsuario = Trim(ObjetoGlobal.NombreUsuario)

        If Trim(TipoDeAlbaran) = "T" Then
            VuelcaEtiquetasT(oImpEtiquetas, SerieAlbaran, NumeroAlbaran)
        Else
            VuelcaEtiquetas(oImpEtiquetas, SerieAlbaran, NumeroAlbaran)
        End If
        oImpEtiquetas.Imprimir()
    End Sub
    Public Function VuelcaEtiquetas(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long) As Boolean
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEtiquetas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Integer
        Dim j As Integer
        Dim Cadena As String
        Dim Clasificacion As Integer
        Dim NumeroEtiqueta As Integer
        Dim NumeroDocumento As Integer
        Dim CajasExtra As Integer
        Dim CodigoMuestra As String

        VuelcaEtiquetas = False
        '
        SQL = "SELECT E.*,S.*,CU.EUREP, cu.prod_integrada, CU.AG_ECOLOGICA,CU.NATURANE_SN,P.DESCRIPCION_PER,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND E.NUMERO_ALBARAN = " + CStr(NumeroAlbaran)
        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            Cadena = ""
            If Trim("" & RsEntradas!prod_integrada) = "S" Then
                Cadena = "SPRING"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" And Trim("" & RsEntradas!eurep) = "S" Then
                Cadena = "PROD.ECOLÓG./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "S" Then
                Cadena = "PROD.ECOLÓGICA"
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" And Trim("" & RsEntradas!eurep) = "S" Then
                Cadena = "PROD.RECONV./GLOBALG.A.P."
            ElseIf Trim("" & RsEntradas!ag_ecologica) = "R" Then
                Cadena = "PROD.RECONVERSIÓN"
            ElseIf Trim("" & RsEntradas!eurep) = "S" Then
                'MensajeEurep = "GLOBALG.A.P."
                If IsDBNull(RsEntradas!grasp) Then
                    Cadena = "GLOBALG.A.P."
                Else
                    If Trim(RsEntradas!grasp) = "S" Then
                        Cadena = "GLOBALG.A.P./GRASP"
                    Else
                        Cadena = "GLOBALG.A.P./GRASP"
                    End If
                End If

            ElseIf Trim("" & RsEntradas!naturane_sn) = "S" Then
                Cadena = "TRAT.NATURANE"
            End If


            SQL = "SELECT * FROM CULTIVOS_ESTIMACION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO='" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_CAMPO = " + Trim(RsEntradas!codigo_campo) + " AND CODIGO_VARIEDAD = '" + Trim(RsEntradas!codigo_variedad) + "' ORDER BY contador desc"
            Clasificacion = 0
            RsClasificacion.Open(SQL, ObjetoGlobal.cn)
            If RsClasificacion.RecordCount > 0 Then Clasificacion = RsClasificacion!clasificacion_campo
            RsClasificacion.Close()

            If Clasificacion > 0 Then
                If Clasificacion = 1 Then
                    Cadena = Trim(Cadena) + "  A"
                ElseIf Clasificacion = 2 Then
                    Cadena = Trim(Cadena) + "  B"
                ElseIf Clasificacion = 3 Then
                    Cadena = Trim(Cadena) + "  C"
                End If
            End If
            SQL = "SELECT * FROM ENTRADAS_BULTOS EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " order by 1,2,3,4,5"
            RsEtiquetas.Open(SQL, ObjetoGlobal.cn)
            CuantasEtiquetas = 0
            For i = 1 To RsEtiquetas.RecordCount
                RsEtiquetas.AbsolutePosition = i
                CuantasEtiquetas = CuantasEtiquetas + 1
                If RsEtiquetas!Bulto < 100 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = Trim(Cadena)
                    If IsDBNull(RsEtiquetas!codigo_envase) Then
                        If Left(RsEntradas!codigo_variedad, 2) = "40" Then
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   PALOT "
                        Else
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   CAJAS: " + CStr(RsEtiquetas!Cajas)
                        End If
                    Else
                        If Trim(RsEtiquetas!codigo_envase) = "PALET" Then
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   CAJAS: " + CStr(RsEtiquetas!Cajas)
                        Else
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   PALOT " '+ CStr(RsEtiquetas!Cajas)
                        End If
                    End If
                    TextoEtiquetas(CuantasEtiquetas, 2) = Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy")
                    If Left(RsEntradas!codigo_variedad, 2) = "14" Or Left(RsEntradas!codigo_variedad, 2) = "17" Or Left(RsEntradas!codigo_variedad, 2) = "09" Or Left(RsEntradas!codigo_variedad, 2) = "08" Or Left(RsEntradas!codigo_variedad, 2) = "46" Or Left(RsEntradas!codigo_variedad, 2) = "48" Or Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 2) = TextoEtiquetas(CuantasEtiquetas, 2) + " " + CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 2) = TextoEtiquetas(CuantasEtiquetas, 2) + " " + Trim(RsEtiquetas!Referencia)
                    End If
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Left(RsEntradas!codigo_variedad, 2) = "14" Or Left(RsEntradas!codigo_variedad, 2) = "17" Or Left(RsEntradas!codigo_variedad, 2) = "09" Or Left(RsEntradas!codigo_variedad, 2) = "08" Or Left(RsEntradas!codigo_variedad, 2) = "46" Or Left(RsEntradas!codigo_variedad, 2) = "48" Or Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If
                ElseIf RsEtiquetas!Bulto < 110 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = ""
                    TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy")
                    TextoEtiquetas(CuantasEtiquetas, 2) = "CLASIFICACION " + Trim(RsEtiquetas!Referencia)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Left(RsEntradas!codigo_variedad, 2) = "14" Or Left(RsEntradas!codigo_variedad, 2) = "17" Or Left(RsEntradas!codigo_variedad, 2) = "09" Or Left(RsEntradas!codigo_variedad, 2) = "08" Or Left(RsEntradas!codigo_variedad, 2) = "46" Or Left(RsEntradas!codigo_variedad, 2) = "48" Or Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If
                ElseIf RsEtiquetas!Bulto < 120 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = ""
                    TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy")
                    TextoEtiquetas(CuantasEtiquetas, 2) = "RECLAMACION " + Trim(RsEtiquetas!Referencia)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Left(RsEntradas!codigo_variedad, 2) = "14" Or Left(RsEntradas!codigo_variedad, 2) = "17" Or Left(RsEntradas!codigo_variedad, 2) = "09" Or Left(RsEntradas!codigo_variedad, 2) = "08" Or Left(RsEntradas!codigo_variedad, 2) = "46" Or Left(RsEntradas!codigo_variedad, 2) = "48" Or Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If
                ElseIf RsEtiquetas!Bulto < 130 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = ""
                    TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy") + " " + CStr(RsEntradas!Capataz)
                    TextoEtiquetas(CuantasEtiquetas, 2) = "POST-RECOL. " + Trim(RsEtiquetas!Referencia)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Left(RsEntradas!codigo_variedad, 2) = "14" Or Left(RsEntradas!codigo_variedad, 2) = "17" Or Left(RsEntradas!codigo_variedad, 2) = "09" Or Left(RsEntradas!codigo_variedad, 2) = "08" Or Left(RsEntradas!codigo_variedad, 2) = "46" Or Left(RsEntradas!codigo_variedad, 2) = "48" Or Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If
                End If
            Next

            If Left(RsEntradas!codigo_variedad, 2) = "14" Or Left(RsEntradas!codigo_variedad, 2) = "17" Or Left(RsEntradas!codigo_variedad, 2) = "09" Or Left(RsEntradas!codigo_variedad, 2) = "08" Or Left(RsEntradas!codigo_variedad, 2) = "46" Or Left(RsEntradas!codigo_variedad, 2) = "48" Or Left(RsEntradas!codigo_variedad, 2) = "03" Then
                CajasExtra = 6 * Fix(1 + (RsEtiquetas.RecordCount - 0.1) / 6)
                For i = 1 To 2
                    CuantasEtiquetas = CuantasEtiquetas + 1
                    If i = 1 Then
                        TextoEtiquetas(CuantasEtiquetas, 0) = "MUESTRA"
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 0) = "VOLCADO"
                    End If
                    If RsEntradas!industria_sn = "S" Then
                        TextoEtiquetas(CuantasEtiquetas, 1) = ""
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy") + " " + CStr(RsEntradas!Capataz)
                        CodigoMuestra = CStr(7 + i) + Format(RsEtiquetas!numero_albaran, "00000")
                    End If
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    TextoEtiquetas(CuantasEtiquetas, 4) = CodigoMuestra
                Next
            End If

            RsEntradas.Close()
            RsEtiquetas.Close()
            NumeroEtiqueta = 0
            NumeroDocumento = 0
            For i = 1 To CuantasEtiquetas
                NumeroEtiqueta = NumeroEtiqueta + 1
                If NumeroEtiqueta Mod 16 = 1 Then
                    NumeroEtiqueta = 1
                    NumeroDocumento = NumeroDocumento + 1
                End If
                For j = 0 To 3
                    If Trim("" & TextoEtiquetas(NumeroEtiqueta, j)) > "" Then
                        oImpEtiquetas.VolcarAImpresion(CLng(NumeroDocumento), 0, 0, 0, "calculado.texto_" + Format(NumeroEtiqueta, "00") + "_" + CStr(j), 0, Trim("" & TextoEtiquetas(i, j)))
                    End If
                Next
                If Trim("" & TextoEtiquetas(NumeroEtiqueta, j)) > "" Then
                    oImpEtiquetas.VolcarAImpresion(CLng(NumeroDocumento), 0, 0, 0, "calculado.cb39_" + Format(NumeroEtiqueta, "00") + "_1", 0, Trim("" & TextoEtiquetas(i, 4)))
                End If
            Next
            Return True
        End If
    End Function
    Public Sub VuelcaAlbaran2T(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer, SQL As String, Cadena As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String
        Dim EnvasesSalidos As String
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        SQL = "SELECT e.*, pr.razon_social, oc.nombre AS NOMBRE_CAPATAZ,ot.nombre AS NOMBRE_TRANSPORTISTA,c.situacion_campo AS DESCRIPCION_SITUACION,v.descripcion AS DESCRIPCION_VARIEDAD,p.descripcion_per AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM entradas_albaranes e JOIN proveedores_coop pr ON pr.codigo_proveedor = e.codigo_proveedor "
        SQL = Trim(SQL) + " JOIN campos_terceros c ON c.empresa = e.empresa AND c.ejercicio = e.ejercicio AND c.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
        SQL = Trim(SQL) + " LEFT JOIN personal_terceros oc ON oc.codigo = e.capataz_terc "
        SQL = Trim(SQL) + " LEFT JOIN personal_terceros ot ON ot.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " LEFT JOIN periodos_coop p ON p.empresa = e.empresa AND p.ejercicio = e.ejercicio AND p.codigo_variedad = e.codigo_variedad AND p.codigo_periodo = e.codigo_periodo "
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.serie_albaran = '" + Trim(SerieAlbaran) + "'  AND e.numero_albaran = " + CStr(NumeroAlbaran)


        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If Cadena = "codigo_socio" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!codigo_proveedor))
                ElseIf Cadena = "codigo_campo" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!campo_terceros))
                ElseIf Cadena = "capataz" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!capataz_terc))
                ElseIf Cadena = "transportista" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!Transportista_terc))
                Else
                    If IsDBNull(RsEntradas(i)) Then
                        oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                    Else
                        oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr("" & RsEntradas(i)))
                    End If
                End If
            Next
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_periodo))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim("" & RsEntradas!razon_social))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim("" & RsEntradas!NOMBRE_CAPATAZ))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))


            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then
                EnvasesEntrados = Strings.Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            End If
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            RsEnvases.Close()
            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'S'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesSalidos = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesSalidos = EnvasesSalidos + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesSalidos) > 2 Then EnvasesSalidos = Strings.Left(EnvasesSalidos, Len(EnvasesSalidos) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_salidos", 0, Trim(EnvasesSalidos))

            SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

            RsCalVar.Open(SQL, ObjetoGlobal.cn)
            For i = 1 To RsCalVar.RecordCount
                RsCalVar.AbsolutePosition = i
                oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (CInt("0" & RsCalVar!codigo_calidad) - 1), 0, Trim("" & RsCalVar!descripcion_docum))
            Next
            RsEntradas.Close()
            RsCalVar.Close()

        End If
    End Sub

    Public Sub VuelcaAlbaran3T(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer, SQL As String, Cadena As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String
        Dim EnvasesSalidos As String
        Dim porcentaje As Single
        Dim Calidad As Integer
        Dim Kilos As Long
        Dim nPorc_nc As Single
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        SQL = "SELECT e.*, pr.razon_social, oc.nombre AS NOMBRE_CAPATAZ,ot.nombre AS NOMBRE_TRANSPORTISTA,c.situacion_campo AS DESCRIPCION_SITUACION,v.descripcion AS DESCRIPCION_VARIEDAD,p.descripcion_per AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM entradas_albaranes e JOIN proveedores_coop pr ON pr.codigo_proveedor = e.codigo_proveedor "
        SQL = Trim(SQL) + " JOIN campos_terceros c ON c.empresa = e.empresa AND c.ejercicio = e.ejercicio AND c.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
        SQL = Trim(SQL) + " JOIN personal_terceros oc ON oc.codigo = e.capataz_terc "
        SQL = Trim(SQL) + " JOIN personal_terceros ot ON ot.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " JOIN periodos_coop p ON p.empresa = e.empresa AND p.ejercicio = e.ejercicio AND p.codigo_variedad = e.codigo_variedad AND p.codigo_periodo = e.codigo_periodo "
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.serie_albaran = '" + Trim(SerieAlbaran) + "'  AND e.numero_albaran = " + CStr(NumeroAlbaran)

        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If Cadena = "codigo_socio" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!codigo_proveedor))
                ElseIf Cadena = "codigo_campo" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!campo_terceros))
                ElseIf Cadena = "capataz" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!capataz_terc))
                ElseIf Cadena = "transportista" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!Transportista_terc))
                Else
                    If IsDBNull(RsEntradas(i)) Then
                        oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                    Else
                        oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr("" & RsEntradas(i)))
                    End If
                End If
            Next
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_periodo))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim("" & RsEntradas!razon_social))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim("" & RsEntradas!NOMBRE_CAPATAZ))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))


            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then EnvasesEntrados = Strings.Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            RsEnvases.Close()
            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'S'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesSalidos = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesSalidos = EnvasesSalidos + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesSalidos) > 2 Then EnvasesSalidos = Strings.Left(EnvasesSalidos, Len(EnvasesSalidos) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_salidos", 0, Trim(EnvasesSalidos))


            RsClasificacion.Open("SELECT ec.*, cv.descripcion_docum FROM ENTRADAS_CLASIF ec LEFT JOIN calidades_var_ej cv ON cv.empresa=ec.empresa AND cv.ejercicio = ec.ejercicio AND cv.codigo_variedad = ec.codigo_variedad AND cv.codigo_calidad=ec.codigo_calidad WHERE ec.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ec.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ec.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND ec.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ec.TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
            If RsClasificacion.RecordCount > 0 Then
                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Calidad = CInt("0" & RsClasificacion!codigo_calidad) - 1
                    Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
                    If Calidad >= 0 And Calidad <= 12 Then
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & CStr(Calidad), 0, Trim("" & RsClasificacion!descripcion_docum))
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.kilos_calidad_" & CStr(Calidad), 0, Format(Kilos, "###,##0"))
                        If RsEntradas!kg_entrada <> 0 Then
                            porcentaje = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_calidad_" & CStr(Calidad), 0, CStr(porcentaje))
                        End If
                        If Calidad = 5 Then
                            nPorc_nc = porcentaje
                        End If
                    End If
                Next
            Else
                SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

                RsCalVar.Open(SQL, ObjetoGlobal.cn)
                For i = 1 To RsCalVar.RecordCount
                    RsCalVar.AbsolutePosition = i
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (RsCalVar!codigo_calidad - 1), 0, Trim(RsCalVar!descripcion_docum))
                Next
                RsCalVar.Close()
            End If
            RsClasificacion.Close()

            'Ahora imprimimor la distrtibución del no comercial
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_defecto", 0, Format((RsEntradas!porcentaje_plaga + RsEntradas!porcentaje_recol), "#0.00"))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_tamano", 0, Format((RsEntradas!porcentaje_grand + RsEntradas!porcentaje_peq), "#0.00"))
            RsEntradas.Close()

        End If
    End Sub

    Public Sub VuelcaDesglosePlagasT(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long)
        Dim i As Integer
        Dim SQL As String
        Dim Cadena As String
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim EnvasesEntrados As String
        Dim EnvasesSalidos As String
        Dim MensajeEurep As String
        Dim porcentaje As Single
        Dim Calidad As Integer
        Dim Kilos As Long
        Dim nPorc_nc As Single
        Dim nTotalPlagas As Double
        Dim nTotalRecoleccion As Double
        Dim sSqlPlagas As String
        Dim RsCalVar As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        SQL = "SELECT e.*, pr.razon_social, oc.nombre AS NOMBRE_CAPATAZ,ot.nombre AS NOMBRE_TRANSPORTISTA,c.situacion_campo AS DESCRIPCION_SITUACION,v.descripcion AS DESCRIPCION_VARIEDAD,p.descripcion_per AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM entradas_albaranes e JOIN proveedores_coop pr ON pr.codigo_proveedor = e.codigo_proveedor "
        SQL = Trim(SQL) + " JOIN campos_terceros c ON c.empresa = e.empresa AND c.ejercicio = e.ejercicio AND c.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
        SQL = Trim(SQL) + " JOIN personal_terceros oc ON oc.codigo = e.capataz_terc "
        SQL = Trim(SQL) + " JOIN personal_terceros ot ON ot.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " JOIN periodos_coop p ON p.empresa = e.empresa AND p.ejercicio = e.ejercicio AND p.codigo_variedad = e.codigo_variedad AND p.codigo_periodo = e.codigo_periodo "
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.serie_albaran = '" + Trim(SerieAlbaran) + "'  AND e.numero_albaran = " + CStr(NumeroAlbaran)

        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then
            For i = 0 To 58
                Cadena = Trim(RsEntradas.NombreCampo(i))
                If Cadena = "codigo_socio" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!codigo_proveedor))
                ElseIf Cadena = "codigo_campo" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!campo_terceros))
                ElseIf Cadena = "capataz" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!capataz_terc))
                ElseIf Cadena = "transportista" Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, Trim("" & RsEntradas!Transportista_terc))
                Else
                    If IsDBNull(RsEntradas(i)) Then
                        oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, "")
                    Else
                        oImp.VolcarAImpresion(1, 0, 0, 0, "entradas." & Trim(Cadena), 0, CStr("" & RsEntradas(i)))
                    End If
                End If
            Next
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_situacion", 0, Trim("" & RsEntradas!descripcion_situacion))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_periodo", 0, Trim("" & RsEntradas!descripcion_periodo))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim("" & RsEntradas!razon_social))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.descripcion_variedad", 0, Trim("" & RsEntradas!descripcion_variedad))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_capataz", 0, Trim("" & RsEntradas!NOMBRE_CAPATAZ))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_transportista", 0, Trim("" & RsEntradas!nombre_transportista))

            MensajeEurep = ""

            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'E'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesEntrados = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesEntrados = EnvasesEntrados + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesEntrados) > 2 Then EnvasesEntrados = Strings.Left(EnvasesEntrados, Len(EnvasesEntrados) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_entrados", 0, Trim(EnvasesEntrados))
            RsEnvases.Close()
            SQL = "SELECT EE.* FROM ENTRADAS_ENVASES EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ENTRADA_SALIDA = 'S'"
            RsEnvases.Open(SQL, ObjetoGlobal.cn)
            EnvasesSalidos = ""
            For i = 1 To RsEnvases.RecordCount
                RsEnvases.AbsolutePosition = i
                EnvasesSalidos = EnvasesSalidos + Trim(RsEnvases!codigo_envase) + " = " + Str(RsEnvases!numero_envases) + "; "
            Next
            If Len(EnvasesSalidos) > 2 Then EnvasesSalidos = Strings.Left(EnvasesSalidos, Len(EnvasesSalidos) - 2)
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.envases_salidos", 0, Trim(EnvasesSalidos))

            RsClasificacion.Open("SELECT ec.*, cv.descripcion_docum FROM ENTRADAS_CLASIF ec LEFT JOIN calidades_var_ej cv ON cv.empresa=ec.empresa AND cv.ejercicio = ec.ejercicio AND cv.codigo_variedad = ec.codigo_variedad  AND cv.codigo_calidad=ec.codigo_calidad WHERE ec.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ec.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ec.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND ec.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " AND ec.TIPO_CLASIFICACION = 'LIQ' ORDER BY 1,2,3,4,5,6", ObjetoGlobal.cn)
            If RsClasificacion.RecordCount > 0 Then
                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Calidad = RsClasificacion!codigo_calidad - 1
                    Kilos = Math.Round(RsClasificacion!kg_calidad, 0)
                    'If Calidad >= 0 And Calidad <= 7 Then
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & CStr(Calidad), 0, Trim(RsClasificacion!descripcion_docum))
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.kilos_calidad_" & CStr(Calidad), 0, Format(Kilos, "###,##0"))
                    If RsEntradas!kg_entrada <> 0 Then
                        porcentaje = Math.Round(Kilos * 100 / RsEntradas!kg_entrada, 2)
                        oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_calidad_" & CStr(Calidad), 0, CStr(porcentaje))
                    End If
                    If Calidad = 5 Then
                        nPorc_nc = porcentaje
                    End If
                    'End If
                Next
            Else
                SQL = "SELECT * FROM calidades_var_ej WHERE Empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND codigo_variedad ='" & RsEntradas!codigo_variedad & "' ORDER BY  1,2,3,4"

                RsCalVar.Open(SQL, ObjetoGlobal.cn)
                For i = 1 To RsCalVar.RecordCount
                    RsCalVar.AbsolutePosition = i
                    oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.lbl_calidad_" & (RsCalVar!codigo_calidad - 1), 0, Trim(RsCalVar!descripcion_docum))
                Next
                RsCalVar.Close()
            End If
            RsClasificacion.Close()

            ' Ahora imprimimos la distrtibución del no comercial
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_defecto", 0, Format((RsEntradas!porcentaje_plaga + IIf(IsDBNull(RsEntradas!porcentaje_recol), 0, RsEntradas!porcentaje_recol)), "#0.00"))
            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_nc_tamano", 0, Format((IIf(IsDBNull(RsEntradas!porcentaje_grand), 0, RsEntradas!porcentaje_grand) + IIf(IsDBNull(RsEntradas!porcentaje_peq), 0, RsEntradas!porcentaje_peq)), "#0.00"))

            sSqlPlagas = "SELECT e.codigo_plaga, e.porcentaje, p.descripcion, e.tipo_plaga FROM entradas_plagas e LEFT JOIN plagas p ON p.empresa =e.empresa AND p.codigo_plaga= e.codigo_plaga "
            sSqlPlagas = sSqlPlagas + "WHERE e.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND e.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) & " ORDER BY e.codigo_plaga"
            RsPlagas.Open(sSqlPlagas, ObjetoGlobal.cn)
            nTotalPlagas = 0
            While Not RsPlagas.EOF
                If RsPlagas!tipo_plaga = "D" Then
                    nTotalPlagas = nTotalPlagas + RsPlagas!porcentaje
                Else
                    nTotalRecoleccion = nTotalRecoleccion + RsPlagas!porcentaje
                End If
                RsPlagas.MoveNext()
            End While
            If RsPlagas.RecordCount > 0 Then
                RsPlagas.MoveFirst()
                i = 0
                If nTotalPlagas <> 0 Then
                    While Not RsPlagas.EOF
                        i = i + 1
                        If RsPlagas!tipo_plaga = "D" Then
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.texto_plaga" & i, 0, Trim(RsPlagas!Descripcion))
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_plaga" & i, 0, Format(Math.Round(((RsEntradas!porcentaje_plaga / nTotalPlagas) * RsPlagas!porcentaje), 2), "#0.00"))
                        ElseIf RsPlagas!tipo_plaga = "R" Then
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.texto_plaga" & i, 0, Trim(RsPlagas!Descripcion))
                            oImp.VolcarAImpresion(1, 0, 0, 0, "calculado.porcentaje_plaga" & i, 0, Format(Math.Round(((RsEntradas!porcentaje_recol / nTotalRecoleccion) * RsPlagas!porcentaje), 2), "#0.00"))
                        Else
                            MsgBox("Tipo de plaga no reconocido")
                        End If
                        RsPlagas.MoveNext()
                    End While
                End If
            End If
            RsPlagas.Close()
            RsEntradas.Close()
        End If
    End Sub

    Public Function VuelcaEtiquetasT(oImp As Object, SerieAlbaran As String, NumeroAlbaran As Long) As Boolean
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEtiquetas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Integer
        Dim j As Integer
        Dim Cadena As String
        Dim NumeroEtiqueta As Integer
        Dim NumeroDocumento As Integer
        Dim CajasExtra As Integer
        Dim CodigoMuestra As String

        VuelcaEtiquetasT = False

        SQL = "SELECT e.*, pr.razon_social, oc.nombre AS NOMBRE_CAPATAZ,ot.nombre AS NOMBRE_TRANSPORTISTA,c.situacion_campo AS DESCRIPCION_SITUACION,v.descripcion AS DESCRIPCION_VARIEDAD,p.descripcion_per AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM entradas_albaranes e JOIN proveedores_coop pr ON pr.codigo_proveedor = e.codigo_proveedor "
        SQL = Trim(SQL) + " JOIN campos_terceros c ON c.empresa = e.empresa AND c.ejercicio = e.ejercicio AND c.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
        SQL = Trim(SQL) + " JOIN personal_terceros oc ON oc.codigo = e.capataz_terc "
        SQL = Trim(SQL) + " JOIN personal_terceros ot ON ot.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " JOIN periodos_coop p ON p.empresa = e.empresa AND p.ejercicio = e.ejercicio AND p.codigo_variedad = e.codigo_variedad AND p.codigo_periodo = e.codigo_periodo "
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND e.serie_albaran = '" + Trim(SerieAlbaran) + "'  AND e.numero_albaran = " + CStr(NumeroAlbaran)
        RsEntradas.Open(SQL, ObjetoGlobal.cn)
        If RsEntradas.RecordCount > 0 Then

            Cadena = ""

            SQL = "SELECT * FROM ENTRADAS_BULTOS EE WHERE EE.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EE.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND EE.SERIE_ALBARAN = '" + Trim(SerieAlbaran) + "'  AND EE.NUMERO_ALBARAN = " + CStr(NumeroAlbaran) + " order by 1,2,3,4,5"
            RsEtiquetas.Open(SQL, ObjetoGlobal.cn)
            CuantasEtiquetas = 0
            For i = 1 To RsEtiquetas.RecordCount
                RsEtiquetas.AbsolutePosition = i
                CuantasEtiquetas = CuantasEtiquetas + 1
                If RsEtiquetas!Bulto < 100 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = Trim(Cadena)
                    If IsDBNull(RsEtiquetas!codigo_envase) Then
                        If Strings.Left(RsEntradas!codigo_variedad, 2) = "40" Then
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   PALOT "
                        Else
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   CAJAS: " + CStr(RsEtiquetas!Cajas)
                        End If
                    Else
                        If Trim(RsEtiquetas!codigo_envase) = "PALET" Then
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   CAJAS: " + CStr(RsEtiquetas!Cajas)
                        Else
                            TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + Format(RsEtiquetas!Bulto, "00") + "   PALOT " '+ CStr(RsEtiquetas!Cajas)
                        End If
                    End If
                    TextoEtiquetas(CuantasEtiquetas, 2) = Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy")
                    If Strings.Left(RsEntradas!codigo_variedad, 2) = "14" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "17" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "09" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "08" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "46" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "48" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 2) = TextoEtiquetas(CuantasEtiquetas, 2) + " " + CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 2) = TextoEtiquetas(CuantasEtiquetas, 2) + " " + Trim(RsEtiquetas!Referencia)
                    End If
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Strings.Left(RsEntradas!codigo_variedad, 2) = "14" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "17" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "09" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "08" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "46" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "48" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If
                ElseIf RsEtiquetas!Bulto < 110 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = ""
                    TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy")
                    TextoEtiquetas(CuantasEtiquetas, 2) = "CLASIFICACION " + Trim(RsEtiquetas!Referencia)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Strings.Left(RsEntradas!codigo_variedad, 2) = "14" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "17" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "09" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "08" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "46" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "48" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If

                ElseIf RsEtiquetas!Bulto < 120 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = ""
                    TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy")
                    TextoEtiquetas(CuantasEtiquetas, 2) = "RECLAMACION " + Trim(RsEtiquetas!Referencia)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Strings.Left(RsEntradas!codigo_variedad, 2) = "14" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "17" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "09" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "08" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "46" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "48" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If

                ElseIf RsEtiquetas!Bulto < 130 Then
                    TextoEtiquetas(CuantasEtiquetas, 0) = ""
                    TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy") + " " + CStr(RsEntradas!capataz_terc)
                    TextoEtiquetas(CuantasEtiquetas, 2) = "POST-RECOL. " + Trim(RsEtiquetas!Referencia)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    If Strings.Left(RsEntradas!codigo_variedad, 2) = "14" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "17" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "09" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "08" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "46" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "48" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "03" Then
                        TextoEtiquetas(CuantasEtiquetas, 4) = CStr(RsEtiquetas!codigo_barras)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 4) = Trim(RsEtiquetas!Referencia)
                    End If
                End If
            Next

            If Strings.Left(RsEntradas!codigo_variedad, 2) = "14" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "17" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "09" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "08" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "46" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "48" Or Strings.Left(RsEntradas!codigo_variedad, 2) = "03" Then
                CajasExtra = 6 * Fix(1 + (RsEtiquetas.RecordCount - 0.1) / 6)
                For i = 1 To 2
                    CuantasEtiquetas = CuantasEtiquetas + 1
                    If i = 1 Then
                        TextoEtiquetas(CuantasEtiquetas, 0) = "MUESTRA"
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 0) = "VOLCADO"
                    End If
                    If Trim(RsEntradas!Tipo_entrada) = "T" Then
                        TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy") + " " + CStr(RsEntradas!capataz_terc)
                    Else
                        TextoEtiquetas(CuantasEtiquetas, 1) = Format(RsEtiquetas!numero_albaran, "00000") + " " + Format(CDate(RsEntradas!fecha_entrada), "dd/mm/yyyy") + " " + CStr(RsEntradas!Capataz)
                    End If
                    CodigoMuestra = CStr(7 + i) + Format(RsEtiquetas!numero_albaran, "00000")
                    TextoEtiquetas(CuantasEtiquetas, 2) = Trim(CodigoMuestra) + " CAJAS:" + CStr(CajasExtra)
                    TextoEtiquetas(CuantasEtiquetas, 3) = Trim(RsEntradas!descripcion_variedad)
                    TextoEtiquetas(CuantasEtiquetas, 4) = CodigoMuestra
                Next
            End If

            RsEntradas.Close()
            RsEtiquetas.Close()
            NumeroEtiqueta = 0
            NumeroDocumento = 0
            For i = 1 To CuantasEtiquetas
                NumeroEtiqueta = NumeroEtiqueta + 1
                If NumeroEtiqueta Mod 16 = 1 Then
                    NumeroEtiqueta = 1
                    NumeroDocumento = NumeroDocumento + 1
                End If
                For j = 0 To 3
                    If Trim("" & TextoEtiquetas(NumeroEtiqueta, j)) > "" Then oImpEtiquetas.VolcarAImpresion(CLng(NumeroDocumento), 0, 0, 0, "calculado.texto_" + Format(NumeroEtiqueta, "00") + "_" + CStr(j), 0, Trim("" & TextoEtiquetas(i, j)))
                Next
                If Trim("" & TextoEtiquetas(NumeroEtiqueta, j)) > "" Then oImpEtiquetas.VolcarAImpresion(CLng(NumeroDocumento), 0, 0, 0, "calculado.cb39_" + Format(NumeroEtiqueta, "00") + "_1", 0, Trim("" & TextoEtiquetas(i, 4)))
            Next
            VuelcaEtiquetasT = True
        End If
    End Function

    Public Function DevuelveControl(ByRef oFrm As Form, ByVal nombrecontrol As String, Optional ByRef oj As Control = Nothing) As Object

        If IsNothing(oj) Then
            For Each control As Control In oFrm.Controls
                If TypeOf control Is TextBox Then
                    If UCase(control.Name).Trim = UCase(nombrecontrol.Trim) Then
                        Return control
                    End If
                ElseIf TypeOf control Is Label Then
                    If UCase(control.Name).Trim = UCase(nombrecontrol.Trim) Then
                        Return control
                    End If
                ElseIf TypeOf control Is picturebox Then
                    If UCase(control.Name).Trim = UCase(nombrecontrol.Trim) Then
                        Return control
                    End If
                End If
            Next
        Else
            For Each control As Control In oj.Controls
                If TypeOf control Is TextBox Then
                    If UCase(control.Name).Trim = UCase(nombrecontrol.Trim) Then
                        Return control
                    End If
                ElseIf TypeOf control Is Label Then
                    If UCase(control.Name).Trim = UCase(nombrecontrol.Trim) Then
                        Return control
                    End If
                ElseIf TypeOf control Is picturebox Then
                    If UCase(control.Name).Trim = UCase(nombrecontrol.Trim) Then
                        Return control
                    End If
                End If
            Next
        End If

        Return Nothing

    End Function

    Public Sub ImprimeComunicacion(ByVal Socio As Long)
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim proceso As String
        Dim formato As String
        Dim oImp As ReportBuilder2.ClsImpresion

        oImp = New ReportBuilder2.ClsImpresion(ObjetoGlobal)

        formato = "FormatoLCA"
        proceso = "Contratos fruisecs"

        If HayImpresoraEtiquetas Then
            If Not oImp.Inicializar(proceso, "Contrato_almendras", formato, 2, False, ObjetoGlobal.cn, "", "", "", "", "Etiquetas") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Return
            End If
        Else
            If Not oImp.Inicializar(proceso, "Contrato_almendras", formato, 2, False, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No se encuentra el formato de edición indicado.")
                Return
            End If
        End If

        VuelcaComunicacion(oImp, Socio)
        oImp.Imprimir()
    End Sub

    Public Function VuelcaComunicacion(CINF As Object, Socio As Long) As Boolean
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim meses(12) As String
        Dim Fecha As Date

        Fecha = CDate(DateTime.Now)

        meses(1) = "Enero"
        meses(2) = "Febrero"
        meses(3) = "Marzo"
        meses(4) = "Abril"
        meses(5) = "Mayo"
        meses(6) = "Junio"
        meses(7) = "Julio"
        meses(8) = "Agosto"
        meses(9) = "Septiembre"
        meses(10) = "Octubre"
        meses(11) = "Noviembre"
        meses(12) = "Diciembre"

        Sql = "SELECT * from socios_coop WHERE codigo_socio =" & Socio
        Rs.Open(Sql, ObjetoGlobal.cn)

        If Rs.RecordCount > 0 Then

            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.nombre_socio", 0, Trim(Trim("" & Rs!nombre_socio) & " " & Rs!Apellidos_socio))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.direccion", 0, Trim("" & Rs!domicilio_socio))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.num", 0, Trim(Trim("" & Rs!Numero) & " " & Trim("" & Rs!puerta)))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.localidad", 0, Trim("" & Rs!poblacion))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.nif", 0, Trim("" & Rs!nif_socio))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.num_socio", 0, Trim("" & Rs!codigo_socio))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.dia", 0, Trim("" & Fecha.Day))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.mes", 0, Trim("" & meses(Month(Fecha))))
            CINF.VolcarAImpresion(1, 0, 0, 0, "calculado.año", 0, Trim("" & Year(Fecha)))
        End If
        Return True

    End Function

End Module
