using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System.IO;
using System.Linq;
using static System.Net.WebRequestMethods;

public class Certificate
{
    public X509Certificate[] Chain { get; private set; }
    public AsymmetricKeyParameter Key { get; private set; }

    public Certificate(string certificate, string password)
    {
        using var file = File.OpenRead(certificate);
        Pkcs12Store store = new (File, password.ToCharArray());
        string alias = GetCertificateAlias(store);

        Key = store.GetKey(alias).Key;
        Chain = store.GetCertificateChain(alias).Select(x => x.Certificate).ToArray();
    }

    private string GetCertificateAlias(Pkcs12Store store)
    {
        foreach (string currentAlias in store.Aliases)
        {
            if (store.IsKeyEntry(currentAlias))
            {
                return currentAlias;
            }
        }

        throw new InvalidOperationException("No se encontró una entrada de clave en el almacén.");
    }
}

