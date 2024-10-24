Imports System.ComponentModel

Public Class FrmFacturasProveedor
    Inherits LibreriaModeloMantenimiento.ModeloMantenimiento
    Private TipoMantenimiento(10) As String
    Private NumeroDatosFijos(10) As Integer
    Private ValoresFijos(10, 10) As String
    Private CamposFijos(10, 10) As String
    Private aCuentaTipoIVA() As String
    Private aCuentas() As String
    Private aDescripcion() As String
    Private aImporte() As Double
    Private aTipoIva() As Double
    Private nLineas As Integer
    Private bSoloCancelacionAlbaranes As Boolean
    Public Xnumero_lineas As Integer
    Public Xdetalle_factura() As String
    Public tipo_retencion As Double
    Public base_retencion As Double

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        Dim i As Integer
        Dim cEntrada As String
        Dim cLineaEntrada As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nTotal As Double
        Dim cNumero As String
        Dim cLinea As String
        Dim sSql As String

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("El formulario no debe de estar realizando ninguna acción.")
            Return
        End If

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        If Trim(CnTabla01.ValorCampo("enlazado_contab")) <> "S" Then
            MsgBox("Esta opción solo es válida para facturas enlazadas en contabilidad")
            Return
        End If

        Xnumero_lineas = 0
        ReDim Xdetalle_factura(0)

        ' Ya tiene líneas, es una modificación
        bSoloCancelacionAlbaranes = True

        If IsDBNull(CnTabla01.ValorCampo("numero_entrada_fra")) Then 'Or (CnTabla01.Rs.BOF Or CnTabla01.Rs.EOF) Then
            MsgBox("Debe seleccionar previamente factura de compra.")
            Return
        End If

        Dim Frm As FrmSeleccionLineaAlbaran = New FrmSeleccionLineaAlbaran

        Frm.ObjetoGlobal = Me.ObjetoGlobal
        Frm.codigo_proveedor = CnTabla01.ValorCampo("codigo_proveedor")
        Frm.ObjetoGlobal = Me.ObjetoGlobal
        If Frm.ShowDialog() = DialogResult.OK Then
            ReDim aCuentas(0)
            ReDim aDescripcion(0)
            ReDim aTipoIva(0)
            ReDim aImporte(0)
            ReDim aCuentaTipoIVA(0)
            nLineas = 0

            If Xnumero_lineas > 0 Then
                For i = 1 To Xnumero_lineas
                    cEntrada = Mid(Xdetalle_factura(i), 1, 10)
                    cLineaEntrada = Mid(Xdetalle_factura(i), 11, 5)
                    SumaLinea(cEntrada, cLineaEntrada)
                Next
            End If
            ' Solo queremos grabar los albaranes
            nTotal = Frm.TotalSeleccion

            'For i = 0 To UBound(aImporte)
            '    nTotal = nTotal + aImporte(i)
            '    nTotal = nTotal + Round((aImporte(i) * aTipoIva(i) / 100), 2)
            'Next
            If CDbl(nTotal) = 0 Then
                If MsgBox("No aparecen totales o es 0 ¿es correcto?", vbYesNo, "Cancelación albaranes") = vbNo Then
                    Return
                End If
            ElseIf CDbl(nTotal) <> CDbl(TxtTotalFactura1.Text) Then
                If MsgBox("No coinciden las cantidades seleccionadas con el importe de la factura" + Chr(13) + Chr(10) + "¿Continuamos proceso de actualización?", vbYesNo, "Cancelación albaranes") <> vbYes Then
                    Return
                End If
            Else
                If MsgBox("¿Marcamos los albaranes como facturados?", vbYesNo, "Cancelación albaranes") = vbNo Then
                    Return
                End If
            End If

            If Xnumero_lineas > 0 Then
                For i = 1 To Xnumero_lineas
                    cNumero = Mid(Xdetalle_factura(i), 1, 10)
                    cLinea = Mid(Xdetalle_factura(i), 11, 5)
                    sSql = "SELECT * FROM albaran_com_l WHERE EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and numero_entrada=" & cNumero & " and linea_albaran =" & cLinea
                    RsAlb.Open(sSql, ObjetoGlobal.cn, True)
                    If RsAlb.RecordCount > 0 Then
                        RsAlb!Situacion = "F"
                        RsAlb!numero_entrada_fra = Rs!numero_entrada_fra
                        RsAlb.Update()
                    End If
                    RsAlb.Close()
                Next
            End If
        End If

    End Sub

    Private Sub importalineas_Click(sender As Object, e As EventArgs) Handles importalineas.Click
        Dim i As Integer
        Dim cEntrada As String
        Dim cLineaEntrada As String
        Dim Rs As Object
        Dim RsCtas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("El formulario no debe de estar realizando ninguna acción.")
            Return
        End If

        Rs = CnTabla01.DevuelveRecordset() 'CnTabla01.Rs(0)

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        If Trim(Rs!enlazado_contab) = "S" Then
            MsgBox("Esta opción solo es válida para facturas no enlazadas en contabilidad")
            Exit Sub
        End If

        RsCtas = CnTabla02.DevuelveRecorsetGrid

        Xnumero_lineas = 0
        ReDim Xdetalle_factura(0)

        If IsDBNull(Rs!numero_entrada_fra) Or (Rs.BOF Or Rs.EOF) Then
            MsgBox("Debe seleccionar previamente factura de compra.")
            Exit Sub
        End If

        Dim frm As FrmSeleccionLineaAlbaran = New FrmSeleccionLineaAlbaran

        frm.ObjetoGlobal = Me.ObjetoGlobal
        frm.codigo_proveedor = CnTabla01.ValorCampo("codigo_proveedor")
        If frm.ShowDialog = DialogResult.OK Then
            ReDim aCuentas(0)
            ReDim aDescripcion(0)
            ReDim aTipoIva(0)
            ReDim aImporte(0)
            ReDim aCuentaTipoIVA(0)
            nLineas = 0

            If Xnumero_lineas > 0 Then
                For i = 1 To Xnumero_lineas
                    cEntrada = Mid(Xdetalle_factura(i), 1, 10)
                    cLineaEntrada = Mid(Xdetalle_factura(i), 11, 5)
                    SumaLinea(cEntrada, cLineaEntrada)
                Next
            End If
            Grabalineas()
            CnTabla02.SeleccionAdicional = "EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND PROCESO =" & CLng(Rs!numero_entrada_fra)
            CnTabla02.EstablecerSeleccion()
            CnTabla_CTAceptar(CnTabla02)
            CnTabla02.SeleccionAdicional = ""
        End If

    End Sub

    Private Sub CmdGenerarDatos_Click(sender As Object, e As EventArgs) Handles CmdGenerarDatos.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If SituacionFormulario <> Estados.Inactivo Then
            'Set Rs = ControlTabla(0).Rs(1)

            If CnTabla01.cuantos = 0 Then
                Return
            End If
            Rs = CnTabla01.DevuelveRecordset()
            If IsDBNull(Rs!numero_entrada_fra) Then
                MsgBox("Debe seleccionar previamente factura de compra.")
            Else
                tipo_retencion = 0
                base_retencion = 0
                If SeRetiene.Checked Then 'Hay que aplicar retención
                    Dim oFrm As FrmRetenciones = New FrmRetenciones
                    If oFrm.ShowDialog = DialogResult.Yes Then
                        base_retencion = oFrm.BaseRetencion
                        tipo_retencion = oFrm.TipoRetencíon
                    End If
                End If
                If GenerarDatosFacturaCompra(Me) = False Then
                    MsgBox("No se ha generado información de la factura de compra.")
                Else
                    MsgBox("Se ha generado correctamente información para la factura de compra.")
                    CnTabla01.MostrarRegistroActual(True)
                End If
            End If
        End If

    End Sub

    Private Sub CmdEnlaceContable_Click(sender As Object, e As EventArgs) Handles CmdEnlaceContable.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs3 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Mensaje As String = ""
        Dim Mensaje2 As String = ""

        Rs = CnTabla01.Rs(0)
        Rs2 = CnTabla04.DevuelveRecorsetGrid  ' factura_com_c_enl
        Rs3 = CnTabla05.DevuelveRecorsetGrid

        If IsDBNull(Rs!numero_entrada_fra) Then
            MsgBox("Debe seleccionar previamente factura de compra.")
            Exit Sub
        End If
        If Trim(Rs!enlazado_contab) = "S" Then
            MsgBox("Esta factura ya está contabilizada.")
            Exit Sub
        End If
        If SituacionFormulario <> Estados.Inactivo Then

            If PrepararEnlaceContableFacturaDeCompra(CnTabla01, Rs2) = False Then
                MsgBox("No se ha enlazado la factura a contabilidad.")
            Else
                If ComprobarCuadreEnArray() = False Then
                    MsgBox("No se ha enlazado la factura a contabilidad.")
                Else
                    If GrabacionContabilidadDesdeArray(Mensaje) > "" Then
                        MsgBox("No se ha enlazado la factura a contabilidad.")
                    Else
                        If GrabacionEfectosDePago(Me, Mensaje2, Rs3, Rs) > "" Then
                            MsgBox(Trim(Mensaje) + vbCrLf + "Atención: NO SE HA GRABADO EL EFECTO DE PAGO. Se ha obtenido el siguiente mensaje:" + vbCrLf + Trim(Mensaje2))
                        Else
                            GrabarFacturaComoEnlazada(Me)
                            CnTabla01.MostrarRegistroActual(True)
                            MsgBox(Trim(Mensaje) + vbCrLf + Trim(Mensaje2))
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CmdSalir_Click_1(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub



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

    'Protected Overrides Function HOOK_Antes_de_Crear_Ficha(CT As CnTabla.CnTabla) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio2" Then
    ' ReDim Preserve Campos(1), Valores(1)
    ' Campos(0) = "alfa_no" : Valores(0) = "Alfa_NUEVO"
    ' Campos(1) = "entero_si" : Valores(1) = "246"
    ' HOOK_AsignarValores(CT, Campos, Valores)
    ' End If


    ' HOOK_Antes_de_Crear_Ficha = False
    'End Function


    'Public Overrides Function HOOK_Antes_de_Crear_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
    ' Dim Campos() As String, Valores() As String

    ' If CT.Estado = CnTabla.CnTabla.EstadoCnTabla.Creando And Trim(CT.Tabla) = "test_serio" Then
    ' ReDim Preserve Campos(1), Valores(1)
    ' Campos(0) = "alfa_no" : Valores(0) = "Alfa_NUEVO"
    ' Campos(1) = "entero_si" : Valores(1) = "246"
    ' HOOK_AsignarValores(CT, Campos, Valores, GE, HH)
    ' End If

    ' HOOK_Antes_de_Crear_Grid = False
    'End Function


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

    Public Function AsignarValores(CT As CnTabla.CnTabla, Campos() As String, Valores() As String)
        HOOK_AsignarValores(CT, Campos, Valores)
    End Function

    Private Sub SumaLinea(cEntrada, cLinea)
        Dim i As Integer
        Dim Rss As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cArt As String
        Dim cCta As String
        Dim cDes As String
        Dim nImp As Double
        Dim nTipoIVA As Double
        Dim Sql As String
        Dim sSqlArt As String
        Dim RsAr As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCtas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Sql = "SELECT * FROM albaran_com_l WHERE EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and numero_entrada=" & cEntrada & " and linea_albaran =" & cLinea
        Rss.Open(Sql, ObjetoGlobal.cn)
        If Rss.RecordCount > 0 Then
            cArt = Rss!articulo
            cDes = Rss!Descripcion
            nTipoIVA = Rss!tipo_iva
            nImp = Rss!Importe
            sSqlArt = "SELECT articulos.Familia_articulo, familias_articulos.cuenta FROM articulos, familias_articulos WHERE familias_articulos.EMPRESA=articulos.EMPRESA AND familias_articulos.codigo_familia=articulos.familia_articulo AND articulos.EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and codigo_articulo ='" & Trim(cArt) & "'"
            RsAr.Open(sSqlArt, ObjetoGlobal.cn)
            If RsAr.RecordCount > 0 Then
                cCta = RsAr!Cuenta
                RsCtas.Open("select * from cuentas where empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' and codigo_cuenta='" & Trim(cCta) & "'", ObjetoGlobal.cn)
                If RsCtas.RecordCount > 0 Then
                    AgrupaLineas(cCta, RsCtas!descripcion_cuenta, nTipoIVA, nImp)
                End If
                RsCtas.Close
            End If
            RsAr.Close
        End If
        Rss.Close
    End Sub

    Private Sub Grabalineas()
        Dim Rs2 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPorc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim sSqlIVA As String

        If IsDBNull(CnTabla01.ValorCampo("tabla_iva_prov")) Then
            MsgBox("Debe seleccionar previamente factura de compra.")
            Return
        End If

        Rs2.Open("SELECT * FROM TEMP_FACTURA_COMPRA WHERE 1=0", ObjetoGlobal.cn, True)
        For i = 0 To nLineas - 1
            Rs2.AddNew()
            Rs2!empresa = Trim(ObjetoGlobal.EmpresaActual)
            Rs2!proceso = CnTabla01.ValorCampo("numero_entrada_fra")
            Rs2!Contador = (i + 1)
            Rs2!Cuenta = aCuentas(i)
            Rs2!Descripcion = aDescripcion(i)

            sSqlIVA = "SELECT * FROM porc_iva_soportado WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND tabla_iva_prov='" & CnTabla01.ValorCampo("tabla_iva_prov") & "' AND tipo_iva=" & aTipoIva(i)
            RsPorc.Open(sSqlIVA, ObjetoGlobal.cn)
            Rs2!tipo_iva = aTipoIva(i)
            Rs2!Porcentaje = RsPorc!porcentaje_iva
            Rs2!Importe = aImporte(i)
            RsPorc.Close()
            Rs2.Update()
        Next
        Rs2.Close

    End Sub
    Private Sub AgrupaLineas(cCta, cDes, nTipIva, nImporte)
        Dim bSuma As Boolean
        Dim i As Integer

        If nLineas = 0 Then
            nLineas = nLineas + 1
            ReDim Preserve aCuentas(nLineas)
            ReDim Preserve aDescripcion(nLineas)
            ReDim Preserve aTipoIva(nLineas)
            ReDim Preserve aImporte(nLineas)
            ReDim Preserve aCuentaTipoIVA(nLineas)

            aCuentas(nLineas - 1) = cCta
            aDescripcion(nLineas - 1) = cDes
            aTipoIva(nLineas - 1) = nTipIva
            aCuentaTipoIVA(nLineas - 1) = "" & nTipIva & cCta
            'aImporte(nLineas - 1) = nImporte
        End If
        bSuma = False
        For i = 0 To nLineas - 1
            If aCuentaTipoIVA(i) = ("" & nTipIva & cCta) Then
                aImporte(i) = aImporte(i) + nImporte
                bSuma = True
                Exit For
            End If
        Next

        If Not bSuma Then
            nLineas = nLineas + 1
            ReDim Preserve aCuentas(nLineas)
            ReDim Preserve aDescripcion(nLineas)
            ReDim Preserve aTipoIva(nLineas)
            ReDim Preserve aImporte(nLineas)
            ReDim Preserve aCuentaTipoIVA(nLineas)
            aCuentas(nLineas - 1) = cCta
            aDescripcion(nLineas - 1) = cDes
            aTipoIva(nLineas - 1) = nTipIva
            aImporte(nLineas - 1) = nImporte
            aCuentaTipoIVA(nLineas - 1) = "" & nTipIva & cCta
        End If

    End Sub

    Private Sub CmdCancelarAlbaranes_Click(sender As Object, e As EventArgs) Handles CmdCancelarAlbaranes.Click
        Dim i As Integer
        Dim cNumero As String
        Dim cLinea As String
        Dim sSql As String
        Dim RsAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cObservaciones As String
        Dim bSalirWhile As Boolean
        Dim pForm As FrmSeleccionLineaAlbaran = New FrmSeleccionLineaAlbaran

        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("El formulario no debe de estar realizando ninguna acción.")
            Exit Sub
        End If

        Xnumero_lineas = 0
        ReDim Xdetalle_factura(0)

        ' Ya tiene líneas, es una modificación
        bSoloCancelacionAlbaranes = True

        pForm.ObjetoGlobal = Me.ObjetoGlobal
        'pForm.codigo_proveedor = CnTabla01.ValorCampo("codigo_proveedor")
        If pForm.ShowDialog() = DialogResult.OK Then
            nLineas = 0

            If Xnumero_lineas = 0 Then
                MsgBox("No seleccionó ningún albarán")
                Exit Sub
            End If

            cObservaciones = ""

            bSalirWhile = False

            While Not bSalirWhile
                cObservaciones = InputBox("Introduzca observaciones de marcado", "Observaciones", cObservaciones)
                If Len(Trim(cObservaciones)) <= 60 Then
                    bSalirWhile = True
                Else
                    MsgBox("Máximo 60 caracteres")
                End If
            End While

            If MsgBox("¿Marcamos los albaranes como facturados?", vbYesNo, "Cancelación albaranes") = vbNo Then
                Exit Sub
            End If

            If Xnumero_lineas > 0 Then
                For i = 1 To Xnumero_lineas
                    cNumero = Mid(Xdetalle_factura(i), 1, 10)
                    cLinea = Mid(Xdetalle_factura(i), 11, 5)
                    sSql = "SELECT * FROM albaran_com_l WHERE EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' and numero_entrada=" & cNumero & " and linea_albaran =" & cLinea
                    RsAlb.Open(sSql, ObjetoGlobal.cn, True)
                    If RsAlb.RecordCount > 0 Then
                        RsAlb!Situacion = "F"
                        RsAlb!Observaciones2 = "" & Trim(cObservaciones)
                        RsAlb.Update()
                    End If
                    RsAlb.Close()
                Next
            End If
        End If

    End Sub

    Private Sub CmdEscaner_Click(sender As Object, e As EventArgs) Handles CmdEscaner.Click


        If SituacionFormulario <> Estados.Inactivo Then
            MsgBox("El formulario no debe de estar realizando ninguna acción.")
            Return
        End If
        If CnTabla01.cuantos > 0 Then
            Dim pFrm As Escanerdocumentos.Escaneadocumento = New Escanerdocumentos.Escaneadocumento
            pFrm.numero_entrada = CnTabla01.ValorCampo("numero_entrada_fra")
            pFrm.ObjetoGlobal = Me.ObjetoGlobal
            pFrm.ShowDialog()
        End If

    End Sub
End Class

