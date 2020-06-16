using CommandLine;
using System;

namespace CmsCheck
{
    class Program
    {
        public class Options
        {
            [Value(0, Required = true, HelpText = "Source path to CMS file. Supports PEM and DER formats")]
            public string Source { get; set; }

            [Option('S', HelpText = "Verify signature only", Default = false)]
            public bool VerifySignatureOnly { get; set; }
        }


        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var cms = new CmsVerifier();
                    cms.Policy.VerifySignatureOnly = o.VerifySignatureOnly;
                    cms.Load(o.Source);
                    cms.Verify(new System.Security.Cryptography.X509Certificates.X509Certificate2Collection());
                });
        }
    }
}
