# imady.WxService


imady.WxService is a SDK allow you build up your WeChat application more easily. Currently the versio 0.1.5 provided the following features:
# Supported:

        #region ==============微信基本身份获取（AccessToken和网页授权）=================   

        #region ===============菜单管理================   

        #region ===============素材管理================  

        #region ===============消息管理（被动消息接收和回复）================


# coming soon: 
        #region ===============微信支付================   

        #region ===============用户管理================  

        #region ===============账号管理（带参二维码、微信认证）================  

        #region ===============数据统计（用户、图文、消息、接口数据等分析）================  

        #region ===============微信卡券================  

        #region ===============微信门店================  

        #region ===============微信小店（微信商铺）================  

        #region ===============微信智能AI接口（语音、AI）================  

        #region ===============微信设备功能（硬件）================  

        #region ===============新版微信客服接口（CustomService）================  

        #region ===============微信摇一摇周边(Shakearound)================  

        #region ===============微信连Wi-Fi接口（Bizwifi）================  

        #region ===============微信扫一扫（Scan）================  

        #region ===============微信发票（Invoice）================  

        #region ===============其它（）================  

# How to use
1. First of all you need apply a WeChat Public Account through https://mp.weixin.qq.com/.
1. Put your WeChat Public Account major developer options in the appsettings.json of a MVC project:

~~~
......
  "WeixinOption": {
    "WxName": "xxxxxx",
    "WxType": "service",
    "AppId": "xxx-xxx-xxx",
    "AppSecret": "xxxxxxxxxx-xxxxx-xxxxx",
    "Token": "xxx-xxx-xxx",
    "EncodingAESKey": "xxxx-xxxx-xxxx-xxxx-xxxx-xxxx"
  },
  ~~~
3. create a controller in your MVC project and add the following code, then your asp.net core server will response customer message:

~~~
    public class WeixinController : ControllerBase
    {
        private readonly IWxMessageService _wxMsgService;

        public WeixinController(IWxMessageService msgService)
        {
            _wxMsgService = msgService;
        }

        /// <summary>
        /// 微信公众号开发信息配置的验证入口。一次性验证完成后无需改动。
        //This is one-time verification code when you try to set your WeChat Developer Model on.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Entrance([Bind("signature, timestamp, nonce, echostr")] WxEntranceVerificationRequest request)
        {
            _logger.LogWarning($"收到请求 {request.timestamp} - {request.nonce}");

            //字典排序
            string[] ArrTmp = { Config.GetWxConfiguration().Token, request.timestamp, request.nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            //字符加密
            var sha1 = Sha1.HmacSign(tmpStr);
            if (sha1.Equals(request.signature))
            {
                return Content(request.echostr);
            }
            else
            {
                return Content("欢迎访问。请提交正确的验证参数。");
            }

        }


        /// <summary>
        /// 这是公众号接收微信推送消息(WxMessage或者WxEvent)的入口
        /// Each time when a user send message to your WeChat or click the menu, 
        /// here will be the entrance of receiving the message
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Entrance()
        {
            DateTime start = DateTime.Now;
            string reply = new StreamReader(HttpContext.Request.Body).ReadToEnd();
            try
            {
                //将接收到的微信消息体传给Handler
                //可以进行扩展，实现用户自定义回复内容。
                reply = await _wxMsgService.HandleMessage(reply);
            }
            catch (Exception ex)
            {
                reply = $"Error: {ex.Message}\r\n{ex.StackTrace}";
            }
            return await Task.FromResult(Content(reply));
        }
    }
~~~

