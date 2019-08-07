﻿using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace imady.Common
{
    public class RSA_ECB_OAEPWithSHA1AndMGF1Padding
    {
        public static byte[] Encrypt(byte[] data, AsymmetricKeyParameter key)
        {
            var cipher = CipherUtilities.GetCipher("RSA/ECB/OAEPWithSHA1AndMGF1Padding");
            cipher.Init(true, key);
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt(byte[] data, AsymmetricKeyParameter key)
        {
            var cipher = CipherUtilities.GetCipher("RSA/ECB/OAEPWithSHA1AndMGF1Padding");
            cipher.Init(false, key);
            return cipher.DoFinal(data);
        }

        public static string Encrypt(string data, AsymmetricKeyParameter key)
        {
            var cipher = CipherUtilities.GetCipher("RSA/ECB/OAEPWithSHA1AndMGF1Padding");
            cipher.Init(true, key);
            return Convert.ToBase64String(cipher.DoFinal(Encoding.UTF8.GetBytes(data)));
        }

        public static string Decrypt(string data, AsymmetricKeyParameter key)
        {
            var cipher = CipherUtilities.GetCipher("RSA/ECB/OAEPWithSHA1AndMGF1Padding");
            cipher.Init(false, key);
            return Encoding.UTF8.GetString(cipher.DoFinal(Convert.FromBase64String(data)));
        }
    }
}
