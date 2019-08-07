using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxMessageHandler_Video : IWxMessageHandler<WxMessage_Video>
    {
        public WxMessageHandler_Video()
        {

        }


        public System.Type HandlingType
        {
            get
            {
                return typeof(WxMessage_Video);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of Text was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
