using imady.Domain.Weixin;
using System.Threading.Tasks;

namespace imady.WxService
{
    public interface IWxIdentityService : IWxService
    {
        #region 关于网页授权access_token和普通access_token的区别
        /// 微信官方文档区分 “网页授权access_token 与 普通access_token”：
        /// 1、微信网页授权是通过OAuth2.0机制实现的，在用户授权给公众号后，公众号可以获取到一个网页授权特有的接口调用凭证（网页授权access_token），
        /// 通过网页授权access_token可以进行授权后接口调用，如获取用户基本信息；
        /// 2、其他微信接口，需要通过基础支持中的“获取access_token”接口来获取到的普通access_token调用。
        #endregion

        /// <summary>
        /// 微信身份基础功能：获取普通access_token
        /// </summary>
        /// <returns></returns>
        Task<WxRsp_AccessToken> GetAccessToken();


        #region
        /// 关于网页授权的两种scope的区别说明
        /// 1、以snsapi_base为scope发起的网页授权，是用来获取进入页面的用户的openid的，并且是静默授权并自动跳转到回调页的。用户感知的就是直接进入了回调页（往往是业务页面）
        /// 2、以snsapi_userinfo为scope发起的网页授权，是用来获取用户的基本信息的。但这种授权需要用户手动同意，并且由于用户同意过，所以无须关注，就可在授权后获取该用户的基本信息。
        /// 3、用户管理类接口中的“获取用户基本信息接口”，是在用户和公众号产生消息交互或关注后事件推送后，才能根据用户OpenID来获取用户基本信息。这个接口，包括其他微信接口，都是需要该用户（即openid）关注了公众号后，才能调用成功的。
        #endregion

        /// <summary>
        /// 第一步：用户同意授权，获取code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_AuthorizeCode> GetAuthorizeCode(WxReq_AuthorizeCode request);


        /// <summary>
        /// 第二步：通过code换取网页授权access_token
        /// </summary>
        /// <param name="request"></param>
        Task<WxRsp_AuthorizeAccessToken> GetAuthorizeAccesstoken(WxReq_AuthorizeAccessToken request);


        /// <summary>
        /// 第三步：刷新access_token（如果需要）
        /// 注意：返回的消息采用WxRsp_AuthorizeAccessToken格式
        /// </summary>
        /// <param name="request"></param>
        Task<WxRsp_AuthorizeAccessToken> GetRefreshedAccessToken(WxReq_RefreshAccessToken request);


        /// <summary>
        /// 第四步：拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="request"></param>
        Task<WxRsp_WxUserInfo> GetWxUserInfo(WxReq_WxUserInfo request);

        /// <summary>
        /// 微信网页授权
        /// 附：检验授权凭证（access_token）是否有效
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842
        /// </summary>
        Task<WxRsp_ErrorCode> ValidateAccessToken(WxReq_ValidateAccessToken request);
    }
}