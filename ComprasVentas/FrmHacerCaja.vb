Public Class Frmarqueocaja
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Dim nContado As Double
    Dim nCredito As Double
    Dim nTarjeta As Double
    Dim nPendiente As Double

    Private Sub BtCalcular_Click(sender As Object, e As EventArgs) Handles BtCalcular.Click
        Dim RsCabeceraAlb As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim sSql As String

        Dim i As Integer

        nContado = 0
        nCredito = 0
        nTarjeta = 0
        nPendiente = 0

        If TxtCaja.Text.Trim = "" Then
            MsgBox("Hay que consignar número de caja")
        End If
        If Format(Today, "dd/MM/yyyy") <> Format(TxtFecha.Text.Trim, "dd/MM/yyyy") Then
            If MsgBox("La fecha consignada no coorresponde con la fecha de hoy, ¿Es correcto?", vbYesNo) <> MsgBoxResult.Yes Then
                TxtFecha.Text = Today
                Return
            End If
            sSql = "SELECT * FROM cabeceras_albaran WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' and punto_venta=" & TxtCaja.Text & " and fecha_albaran='" & TxtFecha.Text & "'"
            If RsCabeceraAlb.Open(sSql, ObjetoGlobal.cn) Then
                For i = 1 To RsCabeceraAlb.RecordCount
                    If Trim(RsCabeceraAlb!albaran_contado) = "S" Then
                        nContado = nContado + RsCabeceraAlb!total_albaran
                    ElseIf Trim(RsCabeceraAlb!albaran_contado) = "N" Then
                        nCredito = nCredito + RsCabeceraAlb!total_albaran
                    ElseIf Trim(RsCabeceraAlb!albaran_contado) = "P" Then
                        nPendiente = nPendiente + RsCabeceraAlb!total_albaran
                    Else
                        nTarjeta = nTarjeta + RsCabeceraAlb!total_albaran
                    End If
                    RsCabeceraAlb.MoveNext()
                Next
            End If
            RsCabeceraAlb.Close()
            lblContado.Text = Format(nContado, "#,###,##0.00")
            LblCredito.Text = Format(nCredito, "#,###,##0.00")
            LblTarjeta.Text = Format(nTarjeta, "#,###,##0.00")
            LblDiferido.Text = Format(nPendiente, "#,###,##0.00")
            lblTotal.Text = Format((nContado + nCredito + nTarjeta + nPendiente), "#,###,##0.00")
            Return
        End If

    End Sub

    Private Sub BtSalir_Click(sender As Object, e As EventArgs) Handles BtSalir.Click
        Me.Close()
    End Sub

    Private Sub BtImprimir_Click(sender As Object, e As EventArgs) Handles BtImprimir.Click
        Dim oImp As ReportBuilder2.ClsImpresion

        oImp = New ReportBuilder2.ClsImpresion(Me.ObjetoGlobal)
        If Not oImp.Inicializar("caja_dia", "caja_dia", "caja_dia", 1, 1, ObjetoGlobal.cn, "", "", "", "", "") Then
            Return
        End If

        'oImp.CINFEmpresa = Trim(ObjetoGlobal.EmpresaActual)
        'oImp.CINFEjercicio = Trim(ObjetoGlobal.EjercicioActual)
        'oImp.CINFTipoDocumento = "Caja"
        'oImp.CINFSerie = ""
        'oImp.CINFNumeroDocumento = Format(dFecha, "dd/mm/yyyy")
        'oImp.CINFUsuario = Trim(ObjetoGlobal.nombreusuario)

        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.fecha", 0, "" & TxtFecha.Text)
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.caja", 0, "" & TxtCaja.Text)
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.contado", 0, "" & nContado)
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.credito", 0, "" & nCredito)
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.tarjeta", 0, "" & nTarjeta)
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.operaciones", 0, "" & nPendiente)
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.dato1", 0, "" & (nContado + nCredito + nTarjeta + nPendiente)) 'Total
        oImp.VolcarAImpresion(1, 0, -1, 0, "Calculado.dato2", 0, DateTime.Now.ToString("hh:mm:ss"))
        oImp.Imprimir()

    End Sub
End Class