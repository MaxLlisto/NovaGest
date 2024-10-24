Public Class FrmNEntradasNuevo05
    Dim Tipo_entrada As String
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public oForm As Form

    Private Sub FrmNEntradasNuevo05_Load(sender As Object, e As EventArgs) Handles Me.Load

        LstAlbaranes.Columns.Clear()
        Dim columnHeader1 As ColumnHeader = New ColumnHeader()
        columnHeader1.Text = "Albarán"
        columnHeader1.TextAlign = HorizontalAlignment.Left
        columnHeader1.Width = 100
        LstAlbaranes.Columns.Add(columnHeader1)

        Dim columnHeader2 As ColumnHeader = New ColumnHeader()
        columnHeader2.Text = "Fecha"
        columnHeader2.TextAlign = HorizontalAlignment.Left
        columnHeader2.Width = 100
        LstAlbaranes.Columns.Add(columnHeader2)

        Dim columnHeader3 As ColumnHeader = New ColumnHeader()
        columnHeader3.Text = "Variedad"
        columnHeader3.TextAlign = HorizontalAlignment.Left
        columnHeader3.Width = 200
        LstAlbaranes.Columns.Add(columnHeader3)

        Dim columnHeader4 As ColumnHeader = New ColumnHeader()
        columnHeader4.Text = "Kilos"
        columnHeader4.TextAlign = HorizontalAlignment.Left
        columnHeader4.Width = 80
        LstAlbaranes.Columns.Add(columnHeader4)

        Dim columnHeader5 As ColumnHeader = New ColumnHeader()
        columnHeader5.Text = "Campo"
        columnHeader5.TextAlign = HorizontalAlignment.Left
        columnHeader5.Width = 80
        LstAlbaranes.Columns.Add(columnHeader5)

        Dim columnHeader6 As ColumnHeader = New ColumnHeader()
        columnHeader6.Text = "Socio"
        columnHeader6.TextAlign = HorizontalAlignment.Left
        columnHeader6.Width = 80
        LstAlbaranes.Columns.Add(columnHeader6)

        Dim columnHeader7 As ColumnHeader = New ColumnHeader()
        columnHeader7.Text = "Capataz"
        columnHeader7.TextAlign = HorizontalAlignment.Left
        columnHeader7.Width = 80
        LstAlbaranes.Columns.Add(columnHeader7)

        Dim columnHeader8 As ColumnHeader = New ColumnHeader()
        columnHeader8.Text = "Capataz"
        columnHeader8.TextAlign = HorizontalAlignment.Left
        columnHeader8.Width = 100
        LstAlbaranes.Columns.Add(columnHeader8)

        Dim columnHeader9 As ColumnHeader = New ColumnHeader()
        columnHeader9.Text = ""
        columnHeader9.TextAlign = HorizontalAlignment.Left
        columnHeader9.Width = 0
        LstAlbaranes.Columns.Add(columnHeader9)

        Dim columnHeader10 As ColumnHeader = New ColumnHeader()
        columnHeader10.Text = ""
        columnHeader10.TextAlign = HorizontalAlignment.Left
        columnHeader10.Width = 0
        LstAlbaranes.Columns.Add(columnHeader10)

        Me.Text = "Selección de Albarán"
        PoblarListaAlbaranes()
        AlbaranSeleccionado = -1
        SerieSeleccionada = ""
    End Sub

    Private Sub PoblarListaAlbaranes()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim i As Long
        Dim Item As ListViewItem

        LstAlbaranes.Items.Clear()
        SQL = "SELECT E.*,(s.apellidos_socio + ' ' + s.nombre_socio) AS razon, OC.NOMBRE_OPERARIO AS NOMBRE_CAPATAZ,OT.NOMBRE_OPERARIO AS NOMBRE_TRANSPORTISTA,SC.DESCRIPCION AS DESCRIPCION_SITUACION,V.DESCRIPCION AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN SOCIOS_COOP S ON S.CODIGO_SOCIO = E.CODIGO_SOCIO"
        SQL = Trim(SQL) + " JOIN CAMPOS C ON C.EMPRESA = E.EMPRESA AND C.CODIGO_CAMPO = E.CODIGO_CAMPO"
        SQL = Trim(SQL) + " JOIN CULTIVOS CU ON CU.EMPRESA = E.EMPRESA AND CU.EJERCICIO = E.EJERCICIO AND CU.CODIGO_CAMPO = E.CODIGO_CAMPO AND CU.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN SITUACION_CAMPOS SC ON SC.EMPRESA = C.EMPRESA AND SC.CODIGO_SITUACION = C.SITUACION_CAMPO"
        SQL = Trim(SQL) + " JOIN VARIEDADES V ON V.EMPRESA = E.EMPRESA AND V.CODIGO_VARIEDAD = E.CODIGO_VARIEDAD"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OC ON OC.CODIGO_OPERARIO = E.CAPATAZ"
        SQL = Trim(SQL) + " JOIN OPERARIOS_COOP OT ON OT.CODIGO_OPERARIO = E.TRANSPORTISTA"
        SQL = Trim(SQL) + " WHERE E.EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND E.EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND TARADA_SN = 'N' AND tipo_entrada <> 'T' "
        SQL = Trim(SQL) + " UNION "
        SQL = Trim(SQL) + " SELECT e.*, p.razon_social as razon,pt.nombre AS NOMBRE_CAPATAZ,tt.nombre AS NOMBRE_TRANSPORTISTA,ct.situacion_campo AS DESCRIPCION_SITUACION, va.descripcion AS DESCRIPCION_VARIEDAD FROM ENTRADAS_ALBARANES E JOIN proveedores_coop p ON p.codigo_proveedor = e.codigo_proveedor"
        SQL = Trim(SQL) + " JOIN campos_terceros ct ON ct.empresa = e.empresa AND ct.ejercicio = e.ejercicio AND ct.codigo_campo = e.campo_terceros "
        SQL = Trim(SQL) + " JOIN variedades va ON va.empresa = e.empresa AND va.codigo_variedad = e.codigo_variedad"
        SQL = Trim(SQL) + " JOIN personal_terceros pt ON pt.codigo = e.capataz_terc"
        SQL = Trim(SQL) + " JOIN personal_terceros tt ON tt.codigo = e.transportista_terc"
        SQL = Trim(SQL) + " WHERE e.empresa = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND e.ejercicio = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND tarada_sn = 'N' AND tipo_entrada = 'T' "
        SQL = Trim(SQL) + " ORDER BY 1,2,3,4"
        Rs.Open(SQL, ObjetoGlobal.cn)

        If Rs.RecordCount > 0 Then
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                Item = New ListViewItem(CStr("" & Rs!numero_albaran))
                Item.SubItems.Add(Format(Rs!fecha_entrada, "dd/mm") + "  " + Strings.Left(Rs!hora_entrada, 5))
                Item.SubItems.Add(Trim(Rs!descripcion_variedad))
                Item.SubItems.Add(Format(Rs!bruto_sin_env, "##,##0"))
                If Trim("" & Rs!Tipo_entrada) <> "T" Then
                    Item.SubItems.Add(CStr("" & Rs!codigo_campo) + "-" + Trim("" & Rs!descripcion_situacion))
                Else
                    Item.SubItems.Add(CStr("" & Rs!campo_terceros) + "-" + Trim("" & Rs!descripcion_situacion))
                End If
                Item.SubItems.Add(Trim("" & Rs!razon))
                Item.SubItems.Add(Trim("" & Rs!NOMBRE_CAPATAZ))
                Item.SubItems.Add(Trim("" & Rs!nombre_transportista))
                Item.SubItems.Add(Trim("" & Rs!serie_albaran))
                Item.SubItems.Add(Trim("" & Rs!Tipo_entrada))
                LstAlbaranes.Items.Add(Item)
            Next
        End If
        Rs.Close()
    End Sub

    Private Sub LstAlbaranes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LstAlbaranes.KeyPress

        If e.KeyChar = ChrW(Keys.Return) Then
            If Not LstAlbaranes.SelectedItems Is Nothing Then
                AlbaranSeleccionado = CLng(LstAlbaranes.SelectedItems(0).SubItems(0).Text)
                SerieSeleccionada = LstAlbaranes.SelectedItems(0).SubItems(8).Text
                Tipo_entrada = LstAlbaranes.SelectedItems(0).SubItems(9).Text
                SalirSeleccion()
                'DialogResult = DialogResult.OK
            End If
        End If
    End Sub

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        SalirSeleccion()
    End Sub
    Private Sub SalirSeleccion()


        If AlbaranSeleccionado > 0 And SerieSeleccionada > "" Then
            If Trim(Tipo_entrada) = "T" Then
                'Dim oFrm As FrmNEntradasNuevo06T = New FrmNEntradasNuevo06T
                'oFrm.oform = Me.oForm
                'oFrm.objetoGlobal = Me.ObjetoGlobal
                'oFrm.ShowDialog()
            Else
                Dim oFrm As FrmNEntradasNuevo06 = New FrmNEntradasNuevo06
                oFrm.oform = Me.oForm
                oFrm.ObjetoGlobal = Me.ObjetoGlobal
                oFrm.ShowDialog()
            End If
        End If
        Me.Close()
    End Sub

    Private Sub LstAlbaranes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstAlbaranes.SelectedIndexChanged

    End Sub

    Private Sub FrmNEntradasNuevo05_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub
End Class