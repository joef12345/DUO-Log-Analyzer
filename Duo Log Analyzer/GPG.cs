using Org.BouncyCastle.Bcpg.OpenPgp;
using System.IO;

namespace Duo_Log_Analyzer
{
    internal class GPG
    {

        //For future auto updating...
        public static bool VerifyFileSignature(string filePath, string publicKeyFilePath)
        {
            using (Stream fileStream = File.OpenRead(filePath))
            using (Stream publicKeyStream = File.OpenRead(publicKeyFilePath))
            {
                PgpPublicKeyRingBundle publicKeyRingBundle = new PgpPublicKeyRingBundle(PgpUtilities.GetDecoderStream(publicKeyStream));
                PgpObjectFactory pgpFact = new PgpObjectFactory(PgpUtilities.GetDecoderStream(fileStream));
                PgpSignatureList signatureList = (PgpSignatureList)pgpFact.NextPgpObject();

                if (signatureList == null)
                {
                    throw new PgpException("File does not contain a signature");
                }

                PgpPublicKey key = publicKeyRingBundle.GetPublicKey(signatureList[0].KeyId);
                signatureList[0].InitVerify(key);

                var signedData = (PgpLiteralData)pgpFact.NextPgpObject();

                using (var dataStream = signedData.GetInputStream())
                {
                    int ch;
                    while ((ch = dataStream.ReadByte()) >= 0)
                    {
                        signatureList[0].Update((byte)ch);
                    }
                }

                if (signatureList[0].Verify())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
