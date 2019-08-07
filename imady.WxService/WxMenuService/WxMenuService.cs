using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using imady.Domain.Weixin;
using Microsoft.Extensions.DependencyInjection;
using imady.Repository;

namespace imady.WxService
{
    /// <summary>
    /// 自定义菜单管理
    /// </summary>
    public class WxMenuService : WxBaseService, IWxMenuService
    {
        public ILogger<WxMenuService> _logger { get; set; }

        private IWxApiRepository _apiRepository;


        #region 构造函数
        public WxMenuService(
            IOptions<WxOption> wxOption,
            ILogger<WxMenuService> logger,
            IWxIdentityService identityService)
            : this(new WxApiRepository(), wxOption, logger, identityService) { }


        public WxMenuService(IWxApiRepository apiRepository, IOptions<WxOption> option, ILogger<WxMenuService> logger, IWxIdentityService identityService)
            :base(option, identityService)
        {
            _logger = logger;
            _apiRepository = apiRepository;

            _logger.LogInformation("======== WxMenuService initiated. ===========\r\n");

        }

        #endregion



        /// <summary>
        /// 自定义菜单创建接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141013
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_ErrorCode> Create(WxReq_CreateMenu request)
        {
            request.access_token = GetWxAccessToken().Result;
            //_logger.LogInformation($"========access_token : {tokenGetter.access_token}========");

            var response = await _apiRepository.POST<WxRsp_ErrorCode, WxReq_CreateMenu>(request);
            return response;
        }


        /// <summary>
        /// 创建个性化菜单
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141014
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_CreateConditionalMenu> Create(WxReq_CreateConditionalMenu request)
        {
            request.access_token = GetWxAccessToken().Result;
            //_logger.LogInformation($"========access_token : {tokenGetter.access_token}========");

            var response = await _apiRepository.POST<WxRsp_CreateConditionalMenu, WxReq_CreateConditionalMenu>(request);
            return response;
        }


        /// <summary>
        /// 自定义菜单查询接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141014
        /// </summary>
        /// <returns></returns>
        public async Task<WxRsp_GetConditionalMenu> GetMenu()
        {
            WxRsp_GetConditionalMenu response =
                await _apiRepository.GET<WxRsp_GetConditionalMenu, WxReq_GetConditionalMenu>
                (
                    new WxReq_GetConditionalMenu()
                    {
                        access_token = GetWxAccessToken().Result
                    });
            _logger.LogInformation($"\r\n========Response.ResponseRawMessage : {response.ResponseRawMessage}========");

            return response;

        }

        /// <summary>
        /// 查询个性化菜单
        /// Frank 2018-11-23: This function was deprecated. Please use GetMenu().
        /// 
        /// 使用与自定义菜单查询相同的接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1455782296
        /// </summary>
        /// <returns></returns>
        /*
        public async Task<WxRsp_GetConditionalMenu> GetConditional()
        {
            WxRsp_GetConditionalMenu response =
                await _apiRepository.GET<WxRsp_GetConditionalMenu, WxReq_GetConditionalMenu>
                (
                    new WxReq_GetConditionalMenu()
                    {
                        access_token = _access_token().Result
                    });
            _logger.LogInformation($"\r\n========Response.ResponseRawMessage : {response.ResponseRawMessage}========");

            return response;
        }
        */

        /// <summary>
        /// 删除所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<WxRsp_ErrorCode> Delete()
        {
            var response = await _apiRepository.POST<WxRsp_ErrorCode, WxReq_DeleteMenu>(
                new WxReq_DeleteMenu()
                {
                    access_token = GetWxAccessToken().Result
                });
            return response;
        }


        /// <summary>
        /// 删除id所指定的单个个性化菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_ErrorCode> Delete(string menuid)
        {
            var response = await _apiRepository.POST<WxRsp_ErrorCode, WxReq_DeleteConditionalMenu>(
                new WxReq_DeleteConditionalMenu()
                {
                    menuid = menuid,
                    access_token = GetWxAccessToken().Result
                });
            return response;
        }


        /// <summary>
        /// 测试个性化菜单匹配结果
        /// FrankShen: 根据用户微信号或者OpenId来查看所得到的动态菜单。
        /// </summary>
        /// <param name="userid">user_id可以是粉丝的OpenID，也可以是粉丝的微信号。</param>
        /// <returns></returns>
        public async Task<WxRsp_TryMatchMenu> TryMatch(string userid)
        {
            var response = await _apiRepository.POST<WxRsp_TryMatchMenu, WxReq_TryMatchMenu>(
                new WxReq_TryMatchMenu()
                {
                    user_id = userid,
                    access_token = GetWxAccessToken().Result
                });
            return response;

        }


        /// <summary>
        /// 获取自定义菜单配置接口
        /// Frank: 获取的菜单为当前激活的配置（API菜单或者微信管理工具菜单之一）
        /// </summary>
        /// <returns></returns>
        public async Task<WxRsp_GetSelfMenu> GetSelfMenu()
        {
            WxRsp_GetSelfMenu response =
                await _apiRepository.GET<WxRsp_GetSelfMenu, WxReq_GetSelfMenu>
                (
                    new WxReq_GetSelfMenu()
                    {
                        access_token = GetWxAccessToken().Result
                    });
            _logger.LogInformation($"\r\n========Response.ResponseRawMessage : {response.ResponseRawMessage}========");

            return response;

        }

    }
}