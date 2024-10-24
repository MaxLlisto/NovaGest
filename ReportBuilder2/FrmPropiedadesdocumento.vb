Imports System.Drawing.Printing
Imports System.Drawing

Public Class FrmPropiedadesdocumento
    Public pd As Formatos

    Public Sub New(ByVal pdc As Formatos)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        pd = pdc
        Copias.Value = IIf(pdc.copias = 0, 1, pdc.copias)
        If pdc.impresoradefecto = "" Then
            RbPredeterminada.Checked = True
            TxtImpresora.Enabled = False
            TxtImpresora.Text = ""
        Else
            rbPredefinida.Checked = True
            TxtImpresora.Enabled = True
            TxtImpresora.Text = pdc.impresoradefecto
        End If
        If pdc.a1 = "" Then
            rbvertical.Checked = True
        Else
            RbHorizontal.Checked = True
        End If
        LoadPaper()
        cbPapel.ValueMember = pdc.papel
        cbPapel.Text = pdc.papel
        cbDocumentoDefecto.Checked = IIf(pdc.defecto = "-1", True, False)

    End Sub

    Private Sub rbPredefinida_CheckedChanged(sender As Object, e As EventArgs) Handles rbPredefinida.CheckedChanged
        If rbPredefinida.Checked Then
            TxtImpresora.Enabled = True
        Else
            TxtImpresora.Enabled = False
        End If
    End Sub

    Private Sub RbPredeterminada_CheckedChanged(sender As Object, e As EventArgs) Handles RbPredeterminada.CheckedChanged
        If Not RbPredeterminada.Checked Then
            TxtImpresora.Enabled = True
        Else
            TxtImpresora.Enabled = False
        End If
    End Sub

    Private Sub BtCancelar_Click(sender As Object, e As EventArgs) Handles BtCancelar.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub BtGrabar_Click(sender As Object, e As EventArgs) Handles BtGrabar.Click

        If cbDocumentoDefecto.Checked Then
            pd.defecto = "-1"
        End If
        If RbPredeterminada.Checked Then
            pd.impresoradefecto = ""
        Else
            pd.impresoradefecto = TxtImpresora.Text
        End If
        If rbvertical.Checked Then
            pd.a1 = ""
            'printset.DefaultPageSettings.Landscape = False
        Else
            pd.a1 = "apaisado"
            'printset.DefaultPageSettings.Landscape = True
        End If
        pd.copias = CInt(Copias.Text)
        pd.papel = cbPapel.Text
        DialogResult = DialogResult.OK
        Close()

    End Sub


    Private Sub LoadPaper()
        Dim Pd As New PrintDialog()
        Dim i As Integer
        Dim _TamañoPapel As String 'PaperSize

        For i = 0 To Pd.PrinterSettings.PaperSizes.Count - 1
            _TamañoPapel = Pd.PrinterSettings.PaperSizes.Item(i).PaperName
            cbPapel.Items.Add(_TamañoPapel)
        Next
    End Sub

    Public ReadOnly Property propiedades() As Formatos
        Get
            Return pd
        End Get
    End Property

End Class

Public Class Propiedadesdocumento
    Public proceso As String
    Public documento As String
    Public formato As String
    Public copias As Integer
    Public impresora As sPrinterSel = sPrinterSel.predeterminada
    Public impresoradefecto As String
    Public Altura As Integer = 0
    Public defecto As Integer
    Public a1 As String
    Public a2 As String
    Public a3 As String
    Public n1 As Double
    Public n2 As Double
    Public n3 As Double
    Public imprimepdf As String
    Public carpetapdf As String
    Public impresoraPDF As String
    Public papel As String

    'impresoradefecto = "" & rs!impresoradefecto
    '           altura = IIf(Not IsDBNull(rs!altura), rs!altura, 0)
    '           a1 = "" & rs!a1
    '           a2 = "" & rs!a2
    '           a3 = "" & rs!a3
    '           n1 = IIf(Not IsDBNull(rs!n1), rs!n1, 0)
    '           n2 = IIf(Not IsDBNull(rs!n2), rs!n2, 0)
    '           n3 = IIf(Not IsDBNull(rs!n3), rs!n3, 0)
    '           imprimepdf = rs!imprimepdf
    '           carpetapdf = "" & rs!carpetapdf
    '           impresoraPDF = "" & rs!impresoraPDF
    '           papel = IIf("" & rs!papel = "", "A4", rs!papel)
    '           If impresoradefecto.Trim <> "" Then
    '               obtener_impresoras(impresoradefecto)
    '           End If


    Public Sub New()
        copias = 1
        impresora = 1
        posicion_papel = 1
    End Sub

    Public Sub New(c, i, p, n)
        copias = c
        impresora = i
        posicion_papel = p
        impresoradefecto = n
    End Sub

    Public Property posicion_papel() As SPapelPos
        Get
            Return a1
        End Get
        Set(ByVal Value As SPapelPos)
            a1 = Value
        End Set
    End Property

End Class

Public Enum sPrinterSel
    predeterminada = 1
    predefinida = 2
End Enum

Public Enum SPapelPos
    Vertical = 1
    Horizontal = 2
End Enum
