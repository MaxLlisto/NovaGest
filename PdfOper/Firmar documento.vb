Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Pkcs
Imports Org.BouncyCastle.X509
Imports System.IO
Imports System.Security.Cryptography.X509Certificates.X509Certificate2
Imports System.Linq

Public Class Certificate
    Public Property Chain As X509Certificate()
    Public Property Key As AsymmetricKeyParameter

    Public Sub New(ByVal certificate As String, ByVal password As String)
        Using file As FileStream = System.IO.File.OpenRead(certificate)
            Dim store As New Pkcs12Store(file, password.ToCharArray())
            Dim al As String = GetCertificateAlias(store)
            Me.Key = store.GetKey(al).Key
            Me.Chain = store.GetCertificateChain(al).Select(Function(x) x.Certificate).ToArray()
        End Using
    End Sub

    Private Function GetCertificateAlias(ByVal store As Pkcs12Store) As String
        For Each currentAlias As String In store.Aliases
            If store.IsKeyEntry(currentAlias) Then
                Return currentAlias
            End If
        Next

        Throw New InvalidOperationException("No se encontró una entrada de clave en el almacén.")
    End Function
End Class

