Imports System.ComponentModel
Imports Word.WdUnits
Imports System.Drawing.Printing

Public Class FrmEdicionFicha
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
    Public ImprimirAhora As Boolean = False

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

        CmdImprimirFichas(TxtPlantilla.Text)

    End Sub
    Public Sub ImprimeFicha()
        If Not ComprobarPlantilla(TxtPlantilla.Text) Then
            MsgBox("No se encuentra la plantilla seleccionada.")
            Return
        End If

        If Not DatosValidos() Then
            Return
        End If
        CmdImprimirFichas(TxtPlantilla.Text)
        Return
    End Sub

    Private Sub CmdBuscarPlantilla_Click()
        Dim fileStream
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        Dim filePath As String

        OpenFileDialog.InitialDirectory = "c:\"
        OpenFileDialog.Filter = "dot files (*.dot)|*.dot|dotx files (*.dotx)|All files (*.*)|*.*|"
        OpenFileDialog.FilterIndex = 2
        OpenFileDialog.RestoreDirectory = True
        OpenFileDialog.Title = "Selección de plantilla"

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            filePath = OpenFileDialog.FileName
            'fileStream = OpenFileDialog.OpenFile()
        End If

    End Sub

    Public Sub InicializarPantalla()
        Dim sPlant As String

        TxtEmpresa.Text = ObjetoGlobal.EmpresaActual
        lblNombreEmpresa.Text = ObjetoGlobal.EmpresaRazonSocial
        TxtEjercicio.Text = ObjetoGlobal.EjercicioActual
        LblNombreEjercicio.Text = ObjetoGlobal.DescripcionEjercicio
        DtpFechaEdicion = Date.Now
        If Not DatosExternos Then
            TxtCodigoSocio.Text = ""
            TxtHastaSocio.Text = ""
            lblNombreSocio.Text = ""
            lblHastaNombresocio.Text = ""
        End If
        DtpFechaEdicion = Date.Now
        sPlant = Trim(DirectorioMisDocumentos) & "\Plantillas"

        If Dir(sPlant & "\FichaSocioNueva.Dot") <> "" Then
            sPlantillaF = sPlant & "\FichaSocioNueva.Dot"
        Else
            sPlantillaF = TablaParametros("Edición Ficha", "Plantilla", "")
        End If
        If Dir(sPlant & "\FichaSocioANueva.Dot") <> "" Then
            sPlantillaS = sPlant & "\FichaSocioANueva.Dot"
        Else
            sPlantillaS = TablaParametros("Ficha Seguro", "Plantilla", "")
        End If
        If Dir(sPlant & "\FichaSocioANueva.Dot") <> "" Then
            sPlantillaD = sPlant & "\FichaSocioANueva.Dot"
        Else
            sPlantillaD = TablaParametros("Ficha Declaración", "Plantilla", "")
        End If
        ChkAlta.Checked = False

        frCartel.Visible = False

        If OpcNormal.Checked = True Then
            TxtPlantilla.Text = sPlantillaF
        ElseIf OpcDeclaracion.Checked = True Then
            TxtPlantilla.Text = sPlantillaS
        Else
            TxtPlantilla.Text = sPlantillaD
        End If

        If Not ComprobarPlantilla(TxtPlantilla.Text) Then
            MsgBox("No he podido encontrar la plantilla del contrato." & vbCrLf &
               "Revise la tabla parámetros proceso 'Edición Ficha'", vbInformation, "Buscar plantilla documento")
            TxtPlantilla.Text = ""
        End If

        'If ImprimirAhora Then
        '    ImprimeFicha()
        '    Me.Close()
        'End If

    End Sub

    Private Sub OpcDeclaracion_Click()
        TxtPlantilla.Text = sPlantillaD
    End Sub

    Private Sub OpcFichaSeguro_Click()
        TxtPlantilla.Text = sPlantillaS
    End Sub

    Private Sub OpcNormal_Click()
        TxtPlantilla.Text = sPlantillaF
    End Sub

    Private Sub TxtCodigoSocio_Validate(Cancel As Boolean)

        If Trim(TxtCodigoSocio.Text) = "" Then
            lblNombreSocio.Text = ""
            Cancel = False
            TxtCodigoSocio.Focus()
            Return
        ElseIf Not ExisteSocio(TxtCodigoSocio.Text, lblNombreSocio.Text) Then
            MsgBox("Ese socio no existe.", vbInformation, "El socio no existe.")
            lblNombreSocio.Text = ""
            TxtCodigoSocio.Text = ""
            Cancel = True
        Else
            Cancel = False
        End If

    End Sub

    Private Sub TxtHastaSocio_Validate(Cancel As Boolean)

        If Trim(TxtHastaSocio.Text) = "" Then
            lblHastaNombresocio.Text = ""
            TxtHastaSocio.Focus()
            Cancel = False
        ElseIf Not ExisteSocio(TxtHastaSocio.Text, lblHastaNombresocio.Text) Then
            MsgBox("Ese socio no existe.", vbInformation, "El socio no existe.")
            lblHastaNombresocio.Text = ""
            TxtHastaSocio.Text = ""
            Cancel = True
        Else
            Cancel = False
        End If

    End Sub

    Private Sub CmdGridcodigosocio_Click()

    End Sub
    Private Sub CmdGridHastasocio_Click()

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
    Private Sub ImprimirEnWord(Plantilla As String)
        Dim MiWord As Microsoft.Office.Interop.Word.Application = New Microsoft.Office.Interop.Word.Application
        Dim RsCoop As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartidas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTerminos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsVariedades As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionTermino As String
        Dim NumeroOpa As Integer
        Dim Fila As Integer

        frCartel.Visible = True
        frCartel.Refresh()

        PrbProgreso.Minimum = 0
        PrbProgreso.Maximum = 10
        PrbProgreso.Text = 1
        PrbProgreso.Refresh()
        MiWord.Documents.Add(Plantilla)
        MiWord.Visible = True

        RsSocios.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " & Trim(TxtCodigoSocio.Text), ObjetoGlobal.cn)
        If RsSocios.RecordCount <= 0 Then Exit Sub
        MiWord.ActiveDocument.FormFields("NombreSocio").Result = "" & Trim(lblNombreSocio.Text)
        MiWord.ActiveDocument.FormFields("DomicilioSocio").Result = "" & Trim(RsSocios!domicilio_socio) & " nº " & Trim(RsSocios!Numero)
        MiWord.ActiveDocument.FormFields("Poblacion").Result = "" & Trim(RsSocios!poblacion)
        MiWord.ActiveDocument.FormFields("DNISocio").Result = "" & Trim(RsSocios!nif_socio)
        MiWord.ActiveDocument.FormFields("CodigoSocio").Result = Trim(RsSocios!CODIGO_SOCIO)
        MiWord.ActiveDocument.FormFields("CodigoPostal").Result = "" & Trim(RsSocios!CODIGO_POSTAL)
        MiWord.ActiveDocument.FormFields("Provincia").Result = "" & Trim(DameProvincia(Trim(RsSocios!PROVINCIA)))
        MiWord.ActiveDocument.FormFields("numtelefono").Result = "" & Trim(DameProvincia(Trim(RsSocios!TELEFONO)))
        MiWord.ActiveDocument.FormFields("nummovil").Result = "" & Trim(RsSocios("movil_socio"))

        If Not ChkAlta.Checked Then
            RsCampos.Open("SELECT CAMPOS.CODIGO_CAMPO, CAMPOS.SITUACION_CAMPO, CAMPOS.TERMINO, CAMPOS.POLIGONO, CAMPOS.PARCELA, CAMPOS.FECHA_INSCRIPCION, CAMPOS.FECHA_BAJA, CULTIVOS.HECTAREAS, CULTIVOS.CODIGO_VARIEDAD, CULTIVOS.ARBOLES, CULTIVOS.FECHA_PLANTACION_1, CAMPOS.SECANO_REGADIO " &
        " FROM CAMPOS LEFT JOIN CULTIVOS ON " &
        " CAMPOS.EMPRESA = CULTIVOS.EMPRESA AND " &
        " CAMPOS.CODIGO_CAMPO = CULTIVOS.CODIGO_CAMPO AND " &
        " CULTIVOS.EJERCICIO = '" & Trim(ObjetoGlobal.EjercicioActual) & "'" &
        " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
        " CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
        Else
            RsCampos.Open("SELECT CAMPOS.CODIGO_CAMPO, CAMPOS.SITUACION_CAMPO, CAMPOS.TERMINO, CAMPOS.POLIGONO, CAMPOS.PARCELA, CAMPOS.FECHA_INSCRIPCION, CAMPOS.FECHA_BAJA, CULTIVOS.HECTAREAS, CULTIVOS.CODIGO_VARIEDAD, CULTIVOS.ARBOLES, CULTIVOS.FECHA_PLANTACION_1, CAMPOS.SECANO_REGADIO " &
        " FROM CAMPOS LEFT JOIN CULTIVOS ON " &
        "(CULTIVOS.EMPRESA = CAMPOS.EMPRESA AND " &
        " CULTIVOS.CODIGO_CAMPO = CAMPOS.CODIGO_CAMPO AND " &
        " CULTIVOS.EJERCICIO = '" & Trim(ObjetoGlobal.EjercicioActual) & "' AND " &
        " CULTIVOS.ALTA_BAJA = 'A')" &
        " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
        " CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'AND CAMPOS.ALTA_BAJA='A'", ObjetoGlobal.cn)
        End If
        While Not RsCampos.EOF
            PrbProgreso.Maximum = RsCampos.RecordCount
            PrbProgreso.Text = RsCampos.AbsolutePosition
            PrbProgreso.Refresh()
            Fila = MiWord.ActiveDocument.Tables(1).Rows.Count

            RsVariedades.Open("SELECT DESCRIPCION FROM VARIEDADES WHERE EMPRESA = '" & Trim(TxtEmpresa.Text) & "' AND CODIGO_VARIEDAD = '" & Trim(RsCampos!CODIGO_VARIEDAD) & "'", ObjetoGlobal.cn)

            RsPartidas.Open("SELECT DESCRIPCION FROM SITUACION_CAMPOS WHERE EMPRESA = '" & Trim(TxtEmpresa.Text) & "' AND CODIGO_SITUACION = " & (RsCampos!SITUACION_CAMPO), ObjetoGlobal.cn)
            If Not IsDBNull(RsCampos!termino) Then

                RsTerminos.Open("SELECT DESCRIPCION_TER FROM TERMINOS WHERE CODIGO_TERMINO = " & (RsCampos!termino), ObjetoGlobal.cn)
                If RsTerminos.RecordCount > 0 Then DescripcionTermino = "" & Trim(RsTerminos!descripcion_ter)
            Else
                DescripcionTermino = ""
            End If
            MiWord.ActiveDocument.Tables(1).Cell(Fila, 1).Range.Text = "" & Trim(RsCampos!Codigo_Campo)
            If RsPartidas.RecordCount > 0 Then MiWord.ActiveDocument.Tables(1).Cell(Fila, 2).Range.Text = "" & Trim(RsPartidas!DESCRIPCION)
            MiWord.ActiveDocument.Tables(1).Cell(Fila, 6).Range.Text = "" & Trim(RsCampos!FECHA_INSCRIPCION)
            MiWord.ActiveDocument.Tables(1).Cell(Fila, 7).Range.Text = "" & Trim(RsCampos!FECHA_BAJA)
            MiWord.ActiveDocument.Tables(1).Cell(Fila, 8).Range.Text = "" & Trim(RsCampos!HECTAREAS)
            If RsVariedades.RecordCount > 0 Then MiWord.ActiveDocument.Tables(1).Cell(Fila, 9).Range.Text = "" & Trim(RsVariedades!DESCRIPCION)
            MiWord.ActiveDocument.Tables(1).Cell(Fila, 10).Range.Text = "" & Trim(RsCampos!ARBOLES)
            If Not IsDBNull(RsCampos!FECHA_PLANTACION_1) Then MiWord.ActiveDocument.Tables(1).Cell(Fila, 11).Range.Text = Year(RsCampos!FECHA_PLANTACION_1)
            MiWord.ActiveDocument.Tables(1).Cell(Fila, 12).Range.Text = "" & Trim(RsCampos!SECANO_REGADIO)
            MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 12).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 12).Range.End)
            MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
            RsCampos.MoveNext()
        End While
        ' Bloqueamos la plantilla y mostramos el word.
        MiWord.ActiveDocument.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyComments)
        MiWord.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateNormal
        MiWord.Visible = True
        frCartel.Visible = False
        Exit Sub

    End Sub

    Private Function DameNumeroOpa() As Integer
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer
        Dim BlnSalir As Boolean

        rs.Open("SELECT * FROM DATOS_EMPRESA WHERE EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
        i = 1
        BlnSalir = False
        If rs.RecordCount > 0 Then
            While i <= 5 And Not BlnSalir
                If InStr(1, rs("tipo_asociacion_" & i), "OPFH") > 0 Or InStr(1, rs("tipo_asociacion_" & i), "O.P.F.H") > 0 Then
                    BlnSalir = True
                Else
                    i = i + 1
                End If
            End While
        End If

        If Not BlnSalir Then
            DameNumeroOpa = 1
        Else
            DameNumeroOpa = i
        End If

    End Function
    Private Function DameCuentaSocio(Socio As Integer, Contador As Integer) As String
        Dim RsBS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        RsBS.Open("SELECT NUMERO_CUENTA FROM BANCOS_SOCIOS_COOP WHERE " &
    " CODIGO_SOCIO = " & Socio & " AND " &
    " CONTADOR = " & Contador, ObjetoGlobal.cn)
        If RsBS.RecordCount > 0 Then
            DameCuentaSocio = (RsBS!NUMERO_CUENTA)
        Else
            DameCuentaSocio = ""
        End If
    End Function

    Private Function DameProvincia(codigo As String) As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        rs.Open("SELECT * FROM PROVINCIAS_COOP WHERE PROVINCIA = '" & codigo & "'", ObjetoGlobal.cn)

        If rs.RecordCount > 0 Then
            DameProvincia = "" & (rs!NOMBRE_PROVINCIA)
        Else
            DameProvincia = ""
        End If

    End Function


    Private Function DatosValidos() As Boolean
        If Trim(TxtCodigoSocio.Text) = "" Then
            MsgBox("El dato Código Socio es requerido.", vbInformation, Me.Text)
            TxtCodigoSocio.Focus()
            Return False
        ElseIf Trim(TxtHastaSocio.text) = "" Then
            TxtHastaSocio = TxtCodigoSocio
            Return True
        Else
            Return True
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


        If Not ChkAlta.Checked Then
            sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio.Text) ' & " AND ALTA_BAJA = 'A' "
        Else
            '       sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio) & " AND ALTA_BAJA = 'A' AND "
            sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio.Text) & " AND "
            sSqlSocios = sSqlSocios + " Exists (SELECT codigo_socio From campos WHERE campos.empresa = '" + Trim(TxtEmpresa.Text) + "' AND campos.codigo_socio = socios_coop.codigo_socio AND campos.alta_baja = 'A' AND CAMPOS.DOC_CATASTRAL <> 'S')"
        End If

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

                If ChIncluirCamposBaja.Checked Then
                    RsCampos.Open("SELECT *  " &
                            " FROM CAMPOS " &
                            " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                            " (CAMPOS.ALTA_BAJA = 'A' OR (CAMPOS.ALTA_BAJA = 'B' AND CAMPOS.FECHA_BAJA >='" & DtpFechaEdicion & "'))" &
                            " AND CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                Else
                    RsCampos.Open("SELECT *  " &
                            " FROM CAMPOS " &
                            " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                            " CAMPOS.ALTA_BAJA = 'A' AND " &
                            " CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                End If

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

                        If ChIncluirCamposBaja.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Cultivos.Fecha_baja>='" & DtpFechaEdicion & "'))"
                        Else
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
                        End If

                        If ChSoloCamposCitricos.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                        End If

                        'sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
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

                            If ChIncluirCamposBaja.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Cultivos.Fecha_baja>='" & DtpFechaEdicion & "'))"
                            Else
                                sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
                            End If

                            If ChSoloCamposCitricos.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                            End If

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
                If Impresora.Checked Then
                    MiWord.PrintOut(FileName:="", Range:=0, Item:=
                                    0, Copies:=1, Pages:="", PageType:=0,
                                    Collate:=True, Background:=True, PrintToFile:=False, PrintZoomColumn:=0,
                                    PrintZoomRow:=0, PrintZoomPaperWidth:=0, PrintZoomPaperHeight:=0)
                    For i = 1 To MiWord.Documents.Count
                        MiWord.Documents(1).Saved = True
                        MiWord.Documents(1).Close()
                    Next
                Else
                    MiWord.Visible = False
                    bEsMiWord = False
                End If

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
    Private Sub ImprimeDeclaracion1(Plantilla, sTipo)
        Dim nNumCultivos As Integer
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCamposSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartidas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSqlCamposSIG As String
        Dim sSqlCultivos As String
        Dim sSqlCultSIG As String
        Dim aDatos(,) As String
        Dim RsTerminos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionTermino As String
        Dim DescripcionPartida As String
        Dim i, o As Integer
        Dim NumeroOpa As Integer
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

        If MiWord Is Nothing Then
            MiWord = New Microsoft.Office.Interop.Word.Application
            MiWord.Visible = False
            bEsMiWord = True
        End If

        If Not ChkAlta.Checked Then
            sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio.Text) & " AND ALTA_BAJA = 'A' "
        Else
            sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio.Text) & " AND ALTA_BAJA = 'A' AND "
            sSqlSocios = sSqlSocios + " Exists (SELECT codigo_socio From campos WHERE campos.empresa = '" + Trim(TxtEmpresa.Text) + "' AND campos.codigo_socio = socios_coop.codigo_socio AND campos.alta_baja = 'A' AND CAMPOS.DOC_CATASTRAL <> 'S')"
        End If

        RsSocios.Open(sSqlSocios, ObjetoGlobal.cn)

        If RsSocios.RecordCount > 0 Then

            PrbProgreso.Maximum = RsSocios.RecordCount
            frCartel.Visible = True

            Do While Not RsSocios.EOF
                PrbProgreso.Text = RsSocios.AbsolutePosition
                PrbProgreso.Refresh()

                lHayPendiente = False

                MiWord.Documents.Add(CStr(Plantilla))

                NombreSocio = IIf(Trim(RsSocios!APELLIDOS_SOCIO) = "", (RsSocios!NOMBRE_SOCIO), Trim(RsSocios!APELLIDOS_SOCIO) & ", " & Trim(RsSocios!NOMBRE_SOCIO))

                If Trim(sTipo) = "SEGURO" Then
                    MiWord.ActiveDocument.FormFields("declaracion").Result = "SEGURO"
                Else
                    MiWord.ActiveDocument.FormFields("declaracion").Result = "EFECTIVOS PRODUCTIVOS"
                End If

                MiWord.ActiveDocument.FormFields("NombreSocio").Result = "" & Trim(NombreSocio)

                MiWord.ActiveDocument.FormFields("DomicilioSocio").Result = "" & Trim(RsSocios!domicilio_socio) & " nº " & Trim(RsSocios!Numero) & IIf(Not IsDBNull(RsSocios!Puerta), ", " & RsSocios!Puerta, "")

                MiWord.ActiveDocument.FormFields("Poblacion").Result = "" & Trim(RsSocios!poblacion)

                MiWord.ActiveDocument.FormFields("DNISocio").Result = "" & Trim(RsSocios!nif_socio)

                MiWord.ActiveDocument.FormFields("CodigoSocio").Result = Trim(RsSocios!CODIGO_SOCIO)

                MiWord.ActiveDocument.FormFields("CodigoPostal").Result = "" & Trim(RsSocios!CODIGO_POSTAL)

                MiWord.ActiveDocument.FormFields("Provincia").Result = "" & Trim(DameProvincia(Trim(RsSocios!PROVINCIA)))

                MiWord.ActiveDocument.FormFields("numtelefono").Result = "" & Trim(Trim(RsSocios!TELEFONO))

                NumeroOpa = DameNumeroOpa()

                MiWord.ActiveDocument.FormFields("CodigoOPA").Result = "" & Trim(RsSocios("num_agrup_" & NumeroOpa))

                If ChIncluirCamposBaja.Checked Then
                    RsCampos.Open("SELECT *  " &
                              " FROM CAMPOS " &
                              " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                              " (CAMPOS.ALTA_BAJA = 'A' OR (CAMPOS.ALTA_BAJA = 'B' AND CAMPOS.FECHA_BAJA >='" & DtpFechaEdicion & "'))" &
                              " AND CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                Else
                    RsCampos.Open("SELECT *  " &
                              " FROM CAMPOS " &
                              " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                              " CAMPOS.ALTA_BAJA = 'A' AND " &
                              " CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                End If

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

                        ' Estudio de los campos SIG
                        sSqlCamposSIG = "SELECT Campos_SIG.Termino ,Campos_SIG.Poligono, Campos_SIG.Parcela, Campos_SIG.hectareas FROM Campos_SIG WHERE empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCamposSIG = sSqlCamposSIG + " and Codigo_Campo=" & RsCampos!Codigo_Campo
                        RsCamposSIG.Open(sSqlCamposSIG, ObjetoGlobal.cn)
                        nMax = RsCamposSIG.RecordCount

                        ' Estudio de los cultivos
                        sSqlCultivos = "SELECT Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones"
                        sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"

                        If ChIncluirCamposBaja.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Fecha_baja>='" & DtpFechaEdicion & "'))"
                        Else
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A' "
                        End If

                        If ChSoloCamposCitricos.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                        End If

                        sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                        RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                        If nMax < RsCultivos.RecordCount Then
                            nMax = RsCultivos.RecordCount
                        End If
                        nNumCultivos = RsCultivos.RecordCount
                        RsCultivos.Close()

                        If nNumCultivos = 0 Then
                            RsCamposSIG.Close()
                        End If

                        If nNumCultivos > 0 Then
                            ' Construcción de la cadena para evaluar cultivos SIG
                            sSqlCultivos = "SELECT Cultivos.Codigo_Variedad "
                            sSqlCultivos = sSqlCultivos + " FROM Cultivos WHERE empresa=cultivos_sig.empresa "
                            sSqlCultivos = sSqlCultivos + " AND ejercicio=Cultivos_sig.ejercicio "
                            sSqlCultivos = sSqlCultivos + " AND Codigo_Variedad=Cultivos_sig.Codigo_Variedad "
                            sSqlCultivos = sSqlCultivos + " AND codigo_campo=Cultivos_sig.codigo_campo "

                            If ChIncluirCamposBaja.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND (Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Fecha_baja>='" & DtpFechaEdicion & "'))"
                            Else
                                sSqlCultivos = sSqlCultivos + " AND Alta_Baja = 'A' "
                            End If

                            ' Contamos los cultivos SIG para este campo
                            sSqlCultSIG = "SELECT * FROM cultivos_sig WHERE "
                            sSqlCultSIG = sSqlCultSIG + " empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Campo=" & RsCampos!Codigo_Campo
                            sSqlCultSIG = sSqlCultSIG + " AND  Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                            If ChSoloCamposCitricos.Checked Then
                                sSqlCultSIG = sSqlCultSIG + " AND Codigo_Variedad BETWEEN '01' AND '02Z' "
                            End If
                            sSqlCultSIG = sSqlCultSIG + " AND EXISTS (" & sSqlCultivos & ")"

                            RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)

                            If nMax < RsCultSIG.RecordCount Then
                                nMax = RsCultSIG.RecordCount
                            End If

                            RsCultSIG.Close()

                            ' Dimensionamos el array
                            ReDim aDatos(22, nMax - 1)

                            ' Y por fin empezamos a incluir datos a la ficha
                            ' Datos generales de campos
                            aDatos(0, 0) = "" & Trim(RsCampos!Codigo_Campo)
                            aDatos(1, 0) = "" & DescripcionPartida
                            aDatos(6, 0) = "" & Format(RsCampos!FECHA_INSCRIPCION, "DD/MM/YY")
                            aDatos(7, 0) = "" & Format(RsCampos!FECHA_BAJA, "DD/MM/YY")
                            aDatos(15, 0) = "" & Trim(RsCampos!SECANO_REGADIO)
                            aDatos(16, 0) = "" & Trim(RsCampos!DOC_CATASTRAL)
                            i = 0
                            Do While Not RsCamposSIG.EOF
                                If i > UBound(aDatos, 2) Then
                                    ReDim Preserve aDatos(UBound(aDatos, 1), i)
                                End If
                                If Not IsDBNull(RsCamposSIG!termino) Then
                                    RsTerminos.Open("SELECT DESCRIPCION_TER FROM TERMINOS WHERE CODIGO_TERMINO = " & (RsCamposSIG!termino), ObjetoGlobal.cn)
                                    If RsTerminos.RecordCount > 0 Then
                                        DescripcionTermino = "" & Trim(RsTerminos!descripcion_ter)
                                    Else
                                        DescripcionTermino = ""
                                    End If
                                    RsTerminos.Close()
                                End If
                                aDatos(2, i) = "" & DescripcionTermino
                                aDatos(3, i) = "" & RsCamposSIG!POLIGONO
                                aDatos(4, i) = "" & RsCamposSIG!PARCELA
                                aDatos(5, i) = "" & RsCamposSIG!HECTAREAS
                                i = i + 1
                                RsCamposSIG.MoveNext()
                            Loop
                            RsCamposSIG.Close()

                            ' Ahora los cultivos
                            sSqlCultivos = "SELECT Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones, Cultivos.Kg_previstos, Cultivos.Recol_cs, Cultivos.Recol_ca, Cultivos.Kg_asegurados_cs, Cultivos.Kg_asegurados_ca, Cultivos.Kg_siguientes, Cultivos.Opcion_seguro_cs"
                            sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"

                            If ChIncluirCamposBaja.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Fecha_baja>='" & DtpFechaEdicion & "'))"
                            Else
                                sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A' "
                            End If

                            If ChSoloCamposCitricos.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                            End If

                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                            RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                            i = 0

                            While Not RsCultivos.EOF
                                If i > UBound(aDatos, 2) Then ReDim Preserve aDatos(UBound(aDatos, 1), i)
                                aDatos(8, i) = "" & RsCultivos!DESCRIPCION
                                '                      aDatos(13, i) = "" & RsCultivos!ARBOLES
                                '                      If Not IsdbNull(RsCultivos!FECHA_PLANTACION_1) Then
                                '                         aDatos(14, i) = "" & Year("" & RsCultivos!FECHA_PLANTACION_1)
                                '                      Else
                                '                         aDatos(14, i) = ""
                                '                      End If

                                If sTipo = "EFECTIVOS PRODUCTIVOS" Then
                                    If OpcCA.Checked = True Then
                                        aDatos(17, i) = "" & RsCultivos!Recol_ca
                                        aDatos(18, i) = "" & RsCultivos!Kg_previstos
                                    Else
                                        aDatos(17, i) = "" & RsCultivos!Recol_cs
                                        aDatos(18, i) = "" & RsCultivos!Kg_siguientes
                                    End If
                                Else
                                    If OpcCA.Checked = True Then
                                        aDatos(17, i) = "" & RsCultivos!Recol_ca
                                        aDatos(18, i) = "" & RsCultivos!Kg_asegurados_ca
                                    Else
                                        aDatos(17, i) = "" & RsCultivos!Recol_cs
                                        aDatos(18, i) = "" & RsCultivos!Kg_asegurados_cs
                                    End If
                                End If
                                aDatos(19, i) = "" & RsCultivos!Opcion_seguro_cs
                                aDatos(20, i) = "" & RsCultivos!Observaciones

                                sSqlCultSIG = "SELECT cultivos_SIG.Termino, Terminos.Descripcion_Ter,cultivos_SIG.Poligono, cultivos_SIG.Parcela, cultivos_SIG.hectareas, cultivos_SIG.arboles, cultivos_SIG.fecha_plantacion FROM cultivos_SIG, Terminos WHERE (terminos.Codigo_termino=cultivos_SIG.termino) "
                                sSqlCultSIG = sSqlCultSIG + " AND  empresa='" & Trim(TxtEmpresa.Text) & "'"
                                sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Campo=" & RsCampos!Codigo_Campo
                                sSqlCultSIG = sSqlCultSIG + " AND  Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                                sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Variedad='" & RsCultivos!CODIGO_VARIEDAD + "'"
                                RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)
                                bIncremento = False
                                Do While Not RsCultSIG.EOF
                                    If Not IsDBNull(RsCultSIG!termino) Then
                                        RsTerminos.Open("SELECT DESCRIPCION_TER FROM TERMINOS WHERE CODIGO_TERMINO = " & (RsCultSIG!termino), ObjetoGlobal.cn)
                                        If RsTerminos.RecordCount > 0 Then
                                            DescripcionTermino = "" & Trim(RsTerminos!descripcion_ter)
                                        Else
                                            DescripcionTermino = ""
                                        End If
                                        RsTerminos.Close()
                                    End If
                                    aDatos(9, i) = "" & Mid(DescripcionTermino, 1, 6)
                                    aDatos(10, i) = "" & RsCultSIG!POLIGONO
                                    aDatos(11, i) = "" & RsCultSIG!PARCELA
                                    aDatos(12, i) = "" & RsCultSIG!HECTAREAS
                                    aDatos(13, i) = "" & RsCultSIG!ARBOLES
                                    If Not IsDBNull(RsCultSIG!FECHA_PLANTACION) Then
                                        aDatos(14, i) = "" & RsCultSIG!FECHA_PLANTACION
                                    Else
                                        aDatos(14, i) = ""
                                    End If
                                    i = i + 1
                                    bIncremento = True
                                    RsCultSIG.MoveNext()
                                Loop
                                If Not bIncremento Then
                                    i = i + 1
                                End If
                                RsCultSIG.Close()
                                RsCultivos.MoveNext()
                            End While
                            RsCultivos.Close()

                            For i = 0 To UBound(aDatos, 2)

                                If lHayPendiente Then

                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 20).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 20).Range.End)

                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                    lHayPendiente = False
                                End If

                                Fila = MiWord.ActiveDocument.Tables(1).Rows.Count

                                For o = 1 To 21
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Shading.BackgroundPatternColor = IIf(Not bControlColor, Microsoft.Office.Interop.Word.WdColor.wdColorGray25, Microsoft.Office.Interop.Word.WdColor.wdColorWhite)
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Text = "" & aDatos(o - 1, i)
                                Next

                                If i < UBound(aDatos, 2) Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 20).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 20).Range.End)
                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                Else
                                    lHayPendiente = True
                                End If
                            Next

                            bControlColor = Not bControlColor
                        End If
                        RsCampos.MoveNext()
                    End While
                End If
                RsCampos.Close()

                ' Por fin empezamos a pintar los campos
                ' Ya esta
                ' Y ahora imprimimos este socio
                If ImpresoraAct <> "" Then
                    MiWord.ActivePrinter = ImpresoraAct
                End If

                MiWord.PrintOut(FileName:="", Range:=0, Item:=0, Copies:=1, Pages:="", PageType:=0, Collate:=True, Background:=True, PrintToFile:=False, PrintZoomColumn:=0, PrintZoomRow:=0, PrintZoomPaperWidth:=0, PrintZoomPaperHeight:=0)

                For i = 1 To MiWord.Documents.Count
                    MiWord.Documents(1).Saved = True
                    MiWord.Documents(1).Close()
                Next

                RsSocios.MoveNext() ' al socio siguiente pasamos

            Loop
        End If
        RsSocios.Close()
        frCartel.Visible = False

        Exit Sub

    End Sub

    Private Sub ImprimeFichaSeguro(Plantilla)
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nNumCultivos As Integer
        Dim RsCamposSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartidas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSqlCultivos As String
        Dim sSqlCultSIG As String
        Dim nCont As Integer
        Dim aDatos(,) As String
        Dim RsTerminos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionTermino As String
        Dim DescripcionPartida As String
        Dim i, o As Integer
        Dim NumeroOpa As Integer
        Dim nMax As Integer
        Dim Fila As Integer
        Dim bControlColor As Boolean
        Dim NombreSocio As String
        Dim sSqlSocios As String
        Dim lHayPendiente As Boolean
        Dim nCult As Integer
        Dim bIncremento As Boolean
        Dim bSoloCitricos As Boolean

        lHayPendiente = False
        '    PctProgreso.Visible = False
        '    PctProgreso.Refresh

        bSoloCitricos = ChSoloCamposCitricos.Checked

        '    PrbProgreso.Min = 0
        '    PrbProgreso.Max = 10
        '    PrbProgreso.Value = 1
        '    PrbProgreso.Refresh

        '  On Error GoTo ERROR:
        If MiWord Is Nothing Then
            MiWord = New Microsoft.Office.Interop.Word.Application
            MiWord.Visible = False
            bEsMiWord = True
        End If

        sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <=" & Trim(TxtHastaSocio.Text)

        RsSocios.Open(sSqlSocios, ObjetoGlobal.cn)

        If RsSocios.RecordCount > 0 Then

            frCartel.Visible = True

            Do While Not RsSocios.EOF
                'PrbProgreso.Value = RsSocios.AbsolutePosition
                'PrbProgreso.Refresh

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
                NumeroOpa = DameNumeroOpa()
                MiWord.ActiveDocument.FormFields("CodigoOPA").Result = "" & Trim(RsSocios("num_agrup_" & NumeroOpa))
                If Not bSoloCitricos Then
                    RsCampos.Open("SELECT *  " &
                           " FROM CAMPOS " &
                           " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                           " (( CAMPOS.ALTA_BAJA = 'B' AND CAMPOS.FECHA_BAJA >='" & DtpFechaEdicion & "') OR ( CAMPOS.ALTA_BAJA = 'A' ) )" &
                           " AND CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                Else
                    RsCampos.Open("SELECT *  " &
                           " FROM CAMPOS " &
                           " WHERE EMPRESA = '" & Trim(TxtEmpresa.Text) & "' AND " &
                           " CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                           " CAMPOS.ALTA_BAJA = 'A' AND " &
                           " EXISTS (SELECT CODIGO_CAMPO FROM CULTIVOS WHERE " &
                           " CULTIVOS.EMPRESA = CAMPOS.EMPRESA AND " &
                           " CULTIVOS.EJERCICIO = '" & Trim(ObjetoGlobal.EjercicioActual) & "' AND " &
                           " CAMPOS.CODIGO_CAMPO = CULTIVOS.CODIGO_CAMPO AND " &
                           " CULTIVOS.CODIGO_VARIEDAD BETWEEN '01' AND '02Z' AND CULTIVOS.ALTA_BAJA = 'A') " _
                           , ObjetoGlobal.cn)
                End If
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
                        'sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
                        If ChIncluirCamposBaja.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Fecha_Baja>='" & DtpFechaEdicion & "'))"
                        End If

                        If ChSoloCamposCitricos.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                        End If

                        sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                        RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                        If nMax < RsCultivos.RecordCount Then
                            nMax = RsCultivos.RecordCount
                        End If
                        RsCultivos.Close()

                        ' Contamos los cultivos SIG para este campo
                        sSqlCultSIG = "SELECT * FROM cultivos_sig, Cultivos WHERE "
                        sSqlCultSIG = sSqlCultSIG + " cultivos_sig.empresa=cultivos.empresa "
                        sSqlCultSIG = sSqlCultSIG + " AND cultivos_sig.ejercicio=cultivos.ejercicio "
                        sSqlCultSIG = sSqlCultSIG + " AND cultivos_sig.codigo_campo=cultivos.codigo_campo "
                        sSqlCultSIG = sSqlCultSIG + " AND cultivos_sig.codigo_variedad=cultivos.codigo_variedad "
                        If bSoloCitricos Then
                            sSqlCultSIG = sSqlCultSIG + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                        End If
                        sSqlCultSIG = sSqlCultSIG + " AND cultivos.Alta_baja='A' "
                        sSqlCultSIG = sSqlCultSIG + " AND cultivos_sig.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultSIG = sSqlCultSIG + " AND  cultivos_sig.Codigo_Campo=" & RsCampos!Codigo_Campo
                        sSqlCultSIG = sSqlCultSIG + " AND  cultivos_sig.Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                        RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)

                        If nMax < RsCultSIG.RecordCount Then
                            nMax = RsCultSIG.RecordCount
                        End If

                        RsCultSIG.Close()

                        ' Dimensionamos el array
                        ReDim aDatos(22, nMax - 1)

                        ' Y por fin empezamos a incluir datos a la ficha
                        ' Datos generales de campos
                        aDatos(0, 0) = "" & Trim(RsCampos!Codigo_Campo)
                        aDatos(1, 0) = "" & DescripcionPartida
                        aDatos(9, 0) = "" & Trim(RsCampos!SECANO_REGADIO)
                        aDatos(10, 0) = "" & Trim(RsCampos!DOC_CATASTRAL)
                        i = 0

                        ' Ahora los cultivos
                        sSqlCultivos = "SELECT Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones, Cultivos.Zona_seguro, Cultivos.Zona_oficial, Cultivos.Opcion_seguro_cs, Cultivos.Kg_estimados_cs"
                        sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"

                        If bSoloCitricos Then
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                        End If

                        sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                        RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                        i = 0

                        While Not RsCultivos.EOF
                            If i > UBound(aDatos, 2) Then ReDim Preserve aDatos(UBound(aDatos, 1), i)
                            aDatos(2, i) = "" & RsCultivos!DESCRIPCION
                            'aDatos(7, i) = "" & IIf(IsdbNull(RsCultivos!ARBOLES), 0, RsCultivos!ARBOLES)
                            aDatos(11, i) = "" & RsCultivos!zona_seguro
                            aDatos(12, i) = "" & RsCultivos!Zona_oficial
                            aDatos(13, i) = "" & RsCultivos!Opcion_seguro_cs
                            aDatos(14, i) = "" & IIf(IsDBNull(RsCultivos!Kg_estimados_cs), 0, RsCultivos!Kg_estimados_cs)
                            'If Not IsdbNull(RsCultivos!FECHA_PLANTACION_1) Then
                            '   aDatos(8, i) = "" & Year("" & RsCultivos!FECHA_PLANTACION_1)
                            'Else
                            '   aDatos(8, i) = ""
                            'End If

                            sSqlCultSIG = "SELECT cultivos_SIG.Termino, Terminos.Descripcion_Ter,cultivos_SIG.Poligono, cultivos_SIG.Parcela, cultivos_SIG.hectareas, cultivos_SIG.arboles, cultivos_SIG.fecha_plantacion FROM cultivos_SIG, Terminos WHERE (terminos.Codigo_termino=cultivos_SIG.termino) "
                            sSqlCultSIG = sSqlCultSIG + " AND  empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Campo=" & RsCampos!Codigo_Campo
                            sSqlCultSIG = sSqlCultSIG + " AND  Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                            sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Variedad='" & RsCultivos!CODIGO_VARIEDAD + "'"
                            RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)
                            bIncremento = False
                            Do While Not RsCultSIG.EOF
                                If Not IsDBNull(RsCultSIG!termino) Then
                                    RsTerminos.Open("SELECT DESCRIPCION_TER FROM TERMINOS WHERE CODIGO_TERMINO = " & (RsCultSIG!termino), ObjetoGlobal.cn)
                                    If RsTerminos.RecordCount > 0 Then
                                        DescripcionTermino = "" & Trim(RsTerminos!descripcion_ter)
                                    Else
                                        DescripcionTermino = ""
                                    End If
                                    RsTerminos.Close()
                                End If
                                aDatos(3, i) = "" & DescripcionTermino
                                aDatos(4, i) = "" & RsCultSIG!POLIGONO
                                aDatos(5, i) = "" & RsCultSIG!PARCELA
                                aDatos(6, i) = "" & RsCultSIG!HECTAREAS
                                aDatos(7, i) = "" & IIf(IsDBNull(RsCultSIG!ARBOLES), 0, RsCultSIG!ARBOLES)
                                If Not IsDBNull(RsCultSIG!FECHA_PLANTACION) Then
                                    aDatos(8, i) = "" & RsCultSIG!FECHA_PLANTACION
                                Else
                                    aDatos(8, i) = ""
                                End If
                                i = i + 1
                                bIncremento = True
                                RsCultSIG.MoveNext()
                            Loop
                            If Not bIncremento Then
                                i = i + 1
                            End If
                            RsCultSIG.Close()
                            RsCultivos.MoveNext()
                        End While
                        RsCultivos.Close()

                        For i = 0 To UBound(aDatos, 2)

                            If lHayPendiente Then
                                MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 15).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 15).Range.End)
                                MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                lHayPendiente = False
                            End If

                            Fila = MiWord.ActiveDocument.Tables(1).Rows.Count

                            For o = 1 To 15
                                MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Shading.BackgroundPatternColor = IIf(Not bControlColor, Microsoft.Office.Interop.Word.WdColor.wdColorGray25, Microsoft.Office.Interop.Word.WdColor.wdColorWhite)
                                MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Text = "" & aDatos(o - 1, i)
                            Next

                            If i < UBound(aDatos, 2) Then
                                MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 15).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 15).Range.End)
                                MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                            Else
                                lHayPendiente = True
                            End If

                        Next
                        bControlColor = Not bControlColor
                        RsCampos.MoveNext()
                    End While
                End If
                RsCampos.Close()
                ' Por fin empezamos a pintar los campos
                ' Ya esta
                ' Y ahora imprimimos este socio
                '          If ImpresoraAct <> "" Then
                '             MiWord.ActivePrinter = ImpresoraAct
                '          End If
                If Impresora.Checked Then
                    MiWord.PrintOut(FileName:="", Range:=0, Item:=
                                    0, Copies:=1, Pages:="", PageType:=0,
                                    Collate:=True, Background:=True, PrintToFile:=False, PrintZoomColumn:=0,
                                    PrintZoomRow:=0, PrintZoomPaperWidth:=0, PrintZoomPaperHeight:=0)
                End If
                For i = 1 To MiWord.Documents.Count
                    MiWord.Documents(1).Saved = True
                    If Impresora.Checked Then
                        MiWord.Documents(1).Close()
                    End If
                Next

                RsSocios.MoveNext() ' al socio siguiente pasamos

            Loop
        End If

        frCartel.Visible = False
        RsSocios.Close()
        Exit Sub

        '    Error

        'MsgBox "Se produjo en error a la hora de traspasar los datos a la plantilla solicitada." & vbCrLf &
        '    "Compruebe que la plantilla introducida es la correcta", vbInformation, Me.Caption
        'Resume
        'Set MiWord = Nothing
        ''PctProgreso.Visible = False

        ''CONTROL DE ERRORES
        'Exit Sub
        '    Resume

    End Sub


    Private Function DirectorioMisDocumentos() As String
        Dim XRuta As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        On Error Resume Next

        XRuta = Strings.Left(XRuta, InStr(XRuta, Chr(0)) - 1)
        If Len(Trim(XRuta)) < 3 Then XRuta = vbNullString
        Return XRuta

        '        String PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        'Ese parametro "Personal" te devuelve la ruta de "Mis documentos"
        'Por si te interesa te dejo otros parametros por si necesitas saber en un futuro otras rutas
        '        Environment.SpecialFolder.ApplicationData
        '        Environment.SpecialFolder.System
        '        Environment.SpecialFolder.CommonApplicationData
        '        Environment.SpecialFolder.CommonProgramFiles
        '        Environment.SpecialFolder.Cookies
        '        Environment.SpecialFolder.Desktop
        '        Environment.SpecialFolder.DesktopDirectory
        '        Environment.SpecialFolder.Favorites
        '        Environment.SpecialFolder.History
        '        Environment.SpecialFolder.InternetCache
        '        Environment.SpecialFolder.LocalApplicationData
        '        Environment.SpecialFolder.MyComputer
        '        Environment.SpecialFolder.MyMusic
        '        Environment.SpecialFolder.MyPictures
        '        Environment.SpecialFolder.Personal
        '        Environment.SpecialFolder.ProgramFiles
        '        Environment.SpecialFolder.Programs
        '        Environment.SpecialFolder.Recent
        '        Environment.SpecialFolder.SendTo
        '        Environment.SpecialFolder.StartMenu

    End Function

    Private Sub CmdImprimirFichas(Optional ByVal Plantilla As String = "")

        If Plantilla = "" Then

        End If

        If OpcNormal.Checked = True Then
            ImprimeFichas(Plantilla)
        ElseIf OpcDeclaracion.Checked = True Then
            ImprimeDeclaracion(Plantilla, "EFECTIVOS PRODUCTIVOS")
        Else
            ImprimeDeclaracion(Plantilla, "SEGURO")
        End If
    End Sub
    Public Function TipoFicha(sTipo)
        Select Case sTipo
            Case "F" ' tipo ficha
                OpcNormal.Checked = True
                TxtPlantilla.Text = sPlantillaF
            Case "D" ' tipo ficha
                OpcDeclaracion.Checked = True
                TxtPlantilla.Text = sPlantillaD
            Case Else
                OpcFichaSeguro.Checked = True
                TxtPlantilla.Text = sPlantillaS
        End Select

    End Function
    Public Sub DesHabilitar()
        FrmTiposFicha.Enabled = False
        FrmDatos.Enabled = False
    End Sub

    Public Sub RangoSocios(sDesdeSocio, Optional sHastaSocio = 0)

        TxtCodigoSocio.Text = sDesdeSocio
        ExisteSocio(TxtCodigoSocio.Text, lblNombreSocio.Text)

        If sHastaSocio = 0 Then
            TxtHastaSocio.Text = sDesdeSocio
            ExisteSocio(TxtCodigoSocio.Text, lblHastaNombresocio.Text)
        Else
            TxtHastaSocio.Text = sHastaSocio
            ExisteSocio(TxtHastaSocio.Text, lblHastaNombresocio.Text)
        End If

    End Sub
    Public Function SoloCitricos(bSoloCitricos As Boolean)
        ChSoloCamposCitricos.Checked = IIf(bSoloCitricos, 1, 0)
    End Function
    Public Function CamposBaja(bCamposBaja As Boolean)
        ChIncluirCamposBaja.Checked = IIf(bCamposBaja, 1, 0)
    End Function
    Public Function CamposDocumentados(bCamposDocumentados As Boolean)
        ChkAlta.Checked = bCamposDocumentados
    End Function

    Private Sub ImprimeDeclaracion(Plantilla, sTipo)
        Dim RsCampos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim nNumCultivos As Integer
        Dim RsCamposSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsSocios As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultSIG As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPartidas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSqlCamposSIG As String
        Dim sSqlCultivos As String
        Dim sSqlCultSIG As String
        Dim nCont As Integer
        Dim aDatos(,) As String
        Dim RsTerminos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim DescripcionTermino As String
        Dim DescripcionPartida As String
        Dim i, o As Integer
        Dim NumeroOpa As Integer
        Dim nMax As Integer
        Dim Fila As Integer
        Dim bControlColor As Boolean
        Dim NombreSocio As String
        Dim sSqlSocios As String
        Dim lHayPendiente As Boolean
        Dim nCult As Integer
        Dim bIncremento As Boolean

        On Error Resume Next
        lHayPendiente = False
        frCartel.Visible = False
        frCartel.Refresh()

        PrbProgreso.Minimum = 0
        PrbProgreso.Maximum = 10
        PrbProgreso.Value = 1
        PrbProgreso.Refresh()

        '  On Error GoTo ERROR:
        If MiWord Is Nothing Or Not bEsMiWord Then
            MiWord = New Microsoft.Office.Interop.Word.Application
            MiWord.Visible = False
            bEsMiWord = True
        End If

        If Not ChkAlta.Checked Then
            sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio.Text) ' & " AND ALTA_BAJA = 'A' "
        Else
            sSqlSocios = "SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO >= " & Trim(TxtCodigoSocio.Text) & " AND CODIGO_SOCIO <= " & Trim(TxtHastaSocio.Text) & " AND "
            sSqlSocios = sSqlSocios + " Exists (SELECT codigo_socio From campos WHERE campos.empresa = '" + Trim(TxtEmpresa.Text) + "' AND campos.codigo_socio = socios_coop.codigo_socio AND campos.alta_baja = 'A' AND CAMPOS.DOC_CATASTRAL <> 'S')"
        End If

        RsSocios.Open(sSqlSocios, ObjetoGlobal.cn)

        If RsSocios.RecordCount > 0 Then

            PrbProgreso.Maximum = RsSocios.RecordCount
            frCartel.Visible = True

            While Not RsSocios.EOF
                PrbProgreso.Value = RsSocios.AbsolutePosition
                PrbProgreso.Refresh()

                lHayPendiente = False


                MiWord.Documents.Add(CStr(Plantilla))

                If Trim(sTipo) = "SEGURO" Then
                    MiWord.ActiveDocument.FormFields("declaracion").Result = "SEGURO"
                Else
                    MiWord.ActiveDocument.FormFields("declaracion").Result = "EFECTIVOS PRODUCTIVOS"
                End If

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

                If ChIncluirCamposBaja.Checked Then
                    RsCampos.Open("SELECT *  " &
                            " FROM CAMPOS " &
                            " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                            " (CAMPOS.ALTA_BAJA = 'A' OR (CAMPOS.ALTA_BAJA = 'B' AND CAMPOS.FECHA_BAJA >='" & DtpFechaEdicion & "'))" &
                            " AND CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                Else
                    RsCampos.Open("SELECT *  " &
                            " FROM CAMPOS " &
                            " WHERE CAMPOS.CODIGO_SOCIO = " & (RsSocios!CODIGO_SOCIO) & " AND " &
                            " CAMPOS.ALTA_BAJA = 'A' AND " &
                            " CAMPOS.EMPRESA = '" & Trim(TxtEmpresa.Text) & "'", ObjetoGlobal.cn)
                End If

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

                        ' Estudio de los campos SIG
                        'sSqlCamposSIG = "SELECT Campos_SIG.Termino ,Campos_SIG.Poligono, Campos_SIG.Parcela, Campos_SIG.hectareas FROM Campos_SIG WHERE empresa='" & Trim(TxtEmpresa.Text) & "'"
                        'sSqlCamposSIG = sSqlCamposSIG + " and Codigo_Campo=" & RsCampos!Codigo_Campo
                        'RsCamposSIG.Open(sSqlCamposSIG, ObjetoGlobal.Cn)
                        'nMax = RsCamposSIG.RecordCount
                        nMax = 1

                        ' Estudio de los cultivos
                        sSqlCultivos = "SELECT Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones"
                        sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                        sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"

                        If ChIncluirCamposBaja.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Cultivos.Fecha_baja>='" & DtpFechaEdicion & "'))"
                        Else
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
                        End If

                        If ChSoloCamposCitricos.Checked Then
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                        End If

                        'sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
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
                            sSqlCultivos = "SELECT Cultivos.Recol_ca , Cultivos.Kg_previstos, Cultivos.Recol_cs, Cultivos.Kg_siguientes, Cultivos.Kg_asegurados_ca, Cultivos.Kg_asegurados_cs, Cultivos.Opcion_seguro_cs, Cultivos.Codigo_Variedad, Variedades.Descripcion, Cultivos.hectareas, Cultivos.fecha_plantacion_1, Cultivos.arboles, Cultivos.observaciones"
                            sSqlCultivos = sSqlCultivos + " FROM Cultivos, Variedades WHERE (Cultivos.Codigo_Variedad=Variedades.Codigo_Variedad) and Cultivos.empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultivos = sSqlCultivos + " AND Variedades.empresa='" & Trim(TxtEmpresa.Text) & "'"
                            sSqlCultivos = sSqlCultivos + " AND Cultivos.ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) & "'"

                            If ChIncluirCamposBaja.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND (Cultivos.Alta_Baja = 'A' OR (Cultivos.Alta_Baja = 'B' AND Cultivos.Fecha_baja>='" & DtpFechaEdicion & "'))"
                            Else
                                sSqlCultivos = sSqlCultivos + " AND Cultivos.Alta_Baja = 'A'"
                            End If

                            If ChSoloCamposCitricos.Checked Then
                                sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Variedad BETWEEN '01' AND '02Z' "
                            End If

                            sSqlCultivos = sSqlCultivos + " AND Cultivos.Codigo_Campo = " & RsCampos!Codigo_Campo
                            RsCultivos.Open(sSqlCultivos, ObjetoGlobal.cn)

                            i = 0

                            Do While Not RsCultivos.EOF
                                If i > UBound(aDatos, 2) Then ReDim Preserve aDatos(UBound(aDatos, 1), i)
                                aDatos(4, i) = "" & RsCultivos!DESCRIPCION
                                '                      aDatos(14, i) = "" & RsCultivos!ARBOLES
                                '
                                '                      If Not IsdbNull(RsCultivos!FECHA_PLANTACION_1) Then
                                '                         aDatos(15, i) = "" & Year("" & RsCultivos!FECHA_PLANTACION_1)
                                '                      Else
                                '                         aDatos(15, i) = ""
                                '                      End If

                                If sTipo = "EFECTIVOS PRODUCTIVOS" Then
                                    If OpcCA.Checked = True Then
                                        aDatos(18, i) = "" & RsCultivos!Recol_ca
                                        aDatos(19, i) = "" & RsCultivos!Kg_previstos
                                    Else
                                        aDatos(18, i) = "" & RsCultivos!Recol_cs
                                        aDatos(19, i) = "" & RsCultivos!Kg_siguientes
                                    End If
                                Else
                                    If OpcCA.Checked = True Then
                                        aDatos(18, i) = "" & RsCultivos!Recol_ca
                                        aDatos(19, i) = "" & RsCultivos!Kg_asegurados_ca
                                    Else
                                        aDatos(18, i) = "" & RsCultivos!Recol_cs
                                        aDatos(19, i) = "" & RsCultivos!Kg_asegurados_cs
                                    End If
                                End If

                                aDatos(20, i) = "" & RsCultivos!Opcion_seguro_cs
                                aDatos(21, i) = "" & RsCultivos!Observaciones

                                sSqlCultSIG = "SELECT cultivos_SIG.Termino, cultivos_SIG.recinto, cultivos_SIG.hectareas_sigpac, cultivos_SIG.uso_sigpac, Terminos.Descripcion_Ter,cultivos_SIG.Poligono, cultivos_SIG.Parcela, cultivos_SIG.hectareas, cultivos_SIG.arboles, cultivos_SIG.fecha_plantacion FROM cultivos_SIG, Terminos WHERE (terminos.Codigo_termino=cultivos_SIG.termino) "
                                sSqlCultSIG = sSqlCultSIG + " AND  empresa='" & Trim(TxtEmpresa.Text) & "'"
                                sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Campo=" & RsCampos!Codigo_Campo
                                sSqlCultSIG = sSqlCultSIG + " AND  Ejercicio='" & Trim(ObjetoGlobal.EjercicioActual) + "'"
                                sSqlCultSIG = sSqlCultSIG + " AND  Codigo_Variedad='" & RsCultivos!CODIGO_VARIEDAD + "'"
                                RsCultSIG.Open(sSqlCultSIG, ObjetoGlobal.cn)
                                bIncremento = False

                                Do While Not RsCultSIG.EOF
                                    If i > UBound(aDatos, 2) Then
                                        ReDim Preserve aDatos(UBound(aDatos, 1), i)
                                    End If
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
                                    aDatos(14, i) = "" & RsCultSIG!ARBOLES
                                    If Not IsDBNull(RsCultSIG!FECHA_PLANTACION) Then
                                        aDatos(15, i) = "" & RsCultSIG!FECHA_PLANTACION
                                    Else
                                        aDatos(15, i) = ""
                                    End If
                                    i = i + 1
                                    bIncremento = True
                                    RsCultSIG.MoveNext()
                                Loop
                                If Not bIncremento Then
                                    i = i + 1
                                End If
                                RsCultSIG.Close()
                                RsCultivos.MoveNext()
                            Loop ' DE LOS CULTIVOS
                            RsCultivos.Close()

                            For i = 0 To UBound(aDatos, 2)

                                If lHayPendiente Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 22).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 22).Range.End)
                                    MiWord.Selection.MoveRight(Unit:=WdUnits.wdCell, Count:=1)
                                    lHayPendiente = False
                                End If

                                Fila = MiWord.ActiveDocument.Tables(1).Rows.Count

                                For o = 1 To 22
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Shading.BackgroundPatternColor = IIf(Not bControlColor, Microsoft.Office.Interop.Word.WdColor.wdColorGray25, Microsoft.Office.Interop.Word.WdColor.wdColorWhite)
                                    MiWord.ActiveDocument.Tables(1).Cell(Fila, o).Range.Text = "" & aDatos(o - 1, i)
                                Next

                                If i < UBound(aDatos, 2) Then
                                    MiWord.Selection.SetRange(MiWord.ActiveDocument.Tables(1).Cell(Fila, 22).Range.End, MiWord.ActiveDocument.Tables(1).Cell(Fila, 22).Range.End)
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
                If Impresora.Checked Then
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
                Else
                    MiWord.Visible = False
                    bEsMiWord = False
                End If
                RsSocios.MoveNext() ' al socio siguiente pasamos
            End While
        End If
        RsSocios.Close()
        frCartel.Visible = False
        On Error GoTo 0
        Return

    End Sub
    Public WriteOnly Property CopiasFicha(ByVal n As Integer) As Integer

        Set(value As Integer)
            TxtCopias.Text = CStr(value)
            Copias = value
        End Set

    End Property

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
    Public Function DelSocio(ByVal CodSocio As Integer, ByVal nombreSocio As String)
        TxtCodigoSocio.Text = CStr(CodSocio)
        lblNombreSocio.Text = nombreSocio
        TxtHastaSocio.Text = CStr(CodSocio)
        lblHastaNombresocio.Text = nombreSocio
        DatosExternos = True
    End Function
    Public Function NumeroCopias(n)
        Copias = n
        TxtCopias.Text = CStr(n)
    End Function

    Private Sub FrmEdicionFicha_Load(sender As Object, e As EventArgs) Handles Me.Load
        InicializarPantalla()
    End Sub
End Class