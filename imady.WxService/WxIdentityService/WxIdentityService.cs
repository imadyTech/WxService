using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using imady.Repository;
using imady.Domain.Weixin;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace imady.WxService
{
    public class WxIdentityService : IWxIdentityService
    {
        private IWxApiRepository _apiRepository;
        private IOptions<WxOption> _wxOption;

        #region 构造函数
        public WxIdentityService(IOptions<WxOption> wxOption): this(new WxApiRepository(), wxOption){ }

        
        public WxIdentityService(IWxApiRepository apiRepository, IOptions<WxOption> wxOption)
        {
            _apiRepository = apiRepository;
            _wxOption = wxOption;
        }
        #endregion


        #region 微信身份基础功能
        public async Task<WxRsp_AccessToken> GetAccessToken()
        {
            //Todo: 应首先从ICacheRepository中获取，如果不存在或者已经超时则可以继续从微信服务器获取。
            //...

            var request = new WxReq_AccessToken()
            {
                grant_type = "client_credential",
                appid = _wxOption.Value.AppId,
                secret = _wxOption.Value.AppSecret
                //grant_type = "client_credential",
                //appid = "wxb5431f6637f4ab4d",
                //secret = "0e5304348170bd4c0851f135325a376d"

            };
            var response = await _apiRepository.GET<WxRsp_AccessToken, WxReq_AccessToken>(request);

            return response;

        }
        #endregion


        #region 微信网页授权 (https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842)
        /// <summary>
        /// 第一步：用户同意授权，获取code
        /// </summary>
        /// <returns></returns>
        public async Task<WxRsp_AuthorizeCode> GetAuthorizeCode(WxReq_AuthorizeCode request)
        {
            ///...
            request.appid = _wxOption.Value.AppId;

            var url = request.WxApiRequestBaseUrl + request.UrlParameter + "#wechat_redirect";

            //注意：由于授权操作安全等级较高，所以在发起授权请求时，微信会对授权链接做正则强匹配校验，如果链接的参数顺序不对，授权页面将无法正常访问
            var response = await _apiRepository.GET(url);
            return request.DeserializeResponse( response );
        }

        /// <summary>
        /// 第二步：通过code换取网页授权access_token
        /// </summary>
        /// <param name="redirectUrl"></param>
        public async Task<WxRsp_AuthorizeAccessToken> GetAuthorizeAccesstoken(WxReq_AuthorizeAccessToken request)
        {
            ///...
            
            var response = await _apiRepository.GET<WxRsp_AuthorizeAccessToken, WxReq_AuthorizeAccessToken>(request);
            return response;
        }


        /// <summary>
        /// 第三步：刷新access_token（如果需要）
        /// 注意：返回的消息采用WxRsp_AuthorizeAccessToken格式
        /// </summary>
        /// <param name="request"></param>
        public async Task<WxRsp_AuthorizeAccessToken> GetRefreshedAccessToken(WxReq_RefreshAccessToken request)
        {
            ///...
            
            var response = await _apiRepository.GET<WxRsp_AuthorizeAccessToken, WxReq_RefreshAccessToken>(request);
            return response;
        }


        /// <summary>
        /// 第四步：拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="request"></param>
        public async Task<WxRsp_WxUserInfo> GetWxUserInfo(WxReq_WxUserInfo request)
        {
            ///...

            var response = await _apiRepository.GET<WxRsp_WxUserInfo, WxReq_WxUserInfo>(request);
            return response;
        }


        /// <summary>
        /// 微信网页授权
        /// 附：检验授权凭证（access_token）是否有效
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842
        /// </summary>
        public async Task<WxRsp_ErrorCode> ValidateAccessToken(WxReq_ValidateAccessToken request)
        {
            ///...

            var response = await _apiRepository.GET<WxRsp_ErrorCode, WxReq_ValidateAccessToken>(request);
            return response;
        }
        #endregion
    }
}
