Imports System.ComponentModel

Public Class FrnOrdenesconfeccion
    Inherits LibreriaModeloMantenimiento.ModeloMantenimiento

    Public numero_orden_actual As Long

    'Public Function CambiarEnableDisable(modo As String)
    '    Dim i As Integer

    '    On Error Resume Next
    '    'If Not CnTabla01.Rs(1).EOF Then
    '    If CnTabla01.RegistroActual > 0 Then
    '        If Trim(CnTabla01.ValorCampo("Estado")) = "A" And modo <> "A" Then
    '            For Each CE In XCNE
    '                If Not (CE Is Nothing) Then
    '                    CE.TxtDatos.Enabled = False
    '                End If
    '            Next
    '            oa.Visible = True
    '        Else
    '            For Each CE In XCNE
    '                If Not (CE Is Nothing) Then
    '                    CE.TxtDatos.Enabled = True
    '                End If
    '            Next
    '            oa.Visible = False
    '        End If
    '    Else
    '        For Each CE In XCNE
    '            If Not (CE Is Nothing) Then
    '                CE.TxtDatos.Enabled = True
    '            End If
    '        Next
    '        oa.Visible = False
    '    End If

    'End Function
    Public Function ObtenPesoConfeccion() As Integer
        Dim RsP As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String
        Dim cCod As String

        'If CnTabla01.Rs(1).EOF Then Exit Function ' Quitado a ver que pasa
        If CnTabla01.RegistroActual = 0 Then
            Return 0
        End If

        cCod = "" & CnTabla01.ValorCampo("codigo_confeccion")

        If Trim(cCod) = "" Then
            ObtenPesoConfeccion = 0
            TxtcostePalet.Text = "0.00"
            Return 0
        End If


        sSql = "SELECT kg_bulto FROM confeccion WHERE Empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND codigo_confeccion='" & Trim(cCod) & "'"

        RsP.Open(sSql, ObjetoGlobal.cn)

        TxtcostePalet.Text = "0.00"

        If RsP.RecordCount > 0 Then
            ObtenPesoConfeccion = RsP!Kg_bulto
            TxtcostePalet.Text = Format(RsP!Kg_bulto, "###,##0.00")
        End If
        Return 0

    End Function


    Private Function OrdenDeCarga(dFecha) As String
        Dim sSql As String
        Dim rsOc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NumOrden As Integer

        sSql = "SELECT Max(numero_orden) AS Numero FROM ordenes_carga WHERE Empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND Fecha_camion='" & dFecha & "'"

        rsOc.Open(sSql, ObjetoGlobal.cn)
        If IsDBNull(rsOc!Numero) Then
            NumOrden = 1 'CInt("0" & rsOc!Numero) + 1
        Else
            NumOrden = CInt(Mid(Format(rsOc!Numero, "00000000"), 7, 2)) + 1
        End If
        'OrdenDeCarga = ("" & Format(DateAndTime.Year(dFecha) - 2000, "00") & Format(DateAndTime.Month(dFecha), "00") & Format(DateAndTime.Day(dFecha), "00") & Format(NumOrden, "00"))
        Return ("" & Format(DateAndTime.Year(dFecha) - 2000, "00") & Format(DateAndTime.Month(dFecha), "00") & Format(DateAndTime.Day(dFecha), "00") & Format(NumOrden, "00"))

    End Function


    'Private Sub CmdRepaletizacion_Click()
    '    Dim oForm As FrmRepaletizarMateriales

    '    If CnTabla01.Rs(1).RecordCount > 0 Then
    '        If CnTabla02.Rs(1).EOF Or CnTabla02.Rs(1).BOF Then
    '            MsgBox("Debe de tener un palet seleccionado")
    '            Exit Sub
    '        Else
    '      Set oForm = FrmRepaletizarMateriales
    '      Set oForm.ObjetoGlobal = Me.ObjetoGlobal
    '      oForm.Palet = CnTabla02.Rs(1)!codigo_palet
    '            oForm.Show 1, Me

    '      If oForm.correcto Then
    '                MsgBox("Palet repaletizado con éxito")
    '            End If
    '        End If
    '    Else
    '        MsgBox("Debe de seleccionar un palet")
    '    End If

    'End Sub

    'Private Sub CmdRestos_Click()
    '    Dim oForm As Object 'cssllirp108.Frmanotarrestos

    '    If SituacionFormulario = Estados.Inactivo Then
    '        If CnTabla01..Cuantos > 0 Then
    '    Set oForm = CreateObject("cssllirp108.Clsanotarrestos").Formulario
    '    Set oForm.ObjetoGlobal = Me.ObjetoGlobal
    '    oForm.Orden = CnTabla01.Rs(1)!numero_orden
    '            oForm.calibre = CnTabla01.Rs(1)!calibre_comercial
    '            oForm.dFecha = CnTabla01.Rs(1)!fecha_orden
    '            oForm.Show 1, Me
    'End If
    '    End If


    'End Sub

    'Private Sub CmdRomperPalet_Click() 'NSTD
    '    Dim oForm As FrmConsultaMateriales

    '    If CnTabla02.Rs(1).RecordCount > 0 Then
    '        If CnTabla02.Rs(1).EOF Or CnTabla02.Rs(1).BOF Then
    '            MsgBox("Debe de tener un palet seleccionado")
    '            Exit Sub
    '        Else
    '      Set oForm = FrmConsultaMateriales
    '      Set oForm.ObjetoGlobal = Me.ObjetoGlobal
    '      oForm.Palet = CnTabla02.Rs(1)!codigo_palet
    '            oForm.Show 1, Me

    '      If oForm.correcto Then
    '                MsgBox("Palet destruido")
    '            End If
    '        End If

    '    End If
    'End Sub

    Private Sub AnularOrden_Click(sender As Object, e As EventArgs) Handles AnularOrden.Click
        Dim Cadena As String, Campos() As String, Valores() As String, CamposTabla() As String
        Dim SubCadena As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer

        If SituacionFormulario = Estados.Inactivo Then

            'If CnTabla02.RecordCount > 0 Then
            If CnTabla02.RegistroActual > 0 Then
                MsgBox("No se puede anular una orden que tenga palets asociados")
                Exit Sub
            End If

            On Error Resume Next

            If CnTabla01.cuantos > 0 Then
                numero_orden_actual = CnTabla01.ValorCampo("numero_orden")
                If Not Trim(CnTabla01.ValorCampo("Estado")) = "A" Then
                    If MsgBox("Desea marcar como anulada la orden " & Trim(numero_orden_actual), vbYesNo + vbCritical + vbDefaultButton2, "anulación orden de confección") = vbYes Then

                        For Each CE In XCNE
                            If Not (CE Is Nothing) Then
                                CE.TxtDatos.Enabled = False
                            End If
                        Next
                        oa.Visible = True
                        Cadena = "A"
                        Rs.Open("SELECT * FROM Ordenes_confeccion WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and numero_orden = '" & numero_orden_actual & "'", ObjetoGlobal.cn)
                        If Rs.RecordCount > 0 Then
                            Rs!Estado = "A"
                            Rs.Update()
                        End If
                        Rs.Close()
                        ReDim Campos(0), Valores(0), CamposTabla(0)
                        Campos(0) = "estado" : Valores(0) = Cadena
                        HOOK_AsignarValores(CnTabla01, Campos, Valores)
                    Else
                        Exit Sub
                    End If
                End If
            Else
                MsgBox("Debe seleccionar previamente el albarán para la modificación")
                Exit Sub
            End If
        End If

    End Sub

    Private Sub DuplicarOrden_Click(sender As Object, e As EventArgs) Handles DuplicarOrden.Click
        Dim CodigoCreado As String
        Dim oFrm As FrmDuplicarOC = New FrmDuplicarOC

        If SituacionFormulario = Estados.Inactivo Then
            If CnTabla01.cuantos > 0 Then
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                'oFrm.RsAnterior = CnTabla01.Rs.rsactual
                oFrm.pform = Me
                If oFrm.ShowDialog() = DialogResult.OK Then
                    CodigoCreado = oFrm.Codigocreado
                    If oFrm.Seleccionar Then
                        Dim Campos(1) As String
                        Dim Valor(1) As String
                        Campos(0) = "empresa"
                        Campos(1) = "numero_orden"
                        Valor(0) = Trim(ObjetoGlobal.EmpresaActual)
                        Valor(1) = CodigoCreado.ToString
                        CnTabla_CTSeleccion(CnTabla01)
                        HOOK_AsignarValores(CnTabla01, Campos, Valor)
                        CnTabla_CTAceptar(CnTabla01)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub CmdRestos_Click(sender As Object, e As EventArgs) Handles CmdRestos.Click
        Dim oForm As FrmAnotarRestos = New FrmAnotarRestos


        If SituacionFormulario = Estados.Inactivo Then
            If CnTabla01.cuantos > 0 Then
                oForm.Orden = CnTabla01.ValorCampo("numero_orden")
                oForm.calibre = CnTabla01.ValorCampo("calibre_comercial")
                oForm.dFecha = CnTabla01.ValorCampo("fecha_orden")
                oForm.ShowDialog()
            End If
        End If

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
    Protected Overrides Function HOOK_Antes_de_Crear_Ficha(CT As CnTabla.CnTabla) As Boolean
        CambiarEnableDisable("A")
    End Function

    'Public Overrides Function HOOK_Antes_de_Crear_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
    Protected Overrides Function HOOK_En_el_validating_de_un_campo_en_creacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String) As Boolean
        Dim campos(0) As String
        Dim valores(0) As Object
        Dim PesoOrden As Double

        If Trim(CT.Tabla) = "ordenes_confeccion" And Trim(CE.Campo) = "fecha_orden" Then
            If Trim(Valor) <> "" Then
                Dim cOrden As String
                cOrden = PonerNumeroOrden(Trim(Valor))
                ReDim campos(1)
                ReDim valores(1)
                campos(0) = "numero_orden"
                valores(0) = cOrden
                HOOK_AsignarValores(CT, campos, valores)
            End If
        End If

        If Trim(CT.Tabla) = "ordenes_confeccion" And Trim(CE.Campo) = "codigo_confeccion" Then
            Try
                If Trim(Valor) <> "" Then
                    PesoOrden = ObtenPesoConfeccion(Valor)
                    'rs = CnTabla01.Rs(2)
                    'If rs!Cajas_palet <> 0 Then
                    If CnTabla01.ValorCampo("cajas_palet") <> 0 Then
                        ReDim campos(1)
                        ReDim valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = "" & (PesoOrden * CnTabla01.ValorCampo("cajas_palet"))
                        HOOK_AsignarValores(CT, campos, valores)
                    Else
                        ReDim campos(1)
                        ReDim valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = 0
                        HOOK_AsignarValores(CT, campos, valores)
                    End If
                Else
                    ReDim campos(1)
                    ReDim valores(1)
                    campos(0) = "peso_palet"
                    valores(0) = 0
                    HOOK_AsignarValores(CT, campos, valores)
                End If
                Return False
            Catch ex As Exception
                Return True
            End Try
        End If


        If Trim(CE.Campo) = "cajas_palet" Then
            Try
                If Trim(Valor) <> "" Then
                    If Trim(CnTabla01.ValorCampo("codigo_confeccion")) > "" Then
                        PesoOrden = ObtenPesoConfeccion(CnTabla01.ValorCampo("codigo_confeccion"))
                        ReDim campos(1)
                        ReDim valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = "" & (PesoOrden * Val(Valor))
                        HOOK_AsignarValores(CT, campos, valores)
                    Else
                        ReDim campos(1), valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = "0"
                        HOOK_AsignarValores(CT, campos, valores)
                    End If
                Else
                    ReDim campos(1)
                    ReDim valores(1)
                    campos(0) = "peso_palet"
                    valores(0) = "0"
                    HOOK_AsignarValores(CT, campos, valores)
                End If
                Return False
            Catch ex As Exception
                Return True
            End Try
        End If


    End Function

    Public Overrides Function HOOK_Cambio_de_registro(CT As CnTabla.CnTabla) As Boolean 'Comentado Probado Ejemplo

        If Trim(CT.Tabla) = "ordenes_confeccion" Then
            ObtenPesoConfeccion()
            CalculoCostesConfeccion()
            CambiarEnableDisable("X")
        End If

    End Function

    Private Function ObtenPesoConfeccion(cConfeccion)
        Dim RsP As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String

        Try
            sSql = "SELECT kg_bulto FROM confeccion WHERE Empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND codigo_confeccion='" & Trim(cConfeccion) & "'"
                If RsP.Open(sSql, ObjetoGlobal.cn) Then
                If RsP.RecordCount > 0 Then
                    Return RsP!Kg_bulto
                End If
            End If
        Catch ex As Exception

        End Try

        Return 0
    End Function

    Private Function PonerNumeroOrden(dFecha As String) As String
        Dim sSql As String
        Dim rsOc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NumOrden As Integer


        If SituacionFormulario = Estados.Creando Then

            sSql = "SELECT Max(Numero_orden) AS Numero FROM ordenes_confeccion WHERE Empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND Fecha_orden='" & dFecha & "'"
            rsOc.Open(sSql, ObjetoGlobal.cn)
            If IsDBNull(rsOc!Numero) Then
                NumOrden = 1
            Else
                NumOrden = CInt(Mid(Format(rsOc!Numero, "00000000"), 7, 2)) + 1
            End If
            Return ("" & Format(DateAndTime.Year(dFecha) - 2000, "00") & Format(DateAndTime.Month(dFecha), "00") & Format(DateAndTime.Day(dFecha), "00") & Format(NumOrden, "00"))
        End If

        Return ""

    End Function

    'Public Overrides Function HOOK_En_el_validating_de_un_campo_en_creacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_Despues_de_modificar_campo_en_creacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String) As Boolean
    'Public Overrides Function HOOK_Despues_de_modificar_campo_en_creacion_grid(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Valor As String, GE As DataGridView, HH As Hashtable) As Boolean
    'Protected Overrides Function HOOK_Antes_de_comprobar_valores_creacion_formato_ficha(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean) As Boolean
    'Public Overrides Function HOOK_Antes_de_comprobar_valores_creacion_formato_grid(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean, GE As DataGridView, HH As Hashtable) As Boolean
    'Public Overrides Function HOOK_Antes_de_InsertarFila_en_creacion(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Antes_de_Update_en_creacion(CT As CnTabla.CnTabla, ByRef Rs As cnRecordset.CnRecordset) As Boolean
    Public Overrides Function HOOK_Despues_de_grabar_registro_en_creacion_formato_ficha(CT As CnTabla.CnTabla) As Boolean
        CalculoCostesConfeccion()
    End Function

    Public Function CalculoCostesConfeccion()
        Dim RsConfeccion As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsCosteArticulos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nCosteConfeccion As Double
        Dim nCoste As Double
        Dim j As Integer
        Dim cCod As String

        'If CnTabla01.Rs(1).EOF Then Exit Function

        cCod = "" & CnTabla01.ValorCampo("codigo_confeccion")
        If Trim(cCod) = "" Then
            nCosteConfeccion = 0
            TxtCosteConf.Text = "0,00"
            Exit Function
        End If

        RsConfeccion.Open("SELECT * FROM MATERIALES_CONFEC WHERE empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CONFECCION = '" + Trim(cCod) + "'", ObjetoGlobal.cn)
        nCosteConfeccion = 0

        For j = 1 To RsConfeccion.RecordCount
            RsConfeccion.AbsolutePosition = j
            nCoste = 0
            rsCosteArticulos.Open("SELECT articulos.situacion_articulo,articulos.familia_compra, articulos.codigo_articulo, e_articulos.coste_standard FROM articulos, e_articulos WHERE e_articulos.empresa=articulos.empresa and e_articulos.codigo_articulo=articulos.codigo_articulo and articulos.empresa='" + Trim(RsConfeccion!empresa) + "' AND articulos.codigo_articulo='" + Trim(RsConfeccion!codigo_material) + "'", ObjetoGlobal.cn)
            If rsCosteArticulos.RecordCount > 0 Then
                If rsCosteArticulos!coste_standard > 0 Then
                    nCosteConfeccion = nCosteConfeccion + Math.Round(rsCosteArticulos!coste_standard * RsConfeccion!unidades_material, 2)
                ElseIf Trim(rsCosteArticulos!familia_compra) <> "0" Then
                    MsgBox("En la confección " & Trim(cCod) & " el artículo " & Trim(RsConfeccion!codigo_material) & " no tiene un coste standard")
                    Return 0
                End If
                If Trim(rsCosteArticulos!Situacion_articulo) = "OB" Then
                    MsgBox("En la confección " & Trim(cCod) & " el artículo " & Trim(RsConfeccion!codigo_material) & " es obsoleto")
                End If
            Else
                MsgBox("En la confección " & Trim(cCod) & " el artículo " & Trim(RsConfeccion!codigo_material) & " no se encuentra")
                Return 0
            End If
            rsCosteArticulos.Close()
        Next
        RsConfeccion.Close()

        TxtCosteConf.Text = Format(nCosteConfeccion, "###,##0.00")
        Return nCosteConfeccion

    End Function


    'Public Overrides Function HOOK_Despues_de_grabar_registro_en_creacion_formato_grid(CT As CnTabla.CnTabla) As Boolean
    'Protected Overrides Function HOOK_Proceso_de_creacion_finalizado_formato_ficha(CT As CnTabla.CnTabla) As Boolean
    'Public Overrides Function HOOK_Proceso_de_creacion_finalizado_formato_grid(CT As CnTabla.CnTabla) As Boolean

    'Protected Overrides Function HOOK_Antes_de_CTModificar(CT As CnTabla.CnTabla) As Boolean
    'Protected Overrides Function HOOK_Antes_de_llamar_a_GridEdicion_en_CTModificar(CT As CnTabla.CnTabla, GridEdicion As LibreriaGeneral.GridEdicion, ByRef Posicion As Point)
    Protected Overrides Function HOOK_Antes_de_Modificar_Ficha(CT As CnTabla.CnTabla) As Boolean
        'Dim rs As cnRecordset.CnRecordset

        If Trim(CT.Tabla) = "ordenes_confeccion" Then
            'rs = CnTabla01.Rs(2)
            'CambiarEnableDisableModicacion("E", IIf(CnTabla02.Rs(1).RecordCount > 0, True, False))
            CambiarEnableDisableModicacion("E", IIf(CnTabla02.cuantos > 0, True, False))
        End If

    End Function

    Private Function CambiarEnableDisableModicacion(E_S As String, bHayPalets As Boolean) As Boolean
        Dim bHayError As Boolean = False

        'If bHayPalets Then
        Try
            CnEdicion007.SiempreSoloLectura = IIf(bHayPalets, False, True)
            CnEdicion008.SiempreSoloLectura = IIf(bHayPalets, False, True)
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function





    'Public Overrides Function HOOK_Antes_de_Modificar_Grid(CT As CnTabla.CnTabla, GE As DataGridView, HH As Hashtable) As Boolean
    Protected Overrides Function HOOK_En_el_validating_de_un_campo_en_modificacion_ficha(CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, ByRef Valor As String) As Boolean 'Comentado Probado Ejemplo
        Dim campos(0) As String
        Dim valores(0) As Object
        Dim PesoOrden As Double

        If Trim(CT.Tabla) = "ordenes_confeccion" And Trim(CE.Campo) = "codigo_confeccion" Then
            Try
                If Trim(Valor) <> "" Then
                    PesoOrden = ObtenPesoConfeccion(Valor)
                    'rs = CnTabla01.Rs(2)
                    'If rs!Cajas_palet <> 0 Then
                    If CnTabla01.ValorCampo("Cajas_palet") <> 0 Then
                        ReDim campos(1)
                        ReDim valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = "" & (PesoOrden * CnTabla01.ValorCampo("Cajas_palet"))
                        HOOK_AsignarValores(CT, campos, valores)
                    Else
                        ReDim campos(1)
                        ReDim valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = 0
                        HOOK_AsignarValores(CT, campos, valores)
                    End If
                Else
                    ReDim campos(1)
                    ReDim valores(1)
                    campos(0) = "peso_palet"
                    valores(0) = 0
                    HOOK_AsignarValores(CT, campos, valores)
                End If
                Return False
            Catch ex As Exception
                Return True
            End Try
        End If


        If Trim(CE.Campo) = "cajas_palet" Then
            Try
                If Trim(Valor) <> "" Then
                    'If Trim(CnTabla01.Rs(2)!codigo_confeccion) > "" Then
                    If CnTabla01.ValorCampo("codigo_confeccion") Then
                        PesoOrden = ObtenPesoConfeccion(CnTabla01.ValorCampo("codigo_confeccion"))
                        ReDim campos(1)
                        ReDim valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = "" & (PesoOrden * Val(Valor))
                        HOOK_AsignarValores(CT, campos, valores)
                    Else
                        ReDim campos(1), valores(1)
                        campos(0) = "peso_palet"
                        valores(0) = "0"
                        HOOK_AsignarValores(CT, campos, valores)
                    End If
                Else
                    ReDim campos(1)
                    ReDim valores(1)
                    campos(0) = "peso_palet"
                    valores(0) = "0"
                    HOOK_AsignarValores(CT, campos, valores)
                End If
                Return False
            Catch ex As Exception
                Return True
            End Try
        End If

    End Function
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

    Protected Overrides Function HOOK_Antes_de_CTModificar(CT As CnTabla.CnTabla) As Boolean
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If Trim(CT.Tabla) = "ordenes_confeccion" Then
            'rs = CnTabla01.Rs(2)
            CambiarEnableDisableModicacion("E", IIf(CnTabla01.cuantos > 0, True, False))
        End If
        Return False

    End Function

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

    Private Sub CmdCambioConfeccion_Click(sender As Object, e As EventArgs) Handles CmdCambioConfeccion.Click
        Dim Campos() As String
        Dim Valores() As String
        Dim CamposTabla() As String
        Dim oForm As FrmCambioConfeccion = New FrmCambioConfeccion
        Dim sSql As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim rsRep As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim txtErrores As String
        Dim trans As SqlClient.SqlTransaction

        If SituacionFormulario = Estados.Inactivo Then
            If CnTabla01.cuantos > 0 Then
                numero_orden_actual = CnTabla01.ValorCampo("numero_orden")
                'CnTabla01.ValorCampo("codigo_dsd")

                oForm.ObjetoGlobal = Me.ObjetoGlobal
                oForm.Cajas = CnTabla01.ValorCampo("cajas_palet")
                oForm.Confeccion = CnTabla01.ValorCampo("codigo_confeccion")
                oForm.paletizacion = CnTabla01.ValorCampo("paletizacion")

                If oForm.ShowDialog = DialogResult.OK Then

                    'CnTabla_CTModificar(CnTabla01)
                    'CEConf = CnTabla01.XCnEdicion("ordenes_confeccion.codigo_confeccion")
                    ''If Not (CEConf Is Nothing) Then
                    ''    CEConf.Editable = True
                    ''    CEConf.Modificable = True
                    ''End If

                    'CEPal = CnTabla01.XCnEdicion("ordenes_confeccion.paletizacion")
                    ''If Not (CEPal Is Nothing) Then
                    ''    CEPal.Editable = True
                    ''    CEPal.Modificable = True
                    ''End If

                    'CECajas = CnTabla01.XCnEdicion("ordenes_confeccion.cajas_palet")
                    ''If Not (CECajas Is Nothing) Then
                    ''    CECajas.Editable = True
                    ''    CECajas.Modificable = True
                    ''End If

                    ReDim Campos(2), Valores(2), CamposTabla(2)
                    Campos(0) = "codigo_confeccion" : Valores(0) = oForm.Confeccion
                    Campos(1) = "paletizacion" : Valores(1) = oForm.paletizacion
                    Campos(2) = "cajas_palet" : Valores(2) = oForm.Cajas
                    HOOK_AsignarValores(CnTabla01, Campos, Valores)

                    Try

                        ObjetoGlobal.cn.Open()
                        trans = ObjetoGlobal.cn.BeginTransaction()

                        sSql = "SELECT * FROM palets WHERE empresa='" & ObjetoGlobal.EmpresaActual & "' AND orden_confeccion =" & "" & CnTabla01.ValorCampo("numero_orden")
                        rs.Open(sSql, ObjetoGlobal.cn, True,,,,,, trans)
                        While Not rs.EOF
                            If rs!tipo_fabricacion <> "R" And rs!tipo_fabricacion <> "A" Then
                                rsRep.Open("SELECT * FROM repaletizacion WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND palet_inicial= '" & rs!codigo_palet & "'", ObjetoGlobal.cn)
                                If rsRep.EOF Then
                                    AnularMovimientosPalet(rs!codigo_palet, trans)
                                    rs!actualizado_mat = "N"
                                    rs.Update()
                                Else
                                    txtErrores = txtErrores & "Palet: " & Trim(rs!codigo_palet) & " no modificado por ser origen de una repaletización " & Chr(13) & Chr(10)
                                End If
                                rsRep.Close()
                            Else
                                If rs!tipo_fabricacion = "R" Then
                                    txtErrores = txtErrores & "Palet: " & Trim(rs!codigo_palet) & " no modificado por tratarse repaletización por error " & Chr(13) & Chr(10)
                                Else
                                    txtErrores = txtErrores & "Palet: " & Trim(rs!codigo_palet) & " no modificado por tratarse de una anulación " & Chr(13) & Chr(10)
                                End If
                            End If
                            rs.MoveNext()
                        End While
                        trans.Commit()
                        ObjetoGlobal.cn.Close()

                        If txtErrores <> "" Then
                            txtErrores = "Proceso concluido con las siguientes salvedades" & Chr(13) & Chr(10) & txtErrores
                            MsgBox(txtErrores)
                        Else
                            MsgBox("Proceso concluido")
                        End If

                    Catch ex As Exception
                        trans.Rollback()
                        ObjetoGlobal.cn.Close()
                        MsgBox("Se ha producido un error al grabar los datos. Error :" & Err.Description, vbInformation, "")
                    End Try

                    rs.Close()

                End If
            Else
                MsgBox("Debe seleccionar previamente el albarán para la modificación")
                Exit Sub
            End If
        End If
        Exit Sub

    End Sub

    Private Sub CmdGeneraCarga_Click(sender As Object, e As EventArgs) Handles CmdGeneraCarga.Click
        Dim RsTemp As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsRel As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nOrden_Carga As Long
        Dim oForm As FrmDatosComplementariosOrden = New FrmDatosComplementariosOrden
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If SituacionFormulario = Estados.Inactivo Then

            If CnTabla01.cuantos > 0 Then
                numero_orden_actual = "" & CnTabla01.ValorCampo("numero_orden")
                If Trim(numero_orden_actual) = "" Then
                    MsgBox("Debe existir una orden de confección seleccionada")
                    Return
                End If
                RsRel.Open("SELECT * FROM carga_confeccion WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND orden_confeccion=" & numero_orden_actual, ObjetoGlobal.cn)
                If RsRel.RecordCount > 0 Then
                    MsgBox("Esta orden de confección ya está ligada a una o más ordenes de carga")
                    Return
                End If
                RsRel.Close()

                oForm.ObjetoGlobal = Me.ObjetoGlobal
                oForm.fecha_camion = CnTabla01.ValorCampo("fecha_orden")
                If oForm.ShowDialog = DialogResult.OK Then
                    If oForm.EsCorrecto Then
                        nOrden_Carga = OrdenDeCarga(oForm.fecha_camion)
                        RsTemp.Open("SELECT * FROM ordenes_carga WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND 1=0", ObjetoGlobal.cn, True)
                        RsTemp.AddNew()
                        RsTemp!cliente = CnTabla01.ValorCampo("codigo_cliente")
                        RsTemp!destinatario = CnTabla01.ValorCampo("codigo_destinatario")
                        RsTemp!tipo_transporte = oForm.tipo_transporte
                        RsTemp!transportista = oForm.transportista
                        RsTemp!matricula2 = oForm.matricula2
                        RsTemp!matricula1 = oForm.matricula1
                        RsTemp!lote = oForm.lote
                        RsTemp!hora_prevista = oForm.hora_prevista
                        RsTemp!fecha_camion = oForm.fecha_camion
                        RsTemp!numero_orden = nOrden_Carga
                        RsTemp!empresa = ObjetoGlobal.EmpresaActual
                        RsTemp.Update()

                        ' Y ahora, por último el nexo de unión
                        RsRel.Open("SELECT * FROM carga_confeccion WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND 1=0", ObjetoGlobal.cn, True)
                        RsRel.AddNew()
                        RsRel!empresa = "" & Trim(ObjetoGlobal.EmpresaActual)
                        RsRel!orden_confeccion = Trim(numero_orden_actual)
                        RsRel!Orden_Carga = Trim(nOrden_Carga)
                        RsRel!numero_palets = CnTabla01.ValorCampo("numero_palets")
                        RsRel!palets_Cargados = 0
                        RsRel!usumodificacion = ObjetoGlobal.NombreUsuarioActual
                        RsRel!fechamodificacion = Date.Now
                        RsRel!horamodificacion = DateTime.Now.ToString("HH:mm:ss")
                        RsRel.Update()
                        MsgBox("Se ha generado una orden de carga con el número " & nOrden_Carga)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub CmdRomperPalet_Click(sender As Object, e As EventArgs) Handles CmdRomperPalet.Click
        Dim oForm As FrmDestruirPalet = New FrmDestruirPalet

        If CnTabla02.cuantos > 0 Then
            If CnTabla02.RegistroActual = 0 Then
                MsgBox("Debe de tener un palet seleccionado")
                Exit Sub
            Else
                oForm.ObjetoGlobal = Me.ObjetoGlobal
                oForm.Palet = CnTabla02.ValorCampo("codigo_palet")
                If oForm.ShowDialog = DialogResult.OK Then
                    MsgBox("Palet destruido")
                End If
            End If
        End If

    End Sub

    Private Sub CmdRepaletizacion_Click(sender As Object, e As EventArgs) Handles CmdRepaletizacion.Click
        Dim oForm As FrmRepaletizacionMateriales = New FrmRepaletizacionMateriales

        If CnTabla02.cuantos > 0 Then
            If CnTabla02.RegistroActual = 0 Then
                MsgBox("Debe de tener un palet seleccionado")
                Return
            Else
                oForm.ObjetoGlobal = Me.ObjetoGlobal
                oForm.Palet = CnTabla02.ValorCampo("codigo_palet")
                If oForm.ShowDialog = DialogResult.OK Then
                    MsgBox("Palet repaletizado con éxito")
                End If
            End If
        Else
            MsgBox("Debe de seleccionar un palet")
        End If

    End Sub

    Public Sub AnularMovimientosPalet(CodPalet As String, trans As SqlClient.SqlTransaction)
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String

        sSql = "SELECT * FROM almacen" & Trim(ObjetoGlobal.EjercicioActual) & "_h "
        sSql = sSql + " WHERE empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND (tipo_movimiento = 'SC' OR tipo_movimiento = 'SPT') AND tipo_documento = 'PAL' AND numero_documento ='" & Trim(CodPalet) & "'"
        rs.Open(sSql, ObjetoGlobal.cn, True,,,,,, trans)
        While rs.RecordCount > 0
            rs.MoveFirst()
            rs.Delete()
        End While
        rs.Close()

    End Sub

    Function HOOK_Antes_de_comprobar_valores_creacion_formato_ficha(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean) As Boolean 'Comentado Probado Ejemplo

        SeIgnoraComprobacion = False
        HOOK_Antes_de_comprobar_valores_creacion_formato_ficha = False

    End Function

    Function HOOK_Antes_de_comprobar_valores_creacion_formato_grid(CT As CnTabla.CnTabla, ByRef SeIgnoraComprobacion As Boolean, GE As DataGridView, HH As Hashtable) As Boolean 'Comentado Probado Ejemplo

        SeIgnoraComprobacion = False
        HOOK_Antes_de_comprobar_valores_creacion_formato_grid = False

    End Function

    Public Function CambiarEnableDisable(modo As String)  'NOSTD
        Dim i As Integer

        Try
            If CnTabla01.cuantos <> 0 Then
                If Trim(CnTabla01.ValorCampo("estado")) = "A" And modo <> "A" Then
                    Me.TabCabecera.Enabled = False
                    Me.TabGeneral.Enabled = False
                    oa.Visible = True
                Else
                    Me.TabCabecera.Enabled = True
                    Me.TabGeneral.Enabled = True
                    oa.Visible = False
                End If
            Else
                Me.TabCabecera.Enabled = True
                Me.TabGeneral.Enabled = True
                oa.Visible = False
            End If
        Catch ex As Exception

        End Try

    End Function


End Class

