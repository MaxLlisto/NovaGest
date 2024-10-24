'
' Created by SharpDevelop.
' User: Louis
' Date: 1/14/2017
' Time: 3:08 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Data
Imports System.Data.Odbc

Public Class ODBCConn	
	Sub New(ByVal DT As datatable, ByVal sql As String, ByVal connstr As String)		
		Try
			Dim MysqlConn As OdbcConnection
			MysqlConn = New OdbcConnection(connstr)
			MysqlConn.Open()
			Dim command As New OdbcCommand(sql, MysqlConn)
			Dim Adapter As New OdbcDataAdapter(command)
			DT.Rows.Clear
			Adapter.Fill(DT)
			MysqlConn.Close()
			MysqlConn.Dispose()
		Catch ex As Exception
			MessageBox.Show("Cannot connect to database: " & ex.Message, "Cannot connect to OLEDb database")		
		End Try
	End Sub
End Class
