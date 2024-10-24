Public Class FrmNEntradasNuevo11
    Public ObjetoGlobal As Object
    Public Campo As Long
    Public Variedad As String
    Dim RsEntradaAs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
    Dim TarasEnvases(9) As Single
    Dim FlagPreguntar As Boolean
    Dim EnvaseEnModificacion As Integer
    Dim PesoNuevo(12) As Long
    Dim FlagCambioEnCodigo As Boolean 'NOSTD
    Dim SocioSeleccionado As Long 'NOSTD
    Dim CampoSeleccionado As Long 'NOSTD
    Dim VariedadSeleccionada As String 'NOSTD
    Dim SerieAlbaran As String
    Dim NumeroAlbaran As Long
    Public SerieSeleccionada As String
    Public AlbaranSeleccionado As Long
    Dim FlagYaSeleccionado As Boolean
    Public oForm As FrmEntradaAlbaranes

    Private Function GrabarEntrada() As Boolean
        Dim RsEntradas As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsEntradasEnvases As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsBultos As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsClasif As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim Campos() As String, Valores() As String
        Dim trans As SqlClient.SqlTransaction

        GrabarEntrada = False

        ObjetoGlobal.cn.Open()
        trans = ObjetoGlobal.cn.BeginTransaction()
        Try

            'If ModoConexion = 1 Then CnLocal.BeginTrans
            'entradas_albaranes
            SQL = "SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
            RsEntradas.Open(SQL, ObjetoGlobal.cn, True,,,,,, trans)
            If RsEntradas.RecordCount = 0 Then
                MsgBox("Error en el albarán a modificar")
                trans.Rollback()
                Return False
            End If

            RsEntradas!codigo_campo = CampoSeleccionado
            RsEntradas!codigo_variedad = VariedadSeleccionada
            RsEntradas!codigo_socio = SocioSeleccionado
            RsEntradas.Update()

            'If ModoConexion = 1 Then GrabarEnLocal RsEntradas, "U"
            trans.Commit()
            ObjetoGlobal.cn.Close()

            '   If ModoConexion = 1 Then CnLocal.CommitTrans
            GrabarEntrada = True
            ReDim Campos(3), Valores(3)
            Campos(0) = "codigo_campo"
            Valores(0) = RsEntradas!codigo_campo
            Campos(1) = "codigo_variedad"
            Valores(1) = RsEntradas!codigo_variedad
            Campos(2) = "codigo_socio"
            Valores(2) = RsEntradas!codigo_socio
            RsEntradas.Close()
            oForm.AsignarValores(oForm.CnTabla01, Campos, Valores)
            MsgBox("Modificada la entrada:" + CStr(AlbaranSeleccionado))
            Return True

        Catch ex As Exception
            trans.Rollback()
            MsgBox("No se puede grabar la entrada." + vbCrLf + "Se ha producido el siguiente mensaje:" + vbCrLf + Trim(ex.Message))
            Return False
        End Try


    End Function

    Private Sub LstCampos_DblClick()
        'KeyAscii = 0
        If Not (LstSocios.SelectedItems Is Nothing) And Not (LstCampos.SelectedItems Is Nothing) Then
            SocioSeleccionado = LstSocios.SelectedItems(0).SubItems(1).Text
            CampoSeleccionado = LstCampos.SelectedItems(0).SubItems(0).Text
            VariedadSeleccionada = LstCampos.SelectedItems(0).SubItems(4).Text
            ActualizarSeleccion()
        Else
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub PicF_Click(Index As Integer)
        Dim Cadena As String

        Cadena = "{F" + CStr(Index) + "}"
        SendKeys.Send(Cadena)
    End Sub

    'Private Sub LstCampos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LstCampos.KeyPress
    '    Static cBusqueda As String = ""
    '    Static nhora As Long
    '    Dim foundItem As ListViewItem

    '    If DateDiff("s", Date.Now, CDate(Format(Date.Now, "dd/MM/yyyy") & " 00:01")) > nhora + 5 Then
    '        nhora = DateDiff("s", Date.Now, CDate(Format(Date.Now, "dd/MM/yyyy") & " 00:01"))
    '        cBusqueda = ""
    '    End If
    '    cBusqueda = cBusqueda + e.KeyChar
    '    foundItem = LstCampos.FindItemWithText(cBusqueda, False, 0, True)

    '    If foundItem IsNot Nothing Then
    '        LstCampos.TopItem = foundItem
    '    End If

    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        If Not (LstSocios.SelectedItems Is Nothing) And (LstCampos.SelectedItems Is Nothing) Then
    '            SocioSeleccionado = LstSocios.SelectedItems.Item(1).Text
    '            CampoSeleccionado = LstCampos.SelectedItems.Item(0).Text
    '            VariedadSeleccionada = LstCampos.SelectedItems.Item(4).Text
    '            ActualizarSeleccion()
    '        Else
    '            SendKeys.Send("{TAB}")
    '        End If
    '    End If


    'End Sub
    Private Sub TxtCodigoSocio_Change()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If FlagCambioEnCodigo = True Then
            If Trim(TxtCodigosocio.Text) = "" Then
                Txtnombre.Text = ""
            Else
                Rs.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(TxtCodigosocio.Text), ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    Txtnombre.Text = ""
                Else
                    Txtnombre.Text = Trim("" & Rs!apellidos_socio) + " " + Trim("" & Rs!nombre_socio)
                End If
                Rs.Close()
            End If
        End If
        If SocioSeleccionado > -1 Then BorrarDatos()
    End Sub
    'Private Sub PoblarListaSocios(Cadena As String)
    '    Dim i As Long, j As Long, P1 As Long, P2 As Long, PM As Long
    '    Dim Aceptable As Boolean, Primero As Boolean, Item As ListViewItem

    '    LstSocios.Items.Clear()
    '    LstCampos.Items.Clear()
    '    LblDatosSeleccionado.Text = ""
    '    LstProceso.Items.Clear()
    '    If Trim(Cadena) = "" Then
    '        Return
    '    End If
    '    If CuantosSocios = 0 Then
    '        Return
    '    End If
    '    P1 = 0 : P2 = CuantosSocios
    '    LstProceso.Items.Add("busco: " + Trim(Txtnombre.Text))

    '    While (P1 <= P2 And Primero = False)
    '        PM = Fix((P1 + P2) / 2)
    '        LstProceso.Items.Add(CStr(P1) + " " + CStr(P2) + " " + CStr(PM))
    '        LstProceso.Items.Add(UCase(ReferenciaSocio(PM)))
    '        Aceptable = False : Primero = False
    '        If Comparar(Strings.Left(UCase(ReferenciaSocio(PM)), Len(Cadena)), UCase(Cadena)) = 0 Then
    '            Aceptable = True
    '            If PM = 1 Then
    '                Primero = True
    '            Else
    '                If Comparar(Strings.Left(UCase(ReferenciaSocio(PM - 1)), Len(Cadena)), UCase(Cadena)) <> 0 Then Primero = True
    '            End If
    '        End If
    '        If Aceptable = True And Primero = True Then
    '            P1 = PM : P2 = PM
    '        Else
    '            If Comparar(Strings.Left(UCase(ReferenciaSocio(PM)), Len(Cadena)), UCase(Cadena)) >= 0 Then
    '                P2 = PM - 1
    '            Else
    '                P1 = PM + 1
    '            End If
    '        End If
    '        LstProceso.Items.Add("Aceptable = " + CStr(Aceptable) + " Primero= " + CStr(Primero))
    '        LstProceso.Items.Add("")
    '    End While


    '    LstProceso.Items.Add(CStr(PM) + " PRIMERO = " + CStr(Primero))
    '    LstProceso.Items.Add(ReferenciaSocio(PM))
    '    If PM > 1 Then
    '        LstProceso.Items.Add(ReferenciaSocio(PM - 1))
    '    End If

    '    If Primero = True Then
    '        For j = PM To CuantosSocios
    '            If Comparar(Strings.Left(UCase(ReferenciaSocio(j)), Len(Cadena)), UCase(Cadena)) = 0 Then
    '                Item = LstSocios.Items.Add(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
    '                Item.SubItems.Add(CStr(CLng(Strings.Mid(ReferenciaSocio(j), 81))))
    '            Else
    '                Exit For
    '            End If
    '        Next
    '        LstSocios.Items(1).Selected = True
    '        PoblarListaCampos(CLng(Strings.Mid(ReferenciaSocio(PM), 81)), Trim(Strings.Left(ReferenciaSocio(PM), 80)), "")
    '    End If

    'End Sub
    'Private Function Comparar(Cadena1 As String, Cadena2 As String) As Integer
    '    Dim C1 As String, C2 As String, j1 As Integer, j2 As Integer

    '    C1 = Cadena1
    '    j1 = InStr(1, C1, Chr(209))
    '    Do While j1 > 0
    '        If j1 = 1 Then
    '            C1 = "Nz" + Mid(C1, 2)
    '        ElseIf j1 = Len(C1) Then
    '            C1 = Strings.Left(C1, j1 - 1) + "Nz"
    '        Else
    '            C1 = Strings.Left(C1, j1 - 1) + "Nz" + Mid(C1, j1 + 1)
    '        End If
    '        j1 = InStr(1, C1, Chr(209))
    '    Loop
    '    C2 = Cadena2
    '    j2 = InStr(1, C2, Chr(209))
    '    Do While j2 > 0
    '        If j2 = 1 Then
    '            C2 = "Nz" + Mid(C2, 2)
    '        ElseIf j2 = Len(C2) Then
    '            C2 = Strings.Left(C2, j2 - 1) + "Nz"
    '        Else
    '            C2 = Strings.Left(C2, j2 - 1) + "Nz" + Mid(C2, j2 + 1)
    '        End If
    '        j2 = InStr(1, C2, Chr(209))
    '    Loop
    '    If C1 < C2 Then
    '        Comparar = -1
    '    ElseIf C1 = C2 Then
    '        Comparar = 0
    '    Else
    '        Comparar = 1
    '    End If
    'End Function
    'Private Sub PoblarListaCampos(Socio As Long, Nombre As String, Variedad As String)
    '    Dim i As Long, j As Long, P1 As Long, P2 As Long, PM As Long, Cadena As String
    '    Dim Aceptable As Boolean, Primero As Boolean, Item As ListViewItem

    '    LstCampos.Items.Clear()
    '    LstProceso.Items.Clear()

    '    If Socio <= 0 Then Exit Sub

    '    LblDatosSeleccionado.Text = "Socio:" + CStr(Socio) + " - " + Trim(Nombre)
    '    P1 = 0
    '    P2 = CuantosCampos
    '    Cadena = Format(Socio, "0000000000")
    '    LstProceso.Items.Add("busco: " + CStr(Socio) + " " + Trim(Variedad))

    '    Do While (P1 <= P2 And Primero = False)
    '        PM = Fix((P1 + P2) / 2)
    '        LstProceso.Items.Add(CStr(P1) + " " + CStr(P2) + " " + CStr(PM))
    '        LstProceso.Items.Add(UCase(ReferenciaCampo(PM)))
    '        Aceptable = False : Primero = False
    '        If Comparar(Strings.Left(UCase(ReferenciaCampo(PM)), 10), UCase(Cadena)) = 0 Then
    '            Aceptable = True
    '            If PM = 1 Then
    '                Primero = True
    '            Else
    '                If Comparar(Strings.Left(UCase(ReferenciaCampo(PM - 1)), 10), UCase(Cadena)) <> 0 Then Primero = True
    '            End If
    '        End If
    '        If Aceptable = True And Primero = True Then
    '            P1 = PM : P2 = PM
    '        Else
    '            If Comparar(Strings.Left(UCase(ReferenciaCampo(PM)), 10), UCase(Cadena)) >= 0 Then
    '                P2 = PM - 1
    '            Else
    '                P1 = PM + 1
    '            End If
    '        End If
    '        LstProceso.Items.Add("Aceptable = " + CStr(Aceptable) + " Primero= " + CStr(Primero))
    '        LstProceso.Items.Add("")
    '    Loop

    '    LstProceso.Items.Add(CStr(PM) + " PRIMERO = " + CStr(Primero))
    '    LstProceso.Items.Add(ReferenciaCampo(PM))
    '    If PM > 1 Then
    '        LstProceso.Items.Add(ReferenciaCampo(PM - 1))
    '    End If

    '    If Primero = True Then
    '        For j = PM To CuantosCampos
    '            If Comparar(Strings.Left(UCase(ReferenciaCampo(j)), 10), UCase(Cadena)) = 0 Then
    '                Item = LstCampos.Items.Add(CStr(CLng(Mid(ReferenciaCampo(j), 21))))
    '                Item.SubItems.Add(Mid(ReferenciaCampo(j), 11, 10) + " " + Trim(Mid(DatosCampo(j), 78)))
    '                Item.SubItems.Add(Trim(Mid(DatosCampo(j), 11, 60)))
    '                Item.SubItems.Add(CStr(CDbl(Mid(DatosCampo(j), 71, 7))))
    '                Item.SubItems.Add(Trim(Mid(ReferenciaCampo(j), 11, 10)))
    '            Else
    '                Exit For
    '            End If
    '        Next
    '    End If


    'End Sub

    Private Sub LstCampos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LstCampos.KeyPress
        Static cBusqueda As String = ""
        Static nhora As Long
        Dim foundItem As ListViewItem

        If LstCampos.Items.Count = 0 Then
            Return
        End If

        If DateDiff("s", Date.Now, CDate(Format(Date.Now, "dd/MM/yyyy") & " 00:01")) > nhora + 5 Then
            nhora = DateDiff("s", Date.Now, CDate(Format(Date.Now, "dd/MM/yyyy") & " 00:01"))
            cBusqueda = ""
        End If
        cBusqueda = cBusqueda + e.KeyChar

        foundItem = LstCampos.FindItemWithText(cBusqueda, False, 0, True)

        If foundItem IsNot Nothing Then
            LstCampos.TopItem = foundItem
        End If

        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            If (Not LstSocios.SelectedItems Is Nothing) And (Not LstCampos.SelectedItems Is Nothing) Then

                SocioSeleccionado = LstSocios.SelectedItems(0).SubItems(1).Text
                CampoSeleccionado = LstCampos.SelectedItems(0).Text
                VariedadSeleccionada = LstCampos.SelectedItems(0).SubItems(4).Text
                'If Not ControlAgendayplazo(CampoSeleccionado, VariedadSeleccionada) Then
                '    Exit Sub
                'End If

                ActualizarSeleccion()
                'VerTramitesPrevios()
                'TxtPeriodo.Focus()
            End If
        End If


    End Sub
    Private Function Comparar(Cadena1 As String, Cadena2 As String) As Integer
        Dim C1 As String, C2 As String, j1 As Integer, j2 As Integer

        C1 = Cadena1
        j1 = InStr(1, C1, Chr(209))

        Do While j1 > 0
            If j1 = 1 Then
                C1 = "Nz" + Mid(C1, 2)
            ElseIf j1 = Len(C1) Then
                C1 = Strings.Left(C1, j1 - 1) + "Nz"
            Else
                C1 = Strings.Left(C1, j1 - 1) + "Nz" + Strings.Mid(C1, j1 + 1)
            End If
            j1 = InStr(1, C1, Chr(209))
        Loop

        C2 = Cadena2
        j2 = InStr(1, C2, Chr(209))

        Do While j2 > 0
            If j2 = 1 Then
                C2 = "Nz" + Mid(C2, 2)
            ElseIf j2 = Len(C2) Then
                C2 = Strings.Left(C2, j2 - 1) + "Nz"
            Else
                C2 = Strings.Left(C2, j2 - 1) + "Nz" + Strings.Mid(C2, j2 + 1)
            End If
            j2 = InStr(1, C2, Chr(209))
        Loop
        If C1 < C2 Then

            Return -1
        ElseIf C1 = C2 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Private Sub PoblarListaCampos(Socio As Long, Nombre As String, Variedad As String)
        Dim i As Long, j As Long, P1 As Long, P2 As Long, PM As Long, Cadena As String
        Dim Aceptable As Boolean, Primero As Boolean, Item As ListViewItem

        LstCampos.Items.Clear()
        LstProceso.Items.Clear()

        If Socio <= 0 Then Exit Sub

        LblDatosSeleccionado.Text = "Socio:" + CStr(Socio) + " - " + Trim(Nombre)
        P1 = 0
        P2 = CuantosCampos
        Cadena = Format(Socio, "0000000000")
        LstProceso.Items.Add("busco: " + CStr(Socio) + " " + Trim(Variedad))

        Do While (P1 <= P2 And Primero = False)
            PM = Fix((P1 + P2) / 2)
            LstProceso.Items.Add(CStr(P1) + " " + CStr(P2) + " " + CStr(PM))
            LstProceso.Items.Add(UCase(ReferenciaCampo(PM)))
            Aceptable = False : Primero = False
            If Comparar(Strings.Left(UCase(ReferenciaCampo(PM)), 10), UCase(Cadena)) = 0 Then
                Aceptable = True
                If PM = 1 Then
                    Primero = True
                Else
                    If Comparar(Strings.Left(UCase(ReferenciaCampo(PM - 1)), 10), UCase(Cadena)) <> 0 Then Primero = True
                End If
            End If
            If Aceptable = True And Primero = True Then
                P1 = PM : P2 = PM
            Else
                If Comparar(Strings.Left(UCase(ReferenciaCampo(PM)), 10), UCase(Cadena)) >= 0 Then
                    P2 = PM - 1
                Else
                    P1 = PM + 1
                End If
            End If
            LstProceso.Items.Add("Aceptable = " + CStr(Aceptable) + " Primero= " + CStr(Primero))
            LstProceso.Items.Add("")
        Loop

        LstProceso.Items.Add(CStr(PM) + " PRIMERO = " + CStr(Primero))
        LstProceso.Items.Add(ReferenciaCampo(PM))
        If PM > 1 Then
            LstProceso.Items.Add(ReferenciaCampo(PM - 1))
        End If

        If Primero = True Then
            For j = PM To CuantosCampos
                If Comparar(Strings.Left(UCase(ReferenciaCampo(j)), 10), UCase(Cadena)) = 0 Then
                    Item = New ListViewItem(CStr(CLng(Mid(ReferenciaCampo(j), 21))))
                    Item.SubItems.Add(Mid(ReferenciaCampo(j), 11, 10) + " " + Trim(Mid(DatosCampo(j), 78)))
                    Item.SubItems.Add(Trim(Mid(DatosCampo(j), 11, 60)))
                    Item.SubItems.Add(CStr(CDbl(Mid(DatosCampo(j), 71, 7))))
                    Item.SubItems.Add(Trim(Mid(ReferenciaCampo(j), 11, 10)))
                    LstCampos.Items.Add(Item)
                Else
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub PoblarListaSocios(Cadena As String)
        Dim j As Long
        Dim P1 As Long
        Dim P2 As Long
        Dim PM As Long
        Dim Aceptable As Boolean
        Dim Primero As Boolean
        Dim Item As ListViewItem

        LstSocios.Items.Clear()
        LstCampos.Items.Clear()
        LblDatosSeleccionado.Text = ""
        LstProceso.Items.Clear()

        If Trim(Cadena) = "" Then
            Return
        End If
        If CuantosSocios = 0 Then
            Return
        End If

        P1 = 0 : P2 = CuantosSocios
        LstProceso.Items.Add("busco: " + Trim(Txtnombre.Text))

        While (P1 <= P2 And Primero = False)
            PM = Fix((P1 + P2) / 2)
            LstProceso.Items.Add(CStr(P1) + " " + CStr(P2) + " " + CStr(PM))
            LstProceso.Items.Add(UCase(ReferenciaSocio(PM)))
            Aceptable = False : Primero = False
            If Comparar(Strings.Left(UCase(ReferenciaSocio(PM)), Len(Cadena)), UCase(Cadena)) = 0 Then
                Aceptable = True
                If PM = 1 Then
                    Primero = True
                Else
                    If Comparar(Strings.Left(UCase(ReferenciaSocio(PM - 1)), Len(Cadena)), UCase(Cadena)) <> 0 Then Primero = True
                End If
            End If
            If Aceptable = True And Primero = True Then
                P1 = PM : P2 = PM
            Else
                If Comparar(Strings.Left(UCase(ReferenciaSocio(PM)), Len(Cadena)), UCase(Cadena)) >= 0 Then
                    P2 = PM - 1
                Else
                    P1 = PM + 1
                End If
            End If
            LstProceso.Items.Add("Aceptable = " + CStr(Aceptable) + " Primero= " + CStr(Primero))
            LstProceso.Items.Add("")
        End While

        LstProceso.Items.Add(CStr(PM) + " PRIMERO = " + CStr(Primero))
        LstProceso.Items.Add(ReferenciaSocio(PM))

        If PM > 1 Then LstProceso.Items.Add(ReferenciaSocio(PM - 1))

        If Primero = True Then
            For j = PM To CuantosSocios
                If Comparar(Strings.Left(UCase(ReferenciaSocio(j)), Len(Cadena)), UCase(Cadena)) = 0 Then
                    'Item = LstSocios.Items.Add(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
                    'Item.SubItems.Add(CStr(CLng(Strings.Mid(ReferenciaSocio(j), 81))))
                    'Item = LstSocios.Items.Add(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
                    Item = New ListViewItem(Trim(Strings.Left(ReferenciaSocio(j), 40)) + " " + Trim(Strings.Mid(ReferenciaSocio(j), 41, 40)))
                    Item.SubItems.Add(CStr(CLng(Strings.Mid(ReferenciaSocio(j), 81))))
                    LstSocios.Items.Add(Item)
                Else
                    Exit For
                End If
            Next
            LstSocios.Items(1).Selected = True
            PoblarListaCampos(CLng(Strings.Mid(ReferenciaSocio(PM), 81)), Trim(Strings.Left(ReferenciaSocio(PM), 80)), "")
        End If

    End Sub

    Private Sub Txtnombre_TextChanged(sender As Object, e As EventArgs) Handles Txtnombre.TextChanged

        If Trim(TxtCodigosocio.Text) > "" And FlagCambioEnCodigo = False Then
            TxtCodigosocio.Text = ""
        End If
        PoblarListaSocios(Trim(Txtnombre.Text))
        If SocioSeleccionado > -1 Then
            BorrarDatos()
        End If

    End Sub

    Private Sub ActualizarSeleccion()
        Dim RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, SQL As String

        RsSocio.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(SocioSeleccionado), ObjetoGlobal.cn)
        If RsSocio.RecordCount = 0 Then
            MsgBox("Socio seleccionado inexistente")
            BorrarDatos()
            Exit Sub
        End If
        LblSocioSeleccionado.Text = CStr(RsSocio!codigo_socio) + " - " + Trim("" & RsSocio!apellidos_socio) + ", " + Trim("" & RsSocio!nombre_socio)

        SQL = "SELECT CU.ALTA_BAJA,CU.CODIGO_CAMPO,CA.CODIGO_SOCIO,CA.ALTA_BAJA AS ALTA_BAJA_CAMPO,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD,CU.HANEGADAS"
        SQL = Trim(SQL) + " FROM CULTIVOS CU JOIN CAMPOS CA ON CA.EMPRESA = CU.EMPRESA  AND CA.CODIGO_CAMPO = CU.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = CU.EMPRESA AND V.CODIGO_VARIEDAD = CU.CODIGO_VARIEDAD"
        'SQL = Trim(SQL) + " JOIN SOCIOS_COOP S  ON S.CODIGO_SOCIO = CA.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = CU.EMPRESA AND SC.CODIGO_SITUACION = CA.SITUACION_CAMPO"
        SQL = Trim(SQL) + " WHERE CU.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "'AND CU.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CU.CODIGO_CAMPO = " + CStr(CampoSeleccionado) + " AND CU.CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'"
        RsCampo.Open(SQL, ObjetoGlobal.cn)
        If RsCampo.RecordCount = 0 Then
            MsgBox("Campo seleccionado inexistente")
            BorrarDatos()
            Exit Sub
        End If
        If RsCampo!codigo_socio <> SocioSeleccionado Then
            MsgBox("Error en la asociación socio-campo en campo seleccionado.")
            BorrarDatos()
            Exit Sub
        End If
        If RsCampo!alta_baja <> "A" Then
            MsgBox("Campo (cultivo) seleccionado en situacion de BAJA.")
            BorrarDatos()
            Exit Sub
        End If
        If RsCampo!alta_baja_Campo <> "A" Then
            MsgBox("Campo seleccionado en situacion de BAJA.")
            BorrarDatos()
            Exit Sub
        End If
        LblCampoVariedadSeleccionado.Text = CStr(RsCampo!codigo_campo) + " - " + Trim("" & RsCampo!descripcion_situacion) + " (" + Trim("" & RsCampo!Hanegadas) + " Haneg.) " + Trim(VariedadSeleccionada) + " - " + Trim(RsCampo!descripcion_variedad)
        LstSocios.Items.Clear()
        LstCampos.Items.Clear()
    End Sub
    Private Sub BorrarDatos()
        SocioSeleccionado = -1
        CampoSeleccionado = -1
        VariedadSeleccionada = ""
        LblSocioSeleccionado.Text = ""
        LblCampoVariedadSeleccionado.Text = ""
    End Sub

    Private Function Comprobacion() As Boolean
        Dim RsOperario As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsSocio As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsCampo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsPeriodo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsFecha As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsCultivo As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, RsVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset, PesoSin As Long, Cajas As Integer, i As Integer

        'Socio
        If SocioSeleccionado <= 0 Then
            MsgBox("No se ha indicado socio.")
            Return False
        End If

        RsSocio.Open("SELECT * FROM SOCIOS_COOP WHERE CODIGO_SOCIO = " + CStr(SocioSeleccionado), ObjetoGlobal.cn)
        If RsSocio.RecordCount = 0 Then
            MsgBox("Socio inexistente")
            Return False
        End If
        'Campo
        If CampoSeleccionado <= 0 Then
            MsgBox("No se ha indicado campo.")
            Return False
        End If

        RsCampo.Open("SELECT * FROM CAMPOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_CAMPO = " + CStr(CampoSeleccionado), ObjetoGlobal.cn)
        If RsCampo.RecordCount = 0 Then
            MsgBox("Campo inexistente")
            Return False
        End If
        If RsCampo!alta_baja <> "A" Then
            MsgBox("El campo seleccionado no está dado de alta.")
            Return False
        End If
        If RsCampo!codigo_socio <> SocioSeleccionado Then
            MsgBox("El campo seleccionado no corresponde al proveedor indicado.")
            Return False
        End If
        RsCampo.Close()
        'Variedad
        If Trim(VariedadSeleccionada) = "" Then
            MsgBox("No se ha indicado variedad.")
            Return False
        End If
        RsVariedad.Open("SELECT * FROM VARIEDADES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'", ObjetoGlobal.cn)
        If RsVariedad.RecordCount = 0 Then
            MsgBox("Variedad inexistente")
            Return False
        End If
        RsVariedad.Close()
        'Cultivo
        RsCultivo.Open("SELECT * FROM CULTIVOS WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_CAMPO = " + CStr(CampoSeleccionado) + " AND CODIGO_VARIEDAD = '" + Trim(VariedadSeleccionada) + "'", ObjetoGlobal.cn)
        If RsCultivo.RecordCount = 0 Then
            MsgBox("Cultivo inexistente")
            Return False
        End If
        If RsCultivo!alta_baja <> "A" Then
            MsgBox("El cultivo seleccionado no está dado de alta.")
            Return False
        End If
        RsCultivo.Close()
        Return True
    End Function
    Private Sub TxtCodigoSocio_GotFocus()
        If FlagYaSeleccionado = True Then
            FlagYaSeleccionado = False
            '    TxtPesoBrutoCON.SetFocus
        End If
    End Sub

    Private Function Salida()
        If FlagPreguntar = True Then
            If MsgBox("¿Desea salir SIN modificar los datos socio/campo/cultivo?", vbYesNo) = vbYes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Function

    Private Sub FrmNEntradasNuevo11_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SQL As String
        Dim RsEntrada As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        FlagPreguntar = True
        If AlbaranSeleccionado <= 0 Or Trim(SerieSeleccionada) = "" Then
            MsgBox("No se ha elegido albarán")
            FlagPreguntar = False
            Salida()
        End If

        cabeceras()

        SQL = "SELECT E.*,S.*,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD,P.DESCRIPCION_PER AS DESCRIPCION_PERIODO "
        SQL = Trim(SQL) + " FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " JOIN PERIODOS_COOP P ON P.EMPRESA = E.EMPRESA AND P.EJERCICIO = E.EJERCICIO AND P.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD AND P.CODIGO_PERIODO = E.CODIGO_PERIODO"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND E.SERIE_ALBARAN = '" + Trim(SerieSeleccionada) + "' and NUMERO_ALBARAN = " + CStr(AlbaranSeleccionado)
        RsEntrada.Open(SQL, ObjetoGlobal.cn)
        If RsEntrada.RecordCount = 0 Then
            FlagPreguntar = False
            MsgBox("El albarán seleccionado es inexistente")
            Salida()
        End If
        Me.KeyPreview = True
        lblAlbaran.Text = CStr(RsEntrada!numero_albaran)
        lblFecha.Text = Format(RsEntrada!fecha_entrada, "dd/MM/yyyy") + " " + Strings.Left(RsEntrada!hora_entrada, 5)
        lblSocio.Text = CStr(RsEntrada!codigo_socio) + " " + Trim("" & RsEntrada!apellidos_socio) + ", " + Trim("" & RsEntrada!nombre_socio)
        lblCampo.Text = CStr(RsEntrada!codigo_campo) + " " + Trim(RsEntrada!descripcion_situacion)
        lblVariedad.Text = CStr(RsEntrada!codigo_variedad) + " " + Trim(RsEntrada!descripcion_variedad)
        lblCapataz.Text = CStr(RsEntrada!Capataz) + " " + Trim(RsEntrada!NOMBRE_CAPATAZ)
        lblTransportista.Text = CStr(RsEntrada!Transportista) + " " + Trim(RsEntrada!nombre_transportista)
        lblPeriodo.Text = CStr(RsEntrada!codigo_periodo) + " " + Trim(RsEntrada!descripcion_periodo)

        Campo = CStr(RsEntrada!codigo_campo)
        Variedad = CStr(RsEntrada!codigo_variedad)

        CampoSeleccionado = 6
        SocioSeleccionado = 0
        VariedadSeleccionada = ""

        TxtCodigosocio.Focus()
    End Sub

    Private Function cabeceras()

        'AlbaranSeleccionado = -1
        'lstAlbaranes.ColumnHeader.Clear
        LstSocios.View = View.Details

        Dim columnHeader1 As ColumnHeader = New ColumnHeader()
        columnHeader1.Text = "Nombre"
        columnHeader1.TextAlign = HorizontalAlignment.Left
        columnHeader1.Width = 400
        LstSocios.Columns.Add(columnHeader1)

        Dim columnHeader2 As ColumnHeader = New ColumnHeader()
        columnHeader2.Text = "Código"
        columnHeader2.TextAlign = HorizontalAlignment.Left
        columnHeader2.Width = 100
        LstSocios.Columns.Add(columnHeader2)
        LstSocios.FullRowSelect = True
        LstSocios.MultiSelect = False


        Dim columnHeader3 As ColumnHeader = New ColumnHeader()
        columnHeader3.Text = "Código"
        columnHeader3.TextAlign = HorizontalAlignment.Left
        columnHeader3.Width = 100
        LstCampos.Columns.Add(columnHeader3)

        Dim columnHeader4 As ColumnHeader = New ColumnHeader()
        columnHeader4.Text = "Variedad"
        columnHeader4.TextAlign = HorizontalAlignment.Left
        columnHeader4.Width = 100
        LstCampos.Columns.Add(columnHeader4)

        Dim columnHeader5 As ColumnHeader = New ColumnHeader()
        columnHeader5.Text = "Situación"
        columnHeader5.TextAlign = HorizontalAlignment.Left
        columnHeader5.Width = 100
        LstCampos.Columns.Add(columnHeader5)

        Dim columnHeader6 As ColumnHeader = New ColumnHeader()
        columnHeader6.Text = "Hanegadas"
        columnHeader6.TextAlign = HorizontalAlignment.Left
        columnHeader6.Width = 100
        LstCampos.Columns.Add(columnHeader6)

        Dim columnHeader7 As ColumnHeader = New ColumnHeader()
        columnHeader7.Text = "CodigoVariedad"
        columnHeader7.TextAlign = HorizontalAlignment.Left
        columnHeader7.Width = 100
        LstCampos.Columns.Add(columnHeader7)
        LstCampos.FullRowSelect = True
        LstCampos.MultiSelect = False

        Return True


    End Function

    Private Sub Grabar_Click(sender As Object, e As EventArgs) Handles Grabar.Click
        If Comprobacion() = True Then
            GrabarEntrada()
            FlagPreguntar = False
            Salida()
        End If

    End Sub

    Private Sub LstSocios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstSocios.SelectedIndexChanged
        Actualizarcampos()
    End Sub

    Private Sub LstSocios_MouseClick(sender As Object, e As MouseEventArgs) Handles LstSocios.MouseClick
        Actualizarcampos()
    End Sub


    Private Sub LstSocios_Click(sender As Object, e As EventArgs) Handles LstSocios.Click
        Actualizarcampos()
    End Sub

    Private Sub Actualizarcampos()
        If LstSocios.Items.Count > 0 Then
            If Not LstSocios.SelectedItems Is Nothing And LstSocios.SelectedItems.Count > 0 Then
                'PoblarListaCampos(CLng(LstSocios.Items(1).SubItems(1)), LstSocios.Items(1), "")
                PoblarListaCampos(CLng(LstSocios.SelectedItems(0).SubItems(1).Text), LstSocios.SelectedItems(0).Text, "")
            End If
        End If

    End Sub

    Private Sub TxtCodigosocio_GotFocus(sender As Object, e As EventArgs) Handles TxtCodigosocio.GotFocus
        If FlagYaSeleccionado = True Then
            FlagYaSeleccionado = False
        End If

    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Salida()
    End Sub
End Class