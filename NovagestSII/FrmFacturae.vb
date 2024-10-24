Imports System.IO

Public Class FrmFacturae
    Inherits libcomunes.FormGenerico
    Dim objetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public cPath As String
    Dim RutaAutofirma As String
    Dim PathFacturas As String
    Dim PathFacturasFirmadas As String


    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub CmdProcesar_Click(sender As Object, e As EventArgs) Handles CmdProcesar.Click
        Dim oFac As ClsClaseFacturae
        Dim oRs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim oRsTotales As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim Sql1 As String
        Dim Sql2 As String
        Dim Sql3 As String
        Dim Codigoscliente() As Integer
        Dim i As Integer
        Dim cXML As String
        Dim Factura As String

        oFac = New ClsClaseFacturae

        oFac.ObjetoGlobal = Me.objetoGlobal

        If Trim(TxtNIF.Text) <> "" Then
            Sql1 = "SELECT * FROM factura_vta_c WHERE cif = '" & Trim(TxtNIF.Text) & "' "
        ElseIf Trim(TxtCliente.Text) = "" Then
            Sql1 = "SELECT DISTINCT codigo_cliente FROM factura_vta_c WHERE "
        Else
            Sql1 = "SELECT * FROM factura_vta_c WHERE codigo_cliente=" & TxtCliente.Text & " AND "
        End If
        If Trim(TxtSerie.Text) <> "" Then
            Sql2 = " serie_factura_vta = '" & TxtSerie.Text & "'"
            Sql3 = " f.serie_factura_vta = '" & TxtSerie.Text & "'"
        End If
        If Trim(TxtDesdefactura.Text) <> "" Then
            Sql2 = Sql2 + IIf(Trim(Sql2) <> "", " AND ", "") + " numero_factura >= " & TxtDesdefactura.Text
            Sql3 = Sql3 + IIf(Trim(Sql3) <> "", " AND ", "") + " f.numero_factura >= " & TxtDesdefactura.Text
        End If
        If Trim(TxtHastaFactura.Text) <> "" Then
            Sql2 = Sql2 + IIf(Trim(Sql2) <> "", " AND ", "") + " numero_factura <= " & TxtHastaFactura.Text
            Sql3 = Sql3 + IIf(Trim(Sql3) <> "", " AND ", "") + " f.numero_factura <= " & TxtHastaFactura.Text
        End If
        'If Trim(TxtHastaFactura.Text) <> "" Then
        Sql2 = Sql2 + IIf(Trim(Sql2) <> "", " AND ", "") + " fecha_factura BETWEEN '" & CStr(DTDesdefecha.value) & "' AND '" & CStr(DTHastaFecha.value) & "'"
        Sql3 = Sql3 + IIf(Trim(Sql3) <> "", " AND ", "") + " fecha_factura BETWEEN '" & CStr(DTDesdefecha.value) & "' AND '" & CStr(DTHastaFecha.value) & "'"
        'End If

        If Trim(TxtNIF.Text) <> "" Then
            ' Nada de nada
        ElseIf Trim(TxtCliente.Text) = "" Then
            Sql = Sql1 + Sql2
            oRs.Open(Sql, objetoGlobal.cn)
            ReDim Codigoscliente(oRs.RecordCount)
            i = 0
            While Not oRs.EOF
                i = i + 1
                Codigoscliente(i) = oRs!codigo_cliente
                oRs.MoveNext()
            End While
            oRs.Close()
        Else
            ReDim Codigoscliente(1)
            Codigoscliente(1) = TxtCliente.Text
        End If


        If Trim(TxtNIF.Text) = "" Then
            For i = 1 To UBound(Codigoscliente)
                Sql1 = "SELECT max(t.importe_cargo) AS totales FROM factura_vta_c f LEFT JOIN factura_vta_c_tot t ON t.empresa = f.empresa AND "
                Sql1 = Sql1 + " t.serie_factura_vta = f.serie_factura_vta AND t.numero_factura = f.numero_factura Where f.codigo_cliente = " & Codigoscliente(i) & " And t.tipo_linea = 'T' AND"
                Sql = Sql1 + Sql3
                oRs.Open(Sql, objetoGlobal.cn)
                oFac.importeremesa = oRs!totales
                oRs.Close()

                Sql1 = "SELECT * FROM factura_vta_c WHERE codigo_cliente=" & Codigoscliente(i) & " AND "
                Sql = Sql1 + Sql2
                oRs.Open(Sql, objetoGlobal.cn)
                oFac.numerofacturas = oRs.RecordCount
                oFac.IdentificadorLote = Trim(oRs!cif) & "-" & Format(Date.Now, "yyyymmdd") & DateTime.Now.ToString("hh:mm:ss")
                oFac.CabeceraEnvio(IIf(oRs.RecordCount > 1, "L", "I"))
                oFac.Emisor(objetoGlobal.EmpresaActual)
                'oFac.Receptor oRs
                oFac.ReceptorAdministracion(oRs)
                oFac.InicioLote()
                Factura = "FACTURAS_" & Trim(oRs!cif) & "_" & Trim(oRs!serie_factura_vta) & "_" & Trim(oRs!numero_factura)
                While Not oRs.EOF
                    oFac.DetalleFacturas(oRs)
                    oRs.MoveNext()
                End While
                oFac.FinLote()

                cXML = oFac.XML
                EscribirXML(Factura, cXML, PathFacturas)
                If CBFirmaFactura.Checked Then
                    FirmarFacturas(Factura)
                End If
            Next
        Else
            If Opc1.Checked Then

                Sql1 = "SELECT * FROM factura_vta_c f WHERE f.cif = '" & Trim(TxtNIF.Text) & "' AND "
                Sql = Sql1 + Sql2
                oRs.Open(Sql, objetoGlobal.cn)
                While Not oRs.EOF
                    Sql1 = "SELECT f.serie_factura_vta, f.numero_factura, t.*  FROM factura_vta_c f LEFT JOIN factura_vta_c_tot t ON t.empresa = f.empresa AND "
                    Sql1 = Sql1 + " t.serie_factura_vta = f.serie_factura_vta AND t.numero_factura = f.numero_factura Where f.empresa = '" & Trim(oRs!empresa) & "' AND f.serie_factura_vta = '" & Trim(oRs!serie_factura_vta) & "' AND f.numero_factura = '" & Trim(oRs!numero_factura) & "' And t.tipo_linea = 'T' "

                    oRsTotales.Open(Sql1, objetoGlobal.cn)
                    If Not IsDBNull(oRsTotales!importe_cargo) Then
                        oFac.importeremesa = oRsTotales!importe_cargo
                    Else
                        MsgBox("No hay importes en factura")
                        Return
                    End If
                    oRsTotales.Close()

                    oFac.numerofacturas = 1
                    oFac.IdentificadorLote = Trim(oRs!cif) & "-" & Format(Date.Now, "yyyymmdd") & DateTime.Now.ToString("hh:mm:ss")
                    oFac.CabeceraEnvio("I")
                    oFac.Emisor(objetoGlobal.EmpresaActual)
                    oFac.ReceptorAdministracion(oRs)
                    oFac.InicioLote()
                    oFac.DetalleFacturas(oRs)
                    oFac.FinLote()
                    cXML = oFac.XML

                    Factura = "FACTURAS_" & Trim(TxtNIF.Text) & "_" & Trim(oRs!serie_factura_vta) & "_" & Trim(oRs!numero_factura)

                    EscribirXML(Factura, cXML, PathFacturas)
                    If CBFirmaFactura.Checked Then
                        FirmarFacturas(Factura)
                    End If
                    oRs.MoveNext()
                End While
            Else
                Sql1 = "SELECT SUM(t.importe_cargo) AS totales FROM factura_vta_c f LEFT JOIN factura_vta_c_tot t ON t.empresa = f.empresa AND "
                Sql1 = Sql1 + " t.serie_factura_vta = f.serie_factura_vta AND t.numero_factura = f.numero_factura Where f.cif = '" & Trim(TxtNIF.Text) & "' And t.tipo_linea = 'T' AND "
                Sql = Sql1 + Sql2
                oRs.Open(Sql, objetoGlobal.cn)
                If Not IsDBNull(oRs!totales) Then
                    oFac.importeremesa = oRs!totales
                Else
                    MsgBox("No hay importes")
                    Exit Sub
                End If
                oRs.Close()

                Sql1 = "SELECT * FROM factura_vta_c f WHERE f.cif = '" & Trim(TxtNIF.Text) & "' AND "
                Sql = Sql1 + Sql2
                oRs.Open(Sql, objetoGlobal.cn)
                oFac.numerofacturas = oRs.RecordCount
                oFac.IdentificadorLote = Trim(oRs!cif) & "-" & Format(Date.Now, "yyyymmdd") & DateTime.Now.ToString("hh:mm:ss")
                oFac.CabeceraEnvio(IIf(oRs.RecordCount > 1, "L", "I"))
                oFac.Emisor(objetoGlobal.EmpresaActual)
                oFac.Receptor(oRs)
                oFac.InicioLote()

                While Not oRs.EOF
                    oFac.DetalleFacturas(oRs)
                    oRs.MoveNext()
                End While
                oFac.FinLote()
                cXML = oFac.XML

                Factura = "FACTURAS_" & Trim(TxtNIF.Text) & "_" & Format(DTDesdefecha.value, "ddmmyyyy") & "_" & Format(DTHastaFecha.value, "ddmmyyyy")

                EscribirXML(Factura, cXML, PathFacturas)
                If CBFirmaFactura.Checked Then
                    FirmarFacturas(Factura)
                End If
            End If
        End If
        MsgBox("Proceso concluido")

    End Sub
    Public Property og(ogr As ObjetoGlobal.ObjetoGlobal) As ObjetoGlobal.ObjetoGlobal
        Set(value As ObjetoGlobal.ObjetoGlobal)
            objetoGlobal = value
        End Set
        Get
            Return objetoGlobal
        End Get
    End Property

    Function FirmarFacturas(Fichero As String) As String
        ''Dim oShell As WshShell
        ''Dim oExec As WshExec
        'Dim ret As String
        'Dim cShell As String

        ''cShell = RutaAutofirma + "\autofirma\" + "AutoFirmaCommandLine.exe sing -i " + PathFacturas + fichero + ".xml" + " -o " + PathFacturasFirmadas + fichero + ".xml" + " -alias " & Trim(txtAlias.Text)

        'cShell = RutaAutofirma + "\autofirma\AutoFirmaCommandLine.exe sign -i " & PathFacturas & Fichero & ".xml -o " + PathFacturasFirmadas + Fichero + ".xml -store windows  -alias " & Trim(TxtAlias.Text)

        'Shell cShell, 0


        ' retornar la salida y devolverla a la función
        'FirmarFacturas = ret ' Replace(ret, Chr(10), vbNewLine)
        Application.DoEvents()

        'Dim x As String
        'x = Shell("rundll32.exe url.dll,FileProtocolHandler " & ("ruta del archivo"))
        '
        'mejor shell"unidad:\direccion del programa\programa.exe"

    End Function

    'Public Class Book
    '    Public Title As String
    'End Class

    'Public Function EscribirXML(ByRef fnombre As String, ByVal cXML As String, Optional cPath As String = "c:\facturae\facturas\")
    '    Dim overview As New Book
    '    overview.Title = "Serialization Overview"
    '    Dim writer As New System.Xml.Serialization.XmlSerializer(GetType(Book))

    '    If UCase(Strings.Right(fnombre.Trim, 4)) <> ".XML" Then
    '        fnombre = fnombre.Trim + ".XML"
    '    End If

    '    If Strings.Right(cPath.Trim, 1) <> "\" Then
    '        cPath = cPath.Trim + "\"
    '    End If

    '    Dim file As New System.IO.StreamWriter(cPath & fnombre)
    '    writer.Serialize(file, overview)
    '    file.Close()

    '        Dim objStream As ADODB.Stream
    '        Dim Archivo As Object
    '        Dim Fichero As String
    '        Dim contador As Integer
    '        Dim Fic As String

    '        Fic = Replace(fnombre, "/", "") & ".xml"
    '        Fichero = cPath & "\" & Fic
    '        contador = 0

    '        While Trim(Dir(Fichero)) = Trim(Fic)
    '            contador = contador + 1
    '            Fic = Replace(fnombre, "/", "") & "(" & contador & ")" & ".xml"
    '            Fichero = cPath & "\" & Fic
    '        End While

    '        EscribirXML = Fichero

    'Set objStream = New ADODB.Stream
    'objStream.Open
    '        objStream.Charset = "UTF-8"
    '        objStream.WriteText cXML
    'objStream.SaveToFile Fichero
    'objStream.Close
    'Set objStream = Nothing


    'End Function


    Private Sub directorios(dir)
        Try
            If Not File.Exists(dir) Then
                ':::Si la carpeta no existe la creamos
                Directory.CreateDirectory(dir)
                MsgBox("Archivo guardado correctamente", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox("Se presento un problema al momento de guardar el archivo: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub EscribirXML(archivo, cXML, ruta)
        Dim escribir As StreamWriter = New StreamWriter(ruta & archivo, True)

        Try
            escribir.Write(cXML)
            escribir.Flush()
            escribir.Close()
            MsgBox("Archivo guardado correctamente", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Se presento un problema al escribir en el archivo: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return objetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            objetoGlobal = value
        End Set
    End Property

End Class