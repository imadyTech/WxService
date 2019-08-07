using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imady.Domain.Weixin;
using System.Net.Http;

namespace imady.Repository
{
    public interface IWxApiRepository

    {
        /*
        T Get(Guid id);

        IEnumerable<T> GetAll();

        void Create(T model);

        void Update(T model);

        void Delete(Guid id);
        */

        /// <summary>
        /// Get方法，返回序列化后的TResponse对象
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> GET<TResponse, TRequest>(TRequest request) where TRequest : IWxRequest<TResponse>;

        /// <summary>
        /// 不作反序列化，直接返回string信息
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        Task<string> GET(string requestUrl);

        Task<TResponse> POST<TResponse, TRequest>(TRequest request) where TRequest : IWxRequest<TResponse>;

        /// <summary>
        /// 仅返回未序列化的RawMessage，由调用者自行处理。
        /// </summary>
        /// <param name="requestContent"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        Task<string> POST(StringContent requestContent, string requestUrl);

        Task<TResponse> Upload<TResponse, TRequest>(TRequest request, ByteArrayContent content)
            where TRequest : IWxRequest<TResponse>;

        Task<TResponse> Download<TResponse, TRequest>(TRequest request)
            where TRequest : IWxRequest<TResponse>
            where TResponse : IWxResponse, IWxDownloadable;

    }
}
