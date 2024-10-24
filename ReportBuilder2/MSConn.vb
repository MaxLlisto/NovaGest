'
' Created by SharpDevelop.
' User: Louis
' Date: 1/14/2017
' Time: 11:32 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
'
'
Imports System.Data
imports System.Data.OleDb
Public Class MSConn	
	Sub New(ByVal DT As datatable, ByVal sql As String, ByVal connstr As String)		
		Try
			Dim MysqlConn As OleDbConnection
			MysqlConn = New OleDbConnection(connstr)
			MysqlConn.Open()
			Dim command As New OleDbCommand(sql, MysqlConn)
			Dim Adapter As New OleDbDataAdapter(command)
			DT.Rows.Clear
			Adapter.Fill(DT)
			MysqlConn.Close()
			MysqlConn.Dispose()
		Catch ex As Exception
            MessageBox.Show("No puedo conectar con la base de datos: " & ex.Message, "No puedo conectar con la base de datos")
        End Try
	End Sub
End Class
