Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.Text
Imports System.Security.Cryptography

Public Class MyHmac
    Public Function CreateToken(ByVal message As String, ByVal secret As String) As String
        secret = If(secret, "")
        Dim encoding = New System.Text.ASCIIEncoding()
        Dim keyByte As Byte() = encoding.GetBytes(secret)
        Dim messageBytes As Byte() = encoding.GetBytes(message)

        Using hmacsha256 = New HMACSHA256(keyByte)
            Dim hashmessage As Byte() = hmacsha256.ComputeHash(messageBytes)
            Return Convert.ToBase64String(hashmessage)
        End Using
    End Function
End Class
