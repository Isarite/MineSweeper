using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using MineServer.Controllers;
using MineServer.Models;
using Microsoft.EntityFrameworkCore;
using MineServer;
using MineServer.Resources;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace ControllerTests
{
    public class PlayerControllerTests
    {
        private readonly HttpClient _client;
        static string apipath = "/api/player/";
        static string mediaType = "application/json";
        private static string requestUri = apipath;
        
        
        public PlayerControllerTests()
        {
            //var server = new TestServer();
            //TestServer;
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            TestServer testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }
        
//        [Fact]
//        public async void Test1()
//        {
//            //Arrange
//            PlayerData player = new PlayerData{userName = "user",password = "12345"};
//            
//            //Act
//            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(player));
//
//            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
//            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
//            HttpResponseMessage response = new HttpResponseMessage();
//            response = await _client.PostAsync(requestUri, httpContent);
//            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//            
//
//            //Assert
//        }
    }
}