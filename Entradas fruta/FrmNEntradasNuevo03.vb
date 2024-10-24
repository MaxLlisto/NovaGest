Imports System.ComponentModel

Public Class FrmNEntradasNuevo03
    Public PeriodoSeleccionado As Long
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public Variedad As String
    Public oForm As Form

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Me.Close()
    End Sub

    'Private Sub Form_Load()
    '    LstPeriodos.FullRowSelect = True
    '    LstPeriodos.MultiSelect = False
    '    Me.Text = "Selección de periodo"
    '    PoblarListaPeriodos()
    'End Sub

    Private Sub LstPeriodos_DblClick()

        If Not LstPeriodos.SelectedItems Is Nothing Then
            PeriodoSeleccionado = LstPeriodos.SelectedItems.Item(1).Text
            DialogResult = DialogResult.OK
        End If

    End Sub


    Private Sub PoblarListaPeriodos()
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String
        Dim i As Long
        Dim Item As ListViewItem

        LstPeriodos.Items.Clear()

        Sql = "SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(Variedad) + "' order by 1,2,3,4"
        Rs.Open(Sql, ObjetoGlobal.cn)

        If Rs.RecordCount > 0 Then
            For i = 1 To Rs.RecordCount
                Rs.AbsolutePosition = i
                Item = New ListViewItem(Trim(Rs!descripcion_per))
                Item.SubItems.Add(CStr(Rs!codigo_periodo))
                LstPeriodos.Items.Add(Item)
            Next
        End If
        Rs.Close

    End Sub

    Private Sub CmdGrabar_Click(sender As Object, e As EventArgs) Handles CmdGrabar.Click
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim SQL As String
        Dim RsCalidadesVariedad As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim RsPrecios_liquid As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        If Trim(TxtCodPeriodo.Text) = "" Then
            MsgBox("Debe indicar un periodo a grabar.")
            TxtDescripcion.Text = ""
        Else
            SQL = "SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(Variedad) + "' AND CODIGO_PERIODO = " + CStr(TxtCodPeriodo.Text)
            Rs.Open(SQL, ObjetoGlobal.cn, True)
            If Rs.RecordCount > 0 Then
                MsgBox("El periodo ya existe.")
                TxtDescripcion.Text = Trim(Rs!descripcion_per)
                Rs.Close()
            Else
                Rs.AddNew()
                Rs!empresa = Trim(ObjetoGlobal.EmpresaActual)
                Rs!Ejercicio = Trim(ObjetoGlobal.EjercicioActual)
                Rs!codigo_variedad = Trim(Variedad)
                Rs!codigo_periodo = Trim(TxtCodPeriodo.Text)
                Rs!descripcion_per = Trim(TxtDescripcion.Text)
                Rs.Update()
                'If ModoConexion = 1 Then GrabarEnLocal Rs, "I"
                Rs.Close()
                ' Ahora los precios periodo

                RsCalidadesVariedad.Open("SELECT * FROM calidades_var_ej  WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND ejercicio = '" & ObjetoGlobal.EjercicioActual & "' AND CODIGO_VARIEDAD='" & Trim(Variedad) & "' ORDER BY 1,2,3,4", ObjetoGlobal.cn)
                If RsCalidadesVariedad.RecordCount > 0 Then
                    'SI NO EXISTIA EL PERIODO NO TIENEN QUE HABER PRECIOS LIQUIDACIÓN, ASI QUE...      FIUM!
                    RsPrecios_liquid.Open("SELECT * FROM PRECIOS_LIQUID WHERE EMPRESA='" & Trim(ObjetoGlobal.EmpresaActual) & "' AND EJERCICIO='" & Trim(ObjetoGlobal.EjercicioActual) & "' AND CODIGO_VARIEDAD='" & Trim(Variedad) & "' AND CODIGO_PERIODO=" & Trim(TxtCodPeriodo.Text), ObjetoGlobal.cn, True)

                    While RsPrecios_liquid.RecordCount > 0
                        RsPrecios_liquid.MoveFirst()
                        RsPrecios_liquid.Delete()
                    End While
                    RsPrecios_liquid.Close()

                    RsPrecios_liquid.Open("SELECT * FROM PRECIOS_LIQUID WHERE 1=0", ObjetoGlobal.cn, True)
                    While Not RsCalidadesVariedad.EOF
                        RsPrecios_liquid.AddNew()
                        RsPrecios_liquid!empresa = "" & Trim(ObjetoGlobal.EmpresaActual)
                        RsPrecios_liquid!Ejercicio = "" & Trim(ObjetoGlobal.EjercicioActual)
                        RsPrecios_liquid!codigo_variedad = "" & Trim(Variedad)
                        RsPrecios_liquid!codigo_periodo = "" & Trim(TxtCodPeriodo.Text)
                        RsPrecios_liquid!codigo_calidad = (RsCalidadesVariedad!codigo_calidad)
                        RsPrecios_liquid!PRECIO_ANTICIPO = 0
                        RsPrecios_liquid!PRECIO_LIQUIDACION = 0
                        RsPrecios_liquid.Update()
                        RsCalidadesVariedad.MoveNext()
                    End While
                End If
                RsCalidadesVariedad.Close()
                RsPrecios_liquid.Close()
                MsgBox("Se ha grabado correctamente el periodo.")
                PoblarListaPeriodos()
            End If
        End If
    End Sub

    Private Sub LstPeriodos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LstPeriodos.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            If Not LstPeriodos.SelectedItems Is Nothing Then
                PeriodoSeleccionado = LstPeriodos.SelectedItems(0).SubItems(1).Text
                DialogResult = DialogResult.OK
            End If
        End If
    End Sub

    Private Sub TxtCodPeriodo_Validating(sender As Object, e As CancelEventArgs) Handles TxtCodPeriodo.Validating
        Dim Rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Dim Sql As String

        If Trim(TxtCodPeriodo.Text) = "" Then
            TxtDescripcion.Text = ""
        Else
            Sql = "SELECT * FROM PERIODOS_COOP WHERE EMPRESA = '" + Trim(ObjetoGlobal.EmpresaActual) + "' AND EJERCICIO = '" + Trim(ObjetoGlobal.EjercicioActual) + "' AND CODIGO_VARIEDAD = '" + Trim(Variedad) + "' AND CODIGO_PERIODO = " + CStr(TxtCodPeriodo.Text)
            Rs.Open(Sql, ObjetoGlobal.cn)
            If Rs.RecordCount > 0 Then
                MsgBox("El periodo ya existe.")
                TxtDescripcion.Text = Trim(Rs!descripcion_per)
                e.Cancel = True
            End If
            Rs.Close
        End If

    End Sub

    Private Sub FrmNEntradasNuevo03_Load(sender As Object, e As EventArgs) Handles Me.Load
        LstPeriodos.FullRowSelect = True
        LstPeriodos.MultiSelect = False
        Me.Text = "Selección de periodo"
        PoblarListaPeriodos()
    End Sub
End Class