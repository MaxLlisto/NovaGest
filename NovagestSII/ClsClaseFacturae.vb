Public Class ClsClaseFacturae
    Public ObjetoGlobal As Object
    Dim Cabecera As String
    Dim cabecera1 As String
    Dim cXML As String
    Dim no_facturas As Integer ' Total facturas en el fichero
    Dim total_importes As Double ' Total importes, IVA incluido en el fichero

    Dim TipoCliente As String
    Dim NifCliente As String
    Dim RazonCliente As String
    Dim Dir_cliente As String
    Dim cp_cliente As String
    Dim Loc_cliente As String
    Dim Prov_cliente As String
    Dim referencia_remesa As String

    Public ReadOnly Property XML() As String
        Get
            Return cXML
        End Get
    End Property


    Public WriteOnly Property numerofacturas() As Integer
        Set(ByVal value As Integer)
            no_facturas = value
        End Set
    End Property

    Public WriteOnly Property importeremesa() As Double
        Set(ByVal value As Double)
            total_importes = value
        End Set
    End Property

    Public WriteOnly Property IdentificadorLote() As String
        Set(ByVal value As String)
            IdentificadorLote = value
        End Set
    End Property


    Public Sub CabeceraEnvio(modalidad)
        cXML = ""
        'cXML = cXML + "<?xml version=" + Chr(34) + "1.0" + Chr(34) + " encoding=" + Chr(34) + "UTF-8" + Chr(34) + " standalone=" + Chr(34) + "yes" + Chr(34) + "?>" + Chr(13) + Chr(10)
        'cXML = cXML + "<namespace:Facturae xmlns:namespace2=" + Chr(34) + "http://uri.etsi.org/01903/v1.2.2#" + Chr(34) + " xmlns:namespace3=" + Chr(34) + "http://www.w3.org/2000/09/xmldsig#" + Chr(34) + "" + Chr(13) + Chr(10)
        cXML = cXML + "<?xml version=" + Chr(34) + "1.0" + Chr(34) + " encoding=" + Chr(34) + "UTF-8" + Chr(34) + "?>" + Chr(13) + Chr(10)
        cXML = cXML + "<fe:Facturae xmlns:ds=" + Chr(34) + "http://www.w3.org/2000/09/xmldsig#" + Chr(34) + " xmlns:fe=" + Chr(34) + "http://www.facturae.es/Facturae/2009/v3.2/Facturae" + Chr(34) + ">" + Chr(13) + Chr(10)
        'cXML = cXML + "xmlns:namespace=" + Chr(34) + "http://www.facturae.es/Facturae/2007/v3.0/Facturae" + Chr(34) + ">" + Chr(13) + Chr(10)

        cXML = cXML + "<FileHeader>" + Chr(13) + Chr(10)
        cXML = cXML + "<SchemaVersion>3.2</SchemaVersion>" + Chr(13) + Chr(10)
        cXML = cXML + "<Modality>" & modalidad & "</Modality>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceIssuerType>EM</InvoiceIssuerType>" + Chr(13) + Chr(10)
        cXML = cXML + "<Batch>" + Chr(13) + Chr(10)
        cXML = cXML + "<BatchIdentifier>" & Trim(referencia_remesa) & "</BatchIdentifier>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoicesCount>" & no_facturas & "</InvoicesCount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalInvoicesAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Format(total_importes, "#######0.00"), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TotalInvoicesAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalOutstandingAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Format(total_importes, "#######0.00"), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TotalOutstandingAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalExecutableAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Format(total_importes, "#######0.00"), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TotalExecutableAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceCurrencyCode>EUR</InvoiceCurrencyCode>" + Chr(13) + Chr(10)
        cXML = cXML + "</Batch>" + Chr(13) + Chr(10)
        cXML = cXML + "</FileHeader>" + Chr(13) + Chr(10)

    End Sub

    Public Sub Emisor(Emp)
        If Trim(Emp) = "22" Then
            cXML = cXML + "<Parties>" + Chr(13) + Chr(10)
            cXML = cXML + "<SellerParty>" + Chr(13) + Chr(10)
            cXML = cXML + "<TaxIdentification>" + Chr(13) + Chr(10)
            cXML = cXML + "<PersonTypeCode>J</PersonTypeCode>" + Chr(13) + Chr(10)
            cXML = cXML + "<ResidenceTypeCode>R</ResidenceTypeCode>" + Chr(13) + Chr(10)
            cXML = cXML + "<TaxIdentificationNumber>F96570585</TaxIdentificationNumber>" + Chr(13) + Chr(10)
            cXML = cXML + "</TaxIdentification>" + Chr(13) + Chr(10)
            cXML = cXML + "<LegalEntity>" + Chr(13) + Chr(10)
            cXML = cXML + "<CorporateName>Conacoop, COOP. V.</CorporateName>" + Chr(13) + Chr(10)
            cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
            cXML = cXML + "<Address>C/Miguel Abriat Canto, 11</Address>" + Chr(13) + Chr(10)
            cXML = cXML + "<PostCode>46160</PostCode>" + Chr(13) + Chr(10)
            cXML = cXML + "<Town>Lliria</Town>" + Chr(13) + Chr(10)
            cXML = cXML + "<Province>VALENCIA</Province>" + Chr(13) + Chr(10)
            cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
            cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
            cXML = cXML + "</LegalEntity>" + Chr(13) + Chr(10)
            cXML = cXML + "</SellerParty>" + Chr(13) + Chr(10)
        Else
            cXML = cXML + "<Parties>" + Chr(13) + Chr(10)
            cXML = cXML + "<SellerParty>" + Chr(13) + Chr(10)
            cXML = cXML + "<TaxIdentification>" + Chr(13) + Chr(10)
            cXML = cXML + "<PersonTypeCode>J</PersonTypeCode>" + Chr(13) + Chr(10)
            cXML = cXML + "<ResidenceTypeCode>R</ResidenceTypeCode>" + Chr(13) + Chr(10)
            cXML = cXML + "<TaxIdentificationNumber>F46026068</TaxIdentificationNumber>" + Chr(13) + Chr(10)
            cXML = cXML + "</TaxIdentification>" + Chr(13) + Chr(10)
            cXML = cXML + "<LegalEntity>" + Chr(13) + Chr(10)
            cXML = cXML + "<CorporateName>Cooperativa Vinicola de Lliria, S.C.V.</CorporateName>" + Chr(13) + Chr(10)
            cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
            cXML = cXML + "<Address>C/Miguel Abriat Canto, 11</Address>" + Chr(13) + Chr(10)
            cXML = cXML + "<PostCode>46160</PostCode>" + Chr(13) + Chr(10)
            cXML = cXML + "<Town>Lliria</Town>" + Chr(13) + Chr(10)
            cXML = cXML + "<Province>VALENCIA</Province>" + Chr(13) + Chr(10)
            cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
            cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
            cXML = cXML + "</LegalEntity>" + Chr(13) + Chr(10)
            cXML = cXML + "</SellerParty>" + Chr(13) + Chr(10)
        End If

    End Sub


    Public Sub Receptor(Rs)

        cXML = cXML + "<BuyerParty>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxIdentification>" + Chr(13) + Chr(10)
        cXML = cXML + "<PersonTypeCode>J</PersonTypeCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<ResidenceTypeCode>R</ResidenceTypeCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxIdentificationNumber>" & Trim(Rs!cif) & "</TaxIdentificationNumber>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxIdentification>" + Chr(13) + Chr(10)
        cXML = cXML + "<LegalEntity>" + Chr(13) + Chr(10)
        cXML = cXML + "<CorporateName>" & Trim(Rs!razon_social) & "</CorporateName>" + Chr(13) + Chr(10)
        cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<Address>" & Trim(Rs!domicilio) & "</Address>" + Chr(13) + Chr(10)
        cXML = cXML + "<PostCode>" & Trim(Rs!codigo_postal) & "</PostCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<Town>" & Trim(Rs!poblacion_cliente) & "</Town>" + Chr(13) + Chr(10)
        cXML = cXML + "<Province>VALENCIA</Province>" + Chr(13) + Chr(10)
        cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "</LegalEntity>" + Chr(13) + Chr(10)
        cXML = cXML + "</BuyerParty>" + Chr(13) + Chr(10)
        cXML = cXML + "</Parties>" + Chr(13) + Chr(10)

    End Sub

    Public Sub InicioLote()
        cXML = cXML + "<Invoices>" + Chr(13) + Chr(10)
    End Sub

    Private Sub CabeceraFactura(Ser_num)

        cXML = cXML + "<InvoiceHeader>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceNumber>" & Ser_num & "</InvoiceNumber>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceDocumentType>FC</InvoiceDocumentType>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceClass>OO</InvoiceClass>" + Chr(13) + Chr(10)
        cXML = cXML + "</InvoiceHeader>" + Chr(13) + Chr(10)

    End Sub

    Private Sub DatosFactura(fecha_factura)

        cXML = cXML + "<InvoiceIssueData>" + Chr(13) + Chr(10)
        cXML = cXML + "<IssueDate>" & Format(fecha_factura, "yyyy-mm-dd") & "</IssueDate>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceCurrencyCode>EUR</InvoiceCurrencyCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxCurrencyCode>EUR</TaxCurrencyCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<LanguageName>es</LanguageName>" + Chr(13) + Chr(10)
        cXML = cXML + "</InvoiceIssueData>" + Chr(13) + Chr(10)

    End Sub

    Private Sub Impuestos_Repercutidos(tipoImp, Base, Porc, Cuota)
        cXML = cXML + "<Tax>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxTypeCode>" & tipoImp & "</TaxTypeCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxRate>" & Replace(Format(Porc, "#0.00"), ",", ".") & "</TaxRate>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxableBase>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Trim(Format(Base, "########0.00")), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxableBase>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Trim(Format(Cuota, "########0.00")), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</Tax>" + Chr(13) + Chr(10)
    End Sub

    Private Sub TablaImpuestos(Rs)
        Dim RsTot As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim Bases As Double
        Dim Cuotas As Double

        Sql = "SELECT * FROM factura_vta_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_vta = '" & Rs!serie_factura_vta & "' AND numero_factura =" & Rs!numero_factura & " AND tipo_linea = 'I' ORDER BY numero_orden"
        RsTot.Open(Sql, ObjetoGlobal.cn)
        cXML = cXML + "<TaxesOutputs>" + Chr(13) + Chr(10)
        Cuotas = 0
        Bases = 0
        While Not RsTot.EOF
            Cuotas = Cuotas + RsTot!importe_cargo
            Bases = Bases + RsTot!Base
            Impuestos_Repercutidos("01", RsTot!Base, RsTot!porcentaje, RsTot!importe_cargo)
            RsTot.MoveNext
        End While
        cXML = cXML + "</TaxesOutputs>" + Chr(13) + Chr(10)

        RsTot.Close
        Sql = "SELECT * FROM factura_vta_c_tot WHERE empresa = '" & Trim(Rs!empresa) & "' AND serie_factura_vta = '" & Trim(Rs!serie_factura_vta) & "' AND numero_factura =" & Rs!numero_factura & " AND tipo_linea = 'T' ORDER BY numero_orden"
        RsTot.Open(Sql, ObjetoGlobal.cn)

        TotalesFactura(Bases, Cuotas, RsTot!importe_cargo)
        RsTot.Close

    End Sub
    Private Sub TotalesFactura(Base, Cuota, totales)

        cXML = cXML + "<InvoiceTotals>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalGrossAmount>" & Replace(Trim(Format(Base, "########0.00")), ",", ".") & "</TotalGrossAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalGrossAmountBeforeTaxes>" & Replace(Trim(Format(Base, "########0.00")), ",", ".") & "</TotalGrossAmountBeforeTaxes>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalTaxOutputs>" & Replace(Trim(Format(Cuota, "########0.00")), ",", ".") & "</TotalTaxOutputs>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalTaxesWithheld>0.00</TotalTaxesWithheld>" + Chr(13) + Chr(10)
        cXML = cXML + "<InvoiceTotal>" & Replace(Trim(Format(totales, "########0.00")), ",", ".") & "</InvoiceTotal>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalOutstandingAmount>" & Replace(Trim(Format(totales, "########0.00")), ",", ".") & "</TotalOutstandingAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalExecutableAmount>" & Replace(Trim(Format(totales, "########0.00")), ",", ".") & "</TotalExecutableAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</InvoiceTotals>" + Chr(13) + Chr(10)

    End Sub

    Private Sub desglosefacturas(DetalleLinea, cantidad, unidad, punitario, total, tipoImp, Porc, Base, Cuota)

        cXML = cXML + "<InvoiceLine>" + Chr(13) + Chr(10)
        cXML = cXML + "<ItemDescription>" & DetalleLinea & "</ItemDescription>" + Chr(13) + Chr(10)
        cXML = cXML + "<Quantity>" & Replace(Format(cantidad, "######0.00"), ",", ".") & "</Quantity>" + Chr(13) + Chr(10)
        cXML = cXML + "<UnitOfMeasure>" & unidad & "</UnitOfMeasure>" + Chr(13) + Chr(10)
        cXML = cXML + "<UnitPriceWithoutTax>" & Replace(Format(punitario, "######0.000000"), ",", ".") & "</UnitPriceWithoutTax>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalCost>" & Replace(Format(total, "######0.000000"), ",", ".") & "</TotalCost>" + Chr(13) + Chr(10)
        cXML = cXML + "<GrossAmount>" & Replace(Format(total, "######0.000000"), ",", ".") & "</GrossAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxesOutputs>" + Chr(13) + Chr(10)
        cXML = cXML + "<Tax>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxTypeCode>" & tipoImp & "</TaxTypeCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxRate>" & Replace(Format(Porc, "#0.00"), ",", ".") & "</TaxRate>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxableBase>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Format(Base, "######0.00"), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxableBase>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "<TotalAmount>" & Replace(Format(Cuota, "######0.00"), ",", ".") & "</TotalAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxAmount>" + Chr(13) + Chr(10)
        cXML = cXML + "</Tax>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxesOutputs>" + Chr(13) + Chr(10)
        cXML = cXML + "</InvoiceLine>" + Chr(13) + Chr(10)

    End Sub

    Public Sub Desglose(Rs)
        Dim RsLineas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim textoLinea As String
        Dim cantidad As Double
        Dim unidad As String
        Dim punitario As Double
        Dim total As Double
        Dim Porc As Double
        Dim Base As Double
        Dim Cuota As Double

        cXML = cXML + "<Items>" + Chr(13) + Chr(10)


        If Trim(Rs!serie_factura_vta) = "A" Or Trim(Rs!serie_factura_vta) = "B" Then
            Sql = "SELECT f.*, a.descripcion, a.unidades, a.precio, a.tipo_iva, a.importe_linea FROM factura_vta_l f LEFT JOIN albaran_vta_l a ON f.empresa = a.empresa AND "
            Sql = Sql + " a.serie_albaran = f.serie_albaran_vta  AND a.numero_albaran = f.numero_albaran_vta "
            Sql = Sql + "WHERE f.empresa = '" & Rs!empresa & "' AND f.serie_factura_vta = '" & Rs!serie_factura_vta & "' AND "
            Sql = Sql + "f.numero_factura = " & Rs!numero_factura
            Sql = Sql + " ORDER BY 1,2,3,4 "
            RsLineas.Open(Sql, ObjetoGlobal.cn)
            While Not RsLineas.EOF
                desglosefacturas(RsLineas!Descripcion, RsLineas!unidades, "01", RsLineas!precio, RsLineas!importe_linea, "01", RsLineas!tipo_iva, RsLineas!importe_linea, Math.Round(RsLineas!importe_linea * RsLineas!tipo_iva / 100, 2))
                RsLineas.MoveNext
            End While
            RsLineas.Close

        ElseIf Trim(Rs!serie_factura_vta) = "D" And Trim(Rs!empresa) = "22" Then
            Sql = "SELECT l.texto_1,l.texto_2,l.unidades_l, l.precio_l, l.cod_tipo_iva, l.importe_l,i.porcentaje_iva FROM factura_vta_l l  "
            Sql = Sql + " LEFT JOIN porc_iva_repercut i ON i.empresa = l.empresa AND i.tipo_iva = l.cod_tipo_iva "
            Sql = Sql + " WHERE l.empresa = '" & Rs!empresa & "' AND l.serie_factura_vta = '" & Rs!serie_factura_vta & "' AND "
            Sql = Sql + " l.numero_factura = " & Rs!numero_factura & " AND i.tabla_iva_clientes = 'N'"
            Sql = Sql + " ORDER BY 1,2,3,4 "
            RsLineas.Open(Sql, ObjetoGlobal.cn)
            textoLinea = "-1"
            While Not RsLineas.EOF
                If Not IsDBNull(RsLineas!importe_l) Then
                    If Trim(textoLinea) <> "-1" Then
                        desglosefacturas(textoLinea, cantidad, unidad, punitario, total, "01", Porc, Base, Cuota)
                    End If
                    cantidad = RsLineas!unidades_l
                    unidad = "01"
                    punitario = RsLineas!precio_l
                    total = RsLineas!importe_l
                    Porc = RsLineas!porcentaje_iva
                    Base = RsLineas!importe_l
                    Cuota = Math.Round(RsLineas!importe_l * RsLineas!porcentaje_iva / 100, 2)
                    textoLinea = RsLineas!texto_2
                Else
                    textoLinea = textoLinea + RsLineas!texto_2
                End If
                RsLineas.MoveNext


            End While
            'desglosefacturas(DetalleLinea, cantidad, unidad, punitario, total, tipoImp, Porc, base, cuota)
            If Trim(textoLinea) <> "-1" Then
                desglosefacturas(textoLinea, cantidad, unidad, punitario, total, "01", Porc, Base, Cuota)
            End If
            RsLineas.Close

        ElseIf Trim(Rs!serie_factura_vta) = "D" And Trim(Rs!empresa) = "1" Then
            Sql = "SELECT l.texto_1,l.texto_2,l.unidades_l, l.precio_l, l.cod_tipo_iva, l.importe_l,i.porcentaje_iva FROM factura_vta_l l  "
            Sql = Sql + " LEFT JOIN porc_iva_repercut i ON i.empresa = l.empresa AND i.tipo_iva = l.cod_tipo_iva "
            Sql = Sql + " WHERE l.empresa = '" & Rs!empresa & "' AND l.serie_factura_vta = '" & Rs!serie_factura_vta & "' AND "
            Sql = Sql + " l.numero_factura = " & Rs!numero_factura & " AND i.tabla_iva_clientes = 'N'"
            Sql = Sql + " ORDER BY 1,2,3,4 "
            RsLineas.Open(Sql, ObjetoGlobal.cn)
            textoLinea = "-1"
            While Not RsLineas.EOF
                If Not IsDBNull(RsLineas!importe_l) Then
                    If Trim(textoLinea) <> "-1" Then
                        desglosefacturas(textoLinea, cantidad, unidad, punitario, total, "01", Porc, Base, Cuota)
                    End If
                    cantidad = RsLineas!unidades_l
                    unidad = "01"
                    punitario = RsLineas!precio_l
                    total = RsLineas!importe_l
                    Porc = RsLineas!porcentaje_iva
                    Base = RsLineas!importe_l
                    Cuota = Math.Round(RsLineas!importe_l * RsLineas!porcentaje_iva / 100, 2)
                    textoLinea = RsLineas!texto_2
                Else
                    textoLinea = textoLinea + RsLineas!texto_2
                End If
                RsLineas.MoveNext
            End While
            'desglosefacturas(DetalleLinea, cantidad, unidad, punitario, total, tipoImp, Porc, base, cuota)
            If Trim(textoLinea) <> "-1" Then
                desglosefacturas(textoLinea, cantidad, unidad, punitario, total, "01", Porc, Base, Cuota)
            End If
            RsLineas.Close

        ElseIf Trim(Rs!empresa) = "11" And (Left(Rs!serie_factura_vta, 2) = "U2" Or Left(Rs!serie_factura_vta, 2) = "UD") Then
            Sql = "SELECT l.texto_1,l.texto_2,l.unidades_l, l.precio_l, l.cod_tipo_iva, l.importe_l,i.porcentaje_iva FROM factura_vta_l l  "
            Sql = Sql + " LEFT JOIN porc_iva_repercut i ON i.empresa = l.empresa AND i.tipo_iva = l.cod_tipo_iva "
            Sql = Sql + " WHERE l.empresa = '" & Rs!empresa & "' AND l.serie_factura_vta = '" & Rs!serie_factura_vta & "' AND "
            Sql = Sql + " l.numero_factura = " & Rs!numero_factura & " AND i.tabla_iva_clientes = 'N'"
            Sql = Sql + " ORDER BY 1,2,3,4 "
            RsLineas.Open(Sql, ObjetoGlobal.cn)

            While Not RsLineas.EOF
                desglosefacturas(Trim(RsLineas!texto_1) + "-" + Trim(RsLineas!texto_2),
                         RsLineas!unidades_l,
                         "01",
                         Math.Round((RsLineas!precio_l * 100) / (100 + RsLineas!porcentaje_iva), 6),
                         Math.Round((RsLineas!importe_l * 100) / (100 + RsLineas!porcentaje_iva), 6),
                         "01",
                         RsLineas!porcentaje_iva,
                         Math.Round((RsLineas!importe_l * 100) / (100 + RsLineas!porcentaje_iva), 6),
                         RsLineas!importe_l - Math.Round((RsLineas!importe_l * 100) / (100 + RsLineas!porcentaje_iva), 6))
                RsLineas.MoveNext()
            End While

            RsLineas.Close

        ElseIf Trim(Rs!empresa) = "1" And Left(Rs!serie_factura_vta, 1) = "F" Then
            Sql = "SELECT v.descripcion,s.neto, s.precio, l.cod_tipo_iva, s.importe,i.porcentaje_iva FROM factura_vta_l l  "
            Sql = Sql + " LEFT JOIN porc_iva_repercut i ON i.empresa = l.empresa AND i.tipo_iva = l.cod_tipo_iva "
            Sql = Sql + " LEFT JOIN lineas_salida s ON s.empresa = l.empresa AND s.ejercicio = '" & ObjetoGlobal.ejercicioactual & "' AND s.numero_documento =  l.numero_doc_salida"
            Sql = Sql + " LEFT JOIN variedades v ON v.empresa = l.empresa AND v.codigo_variedad = s.codigo_variedad "
            Sql = Sql + " WHERE l.empresa = '" & Rs!empresa & "' AND l.serie_factura_vta = '" & Rs!serie_factura_vta & "' AND "
            Sql = Sql + " l.numero_factura = " & Rs!numero_factura & " AND i.tabla_iva_clientes = 'N'"
            Sql = Sql + " ORDER BY 1,2,3,4 "
            RsLineas.Open(Sql, ObjetoGlobal.cn)

            While Not RsLineas.EOF
                desglosefacturas(Trim(RsLineas!Descripcion),
                         RsLineas!neto,
                         "03",
                         RsLineas!precio,
                         RsLineas!importe,
                         "01",
                         RsLineas!porcentaje_iva,
                         RsLineas!importe,
                         Math.Round(RsLineas!importe * RsLineas!porcentaje_iva / 100, 2))

                RsLineas.MoveNext
            End While

            RsLineas.Close

        ElseIf (Trim(Rs!empresa) = "5" And Left(Rs!serie_factura_vta, 2) = "CC") Or (Trim(Rs!empresa) = "11" And Left(Rs!serie_factura_vta, 1) = "S") Then
            Sql = "SELECT l.texto_1,l.texto_2,l.unidades_l, l.precio_l, l.cod_tipo_iva, l.importe_l,i.porcentaje_iva FROM factura_vta_l l  "
            Sql = Sql + " LEFT JOIN porc_iva_repercut i ON i.empresa = l.empresa AND i.tipo_iva = l.cod_tipo_iva "
            Sql = Sql + " WHERE l.empresa = '" & Rs!empresa & "' AND l.serie_factura_vta = '" & Rs!serie_factura_vta & "' AND "
            Sql = Sql + " l.numero_factura = " & Rs!numero_factura & " AND i.tabla_iva_clientes = 'N'"
            Sql = Sql + " ORDER BY 1,2,3,4 "
            RsLineas.Open(Sql, ObjetoGlobal.cn)
            textoLinea = "-1"
            While Not RsLineas.EOF
                If Not IsDBNull(RsLineas!importe_l) Then
                    If Trim(textoLinea) <> "-1" Then
                        desglosefacturas(textoLinea, cantidad, unidad, punitario, total, "01", Porc, Base, Cuota)
                    End If
                    cantidad = RsLineas!unidades_l
                    unidad = "01"
                    punitario = RsLineas!precio_l
                    total = RsLineas!importe_l
                    Porc = RsLineas!porcentaje_iva
                    Base = RsLineas!importe_l
                    Cuota = Math.Round(RsLineas!importe_l * RsLineas!porcentaje_iva / 100, 2)
                    textoLinea = RsLineas!texto_2
                Else
                    textoLinea = textoLinea + RsLineas!texto_2
                End If
                RsLineas.MoveNext
            End While
            'desglosefacturas(DetalleLinea, cantidad, unidad, punitario, total, tipoImp, Porc, base, cuota)
            If Trim(textoLinea) <> "-1" Then
                desglosefacturas(textoLinea, cantidad, unidad, punitario, total, "01", Porc, Base, Cuota)
            End If
        End If
        cXML = cXML + "</Items>" + Chr(13) + Chr(10)


    End Sub

    Private Sub InicioFactura()
        cXML = cXML + "<Invoice>" + Chr(13) + Chr(10)
    End Sub

    Private Sub FinFactura()
        cXML = cXML + "</Invoice>" + Chr(13) + Chr(10)
    End Sub

    Public Sub FinLote()
        cXML = cXML + "</Invoices>" + Chr(13) + Chr(10)
        cXML = cXML + "</fe:Facturae>" + Chr(13) + Chr(10)
    End Sub
    Public Sub DetalleFacturas(Rs)

        InicioFactura()
        CabeceraFactura(Trim(Rs!serie_factura_vta) & Rs!numero_factura)
        DatosFactura(Rs!fecha_factura)
        TablaImpuestos(Rs)
        Desglose(Rs)
        FinFactura()

    End Sub


    '- <AdministrativeCentre>
    '    <CentreCode>E00000034</CentreCode>
    '    <RoleTypeCode>02</RoleTypeCode>
    '    - <AddressInSpain>
    '          <Address>Paseo de la Castellana</Address>
    '          <PostCode>28071</PostCode>
    '          <Town>Madrid</Town>
    '          <Province>Madrid</Province>
    '          <CountryCode>ESP</CountryCode>
    '    </AddressInSpain>
    '    <CentreDescription>Órgano Gestor</CentreDescription>
    '</AdministrativeCentre>

    Public Sub ReceptorAdministracion(Rs)
        Dim rsc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim codc As String
        Dim tipo As String
        Dim direccion As String
        Dim cp As String
        Dim poblacion As String
        Dim provincia As String
        Dim codc1 As String
        Dim tipo1 As String
        Dim direccion1 As String
        Dim cp1 As String
        Dim poblacion1 As String
        Dim provincia1 As String
        Dim codc2 As String
        Dim tipo2 As String
        Dim direccion2 As String
        Dim cp2 As String
        Dim poblacion2 As String
        Dim provincia2 As String
        Dim Sql As String

        Sql = "SELECT * FROM client_efactura WHERE nif = '" & Trim(Rs!cif) & "'"

        rsc.Open(Sql, ObjetoGlobal.cn)
        If Not rsc.EOF Then
            codc = Trim(rsc!codigo_centro)
            tipo = "01"
            direccion = Trim(rsc!direccion)
            cp = Trim(rsc!cp)
            poblacion = Trim(rsc!poblacion)
            provincia = Trim(rsc!provincia)

            If Trim(rsc!repite_sn) = "S" Then
                codc1 = Trim(rsc!codigo_centro)
                tipo1 = "02"
                direccion1 = Trim(rsc!direccion)
                cp1 = Trim(rsc!cp)
                poblacion1 = Trim(rsc!poblacion)
                provincia1 = Trim(rsc!provincia)

                codc2 = Trim(rsc!codigo_centro)
                tipo2 = "03"
                direccion2 = Trim(rsc!direccion)
                cp2 = Trim(rsc!cp)
                poblacion2 = Trim(rsc!poblacion)
                provincia2 = Trim(rsc!provincia)
            Else
                codc1 = Trim(rsc!codigo_centro1)
                tipo1 = "02"
                direccion1 = Trim(rsc!direccion1)
                cp1 = Trim(rsc!cp1)
                poblacion1 = Trim(rsc!poblacion1)
                provincia1 = Trim(rsc!provincia1)

                codc2 = Trim(rsc!codigo_centro2)
                tipo2 = "03"
                direccion2 = Trim(rsc!direccion2)
                cp2 = Trim(rsc!cp2)
                poblacion2 = Trim(rsc!poblacion2)
                provincia2 = Trim(rsc!provincia2)
            End If
        End If


        cXML = cXML + "<BuyerParty>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxIdentification>" + Chr(13) + Chr(10)
        cXML = cXML + "<PersonTypeCode>J</PersonTypeCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<ResidenceTypeCode>R</ResidenceTypeCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<TaxIdentificationNumber>" & Trim(Rs!cif) & "</TaxIdentificationNumber>" + Chr(13) + Chr(10)
        cXML = cXML + "</TaxIdentification>" + Chr(13) + Chr(10)


        cXML = cXML + "<AdministrativeCentres>" + Chr(13) + Chr(10)
        cXML = cXML + "<AdministrativeCentre>" + Chr(13) + Chr(10)
        cXML = cXML + "<CentreCode>" & codc & "</CentreCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<RoleTypeCode>" & tipo & "</RoleTypeCode>"
        cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<Address>" & direccion & "</Address>" + Chr(13) + Chr(10)
        cXML = cXML + "<PostCode>" & cp & "</PostCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<Town>" & poblacion & "</Town>" + Chr(13) + Chr(10)
        cXML = cXML + "<Province>" & provincia & "</Province>" + Chr(13) + Chr(10)
        cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<CentreDescription>Oficina contable</CentreDescription>" + Chr(13) + Chr(10)
        cXML = cXML + "</AdministrativeCentre>" + Chr(13) + Chr(10)

        cXML = cXML + "<AdministrativeCentre>" + Chr(13) + Chr(10)
        cXML = cXML + "<CentreCode>" & codc1 & "</CentreCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<RoleTypeCode>" & tipo1 & "</RoleTypeCode>"
        cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<Address>" & direccion1 & "</Address>" + Chr(13) + Chr(10)
        cXML = cXML + "<PostCode>" & cp1 & "</PostCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<Town>" & poblacion1 & "</Town>" + Chr(13) + Chr(10)
        cXML = cXML + "<Province>" & provincia1 & "</Province>" + Chr(13) + Chr(10)
        cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<CentreDescription>Órgano Gestor</CentreDescription>" + Chr(13) + Chr(10)
        cXML = cXML + "</AdministrativeCentre>" + Chr(13) + Chr(10)

        cXML = cXML + "<AdministrativeCentre>" + Chr(13) + Chr(10)
        cXML = cXML + "<CentreCode>" & codc2 & "</CentreCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<RoleTypeCode>" & tipo2 & "</RoleTypeCode>"
        cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<Address>" & direccion2 & "</Address>" + Chr(13) + Chr(10)
        cXML = cXML + "<PostCode>" & cp2 & "</PostCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<Town>" & poblacion2 & "</Town>" + Chr(13) + Chr(10)
        cXML = cXML + "<Province>" & provincia2 & "</Province>" + Chr(13) + Chr(10)
        cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<CentreDescription>Unidad tramitadora</CentreDescription>" + Chr(13) + Chr(10)
        cXML = cXML + "</AdministrativeCentre>" + Chr(13) + Chr(10)

        cXML = cXML + "</AdministrativeCentres>" + Chr(13) + Chr(10)

        cXML = cXML + "<LegalEntity>" + Chr(13) + Chr(10)
        cXML = cXML + "<CorporateName>" & Trim(Rs!razon_social) & "</CorporateName>" + Chr(13) + Chr(10)
        cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "<Address>" & Trim(Rs!domicilio) & "</Address>" + Chr(13) + Chr(10)
        cXML = cXML + "<PostCode>" & Trim(Rs!codigo_postal) & "</PostCode>" + Chr(13) + Chr(10)
        cXML = cXML + "<Town>" & Trim(Rs!poblacion_cliente) & "</Town>" + Chr(13) + Chr(10)
        cXML = cXML + "<Province>VALENCIA</Province>" + Chr(13) + Chr(10)
        cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        cXML = cXML + "</LegalEntity>" + Chr(13) + Chr(10)

        cXML = cXML + "</BuyerParty>" + Chr(13) + Chr(10)
        cXML = cXML + "</Parties>" + Chr(13) + Chr(10)


        'cXML = cXML + "<AdministrativeCentres>" + Chr(13) + Chr(10)
        'cXML = cXML + "<AdministrativeCentre>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CentreCode>L01461477</CentreCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<RoleTypeCode>01</RoleTypeCode>"
        'cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Address>Plaza Mayor,1</Address>" + Chr(13) + Chr(10)
        'cXML = cXML + "<PostCode>46160</PostCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Town>Lliria</Town>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Province>Valencia</Province>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CentreDescription>Oficina contable</CentreDescription>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AdministrativeCentre>" + Chr(13) + Chr(10)
        '
        'cXML = cXML + "<AdministrativeCentre>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CentreCode>L01461477</CentreCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<RoleTypeCode>02</RoleTypeCode>"
        'cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Address>Plaza Mayor,1</Address>" + Chr(13) + Chr(10)
        'cXML = cXML + "<PostCode>46160</PostCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Town>Lliria</Town>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Province>Valencia</Province>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CentreDescription>Órgano Gestor</CentreDescription>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AdministrativeCentre>" + Chr(13) + Chr(10)
        '
        'cXML = cXML + "<AdministrativeCentre>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CentreCode>L01461477</CentreCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<RoleTypeCode>03</RoleTypeCode>"
        'cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Address>Plaza Mayor,1</Address>" + Chr(13) + Chr(10)
        'cXML = cXML + "<PostCode>46160</PostCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Town>Lliria</Town>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Province>Valencia</Province>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CentreDescription>Unidad tramitadora</CentreDescription>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AdministrativeCentre>" + Chr(13) + Chr(10)
        '
        'cXML = cXML + "</AdministrativeCentres>" + Chr(13) + Chr(10)
        '
        'cXML = cXML + "<LegalEntity>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CorporateName>" & Trim(Rs!razon_social) & "</CorporateName>" + Chr(13) + Chr(10)
        'cXML = cXML + "<AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Address>" & Trim(Rs!domicilio) & "</Address>" + Chr(13) + Chr(10)
        'cXML = cXML + "<PostCode>" & Trim(Rs!codigo_postal) & "</PostCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Town>" & Trim(Rs!poblacion_cliente) & "</Town>" + Chr(13) + Chr(10)
        'cXML = cXML + "<Province>VALENCIA</Province>" + Chr(13) + Chr(10)
        'cXML = cXML + "<CountryCode>ESP</CountryCode>" + Chr(13) + Chr(10)
        'cXML = cXML + "</AddressInSpain>" + Chr(13) + Chr(10)
        'cXML = cXML + "</LegalEntity>" + Chr(13) + Chr(10)
        '
        'cXML = cXML + "</BuyerParty>" + Chr(13) + Chr(10)
        'cXML = cXML + "</Parties>" + Chr(13) + Chr(10)

    End Sub


End Class
