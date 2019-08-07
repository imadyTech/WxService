﻿using System.Collections.Generic;
using System.Text;

namespace imady.Common
{
    public class WxPaySignature
    {
        public static string SignWithKey(SortedDictionary<string, string> parameters, string key, bool signType = true, bool excludeSignType = true)
        {
            var sb = new StringBuilder();
            foreach (var iter in parameters)
            {
                if (!string.IsNullOrEmpty(iter.Value) && iter.Key != "sign" && (excludeSignType ? iter.Key != "sign_type" : true))
                {
                    sb.Append(iter.Key).Append('=').Append(iter.Value).Append("&");
                }
            }
            var signContent = sb.Append("key=").Append(key).ToString();
            return signType ? MD5.Compute(signContent).ToUpper() : HMACSHA256.Compute(signContent, key).ToUpper();
        }

        public static string SignWithSecret(SortedDictionary<string, string> parameters, string secret, List<string> include)
        {
            var sb = new StringBuilder();
            foreach (var iter in parameters)
            {
                if (!string.IsNullOrEmpty(iter.Value) && include.Contains(iter.Key))
                {
                    sb.Append(iter.Key).Append('=').Append(iter.Value).Append("&");
                }
            }
            var signContent = sb.Append("secret=").Append(secret).ToString();
            return MD5.Compute(signContent).ToUpper();
        }
    }
}