Public Class FrmDuplicarOC
    Public NuevoCodigo As String
    Public Codigocreado As String
    Public Seleccionar As Boolean
    Public RsAnterior As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Public pform As Form
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal


    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property


    Private Sub CmdAceptar_Click() 'NOSTD
        CrearNuevaOrden()
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CmdAceptarEIr_Click()
        CrearNuevaOrden()
        DialogResult = DialogResult.OK
        Seleccionar = True
        Me.Close()
    End Sub

    Private Sub CmdSalir_Click()
        CodigoCreado = ""
        Me.Close()
    End Sub

    Private Sub Form_Load()
        NuevoCodigo = ""
        DTFechaOrden.Text = Date.Now
    End Sub

    Private Sub CrearNuevaOrden()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim i As Integer

        NuevoCodigo = PonerNumeroOrden(CDate(DTFechaOrden.value))
        Rs.Open("SELECT * FROM ORDENES_CONFECCION WHERE 1 = 0", ObjetoGlobal.cn, True)
        Rs.AddNew()
        Rs!empresa = Trim(ObjetoGlobal.empresaActual)
        Rs!numero_orden = Trim(NuevoCodigo)
        Rs!fecha_orden = CDate(DTFechaOrden.value)
        For i = 3 To RsAnterior.CuantosCampos - 1
            If i <> 19 And i <> 23 And i <> 24 And i <> 25 And i <> 26 And i <> 39 And Not (IsDBNull(RsAnterior(i).Value)) Then
                Rs(i).Value = Trim(RsAnterior(i).Value)
            End If
            Rs!Estado = "1"
            Rs!impresa_etiqueta = "N"
        Next
        Rs.Update()
        Rs.Close()

        CodigoCreado = NuevoCodigo
        MsgBox("Se ha creado correctamente la nueva orden de confección.")

    End Sub

    Private Function PonerNumeroOrden(dFecha As String) As String
        Dim sSql As String
        Dim rsOc As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim NumOrden As Integer

        ' Pone número de orden de confección
        sSql = "SELECT Max(Numero_orden) AS Numero FROM ordenes_confeccion WHERE Empresa='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND Fecha_orden='" & dFecha & "'"
        If rsOc.Open(sSql, ObjetoGlobal.cn) Then

            If IsDBNull(rsOc!Numero) Then
                NumOrden = 1
            Else
                NumOrden = CInt(Mid(Format(rsOc!Numero, "00000000"), 7, 2)) + 1
            End If
        Else
            Return ""
        End If
        Return ("" & Format(Year(dFecha) - 2000, "00") & Format(Month(dFecha), "00") & Format(DateAndTime.Day(dFecha), "00") & Format(NumOrden, "00"))

    End Function

    Private Sub Aceptaryseguir_Click(sender As Object, e As EventArgs) Handles Aceptaryseguir.Click
        CrearNuevaOrden()
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Aceptaryver_Click(sender As Object, e As EventArgs) Handles Aceptaryver.Click
        CrearNuevaOrden()
        Seleccionar = True
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub
End Class