Imports System.ComponentModel
Imports Word.WdUnits
Imports System.Drawing.Printing

Public Class FrmEdicionContrato
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Private Const MIS_DOCUMENTOS_USUARIO = 5
    Private Const MAX_PATH = 260
    Public FormularioGrid As Boolean
    Public ImpresoraAct As String
    Private bEsMiWord As Boolean
    Public MiWord As Microsoft.Office.Interop.Word.Application
    Dim sPlantillaF As String
    Dim sPlantillaD As String
    Dim sPlantillaS As String
    Dim DtpFechaEdicion As Date
    Dim Copias As Integer
    Dim DatosExternos As Boolean = False

    Public Function DelSocio(ByVal CodSocio As Integer, ByVal nombreSocio As String)
        TxtCodigoSocio.Text = CStr(CodSocio)
        lblNombreSocio.Text = nombreSocio
        DatosExternos = True
    End Function

    Private Enum WdUnits
        wdCharacter = 1  '&H1
        wdWord = 2  '&H2
        wdSentence = 3  '&H3
        wdParagraph = 4  '&H4
        wdLine = 5  '&H5
        wdStory = 6  '&H6
        wdScreen = 7  '&H7
        wdSection = 8  '&H8
        wdColumn = 9  '&H9
        wdRow = 10  '&HA
        wdWindow = 11  '&HB
        wdCell = 12  '&HC
        wdCharacterFormatting = 13  '&HD
        wdParagraphFormatting = 14  '&HE
        wdTable = 15  '&HF
        wdItem = 16  '&H10
    End Enum

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If Not ComprobarPlantilla(TxtPlantilla.Text) Then
            MsgBox("No se encuentra la plantilla seleccionada.")
            Return
        End If

        If Not DatosValidos() Then
            Return
        End If
        ImprimeFicha()

    End Sub
    Public Sub ImprimeFicha()
        If Not ComprobarPlantilla(TxtPlantilla.Text) Then
            MsgBox("No se encuentra la plantilla del contrato seleccionada.")
            Return
        End If

        If Not ComprobarPlantilla(TxtPlantillaficha.Text) Then
            MsgBox("No se encuentra la plantilla de la ficha de campos seleccionada.")
            Return
        End If

        If Not DatosValidos() Then
            Return
        End If

        ImprimirEnWord(TxtPlantilla.Text)
        For i = 1 To CInt(txtCopias.Text)
            ImprimeFichas(TxtPlantillaficha.Text)
        Next
        CmdSalir.Visible = True

    End Sub

    Private Function DatosValidos() As Boolean
        If Trim(TxtCodigoSocio.Text) = "" Then
            MsgBox("El dato Código Socio es requerido.")
            TxtCodigoSocio.Focus()
            Return False
        Else
            Return True

        End If
    End Function

    Private Sub CmdBuscarPlantilla_Click()
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        Dim filePath As String

        OpenFileDialog.InitialDirectory = "c:\"
        OpenFileDialog.Filter = "dot files (*.dot)|*.dot|dotx files (*.dotx)|All files (*.*)|*.*|"
        OpenFileDialog.FilterIndex = 2
        OpenFileDialog.RestoreDirectory = True
        OpenFileDialog.Title = "Selección de plantilla contrato"

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            filePath = OpenFileDialog.FileName
            TxtPlantilla.Text = filePath
            'fileStream = OpenFileDialog.OpenFile()
        End If

    End Sub

    Private Function ComprobarPlantilla(Plantilla As String) As Boolean

        If Trim(Plantilla) = "" Then
            ComprobarPlantilla = False
            Return False
        ElseIf Trim(Dir(Plantilla)) = "" Then
            ComprobarPlantilla = False
            Return False
        Else
            ComprobarPlantilla = True
            Return True
        End If

    End Function
    Private Function ExisteSocio(sSocio, NombreSocio) As Boolean
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        ExisteSocio = False
        If IsNumeric(sSocio) Then
            rs.Open("SELECT APELLIDOS_SOCIO, NOMBRE_SOCIO FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " & sSocio, ObjetoGlobal.cn)
            If rs.RecordCount > 0 Then
                NombreSocio = IIf(Trim(rs!APELLIDOS_SOCIO) = "", (rs!NOMBRE_SOCIO), Trim(rs!APELLIDOS_SOCIO) & ", " & Trim(rs!NOMBRE_SOCIO))
                ExisteSocio = True
            Else
                ExisteSocio = False
            End If
        End If
    End Function

    Private Sub CmdBuscaFicha_Click(sender As Object, e As EventArgs) Handles CmdBuscaFicha.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        Dim filePath As String

        OpenFileDialog.InitialDirectory = "c:\"
        OpenFileDialog.Filter = "dot files (*.dot)|*.dot|dotx files (*.dotx)|All files (*.*)|*.*|"
        OpenFileDialog.FilterIndex = 2
        OpenFileDialog.RestoreDirectory = True
        OpenFileDialog.Title = "Selección de plantilla ficha campos"

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            filePath = OpenFileDialog.FileName
            TxtPlantillaficha.Text = filePath
        End If

    End Sub

    Private Sub ImprimirEnWord(Plantilla As String)
        Dim RsCoop As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartidas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTerminos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsVariedades As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionTermino As String
        Dim i As Integer
        Dim fecha As Date = CDate(TxtFechaEdicion.Text)
        Dim Dia As Integer = fecha.Day

        Dim Fila As Integer

        frCartel.Visible = False
        frCartel.Refresh()

        PrbProgreso.Minimum = 0
        PrbProgreso.Maximum = 10
        PrbProgreso.Text = 1
        PrbProgreso.Refresh()

        Try

            '  On Error GoTo ERROR:
            If MiWord Is Nothing Or Not bEsMiWord Then
                MiWord = New Microsoft.Office.Interop.Word.Application
                MiWord.Visible = False
                bEsMiWord = True
            End If

            MiWord.Documents.Add(Plantilla)

            RsCoop.Open("SELECT * FROM DATOS_EMPRESA WHERE EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)

            If RsCoop.RecordCount <= 0 Then Exit Sub

            RsSocios.Open("SELECT * FROM socios_coop WHERE CODIGO_SOCIO = " & Trim(TxtCodigoSocio.Text), ObjetoGlobal.cn)

            If RsSocios.RecordCount <= 0 Then Exit Sub

            MiWord.ActiveDocument.FormFields("nombre_socio").Result = Trim(lblNombreSocio.Text)
            MiWord.ActiveDocument.FormFields("domicilio_socio").Result = Trim(RsSocios!domicilio_socio) & " nº " & Trim(RsSocios!Numero)
            MiWord.ActiveDocument.FormFields("poblacion_socio").Result = Trim(RsSocios!poblacion)
            MiWord.ActiveDocument.FormFields("nif_socio").Result = Trim(RsSocios!nif_socio)
            MiWord.ActiveDocument.FormFields("numero_socio").Result = Trim(RsSocios!CODIGO_SOCIO)
            MiWord.ActiveDocument.FormFields("nif_socio").Result = Trim(RsSocios!nif_socio)
            'MiWord.ActiveDocument.FormFields("num_cuenta").Result = Trim(DameCuentaSocio(RsSocios!CODIGO_SOCIO, RsSocios!BANCO_LIQUID))
            MiWord.ActiveDocument.FormFields("dia").Result = "" & Dia
            MiWord.ActiveDocument.FormFields("mes").Result = Format(DtpFechaEdicion, "mmmm")
            MiWord.ActiveDocument.FormFields("anyo").Result = Format(DtpFechaEdicion, "yyyy")
            MiWord.ActiveDocument.FormFields("firmado").Result = Trim(TxtFirmadopor.Text)
            MiWord.ActiveDocument.FormFields("Txtfmdosocio").Result = Trim(lblNombreSocio.Text)

            ' Imprimir y cerrar
            If ImpresoraAct <> "" Then
                MiWord.ActivePrinter = ImpresoraAct
            End If

            MiWord.PrintOut(FileName:="", Range:=0, Item:=
                                    0, Copies:=1, Pages:="", PageType:=0,
                                    Collate:=True, Background:=True, PrintToFile:=False, PrintZoomColumn:=0,
                                    PrintZoomRow:=0, PrintZoomPaperWidth:=0, PrintZoomPaperHeight:=0)

            For i = 1 To MiWord.Documents.Count
                MiWord.Documents(1).Saved = True
                MiWord.Documents(1).Close()
            Next

            PrbProgreso.Visible = False
            Return

        Catch ex As Exception
            MsgBox("Se produjo en error a la hora de traspasar los datos a la plantilla solicitada." & vbCrLf &
                "Compruebe que la plantilla introducida es la correcta")
            PrbProgreso.Visible = False
            Return
        End Try

    End Sub

    Private Function DameCuentaSocio(Socio As Integer, Contador As Integer) As String
        Dim RsBS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        RsBS.Open("SELECT * FROM bancos_socios_coop WHERE  CODIGO_SOCIO = " & Socio & " AND " & " CONTADOR = " & Contador, ObjetoGlobal.cn)

        If RsBS.RecordCount > 0 Then
            Return (RsBS!entidad) & " " & (RsBS!sucursal) & " " & (RsBS!digito_control) & " " & (RsBS!NUMERO_CUENTA)
        Else
            Return ""
        End If
        Return ""

    End Function

    Private Function DameProvincia(codigo As String) As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        rs.Open("SELECT * FROM PROVINCIAS_COOP WHERE PROVINCIA = '" & codigo & "'", ObjetoGlobal.cn)

        If rs.RecordCount > 0 Then
            Return "" & (rs!NOMBRE_PROVINCIA)
        Else
            Return ""
        End If

    End Function


    Private Sub ImprimeFichas(Plantilla)
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nNumCultivos As Integer
        Dim RsCamposSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartidas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSqlCultivos As String
        Dim sSqlCultSIG As String
        Dim aDatos(,) As String
        Dim RsTerminos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionTermino As String
        Dim DescripcionPartida As String
        Dim i, o As Integer
        Dim nMax As Integer
        Dim Fila As Integer
        Dim bControlColor As Boolean
        Dim NombreSocio As String
        Dim sSqlSocios As String
        Dim lHayPendiente As Boolean
        Dim bIncremento As Boolean

        lHayPendiente = False
        frCartel.Visible = False
        frCartel.Refresh()

        PrbProgreso.Minimum = 0
        PrbProgreso.Maximum = 10
        PrbProgreso.Text = 1
        PrbProgreso.Refresh()

        '  On Error GoTo ERROR:
        If MiWord Is Nothing Or Not bEsMiWord Then
            MiWord = New Microsoft.Office.Interop.Word.Application
            MiWord.Visible = False
            bEsMiWord = True
        End If

        sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " & Trim(TxtCodigoSocio.Text) & " AND "
        sSqlSocios = sSqlSocios + " Exists (SELECT codigo_socio From campos WHERE campos.empresa = '" + Trim(TxtEmpresa.Text) + "' AND campos.codigo_socio = socios_coop.codigo_socio AND campos.alta_baja = 'A' AND CAMPOS.DOC_CATASTRAL <> 'S')"

        RsSocios.Open(sSqlSocios, ObjetoGlobal.cn)

        If RsSocios.RecordCount > 0 Then

            PrbProgreso.Maximum = RsSocios.RecordCount
            frCartel.Visible = True

            While Not RsSocios.EOF
                PrbProgreso.Text = RsSocios.AbsolutePosition
                PrbProgreso.Refresh()

                lHayPendiente = False

                MiWord.Documents.Add(CStr(Plantilla))

                NombreSocio = IIf(Trim(RsSocios!APELLIDOS_SOCIO) = "", (RsSocios!NOMBRE_SOCIO), Trim(RsSocios!APELLIDOS_SOCIO) & ", " & Trim(RsSocios!NOMBRE_SOCIO))
                MiWord.ActiveDocument.FormFields("NombreSocio").Result = "" & Trim(NombreSocio)
                MiWord.ActiveDocument.FormFields("DomicilioSocio").Result = "" & Trim(RsSocios!domicilio_socio) & " nº " & Trim(RsSocios!Numero) & IIf(Not IsDBNull(RsSocios!Puerta), ", " & RsSocios!Puerta, "")
                MiWord.ActiveDocument.FormFields("Poblacion").Result = "" & Trim(RsSocios!poblacion)
                MiWord.ActiveDocument.FormFields("DNISocio").Result = "" & Trim(RsSocios!nif_socio)
                MiWord.ActiveDocument.FormFields("CodigoSocio").Result = Trim(RsSocios!CODIGO_SOCIO)
                MiWord.ActiveDocument.FormFields("CodigoPostal").Result = "" & Trim(RsSocios!CODIGO_POSTAL)
                MiWord.ActiveDocument.FormFields("Provincia").Result = "" & Trim(DameProvincia(Trim(RsSocios!PROVINCIA)))
                MiWord.ActiveDocument.FormFields("numtelefono").Result = "" & Trim(Trim(RsSocios!TELEFONO))
                MiWord.ActiveDocument.FormFields("nummovil").Result = "" & Trim(RsSocios!movil_socio)
                If RsSocios!Alta_baja_coop = "S" Then
                    MiWord.ActiveDocument.FormFields("situacionsocio").Result = "BAJA COOPERATIVA FECHA " & RsSocios!Baja_coop
                Else
                    MiWord.ActiveDocument.FormFields("situacionsocio").Result = ""
                End If

                RsCampos.Open("SELECT *  " &
                            " FROM CAMPOS " &
                            " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                            " CAMPOS.ALTA_BAJA = 'A' AND " &
                            " CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)

                If RsCampos.RecordCount > 0 Then

                    Fila = MiWord.ActiveDocument.Tables(1).Rows.Count
                    While Not RsCampos.EOF

                        ' Localizamos la partida
                        If Not IsDBNull(RsCampos!SITUACION_CAMPO) Then
                            RsPartidas.Open("SELECT DESCRIPCION FROM SITUACION_CAMPOS WHERE EMPRESA = '" & Trim(TxtEmpresa.Text) & "' AND CODIGO_SITUACION = " & (RsCampos!SITUACION_CAMPO), ObjetoGlobal.cn)
                            If RsPartidas.RecordCount > 0 Then
                                DescripcionPartida = "" & Trim(RsPartidas!DESCRIPCION)
                            Else
                                DescripcionPartida = ""
                            End If
                            RsPartidas.Close()
                        End If

                        nMax = 1

                        ' Estudio de los cultivos
                        sSqlCultivos = "SELECT Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones"
                        sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                        RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                        If nMax < RsCultivos.RecordCount Then
                            nMax = RsCultivos.RecordCount
                        End If
                        nNumCultivos = RsCultivos.RecordCount
                        RsCultivos.Close()

                        If nNumCultivos > 0 Then
                            ' Contamos los cultivos SIG para este campo
                            sSqlCultSIG = "SELECT * FROM cultivos_sig WHERE "
                            sSqlCultSIG = sSqlCultSIG + " empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Campo=" & RsCampos!Codigo_Campo
                            sSqlCultSIG = sSqlCultSIG + " AND  Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                            RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)

                            If nMax < RsCultSIG.RecordCount Then
                                nMax = RsCultSIG.RecordCount
                            End If

                            RsCultSIG.Close()

                            ' Dimensionamos el array
                            If nMax < 1 Then
                                nMax = 1
                            End If
                            ReDim aDatos(22, nMax - 1)
                            ' Y por fin empezamos a incluir datos a la ficha
                            ' Datos generales de campos
                            aDatos(0, 0) = "" & Trim(RsCampos!Codigo_Campo)
                            aDatos(1, 0) = "" & DescripcionPartida
                            aDatos(2, 0) = "" & RsCampos!FECHA_INSCRIPCION
                            aDatos(3, 0) = "" & RsCampos!FECHA_BAJA
                            aDatos(16, 0) = "" & Trim(RsCampos!SECANO_REGADIO)
                            aDatos(17, 0) = "" & Trim(RsCampos!DOC_CATASTRAL)

                            ' Ahora los cultivos
                            sSqlCultivos = "SELECT Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones"
                            sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"
                            'sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"

                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"

                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                            RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                            i = 0

                            While Not RsCultivos.EOF
                                If i > UBound(aDatos, 2) Then ReDim Preserve aDatos(UBound(aDatos, 1), i)

                                aDatos(4, i) = "" & RsCultivos!DESCRIPCION

                                sSqlCultSIG = "SELECT cultivos_SIG.Termino, cultivos_SIG.recinto, cultivos_SIG.hectareas_sigpac, cultivos_SIG.uso_sigpac, Terminos.Descripcion_Ter,cultivos_SIG.Poligono, cultivos_SIG.Parcela, cultivos_SIG.hectareas, cultivos_SIG.ALEGACION, cultivos_SIG.arboles, cultivos_SIG.fecha_plantacion FROM cultivos_SIG, Terminos WHERE (terminos.Codigo_termino=cultivos_SIG.termino) "
                                sSqlCultSIG = sSqlCultSIG + " AND  empresa='" & Trim(TxtEmpresa.Text) & "'"
                                sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Campo=" & RsCampos!Codigo_Campo
                                sSqlCultSIG = sSqlCultSIG + " AND  Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                                sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Variedad='" & RsCultivos!CODIGO_VARIEDAD + "'"
                                RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)
                                bIncremento = False

                                While Not RsCultSIG.EOF
                                    If Not IsDBNull(RsCultSIG!termino) Then
                                        RsTerminos.Open("SELECT DESCRIPCION_TER FROM TERMINOS WHERE CODIGO_TERMINO = " & (RsCultSIG!termino), ObjetoGlobal.cn)
                                        If RsTerminos.RecordCount > 0 Then
                                            DescripcionTermino = "" & Trim(RsTerminos!descripcion_ter)
                                        Else
                                            DescripcionTermino = ""
                                        End If
                                        RsTerminos.Close()
                                    End If
                                    aDatos(5, i) = "" & DescripcionTermino
                                    aDatos(6, i) = "" & RsCultSIG!POLIGONO
                                    aDatos(7, i) = "" & RsCultSIG!PARCELA
                                    aDatos(8, i) = "" & RsCultSIG!RECINTO
                                    aDatos(10, i) = "" & RsCultSIG!HECTAREAS
                                    aDatos(11, i) = "" & RsCultSIG!HECTAREAS_SIGPAC
                                    aDatos(12, i) = "" & RsCultSIG!USO_SIGPAC
                                    aDatos(13, i) = "" & RsCultSIG!ALEGACION
                                    aDatos(14, i) = "" & RsCultSIG!ARBOLES
                                    If Not IsDBNull(RsCultSIG!FECHA_PLANTACION) Then
                                        aDatos(15, i) = "" & RsCultSIG!FECHA_PLANTACION
                                    Else
                                        aDatos(15, i) = ""
                                    End If

                                    i = i + 1
                                    bIncremento = True
                                    RsCultSIG.MoveNext()
                                End While
                                If Not bIncremento Then
                                    i = i + 1
                                End If
                                RsCultSIG.Close()
                                RsCultivos.MoveNext()
                            End While ' DE LOS CULTIVOS
                            RsCultivos.Close()

                            For i = 0 To UBound(aDatos, 2)

                                If lHayPendiente Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End)
                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                    lHayPendiente = False
                                End If

                                Fila = MiWord.ActiveDocument.Tables(1).Rows.Count

                                For o = 1 To 18
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Shading.BackgroundPatternColor = IIf(Not bControlColor, Microsoft.Office.Interop.Word.WdColor.wdColorGray25, Microsoft.Office.Interop.Word.WdColor.wdColorWhite)
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Text = "" & aDatos(o - 1, i)
                                Next

                                If i < UBound(aDatos, 2) Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End)
                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                Else
                                    lHayPendiente = True
                                End If

                            Next
                            bControlColor = Not bControlColor
                        Else
                            If nMax < 1 Then
                                nMax = 1
                            End If
                            ReDim aDatos(22, nMax - 1)
                            ' Datos generales de campos
                            aDatos(0, 0) = "" & Trim(RsCampos!Codigo_Campo)
                            aDatos(1, 0) = "" & DescripcionPartida
                            aDatos(2, 0) = "" & RsCampos!FECHA_INSCRIPCION
                            aDatos(3, 0) = "" & RsCampos!FECHA_BAJA
                            aDatos(16, 0) = "" & Trim(RsCampos!SECANO_REGADIO)
                            aDatos(17, 0) = "" & Trim(RsCampos!DOC_CATASTRAL)
                            For i = 0 To UBound(aDatos, 2)
                                If lHayPendiente Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End)
                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                    lHayPendiente = False
                                End If

                                Fila = MiWord.ActiveDocument.Tables(1).Rows.Count

                                For o = 1 To 18
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Shading.BackgroundPatternColor = IIf(Not bControlColor, Microsoft.Office.Interop.Word.WdColor.wdColorGray25, Microsoft.Office.Interop.Word.WdColor.wdColorWhite)
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Text = "" & aDatos(o - 1, i)
                                Next

                                If i < UBound(aDatos, 2) Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 17).Range.End)
                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                Else
                                    lHayPendiente = True
                                End If

                            Next
                            bControlColor = Not bControlColor
                        End If
                        RsCampos.MoveNext()
                    End While ' DE LOS CAMPOS
                End If
                RsCampos.Close()
                ' Por fin empezamos a pintar los campos
                ' Ya esta
                ' Y ahora imprimimos este socio
                If ImpresoraAct <> "" Then
                    MiWord.ActivePrinter = ImpresoraAct
                End If

                MiWord.PrintOut(FileName:="", Range:=0, Item:=
                                    0, Copies:=1, Pages:="", PageType:=0,
                                    Collate:=True, Background:=True, PrintToFile:=False, PrintZoomColumn:=0,
                                    PrintZoomRow:=0, PrintZoomPaperWidth:=0, PrintZoomPaperHeight:=0)
                    For i = 1 To MiWord.Documents.Count
                        MiWord.Documents(1).Saved = True
                        MiWord.Documents(1).Close()
                    Next

                RsSocios.MoveNext() ' al socio siguiente pasamos

            End While
        End If
        RsSocios.Close()
        frCartel.Visible = False
        Exit Sub

    End Sub

    Private Function SelImpresora_Click()
        Dim i As Integer
        Dim NumCopias As Integer

        NumCopias = 1

        Dim prtDialog As New PrintDialog
        Dim prtSettings As PrinterSettings
        If prtSettings Is Nothing Then
            prtSettings = New PrinterSettings
        End If

        With prtDialog
            .AllowPrintToFile = False
            .AllowSelection = False
            .AllowSomePages = False
            .PrintToFile = False
            .ShowHelp = False
            .ShowNetwork = True

            .PrinterSettings = prtSettings

            If .ShowDialog() = DialogResult.OK Then
                prtSettings = .PrinterSettings
                ImpresoraAct = prtSettings.PrinterName
                Copias = prtSettings.Copies
            Else
                Return False
            End If

        End With

        Return True

    End Function

    Public Function NumeroCopias(n)
        Copias = n
        txtCopias.Text = CStr(n)
    End Function

    Private Sub FrmEdicionFicha_Load(sender As Object, e As EventArgs) Handles Me.Load
        InicializarPantalla()
    End Sub

    Private Function DirectorioMisDocumentos() As String
        Dim XRuta As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        On Error Resume Next

        XRuta = Strings.Left(XRuta, InStr(XRuta, Chr(0)) - 1)
        If Len(Trim(XRuta)) < 3 Then XRuta = vbNullString
        Return XRuta

    End Function
    Public Sub InicializarPantalla()
        Dim sPlant As String

        TxtEmpresa.Text = ObjetoGlobal.EmpresaActual
        lblNombreEmpresa.Text = ObjetoGlobal.EmpresaRazonSocial
        TxtEjercicio.Text = ObjetoGlobal.EjercicioActual
        LblNombreEjercicio.Text = ObjetoGlobal.DescripcionEjercicio
        DtpFechaEdicion = Date.Now
        If Not DatosExternos Then
            TxtCodigoSocio.Text = ""
            lblNombreSocio.Text = ""
        End If
        DtpFechaEdicion = Date.Now
        sPlant = Trim(DirectorioMisDocumentos) & "\Plantillas"

        frCartel.Visible = False

        TxtPlantilla.Text = TablaParametros("Edición Contrato", "Plantilla", "")
        TxtPlantillaficha.Text = TablaParametros("Edición Contrato", "Plantilla Ficha", "")
        TxtFirmadoPor.Text = TablaParametros("Edición Contrato", "Firmado Por", "")
        TxtFechaEdicion.Text = CDate(TablaParametros("Edición Contrato", "Fecha firma", CStr(Date.Now)))

        If Not ComprobarPlantilla(TxtPlantilla.Text) Then
            MsgBox("No he podido encontrar la plantilla del contrato." & vbCrLf &
               "Revise la tabla parámetros proceso 'Edición Ficha'", vbInformation, "Buscar plantilla documento")
            TxtPlantilla.Text = ""
        End If
        If Not ComprobarPlantilla(TxtPlantillaficha.Text) Then
            MsgBox("No he podido encontrar la plantilla del la ficha." & vbCrLf &
               "Revise la tabla parámetros proceso 'Edición Ficha'", vbInformation, "Buscar plantilla documento")
            TxtPlantillaficha.Text = ""
        End If

        PrbProgreso.Visible = False

    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        InicializarPantalla()
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Function TablaParametros(ByVal ed As String, ByVal da As String, Optional ByVal ot As String = "") As String
        Dim Retorno As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If ot = "" Then
            Retorno = ""
        Else
            Retorno = ot
        End If

        rs.Open("SELECT VALOR_DEFECTO FROM TABLA_PARAMETROS WHERE EMPRESA='" & ObjetoGlobal.EmpresaActual &
                 "' AND PROCESO='" & ed & "' AND CAMPO='" & da & "'", ObjetoGlobal.cn)

        If rs.RecordCount > 0 Then
            Retorno = "" & Trim(rs!valor_defecto)
        End If

        Return Retorno

    End Function

End Class