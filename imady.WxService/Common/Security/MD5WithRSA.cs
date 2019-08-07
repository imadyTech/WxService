﻿using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace imady.Common
{
    public class MD5WithRSA
    {
        public static string SignData(string data, AsymmetricKeyParameter key)
        {
            var signer = SignerUtilities.GetSigner("MD5WithRSA");
            signer.Init(true, key);
            var bytes = Encoding.UTF8.GetBytes(data);
            signer.BlockUpdate(bytes, 0, bytes.Length);
            return Convert.ToBase64String(signer.GenerateSignature());
        }

        public static bool VerifyData(string data, string sign, AsymmetricKeyParameter key)
        {
            var verifier = SignerUtilities.GetSigner("MD5WithRSA");
            verifier.Init(false, key);
            var bytes = Encoding.UTF8.GetBytes(data);
            verifier.BlockUpdate(bytes, 0, bytes.Length);
            return verifier.VerifySignature(Convert.FromBase64String(sign));
        }
    }
}
