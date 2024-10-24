Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports System.ComponentModel

Public Class MiDatatable
    Inherits DataTable

    Public Function Filter(ByVal sCad As String, ByVal cOrden As String, ParamArray columnNames() As String) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.Sort = cOrden
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable(False, columnNames)
        Return dtNew

    End Function

    Public Function Filter(ByVal sCad As String, ByVal cOrden As String) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.Sort = cOrden
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable()
        Return dtNew

    End Function

    Public Function Filter(ByVal sCad As String, ByVal cOrden As String, ByVal distinct As Boolean) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.Sort = cOrden
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable(distinct)
        Return dtNew

    End Function

    Public Function Filter(ByVal sCad As String, ByVal distinct As Boolean) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable(distinct)
        Return dtNew

    End Function

    Public Function Filter(ByVal sCad As String, ByVal cOrden As String, ByVal distinct As Boolean, ParamArray columnNames() As String) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.Sort = cOrden
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable(distinct, columnNames)
        Return dtNew
    End Function

    Public Function Filter(ByVal sCad As String, ByVal distinct As Boolean, ParamArray columnNames() As String) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable(distinct, columnNames)
        Return dtNew
    End Function


    Public Function Filter(ByVal sCad As String, ParamArray columnNames() As String) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable()
        Return dtNew
    End Function

    Public Function Filter(ByVal sCad As String) As DataTable
        Dim dtNew As DataTable = Me.Clone
        Dim dv As DataView = New DataView(dtNew)
        dv.RowFilter = String.Format(sCad)
        dtNew = dv.ToTable()
        Return dtNew
    End Function

End Class
