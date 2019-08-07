using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
//using Yingyu88Wx;
using IdentityServer4.Models;

namespace imady.XUnit
{
    public class ValuesTests
    {
        public ValuesTests()
        {
            /*
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Yingyu88Wx.Startup>());
            Client = server.CreateClient();
            */
        }


        public HttpClient Client { get; }

        [Fact]
        public async Task GetById_ShouldBe_Ok()
        {
            // Arrange
            var id = 1;

            // Act
            var response = await Client.GetAsync($"/weixin/entrance/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// 测试client credential是否正确
        /// </summary>
        [Fact]
        public void TestSha256()
        {
            //实测identityserver4.models下的SHA256计算结果与Meiyu.common下的SHA256不同
            var sha = SHA256.Compute("secret");
            var is4sha = "secret".Sha256();
            Assert.Equal("K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=", is4sha);
        }


        [Fact]
        public void TestExceptionSerializer()
        {
            var formatter = new Func<Exception, string>(ExceptionSerializer);
            var ex = new Exception("TestExceptionSerializer");
            var result = formatter(ex);
            Console.WriteLine(result);

            Assert.NotEmpty(result);

        }
        private string ExceptionSerializationResult;
        private string ExceptionSerializer(Exception arg)
        {

            if (arg.InnerException != null)
            {
                var formatter = new Func<Exception, string>(ExceptionSerializer);
                ExceptionSerializationResult += formatter(arg.InnerException);
            }
            return ExceptionSerializationResult + arg.Message;

        }


    }
}
