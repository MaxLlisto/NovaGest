Public Class CambiarEjercicio
    Dim ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public resultado As Boolean = False

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Panel1.Visible = True


    End Sub
    Private Sub BtEntrar_Click(sender As Object, e As EventArgs) Handles BtEntrar.Click
        ObjetoGlobal.EmpresaActual = cbEmpresa.SelectedValue
        ObjetoGlobal.EmpresaRazonSocial = cbEmpresa.Text
        ObjetoGlobal.EjercicioActual = cbEjercicio.SelectedValue
        buscar_usuario()
        resultado = True
        Me.Close()
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
        Rellenaejercicios()


    End Sub

    Private Sub Rellenaejercicios()
        Dim cmd As New cnRecordset.CnRecordset
        Dim comboSource As New Dictionary(Of String, String)()

        cmd.Open("SELECT ejercicio, descripcion FROM ejercicios_contab WHERE empresa='" & cbEmpresa.SelectedValue & "'", ObjetoGlobal.cn)
        While Not cmd.EOF
            comboSource.Add(Trim(cmd!ejercicio), Trim(cmd!descripcion))
            cmd.MoveNext()
        End While
        cbEjercicio.DataSource = New BindingSource(comboSource, Nothing)
        cbEjercicio.DisplayMember = "Value"
        cbEjercicio.ValueMember = "Key"
        cbEjercicio.SelectedIndex = cbEjercicio.Items.Count - 1

    End Sub
    Public Property og As ObjetoGlobal.ObjetoGlobal
        Get
            Return ObjetoGlobal
        End Get
        Set(ByVal value As ObjetoGlobal.ObjetoGlobal)
            ObjetoGlobal = value
        End Set
    End Property

    Public Function MiShowDialog()
        RellenaEmpresa()
        cbEmpresa.SelectedValue = ObjetoGlobal.EmpresaActual
        cbEmpresa.Text = ObjetoGlobal.EmpresaRazonSocial
        cbEjercicio.SelectedValue = ObjetoGlobal.EjercicioActual
        Return Me.ShowDialog()
    End Function

    Private Sub buscar_usuario()
        Dim RsUltimaEmpresa As cnRecordset.CnRecordset = New cnRecordset.CnRecordset

        ObjetoGlobal.EmpresaActual = cbEmpresa.SelectedValue
        ObjetoGlobal.EmpresaRazonSocial = cbEmpresa.Text
        ObjetoGlobal.EjercicioActual = cbEjercicio.SelectedValue

        RsUltimaEmpresa.Open("SELECT * FROM ZZULTIMAEMPRESA WHERE USUARIO='" & ObjetoGlobal.UsuarioActual & "'", ObjetoGlobal.cn, True)

        If RsUltimaEmpresa.RecordCount > 0 Then
            RsUltimaEmpresa!usuario = Trim(ObjetoGlobal.UsuarioActual)
            RsUltimaEmpresa!Empresa = ObjetoGlobal.EmpresaActual
            RsUltimaEmpresa!ejercicio = ObjetoGlobal.EjercicioActual
            RsUltimaEmpresa.Update()
        End If
        RsUltimaEmpresa.Close()

    End Sub
End Class