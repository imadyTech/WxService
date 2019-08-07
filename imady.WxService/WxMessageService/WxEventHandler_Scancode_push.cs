using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_Scancode_push : IWxMessageHandler<WxEvent_Scancode_push>
    {
        public WxEventHandler_Scancode_push()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Scancode_push);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of WxEvent_Scancode_push was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
