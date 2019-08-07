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
    public class WxBaseService : IWxService
    {
        public IWxIdentityService _identityService;

        public WxOption _option;

        #region 构造函数
        public WxBaseService(
            IOptions<WxOption> option, 
            IWxIdentityService identityService)
            {
                _option = option.Value;
                _identityService = identityService;

            }

        #endregion

        public async Task<string> GetWxAccessToken()
        {
            var tokenGetter = await _identityService.GetAccessToken();

            return tokenGetter.access_token;
        }




    }
}