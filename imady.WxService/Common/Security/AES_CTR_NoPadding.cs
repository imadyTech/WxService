﻿using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace imady.Common
{
    public class AES_CTR_NoPadding
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
            return cipher.DoFinal(data);
        }

        public static string Encrypt(string data, string key, byte[] iv)
        {
            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", Encoding.UTF8.GetBytes(key)), iv));
            return Convert.ToBase64String(cipher.DoFinal(Encoding.UTF8.GetBytes(data)));
        }

        public static string Decrypt(string data, string key, byte[] iv)
        {
            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
            cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", Encoding.UTF8.GetBytes(key)), iv));
            return Encoding.UTF8.GetString(cipher.DoFinal(Convert.FromBase64String(data)));
        }
    }
}
