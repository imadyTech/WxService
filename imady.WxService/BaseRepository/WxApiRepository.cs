using System;
using System.Collections.Generic;
using System.Text;
using imady.Domain;
using imady.WxContext;
using imady.Domain.Weixin;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace imady.Repository
{
    // ======== 待实现 ========

    public class WxApiRepository : IWxApiRepository
    {
        private WxOption _options;
        private HttpClient _wxClient;

        public WxApiRepository()
        {
            _wxClient = new HttpClient();
        }

        public WxApiRepository(IOptions<WxOption> options)
        {
            _options = options.Value;
        }


        /// <summary>
        /// Get方法，返回序列化后的TResponse对象
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> GET<TResponse, TRequest>(TRequest request) where TRequest : IWxRequest<TResponse>
        {
            using (var response = await _wxClient.GetAsync(request.WxApiRequestBaseUrl + request.UrlParameter))
            using (var responseContent = response.Content)
            {
                string wxresponse = await responseContent.ReadAsStringAsync();
                //return request.ResponseDeserializer.Parse(wxresponse);
                var result = request.DeserializeResponse(wxresponse);
                return result;
            }
        }


        /// <summary>
        /// 不作反序列化，直接返回string信息
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public async Task<string> GET(string requestUrl)
        {
            using (var response = await _wxClient.GetAsync( requestUrl ))
            {
                string wxresponse = await response.Content.ReadAsStringAsync();
                return wxresponse;
            }
        }


        public async Task<TResponse> POST<TResponse, TRequest>(TRequest request) where TRequest : IWxRequest<TResponse>
        {
            using (var requestContent = new StringContent(
                new WxSerializer_Json().Serialize(request),
                Encoding.UTF8,
                "application/json"
                ))
            using (var response = await _wxClient.PostAsync(
                request.WxApiRequestBaseUrl + request.UrlParameter,
                requestContent))

            using (var responseContent = response.Content)
            {
                string wxresponse = await responseContent.ReadAsStringAsync();
                //return request.ResponseDeserializer.Parse(wxresponse);
                return request.DeserializeResponse(wxresponse);
            }
        }

        /// <summary>
        /// 不作反序列化，直接返回string信息
        /// </summary>
        /// <param name="requestContent"></param>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public async Task<string> POST(StringContent requestContent, string requestUrl)
        {
            using (var response = await _wxClient.PostAsync(requestUrl, requestContent))

            using (var responseContent = response.Content)
            {
                string wxresponse = await responseContent.ReadAsStringAsync();
                return wxresponse;
            }

        }


        public async Task<TResponse> Upload<TResponse, TRequest>(TRequest request, ByteArrayContent bytecontent)
            where TRequest : IWxRequest<TResponse>
        {
            //content.Headers.Add("filelength", "1000 ");
            //content.Headers.Add("filename", "frank.jpg ");
            //content.Headers.Add("KeepAlive", "true ");
            //content.Headers.Add("media", "@frank.jpg ");
            //content.Headers.Add("Content-Type", "application/octet-stream ");
            //content.Headers.Add("Content-Disposition", "form-data ");


            var content = new MultipartFormDataContent();
            content.Headers.Remove("Content-Type");
            content.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data;");

            bytecontent.Headers.Remove("Content-Disposition");
            bytecontent.Headers.TryAddWithoutValidation("Content-Disposition", $"form-data; name=\"media\";filename=\"Frank_2.png\"" + "");
            bytecontent.Headers.Remove("Content-Type");
            bytecontent.Headers.TryAddWithoutValidation("Content-Type", "image/png");
            content.Add(bytecontent);




            StringBuilder sbHeader = new StringBuilder(
                string.Format(
                    "Content-Disposition:form-data;name=\"media\";filelength=\"{1}\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n",
                    "",
                    10000));


            _wxClient.DefaultRequestHeaders.Add("User-Agent", "KnowledgeCenter");
            _wxClient.DefaultRequestHeaders.Remove("Expect");
            _wxClient.DefaultRequestHeaders.Remove("Connection");
            _wxClient.DefaultRequestHeaders.ExpectContinue = false;
            _wxClient.DefaultRequestHeaders.ConnectionClose = true;

            using (var response = await _wxClient.PostAsync(
                request.WxApiRequestBaseUrl + request.UrlParameter,
                content))

            using (var responseContent = response.Content)
            {
                string wxresponse = await responseContent.ReadAsStringAsync();

                //return request.ResponseDeserializer.Parse(wxresponse);
                return request.DeserializeResponse(wxresponse);
            }
        }

        /// <summary>
        /// 暂时无引用
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<TResponse> Upload<TResponse, TRequest>(TRequest request, MultipartFormDataContent content)
            where TRequest : IWxRequest<TResponse>
        {
            //设定要响应的数据格式
            this._wxClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            this._wxClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            this._wxClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            this._wxClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            this._wxClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));

            content.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            content.Headers.Add("Timeout", "1000");
            content.Headers.Add("KeepAlive", "true");

            using (var response = await _wxClient.PostAsync(
                request.WxApiRequestBaseUrl + request.UrlParameter,
                content))

            using (var responseContent = response.Content)
            {
                string wxresponse = await responseContent.ReadAsStringAsync();

                //return request.ResponseDeserializer.Parse(wxresponse);
                return request.DeserializeResponse(wxresponse);
            }
        }


        public async Task<TResponse> Download<TResponse, TRequest>(TRequest request)
            where TRequest : IWxRequest<TResponse>
            where TResponse : IWxResponse, IWxDownloadable
        {
            using (var requestContent = new StringContent(
                new WxSerializer_Json().Serialize(request),
                Encoding.UTF8,
                "application/json"
                ))
            using (var response = await _wxClient.PostAsync(
                request.WxApiRequestBaseUrl + request.UrlParameter,
                requestContent))

            using (var responseContent = response.Content)
            {
                string wxresponse = await responseContent.ReadAsStringAsync();
                //return request.ResponseDeserializer.Parse(wxresponse);
                var download = request.DeserializeResponse(wxresponse);
                var entity = responseContent.ReadAsByteArrayAsync();

                download.Content_Type = responseContent.Headers.ContentType?.ToString();
                download.Content_Disposition = responseContent.Headers.ContentDisposition.ToString();
                download.Content_Length = responseContent.Headers.ContentLength;
                download.Content_Entity = entity.Result;

                return download;
            }
        }

    }
    /*
    {
        private IMeiyuBaseContext Context;
        private DbSet<T> Entities
        {
            get { return this.Context.Set<T>(); }
        }

        #region 构造函数（需要传入一个泛型DbContext）
        public MeiyuApiRepository(IMeiyuBaseContext context)
        {
            this.Context = context;
        }
        #endregion

        #region 析构函数
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
        #endregion

        //==================================================

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Context.SaveChanges();
        }
        
        public void Delete(Guid id)
        {
            var course = Context.Set<T>().Find(id);
            Entities.Remove(course);
        }

        public T Get(Guid id)
        {
            var entity = Context.Set<T>().Find(id);
            return entity;
        }

        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.AsEnumerable<T>();
        }

    }
    */



}
//https://blog.csdn.net/niunan/article/details/78046471
/// <summary>
/// 上传临时素材
/// 返回media_id
/// </summary>
/// <param name="userid"></param>
/// <returns></returns>
