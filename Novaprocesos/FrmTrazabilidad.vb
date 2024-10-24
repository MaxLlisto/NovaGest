Imports System.ComponentModel
Imports Interop.Microsoft.Office.Interop.Excel
Imports Interop.Office
Imports stdole

Public Class FrmTrazabilidad
    Inherits LibreriaModeloMantenimiento.ModeloMantenimiento

    Dim PesoIndustriaClementinaPequeno As Long
    Dim PesoIndustriaNaranjaPequeno As Long
    Dim PesoIndustriaClementinaGrande As Long
    Dim PesoIndustriaNaranjaGrande As Long
    Dim PesoIndustriaSandia As Long
    Dim PesoVolcadoClementinaPalet As Long
    Dim PesoVolcadoNaranjaPalet As Long
    Dim PesoVolcadoClementinaPalot As Long
    Dim PesoVolcadoNaranjaPalot As Long
    Dim PesoCajaNaranja As Long
    Dim PesoCajaClementina As Long
    Dim PesoCajaNaranjaPrecalibre As Long
    Dim PesoCajaClementinaPrecalibre As Long
    Dim PesoVolcadoPalotOtros

    Dim MiExcel As LibreriaGeneral.EXCEL_VB
    Dim MiHojaExcel As Microsoft.Office.Interop.Excel.Workbook

    Dim RutaPlantillaCierre As String = "\\Srvv02\pt\Plantilla trazabilidad.xlt"
    Dim RutaPlantillaInforme As String = "\\Srvv02\pt\Modelo producción.xlt"
    'Dim RutaPlantillaCierre As String = "C:\Users\paco\Desktop\Plantilla.xltx" 'OJO
    'Dim RutaPlantillaInforme As String = "C:\Users\paco\Desktop\Plantilla.xltx" 'OJO
    Dim RutaMisDocumentos As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    Dim CarpetaExcel As String = "\\Srvv02\Grupos\Calidad\Trazabilidad"

    'Private Const MIS_DOCUMENTOS_USUARIO = 5 'NOSTD
    'Private Const MAX_PATH = 260 'NOSTD

    Private Const cuHoraConfeccion As Integer = 1 'NOSTD
    Private Const cuCodigoVariedad As Integer = 2 'NOSTD
    Private Const cuPeso As Integer = 3 'NOSTD
    Private Const cuOrdenConfeccion As Integer = 4 'NOSTD
    'Private Const cuFechaConfeccion As Integer = 3 'NOSTD
    Private Const cuCajasPalets As Integer = 5 'NOSTD
    Private Const cuOrdenCarga As Integer = 6 'NOSTD
    Private Const cuFechaCarga As Integer = 7 'NOSTD
    Private Const cuHoraCarga As Integer = 8 'NOSTD
    Private Const cuCodigoConfeccion As Integer = 9 'NOSTD
    Private Const cuCodigoPaletizacion As Integer = 10 'NOSTD
    Private Const cuMarca As Integer = 11 'NOSTD
    Private Const cuCalibreTecnico As Integer = 12 'NOSTD
    Private Const cuCalibreComercial As Integer = 13 'NOSTD
    Private Const cuLote As Integer = 14 'NOSTD
    Private Const cuCliente As Integer = 15 'NOSTD
    Private Const cuDestinatario As Integer = 16
    Private Const cuProduccionIntegrada As Integer = 17
    Private Const cuMatricula As Integer = 18 'NOSTD
    Private Const cuObs_Cierre As Integer = 19 'NOSTD
    Private Const cuCodigoPalet As Integer = 20 'NOSTD
    Private Const cuNARANJAS As Integer = 1 'NOSTD
    Private Const cuClementina As Integer = 2 'NOSTD
    Private Const cuSANDIAS As Integer = 3 'NOSTD
    Private Const cuESPECIAL As Integer = 1 'NOSTD
    Private Const cuPRIMERA As Integer = 2 'NOSTD
    Private Const cuSEGUNDA As Integer = 3 'NOSTD
    Private Const cuTERCERA As Integer = 4 'NOSTD
    Private Const cuCUARTA As Integer = 5 'NOSTD
    Private Const cuDESTRIO As Integer = 5 'NOSTD
    Private Const cuQUINTA As Integer = 6 'NOSTD
    Private Const cuINICIAL As Integer = 1 'NOSTD
    Private Const cuFINAL As Integer = 2 'NOSTD
    Private Const cuCAL_ESPECIAL As Integer = 0 'NOSTD
    Private Const cuCAL_PRIMERA As Integer = 1 'NOSTD
    Private Const cuCAL_SEGUNDA As Integer = 2 'NOSTD
    Private Const cuCAL_TERCERA As Integer = 3 'NOSTD
    Private Const cuCAL_CUARTA As Integer = 4 'NOSTD
    Private Const cuCAL_OTRA As Integer = 5 'NOSTD

    Dim oVolcado As New Hashtable
    Dim oVolcadoOrdenado() As String 'OJO

    Dim oConfeccionado As New Hashtable
    Dim oConfeccionadoOrdenado() As String 'OJO

    Dim oCalidades As New Hashtable

    Dim oNecesidades As New Hashtable
    Dim oNecesidadesOrdenado() As String

    Dim oFinal As New Hashtable
    Dim oFinalOrdenado() As String

    Dim Volcado(31, 0) As Long 'NOSTD
    Dim Volcado2(10, 0) As String 'NOSTD
    Dim VolcadoTotal(50) As Long 'NOSTD
    Dim VolcadoHastaAqui(31) As Long 'NOSTD
    Dim Utilizado(31) As Long 'NOSTD
    Dim CuantosVolcados As Long 'NOSTD
    Dim Confeccionado(60, 0) As Long 'NOSTD
    Dim Confeccionado2(10, 0) As String 'NOSTD
    Dim CuantosConfeccionados As Long 'NOSTD
    Dim ConfeccionadoTotal As Long 'NOSTD
    Dim TotalAsignado(10, 0) As Long 'NOSTD
    Dim TotalUtilizado(10, 0) As Long 'NOSTD
    Dim FlagTodos As Boolean 'NOSTD

    Dim Fichero As New cnFichero.cnFichero


    'oVolcado
    'CLAVE: yyMMddHHmmss + 000000 (contador en 'trazab_volcado')
    'ITEM: 00000 (puntero a Volcado())

    'Volcado(0 a 31,?) donde ? es el puntero indicado en oVolcado
    'Volcado(0 a 5,?) kg. calidad (sin descontar merma)
    'Volcado(10 a 15,?) kg. calidad (con merma descontada)
    'Volcado(20 a 25,?) Pendie asignar en proceso final a algún palet
    'Volcado(30,?) kg. totales palet (sim descontar merma)
    'Volcado(31,?) kg. totales palet (descontando merma)

    'Volcado2(10,?)
    'Volcado2(0,?)= Número de albarán
    'Volcado2(1,?)= Número bulto
    'Volcado2(2,?)= Fecha entrada

    'oConfeccionado
    'CLAVE: yyMMddHHmmss + 000000 (absoluteposition en recordset 'trazab_vonfeccion') + 'I' o 'C' (I=Industria,C=Confeccionado) + Variedad (8) + Calibre (6) + CadenaCalidades (6) (N's,S's,X's,O's)
    'ITEM: 0000000 (puntero a Confeccionado())

    'Confeccionado(0 a 60,?) donde ? es el puntero indicado en oConfeccionado
    'Confeccionado(0 a 5,?) kg. teóricos de cada calidad
    'Confeccionado(10 a 15,?) kg. asignados de cada calidad
    'Confeccionado(20 a 25,?) acumulado de lo volcado SIN DESCONTAR MERMA de cada calidad antes de ese palet
    'Confeccionado(30 a 35,?) acumulado de lo volcado DESCONTANDO MERMA de cada calidad antes de ese palet
    'Confeccionado(40 a 45,?) Kg utilizado de esa calidad antes de asignar este palet (lo disponible es, por tanto, volcado(30+j,?)-volcado(40+j,?))
    'Confeccionado(50 a 55,?) Kg posibles para merma final de ajuste de cada calidad
    'Confeccionado(60,?) kg. totales palet

    'Confeccionado2(10,?)
    'Confeccionado2(0,?) = Código palet
    'Confeccionado2(1,?) = Código destinatario
    'Confeccionado2(2,?) = Nombre destinatario
    'Confeccionado2(3,?) = Número albaran
    'Confeccionado2(4,?) = Fecha albaran

    'oNecesidades
    'CLAVE: xxxxxx (N's,S's,X's,O's)
    'ITEM: 0000000000 (peso total necesario de ese grupo) + 0000000000 (peso pendiente de asignar de ese grupo)
    'El grupo de industria se elimina

    'VolcadoTotal(0 a 50)
    'VolcadoTotal(0 a 5) Peso volcado de cada calidad (sin descontar mermas)
    'VolcadoTotal(10 a 15) Peso volcado de cada calidad (descontada merma)
    'VolcadoTotal(20 a 25)
    'VolcadoTotal(30 a 35)
    'VolcadoTotal(40 a 45) Peso libre de cada calidad después de descontar merma e industria
    'VolcadoTotal(50) Peso Volcado Total

    'VolcadoHastaAqui(31) es un array auxiliar que contiene lo volcado hasta un palet confeccionado determinado
    'VolcadoHastaAqui(0 a 5) Peso SIN CONSIDERAR MERMA volcado de cada calidad
    'VolcadoHastaAqui(10 a 15) Peso CON MERMA DESCONTADA volcado de cada calidad
    'VolcadoHastaAqui(30) Total Peso volcado SIN MERMA
    'VolcadoHastaAqui(31) Total Peso volcado CON MERMA DESCONTADA

    'Utilizado(31) es un array auxiliar que contiene lo utilizado hasta un palet determinado
    'Utilizado(0 a 5) Peso SIN CONSIDERAR MERMA utilizado de cada calidad
    'Utilizado(10 a 15) Peso CON MERMA DESCONTADA utilizado de cada calidad
    'Utilizado(30) Total Peso utilizado SIN MERMA
    'Utilizado(31) Total Peso utilizado CON MERMA DESCONTADA


    'Procesos:
    '1) Análisis inicial volcado-confecciòn
    '        1a) Lazo volcados: Para cada palet volcado (también de terceros) calcula los kg. de cada calidad (ignora las cajas y clasifica los volcados genéricos con la media de volcado).
    '            Graba: oVolcado,Volcado(0 a 5,?) con el peso de cada calidad,
    '                   Volcado(30,?) con el peso total del palet volcado,
    '                   VolcadoTotal(0 a 5) con el peso volcado total de cada calidad
    '                   VolcadoTotal(50) con el peso volcado total
    '        1b) Lazo calidades-calibre: Proceso auxiliar que graba oCalidades desde un recordset de la
    '                   tabla calidades_calibre para mejorar el rendimiento en las consultas posteriores
    '        1c) Lazo confección: Para cada palet confeccionado o de industria analiza las calidades con que puede ser confecionado.
    '            Graba: oConfeccionado, oNecesidades (en dos pasos), Confeccionado(60,?) con el peso total palet confeccionado
    '                   ConfeccionadoTotal con el total de los kg. confeccionados
    '2) Análisis posibilidad de cuadre(kg)
    '        2a) Calcula el RatioMermaTotal del día y graba los Volcado(10 a 15,?) y el Volcado(31,?) con los pesos con merma descontada
    '        2b) Calcula para cada palet confeccionado Confeccionado(20 a 25,?) con el acumulado de lo volcado antes de ese palet sin descontar merma
    '                   y Confeccionado(30 a 35,?) con el acumulado de lo volcado antes de ese palet con la merma descontada
    '        2c) Calcula para cada palet de industria los pesos asignados Confeccionado(0 a 5,?).
    '            Graba todos los datos del array Utilizado()
    '3) Análisis cuadre calidades
    '        Graba VolcadoTotal(40 a 45) con los datos de los kg. libres netos (después de industria y de merma) por calidad.
    '        Reparte esos kg entre los grupos y categorías y rellena el array de reparto teórico TotalAsignado(Calidades,Grupos)
    '4)

    'DECLARACIÓN DE API DE WINDOWS PARA RECUPERAR LA RUTA DE LOS DIRECTORIOS DEL SISTEMA
    '    Private Declare Function SHGetSpecialFolderPath Lib "shell32.dll" Alias "SHGetSpecialFolderPathA" (ByVal hwnd As Long, ByVal sPath As String, ByVal Folder As Long, ByVal Create As Long) As Long 'NOSTD




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

    Protected Overrides Function HOOK_Despues_de_InicializarControles() As Boolean
        'If Trim(ObjetoGlobal.UsuarioActual) = "informatica" Then
        '    CnEdicion033.MaximaFecha = "1/1/2023"
        'End If
        TabPage03.Text = "Trazabilidad"
        PBCabecera.Visible = False
        Pic0.Visible = False
        Pic0.Visible = False
        PB0.Visible = False
        PB0.Visible = False
        Pic1.Visible = False
        Pic1.Visible = False
        PB1.Visible = False
        PB1.Visible = False
        Pic2.Visible = False
        Pic2.Visible = False
        PB2.Visible = False
        PB2.Visible = False
        PB22.Visible = False
        PB22.Visible = False
        PB23.Visible = False
        PB23.Visible = False
        PB3.Visible = False
        Lbl3.Text = ""
        Pic3.Visible = False
        Pic3.Visible = False
        Pic4.Visible = False
        Pic4.Visible = False
        PB4.Visible = False
        PB4.Visible = False
        Pic5.Visible = False
        Pic5.Visible = False
        PB5.Visible = False
        PB5.Visible = False
        Pic6.Visible = False
        Pic6.Visible = False
        PB6.Visible = False
        PB6.Visible = False
        Pic7.Visible = False
        Pic7.Visible = False
        PB7.Visible = False
        PB7.Visible = False
        LW.Visible = False
        LeerPesosTeoricos()

        HOOK_Despues_de_InicializarControles = False
    End Function

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
    Private Function BorrarDatos(SQL As String) As Boolean
        Dim Rs As New cnRecordset.CnRecordset

        BorrarDatos = False

        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir el recordset de borrado" + vbCrLf + Trim(SQL))
            Exit Function
        End If
        Do While Rs.RecordCount > 0
            Rs.MoveFirst()
            Rs.Delete()
        Loop
        Rs.Close()
        BorrarDatos = True

    End Function


    Private Sub CmdGrabarVolcados_Click(sender As Object, e As EventArgs) Handles CmdGrabarVolcados.Click
        ProcesarVolcados()
    End Sub

    Private Sub ProcesarVolcados()
        Dim FechaProceso1 As Date, FechaProceso2 As Date
        Dim RsGrabar As New cnRecordset.CnRecordset, Contador As Long
        Dim SQL As String
        Dim Bulto As String, Cajas As Integer, Variedad As String, TipoVariedad As String
        Dim Tipo As String, Clave As Integer
        Dim EjercicioEntrada As String, SerieEntrada As String, AlbaranEntrada As String
        Dim RsEtiquetas As New cnRecordset.CnRecordset, RsEntradas As New cnRecordset.CnRecordset, RsAlbPre As New cnRecordset.CnRecordset
        Dim RsVariedad As New cnRecordset.CnRecordset, RsCalidades As New cnRecordset.CnRecordset
        Dim RsEntrada As New cnRecordset.CnRecordset, RsPrecalibrado As New cnRecordset.CnRecordset
        Dim RsEnvases As New cnRecordset.CnRecordset
        Dim FlagHayEntrada As Boolean, FlagHayPrecalibrado As Boolean
        Dim FechaConfeccion As Date, FechaVolcado As Date
        Dim FlagGrabar As Boolean
        Dim i As Integer, j As Integer, jj As Integer
        Dim Pesos(10) As Long
        Dim DescripcionVariedad As String, CodigoCampo As Long
        Dim Cadena As String
        Dim RsAux As New cnRecordset.CnRecordset
        Dim CodigoFamilia As String
        Dim CodigoSubfamilia As String
        Dim EsElPrimero As Boolean, PesoMedio As Long, PesoMedioPrimero As Long

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        If CnTabla01.CuantosRegistros <= 0 Then
            MsgBox("Debe seleccionar previamente el proceso de trazabilidad.")
            Exit Sub
        End If

        Cadena = Trim(CnTabla01.Rs("familia_variedad"))

        If Cadena <> "CLE" And Cadena <> "NAR" And Cadena <> "SAT" And Cadena <> "HIB" And Cadena <> "SANDB" And Cadena <> "SANDN" And Cadena <> "SANDP" And Cadena <> "SANNS" And Cadena <> "COLI" And Cadena <> "ALCA" And Cadena <> "NAB" And Cadena <> "ROJ" Then
            MsgBox("Para el análisis de trazabilidad debe indicar una de las siguientes familias:" + vbCrLf + "NARANJA (NAR), CLEMENTINA (CLE), SATSUMA (SAT), HIBRIDOS (HIB), SANDIA BLANCA(SANDB), SANDIA NEGRA(SANDN), SANDIA NEGRA SIN PEPITAS (SANNS), SANDIA BLANCA CON PEPITAS (SANDP), COLIFLOR (COLI) o ALCACHOFA (ALCA) o naranja blanca (NAB)." + vbCrLf + vbCrLf + "Se cancelará el proceso.")
            Exit Sub
        End If
        FechaProceso1 = CDate(CnTabla01.Rs("fecha_inicio"))
        FechaProceso2 = CDate(CnTabla01.Rs("fecha_fin"))

        SQL = "SELECT * FROM HCO_FICHADAS WHERE FECHA between '" + Trim(FechaProceso1) + "' AND '" + Trim(FechaProceso2) + "' AND (clave_marcaje IN (2300,2310,2320,2400,3000,3100,3200,3300,4300,4400,4420,4460,4500,4600,4700,4720,4760,4800) OR (LEFT(accion,7) = 'VOLCADO' and not accion like '%FRUTA%')) AND LEFT(incidencia,9)='Correcto:' ORDER BY FECHA,HORA"
        If RsAux.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla hco_fichadas")
            Exit Sub
        End If
        If RsAux.RecordCount = 0 Then
            MsgBox("No existen entradas volcadas en el periodo indicado.")
        End If

        Cadena = "SELECT * FROM TRAZAB_VOLCADO WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "'"
        Cadena = Trim(Cadena) + " AND CODIGO_PROCESO = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "'"
        If BorrarDatos(Cadena) = False Then
            MsgBox("No es posible eliminar los volcados anteriores del proceso indicado")
            Exit Sub
        End If

        PBCabecera.Value = 0
        PBCabecera.Minimum = 0
        PBCabecera.Maximum = RsAux.RecordCount

        SQL = "SELECT * FROM TRAZAB_VOLCADO WHERE 1=0"
        If RsGrabar.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla trazab_volcado (grabación)")
            Exit Sub
        End If

        PBCabecera.Visible = True
        For jj = 1 To RsAux.RecordCount
            RsAux.AbsolutePosition = jj

            FlagGrabar = True
            PBCabecera.Value = jj '* RsAux.RecordCount / 100
            For i = 0 To 10 : Pesos(i) = 0 : Next
            EjercicioEntrada = ""
            SerieEntrada = ""
            AlbaranEntrada = ""
            FlagHayEntrada = False
            FlagHayPrecalibrado = False
            DescripcionVariedad = ""
            CodigoCampo = 0
            Tipo = Trim(RsAux!tipomarca)
            Clave = RsAux!clave_marcaje

            If RsAux!Fecha >= FechaProceso2 And RsAux!hora > CnTabla01.Rs("hora_fin") Then FlagGrabar = False
            If RsAux!Fecha <= FechaProceso1 And RsAux!hora < CnTabla01.Rs("hora_inicio") Then FlagGrabar = False
            If FlagGrabar = True Then
                EjercicioEntrada = "0"
                SerieEntrada = "0"
                AlbaranEntrada = "0"
                Bulto = ""
                Cajas = -1


                If Trim(RsAux!Accion) = "VOLCADO NOSE" Then ' Volcado normal
                    CodigoSubfamilia = ""
                    CodigoFamilia = ""
                    SQL = "SELECT e.serie_albaran, e.numero_albaran, v.codigo_variedad, v.codigo_familia, v.codigo_subfamilia FROM Entradas_albaranes e LEFT JOIN variedades v ON v.empresa = e.empresa AND v.codigo_variedad = e.codigo_variedad "
                    SQL = SQL + "WHERE e.empresa = '" & RsAux!empresa & "' AND e.Ejercicio='" & RsAux!Ejercicio & "' AND e.serie_albaran = '" & RsAux!serie_albaran & "' AND "
                    SQL = SQL + "e.numero_albaran = " & RsAux!numero_albaran
                    If RsEntradas.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla entradas_albaranes")
                        Exit Sub
                    End If

                    If RsEntradas.RecordCount > 0 Then
                        If Not IsDBNull(RsEntradas!Codigo_Subfamilia) And Trim("" & RsEntradas!Codigo_Subfamilia) > "0" Then
                            CodigoFamilia = Trim(RsEntradas!Codigo_familia)
                            CodigoSubfamilia = Trim(RsEntradas!Codigo_Subfamilia)
                        Else
                            CodigoFamilia = Trim(RsEntradas!Codigo_familia)
                            CodigoSubfamilia = Trim(RsEntradas!Codigo_familia)
                        End If
                    End If

                    FlagHayEntrada = False
                    If Trim(CnTabla01.Rs("familia_variedad")) = Trim(CodigoSubfamilia) Then
                        FlagHayEntrada = True
                    End If

                    If FlagHayEntrada = True Then
                        RsGrabar.AddNew()
                        Contador = Contador + 1
                        RsGrabar!Empresa = Trim(CnTabla01.Rs("Empresa"))
                        RsGrabar!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                        RsGrabar!Contador = Contador
                        RsGrabar!tipo_marcaje = Trim(Tipo)
                        RsGrabar!marcaje = Trim(RsAux!codigo)
                        RsGrabar!fecha_marcaje = Format(CDate(RsAux!Fecha), "dd/MM/yyyy")
                        RsGrabar!hora_marcaje = Format(CDate(RsAux!hora), "HH:mm:ss")
                        RsGrabar!Contador_fichero = RsAux!Orden
                        RsGrabar!Ejercicio_albaran = ""
                        RsGrabar!serie_albaran = ""
                        RsGrabar!numero_albaran = 0
                        RsGrabar!Kilos_e = 0
                        RsGrabar!Kilos_1 = 0
                        RsGrabar!Kilos_2 = 0
                        RsGrabar!Kilos_3 = 0
                        RsGrabar!Kilos_4 = 0
                        RsGrabar!Kilos_5 = 0
                        RsGrabar!Kilos_6 = 0
                        RsGrabar!Kilos_otros = 0
                        RsGrabar!Usuario = CStr(ObjetoGlobal.UsuarioActual)
                        RsGrabar!Estado = "AC"
                        RsGrabar!Fecha_Actualiz = Now
                        RsGrabar!Contador_proceso = Contador * 1000

                        If Trim(CodigoSubfamilia) = "NAR" And Tipo = "PALET" Then
                            RsGrabar!descripcion_variedad = "Palet genérico naranja"
                            RsGrabar!Kilos = PesoVolcadoNaranjaPalet
                        ElseIf Trim(CodigoSubfamilia) = "ROJ" And Tipo = "PALET" Then
                            RsGrabar!descripcion_variedad = "Palet genérico naranja roja"
                            RsGrabar!Kilos = PesoVolcadoNaranjaPalet
                        ElseIf Trim(CodigoSubfamilia) = "NAB" And Tipo = "PALET" Then
                            RsGrabar!descripcion_variedad = "Palet genérico naranja blanca"
                            RsGrabar!Kilos = PesoVolcadoNaranjaPalet
                        ElseIf Trim(CodigoSubfamilia) = "CLE" And Trim(Tipo) = "PALET" Then
                            RsGrabar!descripcion_variedad = "Palet genérico clementina"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalet
                        ElseIf Trim(CodigoSubfamilia) = "SAT" And Trim(Tipo) = "PALET" Then
                            RsGrabar!descripcion_variedad = "Palet genérico satsuma"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalet
                        ElseIf Trim(CodigoSubfamilia) = "HIB" And Trim(Tipo) = "PALET" Then
                            RsGrabar!descripcion_variedad = "Palet genérico hibridos"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalet
                        ElseIf Mid(Trim(CodigoSubfamilia), 1, 3) = "SAN" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico sandía"
                            RsGrabar!Kilos = PesoVolcadoPalotOtros
                        ElseIf Trim(CodigoSubfamilia) = "NAR" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico naranja"
                            RsGrabar!Kilos = PesoVolcadoNaranjaPalot
                        ElseIf Trim(CodigoSubfamilia) = "ROJ" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico naranja roja"
                            RsGrabar!Kilos = PesoVolcadoNaranjaPalot
                        ElseIf Trim(CodigoSubfamilia) = "NAB" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico naranja blanca"
                            RsGrabar!Kilos = PesoVolcadoNaranjaPalot
                        ElseIf Trim(CodigoSubfamilia) = "CLE" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico clementina"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalot
                        ElseIf Trim(CodigoSubfamilia) = "SAT" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico satsuma"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalot
                        ElseIf Trim(CodigoSubfamilia) = "HIB" And Trim(Tipo) = "PALOT" Then
                            RsGrabar!descripcion_variedad = "Palot genérico hibridos"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalot
                        Else
                            RsGrabar!descripcion_variedad = "Palot genérico Clementina"
                            RsGrabar!Kilos = PesoVolcadoClementinaPalot
                        End If
                        RsGrabar!Kilos_4 = 100
                        RsGrabar!codigo_campo = 0
                        RsGrabar.Update()
                    End If
                    RsEntradas.Close()

                ElseIf Trim(RsAux!Accion) = "VOLCADO DE PRECALIBR" Then ' Volcado precalibrado
                    SQL = "SELECT p.*, v.descripcion as descv, v.codigo_familia, v.codigo_subfamilia FROM palets_precalibre p JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = p.codigo_variedad WHERE p.empresa = '" + Trim(RsAux!empresa) + "' and p.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND p.referencia = '" + Trim(RsAux!codigo) + "'"
                    If RsPrecalibrado.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla palets_precalibre")
                        Exit Sub
                    End If

                    If RsPrecalibrado.RecordCount = 1 Then

                        CodigoSubfamilia = ""
                        CodigoFamilia = ""

                        If Not IsDBNull(RsPrecalibrado!Codigo_Subfamilia) And Trim("" & RsPrecalibrado!Codigo_Subfamilia) > "0" Then
                            CodigoFamilia = Trim(RsPrecalibrado!Codigo_familia)
                            CodigoSubfamilia = Trim(RsPrecalibrado!Codigo_Subfamilia)
                        Else
                            CodigoFamilia = Trim(RsPrecalibrado!Codigo_familia)
                            CodigoSubfamilia = Trim(RsPrecalibrado!Codigo_familia)
                        End If

                        FlagHayEntrada = False
                        If Trim(CnTabla01.Rs("familia_variedad")) = Trim(CodigoSubfamilia) Then
                            FlagHayEntrada = True
                        End If

                        If FlagHayEntrada Then
                            FlagHayPrecalibrado = True
                            Variedad = Trim(RsPrecalibrado!codigo_variedad) + " - " + Trim(RsPrecalibrado!descv)
                            TipoVariedad = Mid(RsPrecalibrado!codigo_variedad, 1, 2)
                            Pesos(0) = 0
                            If Trim(TipoVariedad) = "02" Then
                                Pesos(0) = Math.Round(RsPrecalibrado!Bultos * PesoCajaNaranjaPrecalibre, 0)
                            ElseIf Trim(TipoVariedad) = "01" Then
                                If Trim(RsPrecalibrado!codigo_variedad) = "019" Or Trim(RsPrecalibrado!codigo_variedad) = "0115" Then
                                    Pesos(0) = Math.Round(RsPrecalibrado!Bultos * (PesoCajaClementinaPrecalibre + 1), 0)
                                Else
                                    Pesos(0) = Math.Round(RsPrecalibrado!Bultos * PesoCajaClementinaPrecalibre, 0)
                                End If
                            ElseIf Trim(TipoVariedad) = "40" Then
                                Pesos(0) = Math.Round(RsPrecalibrado!Bultos * PesoVolcadoPalotOtros, 0)
                            End If
                            FechaConfeccion = CDate(Format(RsPrecalibrado!fecha_confeccion, "dd/MM/yyyy"))
                            FechaVolcado = CDate(Format(RsPrecalibrado!fecha_volcado, "dd/MM/yyyy"))

                            SQL = "SELECT * FROM palets_precalibre_t WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) & "' AND codigo_palet=" + CStr(RsPrecalibrado!Codigo_palet)
                            If RsAlbPre.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla palets_precalibre_t")
                                Exit Sub
                            End If
                            If RsAlbPre.RecordCount > 0 And (FechaConfeccion <> FechaVolcado) Then
                                EsElPrimero = True
                                PesoMedio = 0
                                PesoMedioPrimero = 0
                                PesoMedio = Math.Round(Pesos(0) / RsAlbPre.RecordCount, 0)
                                PesoMedioPrimero = Pesos(0) - (PesoMedio * (RsAlbPre.RecordCount - 1))

                                For j = 1 To RsAlbPre.RecordCount
                                    RsAlbPre.AbsolutePosition = j
                                    RsGrabar.AddNew()
                                    Contador = Contador + 1
                                    RsGrabar!Empresa = Trim(CnTabla01.Rs("Empresa"))
                                    RsGrabar!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                                    RsGrabar!Contador = Contador
                                    RsGrabar!tipo_marcaje = Trim(Tipo)
                                    RsGrabar!marcaje = Trim(RsAux!codigo)
                                    RsGrabar!fecha_marcaje = Format(CDate(RsAux!Fecha), "dd/MM/yyyy")
                                    RsGrabar!hora_marcaje = Format(CDate(RsAux!hora), "HH:mm:ss")
                                    RsGrabar!Contador_fichero = RsAux!Orden
                                    RsGrabar!Ejercicio_albaran = Trim(RsPrecalibrado!Ejercicio)
                                    RsGrabar!serie_albaran = RsAlbPre!serie_albaran
                                    RsGrabar!numero_albaran = RsAlbPre!numero_albaran
                                    RsGrabar!Kilos_e = 0
                                    RsGrabar!Kilos_1 = 0
                                    RsGrabar!Kilos_2 = 0
                                    RsGrabar!Kilos_3 = 0
                                    RsGrabar!Kilos_4 = 0
                                    RsGrabar!Kilos_5 = 0
                                    RsGrabar!Kilos_6 = 0
                                    RsGrabar!Kilos_otros = 0
                                    RsGrabar!Usuario = CStr(ObjetoGlobal.UsuarioActual)
                                    RsGrabar!Estado = "AC"
                                    RsGrabar!Fecha_Actualiz = Now
                                    RsGrabar!Contador_proceso = Contador * 1000
                                    RsGrabar!descripcion_variedad = Variedad
                                    RsGrabar!Kilos = IIf(EsElPrimero, PesoMedioPrimero, PesoMedio)
                                    RsGrabar!codigo_campo = 0
                                    RsGrabar.Update()
                                    EsElPrimero = False
                                Next
                            Else
                                RsGrabar.AddNew()
                                Contador = Contador + 1
                                RsGrabar!empresa = Trim(CnTabla01.Rs("empresa"))
                                RsGrabar!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                                RsGrabar!Contador = Contador
                                RsGrabar!tipo_marcaje = Trim(Tipo)
                                RsGrabar!marcaje = Trim(RsAux!codigo)
                                RsGrabar!fecha_marcaje = Format(CDate(RsAux!Fecha), "dd/MM/yyyy")
                                RsGrabar!hora_marcaje = Format(CDate(RsAux!Hora), "HH:mm:ss")
                                RsGrabar!Contador_fichero = RsAux!Orden
                                RsGrabar!Ejercicio_albaran = RsPrecalibrado!Ejercicio
                                RsGrabar!serie_albaran = ""
                                RsGrabar!numero_albaran = 0
                                RsGrabar!Kilos_e = 0
                                RsGrabar!Kilos_1 = 0
                                RsGrabar!Kilos_2 = 0
                                RsGrabar!Kilos_3 = 0
                                RsGrabar!Kilos_4 = 0
                                RsGrabar!Kilos_5 = 0
                                RsGrabar!Kilos_6 = 0
                                RsGrabar!Kilos_otros = 0
                                RsGrabar!Usuario = CStr(ObjetoGlobal.UsuarioActual)
                                RsGrabar!Estado = "AC"
                                RsGrabar!Fecha_Actualiz = Now
                                RsGrabar!contador_proceso = Contador * 1000
                                RsGrabar!descripcion_variedad = Variedad
                                RsGrabar!Kilos = Pesos(0)
                                RsGrabar!codigo_campo = 0
                                RsGrabar.Update()
                            End If
                            RsAlbPre.Close()
                        End If
                    End If
                    RsPrecalibrado.Close()
                Else
                    If Trim(RsAux!Accion) = "VOLCADO CAJA" Then
                        SQL = "SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(RsAux!empresa) + "' AND EJERCICIO = '" + Trim(RsAux!Ejercicio) + "' AND SERIE_ALBARAN = '" + Trim(RsAux!serie_albaran) + "' AND NUMERO_ALBARAN = " + CStr(RsAux!numero_albaran)
                        If RsEtiquetas.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla entradas_bultos")
                            Exit Sub
                        End If
                        If RsEtiquetas.RecordCount > 0 Then
                            EjercicioEntrada = Trim(RsEtiquetas!Ejercicio)
                            SerieEntrada = Trim(RsEtiquetas!serie_albaran)
                            AlbaranEntrada = Trim(RsEtiquetas!numero_albaran)
                            FlagHayEntrada = True
                        End If
                        RsEtiquetas.Close()
                        Cajas = 1
                    ElseIf Trim(RsAux!Accion) = "VOLCADO" Then
                        Bulto = RsAux!Bulto
                        Cajas = RsAux!Cajas
                        SQL = "SELECT * FROM ENTRADAS_BULTOS WHERE EMPRESA = '" + Trim(RsAux!empresa) + "' AND EJERCICIO = '" + Trim(RsAux!Ejercicio) + "' AND SERIE_ALBARAN = '" + Trim(RsAux!serie_albaran) + "' AND NUMERO_ALBARAN = " + CStr(RsAux!numero_albaran)
                        If RsEtiquetas.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla entradas_bultos")
                            Exit Sub
                        End If
                        If RsEtiquetas.RecordCount > 0 Then
                            EjercicioEntrada = Trim(RsEtiquetas!Ejercicio)
                            SerieEntrada = Trim(RsEtiquetas!serie_albaran)
                            AlbaranEntrada = Trim(RsEtiquetas!numero_albaran)
                            If RsEtiquetas!Cajas > 0 Then
                                Tipo = "PALET"
                            Else
                                Tipo = "PALOT"
                            End If
                        End If
                        RsEtiquetas.Close()
                    End If
                    FlagHayEntrada = False
                    If Trim(AlbaranEntrada) > "" Then
                        SQL = "SELECT codigo_campo as ccampo, * FROM entradas_albaranes WHERE  empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio ='" & Trim(EjercicioEntrada) & "' AND serie_albaran = '" & Trim(SerieEntrada) & "' AND numero_albaran = " & Trim(AlbaranEntrada) & " AND tipo_entrada <> 'T'"
                        SQL = Trim(SQL) + " UNION "
                        SQL = Trim(SQL) + " SELECT campo_terceros as ccampo, * FROM entradas_albaranes WHERE  empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio ='" & Trim(EjercicioEntrada) & "' AND serie_albaran = '" & Trim(SerieEntrada) & "' AND numero_albaran = " & Trim(AlbaranEntrada) & " AND tipo_entrada = 'T'"
                        If RsEntrada.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla campos (campos terceros)")
                            Exit Sub
                        End If
                        If RsEntrada.RecordCount > 0 Then FlagHayEntrada = True
                    End If
                    If FlagHayEntrada = True Then
                        CodigoCampo = RsEntrada!ccampo
                        SQL = "SELECT * FROM VARIEDADES WHERE EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND CODIGO_VARIEDAD = '" + Trim(RsEntrada!codigo_variedad) + "'"
                        If RsVariedad.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla variedades")
                            Exit Sub
                        End If
                        If RsVariedad.RecordCount = 0 Then
                            Variedad = "NO EXISTE"
                        Else
                            Variedad = Trim(RsEntrada!codigo_variedad) + " - " + Trim(RsVariedad!Descripcion)
                            TipoVariedad = Mid(RsEntrada!codigo_variedad, 1, 2)

                            CodigoSubfamilia = ""
                            CodigoFamilia = ""

                            If Not IsDBNull(RsVariedad!Codigo_Subfamilia) And Trim("" & RsVariedad!Codigo_Subfamilia) > "0" Then
                                CodigoFamilia = Trim(RsVariedad!Codigo_familia)
                                CodigoSubfamilia = Trim(RsVariedad!Codigo_Subfamilia)
                            Else
                                CodigoFamilia = Trim(RsVariedad!Codigo_familia)
                                CodigoSubfamilia = Trim(RsVariedad!Codigo_familia)
                            End If
                        End If
                        FlagGrabar = False
                        If Trim(CodigoSubfamilia) = Trim(CnTabla01.Rs("familia_variedad")) Then
                            FlagGrabar = True
                        End If
                        DescripcionVariedad = Trim(CodigoSubfamilia) & "-" & Trim(RsVariedad!Descripcion)
                        RsVariedad.Close()
                        SQL = "SELECT * FROM entradas_clasif WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio ='" & Trim(EjercicioEntrada) & "' AND serie_albaran = '" & Trim(SerieEntrada) & "' AND numero_albaran = " & Trim(AlbaranEntrada) & " AND tipo_clasificacion = 'LIQ'"
                        If RsCalidades.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla entradas_clasif")
                            Exit Sub
                        End If
                        If RsCalidades.RecordCount > 0 Then
                            For j = 1 To RsCalidades.RecordCount
                                RsCalidades.AbsolutePosition = j
                                If RsCalidades!CODIGO_CALIDAD < 10 Then
                                    Pesos(RsCalidades!CODIGO_CALIDAD) = RsCalidades!kg_calidad
                                End If
                            Next
                        End If
                        RsCalidades.Close()
                        For i = 1 To 9 : Pesos(10) = Pesos(10) + Pesos(i) : Next
                        If Pesos(10) > 0 Then
                            For i = 1 To 9 : Pesos(i) = Math.Round(100 * Pesos(i) / Pesos(10), 0) : Next
                            For i = 7 To 9 : Pesos(6) = Pesos(6) + Pesos(i) : Pesos(i) = 0 : Next
                        End If
                        If TipoVariedad = "02" And Cajas > 0 Then
                            Pesos(0) = Math.Round(Cajas * PesoCajaNaranja, 0)
                        ElseIf TipoVariedad = "01" And Cajas > 0 Then
                            If Trim(RsEntrada!codigo_variedad) = "019" Or Trim(RsEntrada!codigo_variedad) = "0115" Then
                                Pesos(0) = Math.Round(Cajas * (PesoCajaClementina + 1), 0)
                            Else
                                Pesos(0) = Math.Round(Cajas * PesoCajaClementina, 0)
                            End If
                        ElseIf TipoVariedad = "02" Then
                            Pesos(0) = PesoVolcadoClementinaPalot
                        ElseIf TipoVariedad = "01" Then
                            Pesos(0) = PesoVolcadoNaranjaPalot
                        ElseIf TipoVariedad = "40" Then
                            SQL = "SELECT SUM(numero_envases) as cuantos FROM entradas_envases WHERE EMPRESA = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio ='" & Trim(EjercicioEntrada) & "' AND serie_albaran = '" & Trim(SerieEntrada) & "' AND numero_albaran = " & Trim(AlbaranEntrada) & " AND entrada_salida='E' AND codigo_envase <> 'VPALOT' "
                            If RsEnvases.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla entradas_envases")
                                Exit Sub
                            End If
                            If RsEnvases.RecordCount > 0 Then ' si lo encuentro pongo el correcto
                                If RsEnvases!cuantos > 0 Then
                                    Pesos(0) = Math.Round(RsEntrada!kg_a_liquidar / RsEnvases!cuantos, 0)
                                Else
                                    Pesos(0) = PesoVolcadoPalotOtros
                                End If
                            Else    ' si no lo encuentro pongo la media
                                Pesos(0) = PesoVolcadoPalotOtros
                            End If
                            RsEnvases.Close()
                        ElseIf TipoVariedad = "29" Then
                            SQL = "SELECT SUM(numero_envases) as cuantos FROM entradas_envases WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio ='" & Trim(EjercicioEntrada) & "' AND serie_albaran = '" & Trim(SerieEntrada) & "' AND numero_albaran = " & Trim(AlbaranEntrada) & " AND entrada_salida='E' AND codigo_envase <> 'VPALOT' "
                            If RsEnvases.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla entradas_envases")
                                Exit Sub
                            End If
                            If RsEnvases.RecordCount > 0 Then ' si lo encuentro pongo el correcto
                                If RsEnvases!cuantos > 0 Then
                                    Pesos(0) = Math.Round(RsEntrada!kg_a_liquidar / RsEnvases!cuantos, 0)
                                Else
                                    Pesos(0) = PesoVolcadoPalotOtros
                                End If
                            Else    ' si no lo encuentro pongo la media
                                Pesos(0) = PesoVolcadoPalotOtros
                            End If
                            RsEnvases.Close()
                        End If
                    End If

                    If FlagGrabar = True Then
                        RsGrabar.AddNew()
                        Contador = Contador + 1
                        RsGrabar!empresa = Trim(CnTabla01.Rs("empresa"))
                        RsGrabar!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                        RsGrabar!Contador = Contador
                        RsGrabar!tipo_marcaje = Trim(Tipo)
                        RsGrabar!marcaje = Trim(RsAux!codigo)
                        RsGrabar!fecha_marcaje = Format(CDate(RsAux!Fecha), "dd/MM/yyyy")
                        RsGrabar!hora_marcaje = Format(CDate(RsAux!Hora), "HH:mm:ss")
                        RsGrabar!Contador_fichero = RsAux!Orden
                        RsGrabar!Ejercicio_albaran = Trim(EjercicioEntrada)
                        RsGrabar!serie_albaran = Trim(SerieEntrada)
                        RsGrabar!numero_albaran = AlbaranEntrada
                        RsGrabar!Kilos = Pesos(0)
                        RsGrabar!Kilos_e = Pesos(1)
                        RsGrabar!Kilos_1 = Pesos(2)
                        RsGrabar!Kilos_2 = Pesos(3)
                        RsGrabar!Kilos_3 = Pesos(4)
                        RsGrabar!Kilos_4 = Pesos(5)
                        RsGrabar!Kilos_5 = Pesos(6)
                        RsGrabar!Kilos_6 = Pesos(7)
                        RsGrabar!Kilos_otros = Pesos(8)
                        RsGrabar!Usuario = CStr(ObjetoGlobal.UsuarioActual)
                        RsGrabar!Estado = "AC"
                        RsGrabar!Fecha_Actualiz = Now
                        RsGrabar!contador_proceso = Contador * 1000
                        RsGrabar!descripcion_variedad = Trim(DescripcionVariedad)
                        RsGrabar!codigo_campo = CodigoCampo
                        RsGrabar.Update()
                    End If
                    Try
                        RsEntrada.Close()
                    Catch
                    End Try
                End If
            End If
        Next
        RsAux.Close()
        RsGrabar.Close()
        PBCabecera.Visible = False
        CnTabla01.MostrarRegistroActual(True)

        TabGeneral.SelectedIndex = 1

    End Sub

    Private Sub CmdGrabarConfeccion_Click(sender As Object, e As EventArgs) Handles CmdGrabarConfeccion.Click
        ProcesarConfeccion()
    End Sub

    Private Sub ProcesarConfeccion()
        Dim FechaProceso1 As Date, FechaProceso2 As Date, RsGrabar As New cnRecordset.CnRecordset, Contador As Long
        Dim SQL As String, Variedad As String
        Dim RsAux As New cnRecordset.CnRecordset, RsEntrada As New cnRecordset.CnRecordset, RsVariedad As New cnRecordset.CnRecordset
        Dim RsPalets As New cnRecordset.CnRecordset, RsOrdenesConfeccion As New cnRecordset.CnRecordset
        Dim RsPrecalibrado As New cnRecordset.CnRecordset
        Dim FlagGrabar As Boolean
        Dim jj As Integer, Clave As Integer, Orden As Long
        Dim Accion As String
        Dim CodigoPalet As String, Peso As Long, Cadena As String
        Dim CodigoFamilia As String, CodigoSubfamilia As String

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        If CnTabla01.CuantosRegistros <= 0 Then
            MsgBox("Debe seleccionar previamente el proceso de trazabilidad.")
            Exit Sub
        End If

        Cadena = Trim(CnTabla01.Rs("familia_variedad"))
        If Cadena <> "CLE" And Cadena <> "NAR" And Cadena <> "SAT" And Cadena <> "HIB" And Cadena <> "SANDB" And Cadena <> "SANDN" And Cadena <> "SANDP" And Cadena <> "SANNS" And Cadena <> "COLI" And Cadena <> "ALCA" And Cadena <> "NAB" And Cadena <> "ROJ" Then
            MsgBox("Para el análisis de trazabilidad debe indicar una de las siguientes familias:" + vbCrLf + "NARANJA (NAR), CLEMENTINA (CLE), SATSUMA (SAT), HIBRIDOS (HIB), SANDIA BLANCA(SANDB), SANDIA NEGRA(SANDN), SANDIA NEGRA SIN PEPITAS (SANNS), SANDIA BLANCA CON PEPITAS (SANDP), COLIFLOR (COLI) o ALCACHOFA (ALCA) o naranja blanca (NAB) o naranja roja (ROJ)." + vbCrLf + vbCrLf + "Se cancelará el proceso.")
            Exit Sub
        End If
        FechaProceso1 = CDate(CnTabla01.Rs("fecha_inicio"))
        FechaProceso2 = CDate(CnTabla01.Rs("fecha_fin"))

        'ANTIGUO SQL = "SELECT * FROM HCO_FICHADAS WHERE FECHA between '" + Format(FechaProceso1, "dd-MM-yyyy") + "' AND '" + Format(FechaProceso2, "dd-MM-yyyy") + "' AND clave_marcaje <>10000 AND (ACCION LIKE '%FABRICACION_P%' OR ACCION LIKE '%INDUSTRIA%') ORDER BY FECHA,HORA"

        SQL = "SELECT empresa as empresa, codigo AS codigo, fecha as fecha, clave_marcaje as clave_marcaje, accion as accion, hora as hora, contador_proceso as contador_proceso, orden as orden, tipomarca as tipomarca FROM HCO_FICHADAS "
        SQL = SQL + "WHERE (left(HCO_FICHADAS.incidencia,9) = 'Correcto:' OR left(HCO_FICHADAS.incidencia,25) = 'Bulto de industria creado' or left(HCO_FICHADAS.incidencia,19) = 'PALET CONFECCIONADO' ) and FECHA between '" + Format(FechaProceso1, "dd-MM-yyyy") + "' AND '" + Format(FechaProceso2, "dd-MM-yyyy") + "' AND clave_marcaje <>10000 AND (ACCION LIKE '%FABRICACION_P%' OR ACCION LIKE '%INDUSTRIA%') "
        SQL = SQL + "UNION SELECT empresa as empresa, referencia as codigo, fecha_confeccion as fecha, 99 as clave_marcaje, 'CONFECCION PRECALIBRADO' AS accion, convert(varchar, CONVERT(varchar, CONVERT(datetime, hora_confeccion), 108), 108) as hora, codigo_palet AS contador_proceso, '1' AS orden, 'CPRE' AS tipomarca FROM palets_precalibre "
        SQL = SQL + "WHERE fecha_confeccion between '" + Format(FechaProceso1, "dd-MM-yyyy") + "' AND '" + Format(FechaProceso2, "dd-MM-yyyy") + "' "
        SQL = SQL + "ORDER BY FECHA,HORA"

        If RsAux.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla hco_fichadas")
            Exit Sub
        End If
        If RsAux.RecordCount = 0 Then
            MsgBox("No existen palets confeccionados en el periodo indicado.")
        End If

        Cadena = "SELECT * FROM TRAZAB_CONFECCION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "'"
        Cadena = Trim(Cadena) + " AND CODIGO_PROCESO = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "'"
        If BorrarDatos(Cadena) = False Then
            MsgBox("No es posible eliminar los palets confeccionados anteriores del proceso indicado")
            Exit Sub
        End If

        PBCabecera.Value = 0
        PBCabecera.Minimum = 0
        PBCabecera.Maximum = RsAux.RecordCount

        SQL = "SELECT * FROM TRAZAB_CONFECCION WHERE 1=0"
        If RsGrabar.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla trazab_confeccion (grabación)")
            Exit Sub
        End If

        Contador = 0
        If RsAux.RecordCount > 0 Then
            PBCabecera.Visible = True
            For jj = 1 To RsAux.RecordCount
                RsAux.AbsolutePosition = jj

                FlagGrabar = True
                PBCabecera.Value = PBCabecera.Value + 1
                If RsAux!Fecha >= FechaProceso2 And RsAux!hora > CnTabla01.Rs("hora_fin") Then FlagGrabar = False
                If RsAux!Fecha <= FechaProceso1 And RsAux!hora < CnTabla01.Rs("hora_inicio") Then FlagGrabar = False
                If FlagGrabar = True Then
                    CodigoPalet = RsAux!codigo
                    Peso = 0
                    Clave = RsAux!clave_marcaje
                    Accion = Trim(RsAux!Accion)
                    If Mid(Accion, 1, 6) = "FABRIC" Then
                        SQL = "SELECT * FROM palets WHERE codigo_palet='" + Trim(RsAux!codigo) + "' AND fecha_confeccion='" + Format(CDate(RsAux!Fecha), "dd/MM/yyyy") + "' AND hora_confeccion='" & Trim(RsAux!hora) & "'"
                        If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla palets")
                            Exit Sub
                        End If
                        FlagGrabar = False
                        If RsPalets.RecordCount > 0 Then
                            If Trim(RsPalets!tipo_fabricacion) <> "R" Then FlagGrabar = True
                        End If
                        If FlagGrabar = True Then
                            Orden = CLng(Mid(Trim(RsPalets!orden_confeccion), 1, 8))
                            SQL = "SELECT * FROM ORDENES_CONFECCION WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND NUMERO_ORDEN = " + CStr(Orden)
                            If RsOrdenesConfeccion.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla ordenes_confeccion")
                                Exit Sub
                            End If
                            If RsOrdenesConfeccion.RecordCount = 0 Then
                                RsOrdenesConfeccion.Close()
                                FlagGrabar = False
                            Else
                                If Not IsDBNull(RsPalets!peso_palet) Then
                                    If RsPalets!peso_palet <> 0 Then
                                        Peso = RsPalets!peso_palet
                                    Else
                                        Peso = RsOrdenesConfeccion!peso_palet
                                    End If
                                Else
                                    Peso = RsOrdenesConfeccion!peso_palet
                                End If
                                SQL = "SELECT * FROM VARIEDADES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_VARIEDAD = '" + Trim(RsOrdenesConfeccion!codigo_variedad) + "'"
                                If RsVariedad.Open(SQL, ObjetoGlobal.cn) = False Then
                                    MsgBox("Error al abrir la tabla variedades")
                                    Exit Sub
                                End If
                                If RsVariedad.RecordCount = 0 Then
                                    FlagGrabar = False
                                Else
                                    CodigoSubfamilia = ""
                                    CodigoFamilia = ""
                                    If Not IsDBNull(RsVariedad!Codigo_Subfamilia) And Trim("" & RsVariedad!Codigo_Subfamilia) > "0" Then
                                        CodigoFamilia = Trim(RsVariedad!Codigo_familia)
                                        CodigoSubfamilia = Trim(RsVariedad!Codigo_Subfamilia)
                                    Else
                                        CodigoFamilia = Trim(RsVariedad!Codigo_familia)
                                        CodigoSubfamilia = Trim(RsVariedad!Codigo_familia)
                                    End If

                                    Variedad = Trim(RsVariedad!codigo_variedad) + " - " + Trim(RsVariedad!Descripcion)
                                    'TipoVariedad = Mid(RsVariedad!codigo_variedad, 1, 2)
                                    FlagGrabar = False

                                    If Trim(CnTabla01.Rs("familia_variedad")) = Trim(CodigoSubfamilia) Then
                                        FlagGrabar = True
                                    End If
                                End If
                                If Trim(RsOrdenesConfeccion!codigo_confeccion) = "5098" Then 'Fruta con hoja
                                    FlagGrabar = False
                                End If
                                RsVariedad.Close()
                                RsOrdenesConfeccion.Close()
                            End If
                        End If
                        RsPalets.Close()

                    ElseIf Trim(Accion) = "CREA_INDUSTRIA" Then
                        FlagGrabar = False
                        If Mid(RsAux!codigo, 1, 2) = "NG" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "NAR" Then
                            Peso = PesoIndustriaNaranjaGrande
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "BG" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "NAB" Then
                            Peso = PesoIndustriaNaranjaGrande
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "RG" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "ROJ" Then
                            Peso = PesoIndustriaNaranjaGrande
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "NQ" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "NAR" Then
                            Peso = PesoIndustriaNaranjaPequeno
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "BQ" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "NAB" Then
                            Peso = PesoIndustriaNaranjaPequeno
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "RQ" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "ROJ" Then
                            Peso = PesoIndustriaNaranjaPequeno
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "MG" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "CLE" Then
                            Peso = PesoIndustriaClementinaGrande
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "MQ" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "CLE" Then
                            Peso = PesoIndustriaClementinaPequeno
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "SG" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "SAT" Then
                            Peso = PesoIndustriaClementinaGrande
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "SQ" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "SAT" Then
                            Peso = PesoIndustriaClementinaPequeno
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "HG" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "HIB" Then
                            Peso = PesoIndustriaClementinaGrande
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "HQ" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 3) = "HIB" Then
                            Peso = PesoIndustriaClementinaPequeno
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "SB" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 5) = "SANDB" Then
                            Peso = PesoIndustriaSandia
                            FlagGrabar = True
                        ElseIf Mid(RsAux!codigo, 1, 2) = "SN" And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 5) = "SANDN" Then
                            Peso = PesoIndustriaSandia
                            FlagGrabar = True
                        ElseIf (Mid(RsAux!codigo, 1, 2) = "SX" Or Mid(RsAux!codigo, 1, 2) = "SB") And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 5) = "SANDP" Then
                            Peso = PesoIndustriaSandia
                            FlagGrabar = True
                        ElseIf (Mid(RsAux!codigo, 1, 2) = "SY" Or Mid(RsAux!codigo, 1, 2) = "SN") And Mid(Trim(CnTabla01.Rs("familia_variedad")), 1, 5) = "SANNS" Then ' AAA
                            Peso = PesoIndustriaSandia
                            FlagGrabar = True
                        End If
                    ElseIf Trim(Accion) = "CONFECCION PRECALIBRADO" Then
                        SQL = "SELECT p.empresa, p.bultos, p.codigo_variedad, v.codigo_familia, v.codigo_subfamilia FROM palets_precalibre p JOIN variedades v ON v.empresa = p.empresa AND v.codigo_variedad = p.codigo_variedad "
                        SQL = Trim(SQL) + " WHERE p.empresa = '" & RsAux!empresa & "' and p.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND p.referencia = '" & CodigoPalet & "'"
                        If RsPrecalibrado.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla palets_precalibre")
                            Exit Sub
                        End If
                        FlagGrabar = False
                        If RsPrecalibrado.RecordCount = 1 Then
                            CodigoSubfamilia = ""
                            CodigoFamilia = ""

                            If Not IsDBNull(RsPrecalibrado!Codigo_Subfamilia) And Trim("" & CnTabla01.Rs("familia_variedad")) > "0" Then
                                CodigoFamilia = Trim(RsPrecalibrado!Codigo_familia)
                                CodigoSubfamilia = Trim(RsPrecalibrado!Codigo_Subfamilia)
                            Else
                                CodigoFamilia = Trim(RsPrecalibrado!Codigo_familia)
                                CodigoSubfamilia = Trim(RsPrecalibrado!Codigo_familia)
                            End If

                            If Trim(CodigoSubfamilia) = Trim(CnTabla01.Rs("familia_variedad")) Then
                                FlagGrabar = True
                            End If

                            If FlagGrabar Then
                                Peso = 0
                                If Mid(RsPrecalibrado!codigo_variedad, 1, 2) = "02" Then
                                    Peso = Math.Round(RsPrecalibrado!Bultos * PesoCajaNaranjaPrecalibre, 0)
                                ElseIf Mid(RsPrecalibrado!codigo_variedad, 1, 2) = "01" Then
                                    If Trim(RsPrecalibrado!codigo_variedad) = "019" Or Trim(RsPrecalibrado!codigo_variedad) = "0115" Then
                                        Peso = Math.Round(RsPrecalibrado!Bultos * (PesoCajaClementinaPrecalibre + 1), 0)
                                    Else
                                        Peso = Math.Round(RsPrecalibrado!Bultos * PesoCajaClementinaPrecalibre, 0)
                                    End If
                                ElseIf Mid(RsPrecalibrado!codigo_variedad, 1, 2) = "40" Then
                                    Peso = Math.Round(RsPrecalibrado!Bultos * PesoVolcadoPalotOtros, 0)
                                End If
                            End If
                        End If
                        RsPrecalibrado.Close()
                    End If

                    If FlagGrabar = True Then
                        RsGrabar.AddNew()
                        Contador = Contador + 1
                        RsGrabar!Empresa = Trim(CnTabla01.Rs("Empresa"))
                        RsGrabar!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                        RsGrabar!Contador = Contador
                        RsGrabar!tipo_marcaje = Trim(RsAux!tipomarca)
                        RsGrabar!marcaje = Trim(RsAux!codigo)
                        RsGrabar!fecha_marcaje = Format(CDate(RsAux!Fecha), "dd/MM/yyyy")
                        RsGrabar!hora_marcaje = Format(CDate(RsAux!hora), "HH:mm:ss")
                        '            RsGrabar!Fichero = Trim(Rsaux!Fichero_lecturas)
                        RsGrabar!Contador_fichero = RsAux!Orden
                        RsGrabar!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsGrabar!Kilos = Peso
                        RsGrabar!Usuario = CStr(ObjetoGlobal.UsuarioActual)
                        RsGrabar!Estado = "AC"
                        RsGrabar!Fecha_Actualiz = Now
                        RsGrabar!Contador_proceso = Contador * 1000
                        RsGrabar.Update()
                    End If
                End If
            Next
        End If
        RsAux.Close()
        RsGrabar.Close()
        PBCabecera.Visible = False

        CnTabla01.MostrarRegistroActual(True)
        TabGeneral.SelectedIndex = 2

    End Sub

    'Private Sub CmdGrabarTodo_Click(sender As Object, e As EventArgs) Handles CmdGrabarTodo.Click
    '    Dim i As Long

    '    If SituacionFormulario <> Estados.Inactivo Then Exit Sub
    '    If CnTabla01.CuantosRegistros <= 0 Then
    '        MsgBox("Debe seleccionar previamente el proceso de trazabilidad.")
    '        Exit Sub
    '    End If

    '    FlagTodos = True
    '    For i = 1 To CnTabla01.CuantosRegistros
    '        CnTabla01.IrARegistro(i, True)
    '        Me.Refresh()
    '        ProcesarVolcados()
    '        ProcesarConfeccion()
    '    Next
    '    FlagTodos = False

    'End Sub
    Private Function DiferenciaSegundos(Hora1 As String, Hora2 As String) As Long
        Dim Cadena1 As String, Cadena2 As String
        Dim Horas1 As Integer, Horas2 As Integer, Minutos1 As Integer, Minutos2 As Integer, Segundos1 As Integer, Segundos2 As Integer
        Dim Diferencia As Long

        Diferencia = 0
        Cadena1 = Trim(Hora1)
        If Len(Cadena1) = 5 Then Cadena1 = Trim(Cadena1) & ":00"
        If Len(Cadena1) = 7 Then Cadena1 = "0" & Cadena1
        If Len(Cadena1) <> 8 Or Mid(Cadena1, 3, 1) <> ":" Or Mid(Cadena1, 6, 1) <> ":" _
    Or Not IsNumeric(Mid(Cadena1, 1, 2)) Or Not IsNumeric(Mid(Cadena1, 4, 2)) Or Not IsNumeric(Mid(Cadena1, 7, 2)) Then
            MsgBox("Atención hora incorrecta:" + Hora1)
            DiferenciaSegundos = 0
            Exit Function
        End If
        Cadena2 = Trim(Hora2)
        If Len(Cadena2) = 5 Then Cadena2 = Trim(Cadena2) & ":00"
        If Len(Cadena2) = 7 Then Cadena2 = "0" & Cadena2
        If Len(Cadena2) <> 8 Or Mid(Cadena2, 3, 1) <> ":" Or Mid(Cadena2, 6, 1) <> ":" _
    Or Not IsNumeric(Mid(Cadena2, 1, 2)) Or Not IsNumeric(Mid(Cadena2, 4, 2)) Or Not IsNumeric(Mid(Cadena2, 7, 2)) Then
            MsgBox("Atención hora incorrecta:" & Hora2)
            DiferenciaSegundos = 0
            Exit Function
        End If
        Horas1 = CInt(Mid(Cadena1, 1, 2)) : Minutos1 = CInt(Mid(Cadena1, 4, 2)) : Segundos1 = CInt(Mid(Cadena1, 7, 2))
        Horas2 = CInt(Mid(Cadena2, 1, 2)) : Minutos2 = CInt(Mid(Cadena2, 4, 2)) : Segundos2 = CInt(Mid(Cadena2, 7, 2))
        Diferencia = Horas2 * 3600.0# + Minutos2 * 60.0# + Segundos2 - (Horas1 * 3600.0# + Minutos1 * 60.0# + Segundos1)
        DiferenciaSegundos = Diferencia
    End Function
    Private Function LiberarKilosDirectamente(Calidad As Long, ContadorPalet As Long, PunteroPalet As Long, PesoMaximo As Long, PesoUtilizado As Long) As Boolean 'NOSTD
        Dim i As Long, j As Long, k As Long, l As Long, Cadena As String
        Dim PesoPendiente As Long
        Dim ClaveConfeccionado As String, CadenaConfeccionado As String
        Dim ClaveConfeccionado2 As String, CadenaConfeccionado2 As String
        Dim CadenaCalidades As String
        Dim PunteroConfeccionado As Long
        Dim PunteroConfeccionado2 As Long
        Dim KgPosibles As Long

        'Busco un palet confeccionado (anterior a ContadorPalet) 'contadorPalet2' que cumpla:
        '    1) Ha utilizado 'Calidad'
        '    2) Podría utilizar otra 'Calidad2' (es compatible para 'ContadorPalet2' y está libre en ese momento)
        '    3) Si utilizara 'Calidad2' no faltaría 'Calidad2' en ningún palet posterior a 'ContadorPalet2'

        PesoPendiente = PesoMaximo
        PesoUtilizado = 0
        LiberarKilosDirectamente = False
        For i = 0 To ContadorPalet
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            CadenaCalidades = Mid(ClaveConfeccionado, 34)
            If Confeccionado(10 + Calidad, PunteroConfeccionado) > 0 Then
                For j = 0 To 5
                    If j <> Calidad And Mid(CadenaCalidades, j + 1, 1) <> "N" And Confeccionado(30 + j, PunteroConfeccionado) - Confeccionado(40 + j, PunteroConfeccionado) > Confeccionado(10 + j, PunteroConfeccionado) Then
                        KgPosibles = Confeccionado(30 + j, PunteroConfeccionado) - Confeccionado(40 + j, PunteroConfeccionado) - Confeccionado(10 + j, PunteroConfeccionado)
                        For k = i + 1 To ContadorPalet
                            ClaveConfeccionado2 = Trim(oConfeccionadoOrdenado(k))
                            CadenaConfeccionado2 = Trim(oConfeccionado.Item(ClaveConfeccionado2))
                            PunteroConfeccionado2 = CLng(CadenaConfeccionado2)
                            If Confeccionado(30 + j, PunteroConfeccionado2) - Confeccionado(40 + j, PunteroConfeccionado2) - Confeccionado(10 + j, PunteroConfeccionado2) < KgPosibles Then
                                KgPosibles = Confeccionado(30 + j, PunteroConfeccionado2) - Confeccionado(40 + j, PunteroConfeccionado2) - Confeccionado(10 + j, PunteroConfeccionado2)
                                If KgPosibles <= 0 Then
                                    Exit For
                                End If
                            End If
                        Next
                        If KgPosibles > 0 Then
                            If KgPosibles > PesoPendiente Then KgPosibles = PesoPendiente
                            If KgPosibles > Confeccionado(10 + Calidad, PunteroConfeccionado) Then KgPosibles = Confeccionado(10 + Calidad, PunteroConfeccionado)
                            AsignarKilos(i, ContadorPalet, j, KgPosibles)
                            AsignarKilos(i, ContadorPalet, Calidad, -KgPosibles)
                            AsignarKilos(ContadorPalet, ContadorPalet, Calidad, KgPosibles)
                            PesoUtilizado = PesoUtilizado + KgPosibles
                            PesoPendiente = PesoMaximo - PesoUtilizado
                        End If
                    End If
                    If PesoPendiente <= 0 Then Exit For
                Next
                If PesoPendiente <= 0 Then Exit For
            End If
        Next
        If PesoUtilizado > 0 Then LiberarKilosDirectamente = True
    End Function
    Private Sub AsignarKilos(IndicePalet As Long, UltimoPalet As Long, Calidad As Long, Kilos As Long) 'NOSTD
        Dim i As Long, j As Long, k As Long, l As Long, ClaveIndustria As String
        Dim ClaveConfeccionado As String, CadenaConfeccionado As String
        Dim CadenaCalidades As String
        Dim PunteroConfeccionado As Long, PunteroGrupo As Long

        ClaveConfeccionado = Trim(oConfeccionadoOrdenado(IndicePalet))
        CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
        PunteroConfeccionado = CLng(CadenaConfeccionado)
        CadenaCalidades = Mid(ClaveConfeccionado, 34)
        ClaveIndustria = Mid(ClaveConfeccionado, 19, 1)

        Confeccionado(10 + Calidad, PunteroConfeccionado) = Confeccionado(10 + Calidad, PunteroConfeccionado) + Kilos
        PunteroGrupo = DevuelvePunteroGrupo(ClaveConfeccionado)
        Utilizado(Calidad) = Utilizado(Calidad) + Kilos
        TotalUtilizado(Calidad, PunteroGrupo) = TotalUtilizado(Calidad, PunteroGrupo) + Kilos
        For i = IndicePalet + 1 To UltimoPalet
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            Confeccionado(40 + Calidad, PunteroConfeccionado) = Confeccionado(40 + Calidad, PunteroConfeccionado) + Kilos
        Next
    End Sub
    Private Function DevuelvePunteroGrupo(ClaveConfeccionado As String) As Long 'NOSTD
        Dim j As Long

        If Trim(Mid(ClaveConfeccionado, 19, 1)) = "I" Then
            DevuelvePunteroGrupo = oNecesidades.Count
        Else
            DevuelvePunteroGrupo = -1
            For j = 0 To oNecesidades.Count - 1
                If Trim(oNecesidadesOrdenado(j)) = Mid(ClaveConfeccionado, 34) Then
                    DevuelvePunteroGrupo = j
                    Exit For
                End If
            Next
        End If
        If DevuelvePunteroGrupo = -1 Then
            MsgBox("Atención: No existe ficha de calidades para el palet : '" + Trim(ClaveConfeccionado) & "'")
            Exit Function
        End If
    End Function
    Private Sub InformeFinal()
        Dim i As Long, j As Long, k As Long, l As Long
        Dim ClaveVolcado As String, CadenaVolcado As String, ClaveConfeccionado As String, CadenaConfeccionado As String
        Dim ClaveCalidades As String
        Dim PunteroVolcado As Long, PunteroConfeccionado As Long
        Dim HoraConfeccion As String, HoraVolcado As String
        Dim ClaveIndustria As String, CadenaMensaje As String

        For k = 0 To oVolcado.Count - 1
            ClaveVolcado = oVolcadoOrdenado(k)
            CadenaVolcado = Trim(oVolcado.Item(ClaveVolcado))
            PunteroVolcado = CInt(CadenaVolcado)
            HoraVolcado = Mid(ClaveVolcado, 7, 2) & ":" & Mid(ClaveVolcado, 9, 2) & ":" & Mid(ClaveVolcado, 11, 2)

            CadenaMensaje = Trim(HoraVolcado)
            For l = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(Volcado(l, PunteroVolcado), "###0")
            Next
            For l = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(Volcado(10 + l, PunteroVolcado), "###0")
            Next
            LW.Items.Add(CadenaMensaje)
        Next
        For i = 0 To oConfeccionado.Count - 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            HoraConfeccion = Mid(ClaveConfeccionado, 7, 2) & ":" & Mid(ClaveConfeccionado, 9, 2) & ":" & Mid(ClaveConfeccionado, 11, 2)
            ClaveCalidades = Mid(ClaveConfeccionado, 34)
            ClaveIndustria = Mid(ClaveConfeccionado, 19, 1)

            CadenaMensaje = Trim(HoraConfeccion) + vbTab + ClaveCalidades
            For l = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(10 + l, PunteroConfeccionado), "###0")
            Next
            LW.Items.Add(CadenaMensaje)
        Next
    End Sub
    Private Sub AsignarVolcado(ClaveConfeccion As String, ClaveVolcado As String, Calidad As Long, Kilos As Long, ByVal CodigoPalet As String, ByVal NumeroAlbaran As Long, RefPrecalibre As String, Referencia As String)
        Dim ClaveFinal As String, DatoS(5) As Long, CadenaFinal As String, SQL
        Dim j As Long, Rs As New cnRecordset.CnRecordset, FlagExiste As Boolean, MaximoContador As Long

        ClaveFinal = ClaveConfeccion + ClaveVolcado  '39 + 18
        If oFinal.ContainsKey(ClaveFinal) Then
            CadenaFinal = oFinal.Item(ClaveFinal)
            For j = 0 To 5 : DatoS(j) = CLng(Mid(CadenaFinal, 10 * j + 1, 10)) : Next
            DatoS(Calidad) = DatoS(Calidad) + Kilos
        Else
            For j = 0 To 5 : DatoS(j) = 0 : Next
            CadenaFinal = ""
            oFinal.Add(ClaveFinal, CadenaFinal)
            DatoS(Calidad) = Kilos
        End If
        CadenaFinal = ""
        For j = 0 To 5
            CadenaFinal = CadenaFinal + Format(DatoS(j), "0000000000")
        Next
        oFinal.Item(ClaveFinal) = CadenaFinal
        If Trim(CodigoPalet) > "" Then
            FlagExiste = False
            MaximoContador = 0
            If Mid(ClaveFinal, 19, 1) = "C" Then
                SQL = "SELECT * FROM PALETS_TRAZAB WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_PALET = '" + Trim(CodigoPalet) + "' ORDER BY 1,2,3"
                If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla de palets_trazab")
                    Exit Sub
                End If
                Do While Rs.EOF = False
                    MaximoContador = Rs!Contador
                    If Rs!numero_albaran = NumeroAlbaran Then
                        FlagExiste = True
                        Exit Do
                    End If
                    Rs.MoveNext()
                Loop
                If FlagExiste = False Then
                    Rs.AddNew()
                    Rs!Empresa = Trim(ObjetoGlobal.EmpresaActual)
                    Rs!Codigo_palet = CodigoPalet
                    Rs!Contador = MaximoContador + 1
                    Rs!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    Rs!serie_albaran = "N" + Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                    Rs!numero_albaran = NumeroAlbaran
                    Rs!Referencia = Format(Now, "dd/MM/yyyy dd:mm") + "," + Trim(ObjetoGlobal.NombreUsuarioActual)
                    Rs.Update()
                End If
                Rs.Close()
            ElseIf Mid(ClaveFinal, 19, 1) = "P" Then
                SQL = "SELECT * FROM PALETS_PRECALIBRE_T WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_PALET = '" + Trim(CodigoPalet) + "' ORDER BY 1,2,3"
                If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla de palets_precalibre_t")
                    Exit Sub
                End If
                MaximoContador = 10000 + CLng(Mid(Trim(ObjetoGlobal.EjercicioActual), 1, 1)) * 1000
                Do While Rs.EOF = False
                    MaximoContador = Rs!Contador
                    If Rs!numero_albaran = NumeroAlbaran Then
                        FlagExiste = True
                        Exit Do
                    End If
                    Rs.MoveNext()
                Loop
                If FlagExiste = False Then
                    Rs.AddNew()
                    Rs!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    Rs!Codigo_palet = CodigoPalet
                    Rs!Contador = MaximoContador + 1
                    Rs!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    Rs!serie_albaran = "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                    Rs!numero_albaran = NumeroAlbaran
                    Rs!Referencia = Referencia
                    Rs!Ref_precalibre = Trim(RefPrecalibre)
                    Rs.Update()
                Else
                    'MsgBox("parar"
                End If
                Rs.Close()
            End If
        End If
    End Sub

    Private Function ComprobarVolcados(proceso) As Boolean
        Dim Rs As New cnRecordset.CnRecordset, Rs1 As New cnRecordset.CnRecordset
        Dim SQL As String, j As Integer


        ComprobarVolcados = True

        SQL = "SELECT * FROM trazab_volcado WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_proceso = '" & Trim(proceso) & "' AND numero_albaran = 0 ORDER BY contador"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla trazab_volcado")
            Exit Function
        End If
        Do While Not Rs.EOF
            SQL = "SELECT * FROM trazab_confeccion WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_proceso = '" & Trim(proceso) & "' AND marcaje = '" & Rs!marcaje & "' ORDER BY contador"
            If Rs1.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla trazab_confeccion")
                Exit Function
            End If
            Do While Not Rs1.EOF
                If Rs!hora_marcaje <= Rs1!hora_marcaje Then
                    ' Error
                    Rs.Close()
                    Rs1.Close()
                    ComprobarVolcados = False
                    Exit Function
                End If
                Rs1.MoveNext()
            Loop
            Rs1.Close()
            Rs.MoveNext()
        Loop
        Rs.Close()
    End Function

    Private Sub LeerPesosTeoricos()
        Dim Rs As New cnRecordset.CnRecordset, SQL As String
        SQL = "SELECT * FROM PESOS_TEORICOS WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "'"
        If Rs.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla de pesos_teoricos")
            Exit Sub
        End If
        If Rs.RecordCount = 0 Then
            PesoIndustriaClementinaPequeno = 225
            PesoIndustriaNaranjaPequeno = 180
            PesoIndustriaClementinaGrande = 340
            PesoIndustriaNaranjaGrande = 295
            PesoVolcadoClementinaPalet = 600
            PesoVolcadoNaranjaPalet = 600
            PesoVolcadoClementinaPalot = 295
            PesoVolcadoNaranjaPalot = 295
            PesoIndustriaSandia = 300
            PesoCajaNaranja = 19
            PesoCajaClementina = 20
            PesoCajaNaranjaPrecalibre = 13
            PesoCajaClementinaPrecalibre = 16
            PesoVolcadoPalotOtros = 295
        Else
            PesoIndustriaClementinaPequeno = Rs!industria_mand_peq
            PesoIndustriaNaranjaPequeno = Rs!industria_nar_peq
            PesoIndustriaClementinaGrande = Rs!industria_mand_gr
            PesoIndustriaNaranjaGrande = Rs!industria_nar_gr
            PesoIndustriaSandia = Rs!industria_sandia
            PesoVolcadoClementinaPalet = Rs!volcado_palet_mand
            PesoVolcadoNaranjaPalet = Rs!volcado_palet_nar
            PesoVolcadoClementinaPalot = Rs!volcado_palot_mand
            PesoVolcadoNaranjaPalot = Rs!volcado_palot_nar
            PesoCajaNaranja = Rs!caja_naranja
            PesoCajaClementina = Rs!caja_mandarina
            PesoCajaNaranjaPrecalibre = Rs!caja_naranja_pre
            PesoCajaClementinaPrecalibre = Rs!caja_mandarina_pre
            PesoVolcadoPalotOtros = Rs!volcado_palot_otros
        End If
        Rs.Close()
    End Sub
    Private Sub AsignarCeros(Referencia As String)
        Dim SQL As String, ContadorAuxiliar As Long
        Dim RsPre As New cnRecordset.CnRecordset, RsPro1 As New cnRecordset.CnRecordset, RsPro2 As New cnRecordset.CnRecordset, RsPro3 As New cnRecordset.CnRecordset

        SQL = "SELECT * FROM palets_trazab WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
        SQL = Trim(SQL) + " numero_albaran = 0 and referencia = '" & Referencia & "'"
        If RsPro1.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla de palets_trazab")
            Exit Sub
        End If

        SQL = "SELECT * FROM palets_trazab WHERE 1=0"
        If RsPro2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla de palets_trazab (grabación)")
            Exit Sub
        End If

        Do While Not RsPro1.EOF
            ' Buscamos el contador
            SQL = "SELECT max(contador)  as cont FROM palets_trazab WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
            SQL = Trim(SQL) + " Codigo_palet = '" & RsPro1!Codigo_palet & "' and referencia = '" & Referencia & "'"
            If RsPro3.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla de palets_trazab (contador)")
                Exit Sub
            End If
            ContadorAuxiliar = 0
            If RsPro3.RecordCount > 0 Then
                ContadorAuxiliar = RsPro3!cont
            End If

            RsPro3.Close()

            SQL = "SELECT t.empresa, t.serie_albaran, t.numero_albaran, pc.referencia AS ref "
            SQL = Trim(SQL) + " FROM palets_precalibre_t t INNER JOIN palets_precalibre pc ON "
            SQL = Trim(SQL) + " pc.empresa = t.empresa AND pc.ejercicio = t.ejercicio AND pc.codigo_palet = t.codigo_palet "
            SQL = Trim(SQL) + " WHERE t.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND t.ejercicio ='" & ObjetoGlobal.EjercicioActual & "' "
            SQL = Trim(SQL) + " AND pc.referencia = '" & RsPro1!Ref_precalibre & "'"
            If RsPre.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla de palets_precalibre_t")
                Exit Sub
            End If
            If RsPre.RecordCount > 0 Then
                Do While Not RsPre.EOF
                    SQL = "SELECT * FROM palets_trazab WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
                    SQL = Trim(SQL) + " Codigo_palet = '" & RsPro1!Codigo_palet & "' and referencia = '" & Referencia & "' and numero_albaran=" & RsPre!numero_albaran
                    If RsPro3.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla de palets_trazab")
                        Exit Sub
                    End If
                    If RsPro3.RecordCount = 0 Then
                        ContadorAuxiliar = ContadorAuxiliar + 1
                        RsPro2.AddNew()
                        RsPro2!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsPro2!Codigo_palet = RsPro1!Codigo_palet
                        RsPro2!Contador = ContadorAuxiliar
                        RsPro2!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        'RsPro2!serie_albaran = Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                        RsPro2!serie_albaran = "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                        RsPro2!numero_albaran = RsPre!numero_albaran
                        RsPro2!Referencia = Referencia
                        RsPro2!Ref_precalibre = RsPro1!Ref_precalibre
                        RsPro2.Update()
                    End If
                    RsPro3.Close()
                    RsPre.MoveNext()
                Loop
                RsPro1.Delete()
            End If
            RsPre.Close()
            If Not RsPro1.EOF Then RsPro1.MoveNext()
        Loop
        RsPro2.Close()
        RsPro1.Close()

        ' Primero los palets de precalibrado

        SQL = "SELECT * FROM palets_precalibre_t WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
        SQL = Trim(SQL) + " numero_albaran = 0 and referencia = '" & Referencia & "'"
        If RsPro1.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla de palets_precalibre_t")
            Exit Sub
        End If

        SQL = "SELECT * FROM palets_precalibre_t WHERE 1=0"
        If RsPro2.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla de palets_precalibre_t (grabación)")
            Exit Sub
        End If

        Do While Not RsPro1.EOF

            ' Buscamos el contador
            SQL = "SELECT max(contador)  as cont FROM palets_precalibre_t WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
            SQL = Trim(SQL) + " Codigo_palet = '" & RsPro1!Codigo_palet & "' and referencia = '" & Referencia & "'"
            If RsPro3.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla de palets_precalibre_t (contador)")
                Exit Sub
            End If
            ContadorAuxiliar = 0
            If RsPro3.RecordCount > 0 Then
                ContadorAuxiliar = RsPro3!cont
            End If
            RsPro3.Close()

            SQL = "SELECT t.empresa, t.serie_albaran, t.numero_albaran, pc.referencia AS ref "
            SQL = Trim(SQL) + " FROM palets_precalibre_t t INNER JOIN palets_precalibre pc ON "
            SQL = Trim(SQL) + " pc.empresa = t.empresa AND pc.ejercicio = t.ejercicio AND pc.codigo_palet = t.codigo_palet "
            SQL = Trim(SQL) + " WHERE t.EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND t.ejercicio ='" & ObjetoGlobal.EjercicioActual & "' "
            SQL = Trim(SQL) + " AND pc.referencia = '" & RsPro1!Ref_precalibre & "'"
            If RsPre.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla de palets_precalibre_t")
                Exit Sub
            End If
            If RsPre.RecordCount > 0 Then
                Do While Not RsPre.EOF
                    SQL = "SELECT * FROM palets_precalibre_t WHERE EMPRESA = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio ='" & ObjetoGlobal.EjercicioActual & "' AND "
                    SQL = Trim(SQL) + " Codigo_palet = '" & RsPro1!Codigo_palet & "' and referencia = '" & Referencia & "' and numero_albaran=" & RsPre!numero_albaran
                    If RsPro3.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla de palets_precalibre_t")
                        Exit Sub
                    End If
                    If RsPro3.RecordCount = 0 Then
                        ContadorAuxiliar = ContadorAuxiliar + 1
                        RsPro2.AddNew()
                        RsPro2!empresa = Trim(ObjetoGlobal.EmpresaActual)
                        RsPro2!Codigo_palet = RsPro1!Codigo_palet
                        RsPro2!Contador = ContadorAuxiliar
                        RsPro2!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                        RsPro2!serie_albaran = "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                        RsPro2!numero_albaran = RsPre!numero_albaran
                        RsPro2!Referencia = Referencia
                        RsPro2!Ref_precalibre = RsPro1!Ref_precalibre
                        RsPro2.Update()
                    End If
                    RsPro3.Close()
                    RsPre.MoveNext()
                Loop
                RsPro1.Delete()
            End If
            RsPre.Close()
            If Not RsPro1.EOF Then RsPro1.MoveNext()
        Loop
        RsPro2.Close()
        RsPro1.Close()
        ' Fin de sustitución de albaranes vacios
    End Sub

    Private Sub CmdCierre_Click(sender As Object, e As EventArgs) Handles CmdCierre.Click

        Dim Fichero2 As New cnFichero.cnFichero
        Dim i As Long, j As Long, k As Long, l As Long, rr As Long, Cadena As String
        Dim SqlPalets As String, SqlOrdenes As String, SqlSalida As String
        Dim RsAux As New cnRecordset.CnRecordset, RsEnvases As New cnRecordset.CnRecordset, RsCalidades As New cnRecordset.CnRecordset, RsEntradas As New cnRecordset.CnRecordset, RsSalida As New cnRecordset.CnRecordset
        Dim RsPalets As New cnRecordset.CnRecordset, RsOrdenesConfeccion As New cnRecordset.CnRecordset, RsCalidadesCalibre As New cnRecordset.CnRecordset
        Dim Pesos(10) As Long, PesoPalet As Long, PesoPendiente As Long
        Dim PesoUtilizado As Long, PesoUtilizadoConMerma As Long
        Dim MermaPendiente As Long, MermaUtilizada As Long
        Dim TotalMuestreo As Long, TotalAjustado As Long, CalidadPrincipal As Integer, PesoCalidadPrincipal As Long, PesosAjustados(10) As Long
        Dim FechaProceso1 As Date, FechaProceso2 As Date
        Dim ClaveVolcado As String, CadenaVolcado As String, ClaveConfeccionado As String, CadenaConfeccionado As String
        Dim ClaveConfeccionado2 As String, CadenaConfeccionado2 As String
        Dim ClaveCalidades As String, CadenaCalidades As String, ClaveNecesidades As String, CadenaNecesidades As String
        Dim PunteroVolcado As Long, PunteroConfeccionado As Long, PunteroGrupo As Long, PunteroConfeccionado2 As Long
        Dim CalibrePalet As String, VariedadPalet As String
        Dim HoraConfeccion As String, HoraVolcado As String
        Dim CodigoError As Long, ClaveIndustria As String, Calidades(6) As String
        Dim RegistroInicialVolcado As Long, RegistroInicialConfeccion As Long
        Dim AsignadoPalet(6) As Long, PesoNecesidad As Long
        Dim Clave As String, Dato As String, Numeros(10) As Double
        Dim ClaveAAsignar As String, DatoAAsignar As String
        Dim Grupo As Long, MaximoValor As Double, ValorMaximo() As Double
        Dim GrupoAAsignar As Long, MaximoValorAAsignar As Double
        Dim ClaveNueva As String, DatoNuevo As String
        Dim FlagEsPosible As Boolean
        Dim CalidadSeleccionada As Long, MaximoSeleccionado As Double
        Dim FlagPosibleMover As Boolean, FlagSalir As Boolean
        Dim Tot As Long, Tot2 As Long, Ratio As Double
        Dim RatioMermaTotal As Double, MaximaMerma As Double, CalidadMaximaMerma As Long
        Dim TotalPorAsignar As Long, CadenaMensaje As String
        Dim GrupoActivo() As Boolean, GruposActivos As Integer
        Dim ConfeccionadoAcumulado As Long
        Dim RatioReparto(5) As Double, KgReparto(5) As Long, KgReparto2(5) As Long, FlagFalta As Boolean
        Dim HoraX As String, KilosX As String, PunteroX As Long
        Dim MermaFinal(10) As Long, MermaAAsociar As Long, HoraParaMerma(10) As String
        Dim ClaveFinal As String, DatoS(10) As Long, CadenaFinal As String
        Dim NombreFichero As String, Fila As Long
        Dim Clientex As String, NombreClientex As String, Cargax As String, Albaranx As String, Paletx As String
        Dim RsFin As New cnRecordset.CnRecordset, KilosReservados As Long, PrimerVolcadoReservado As Long
        Dim KilosAnteriores As Long, PrimerVolcadoAnterior As Long
        Dim RsAyer As New cnRecordset.CnRecordset, RsVolcadosAyer As New cnRecordset.CnRecordset, RsBorrar As New cnRecordset.CnRecordset
        Dim RetardoLinea As Integer
        Dim SQL As String
        Dim RsV As New cnRecordset.CnRecordset, RsC As New cnRecordset.CnRecordset
        Dim kk As Integer, HastaVolcado As Integer, RutaExcel As String

        Dim ReferenciaProceso As String, Codigo_palet As String, TipoDeEnvase As String
        Dim Rs As New cnRecordset.CnRecordset, RsEnt_tazab As New cnRecordset.CnRecordset


        ReferenciaProceso = Format(Now, "dd/MM/yyyy") + "," + Format(Now, "HH:mm") + "," + Trim(ObjetoGlobal.UsuarioActual)
        LW.Items.Clear()
        Pic0.Visible = False : Pic1.Visible = False : Pic2.Visible = False : Pic3.Visible = False
        Pic4.Visible = False : Pic5.Visible = False : Pic6.Visible = False : Pic7.Visible = False
        PB0.Visible = False : PB1.Visible = False : PB2.Visible = False : PB22.Visible = False : PB23.Visible = False
        PB3.Visible = False : PB4.Visible = False : PB5.Visible = False : PB6.Visible = False : PB7.Visible = False
        Lbl3.Text = ""

        'Existencia volcados y confección
        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        If CnTabla01.CuantosRegistros <= 0 Then
            MsgBox("Debe seleccionar previamente el proceso de trazabilidad.")
            Exit Sub
        End If
        If FlagTodos = True Then Fichero.EscribeLinea("Proceso:" + CnTabla01.Rs("codigo_proceso"))
        'If CnTabla02.CuantosRegistros = 0 Then
        '    If FlagTodos = False Then
        '        MsgBox("No existen volcados para procesar.")
        '    Else
        '        Fichero.EscribeLinea("No existen volcados para procesar.")
        '    End If
        '    '    Exit Sub
        'End If
        If CnTabla03.CuantosRegistros = 0 Then
            If FlagTodos = False Then
                MsgBox("No existen palets confeccionados para procesar.")
            Else
                Fichero.EscribeLinea("No existen palets confeccionados para procesar.")
            End If
            Exit Sub
        End If
        RetardoLinea = 0
        Cadena = Trim(CnTabla01.Rs("familia_variedad"))
        If Mid(Cadena, 1, 3) <> "COL" And Mid(Cadena, 1, 3) <> "ALC" Then RetardoLinea = 300

        If Not ComprobarVolcados(Trim(CnTabla01.Rs("codigo_proceso"))) Then
            MsgBox("Existen palets volcados antes de su confección.")
            Exit Sub
        End If

        'Grabar tabla temporal que sustituye a la ordenación del recordset de las Cntabla02 y Cntabla03
        Cadena = "SELECT * FROM temp_trazab_volcado WHERE empresa = '" + Trim(CnTabla01.Rs("empresa")) + "' AND codigo_proceso = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "'"
        BorrarDatos(Cadena)
        Cadena = "SELECT * FROM temp_trazab_confeccion WHERE empresa = '" + Trim(CnTabla01.Rs("empresa")) + "' AND codigo_proceso = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "'"
        BorrarDatos(Cadena)

        'Comprobación inicial
        If CnTabla01.Rs("stock_inicial") > 0 Then
            If IsDBNull(CnTabla01.Rs("proceso_anterior")) Then
                If FlagTodos = False Then
                    MsgBox("No es posible realizar el proceso: se ha indicado stock inicial y no proceso anterior")
                Else
                    Fichero.EscribeLinea("No es posible realizar el proceso: se ha indicado stock inicial y no proceso anterior")
                End If
                Exit Sub
            End If
            If Trim(CnTabla01.Rs("proceso_anterior")) = "" Then
                If FlagTodos = False Then
                    MsgBox("No es posible realizar el proceso: se ha indicado stock inicial y no proceso anterior")
                Else
                    Fichero.EscribeLinea("No es posible realizar el proceso: se ha indicado stock inicial y no proceso anterior")
                End If
                Exit Sub
            End If

            SQL = "SELECT * FROM TRAZAB_PERIODOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_PROCESO = '" + Trim(CnTabla01.Rs("proceso_anterior")) + "'"
            If RsAyer.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla trazab_periodos (ayer)")
                Exit Sub
            End If
            If RsAyer.RecordCount = 0 Then
                RsAyer.Close()
                If FlagTodos = False Then
                    MsgBox("No es posible realizar el proceso: se ha indicado stock inicial y el proceso anterior indicado no existe.")
                Else
                    Fichero.EscribeLinea("No es posible realizar el proceso: se ha indicado stock inicial y el proceso anterior indicado no existe.")
                End If
                Exit Sub
            ElseIf Trim(RsAyer!familia_variedad) <> Trim(CnTabla01.Rs("familia_variedad")) Then
                If FlagTodos = False Then
                    MsgBox("No es posible realizar el proceso: se ha indicado un proceso anterior de diferente familia.")
                Else
                    Fichero.EscribeLinea("No es posible realizar el proceso: se ha indicado un proceso anterior de diferente familia.")
                End If
                Exit Sub
            End If

            SQL = "SELECT * FROM TRAZAB_VOLCADO WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_PROCESO = '" + Trim(Trim(CnTabla01.Rs("proceso_anterior"))) + "' ORDER BY 1,2,3"
            If RsVolcadosAyer.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla trazab_periodos (ayer)")
                Exit Sub
            End If

            If RsVolcadosAyer.RecordCount = 0 Then
                RsAyer.Close()
                If FlagTodos = False Then
                    MsgBox("No es posible realizar el proceso: se ha indicado stock inicial y el proceso anterior indicado no tiene palets volcados.")
                Else
                    Fichero.EscribeLinea("No es posible realizar el proceso: se ha indicado stock inicial y el proceso anterior indicado no tiene palets volcados.")
                End If
                Exit Sub
            End If
        End If

        'Grabación inicial para el uso de las tablas temporales, en lugar de los recorsets de Cntabla02 y Cntabla03
        SQL = "SELECT * FROM temp_trazab_volcado WHERE 1 = 0"
        If RsV.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_trazab_volcado")
            Exit Sub
        End If
        For i = 1 To CnTabla02.CuantosRegistros
            CnTabla02.IrARegistro(i, True)
            RsV.AddNew()
            RsV!empresa = Trim(CnTabla02.Rs("empresa"))
            RsV!codigo_proceso = CnTabla02.Rs("codigo_proceso")
            RsV!contador = CnTabla02.Rs("contador")
            RsV!tipo_marcaje = Trim(CnTabla02.Rs("tipo_marcaje"))
            RsV!marcaje = Trim(CnTabla02.Rs("marcaje"))
            RsV!fecha_marcaje = CDate(CnTabla02.Rs("fecha_marcaje"))
            RsV!hora_marcaje = Trim(CnTabla02.Rs("hora_marcaje"))
            If Not IsDBNull(CnTabla02.Rs("fichero")) Then RsV!fichero = Trim(CnTabla02.Rs("fichero"))
            If Not IsDBNull(CnTabla02.Rs("contador_fichero")) Then RsV!contador_fichero = CnTabla02.Rs("contador_fichero")
            If Not IsDBNull(CnTabla02.Rs("ejercicio_albaran")) Then RsV!ejercicio_albaran = Trim(CnTabla02.Rs("ejercicio_albaran"))
            If Not IsDBNull(CnTabla02.Rs("serie_albaran")) Then RsV!serie_albaran = Trim(CnTabla02.Rs("serie_albaran"))
            If Not IsDBNull(CnTabla02.Rs("numero_albaran")) Then RsV!numero_albaran = Trim(CnTabla02.Rs("numero_albaran"))
            RsV!kilos = CnTabla02.Rs("kilos")
            RsV!kilos_e = CnTabla02.Rs("kilos_e")
            RsV!kilos_1 = CnTabla02.Rs("kilos_1")
            RsV!kilos_2 = CnTabla02.Rs("kilos_2")
            RsV!kilos_3 = CnTabla02.Rs("kilos_3")
            RsV!kilos_4 = CnTabla02.Rs("kilos_4")
            RsV!kilos_5 = CnTabla02.Rs("kilos_5")
            RsV!kilos_6 = CnTabla02.Rs("kilos_6")
            RsV!kilos_otros = CnTabla02.Rs("kilos_otros")
            If Not IsDBNull(CnTabla02.Rs("usuario")) Then RsV!usuario = Trim(CnTabla02.Rs("usuario"))
            If Not IsDBNull(CnTabla02.Rs("estado")) Then RsV!estado = Trim(CnTabla02.Rs("estado"))
            If Not IsDBNull(CnTabla02.Rs("fecha_actualiz")) Then RsV!fecha_actualiz = CDate(CnTabla02.Rs("fecha_actualiz"))
            RsV!contador_proceso = CnTabla02.Rs("contador_proceso")
            If Not IsDBNull(CnTabla02.Rs("descripcion_variedad")) Then RsV!descripcion_variedad = Trim(CnTabla02.Rs("descripcion_variedad"))
            If Not IsDBNull(CnTabla02.Rs("codigo_campo")) Then RsV!codigo_campo = CnTabla02.Rs("codigo_campo")
            RsV.Update()
        Next
        RsV.Close()

        SQL = "SELECT * FROM temp_trazab_volcado where empresa = '" + Trim(CnTabla01.Rs("empresa")) + "' AND codigo_proceso = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "' ORDER BY fecha_marcaje, hora_marcaje"
        If RsV.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_trazab_volcado")
            Exit Sub
        End If

        SQL = "SELECT * FROM temp_trazab_confeccion WHERE 1 = 0"
        If RsC.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_trazab_confeccion")
            Exit Sub
        End If

        For i = 1 To CnTabla03.CuantosRegistros
            CnTabla03.IrARegistro(i, True)
            RsC.AddNew()
            RsC!empresa = Trim(CnTabla03.Rs("empresa"))
            RsC!codigo_proceso = CnTabla03.Rs("codigo_proceso")
            RsC!contador = CnTabla03.Rs("contador")
            RsC!tipo_marcaje = Trim(CnTabla03.Rs("tipo_marcaje"))
            RsC!marcaje = Trim(CnTabla03.Rs("marcaje"))
            RsC!fecha_marcaje = CDate(CnTabla03.Rs("fecha_marcaje"))
            RsC!hora_marcaje = Trim(CnTabla03.Rs("hora_marcaje"))
            If Not IsDBNull(CnTabla03.Rs("fichero")) Then RsC!fichero = Trim(CnTabla03.Rs("fichero"))
            If Not IsDBNull(CnTabla03.Rs("contador_fichero")) Then RsC!contador_fichero = CnTabla03.Rs("contador_fichero")
            If Not IsDBNull(CnTabla03.Rs("ejercicio")) Then RsC!ejercicio = Trim(CnTabla03.Rs("ejercicio"))
            If Not IsDBNull(CnTabla03.Rs("codigo_palet")) Then RsC!codigo_palet = Trim(CnTabla03.Rs("codigo_palet"))
            RsC!kilos = CnTabla03.Rs("kilos")
            If Not IsDBNull(CnTabla03.Rs("usuario")) Then RsC!usuario = Trim(CnTabla03.Rs("usuario"))
            If Not IsDBNull(CnTabla03.Rs("estado")) Then RsC!estado = Trim(CnTabla03.Rs("estado"))
            If Not IsDBNull(CnTabla03.Rs("fecha_actualiz")) Then RsC!fecha_actualiz = CDate(CnTabla03.Rs("fecha_actualiz"))
            RsC!contador_proceso = CnTabla03.Rs("contador_proceso")
            If Not IsDBNull(CnTabla03.Rs("proceso_anterior")) Then RsC!proceso_anterior = Trim(CnTabla03.Rs("proceso_anterior"))
            RsC.Update()
        Next

        RsC.Close()
        SQL = "SELECT * FROM temp_trazab_confeccion WHERE empresa = '" + Trim(CnTabla01.Rs("empresa")) + "' AND codigo_proceso = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "' ORDER BY fecha_marcaje, hora_marcaje"
        If RsC.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla temp_trazab_confeccion")
            Exit Sub
        End If

        If RsV.RecordCount > 0 Then RegistroInicialVolcado = RsV.AbsolutePosition
        RegistroInicialConfeccion = RsC.AbsolutePosition

        'Inicialización
        FechaProceso1 = CDate(CnTabla01.Rs("fecha_inicio"))
        FechaProceso2 = CDate(CnTabla01.Rs("fecha_fin"))
        ReDim Volcado(31, 0)
        ReDim Volcado2(10, 0)
        ReDim Confeccionado(60, 0)
        ReDim Confeccionado2(10, 0)
        For i = 0 To 50 : VolcadoTotal(i) = 0 : Next
        ConfeccionadoTotal = 0
        CuantosVolcados = -1
        PB0.Visible = True
        PB0.Value = 0
        Pic0.Visible = True

        SQL = "SELECT * FROM calidades_calibre WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) + "' ORDER BY 1,2,3"
        If RsCalidadesCalibre.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("Error al abrir la tabla calidades calibre")
            Exit Sub
        End If

        PB0.Maximum = RsV.RecordCount + RsC.RecordCount + RsCalidadesCalibre.RecordCount

        oVolcado.Clear()
        ReDim oVolcadoOrdenado(0)
        oConfeccionado.Clear()
        ReDim oConfeccionadoOrdenado(0)
        oCalidades.Clear()
        oNecesidades.Clear()
        ReDim oNecesidadesOrdenado(0)
        Me.Refresh()

        'Lazo Volcados Iniciales (día anterior)
        If CnTabla01.Rs("stock_inicial") > 0 Then
            KilosAnteriores = 0
            PrimerVolcadoAnterior = 999999
            If RsVolcadosAyer.RecordCount > 0 Then
                RsVolcadosAyer.MoveLast()

                Do While RsVolcadosAyer.BOF = False And KilosAnteriores < CnTabla01.Rs("stock_inicial")
                    KilosAnteriores = KilosAnteriores + RsVolcadosAyer!Kilos
                    PrimerVolcadoAnterior = RsVolcadosAyer.AbsolutePosition
                    RsVolcadosAyer.MovePrevious()
                Loop
            End If

            If KilosAnteriores < CnTabla01.Rs("stock_inicial") Then
                If FlagTodos = False Then
                    MsgBox("No existen suficientes kilos en el proceso anterior para cumplimentar el stock inicial." + vbCrLf + "Se cancela el proceso.")
                Else
                    Fichero.EscribeLinea("No existen suficientes kilos en el proceso anterior para cumplimentar el stock inicial." + vbCrLf + "Se cancela el proceso.")
                End If
                Pic0.Visible = False
                PB0.Visible = False
                Exit Sub
            End If

            RsVolcadosAyer.AbsolutePosition = PrimerVolcadoAnterior
            Do While RsVolcadosAyer.EOF = False
                Pesos(0) = RsVolcadosAyer!Kilos
                Pesos(1) = RsVolcadosAyer!Kilos_e
                For i = 1 To 5
                    Pesos(i + 1) = CLng(RsVolcadosAyer("kilos_" + CStr(i)))
                Next
                TotalMuestreo = 0
                TotalAjustado = 0
                CalidadPrincipal = 0
                PesoCalidadPrincipal = -1
                For i = 1 To 6
                    PesosAjustados(i) = 0
                    If Pesos(i) > PesoCalidadPrincipal Then
                        CalidadPrincipal = i
                        PesoCalidadPrincipal = Pesos(i)
                    End If
                    TotalMuestreo = TotalMuestreo + Pesos(i)
                Next
                If TotalMuestreo = 0 And Pesos(0) > 0 Then
                    CalidadPrincipal = 0
                    PesoCalidadPrincipal = -1
                    For i = 1 To 6
                        Pesos(i) = VolcadoTotal(i - 1)
                        If Pesos(i) > PesoCalidadPrincipal Then
                            CalidadPrincipal = i
                            PesoCalidadPrincipal = Pesos(i)
                        End If
                        TotalMuestreo = TotalMuestreo + Pesos(i)
                    Next
                End If
                If TotalMuestreo = 0 And Pesos(0) > 0 Then
                    CalidadPrincipal = 0
                    PesoCalidadPrincipal = -1
                    For i = 1 To 6
                        If i <= 5 Then Pesos(i) = 20 Else Pesos(i) = 0
                        If Pesos(i) > PesoCalidadPrincipal Then
                            CalidadPrincipal = i
                            PesoCalidadPrincipal = Pesos(i)
                        End If
                        TotalMuestreo = TotalMuestreo + Pesos(i)
                    Next
                End If
                If TotalMuestreo > 0 Then
                    CuantosVolcados = CuantosVolcados + 1
                    If CuantosVolcados > 0 Then
                        ReDim Preserve Volcado(31, CuantosVolcados)
                        ReDim Preserve Volcado2(10, CuantosVolcados)
                    End If
                    ClaveVolcado = Format(CDate(RsVolcadosAyer!fecha_marcaje), "yyMMdd") + Format(CDate(RsVolcadosAyer!hora_marcaje), "HHmmss") + Format(RsVolcadosAyer!Contador, "000000")
                    CadenaVolcado = Format(CuantosVolcados, "00000")
                    oVolcado.Add(ClaveVolcado, CadenaVolcado)
                    For i = 1 To 6
                        PesosAjustados(i) = Math.Round(Pesos(i) * Pesos(0) / TotalMuestreo, 0)
                        TotalAjustado = TotalAjustado + PesosAjustados(i)
                    Next
                    If TotalAjustado <> Pesos(0) Then
                        PesosAjustados(CalidadPrincipal) = PesosAjustados(CalidadPrincipal) - TotalAjustado + Pesos(0)
                    End If
                    Volcado(30, CuantosVolcados) = Pesos(0)
                    For i = 0 To 5
                        Volcado(i, CuantosVolcados) = PesosAjustados(i + 1)
                    Next
                    For i = 6 To 29 : Volcado(i, CuantosVolcados) = 0 : Next
                    Volcado(31, CuantosVolcados) = 0
                    Volcado2(0, CuantosVolcados) = CStr(RsVolcadosAyer!numero_albaran)
                    If Trim(RsVolcadosAyer!tipo_marcaje) = "PRECL" Then
                        Volcado2(1, CuantosVolcados) = RsVolcadosAyer!marcaje
                    Else
                        Volcado2(1, CuantosVolcados) = Mid(RsVolcadosAyer!marcaje, 6, 2)
                    End If


                    If Trim(RsVolcadosAyer!tipo_marcaje) = "PRECL" Then
                        SQL = "SELECT * FROM palets_precalibre WHERE empresa = '" & Trim(RsVolcadosAyer!empresa) + "' AND ejercicio = '" + Trim(RsVolcadosAyer!Ejercicio_albaran) + "' and referencia = '" + CStr(RsVolcadosAyer!marcaje) & "'"
                        If RsEntradas.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla palets_precalibre")
                            Exit Sub
                        End If
                        If RsEntradas.RecordCount > 0 Then
                            Volcado2(2, CuantosVolcados) = Format(CDate(RsEntradas!fecha_confeccion), "dd/MM/yyyy")
                        End If
                        RsEntradas.Close()
                    Else
                        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(RsVolcadosAyer!Empresa) + "' AND EJERCICIO = '" + Trim(RsVolcadosAyer!Ejercicio_albaran) + "' and serie_albaran = '" + Trim(RsVolcadosAyer!serie_albaran) + "' and numero_albaran = " + CStr(RsVolcadosAyer!numero_albaran)
                        If RsEntradas.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla entradas_albaranes")
                            Exit Sub
                        End If
                        If RsEntradas.RecordCount > 0 Then
                            Volcado2(2, CuantosVolcados) = Format(CDate(RsEntradas!fecha_entrada), "dd/MM/yyyy")
                        End If
                        RsEntradas.Close()
                    End If
                    For i = 0 To 5 : VolcadoTotal(i) = VolcadoTotal(i) + PesosAjustados(i + 1) : Next
                End If

                VolcadoTotal(50) = VolcadoTotal(50) + Pesos(0)
                CadenaMensaje = ClaveVolcado + "  PESO:" + CStr(Pesos(0))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = CStr(PesosAjustados(0)) + "/" + CStr(PesosAjustados(1)) + "/" + CStr(PesosAjustados(2)) + "/" + CStr(PesosAjustados(3)) + "/" + CStr(PesosAjustados(4)) + "/" + CStr(PesosAjustados(5)) + "/" + CStr(PesosAjustados(6))
                LW.Items.Add(CadenaMensaje)
                RsVolcadosAyer.MoveNext()
            Loop
        End If

        'Lazo Volcados No utilizables (resto de fin de día)
        PrimerVolcadoReservado = 999999
        If CnTabla01.Rs("stock_final") > 0 Then
            KilosReservados = 0
            RsV.MoveLast()

            Do While RsV.BOF = False And KilosReservados < CnTabla01.Rs("stock_final")
                Pesos(0) = RsV!Kilos
                KilosReservados = KilosReservados + Pesos(0)
                PrimerVolcadoReservado = RsV.AbsolutePosition
                RsV.MovePrevious()
            Loop
        End If

        'Lazo Volcados
        If RsV.RecordCount > 0 Then
            HastaVolcado = PrimerVolcadoReservado - 1
            If HastaVolcado > RsV.RecordCount Then
                HastaVolcado = RsV.RecordCount
            End If
            For kk = 1 To HastaVolcado
                RsV.AbsolutePosition = kk

                PB0.Value = PB0.Value + 1
                Pesos(0) = RsV!Kilos
                Pesos(1) = RsV!Kilos_e
                For i = 1 To 5
                    Pesos(i + 1) = CLng(RsV("kilos_" & CStr(i)))
                Next
                TotalMuestreo = 0
                TotalAjustado = 0
                CalidadPrincipal = 0
                PesoCalidadPrincipal = -1

                For i = 1 To 6
                    PesosAjustados(i) = 0
                    If Pesos(i) > PesoCalidadPrincipal Then
                        CalidadPrincipal = i
                        PesoCalidadPrincipal = Pesos(i)
                    End If
                    TotalMuestreo = TotalMuestreo + Pesos(i)
                Next
                If TotalMuestreo = 0 And Pesos(0) > 0 Then
                    CalidadPrincipal = 0
                    PesoCalidadPrincipal = -1
                    For i = 1 To 6
                        Pesos(i) = VolcadoTotal(i - 1)
                        If Pesos(i) > PesoCalidadPrincipal Then
                            CalidadPrincipal = i
                            PesoCalidadPrincipal = Pesos(i)
                        End If
                        TotalMuestreo = TotalMuestreo + Pesos(i)
                    Next
                End If
                If TotalMuestreo = 0 And Pesos(0) > 0 Then
                    CalidadPrincipal = 0
                    PesoCalidadPrincipal = -1
                    For i = 1 To 6
                        If i <= 5 Then Pesos(i) = 20 Else Pesos(i) = 0
                        If Pesos(i) > PesoCalidadPrincipal Then
                            CalidadPrincipal = i
                            PesoCalidadPrincipal = Pesos(i)
                        End If
                        TotalMuestreo = TotalMuestreo + Pesos(i)
                    Next
                End If
                If TotalMuestreo > 0 Then
                    CuantosVolcados = CuantosVolcados + 1
                    If CuantosVolcados > 0 Then
                        ReDim Preserve Volcado(31, CuantosVolcados)
                        ReDim Preserve Volcado2(10, CuantosVolcados)
                    End If
                    ClaveVolcado = Format(CDate(RsV!fecha_marcaje), "yyMMdd") + Format(CDate(RsV!hora_marcaje), "HHmmss") + Format(RsV!Contador, "000000")
                    'If RsV!Contador = 213 Then
                    '    MsgBox("hasta aqui")
                    'End If
                    CadenaVolcado = Format(CuantosVolcados, "00000")
                    oVolcado.Add(ClaveVolcado, CadenaVolcado)
                    For i = 1 To 6
                        PesosAjustados(i) = Math.Round(Pesos(i) * Pesos(0) / TotalMuestreo, 0)
                        TotalAjustado = TotalAjustado + PesosAjustados(i)
                    Next
                    If TotalAjustado <> Pesos(0) Then
                        PesosAjustados(CalidadPrincipal) = PesosAjustados(CalidadPrincipal) - TotalAjustado + Pesos(0)
                    End If
                    Volcado(30, CuantosVolcados) = Pesos(0)
                    For i = 0 To 5
                        Volcado(i, CuantosVolcados) = PesosAjustados(i + 1)
                    Next
                    For i = 6 To 29 : Volcado(i, CuantosVolcados) = 0 : Next
                    Volcado(31, CuantosVolcados) = 0
                    If Not IsDBNull(RsV!numero_albaran) Then Volcado2(0, CuantosVolcados) = CStr(RsV!numero_albaran)
                    If Trim(RsV!tipo_marcaje) = "PRECL" Then
                        Volcado2(1, CuantosVolcados) = RsV!marcaje
                    Else
                        Volcado2(1, CuantosVolcados) = Mid(RsV!marcaje, 6, 2)
                    End If
                    If Trim(RsV!tipo_marcaje) = "PALET" Then
                        SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(RsV!Empresa) + "' AND EJERCICIO = '" + Trim(RsV!Ejercicio_albaran) + "' and serie_albaran = '" + Trim(RsV!serie_albaran) + "' and numero_albaran = " + CStr(RsV!numero_albaran)
                        If RsEntradas.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla entradas_albaranes")
                            Exit Sub
                        End If
                        If RsEntradas.RecordCount > 0 Then
                            Volcado2(2, CuantosVolcados) = Format(CDate(RsEntradas!fecha_entrada), "dd/MM/yyyy")
                        End If
                        RsEntradas.Close()
                    ElseIf Trim(RsV!tipo_marcaje) = "PRECL" Then
                        SQL = "SELECT * FROM palets_precalibre WHERE empresa = '" & Trim(RsV!empresa) + "' AND ejercicio = '" + Trim(RsV!Ejercicio_albaran) + "' and referencia = '" + CStr(RsV!marcaje) & "'"
                        If RsEntradas.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla palets_precalibre")
                            Exit Sub
                        End If
                        If RsEntradas.RecordCount > 0 Then
                            If RsEntradas!fecha_confeccion = RsEntradas!fecha_volcado Then
                                ' Confeccionado y volcado hoy
                            End If
                            Volcado2(2, CuantosVolcados) = Format(CDate(RsEntradas!fecha_confeccion), "dd/MM/yyyy")
                        End If
                        RsEntradas.Close()
                    End If
                    For i = 0 To 5 : VolcadoTotal(i) = VolcadoTotal(i) + PesosAjustados(i + 1) : Next
                End If
                VolcadoTotal(50) = VolcadoTotal(50) + Pesos(0)
                CadenaMensaje = ClaveVolcado + "  PESO:" + CStr(Pesos(0))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = CStr(PesosAjustados(0)) + "/" + CStr(PesosAjustados(1)) + "/" + CStr(PesosAjustados(2)) + "/" + CStr(PesosAjustados(3)) + "/" + CStr(PesosAjustados(4)) + "/" + CStr(PesosAjustados(5)) + "/" + CStr(PesosAjustados(6))
                LW.Items.Add(CadenaMensaje)
            Next
        End If
        CadenaMensaje = "Volcado total: " + CStr(VolcadoTotal(50))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal por clasificaciones: " + CStr(VolcadoTotal(0) + VolcadoTotal(1) + VolcadoTotal(2) + VolcadoTotal(3) + VolcadoTotal(4) + VolcadoTotal(5))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal E: " + CStr(VolcadoTotal(0))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal 1: " + CStr(VolcadoTotal(1))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal 2: " + CStr(VolcadoTotal(2))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal 3: " + CStr(VolcadoTotal(3))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal 4: " + CStr(VolcadoTotal(4))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "VolcadoTotal 5: " + CStr(VolcadoTotal(5))
        LW.Items.Add(CadenaMensaje)

        'Ordenar oVolcado en oVolcadoOrdenado
        ReDim oVolcadoOrdenado(oVolcado.Count - 1)
        For i = 0 To oVolcado.Count - 1
            oVolcadoOrdenado(i) = Trim(oVolcado.Keys(i))
        Next
        Array.Sort(oVolcadoOrdenado)

        'Lazo Calidades-Calibre
        RsCalidadesCalibre.MoveFirst()
        Do While Not RsCalidadesCalibre.EOF
            PB0.Value = PB0.Value + 1
            ClaveCalidades = Mid(Trim(RsCalidadesCalibre!codigo_variedad) + "        ", 1, 8) + Mid(Trim(RsCalidadesCalibre!calibre) + "            ", 1, 12)
            CadenaCalidades = "XXXXXX"
            If Trim(RsCalidadesCalibre!especial) = "S" Or Trim(RsCalidadesCalibre!especial) = "O" Or Trim(RsCalidadesCalibre!especial) = "X" Then Mid(CadenaCalidades, 1, 1) = Trim(RsCalidadesCalibre!especial)
            If Trim(RsCalidadesCalibre!Primera) = "S" Or Trim(RsCalidadesCalibre!Primera) = "O" Or Trim(RsCalidadesCalibre!Primera) = "X" Then Mid(CadenaCalidades, 2, 1) = Trim(RsCalidadesCalibre!Primera)
            If Trim(RsCalidadesCalibre!segunda) = "S" Or Trim(RsCalidadesCalibre!segunda) = "O" Or Trim(RsCalidadesCalibre!segunda) = "X" Then Mid(CadenaCalidades, 3, 1) = Trim(RsCalidadesCalibre!segunda)
            If Trim(RsCalidadesCalibre!tercera) = "S" Or Trim(RsCalidadesCalibre!tercera) = "O" Or Trim(RsCalidadesCalibre!tercera) = "X" Then Mid(CadenaCalidades, 4, 1) = Trim(RsCalidadesCalibre!tercera)
            If Trim(RsCalidadesCalibre!cuarta) = "S" Or Trim(RsCalidadesCalibre!cuarta) = "O" Or Trim(RsCalidadesCalibre!cuarta) = "X" Then Mid(CadenaCalidades, 5, 1) = Trim(RsCalidadesCalibre!cuarta)
            If Trim(RsCalidadesCalibre!otras) = "S" Or Trim(RsCalidadesCalibre!otras) = "O" Or Trim(RsCalidadesCalibre!otras) = "X" Then Mid(CadenaCalidades, 6, 1) = Trim(RsCalidadesCalibre!otras)
            oCalidades.Add(ClaveCalidades, CadenaCalidades)
            RsCalidadesCalibre.MoveNext()
        Loop
        RsCalidadesCalibre.Close()

        'Lazo Confección
        CuantosConfeccionados = -1
        If RsC.RecordCount > 0 Then
            RsC.MoveFirst()

            Do While RsC.EOF = False
                ClaveIndustria = ""
                CalibrePalet = ""
                VariedadPalet = ""
                ClaveCalidades = ""
                CadenaCalidades = "XXXOSS"
                Clientex = ""
                Cargax = ""
                Paletx = ""
                NombreClientex = ""
                Albaranx = ""
                PB0.Value = PB0.Value + 1


                If Mid(Trim(RsC!tipo_marcaje), 1, 1) <> "I" And Mid(Trim(RsC!tipo_marcaje), 1, 1) <> "S" And Trim(RsC!tipo_marcaje) <> "CPRE" Then
                    ClaveIndustria = "C"

                    SQL = "SELECT * FROM palets WHERE codigo_palet='" + Trim(RsC!marcaje) + "' AND fecha_confeccion='" + Format(CDate(RsC!fecha_marcaje), "dd-MM-yyyy") + "' AND hora_confeccion='" & Format(CDate(RsC!hora_marcaje), "HH:mm:ss") & "'"
                    If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla palets")
                        Exit Sub
                    End If

                    If RsPalets.RecordCount > 0 Then
                        If Trim(RsPalets!tipo_fabricacion) <> "R" Then
                            Paletx = Trim(RsC!marcaje)
                            If Not IsDBNull(RsPalets!fecha_carga) Then
                                Cargax = Format(CDate(RsPalets!fecha_carga), "dd/MM/yy")
                            End If
                            SQL = "SELECT * FROM ordenes_confeccion WHERE EMPRESA='" + Trim(ObjetoGlobal.EmpresaActual) + "' AND numero_orden=" + CStr(RsPalets!orden_Confeccion)
                            If RsOrdenesConfeccion.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla ordenes_confeccion")
                                Exit Sub
                            End If
                            If RsOrdenesConfeccion.RecordCount > 0 Then
                                Clientex = CStr(RsOrdenesConfeccion!codigo_cliente)
                                PesoPalet = Math.Round(RsC!Kilos, 0)
                                VariedadPalet = Trim(RsOrdenesConfeccion!codigo_variedad)
                                CalibrePalet = Trim(RsOrdenesConfeccion!calibre_tecnico)
                                ClaveCalidades = Mid(Trim(RsOrdenesConfeccion!codigo_variedad) + "        ", 1, 8) + Mid(Trim(RsOrdenesConfeccion!calibre_tecnico) + "            ", 1, 12)
                                If oCalidades.ContainsKey(ClaveCalidades) = True Then
                                    CadenaCalidades = oCalidades.Item(ClaveCalidades)
                                ElseIf Trim(CalibrePalet) = "G" Then
                                    CadenaCalidades = "XXXXXX"
                                Else
                                    If FlagTodos = False Then
                                        MsgBox("Atención: No existe ficha de calidades para el palet código: '" + Trim(RsC!marcaje) + "'" + vbCrLf + "Corresponde a la variedad:'" + Trim(VariedadPalet) + "' y al calibre:'" + Trim(CalibrePalet) + "'" + vbCrLf + "Se cancela el cierre.")
                                    Else
                                        Fichero.EscribeLinea("Atención: No existe ficha de calidades para el palet código: '" + Trim(RsV!marcaje) + "'" + vbCrLf + "Corresponde a la variedad:'" + Trim(VariedadPalet) + "' y al calibre:'" + Trim(CalibrePalet) + "'" + vbCrLf + "Se cancela el cierre.")
                                    End If
                                    If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                                    RsC.AbsolutePosition = RegistroInicialConfeccion
                                    Pic0.Visible = False
                                    PB0.Visible = False
                                    Exit Sub
                                End If
                            Else
                                If FlagTodos = False Then
                                    MsgBox("Atención: No existe ficha de orden para el palet código: '" + Trim(RsC!marcaje) + "'" + vbCrLf + "Se cancela el cierre.")
                                Else
                                    Fichero.EscribeLinea("Atención: No existe ficha de orden para el palet código: '" + Trim(RsC!marcaje) & "'" + vbCrLf + "Se cancela el cierre.")
                                End If
                                If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                                RsC.AbsolutePosition = RegistroInicialConfeccion
                                Pic0.Visible = False
                                PB0.Visible = False
                                Exit Sub
                            End If
                            RsOrdenesConfeccion.Close()

                            If Not IsDBNull(RsPalets!orden_carga) Then
                                SQL = "SELECT * FROM CABECERAS_SALIDA WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND ORDEN_CARGA = " + CStr(RsPalets!orden_carga)
                                If RsSalida.Open(SQL, ObjetoGlobal.cn) = False Then
                                    MsgBox("Error al abrir la tabla cabeceras_salida")
                                    Exit Sub
                                End If
                                If RsSalida.RecordCount > 0 Then
                                    If IsDBNull(RsSalida!destinatario) Then
                                        Clientex = CStr(RsSalida!codigo_cliente)
                                        If Not IsDBNull(RsSalida!nombre_cliente) Then
                                            NombreClientex = Trim(RsSalida!nombre_cliente)
                                        End If
                                    Else
                                        Clientex = CStr(RsSalida!destinatario)
                                        If Not IsDBNull(RsSalida!nombre_dest) Then
                                            NombreClientex = Trim(RsSalida!nombre_dest)
                                        End If
                                    End If
                                    Albaranx = CStr(Val(RsSalida!numero_documento))
                                    Cargax = Format(CDate(RsSalida!Fecha_documento), "dd/MM/yy")
                                End If
                                RsSalida.Close()
                            End If
                        Else
                            If FlagTodos = False Then
                                MsgBox("Atención: No existe ficha de palet código: '" + Trim(RsC!marcaje) + "'" + vbCrLf + "Se cancela el cierre.")
                            Else
                                Fichero.EscribeLinea("Atención: No existe ficha de palet código: '" + Trim(RsC!marcaje) + "'" + vbCrLf + "Se cancela el cierre.")
                            End If
                            If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                            RsC.AbsolutePosition = RegistroInicialConfeccion
                            Pic0.Visible = False
                            PB0.Visible = False
                            Exit Sub
                        End If
                    Else
                        If FlagTodos = False Then
                            MsgBox("Atención: No existe ficha de palet código: '" + Trim(RsC!marcaje) + "'" + vbCrLf + "Se cancela el cierre.")
                        Else
                            Fichero.EscribeLinea("Atención: No existe ficha de palet código: '" + Trim(RsC!marcaje) + "'" + vbCrLf + "Se cancela el cierre.")
                        End If
                        If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                        RsC.AbsolutePosition = RegistroInicialConfeccion
                        Pic0.Visible = False
                        PB0.Visible = False
                        Exit Sub
                    End If
                    RsPalets.Close()

                ElseIf Trim(RsC!tipo_marcaje) = "CPRE" Then
                    ClaveIndustria = "P"
                    SQL = "SELECT * FROM palets_precalibre WHERE referencia='" & Trim(RsC!marcaje) & "' AND fecha_confeccion='" & Format(CDate(RsC!fecha_marcaje), "dd/MM/yyyy") & "' AND substring('0'+hora_confeccion,len(hora_confeccion) -6,8) = '" & Trim(Format(CDate(RsC!hora_marcaje), "HH:mm:ss")) & "'"
                    If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                        MsgBox("Error al abrir la tabla palets_precalibre")
                        Exit Sub
                    End If
                    If RsPalets.RecordCount > 0 Then
                        Paletx = RsPalets!Codigo_palet 'Trim(rsC!marcaje)
                        Clientex = ""
                        NombreClientex = "PRECALIBRADO"
                        PesoPalet = Math.Round(RsC!Kilos, 0)
                        VariedadPalet = Trim(RsPalets!codigo_variedad)
                        CalibrePalet = Trim(RsPalets!codigo_calibre)
                        ClaveCalidades = Mid(Trim(RsPalets!codigo_variedad) + "        ", 1, 8) + Mid(Trim(RsPalets!codigo_calibre) + "            ", 1, 12)
                        If oCalidades.ContainsKey(ClaveCalidades) = True Then
                            CadenaCalidades = oCalidades.Item(ClaveCalidades)
                        ElseIf Trim(CalibrePalet) = "G" Or Trim(CalibrePalet) = "" Then
                            CadenaCalidades = "XXXXXX"
                        ElseIf Trim(CalibrePalet) = "-" Then
                            CadenaCalidades = "XXXXXX"
                        Else
                            If FlagTodos = False Then
                                MsgBox("Atención: No existe ficha de calidades para el palet código: '" & Trim(RsC!marcaje) & "'" + vbCrLf + "Corresponde a la variedad:'" + Trim(VariedadPalet) + "' y al calibre:'" + Trim(CalibrePalet) + "'" + vbCrLf + "Se cancela el cierre.")
                            Else
                                Fichero.EscribeLinea("Atención: No existe ficha de calidades para el palet código: '" & Trim(RsC!marcaje) & "'" + vbCrLf + "Corresponde a la variedad:'" + Trim(VariedadPalet) + "' y al calibre:'" + Trim(CalibrePalet) + "'" + vbCrLf + "Se cancela el cierre.")
                            End If
                            If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                            RsC.AbsolutePosition = RegistroInicialConfeccion
                            Pic0.Visible = False
                            PB0.Visible = False
                            Exit Sub
                        End If
                    Else
                        ' Ver que pasa con esto
                        If FlagTodos = False Then
                            MsgBox("Atención: No existe ficha de palet código: '" & Trim(RsC!marcaje) & "'" + vbCrLf + "Se cancela el cierre.")
                        Else
                            Fichero.EscribeLinea("Atención: No existe ficha de palet código: '" & Trim(RsC!marcaje) & "'" + vbCrLf + "Se cancela el cierre.")
                        End If
                        If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                        RsC.AbsolutePosition = RegistroInicialConfeccion
                        Pic0.Visible = False
                        PB0.Visible = False
                        Exit Sub
                    End If
                    RsPalets.Close()
                ElseIf Trim(RsC!marcaje) = "INAPLGR" Or Mid(RsC!marcaje, 1, 2) = "NG" Or Mid(RsC!marcaje, 1, 2) = "BG" Or Mid(RsC!marcaje, 1, 2) = "RG" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaNaranjaGrande
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "INAPLPQ" Or Mid(RsC!marcaje, 1, 2) = "NQ" Or Mid(RsC!marcaje, 1, 2) = "BQ" Or Mid(RsC!marcaje, 1, 2) = "RQ" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaNaranjaPequeno
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "IMAPLGR" Or Mid(RsC!marcaje, 1, 2) = "MG" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaClementinaGrande
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "IMAPLPQ" Or Mid(RsC!marcaje, 1, 2) = "MQ" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaClementinaPequeno
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "ISAPLGR" Or Mid(RsC!marcaje, 1, 2) = "SG" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaClementinaGrande
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "ISAPLPQ" Or Mid(RsC!marcaje, 1, 2) = "SQ" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaClementinaPequeno
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "IHIPLGR" Or Mid(RsC!marcaje, 1, 2) = "HG" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaClementinaGrande
                    ClaveIndustria = "I"
                ElseIf Trim(RsC!marcaje) = "IHIPLPQ" Or Mid(RsC!marcaje, 1, 2) = "HQ" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaClementinaPequeno
                    ClaveIndustria = "I"
                ElseIf Mid(Trim(RsC!marcaje), 1, 2) = "SN" Or Mid(Trim(RsC!marcaje), 1, 2) = "SB" Or Mid(Trim(RsC!marcaje), 1, 2) = "SB" Or Mid(Trim(RsC!marcaje), 1, 2) = "SY" Then
                    NombreClientex = "INDUSTRIA"
                    PesoPalet = PesoIndustriaSandia
                    ClaveIndustria = "I"
                Else
                    If FlagTodos = False Then
                        MsgBox("Palet no reconocido")
                    Else
                        Fichero.EscribeLinea("Palet no reconocido")
                    End If
                End If
                If PesoPalet > 0 Then
                    ClaveConfeccionado = Format(CDate(RsC!fecha_marcaje), "yyMMdd") + Format(CDate(RsC!hora_marcaje), "HHmmss") + Format(RsC.AbsolutePosition, "000000") + Trim(ClaveIndustria) + Mid(Trim(VariedadPalet) + "        ", 1, 8) + Mid(Trim(CalibrePalet) + "      ", 1, 6) + Trim(CadenaCalidades)
                    ConfeccionadoTotal = ConfeccionadoTotal + PesoPalet
                    CuantosConfeccionados = CuantosConfeccionados + 1
                    If CuantosConfeccionados > 0 Then
                        ReDim Preserve Confeccionado(60, CuantosConfeccionados)
                        ReDim Preserve Confeccionado2(10, CuantosConfeccionados)
                    End If
                    Confeccionado2(0, CuantosConfeccionados) = Paletx
                    Confeccionado2(1, CuantosConfeccionados) = Clientex
                    Confeccionado2(2, CuantosConfeccionados) = NombreClientex
                    Confeccionado2(3, CuantosConfeccionados) = Albaranx
                    Confeccionado2(4, CuantosConfeccionados) = Cargax
                    CadenaConfeccionado = Format(CuantosConfeccionados, "0000000")
                    Confeccionado(60, CuantosConfeccionados) = PesoPalet
                    oConfeccionado.Add(ClaveConfeccionado, CadenaConfeccionado)
                    If oNecesidades.ContainsKey(CadenaCalidades) Then
                        PesoNecesidad = CLng(Mid(oNecesidades.Item(CadenaCalidades), 1, 10))
                        PesoNecesidad = PesoNecesidad + PesoPalet
                        CadenaNecesidades = Format(PesoNecesidad, "0000000000") + "0000000000"
                        oNecesidades.Item(CadenaCalidades) = CadenaNecesidades
                    Else
                        CadenaNecesidades = Format(PesoPalet, "0000000000") + "0000000000"
                        oNecesidades.Add(CadenaCalidades, CadenaNecesidades)
                    End If
                End If
                RsC.MoveNext()
            Loop
        End If

        'Ordenar oConfecionado en oConfeccionadoOrdenado 
        ReDim oConfeccionadoOrdenado(oConfeccionado.Count - 1)
        For i = 0 To oConfeccionado.Count - 1
            oConfeccionadoOrdenado(i) = Trim(oConfeccionado.Keys(i))
        Next
        Array.Sort(oConfeccionadoOrdenado)

        'Ordenar oNecesidades en oNecesiddesOrdenado 
        ReDim oNecesidadesOrdenado(oNecesidades.Count - 1)
        For i = 0 To oNecesidades.Count - 1
            oNecesidadesOrdenado(i) = Trim(oNecesidades.Keys(i))
        Next
        Array.Sort(oNecesidadesOrdenado)


        For i = 0 To oNecesidades.Count - 1
            Clave = oNecesidadesOrdenado(i)
            CadenaNecesidades = oNecesidades.Item(Clave)
            PesoNecesidad = CLng(Mid(CadenaNecesidades, 1, 10))
            CadenaNecesidades = Format(PesoNecesidad, "0000000000") + Format(PesoNecesidad, "0000000000")
            oNecesidades.Item(Clave) = CadenaNecesidades
            CadenaMensaje = "Calidad " + oNecesidadesOrdenado(i)
            CadenaMensaje = CadenaMensaje + " " + CStr(PesoNecesidad)
            LW.Items.Add(CadenaMensaje)
        Next

        'Lazo Cuadre
        Pic0.Visible = False
        PB0.Visible = False
        PB1.Value = 0
        PB1.Maximum = 2 * oConfeccionado.Count + oVolcado.Count
        PB1.Visible = True
        Pic1.Visible = True
        Me.Refresh()

        If VolcadoTotal(50) > 0 Then
            RatioMermaTotal = ConfeccionadoTotal / VolcadoTotal(50)
        Else
            If FlagTodos = False Then
                MsgBox("No es posible realizar el cuadre del proceso." + vbCrLf + "No existen kilos volcados.")
            Else
                Fichero.EscribeLinea("No es posible realizar el cuadre del proceso." + vbCrLf + "No existen kilos volcados.")
            End If
            If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
            RsC.AbsolutePosition = RegistroInicialConfeccion
            Pic1.Visible = False
            PB1.Visible = False
            Exit Sub
        End If
        If RatioMermaTotal > 1 Then
            If FlagTodos = False Then
                MsgBox("No es posible realizar el cuadre del proceso." + vbCrLf + "Más kilos confeccionados que volcados.")
            Else
                Fichero.EscribeLinea("No es posible realizar el cuadre del proceso." + vbCrLf + "Más kilos confeccionados que volcados.")
            End If
            If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
            RsC.AbsolutePosition = RegistroInicialConfeccion
            Pic1.Visible = False
            PB1.Visible = False
            Exit Sub
        End If
        RatioMermaTotal = RatioMermaTotal + ((1 - RatioMermaTotal) / 3) 'OJO
        CadenaMensaje = "Ratio merma usado:" & CStr(RatioMermaTotal)
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Ratio merma teorico:" & CStr(ConfeccionadoTotal / VolcadoTotal(50))
        LW.Items.Add(CadenaMensaje)
        LW.Items.Add("")

        For j = 0 To oVolcado.Count - 1
            PB1.Value = PB1.Value + 1
            CadenaVolcado = Trim(oVolcado.Item(oVolcadoOrdenado(j)))
            k = CInt(CadenaVolcado)
            Tot = 0
            For l = 0 To 5
                If Volcado(l, k) > 0 Then
                    Volcado(10 + l, k) = Math.Round(Volcado(l, k) * RatioMermaTotal, 0)
                    Tot = Tot + Volcado(10 + l, k)
                End If
            Next
            If Tot <> Math.Round(Volcado(30, k) * RatioMermaTotal, 0) Then
                GrupoAAsignar = -1
                MaximoValorAAsignar = -99999999.0#
                For l = 0 To 5
                    If Volcado(10 + l, k) > 0 Then
                        If GrupoAAsignar = -1 Or Volcado(10 + l, k) > MaximoValorAAsignar Then
                            GrupoAAsignar = l
                            MaximoValorAAsignar = Volcado(10 + l, k)
                        End If
                    End If
                Next
                If GrupoAAsignar > -1 Then
                    Volcado(10 + GrupoAAsignar, k) = Volcado(10 + GrupoAAsignar, k) - Tot + Math.Round(Volcado(30, k) * RatioMermaTotal, 0)  'OJO esto lo he corregido. Estaba mal en vb6
                End If
            End If
            Volcado(31, k) = Math.Round(Volcado(30, k) * RatioMermaTotal, 0)
            For l = 0 To 5
                VolcadoTotal(10 + l) = VolcadoTotal(10 + l) + Volcado(10 + l, k)
            Next
        Next
        ConfeccionadoAcumulado = 0
        For i = 0 To oConfeccionado.Count - 1
            PB1.Value = PB1.Value + 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            PesoPalet = Confeccionado(60, PunteroConfeccionado)
            CadenaCalidades = Mid(ClaveConfeccionado, 34)
            HoraConfeccion = Mid(ClaveConfeccionado, 7, 2) + ":" + Mid(ClaveConfeccionado, 9, 2) + ":" & Mid(ClaveConfeccionado, 11, 2)
            For j = 0 To 31 : VolcadoHastaAqui(j) = 0 : Next
            For j = 0 To oVolcado.Count - 1
                ClaveVolcado = Trim(oVolcadoOrdenado(j))
                CadenaVolcado = Trim(oVolcado.Item(ClaveVolcado))
                k = CInt(CadenaVolcado)
                HoraVolcado = Mid(ClaveVolcado, 7, 2) & ":" & Mid(ClaveVolcado, 9, 2) & ":" & Mid(ClaveVolcado, 11, 2)
                If Mid(ClaveVolcado, 1, 6) < Mid(ClaveConfeccionado, 1, 6) Or DiferenciaSegundos(HoraVolcado, HoraConfeccion) > RetardoLinea Then 'OJO
                    If Volcado(30, k) > 0 Then
                        For l = 0 To 5
                            VolcadoHastaAqui(l) = VolcadoHastaAqui(l) + Volcado(l, k)
                            VolcadoHastaAqui(10 + l) = VolcadoHastaAqui(10 + l) + Volcado(10 + l, k)
                        Next
                        VolcadoHastaAqui(30) = VolcadoHastaAqui(30) + Volcado(30, k)
                        VolcadoHastaAqui(31) = VolcadoHastaAqui(31) + Volcado(31, k)
                    End If
                Else
                    Exit For
                End If
            Next
            If ConfeccionadoAcumulado + PesoPalet > VolcadoHastaAqui(30) Then
                Cadena = Mid(ClaveConfeccionado, 13, 6)
                CodigoError = CLng(Cadena)
                RsC.AbsolutePosition = CodigoError
                If FlagTodos = False Then
                    MsgBox("No es posible realizar el cuadre del proceso." + vbCrLf + "No existen kilos volcados suficientes para el palet: " + Trim(RsC!marcaje) + vbCrLf + "confeccionado el :" + Format(RsC!fecha_marcaje) + " " + Trim(RsC!hora_marcaje))
                Else
                    Fichero.EscribeLinea("No es posible realizar el cuadre del proceso." + vbCrLf + "No existen kilos volcados suficientes para el palet: " + Trim(RsC!marcaje) + vbCrLf + "confeccionado el :" + Format(RsC!fecha_marcaje) + " " + Trim(RsC!hora_marcaje))
                End If
                If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                RsC.AbsolutePosition = RegistroInicialConfeccion
                Pic1.Visible = False
                PB1.Visible = False
                Exit Sub
            End If
            If ConfeccionadoAcumulado >= VolcadoHastaAqui(31) Then
                Cadena = Mid(ClaveConfeccionado, 13, 6)
                CodigoError = CLng(Cadena)
                RsC.AbsolutePosition = CodigoError
                If FlagTodos = False Then
                    MsgBox("No existen kilos volcados suficientes (SI SE DESCUENTA MERMA) para el palet: " + Trim(RsC!marcaje) + vbCrLf + "confeccionado el :" + Format(RsC!fecha_marcaje) + " " + Trim(RsC!hora_marcaje))
                Else
                    Fichero.EscribeLinea("No existen kilos volcados suficientes (SI SE DESCUENTA MERMA) para el palet: " + Trim(RsC!marcaje) + vbCrLf + "confeccionado el :" + Format(RsC!fecha_marcaje) + " " + Trim(RsC!hora_marcaje))
                End If
                If RsV.RecordCount > 0 Then RsV.AbsolutePosition = RegistroInicialVolcado
                RsC.AbsolutePosition = RegistroInicialConfeccion
                Pic1.Visible = False
                PB1.Visible = False
                Exit Sub
            End If
            If ConfeccionadoAcumulado + PesoPalet > VolcadoHastaAqui(31) Then
                Cadena = Mid(ClaveConfeccionado, 13, 6)
                CodigoError = CLng(Cadena)
                RsC.AbsolutePosition = CodigoError
                If FlagTodos = False Then
                    '            MsgBox("No existen kilos volcados suficientes (SI SE DESCUENTA MERMA) para el palet: " + Trim(RsC!marcaje) + vbCrLf + "confeccionado el :" + Format(RsAux!fecha_marcaje) + " " + Trim(RsAux!hora_marcaje)
                Else
                    Fichero.EscribeLinea("No existen kilos volcados suficientes (SI SE DESCUENTA MERMA) para el palet: " + Trim(RsC!marcaje) + vbCrLf + "confeccionado el :" + Format(RsC!fecha_marcaje) + " " + Trim(RsC!hora_marcaje))
                End If
            End If
            ConfeccionadoAcumulado = ConfeccionadoAcumulado + PesoPalet
            For j = 0 To 5
                Confeccionado(20 + j, PunteroConfeccionado) = VolcadoHastaAqui(j)
                Confeccionado(30 + j, PunteroConfeccionado) = VolcadoHastaAqui(10 + j)
            Next
        Next
        For i = 0 To 31 : Utilizado(i) = 0 : Next
        For i = 0 To oConfeccionado.Count - 1
            If PB1.Value < PB1.Maximum Then PB1.Value = PB1.Value + 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            ClaveIndustria = Mid(ClaveConfeccionado, 19, 1)
            If Trim(ClaveIndustria) = "I" Then
                CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
                PunteroConfeccionado = CLng(CadenaConfeccionado)
                PesoPalet = Confeccionado(60, PunteroConfeccionado)
                CadenaCalidades = Mid(ClaveConfeccionado, 34)
                '        HoraConfeccion = Mid(ClaveConfeccionado, 7, 2) & ":" & Mid(ClaveConfeccionado, 9, 2) & ":" & Mid(ClaveConfeccionado, 11, 2)
                PesoPendiente = PesoPalet
                For j = 6 To 0 Step -1
                    If PesoPendiente > 0 Then
                        If Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(10 + j) > 0 Then
                            If Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(10 + j) >= PesoPendiente Then
                                Utilizado(10 + j) = Utilizado(10 + j) + PesoPendiente
                                PesoUtilizado = Math.Round(PesoPendiente / RatioMermaTotal, 0)
                                If Confeccionado(20 + j, PunteroConfeccionado) < PesoUtilizado + Utilizado(j) Then
                                    PesoUtilizado = Confeccionado(20 + j, PunteroConfeccionado) - Utilizado(j)
                                End If
                                Utilizado(j) = Utilizado(j) + PesoUtilizado
                                Confeccionado(j, PunteroConfeccionado) = PesoPendiente
                                Utilizado(30) = Utilizado(30) + PesoUtilizado
                                Utilizado(31) = Utilizado(31) + PesoPendiente
                                PesoPendiente = 0
                            Else
                                PesoUtilizadoConMerma = Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(10 + j)
                                Confeccionado(j, PunteroConfeccionado) = PesoUtilizadoConMerma
                                Utilizado(10 + j) = Utilizado(10 + j) + PesoUtilizadoConMerma
                                PesoUtilizado = Math.Round(PesoUtilizadoConMerma / RatioMermaTotal, 0)
                                If Confeccionado(20 + j, PunteroConfeccionado) < PesoUtilizado + Utilizado(j) Then
                                    PesoUtilizado = Confeccionado(20 + j, PunteroConfeccionado) - Utilizado(j)
                                End If
                                Utilizado(j) = Utilizado(j) + PesoUtilizado
                                Utilizado(30) = Utilizado(30) + PesoUtilizado
                                Utilizado(31) = Utilizado(31) + PesoUtilizadoConMerma
                                PesoPendiente = PesoPendiente - PesoUtilizadoConMerma
                            End If
                        End If
                    End If
                Next
                CadenaMensaje = "Palet industria:" & Trim(ClaveConfeccionado) & " PESO:" & CStr(Confeccionado(60, PunteroConfeccionado))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Asignado:"
                For k = 0 To 5
                    CadenaMensaje = CadenaMensaje + CStr(Confeccionado(k, PunteroConfeccionado)) + "  "
                Next
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
            End If
        Next
        LW.Items.Add("")
        CadenaMensaje = "Utilizado industria:" + CStr(Utilizado(30)) + "  " + CStr(Utilizado(31))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = ""
        For k = 0 To 5
            CadenaMensaje = CadenaMensaje + CStr(Utilizado(k)) + "  "
        Next
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = ""
        For k = 0 To 5
            CadenaMensaje = CadenaMensaje + CStr(Utilizado(10 + k)) + "  "
        Next
        LW.Items.Add(CadenaMensaje)
        LW.Items.Add("")
        If oNecesidades.ContainsKey("XXXOSS") Then
            oNecesidades.Remove("XXXOSS") 'Grupo de industria

            'Ordenar oNecesidades en oNecesiddesOrdenado 
            ReDim oNecesidadesOrdenado(0)
            ReDim oNecesidadesOrdenado(oNecesidades.Count - 1)
            For rr = 0 To oNecesidades.Count - 1
                oNecesidadesOrdenado(rr) = Trim(oNecesidades.Keys(rr))
            Next
            Array.Sort(oNecesidadesOrdenado)
        End If

        'Lazo Reparto por calidades
        Pic1.Visible = False
        PB1.Visible = False
        For i = 0 To 10 : LW.Items.Add("") : Next
        LW.Items.Add("AHORA CON LAS CALIDADES S")
        For i = 0 To 2 : LW.Items.Add("") : Next
        If oNecesidades.Count > 0 Then
            ReDim ValorMaximo(oNecesidades.Count - 1)
            ReDim GrupoActivo(oNecesidades.Count - 1)
        Else
            ReDim ValorMaximo(0)
            ReDim GrupoActivo(0)
        End If
        ReDim TotalAsignado(10, oNecesidades.Count)
        ReDim TotalUtilizado(10, oNecesidades.Count)
        ' 0 = Lo que se necesita originalmente
        ' 1 = Lo que se necesita ahora
        ' 2 = Lo que hay no volcado en las calidades aceptables
        ' El cociente 1/2 indica necesidad de asignar ese grupo
        LW.Items.Add("")
        For i = 0 To 5
            VolcadoTotal(40 + i) = Math.Round(VolcadoTotal(10 + i) - Utilizado(10 + i), 0)
        Next
        CadenaMensaje = "Antes de empezar:"
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil especial: " + CStr(VolcadoTotal(40))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil primera: " + CStr(VolcadoTotal(41))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil segunda: " + CStr(VolcadoTotal(42))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil tercera: " + CStr(VolcadoTotal(43))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil cuarta: " + CStr(VolcadoTotal(44))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil quinta: " + CStr(VolcadoTotal(45))
        LW.Items.Add(CadenaMensaje)

        TotalPorAsignar = 0
        For i = 0 To 9 : TotalPorAsignar = TotalPorAsignar + VolcadoTotal(40 + i) : Next
        For i = 0 To oNecesidades.Count - 1
            GrupoActivo(i) = True
        Next
        GruposActivos = oNecesidades.Count
        PB2.Minimum = 0
        PB2.Value = 0
        PB2.Maximum = TotalPorAsignar
        PB2.Visible = True
        Pic2.Visible = True
        Me.Refresh()

        Do While TotalPorAsignar > 0 And GruposActivos > 0
            MaximoValorAAsignar = -99999999.0#
            If TotalPorAsignar < PB2.Maximum Then PB2.Value = PB2.Maximum - TotalPorAsignar
            GrupoAAsignar = -1
            For i = 0 To oNecesidades.Count - 1
                If GrupoActivo(i) = True Then
                    Clave = oNecesidadesOrdenado(i)
                    Dato = oNecesidades.Item(Clave)
                    Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                    Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                    If Numeros(1) > 0 Then
                        Numeros(2) = 0
                        For j = 0 To 5
                            If Mid(Clave, j + 1, 1) = "S" Then
                                Numeros(2) = Numeros(2) + VolcadoTotal(40 + j)
                            End If
                        Next
                        If Numeros(2) = 0 Then
                            ValorMaximo(i) = 0
                        Else
                            ValorMaximo(i) = Numeros(1) / Numeros(2)
                        End If
                        If GrupoAAsignar = -1 Or MaximoValorAAsignar < ValorMaximo(i) Then
                            GrupoAAsignar = i
                            MaximoValorAAsignar = ValorMaximo(i)
                            ClaveAAsignar = Clave
                            DatoAAsignar = Dato
                        End If
                    End If
                End If
            Next
            If GrupoAAsignar > -1 Then
                'Hay que asignar un kilo al grupo que indica GrupoAAsignar (el más necesitado, el que tiene mayor ratio de necesidad).
                'Es necesario elegir la calidad a asignar a ese grupo
                'Se debe elegir la calidad entre las aceptables para el grupo que minimice el máximo de los ratios de necesidad 1/2
                CalidadSeleccionada = -1
                MaximoSeleccionado = 99999999.0#
                For k = 0 To 5
                    If Mid(ClaveAAsignar, k + 1, 1) = "S" And VolcadoTotal(40 + k) > 0 Then
                        MaximoValor = -99999999.0#
                        Grupo = -1
                        For i = 0 To oNecesidades.Count - 1
                            If i <> GrupoAAsignar Then
                                Clave = oNecesidadesOrdenado(i)
                                Dato = oNecesidades.Item(Clave)
                                Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                                Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                                '                    If i = GrupoAAsignar Then Numeros(1) = Numeros(1) - 1
                                Numeros(2) = 0
                                For j = 0 To 5
                                    If Mid(Clave, j + 1, 1) = "S" Then
                                        Numeros(2) = Numeros(2) + VolcadoTotal(40 + j)
                                        If j = k Then Numeros(2) = Numeros(2) - 1
                                    End If
                                Next
                                If Numeros(2) = 0 Then
                                    ValorMaximo(i) = 0
                                Else
                                    ValorMaximo(i) = Numeros(1) / Numeros(2)
                                End If
                                If Grupo = -1 Or MaximoValor < ValorMaximo(i) Then
                                    Grupo = i
                                    MaximoValor = ValorMaximo(i)
                                    ClaveNueva = Clave
                                    DatoNuevo = Dato
                                End If
                            End If
                        Next
                        If MaximoValor < MaximoSeleccionado Then
                            CalidadSeleccionada = k
                            MaximoSeleccionado = MaximoValor
                        End If
                    End If
                Next
                If CalidadSeleccionada > -1 Then
                    'Vamos a descontar 1 kg. de la calidad 'CalidadSeleccionada'
                    VolcadoTotal(40 + CalidadSeleccionada) = VolcadoTotal(40 + CalidadSeleccionada) - 1
                    Clave = oNecesidadesOrdenado(GrupoAAsignar)
                    Dato = oNecesidades.Item(Clave)
                    Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                    Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                    Numeros(1) = Numeros(1) - 1
                    Dato = ""
                    For j = 0 To 1
                        Dato = Dato & Format(Numeros(j), "0000000000")
                    Next j
                    oNecesidades.Item(Clave) = Dato
                    TotalAsignado(CalidadSeleccionada, GrupoAAsignar) = TotalAsignado(CalidadSeleccionada, GrupoAAsignar) + 1
                    TotalPorAsignar = 0
                    For i = 0 To 10 : TotalPorAsignar = TotalPorAsignar + VolcadoTotal(40 + i) : Next
                Else
                    CadenaMensaje = "No puedo asignar ninguna calidad al grupo:" + CStr(GrupoAAsignar)
                    LW.Items.Add(CadenaMensaje)
                    GrupoActivo(GrupoAAsignar) = False
                    GruposActivos = GruposActivos - 1
                End If
            Else
                TotalPorAsignar = 0
            End If
            If TotalPorAsignar Mod 100 = 0 Then
                For i = 0 To oNecesidades.Count - 1
                    CadenaMensaje = "Grupo " + Trim(oNecesidadesOrdenado(i)) + ":"
                    Tot = 0
                    For j = 0 To 5
                        Tot = Tot + TotalAsignado(j, i)
                        CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(TotalAsignado(j, i))
                    Next
                    CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
                    LW.Items.Add(CadenaMensaje)
                Next
                CadenaMensaje = "TOTAL:"
                For j = 0 To 5
                    Tot = 0
                    For i = 0 To oNecesidades.Count - 1
                        Tot = Tot + TotalAsignado(j, i)
                    Next
                    CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
                Next
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
                CadenaMensaje = "Resto útil especial: " + CStr(VolcadoTotal(40))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil primera: " + CStr(VolcadoTotal(41))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil segunda: " + CStr(VolcadoTotal(42))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil tercera: " + CStr(VolcadoTotal(43))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil cuarta: " + CStr(VolcadoTotal(44))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil quinta: " + CStr(VolcadoTotal(45))
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
                LW.Items.Add("")
                LW.Items.Add("")
                LW.Refresh()
            End If
        Loop

        For i = 0 To 10 : LW.Items.Add("") : Next
        LW.Items.Add("AHORA CON LAS CALIDADES O")
        For i = 0 To 2 : LW.Items.Add("") : Next

        PB2.Visible = False
        TotalPorAsignar = 0
        For i = 0 To 9 : TotalPorAsignar = TotalPorAsignar + VolcadoTotal(40 + i) : Next
        For i = 0 To oNecesidades.Count - 1
            GrupoActivo(i) = True
        Next
        GruposActivos = oNecesidades.Count
        If TotalPorAsignar > 0 Then
            PB22.Minimum = 0
            PB22.Value = 0
            PB22.Maximum = TotalPorAsignar
            PB22.Visible = True
            Me.Refresh()
        End If
        Do While TotalPorAsignar > 0 And GruposActivos > 0
            MaximoValorAAsignar = -99999999.0#
            If TotalPorAsignar < PB22.Maximum Then PB22.Value = PB22.Maximum - TotalPorAsignar
            GrupoAAsignar = -1
            For i = 0 To oNecesidades.Count - 1
                If GrupoActivo(i) = True Then
                    Clave = oNecesidadesOrdenado(i)
                    Dato = oNecesidades.Item(Clave)
                    Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                    Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                    If Numeros(1) > 0 Then
                        Numeros(2) = 0
                        For j = 0 To 5
                            If Mid(Clave, j + 1, 1) = "O" Then
                                Numeros(2) = Numeros(2) + VolcadoTotal(40 + j)
                            End If
                        Next
                        If Numeros(2) = 0 Then
                            ValorMaximo(i) = 0
                        Else
                            ValorMaximo(i) = Numeros(1) / Numeros(2)
                        End If
                        If GrupoAAsignar = -1 Or MaximoValorAAsignar < ValorMaximo(i) Then
                            GrupoAAsignar = i
                            MaximoValorAAsignar = ValorMaximo(i)
                            ClaveAAsignar = Clave
                            DatoAAsignar = Dato
                        End If
                    End If
                End If
            Next
            If GrupoAAsignar > -1 Then
                'Hay que asignar un kilo al grupo que indica GrupoAAsignar (el más necesitado, el que tiene mayor ratio de necesidad).
                'Es necesario elegir la calidad a asignar a ese grupo
                'Se debe elegir la calidad entre las aceptables para el grupo que minimice el máximo de los ratios de necesidad 1/2
                CalidadSeleccionada = -1
                MaximoSeleccionado = 99999999.0#
                For k = 0 To 5
                    If Mid(ClaveAAsignar, k + 1, 1) = "O" And VolcadoTotal(40 + k) > 0 Then
                        MaximoValor = -99999999.0#
                        Grupo = -1
                        For i = 0 To oNecesidades.Count - 1
                            If i <> GrupoAAsignar Then
                                Clave = oNecesidadesOrdenado(i)
                                Dato = oNecesidades.Item(Clave)
                                Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                                Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                                '                    If i = GrupoAAsignar Then Numeros(1) = Numeros(1) - 1
                                Numeros(2) = 0
                                For j = 0 To 5
                                    If Mid(Clave, j + 1, 1) = "O" Then
                                        Numeros(2) = Numeros(2) + VolcadoTotal(40 + j)
                                        If j = k Then Numeros(2) = Numeros(2) - 1
                                    End If
                                Next
                                If Numeros(2) = 0 Then
                                    ValorMaximo(i) = 0
                                Else
                                    ValorMaximo(i) = Numeros(1) / Numeros(2)
                                End If
                                If Grupo = -1 Or MaximoValor < ValorMaximo(i) Then
                                    Grupo = i
                                    MaximoValor = ValorMaximo(i)
                                    ClaveNueva = Clave
                                    DatoNuevo = Dato
                                End If
                            End If
                        Next
                        If MaximoValor < MaximoSeleccionado Then
                            CalidadSeleccionada = k
                            MaximoSeleccionado = MaximoValor
                        End If
                    End If
                Next
                If CalidadSeleccionada > -1 Then
                    'Vamos a descontar 1 kg. de la calidad 'CalidadSeleccionada'
                    VolcadoTotal(40 + CalidadSeleccionada) = VolcadoTotal(40 + CalidadSeleccionada) - 1
                    Clave = oNecesidadesOrdenado(GrupoAAsignar)
                    Dato = oNecesidades.Item(Clave)
                    Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                    Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                    Numeros(1) = Numeros(1) - 1
                    Dato = ""
                    For j = 0 To 1
                        Dato = Dato & Format(Numeros(j), "0000000000")
                    Next j
                    oNecesidades.Item(Clave) = Dato
                    TotalAsignado(CalidadSeleccionada, GrupoAAsignar) = TotalAsignado(CalidadSeleccionada, GrupoAAsignar) + 1
                    TotalPorAsignar = 0
                    For i = 0 To 10 : TotalPorAsignar = TotalPorAsignar + VolcadoTotal(40 + i) : Next
                Else
                    CadenaMensaje = "No puedo asignar ninguna calidad al grupo:" + CStr(GrupoAAsignar)
                    LW.Items.Add(CadenaMensaje)
                    GrupoActivo(GrupoAAsignar) = False
                    GruposActivos = GruposActivos - 1
                End If
            Else
                TotalPorAsignar = 0
            End If
            If TotalPorAsignar Mod 100 = 0 Then
                For i = 0 To oNecesidades.Count - 1
                    CadenaMensaje = "Grupo " + Trim(oNecesidadesOrdenado(i)) + ":"
                    Tot = 0
                    For j = 0 To 5
                        Tot = Tot + TotalAsignado(j, i)
                        CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(TotalAsignado(j, i))
                    Next
                    CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
                    LW.Items.Add(CadenaMensaje)
                Next
                CadenaMensaje = "TOTAL:"
                For j = 0 To 5
                    Tot = 0
                    For i = 0 To oNecesidades.Count - 1
                        Tot = Tot + TotalAsignado(j, i)
                    Next
                    CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
                Next
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
                CadenaMensaje = "Resto útil especial: " + CStr(VolcadoTotal(40))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil primera: " + CStr(VolcadoTotal(41))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil segunda: " + CStr(VolcadoTotal(42))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil tercera: " + CStr(VolcadoTotal(43))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil cuarta: " + CStr(VolcadoTotal(44))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil quinta: " + CStr(VolcadoTotal(45))
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
                LW.Items.Add("")
                LW.Items.Add("")
                LW.Refresh()
            End If
        Loop

        For i = 0 To 10 : LW.Items.Add("") : Next
        LW.Items.Add("AHORA CON LAS CALIDADES X")
        For i = 0 To 2 : LW.Items.Add("") : Next

        PB22.Visible = False
        TotalPorAsignar = 0
        For i = 0 To 9 : TotalPorAsignar = TotalPorAsignar + VolcadoTotal(40 + i) : Next
        For i = 0 To oNecesidades.Count - 1
            GrupoActivo(i) = True
        Next
        If TotalPorAsignar > 0 Then
            GruposActivos = oNecesidades.Count
            PB23.Minimum = 0
            PB23.Value = 0
            PB23.Maximum = TotalPorAsignar
            PB23.Visible = True
            Me.Refresh()
        End If
        Do While TotalPorAsignar > 0 And GruposActivos > 0
            MaximoValorAAsignar = -99999999.0#
            If TotalPorAsignar < PB23.Maximum Then PB23.Value = PB23.Maximum - TotalPorAsignar
            GrupoAAsignar = -1
            For i = 0 To oNecesidades.Count - 1
                If GrupoActivo(i) = True Then
                    Clave = oNecesidadesOrdenado(i)
                    Dato = oNecesidades.Item(Clave)
                    Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                    Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                    If Numeros(1) > 0 Then
                        Numeros(2) = 0
                        For j = 0 To 5
                            If Mid(Clave, j + 1, 1) = "X" Then
                                Numeros(2) = Numeros(2) + VolcadoTotal(40 + j)
                            End If
                        Next
                        If Numeros(2) = 0 Then
                            ValorMaximo(i) = 0
                        Else
                            ValorMaximo(i) = Numeros(1) / Numeros(2)
                        End If
                        If GrupoAAsignar = -1 Or MaximoValorAAsignar < ValorMaximo(i) Then
                            GrupoAAsignar = i
                            MaximoValorAAsignar = ValorMaximo(i)
                            ClaveAAsignar = Clave
                            DatoAAsignar = Dato
                        End If
                    End If
                End If
            Next
            If GrupoAAsignar > -1 Then
                'Hay que asignar un kilo al grupo que indica GrupoAAsignar (el más necesitado, el que tiene mayor ratio de necesidad).
                'Es necesario elegir la calidad a asignar a ese grupo
                'Se debe elegir la calidad entre las aceptables para el grupo que minimice el máximo de los ratios de necesidad 1/2
                CalidadSeleccionada = -1
                MaximoSeleccionado = 99999999.0#
                For k = 0 To 5
                    If Mid(ClaveAAsignar, k + 1, 1) = "X" And VolcadoTotal(40 + k) > 0 Then
                        MaximoValor = -99999999.0#
                        Grupo = -1
                        For i = 0 To oNecesidades.Count - 1
                            If i <> GrupoAAsignar Then
                                Clave = oNecesidadesOrdenado(i)
                                Dato = oNecesidades.Item(Clave)
                                Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                                Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                                '                    If i = GrupoAAsignar Then Numeros(1) = Numeros(1) - 1
                                Numeros(2) = 0
                                For j = 0 To 5
                                    If Mid(Clave, j + 1, 1) = "X" Then
                                        Numeros(2) = Numeros(2) + VolcadoTotal(40 + j)
                                        If j = k Then Numeros(2) = Numeros(2) - 1
                                    End If
                                Next
                                If Numeros(2) = 0 Then
                                    ValorMaximo(i) = 0
                                Else
                                    ValorMaximo(i) = Numeros(1) / Numeros(2)
                                End If
                                If Grupo = -1 Or MaximoValor < ValorMaximo(i) Then
                                    Grupo = i
                                    MaximoValor = ValorMaximo(i)
                                    ClaveNueva = Clave
                                    DatoNuevo = Dato
                                End If
                            End If
                        Next
                        If MaximoValor < MaximoSeleccionado Then
                            CalidadSeleccionada = k
                            MaximoSeleccionado = MaximoValor
                        End If
                    End If
                Next
                If CalidadSeleccionada > -1 Then
                    'Vamos a descontar 1 kg. de la calidad 'CalidadSeleccionada'
                    VolcadoTotal(40 + CalidadSeleccionada) = VolcadoTotal(40 + CalidadSeleccionada) - 1
                    Clave = oNecesidadesOrdenado(GrupoAAsignar)
                    Dato = oNecesidades.Item(Clave)
                    Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                    Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                    Numeros(1) = Numeros(1) - 1
                    Dato = ""
                    For j = 0 To 1
                        Dato = Dato & Format(Numeros(j), "0000000000")
                    Next j
                    oNecesidades.Item(Clave) = Dato
                    TotalAsignado(CalidadSeleccionada, GrupoAAsignar) = TotalAsignado(CalidadSeleccionada, GrupoAAsignar) + 1
                    TotalPorAsignar = 0
                    For i = 0 To 10 : TotalPorAsignar = TotalPorAsignar + VolcadoTotal(40 + i) : Next
                Else
                    CadenaMensaje = "No puedo asignar ninguna calidad al grupo:" + CStr(GrupoAAsignar)
                    LW.Items.Add(CadenaMensaje)
                    GrupoActivo(GrupoAAsignar) = False
                    GruposActivos = GruposActivos - 1
                End If
            Else
                TotalPorAsignar = 0
            End If
            If TotalPorAsignar Mod 100 = 0 Then
                For i = 0 To oNecesidades.Count - 1
                    CadenaMensaje = "Grupo " + Trim(oNecesidadesOrdenado(i)) + ":"
                    Tot = 0
                    For j = 0 To 5
                        Tot = Tot + TotalAsignado(j, i)
                        CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(TotalAsignado(j, i))
                    Next
                    CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
                    LW.Items.Add(CadenaMensaje)
                Next
                CadenaMensaje = "TOTAL:"
                For j = 0 To 5
                    Tot = 0
                    For i = 0 To oNecesidades.Count - 1
                        Tot = Tot + TotalAsignado(j, i)
                    Next
                    CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
                Next
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
                CadenaMensaje = "Resto útil especial: " + CStr(VolcadoTotal(40))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil primera: " + CStr(VolcadoTotal(41))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil segunda: " + CStr(VolcadoTotal(42))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil tercera: " + CStr(VolcadoTotal(43))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil cuarta: " + CStr(VolcadoTotal(44))
                LW.Items.Add(CadenaMensaje)
                CadenaMensaje = "Resto útil quinta: " + CStr(VolcadoTotal(45))
                LW.Items.Add(CadenaMensaje)
                LW.Items.Add("")
                LW.Items.Add("")
                LW.Items.Add("")
                LW.Refresh()
            End If
        Loop

        '        Lazo movimientos de asignación para mejora del cuadre
        Pic2.Visible = False
        PB23.Visible = False
        Pic3.Visible = True
        Lbl3.Text = "Análisis por calidades"
        Me.Refresh()

        For i = 0 To oNecesidades.Count - 1
            Clave = oNecesidadesOrdenado(i)
            Dato = oNecesidades.Item(Clave)
            Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
            Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
            If Numeros(1) <= 0 Then
                GrupoActivo(i) = False
            Else
                GrupoActivo(i) = True
            End If
        Next
        FlagPosibleMover = True
        Do While FlagPosibleMover = True
            FlagPosibleMover = False
            'Movimiento directo (utilización de un kg. no asignado)
            For j = 0 To 5
                If VolcadoTotal(40 + j) > 0 Then
                    For i = 0 To oNecesidades.Count - 1
                        If GrupoActivo(i) = True Then
                            Clave = oNecesidadesOrdenado(i)
                            If Mid(Clave, j + 1, 1) <> "N" Then
                                Dato = oNecesidades.Item(Clave)
                                Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
                                Numeros(1) = Math.Round(CDbl(Mid(Dato, 11, 10)), 0)
                                Numeros(1) = Numeros(1) - 1
                                Dato = Format(Numeros(0), "0000000000") + Format(Numeros(1), "0000000000")
                                oNecesidades.Item(Clave) = Dato
                                VolcadoTotal(40 + j) = VolcadoTotal(40 + j) - 1
                                TotalAsignado(j, i) = TotalAsignado(j, i) + 1
                                If Numeros(1) <= 0 Then GrupoActivo(i) = False
                                FlagPosibleMover = True
                            End If
                        End If
                    Next
                End If
            Next
            'Movimiento indirecto (primero un cambio de la misma calidad entre grupos y luego utilización un kg. no asignado en otra calidad del grupo que ha quedado libre)
            If FlagPosibleMover = False Then 'primero los movimientos directos
                For j = 0 To 5
                    If VolcadoTotal(40 + j) > 0 Then 'Una calidad j con existencias disponibles.
                        For i = 0 To oNecesidades.Count - 1
                            If GrupoActivo(i) = False Then 'Un grupo i completo ...
                                Clave = oNecesidadesOrdenado(i)
                                If Mid(Clave, j + 1, 1) <> "N" Then 'que aceptaría la calidad j (aunque ahora esta completo).
                                    For k = 0 To oNecesidades.Count - 1
                                        If GrupoActivo(k) = True Then 'Un segundo grupo k incompleto ...
                                            ClaveAAsignar = oNecesidadesOrdenado(k)
                                            DatoAAsignar = oNecesidades.Item(ClaveAAsignar)
                                            For l = 0 To 5
                                                If Mid(ClaveAAsignar, l + 1, 1) <> "N" And TotalAsignado(l, i) > 0 Then 'que comparte con el grupo i una calidad en la que i tiene existencias.
                                                    'Le quitamos 1kg al grupo i en la calidad l y se la pasamos al grupo k
                                                    'y después le añadimos 1kg al grupo i en la calidad j (de la cual hay existencias).
                                                    'Finalmente rompemos todos los bucles menos el externo
                                                    Lbl3.Text = "Muevo calidad " & l & " del grupo " & i & "al " & k & ". Asigno calidad " & j
                                                    Lbl3.Refresh()
                                                    CadenaMensaje = Lbl3.Text
                                                    LW.Items.Add(CadenaMensaje)

                                                    TotalAsignado(l, i) = TotalAsignado(l, i) - 1
                                                    TotalAsignado(l, k) = TotalAsignado(l, k) + 1
                                                    Numeros(0) = Math.Round(CDbl(Mid(DatoAAsignar, 1, 10)), 0)
                                                    Numeros(1) = Math.Round(CDbl(Mid(DatoAAsignar, 11, 10)), 0)
                                                    Numeros(1) = Numeros(1) - 1
                                                    If Numeros(1) <= 0 Then GrupoActivo(k) = False
                                                    DatoAAsignar = Format(Numeros(0), "0000000000") + Format(Numeros(1), "0000000000")
                                                    oNecesidades.Item(ClaveAAsignar) = DatoAAsignar

                                                    VolcadoTotal(40 + j) = VolcadoTotal(40 + j) - 1
                                                    TotalAsignado(j, i) = TotalAsignado(j, i) + 1

                                                    FlagPosibleMover = True
                                                    k = 1000
                                                    i = 1000
                                                    j = 1000
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Loop
        FlagSalir = False
        For i = 0 To oNecesidades.Count - 1
            Clave = oNecesidadesOrdenado(i)
            Dato = oNecesidades.Item(Clave)
            Numeros(0) = Math.Round(CDbl(Mid(Dato, 1, 10)), 0)
            CadenaMensaje = "Grupo " + Trim(Clave) + ":"
            Tot = 0
            For j = 0 To 5
                Tot = Tot + TotalAsignado(j, i)
                CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(TotalAsignado(j, i))
            Next
            CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot) + " de " + CStr(Numeros(0))
            LW.Items.Add(CadenaMensaje)
            If Tot < Numeros(0) Then
                MsgBox("No se puede cuadrar la calidad " + Trim(Clave) + vbCrLf + CadenaMensaje)
                FlagSalir = True
            End If
        Next
        CadenaMensaje = "TOTAL:"
        For j = 0 To 5
            Tot = 0
            For i = 0 To oNecesidades.Count - 1
                Tot = Tot + TotalAsignado(j, i)
            Next
            CadenaMensaje = Trim(CadenaMensaje) + vbTab + CStr(Tot)
        Next
        LW.Items.Add(CadenaMensaje)
        LW.Items.Add("")
        CadenaMensaje = "Resto útil especial: " + CStr(VolcadoTotal(40))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil primera: " + CStr(VolcadoTotal(41))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil segunda: " + CStr(VolcadoTotal(42))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil tercera: " + CStr(VolcadoTotal(43))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil cuarta: " + CStr(VolcadoTotal(44))
        LW.Items.Add(CadenaMensaje)
        CadenaMensaje = "Resto útil quinta: " + CStr(VolcadoTotal(45))
        LW.Items.Add(CadenaMensaje)
        If FlagSalir = True Then GoTo grabar

        '        Lazo Reparto por calidades
        Pic3.Visible = False
        Lbl3.Visible = False
        PB4.Minimum = 0
        PB4.Value = 0
        PB4.Maximum = 2 * oConfeccionado.Count
        PB4.Visible = True
        Pic4.Visible = True
        Me.Refresh()

        For i = 0 To 5
            Utilizado(i) = 0
            Utilizado(10 + i) = 0
        Next
        Utilizado(30) = 0
        Utilizado(31) = 0
        For i = 0 To 5
            For j = 0 To oNecesidades.Count
                TotalUtilizado(i, j) = 0
            Next
        Next
        For i = 0 To oConfeccionado.Count - 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            PB4.Value = PB4.Value + 1
            ClaveCalidades = Mid(ClaveConfeccionado, 34)
            ClaveIndustria = Mid(ClaveConfeccionado, 19, 1)

            CadenaMensaje = ClaveConfeccionado & " PESO:" & Confeccionado(60, PunteroConfeccionado)
            LW.Items.Add(CadenaMensaje)

            For j = 0 To 5
                Confeccionado(10 + j, PunteroConfeccionado) = 0
                RatioReparto(j) = 0
                KgReparto(j) = 0
                Confeccionado(40 + j, PunteroConfeccionado) = Utilizado(j)
            Next
            PunteroGrupo = DevuelvePunteroGrupo(ClaveConfeccionado)
            If Trim(ClaveIndustria) = "I" Then
                For j = 0 To 5
                    KgReparto(j) = Confeccionado(j, PunteroConfeccionado)
                Next
            Else
                Dato = oNecesidades.Item(ClaveCalidades)
                Numeros(0) = CLng(Mid(Dato, 1, 10))
                If Numeros(0) > 0 Then
                    For j = 0 To 5
                        RatioReparto(j) = TotalAsignado(j, PunteroGrupo) / Numeros(0)
                        KgReparto(j) = Math.Round(RatioReparto(j) * Confeccionado(60, PunteroConfeccionado), 0)
                    Next
                End If
            End If

            CadenaMensaje = "Disponb:"
            For j = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j), "#,0")
            Next
            LW.Items.Add(CadenaMensaje)
            CadenaMensaje = "Reparto:"
            For j = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(KgReparto(j), "#,0")
            Next
            LW.Items.Add(CadenaMensaje)

            FlagFalta = False
            For j = 0 To 5 : KgReparto2(j) = KgReparto(j) : Next
            For j = 0 To 5
                If KgReparto(j) > 0 Then
                    If Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j) >= KgReparto(j) Then
                        Confeccionado(10 + j, PunteroConfeccionado) = KgReparto(j)
                        Utilizado(j) = Utilizado(j) + KgReparto(j)
                        TotalUtilizado(j, PunteroGrupo) = TotalUtilizado(j, PunteroGrupo) + KgReparto(j)
                    Else
                        FlagFalta = True
                        KgReparto2(j) = Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j)
                        Confeccionado(10 + j, PunteroConfeccionado) = KgReparto2(j)
                        Utilizado(j) = Utilizado(j) + KgReparto2(j)
                        TotalUtilizado(j, PunteroGrupo) = TotalUtilizado(j, PunteroGrupo) + KgReparto2(j)
                    End If
                End If
            Next

            CadenaMensaje = "ASIGNO0:"
            For j = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(KgReparto2(j), "#,0")
            Next
            LW.Items.Add(CadenaMensaje)
            CadenaMensaje = "Disponb:"
            For j = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j), "#,0")
            Next
            LW.Items.Add("")

            If FlagFalta = True Then
                If Trim(ClaveIndustria) = "I" Then
                    For j = 0 To 5
                        If KgReparto2(j) < KgReparto(j) Then 'Falta de la calidad j
                            PesoPendiente = KgReparto(j) - KgReparto2(j)
                            'Intento solucionarlo con una calidad inferior y, si no lo consigo, superior
                            For k = 5 To 1 Step -1
                                If k <> j Then
                                    If Confeccionado(30 + k, PunteroConfeccionado) - Utilizado(k) > 0 Then 'Algo utilizable de la calidad k
                                        If Confeccionado(30 + k, PunteroConfeccionado) - Utilizado(k) >= PesoPendiente Then 'Suficiente de calidad k para completar lo que falta de j
                                            PesoUtilizado = PesoPendiente
                                        Else 'Cubro la necesidad en parte
                                            PesoUtilizado = Confeccionado(30 + k, PunteroConfeccionado) - Utilizado(k)
                                        End If
                                        KgReparto2(k) = KgReparto2(k) + PesoUtilizado
                                        KgReparto(k) = KgReparto2(k)
                                        KgReparto(j) = KgReparto(j) - PesoUtilizado
                                        Confeccionado(10 + k, PunteroConfeccionado) = Confeccionado(10 + k, PunteroConfeccionado) + PesoUtilizado
                                        Utilizado(k) = Utilizado(k) + PesoUtilizado
                                        TotalUtilizado(k, PunteroGrupo) = TotalUtilizado(k, PunteroGrupo) + PesoUtilizado
                                        PesoPendiente = PesoPendiente - PesoUtilizado
                                    End If
                                End If
                            Next
                        End If
                    Next
                    FlagFalta = False
                    For j = 0 To 5
                        If KgReparto(j) <> KgReparto2(j) Then FlagFalta = True
                    Next

                    CadenaMensaje = "ASIGNO1:"
                    For j = 0 To 5
                        CadenaMensaje = CadenaMensaje + vbTab + Format(KgReparto2(j), "#,0")
                    Next
                    LW.Items.Add(CadenaMensaje)
                    CadenaMensaje = "Disponb:"
                    For j = 0 To 5
                        CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j), "#,0")
                    Next
                    LW.Items.Add("")
                Else
                    For j = 0 To 5
                        If KgReparto2(j) < KgReparto(j) Then 'Falta de la calidad j
                            PesoPendiente = KgReparto(j) - KgReparto2(j)
                            'Intento solucionarlo con una aceptable
                            FlagEsPosible = True
                            Do While FlagEsPosible = True And PesoPendiente > 0
                                FlagEsPosible = False
                                For k = 5 To 1 Step -1
                                    If PesoPendiente > 0 And k <> j And Mid(ClaveCalidades, k + 1, 1) <> "N" And Confeccionado(30 + k, PunteroConfeccionado) - Utilizado(k) > 0 Then
                                        KgReparto2(k) = KgReparto2(k) + 1
                                        KgReparto(k) = KgReparto2(k)
                                        KgReparto(j) = KgReparto(j) - 1
                                        Confeccionado(10 + k, PunteroConfeccionado) = Confeccionado(10 + k, PunteroConfeccionado) + 1
                                        Utilizado(k) = Utilizado(k) + 1
                                        TotalUtilizado(k, PunteroGrupo) = TotalUtilizado(k, PunteroGrupo) + 1
                                        PesoPendiente = PesoPendiente - 1
                                        FlagEsPosible = True
                                    End If
                                Next
                                '                        Debug.Print KgReparto(0), KgReparto(1), KgReparto(2), KgReparto(3)
                            Loop
                        End If
                    Next
                    CadenaMensaje = "ASIGNO2:"
                    For j = 0 To 5
                        CadenaMensaje = CadenaMensaje + vbTab + Format(KgReparto2(j), "#,0")
                    Next
                    LW.Items.Add(CadenaMensaje)
                    CadenaMensaje = "Disponb:"
                    For j = 0 To 5
                        CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j), "#,0")
                    Next
                    LW.Items.Add("")
                End If

                FlagFalta = False
                For j = 0 To 5
                    If KgReparto(j) <> KgReparto2(j) Then FlagFalta = True
                Next
                If FlagFalta = True Then
                    CadenaMensaje = "FALTA PESO"
                    LW.Items.Add(CadenaMensaje)
                    For j = 0 To 5
                        If KgReparto(j) <> KgReparto2(j) Then
                            PesoUtilizado = 0
                            PesoPendiente = KgReparto(j) - KgReparto2(j)
                            If LiberarKilosDirectamente(j, i, PunteroConfeccionado, PesoPendiente, PesoUtilizado) = True Then
                                KgReparto2(j) = KgReparto2(j) + PesoUtilizado
                                CadenaMensaje = "Asignados (liberados) " + CStr(PesoUtilizado) + " kilos de calidad " + CStr(j)
                                LW.Items.Add(CadenaMensaje)
                            End If
                        End If
                    Next
                    CadenaMensaje = "ASIGNO3:"
                    For j = 0 To 5
                        CadenaMensaje = CadenaMensaje + vbTab + Format(KgReparto2(j), "#,0")
                    Next
                    LW.Items.Add(CadenaMensaje)
                    CadenaMensaje = "Disponb:"
                    For j = 0 To 5
                        CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j), "#,0")
                    Next
                    LW.Items.Add("")

                    FlagFalta = False
                    Tot = 0
                    For j = 0 To 5
                        If KgReparto(j) <> KgReparto2(j) Then FlagFalta = True
                        Tot = Tot + KgReparto2(j)
                    Next
                    If FlagFalta = True Then
                        CadenaMensaje = "SIGUE FALTANDO PESO"
                        LW.Items.Add(CadenaMensaje)

                        PesoPendiente = Confeccionado(60, PunteroConfeccionado) - Tot
                        For j = 0 To 5
                            If Mid(ClaveCalidades, j + 1, 1) <> "N" And PesoPendiente > 0 Then
                                PesoUtilizado = 0
                                If LiberarKilosDirectamente(j, i, PunteroConfeccionado, PesoPendiente, PesoUtilizado) = True Then
                                    KgReparto2(j) = KgReparto2(j) + PesoUtilizado
                                    PesoPendiente = PesoPendiente - PesoUtilizado
                                    CadenaMensaje = "Asignados (liberados2) " + CStr(PesoUtilizado) + " kilos de calidad " + CStr(j)
                                    LW.Items.Add(CadenaMensaje)
                                End If
                            End If
                        Next
                        CadenaMensaje = "ASIGNO4:"
                        For j = 0 To 5
                            CadenaMensaje = CadenaMensaje + vbTab + Format(KgReparto2(j), "#,0")
                        Next
                        LW.Items.Add(CadenaMensaje)
                        CadenaMensaje = "Disponb:"
                        For j = 0 To 5
                            CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(30 + j, PunteroConfeccionado) - Utilizado(j), "#,0")
                        Next
                        LW.Items.Add("")

                        Tot = 0
                        For j = 0 To 5 : Tot = Tot + KgReparto2(j) : Next
                        If Tot <> Confeccionado(60, PunteroConfeccionado) Then
                            CadenaMensaje = "SIGUEN FALTANDO " + CStr(Confeccionado(60, PunteroConfeccionado) - Tot) + " kilos."
                            LW.Items.Add(CadenaMensaje)
                        End If
                    End If
                End If
            End If
        Next

        Pic4.Visible = False
        PB4.Visible = False
        PB5.Minimum = 0
        PB5.Value = 0
        PB5.Maximum = 3 * oConfeccionado.Count
        PB5.Visible = True
        Pic5.Visible = True
        Me.Refresh()

        For i = 0 To oConfeccionado.Count - 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            For j = 0 To 5
                Confeccionado(50 + j, PunteroConfeccionado) = 0
            Next
        Next
        For i = 0 To oConfeccionado.Count - 1
            PB5.Value = PB5.Value + 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            For j = 0 To 5
                Confeccionado(50 + j, PunteroConfeccionado) = Confeccionado(30 + j, PunteroConfeccionado) - Confeccionado(40 + j, PunteroConfeccionado) - Confeccionado(10 + j, PunteroConfeccionado)
                For k = 0 To i - 1
                    ClaveConfeccionado2 = Trim(oConfeccionadoOrdenado(k))
                    CadenaConfeccionado2 = Trim(oConfeccionado.Item(ClaveConfeccionado2))
                    PunteroConfeccionado2 = CLng(CadenaConfeccionado2)
                    If Confeccionado(50 + j, PunteroConfeccionado2) > Confeccionado(50 + j, PunteroConfeccionado) Then
                        Confeccionado(50 + j, PunteroConfeccionado2) = Confeccionado(50 + j, PunteroConfeccionado)
                    End If
                Next
            Next
        Next
        For i = 0 To oConfeccionado.Count - 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            CadenaMensaje = "Merm:" + Format(i, "000")
            For j = 0 To 5
                CadenaMensaje = CadenaMensaje + vbTab + Format(Confeccionado(50 + j, PunteroConfeccionado), "#,0")
            Next
            LW.Items.Add(CadenaMensaje)
        Next

        InformeFinal()
        oFinal.Clear()
        For k = 0 To oVolcado.Count - 1
            For j = 0 To 5
                Volcado(20 + j, k) = Volcado(10 + j, k)
            Next
        Next

        For i = 0 To oConfeccionado.Count - 1
            PB5.Value = PB5.Value + 1
            ClaveConfeccionado = Trim(oConfeccionadoOrdenado(i))
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            HoraConfeccion = Mid(ClaveConfeccionado, 7, 2) & ":" & Mid(ClaveConfeccionado, 9, 2) & ":" & Mid(ClaveConfeccionado, 11, 2)

            If Mid(ClaveConfeccionado, 19, 1) = "C" Then
                Cadena = "SELECT * FROM PALETS_TRAZAB WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_PALET = '" + Trim(Confeccionado2(0, PunteroConfeccionado)) + "'"
                BorrarDatos(Cadena)
            ElseIf Mid(ClaveConfeccionado, 19, 1) = "P" Then
                Cadena = "SELECT * FROM PALETS_PRECALIBRE_T WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' and ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_PALET = '" + Trim(Confeccionado2(0, PunteroConfeccionado)) + "'"
                BorrarDatos(Cadena)
            End If

            For j = 0 To 5
                If Confeccionado(10 + j, PunteroConfeccionado) > 0 Then
                    PesoPendiente = Confeccionado(10 + j, PunteroConfeccionado)
                    For k = 0 To oVolcado.Count - 1
                        ClaveVolcado = oVolcadoOrdenado(k)
                        CadenaVolcado = Trim(oVolcado.Item(ClaveVolcado))
                        PunteroVolcado = CInt(CadenaVolcado)
                        HoraVolcado = Mid(ClaveVolcado, 7, 2) & ":" & Mid(ClaveVolcado, 9, 2) & ":" & Mid(ClaveVolcado, 11, 2)
                        If Mid(ClaveVolcado, 1, 6) >= Mid(ClaveConfeccionado, 1, 6) And DiferenciaSegundos(HoraVolcado, HoraConfeccion) <= RetardoLinea Then
                            If FlagTodos = False Then
                                MsgBox("Problema con las horas")
                            Else
                                Fichero.EscribeLinea("Problema con las horas")
                            End If
                        End If
                        If Volcado(20 + j, PunteroVolcado) > 0 Then
                            If Volcado(20 + j, PunteroVolcado) >= PesoPendiente Then
                                AsignarVolcado(ClaveConfeccionado, ClaveVolcado, j, PesoPendiente, Confeccionado2(0, PunteroConfeccionado), CLng(Volcado2(0, PunteroVolcado)), Volcado2(1, PunteroVolcado), ReferenciaProceso)
                                Volcado(20 + j, PunteroVolcado) = Volcado(20 + j, PunteroVolcado) - PesoPendiente
                                PesoPendiente = 0
                            Else
                                AsignarVolcado(ClaveConfeccionado, ClaveVolcado, j, Volcado(20 + j, PunteroVolcado), Confeccionado2(0, PunteroConfeccionado), CLng(Volcado2(0, PunteroVolcado)), Volcado2(1, PunteroVolcado), ReferenciaProceso)

                                PesoPendiente = PesoPendiente - Volcado(20 + j, PunteroVolcado)
                                Volcado(20 + j, PunteroVolcado) = 0
                            End If
                        End If
                        If PesoPendiente <= 0 Then Exit For
                    Next
                    If PesoPendiente > 0 Then
                        If FlagTodos = False Then
                            MsgBox("Problema con la asignacion")
                        Else
                            Fichero.EscribeLinea("Problema con la asignacion")
                        End If
                    End If
                End If
            Next
        Next
        AsignarCeros(ReferenciaProceso)

        'Ordenar oFinal en oFonalOrdenado
        ReDim oFinalOrdenado(oFinal.Count - 1)
        For i = 0 To oFinal.Count - 1
            oFinalOrdenado(i) = Trim(oFinal.Keys(i))
        Next
        Array.Sort(oFinalOrdenado)



        '        'Edición informe
        Pic5.Visible = False
        PB5.Visible = False
        PB6.Minimum = 0
        PB6.Value = 0
        PB6.Maximum = oFinal.Count
        PB6.Visible = True
        Pic6.Visible = True
        Me.Refresh()

        '==== BLOQUE EXCEL ====

        MiExcel = New LibreriaGeneral.EXCEL_VB(False)
        MiExcel.AbrirHojaExcel(RutaPlantillaCierre)
        MiExcel.Visible = False
        'MiHojaExcel = MiExcel.ObjetoHoja() ' En principio, no hace falta manipular directamente el objeto
        MiExcel.Path = RutaExcel

        MiExcel.EscribeCelda(3, 3, "'" & Format(FechaProceso1, "dd/MM/yyyy"))
        Fila = 6

        Cadena = "SELECT * FROM entradas_trazab WHERE codigo_proceso = '" & Trim(CnTabla01.Rs("codigo_proceso")) + "'"
        BorrarDatos(Cadena)

        SQL = "SELECT * FROM entradas_trazab WHERE 1=0"
        If RsEnt_tazab.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla entradas_trazab")
            Exit Sub
        End If

        Clave = ""
        Codigo_palet = ""
        TipoDeEnvase = ""
        For i = 0 To oFinal.Count - 1
            PB6.Value = PB6.Value + 1
            '            ClaveFinal = Trim(oFinal.Keys(i))
            ClaveFinal = Trim(oFinalOrdenado(i))
            CadenaFinal = Trim(oFinal.Item(ClaveFinal))
            For j = 0 To 5 : DatoS(j) = CLng(Mid(CadenaFinal, 10 * j + 1, 10)) : Next
            ClaveConfeccionado = Mid(ClaveFinal, 1, 39)
            CadenaConfeccionado = Trim(oConfeccionado.Item(ClaveConfeccionado))
            PunteroConfeccionado = CLng(CadenaConfeccionado)
            ClaveCalidades = Mid(ClaveConfeccionado, 34)
            ClaveIndustria = Mid(ClaveConfeccionado, 19, 1)
            HoraConfeccion = Mid(ClaveConfeccionado, 7, 2) & ":" & Mid(ClaveConfeccionado, 9, 2) & ":" & Mid(ClaveConfeccionado, 11, 2)
            ClaveVolcado = Mid(ClaveFinal, 40)
            CadenaVolcado = Trim(oVolcado.Item(ClaveVolcado))
            PunteroVolcado = CInt(CadenaVolcado)
            HoraVolcado = Mid(ClaveVolcado, 7, 2) & ":" & Mid(ClaveVolcado, 9, 2) & ":" & Mid(ClaveVolcado, 11, 2)

            'MiExcel.EscribeCelda(5000 + i, 1, "'" & ClaveFinal)
            'MiExcel.EscribeCelda(5000 + i, 2, "'" & CadenaFinal)
            'MiExcel.EscribeCelda(5000 + i, 3, "'" & ClaveConfeccionado)
            'MiExcel.EscribeCelda(5000 + i, 4, "'" & CadenaConfeccionado)

            Fila = Fila + 1
            If ClaveConfeccionado <> Clave Then
                Clave = ClaveConfeccionado
                MiExcel.EscribeCelda(Fila, 1, Trim(Confeccionado2(0, PunteroConfeccionado))) 'Palet
                MiExcel.EscribeCelda(Fila, 2, HoraConfeccion) 'Hora
                Cadena = Confeccionado2(3, PunteroConfeccionado) + "  " + Confeccionado2(4, PunteroConfeccionado) + "  " + Confeccionado2(2, PunteroConfeccionado)
                MiExcel.EscribeCelda(Fila, 3, Cadena)
                MiExcel.EscribeCelda(Fila, 4, Format(Confeccionado(60, PunteroConfeccionado), "#####0")) 'Peso

                If Trim(MiExcel.DevuelveValorCelda(Fila, 3)) = "PRECALIBRADO" Then
                    TipoDeEnvase = "PRECALIBRADO"
                ElseIf Trim(MiExcel.DevuelveValorCelda(Fila, 3)) = "INDUSTRIA" Then
                    TipoDeEnvase = "INDUSTRIA"
                Else
                    TipoDeEnvase = "NORMAL"
                End If
            End If

            If Trim(MiExcel.DevuelveValorCelda(Fila, 1)) <> Trim(Codigo_palet) Then
                If Trim(MiExcel.DevuelveValorCelda(Fila, 1)) = "" And TipoDeEnvase = "INDUSTRIA" Then
                    Codigo_palet = ""
                ElseIf Trim(MiExcel.DevuelveValorCelda(Fila, 1)) <> "" Then
                    Codigo_palet = Trim(MiExcel.DevuelveValorCelda(Fila, 1))
                End If
            End If

            Rellenar_entradas_trazab(Fila, PunteroVolcado, RsEnt_tazab, TipoDeEnvase, DatoS, HoraVolcado, Clave, Codigo_palet)
        Next

        RutaExcel = Trim(CarpetaExcel) + "\" + Trim(CnTabla01.Rs("codigo_proceso")) + ".xls"
        MiExcel.GuardarHoja(RutaExcel)
        MiExcel.Close()

