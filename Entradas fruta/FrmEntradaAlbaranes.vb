Imports System.ComponentModel

Public Class FrmEntradaAlbaranes
    Inherits LibreriaModeloMantenimiento.ModeloMantenimiento
    Dim Labels1(60) As Label
    Dim LblCajas(60) As Label
    Dim LblVolcado(60) As PictureBox
    Dim LblEnvaseEntrada(6) As Label
    Dim lblCuantosEntrada(6) As Label
    Dim lblEnvaseSalida(6) As Label
    Dim lblCuantosSalida(6) As Label
    Dim LblPlaga(12) As Label
    Dim LblPorcentajePlaga(12) As Label

    Private Sub CmdVolver_Click(sender As Object, e As EventArgs) Handles CmdVolver.Click
        panel.Visible = False
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        panel.Visible = True
    End Sub

    Private Sub CmdEdicionAlbaran1_Click_1(sender As Object, e As EventArgs) Handles CmdEdicionAlbaran1.Click
        If SituacionFormulario = Estados.Inactivo And CnTabla01.cuantos > 0 Then
            ImprimeAlbaran1(CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"), CnTabla01.ValorCampo("codigo_variedad"), CnTabla01.ValorCampo("Transportista"), CDbl("0" & CnTabla01.ValorCampo("peso_campo")))
        End If
    End Sub

    Private Sub CmdEdicionAlbaran2_Click(sender As Object, e As EventArgs) Handles CmdEdicionAlbaran2.Click
        If SituacionFormulario = Estados.Inactivo And CnTabla01.cuantos > 0 Then
            ImprimeAlbaran2(CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"), CnTabla01.ValorCampo("codigo_variedad"), CnTabla01.ValorCampo("Transportista"), CDbl("0" & CnTabla01.ValorCampo("peso_campo")))
        End If
    End Sub

    Private Sub CmdEdicionAlbaran3_Click(sender As Object, e As EventArgs) Handles CmdEdicionAlbaran3.Click
        If SituacionFormulario = Estados.Inactivo And CnTabla01.cuantos > 0 Then
            If Trim(CnTabla01.Rs(1)!entregada_sn) = "B" Then
                MsgBox("Clasificación definitiva pendiente de determinar")
            Else
                ImprimeAlbaran3(CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"), CnTabla01.ValorCampo("codigo_variedad"), CnTabla01.ValorCampo("Transportista"), CDbl("0" & CnTabla01.ValorCampo("peso_campo")))
            End If
        End If
    End Sub

    Private Sub ImprimeAlbaranPlagas_Click(sender As Object, e As EventArgs) Handles ImprimeAlbaranPlagas.Click
        If SituacionFormulario = Estados.Inactivo And CnTabla01.cuantos > 0 Then
            ImprimeDesglosePlagas(CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"), CnTabla01.ValorCampo("codigo_variedad"))
        End If
    End Sub

    Private Sub ImprimeNuevasEtiquetas_Click(sender As Object, e As EventArgs) Handles ImprimeNuevasEtiquetas.Click
        If SituacionFormulario = Estados.Inactivo Then
            If CnTabla01.cuantos > 0 Then
                ImprimeEtiquetas(CnTabla01.ValorCampo("serie_albaran"), CnTabla01.ValorCampo("numero_albaran"))
            End If
        End If
    End Sub

    Private Sub CmdBloquear_Click(sender As Object, e As EventArgs) Handles CmdBloquear.Click
        Dim Campos() As String, Valores() As String
        ReDim Campos(0), Valores(0)

        If Trim(CnTabla01.ValorCampo("entregada_sn")) <> "B" Then
            MsgBox("Este botón bloqueará la visualización de la clasificación")
            Campos(0) = "entregada_sn"
            Valores(0) = "B"
            HOOK_AsignarValores(CnTabla01, Campos, Valores)
            'RehacerSeleccion
        Else
            MsgBox("Este botón desbloqueará la visualización de la clasificación")
            Campos(0) = "entregada_sn" : Valores(0) = "N"
            HOOK_AsignarValores(CnTabla01, Campos, Valores)
            'RehacerSeleccion
        End If

    End Sub

    Private Sub FrmEntradaAlbaranes_Load(sender As Object, e As EventArgs) Handles Me.Load
        funciones.ObjetoGlobal = Me.ObjetoGlobal

        For i = 1 To 60
            Labels1(i) = DevuelveControl(Me, "label0" & Strings.Right("00" & i, 2), Frmbultos)
            Labels1(i).Visible = False
            LblCajas(i) = DevuelveControl(Me, "LblCajas" & Strings.Right("00" & i, 2), Frmbultos)
            LblCajas(i).Visible = False
            LblVolcado(i) = DevuelveControl(Me, "ok" & Strings.Right("00" & i, 2), Frmbultos)
            'LblVolcado(i).Visible = False
            LblVolcado(i).Visible = False
        Next

        For i = 1 To 12
            If i <= 6 Then
                LblEnvaseEntrada(i) = DevuelveControl(Me, "LblEnvaseEntrada" & Strings.Right("00" & i, 2), FrmEnvases)
                lblCuantosEntrada(i) = DevuelveControl(Me, "lblCuantosEntrada" & Strings.Right("00" & i, 2), FrmEnvases)
                lblEnvaseSalida(i) = DevuelveControl(Me, "lblEnvasesSalida" & Strings.Right("00" & i, 2), FrmEnvases)
                lblCuantosSalida(i) = DevuelveControl(Me, "lblCuantosSalida" & Strings.Right("00" & i, 2), FrmEnvases)
            End If
            LblPlaga(i) = DevuelveControl(Me, "LblPlaga" & Strings.Right("00" & i, 2), FrmPlagas)
            LblPorcentajePlaga(i) = DevuelveControl(Me, "LblPorcentajePlaga" & Strings.Right("00" & i, 2), FrmPlagas)
        Next

        With LvClasificacion
            .Columns.Clear()
            .Items.Clear()
            .Columns.Add("Descrición", 70, HorizontalAlignment.Center)
            .Columns.Add("Porc %", 200, HorizontalAlignment.Left)
            .Columns.Add("Clasif.", 100, HorizontalAlignment.Left)
        End With

        HayImpresoraEtiquetas = False
        HayImpresoraAlbaranes = False

        CargarSociosCampos()
        CargarProveedoresCampos()

        Dim ps As New System.Drawing.Printing.PrinterSettings
        Dim InstalledPrinters = ps.InstalledPrinters

        For i = 0 To ps.InstalledPrinters.Count - 1
            If LCase(Trim(InstalledPrinters(i))) = "etiquetas" Then
                HayImpresoraEtiquetas = True
            ElseIf LCase(Trim(InstalledPrinters(i))) = "albaran" Or LCase(Trim(InstalledPrinters(i))) = "albaranoki" Then 'NOSTD
                HayImpresoraAlbaranes = True
                NombreImpresoraAlbaranes = Trim(LCase(Trim(InstalledPrinters(i))))
            End If
        Next

    End Sub

    Private Sub CmdAlbaranNuevo_Click(sender As Object, e As EventArgs) Handles CmdAlbaranNuevo.Click
        Dim oFrm As FrmNEntradasNuevo01 = New FrmNEntradasNuevo01
        oFrm.ObjetoGlobal = Me.ObjetoGlobal
        oFrm.oForm = Me
        oFrm.ShowDialog()
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
    'Public Overrides Function HOOK_Antes_de_Update_en_creacion(CT As CnTabla.CnTabla, ByRef Rs As cncnRecordset.cnRecordset = New cnRecordset.cnRecordset.CncnRecordset.cnRecordset = New cnRecordset.cnRecordset) As Boolean
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
    'Public Overrides Function HOOK_Antes_de_Update_en_modificacion(CT As CnTabla.CnTabla, ByRef Rs As cncnRecordset.cnRecordset = New cnRecordset.cnRecordset.CncnRecordset.cnRecordset = New cnRecordset.cnRecordset) As Boolean
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

    'Public Overrides Function HOOK_Antes_de_Update_en_creacion(CT As CnTabla.CnTabla, ByRef Rs As cncnRecordset.cnRecordset = New cnRecordset.cnRecordset.CncnRecordset.cnRecordset = New cnRecordset.cnRecordset) As Boolean

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

    'Public Overrides Function HOOK_Antes_de_Update_en_modificacion(CT As CnTabla.CnTabla, ByRef Rs As cncnRecordset.cnRecordset = New cnRecordset.cnRecordset.CncnRecordset.cnRecordset = New cnRecordset.cnRecordset) As Boolean

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

    Public Overrides Function HOOK_Cambio_de_registro(CT As CnTabla.CnTabla) As Boolean
        ' Me.Text = "Cambio en tabla " + Trim(CT.Tabla) + " a registro " + CStr(CT.RegistroActual)

        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Integer
        Dim j As Integer
        Dim RsClasificacion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Clasificacion(12) As Long
        Dim porcentaje As Single
        Dim RsPlagas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Cuantos As Integer
        Dim Item As ListViewItem

        If UCase(CT.Tabla) = "ENTRADAS_ALBARANES" Then

            If Trim("" & CT.ValorCampo("codigo_socio")) <> "" Then
                RsSocios.Open("SELECT * FROM socios_coop WHERE codigo_socio = " & CT.ValorCampo("codigo_socio"), ObjetoGlobal.cn)
                If Not RsSocios.EOF Then
                    If Trim("" & RsSocios!doc1) <> "S" Then
                        Me.Text = "Entrada Albaranes  -    AVISO: Este socio no ha firmado la comunicación de la Ley Cadena Alimentaria"
                    Else
                        Me.Text = "Entrada Albaranes"
                    End If
                End If
                RsSocios.Close()
            Else
                Me.Text = "Entrada Albaranes"
            End If

            LblSituacion.Text = ""
            If Trim("" & CT.ValorCampo("empresa")) > "" Then

                TipoDeAlbaran = Trim(CT.Rs("tipo_entrada"))
                If TipoDeAlbaran <> "T" Then
                    SQL = "SELECT SITUACION_CAMPOS.DESCRIPCION FROM CAMPOS JOIN SITUACION_CAMPOS ON SITUACION_CAMPOS.EMPRESA = CAMPOS.EMPRESA AND SITUACION_CAMPOS.CODIGO_SITUACION = CAMPOS.SITUACION_CAMPO"
                    SQL = Trim(SQL) + " WHERE CAMPOS.EMPRESA = '" + Trim("" & CT.ValorCampo("empresa")) + "' AND CAMPOS.CODIGO_CAMPO = " + CStr(CT.ValorCampo("codigo_campo"))
                    Rs.Open(SQL, ObjetoGlobal.cn)
                    If Rs.RecordCount > 0 Then
                        LblSituacion.Text = Rs!Descripcion
                    Else
                        LblSituacion.Text = ""
                    End If
                    Rs.Close()
                    SQL = "SELECT NOMBRE_SOCIO FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(CT.ValorCampo("codigo_socio"))
                    Rs.Open(SQL, ObjetoGlobal.cn)
                    If Rs.RecordCount > 0 Then
                        lblNombresocio.Text = Trim("" & Rs!nombre_socio)
                    Else
                        lblNombresocio.Text = ""
                    End If
                    Rs.Close()
                End If
                Frmbultos.Visible = True
                For i = 1 To 60
                    Labels1(i).Visible = False
                    LblCajas(i).Visible = False
                    LblVolcado(i).Visible = False
                Next
                SQL = "SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim("" & CT.ValorCampo("empresa")) + "' AND EJERCICIO = '" + Trim("" & CT.ValorCampo("Ejercicio")) + "' AND SERIE_ALBARAN = '" + Trim("" & CT.ValorCampo("serie_albaran")) + "' AND NUMERO_ALBARAN = " + CStr("" & CT.ValorCampo("numero_albaran")) + " AND BULTO between 1 and 60 order by 1,2,3,4,5"
                Rs.Open(SQL, ObjetoGlobal.cn)
                For i = 1 To 60
                    Labels1(i).Visible = False
                    LblCajas(i).Visible = False
                    LblVolcado(i).Visible = False
                Next
                If Rs.RecordCount > 0 Then
                    For i = 1 To Rs.RecordCount
                        Rs.AbsolutePosition = i
                        j = Rs!Bulto
                        LblCajas(j).Text = CStr(Rs!Cajas)
                        Labels1(j).Visible = True
                        LblCajas(j).Visible = True
                        If Rs!Situacion <> "N" Then LblVolcado(i).Visible = True
                    Next
                End If
                Rs.Close()


                For i = 1 To 12
                    Clasificacion(i) = 0
                Next
                RsClasificacion.Open("SELECT ec.*, cv.descripcion FROM ENTRADAS_CLASIF ec JOIN calidades_var_ej cv ON cv.empresa = ec.empresa AND cv.ejercicio = ec.ejercicio AND cv.codigo_variedad = ec.codigo_variedad AND cv.codigo_calidad = ec.codigo_calidad WHERE ec.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ec.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND ec.SERIE_ALBARAN = '" + Trim(CnTabla01.ValorCampo("serie_albaran")) + "' and ec.NUMERO_ALBARAN = " + CStr(CnTabla01.ValorCampo("numero_albaran")) + " AND ec.TIPO_CLASIFICACION = 'LIQ' order by 1,2,3,4,5,6", ObjetoGlobal.cn)

                LvClasificacion.Items.Clear()

                For i = 1 To RsClasificacion.RecordCount
                    RsClasificacion.AbsolutePosition = i
                    Clasificacion(i) = RsClasificacion!kg_calidad
                    porcentaje = 0
                    If CnTabla01.ValorCampo("kg_entrada") <> 0 Then
                        porcentaje = Math.Round(Clasificacion(i) * 100 / CnTabla01.ValorCampo("kg_entrada"), 2)
                    End If
                    Item = LvClasificacion.Items.Add(Trim(RsClasificacion!Descripcion))
                    Item.SubItems.Add("" & Format(porcentaje, "##0.00"))
                    Item.SubItems.Add("" & Format(Clasificacion(i), "##,###,##0"))
                Next

                '            End If
                RsClasificacion.Close()
                'Else
                '    FrmClasificacion.Visible = False
                'End If
                'Plagas
                'If ChkVerPlagas.Value = vbChecked Then
                FrmPlagas.Visible = True
                For i = 1 To 12
                    LblPlaga(i).Text = ""
                    LblPorcentajePlaga(i).Text = ""
                Next

                RsPlagas.Open("SELECT E.*,P.DESCRIPCION FROM ENTRADAS_PLAGAS E JOIN PLAGAS P ON P.EMPRESA = E.EMPRESA AND P.CODIGO_PLAGA = E.CODIGO_PLAGA WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(CnTabla01.ValorCampo("serie_albaran")) + "' and E.NUMERO_ALBARAN = " + CStr(CnTabla01.ValorCampo("numero_albaran")) + " order by 1,2,3,4,5", ObjetoGlobal.cn)
                For i = 1 To RsPlagas.RecordCount
                    If i <= 12 Then
                        RsPlagas.AbsolutePosition = i
                        LblPlaga(i).Text = Trim(RsPlagas!Descripcion)
                        LblPorcentajePlaga(i).Text = Format(RsPlagas!porcentaje, "##0.#0")
                    End If
                Next
                RsPlagas.Close()

                FrmEnvases.Visible = True
                For i = 1 To 6
                    LblEnvaseEntrada(i).Text = ""
                    lblCuantosEntrada(i).Text = ""
                    lblEnvaseSalida(i).Text = ""
                    lblCuantosSalida(i).Text = ""
                Next

                RsEnvases.Open("SELECT * FROM ENTRADAS_ENVASES E WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(CnTabla01.ValorCampo("serie_albaran")) + "' and E.NUMERO_ALBARAN = " + CStr(CnTabla01.ValorCampo("numero_albaran")) + " order by 1,2,3,4,5", ObjetoGlobal.cn)
                Cuantos = 0
                For i = 1 To RsEnvases.RecordCount
                    If Cuantos < 6 Then
                        RsEnvases.AbsolutePosition = i
                        If Trim(RsEnvases!entrada_salida) = "E" Then
                            Cuantos = Cuantos + 1
                            LblEnvaseEntrada(Cuantos).Text = Trim(RsEnvases!codigo_envase)
                            lblCuantosEntrada(Cuantos).Text = Format(RsEnvases!numero_envases, "##,##0")
                        End If
                    End If
                Next
                Cuantos = 0
                For i = 1 To RsEnvases.RecordCount
                    If Cuantos < 6 Then
                        RsEnvases.AbsolutePosition = i
                        If Trim(RsEnvases!entrada_salida) = "S" Then
                            Cuantos = Cuantos + 1
                            lblEnvaseSalida(Cuantos).Text = Trim(RsEnvases!codigo_envase)
                            lblCuantosSalida(Cuantos).Text = Format(RsEnvases!numero_envases, "##,##0")
                        End If
                    End If
                Next
                RsEnvases.Close()
            End If
        End If
        Return False

    End Function



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
        Return True
    End Function

    Private Sub CmdCambioCampo_Click(sender As Object, e As EventArgs) Handles CmdCambioCampo.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        If CnTabla01.cuantos > 0 Then
            SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
            AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")
            If Trim(TipoDeAlbaran) = "T" Then
                Dim oFrm As FrmNEntradasNuevo11T = New FrmNEntradasNuevo11T
                oFrm.oForm = Me
                oFrm.SerieSeleccionada = SerieSeleccionada
                oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.ShowDialog()
            Else
                Dim oFrm As FrmNEntradasNuevo11 = New FrmNEntradasNuevo11
                oFrm.oForm = Me
                oFrm.SerieSeleccionada = SerieSeleccionada
                oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.ShowDialog()
            End If
        End If


    End Sub

    Private Sub CmdCambiodetara_Click(sender As Object, e As EventArgs) Handles CmdCambiodetara.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Rs1 As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        Rs.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            If MsgBox("ATENCIÓN: Este albaran ya está clasificado." + vbCrLf + "Si modifica los pesos, envases o bultos," + vbCrLf + "Se modificarán los datos de TARA y la CLASIFICACION." + vbCrLf + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Rs.Close()
                Return
            End If
            Rs.Close()
        Else
            Rs1.Open("SELECT tarada_sn FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn)
            If Trim(Rs1("tarada_sn")) = "S" Then
                If MsgBox("ATENCIÓN: Este albaran ya está tarado." + vbCrLf + "Si modifica los pesos, envases o bultos," + vbCrLf + "se MODIFICARÁN los datos de TARA." + vbCrLf + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Rs.Close()
                    Rs1.Close()
                    Return
                End If
            End If
        End If
        Rs.Close()
        Rs1.Close()

            If Trim(TipoDeAlbaran) = "T" Then
            Dim oForm As FrmNEntradasNuevo10T = New FrmNEntradasNuevo10T
            oForm.oForm = Me
            oForm.SerieSeleccionada = SerieSeleccionada
            oForm.AlbaranSeleccionado = AlbaranSeleccionado
            oForm.ObjetoGlobal = Me.ObjetoGlobal
            oForm.ShowDialog()
        Else
            Dim oForm As FrmNEntradasNuevo10 = New FrmNEntradasNuevo10
            oForm.oForm = Me
            oForm.SerieSeleccionada = SerieSeleccionada
            oForm.AlbaranSeleccionado = AlbaranSeleccionado
            oForm.ObjetoGlobal = Me.ObjetoGlobal
            oForm.ShowDialog()
        End If
        Return

    End Sub

    Private Sub CmdCambioPesoCuadrilla_Click(sender As Object, e As EventArgs) Handles CmdCambioPesoCuadrilla.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")
        If Trim(TipoDeAlbaran) = "T" Then
            Dim oFrm As FrmNEntradasNuevo15T = New FrmNEntradasNuevo15T
            oFrm.oform = Me
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.ShowDialog()
        Else
            Dim oFrm As FrmNEntradasNuevo15 = New FrmNEntradasNuevo15
            oFrm.oform = Me
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.ShowDialog()
        End If

    End Sub

    Private Sub CmdReclamaciones_Click(sender As Object, e As EventArgs) Handles CmdReclamaciones.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        If Trim(TipoDeAlbaran) = "T" Then
            Dim oFrm As FrmNEntradasNuevo20t = New FrmNEntradasNuevo20t
            oFrm.oform = Me
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.ShowDialog()
        Else
            Dim oFrm As FrmNEntradasNuevo20 = New FrmNEntradasNuevo20
            oFrm.oform = Me
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.ShowDialog()
        End If

    End Sub

    Private Sub CmdIndustria_Click(sender As Object, e As EventArgs) Handles CmdIndustria.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Rs.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("ATENCIÓN: Este albaran ya está clasificado." + vbCrLf + "Compruebe que la clasificación sea la correcta en la nueva situación.", MsgBoxStyle.Exclamation)
        End If
        Rs.Close
        Rs.Open("SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn)
        If Rs.RecordCount = 1 Then
            If Rs!industria_sn = "S" Then ' Ahora es industria
                If MsgBox("A continuación indicará el número de bultos del albarán." + vbCrLf +
            "Al grabar las modificaciones, perderá su estado de industria directa." + vbCrLf +
            "¿Desea continuar con el cambio?", MsgBoxStyle.YesNo And MsgBoxStyle.Critical) <> MsgBoxResult.Yes Then
                    Return
                End If
                If Trim(TipoDeAlbaran) = "T" Then
                    Dim oForm As FrmNEntradasNuevo09T = New FrmNEntradasNuevo09T
                    oForm.oForm = Me
                    oForm.ObjetoGlobal = Me.ObjetoGlobal
                    oForm.ModificaIndustria = True
                    oForm.ShowDialog()
                Else
                    Dim oForm As FrmNEntradasNuevo09 = New FrmNEntradasNuevo09
                    oForm.oForm = Me
                    oForm.ObjetoGlobal = Me.ObjetoGlobal
                    oForm.ModificaIndustria = True
                    oForm.showdialog()
                End If
            ElseIf Rs!industria_sn = "N" Then ' Ahora NO es industria
                If MsgBox("A continuación cambiará la condición de albarán a industria. " + vbCrLf +
            "Todos los bultos indicados en el albarán serán borrados " + vbCrLf +
            "definitivamente de la base de datos" + vbCrLf +
            "¿Desea continuar con el cambio?", vbCritical + vbYesNo) = vbNo Then Exit Sub
                CambiarAIndustria()
            End If
        End If

    End Sub

    Public Function HayBultosVolcados()
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        HayBultosVolcados = False
        Rs.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND SITUACION <> 'N'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("No se puede manipular esta entrada porque tiene ya bultos volcados.")
            Return True
        End If
        Rs.Close
        Return False

    End Function


    Public Sub CambiarAIndustria()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        If HayBultosVolcados Then
            Return
        End If

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            ' Se borran los bultos
            Rs.Open("SELECT * FROM entradas_albaranes WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                Rs!industria_sn = "S"
                Rs.Update()
            End If
            Rs.Close()

            ' Se borran los bultos
            Rs.Open("SELECT * FROM entradas_bultos WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close

            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return

        Catch ex As Exception
            trans.Rollback()
            MsgBox("No se puede eliminar el albarán indicado." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(ex.Message))
            Return

        End Try

    End Sub

    Private Sub CmdDividir_Click(sender As Object, e As EventArgs) Handles CmdDividir.Click
        MsgBox("Todavía no se puede dividir albaranes de entrada", MsgBoxStyle.Exclamation)
        Return
    End Sub

    Private Sub CmdTransportista_Click(sender As Object, e As EventArgs) Handles CmdTransportista.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        If CnTabla01.ValorCampo("liquidada_c_sn") = "S" Then
            If MsgBox("ATENCIÓN: Este albaran ya está liquidado a cuadrillas." + vbCrLf + "Si modifica capataz/transportisa, debe anular la liquidación" + vbCrLf + " y realizar una nueva." + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return
            End If
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        If Trim(TipoDeAlbaran) = "T" Then
            Dim oFrm As FrmNEntradasNuevo14T = New FrmNEntradasNuevo14T
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.oForm = Me
            oFrm.ShowDialog()
        Else
            Dim oFrm As FrmNEntradasNuevo14 = New FrmNEntradasNuevo14
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.oForm = Me
            oFrm.ShowDialog()
        End If

    End Sub

    Private Sub CmdCambioClareta_Click(sender As Object, e As EventArgs) Handles CmdCambioClareta.Click
        Dim oFrm As FrmNEntradasNuevo21 = New FrmNEntradasNuevo21

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        oFrm.ObjetoGlobal = Me.ObjetoGlobal
        oFrm.oForm = Me
        oFrm.SerieSeleccionada = SerieSeleccionada
        oFrm.AlbaranSeleccionado = AlbaranSeleccionado
        oFrm.ShowDialog()
    End Sub

    Private Sub CmdEntregado_Click(sender As Object, e As EventArgs) Handles CmdEntregado.Click
        Dim Cadena As String
        Dim Campos() As String
        Dim Valores() As String

        If Trim(CnTabla01.ValorCampo("entregada_sn2")) = "S" Then
            Cadena = "NO"
        Else
            Cadena = "SÍ"
        End If

        If MsgBox("ATENCIÓN: Se cambiará el dato de entregado a " + Trim(Cadena) + vbCrLf + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        ReDim Campos(0), Valores(0)
        Campos(0) = "entregada_sn"
        Valores(0) = Strings.Left(Cadena, 1)
        HOOK_AsignarValores(CnTabla01, Campos, Valores)

    End Sub

    Private Sub CmdLiquidacionCuadrilla_Click(sender As Object, e As EventArgs) Handles CmdLiquidacionCuadrilla.Click
        Dim Cadena As String
        Dim Campos() As String
        Dim Valores() As String
        Dim SubCadena As String

        If Trim(CnTabla01.Rs(1)!liquidada_c_sn) = "S" Then
            Cadena = "No liquidada"
            SubCadena = "N"
        Else
            Cadena = "Liquidada"
            SubCadena = "S"
        End If
        If MsgBox("ATENCIÓN: Se cambiará el dato de liqu. cuadrillas a: " + Trim(Cadena) + vbCrLf + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Return
        End If
        ReDim Campos(0), Valores(0)
        Campos(0) = "liquidada_c_sn"
        Valores(0) = SubCadena
        HOOK_AsignarValores(CnTabla01, Campos, Valores)

    End Sub

    Private Sub CmdCambioDefectos_Click(sender As Object, e As EventArgs) Handles CmdCambioDefectos.Click
        Dim oFrm As FrmNEntradasNuevo18 = New FrmNEntradasNuevo18

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        oFrm.ObjetoGlobal = Me.ObjetoGlobal
        oFrm.oform = Me
        oFrm.SerieSeleccionada = SerieSeleccionada
        oFrm.AlbaranSeleccionado = AlbaranSeleccionado
        oFrm.ShowDialog()
    End Sub

    Private Sub CmdAvisos_Click(sender As Object, e As EventArgs) Handles CmdAvisos.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")
        If Trim(TipoDeAlbaran) = "T" Then
            Dim oFrm As FrmNEntradasNuevo13t = New FrmNEntradasNuevo13t
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.oform = Me
            oFrm.ShowDialog()
        Else
            Dim oFrm As FrmNEntradasNuevo13 = New FrmNEntradasNuevo13
            oFrm.SerieSeleccionada = SerieSeleccionada
            oFrm.AlbaranSeleccionado = AlbaranSeleccionado
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.oform = Me
            oFrm.ShowDialog()
        End If

    End Sub

    Private Sub CmdCambiodepeso_Click(sender As Object, e As EventArgs) Handles CmdCambiodepeso.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim FlagError As Boolean
        Dim FlagVolcado As Boolean

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        FlagVolcado = False
        Rs.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND SITUACION <> 'N'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("No se puede modificar bultos en esta entrada porque ya existen bultos volcados." + vbCrLf + "Se permitirá únicamente la modificación de peso.")
            FlagVolcado = True
        End If
        Rs.Close
        Rs.Open("SELECT * FROM ENTRADAS_CLASIF WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND TIPO_CLASIFICACION = 'LIQ'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            If MsgBox("ATENCIÓN: Este albaran ya está clasificado." + vbCrLf + "Si modifica los pesos, envases o bultos," + vbCrLf + "se MODIFICARÁN los datos de TARA y la CLASIFICACION." + vbCrLf + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return
            End If
            Rs.Close()
        End If
        If Trim(CnTabla01.ValorCampo("tarada_sn")) = "S" Then
            If MsgBox("ATENCIÓN: Este albaran ya está tarado." + vbCrLf + "Si modifica los pesos, envases o bultos," + vbCrLf + "se MODIFICARÁN los datos de TARA." + vbCrLf + "¿Desea seguir y modificar el albarán?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return
            End If
        End If

        If FlagVolcado = False Then
            If Trim(TipoDeAlbaran) = "T" Then
                Dim oFrm As FrmNEntradasNuevo09T = New FrmNEntradasNuevo09T
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.oForm = Me
                oFrm.SerieSeleccionada = SerieSeleccionada
                oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                oFrm.ShowDialog()
            Else
                Dim oFrm As FrmNEntradasNuevo09 = New FrmNEntradasNuevo09
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.oForm = Me
                oFrm.SerieSeleccionada = SerieSeleccionada
                oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                oFrm.ShowDialog()
            End If
        Else
            If Trim(TipoDeAlbaran) = "T" Then
                Dim oFrm As FrmNEntradasNuevo16T = New FrmNEntradasNuevo16T
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.oForm = Me
                oFrm.SerieSeleccionada = SerieSeleccionada
                oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                oFrm.ShowDialog()
            Else
                Dim oFrm As FrmNEntradasNuevo16 = New FrmNEntradasNuevo16
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.oForm = Me
                oFrm.SerieSeleccionada = SerieSeleccionada
                oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                oFrm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub CmdHora_Click(sender As Object, e As EventArgs) Handles CmdHora.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        If Trim(TipoDeAlbaran) = "T" Then
            Dim oFrm As FrmNEntradasNuevo12T = New FrmNEntradasNuevo12T
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.oform = Me
            oFrm.ShowDialog()
        Else
            Dim oFrm As FrmNEntradasNuevo12 = New FrmNEntradasNuevo12
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.oform = Me
            oFrm.ShowDialog()
        End If

    End Sub

    Private Sub CmdCambiaHelada_Click(sender As Object, e As EventArgs) Handles CmdCambiaHelada.Click
        Dim oFrm As FrmNEntradasNuevo19 = New FrmNEntradasNuevo19

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        SerieSeleccionada = CnTabla01.ValorCampo("Serie_albaran")
        AlbaranSeleccionado = CnTabla01.ValorCampo("Numero_albaran")

        oFrm.ObjetoGlobal = Me.ObjetoGlobal
        oFrm.oform = Me
        oFrm.SerieSeleccionada = SerieSeleccionada
        oFrm.AlbaranSeleccionado = AlbaranSeleccionado
        oFrm.ShowDialog()

    End Sub

    Private Sub CmdBorra_Click(sender As Object, e As EventArgs) Handles CmdBorra.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        Rs.Open("SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado) + " AND SITUACION <> 'N'", ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            MsgBox("No se puede eliminar esta entrada porque tiene ya bultos volcados.")
            Exit Sub
        End If
        Rs.Close
        If Trim(CnTabla01.ValorCampo("liquidada_c_sn")) = "S" Then
            MsgBox("ATENCIÓN: Este albarán ya está liquidado a cuadrillas." + vbCrLf + "No puede eliminarse.")
        End If
        If MsgBox("ATENCIÓN: Este proceso borrará definitivamente este albarán." + vbCrLf + "¿Conforme en continuar?", MsgBoxStyle.Critical And MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Return
        End If
        Borrar_Albaran

    End Sub

    Public Sub Borrar_Albaran()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        If HayBultosVolcados() Then
            Return
        End If

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try
            Rs.Open("SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                Rs.Delete
            End If
            Rs.Close
            Rs.Open("SELECT * FROM entradas_bultos WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close
            Rs.Open("SELECT * FROM entradas_clasif WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close
            Rs.Open("SELECT * FROM entradas_costes WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close
            Rs.Open("SELECT * FROM entradas_envases WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close
            Rs.Open("SELECT * FROM entradas_plagas WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close
            Rs.Open("SELECT * FROM entradas_recol WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' AND NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado), ObjetoGlobal.cn, True,,,,,, trans)
            If Rs.RecordCount > 0 Then
                While Rs.RecordCount > 0
                    Rs.MoveFirst
                    Rs.Delete
                End While
            End If
            Rs.Close
            trans.Commit()
            ObjetoGlobal.cn.Close()
            Return

        Catch ex As Exception
            trans.Rollback()
            MsgBox("No se puede eliminar el albarán indicado." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(ex.Message))
            Return
        End Try

    End Sub

    Private Sub CmdClasifica_kaki_Click(sender As Object, e As EventArgs) Handles CmdClasifica_kaki.Click

        If CnTabla01.cuantos = 0 Then
            Return
        End If

        If SituacionFormulario = Estados.Inactivo Then
            If ModoConexion > 1 Then
                MsgBox("Esta tarea sólo puede realizarse si está conectado al servidor")
                Exit Sub
            End If
            SerieSeleccionada = CnTabla01.ValorCampo("serie_albaran")
            AlbaranSeleccionado = CnTabla01.ValorCampo("numero_albaran")
            If Trim("" & CnTabla01.ValorCampo("codigo_socio")) <> "" Then
                Dim oFrm As FrmNEntradasNuevo22 = New FrmNEntradasNuevo22
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.oForm = Me
                oFrm.ShowDialog()
            ElseIf Trim("" & CnTabla01.ValorCampo("codigo_proveedor")) <> "" Then
                Dim oFrm As FrmNEntradasNuevo22t = New FrmNEntradasNuevo22t
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.oForm = Me
                oFrm.ShowDialog()
            End If
        End If

    End Sub

    Private Sub CmdAlbaranNuevoTerceros_Click(sender As Object, e As EventArgs) Handles CmdAlbaranNuevoTerceros.Click
        Dim oFrm As FrmNEntradasNuevo01T = New FrmNEntradasNuevo01T
        oFrm.ObjetoGlobal = Me.ObjetoGlobal
        oFrm.oForm = Me
        oFrm.ShowDialog()

    End Sub

    Private Sub CmdTarar_Click(sender As Object, e As EventArgs) Handles CmdTarar.Click
        Dim oFrm As FrmNEntradasNuevo05 = New FrmNEntradasNuevo05
        oFrm.ObjetoGlobal = Me.ObjetoGlobal
        oFrm.oForm = Me
        oFrm.ShowDialog()
    End Sub

    Private Sub CmdClasificar_Click(sender As Object, e As EventArgs) Handles CmdClasificar.Click
        Dim oFrm As FrmNEntradasNuevo07 = New FrmNEntradasNuevo07

        If SituacionFormulario = Estados.Inactivo Then
            If ModoConexion > 1 Then
                MsgBox("Esta tarea sólo puede realizarse si está conectado al servidor")
                Exit Sub
            End If
            oFrm.ObjetoGlobal = Me.ObjetoGlobal
            oFrm.oForm = Me
            oFrm.ShowDialog()
        End If

    End Sub

    Private Sub CmdComunicacion_Click(sender As Object, e As EventArgs) Handles CmdComunicacion.Click
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        If SituacionFormulario <> Estados.Inactivo Then
            Exit Sub
        End If

        If MsgBox("¿Imprimir conocimiento Ley Cadena alimentaria?", vbQuestion + vbYesNo) = vbYes Then
            ImprimeComunicacion(CnTabla01.ValorCampo("codigo_socio"))
        End If


        If CnTabla01.CuantosRegistros <> 0 Then
            If MsgBox("¿Marcar como firmado el documento de la Ley Cadena Alimentaria?", MsgBoxStyle.Question And MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Sql = "SELECT * FROM socios_coop WHERE codigo_socio = " & CnTabla01.ValorCampo("codigo_socio")
                RsSocios.Open(Sql, ObjetoGlobal.cn)
                If RsSocios.RecordCount = 1 Then
                    RsSocios!doc1 = "S"
                    RsSocios.Update
                End If
                RsSocios.Close
            End If
        End If

    End Sub
    Public Sub CargarSociosCampos()
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsOperarios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cadena As String
        Dim Cadena2 As String
        Dim Sql As String
        Dim i As Integer

        Sql = "SELECT CAMPOS.CODIGO_CAMPO, campos.codigo_socio, cultivos.codigo_variedad, CAMPOS.SITUACION_CAMPO, SITUACION_CAMPOS.DESCRIPCION, CULTIVOS.HANEGADAS, VARIEDADES.DESCRIPCION AS DESCRIPCION_VARIEDAD "
        Sql = Sql + " FROM CULTIVOS JOIN CAMPOS ON campos.empresa = cultivos.empresa AND campos.codigo_campo = cultivos.codigo_campo "
        Sql = Sql + " JOIN SITUACION_CAMPOS ON situacion_campos.empresa = cultivos.empresa AND situacion_campos.codigo_situacion = campos.situacion_campo "
        Sql = Sql + " JOIN VARIEDADES ON VARIEDADES.EMPRESA = CULTIVOS.EMPRESA AND VARIEDADES.CODIGO_VARIEDAD = CULTIVOS.CODIGO_VARIEDAD"
        Sql = Sql + " WHERE CULTIVOS.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CULTIVOS.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND "
        Sql = Sql + " CULTIVOS.ALTA_BAJA = 'A' AND CAMPOS.ALTA_BAJA = 'A' "
        Sql = Sql + " ORDER BY campos.codigo_socio, CULTIVOS.CODIGO_VARIEDAD, CAMPOS.CODIGO_CAMPO"

        RsCampos.Open(Sql, ObjetoGlobal.cn)
        ReDim ReferenciaCampo(RsCampos.RecordCount)
        ReDim DatosCampo(RsCampos.RecordCount)
        CuantosCampos = RsCampos.RecordCount

        For i = 1 To RsCampos.RecordCount
            RsCampos.AbsolutePosition = i

            cadena = Space(30)
            Cadena2 = Format(RsCampos!codigo_socio, "0000000000")
            Mid(cadena, 1, 10) = Cadena2
            Mid(cadena, 11, Len(Trim("" & RsCampos!codigo_variedad))) = Trim("" & RsCampos!codigo_variedad)
            Cadena2 = Format(RsCampos!codigo_campo, "0000000000")
            Mid(cadena, 21, 10) = Cadena2
            ReferenciaCampo(i) = cadena

            cadena = Space(107)
            Mid(cadena, 1, Len(Trim("" & RsCampos!situacion_campo))) = Trim("" & RsCampos!situacion_campo)
            Mid(cadena, 11, Len(Trim("" & RsCampos!Descripcion))) = Trim("" & RsCampos!Descripcion)
            Cadena2 = Format(RsCampos!Hanegadas, "00.0000")
            Mid(cadena, 71, 7) = Cadena2
            Cadena2 = Trim(Strings.Left("" & RsCampos!descripcion_variedad, 30))
            Mid(cadena, 78, 30) = Cadena2
            DatosCampo(i) = cadena
        Next
        RsCampos.Close()

        Sql = "SELECT SOCIOS_COOP.APELLIDOS_SOCIO, SOCIOS_COOP.NOMBRE_SOCIO,SOCIOS_COOP.CODIGO_SOCIO "
        Sql = Sql + " FROM CULTIVOS JOIN CAMPOS ON campos.empresa = cultivos.empresa AND campos.codigo_campo = cultivos.codigo_campo "
        Sql = Sql + " JOIN SOCIOS_COOP ON SOCIOS_COOP.CODIGO_SOCIO = CAMPOS.CODIGO_SOCIO"
        Sql = Sql + " WHERE CULTIVOS.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CULTIVOS.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND "
        Sql = Sql + " CULTIVOS.ALTA_BAJA = 'A' AND CAMPOS.ALTA_BAJA = 'A' "
        Sql = Sql + " GROUP BY SOCIOS_COOP.APELLIDOS_SOCIO, SOCIOS_COOP.NOMBRE_SOCIO,SOCIOS_COOP.CODIGO_SOCIO "
        Sql = Sql + " ORDER BY SOCIOS_COOP.APELLIDOS_SOCIO, SOCIOS_COOP.NOMBRE_SOCIO,SOCIOS_COOP.CODIGO_SOCIO "
        RsSocios.Open(Sql, ObjetoGlobal.cn)

        ReDim ReferenciaSocio(RsSocios.RecordCount)
        CuantosSocios = RsSocios.RecordCount

        For i = 1 To RsSocios.RecordCount
            RsSocios.AbsolutePosition = i
            cadena = Space(90)
            Cadena2 = Trim("" & RsSocios!Apellidos_socio) + " " + Trim("" & RsSocios!nombre_socio)
            Mid(cadena, 1, Len(Cadena2)) = Cadena2
            Cadena2 = Format(RsSocios!codigo_socio, "0000000000")
            Mid(cadena, 81, 10) = Cadena2
            ReferenciaSocio(i) = cadena
        Next
        RsSocios.Close()

        Sql = "SELECT CODIGO_OPERARIO,NOMBRE_OPERARIO "
        Sql = Sql + " FROM OPERARIOS_COOP "
        Sql = Sql + " WHERE CODIGO_OPERARIO<20000 AND SITUACION in ('AC','ET','G','OC')"
        Sql = Sql + " ORDER BY NOMBRE_OPERARIO, CODIGO_OPERARIO"
        RsOperarios.Open(Sql, ObjetoGlobal.cn)
        ReDim ReferenciaOperario(RsOperarios.RecordCount)
        CuantosOperarios = RsOperarios.RecordCount

        For i = 1 To RsOperarios.RecordCount
            RsOperarios.AbsolutePosition = i
            cadena = Space(90)
            Cadena2 = Trim("" & RsOperarios!nombre_operario)
            Mid(cadena, 1, Len(Cadena2)) = Cadena2
            Cadena2 = Format(RsOperarios!codigo_operario, "0000000000")
            Mid(cadena, 81, 10) = Cadena2
            ReferenciaOperario(i) = cadena
        Next
        RsOperarios.Close()
    End Sub

    Public Sub CargarProveedoresCampos()
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim cadena As String
        Dim Cadena2 As String
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsOperarios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim i As Integer

        Sql = "SELECT c.proveedor, c.codigo_variedad, c.codigo_campo, c.situacion_campo, v.descripcion AS descripcion_variedad FROM campos_terceros c "
        Sql = Sql + " JOIN variedades v ON v.empresa = c.empresa AND v.codigo_variedad = c.codigo_variedad"
        Sql = Sql + " WHERE c.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND c.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) & "' "
        Sql = Sql + " ORDER BY c.proveedor, c.codigo_variedad, c.codigo_campo"

        RsCampos.Open(Sql, ObjetoGlobal.cn)
        ReDim ReferenciaCampoTerceros(RsCampos.RecordCount)
        ReDim DatosCampoTerceros(RsCampos.RecordCount)
        CuantosCamposTerceros = RsCampos.RecordCount

        For i = 1 To RsCampos.RecordCount
            RsCampos.AbsolutePosition = i
            cadena = Space(30)
            Cadena2 = Format(RsCampos!proveedor, "0000000000")
            Mid(cadena, 1, 10) = Cadena2
            Mid(cadena, 11, Len(Trim("" & RsCampos!codigo_variedad))) = Trim("" & RsCampos!codigo_variedad)
            Cadena2 = Format(RsCampos!codigo_campo, "0000000000")
            Mid(cadena, 21, 10) = Cadena2
            ReferenciaCampoTerceros(i) = cadena

            cadena = Space(107)
            Mid(cadena, 1, Len(Trim("" & RsCampos!situacion_campo))) = Trim("" & RsCampos!situacion_campo)
            Mid(cadena, 11, Len(Trim("" & RsCampos!situacion_campo))) = Trim("" & RsCampos!situacion_campo)
            Cadena2 = "00.0000" ' Format(RsCampos!Hanegadas, "00.0000")
            Mid(cadena, 71, 7) = Cadena2
            Cadena2 = Trim(Strings.Left("" & RsCampos!descripcion_variedad, 30))
            Mid(cadena, 78, 30) = Cadena2
            DatosCampoTerceros(i) = cadena
        Next
        RsCampos.Close()

        Sql = "SELECT p.razon_social,p.codigo_proveedor "
        Sql = Sql + " FROM campos_terceros c "
        Sql = Sql + " join proveedores_coop p ON p.codigo_proveedor = c.proveedor"
        Sql = Sql + " WHERE c.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND c.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' "
        Sql = Sql + " GROUP BY p.razon_social,p.codigo_proveedor  "
        Sql = Sql + " ORDER BY p.razon_social,p.codigo_proveedor  "

        RsSocios.Open(Sql, ObjetoGlobal.cn)
        ReDim ReferenciaSocioTerceros(RsSocios.RecordCount)
        CuantosSociosTerceros = RsSocios.RecordCount

        For i = 1 To RsSocios.RecordCount
            RsSocios.AbsolutePosition = i
            cadena = Space(90)
            Cadena2 = Trim("" & RsSocios!razon_social)
            Mid(cadena, 1, Len(Cadena2)) = Cadena2
            Cadena2 = Format(RsSocios!codigo_proveedor, "0000000000")
            Mid(cadena, 81, 10) = Cadena2
            ReferenciaSocioTerceros(i) = cadena
        Next
        RsSocios.Close()

        Sql = "SELECT codigo, nombre "
        Sql = Sql + " FROM personal_terceros "
        Sql = Sql + " ORDER BY nombre, codigo"
        RsOperarios.Open(Sql, ObjetoGlobal.cn)
        ReDim ReferenciaOperarioTerceros(RsOperarios.RecordCount)
        CuantosOperariosTerceros = RsOperarios.RecordCount

        For i = 1 To RsOperarios.RecordCount
            RsOperarios.AbsolutePosition = i
            cadena = Space(90)
            Cadena2 = Trim("" & RsOperarios!Nombre)
            Mid(cadena, 1, Len(Cadena2)) = Cadena2
            Cadena2 = Format(RsOperarios!codigo, "0000000000")
            Mid(cadena, 81, 10) = Cadena2
            ReferenciaOperarioTerceros(i) = cadena
        Next
        RsOperarios.Close()
    End Sub

    Private Sub CmdLCA_Click(sender As Object, e As EventArgs) Handles CmdLCA.Click
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        If SituacionFormulario <> Estados.Inactivo Then
            Exit Sub
        End If

        If MsgBox("¿Imprimir conocimiento Ley Cadena alimentaria?", vbQuestion + vbYesNo) = vbYes Then
            ImprimeComunicacion(CnTabla01.ValorCampo("codigo_socio"))
        End If

        If CnTabla01.cuantos <> 0 Then
            If MsgBox("¿Marcar como firmado el documento de la Ley Cadena Alimentaria?", vbQuestion + vbYesNo) = vbYes Then
                Sql = "SELECT * FROM socios_coop WHERE codigo_socio = " & CnTabla01.ValorCampo("codigo_socio")
                RsSocios.Open(Sql, ObjetoGlobal.cn, True)
                If RsSocios.RecordCount = 1 Then
                    RsSocios!doc1 = "S"
                    RsSocios.Update
                End If
                RsSocios.Close
            End If
        End If
    End Sub
End Class

