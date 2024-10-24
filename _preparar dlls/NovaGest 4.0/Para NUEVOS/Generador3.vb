Imports System.Text.RegularExpressions
Imports System.ComponentModel

Public Class Generador3

    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Dim DatosGrabados(200) As Boolean
    Dim MatrizOcupacion(19, 2, 36, 1) As String  'Corresponde el Grid de pantalla, no a la generación (formulario)
    '                   i=0 TabCabecera TP00  i=1 TabCabecera TP01     i=9 TabCabecera TP09
    '                       i=10 TabGeneral TabPage00     i=1 TabGeneral TabPage01        i=19 TabGeneral TabPage09
    '                   j=0 ColumnaIzquierda   j=1 ColumnaCentral   j=2 ColumnaDerecha
    '                   k= Número de fila

    '                   En l=0 se almacena el Número de campo (pos en Datos())  
    '                   En l=1 se almacena el tipo de dato grabado en esa posición:
    '                         C | L1 | L2 | L3 | C L1 | C L2| C L3| L1 L2| L2 L3| L1 L2 L3
    '                   Si la posición está vacía, entonces MatrizOcupacion(i,j,k,0)=""  MatrizOcupacion(i,j,k,1)=""

    Dim XOcupacion(19, 2, 36) As Integer 'Corresponde el Grid de pantalla, no a la generación (formulario)
    '                   i=0 TabCabecera TP00  i=1 TabCabecera TP01     i=9 TabCabecera TP09
    '                       i=10 TabGeneral TabPage00     i=1 TabGeneral TabPage01        i=19 TabGeneral TabPage09
    '                   j = 0 --> Columna de la izquierda   j = 1 --> Columna Central,   j=2 --> Columna de la derecha
    '                   k= Número de fila
    '                   Valor = Ultima X ocupada en la fila
    Dim MensajeError As String
    Dim FlagExitoEnGeneracion As Boolean = False

    'VARIABLES GENERACION DE FORMULARIO (Ver el resto en DatosGeneracion.bas)

    Dim DirectorioBase As String
    Dim DirectorioRecursos As String
    Dim DirectorioSalida As String
    Dim DirectorioDefinitivo As String
    Dim DirectorioCopiasEliminadas As String

    Dim FicheroSalida As New cnFichero.cnFichero
    Dim FicheroEntrada As New cnFichero.cnFichero
    Dim FicheroEntradaAuxiliar As New cnFichero.cnFichero

    Dim MantenimientoCodigo As String
    Dim MantenimientoDesigner As String
    Dim MantenimientoRESX As String

    'Dim TablaPrincipalGeneracion As String
    'Dim TablaPrincipalPlantilla As String = "TablaPlantilla"
    'Dim NombreFormularioGeneracion As String
    'Dim NombreFormularioPlantilla As String = "PlantillaMantenimiento"

    Dim ContadorCodigo As Integer = 0
    Dim ContadorDesigner As Integer = 0
    Dim ContadorRESX As Integer = 0

    Dim GeneracionUltimaX(19, 36) As Integer  'Corresponde al formulario, no al Grid de pantalla
    '                                             Representa la última X ocupada en el el tab y fila indicados del formulario
    '                   i=0 TabCabecera TP00  i=1 TabCabecera TP01     i=9 TabCabecera TP09
    '                       i=10 TabGeneral TabPage00     i=1 TabGeneral TabPage01        i=19 TabGeneral TabPage09
    '                       j= Número de fila
    '                   Valor = Ultima coordenada X ocupada en la fila en el formulario


    Private Sub Generador3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InicializarValores()

        'Generación
        DirectorioBase = "D:\Nuevo desarrollo\__NovaGest 2.0\Generador"
        DirectorioRecursos = Trim(DirectorioBase) + "\__Recursos para el Generador"
        DirectorioSalida = Trim(DirectorioBase) + "\__Mantenimientos Generados"
        DirectorioDefinitivo = "D:\Nuevo desarrollo\__NovaGest 2.0\NovaGest 2.0"
        DirectorioCopiasEliminadas = "D:\Nuevo desarrollo\__NovaGest 2.0\Generador\__Recursos Formularios sustituidos automáticamente"
        GeneracionPosicionColumnas(0) = 8 : GeneracionPosicionColumnas(1) = 388 : GeneracionPosicionColumnas(2) = 776


    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    '
    ' ========================= BLOQUE GENRACION DE MODELO ===============================
    '


    Private Sub CmdModelos_Click(sender As Object, e As EventArgs) Handles CmdModelo10.Click, CmdModelo20.Click, CmdModelo30.Click, CmdModelo13.Click, CmdModelo22.Click, CmdModelo23.Click, CmdModelo31.Click, CmdModelo32.Click, CmdModelo33.Click
        Dim Cadena As String, j1 As Integer, j2 As Integer, Salida As Boolean, MensajeError As String

        FlagExitoEnGeneracion = False
        Cadena = DirectCast(sender, Button).Name
        j1 = CInt(Mid(Cadena, Microsoft.VisualBasic.Len(Cadena) - 1, 1))
        j2 = CInt(Microsoft.VisualBasic.Right(Cadena, 1))

        If j2 = 0 Then
            MensajeError = "No es posible diseñar el mantenimiento con un único Tab en formato de " + CStr(j1) + " col."
        Else
            MensajeError = "No es posible diseñar el mantenimiento con dos Tabs (en formatos de " + CStr(j1) + " y " + CStr(j2) + " columnas)"
        End If


        Salida = ProcedimientoCabecera()
        Do While Salida = False
            IncluyeFilaEnCabecera()
            Salida = ProcedimientoCabecera()
        Loop
        If Procedimiento(10, j1) = False Then
            If j2 = 0 Then
                MsgBox(MensajeError)
                Exit Sub
            Else
                If Procedimiento(11, j2) = False Then
                    MsgBox(MensajeError)
                    Exit Sub
                End If
            End If
        End If
        FlagExitoEnGeneracion = True
    End Sub


    Private Function Procedimiento(TabActual As Integer, CuantasColumnas As Integer)
        Dim i As Integer, FilaOcupada As Integer
        Dim L(5) As Integer
        Dim DondePongoCampo As String ' "I","C,"D"

        Procedimiento = False
        FilaOcupada = 1
        For i = 1 To CInt(Datos(0, 0))
            If DatosGrabados(i) = False Then
                L(0) = CInt(Datos(i, 34)) 'Ancho campo
                L(1) = CInt(Datos(i, 35)) 'Ancho Lookup 1
                L(2) = CInt(Datos(i, 36)) 'Ancho Lookup 2
                L(3) = CInt(Datos(i, 37)) 'Ancho Lookup 3
                L(4) = L(1) 'Total Lookups
                If L(2) > 0 Then L(4) = L(4) + 1 + L(2)
                If L(3) > 0 Then L(4) = L(4) + 1 + L(3)
                L(5) = L(0) 'Total Campo+ Lookups
                If L(4) > 0 Then L(5) = L(5) + 1 + L(4)

                '1 Columna
                If CuantasColumnas = 1 Then
                    If Trim(MatrizOcupacion(TabActual, 0, FilaOcupada, 1)) = "" Then
                        If (FilaOcupada + 1 = CapacidadFilasCuerpo And L(2) > 0 And L(3) > 0) Or (FilaOcupada = CapacidadFilasCuerpo And (L(2) > 0 Or L(3) > 0)) Then 'No cabrían los lookups
                            Exit Function
                        Else
                            PonerDato(i, 0, TabActual, 0, FilaOcupada)
                        End If

                    Else
                        If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                        FilaOcupada = FilaOcupada + 1
                        PonerDato(i, 0, TabActual, 0, FilaOcupada)
                    End If

                    If L(1) > 0 Then PonerDato(i, 1, TabActual, 1, FilaOcupada)
                    If L(2) > 0 Then
                        If XOcupacion(TabActual, 1, FilaOcupada) + 1 + L(2) > GeneracionAnchoColumna Then
                            If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                            FilaOcupada = FilaOcupada + 1
                            PonerDato(i, 2, TabActual, 1, FilaOcupada)
                        Else
                            PonerDato(i, 2, TabActual, 1, FilaOcupada)
                        End If
                    End If
                    If L(3) > 0 Then
                        If XOcupacion(TabActual, 1, FilaOcupada) + 1 + L(3) > GeneracionAnchoColumna Then
                            If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                            FilaOcupada = FilaOcupada + 1
                            PonerDato(i, 3, TabActual, 1, FilaOcupada)
                        Else
                            PonerDato(i, 3, TabActual, 1, FilaOcupada)
                        End If
                    End If
                    ' 2 Columnas    
                ElseIf CuantasColumnas = 2 Then
                    DondePongoCampo = ""
                    If Trim(MatrizOcupacion(TabActual, 0, FilaOcupada, 1)) = "" Then 'Izquierda libre 
                        PonerDato(i, 0, TabActual, 0, FilaOcupada)
                        DondePongoCampo = "I"
                    ElseIf Trim(MatrizOcupacion(TabActual, 1, FilaOcupada, 1)) = "" Then 'Centro libre    
                        'En principio la pomndría en C, pero, si no caben los lookups en este tab, no lo hago
                        If FilaOcupada = CapacidadFilasCuerpo And L(5) > GeneracionAnchoColumna And L(0) + 1 + L(1) > AnchoGeneralEtiquetas Then 'Los Lookups no cabrán en el tab
                            Exit Function
                        Else
                            PonerDato(i, 0, TabActual, 1, FilaOcupada)
                            DondePongoCampo = "C"
                        End If
                    Else
                        If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                        FilaOcupada = FilaOcupada + 1
                        PonerDato(i, 0, TabActual, 0, FilaOcupada)
                        DondePongoCampo = "I"
                    End If
                    If L(4) > 0 Then
                        If DondePongoCampo = "I" Then
                            If L(4) < GeneracionAnchoColumna Then
                                If L(1) > 0 Then PonerDato(i, 1, TabActual, 1, FilaOcupada)
                                If L(2) > 0 Then PonerDato(i, 2, TabActual, 1, FilaOcupada)
                                If L(3) > 0 Then PonerDato(i, 3, TabActual, 1, FilaOcupada)
                            Else
                                If L(1) > 0 Then PonerDato(i, 1, TabActual, 1, FilaOcupada)
                                If L(2) > 0 Then PonerDato(i, 2, TabActual, 2, FilaOcupada)
                                If L(3) > 0 Then PonerDato(i, 3, TabActual, 2, FilaOcupada)
                            End If
                        ElseIf DondePongoCampo = "C" Then
                            If L(4) < GeneracionAnchoColumna Then
                                If L(1) > 0 Then PonerDato(i, 1, TabActual, 2, FilaOcupada)
                                If L(2) > 0 Then PonerDato(i, 2, TabActual, 2, FilaOcupada)
                                If L(3) > 0 Then PonerDato(i, 3, TabActual, 2, FilaOcupada)
                            Else
                                If L(1) > 0 Then
                                    If XOcupacion(TabActual, 1, FilaOcupada) + 1 + L(1) < GeneracionAnchoColumna Then
                                        PonerDato(i, 1, TabActual, 1, FilaOcupada)
                                    Else
                                        PonerDato(i, 1, TabActual, 2, FilaOcupada)
                                    End If
                                End If
                                If L(2) > 0 Then
                                    PonerDato(i, 2, TabActual, 2, FilaOcupada)
                                End If
                                If L(3) > 0 Then
                                    If XOcupacion(TabActual, 2, FilaOcupada) + 1 + L(3) < GeneracionAnchoColumna Then
                                        PonerDato(i, 3, TabActual, 2, FilaOcupada)
                                    Else
                                        If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                                        FilaOcupada = FilaOcupada + 1
                                        PonerDato(i, 3, TabActual, 2, FilaOcupada)
                                    End If
                                End If
                            End If
                        End If
                    End If
                    '3 Columnas
                ElseIf CuantasColumnas = 3 Then
                    DondePongoCampo = ""
                    If Trim(MatrizOcupacion(TabActual, 0, FilaOcupada, 1)) = "" Then
                        PonerDato(i, 0, TabActual, 0, FilaOcupada)
                        DondePongoCampo = "I"
                    ElseIf Trim(MatrizOcupacion(TabActual, 1, FilaOcupada, 1)) = "" Then
                        PonerDato(i, 0, TabActual, 1, FilaOcupada)
                        DondePongoCampo = "C"
                    ElseIf Trim(MatrizOcupacion(TabActual, 2, FilaOcupada, 1)) = "" Then
                        If L(5) < GeneracionAnchoColumna Then
                            PonerDato(i, 0, TabActual, 2, FilaOcupada)
                            DondePongoCampo = "D"
                        Else
                            If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                            FilaOcupada = FilaOcupada + 1
                            PonerDato(i, 0, TabActual, 0, FilaOcupada)
                            DondePongoCampo = "I"
                        End If
                    Else ' Nueva fila
                        If FilaOcupada >= CapacidadFilasCuerpo Then Exit Function
                        FilaOcupada = FilaOcupada + 1
                        PonerDato(i, 0, TabActual, 0, FilaOcupada)
                        DondePongoCampo = "I"
                    End If
                    If L(4) > 0 Then
                        If DondePongoCampo = "I" Then
                            If L(1) > 0 Then
                                PonerDato(i, 1, TabActual, 1, FilaOcupada)
                            End If
                            If L(2) > 0 Then
                                PonerDato(i, 2, TabActual, 2, FilaOcupada)
                            End If
                            If L(3) > 0 Then
                                PonerDato(i, 3, TabActual, 2, FilaOcupada)
                            End If
                        ElseIf DondePongoCampo = "C" Then
                            If L(1) > 0 Then
                                If XOcupacion(TabActual, 1, FilaOcupada) + 1 + L(1) < GeneracionAnchoColumna Then
                                    PonerDato(i, 1, TabActual, 1, FilaOcupada)
                                Else
                                    PonerDato(i, 1, TabActual, 2, FilaOcupada)
                                End If
                            End If
                            If L(2) > 0 Then
                                PonerDato(i, 2, TabActual, 2, FilaOcupada)
                            End If
                            If L(3) > 0 Then
                                PonerDato(i, 3, TabActual, 2, FilaOcupada)
                            End If
                        ElseIf DondePongoCampo = "D" Then 'Solo se ha utilizado la derecha para el campo si caben todos los datos en esa columna
                            If L(1) > 0 Then
                                PonerDato(i, 1, TabActual, 2, FilaOcupada)
                            End If
                            If L(2) > 0 Then
                                PonerDato(i, 2, TabActual, 2, FilaOcupada)
                            End If
                            If L(3) > 0 Then
                                PonerDato(i, 3, TabActual, 2, FilaOcupada)
                            End If
                        End If
                    End If
                End If
            End If
        Next
        Procedimiento = True
    End Function


    Private Function ProcedimientoCabecera() As Boolean
        Dim i As Integer, CuantosPK As Integer, FilaOcupada As Integer, NecesitaFilas As Integer

        ProcedimientoCabecera = True
        InicializarValores()
        CuantosPK = CInt(Datos(0, 1))

        '=== TAB CABECERA ===

        FilaOcupada = 0
        'Campos de la clave primaria
        For j = 1 To CuantosPK
            For i = 1 To CInt(Datos(0, 0))
                If CInt(Datos(i, 10)) = j Then
                    If FilaOcupada >= CapacidadFilasCabecera Then
                        ProcedimientoCabecera = False
                        Exit Function
                    End If
                    FilaOcupada = FilaOcupada + 1
                    PonerDato(i, 0, 0, 0, FilaOcupada)
                    If CInt(Datos(i, 35)) > 0 Then PonerDato(i, 1, 0, 1, FilaOcupada)
                    If CInt(Datos(i, 36)) > 0 Then
                        If FilaOcupada >= CapacidadFilasCabecera Then
                            ProcedimientoCabecera = False
                            Exit Function
                        End If
                        FilaOcupada = FilaOcupada + 1
                        PonerDato(i, 2, 0, 1, FilaOcupada)
                    End If
                    If CInt(Datos(i, 37)) > 0 Then
                        If FilaOcupada >= CapacidadFilasCabecera Then
                            ProcedimientoCabecera = False
                            Exit Function
                        End If
                        FilaOcupada = FilaOcupada + 1
                        PonerDato(i, 3, 0, 1, FilaOcupada)
                    End If
                    Exit For
                End If
            Next
        Next

        'Campo adicional SI cabe en cabecera
        If FilaOcupada < CapacidadFilasCabecera Then
            For i = 1 To CInt(Datos(0, 0))
                If CInt(Datos(i, 10)) = 0 Then 'Campo que no es de la clave primaria
                    NecesitaFilas = 1
                    If CInt(Datos(i, 36)) > 0 Then NecesitaFilas = NecesitaFilas + 1
                    If CInt(Datos(i, 37)) > 0 Then NecesitaFilas = NecesitaFilas + 1
                    If FilaOcupada + NecesitaFilas <= CapacidadFilasCabecera Then
                        FilaOcupada = FilaOcupada + 1
                        PonerDato(i, 0, 0, 0, FilaOcupada)
                        If CInt(Datos(i, 35)) > 0 Then PonerDato(i, 1, 0, 1, FilaOcupada)
                        If CInt(Datos(i, 36)) > 0 Then
                            FilaOcupada = FilaOcupada + 1
                            PonerDato(i, 2, 0, 1, FilaOcupada)
                        End If
                        If CInt(Datos(i, 37)) > 0 Then
                            FilaOcupada = FilaOcupada + 1
                            PonerDato(i, 3, 0, 1, FilaOcupada)
                        End If
                    End If
                    Exit For 'Solo se intenta el primer campo no pk
                End If
            Next
        End If

    End Function


    Private Sub PonerDato(NumeroDato As Integer, EsLookup As Integer, QueTab As Integer, QueColumna As Integer, QueFila As Integer)
        Dim DatoParaMatrizOcupacion As String, NuevoDatoParaMatrizOcupacion As String, XAnterior As Integer

        'EsLookup = 0 --> Solo campo / Eslookup =1 a 3 NúmLookup / EsLookup = 12 Lookups y y 2 / EsLookup = 23 Lookups 2 y 3 / EsLookup = 123 Todos los Lookups
        'EsLookup = 10 ---> Campo + Lookups
        'QueTab = 0 --> TabCabecera TP00, QueTab = 10 --> TabGeneral TabPage00, QueTab = 11 --> TabGeneral TabPage01
        'QueColumna = 0 --> IZQUIERDA    QueColumna = 1 --> DERECHA
        'QueFila  1 a 3 en TabCabecera   1 a 36 en TabPage00

        DatoParaMatrizOcupacion = MatrizOcupacion(QueTab, QueColumna, QueFila, 1) 'Dato anterior en esa posición
        NuevoDatoParaMatrizOcupacion = ""
        If EsLookup = 0 Then
            NuevoDatoParaMatrizOcupacion = "C"
        ElseIf EsLookup >= 1 And EsLookup <= 3 Then
            NuevoDatoParaMatrizOcupacion = "L" + CStr(EsLookup)
        Else
            MsgBox("No sé")
        End If
        If Trim(DatoParaMatrizOcupacion) > "" Then
            DatoParaMatrizOcupacion = Trim(DatoParaMatrizOcupacion) + " " + Trim(NuevoDatoParaMatrizOcupacion)
        Else
            DatoParaMatrizOcupacion = Trim(NuevoDatoParaMatrizOcupacion)
        End If
        MatrizOcupacion(QueTab, QueColumna, QueFila, 1) = DatoParaMatrizOcupacion
        MatrizOcupacion(QueTab, QueColumna, QueFila, 0) = CStr(NumeroDato)

        XAnterior = XOcupacion(QueTab, QueColumna, QueFila)
        If XAnterior > 0 Then XAnterior = XAnterior + 1
        If EsLookup = 0 Then
            XOcupacion(QueTab, QueColumna, QueFila) = XAnterior + 1 + CInt(Datos(NumeroDato, 34))
        ElseIf EsLookup >= 1 And EsLookup <= 3 Then
            XOcupacion(QueTab, QueColumna, QueFila) = XAnterior + 1 + CInt(Datos(NumeroDato, 34 + EsLookup))
        Else
            MsgBox("Ni idea")
        End If
        PintarEnGrid(NumeroDato, EsLookup, QueTab, QueColumna, QueFila, DatoParaMatrizOcupacion)
        DatosGrabados(NumeroDato) = True
    End Sub

    Private Sub PintarEnGrid(NumeroDato As Integer, EsLookup As Integer, QueTab As Integer, QueColumna As Integer, QueFila As Integer, DatoOcupacion As String)
        Dim DG As DataGridView, IZQ As Integer, Ancho As Integer

        If QueTab = 0 Then
            DG = DGCabecera
        ElseIf QueTab = 10 Then
            DG = DGTabPage00
        ElseIf QueTab = 11 Then
            DG = DGTabPage01
        Else
            MsgBox("Ese tab no está previsto")
            Exit Sub
        End If

        IZQ = 3 * QueColumna

        DG(1 + IZQ, QueFila - 1).Value = Trim(Datos(NumeroDato, 1)) 'Campo
        DG(2 + IZQ, QueFila - 1).Value = Trim(DatoOcupacion)
        Ancho = 0
        If IsNumeric(DG(3 + IZQ, QueFila - 1).Value) = True Then
            If CInt(DG(3 + IZQ, QueFila - 1).Value) > 0 Then
                Ancho = CInt(DG(3 + IZQ, QueFila - 1).Value) + 1
            End If
        End If
        DG(3 + IZQ, QueFila - 1).Value = CStr(Datos(NumeroDato, 34 + EsLookup)) + Ancho

    End Sub

    Private Sub IncluyeFilaEnCabecera()
        CapacidadFilasCabecera = CapacidadFilasCabecera + 1
        CapacidadFilasCuerpo = CapacidadFilasCuerpo - 1

        DGTabPage00.Height = DGTabPage00.Height - HeightNuevaFila
        DGTabPage01.Height = DGTabPage01.Height - HeightNuevaFila

        TabGeneral.Height = TabGeneral.Height - HeightNuevaFila
        TabPage00.Height = TabPage00.Height - HeightNuevaFila
        TabPage01.Height = TabPage01.Height - HeightNuevaFila
        TabGeneral.Top = TabGeneral.Top + HeightNuevaFila

        DGCabecera.Height = DGCabecera.Height + HeightNuevaFila

    End Sub
    Private Sub InicializarValores()
        Dim i As Integer, j As Integer, k As Integer

        ' TabCabecera 
        DGCabecera.Rows.Clear()
        DGCabecera.AllowUserToAddRows = False
        For i = 0 To CapacidadFilasCabecera - 1
            DGCabecera.Rows.Add()
            DGCabecera(0, i).Value = CStr(i + 1)
        Next
        For j = 4 To 6
            DGCabecera.Columns(j).DefaultCellStyle.BackColor = Color.LightGray
        Next

        ' TabPage00 
        DGTabPage00.Rows.Clear()
        DGTabPage00.AllowUserToAddRows = False
        For i = 0 To CapacidadFilasCuerpo - 1
            DGTabPage00.Rows.Add()
            DGTabPage00(0, i).Value = CStr(i + 1)
        Next
        For j = 4 To 6
            DGTabPage00.Columns(j).DefaultCellStyle.BackColor = Color.LightGray
        Next

        ' TabPage01
        DGTabPage01.Rows.Clear()
        DGTabPage01.AllowUserToAddRows = False
        For i = 0 To CapacidadFilasCuerpo - 1
            DGTabPage01.Rows.Add()
            DGTabPage01(0, i).Value = CStr(i + 1)
        Next
        For j = 4 To 6
            DGTabPage01.Columns(j).DefaultCellStyle.BackColor = Color.LightGray
        Next

        For i = 1 To CInt(Datos(0, 0)) : DatosGrabados(i) = False : Next

        For i = 0 To 19
            For j = 0 To 2
                For k = 0 To 36
                    MatrizOcupacion(i, j, k, 0) = "0"
                    MatrizOcupacion(i, j, k, 1) = ""
                    XOcupacion(i, j, k) = 0
                Next
            Next
        Next

    End Sub


    '
    ' ========================= BLOQUE GENRACION DE MANTENIMIENTO ===============================
    '

    Private Sub CmdGenerarMantenimiento_Click(sender As Object, e As EventArgs) Handles CmdGenerarMantenimiento.Click
        Dim i As Integer, j As Integer, k As Integer

        If Trim(TxtNombreFormulario.Text) = "" Then
            MsgBox("Es preciso indicar nombre para el formulario a generar")
            Exit Sub
        End If
        If FlagExitoEnGeneracion = False Then
            MsgBox("No se ha generado un modelo correcto de mantenimiento")
            Exit Sub
        End If
        GeneracionNombreFormulario = Trim(TxtNombreFormulario.Text)

        For i = 0 To 19 : GeneracionContenedorUsado(i) = False : Next
        For i = 0 To 19
            For j = 0 To 36
                GeneracionUltimaX(i, j) = 0
            Next
        Next

        CopiarPlantilla()
        GeneracionIndiceUltimoControl = 0
        ControlesMantenimiento()
        '        ControlesTabgeneral()
        LLevarANovagest()
        MsgBox("Se ha generado el mantenimiento.")
    End Sub

    Private Sub CopiarPlantilla()
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String
        Dim TabCabeceraH As Integer, TabGeneralH As Integer, TabGeneralT As Integer

        'Resx
        CadenaFicheroEntrada = Trim(DirectorioRecursos) + "\PlantillaMantenimiento.resx.gen"
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroSalida = Trim(DirectorioSalida) + "\" + Trim(GeneracionNombreFormulario) + ".resx"
        MantenimientoRESX = Trim(CadenaFicheroSalida)
        ContadorRESX = 0
        CadenaFicheroSalida = Trim(MantenimientoRESX) + Format(ContadorRESX, "0000")

        FicheroSalida.Open(CadenaFicheroSalida, True)
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Código
        CadenaFicheroEntrada = Trim(DirectorioRecursos) + "\PlantillaMantenimiento.vb.gen"
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroSalida = Trim(DirectorioSalida) + "\" + Trim(GeneracionNombreFormulario) + ".vb"
        MantenimientoCodigo = Trim(CadenaFicheroSalida)
        ContadorCodigo = 0
        CadenaFicheroSalida = Trim(CadenaFicheroSalida) + Format(ContadorCodigo, "0000")

        FicheroSalida.Open(CadenaFicheroSalida, True)
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            If InStr(CadenaL, "<NOMBREFORMULARIO>") > 0 Then CadenaL = Replace(CadenaL, "<NOMBREFORMULARIO>", GeneracionNombreFormulario)
            If InStr(CadenaL, "<TABLAFORMULARIO>") > 0 Then CadenaL = Replace(CadenaL, "<TABLAFORMULARIO>", GeneracionTablaPrincipal)
            If InStr(CadenaL, "<TABGENERAL.HEIGHT>") > 0 Then CadenaL = Replace(CadenaL, "<TABGENERAL.HEIGHT>", CStr(TabGeneralH))
            If InStr(CadenaL, "<TABGENERAL.TOP>") > 0 Then CadenaL = Replace(CadenaL, "<TABGENERAL.TOP>", CStr(TabGeneralT))
            If InStr(CadenaL, "<TABCABECERA.HEIGHT>") > 0 Then CadenaL = Replace(CadenaL, "<TABCABECERA.HEIGHT>", CStr(TabCabeceraH))
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Designer
        CadenaFicheroEntrada = Trim(DirectorioRecursos) + "\PlantillaMantenimiento.Designer.vb.gen"
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroSalida = Trim(DirectorioSalida) + "\" + Trim(TxtNombreFormulario.Text) + ".Designer.vb"
        MantenimientoDesigner = Trim(CadenaFicheroSalida)
        ContadorDesigner = 0
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")

        'Los tamaños de TabCabecera y TabGeneral están pensados para 3 y 36 filas. Aquí se ajustan al número de filas real
        TabCabeceraH = 78 + (CapacidadFilasCabecera - 3) * 21
        TabGeneralH = 789 - (36 - CapacidadFilasCuerpo) * 21
        TabGeneralT = 79 + (36 - CapacidadFilasCuerpo) * 21

        FicheroSalida.Open(CadenaFicheroSalida, True)
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            If InStr(CadenaL, "<NOMBREFORMULARIO>") > 0 Then CadenaL = Replace(CadenaL, "<NOMBREFORMULARIO>", GeneracionNombreFormulario)
            If InStr(CadenaL, "<TABLAFORMULARIO>") > 0 Then CadenaL = Replace(CadenaL, "<TABLAFORMULARIO>", GeneracionTablaPrincipal)
            If InStr(CadenaL, "<TABGENERAL.HEIGHT>") > 0 Then CadenaL = Replace(CadenaL, "<TABGENERAL.HEIGHT>", CStr(TabGeneralH))
            If InStr(CadenaL, "<TABGENERAL.TOP>") > 0 Then CadenaL = Replace(CadenaL, "<TABGENERAL.TOP>", CStr(TabGeneralT))
            If InStr(CadenaL, "<TABCABECERA.HEIGHT>") > 0 Then CadenaL = Replace(CadenaL, "<TABCABECERA.HEIGHT>", CStr(TabCabeceraH))

            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()
    End Sub

    Private Sub ControlesMantenimiento()
        Dim NumeroTab As Integer = 0, Fila As Integer, ColICD As Integer
        Dim DatoAColocar As Integer, CapacidadFilas As Integer, FlagDatoAPegar As Boolean, IndiceEnUso As Integer


        For NumeroTab = 0 To 19   'TabCabecera/TP00 --> 0.    TabGeneral/TabPage00 --> 10. TabGeneral/TabPage01 --> 11
            If NumeroTab = 0 Or NumeroTab = 10 Or NumeroTab = 11 Then 'Provisional hasta el mtmto. multitabla ocupe otros Tabs
                If NumeroTab = 0 Then CapacidadFilas = CapacidadFilasCabecera Else CapacidadFilas = CapacidadFilasCuerpo
                For Fila = 1 To CapacidadFilas
                    For ColICD = 0 To 2
                        If MatrizOcupacion(NumeroTab, ColICD, Fila, 0) > 0 Then
                            DatoAColocar = MatrizOcupacion(NumeroTab, ColICD, Fila, 0)
                            LblEnProceso.Text = Datos(DatoAColocar, 1) + " Fila:" + CStr(Fila)
                            LblEnProceso.Refresh()

                            'MsgBox("Voy a pintar el dato: " + CStr(Datos(DatoAColocar, 1)))
                            If InStr(MatrizOcupacion(NumeroTab, ColICD, Fila, 1), "C") > 0 Then
                                GeneracionIndiceUltimoControl = GeneracionIndiceUltimoControl + 1
                                Datos(DatoAColocar, 39) = CStr(GeneracionIndiceUltimoControl)
                                IndiceEnUso = CInt(Datos(DatoAColocar, 39)) 'Porque el dato a pintar no tiene que corresponder necesariamente al último campo (filas con lookup cuyo campo se pintó en la fila anterior)
                                IncluirEtiqueta(NumeroTab, "Lbl" + Format(IndiceEnUso, "000"), Datos(DatoAColocar, 4) + ":", Fila, ColICD)
                                IncluirTxtDatos(NumeroTab, "TxtDatos" + Format(IndiceEnUso, "000"), Fila, ColICD, CInt(Datos(DatoAColocar, 32)))
                                IncluirCEdicion(NumeroTab, "CnEdicion" + Format(IndiceEnUso, "000"), Fila, ColICD, Datos(DatoAColocar, 1), DatoAColocar)

                                If Trim(Datos(DatoAColocar, 5)) = "S" Then
                                    IncluirCmdBoton(NumeroTab, "CmdFecha" + Format(IndiceEnUso, "000"), Fila, ColICD)
                                End If
                                If Trim(Datos(DatoAColocar, 6)) = "S" Then
                                    IncluirCmdBoton(NumeroTab, "CmdCombo" + Format(IndiceEnUso, "000"), Fila, ColICD)
                                End If
                                If Trim(Datos(DatoAColocar, 7)) = "S" Then
                                    IncluirCmdBoton(NumeroTab, "CmdGrid" + Format(IndiceEnUso, "000"), Fila, ColICD)
                                End If
                                If Trim(Datos(DatoAColocar, 8)) = "S" Then
                                    IncluirCmdBoton(NumeroTab, "CmdMantenimiento" + Format(IndiceEnUso, "000"), Fila, ColICD)
                                End If
                            End If
                            If InStr(MatrizOcupacion(NumeroTab, ColICD, Fila, 1), "L1") > 0 Then
                                DatoAColocar = MatrizOcupacion(NumeroTab, ColICD, Fila, 0)
                                IndiceEnUso = CInt(Datos(DatoAColocar, 39))

                                If CInt(Datos(DatoAColocar, 11)) > 0 Then
                                    FlagDatoAPegar = False
                                    If ColICD > 0 Then
                                        If MatrizOcupacion(NumeroTab, ColICD - 1, Fila, 0) = DatoAColocar Then
                                            FlagDatoAPegar = True
                                        End If
                                    End If
                                    If FlagDatoAPegar = False Then
                                        IncluirTxtLookup(NumeroTab, "TxtLookup" + Format(IndiceEnUso, "000") + "1", Fila, ColICD, CInt(Datos(DatoAColocar, 35)))
                                    Else
                                        'Coloco el dato a izquierda, para que el control se pegue en la primera posición libre de la fila
                                        IncluirTxtLookup(NumeroTab, "TxtLookup" + Format(IndiceEnUso, "000") + "1", Fila, 0, CInt(Datos(DatoAColocar, 35)))
                                    End If
                                End If
                            End If

                            If InStr(MatrizOcupacion(NumeroTab, ColICD, Fila, 1), "L2") > 0 Then
                                DatoAColocar = MatrizOcupacion(NumeroTab, ColICD, Fila, 0)
                                IndiceEnUso = CInt(Datos(DatoAColocar, 39))

                                If CInt(Datos(DatoAColocar, 16)) > 0 Then
                                    FlagDatoAPegar = False
                                    If ColICD > 0 Then
                                        If MatrizOcupacion(NumeroTab, ColICD - 1, Fila, 0) = DatoAColocar Then
                                            FlagDatoAPegar = True
                                        End If
                                    End If
                                    If FlagDatoAPegar = False Then
                                        IncluirTxtLookup(NumeroTab, "TxtLookup" + Format(IndiceEnUso, "000") + "2", Fila, ColICD, CInt(Datos(DatoAColocar, 36)))
                                    Else
                                        'Coloco el dato a izquierda, para que el control se pegue en la primera posición libre de la fila
                                        IncluirTxtLookup(NumeroTab, "TxtLookup" + Format(IndiceEnUso, "000") + "2", Fila, 0, CInt(Datos(DatoAColocar, 36)))
                                    End If
                                End If
                            End If
                            If InStr(MatrizOcupacion(NumeroTab, ColICD, Fila, 1), "L3") > 0 Then
                                DatoAColocar = MatrizOcupacion(NumeroTab, ColICD, Fila, 0)
                                IndiceEnUso = CInt(Datos(DatoAColocar, 39))

                                If CInt(Datos(DatoAColocar, 21)) > 0 Then
                                    FlagDatoAPegar = False
                                    If ColICD > 0 Then
                                        If MatrizOcupacion(NumeroTab, ColICD - 1, Fila, 0) = DatoAColocar Then
                                            FlagDatoAPegar = True
                                        End If
                                    End If
                                    If FlagDatoAPegar = False Then
                                        IncluirTxtLookup(NumeroTab, "TxtLookup" + Format(IndiceEnUso, "000") + "3", Fila, ColICD, CInt(Datos(DatoAColocar, 37)))
                                    Else
                                        'Coloco el dato a izquierda, para que el control se pegue en la primera posición libre de la fila
                                        IncluirTxtLookup(NumeroTab, "TxtLookup" + Format(IndiceEnUso, "000") + "3", Fila, 0, CInt(Datos(DatoAColocar, 37)))
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        Next
    End Sub

    Private Sub IncluirEtiqueta(Contenedor As Integer, NombreEtiqueta As String, TextoEtiqueta As String, FilaE As Integer, ColumnaE As Integer)
        Dim TopE As Integer, LeftE As Integer
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String, CadenaL2 As String
        Dim V() As String, CadenaBuscada As String
        Dim BloqueEncontrado As Boolean, LineaEncontrada As Boolean, Cadena As String, FlagSalto As Boolean

        TopE = GeneracionPosicionPrimeraFila + 21 * (FilaE - 1)
        LblNoQuitar.Text = Trim(TextoEtiqueta)
        LeftE = GeneracionPosicionColumnas(ColumnaE) + AnchoGeneralEtiquetas - LblNoQuitar.Width
        If LeftE > 0 And LeftE <= GeneracionUltimaX(Contenedor, FilaE) Then
            LeftE = GeneracionUltimaX(Contenedor, FilaE) + 1
        End If

        If GeneracionContenedorUsado(Contenedor) = False Then
            GeneracionContenedorUsado(Contenedor) = True
            ContenedorLayout(Contenedor)
        End If

        'Bloque InitializeComponent (Incluir etiqueta)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 2 Then
                    If V(0) = "Private" And V(1) = "Sub" And V(2) = "InitializeComponent()" Then
                        BloqueEncontrado = True
                        If Contenedor < 10 Then
                            CadenaBuscada = "Me.TP" + Format(Contenedor, "00")
                        Else
                            CadenaBuscada = "Me.TabPage" + Format(Contenedor - 10, "00")
                        End If
                    End If
                End If
                If UBound(V, 1) >= 3 Then
                    If BloqueEncontrado = True Then
                        If V(0) = CadenaBuscada And V(1) = "=" And V(2) = "New" Then
                            LineaEncontrada = True
                        End If
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
            If BloqueEncontrado = True And LineaEncontrada = True Then
                CadenaL = "Me." + Trim(NombreEtiqueta) + " = New System.Windows.Forms.Label()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque de cada control
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        If Contenedor < 10 Then
            CadenaBuscada = "'TP" + Format(Contenedor, "00")
        Else
            CadenaBuscada = "'TabPage" + Format(Contenedor - 10, "00")
        End If
        FlagSalto = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = CadenaBuscada And BloqueEncontrado = False Then
                        BloqueEncontrado = True
                        Debug.Print(CadenaL)
                        FicheroSalida.EscribeLinea(CadenaL)
                        CadenaL = FicheroEntrada.LeeLinea()
                        FicheroSalida.EscribeLinea(CadenaL) 'Esto graba la línea siguiente que contiene solo un ', porque necesitamos buscar la siguiente aparición de un '
                        CadenaBuscada = "'"
                        FlagSalto = True
                    End If
                End If
                If FlagSalto = False Then
                    If UBound(V, 1) = 0 Then
                        If BloqueEncontrado = True Then
                            If V(0) = CadenaBuscada Then
                                LineaEncontrada = True
                            End If
                        End If
                    End If
                End If
            End If
            If FlagSalto = True Then
                FlagSalto = False
            Else
                If BloqueEncontrado = True And LineaEncontrada = True Then
                    'Esta línea es del bloque del controlador
                    If Contenedor < 10 Then
                        CadenaL2 = "Me.TP" + Format(Contenedor, "00") + ".Controls.Add(Me." + Trim(NombreEtiqueta) + ")"
                    Else
                        CadenaL2 = "Me.TabPage" + Format(Contenedor - 10, "00") + ".Controls.Add(Me." + Trim(NombreEtiqueta) + ")"
                    End If
                    FicheroSalida.EscribeLinea("'<<MarcaParaCE>>") 'Para colocar luego en CnEdicion antes y que esté arriba del TxtDatos en el formulario
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'Este es el nuevo bloque del control
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'" + Trim(NombreEtiqueta)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreEtiqueta + ".AutoSize = True"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreEtiqueta + ".Location = New System.Drawing.Point(" + CStr(LeftE) + "," + CStr(TopE) + ")"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreEtiqueta + ".Name = " + Chr(34) + NombreEtiqueta + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreEtiqueta + ".Size = New System.Drawing.Size(41, 13)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreEtiqueta + ".Tabindex = 9999"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreEtiqueta + ".Text = " + Chr(34) + Trim(TextoEtiqueta) + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)

                    BloqueEncontrado = False
                    LineaEncontrada = False
                    CadenaBuscada = "PARA NO REPETIR"
                End If
                Debug.Print(CadenaL)
                FicheroSalida.EscribeLinea(CadenaL)
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque Friends
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        Debug.Print("Friends " + Trim(NombreEtiqueta))

        BloqueEncontrado = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 1 Then
                    If V(0) = "End" And V(1) = "Class" Then
                        CadenaL2 = "Friend WithEvents " + Trim(NombreEtiqueta) + " As Label"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        GeneracionUltimaX(Contenedor, FilaE) = LeftE + LblNoQuitar.Width + 3
    End Sub

    Private Sub IncluirTxtDatos(Contenedor As Integer, NombreTxtDatos As String, FilaE As Integer, ColumnaE As Integer, AnchoTxtDatos As Integer)

        Dim TopE As Integer, LeftE As Integer
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String, CadenaL2 As String
        Dim V() As String, CadenaBuscada As String
        Dim BloqueEncontrado As Boolean, LineaEncontrada As Boolean, Cadena As String, FlagSalto As Boolean

        TopE = GeneracionPosicionPrimeraFila + 21 * (FilaE - 1) - 2
        LeftE = GeneracionPosicionColumnas(ColumnaE)
        If LeftE > 0 And LeftE <= GeneracionUltimaX(Contenedor, FilaE) Then
            LeftE = GeneracionUltimaX(Contenedor, FilaE) + 1
        End If

        If GeneracionContenedorUsado(Contenedor) = False Then
            GeneracionContenedorUsado(Contenedor) = True
            ContenedorLayout(Contenedor)
        End If

        'Bloque InitializeComponent (Incluir textbox)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 2 Then
                    If V(0) = "Private" And V(1) = "Sub" And V(2) = "InitializeComponent()" Then
                        BloqueEncontrado = True
                        If Contenedor < 10 Then
                            CadenaBuscada = "Me.TP" + Format(Contenedor, "00")
                        Else
                            CadenaBuscada = "Me.TabPage" + Format(Contenedor - 10, "00")
                        End If
                    End If
                End If
                If UBound(V, 1) >= 3 Then
                    If BloqueEncontrado = True Then
                        If V(0) = CadenaBuscada And V(1) = "=" And V(2) = "New" Then
                            LineaEncontrada = True
                        End If
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
            If BloqueEncontrado = True And LineaEncontrada = True Then
                CadenaL = "Me." + Trim(NombreTxtDatos) + " = New System.Windows.Forms.TextBox()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque de cada control
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        If Contenedor < 10 Then
            CadenaBuscada = "'TP" + Format(Contenedor, "00")
        Else
            CadenaBuscada = "'TabPage" + Format(Contenedor - 10, "00")
        End If
        FlagSalto = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = CadenaBuscada And BloqueEncontrado = False Then
                        BloqueEncontrado = True
                        Debug.Print(CadenaL)
                        FicheroSalida.EscribeLinea(CadenaL)
                        CadenaL = FicheroEntrada.LeeLinea()
                        FicheroSalida.EscribeLinea(CadenaL) 'Esto graba la línea siguiente que contiene solo un ', porque necesitamos buscar la siguiente aparición de un '
                        CadenaBuscada = "'"
                        FlagSalto = True
                    End If
                End If
                If FlagSalto = False Then
                    If UBound(V, 1) = 0 Then
                        If BloqueEncontrado = True Then
                            If V(0) = CadenaBuscada Then
                                LineaEncontrada = True
                            End If
                        End If
                    End If
                End If
            End If
            If FlagSalto = True Then
                FlagSalto = False
            Else
                If BloqueEncontrado = True And LineaEncontrada = True Then
                    'Esta línea es del bloque del controlador
                    If Contenedor < 10 Then
                        CadenaL2 = "Me.TP" + Format(Contenedor, "00") + ".Controls.Add(Me." + Trim(NombreTxtDatos) + ")"
                    Else
                        CadenaL2 = "Me.TabPage" + Format(Contenedor - 10, "00") + ".Controls.Add(Me." + Trim(NombreTxtDatos) + ")"
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'Este es el nuevo bloque del control
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'" + Trim(NombreTxtDatos)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtDatos + ".Location = New System.Drawing.Point(" + CStr(LeftE) + "," + CStr(TopE) + ")"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtDatos + ".Name = " + Chr(34) + NombreTxtDatos + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtDatos + ".Size = New System.Drawing.Size(" + CStr(AnchoTxtDatos) + ", 20)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtDatos + ".Tabindex = 1"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    BloqueEncontrado = False
                    LineaEncontrada = False
                    CadenaBuscada = "PARA NO REPETIR"
                End If
                Debug.Print(CadenaL)
                FicheroSalida.EscribeLinea(CadenaL)
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque Friends
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 1 Then
                    If V(0) = "End" And V(1) = "Class" Then
                        CadenaL2 = "Friend WithEvents " + Trim(NombreTxtDatos) + " As TextBox"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()
        GeneracionUltimaX(Contenedor, FilaE) = LeftE + AnchoTxtDatos
    End Sub

    Private Sub IncluirCEdicion(Contenedor As Integer, NombreCEdicion As String, FilaE As Integer, ColumnaE As Integer, Campo As String, NumeroDeDato As Integer)
        Dim TopE As Integer, LeftE As Integer
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String, CadenaL2 As String
        Dim V() As String, CadenaBuscada As String
        Dim BloqueEncontrado As Boolean, LineaEncontrada As Boolean, Cadena As String, FlagSalto As Boolean
        Dim Rs As New cnRecordset.CnRecordset, SQL As String, FlagHay As Boolean, FlagHayParametro As Boolean


        SQL = "SELECT * FROM zpa_estructura where tabla = '" + Trim(Datos(NumeroDeDato, 0) + "' and campo = '" + Trim(Datos(NumeroDeDato, 1))) + "'"
        If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
            MsgBox("No se puede abrir la tabla zpa_estructura")
            Exit Sub
        End If


        TopE = GeneracionPosicionPrimeraFila + 21 * (FilaE - 1)
        LeftE = GeneracionPosicionColumnas(ColumnaE) + AnchoGeneralEtiquetas + 9

        If GeneracionContenedorUsado(Contenedor) = False Then
            GeneracionContenedorUsado(Contenedor) = True
            ContenedorLayout(Contenedor)
        End If

        'Bloque InitializeComponent (Incluir control CnEdicion)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 2 Then
                    If V(0) = "Private" And V(1) = "Sub" And V(2) = "InitializeComponent()" Then
                        BloqueEncontrado = True
                        If Contenedor < 10 Then
                            CadenaBuscada = "Me.TP" + Format(Contenedor, "00")
                        Else
                            CadenaBuscada = "Me.TabPage" + Format(Contenedor - 10, "00")
                        End If
                    End If
                End If
                If UBound(V, 1) >= 3 Then
                    If BloqueEncontrado = True Then
                        If V(0) = CadenaBuscada And V(1) = "=" And V(2) = "New" Then
                            LineaEncontrada = True
                        End If
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
            If BloqueEncontrado = True And LineaEncontrada = True Then
                CadenaL = "Me." + Trim(NombreCEdicion) + " = New CnEdicion.CnEdicion()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque de cada control
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        If Contenedor < 10 Then
            CadenaBuscada = "'TP" + Format(Contenedor, "00")
        Else
            CadenaBuscada = "'TabPage" + Format(Contenedor - 10, "00")
        End If
        FlagSalto = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = CadenaBuscada And BloqueEncontrado = False Then
                        BloqueEncontrado = True
                        Debug.Print(CadenaL)
                        FicheroSalida.EscribeLinea(CadenaL)
                        CadenaL = FicheroEntrada.LeeLinea()
                        FicheroSalida.EscribeLinea(CadenaL) 'Esto graba la línea siguiente que contiene solo un ', porque necesitamos buscar la siguiente aparición de un '
                        CadenaBuscada = "'"
                        FlagSalto = True
                    End If
                End If
                If FlagSalto = False Then
                    If UBound(V, 1) = 0 Then
                        If BloqueEncontrado = True Then
                            If V(0) = CadenaBuscada Then
                                LineaEncontrada = True
                            End If
                        End If
                    End If
                End If
            End If
            If FlagSalto = True Then
                FlagSalto = False
            Else
                If BloqueEncontrado = True And LineaEncontrada = True Then

                    'Esto no se graba aquí, porque el CnEdicion quedaría abajo en el formulario.
                    'La etiqueta ha creado una marca '<<MarcaParaCE>>. Esta marca se sustituirá por la declaración Controls.Add

                    'If Contenedor < 10 Then
                    '    CadenaL2 = "Me.TP" + Format(Contenedor, "00") + ".Controls.Add(Me." + Trim(NombreCEdicion) + ")"
                    'Else
                    '    CadenaL2 = "Me.TabPage" + Format(Contenedor - 10, "00") + ".Controls.Add(Me." + Trim(NombreCEdicion) + ")"
                    'End If
                    'FicheroSalida.EscribeLinea(CadenaL2)


                    'Este es el nuevo bloque del control
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'" + Trim(NombreCEdicion)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    If Trim(Datos(NumeroDeDato, 3)) = "CHA" Then
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaEspacios = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMayusculas = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMayusculasAcentuadas = False    "
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMinusculas = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMinusculasAcentuadas = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaNumeros = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaSimbolos = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".CaracteresAceptables = " + Chr(34) + "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ -_,.;<>{}!¡?¿@#$%&/\()[]=+*" + Chr(34)
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ConvierteAMayusculas = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ConvierteAMinusculas = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    Else
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaEspacios = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMayusculas = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMayusculasAcentuadas = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMinusculas = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaMinusculasAcentuadas = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaNumeros = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".AceptaSimbolos = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".CaracteresAceptables = " + Chr(34) + "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz áéíóúÁÉÍÓÚ" + Chr(34)
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ConvierteAMayusculas = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ConvierteAMinusculas = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If


                    'CadenaL2 = "Me." + NombreCEdicion + ".Alineacion = System.Windows.Forms.HorizontalAlignment.Left"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".AnchoColumnaGrid = 0"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".AnchoColumnaGridEnlaceLookup1 = 0"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".AnchoColumnaGridEnlaceLookup2 = 0"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".AnchoColumnaGridEnlaceLookup3 = 0"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".BackColor = System.Drawing.Color.Yellow"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Bloqueado = True"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".Campo = " + Chr(34) + Trim(Campo) + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".CampoEnlacesLookup1 = Nothing"
                    If Trim(Datos(NumeroDeDato, 13)) > "" Then
                        CadenaL2 = "Me." + NombreCEdicion + ".CampoEnlacesLookup1 = " + Chr(34) + Trim(Datos(NumeroDeDato, 13)) + Chr(34)
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".CampoEnlacesLookup2 = Nothing"
                    If Trim(Datos(NumeroDeDato, 18)) > "" Then
                        CadenaL2 = "Me." + NombreCEdicion + ".CampoEnlacesLookup2 = " + Chr(34) + Trim(Datos(NumeroDeDato, 18)) + Chr(34)
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".CampoEnlacesLookup3 = Nothing"
                    If Trim(Datos(NumeroDeDato, 23)) > "" Then
                        CadenaL2 = "Me." + NombreCEdicion + ".CampoEnlacesLookup3 = " + Chr(34) + Trim(Datos(NumeroDeDato, 23)) + Chr(34)
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    '
                    'CadenaL2 = "Me." + NombreCEdicion + ".Clave = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".CmdCombo = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".CmdFecha = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".CmdGrid = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".CmdMantenimiento = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".ColorFondo = System.Drawing.SystemColors.Window"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".ColorFondoRequerido = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".ColorTexto = System.Drawing.SystemColors.ControlText"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Contenedor = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".CuantosEnlacesCampo = 0"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Decimales = 0"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".EdicionEnCombo = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".EdicionEnGrid = False"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".EnCreacionBloqueado = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".EnCreacionSoloLectura = False"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".EnCreacionOculto = False"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".SiempreSoloLectura = False"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".SiempreOculto = False"
                    FicheroSalida.EscribeLinea(CadenaL2)




                    ''CadenaL2 = "Me." + NombreCEdicion + ".EnEdicionGenericaBloqueado = False"
                    ''FicheroSalida.EscribeLinea(CadenaL2)
                    ''CadenaL2 = "Me." + NombreCEdicion + ".EnEdicionGenericaSoloLectura = False"
                    ''FicheroSalida.EscribeLinea(CadenaL2)
                    ''CadenaL2 = "Me." + NombreCEdicion + ".EnEdicionGenericaVisible = True"
                    ''FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".EnlacesLookup1 = 0"
                    If IsNumeric(Datos(NumeroDeDato, 11)) Then
                        If CInt(Datos(NumeroDeDato, 11)) Then
                            CadenaL2 = "Me." + NombreCEdicion + ".EnlacesLookup1 = " + Datos(NumeroDeDato, 11)
                        End If
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".EnlacesLookup2 = 0"
                    If IsNumeric(Datos(NumeroDeDato, 16)) Then
                        If CInt(Datos(NumeroDeDato, 16)) Then
                            CadenaL2 = "Me." + NombreCEdicion + ".EnlacesLookup2 = " + Datos(NumeroDeDato, 16)
                        End If
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".EnlacesLookup3 = 0"
                    If IsNumeric(Datos(NumeroDeDato, 21)) Then
                        If CInt(Datos(NumeroDeDato, 21)) Then
                            CadenaL2 = "Me." + NombreCEdicion + ".EnlacesLookup3 = " + Datos(NumeroDeDato, 21)
                        End If
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".EnModificacionBloqueado = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".EnModificacionSoloLectura = False"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".EnModificacionOculto = False  "
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Formatear = True"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".Fuente = Nothing"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".HayMascaraEspecial = False"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'Valor defecto
                    FlagHay = False
                    If IsNumeric(Rs!hay_valor_defecto) Then
                        If CInt(Rs!hay_valor_defecto) > 0 Then
                            FlagHay = True
                        End If
                    End If
                    FlagHayParametro = False
                    If FlagHay = True Then
                        If IsNumeric(Rs!valor_defecto_p) Then
                            If CInt(Rs!valor_defecto_p) >= 1 And CInt(Rs!valor_defecto_p) <= 10 Then
                                FlagHayParametro = True
                            End If
                        End If
                        If FlagHayParametro = False Then
                            FlagHay = False
                            If Not (IsDBNull(Rs!valor_defecto)) Then
                                If Trim(Rs!valor_defecto) > "" Then
                                    FlagHay = True
                                End If
                            End If
                        End If
                    End If
                    If FlagHay = True Then
                        CadenaL2 = "Me." + NombreCEdicion + ".HayValorDefecto = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        If FlagHayParametro = True Then
                            CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorDefecto = " + CStr(CInt(Rs!valor_defecto_p))
                            FicheroSalida.EscribeLinea(CadenaL2)
                            CadenaL2 = "Me." + NombreCEdicion + ".ValorDefecto = " + Chr(34) + Chr(34)
                            FicheroSalida.EscribeLinea(CadenaL2)
                        Else
                            CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorDefecto = 0"
                            FicheroSalida.EscribeLinea(CadenaL2)
                            CadenaL2 = "Me." + NombreCEdicion + ".ValorDefecto = " + Chr(34) + Trim(Rs!valor_defecto) + Chr(34)
                            FicheroSalida.EscribeLinea(CadenaL2)
                        End If
                    Else
                        CadenaL2 = "Me." + NombreCEdicion + ".HayValorDefecto = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorDefecto = 0"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ValorDefecto = " + Chr(34) + Chr(34)
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If

                    'Valor fijo
                    FlagHay = False
                    If IsNumeric(Rs!m_hay_valor_fijo) Then
                        If CInt(Rs!m_hay_valor_fijo) > 0 Then
                            FlagHay = True
                        End If
                    End If
                    FlagHayParametro = False
                    If FlagHay = True Then
                        If IsNumeric(Rs!m_valor_fijo_p) Then
                            If CInt(Rs!m_valor_fijo_p) >= 1 And CInt(Rs!m_valor_fijo_p) <= 10 Then
                                FlagHayParametro = True
                            End If
                        End If
                        If FlagHayParametro = False Then
                            FlagHay = False
                            If Not (IsDBNull(Rs!m_valor_fijo)) Then
                                If Trim(Rs!m_valor_fijo) > "" Then
                                    FlagHay = True
                                End If
                            End If
                        End If
                    End If
                    If FlagHay = True Then
                        CadenaL2 = "Me." + NombreCEdicion + ".HayValorFijo = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        If FlagHayParametro = True Then
                            CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijo = " + CStr(CInt(Rs!m_valor_fijo_p))
                            FicheroSalida.EscribeLinea(CadenaL2)
                            CadenaL2 = "Me." + NombreCEdicion + ".ValorFijo = " + Chr(34) + Chr(34)
                            FicheroSalida.EscribeLinea(CadenaL2)
                        Else
                            CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijo = 0"
                            FicheroSalida.EscribeLinea(CadenaL2)
                            CadenaL2 = "Me." + NombreCEdicion + ".ValorFijo = " + Chr(34) + Trim(Rs!m_valor_fijo) + Chr(34)
                            FicheroSalida.EscribeLinea(CadenaL2)
                        End If
                    Else
                        CadenaL2 = "Me." + NombreCEdicion + ".HayValorFijo = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijo = 0"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ValorFijo = " + Chr(34) + Chr(34)
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If

                    'Valor fijo creación
                    FlagHay = False
                    If IsNumeric(Rs!m_hay_valor_crear) Then
                        If CInt(Rs!m_hay_valor_crear) > 0 Then
                            FlagHay = True
                        End If
                    End If
                    FlagHayParametro = False
                    If FlagHay = True Then
                        If IsNumeric(Rs!m_valor_crear_p) Then
                            If CInt(Rs!m_valor_crear_p) >= 1 And CInt(Rs!m_valor_crear_p) <= 10 Then
                                FlagHayParametro = True
                            End If
                        End If
                        If FlagHayParametro = False Then
                            FlagHay = False
                            If Not (IsDBNull(Rs!m_valor_crear)) Then
                                If Trim(Rs!m_valor_crear) > "" Then
                                    FlagHay = True
                                End If
                            End If
                        End If
                    End If
                    If FlagHay = True Then
                        CadenaL2 = "Me." + NombreCEdicion + ".HayValorFijoCreacion = True"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        If FlagHayParametro = True Then
                            CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijoCreacion = " + CStr(CInt(Rs!m_valor_crear_p))
                            FicheroSalida.EscribeLinea(CadenaL2)
                            CadenaL2 = "Me." + NombreCEdicion + ".ValorFijoCreacion = " + Chr(34) + Chr(34)
                            FicheroSalida.EscribeLinea(CadenaL2)
                        Else
                            CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijoCreacion = 0"
                            FicheroSalida.EscribeLinea(CadenaL2)
                            CadenaL2 = "Me." + NombreCEdicion + ".ValorFijoCreacion = " + Chr(34) + Trim(Rs!m_valor_crear) + Chr(34)
                            FicheroSalida.EscribeLinea(CadenaL2)
                        End If
                    Else
                        CadenaL2 = "Me." + NombreCEdicion + ".HayValorFijoCreacion = False"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijoCreacion = 0"
                        FicheroSalida.EscribeLinea(CadenaL2)
                        CadenaL2 = "Me." + NombreCEdicion + ".ValorFijoCreacion = " + Chr(34) + Chr(34)
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If

                    'CadenaL2 = "Me." + NombreCEdicion + ".Identidad = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".Location = New System.Drawing.Point(" + CStr(LeftE) + "," + CStr(TopE) + ")"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Longitud = 90"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".Margin = New System.Windows.Forms.Padding(0)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MascaraEspecial = " + Chr(34) + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MaximaFecha = New Date(2050, 12, 31, 0, 0, 0, 0)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MaximaFechaHora = New Date(2050, 12, 31, 23, 59, 59, 0)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MaximoNumero = 999999999999999.0R"

                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".MaxLength = 90"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MinimaFecha = New Date(1900, 1, 1, 0, 0, 0, 0)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MinimaFechaHora = New Date(1900, 1, 1, 0, 0, 1, 0)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".MinimoNumero = -999999999999999.0R"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".Name = " + Chr(34) + NombreCEdicion + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".NumeroCampo = -1"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijo = 0"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".NumeroParametroValorFijoCreacion = 0"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".NumeroTablaFormulario = -1"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Requerido = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".Restriccion = " + Chr(34) + Chr(34)
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".Situacion = CnEdicion.CnEdicion.SituacionControl.NoInicializado"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".Size = New System.Drawing.Size(30, 20)"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".SoloLectura = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".TabIndex = 9999"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".Tabla = " + Chr(34) + Trim(GeneracionTablaPrincipal) + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".TablaEnlacesLookup1 = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".TablaEnlacesLookup2 = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".TablaEnlacesLookup3 = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".TipoDeDato = CnEdicion.CnEdicion.Tipo_de_dato.Texto"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    CadenaL2 = "Me." + NombreCEdicion + ".TituloGridEnlaceLookup1 = Nothing"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".TituloGridEnlaceLookup2 = Nothing"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".TituloGridEnlaceLookup3 = Nothing"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreCEdicion + ".TituloParaGrid = Nothing"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".TTEdicion = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".TxtDatos = Nothing"
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    'CadenaL2 = "Me." + NombreCEdicion + ".ValoresFijosManuales = False"
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".ValorFijo = " + Chr(34) + Chr(34)
                    'FicheroSalida.EscribeLinea(CadenaL2)
                    'CadenaL2 = "Me." + NombreCEdicion + ".ValorFijoCreacion = " + Chr(34) + Chr(34)
                    'FicheroSalida.EscribeLinea(CadenaL2)

                    BloqueEncontrado = False
                    LineaEncontrada = False
                    CadenaBuscada = "PARA NO REPETIR"
                End If
                Debug.Print(CadenaL)
                FicheroSalida.EscribeLinea(CadenaL)
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque para incluir el Controls.Add en el sitio correcto sustituyendo la mara '<<MarcaParaCE>>
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            FlagSalto = False
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = "'<<MarcaParaCE>>" Then
                        If Contenedor < 10 Then
                            CadenaL2 = "Me.TP" + Format(Contenedor, "00") + ".Controls.Add(Me." + Trim(NombreCEdicion) + ")"
                        Else
                            CadenaL2 = "Me.TabPage" + Format(Contenedor - 10, "00") + ".Controls.Add(Me." + Trim(NombreCEdicion) + ")"
                        End If
                        FicheroSalida.EscribeLinea(CadenaL2)
                        FlagSalto = True
                    End If
                End If
            End If
            If FlagSalto = False Then
                FicheroSalida.EscribeLinea(CadenaL)
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque Friends
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 1 Then
                    If V(0) = "End" And V(1) = "Class" Then
                        CadenaL2 = "Friend WithEvents " + Trim(NombreCEdicion) + " As CnEdicion.CnEdicion"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        Rs.Close()

        ' La posición del CnEdicion, que será invisible en ejecución, es irrelevante para el diseño de los controles del formulario
    End Sub

    Private Sub IncluirCmdBoton(Contenedor As Integer, NombreBoton As String, FilaE As Integer, ColumnaE As Integer)
        Dim TopE As Integer, LeftE As Integer
        Dim CadenaFicheroEntrada As String, CadenaFicheroEntradaAuxiliar As String, CadenaFicheroSalida As String, CadenaL As String, CadenaL2 As String
        Dim V() As String, CadenaBuscada As String
        Dim BloqueEncontrado As Boolean, LineaEncontrada As Boolean, FlagSalto As Boolean
        Dim TipoBoton As String

        TopE = GeneracionPosicionPrimeraFila + 21 * (FilaE - 1) - 3
        LeftE = GeneracionPosicionColumnas(ColumnaE)
        If LeftE > 0 And LeftE <= GeneracionUltimaX(Contenedor, FilaE) Then
            LeftE = GeneracionUltimaX(Contenedor, FilaE) + 1
        End If

        If GeneracionContenedorUsado(Contenedor) = False Then
            GeneracionContenedorUsado(Contenedor) = True
            ContenedorLayout(Contenedor)
        End If

        TipoBoton = Mid(NombreBoton, 1, NombreBoton.Length - 3)

        'Bloque InitializeComponent (Incluir Botón)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 2 Then
                    If V(0) = "Private" And V(1) = "Sub" And V(2) = "InitializeComponent()" Then
                        BloqueEncontrado = True
                        If Contenedor < 10 Then
                            CadenaBuscada = "Me.TP" + Format(Contenedor, "00")
                        Else
                            CadenaBuscada = "Me.TabPage" + Format(Contenedor - 10, "00")
                        End If
                    End If
                End If
                If UBound(V, 1) >= 3 Then
                    If BloqueEncontrado = True Then
                        If V(0) = CadenaBuscada And V(1) = "=" And V(2) = "New" Then
                            LineaEncontrada = True
                        End If
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
            If BloqueEncontrado = True And LineaEncontrada = True Then
                CadenaL = "Me." + Trim(NombreBoton) + " = New System.Windows.Forms.Button()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque de cada control
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        If Contenedor < 10 Then
            CadenaBuscada = "'TP" + Format(Contenedor, "00")
        Else
            CadenaBuscada = "'TabPage" + Format(Contenedor - 10, "00")
        End If
        FlagSalto = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = CadenaBuscada And BloqueEncontrado = False Then
                        BloqueEncontrado = True
                        Debug.Print(CadenaL)
                        FicheroSalida.EscribeLinea(CadenaL)
                        CadenaL = FicheroEntrada.LeeLinea()
                        FicheroSalida.EscribeLinea(CadenaL) 'Esto graba la línea siguiente que contiene solo un ', porque necesitamos buscar la siguiente aparición de un '
                        CadenaBuscada = "'"
                        FlagSalto = True
                    End If
                End If
                If FlagSalto = False Then
                    If UBound(V, 1) = 0 Then
                        If BloqueEncontrado = True Then
                            If V(0) = CadenaBuscada Then
                                LineaEncontrada = True
                            End If
                        End If
                    End If
                End If
            End If
            If FlagSalto = True Then
                FlagSalto = False
            Else
                If BloqueEncontrado = True And LineaEncontrada = True Then
                    'Esta línea es del bloque del controlador
                    If Contenedor < 10 Then
                        CadenaL2 = "Me.TP" + Format(Contenedor, "00") + ".Controls.Add(Me." + Trim(NombreBoton) + ")"
                    Else
                        CadenaL2 = "Me.TabPage" + Format(Contenedor - 10, "00") + ".Controls.Add(Me." + Trim(NombreBoton) + ")"
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)


                    'Este es el nuevo bloque del control
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'" + Trim(NombreBoton)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreBoton + ".Image = CType(Resources.GetObject(" + Chr(34) + Trim(NombreBoton) + ".Image" + Chr(34) + "), System.Drawing.Image)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreBoton + ".Location = New System.Drawing.Point(" + CStr(LeftE) + "," + CStr(TopE) + ")"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreBoton + ".Name = " + Chr(34) + NombreBoton + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreBoton + ".Size = New System.Drawing.Size(24, 22)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreBoton + ".UseVisualStyleBackColor = True"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreBoton + ".Tabindex = 9999"
                    FicheroSalida.EscribeLinea(CadenaL2)

                    BloqueEncontrado = False
                    LineaEncontrada = False
                    CadenaBuscada = "PARA NO REPETIR"

                End If
                Debug.Print(CadenaL)
                FicheroSalida.EscribeLinea(CadenaL)
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque Friends
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 1 Then
                    If V(0) = "End" And V(1) = "Class" Then
                        CadenaL2 = "Friend WithEvents " + Trim(NombreBoton) + " As Button"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'RESX
        CadenaFicheroEntrada = Trim(MantenimientoRESX) + Format(ContadorRESX, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroEntradaAuxiliar = Trim(DirectorioRecursos) + "\XML" + Trim(TipoBoton) + ".gen"
        FicheroEntradaAuxiliar.Open(CadenaFicheroEntradaAuxiliar)

        ContadorRESX = ContadorRESX + 1
        CadenaFicheroSalida = Trim(MantenimientoRESX) + Format(ContadorRESX, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        CadenaBuscada = "</root>"
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = CadenaBuscada Then
                        Do While FicheroEntradaAuxiliar.EOF = False
                            CadenaL2 = FicheroEntradaAuxiliar.LeeLinea()
                            CadenaL2 = Replace(CadenaL2, Trim(TipoBoton) + ".Image", Trim(NombreBoton) + ".Image")
                            FicheroSalida.EscribeLinea(CadenaL2)
                        Loop
                        FicheroEntradaAuxiliar.Close()
                    End If
                End If
            End If
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        GeneracionUltimaX(Contenedor, FilaE) = LeftE + 24
    End Sub


    Private Sub IncluirTxtLookup(Contenedor As Integer, NombreTxtLookup As String, FilaE As Integer, ColumnaE As Integer, AnchoTxtDatos As Integer)
        Dim TopE As Integer, LeftE As Integer
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String, CadenaL2 As String
        Dim V() As String, CadenaBuscada As String
        Dim BloqueEncontrado As Boolean, LineaEncontrada As Boolean, Cadena As String, FlagSalto As Boolean


        TopE = GeneracionPosicionPrimeraFila + 21 * (FilaE - 1) + 1
        LeftE = GeneracionPosicionColumnas(ColumnaE)
        If LeftE > 0 And LeftE <= GeneracionUltimaX(Contenedor, FilaE) Then
            LeftE = GeneracionUltimaX(Contenedor, FilaE) + 1
        End If

        If GeneracionContenedorUsado(Contenedor) = False Then
            GeneracionContenedorUsado(Contenedor) = True
            ContenedorLayout(Contenedor)
        End If

        'Bloque InitializeComponent (Incluir textbox)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 2 Then
                    If V(0) = "Private" And V(1) = "Sub" And V(2) = "InitializeComponent()" Then
                        BloqueEncontrado = True
                        If Contenedor < 10 Then
                            CadenaBuscada = "Me.TP" + Format(Contenedor, "00")
                        Else
                            CadenaBuscada = "Me.TabPage" + Format(Contenedor - 10, "00")
                        End If
                    End If
                End If
                If UBound(V, 1) >= 3 Then
                    If BloqueEncontrado = True Then
                        If V(0) = CadenaBuscada And V(1) = "=" And V(2) = "New" Then
                            LineaEncontrada = True
                        End If
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
            If BloqueEncontrado = True And LineaEncontrada = True Then
                CadenaL = "Me." + Trim(NombreTxtLookup) + " = New System.Windows.Forms.TextBox()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque de cada control
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        If Contenedor < 10 Then
            CadenaBuscada = "'TP" + Format(Contenedor, "00")
        Else
            CadenaBuscada = "'TabPage" + Format(Contenedor - 10, "00")
        End If
        FlagSalto = False

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 0 Then
                    If V(0) = CadenaBuscada And BloqueEncontrado = False Then
                        BloqueEncontrado = True
                        Debug.Print(CadenaL)
                        FicheroSalida.EscribeLinea(CadenaL)
                        CadenaL = FicheroEntrada.LeeLinea()
                        FicheroSalida.EscribeLinea(CadenaL) 'Esto graba la línea siguiente que contiene solo un ', porque necesitamos buscar la siguiente aparición de un '
                        CadenaBuscada = "'"
                        FlagSalto = True
                    End If
                End If
                If FlagSalto = False Then
                    If UBound(V, 1) = 0 Then
                        If BloqueEncontrado = True Then
                            If V(0) = CadenaBuscada Then
                                LineaEncontrada = True
                            End If
                        End If
                    End If
                End If
            End If
            If FlagSalto = True Then
                FlagSalto = False
            Else
                If BloqueEncontrado = True And LineaEncontrada = True Then
                    'Esta línea es del bloque del controlador
                    If Contenedor < 10 Then
                        CadenaL2 = "Me.TP" + Format(Contenedor, "00") + ".Controls.Add(Me." + Trim(NombreTxtLookup) + ")"
                    Else
                        CadenaL2 = "Me.TabPage" + Format(Contenedor - 10, "00") + ".Controls.Add(Me." + Trim(NombreTxtLookup) + ")"
                    End If
                    FicheroSalida.EscribeLinea(CadenaL2)

                    'Este es el nuevo bloque del control
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'" + Trim(NombreTxtLookup)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "'"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".BorderStyle = System.Windows.Forms.BorderStyle.None"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".Location = New System.Drawing.Point(" + CStr(LeftE) + "," + CStr(TopE) + ")"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".Name = " + Chr(34) + NombreTxtLookup + Chr(34)
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".ReadOnly = True"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".Size = New System.Drawing.Size(" + CStr(AnchoTxtDatos) + ", 13)"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    CadenaL2 = "Me." + NombreTxtLookup + ".Tabindex = 9999"
                    FicheroSalida.EscribeLinea(CadenaL2)
                    BloqueEncontrado = False
                    LineaEncontrada = False
                    CadenaBuscada = "PARA NO REPETIR"
                End If
                Debug.Print(CadenaL)
                FicheroSalida.EscribeLinea(CadenaL)
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque Friends
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) = 1 Then
                    If V(0) = "End" And V(1) = "Class" Then
                        CadenaL2 = "Friend WithEvents " + Trim(NombreTxtLookup) + " As TextBox"
                        FicheroSalida.EscribeLinea(CadenaL2)
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        GeneracionUltimaX(Contenedor, FilaE) = LeftE + AnchoTxtDatos
    End Sub

    Private Sub ContenedorLayout(Contenedor As Integer)
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String
        Dim V() As String, CadenaBuscada As String
        Dim BloqueEncontrado As Boolean, LineaEncontrada As Boolean, Cadena As String

        '    Designer

        'Bloque InitializeComponent (SuspendLayout del contenedor)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 2 Then
                    If V(0) = "Private" And V(1) = "Sub" And V(2) = "InitializeComponent()" Then
                        BloqueEncontrado = True
                    End If
                End If
                If UBound(V, 1) >= 0 Then
                    If BloqueEncontrado = True And Contenedor < 10 Then
                        If V(0) = "Me.TabCabecera.SuspendLayout()" Then
                            LineaEncontrada = True
                        End If
                    ElseIf BloqueEncontrado = True Then
                        If V(0) = "Me.TabGeneral.SuspendLayout()" Then
                            LineaEncontrada = True
                        End If
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)
            If BloqueEncontrado = True And LineaEncontrada = True Then
                If Contenedor < 10 Then
                    Cadena = "TP" + Format(Contenedor, "00")
                Else
                    Cadena = "TabPage" + Format(Contenedor - 10, "00")
                End If
                CadenaL = "Me." + Trim(Cadena) + ".SuspendLayout()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()

        'Bloque Formulario (Resume / Perform Layout del contenedor)
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        ContadorDesigner = ContadorDesigner + 1
        CadenaFicheroSalida = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroSalida.Open(CadenaFicheroSalida, True)

        BloqueEncontrado = False
        LineaEncontrada = False
        CadenaBuscada = "'" + GeneracionNombreFormulario

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            CadenaL = Trim(Regex.Replace(CadenaL, "\s+", " "))
            If Trim(CadenaL) > "" Then
                V = CadenaL.Split(" ")
                If UBound(V, 1) >= 0 Then
                    If BloqueEncontrado = False And V(0) = CadenaBuscada Then
                        BloqueEncontrado = True
                        CadenaBuscada = "Me.TabCabecera.ResumeLayout(False)"
                    ElseIf BloqueEncontrado = True And V(0) = CadenaBuscada Then
                        LineaEncontrada = True
                    End If
                End If
            End If
            Debug.Print(CadenaL)
            FicheroSalida.EscribeLinea(CadenaL)

            If BloqueEncontrado = True And LineaEncontrada = True Then
                If Contenedor < 10 Then
                    Cadena = "TP" + Format(Contenedor, "00")
                Else
                    Cadena = "TabPage" + Format(Contenedor - 10, "00")
                End If
                CadenaL = "Me." + Cadena + ".ResumeLayout(False)"
                FicheroSalida.EscribeLinea(CadenaL)
                CadenaL = "Me." + Cadena + ".PerformLayout()"
                FicheroSalida.EscribeLinea(CadenaL)
                BloqueEncontrado = False
                LineaEncontrada = False
            End If
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()
    End Sub

    Private Sub LLevarANovagest()
        Dim CadenaFicheroEntrada As String, CadenaFicheroSalida As String, CadenaL As String
        Dim FlagHay As Boolean

        '   ===  Designer ===

        '       Copia si se sustituye
        CadenaFicheroEntrada = Trim(DirectorioDefinitivo) + "\" + Trim(GeneracionNombreFormulario) + ".Designer.vb"
        Try
            FicheroEntrada.Open(CadenaFicheroEntrada)
            FlagHay = True
        Catch ex As Exception
            FlagHay = False
        End Try
        If FlagHay = True Then
            CadenaFicheroSalida = Trim(DirectorioCopiasEliminadas) + "\" + Trim(GeneracionNombreFormulario) + Format(Now, "yyyyMMddHHmss") + ".Designer.vb"
            FicheroSalida.Open(CadenaFicheroSalida, True)
            Do While FicheroEntrada.EOF = False
                CadenaL = FicheroEntrada.LeeLinea()
                FicheroSalida.EscribeLinea(CadenaL)
            Loop
            FicheroEntrada.Close()
            FicheroSalida.Close()
        End If
        '       Grabación en Novagest
        CadenaFicheroEntrada = Trim(MantenimientoDesigner) + Format(ContadorDesigner, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroSalida = Trim(DirectorioDefinitivo) + "\" + Trim(GeneracionNombreFormulario) + ".Designer.vb"
        FicheroSalida.Open(CadenaFicheroSalida, True)

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()




        '  === Resx ===

        '       Copia si se sustituye
        CadenaFicheroEntrada = Trim(DirectorioDefinitivo) + "\" + Trim(GeneracionNombreFormulario) + ".resx"
        Try
            FicheroEntrada.Open(CadenaFicheroEntrada)
            FlagHay = True
        Catch ex As Exception
            FlagHay = False
        End Try
        If FlagHay = True Then
            CadenaFicheroSalida = Trim(DirectorioCopiasEliminadas) + "\" + Trim(GeneracionNombreFormulario) + Format(Now, "yyyyMMddHHmss") + ".resx"
            FicheroSalida.Open(CadenaFicheroSalida, True)
            Do While FicheroEntrada.EOF = False
                CadenaL = FicheroEntrada.LeeLinea()
                FicheroSalida.EscribeLinea(CadenaL)
            Loop
            FicheroEntrada.Close()
            FicheroSalida.Close()
        End If
        '       Grabación en Novagest
        CadenaFicheroEntrada = Trim(MantenimientoRESX) + Format(ContadorRESX, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroSalida = Trim(DirectorioDefinitivo) + "\" + Trim(GeneracionNombreFormulario) + ".resx"
        FicheroSalida.Open(CadenaFicheroSalida, True)

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()




        '  === Código ===

        '       Copia si se sustituye
        CadenaFicheroEntrada = Trim(DirectorioDefinitivo) + "\" + Trim(GeneracionNombreFormulario) + ".vb"
        Try
            FicheroEntrada.Open(CadenaFicheroEntrada)
            FlagHay = True
        Catch ex As Exception
            FlagHay = False
        End Try
        If FlagHay = True Then
            CadenaFicheroSalida = Trim(DirectorioCopiasEliminadas) + "\" + Trim(GeneracionNombreFormulario) + Format(Now, "yyyyMMddHHmss") + ".vb"
            FicheroSalida.Open(CadenaFicheroSalida, True)
            Do While FicheroEntrada.EOF = False
                CadenaL = FicheroEntrada.LeeLinea()
                FicheroSalida.EscribeLinea(CadenaL)
            Loop
            FicheroEntrada.Close()
            FicheroSalida.Close()
        End If
        '       Grabación en Novagest

        CadenaFicheroEntrada = Trim(MantenimientoCodigo) + Format(ContadorCodigo, "0000")
        FicheroEntrada.Open(CadenaFicheroEntrada)

        CadenaFicheroSalida = Trim(DirectorioDefinitivo) + "\" + Trim(GeneracionNombreFormulario) + ".vb"
        FicheroSalida.Open(CadenaFicheroSalida, True)

        Do While FicheroEntrada.EOF = False
            CadenaL = FicheroEntrada.LeeLinea()
            FicheroSalida.EscribeLinea(CadenaL)
        Loop
        FicheroEntrada.Close()
        FicheroSalida.Close()
    End Sub

End Class