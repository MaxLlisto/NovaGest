Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Data
Imports System.IO
Imports System.Drawing.Text
Imports System.ComponentModel

Public Class Formatos
    Public proceso As String
    Public documento As String
    Public formato As String
    Public defecto As String
    Public copias As Integer
    Public impresoradefecto As String
    Public altura As Integer
    Public a1 As String
    Public a2 As String
    Public a3 As String
    Public n1 As Integer
    Public n2 As Integer
    Public n3 As Integer
    Public imprimepdf As String
    Public carpetapdf As String
    Public impresoraPDF As String
    Public papel As String = "A4"
    Public ImpresoraSeleccionada As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Public Sub New()

    End Sub

    Public Sub New(ByVal adocumento As String, ByVal aformato As String)
        CargaFormato("", adocumento, aformato)
    End Sub

    Public Sub New(ByVal aproceso As String, ByVal adocumento As String, ByVal aformato As String)
        CargaFormato(aproceso, adocumento, aformato)
    End Sub

    Public Property doc() As String
        Get
            Return documento
        End Get
        Set(ByVal Value As String)
            documento = Value
        End Set
    End Property

    Public Property format() As String
        Get
            Return formato
        End Get
        Set(ByVal Value As String)
            formato = Value
        End Set
    End Property

    Public Property def() As String
        Get
            Return defecto
        End Get
        Set(ByVal Value As String)
            defecto = Value
        End Set
    End Property

    Public Property copy() As String
        Get
            Return copias
        End Get
        Set(ByVal Value As String)
            copias = Value
        End Set
    End Property

    Public Property PrinterDef() As String
        Get
            Return impresoradefecto
        End Get
        Set(ByVal Value As String)
            impresoradefecto = Value
        End Set
    End Property

    Public Property PrinterSelect() As String
        Get
            Return ImpresoraSeleccionada
        End Get
        Set(ByVal Value As String)
            ImpresoraSeleccionada = Value
        End Set
    End Property


    Public Property paperssize() As String
        Get
            Return papel
        End Get
        Set(ByVal Value As String)
            papel = Value
        End Set
    End Property

    Public Function GrabaFormato(ByRef trans As SqlClient.SqlTransaction) As Boolean
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sql As String

        Try
            sql = "SELECT * FROM zzformatosdefecto_n WHERE proceso = '" & proceso & "' AND documento = '" & documento & "' AND formato = '" & formato & "'"
            If rs.Open(sql, ObjetoGlobal.cn, True,,,,,, trans) Then
                If rs.EOF Then
                    rs.AddNew()
                    rs!proceso = proceso
                    rs!documento = documento
                    rs!formato = formato
                End If
                rs!copias = copias
                rs!impresoradefecto = impresoradefecto
                rs!altura = altura
                rs!defecto = IIf(defecto = "-1", -1, 0)
                rs!a1 = a1
                rs!a2 = a2
                rs!a3 = a3
                rs!n1 = n1
                rs!n2 = n2
                rs!n3 = n3
                rs!imprimepdf = IIf(Trim(imprimepdf) = "", "N", imprimepdf)
                rs!carpetapdf = carpetapdf
                rs!impresoraPDF = impresoraPDF
                rs!papel = papel
                rs.Update()
                rs.Close()
                Return True
            Else
                MsgBox("No se puede abrir la tabla de formatos defecto")
                Return False
            End If

        Catch ex As Exception
            MsgBox("Error grabando el formato")
            Return False
        End Try

    End Function


    Public Function CargaFormato(ByVal a As String, ByVal b As String, ByVal c As String) As Boolean
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sql As String

        proceso = a
        documento = b
        formato = c

        If a <> "" Then
            sql = "SELECT * FROM zzformatosdefecto_n WHERE proceso = '" & a & "' AND documento = '" & b & "' AND formato = '" & c & "'"
        Else
            sql = "SELECT * FROM zzformatosdefecto_n WHERE documento = '" & b & "' AND formato = '" & c & "'"
        End If

        If rs.Open(sql, ObjetoGlobal.cn) Then
            If Not rs.EOF Then
                If proceso = "" Then
                    proceso = rs!proceso
                End If
                printset.DefaultPageSettings.Landscape = False
                copias = IIf(CInt("0" & rs!copias) = 0, 1, CInt("0" & rs!copias))
                impresoradefecto = "" & rs!impresoradefecto
                altura = IIf(Not IsDBNull(rs!altura), rs!altura, 0)
                defecto = "" & rs!defecto
                a1 = "" & rs!a1
                a2 = "" & rs!a2
                a3 = "" & rs!a3
                n1 = IIf(Not IsDBNull(rs!n1), rs!n1, 0)
                n2 = IIf(Not IsDBNull(rs!n2), rs!n2, 0)
                n3 = IIf(Not IsDBNull(rs!n3), rs!n3, 0)
                imprimepdf = rs!imprimepdf
                carpetapdf = "" & rs!carpetapdf
                impresoraPDF = "" & rs!impresoraPDF
                papel = IIf("" & rs!papel = "", "A4", rs!papel)
                If a1.Trim = "apaisado" Then
                    printset.DefaultPageSettings.Landscape = True
                End If
                If impresoradefecto.Trim <> "" Then
                    obtener_impresoras(impresoradefecto)
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
        Return True
    End Function

    Private Sub impresoras()
        'Settings
        Dim Pd As New PrintDialog()
        Pd.PrinterSettings = printset
        If Pd.ShowDialog = DialogResult.OK Then
            printset = Pd.PrinterSettings
            RP.Orn = printset.DefaultPageSettings.Landscape
            If RP.Orn Then
                RP.w = printset.DefaultPageSettings.PaperSize.Height - RP.leftm - RP.Rightm
            Else
                RP.w = printset.DefaultPageSettings.PaperSize.Width - RP.leftm - RP.Rightm
            End If

        End If
    End Sub

    Public Sub obtener_impresoras(Impresoradefecto)
        'Sender es el objeto donde se veran las impresoras
        'en este caso yo utilizo un ListBox pero podria tambien ser un ComboBox
        'un List, entre otros que sean de tipo collections
        Dim pd As New PrintDocument
        Dim i As Integer
        'Se define el print Document.
        Dim impresora_predeterminada As String = pd.PrinterSettings.PrinterName
        ImpresoraSeleccionada = ""
        For i = 0 To PrinterSettings.InstalledPrinters.Count - 1
            If PrinterSettings.InstalledPrinters.Item(i).ToString().Trim = Impresoradefecto Then
                ImpresoraSeleccionada = Impresoradefecto
            End If
        Next
        If ImpresoraSeleccionada = "" Then
            ImpresoraSeleccionada = impresora_predeterminada
        End If

    End Sub

End Class
