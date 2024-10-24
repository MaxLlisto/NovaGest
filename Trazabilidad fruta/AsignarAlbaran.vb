Public Class AsignarAlbaran
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public resultado As Integer
    Public NBulto As Integer


    Public Property Documento() As String
        Get
            Return TxtAlbaran.Text
        End Get
        Set(ByVal value As String)
            TxtAlbaran.Text = value
        End Set
    End Property

    Public Property NoBulto() As String
        Get
            Return NBulto
        End Get
        Set(ByVal value As String)
            TxtBulto.Text = value
        End Set
    End Property

    Public Property PonLote() As String
        Get
            Return lblLote.Text
        End Get
        Set(ByVal value As String)
            lblLote.Text = value
        End Set
    End Property

    Private Sub Asignar_Click(sender As Object, e As EventArgs) Handles Asignar.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim rs1 As New cnRecordset.CnRecordset
        Dim Sql As String

        If OpReasignar.Checked Then
            Sql = "SELECT * FROM entradas_bultos WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND numero_albaran=" & TxtAlbaran.Text & " AND bulto=" & TxtBulto.Text
            Rs.Open(Sql, ObjetoGlobal.cn)
            If Rs.EOF Then
                MsgBox("No encuentro este albarán o este albarán no tiene este número de bulto")
                TxtAlbaran.Select()
                Rs.Close()
                Exit Sub
            End If
            Rs.Close()
        End If

        Sql = "select * FROM entradas_lotes_test WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND lote=" & lblLote.Text
        Rs.Open(Sql, ObjetoGlobal.cn, True)

        Sql = "select * FROM entradas_lotes WHERE empresa = '" & ObjetoGlobal.EmpresaActual & "' AND ejercicio='" & ObjetoGlobal.EjercicioActual & "' AND lote=" & lblLote.Text
        rs1.Open(Sql, ObjetoGlobal.cn, True)

        If Not Rs.EOF Then
            If OpReasignar.Checked Then
                If rs1.EOF Then
                    rs1.AddNew()
                    rs1!Empresa = ObjetoGlobal.EmpresaActual
                    rs1!Ejercicio = ObjetoGlobal.EjercicioActual
                    rs1!lote = lblLote.Text
                End If
                rs1!serie_albaran = "N" & CInt(Strings.Right(ObjetoGlobal.EjercicioActual.Trim, 2))
                rs1!numero_albaran = CLng(TxtAlbaran.Text)
                rs1!Bulto = CLng("0" & TxtBulto.Text)
                NBulto = CLng("0" & TxtBulto.Text)
                Documento = TxtAlbaran.Text
                rs1!ignorar_sn = "N"
                rs1.Update()

                Rs!serie_albaran = "N" & CInt(Strings.Right(ObjetoGlobal.EjercicioActual.Trim, 2))
                Rs!numero_albaran = CLng(TxtAlbaran.Text)
                NBulto = CInt("0" & TxtBulto.Text)
                Rs!Bulto = NBulto
                Rs!ignorar_sn = "N"
                Rs.Update()

                resultado = 1

            ElseIf OpPrecalibrado.Checked Then
                If rs1.EOF Then
                    rs1.AddNew()
                    rs1!Empresa = ObjetoGlobal.EmpresaActual
                    rs1!Ejercicio = ObjetoGlobal.EjercicioActual
                    rs1!lote = lblLote.Text
                End If
                'rs1!Bulto = CLng(TxtBulto.Text)
                rs1!palet_precalibrado = CLng(TxtAlbaran.Text)
                rs1!ignorar_sn = "N"
                rs1.Update()

                'Rs!codigo_precalibre = CLng(TxtAlbaran.Text)
                Rs!ignorar_sn = "N"
                Rs.Update()

                Documento = TxtAlbaran.Text
                resultado = 2

            Else
                If rs1.EOF Then
                    rs1.AddNew
                    rs1!Empresa = ObjetoGlobal.empresaactual
                    rs1!Ejercicio = ObjetoGlobal.EjercicioActual
                    rs1!lote = lblLote.Text
                End If
                rs1!ignorar_sn = "S"
                'rs1!serie_albaran = Null
                'rs1!numero_albaran = Null
                'rs1!Bulto = Null
                rs1.Update

                Rs!ignorar_sn = "S"
                'Rs!serie_albaran = Null
                'Rs!numero_albaran = Null
                'Rs!Bulto = Null
                Documento = 0
                NBulto = 0
                Rs.Update
                resultado = 3
            End If
        End If
    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        resultado = 0
        Me.Close()
    End Sub
End Class