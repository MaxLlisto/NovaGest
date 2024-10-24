Public Class ClsFacturasRecibidas
    Private nEjercicio_ As String
    Private ObjetoGlobal As Object
    Private periodo_ As String
    Private Ejercicio_ As String
    Private NIF_ As String
    Private empresa_ As String
    Private tipo_comunicacion_ As String
    Private version As String
    Public XMLs As String
    Public Sql As String
    Private Porc_Compensacion As Integer
    Dim deducible As Double
    Dim Cadena2 As String

    Private Sub Class_Initialize()
        'Inicializar
        version = "1.1"
    End Sub
    Public WriteOnly Property Compensacion() As Integer

        Set(ByVal valor As Integer)
            Porc_Compensacion = valor
        End Set
    End Property

    Public WriteOnly Property ObjGlobal()
        Set(ByVal valor As Object)
            ObjetoGlobal = valor
        End Set
    End Property
    Public WriteOnly Property Ejercicio() As String
        Set(ByVal valor As String)
            nEjercicio_ = valor
        End Set

    End Property
    Public WriteOnly Property periodo() As String
        Set(ByVal valor As String)
            periodo_ = valor
        End Set
    End Property

    Public WriteOnly Property nif() As String
        Set(ByVal valor As String)
            NIF_ = LimpiaNif(valor)
        End Set
    End Property
    Public WriteOnly Property empresa() As String
        Set(ByVal valor As String)
            empresa_ = valor
        End Set
    End Property

    Public WriteOnly Property tipo_comunicacion() As String
        Set(ByVal valor As String)
            tipo_comunicacion_ = valor
        End Set
    End Property

    Public Function InicioTransmision() As Boolean
        CabeceraGeneral()
        CabeceraFacRecibida()
    End Function

    Public Function FinTransmision() As Boolean
        PieEnvio()
    End Function

    Private Function CabeceraGeneral()

        'XMLs = "<soapenv:Envelope xmlns:soapenv=" & Chr(34) & "http://schemas.xmlsoap.org/soap/envelope/& Chr(34) & "
        'XMLs = XMLs + "xmlns:siiLR = & Chr(34) & https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd & Chr(34) " & vbCrLf
        'XMLs = XMLs + "xmlns:sii=" & Chr(34) & "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd & Chr(34) & " > " & vbCrLf"
        'XMLs = XMLs + "<soapenv:Header/>" & vbCrLf
        'XMLs = XMLs + "<soapenv:Body>" & vbCrLf

        XMLs = "<?xml version=" + Chr(34) + "1.0" + Chr(34) + " encoding=" + Chr(34) + "UTF-8" + Chr(34) + "?>" + vbCrLf
        XMLs = XMLs + "<soapenv:Envelope xmlns:soapenv=" + Chr(34) + "http://schemas.xmlsoap.org/soap/envelope/" + Chr(34) + "" + vbCrLf
        XMLs = XMLs + "xmlns:siiLR = " + Chr(34) + "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd" + Chr(34) + "" + vbCrLf
        XMLs = XMLs + "xmlns:sii=" + Chr(34) + "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd" + Chr(34) + ">" + vbCrLf
        XMLs = XMLs + "<soapenv:Header/>" + vbCrLf
        XMLs = XMLs + "<soapenv:Body>" + vbCrLf


        '" & Chr(34) & "
    End Function

    Private Function CabeceraFacRecibida()

        XMLs = XMLs + "<siiLR:SuministroLRFacturasRecibidas>" + vbCrLf
        XMLs = XMLs + "<sii:Cabecera>" + vbCrLf
        XMLs = XMLs + "<sii:IDVersionSii>" & version & "</sii:IDVersionSii>" + vbCrLf
        XMLs = XMLs + "<sii:Titular>" + vbCrLf
        XMLs = XMLs + "<sii:NombreRazon>" & Trim(UCase(empresa_)) & "</sii:NombreRazon>" + vbCrLf
        XMLs = XMLs + "<sii:NIF>" & Trim(NIF_) & "</sii:NIF>" + vbCrLf
        XMLs = XMLs + "</sii:Titular>" + vbCrLf
        XMLs = XMLs + "<sii:TipoComunicacion>" & tipo_comunicacion_ & "</sii:TipoComunicacion>" + vbCrLf
        XMLs = XMLs + "</sii:Cabecera>" + vbCrLf


    End Function

    Private Sub PieEnvio()

        XMLs = XMLs + "</siiLR:SuministroLRFacturasRecibidas>" + vbCrLf
        XMLs = XMLs + "</soapenv:Body>" + vbCrLf
        XMLs = XMLs + "</soapenv:Envelope>" + vbCrLf
    End Sub

    Public ReadOnly Property envioXml()
        Get
            envioXml = XMLs
        End Get

    End Property

    Private Function RegistroFacturaRecibida(ejer, per)
        '-----------------------------------------
        ' Primer bloque de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<siiLR:RegistroLRFacturasRecibidas>" + vbCrLf
        'XMLs = XMLs + "<sii:PeriodoImpositivo>" + vbCrLf Ver. 1.0
        XMLs = XMLs + "<sii:PeriodoLiquidacion>" + vbCrLf ' Ver. 1.1
        XMLs = XMLs + "<sii:Ejercicio>" & ejer & "</sii:Ejercicio>" + vbCrLf
        XMLs = XMLs + "<sii:Periodo>" & per & "</sii:Periodo>" + vbCrLf
        'XMLs = XMLs + "</sii:PeriodoImpositivo>" + vbCrLf Ver. 1.0
        XMLs = XMLs + "</sii:PeriodoLiquidacion>" + vbCrLf ' Ver. 1.1
        Cadena2 = ejer & ";" & per
    End Function

    Private Function CabeceraFacturaRecibida(Serie_numero, fecha_factura, nif, EsNif, Optional IDType = "")
        '-----------------------------------------
        ' Cabecera de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<siiLR:IDFactura>" + vbCrLf
        XMLs = XMLs + "<sii:IDEmisorFactura>" + vbCrLf
        If EsNif Then
            XMLs = XMLs + "<sii:NIF>" & Trim(LimpiaNif(nif)) & "</sii:NIF>" + vbCrLf
        Else
            XMLs = XMLs + "<sii:IDOtro>" + vbCrLf
            'XMLs = XMLs + "<sii:CodigoPais>" & codigo_pais & "</sii:CodigoPais>"
            XMLs = XMLs + "<sii:IDType>" & IDType & "</sii:IDType>" + vbCrLf
            XMLs = XMLs + "<sii:ID>" & LimpiaNif(nif) & "</sii:ID>" + vbCrLf
            XMLs = XMLs + "</sii:IDOtro>" + vbCrLf
        End If
        XMLs = XMLs + "</sii:IDEmisorFactura>" + vbCrLf
        XMLs = XMLs + "<sii:NumSerieFacturaEmisor>" & Trim(Left(Serie_numero, 60)) & "</sii:NumSerieFacturaEmisor>" + vbCrLf
        XMLs = XMLs + "<sii:FechaExpedicionFacturaEmisor>" & fecha_factura & "</sii:FechaExpedicionFacturaEmisor>" + vbCrLf
        XMLs = XMLs + "</siiLR:IDFactura>" + vbCrLf

    End Function


    Private Function DesgloseIVAFactura(oRsTot As cnRecordset.CnRecordset)
        Dim TipoIVA(5) As String
        Dim baseiva(5) As String
        Dim cuotaIVA(5) As String
        Dim TipoRec(5) As String
        Dim baseRec(5) As String
        Dim cuotaRec(5) As String
        Dim Orden As Integer
        Dim i As Integer
        Dim TieneExentos As Boolean

        Orden = 0
        deducible = 0#

        While Not oRsTot.EOF
            Orden = Orden + 1
            TipoIVA(Orden) = Format(oRsTot!porcentaje, "#0")
            baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
            cuotaIVA(Orden) = Replace(Format(oRsTot!importe_cargo, "#########0.00"), ",", ".")
            deducible = deducible + oRsTot!importe_cargo
            oRsTot.MoveNext()
        End While

        XMLs = XMLs + "<sii:DesgloseFactura>" + vbCrLf
        XMLs = XMLs + "<sii:DesgloseIVA>" + vbCrLf
        For i = 1 To Orden
            XMLs = XMLs + "<sii:DetalleIVA>" + vbCrLf
            XMLs = XMLs + "<sii:TipoImpositivo>" & Trim(TipoIVA(i)) & "</sii:TipoImpositivo>" + vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(i)) & "</sii:BaseImponible>" + vbCrLf
            XMLs = XMLs + "<sii:CuotaSoportada>" & Trim(cuotaIVA(i)) & "</sii:CuotaSoportada>" + vbCrLf
            XMLs = XMLs + "</sii:DetalleIVA>"
            Cadena2 = Cadena2 & ";" & Trim(baseiva(i)) & ";" & Trim(TipoIVA(i)) & ";" & Trim(cuotaIVA(i))
        Next
        For i = i To 3
            Cadena2 = Cadena2 & ";0.00;0.00;0.00"
        Next
        XMLs = XMLs + "</sii:DesgloseIVA>" + vbCrLf
        XMLs = XMLs + "</sii:DesgloseFactura>" + vbCrLf
        'XMLs = XMLs + "</siiLR:FacturaExpedida>"
        'XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>"
    End Function

    Private Function DesgloseIVAFacturaCE(oRsTot As cnRecordset.CnRecordset)
        Dim TipoIVA(5) As String
        Dim baseiva(5) As String
        Dim cuotaIVA(5) As String
        Dim TipoRec(5) As String
        Dim baseRec(5) As String
        Dim cuotaRec(5) As String
        Dim Orden As Integer
        Dim i As Integer
        Dim TieneExentos As Boolean

        Orden = 0
        deducible = 0#
        While Not oRsTot.EOF
            Orden = Orden + 1
            TipoIVA(Orden) = Format(21, "#0")
            baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
            cuotaIVA(Orden) = Replace(Format(Math.Round(oRsTot!Base * 0.21, 2), "#########0.00"), ",", ".")
            deducible = deducible + Math.Round(oRsTot!Base * 0.21, 2)
            oRsTot.MoveNext()
        End While

        XMLs = XMLs + "<sii:DesgloseFactura>" + vbCrLf
        XMLs = XMLs + "<sii:DesgloseIVA>" + vbCrLf
        For i = 1 To Orden
            XMLs = XMLs + "<sii:DetalleIVA>" + vbCrLf
            XMLs = XMLs + "<sii:TipoImpositivo>" & Trim(TipoIVA(i)) & "</sii:TipoImpositivo>" + vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(i)) & "</sii:BaseImponible>" + vbCrLf
            XMLs = XMLs + "<sii:CuotaSoportada>" & Trim(cuotaIVA(i)) & "</sii:CuotaSoportada>" + vbCrLf
            XMLs = XMLs + "</sii:DetalleIVA>"
            Cadena2 = Cadena2 & ";" & Trim(baseiva(i)) & ";" & Trim(TipoIVA(i)) & ";" & Trim(cuotaIVA(i))
        Next
        For i = i To 3
            Cadena2 = Cadena2 & ";0.00;0.00;0.00"
        Next
        XMLs = XMLs + "</sii:DesgloseIVA>" + vbCrLf
        XMLs = XMLs + "</sii:DesgloseFactura>" + vbCrLf
        'XMLs = XMLs + "</siiLR:FacturaExpedida>"
        'XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>"
    End Function

    Private Function DesgloseIVAFacturaISP(oRsTot As cnRecordset.CnRecordset)
        Dim TipoIVA(5) As String
        Dim baseiva(5) As String
        Dim cuotaIVA(5) As String
        Dim TipoRec(5) As String
        Dim baseRec(5) As String
        Dim cuotaRec(5) As String
        Dim Orden As Integer
        Dim i As Integer

        Orden = 0
        deducible = 0#
        While Not oRsTot.EOF
            Orden = Orden + 1
            '    If oRsTot!porcentaje <> 0 Then
            TipoIVA(Orden) = Format(21, "##")
            baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
            cuotaIVA(Orden) = Replace(Format(Math.Round(oRsTot!Base * 0.21, 2), "#########0.00"), ",", ".")
            deducible = deducible + Math.Round(oRsTot!Base * 0.21, 2)
            '    End If
            oRsTot.MoveNext()
        End While

        XMLs = XMLs + "<sii:DesgloseFactura>" + vbCrLf
        XMLs = XMLs + "<sii:InversionSujetoPasivo>" + vbCrLf
        For i = 1 To Orden
            XMLs = XMLs + "<sii:DetalleIVA>" + vbCrLf
            XMLs = XMLs + "<sii:TipoImpositivo>" & Trim(TipoIVA(i)) & "</sii:TipoImpositivo>" + vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(i)) & "</sii:BaseImponible>" + vbCrLf
            XMLs = XMLs + "<sii:CuotaSoportada>" & Trim(cuotaIVA(i)) & "</sii:CuotaSoportada>" + vbCrLf
            XMLs = XMLs + "</sii:DetalleIVA>" + vbCrLf
            'cadena2 = cadena2 & ";" & Trim(baseiva(i)) & ";" & Trim(TipoIVA(i)) & ";" & Trim(cuotaIVA(i))
        Next
        XMLs = XMLs + "</sii:InversionSujetoPasivo>" + vbCrLf
        XMLs = XMLs + "</sii:DesgloseFactura>" + vbCrLf
        'XMLs = XMLs + "</siiLR:FacturaExpedida>"
        'XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>"
    End Function


    Public Function FacturaRecepcion(ByRef pb) As Boolean
        Dim tipo_factura As String
        Dim Trascendencia As String
        Dim Tipo_operacion As String
        Dim razon As String
        Dim cif As String
        Dim TipoNoExenta As String
        Dim TipoRectificativa As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTot As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Serie_numero As String
        Dim fecha_factura As String
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAnteriores As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NoEnvio As Long
        Dim EsComunitaria As Boolean
        Dim TieneRetencion As Boolean
        Dim Tipo_documento As String
        Dim Imp_deducible As String
        Dim RsPais As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPais As String
        Dim TieneCabecera As Boolean
        Dim tipofactura As String
        Dim HayRegistros As Boolean
        Dim periodo As String
        Dim EjercicioFactura As String
        Dim RsBloque As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias
        Dim trans As SqlClient.SqlTransaction
        Dim SerFacClient As String

        FacturaRecepcion = True

        Tipo_operacion = "Compra"
        TipoNoExenta = "S1"
        tipo_factura = "F1"
        tipo_comunicacion_ = "A0"


        Try

            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            RsEnviadas.Open("Select max(numero_envio) AS no FROM factura_com_aeat", ObjetoGlobal.cn, ,,,,,, trans)

            If RsEnviadas.EOF Then
                NoEnvio = 1
            Else
                NoEnvio = IIf(IsDBNull(RsEnviadas!no), 1, RsEnviadas!no + 1)
            End If
            RsEnviadas.Close()

            RsEnviadas.Open("Select * FROM factura_com_aeat where 1=0", ObjetoGlobal.cn, True,,,,,, trans)

            Rs.Open("SELECT * FROM Factura_com_c WHERE " & Sql, ObjetoGlobal.cn,,,,,,, trans)

            If Rs.RecordCount = 0 Then
                Rs.Close()
                RsEnviadas.Close()
                MsgBox("No hay registros")
                FacturaRecepcion = False

                trans.Rollback()
                ObjetoGlobal.cn.Close()

                Return False
            End If

            pb.Min = 0
            pb.max = Rs.RecordCount
            pb.value = 0

            While Not Rs.EOF

                SerFacClient = "SERIE/NUMERO: " & Trim(Rs!serie_factura_com) & " - " & Trim("" & Rs!numero_fra_com) & " PROVEEDOR: " & Rs!codigo_proveedor & " el error es: "

                RsAnteriores.Open("Select * from factura_com_aeat where empresa = '" & Trim(Rs!empresa) & "' AND numero_entrada =" & Rs!numero_entrada_fra, ObjetoGlobal.cn, True,,,,,, trans)
                If RsAnteriores.EOF Then
                    If Not TieneCabecera Then
                        InicioTransmision()
                        TieneCabecera = True
                    End If
                    HayRegistros = True

                    Imp_deducible = "0"
                    deducible = 0#
                    Trascendencia = "01"

                    pb.value = pb.value + 1
                    Application.DoEvents()
                    EsComunitaria = False

                    tipofactura = "F1" ' Normal F2 Ticket R1 Rectificativa

                    If Trim(Rs!tipo_compra) = "O" Or Trim(Rs!tipo_compra) = "CO" Then
                        tipo_factura = "COMPENSACION"
                        Trascendencia = "02"
                    ElseIf Trim(Rs!tipo_compra) = "I" Then
                        tipo_factura = "INVERSION"
                    ElseIf Trim(Rs!tipo_compra) = "CE" Then
                        tipo_factura = "CE"
                        EsComunitaria = True
                        Trascendencia = "09"
                    ElseIf Trim(Rs!tipo_compra) = "SP" Then
                        tipo_factura = "ISP" ' Inversión del sujeto pasivo
                    ElseIf Trim(Rs!tipo_compra) = "SG" Then
                        tipo_factura = "SG" ' Inversión del sujeto pasivo
                    Else
                        tipo_factura = "NORMALES"
                    End If

                    '            EsComunitaria = False
                    '            SqlPais = "SELECT p.*, pp.abreviatura, pp.comunitario_sn FROM provincias_coop p LEFT JOIN paises_coop pp ON pp.codigo_pais = p.codigo_pais where p.provincia = '" & Rs!provincia_prov & "'"
                    '            RsPais.Open SqlPais, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
                    '            If Not RsPais.EOF Then
                    '               If Trim(RsPais!comunitario_sn) = "S" And Trim(RsPais!codigo_pais) <> "ES" Then
                    '                  EsComunitaria = True
                    '               End If
                    '            End If
                    '            RsPais.Close


                    RsBloque.Open("SELECT * FROM asig_bloques_com WHERE empresa = '" & Trim(Rs!empresa) & "' AND bloque_liquid = '" & Rs!bloque_liquid & "'", ObjetoGlobal.cn,,,,,,, trans)

                    periodo = ""
                    EjercicioFactura = ""
                    If Not RsBloque.EOF Then
                        periodo = Right("00" & Month(RsBloque!fecha_inicio), 2)
                        EjercicioFactura = Right("00" & Year(RsBloque!fecha_inicio), 4)
                    End If
                    RsBloque.Close()

                    If (periodo >= "07" And EjercicioFactura = "2017") Or EjercicioFactura > "2017" Then
                        RegistroFacturaRecibida(EjercicioFactura, periodo)
                        Serie_numero = Trim(Rs!serie_factura_com) & CStr(Rs!numero_fra_com)
                        fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
                        Cadena2 = Cadena2 & ";" & Serie_numero & ";" & fecha_factura & ";" & Rs!numero_entrada_fra & ";" & Rs!Fecha_entrada & ";" & tipo_factura & ";" & Rs!cif_prov & ";" & Trim("" & Rs!razon_social)
                        CabeceraFacturaRecibida(Serie_numero, fecha_factura, Rs!cif_prov, Not EsComunitaria, "02")
                        CabeceraRegistro(tipofactura, fecha_factura, Trascendencia, IIf(Trascendencia = "02", "LIQUIDACIÓN SOCIO", "COMPRAS GENERALES"))

                        RsTot.Open("SELECT * FROM Factura_com_c_tot WHERE empresa = '" & Rs!empresa & "' AND numero_entrada_fra = " & Rs!numero_entrada_fra & " and tipo_linea = 'I' order by numero_orden", ObjetoGlobal.cn,,,,,,, trans)

                        If tipo_factura = "ISP" Then
                            DesgloseIVAFacturaISP(RsTot)
                        ElseIf tipo_factura = "COMPENSACION" Then
                            DesgloseCompensacion(RsTot)
                        ElseIf tipo_factura = "CE" Then
                            DesgloseIVAFacturaCE(RsTot)
                        ElseIf tipo_factura = "SG" Then
                            DesgloseIVAFactura(RsTot)
                            deducible = 0
                        Else
                            DesgloseIVAFactura(RsTot)
                        End If

                        If Trim(Rs!empresa) = "11" Then
                            Imp_deducible = Replace(Format(deducible * (Porc_Compensacion / 100), "########0.00"), ",", ".")
                        Else
                            Imp_deducible = Replace(Format(deducible, "########0.00"), ",", ".")
                        End If

                        Cadena2 = Cadena2 & ";" & Imp_deducible

                        Contraparte(Trim(Rs!razon_social), Rs!cif_prov, Rs!Fecha_entrada, Imp_deducible, Not EsComunitaria, "02")

                        ' Grabación del registro
                        RsEnviadas.AddNew()
                        RsEnviadas!empresa = Rs!empresa
                        RsEnviadas!numero_entrada = Rs!numero_entrada_fra
                        RsEnviadas!numero_envio = NoEnvio
                        RsEnviadas!serie_factura_com = Rs!serie_factura_com
                        RsEnviadas!numero_fra_com = Rs!numero_fra_com
                        RsEnviadas!fecha_factura = Rs!fecha_factura
                        RsEnviadas!Fecha_registro = Rs!Fecha_entrada
                        RsEnviadas!fecha_envio = Date.Now
                        RsEnviadas!Situacion = "P"
                        RsEnviadas!codigo_proveedor = Rs!codigo_proveedor
                        RsEnviadas!cif_prov = Rs!cif_prov
                        RsEnviadas!csv = ""
                        RsEnviadas!tipo_factura = Left(tipo_factura, 2)
                        RsEnviadas!Serie_numero = Serie_numero
                        RsEnviadas.Update()
                        RsTot.Close()
                        PrintLine(2, "", Cadena2)
                    End If
                End If
                RsAnteriores.Close()
                Rs.MoveNext()
            End While
            RsEnviadas.Close()

            If TieneCabecera Then
                FinTransmision()
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()

            If HayRegistros Then
                Return True
            Else
                MsgBox("No hay registros")
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("Se ha producido un error al procesar la factura" & SerFacClient & ex.Message & " / " & ex.ToString, vbInformation, "")
            Return False
        End Try

        '--------------
        ' F1 - Factura
        ' F2 - Fac. simplificada
        ' F3 - Fac. sust. factura simplificada
        ' F4 - Resumen facturas
        ' R1 - Rectificativas
        ' R2
        ' R3
        ' R4
        '--------------
        ' where empresa = '1' and serie_factura_vta = 'A2017' AND tipo_linea = 'I'

        '--------------
        ' 01 Operación de régimen común
        ' 02 Exportación
        ' 06 Régimen especial grupo de entidades en IVA (Nivel Avanzado)
        ' 07 Régimen especial criterio de caja
        ' 08 Operaciones sujetas al IPSI  / IGIC
        ' 10 Cobros por cuenta de terceros de honorarios profesionales o de derechos derivados de
        '    la propiedad industrial, de autor u otros por cuenta de sus socios, asociados o
        '    colegiados efectuados por sociedades, asociaciones, colegios profesionales u otras
        '    entidades que realicen estas funciones de cobro
        ' 11 Operaciones de arrendamiento de local de negocio sujetas a retención
        ' 12 Operaciones de arrendamiento de local de negocio no  sujetos a retención

    End Function

    Private Function Contraparte(razon, cif, Fecha_registro, Cuota_deducible, EsNif, Optional IDType = "")
        '-----------------------------------------
        ' Información destinatario facturas emitidas"
        '-----------------------------------------
        razon = PreparaTextos(razon)
        XMLs = XMLs + "<sii:Contraparte>" + vbCrLf
        XMLs = XMLs + "<sii:NombreRazon>" & Trim(razon) & "</sii:NombreRazon>" + vbCrLf
        If EsNif Then
            XMLs = XMLs + "<sii:NIF>" & LimpiaNif(cif) & "</sii:NIF>" + vbCrLf
        Else
            XMLs = XMLs + "<sii:IDOtro>" + vbCrLf
            'XMLs = XMLs + "<sii:CodigoPais>" & codigo_pais & "</sii:CodigoPais>"
            XMLs = XMLs + "<sii:IDType>" & IDType & "</sii:IDType>" + vbCrLf
            XMLs = XMLs + "<sii:ID>" & LimpiaNif(cif) & "</sii:ID>" + vbCrLf
            XMLs = XMLs + "</sii:IDOtro>" + vbCrLf
        End If
        XMLs = XMLs + "</sii:Contraparte>" + vbCrLf
        XMLs = XMLs + "<sii:FechaRegContable>" & Fecha_registro & "</sii:FechaRegContable>" + vbCrLf
        XMLs = XMLs + "<sii:CuotaDeducible>" & Cuota_deducible & "</sii:CuotaDeducible>" + vbCrLf
        XMLs = XMLs + "</siiLR:FacturaRecibida>" + vbCrLf
        XMLs = XMLs + "</siiLR:RegistroLRFacturasRecibidas>" + vbCrLf
    End Function

    Private Function CabeceraRegistro(Tipo_fac, Fecha_Ope, Transc, Objeto)

        XMLs = XMLs + "<siiLR:FacturaRecibida>" + vbCrLf
        XMLs = XMLs + "<sii:TipoFactura>" & Trim(Tipo_fac) & "</sii:TipoFactura>" + vbCrLf
        XMLs = XMLs + "<sii:FechaOperacion>" & Fecha_Ope & "</sii:FechaOperacion>" + vbCrLf
        XMLs = XMLs + "<sii:ClaveRegimenEspecialOTrascendencia>" & Trim(Transc) & "</sii:ClaveRegimenEspecialOTrascendencia>" + vbCrLf
        XMLs = XMLs + "<sii:DescripcionOperacion>" & Trim(Objeto) & "</sii:DescripcionOperacion>" + vbCrLf

    End Function

    Private Function DesgloseCompensacion(oRsTot As cnRecordset.CnRecordset)
        Dim TipoIVA(5) As String
        Dim baseiva(5) As String
        Dim cuotaIVA(5) As String
        Dim TipoRec(5) As String
        Dim baseRec(5) As String
        Dim cuotaRec(5) As String
        Dim Orden As Integer
        Dim i As Integer

        Orden = 0
        deducible = 0#
        While Not oRsTot.EOF
            If oRsTot!porcentaje <> 0 Then
                Orden = Orden + 1
                TipoIVA(Orden) = Format(oRsTot!porcentaje, "##")
                baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
                cuotaIVA(Orden) = Replace(Format(oRsTot!importe_cargo, "#########0.00"), ",", ".")
                deducible = deducible + oRsTot!importe_cargo
            End If
            oRsTot.MoveNext()
        End While

        XMLs = XMLs + "<sii:DesgloseFactura>" + vbCrLf
        XMLs = XMLs + "<sii:DesgloseIVA>" + vbCrLf
        For i = 1 To Orden
            XMLs = XMLs + "<sii:DetalleIVA>" + vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(i)) & "</sii:BaseImponible>" + vbCrLf
            XMLs = XMLs + "<sii:PorcentCompensacionREAGYP>" & Trim(TipoIVA(i)) & "</sii:PorcentCompensacionREAGYP>" + vbCrLf
            XMLs = XMLs + "<sii:ImporteCompensacionREAGYP>" & Trim(cuotaIVA(i)) & "</sii:ImporteCompensacionREAGYP>" + vbCrLf
            XMLs = XMLs + "</sii:DetalleIVA>" + vbCrLf
            Cadena2 = Cadena2 & ";" & Trim(baseiva(i)) & ";" & Trim(TipoIVA(i)) & ";" & Trim(cuotaIVA(i))
        Next

        For i = i To 3
            Cadena2 = Cadena2 & ";0.00;0.00;0.00"
        Next

        XMLs = XMLs + "</sii:DesgloseIVA>" + vbCrLf
        XMLs = XMLs + "</sii:DesgloseFactura>" + vbCrLf
        'XMLs = XMLs + "</siiLR:FacturaExpedida>"
        'XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>"
    End Function



    Private Function FacturaModificacion()
        Dim tipo_factura As String
        Dim Trascendencia As String
        Dim Tipo_operacion As String
        Dim razon As String
        Dim cif As String
        Dim TipoNoExenta As String
        Dim TipoRectificativa As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTot As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Serie_numero As String
        Dim fecha_factura As String
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NoEnvio As Long
        Dim EsComunitaria As Boolean
        Dim TieneRetencion As Boolean
        Dim Tipo_documento As String
        Dim Imp_deducible As String
        Dim RsPais As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPais As String
        Dim SqlModif As String
        Dim tipofactura As String
        Dim periodo As String
        Dim EjercicioFactura As String
        Dim RsBloque As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim TieneCabecera As Boolean

        TieneCabecera = False


        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias

        Tipo_operacion = "Compra"
        TipoNoExenta = "S1"
        tipofactura = "F1"
        tipo_comunicacion_ = "A1"


        SqlModif = "SELECT * FROM factura_com_aeat WHERE tipo_movimiento = 'M' AND csv = '' and situacion = ''"
        RsEnviadas.Open(SqlModif, ObjetoGlobal.cn, True)

        While Not RsEnviadas.EOF

            If Not TieneCabecera Then
                InicioTransmision()
                TieneCabecera = True
            End If

            Imp_deducible = "0"
            deducible = 0
            Trascendencia = "01"
            EsComunitaria = False

            SqlModif = " EMPRESA = '" & RsEnviadas!empresa = "' and numero_entrada=" & RsEnviadas!numero_entrada
            Rs.Open("SELECT * FROM Factura_com_c WHERE " & SqlModif, ObjetoGlobal.cn)

            If Trim(Rs!tipo_compra) = "O" Or Trim(Rs!tipo_compra) = "CO" Then
                tipo_factura = "COMPENSACION"
                Trascendencia = "02"
            ElseIf Trim(Rs!tipo_compra) = "I" Then
                tipo_factura = "INVERSION"
            ElseIf Trim(Rs!tipo_compra) = "SG" Then
                tipo_factura = "SG"
            ElseIf Trim(Rs!tipo_compra) = "CE" Then
                tipo_factura = "CE"
                EsComunitaria = True
                Trascendencia = "09"
            ElseIf Trim(Rs!tipo_compra) = "SP" Then
                tipo_factura = "ISP" ' Inversión del sujeto pasivo
            Else
                tipo_factura = "NORMALES"
            End If

            periodo = ""
            EjercicioFactura = ""
            If Not RsBloque.EOF Then
                periodo = Right("00" & Month(RsBloque!fecha_inicio), 2)
                EjercicioFactura = Right("00" & Year(RsBloque!fecha_inicio), 4)
            End If
            RsBloque.Close()

            RegistroFacturaRecibida(EjercicioFactura, periodo)
            'RegistroFacturaRecibida
            Serie_numero = Trim(Rs!serie_factura_com) & CStr(Rs!numero_fra_com)
            fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
            CabeceraFacturaRecibida(Serie_numero, fecha_factura, Rs!cif_prov, Not EsComunitaria, "02")
            CabeceraRegistro(tipofactura, fecha_factura, Trascendencia, "COMPRAS GENERALES")

            RsTot.Open("SELECT * FROM Factura_com_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_com = '" & Rs!serie_factura_com & "' AND numero_fra_com=" & Rs!numero_fra_com & " and tipo_linea = 'I' order by tipo_orden", ObjetoGlobal.cn)

            If tipo_factura = "ISP" Then
                DesgloseIVAFacturaISP(RsTot)
            ElseIf tipo_factura = "COMPENSACION" Then
                DesgloseCompensacion(RsTot)
            ElseIf tipo_factura = "CE" Then
                DesgloseIVAFacturaCE(RsTot)
            ElseIf tipo_factura = "SG" Then
                DesgloseIVAFactura(RsTot)
                deducible = 0#
            Else
                DesgloseIVAFactura(RsTot)
            End If

            If Trim(Rs!empresa) = "11" Then
                Imp_deducible = Replace(Format(deducible * (Porc_Compensacion / 100), "########0.00"), ",", ".")
            Else
                Imp_deducible = Replace(Format(deducible, "########0.00"), ",", ".")
            End If

            Contraparte(Trim(Rs!razon_social), Rs!cif_prov, Rs!Fecha_entrada, Imp_deducible, Not EsComunitaria, "02")

            ' Grabación del registro
            RsEnviadas!fecha_envio = Date.Now
            RsEnviadas!Situacion = "P"
            RsEnviadas!csv = ""
            RsEnviadas!tipo_factura = Left(tipo_factura, 2)
            RsEnviadas!Serie_numero = Serie_numero
            RsEnviadas!cif_prov = Rs!cif_prov
            RsEnviadas.Update()
            RsTot.Close()
            RsEnviadas.MoveNext()
        End While


        If TieneCabecera Then
            FinTransmision()
        End If

        RsEnviadas.Close()

        '--------------
        ' F1 - Factura
        ' F2 - Fac. simplificada
        ' F3 - Fac. sust. factura simplificada
        ' F4 - Resumen facturas
        ' R1 - Rectificativas
        ' R2
        ' R3
        ' R4
        '--------------
        ' where empresa = '1' and serie_factura_vta = 'A2017' AND tipo_linea = 'I'

        '--------------
        ' 01 Operación de régimen común
        ' 02 Exportación
        ' 06 Régimen especial grupo de entidades en IVA (Nivel Avanzado)
        ' 07 Régimen especial criterio de caja
        ' 08 Operaciones sujetas al IPSI  / IGIC
        ' 10 Cobros por cuenta de terceros de honorarios profesionales o de derechos derivados de
        '    la propiedad industrial, de autor u otros por cuenta de sus socios, asociados o
        '    colegiados efectuados por sociedades, asociaciones, colegios profesionales u otras
        '    entidades que realicen estas funciones de cobro
        ' 11 Operaciones de arrendamiento de local de negocio sujetas a retención
        ' 12 Operaciones de arrendamiento de local de negocio no  sujetos a retención
    End Function

    Public Function ReenvioErroresFactura(Optional envio = 0)
        Dim tipo_factura As String
        Dim Trascendencia As String
        Dim Tipo_operacion As String
        Dim razon As String
        Dim cif As String
        Dim TipoNoExenta As String
        Dim TipoRectificativa As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTot As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Serie_numero As String
        Dim fecha_factura As String
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NoEnvio As Long
        Dim EsComunitaria As Boolean
        Dim TieneRetencion As Boolean
        Dim Tipo_documento As String
        Dim Imp_deducible As String
        Dim RsPais As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPais As String
        Dim SqlModif As String
        Dim tipofactura As String
        Dim periodo As String
        Dim HayRegistros As Boolean
        Dim EjercicioFactura As String
        Dim RsBloque As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim TieneCabecera As Boolean
        Dim trans As SqlClient.SqlTransaction
        Dim SerFacClient As String

        TieneCabecera = False

        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias

        Tipo_operacion = "Compra"
        TipoNoExenta = "S1"
        tipofactura = "F1"
        tipo_comunicacion_ = "A0"

        Try
            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            SqlModif = "SELECT * FROM factura_com_aeat WHERE numero_envio = " & envio & " AND csv = '' and situacion = 'E'"
            RsEnviadas.Open(SqlModif, ObjetoGlobal.cn, True,,,,,, trans)

            While Not RsEnviadas.EOF
                Imp_deducible = "0"
                deducible = 0
                Trascendencia = "01"
                EsComunitaria = False

                HayRegistros = True

                SerFacClient = "SERIE/NUMERO: " & Trim(RsEnviadas!Serie_Factura_Vta) & " - " & RsEnviadas!numero_factura & " CLIENTE: " & RsEnviadas!codigo_cliente & " Error: "

                If Not TieneCabecera Then
                    InicioTransmision()
                    TieneCabecera = True
                End If

                SqlModif = ""
                SqlModif = " EMPRESA = '" & RsEnviadas!empresa & "' and numero_entrada_fra=" & RsEnviadas!numero_entrada
                Rs.Open("SELECT * FROM Factura_com_c WHERE " & SqlModif, ObjetoGlobal.cn,,,,,,, trans)

                If Trim(Rs!tipo_compra) = "O" Or Trim(Rs!tipo_compra) = "CO" Then
                    tipo_factura = "COMPENSACION"
                    Trascendencia = "02"
                ElseIf Trim(Rs!tipo_compra) = "I" Then
                    tipo_factura = "INVERSION"
                ElseIf Trim(Rs!tipo_compra) = "CE" Then
                    tipo_factura = "CE"
                    EsComunitaria = True
                    Trascendencia = "09"
                ElseIf Trim(Rs!tipo_compra) = "SG" Then
                    tipo_factura = "SG"
                ElseIf Trim(Rs!tipo_compra) = "SP" Then
                    tipo_factura = "ISP" ' Inversión del sujeto pasivo
                Else
                    tipo_factura = "NORMALES"
                End If

                periodo = ""
                EjercicioFactura = ""

                RsBloque.Open("SELECT * FROM asig_bloques_com WHERE empresa = '" & Trim(Rs!empresa) & "' AND bloque_liquid = '" & Rs!bloque_liquid & "'", ObjetoGlobal.cn,,,,,,, trans)

                If Not RsBloque.EOF Then
                    periodo = Right("00" & Month(RsBloque!fecha_inicio), 2)
                    EjercicioFactura = Right("00" & Year(RsBloque!fecha_inicio), 4)
                End If
                RsBloque.Close()

                RegistroFacturaRecibida(EjercicioFactura, periodo)

                'RegistroFacturaRecibida
                Serie_numero = Trim(Rs!serie_factura_com) & CStr(Rs!numero_fra_com)
                fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
                CabeceraFacturaRecibida(Serie_numero, fecha_factura, Rs!cif_prov, Not EsComunitaria, "02")
                CabeceraRegistro(tipofactura, fecha_factura, Trascendencia, "COMPRAS GENERALES")

                'RsTot.Open "SELECT * FROM Factura_com_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_com = '" & Rs!serie_factura_com & "' AND numero_fra_com=" & Rs!numero_fra_com & " and tipo_linea = 'I' order by tipo_orden", ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
                RsTot.Open("SELECT * FROM Factura_com_c_tot WHERE empresa = '" & Rs!empresa & "' AND numero_entrada_fra = " & Rs!numero_entrada_fra & " and tipo_linea = 'I' order by numero_orden", ObjetoGlobal.cn,,,,,,, trans)

                deducible = 0#
                If tipo_factura = "ISP" Then
                    DesgloseIVAFacturaISP(RsTot)
                ElseIf tipo_factura = "COMPENSACION" Then
                    DesgloseCompensacion(RsTot)
                ElseIf tipo_factura = "CE" Then
                    DesgloseIVAFacturaCE(RsTot)
                ElseIf tipo_factura = "SG" Then
                    DesgloseIVAFactura(RsTot)
                    deducible = 0#
                Else
                    DesgloseIVAFactura(RsTot)
                End If

                If Trim(Rs!empresa) = "11" Then
                    Imp_deducible = Replace(Format(deducible * (Porc_Compensacion / 100), "########0.00"), ",", ".")
                Else
                    Imp_deducible = Replace(Format(deducible, "########0.00"), ",", ".")
                End If


                Contraparte(Trim(Rs!razon_social), Rs!cif_prov, Rs!Fecha_entrada, Imp_deducible, Not EsComunitaria, "02")

                ' Grabación del registro
                RsEnviadas!fecha_envio = Date.Now
                RsEnviadas!Situacion = "P"
                RsEnviadas!csv = ""
                RsEnviadas!tipo_factura = ""
                RsEnviadas!Serie_numero = Serie_numero
                RsEnviadas!cif_prov = Rs!cif_prov
                RsEnviadas.Update()
                RsTot.Close()
                Rs.Close()
                RsEnviadas.MoveNext()
            End While

            If TieneCabecera Then
                FinTransmision()
            End If

            RsEnviadas.Close()

            trans.Commit()
            ObjetoGlobal.cn.Close()

            If HayRegistros Then
                Return True
            Else
                MsgBox("No hay registros")
                Return False
            End If

            Return True

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            SerFacClient = "Se ha producido un error al procesar la factura" & SerFacClient & ex.Message & " / " & ex.ToString
            MsgBox(SerFacClient, vbInformation, "")
            Return False
        End Try

        '--------------
        ' F1 - Factura
        ' F2 - Fac. simplificada
        ' F3 - Fac. sust. factura simplificada
        ' F4 - Resumen facturas
        ' R1 - Rectificativas
        ' R2
        ' R3
        ' R4
        '--------------
        ' where empresa = '1' and serie_factura_vta = 'A2017' AND tipo_linea = 'I'

        '--------------
        ' 01 Operación de régimen común
        ' 02 Exportación
        ' 06 Régimen especial grupo de entidades en IVA (Nivel Avanzado)
        ' 07 Régimen especial criterio de caja
        ' 08 Operaciones sujetas al IPSI  / IGIC
        ' 10 Cobros por cuenta de terceros de honorarios profesionales o de derechos derivados de
        '    la propiedad industrial, de autor u otros por cuenta de sus socios, asociados o
        '    colegiados efectuados por sociedades, asociaciones, colegios profesionales u otras
        '    entidades que realicen estas funciones de cobro
        ' 11 Operaciones de arrendamiento de local de negocio sujetas a retención
        ' 12 Operaciones de arrendamiento de local de negocio no  sujetos a retención
    End Function


    Public Function ExisteRemesa()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPrueba As String
        ExisteRemesa = False
        SqlPrueba = Sql & " AND situacion = 'P'"

        Rs.Open("SELECT * FROM factura_com_aeat WHERE " & SqlPrueba, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("Hay registros pendientes de procesar con la fecha indicada")
            ExisteRemesa = True
            Rs.Close
            Exit Function
        End If
        Rs.Close

        SqlPrueba = Sql & " AND situacion = 'C'"
        Rs.Open("SELECT * FROM factura_com_aeat WHERE " & SqlPrueba, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("Atención, hay registro procesados con la fecha indicada")
        End If
        Rs.Close

    End Function
    Private Function LimpiaNif(nif) As String
        Dim ret As String

        ret = Replace(nif, " ", "")
        ret = Replace(ret, "-", "")
        ret = Replace(ret, "_", "")
        ret = Replace(ret, ".", "")

        LimpiaNif = ret

    End Function

    Private Function PreparaTextos(txt) As String
        Dim ret As String

        ret = Replace(txt, "&", "&amp;")
        ret = Replace(ret, "<", "&lt;")
        ret = Replace(ret, ">", "&gt;")

        PreparaTextos = ret

    End Function


End Class
