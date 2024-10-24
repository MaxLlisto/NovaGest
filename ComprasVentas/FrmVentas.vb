Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing

Public Class FrmVentas
    Inherits LibreriaModeloMantenimiento.ModeloMantenimiento
    Public ArticuloControlado As Boolean = False
    Public CINF As Object
    Public ProcesoActual As Long
    Public SerieFacturaAImprimir As String
    Public NumeroFacturaAImprimir As Long
    Public FechaFacturaAImprimir As Date
    Public TipoDocumentoFacturaAImprimir As String
    Public TipoDesgloseFacturaAImprimir As String
    Public TipoVentaFacturaAImprimir As String
    Public FacturaYaGrabada As Boolean
    Dim TBaseImponible(2) As Double
    Dim TTipoIva(2) As Double
    Dim TPorcentajeIva(2) As Double
    Dim TImporteIva(2) As Double
    Dim TPorcentajeRecargo(2) As Double
    Dim TImporteRecargo(2) As Double
    Dim TotalesFactura(5) As Double
    Dim TotalPesoNeto As Double
    Dim TotalLineas As Double
    Dim TotalIva As Double
    Dim TotalRecargo As Double
    Dim TotalDocumento As Double
    Dim CuantosIVAs As Integer

    Dim HayDatosEdicion As Boolean
    Dim Ndoc As Integer
    Dim DecimalesImporte As Integer
    Dim DecimalesPrecio As Integer
    Dim Cambio As Double
    Dim FrmEstImpO As Object
    Dim DecimalesKilos As Integer
    Dim Linea As Integer
    Dim ArrayFechaImporteVto()

    Dim EFechas() As Date
    Dim ECuentas() As String
    Dim EDescripciones() As String
    Dim EConceptos() As String
    Dim EDebe() As Double
    Dim EHaber() As Double
    Dim EDocumentos() As String
    Dim EReferencias() As String
    Dim ETipos() As String
    Dim EDiarios() As Integer
    Dim EDiscriminanteAsiento() As Integer
    Dim ENumeroApuntes As Long

    Private Sub FrmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub BtCaja_Click(sender As Object, e As EventArgs) Handles BtCaja.Click
        Dim oCaja As Frmarqueocaja = New Frmarqueocaja
        oCaja.ObjetoGlobal = Me.ObjetoGlobal
        oCaja.ShowDialog()
    End Sub

    Private Sub BtAlbaran_Click(sender As Object, e As EventArgs) Handles BtAlbaran.Click
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CodigoCliente As Long
        Dim RsCondiciones As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim oFrm As FrmFirmaDocumentos = New FrmFirmaDocumentos
        Dim PathFirma As String

        If ArticuloControlado Then
            MsgBox("En los artículos de estas características no se admiten los albaranes")
            Return
        End If

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("Debe aceptar o cancelar la modificación antes de imprimir.")
            Return
        End If
        If CnTabla02.cuantos = 0 Then
            MsgBox("No existen líneas para imprimir")
            Return
        End If

        RsSocios.Open("SELECT codigo_socio, alta_baja_coop from socios_coop where codigo_socio =" & (CLng(CnTabla01.ValorCampo("codigo_cliente")) - 10000), ObjetoGlobal.cn)
        If RsSocios.RecordCount > 0 Then
            If RsSocios!Alta_baja_coop = "B" Then
                CodigoCliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
                Sql = "SELECT * FROM COND_GEN_VTA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CLIENTE = " + CStr(CodigoCliente) + " AND TIPO_VENTA = 'C' AND VALIDEZ = 'S'  AND FORMA_PAGO<>'CONT' ORDER BY 1,2,3,4"
                RsCondiciones.Open(Sql, ObjetoGlobal.cn)
                If RsCondiciones.RecordCount = 0 Then
                    MsgBox("Imposible emitir albarán, este socio se encuentra en situación de baja.")
                    Return
                End If
                RsCondiciones.Close()
            End If
        Else
            CodigoCliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
            Sql = "SELECT * FROM COND_GEN_VTA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CLIENTE = " + CStr(CodigoCliente) + " AND TIPO_VENTA = 'C' AND VALIDEZ = 'S'  AND FORMA_PAGO<>'CONT' ORDER BY 1,2,3,4"
            RsCondiciones.Open(Sql, ObjetoGlobal.cn)
            If RsCondiciones.RecordCount = 0 Then
                MsgBox("Imposible emitir albarán de crédito: cliente con forma de pago CONTADO.")
                Return
            End If
            RsCondiciones.Close()
        End If
        RsSocios.Close()

        'Firmar albarán
        PathFirma = "\\Srvv01\Firmas\" + Trim("IM" + Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))) + ".bmp"
        If Trim(Dir(PathFirma)) = "" Then
            oFrm.path = "\\Srvv01\Firmas"
            oFrm.TextoAlbaran = Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))
            oFrm.Importes = CnTabla01.ValorCampo("total_albaran")
            If oFrm.ShowDialog() <> DialogResult.OK Then
                Return
            End If
        End If
        ImprimirAlbaranVenta(CnTabla01.ValorCampo("empresa"), CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"), False, False, 1)

    End Sub

    Private Sub BtCredito_Click(sender As Object, e As EventArgs) Handles BtCredito.Click
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CodigoCliente As Long
        Dim RsCondiciones As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim oFrm As FrmFirmaDocumentos = New FrmFirmaDocumentos
        Dim PathFirma As String
        Dim SerNum As String

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("Debe aceptar o cancelar la modificación antes de imprimir.")
            Return
        End If
        If CnTabla02.cuantos = 0 Then
            MsgBox("No existen líneas para imprimir")
            Return
        End If
        If CnTabla01.cuantos = 0 Then
            MsgBox("Debes indicar albarán para FACTURA DE CRÉDITO")
            Exit Sub
        End If

        RsSocios.Open("SELECT codigo_socio, alta_baja_coop from socios_coop where codigo_socio =" & (CLng(CnTabla01.ValorCampo("codigo_cliente")) - 10000), ObjetoGlobal.cn)
        If RsSocios.RecordCount > 0 Then
            If RsSocios!Alta_baja_coop = "B" Then
                CodigoCliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
                Sql = "SELECT * FROM COND_GEN_VTA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CLIENTE = " + CStr(CodigoCliente) + " AND TIPO_VENTA = 'C' AND VALIDEZ = 'S'  AND FORMA_PAGO<>'CONT' ORDER BY 1,2,3,4"
                RsCondiciones.Open(Sql, ObjetoGlobal.cn)
                If RsCondiciones.RecordCount = 0 Then
                    MsgBox("Imposible emitir albarán, este socio se encuentra en situación de baja.")
                    Return
                End If
                RsCondiciones.Close()
            End If
        Else
            CodigoCliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
            Sql = "SELECT * FROM COND_GEN_VTA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CLIENTE = " + CStr(CodigoCliente) + " AND TIPO_VENTA = 'C' AND VALIDEZ = 'S'  AND FORMA_PAGO<>'CONT' ORDER BY 1,2,3,4"
            RsCondiciones.Open(Sql, ObjetoGlobal.cn)
            If RsCondiciones.RecordCount = 0 Then
                MsgBox("Imposible emitir albarán de crédito: cliente con forma de pago CONTADO.")
                Return
            End If
            RsCondiciones.Close()
        End If
        RsSocios.Close()

        'Firmar albarán
        PathFirma = "\\Srvv01\Firmas\" + Trim("IM" + Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))) + ".bmp"
        If Trim(Dir(PathFirma)) = "" Then
            oFrm.path = "\\Srvv01\Firmas"
            oFrm.TextoAlbaran = Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))
            oFrm.Importes = CnTabla01.ValorCampo("total_albaran")
            If oFrm.ShowDialog() <> DialogResult.OK Then
                Return
            End If
            'CmdImprimir_Click
        End If

        If Trim(CnTabla01.ValorCampo("Situacion")) = "N" Then
            If GrabaFacturaCredito() Then
                ImprimirFacturaVentaCredito(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, SerNum, True, ArticuloControlado)
            End If
        Else
            SerieFacturaAImprimir = Trim(CnTabla01.ValorCampo("serie_factura_vta"))
            NumeroFacturaAImprimir = CLng(CnTabla01.ValorCampo("numero_factura"))
            ImprimirFacturaVentaCredito(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, SerNum, True, ArticuloControlado)
        End If

    End Sub

    Private Sub BtFactura_Click(sender As Object, e As EventArgs) Handles BtFactura.Click
        Dim PathFirma As String
        Dim SerNum As String
        Dim Sql As String
        Dim oFrm As FrmFirmaDocumentos = New FrmFirmaDocumentos

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("Debe aceptar o cancelar la modificación antes de imprimir.")
            Return
        End If
        If CnTabla02.cuantos = 0 Then
            MsgBox("No existen líneas para imprimir")
            Return
        End If
        If CnTabla01.cuantos = 0 Then
            MsgBox("Debes indicar albarán para la FACTURA")
            Exit Sub
        End If
        If Trim(CnTabla01.ValorCampo("albaran_contado")) = "P" Then
            MsgBox("Imposible facturar un albarán pendiente desde este proceso.")
            Return
        End If

        PathFirma = "\\Srvv01\Firmas\" + Trim("IM" + Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))) + ".bmp"
        SerNum = Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))

        If ArticuloControlado Then
            If Trim(Dir(PathFirma)) = "" Then
                oFrm.path = "\\Srvv01\Firmas"
                oFrm.TextoAlbaran = Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))
                oFrm.Importes = CnTabla01.ValorCampo("total_albaran")
                If oFrm.ShowDialog() <> DialogResult.OK Then
                    Return
                End If
                'CmdImprimir_Click
            End If
        End If



        If Trim(Dir(PathFirma)) = "" Then
            PathFirma = ""
            SerNum = ""
        End If

        If Trim(CnTabla01.ValorCampo("Situacion")) = "N" Then
            If GrabaFacturaContado() Then
                ImprimirFacturaVenta(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, "", True, ArticuloControlado)
            End If
        Else
            SerieFacturaAImprimir = Trim(CnTabla01.ValorCampo("serie_factura_vta"))
            NumeroFacturaAImprimir = CLng(CnTabla01.ValorCampo("numero_factura"))
            ImprimirFacturaVenta(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, "", True, ArticuloControlado)
        End If

    End Sub

    Public Sub ImprimirFacturaVenta(empresa As String, Serie As String, Numero As Long, bPrevisualizar As Boolean, nNumeroCopias As Integer, PathFirma As String, NumeroSerie As String, Optional bPreviewReal As Boolean = True, Optional Art_controlado As Boolean = False)
        Dim i As Integer, SQL As String
        Dim cDoc As String
        Dim RsCabeceras_factura As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsLineas_factura As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        cDoc = "Formato1"
        If Trim(ObjetoGlobal.EmpresaActual) = "1" Then
            cDoc = "formatohorto"
        End If
        If Art_controlado Then
            cDoc = "Formato1 ctr"
        End If


        Dim oImp As ReportBuilder2.ClsImpresion

        oImp = New ReportBuilder2.ClsImpresion(Me.ObjetoGlobal)

        If Trim(ObjetoGlobal.EmpresaActual) = "3" Then
            If Not oImp.Inicializar("Factura de venta A5", "Factura de venta A5", "Formato2", 0, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No existe el formato para la impresión del albarán de venta")
                Return
            End If
        Else
            If Not oImp.Inicializar("Factura de venta A5", "Factura de venta A5", cDoc, 0, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No existe el formato para la impresión del albarán de venta")
                Return
            End If
        End If

        SQL = "SELECT * FROM FACTURA_VTA_C WHERE "
        SQL = Trim(SQL) + " EMPRESA = '" + Trim(empresa) & "' AND SERIE_FACTURA_VTA='" & Trim(Serie) & "' AND NUMERO_FACTURA=" & CStr(Numero)

        RsCabeceras_factura.Open(SQL, ObjetoGlobal.cn)
        SQL = "SELECT * FROM FACTURA_VTA_L WHERE "
        SQL = Trim(SQL) + " EMPRESA = '" + Trim(empresa) & "' AND SERIE_FACTURA_VTA='" & Trim(Serie) & "' AND NUMERO_FACTURA=" & CStr(Numero)
        SQL = Trim(SQL) + " ORDER BY EMPRESA,SERIE_FACTURA_VTA,NUMERO_FACTURA,LINEA_FACTURA"

        RsLineas_factura.Open(SQL, ObjetoGlobal.cn)

        'CINF.CINFEmpresa = Trim(empresa)
        'CINF.CINFEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'CINF.CINFTipoDocumento = "Factura de venta"
        'CINF.CINFSerie = Trim(Serie)
        'CINF.CINFNumeroDocumento = CStr(Numero)
        'CINF.CINFUsuario = Trim(ObjetoGlobal.nombreusuario)
        RellenaFacturaVenta(PathFirma, NumeroSerie, RsCabeceras_factura, RsLineas_factura, oImp)
        oImp.Imprimir()
        'If bPreviewReal = True Then CINF.Imprimir
    End Sub
    Private Sub RellenaFacturaVenta(ByVal PathFirma As String, ByVal NumeroSerie As String, ByRef RsCabeceras_factura As cnRecordset.CnRecordset, ByRef RsLineas_factura As cnRecordset.CnRecordset, ByRef oImp As ReportBuilder2.ClsImpresion)
        VuelcaCuerpo_FacturaVenta(PathFirma, NumeroSerie, RsCabeceras_factura, RsLineas_factura, oImp)
    End Sub

    Private Sub VuelcaCabecera_FacturaVenta(nPag, RsCabeceras_factura, oImp)
        Dim RsProvincias As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim FlagValorado As Boolean

        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.documento", 0, "Factura")
        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.pagina", 0, "" & nPag)

        If RsCabeceras_factura.RecordCount > 0 Then
            For i = 0 To RsCabeceras_factura.cuantoscampos - 1

                If Trim(RsCabeceras_factura.NombreCampo(i)) = "numero_factura" Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c.numero_factura", 0, Trim(CStr(RsCabeceras_factura!serie_factura_vta)) & Trim(CStr(RsCabeceras_factura!numero_factura)))
                Else
                    If Not IsDBNull(RsCabeceras_factura(i)) Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c." + Trim(RsCabeceras_factura.NombreCampo(i)), 0, Trim(CStr(RsCabeceras_factura(i))))
                    End If
                End If
            Next
            If Not IsDBNull(RsCabeceras_factura!provincia_cliente) Then
                RsProvincias.Open("SELECT * FROM PROVINCIAS_COOP WHERE PROVINCIA = '" & RsCabeceras_factura!provincia_cliente & "'", ObjetoGlobal.cn)
                If RsProvincias.RecordCount > 0 Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "Calculado.Provincia", 0, IIf(IsDBNull(RsProvincias!nombre_provincia), "", Trim(RsProvincias!nombre_provincia)))
                End If
                RsProvincias.Close()
            End If
        End If
    End Sub
    Private Sub VuelcaCuerpo_FacturaVenta(PathFirma, NumeroSerie, RsCabeceras_factura, RsLineas_factura, oImp)
        Dim i As Integer
        Dim j As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nPag As Integer
        Dim nLineaActual As Integer
        nLineaActual = 0
        nPag = 1

        VuelcaCabecera_FacturaVenta(nPag, RsCabeceras_factura, oImp)

        If RsLineas_factura.RecordCount > 0 Then
            For i = 1 To RsLineas_factura.RecordCount
                RsLineas_factura.AbsolutePosition = i
                If nLineaActual >= 12 Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.sumaysigue", 0, "Sigue en la página número " & (nPag + 1))
                    nPag = nPag + 1
                    VuelcaCabecera_FacturaVenta(nPag, RsCabeceras_factura, oImp)
                    nLineaActual = 0
                End If
                nLineaActual = nLineaActual + 1
                Rs.Open("SELECT * FROM LINEAS_ALBARAN WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERIE_ALBARAN = '" + Trim(RsLineas_factura!serie_albaran_vta) + "' AND NUMERO_ALBARAN = " + Trim(RsLineas_factura!numero_albaran_vta) + " AND LINEA_ALBARAN  =" + CStr(RsLineas_factura!linea_salida), ObjetoGlobal.cn)
                If Rs.RecordCount > 0 Then
                    For j = 0 To Rs.cuantosCampos - 1
                        If Not IsDBNull(Rs(j)) Then
                            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado." + Trim(Rs.NombreCampo(j)) & nLineaActual, 0, Trim(CStr(Rs(j))))
                        End If
                    Next
                Else
                    MsgBox("Anote error en línea " + CStr(i) + " de factura")
                End If
                Rs.Close()
            Next
        End If
        VuelcaPie_FacturaVenta(nPag, PathFirma, RsCabeceras_factura, RsLineas_factura, oImp)
    End Sub
    Private Sub VuelcaPie_FacturaVenta(nPag, PathFirma, RsCabeceras_factura, RsLineas_factura, oImp)
        Dim i As Integer
        Dim j As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim TT(10) As Double

        Sql = "SELECT * FROM FACTURA_VTA_C_TOT WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERIE_FACTURA_VTA='" + Trim(SerieFacturaAImprimir) + "' AND NUMERO_FACTURA=" + CStr(NumeroFacturaAImprimir) + " ORDER BY 1,2,3,4,5"
                            Rs2.Open(Sql, ObjetoGlobal.cn)
        j = 0
        For i = 1 To Rs2.RecordCount
            Rs2.AbsolutePosition = i
            If Trim(Rs2!tipo_linea) = "B" Then
                TT(0) = Math.Round(Rs2!importe_cargo, 2)
            ElseIf Trim(Rs2!tipo_linea) = "I" Then
                j = j + 1
                TT(3 * j - 2) = Math.Round(Rs2!Base, 2)
                TT(3 * j - 1) = Rs2!Porcentaje
                TT(3 * j) = Math.Round(Rs2!importe_cargo, 2)
            ElseIf Trim(Rs2!tipo_linea) = "T" Then
                TT(10) = Math.Round(Rs2!importe_cargo, 2)
            End If
        Next
        Rs2.Close()

        For i = 1 To j
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.base_iva_" + CStr(i), 0, Trim(CStr(TT(3 * i - 2))))
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.porcentaje_iva_" + CStr(i), 0, Trim(CStr(TT(3 * i - 1))))
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.importe_iva_" + CStr(i), 0, Trim(CStr(TT(3 * i))))
        Next
        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.total_factura", 0, Trim(CStr(TT(10))))
        If Not IsDBNull(RsCabeceras_factura!cod_banco) Then
            Rs.Open("SELECT * FROM BANCOS WHERE CODIGO_BANCO ='" + Trim(RsCabeceras_factura!cod_banco) + "'", ObjetoGlobal.cn)
            If Rs.RecordCount > 0 Then
                oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.banco" + CStr(i), 0, Trim(Rs!nombre_banco))
            End If
            Rs.Close()
        End If
        If Not IsDBNull(RsCabeceras_factura!clave_bancaria) Then
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.cuenta", 0, Trim(RsCabeceras_factura!clave_bancaria))
        End If
        PathFirma = Trim(PathFirma)
        If Trim(PathFirma) <> "" Then
            oImp.VolcarImagenAImpresion(CLng(nPag), 0, -1, 0, "calculado.imagen1", 0, PathFirma)
        End If
    End Sub

    Public Sub ImprimirAlbaranVenta(empresa As String, Serie As String, Numero As Long, bPrevisualizar As Boolean, nNumeroCopias As Integer, Optional bPreviewReal As Boolean = True)
        Dim i As Integer, SQL As String
        Dim cDoc As String
        Dim RsCabeceras_Albaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsLineas_Albaran As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        cDoc = "formato A5 modificado"

        Dim oImp As ReportBuilder2.ClsImpresion

        oImp = New ReportBuilder2.ClsImpresion(Me.ObjetoGlobal)

        If Trim(ObjetoGlobal.EmpresaActual) = "3" Then
            If Not oImp.Inicializar("Factura de venta A5AL", "Factura de venta A5", "Formato2", 0, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No existe el formato para la impresión del albarán de venta")
                Return
            End If
        Else
            If Not oImp.Inicializar("Factura de venta A5", "Factura de venta A5", cDoc, 0, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No existe el formato para la impresión del albarán de venta")
                Return
            End If
        End If

        SQL = "SELECT * FROM CABECERAS_ALBARAN WHERE "
        SQL = Trim(SQL) + " EMPRESA = '" + Trim(empresa) & "' AND SERIE_ALBARAN='" & Trim(Serie) & "' AND NUMERO_ALBARAN=" & CStr(Numero)
        RsCabeceras_Albaran.Open(SQL, ObjetoGlobal.cn)

        SQL = "SELECT * FROM LINEAS_ALBARAN WHERE "
        SQL = Trim(SQL) + " EMPRESA = '" + Trim(empresa) & "' AND SERIE_ALBARAN='" & Trim(Serie) & "' AND NUMERO_ALBARAN=" & CStr(Numero)
        SQL = Trim(SQL) + " ORDER BY EMPRESA,SERIE_ALBARAN,NUMERO_ALBARAN,LINEA_ALBARAN"
        RsLineas_Albaran.Open(SQL, ObjetoGlobal.cn)

        'CINF.CINFEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'CINF.CINFEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'CINF.CINFTipoDocumento = "Albaran de venta"
        'CINF.CINFSerie = Trim("" & RsCabeceras_Albaran!serie_albaran)
        'CINF.CINFNumeroDocumento = CStr("" & RsCabeceras_Albaran!numero_albaran)
        'CINF.CINFUsuario = Trim(ObjetoGlobal.nombreusuario)
        RellenaAlbaranVenta(oImp, RsCabeceras_Albaran, RsLineas_Albaran)
        'If bPreviewReal = True Then CINF.Imprimir
        oImp.Imprimir()
    End Sub

    Public Function ObtenerNumeroFactura(empresa As String, Serie As String, UtilizaPendientes As Boolean) As Long
        Dim RsPendientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsContadores As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim FlagExiste As Boolean
        Dim FlagCambiado As Boolean
        Dim Contador As Long
        Dim trans As SqlClient.SqlTransaction


        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            Contador = -1
            If UtilizaPendientes = True Then
                RsPendientes.Open("SELECT * FROM CONTADORES_PEND WHERE EMPRESA = '" + Trim(empresa) + "' AND EJERCICIO = 'T' AND NOMBRE = 'ultima_factura' AND SERIE = '" + Trim(Serie) + "' ORDER BY 1,2,3,4,5", ObjetoGlobal.cn, True)
                If RsPendientes.RecordCount > 0 Then
                    Do While Contador < 0 And RsPendientes.EOF = False
                        If RsPendientes!Contador >= 0 Then Contador = RsPendientes!Contador
                        RsPendientes.Delete
                    Loop
                End If
                RsPendientes.Close
            End If
            RsContadores.Open("SELECT * FROM CONTADORES WHERE EMPRESA = '" + Trim(empresa) + "' AND EJERCICIO = 'T' AND NOMBRE = 'ultima_factura'  AND SERIE = '" + Trim(Serie) + "'", ObjetoGlobal.cn, True)
            If RsContadores.RecordCount <= 0 Then
                If Contador < 0 Then Contador = 1
                RsContadores.AddNew
                RsContadores!empresa = ObjetoGlobal.EmpresaActual
                RsContadores!Ejercicio = "T"
                RsContadores!Nombre = "ultima_factura"
                RsContadores!Serie = Trim(Serie)
                RsContadores!Contador = Contador
                RsContadores.Update
            Else
                If Contador < 0 Then Contador = RsContadores!Contador + 1
                RsContadores!Contador = Contador
                RsContadores.Update
            End If

            FlagExiste = True
            FlagCambiado = False
            Do While FlagExiste = True
                FlagExiste = False
                RsFacturas.Open("SELECT EMPRESA FROM FACTURA_VTA_C WHERE EMPRESA = '" + Trim(empresa) + "' AND SERIE_FACTURA_VTA= '" + Trim(Serie) + "' AND NUMERO_FACTURA = '" + CStr(Contador) + "'", ObjetoGlobal.cn, True)
                If RsFacturas.RecordCount > 0 Then
                    Contador = Contador + 1
                    FlagExiste = True
                    FlagCambiado = True
                End If
                RsFacturas.Close
            Loop
            If FlagCambiado = True Then
                RsContadores!Contador = Contador
                RsContadores.Update
            End If
            RsContadores.Close
            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return Contador
        Catch ex As Exception
            trans.Rollback()
            MsgBox(ex.Message & "(" & ex.ToString & ")")
        End Try

        Return -1

HacerRollBack:
        trans.Rollback()
    End Function

    'Protected Overrides Function HOOK_Antes_de_hacer_LOAD() As Boolean
    'Protected Overrides Function HOOK_Despues_de_AsignarParametros() As Boolean
    'Protected Overrides Function HOOK_Después_de_CrearArrayDeControles() As Boolean
    'Protected Overrides Function HOOK_Antes_de_VisibilidadTabs() As Boolean
    'Protected Overrides Function HOOK_Despues_de_InicializarControles() As Boolean
    'Protected Overrides Function HOOK_Despues_de_ConstruirGrids() As Boolean
    'Protected Overrides Function HOOK_Antes_de_SeleccionInicialGrid() As Boolean
    'Protected Overrides Function HOOK_Despues_de_SeleccionInicialGrid() As Boolean
    'Protected Overrides Function HOOK_Despues_de_AsociarManejadores() As Boolean
    'Protected Overrides Function HOOK_Despues_de_hacer_LOAD() As Boolean

    'Protected Overrides Function HOOK_Antes_de_CTAnadir(CT As CnTabla.CnTabla) As Boolean
    'Protected Overrides Function HOOK_Antes_de_llamar_a_GridEdicion_en_CTAnadir(CT As CnTabla.CnTabla, GridEdicion As LibreriaGeneral.GridEdicion, ByRef Posicion As Point)
    'Protected Overrides Function HOOK_Antes_de_Crear_Ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Antes_de_Crear_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_En_el_validating_de_un_campo_en_creacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String) As Boolean
    'Public Overrides Function HOOK_En_el_validating_de_un_campo_en_creacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_Despues_de_modificar_campo_en_creacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String) As Boolean
    'Public Overrides Function HOOK_Despues_de_modificar_campo_en_creacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_Antes_de_comprobar_valores_creacion_formato_ficha(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean) As Boolean
    'Public Overrides Function HOOK_Antes_de_comprobar_valores_creacion_formato_grid(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean, GE As DataGridView, HH As Hashtable) As Boolean
    'Public Overrides Function HOOK_Antes_de_InsertarFila_en_creacion(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Antes_de_Update_en_creacion(CT As CnTabla.CnTabla, ByRef Rs As cnRecordset.CnRecordset) As Boolean
    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_creacion_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_creacion_formato_grid(CT As CnTabla.CnTabla) As Boolean
    'Protected Overrides Function HOOK_Proceso_de_creacion_finalizado_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Proceso_de_creacion_finalizado_formato_grid(CT As CnTabla.CnTabla) As Boolean

    'Protected Overrides Function HOOK_Antes_de_CTModificar(CT As CnTabla.CnTabla) As Boolean
    'Protected Overrides Function HOOK_Antes_de_llamar_a_GridEdicion_en_CTModificar(CT As CnTabla.CnTabla, GridEdicion As LibreriaGeneral.GridEdicion, ByRef Posicion As Point)
    'Protected Overrides Function HOOK_Antes_de_Modificar_Ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Antes_de_Modificar_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_En_el_validating_de_un_campo_en_modificacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String) As Boolean 'Comentado Probado Ejemplo
    'Public Overrides Function HOOK_En_el_validating_de_un_campo_en_modificacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_Despues_de_modificar_campo_en_modificacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String) As Boolean
    'Public Overrides Function HOOK_Despues_de_modificar_campo_en_modificacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_Antes_de_comprobar_valores_modificacion_formato_ficha(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean) As Boolean
    'Public Overrides Function HOOK_Antes_de_comprobar_valores_modificacion_formato_grid(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean, GE As DataGridView, HH As Hashtable) As Boolean
    'Public Overrides Function HOOK_Antes_de_ModificarFila_en_modificacion(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Antes_de_Update_en_modificacion(CT As CnTabla.CnTabla, ByRef Rs As cnRecordset.CnRecordset) As Boolean
    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_modificacion_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_modificacion_formato_grid(CT As CnTabla.CnTabla) As Boolean
    'Protected Overrides Function HOOK_Proceso_de_modificacion_finalizado_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Proceso_de_modificacion_finalizado_formato_grid(CT As CnTabla.CnTabla) As Boolean


    'Public Overrides Function HOOK_Antes_de_establecer_seleccion(CT As CnTabla.CnTabla, ByRef SQLGridSinWhere As String, ByRef SQLWhere As String, ByRef SqlOrder As String) as boolean
    'Public Overrides Function HOOK_Despues_de_seleccionar(CT As CnTabla.CnTabla) as boolean

    'Protected Overrides Function HOOK_Antes_de_eliminar_registro(CT As CnTabla.CnTabla) As Boolean 'Comentado
    'Protected Overrides Function HOOK_Despues_de_eliminar_registro(CT) As Boolean 'Comentado

    'Public Overrides Function HOOK_Cambio_de_registro(CT As CnTabla.CnTabla) As Boolean 'Comentado Probado Ejemplo

    'Protected Overrides Function HOOK_Antes_de_CambiarSituacionFormulario_Descendiente(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, ByRef SeIgnoraCambio As Boolean) As Boolean
    'Protected Overrides Function HOOK_Despues_de_CambiarSituacionFormulario_Descendiente(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, Retorno As String) As Boolean
    'Protected Overrides Function HOOK_Antes_de_CambiarSituacionFormulario_OtrasTablas(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, ByRef SeIgnoraCambio As Boolean) As Boolean
    'Protected Overrides Function HOOK_Despues_de_CambiarSituacionFormulario_OtrasTablas(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, Retorno As String) As Boolean
    'Protected Overrides Function HOOK_Antes_de_CambiarSituacionFormulario_TablaEnEdicion(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, ByRef SeIgnoraCambio As Boolean) As Boolean
    'Protected Overrides Function HOOK_Despues_de_CambiarSituacionFormulario_TablaEnEdicion(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, Retorno As String) As Boolean









    '=== Proceso de inicialización ===

    'Protected Overrides Function HOOK_Antes_de_hacer_LOAD() As Boolean
    ' If Trim(ObjetoGlobal.UsuarioActual) = "informatica" Then CnTabla01.HayModificacion = False
    ' HOOK_Antes_de_hacer_LOAD = false
    'End Function

    'Protected Overrides Function HOOK_Despues_de_AsignarParametros() As Boolean
    ' Parametros(1) = "4"
    ' HOOK_Despues_de_AsignarParametros = False
    'End Function


    'Protected Overrides Function HOOK_Después_de_CrearArrayDeControles() As Boolean
    ' MsgBox(XTab(0).Name)
    ' HOOK_Después_de_CrearArrayDeControles = False
    'End Function

    'Protected Overrides Function HOOK_Antes_de_VisibilidadTabs() As Boolean
    ' XTab(0).SelectedTab = XTab(0).TabPages(0)
    ' XTab(1).SelectedTab = XTab(1).TabPages(0)
    ' XTab(0).ItemSize = New Size(0, 0)

    ' HOOK_Antes_de_VisibilidadTabs = True 'Los tabs no usados no se eliminarán
    'End Function

    'Protected Overrides Function HOOK_Despues_de_InicializarControles() As Boolean
    ' If Trim(ObjetoGlobal.UsuarioActual) = "informatica" Then
    ' CnEdicion033.MaximaFecha = "1/1/2023"
    ' End If
    ' HOOK_Despues_de_InicializarControles = false
    'End Function

    'Protected Overrides Function HOOK_Despues_de_ConstruirGrids() As Boolean
    ' CnTabla01.Grid.Columns(3).HeaderText = "nuevo titulo cabecera"
    ' HOOK_Despues_de_ConstruirGrids = false
    'End Function

    'Protected Overrides Function HOOK_Antes_de_SeleccionInicialGrid() As Boolean
    ' If Trim(ObjetoGlobal.UsuarioActual) = "informatica" Then
    ' CnTabla01.SeleccionAdicional = "test_serio.codigo_socio > 1 "
    ' End If
    ' HOOK_Antes_de_SeleccionInicialGrid = False
    'End Function

    'Protected Overrides Function HOOK_Despues_de_SeleccionInicialGrid() As Boolean
    ' CnTabla01.SeleccionAdicional = ""

    ' HOOK_Despues_de_SeleccionInicialGrid = False
    'End Function

    'Protected Overrides Function HOOK_Despues_de_AsociarManejadores() As Boolean

    ' RemoveHandler CmdGrid026.Click, AddressOf CmdGrid_Click
    ' AddHandler CmdGrid026.Click, AddressOf GridEspecial
    ' HOOK_Despues_de_AsociarManejadores = False
    'End Function
    '' Private Sub GridEspecial(sender As Object, e As EventArgs)
    '' MsgBox(TxtDatos026.Text)
    '' End Sub

    'Protected Overrides Function HOOK_Despues_de_hacer_LOAD() As Boolean
    ' TabGeneral.SelectedTab = TabGeneral.TabPages(1)
    ' CnTabla_CTAnadir(CnTabla02)
    ' HOOK_Despues_de_hacer_LOAD = False
    'End Function


    '=== Proceso de creación ===

    'Protected Overrides Function HOOK_Antes_de_CTAnadir(CT As CnTabla.CnTabla) As Boolean
    ' If Trim(ObjetoGlobal.UsuarioActual) = "informatica" Then
    ' MsgBox("Tú no puedes crear fichas")
    ' HOOK_Antes_de_CTAnadir = True
    ' Exit Function
    ' End If
    ' HOOK_Antes_de_CTAnadir = fasle
    'End Function


    'Protected Overrides Function HOOK_Antes_de_llamar_a_GridEdicion_en_CTAnadir(CT As CnTabla.CnTabla, GridEdicion As LibreriaGeneral.GridEdicion, ByRef Posicion As Point)
    ' Posicion = New Point(100, 100)

    ' HOOK_Antes_de_llamar_a_GridEdicion_en_CTAnadir = False
    'End Function

    Protected Overrides Function HOOK_Antes_de_Crear_Ficha(CT As CnTabla.CnTabla) As Boolean
        Dim Campos() As String, Valores() As String

        ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio2" Then
        ' ReDim Preserve Campos(1), Valores(1)
        ' Campos(0) = "alfa_no" : Valores(0) = "Alfa_NUEVO"
        ' Campos(1) = "entero_si" : Valores(1) = "246"
        ' HOOK_AsignarValores(CT, Campos, Valores)

        If CT.Tabla = "cabeceras_albaran" Then
            If (Not CnEdicion003 Is Nothing) Then
                CnEdicion003.HayValorFijoCreacion = False
            End If
            ReDim Campos(2), Valores(2)
            Campos(0) = "fecha_albaran"
            Campos(1) = "numero_albaran"
            Campos(2) = "hora_albaran"

            Valores(0) = Format(Now.Date, "dd:MM:yyyy")
            Valores(1) = ""
            Valores(2) = Format(Now.Date, "HH:mm:ss")
            HOOK_AsignarValores(CT, Campos, Valores)

            'If (Not CnEdicion003 Is Nothing) Then
            '    CnEdicion003.ValorFijoCreacion = CnEdicion003.ValorDefecto
            '    CnEdicion003.HayValorFijoCreacion = False
            'End If

            If ChkTicket.Checked Then
                ReDim Campos(0), Valores(0)
                Campos(0) = "codigo_cliente"
                Valores(0) = "1"
                HOOK_AsignarValores(CT, Campos, Valores)
            Else
                If Not (CnEdicion006 Is Nothing) Then
                    TxtDatos004.Focus()
                End If
            End If
            ArticuloControlado = False



        End If
        Return False

    End Function


    ' HOOK_Antes_de_Crear_Ficha = False
    'End Function


    Public Overrides Function HOOK_Antes_de_Crear_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
        Dim Campos() As String, Valores() As String

        If CT.Tabla = "lineas_albaran" Then
            If Trim(CnTabla01.ValorCampo("Situacion")) <> "N" Then
                MsgBox("No se puede añadir líneas a un albarán facturado.")
                Return True
            End If
            If CDate(CnTabla01.ValorCampo("fecha_albaran")) <> Now.Date Then
                MsgBox("No se puede añadir líneas a un albarán de una fecha anterior.")
                Return True
            End If

            Dim CE As CnEdicion.CnEdicion
            CE = CnEdicion044

            If (Not CE Is Nothing) Then
                CE.HayValorFijoCreacion = False
            End If

            Dim NumLin As Integer
            NumLin = ObtenerNumeroLineaAlbaranVenta(CnTabla01.ValorCampo("empresa"), CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"))

            If CE Is Nothing Then
                ReDim Campos(0), Valores(0)
                Campos(0) = "linea_albaran"
                Valores(0) = CStr(NumLin)
                HOOK_AsignarValores(CT, Campos, Valores)
            Else
                ReDim Campos(0), Valores(0)
                Campos(0) = "linea_albaran"
                Valores(0) = CStr(CT.ValorCampo("linea_albaran"))
                HOOK_AsignarValores(CT, Campos, Valores)
                CE.ValorFijoCreacion = CStr(CT.ValorCampo("linea_albaran"))
                CE.HayValorFijoCreacion = True
                CE.TxtDatos.Text = "1"
            End If
            'Set CE = pform.ControlTabla(QueControl).DevolverCedicion("articulo")
            If Not (CE Is Nothing) Then
                CE.Focus()
            End If
            Return False
        End If


    End Function

    Public Function ObtenerNumeroLineaAlbaranVenta(empresa As String, Serie As String, NumeroDocumento As String) As Long
        Dim RsLineas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Contador As Long

        Contador = 0
        RsLineas.Open("SELECT * FROM LINEAS_ALBARAN WHERE EMPRESA = '" + Trim(empresa) + "'  AND SERIE_ALBARAN= '" + Trim(Serie) + "' AND NUMERO_ALBARAN = " + Trim(NumeroDocumento) + " order by 1,2,3,4 desc", ObjetoGlobal.cn)
        If RsLineas.RecordCount > 0 Then
            Contador = RsLineas!linea_albaran
        End If
        RsLineas.Close()

        Return (Contador + 1)
    End Function


    'Protected Overrides Function HOOK_En_el_validating_de_un_campo_en_creacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String) As Boolean
    ' HOOK_En_el_validating_de_un_campo_en_creacion_ficha = False

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio2" And Trim(CE.Campo) = "alfa_si" And Trim(Valor) > "" Then
    ' Valor = Trim(Valor) + " MENOS"
    ' If Microsoft.VisualBasic.Left(Valor, 1) = "1" Then HOOK_En_el_validating_de_un_campo_en_creacion_ficha = True
    ' End If

    'End Function

    'Public Overrides Function HOOK_En_el_validating_de_un_campo_en_creacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String, GE As DataGridView, HH As Hashtable) As Boolean

    ' HOOK_En_el_validating_de_un_campo_en_creacion_grid = False

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio" And Trim(CE.Campo) = "alfa_si" And Trim(Valor) > "" Then
    ' Valor = Trim(Valor) + " MENOS"
    ' If Microsoft.VisualBasic.Left(Valor, 1) = "1" Then HOOK_En_el_validating_de_un_campo_en_creacion_grid = True
    ' End If


    'End Function


    'Protected Overrides Function HOOK_Despues_de_modificar_campo_en_creacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio2" And Trim(CE.Campo) = "alfa_si" Then

    ' ReDim Preserve Campos(2), Valores(2)
    ' Campos(0) = "alfa_no" : Valores(0) = "AB" + Trim(Valor)
    ' Campos(1) = "decimal_si" : Valores(1) = "12.34"
    ' Campos(2) = "decimal_no" : Valores(2) = "945.67"

    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If

    ' HOOK_Despues_de_modificar_campo_en_creacion_ficha = False

    'End Function

    'Public Overrides Function HOOK_Despues_de_modificar_campo_en_creacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CE.Campo) = "alfa_si" Then

    ' ReDim Preserve Campos(2), Valores(2)
    ' Campos(0) = "alfa_no" : Valores(0) = "AB" + Trim(Valor)
    ' Campos(1) = "decimal_si" : Valores(1) = "12.34"
    ' Campos(2) = "decimal_no" : Valores(2) = "945.67"

    ' HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    ' End If

    ' HOOK_Despues_de_modificar_campo_en_creacion_grid = False

    'End Function


    'Protected Overrides Function HOOK_Antes_de_comprobar_valores_creacion_formato_ficha(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio2" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "contador" : Valores(0) = CStr(CInt(Format(Now, "ss")))
    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If


    ' SeIgnoraComprobacion = False
    ' HOOK_Antes_de_comprobar_valores_creacion_formato_ficha = False

    'End Function

    'Public Overrides Function HOOK_Antes_de_comprobar_valores_creacion_formato_grid(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean, GE As DataGridView, HH As Hashtable) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "codigo_variedad" : Valores(0) = "MEL"
    ' HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    ' End If



    ' SeIgnoraComprobacion = False
    ' HOOK_Antes_de_comprobar_valores_creacion_formato_grid = False

    'End Function

    'Public Overrides Function HOOK_Antes_de_InsertarFila_en_creacion(CT As CnTabla.CnTabla) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.AceptandoCreacion And Trim(CT.Tabla) = "test_serio" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "decimal_no" : Valores(0) = "2357.11"
    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If

    ' HOOK_Antes_de_InsertarFila_en_creacion = False

    'End Function

    'Public Overrides Function HOOK_Antes_de_Update_en_creacion(CT As CnTabla.CnTabla, ByRef Rs As cnRecordset.CnRecordset) As Boolean

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.AceptandoCreacion And Trim(CT.Tabla) = "test_serio2" Then
    ' Rs!decimal_no = 235711.13
    ' Rs!alfa_no = Trim("nuevo Alfa_no")
    ' Rs!decimal_si = 1311.75
    ' End If

    ' HOOK_Antes_de_Update_en_creacion = False

    'End Function

    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_creacion_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    ' HOOK_Despues_de_grabar_registro_en_creacion_formato_ficha = False
    ' MsgBox("F")
    'End Function

    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_creacion_formato_grid(CT As CnTabla.CnTabla) As Boolean
    ' HOOK_Despues_de_grabar_registro_en_creacion_formato_grid = False
    ' MsgBox("G")

    'End Function

    'Protected Overrides Function HOOK_Proceso_de_creacion_finalizado_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    ' CnTabla_CTAnadir(CT)
    ' HOOK_Proceso_de_creacion_finalizado_formato_ficha = False
    'End Function

    'Public Overrides Function HOOK_Proceso_de_creacion_finalizado_formato_grid(CT As CnTabla.CnTabla) As Boolean
    ' CnTabla_CTAnadir(CT)
    ' HOOK_Proceso_de_creacion_finalizado_formato_grid = False
    'End Function

    '=== Proceso de modificación ===

    'Protected Overrides Function HOOK_Antes_de_CTModificar(CT As CnTabla.CnTabla) As Boolean
    ' If Trim(ObjetoGlobal.UsuarioActual) = "informatica" Then
    ' MsgBox("Tú no puedes modificar fichas")
    ' HOOK_Antes_de_CTModificar = True
    ' Exit Function
    ' End If
    ' HOOK_Antes_de_CTModificar = False
    'End Function

    'Protected Overrides Function HOOK_Antes_de_llamar_a_GridEdicion_en_CTModificar(CT As CnTabla.CnTabla, GridEdicion As LibreriaGeneral.GridEdicion, ByRef Posicion As Point)
    ' Posicion = New Point(100, 100)
    ' HOOK_Antes_de_llamar_a_GridEdicion_en_CTModificar = False
    'End Function

    'Protected Overrides Function HOOK_Antes_de_Modificar_Ficha(CT As CnTabla.CnTabla) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio2" Then
    ' ReDim Preserve Campos(1), Valores(1)
    ' Campos(0) = "alfa_no" : Valores(0) = "Alfa_NUEVO"
    ' Campos(1) = "entero_si" : Valores(1) = "246"
    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If

    ' HOOK_Antes_de_Modificar_Ficha = False
    'End Function

    'Public Overrides Function HOOK_Antes_de_Modificar_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio" Then
    ' ReDim Preserve Campos(1), Valores(1)
    ' Campos(0) = "alfa_no" : Valores(0) = "Alfa_NUEVO"
    ' Campos(1) = "entero_si" : Valores(1) = "246"
    ' HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    ' End If
    ' HOOK_Antes_de_Modificar_Grid = False
    'End Function

    'Protected Overrides Function HOOK_En_el_validating_de_un_campo_en_modificacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String) As Boolean 'Comentado Probado Ejemplo
    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio2" And Trim(CE.Campo) = "alfa_si" Then
    ' If Microsoft.VisualBasic.Left(Valor, 1) = "Z" Then
    ' MsgBox("No pasa validating")
    ' HOOK_En_el_validating_de_un_campo_en_modificacion_ficha = True
    ' Exit Function
    ' End If
    ' End If
    ' HOOK_En_el_validating_de_un_campo_en_modificacion_ficha = False
    'End Function

    'Public Overrides Function HOOK_En_el_validating_de_un_campo_en_modificacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio" And Trim(CE.Campo) = "alfa_no" Then
    ' If Microsoft.VisualBasic.Left(Valor, 1) = "Z" Then
    ' MsgBox("No pasa validating")
    ' HOOK_En_el_validating_de_un_campo_en_modificacion_grid = True
    ' Exit Function
    ' End If
    ' End If
    ' HOOK_En_el_validating_de_un_campo_en_modificacion_grid = False
    'End Function

    'Protected Overrides Function HOOK_Despues_de_modificar_campo_en_modificacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio2" And Trim(CE.Campo) = "alfa_no" Then
    ' ReDim Preserve Campos(1), Valores(1)
    ' Campos(0) = "alfa_si" : Valores(0) = "Alfa_NUEVO"
    ' Campos(1) = "entero_si" : Valores(1) = "2467"
    ' HOOK_AsignarValores(CT, Campos, Valores)

    ' TxtDatos034.Focus()

    ' End If

    ' HOOK_Despues_de_modificar_campo_en_modificacion_ficha = False
    'End Function

    'Public Overrides Function HOOK_Despues_de_modificar_campo_en_modificacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio" And Trim(CE.Campo) = "alfa_no" Then
    ' 'ReDim Preserve Campos(1), Valores(1)
    ' 'Campos(0) = "alfa_si" : Valores(0) = "Alfa_NUEVO"
    ' 'Campos(1) = "entero_si" : Valores(1) = "2467"
    ' 'HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    ' End If

    ' HOOK_Despues_de_modificar_campo_en_modificacion_grid = False
    'End Function

    'Protected Overrides Function HOOK_Antes_de_comprobar_valores_modificacion_formato_ficha(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio2" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "alfa_no" : Valores(0) = "nuevo:" + CStr(CInt(Format(Now, "ss")))
    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If

    ' SeIgnoraComprobacion = False
    ' HOOK_Antes_de_comprobar_valores_modificacion_formato_ficha = False
    'End Function

    'Public Overrides Function HOOK_Antes_de_comprobar_valores_modificacion_formato_grid(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean, GE As DataGridView, HH As Hashtable) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Modificando And Trim(CT.Tabla) = "test_serio" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "alfa_no" : Valores(0) = "nuevo:" + CStr(CInt(Format(Now, "ss")))
    ' HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    ' End If

    ' SeIgnoraComprobacion = False
    ' HOOK_Antes_de_comprobar_valores_modificacion_formato_grid = False
    'End Function

    'Public Overrides Function HOOK_Antes_de_ModificarFila_en_modificacion(CT As CnTabla.CnTabla) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.AceptandoModificacion And Trim(CT.Tabla) = "test_serio" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "decimal_no" : Valores(0) = "235711.13"
    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If

    ' HOOK_Antes_de_ModificarFila_en_modificacion = False

    'End Function

    'Public Overrides Function HOOK_Antes_de_Update_en_modificacion(CT As CnTabla.CnTabla, ByRef Rs As cnRecordset.CnRecordset) As Boolean

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.AceptandoModificacion And Trim(CT.Tabla) = "test_serio" Then
    ' Rs!decimal_no = 235711.13
    ' Rs!alfa_no = Trim("Alfa_no RS")
    ' Rs!decimal_si = 2222.22
    ' End If

    ' HOOK_Antes_de_Update_en_modificacion = False

    'End Function

    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_modificacion_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    ' HOOK_Despues_de_grabar_registro_en_modificacion_formato_ficha = False
    ' MsgBox("F")
    'End Function

    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_modificacion_formato_grid(CT As CnTabla.CnTabla) As Boolean
    ' HOOK_Despues_de_grabar_registro_en_modificacion_formato_grid = False
    ' MsgBox("G")

    'End Function

    'Protected Overrides Function HOOK_Proceso_de_modificacion_finalizado_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    ' MsgBox("F")
    ' CT.Siguiente(True)
    ' HOOK_Proceso_de_modificacion_finalizado_formato_ficha = False
    'End Function

    'Public Overrides Function HOOK_Proceso_de_modificacion_finalizado_formato_grid(CT As CnTabla.CnTabla) As Boolean
    ' MsgBox("G")
    ' CT.Siguiente(True)
    ' HOOK_Proceso_de_modificacion_finalizado_formato_grid = False
    'End Function

    '=== Proceso de selección ===

    'Public Overrides Function HOOK_Antes_de_establecer_seleccion(CT As CnTabla.CnTabla, ByRef SQLGridSinWhere As String, ByRef SQLWhere As String, ByRef SqlOrder As String) as boolean
    ' If FlagEstoyEnSeleccionInicial = False Then
    ' MsgBox(SQLGridSinWhere)
    ' SQLWhere = Trim(SQLWhere) + " AND TEST_SERIO.CODIGO_VARIEDAD = 'SAN'"
    ' MsgBox(SQLWhere)
    ' MsgBox(SqlOrder)
    ' End If

    ' HOOK_Antes_de_establecer_seleccion = False

    'End Function

    'Public Overrides Function HOOK_Despues_de_seleccionar(CT As CnTabla.CnTabla) as boolean
    ' 'La rutina se ejecuta cuando se han seleccionado registros con éxito, la situación del formulario ha vuelto a Inactivo y se ha ejecutado CT.Primero(True) para mostrar el primer registro

    ' HOOK_Despues_de_seleccionar = False
    ' CT.Ultimo(True)

    'End Function

    '=== Proceso de borrado de registro ===

    'Protected Overrides Function HOOK_Antes_de_eliminar_registro(CT As CnTabla.CnTabla) As Boolean 'Comentado
    ' If Trim(CT.Tabla) = "test_serio" Then
    ' MsgBox("No se puede borrar")
    ' HOOK_Antes_de_eliminar_registro = True
    ' Exit Function
    ' End If

    ' HOOK_Antes_de_eliminar_registro = False


    'End Function

    'Protected Overrides Function HOOK_Despues_de_eliminar_registro(CT) As Boolean 'Comentado
    ' 'La rutina se ejecuta cuando se ha eliminado un registro con éxito y la situación del formulario ha vuelto a Inactivo

    ' CT.Siguiente(True)
    ' HOOK_Despues_de_eliminar_registro = False

    'End Function

    '==== Cambio de registro ====

    'Public Overrides Function HOOK_Cambio_de_registro(CT As CnTabla.CnTabla) As Boolean 'Comentado Probado Ejemplo
    ' Me.Text = "Cambio en tabla " + Trim(CT.Tabla) + " a registro " + CStr(CT.RegistroActual)

    ' HOOK_Cambio_de_registro = False
    'End Function



    ''=== Cambios de situación formulario
    'Protected Overrides Function HOOK_Antes_de_CambiarSituacionFormulario_Descendiente(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, ByRef SeIgnoraCambio As Boolean) As Boolean
    ' HOOK_Antes_de_CambiarSituacionFormulario_Descendiente = False
    ' SeIgnoraCambio = False

    ' MsgBox(CT.Tabla + " " + Estado.ToString)
    'End Function

    ''Protected Overrides Function HOOK_Despues_de_CambiarSituacionFormulario_Descendiente(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, Retorno As String) As Boolean

    'Protected Overrides Function HOOK_Antes_de_CambiarSituacionFormulario_OtrasTablas(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, ByRef SeIgnoraCambio As Boolean) As Boolean
    ' HOOK_Antes_de_CambiarSituacionFormulario_OtrasTablas = False

    ' MsgBox(CT.Tabla + " " + Estado.ToString)
    'End Function
    ''Protected Overrides Function HOOK_Despues_de_CambiarSituacionFormulario_OtrasTablas(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, Retorno As String) As Boolean

    'Protected Overrides Function HOOK_Antes_de_CambiarSituacionFormulario_TablaEnEdicion(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, ByRef SeIgnoraCambio As Boolean) As Boolean
    ' Dim Cadena As String

    ' HOOK_Antes_de_CambiarSituacionFormulario_TablaEnEdicion = False
    ' SeIgnoraCambio = False

    ' If Trim(CT.Tabla) = "test_serio2" And Estado = CnTabla.CnTabla.EstadoCnTabla.PasandoACrear Then
    ' Cadena = Trim("test_serio2.contador")
    ' CT.XCnEdicion(Cadena).HayValorFijoCreacion = True
    ' CT.XCnEdicion(Cadena).ValorFijoCreacion = CStr(1000 + CInt(Format(Now, "ss")))
    ' End If
    'End Function


    ''Protected Overrides Function HOOK_Despues_de_CambiarSituacionFormulario_TablaEnEdicion(CT As CnTabla.CnTabla, Estado As CnTabla.CnTabla.EstadoCnTabla, Retorno As String) As Boolean
    'MsgBox("Nueva ficha en " + CT.Tabla)

    'HOOK_Antes_de_CTAnadir = False

    'For i = 1 To UltimoControlCnEdicion
    ' CE = XCNE(i)
    ' If Not (CE Is Nothing) Then
    ' If Trim(CE.Tabla) = Trim(CT.Tabla) Then
    ' MsgBox(CE.Campo + " " + Trim(CT.RsCreacion(CE.Campo)))
    ' End If
    ' End If
    'Next


    'ReDim Campos(2), Valores(2)
    'Campos(0) = "alfa_no" : Valores(0) = "A123B"
    'Campos(1) = "codigo_socio" : Valores(1) = "12"
    'Campos(2) = "decimal_no" : Valores(2) = "45.67"
    'HOOK_AsignarValores(CT, Campos, Valores, GE, HH)

    'Esto está bien si se trata de un valor por defecto, modificable por el usuario
    'Esto NO es una buena idea si es un valor que no debe ser modificado por el usaurio: mejor en el cambio de situación a Pasando a seleccionar (valor fijo creación)
    'If Trim(CT.Tabla) = "test_serio2" Then
    ' ReDim Campos(0), Valores(0)
    ' Campos(0) = "contador" : Valores(0) = CStr(CInt(Format(Now, "ss")))
    ' Cadena = Trim("test_serio2.contador")
    ' CT.XCnEdicion(Cadena).TxtDatos.ReadOnly = True

    ' HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    'End If


    Private Function GrabaFacturaContado() As Boolean 'NOSTD
        Dim RsClientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsProvincias As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPaises As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsArticulos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCamposPartes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rsvariedades As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturaVTA_C As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturaVTA_L As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturaVTA_C_TOT As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCabecera As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction
        Dim SQL As String
        Dim i As Long
        Dim k As Integer
        Dim l As Integer
        Dim jj As Long
        Dim CodigoCliente As Long
        Dim Domicilio As String
        Dim Provincia As String
        Dim Linea As Long
        Dim DescripcionCampo As String
        Dim Totales(50, 10) As Double
        Dim TipoIva As Integer
        Dim Cadena As String
        Dim Precio As Double
        Dim Importe As Double
        Dim QueTipo As Integer
        Dim DescripcionParte As String
        Dim Clave As String
        Dim Dato As String
        Dim NombreFichero As String
        Dim CadenaSerie As String
        Dim Contador As Long
        Dim RegistroActual As Long
        Dim Campos() As String
        Dim CamposTabla() As String
        Dim Valores() As String

        GrabaFacturaContado = False
        CadenaSerie = "facturacion_suministros_pto" + Trim(CnTabla01.ValorCampo("punto_venta"))

        If Date.Now = CDate(cnTabla01.valorCampo("fecha_albaran")) Then
            FechaFacturaAImprimir = Date.Now
        Else
            Dim oFrm As fechaFactura = New fechafactura
            If oFrm.showDialog() <> DialogResult.OK Then
                Return False
            End If
            FechaFacturaAImprimir = oFrm.Fecha_factura
        End If
        If ObtenerSerie(ObjetoGlobal.EmpresaActual, CadenaSerie, "T", FechaFacturaAImprimir, SerieFacturaAImprimir, TipoVentaFacturaAImprimir, TipoDesgloseFacturaAImprimir, TipoDocumentoFacturaAImprimir) = False Then
            MsgBox("No se ha determinado serie de facturación para este proceso")
            Return False
        End If

        'RsCabecera = CnTabla01.d
        CodigoCliente = CnTabla01.ValorCampo("codigo_cliente")
        RsClientes.Open("SELECT * FROM CLIENTES_COOP WHERE CODIGO_CLIENTE = " + CStr(CodigoCliente), ObjetoGlobal.cn)
        If RsClientes.RecordCount = 0 Then
            MsgBox("No existe ficha de cliente. Se cancela el proceso.")
            Return False
        End If
        'For k = 0 To 50: For l = 0 To 10: Totales(k, l) = 0: Next: Next

        NumeroFacturaAImprimir = ObtenerNumeroFactura(ObjetoGlobal.EmpresaActual, SerieFacturaAImprimir, True)
        If NumeroFacturaAImprimir = -1 Then
            Return False
        End If

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            SQL = "SELECT * FROM FACTURA_VTA_C WHERE 1=0"
            RsFacturaVTA_C.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            RsFacturaVTA_C.AddNew()
            RsFacturaVTA_C!empresa = Trim(ObjetoGlobal.EmpresaActual)
            RsFacturaVTA_C!serie_factura_vta = Trim(SerieFacturaAImprimir)
            RsFacturaVTA_C!numero_factura = NumeroFacturaAImprimir
            RsFacturaVTA_C!fecha_factura = CDate(FechaFacturaAImprimir)
            RsFacturaVTA_C!tipo_documento = Trim(TipoDocumentoFacturaAImprimir)
            RsFacturaVTA_C!codigo_documento = CStr(NumeroFacturaAImprimir)
            RsFacturaVTA_C!clave_docum = CStr(NumeroFacturaAImprimir)
            RsFacturaVTA_C!tipo_venta = Trim(TipoVentaFacturaAImprimir)
            RsFacturaVTA_C!factura_contado = "S"
            RsFacturaVTA_C!tipo_desglose = Trim(TipoDesgloseFacturaAImprimir)
            RsFacturaVTA_C!codigo_cliente = CodigoCliente
            RsFacturaVTA_C!nombre_cliente = "" & Trim(CnTabla01.ValorCampo("nombre_cliente"))
            RsFacturaVTA_C!razon_social = "" & Trim(CnTabla01.ValorCampo("razon_social"))
            RsFacturaVTA_C!Domicilio = "" & Trim(CnTabla01.ValorCampo("Domicilio"))
            RsFacturaVTA_C!domicilio_2 = "" & Trim(CnTabla01.ValorCampo("domicilio_2"))
            RsFacturaVTA_C!codigo_postal = "" & Trim(CnTabla01.ValorCampo("cpostal_cliente"))
            RsFacturaVTA_C!poblacion_cliente = "" & Trim(CnTabla01.ValorCampo("poblacion_cliente"))
            RsFacturaVTA_C!poblacion2_cliente = "" & Trim(CnTabla01.ValorCampo("poblacion2_cliente"))
            RsFacturaVTA_C!provincia_cliente = "" & Trim(CnTabla01.ValorCampo("provincia_cliente"))
            RsFacturaVTA_C!CIF = "" & Trim(CnTabla01.ValorCampo("cif_cliente"))
            RsFacturaVTA_C!f_pago = "" & Trim(CnTabla01.ValorCampo("forma_pago"))
            RsFacturaVTA_C!dia1_pago = CnTabla01.ValorCampo("dia1_pago")
            RsFacturaVTA_C!dia2_pago = CnTabla01.ValorCampo("dia2_pago")
            RsFacturaVTA_C!dia3_pago = CnTabla01.ValorCampo("dia3_pago")
            RsFacturaVTA_C!dias_retraso_vto = CnTabla01.ValorCampo("dias_retraso_vto")
            If IsDate(CnTabla01.ValorCampo("no_girar_desde")) Then RsFacturaVTA_C!no_girar_desde = CnTabla01.ValorCampo("no_girar_desde")
            If IsDate(CnTabla01.ValorCampo("no_girar_hasta")) Then RsFacturaVTA_C!no_girar_hasta = CnTabla01.ValorCampo("no_girar_hasta")
            RsFacturaVTA_C!no_girar_ejerc = "" & Trim(CnTabla01.ValorCampo("no_girar_ejerc"))
            RsFacturaVTA_C!domicilia_sn = "N"
            RsFacturaVTA_C!acepta_sn = "N"
            RsFacturaVTA_C!divisa_factura = "EUR"
            RsFacturaVTA_C!tabla_iva_clientes = "" & Trim(CnTabla01.ValorCampo("tabla_iva"))
            RsFacturaVTA_C!recargo_sn = "N"
            RsFacturaVTA_C!enlazado_contab = "N"
            RsFacturaVTA_C!enlazado_comis = "N"
            RsFacturaVTA_C!Situacion = "N"
            RsFacturaVTA_C.Update()
            RsFacturaVTA_C.Close()
            Contador = 0

            SQL = "SELECT * FROM FACTURA_VTA_C_TOT WHERE 1=0"
            RsFacturaVTA_C_TOT.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            RsFacturaVTA_C_TOT.AddNew()
            RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
            RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
            RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
            RsFacturaVTA_C_TOT!tipo_linea = "B"
            RsFacturaVTA_C_TOT!numero_orden = Contador
            RsFacturaVTA_C_TOT!Descripcion = "Bruto"
            RsFacturaVTA_C_TOT!importe_abono = 0
            RsFacturaVTA_C_TOT!importe_cargo = Math.Round(CDbl(CnTabla01.ValorCampo("total_neto")), 2)
            RsFacturaVTA_C_TOT.Update()

            If (CnTabla01.ValorCampo("base_iva_1") <> 0 Or CnTabla01.ValorCampo("importe_iva_1") <> 0) And CnTabla01.ValorCampo("porcentaje_iva_1") >= 0 Then
                Contador = Contador + 10
                RsFacturaVTA_C_TOT.AddNew()
                RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
                RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_C_TOT!tipo_linea = "I"
                RsFacturaVTA_C_TOT!numero_orden = Contador
                RsFacturaVTA_C_TOT!Descripcion = "Iva al " + Trim(CnTabla01.ValorCampo("porcentaje_iva_1")) + "%"
                RsFacturaVTA_C_TOT!cod_tipo_iva = CDbl(CnTabla01.ValorCampo("porcentaje_iva_1"))
                RsFacturaVTA_C_TOT!Base = CDbl(CnTabla01.ValorCampo("base_iva_1"))
                RsFacturaVTA_C_TOT!Porcentaje = CDbl(CnTabla01.ValorCampo("porcentaje_iva_1"))
                RsFacturaVTA_C_TOT!importe_abono = 0
                RsFacturaVTA_C_TOT!importe_cargo = CDbl(CnTabla01.ValorCampo("importe_iva_1"))
                RsFacturaVTA_C_TOT.Update()
            End If
            If (CnTabla01.ValorCampo("base_iva_2") <> 0 Or CnTabla01.ValorCampo("importe_iva_2") <> 0) And CnTabla01.ValorCampo("porcentaje_iva_2") >= 0 Then
                Contador = Contador + 10
                RsFacturaVTA_C_TOT.AddNew()
                RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
                RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_C_TOT!tipo_linea = "I"
                RsFacturaVTA_C_TOT!numero_orden = Contador
                RsFacturaVTA_C_TOT!Descripcion = "Iva al " + Trim(CnTabla01.ValorCampo("porcentaje_iva_2")) + "%"
                RsFacturaVTA_C_TOT!cod_tipo_iva = CDbl(CnTabla01.ValorCampo("porcentaje_iva_2"))
                RsFacturaVTA_C_TOT!Base = CDbl(CnTabla01.ValorCampo("base_iva_2"))
                RsFacturaVTA_C_TOT!Porcentaje = CDbl(CnTabla01.ValorCampo("porcentaje_iva_2"))
                RsFacturaVTA_C_TOT!importe_abono = 0
                RsFacturaVTA_C_TOT!importe_cargo = CDbl(CnTabla01.ValorCampo("importe_iva_2"))
                RsFacturaVTA_C_TOT.Update()
            End If
            If (CnTabla01.ValorCampo("base_iva_3") <> 0 Or CnTabla01.ValorCampo("importe_iva_3") <> 0) And CnTabla01.ValorCampo("porcentaje_iva_3") >= 0 Then
                Contador = Contador + 10
                RsFacturaVTA_C_TOT.AddNew()
                RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
                RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_C_TOT!tipo_linea = "I"
                RsFacturaVTA_C_TOT!numero_orden = Contador
                RsFacturaVTA_C_TOT!Descripcion = "Iva al " + Trim(CnTabla01.ValorCampo("porcentaje_iva_3")) + "%"
                RsFacturaVTA_C_TOT!cod_tipo_iva = CDbl(CnTabla01.ValorCampo("porcentaje_iva_3"))
                RsFacturaVTA_C_TOT!Base = CDbl(CnTabla01.ValorCampo("base_iva_3"))
                RsFacturaVTA_C_TOT!Porcentaje = CDbl(CnTabla01.ValorCampo("porcentaje_iva_3"))
                RsFacturaVTA_C_TOT!importe_abono = 0
                RsFacturaVTA_C_TOT!importe_cargo = CDbl(CnTabla01.ValorCampo("importe_iva_3"))
                RsFacturaVTA_C_TOT.Update()
            End If
            Contador = Contador + 10
            RsFacturaVTA_C_TOT.AddNew()
            RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
            RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
            RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
            RsFacturaVTA_C_TOT!tipo_linea = "T"
            RsFacturaVTA_C_TOT!numero_orden = Contador
            RsFacturaVTA_C_TOT!Descripcion = "Total factura"
            RsFacturaVTA_C_TOT!cod_tipo_iva = 0
            RsFacturaVTA_C_TOT!Base = 0
            RsFacturaVTA_C_TOT!Porcentaje = 0
            RsFacturaVTA_C_TOT!importe_abono = 0
            RsFacturaVTA_C_TOT!importe_cargo = CDbl(CnTabla01.ValorCampo("total_albaran"))
            RsFacturaVTA_C_TOT.Update()
            SQL = "SELECT * FROM FACTURA_VTA_L WHERE 1=0"

            RsFacturaVTA_L.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            Contador = 0
            'RegistroActual = CTabla(1).ValorCampo(1).AbsolutePosition
            'RegistroActual = CnTabla02.RegistroActual
            'Dim RsGrid As cnRecordset.CnRecordset
            'RsGrid = CnTabla02.DevuelveRecorsetGrid()

            Dim RsGrid As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            'Dim Contador As Integer
            'RsGrid = CnTabla02.DevuelveRecorsetGrid()
            SQL = "SELECT * FROM lineas_albaran WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND serie_albaran = '" & CnTabla01.ValorCampo("serie_albaran") & "' And numero_albaran = " & CnTabla01.ValorCampo("numero_albaran") & " ORDER BY linea_albaran "
            RsGrid.Open(SQL, ObjetoGlobal.cn)

            For i = 1 To RsGrid.RecordCount
                RsGrid.AbsolutePosition = i
                Contador = Contador + 1
                RsFacturaVTA_L.AddNew()
                RsFacturaVTA_L!empresa = Trim(RsGrid!empresa)
                RsFacturaVTA_L!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_L!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_L!linea_factura = Contador
                RsFacturaVTA_L!tipo_linea_fra = "T"
                RsFacturaVTA_L!texto_1 = Trim(RsGrid!articulo)
                RsFacturaVTA_L!texto_2 = Trim(RsGrid!Descripcion)
                RsFacturaVTA_L!texto_3 = ""
                RsFacturaVTA_L!cod_tipo_iva = Trim(RsGrid!tipo_iva)
                RsFacturaVTA_L!unidades_l = CDbl(RsGrid!Unidades)
                RsFacturaVTA_L!precio_l = CDbl(RsGrid!Precio)
                RsFacturaVTA_L!importe_l = CDbl(RsGrid!Importe_neto)
                RsFacturaVTA_L!serie_albaran_vta = Trim(RsGrid!serie_albaran)
                RsFacturaVTA_L!numero_albaran_vta = (RsGrid!numero_albaran)
                RsFacturaVTA_L!linea_salida = CDbl(RsGrid!linea_albaran)
                RsFacturaVTA_L.Update()
            Next
            RsFacturaVTA_L.Close()
            RsGrid.AbsolutePosition = RegistroActual

            RegistroActual = CnTabla02.RegistroActual
            ReDim Campos(4), Valores(4), CamposTabla(4)
            Campos(0) = "serie_factura_vta"
            Campos(1) = "numero_factura"
            Campos(2) = "fecha_factura"
            Campos(3) = "albaran_contado"
            Campos(4) = "situacion"
            Valores(0) = Trim(SerieFacturaAImprimir)
            Valores(1) = Trim(NumeroFacturaAImprimir)
            Valores(2) = CStr(FechaFacturaAImprimir)
            Valores(3) = "S"
            Valores(4) = "F"
            HOOK_AsignarValores(CnTabla01, Campos, Valores)

            'CnTabla01.EsteRegistro(RegistroActual, True)
            RsGrid.Close()
            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return True
        Catch ex As Exception
            MsgBox("Se ha producido un error al grabar la factura." + vbCrLf + "Se ha obtenido el siguiente mensaje: " + Trim(Err.Description))
            trans.Rollback()
            Return False
        End Try

HacerRollBack:

        MsgBox("Se ha producido un error al grabar la factura." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + Trim(Err.Description))
        trans.Rollback()
        Return False

    End Function


    Public Function ObtenerSerie(ByVal empresa As String, ByVal proceso As String, ByVal Ejercicio As String, ByVal Fecha As Date, ByRef Serie As String, ByRef tipo1 As String, ByRef tipo2 As String, ByRef tipo3 As String) As Boolean
        Dim RsTablaSeries As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim FechasOK As Boolean

        ''ObtenerSerie = False
        Serie = ""
        tipo1 = ""
        tipo2 = ""
        RsTablaSeries.Open("SELECT * FROM TABLA_SERIES WHERE EMPRESA = '" + Trim(empresa) + "' AND PROCESO = '" + Trim(proceso) + "' ORDER BY 1,2,3", ObjetoGlobal.cn)
        If RsTablaSeries.RecordCount > 0 Then
            RsTablaSeries.MoveFirst()

            Do While RsTablaSeries.EOF = False
                If Trim(RsTablaSeries!Ejercicio) = "T" Or Trim(RsTablaSeries!Ejercicio) = Trim(Ejercicio) Then
                    If IsDBNull(RsTablaSeries!desde_fecha) Then
                        FechasOK = True
                    Else
                        If IsDate(RsTablaSeries!desde_fecha) = False Then
                            FechasOK = True
                        ElseIf IsDate(Fecha) = False Then
                            FechasOK = False
                        ElseIf CDate(RsTablaSeries!desde_fecha) <= CDate(Fecha) Then
                            FechasOK = True
                        Else
                            FechasOK = False
                        End If
                    End If
                    If FechasOK = True Then
                        If IsDBNull(RsTablaSeries!hasta_fecha) Then
                            FechasOK = True
                        Else
                            If IsDate(RsTablaSeries!hasta_fecha) = False Then
                                FechasOK = True
                            ElseIf IsDate(Fecha) = False Then
                                FechasOK = False
                            ElseIf CDate(RsTablaSeries!hasta_fecha) >= CDate(Fecha) Then
                                FechasOK = True
                            Else
                                FechasOK = False
                            End If
                        End If
                    End If
                    If FechasOK = True Then
                        'ObtenerSerie = True
                        If Not IsDBNull(RsTablaSeries!Serie) Then Serie = Trim(RsTablaSeries!Serie)
                        If Not IsDBNull(RsTablaSeries!tipo1) Then tipo1 = Trim(RsTablaSeries!tipo1)
                        If Not IsDBNull(RsTablaSeries!tipo2) Then tipo2 = Trim(RsTablaSeries!tipo2)
                        If Not IsDBNull(RsTablaSeries!tipo3) Then tipo3 = Trim(RsTablaSeries!tipo3)
                        Return True
                    End If
                End If
                RsTablaSeries.MoveNext()
            Loop
        End If
        RsTablaSeries.Close()
        Return False
    End Function

    Private Function GrabaFacturaCredito() As Boolean 'NOSTD
        Dim RsClientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsProvincias As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPaises As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsArticulos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Long
        Dim k As Integer
        Dim l As Integer
        Dim jj As Long
        Dim CodigoCliente As Long
        Dim Domicilio As String
        Dim Provincia As String
        Dim Linea As Long
        Dim RsCamposPartes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionCampo As String
        Dim Rsvariedades As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Totales(50, 10) As Double
        Dim TipoIva As Integer
        Dim Cadena As String
        Dim Precio As Double
        Dim Importe As Double
        Dim QueTipo As Integer
        Dim DescripcionParte As String
        Dim Clave As String
        Dim Dato As String
        Dim NombreFichero As String
        Dim CadenaSerie As String
        Dim RsFacturaVTA_C As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturaVTA_L As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturaVTA_C_TOT As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        'Dim RsCabecera As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim Contador As Long
        Dim RegistroActual As Long
        Dim Campos() As String
        Dim CamposTabla() As String
        Dim Valores() As String
        Dim oFrm As FechaFactura = New FechaFactura
        Dim trans As SqlClient.SqlTransaction

        GrabaFacturaCredito = False
        CadenaSerie = "facturacion_diferida_suministros" '+ Trim(CTabla(0).Rs(1)!punto_venta)

        If Now.Date = CDate(CnTabla01.ValorCampo("fecha_albaran")) Then
            FechaFacturaAImprimir = Now.Date
        Else
            If oFrm.DialogResult <> DialogResult.OK Then
                Return False
            End If
            FechaFacturaAImprimir = oFrm.Fecha_factura
        End If

        If ObtenerSerie(ObjetoGlobal.EmpresaActual, CadenaSerie, "T", FechaFacturaAImprimir, SerieFacturaAImprimir, TipoVentaFacturaAImprimir, TipoDesgloseFacturaAImprimir, TipoDocumentoFacturaAImprimir) = False Then
            MsgBox("No se ha determinado serie de facturación para este proceso")
            Return False
        End If

        'Set RsCabecera = cntabla01.valorCampo("'CTabla(0).Rs(1)
        CodigoCliente = CnTabla01.ValorCampo("codigo_cliente")
        RsClientes.Open("SELECT * FROM CLIENTES_COOP WHERE CODIGO_CLIENTE = " + CStr(CodigoCliente), ObjetoGlobal.cn, True)
        If RsClientes.RecordCount = 0 Then
            MsgBox("No existe ficha de cliente. Se cancela el proceso.")
            Return False
        End If

        ' Veremos si es interna
        If Trim(CnTabla01.ValorCampo("cif_cliente")) = "F46026068" Then
            SerieFacturaAImprimir = "FI"
        End If

        NumeroFacturaAImprimir = ObtenerNumeroFactura(ObjetoGlobal.EmpresaActual, SerieFacturaAImprimir, True)
        If NumeroFacturaAImprimir = -1 Then Exit Function
        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            Sql = "SELECT * FROM FACTURA_VTA_C WHERE 1=0"
            RsFacturaVTA_C.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans)
            RsFacturaVTA_C.AddNew()
            RsFacturaVTA_C!empresa = Trim(ObjetoGlobal.EmpresaActual)
            RsFacturaVTA_C!serie_factura_vta = Trim(SerieFacturaAImprimir)
            RsFacturaVTA_C!numero_factura = NumeroFacturaAImprimir
            RsFacturaVTA_C!fecha_factura = CDate(FechaFacturaAImprimir)
            RsFacturaVTA_C!tipo_documento = Trim(TipoDocumentoFacturaAImprimir)
            RsFacturaVTA_C!codigo_documento = CStr(NumeroFacturaAImprimir)
            RsFacturaVTA_C!clave_docum = CStr(NumeroFacturaAImprimir)
            RsFacturaVTA_C!tipo_venta = Trim(TipoVentaFacturaAImprimir)
            RsFacturaVTA_C!factura_contado = "N"
            RsFacturaVTA_C!tipo_desglose = Trim(TipoDesgloseFacturaAImprimir)
            RsFacturaVTA_C!codigo_cliente = CodigoCliente
            RsFacturaVTA_C!nombre_cliente = "" & Trim(CnTabla01.ValorCampo("nombre_cliente"))
            RsFacturaVTA_C!razon_social = "" & Trim(CnTabla01.ValorCampo("razon_social"))
            RsFacturaVTA_C!Domicilio = "" & Trim(cntabla01.valorCampo("Domicilio"))
            RsFacturaVTA_C!domicilio_2 = "" & Trim(cntabla01.valorCampo("domicilio_2"))
            RsFacturaVTA_C!codigo_postal = "" & Trim(cntabla01.valorCampo("cpostal_cliente"))
            RsFacturaVTA_C!poblacion_cliente = "" & Trim(cntabla01.valorCampo("poblacion_cliente"))
            RsFacturaVTA_C!poblacion2_cliente = "" & Trim(cntabla01.valorCampo("poblacion2_cliente"))
            RsFacturaVTA_C!provincia_cliente = "" & Trim(cntabla01.valorCampo("provincia_cliente"))
            RsFacturaVTA_C!CIF = "" & Trim(cntabla01.valorCampo("cif_cliente"))
            RsFacturaVTA_C!f_pago = "" & Trim(cntabla01.valorCampo("forma_pago"))
            RsFacturaVTA_C!dia1_pago = cntabla01.valorCampo("dia1_pago")
            RsFacturaVTA_C!dia2_pago = cntabla01.valorCampo("dia2_pago")
            RsFacturaVTA_C!dia3_pago = cntabla01.valorCampo("dia3_pago")
            RsFacturaVTA_C!dias_retraso_vto = cntabla01.valorCampo("dias_retraso_vto")
            If IsDate(cntabla01.valorCampo("no_girar_desde")) Then
                RsFacturaVTA_C!no_girar_desde = cntabla01.valorCampo("no_girar_desde")
            End If

            If IsDate(cntabla01.valorCampo("no_girar_hasta")) Then RsFacturaVTA_C!no_girar_hasta = cntabla01.valorCampo("no_girar_hasta")
            RsFacturaVTA_C!no_girar_ejerc = "" & Trim(cntabla01.valorCampo("no_girar_ejerc"))
            RsFacturaVTA_C!domicilia_sn = "N"
            RsFacturaVTA_C!acepta_sn = "N"
            RsFacturaVTA_C!divisa_factura = "EUR"
            RsFacturaVTA_C!tabla_iva_clientes = "" & Trim(cntabla01.valorCampo("tabla_iva"))
            RsFacturaVTA_C!recargo_sn = "N"
            RsFacturaVTA_C!enlazado_contab = "N"
            RsFacturaVTA_C!enlazado_comis = "N"
            RsFacturaVTA_C!Situacion = "N"
            RsFacturaVTA_C.Update
            RsFacturaVTA_C.Close
            Contador = 0
            Sql = "SELECT * FROM FACTURA_VTA_C_TOT WHERE 1=0"
            RsFacturaVTA_C_TOT.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans)
            RsFacturaVTA_C_TOT.AddNew
            RsFacturaVTA_C_TOT!empresa = Trim(cntabla01.valorCampo("empresa"))
            RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
            RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
            RsFacturaVTA_C_TOT!tipo_linea = "B"
            RsFacturaVTA_C_TOT!numero_orden = Contador
            RsFacturaVTA_C_TOT!Descripcion = "Bruto"
            RsFacturaVTA_C_TOT!importe_abono = 0
            RsFacturaVTA_C_TOT!importe_cargo = Math.Round(CDbl(CnTabla01.ValorCampo("total_neto")), 2)
            RsFacturaVTA_C_TOT.Update
            If (cntabla01.valorCampo("base_iva_1") <> 0 Or cntabla01.valorCampo("importe_iva_1 <> 0")) And cntabla01.valorCampo("porcentaje_iva_1") >= 0 Then
                Contador = Contador + 10
                RsFacturaVTA_C_TOT.AddNew
                RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
                RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_C_TOT!tipo_linea = "I"
                RsFacturaVTA_C_TOT!numero_orden = Contador
                RsFacturaVTA_C_TOT!Descripcion = "Iva al " + Trim(cntabla01.valorCampo("porcentaje_iva_1")) + "%"
                RsFacturaVTA_C_TOT!cod_tipo_iva = CDbl(CnTabla01.ValorCampo("porcentaje_iva_1"))
                RsFacturaVTA_C_TOT!Base = CDbl(cntabla01.valorCampo("base_iva_1"))
                RsFacturaVTA_C_TOT!Porcentaje = CDbl(CnTabla01.ValorCampo("porcentaje_iva_1"))
                RsFacturaVTA_C_TOT!importe_abono = 0
                RsFacturaVTA_C_TOT!importe_cargo = CDbl(cntabla01.valorCampo("importe_iva_1"))
                RsFacturaVTA_C_TOT.Update
            End If
            If (CnTabla01.ValorCampo("base_iva_2") <> 0 Or CnTabla01.ValorCampo("importe_iva_2") <> 0) And CnTabla01.ValorCampo("porcentaje_iva_2") >= 0 Then
                Contador = Contador + 10
                RsFacturaVTA_C_TOT.AddNew()
                RsFacturaVTA_C_TOT!empresa = Trim(cntabla01.valorCampo("empresa"))
                RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_C_TOT!tipo_linea = "I"
                RsFacturaVTA_C_TOT!numero_orden = Contador
                RsFacturaVTA_C_TOT!Descripcion = "Iva al " + Trim(cntabla01.valorCampo("porcentaje_iva_2")) + "%"
                RsFacturaVTA_C_TOT!cod_tipo_iva = CDbl(CnTabla01.ValorCampo("porcentaje_iva_2"))
                RsFacturaVTA_C_TOT!Base = CDbl(CnTabla01.ValorCampo("base_iva_2"))
                RsFacturaVTA_C_TOT!Porcentaje = CDbl(CnTabla01.ValorCampo("porcentaje_iva_2"))
                RsFacturaVTA_C_TOT!importe_abono = 0
                RsFacturaVTA_C_TOT!importe_cargo = CDbl(CnTabla01.ValorCampo("importe_iva_2"))
                RsFacturaVTA_C_TOT.Update()
            End If
            If (cntabla01.valorCampo("base_iva_3") <> 0 Or cntabla01.valorCampo("importe_iva_3") <> 0) And cntabla01.valorCampo("porcentaje_iva_3") >= 0 Then
                Contador = Contador + 10
                RsFacturaVTA_C_TOT.AddNew
                RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
                RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_C_TOT!tipo_linea = "I"
                RsFacturaVTA_C_TOT!numero_orden = Contador
                RsFacturaVTA_C_TOT!Descripcion = "Iva al " + Trim(cntabla01.valorCampo("porcentaje_iva_3")) + "%"
                RsFacturaVTA_C_TOT!cod_tipo_iva = CDbl(CnTabla01.ValorCampo("porcentaje_iva_3"))
                RsFacturaVTA_C_TOT!Base = CDbl(cntabla01.valorCampo("base_iva_3"))
                RsFacturaVTA_C_TOT!Porcentaje = CDbl(CnTabla01.ValorCampo("porcentaje_iva_3"))
                RsFacturaVTA_C_TOT!importe_abono = 0
                RsFacturaVTA_C_TOT!importe_cargo = CDbl(cntabla01.valorCampo("importe_iva_3"))
                RsFacturaVTA_C_TOT.Update
            End If
            Contador = Contador + 10
            RsFacturaVTA_C_TOT.AddNew
            RsFacturaVTA_C_TOT!empresa = Trim(CnTabla01.ValorCampo("empresa"))
            RsFacturaVTA_C_TOT!serie_factura_vta = Trim(SerieFacturaAImprimir)
            RsFacturaVTA_C_TOT!numero_factura = NumeroFacturaAImprimir
            RsFacturaVTA_C_TOT!tipo_linea = "T"
            RsFacturaVTA_C_TOT!numero_orden = Contador
            RsFacturaVTA_C_TOT!Descripcion = "Total factura"
            RsFacturaVTA_C_TOT!cod_tipo_iva = 0
            RsFacturaVTA_C_TOT!Base = 0
            RsFacturaVTA_C_TOT!Porcentaje = 0
            RsFacturaVTA_C_TOT!importe_abono = 0
            RsFacturaVTA_C_TOT!importe_cargo = CDbl(cntabla01.valorCampo("total_albaran"))
            RsFacturaVTA_C_TOT.Update
            SQL = "SELECT * FROM FACTURA_VTA_L WHERE 1=0"

            RsFacturaVTA_L.Open(Sql, ObjetoGlobal.cn, True,,,,,, trans)
            Contador = 0

            Dim RsGrid As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
            'Dim Contador As Integer
            'RsGrid = CnTabla02.DevuelveRecorsetGrid()
            Sql = "SELECT * FROM lineas_albaran WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND serie_albaran = '" & CnTabla01.ValorCampo("serie_albaran") & "' And numero_albaran = " & CnTabla01.ValorCampo("numero_albaran") & " ORDER BY linea_albaran "
            RsGrid.Open(Sql, ObjetoGlobal.cn)

            For i = 1 To RsGrid.RecordCount
                RsGrid.AbsolutePosition = i
                'CTabla(1).EsteRegistro i, False
                Contador = Contador + 1
                RsFacturaVTA_L.AddNew()
                RsFacturaVTA_L!empresa = Trim(RsGrid!empresa)
                RsFacturaVTA_L!serie_factura_vta = Trim(SerieFacturaAImprimir)
                RsFacturaVTA_L!numero_factura = NumeroFacturaAImprimir
                RsFacturaVTA_L!linea_factura = Contador
                RsFacturaVTA_L!tipo_linea_fra = "T"
                RsFacturaVTA_L!texto_1 = Trim(RsGrid!articulo)
                RsFacturaVTA_L!texto_2 = Trim(RsGrid!Descripcion)
                RsFacturaVTA_L!texto_3 = ""
                RsFacturaVTA_L!cod_tipo_iva = Trim(RsGrid!tipo_iva)
                RsFacturaVTA_L!unidades_l = CDbl(RsGrid!Unidades)
                RsFacturaVTA_L!precio_l = CDbl(RsGrid!Precio)
                RsFacturaVTA_L!importe_l = CDbl(RsGrid!Importe_neto)
                RsFacturaVTA_L!serie_albaran_vta = Trim(RsGrid!serie_albaran)
                RsFacturaVTA_L!numero_albaran_vta = (RsGrid!numero_albaran)
                RsFacturaVTA_L!linea_salida = CDbl(RsGrid!linea_albaran)
                RsFacturaVTA_L.Update()
            Next
            RsFacturaVTA_L.Close
            RsGrid.AbsolutePosition = RegistroActual

            RegistroActual = CnTabla02.RegistroActual

            ReDim Campos(4), Valores(4), CamposTabla(4)
            Campos(0) = "serie_factura_vta"
            Campos(1) = "numero_factura"
            Campos(2) = "fecha_factura"
            Campos(3) = "albaran_contado"
            Campos(4) = "situacion"
            Valores(0) = Trim(SerieFacturaAImprimir)
            Valores(1) = Trim(NumeroFacturaAImprimir)
            Valores(2) = CStr(FechaFacturaAImprimir)
            If Trim(CnTabla01.ValorCampo("albaran_contado")) <> "P" Then
                Valores(3) = "N"
            Else
                Valores(3) = "P"
            End If
            Valores(4) = "F"

            HOOK_AsignarValores(CnTabla01, Campos, Valores)

            'CTabla(0).EsteRegistro RegistroActual, True

            RsGrid.Close()
            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("Se ha prodcido un error al grabar la factura." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + Trim(Err.Description))
            Return False
        End Try


HacerRollBack:
        'Resume
        trans.Rollback()
        MsgBox("Se ha prodcido un error al grabar la factura." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + Trim(Err.Description))

    End Function

    Private Sub ImprimirTicket() 'NOSTD
        Dim Serie As String
        Dim tipo1 As String
        Dim tipo2 As String
        Dim HayDocumento As Boolean
        Dim Registros As Long
        Dim RegistroActual As Long
        Dim i As Integer
        Dim Cadena As String
        Dim Sql As String
        Dim Unidades As Double
        Dim CadenaUnidades As String
        Dim Precio As Double
        Dim CadenaPrecio As String
        Dim Importe As Double
        Dim Importe2 As Double
        Dim CadenaImporte As String
        Dim pr As ReportBuilder2.RawPrinterHelper = New ReportBuilder2.RawPrinterHelper
        Dim printername As String


        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("Debe aceptar o cancelar la modificación antes de anotar ticket.")
            Return
        End If
        If CnTabla02.cuantos = 0 Then
            MsgBox("No existen líneas para imprimir")
            Return
        End If

        Dim pd As PrintDocument = New PrintDocument

        'Se define el print Document.
        Dim impresora_predeterminada As String = pd.PrinterSettings.PrinterName

        printername = impresora_predeterminada
        For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
            If Trim(LCase(PrinterSettings.InstalledPrinters.Item(i).ToString)) = "epson" Then
                pr.OpenPrint("epson")
                printername = "epson"
                Exit For
            End If
        Next

        If printername <> "epson" Then
            pr.OpenPrint(printername)
        End If

        ' Principop ejemplo
        Dim strPrint As String
        strPrint = cTab(1) + " " & vbCrLf
        strPrint = strPrint & cTab(1) & "COOP.VINICOLA LLIRIA S.C.V." & vbCrLf
        strPrint = strPrint & cTab(1) & "Ctra. Liria Alcublas Km. 2,6" & vbCrLf
        strPrint = strPrint & cTab(1) & "46160 LLIRIA   Tel.962798260" & vbCrLf
        strPrint = strPrint & cTab(1) & "   N.I.F. F-46026068" & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf

        strPrint = strPrint & cTab(1) & "Factura simplificada:" + Trim(SerieFacturaAImprimir) + " " + CStr(NumeroFacturaAImprimir) & vbCrLf
        strPrint = strPrint & cTab(1) & Format(CDate(CnTabla01.ValorCampo("fecha_factura")), "dd/mm/yyyy") & " " & DateTime.Now.ToString("hh:mm:ss") & vbCrLf
        strPrint = strPrint & cTab(1) & "Fecha salida:" + Format(CDate(CnTabla01.ValorCampo("fecha_albaran")), "dd/mm/yyyy") + " " + Format(CDate(CnTabla01.ValorCampo("hora_albaran")), "hh:mm:ss") & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & "----------------------------------------" & vbCrLf

        'RegistroActual = CnTabla02.RegistroActual
        Dim RsGrid As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Contador As Integer
        'RsGrid = CnTabla02.DevuelveRecorsetGrid()
        Sql = "SELECT * FROM lineas_albaran WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND serie_albaran = '" & CnTabla01.ValorCampo("serie_albaran") & "' And numero_albaran = " & CnTabla01.ValorCampo("numero_albaran") & " ORDER BY linea_albaran "
        RsGrid.Open(Sql, ObjetoGlobal.cn)

        For i = 1 To RsGrid.RecordCount
            RsGrid.AbsolutePosition = i
            Contador = Contador + 1

            Unidades = CDbl(RsGrid!Unidades)
            If Math.Round(Unidades, 0) = Unidades Then
                CadenaUnidades = Format(Unidades, "#####")
            Else
                CadenaUnidades = Format(Unidades, "##0.0")
            End If
            Precio = CDbl(RsGrid!Precio)

            If Math.Round(Precio, 2) = Precio Then
                CadenaPrecio = Format(Precio, "##0.00")
            Else
                CadenaPrecio = Format(Precio, "#0.0000")
            End If
            Importe = CDbl(RsGrid!importe_linea)
            CadenaImporte = Format(Importe, "####0.00")

            strPrint = strPrint & Strings.Left(RsGrid!Descripcion.PadRight(35), 35) & "   "
            Cadena = CadenaUnidades.PadLeft(6) + " x" + CadenaPrecio.PadLeft(6)
            strPrint = strPrint & cTab(1) & Cadena & cTab(18) & CadenaImporte.PadLeft(8) & vbCrLf
            If i < RsGrid.RecordCount Then strPrint = strPrint & cTab(1) & cTab(1) & " " & vbCrLf
        Next

        RsGrid.Close()

        'RsGrid.AbsolutePosition = RegistroActual
        'RegistroActual = CnTabla02.RegistroActual

        strPrint = strPrint & "----------------------------------------" & vbCrLf
        Importe = CDbl(CnTabla01.ValorCampo("total_albaran"))
        CadenaImporte = Format(Importe, "####0.00").PadLeft(6)
        strPrint = strPrint & StrDup(73, " ") & CadenaImporte & " " & vbCrLf

        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & "IVA incluido en el precio." & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf
        strPrint = strPrint & cTab(1) & "." & vbCrLf
        strPrint = strPrint & cTab(1) & " " & vbCrLf

        pr.SendStringToPrinter(printername, strPrint)
        If pr.PrinterIsOpen Then
            pr.ClosePrint()
        End If
        pd.PrinterSettings.PrinterName = impresora_predeterminada

    End Sub

    Private Sub BtCarnetManipulador_Click(sender As Object, e As EventArgs) Handles BtCarnetManipulador.Click
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim CodigoCliente As Long
        Dim RsCondiciones As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim oFrmMan As FrmSeleccionManipulador = New FrmSeleccionManipulador
        Dim oFrm As FrmFirmaDocumentos = New FrmFirmaDocumentos
        Dim PathFirma As String
        Dim SerNum As String
        Dim nif_retorno As String
        Dim nombre_retorno As String
        Dim Tipo_factura As String
        Dim Seleccion_sn As String
        Dim i As Integer
        Dim Contado_sn As String

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("Debe aceptar o cancelar la modificación antes de imprimir.")
            Return
        End If
        If CnTabla02.cuantos = 0 Then
            MsgBox("No existen líneas para imprimir.")
            Return
        End If


        oFrmMan.ObjetoGlobal = Me.ObjetoGlobal 'NOSTD
        oFrmMan.nif_titular = Trim(CnTabla01.ValorCampo("cif_cliente"))
        oFrmMan.codigo_cliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
        If oFrmMan.ShowDialog <> DialogResult.OK Then
            MsgBox("Imposible emitir factura sin asignar un responsable con carnet de manipulador.")
            Return
        End If
        Contado_sn = oFrmMan.Contado_sn
        nif_retorno = Trim(oFrmMan.nif_retorno)

        CnTabla01.AsignarValor("modo_envio", nif_retorno)

        If Contado_sn = "N" Then
            RsSocios.Open("SELECT codigo_socio, alta_baja_coop from socios_coop where codigo_socio =" & (CLng(CnTabla01.ValorCampo("codigo_cliente")) - 10000), ObjetoGlobal.cn)
            If RsSocios.RecordCount > 0 Then
                If RsSocios!Alta_baja_coop = "B" Then
                    CodigoCliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
                    Sql = "SELECT * FROM COND_GEN_VTA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CLIENTE = " + CStr(CodigoCliente) + " AND TIPO_VENTA = 'C' AND VALIDEZ = 'S'  AND FORMA_PAGO<>'CONT' ORDER BY 1,2,3,4"
                    RsCondiciones.Open(Sql, ObjetoGlobal.cn)
                    If RsCondiciones.RecordCount = 0 Then
                        MsgBox("Imposible emitir factura de crédito, este socio se encuentra en situación de baja.")
                        Exit Sub
                    End If
                    RsCondiciones.Close()
                End If
            Else
                CodigoCliente = CLng(CnTabla01.ValorCampo("codigo_cliente"))
                Sql = "SELECT * FROM COND_GEN_VTA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CLIENTE = " + CStr(CodigoCliente) + " AND TIPO_VENTA = 'C' AND VALIDEZ = 'S'  AND FORMA_PAGO<>'CONT' ORDER BY 1,2,3,4"
                RsCondiciones.Open(Sql, ObjetoGlobal.cn)
                If RsCondiciones.RecordCount = 0 Then
                    MsgBox("Imposible emitir factura de crédito: cliente con forma de pago CONTADO.")
                    Exit Sub
                End If
                RsCondiciones.Close()
            End If
            RsSocios.Close()
        End If


        'Firmar albarán
        PathFirma = "\\Srvv01\Firmas\" + Trim("IM" + Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))) + ".bmp"
        If Trim(Dir(PathFirma)) = "" Then
            oFrm.path = "\\Srvv01\Firmas"
            oFrm.TextoAlbaran = Trim(Trim(CnTabla01.ValorCampo("serie_albaran")) & "-" & CnTabla01.ValorCampo("numero_albaran"))
            oFrm.Importes = CnTabla01.ValorCampo("total_albaran")
            If oFrm.ShowDialog() <> DialogResult.OK Then
                Return
            End If
            'CmdImprimir_Click
        End If

        If Contado_sn = "S" Then
            If Trim(CnTabla01.ValorCampo("Situacion")) = "N" Then
                If GrabaFacturaContado() = True Then
                    ImprimirFacturaVenta(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, SerNum, True)
                End If
            Else
                SerieFacturaAImprimir = Trim(CnTabla01.ValorCampo("serie_factura_vta"))
                NumeroFacturaAImprimir = CLng(CnTabla01.ValorCampo("numero_factura"))
                ImprimirFacturaVenta(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, SerNum, True)
            End If
        Else
            If Trim(CnTabla01.ValorCampo("Situacion")) = "N" Then
                If GrabaFacturaCredito() = True Then
                    ImprimirFacturaVentaCredito(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, SerNum)
                End If
            Else
                SerieFacturaAImprimir = Trim(CnTabla01.ValorCampo("serie_factura_vta"))
                NumeroFacturaAImprimir = CLng(CnTabla01.ValorCampo("numero_factura"))
                ImprimirFacturaVentaCredito(CnTabla01.ValorCampo("empresa"), SerieFacturaAImprimir, NumeroFacturaAImprimir, False, 1, PathFirma, SerNum)
            End If
        End If
    End Sub

    Private Sub BtTicket_Click(sender As Object, e As EventArgs) Handles BtTicket.Click

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("Debe aceptar o cancelar la modificación antes de imprimir.")
            Exit Sub
        End If
        If ArticuloControlado Then
            MsgBox("En los artículos de estas características no se admite el ticket")
            Exit Sub
        End If
        If CnTabla01.cuantos = 0 Then
            MsgBox("Debe indicar albarán para imprimir ticket.")
            Exit Sub
        End If
        If CnTabla02.cuantos = 0 Then
            MsgBox("No existen líneas para imprimir.")
            Exit Sub
        End If
        If Trim(CnTabla01.ValorCampo("albaran_contado")) = "P" Then
            MsgBox("Imposible facturar un albarán pendiente desde este proceso.")
            Exit Sub
        End If
        If Trim(CnTabla01.ValorCampo("Situacion")) = "N" Then
            If GrabaFacturaContado() = True Then ImprimirTicket
        Else
            SerieFacturaAImprimir = Trim(CnTabla01.ValorCampo("serie_factura_vta"))
            NumeroFacturaAImprimir = CLng(CnTabla01.ValorCampo("numero_factura"))
            ImprimirTicket()
        End If

    End Sub
    Private Function cTab(no) As String
        Return StrDup(no, " ")
    End Function
    Private Sub RellenaAlbaranVenta(oImp, RsCabeceras_Albaran, RsLineas_Albaran)
        VuelcaCuerpo_AlbaranVenta(oImp, RsCabeceras_Albaran, RsLineas_Albaran)
    End Sub
    Private Sub VuelcaCabecera_AlbaranVenta(oImp, RsCabeceras_Albaran, nPag)
        Dim RsProvincias As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer


        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.documento", 0, "Albarán")
        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.pagina", 0, "" & nPag)

        If RsCabeceras_Albaran.RecordCount > 0 Then
            For i = 0 To RsCabeceras_Albaran.CuantosCampos - 1
                If Trim(RsCabeceras_Albaran.NombreCampo(i)) = "numero_albaran" Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c.numero_factura", 0, Trim(CStr(RsCabeceras_Albaran!serie_albaran)) & Trim(CStr(RsCabeceras_Albaran!numero_albaran)))
                ElseIf Trim(RsCabeceras_Albaran.NombreCampo(i)) = "fecha_factura" Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c.fecha_factura", 0, Trim(CStr(RsCabeceras_Albaran!fecha_albaran)))
                ElseIf Trim(RsCabeceras_Albaran.NombreCampo(i)) = "cpostal_cliente" Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c.codigo_postal", 0, Trim(CStr(RsCabeceras_Albaran!cpostal_cliente)))
                ElseIf Trim(RsCabeceras_Albaran.NombreCampo(i)) = "numero_factura" Then

                ElseIf RsCabeceras_Albaran.NombreCampo(i) = "cif_cliente" Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c.cif", 0, Trim(CStr(RsCabeceras_Albaran!cif_cliente)))
                Else
                    If Not IsDBNull(RsCabeceras_Albaran(i)) Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c." + Trim(RsCabeceras_Albaran.nombreCampo(i)), 0, Trim(CStr(RsCabeceras_Albaran(i))))
                    End If
                End If
            Next
            If Not IsDBNull(RsCabeceras_Albaran!provincia_cliente) Then
                RsProvincias.Open("SELECT * FROM PROVINCIAS_COOP WHERE PROVINCIA = '" & RsCabeceras_Albaran!provincia_cliente & "'", ObjetoGlobal.cn)
                If RsProvincias.RecordCount > 0 Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "Calculado.Provincia", 0, IIf(IsDBNull(RsProvincias!nombre_provincia), "", Trim(RsProvincias!nombre_provincia)))
                End If
                RsProvincias.Close()
            End If
        End If

    End Sub
    Private Sub VuelcaCuerpo_AlbaranVenta(oImp, RsCabeceras_Albaran, RsLineas_Albaran)
        Dim i As Integer, j As Integer
        Dim nPag As Integer
        Dim nLineaActual As Integer

        nPag = 1
        nLineaActual = 0

        VuelcaCabecera_AlbaranVenta(oImp, RsCabeceras_Albaran, nPag)

        If RsLineas_Albaran.RecordCount > 0 Then
            For i = 1 To RsLineas_Albaran.RecordCount
                If nLineaActual >= 12 Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.sumaysigue", 0, "Sigue en la página número " & (nPag + 1))
                    nPag = nPag + 1
                    VuelcaCabecera_AlbaranVenta(oImp, RsCabeceras_Albaran, nPag)
                    nLineaActual = 0
                End If
                nLineaActual = nLineaActual + 1
                RsLineas_Albaran.AbsolutePosition = i
                For j = 0 To RsLineas_Albaran.CuantosCampos - 1
                    If Not IsDBNull(RsLineas_Albaran(j)) Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado." + Trim(RsLineas_Albaran.nombreCampo(j)) & nLineaActual, 0, Trim(CStr(RsLineas_Albaran(j))))
                    End If
                Next
            Next
        End If
        VuelcaPie_AlbaranVenta(oImp, RsCabeceras_Albaran, nPag)
    End Sub
    Private Sub VuelcaPie_AlbaranVenta(oImp, RsCabeceras_Albaran, nPag)
        Dim i As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If RsCabeceras_Albaran.RecordCount > 0 Then
            For i = 1 To 3
                If Trim(CStr(RsCabeceras_Albaran("porcentaje_iva_" & CStr(i)))) <> "-1" Then
                    If RsCabeceras_Albaran("base_iva_" & CStr(i)) <> 0 Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.base_iva_" & i, 0, Trim(CStr(RsCabeceras_Albaran("base_iva_" & CStr(i)))))
                    End If
                    If RsCabeceras_Albaran("porcentaje_iva_" & CStr(i)) <> 0 Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.porcentaje_iva_" & i, 0, Trim(CStr(RsCabeceras_Albaran("porcentaje_iva_" & CStr(i)))))
                    End If
                    If RsCabeceras_Albaran("importe_iva_" & CStr(i)) <> 0 Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.importe_iva_" & i, 0, Trim(CStr(RsCabeceras_Albaran("importe_iva_" & CStr(i)))))
                    End If
                End If
            Next
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.total_factura", 0, RsCabeceras_Albaran!total_albaran)

            If Not IsDBNull(RsCabeceras_Albaran!banco) Then
                Rs.Open("SELECT * FROM BANCOS WHERE CODIGO_BANCO ='" + Trim("" & RsCabeceras_Albaran!banco) + "'", ObjetoGlobal.cn)
                If Rs.RecordCount > 0 Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.banco", 0, Trim("" & Rs!nombre_banco))
                End If
                Rs.Close()
            End If
            If Not IsDBNull(RsCabeceras_Albaran!clave_bancaria) Then
                oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.cuenta", 0, Trim(RsCabeceras_Albaran!clave_bancaria))

            End If
            oImp.VolcarImagenAImpresion(CLng(nPag), 0, -1, 0, "calculado.imagen1", 0, Trim("\\Srvv01\Firmas\IM" & Trim(RsCabeceras_Albaran!serie_albaran) & "-" & Trim("" & RsCabeceras_Albaran!numero_albaran)) & ".bmp")
        End If

    End Sub
    Public Sub ImprimirFacturaVentaCredito(empresa As String, Serie As String, Numero As Long, bPrevisualizar As Boolean, nNumeroCopias As Integer, PathFirma As String, NumeroSerie As String, Optional bPreviewReal As Boolean = True, Optional Art_controlado As Boolean = False)
        Dim i As Integer, SQL As String
        Dim cDoc As String
        Dim RsCabeceras_factura As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsLineas_factura As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsFacturaVTA_C_TOT As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        cDoc = "Formato1"
        If Trim(ObjetoGlobal.EmpresaActual) = "1" Then
            cDoc = "formatohorto"
        ElseIf Trim(ObjetoGlobal.EmpresaActual) = "11" Then
            cDoc = "Formato A5 credito"
            If Art_controlado Then
                cDoc = "Formato A5 credito ctr"
            End If
        End If

        Dim oImp As ReportBuilder2.ClsImpresion

        oImp = New ReportBuilder2.ClsImpresion(Me.ObjetoGlobal)

        If Trim(ObjetoGlobal.EmpresaActual) = "3" Then
            If Not oImp.Inicializar("Factura de venta A5", "Factura de venta A5", "Formato A5 credito almazara", 1, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No existe el formato para la impresión del albarán de venta")
                Return
            End If
        Else
            If Not oImp.Inicializar("Factura de venta A5", "Factura de venta A5", cDoc, 1, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
                MsgBox("No existe el formato para la impresión del albarán de venta")
                Return
            End If
        End If

        SQL = "SELECT * FROM FACTURA_VTA_C WHERE "
        SQL = Trim(SQL) + " EMPRESA = '" + Trim(empresa) & "' AND SERIE_FACTURA_VTA='" & Trim(Serie) & "' AND NUMERO_FACTURA=" & CStr(Numero)

        RsCabeceras_factura.Open(SQL, ObjetoGlobal.cn)
        SQL = "SELECT * FROM FACTURA_VTA_L WHERE "
        SQL = Trim(SQL) + " EMPRESA = '" + Trim(empresa) & "' AND SERIE_FACTURA_VTA='" & Trim(Serie) & "' AND NUMERO_FACTURA=" & CStr(Numero)
        SQL = Trim(SQL) + " ORDER BY EMPRESA,SERIE_FACTURA_VTA,NUMERO_FACTURA,LINEA_FACTURA"

        RsLineas_factura.Open(SQL, ObjetoGlobal.cn)

        RellenaFacturaVentaCredito(oImp, RsCabeceras_factura, RsLineas_factura, PathFirma, NumeroSerie)

        If bPreviewReal = True Then oImp.Imprimir()
    End Sub

    Private Sub RellenaFacturaVentaCredito(oImp, RsCabeceras_factura, RsLineas_factura, PathFirma, NumeroSerie)
        VuelcaCuerpo_FacturaVentaCredito(oImp, RsCabeceras_factura, RsLineas_factura, PathFirma, NumeroSerie)

    End Sub

    Private Sub VuelcaCuerpo_FacturaVentaCredito(oimp, RsCabeceras_factura, RsLineas_factura, PathFirma, NumeroSerie)
        Dim i As Integer
        Dim j As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nPag As Integer
        Dim nLineaActual As Integer
        nLineaActual = 0
        nPag = 1

        VuelcaCabecera_FacturaVentaCredito(oimp, RsCabeceras_factura, nPag, NumeroSerie)

        If RsLineas_factura.RecordCount > 0 Then
            For i = 1 To RsLineas_factura.RecordCount
                RsLineas_factura.AbsolutePosition = i
                If nLineaActual >= 12 Then
                    oimp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.sumaysigue", 0, "Sigue en la página número " & (nPag + 1))
                    nPag = nPag + 1
                    VuelcaCabecera_FacturaVentaCredito(oimp, RsCabeceras_factura, nPag, "")
                    nLineaActual = 0
                End If
                nLineaActual = nLineaActual + 1
                Rs.Open("SELECT * FROM LINEAS_ALBARAN WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERIE_ALBARAN = '" + Trim(RsLineas_factura!serie_albaran_vta) + "' AND NUMERO_ALBARAN = " + Trim(RsLineas_factura!numero_albaran_vta) + " AND LINEA_ALBARAN  =" + CStr(RsLineas_factura!linea_salida), ObjetoGlobal.cn)
                If Rs.RecordCount > 0 Then
                    For j = 0 To Rs.CuantosCampos
                        If Not IsDBNull(Rs(j)) Then
                            oimp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado." + Trim(Rs.NombreCampo(j)) & nLineaActual, 0, Trim(CStr(Rs(j))))
                        End If
                    Next
                Else
                    MsgBox("Anote error en línea " + CStr(i) + " de factura")
                End If
                Rs.Close()
            Next
        End If
        VuelcaPie_FacturaVentaCredito(oimp, RsCabeceras_factura, nPag, PathFirma)
    End Sub

    Private Sub VuelcaCabecera_FacturaVentaCredito(oImp, RsCabeceras_factura, nPag, NumeroSerie)
        Dim RsProvincias As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClientes As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer

        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.documento", 0, "Factura")
        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.pagina", 0, "" & nPag)

        If nPag = 1 Then
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.sernumalbaran", 0, Trim(NumeroSerie))
        End If

        If RsCabeceras_factura.RecordCount > 0 Then
            For i = 0 To RsCabeceras_factura.cuantoscampos - 1
                If Trim(RsCabeceras_factura.NombreCampo(i)) = "numero_factura" Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c.numero_factura", 0, Trim(CStr(RsCabeceras_factura!serie_factura_vta)) & Trim(CStr(RsCabeceras_factura!numero_factura)))
                Else
                    If Not IsDBNull(RsCabeceras_factura(i)) Then
                        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "factura_vta_c." + Trim(RsCabeceras_factura.NombreCampo(i)), 0, Trim(CStr(RsCabeceras_factura(i))))
                    End If
                End If
            Next

            If Not IsDBNull(RsCabeceras_factura!provincia_cliente) Then
                RsProvincias.Open("SELECT * FROM PROVINCIAS_COOP WHERE PROVINCIA = '" & RsCabeceras_factura!provincia_cliente & "'", ObjetoGlobal.cn)
                If RsProvincias.RecordCount > 0 Then
                    oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "Calculado.Provincia", 0, IIf(IsDBNull(RsProvincias!nombre_provincia), "", Trim(RsProvincias!nombre_provincia)))
                End If
                RsProvincias.Close()
            End If
        End If
    End Sub

    Private Sub VuelcaPie_FacturaVentaCredito(oImp, RsCabeceras_factura, nPag, PathFirma)
        Dim i As Integer
        Dim j As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim TT(10) As Double

        Sql = "SELECT * FROM FACTURA_VTA_C_TOT WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND SERIE_FACTURA_VTA='" + Trim(SerieFacturaAImprimir) + "' AND NUMERO_FACTURA=" + CStr(NumeroFacturaAImprimir) + " ORDER BY 1,2,3,4,5"
        Rs2.Open(Sql, ObjetoGlobal.cn)
        j = 0
        For i = 1 To Rs2.RecordCount
            Rs2.AbsolutePosition = i
            If Trim(Rs2!tipo_linea) = "B" Then
                TT(0) = Math.Round(Rs2!importe_cargo, 2)
            ElseIf Trim(Rs2!tipo_linea) = "I" Then
                j = j + 1
                TT(3 * j - 2) = Math.Round(Rs2!Base, 2)
                TT(3 * j - 1) = Rs2!Porcentaje
                TT(3 * j) = Math.Round(Rs2!importe_cargo, 2)
            ElseIf Trim(Rs2!tipo_linea) = "T" Then
                TT(10) = Math.Round(Rs2!importe_cargo, 2)
            End If
        Next
        Rs2.Close()

        For i = 1 To j
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.base_iva_" + CStr(i), 0, Trim(CStr(TT(3 * i - 2))))
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.porcentaje_iva_" + CStr(i), 0, Trim(CStr(TT(3 * i - 1))))
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.importe_iva_" + CStr(i), 0, Trim(CStr(TT(3 * i))))
        Next

        oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.total_factura", 0, Trim(CStr(TT(10))))

        Rs.Open("SELECT * FROM BANCOS WHERE CODIGO_BANCO ='" + Trim(RsCabeceras_factura!cod_banco) + "'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.banco", 0, Trim(Rs!nombre_banco))
        End If
        Rs.Close()
        If Not IsDBNull(RsCabeceras_factura!clave_bancaria) Then
            oImp.VolcarAImpresion(CLng(nPag), 0, -1, 0, "calculado.cuenta", 0, Trim(RsCabeceras_factura!clave_bancaria))
        End If
        If Trim(PathFirma) <> "" Then
            oImp.VolcarImagenAImpresion(CLng(nPag), 0, -1, 0, "calculado.imagen1", 0, Trim(PathFirma))
        End If

    End Sub

    Private Function VerificarHora(ByRef Hora As String) As Boolean
        Dim j1 As Integer
        Dim j2 As Integer
        Dim HH As Integer
        Dim MM As Integer
        Dim SS As Integer
        Dim HHc As String
        Dim MMc As String
        Dim SSc As String

        j1 = InStr(1, Hora, ":")
        If j1 = 0 Then
            If Len(Hora) = 6 Then
                Hora = Mid(Hora, 1, 2) + ":" + Mid(Hora, 3, 2) + ":" + Mid(Hora, 5, 2)
            ElseIf Len(Hora) = 5 Then
                Hora = "0" + Mid(Hora, 1, 1) + ":" + Mid(Hora, 2, 2) + ":" + Mid(Hora, 4, 2)
            ElseIf Len(Hora) = 4 Then
                Hora = Mid(Hora, 1, 2) + ":" + Mid(Hora, 3, 2) + ":00"
            ElseIf Len(Hora) = 3 Then
                Hora = "0" + Mid(Hora, 1, 1) + ":" + Mid(Hora, 2, 2) + ":00"
            ElseIf Len(Hora) = 2 Then
                Hora = Trim(Hora) + ":00:00"
            ElseIf Len(Hora) = 1 Then
                Hora = "0" + Trim(Hora) + ":00:00"
            End If
            j1 = InStr(1, Hora, ":")
        End If
        If j1 = 0 Or j1 = Len(Hora) Then
            Return False
        End If
        j2 = InStr(j1 + 1, Hora, ":")
        If j2 = 0 Then
            If Len(Hora) = 5 Then
                Hora = Trim(Hora) + ":00"
            ElseIf Len(Hora) = 4 And j1 = 3 Then
                Hora = Mid(Hora, 1, 3) + "0" + Mid(Hora, 4, 1) + ":00"
            ElseIf Len(Hora) = 4 And j1 = 2 Then
                Hora = "0" + Mid(Hora, 1, 4) + ":00"
            End If
        End If
        j2 = InStr(j1 + 1, Hora, ":")
        If j2 = 0 Then
            Return False
        End If
        HHc = Mid(Hora, 1, j1 - 1)
        MMc = Mid(Hora, j1 + 1, j2 - j1 - 1)
        SSc = Mid(Hora, j2 + 1)
        If Not (IsNumeric(HHc)) Or Not (IsNumeric(MMc)) Or Not (IsNumeric(SSc)) Then
            Return False
        End If
        HH = CInt(HHc) : MM = CInt(MMc) : SS = CInt(SSc)
        If HH < 0 Or HH > 23 Then
            Return False
        End If
        If MM < 0 Or MM >= 60 Then
            Return False
        End If
        If SS < 0 Or SS >= 60 Then
            Return False
        End If
        Hora = Format(HH, "00") + ":" + Format(MM, "00") + ":" + Format(SS, "00")
        If Hora = "00:00:00" Then
            Return False
        End If

        Return True

    End Function

    Private Function DameStock(cArg As String, cAlm As String, dFechaHora As Date) As Double
        Dim RsInv As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Cadena As String
        Dim sSql As String
        Dim sk As Double

        sk = 0
        If cArg <> "" And cAlm <> "" Then
            Cadena = Format(dFechaHora, "dd/mm/yyyy hh:m:ss")
            sSql = "select dbo.fn_existencia_a_fecha('" & Trim(ObjetoGlobal.EmpresaActual) & "', '" & Trim(ObjetoGlobal.EjercicioActual) & "', '" & Trim(cArg) & "', '" & Cadena & "', 'N', '" & Trim(cAlm) & "' )"
            RsInv.Open(sSql, ObjetoGlobal.cn)
            If Not RsInv.EOF Then
                sk = RsInv(0)
            End If
        End If
        Return sk
    End Function

End Class

