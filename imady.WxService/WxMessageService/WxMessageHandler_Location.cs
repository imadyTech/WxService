using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxMessageHandler_Location : IWxMessageHandler<WxMessage_Location>
    {
        public WxMessageHandler_Location()
        {

        }
        public System.Type HandlingType
        {
            get { return typeof(WxMessage_Location); }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Location was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
