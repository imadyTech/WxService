using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_Pic_sysphoto : IWxMessageHandler<WxEvent_Pic_sysphoto>
    {
        public WxEventHandler_Pic_sysphoto()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Pic_sysphoto);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Event_Pic_photo was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
