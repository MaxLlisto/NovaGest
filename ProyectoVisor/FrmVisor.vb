Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.ComponentModel

Public Class FrmVisor
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public licenseKey As String = "0B0E-336F-637B-39FF-80E3-38F4-CC63-F435-703D-007F-2E76-F831"
    Public documento As String
    Public licraw As String = "+NCRFvbIPeK6uS3qF1jvXty+cVc1BOG6+r4WFb7MDYGJb0S663IY"

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Gnostice.Documents.Framework.ActivateLicense(licenseKey)

    End Sub

    Private Sub CargarGRID(codProv)
        Dim Row As DataGridViewRow
        Dim RSGrabarProceso As New cnRecordset.CnRecordset
        Dim aExt() As String = {"doc", "xls", "pdf", "tif", "bmp", "jpg", "odp", "ods", "odt", "ppt", "txt", "png"}
        Dim fich As String
        Dim i As Integer
        Dim o As Integer
        Dim ext3 As String
        Dim ext4 As String
        Dim numfac As String
        Dim sql As String

        dgv.Rows.Clear()

        If Trim(cbEmpresa.SelectedValue) = "0" Then
            sql = "SELECT p.numero_entrada, f.fecha_entrada, f.serie_factura_com, f.numero_fra_com, f.fecha_factura, p.carpeta, p.fichero, p.situacion FROM gd_procesos p left join factura_com_c f ON f.empresa = p.empresa and f.numero_entrada_fra = p.numero_entrada WHERE p.codigo_proveedor=" & codProv & " AND fecha_factura BETWEEN '" & DesdeFecha.Value & "' and '" & HastaFecha.Value & "'  ORDER BY p.numero_entrada DESC, f.fecha_entrada DESC"
        Else
            sql = "SELECT p.numero_entrada, f.fecha_entrada, f.serie_factura_com, f.numero_fra_com, f.fecha_factura, p.carpeta, p.fichero, p.situacion FROM gd_procesos p left join factura_com_c f ON f.empresa = p.empresa and f.numero_entrada_fra = p.numero_entrada WHERE p.empresa = '" & cbEmpresa.SelectedValue & "' and p.codigo_proveedor=" & codProv & " AND fecha_factura BETWEEN '" & DesdeFecha.Value & "' and '" & HastaFecha.Value & "'  ORDER BY p.numero_entrada DESC, f.fecha_entrada DESC"
        End If


        If RSGrabarProceso.Open(sql, ObjetoGlobal.cn) Then
            For i = 0 To RSGrabarProceso.RecordCount - 1
                Row = New DataGridViewRow
                Row.Height = 36
                dgv.Rows.Add(Row)
            Next
            For i = 0 To RSGrabarProceso.RecordCount - 1
                RSGrabarProceso.AbsolutePosition = i + 1
                dgv.Rows(i).Cells.Item("entrada").Value = IIf(IsDBNull(RSGrabarProceso!numero_entrada), "", CStr(RSGrabarProceso!numero_entrada))
                If Not IsDBNull(RSGrabarProceso!fecha_entrada) Then
                    dgv.Rows(i).Cells.Item("Fecha").Value = CStr(RSGrabarProceso!fecha_entrada)
                End If
                numfac = ""
                If Not IsDBNull(RSGrabarProceso!serie_factura_com) Then
                    numfac = Trim(CStr(RSGrabarProceso!serie_factura_com))
                End If
                If Not IsDBNull(RSGrabarProceso!serie_factura_com) Then
                    numfac = numfac + " " + Trim(CStr(RSGrabarProceso!numero_fra_com))
                End If
                dgv.Rows(i).Cells.Item("factura").Value = Trim(numfac)
                If Not IsDBNull(RSGrabarProceso!fecha_factura) Then
                    dgv.Rows(i).Cells.Item("fechafactura").Value = CStr(RSGrabarProceso!fecha_factura)
                End If
                dgv.Rows(i).Cells.Item("Directorio").Value = IIf(IsDBNull(RSGrabarProceso!carpeta), "", Trim(CStr(RSGrabarProceso!carpeta)))
                dgv.Rows(i).Cells.Item("archivo").Value = IIf(IsDBNull(RSGrabarProceso!fichero), "", Trim(CStr(RSGrabarProceso!fichero)))
                dgv.Rows(i).Cells.Item("situacion").Value = IIf(IsDBNull(RSGrabarProceso!situacion), "", Trim(CStr(RSGrabarProceso!situacion)))
                dgv.Rows(i).Cells.Item("imprimir").Value = VisorDocumentos.My.Resources.Resource1.print
                For o = 0 To 11
                    fich = Trim(RSGrabarProceso!fichero)
                    fich = fich.ToLower
                    ext3 = fich.Substring(fich.Length - 3, 3)
                    ext4 = fich.Substring(fich.Length - 4, 4)
                    ext4 = ext4.Substring(1, 3)
                    If ext3 = aExt(o) Or ext4 = aExt(o) Then
                        dgv.Rows(i).Cells.Item("Visor").Value = VisorDocumentos.My.Resources.Resource1.ResourceManager.GetObject(aExt(o))
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        Dim Ruta As String
        If Not IsNothing(dgv.Rows(e.RowIndex).Cells("Directorio").Value) And Not IsNothing(dgv.Rows(e.RowIndex).Cells("archivo").Value) Then
            If e.ColumnIndex = 4 Then ' Imprimir
                Ruta = Path.Combine(dgv.Rows(e.RowIndex).Cells("Directorio").Value, dgv.Rows(e.RowIndex).Cells("archivo").Value)
                documento = Ruta
                ExecuteRawFilePrinter()
            ElseIf e.ColumnIndex = 5 Then ' Visualizar
                Ruta = Path.Combine(dgv.Rows(e.RowIndex).Cells("Directorio").Value, dgv.Rows(e.RowIndex).Cells("archivo").Value)
                documento = Ruta
                Try
                    Process.Start(Ruta)
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If
        End If

    End Sub

    Private Sub RellenaEmpresa()
        Dim cmd As New cnRecordset.CnRecordset
        Dim comboSource As New Dictionary(Of String, String)()
        Dim i As Integer

        cmd.Open("SELECT empresa, razon_social FROM empresas", ObjetoGlobal.cn)
        comboSource.Add("0", "Todas las empresas ")
        For i = 1 To cmd.RecordCount
            cmd.AbsolutePosition = i
            comboSource.Add(Trim(cmd!empresa), Trim(cmd!empresa) + " - " + Trim(cmd!razon_social))
        Next
        cbEmpresa.DataSource = New BindingSource(comboSource, Nothing)
        cbEmpresa.DisplayMember = "Value"
        cbEmpresa.ValueMember = "Key"
        cbEmpresa.SelectedIndex = 0

    End Sub

    Private Sub dgv_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellEnter
        Dim Ruta As String
        If Not IsNothing(dgv.Rows(e.RowIndex).Cells("Directorio").Value) And Not IsNothing(dgv.Rows(e.RowIndex).Cells("archivo").Value) Then
            Ruta = Trim(Path.Combine(dgv.Rows(e.RowIndex).Cells("Directorio").Value, dgv.Rows(e.RowIndex).Cells("archivo").Value))
            documento = Ruta
            MostrarDocumento(Ruta)
        End If

    End Sub

    Private Sub MostrarDocumento(ruta)
        Try
            dV1.CloseDocument()
            dV1.LoadDocument(ruta, "")
        Catch ex As Exception
            MessageBox.Show(ruta)
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Buscar_Click(sender As Object, e As EventArgs) Handles Buscar.Click
        Dim RSGrabarProceso As New cnRecordset.CnRecordset
        Dim dt As DataTable = New DataTable("Tabla")
        Dim dr As DataRow
        Dim sql As String

        If Trim(cbcodigo.Text) <> "" Then
            sql = "SELECT codigo_proveedor, razon_social From proveedores_coop WHERE razon_social Like '%" & UCase(Trim(cbcodigo.Text)) & "%' ORDER BY codigo_proveedor"
            If RSGrabarProceso.Open(sql, ObjetoGlobal.cn) Then
                dt.Columns.Add("codigo")
                dt.Columns.Add("razon_social")
                For i = 1 To RSGrabarProceso.RecordCount
                    RSGrabarProceso.AbsolutePosition = i
                    dr = dt.NewRow()
                    dr("Codigo") = RSGrabarProceso!codigo_proveedor
                    dr("razon_social") = RSGrabarProceso!razon_social
                    dt.Rows.Add(dr)
                Next
                cbcodigo.DataSource = dt
                cbcodigo.ValueMember = "codigo"
                cbcodigo.DisplayMember = "razon_social"
            End If
        End If

    End Sub
    Private Sub cbcodigo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbcodigo.SelectionChangeCommitted
        CargarGRID(cbcodigo.SelectedValue)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
        If ObjetoGlobal.Inicializar("") = False Then
            MsgBox("No se puede abrir la conexión a la BD")
        End If
        DesdeFecha.Value = CDate("01/01/" & (Year(Now()) - 1))
        HastaFecha.Value = Now()
        RellenaEmpresa()
    End Sub

    Private Sub panterior_Click(sender As Object, e As EventArgs) Handles panterior.Click
        If dV1.IsDocumentLoaded Then
            dV1.PreviousPage()
        End If
    End Sub

    Private Sub psiguiente_Click(sender As Object, e As EventArgs) Handles psiguiente.Click
        If dV1.IsDocumentLoaded Then
            dV1.NextPage()
        End If

    End Sub

    Private Sub pprimera_Click(sender As Object, e As EventArgs) Handles pprimera.Click
        If dV1.IsDocumentLoaded Then
            dV1.GoToPage(1)
        End If
    End Sub

    Private Sub pultima_Click(sender As Object, e As EventArgs) Handles pultima.Click
        If dV1.IsDocumentLoaded Then
            dV1.GoToPage(dV1.PageCount)
        End If
    End Sub

    Private Sub gizquierda_Click(sender As Object, e As EventArgs) Handles gizquierda.Click
        If dV1.IsDocumentLoaded Then
            dV1.RotatePagesAntiClockwise90()
        End If
    End Sub

    Private Sub gderecha_Click(sender As Object, e As EventArgs) Handles gderecha.Click
        If dV1.IsDocumentLoaded Then
            dV1.RotatePagesClockwise90()
        End If
    End Sub

    Private Sub pmas_Click(sender As Object, e As EventArgs) Handles pmas.Click
        If dV1.IsDocumentLoaded Then
            dV1.ZoomIn()
        End If
    End Sub

    Private Sub pmenos_Click(sender As Object, e As EventArgs) Handles pmenos.Click
        If dV1.IsDocumentLoaded Then
            dV1.ZoomOut()
        End If
    End Sub

    Private Sub pimprimir_Click(sender As Object, e As EventArgs) Handles pimprimir.Click
        If dV1.IsDocumentLoaded Then
            EjecutarRawFilePrinter()
        End If
    End Sub

    Private Sub EjecutarRawFilePrinter()
        Dim Process As Process = New Process()
        Dim Carpeta As String = Application.ExecutablePath 'My.Computer.FileSystem.CurrentDirectory
        Dim Impresora As PrintDialog = New PrintDialog
        Dim path1 As String
        Dim path2 As String
        Dim path3 As String

        '//Declaramos un objeto PrintDocument

        'Dim Documento As New Printing.PrintDocument
        '//Mostrar el botón de ayuda
        Impresora.ShowHelp = True
        '        Impresora.Document = Path_file

        DialogResult = Impresora.ShowDialog()

        path1 = ShortPath(Carpeta) & "\RawFilePrinter.exe"
        path2 = ShortPath(Carpeta & "\licencia.lic")
        path3 = ShortPath(documento)

        If DialogResult.OK Then
            Process.StartInfo.FileName = path1
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            Process.StartInfo.Arguments = " -p --lic """ + "+NCRFvbIPeK6uS3qF1jvXty+cVc1BOG6+r4WFb7MDYGJb0S663IY" + """ """ + path3 + """   """ + Impresora.PrinterSettings.PrinterName + """"
            Process.Start()
            Process.WaitForExit()

        End If
    End Sub

    Private Sub ExecuteRawFilePrinter()
        Dim Process As Process = New Process()
        Dim Carpeta As String = My.Computer.FileSystem.CurrentDirectory
        Dim Impresora As PrintDialog = New PrintDialog
        Dim proc As String
        Dim path1 As String
        Dim path2 As String
        Dim path3 As String


        Impresora.ShowHelp = True
        '        Impresora.Document = Path_file

        DialogResult = Impresora.ShowDialog()

        If DialogResult.OK Then
            path1 = ShortPath(Carpeta) & "\RawFilePrinter.exe"
            path2 = ShortPath(Carpeta & "\licencia.lic") + " "
            path3 = ShortPath(documento)

            proc = path1 + " -p --lic """ + "+NCRFvbIPeK6uS3qF1jvXty+cVc1BOG6+r4WFb7MDYGJb0S663IY" + """ """ + path3 + """   """ + Impresora.PrinterSettings.PrinterName + """"
            'proc = path1 + " -p --lic """ + "+NCRFvbIPeK6uS3qF1jvXty+cVc1BOG6+r4WFb7MDYGJb0S663IY" + """ """ + path3 + """   """ + "Canon iR35704570 Class Driver" + """"

            'proc = Carpeta & "\" & "RawFilePrinter.exe -p --lic " & licRawCarpeta & "\licencia.lic" & Chr(34) & " " & Chr(34) & documento & Chr(34) & " " & Chr(34) & Impresora.PrinterSettings.PrinterName & Chr(34)

            proc = proc.Replace(Chr(34) + Chr(34), "")

            Dim procID As Integer
            procID = Shell(proc)

        End If

    End Sub

    Public Property og() As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Declare Function GetShortPathName Lib "kernel32.dll" Alias "GetShortPathNameA" _
     (ByVal lpszLongPath As String, ByVal lpszShortPath As String, ByVal cchBuffer _
     As Integer) As Integer

    Private Function ShortPath(ByVal LongPath As String) As String
        Dim shrtPath As String = Space(255)
        GetShortPathName(LongPath, shrtPath, shrtPath.Length)

        Return shrtPath.Substring(0, InStr(shrtPath, Chr(0)) - 1)
    End Function

    Private Sub cbcodigo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbcodigo.SelectedIndexChanged

    End Sub
End Class

