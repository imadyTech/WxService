using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_UnSubscribe : IWxMessageHandler<WxEvent_Unsubscribe>
    {
        public WxEventHandler_UnSubscribe()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Unsubscribe);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Event_Unsubscribe was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
