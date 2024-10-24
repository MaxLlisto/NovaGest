Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar

Public Class SEPA34
    Public ObjetoGlobal As Object
    Public DiscAbonos() As String
    Public cuenta As String
    Public NIF As String
    Public nombre As String
    Public domicilio As String
    Public Poblacion As String
    Public entidad As String
    Public oficina As String
    Public dc As String
    Public codcuenta As String
    Public Path As String
    Public Cuantos As Long
    Public NumDisc As Integer
    Public ArchivoEnCurso As Integer
    Public lvAbonos As Object
    Public FechaRemesa As Date
    Public FechaAbono As Date
    Public lbl As Object
    Public trans As SqlClient.SqlTransaction
    Public PB As Object
    Dim HayErrores As Boolean
    Dim fecha_hora As String
    Dim numero_orden As Integer
    Dim CuantosDisco(100) As Integer
    Dim EntradasDisco(100) As Integer
    Dim EntradasDiscoOper(100) As Integer
    Dim ImporteDisco(100) As Double
    Private Const CAB3ITR = "<CstmrCdtTrfInitn>"  '  Inicio cabecera general
    Private Const CAB3FTR = "</CstmrCdtTrfInitn>"
    Private Const CABIP10iTR = "<ReqdExctnDt>"
    Private Const CABIP10fTR = "</ReqdExctnDt>"
    Private Const CABIP17iTr = "<DbtrAcct>"  '  Inicio de cuenta Identificación IBAN + moneda                                                           2.20
    Private Const CABIP17fTr = "</DbtrAcct>"
    Private Const CABIP11itr = "<Dbtr>"  '  Inicio cabecera general
    Private Const CABIP11ftr = "</Dbtr>"
    Private Const CABIP20ftr = "</BIC></FinInstnId></DbtrAgt>"
    Private Const RECI01iTr = "<CdtTrfTxInf>"  '  Cabecera por recibo emitido                                                                          2.28
    Private Const RECI01fTr = "</CdtTrfTxInf>"  '  Cabecera por recibo emitido                                                                         2.28
    Private Const CABIP7iTr = "<SvcLvl><Cd>SEPA</Cd></SvcLvl>"  '                                    2.8 a 2.12
    Private Const REC242i = "<Amt>"  '                                   2.42
    Private Const REC242f = "</Amt>"  '                                      2.42
    Private Const RECI11itr = "<CdtrAgt><FinInstnId><BIC>"   '  Entidad del deudor, identificación Ej. <DbtrAgt><FinInstnId><BIC>ESPCESMMXXX</BIC></FinInstnId></DbtrAgt> 2.70
    Private Const RECI11ftr = "</BIC></FinInstnId></CdtrAgt>"
    Private Const RECI12itr = "<Cdtr>"                        '  Deudor                                                      2.72
    Private Const RECI12ftr = "</Cdtr>"                       '  Deudor
    Private Const RECI18itr = "<CdtrAcct>"                        '  Cuenta del deudor                                        2.73
    Private Const RECI18ftr = "</CdtrAcct>"
    Private Const RECI19i = "<Id><IBAN>"     '                                                                          2.73
    Private Const RECI19f = "</IBAN></Id>"
    Private Const CABIP18i = "<Id><IBAN>"  '  Ejemplo <Id><IBAN>ES8831237271112710000186</IBAN></Id>                                                  2.21
    Private Const CABIP18f = "</IBAN></Id>"
    Private Const CABIP21if = "<ChrgBr>SLEV</ChrgBr>"  '   Cláusula de gastos – ChargeBearer (solo tiene un valor posible)                             2.24
    Private Const CABIP19if = "<Ccy>EUR</Ccy>"  '  Moneda euros 2.20
    Private Const RECI14i = "<Nm>"                        '  Deudor                                                      2.72
    Private Const RECI14f = "</Nm>"                       '  Deudor
    Private Const RECI02i = "<PmtId>"  '  Identificación del pago 2.29
    Private Const RECI02f = "</PmtId>"  '  Identificación del pago                                                                                    2.29
    Private Const RECI03i = "<InstrId>"  '  Identificación de la instrucción(1-35) Ej. <InstrId>20151125122308-0001</InstrId>
    Private Const RECI03f = "</InstrId>"      '                                                                                                         2.30
    Private Const RECI04i = "<EndToEndId>"  '  dentificación de extremo a extremo(1-35) Ej. <EndToEndId>7045299</EndToEndId>                          2.31
    Private Const RECI04f = "</EndToEndId>"  '  Identificación única asignada por la parte iniciadora para identificar inequívocamente cada operación (AT-10). Esta referencia se transmite de extremo a extremo, sin cambios, a lo largo de toda la cadena de pago.
    Private Const CABIP12i = "<Nm>"  '  Nombre                                                                                                        2.19
    Private Const CABIP12f = "</Nm>"  '  Nombre                                                                                                        2.19
    Private Const CABIP14i = "<PstlAdr>"  '  Dirección postal (País + Dirección en texto libre)                                                       2.19
    Private Const CABIP14f = "</PstlAdr>"  '  Dirección postal (País + Dirección en texto libre)                                                       2.19
    Private Const CABIP15i = "<Ctry>"  '  País Ej. <Ctry>ES</Ctry>                                                                                    2.19
    Private Const CABIP15f = "</Ctry>"  '  País Ej. <Ctry>ES</Ctry>                                                                                    2.19
    Private Const CABIP16i = "<AdrLine>"  '  Dirección en una línea <AdrLine>VALENCIA TURIS VALENCIA</AdrLine>                                        2.19
    Private Const CABIP16f = "</AdrLine>"
    Private Const CABIP1f = "</PmtInfId>"  '  Identificación de la información del pago (35) Ej. <PmtInfId>ES64000F97224828-201511251223080000</PmtInfId> 2.1
    Private Const CABIP2f = "</PmtMtd>"  '  Método de pago(2) Ej. <PmtMtd>DD</PmtMtd>                                                                     2.2
    Private Const RECI05i = "<InstdAmt Ccy=""EUR"">" '  Importe ordenado(del recibo Máx. 11 dígitos 2 dec . sep.decimal) Ej. <InstdAmt Ccy="EUR">2.70</InstdAmt> 2.44
    Private Const RECI05f = "</InstdAmt>"
    Private Const CABP1I = "<MsgId>"  '  Identificación del mensaje Ej. <MsgId>PRE20151125122258559721357924680124</MsgId>                          1.1
    Private Const CABP1f = "</MsgId>"
    Private Const CABIP6f = "</PmtTpInf>"  '  Información del tipo de pago                                                                                2.6
    Private Const CABPI = "<GrpHdr>"   '  Cabecera grupo presentador                                                 1.0
    Private Const CABIPf = "</PmtInf>"
    Private Const RECI20i = "<RmtInf>"    '       '                                                                         2.88
    Private Const RECI20f = "</RmtInf>"
    Private Const RECI21i = "<Ustrd>"    '                                                                                   2.89
    Private Const RECI21f = "</Ustrd>"
    Private Const CAB2F = "</Document>"
    Private Const CABP2i = "<CreDtTm>"  '  Fecha hora de creación Ej. <CreDtTm>2015-11-25T12:22:58</CreDtTm>        1.1
    Private Const CABP2f = "</CreDtTm>"
    Private Const CABP4i = "<CtrlSum>"  '  Control de suma Ej. <CtrlSum>45185.05</CtrlSum>                          1.7
    Private Const CABP3i = "<NbOfTxs>"  '  Numero de operaciones Ej. <NbOfTxs>451</NbOfTxs>                         1.6
    Private Const CABP3f = "</NbOfTxs>"
    Private Const CAB1 = "<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>"
    Private Const CABA2i = "<Nm>"  '  Nombre del acreedor Ej. <Nm>AUGUIS DE TURIS</Nm>                              1.8
    Private Const CABA3i = "<Id><OrgId><Othr><Id>"  '  Identificador acreedor Ej. <Id><OrgId><Othr><Id>ES64000F97224828</Id></Othr></OrgId></Id> NOTA: Para persona jutídica  1.8
    Private Const CABA3f = "</Id></Othr></OrgId></Id>"
    '  Se cierra con  CABA1f+CABPF </InitgPty></GrpHdr>
    '  Información del pago
    Private Const CABIPi = "<PmtInf>"    '                                                                          2.0
    Private Const CABIP1i = "<PmtInfId>"  '  Identificación de la información del pago (35) Ej. <PmtInfId>ES64000F97224828-201511251223080000</PmtInfId> 2.1
    Private Const CABIP2i = "<PmtMtd>"  '  Método de pago(2) Ej. <PmtMtd>DD</PmtMtd>                                                                     2.2
    Private Const CABIP3i = "<BtchBookg>"  '   Indicador de apunte en cuenta(5) Ej. <BtchBookg>TRUE</BtchBookg>                                          2.3
    Private Const CABIP4i = "<NbOfTxs>"  '  Número de operaciones                                                                                        2.4
    Private Const CABIP5i = "<CtrlSum>"  '  Control de suma suma de importes individuales                                                                2.5
    Private Const CABIP6i = "<PmtTpInf>"  '  Información del tipo de pago                                                                                2.6
    Private Const CABA1i = "<InitgPty>"  '  Inicio cabecera acreedor                                                1.8
    Private Const CABA1f = "</InitgPty>"  '  Inicio cabecera acreedor                                                1.8
    Private Const CABPF = "</GrpHdr>"  '  Cabecera grupo presentador
    Private Const CABP4f = "</CtrlSum>"
    Private Const CABA2f = "</Nm>"  '  Nombre del acreedor Ej. <Nm>AUGUIS DE TURIS</Nm>                              1.8
    Private Const CABIP3f = "</BtchBookg>"  '   Indicador de apunte en cuenta(5) Ej. <BtchBookg>TRUE</BtchBookg>                                          2.3
    Private Const CABIP4f = "</NbOfTxs>"  '  Número de operaciones                                                                                        2.4
    Private Const CABIP5f = "</CtrlSum>"  '  Control de suma suma de importes individuales                                                                2.5
    Private Const CAB2I = "<Document xmlns=""urn:iso:std:iso:20022:tech:xsd:pain.001.001.03"">"
    Private Const CABIP20itr = "<DbtrAgt><FinInstnId><BIC>"
    Private Const CABIP20i = "<DbtrAgt><FinInstnId><BIC>"  '  Entidad de crédito donde el acreedor mantiene su cuenta Ej. <CdtrAgt><FinInstnId><BIC>NOTPROVIDED</BIC></FinInstnId></CdtrAgt>  2.21
    'PRIVATE CONST CABIP20f = "</BIC></FinInstnId></CdtrAgt>"

    ' 'INHERIT N19XML
    Dim Ident_pago As String '= "TRF"  AS STRING
    Dim Cod_banco As String
    'Public FechaAbono  As Date
    Dim cadena As String
    Dim Fecha_envio As Date
    Dim Hora_envio As String
    Dim aTiposDePago() As String
    Dim aRecibosTPag() As String
    Dim aImportesTPa() As Double
    Dim aRecTiposDePago() As String
    Dim IbanBan As String
    Dim cIdentificadorPr As String
    Dim BICPresentador As String
    'Dim oTB  As FicheroTexto
    Dim cTipoRemesa As String '= "PRE" AS STRING  '  Pueder ser PRE lo normal o puede ser FSDD
    Dim cModalidad As String '= "COR1" AS STRING
    'Public FechaRemesa As Date
    Dim lErrorEnRemesa As Boolean
    Dim aRem() As String
    Dim aErrores() As String
    Dim CINE As String
    'Dim oDialog     As TraspasoDisco
    Dim cSufOrd As String
    Dim NumRemesa As Double
    Dim xBanIde As String
    Dim xDcrut As String
    Dim cSufOrd34 As String
    Dim cRutaArchivo As String
    Public Function DiscoAbonos(Arg() As String)
        DiscAbonos = Arg
    End Function

    Public Function HacerRemesa()
        Dim num_recibos As Integer '= 0 AS Integer
        Dim imp_recibos As Double ' = 0.00 AS DOUBLE
        Dim i As Integer
        Dim aPresentador(4) As String
        Dim nReci As Long
        Dim no_recibo As Long
        Dim Bic_recibo As String
        Dim cIban As String
        Dim DetPago As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim rs34 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Nombre2X As String
        'Dim rs As cnRecordset.cnRecordset = New cnRecordset.cnRecordset
        Dim Fichero As Integer
        Dim CodigoDestinatario1 As String
        Dim nombre1 As String
        Dim importe1 As Double
        Dim RefOrdenante As String

        ' Obtendrá los totales de cada disco
        ObtenTotales()
        Ident_pago = "TRF" ' Ident_pago = "CHK" en el caso de cheques

        ' Cabeceras
        SQL = "SELECT * FROM auxiliar_soporte WHERE empresa='" & ObjetoGlobal.empresaactual & "' AND codigo_norma = 34 AND cta_empresa = '" & cuenta & "'"
        Rs.Open(SQL, ObjetoGlobal.cn, ,,,,,, trans)
        If Not Rs.EOF Then
            For Fichero = 1 To NumDisc
                ArchivoEnCurso = Fichero
                Nombre2X = Trim(Mid(DiscAbonos(Fichero), 5))
                rs34.Open("SELECT * FROM sufijos_cuaderno34 WHERE concepto = '" & Nombre2X & "' AND codigo_empresa='" & ObjetoGlobal.empresaactual & "'", ObjetoGlobal.cn,,,,,,, trans)
                If rs34.RecordCount = 0 Then
                    rs34.Close()
                    rs34.Open("SELECT * FROM sufijos_cuaderno34 WHERE concepto = '" & Nombre2X & "'", ObjetoGlobal.cn,,,,,,, trans)
                    If rs34.RecordCount = 0 Then
                        MsgBox("Concepto " & Trim(Nombre2X) & " no incluido en la tabla de sufijos")
                        rs34.Close()
                        Exit Function
                    End If
                End If

                cSufOrd34 = rs34!sufijo
                cIdentificadorPr = CalculoDelIdentificador("ES", NIF, cSufOrd34)

                'PREPARACION DE DATOS
                CabeceraRemesa(aPresentador)
                num_recibos = CuantosDisco(Fichero)
                imp_recibos = ImporteDisco(Fichero)

                InicioDocumento()
                Cabecera1()
                CabeceraPresentador()
                Mensaje()
                HoraEnvio()
                NumeroOperacionesC1(num_recibos) 'aaa
                ImporteOperacionesC1(imp_recibos)
                ParteIniciadora(aPresentador(1), aPresentador(2) + cSufOrd34)

                IdPago(IdentificadorPago, Ident_pago, "un") ', num_recibos, imp_recibos '  Un identificador del pago Max35Str por bloque se pago
                NumeroOperacionesC2(num_recibos) 'aaa
                ImporteOperacionesC2(imp_recibos)

                TipoDePago()
                FechaDePago(FechaAbono)  '  Fecha de cobro de la remesa
                IdentificacionOrdenante(Trim(Left(aPresentador(1), 70)), Trim(Left(aPresentador(3), 70)), aPresentador(2) + cSufOrd34)
                CuentadeCargo(aPresentador(4)) '  Cuenta donde se abonará la remesa
                BicAcreedor(BICPresentador)
                GrabarArchivo()
                rs34.Close()
            Next
        End If
        Rs.Close()
        ' El detalle

        For Fichero = 1 To NumDisc
            ArchivoEnCurso = Fichero
            For Fila = 1 To Cuantos
                If Trim(DiscAbonos(Fichero)) = Format(CInt(lvAbonos.Items.Item(Fila).subitems(13).text), "0000") + Trim(lvAbonos.Items.Item(Fila).subitems(14).text) Then
                    Rs.Open("SELECT * FROM Bancos WHERE codigo_banco = '" & Right("0000" + Trim(lvAbonos.Items.Item(Fila).subitems(4).text), 4) & "'", ObjetoGlobal.cn,,,,,,, trans)
                    If Not Rs.EOF Then
                        Bic_recibo = Rs!bic
                    Else
                        Bic_recibo = Space(11)
                    End If
                    Rs.Close()

                    If PB.Value < PB.Max Then
                        PB.Value = PB.Value + 1
                        PB.Refresh
                    End If

                    CodigoDestinatario1 = Right("000000000000" + lvAbonos.Items.Item(Fila).subitems(0).text, 14)
                    nombre1 = ETxt(Trim(lvAbonos.Items.Item(Fila).subitems(1).text))
                    IbanBan = Trim(lvAbonos.Items.Item(Fila).subitems(2).text) + Right("00" + Trim(lvAbonos.Items.Item(Fila).subitems(3).text), 2) + Right("0000" + Trim(lvAbonos.Items.Item(Fila).subitems(4).text), 4) + Right("0000" + Trim(lvAbonos.Items.Item(Fila).subitems(5).text), 4) + Right("00" + Trim(lvAbonos.Items.Item(Fila).subitems(6).text), 2) + Right("0000000000" + Trim(lvAbonos.Items.Item(Fila).subitems(7).text), 10)
                    'If CalculaIban Then
                    '   IbanBan = IBANCalculo("ES", Right("0000" + Trim(LvAbonos.TextMatrix(i, 4)), 4) + Right("0000" + Trim(LvAbonos.TextMatrix(i, 5)), 4) + Right("00" + Trim(LvAbonos.TextMatrix(i, 6)), 2) + Right("0000000000" + Trim(LvAbonos.TextMatrix(i, 7)), 10))
                    'End If
                    importe1 = Math.Round(CDbl(lvAbonos.Items.Item(Fila).subitems(8).text), 2)
                    DetPago = ETxt(Trim(lvAbonos.Items.Item(Fila).subitems(9).text) + Trim(lvAbonos.Items.Item(Fila).subitems(10).text))
                    RefOrdenante = Format(CLng(Trim(lvAbonos.Items.Item(Fila).subitems(33).text) * 100000) + CLng(Trim(lvAbonos.Items.Item(Fila).subitems(34).text)), "000000000000")
                    If Not IBANValidacion(IbanBan) Then
                        HayErrores = True
                    End If
                    IdentificacionDelPago(RefOrdenante) 'no_recibo
                    ImporteRecibo(importe1)
                    BICRecibo(Bic_recibo)
                    DatosAcreedor(nombre1, "")
                    CuentaAcreedor(IbanBan)
                    DetallePago(DetPago) '  Línea única para detalle del pago y observaciones
                    GrabarArchivo()
                End If
            Next
        Next

        For Fichero = 1 To NumDisc
            ArchivoEnCurso = Fichero
            FinIdentificacionPago()    '  Fin del bloque de pago
            FinCabecera1()
            FinDocumento()
            GrabarArchivo()
            CerrarArchivo()
        Next

        HacerRemesa = True
    End Function

    Private Function IdentificadorFichero(cArg) As String
        IdentificadorFichero = cArg + dTosSeg(Date.Now) + "1357924680124"
    End Function
    Private Function TimeGetTime()
        Return DateTime.Now.Millisecond
    End Function
    Private Function dTosSeg(Fecha) As String
        Dim Seg As String
        ' Devuelve la fecha en formato aaaammddhhmmssmmmmm año mes dia hora minuto segundo y 5 milisegundos
        Seg = "00000" & Math.Abs(TimeGetTime())
        Seg = Replace(Seg, "-", "")
        dTosSeg = Year(Fecha) & Right("00" & Month(Fecha), 2) & Right("00" & DateAndTime.Day(Fecha), 2) & Mid(Format(Time(), "HH:mm:ss"), 1, 2) & Mid(Format(Time(), "HH:mm:ss"), 4, 2) & Mid(Format(Time(), "HH:mm:ss"), 7, 2) & Format(CLng(Right(Seg, 5)), "00000")

    End Function

    Private Function Mensaje() As String
        Dim cRet As String
        Dim id As String
        Dim IdTipo As String

        IdTipo = "HO" + Cod_banco + "/"
        id = IdentificadorFichero(IdTipo)
        cRet = CABP1I + id + CABP1f
        cadena = cadena + cRet

        Mensaje = cRet
    End Function


    Public Function ERRORES() As String()
        ERRORES = aErrores
    End Function


    Public Function ErrorEnDatos() As Boolean
        ErrorEnDatos = lErrorEnRemesa
    End Function

    Private Function Cabecera1() As String
        Dim cRet As String
        cRet = CAB3ITR
        cadena = cadena + cRet
        Cabecera1 = cRet
    End Function

    Private Function FinCabecera1() As String
        Dim cRet As String
        cRet = CAB3FTR
        cadena = cadena + cRet

        FinCabecera1 = cRet
    End Function

    Private Function IdPago(ByVal Cod As String, Optional MetodoPago As String = "TRF", Optional TipoApunte As String = "un") As String '  2.1 y 2.2
        Dim cRet As String
        'aaa
        cRet = IdentificacionPago()
        cRet = cRet + (CABIP1i + Left(Cod, 35) + CABIP1f)
        cRet = cRet + (CABIP2i + MetodoPago + CABIP2f)
        cRet = cRet + TipoApuntes(TipoApunte)

        cadena = cadena + cRet
        IdPago = cRet
    End Function

    Private Function FechaDePago(fec) As String  '  2.18
        Dim cRet As String

        cRet = CABIP10iTR
        cRet = cRet + FormatoFecha(fec)
        cRet = cRet + CABIP10fTR

        cadena = cadena + cRet
        FechaDePago = cRet
    End Function

    Private Function IdentificadorPago() As String
        IdentificadorPago = dTosSeg(Date.Now)
    End Function

    Private Function IdentificacionOrdenante(NombreAcreedor, DireccionPostal, Cod) As String  '  2.19
        Dim cRet As String

        '  Las variables las rellenaremos
        cRet = (CABIP11itr + CABIP12i + NombreAcreedor + CABIP12f) '+ CABIP14i + CABIP15i + "ES" + CABIP15f + CABIP16i + DireccionPostal + CABIP16f + CABIP14f)
        cRet = cRet + (CABA3i + Cod + CABA3f)
        cRet = cRet + CABIP11ftr

        cadena = cadena + cRet
        IdentificacionOrdenante = cRet
    End Function

    Private Function CuentadeCargo(Cta) As String  '  2.20 2.21
        Dim cRet As String

        '  Las variables las rellenaremos
        cRet = (CABIP17iTr + CABIP18i + Cta + CABIP18f + CABIP19if + CABIP17fTr)

        cadena = cadena + cRet
        CuentadeCargo = cRet
    End Function

    Private Function BicAcreedor(cbic) As String     '  2.21 y 2.24
        Dim cRet As String
        If Trim(cbic) = "" Then
            cbic = "NOTPROVIDED"
        End If
        cRet = CABIP20itr + cbic + CABIP20ftr + CABIP21if

        cadena = cadena + cRet
        BicAcreedor = cRet
    End Function

    Private Function InicioRecibo() As String
        Dim cRet As String
        cRet = RECI01iTr
        cadena = cadena + cRet
        InicioRecibo = cRet
    End Function

    Private Function FinRecibo() As String
        Dim cRet As String
        cRet = RECI01fTr
        cadena = cadena + cRet
        FinRecibo = cRet
    End Function

    Private Function IdentificacionDelPago(no_recibo) As String  '  2.29
        'Static fecha_hora As String
        'Static numero_orden As Integer
        Dim cRet As String
        Dim IdPago As String

        If no_recibo = "--_xx" Then
            Fecha_envio = Date.Now
            Hora_envio = Format(Date.Now, "HH:mm:ss")
            fecha_hora = FormatoFechaHora(Fecha_envio, Hora_envio)
            numero_orden = 0
            IdentificacionDelPago = ""
            Exit Function
        End If

        InicioRecibo()
        numero_orden = numero_orden + 1
        IdPago = fecha_hora + Right("000" + Trim(CStr(numero_orden)), 4)
        cRet = RECI02i                                                                       '  2.29
        cRet = cRet + RECI03i + IdPago + RECI03f
        cRet = cRet + RECI04i + Trim(no_recibo) + RECI04f
        cRet = cRet + RECI02f

        cadena = cadena + cRet
        IdentificacionDelPago = cRet
    End Function


    Private Function TipoDePago() As String    '  2.6   , 2.7 a 2.12 , 2.14
        Dim cRet As String

        cRet = CABIP6i '  2.6
        cRet = cRet + CABIP7iTr
        cRet = cRet + CABIP6f

        cadena = cadena + cRet
        TipoDePago = cRet
    End Function

    Private Function ImporteRecibo(import) As String
        Dim cRet As String

        cRet = REC242i
        cRet = cRet + RECI05i + ComaPorPunto(Format(import, "#######0.00")) + RECI05f
        cRet = cRet + REC242f

        cadena = cadena + cRet
        ImporteRecibo = cRet
    End Function

    Private Function BICRecibo(cbic) As String
        Dim cRet As String

        cRet = RECI11itr + Trim(cbic) + RECI11ftr

        cadena = cadena + cRet
        BICRecibo = cRet
    End Function

    Private Function DatosAcreedor(nom, direccion) As String
        Dim cRet As String

        cRet = RECI12itr
        cRet = cRet + (RECI14i + Trim(nom) + RECI14f)
        cRet = cRet + RECI12ftr

        cadena = cadena + cRet
        DatosAcreedor = cRet
    End Function

    Private Function CuentaAcreedor(Cta) As String
        Dim cRet As String

        cRet = RECI18itr
        cRet = cRet + (RECI19i + Trim(Cta) + RECI19f)
        cRet = cRet + RECI18ftr

        cadena = cadena + cRet
        CuentaAcreedor = cRet
    End Function

    Private Function FormatoFecha(darg) As String
        FormatoFecha = (CStr(Year(darg)) + "-" + Right("00" & Month(darg), 2) + "-" + Right("00" & DateAndTime.Day(darg), 2))
    End Function

    Private Function Init(nArg, oArg)
        NumRemesa = nArg
        ReDim aRem(0)
        ReDim aErrores(0)
        Call SacaIniciales()
    End Function

    Private Function Transferencia_Cheque() As String
        Transferencia_Cheque = IIf(Ident_pago = "TRF", "T", "C")
    End Function

    Public Function ModalidadDePago(id)
        Ident_pago = IIf(id = "C", "CHK", "TRF")
    End Function
    Private Function IniciarArchivo() As String
        Dim i As Integer

        For i = 1 To 100
            CuantosDisco(i) = 0
            EntradasDisco(i) = 0
            EntradasDiscoOper(i) = 0
            ImporteDisco(i) = 0
        Next

        SacaIniciales()
        cadena = ""

    End Function

    Private Function SacaIniciales()
        If UCase(Trim(Right(Path, 4))) <> ".XML" Then
            xDcrut = Trim(Path) + ".xml"
        End If
    End Function


    Public Function RutaArchivo(cArg)
        cRutaArchivo = cArg
        Call SacaIniciales()
    End Function

    Private Function CabeceraRemesa(aRet() As String)
        Dim cDireccion As String
        Dim cLocalidad As String
        Dim cProvincia As String
        'Dim aRet() As String

        'aRet = Array("", "", "", "")
        entidad = Right("0000" + Trim(entidad), 4)
        oficina = Right("0000" + Trim(oficina), 4)
        dc = Right("00" + Trim(dc), 2)
        codcuenta = Right("0000000000" + Trim(codcuenta), 10)
        aRet(1) = ETxt(nombre)
        aRet(2) = LimpiaNif(NIF)
        aRet(3) = Trim(domicilio) + " " + Trim(cLocalidad) + " " + Trim(Poblacion)
        aRet(4) = IBANCalculo("ES", entidad & oficina & dc & codcuenta)

        'CabeceraRemesa = aRet
    End Function

    Private Function InicioDocumento() As String
        Dim cRet As String
        IniciarArchivo()
        cRet = CAB1 + CAB2I
        cadena = cadena + cRet
        IdentificacionDelPago("--_xx")  '  Iniciamos contador de pagos
        InicioDocumento = cRet
    End Function

    Private Function CabeceraPresentador() As String '  1.0
        Dim cRet As String
        cRet = CABPI
        cadena = cadena + cRet
        CabeceraPresentador = cRet
    End Function

    Private Function HoraEnvio() As String  '1.1
        Dim cRet As String
        Dim cFec As String

        cFec = FormatoFecha(Fecha_envio) + "T" + Trim(Hora_envio)
        cRet = CABP2i + cFec + CABP2f
        cadena = cadena + cRet

        HoraEnvio = cRet
    End Function

    Private Function NumeroOperacionesC1(no) As String  '  1.6
        Dim cRet As String
        cRet = CABP3i + CStr(no) + CABP3f
        cadena = cadena + cRet
        NumeroOperacionesC1 = cRet
    End Function

    Private Function ImporteOperacionesC1(import) As String  '  1.7
        Dim cRet As String
        cRet = CABP4i + ComaPorPunto(Trim(Format(import, "########0.00"))) + CABP4f
        cadena = cadena + cRet
        ImporteOperacionesC1 = cRet
    End Function

    Private Function TotalOperaciones(num, import) As String         '  2.4 y  '  2.5
        Dim cRet As String

        cRet = (CABIP4i + CStr(num) + CABIP4f)
        cRet = cRet + (CABIP5i + ComaPorPunto(Trim(Format(import, "########0.00"))) + CABIP5f)

        cadena = cadena + cRet
        TotalOperaciones = cRet
    End Function

    Private Function CerrarArchivo()
        FileClose(ArchivoEnCurso)
    End Function

    Private Function GrabarArchivo()
        FilePut(ArchivoEnCurso, cadena)
        cadena = ""
    End Function

    Private Function ParteIniciadora(nom, Cod) As String '  1.8
        Dim cRet As String

        cRet = CabeceraParteIniciadora()
        cRet = cRet + (CABA2i + Trim(nom) + CABA2f)
        cRet = cRet + (CABA3i + Cod + CABA3f)
        cRet = cRet + FinCabeceraParteIniciadora()
        cRet = cRet + FinCabeceraPresentador()

        cadena = cadena + cRet
        ParteIniciadora = cRet
    End Function

    Private Function DetallePago(Det) As String
        Dim cRet As String

        cRet = RECI20i
        cRet = cRet + (RECI21i + Trim(Det) + RECI21f)
        cRet = cRet + RECI20f
        cadena = cadena + cRet
        FinRecibo()

        'Ejemplo:
        '    <RmtInf>
        '        <Ustrd>FAC. SER/NO. 01 000003143 DE FECHA 29-07-2014 CONSUMO AGUA RIEGOJUNIO 2014</Ustrd>
        '    </RmtInf>
        '</DrctDbtTxInf> Fin del recibo
        DetallePago = cRet
    End Function

    Private Function FinIdentificacionPago() As String
        Dim cRet As String
        cRet = CABIPf
        cadena = cadena + cRet
        FinIdentificacionPago = cRet
    End Function

    Private Function FinDocumento() As String
        Dim cRet As String
        cRet = CAB2F
        cadena = cadena + cRet
        FinDocumento = cRet
    End Function

    Private Function IdentificacionPago() As String  '  2.0
        Dim cRet As String
        cRet = CABIPi
        IdentificacionPago = cRet
    End Function

    Private Function TipoApuntes(Cod) As String  '  2.3
        Dim cRet As String

        If Cod = "de" Then
            cRet = CABIP3i + "false" + CABIP3f
        Else
            cRet = CABIP3i + "true" + CABIP3f
        End If

        TipoApuntes = cRet
    End Function

    Private Function NumeroOperacionesC2(no) As String  '  1.6
        Dim cRet As String
        cRet = CABIP4i + CStr(no) + CABIP4f
        cadena = cadena + cRet
        NumeroOperacionesC2 = cRet
    End Function

    Private Function ImporteOperacionesC2(import) As String  '  1.7
        Dim cRet As String
        cRet = CABIP5i + ComaPorPunto(Trim(Format(import, "########0.00"))) + CABIP5f
        cadena = cadena + cRet
        ImporteOperacionesC2 = cRet
    End Function



    Private Function ETxt(cTexto) As String
        cTexto = Replace(cTexto, "Ñ", "N")
        cTexto = Replace(cTexto, "ñ", "n")
        cTexto = Replace(cTexto, "Ç", "C")
        cTexto = Replace(cTexto, "ç", "c")
        cTexto = Replace(cTexto, "Á", "A")
        cTexto = Replace(cTexto, "É", "E")
        cTexto = Replace(cTexto, "Í", "I")
        cTexto = Replace(cTexto, "Ó", "O")
        cTexto = Replace(cTexto, "Ú", "U")
        cTexto = Replace(cTexto, "á", "a")
        cTexto = Replace(cTexto, "é", "e")
        cTexto = Replace(cTexto, "í", "i")
        cTexto = Replace(cTexto, "ó", "o")
        cTexto = Replace(cTexto, "ú", "u")
        cTexto = Replace(cTexto, "'", " ")
        cTexto = Replace(cTexto, "ª", " ")
        cTexto = Replace(cTexto, "º", " ")
        cTexto = LimpiaXML(cTexto)
        ETxt = cTexto
    End Function

    Private Function CabeceraParteIniciadora() As String   '  1.8
        Dim cRet As String
        cRet = CABA1i
        CabeceraParteIniciadora = cRet
    End Function

    Private Function FinCabeceraParteIniciadora() As String
        Dim cRet As String
        cRet = CABA1f
        FinCabeceraParteIniciadora = cRet
    End Function

    Private Function FinCabeceraPresentador() As String
        Dim cRet As String
        cRet = CABPF
        FinCabeceraPresentador = cRet
    End Function


    Private Function LimpiaNif(cArg) As String
        Dim cCad As String ' = "" AS STRING
        Dim cRet As String '= "" AS STRING
        Dim i As Integer '= 0 AS Integer

        cCad = ""
        cRet = ""
        i = 0

        cArg = UCase(Trim(cArg))

        For i = 1 To Len(cArg)
            cCad = Mid(cArg, i, 1)
            If cCad >= "A" And cCad <= "Z" Then
                cRet = cRet + cCad
            ElseIf cCad >= "0" And cCad <= "9" Then
                cRet = cRet + cCad
            End If
        Next

        LimpiaNif = cRet
    End Function

    Private Function ComaPorPunto(cImporte) As String
        ComaPorPunto = Trim(Replace(cImporte, ",", "."))
    End Function

    Private Function LimpiaXML(cTexto) As String
        Dim cRet As String
        Dim i As Integer
        Dim C As String

        cRet = ""
        cTexto = Trim(cTexto)
        For i = 1 To Len(cTexto)
            C = Mid(cTexto, i, 1)
            If InStr(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrStuvwxyz0123456789/-?:().,‘+ ", C) <> 0 Then
                cRet = cRet + C
            Else
                cRet = cRet + " "
            End If
        Next

        LimpiaXML = cRet
    End Function

    Private Function IBANCalculo(ByVal Pais As String, ByVal cuenta As String) As String
        ' Recibe el pais con 2 letras (ES para España)
        ' Recibe el número de cuenta

        Dim Letras As String
        Dim IBAN As String
        Dim Dividendo As Integer
        Dim Resto As Integer
        Dim Contador As Integer

        ' Quita los posibles espacios
        cuenta = Replace(cuenta, " ", "")

        ' Calcula el valor de las letras, las quita y añade el valor al final
        Letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        IBAN = cuenta & CStr(InStr(1, Letras, Left(Pais, 1)) + 9) & CStr(InStr(1, Letras, Right(Pais, 1)) + 9) & "00"

        For Contador = 1 To Len(IBAN)
            Dividendo = Resto & Mid(IBAN, Contador, 1)
            Resto = Dividendo Mod 97
        Next Contador

        IBANCalculo = Pais & Format((98 - Resto), "00") & cuenta

    End Function

    Private Function IBANValidacion(ByVal IBAN As String) As Boolean
        ' Recibe el IBAN

        Dim Letras As String
        Dim Dividendo As Integer
        Dim Resto As Integer
        Dim Contador As Integer

        Letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        ' Quita la palabra IBAN
        IBAN = Replace(IBAN, "IBAN", "")

        ' Quita los posibles espacios
        IBAN = Replace(IBAN, " ", "")

        ' Calcula el valor de las letras, las quita y añade el valor al final
        IBAN = Mid(IBAN, 3, Len(IBAN) - 2) & CStr(InStr(1, Letras, Left(IBAN, 1)) + 9) & CStr(InStr(1, Letras, Mid(IBAN, 2, 1)) + 9)

        ' Quita los digitos de control y los pone al final
        IBAN = Mid(IBAN, 3, Len(IBAN) - 2) & Left(IBAN, 2)

        For Contador = 1 To Len(IBAN)
            Dividendo = Resto & Mid(IBAN, Contador, 1)
            Resto = Dividendo Mod 97
        Next Contador

        If Resto = 1 Then
            IBANValidacion = True
        Else
            IBANValidacion = False
        End If

    End Function


    Private Function CalculoDelIdentificador(Pais As String, Nif2 As String, SufOrd19 As String) As String
        Dim NIF As String
        Dim aLetra As Object
        Dim aNumero As Object
        Dim Valor As String
        Dim i As Integer
        Dim cMod As String
        Dim cValor As String

        cValor = Replace(Nif2, " ", "")
        cValor = Replace(cValor, "-", "")
        cValor = Replace(cValor, "/", "")
        cValor = Replace(cValor, "_", "")
        cValor = Replace(cValor, "\", "")
        cValor = Replace(cValor, ":", "")
        cValor = Left(Trim(cValor), 9)
        NIF = cValor

        cValor = cValor + Trim(Pais) + "00"

        aLetra = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        aNumero = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35"}

        For i = 1 To UBound(aLetra)
            cValor = Replace(cValor, aLetra(i), aNumero(i))
        Next

        cMod = CalculaModulo(cValor)

        CalculoDelIdentificador = (Pais + cMod + SufOrd19 + NIF)
    End Function

    Private Sub ObtenTotales()
        Dim importe1 As Double
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Fichero As Integer
        Dim i As Integer

        'aaaa

        For Fichero = 1 To NumDisc
            For i = 1 To Cuantos
                If Trim(DiscAbonos(Fichero)) = Format(CInt(lvAbonos.Items.Item(i).subitems(13).text), "0000") + Trim(lvAbonos.Items.Item(i).subitems(14)) Then
                    importe1 = Math.Round(CDbl(lvAbonos.Items.Item(i).subitems(8).text), 2)
                    EntradasDisco(Fichero) = EntradasDisco(Fichero) + 1
                    EntradasDiscoOper(Fichero) = EntradasDiscoOper(Fichero) + 1
                    CuantosDisco(Fichero) = CuantosDisco(Fichero) + 1
                    ImporteDisco(Fichero) = ImporteDisco(Fichero) + importe1
                End If
            Next
        Next
    End Sub

    Private Function DToS(dfecha) As String
        Dim Dia, Mes, anno As String
        anno = "" & Year(dfecha)
        Mes = Right("00" & Month(dfecha), 2)
        Dia = Right("00" & DateAndTime.Day(dfecha), 2)
        DToS = anno & Mes & Dia
    End Function


    Private Function CalculaModulo(num As String) As String
        Dim Dividendo As Integer
        Dim Resto As Integer
        Dim Contador As Integer

        For Contador = 1 To Len(num)
            Dividendo = Resto & Mid(num, Contador, 1)
            Resto = Dividendo Mod 97
        Next Contador

        CalculaModulo = Format((98 - Resto), "00")

    End Function

    Private Function Time() As String
        Return DateAndTime.Now.ToString("HH:mm:ss")
    End Function

    Private Sub CerrarTodos(Max As Integer)

        Dim i As Integer

        On Error GoTo errorescerrar

        For i = 1 To Max
            ' Close #i
        Next
        Exit Sub

errorescerrar:
        Resume Next
    End Sub

    Private Function FormatoFechaHora(darg, hora) As String
        FormatoFechaHora = CStr(Year(darg)) + Right("00" & Month(darg), 2) + Right("00" & DateAndTime.Day(darg), 2) + Trim(Replace(hora, ":", ""))
    End Function

End Class
