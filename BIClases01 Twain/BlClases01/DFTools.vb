Imports System
Imports System.IO
Imports System.Collections

Public Class DFTools
    Private PathExe As String
    Private Args As String
    Private accion As String
    Private Tareas() As String
    Private PathArchivo As String
    Private NombreArchivo As String
    Private NombreArchivoIni As String
    Private IdArchivo As Long
    Private IniRutayArchivo
    Private CadenaError As String

    Public Sub New(ByVal RutayArchivo As String, ByVal nombre As String)
        IniRutayArchivo = RutayArchivo
        PathArchivo = RutayArchivo
        NombreArchivoIni = nombre
        NombreArchivo = nombre
        accion = ""
        CadenaError = ""
        IdArchivo = 0
    End Sub

    Public Function CopiarArchivo(ByVal argPat As String, Optional ByVal argNombre As String = "") As Boolean
        Dim curFile As String
        Dim namefile As String
        Dim extfile As String
        Dim PathFile As String
        Dim i As Integer = 0
        Dim HayQuerenombrar As Boolean



        If Trim(argNombre) <> "" Then
            NombreArchivo = argNombre
        End If


        Try
            PathArchivo = Trim(PathArchivo)

            If Right(argPat, 1) <> "\" Then
                argPat = argPat + "\"
            End If

            PathArchivo = argPat

            curFile = PathArchivo & NombreArchivo

            namefile = Path.GetFileNameWithoutExtension(curFile)
            extfile = Path.GetExtension(curFile)
            PathFile = Path.GetDirectoryName(curFile)

            Me.RutaArchivo = Path.GetDirectoryName(curFile)

            If Right(PathFile, 1) <> "\" Then
                PathFile = PathFile + "\"
            End If

            If Right(IniRutayArchivo, 1) <> "\" Then
                Me.IniRutayArchivo = Me.IniRutayArchivo + "\"
            End If

            HayQuerenombrar = False
            While File.Exists(curFile)
                i = i + 1
                curFile = PathFile & namefile & "_" & i & extfile
                HayQuerenombrar = True
            End While
            If HayQuerenombrar Then
                RenombraFicheroDF(Path.GetDirectoryName(curFile), Path.GetFileName(curFile))
            End If
            My.Computer.FileSystem.CopyFile(
            IniRutayArchivo & NombreArchivoIni,
            PathArchivo & NombreArchivo,
            overwrite:=True)
            GetIdArchivo()
        Catch ex As Exception
            Me.CadenaError = Me.CadenaError & "/" & Trim(ex.Message)
            Return False
        End Try

        Return True

    End Function

    Private Function ProcesoDFTools(ByVal arg As String) As Boolean
        Dim p As New ProcessStartInfo
        Dim miProceso As New Process

        Try
            p.FileName = PathExe + "DFTOOLBOX.exe"
            p.Arguments = arg
            p.WindowStyle = ProcessWindowStyle.Hidden

            miProceso.StartInfo = p
            miProceso.Start()
            miProceso.WaitForExit(10000)
            miProceso.Kill()
        Catch ex As Exception
            CadenaError = CadenaError & "/" & Trim(ex.Message)
            Return False
        End Try

        Return True

    End Function
    Public Property RutaExe() As String
        Get
            Return PathExe
        End Get
        Set(ByVal value As String)
            If Right(Trim(value), 1) = "\" Then
                PathExe = Trim(value)
            Else
                PathExe = Trim(value) + "\"
            End If
        End Set
    End Property

    Public Property RutaArchivo() As String
        Get
            Return PathArchivo
        End Get
        Set(ByVal value As String)
            If Right(Trim(value), 1) = "\" Then
                PathArchivo = Trim(value)
            Else
                PathArchivo = Trim(value) + "\"
            End If
        End Set
    End Property

    Public Function LeerArchivo(ByVal PathAndFile As String) As String
        Dim fileReader As String

        Try
            fileReader = My.Computer.FileSystem.ReadAllText(PathAndFile)
            Return fileReader
        Catch ex As Exception
            CadenaError = CadenaError & "/" & Trim(ex.Message)
            Return ""
        End Try

    End Function
    Public Function ObtenerRespuesta() As String
        Dim fileReader As String
        Try
            fileReader = My.Computer.FileSystem.ReadAllText(PathExe & "testlog.txt")
            Return fileReader
        Catch ex As Exception
            CadenaError = CadenaError & "/" & Trim(ex.Message)
            Return ""
        End Try
    End Function

    Public Function EscribirArchivo(ByVal arg As String, Optional ByVal nomFich As String = "envio.txt") As Boolean
        Dim sw As New System.IO.StreamWriter(nomFich, False)

        Try
            sw.WriteLine(arg)
            sw.Close()
        Catch ex As Exception
            CadenaError = CadenaError & "/" & Trim(ex.Message)
            Return False
        End Try
        Return True

    End Function

    Public Property Archivo() As String

        Get
            Return NombreArchivo
        End Get

        Set(ByVal value As String)
            NombreArchivo = Trim(value)
        End Set

    End Property

    Private Function ObtenerIdArchivo(ByVal cArg As String) As String
        Dim p As New ProcessStartInfo
        Dim miProceso As New Process
        Try
            p.FileName = PathExe + "DFTOOLBOX.exe"
            p.Arguments = cArg
            p.WindowStyle = ProcessWindowStyle.Hidden
            miProceso.StartInfo = p
            miProceso.Start()
            miProceso.WaitForExit(10000)
            miProceso.Kill()
            Return CStr(ObtenerRespuesta())
        Catch ex As Exception
            CadenaError = CadenaError & "/" & Trim(ex.Message)
            Return ""
        End Try
        Return ""

    End Function

    Private Function GetIdArchivo() As Boolean
        Dim p As New ProcessStartInfo
        Dim miProceso As New Process
        Try
            IdArchivo = ObtenerIdArchivo(Trim(PathExe) & "testlog.txt;ObtieneIDarchivo;" & PathArchivo & ";" & NombreArchivo)
            If Trim(IdArchivo) = "" Then
                Return False
            End If
        Catch ex As Exception
            CadenaError = CadenaError & "/" & Trim(ex.Message)
            Return False
        End Try
        Return True

    End Function

    Public Function LeeIndice(ByVal campo As String) As String
        Dim p As New ProcessStartInfo
        Dim miProceso As New Process
        Dim Respuesta As String

        ProcesoDFTools(";" & Trim(PathExe) & "testlog.txt" & ";LeeIndice;" & IdArchivo & ";" & campo)
        Respuesta = Trim(CStr(ObtenerRespuesta()))
        If Trim(Respuesta) <> "" Then
            Return Respuesta
        Else
            Return ""
        End If

    End Function

    Public Function EscribeIndice(ByVal campo As String, ByVal valor As String) As Boolean
        Dim p As New ProcessStartInfo
        Dim miProceso As New Process
        Dim Respuesta As String

        ProcesoDFTools(";" & Trim(PathExe) & "testlog.txt" & ";EscribeIndice;" & IdArchivo & ";" & campo & ";" & valor)
        Respuesta = Trim(CStr(ObtenerRespuesta()))
        If Respuesta = "OK" Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function RenombraFicheroDF(ByVal valor As String, ByVal valor1 As String) As String
        Dim Idf As String
        Dim Respuesta As String

        Idf = ObtenerIdArchivo(Trim(PathExe) & "testlog.txt;ObtieneIDarchivo;" & valor & ";" & valor1)
        If Idf <> "" Then
            ProcesoDFTools(";" & Trim(PathExe) & "testlog.txt" & ";RenombraFicheroDF;" & Idf & ";" & valor1)
            Respuesta = Trim(CStr(ObtenerRespuesta()))
            If Respuesta = "OK" Then
                Return True
            Else
                Return False
            End If
        End If
        Return False

    End Function

    'Public Function AbrirDocumento(ByVal valor As String) As String

    'End Function


    'Public Function OBTIENEHISTOWF(ByVal valor As String) As String

    'End Function

    'Public Function OBTIENEPATHBYID(ByVal valor As String) As String

    'End Function

    Public ReadOnly Property HayError() As Boolean
        Get
            Return (Trim(CadenaError) <> "")
        End Get
    End Property

    Public ReadOnly Property DescripcionError() As String

        Get
            Return CadenaError
        End Get

    End Property

End Class