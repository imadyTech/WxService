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

namespace imady.WxService
{
    /// <summary>
    /// 消息管理（被动消息接收和回复）
    /// 根据动态配置的消息回复方式，解析微信推送的消息并返回相应的答复。
    /// </summary>
    public class WxMessageService : WxBaseService, IWxMessageService
    {
        public virtual ILogger<WxMessageService> _logger { get; set; }

        public IServiceProvider _provider;

        //Frank： 这是IEnumerable方案来实现查找WxMessage对应的Handler的方法。未采用。
        //public IEnumerable<IWxMessageHandler> _handlers;

        #region 构造函数
        public WxMessageService(IOptions<WxOption> option, ILogger<WxMessageService> logger, IServiceProvider provider, IWxIdentityService identityService)
            :base(option, identityService)
        {
            _provider = provider;
            _logger = logger;
            //_handlers = handlers;
            logger.LogTrace("========WxMessageService initiated.===========");

        }

        #endregion


        public async Task<string> HandleMessage(string messageBody)
        {
            #region Frank：测试SimpleXML序列化器用
            /* 
            var reply = new WxReply_News()
            {
                ToUserName = "ohKYFxDy4WBQJx_17dAMJihaJN-8",
                FromUserName = "gh_bdd620900a5b",
                ArticleCount = 1,
                Articles = new WxArticle[]
{
                    new WxArticle(){
                        Title ="古镇",
                        Description ="震泽古镇",
                        PicUrl = "http://www.imady.com/images/Imady%20logo_2010_256.png",
                        Url = "http://www.imady.com/Home/Portfolio#Zhenze"}
}
            };
            var reply1 = new WxReply_Text()
            {
                ToUserName = "ohKYFxDy4WBQJx_17dAMJihaJN-8",
                FromUserName = "gh_bdd620900a5b",
                Content = "test"

            };
            var music = new WxReply_Music()
            {
                ToUserName = "ohKYFxDy4WBQJx_17dAMJihaJN-8",
                FromUserName = "gh_bdd620900a5b",
                Music = new WxMusic
                {
                    Title = "音乐",
                    Description = "数鸭歌",
                    MusicURL = "http://yingyu88.imady.com/musictest.mp3",
                    HQMusicUrl = "http://yingyu88.imady.com/musictest.mp3",
                    ThumbMediaId = "4Ln9zjbEdBjMWS-TtYCmd89stx7CVmmmMGx1wdKul9-g6zOHQYJMukPEsC0pVd4h"
                }
            };

            var deserialized = new WxSerializer_SimpleXml().Serialize(music);
            return deserialized;
            */
            #endregion

            //转换成为WxMsgBase对象
            //var parser = new WxInMessageDeserializer();
            //var reply =(WxReplyMessage_Text) await HandleMessage( parser.Parse(messageBody));

            //return await Task.FromResult(BuildXmlContent(reply));
            //_logger.LogInformation($"===Deserializing: ==={messageBody}");

            var reply = default(WxMessage);

            //实际上，接收微信消息并进行处理器分发未必需要反序列化成WxMessage数据对象，采用文本Enum也是效率很高的方式。
            using (var deserial = new WxDeserializer_InMessage())
            {
                //_logger.LogInformation($"===收到信息: ==={messageBody}");
                var deserializedModel =  deserial.Parse(messageBody);
                //_logger.LogTrace($"===解析格式: ==={deserializedModel.GetType()}");

                //注意：此处调用的HandleMessage()方法用到了C#的类型参数推断
                reply = await HandleMessage(deserializedModel);

            }
            using (var serializer = new WxSerializer_SimpleXml()) 
            return await Task.FromResult(serializer.Serialize(reply));

        }


        public async Task<WxMessage> HandleMessage<TMessage>(TMessage message) where TMessage : IWxMessage
        {
            //_logger.LogInformation("=====MsgType===== " + message.GetType());
            //_logger.LogInformation("=====MsgType===== " + typeof( TMessage));

            //Frank： 这个方法来自于StackOverflow的回答
            var messageType = message.GetType();
            var messageHandlerType = typeof(IWxMessageHandler<>).MakeGenericType(messageType);
            //这是自己通过dynamic的方式的解决方法，但是并非最理想：
            dynamic dyna = _provider.GetService(messageHandlerType);
            _logger.LogInformation($"===信息处理方式: ==={dyna.GetType()}");
            var handlingstxnResult = dyna.Handle(message);
            return handlingstxnResult.Result;


            //Frank： 这是IEnumerable方案来实现查找WxMessage对应的Handler的方法。未采用。
            /*
            var _handlers = _provider.GetServices<IWxMessageHandler<TMessage>>();
            //_logger.LogInformation("=====is _hanlderfromprovider null?===== " + (_handlers == null));
            
            var type = typeof(TMessage);
            var _handler = _handlers
                .Where(h => h.HandlingType.Equals(message.GetType()))
                .FirstOrDefault();
            var handlingResult = _handler.Handle(message);
            return await handlingResult;
            */

            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)(object)message).FromUserName,
                FromUserName = ((WxMessage)(object)message).ToUserName,
                Content = ((WxMessage)(object)message).MsgType
            };
            return await Task.FromResult(reply);

        }


        public async Task<string> ReplyErrorMessage(WxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = message.FromUserName,
                FromUserName = _option.WxName,
                CreateTime = message.CreateTime,
                Content = "非常抱歉，我们无法识别您的消息。请检查后重新发送。"

            };
            return await Task.FromResult(new WxSerializer_SimpleXml().Serialize(reply));
        }
    }
}