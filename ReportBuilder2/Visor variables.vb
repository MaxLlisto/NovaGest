Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports System.ComponentModel


Public Class Visor
    Public ObjetoGlobal As ObjetoGlobal.ObjetoGlobal
    Public cn As System.Data.SqlClient.SqlConnection
        Dim lbdt As DataTable = New DataTable
        Dim pImageList As ImageList = New ImageList
        Dim imagenes As Dictionary(Of String, Bitmap)
    Dim codigo_imagen As String
    Public resultado = DialogResult.Cancel
    Dim nombrearchivo As String
    Dim extension As String
    Public item As String



    Public Sub New()

            ' Esta llamada es exigida por el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        End Sub


        Private Sub Visor_Load(sender As Object, e As EventArgs) Handles Me.Load
            Dim Rs As New cnRecordset.CnRecordset
            Dim Sql As String
            Dim ms As MemoryStream
            Dim arrImage() As Byte
            Dim imgList As ImageList = New ImageList

            lbdt.Columns.Add("tipo_imagen")
            lbdt.Columns.Add("codigo_imagen")
            lbdt.Columns.Add("extension")
            lbdt.Columns.Add("imagen")

            Sql = "SELECT * FROM zzImagenes_p "
            ListView1.Items.Clear()
            If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
                While Not Rs.EOF
                    arrImage = CType(Rs!imagen, Byte())
                    ms = New MemoryStream(arrImage)
                    ImageList1.Images.Add(Rs!codigo_imagen, Image.FromStream(ms))
                    ListView1.Items.Add(Rs!codigo_imagen, Rs!codigo_imagen)
                    Rs.MoveNext()
                End While
            End If

        End Sub

        Private Sub refrescar_imagenes()
        End Sub

        Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
            Dim Rs As New cnRecordset.CnRecordset
            Dim Sql As String
            Dim ms As MemoryStream
            Dim arrImage() As Byte
            Dim img As Image


            If ListView1.SelectedItems.Count > 0 Then
                codigo_imagen = ListView1.SelectedItems(0).SubItems(0).Text
                Sql = "SELECT * FROM zzImagenes_p WHERE codigo_imagen = '" & ListView1.SelectedItems(0).SubItems(0).Text & "'"
                If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
                    arrImage = CType(Rs!imagen, Byte())
                    ms = New MemoryStream(arrImage)
                PictureBox1.Image = Image.FromStream(ms)
                nombrearchivo = ""
            End If
            End If

        End Sub
        Public Property img() As Image
            Get
                Return PictureBox1.Image
            End Get
            Set(ByVal value As Image)
                PictureBox1.Image = value
            End Set
        End Property

        Public Property codigo() As String
            Get
                Return codigo_imagen
            End Get
            Set(ByVal value As String)
                codigo_imagen = value
            End Set
        End Property

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        resultado = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            Me.Close()
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            Dim file As New OpenFileDialog()
        Dim ext As String
        Dim tipo As String


            file.Filter = "Archivo JPG|*.jpg|PNG|*.png|GIF|*.gif|BMP|*.bmp"
            If file.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(file.FileName)
            ext = UCase(Strings.Right(Trim(file.FileName), 3))
            nombrearchivo = file.FileName
            extension = ext
            'Dim ms1 As New System.IO.MemoryStream() 'Creamos el MemoryStream.
            Select Case ext
                Case "JPG"
                    'PictureBox1.BackgroundImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg) 'Salvamos el imagen que tenemos cargada en el PictureBox en el MemoryStream.
                    tipo = "JPG"
                Case "PNG"
                    'PictureBox1.BackgroundImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Png) 'Salvamos el imagen que tenomos cargada en el PictureBox en el MemoryStream.
                    tipo = "PNG"
                Case "BMP"
                    'PictureBox1.BackgroundImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp) 'Salvamos el imagen que tenomos cargada en el PictureBox en el MemoryStream.
                    tipo = "BMP"
                Case "GIF"
                    'PictureBox1.BackgroundImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Gif) 'Salvamos el imagen que tenomos cargada en el PictureBox en el MemoryStream.
                    tipo = "GIF"
            End Select
            nombrearchivo = file.FileName
            extension = tipo

            'Sql = "SELECT * FROM zzImagenes_p 1=0"
            'If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
            '    Rs.AddNew()
            '    Rs!tipo_imagen = "IMAGEN"
            '    Rs!codigo_imagen = "CODIGO"
            '    Rs!extension = tipo
            '    Rs!imagen = ms1.GetBuffer()
            'End If
        End If

        End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Rs As New cnRecordset.CnRecordset
        Dim Sql As String
        Dim Message As String
        Dim Title As String
        Dim defecto As String
        Dim MyValue As String

        If Trim(nombrearchivo) = "" Then
            Return
        End If

        Message = "Introduce el código para la imagen"    ' Set prompt.
        Title = "Código de la imagen"    ' Set title.
        defecto = SacarCodigo(nombrearchivo)
        MyValue = InputBox(Message, Title, defecto)

        Dim ms1 As New System.IO.MemoryStream() 'Creamos el MemoryStream.
        Select Case extension
            Case "JPG"
                PictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg) 'Salvamos el imagen que tenemos cargada en el PictureBox en el MemoryStream.
            Case "PNG"
                PictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Png) 'Salvamos el imagen que tenomos cargada en el PictureBox en el MemoryStream.
            Case "BMP"
                PictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp) 'Salvamos el imagen que tenomos cargada en el PictureBox en el MemoryStream.
            Case "GIF"
                PictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Gif) 'Salvamos el imagen que tenomos cargada en el PictureBox en el MemoryStream.
        End Select

        Sql = "SELECT * FROM zzImagenes_p where 1=0"
        If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
            Rs.AddNew()
            Rs!tipo_imagen = "IMAGEN"
            Rs!codigo_imagen = MyValue
            Rs!extension = extension
            Rs!imagen = ms1.GetBuffer()
            Rs.Update()
        End If

    End Sub
    'Private Function ImageToByteArray(imag As Image) As Byte
    '    Dim ms As MemoryStream = New System.IO.MemoryStream() 'Creamos el MemoryStream.
    '    imag.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
    '    Return ms.ToArray()
    'End Function

    Private Function SacarCodigo(ByVal nombre As String) As String
        Dim retorno As String
        retorno = Trim(nombre)
        retorno = Strings.Right(retorno, Len(retorno) - InStrRev(retorno, "\"))
        retorno = Strings.Left(retorno, InStrRev(retorno, ".") - 1)
        Return retorno
    End Function



    'Private Sub MostrarImagenes()
    '    Dim Rs As New cnRecordset.CnRecordset
    '    Dim Sql As String
    '    Dim ms As MemoryStream
    '    Dim arrImage() As Byte
    '    Dim imagen1 As System.Drawing.Bitmap

    '    Sql = "SELECT * FROM zzImagenes_p where not imagen is null "
    '    ListView1.Items.Clear()
    '    'imgList.ImageSize = New Size(64, 64)
    '    If Rs.Open(Sql, ObjetoGlobal.cn, True) Then
    '        While Not Rs.EOF
    '            arrImage = CType(Rs!imagen, Byte())
    '            ms = New MemoryStream(arrImage)
    '            imagen1 = CType(Image.FromStream(ms), Bitmap)
    '            imagenes.Add(Rs!codigo_imagen, imagen1)
    '            Rs.MoveNext()
    '        End While
    '    End If

    '    If imagenes.Count > 0 Then
    '        Dim lista = New ImageList()

    '        For Each Item As KeyValuePair(Of String, Bitmap) In imagenes
    '            ListView1.LargeImageList = lista
    '            lista.Images.Add(Item.Key, Item.Value)
    '            Dim listViewItem = ListView1.Items.Add(Item.Key)
    '            listViewItem.ImageKey = Item.Key
    '        Next

    '    Else
    '            MessageBox.Show("no hay imagenes para mostrar")
    '        End If
    '    End Sub

    '    Private Sub listView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    '        If ListView1.SelectedItems.Count = 0 OrElse imagenes Is Nothing Then Return
    '        PictureBox1.Image = imagenes(ListView1.SelectedItems(0).ImageKey)
    '    End Sub
End Class


'Private Sub listBox1_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
'    Dim img As Image
'    Dim x As Single = 10
'    Dim y As Single = 10
'    Dim width As Single = 120
'    Dim height As Single = 120
'    If e.Index >= 0 Then
'        'img = sender.items(e.Index)
'        e.Graphics.DrawImage(sender.items(e.Index), x, y + (128 * e.Index), width, height)
'    End If
'End Sub

'Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

'End Sub
