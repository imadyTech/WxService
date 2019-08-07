using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_ScanSubscribe : IWxMessageHandler<WxEvent_ScanSubscribe>
    {
        public WxEventHandler_ScanSubscribe()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_ScanSubscribe);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Event Scan&Subscribe was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
