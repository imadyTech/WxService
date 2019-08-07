using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Meiyu.Domain.Weixin;
using Meiyu.Common.Utilities;

namespace Meiyu.WxService
{
    public class WeixinClient : IWeixinClient
    {
        private const string appid = "appid";
        private const string mch_id = "mch_id";
        private const string mch_appid = "mch_appid";
        private const string mchid = "mchid";
        private const string wxappid = "wxappid";
        private const string sign_type = "sign_type";
        private const string nonce_str = "nonce_str";
        private const string sign = "sign";
        private const string enc_bank_no = "enc_bank_no";
        private const string enc_true_name = "enc_true_name";
        private const string partnerid = "partnerid";
        private const string noncestr = "noncestr";
        private const string timestamp = "timestamp";
        private const string appId = "appId";
        private const string timeStamp = "timeStamp";
        private const string nonceStr = "nonceStr";
        private const string signType = "signType";
        private const string paySign = "paySign";

        private readonly AsymmetricKeyParameter PublicKey;

        public WxOption Options { get; set; }

        public virtual ILogger Logger { get; set; }

        protected internal HttpClientEx Client { get; set; }

        protected internal HttpClientEx CertificateClient { get; set; }

        #region WeChatPayClient Constructors

        public WeixinClient(
            IOptions<WxOption> optionsAccessor,
            ILogger<WeixinClient> logger)
        {
            Options = optionsAccessor.Value;
            Logger = logger;
            Client = new HttpClientEx();
        
            if (string.IsNullOrEmpty(Options.WxName))
            {
                throw new ArgumentNullException(nameof(Options.WxName));
            }
            
            if (string.IsNullOrEmpty(Options.AppId))
            {
                throw new ArgumentNullException(nameof(Options.AppId));
            }


            if (string.IsNullOrEmpty(Options.AppSecret))
            {
                throw new ArgumentNullException(nameof(Options.AppSecret));
            }

            if (string.IsNullOrEmpty(Options.Token))
            {
                throw new ArgumentNullException(nameof(Options.Token));
            }

            if (string.IsNullOrEmpty(Options.EncodingAESKey))
            {
                throw new ArgumentNullException(nameof(Options.EncodingAESKey));
            }
        }

        public WeixinClient(IOptions<WxOption> optionsAccessor)
            : this(optionsAccessor, null)
        { }

        #endregion

        #region IWeChatPayClient Members

        public void SetTimeout(int timeout)
        {
            Client.Timeout = new TimeSpan(0, 0, 0, timeout);

            if (CertificateClient != null)
            {
                CertificateClient.Timeout = new TimeSpan(0, 0, 0, timeout);
            }
        }

        #endregion


    }

}
