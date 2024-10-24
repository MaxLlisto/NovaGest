Imports System.IO

Public Class FrmSII
    Inherits libcomunes.FormGenerico
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Private Sub CmdBuscarXML_Click(sender As Object, e As EventArgs) Handles CmdBuscarXML.Click
        Dim FileDialogo As New OpenFileDialog

        FileDialogo.FileName = TxtNombreXML.Text
        FileDialogo.InitialDirectory = TxtNombreXML.Text
        If FileDialogo.ShowDialog = Windows.Forms.DialogResult.OK Then
            TxtNombreXML.Text = FileDialogo.FileName
        End If
        Return
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub CmdFacturasEmitidas_Click(sender As Object, e As EventArgs) Handles CmdFacturasEmitidas.Click
        Dim oCls As ClsTFacturasEmitidas
        Dim dDesde As Date
        Dim dHasta As Date
        Dim Sql As String
        Dim periodo As String
        Dim cXML As String

        dDesde = CDate(DPDesde.value)
        dHasta = CDate(DPHasta.value)
        If Month(dDesde) <> Month(dHasta) Then
            MsgBox("Los documentos será de distintos periodos")
            Exit Sub
        End If
        periodo = Strings.Right("00" & Month(dDesde), 2)
        Sql = " empresa in ('1', '2', '3', '4', '5', '11') and (fecha_factura >= '" & CStr(dDesde) & "' AND fecha_factura <= '" & CStr(dHasta) & "') AND cif <> 'F46026068' " ' AND serie_factura_vta = 'FESFS' "

        oCls = New ClsTFacturasEmitidas
        oCls.ObjGlobal = ObjetoGlobal
        oCls.Sql = Sql
        oCls.Ejercicio = "" & Year(dDesde)
        oCls.periodo = periodo
        oCls.empresa = "Cooperativa Vinícola de Lliria, S.C.V."
        oCls.nif = "F46026068"

        FileOpen(2, Replace("c:\F_emitidas_" & CDate(dDesde) & "_" & CDate(dHasta), "/", "_") & ".csv", OpenMode.Output)

        If Trim(TxtEnvioE.Text) = "" Then
            oCls.AltaFactura(PB1)
        Else
            oCls.ReenvioErroresFactura(CLng("0" & Trim(TxtEnvioE.Text)))
        End If
        cXML = oCls.XMLs
        If Trim(cXML) <> "" Then
            If CkEmi.Checked Then
                'PasarServicio(cXML)
                EscribirXML("F_emitidas_" & CDate(dDesde) & "_" & CDate(dHasta), cXML)
            Else
                EscribirXML("F_emitidas_" & CDate(dDesde) & "_" & CDate(dHasta), cXML)
            End If
        End If
        FileClose(2)
        txtPath.Text = "/wlpl/SSII-FACT/ws/fe/SiiFactFEV1SOAP"
        MsgBox("Envio finalizado")

    End Sub

    Private Sub CmdFacturasRecibidas_Click(sender As Object, e As EventArgs) Handles CmdFacturasRecibidas.Click
        Dim oCls As ClsFacturasRecibidas = New ClsFacturasRecibidas
        Dim dDesde As Date
        Dim dHasta As Date
        Dim Sql As String
        Dim periodo As String
        Dim cXML As String


        dDesde = CDate(DPDesde.value)
        dHasta = CDate(DPHasta.value)
        '    If Month(dDesde) <> Month(dHasta) Then
        '       MsgBox "Los documentos será de distintos periodos"
        '       Exit Sub
        '    End If
        periodo = Strings.Right("00" & Month(dDesde), 2)
        Sql = " empresa in ('1', '2', '3', '4', '5', '11') and fecha_entrada >= '" & CStr(dDesde) & "' AND fecha_entrada <= '" & CStr(dHasta) & "' AND cif_prov <> 'F46026068'"


        oCls.ObjGlobal = ObjetoGlobal
        oCls.Sql = Sql
        oCls.Ejercicio = "" & Year(dDesde)
        oCls.empresa = "Cooperativa Vinícola de Lliria, S.C.V."
        oCls.periodo = periodo
        oCls.nif = "F46026068"
        oCls.Compensacion = 100

        FileOpen(2, Replace("c:\F_recibidas_" & CDate(dDesde) & "_" & CDate(dHasta), "/", "_") & ".csv", OpenMode.Output)

        If Trim(TxtEnvioR.Text) = "" Then
            'If Not oCls.ExisteRemesa Then
            If Not oCls.FacturaRecepcion(PB2) Then
                FileClose(2)
                Exit Sub
            End If
            'Else
            '   Exit Sub
            'End If
        Else
            oCls.ReenvioErroresFactura(CLng("0" & Trim(TxtEnvioR.Text)))
        End If
        FileClose(2)

        cXML = oCls.XMLs
        If Trim(cXML) <> "" Then
            EscribirXML("F_recibidas_" & CDate(dDesde) & "_" & CDate(dHasta), cXML)
        End If
        txtPath.Text = "/wlpl/SSII-FACT/ws/fr/SiiFactFRV1SOAP"
        FileClose(2)
        MsgBox("Envio finalizado")

    End Sub

    Private Sub CmdFacturasInversion_Click(sender As Object, e As EventArgs) Handles CmdFacturasInversion.Click
        Dim oCls As ClsFacturasRecibidas = New ClsFacturasRecibidas
        Dim dDesde As Date
        Dim dHasta As Date
        Dim Sql As String
        Dim periodo As String
        Dim cXML As String

        dDesde = CDate(DPDesde.value)
        dHasta = CDate(DPHasta.value)
        periodo = Strings.Right("00" & Month(dDesde), 2)
        Sql = " empresa in ('1', '2', '3', '4', '5', '11') and fecha_entrada >= '" & CStr(dDesde) & "' AND fecha_entrada <= '" & CStr(dHasta) & "' AND cif_prov <> 'F46026068'"

        oCls.ObjGlobal = ObjetoGlobal
        oCls.Sql = Sql
        oCls.Ejercicio = "" & Year(dDesde)
        oCls.empresa = "Cooperativa Vinícola de Lliria, S.C.V."
        oCls.periodo = periodo
        oCls.nif = "F46026068"
        oCls.Compensacion = 100

        FileOpen(2, Replace("c:\F_recibidas_" & CDate(dDesde) & "_" & CDate(dHasta), "/", "_") & ".csv", OpenMode.Output)

        If Trim(TxtEnvioR.Text) = "" Then
            'If Not oCls.ExisteRemesa Then
            If Not oCls.FacturaRecepcion(PB2) Then
                FileClose(2)
                Exit Sub
            End If
        Else
            oCls.ReenvioErroresFactura(CLng("0" & Trim(TxtEnvioR.Text)))
        End If
        FileClose(2)

        cXML = oCls.XMLs
        If Trim(cXML) <> "" Then
            EscribirXML("F_recibidas_" & CDate(dDesde) & "_" & CDate(dHasta), cXML)
        End If
        txtPath.Text = "/wlpl/SSII-FACT/ws/fr/SiiFactFRV1SOAP"
        FileClose(2)
        MsgBox("Envio finalizado")

    End Sub

    Private Sub CmdProcesar_Click(sender As Object, e As EventArgs) Handles CmdProcesar.Click
        'Dim f As TextStream
        Dim Cadena As String
        Dim oObj As Object
        'Dim fs As New FileSystemObject
        Dim Linea As String
        Dim objStream, strData

        objStream = CreateObject("ADODB.Stream")
        objStream.Charset = "ISO-8859-1"
        objStream.Open
        TxtNombreXML.Text = Replace(TxtNombreXML.Text, ")", "")
        TxtNombreXML.Text = Replace(TxtNombreXML.Text, "(", "")
        'TxtNombreXML.Text = Replace(TxtNombreXML.Text, " ", "")
        objStream.LoadFromFile(TxtNombreXML.Text)
        Cadena = objStream.ReadText()

        '    'Set fs = New FileSystemObject  'CreateObject("Scripting.FileSystemObject")
        '    Open TxtNombreXML.Text For Binary As #2
        '    'Set f = fs.OpenTextFile(TxtNombreXML.Text, 1, -1)
        '    cadena = ""
        '    Do While Not EOF(2)
        '        Line Input #2, Linea
        '        cadena = cadena + Linea
        '    Loop
        '    Close #2


        If InStr(1, Cadena, "siiR:RespuestaLRFacturasEmitidas") <> 0 Then
            oObj = New ClsContestacionEmitidas
            oObj.ObjetoGlobal = Me.ObjetoGlobal
            oObj.CargaDocumento(Cadena, PBR)
        ElseIf InStr(1, Cadena, "siiR:RespuestaLRFacturasRecibidas") <> 0 Then
            oObj = New ClsContestacionRecibidas
            oObj.ObjetoGlobal = Me.ObjetoGlobal
            oObj.CargaDocumento(Cadena, PBR)
        End If

        MsgBox("Envio finalizado")


    End Sub

    Public Function EscribirXML(fnombre, cXML, Optional cPath = "c:\sii") As Boolean
        Dim escribir As StreamWriter
        Dim fichero As String
        Dim Contador As Integer
        Dim Fic As String

        Try
            Fic = Replace(fnombre, "/", "") & ".xml"
            fichero = cPath & "\" & Fic
            'Fichero = Trim(Replace(Fichero, "-", "_"))
            Contador = 0

            'escribir = New StreamWriter(fichero)

            While Trim(Dir(fichero)) = Trim(Fic)
                Contador = Contador + 1
                Fic = Replace(fnombre, "/", "") & "(" & Contador & ")" & ".xml"
                fichero = cPath & "\" & Fic
                'Fichero = Trim(Replace(Fichero, "-", "_"))
            End While
            escribir = New StreamWriter(fichero, True)
            escribir.Write(cXML)
            escribir.Flush()
            escribir.Close()
            escribir = Nothing
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

    Private Sub CmdClipBoard_Click(sender As Object, e As EventArgs) Handles CmdClipBoard.Click

    End Sub

End Class