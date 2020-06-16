using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CmsCheck
{
    public class CmsVerifier
    {

        public SignedCms SignedData { get; private set; }

        public CmsVerifierPolicy Policy { get; set; } = new CmsVerifierPolicy();

        /// <summary>
        /// Loads CMS file
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="FileNotFoundException"/>
        public void Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found", path);
            }

            var data = File.ReadAllBytes(path);

            try
            {
                var pem = Encoding.UTF8.GetString(data);
                data = PemConverter.Decode(pem)[0];
            }
            catch
            {
                // nothing
            }

            SignedData = new SignedCms();
            SignedData.Decode(data);
        }

        public void Verify(X509Certificate2Collection extraCerts)
        {
            SignedData.CheckSignature(Policy.VerifySignatureOnly);
        }

    }
}
