'Imports System.IO

'Public Function EscribirXML(fnombre, cXML, Optional cPath = "c:\sii") As Boolean
'    Dim escribir As StreamWriter
'    Dim fichero As String
'    Dim Contador As Integer
'    Dim Fic As String

'    Try
'        Fic = Replace(fnombre, "/", "") & ".xml"
'        fichero = cPath & "\" & Fic
'        'Fichero = Trim(Replace(Fichero, "-", "_"))
'        Contador = 0

'        'escribir = New StreamWriter(fichero)

'        While Trim(Dir(fichero)) = Trim(Fic)
'            Contador = Contador + 1
'            Fic = Replace(fnombre, "/", "") & "(" & Contador & ")" & ".xml"
'            fichero = cPath & "\" & Fic
'            'Fichero = Trim(Replace(Fichero, "-", "_"))
'        End While

'        escribir = File.AppendText(fichero)
'        escribir.Write(cXML)
'        escribir.Flush()
'        escribir.Close()
'        escribir = Nothing
'        Return True
'    Catch ex As Exception
'        Return False
'    End Try



'End Function
