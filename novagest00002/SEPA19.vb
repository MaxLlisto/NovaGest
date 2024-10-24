Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar

Public Class SEPA19
    Public ObjetoGlobal As Object
    Public DiscCargos() As String
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
    Public LvCargos As Object
    Public FechaRemesa As Date
    Public FechaCargo As Date
    Public trans As SqlClient.SqlTransaction
    Dim fecha_hora As String
    Dim numero_orden As Integer
    Dim Cadena As String
    Dim Fecha_envio As Date
    Dim Hora_envio As String
    Dim aTiposDePago(5) As String
    Dim aRecibosTPag(5) As Integer
    Dim aImportesTPa(5) As Double
    Dim aRecTiposDePago As Object
    Dim IbanBan As String
    Dim cIdentificadorPr As String
    Dim BICPresentador As String
    'Dim oTB  As FicheroTexto
    Dim cTipoRemesa As String ' = "PRE" AS STRING  '   Pueder ser PRE lo normal o puede ser FSDD
    Dim cModalidad As String '  = "COR1" AS STRING
    Dim CuantosDisco(100) As Integer
    Dim EntradasDisco(100) As Integer
    Dim EntradasDiscoOper(100) As Integer
    Dim ImporteDisco(100) As Double
    Dim BloqueRecibos() As String
    Dim CuentaAnterior() As String
    Dim BicAnterior() As String
    Dim FirmaMandato() As String
    Dim ReferenciaUnica() As String
    Dim RecibosEnBloque() As String
    Dim ImporteBloque() As String
    Dim NumeroRecibo() As String
    Public ArchivoEnCurso As Integer
    Public lbl As Object
    Public PB As Object
    Private xDcrut As String

    Dim CalculaIban As Boolean

    Private Const CABP2i = "<CreDtTm>"  '   Fecha hora de creaci n Ej. <CreDtTm>2015-11-25T12:22:58</CreDtTm>        1.1
    Private Const CABP3i = "<NbOfTxs>"  '   Numero de operaciones Ej. <NbOfTxs>451</NbOfTxs>                         1.6
    Private Const CABP4i = "<CtrlSum>"  '   Control de suma Ej. <CtrlSum>45185.05</CtrlSum>                          1.7
    Private Const CABA1i = "<InitgPty>"  '   Inicio cabecera acreedor                                                1.8
    Private Const CABA2i = "<Nm>"  '   Nombre del acreedor Ej. <Nm>AUGUIS DE TURIS</Nm>                              1.8
    Private Const CABA3i = "<Id><OrgId><Othr><Id>"  '   Identificador acreedor Ej. <Id><OrgId><Othr><Id>ES64000F97224828</Id></Othr></OrgId></Id> NOTA: Para persona jut dica  1.8
    Private Const CABA3f = "</Id></Othr></OrgId></Id>"
    '   Se cierra con  CABA1f+CABPF </InitgPty></GrpHdr>
    '   Informaci n del pago
    Private Const CABIPi = "<PmtInf>"     '                                                                              2.0
    Private Const CABIP1i = "<PmtInfId>"  '   Identificaci n de la informaci n del pago (35) Ej. <PmtInfId>ES64000F97224828-201511251223080000</PmtInfId> 2.1
    Private Const CABIP2i = "<PmtMtd>"  '   M todo de pago(2) Ej. <PmtMtd>DD</PmtMtd>                                                                     2.2
    Private Const CABIP3i = "<BtchBookg>"  '    Indicador de apunte en cuenta(5) Ej. <BtchBookg>TRUE</BtchBookg>                                          2.3
    Private Const CABIP4i = "<NbOfTxs>"  '   N mero de operaciones                                                                                        2.4
    Private Const CABIP5i = "<CtrlSum>"  '   Control de suma suma de importes individuales                                                                2.5
    Private Const CABIP6i = "<PmtTpInf>"  '   Informaci n del tipo de pago                                                                                2.6
    Private Const CABIP8i = "<SeqTp>"  '   Tipop de secuencia Ej. SeqTp>RCUR</SeqTp> Posibilidades                                                        2.14
    '
    'Tipos disponibles
    'FNAL Final  ltimo adeudo de una serie de adeudos recurrentes.
    'FRST First Primer adeudo de una serie de adeudos recurrentes.
    'OOFF OneOff Adeudo correspondiente a una operaci n con un  nico pago*.
    'RCUR Recurring Adeudo de una serie de adeudos recurrentes, cuando no se trata ni del primero ni del  ltimo.
    '
    Private Const CABIP8f = "</SeqTp>"
    Private Const CABIP9i = "<CtgyPurp><Cd>"  '   Categor a del proposito Ej. <CtgyPurp><Cd>TRAD</Cd></CtgyPurp>                                       2.15 y 2.16
    Private Const CABIP10i = "<ReqdColltnDt>"   '   Fecha de cobro Ej. <ReqdColltnDt>2014-08-04</ReqdColltnDt>                                         2.18
    Private Const CABIP11i = "<Cdtr>"  '   Acreedor                                                                                                    2.19
    Private Const CABIP12i = "<Nm>"  '   Nombre                                                                                                        2.19
    Private Const CABIP14i = "<PstlAdr>"  '   Direcci n postal (Pa s + Direcci n en texto libre)                                                       2.19
    Private Const CABIP15i = "<Ctry>"  '   Pa s Ej. <Ctry>ES</Ctry>                                                                                    2.19
    Private Const CABIP16i = "<AdrLine>"  '   Direcci n en una l nea <AdrLine>VALENCIA TURIS VALENCIA</AdrLine>                                        2.19
    Private Const CABIP16f = "</AdrLine>"
    '
    'Cierra
    '</PstlAdr></Cdtr>
    '

    ' <CdtrAcct><Id><IBAN>ES8831237271112710000186</IBAN></Id><Ccy>EUR</Ccy></CdtrAcct> */

    Private Const CABIP17i = "<CdtrAcct>"  '   Inicio de cuenta Identificaci n IBAN + moneda                                                           2.20
    Private Const CABIP17f = "</CdtrAcct>"
    Private Const CABIP18i = "<Id><IBAN>"  '   Ejemplo <Id><IBAN>ES8831237271112710000186</IBAN></Id>                                                  2.21
    Private Const CABIP18f = "</IBAN></Id>"
    Private Const CABIP19if = "<Ccy>EUR</Ccy>"  '   Moneda euros 2.20

    ' Cierra monera
    ' </CdtrAcct>

    Private Const CABIP20i = "<CdtrAgt><FinInstnId><BIC>"  '   Entidad de cr dito donde el acreedor mantiene su cuenta Ej. <CdtrAgt><FinInstnId><BIC>NOTPROVIDED</BIC></FinInstnId></CdtrAgt>  2.21
    Private Const CABIP20f = "</BIC></FinInstnId></CdtrAgt>"
    Private Const CABIP21if = "<ChrgBr>SLEV</ChrgBr>"  '    Cl usula de gastos   ChargeBearer (solo tiene un valor posible)                            2.24

    Private Const CABIP22i = "<CdtrSchmeId><Id><PrvtId><Othr><Id>"  '   Identificaci n del acreedor <CdtrSchmeId><Id><PrvtId><Othr><Id>ES64000F97224828</Id><SchmeNm><Prtry>SEPA</Prtry></SchmeNm></Othr></PrvtId></Id></CdtrSchmeId> 2.27
    Private Const CABIP22f = "</Id><SchmeNm><Prtry>SEPA</Prtry></SchmeNm></Othr></PrvtId></Id></CdtrSchmeId>"

    ' Recibos, se har  un bloque por recibo
    Private Const RECI01i = "<DrctDbtTxInf>"  '   Cabecera por recibo emitido                                                                          2.28
    Private Const RECI02f = "</PmtId>"  '   Identificaci n del pago                                                                                    2.29
    Private Const RECI03i = "<InstrId>"  '   Identificaci n de la instrucci n(1-35) Ej. <InstrId>20151125122308-0001</InstrId>
    Private Const RECI03f = "</InstrId>"      '                                                                                                          2.30
    Private Const RECI04i = "<EndToEndId>"  '   dentificaci n de extremo a extremo(1-35) Ej. <EndToEndId>7045299</EndToEndId>                          2.31
    Private Const RECI04f = "</EndToEndId>"  '   Identificaci n  nica asignada por la parte iniciadora para identificar inequ vocamente cada operaci n (AT-10). Esta referencia se transmite de extremo a extremo, sin cambios, a lo largo de toda la cadena de pago.

    'Se cerrar a con RECI03f = "</InstrId>"
    Private Const RECI05i = "<InstdAmt Ccy=""EUR"">"  '   Importe ordenado(del recibo M x. 11 d gitos 2 dec . sep.decimal) Ej. <InstdAmt Ccy="EUR">2.70</InstdAmt> 2.44
    Private Const RECI05f = "</InstdAmt>"

    Private Const CAB2F = "</Document>"
    ' Cabecera general
    Private Const CAB3F = "</CstmrDrctDbtInitn>"
    ' Cabecera presentador
    Private Const CABPF = "</GrpHdr>"  '   Cabecera grupo presentador
    Private Const CABP1I = "<MsgId>"  '   Identificaci n del mensaje Ej. <MsgId>PRE20151125122258559721357924680124</MsgId>                         1.1
    Private Const CABP1f = "</MsgId>"
    Private Const CABP2f = "</CreDtTm>"
    Private Const CABP3f = "</NbOfTxs>"
    Private Const CABP4f = "</CtrlSum>"
    ' Cabecera acreedor
    Private Const CABA1f = "</InitgPty>"  '   Inicio cabecera acreedor
    Private Const CABA2f = "</Nm>"
    Private Const CABIPf = "</PmtInf>"
    Private Const CABIP1f = "</PmtInfId>"
    Private Const CABIP2f = "</PmtMtd>"
    Private Const CABIP3f = "</BtchBookg>"
    Private Const CABIP4f = "</NbOfTxs>"
    Private Const CABIP5f = "</CtrlSum>"
    Private Const CABIP6f = "</PmtTpInf>"
    Private Const CABIP10f = "</ReqdColltnDt>"

    '<Cdtr><Nm>AUGUIS DE TURIS</Nm><PstlAdr><Ctry>ES</Ctry><AdrLine>VALENCIA TURIS VALENCIA</AdrLine></PstlAdr></Cdtr>
    Private Const CABIP11f = "</Cdtr>"
    Private Const CABIP12f = "</Nm>"  '   Nombre
    Private Const CABIP14f = "</PstlAdr>"  '
    Private Const CABIP15f = "</Ctry>"
    Private Const RECI01f = "</DrctDbtTxInf>"
    Private Const RECI06i = "<DrctDbtTx>"       '                                                                                                     2.46
    Private Const RECI06f = "</DrctDbtTx>"
    '
    '<DrctDbtTx>
    '   <MndtRltdInf>
    '       <MndtId>00502</MndtId>                      - Identificaci n del mandato c digo socio o cliente
    '       <DtOfSgntr>2009-10-31</DtOfSgntr>           - Fecha de la firma del mandato
    '       <AmdmntInd>FALSE</AmdmntInd>                - Indica si el mandato se ha modificado TRUE S  se ha modificado FALSE No se ha modificado
    '   </MndtRltdInf>
    '</DrctDbtTx>
    '

    Private Const RECI08i = "<MndtId>"     '                                                                                                       2.48
    Private Const RECI09i = "<DtOfSgntr>"     '                                                                                                      2.49
    Private Const RECI10i = "<AmdmntInd>"       '                                                                                                    2.50
    Private Const RECI11i = "<DbtrAgt><FinInstnId><BIC>"   '   Entidad del deudor, identificaci n Ej. <DbtrAgt><FinInstnId><BIC>ESPCESMMXXX</BIC></FinInstnId></DbtrAgt> 2.70
    Private Const RECI11f = "</BIC></FinInstnId></DbtrAgt>"

    Private Const RECI12i = "<Dbtr>"                          '   Deudor                                                     2.72
    Private Const RECI12f = "</Dbtr>"                         '   Deudor

    Private Const RECI14i = "<Nm>"                        '   Deudor                                                      2.72
    Private Const RECI14f = "</Nm>"                       '   Deudor

    Private Const RECI15i = "<PstlAdr>"                       '   Deudor                                                  2.72
    Private Const RECI15f = "</PstlAdr>"                          '   Deudor

    Private Const RECI16i = "<Ctry>"                          '   Deudor                                                      2.72
    Private Const RECI16f = "</Ctry>"                         '   Deudor

    Private Const RECI17i = "<AdrLine>"                       '   Deudor                                                  2.72
    Private Const RECI17f = "</AdrLine>"                          '   Deudor

    Private Const RECI18i = "<DbtrAcct>"                          '   Cuenta del deudor                                        2.73
    Private Const RECI18f = "</DbtrAcct>"
    Private Const RECI19i = "<Id><IBAN>"      '                                                                              2.73
    Private Const RECI20i = "<RmtInf>"            '                                                                             2.88
    Private Const RECI20f = "</RmtInf>"

    Private Const RECI21i = "<Ustrd>"    '                                                                                    2.89
    Private Const RECI21f = "</Ustrd>"
    'Private Const fin = Chr(13) + Chr(10)
    Private Const CAB2I = "<Document xmlns=""urn:iso:std:iso:20022:tech:xsd:pain.008.001.02"">"
    Private Const RECI30i = "<AmdmntInfDtls>"                         '   Relaci n elementos del mandato modificados 2.51
    Private Const RECI30f = "</AmdmntInfDtls>"
    Private Const RECI31i = "<OrgnlDbtrAcct><Id><IBAN>"           '   Cuenta del deudor original que ha sido modificada.  2.57
    Private Const RECI31f = "</IBAN></Id></OrgnlDbtrAcct>"
    Private Const RECI32if = "<OrgnlDbtrAgt><FinInstnId><Othr><Id>SMNDA</Id></Othr></FinInstnId></OrgnlDbtrAgt>"
    Private Const RECI09f = "</DtOfSgntr>"
    Private Const RECI10f = "</AmdmntInd>"
    Private Const RECI07f = "</MndtRltdInf>"
    Private Const CABIP7i = "<SvcLvl><Cd>SEPA</Cd></SvcLvl><LclInstrm><Cd>"  '                                       2.8 a 2.12
    Private Const CAB1 = "<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>"
    Private Const RECI08f = "</MndtId>"     '                                                                                                       2.48
    Private Const CAB3I = "<CstmrDrctDbtInitn>"  '   Inicio cabecera general
    Private Const RECI07i = "<MndtRltdInf>"      '                                                                                                   2.47
    Private Const CABIP9f = "</Cd></CtgyPurp>"
    Private Const CABPI = "<GrpHdr>"   '   Cabecera grupo presentador                                                1.0
    Private Const CABIP7f = "</Cd></LclInstrm>"  '   Presentaci n reducida 1 d a
    Private Const RECI19f = "</IBAN></Id>"
    Private Const RECI02i = "<PmtId>"  '   Identificaci n del pago 2.29



    '
    '   Ejemplo:
    '   <RmtInf>
    '       <Ustrd>FAC. SER/NO. 01 000003143 DE FECHA 29-07-2014 CONSUMO AGUA RIEGOJUNIO 2014</Ustrd>
    '   </RmtInf>
    '</DrctDbtTxInf>    Fin del recibo
    '

    Private Function InicioDocumento() As String
        Dim cRet As String

        IniciarArchivo()
        cRet = CAB1 + CAB2I
        Cadena = Cadena + cRet
        IdentificacionDelPago(-1)  '   Iniciamos contador de pagos
        InicioDocumento = cRet
    End Function

    Private Function FinDocumento() As String
        Dim cRet As String
        cRet = CAB2F
        Cadena = Cadena + cRet
        FinDocumento = cRet
    End Function


    Private Function Cabecera1() As String
        Dim cRet As String
        cRet = CAB3I
        Cadena = Cadena + cRet
        Cabecera1 = cRet
    End Function

    Private Function FinCabecera1() As String
        Dim cRet As String
        cRet = CAB3F
        Cadena = Cadena + cRet
        FinCabecera1 = cRet
    End Function

    Private Function CabeceraPresentador() As String '   1.0
        Dim cRet As String
        cRet = CABPI
        Cadena = Cadena + cRet
        CabeceraPresentador = cRet
    End Function

    Private Function FinCabeceraPresentador() As String
        Dim cRet As String
        cRet = CABPF
        FinCabeceraPresentador = cRet
    End Function

    Private Function Mensaje() As String
        Dim cRet As String
        Dim id As String
        Dim IdTipo As String

        If TipoRemesa() = "N32" Then
            IdTipo = "FSDD"
        Else
            IdTipo = "PRE"
        End If
        id = IdentificadorFichero(IdTipo)
        cRet = CABP1I + id + CABP1f
        Cadena = Cadena + cRet

        Mensaje = cRet
    End Function

    Private Function HoraEnvio() As String   '  1.1
        Dim cRet As String
        Dim cFec As String

        cFec = FormatoFecha(Fecha_envio) + "T" + Trim(Hora_envio)
        cRet = CABP2i + cFec + CABP2f
        Cadena = Cadena + cRet
        HoraEnvio = cRet
    End Function

    Private Function NumeroOperacionesC1(no) As String  '   1.6
        Dim cRet As String
        cRet = CABP3i + CStr(no) + CABP3f
        Cadena = Cadena + cRet
        NumeroOperacionesC1 = cRet
    End Function

    Private Function ImporteOperacionesC1(import) As String   '   1.7
        Dim cRet As String
        cRet = CABP4i + ComaPorPunto(Trim(Format(import, "######0.00"))) + CABP4f
        Cadena = Cadena + cRet
        ImporteOperacionesC1 = cRet
    End Function


    Private Function IdentificacionPago() As String  '   2.0
        Dim cRet As String
        cRet = CABIPi
        IdentificacionPago = cRet
    End Function

    Private Function FinIdentificacionPago() As String
        Dim cRet As String
        cRet = CABIPf
        Cadena = Cadena + cRet
        FinIdentificacionPago = cRet
    End Function

    Private Function IdPago(ByVal Cod As String, Optional MetodoPago As String = "DD") As String   '   2.1 y 2.2
        Dim cRet As String

        cRet = IdentificacionPago()
        cRet = cRet + (CABIP1i + Left(Cod, 35) + CABIP1f)
        cRet = cRet + (CABIP2i + MetodoPago + CABIP2f)
        cRet = cRet + TipoApuntes("un")

        Cadena = Cadena + cRet
        IdPago = cRet

    End Function

    Private Function CuentraDeudor(Cta) As String
        Dim cRet As String

        cRet = RECI18i
        cRet = cRet + (RECI19i + Trim(Cta) + RECI19f)
        cRet = cRet + RECI18f

        Cadena = Cadena + cRet
        CuentraDeudor = cRet
    End Function

    Private Function DetallePago(Det) As String
        Dim cRet As String

        cRet = RECI20i
        cRet = cRet + (RECI21i + Trim(Det) + RECI21f)
        cRet = cRet + RECI20f
        Cadena = Cadena + cRet
        FinRecibo()
        '
        '   Ejemplo:
        '   <RmtInf>
        '       <Ustrd>FAC. SER/NO. 01 000003143 DE FECHA 29-07-2014 CONSUMO AGUA RIEGOJUNIO 2014</Ustrd>
        '   </RmtInf>
        '</DrctDbtTxInf>    Fin del recibo
        '
        DetallePago = cRet
    End Function


    Private Function SacaIniciales() As String
        If UCase(Trim(Right(Path, 4))) <> ".XML" Then
            xDcrut = Trim(Path) + ".xml"
        End If
    End Function

    Private Function IniciarArchivo() As String
        Dim i As Integer

        For i = 1 To 100
            CuantosDisco(i) = 0
            EntradasDisco(i) = 0
            EntradasDiscoOper(i) = 0
            ImporteDisco(i) = 0
        Next
        ReDim BloqueRecibos(0)
        ReDim BloqueRecibos(0)
        ReDim CuentaAnterior(0)
        ReDim BicAnterior(0)
        ReDim FirmaMandato(0) 'aaa
        ReDim NumeroRecibo(0)

        SacaIniciales()
        'AbrirFicheros
        Cadena = ""

    End Function

    Private Function CerrarArchivo()
        FileClose(ArchivoEnCurso)
    End Function

    Private Function GrabarArchivo()
        FilePut(ArchivoEnCurso, Cadena)
        Cadena = ""
    End Function

    Function FormatoHora(hora) As String
        FormatoHora = Trim(Replace(hora, ":", ""))
    End Function

    'Private Function IdentificadorFichero(cArg) As String
    'IdentificadorFichero = cArg + DToS(Date) + Mid(Time(), 1, 2) + Mid(Time(), 4, 2) + Mid(Time(), 7, 2) + PadR(CInt(GetTickCountLow() / 10), 5, "0") + Right("1357924680124", 18 - Len(Trim(cArg)))
    'End Function

    Private Function IdentificadorFichero(cArg) As String
        IdentificadorFichero = cArg + dTosSeg(Date.Now) + "1357924680124"
    End Function
    Private Function dTosSeg(Fecha) As String
        Dim Seg As String
        ' Devuelve la fecha en formato aaaammddhhmmssmmmmm a o mes dia hora minuto segundo y 5 milisegundos
        Seg = "00000" & Math.Abs(TimeGetTime)
        Seg = Replace(Seg, "-", "")
        dTosSeg = Year(Fecha) & Right("00" & Month(Fecha), 2) & Right("00" & DateAndTime.Day(Fecha), 2) & Mid(Format(Time(), "HH:mm:ss"), 1, 2) & Mid(Format(Time(), "HH:mm:ss"), 4, 2) & Mid(Format(Time(), "HH:mm:ss"), 7, 2) & Format(CLng(Right(Seg, 5)), "00000")
    End Function

    Private Function TimeGetTime()
        Return DateTime.Now.Millisecond
    End Function

    Private Function Time() As String
        Return DateAndTime.Now.ToString("HH:mm:ss")
    End Function

    Private Function ETxt(cTexto) As String
        cTexto = Replace(cTexto, " ", "N")
        cTexto = Replace(cTexto, " ", "n")
        cTexto = Replace(cTexto, " ", "C")
        cTexto = Replace(cTexto, " ", "c")
        cTexto = Replace(cTexto, " ", "A")
        cTexto = Replace(cTexto, " ", "E")
        cTexto = Replace(cTexto, " ", "I")
        cTexto = Replace(cTexto, " ", "O")
        cTexto = Replace(cTexto, " ", "U")
        cTexto = Replace(cTexto, " ", "a")
        cTexto = Replace(cTexto, " ", "e")
        cTexto = Replace(cTexto, " ", "i")
        cTexto = Replace(cTexto, " ", "o")
        cTexto = Replace(cTexto, " ", "u")
        cTexto = Replace(cTexto, "'", " ")
        cTexto = Replace(cTexto, " ", " ")
        cTexto = Replace(cTexto, " ", " ")
        cTexto = LimpiaXML(cTexto)
        ETxt = cTexto
    End Function
    '
    ' Cerramos la informaci n del tipo de pago
    ' </PmtTpInf>
    '
    Private Function CabeceraParteIniciadora() As String  '   1.8
        Dim cRet As String
        cRet = CABA1i
        CabeceraParteIniciadora = cRet
    End Function


    Private Function FinCabeceraParteIniciadora() As String
        Dim cRet As String
        cRet = CABA1f
        FinCabeceraParteIniciadora = cRet
    End Function

    Private Function ParteIniciadora(nom, Cod) As String '   1.8
        Dim cRet As String

        cRet = CabeceraParteIniciadora()
        cRet = cRet + (CABA2i + Trim(nom) + CABA2f)
        cRet = cRet + (CABA3i + Cod + CABA3f)
        cRet = cRet + FinCabeceraParteIniciadora()
        cRet = cRet + FinCabeceraPresentador()

        Cadena = Cadena + cRet
        ParteIniciadora = cRet
    End Function

    Private Function IdentificacionDeudor(Cod, fecha_mandato, Modif, BICModif, CtaModif) As String
        Dim cRet As String

        cRet = RECI06i
        cRet = cRet + RECI07i
        cRet = cRet + (RECI08i + Trim(Cod) + RECI08f)
        cRet = cRet + (RECI09i + fecha_mandato + RECI09f)
        cRet = cRet + (RECI10i + IIf(Modif, "true", "false") + RECI10f)

        If Trim(BICModif) <> "" Or Trim(CtaModif) <> "" Then '   Hay modificaciones
            cRet = cRet + ModificacionMandato(CtaModif, BICModif)
        End If

        cRet = cRet + RECI07f
        cRet = cRet + RECI06f

        '
        '<DrctDbtTx>
        '   <MndtRltdInf>
        '       <MndtId>00502</MndtId>                      - Identificaci n del mandato c digo socio o cliente
        '       <DtOfSgntr>2009-10-31</DtOfSgntr>           - Fecha de la firma del mandato
        '       <AmdmntInd>FALSE</AmdmntInd>                - Indica si el mandato se ha modificado TRUE S  se ha modificado FALSE No se ha modificado
        '   </MndtRltdInf>
        '</DrctDbtTx>
        '
        Cadena = Cadena + cRet
        IdentificacionDeudor = cRet
    End Function

    Private Function FechaDeCobro(fec) As String   '   2.18
        Dim cRet As String

        cRet = CABIP10i
        cRet = cRet + FormatoFecha(fec)
        cRet = cRet + CABIP10f

        Cadena = Cadena + cRet
        FechaDeCobro = cRet
    End Function

    Private Function IdentificacionAcreedor(NombreAcreedor, DireccionPostal) As String   '   2.19
        Dim cRet As String

        '   Las variables las rellenaremos
        cRet = (CABIP11i + CABIP12i + NombreAcreedor + CABIP12f + CABIP14i + CABIP15i + "ES" + CABIP15f + CABIP16i + DireccionPostal + CABIP16f + CABIP14f)
        cRet = cRet + CABIP11f

        Cadena = Cadena + cRet
        IdentificacionAcreedor = cRet
    End Function



    Private Function CuentadeAbono(Cta) As String   '   2.20 2.21
        Dim cRet As String

        '   Las variables las rellenaremos
        cRet = (CABIP17i + CABIP18i + Cta + CABIP18f + CABIP19if + CABIP17f)

        Cadena = Cadena + cRet
        CuentadeAbono = cRet
    End Function

    Private Function InicioRecibo() As String
        Dim cRet As String
        cRet = RECI01i
        Cadena = Cadena + cRet
        InicioRecibo = cRet
    End Function

    Private Function FinRecibo() As String
        Dim cRet As String
        cRet = RECI01f
        Cadena = Cadena + cRet
        FinRecibo = cRet
    End Function

    Private Function IdentificacionDelPago(no_recibo) As String   '   2.29
        'Static fecha_hora As String
        'Static numero_orden As Integer
        Dim cRet As String
        Dim IdPago As String

        If no_recibo < 0 Then
            Fecha_envio = Date.Now
            Hora_envio = Format(Time, "HH:mm:ss")
            fecha_hora = FormatoFechaHora(Fecha_envio, Hora_envio)
            numero_orden = 0
            IdentificacionDelPago = ""
            Exit Function
        End If

        InicioRecibo()
        numero_orden = numero_orden + 1
        IdPago = fecha_hora + Right("000" + Trim(CStr(numero_orden)), 4)
        cRet = RECI02i                                                                       '   2.29
        cRet = cRet + RECI03i + IdPago + RECI03f
        cRet = cRet + RECI04i + Trim(CStr(no_recibo)) + RECI04f
        cRet = cRet + RECI02f

        Cadena = Cadena + cRet
        IdentificacionDelPago = cRet
    End Function

    Private Function ImporteRecibo(import) As String
        Dim cRet As String
        cRet = RECI05i + ComaPorPunto(Trim(Format(import, "#######0.00"))) + RECI05f
        Cadena = Cadena + cRet
        ImporteRecibo = cRet
    End Function

    Private Function BICRecibo(bic) As String
        Dim cRet As String

        cRet = RECI11i + Trim(bic) + RECI11f
        Cadena = Cadena + cRet

        BICRecibo = cRet
    End Function

    Private Function DatosDeudor(ByVal nom As String, Optional direccion As String = "") As String
        Dim cRet As String

        cRet = RECI12i
        cRet = cRet + (RECI14i + Trim(nom) + RECI14f)
        If direccion <> "" Then
            cRet = cRet + RECI15i
            cRet = cRet + RECI16i + "ES" + RECI16f
            cRet = cRet + RECI17i + Trim(direccion) + RECI17f
            cRet = cRet + RECI15f
        End If
        cRet = cRet + RECI12f

        Cadena = Cadena + cRet
        DatosDeudor = cRet
    End Function

    Private Function FormatoFechaHora(darg, hora) As String
        FormatoFechaHora = CStr(Year(darg)) + Right("00" & Month(darg), 2) + Right("00" & DateAndTime.Day(darg), 2) + Trim(Replace(hora, ":", ""))
    End Function

    Private Function BicAcreedor(cbic) As String   '   2.21 y 2.24
        Dim cRet As String

        cRet = CABIP20i + cbic + CABIP20f + CABIP21if

        Cadena = Cadena + cRet
        BicAcreedor = cRet
    End Function

    Private Function CodigoIdentificacionAcreedor(id) As String
        Dim cRet As String

        cRet = CABIP22i + id + CABIP22f

        Cadena = Cadena + cRet
        CodigoIdentificacionAcreedor = cRet
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


    Private Function TipoApuntes(Cod) As String   '   2.3
        Dim cRet As String

        If Cod = "de" Then
            cRet = CABIP3i + "false" + CABIP3f
        Else
            cRet = CABIP3i + "true" + CABIP3f
        End If

        TipoApuntes = cRet
    End Function


    Private Function TotalOperaciones(num, import) As String         '   2.4 y  '   2.5
        Dim cRet As String

        cRet = (CABIP4i + CStr(num) + CABIP4f)
        cRet = cRet + (CABIP5i + ComaPorPunto(Trim(Format(import, "#######0.00"))) + CABIP5f)

        Cadena = Cadena + cRet
        TotalOperaciones = cRet
    End Function

    '
    '  Ejemplo:
    '   <DbtrAcct>
    '       <Id>
    '           <IBAN>ES8700301772490003023271</IBAN>
    '       </Id>
    '   </DbtrAcct>
    '
    Private Function ProcesarRecibos(Fichero, SufOrd19)
        Dim i As Integer
        'Dim aTiposDePago(4) As String
        Dim no As Integer
        Dim cOrd As String
        Dim MandatoModificado As Boolean
        Dim QueModifico As String
        Dim cIban As String
        Dim cRefUnica As String
        Dim CtaModif As String
        Dim BICModif As String
        Dim FechaFirma As String
        Dim HayModificaciones As Boolean
        Dim import As Double
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsMandatos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CodigoDestinatario2 As String
        Dim Nombre2 As String
        Dim bic As String
        Dim IbanBan As String
        Dim NumRecibo As String

        aTiposDePago(1) = "RCUR"
        aTiposDePago(2) = "FRST"
        aTiposDePago(3) = "FNAL"
        aTiposDePago(4) = "OOFF"

        aRecibosTPag(1) = 0
        aRecibosTPag(2) = 0
        aRecibosTPag(3) = 0
        aRecibosTPag(4) = 0

        aImportesTPa(1) = 0#
        aImportesTPa(2) = 0#
        aImportesTPa(3) = 0#
        aImportesTPa(4) = 0#

        For i = 1 To Cuantos

            'Print #101, "Proceso procesar recibos  remesa inicio n mero " & Fichero & " recibo n mero " & i & " hora " & Time

            If Trim(DiscCargos(Fichero)) = Format(CInt(LvCargos.Items.Item(i).subitems(13).text), "0000") + Trim(LvCargos.Items.Item(i).subitems(14).text) Then
                'PREPARACION DE DATOS
                Rs.Open("SELECT * FROM Bancos WHERE codigo_banco = '" & Right("0000" + Trim(LvCargos.Items.Item(i).subitems(4).text), 4) & "'", ObjetoGlobal.cn,,,,,,, trans)
                CodigoDestinatario2 = Right("000000000000" + LvCargos.Items.Item(i).subitems(0).text, 14)
                Nombre2 = ETxt(Trim(LvCargos.Items.Item(i).subitems(1).text))
                IbanBan = Trim(LvCargos.Items.Item(i).subitems(2).text) + Right("00" + Trim(LvCargos.Items.Item(i).subitems(3).text), 2) + Right("0000" + Trim(LvCargos.Items.Item(i).subitems(4).text), 4) + Right("0000" + Trim(LvCargos.Items.Item(i).subitems(5).text), 4) + Right("00" + Trim(LvCargos.Items.Item(i).subitems(6).text), 2) + Right("0000000000" + Trim(LvCargos.Items.Item(i).subitems(7).text), 10)
                import = Math.Round(CDbl(LvCargos.Items.Item(i).subitems(8).text), 2)

                If CalculaIban Then
                    IbanBan = IBANCalculo("ES", Right("0000" + Trim(LvCargos.Items.Item(i).subitems(4).text), 4) + Right("0000" + Trim(LvCargos.Items.Item(i).subitems(5).text), 4) + Right("00" + Trim(LvCargos.Items.Item(i).subitems(6).text), 2) + Right("0000000000" + Trim(LvCargos.Items.Item(i).subitems(7).text), 10))
                End If

                cRefUnica = Right("000000000000" + LvCargos.Items.Item(i).subitems(0).text, 14)
                If Rs.RecordCount > 0 Then
                    bic = Rs!bic
                Else
                    bic = "           "
                End If

                'If Not IBANValidacion(IbanBan) Then
                'HayErrores = True
                'Print #102, Trim(Nombre1) & " Iban err nero "
                'End If

                If Trim(LvCargos.Items.Item(i).subitems(14).text) = "Telefon a" Then
                    RellenaRegistroCargos(i, import, CodigoDestinatario2, Trim(LvCargos.Items.Item(i).subitems(0).text))
                Else
                    RellenaRegistroCargos(i, import, CodigoDestinatario2, "")
                End If

                BloqueRecibos(i) = "RCUR"
                CuentaAnterior(i) = ""
                BicAnterior(i) = ""
                FirmaMandato(i) = "2009-10-31"
                ReferenciaUnica(i) = cRefUnica
                NumRecibo = "" & LvCargos.Items.Item(i).subitems(15).text
                NumeroRecibo(i) = "" & LvCargos.Items.Item(i).subitems(15).text

                ' Lectura del mandato en busca de modificaciones
                rsMandatos.Open("SELECT * FROM Mandatos WHERE empresa ='" & ObjetoGlobal.empresaactual & "' AND referencia='" & LvCargos.Items.Item(i).subitems(0).text & "' AND sufijo_emisor = '" & Trim(SufOrd19) & "'", ObjetoGlobal.cn, True,,,,,, trans)
                If Not rsMandatos.EOF Then  ' Existe el mandato
                    If Trim("" & rsMandatos!bic_anterior) <> "" Or Trim("" & rsMandatos!iban_anterior) <> "" Then
                        If CLng("0" & rsMandatos!Recibo) = 0 Or rsMandatos!Recibo = CDbl(NumRecibo) Then
                            If Trim("" & rsMandatos!IBAN) <> Trim(IbanBan) Then
                                MsgBox("La cuenta que figura en la ficha y la que figura en el mandato no son iguales, tiene que actualizarse la incorrecta:" + vbCrLf + Trim("" & rsMandatos!IBAN) + vbCrLf + Trim(IbanBan) + vbCrLf + "Ref:" + LvCargos.Items.Item(i).subitems(0).text)
                            End If
                            If Trim("" & rsMandatos!bic) <> Trim("" & bic) Then
                                MsgBox("La c digo BIC que figura en la ficha y el que figura en el mandato no son iguales, tiene que actualizarse el incorrecto")
                            End If
                            If Trim("" & rsMandatos!bic_anterior) <> "" Or (Trim(rsMandatos!bic) <> Trim(rsMandatos!bic_anterior)) Then
                                BloqueRecibos(i) = "FRST"
                                aRecibosTPag(2) = aRecibosTPag(2) + 1
                                aImportesTPa(2) = aImportesTPa(2) + Math.Round(CDbl(LvCargos.Items.Item(i).subitems(8).text), 2)
                            End If
                            CuentaAnterior(i) = Trim(rsMandatos!iban_anterior)
                            BicAnterior(i) = IIf(Trim(rsMandatos!bic) <> Trim(rsMandatos!bic_anterior), rsMandatos!bic_anterior, "")
                            If CLng("0" & rsMandatos!Recibo) = 0 Then
                                rsMandatos!Recibo = CLng(NumRecibo)
                                rsMandatos.Update()
                            End If
                        End If
                    End If
                End If
                rsMandatos.Close()
                Rs.Close()

                If BloqueRecibos(i) = "RCUR" Then ' No se han producido cambios
                    aRecibosTPag(1) = aRecibosTPag(1) + 1
                    aImportesTPa(1) = aImportesTPa(1) + Math.Round(CDbl(LvCargos.Items.Item(i).subitems(8).text), 2)
                End If
            End If
        Next

    End Function

    Private Function CheckIban(cban) As Boolean
        Dim cMod As String
        Dim cIban As String
        Dim cValor As String
        Dim IbanIban As String
        Dim cNum As String

        IbanIban = cban
        cban = Replace(cban, " ", "")
        cban = Replace(cban, "-", "")
        cban = Replace(cban, "/", "")
        cban = Replace(cban, "\", "")
        cban = Replace(cban, ":", "")
        cban = Replace(cban, ",", "")
        cban = Replace(cban, ".", "")
        cban = Replace(cban, "*", "")

        cban = Trim(cban)
        If Len(cban) < 24 Then
            CheckIban = False
            Exit Function
        End If

        cNum = PasaraNumero(Left(cban, 2))
        cIban = Mid(cban, 5) + cNum + "00"
        cValor = cIban

        cMod = Right("00" & (98 - CalculaModulo(cValor)), 2)

        If Left(cban, 2) + cMod + Mid(cban, 5) <> IbanIban Then
            CheckIban = False
            Exit Function
        End If

        CheckIban = True
    End Function


    Private Function PasaraNumero(cArg) As String
        Dim cValor As String
        Dim aLetra() As String
        Dim aNumero() As String
        Dim i As Integer

        aLetra = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        aNumero = {"10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35"}

        cValor = Replace(cArg, " ", "")
        cValor = Replace(cValor, "-", "")
        cValor = Replace(cValor, "/", "")
        cValor = Replace(cValor, "_", "")
        cValor = Replace(cValor, "\", "")
        cValor = Replace(cValor, ":", "")
        cValor = Trim(cValor)

        For i = 0 To UBound(aLetra)
            cValor = Replace(cValor, aLetra(i), aNumero(i))
        Next
        cValor = Trim(cValor)
        PasaraNumero = cValor
    End Function

    Private Function OrganizacionoPersona(cArg) As String()
        Dim aRet() As String
        aRet = {"", ""}
        cArg = LimpiaNif(cArg)
        If Left(cArg, 1) >= "A" And Left(cArg, 1) <= "Z" Then
            If Right(cArg, 1) >= "A" And Right(cArg, 1) <= "Z" Then  '   Tarjeta de extranger a
                aRet(1) = "2"
                aRet(2) = "J" + cArg
            Else
                aRet(1) = "1"
                aRet(2) = "I" + cArg
            End If
        Else
            If Left(cArg, 1) >= "1" And Left(cArg, 1) <= "9" Then
                aRet(1) = "2"
                aRet(2) = "J" + cArg
            Else
                MsgBox("error en el NIF")
                aRet(1) = "2"
                aRet(2) = "J" + cArg
            End If
        End If
        OrganizacionoPersona = aRet
    End Function

    Private Function LimpiaNif(cArg) As String
        Dim cCad As String
        Dim cRet As String
        Dim i As Integer

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

    Private Function CabeceraRemesa() As String()
        Dim cDireccion As String
        Dim cLocalidad As String
        Dim cProvincia As String
        Dim aRet() As String

        'aRet = Array("", "", "", "")
        entidad = Right("0000" + Trim(entidad), 4)
        oficina = Right("0000" + Trim(oficina), 4)
        dc = Right("00" + Trim(dc), 2)
        codcuenta = Right("0000000000" + Trim(codcuenta), 10)
        ReDim aRet(5)
        aRet(1) = ETxt(nombre)
        aRet(2) = LimpiaNif(NIF)
        aRet(3) = Trim(domicilio) + " " + Trim(cLocalidad) + " " + Trim(Poblacion)
        aRet(4) = IBANCalculo("ES", entidad & oficina & dc & codcuenta)

        Return aRet

    End Function

    '
    'Ejemplo
    '<Dbtr>
    '   <Nm>HERRANZ GARCIA ENRIQUE</Nm>
    '   <PstlAdr>
    '       <Ctry>ES</Ctry>
    '       <AdrLine>BECHINOS, 7 46370 CHIVA VALENCIA</AdrLine>
    '   </PstlAdr>
    '</Dbtr>
    '
    Private Function ModificacionMandato(CtaOri, BICOri) As String        '   2.57 y  '   2.58
        Dim cRet As String

        If Trim(CtaOri) <> "" And Trim(BICOri) = "" Then
            cRet = (RECI30i + RECI31i + Trim(CtaOri) + RECI31f + RECI30f)
        End If
        If Trim(BICOri) <> "" Then
            cRet = cRet + RECI30i + RECI32if + RECI30f
        End If

        ModificacionMandato = cRet
    End Function

    Private Function FormatoFecha(darg) As String
        FormatoFecha = (CStr(Year(darg)) + "-" + Right("00" & Month(darg), 2) + "-" + Right("00" & DateAndTime.Day(darg), 2))
    End Function

    Public Function TipoRemesa(Optional cArg As String = "") As String
        If cArg = "" Then
            TipoRemesa = cTipoRemesa
        Else
            cTipoRemesa = cArg
            TipoRemesa = cTipoRemesa
        End If
    End Function
    Public Function Modalidad(Optional cArg As String = "")
        If cArg = "" Then
            Modalidad = cModalidad
        Else
            cModalidad = cArg
            Modalidad = cModalidad
        End If
    End Function

    Private Function TipoDePago(sec) As String  '   2.6 , 2.7 a 2.12 , 2.14
        Dim cRet As String

        cRet = CABIP6i '   2.6

        If cModalidad <> "CORE" And cModalidad <> "B2B" Then
            cModalidad = "CORE"
        End If

        If cTipoRemesa = "FSDD" And cModalidad = "B2B" Then
            cModalidad = "CORE"
        End If

        cRet = cRet + CABIP7i + Trim(cModalidad) + CABIP7f  '   2.7 a 2.12
        cRet = cRet + CABIP8i  '   2.14

        Select Case sec
            Case "P"
                cRet = cRet + "FRST"
            Case "U"
                cRet = cRet + "FNAL"
            Case "R"
                cRet = cRet + "RCUR"
            Case "X"
                cRet = cRet + "OOFF"
            Case Else
                cRet = cRet + "RCUR"
        End Select

        cRet = cRet + CABIP8f
        cRet = cRet + (CABIP9i + "TRAD" + CABIP9f)  '   2.15 a 2.16
        cRet = cRet + CABIP6f

        Cadena = Cadena + cRet
        TipoDePago = cRet
    End Function

    Private Function Init(nArg, oArg)
        SacaIniciales()
    End Function

    Public Function HacerRemesa()
        Dim num_recibos As Integer
        Dim imp_recibos As Double
        Dim identificador As String
        Dim nombre_acreedor As String
        Dim IdentificadorPago As String
        Dim i As Integer
        Dim F As Integer
        Dim AbrPago() As String
        Dim aPresentador() As String
        Dim nReci As Long
        Dim no_recibo As Long
        Dim Bic_recibo As String
        Dim cIban As String
        Dim DetPago As String
        Dim cDIMidad As String
        Dim cProvincia As String
        Dim cRefUnica As String
        Dim CtaModif As String
        Dim BICModif As String
        Dim FechaFirma As String
        Dim cOrd As String
        Dim Modif As Boolean
        Dim Nombre2X As String
        Dim o As Integer
        Dim SufOrd19 As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim bic As String
        Dim Fichero As Integer
        Dim SQL As String
        Dim CodigoDestinatario2 As String
        Dim Nombre2 As String
        Dim IbanBan As String
        Dim importe2 As Double
        Dim trans As SqlClient.SqlTransaction


        CalculaIban = False
        For Fichero = 1 To NumDisc
            num_recibos = 0
            imp_recibos = 0#
            '   Antes de nada deberemos procesar los datos y generar los bloques por tipo de pago aaaa
            aTiposDePago(1) = "RCUR"
            aTiposDePago(2) = "FRST"
            aTiposDePago(3) = "FNAL"
            aTiposDePago(4) = "OOFF"

            aRecibosTPag(1) = 0
            aRecibosTPag(2) = 0
            aRecibosTPag(3) = 0
            aRecibosTPag(4) = 0

            aImportesTPa(1) = 0#
            aImportesTPa(2) = 0#
            aImportesTPa(3) = 0#
            aImportesTPa(4) = 0#

            ReDim AbrPago(5)
            AbrPago(1) = "R"
            AbrPago(2) = "P"
            AbrPago(3) = "U"
            AbrPago(4) = "X"

            '  aRecTiposDePago = { Tipo_pago (1..4), numero_recibos, importe_recibos, Recibos{}}
            ' Cabecera presentador
            SQL = "SELECT * FROM auxiliar_soporte WHERE empresa='" & ObjetoGlobal.empresaactual & "' AND codigo_norma = 19 AND cta_empresa = '" & cuenta & "'"
            Rs.Open(SQL, ObjetoGlobal.cn,,,,,,, trans)
            If Not Rs.EOF Then
                ArchivoEnCurso = 50 + Fichero
                Nombre2X = Trim(Mid(DiscCargos(Fichero), 5)) + " HORTO " + Trim(nombre)
                ObtenTotales(Fichero)

                'PREPARACION DE DATOS
                aPresentador = CabeceraRemesa()
                num_recibos = CuantosDisco(Fichero)
                imp_recibos = ImporteDisco(Fichero)
                '   Procesos
                InicioDocumento()
                Cabecera1()
                CabeceraPresentador()
                Mensaje()
                HoraEnvio()
                NumeroOperacionesC1(num_recibos)
                ImporteOperacionesC1(imp_recibos)
                SufOrd19 = Rs!sufijo_presentador

                IdentificadorPago = CalculoDelIdentificador("ES", aPresentador(2), SufOrd19)    '   M x 35, devuelve 34 y a adiremos 1,2,3, en el  ltimo d gito por bloque

                ParteIniciadora(aPresentador(1), Left(IdentificadorPago, 16))
                GrabarArchivo()
            End If
            Rs.Close()

            ' Procesamos recibos para emitir bloques de recibos ("RCUR", "FRST", "FNAL", "OOFF")
            ReDim BloqueRecibos(Cuantos)
            ReDim CuentaAnterior(Cuantos + 1)
            ReDim BicAnterior(Cuantos + 1)
            ReDim FirmaMandato(Cuantos + 1)
            ReDim ReferenciaUnica(Cuantos + 1)
            ReDim NumeroRecibo(Cuantos + 1)

            ' Procesamos recibos para emitir bloques de recibos ("RCUR", "FRST", "FNAL", "OOFF")
            ProcesarRecibos(Fichero, SufOrd19)

            ' Y ahora hacemos los distintos bloques de recibos
            For o = 1 To 4
                If aRecibosTPag(o) > 0 Then
                    'Cabeceras del bloque
                    IdPago(IdentificadorPago + Right("00" & i, 4)) '   Un identificador del pago Max35Str por bloque se pago

                    TotalOperaciones(aRecibosTPag(o), aImportesTPa(o))
                    ' AbrPago(i)  P Primero U Ultimo R Recurrente X  nico
                    TipoDePago(AbrPago(o))   '   Todo el bloque tendr  el mismo tipo
                    FechaDeCobro(FechaCargo) '   Fecha de cobro de la remesa
                    IdentificacionAcreedor(Trim(Left(aPresentador(1), 70)), Trim(Left(aPresentador(3), 70)))
                    CuentadeAbono(aPresentador(4)) '   Cuenta donde se abonar  la remesa
                    Rs.Open("SELECT * FROM Bancos WHERE codigo_banco = '" & Right("0000" + Trim(entidad), 4) & "'", ObjetoGlobal.cn,,,,,,, trans)
                    If Not Rs.EOF Then
                        BICPresentador = Trim(Rs!bic)
                    Else
                        BICPresentador = "NOTPROVIDED"
                    End If
                    Rs.Close()

                    BicAcreedor(BICPresentador)
                    CodigoIdentificacionAcreedor(Left(IdentificadorPago, 16))
                    GrabarArchivo()

                    For i = 1 To Cuantos
                        If aTiposDePago(o) = BloqueRecibos(i) Then ' Pertenece al mismo bloque
                            If Trim(DiscCargos(Fichero)) = Format(CInt(LvCargos.Items.Item(i).subitems(13).text), "0000") + Trim(LvCargos.Items.Item(i).subitems(14).text) Then

                                'PREPARACION DE DATOS
                                If PB.Value < PB.Max Then
                                    PB.Value = PB.Value + 1
                                    PB.Refresh
                                End If

                                Rs.Open("SELECT * FROM Bancos WHERE codigo_banco = '" & Right("0000" + Trim(LvCargos.Items.Item(i).subitems(4).text), 4) & "'", ObjetoGlobal.cn,,,,,,, trans)
                                CodigoDestinatario2 = Right("000000000000" + LvCargos.Items.Item(i).subitems(0).text, 14)
                                Nombre2 = ETxt(Trim(LvCargos.Items.Item(i).subitems(1).text))
                                IbanBan = Trim(LvCargos.Items.Item(i).subitems(2).text) + Right("00" + Trim(LvCargos.Items.Item(i).subitems(3).text), 2) + Right("0000" + Trim(LvCargos.Items.Item(i).subitems(4).text), 4) + Right("0000" + Trim(LvCargos.Items.Item(i).subitems(5).text), 4) + Right("00" + Trim(LvCargos.Items.Item(i).subitems(6).text), 2) + Right("0000000000" + Trim(LvCargos.Items.Item(i).subitems(7).text), 10)

                                If CalculaIban Then
                                    IbanBan = IBANCalculo("ES", Right("0000" + Trim(LvCargos.Items.Item(i).subitems(4).text), 4) + Right("0000" + Trim(LvCargos.Items.Item(i).subitems(5).text), 4) + Right("00" + Trim(LvCargos.Items.Item(i).subitems(6).text), 2) + Right("0000000000" + Trim(LvCargos.Items.Item(i).subitems(7).text), 10))
                                End If

                                cRefUnica = Right("000000000000" + LvCargos.Items.Item(i).subitems(0).text, 14)
                                importe2 = Math.Round(CDbl(LvCargos.Items.Item(i).subitems(8).text), 2)
                                If Rs.RecordCount > 0 Then
                                    bic = Trim(Rs!bic)
                                Else
                                    bic = "NOTPROVIDED"
                                    '                       Print #102, Trim(Nombre2) & " Error obteniendo el c digo BIC del banco "
                                End If

                                Bic_recibo = bic
                                cRefUnica = ReferenciaUnica(i)
                                FechaFirma = FirmaMandato(i)
                                'cOrd = aRecTiposDePago(i)(f)(4)  ?
                                BICModif = BicAnterior(i)
                                CtaModif = CuentaAnterior(i)
                                no_recibo = NumeroRecibo(i)
                                IdentificacionDelPago(no_recibo)
                                ImporteRecibo(importe2)
                                DetPago = Trim(LvCargos.Items.Item(i).subitems(9).text) + Trim(LvCargos.Items.Item(i).subitems(10).text)

                                If Trim(BICModif) <> "" Or Trim(CtaModif) <> "" Then
                                    Modif = True
                                Else
                                    Modif = False
                                End If
                                IdentificacionDeudor(cRefUnica, FechaFirma, Modif, BICModif, CtaModif) '   C digo deudor, fecha firma y si ha tenido modificaci n en la orden
                                '   Si se modif = true tendremos que poner el detalle de la modificaci n
                                BICRecibo(Bic_recibo)
                                DatosDeudor(ETxt(Trim(Nombre2))) ',ETxt(TRIM(Left(TRIM(oRecibos:RECDIRCLI) + " " +  TRIM(cDIMidad) + " " + TRIM(cProvincia),70)))
                                CuentraDeudor(IbanBan)
                                DetallePago(DetPago) '   L nea  nica para detalle del pago y observaciones
                                Rs.Close()
                                GrabarArchivo()
                            End If
                        End If
                    Next
                    FinIdentificacionPago()
                End If
            Next
            FinCabecera1()
            FinDocumento()
            GrabarArchivo()
            CerrarArchivo()
        Next

        HacerRemesa = True
    End Function

    Private Function ComaPorPunto(cImporte) As String
        ComaPorPunto = Replace(cImporte, ",", ".")
    End Function


    Private Function LimpiaXML(cTexto) As String
        Dim cRet As String
        Dim i As Integer
        Dim C As String

        cRet = ""
        cTexto = Trim(cTexto)
        For i = 1 To Len(cTexto)
            C = Mid(cTexto, i, 1)
            If InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrStuvwxyz0123456789/-?:()., + ", C) > 0 Then
                cRet = cRet + C
            Else
                cRet = cRet + " "
            End If
        Next

        LimpiaXML = cRet
    End Function


    Private Function IBANCalculo(ByVal Pais As String, ByVal cuenta As String) As String
        ' Recibe el pais con 2 letras (ES para Espa a)
        ' Recibe el n mero de cuenta

        'Dim Letras As String * 26
        Dim letras As String
        Dim IBAN As String
        Dim Dividendo As Integer
        Dim Resto As Integer
        Dim Contador As Integer

        ' Quita los posibles espacios
        cuenta = Replace(cuenta, " ", "")

        ' Calcula el valor de las letras, las quita y a ade el valor al final
        letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        IBAN = cuenta & CStr(InStr(1, letras, Left(Pais, 1)) + 9) & CStr(InStr(1, letras, Right(Pais, 1)) + 9) & "00"

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

        ' Calcula el valor de las letras, las quita y a ade el valor al final
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

        CalculoDelIdentificador = (Pais + cMod + SufOrd19 + NIF + "-" + DToS(Fecha_envio) + Replace(Hora_envio, ":", ""))
        'CalculoDelIdentificador = (Pais + cMod + SufOrd19 + NIF)
    End Function

    Private Sub ObtenTotales(Fichero)
        'Dim Cuantos As Integer
        Dim importe1 As Double
        Dim i As Integer

        For i = 1 To Cuantos
            If Trim(DiscCargos(Fichero)) = Format(CInt(LvCargos.Items.Item(i).subitems(13).text), "0000") + Trim(LvCargos.Items.Item(i).subitems(14).text) Then
                importe1 = Math.Round(CDbl(LvCargos.Items.Item(i).subitems(8).text), 2)
                CuantosDisco(Fichero) = CuantosDisco(Fichero) + 1
                EntradasDisco(Fichero) = EntradasDisco(Fichero) + 1
                EntradasDiscoOper(Fichero) = EntradasDiscoOper(Fichero) + 1
                ImporteDisco(Fichero) = Math.Round(ImporteDisco(Fichero) + Math.Round(importe1, 2), 2)
            End If
        Next

    End Sub

    Private Sub RellenaRegistroCargos(Fila, Importe, CodigoDestinatario, Referencia)
        Dim oRS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim oRSh As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim orden As Long
        Dim oRsAut As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Numero As Long

        If Trim(LvCargos.Items.Item(Fila).subitems(15).text) = "" Or Trim(LvCargos.Items.Item(Fila).subitems(15).text) = "0" Then ' Es nuevo
            SQL = "SELECT * FROM recibos WHERE 1=0"
            oRS.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            oRS.AddNew()

            If Trim("" & LvCargos.Items.Item(Fila).subitems(16).text) = "" Then
                oRS!empresa_recibo = ObjetoGlobal.empresaactual
            Else
                oRS!empresa_recibo = Trim(LvCargos.Items.Item(Fila).subitems(16).text)
            End If
            oRS!cargo_abono = "C" 'Trim(LvCargos.TextMatrix(Fila, 15))
            oRS!Documento = Trim(LvCargos.Items.Item(Fila).subitems(18).text)
            If Trim(Trim(LvCargos.Items.Item(Fila).subitems(20).text)) <> "" Then
                oRS!serie = Trim(LvCargos.Items.Item(Fila).subitems(19).text)
                oRS!Numero = Trim(LvCargos.Items.Item(Fila).subitems(20).text)
            Else
                oRsAut.Open("SELECT max(cast(numero as integer)) AS contador FROM recibos WHERE serie= 'Aut'", ObjetoGlobal.cn,,,,,,, trans)
                If Not oRsAut.EOF Then
                    If IsDBNull(oRsAut!Contador) Then
                        Numero = 1
                    Else
                        Numero = oRsAut!Contador + 1
                    End If
                Else
                    Numero = 1
                End If
                oRsAut.Close()
                oRS!serie = "Aut"
                oRS!Numero = "" & Format(Numero, "000000000")
            End If
            ' aaa
            If Trim(LvCargos.Items.Item(Fila).subitems(21).text) <> "" Then
                oRS!fecha_documento = CDate(LvCargos.Items.Item(Fila).subitems(21).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(22).text) <> "" Then
                oRS!fecha_recibo = CDate(LvCargos.Item(Fila).subitems(22).text)
            Else
                oRS!fecha_recibo = CDate(FechaRemesa)
            End If
            oRS!fecha_remesa = FechaRemesa 'CDate(LvCargos.TextMatrix(Fila, 21))
            If Trim(LvCargos.Item(Fila).subitems(24).text) <> "" Then
                oRS!cliente = CLng(LvCargos.Item(Fila).subitems(24).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(25).text) <> "" Then
                oRS!proveedor = CLng(LvCargos.Items.Item(Fila).subitems(25).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(26).text) <> "" Then
                oRS!socio = CLng(LvCargos.Items.Item(Fila).subitems(26).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(27).text) <> "" Then
                oRS!operario = CLng(LvCargos.Items.Item(Fila).subitems(27).text)
            End If
            oRS!NIF = Trim(LvCargos.Items.Item(Fila).subitems(28).text)

            oRS!Importe = Math.Abs(CDbl(LvCargos.Items.Item(Fila).subitems(29).text))
            oRS!gastos = Math.Abs(CDbl(LvCargos.Items.Item(Fila).subitems(30).text))
            oRS!aremesar = Math.Abs(CDbl(LvCargos.Items.Item(Fila).subitems(31).text))
            oRS!situacion = "R"
            oRS!procedencia = "N"
            oRS!codigo_referencia = Trim(CodigoDestinatario)
            oRS!Referencia = CDbl("0" & Referencia)
            oRS.Update()
            'oRS.Close
            SQL = "SELECT @@identity AS num_recibo"
            oRSh.Open(SQL, ObjetoGlobal.cn,,,,,,, trans)
            If Not oRSh.EOF Then
                'Print #101, "Grabado nuevo recibo " & oRSh!num_Recibo & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
                LvCargos.Items.Item(Fila).subitems(15).text = "" & oRSh!num_Recibo
                orden = oRSh!num_Recibo
                oRSh.Close()
                ' Y grabo el hist rico
                SQL = "SELECT * FROM recibos_h WHERE 1=0"
                oRSh.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
                oRSh.AddNew()
                oRSh!codigo = orden 'oRS!Recibo
                oRSh!Contador = 1
                oRSh!detalle = "Emisi n nueva remesa"
                oRSh!Fecha = CDate(FechaRemesa) '
                oRSh!Importe = Math.Abs(Importe)
                oRSh.Update()
                'Print #101, "Grabada l nea orden: 1 del recibo " & orden & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
                oRSh.Close()
                ' Anotamos el n mero de recibo en temp_cargos_abonos
                AnotarDatosRecibosCargos(Fila, orden)
            Else
                oRSh.Close()
                MsgBox("Error localizando identidad")
            End If
            oRS.Close()
        Else
            SQL = "SELECT * FROM recibos WHERE codigo=" & LvCargos.Items.Item(Fila).subitems(15).text
            oRS.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If oRS.RecordCount = 1 Then
                If oRS!situacion <> "R" Then
                    oRS!fecha_remesa = FechaRemesa
                    oRS!situacion = "R"
                    oRS.Update()
                    'Print #101, "Modificado recibo " & LvCargos.TextMatrix(Fila, 15) & " proceso: " & ProcesoActual & " " & Date.now & " hora " & Time
                    SQL = "SELECT max(contador) as orden FROM recibos_h WHERE codigo=" & LvCargos.Items.Item(Fila).subitems(15).text
                    oRSh.Open(SQL, ObjetoGlobal.cn,,,,,,, trans)
                    If IsDBNull(oRSh!orden) Then
                        orden = 1
                    Else
                        orden = oRSh!orden + 1
                    End If
                    oRSh.Close()

                    SQL = "SELECT * FROM recibos_h WHERE 1=0"
                    oRSh.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)

                    oRSh.AddNew()
                    oRSh!codigo = CLng(LvCargos.Items.Item(Fila).subitems(15).text)
                    oRSh!Contador = orden
                    oRSh!detalle = "Emisi n nueva remesa"
                    oRSh!Fecha = FechaRemesa
                    oRSh!Importe = Math.Abs(Importe)
                    oRSh.Update()
                    'Print #101, "A adida l nea orden: " & orden & " del recibo " & LvCargos.TextMatrix(Fila, 15) & " proceso: " & ProcesoActual & " " & Date.Now & " hora " & Time
                    oRSh.Close()
                Else
                    'Print #101, " Recibo " & LvCargos.TextMatrix(Fila, 15) & " ya remesado. " & ProcesoActual & " " & Date & " hora " & Time
                End If
            Else
                MsgBox("Error localizando recibo número " & LvCargos.Items.Item(Fila).subitems(15).text)
                'Print #101, "Error localizando recibo n mero " & LvCargos.TextMatrix(Fila, 15) & " proceso: " & ProcesoActual & " " & Date.Now & " hora " & Time
            End If
            oRS.Close()
        End If

    End Sub

    Private Function DToS(dfecha) As String
        Dim Dia, Mes, anno As String
        anno = "" & Year(dfecha)
        Mes = Right("00" & Month(dfecha), 2)
        Dia = Right("00" & DateAndTime.Day(dfecha), 2)
        DToS = anno & Mes & Dia
    End Function

    '    Private Sub CerrarTodos(Max As Integer)

    '        Dim i As Integer

    '        On Error GoTo errorescerrar

    '        For i = 1 To Max
    '            'Close #i
    '        Next
    '        Exit Sub

    'errorescerrar:
    '        Resume Next
    '    End Sub

    Public Function DiscoCargos(Arg() As String)
        DiscCargos = Arg
    End Function

    Private Sub AnotarDatosRecibosCargos(nFila, Recibo)
        Dim sSQL As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso='" & Trim(LvCargos.Items.Item(nFila).subitems(33).text) & "' AND codigo =" & LvCargos.Items.Item(nFila).subitems(34).text
        Rs.Open(sSQL, ObjetoGlobal.cn, True,,,,,, trans)
        If Not Rs.EOF Then
            Rs!Recibo = Recibo
            Rs.Update()
        End If
        Rs.Close()

    End Sub

End Class
