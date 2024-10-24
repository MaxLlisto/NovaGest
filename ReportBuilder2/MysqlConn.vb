'
' Created by SharpDevelop.
' User: Louis
' Date: 1/14/2017
' Time: 10:34 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports MySql.Data.MySqlClient
imports System.Data
Public Class MySqlClass	
	Sub New(ByVal DT As datatable, ByVal sql As String, ByVal connstr As String)		
		Try
			Dim MysqlConn As MySqlConnection
			MysqlConn = New MySqlConnection(connstr)
			MysqlConn.Open()
			Dim command As New MySqlCommand(sql, MysqlConn)
			Dim Adapter As New MySqlDataAdapter(command)
			DT.Rows.Clear
			Adapter.Fill(DT)			
			MysqlConn.Close()
			MysqlConn.Dispose()
		Catch ex As Exception
            MessageBox.Show("No puedo conectar con la base de datos: " & ex.Message, "No puedo conectar con la base de datos SQL")
        End Try
	End Sub
End Class
