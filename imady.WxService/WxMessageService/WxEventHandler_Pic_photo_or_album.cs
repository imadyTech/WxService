using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxEventHandler_Pic_photo_or_album : IWxMessageHandler<WxEvent_Pic_photo_or_album>
    {
        public WxEventHandler_Pic_photo_or_album()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxEvent_Pic_photo_or_album);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Event_Pic_photo_or_album was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
