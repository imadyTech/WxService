using imady.Domain.Weixin;
using System.Threading.Tasks;

namespace imady.WxService
{
    public interface IWxMaterialService : IWxService
    {
        /// <summary>
        /// 新增临时素材
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738726
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_UploadMedia> UploadMedia(WxReq_UploadMedia request);

        /// <summary>
        /// 获取临时素材
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738727
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_GetMedia> GetMedia(WxReq_GetMedia request);

        /// <summary>
        /// 新增永久图文素材 
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_AddNews> AddNews(WxReq_AddNews request);

        /// <summary>
        /// 新增其他类型永久素材
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_AddMaterial> AddMaterial(WxReq_AddMaterial request);

        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_GetMaterial> GetMaterial(WxReq_GetMaterial request);


        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_ErrorCode> Delete(WxReq_DeleteMaterial request);

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_ErrorCode> Update(WxReq_UpdateMaterial request);

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_GetMaterialCount> GetCount(WxReq_GetMaterialCount request);

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WxRsp_BatchGetMaterial> BatchGet(WxReq_BatchGetMaterial request);

    }
}