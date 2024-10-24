Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class Inicio
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public PerfilUsuario As String
    Public resultado As Boolean = False
    Public Empresaactual As String
    Public Ejercicioactual As String
    Dim fslogin As String
    Dim fsPassword As String
    Dim NoActualizar As Boolean
    Dim fempresa As String
    Dim fejercicio As String

    Public Sub New()
        Dim aCal(0) As Array
        Dim aCab(0) As String

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal
        'If ObjetoGlobal.Inicializar("") = False Then
        ' MsgBox("No se puede abrir la conexión a la BD")
        'End If
        PerfilUsuario = "ADMINISTRADOR"
        BtEntrar.Enabled = False
        Entrar.Enabled = False
        Panel1.Visible = False
    End Sub

    Private Sub ver_Click(sender As Object, e As EventArgs) Handles ver.Click
        TxtPassword.UseSystemPasswordChar = False
        ver.Visible = False
        Nover.Visible = True
    End Sub

    Private Sub Nover_Click(sender As Object, e As EventArgs) Handles Nover.Click
        TxtPassword.UseSystemPasswordChar = True
        ver.Visible = True
        Nover.Visible = False
    End Sub


    Private Function ConectarBD() As Boolean
        Dim CadenaConexion As String = "Data Source=MAXIMSURFACE;Initial Catalog=Compugest;Persist Security Info=True;User ID=" & fslogin & ";Password=h" & fsPassword & "to"

        Try
            If ObjetoGlobal.Inicializar(CadenaConexion) = False Then
                MsgBox("No se puede abrir la conexión a la BD")
                Return False
            End If
            GrabaUltimaConexion()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub Entrar_Click(sender As Object, e As EventArgs) Handles Entrar.Click
        Static Intentos As Integer = 1
        fslogin = TxtUsuario.Text.Trim
        fsPassword = "666666"
        Try
            If ConectarBD() Then
                If Not ValidarUsuario() Then
                    Intentos = Intentos + 1
                    If Intentos > 3 Then
                        MsgBox("Has superado el número de intentos")
                        Me.Close()
                        resultado = False
                        Return
                    End If
                    Return
                Else
                    RellenaEmpresa()
                    buscar_usuario(TxtUsuario.Text.Trim)
                    cbEmpresa.SelectedValue = fempresa
                    cbEjercicio.SelectedValue = fejercicio
                    resultado = True
                    Panel1.Visible = True
                End If
            Else
                resultado = False
                Me.Close()
            End If
        Catch ex As Exception
            resultado = False
            Me.Close()
        End Try

    End Sub

    Private Sub TxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles TxtUsuario.TextChanged
        If TxtUsuario.Text.Trim <> "" And TxtPassword.Text.Trim <> "" Then
            Entrar.Enabled = True
        Else
            Entrar.Enabled = False
        End If
    End Sub

    Private Function ValidarUsuario() As Boolean
        Dim Rs As New cnRecordset.CnRecordset


        Rs.Open("SELECT * FROM zzusuarios WHERE nombre='" & TxtUsuario.Text.Trim & "'", ObjetoGlobal.cn)

        If Rs.RecordCount > 0 Then
            If Not String.IsNullOrEmpty(Rs!clave) Then
                If Trim(Rs("clave")) <> TxtPassword.Text.Trim Then
                    MsgBox("Clave no válida", vbOKOnly + vbInformation, "Usuarios")
                    TxtPassword.Text = ""
                    Rs.Close()
                    TxtPassword.Focus()
                    Return False
                End If
            End If
        Else
            MsgBox("Usuario inexistente", , "Usuarios")
            TxtUsuario.Text = ""
            TxtUsuario.Focus()
            Return False
        End If

        ObjetoGlobal.UsuarioActual = TxtUsuario.Text
        ObjetoGlobal.NombreUsuarioActual = Trim(Rs!nombre)
        PerfilUsuario = Trim(Rs!perfil)
        Return True

    End Function

    Private Sub buscar_usuario(ByVal cUsuario As String, Optional ByVal cEmpresa As String = "", Optional ByVal cEjercicio As String = "")
        Dim RsUltimaEmpresa As cnRecordset.CnRecordset = New cnRecordset.CnRecordset


        RsUltimaEmpresa.Open("SELECT * FROM ZZULTIMAEMPRESA WHERE USUARIO='" & cUsuario & "'", ObjetoGlobal.cn, True)
        If cEmpresa = "" Then
            If RsUltimaEmpresa.RecordCount > 0 Then
                fempresa = Trim(RsUltimaEmpresa!Empresa)
                fejercicio = Trim(RsUltimaEmpresa!ejercicio)
            Else
                RsUltimaEmpresa.AddNew()
                RsUltimaEmpresa!usuario = Trim(cUsuario)
                RsUltimaEmpresa!Empresa = ""
                RsUltimaEmpresa!ejercicio = ""
                RsUltimaEmpresa.Update()
            End If
        Else
            RsUltimaEmpresa!usuario = Trim(cUsuario)
            RsUltimaEmpresa!Empresa = cEmpresa
            RsUltimaEmpresa!ejercicio = cEjercicio
            RsUltimaEmpresa.Update()
        End If
        RsUltimaEmpresa.Close()
    End Sub


    Private Sub GrabaUltimaConexion()
        '        Dim lsCadena As String
        '        Dim LongDevuelta As Integer
        '        Dim Ruta As String
        '        Ruta = Trim(App.Path)
        '        If Right(Ruta, 1) = "\" Then
        '            Ruta = Mid(Ruta, 1, Len(Ruta) - 1)
        '        End If
        '        ' GRABAR INI
        '        lsCadena = "" & Trim(fsConexion)
        '        LongDevuelta = WritePrivateProfileString("CONEXIONES", "NOMBRE_ULTIMA_CONEXION", lsCadena, Ruta + "\Aplliria.ini")
        '        If LongDevuelta = 0 Then GoTo Errores
        '        '
        '        lsCadena = "" & Format(Of Date, "dd/mm/yyyy")() & ", " & Format(Time, "hh:mm:ss")
        '        LongDevuelta = WritePrivateProfileString("CONEXIONES", "FECHA_ULTIMA_CONEXION", lsCadena, Ruta + "\Aplliria.ini")
        '        If LongDevuelta = 0 Then GoTo Errores
        '        Exit Sub
        'Errores:
        '        lsCadena = "Ha ocurrido un error al registrar la última conexión."
        '        MsgBox lsCadena, vbExclamation, "CompuGest"
    End Sub
    Private Sub RellenaEmpresa()
        Dim cmd As New cnRecordset.CnRecordset
        Dim comboSource As New Dictionary(Of String, String)()

        NoActualizar = True

        cmd.Open("SELECT empresa, razon_social FROM empresas", ObjetoGlobal.cn)

        While Not cmd.EOF
            comboSource.Add(Trim(cmd!empresa), Trim(cmd!empresa) + " - " + Trim(cmd!razon_social))
            cmd.MoveNext()
        End While
        cbEmpresa.DataSource = New BindingSource(comboSource, Nothing)
        cbEmpresa.DisplayMember = "Value"
        cbEmpresa.ValueMember = "Key"
        cbEmpresa.SelectedIndex = 0

        NoActualizar = False
        Rellenaejercicios()


    End Sub
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim key As String = DirectCast(ComboBox1.SelectedItem, KeyValuePair(Of String, String)).Key
    '    Dim value As String = DirectCast(ComboBox1.SelectedItem, KeyValuePair(Of String, String)).Value
    '    MessageBox.Show(key & "   " & value)
    'End Sub

    Private Sub Rellenaejercicios()
        Dim cmd As New cnRecordset.CnRecordset
        Dim comboSource As New Dictionary(Of String, String)()

        If Not NoActualizar Then
            cmd.Open("SELECT ejercicio, descripcion FROM ejercicios_contab WHERE empresa='" & cbEmpresa.SelectedValue & "'", ObjetoGlobal.cn)
            While Not cmd.EOF
                comboSource.Add(Trim(cmd!ejercicio), Trim(cmd!descripcion))
                cmd.MoveNext()
            End While
            cbEjercicio.DataSource = New BindingSource(comboSource, Nothing)
            cbEjercicio.DisplayMember = "Value"
            cbEjercicio.ValueMember = "Key"
            cbEjercicio.SelectedIndex = cbEjercicio.Items.Count - 1
        End If


    End Sub

    Private Sub cbEmpresa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEmpresa.SelectedIndexChanged
        BtEntrar.Enabled = False
        Rellenaejercicios()
    End Sub

    Private Sub cbEjercicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEjercicio.SelectedIndexChanged
        BtEntrar.Enabled = True
    End Sub

    Private Sub TxtPassword_TextChanged(sender As Object, e As EventArgs) Handles TxtPassword.TextChanged
        Entrar.Enabled = True
    End Sub

    Private Sub BtEntrar_Click(sender As Object, e As EventArgs) Handles BtEntrar.Click
        Empresaactual = cbEmpresa.SelectedValue
        Ejercicioactual = cbEjercicio.SelectedValue
        ObjetoGlobal.EmpresaActual = Empresaactual
        ObjetoGlobal.EmpresaRazonSocial = cbEmpresa.Text
        ObjetoGlobal.EjercicioActual = Ejercicioactual
        buscar_usuario(TxtUsuario.Text.Trim, ObjetoGlobal.EmpresaActual, ObjetoGlobal.EjercicioActual)
        libcomunes.ObjetoGlobal = ObjetoGlobal
        libcomunes.conta.CalculoNivelDeCuenta()
        resultado = True
        Me.Close()
    End Sub

    Private Sub EjecutarBat()
        Dim pathArchivoBat As String = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "copiar.exe")
        Dim ps As ProcessStartInfo = New ProcessStartInfo
        Dim proceso As Process = New Process()

        If System.IO.File.Exists(pathArchivoBat) Then
            proceso.StartInfo.FileName = pathArchivoBat
            proceso.Start()
            'proceso.WaitForExit()
        End If

        'Dim p As Process = New Process
        'Dim ps As ProcessStartInfo = New ProcessStartInfo
        'ps.FileName = "regedit"
        'ps.Arguments = "/S C:\yourRegFile.reg"
        'p.StartInfo = ps
        'p.Start()

    End Sub

    Private Sub Inicio_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' EjecutarBat()
    End Sub

End Class
Public Class ComboboxItem
    Public Property Text As String
    Public Property Value As Object

    Public Overrides Function ToString() As String
        Return Text
    End Function
End Class