Imports System.ComponentModel

Public Class FrmNEntradasNuevo07
    Public ObjetoGlobal As Object
    Private Tipo_entrada As String
    Public AlbaranSeleccionado As Long
    Public oForm As Form

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        PoblarListaAlbaranes(-1)
    End Sub

    Private Sub CmdSalir_Click()

        Try
            If AlbaranSeleccionado > 0 And SerieSeleccionada > "" Then
                If Trim(Tipo_entrada) = "T" Then
                    Dim oFrm As FrmNEntradasNuevo08T = New FrmNEntradasNuevo08T
                    oFrm.ObjetoGlobal = Me.ObjetoGlobal
                    oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                    oFrm.SerieSeleccionada = SerieSeleccionada
                    oFrm.oform = Me.oForm
                    oFrm.ShowDialog()
                Else
                    Dim oFrm As FrmNEntradasNuevo08 = New FrmNEntradasNuevo08
                    oFrm.ObjetoGlobal = Me.ObjetoGlobal
                    oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                    oFrm.SerieSeleccionada = SerieSeleccionada
                    oFrm.oform = Me.oForm
                    oFrm.ShowDialog()
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PoblarListaAlbaranes(NumeroAlbaran As Long)
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Long
        Dim Item As ListViewItem

        lstAlbaranes.Items.Clear()
        SQL = "SELECT e.*,(s.apellidos_socio + ' ' + s.nombre_socio) AS razon,OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        If NumeroAlbaran = -1 Then
            SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND kg_sin_clasif > 0 AND tipo_entrada <> 'T' "
        Else
            SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND numero_albaran = " & CStr(NumeroAlbaran) & " AND tipo_entrada <> 'T' "
        End If
        SQL = Trim(SQL) + " UNION "
        SQL = Trim(SQL) + " SELECT e.*, p.razon_social as razon,pt.nombre AS NOMBRE_CAPATAZ,tt.nombre AS NOMBRE_TRANSPORTISTA,ct.situacion_campo AS DESCRIPCION_SITUACION, va.descripcion AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN proveedores_coop p ON p.codigo_proveedor = e.codigo_proveedor"
        SQL = Trim(SQL) + " JOIN campos_terceros ct ON ct.empresa = e.empresa AND ct.ejercicio = e.ejercicio AND ct.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades va ON va.empresa = e.empresa AND va.codigo_variedad = e.codigo_variedad"
        SQL = Trim(SQL) + " JOIN personal_terceros pt ON pt.codigo = e.capataz_terc"
        SQL = Trim(SQL) + " JOIN personal_terceros tt ON tt.codigo = e.transportista_terc"
        'SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND TARADA_SN = 'S' AND numero_albaran = " & CStr(NumeroAlbaran) & " order by 1,2,3,4"
        If NumeroAlbaran = -1 Then
            SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND kg_sin_clasif > 0 AND tipo_entrada = 'T' "
        Else
            SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND numero_albaran = " & CStr(NumeroAlbaran) & " AND tipo_entrada = 'T' "
        End If
        SQL = Trim(SQL) + " order by 1,2,3,4"

        Rs.Open(SQL, ObjetoGlobal.cn)
        If Rs.RecordCount > 0 Then
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                Item = New ListViewItem(CStr("" & Rs!numero_albaran))
                Item.SubItems.Add(Format(Rs!fecha_entrada, "dd/mm") + "  " + Strings.Left(Rs!hora_entrada, 5))
                Item.SubItems.Add(Trim(Rs!descripcion_variedad))
                Item.SubItems.Add(Format(Rs!bruto_sin_env, "##,##0"))
                If Trim(Rs!Tipo_entrada) <> "T" Then
                    Item.SubItems.Add(CStr(Rs!codigo_campo) + "-" + Trim("" & Rs!descripcion_situacion))
                Else
                    Item.SubItems.Add(CStr(Rs!campo_terceros) + "-" + Trim("" & Rs!descripcion_situacion))
                End If
                Item.SubItems.Add(Trim("" & Rs!razon))
                Item.SubItems.Add(Trim("" & Rs!NOMBRE_CAPATAZ))
                Item.SubItems.Add(Trim("" & Rs!nombre_transportista))
                Item.SubItems.Add(Trim("" & Rs!serie_albaran))
                Item.SubItems.Add(Trim("" & Rs!Tipo_entrada))
                lstAlbaranes.Items.Add(Item)
            Next
        End If
        Rs.Close()
        lstAlbaranes.Focus()
    End Sub


    Private Sub FrmNEntradasNuevo07_Load(sender As Object, e As EventArgs) Handles Me.Load
        AlbaranSeleccionado = -1
        'lstAlbaranes.ColumnHeader.Clear
        lstAlbaranes.View = View.Details

        Dim columnHeader1 As ColumnHeader = New ColumnHeader()
        columnHeader1.Text = "Albaran"
        columnHeader1.TextAlign = HorizontalAlignment.Left
        columnHeader1.Width = 100
        lstAlbaranes.Columns.Add(columnHeader1)

        Dim columnHeader2 As ColumnHeader = New ColumnHeader()
        columnHeader2.Text = "Fecha"
        columnHeader2.TextAlign = HorizontalAlignment.Left
        columnHeader2.Width = 100
        lstAlbaranes.Columns.Add(columnHeader2)

        Dim columnHeader3 As ColumnHeader = New ColumnHeader()
        columnHeader3.Text = "Variedad"
        columnHeader3.TextAlign = HorizontalAlignment.Left
        columnHeader3.Width = 100
        lstAlbaranes.Columns.Add(columnHeader3)

        Dim columnHeader4 As ColumnHeader = New ColumnHeader()
        columnHeader4.Text = "Kilos"
        columnHeader4.TextAlign = HorizontalAlignment.Left
        columnHeader4.Width = 100
        lstAlbaranes.Columns.Add(columnHeader4)

        Dim columnHeader5 As ColumnHeader = New ColumnHeader()
        columnHeader5.Text = "Campo"
        columnHeader5.TextAlign = HorizontalAlignment.Left
        columnHeader5.Width = 100
        lstAlbaranes.Columns.Add(columnHeader5)

        Dim columnHeader6 As ColumnHeader = New ColumnHeader()
        columnHeader6.Text = "Socio"
        columnHeader6.TextAlign = HorizontalAlignment.Left
        columnHeader6.Width = 100
        lstAlbaranes.Columns.Add(columnHeader6)

        Dim columnHeader7 As ColumnHeader = New ColumnHeader()
        columnHeader7.Text = "Capataz"
        columnHeader7.TextAlign = HorizontalAlignment.Left
        columnHeader7.Width = 100
        lstAlbaranes.Columns.Add(columnHeader7)

        Dim columnHeader8 As ColumnHeader = New ColumnHeader()
        columnHeader8.Text = "Transportista"
        columnHeader8.TextAlign = HorizontalAlignment.Left
        columnHeader8.Width = 100
        lstAlbaranes.Columns.Add(columnHeader8)

        Dim columnHeader9 As ColumnHeader = New ColumnHeader()
        columnHeader9.Text = ""
        columnHeader9.TextAlign = HorizontalAlignment.Left
        columnHeader9.Width = 100
        lstAlbaranes.Columns.Add(columnHeader9)

        Dim columnHeader10 As ColumnHeader = New ColumnHeader()
        columnHeader10.Text = ""
        columnHeader10.TextAlign = HorizontalAlignment.Left
        columnHeader10.Width = 100
        lstAlbaranes.Columns.Add(columnHeader10)

        Dim columnHeader11 As ColumnHeader = New ColumnHeader()
        columnHeader11.Text = ""
        columnHeader11.TextAlign = HorizontalAlignment.Left
        columnHeader11.Width = 100
        lstAlbaranes.Columns.Add(columnHeader11)
        lstAlbaranes.FullRowSelect = True
        lstAlbaranes.MultiSelect = False
        AlbaranSeleccionado = -1
        SerieSeleccionada = ""
    End Sub


    Private Sub TxtAlbaran_Validating(sender As Object, e As CancelEventArgs) Handles TxtAlbaran.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If Trim(TxtAlbaran.Text) > "" Then
            Rs.Open("SELECT * FROM ENTRADAS_ALBARANES WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND NUMERO_ALBARAN = " + CStr(TxtAlbaran.Text), ObjetoGlobal.cn)
            If Rs.RecordCount = 0 Then
                MsgBox("Error. Albarán inexistente.")
                e.Cancel = True
            ElseIf Rs.RecordCount > 1 Then
                PoblarListaAlbaranes(CLng(TxtAlbaran.Text))
            Else
                AlbaranSeleccionado = Rs!numero_albaran
                SerieSeleccionada = Rs!serie_albaran
                Tipo_entrada = Trim("" & Rs!Tipo_entrada)
                'DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If

    End Sub

    Private Sub TxtAlbaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtAlbaran.KeyPress
        'SendKeys.Send("{TAB}")

        'If e.KeyChar = ChrW(Keys.Enter) Then
        '    If Not lstAlbaranes.SelectedItems Is Nothing Then
        '        AlbaranSeleccionado = CLng(lstAlbaranes.SelectedItems.Item(0).Text)
        '        SerieSeleccionada = lstAlbaranes.SelectedItems.Item(8).Text
        '        Tipo_entrada = Trim("" & lstAlbaranes.SelectedItems.Item(9).Text)
        '        Me.Close()
        '    End If
        'End If

    End Sub

    Private Sub FrmNEntradasNuevo07_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Try
            If AlbaranSeleccionado > 0 And SerieSeleccionada > "" Then
                If Trim(Tipo_entrada) = "T" Then
                    Dim oFrm As FrmNEntradasNuevo08T = New FrmNEntradasNuevo08T
                    oFrm.ObjetoGlobal = Me.ObjetoGlobal
                    oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                    oFrm.SerieSeleccionada = SerieSeleccionada
                    oFrm.oform = Me.oForm
                    oFrm.ShowDialog()
                Else
                    Dim oFrm As FrmNEntradasNuevo08 = New FrmNEntradasNuevo08
                    oFrm.ObjetoGlobal = Me.ObjetoGlobal
                    oFrm.oform = Me.oForm
                    oFrm.AlbaranSeleccionado = AlbaranSeleccionado
                    oFrm.SerieSeleccionada = SerieSeleccionada
                    oFrm.ShowDialog()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtAlbaran_TextChanged(sender As Object, e As EventArgs) Handles TxtAlbaran.TextChanged

    End Sub
End Class