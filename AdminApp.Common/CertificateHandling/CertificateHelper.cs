using System;
using System.Security.Cryptography.X509Certificates;

namespace AdminApp.Common.CertificateHandling
{
    public static class CertificateHelper
    {
        private static X509Certificate2 _gatewayAttorneyServiceCertificate;
        public static X509Certificate2 GatewayAttorneyServiceCertificate(string thumbprint)
        {
            if (_gatewayAttorneyServiceCertificate == null)
            {
                try
                {
                    var gatewayCertificateDetails = new CertificateDetails { Thumbprint = thumbprint };
                    _gatewayAttorneyServiceCertificate = GetCertificate(gatewayCertificateDetails);
                }
                catch
                {
                    throw; //TODO: add proper error handling
                }
            }
            return _gatewayAttorneyServiceCertificate;
        }

        private static X509Certificate2 GetCertificate(CertificateDetails certificateDetails)
        {
            using (X509Store certStore = new X509Store(certificateDetails.StoreName, certificateDetails.StoreLocation))
            {
                try
                {
                    certStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    var certificates = certStore.Certificates.Find(X509FindType.FindByThumbprint, certificateDetails.Thumbprint, true);
                    if (certificates.Count == 0)
                    {
                        throw new InvalidOperationException($"Certificate Details Thumbprint:{certificateDetails.Thumbprint}, Store:{certificateDetails.StoreName}, Location:{certificateDetails.StoreLocation} - not found");
                    }

                    return certificates[0]; //TODO: should check if the private key is accessible?
                }
                catch
                {
                    throw; //TODO: add proper error handling
                }
            }
        }
    }

    public class CertificateDetails
    {
        public StoreName StoreName { get; set; } = StoreName.My;
        public StoreLocation StoreLocation { get; set; } = StoreLocation.LocalMachine;
        public string Thumbprint { get; set; }
    }
}
