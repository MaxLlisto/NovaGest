Imports System
Imports System.IO
Imports System.Collections



Public Class DFFTools
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
    'Private df As New DF.Class1
    Private conectado As Boolean



    Private Function conexion() As Boolean
        Dim result As String
        Dim id As Long

        'df.EscribeConDf("101.101.100.22", "3309", "dfserver_user", "dfserver_user")
        'result = df.AbreConDf()

        result = "OK"

        If result = "OK" Then ' ya conecta, por tanto ejecutar codigo aquí....................
            Return True
        Else
            MsgBox(result) 'devuelve un sring con el error de conexion 
            Return False
        End If

        Return False
    End Function



    Public Sub New(ByVal RutayArchivo As String, ByVal nombre As String)
        IniRutayArchivo = RutayArchivo
        PathArchivo = RutayArchivo
        NombreArchivoIni = nombre
        NombreArchivo = nombre
        accion = ""
        CadenaError = ""
        IdArchivo = 0
        conectado = conexion()
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
                'If Me.RenombraFichero(Path.GetDirectoryName(curFile), NombreArchivo, Path.GetFileName(curFile)) Then
                My.Computer.FileSystem.RenameFile(PathArchivo & NombreArchivo, namefile & "_" & i & extfile)
                'End If
            End If

            If HacerRuta(PathFile) Then
                My.Computer.FileSystem.CopyFile(
                IniRutayArchivo & NombreArchivoIni,
                PathArchivo & NombreArchivo,
                overwrite:=True)
            Else
                Me.CadenaError = Me.CadenaError & "/" & "problemas creando la ruta " & PathArchivo & NombreArchivo
            End If

        Catch ex As Exception
            Me.CadenaError = Me.CadenaError & "/" & Trim(ex.Message)
            Return False
        End Try

        Return True

    End Function

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

    Public Property Archivo() As String

        Get
            Return NombreArchivo
        End Get

        Set(ByVal value As String)
            NombreArchivo = Trim(value)
        End Set

    End Property

    'Private Function ObtenerIdArchivo(ByVal ArgPath As String, ByVal argName As String) As String
    '    Dim p As New ProcessStartInfo
    '    Dim miProceso As New Process
    '    Dim Id As Integer
    '    Try
    '        Id = df.ObtieneIDarchivo(ArgPath, argName)
    '        'MsgBox(Id)
    '        ' Id = DFServer_helper.FileSystem.cDFFile.obtenerIdFichero(bbdd, ArgPath & argName)
    '        Return CStr(Id)
    '    Catch ex As Exception
    '        CadenaError = CadenaError & "/" & Trim(ex.Message)
    '        Return ""
    '    End Try
    '    Return ""

    'End Function

    'Private Function GetIdArchivo() As Boolean
    '    Dim p As New ProcessStartInfo
    '    Dim miProceso As New Process
    '    Try
    '        IdArchivo = ObtenerIdArchivo(PathArchivo, NombreArchivo)
    '        If Trim(IdArchivo) <> 0 Then
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        CadenaError = CadenaError & "/" & Trim(ex.Message)
    '        Return False
    '    End Try
    '    Return False

    'End Function

    'Public Function LeerIndice(ByVal campo As String) As String
    '    Dim p As New ProcessStartInfo
    '    Dim miProceso As New Process
    '    Dim Respuesta As String

    '    Try
    '        Respuesta = df.LeeIndice(PathArchivo, NombreArchivo, campo)
    '        If Trim(Respuesta) <> "" Then
    '            Return Respuesta
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        CadenaError = CadenaError & "/" & Trim(ex.Message)
    '        Return False
    '    End Try
    '    Return False

    'End Function

    'Public Function EscribirIndice(ByVal campo As String, ByVal valor As String) As Boolean
    '    Dim p As New ProcessStartInfo
    '    Dim miProceso As New Process
    '    Dim Respuesta As String

    '    Try
    '        Respuesta = df.EscribeIndice(PathArchivo, NombreArchivo, campo, valor)
    '        If Respuesta = "OK" Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        CadenaError = CadenaError & "/" & Trim(ex.Message)
    '        Return False
    '    End Try
    '    Return False

    'End Function

    'Public Function RenombraFichero(ByVal apath As String, ByVal anombre As String, ByVal nuevonombre As String) As Boolean
    '    Dim Respuesta As String

    '    Respuesta = df.RenombraFicheroDF(anombre, nuevonombre, apath)
    '    If Respuesta = "OK" Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    '    Return False

    'End Function


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

    Public ReadOnly Property EstaConectado() As String

        Get
            Return conectado
        End Get

    End Property

    Private Function HacerRuta(cArg) As Boolean
        Dim s As String
        Dim sp As String
        Dim parts As String()
        Dim part As String
        Dim pathInicial As String = "\\101.101.100.22\horto_doc\documentos\"
        'Dim ret As String

        Try
            s = LCase(Trim(cArg))

            ' "\\101.101.101.100.22\documentos\0_9\2\22538125B_MANUEL LUIS RISQUEZ ORTS\"

            sp = s.Replace(pathInicial, "")

            parts = sp.Split(New Char() {"\"c})

            For Each part In parts
                If Trim(part) <> "" Then
                    'ret = df.CREARCARPETA(pathInicial, part)
                    pathInicial = pathInicial & part & "\"
                    If Not Directory.Exists(pathInicial) Then
                        My.Computer.FileSystem.CreateDirectory(pathInicial)
                    End If
                    'Console.WriteLine(pathInicial)
                End If
            Next

            'crearcarpeta
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

End Class