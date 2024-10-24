Imports System.IO
Imports System.Data

'Imports System
'Imports System.ComponentModel
'Imports System.Windows.Forms

'Namespace PropertyGridFileBrowser
'    Public Class VisorImagenes

'        Private FrmVisor As Visor = New Visor

'        Public Sub New()
'            InitializeComponent()
'        End Sub

'        Private Sub frmMain_Load(ByVal sender As Object, ByVal e As EventArgs)
'            tester.Filename = "F:\Temp\Blue.bmp"
'            pgDetails.SelectedObject = tester
'        End Sub
'    End Class

'    Public Class MyTestClass
'        <Category("File")>
'        <Description("Source file for thumbnail and web images")>
'        <EditorAttribute(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor))>
'        Public Property Filename As String

'        Public Sub New()
'        End Sub
'    End Class
'End Namespace

Public Class TxtScripts
    Dim aVar As Dictionary(Of Integer, String)

    Public Function add(ByVal key As Integer, Optional secc As Integer = 0, Optional valor As String = "") As Boolean
        Dim clave As String

        clave = Right("0" & key, 2) & Right("0" & secc, 2)

        If aVar.ContainsKey(key) Then
            Return False
        End If

        aVar.Add(key, valor)

        Return True
    End Function

    Public Function ValorScrips(key, secc) As String
        Dim clave As String

        clave = Right("0" & key, 2) & Right("0" & secc, 2)

        If aVar.ContainsKey(key) Then
            Return aVar.Item(key)
        End If

        Return ""

    End Function

End Class
Public Class ControlVariables
    Dim aVar As Dictionary(Of String, Double) = New Dictionary(Of String, Double)
    Dim tipos As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Function add(ByVal key As String, Optional valor As Object = Nothing) As Boolean
        key = Mid(key, 2)
        If aVar.ContainsKey(key) Then
            Return False
        End If
        aVar.Add(key, valor)
        Return True
    End Function
    Public Function Operacion(ByVal Key As String, ByVal op As String, ByVal valor As VariantType) As VariantType
        Dim val As VariantType
        Dim value As String
        Key = Mid(Key, 1)
        If aVar.ContainsKey(Key) Then
            value = aVar.Item(Key)
            Select Case op
                Case "+"
                    If IsNumeric(value) Then
                        val = value + valor
                    Else
                        Return 0
                    End If
                Case "-"
                    If IsNumeric(value) Then
                        val = value - valor
                    Else
                        Return 0
                    End If
                Case "*"
                    If IsNumeric(value) Then
                        val = value * valor
                    Else
                        Return 0
                    End If
                Case "/"
                    If IsNumeric(value) Then
                        val = value / valor
                    Else
                        Return 0
                    End If
                Case "r"
                    If IsNumeric(value) Then
                        val = Math.Round(CDbl(value), valor)
                    End If
                Case "&"
                    val = value & valor

                Case "L"
                    val = Left(value, value)

                Case "R"
                    val = Right(value, value)
            End Select

        End If
        aVar.Item(Key) = CStr(val)
        Return val
    End Function

    Default Public Property Valor(key) As Object

        Get
            If Left(key, 1) = "@" Then key = Mid(key, 2)
            If aVar.ContainsKey(key) Then
                Return aVar.Item(key)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal value As Object)
            If Left(key, 1) = "@" Then key = Mid(key, 2)
            If aVar.ContainsKey(key) Then
                aVar.Item(key) = value
            End If
        End Set

    End Property

    Public Function Reset() As Boolean
        Dim linea As Object

        For Each linea In aVar
            aVar.Remove(linea.Key)
        Next
        For Each linea In tipos
            tipos.Remove(linea.Key)
        Next

    End Function

End Class

Public Class MiDataRow
    Dim dws As DataRow()

    Public Property rows() As DataRow()

        Get
            Return dws
        End Get

        Set(ByVal value As DataRow())
            dws = value
        End Set

    End Property

    Default Public ReadOnly Property Valor(key) As Object

        Get
            For Each dt2_row As System.Data.DataRow In dws
                If UCase(Trim(dt2_row("cod_variable"))) = UCase(Trim(key)) Then
                    Return dt2_row("valor_dato")
                End If
            Next
            Return 0
        End Get

        'Set(ByVal value As Object)
        '    For Each dt2_row As System.Data.DataRow In dws
        '        If UCase(Trim(dt2_row("cod_variable"))) = UCase(Trim(key)) Then
        '            dt2_row(Trim(key)) = value
        '        End If
        '    Next
        'End Set

    End Property

End Class