using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxMessageHandler_Image : IWxMessageHandler<WxMessage_Image>
    {
        public WxMessageHandler_Image()
        {

        }
        public System.Type HandlingType
        {
            get { return typeof(WxMessage_Image); }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            /*
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                MsgType = "text",
                Content = "The MsgType of Image was successfully handled."
            };
            */
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

            reply.ToUserName = ((WxMessage)message).FromUserName;
            reply.FromUserName = ((WxMessage)message).ToUserName;


            return await Task.FromResult(reply);
        }
    }
}
