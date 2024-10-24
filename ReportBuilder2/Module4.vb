'Private Sub pDoc_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pDoc.PrintPage

'    e.Graphics.DrawString("Esto es la primera linea", New System.Drawing.Font("Arial", 10, FontStyle.Regular), Brushes.Black, 100, 50)
'    e.HasMorePages = True
'    If NumeroPagina = 1 Then
'        If MsgBox("Imprimir la segunda linea", MsgBoxStyle.YesNo) =
'        MsgBoxResult.No Then
'            NumeroPagina += 1
'            Exit Sub
'        End If
'    End If
'    e.Graphics.DrawString("Esto es la segunda linea",
'    New System.Drawing.Font("Arial", 10, FontStyle.Regular),
'    Brushes.Black, 100, 100)

'    If NumeroPagina = 2 Then e.HasMorePages = False
'    NumeroPagina += 1
'End Sub
'Dim fuente As System.Drawing.Font = New Font("consolas", 15)
'Dim topMargin As Double = e.MarginBounds.Top
'Dim yPos As Double = 0
'Dim count As Integer = 0
'Dim texto As String = ""
'Dim unidad As Byte = 0
'Dim detalle As String = ""
'Dim valor As Decimal = 0
'Dim tabulacion As String = ""
'Dim compensador As Integer = 0
'Dim total As Decimal = 0


'Dim ds As New DataSet
'Dim dt As New DataTable
''  Dim dr As DataRow

'Dim campito As String


'campito = "id"
'Dim strsql As String = "select id,impresora,representante,establecimiento,direccion,telefono,nit,mensaje,regimen,resolucion from mi_factura where " & campito & " like '" & "" & "%" & "'"

'Dim adp As New MySqlDataAdapter(strsql, conn)

'ds.Tables.Add("mi_factura")
'adp.Fill(ds.Tables("mi_factura"))

'Dim establecimiento As String
'Dim direccion As String
'Dim telefono As String
'Dim nit As String
'Dim mensaje As String
'Dim impresora As String



'establecimiento = ds.Tables("mi_factura").Rows(0).Item("establecimiento").ToString
'direccion = ds.Tables("mi_factura").Rows(0).Item("direccion").ToString
'telefono = ds.Tables("mi_factura").Rows(0).Item("telefono").ToString
'nit = ds.Tables("mi_factura").Rows(0).Item("nit").ToString
'mensaje = ds.Tables("mi_factura").Rows(0).Item("mensaje").ToString
'impresora = ds.Tables("mi_factura").Rows(0).Item("impresora").ToString()
'representante = ds.Tables("mi_factura").Rows(0).Item("representante").ToString()


'' Imprime la cabecera
'yPos = 40
'Dim printFont As System.Drawing.Font = New Font("consolas", 12)
'e.Graphics.DrawString(establecimiento, fuente, Brushes.Black, 80, 40)
'e.Graphics.DrawString(direccion, printFont, Brushes.Black, 80, 60)
'e.Graphics.DrawString(nit, printFont, Brushes.Black, 80, 80)
'''  e.Graphics.DrawString("Tiket N. " & Me.Numerotiket.Text, printFont, Brushes.Black, 10, 100)
'e.Graphics.DrawString("Fecha: " & Date.Now, printFont, Brushes.Black, 10, 120)
''' e.Graphics.DrawString("Mesa N. " & NumeroMesa.Text, printFont, Brushes.Black, 10, 160)
'e.Graphics.DrawString("****************************", printFont, Brushes.Black, 10, 180)

'Dim cant As String
'Dim desc As String
'Dim imp As String
'Dim i As Integer

'Dim dst As New DataSet
'Dim dtp As New DataTable
'Dim camp As String

'camp = "mesa"

''Dim adpt As New MySqlDataAdapter("select * from  tikets", conn)

'Dim adpT As New MySqlDataAdapter("select * from pedidofin where " & camp & " like '" & VENTAS.ComboBox2.Text & "'", conn)

'dst.Tables.Add("pedidofin")
'adpT.Fill(dst.Tables("pedidofin"))


'''''''''''''''''''''''
'e.Graphics.DrawString("CNT       ITEM          IMPO ", printFont, System.Drawing.Brushes.Black, 10, 200)
'For i = 1 To adpT.Fill(dst.Tables("pedidofin"))
'Cant = dst.Tables("pedidofin").Rows(i).Item("cnt").ToString
'desc = dst.Tables("pedidofin").Rows(i).Item("item").ToString
'imp = dst.Tables("pedidofin").Rows(i).Item("precio").ToString
'' Cant = Cant + "  "

'texto = cant & "  " & desc & "  " & imp
'yPos = 120 + topMargin + (count * printFont.GetHeight(e.Graphics)) ' Calcula la posición en la que se escribe la línea
'' Imprime la línea con el objeto Graphics

'e.Graphics.DrawString(texto, printFont, System.Drawing.Brushes.Black, 10, yPos)
'total += valor

'count += 1

'Next



'yPos += 10
'e.Graphics.DrawString("____________________________", printFont, System.Drawing.Brushes.Black, 10, yPos)
'Dim tot As Integer = 0
'tot = Len(TextBox1.Text)
''' lineaTotal = StrDup(28 - XXX, ".")
'yPos += 20
'e.Graphics.DrawString("Total " & tot & total, printFont, System.Drawing.Brushes.Black, 10, yPos)
'yPos += 30
'e.Graphics.DrawString("****************************", printFont, System.Drawing.Brushes.Black, 10, yPos)
'yPos += 30
'e.Graphics.DrawString("gracias por su compra", printFont, System.Drawing.Brushes.Black, 10, yPos)
''' e.Graphics.DrawString(Me.Preferenciabindingdource.Current("Coletilla"), printFont, Brushes.Black, 10, yPos + 20)
'''e.Graphics.DrawString("---------------------------------", printFont, Brushes.Black, 10, 180)
''' e.Graphics.DrawString("Gracias por su compra!", printFont, Brushes.Black, 10, 180)
'e.HasMorePages = False