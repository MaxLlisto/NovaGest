

Public Class frmcomprobacionretenciones
    Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal

    Private Sub frmcomprobacionretenciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        RellenaEmpresa()
    End Sub

    Private Sub RellenaEmpresa()
        Dim cmd As New cnRecordset.CnRecordset
        Dim comboSource As New Dictionary(Of String, String)()

        cmd.Open("SELECT empresa, razon_social FROM empresas", ObjetoGlobal.cn)

        While Not cmd.EOF
            comboSource.Add(Trim(cmd!empresa), Trim(cmd!empresa) + " - " + Trim(cmd!razon_social))
            cmd.MoveNext()
        End While
        cbEmpresa.DataSource = New BindingSource(comboSource, Nothing)
        cbEmpresa.DisplayMember = "Value"
        cbEmpresa.ValueMember = "Key"
        cbEmpresa.SelectedIndex = 0
    End Sub

    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Private Sub Salir_Click(sender As Object, e As EventArgs) Handles Salir.Click
        Close()
    End Sub

    Private Sub BtCargar_Click(sender As Object, e As EventArgs) Handles BtCargar.Click
        Dim sql As String
        Dim rs As cnRecordset.CnRecordset = New cnRecordset.CnRecordset
        Try
            sql = "Select alta_agrup_5,codigo_socio,apellidos_socio,nombre_socio From socios_coop s Where codigo_socio < 7000 "
            sql = sql + " And exists (select cu.empresa from cultivos cu join campos c on c.empresa = cu.empresa And c.codigo_campo = cu.codigo_campo "
            sql = sql + " where cu.empresa ='" & cbEmpresa.SelectedValue & "' and cu.ejercicio >= '" & Trim(ObjetoGlobal.EjercicioActual) & "' "
            sql = sql + " And (cu.alta_baja  ='A' or cu.fecha_baja>='1/07/" & Trim(ObjetoGlobal.EjercicioActual) & "' )"

            If rbRetenciones.Checked Then
                sql = sql + " And c.codigo_socio = s.codigo_socio) And Not exists (select empresa from retenciones_socio r where r.empresa ='" & cbEmpresa.SelectedValue & "' "
                sql = sql + " And r.codigo_socio = s.codigo_socio And codigo_retencion In (3, 5) And retencion = 2) "
            Else
                sql = sql + " And c.codigo_socio = s.codigo_socio) And Not exists (select empresa from retenciones_socio r where r.empresa ='" & cbEmpresa.SelectedValue & "' "
                sql = sql + " And r.codigo_socio = s.codigo_socio And codigo_retencion = 2 And retencion = 1) "
            End If

            If rs.Open(sql, ObjetoGlobal.cn) Then
                DGComprobaciones.DataSource = rs.cnDataSet.Tables(0)
            Else
                MsgBox("No se puede establecer conexión con la base de datos")
            End If
        Catch ex As Exception
            MsgBox("No se puede establecer conexión con la base de datos")
        End Try

    End Sub
End Class