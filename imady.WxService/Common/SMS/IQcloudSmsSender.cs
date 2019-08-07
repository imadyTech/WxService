using System.Collections.Generic;

namespace imady.Common
{
    public interface IQcloudSmsSender
    {
        SmsSingleSenderResult Send(
            int type,
            string nationCode,
            string phoneNumber,
            string msg,
            string extend,
            string ext);

        SmsSingleSenderResult SendWithParam(
            string nationCode,
            string phoneNumber,
            int templId,
            List<string> templParams,
            string sign,
            string extend,
            string ext);
    }
}