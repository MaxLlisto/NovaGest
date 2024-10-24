Imports System.ComponentModel
Imports CnTabla
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop.Excel 'Depende del COM Microsoft Excel 12.0
Imports System.Data



Public Class Mantenimiento20
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

    Private TabsUsados(2, 9) As Boolean

    Private SituacionFormulario As Estados = Estados.Inicial
    Private TablaEnEdicion As Integer = -1

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


    'Private TipoMantenimiento(10) As String
    '    Private XMshFicha() As CFlex
    '    Private XCEdicion() As CEdicion
    '    Private XCnTabla() As CnTabla
    '    Private Parametros(10) As String


    Dim ContadorTraza As Integer = 0



    Private Sub Mantenimiento2_Load(sender As Object, e As EventArgs) Handles Me.Load
        TabGeneral.SelectedTab = TabGeneral.TabPages(0)
        '        AccionParaFoco = ""
        AsignarParametros()
        CrearArrayDeControles()
        HacerInvisiblesTabsNoUsados()
        RehacerTabIndex()
        InicializarControles()
        CambioSituacionFormulario(Estados.Inactivo, -1)
        'MsgBox(CnEdicion002.SoloLectura)
        'EdgUnMomento.Visible = False
        '        AlAbrirFormulario Me

        'CnTabla01.Enabled = True
        'TxtDatos002.ReadOnly = True
        'TxtDatos003.Enabled = False


    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

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
            ' Debug.print(C.Name)
            ' Debug.print(C.GetType().Name)
            TabsUsados(0, 0) = True
            IncluirControl(C, Me.PanelSuperior, 1, 0)
        Next

        Me.PanelCentral.Controls.Item("TabCabecera").TabIndex = 10
        Me.PanelCentral.Controls.Item("TabCabecera").TabStop = False

        For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
            ' Debug.print(D.Name)
            ' Debug.print(D.GetType().Name)
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                D.TabIndex = 10 * (NT + 1)
                D.TabStop = False
                For Each C In D.Controls
                    ' Debug.print(C.Name)
                    ' Debug.print(C.GetType().Name)
                    TabsUsados(1, NT) = True
                    IncluirControl(C, D, 1, NT)
                Next
            End If
        Next

        Me.PanelCentral.Controls.Item("TabGeneral").TabIndex = 1000
        Me.PanelCentral.Controls.Item("TabGeneral").TabStop = False

        For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
            ' Debug.print(D.Name)
            ' Debug.print(D.GetType().Name)
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                D.TabIndex = 1000 + 10 * NT
                D.TabStop = False
                For Each C In D.Controls
                    NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                    ' Debug.print(C.Name)
                    ' Debug.print(C.GetType().Name)
                    TabsUsados(2, NT) = True
                    IncluirControl(C, D, 1, 10 + NT)
                Next
            End If
        Next

        'Resto de controles
        For Each C In Me.PanelSuperior.Controls
            ' Debug.print(C.Name)
            ' Debug.print(C.GetType().Name)
            TabsUsados(0, 0) = True
            IncluirControl(C, Me.PanelSuperior, 2, 0)
        Next
        For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
            ' Debug.print(D.Name)
            ' Debug.print(D.GetType().Name)
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                For Each C In D.Controls
                    ' Debug.print(C.Name)
                    ' Debug.print(C.GetType().Name)
                    TabsUsados(1, NT) = True
                    IncluirControl(C, D, 2, NT)
                Next
            End If
        Next
        For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
            ' Debug.print(D.Name)
            ' Debug.print(D.GetType().Name)
            If LCase(D.GetType().Name) = "tabpage" Then
                NT = CInt(Microsoft.VisualBasic.Right(D.Name, 2))
                For Each C In D.Controls
                    ' Debug.print(C.Name)
                    ' Debug.print(C.GetType().Name)
                    TabsUsados(2, NT) = True
                    IncluirControl(C, D, 2, 10 + NT)
                Next
            End If
        Next


        '        Dim i As Integer, j As Integer, k As Integer, TabX As Long, TTEncontrada As Integer
        '        Dim C As Control, CE As CEdicion, TT As String, TTS(10) As String, NumeroControlesCEdicion As Integer, TC As String
        '        Dim TabUsado(20) As Boolean, QueT As Integer, XT As Integer

        '        ReDim XMshFicha(0)
        '        ReDim XCnTabla(0)
        '        ReDim XCEdicion(0)
        '        NumeroTablas = 0

        '        For Each C In Me.Controls
        '            If LCase(C.Name) = "CnTabla" Then
        '                If Tablas(C.NumTablaFormulario) <> "" And Tablas(C.NumTablaFormulario) <> Trim(C.Tabla) Then 'Nuevo01032004
        '                    ManejoDeMensajes "Anote número de tabla en formulario (CnTabla) duplicado.", Me.Caption, "CrearArrayDeControles", True 'Nuevo01032004
        '                    Exit Sub 'Nuevo01032004
        '                End If
        '                Tablas(C.NumTablaFormulario) = Trim(C.Tabla)
        '                EnlacesTablas(C.NumTablaFormulario) = Trim(C.EnlaceATablaPadre)
        '                TablaPadre(C.NumTablaFormulario) = Trim(C.NumTablaPadreFormulario)
        '                NumeroTablas = NumeroTablas + 1
        '            End If
        '        Next
        '        For i = 0 To NumeroTablas - 1 'Nuevo01032004
        '            If Trim(Tablas(i)) = "" Then 'Nuevo01032004
        '                ManejoDeMensajes "Anote error en número de tabla en formulario (CnTabla).", Me.Caption, "CrearArrayDeControles", True 'Nuevo01032004
        '                Exit Sub 'Nuevo01032004
        '            End If 'Nuevo01032004
        '        Next 'Nuevo01032004
        '        For i = 1 To 10 : TTS(i) = "" : TabUsado(i) = False : Next
        '        ReDim XCnTabla(NumeroTablas - 1)
        '        For Each C In Me.Controls 'Nuevo01032004
        '            If LCase(C.Name) = "CnTabla" Then 'Nuevo01032004
        '        Set XCnTabla(C.NumTablaFormulario) = C 'Nuevo01032004
        '        If C.NumeroTab > -1 Then 'Nuevo01032004
        '                    If C.NumeroTab > 10 Then 'Nuevo01032004
        '                        XT = Int(C.NumeroTab / 10) 'Nuevo01032004
        '                        TabUsado(XT) = True 'Nuevo01032004
        '                    Else 'Nuevo01032004
        '                        TabUsado(C.NumeroTab) = True 'Nuevo01032004
        '                    End If 'Nuevo01032004
        '                End If 'Nuevo01032004
        '            End If
        '        Next
        '        NumeroControlesCEdicion = 0
        '        For Each C In Me.Controls
        '            If LCase(C.Name) = "cedicion" Then
        '                If NumeroControlesCEdicion < C.Index Then
        '                    NumeroControlesCEdicion = C.Index
        '                    ReDim Preserve XCEdicion(NumeroControlesCEdicion)
        '                End If
        '        Set XCEdicion(C.Index) = C
        '        If C.NumeroTab > -1 And C.ParaGrid = False Then
        '                    If C.NumeroTab > 10 Then
        '                        XT = Int(C.NumeroTab / 10)
        '                        TabUsado(XT) = True
        '                    Else
        '                        TabUsado(C.NumeroTab) = True
        '                    End If
        '                End If
        '                QueT = QueTablaEs(C.Tabla)
        '                C.NumTablaFormulario = QueT
        '            ElseIf LCase(C.Name) = "cflex" Then
        '                j = C.Index
        '                If j > UBound(XMshFicha()) Then
        '                    ReDim Preserve XMshFicha(j)
        '                End If
        '        Set XMshFicha(j) = C
        '    End If
        '        Next
        '        For i = 0 To TabPrincipalPrincipal.Tabs - 1
        '            If TabUsado(i) = False Then
        '                TabPrincipalPrincipal.TabVisible(i) = False
        '            End If
        '        Next
        '        For i = 0 To NumeroControlesCEdicion
        '    Set CE = XCEdicion(i)
        '    If Not (CE Is Nothing) Then
        '                If CE.IndiceControl < 0 And CE.ParaGrid = False Then
        '                    CE.EsVisible = True
        '                Else
        '                    CE.EsVisible = False
        '                End If
        '                CE.RepintarControl
        '                If CE.NumDatoFijo > 0 And CE.NumDatoFijo <= 10 Then
        '                    TTEncontrada = QueTablaEs(CE.Tabla)
        '                    If TTEncontrada > -1 Then
        '                        NumeroDatosFijos(TTEncontrada) = NumeroDatosFijos(TTEncontrada) + 1
        '                Set DatosFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = CE
        '                ValoresFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Parametros(CE.NumDatoFijo)
        '                        CamposFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Trim(CE.Campo)
        '                        CE.ValorFijo = Parametros(CE.NumDatoFijo)
        '                        CE.EnlacePrincipal = -1
        '                        '                CE.LongitudControlFicha = Len(Trim(Parametros(CE.NumDatoFijo)))
        '                        CE.Control = "TextBox"
        '                        CE.Bloqueado = True
        '                    End If
        '                End If
        '                If CE.NumDatoFijoLookUp > 0 And CE.NumDatoFijoLookUp <= 10 Then
        '                    TTEncontrada = QueTablaEs(CE.Tabla)
        '                    If TTEncontrada > -1 Then
        '                        NumeroDatosFijos(TTEncontrada) = NumeroDatosFijos(TTEncontrada) + 1
        '                Set DatosFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = CE
        '                ValoresFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Parametros(CE.NumDatoFijoLookUp)
        '                        CamposFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Trim(CE.Campo)
        '                        CE.ValorFijoLookUp = Parametros(CE.NumDatoFijoLookUp)
        '                        CE.EnlacePrincipal = -1
        '                        CE.LongitudLookUp = Len(Trim(Parametros(CE.NumDatoFijoLookUp)))
        '                        CE.Control = "TextBox"
        '                        CE.Bloqueado = True
        '                    End If
        '                End If
        '            End If
        '        Next
    End Sub



    '    Option Explicit On

    '    'Private Declare Function GetKeyState Lib "user32" (ByVal nvirtkey As Long) As Integer
    '    Public ObjetoGlobal As Object
    '    Public ObjetoLlamado As Object
    '    Public FormDestino As Form
    '    Public ModoSalida As String
    '    Public AccionParaFoco As String

    '    Public ClsBotones As Object
    '    Public FrmB As Form
    '    Public ClsExportar As Object
    '    Public FrmExportar As Form
    '    Public ClsGridNuevo As Object
    '    Public FrmGridNuevo As Form
    '    Public ClsLView As Object
    '    Public FrmLView As Form
    '    Public ClsCalendario As Object
    '    Public FrmCalendario As Form

    '    Private NumeroTablas As Integer
    '    Private Tablas(10) As String
    '    Private TablaPadre(10) As Integer
    '    Private EnlacesTablas(10) As Long
    '    Private TipoMantenimiento(10) As String
    '    Private XMshFicha() As CFlex
    '    Private XCEdicion() As CEdicion
    '    Private XCnTabla() As CnTabla
    '    Private Parametros(10) As String
    '    Private NumeroDatosFijos(10) As Integer
    '    Private DatosFijos(10, 10) As CEdicion
    '    Private ValoresFijos(10, 10) As String
    '    Private CamposFijos(10, 10) As String
    '    Private SituacionFormulario As Estados
    '    Private TablaEnEdicion As Integer

    '    Private Sub CEdicion_Validate(Index As Integer, Cancel As Boolean)
    '        If SituacionFormulario <> Inactivo Then
    '            If CEdicion(Index).IndiceControl = -1 Then
    '                Cancel = Not (CEdicion(Index).Validacion)
    '            Else
    '                Cancel = False
    '            End If
    '        End If
    '    End Sub

    '    Private Sub CFlex_GotFocus(Index As Integer)

    '    End Sub

    '    Private Sub CmdSalir_Click()
    '        Unload Me
    'End Sub
    '    Private Sub Form_Load()
    '        TabPrincipalPrincipal.Tab = 0
    '        AccionParaFoco = ""
    '        AsignarParametros()
    '        CrearArrayDeControles()
    '        RehacerTabIndex()
    '        InicializarCnTablas()
    '        CambioSituacionFormulario CInt(Inactivo), -1
    'EdgUnMomento.Visible = False
    '        AlAbrirFormulario Me
    'End Sub
    '    Private Sub CrearArrayDeControles()
    '        Dim i As Integer, j As Integer, k As Integer, TabX As Long, TTEncontrada As Integer
    '        Dim C As Control, CE As CEdicion, TT As String, TTS(10) As String, NumeroControlesCEdicion As Integer, TC As String
    '        Dim TabUsado(20) As Boolean, QueT As Integer, XT As Integer

    '        ReDim XMshFicha(0)
    '        ReDim XCnTabla(0)
    '        ReDim XCEdicion(0)
    '        NumeroTablas = 0

    '        For Each C In Me.Controls
    '            If LCase(C.Name) = "CnTabla" Then
    '                If Tablas(C.NumTablaFormulario) <> "" And Tablas(C.NumTablaFormulario) <> Trim(C.Tabla) Then 'Nuevo01032004
    '                    ManejoDeMensajes "Anote número de tabla en formulario (CnTabla) duplicado.", Me.Caption, "CrearArrayDeControles", True 'Nuevo01032004
    '                    Exit Sub 'Nuevo01032004
    '                End If
    '                Tablas(C.NumTablaFormulario) = Trim(C.Tabla)
    '                EnlacesTablas(C.NumTablaFormulario) = Trim(C.EnlaceATablaPadre)
    '                TablaPadre(C.NumTablaFormulario) = Trim(C.NumTablaPadreFormulario)
    '                NumeroTablas = NumeroTablas + 1
    '            End If
    '        Next
    '        For i = 0 To NumeroTablas - 1 'Nuevo01032004
    '            If Trim(Tablas(i)) = "" Then 'Nuevo01032004
    '                ManejoDeMensajes "Anote error en número de tabla en formulario (CnTabla).", Me.Caption, "CrearArrayDeControles", True 'Nuevo01032004
    '                Exit Sub 'Nuevo01032004
    '            End If 'Nuevo01032004
    '        Next 'Nuevo01032004
    '        For i = 1 To 10 : TTS(i) = "" : TabUsado(i) = False : Next
    '        ReDim XCnTabla(NumeroTablas - 1)
    '        For Each C In Me.Controls 'Nuevo01032004
    '            If LCase(C.Name) = "CnTabla" Then 'Nuevo01032004
    '        Set XCnTabla(C.NumTablaFormulario) = C 'Nuevo01032004
    '        If C.NumeroTab > -1 Then 'Nuevo01032004
    '                    If C.NumeroTab > 10 Then 'Nuevo01032004
    '                        XT = Int(C.NumeroTab / 10) 'Nuevo01032004
    '                        TabUsado(XT) = True 'Nuevo01032004
    '                    Else 'Nuevo01032004
    '                        TabUsado(C.NumeroTab) = True 'Nuevo01032004
    '                    End If 'Nuevo01032004
    '                End If 'Nuevo01032004
    '            End If
    '        Next
    '        NumeroControlesCEdicion = 0
    '        For Each C In Me.Controls
    '            If LCase(C.Name) = "cedicion" Then
    '                If NumeroControlesCEdicion < C.Index Then
    '                    NumeroControlesCEdicion = C.Index
    '                    ReDim Preserve XCEdicion(NumeroControlesCEdicion)
    '                End If
    '        Set XCEdicion(C.Index) = C
    '        If C.NumeroTab > -1 And C.ParaGrid = False Then
    '                    If C.NumeroTab > 10 Then
    '                        XT = Int(C.NumeroTab / 10)
    '                        TabUsado(XT) = True
    '                    Else
    '                        TabUsado(C.NumeroTab) = True
    '                    End If
    '                End If
    '                QueT = QueTablaEs(C.Tabla)
    '                C.NumTablaFormulario = QueT
    '            ElseIf LCase(C.Name) = "cflex" Then
    '                j = C.Index
    '                If j > UBound(XMshFicha()) Then
    '                    ReDim Preserve XMshFicha(j)
    '                End If
    '        Set XMshFicha(j) = C
    '    End If
    '        Next
    '        For i = 0 To TabPrincipalPrincipal.Tabs - 1
    '            If TabUsado(i) = False Then
    '                TabPrincipalPrincipal.TabVisible(i) = False
    '            End If
    '        Next
    '        For i = 0 To NumeroControlesCEdicion
    '    Set CE = XCEdicion(i)
    '    If Not (CE Is Nothing) Then
    '                If CE.IndiceControl < 0 And CE.ParaGrid = False Then
    '                    CE.EsVisible = True
    '                Else
    '                    CE.EsVisible = False
    '                End If
    '                CE.RepintarControl
    '                If CE.NumDatoFijo > 0 And CE.NumDatoFijo <= 10 Then
    '                    TTEncontrada = QueTablaEs(CE.Tabla)
    '                    If TTEncontrada > -1 Then
    '                        NumeroDatosFijos(TTEncontrada) = NumeroDatosFijos(TTEncontrada) + 1
    '                Set DatosFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = CE
    '                ValoresFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Parametros(CE.NumDatoFijo)
    '                        CamposFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Trim(CE.Campo)
    '                        CE.ValorFijo = Parametros(CE.NumDatoFijo)
    '                        CE.EnlacePrincipal = -1
    '                        '                CE.LongitudControlFicha = Len(Trim(Parametros(CE.NumDatoFijo)))
    '                        CE.Control = "TextBox"
    '                        CE.Bloqueado = True
    '                    End If
    '                End If
    '                If CE.NumDatoFijoLookUp > 0 And CE.NumDatoFijoLookUp <= 10 Then
    '                    TTEncontrada = QueTablaEs(CE.Tabla)
    '                    If TTEncontrada > -1 Then
    '                        NumeroDatosFijos(TTEncontrada) = NumeroDatosFijos(TTEncontrada) + 1
    '                Set DatosFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = CE
    '                ValoresFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Parametros(CE.NumDatoFijoLookUp)
    '                        CamposFijos(TTEncontrada, NumeroDatosFijos(TTEncontrada)) = Trim(CE.Campo)
    '                        CE.ValorFijoLookUp = Parametros(CE.NumDatoFijoLookUp)
    '                        CE.EnlacePrincipal = -1
    '                        CE.LongitudLookUp = Len(Trim(Parametros(CE.NumDatoFijoLookUp)))
    '                        CE.Control = "TextBox"
    '                        CE.Bloqueado = True
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End Sub
    '    Private Sub AsignarParametros()
    '        'Para la versión EXE
    '        'Dim V() As String, i As Integer
    '        '
    '        'V() = Split(Command$, ",")
    '        'If UBound(V) > 10 Then
    '        '    MsgBox "Más de 10 parámetros. Se ignorará el resto."
    '        '    ReDim Preserve V(10)
    '        'End If
    '        'For i = 0 To UBound(V())
    '        '    Parametros(i + 1) = V(i)
    '        'Next

    '        Parametros(1) = ObjetoGlobal.empresaactual
    '        Parametros(2) = ObjetoGlobal.EmpresaRazonSocial
    '        Parametros(3) = ObjetoGlobal.EjercicioActual
    '        Parametros(4) = ObjetoGlobal.DescripcionEjercicio
    '    End Sub
    '    Private Sub TxtPARAFOCO_GotFocus()
    '        Dim TT As Integer

    '        If AccionParaFoco = "" Or SituacionFormulario = Inactivo Then
    '            Exit Sub
    '        ElseIf AccionParaFoco = "anterior" Then
    '            AccionParaFoco = ""
    '            XCnTabla(TablaEnEdicion).GridAsociado.AnteriorEnGrid False 'Nuevo01032004
    '            XCnTabla(TablaEnEdicion).PrimeroEnGrid.GanaFoco 'Nuevo01032004
    '        ElseIf AccionParaFoco = "aceptar" Then
    '            AccionParaFoco = ""
    '            TT = TablaEnEdicion
    '            XCnTabla(TablaEnEdicion).GanaFocoAceptar 'Nuevo01032004
    '        End If
    '    End Sub
    '    Private Function QueTablaEs(Cad As String) As Integer
    '        Dim TablaEncontrada As Boolean, NumeroTablaEncontrada As Integer, j As Integer

    '        TablaEncontrada = False
    '        For j = 0 To NumeroTablas
    '            If Trim(Tablas(j)) = Trim(Cad) Then
    '                NumeroTablaEncontrada = j
    '                TablaEncontrada = True
    '                Exit For
    '            End If
    '        Next
    '        If TablaEncontrada = True Then
    '            QueTablaEs = NumeroTablaEncontrada
    '        Else
    '            QueTablaEs = -1
    '        End If
    '    End Function
    '    Public Sub Seleccion(NumTabla As Integer)
    '        Dim QueT As Integer, Cadena As String, YaSeleccionado(10) As Boolean, i As Integer
    '        Dim AlgunaSeleccionada As Boolean

    '        For i = 0 To NumeroTablas - 1
    '            If XCnTabla(i).NumTablaPadreFormulario = -1 Then 'Nuevo01032004
    '                YaSeleccionado(i) = True
    '            Else
    '                YaSeleccionado(i) = False
    '            End If
    '        Next
    '        If TablaPadre(NumTabla) > -1 Then 'OJO
    '            AlgunaSeleccionada = True
    '            Do While AlgunaSeleccionada = True
    '                AlgunaSeleccionada = False
    '                For QueT = 0 To NumeroTablas - 1
    '                    If TablaPadre(QueT) >= 0 Then
    '                        If YaSeleccionado(TablaPadre(QueT)) = True And YaSeleccionado(QueT) = False And QueT <> NumTabla Then
    '                            YaSeleccionado(QueT) = True
    '                            AlgunaSeleccionada = True
    '                        End If
    '                    End If
    '                Next
    '            Loop
    '            YaSeleccionado(NumTabla) = True
    '        End If
    '        AlgunaSeleccionada = True
    '        Do While AlgunaSeleccionada = True
    '            AlgunaSeleccionada = False
    '            For QueT = 0 To NumeroTablas - 1
    '                If YaSeleccionado(QueT) = False Then
    '                    If YaSeleccionado(TablaPadre(QueT)) = True Then 'OJO
    '                        If XCnTabla(QueT).EstablecerSeleccionAuxiliar = True Then 'Nuevo01032004
    '                            If XCnTabla(QueT).TipoMantenimiento = "G" Then 'Nuevo01032004
    '                                XCnTabla(QueT).MostrarRegistrosGrid 'Nuevo01032004
    '                            Else
    '                                XCnTabla(QueT).Primero False 'Nuevo01032004
    '                            End If
    '                        Else
    '                            XCnTabla(QueT).LimpiarDatos True, False, False 'Nuevo01032004
    '                            XCnTabla(QueT).LimpiarControles True 'Nuevo01032004
    '                        End If
    '                        YaSeleccionado(QueT) = True
    '                        AlgunaSeleccionada = True
    '                    End If
    '                End If
    '            Next
    '        Loop
    '    End Sub


    '    Private Sub InicializarCnTablas()
    '        Dim i As Integer, QueT As Integer

    '        For i = 0 To UBound(XCEdicion())
    '            If Not (XCEdicion(i) Is Nothing) Then
    '                QueT = QueTablaEs(XCEdicion(i).Tabla)
    '                XCnTabla(QueT).AsignarCEdicion XCEdicion(i)
    '    End If
    '        Next
    '        For i = 0 To UBound(XMshFicha())
    '            If Not (XMshFicha(i) Is Nothing) Then
    '                QueT = QueTablaEs(XMshFicha(i).Tabla)
    '                XCnTabla(QueT).AsignarCFlex XMshFicha(i)
    '    End If
    '        Next

    '        For i = 0 To NumeroTablas - 1
    '            XCnTabla(i).Inicializar ObjetoGlobal.cn, Parametros()
    'Next
    '        For i = 0 To NumeroTablas - 1
    '            XCnTabla(i).Inicializar2
    '        Next
    '    End Sub

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




    Private Function IncluirControl(C As Control, CNT As Control, NumeroDeProceso As Integer, NumeroContenedor As Integer) As Boolean
        Dim MensajeError As String = "", i As Integer
        Dim CT As CnTabla.CnTabla, CE As CnEdicion.CnEdicion, Cadena As String, Indice As Integer

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

                    CE.Contenedor = CNT
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
                CE.TxtDatos = C
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
            ElseIf LCase(C.GetType().Name) = "combobox" And LCase(Microsoft.VisualBasic.Left(C.Name, 11)) = "txtcombobox" Then
                Cadena = Trim(C.Name)
                Indice = CInt(Microsoft.VisualBasic.Right(Cadena, 3))
                CE = XCNE(Indice)
                If CE Is Nothing Then
                    MensajeError = "Anote control CnEdicion inexistente (" + Trim(CE.Name) + ")"
                    Throw New ArgumentException(MensajeError)
                End If
                CE.ComboBox = C
                CE.EdicionEnCombo = True
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
                CE.CmdGrid = C
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
                CE.CmdFecha = C
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
                CE.CmdMantenimiento = C
                C.TabIndex = 9999
                C.TabStop = False
            ElseIf LCase(C.GetType().Name) = "button" Or LCase(C.GetType().Name) = "label" Then
                C.TabIndex = 9999
                C.TabStop = False
            End If
        End If
        IncluirControl = True
    End Function

    Public Sub RehacerTabIndex()

        Dim i As Integer, j As Integer, Contenedor As Integer, L1 As Long, L2 As Long, Clave As String, Cuantos As Integer
        Dim CE As CnEdicion.CnEdicion
        Dim ArrayDeControles() As String, Array2DeControles() As Integer, Provisional As String, Provisional2 As Integer
        Dim MismaLinea As Boolean, TAG As Integer, TAGActual As Integer, Posicion As Long, PosicionLinea As Long
        Dim FlagCambiado As Boolean

        Cuantos = 0
        For i = 1 To UltimoControlCnEdicion
            If Not (XCNE(i)) Is Nothing Then
                CE = XCNE(i)
                L1 = CE.TxtDatos.Top
                L2 = CE.TxtDatos.Left

                Contenedor = 0
                If LCase(Microsoft.VisualBasic.Left(ContenedorCnEdicion(i).Name, 2)) = "tp" Then
                    Contenedor = 10 + CInt(Mid(ContenedorCnEdicion(i).Name, 3))
                ElseIf LCase(Microsoft.VisualBasic.Left(ContenedorCnEdicion(i).Name, 7)) = "tabpage" Then
                    Contenedor = 30 + CInt(Mid(ContenedorCnEdicion(i).Name, 8))
                End If

                Clave = Format(Contenedor, "00") + Format(L1, "00000") + Format(L2, "00000")
                Cuantos = Cuantos + 1
                ReDim Preserve ArrayDeControles(Cuantos)
                ReDim Preserve Array2DeControles(Cuantos)
                ArrayDeControles(Cuantos) = Clave
                Array2DeControles(Cuantos) = i
            End If
        Next
        For i = 1 To Cuantos - 1
            For j = i + 1 To Cuantos
                If ArrayDeControles(j) < ArrayDeControles(i) Then
                    Provisional = ArrayDeControles(i)
                    ArrayDeControles(i) = ArrayDeControles(j)
                    ArrayDeControles(j) = Provisional

                    Provisional2 = Array2DeControles(i)
                    Array2DeControles(i) = Array2DeControles(j)
                    Array2DeControles(j) = Provisional2
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

                        Provisional2 = Array2DeControles(i)
                        Array2DeControles(i) = Array2DeControles(j)
                        Array2DeControles(j) = Provisional2
                    End If
                Next
            Next
        End If

        For i = 1 To Cuantos
            XCNE(Array2DeControles(i)).TxtDatos.TabIndex = i
            XCNE(Array2DeControles(i)).TxtDatos.TabStop = True
        Next

        TxtParaFoco.TabIndex = 9999
        TxtParaFoco.TabStop = False


        'For i = 1 To 5
        '    Debug.Print(i)
        '    Debug.Print(XCNE(i).TxtDatos.Name)
        '    Debug.Print(XCNE(i).TxtDatos.TabIndex)
        'Next

    End Sub

    Private Sub HacerInvisiblesTabsNoUsados()
        Dim i As Integer, Cadena As String
        Dim C As TabControl, CP As TabPage


        For i = 0 To 9
            If TabsUsados(1, i) = False Then
                Cadena = "TP" + Format(i, "00")
                C = Me.PanelCentral.Controls.Item("TabCabecera")
                CP = C.TabPages(Cadena)
                C.TabPages.Remove(CP)
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
    Private Sub InicializarControles()
        Dim CE As CnEdicion.CnEdicion, i As Integer, j As Integer

        For i = 1 To UltimoControlCnEdicion
            If Not (XCNE(i) Is Nothing) Then
                CE = XCNE(i)
                QueTabla(CE).XCnEdicion(Trim(CE.Tabla) + "." + Trim(CE.Campo)) = CE
            End If

        Next
        'FALTA: Igual para los Grids

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
        '        For i = 0 To NumeroTablas - 1
        '            XCnTabla(i).Inicializar2
        '        Next
    End Sub

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



    Private Sub Comprobar()
        Dim C As Control, D As Control
        Dim i As Integer, j As Integer



        For Each C In Me.PanelSuperior.Controls
            ' Debug.print(C.Name)
            ' Debug.print(C.GetType().Name)
            Try
                ' Debug.print(CStr(C.TabIndex))
            Catch ex As Exception
                ' Debug.print("NO TABINDEX")
            End Try
            Try
                ' Debug.print(CStr(C.TabStop))
            Catch ex As Exception
                ' Debug.print("NO TABSTOP")
            End Try
        Next


        Try
            ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabCabecera").TabIndex))
        Catch ex As Exception
            ' Debug.print("NO TABINDEX")
        End Try
        Try
            ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabCabecera").TabStop))
        Catch ex As Exception
            ' Debug.print("NO TABSTOP")
        End Try



        For Each D In Me.PanelCentral.Controls.Item("TabCabecera").Controls
            ' Debug.print(D.Name)
            ' Debug.print(D.GetType().Name)
            Try
                ' Debug.print(CStr(D.TabIndex))
            Catch ex As Exception
                ' Debug.print("NO TABINDEX")
            End Try
            Try
                ' Debug.print(CStr(D.TabStop))
            Catch ex As Exception
                ' Debug.print("NO TABSTOP")
            End Try

            If LCase(D.GetType().Name) = "tabpage" Then
                For Each C In D.Controls
                    ' Debug.print(C.Name)
                    ' Debug.print(C.GetType().Name)
                    Try
                        ' Debug.print(CStr(C.TabIndex))
                    Catch ex As Exception
                        ' Debug.print("NO TABINDEX")
                    End Try
                    Try
                        ' Debug.print(CStr(C.TabStop))
                    Catch ex As Exception
                        ' Debug.print("NO TABSTOP")
                    End Try
                Next
            End If
        Next

        Try
            ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabGeneral").TabIndex))
        Catch ex As Exception
            ' Debug.print("NO TABINDEX")
        End Try
        Try
            ' Debug.print(CStr(Me.PanelCentral.Controls.Item("TabGeneral").TabStop))
        Catch ex As Exception
            ' Debug.print("NO TABSTOP")
        End Try


        For Each D In Me.PanelCentral.Controls.Item("TabGeneral").Controls
            ' Debug.print(D.Name)
            ' Debug.print(D.GetType().Name)
            Try
                ' Debug.print(CStr(D.TabIndex))
            Catch ex As Exception
                ' Debug.print("NO TABINDEX")
            End Try
            Try
                ' Debug.print(CStr(D.TabStop))
            Catch ex As Exception
                ' Debug.print("NO TABSTOP")
            End Try

            If LCase(D.GetType().Name) = "tabpage" Then
                For Each C In D.Controls
                    ' Debug.print(C.Name)
                    ' Debug.print(C.GetType().Name)
                    Try
                        ' Debug.print(CStr(C.TabIndex))
                    Catch ex As Exception
                        ' Debug.print("NO TABINDEX")
                    End Try
                    Try
                        ' Debug.print(CStr(C.TabStop))
                    Catch ex As Exception
                        ' Debug.print("NO TABSTOP")
                    End Try
                Next
            End If
        Next

        For Each C In Me.PanelInferior.Controls
            ' Debug.print(C.Name)
            ' Debug.print(C.GetType().Name)
            Try
                ' Debug.print(CStr(C.TabIndex))
            Catch ex As Exception
                ' Debug.print("NO TABINDEX")
            End Try
            Try
                ' Debug.print(CStr(C.TabStop))
            Catch ex As Exception
                ' Debug.print("NO TABSTOP")
            End Try
        Next

        'For i = 1 To 2
        '    For j = 0 To 9
        '        Debug.Print(TabsUsados(i, j))
        '    Next
        'Next



    End Sub

    Private Sub CmdComprobar_Click(sender As Object, e As EventArgs) Handles CmdComprobar.Click

        Dim RS As New cnRecordset.CnRecordset

        RS.Open("SELECT * FROM PRUEBA ORDER BY 1", ObjetoGlobal.cn)

        For j = 0 To RS.CuantosCampos - 1
            MsgBox(CStr(RS.NombreCampo(j)))
        Next




        For i = 1 To RS.RecordCount
            RS.AbsolutePosition = i
            For j = 0 To RS.CuantosCampos - 1
                MsgBox(CStr(RS!fecha_nohora_requerida))
                MsgBox(CStr(RS!fecha_nohora_no_requerida))
            Next
        Next

    End Sub


    Private Sub CnTabla_CTSeleccion(sender As Object, e As EventArgs, CT As CnTabla.CnTabla) Handles CnTabla01.CTSeleccion
        Dim CNT As Control

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

    End Sub

    Private Sub CnTabla_CTAnadir(sender As Object, e As EventArgs, CT As CnTabla.CnTabla) Handles CnTabla01.CTAnadir
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

    End Sub

    Private Sub CnTabla_CTModificar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla) Handles CnTabla01.CTModificar
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

    Private Sub CnTabla_CTAceptar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla) Handles CnTabla01.CTAceptar
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
                '            If m_TipoMantenimiento = "G" Then
                '                If m_Rs(1).RecordCount = 0 Then
                '                    RegistroAnterior = -1
                '                Else
                '                    RegistroAnterior = m_Rs(1).AbsolutePosition
                '                End If
                '            End If
                '            Parent.CambioSituacionFormulario CInt(AceptandoCreacion), m_NumTablaEnFormulario
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


                '                Parent.CambioSituacionFormulario CInt(Creando), m_NumTablaEnFormulario
                '        Exit Sub
                '            End If
                '            If ComprobacionAntesDeGrabarCreacion(m_NumTablaEnFormulario, Parent, Me) = True Then
                '                Parent.CambioSituacionFormulario CInt(Creando), m_NumTablaEnFormulario
                '        Exit Sub
                '            End If
                '            FlagExito = Not (AntesDeGrabarCreacion(m_NumTablaEnFormulario, Parent, Me))
                '            Mensaje = ""
                '            If FlagExito = True Then FlagExito = ActualizarDatos(Mensaje)
                '            If FlagExito = False Then
                '                DespuesDeFracasarCreacion m_NumTablaEnFormulario, Parent, Me
                '        If Trim(Mensaje) > "" Then ManejoDeMensajes "No ha sido posible actualizar el registro en la Base de Datos." + vbCrLf + "Se ha obtenido el siguiente mensaje:" + vbCrLf + Trim(Mensaje), "Ctabla", "CmdAceptar_Click", True
                '        Parent.CambioSituacionFormulario CInt(Inactivo), m_NumTablaEnFormulario
                '        If Parent.ControlTabla(Ancestro).Cuantos <= 0 Then
                '                    Parent.LimpiarTodosDatos
                '                ElseIf m_NumTablaPadre = -1 Then
                '                    EsteRegistro m_Rs(1).AbsolutePosition, True
                '        ElseIf Parent.ControlTabla(m_NumTablaPadre).Cuantos > 0 Then
                '                    Parent.ControlTabla(m_NumTablaPadre).EsteRegistro Parent.ControlTabla(m_NumTablaPadre).RegistroActual, True
                '        Else
                '                    LimpiarDatos True, True, True
                '            LimpiarControles True
                '        End If
                '                If m_TipoMantenimiento = "G" Then
                '                    m_GridAsociado.ConBarras
                '                End If
                '                Exit Sub
                '            Else
                '                DespuesDeGrabarCreacion m_NumTablaEnFormulario, Parent, Me
                '        ActualizarEnCreacion
                '                CuantosRegistros
                '                Parent.CambioSituacionFormulario CInt(Inactivo), 0
                '        If m_TipoMantenimiento = "G" Then
                '                    m_GridAsociado.LeftCol = 0
                '                    m_GridAsociado.MostrarUnRegistro m_Rs(1).AbsolutePosition
                '            m_GridAsociado.SeleccionarRegistro RegistroAnterior, m_Rs(1).AbsolutePosition
                '            m_GridAsociado.ConBarras
                '                End If
                '            End If
                '            DespuesDeCrear Me, Parent
            End If
        End If
    End Sub

    Private Sub CnTabla_CTCancelar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla) Handles CnTabla01.CTCancelar
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

    Private Sub CnTabla_CTEliminar(sender As Object, e As EventArgs, CT As CnTabla.CnTabla) Handles CnTabla01.CTEliminar
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

            DespuesdeCancelarEliminacion(Me, CT)
        End If
        CambioSituacionFormulario(Estados.Inactivo, -1)

    End Sub



    'Private Sub CnTabla_CambioSituacion(CT As CnTabla.CnTabla, ByVal Estado As Integer) Handles CnTabla01.CTCambiaSituacionFormulario
    '    Dim EstadoFormulario As Estados
    '    EstadoFormulario = DirectCast(Estado, Estados)
    '    CambioSituacionFormulario(EstadoFormulario, CT.NumTablaFormulario)
    'End Sub

    Private Sub CnTabla_MostrarLookup(CT As CnTabla.CnTabla, TxtLK As System.Windows.Forms.TextBox, Valor As String) Handles CnTabla01.CTMostrarLookup
        TxtLK.Text = Trim(Valor)
    End Sub






    'Private Sub CmdCombo_Click(Index As Integer)
    '    XCEdicion(Index).CmdCombo_Click
    'End Sub

    Private Sub TxtDatos_TextChanged(sender As Object, e As EventArgs) Handles TxtDatos001.TextChanged, TxtDatos002.TextChanged, TxtDatos003.TextChanged, TxtDatos004.TextChanged, TxtDatos005.TextChanged, TxtDatos006.TextChanged, TxtDatos007.TextChanged, TxtDatos008.TextChanged, TxtDatos009.TextChanged, TxtDatos010.TextChanged, TxtDatos011.TextChanged
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_TextChanged " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CETextChanged(sender, e)
    End Sub


    Private Sub TxtDatos_GotFocus(sender As Object, e As EventArgs) Handles TxtDatos001.GotFocus, TxtDatos002.GotFocus, TxtDatos003.GotFocus, TxtDatos004.GotFocus, TxtDatos005.GotFocus, TxtDatos006.GotFocus, TxtDatos007.GotFocus, TxtDatos008.GotFocus, TxtDatos009.GotFocus, TxtDatos010.GotFocus, TxtDatos011.GotFocus
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_GotFocus " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEGotFocus(sender, e)
    End Sub

    Private Sub TxtDatos_LostFocus(sender As Object, e As EventArgs) Handles TxtDatos001.LostFocus, TxtDatos002.LostFocus, TxtDatos003.LostFocus, TxtDatos004.LostFocus, TxtDatos005.LostFocus, TxtDatos006.LostFocus, TxtDatos007.LostFocus, TxtDatos008.LostFocus, TxtDatos009.LostFocus, TxtDatos010.LostFocus, TxtDatos011.LostFocus
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_LostFocus " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CELostFocus(sender, e)


    End Sub
    Private Sub TxtDatos_Validating(sender As Object, e As CancelEventArgs) Handles TxtDatos001.Validating, TxtDatos002.Validating, TxtDatos003.Validating, TxtDatos004.Validating, TxtDatos005.Validating, TxtDatos006.Validating, TxtDatos007.Validating, TxtDatos008.Validating, TxtDatos009.Validating, TxtDatos010.Validating, TxtDatos011.Validating
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Validating " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEValidating(sender, e)

        'If sender.name = "TxtDatos008" Then
        '    MsgBox("")
        'End If
        'Debug.Print("SIGO CON VALIDATED")

        If e.Cancel = False Then
            CT = XCT(CE.NumeroTablaFormulario)

            CT.AsignarValor(CE.Campo, DirectCast(sender, TextBox).Text)

            If CT.VerificarEnlacesCampo(CE) = False Then
                e.Cancel = True
                CT.LimpiarLookups(CE)
            Else
                CT.MostrarLookups(CE)
            End If
            If e.Cancel = False And SituacionFormulario = Estados.Creando And CE.Clave = True Then
                If CT.ClavePrimariaExistente() Then
                    CT.LimpiarLookups(CE)
                    e.Cancel = True
                End If
            End If
        End If





    End Sub

    Private Sub TxtDatos_Validated(sender As Object, e As EventArgs) Handles TxtDatos001.Validated, TxtDatos002.Validated, TxtDatos003.Validated, TxtDatos004.Validated, TxtDatos005.Validated, TxtDatos006.Validated, TxtDatos007.Validated, TxtDatos008.Validated, TxtDatos009.Validated, TxtDatos010.Validated, TxtDatos011.Validated
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Validated " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEValidated(sender, e)
        'CT = XCT(CE.NumeroTablaFormulario)
        'CT.AsignarValor(CE.Campo, CE.Valor)





    End Sub


    Private Sub TxtDatos_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtDatos001.KeyDown, TxtDatos002.KeyDown, TxtDatos003.KeyDown, TxtDatos004.KeyDown, TxtDatos005.KeyDown, TxtDatos006.KeyDown, TxtDatos007.KeyDown, TxtDatos008.KeyDown, TxtDatos009.KeyDown, TxtDatos010.KeyDown, TxtDatos011.KeyDown
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyDown " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyDown(sender, e)

    End Sub

    Private Sub TxtDatos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDatos001.KeyPress, TxtDatos002.KeyPress, TxtDatos003.KeyPress, TxtDatos004.KeyPress, TxtDatos005.KeyPress, TxtDatos006.KeyPress, TxtDatos007.KeyPress, TxtDatos008.KeyPress, TxtDatos009.KeyPress, TxtDatos010.KeyPress, TxtDatos011.KeyPress
        Dim CE As CnEdicion.CnEdicion, Indice As Integer
        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyPress " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyPress(sender, e)

    End Sub

    Private Sub TxtDatos_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtDatos001.KeyUp, TxtDatos002.KeyUp, TxtDatos003.KeyUp, TxtDatos004.KeyUp, TxtDatos005.KeyUp, TxtDatos006.KeyUp, TxtDatos007.KeyUp, TxtDatos008.KeyUp, TxtDatos009.KeyUp, TxtDatos010.KeyUp, TxtDatos011.KeyUp
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_KeyUp " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEKeyUp(sender, e)


    End Sub
    Private Sub TxtDatos_MouseClick(sender As Object, e As MouseEventArgs) Handles TxtDatos001.MouseClick, TxtDatos002.MouseClick, TxtDatos003.MouseClick, TxtDatos004.MouseClick, TxtDatos005.MouseClick, TxtDatos006.MouseClick, TxtDatos007.MouseClick, TxtDatos008.MouseClick, TxtDatos009.MouseClick, TxtDatos010.MouseClick, TxtDatos011.MouseClick
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_MouseClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEMouseClick(sender, e)

    End Sub
    Private Sub TxtDatos_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TxtDatos001.MouseDoubleClick, TxtDatos002.MouseDoubleClick, TxtDatos003.MouseDoubleClick, TxtDatos004.MouseDoubleClick, TxtDatos005.MouseDoubleClick, TxtDatos006.MouseDoubleClick, TxtDatos007.MouseDoubleClick, TxtDatos008.MouseDoubleClick, TxtDatos009.MouseDoubleClick, TxtDatos010.MouseDoubleClick, TxtDatos011.MouseDoubleClick
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_MouseDoubleClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEMouseDoubleClick(sender, e)

    End Sub

    Private Sub TxtDatos_Click(sender As Object, e As EventArgs) Handles TxtDatos001.Click, TxtDatos002.Click, TxtDatos003.Click, TxtDatos004.Click, TxtDatos005.Click, TxtDatos006.Click, TxtDatos007.Click, TxtDatos008.Click, TxtDatos009.Click, TxtDatos010.Click, TxtDatos011.Click
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Click " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDoubleClick(sender, e)

    End Sub

    Private Sub TxtDatos_Disposed(sender As Object, e As EventArgs) Handles TxtDatos001.Disposed, TxtDatos002.Disposed, TxtDatos003.Disposed, TxtDatos004.Disposed, TxtDatos005.Disposed, TxtDatos006.Disposed, TxtDatos007.Disposed, TxtDatos008.Disposed, TxtDatos009.Disposed, TxtDatos010.Disposed, TxtDatos011.Disposed
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Disposed " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDisposed(sender, e)

    End Sub

    Private Sub TxtDatos_DoubleClick(sender As Object, e As EventArgs) Handles TxtDatos001.DoubleClick, TxtDatos002.DoubleClick, TxtDatos003.DoubleClick, TxtDatos004.DoubleClick, TxtDatos005.DoubleClick, TxtDatos006.DoubleClick, TxtDatos007.DoubleClick, TxtDatos008.DoubleClick, TxtDatos009.DoubleClick, TxtDatos010.DoubleClick, TxtDatos011.DoubleClick
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_DoubleClick " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEDoubleClick(sender, e)

    End Sub

    Private Sub TxtDatos_Enter(sender As Object, e As EventArgs) Handles TxtDatos001.Enter, TxtDatos002.Enter, TxtDatos003.Enter, TxtDatos004.Enter, TxtDatos005.Enter, TxtDatos006.Enter, TxtDatos007.Enter, TxtDatos008.Enter, TxtDatos009.Enter, TxtDatos010.Enter, TxtDatos011.Enter
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Enter " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEEnter(sender, e)

    End Sub


    Private Sub TxtDatos_Leave(sender As Object, e As EventArgs) Handles TxtDatos001.Leave, TxtDatos002.Leave, TxtDatos003.Leave, TxtDatos004.Leave, TxtDatos005.Leave, TxtDatos006.Leave, TxtDatos007.Leave, TxtDatos008.Leave, TxtDatos009.Leave, TxtDatos010.Leave, TxtDatos011.Leave
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_Leave " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CELeave(sender, e)

    End Sub

    Private Sub TxtDatos_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TxtDatos001.PreviewKeyDown, TxtDatos002.PreviewKeyDown, TxtDatos003.PreviewKeyDown, TxtDatos004.PreviewKeyDown, TxtDatos005.PreviewKeyDown, TxtDatos006.PreviewKeyDown, TxtDatos007.PreviewKeyDown, TxtDatos008.PreviewKeyDown, TxtDatos009.PreviewKeyDown, TxtDatos010.PreviewKeyDown, TxtDatos011.PreviewKeyDown
        Dim CE As CnEdicion.CnEdicion, Indice As Integer

        ContadorTraza = ContadorTraza + 1
        Debug.Print(CStr(ContadorTraza) + " Mantenimiento_PreviewKeyDown " + sender.name + " " + DirectCast(sender, TextBox).Text)

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)
        CE.CEPreviewKeyDown(sender, e)

    End Sub


    Private Sub CmdFecha_Click(sender As Object, e As EventArgs) Handles CmdFecha005.Click, CmdFecha010.Click, CmdFecha012.Click, CmdFecha013.Click
        Dim X0 As Integer, X1 As Integer, X2 As Integer, X3 As Integer, X4 As Integer
        Dim Y0 As Integer, Y1 As Integer, Y2 As Integer, Y3 As Integer, Y4 As Integer
        Dim CE As CnEdicion.CnEdicion, CT As CnTabla.CnTabla, Indice As Integer

        Indice = CInt(Microsoft.VisualBasic.Right(sender.name, 3))
        CE = XCNE(Indice)

        X0 = Me.Location.X
        X1 = Me.PanelCentral.Location.X
        X2 = Me.TabGeneral.Location.X
        X3 = DirectCast(sender, Button).Location.X
        X4 = X4 + DirectCast(sender, Button).Width

        Y0 = Me.Location.Y
        Y1 = Me.PanelCentral.Location.Y
        Y2 = Me.TabGeneral.Location.Y
        Y3 = DirectCast(sender, Button).Location.Y
        Y4 = X4 + DirectCast(sender, Button).Height

        FrmFecha.X = X0 + X1 + X2 + X3 + X4 + 6
        FrmFecha.Y = Y0 + Y1 + Y2 + Y3 + 34

        If CE.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Fecha Then
            FrmFecha.EsFechaHora = False
        ElseIf CE.TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.FechaHora Then
            FrmFecha.EsFechaHora = True
        Else
            MsgBox("¿De qué tipo es el campo?")
        End If

        FrmFecha.ShowDialog()
        If FrmFecha.HayResultado = True Then
            CT = XCT(CE.NumeroTablaFormulario)
            CT.AsignarValor(CE.Campo, CStr(FrmFecha.FechaResultado))
        End If
    End Sub
End Class
