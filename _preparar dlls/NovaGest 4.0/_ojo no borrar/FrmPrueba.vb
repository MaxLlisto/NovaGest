
Imports System.ComponentModel
'Imports Microsoft.Office.Core
'Imports Microsoft.Office.Interop.Excel 'Depende del COM Microsoft Excel 12.0
'Imports System.Data

Public Class FrmPrueba

    Public FechaHoraSeleccionada As DateTime
    Public FechaSeleccionada As Date

    Private Parametros(10) As String

    Private NumeroTablas As Integer = 0
    Private Tablas() As String
    Private TablaPadre() As String
    Private EnlacesTablas() As Long
    Private ContenedorTabla() As Control
    Private XCT() As CnTabla.CnTabla

    Private UltimoControlCnEdicion As Integer = 0
    Private XCNE() As CnEdicion.CnEdicion
    Private ContenedorCnEdicion() As Control

    Private XGrid(0) As DataGridView
    Private ContenedorGrid(0) As Control

    Private TabsUsados(2, 9) As Boolean

    Private SituacionFormulario As Estados = Estados.Inicial
    Private TablaEnEdicion As Integer = -1

    Private OrdenControles() As Integer

    Dim ContadorTraza As Integer = 0

    Private WithEvents TTCell As DataGridViewTextBoxEditingControl

    Public Enum Estados
        Inicial
        Inactivo
        PasandoACrear
        Creando
        AceptandoCreacion
        CancelandoCreacion
        PasandoAModificar
        Modificando
        AceptandoModificacion
        CancelandoModificacion
        PasandoAEliminar
        Eliminando
        AceptandoEliminacion
        CancelandoEliminacion
        PasandoASeleccionar
        Seleccionando
        AceptandoSeleccion
        CancelandoSeleccion
        PasandoAFiltrar
        Filtrando
        AceptandoFiltro
        CancelandoFiltro
    End Enum


    Private Sub Mantenimiento_Load(sender As Object, e As EventArgs) Handles Me.Load
        TabGeneral.SelectedTab = TabGeneral.TabPages(0)
        AsignarParametros()
        CrearArrayDeControles()
        HacerInvisiblesTabsNoUsados()
        RehacerTabIndex()
        InicializarControles()
        ConstruirGrids()
        CambioSituacionFormulario(Estados.Inactivo, -1)
        AsociarManejadores()
    End Sub

    ' ==== Proceso de inicialización  =====
    Private Sub AsignarParametros()
        Parametros(1) = ObjetoGlobal.EmpresaActual
        Parametros(2) = ObjetoGlobal.EmpresaRazonSocial
        Parametros(3) = ObjetoGlobal.EjercicioActual
        Parametros(4) = ObjetoGlobal.DescripcionEjercicio
        Parametros(5) = ObjetoGlobal.UsuarioActual
        Parametros(6) = ObjetoGlobal.NombreUsuarioActual
    End Sub

    Private Sub CrearArrayDeControles()
        Dim C As Control, D As Control, NT As Integer

        'CnTabla y CnEdicion
        For Each C In Me.PanelSuperior.Controls
            TabsUsados(0, 0) = True
            IncluirControl(C, Me.PanelSuperior, 1, 0)
        Next

        Me.PanelCentral.Controls.Item("TabCabecera").TabIndex = 10
        Me.PanelCentral.Controls.Item("TabCabecera").TabStop = False

        For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                D.TabIndex = 10 * (NT + 1)
                D.TabStop = False
                For Each C In D.Controls
                    TabsUsados(1, NT) = True
                    IncluirControl(C, D, 1, NT)
                Next
            End If
        Next

        Me.PanelCentral.Controls.Item("TabGeneral").TabIndex = 1000
        Me.PanelCentral.Controls.Item("TabGeneral").TabStop = False

        For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                D.TabIndex = 1000 + 10 * NT
                D.TabStop = False
                For Each C In D.Controls
                    NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                    TabsUsados(2, NT) = True
                    IncluirControl(C, D, 1, 10 + NT)
                Next
            End If
        Next

        'Resto de controles
        For Each C In Me.PanelSuperior.Controls
            TabsUsados(0, 0) = True
            IncluirControl(C, Me.PanelSuperior, 2, 0)
        Next
        For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                For Each C In D.Controls
                    TabsUsados(1, NT) = True
                    IncluirControl(C, D, 2, NT)
                Next
            End If
        Next
        For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                For Each C In D.Controls
                    TabsUsados(2, NT) = True
                    IncluirControl(C, D, 2, 10 + NT)
                Next
            End If
        Next
    End Sub

    Private Sub HacerInvisiblesTabsNoUsados()
        Dim i As Integer, Cadena As String
        Dim C As TabControl, CP As TabPage
        Dim FlagHay As Boolean = False
        Dim H1 As Integer


        For i = 0 To 9
            If TabsUsados(1, i) = False Then
                Cadena = "TP" + Format(i, "00")
                C = Me.PanelCentral.Controls.Item("TabCabecera")
                CP = C.TabPages(Cadena)
                C.TabPages.Remove(CP)
            Else
                FlagHay = True
            End If
        Next

        For i = 0 To 9
            If TabsUsados(2, i) = False Then
                Cadena = "TabPage" + Format(i, "00")
                C = Me.PanelCentral.Controls.Item("TabGeneral")
                CP = C.TabPages(Cadena)
                C.TabPages.Remove(CP)
            End If

        Next
    End Sub

    Public Sub RehacerTabIndex()

        Dim i As Integer, j As Integer, Contenedor As Integer, L1 As Long, L2 As Long, Clave As String, Cuantos As Integer
        Dim CE As CnEdicion.CnEdicion
        Dim ArrayDeControles() As String, Provisional As String, Provisional2 As Integer
        Dim MismaLinea As Boolean, TAG As Integer, TAGActual As Integer, Posicion As Long, PosicionLinea As Long
        Dim FlagCambiado As Boolean, OrdenTabindex As Integer, OrdenGrid As Integer

        Cuantos = 0
        For i = 1 To UltimoControlCnEdicion
            If Not (XCNE(i)) Is Nothing Then
                CE = XCNE(i)
                If CE.EdicionEnGrid = False Then
                    L1 = CE.TxtDatos(0).Top
                    L2 = CE.TxtDatos(0).Left
                Else
                    L1 = CE.Top
                    L2 = CE.Left
                End If
                Contenedor = 0
                If LCase(Microsoft.VisualBasic.Left(ContenedorCnEdicion(i).Name, 2)) = "tp" Then
                    Contenedor = 10 + CInt(Mid(ContenedorCnEdicion(i).Name, 3))
                ElseIf LCase(Microsoft.VisualBasic.Left(ContenedorCnEdicion(i).Name, 7)) = "tabpage" Then
                    Contenedor = 30 + CInt(Mid(ContenedorCnEdicion(i).Name, 8))
                End If

                Clave = Format(Contenedor, "00") + Format(L1, "00000") + Format(L2, "00000")
                Cuantos = Cuantos + 1
                ReDim Preserve ArrayDeControles(Cuantos)
                ReDim Preserve OrdenControles(Cuantos)
                ArrayDeControles(Cuantos) = Clave
                OrdenControles(Cuantos) = i
            End If

        Next
        For i = 1 To Cuantos - 1
            For j = i + 1 To Cuantos
                If ArrayDeControles(j) < ArrayDeControles(i) Then
                    Provisional = ArrayDeControles(i)
                    ArrayDeControles(i) = ArrayDeControles(j)
                    ArrayDeControles(j) = Provisional

                    Provisional2 = OrdenControles(i)
                    OrdenControles(i) = OrdenControles(j)
                    OrdenControles(j) = Provisional2
                End If
            Next
        Next

        MismaLinea = False : TAGActual = -1
        PosicionLinea = 0
        FlagCambiado = False
        For i = 1 To Cuantos
            TAG = CInt(Microsoft.VisualBasic.Left(ArrayDeControles(i), 2))
            Posicion = CLng(Mid(ArrayDeControles(i), 3, 5))
            If TAG > TAGActual Then
                MismaLinea = False
                PosicionLinea = Posicion
                TAGActual = TAG
            Else
                If Posicion - PosicionLinea < 5 Then
                    MismaLinea = True
                Else
                    MismaLinea = False
                    PosicionLinea = Posicion
                End If
            End If
            If MismaLinea = True And Posicion <> PosicionLinea Then
                ArrayDeControles(i) = Microsoft.VisualBasic.Left(ArrayDeControles(i), 2) + Format(PosicionLinea, "00000") + Mid(ArrayDeControles(i), 8)
                FlagCambiado = True
            End If
        Next

        If FlagCambiado = True Then
            For i = 1 To Cuantos - 1
                For j = i + 1 To Cuantos
                    If ArrayDeControles(j) < ArrayDeControles(i) Then
                        Provisional = ArrayDeControles(i)
                        ArrayDeControles(i) = ArrayDeControles(j)
                        ArrayDeControles(j) = Provisional

                        Provisional2 = OrdenControles(i)
                        OrdenControles(i) = OrdenControles(j)
                        OrdenControles(j) = Provisional2
                    End If
                Next
            Next
        End If

        OrdenTabindex = 0
        For i = 1 To Cuantos
            If XCNE(OrdenControles(i)).EdicionEnGrid = False Then
                OrdenTabindex = OrdenTabindex + 1
                XCNE(OrdenControles(i)).TxtDatos(0).TabIndex = OrdenTabindex
                XCNE(OrdenControles(i)).TxtDatos(0).TabStop = True
            End If
        Next

        'TxtParaFoco.TabIndex = 9999
        'TxtParaFoco.TabStop = False
    End Sub

    Private Sub InicializarControles()
        Dim CE As CnEdicion.CnEdicion, i As Integer, j As Integer

        For i = 1 To UltimoControlCnEdicion
            If Not (XCNE(i) Is Nothing) Then
                CE = XCNE(i)
                QueTabla(CE).XCnEdicion(Trim(CE.Tabla) + "." + Trim(CE.Campo)) = CE
            End If
        Next

        For i = 1 To NumeroTablas
            XCT(i).Inicializar(ObjetoGlobal)
            XCT(i).Enabled = True
            If Trim(XCT(i).TablaPadre) > "" Then
                For j = 1 To NumeroTablas
                    If XCT(j).Tabla = Trim(XCT(i).TablaPadre) Then
                        XCT(i).NumTablaPadreFormulario = j
                        Exit For
                    End If
                Next
            End If
            XCT(i).DefinirPrimeryUltimoControl()
        Next
    End Sub

    Private Sub ConstruirGrids()
        Dim C As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, i As Integer

        For Each C In XCT
            If Not (C Is Nothing) Then
                If C.HayGrid = True Then
                    For i = 1 To UBound(OrdenControles, 1)
                        CE = XCNE(OrdenControles(i))
                        If CE.EdicionEnGrid = True Then
                            If CE.NumeroTablaFormulario(0) = C.NumTablaFormulario Then
                                C.IncluirEnGrid(CE)
                            End If
                        End If
                    Next
                    C.InicializarGrid()
                End If
            End If
        Next
    End Sub
    Public Sub CambioSituacionFormulario(Estado As Estados, NumTabla As Integer, Optional ByRef Retorno As String = "")
        Dim C As CnTabla.CnTabla

        For Each C In XCT
            If Not (C Is Nothing) Then
                If NumTabla = C.NumTablaFormulario Then
                    C.CambioSituacionTabla(Estado, (NumTabla = C.NumTablaFormulario), EsAntecesor(C.NumTablaFormulario, NumTabla), Retorno)
                Else
                    C.CambioSituacionTabla(Estado, (NumTabla = C.NumTablaFormulario), EsAntecesor(C.NumTablaFormulario, NumTabla))
                End If
            End If
        Next
        SituacionFormulario = Estado
        TablaEnEdicion = NumTabla
    End Sub

    Private Sub AsociarManejadores()
        Dim CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion
        Dim C As Control, D As Control, BT As Button

        For Each CT In XCT
            If Not (CT Is Nothing) Then
                AddHandler CT.CTSeleccion, AddressOf CnTabla_CTSeleccion
                AddHandler CT.CTAnadir, AddressOf CnTabla_CTAnadir
                AddHandler CT.CTModificar, AddressOf CnTabla_CTModificar
                AddHandler CT.CTAceptar, AddressOf CnTabla_CTAceptar
                AddHandler CT.CTCancelar, AddressOf CnTabla_CTCancelar
                AddHandler CT.CTEliminar, AddressOf CnTabla_CTEliminar
                AddHandler CT.CTMostrarLookup, AddressOf CnTabla_MostrarLookup
            End If
        Next

        For Each CE In XCNE
            If Not (CE Is Nothing) Then
                If Not CE.TxtDatos(0) Is Nothing Then
                    AddHandler CE.TxtDatos(0).Enter, AddressOf TxtDatos_Enter
                    AddHandler CE.TxtDatos(0).GotFocus, AddressOf TxtDatos_GotFocus
                    AddHandler CE.TxtDatos(0).KeyPress, AddressOf TxtDatos_KeyPress
                    AddHandler CE.TxtDatos(0).KeyDown, AddressOf TxtDatos_KeyDown
                    AddHandler CE.TxtDatos(0).KeyUp, AddressOf TxtDatos_KeyUp
                    AddHandler CE.TxtDatos(0).TextChanged, AddressOf TxtDatos_TextChanged
                    AddHandler CE.TxtDatos(0).Validating, AddressOf TxtDatos_Validating
                    AddHandler CE.TxtDatos(0).Validated, AddressOf TxTDatos_Validated
                    AddHandler CE.TxtDatos(0).Leave, AddressOf TxtDatos_Leave
                    AddHandler CE.TxtDatos(0).LostFocus, AddressOf TxtDatos_LostFocus
                    AddHandler CE.TxtDatos(0).Click, AddressOf TxtDatos_Click
                    AddHandler CE.TxtDatos(0).DoubleClick, AddressOf TxtDatos_DoubleClick
                    AddHandler CE.TxtDatos(0).MouseClick, AddressOf TxtDatos_MouseClick
                    AddHandler CE.TxtDatos(0).MouseDoubleClick, AddressOf TxtDatos_MouseDoubleClick
                    AddHandler CE.TxtDatos(0).PreviewKeyDown, AddressOf TxtDatos_PreviewKeyDown
                    AddHandler CE.TxtDatos(0).Disposed, AddressOf TxtDatos_Disposed
                End If
            End If
        Next

        'AddHandler TTCell.Enter, AddressOf TTCell_Enter
        'AddHandler TTCell.GotFocus, AddressOf TTCell_GotFocus
        'AddHandler TTCell.KeyPress, AddressOf TTCell_KeyPress
        'AddHandler TTCell.KeyDown, AddressOf TTCell_KeyDown
        'AddHandler TTCell.KeyUp, AddressOf TTCell_KeyUp
        'AddHandler TTCell.TextChanged, AddressOf TTCell_TextChanged
        'AddHandler TTCell.Validating, AddressOf TTCell_Validating
        'AddHandler TTCell.Validated, AddressOf TTCEll_Validated
        'AddHandler TTCell.Leave, AddressOf TTCell_Leave
        'AddHandler TTCell.LostFocus, AddressOf TTCell_LostFocus
        'AddHandler TTCell.Click, AddressOf TTCell_Click
        'AddHandler TTCell.DoubleClick, AddressOf TTCell_DoubleClick
        'AddHandler TTCell.MouseClick, AddressOf TTCell_MouseClick
        'AddHandler TTCell.MouseDoubleClick, AddressOf TTCell_MouseDoubleClick
        'AddHandler TTCell.PreviewKeyDown, AddressOf TTCell_PreviewKeyDown
        'AddHandler TTCell.Disposed, AddressOf TTCell_Disposed

        For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
            If LCase(D.GetType().Name) = "tabpage" Then
                For Each C In D.Controls
                    If LCase(Microsoft.VisualBasic.Left(C.Name, 3)) = "cmd" Then
                        BT = TryCast(C, Button)
                        If Microsoft.VisualBasic.Left(LCase(BT.Name), 8) = "cmdcombo" Then
                            AddHandler BT.Click, AddressOf CmdCombo_Click
                        ElseIf Microsoft.VisualBasic.Left(LCase(BT.Name), 7) = "cmdfecha" Then
                            AddHandler BT.Click, AddressOf CmdFecha_Click
                        ElseIf Microsoft.VisualBasic.Left(LCase(BT.Name), 7) = "cmdgrid" Then
                            AddHandler BT.Click, AddressOf CmdGrid_Click
                        ElseIf Microsoft.VisualBasic.Left(LCase(BT.Name), 16) = "cmdmantenimiento" Then
                            AddHandler BT.Click, AddressOf CmdMantenimiento_Click
                        End If
                    End If
                Next
            End If
        Next
        For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
            If LCase(D.GetType().Name) = "tabpage" Then
                For Each C In D.Controls
                    If LCase(Microsoft.VisualBasic.Left(C.Name, 3)) = "cmd" Then
                        BT = TryCast(C, Button)
                        If Microsoft.VisualBasic.Left(LCase(BT.Name), 8) = "cmdcombo" Then
                            AddHandler BT.Click, AddressOf CmdCombo_Click
                        ElseIf Microsoft.VisualBasic.Left(LCase(BT.Name), 8) = "cmdfecha" Then
                            AddHandler BT.Click, AddressOf CmdFecha_Click
                        ElseIf Microsoft.VisualBasic.Left(LCase(BT.Name), 7) = "cmdgrid" Then
                            AddHandler BT.Click, AddressOf CmdGrid_Click
                        ElseIf Microsoft.VisualBasic.Left(LCase(BT.Name), 16) = "cmdmantenimiento" Then
                            AddHandler BT.Click, AddressOf CmdMantenimiento_Click
                        End If
                    End If
                Next
            End If
        Next



    End Sub


    Private Function IncluirControl(C As Control, CNT As Control, NumeroDeProceso As Integer, NumeroContenedor As Integer) As Boolean
        Dim MensajeError As String = "", i As Integer, FlagEncontrado As Boolean = False
        Dim CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Cadena As String, Indice As Integer, Indice2 As Integer

        IncluirControl = False
        If NumeroDeProceso = 1 Then
            If LCase(C.GetType().Name) = "cntabla" Then
                CT = DirectCast(C, CnTabla.CnTabla)
                NumeroTablas = NumeroTablas + 1
                CT.NumTablaFormulario = NumeroTablas

                ReDim Preserve Tablas(NumeroTablas)
                ReDim Preserve EnlacesTablas(NumeroTablas)
                ReDim Preserve TablaPadre(NumeroTablas)
                ReDim Preserve ContenedorTabla(NumeroTablas)
                ReDim Preserve XCT(NumeroTablas)
                Tablas(NumeroTablas) = Trim(CT.Tabla)
                EnlacesTablas(NumeroTablas) = Trim(CT.EnlaceATablaPadre)
                TablaPadre(NumeroTablas) = Trim(CT.TablaPadre)
                ContenedorTabla(NumeroTablas) = CNT
                XCT(NumeroTablas) = CT
                For i = 1 To 10 : XCT(NumeroTablas).Parametros(i) = Parametros(i) : Next
                XCT(NumeroTablas).NumTablaFormulario = NumeroTablas
                XCT(NumeroTablas).Contenedor = CNT
                CT.TabIndex = 9999
                CT.TabStop = False
            ElseIf LCase(C.GetType().Name) = "cnedicion" Then
                CE = DirectCast(C, CnEdicion.CnEdicion)
                Cadena = Trim(CE.Name)
                If Not IsNumeric(Microsoft.VisualBasic.Right(Cadena, 3)) Then
                    MensajeError = "Anote número de control CnEdicion incorrecto. " + Trim(CE.Name)
                    Throw New ArgumentException(MensajeError)
                Else
                    Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                    If Indice < 0 Then
                        MensajeError = "Anote número de control CnEdicion incorrecto (negativo). " + Trim(CE.Name)
                        Throw New ArgumentException(MensajeError)
                    ElseIf Indice <= UltimoControlCnEdicion Then
                        If Not (XCNE(Indice) Is Nothing) Then
                            MensajeError = "Anote número de control CnEdicion incorrecto (duplicado). " + Trim(CE.Name)
                            Throw New ArgumentException(MensajeError)
                        End If
                    End If
                    If Indice > UltimoControlCnEdicion Then
                        ReDim Preserve XCNE(Indice)
                        ReDim Preserve ContenedorCnEdicion(Indice)
                        UltimoControlCnEdicion = Indice
                    End If
                    XCNE(Indice) = CE
                    ContenedorCnEdicion(Indice) = CNT

                    CE.Contenedor(0) = CNT
                    For i = 1 To 10 : CE.Parametros(i) = Parametros(i) : Next
                    CE.TabIndex = 9999
                    CE.TabStop = False
                    CE.Visible = False
                End If

            End If
        ElseIf NumeroDeProceso = 2 Then
            If LCase(C.GetType().Name) = "textbox" And LCase(Microsoft.VisualBasic.Left(C.Name, 8)) = "txtdatos" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                CE = XCNE(Indice)
                If CE Is Nothing Then
                    MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                CE.TxtDatos(0) = C
            ElseIf LCase(C.GetType().Name) = "textbox" And LCase(Microsoft.VisualBasic.Left(C.Name, 9)) = "txtlookup" Then
                Cadena = Trim(C.Name)
                If Microsoft.VisualBasic.Len(Cadena) = 12 Then
                    Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                    CE = XCNE(Indice)
                    If CE Is Nothing Then
                        MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                        Throw New ArgumentException(MensajeError)
                    End If
                    CE.TxtLookup(1) = C
                    C.TabIndex = 9999
                    C.TabStop = False
                ElseIf Microsoft.VisualBasic.Len(Cadena) = 13 Then
                    Indice = CInt(Microsoft.VisualBasic.Mid(Cadena, 10, 3))
                    CE = XCNE(Indice)
                    If CE Is Nothing Then
                        MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                        Throw New ArgumentException(MensajeError)
                    End If
                    If Microsoft.VisualBasic.Right(Cadena, 1) <> "1" And Microsoft.VisualBasic.Right(Cadena, 1) <> "2" And Microsoft.VisualBasic.Right(Cadena, 1) <> "3" Then
                        MensajeError = "Anote control control TxtLookup incorrecto (" + Trim(CE.Name) + ")"
                        Throw New ArgumentException(MensajeError)
                    End If
                    CE.TxtLookup(CInt(Microsoft.VisualBasic.Right(Cadena, 1))) = C
                    C.TabIndex = 9999
                    C.TabStop = False
                End If
            ElseIf LCase(C.GetType().Name) = "button" And LCase(Microsoft.VisualBasic.Left(C.Name, 8)) = "cmdcombo" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                CE = XCNE(Indice)
                If CE Is Nothing Then
                    MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                CE.CmdCombo(0) = C
                'CE.EdicionEnCombo = True
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "button" And LCase(Microsoft.VisualBasic.Left(C.Name, 7)) = "cmdgrid" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                CE = XCNE(Indice)
                If CE Is Nothing Then
                    MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                CE.CmdGrid(0) = C
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "button" And LCase(Microsoft.VisualBasic.Left(C.Name, 8)) = "cmdfecha" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                CE = XCNE(Indice)
                If CE Is Nothing Then
                    MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                CE.CmdFecha(0) = C
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "button" And LCase(Microsoft.VisualBasic.Left(C.Name, 16)) = "cmdmantenimiento" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                CE = XCNE(Indice)
                If CE Is Nothing Then
                    MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                CE.CmdMantenimiento(0) = C
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "label" And LCase(Microsoft.VisualBasic.Left(C.Name, 3)) = "lbl" And Microsoft.VisualBasic.Len(C.Name) = 6 Then
                If IsNumeric(LCase(Microsoft.VisualBasic.Right(C.Name, 3))) Then
                    Cadena = Trim(C.Name)
                    Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                    CE = XCNE(Indice)
                    If CE Is Nothing Then
                        MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                        Throw New ArgumentException(MensajeError)
                    End If
                    CE.Etiqueta(0) = C
                End If
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "button" Or LCase(C.GetType().Name) = "label" Then
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "datagridview" And LCase(Microsoft.VisualBasic.Left(C.Name, 9)) = "gridtabla" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 2))
                FlagEncontrado = False
                For i = 1 To NumeroTablas
                    If Not XCT(i) Is Nothing Then
                        Cadena = Trim(XCT(i).Name)
                        Indice2 = CInt(Microsoft.VisualBasic.Right(Cadena, 2))
                        If Indice = Indice2 Then
                            If i > UBound(XGrid, 1) Then
                                ReDim Preserve XGrid(i)
                                ReDim Preserve ContenedorGrid(i)
                                XGrid(i) = DirectCast(C, DataGridView)
                                FlagEncontrado = True
                                XCT(i).Grid = XGrid(i)
                                XCT(i).HayGrid = True
                                ContenedorGrid(i) = CNT
                                If NumeroContenedor < 10 Then
                                    XCT(i).GridEnTabCabecera = True
                                    XCT(i).NumeroTabpageGrid = NumeroContenedor
                                Else
                                    XCT(i).GridEnTabCabecera = False
                                    XCT(i).NumeroTabpageGrid = NumeroContenedor - 10
                                End If
                            End If
                        End If
                    End If
                Next
                If FlagEncontrado = False Then
                    MensajeError = "No se ha encontrado objeto CnTabla para el grid: " + Trim(C.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                C.TabIndex = 9999
                C.TabStop = False
            End If
        End If
        IncluirControl = True
    End Function


    Public Function EsAntecesor(TablaActual As Integer, TablaQuizaPadre As Integer) As Boolean
        Dim i As Integer, j As Integer, FlagFin As Integer

        EsAntecesor = False
        i = TablaActual
        FlagFin = False
        Do While FlagFin = False
            j = XCT(i).NumTablaPadreFormulario
            If j = -1 Then
                FlagFin = True
            ElseIf j = TablaQuizaPadre Then
                EsAntecesor = True
                FlagFin = True
            Else
                i = XCT(i).NumTablaPadreFormulario
            End If
        Loop
    End Function
    Private Function QueTabla(CE As CnEdicion.CnEdicion) As CnTabla.CnTabla
        Dim i As Integer

        QueTabla = Nothing
        For i = 1 To NumeroTablas
            If Trim(XCT(i).Tabla) = Trim(CE.Tabla) Then
                QueTabla = XCT(i)
                Exit For
            End If
        Next

    End Function


    '    Public Function DevolverFiltros() As String
    '        Dim i As Integer, j As Integer, k As Integer, Cadena As String, DevolverParcial As String, DevolverActual As String
    '        Dim FiltroAGuardar(10) As String
    '        Dim QueT As Integer, TieneSeleccion(10) As Boolean, YaIncluido(10) As Boolean
    '        Dim AlgunaSeleccionada As Boolean, TieneHijo As Boolean, Padres(10) As Integer

    '        For i = 0 To NumeroTablas - 1
    '            TieneSeleccion(i) = False
    '            YaIncluido(i) = False
    '            FiltroAGuardar(i) = ""
    '        Next
    '        For i = 0 To NumeroTablas - 1
    '            If Trim(XCnTabla(i).SeleccionExistencia) <> "TODOS" Then 'Nuevo01032004
    '                TieneSeleccion(i) = True
    '                QueT = TablaPadre(i)
    '                Do While QueT <> -1
    '                    If TablaPadre(QueT) <> -1 Then
    '                        YaIncluido(QueT) = True
    '                        QueT = TablaPadre(QueT)
    '                    Else
    '                        QueT = -1
    '                    End If
    '                Loop
    '            End If
    '        Next

    '        DevolverFiltros = ""
    '        For i = 0 To NumeroTablas - 1
    '            If TieneSeleccion(i) = True And YaIncluido(i) = False Then
    '                j = 0 : Padres(j) = i
    '                QueT = TablaPadre(i)
    '                Do While QueT <> -1
    '                    If TablaPadre(QueT) <> -1 Then
    '                        j = j + 1 : Padres(j) = QueT
    '                        QueT = TablaPadre(QueT)
    '                    Else
    '                        QueT = -1
    '                    End If
    '                Loop
    '                DevolverParcial = ""
    '                For QueT = j To 0 Step -1
    '                    DevolverActual = ""
    '                    Cadena = Trim(XCnTabla(Padres(QueT)).SeleccionExistencia) 'Nuevo01032004
    '                    If Trim(Cadena) = "EXISTE" Or Trim(Cadena) = "TODOS" Or QueT > 0 Then
    '                        DevolverActual = " AND EXISTS(" + Trim(XCnTabla(Padres(QueT)).SubEnlace) 'Nuevo01032004
    '                    Else
    '                        DevolverActual = " AND NOT EXISTS(" + Trim(XCnTabla(Padres(QueT)).SubEnlace) 'Nuevo01032004
    '                    End If
    '                    Cadena = Trim(XCnTabla(Padres(QueT)).FiltroDefinido)
    '                    If Trim(Cadena) > "" Then
    '                        k = InStr(1, Cadena, "ORDER BY")
    '                        If k > 1 Then Cadena = " (" + Trim(Left(Cadena, k - 1)) + ")"
    '                        k = InStr(1, Cadena, "WHERE")
    '                        If k > 0 Then Cadena = " (" + Trim(Mid(Cadena, k + 5)) + ")"
    '                        DevolverActual = DevolverActual + " AND " + Trim(Cadena)
    '                    End If
    '                    For k = QueT + 1 To j
    '                        FiltroAGuardar(Padres(k)) = Trim(FiltroAGuardar(Padres(k))) + " " + Trim(DevolverActual)
    '                    Next
    '                    DevolverParcial = Trim(DevolverParcial) + " " + Trim(DevolverActual)
    '                Next
    '                If Trim(DevolverParcial) > "" Then
    '                    DevolverParcial = Trim(DevolverParcial) + String(j + 1, ")")
    '                    DevolverFiltros = Trim(DevolverFiltros) + " " + Trim(DevolverParcial)
    '                End If
    '                For k = 0 To j
    '                    FiltroAGuardar(Padres(k)) = Trim(FiltroAGuardar(Padres(k))) + String(k, ")")
    '                Next
    '            End If
    '        Next
    '        For i = 0 To NumeroTablas - 1
    '            XCnTabla(i).FiltroAuxiliar = Trim(FiltroAGuardar(i)) 'Nuevo01032004
    '        Next
    '        'MsgBox DevolverFiltros
    '    End Function

    '    Public Function LimpiarTodosDatos()
    '        Dim i As Integer

    '        For i = 0 To NumeroTablas - 1
    '            If XCnTabla(i).Ancestro = XCnTabla(TablaEnEdicion).Ancestro Then 'Nuevo01032004
    '                XCnTabla(i).LimpiarDatos True, True, True 'Nuevo01032004
    '                XCnTabla(i).LimpiarControles True 'Nuevo01032004
    '            End If
    '        Next
    '    End Function
    '    Public Sub Desplegar(C As Control, NumTabla As Integer)
    '        Dim i As Integer

    '        ModoSalida = ""
    '        If ClsBotones Is Nothing Then
    '    Set ClsBotones = CreateObject("proyectogen.clsbotones")
    '    Set FrmB = ClsBotones.FrmB
    'End If
    'Set FrmB.Formulario = Me
    'FrmB.NumTabla = NumTabla
    '        For i = 0 To NumeroTablas - 1
    '            FrmB.Tablas(i) = Tablas(i)
    '        Next
    'Set FrmB.Campo = C
    'If ModoSalida = "LISTADO" Then
    '    Set ObjetoLlamado = CreateObject("css86lis.ClsPreparaListadoN9")
    '    Set ObjetoLlamado.Formulario.ObjetoGlobal = Me.ObjetoGlobal
    '    ObjetoLlamado.Formulario.tablaprincipal = Tablas(NumTabla)
    '            ObjetoLlamado.Formulario.Show , Me
    '    ObjetoLlamado.Formulario.SetFocus
    '        End If
    '    End Sub
    '    Private Sub ManejoDeMensajes(Mensaje As String, Modulo As String, Procedimiento As String, MensajeDeError As Boolean)
    '        If MensajeDeError = False Then
    '            MsgBox Trim(Mensaje), vbOKOnly + vbExclamation + vbMsgBoxSetForeground, Trim(Modulo) + "(" + Trim(Procedimiento) + ")"
    'Else
    '            MsgBox Trim(Mensaje), vbOKOnly + vbCritical + vbMsgBoxSetForeground, Trim(Modulo) + "(" + Trim(Procedimiento) + ")"
    'End If
    '    End Sub
    '    Private Sub TxtDato_Change(Index As Integer)
    '        XCEdicion(Index).TxtDato_Change
    '    End Sub
    '    Private Sub TxtDato_GotFocus(Index As Integer)
    '        XCEdicion(Index).TxtDato_GotFocus
    '    End Sub
    '    Private Sub TxtDato_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
    '        XCEdicion(Index).TxtDato_KeyDown KeyCode, Shift
    'End Sub
    '    Private Sub TxtDato_KeyPress(Index As Integer, KeyAscii As Integer)
    '        XCEdicion(Index).TxtDato_KeyPress KeyAscii
    'End Sub
    '    Private Sub TxtDato_KeyUp(Index As Integer, KeyCode As Integer, Shift As Integer)
    '        XCEdicion(Index).TxtDato_KeyUp KeyCode, Shift
    'End Sub
    '    Private Sub TxtDato_LostFocus(Index As Integer)
    '        XCEdicion(Index).TxtDato_LostFocus
    '    End Sub
    '    Private Sub TxtDato_Validate(Index As Integer, Cancel As Boolean)
    '        Cancel = XCEdicion(Index).TxtDato_Validate
    '    End Sub
    '    Private Sub TxtLookup_GotFocus(Index As Integer)
    '        XCEdicion(Index).TxtLookup_GotFocus
    '    End Sub
    '    Private Sub TxtParaCombo_GotFocus(Index As Integer)
    '        XCEdicion(Index).TxtParaCombo_GotFocus
    '    End Sub
    '    Private Sub CmdCalendario_Click(Index As Integer)
    '        XCEdicion(Index).CmdCalendario_Click
    '    End Sub
    '    Private Sub CmdGrid_Click(Index As Integer)
    '        XCEdicion(Index).CmdGrid_Click
    '    End Sub
    '    Private Sub CmdFicha_Click(Index As Integer)
    '        XCEdicion(Index).CmdFicha_Click
    '    End Sub
    '    Private Sub CmdCombo_Click(Index As Integer)
    '        XCEdicion(Index).CmdCombo_Click
    '    End Sub
    '    Public Property Get XCT(Index As Integer) 'Nuevo01032004
    '        Set XCT = XCnTabla(Index) 'Nuevo01032004
    'End Property 'Nuevo01032004


    'Private Sub Comprobar()
    '    Dim C As Control, D As Control
    '    Dim i As Integer, j As Integer



    '    For Each C In Me.PanelSuperior.Controls
    '        ' Debug.print(C.Name)
    '        ' Debug.print(C.GetType().Name)
    '        Try
    '            ' Debug.print(CStr(C.TabIndex))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABINDEX")
    '        End Try
    '        Try
    '            ' Debug.print(CStr(C.TabStop))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABSTOP")
    '        End Try
    '    Next


    '    Try
    '        ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabCabecera").TabIndex))
    '    Catch ex As Exception
    '        ' Debug.print("NO TABINDEX")
    '    End Try
    '    Try
    '        ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabCabecera").TabStop))
    '    Catch ex As Exception
    '        ' Debug.print("NO TABSTOP")
    '    End Try



    '    For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
    '        ' Debug.print(D.Name)
    '        ' Debug.print(D.GetType().Name)
    '        Try
    '            ' Debug.print(CStr(D.TabIndex))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABINDEX")
    '        End Try
    '        Try
    '            ' Debug.print(CStr(D.TabStop))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABSTOP")
    '        End Try

    '        If LCase(D.GetType().Name) = "tabpage" Then
    '            For Each C In D.Controls
    '                ' Debug.print(C.Name)
    '                ' Debug.print(C.GetType().Name)
    '                Try
    '                    ' Debug.print(CStr(C.TabIndex))
    '                Catch ex As Exception
    '                    ' Debug.print("NO TABINDEX")
    '                End Try
    '                Try
    '                    ' Debug.print(CStr(C.TabStop))
    '                Catch ex As Exception
    '                    ' Debug.print("NO TABSTOP")
    '                End Try
    '            Next
    '        End If
    '    Next

    '    Try
    '        ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabGeneral").TabIndex))
    '    Catch ex As Exception
    '        ' Debug.print("NO TABINDEX")
    '    End Try
    '    Try
    '        ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabGeneral").TabStop))
    '    Catch ex As Exception
    '        ' Debug.print("NO TABSTOP")
    '    End Try


    '    For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
    '        ' Debug.print(D.Name)
    '        ' Debug.print(D.GetType().Name)
    '        Try
    '            ' Debug.print(CStr(D.TabIndex))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABINDEX")
    '        End Try
    '        Try
    '            ' Debug.print(CStr(D.TabStop))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABSTOP")
    '        End Try

    '        If LCase(D.GetType().Name) = "tabpage" Then
    '            For Each C In D.Controls
    '                ' Debug.print(C.Name)
    '                ' Debug.print(C.GetType().Name)
    '                Try
    '                    ' Debug.print(CStr(C.TabIndex))
    '                Catch ex As Exception
    '                    ' Debug.print("NO TABINDEX")
    '                End Try
    '                Try
    '                    ' Debug.print(CStr(C.TabStop))
    '                Catch ex As Exception
    '                    ' Debug.print("NO TABSTOP")
    '                End Try
    '            Next
    '        End If
    '    Next

    '    For Each C In Me.PanelInferior.Controls
    '        ' Debug.print(C.Name)
    '        ' Debug.print(C.GetType().Name)
    '        Try
    '            ' Debug.print(CStr(C.TabIndex))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABINDEX")
    '        End Try
    '        Try
    '            ' Debug.print(CStr(C.TabStop))
    '        Catch ex As Exception
    '            ' Debug.print("NO TABSTOP")
    '        End Try
    '    Next

    '    'For i = 1 To 2
    '    '    For j = 0 To 9
    '    '        Debug.Print(TabsUsados(i, j))
    '    '    Next
    '    'Next



    'End Sub

    'Private Sub CmdComprobar_Click(sender As Object, e As EventArgs) Handles CmdComprobar.Click
    '    Static jj As Integer = 0

    '    If jj = 0 Then
    '        CmdComprobar.Text = "Moccasin"
    '        GridTabla01.DefaultCellStyle.SelectionBackColor = Color.Moccasin
    '    ElseIf jj = 1 Then
    '        CmdComprobar.Text = "NavajoWhite"
    '        GridTabla01.DefaultCellStyle.SelectionBackColor = Color.NavajoWhite
    '    ElseIf jj = 2 Then
    '        CmdComprobar.Text = "Wheat"
    '        GridTabla01.DefaultCellStyle.SelectionBackColor = Color.Wheat
    '    End If

    '    jj = jj + 1
    '    If jj = 3 Then jj = 0
    'End Sub

    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    Me.Text = MousePosition.ToString
    'End Sub



    ' ==== Eventos CnTabla  =====

    Private Sub CnTabla_CTSeleccion(sender As Object, e As EventArgs, CT As CnTabla.CnTabla)
        Dim CNT As Control, Retorno As String

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub

        CNT = XCT(CT.NumTablaFormulario).Contenedor
        If LCase(Microsoft.VisualBasic.Left(CNT.Name, 7)) = "tabpage" Then
            TabGeneral.SelectedTab = CNT
        End If

        If AntesDeSeleccionar1(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.PasandoASeleccionar, CT.NumTablaFormulario)

        If AntesDeSeleccionar2(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.Seleccionando, CT.NumTablaFormulario)

        If CT.HayGrid = True Then
            GridEdicion.Actividad = GridEdicion.ActividadDelGrid.Seleccionando
            GridEdicion.CT = CT
            GridEdicion.PosicionInicial = CalcularPosicionGridEdicion(CT.Grid)
            GridEdicion.WidthInicial = CT.Grid.Width - 100
            AddOwnedForm(GridEdicion)
            GridEdicion.ShowDialog()

            'Si la tabla se edita en modo grid, el CmdAceptar (y el CmdCancelar) corresponden al Objeto GridEdicion. 
            ' El código que sigue ejecuta las acciones asociadas a aceptar (o cancelar) el proceso de creación.
            If GridEdicion.Actividad = GridEdicion.ActividadDelGrid.SeleccionAceptada Then
                If AntesDeAceptarSeleccion(Me, CT) = False Then
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                    Exit Sub
                End If
                Retorno = ""
                CambioSituacionFormulario(Estados.AceptandoSeleccion, CT.NumTablaFormulario, Retorno)
                If Retorno = "Registros seleccionados" Then
                    If RegistrosSeleccionados(Me, CT) = False Then
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                        Exit Sub
                    Else
                        CT.MostrarRegistrosEnGrid()

                        CambioSituacionFormulario(Estados.Inactivo, -1)
                        CT.Primero(True)
                    End If
                ElseIf Retorno = "No hay registros" Then
                    If NoHayRegistros(Me, CT) = False Then
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                        Exit Sub
                    Else
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                    End If

                Else 'Fin anómalo de la selección
                    MsgBox("Se ha producido un error en el proceso de selección.")
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                    Exit Sub
                End If
                CambioSituacionFormulario(Estados.Inactivo, -1)
            ElseIf GridEdicion.Actividad = GridEdicion.ActividadDelGrid.CreacionCancelada Then
                CambioSituacionFormulario(Estados.Inactivo, -1)
            End If

        End If

    End Sub

    Private Sub CnTabla_CTAnadir(sender As Object, e As EventArgs, CT As CnTabla.CnTabla)
        Dim CNT As Control

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        CNT = XCT(CT.NumTablaFormulario).Contenedor
        If LCase(Microsoft.VisualBasic.Left(CNT.Name, 7)) = "tabpage" Then
            TabGeneral.SelectedTab = CNT
        End If

        If AntesDeCrear1(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.PasandoACrear, CT.NumTablaFormulario)

        If AntesDeCrear2(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.Creando, CT.NumTablaFormulario)

        If CT.HayGrid = True Then
            GridEdicion.Actividad = GridEdicion.ActividadDelGrid.Creando
            GridEdicion.CT = CT
            GridEdicion.PosicionInicial = CalcularPosicionGridEdicion(CT.Grid)
            GridEdicion.WidthInicial = CT.Grid.Width - 100
            GridEdicion.ShowDialog()

            'Si la tabla se edita en modo grid, el CmdAceptar (y el CmdCancelar) corresponden al Objeto GridEdicion. 
            ' El código que sigue ejecuta las acciones asociadas a aceptar (o cancelar) el proceso de creación.
            If GridEdicion.Actividad = GridEdicion.ActividadDelGrid.CreacionAceptada Then
                If AntesDeGrabarCreacion(Me, CT) = False Then
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                    Exit Sub
                End If
                CambioSituacionFormulario(Estados.AceptandoCreacion, CT.NumTablaFormulario)
                CT.IncluirRegistroGrabadoEnGrid()
                DespuesDeGrabarCreacion(Me, CT)
                CambioSituacionFormulario(Estados.Inactivo, -1)
            ElseIf GridEdicion.Actividad = GridEdicion.ActividadDelGrid.CreacionCancelada Then
                CambioSituacionFormulario(Estados.Inactivo, -1)
            End If

        End If

    End Sub


    Private Sub CnTabla_CTModificar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla)
        Dim CNT As Control, Rs As New cnRecordset.CnRecordset

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        If CT.CuantosRegistros <= 0 Then
            MsgBox("Debe seleccionar el registro a modificar.")
            Exit Sub
        End If

        CNT = XCT(CT.NumTablaFormulario).Contenedor
        If LCase(Microsoft.VisualBasic.Left(CNT.Name, 7)) = "tabpage" Then
            TabGeneral.SelectedTab = CNT
        End If

        If AntesDeModificar1(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.PasandoAModificar, CT.NumTablaFormulario)

        If AntesDeModificar2(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.Modificando, CT.NumTablaFormulario)

    End Sub


    Private Sub CnTabla_CTAceptar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla)
        'Dim Mensaje As String, RegistroAnterior As Long, XT As Integer
        'Dim FlagExito As Boolean
        Dim Retorno As String

        If SituacionFormulario = Estados.Seleccionando Then


            'If AntesDeSeleccionar1(Me, CT) = False Then
            '    CambioSituacionFormulario(Estados.Inactivo, -1)
            '    Exit Sub
            'End If
            'CambioSituacionFormulario(Estados.PasandoASeleccionar, CT.NumTablaFormulario, Retorno)

            'If Trim(Retorno) = "No ha registros" Then
            '    If DespuesDeCargarRegistrosNOHAY(Me, CT) Then
            '        CambioSituacionFormulario(Estados.Inactivo, -1)
            '        Exit Sub
            '    End If
            'Else
            '    If DespuesDeCargarRegistros(Me, CT) Then
            '        CambioSituacionFormulario(Estados.Inactivo, -1)
            '        Exit Sub
            '    End If
            'End If

            'If DespuesDeCargarSeleccion1(Me, CT) = False Then
            '    CambioSituacionFormulario(Estados.Inactivo, -1)
            '    Exit Sub
            'End If

            'CambioSituacionFormulario(Estados.Seleccionando, CT.NumTablaFormulario)

            'If DespuesDeCargarSeleccion1(Me, CT) = False Then
            '    CambioSituacionFormulario(Estados.Inactivo, -1)
            '    Exit Sub
            'End If

            'CambioSituacionFormulario(Estados.Inactivo, -1)


            If TablaEnEdicion = CT.NumTablaFormulario Then

                If AntesDeAceptarSeleccion(Me, CT) = False Then
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                    Exit Sub
                End If
                Retorno = ""
                CambioSituacionFormulario(Estados.AceptandoSeleccion, CT.NumTablaFormulario, Retorno)

                If Retorno = "Registros seleccionados" Then
                    If RegistrosSeleccionados(Me, CT) = False Then
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                        Exit Sub
                    Else
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                        CT.Primero(True)
                    End If

                ElseIf Retorno = "No hay registros" Then
                    If NoHayRegistros(Me, CT) = False Then
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                        Exit Sub
                    Else
                        CambioSituacionFormulario(Estados.Inactivo, -1)
                    End If

                Else 'Fin anómalo de la selección
                    MsgBox("Se ha producido un error en el proceso de selección.")
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                    Exit Sub
                End If


                '        If m_TablaActiva = False Then Exit Sub
                '        m_EstadoControl = AceptandoSeleccion
                '        If m_NumeroTab = -1 Then
                '            If Parent.SSTabPrincipal.TabVisible(0) = True Then Parent.SSTabPrincipal.Tab = 0
                '        Else
                '            If m_NumeroTab > 10 Then
                '                XT = Int(m_NumeroTab / 10)
                '            Else
                '                XT = m_NumeroTab
                '            End If
                '            If XT <> Parent.SSTabPrincipal.Tab Then
                '                Parent.SSTabPrincipal.Tab = XT
                '            End If
                '            Parent.SSTabPrincipal.Tab = XT
                '        End If
                '        Parent.CambioSituacionFormulario CInt(AceptandoSeleccion), m_NumTablaEnFormulario
                'Screen.MousePointer = vbHourglass
                '        If EstablecerSeleccion = True Then
                '            Screen.MousePointer = vbDefault
                '            Parent.CambioSituacionFormulario CInt(Inactivo), m_NumTablaEnFormulario

                '    If m_TipoMantenimiento = "G" Then
                '                m_GridAsociado.MostrarRegistros m_Rs(1)
                '        m_GridAsociado.ConBarras
                '            End If
                '            Primero True
                'Else
                '            Screen.MousePointer = vbDefault
                '            MsgBox "No existen registros que cumplan la selección actual."
                '    Parent.LimpiarTodosDatos
                '            Parent.CambioSituacionFormulario CInt(Inactivo), m_NumTablaEnFormulario
                '    If m_TipoMantenimiento = "G" Then
                '                m_GridAsociado.ConBarras
                '            End If
                '        End If
                '        EstablecerControlesEnSeleccion False
                'ElseIf m_EstadoControl = Filtrando Then
                '            If m_TablaActiva = False Then Exit Sub
                '            If m_NumeroTab = -1 Then
                '                If Parent.SSTabPrincipal.TabVisible(0) = True Then Parent.SSTabPrincipal.Tab = 0
                '            Else
                '                If m_NumeroTab > 10 Then
                '                    XT = Int(m_NumeroTab / 10)
                '                Else
                '                    XT = m_NumeroTab
                '                End If
                '                If XT <> Parent.SSTabPrincipal.Tab Then
                '                    Parent.SSTabPrincipal.Tab = XT
                '                End If
                '                Parent.SSTabPrincipal.Tab = XT
                '            End If
                '            Parent.CambioSituacionFormulario CInt(AceptandoFiltro), m_NumTablaEnFormulario
                '    SQLFiltro = EstablecerFiltro
                '            If Parent.ControlTabla(Ancestro).Cuantos <= 0 Then
                '                Parent.LimpiarTodosDatos
                '            Else
                '                Parent.ControlTabla(Ancestro).EsteRegistro Parent.ControlTabla(Ancestro).RegistroActual, True
                '    End If
                '            Parent.CambioSituacionFormulario CInt(Inactivo), 0
                '    If m_TipoMantenimiento = "G" Then
                '                m_GridAsociado.ConBarras
                '            End If
                '            EstablecerControlesEnSeleccion False
            End If
        ElseIf SituacionFormulario = Estados.Modificando Then
            If TablaEnEdicion = CT.NumTablaFormulario Then
                If AntesDeAceptarModificacion(Me, CT) = False Then
                    DespuesDeFracasarAceptarModificacion(Me, CT)
                    Exit Sub
                End If
                If CT.ComprobacionesAntesDeGrabar(False) = False Then
                    DespuesDeFracasarAceptarModificacion(Me, CT)
                    Exit Sub
                End If

                If AntesDeGrabarModificacion(Me, CT) = False Then
                    DespuesDeFracasarAceptarModificacion(Me, CT)
                    Exit Sub
                End If
                CambioSituacionFormulario(Estados.AceptandoModificacion, CT.NumTablaFormulario)
                DespuesDeGrabarModificacion(Me, CT)

                CambioSituacionFormulario(Estados.Inactivo, -1)


                '            If m_TablaActiva = False Then Exit Sub
                '            DespuesDeAceptarModificacion m_NumTablaEnFormulario, Parent, Me
                '    Parent.CambioSituacionFormulario CInt(AceptandoModificacion), m_NumTablaEnFormulario
                '    If CamposRequeridosOK = False Then
                '                Parent.CambioSituacionFormulario CInt(Modificando), m_NumTablaEnFormulario
                '        Exit Sub
                '            End If
                '            If CamposConRestriccionOK = False Then
                '                Parent.CambioSituacionFormulario CInt(Modificando), m_NumTablaEnFormulario
                '        Exit Sub
                '            End If
                '            If TodasClavesOK = False Then
                '                Parent.CambioSituacionFormulario CInt(Modificando), m_NumTablaEnFormulario
                '        Exit Sub
                '            End If
                '            If ComprobacionAntesDeGrabarModificacion(m_NumTablaEnFormulario, Parent, Me) = True Then
                '                Parent.CambioSituacionFormulario CInt(Modificando), m_NumTablaEnFormulario
                '        Exit Sub
                '            End If
                '            FlagExito = Not (AntesDeGrabarModificacion(m_NumTablaEnFormulario, Parent, Me))
                '            If FlagExito = True Then FlagExito = ActualizarDatos(Mensaje)
                '            If FlagExito = False Then
                '                DespuesDeFracasarModificacion m_NumTablaEnFormulario, Parent, Me
                '        ManejoDeMensajes "No ha sido posible actualizar el registro en la Base de Datos." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + vbCrLf + Trim(Mensaje), "Ctabla", "CmdAceptar_Click", True
                '        Parent.CambioSituacionFormulario CInt(Inactivo), m_NumTablaEnFormulario
                '        If Parent.ControlTabla(Ancestro).Cuantos <= 0 Then
                '                    Parent.LimpiarTodosDatos
                '                Else
                '                    Parent.ControlTabla(Ancestro).EsteRegistro Parent.ControlTabla(Ancestro).RegistroActual, True
                '        End If
                '                If m_TipoMantenimiento = "G" Then
                '                    m_GridAsociado.ConBarras
                '                End If
                '                Exit Sub
                '            Else
                '                DespuesDeGrabarModificacion m_NumTablaEnFormulario, Parent, Me
                '        RegistroModificado
                '                If TipoMantenimiento = "G" Then m_GridAsociado.MostrarUnRegistro RegistroActual
                '        EsteRegistro RegistroActual, False
                '        If m_TipoMantenimiento = "G" Then
                '                    m_GridAsociado.ConBarras
                '                End If
                '            End If
                '            Parent.CambioSituacionFormulario CInt(Inactivo), 0
                '    DespuesDeModificar Me, Parent
            End If

        ElseIf SituacionFormulario = Estados.Creando Then
            If TablaEnEdicion = CT.NumTablaFormulario Then
                If AntesDeAceptarCreacion(Me, CT) = False Then
                    DespuesDeFracasarAceptarCreacion(Me, CT)
                    Exit Sub
                End If

                If CT.ComprobacionesAntesDeGrabar(False) = False Then
                    DespuesDeFracasarAceptarCreacion(Me, CT)
                    Exit Sub
                End If

                If AntesDeGrabarCreacion(Me, CT) = False Then
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                    Exit Sub
                End If
                CambioSituacionFormulario(Estados.AceptandoCreacion, CT.NumTablaFormulario)

                DespuesDeGrabarCreacion(Me, CT)
                CambioSituacionFormulario(Estados.Inactivo, -1)
            End If
        End If
    End Sub


    Private Sub CnTabla_CTCancelar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla)
        'Dim Mensaje As String, RegistroAnterior As Long, XT As Integer
        'Dim FlagExito As Boolean
        Dim SQL As String

        If SituacionFormulario = Estados.Seleccionando Then
            If TablaEnEdicion = CT.NumTablaFormulario Then

                If AntesDeCancelarSeleccion(Me, CT) = False Then
                End If
                CambioSituacionFormulario(Estados.CancelandoSeleccion, CT.NumTablaFormulario)

                If DespuesDeCancelarSeleccion(Me, CT) = False Then
                End If

                CambioSituacionFormulario(Estados.Inactivo, -1)
            End If

        ElseIf SituacionFormulario = Estados.Modificando Then
            If TablaEnEdicion = CT.NumTablaFormulario Then

                If AntesDeCancelarModificacion1(Me, CT) = True Then
                    CambioSituacionFormulario(Estados.CancelandoModificacion, CT.NumTablaFormulario)
                Else
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                End If
                If AntesDeCancelarModificacion2(Me, CT) = False Then
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                End If
                CambioSituacionFormulario(Estados.Inactivo, -1)
                DespuesDeCancelarModificacion(Me, CT)


            End If

        ElseIf SituacionFormulario = Estados.Creando Then

            If TablaEnEdicion = CT.NumTablaFormulario Then

                If AntesDeCancelarCreacion1(Me, CT) = True Then
                    CambioSituacionFormulario(Estados.CancelandoCreacion, CT.NumTablaFormulario)
                Else
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                End If

                If AntesDeCancelarCreacion2(Me, CT) = False Then
                    CambioSituacionFormulario(Estados.Inactivo, -1)
                End If

                CambioSituacionFormulario(Estados.Inactivo, -1)

                DespuesDeCancelarCreacion(Me, CT)
            End If



            '            CT.LimpiarDatosYControles()
        End If
    End Sub


    Private Sub CnTabla_CTEliminar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla)
        Dim CNT As Control

        If SituacionFormulario <> Estados.Inactivo Then Exit Sub
        If CT.CuantosRegistros <= 0 Then
            MsgBox("Debe seleccionar el registro a eliminar.")
            Exit Sub
        End If

        CNT = XCT(CT.NumTablaFormulario).Contenedor
        If LCase(Microsoft.VisualBasic.Left(CNT.Name, 7)) = "tabpage" Then
            TabGeneral.SelectedTab = CNT
        End If


        If AntesDeEliminar1(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Inactivo, -1)
            Exit Sub
        End If
        CambioSituacionFormulario(Estados.PasandoAEliminar, CT.NumTablaFormulario)

        If AntesDeEliminar2(Me, CT) = False Then
            CambioSituacionFormulario(Estados.Eliminando, CT.NumTablaFormulario)
            Exit Sub
        End If

        If MsgBox("¿Desea eliminar este registro?", vbYesNo, "Borrado de registro de la tabla " + Trim(CT.Tabla)) = vbYes Then
            If AntesDeAceptarEliminacion(Me, CT) = False Then
                CambioSituacionFormulario(Estados.Inactivo, -1)
                Exit Sub
            End If
            CambioSituacionFormulario(Estados.AceptandoEliminacion, CT.NumTablaFormulario)

            DespuesDeEliminar(Me, CT)
        Else
            If AntesDeCancelarEliminacion(Me, CT) = False Then
                CambioSituacionFormulario(Estados.Inactivo, -1)
                Exit Sub
            End If
            CambioSituacionFormulario(Estados.CancelandoEliminacion, CT.NumTablaFormulario)

            DespuesDeCancelarEliminacion(Me, CT)
        End If
        CambioSituacionFormulario(Estados.Inactivo, -1)

    End Sub

    Private Sub CnTabla_MostrarLookup(CT As CnTabla.CnTabla, TxtLK As System.Windows.Forms.TextBox, Valor As String)
        TxtLK.Text = Trim(Valor)
    End Sub


    ' ==== Eventos TxtDatos y TTCell  =====

    Private Sub TxtDatos_TextChanged(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_TextChanged " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CETextChanged(sender, e)
    End Sub
    Private Sub TTCell_TextChanged(sender As Object, e As EventArgs) Handles TTCell.TextChanged
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_TextChanged " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CETextChanged(sender, e)
    End Sub


    Private Sub TxtDatos_GotFocus(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_GotFocus " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEGotFocus(sender, e)
    End Sub
    Private Sub TTCell_GotFocus(sender As Object, e As EventArgs) Handles TTCell.GotFocus
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_GotFocus " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEGotFocus(sender, e)
    End Sub


    Private Sub TxtDatos_LostFocus(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_LostFocus " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CELostFocus(sender, e)
    End Sub
    Private Sub TTCell_LostFocus(sender As Object, e As EventArgs) Handles TTCell.LostFocus
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_LostFocus " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CELostFocus(sender, e)
    End Sub


    Private Sub TxtDatos_Validating(sender As Object, e As CancelEventArgs)
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Validating " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        If CE.Situacion(0) <> CnEdicion.CnEdicion.SituacionControl.Creacion And CE.Situacion(0) <> CnEdicion.CnEdicion.SituacionControl.Modificacion Then Exit Sub

        e.Cancel = Not (CE.CEValidating(CE.TxtDatos(0)))
        'CE.CEValidating(sender, e)

        If e.Cancel = False Then
            CT = XCT(CE.NumeroTablaFormulario(0))
            e.Cancel = Not (CT.ValidacionCampo(CE, DirectCast(sender, TextBox).Text, (SituacionFormulario = Estados.Creando)))
        End If
    End Sub
    Private Sub TTCell_Validating(sender As Object, e As CancelEventArgs) Handles TTCell.Validating
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Validating " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)

        CE.CEValidating(CE.TxtDatos(0))
        'CE.CEValidating(sender, e)

        If e.Cancel = False Then
            CT = XCT(CE.NumeroTablaFormulario(0))
            e.Cancel = Not (CT.ValidacionCampo(CE, DirectCast(sender, TextBox).Text, (SituacionFormulario = Estados.Creando)))
        End If
    End Sub


    Private Sub TxTDatos_Validated(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Validated " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEValidated(CE.TxtDatos(0))
    End Sub
    Private Sub TTCEll_Validated(sender As Object, e As EventArgs) Handles TTCell.Validated
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Validated " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEValidated(CE.TxtDatos(0))
    End Sub


    Private Sub TxtDatos_KeyDown(sender As Object, e As KeyEventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyDown " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyDown(sender, e)
    End Sub
    Private Sub TTCell_KeyDown(sender As Object, e As KeyEventArgs) Handles TTCell.KeyDown
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyDown " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyDown(sender, e)
    End Sub


    Private Sub TxtDatos_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyPress " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyPress(sender, e)
    End Sub
    Private Sub TTCell_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TTCell.KeyPress
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyPress " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyPress(sender, e)
    End Sub



    Private Sub TxtDatos_KeyUp(sender As Object, e As KeyEventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyUp " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyUp(sender, e)
    End Sub
    Private Sub TTCell_KeyUp(sender As Object, e As KeyEventArgs) Handles TTCell.KeyUp
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyUp " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyUp(sender, e)
    End Sub



    Private Sub TxtDatos_MouseClick(sender As Object, e As MouseEventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_MouseClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEMouseClick(sender, e)
    End Sub
    Private Sub TTCell_MouseClick(sender As Object, e As MouseEventArgs) Handles TTCell.MouseClick
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_MouseClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEMouseClick(sender, e)
    End Sub



    Private Sub TxtDatos_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_MouseDoubleClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEMouseDoubleClick(sender, e)
    End Sub
    Private Sub TTCell_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TTCell.MouseDoubleClick
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_MouseDoubleClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEMouseDoubleClick(sender, e)
    End Sub



    Private Sub TxtDatos_Click(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Click " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDoubleClick(sender, e)
    End Sub
    Private Sub TTCell_Click(sender As Object, e As EventArgs) Handles TTCell.Click
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Click " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDoubleClick(sender, e)
    End Sub



    Private Sub TxtDatos_Disposed(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        'Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Disposed " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDisposed(sender, e)
    End Sub
    Private Sub TTCell_Disposed(sender As Object, e As EventArgs) Handles TTCell.Disposed
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Disposed " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDisposed(sender, e)
    End Sub



    Private Sub TxtDatos_DoubleClick(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_DoubleClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDoubleClick(sender, e)
    End Sub
    Private Sub TTCell_DoubleClick(sender As Object, e As EventArgs) Handles TTCell.DoubleClick
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_DoubleClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDoubleClick(sender, e)
    End Sub



    Private Sub TxtDatos_Enter(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Enter " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEEnter(sender, e)
    End Sub
    Private Sub TTCell_Enter(sender As Object, e As EventArgs) Handles TTCell.Enter
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Enter " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEEnter(sender, e)
    End Sub



    Private Sub TxtDatos_Leave(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Leave " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CELeave(sender, e)
    End Sub
    Private Sub TTCell_Leave(sender As Object, e As EventArgs) Handles TTCell.Leave
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Leave " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CELeave(sender, e)
    End Sub




    Private Sub TxtDatos_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs)
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_PreviewKeyDown " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEPreviewKeyDown(sender, e)
    End Sub
    Private Sub TTCell_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TTCell.PreviewKeyDown
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_PreviewKeyDown " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEPreviewKeyDown(sender, e)

    End Sub

    ' ==== Eventos Botones  =====

    Private Sub CmdFecha_Click(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer, P As Point

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        P = CE.TxtDatos(0).PointToScreen(New Point)
        FrmFecha.X = P.X
        FrmFecha.Y = P.Y

        If CE.TipoDeDato(0) = CnEdicion.CnEdicion.Tipo_de_dato.Fecha Then
            FrmFecha.EsFechaHora = False
        ElseIf CE.TipoDeDato(0) = CnEdicion.CnEdicion.Tipo_de_dato.FechaHora Then
            FrmFecha.EsFechaHora = True
        Else
            MsgBox("Tipo de dato incorrecto para un botón de fecha")
            Exit Sub
        End If

        FrmFecha.ShowDialog()
        If FrmFecha.HayResultado = True Then
            CT = XCT(CE.NumeroTablaFormulario(0))
            CT.AsignarValor(CE.Campo, CStr(FrmFecha.FechaResultado))
        End If
    End Sub

    Private Sub CmdCombo_Click(sender As Object, e As EventArgs)
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer, P As Point

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        P = CE.TxtDatos(0).PointToScreen(New Point)
        FrmLV.X = P.X - 5
        FrmLV.Y = P.Y - 5
        FrmLV.CE = CE

        FrmLV.ShowDialog()
        If FrmLV.HayResultado = True Then
            CT = XCT(CE.NumeroTablaFormulario(0))
            CT.AsignarValor(CE.Campo, Trim(CE.ValoresBDRestriccion(FrmLV.IndiceResultado)))
        End If
    End Sub
    Private Sub CmdGrid_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub CmdMantenimiento_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " leave " + sender.name + " " + DirectCast(sender, TextBox).Text)
    End Sub

    Private Sub TextBox1_Validating(sender As Object, e As CancelEventArgs) Handles TextBox1.Validating
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " validating " + sender.name + " " + DirectCast(sender, TextBox).Text)
        If Trim(TextBox1.Text) > "" Then e.Cancel = True
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " lostfocus " + sender.name + " " + DirectCast(sender, TextBox).Text)
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " leave " + sender.name + " " + DirectCast(sender, TextBox).Text)
    End Sub

    Private Sub TextBox2_Validating(sender As Object, e As CancelEventArgs) Handles TextBox2.Validating
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " validating " + sender.name + " " + DirectCast(sender, TextBox).Text)
        If Trim(TextBox2.Text) > "" Then e.Cancel = True
    End Sub

    Private Sub TextBox2_LostFocus(sender As Object, e As EventArgs) Handles TextBox2.LostFocus
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " lostfocus " + sender.name + " " + DirectCast(sender, TextBox).Text)
    End Sub




    Private Function CalcularPosicionGridEdicion(CTGrid As DataGridView) As Point
        Dim X1 As Integer, Y1 As Integer

        X1 = Me.Location.X + PanelCentral.Left + TabGeneral.Left + CTGrid.Left + 30
        Y1 = Me.Location.Y + PanelCentral.Top + TabGeneral.Top + CTGrid.Top + CTGrid.Height - 140
        CalcularPosicionGridEdicion.X = X1
        CalcularPosicionGridEdicion.Y = Y1

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.CausesValidation = True Then
            TextBox2.CausesValidation = False
            Button1.BackColor = Color.Green
        Else
            TextBox2.CausesValidation = True
            Button1.BackColor = Color.Red
        End If
    End Sub

    Private Sub Button1_Enter(sender As Object, e As EventArgs) Handles Button1.Enter
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " ENTER DEL BOTÓN ")

    End Sub



    Private Sub FrmPrueba_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " closing ")
        e.Cancel = False
    End Sub

End Class

