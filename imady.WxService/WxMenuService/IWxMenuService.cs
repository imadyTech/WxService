using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imady.Domain.Weixin;

namespace imady.WxService
{
    public interface IWxMenuService: IWxService
    {
        /// <summary>
        /// 自定义菜单创建接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141013
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<WxRsp_ErrorCode> Create(WxReq_CreateMenu menu);


        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="requst"></param>
        /// <returns></returns>
        Task<WxRsp_CreateConditionalMenu> Create(WxReq_CreateConditionalMenu requst);


        /// <summary>
        /// 自定义菜单查询接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141014
        /// </summary>
        /// <returns></returns>
        Task<WxRsp_GetConditionalMenu> GetMenu();


        /// <summary>
        /// 查询个性化菜单
        /// Frank 2018-11-23: This function was deprecated. Please use GetMenu().
        /// 
        /// 使用与自定义菜单查询相同的接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1455782296
        /// </summary>
        /// <returns></returns>
        //Task<WxRsp_GetConditionalMenu> GetConditional();

        /// <summary>
        /// 自定义菜单删除接口
        /// 使用接口创建自定义菜单后，开发者还可使用接口删除当前使用的自定义菜单。
        /// !!! 注意，在个性化菜单时，调用此接口会删除默认菜单及全部个性化菜单。
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141015
        /// </summary>
        /// <returns></returns>
        Task<WxRsp_ErrorCode> Delete();


        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuid">menuid为菜单id，可以通过自定义菜单查询接口获取。</param>
        /// <returns></returns>
        Task<WxRsp_ErrorCode> Delete(string menuid);


        /// <summary>
        /// 测试个性化菜单匹配结果
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1455782296
        /// </summary>
        /// <returns></returns>
        Task<WxRsp_TryMatchMenu> TryMatch(string userid);


        /// <summary>
        /// 获取自定义菜单配置接口
        /// Frank: 获取的菜单为当前激活的配置（API菜单或者微信管理工具菜单之一）
        /// (本接口无论公众号的接口是如何设置的，都能查询到接口，而自定义菜单查询接口则仅能查询到使用API设置的菜单配置。)
        /// 本接口将会提供公众号当前使用的自定义菜单的配置，如果公众号是通过API调用设置的菜单，则返回菜单的开发配置，
        /// 而如果公众号是在公众平台官网通过网站功能发布菜单，则本接口返回运营者设置的菜单配置。
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1434698695     
        /// </summary>
        /// <returns></returns>
        Task<WxRsp_GetSelfMenu> GetSelfMenu();
    }


}
