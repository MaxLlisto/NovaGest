'//using iTextSharp.text;
'//using iTextSharp.text.pdf;
'//using iTextSharp.text.pdf.security;
'//using iTextSharp.text.pdf.parser;
'//using System.IO;
'//using System.Text;

'//public class Signature
'//{
'//    public void SignPdf(string sourcePathPdf, string targetPathPdf, string certificatePath, string password)
'//    {
'//        Certificate certificado = New (certificatePath, password);

'//        int nsejeX = 400;
'//        int nsejeY = 30;

'//        using var reader = New iTextSharp.text.pdfPdfReader(sourcePathPdf);
'//        using var writer = New iTextSharp.text.pdf.FileStream(targetPathPdf, FileMode.Create, FileAccess.Write);
'//        using var stamper = PdfStamper.CreateSignature(reader, writer, '\0', null, true);
'//        stamper.SignatureAppearance.SetVisibleSignature(New Rectangle(nsejeX, nsejeY, nsejeX + 150, nsejeY + 50), 1, "Signature");

'//        var signature = stamper.SignatureAppearance;
'//        signature.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED;

'//        var signatureKey = New PrivateKeySignature(certificado.Key, DigestAlgorithms.SHA256);
'//        var signatureChain = certificado.Chain;
'//        var standard = CryptoStandard.CADES;

'//        MakeSignature.SignDetached(signature, signatureKey, signatureChain, null, null, null, 0, standard);
'//    }
'//}
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Pkcs
Imports Org.BouncyCastle.X509
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.security
Imports iTextSharp.text.pdf.parser
Imports System.IO
Imports System.Text

Public Class Signature
    Public Sub SignPdf(ByVal sourcePathPdf As String, ByVal targetPathPdf As String, ByVal certificatePath As String, ByVal password As String)
        Dim certificado As New Certificate(certificatePath, password)
        Dim nsejeX As Integer = 400
        Dim nsejeY As Integer = 30

        Using reader As New PdfReader(sourcePathPdf)
            Using writer As New FileStream(targetPathPdf, FileMode.Create, FileAccess.Write)
                Using stamper As PdfStamper = PdfStamper.CreateSignature(reader, writer, CChar(vbNullChar), Nothing, True)
                    stamper.SignatureAppearance.SetVisibleSignature(New Rectangle(nsejeX, nsejeY, nsejeX + 150, nsejeY + 50), 1, "Signature")

                    Dim signature As PdfSignatureAppearance = stamper.SignatureAppearance
                    signature.CertificationLevel = PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED

                    Dim signatureKey As New PrivateKeySignature(certificado.Key, DigestAlgorithms.SHA256)
                    Dim signatureChain As X509Certificate() = certificado.Chain
                    Dim standard As CryptoStandard = CryptoStandard.CADES

                    MakeSignature.SignDetached(signature, signatureKey, signatureChain, Nothing, Nothing, Nothing, 0, standard)
                End Using
            End Using
        End Using
    End Sub
End Class

