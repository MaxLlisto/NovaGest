﻿Public Class ClsTFacturasEmitidas
    Private nEjercicio_ As String
    Private ObjetoGlobal As Object
    Private periodo_ As String
    Dim Ejercicio_ As String
    Private NIF_ As String
    Private empresa_ As String
    Private tipo_comunicacion_ As String
    Private version As String
    Public XMLs As String
    Public Sql As String
    Const quote As String = """"
    Public Cadena2 As String


    Private Sub Class_Initialize()
        'Inicializar
        version = "1.1"
    End Sub

    Public WriteOnly Property ObjGlobal() As Object
        Set(oObj As Object)
            ObjetoGlobal = oObj
        End Set
    End Property

    Public WriteOnly Property Ejercicio As String
        Set(ByVal value As String)
            nEjercicio_ = value
        End Set
    End Property


    Public WriteOnly Property periodo() As String
        Set(ByVal value As String)
            periodo_ = value
        End Set
    End Property
    Public WriteOnly Property nif() As String
        Set(ByVal value As String)
            NIF_ = LimpiaNif(value)
        End Set

    End Property
    Public WriteOnly Property empresa() As String
        Set(ByVal value As String)
            empresa_ = value
        End Set

    End Property

    Public WriteOnly Property tipo_comunicacion() As String
        Set(ByVal value As String)
            tipo_comunicacion_ = value
        End Set
    End Property

    Public Function InicioTransmision() As Boolean
        CabeceraGeneral()
        CabeceraFacEmision()
    End Function

    Public Function FinTransmision() As Boolean
        PieEnvio()
    End Function

    Private Function CabeceraGeneral()

        'XMLs = "<?xml version=" & quote & "1.0" & quote & " encoding=" & quote & "UTF-8" & quote & "?>" & vbCrLf
        'XMLs = XMLs + "<soapenv:Envelope xmlns:soapenv=" & quote & "http://schemas.xmlsoap.org/soap/envelope/" & quote & "" & vbCrLf
        'XMLs = XMLs + "xmlns: siiLR = " & quote & "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd" & quote & "" & vbCrLf
        'XMLs = XMLs + "xmlns:sii=" & quote & "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd" & quote & ">" & vbCrLf
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

    Private Function CabeceraFacEmision()

        XMLs = XMLs + "<siiLR:SuministroLRFacturasEmitidas>" & vbCrLf
        XMLs = XMLs + "<sii:Cabecera>" & vbCrLf
        XMLs = XMLs + "<sii:IDVersionSii>" & version & "</sii:IDVersionSii>" & vbCrLf
        XMLs = XMLs + "<sii:Titular>" & vbCrLf
        XMLs = XMLs + "<sii:NombreRazon>" & Trim(UCase(empresa_)) & "</sii:NombreRazon>" & vbCrLf
        XMLs = XMLs + "<sii:NIF>" & Trim(NIF_) & "</sii:NIF>" & vbCrLf
        XMLs = XMLs + "</sii:Titular>" & vbCrLf
        XMLs = XMLs + "<sii:TipoComunicacion>" & tipo_comunicacion_ & "</sii:TipoComunicacion>" & vbCrLf
        XMLs = XMLs + "</sii:Cabecera>" & vbCrLf

    End Function

    Private Sub PieEnvio()

        XMLs = XMLs + "</siiLR:SuministroLRFacturasEmitidas>" & vbCrLf
        XMLs = XMLs + "</soapenv:Body>" & vbCrLf
        XMLs = XMLs + "</soapenv:Envelope>" & vbCrLf

    End Sub

    Public ReadOnly Property envioXml()
        Get
            envioXml = XMLs
        End Get

    End Property

    Public Function AltaFactura(ByRef pb)
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
        Dim TieneRetencion As Boolean
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAnteriores As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NoEnvio As Long
        Dim EsComunitaria As Boolean
        Dim RsPais As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPais As String
        Dim ImpresaCabecera As Boolean
        Dim Tipooperacion As String
        Dim cex As String
        Dim Emitida_terceros As String
        Dim HayRegistros As Boolean
        Dim Isp As Boolean
        Dim trans As SqlClient.SqlTransaction
        Dim SerFacClient As String

        HayRegistros = False
        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias

        Tipo_operacion = "Venta"
        TipoNoExenta = "S1"
        tipo_factura = "F1"
        Trascendencia = "01"
        tipo_comunicacion_ = "A0"
        Isp = False


        RsEnviadas.Open("Select max(numero_envio) AS no FROM factura_vta_aeat", ObjetoGlobal.cn)

        If RsEnviadas.EOF Then
            NoEnvio = 1
        Else
            NoEnvio = IIf(IsDBNull(RsEnviadas!no), 1, RsEnviadas!no + 1)
        End If
        RsEnviadas.Close()

        Try
            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            RsEnviadas.Open("Select * FROM factura_vta_aeat where 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            Rs.Open("SELECT * FROM Factura_vta_c WHERE " & Sql, ObjetoGlobal.cn,,,,,,, trans)

            pb.Min = 0
            pb.max = Rs.RecordCount
            pb.value = 0

            While Not Rs.EOF

                SerFacClient = "SERIE/NUMERO: " & Trim(Rs!Serie_Factura_Vta) & " - " & Rs!numero_factura & " CLIENTE: " & Rs!codigo_cliente & " el error es: "

                RsAnteriores.Open("Select * from factura_vta_aeat where empresa = '" & Trim(Rs!empresa) & "' AND serie_factura_vta ='" & Trim(Rs!serie_factura_vta) & "' and numero_factura=" & Rs!numero_factura, ObjetoGlobal.cn,,,,,,, trans)
                If RsAnteriores.EOF Then
                    If Not ImpresaCabecera Then
                        InicioTransmision()
                        ImpresaCabecera = True
                    End If
                    HayRegistros = True

                    pb.value = pb.value + 1
                    Application.DoEvents()

                    tipo_factura = "F1"
                    If Trim(Rs!cif) = "1" Or Rs!codigo_cliente = 1 Then
                        tipo_factura = "F2"
                    End If

                    Emitida_terceros = "N"
                    If Trim(Rs!serie_factura_vta) = "A" & CStr(Year(Rs!fecha_factura)) Then
                        Emitida_terceros = "S"
                    End If

                    If Left(Rs!serie_factura_vta, 1) = "R" Then
                        tipo_factura = "R1"
                    End If
                    EsComunitaria = False

                    EsComunitaria = IIf(Trim(Rs!tabla_iva_clientes) = "I", True, False)

                    SqlPais = "SELECT p.*, pp.abreviatura, pp.comunitario_sn FROM provincias_coop p LEFT JOIN paises_coop pp ON pp.codigo_pais = p.codigo_pais where p.provincia = '" & Rs!provincia_cliente & "'"
                    RsPais.Open(SqlPais, ObjetoGlobal.cn,,,,,,, trans)
                    RsPais.Close()
                    RegistroFacturaEmitida()
                    Serie_numero = Trim(Rs!serie_factura_vta) & CStr(Rs!numero_factura)
                    fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
                    RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and conc_cargo_dto = 'IRPF' order by tipo_linea", ObjetoGlobal.cn, ,,,,,, trans)
                    TieneRetencion = False
                    Trascendencia = "01"
                    Cadena2 = Cadena2 & ";" & Serie_numero & ";" & fecha_factura & ";" & IIf(TieneRetencion, "Con retención", "Sin retención")
                    If RsTot.RecordCount > 0 Then
                        TieneRetencion = True
                        Trascendencia = "11"
                    End If
                    RsTot.Close()
                    Tipo_operacion = VerTipoOperacion(Trim(Rs!serie_factura_vta))

                    Cadena2 = Cadena2 & ";" & Tipo_operacion & ";" & IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)) & ";" & Trim(Rs!cif)

                    CabeceraFacturaEmitida(Serie_numero, fecha_factura)

                    RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and tipo_linea = 'T'", ObjetoGlobal.cn,,,,,,, trans)

                    If tipo_factura = "R1" Then
                        If Not EsComunitaria Then
                            ContraparteRectificativa(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, True, "02", Emitida_terceros, RsTot!importe_cargo) ', Serie_numero, fecha_factura
                        Else
                            ContraparteRectificativa(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, False, "02", Emitida_terceros, RsTot!importe_cargo) ', Serie_numero, fecha_factura
                        End If
                    Else
                        If Not EsComunitaria Then
                            Contraparte(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, True, "02", Emitida_terceros, RsTot!importe_cargo)
                        Else
                            Contraparte(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, False, "02", Emitida_terceros, RsTot!importe_cargo)
                        End If
                    End If

                    RsTot.Close()
                    RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and tipo_linea = 'I' AND porcentaje <>  -1 order by tipo_linea", ObjetoGlobal.cn,,,,,,, trans)

                    cex = ""
                    Isp = False
                    ' Vamos si la serie tiene excención para ver el artículo
                    If Trim(Rs!serie_factura_vta) = "D20" Then
                        cex = "E1"
                    ElseIf Trim(Rs!serie_factura_vta) = "D84" Then
                        Isp = True
                    End If

                    DesgloseIVAFactura(RsTot, EsComunitaria, cex, Isp)
                    ' Grabamos la tabla de enviadas
                    RsEnviadas.AddNew()
                    RsEnviadas!empresa = Rs!empresa
                    RsEnviadas!serie_factura_vta = Trim(Rs!serie_factura_vta)
                    RsEnviadas!numero_factura = Rs!numero_factura
                    RsEnviadas!numero_envio = NoEnvio
                    RsEnviadas!fecha_factura = Rs!fecha_factura
                    RsEnviadas!fecha_envio = Date.Now
                    RsEnviadas!Situacion = "P"
                    RsEnviadas!csv = ""
                    RsEnviadas!tipo_factura = ""
                    RsEnviadas!codigo_cliente = Rs!codigo_cliente
                    RsEnviadas!cif = Rs!cif
                    RsEnviadas!Serie_numero = Serie_numero
                    RsEnviadas.Update()
                    RsTot.Close()
                    PrintLine(2, "", Cadena2)
                End If
                RsAnteriores.Close()
                Rs.MoveNext()
            End While
            RsEnviadas.Close()

            If ImpresaCabecera Then
                PieEnvio()
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()

            If HayRegistros Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("Se ha producido un error al procesar la factura" & SerFacClient & ex.Message & " / " & ex.ToString, vbInformation, "")
            Return false
        End Try

        'TipoNoExenta = "S1"
        'TipoIVA = "0.00"
        'baseiva = "0.00"
        'cuotaIVA = "0.00"
        'TipoRec = "0.00"
        'baseRec = "0.00"
        'cuotaRec = "0.00"
        'TipoNoExenta = "S1"
        'tipo_factura = "F1"
        'Trascendencia = "01"
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

        'Trascendencia = "01"
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



        'importe_factura = "00.00"
        'Tipo_operacion = "COMPRAXXXXX"
        'razon = ""
        'cif = ""
    End Function

    Private Function DesgloseIVAFactura(ByVal oRsTot As cnRecordset.CnRecordset, ByVal EsComunitaria As Boolean, Optional ByVal cex As String = "", Optional ByVal Isp As Boolean = False)
        Dim TipoIVA(5) As String
        Dim baseiva(5) As String
        Dim cuotaIVA(5) As String
        Dim TipoRec(5) As String
        Dim baseRec(5) As String
        Dim cuotaRec(5) As String
        Dim Orden As Integer
        Dim i As Integer
        Dim TotalBase As Double
        Dim base0 As Double

        Orden = 0
        TotalBase = 0
        oRsTot.MoveFirst()

        While Not oRsTot.EOF
            If (oRsTot!porcentaje > 0 Or (oRsTot!porcentaje = 0 And oRsTot!Base <> 0 And Trim(cex) = "")) And Not EsComunitaria And Not Isp Then
                Orden = Orden + 1
                TipoIVA(Orden) = Format(oRsTot!porcentaje, "#0")
                baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
                cuotaIVA(Orden) = Replace(Format(oRsTot!importe_cargo, "#########0.00"), ",", ".")
                TotalBase = TotalBase + oRsTot!Base
            ElseIf oRsTot!porcentaje = 0 And oRsTot!Base <> 0 And Not EsComunitaria And Isp Then
                base0 = base0 + oRsTot!Base
            ElseIf EsComunitaria Then
                TotalBase = TotalBase + oRsTot!Base
            ElseIf (oRsTot!porcentaje = 0 And oRsTot!Base = 0 And Trim(cex) = "") And Not EsComunitaria And Not Isp Then
                Orden = Orden + 1
                TipoIVA(Orden) = Format(oRsTot!porcentaje, "#0")
                baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
                cuotaIVA(Orden) = Replace(Format(oRsTot!importe_cargo, "#########0.00"), ",", ".")
                TotalBase = TotalBase + oRsTot!Base
            ElseIf cex <> "" Then
                base0 = base0 + oRsTot!Base
                TotalBase = TotalBase + oRsTot!Base
            End If
            oRsTot.MoveNext()
        End While

        XMLs = XMLs + "<sii:TipoDesglose>" & vbCrLf
        If EsComunitaria Then ' En las facturas comunitarias el desglose será siempre por el tipo de operación.
            XMLs = XMLs + "<sii:DesgloseTipoOperacion>" & vbCrLf
            XMLs = XMLs + "<sii:Entrega>" & vbCrLf
            XMLs = XMLs + "<sii:Sujeta>" & vbCrLf
            XMLs = XMLs + "<sii:Exenta>" & vbCrLf
            XMLs = XMLs + "<sii:DetalleExenta>" & vbCrLf ' Ver. 1.1
            XMLs = XMLs + "<sii:CausaExencion>E5</sii:CausaExencion>" & vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(Replace(Format(TotalBase, "#########0.00"), ",", ".")) & "</sii:BaseImponible>" & vbCrLf
            XMLs = XMLs + "</sii:DetalleExenta>" & vbCrLf ' Ver. 1.1
            XMLs = XMLs + "</sii:Exenta>" & vbCrLf
            XMLs = XMLs + "</sii:Sujeta>" & vbCrLf
            XMLs = XMLs + "</sii:Entrega>" & vbCrLf
            XMLs = XMLs + "</sii:DesgloseTipoOperacion>" & vbCrLf

            Cadena2 = Cadena2 & ";" & Trim(Replace(Format(TotalBase, "#########0.00"), ",", ".")) & "0,00;0,00"

        Else
            XMLs = XMLs + "<sii:DesgloseFactura>" & vbCrLf
            XMLs = XMLs + "<sii:Sujeta>" & vbCrLf

            If Isp Then
                XMLs = XMLs + "<sii:NoExenta>" & vbCrLf
                XMLs = XMLs + "<sii:TipoNoExenta>S2</sii:TipoNoExenta>" & vbCrLf
                XMLs = XMLs + "<sii:DesgloseIVA>" & vbCrLf

                XMLs = XMLs + "<sii:DetalleIVA>" & vbCrLf
                XMLs = XMLs + "<sii:TipoImpositivo>0</sii:TipoImpositivo>" & vbCrLf
                XMLs = XMLs + "<sii:BaseImponible>" & Trim(Replace(Format(base0, "#########0.00"), ",", ".")) & "</sii:BaseImponible>" & vbCrLf
                XMLs = XMLs + "<sii:CuotaRepercutida>0</sii:CuotaRepercutida>" & vbCrLf
                XMLs = XMLs + "</sii:DetalleIVA>" & vbCrLf

                Cadena2 = Cadena2 & ";" & Trim(baseiva(i)) & ";" & Trim(TipoIVA(i)) & ";" & Trim(cuotaIVA(i))

                XMLs = XMLs + "</sii:DesgloseIVA>" & vbCrLf
                XMLs = XMLs + "</sii:NoExenta>" & vbCrLf
            Else
                If base0 <> 0 And cex <> "" Then ' Tiene
                    XMLs = XMLs + "<sii:Exenta>" & vbCrLf
                    XMLs = XMLs + "<sii:DetalleExenta>" & vbCrLf ' Ver 1.1
                    XMLs = XMLs + "<sii:CausaExencion>" & cex & "</sii:CausaExencion>" & vbCrLf
                    XMLs = XMLs + "<sii:BaseImponible>" & Trim(Replace(Format(base0, "#########0.00"), ",", ".")) & "</sii:BaseImponible>" & vbCrLf
                    XMLs = XMLs + "</sii:DetalleExenta>" & vbCrLf ' Ver 1.1
                    XMLs = XMLs + "</sii:Exenta>" & vbCrLf
                    Cadena2 = Cadena2 & ";" & Trim(Replace(Format(TotalBase, "#########0.00"), ",", ".")) & "0,00;0,00"
                End If
                If Orden > 0 Then
                    XMLs = XMLs + "<sii:NoExenta>" & vbCrLf
                    XMLs = XMLs + "<sii:TipoNoExenta>S1</sii:TipoNoExenta>" & vbCrLf
                    XMLs = XMLs + "<sii:DesgloseIVA>" & vbCrLf

                    For i = 1 To Orden
                        XMLs = XMLs + "<sii:DetalleIVA>" & vbCrLf
                        XMLs = XMLs + "<sii:TipoImpositivo>" & Trim(TipoIVA(i)) & "</sii:TipoImpositivo>" & vbCrLf
                        XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(i)) & "</sii:BaseImponible>" & vbCrLf
                        XMLs = XMLs + "<sii:CuotaRepercutida>" & Trim(cuotaIVA(i)) & "</sii:CuotaRepercutida>" & vbCrLf
                        'XMLs = XMLs + "<sii:TipoRecargoEquivalencia>0</sii:TipoRecargoEquivalencia>" & vbCrLf
                        'XMLs = XMLs + "<sii:CuotaRecargoEquivalencia>0</sii:CuotaRecargoEquivalencia>" & vbCrLf
                        XMLs = XMLs + "</sii:DetalleIVA>" & vbCrLf
                        Cadena2 = Cadena2 & ";" & Trim(baseiva(i)) & ";" & Trim(TipoIVA(i)) & ";" & Trim(cuotaIVA(i))
                    Next
                    XMLs = XMLs + "</sii:DesgloseIVA>" & vbCrLf
                    XMLs = XMLs + "</sii:NoExenta>" & vbCrLf
                End If
            End If
            XMLs = XMLs + "</sii:Sujeta>" & vbCrLf
            XMLs = XMLs + "</sii:DesgloseFactura>" & vbCrLf
        End If
        XMLs = XMLs + "</sii:TipoDesglose>" & vbCrLf
        XMLs = XMLs + "</siiLR:FacturaExpedida>" & vbCrLf
        XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>" & vbCrLf
    End Function

    'Private Function DesgloseEntregaBienes(oRsTot As cnRecordset.cnRecordset = New cnRecordset.cnRecordset)
    'Dim TipoIVA(5) As String
    'Dim baseiva(5) As String
    'Dim cuotaIVA(5) As String
    'Dim TipoRec(5) As String
    'Dim baseRec(5) As String
    'Dim cuotaRec(5) As String
    'Dim Orden As Integer
    'Dim I As Integer
    '
    'Orden = 0
    'While Not oRsTot.EOF
    '    Orden = Orden + 1
    '    TipoIVA(Orden) = Format(oRsTot!porcentaje, "##")
    '    baseiva(Orden) = Replace(Format(oRsTot!base, "#########0.00"), ".", ",")
    '    cuotaIVA(Orden) = Replace(Format(oRsTot!importe_cargo, "#########0.00"), ".", ",")
    '    oRsTot.MoveNext
    'Wend
    '
    'XMLs = XMLs + "<sii:TipoDesglose>" & vbCrLf
    'XMLs = XMLs + "<sii:DesgloseTipoOperacion>" & vbCrLf
    'XMLs = XMLs + "<sii:Entrega>" & vbCrLf
    'XMLs = XMLs + "<sii:Sujeta>" & vbCrLf
    'XMLs = XMLs + "<sii:NoExenta>" & vbCrLf
    'XMLs = XMLs + "<sii:TipoNoExenta>" & TipoNoExenta & "</sii:TipoNoExenta>" & vbCrLf
    'XMLs = XMLs + "<sii:DesgloseIVA>" & vbCrLf
    '
    'For I = 1 To Orden
    '    XMLs = XMLs + "<sii:DetalleIVA>" & vbCrLf
    '    XMLs = XMLs + "<sii:TipoImpositivo>" & Trim(TipoIVA(Orden)) & "</sii:TipoImpositivo>" & vbCrLf
    '    XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(Orden)) & "</sii:BaseImponible>" & vbCrLf
    '    XMLs = XMLs + "<sii:CuotaRepercutida>" & Trim(cuotaIVA(Orden)) & "</sii:CuotaRepercutida>" & vbCrLf
    '    XMLs = XMLs + "<sii:TipoRecargoEquivalencia>0</sii:TipoRecargoEquivalencia>" & vbCrLf
    '    XMLs = XMLs + "<sii:CuotaRecargoEquivalencia>0</sii:CuotaRecargoEquivalencia>" & vbCrLf
    '    XMLs = XMLs + "</sii:DetalleIVA>" & vbCrLf
    'Next
    '
    'XMLs = XMLs + "</sii:DesgloseIVA>" & vbCrLf
    'XMLs = XMLs + "</sii:NoExenta>" & vbCrLf
    'XMLs = XMLs + "</sii:Sujeta>" & vbCrLf
    'XMLs = XMLs + "</sii:Entrega>" & vbCrLf
    'XMLs = XMLs + "</sii:DesgloseTipoOperacion>" & vbCrLf
    'XMLs = XMLs + "</sii:TipoDesglose>" & vbCrLf
    'XMLs = XMLs + "</siiLR:FacturaExpedida>" & vbCrLf
    'XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>" & vbCrLf
    'End Function


    Private Function DesglosePrestacionServicios(oRsTot As cnRecordset.CnRecordset, cex As String)
        Dim TipoIVA(5) As String
        Dim baseiva(5) As String
        Dim cuotaIVA(5) As String
        Dim TipoRec(5) As String
        Dim baseRec(5) As String
        Dim cuotaRec(5) As String
        Dim Orden As Integer
        Dim i As Integer
        Dim TotalBase As Double
        Dim base0 As Double

        Orden = 0
        TotalBase = 0
        oRsTot.MoveFirst()

        While Not oRsTot.EOF
            If oRsTot!porcentaje <> 0 Then
                Orden = Orden + 1
                TipoIVA(Orden) = Format(oRsTot!porcentaje, "##")
                baseiva(Orden) = Replace(Format(oRsTot!Base, "#########0.00"), ",", ".")
                cuotaIVA(Orden) = Replace(Format(oRsTot!importe_cargo, "#########0.00"), ",", ".")
                TotalBase = TotalBase + oRsTot!Base
            ElseIf oRsTot!porcentaje = 0 And oRsTot!Base <> 0 Then
                base0 = oRsTot!Base
            End If
            oRsTot.MoveNext()
        End While

        XMLs = XMLs + "<sii:TipoDesglose>" & vbCrLf
        XMLs = XMLs + "<sii:DesgloseTipoOperacion>" & vbCrLf
        XMLs = XMLs + "<sii:PrestacionServicios>" & vbCrLf
        XMLs = XMLs + "<sii:Sujeta>" & vbCrLf

        If base0 <> 0 Then ' Tiene
            XMLs = XMLs + "<sii:Exenta>" & vbCrLf
            XMLs = XMLs + "<sii:CausaExencion>" & cex & "</sii:CausaExencion>" & vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(Replace(Format(base0, "#########0.00"), ",", ".")) & "</sii:BaseImponible>" & vbCrLf
            XMLs = XMLs + "</sii:Exenta>" & vbCrLf
        End If

        XMLs = XMLs + "<sii:NoExenta>" & vbCrLf
        XMLs = XMLs + "<sii:TipoNoExenta>S1</sii:TipoNoExenta>" & vbCrLf
        XMLs = XMLs + "<sii:DesgloseIVA>" & vbCrLf

        For i = 1 To Orden
            XMLs = XMLs + "<sii:DetalleIVA>" & vbCrLf
            XMLs = XMLs + "<sii:TipoImpositivo>" & Trim(TipoIVA(Orden)) & "</sii:TipoImpositivo>" & vbCrLf
            XMLs = XMLs + "<sii:BaseImponible>" & Trim(baseiva(Orden)) & "</sii:BaseImponible>" & vbCrLf
            XMLs = XMLs + "<sii:CuotaRepercutida>" & Trim(cuotaIVA(Orden)) & "</sii:CuotaRepercutida>" & vbCrLf
            XMLs = XMLs + "<sii:TipoRecargoEquivalencia>0</sii:TipoRecargoEquivalencia>" & vbCrLf
            XMLs = XMLs + "<sii:CuotaRecargoEquivalencia>0</sii:CuotaRecargoEquivalencia>" & vbCrLf
            XMLs = XMLs + "</sii:DetalleIVA>" & vbCrLf
        Next

        XMLs = XMLs + "</sii:DesgloseIVA>" & vbCrLf
        XMLs = XMLs + "</sii:NoExenta>" & vbCrLf
        XMLs = XMLs + "</sii:Sujeta>" & vbCrLf
        XMLs = XMLs + "</sii:PrestacionServicios>" & vbCrLf
        XMLs = XMLs + "</sii:DesgloseTipoOperacion>" & vbCrLf
        XMLs = XMLs + "</sii:TipoDesglose>" & vbCrLf
        XMLs = XMLs + "</siiLR:FacturaExpedida>" & vbCrLf
        XMLs = XMLs + "</siiLR:RegistroLRFacturasEmitidas>" & vbCrLf
    End Function


    Private Function RegistroFacturaEmitida()
        '-----------------------------------------
        ' Primer bloque de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<siiLR:RegistroLRFacturasEmitidas>" & vbCrLf
        ' XMLs = XMLs + "<sii:PeriodoImpositivo>" & vbCrLf Versión 1.0
        XMLs = XMLs + "<sii:PeriodoLiquidacion>" & vbCrLf ' Versión 1.1
        XMLs = XMLs + "<sii:Ejercicio>" & nEjercicio_ & "</sii:Ejercicio>" & vbCrLf
        XMLs = XMLs + "<sii:Periodo>" & periodo_ & "</sii:Periodo>" & vbCrLf
        ' XMLs = XMLs + "</sii:PeriodoImpositivo>" & vbCrLf Ver. 1.0
        XMLs = XMLs + "</sii:PeriodoLiquidacion>" & vbCrLf ' Ver. 1.1
        Cadena2 = nEjercicio_ & ";" & periodo_
    End Function

    Private Function CabeceraFacturaEmitida(Serie_numero, fecha_factura)
        '-----------------------------------------
        ' Cabecera de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<siiLR:IDFactura>" & vbCrLf
        XMLs = XMLs + "<sii:IDEmisorFactura>" & vbCrLf
        XMLs = XMLs + "<sii:NIF>" & NIF_ & "</sii:NIF>" & vbCrLf
        XMLs = XMLs + "</sii:IDEmisorFactura>" & vbCrLf
        XMLs = XMLs + "<sii:NumSerieFacturaEmisor>" & Trim(Left(Serie_numero, 60)) & "</sii:NumSerieFacturaEmisor>" & vbCrLf
        XMLs = XMLs + "<sii:FechaExpedicionFacturaEmisor>" & fecha_factura & "</sii:FechaExpedicionFacturaEmisor>" & vbCrLf
        XMLs = XMLs + "</siiLR:IDFactura>" & vbCrLf
        Return True


    End Function

    Private Function Contraparte(tipo_factura, Trascendencia, razon, cif, Tipo_operacion, EsNif, Optional IDType = "", Optional Emitida_terceros = "N", Optional Importe_factura = 0)
        '-----------------------------------------
        ' Información destinatario facturas emitidas"
        '-----------------------------------------
        razon = PreparaTextos(razon)
        XMLs = XMLs + "<siiLR:FacturaExpedida>" & vbCrLf
        XMLs = XMLs + "<sii:TipoFactura>" & tipo_factura & "</sii:TipoFactura>" & vbCrLf
        XMLs = XMLs + "<sii:ClaveRegimenEspecialOTrascendencia>" & Trascendencia & "</sii:ClaveRegimenEspecialOTrascendencia>" & vbCrLf
        If Trim(Trascendencia) <> "11" Then
            XMLs = XMLs + "<sii:ImporteTotal>" & Replace(Format(Importe_factura, "########0.00"), ",", ".") & "</sii:ImporteTotal>" & vbCrLf
        End If
        XMLs = XMLs + "<sii:DescripcionOperacion>" & Tipo_operacion & "</sii:DescripcionOperacion>" & vbCrLf
        If Emitida_terceros = "S" Then
            'XMLs = XMLs + " <sii:EmitidaPorTercerosODestinatario>S</sii:EmitidaPorTercerosODestinatario>" & vbCrLf Ver 1.1
        End If
        If Trim(tipo_factura) <> "F2" Then
            XMLs = XMLs + "<sii:Contraparte>" & vbCrLf
            XMLs = XMLs + "<sii:NombreRazon>" & Trim(razon) & "</sii:NombreRazon>" & vbCrLf
            If EsNif Then
                XMLs = XMLs + "<sii:NIF>" & LimpiaNif(cif) & "</sii:NIF>" & vbCrLf
            Else
                XMLs = XMLs + "<sii:IDOtro>" & vbCrLf
                XMLs = XMLs + "<sii:IDType>" & IDType & "</sii:IDType>" & vbCrLf
                XMLs = XMLs + "<sii:ID>" & cif & "</sii:ID>" & vbCrLf
                XMLs = XMLs + "</sii:IDOtro>" & vbCrLf
            End If
            XMLs = XMLs + "</sii:Contraparte>" & vbCrLf
        End If
        Return True

    End Function

    Private Function ContraparteRectificativa(tipo_factura, Trascendencia, razon, cif, Tipo_operacion, EsNif, Optional IDType = "", Optional Emitida_terceros = "N", Optional Importe_factura = 0) '', Ser_numR, FechaR)
        '-----------------------------------------------------------
        ' Información destinatario facturas emitidas rectificativas"
        '-----------------------------------------------------------
        razon = PreparaTextos(razon)
        XMLs = XMLs + "<siiLR:FacturaExpedida>" & vbCrLf
        XMLs = XMLs + "<sii:TipoFactura>R1</sii:TipoFactura>" & vbCrLf
        XMLs = XMLs + "<sii:TipoRectificativa>I</sii:TipoRectificativa>" & vbCrLf
        'XMLs = XMLs + "<sii:FacturasRectificadas>"
        'XMLs = XMLs + "<sii:IDFacturaRectificada>"
        'XMLs = XMLs + "<sii:NumSerieFacturaEmisor>" & Trim(Ser_numR) & "</sii:NumSerieFacturaEmisor>"
        'XMLs = XMLs + "<sii:FechaExpedicionFacturaEmisor>" & Format(FechaR, "dd-mm-yyyy") & "</sii:FechaExpedicionFacturaEmisor>"
        'XMLs = XMLs + "</sii:IDFacturaRectificada>"
        'XMLs = XMLs + "</sii:FacturasRectificadas>"
        XMLs = XMLs + "<sii:ClaveRegimenEspecialOTrascendencia>" & Trascendencia & "</sii:ClaveRegimenEspecialOTrascendencia>" & vbCrLf
        XMLs = XMLs + "<sii:ImporteTotal>" & Replace(Format(Importe_factura, "########0.00"), ",", ".") & "</sii:ImporteTotal>" & vbCrLf
        XMLs = XMLs + "<sii:DescripcionOperacion>" & Tipo_operacion & "</sii:DescripcionOperacion>" & vbCrLf
        If Emitida_terceros = "S" Then
            'XMLs = XMLs + "<sii:EmitidaPorTercerosODestinatario>S</sii:EmitidaPorTercerosODestinatario>" & vbCrLf ' Ver 1.1
        End If
        XMLs = XMLs + "<sii:Contraparte>" & vbCrLf
        XMLs = XMLs + "<sii:NombreRazon>" & Trim(razon) & "</sii:NombreRazon>" & vbCrLf
        'XMLs = XMLs + "<sii:NIF>" & Trim(cif) & "</sii:NIF>"
        If EsNif Then
            XMLs = XMLs + "<sii:NIF>" & Trim(LimpiaNif(cif)) & "</sii:NIF>" & vbCrLf
        Else
            XMLs = XMLs + "<sii:IDOtro>" & vbCrLf
            'XMLs = XMLs + "<sii:CodigoPais>" & codigo_pais & "</sii:CodigoPais>"
            XMLs = XMLs + "<sii:IDType>" & IDType & "</sii:IDType>" & vbCrLf
            XMLs = XMLs + "<sii:ID>" & cif & "</sii:ID>" & vbCrLf
            XMLs = XMLs + "</sii:IDOtro>" & vbCrLf
        End If
        XMLs = XMLs + "</sii:Contraparte>" & vbCrLf
        Return True

    End Function

    Private Function VerTipoOperacion(Ser)

        VerTipoOperacion = "VENTAS"
        If (Left(Ser, 4) = "ALQC") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "ALQUILER LOCAL"
        ElseIf (Left(Ser, 4) = "ALQP") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "ALQUILER APARCAMIENTO"
        ElseIf Len(Trim(Ser)) = 1 And (Left(Ser, 1) = "A" Or Left(Ser, 1) = "B") Then
            ' "Entra en facturas gasolina"
            VerTipoOperacion = "CARBURANTES"
        ElseIf Len(Trim(Ser)) = 6 And Left(Ser, 1) = "U" Then
            ' "Entra en facturas suministros" + Chr(13) + Chr(10)
            VerTipoOperacion = "SUMINISTROS"
        ElseIf Len(Trim(Ser)) = 5 And Left(Ser, 1) = "S" Then
            ' CmdRellenaFacturasservicios empresa, Serie, Numero, ruta, rs!Fecha_factura, fichero
            VerTipoOperacion = "SERVICIOS AGRARIOS"
        ElseIf Len(Trim(Ser)) = 6 And Left(Ser, 2) = "CC" Then
            ' CmdRellenaFacturascultivocomun empresa, Serie, Numero, ruta, rs!Fecha_factura, fichero
            VerTipoOperacion = "SERVICIOS AGRARIOS"
        ElseIf Len(Trim(Ser)) = 5 And Left(Ser, 1) = "A" Then
            ' CmdRellenaFacturascultivocomun empresa, Serie, Numero, ruta, rs!Fecha_factura, fichero
            VerTipoOperacion = "VENTA DE FRUTA"
        ElseIf (Left(Ser, 1) = "T") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "COMUNICACIONES"
        ElseIf (Left(Ser, 2) = "FS") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "CARBURANTES"
        ElseIf (Left(Ser, 1) = "F") Or (Left(Ser, 1) = "R") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "VENTA DE FRUTA"
        ElseIf (Left(Ser, 1) = "M") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "VENTA DE MATERIALES"
        ElseIf (Left(Ser, 1) = "D") Then
            ' CmdRellenaFacturasTelefonia empresa, Serie, Numero, ruta, fichero
            VerTipoOperacion = "PRODUCTOS DIVERSOS"
        ElseIf Len(Trim(Ser)) = 5 And Left(Ser, 1) = "C" Then
            ' CmdRellenaFacturasaportaciones empresa, Serie, Numero, ruta, fichero
        ElseIf Len(Ser) <= 3 And (Left(Ser, 1) = "L" Or Left(Ser, 1) = "R") Then
            VerTipoOperacion = "LIQUIDACIONES"
        End If
        Return VerTipoOperacion

    End Function

    Private Function ModificacionFactura()
        Dim tipo_factura As String
        Dim Trascendencia As String
        Dim Tipo_operacion As String
        Dim Tipooperacion As String
        Dim razon As String
        Dim cif As String
        Dim TipoNoExenta As String
        Dim TipoRectificativa As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTot As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Serie_numero As String
        Dim fecha_factura As String
        Dim TieneRetencion As Boolean
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NoEnvio As Long
        Dim EsComunitaria As Boolean
        Dim RsPais As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPais As String
        Dim RsModif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlModif As String
        Dim cex As String
        Dim Isp As Boolean
        Dim Emitida_terceros As String
        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias

        Tipo_operacion = "Venta"
        TipoNoExenta = "S1"
        tipo_factura = "F1"
        Trascendencia = "01"
        tipo_comunicacion_ = "A1"
        Isp = False


        SqlModif = "SELECT * FROM factura_vta_aeat WHERE tipo_movimiento = 'M' AND csv = '' and situacion = ''"

        RsModif.Open(SqlModif, ObjetoGlobal.cn)

        While Not RsModif.EOF

            SqlModif = "empresa = '" & RsModif!empresa & "' AND serie_factura_vta = '" & RsModif!serie_factura_vta & "' AND numero_factura=" & RsModif!numero_factura
            Rs.Open("SELECT * FROM Factura_vta_c WHERE " & SqlModif, ObjetoGlobal.cn)

            tipo_factura = "F1"
            If Strings.Left(Rs!serie_factura_vta, 2) = "U2" And Rs!codigo_cliente = 1 Then
                tipo_factura = "F2"
            ElseIf strings.Left(Rs!serie_factura_vta, 1) = "R" Then
                tipo_factura = "R1"
            End If
            EsComunitaria = False

            Emitida_terceros = "N"
            If Trim(Rs!serie_factura_vta) = "A" & CStr(Year(Rs!fecha_factura)) Then
                Emitida_terceros = "S"
            End If

            'If Left(Rs!serie_factura_vta, 1) = "F" And Rs!empresa = "1" Then
            'Escomunitaria = EsfacturaComunitaria(Rs)
            EsComunitaria = IIf(Trim(Rs!tabla_iva_clientes) = "I", True, False)
            'End If

            '        SqlPais = "SELECT p.*, pp.apreviatura, pp.comunitario_sn FROM provincias_coop p LEFT JOIN paises_coop pp ON pp.codigo_pais = p.codigo_pais where p.provincia = '" & Rs!provincia_prov & "'"
            '        RsPais.Open SqlPais, ObjetoGlobal.cn)
            '        If Not RsPais.EOF Then
            '           If Trim(RsPais!comunitaria_sn) = "S" Then
            '              Escomunitaria = True
            '           End If
            '        End If
            RegistroFacturaEmitida()
            Serie_numero = Trim(Rs!serie_factura_vta) & CStr(Rs!numero_factura)
            fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
            RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and conc_cargo_dto = 'IRPF' order by tipo_orden", ObjetoGlobal.cn)
            TieneRetencion = False
            Trascendencia = "01"
            If RsTot.RecordCount > 0 Then
                TieneRetencion = True
                Trascendencia = "11"
            End If
            RsTot.Close()
            Tipo_operacion = VerTipoOperacion(Rs!serie_factura_vta)

            CabeceraFacturaEmitida(Serie_numero, fecha_factura)

            RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and tipo_linea = 'T'", ObjetoGlobal.cn)


            If tipo_factura = "R1" Then
                If Not EsComunitaria Then
                    ContraparteRectificativa(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, True, "02", Emitida_terceros, RsTot!importe_cargo) ', Serie_numero, fecha_factura
                Else
                    ContraparteRectificativa(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, False, "02", Emitida_terceros, RsTot!importe_cargo) ', Serie_numero, fecha_factura
                End If
            Else
                If Not EsComunitaria Then
                    Contraparte(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, True, "02", Emitida_terceros, RsTot!importe_cargo)
                Else
                    Contraparte(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, False, "02", Emitida_terceros, RsTot!importe_cargo)
                End If
            End If

            RsTot.Close()
            RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and tipo_linea = 'I' and porcentaje <> -1 order by tipo_orden", ObjetoGlobal.cn)

            cex = ""
            Isp = False
            ' Vamos si la serie tiene excención para ver el artículo
            If Trim(Rs!serie_factura_vta) = "D20" Then
                cex = "E1"
            ElseIf Trim(Rs!serie_factura_vta) = "D84" Then
                Isp = True
            End If

            DesgloseIVAFactura(RsTot, EsComunitaria, cex, Isp)
            ' Grabamos la tabla de enviadas
            RsModif!fecha_envio = Date.Now
            RsModif!Situacion = "P"
            RsModif!csv = ""
            RsModif!tipo_factura = tipo_factura
            RsModif!Serie_numero = Serie_numero
            RsModif!codigo_cliente = Rs!codigo_cliente
            RsModif!cif = Rs!cif
            RsModif.Update()
            RsTot.Close()
            RsModif.MoveNext()
        End While
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
        Dim TieneRetencion As Boolean
        Dim RsEnviadas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NoEnvio As Long
        Dim EsComunitaria As Boolean
        Dim RsPais As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPais As String
        Dim RsModif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlModif As String
        Dim Tipooperacion As String
        Dim cex As String
        Dim ImpresaCabecera As Boolean
        Dim Isp As Boolean
        Dim Emitida_terceros As String
        Dim trans As SqlClient.SqlTransaction
        Dim SerFacClient As String

        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias
        ImpresaCabecera = False
        Tipo_operacion = "Venta"
        TipoNoExenta = "S1"
        tipo_factura = "F1"
        Trascendencia = "01"
        tipo_comunicacion_ = "A0"
        Isp = False


        SqlModif = "SELECT * FROM factura_vta_aeat WHERE numero_envio = " & envio & " and (situacion = 'E' or situacion = 'A')"

        Try
            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            RsModif.Open(SqlModif, ObjetoGlobal.cn, True,,,,,, trans)

            While Not RsModif.EOF

                SerFacClient = "SERIE/NUMERO: " & Trim(RsModif!Serie_Factura_Vta) & " - " & RsModif!numero_factura & " CLIENTE: " & RsModif!codigo_cliente & " Error: "

                If Not ImpresaCabecera Then
                    InicioTransmision()
                    ImpresaCabecera = True
                End If

                'pb.Value = pb.Value + 1
                'DoEvents

                SqlModif = "empresa = '" & RsModif!empresa & "' AND serie_factura_vta = '" & RsModif!serie_factura_vta & "' AND numero_factura=" & RsModif!numero_factura
                Rs.Open("SELECT * FROM Factura_vta_c WHERE " & SqlModif, ObjetoGlobal.cn,,,,,,, trans)


                tipo_factura = "F1"
                If Trim(Rs!cif) = "1" Or Rs!codigo_cliente = 1 Then
                    tipo_factura = "F2"
                ElseIf Left(Rs!serie_factura_vta, 1) = "R" Then
                    tipo_factura = "R1"
                End If
                EsComunitaria = False
                EsComunitaria = IIf(Trim(Rs!tabla_iva_clientes) = "I", True, False)

                Emitida_terceros = "N"
                If Trim(Rs!serie_factura_vta) = "A" & CStr(Year(Rs!fecha_factura)) Then
                    Emitida_terceros = "S"
                End If

                RegistroFacturaEmitida()
                Serie_numero = Trim(Rs!serie_factura_vta) & CStr(Rs!numero_factura)
                fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
                RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and conc_cargo_dto = 'IRPF' order by tipo_linea", ObjetoGlobal.cn,,,,,,, trans)
                TieneRetencion = False
                Trascendencia = "01"
                If RsTot.RecordCount > 0 Then
                    TieneRetencion = True
                    Trascendencia = "11"
                End If
                RsTot.Close()
                Tipo_operacion = VerTipoOperacion(Rs!serie_factura_vta)

                CabeceraFacturaEmitida(Serie_numero, fecha_factura)
                RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and tipo_linea = 'T'", ObjetoGlobal.cn,,,,,,, trans)

                If tipo_factura = "R1" Then
                    If Not EsComunitaria Then
                        ContraparteRectificativa(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, True, "02", Emitida_terceros, RsTot!importe_cargo)
                    Else
                        ContraparteRectificativa(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, False, "02", Emitida_terceros, RsTot!importe_cargo)
                    End If
                Else
                    If Not EsComunitaria Then
                        Contraparte(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, True, "02", Emitida_terceros, RsTot!importe_cargo)
                    Else
                        Contraparte(tipo_factura, Trascendencia, IIf(Trim(Rs!razon_social) = "", Trim(Rs!nombre_cliente), Trim(Rs!razon_social)), Trim(Rs!cif), Tipo_operacion, False, "02", Emitida_terceros, RsTot!importe_cargo)
                    End If
                End If
                RsTot.Close()
                RsTot.Open("SELECT * FROM Factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura=" & Rs!numero_factura & " and tipo_linea = 'I' and porcentaje <> -1 order by tipo_linea", ObjetoGlobal.cn,,,,,,, trans)

                cex = ""
                Isp = False
                ' Vamos si la serie tiene excención para ver el artículo
                If Trim(Rs!serie_factura_vta) = "D20" Then
                    cex = "E1"
                ElseIf Trim(Rs!serie_factura_vta) = "D84" Then
                    Isp = True
                End If

                DesgloseIVAFactura(RsTot, EsComunitaria, cex, Isp)
                ' Grabamos la tabla de enviadas
                RsModif!fecha_envio = Date.Now
                RsModif!Situacion = "P"
                RsModif!csv = ""
                RsModif!tipo_factura = ""
                RsModif!Serie_numero = Serie_numero
                RsModif!codigo_cliente = Rs!codigo_cliente
                RsModif!cif = Rs!cif
                RsModif.Update()
                RsTot.Close()
                Rs.Close()
                RsModif.MoveNext()
            End While
            RsModif.Close()

            If ImpresaCabecera Then
                PieEnvio()
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()

            MsgBox("Envio concluido")
            Return True

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


    Public Function ExisteRemesa()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SqlPrueba As String
        ExisteRemesa = False
        SqlPrueba = Sql & " AND situacion = 'P'"


        Rs.Open("SELECT * FROM factura_vta_aeat WHERE " & SqlPrueba, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("Hay registros pendientes de procesar con la fecha indicada")
            ExisteRemesa = True
            Rs.Close()
            Exit Function
        End If
        Rs.Close()

        SqlPrueba = Sql & " AND situacion = 'C'"
        Rs.Open("SELECT * FROM factura_vta_aeat WHERE " & SqlPrueba, ObjetoGlobal.cn)
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
