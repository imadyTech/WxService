using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_Location_select : IWxMessageHandler<WxEvent_Location_select>
    {
        public WxEventHandler_Location_select()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Location_select);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Event_Location_select was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
