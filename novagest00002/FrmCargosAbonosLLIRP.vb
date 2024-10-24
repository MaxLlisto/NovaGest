Imports System.IO
Imports System
Imports System.Text
Imports System.Xml
Imports System.ComponentModel
Imports cnRecordset
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
'Imports System.Net.WebRequestMethods
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing

Public Class FrmCargosAbonosLLIRP
    Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim Cuantos1 As Long
    Dim Cuantos2 As Long
    Dim DiscAbonos(100) As String
    Dim DiscCargos(100) As String
    Dim CuantosDisco(100) As Integer
    Dim EntradasDisco(100) As Integer
    Dim EntradasDiscoOper(100) As Integer
    Dim ImporteDisco(100) As Double
    Dim ProcesoActual As String
    Dim NumDiscAbonos As Integer = 0
    Dim NumDiscCargos As Integer = 0
    Dim strStreamW As FileStream
    Dim strStreamWriter As StreamWriter
    Dim NombreArchivo As String
    'Dim RsTemp As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

    Private Function hacerfichero(ByVal fich As String) As Boolean
        Dim strStreamW As Stream
        Dim result = "c:\ficherosxml\" 'Path.GetTempPath()
        Dim PathFile As String = result + fich

        If System.IO.File.Exists(PathFile) Then
            File.Delete(PathFile)
        End If

        Try
            'Se abre el archivo y si este no existe se crea
            strStreamW = File.OpenWrite(PathFile)
            strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.UTF8)
            NombreArchivo = PathFile
            Return True

        Catch ex As Exception
            MsgBox("Imposible abrir el archivo de los registros")
            Return False
        End Try


    End Function

    Private Function GrabarTextoArchivo(ByVal cArg As String) As Boolean
        Try
            strStreamWriter.Write(cArg)
            strStreamWriter.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property


    Private Sub CmdGenerarsepa_Click(sender As Object, e As EventArgs) Handles CmdGenerarsepa.Click
        Dim i As Integer
        Dim Contador As Integer
        Dim Entrar As Boolean
        Dim Cuantos As Long
        Dim RsTemp As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim trans As SqlClient.SqlTransaction

        Cuantos1 = CLng("0" + TxtCuantosAbonos.Text)
        Cuantos2 = CLng("0" + TxtCuantosCargos.Text)

        Try

            ObjetoGlobal.cn.Open()
            trans = ObjetoGlobal.cn.BeginTransaction()


            If Cuantos1 + Cuantos2 = 0 Then
                MsgBox("No existen cargos o abonos para grabar")
                Exit Sub
            End If

            If Cuantos1 > 0 And Trim(TxtCuenta1.Text) = "" Then
                MsgBox("Debe indicar cuenta para fichero de abonos")
                TxtCuenta1.Focus()
                Exit Sub
            End If
            If Cuantos2 > 0 And Trim(TxtCuenta2.Text) = "" Then
                MsgBox("Debe indicar cuenta para fichero de cargos")
                TxtCuenta2.Focus()
                Exit Sub
            End If
            If Cuantos1 > 0 And Trim(TxtFicheroAbonos.Text) = "" Then
                MsgBox("Debe indicar nombre de fichero para grabar abonos.")
                TxtFicheroAbonos.Focus()
                Exit Sub
            End If
            If Cuantos2 > 0 And Trim(TxtFicheroCargos.Text) = "" Then
                MsgBox("Debe indicar nombre de fichero para grabar cargos.")
                TxtFicheroCargos.Focus()
                Exit Sub
            End If
            If Trim(TxtFicheroAbonos.Text) = Trim(TxtFicheroCargos.Text) Then
                MsgBox("Debe indicar nombres diferentes para los ficheros de cargos y abonos.")
                TxtFicheroAbonos.Focus()
                Exit Sub
            End If

            Cuantos = RsTemp.RecordCount
            HabilitarProgreso("Se genera fichero cargos/abonos:", Cuantos + 4)
            If Not AbrirFicheros(TxtFicheroAbonos.Text, TxtFicheroCargos.Text) Then
                Exit Sub
            End If

            For i = 1 To 100
                CuantosDisco(i) = 0
                EntradasDisco(i) = 0
                EntradasDiscoOper(i) = 0
                ImporteDisco(i) = 0
            Next

            'Print #101, "Inicio proceso grabación remesas " & ProcesoActual & " " & Date & " hora " & Time

            If Cuantos1 > 0 Or Cuantos2 > 0 Then
                If Cuantos1 > 0 Then ' Tiene abonos
                    Dim SEPAXML34 = New SEPA34
                    SEPAXML34.ObjetoGlobal = Me.ObjetoGlobal
                    SEPAXML34.DiscoAbonos(DiscAbonos)
                    SEPAXML34.cuenta = TxtCuenta1.Text
                    SEPAXML34.NIF = TxtNifAbono.Text
                    SEPAXML34.nombre = TxtNombreAbono.Text
                    SEPAXML34.domicilio = TxtDomicilioAbono.Text
                    SEPAXML34.Poblacion = TxtPoblacionAbono.Text
                    SEPAXML34.entidad = TxtEntidad1.Text
                    SEPAXML34.oficina = TxtOficina1.Text
                    SEPAXML34.dc = TxtDC1.Text
                    SEPAXML34.codcuenta = TxtCodCuenta1.Text
                    SEPAXML34.Path = TxtFicheroAbonos.Text
                    SEPAXML34.Cuantos = CLng(TxtCuantosAbonos.Text)
                    SEPAXML34.NumDisc = NumDiscAbonos
                    SEPAXML34.lvAbonos = lvabonos
                    SEPAXML34.FechaRemesa = DTFechaEmision.Value
                    SEPAXML34.FechaAbono = DTFechaEnvio.Value
                    SEPAXML34.lbl = lblProgreso
                    SEPAXML34.PB = pb
                    SEPAXML34.trans = trans
                    SEPAXML34.HacerRemesa()
                End If
                If Cuantos2 > 0 Then ' Tiene cargos
                    Dim SepaXML19 = New SEPA19
                    SepaXML19.ObjetoGlobal = Me.ObjetoGlobal
                    SepaXML19.DiscoCargos(DiscCargos)
                    SepaXML19.cuenta = TxtCuenta2.Text
                    SepaXML19.NIF = TxtNifCargo.Text
                    SepaXML19.nombre = TxtNombreCargo.Text
                    SepaXML19.domicilio = TxtDomicilioCargo.Text
                    SepaXML19.Poblacion = TxtPoblacionCargo.Text
                    SepaXML19.entidad = TxtEntidad2.Text
                    SepaXML19.oficina = TxtOficina2.Text
                    SepaXML19.dc = TxtDC2.Text
                    SepaXML19.codcuenta = TxtCodCuenta2.Text
                    SepaXML19.Path = TxtFicheroCargos.Text
                    SepaXML19.Cuantos = CLng(TxtCuantosCargos.Text)
                    SepaXML19.NumDisc = NumDiscCargos
                    SepaXML19.LvCargos = LvCargos
                    SepaXML19.FechaRemesa = DTFechaEmision.Value
                    SepaXML19.FechaCargo = DTFechaEnvio.Value
                    SepaXML19.lbl = lblProgreso
                    SepaXML19.PB = pb
                    SepaXML19.trans = trans
                    SepaXML19.HacerRemesa()
                End If
            End If

            CerrarTodos(100)
            'Print #101, "Concluido correctamente proceso grabación remesas " & ProcesoActual & " " & Date & " hora " & Time
            MsgBox("El proceso de grabación ha finalizado correctamente")
            DesHabilitarProgreso()

            trans.Commit()
            ObjetoGlobal.cn.Close()


        Catch ex As Exception
            trans.Rollback()
            ObjetoGlobal.cn.Close()

        End Try

    End Sub

    Private Function AbrirFicheros(Fichero1 As String, Fichero2 As String) As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim maximo As Integer
        Dim Cadena As String
        Dim Cadena2 As String
        Dim Cadena3 As String

        Try

            Cuantos1 = CLng(TxtCuantosAbonos.Text)
            Cuantos2 = CLng(TxtCuantosCargos.Text)
            maximo = 0
            If Cuantos1 > 0 Then
                j = InStr(1, Trim(Fichero1), ".")
                If j > 0 Then
                    If j = 1 Then Cadena = "" Else Cadena = Left(Trim(Fichero1), j - 1)
                    Cadena2 = Mid(Trim(Fichero1), j)
                Else
                    Cadena = Trim(Fichero1)
                    Cadena2 = ""
                End If
                For i = 1 To NumDiscAbonos
                    Cadena3 = Trim(Cadena) & "_" & i & Trim(Cadena2)
                    FileOpen(i, Trim(Cadena3) & ".xml", OpenMode.Output)
                    maximo = i
                Next
            End If
            If Cuantos2 > 0 Then
                j = InStr(1, Trim(Fichero2), ".")
                If j > 0 Then
                    If j = 1 Then Cadena = "" Else Cadena = Left(Trim(Fichero2), j - 1)
                    Cadena2 = Mid(Trim(Fichero2), j)
                Else
                    Cadena = Trim(Fichero2)
                    Cadena2 = ""
                End If
                For i = 1 To NumDiscCargos
                    Cadena3 = Trim(Cadena) & "_" & i & Trim(Cadena2)
                    FileOpen(i + 50, Trim(Cadena3) & ".xml", OpenMode.Output)
                    maximo = 50 + i
                Next
            End If
            Return True
            Exit Function
        Catch ex As Exception
            MsgBox "Error en la apertura de fichero:" + vbCrLf + Err.Description + "Se interrumpe el proceso de grabación"
            If maximo > 0 Then
                CerrarTodos(maximo)
            End If
        End Try
    End Function

    Private Sub CerrarTodos(no)
        On Error GoTo ErrorHandler
        Dim i As Integer

        For i = 1 To Max
            FileClose(i)
        Next

