using imady.Domain.Weixin;
using System.Threading.Tasks;

namespace imady.WxService
{
    public interface IWxMessageHandler<TMessage>
    {
        Task<WxMessage> Handle(IWxMessage message);

        //Frank： 这是IEnumerable方案中用于识别Handler所对应的WxMessage的方法。
        System.Type HandlingType { get; }
    }
}