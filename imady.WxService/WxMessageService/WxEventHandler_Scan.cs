using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_Scan : IWxMessageHandler<WxEvent_Scan>
    {
        public WxEventHandler_Scan()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Scan);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of WxEvent_Scan was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
