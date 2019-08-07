using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imady.WxService;
using imady.Repository;
using Microsoft.Extensions.DependencyInjection;
using imady.Domain.Weixin;
using Microsoft.Extensions.Options;

namespace imady.WxService
{
    public static class ServiceCollectionExtensions
    {
        /* Frank 2018-11-07 : WeixinClient has been deprecated.
        public static void AddWeixinService(this IServiceCollection services)
        {
            services.AddWeixin(setupAction: null);
        }

        public static void AddWeixin(this IServiceCollection services, Action<WxOption> setupAction)
        {
            services.AddSingleton<WeixinClient>();
            //services.AddSingleton<WeChatPayNotifyClient>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
        */

        #region ==============微信身份（AccessToken和网页授权）=================   

        public static IServiceCollection AddWxIdentityService(this IServiceCollection services)
        {

            services.AddScoped<IWxIdentityService, WxIdentityService>();
            return services;
        }

        #endregion

        #region ===============菜单管理================   
        public static void AddWxMenuService(this IServiceCollection services)
        {
            services.AddScoped <IWxMenuService, WxMenuService >();
        }
        #endregion


        #region ===============素材管理================  
        public static void AddWxMaterialService(this IServiceCollection services)
        {

            services.AddScoped <IWxMaterialService, WxMaterialService >();
        }
        #endregion



        #region ===============消息管理（被动消息接收和回复）================
        public static IServiceCollection AddWxMessageService(this IServiceCollection services)
        {
            //添加各种微信推送消息的Handler处理器
            services.AddScoped<IWxMessageHandler<WxMessage_File>, WxMessageHandler_File>();
            services.AddTransient<IWxMessageHandler<WxMessage_Image>, WxMessageHandler_Image>();
            services.AddScoped<IWxMessageHandler<WxMessage_Link>, WxMessageHandler_Link>();
            services.AddScoped<IWxMessageHandler<WxMessage_Location>, WxMessageHandler_Location>();
            services.AddScoped<IWxMessageHandler<WxMessage_ShortVideo>, WxMessageHandler_ShortVideo>();
            services.AddScoped<IWxMessageHandler<WxMessage_Text>, WxMessageHandler_Text>();
            services.AddScoped<IWxMessageHandler<WxMessage_Unknown>, WxMessageHandler_Unknown>();
            services.AddScoped<IWxMessageHandler<WxMessage_Video>, WxMessageHandler_Video>();
            services.AddScoped<IWxMessageHandler<WxMessage_Voice>, WxMessageHandler_Voice>();
            //添加各种微信推送事件的Handler处理器
            services.AddScoped<IWxMessageHandler<WxEvent_Scan>, WxEventHandler_Scan>();
            services.AddScoped<IWxMessageHandler<WxEvent_ScanSubscribe>, WxEventHandler_ScanSubscribe>();
            services.AddScoped<IWxMessageHandler<WxEvent_Subscribe>, WxEventHandler_Subscribe>();
            services.AddScoped<IWxMessageHandler<WxEvent_Unsubscribe>, WxEventHandler_UnSubscribe>();
            services.AddScoped<IWxMessageHandler<WxEvent_Location>, WxEventHandler_Location>();
            //自定义菜单事件推送
            services.AddScoped<IWxMessageHandler<WxEvent_Click>, WxEventHandler_Click>();
            services.AddScoped<IWxMessageHandler<WxEvent_View>, WxEventHandler_View>();
            services.AddScoped<IWxMessageHandler<WxEvent_Location_select>, WxEventHandler_Location_select>();
            services.AddScoped<IWxMessageHandler<WxEvent_Pic_photo_or_album>, WxEventHandler_Pic_photo_or_album>();
            services.AddScoped<IWxMessageHandler<WxEvent_Pic_sysphoto>, WxEventHandler_Pic_sysphoto>();
            services.AddScoped<IWxMessageHandler<WxEvent_Pic_weixin>, WxEventHandler_Pic_weixin>();
            services.AddScoped<IWxMessageHandler<WxEvent_Scancode_push>, WxEventHandler_Scancode_push>();
            services.AddScoped<IWxMessageHandler<WxEvent_Scancode_waitmsg>, WxEventHandler_Scancode_waitmsg>();
            //添加微信消息服务
            services.AddSingleton<IWxMessageService, WxMessageService>();
            return services;
        }
        #endregion


        #region ===============微信支付================   
        public static void AddWxPayment(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion


        #region ===============用户管理================  
        public static void AddWxUserService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion


        #region ===============账号管理（带参二维码、微信认证）================  
        public static void AddWxAccountService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }

        #endregion


        #region ===============数据统计（用户、图文、消息、接口数据等分析）================  
        public static void AddWxAnalyticalService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }

        #endregion


        #region ===============微信卡券================  
        public static void AddWxCardService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion


        #region ===============微信门店================  
        public static void AddWxBusinessService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion


        #region ===============微信小店（微信商铺）================  
        public static void AddWxShopService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============微信智能AI接口（语音、AI）================  
        public static void AddWxAIService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============微信设备功能（硬件）================  
        public static void AddWxHardwareService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============新版微信客服接口（CustomService）================  
        public static void AddCRMService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============微信摇一摇周边(Shakearound)================  
        public static void AddShakearoundService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============微信连Wi-Fi接口（Bizwifi）================  
        public static void AddBizwifiService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============微信扫一扫（Scan）================  
        public static void AddScanService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============微信发票（Invoice）================  
        public static void AddInvoiceService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

        #region ===============其它（）================  
        public static void AddService(this IServiceCollection services, IOptions<WxOption> options)
        {

            //services.AddScoped <IWxService, WxService >();
        }
        #endregion

    }
}