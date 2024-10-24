Public Class Provisional
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    '    Dim CINF As Object
    '    Dim XProceso As String
    '    Dim XDocumento As String
    '    Dim XFormato As String
    '    Dim XDiccionario As Dictionary

    '    Dim Proceso As Long
    '    Dim ContadorGeneral As Long
    Dim Dias() As Date
    '    Dim VariedadesDia() As String
    Dim CuantosDias As Integer
    '    Dim Confecciones() As String
    '    Dim CuantasConfecciones As Integer
    Dim Familia As String
    '    Dim VariedadGeneral As String
    Dim CuantasColumnas As Long
    Dim NombreColumna(3, 0) As String
    '    Dim CuantasFilas As Long
    '    Dim NombreFila() As String

    '    Dim PesoIndustriaClementinaPequeno As Long
    '    Dim PesoIndustriaNaranjaPequeno As Long
    '    Dim PesoIndustriaClementinaGrande As Long
    '    Dim PesoIndustriaNaranjaGrande As Long


    'Private Sub CmdBuscarPlantilla_Click()
    '        CommonDialog1.FileName = TxtDocumento
    '        CommonDialog1.Filter = "*.xls"
    '        CommonDialog1.DialogTitle = "Grabar documento EXCEL"
    '        CommonDialog1.InitDir = Trim("\\Srvv02\Grupos\Comercial\Costes")
    '        CommonDialog1.ShowSave
    '        If Not CommonDialog1.CancelError Then TxtDocumento.Text = Trim(CommonDialog1.FileName)
    '    End Sub
    '    Private Sub cmdImprimir_Click()
    '        Dim FlagHayCampos As Boolean, Cuantos As Long
    '        Dim i As Integer

    '        PicCartel.Visible = True
    '        LblEnproceso.Caption = ""
    '        LblEnproceso.Visible = True
    '        LblMensaje.Caption = ""
    '        LblMensaje.Visible = True
    '        CuantasConfecciones = -1
    '        ReDim Confecciones(1, 0)
    '        If OptFamilia(0).Value = True Then
    '            Familia = "NAR"
    '            VariedadGeneral = "022"
    '        ElseIf OptFamilia(1).Value = True Then
    '            Familia = "CLE"
    '            VariedadGeneral = "012"
    '        ElseIf OptFamilia(2).Value = True Then
    '            Familia = "HIB"
    '            VariedadGeneral = "017"
    '        ElseIf OptFamilia(3).Value = True Then
    '            Familia = "SAT"
    '            VariedadGeneral = "014"
    '        Else
    '            Familia = "NAB"
    '            VariedadGeneral = "024"
    '        End If

    '        CuantosDias = 0
    '        ReDim Dias(0)
    '        Dias(0) = DTDesdeFecha.Value
    '        If DTHastaFecha.Value >= DTDesdeFecha.Value Then
    '            Cuantos = DateDiff("d", CDate(DTDesdeFecha.Value), CDate(DTHastaFecha.Value))
    '            For i = 1 To Cuantos
    '                CuantosDias = CuantosDias + 1
    '                ReDim Preserve Dias(CuantosDias)
    '                Dias(CuantosDias) = DateAdd("d", i, CDate(DTDesdeFecha.Value))
    '            Next
    '        End If

    '        GrabarExcel()

    '        PicCartel.Visible = False
    '        LblEnproceso.Caption = ""
    '        LblEnproceso.Visible = False
    '        LblMensaje.Caption = ""
    '        LblMensaje.Visible = False
    '    End Sub
    '    Private Sub CmdSalir_Click()
    '        Unload Me
    'End Sub
    '    Private Sub Form_Load()
    '        Dim Fecha As Date, Clave As String

    '        OptFamilia(0).Value = True
    '        SSTab1.Tab = 0
    '        SSTab1.TabVisible(1) = False
    '        SSTab1.TabVisible(2) = False
    '        Fecha = DateAdd("d", -1, Date)
    '        DTDesdeFecha.Value = Fecha
    '        DTHastaFecha.Value = Fecha
    '        PicCartel.Visible = False
    '        LblEnproceso.Caption = ""
    '        LblEnproceso.Visible = False
    '        LblMensaje.Caption = ""
    '        LblMensaje.Visible = False
    '        Clave = InputBox("Introduzca clave de acceso:")
    '        If LCase(Trim(Clave)) <> "vercostes" Then
    '            Unload Me
    'Else
    '            TxtDocumento.Text = "\\Srvv02\Grupos\Comercial\Costes"
    '        End If
    '    End Sub
    Private Sub GrabarExcel()
        Dim i As Integer, j As Integer, k As Integer, jj As Integer
        Dim Fecha As Date
        Dim Rs As New cnRecordset.CnRecordset, SQL As String, Cadena As String
        '        Dim Fila As Long, Columna As Long, ApartadoActual As Long, NombreActual As String
        '        Dim FilasTotales(10) As Long, Formulas(10, 99) As String, QueAcumulado As Integer
        '        Dim FilaSubapartado As Long, 
        Dim UltimoGrupo As String
        ' OJO Dim MiExcel As New LibreriaGeneral.EXCEL
        Dim MiRango As Microsoft.Office.Interop.Excel.Range

        '        Dim PlantillaExcel As excel.Workbook
        '        Dim HojaExcel As excel.Worksheet

        '        LblEnproceso.Caption = "Grabar Excel:"
        '        LblEnproceso.Refresh
        '        LblMensaje.Caption = ""

        '        If AppExcel Is Nothing Then Set AppExcel = New excel.Application
        For jj = 0 To CuantosDias
            '            LblMensaje.Caption = Format(Dias(jj))
            '            LblMensaje.Refresh
            '    Set PlantillaExcel = AppExcel.Workbooks.Open("\\Srvv02\Pt\Forfait.xlt")
            '    Set HojaExcel = PlantillaExcel.Sheets(1)
            '    AppExcel.Visible = False

            '            'Cabecera del informe

            Fecha = Dias(jj)
            UltimoGrupo = "NO"
            CuantasColumnas = 0
            ReDim NombreColumna(3, 0)
            SQL = "SELECT H_COSTES_FABRICACION.EMPRESA,H_COSTES_FABRICACION.GRUPO_CONFECCION,H_COSTES_FABRICACION.CODIGO_CONFECCION, CONFECCION.SUBGRUPO_CONFECCION, SUBGRUPOS_CONFECCION.GRUPO_CONFECCION, "
            SQL = SQL + " CONFECCION.DESCRIPCION,GRUPOS_CONFECCION.PARA_ORDENAR,SUM(H_COSTES_FABRICACION.PALETS) AS PALETS,SUM(H_COSTES_FABRICACION.KILOS) AS KILOS FROM H_COSTES_FABRICACION "
            SQL = SQL + " LEFT JOIN CONFECCION ON CONFECCION.EMPRESA = H_COSTES_FABRICACION.EMPRESA AND CONFECCION.CODIGO_CONFECCION = H_COSTES_FABRICACION.CODIGO_CONFECCION "
            SQL = SQL + " LEFT JOIN SUBGRUPOS_CONFECCION ON SUBGRUPOS_CONFECCION.EMPRESA = H_COSTES_FABRICACION.EMPRESA AND SUBGRUPOS_CONFECCION.CODIGO_SUBGRUPO=CONFECCION.SUBGRUPO_CONFECCION "
            SQL = SQL + " LEFT JOIN GRUPOS_CONFECCION ON GRUPOS_CONFECCION.EMPRESA = H_COSTES_FABRICACION.EMPRESA AND GRUPOS_CONFECCION.GRUPO_CONFECCION=SUBGRUPOS_CONFECCION.GRUPO_CONFECCION "
            SQL = SQL + " WHERE H_COSTES_FABRICACION.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "'"
            SQL = SQL + " AND H_COSTES_FABRICACION.FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND H_COSTES_FABRICACION.TIPO_COSTE = '1' AND H_COSTES_FABRICACION.FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "'"
            SQL = SQL + " GROUP BY H_COSTES_FABRICACION.EMPRESA, H_COSTES_FABRICACION.GRUPO_CONFECCION,H_COSTES_FABRICACION.CODIGO_CONFECCION,CONFECCION.DESCRIPCION, CONFECCION.SUBGRUPO_CONFECCION, SUBGRUPOS_CONFECCION.GRUPO_CONFECCION,GRUPOS_CONFECCION.PARA_ORDENAR "
            SQL = SQL + " ORDER BY H_COSTES_FABRICACION.EMPRESA, GRUPOS_CONFECCION.PARA_ORDENAR, SUBGRUPOS_CONFECCION.GRUPO_CONFECCION, CONFECCION.SUBGRUPO_CONFECCION, H_COSTES_FABRICACION.GRUPO_CONFECCION, H_COSTES_FABRICACION.CODIGO_CONFECCION"
            If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla h_costes_fabricacion")
                Exit Sub
            End If

            'If RB1.Checked = True Then
            '    Miexcel.EscribeCelda(1, 2, "NARANJA " + Format(Fecha, "dd/MM/yyyy"))
            'ElseIf RB2.Checked = True Then
            '    Miexcel.EscribeCelda(1, 2, "CLEMENTINA " + Format(Fecha, "dd/MM/yyyy"))
            'ElseIf RB3.Checked = True Then
            '    Miexcel.EscribeCelda(1, 2, "HIBRIDOS " + Format(Fecha, "dd/MM/yyyy"))
            'ElseIf RB4.Checked = True Then
            '    Miexcel.EscribeCelda(1, 2, "SATSUMA " + Format(Fecha, "dd/MM/yyyy"))
            'Else
            '    Miexcel.EscribeCelda(1, 2, "NAR.BLANCA " + Format(Fecha, "dd/MM/yyyy"))
            'End If

            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                CuantasColumnas = CuantasColumnas + 1
                ReDim Preserve NombreColumna(3, CuantasColumnas)
                NombreColumna(0, CuantasColumnas) = Trim(Rs!codigo_confeccion)
                NombreColumna(1, CuantasColumnas) = Trim(Rs!descripcion) + "(" + Trim(Rs!grupo_confeccion) + ")"
                NombreColumna(2, CuantasColumnas) = CStr(Rs!Palets)
                NombreColumna(3, CuantasColumnas) = CStr(Rs!Kilos)
            Next

            For i = 1 To CuantasColumnas
                Cadena = XColumna(1 + i) + "3"

                'MiExcel.rangoActual = Cadena

                MiRango.NumberFormat = "@"
                MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                MiRango.WrapText = True
                MiRango.Orientation = 0
                MiRango.AddIndent = False
                MiRango.ShrinkToFit = False
                MiRango.MergeCells = False

                MiRango.Font.Name = "Arial"
                MiRango.Font.Size = 8
                MiRango.Font.Strikethrough = False
                MiRango.Font.Superscript = False
                MiRango.Font.Subscript = False
                MiRango.Font.OutlineFont = False
                MiRango.Font.Shadow = False
                'MiRango.Font.Underline = Microsoft.Office.Interop.Excel.Constants.xlUnderlineStyleNone
                MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                MiRango.Font.Bold = True

                '                HojaExcel.Cells(3, 1 + i).Value = Trim(NombreColumna(1, i)) + Chr(10) + "(Palets:" + Trim(NombreColumna(2, i)) + Chr(10) + "Kg:" + Trim(NombreColumna(3, i)) + Chr(10) + "Cod:" + Trim(NombreColumna(0, i)) + ")"
                '                Cadena = XColumna(1 + i) + ":" + XColumna(1 + i)
                '                HojaExcel.Columns(Cadena).ColumnWidth = 9
            Next

            '            Rs.Close
            '            For i = 0 To 10
            '                For j = 0 To 99
            '                    Formulas(i, j) = ""
            '                Next
            '            Next

            '            'Cuerpo del informe

            '            CuantasFilas = 0
            '            ReDim NombreFila(5, 0)
            '            ApartadoActual = -1
            '            FilaSubapartado = -1
            '            SQL = "SELECT * FROM GRUPOS_COSTE_INFORME JOIN GRUPOS_COSTE ON GRUPOS_COSTE.EMPRESA = GRUPOS_COSTE_INFORME.EMPRESA AND GRUPOS_COSTE.CODIGO_GRUPO = GRUPOS_COSTE_INFORME.CODIGO_GRUPO ORDER BY GRUPOS_COSTE_INFORME.EMPRESA,GRUPOS_COSTE_INFORME.APARTADO,GRUPOS_COSTE_INFORME.SUBAPARTADO,GRUPOS_COSTE_INFORME.LINEA_INFORME"
            '            Rs.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
            '    For i = 1 To Rs.RecordCount
            '                Rs.AbsolutePosition = i

            '                '       Cambio de grupo

            '                If ApartadoActual <> Rs!apartado Then
            '                    If ApartadoActual > -1 Then
            '                        CuantasFilas = CuantasFilas + 1
            '                        ReDim Preserve NombreFila(5, CuantasFilas)
            '                        NombreFila(0, CuantasFilas) = CStr(ApartadoActual)
            '                        NombreFila(1, CuantasFilas) = "TOTAL " + Trim(NombreActual)
            '                        NombreFila(2, CuantasFilas) = "0"
            '                        NombreFila(3, CuantasFilas) = CStr(ApartadoActual)
            '                        NombreFila(4, CuantasFilas) = "0"
            '                        NombreFila(5, CuantasFilas) = "0"
            '                        FilasTotales(ApartadoActual) = CuantasFilas
            '                        Cadena = "A" + CStr(CuantasFilas + 3)
            '                        HojaExcel.Range(Cadena).Select
            '                        AppExcel.Selection.NumberFormat = "@"
            '                        AppExcel.Selection.HorizontalAlignment = xlCenter
            '                        AppExcel.Selection.VerticalAlignment = xlCenter
            '                        AppExcel.Selection.WrapText = False
            '                        AppExcel.Selection.AddIndent = False
            '                        AppExcel.Selection.ShrinkToFit = False
            '                        AppExcel.Selection.MergeCells = False
            '                        With AppExcel.Selection.Font
            '                            .Name = "Arial"
            '                            .Size = 8
            '                            .Strikethrough = False
            '                            .Superscript = False
            '                            .Subscript = False
            '                            .OutlineFont = False
            '                            .Shadow = False
            '                            .Underline = xlUnderlineStyleNone
            '                            .ColorIndex = 1
            '                            .Bold = True
            '                        End With
            '                        If ApartadoActual = 1 Then
            '                            AppExcel.Selection.Interior.ColorIndex = 36
            '                        ElseIf ApartadoActual = 2 Then
            '                            AppExcel.Selection.Interior.ColorIndex = 40
            '                        Else
            '                            AppExcel.Selection.Interior.ColorIndex = 35
            '                        End If
            '                        AppExcel.Selection.Interior.Pattern = xlSolid
            '                        HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))

            '                        Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
            '                        HojaExcel.Range(Cadena).Select
            '                        AppExcel.Selection.NumberFormat = "0.0000"
            '                        AppExcel.Selection.HorizontalAlignment = xlRight
            '                        AppExcel.Selection.VerticalAlignment = xlBottom
            '                        AppExcel.Selection.WrapText = False
            '                        AppExcel.Selection.Orientation = 0
            '                        AppExcel.Selection.AddIndent = False
            '                        AppExcel.Selection.ShrinkToFit = False
            '                        AppExcel.Selection.MergeCells = False
            '                        With AppExcel.Selection.Font
            '                            .Name = "Arial"
            '                            .FontStyle = "Normal"
            '                            .Size = 8
            '                            .Strikethrough = False
            '                            .Superscript = False
            '                            .Subscript = False
            '                            .OutlineFont = False
            '                            .Shadow = False
            '                            .Underline = xlUnderlineStyleNone
            '                            .ColorIndex = xlAutomatic
            '                            .Bold = True
            '                        End With
            '                        For j = 2 To CuantasColumnas + 1
            '                            HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
            '                        Next
            '                    End If
            '                    ApartadoActual = Rs!apartado
            '                    NombreActual = Trim(Rs!descripcion)
            '                End If

            '                '       Nueva Línea

            '                CuantasFilas = CuantasFilas + 1
            '                ReDim Preserve NombreFila(5, CuantasFilas)
            '                NombreFila(0, CuantasFilas) = Trim(Rs!codigo_grupo)
            '                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
            '                    NombreFila(1, CuantasFilas) = Space(10) + CStr(Rs!apartado) + "." + CStr(Rs!subapartado) + "." + CStr(Rs!linea_informe) + " " + Trim(Rs!descripcion)
            '                ElseIf Rs!apartado > 0 And Rs!subapartado > 0 Then
            '                    NombreFila(1, CuantasFilas) = CStr(Rs!apartado) + "." + CStr(Rs!subapartado) + " " + Trim(Rs!descripcion)
            '                    FilaSubapartado = CuantasFilas
            '                    For j = 0 To 99 : Formulas(0, j) = "" : Next
            '                Else
            '                    NombreFila(1, CuantasFilas) = CStr(Rs!apartado) + " " + Trim(Rs!descripcion)
            '                    FilaSubapartado = -1
            '                    For j = 0 To 99 : Formulas(0, j) = "" : Next
            '                End If
            '                NombreFila(2, CuantasFilas) = CStr(i)
            '                NombreFila(3, CuantasFilas) = CStr(Rs!apartado)
            '                NombreFila(4, CuantasFilas) = CStr(Rs!subapartado)
            '                NombreFila(5, CuantasFilas) = CStr(Rs!linea_informe)
            '                Cadena = "A" + CStr(CuantasFilas + 3)
            '                HojaExcel.Range(Cadena).Select
            '                AppExcel.Selection.NumberFormat = "@"
            '                AppExcel.Selection.HorizontalAlignment = xlLeft
            '                AppExcel.Selection.VerticalAlignment = xlCenter
            '                AppExcel.Selection.WrapText = False
            '                AppExcel.Selection.AddIndent = False
            '                AppExcel.Selection.ShrinkToFit = False
            '                AppExcel.Selection.MergeCells = False
            '                With AppExcel.Selection.Font
            '                    .Name = "Arial"
            '                    .Size = 8
            '                    .Strikethrough = False
            '                    .Superscript = False
            '                    .Subscript = False
            '                    .OutlineFont = False
            '                    .Shadow = False
            '                    .Underline = xlUnderlineStyleNone
            '                    .ColorIndex = xlAutomatic
            '                    .Bold = True
            '                End With
            '                If ApartadoActual = 1 Then
            '                    AppExcel.Selection.Interior.ColorIndex = 36
            '                ElseIf ApartadoActual = 2 Then
            '                    AppExcel.Selection.Interior.ColorIndex = 40
            '                Else
            '                    AppExcel.Selection.Interior.ColorIndex = 35
            '                End If
            '                HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))
            '                If Rs!apartado > 0 And Rs!subapartado = 0 And Rs!linea_informe = 0 Then
            '                    AppExcel.Selection.Interior.ColorIndex = 16
            '                    AppExcel.Selection.Interior.Pattern = xlSolid
            '                    AppExcel.Selection.Font.ColorIndex = 2
            '                Else
            '                    If ApartadoActual = 1 Then
            '                        AppExcel.Selection.Interior.ColorIndex = 36
            '                    ElseIf ApartadoActual = 2 Then
            '                        AppExcel.Selection.Interior.ColorIndex = 40
            '                    Else
            '                        AppExcel.Selection.Interior.ColorIndex = 35
            '                    End If
            '                End If
            '                Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(CuantasColumnas + 1) + CStr(CuantasFilas + 3)
            '                HojaExcel.Range(Cadena).Select
            '                If Rs!apartado > 0 And Rs!subapartado = 0 And Rs!linea_informe = 0 Then
            '                    AppExcel.Selection.Interior.ColorIndex = 16
            '                    AppExcel.Selection.Interior.Pattern = xlSolid
            '                    AppExcel.Selection.Font.ColorIndex = 2
            '                Else
            '                    If ApartadoActual = 1 Then
            '                        AppExcel.Selection.Interior.ColorIndex = 36
            '                    ElseIf ApartadoActual = 2 Then
            '                        AppExcel.Selection.Interior.ColorIndex = 40
            '                    Else
            '                        AppExcel.Selection.Interior.ColorIndex = 35
            '                    End If
            '                    AppExcel.Selection.NumberFormat = "0.0000"
            '                    AppExcel.Selection.HorizontalAlignment = xlRight
            '                    AppExcel.Selection.VerticalAlignment = xlBottom
            '                    AppExcel.Selection.WrapText = False
            '                    AppExcel.Selection.Orientation = 0
            '                    AppExcel.Selection.AddIndent = False
            '                    AppExcel.Selection.ShrinkToFit = False
            '                    AppExcel.Selection.MergeCells = False
            '                    With AppExcel.Selection.Font
            '                        .Name = "Arial"
            '                        .FontStyle = "Normal"
            '                        .Size = 8
            '                        .Strikethrough = False
            '                        .Superscript = False
            '                        .Subscript = False
            '                        .OutlineFont = False
            '                        .Shadow = False
            '                        .Underline = xlUnderlineStyleNone
            '                        .ColorIndex = xlAutomatic
            '                    End With
            '                    For j = 2 To CuantasColumnas + 1
            '                        HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
            '                    Next
            '                End If
            '                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
            '                    For j = 1 To CuantasColumnas
            '                        Formulas(0, j) = Formulas(0, j) + "+R" + CStr(CuantasFilas + 3) + "C" + CStr(j + 1)
            '                        Cadena = XColumna(CLng(j + 1)) + CStr(FilaSubapartado + 3)
            '                        HojaExcel.Range(Cadena).Select
            '                        AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(0, j), 2)
            '                    Next
            '                End If
            '            Next
            '            Rs.Close
            '            If ApartadoActual > -1 Then
            '                CuantasFilas = CuantasFilas + 1
            '                ReDim Preserve NombreFila(5, CuantasFilas)
            '                NombreFila(0, CuantasFilas) = CStr(ApartadoActual)
            '                NombreFila(1, CuantasFilas) = "TOTAL " + Trim(NombreActual)
            '                NombreFila(2, CuantasFilas) = "0"
            '                NombreFila(3, CuantasFilas) = CStr(ApartadoActual)
            '                NombreFila(4, CuantasFilas) = "0"
            '                NombreFila(5, CuantasFilas) = "0"
            '                FilasTotales(ApartadoActual) = CuantasFilas
            '                Cadena = "A" + CStr(CuantasFilas + 3)
            '                HojaExcel.Range(Cadena).Select
            '                AppExcel.Selection.NumberFormat = "@"
            '                AppExcel.Selection.HorizontalAlignment = xlCenter
            '                AppExcel.Selection.VerticalAlignment = xlCenter
            '                AppExcel.Selection.WrapText = False
            '                AppExcel.Selection.AddIndent = False
            '                AppExcel.Selection.ShrinkToFit = False
            '                AppExcel.Selection.MergeCells = False
            '                With AppExcel.Selection.Font
            '                    .Name = "Arial"
            '                    .Size = 8
            '                    .Strikethrough = False
            '                    .Superscript = False
            '                    .Subscript = False
            '                    .OutlineFont = False
            '                    .Shadow = False
            '                    .Underline = xlUnderlineStyleNone
            '                    .ColorIndex = 1
            '                    .Bold = True
            '                End With
            '                If ApartadoActual = 1 Then
            '                    AppExcel.Selection.Interior.ColorIndex = 36
            '                ElseIf ApartadoActual = 2 Then
            '                    AppExcel.Selection.Interior.ColorIndex = 40
            '                Else
            '                    AppExcel.Selection.Interior.ColorIndex = 35
            '                End If
            '                AppExcel.Selection.Interior.Pattern = xlSolid
            '                HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))

            '                Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
            '                HojaExcel.Range(Cadena).Select
            '                AppExcel.Selection.NumberFormat = "0.0000"
            '                AppExcel.Selection.HorizontalAlignment = xlRight
            '                AppExcel.Selection.VerticalAlignment = xlBottom
            '                AppExcel.Selection.WrapText = False
            '                AppExcel.Selection.Orientation = 0
            '                AppExcel.Selection.AddIndent = False
            '                AppExcel.Selection.ShrinkToFit = False
            '                AppExcel.Selection.MergeCells = False
            '                With AppExcel.Selection.Font
            '                    .Name = "Arial"
            '                    .FontStyle = "Normal"
            '                    .Size = 8
            '                    .Strikethrough = False
            '                    .Superscript = False
            '                    .Subscript = False
            '                    .OutlineFont = False
            '                    .Shadow = False
            '                    .Underline = xlUnderlineStyleNone
            '                    .ColorIndex = xlAutomatic
            '                    .Bold = True
            '                End With
            '                For j = 2 To CuantasColumnas + 1
            '                    HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
            '                Next
            '            End If
            '            CuantasFilas = CuantasFilas + 1
            '            ReDim Preserve NombreFila(5, CuantasFilas)
            '            NombreFila(0, CuantasFilas) = "TOTALES INFORME"
            '            NombreFila(1, CuantasFilas) = "TOTALES INFORME"
            '            NombreFila(2, CuantasFilas) = "-1"
            '            NombreFila(3, CuantasFilas) = "0"
            '            NombreFila(4, CuantasFilas) = "0"
            '            NombreFila(5, CuantasFilas) = "0"
            '            FilasTotales(0) = CuantasFilas
            '            Cadena = "A" + CStr(CuantasFilas + 3)
            '            HojaExcel.Range(Cadena).Select
            '            AppExcel.Selection.NumberFormat = "@"
            '            AppExcel.Selection.HorizontalAlignment = xlLeft
            '            AppExcel.Selection.VerticalAlignment = xlCenter
            '            AppExcel.Selection.WrapText = False
            '            AppExcel.Selection.AddIndent = False
            '            AppExcel.Selection.ShrinkToFit = False
            '            AppExcel.Selection.MergeCells = False
            '            With AppExcel.Selection.Font
            '                .Name = "Arial"
            '                .Size = 10
            '                .Strikethrough = False
            '                .Superscript = False
            '                .Subscript = False
            '                .OutlineFont = False
            '                .Shadow = False
            '                .Underline = xlUnderlineStyleNone
            '                .ColorIndex = 2
            '                .Bold = True
            '            End With
            '            Cadena = CStr(CuantasFilas + 3) + ":" + CStr(CuantasFilas + 3)
            '            AppExcel.Selection.Interior.ColorIndex = 16
            '            AppExcel.Selection.Interior.Pattern = xlSolid
            '            HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))
            '            HojaExcel.Range(Cadena).Select
            '            AppExcel.Selection.RowHeight = 18.75

            '            Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
            '            HojaExcel.Range(Cadena).Select
            '            AppExcel.Selection.NumberFormat = "0.0000"
            '            AppExcel.Selection.HorizontalAlignment = xlRight
            '            AppExcel.Selection.VerticalAlignment = xlBottom
            '            AppExcel.Selection.WrapText = False
            '            AppExcel.Selection.Orientation = 0
            '            AppExcel.Selection.AddIndent = False
            '            AppExcel.Selection.ShrinkToFit = False
            '            AppExcel.Selection.MergeCells = False
            '            With AppExcel.Selection.Font
            '                .Name = "Arial"
            '                .FontStyle = "Normal"
            '                .Size = 10
            '                .Strikethrough = False
            '                .Superscript = False
            '                .Subscript = False
            '                .OutlineFont = False
            '                .Shadow = False
            '                .Underline = xlUnderlineStyleNone
            '                .ColorIndex = xlAutomatic
            '                .Bold = True
            '            End With
            '            For j = 2 To CuantasColumnas + 1
            '                HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
            '            Next

            '            'Valores costes

            '            SQL = "SELECT H_COSTES_FABRICACION.* FROM H_COSTES_FABRICACION WHERE H_COSTES_FABRICACION.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND H_COSTES_FABRICACION.FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND H_COSTES_FABRICACION.TIPO_COSTE = '3' AND H_COSTES_FABRICACION.FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "' ORDER BY H_COSTES_FABRICACION.EMPRESA,H_COSTES_FABRICACION.CONTADOR"
            '            Rs.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
            '    For i = 1 To Rs.RecordCount
            '                Rs.AbsolutePosition = i
            '                Fila = QueFilaEs(Rs)
            '                Columna = QueColumnaEs(Rs)
            '                If Round(Rs!centimos_kg, 4) = 0 Then
            '                    HojaExcel.Cells(Fila, Columna).Value = "0"
            '                Else
            '                    HojaExcel.Cells(Fila, Columna).Value = Replace(Format(Rs!centimos_kg, "##.####"), ",", ".")
            '                End If
            '            Next

            '            'Fórmulas de totales
            '            For i = 0 To 10
            '                For j = 0 To 99
            '                    Formulas(i, j) = ""
            '                Next
            '            Next
            '            For i = 1 To CuantasFilas
            '                If CInt(NombreFila(2, i)) > "0" And CInt(NombreFila(3, i)) > "0" And CInt(NombreFila(4, i)) > "0" And CInt(NombreFila(5, i)) = "0" Then
            '                    QueAcumulado = CInt(NombreFila(3, i))
            '                    For j = 1 To CuantasColumnas
            '                        Formulas(QueAcumulado, j) = Formulas(QueAcumulado, j) + "+R" + CStr(i + 3) + "C" + CStr(j + 1)
            '                    Next
            '                End If
            '            Next
            '            For i = 1 To 10
            '                If FilasTotales(i) > 0 Then
            '                    For j = 1 To CuantasColumnas
            '                        Cadena = XColumna(CLng(j + 1)) + CStr(FilasTotales(i) + 3)
            '                        HojaExcel.Range(Cadena).Select
            '                        AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(i, j), 2)
            '                    Next
            '                End If
            '            Next
            '            For j = 1 To CuantasColumnas
            '                For i = 1 To 10
            '                    If FilasTotales(i) > 0 Then
            '                        Formulas(0, j) = Formulas(0, j) + "+R" + CStr(FilasTotales(i) + 3) + "C" + CStr(j + 1)
            '                    End If
            '                Next
            '            Next
            '            For j = 1 To CuantasColumnas
            '                Cadena = XColumna(CLng(j + 1)) + CStr(FilasTotales(0) + 3)
            '                HojaExcel.Range(Cadena).Select
            '                AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(0, j), 2)
            '            Next
            '            With HojaExcel.PageSetup
            '                .PrintTitleRows = "$1:$3"
            '                .PrintTitleColumns = "$A:$A"
            '                .PrintArea = ""
            '                .LeftHeader = ""
            '                .CenterHeader = ""
            '                .RightHeader = ""
            '                .LeftFooter = ""
            '                .CenterFooter = ""
            '                .RightFooter = ""
            '                .LeftMargin = AppExcel.InchesToPoints(0)
            '                .RightMargin = AppExcel.InchesToPoints(0)
            '                .TopMargin = AppExcel.InchesToPoints(0)
            '                .BottomMargin = AppExcel.InchesToPoints(0)
            '                .HeaderMargin = AppExcel.InchesToPoints(0)
            '                .FooterMargin = AppExcel.InchesToPoints(0)
            '                .PrintHeadings = False
            '                .PrintGridlines = True
            '                .PrintComments = xlPrintNoComments
            '                .PrintQuality = 600
            '                .CenterHorizontally = False
            '                .CenterVertically = False
            '                .Orientation = xlPortrait
            '                .Draft = False
            '                .PaperSize = xlPaperA4
            '                .FirstPageNumber = xlAutomatic
            '                .Order = xlDownThenOver
            '                .BlackAndWhite = False
            '                .Zoom = 100
            '            End With

            '            Cadena = "A3"
            '            HojaExcel.Range(Cadena).Select
            '            If CuantasColumnas = 0 Then
            '                PlantillaExcel.Saved = True
            '            Else
            '                HojaExcel.SaveAs Trim(TxtDocumento.Text + "\" + Trim(Familia) + "_" + Format(Fecha, "yyyymmdd") + "__" + Format(Now, "hhmmss") + ".xls")
            '    End If

            '            PlantillaExcel.Close
            '            Rs.Close
        Next
        'Set HojaExcel = Nothing
        'Set PlantillaExcel = Nothing
        'AppExcel.Quit
        'Set AppExcel = Nothing
        'MsgBox "Se ha grabado correctamente la hoja EXCEL.", vbInformation + vbOKOnly
    End Sub

    '    'Private Sub GrabarExcelResumen()
    '    'Dim i As Integer, j As Integer, k As Integer, jj As Integer, Fecha As Date
    '    'Dim Rs As Recordset, RsGeneral As Recordset, SQL As String, Cadena As String, ImporteL As Double
    '    'Dim Fila As Long, Columna As Long, ApartadoActual As Long, NombreActual As String
    '    'Dim FilasTotales(10) As Long, Formulas(10, 99) As String, QueAcumulado As Integer
    '    'Dim FilaSubapartado As Long
    '    'Dim AppExcel As excel.Application
    '    'Dim PlantillaExcel As excel.Workbook
    '    'Dim HojaExcel As excel.Worksheet
    '    '
    '    'Set RsGeneral = New Recordset
    '    'RsGeneral.CursorLocation = adUseClient
    '    'Set Rs = New Recordset
    '    'Rs.CursorLocation = adUseClient
    '    'LblEnproceso.Caption = "Grabar Excel:"
    '    'LblEnproceso.Refresh
    '    'LblMensaje.Caption = ""
    '    'If AppExcel Is Nothing Then Set AppExcel = New excel.Application
    '    'Set PlantillaExcel = AppExcel.Workbooks.Open("\\Srvv02\Pt\ForfaitG.xlt")
    '    'Set HojaExcel = PlantillaExcel.Sheets(1)
    '    'AppExcel.Visible = True
    '    ''Cabecera del informe
    '    'If OptFamilia(0).Value = True Then
    '    '    HojaExcel.Cells(1, 2).Value = "NARANJA "
    '    'ElseIf OptFamilia(1).Value = True Then
    '    '    HojaExcel.Cells(1, 2).Value = "CLEMENTINA "
    '    'ElseIf OptFamilia(2).Value = True Then
    '    '    HojaExcel.Cells(1, 2).Value = "HIBRIDOS "
    '    'Else
    '    '    HojaExcel.Cells(1, 2).Value = "SATSUMA "
    '    'End If
    '    'CuantasColumnas = 0
    '    'For jj = 0 To CuantosDias
    '    '    Fecha = Dias(jj)
    '    '    SQL = "SELECT sum(kilos) as kilos FROM H_COSTES_FABRICACION WHERE H_COSTES_FABRICACION.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND H_COSTES_FABRICACION.PROCESO = " + CStr(proceso) + " AND H_COSTES_FABRICACION.FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND H_COSTES_FABRICACION.TIPO_COSTE = '1' AND H_COSTES_FABRICACION.FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "'"
    '    '    Rs.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
    '    '    If Rs!Kilos > 0 Then
    '    '        CuantasColumnas = CuantasColumnas + 1
    '    '        ReDim Preserve NombreColumna(3, CuantasColumnas)
    '    '        NombreColumna(0, CuantasColumnas) = Format(Fecha, "dd/MM/yyyy")
    '    '        NombreColumna(1, CuantasColumnas) = CStr(Rs!Kilos)
    '    '    End If
    '    '    Rs.Close
    '    'Next jj
    '    'For i = 1 To CuantasColumnas
    '    '    Cadena = XColumna(1 + i) + "2"
    '    '    HojaExcel.Range(Cadena).Select
    '    '    AppExcel.Selection.NumberFormat = "@"
    '    '    AppExcel.Selection.HorizontalAlignment = xlCenter
    '    '    AppExcel.Selection.VerticalAlignment = xlCenter
    '    '    AppExcel.Selection.WrapText = True
    '    '    AppExcel.Selection.Orientation = 0
    '    '    AppExcel.Selection.AddIndent = False
    '    '    AppExcel.Selection.ShrinkToFit = False
    '    '    AppExcel.Selection.MergeCells = False
    '    '    With AppExcel.Selection.Font
    '    '        .Name = "Arial"
    '    '        .Size = 8
    '    '        .Strikethrough = False
    '    '        .Superscript = False
    '    '        .Subscript = False
    '    '        .OutlineFont = False
    '    '        .Shadow = False
    '    '        .Underline = xlUnderlineStyleNone
    '    '        .ColorIndex = xlAutomatic
    '    '        .Bold = True
    '    '    End With
    '    '    HojaExcel.Cells(2, 1 + i).Value = "'" & Trim(NombreColumna(0, i))
    '    '    HojaExcel.Cells(3, 1 + i).Value = Trim(NombreColumna(1, i))
    '    '    Cadena = XColumna(1 + i) + ":" + XColumna(1 + i)
    '    '    HojaExcel.Columns(Cadena).ColumnWidth = 9
    '    'Next
    '    ''Rs.Close
    '    'For i = 0 To 10
    '    '    For j = 0 To 99
    '    '        Formulas(i, j) = ""
    '    '    Next
    '    'Next
    '    ''
    '    '''Cuerpo del informe
    '    ''
    '    'CuantasFilas = 0
    '    'ReDim NombreFila(5, 0)
    '    'ApartadoActual = -1
    '    'FilaSubapartado = -1
    '    'SQL = "SELECT * FROM GRUPOS_COSTE_INFORME JOIN GRUPOS_COSTE ON GRUPOS_COSTE.EMPRESA = GRUPOS_COSTE_INFORME.EMPRESA AND GRUPOS_COSTE.CODIGO_GRUPO = GRUPOS_COSTE_INFORME.CODIGO_GRUPO ORDER BY GRUPOS_COSTE_INFORME.EMPRESA,GRUPOS_COSTE_INFORME.APARTADO,GRUPOS_COSTE_INFORME.SUBAPARTADO,GRUPOS_COSTE_INFORME.LINEA_INFORME"
    '    'Rs.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
    '    'For i = 1 To Rs.RecordCount
    '    '    Rs.AbsolutePosition = i
    '    ''
    '    '''       Cambio de grupo
    '    ''
    '    '    If ApartadoActual <> Rs!apartado Then
    '    '        If ApartadoActual > -1 Then
    '    '            CuantasFilas = CuantasFilas + 1
    '    '            ReDim Preserve NombreFila(5, CuantasFilas)
    '    '            NombreFila(0, CuantasFilas) = CStr(ApartadoActual)
    '    '            NombreFila(1, CuantasFilas) = "TOTAL " + Trim(NombreActual)
    '    '            NombreFila(2, CuantasFilas) = "0"
    '    '            NombreFila(3, CuantasFilas) = CStr(ApartadoActual)
    '    '            NombreFila(4, CuantasFilas) = "0"
    '    '            NombreFila(5, CuantasFilas) = "0"
    '    '            FilasTotales(ApartadoActual) = CuantasFilas
    '    '            Cadena = "A" + CStr(CuantasFilas + 3)
    '    '            HojaExcel.Range(Cadena).Select
    '    '            AppExcel.Selection.NumberFormat = "@"
    '    '            AppExcel.Selection.HorizontalAlignment = xlCenter
    '    '            AppExcel.Selection.VerticalAlignment = xlCenter
    '    '            AppExcel.Selection.WrapText = False
    '    '            AppExcel.Selection.AddIndent = False
    '    '            AppExcel.Selection.ShrinkToFit = False
    '    '            AppExcel.Selection.MergeCells = False
    '    '            With AppExcel.Selection.Font
    '    '                .Name = "Arial"
    '    '                .Size = 8
    '    '                .Strikethrough = False
    '    '                .Superscript = False
    '    '                .Subscript = False
    '    '                .OutlineFont = False
    '    '                .Shadow = False
    '    '                .Underline = xlUnderlineStyleNone
    '    '                .ColorIndex = 1
    '    '                .Bold = True
    '    '            End With
    '    '            If ApartadoActual = 1 Then
    '    '                AppExcel.Selection.Interior.ColorIndex = 36
    '    '            ElseIf ApartadoActual = 2 Then
    '    '                AppExcel.Selection.Interior.ColorIndex = 40
    '    '            Else
    '    '                AppExcel.Selection.Interior.ColorIndex = 35
    '    '            End If
    '    '            AppExcel.Selection.Interior.Pattern = xlSolid
    '    '            HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))
    '    '
    '    '            Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
    '    '            HojaExcel.Range(Cadena).Select
    '    '            AppExcel.Selection.NumberFormat = "0.0000"
    '    '            AppExcel.Selection.HorizontalAlignment = xlRight
    '    '            AppExcel.Selection.VerticalAlignment = xlBottom
    '    '            AppExcel.Selection.WrapText = False
    '    '            AppExcel.Selection.Orientation = 0
    '    '            AppExcel.Selection.AddIndent = False
    '    '            AppExcel.Selection.ShrinkToFit = False
    '    '            AppExcel.Selection.MergeCells = False
    '    '            With AppExcel.Selection.Font
    '    '                .Name = "Arial"
    '    '                .FontStyle = "Normal"
    '    '                .Size = 8
    '    '                .Strikethrough = False
    '    '                .Superscript = False
    '    '                .Subscript = False
    '    '                .OutlineFont = False
    '    '                .Shadow = False
    '    '                .Underline = xlUnderlineStyleNone
    '    '                .ColorIndex = xlAutomatic
    '    '                .Bold = True
    '    '            End With
    '    '            For j = 2 To CuantasColumnas + 1
    '    '                HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
    '    '            Next
    '    '        End If
    '    '        ApartadoActual = Rs!apartado
    '    '        NombreActual = Trim(Rs!descripcion)
    '    '    End If
    '    '
    '    ''       Nueva Línea
    '    '
    '    '    CuantasFilas = CuantasFilas + 1
    '    '    ReDim Preserve NombreFila(5, CuantasFilas)
    '    '    NombreFila(0, CuantasFilas) = Trim(Rs!codigo_grupo)
    '    '    If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
    '    '        NombreFila(1, CuantasFilas) = Space(10) + CStr(Rs!apartado) + "." + CStr(Rs!subapartado) + "." + CStr(Rs!linea_informe) + " " + Trim(Rs!descripcion)
    '    '    ElseIf Rs!apartado > 0 And Rs!subapartado > 0 Then
    '    '        NombreFila(1, CuantasFilas) = CStr(Rs!apartado) + "." + CStr(Rs!subapartado) + " " + Trim(Rs!descripcion)
    '    '        FilaSubapartado = CuantasFilas
    '    '        For j = 0 To 99: Formulas(0, j) = "": Next
    '    '    Else
    '    '        NombreFila(1, CuantasFilas) = CStr(Rs!apartado) + " " + Trim(Rs!descripcion)
    '    '        FilaSubapartado = -1
    '    '        For j = 0 To 99: Formulas(0, j) = "": Next
    '    '    End If
    '    '    NombreFila(2, CuantasFilas) = CStr(i)
    '    '    NombreFila(3, CuantasFilas) = CStr(Rs!apartado)
    '    '    NombreFila(4, CuantasFilas) = CStr(Rs!subapartado)
    '    '    NombreFila(5, CuantasFilas) = CStr(Rs!linea_informe)
    '    '    Cadena = "A" + CStr(CuantasFilas + 3)
    '    '    HojaExcel.Range(Cadena).Select
    '    '    AppExcel.Selection.NumberFormat = "@"
    '    '    AppExcel.Selection.HorizontalAlignment = xlLeft
    '    '    AppExcel.Selection.VerticalAlignment = xlCenter
    '    '    AppExcel.Selection.WrapText = False
    '    '    AppExcel.Selection.AddIndent = False
    '    '    AppExcel.Selection.ShrinkToFit = False
    '    '    AppExcel.Selection.MergeCells = False
    '    '    With AppExcel.Selection.Font
    '    '        .Name = "Arial"
    '    '        .Size = 8
    '    '        .Strikethrough = False
    '    '        .Superscript = False
    '    '        .Subscript = False
    '    '        .OutlineFont = False
    '    '        .Shadow = False
    '    '        .Underline = xlUnderlineStyleNone
    '    '        .ColorIndex = xlAutomatic
    '    '        .Bold = True
    '    '    End With
    '    '    If ApartadoActual = 1 Then
    '    '        AppExcel.Selection.Interior.ColorIndex = 36
    '    '    ElseIf ApartadoActual = 2 Then
    '    '        AppExcel.Selection.Interior.ColorIndex = 40
    '    '    Else
    '    '        AppExcel.Selection.Interior.ColorIndex = 35
    '    '    End If
    '    '    HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))
    '    '    If Rs!apartado > 0 And Rs!subapartado = 0 And Rs!linea_informe = 0 Then
    '    '        AppExcel.Selection.Interior.ColorIndex = 16
    '    '        AppExcel.Selection.Interior.Pattern = xlSolid
    '    '        AppExcel.Selection.Font.ColorIndex = 2
    '    '    Else
    '    '        If ApartadoActual = 1 Then
    '    '            AppExcel.Selection.Interior.ColorIndex = 36
    '    '        ElseIf ApartadoActual = 2 Then
    '    '            AppExcel.Selection.Interior.ColorIndex = 40
    '    '        Else
    '    '            AppExcel.Selection.Interior.ColorIndex = 35
    '    '        End If
    '    '    End If
    '    '    Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(CuantasColumnas + 1) + CStr(CuantasFilas + 3)
    '    '    HojaExcel.Range(Cadena).Select
    '    '    If Rs!apartado > 0 And Rs!subapartado = 0 And Rs!linea_informe = 0 Then
    '    '        AppExcel.Selection.Interior.ColorIndex = 16
    '    '        AppExcel.Selection.Interior.Pattern = xlSolid
    '    '        AppExcel.Selection.Font.ColorIndex = 2
    '    '    Else
    '    '        If ApartadoActual = 1 Then
    '    '            AppExcel.Selection.Interior.ColorIndex = 36
    '    '        ElseIf ApartadoActual = 2 Then
    '    '            AppExcel.Selection.Interior.ColorIndex = 40
    '    '        Else
    '    '            AppExcel.Selection.Interior.ColorIndex = 35
    '    '        End If
    '    '        AppExcel.Selection.NumberFormat = "0.0000"
    '    '        AppExcel.Selection.HorizontalAlignment = xlRight
    '    '        AppExcel.Selection.VerticalAlignment = xlBottom
    '    '        AppExcel.Selection.WrapText = False
    '    '        AppExcel.Selection.Orientation = 0
    '    '        AppExcel.Selection.AddIndent = False
    '    '        AppExcel.Selection.ShrinkToFit = False
    '    '        AppExcel.Selection.MergeCells = False
    '    '        With AppExcel.Selection.Font
    '    '            .Name = "Arial"
    '    '            .FontStyle = "Normal"
    '    '            .Size = 8
    '    '            .Strikethrough = False
    '    '            .Superscript = False
    '    '            .Subscript = False
    '    '            .OutlineFont = False
    '    '            .Shadow = False
    '    '            .Underline = xlUnderlineStyleNone
    '    '            .ColorIndex = xlAutomatic
    '    '        End With
    '    '        For j = 2 To CuantasColumnas + 1
    '    '            HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
    '    '        Next
    '    '    End If
    '    '    If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
    '    '        For j = 1 To CuantasColumnas
    '    '            Formulas(0, j) = Formulas(0, j) + "+R" + CStr(CuantasFilas + 3) + "C" + CStr(j + 1)
    '    '            Cadena = XColumna(CLng(j + 1)) + CStr(FilaSubapartado + 3)
    '    '            HojaExcel.Range(Cadena).Select
    '    '            AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(0, j), 2)
    '    '        Next
    '    '    End If
    '    'Next
    '    'Rs.Close
    '    'If ApartadoActual > -1 Then
    '    '    CuantasFilas = CuantasFilas + 1
    '    '    ReDim Preserve NombreFila(5, CuantasFilas)
    '    '    NombreFila(0, CuantasFilas) = CStr(ApartadoActual)
    '    '    NombreFila(1, CuantasFilas) = "TOTAL " + Trim(NombreActual)
    '    '    NombreFila(2, CuantasFilas) = "0"
    '    '    NombreFila(3, CuantasFilas) = CStr(ApartadoActual)
    '    '    NombreFila(4, CuantasFilas) = "0"
    '    '    NombreFila(5, CuantasFilas) = "0"
    '    '    FilasTotales(ApartadoActual) = CuantasFilas
    '    '    Cadena = "A" + CStr(CuantasFilas + 3)
    '    '    HojaExcel.Range(Cadena).Select
    '    '    AppExcel.Selection.NumberFormat = "@"
    '    '    AppExcel.Selection.HorizontalAlignment = xlCenter
    '    '    AppExcel.Selection.VerticalAlignment = xlCenter
    '    '    AppExcel.Selection.WrapText = False
    '    '    AppExcel.Selection.AddIndent = False
    '    '    AppExcel.Selection.ShrinkToFit = False
    '    '    AppExcel.Selection.MergeCells = False
    '    '    With AppExcel.Selection.Font
    '    '        .Name = "Arial"
    '    '        .Size = 8
    '    '        .Strikethrough = False
    '    '        .Superscript = False
    '    '        .Subscript = False
    '    '        .OutlineFont = False
    '    '        .Shadow = False
    '    '        .Underline = xlUnderlineStyleNone
    '    '        .ColorIndex = 1
    '    '        .Bold = True
    '    '    End With
    '    '    If ApartadoActual = 1 Then
    '    '        AppExcel.Selection.Interior.ColorIndex = 36
    '    '    ElseIf ApartadoActual = 2 Then
    '    '        AppExcel.Selection.Interior.ColorIndex = 40
    '    '    Else
    '    '        AppExcel.Selection.Interior.ColorIndex = 35
    '    '    End If
    '    '    AppExcel.Selection.Interior.Pattern = xlSolid
    '    '    HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))
    '    '
    '    '    Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
    '    '    HojaExcel.Range(Cadena).Select
    '    '    AppExcel.Selection.NumberFormat = "0.0000"
    '    '    AppExcel.Selection.HorizontalAlignment = xlRight
    '    '    AppExcel.Selection.VerticalAlignment = xlBottom
    '    '    AppExcel.Selection.WrapText = False
    '    '    AppExcel.Selection.Orientation = 0
    '    '    AppExcel.Selection.AddIndent = False
    '    '    AppExcel.Selection.ShrinkToFit = False
    '    '    AppExcel.Selection.MergeCells = False
    '    '    With AppExcel.Selection.Font
    '    '        .Name = "Arial"
    '    '        .FontStyle = "Normal"
    '    '        .Size = 8
    '    '        .Strikethrough = False
    '    '        .Superscript = False
    '    '        .Subscript = False
    '    '        .OutlineFont = False
    '    '        .Shadow = False
    '    '        .Underline = xlUnderlineStyleNone
    '    '        .ColorIndex = xlAutomatic
    '    '        .Bold = True
    '    '    End With
    '    '    For j = 2 To CuantasColumnas + 1
    '    '        HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
    '    '    Next
    '    'End If
    '    'CuantasFilas = CuantasFilas + 1
    '    'ReDim Preserve NombreFila(5, CuantasFilas)
    '    'NombreFila(0, CuantasFilas) = "TOTALES INFORME"
    '    'NombreFila(1, CuantasFilas) = "TOTALES INFORME"
    '    'NombreFila(2, CuantasFilas) = "-1"
    '    'NombreFila(3, CuantasFilas) = "0"
    '    'NombreFila(4, CuantasFilas) = "0"
    '    'NombreFila(5, CuantasFilas) = "0"
    '    'FilasTotales(0) = CuantasFilas
    '    'Cadena = "A" + CStr(CuantasFilas + 3)
    '    'HojaExcel.Range(Cadena).Select
    '    'AppExcel.Selection.NumberFormat = "@"
    '    'AppExcel.Selection.HorizontalAlignment = xlLeft
    '    'AppExcel.Selection.VerticalAlignment = xlCenter
    '    'AppExcel.Selection.WrapText = False
    '    'AppExcel.Selection.AddIndent = False
    '    'AppExcel.Selection.ShrinkToFit = False
    '    'AppExcel.Selection.MergeCells = False
    '    'With AppExcel.Selection.Font
    '    '    .Name = "Arial"
    '    '    .Size = 10
    '    '    .Strikethrough = False
    '    '    .Superscript = False
    '    '    .Subscript = False
    '    '    .OutlineFont = False
    '    '    .Shadow = False
    '    '    .Underline = xlUnderlineStyleNone
    '    '    .ColorIndex = 2
    '    '    .Bold = True
    '    'End With
    '    'Cadena = CStr(CuantasFilas + 3) + ":" + CStr(CuantasFilas + 3)
    '    'AppExcel.Selection.Interior.ColorIndex = 16
    '    'AppExcel.Selection.Interior.Pattern = xlSolid
    '    'HojaExcel.Cells(CuantasFilas + 3, 1).Value = RTrim(NombreFila(1, CuantasFilas))
    '    'HojaExcel.Range(Cadena).Select
    '    'AppExcel.Selection.RowHeight = 18.75
    '    'Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
    '    'HojaExcel.Range(Cadena).Select
    '    'AppExcel.Selection.NumberFormat = "0.0000"
    '    'AppExcel.Selection.HorizontalAlignment = xlRight
    '    'AppExcel.Selection.VerticalAlignment = xlBottom
    '    'AppExcel.Selection.WrapText = False
    '    'AppExcel.Selection.Orientation = 0
    '    'AppExcel.Selection.AddIndent = False
    '    'AppExcel.Selection.ShrinkToFit = False
    '    'AppExcel.Selection.MergeCells = False
    '    'With AppExcel.Selection.Font
    '    '    .Name = "Arial"
    '    '    .FontStyle = "Normal"
    '    '    .Size = 10
    '    '    .Strikethrough = False
    '    '    .Superscript = False
    '    '    .Subscript = False
    '    '    .OutlineFont = False
    '    '    .Shadow = False
    '    '    .Underline = xlUnderlineStyleNone
    '    '    .ColorIndex = xlAutomatic
    '    '    .Bold = True
    '    'End With
    '    'For j = 2 To CuantasColumnas + 1
    '    '    HojaExcel.Cells(CuantasFilas + 3, j).Value = "0"
    '    'Next
    '    ''
    '    '''Valores costes
    '    ''
    '    ''MsgBox "6"
    '    'SQL = "SELECT FECHA, GRUPO_COSTE, SUM(KILOS) AS KILOS, SUM(IMPORTE_TOTAL) AS IMPORTE_TOTAL FROM H_COSTES_FABRICACION WHERE H_COSTES_FABRICACION.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND H_COSTES_FABRICACION.PROCESO = " + CStr(proceso) + " AND H_COSTES_FABRICACION.FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND H_COSTES_FABRICACION.TIPO_COSTE = '3' group by FECHA,GRUPO_COSTE having sum(kilos)>0 ORDER BY FECHA,GRUPO_COSTE"
    '    'Rs.Open SQL, ObjetoGlobal.cn, adOpenDynamic, adLockReadOnly
    '    'For i = 1 To Rs.RecordCount
    '    '    Rs.AbsolutePosition = i
    '    '    Fila = QueFilaEs(Rs)
    '    ''    MsgBox "Fila: " + CStr(Fila) + " " + CStr(i)
    '    '    Columna = QueColumnaEs(Rs)
    '    ''    MsgBox "Columna: " + CStr(Columna) + " " + CStr(i)
    '    '    ImporteL = 0
    '    '    If Not IsNull(Rs!Kilos) And Not IsNull(Rs!importe_total) Then
    '    '        If Rs!Kilos > 0 And Rs!importe_total > 0 Then
    '    '            ImporteL = Round(Rs!importe_total / Rs!Kilos, 4)
    '    '        End If
    '    '    End If
    '    ''    MsgBox "Importe: " + CStr(ImporteL)
    '    '    If Round(ImporteL, 4) = 0 Then
    '    '        HojaExcel.Cells(Fila, Columna).Value = "0"
    '    '    Else
    '    '        HojaExcel.Cells(Fila, Columna).Value = Replace(Format(ImporteL, "##.####"), ",", ".")
    '    '    End If
    '    'Next
    '    ''
    '    '''Fórmulas de totales
    '    ''
    '    ''MsgBox "7"
    '    'For i = 0 To 10
    '    '    For j = 0 To 99
    '    '        Formulas(i, j) = ""
    '    '    Next
    '    'Next
    '    'For i = 1 To CuantasFilas
    '    '    If CInt(NombreFila(2, i)) > "0" And CInt(NombreFila(3, i)) > "0" And CInt(NombreFila(4, i)) > "0" And CInt(NombreFila(5, i)) = "0" Then
    '    '        QueAcumulado = CInt(NombreFila(3, i))
    '    '        For j = 1 To CuantasColumnas
    '    '            Formulas(QueAcumulado, j) = Formulas(QueAcumulado, j) + "+R" + CStr(i + 3) + "C" + CStr(j + 1)
    '    '        Next
    '    '    End If
    '    'Next
    '    'For i = 1 To 10
    '    '    If FilasTotales(i) > 0 Then
    '    '        For j = 1 To CuantasColumnas
    '    '            Cadena = XColumna(CLng(j + 1)) + CStr(FilasTotales(i) + 3)
    '    '            HojaExcel.Range(Cadena).Select
    '    '            AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(i, j), 2)
    '    '        Next
    '    '    End If
    '    'Next
    '    'For j = 1 To CuantasColumnas
    '    '    For i = 1 To 10
    '    '        If FilasTotales(i) > 0 Then
    '    '            Formulas(0, j) = Formulas(0, j) + "+R" + CStr(FilasTotales(i) + 3) + "C" + CStr(j + 1)
    '    '        End If
    '    '    Next
    '    'Next
    '    ''MsgBox "8"
    '    'For j = 1 To CuantasColumnas
    '    '    Cadena = XColumna(CLng(j + 1)) + CStr(FilasTotales(0) + 3)
    '    '    HojaExcel.Range(Cadena).Select
    '    '    AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(0, j), 2)
    '    'Next
    '    'With HojaExcel.PageSetup
    '    '    .PrintTitleRows = "$1:$3"
    '    '    .PrintTitleColumns = "$A:$A"
    '    '    .PrintArea = ""
    '    '    .LeftHeader = ""
    '    '    .CenterHeader = ""
    '    '    .RightHeader = ""
    '    '    .LeftFooter = ""
    '    '    .CenterFooter = ""
    '    '    .RightFooter = ""
    '    '    .LeftMargin = AppExcel.InchesToPoints(0)
    '    '    .RightMargin = AppExcel.InchesToPoints(0)
    '    '    .TopMargin = AppExcel.InchesToPoints(0)
    '    '    .BottomMargin = AppExcel.InchesToPoints(0)
    '    '    .HeaderMargin = AppExcel.InchesToPoints(0)
    '    '    .FooterMargin = AppExcel.InchesToPoints(0)
    '    '    .PrintHeadings = False
    '    '    .PrintGridlines = True
    '    '    .PrintComments = xlPrintNoComments
    '    '    .PrintQuality = 600
    '    '    .CenterHorizontally = False
    '    '    .CenterVertically = False
    '    '    .Orientation = xlPortrait
    '    '    .Draft = False
    '    '    .PaperSize = xlPaperA4
    '    '    .FirstPageNumber = xlAutomatic
    '    '    .Order = xlDownThenOver
    '    '    .BlackAndWhite = False
    '    '    .Zoom = 100
    '    'End With
    '    '
    '    ''MsgBox "9"
    '    'Cadena = "A3"
    '    'HojaExcel.Range(Cadena).Select
    '    'If CuantasColumnas = 0 Then
    '    '    PlantillaExcel.Saved = True
    '    'Else
    '    '    HojaExcel.SaveAs Trim(TxtDocumento.Text + "\" + Trim(Familia) + "_" + Format(Fecha, "yyyymmdd") + "__" + Format(Now, "hhmmss") + ".xls")
    '    'End If
    '    'PlantillaExcel.Close
    '    'Rs.Close
    '    ''MsgBox "10"
    '    'Set HojaExcel = Nothing
    '    'Set PlantillaExcel = Nothing
    '    'AppExcel.Quit
    '    'Set AppExcel = Nothing
    '    'MsgBox "Se ha grabado correctamente la hoja EXCEL.", vbInformation + vbOKOnly
    '    'End Sub

    Private Function XColumna(MsC As Long) As String
        Dim C1 As Integer, C2 As Integer
        Dim C As String
        Dim i As Long

        If MsC < 27 Then
            C = Chr(Asc("A") + MsC - 1)
        Else
            C1 = Fix(MsC / 26)
            If MsC = C1 * 26 Then C1 = C1 - 1
            C2 = MsC Mod 26
            If C2 = 0 Then C2 = 26
            C = Chr(Asc("A") + C1 - 1) + Chr(Asc("A") + C2 - 1)
        End If
        XColumna = C
    End Function

    '    Private Function QueFilaEs(Rs As Recordset)
    '        Dim Grupo As Long, i As Integer

    '        QueFilaEs = -1
    '        Grupo = Rs!grupo_coste
    '        For i = 1 To CuantasFilas
    '            If Grupo = CLng(NombreFila(0, i)) Then
    '                QueFilaEs = 3 + i
    '                Exit For
    '            End If
    '        Next
    '    End Function

    '    Private Function QueColumnaEs(Rs As Recordset)
    '        Dim Confeccion As String, i As Integer

    '        QueColumnaEs = -1
    '        'If OptDetalle(0).Value = True Then
    '        Confeccion = Trim(Rs!codigo_confeccion)
    '        For i = 1 To CuantasColumnas
    '            If Trim(Confeccion) = Trim(NombreColumna(0, i)) Then
    '                QueColumnaEs = 1 + i
    '                Exit For
    '            End If
    '        Next
    '        'Else
    '        '    Confeccion = Format(Rs!Fecha, "dd/MM/yyyy")
    '        '    For i = 1 To CuantasColumnas
    '        '        If Trim(Confeccion) = Trim(NombreColumna(0, i)) Then
    '        '            QueColumnaEs = 1 + i
    '        '            Exit For
    '        '        End If
    '        '    Next
    '        'End If
    '    End Function

End Class