grabar:
        Pic6.Visible = False
        PB6.Visible = False
        PB7.Minimum = 0
        PB7.Value = 0
        If LW.Items.Count < 1 Then
            PB7.Maximum = 2
        Else
            PB7.Maximum = LW.Items.Count
        End If
        PB7.Visible = True
        Pic7.Visible = True
        Me.Refresh()

        LW.Visible = True

        NombreFichero = Trim(RutaMisDocumentos) + "\trazabilidad" & Format(FechaProceso1, "yyMMdd") & ".txt"
        Fichero2.Open(NombreFichero, True)

        LW.SelectedIndex = 0
        For i = 0 To LW.Items.Count - 1
            PB7.Value = PB7.Value + 1
            LW.SelectedIndex = i
            CadenaMensaje = LW.Text
            Fichero2.EscribeLinea(CadenaMensaje)
        Next
        Fichero2.Close()
        Pic7.Visible = False
        PB7.Visible = False
        'OJO EXCEL If Not (miExcel Is Nothing) Then
        'OJO EXCEL If FlagTodos = False Then
        'OJO EXCEL miExcel.Visible = True
        'OJO EXCEL Else
        'OJO EXCEL miExcel.Quit
        'OJO EXCEL End If
        'OJO EXCEL End If
        If FlagTodos = True Then Fichero.EscribeLinea("FIN: " + CnTabla01.Rs("codigo_proceso"))

        SQL = "SELECT * FROM TRAZAB_PERIODOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_PROCESO = '" + Trim(CnTabla01.Rs("codigo_proceso")) + "'"
        If RsFin.Open(SQL, ObjetoGlobal.cn, True) = False Then
            MsgBox("Error al abrir la tabla trazab_periodos")
            Exit Sub
        End If
        RsFin!tipo_proceso = "S"
        RsFin.Update()
        RsFin.Close()

        LW.Visible = False
        MsgBox("Proceso de análisis de trazabilidad finalizado correctamente")
    End Sub

    Private Sub Rellenar_entradas_trazab(ByRef Fila As Long, PunteroVolcado As Long, RsEnt_tazab As cnRecordset.CnRecordset, TipoDeEnvase As String, DatoS() As Long, horavolcado As String, Clave As String, Codigo_palet As String)
        Dim RsEnt_tazabC As New cnRecordset.CnRecordset, Rs As New cnRecordset.CnRecordset, Rs1 As New cnRecordset.CnRecordset
        Dim ContAux As Integer
        'Dim TipoDeEnvase As String
        Dim Kg_albaran As Double
        Dim Kg_totales_reparto As Double
        Dim SQL As String
        Dim j As Integer, Cadena As String


        If (Trim(Volcado2(0, PunteroVolcado)) = "0" Or Trim(Volcado2(0, PunteroVolcado)) = "") And Trim(Volcado2(1, PunteroVolcado)) <> "" Then ' No tiene número de albarán
            ' Buscamos los albaranes, a ver si ya están grabados
            SQL = "SELECT p.*, t.serie_albaran, t.numero_albaran, t.tipo_entrada, t.contador, t.kilos FROM palets_precalibre p JOIN entradas_trazab t ON t.empresa = p.empresa AND "
            SQL = Trim(SQL) + " t.ejercicio = p.ejercicio AND t.palet_precalibre = p.codigo_palet "
            SQL = Trim(SQL) + " WHERE p.empresa = '" & ObjetoGlobal.EmpresaActual & "' AND p.ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND "
            SQL = Trim(SQL) + " p.referencia='" & Trim(Volcado2(1, PunteroVolcado)) & "' ORDER BY t.contador"
            If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla palets_precalibre")
                Exit Sub
            End If

            Kg_totales_reparto = 0
            Do While Not Rs.EOF
                Kg_totales_reparto = Kg_totales_reparto + Rs!Kilos
                Rs.MoveNext()
            Loop
            If Rs.RecordCount > 0 Then
                Rs.MoveFirst()
            Else
                MsgBox("Problemas con la trazabilidad palet Ref. " & Trim(Volcado2(1, PunteroVolcado)) & " posiblemente no este confeccionado en el momento del volcado")
            End If

            Do While Not Rs.EOF
                MiExcel.EscribeCelda(Fila, 6, "" & Rs!numero_albaran) 'Albaran
                MiExcel.EscribeCelda(Fila, 7, Trim(Volcado2(1, PunteroVolcado))) 'Bulto
                MiExcel.EscribeCelda(Fila, 8, horavolcado) 'Hora
                MiExcel.EscribeCelda(Fila, 9, "'" & Trim(Volcado2(2, PunteroVolcado))) 'Fecha entrada
                ' Hay que distribuir los kilos por albaranes
                Kg_albaran = 0
                For j = 0 To 5
                    MiExcel.EscribeCelda(Fila, 10 + j, Format(Math.Round(DatoS(j) / Kg_totales_reparto * Rs!Kilos, 0), "#####0"))
                    Kg_albaran = Kg_albaran + Math.Round(DatoS(j) / Kg_totales_reparto * Rs!Kilos, 0)
                Next

                ' Grabamos la cabecera y línea de la nueva tabla
                SQL = "SELECT * FROM entradas_albaranes WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & Trim(ObjetoGlobal.EjercicioActual) & "'AND "
                SQL = Trim(SQL) + " serie_albaran = '" & "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2) & "' AND numero_albaran =" & MiExcel.DevuelveValorCelda(Fila, 6)
                If Rs1.Open(SQL, ObjetoGlobal.cn) = False Then
                    MsgBox("Error al abrir la tabla palets_precalibre")
                    Exit Sub
                End If
                If Not Rs1.EOF Then
                    SQL = "select max(contador) as cont from Entradas_trazab WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & Trim(ObjetoGlobal.EjercicioActual) & "'AND "
                    SQL = Trim(SQL) + " serie_albaran = '" & "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2) & "' AND numero_albaran =" & CStr(CLng(MiExcel.DevuelveValorCelda(Fila, 6)))
                    If RsEnt_tazabC.Open(SQL, ObjetoGlobal.cn, True) = False Then
                        MsgBox("Error al abrir la tabla entradas_trazab (grabación)")
                        Exit Sub
                    End If
                    If RsEnt_tazabC.RecordCount > 0 Then
                        ContAux = IIf(IsDBNull(RsEnt_tazabC!cont), 1, RsEnt_tazabC!cont + 1)
                    Else
                        ContAux = 1
                    End If
                    RsEnt_tazabC.Close()

                    RsEnt_tazab.AddNew()
                    RsEnt_tazab!empresa = Trim(ObjetoGlobal.EmpresaActual)
                    RsEnt_tazab!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                    RsEnt_tazab!serie_albaran = "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                    RsEnt_tazab!numero_albaran = CLng(MiExcel.DevuelveValorCelda(Fila, 6))
                    RsEnt_tazab!Contador = ContAux
                    RsEnt_tazab!tipo_entrada = Rs1!tipo_entrada
                    RsEnt_tazab!codigo_variedad = Rs1!codigo_variedad
                    RsEnt_tazab!codigo_campo = Rs1!codigo_campo
                    RsEnt_tazab!campo_terceros = Rs1!campo_terceros
                    RsEnt_tazab!kg_a_liquidar = Rs1!kg_a_liquidar

                    If TipoDeEnvase = "NORMAL" Then
                        If Len(Trim(Codigo_palet)) < 9 Then
                            Cadena = StrDup(9 - Len(Trim(Codigo_palet)), "0") + Trim(Codigo_palet)
                        Else
                            Cadena = Trim(Codigo_palet)
                        End If
                        RsEnt_tazab!Codigo_palet = Cadena
                    ElseIf TipoDeEnvase = "PRECALIBRADO" Then
                        RsEnt_tazab!palet_precalibre = Codigo_palet
                    End If
                    RsEnt_tazab!Kilos = Kg_albaran
                    RsEnt_tazab!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                    RsEnt_tazab.Update()
                Else
                    MsgBox("No encuentro el albarán " & MiExcel.DevuelveValorCelda(Fila, 6))
                End If
                ' Siguiente registro
                Rs1.Close()
                Rs.MoveNext()

                If Not Rs.EOF Then
                    Fila = Fila + 1
                End If
            Loop
        Else
            MiExcel.EscribeCelda(Fila, 6, Trim(Volcado2(0, PunteroVolcado))) 'Albaran
            MiExcel.EscribeCelda(Fila, 7, Trim(Volcado2(1, PunteroVolcado))) 'Bulto
            MiExcel.EscribeCelda(Fila, 8, horavolcado) 'Hora
            MiExcel.EscribeCelda(Fila, 9, "'" & Trim(Volcado2(2, PunteroVolcado))) 'Fecha entrada
            Kg_albaran = 0
            For j = 0 To 5
                MiExcel.EscribeCelda(Fila, 10 + j, Format(DatoS(j), "#####0"))
                Kg_albaran = Kg_albaran + DatoS(j)
            Next
            ' Grabamos la cabecera y línea de la nueva tabla
            SQL = "SELECT * FROM entradas_albaranes WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & Trim(ObjetoGlobal.EjercicioActual) & "'AND "
            SQL = Trim(SQL) + " serie_albaran = '" & "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2) & "' AND numero_albaran =" & CStr(CLng(MiExcel.DevuelveValorCelda(Fila, 6)))

            If Rs1.Open(SQL, ObjetoGlobal.cn, True) = False Then
                MsgBox("Error al abrir la tabla entradas_albaranes")
                Exit Sub
            End If
            If Not Rs1.EOF Then
                SQL = "select max(contador) as cont from Entradas_trazab WHERE empresa = '" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & Trim(ObjetoGlobal.EjercicioActual) & "'AND "
                SQL = Trim(SQL) + " serie_albaran = '" & "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2) & "' AND numero_albaran =" & CStr(CLng(MiExcel.DevuelveValorCelda(Fila, 6)))
                If RsEnt_tazabC.Open(SQL, ObjetoGlobal.cn, True) = False Then
                    MsgBox("Error al abrir la tabla entradas_albaranes")
                    Exit Sub
                End If
                ContAux = 1
                If RsEnt_tazabC.RecordCount > 0 Then
                    If Not (IsDBNull(RsEnt_tazabC!cont)) Then
                        ContAux = RsEnt_tazabC!cont + 1
                    End If
                End If
                RsEnt_tazabC.Close()

                RsEnt_tazab.AddNew()
                RsEnt_tazab!empresa = Trim(ObjetoGlobal.EmpresaActual)
                RsEnt_tazab!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                RsEnt_tazab!serie_albaran = "N" & Mid(Trim(ObjetoGlobal.EjercicioActual), 3, 2)
                RsEnt_tazab!numero_albaran = CLng(MiExcel.DevuelveValorCelda(Fila, 6))
                RsEnt_tazab!Contador = ContAux
                RsEnt_tazab!tipo_entrada = Rs1!tipo_entrada
                RsEnt_tazab!codigo_variedad = Rs1!codigo_variedad
                RsEnt_tazab!codigo_campo = Rs1!codigo_campo
                RsEnt_tazab!campo_terceros = Rs1!campo_terceros
                RsEnt_tazab!kg_a_liquidar = Rs1!kg_a_liquidar
                Cadena = ""
                If TipoDeEnvase = "NORMAL" Then
                    If Len(Trim(Codigo_palet)) < 9 Then
                        Cadena = StrDup(9 - Len(Trim(Codigo_palet)), "0") + Trim(Codigo_palet)
                    Else
                        Cadena = Trim(Codigo_palet)
                    End If
                    RsEnt_tazab!Codigo_palet = Cadena
                ElseIf TipoDeEnvase = "PRECALIBRADO" Then
                    RsEnt_tazab!palet_precalibre = Codigo_palet
                End If
                RsEnt_tazab!Kilos = Kg_albaran
                RsEnt_tazab!codigo_proceso = Trim(CnTabla01.Rs("codigo_proceso"))
                RsEnt_tazab.Update()
            Else
                MsgBox("No encuentro el albarán " & MiExcel.DevuelveValorCelda(Fila, 6))
            End If
            Rs1.Close()
        End If

    End Sub

    Private Sub CmdEdicionInforme_Click(sender As Object, e As EventArgs) Handles CmdEdicionInforme.Click
        Dim SQL As String
        Dim RsEtiquetas As New cnRecordset.CnRecordset, RsEntrada As New cnRecordset.CnRecordset
        Dim RsVariedad As New cnRecordset.CnRecordset, RsEnvases As New cnRecordset.CnRecordset, RsCalidades As New cnRecordset.CnRecordset
        Dim RsPalets As New cnRecordset.CnRecordset, RsConfecciones As New cnRecordset.CnRecordset, RsOrdenesConfeccion As New cnRecordset.CnRecordset
        Dim RsCarga_confeccion As New cnRecordset.CnRecordset, RsOrdenesCarga As New cnRecordset.CnRecordset
        Dim RsGrabar As New cnRecordset.CnRecordset, RsCliente As New cnRecordset.CnRecordset
        Dim Fila As Long, Pesos(10) As Long
        Dim i As Integer, j As Integer, jj As Integer, k As Integer
        Dim Tara As Double, PesoPalet As Long
        Dim TotalMuestreo As Long, TotalAjustado As Long, CalidadPrincipal As Integer, PesoCalidadPrincipal As Long, PesosAjustados(10) As Long
        Dim FechaProceso1 As Date, FechaProceso2 As Date
        Dim CuantosVolcados As Integer

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        If CnTabla01.CuantosRegistros <= 0 Then
            MsgBox("Debe seleccionar previamente el proceso de trazabilidad.")
            Exit Sub
        End If

        MiExcel = New LibreriaGeneral.EXCEL_VB(False)
        MiExcel.AbrirHojaExcel(RutaPlantillaInforme)
        MiExcel.Visible = False
        'MiHojaExcel = MiExcel.ObjetoHoja() ' En principio, no hace falta manipular directamente el objeto
        'RutaExcel = Trim(RutaMisDocumentos) + "\Producción\" + Trim(CnTabla01.Rs("codigo_proceso")) + "xls"
        'MiExcel.Path = RutaExcel

        FechaProceso1 = CDate(CnTabla01.Rs("fecha_inicio"))
        FechaProceso2 = CDate(CnTabla01.Rs("fecha_fin"))

        Fila = 6
        ReDim Volcado(30, 0)
        ReDim Volcado2(10, 0)
        CuantosVolcados = -1
        PBCabecera.Visible = True
        PBCabecera.Maximum = CnTabla02.CuantosRegistros
        PBCabecera.Value = 0
        MiExcel.NumeroPestañaActual = 1
        MiExcel.EscribeCelda(3, 2, "'" & Format(FechaProceso1, "dd/MM/yyyy"))
        Fila = 6

        For jj = 1 To CnTabla02.CuantosRegistros
            CnTabla02.IrARegistro(jj, True)
            PBCabecera.Value = jj
            Fila = Fila + 1
            MiExcel.EscribeCelda(Fila, 1, Format(CDate(CnTabla02.Rs("hora_marcaje")), "HH:mm"))
            MiExcel.EscribeCelda(Fila, 2, Trim(CnTabla02.Rs("descripcion_variedad")))
            Pesos(0) = CnTabla02.Rs("Kilos")
            Pesos(1) = CnTabla02.Rs("Kilos_e")
            For j = 1 To 5
                Pesos(j + 1) = CLng(CnTabla02.Rs("kilos_" & CStr(j)))
            Next
            MiExcel.EscribeCelda(Fila, 3, Format(Pesos(0), "00000"))
            For j = 1 To 6
                MiExcel.EscribeCelda(Fila, 3 + j, Format(Pesos(j), "00000"))
            Next
            MiExcel.EscribeCelda(Fila, 10, Trim(CnTabla02.Rs("tipo_marcaje")))
            MiExcel.EscribeCelda(Fila, 11, CStr(CnTabla02.Rs("numero_albaran")))
            MiExcel.EscribeCelda(Fila, 12, CStr(CnTabla02.Rs("codigo_campo")))
            MiExcel.EscribeCelda(Fila, 13, Trim(CnTabla02.Rs("marcaje")))

            TotalMuestreo = 0
            TotalAjustado = 0
            CalidadPrincipal = 0
            PesoCalidadPrincipal = -1
            For j = 1 To 6
                PesosAjustados(j) = 0
                If Pesos(j) > PesoCalidadPrincipal Then
                    CalidadPrincipal = j
                    PesoCalidadPrincipal = Pesos(j)
                End If
                TotalMuestreo = TotalMuestreo + Pesos(j)
            Next
            If TotalMuestreo = 0 And Pesos(0) > 0 Then
                CalidadPrincipal = 0
                PesoCalidadPrincipal = -1
                For j = 1 To 6
                    Pesos(j) = VolcadoTotal(j)
                    If Pesos(j) > PesoCalidadPrincipal Then
                        CalidadPrincipal = j
                        PesoCalidadPrincipal = Pesos(j)
                    End If
                    TotalMuestreo = TotalMuestreo + Pesos(j)
                Next
            End If
            If TotalMuestreo = 0 And Pesos(0) > 0 Then
                CalidadPrincipal = 0
                PesoCalidadPrincipal = -1
                For j = 1 To 6
                    If j <= 5 Then Pesos(j) = 20 Else Pesos(j) = 0
                    If Pesos(j) > PesoCalidadPrincipal Then
                        CalidadPrincipal = j
                        PesoCalidadPrincipal = Pesos(j)
                    End If
                    TotalMuestreo = TotalMuestreo + Pesos(j)
                Next
            End If
            If TotalMuestreo > 0 Then
                CuantosVolcados = CuantosVolcados + 1
                If CuantosVolcados > 0 Then
                    ReDim Preserve Volcado(30, CuantosVolcados)
                    ReDim Preserve Volcado2(10, CuantosVolcados)
                End If
                For j = 1 To 6
                    PesosAjustados(j) = Math.Round(Pesos(j) * Pesos(0) / TotalMuestreo, 0)
                    TotalAjustado = TotalAjustado + PesosAjustados(j)
                Next
                If TotalAjustado <> Pesos(0) Then
                    PesosAjustados(CalidadPrincipal) = PesosAjustados(CalidadPrincipal) - TotalAjustado + Pesos(0)
                End If
                Volcado(0, CuantosVolcados) = Pesos(0)
                For j = 1 To 6
                    Volcado(j, CuantosVolcados) = PesosAjustados(j)
                Next
                For j = 7 To 30 : Volcado(j, CuantosVolcados) = 0 : Next
                For j = 1 To 6 : VolcadoTotal(j) = VolcadoTotal(j) + PesosAjustados(j) : Next
            End If
            VolcadoTotal(0) = VolcadoTotal(0) + Pesos(0)
        Next

        Fila = 6
        ReDim Volcado(30, 0)
        ReDim Volcado2(10, 0)
        CuantosVolcados = -1
        PBCabecera.Visible = True
        PBCabecera.Maximum = CnTabla02.CuantosRegistros
        PBCabecera.Value = 0
        MiExcel.NumeroPestañaActual = 2
        MiExcel.EscribeCelda(3, 2, "'" & Format(FechaProceso1, "dd/MM/yyyy"))
        Fila = 6

        For jj = 1 To CnTabla03.CuantosRegistros
            CnTabla03.IrARegistro(jj, True)
            PBCabecera.Value = jj


            If Mid(Trim(CnTabla03.Rs("tipo_marcaje")), 1, 1) <> "I" And Mid(Trim(CnTabla03.Rs("tipo_marcaje")), 1, 1) <> "S" Then
                SQL = "SELECT * FROM palets WHERE codigo_palet='" & Trim(CnTabla03.Rs("marcaje")) & "' AND fecha_confeccion='" & Format(CnTabla03.Rs("fecha_marcaje"), "dd/MM/yyyy") & "' AND hora_confeccion='" & Format(CDate(CnTabla03.Rs("hora_marcaje")), "HH:mm:ss") & "'"
                If RsPalets.Open(SQL, ObjetoGlobal.cn) = False Then
                    MsgBox("Error al abrir la tabla palets")
                    Exit Sub
                End If
                If RsPalets.RecordCount > 0 Then
                    If Trim(RsPalets!tipo_fabricacion) <> "R" Then
                        Fila = Fila + 1
                        MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & CnTabla03.Rs("Codigo_palet"))
                        MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, Trim("" & RsPalets!orden_confeccion))
                        MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & RsPalets!hora_confeccion)
                        MiExcel.EscribeCelda(Fila, cuOrdenCarga, "" & RsPalets!orden_carga)
                        MiExcel.EscribeCelda(Fila, cuFechaCarga, "" & RsPalets!fecha_carga)
                        MiExcel.EscribeCelda(Fila, cuHoraCarga, "" & RsPalets!hora_Carga)

                        SQL = "SELECT * FROM ordenes_confeccion WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND numero_orden=" & CStr(RsPalets!orden_confeccion)
                        If RsOrdenesConfeccion.Open(SQL, ObjetoGlobal.cn) = False Then
                            MsgBox("Error al abrir la tabla ordenes_confeccion")
                            Exit Sub
                        End If
                        If RsOrdenesConfeccion.RecordCount > 0 Then
                            SQL = "SELECT * FROM VARIEDADES WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND CODIGO_VARIEDAD = '" & Trim(RsOrdenesConfeccion!codigo_variedad) & "'"
                            If RsVariedad.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla variedades")
                                Exit Sub
                            End If


                            If RsVariedad.RecordCount = 0 Then
                                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "" & RsOrdenesConfeccion!codigo_variedad)
                            Else
                                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, Trim(RsOrdenesConfeccion!codigo_variedad) + " - " + Trim(RsVariedad!Descripcion))
                            End If
                            RsVariedad.Close()

                            Tara = 0
                            SQL = "SELECT empresa, CODIGO_CONFECCION, DESCRIPCION FROM Confeccion WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND CODIGO_CONFECCION='" & Trim(RsOrdenesConfeccion!codigo_confeccion) & "'"

                            If RsConfecciones.Open(SQL, ObjetoGlobal.cn) = False Then
                                MsgBox("Error al abrir la tabla confeccion")
                                Exit Sub
                            End If
                            If RsConfecciones.RecordCount > 0 Then
                                MiExcel.EscribeCelda(Fila, cuCodigoConfeccion, "" & RsOrdenesConfeccion!codigo_confeccion & " - " & RsConfecciones!Descripcion)
                            Else
                                MiExcel.EscribeCelda(Fila, cuCodigoConfeccion, "" & RsOrdenesConfeccion!codigo_confeccion)
                            End If
                            RsConfecciones.Close()

                            MiExcel.EscribeCelda(Fila, cuCodigoPaletizacion, "" & RsOrdenesConfeccion!paletizacion)
                            PesoPalet = Math.Round(CnTabla03.Rs("Kilos"))
                            MiExcel.EscribeCelda(Fila, cuPeso, "" & Math.Round(PesoPalet, 0))
                            MiExcel.EscribeCelda(Fila, cuMarca, Trim("" & RsOrdenesConfeccion!codigo_marca))
                            MiExcel.EscribeCelda(Fila, cuCalibreTecnico, Trim("" & RsOrdenesConfeccion!calibre_tecnico))
                            MiExcel.EscribeCelda(Fila, cuCalibreComercial, Trim("" & RsOrdenesConfeccion!calibre_comercial))
                            MiExcel.EscribeCelda(Fila, cuCajasPalets, "" & RsOrdenesConfeccion!Cajas_Palet)
                            MiExcel.EscribeCelda(Fila, cuProduccionIntegrada, Trim("" & RsOrdenesConfeccion!produccion_integrada))
                            MiExcel.EscribeCelda(Fila, cuLote, Trim("" & RsOrdenesConfeccion!lote))
                            If Not IsDBNull(RsPalets!orden_carga) Then
                                SQL = "SELECT * FROM ordenes_carga WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND numero_orden=" & RsPalets!orden_carga
                                If RsOrdenesCarga.Open(SQL, ObjetoGlobal.cn) = False Then
                                    MsgBox("Error al abrir la tabla ordenes_carga")
                                    Exit Sub
                                End If
                                If RsOrdenesCarga.RecordCount > 0 Then
                                    SQL = "SELECT * FROM CLIENTES_COOP WHERE codigo_cliente = " & RsOrdenesCarga!Cliente
                                    If RsCliente.Open(SQL, ObjetoGlobal.cn) = False Then
                                        MsgBox("Error al abrir la tabla clientes_coop")
                                        Exit Sub
                                    End If
                                    MiExcel.EscribeCelda(Fila, cuCliente, "" & RsOrdenesCarga!Cliente & " " & Trim(RsCliente!nombre_cliente))
                                    RsCliente.Close()

                                    SQL = "SELECT * FROM DESTINATARIOS WHERE destinatario = " & RsOrdenesCarga!destinatario
                                    If RsCliente.Open(SQL, ObjetoGlobal.cn) = False Then
                                        MsgBox("Error al abrir la tabla destinatarios")
                                        Exit Sub
                                    End If
                                    MiExcel.EscribeCelda(Fila, cuDestinatario, "" & RsOrdenesCarga!destinatario & " " & Trim(RsCliente!nombre_dest))
                                    RsCliente.Close()

                                    MiExcel.EscribeCelda(Fila, cuMatricula, "" & RsOrdenesCarga!matricula1)
                                    MiExcel.EscribeCelda(Fila, cuObs_Cierre, "" & RsOrdenesCarga!obs_cierre)
                                End If
                                RsOrdenesCarga.Close()
                            End If
                        End If
                        RsOrdenesConfeccion.Close()
                    End If
                End If
                RsPalets.Close()
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "INAPLGR" Then
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "1")
                PesoPalet = PesoIndustriaNaranjaGrande
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "NARANJA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "INAPLPQ" Then '3900
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "2")
                PesoPalet = PesoIndustriaNaranjaPequeno
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "NARANJA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "IMAPLGR" Then '4000
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "3")
                PesoPalet = PesoIndustriaClementinaGrande
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "CLEMENTINA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "IMAPLPQ" Then '3800
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "4")
                PesoPalet = PesoIndustriaClementinaPequeno
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "CLEMENTINA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "ISAPLGR" Then '4020
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "5")
                PesoPalet = PesoIndustriaClementinaGrande
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "SATSUMA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "ISAPLPQ" Then '3820
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "6")
                PesoPalet = PesoIndustriaClementinaPequeno
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "SATSUMA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "IHIPLGR" Then '4060
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "7")
                PesoPalet = PesoIndustriaClementinaGrande
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "HIBRIDOS")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Trim(CnTabla03.Rs("marcaje")) = "IHIPLPQ" Then '3860
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "8")
                PesoPalet = PesoIndustriaClementinaPequeno
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "HIBRIDOS")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            ElseIf Mid(Trim(CnTabla03.Rs("marcaje")), 1, 2) = "SB" Or Mid(Trim(CnTabla03.Rs("marcaje")), 1, 2) = "SN" Or Mid(Trim(CnTabla03.Rs("marcaje")), 1, 2) = "SX" Or Mid(Trim(CnTabla03.Rs("marcaje")), 1, 2) = "SY" Then 'SANDIA
                Fila = Fila + 1
                MiExcel.EscribeCelda(Fila, cuCodigoPalet, "" & Trim(CnTabla03.Rs("marcaje")))
                MiExcel.EscribeCelda(Fila, cuOrdenConfeccion, "8")
                PesoPalet = PesoIndustriaClementinaPequeno
                MiExcel.EscribeCelda(Fila, cuPeso, CStr(PesoPalet))
                MiExcel.EscribeCelda(Fila, cuCodigoVariedad, "SANDIA")
                MiExcel.EscribeCelda(Fila, cuCalibreTecnico, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuCalibreComercial, "INDUSTRIA")
                MiExcel.EscribeCelda(Fila, cuHoraConfeccion, "" & CnTabla03.Rs("hora_marcaje"))
            End If
        Next

        PBCabecera.Visible = False
        MiExcel.Visible = True

    End Sub

End Class

