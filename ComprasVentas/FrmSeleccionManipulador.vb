Public Class FrmSeleccionManipulador
    Public ObjetoGlobal As Object
    Public nif_titular As String
    Public codigo_cliente As Long
    Public nif_retorno As String
    Public nombre_retorno As String
    Public Seleccion_sn As String
    Public Contado_sn As String

    Private Sub GrabarContado_Click(sender As Object, e As EventArgs) Handles GrabarContado.Click
        Aceptar()
        If Trim(nif_retorno) = "" Then
            MsgBox("Debe realizar una selección")
            Return
        End If
        Contado_sn = "S"
        Seleccion_sn = "S"
        DialogResult = DialogResult.OK
    End Sub

    Sub Aceptar()
        Dim no As Integer

        no = 0
        nif_retorno = ""
        nombre_retorno = ""

        For Each item As ListViewItem In lv.Items
            If item.Checked Then
                no = no + 1
                nif_retorno = item.Text 'lv.Items.lv.SelectedItem
                nombre_retorno = item.SubItems(1).Text
            End If
        Next
        If no > 1 Then
            MsgBox("Debe seleccionar únicamente una persona")
            Return
        End If
        If no = 0 Then
            MsgBox("Debe seleccionar una persona")
            Return
        End If

    End Sub

    Sub Cargar_ListView()
        Dim item As ListViewItem
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim HaySocio As Boolean

        HaySocio = False

        Try
            Seleccion_sn = "N"
            lv.Items.Clear()

            SQL = "SELECT * FROM carnet_manipulador WHERE dni = '" & Trim(nif_titular) & "'"
            Rs.Open(SQL, ObjetoGlobal.cn)
            If Not Rs.EOF Then ' El títular de la factura tiene el carnet
                If Rs!valido_hasta >= Now.Date Then
                    item = New ListViewItem(Trim("" & Rs!dni))
                    item.SubItems.Add(Trim("" & Rs!Nombre))
                    item.SubItems.Add(RetornaNivel(Trim("" & Rs!Nivel)))
                    item.Checked = True
                    lv.Items.Add(item)
                Else
                    MsgBox("Este cliente tiene el carnet de manipulador caducado o sin validez")
                End If
                HaySocio = True
                Rs.Close()
                SQL = "SELECT c.codigo_cliente, c.manipulador, c.activo_sn, m.dni, m.nombre, m.nivel, m.valido_hasta FROM cliente_manipulad c LEFT JOIN carnet_manipulador m ON "
                SQL = SQL + "m.dni = c.manipulador WHERE c.codigo_cliente=" & codigo_cliente & " AND "
                SQL = SQL + "c.activo_sn = 'S' AND c.valido_hasta >= '" & CDate(Now.Date) & "' AND m.valido_hasta >= '" & CDate(Now.Date) & "' ORDER BY orden"
                Rs.Open(SQL, ObjetoGlobal.cn)
                If Rs.RecordCount = 0 Then
                    If Not HaySocio Then
                        MsgBox("NO existen manipuladores autorizados para este cliente")
                        Rs.Close()
                        Return
                    End If
                Else
                    Rs.MoveFirst()
                End If

                ' Recorre todos los registros
                While Not Rs.EOF
                    item = New ListViewItem(Trim("" & Rs!dni))
                    item.SubItems.Add(Trim("" & Rs!Nombre))
                    item.SubItems.Add(RetornaNivel(Trim("" & Rs!Nivel)))
                    item.Checked = True
                    lv.Items.Add(item)
                    Rs.MoveNext()
                End While
                Rs.Close()

                Return
            End If
        Catch ex As Exception
            MsgBox(ex.Message.Trim & " (" & ex.ToString.Trim & ")")
        End Try

    End Sub

    Private Sub CmdCredito_Click()
        Aceptar()

        If Trim(nif_retorno) = "" Then
            MsgBox("Debe realizar una selección")
            Return
        End If
        Contado_sn = "N"
        Seleccion_sn = "S"
        DialogResult = DialogResult.OK

    End Sub

    Private Function RetornaNivel(arg) As String
        Dim retorno As String

        retorno = ""
        Select Case arg
            Case "B"
                retorno = "Básico"
            Case "C"
                retorno = "Cualificado"
            Case "F"
                retorno = "Fumigador"
            Case "P"
                retorno = "Piloto aplicador"
            Case Else
                retorno = "Nivel no reconocido"
        End Select
        Return retorno

    End Function

    Private Sub FrmSeleccionManipulador_Load(sender As Object, e As EventArgs) Handles Me.Load
        Cargar_ListView()
    End Sub
End Class