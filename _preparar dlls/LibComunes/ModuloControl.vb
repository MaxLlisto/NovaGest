Public Module ModuloControl

    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    'Public Sub Main()
    '    Dim PathLocal As String, Cadena As String, FicheroConexionLocal As New cnFichero.cnFichero, MensajeError As String
    '    Dim Rs As New cnRecordset.CnRecordset, SQL As String

    '    ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
    '    '        PathLocal = Application.StartupPath + ".\\ConexionLocal.txt"
    '    Cadena = ""
    '    '       If System.IO.File.Exists(PathLocal) Then
    '    '      Try
    '    '     FicheroConexionLocal.Open(PathLocal)
    '    '    Cadena = FicheroConexionLocal.LeeLinea
    '    '   FicheroConexionLocal.Close()
    '    '  Catch e As Exception
    '    ' MensajeError = "Error al leer el fichero de conexión local (ConexionLocal.txt):" & vbCrLf & e.Message
    '    'Throw New ArgumentException(MensajeError)
    '    'End Try
    '    'End If

    '    If ObjetoGlobal.Inicializar(Cadena) = False Then
    '        MsgBox("No se puede abrir la conexión a la BD")
    '    End If
    '    '
    '    SQL = "select top 1 * from sys.tables"
    '    If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
    '        MsgBox("No se pueden abrir las tablas de la BD. Revise la cadena de conexión: " + vbCrLf + Trim(Cadena))
    '        Exit Sub
    '    End If

    '    'Application.Run(Prueba)
    '    'Application.Run(MantenimientoG1)
    '    Application.Run(FrmBienes)
    '    Application.Run(FrmPersonalMarcajes)
    'End Sub

    Public Function ControlaTecla(C As TextBox, TeclaPulsada As Char, Optional CuantosDecimales As Integer = 2, Optional PermiteNegativos As Boolean = True, Optional LongitudMaxima As Integer = 0) As Char

        Dim PosicionComa As Integer

        ControlaTecla = Chr(0)
        If TeclaPulsada = Chr(13) Then
            SendKeys.Send("{TAB}")
        ElseIf TeclaPulsada = Chr(8) Then
            ControlaTecla = TeclaPulsada
        ElseIf TeclaPulsada = Chr(45) Then 'Guión
            If PermiteNegativos = True And (LongitudMaxima = 0 Or Microsoft.VisualBasic.Len(C.Text) < LongitudMaxima) Then
                If C.Text = "" Or (C.SelectionStart = 0 And InStr(C.Text, "-") = 0) Then
                    ControlaTecla = TeclaPulsada
                End If
            End If
        ElseIf (TeclaPulsada = Chr(46) Or TeclaPulsada = Chr(44)) And (LongitudMaxima = 0 Or Microsoft.VisualBasic.Len(C.Text) < LongitudMaxima) Then   'Coma y punto
            If CuantosDecimales > 0 Then
                If (InStr(C.Text, ",") <= 0) And (Len(Trim(C.Text)) - C.SelectionStart <= CuantosDecimales) Then
                    ControlaTecla = Chr(44)
                End If
            End If
        ElseIf TeclaPulsada >= Chr(48) And TeclaPulsada <= Chr(57) And (LongitudMaxima = 0 Or Microsoft.VisualBasic.Len(C.Text) < LongitudMaxima) Then ' 0 a 9
            PosicionComa = InStr(C.Text, ",")
            If PosicionComa <= 0 Or C.SelectionStart < PosicionComa Then
                ControlaTecla = TeclaPulsada
            ElseIf Len(Right(C.Text, Len(C.Text) - PosicionComa)) < CuantosDecimales Then
                ControlaTecla = TeclaPulsada
            End If
        End If
    End Function

    Public Enum ActividadDelGrid
        Creando
        Modificando
        Seleccionando
        CreacionAceptada
        CreacionCancelada
        ModificacionAceptada
        ModificacionCancelada
        SeleccionAceptada
        SeleccionCancelada
    End Enum

End Module
