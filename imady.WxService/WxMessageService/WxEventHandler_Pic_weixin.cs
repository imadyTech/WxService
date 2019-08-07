using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_Pic_weixin : IWxMessageHandler<WxEvent_Pic_weixin>
    {
        public WxEventHandler_Pic_weixin()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Pic_weixin);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of WxEvent_Pic_weixin was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
