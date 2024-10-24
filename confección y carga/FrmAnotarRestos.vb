Public Class FrmAnotarRestos
    Public Orden As Long
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public calibre As String
    Public dFecha As Date
    Public strPath As String
    Dim nCount As Integer
    Dim nCajas As Integer

    Private Sub AnotarRestos_Click(sender As Object, e As EventArgs) Handles AnotarRestos.Click
        Dim RsRestos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If CInt(Trim("0" & TxtNumeroCajas.Text)) < 1 Then
            MsgBox("Hay que indicar el número de cajas")
            Return
        End If
        If nCount < 1 Then
            RsRestos.Open("SELECT max(contador) as cont FROM ord_confec_restos WHERE EMPRESA='" + ObjetoGlobal.EmpresaActual + "' AND numero_orden =" & Orden, ObjetoGlobal.cn)
            If RsRestos.RecordCount > 0 Then
                If IsDBNull(RsRestos!Cont) Then
                    nCount = 1
                Else
                    nCount = RsRestos!Cont + 1
                End If
            Else
                nCount = 1
            End If
            RsRestos.Close()

            RsRestos.Open("SELECT * FROM ord_confec_restos WHERE 1=0", ObjetoGlobal.cn, True)
            RsRestos.AddNew()
            RsRestos!empresa = ObjetoGlobal.EmpresaActual
            RsRestos!numero_orden = Orden
            RsRestos!Contador = nCount

            RsRestos!fecha_orden = dFecha 'Date.Now
            RsRestos!Cajas = CInt(Trim("0" & TxtNumeroCajas.Text))
            RsRestos!calibre_comercial = Trim(calibre)
            RsRestos!Situacion = "N"
            RsRestos.Update()
            RsRestos.Close()
            nCajas = CInt(Trim("0" & TxtNumeroCajas.Text))
        Else
            If nCajas <> CInt(TxtNumeroCajas.Text) Then
                MsgBox("Ya se ha grabado el palet con " & nCajas & " no se puede grabar con distinto número de cajas, cierre la ventana y vuelvala a abrir")
                Return
            End If
        End If

        Imprimir(Orden, nCount)

    End Sub


    Private Sub Imprimir(orden, numero)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim codOrden As String
        Dim ObjDoc As Object
        Dim strPath As String


        Rs.Open("SELECT o.*, v.descripcion as descripcion_variedad FROM ordenes_confeccion o LEFT JOIN variedades v on v.empresa = o.empresa AND v.codigo_variedad = o.codigo_variedad WHERE o.EMPRESA='" + ObjetoGlobal.EmpresaActual + "' AND o.numero_orden =" & orden, ObjetoGlobal.cn)

        strPath = GetFilePath

        If Rs.RecordCount > 0 Then

            codOrden = Strings.Right("00" & numero, 2)
            codOrden = "" & orden & codOrden

            ObjDoc = CreateObject("bpac.Document")
            If (ObjDoc.Open(strPath) <> False) Then
                ObjDoc.GetObject("cb").Text = "" & orden
                ObjDoc.GetObject("no").Text = codOrden
                ObjDoc.GetObject("bultos").Text = TxtNumeroCajas.Text
                ObjDoc.GetObject("variedad").Text = Trim(Rs!codigo_variedad) & " - " & Trim(Rs!descripcion_variedad)
                ObjDoc.GetObject("calibre").Text = Rs!calibre_comercial
                ObjDoc.GetObject("fecha").Text = Format(Date.Now, "dd/MM/yyyy")
                ObjDoc.StartPrint("", bpac.PrintOptionConstants.bpoDefault)
                ObjDoc.PrintOut(1, bpac.PrintOptionConstants.bpoDefault)
                ObjDoc.EndPrint
                ObjDoc.Close
            End If
        Else
            MsgBox("Error localizando la orden " & codOrden)
        End If
        Rs.Close
    End Sub

    Private Sub FrmAnotarRestos_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblOrden.Text = CStr(Orden)
    End Sub

    Public Function GetFilePath() As String
        Return "\\srvv02\pt\palet_restos.lbx"
    End Function


End Class