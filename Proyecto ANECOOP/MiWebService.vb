Imports System.Net
Imports System
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography

    Class MyWebClient
        Inherits WebClient

        Protected Overrides Function GetWebRequest(ByVal address As Uri) As WebRequest
            Dim request As HttpWebRequest = TryCast(MyBase.GetWebRequest(address), HttpWebRequest)
            request.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
            Return request
        End Function

    End Class


