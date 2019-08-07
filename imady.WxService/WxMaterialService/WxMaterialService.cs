using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using imady.Repository;
using imady.Domain.Weixin;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace imady.WxService
{
    public class WxMaterialService : WxBaseService, IWxMaterialService
    {
        private IWxApiRepository _apiRepository;
        private ILogger<WxMenuService> _logger { get; set; }

        #region 构造函数
        public WxMaterialService(
            IOptions<WxOption> wxOption,
            ILogger<WxMenuService> logger,
            IWxIdentityService identityService)
            : this(new WxApiRepository(), wxOption, logger, identityService) { }


        public WxMaterialService(IWxApiRepository apiRepository, IOptions<WxOption> option, ILogger<WxMenuService> logger, IWxIdentityService identityService)
            : base(option, identityService)
        {
            _logger = logger;
            _apiRepository = apiRepository;

            _logger.LogInformation("======== WxMaterialService initiated. ===========");
        }
        #endregion



        #region Member functions
        /// <summary>
        /// 新增临时素材
        /// </summary>
        /// <param name="request">请求必须包含media附件的ByteArrayContent</param>
        /// <returns></returns>
        public async Task<WxRsp_UploadMedia> UploadMedia(WxReq_UploadMedia request)
        {
            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.Upload<WxRsp_UploadMedia, WxReq_UploadMedia>(request, request.media);

            return response;
        }


        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_GetMedia> GetMedia(WxReq_GetMedia request)
        {

            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.Download<WxRsp_GetMedia, WxReq_GetMedia>(request);

            return response;
        }

        /// <summary>
        /// 新增永久图文素材 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_AddNews> AddNews(WxReq_AddNews request)
        {
            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.POST<WxRsp_AddNews, WxReq_AddNews>(request);

            return response;
        }


        /// <summary>
        /// 新增其他类型永久素材
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_AddMaterial> AddMaterial(WxReq_AddMaterial request)
        {
            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.Upload<WxRsp_AddMaterial, WxReq_AddMaterial>(request, request.media);

            return response;
        }


        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_GetMaterial> GetMaterial(WxReq_GetMaterial request)
        {
            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.Download<WxRsp_GetMaterial, WxReq_GetMaterial>(request);

            return response;
        }


        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_ErrorCode> Delete(WxReq_DeleteMaterial request)
        {
            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.POST<WxRsp_ErrorCode, WxReq_DeleteMaterial>(request);

            return response;
        }


        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_ErrorCode> Update(WxReq_UpdateMaterial request)
        {
            request.access_token = GetWxAccessToken().Result;

            var response = await _apiRepository.POST<WxRsp_ErrorCode, WxReq_UpdateMaterial>(request);

            return response;
        }


        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_GetMaterialCount> GetCount(WxReq_GetMaterialCount request)
        {
            request.access_token = GetWxAccessToken().Result;
            var response = await _apiRepository.POST<WxRsp_GetMaterialCount, WxReq_GetMaterialCount>(request);

            return response;
        }


        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WxRsp_BatchGetMaterial> BatchGet(WxReq_BatchGetMaterial request)
        {
            request.access_token = this.GetWxAccessToken().Result;
            var requestUrl = request.WxApiRequestBaseUrl + request.UrlParameter;

            using (StringContent requestContent = new StringContent(
                new WxSerializer_Json().Serialize(request), 
                Encoding.UTF8, 
                "application/json"))
            {
                string responseMessage = await _apiRepository.POST(requestContent, requestUrl);

                return request.DeserializeResponse( responseMessage );
            }
    }

    #endregion


}
}
