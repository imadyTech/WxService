using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxMessageHandler_Unknown : IWxMessageHandler<WxMessage_Unknown>
    {
        public WxMessageHandler_Unknown()
        {

        }
        public System.Type HandlingType
        {
            get { return typeof(WxMessageHandler_Unknown); }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType is unknown."
            };

            return await Task.FromResult(reply);
        }
    }
}
