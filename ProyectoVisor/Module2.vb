Option Compare Text

Imports System.Runtime.CompilerServices

Public Module DataGridViewExtensions

#Region "Find"
    ''' <summary>
    ''' La función devolverá una lista de objetos DataGridViewRow
    ''' que satisfagan el criterio de búsqueda especificado.
    ''' </summary>
    ''' <param name="dgv">Control DataGridView.</param>
    ''' <param name="fieldName">Nombre de la columna por donde se desea buscar.</param>
    ''' <param name="criterio">Valor con el criterio de búsqueda deseado.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Find(
        ByVal dgv As DataGridView,
        ByVal fieldName As String,
        ByVal criterio As String) As List(Of DataGridViewRow)

        ' Comprobamos los valores de los parámetros.
        '
        If (fieldName = String.Empty) OrElse
            (criterio = String.Empty) Then Return Nothing

        ' Para que funcione adecuadamente el operador Like, hay que establecer
        ' Option Compare Text a nivel del módulo donde aparezca la función,
        ' o a nivel del proyecto.
        '
        Try
            Dim query As IEnumerable(Of DataGridViewRow) =
                From item As DataGridViewRow In dgv.Rows.Cast(Of DataGridViewRow)()
                Where ((item.Cells(fieldName).Value IsNot DBNull.Value) AndAlso
                      (CStr(item.Cells(fieldName).Value) Like criterio))
                Select item

            ' Devolvemos la consulta LINQ ejecutada.
            Return query.ToList()

        Catch ex As Exception
            Return Nothing

        End Try

    End Function

    ''' <summary>
    ''' Hace que el control DataGridView se desplace a la posición indicada
    ''' dentro del conjunto de filas que satisfacen el criterio de búsqueda
    ''' especificado en el método Find.
    ''' </summary>
    ''' <param name="dgv">Control DataGridView.</param>
    ''' <param name="position">Índice de la posición a la que desea desplazarse.</param>
    ''' <param name="listRows">Lista de objetos DataGridViewRow.</param>
    ''' <remarks></remarks>
    <Extension()>
    Public Sub Find(ByVal dgv As DataGridView, ByVal position As Int32,
                    ByVal listRows As List(Of DataGridViewRow))

        If (listRows Is Nothing) Then _
            Throw New InvalidOperationException(
                "Operación no válida. " &
                "Establezca primero los criterios de búsqueda " &
                "mediante el método Find.")

        ' Seleccionamos el elemento correspondiente a la
        ' posición actual especificada.
        '
        Dim row As DataGridViewRow = listRows.ElementAtOrDefault(position)

        If ((row IsNot Nothing) AndAlso (row.Index <> -1)) Then
            ' Establecemos la celda actual si el índice de la fila
            ' no es igual a -1.
            '
            dgv.CurrentCell = dgv.Rows(row.Index).Cells(0)
            dgv.FirstDisplayedCell = dgv.CurrentCell

        End If

    End Sub

#End Region

End Module