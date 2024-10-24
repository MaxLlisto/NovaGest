Imports System.Data.SqlClient

'Versión 0 12/6/2018

Public Class CnRecordset

    Private Enum Tipo_de_cnRecordset
        Cliente_SoloLectura = 0
        Cliente_Actualizable
        Servidor_SoloLectura
    End Enum

    Private Enum Situacion_cnRecordset
        Cerrado = 0
        AbiertoYVacio
        Abierto
        EnCreacion
        EnModificacion
    End Enum

    Private m_CadenaSQL As String
    Private m_TipoRecordset As Tipo_de_cnRecordset
    Private m_ValoresAnteriores() As Object
    Private m_Buffer As Long

    Private cnConexion As Data.SqlClient.SqlConnection
    Private cnSet As Data.DataSet
    Private cnAdapter As Data.SqlClient.SqlDataAdapter
    Private cnSituacion As Situacion_cnRecordset
    Private cnBuilder As SqlCommandBuilder
    Private cnPuntero As Integer
    Private cnDataReader As Data.SqlClient.SqlDataReader
    Private cnCommand As Data.SqlClient.SqlCommand

    Private cnNuevaFila As DataRow

    Private m_EOF As Boolean 'NUEVO
    Private m_BOF As Boolean 'NUEVO

    Public Sub New()
        cnSituacion = Situacion_cnRecordset.Cerrado
        m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura
    End Sub


    'Propiedades públicas 

    Public ReadOnly Property Actualizable() As Boolean
        Get
            Dim MensajeError As String = ""

            Try
                Actualizable = False
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                End If
                If m_TipoRecordset = Tipo_de_cnRecordset.Cliente_Actualizable Then
                    Actualizable = True
                End If
            Catch e As Exception
                MensajeError = "No se puede devolver el tipo de recordset." + Trim(MensajeError)
                Throw New ArgumentException(MensajeError)
            End Try
        End Get
    End Property

    Public ReadOnly Property CadenaSQL() As String
        Get
            Dim MensajeError As String = ""
            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                End If
            Catch e As Exception
                MensajeError = "No se puede devolver la cadena SQL. " + Trim(MensajeError)
                Throw New ArgumentException(MensajeError)
            End Try
            Return m_CadenaSQL
        End Get
    End Property


    Default Public Property Valor(ByVal Cadena As String) As Object
        Get
            Dim MensajeError As String = "", NColumna As Integer
            Valor = Nothing
            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                Else
                    If cnSituacion = Situacion_cnRecordset.EnCreacion Then
                        Try
                            If IsNumeric(Cadena) Then
                                NColumna = Val(Cadena)
                                If NColumna >= 0 And NColumna < cnSet.Tables(0).Columns.Count Then
                                    Valor = cnNuevaFila.Item(NColumna)
                                Else
                                    MensajeError = "No es posible obtener el valor de la columna (" + Trim(Cadena) + ")"
                                    Throw New ArgumentException(MensajeError)
                                End If
                            Else
                                Valor = cnNuevaFila.Item(Cadena)
                            End If
                        Catch ex2 As Exception
                            MensajeError = "No es posible obtener el valor de la columna en la fila que se está creando." + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                            Throw New ArgumentException(MensajeError)
                        End Try
                    Else
                        If cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                            MensajeError = "No existe registro actual en el recordset."
                            Throw New ArgumentException(MensajeError)
                        ElseIf EOF = True Then 'NUEVO
                            MensajeError = "No existe registro actual en el recordset. (EOF)" 'NUEVO
                            Throw New ArgumentException(MensajeError) 'NUEVO
                        ElseIf BOF = True Then 'NUEVO
                            MensajeError = "No existe registro actual en el recordset. (BOF)" 'NUEVO
                            Throw New ArgumentException(MensajeError) 'NUEVO
                        Else
                            If m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                                Try
                                    If IsNumeric(Cadena) Then
                                        NColumna = Val(Cadena)
                                        If NColumna >= 0 And NColumna < cnDataReader.FieldCount Then
                                            Valor = cnDataReader.GetValue(NColumna)
                                        Else
                                            MensajeError = "No es posible obtener el valor de la columna (" + Trim(Cadena) + ")"
                                            Throw New ArgumentException(MensajeError)
                                        End If
                                    Else
                                        Valor = cnDataReader.GetValue(cnDataReader.GetOrdinal(Cadena))
                                    End If
                                Catch ex2 As Exception
                                    MsgBox("No se ha podido obtener el valor de la columna = " + Trim(Cadena) + " (Recordset de tipo 'servidor') " & ex2.Message)
                                    Throw New ArgumentException(MensajeError)
                                End Try
                            Else
                                Try
                                    If IsNumeric(Cadena) Then
                                        NColumna = Val(Cadena)
                                        If NColumna >= 0 And NColumna < cnSet.Tables(0).Columns.Count Then
                                            Valor = cnSet.Tables(0).Rows(cnPuntero - 1).Item(NColumna)
                                            If cnSet.Tables(0).Rows(0).Item(3).GetType.Name = "Decimal" Then
                                                Valor = Replace(Valor, ",", ".")
                                            End If
                                        Else
                                                MensajeError = "No es posible obtener el valor de la columna (" + Trim(Cadena) + ")"
                                            Throw New ArgumentException(MensajeError)
                                        End If
                                    Else
                                        Valor = cnSet.Tables(0).Rows(cnPuntero - 1).Item(Cadena)
                                    End If
                                Catch ex2 As Exception
                                    MensajeError = "No es posible obtener el valor de la columna en la fila actual (" + CStr(cnPuntero) + ")" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                                    Throw New ArgumentException(MensajeError)
                                End Try
                            End If
                        End If
                    End If
                End If
            Catch e As Exception
                MensajeError = "Error al leer el campo '" & Trim(Cadena) & "' :" & vbNewLine & e.Message
                Throw New ArgumentException(MensajeError)
            End Try


        End Get
        Set(ByVal X As Object)
            Dim MensajeError As String = "", i As Integer, NColumna As Integer

            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    Throw New ArgumentException("El recodset está cerrado: no se puede actualizar el dato")
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura Then
                    Throw New ArgumentException("No se puede actualizar el dato. El recordset es de 'solo-lectura'")
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    Throw New ArgumentException("No se puede actualizar el dato. El recordset es de 'Servidor:solo-lectura'")
                ElseIf cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                    Throw New ArgumentException("El recodset no contiene registros: no se puede actualizar el dato")
                ElseIf EOF = True Then 'NUEVO
                    MensajeError = "No se puede actualizar el dato. No existe registro actual en el recordset. (EOF)" 'NUEVO
                    Throw New ArgumentException(MensajeError) 'NUEVO
                ElseIf BOF = True Then 'NUEVO
                    MensajeError = "No se puede actualizar el dato. No existe registro actual en el recordset. (BOF)" 'NUEVO
                    Throw New ArgumentException(MensajeError) 'NUEVO


                ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                    Try
                        If IsNumeric(Cadena) Then
                            NColumna = Val(Cadena)
                            If NColumna >= 0 And NColumna < cnSet.Tables(0).Columns.Count Then
                                cnNuevaFila.Item(NColumna) = X
                            Else
                                MensajeError = "No es posible modificar el valor de la columna (" + Trim(Cadena) + ")"
                                Throw New ArgumentException(MensajeError)
                            End If
                        Else
                            cnNuevaFila.Item(Cadena) = X
                        End If
                    Catch ex2 As Exception
                        MensajeError = "No se pueden asociar ese valor al registro que se está creando" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                ElseIf cnPuntero >= 1 And (cnSituacion = Situacion_cnRecordset.Abierto Or cnSituacion = Situacion_cnRecordset.EnModificacion) Then
                    If cnSituacion = Situacion_cnRecordset.Abierto Then
                        Try
                            For i = 0 To cnSet.Tables(0).Columns.Count - 1
                                m_ValoresAnteriores(i) = cnSet.Tables(0).Rows(cnPuntero - 1).Item(i)
                            Next
                            cnSituacion = Situacion_cnRecordset.EnModificacion
                        Catch ex2 As Exception
                            MensajeError = "No es posible cambiar la situacion del recordset a 'EnModificacion' cnPuntero = " + CStr(cnPuntero) + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                            Throw New ArgumentException(MensajeError)
                        End Try
                    End If
                    Try
                        If IsNumeric(Cadena) Then
                            NColumna = Val(Cadena)
                            If NColumna >= 0 And NColumna < cnSet.Tables(0).Columns.Count Then
                                cnSet.Tables(0).Rows(cnPuntero - 1).Item(NColumna) = X
                            Else
                                MensajeError = "Número de columna inexistente"
                                Throw New ArgumentException(MensajeError)
                            End If
                        Else
                            cnSet.Tables(0).Rows(cnPuntero - 1).Item(Cadena) = X
                        End If
                    Catch ex2 As Exception
                        MensajeError = "No se pueden asocia el valor '" + X.ToString + " al registro que se está modificando" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                End If
            Catch ex As Exception
                MensajeError = "Error al modificar el valor del campo '" + Trim(Cadena) + "'" + vbNewLine + ex.Message
                Throw New ArgumentException(MensajeError)
            End Try
        End Set
    End Property

    Public Property AbsolutePosition() As Integer
        Get
            Dim MensajeError As String = ""
            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf cnPuntero >= 1 And (cnSituacion = Situacion_cnRecordset.EnCreacion Or cnSituacion = Situacion_cnRecordset.EnModificacion) Then
                    MensajeError = "El recordset está creación/modificación."
                    Throw New ArgumentException(MensajeError)
                ElseIf cnPuntero < 1 Or cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                    MensajeError = "El recorsdset no devuelve ningún registro"
                    Throw New ArgumentException(MensajeError)
                ElseIf eof = True Then 'NUEVO
                    MensajeError = "El recorsdset no devuelve ningún registro (EOF)" 'NUEVO
                    Throw New ArgumentException(MensajeError) 'NUEVO
                ElseIf bof = True Then 'NUEVO
                    MensajeError = "El recorsdset no devuelve ningún registro (BOF)" 'NUEVO
                    Throw New ArgumentException(MensajeError) 'NUEVO
                End If
                If m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    AbsolutePosition = cnPuntero
                ElseIf cnSet.Tables(0).Rows.Count = 0 Then
                    MensajeError = "El recorsdset no devuelve ningún registro"
                    Throw New ArgumentException(MensajeError)
                Else
                    AbsolutePosition = cnPuntero
                End If
            Catch ex As Exception
                MensajeError = "No se puede devolver el valor de AbsolutePosition." + Trim(MensajeError)
                Throw New ArgumentException("No es posible mostrar el número de registro actual." + vbNewLine + ex.Message)
            End Try
        End Get


        Set(ByVal X As Integer)
            Dim MensajeError As String = ""

            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    MensajeError = "No se puede establecer el valor de AbsolutePosition (Recordset de modo 'servidor')" + Trim(MensajeError)
                    Throw New ArgumentException(MensajeError)
                ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Or cnSituacion = Situacion_cnRecordset.EnModificacion Then
                    MensajeError = "No se puede variar el registro actual del recordset sin actualizar o cancelar la creación/modificación."
                    Throw New ArgumentException(MensajeError)
                ElseIf cnSet.Tables(0).Rows.Count = 0 Or cnSituacion = Situacion_cnRecordset.Abiertoyvacio Then
                    MensajeError = "El recorsdset no devuelve ningún registro"
                    Throw New ArgumentException(MensajeError)
                Else
                    If X > cnSet.Tables(0).Rows.Count Or X < 1 Then
                        MensajeError = "El número de registro indicado no existe"
                        Throw New ArgumentException(MensajeError)
                    End If
                    cnPuntero = X
                    m_EOF = False 'NUEVO
                    m_BOF = False 'NUEVO
                End If
            Catch ex As Exception
                Throw New ArgumentException("No es posible establecer como registro actual el registro número:" + CStr(X) + "." + vbNewLine + ex.Message)
            End Try
        End Set
    End Property

    Public ReadOnly Property RecordCount As Integer
        Get
            Dim MensajeError As String = ""

            Try
                RecordCount = 0
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    MensajeError = "No se puede consultar el valor de la propiedad.(Recordset de modo 'servidor')"
                    Throw New ArgumentException(MensajeError)
                Else
                    RecordCount = cnSet.Tables(0).Rows.Count
                    If cnSituacion = Situacion_cnRecordset.EnCreacion Then RecordCount = RecordCount + 1
                End If
            Catch ex As Exception
                Throw New ArgumentException("No es posible mostrar la propiedad 'Recordcount'." + vbNewLine + ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property CuantosCampos As Integer
        Get
            Dim MensajeError As String = ""

            Try
                CuantosCampos = 0
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    CuantosCampos = cnDataReader.FieldCount
                Else
                    CuantosCampos = cnSet.Tables(0).Columns.Count
                End If
            Catch ex As Exception
                Throw New ArgumentException("No es posible mostrar la propiedad 'CuantosCampos'." + vbNewLine + ex.Message)
            End Try

        End Get
    End Property

    Public ReadOnly Property NombreCampo(ByVal NColumna As Integer) As String
        Get
            Dim MensajeError As String = ""

            Try
                NombreCampo = ""
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    If NColumna < 0 Or NColumna >= cnDataReader.FieldCount Then
                        MensajeError = "El número de columna indicado no existe"
                        Throw New ArgumentException(MensajeError)
                    Else
                        NombreCampo = cnDataReader.GetName(NColumna)
                    End If
                ElseIf NColumna < 0 Or NColumna >= cnSet.Tables(0).Columns.Count Then
                    MensajeError = "El número de columna indicado no existe"
                        Throw New ArgumentException(MensajeError)
                    Else
                        NombreCampo = cnSet.Tables(0).Columns(NColumna).ColumnName
                End If
            Catch ex As Exception
                Throw New ArgumentException("No es posible mostrar el nombre de la columna indicada: '" + CStr(NColumna) + "'" + vbNewLine + ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property EOF As Boolean
        Get
            Dim MensajeError As String = ""

            Try
                EOF = True
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    MensajeError = "No se puede consultar el valor de la propiedad.(Recordset de modo 'servidor')"
                    Throw New ArgumentException(MensajeError)
                Else 'NUEVO
                    EOF = m_EOF 'NUEVO
                End If
            Catch ex As Exception
                Throw New ArgumentException("No es posible mostrar la propiedad EOF del recordset." + vbNewLine + ex.Message)
            End Try
        End Get
    End Property
    Public ReadOnly Property BOF As Boolean 'NUEVO
        Get
            Dim MensajeError As String = ""

            Try
                BOF = True
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    MensajeError = "No se puede consultar el valor de la propiedad.(Recordset de modo 'servidor')"
                    Throw New ArgumentException(MensajeError)
                Else
                    BOF = m_EOF
                End If
            Catch ex As Exception
                Throw New ArgumentException("No es posible mostrar la propiedad EOF del recordset." + vbNewLine + ex.Message)
            End Try
        End Get
    End Property


    Public Property Buffer() As Long
        Get
            Dim MensajeError As String = ""
            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    MensajeError = "El recordset está cerrado."
                    Throw New ArgumentException(MensajeError)
                ElseIf m_TipoRecordset <> Tipo_de_cnRecordset.Cliente_Actualizable Then
                    MensajeError = "El recordset es de solo-lectura."
                    Throw New ArgumentException(MensajeError)
                Else
                    Buffer = m_Buffer
                End If
            Catch e As Exception
                MensajeError = "Error al obtener el valor de la propiedad 'buffer': " & vbNewLine & e.Message
                Throw New ArgumentException(MensajeError)
            End Try
        End Get

        Set(ByVal B As Long)
            Dim MensajeError As String = ""

            Try
                If cnSituacion = Situacion_cnRecordset.Cerrado Then
                    Throw New ArgumentException("El recodset está cerrado.")
                ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura Then
                    Throw New ArgumentException("El recordset es de 'solo-lectura'")
                Else
                    m_Buffer = B
                End If
            Catch ex As Exception
                MensajeError = "Error al establecerr el valor de la propiedad 'buffer': " + vbNewLine + ex.Message
                Throw New ArgumentException(MensajeError)
            End Try
        End Set
    End Property


    'Procedimientos

    Public Function Open(ByVal Cadena As String, ByVal cn As Data.SqlClient.SqlConnection, Optional ByVal Actualizable As Boolean = False, Optional ByVal ModoServidor As Boolean = False, Optional ByVal Buffer As Integer = 0) As Boolean
        Dim MensajeError As String = ""

        Open = False
        Try
            If cnSituacion <> Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset ya está abierto."
                Throw New ArgumentException(MensajeError)
            Else
                m_EOF = True 'NUEVO
                m_BOF = True 'NUEVO
                If Trim(Cadena) = "" Then
                    MensajeError = "Cadena de SQL vacía"
                    Throw New ArgumentException(MensajeError)
                Else
                    If Actualizable = True Then
                        If ModoServidor = True Then
                            MensajeError = "No se puede indicar el modo 'servidor' en un recordset actualizable."
                            Throw New ArgumentException(MensajeError)
                        Else
                            m_TipoRecordset = Tipo_de_cnRecordset.Cliente_Actualizable
                        End If

                    Else
                        If ModoServidor = True Then
                            m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura
                        Else
                            m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura
                        End If
                    End If
                End If
            End If

            If m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                Try
                    cnConexion = New SqlConnection
                    cnConexion.ConnectionString = cn.ConnectionString
                    cnConexion.Open()
                Catch ex2 As Exception
                    MensajeError = "No se ha podido abrir la conexión (Recordset de tipo 'servidor') " & ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try
                Try
                    cnCommand = New SqlCommand
                    cnCommand.Connection = cnConexion
                    cnCommand.CommandType = CommandType.Text
                    cnCommand.CommandText = Cadena
                    cnDataReader = cnCommand.ExecuteReader()
                Catch ex2 As Exception
                    MensajeError = "No se ha podido abrir el objeto command (Recordset de tipo 'servidor') " & ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try

                If cnDataReader.HasRows = False Then
                    cnSituacion = Situacion_cnRecordset.AbiertoYVacio
                    cnPuntero = 0
                    m_EOF = True 'NUEVO
                    m_BOF = True 'NUEVO
                Else
                    cnSituacion = Situacion_cnRecordset.Abierto
                    cnPuntero = 0
                    m_EOF = False 'NUEVO
                    m_BOF = False 'NUEVO

                    '                    MsgBox(cnDataReader.GetValue(1))
                    'cnDataReader.Read()
                    'MsgBox(cnDataReader.GetValue(1))
                    'cnDataReader.Read()
                    'MsgBox(cnDataReader.GetValue(1))
                    'MsgBox(cnDataReader.GetValue(1))

                End If
            Else
                cnConexion = cn
                Try
                    cnSet = New DataSet
                Catch ex2 As Exception
                    MensajeError = "No se puede generar el Dataset asociado" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try

                Try
                    cnAdapter = New Data.SqlClient.SqlDataAdapter(Cadena, cnConexion)
                    cnAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
                Catch ex2 As Exception
                    MensajeError = "No se puede generar el Adaptador asociado (o no acepta el esquema definido)" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try

                Try
                    cnAdapter.Fill(cnSet)
                Catch ex2 As Exception
                    MensajeError = "No se pueden obtener los datos asociados al recordset" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try

                If Actualizable = True Then
                    Try
                        cnBuilder = New SqlCommandBuilder()
                        cnBuilder.DataAdapter = cnAdapter
                        m_Buffer = Buffer
                    Catch ex2 As Exception
                        MensajeError = "No se puede generar el SQLCommandBuilder asociado al recordset" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                End If
                If cnSet.Tables(0).Rows.Count = 0 Then
                    cnSituacion = Situacion_cnRecordset.AbiertoYVacio
                    cnPuntero = 0
                    m_EOF = True 'NUEVO
                    m_BOF = True 'NUEVO

                Else
                    cnSituacion = Situacion_cnRecordset.Abierto
                    cnPuntero = 1
                    m_EOF = False 'NUEVO
                    m_BOF = False 'NUEVO

                End If
                cnNuevaFila = Nothing
                ReDim m_ValoresAnteriores(cnSet.Tables(0).Columns.Count - 1)
            End If
            m_CadenaSQL = Trim(Cadena)
            Open = True

        Catch ex As Exception
            Throw New ArgumentException("No es posible abrir el recordset." + vbNewLine + ex.Message)
        End Try
    End Function

    Public Function Close(Optional ByVal FuerzaCancelUpdate As Boolean = False) As Boolean
        Dim MensajeError As String = "", i As Integer

        Close = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset ya está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion And FuerzaCancelUpdate = False Then
                MensajeError = "No se puede cerrar el recordset sin actualizar o cancelar la modificación."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion And FuerzaCancelUpdate = False Then
                MensajeError = "No se puede cerrar el recordset sin actualizar o cancelar la creación."
                Throw New ArgumentException(MensajeError)
            Else
                If m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                    Try
                        cnDataReader.Close()
                    Catch ex2 As Exception
                        MensajeError = "Error al cerrar el objeto SQLDatareader del recordset (modo servidor)" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try

                    Try
                        cnCommand.Dispose()
                    Catch ex2 As Exception
                        MensajeError = "Error al cerrar el objeto SQLCommand del recordset (modo servidor)" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try

                    Try
                        cnConexion.Close()
                    Catch ex2 As Exception
                        MensajeError = "Error al cerrar el objeto conexión del recordset (modo servidor)" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                Else
                    Try
                        cnAdapter.Dispose()
                        cnAdapter = Nothing
                    Catch ex2 As Exception
                        MensajeError = "Error al eliminar el adaptador (SqlAdapter) del recordset" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                    If m_TipoRecordset = Tipo_de_cnRecordset.Cliente_Actualizable Then
                        Try
                            cnBuilder.Dispose()
                            cnBuilder = Nothing
                        Catch ex2 As Exception
                            MensajeError = "Error al eliminar el constructor (SqlBuilder) del recordset" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                            Throw New ArgumentException(MensajeError)
                        End Try
                    End If
                    cnNuevaFila = Nothing
                    Try
                        cnSet.Tables(0).Clear()
                    Catch ex2 As Exception
                        MensajeError = "Error al eliminar los datos de la TableSet (procedimiento 'clear') del recordset" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                    Try
                        For i = 0 To m_ValoresAnteriores.GetUpperBound(0) : m_ValoresAnteriores(i) = Nothing : Next
                    Catch ex2 As Exception
                        MensajeError = "Error al inicializar los datos del array 'ValoresAnteriores' del recordset" + vbNewLine + ex2.Source + vbNewLine + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                    cnSituacion = Situacion_cnRecordset.Cerrado
                    m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura
                    Close = True
                End If
            End If
        Catch ex As Exception
            Throw New ArgumentException("No es posible cerrar el recordset." + vbNewLine + ex.Message)
        End Try

    End Function

    Public Function MoveFirst() As Boolean
        Dim MensajeError As String = ""

        MoveFirst = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la modificación."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la creación."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "No se puede cambiar el registro activo al primero (recordset en modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            ElseIf cnSet.Tables(0).Rows.Count = 0 Or cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                MensajeError = "El recorsdset no contiene ningún registro"
                    Throw New ArgumentException(MensajeError)
                Else
                cnPuntero = 1
                m_EOF = False 'NUEVO
                m_BOF = False 'NUEVO
                MoveFirst = True
            End If
        Catch ex As Exception
            Throw New ArgumentException("No es posible camiar el registro actual (procedimiento 'MoveFirst') en el recordset." + vbNewLine + ex.Message)
        End Try

    End Function

    Public Function MoveNext() As Boolean
        Dim MensajeError As String = ""

        MoveNext = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la modificación."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la creación."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "No se puede cambiar el registro activo al siguiente sin ejecutar el procedimiento Read (recordset en modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            ElseIf cnSet.Tables(0).Rows.Count = 0 Or cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                MensajeError = "El recorsdset no contiene ningún registro"
                Throw New ArgumentException(MensajeError)
            ElseIf EOF = True Then 'NUEVO
                MensajeError = "El recorsdset no contiene registro siguiente (EOF)" 'NUEVO
                Throw New ArgumentException(MensajeError) 'NUEVO
            Else
                If cnPuntero > cnSet.Tables(0).Rows.Count Then 'NUEVO
                    MensajeError = "El recorsdset ya no acepta movimiento a 'siguiente' (EOF)" 'NUEVO
                    Throw New ArgumentException(MensajeError) 'NUEVO

                ElseIf cnPuntero = cnSet.Tables(0).Rows.Count Then 'NUEVO
                    m_EOF = True 'NUEVO
                Else
                    cnPuntero = cnPuntero + 1
                    m_EOF = False 'NUEVO
                    m_BOF = False 'NUEVO
                    MoveNext = True
                End If
            End If
        Catch ex As Exception
            Throw New ArgumentException("No es posible camiar el registro actual (procedimiento 'MoveNext') en el recordset." + vbNewLine + ex.Message)
        End Try
    End Function

    Public Function MovePrevious() As Boolean
        Dim MensajeError As String = ""

        MovePrevious = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la modificación."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la creación."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "No se puede cambiar el registro activo al anterior (recordset en modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            ElseIf cnSet.Tables(0).Rows.Count = 0 Or cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                MensajeError = "El recorsdset no contiene ningún registro"
                Throw New ArgumentException(MensajeError)
            Else
                If cnPuntero < 1 Then
                    MensajeError = "El recorsdset ya no acepta movimiento a 'anterior' (BOF)" 'NUEVO
                ElseIf cnPuntero < 1 Then
                    m_BOF = True 'NUEVO
                Else
                    cnPuntero = cnPuntero - 1
                    m_BOF = False 'NUEVO
                    m_EOF = False 'NUEVO
                    MovePrevious = True
                End If
            End If
        Catch ex As Exception
            Throw New ArgumentException("No es posible camiar el registro actual (procedimiento 'MovePrevious') en el recordset." + vbNewLine + ex.Message)
        End Try
    End Function

    Public Function MoveLast() As Boolean
        Dim MensajeError As String = ""

        MoveLast = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la modificación."
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                MensajeError = "No se puede cambiar el registro activo sin actualizar o cancelar la creación."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "No se puede cambiar el registro activo al último (recordset en modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            ElseIf cnSet.Tables(0).Rows.Count = 0 Or cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                MensajeError = "El recorsdset no contiene ningún registro"
                Throw New ArgumentException(MensajeError)
            Else
                cnPuntero = cnSet.Tables(0).Rows.Count
                m_BOF = False 'NUEVO
                m_EOF = False 'NUEVO
                MoveLast = True
            End If
        Catch ex As Exception
            Throw New ArgumentException("No es posible camiar el registro actual (procedimiento 'MoveLast') en el recordset." + vbNewLine + ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Dim MensajeError As String = "", i As Integer

        Update = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura Then
                MensajeError = " No se puede actualizar un recordset de 'solo-lectura'"
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "No se puede actualizar un recordset de solo-lectura (modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            Else
                If cnSituacion = Situacion_cnRecordset.EnModificacion Or cnSituacion = Situacion_cnRecordset.EnCreacion Then
                    If cnSituacion = Situacion_cnRecordset.EnModificacion Then
                        Try
                            cnAdapter.Update(cnSet, cnSet.Tables(0).TableName)
                            For i = 0 To cnSet.Tables(0).Columns.Count - 1 : m_ValoresAnteriores(i) = Nothing : Next
                            cnSituacion = Situacion_cnRecordset.Abierto
                            Update = True
                        Catch ex2 As Exception
                            MensajeError = "Error al grabar el registro modificado. " & vbNewLine & ex2.Message
                            Throw New ArgumentException(MensajeError)
                        End Try
                    Else
                        Try
                            cnSet.Tables(0).Rows.Add(cnNuevaFila)
                            cnAdapter.Update(cnSet, cnSet.Tables(0).TableName)
                            cnSituacion = Situacion_cnRecordset.Abierto
                            cnPuntero = cnSet.Tables(0).Rows.Count
                            Update = True
                        Catch ex2 As Exception
                            MensajeError = "Error al grabar el registro creado. " & vbNewLine & ex2.Message
                            Throw New ArgumentException(MensajeError)
                        End Try
                    End If
                    If m_Buffer > 0 And m_Buffer <= cnSet.Tables(0).Rows.Count Then
                        Try
                            Close()
                            Open(m_CadenaSQL, cnConexion, True, False, m_Buffer)
                        Catch ex2 As Exception
                            Update = False
                            MensajeError = "Error al reabrir el recordset (Tamaño de buffer alcanzado)" & vbNewLine & ex2.Message
                            Throw New ArgumentException(MensajeError)
                        End Try
                    End If
                Else
                    MensajeError = "El recordset no está en estado 'modificación' o 'creación'"
                    Throw New ArgumentException(MensajeError)
                End If
            End If
        Catch ex As Exception
            MensajeError = "Error al actualizar la Base de datos a través del recordset. " & vbNewLine & ex.Message
            Throw New ArgumentException(MensajeError)
        End Try

    End Function

    Public Function Delete() As Boolean
        Dim MensajeError As String = ""

        Delete = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura Then
                MensajeError = " No se puede actualizar un recordset de 'solo-lectura'"
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "No se puede actualizar un recordset de solo-lectura (modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            Else
                If cnSet.Tables(0).Rows.Count = 0 Or cnSituacion = Situacion_cnRecordset.AbiertoYVacio Then
                    MensajeError = "No existen registros en el recordset actual"
                    Throw New ArgumentException(MensajeError)
                ElseIf cnPuntero = 0 Or EOF = True Or BOF = True Then 'NUEVO
                    MensajeError = "No existe registro actual pra eliminar"
                    Throw New ArgumentException(MensajeError)
                ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                    MensajeError = "El recordset está modificando un registro. Debe aceptar o cancelar la modificación antes de eliminar cualquier registro."
                    Throw New ArgumentException(MensajeError)
                ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                    MensajeError = "El recordset está creando un registro. Debe aceptar o cancelar la creación antes de eliminar cualquier registro."
                    Throw New ArgumentException(MensajeError)
                Else
                    Try
                        cnSet.Tables(0).Rows(cnPuntero - 1).Delete()
                        cnAdapter.Update(cnSet, cnSet.Tables(0).TableName)
                    Catch ex2 As Exception
                        MensajeError = "Error al ejecutar el comando 'delete' sobre la BD." + vbNewLine + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    End Try
                    Try
                        If cnSet.Tables(0).Rows.Count = 0 Then
                            cnSituacion = Situacion_cnRecordset.AbiertoYVacio
                            cnPuntero = 0
                        Else
                            cnSituacion = Situacion_cnRecordset.Abierto
                            If cnPuntero > cnSet.Tables(0).Rows.Count Then
                                cnPuntero = cnSet.Tables(0).Rows.Count
                            End If
                        End If
                    Catch ex2 As Exception
                        MensajeError = "Error al actualizar la situacion del recordset, después de eliminar registro." + vbNewLine + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    End Try
                End If
                Delete = True
            End If
        Catch ex As Exception
            Throw New ArgumentException("Error al invocar el procedimiento 'Delete' " + vbNewLine + ex.Message)
        End Try

    End Function

    Public Function AddNew() As Boolean
        Dim MensajeError As String = ""

        AddNew = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura Then
                MensajeError = " No se puede generar una nueva fila en un recordset de 'solo-lectura'"
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = " No se puede generar una nueva fila en un recordset de 'solo-lectura' (modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            Else
                If cnSituacion = Situacion_cnRecordset.EnCreacion Then
                    MensajeError = "No se puede crear una nueva fila sin grabar o eliminar la fila que se está creando."
                    Throw New ArgumentException(MensajeError)
                ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                    MensajeError = "No se puede crear una nueva fila sin grabar o cancelar modificaciones en la fila que se está modificando."
                    Throw New ArgumentException(MensajeError)
                Else
                    Try
                        cnNuevaFila = cnSet.Tables(0).NewRow()
                        cnSituacion = Situacion_cnRecordset.EnCreacion
                        m_BOF = False
                        m_EOF = False
                        AddNew = True
                    Catch ex2 As Exception
                        MensajeError = "Error en el método NewRow" + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                        Throw New ArgumentException(MensajeError)
                    End Try
                End If
            End If
        Catch ex As Exception
            Throw New ArgumentException("No es posible generar una nueva fila en recordset." + vbNewLine + ex.Message)
        End Try
    End Function

    Public Function CancelUpdate() As Boolean
        Dim MensajeError As String = ""

        CancelUpdate = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Cliente_SoloLectura Then
                MensajeError = " No se puede actualizar un recordset de 'solo-lectura'"
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset = Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = " No se puede actualizar recordset de 'solo-lectura' (modo 'servidor')"
                Throw New ArgumentException(MensajeError)
            ElseIf cnSituacion = Situacion_cnRecordset.EnCreacion Then
                Try
                    cnNuevaFila = Nothing
                    If cnSet.Tables(0).Rows.Count = 0 Then
                        cnSituacion = Situacion_cnRecordset.AbiertoYVacio
                    Else
                        cnSituacion = Situacion_cnRecordset.Abierto

                    End If
                Catch ex2 As Exception
                    MensajeError = "Error en el método 'CancelUpdate' al cancelar creación de registro." + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try
            ElseIf cnSituacion = Situacion_cnRecordset.EnModificacion Then
                Try
                    For i = 0 To cnSet.Tables(0).Columns.Count - 1
                        cnSet.Tables(0).Rows(cnPuntero - 1).Item(i) = m_ValoresAnteriores(i)
                    Next
                    For i = 0 To cnSet.Tables(0).Columns.Count - 1 : m_ValoresAnteriores(i) = Nothing : Next
                    If cnSet.Tables(0).Rows.Count = 0 Then
                        cnSituacion = Situacion_cnRecordset.AbiertoYVacio
                        m_BOF = True 'NUEVO
                        m_EOF = True 'NUEVO
                    Else
                        cnSituacion = Situacion_cnRecordset.Abierto
                        m_BOF = False 'NUEVO
                        m_EOF = False 'NUEVO

                    End If
                Catch ex2 As Exception
                    MensajeError = "Error al cancelar modificación de registro." + vbNewLine + "Objeto:" + ex2.Source + vbNewLine + "Mensaje:" + ex2.Message
                    Throw New ArgumentException(MensajeError)
                End Try
            Else
                MensajeError = " No hay ningún registro en creación/modificación"
                Throw New ArgumentException(MensajeError)
            End If
        Catch ex As Exception
            Throw New ArgumentException("Error al invocar el procedimiento 'CancelUpdate' " + vbNewLine + ex.Message)
        End Try
    End Function

    Public Function Read() As Boolean
        Dim MensajeError As String = ""

        Read = False
        Try
            If cnSituacion = Situacion_cnRecordset.Cerrado Then
                MensajeError = "El recordset está cerrado."
                Throw New ArgumentException(MensajeError)
            ElseIf m_TipoRecordset <> Tipo_de_cnRecordset.Servidor_SoloLectura Then
                MensajeError = "El procedimiento solo se puede utilizar en los recordsets de modo 'servidor'"
                Throw New ArgumentException(MensajeError)
            Else
                If cnDataReader.Read() = True Then
                    cnPuntero = cnPuntero + 1
                    m_BOF = False 'NUEVO
                    m_EOF = False 'NUEVO
                    Read = True
                Else 'NUEVO
                    m_EOF = True 'NUEVO
                End If
            End If
        Catch ex As Exception
            Read = False
            Throw New ArgumentException("Error al invocar el procedimiento 'Read' " + vbNewLine + ex.Message)
        End Try
    End Function


End Class





