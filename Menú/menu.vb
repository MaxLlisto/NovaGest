Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections
Imports System.Reflection

Public Class menu
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public PerfilUsuario As String

    Private Sub menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim frmInicio As Inicio

        frmInicio = New Inicio
        frmInicio.ShowDialog()
        If frmInicio.resultado Then
            PerfilUsuario = frmInicio.PerfilUsuario
            ObjetoGlobal = frmInicio.ObjetoGlobal
            ' Librerías comunes
            libcomunes.ObjetoGlobal = ObjetoGlobal
        Else
            'Application.Exit()
            Environment.Exit(0)
        End If

        lblEmpresa.Text = ObjetoGlobal.EmpresaRazonSocial
        lblEjercicio.Text = ObjetoGlobal.EjercicioActual
        lblUsuario.Text = ObjetoGlobal.UsuarioActual

        ObtenerCategorias()
        cbSecciones.SelectedItem = 1
        ObtenerMenuCategoria()
        libcomunes.conta.CalculoNivelDeCuenta()

    End Sub
    Public Sub New()
        Dim aCal(0) As Array
        Dim aCab(0) As String

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        ObjetoGlobal = New ObjetoGlobal.ObjetoGlobal


        'If ObjetoGlobal.Inicializar("") = False Then
        'MsgBox("No se puede abrir la conexión a la BD")
        'End If
        PerfilUsuario = "ADMINISTRADOR"

        AddHandler tvmenu.MouseDoubleClick, AddressOf tvmenu_MouseDoubleClick
        'AddHandler tvmenu.Click, AddressOf tvmenu_Click
        'AddHandler tvmenu.NodeMouseDoubleClick, AddressOf tvmenu_NodeMouseDoubleClick

    End Sub
    Private Sub ObtenerCategorias()
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim ElementosMenu As New List(Of ItemsMenu)
        Dim myImageList As New ImageList()
        Dim ExeDirectory As String = String.Format("{0}\", Environment.CurrentDirectory)
        Dim nIcono As Integer
        myImageList.ImageSize = New Size(32, 32)
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men1.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men2.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men3.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men4.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men5.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men6.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men7.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men8.png"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "men9.png"))
        myImageList.TransparentColor = System.Drawing.Color.Transparent
        cbSecciones.ImageList = myImageList

        sql = "SELECT ZZZMENU_USUARIOS_N.*, ZZZMENU_NET.* FROM ZZZMENU_USUARIOS_N INNER JOIN ZZZMENU_NET ON ZZZMENU_NET.CODIGO = ZZZMENU_USUARIOS_N.CODIGO WHERE ZZZMENU_NET.TIPO_LLAMADA = 'N' AND ZZZMENU_USUARIOS_N.PERFIL='" & Trim(PerfilUsuario) & "' AND N0 = 0 ORDER BY NUMERO_TW"
        Rs.Open(sql, ObjetoGlobal.cn)
        cbSecciones.Items.Clear()
        'Rs!n0 = 0 Then
        nIcono = 0
        Me.cbSecciones.ComboFont = New Font("Verdana", 8, FontStyle.Bold)
        While Not Rs.EOF
            'ElementosMenu.Add(New ItemsMenu(Rs!texto_menu, Rs!numero_tw))
            Me.cbSecciones.ComboAddItem(New ItemsMenu(Rs!texto_menu, Rs!numero_tw), Color.Black, nIcono, Color.White)
            nIcono = nIcono + 1
            Rs.MoveNext()
        End While
        'cbSecciones.DataSource = ElementosMenu
        cbSecciones.DisplayMember = "ItemText"
        cbSecciones.ValueMember = "ItemValue"
        agregar(Me.cbSecciones)



    End Sub

    Function agregar(ByVal caja As ComboBox)
        AddHandler caja.SelectedIndexChanged, AddressOf cbSecciones_SelectedIndexChanged
    End Function

    Private Sub cbSecciones_SelectedIndexChanged(sender As Object, e As EventArgs) ' Handles cbSecciones.SelectedIndexChanged
        If cbSecciones.Text.ToUpper.Trim = "SALIR" Then
            Me.Close()
        Else
            ObtenerMenuCategoria()
        End If

    End Sub
    Private Sub ObtenerMenuCategoria()
        Dim sql As String
        Dim Rs As New cnRecordset.CnRecordset
        Dim Categoria As New List(Of String)
        Dim ElementosMenu As ArrayList = New ArrayList()
        Dim dict As New Dictionary(Of String, String)()
        Dim claveTree As String
        Dim RsDt As System.Data.DataTable = New DataTable
        Dim RsDV As DataView
        Dim RsOr As System.Data.DataTable
        Dim i As Integer
        Dim Orden As Integer
        Dim nuevoNodo As New TreeNode
        Dim myImageList As New ImageList()
        Dim ExeDirectory As String = String.Format("{0}\", Environment.CurrentDirectory)

        myImageList.Images.Add(Image.FromFile(ExeDirectory & "ico01.ico"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "ico02.ico"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "ico03.ico"))
        myImageList.Images.Add(Image.FromFile(ExeDirectory & "ico04.ico"))

        ' Assign the ImageList to the TreeView.
        tvmenu.ImageList = myImageList
        tvmenu.ImageIndex = 1
        tvmenu.SelectedImageIndex = 2

        RsDt.Columns.Add("clave")
        RsDt.Columns.Add("text_menu")
        RsDt.Columns.Add("codigo", GetType(Integer))
        RsDt.Columns.Add("tipo")



        sql = "SELECT ZZZMENU_USUARIOS_N.*, ZZZMENU_NET.* FROM ZZZMENU_USUARIOS_N INNER JOIN ZZZMENU_NET ON ZZZMENU_NET.CODIGO = ZZZMENU_USUARIOS_N.CODIGO WHERE ZZZMENU_NET.TIPO_LLAMADA = 'N' AND ZZZMENU_USUARIOS_N.PERFIL='" & Trim(PerfilUsuario) & "' AND numero_tw =" + Me.cbSecciones.SelectedItem.ItemValue.ToString() + " AND N0 > 0 ORDER BY NUMERO_TW,N0,N1,N2,N3,N4,N5,N6"

        Rs.Open(sql, ObjetoGlobal.cn)


        For i = 1 To Rs.RecordCount
            Rs.AbsolutePosition = i
            claveTree = Devuelveclave(Rs)
            RsDt.Rows.Add(claveTree, Rs!texto_menu.ToString.Trim, Rs!codigo, "IT")
        Next

        RsDV = RsDt.DefaultView
        RsDV.Sort = "clave ASC"
        RsOr = RsDV.ToTable
        Orden = 0

        tvmenu.Nodes.Clear()
        tvmenu.BeginUpdate()

        nuevoNodo.Text = cbSecciones.Text
        nuevoNodo.ImageIndex = 0

        For i = 0 To RsOr.Rows.Count - 1
            If EsNodo(RsOr.Rows(i)(0), RsOr) Then
                RsOr.Rows(i)(3) = "NO"
            End If
        Next


        Do
            HacerArbol(nuevoNodo, RsOr, "", Orden, 0)
        Loop While orden <= RsOr.Rows.Count - 1
        nuevoNodo.Expand()
        tvmenu.Nodes.Add(nuevoNodo)


        tvmenu.EndUpdate()

    End Sub

    Private Function HacerArbol(ByRef oTree As TreeNode, ByRef rs As System.Data.DataTable, ByVal clave As String, ByRef orden As Integer, ByVal nivel As Integer) As String
        Dim claveTree As String
        Dim OldClave As String
        Dim clavedevuelta As String
        Dim nuevoNodo As New TreeNode
        Dim ord As Integer = 0
        Dim sNivel As String
        Dim HayNodo As Boolean
        Dim claveanterior As String

        If rs.Rows(orden)(3) = "NO" Then
            HayNodo = False
            OldClave = rs.Rows(orden)(0) '+ "000000"
            claveTree = rs.Rows(orden)(0) 'clave
            nuevoNodo.Text = rs.Rows(orden)(1) ' & "-" & rs.Rows(orden)(0)
            nuevoNodo.ImageIndex = 1
            nuevoNodo.SelectedImageIndex = 2
            oTree.Nodes.Add(nuevoNodo)
            claveanterior = claveTree
            nivel = nivel + 1
            sNivel = Strings.Left(claveTree, (nivel * 6))
        ElseIf nivel = 0 Then
            OldClave = rs.Rows(orden)(0)
            claveTree = rs.Rows(orden)(0)
            oTree.Nodes.Add(rs.Rows(orden)(2), rs.Rows(orden)(1), 3)
            claveanterior = claveTree
            nivel = nivel + 1
            sNivel = Strings.Left(claveTree, (nivel * 6))
            orden = orden + 1
            Return ""
        Else
            ord = 1
            claveanterior = rs.Rows(orden)(0) 'clave
        End If

        Do
            If ord > 0 And Not HayNodo Then
                nuevoNodo.Nodes.Add(rs.Rows(orden)(2), rs.Rows(orden)(1), 3)
            End If
            If Not HayNodo Then
                ord = ord + 1
                orden = orden + 1
            End If
            HayNodo = False
            If orden <= rs.Rows.Count - 1 Then
                claveTree = rs.Rows(orden)(0)
                If rs.Rows(orden)(3) = "NO" Then
                    If claveTree.Trim.Length >= claveanterior.Trim.Length Then ' A un nivel superior
                        clavedevuelta = HacerArbol(nuevoNodo, rs, claveTree, orden, nivel)
                        If claveTree.Trim.Length > clavedevuelta.Trim.Length Then
                            Return clavedevuelta
                        Else
                            If rs.Rows(orden)(3) = "NO" Then
                                HayNodo = True
                            End If
                        End If
                    ElseIf claveTree.Trim.Length <= claveanterior.Trim.Length Then
                        Return claveTree
                    End If
                ElseIf claveTree.Trim.Length < claveanterior.Trim.Length Then
                    Return claveTree
                End If
                claveanterior = claveTree
            End If
        Loop While orden <= rs.Rows.Count - 1

        Return ""
    End Function


    Private Function Devuelveclave(ByRef rs As cnRecordset.CnRecordset) As String
        Dim Campo As String
        Dim i As Integer
        Dim clave As String
        Dim letras(7) As String

        letras(0) = "A"
        letras(1) = "B"
        letras(2) = "C"
        letras(3) = "D"
        letras(4) = "E"
        letras(5) = "F"
        letras(6) = "G"

        clave = ""

        For i = 0 To 6
            Campo = "N" & i
            If rs(Campo) <> 0 Then
                clave = clave + letras(i) & Strings.Right("00000" & rs(Campo), 5)
            Else
                Return clave
            End If
        Next

        Return clave

    End Function

    Private Sub Tvmenu_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvmenu.AfterSelect
        'e.Node.Text
        'e.Node.Text
        PerfilUsuario = "ADMINISTRADOR"

    End Sub
    Function EsNodo(ByVal clave As String, ByRef dt As System.Data.DataTable) As Boolean
        Dim encontrado() As DataRow
        Dim filtro As String = ""

        filtro = "clave LIKE '" & clave.Trim & "*'"

        encontrado = dt.Select(filtro)
        If encontrado.Length > 1 Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Sub EjecutarSeleccion(item)
        Dim Rs As New cnRecordset.CnRecordset
        Dim sql As String
        Dim Arcdll As String
        Dim formulario As String
        Dim og As ObjetoGlobal.ObjetoGlobal = ObjetoGlobal

        Try
            If Trim("" & item.name) = "" Then
                Return
            End If
            sql = "SELECT * FROM zzzmenu_NET WHERE codigo = " & item.name
            Rs.Open(sql, ObjetoGlobal.cn)
            If Not Rs.EOF Then
                If Not IsDBNull(Rs!proyecto) Then
                    If Not IsDBNull(Rs!formulario) And Trim(Rs!formulario) <> "" Then
                        formulario = UCase(Rs!formulario)
                    Else
                        If trim(Rs!proyecto) = "cambiarempresa" Then
                            Dim oFrm As CambiarEjercicio = New CambiarEjercicio
                            oFrm.og = ObjetoGlobal
                            oFrm.MiShowDialog()
                            If oFrm.resultado Then
                                ObjetoGlobal = oFrm.og
                                ' Librerías comunes
                                libcomunes.ModuloControl.ObjetoGlobal = ObjetoGlobal
                                lblEmpresa.Text = ObjetoGlobal.EmpresaRazonSocial
                                lblEjercicio.Text = ObjetoGlobal.EjercicioActual
                                lblUsuario.Text = ObjetoGlobal.UsuarioActual
                                Return
                            End If
                        Else
                            Return
                        End If
                    End If

                    If Not formulario.IndexOf(".EXE") Then
                        Dim proces As New Process()
                        'proces.StartInfo.FileName = formulario
                        proces.Start()
                    Else
                        Dim tempAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                        Dim Ass As Reflection.Assembly = Reflection.Assembly.LoadFrom(Trim(Rs!proyecto) & ".dll")
                        Dim obj As Form = Ass.CreateInstance(Trim(Rs!proyecto) & "." & Trim(Rs!formulario), True)
                        CallByName(obj, "OG", Microsoft.VisualBasic.CallType.Set, ObjetoGlobal)
                        obj.Show()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(Err.Description)
        End Try

    End Sub

    Private Sub tvmenu_Click(sender As Object, e As EventArgs) 'Handles tvmenu.Click
        PerfilUsuario = "ADMINISTRADOR"
        'MessageBox.Show("Click.")
    End Sub

    Private Sub tvmenu_MouseDoubleClick(sender As Object, e As MouseEventArgs) 'Handles tvmenu.MouseDoubleClick
        Dim item As System.Windows.Forms.TreeNode = tvmenu.GetNodeAt(e.Location)

        If item IsNot Nothing Then
            EjecutarSeleccion(item)
            PerfilUsuario = "ADMINISTRADOR"
        End If

    End Sub

    Private Sub tvmenu_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) 'Handles tvmenu.NodeMouseDoubleClick
        MessageBox.Show("Mouse Doble Click.")
    End Sub




End Class


'============
Public Class ItemsMenu1
    Private _itemtext As String
    Private _itemvalue As Integer
    Public Property ItemText() As String
        Get
            Return _itemtext
        End Get
        Set(ByVal value As String)
            _itemtext = value
        End Set
    End Property
    Public Property ItemValue() As Integer
        Get
            Return _itemvalue
        End Get
        Set(ByVal value As Integer)
            _itemvalue = value
        End Set
    End Property
    Public Sub New(ByVal displayText As String, ByVal value As Integer)
        _itemtext = displayText
        _itemvalue = value
    End Sub
    Public Overrides Function ToString() As String
        Return _itemvalue.ToString.Trim
    End Function

End Class

