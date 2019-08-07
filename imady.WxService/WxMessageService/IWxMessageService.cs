using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imady.Domain.Weixin;

namespace imady.WxService
{
    public interface IWxMessageService: IWxService
    {
        Task<string> HandleMessage(string message);


        /// <summary>
        /// 执行WeChatPay API请求。
        /// </summary>
        /// <param name="request">具体的WeChatPay API请求</param>
        /// <returns>领域对象</returns>
        Task<WxMessage> HandleMessage<TMessage>(TMessage inMessage) where TMessage : IWxMessage;

        /// <summary>
        /// 返回一条错误提示消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<string> ReplyErrorMessage(WxMessage message);




    }
}
