Imports System.ComponentModel

Public Class FrmFacturasRecibidasSII
Inherits LibreriaModeloMantenimiento.ModeloMantenimiento

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





End Class



























