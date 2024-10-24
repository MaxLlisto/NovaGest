Public Class ClsBienesdeinversion
    Private nEjercicio_ As String
    Private ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Private periodo_ As String
    Private Ejercicio_ As String
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

    Public WriteOnly Property Ejercicio() As String
        Set(ByVal value As String)
            nEjercicio_ = value
        End Set
    End Property

    Public WriteOnly Property periodo() As String
        Set(ByVal value As String)
            periodo_ = value
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

        XMLs = "<soapenv:Envelope xmlns:soapenv= & Chr(34) & http://schemas.xmlsoap.org/soap/envelope/& Chr(34) & "
        XMLs = XMLs + "xmlns:Sum= & Chr(34) & https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd & Chr(34) " + vbCrLf
        XMLs = XMLs + "xmlns:sum1= & Chr(34) & https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd & Chr(34) & " > "" + vbCrLf
        XMLs = XMLs + "<soapenv:Header/>" + vbCrLf
        XMLs = XMLs + "<soapenv:Body>" + vbCrLf

        '" & Chr(34) & "
    End Function

    Private Function CabeceraFacEmision()

        XMLs = XMLs + "<sum:SuministroLRBienesInversion>"
        XMLs = XMLs + "<sum1:Cabecera>"
        XMLs = XMLs + "<sum1:IDVersionSii>" & version & "</sum1:IDVersionSii>"
        XMLs = XMLs + "<sum1:Titular>"
        XMLs = XMLs + "<sum1:NombreRazon>" & Trim(UCase(empresa_)) & "</sum1:NombreRazon>"
        XMLs = XMLs + "<sum1:NIF>" & Trim(NIF_) & "</sum1:NIF>"
        XMLs = XMLs + "</sum1:Titular>"
        XMLs = XMLs + "<sum1:TipoComunicacion>" & tipo_comunicacion_ & "</sum1:TipoComunicacion>"
        XMLs = XMLs + "</sum1:Cabecera>"

    End Function

    Private Sub PieEnvio()

        XMLs = XMLs + "</sum:SuministroLRBienesInversion>"
        XMLs = XMLs + "</soapenv:Body>"
        XMLs = XMLs + "</soapenv:Envelope>"
    End Sub

    Public ReadOnly Property envioXml() As String
        Get
            envioXml = XMLs
        End Get
    End Property

    Private Function RegistroFacturaRecibida(Ejercicio)
        '-----------------------------------------
        ' Primer bloque de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<sum:RegistroLRBienesInversion>"
        XMLs = XMLs + "<sum1:PeriodoImpositivo>"
        XMLs = XMLs + "<sum1:Ejercicio>" & Ejercicio & "</sum1:Ejercicio>"
        XMLs = XMLs + "<sum1:Periodo>0A</sum1:Periodo>"
        XMLs = XMLs + "</sum1:PeriodoImpositivo>"

    End Function

    Private Function CabeceraFacturaRecibida(Serie_numero, fecha_factura, razon, numnif)
        '-----------------------------------------
        ' Cabecera de cada factura emitida"
        '-----------------------------------------
        XMLs = XMLs + "<sum:IDFactura>"
        XMLs = XMLs + "<sum1:IDEmisorFactura>"
        XMLs = XMLs + "<sum1:NombreRazon>" & razon & "</sum1:NombreRazon>"
        XMLs = XMLs + "<sum1:NIF>" & numnif & "</sum1:NIF>"
        XMLs = XMLs + "</sum1:IDEmisorFactura>"
        XMLs = XMLs + "<sum1:NumSerieFacturaEmisor>" & Trim(Left(Serie_numero, 60)) & "</sum1:NumSerieFacturaEmisor>"
        XMLs = XMLs + "<sum1:FechaExpedicionFacturaEmisor>" & fecha_factura & "</sum1:FechaExpedicionFacturaEmisor>"
        XMLs = XMLs + "</sum:IDFactura>"

    End Function

    Private Function DetalleDelBien(Identificacion, fechaInicio, Prorrata)

        XMLs = XMLs + "<sum:BienesInversion>"
        XMLs = XMLs + "<sum1:IdentificacionBien>" & Identificacion & "</sum1:IdentificacionBien>"
        XMLs = XMLs + "<sum1:FechaInicioUtilizacion>" & fechaInicio & "</sum1:FechaInicioUtilizacion>"
        XMLs = XMLs + "<sum1:ProrrataAnualDefinitiva>" & Prorrata & "</sum1:ProrrataAnualDefinitiva>"
        XMLs = XMLs + "</sum:BienesInversion>"
        XMLs = XMLs + "</sum:RegistroLRBienesInversion"

    End Function


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
        Dim HayCabecera As Boolean


        '' TipoRectificativa = "S"
        '' S Por sustitución
        '' I Por diferencias
        '
        '  Tipo_operacion = "Compra"
        '  TipoNoExenta = "S1"
        '  tipo_factura = "F1"
        '  HayCabecera = False
        '
        '  Rs.Open "SELECT * FROM Factura_com_c WHERE " & Sql, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        '  While Not Rs.EOF
        '
        '        If Not HayCabecera Then
        '           InicioTransmision
        '           HayCabecera = True
        '        End If
        '        RegistroFacturaRecibida Ejercicio_
        '        Serie_numero = Trim(Rs!serie_factura_com) & CStr(Rs!numero_fra_com)
        '        fecha_factura = Format(Rs!fecha_factura, "dd-mm-yyyy")
        '        CabeceraFacturaRecibida Serie_numero, fecha_factura, Trim(Rs!razon_social), Rs!cif_prov
        '        'DetalleDelBien(Identificacion, fechaInicio, Prorrata)
        '        RsTot.Open "SELECT * FROM Factura_com_c_tot WHERE empresa = '" & Rs!empresa & "' AND serie_factura_com = '" & Rs!serie_factura_com & "' AND numero_fra_com=" & Rs!numero_fra_com & " and tipo_linea = 'I' order by tipo_orden", ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
        '        DesgloseIVAFactura RsTot
        '        RsTot.Close
        '        Rs.MoveNext
        '  Wend
        '
        '  If HayCabecera Then
        '     PieEnvio
        '  End If
        '  Rs.Close

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


    'TOP 1000 [empresa]
    '      ,[numero_entrada]
    '      ,[numero_envio]
    '      ,[serie_factura_com]
    '      ,[numero_fra_com]
    '      ,[fecha_factura]
    '      ,[fecha_registro]
    '      ,[fecha_envio]
    '      ,[situacion]
    '      ,[csv]
    '      ,[tipo_factura]
    '
    'ipt para el comando SelectTopNRows de SSMS  ******/
    'SELECT TOP 1000 [empresa]
    '      ,[serie_factura_vta]
    '      ,[numero_factura]
    '      ,[numero_envio]
    '      ,[fecha_factura]
    '      ,[fecha_envio]
    '      ,[situacion]
    '      ,[csv]
    '      ,[tipo_factura]
    '  From [Compugest].[dbo].[factura_vta_aeat]



End Class