ErrorHandler:
        Resume Next
    End Sub

    Private Sub habilitarProgreso()
        pb.Visible = True
    End Sub

    Private Sub CmdImprimirAbonos_Click(sender As Object, e As EventArgs) Handles CmdImprimirAbonos.Click
        Cuantos1 = CLng(TxtCuantosAbonos.Text)
        If Cuantos1 > 0 Then
            ImprimirInformes("Abonos")
        Else
            MsgBox("No existen abonos definidos")
        End If
    End Sub
    Private Function AsociarRemesa() As String
        Dim ListaRemesas As Object
        Dim oFrm As SeleccionarRemesa = New SeleccionarRemesa

        oFrm.ObjetoGlobal = Me.ObjetoGlobal

        If oFrm.ShowDialog() = DialogResult.OK Then
            If Trim(oFrm.RemesaAsociada) = "VARIAS" Then
                ListaRemesas = oFrm.DevolverRelacion
                AsociarRemesa = oFrm.ListaRemesas() '= EnGlobalRemesas(ListaRemesas)
            ElseIf Trim(oFrm.RemesaAsociada) = "NINGUNA" Then
                Return ""
            Else
                AsociarRemesa = oFrm.ListaRemesas()
                Return oFrm.ListaRemesas
            End If
        End If

    End Function

    Private Function CargarRemesas() As Boolean
        Dim sSQL As String
        Dim Cuantos As Long
        Dim RsTemp As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If ProcesoActual = "" Then
            ProcesoActual = HaySoloUna()
        End If
        If ProcesoActual = "" Then
            ' Ahora cargamos los datos
            ProcesoActual = AsociarRemesa()
        End If

        If ProcesoActual <> "" Then
            NumDiscAbonos = 0
            NumDiscCargos = 0

            sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso in(" & ProcesoActual & ") ORDER BY 1,2"
            RsTemp.Open(sSQL, ObjetoGlobal.cn)
            TabPrincipal.TabIndex = 0
            Cuantos = RsTemp.RecordCount + 2
            HabilitarProgreso("Un momento por favor. Se lee fichero de cargos/abonos:", Cuantos)
            ActualizarGrids(RsTemp)
            DesHabilitarProgreso()
            CargarRemesas = True
            Return True
        Else
            ' Deberemos solicitar un número de discriminante
            ProcesoActual = ""
            sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso= (" & Trim(ProcesoActual) & ") ORDER BY 1,2"
            RsTemp.Open(sSQL, ObjetoGlobal.cn)
            ActualizarGrids(RsTemp)
            Return True
        End If


    End Function

    Private Function HaySoloUna()
        Dim RsDocsAso As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSQL As String

        sSQL = "Select proceso FROM TEMP_CARGOS_ABONOS group by proceso"

        RsDocsAso.Open(sSQL, ObjetoGlobal.cn)
        If RsDocsAso.RecordCount = 1 Then
            HaySoloUna = "" & RsDocsAso!Proceso
        Else
            HaySoloUna = ""
        End If

    End Function

    Private Sub RellenaRegistroAbonos(Fila, Importe, CodigoDestinatario)
        Dim oRS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim oRSh As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim orden As Long
        Dim oRsAut As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Numero As Long

        If Trim(lvabonos.Items.Item(Fila).SubItems(15).Text) = "" Or Trim(lvabonos.Items.Item(Fila).SubItems(15).Text) = "0" Then ' Es nuevo

            lvabonos.Items.Item(Fila).SubItems(15).Text

            SQL = "SELECT * FROM recibos WHERE 1=0"
            oRS.Open(SQL, ObjetoGlobal.cn, True)
            oRS.AddNew()

            If Trim("" & lvabonos.Items.Item(Fila).subitems(16).text) = "" Then
                oRS!empresa_recibo = ObjetoGlobal.EmpresaActual
            Else
                oRS!empresa_recibo = Trim("" & lvabonos.Items.Item(Fila).subitems(16).text)
            End If
            oRS!cargo_abono = Trim("" & lvabonos.Items.Item(Fila).subitems(17).text)
            oRS!Documento = Trim("" & lvabonos.Items.Item(Fila).subitems(18).text)

            If Trim("" & lvabonos.Items.Item(Fila).subitems(20).text) <> "" Then
                oRS!serie = Trim("" & lvabonos.Items.Item(Fila).subitems(19).text)
                oRS!Numero = Trim("" & lvabonos.Items.Item(Fila).subitems(20).text)
            Else
                oRsAut.Open("SELECT max(numero) AS numero WHERE serie= 'Aut'", ObjetoGlobal.cn)
                If Not oRsAut.EOF Then
                    If IsDBNull(oRsAut!Numero) Then
                        Numero = 1
                    Else
                        Numero = oRsAut!Numero + 1
                    End If
                Else
                    Numero = 1
                End If
                oRsAut.Close()
                oRS!serie = "Aut"
                oRS!Numero = "" & Numero
            End If

            If Trim(lvabonos.Items.Item(Fila).subitems(21).text) <> "" Then
                oRS!fecha_documento = CDate(lvabonos.Items.Item(Fila).subitems(21).text)
            Else
                oRS!fecha_documento = DTFechaEmision.Value
            End If
            oRS!fecha_recibo = DTFechaEmision.Value
            oRS!fecha_remesa = DTFechaEmision.Value
            If Trim(lvabonos.Items.Item(Fila).subitems(24).text) <> "" Then
                oRS!cliente = CLng(lvabonos.Items.Item(Fila).subitems(24).text)
            End If
            If Trim(lvabonos.Items.Item(Fila).subitems(25).text) <> "" Then
                oRS!proveedor = CLng(lvabonos.Items.Item(Fila).subitems(25).text)
            End If
            If Trim(lvabonos.Items.Item(Fila).subitems(26).text) <> "" Then
                oRS!socio = CLng(lvabonos.Items.Item(Fila).subitems(26).text)
            End If
            If Trim(lvabonos.Items.Item(Fila).subitems(27).text) <> "" Then
                oRS!operario = CLng(lvabonos.Items.Item(Fila).subitems(27).text)
            End If


            oRS!NIF = Trim(lvabonos.Items.Item(Fila).subitems(28).text)
            oRS!Importe = CDbl(lvabonos.Items.Item(Fila).subitems(29).text)
            oRS!gastos = CDbl(lvabonos.Items.Item(Fila).subitems(30).text)
            oRS!aremesar = CDbl(lvabonos.Items.Item(Fila).subitems(31).text)
            oRS!situacion = "R"
            oRS!procedencia = "N"
            oRS!codigo_referencia = Trim(CodigoDestinatario)
            'oRS!referencia =

            oRS.Update()
            'oRS.Close
            SQL = "SELECT @@identity AS num_recibo"
            oRSh.Open(SQL, ObjetoGlobal.cn)
            If Not oRSh.EOF Then
                lvabonos.Items.Item(Fila).subitems(15).text = "" & oRSh!num_Recibo
                oRSh.Close()
                ' Y grabo el histórico
                SQL = "SELECT * FROM recibos_h WHERE 1=0"
                oRSh.Open(SQL, ObjetoGlobal.cn, True)
                oRSh.AddNew()
                oRSh!codigo = CLng(lvabonos.Items.Item(Fila).subitems(15).text)
                oRSh!Contador = 1
                oRSh!detalle = "Emisión nueva remesa"
                oRSh!Fecha = DTFechaEmision.Value
                oRSh!Importe = Importe
                oRSh.Update()
                oRSh.Close()
                ' Anotamos el número de recibo en temp_cargos_abonos
                AnotarDatosRecibosAbonos(Fila, CLng(lvabonos.Items.Item(Fila).subitems(15).text))
            Else
                oRSh.Close()
                MsgBox("Error localizando identidad")
            End If
            oRS.Close()
        Else
            SQL = "SELECT * FROM recibos WHERE codigo=" & lvabonos.Items.Item(Fila).subitems(15).text
            oRS.Open(SQL, ObjetoGlobal.cn, True)
            If oRS.RecordCount = 1 Then
                oRS!fecha_remesa = DTFechaEmision.Value
                oRS!situacion = "R"
                oRS.Update()
                SQL = "SELECT * FROM recibos_h WHERE codigo=" & lvabonos.Items.Item(Fila).subitems(15).text & " order by codigo DESC"
                oRSh.Open(SQL, ObjetoGlobal.cn, True)
                If oRSh.EOF Then
                    orden = 1
                Else
                    orden = oRSh!Contador + 1
                End If

                oRSh.AddNew()
                oRSh!codigo = CLng(lvabonos.Items.Item(Fila).subitems(15).text)
                oRSh!Contador = orden
                oRSh!detalle = "Emisión nueva remesa"
                oRSh!Fecha = DTFechaEmision.Value
                oRSh!Importe = Importe
                oRSh.Update()
                oRSh.Close()
            Else
                MsgBox("Error localizando recibo número " & lvabonos.Items.Item(Fila).subitems(16).text)
            End If
        End If
    End Sub

    Private Sub RellenaRegistroCargos(Fila, Importe, CodigoDestinatario, Referencia)
        Dim oRS As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim oRSh As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim orden As Long
        Dim oRsAut As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Numero As Long


        If Trim(LvCargos.Items.Item(Fila).subitems(15).text) = "" Or Trim(LvCargos.Items.Item(Fila).subitems(15).text) = "0" Then ' Es nuevo
            SQL = "SELECT * FROM recibos WHERE 1=0"
            oRS.Open(SQL, ObjetoGlobal.cn, True)
            oRS.AddNew()

            If Trim("" & LvCargos.Items.Item(Fila).subitems(16).text) = "" Then
                oRS!empresa_recibo = ObjetoGlobal.EmpresaActual
            Else
                oRS!empresa_recibo = Trim(LvCargos.Items.Item(Fila).subitems(16).text)
            End If
            oRS!cargo_abono = "C" 'Trim(LvCargos.TextMatrix(Fila, 15))
            oRS!Documento = Trim(LvCargos.Items.Item(Fila).subitems(18).text)
            If Trim(Trim(LvCargos.Items.Item(Fila).subitems(20).text)) <> "" Then
                oRS!serie = Trim(LvCargos.Items.Item(Fila).subitems(19).text)
                oRS!Numero = Trim(LvCargos.Items.Item(Fila).subitems(20).text)
            Else
                oRsAut.Open("SELECT max(cast(numero as integer)) AS contador FROM recibos WHERE serie= 'Aut'", ObjetoGlobal.cn)
                If Not oRsAut.EOF Then
                    If IsDBNull(oRsAut!Contador) Then
                        Numero = 1
                    Else
                        Numero = oRsAut!Contador + 1
                    End If
                Else
                    Numero = 1
                End If
                oRsAut.Close()
                oRS!serie = "Aut"
                oRS!Numero = "" & Format(Numero, "000000000")
            End If
            ' aaa
            If Trim(LvCargos.Items.Item(Fila).subitems(21).text) <> "" Then
                oRS!fecha_documento = CDate(LvCargos.Items.Item(Fila).subitems(21).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(22).text) <> "" Then
                oRS!fecha_recibo = CDate(LvCargos.Items.Item(Fila).subitems(22).text)
            Else
                oRS!fecha_recibo = CDate(DTFechaEmision.Value)
            End If
            oRS!fecha_remesa = DTFechaEmision.Value 'CDate(LvCargos.TextMatrix(Fila, 21))
            If Trim(LvCargos.Items.Item(Fila).subitems(24).text) <> "" Then
                oRS!cliente = CLng(LvCargos.Items.Item(Fila).subitems(24).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(25).text) <> "" Then
                oRS!proveedor = CLng(LvCargos.Items.Item(Fila).subitems(25).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(26).text) <> "" Then
                oRS!socio = CLng(LvCargos.Items.Item(Fila).subitems(26).text)
            End If
            If Trim(LvCargos.Items.Item(Fila).subitems(27).text) <> "" Then
                oRS!operario = CLng(LvCargos.Items.Item(Fila).subitems(27).text)
            End If
            oRS!NIF = Trim(LvCargos.Items.Item(Fila).subitems(28).text)

            oRS!Importe = Math.Abs(CDbl(LvCargos.Items.Item(Fila).subitems(29).text))
            oRS!gastos = Math.Abs(CDbl(LvCargos.Items.Item(Fila).subitems(30).text))
            oRS!aremesar = Math.Abs(CDbl(LvCargos.Items.Item(Fila).subitems(31).text))
            oRS!situacion = "R"
            oRS!procedencia = "N"
            oRS!codigo_referencia = Trim(CodigoDestinatario)
            oRS!Referencia = CDbl("0" & Referencia)
            oRS.Update()
            'oRS.Close

            SQL = "SELECT IDENT_CURRENT ('recibos') AS as num_recibo"
            'SQL = "SELECT @@identity AS num_recibo"

            oRSh.Open(SQL, ObjetoGlobal.cn)
            If Not oRSh.EOF Then
                'Print #101, "Grabado nuevo recibo " & oRSh!num_Recibo & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
                LvCargos.Items.Item(Fila).subitems(15).text = "" & oRSh!num_Recibo
                orden = oRSh!num_Recibo
                oRSh.Close()
                ' Y grabo el histórico
                SQL = "SELECT * FROM recibos_h WHERE 1=0"
                oRSh.Open(SQL, ObjetoGlobal.cn, True)
                oRSh.AddNew()
                oRSh!codigo = orden 'oRS!Recibo
                oRSh!Contador = 1
                oRSh!detalle = "Emisión nueva remesa"
                oRSh!Fecha = CDate(DTFechaEmision.Value)
                oRSh!Importe = Math.Abs(Importe)
                oRSh.Update()
                'Print #101, "Grabada línea orden: 1 del recibo " & orden & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
                oRSh.Close()
                ' Anotamos el número de recibo en temp_cargos_abonos
                AnotarDatosRecibosCargos(Fila, orden)
            Else
                oRSh.Close()
                MsgBox("Error localizando identidad")
            End If
            oRS.Close()
        Else
            SQL = "SELECT * FROM recibos WHERE codigo=" & LvCargos.Items.Item(Fila).subitems(15).text
            oRS.Open(SQL, ObjetoGlobal.cn, True)
            If oRS.RecordCount = 1 Then
                If oRS!situacion <> "R" Then
                    oRS!fecha_remesa = DTFechaEmision.Value
                    oRS!situacion = "R"
                    oRS.Update()
                    'Print #101, "Modificado recibo " & LvCargos.TextMatrix(Fila, 15) & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
                    SQL = "SELECT max(contador) as orden FROM recibos_h WHERE codigo=" & LvCargos.Items.Item(Fila).subitems(15).text
                    oRSh.Open(SQL, ObjetoGlobal.cn)
                    If IsDBNull(oRSh!orden) Then
                        orden = 1
                    Else
                        orden = oRSh!orden + 1
                    End If
                    oRSh.Close()

                    SQL = "SELECT * FROM recibos_h WHERE 1=0"
                    oRSh.Open(SQL, ObjetoGlobal.cn, True)
                    oRSh.AddNew()
                    oRSh!codigo = CLng(LvCargos.Items.Item(Fila).subitems(15).text)
                    oRSh!Contador = orden
                    oRSh!detalle = "Emisión nueva remesa"
                    oRSh!Fecha = DTFechaEmision.Value
                    oRSh!Importe = Math.Abs(Importe)
                    oRSh.Update()
                    'Print #101, "Añadida línea orden: " & orden & " del recibo " & LvCargos.TextMatrix(Fila, 15) & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
                    oRSh.Close()
                Else
                    'Print #101, " Recibo " & LvCargos.TextMatrix(Fila, 15) & " ya remesado. " & ProcesoActual & " " & Date & " hora " & Time
                End If
            Else
                MsgBox("Error localizando recibo número " & LvCargos.Items.Item(Fila).subitems(15).text)
                'Print #101, "Error localizando recibo número " & LvCargos.TextMatrix(Fila, 15) & " proceso: " & ProcesoActual & " " & Date & " hora " & Time
            End If
            oRS.Close()
        End If

    End Sub

    Private Sub CmdSeleccionRemesa_Click(sender As Object, e As EventArgs) Handles CmdSeleccionRemesa.Click
        ProcesoActual = ""
        CargarRemesas()
    End Sub

    Private Sub FrmCargosAbonosLLIRP_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sql As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim comboSource As New Dictionary(Of String, String)()


        sql = "SELECT * FROM auxiliar_soporte WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_norma = '34'"

        comboSource.Add("", "Selecciona opción")
        Rs.Open(sql, ObjetoGlobal.cn)
        While Not Rs.EOF
            comboSource.Add(Trim(Rs!cta_empresa), Trim(Rs!cta_empresa) + " - " + Trim(Rs!nombre_presentador))
            Rs.MoveNext()
        End While

        TxtCuenta1.DataSource = New BindingSource(comboSource, Nothing)
        TxtCuenta1.DisplayMember = “Value”
        TxtCuenta1.ValueMember = “Key”
        Rs.Close()

        comboSource.Clear()

        sql = "SELECT * FROM auxiliar_soporte WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_norma = '19'"
        Rs.Open(sql, ObjetoGlobal.cn)
        comboSource.Add("", "Selecciona opción")
        While Not Rs.EOF
            comboSource.Add(Trim(Rs!cta_empresa), Trim(Rs!cta_empresa) + " - " + Trim(Rs!nombre_presentador))
            Rs.MoveNext()
        End While
        'Rs.Close()

        TxtCuenta2.DataSource = New BindingSource(comboSource, Nothing)
        TxtCuenta2.DisplayMember = “Value”
        TxtCuenta2.ValueMember = “Key”
        Rs.Close()

        CargarRemesas()

    End Sub

    Private Sub TxtCuenta1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TxtCuenta1.SelectedIndexChanged

    End Sub

    'Private Sub TxtCuenta1_Validating(sender As Object, e As CancelEventArgs) Handles TxtCuenta1.Validating
    '    Dim sql As String
    '    Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

    '    sql = "SELECT * FROM auxiliar_soporte WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_norma = '34' and cta_empresa = '" & TxtCuenta1.SelectedText & "'"
    '    If Rs.Open(sql, ObjetoGlobal.cn) Then
    '        If Not Rs.EOF Then
    '            TxtNombreAbono.Text = Rs!nombre_presentador
    '            TxtDomicilioAbono.Text = Rs!a2
    '            TxtPoblacionAbono.Text = Rs!a1
    '            TxtNifAbono.Text = Rs!nif_presentador
    '            TxtEntidad1.Text = Trim(Rs!entidad_abono) + "-" + Trim(Rs!sucursal_abono) + "-" + Trim(Rs!dc_abono) + "-" + Trim(Rs!cuenta_abono)
    '        End If
    '    End If
    'End Sub

    Private Sub TxtCuenta1_Validated(sender As Object, e As EventArgs) Handles TxtCuenta1.Validated
        Dim sql As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        sql = "SELECT * FROM auxiliar_soporte WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_norma = '34' and cta_empresa = '" & TxtCuenta1.SelectedItem.key & "'"
        If Rs.Open(sql, ObjetoGlobal.cn) Then
            If Not Rs.EOF Then
                TxtNombreAbono.Text = Rs!nombre_presentador
                TxtDomicilioAbono.Text = Rs!a2
                TxtPoblacionAbono.Text = Rs!a1
                TxtNifAbono.Text = Rs!nif_presentador
                TxtEntidad1.Text = Trim(Rs!entidad_abono)
                TxtOficina1.Text = Trim(Rs!sucursal_abono)
                TxtDC1.Text = Trim(Rs!dc_abono)
                TxtCodCuenta1.Text = Trim(Rs!cuenta_abono)
            End If
        End If

    End Sub


    Private Sub TxtCuenta2_Validated(sender As Object, e As EventArgs) Handles TxtCuenta2.Validated
        Dim sql As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        sql = "SELECT * FROM auxiliar_soporte WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_norma = '19' and cta_empresa = '" & TxtCuenta2.SelectedItem.key & "'"
        If Rs.Open(sql, ObjetoGlobal.cn) Then
            If Not Rs.EOF Then
                TxtNombreCargo.Text = Rs!nombre_presentador
                TxtDomicilioCargo.Text = Rs!a2
                TxtPoblacionCargo.Text = Rs!a1
                TxtNifCargo.Text = Trim(Rs!nif_presentador) + Trim(Rs!sufijo_presentador)
                TxtEntidad2.Text = Trim(Rs!entidad_abono)
                TxtOficina2.Text = Trim(Rs!sucursal_abono)
                TxtDC2.Text = Trim(Rs!dc_abono)
                TxtCodCuenta2.Text = Trim(Rs!cuenta_abono)
            End If
        End If
    End Sub

    Private Sub CmdBorraRemesa_Click(sender As Object, e As EventArgs) Handles CmdBorraRemesa.Click
        Dim sSQL As String
        Dim sSQLR As String
        Dim sSQLH As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsR As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsH As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsTemp As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If ProcesoActual <> "" Then
            If MsgBox("Este proceso elimina todos los registros de la remesa en curso y sus recibos, ¿confirma borrar?", vbYesNo, "¡Eliminar remesa!") = vbYes Then

                ' Comprobamos recibos
                sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso=" & ProcesoActual & " AND recibo > 0"
                Rs.Open(sSQL, ObjetoGlobal.cn)

                While Not Rs.EOF
                    If Rs!Recibo > 0 Then
                        sSQLR = "Select * FROM recibos WHERE codigo =" & Rs!Recibo
                        sSQLH = "Select * FROM recibos_h WHERE codigo =" & Rs!Recibo
                        RsR.Open(sSQLR, ObjetoGlobal.cn)
                        If Not RsR.EOF Then
                            If RsR!situacion <> "R" Then
                                MsgBox("En el recibo " & Rs!Recibo & " se han realizado operaciones distintas de remesado, se cancela la eliminación")
                                Exit Sub
                            End If
                        End If
                        RsR.Close

                        RsH.Open(sSQLH, ObjetoGlobal.cn)
                        If Not RsH.EOF Then
                            If RsH.RecordCount > 1 Then
                                MsgBox("En el recibo " & Rs!Recibo & " se han realizado operaciones distintas de remesado, se cancela la eliminación")
                                Exit Sub
                            End If
                        End If
                        RsH.Close
                    End If
                    Rs.MoveNext
                End While
                Rs.Close()
                sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso=" & ProcesoActual & " AND recibo > 0"
                Rs.Open(sSQL, ObjetoGlobal.cn)

                While Not Rs.EOF
                    If Rs!Recibo > 0 Then
                        sSQLR = "Select * FROM recibos WHERE codigo =" & Rs!Recibo
                        sSQLH = "Select * FROM recibos_h WHERE codigo =" & Rs!Recibo
                        RsR.Open(sSQLR, ObjetoGlobal.cn, True)
                        If Not RsR.EOF Then
                            RsR.Delete()
                        End If
                        RsR.Update()
                        RsR.Close()
                        RsH.Open(sSQLH, ObjetoGlobal.cn, True)
                        If Not RsH.EOF Then
                            RsH.Delete()
                        End If
                        RsH.Update()
                        RsH.Close()
                    End If
                    Rs.MoveNext()
                End While
                Rs.Close

                sSQL = "DELETE FROM TEMP_CARGOS_ABONOS WHERE Proceso=" & ProcesoActual
                RsTemp.Open(sSQL, ObjetoGlobal.cn, True)
                RsTemp.MoveFirst()
                While RsTemp.RecordCount > 0
                    RsTemp.Delete()
                    RsTemp.MoveFirst()
                End While
                RsTemp.Close()

                ProcesoActual = ""

                'If RsTemp.State = adStateOpen Then
                '    RsTemp.Close()
                'End If

                sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso='" & Trim(ProcesoActual) & "' ORDER BY 1,2"
                RsTemp.Open(sSQL, ObjetoGlobal.cn, True)
                ActualizarGrids(RsTemp)
            End If
        End If
    End Sub

    Private Sub CmdGuardarAbono_Click(sender As Object, e As EventArgs) Handles CmdGuardarAbono.Click

        SaveFileDialog1.FileName = "ABO_" & CStr(Year(Date.Now)) & CStr(Month(Date.Now)) & CStr(DateAndTime.Day(Date.Now)) & CStr(DateAndTime.Timer)
        SaveFileDialog1.Filter = "XML Files (*.XML*)|*.XML"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
         Then
            TxtFicheroAbonos.Text = SaveFileDialog1.FileName
        End If
    End Sub

    Private Sub CmdGuardarCargo_Click(sender As Object, e As EventArgs) Handles CmdGuardarCargo.Click
        SaveFileDialog1.FileName = "CAR_" & CStr(Year(Date.Now)) & CStr(Month(Date.Now)) & CStr(DateAndTime.Day(Date.Now)) & CStr(DateAndTime.Timer)
        SaveFileDialog1.Filter = "XML Files (*.XML*)|*.XML"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
         Then
            TxtFicheroCargos.Text = SaveFileDialog1.FileName
        End If
    End Sub

    Private Sub ActualizarDiscriminantes(RsAux As cnRecordset.CnRecordset, Tipo As Integer)
        Dim i As Integer
        Dim Existe As Boolean

        If Tipo = 0 Then 'Abonos
            Existe = False
            If NumDiscAbonos > 0 Then
                For i = 1 To NumDiscAbonos
                    If Format(Trim(RsAux!Discriminante), "0000") + Trim(RsAux!concepto_8) = Trim(DiscAbonos(i)) Then
                        Existe = True
                        Exit For
                    End If
                Next
            End If
            If Existe = False Then
                NumDiscAbonos = NumDiscAbonos + 1
                If NumDiscAbonos > 50 Then
                    MsgBox("ATENCIÓN: No se puede grabar simultáneamente más de 50 ficheros de cargos/abonos")
                    Return
                End If
                DiscAbonos(NumDiscAbonos) = Format(Trim(RsAux!Discriminante), "0000") + Trim(RsAux!concepto_8)
            End If
        Else 'Cargos
            Existe = False
            If NumDiscCargos > 0 Then
                For i = 1 To NumDiscCargos
                    If Format(Trim(RsAux!Discriminante), "0000") + Trim(RsAux!concepto_8) = Trim(DiscCargos(i)) Then
                        Existe = True
                        Exit For
                    End If
                Next
            End If
            If Existe = False Then
                NumDiscCargos = NumDiscCargos + 1
                If NumDiscCargos > 100 Then
                    MsgBox("ATENCIÓN: No se puede grabar simultáneamente más de 50 ficheros de cargos/abonos")
                    Return
                End If
                DiscCargos(NumDiscCargos) = Format(Trim(RsAux!Discriminante), "0000") + Trim(RsAux!concepto_8)
            End If
        End If
    End Sub

    Private Sub ActualizarGrids(RsTemp)
        Dim Importe As Double
        Dim importe1 As Double
        Dim importe2 As Double
        Dim CuantosAbonos As Long
        Dim CuantosCargos As Long
        Dim Fila As Long
        Dim Poblacion As String
        Dim FechaEmision As Date, FechaEmisionEfecto As Date 'OJO
        'Dim Item As ListViewItem = New ListViewItem
        Dim arrLVAbono As String()
        Dim item As ListViewItem
        Dim arrLVCargo As ListViewItem()

        importe1 = 0
        importe2 = 0
        CuantosAbonos = 0
        CuantosCargos = 0

        If RsTemp.recordcount = 0 Then
            Exit Sub
        End If



        lvabonos.View = View.Details
        lvabonos.LabelEdit = False
        lvabonos.CheckBoxes = True
        lvabonos.FullRowSelect = True
        lvabonos.GridLines = True

        LvCargos.View = View.Details
        LvCargos.LabelEdit = False
        LvCargos.CheckBoxes = True
        LvCargos.FullRowSelect = True
        LvCargos.GridLines = True

        'lvabonos.Columns(i).AutoResize = vbTrue
        'lvabonos.Columns(i).TextAlign =
        'lvabonos.Items.


        FechaEmision = CDate("1/1/1990") 'OJO
        RsTemp.MoveFirst()

        ' Cabeceras abono
        PintaCabecera(lvabonos)
        PintaCabecera(LvCargos)

        While Not RsTemp.EOF
            pb.Value = pb.Value + 1
            pb.Refresh()
            'OJO Desde aquí
            If Not IsDBNull(RsTemp!fecha_valor) Then
                If IsDate(RsTemp!fecha_valor) Then
                    FechaEmisionEfecto = RsTemp!fecha_valor
                    If FechaEmision = CDate("1/1/1990") Then
                        FechaEmision = FechaEmisionEfecto
                    Else
                        If FechaEmision <> FechaEmisionEfecto Then FechaEmision = CDate("1/1/1991")
                    End If
                End If
            End If
            'OJO Hasta aquí
            Importe = IIf(IsDBNull(RsTemp!Importe), 0, Math.Round(RsTemp!Importe, 2))
            If Importe >= 0 Then
                CuantosAbonos = CuantosAbonos + 1
                importe1 = importe1 + Importe
                ActualizarDiscriminantes(RsTemp, 0)
                ReDim arrLVAbono(35)
                arrLVAbono(0) = If(IsDBNull(RsTemp!codigo_dest), "", (RsTemp!codigo_dest))
                arrLVAbono(1) = If(IsDBNull(RsTemp!NOMBRE_DEST), "", Trim(RsTemp!NOMBRE_DEST))
                arrLVAbono(2) = If(IsDBNull(RsTemp!pais_banco), "", Trim(RsTemp!pais_banco))
                arrLVAbono(3) = If(IsDBNull(RsTemp!dc_iban), "", Trim(RsTemp!dc_iban))
                arrLVAbono(4) = If(IsDBNull(RsTemp!entidad), "", Trim(RsTemp!entidad))
                arrLVAbono(5) = If(IsDBNull(RsTemp!sucursal), "", Trim(RsTemp!sucursal))
                arrLVAbono(6) = If(IsDBNull(RsTemp!dc), "", Trim(RsTemp!dc))
                arrLVAbono(7) = If(IsDBNull(RsTemp!cuenta), "", Trim(RsTemp!cuenta))
                arrLVAbono(8) = Format(Importe, "#,0.00")
                arrLVAbono(9) = If(IsDBNull(RsTemp!concepto_1), "", Trim(RsTemp!concepto_1))
                arrLVAbono(10) = If(IsDBNull(RsTemp!concepto_2), "", Trim(RsTemp!concepto_2))
                arrLVAbono(11) = If(IsDBNull(RsTemp!DOMICILIO_DEST), "", Trim(RsTemp!DOMICILIO_DEST))
                If IsDBNull(RsTemp!poblacion_dest) Then
                    Poblacion = ""
                Else
                    Poblacion = Trim(RsTemp!poblacion_dest)
                End If
                If Not IsDBNull(RsTemp!codigo_postal) Then
                    If Trim(RsTemp!codigo_postal) > "" Then
                        Poblacion = Trim(RsTemp!codigo_postal) + " " + Trim(Poblacion)
                    End If
                End If
                arrLVAbono(12) = Trim(Poblacion)
                arrLVAbono(13) = IIf(IsDBNull(RsTemp!Discriminante), "7", Trim(RsTemp!Discriminante))
                arrLVAbono(14) = IIf(IsDBNull(RsTemp!concepto_8), "", Trim(RsTemp!concepto_8))
                ' Datos del recibo
                arrLVAbono(15) = ("" & RsTemp!Recibo)
                arrLVAbono(16) = ("" & RsTemp!Empresa)
                arrLVAbono(17) = ("" & RsTemp!cargo_abono)
                arrLVAbono(18) = ("" & RsTemp!Documento)
                arrLVAbono(19) = ("" & RsTemp!serie)
                arrLVAbono(20) = ("" & RsTemp!Numero)
                arrLVAbono(21) = ("" & RsTemp!fecha_documento)
                arrLVAbono(22) = ("" & RsTemp!fecha_recibo)
                arrLVAbono(23) = ("" & RsTemp!fecha_remesa)
                arrLVAbono(24) = ("" & RsTemp!cliente)
                arrLVAbono(25) = ("" & RsTemp!proveedor)
                arrLVAbono(26) = ("" & RsTemp!socio)
                arrLVAbono(27) = ("" & RsTemp!operario)
                arrLVAbono(28) = ("" & RsTemp!NIF)
                arrLVAbono(29) = ("" & RsTemp!importe_recibo)
                arrLVAbono(30) = ("" & RsTemp!gastos)
                arrLVAbono(31) = ("" & RsTemp!aremesar)
                arrLVAbono(32) = ("" & RsTemp!situacion)
                arrLVAbono(33) = ("" & RsTemp!Proceso)
                arrLVAbono(34) = ("" & RsTemp!codigo)
                item = New ListViewItem(arrLVAbono)
                lvabonos.Items.Add(Item)

                'arrLVAbono(CuantosAbonos - 1) = New ListViewItem(CStr(RsTemp!Proceso))
                ''arrLVAbono(CuantosAbonos - 1).SubItems(0).Text = RsTemp!Proceso
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!codigo_dest), "", (RsTemp!codigo_dest)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!NOMBRE_DEST), "", Trim(RsTemp!NOMBRE_DEST)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!pais_banco), "", Trim(RsTemp!pais_banco)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!dc_iban), "", Trim(RsTemp!dc_iban)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!entidad), "", Trim(RsTemp!entidad)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!sucursal), "", Trim(RsTemp!sucursal)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!dc), "", Trim(RsTemp!dc)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!cuenta), "", Trim(RsTemp!cuenta)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(Format(Importe, "#,0.00"))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!concepto_1), "", Trim(RsTemp!concepto_1)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!concepto_2), "", Trim(RsTemp!concepto_2)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!DOMICILIO_DEST), "", Trim(RsTemp!DOMICILIO_DEST)))
                'If IsDBNull(RsTemp!poblacion_dest) Then
                '    Poblacion = ""
                'Else
                '    Poblacion = Trim(RsTemp!poblacion_dest)
                'End If
                'If Not IsDBNull(RsTemp!codigo_postal) Then
                '    If Trim(RsTemp!codigo_postal) > "" Then
                '        Poblacion = Trim(RsTemp!codigo_postal) + " " + Trim(Poblacion)
                '    End If
                'End If
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(Trim(Poblacion))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!Discriminante), "7", Trim(RsTemp!Discriminante)))
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!concepto_8), "", Trim(RsTemp!concepto_8)))
                '' Datos del recibo
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!Recibo)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!Empresa)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!cargo_abono)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!Documento)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!serie)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!Numero)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!fecha_documento)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!fecha_recibo)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!fecha_remesa)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!cliente)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!proveedor)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!socio)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!operario)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!NIF)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!importe_recibo)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!gastos)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!aremesar)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!situacion)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!Proceso)
                'arrLVAbono(CuantosAbonos - 1).SubItems.Add("" & RsTemp!codigo)
                'lvabonos.Items.Add(arrLVAbono(CuantosAbonos - 1))
            Else
                Importe = -Importe
                importe2 = importe2 + Importe
                ActualizarDiscriminantes(RsTemp, 1)
                CuantosCargos = CuantosCargos + 1
                ReDim Preserve arrLVCargo(CuantosCargos - 1)
                arrLVCargo(CuantosCargos - 1) = New ListViewItem()
                'arrLVCargo(CuantosCargos - 1).SubItems(0).Text = RsTemp!Proceso
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!codigo_dest), "", (RsTemp!codigo_dest)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!NOMBRE_DEST), "", Trim(RsTemp!NOMBRE_DEST)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!pais_banco), "", Trim(RsTemp!pais_banco)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!dc_iban), "", Trim(RsTemp!dc_iban)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!entidad), "", Trim(RsTemp!entidad)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!sucursal), "", Trim(RsTemp!sucursal)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!dc), "", Trim(RsTemp!dc)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!cuenta), "", Trim(RsTemp!cuenta)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(Format(Importe, "#,0.00"))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!concepto_1), "", Trim(RsTemp!concepto_1)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!concepto_2), "", Trim(RsTemp!concepto_2)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!DOMICILIO_DEST), "", Trim(RsTemp!DOMICILIO_DEST)))
                If IsDBNull(RsTemp!poblacion_dest) Then
                    Poblacion = ""
                Else
                    Poblacion = Trim(RsTemp!poblacion_dest)
                End If
                If Not IsDBNull(RsTemp!codigo_postal) Then
                    If Trim(RsTemp!codigo_postal) > "" Then
                        Poblacion = Trim(RsTemp!codigo_postal) + " " + Trim(Poblacion)
                    End If
                End If
                arrLVCargo(CuantosCargos - 1).SubItems.Add(Trim(Poblacion))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!Discriminante), "7", Trim(RsTemp!Discriminante)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add(IIf(IsDBNull(RsTemp!concepto_8), "", Trim(RsTemp!concepto_8)))
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!Recibo)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!Empresa)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!cargo_abono)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!Documento)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!serie)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!Numero)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!fecha_documento)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!fecha_recibo)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!fecha_remesa)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!cliente)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!proveedor)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!socio)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!operario)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!NIF)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!importe_recibo)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!gastos)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!aremesar)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!situacion)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!Proceso)
                arrLVCargo(CuantosCargos - 1).SubItems.Add("" & RsTemp!codigo)
                LvCargos.Items.Add(arrLVCargo(CuantosCargos - 1))
            End If
            RsTemp.MoveNext()
        End While
        TxtCuantosAbonos.Text = Format(CuantosAbonos, "####0")
        TxtCuantosCargos.Text = Format(CuantosCargos, "####0")
        TxtTotalAbonos.Text = Format(importe1, "#,0.00")
        TxtTotalCargos.Text = Format(importe2, "#,0.00")

        If FechaEmision = CDate("1/1/1990") Then
            DTFechaEmision.Value = Date.Now
        ElseIf FechaEmision = CDate("1/1/1991") Then
            DTFechaEmision.Value = Date.Now
            MsgBox("La remesa contiene efectos con distintas fechas de vencimiento")
        Else
            DTFechaEmision.Value = FechaEmision
        End If

    End Sub

    Private Sub HabilitarProgreso(ByVal Texto As String, ByVal Cuantos As Long)
        lblProgreso.Visible = True
        lblProgreso.Text = Trim(Texto)
        lblProgreso.Refresh()
        pb.Value = 0
        pb.Minimum = 0
        pb.Maximum = Cuantos
        pb.Visible = True
        pb.Refresh()
    End Sub
    Private Sub DesHabilitarProgreso()
        lblProgreso.Visible = False
        lblProgreso.Text = ""
        lblProgreso.Refresh()
        pb.Minimum = 0
        pb.Maximum = 0
        pb.Value = 0
        pb.Visible = False
        pb.Refresh()
    End Sub
    Private Sub AnotarDatosRecibosCargos(nFila, Recibo)
        Dim sSQL As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso='" & Trim(LvCargos.Items.Item(nFila).subitem(33).text) & "' AND codigo =" & Trim(LvCargos.Items.Item(nFila).subitem(34).text)
        Rs.Open(sSQL, ObjetoGlobal.cn, True)
        If Not Rs.EOF Then
            Rs!Recibo = Recibo
            Rs.Update()
        End If
        Rs.Close()

    End Sub

    Private Sub AnotarDatosRecibosAbonos(nFila, Recibo)
        Dim sSQL As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso='" & Trim(lvabonos.Items.Item(nFila).subitem(33).text) & "' AND codigo =" & Trim(lvabonos.Items.Item(nFila).subitem(34).text)
        Rs.Open(sSQL, ObjetoGlobal.cn, True)
        If Not Rs.EOF Then
            Rs!Recibo = Recibo
            Rs.Update()
        End If
        Rs.Close()
    End Sub
    Private Function EnGlobalRemesas(ListaRemesas())
        Dim sSQL As String
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Proceso As Long
        Dim i As Integer
        Dim o As Integer
        Dim Linea As Integer
        Dim RsNueva As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE 1=0"
        RsNueva.Open(sSQL, ObjetoGlobal.cn, True)

        sSQL = "Select max(proceso) as codigo FROM TEMP_CARGOS_ABONOS"
        Rs.Open(sSQL, ObjetoGlobal.cn)

        If Not Rs.EOF Then
            If Not IsDBNull(Rs!codigo) Then
                Proceso = Rs!codigo + 1
            Else
                Proceso = 1
            End If
        Else
            Proceso = 1
        End If

        Rs.Close()

        For i = 0 To UBound(ListaRemesas)
            sSQL = "Select * FROM TEMP_CARGOS_ABONOS WHERE Proceso='" & Trim(ListaRemesas(i)) & "' ORDER BY 1, 2"
            Rs.Open(sSQL, ObjetoGlobal.cn)
            While Not Rs.EOF
                RsNueva.AddNew()
                Linea = Linea + 1
                RsNueva!Proceso = Proceso
                RsNueva!codigo = Linea
                For o = 2 To Rs.CuantosCampos - 1
                    'If UCase(Trim(Rs.Fields.Item(o).Name)) = "FECHA_VALOR" Then
                    If UCase(Trim(Rs(o))) = "FECHA_VALOR" Then
                        RsNueva(o) = Date.Now
                    Else
                        RsNueva(o) = Rs(o)
                    End If
                Next
                RsNueva.Update()
                Rs.MoveNext()
            End While
            Rs.Close()
        Next
        Return Proceso

    End Function
    Private Sub ImprimirInformes(Tipo)
        Dim MsGrid As Object
        Dim CuantosDocs As Integer
        Dim i As Integer
        Dim Importe As Double
        Dim IbanBan As String
        Dim bPrevisualizar As Boolean = False
        Dim oImp As ReportBuilder2.ClsImpresion = New ReportBuilder2.ClsImpresion(Me.ObjetoGlobal)
        Dim TotalInforme As Double
        Dim NLinea As Integer
        Dim nPagina As Integer
        Dim cDoc As String

        cDoc = "formato A5 modificado"

        If Not oImp.Inicializar("informe_remesas", "informe_remesas", "Formato1", 0, bPrevisualizar, ObjetoGlobal.cn, "", "", "", "", "") Then
            MsgBox("No se encuentra el formato de edición del documento.")
            Return
        End If

        If Trim(Tipo) = "Abonos" Then
            CuantosDocs = CLng(TxtCuantosAbonos.Text)
            MsGrid = lvabonos
        Else
            CuantosDocs = CLng(TxtCuantosCargos.Text)
            MsGrid = LvCargos
        End If

        NLinea = 0

        '  MsGrid = MSAbonos

        nPagina = 0
        TotalInforme = 0

        For i = 1 To CuantosDocs
            Importe = CDbl(MsGrid.Items.Item(i).subitem(8))
            If i = 1 Then
                ImprimeCabecera(oImp, Tipo, nPagina)
            End If
            TotalInforme = TotalInforme + Math.Round(Importe, 2)
            IbanBan = Trim(MsGrid.Items.Item(i).subitem(2)) + Strings.Right("00" + Trim(MsGrid.Items.Item(i).subitem(3).text), 2) + Strings.Right("0000" + Trim(MsGrid.Items.Item(i).subitem(4)), 4) + Strings.Right("0000" + Trim(MsGrid.Items.Item(i).subitem(5)), 4) + Strings.Right("00" + Trim(MsGrid.Items.Item(i).subitem(6).text), 2) + Strings.Right("0000000000" + Trim(MsGrid.Items.Item(i).subitem(7).text), 10)

            oImp.VolcarAImpresion(CLng(nPagina), 1, 0, CLng(NLinea), "calculado.codigo", 0, "" & Trim(MsGrid.Items.Item(i).subitem(0).text))
            oImp.VolcarAImpresion(CLng(nPagina), 1, 0, CLng(NLinea), "calculado.interesado", 0, "" & Trim(MsGrid.Items.Item(i).subitem(1).text))
            oImp.VolcarAImpresion(CLng(nPagina), 1, 0, CLng(NLinea), "calculado.iban", 0, "" & IbanBan)
            oImp.VolcarAImpresion(CLng(nPagina), 1, 0, CLng(NLinea), "calculado.importe", 0, "" & Importe)
            oImp.VolcarAImpresion(CLng(nPagina), 1, 0, CLng(NLinea), "calculado.concepto", 0, "" & Trim(MsGrid.Items.Item(i).subitem(9).text) + Trim(MsGrid.Items.Item(i).subitem(10).text))
            NLinea = NLinea + 1
            If NLinea > 58 Then
                '   NLinea = 0
            End If
        Next
        ImprimePie(oImp, TotalInforme, nPagina)
        oImp.Imprimir()

    End Sub

    Sub ImprimeCabecera(oImp, tipo, nPagina)

        oImp.VolcarAImpresion(CLng(nPagina), 0, -1, 0, "calculado.fecha_envio", 0, "" & Format(DTFechaEnvio.Value, "dd/mm/yyyy"))
        oImp.VolcarAImpresion(CLng(nPagina), 0, -1, 0, "calculado.Fecha_emision", 0, "" & Format(DTFechaEmision.Value, "dd/mm/yyyy"))
        oImp.VolcarAImpresion(CLng(nPagina), 0, -1, 0, "calculado.tipo", 0, "" & UCase(tipo))
        'nLineaGeneral = 9
        'NLinea = 1

    End Sub

    Sub ImprimePie(oImp, TotalInforme, nPagina)
        ' Cabecera
        oImp.VolcarAImpresion(CLng(nPagina), 2, -1, 0, "calculado.total", 0, "" & Math.Round(TotalInforme, 2))
    End Sub

    Sub PintaCabecera(lv As Object)
        Dim Titulos As String() = {"Cód", "Nombre/Razón soocial", "País", "DC", "Ent", "Suc", "DC", "Cuenta", "Importe", "concepto 1", "Concepto 2", "Domicilio", "población", "F", "Concepto 8"}

        For i = 0 To UBound(Titulos) - 1
            lv.Columns.Add(New ColumnHeader())
            lv.Columns(i).Text = Titulos(i)
            'lv.Columns(i).AutoResize = vbTrue
        Next
        lv.Columns(0).TextAlign = HorizontalAlignment.Right
        lv.Columns(8).TextAlign = HorizontalAlignment.Right





    End Sub

    'Private Sub TxtCuenta2_Validating(sender As Object, e As CancelEventArgs) Handles TxtCuenta2.Validating
    '    Dim sql As String
    '    Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

    '    sql = "SELECT * FROM auxiliar_soporte WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND codigo_norma = '19' and cta_empresa = '" & TxtCuenta1.SelectedText & "'"
    '    If Rs.Open(sql, ObjetoGlobal.cn) Then
    '        If Not Rs.EOF Then
    '            TxtNombreAbono.Text = Rs!nombre_presentador
    '            TxtDomicilioAbono.Text = Rs!a2
    '            TxtPoblacionAbono.Text = Rs!a1
    '            TxtNifAbono.Text = Trim(Rs!nif_presentador) + Trim(Rs!sufijo_presentador)
    '            TxtEntidad1.Text = Trim(Rs!entidad_abono) + "-" + Trim(Rs!sucursal_abono) + "-" + Trim(Rs!dc_abono) + "-" + Trim(Rs!cuenta_abono)
    '        End If
    '    End If
    'End Sub
End Class