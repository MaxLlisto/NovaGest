Imports System.ComponentModel
Imports Interop.Microsoft.Office.Interop.Excel
Imports Interop.Office
Imports stdole



Public Class FrmGrabarExcelCostes
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Dim RutaExcel As String = "\\Srvv02\Grupos\Comercial\Costes"
    Dim RutaPlantillaExcel As String = "\\Srvv02\Pt\Forfait.xlt"

    Dim Familia As String
    Dim VariedadGeneral As String
    Dim Dias() As Date
    '    Dim VariedadesDia() As String
    Dim CuantosDias As Integer

    Dim CuantasColumnas As Long
    Dim NombreColumna(3, 0) As String

    Dim CuantasFilas As Long
    Dim NombreFila(5, 0) As String


    Dim MiExcel As LibreriaGeneral.EXCEL_VB
    Dim MiHojaExcel As Microsoft.Office.Interop.Excel.Workbook

    Dim ContadorCRT As Long

    Private Sub FrmGrabarExcelCostes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Clave As String, Fecha As Date

        Clave = InputBox("Introduzca clave de acceso:")
        If LCase(Trim(Clave)) <> "vercostes" Then
            Me.Close()
        End If
        Fecha = DateAdd("d", -1, Now)
        DTDesdeFecha.Value = Fecha
        DTHastaFecha.Value = Fecha

        LblContador.Text = ""
        LblEXCEL.Visible = False

    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        Dim i As Integer, Cuantos As Integer

        '        Dim FlagHayCampos As Boolean

        '        PicCartel.Visible = True
        '        LblEnproceso.Caption = ""
        '        LblEnproceso.Visible = True
        '        LblMensaje.Caption = ""
        '        LblMensaje.Visible = True
        '        CuantasConfecciones = -1
        '        ReDim Confecciones(1, 0)
        If RB1.Checked = True Then
            Familia = "NAR"
            VariedadGeneral = "022"
        ElseIf RB2.Checked = True Then
            Familia = "CLE"
            VariedadGeneral = "012"
        ElseIf RB3.Checked = True Then
            Familia = "HIB"
            VariedadGeneral = "017"
        ElseIf RB4.Checked = True Then
            Familia = "SAT"
            VariedadGeneral = "014"
        ElseIf RB6.Checked = True Then
            Familia = "ROJ"
            VariedadGeneral = "021"
        Else
            Familia = "NAB"
            VariedadGeneral = "024"
        End If

        CuantosDias = -1
        ReDim Dias(0)
        DTDesdeFecha.Value = CDate(Format(DTDesdeFecha.Value, "dd/MM/yyyy"))
        DTHastaFecha.Value = CDate(Format(DTHastaFecha.Value, "dd/MM/yyyy"))
        If DTHastaFecha.Value >= DTDesdeFecha.Value Then
            CuantosDias = DateDiff("d", CDate(DTDesdeFecha.Value), CDate(DTHastaFecha.Value)) + 1
            For i = 1 To CuantosDias
                ReDim Preserve Dias(CuantosDias)
                Dias(i) = DateAdd("d", i - 1, CDate(DTDesdeFecha.Value))
            Next
        End If

        If CuantosDias > 0 Then
            GrabarExcel()
        End If


        '        PicCartel.Visible = False
        '        LblEnproceso.Caption = ""
        '        LblEnproceso.Visible = False
        '        LblMensaje.Caption = ""
        '        LblMensaje.Visible = False

    End Sub


    '    Dim CINF As Object
    '    Dim XProceso As String
    '    Dim XDocumento As String
    '    Dim XFormato As String
    '    Dim XDiccionario As Dictionary

    '    Dim Proceso As Long
    '    Dim ContadorGeneral As Long
    '    Dim Dias() As Date
    '    Dim VariedadesDia() As String
    '    Dim CuantosDias As Integer
    '    Dim Confecciones() As String
    '    Dim CuantasConfecciones As Integer

    '    Dim PesoIndustriaClementinaPequeno As Long
    '    Dim PesoIndustriaNaranjaPequeno As Long
    '    Dim PesoIndustriaClementinaGrande As Long
    '    Dim PesoIndustriaNaranjaGrande As Long


    '    Private Sub CmdBuscarPlantilla_Click()
    '        CommonDialog1.FileName = TxtDocumento
    '        CommonDialog1.Filter = "*.xls"
    '        CommonDialog1.DialogTitle = "Grabar documento EXCEL"
    '        CommonDialog1.InitDir = Trim("\\Srvv02\Grupos\Comercial\Costes")
    '        CommonDialog1.ShowSave
    '        If Not CommonDialog1.CancelError Then TxtDocumento.Text = Trim(CommonDialog1.FileName)
    '    End Sub
    '    Private Sub CmdSalir_Click()
    '        Unload Me
    'End Sub


    Private Sub GrabarExcel()
        Dim i As Integer, j As Integer, k As Integer, jj As Integer, Fecha As Date
        Dim Rs As New cnRecordset.CnRecordset, SQL As String
        Dim Cadena As String

        Dim Fila As Long, Columna As Long, ApartadoActual As Long, NombreActual As String
        Dim FilasTotales(10) As Long, Formulas(10, 99) As String, QueAcumulado As Integer
        Dim FilaSubapartado As Long, UltimoGrupo As String
        '        Dim AppExcel As excel.Application
        '        Dim PlantillaExcel As excel.Workbook
        '        Dim HojaExcel As excel.Worksheet

        LblEXCEL.Visible = True 'OJO
        LblEXCEL.Refresh()

        LblContador.Visible = True
        LblContador.Text = ""
        LblContador.Refresh()
        ContadorCRT = 0

        Dim MiRango As Microsoft.Office.Interop.Excel.Range
        Dim MiPestaña As Microsoft.Office.Interop.Excel.Worksheet

        MiExcel = New LibreriaGeneral.EXCEL_VB(False)
        MiExcel.Visible = False
        'MiHojaExcel = MiExcel.ObjetoHoja() ' En principio, no hace falta manipular directamente el objeto

        For jj = 1 To CuantosDias
            ContadorCRT = ContadorCRT + 1
            LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
            LblContador.Refresh()

            MiExcel.AbrirHojaExcel(RutaPlantillaExcel)

            'Cabecera del informe

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

            If RB1.Checked = True Then
                MiExcel.EscribeCelda(1, 2, "NARANJA " + Format(Fecha, "dd/MM/yyyy") + ".")
            ElseIf RB2.Checked = True Then
                MiExcel.EscribeCelda(1, 2, "CLEMENTINA " + Format(Fecha, "dd/MM/yyyy"))
            ElseIf RB3.Checked = True Then
                MiExcel.EscribeCelda(1, 2, "HIBRIDOS " + Format(Fecha, "dd/MM/yyyy"))
            ElseIf RB4.Checked = True Then
                MiExcel.EscribeCelda(1, 2, "SATSUMA " + Format(Fecha, "dd/MM/yyyy"))
            ElseIf RB6.Checked = True Then
                MiExcel.EscribeCelda(1, 2, "ROJA " + Format(Fecha, "dd/MM/yyyy"))
            Else
                MiExcel.EscribeCelda(1, 2, "NAR.BLANCA " + Format(Fecha, "dd/MM/yyyy"))
            End If

            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i

                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                CuantasColumnas = CuantasColumnas + 1
                ReDim Preserve NombreColumna(3, CuantasColumnas)
                NombreColumna(0, CuantasColumnas) = Trim(Rs!codigo_confeccion)
                NombreColumna(1, CuantasColumnas) = Trim(Rs!descripcion) + "(" + Trim(Rs!grupo_confeccion) + ")"
                NombreColumna(2, CuantasColumnas) = CStr(Rs!Palets)
                NombreColumna(3, CuantasColumnas) = CStr(Rs!Kilos)
            Next

            For i = 1 To CuantasColumnas
                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                Cadena = XColumna(1 + i) + "3"
                MiRango = MiExcel.ObjetoRango(Cadena)

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
                MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                MiRango.Font.Bold = True

                MiExcel.EscribeCelda(3, i + 1, Trim(NombreColumna(1, i)) + Chr(10) + "(Palets:" + Trim(NombreColumna(2, i)) + Chr(10) + "Kg:" + Trim(NombreColumna(3, i)) + Chr(10) + "Cod:" + Trim(NombreColumna(0, i)) + ")")
                Cadena = XColumna(1 + i) + ":" + XColumna(1 + i)
                MiRango = MiExcel.ObjetoRango(Cadena)
                MiRango.ColumnWidth = 9
            Next

            Rs.Close()
            For i = 0 To 10
                For j = 0 To 99
                    Formulas(i, j) = ""
                Next
            Next

            'Cuerpo del informe

            CuantasFilas = 0
            ReDim NombreFila(5, 0)
            ApartadoActual = -1
            FilaSubapartado = -1

            SQL = "SELECT * FROM GRUPOS_COSTE_INFORME JOIN GRUPOS_COSTE ON GRUPOS_COSTE.EMPRESA = GRUPOS_COSTE_INFORME.EMPRESA AND GRUPOS_COSTE.CODIGO_GRUPO = GRUPOS_COSTE_INFORME.CODIGO_GRUPO ORDER BY GRUPOS_COSTE_INFORME.EMPRESA,GRUPOS_COSTE_INFORME.APARTADO,GRUPOS_COSTE_INFORME.SUBAPARTADO,GRUPOS_COSTE_INFORME.LINEA_INFORME"
            If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla h_costes_fabricacion")
                Exit Sub
            End If

            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i

                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                '       Cambio de grupo

                If ApartadoActual <> Rs!apartado Then
                    If ApartadoActual > -1 Then
                        CuantasFilas = CuantasFilas + 1
                        ReDim Preserve NombreFila(5, CuantasFilas)
                        NombreFila(0, CuantasFilas) = CStr(ApartadoActual)
                        NombreFila(1, CuantasFilas) = "TOTAL " + Trim(NombreActual)
                        NombreFila(2, CuantasFilas) = "0"
                        NombreFila(3, CuantasFilas) = CStr(ApartadoActual)
                        NombreFila(4, CuantasFilas) = "0"
                        NombreFila(5, CuantasFilas) = "0"
                        FilasTotales(ApartadoActual) = CuantasFilas

                        Cadena = "A" + CStr(CuantasFilas + 3)
                        MiRango = MiExcel.ObjetoRango(Cadena)
                        MiRango.NumberFormat = "@"
                        MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                        MiRango.WrapText = False
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
                        MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                        MiRango.Font.ColorIndex = 1
                        MiRango.Font.Bold = True
                        If ApartadoActual = 1 Then
                            MiRango.Interior.ColorIndex = 36
                        ElseIf ApartadoActual = 2 Then
                            MiRango.Interior.ColorIndex = 40
                        Else
                            MiRango.Interior.ColorIndex = 35
                        End If
                        MiRango.Interior.Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
                        MiExcel.EscribeCelda(CuantasFilas + 3, 1, RTrim(NombreFila(1, CuantasFilas)))

                        Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
                        MiRango = MiExcel.ObjetoRango(Cadena)
                        MiRango.NumberFormat = "0.0000"
                        MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                        MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
                        MiRango.WrapText = False
                        MiRango.Orientation = 0
                        MiRango.AddIndent = False
                        MiRango.ShrinkToFit = False
                        MiRango.MergeCells = False
                        MiRango.Font.Name = "Arial"
                        MiRango.Font.FontStyle = "Normal"
                        MiRango.Font.Size = 8
                        MiRango.Font.Strikethrough = False
                        MiRango.Font.Superscript = False
                        MiRango.Font.Subscript = False
                        MiRango.Font.OutlineFont = False
                        MiRango.Font.Shadow = False
                        MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                        MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                        MiRango.Font.Bold = True


                        For j = 2 To CuantasColumnas + 1
                            MiExcel.EscribeCelda(CuantasFilas + 3, j, "0")
                        Next
                    End If
                    ApartadoActual = Rs!apartado
                    NombreActual = Trim(Rs!descripcion)
                End If

                '       Nueva Línea

                CuantasFilas = CuantasFilas + 1
                ReDim Preserve NombreFila(5, CuantasFilas)
                NombreFila(0, CuantasFilas) = Trim(Rs!codigo_grupo)
                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    NombreFila(1, CuantasFilas) = Space(10) + CStr(Rs!apartado) + "." + CStr(Rs!subapartado) + "." + CStr(Rs!linea_informe) + " " + Trim(Rs!descripcion)
                ElseIf Rs!apartado > 0 And Rs!subapartado > 0 Then
                    NombreFila(1, CuantasFilas) = CStr(Rs!apartado) + "." + CStr(Rs!subapartado) + " " + Trim(Rs!descripcion)
                    FilaSubapartado = CuantasFilas
                    For j = 0 To 99 : Formulas(0, j) = "" : Next
                Else
                    NombreFila(1, CuantasFilas) = CStr(Rs!apartado) + " " + Trim(Rs!descripcion)
                    FilaSubapartado = -1
                    For j = 0 To 99 : Formulas(0, j) = "" : Next
                End If
                NombreFila(2, CuantasFilas) = CStr(i)
                NombreFila(3, CuantasFilas) = CStr(Rs!apartado)
                NombreFila(4, CuantasFilas) = CStr(Rs!subapartado)
                NombreFila(5, CuantasFilas) = CStr(Rs!linea_informe)

                Cadena = "A" + CStr(CuantasFilas + 3)
                MiRango = MiExcel.ObjetoRango(Cadena)
                MiRango.NumberFormat = "@"
                MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                MiRango.WrapText = False
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
                MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                MiRango.Font.Bold = True
                If ApartadoActual = 1 Then
                    MiRango.Interior.ColorIndex = 36
                ElseIf ApartadoActual = 2 Then
                    MiRango.Interior.ColorIndex = 40
                Else
                    MiRango.Interior.ColorIndex = 35
                End If

                MiExcel.EscribeCelda(CuantasFilas + 3, 1, RTrim(NombreFila(1, CuantasFilas)))
                If Rs!apartado > 0 And Rs!subapartado = 0 And Rs!linea_informe = 0 Then
                    MiRango.Interior.ColorIndex = 16
                    MiRango.Interior.Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
                    MiRango.Font.ColorIndex = 2
                Else
                    If ApartadoActual = 1 Then
                        MiRango.Interior.ColorIndex = 36
                    ElseIf ApartadoActual = 2 Then
                        MiRango.Interior.ColorIndex = 40
                    Else
                        MiRango.Interior.ColorIndex = 35
                    End If
                End If

                Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(CuantasColumnas + 1) + CStr(CuantasFilas + 3)
                MiRango = MiExcel.ObjetoRango(Cadena)
                If Rs!apartado > 0 And Rs!subapartado = 0 And Rs!linea_informe = 0 Then
                    MiRango.Interior.ColorIndex = 16
                    MiRango.Interior.Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
                    MiRango.Font.ColorIndex = 2
                Else
                    If ApartadoActual = 1 Then
                        MiRango.Interior.ColorIndex = 36
                    ElseIf ApartadoActual = 2 Then
                        MiRango.Interior.ColorIndex = 40
                    Else
                        MiRango.Interior.ColorIndex = 35
                    End If
                    MiRango.NumberFormat = "0.0000"
                    MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                    MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
                    MiRango.WrapText = False
                    MiRango.Orientation = 0
                    MiRango.AddIndent = False
                    MiRango.ShrinkToFit = False
                    MiRango.MergeCells = False
                    MiRango.Font.Name = "Arial"
                    MiRango.Font.FontStyle = "Normal"
                    MiRango.Font.Size = 8
                    MiRango.Font.Strikethrough = False
                    MiRango.Font.Superscript = False
                    MiRango.Font.Subscript = False
                    MiRango.Font.OutlineFont = False
                    MiRango.Font.Shadow = False
                    MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                    MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic

                    For j = 2 To CuantasColumnas + 1
                        MiExcel.EscribeCelda(CuantasFilas + 3, j, "0")
                    Next
                End If

                If Rs!apartado > 0 And Rs!subapartado > 0 And Rs!linea_informe > 0 Then
                    For j = 1 To CuantasColumnas
                        Formulas(0, j) = Formulas(0, j) + "+R" + CStr(CuantasFilas + 3) + "C" + CStr(j + 1)
                        Cadena = XColumna(CLng(j + 1)) + CStr(FilaSubapartado + 3)
                        MiExcel.EscribeCelda(Cadena, "=" + Mid(Formulas(0, j), 2))
                    Next
                End If
            Next
            Rs.Close()

            If ApartadoActual > -1 Then
                CuantasFilas = CuantasFilas + 1
                ReDim Preserve NombreFila(5, CuantasFilas)
                NombreFila(0, CuantasFilas) = CStr(ApartadoActual)
                NombreFila(1, CuantasFilas) = "TOTAL " + Trim(NombreActual)
                NombreFila(2, CuantasFilas) = "0"
                NombreFila(3, CuantasFilas) = CStr(ApartadoActual)
                NombreFila(4, CuantasFilas) = "0"
                NombreFila(5, CuantasFilas) = "0"
                FilasTotales(ApartadoActual) = CuantasFilas

                Cadena = "A" + CStr(CuantasFilas + 3)
                MiRango = MiExcel.ObjetoRango(Cadena)
                MiRango.NumberFormat = "@"
                MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                MiRango.WrapText = False
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
                MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                MiRango.Font.ColorIndex = 1
                MiRango.Font.Bold = True
                If ApartadoActual = 1 Then
                    MiRango.Interior.ColorIndex = 36
                ElseIf ApartadoActual = 2 Then
                    MiRango.Interior.ColorIndex = 40
                Else
                    MiRango.Interior.ColorIndex = 35
                End If
                MiRango.Interior.Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid

                MiExcel.EscribeCelda(CuantasFilas + 3, 1, RTrim(NombreFila(1, CuantasFilas)))

                Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
                MiRango = MiExcel.ObjetoRango(Cadena)
                MiRango.NumberFormat = "0.0000"
                MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
                MiRango.WrapText = False
                MiRango.Orientation = 0
                MiRango.AddIndent = False
                MiRango.ShrinkToFit = False
                MiRango.MergeCells = False

                MiRango.Font.Name = "Arial"
                MiRango.Font.FontStyle = "Normal"
                MiRango.Font.Size = 8
                MiRango.Font.Strikethrough = False
                MiRango.Font.Superscript = False
                MiRango.Font.Subscript = False
                MiRango.Font.OutlineFont = False
                MiRango.Font.Shadow = False
                MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
                MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
                MiRango.Font.Bold = True

                For j = 2 To CuantasColumnas + 1
                    MiExcel.EscribeCelda(CuantasFilas + 3, j, "0")
                Next
            End If

            CuantasFilas = CuantasFilas + 1
            ReDim Preserve NombreFila(5, CuantasFilas)
            NombreFila(0, CuantasFilas) = "TOTALES INFORME"
            NombreFila(1, CuantasFilas) = "TOTALES INFORME"
            NombreFila(2, CuantasFilas) = "-1"
            NombreFila(3, CuantasFilas) = "0"
            NombreFila(4, CuantasFilas) = "0"
            NombreFila(5, CuantasFilas) = "0"
            FilasTotales(0) = CuantasFilas

            Cadena = "A" + CStr(CuantasFilas + 3)
            MiRango = MiExcel.ObjetoRango(Cadena)
            MiRango.NumberFormat = "@"
            MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
            MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
            MiRango.WrapText = False
            MiRango.AddIndent = False
            MiRango.ShrinkToFit = False
            MiRango.MergeCells = False
            MiRango.Font.Name = "Arial"
            MiRango.Font.Size = 10
            MiRango.Font.Strikethrough = False
            MiRango.Font.Superscript = False
            MiRango.Font.Subscript = False
            MiRango.Font.OutlineFont = False
            MiRango.Font.Shadow = False
            MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
            MiRango.Font.ColorIndex = 2
            MiRango.Font.Bold = True
            MiRango.Interior.ColorIndex = 16
            MiRango.Interior.Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
            MiExcel.EscribeCelda(CuantasFilas + 3, 1, RTrim(NombreFila(1, CuantasFilas)))

            Cadena = CStr(CuantasFilas + 3) + ":" + CStr(CuantasFilas + 3)
            MiRango = MiExcel.ObjetoRango(Cadena)
            MiRango.RowHeight = 18.75

            Cadena = "B" + CStr(CuantasFilas + 3) + ":" + XColumna(1 + CuantasColumnas) + CStr(CuantasFilas + 3)
            MiRango = MiExcel.ObjetoRango(Cadena)
            MiRango.NumberFormat = "0.0000"
            MiRango.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
            MiRango.VerticalAlignment = Microsoft.Office.Interop.Excel.Constants.xlBottom
            MiRango.WrapText = False
            MiRango.Orientation = 0
            MiRango.AddIndent = False
            MiRango.ShrinkToFit = False
            MiRango.MergeCells = False

            MiRango.Font.Name = "Arial"
            MiRango.Font.FontStyle = "Normal"
            MiRango.Font.Size = 10
            MiRango.Font.Strikethrough = False
            MiRango.Font.Superscript = False
            MiRango.Font.Subscript = False
            MiRango.Font.OutlineFont = False
            MiRango.Font.Shadow = False
            MiRango.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone
            MiRango.Font.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
            MiRango.Font.Bold = True

            For j = 2 To CuantasColumnas + 1
                MiExcel.EscribeCelda(CuantasFilas + 3, j, "0")
            Next

            'Valores costes

            SQL = "SELECT H_COSTES_FABRICACION.* FROM H_COSTES_FABRICACION WHERE H_COSTES_FABRICACION.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND H_COSTES_FABRICACION.FAMILIA_VARIEDAD = '" + Trim(Familia) + "' AND H_COSTES_FABRICACION.TIPO_COSTE = '3' AND H_COSTES_FABRICACION.FECHA = '" + Format(Fecha, "dd/MM/yyyy") + "' ORDER BY H_COSTES_FABRICACION.EMPRESA,H_COSTES_FABRICACION.CONTADOR"
            If Rs.Open(SQL, ObjetoGlobal.cn) = False Then
                MsgBox("Error al abrir la tabla h_costes_fabricacion")
                Exit Sub
            End If
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i

                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                Fila = QueFilaEs(Rs!grupo_coste)  'Antes (Rs)
                Columna = QueColumnaEs(Rs!codigo_confeccion)  'Antes (Rs)
                If Math.Round(Rs!centimos_kg, 4) = 0 Then
                    MiExcel.EscribeCelda(Fila, Columna, "0")
                Else
                    MiExcel.EscribeCelda(Fila, Columna, Replace(Format(Rs!centimos_kg, "##.####"), ",", "."))
                End If
            Next

            'Fórmulas de totales
            For i = 0 To 10
                For j = 0 To 99
                    Formulas(i, j) = ""
                Next
            Next

            For i = 1 To CuantasFilas
                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                If CInt(NombreFila(2, i)) > "0" And CInt(NombreFila(3, i)) > "0" And CInt(NombreFila(4, i)) > "0" And CInt(NombreFila(5, i)) = "0" Then
                    QueAcumulado = CInt(NombreFila(3, i))
                    For j = 1 To CuantasColumnas
                        Formulas(QueAcumulado, j) = Formulas(QueAcumulado, j) + "+R" + CStr(i + 3) + "C" + CStr(j + 1)
                    Next
                End If
            Next

            For i = 1 To 10
                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                If FilasTotales(i) > 0 Then
                    For j = 1 To CuantasColumnas
                        Cadena = XColumna(CLng(j + 1)) + CStr(FilasTotales(i) + 3)
                        MiExcel.EscribeCelda(Cadena, "=" + Mid(Formulas(i, j), 2))
                    Next
                End If
            Next

            For j = 1 To CuantasColumnas
                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                For i = 1 To 10
                    If FilasTotales(i) > 0 Then
                        Formulas(0, j) = Formulas(0, j) + "+R" + CStr(FilasTotales(i) + 3) + "C" + CStr(j + 1)
                    End If
                Next
            Next

            For j = 1 To CuantasColumnas
                ContadorCRT = ContadorCRT + 1
                LblContador.Text = Format(Dias(jj), "ddMMyyyy") + " " + CStr(ContadorCRT)
                LblContador.Refresh()

                Cadena = XColumna(CLng(j + 1)) + CStr(FilasTotales(0) + 3)
                MiExcel.EscribeCelda(Cadena, "=" + Mid(Formulas(0, j), 2))
                ' Antes               AppExcel.ActiveCell.FormulaR1C1 = "=" + Mid(Formulas(0, j), 2)
            Next

            MiPestaña = MiExcel.ObjetoPestaña()
            MiPestaña.PageSetup.PrintTitleRows = "$1:$3"
            MiPestaña.PageSetup.PrintTitleColumns = "$A:$A"
            MiPestaña.PageSetup.PrintArea = ""
            MiPestaña.PageSetup.LeftHeader = ""
            MiPestaña.PageSetup.CenterHeader = ""
            MiPestaña.PageSetup.RightHeader = ""
            MiPestaña.PageSetup.LeftFooter = ""
            MiPestaña.PageSetup.CenterFooter = ""
            MiPestaña.PageSetup.RightFooter = ""
            MiPestaña.PageSetup.LeftMargin = MiExcel.InchesToPoints(0)
            MiPestaña.PageSetup.RightMargin = MiExcel.InchesToPoints(0)
            MiPestaña.PageSetup.TopMargin = MiExcel.InchesToPoints(0)
            MiPestaña.PageSetup.BottomMargin = MiExcel.InchesToPoints(0)
            MiPestaña.PageSetup.HeaderMargin = MiExcel.InchesToPoints(0)
            MiPestaña.PageSetup.FooterMargin = MiExcel.InchesToPoints(0)
            MiPestaña.PageSetup.PrintHeadings = False
            MiPestaña.PageSetup.PrintGridlines = True
            MiPestaña.PageSetup.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments
            MiPestaña.PageSetup.PrintQuality = 600
            MiPestaña.PageSetup.CenterHorizontally = False
            MiPestaña.PageSetup.CenterVertically = False
            MiPestaña.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait
            MiPestaña.PageSetup.Draft = False
            MiPestaña.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4
            MiPestaña.PageSetup.FirstPageNumber = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
            MiPestaña.PageSetup.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver
            MiPestaña.PageSetup.BlackAndWhite = False
            MiPestaña.PageSetup.Zoom = 100

            Cadena = "A3"
            MiRango = MiExcel.ObjetoRango(Cadena)
            MiRango.Select()

            MiExcel.GuardarHoja(Trim(Trim(RutaExcel) + "\" + Trim(Familia) + "_" + Format(Fecha, "yyyyMMdd") + "__" + Format(Now, "hhMMss") + ".xls"))
            Rs.Close()
        Next

        MiExcel.Close()
        LblContador.Text = ""
        LblEXCEL.Visible = False

        MsgBox("Se ha grabado correctamente la hoja EXCEL")
    End Sub

    Private Function XColumna(MsC As Long) As String
        Dim C1 As Integer, C2 As Integer
        Dim C As String

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

    Private Function QueFilaEs(GG As Long) 'Antes el parámetro era un recordset
        Dim Grupo As Long, i As Integer

        QueFilaEs = -1
        Grupo = GG ' Antes Rs!grupo_coste
        For i = 1 To CuantasFilas
            If Grupo = CLng(NombreFila(0, i)) Then
                QueFilaEs = 3 + i
                Exit For
            End If
        Next
    End Function

    Private Function QueColumnaEs(CC As String) 'Antes el parámetro era un recordset
        Dim Confeccion As String, i As Integer

        QueColumnaEs = -1
        Confeccion = CC    'Antes Trim(Rs!codigo_confeccion)
        For i = 1 To CuantasColumnas
            If Trim(Confeccion) = Trim(NombreColumna(0, i)) Then
                QueColumnaEs = 1 + i
                Exit For
            End If
        Next
    End Function

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property


End Class