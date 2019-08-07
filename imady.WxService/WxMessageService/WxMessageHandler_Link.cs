using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxMessageHandler_Link : IWxMessageHandler<WxMessage_Link>
    {
        public WxMessageHandler_Link()
        {

        }
        public System.Type HandlingType
        {
            get { return typeof(WxMessage_Link); }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Link was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
