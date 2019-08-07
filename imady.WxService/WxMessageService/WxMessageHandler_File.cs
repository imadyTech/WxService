using imady.Domain.Weixin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace imady.WxService
{
    public class WxMessageHandler_File : IWxMessageHandler<WxMessage_File>
    {
        public WxMessageHandler_File()
        {

        }

        public System.Type HandlingType
        {
            get
            {
                return typeof(WxMessage_File);
            }
        }


        public async Task<WxMessage> Handle(IWxMessage message)
        {
            var reply = new WxReply_Text()
            {
                ToUserName = ((WxMessage)message).FromUserName,
                FromUserName = ((WxMessage)message).ToUserName,
                Content = "The MsgType of FILE was successfully handled."
            };

            return await Task.FromResult(reply);
        }
    }
}
