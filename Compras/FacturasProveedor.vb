Module FacturasProveedor
    Dim ArrayFechaImporteVto(3, 0)
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
    Public Xnumero_lineas As Integer
    Public Xdetalle_factura() As String
    Public Const PROCESS_QUERY_INFORMATION = &H400 'NOSTD
    Public Const STILL_ACTIVE = &H103 'NOSTD
    Public ObjetoGlobal As Object

    Public Sub TotalFacturaCompra(pform As FrmFacturasProveedor, Importe1 As Double, Importe2 As Double)
        Dim i As Integer
        Dim j As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim BI(10) As Double, IVA(10) As Double, Tipos(10) As Integer, Porcentaje(10) As Single, CuantosIVAs As Integer
        Dim Tipo As Integer, TipoLinea As String
        Dim Sql As String

        Sql = "select * temp_factura_compra WHERE empresa = '" & ObjetoGlobal.empresaactual & "' AND proceso =" & pform.CnTabla02.ValorCampo("proceso") & " order by 1,2,3"
        Rs.Open(Sql, ObjetoGlobal.cn)
        Importe1 = 0
        CuantosIVAs = 0
        While Not Rs.EOF
            Tipo = Rs!tipo_iva
            For j = 1 To CuantosIVAs
                If Tipos(j) = Tipo Then
                    BI(j) = Math.Round(BI(j) + Math.Round(Rs!Importe, 2), 2)
                    j = 1000
                End If
            Next
            If j < 1000 Then
                CuantosIVAs = CuantosIVAs + 1
                BI(CuantosIVAs) = Math.Round(Rs!Importe, 2)
                Tipos(CuantosIVAs) = Tipo
                Porcentaje(CuantosIVAs) = Rs!Porcentaje
            End If
            Rs.MoveNext()
        End While
        For i = 1 To CuantosIVAs
            IVA(i) = Math.Round(BI(i) * Porcentaje(i) / 100, 2)
            Importe1 = Math.Round(Importe1 + BI(i) + IVA(i), 2)
        Next
        pform.TxtTotalFactura0.Text = Format(Importe1, "#, 0.00")

        Rs2.Open("SELECT * FROM facturas_com_c_tot WHERE empresa =" & ObjetoGlobal.empresaactual & " AND numero_entrada_fra = " & pform.CnTabla01.ValorCampo("numero_entrada_fra") & " ORDER BY 1,2,3,4", ObjetoGlobal.cn)
        Importe2 = 0
        While Not Rs2.EOF
            TipoLinea = Trim(Rs2!tipo_linea)
            If TipoLinea = "T" Then
                    Importe2 = Math.Round(Rs2!importe_cargo - Rs2!importe_abono, 2)
                Exit While
            End If
            Rs2.MoveNext()
        End While
        pform.TxtTotalFactura1.Text = Format(Importe2, "#, 0.00")

    End Sub
    Public Function GenerarDatosFacturaCompra(pform As FrmFacturasProveedor) As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsF As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs3 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs4 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsParametros As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsConceptos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Puntero1 As Long
        Dim Puntero2 As Long
        Dim Importe1 As Double
        Dim Importe2 As Double
        Dim BI(10) As Double
        Dim IVA(10) As Double
        Dim Tipos(10) As Integer
        Dim Porcentaje(10) As Single
        Dim CuentasIVA(10) As String
        Dim ImporteCuentaIVA(10) As Double
        Dim CuantosIVAs As Integer
        Dim CuantasCuentasIVA As Integer
        Dim CuentasCOMPRA(30) As String
        Dim ImporteCuentaCOMPRA(30) As Double
        Dim CuantasCuentasCOMPRA As Integer
        Dim CuentaCompra As String
        Dim Tipo As Integer, TipoLinea As String, Grupo As String, Sql As String
        Dim CuentaProveedor As String, CuentaIva As String
        Dim CuentaIRPF As String
        Dim DigitosContabilidad As Integer, proceso As String
        Dim DescripcionProveedor As String, ConceptoProveedor As String
        Dim DescripcionCompra As String, ConceptoCompra As String
        Dim DescripcionIVA As String, ConceptoIVA As String
        Dim sSql As String
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction
        Dim cNumero As String
        Dim cLinea As String
        Dim SqlPar As String
        Dim retencion As Double

        GenerarDatosFacturaCompra = False

        RsF = pform.CnTabla01.DevuelveRecordset()
        Rs = pform.CnTabla02.DevuelveRecorsetGrid


        Importe1 = 0
        Importe2 = 0
        Puntero1 = Rs.AbsolutePosition
        CuantosIVAs = 0
        CuantasCuentasIVA = 0
        CuantasCuentasCOMPRA = 0
        retencion = 0


        Rs3.Open("SELECT * FROM TABLAS_IVA_PROV WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND CODIGO_TABLA = '" + Trim(pform.CnTabla01.ValorCampo("tabla_iva_prov")) + "'", ObjetoGlobal.cn)
        If Rs3.RecordCount = 0 Then
            MsgBox("No existe tabla de iva para la factura. Se cancela grabación.")
            Return False
            Exit Function
        End If
        Grupo = Trim(Rs3!grupo_liquidacion)
        DigitosContabilidad = CalculoNivelSubcuenta()
        If DigitosContabilidad <= 0 Then
            MsgBox("No existe nivel definido para las subcuentas de esta contabilidad. Se cancela grabación.")
            Return False
        End If
        Rs3.Close()

        Sql = "select * temp_factura_compra WHERE empresa = '" & ObjetoGlobal.empresaactual & "' AND proceso =" & pform.CnTabla02.ValorCampo("proceso") & " order by 1,2,3"
        Rs.Open(Sql, ObjetoGlobal.cn)

        While Not Rs.EOF
            Tipo = Rs!tipo_iva
            For j = 1 To CuantosIVAs
                If Tipos(j) = Tipo Then
                    BI(j) = Math.Round(BI(j) + Math.Round(Rs!Importe, 2), 2)
                    j = 1000
                End If
            Next
            If j < 1000 Then
                CuantosIVAs = CuantosIVAs + 1
                BI(CuantosIVAs) = Math.Round(Rs!Importe, 2)
                Tipos(CuantosIVAs) = Tipo
                Porcentaje(CuantosIVAs) = Rs!Porcentaje
            End If
            Rs.MoveNext()
        End While
        For i = 1 To CuantosIVAs
            IVA(i) = Math.Round(BI(i) * Porcentaje(i) / 100, 2)
            Importe1 = Math.Round(Importe1 + BI(i) + IVA(i), 2)
            Importe2 = Math.Round(Importe2 + BI(i), 2)

            Rs2.Open("SELECT * FROM PORC_IVA_SOPORTADO WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND TABLA_IVA_PROV = '" + Trim(pform.CnTabla01.Rs("tabla_iva_prov")) + "' AND TIPO_IVA = " + CStr(Tipos(i)), ObjetoGlobal.cn, True)
            If Rs2.RecordCount = 0 Then
                MsgBox("No existe tipo de iva para la factura. Se cancela grabación.")
                Exit Function
            End If
            If Len(Trim(Rs2!CUENTA_IVA)) <> DigitosContabilidad Then
                CuentaIva = "472" + Left("0000000000", DigitosContabilidad - 3)
            Else
                CuentaIva = Trim(Rs2!CUENTA_IVA)
            End If
            Rs2.Close()

            For j = 1 To CuantasCuentasIVA
                If Trim(CuentaIva) = Trim(CuentasIVA(j)) Then
                    ImporteCuentaIVA(j) = Math.Round(ImporteCuentaIVA(j) + IVA(i), 2)
                    j = 1000
                End If
            Next
            If j < 1000 Then
                CuantasCuentasIVA = CuantasCuentasIVA + 1
                CuentasIVA(CuantasCuentasIVA) = Trim(CuentaIva)
                ImporteCuentaIVA(CuantasCuentasIVA) = IVA(i)
                Rs2.Open("SELECT * FROM CUENTAS WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND CODIGO_CUENTA = '" + Trim(CuentaIva) + "'", ObjetoGlobal.cn)
                If Rs2.RecordCount = 0 Then
                    MsgBox("No existe cuenta para el apunte de iva. Se cancela grabación.")
                    Return False
                End If
                Rs2.Close()
            End If
        Next
        For i = 0 To Rs.RecordCount - 1
            Rs.AbsolutePosition = i + 1
            CuentaCompra = Trim(Rs!Cuenta)
            For j = 1 To CuantasCuentasCOMPRA
                If Trim(CuentasCOMPRA(j)) = Trim(CuentaCompra) Then
                    ImporteCuentaCOMPRA(j) = Math.Round(ImporteCuentaCOMPRA(j) + Math.Round(Rs!Importe, 2), 2)
                    j = 1000
                End If
            Next
            If j < 1000 Then
                CuantasCuentasCOMPRA = CuantasCuentasCOMPRA + 1
                ImporteCuentaCOMPRA(CuantasCuentasCOMPRA) = Math.Round(Rs!Importe, 2)
                CuentasCOMPRA(CuantasCuentasCOMPRA) = Trim(CuentaCompra)
            End If
        Next
        If Puntero1 > -1 Then Rs.AbsolutePosition = Puntero1
        Sql = "SELECT * FROM CONDICIONES_COMPRA WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "'"
        Sql = Trim(Sql) + " AND CODIGO_PROVEEDOR = " + Trim(pform.CnTabla01.ValorCampo("codigo_proveedor"))
        Sql = Trim(Sql) + " AND TIPO_COMPRA = '" + Trim(pform.CnTabla01.ValorCampo("Tipo_compra")) + "'"
        Sql = Trim(Sql) + " AND FECHA_INICIO <= '" + Format(CDate(pform.CnTabla01.ValorCampo("fecha_factura")), "dd/MM/yyyy") +
        Sql = Trim(Sql) + " AND FECHA_FINAL >= '" + Format(CDate(pform.CnTabla01.ValorCampo("fecha_factura")), "dd/mm/yyyy") + "'"
        Sql = Trim(Sql) + " AND VALIDEZ = 'S' AND LEN(CODIGO_CONTABLE)=" + CStr(DigitosContabilidad)
        Rs3.Open(Sql, ObjetoGlobal.cn)
        If Rs3.RecordCount > 0 Then
            CuentaProveedor = Rs3!codigo_contable
        Else
            CuentaProveedor = Trim(CStr(pform.CnTabla01.ValorCampo("codigo_proveedor")))
            CuentaProveedor = "4" & Left("000000000", DigitosContabilidad - 1 - Len(CuentaProveedor)) + Trim(CStr(pform.CnTabla01.ValorCampo("codigo_proveedor")))
        End If
        Rs3.Close()

        CuentaIRPF = "4751" & Left("000000000", DigitosContabilidad - 4)

        Rs3.Open("SELECT * FROM CUENTAS WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND CODIGO_CUENTA = '" + Trim(CuentaProveedor) + "'", ObjetoGlobal.cn)
        If Rs3.RecordCount = 0 Then
            MsgBox("No existe cuenta para el apunte de proveedor. Se cancela grabación.")
            Return False
        End If
        proceso = "ANOTACION FACTURA COMPRA"

        'RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "'", ObjetoGlobal.cn)

        'Datos apunte proveedor
        If Importe2 > 0 Then
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'DESCRIPCION FACTURA PROVEEDOR'", ObjetoGlobal.cn)
        Else
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'DESCRIPCION ABONO PROVEEDOR'", ObjetoGlobal.cn)
        End If
        If RsParametros.RecordCount > 0 Then
            DescripcionProveedor = Trim(RsParametros!valor_defecto)
        Else
            DescripcionProveedor = "Su Factura /S/-/N/"
        End If
        RsParametros.Close()

        If Importe2 > 0 Then
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'CONCEPTO FACTURA PROVEEDOR'", ObjetoGlobal.cn)
        Else
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'CONCEPTO ABONO PROVEEDOR'", ObjetoGlobal.cn)
        End If
        If RsParametros.RecordCount > 0 Then
            ConceptoProveedor = Trim(RsParametros!valor_defecto)
        Else
            ConceptoProveedor = "---"
        End If
        RsParametros.Close()
        Sql = "SELECT * FROM CONCEPTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND CODIGO_CONCEPTO = '" + Trim(ConceptoProveedor) + "'"
        RsConceptos.Open(Sql, ObjetoGlobal.cn)
        If RsConceptos.RecordCount = 0 Then
            MsgBox("No está definido el concepto contable indicado para apunte de proveedor (" + Trim(ConceptoProveedor) + "). Se cancela grabación.")
            Return False
        End If
        RsConceptos.Close()

        'Datos apunte compra
        If Importe2 > 0 Then
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'DESCRIPCION FACTURA COMPRA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'DESCRIPCION FACTURA COMPRA'"
        Else
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'DESCRIPCION ABONO COMPRA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'DESCRIPCION ABONO COMPRA'"
        End If
        If RsParametros.RecordCount > 0 Then
            DescripcionCompra = Trim(RsParametros!valor_defecto)
        Else
            DescripcionCompra = "Fra. /S/-/N/ Pr: /P/  /NP/"
        End If
        RsParametros.Close()
        If Importe2 > 0 Then
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'CONCEPTO FACTURA COMPRA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'CONCEPTO FACTURA COMPRA'"
        Else
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'CONCEPTO ABONO COMPRA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'CONCEPTO ABONO COMPRA'"
        End If
        If RsParametros.RecordCount > 0 Then
            ConceptoCompra = Trim(RsParametros!valor_defecto)
        Else
            ConceptoCompra = "---"
        End If
        RsParametros.Close()
        RsConceptos.Open(Sql, ObjetoGlobal.cn)
        If RsConceptos.RecordCount = 0 Then
            MsgBox("No está definido el concepto contable indicado para apunte de compra (" + Trim(ConceptoCompra) + "). Se cancela grabación.")
            Return False
        End If
        RsConceptos.Close()

        'Datos apunte iva
        If Importe2 > 0 Then
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'DESCRIPCION FACTURA IVA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'DESCRIPCION FACTURA IVA'"
        Else
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'DESCRIPCION ABONO IVA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'DESCRIPCION ABONO IVA'"
        End If
        If RsParametros.RecordCount > 0 Then
            DescripcionIVA = Trim(RsParametros!valor_defecto)
        Else
            DescripcionIVA = "Fra. /S/-/N/ Pr: /P/  /NP/"
        End If
        RsParametros.Close()
        If Importe2 > 0 Then
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'CONCEPTO FACTURA IVA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'CONCEPTO FACTURA IVA'"
        Else
            RsParametros.Open("SELECT * FROM TABLA_PARAMETROS WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' AND PROCESO = '" & proceso & "' AND CAMPO = 'CONCEPTO ABONO IVA'", ObjetoGlobal.cn)
            'RsParametros.Filter = "CAMPO = 'CONCEPTO ABONO IVA'"
        End If
        If RsParametros.RecordCount > 0 Then
            ConceptoIVA = Trim(RsParametros!valor_defecto)
        Else
            ConceptoIVA = "---"
        End If
        RsParametros.Close()
        RsConceptos.Open(Sql, ObjetoGlobal.cn)
        If RsConceptos.RecordCount = 0 Then
            MsgBox("No está definido el concepto contable indicado para apunte de I.V.A. (" + Trim(ConceptoIVA) + "). Se cancela grabación.")
            Return False
        End If
        RsConceptos.Close()
        RsParametros.Close()

        If pform.SeRetiene.Checked Then
            retencion = Math.Round(CDbl(pform.base_retencion) * CDbl(pform.tipo_retencion), 2)
            Importe1 = Importe1 - retencion
        End If

        If GenerarVencimientos(pform.CnTabla01.Rs("f_pago"), pform.CnTabla01.Rs("dia1_pago"), pform.CnTabla01.Rs("dia2_pago"), pform.CnTabla01.Rs("dia3_pago"), pform.CnTabla01.Rs("dias_retraso_vto"), Importe1, pform.CnTabla01.Rs("fecha_factura")) = False Then
            Return False
        End If
        Try
            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            Rs2.Open("SELECT * FROM factura_com_c_tot WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND NUMERO_ENTRADA_FRA= " + CStr(pform.CnTabla01.ValorCampo("numero_entrada_fra")), ObjetoGlobal.cn, True,,,,,, trans)
            Do While Rs2.RecordCount > 0
                Rs2.MoveFirst()
                Rs2.Delete()
            Loop
            Rs2.AddNew()
            Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
            Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
            Rs2!tipo_linea = "B"
            Rs2!numero_orden = 10
            'Rs2!conc_cargo_dto = Null
            Rs2!Descripcion = "Base factura"
            Rs2!cod_tipo_iva = 0
            Rs2!Base = 0
            Rs2!Porcentaje = 0
            Rs2!importe_abono = 0
            Rs2!importe_cargo = Importe2
            'Rs2!cod_grupo_liq_iva = Null
            Rs2.Update()

            ' Si tiene retención la ponemos aquí
            If pform.SeRetiene.Checked Then

                Rs2.AddNew()
                Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
                Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
                Rs2!tipo_linea = "IRP"
                Rs2!numero_orden = 10
                'Rs2!conc_cargo_dto = Null
                Rs2!Descripcion = "IRPF"
                Rs2!cod_tipo_iva = 0
                Rs2!Base = CDbl(pform.base_retencion)
                Rs2!Porcentaje = CDbl(pform.tipo_retencion)
                Rs2!importe_abono = 0
                Rs2!importe_cargo = retencion
                'Rs2!cod_grupo_liq_iva = Null
                Rs2.Update()
            End If

            For i = 1 To CuantosIVAs
                Rs2.AddNew()
                Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
                Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
                Rs2!tipo_linea = "I"
                Rs2!numero_orden = 10 * i
                'Rs2!conc_cargo_dto = Null
                'Rs2!Descripcion = Null
                Rs2!cod_tipo_iva = Tipos(i)
                Rs2!Base = BI(i)
                Rs2!Porcentaje = Porcentaje(i)
                Rs2!importe_abono = 0
                Rs2!importe_cargo = IVA(i)
                Rs2!cod_grupo_liq_iva = Trim(Grupo)
                Rs2.Update()
            Next

            Rs2.AddNew()
            Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
            Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
            Rs2!tipo_linea = "T"
            Rs2!numero_orden = 10
            'Rs2!conc_cargo_dto = Null
            Rs2!Descripcion = "Total factura"
            Rs2!cod_tipo_iva = 0
            Rs2!Base = 0
            Rs2!Porcentaje = 0
            Rs2!importe_abono = 0
            Rs2!importe_cargo = Importe1
            'Rs2!cod_grupo_liq_iva = Null
            Rs2.Update()
            Rs2.Close()

            'factura_com_c_ENL
            Rs2.Open("SELECT * FROM factura_com_c_enl WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND NUMERO_ENTRADA_FRA= " + CStr(pform.CnTabla01.ValorCampo("numero_entrada_fra")), ObjetoGlobal.cn, True,,,,,, trans)
            While Rs2.RecordCount > 0
                Rs2.MoveFirst()
                Rs2.Delete()
            End While
            Rs2.AddNew()
            Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
            Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
            Rs2!tipo_linea = "P"
            Rs2!numero_orden = 10
            Rs2!Codigo_cuenta = Trim(CuentaProveedor)
            Rs2!concepto = Trim(ConceptoProveedor)
            Rs2!Descripcion_apunte = ConvertirDescripcionFacturaCompra(DescripcionProveedor, pform.CnTabla01.ValorCampo("numero_entrada_fra"))
            Rs2!Importe_debe = 0
            Rs2!Importe_haber = Importe1
            Rs2.Update()

            For i = 1 To CuantasCuentasIVA
                Rs2.AddNew()
                Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
                Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
                Rs2!tipo_linea = "I"
                Rs2!numero_orden = 10 * i
                Rs2!Codigo_cuenta = CuentasIVA(i)
                Rs2!concepto = Trim(ConceptoIVA)
                Rs2!Descripcion_apunte = ConvertirDescripcionFacturaCompra(DescripcionIVA, pform.CnTabla01)
                Rs2!Importe_debe = ImporteCuentaIVA(i)
                Rs2!Importe_haber = 0
                Rs2.Update()
            Next
            For i = 1 To CuantasCuentasCOMPRA
                Rs2.AddNew()
                Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
                Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
                Rs2!tipo_linea = "C"
                Rs2!numero_orden = 10 * i
                Rs2!Codigo_cuenta = CuentasCOMPRA(i)
                Rs2!concepto = Trim(ConceptoCompra)
                Rs2!Descripcion_apunte = ConvertirDescripcionFacturaCompra(DescripcionCompra, pform.CnTabla01)
                Rs2!Importe_debe = ImporteCuentaCOMPRA(i)
                Rs2!Importe_haber = 0
                Rs2.Update()
            Next

            Rs2.AddNew()
            Rs2!empresa = Trim(ObjetoGlobal.empresaactual)
            Rs2!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
            Rs2!tipo_linea = "C"
            Rs2!numero_orden = 10 * i
            Rs2!Codigo_cuenta = CuentaIRPF
            Rs2!concepto = "Retención IRPF"
            Rs2!Descripcion_apunte = ConvertirDescripcionFacturaCompra(DescripcionCompra, pform.CnTabla01)
            Rs2!Importe_debe = 0
            Rs2!Importe_haber = retencion
            Rs2.Update()

            Rs2.Close()

            Rs2.Open("SELECT * FROM FACTURA_COM_C_VTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "' AND NUMERO_ENTRADA_FRA= " + CStr(pform.CnTabla01.ValorCampo("numero_entrada_fra")), ObjetoGlobal.cn, True,,,,,, trans)
            Do While Rs2.RecordCount > 0
                Rs2.MoveFirst()
                Rs2.Delete()
            Loop
            For i = 0 To UBound(ArrayFechaImporteVto, 2) - 1
                Rs2.AddNew()
                Rs2!empresa = ObjetoGlobal.empresaactual
                Rs2!numero_entrada_fra = Trim(pform.CnTabla01.ValorCampo("numero_entrada_fra"))
                Rs2!Contador = 10 * (i + 1)
                Rs2!Tipo_Documento = Trim(ArrayFechaImporteVto(2, i))
                Rs2!codigo_motivo = "ME"
                Rs2!fecha_vencimiento = CDate(ArrayFechaImporteVto(0, i))
                Rs2!Importe = Math.Round(ArrayFechaImporteVto(1, i), 2)
                Rs2.Update()
            Next i
            Rs2.Close()

            ' Pone los albaranes de compra como facturados

            If Xnumero_lineas > 0 Then
                For i = 1 To Xnumero_lineas
                    cNumero = Mid(Xdetalle_factura(i), 1, 10)
                    cLinea = Mid(Xdetalle_factura(i), 11, 5)
                    sSql = "SELECT * FROM albaran_com_l WHERE EMPRESA = '" & Trim(ObjetoGlobal.empresaactual) & "' and numero_entrada=" & cNumero & " and linea_albaran =" & cLinea
                    RsAlb.Open(sSql, ObjetoGlobal.cn, True,,,,,, trans)
                    If RsAlb.RecordCount > 0 Then
                        RsAlb!Situacion = "F"
                        RsAlb!numero_entrada_fra = pform.CnTabla01.ValorCampo("numero_entrada_fra")
                        RsAlb.Update()
                    End If
                    RsAlb.Close()
                Next
            End If

            trans.Commit()
            ObjetoGlobal.cn.Close()
            Xnumero_lineas = 0
            Return True

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("No se puede generar datos factura." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + Trim(Err.Description))
            Return False
        End Try


    End Function
    Public Function CalculoNivelSubcuenta()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, Nivel As Integer, i As Integer

        CalculoNivelSubcuenta = -1

        Rs.Open("SELECT * FROM NIVELES_CONTABLES WHERE EMPRESA = '" + Trim(ObjetoGlobal.empresaactual) + "'", ObjetoGlobal.cn)
        If Rs.RecordCount = 0 Then Exit Function
        Nivel = -1
        For i = 13 To 1 Step -1
            If Rs("nivel_" + CStr(i)).Value = "S" Then
                Nivel = i
                Exit For
            End If
        Next
        Rs.Close()
        Return Nivel
    End Function
    Private Function ConvertirDescripcionFacturaCompra(Cad As String, Rs As Object) As String
        Dim i As Integer

        ConvertirDescripcionFacturaCompra = ""
        i = 1
        While i <= Len(Cad)
            If Mid(Cad, i, 1) = "/" Then
                Select Case Mid(Cad, i, 3)
                    Case "/S/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & Trim(Rs!serie_factura_com) : i = i + 2
                    Case "/N/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & Trim(Rs!numero_fra_com) : i = i + 2
                    Case "/FF" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & Format(Rs!fecha_factura, "dd/mm/yy") : i = i + 2
                    Case "/NP" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & Trim(Rs!razon_social) : i = i + 2
                    Case "/P/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & Trim(Rs!codigo_proveedor) : i = i + 2
                    Case "/C/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "" : i = i + 2
                    Case "/NC" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "" : i = i + 3
                    Case "/I/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "" : i = i + 2
                    Case "/1/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "" : i = i + 2
                    Case "/U/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "" : i = i + 2
                    Case "/E/" : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "" : i = i + 2
                    Case Else : ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & "/"
                End Select
            Else
                ConvertirDescripcionFacturaCompra = ConvertirDescripcionFacturaCompra & Mid(Cad, i, 1)
            End If
            i = i + 1
        End While
        ConvertirDescripcionFacturaCompra = Left(ConvertirDescripcionFacturaCompra, 60)
    End Function
    Public Function GenerarVencimientos(FormaPago As String, Dia1Pago As Integer, Dia2Pago As Integer, Dia3pago As Integer, DiasRetraso As Integer, ImporteFactura As Double, FechaFactura As Date) As Boolean
        Dim RsFormasPago As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsVencimientos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsAux As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim ArrayPorcentajes()
        Dim SinPorcentaje As Integer, Porcentaje As Double
        Dim FechaVencimiento As Date
        Dim mes As Integer
        Dim año As Integer
        Dim FechaAuxiliar As Date
        Dim FechaMinima As Date
        Dim FechaPago As Date
        Dim DiaAux1 As Integer
        Dim DiaAux2 As Integer
        Dim DiaAux3 As Integer
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim TotalFacturaAux As Double
        Dim retraso As Boolean
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim FlagVacaciones As Boolean
        Dim VacacionesDesde As Date
        Dim VacacionesHasta As Date
        Dim VacacionesEjercicio As Boolean
        Dim NumeroEfecto As Long

        Try
            GenerarVencimientos = False
            If IsDBNull(Dia1Pago) Then
                Dia1Pago = 0
            Else
                If Not IsNumeric(Dia1Pago) Then Dia1Pago = 0
                If Dia1Pago < 0 Then Dia1Pago = 0
            End If
            If IsDBNull(Dia2Pago) Then
                Dia2Pago = 0
            Else
                If Not IsNumeric(Dia2Pago) Then Dia2Pago = 0
                If Dia2Pago < 0 Then Dia2Pago = 0
            End If
            If IsDBNull(Dia3pago) Then
                Dia3pago = 0
            Else
                If Not IsNumeric(Dia3pago) Then Dia3pago = 0
                If Dia3pago < 0 Then Dia3pago = 0
            End If
            If IsDBNull(DiasRetraso) Then
                DiasRetraso = 0
            Else
                If Not IsNumeric(DiasRetraso) Then DiasRetraso = 0
                If DiasRetraso < 0 Then DiasRetraso = 0
            End If
            If Trim(FormaPago) = "" Then FormaPago = "CONT"

            RsFormasPago.Open("SELECT * FROM FORMAS_PAGO WHERE EMPRESA='" & ObjetoGlobal.empresaactual & "' AND FORMA_PAGO='" & Trim(FormaPago) & "'", ObjetoGlobal.cn)
            If RsFormasPago.RecordCount = 0 Then
                MsgBox("No se pueden generar los vencimientos de la factura : forma de pago inexistente.")
                Return False
            End If

            RsVencimientos.Open("SELECT * FROM VENCIMIENTOS WHERE EMPRESA='" & ObjetoGlobal.empresaactual & "' AND FORMA_PAGO='" & FormaPago & "' ORDER BY CONTADOR", ObjetoGlobal.cn, True)
            If RsVencimientos.RecordCount = 0 Then
                MsgBox("No se pueden generar los vencimientos de la factura : forma de pago inexistente.")
                Return False
            End If
            ReDim ArrayFechaImporteVto(2, RsVencimientos.RecordCount) '1->Fecha,2->Importe
            ReDim ArrayPorcentajes(RsVencimientos.RecordCount) 'Porcentajes de cada vto.
            RsVencimientos.MoveFirst()
            SinPorcentaje = 0
            Porcentaje = 0
            'Reparto los porcentajes
            For i = 1 To RsVencimientos.RecordCount
                If Trim(RsVencimientos!Porcentaje) <> "" And RsVencimientos!Porcentaje <> 0 Then
                    ArrayPorcentajes(i - 1) = RsVencimientos!Porcentaje
                    Porcentaje = Porcentaje + RsVencimientos!Porcentaje
                Else
                    SinPorcentaje = SinPorcentaje + 1
                End If
                RsVencimientos.MoveNext()
            Next i
            If SinPorcentaje <> 0 And Porcentaje <> 100 Then
                Porcentaje = 100 - Porcentaje
                RsVencimientos.MoveFirst()

                For i = 1 To RsVencimientos.RecordCount
                    If Trim(RsVencimientos!Porcentaje) = "" Or RsVencimientos!Porcentaje = 0 Then
                        ArrayPorcentajes(i - 1) = CDbl(Porcentaje / CDbl(SinPorcentaje))
                    End If
                    RsVencimientos.MoveNext()
                Next i
            End If
            'Reordeno en orden creciente los días de pago
            If Dia1Pago = 0 Or Dia1Pago > 31 Then Dia1Pago = 999
            If Dia2Pago = 0 Or Dia2Pago > 31 Then Dia2Pago = 999
            If Dia3pago = 0 Or Dia3pago > 31 Then Dia3pago = 999
            If Dia1Pago < Dia2Pago Then
                If CInt(Dia1Pago) < CInt(Dia3pago) Then
                    DiaAux1 = Dia1Pago
                    If CInt(Dia2Pago) < CInt(Dia3pago) Then
                        DiaAux2 = Dia2Pago
                        DiaAux3 = Dia3pago
                    Else
                        DiaAux2 = Dia3pago
                        DiaAux3 = Dia2Pago
                    End If
                Else
                    DiaAux1 = Dia3pago
                    If CInt(Dia1Pago) < CInt(Dia2Pago) Then
                        DiaAux2 = Dia1Pago
                        DiaAux3 = Dia2Pago
                    Else
                        DiaAux2 = Dia2Pago
                        DiaAux3 = Dia1Pago
                    End If
                End If
            Else
                If CInt(Dia2Pago) < CInt(Dia3pago) Then
                    DiaAux1 = Dia2Pago
                    If CInt(Dia1Pago) < CInt(Dia3pago) Then
                        DiaAux2 = Dia1Pago
                        DiaAux3 = Dia3pago
                    Else
                        DiaAux2 = Dia3pago
                        DiaAux3 = Dia1Pago
                    End If
                Else
                    DiaAux1 = Dia1Pago
                    DiaAux2 = Dia2Pago
                    DiaAux3 = Dia3pago
                End If
            End If
            Dia1Pago = DiaAux1
            Dia2Pago = DiaAux2
            Dia3pago = DiaAux3 'Ahora ya se cumple dia1pago<dia2pago<dia3pago

            'Calculo las fechas
            RsVencimientos.MoveFirst()

            For i = 1 To RsVencimientos.RecordCount
                retraso = False
                Select Case Trim(RsVencimientos!tipo_vencimiento)
                    Case "S"
                        FechaVencimiento = DateAdd("ww", Val(RsVencimientos!vencimiento), FechaFactura)
                    Case "M"
                        FechaVencimiento = DateAdd("m", Val(RsVencimientos!vencimiento), FechaFactura)
                    Case Else '"D"
                        FechaVencimiento = DateAdd("d", Val(RsVencimientos!vencimiento), FechaFactura)
                End Select
                If Dia1Pago <> 999 Or Dia2Pago <> 999 Or Dia3pago <> 999 Then
                    'Posibilidad de retrasar vto.
                    For k = DiasRetraso To 0 Step -1
                        If (Microsoft.VisualBasic.Day(DateAdd("d", -k, FechaVencimiento)) = Dia1Pago Or Microsoft.VisualBasic.Day(DateAdd("d", -k, FechaVencimiento)) = Dia2Pago Or Microsoft.VisualBasic.Day(DateAdd("d", -k, FechaVencimiento)) = Dia3pago) And (DateAdd("d", -k, FechaVencimiento) >= FechaFactura) Then
                            FechaPago = DateAdd("d", -k, FechaVencimiento)
                            retraso = True
                        End If
                    Next k
                    If retraso = False Then
                        If Dia1Pago <> 999 Then
                            mes = Month(FechaVencimiento)
                            año = Year(FechaVencimiento)
                            If Not IsDate(Dia1Pago & "/" & mes & "/" & año) And (Dia1Pago >= 29) Then
                                If IsDate((Dia1Pago - 1) & "/" & mes & "/" & año) Then
                                    FechaAuxiliar = CDate((Dia1Pago - 1) & "/" & mes & "/" & año)
                                ElseIf IsDate((Dia1Pago - 2) & "/" & mes & "/" & año) Then
                                    FechaAuxiliar = CDate((Dia1Pago - 2) & "/" & mes & "/" & año)
                                ElseIf IsDate((Dia1Pago - 3) & "/" & mes & "/" & año) Then
                                    FechaAuxiliar = CDate((Dia1Pago - 3) & "/" & mes & "/" & año)
                                End If
                            Else
                                FechaAuxiliar = CDate(Dia1Pago & "/" & mes & "/" & año)
                            End If
                            If FechaAuxiliar >= FechaVencimiento Then
                                FechaVencimiento = FechaAuxiliar
                            Else
                                If Dia2Pago <> 999 Then
                                    mes = Month(FechaVencimiento)
                                    año = Year(FechaVencimiento)
                                    If Not IsDate(Dia2Pago & "/" & mes & "/" & año) And (Dia2Pago >= 29) Then
                                        If IsDate((Dia2Pago - 1) & "/" & mes & "/" & año) Then
                                            FechaAuxiliar = CDate((Dia2Pago - 1) & "/" & mes & "/" & año)
                                        ElseIf IsDate((Dia2Pago - 2) & "/" & mes & "/" & año) Then
                                            FechaAuxiliar = CDate((Dia2Pago - 2) & "/" & mes & "/" & año)
                                        ElseIf IsDate((Dia2Pago - 3) & "/" & mes & "/" & año) Then
                                            FechaAuxiliar = CDate((Dia2Pago - 3) & "/" & mes & "/" & año)
                                        End If
                                    Else
                                        FechaAuxiliar = CDate(Dia2Pago & "/" & mes & "/" & año)
                                    End If
                                    If FechaAuxiliar >= FechaVencimiento Then
                                        FechaVencimiento = FechaAuxiliar
                                    Else
                                        If Dia3pago <> 999 Then
                                            mes = Month(FechaVencimiento)
                                            año = Year(FechaVencimiento)
                                            If Not IsDate(Dia3pago & "/" & mes & "/" & año) And (Dia3pago >= 29) Then
                                                If IsDate((Dia3pago - 1) & "/" & mes & "/" & año) Then
                                                    FechaAuxiliar = CDate((Dia3pago - 1) & "/" & mes & "/" & año)
                                                ElseIf IsDate((Dia3pago - 2) & "/" & mes & "/" & año) Then
                                                    FechaAuxiliar = CDate((Dia3pago - 2) & "/" & mes & "/" & año)
                                                ElseIf IsDate((Dia3pago - 3) & "/" & mes & "/" & año) Then
                                                    FechaAuxiliar = CDate((Dia3pago - 3) & "/" & mes & "/" & año)
                                                End If
                                            Else
                                                FechaAuxiliar = CDate(Dia3pago & "/" & mes & "/" & año)
                                            End If
                                            If FechaAuxiliar >= FechaVencimiento Then
                                                FechaVencimiento = FechaAuxiliar
                                            Else
                                                mes = Month(DateAdd("m", 1, FechaVencimiento))
                                                año = Year(DateAdd("m", 1, FechaVencimiento))
                                                If Not IsDate(Dia1Pago & "/" & mes & "/" & año) And (Dia1Pago >= 29) Then
                                                    If IsDate((Dia1Pago - 1) & "/" & mes & "/" & año) Then
                                                        FechaAuxiliar = CDate((Dia1Pago - 1) & "/" & mes & "/" & año)
                                                    ElseIf IsDate((Dia1Pago - 2) & "/" & mes & "/" & año) Then
                                                        FechaAuxiliar = CDate((Dia1Pago - 2) & "/" & mes & "/" & año)
                                                    ElseIf IsDate((Dia1Pago - 3) & "/" & mes & "/" & año) Then
                                                        FechaAuxiliar = CDate((Dia1Pago - 3) & "/" & mes & "/" & año)
                                                    End If
                                                Else
                                                    FechaAuxiliar = CDate(Dia1Pago & "/" & mes & "/" & año)
                                                End If
                                                FechaAuxiliar = CDate(Dia1Pago & "/" & mes & "/" & año)
                                                If FechaAuxiliar > FechaVencimiento Then
                                                    FechaVencimiento = FechaAuxiliar
                                                End If
                                            End If
                                        Else
                                            mes = Month(DateAdd("m", 1, FechaVencimiento))
                                            año = Year(DateAdd("m", 1, FechaVencimiento))
                                            If Not IsDate(Dia1Pago & "/" & mes & "/" & año) Then
                                                mes = mes + 1
                                                Dia1Pago = 1
                                            End If
                                            FechaAuxiliar = CDate(Dia1Pago & "/" & mes & "/" & año)
                                            If FechaAuxiliar > FechaVencimiento Then
                                                FechaVencimiento = FechaAuxiliar
                                            End If
                                        End If
                                    End If
                                Else
                                    mes = Month(DateAdd("m", 1, FechaVencimiento))
                                    año = Year(DateAdd("m", 1, FechaVencimiento))
                                    FechaAuxiliar = CDate(Dia1Pago & "/" & mes & "/" & año)
                                    If FechaAuxiliar > FechaVencimiento Then
                                        FechaVencimiento = FechaAuxiliar
                                    End If
                                End If
                            End If
                        End If
                    Else
                        FechaVencimiento = FechaPago
                    End If
                End If
                ArrayFechaImporteVto(0, i - 1) = FechaVencimiento
                ArrayFechaImporteVto(1, i - 1) = Math.Round(ImporteFactura * ArrayPorcentajes(i - 1) / 100, 2)
                ArrayFechaImporteVto(2, i - 1) = Trim(RsFormasPago!Tipo_Documento)
                If i = RsVencimientos.RecordCount Then
                    ArrayFechaImporteVto(1, i - 1) = Math.Round(ImporteFactura - TotalFacturaAux, 2)
                Else
                    TotalFacturaAux = Math.Round(TotalFacturaAux + ArrayFechaImporteVto(1, i - 1), 2)
                End If
                RsVencimientos.MoveNext()
            Next i
            GenerarVencimientos = True
            Return True
        Catch ex As Exception
            MsgBox("No se ha podido grabar efecto." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + vbCrLf + Trim(Err.Description))
            Return False
        End Try


    End Function
    Public Function PrepararEnlaceContableFacturaDeCompra(RsF As CnTabla.CnTabla, RsE As cnRecordset.CnRecordset) As Boolean
        Dim Puntero As Long

        Puntero = RsE.AbsolutePosition
        ENumeroApuntes = 0
        ReDim EFechas(0)
        ReDim ECuentas(0)
        ReDim EDescripciones(0)
        ReDim EConceptos(0)
        ReDim EDebe(0)
        ReDim EHaber(0)
        ReDim EDocumentos(0)
        ReDim EReferencias(0)
        ReDim ETipos(0)
        ReDim EDiarios(0)
        ReDim EDiscriminanteAsiento(0)

        Try
            RsE.MoveFirst()
            While Not RsE.EOF
                InsertarApunteEnArray(RsF.ValorCampo("fecha_registro"), RsE!Codigo_cuenta, RsE!Descripcion_apunte, RsE!concepto, RsE!Importe_debe, RsE!Importe_haber, RsF.ValorCampo("numero_fra_com"), False, "", RsE!tipo_linea, RsF.ValorCampo("Diario_contab"), 0)
                RsE.MoveNext()
            End While

            If Puntero > -1 Then
                RsE.AbsolutePosition = Puntero
            End If
            Return True
        Catch ex As Exception
            MsgBox("No se puede preparar enlace contable." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + vbCrLf + Trim(Err.Description))
            Return False
        End Try

    End Function
    Private Sub InsertarApunteEnArray(Fecha As Date, Cuenta As String, Descripcion As String, concepto As String, ImporteDebe As Double, ImporteHaber As Double, documento As String, Agrupa As Boolean, ReferenciaAgrupacion As String, TipoLinea As String, Diario As Integer, Discriminante As String) 'NOSTD
        Dim i As Integer

        If ImporteDebe = 0 And ImporteHaber = 0 Then Exit Sub
        If Agrupa = True Then
            Agrupa = False
            For i = 1 To ENumeroApuntes
                If Fecha = EFechas(i) And Trim(Cuenta) = Trim(ECuentas(i)) And Trim(concepto) = Trim(EConceptos(i)) And Trim(TipoLinea) = Trim(ETipos(i)) And EReferencias(i) = Trim(ReferenciaAgrupacion) And EDiarios(i) = Trim(Diario) And Trim(EDiscriminanteAsiento(i)) = Trim(Discriminante) Then
                    EDebe(i) = Math.Round(EDebe(i) + ImporteDebe, 2)
                    EHaber(i) = Math.Round(EHaber(i) + ImporteHaber, 2)
                    Agrupa = True
                End If
            Next
        End If
        If Agrupa = False Then
            ENumeroApuntes = ENumeroApuntes + 1
            ReDim Preserve EFechas(ENumeroApuntes)
            ReDim Preserve ECuentas(ENumeroApuntes)
            ReDim Preserve EDescripciones(ENumeroApuntes)
            ReDim Preserve EConceptos(ENumeroApuntes)
            ReDim Preserve EDebe(ENumeroApuntes)
            ReDim Preserve EHaber(ENumeroApuntes)
            ReDim Preserve EDocumentos(ENumeroApuntes)
            ReDim Preserve EReferencias(ENumeroApuntes)
            ReDim Preserve ETipos(ENumeroApuntes)
            ReDim Preserve EDiarios(ENumeroApuntes)
            ReDim Preserve EDiscriminanteAsiento(ENumeroApuntes)
            EFechas(ENumeroApuntes) = Fecha
            ECuentas(ENumeroApuntes) = Trim(Cuenta)
            EDescripciones(ENumeroApuntes) = Trim(Descripcion)
            EConceptos(ENumeroApuntes) = Trim(concepto)
            EDebe(ENumeroApuntes) = Math.Round(ImporteDebe, 2)
            EHaber(ENumeroApuntes) = Math.Round(ImporteHaber, 2)
            EDocumentos(ENumeroApuntes) = documento
            EReferencias(ENumeroApuntes) = Trim(ReferenciaAgrupacion)
            ETipos(ENumeroApuntes) = Trim(TipoLinea)
            EDiarios(ENumeroApuntes) = Diario
            EDiscriminanteAsiento(ENumeroApuntes) = Trim(CStr(Discriminante))
        End If

    End Sub
    Public Function ComprobarCuadreEnArray() As Boolean
        Dim i As Integer, ID As Double, IH As Double, documento As String

        ID = 0
        IH = 0
        documento = "%&"
        ComprobarCuadreEnArray = True
        For i = 1 To ENumeroApuntes
            If Trim(EDocumentos(i)) = Trim(documento) Then
                ID = ID + Math.Round(EDebe(i), 2)
                IH = IH + Math.Round(EHaber(i), 2)
            Else
                If documento <> "%&" Then
                    If Math.Round(ID, 2) <> Math.Round(IH, 2) Then
                        MsgBox("El documento: " + Trim(documento) + " ha generado un descuadre." + vbCrLf + "Importe DEBE:" + Format(ID, "#,0.00") + vbCrLf + "Importe HABER:" + Format(IH, "#,0.00") + vbCrLf + "El asiento contable no se realizará.")
                        ComprobarCuadreEnArray = False
                    End If
                End If
                ID = Math.Round(EDebe(i), 2)
                IH = Math.Round(EHaber(i), 2)
                documento = Trim(EDocumentos(i))
            End If
        Next
        If documento <> "%&" Then
            If Math.Round(ID, 2) <> Math.Round(IH, 2) Then
                MsgBox("El documento: " + Trim(documento) + " ha generado un descuadre." + vbCrLf + "Importe DEBE:" + Format(ID, "#,0.00") + vbCrLf + "Importe HABER:" + Format(IH, "#,0.00") + vbCrLf + "El asiento contable no se realizará.")
                ComprobarCuadreEnArray = False
            End If
        End If
    End Function
    Public Function GrabacionContabilidadDesdeArray(ByRef MensajeDeRespuesta As String) As String
        Dim i As Long
        Dim j As Long
        Dim ClaveAsiento As String
        Dim FlagExiste As Boolean
        Dim Fecha As Date
        Dim DiariosFechas() As String
        Dim ClavesAsiento() As String
        Dim DebeAsiento() As Double
        Dim HaberAsiento() As Double
        Dim PeriodoAsiento() As Integer
        Dim FechaAsiento() As Date
        Dim Periodo As Long
        Dim NumeroAsiento As Double
        Dim Mensaje As String
        Dim FechaContabilidad As Date
        Dim rsAux As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NumeroEfecto As Long
        Dim trans As SqlClient.SqlTransaction

        GrabacionContabilidadDesdeArray = "Se ha producido un error en la grabación de asientos."
        MensajeDeRespuesta = ""
        ReDim ClavesAsiento(0)
        ReDim DebeAsiento(0)
        ReDim HaberAsiento(0)
        ReDim PeriodoAsiento(0)
        ReDim FechaAsiento(0)

        For i = 1 To ENumeroApuntes
            ClaveAsiento = Format(EDiarios(i), "00000") + Format(EFechas(i), "ddmmyyyy") + Trim(EDiscriminanteAsiento(i))
            FlagExiste = False
            For j = 1 To UBound(ClavesAsiento)
                If Trim(ClaveAsiento) = Trim(ClavesAsiento(j)) Then
                    DebeAsiento(j) = Math.Round(DebeAsiento(j) + EDebe(i), 2)
                    HaberAsiento(j) = Math.Round(HaberAsiento(j) + EHaber(i), 2)
                    FlagExiste = True
                    Exit For
                End If
            Next
            If FlagExiste = False Then
                ReDim Preserve ClavesAsiento(UBound(ClavesAsiento) + 1)
                ReDim Preserve DebeAsiento(UBound(ClavesAsiento) + 1)
                ReDim Preserve HaberAsiento(UBound(ClavesAsiento) + 1)
                ReDim Preserve PeriodoAsiento(UBound(ClavesAsiento) + 1)
                ReDim Preserve FechaAsiento(UBound(ClavesAsiento) + 1)
                ClavesAsiento(UBound(ClavesAsiento)) = Trim(ClaveAsiento)
                DebeAsiento(UBound(ClavesAsiento)) = Math.Round(EDebe(i), 2)
                HaberAsiento(UBound(ClavesAsiento)) = Math.Round(EHaber(i), 2)
            End If
        Next
        For j = 1 To UBound(ClavesAsiento)
            Fecha = CDate(Mid(ClavesAsiento(j), 6, 2) + "/" + Mid(ClavesAsiento(j), 8, 2) + "/" + Mid(ClavesAsiento(j), 10, 4))
            If Math.Round(DebeAsiento(j), 2) <> Math.Round(HaberAsiento(j), 2) Then
                MsgBox("El asiento correspondiente a la fecha " + Format(Fecha, "dd/mm/yyyy") + " no está cuadrado." + vbCrLf + "Importe DEBE :" + CStr(Math.Round(DebeAsiento(j), 2)) + vbCrLf + "Importe HABER :" + Math.Round(HaberAsiento(j), 2) + vbCrLf + "No se realizará ningún asiento en contabilidad.")
                Exit Function
            End If
            Periodo = DevuelvePeriodo(ObjetoGlobal.empresaactual, ObjetoGlobal.EjercicioActual, Fecha)
            If Periodo = -1 Then
                MsgBox("La fecha " + Format(Fecha, "dd/mm/yyyy") + " no corresponde a un periodo válido del ejercicio." + vbCrLf + "No se realizará ningún asiento en contabilidad.")
                Exit Function
            End If
            PeriodoAsiento(j) = Periodo
            FechaAsiento(j) = Fecha
        Next

        Try

            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            For j = 1 To UBound(ClavesAsiento)
                NumeroAsiento = -1
                For i = 1 To ENumeroApuntes
                    ClaveAsiento = Format(EDiarios(i), "00000") + Format(EFechas(i), "ddmmyyyy") + Trim(EDiscriminanteAsiento(i))
                    If Trim(ClaveAsiento) = Trim(ClavesAsiento(j)) Then
                        Mensaje = GrabacionApunteContable(ObjetoGlobal.empresaactual, ObjetoGlobal.EjercicioActual, FechaAsiento(j), EDiarios(i), NumeroAsiento, -1, PeriodoAsiento(j), ECuentas(i), EConceptos(i), EDescripciones(i), EDebe(i), EHaber(i), "N", "N", "N", "", 0, 0, 0, "N", 0, 0, "", FechaAsiento(j), False, trans)
                        If Trim(Mensaje) > "" Then
                            trans.Rollback()
                            ObjetoGlobal.cn.Close()
                            MsgBox("Se ha producido un error en la grabación del asiento contable." + vbCrLf + "No se realizará ningún asiento en contabilidad")
                            Return ""
                        End If
                    End If
                Next
                If UBound(ClavesAsiento) = 1 Then
                    MensajeDeRespuesta = "Se ha grabado correctamente el asiento " + CStr(NumeroAsiento) + ", de fecha " + Format(CDate(FechaAsiento(j)), "dd/mm/yyyy") + "."
                Else
                    MensajeDeRespuesta = "Se han grabado correctamente " + CStr(UBound(ClavesAsiento)) + " asientos."
                End If
            Next
            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return ""

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("Se ha producido un error en la grabación del asiento contable." + vbCrLf + "No se realizará ningún asiento en contabilidad")
            Return ""
        End Try

    End Function
    Public Function GrabacionEfectosDePago(pForm As FrmFacturasProveedor, ByRef MensajeDeRespuesta As String, ByRef Rs As cnRecordset.CnRecordset, ByRef RsF As cnRecordset.CnRecordset) As String
        Dim i As Long, j As Long
        Dim rsAux As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NumeroEfecto As Long, Puntero As Long
        Dim trans As SqlClient.SqlTransaction

        GrabacionEfectosDePago = "Se ha producido un error en la grabación de efectos de pago."
        MensajeDeRespuesta = ""

        Try
            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()

            rsAux.Open("SELECT MAX(NUMERO_EFECTO) AS MAXIMO FROM EFECTOS_PAGO_PROV WHERE EMPRESA='" & Trim(ObjetoGlobal.empresaactual) + "' ", ObjetoGlobal.cn,,,,,,, trans)
            If IsDBNull(rsAux!maximo) Then
                NumeroEfecto = 0
            Else
                NumeroEfecto = rsAux!maximo
            End If
            rsAux.Close()
            Puntero = Rs.AbsolutePosition

            rs1.Open("SELECT * FROM EFECTOS_PAGO_PROV WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            For i = 1 To Rs.RecordCount
                rs1.AddNew()
                rs1!empresa = Trim(RsF!empresa)
                rs1!numero_efecto = NumeroEfecto + i
                rs1!proveedor = "" & RsF!codigo_proveedor
                rs1!tipo_efecto = Trim(Rs!Tipo_Documento)
                rs1!numero_entrada_fra = (pForm.CnTabla01.ValorCampo("numero_entrada_fra"))
                rs1!Diario = RsF!Diario_contab
                rs1!fecha_vencimiento = CDate(Rs!fecha_vencimiento)
                rs1!importe_efecto = Math.Round(Rs!Importe, 2)
                rs1!importe_pagado = 0
                rs1!situacion_efecto = "N"
                rs1.Update()
            Next
            If Puntero > -1 Then rs1.AbsolutePosition = Puntero
            rs1.Close()
            trans.Commit()
            ObjetoGlobal.cn.Close()
            GrabacionEfectosDePago = ""
            Return ""

        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()
            MsgBox("Se ha producido un error en la grabación de los efectos de pago." + vbCrLf + "No se realizará ningún asiento en contabilidad" + vbCrLf + "Anote número de mensaje: " + CStr(MensajeDeRespuesta) + vbCrLf + Trim(Err.Description))
            Return "Se ha producido un error en la grabación de efectos de pago."
        End Try

    End Function
    Public Sub GrabarFacturaComoEnlazada(pform As FrmFacturasProveedor)
        Dim Campos() As String, Valores() As String

        ReDim Campos(0), Valores(0)
        Campos(0) = "enlazado_contab"
        Valores(0) = "S"
        pform.AsignarValores(pform.CnTabla01, Campos, Valores)

    End Sub
    Public Function GrabacionApunteContable(empresa As String, Ejercicio As String, Fecha_asiento As Date, Diario As Integer, Numero_asiento As Double, Posicion As Double, Periodo As Integer, Codigo_cuenta As String, concepto As String, Descripcion_apunte As String, Importe_debe As Double, Importe_haber As Double, punteado As String, Punteado2 As String, Punteado3 As String, Cod_divisa As String, Debe_divisa As Double, Haber_divisa As Double, Cambio_divisa As Double, Anot_contravalor As String, Contravalor_debe As Double, Contravalor_haber As Double, Contrapartida As String, Fecha_valor As Date, Optimista As Boolean, ByRef trans As SqlClient.SqlTransaction) As String 'NOSTD
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCabeceraAsiento As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsApunte As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim ValorClaveApunte As Long
        Dim CompraVenta As String
        Dim Mensaje As String

        Try
            Importe_debe = Math.Round(Importe_debe, 2)
            Importe_haber = Math.Round(Importe_haber, 2)
            Contravalor_debe = Math.Round(Contravalor_debe, ObjetoGlobal.DecimalesDivisaSecundaria)
            Contravalor_haber = Math.Round(Contravalor_haber, ObjetoGlobal.DecimalesDivisaSecundaria)
            If Importe_debe = 0 And Importe_haber = 0 Then
                Return "No se permite introducir apuntes con debe=0 y haber=0"
            End If
            If Trim(Descripcion_apunte) = "" Then
                Return "No se permite introducir apuntes sin descripción"
            End If
            Descripcion_apunte = Left(Trim(Descripcion_apunte), 60)

            Rs.Open("SELECT Isdbnull(MAX(CLAVE_APUNTE),0) FROM ASIENTOS WHERE EMPRESA='" & Trim(empresa) & "'", ObjetoGlobal.cn,,,,,,, trans)
            ValorClaveApunte = Rs(0) + 1
            Rs.Close()
            Rs.Open("SELECT COMPRA_VTA FROM CONCEPTOS WHERE EMPRESA='" & Trim(empresa) & "' AND codigo_concepto='" & Trim(concepto) & "'", ObjetoGlobal.cn,,,,,,, trans)
            If Rs.RecordCount = 0 Then
                Return "No existe el concepto" & Trim(concepto)
            Else
                CompraVenta = IIf("S" = "" & Rs!compra_vta, "S", "N")
            End If
            Rs.Close()

            If Not Optimista Then
                Codigo_cuenta = Trim(CStr(CLng(Codigo_cuenta)))
                If Len(Trim(Codigo_cuenta)) <> ObjetoGlobal.NivelDeCuenta Then
                    Return "El número de dígitos de la cuenta: " & Codigo_cuenta & " no coincide con el nivel contable"
                End If
                Rs.Open("SELECT EMPRESA FROM DIARIOS WHERE EMPRESA='" & Trim(empresa) & "' AND DIARIO=" & CStr(Diario), ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount = 0 Then
                    Return "No existe el diario especificado: " & CStr(Diario)
                End If
                Rs.Close()
                Rs.Open("SELECT EMPRESA FROM PERIODOS WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND PERIODO=" & Periodo & " AND fecha_inicio<= '" & Format(Fecha_asiento, "dd/mm/yyyy") & "' AND fecha_fin>='" & Format(Fecha_asiento, "dd/mm/yyyy") & "'", ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount = 0 Then
                    Return "No existe el periodo: " & Periodo & ", o la fecha " & Format(Fecha_asiento, "dd/mm/yyyy") & " no corresponde al mismo."
                End If
                Rs.Close()
            End If
            If Numero_asiento = -1 Then
                Rs.Open("SELECT MAX(NUMERO_ASIENTO) FROM CABECERAS_ASIENTO WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(Fecha_asiento, "dd/mm/yyyy") & "' AND DIARIO= " & Diario, ObjetoGlobal.cn,,,,,,, trans)
                If IsDBNull(Rs(0)) Then
                    Numero_asiento = 1
                Else
                    Numero_asiento = Rs(0) + 1
                End If
                Rs.Close()

                RsCabeceraAsiento.Open("SELECT * FROM CABECERAS_ASIENTO WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
                RsCabeceraAsiento.AddNew()
                RsCabeceraAsiento!empresa = Trim(empresa)
                RsCabeceraAsiento!Ejercicio = Trim(Ejercicio)
                RsCabeceraAsiento!Fecha_asiento = Fecha_asiento
                RsCabeceraAsiento!Diario = Diario
                RsCabeceraAsiento!Numero_asiento = Numero_asiento
                RsCabeceraAsiento!Importe_debe = Math.Round(Importe_debe, 2)
                RsCabeceraAsiento!Importe_haber = Math.Round(Importe_haber, 2)
                RsCabeceraAsiento!Periodo = Periodo
                RsCabeceraAsiento!usucreacion = ObjetoGlobal.nombreusuario
                RsCabeceraAsiento!fechacreacion = Date.Now
                RsCabeceraAsiento!horacreacion = Date.Now.TimeOfDay
                RsCabeceraAsiento.Update()
            Else
                RsCabeceraAsiento.Open("SELECT * FROM CABECERAS_ASIENTO WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(Fecha_asiento, "dd/mm/yyyy") & "' AND DIARIO= " & Diario & " AND NUMERO_ASIENTO=" & Numero_asiento, ObjetoGlobal.cn, True,,,,,, trans)
                If RsCabeceraAsiento.RecordCount > 0 Then
                    RsCabeceraAsiento!Importe_debe = Math.Round(RsCabeceraAsiento!Importe_debe, 2) + Math.Round(Importe_debe, 2)
                    RsCabeceraAsiento!Importe_haber = Math.Round(RsCabeceraAsiento!Importe_haber, 2) + Math.Round(Importe_haber, 2)
                    RsCabeceraAsiento!USUMODIFICACION = ObjetoGlobal.nombreusuario
                    RsCabeceraAsiento!FECHAMODIFICACION = Now.Date
                    RsCabeceraAsiento!HORAMODIFICACION = Date.Now.TimeOfDay
                    RsCabeceraAsiento.Update()
                Else
                    RsCabeceraAsiento.AddNew()
                    RsCabeceraAsiento!empresa = Trim(empresa)
                    RsCabeceraAsiento!Ejercicio = Trim(Ejercicio)
                    RsCabeceraAsiento!Fecha_asiento = Fecha_asiento
                    RsCabeceraAsiento!Diario = Diario
                    RsCabeceraAsiento!Numero_asiento = Numero_asiento
                    RsCabeceraAsiento!Importe_debe = Math.Round(Importe_debe, 2)
                    RsCabeceraAsiento!Importe_haber = Math.Round(Importe_haber, 2)
                    RsCabeceraAsiento!Periodo = Periodo
                    RsCabeceraAsiento!usucreacion = ObjetoGlobal.nombreusuario
                    RsCabeceraAsiento!fechacreacion = Now.Date
                    RsCabeceraAsiento!horacreacion = Date.Now.TimeOfDay
                End If
            End If
            If Posicion = -1 Then
                Rs.Open("SELECT MAX(POSICION) FROM ASIENTOS WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(Fecha_asiento, "dd/mm/yyyy") & "' AND DIARIO= " & Diario & " AND NUMERO_ASIENTO=" & Numero_asiento, ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount = 0 Then
                    Posicion = 10
                Else
                    If IsDBNull(Rs(0).Value) Then
                        Posicion = 10
                    Else
                        Posicion = Rs(0).Value + 10
                    End If
                End If
                Rs.Close()
            Else
                Rs.Open("SELECT POSICION FROM ASIENTOS WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(Fecha_asiento, "dd/mm/yyyy") & "' AND DIARIO= " & Diario & " AND NUMERO_ASIENTO=" & Numero_asiento & " AND POSICION=" & CStr(Posicion), ObjetoGlobal.cn,,,,,,, trans)
                If Rs.RecordCount > 0 Then
                    Rs.Close()
                    Rs.Open("SELECT MAX(POSICION) FROM ASIENTOS WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND FECHA_ASIENTO= '" & Format(Fecha_asiento, "dd/mm/yyyy") & "' AND DIARIO= " & Diario & " AND NUMERO_ASIENTO=" & Numero_asiento, ObjetoGlobal.cn,,,,,,, trans)
                    Posicion = Rs(0) + 10
                    Rs.Close()
                End If
            End If
            Rs.Open("SELECT MAX(CLAVE_APUNTE) FROM ASIENTOS WHERE EMPRESA='" & Trim(empresa) & "'", ObjetoGlobal.cn,,,,,,, trans)
            If Rs.RecordCount > 0 Then
                If IsDBNull(Rs(0)) = True Then
                    ValorClaveApunte = 1
                Else
                    ValorClaveApunte = Rs(0) + 1
                End If
            Else
                ValorClaveApunte = 1
            End If
            Rs.Close()
            RsApunte.Open("SELECT * FROM ASIENTOS WHERE 1=0", ObjetoGlobal.cn, True,,,,,, trans)
            RsApunte.AddNew()
            RsApunte!empresa = Trim(empresa)
            RsApunte!Ejercicio = Trim(Ejercicio)
            RsApunte!Fecha_asiento = CDate(Fecha_asiento)
            RsApunte!Diario = Diario
            RsApunte!Numero_asiento = Numero_asiento
            RsApunte!Posicion = Posicion
            RsApunte!Periodo = Periodo
            RsApunte!Codigo_cuenta = Trim(Codigo_cuenta)
            RsApunte!concepto = Trim(concepto)
            RsApunte!Descripcion_apunte = Trim(Descripcion_apunte)
            RsApunte!Importe_debe = Math.Round(Importe_debe, 2)
            RsApunte!Importe_haber = Math.Round(Importe_haber, 2)
            RsApunte!clave_apunte = ValorClaveApunte
            RsApunte!fecha_act = Date.Now
            RsApunte!usuario_act = ObjetoGlobal.nombreusuario
            RsApunte!tipo_act = "C"
            RsApunte!punteado = "N"
            RsApunte!Punteado2 = "N"
            RsApunte!Punteado3 = "N"
            If Trim(punteado) = "S" Then
                RsApunte!punteado = Trim(punteado)
            End If
            If Trim(Punteado2) = "S" Then
                RsApunte!Punteado2 = Trim(Punteado2)
            End If
            If Trim(Punteado3) = "S" Then
                RsApunte!Punteado3 = Trim(Punteado3)
            End If
            If Trim(Cod_divisa) <> "" Then
                RsApunte!anot_divisa = "S"
                RsApunte!Cod_divisa = Trim(Cod_divisa)
                RsApunte!Debe_divisa = Debe_divisa
                RsApunte!Haber_divisa = Haber_divisa
                RsApunte!Cambio_divisa = Cambio_divisa
            Else
                RsApunte!anot_divisa = "N"
                RsApunte!Debe_divisa = 0
                RsApunte!Haber_divisa = 0
                RsApunte!Cod_divisa = ""
                RsApunte!Cambio_divisa = 0
            End If
            RsApunte!Anot_contravalor = "N"
            If Trim(Anot_contravalor) = "S" Then
                RsApunte!Anot_contravalor = "S"
                RsApunte!Contravalor_debe = Contravalor_debe
                RsApunte!Contravalor_haber = Contravalor_haber
            Else
                RsApunte!Contravalor_debe = 0
                RsApunte!Contravalor_haber = 0
            End If
            RsApunte!compra_vta = "N"
            If CompraVenta = "S" Then
                RsApunte!compra_vta = "S"
            End If
            If Trim(Contrapartida) <> "" Then RsApunte!Contrapartida = Trim(Contrapartida)
            If Trim(Fecha_valor) <> "" Then RsApunte!Fecha_valor = CDate(Fecha_valor)
            RsApunte!enviado_concili = "N"
            'RsApunte!f_envio_concili = Null
            RsApunte!enviado_tesoreria = "N"
            'RsApunte!f_envio_tesoreria = Null
            RsApunte!usucreacion = ObjetoGlobal.nombreusuario
            RsApunte!fechacreacion = Date.Now
            RsApunte!horacreacion = TimeOfDay
            If RsApunte!anot_divisa = "S" Then
                Mensaje = GrabacionSaldos(RsApunte!empresa, RsApunte!Ejercicio, RsApunte!Codigo_cuenta, RsApunte!Diario, RsApunte!Periodo, RsApunte!Debe_divisa, RsApunte!Haber_divisa, False, "" & RsApunte!Cod_divisa, False, trans)
                If Trim(Mensaje) > "" Then
                    RsApunte.Close()
                    Return "Error en la grabación de saldos divisa. " + vbCrLf + Trim(Mensaje)
                End If
            End If
            Mensaje = GrabacionSaldos(RsApunte!empresa, RsApunte!Ejercicio, RsApunte!Codigo_cuenta, RsApunte!Diario, RsApunte!Periodo, RsApunte!Importe_debe, RsApunte!Importe_haber, IIf(RsApunte!compra_vta = "S", True, False), ObjetoGlobal.DivisaActual, True, trans)
            If Trim(Mensaje) > "" Then
                RsApunte.Close()
                Return "Error en la grabación de saldos. " + vbCrLf + Trim(Mensaje)
            End If
            RsApunte.Update()
            RsApunte.Close()
            Return ""
        Catch ex As Exception
            Return "Error en grabación asiento fecha: " + Format(Fecha_asiento, "dd/mm/yyyy")
        End Try

    End Function
    Private Function DevuelvePeriodo(empresa As String, Ejercicio As String, Fecha As Date) As Long 'NOSTD
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, Sql As String

        DevuelvePeriodo = -1
        Sql = "SELECT * FROM PERIODOS WHERE EMPRESA = '" + Trim(empresa) + "' AND EJERCICIO = '" + Trim(Ejercicio) + "' AND PERIODO>100 ORDER BY EMPRESA,EJERCICIO,PERIODO"

        Rs.Open(Sql, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            Rs.MoveFirst()

            While Rs.EOF = False
                If Fecha >= Rs!fecha_inicio And Fecha <= Rs!fecha_fin Then
                    DevuelvePeriodo = Rs!Periodo
                    Exit While
                End If
                Rs.MoveNext()
            End While
        End If
    End Function
    Private Function GrabacionSaldos(empresa As String, Ejercicio As String, Cuenta As String, Diario As Integer, Periodo As Integer, ImporteDebe As Double, ImporteHaber As Double, CompraVenta As Boolean, Divisa As String, GrabaMayores As Boolean, ByRef trans As SqlClient.SqlTransaction) As String 'NOSTD
        Dim i As Integer, Nivel As Integer
        Dim NivelAComprobar As Integer, CuentaABuscar As String
        Dim rsSaldos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, Sql As String

        Try
            GrabacionSaldos = ""
            NivelAComprobar = 0
            Nivel = Len(Trim(Cuenta))
            For i = Nivel To ObjetoGlobal.NivelDeSaldos Step -1
                If ObjetoGlobal.NivelesDisponibles(i) Then
                    NivelAComprobar = i
                    CuentaABuscar = Left$(Trim(Cuenta), NivelAComprobar)
                    rsSaldos.Open("SELECT * FROM SALDOS WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND CODIGO_CUENTA='" & Trim(CuentaABuscar) & "' AND DIARIO=" & Trim(Diario) & " AND PERIODO=" & Trim(Periodo) & " AND CODIGO_DIVISA='" & Trim(Divisa) & "'", ObjetoGlobal.cn, True,,,,,, trans)
                    If rsSaldos.RecordCount > 0 Then
                        Sql = "UPDATE SALDOS SET DEBE_PERIODO=" & NumeroParaSQL(Math.Round(rsSaldos!debe_periodo, 2) + Math.Round(ImporteDebe, 2), 2) & ", HABER_PERIODO=" & NumeroParaSQL(Math.Round(rsSaldos!haber_periodo, 2) + Math.Round(ImporteHaber, 2), 2)
                        If CompraVenta And i = ObjetoGlobal.NivelDeCuenta Then
                            Sql = Trim(Sql) + " , SALDO_CV_PERIODO=" & NumeroParaSQL(Math.Round(rsSaldos!saldo_cv_periodo, 2) + Math.Round(ImporteDebe, 2) - Math.Round(ImporteHaber, 2), 2)
                        End If
                        Sql = Trim(Sql) & " WHERE EMPRESA='" & Trim(empresa) & "' AND EJERCICIO='" & Trim(Ejercicio) & "' AND CODIGO_CUENTA='" & Trim(CuentaABuscar) & "' AND DIARIO=" & Trim(Diario) & " AND PERIODO=" & Trim(Periodo) & " AND CODIGO_DIVISA='" & Trim(Divisa) & "'"
                        ObjetoGlobal.cn.Execute(Sql)
                    Else
                        If CompraVenta And i = ObjetoGlobal.NivelDeCuenta Then
                            Sql = "INSERT INTO SALDOS (EMPRESA, EJERCICIO, CODIGO_CUENTA, DIARIO, PERIODO, CODIGO_DIVISA, DEBE_PERIODO, HABER_PERIODO, DIGITOS, CTA_NUMERICA," &
                "SALDO_CV_PERIODO) VALUES('" & Trim(empresa) & "', '" & Trim(Ejercicio) & "', '" & Trim(CuentaABuscar) & "', " & Val(Diario) & ", " & Val(Periodo) &
                ", '" & Trim(Divisa) & "', " & NumeroParaSQL(Math.Round(ImporteDebe, 2), 2) & ", " & NumeroParaSQL(Math.Round(ImporteHaber, 2), 2) & ", " & NivelAComprobar &
                ", " & Val(Trim(CuentaABuscar)) & ", " & NumeroParaSQL(Math.Round(ImporteDebe, 2) - Math.Round(ImporteHaber, 2), 2) & ")"
                            ObjetoGlobal.cn.Execute(Sql)
                        Else
                            Sql = "INSERT INTO SALDOS (EMPRESA, EJERCICIO, CODIGO_CUENTA, DIARIO, PERIODO, CODIGO_DIVISA, DEBE_PERIODO, HABER_PERIODO, DIGITOS, CTA_NUMERICA," &
                "SALDO_CV_PERIODO) VALUES('" & Trim(empresa) & "', '" & Trim(Ejercicio) & "', '" & Trim(CuentaABuscar) & "', " & Val(Diario) & ", " & Val(Periodo) &
                ", '" & Trim(Divisa) & "', " & NumeroParaSQL(Math.Round(ImporteDebe, 2), 2) & ", " & NumeroParaSQL(Math.Round(ImporteHaber, 2), 2) & ", " & NivelAComprobar &
                ", " & Val(Trim(CuentaABuscar)) & ", 0)"
                            ObjetoGlobal.cn.Execute(Sql)
                        End If
                    End If
                    rsSaldos.Close()
                End If
                If Not GrabaMayores Then Exit For
            Next
            Return ""
        Catch ex As Exception
            Return "Error en grabación saldos asiento cuenta '" + CStr(Cuenta) + "'"
        End Try

    End Function
    Public Function BuscaAsigBloqueLiquid(Tipo_compra As String, Fecha As Date) As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        BuscaAsigBloqueLiquid = ""

        Rs.Open("SELECT BLOQUE_LIQUID FROM ASIG_BLOQUES_COM WHERE EMPRESA='" & ObjetoGlobal.empresaactual & "' AND TIPO_COMPRA='" & Trim(Tipo_compra) & "' AND FECHA_INICIO<='" & CDate(Fecha) & "' AND FECHA_FIN>='" & CDate(Fecha) & "'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            BuscaAsigBloqueLiquid = Trim(Rs!bloque_liquid)
        End If
        Rs.Close()
    End Function
    Public Function CalculaContadorLiquid(bloque_liquid As String) As Long
        Dim RsContGest As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsBloqLiqCom As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Contador As Long

        RsContGest.Open("SELECT * FROM CONTADORES_GESTION WHERE EMPRESA='" & ObjetoGlobal.empresaactual & "' AND NOMBRE_CONTADOR='" & Trim(bloque_liquid) & "'", ObjetoGlobal.cn, True)
        If RsContGest.RecordCount > 0 Then
            Contador = CLng(RsContGest!Contador) + 1
            RsContGest!Contador = CLng(RsContGest!Contador) + 1
            RsContGest.Update()
        Else

            RsBloqLiqCom.Open("SELECT CONTADOR_INICIAL FROM BLOQUES_LIQUID_COM WHERE EMPRESA='" & ObjetoGlobal.empresaactual & "' AND BLOQUE_LIQUID='" & Trim(bloque_liquid) & "' AND ACTIVO_SN='S'", ObjetoGlobal.cn)
            If RsBloqLiqCom.RecordCount > 0 Then
                Contador = CLng(RsBloqLiqCom!contador_inicial)
                RsContGest.AddNew()
                RsContGest!empresa = ObjetoGlobal.empresaactual
                RsContGest!Ejercicio = ObjetoGlobal.EjercicioActual
                RsContGest!nombre_contador = Trim(bloque_liquid)
                RsContGest!serie_contador = ""
                RsContGest!Contador = CLng(RsBloqLiqCom!contador_inicial)
                RsContGest.Update()
            End If
            RsBloqLiqCom.Close()
        End If
        RsContGest.Close()
        CalculaContadorLiquid = Contador
    End Function

    Public Function NumeroParaSQL(Num, Decimales) As String
        Dim Cad As String, Formato As String

        Formato = "#0"
        If Decimales > 0 Then Formato = Trim(Formato) + "." + StrDup(Decimales, "#")
        NumeroParaSQL = Format(Num, Formato)
        Return Replace(Trim(NumeroParaSQL), ",", ".")

    End Function

    'Public Sub EsperarShell(sCmd As String) 'NOSTD
    '    Dim hShell As Long, hProc As Long, codExit As Long

    '    ' ejecutar comando
    '    hShell = Shell(Environ$("Comspec") & " /c " & sCmd, 2)
    '    ' esperar a que se complete el proceso
    '    hProc = OpenProcess(PROCESS_QUERY_INFORMATION, False, hShell)
    '    Do
    '        GetExitCodeProcess hProc, codExit
    'DoEvents
    '    Loop While codExit = STILL_ACTIVE
    'End Sub

    Function CrearUnaInstancia(Formulario As String) As Object
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, proyecto As String, clase As String, cadenaclase As String

        clase = "Cls" + Mid(Formulario, 4)

        Rs.Open("select * from ZZDISTRIBUIR where formulario='" & UCase(Formulario) & "'", ObjetoGlobal.cn, True)
        If Rs.RecordCount > 0 Then
            proyecto = Trim(Rs!proyecto)
            cadenaclase = proyecto & "." & clase
            Return CreateObject(cadenaclase)
        Else
            Return Nothing
        End If
    End Function
    Function ConstruyeCadenaWhere01(ParamArray Linea()) As String

        Dim npar As Integer, TxtWhere As String, NombreCampo As String, TipoCampo As String, valorcampo As String
        Dim separacion As String, i As Integer
        Dim b, Tipo

        For Each b In Linea
            npar = npar + 1
        Next
        TxtWhere = ""
        For i = 0 To npar - 1 Step 3
            NombreCampo = Linea(i)
            Tipo = Linea(i + 1)
            valorcampo = Linea(i + 2)
            If valorcampo <> "" Then
                Select Case LCase(Tipo)
                    Case "t"
                        separacion = "'"
                    Case "d"
                        separacion = ObjetoGlobal.AccesoFechas
                    Case Else
                        separacion = ""
                End Select
                'separacion = IIf(LCase(tipo) = "t" Or LCase(tipo) = "d", "'", "")
                TxtWhere = TxtWhere + NombreCampo + "=" + separacion + valorcampo + separacion + " and "
            End If
        Next
        If TxtWhere <> "" Then
            TxtWhere = Left(TxtWhere, Len(TxtWhere) - 5)
        End If

        ConstruyeCadenaWhere01 = TxtWhere
    End Function
    Function TablaParametros(proceso As String, Campo As String, Optional valor_defecto As String = "")
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If valor_defecto = "" Then
            TablaParametros = ""
        Else
            TablaParametros = valor_defecto
        End If

        Rs.Open("SELECT VALOR_DEFECTO FROM TABLA_PARAMETROS WHERE EMPRESA='" & ObjetoGlobal.empresaactual &
      "' AND PROCESO='" & proceso & "' AND CAMPO='" & Campo & "'", ObjetoGlobal.cn)

        If Rs.RecordCount > 0 Then TablaParametros = "" & Trim(Rs!valor_defecto)
    End Function

End Module
