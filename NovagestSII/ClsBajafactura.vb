Public Class ClsBajafactura
    Private nEjercicio_ As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Private periodo_ As String
    Dim Ejercicio As String
    Private NIF_ As String
    Private empresa_ As String
    Private tipo_comunicacion_ As String
    Private version As String
    Public XMLs As String
    Public Sql As String

    Private Sub Class_Initialize()
        'Inicializar
        version = "0.6"
    End Sub

    Public WriteOnly Property ObjGlobal() As ObjetoGlobal.ObjetoGlobal
        Set(Value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = Value
        End Set

    End Property
    Public WriteOnly Property nif() As String
        Set(ByVal value As String)
            NIF_ = value
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

    Public Function FinTransmision() As Boolean
        PieEnvio()
    End Function

    Private Function CabeceraGeneral()
        XMLs = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>"
        XMLs = XMLs + "<soapenv:Envelope xmlns:soapenv=" & Chr(34) & "http://schemas.xmlsoap.org/soap/envelope/" & Chr(34) & ""
        XMLs = XMLs + "xmlns:siiLR = " & Chr(34) & "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd" & Chr(34) & ""
        XMLs = XMLs + "xmlns:sii=" & Chr(34) & "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd" & Chr(34) & ">"
        XMLs = XMLs + "<soapenv:Header/>"
        XMLs = XMLs + "<soapenv:Body>"

        '" & Chr(34) & "
    End Function

    Private Function CabeceraFacBaja()
        XMLs = XMLs + "<siiLR:BajaLRFacturasEmitidas>"
        XMLs = XMLs + "<sii:Cabecera>"
        XMLs = XMLs + "<sii:IDVersionSii>" & version & "</sii:IDVersionSii>"
        XMLs = XMLs + "<sii:Titular>"
        XMLs = XMLs + "<sii:NombreRazon>" & Trim(UCase(empresa_)) & "</sii:NombreRazon>"
        XMLs = XMLs + "<sii:NIF>" & Trim(NIF_) & "</sii:NIF>"
        XMLs = XMLs + "</sii:Titular>"
        XMLs = XMLs + "</sii:Cabecera>"

    End Function

    Private Sub PieEnvio()
        XMLs = XMLs + "</siiLR:BajaLRFacturasEmitidas>"
        XMLs = XMLs + "</soapenv:Body>"
        XMLs = XMLs + "</soapenv:Envelope>"
    End Sub

    Public ReadOnly Property envioXml() As String
        Get
            Return XMLs
        End Get
    End Property

    Private Function Factura()
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
        Dim TieneCabecera As Boolean

        ' TipoRectificativa = "S"
        ' S Por sustitución
        ' I Por diferencias

        tipo_comunicacion_ = "A0"
        Tipo_operacion = "Venta"
        TipoNoExenta = "S1"
        tipo_factura = "F1"
        Trascendencia = "01"

        TieneCabecera = False

        Rs.Open("SELECT * FROM Factura_vta_c WHERE " & Sql, ObjetoGlobal.cn)

        While Not Rs.EOF

            If Not TieneCabecera Then
                CabeceraGeneral()
                CabeceraFacBaja()
                TieneCabecera = True
            End If
            Serie_numero = Trim(Rs!serie_factura_vta) & CStr(Rs!numero_factura)
            RegistroFactura(Rs!Ejercicio, Rs!periodo, Rs!nif, Serie_numero, Rs!fecha_factura)
            Rs!Situacion = "P"
            Rs!csv = ""
            Rs!tipo_factura = ""
            Rs!Serie_numero = Serie_numero
            Rs.Update
            Rs.MoveNext
        End While
        PieEnvio()

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

    Private Function RegistroFactura(Ejercicio, periodo, nif, Ser_num, fecha)
        '-----------------------------------------
        ' Primer bloque de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<siiLR:RegistroLRBajaExpedidas>"
        XMLs = XMLs + "<sii:PeriodoImpositivo>"
        XMLs = XMLs + "<sii:Ejercicio>" & Ejercicio & "</sii:Ejercicio>"
        XMLs = XMLs + "<sii:Periodo>" & periodo & "</sii:Periodo>"
        XMLs = XMLs + "</sii:PeriodoImpositivo>"
        XMLs = XMLs + "<siiLR:IDFactura>"
        XMLs = XMLs + "<sii:IDEmisorFactura>"
        XMLs = XMLs + "<sii:NIF>" & nif & "</sii:NIF>"
        XMLs = XMLs + "</sii:IDEmisorFactura>"
        XMLs = XMLs + "<sii:NumSerieFacturaEmisor>" & Ser_num & "</sii:NumSerieFacturaEmisor>"
        XMLs = XMLs + "<sii:FechaExpedicionFacturaEmisor>" & Format(fecha, "dd-mm-yyyy") & "</sii:FechaExpedicionFacturaEmisor>"
        XMLs = XMLs + "</siiLR:IDFactura>"

    End Function

    Private Function CabeceraBajaFactura(Serie_numero, fecha_factura)
        '-----------------------------------------
        ' Cabecera de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<siiLR:IDFactura>"
        XMLs = XMLs + "<sii:IDEmisorFactura>"
        XMLs = XMLs + "<sii:NIF>" & NIF_ & "</sii:NIF>"
        XMLs = XMLs + "</sii:IDEmisorFactura>"
        XMLs = XMLs + "<sii:NumSerieFacturaEmisor>" & Trim(Left(Serie_numero, 60)) & "</sii:NumSerieFacturaEmisor>"
        XMLs = XMLs + "<sii:FechaExpedicionFacturaEmisor>" & fecha_factura & "</sii:FechaExpedicionFacturaEmisor>"
        XMLs = XMLs + "</siiLR:IDFactura>"
        XMLs = XMLs + "</siiLR:RegistroLRBajaExpedidas>"

    End Function


    '<?xml version="1.0" encoding="UTF-8"?>
    '<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/"
    'xmlns: siiLR = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd"
    'xmlns:sii="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd">
    '   <soapenv:Header/>
    '   <soapenv:Body>
    '
    '
    '      <siiLR:BajaLRFacturasEmitidas>
    '         <sii:Cabecera>
    '            <sii:IDVersionSii>0.6</sii:IDVersionSii>
    '            <sii:Titular>
    '               <sii:NombreRazon>EMPRESAXXXX</sii:NombreRazon>
    '               <sii:NIF>A84532501</sii:NIF>
    '            </sii:Titular>
    '         </sii:Cabecera>
    '
    '
    '         <siiLR:RegistroLRBajaExpedidas>
    '            <sii:PeriodoImpositivo>
    '               <sii:Ejercicio>2016</sii:Ejercicio>
    '               <sii:Periodo>10</sii:Periodo>
    '            </sii:PeriodoImpositivo>
    '            <siiLR:IDFactura>
    '               <sii:IDEmisorFactura>
    '                  <sii:NIF>A84532501</sii:NIF>
    '               </sii:IDEmisorFactura>
    '               <sii:NumSerieFacturaEmisor>000000000000000000000000000001000000000000000000000000000001</sii:NumSerieFacturaEmisor>
    '               <sii:FechaExpedicionFacturaEmisor>08-05-2015</sii:FechaExpedicionFacturaEmisor>
    '            </siiLR:IDFactura>
    '         </siiLR:RegistroLRBajaExpedidas>
    '      </siiLR:BajaLRFacturasEmitidas>
    '   </soapenv:Body>
    '</soapenv:Envelope>



End Class
