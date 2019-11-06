using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MineServer;
using MineServer.Resources;
using Newtonsoft.Json;
using NUnit.Framework;

namespace NunitTests
{
    public class PlayerControllerTests
    {
        private HttpClient _client;
        static string apipath = "/api/player/";
        static string mediaType = "application/json";
        private static string requestUri = apipath;

        [SetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            TestServer testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Test]
        public async Task Test1()
        {
            //Arrange
            PlayerData player = new PlayerData{userName = "user",password = "12345"};
            
            //Act
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(player));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            HttpResponseMessage response = new HttpResponseMessage();
            response = await _client.PostAsync(requestUri, httpContent);
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
            

            //Assert
            Assert.Pass();
        }
    }
}