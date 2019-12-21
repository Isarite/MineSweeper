using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private WebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            //TestServer testServer = new TestServer(builder);
            //TestServer testServer = TestUtils.CreateTestServer();
            //var handler = testServer.CreateHandler();
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
            SetUpPlayers();
            //_client = testServer.CreateClient();
            //Setting up a player account
        }

        [TearDown]
        public void TearDown()
        {
            _client = null;
            _factory = null;
        }


        /// <summary>
        /// Sets up players in database
        /// </summary>
        private void SetUpPlayers()
        {
            PlayerData player = new PlayerData{userName = "user2",password = "#aAaA12345"};
            var stringPayload =  Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent0 = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            Task.Run(() => _client.PostAsync(requestUri, httpContent0)).Wait();

            player = new PlayerData{userName = "user1",password = "#aAaA12345"};
            stringPayload =  Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            httpContent0 = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            Task.Run(() => _client.PostAsync(requestUri, httpContent0)).Wait();

            player = new PlayerData{userName = "user3",password = "#aAaA12345"};
            stringPayload =  Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            httpContent0 = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            Task.Run(() => _client.PostAsync(requestUri, httpContent0)).Wait();
        }

        [TestCase("user", "#aAaA12345")]
        [TestCase("userWithoutPassword", "")]
        public async Task PlayerCreationTest(string userName, string password)
        {
            //Arrange
            PlayerData player = new PlayerData{userName = userName, password = password};

            //Act
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(player));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            HttpResponseMessage response = new HttpResponseMessage();
            response = await _client.PostAsync(requestUri, httpContent);
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase("user1", "#aAaA12345")]
        [TestCase("user2", "#aAaA12345")]
        [TestCase("user4", "#aAaA12345",HttpStatusCode.NotFound)]
        [TestCase("user1", "wrongPa$$word",HttpStatusCode.NotFound)]
        public void PlayerTokenGetTest(string userName, string password, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            //Arrange
            PlayerData player = new PlayerData{userName = userName, password = password};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            //Act
            var response = Task.Run( () => _client.PutAsync(requestUri, httpContent)).Result;
            var responseBody = Task.Run(() =>response.Content.ReadAsStringAsync()).Result;
            string token = JsonConvert.DeserializeObject<string>(responseBody);

            //Assert
            TestContext.Out.WriteLine(token);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
            //Assert.IsInstanceOf<string>(token);
            if (expectedStatusCode.Equals(HttpStatusCode.OK))
            {
                Assert.IsNotEmpty(token);
                Assert.IsTrue(token.Length > 0);
            }
        }

        [TestCase("user1", "#aAaA12345")]
        [TestCase("user2", "#aAaA12345")]
        public void StartGameTest(string userName, string password)
        {
            //Arrange(string userName, string password)
            PlayerData player = new PlayerData{userName = userName,password = password};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            var response = Task.Run( () => _client.PutAsync(requestUri, httpContent)).Result;
            var responseBody = Task.Run(() =>response.Content.ReadAsStringAsync()).Result;
            string token = JsonConvert.DeserializeObject<string>(responseBody);
            //Set authentication token in client
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpContent = new StringContent("", Encoding.UTF8, mediaType);
            TestContext.Out.WriteLine(responseBody);
            //Act
            response = Task.Run(()=>_client.PostAsync("api/player/StartGame", httpContent)).Result;

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var data = JsonConvert.DeserializeObject<GameData>(responseBody);

            //Assert

            TestContext.Out.WriteLine(response.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(MoveSet.MineSetter,data.Role);
            Assert.IsTrue( data.GameId > 0);
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]
        [TestCase(9, 0, "user1", "#aAaA12345", false, true)]
        public void SetMineTest(int X,  int Y, string userName = "user2", string password = "#aAaA12345"
            , bool turn = true, bool isNull = false)
        {
            //Arrange
            PlayerData player = new PlayerData{userName = "user2",password = "#aAaA12345"};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            var response = Task.Run( () => _client.PutAsync(requestUri, httpContent)).Result;
            //Get Response body
            var responseBody = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
            //Deserialize token
            string token = JsonConvert.DeserializeObject<string>(responseBody);
            TestContext.Out.WriteLine("Token: " + token);
            //Set authentication token in client
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //Start game
            httpContent = new StringContent("", Encoding.UTF8, mediaType);
            response = Task.Run(()=>_client.PostAsync("api/player/StartGame", httpContent)).Result;

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var data = JsonConvert.DeserializeObject<GameData>(responseBody);
            TestContext.Out.WriteLine(responseBody);
            SetClientToken(new PlayerData{userName ="user1", password = "#aAaA12345"});
            StartGame();
            SetClientToken(new PlayerData{userName =userName, password = password});


            Move move = new Move {Type = MoveType.Set, X = X, Y = Y};
            stringPayload =  Task.Run(() => JsonConvert.SerializeObject(move)).Result;
            httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            //Act
            response = Task.Run(()=>_client.PostAsync("/api/player/DoMove/" + data.GameId, httpContent)).Result;

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert

            //TestContext.Out.WriteLine(response.StatusCode);
            //TestContext.Out.WriteLine(responseBody);
            //TestContext.Out.WriteLine(result.status);
            Assert.AreEqual(isNull,result.map == null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true,result.success);
            Assert.AreEqual(turn,result.turn);
            Assert.AreEqual(GameStatus.Ongoing,result.status);
            if(result.map!=null)
                Assert.AreEqual('t', result.map[X,Y]);
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]
        public void UnsetMineTest(int X, int Y)
        {
                        //Arrange
            PlayerData player = new PlayerData{userName = "user2",password = "#aAaA12345"};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            var response = Task.Run( () => _client.PutAsync(requestUri, httpContent)).Result;
            //Get Response body
            var responseBody = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
            //Deserialize token
            string token = JsonConvert.DeserializeObject<string>(responseBody);
            TestContext.Out.WriteLine("Token: " + token);
            //Set authentication token in client
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //Start game
            httpContent = new StringContent("", Encoding.UTF8, mediaType);
            response = Task.Run(()=>_client.PostAsync("api/player/StartGame", httpContent)).Result;

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var data = JsonConvert.DeserializeObject<GameData>(responseBody);
            TestContext.Out.WriteLine(responseBody);

            //Set Mine
            Move move = new Move {Type = MoveType.Set, X = X, Y = Y};
            stringPayload =  Task.Run(() => JsonConvert.SerializeObject(move)).Result;
            httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            response = Task.Run(()=>_client.PostAsync("/api/player/DoMove/" + data.GameId, httpContent)).Result;

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;

            //Act
            move = new Move {Type = MoveType.Unset, X = X, Y = Y};
            stringPayload =  Task.Run(() => JsonConvert.SerializeObject(move)).Result;
            httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            //Unset mine
            response = Task.Run(()=>_client.PostAsync("/api/player/DoMove/" + data.GameId, httpContent)).Result;
            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert

            //TestContext.Out.WriteLine(response.StatusCode);
            //TestContext.Out.WriteLine(responseBody);
            //TestContext.Out.WriteLine(result.status);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true,result.success);
            Assert.AreEqual(true,result.turn);
            Assert.AreEqual(GameStatus.Ongoing,result.status);
            Assert.AreEqual('u', result.map[X,Y]);
        }

        [Test]
        public void EndTurnTest()
        {
            PlayerData player = new PlayerData{userName = "user2",password = "#aAaA12345"};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            var response = Task.Run( () => _client.PutAsync(requestUri, httpContent)).Result;
            //Get Response body
            var responseBody = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
            //Deserialize token
            string token = JsonConvert.DeserializeObject<string>(responseBody);
            TestContext.Out.WriteLine("Token: " + token);
            //Set authentication token in client
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            //Start game
            httpContent = new StringContent("", Encoding.UTF8, mediaType);
            response = Task.Run(()=>_client.PostAsync("api/player/StartGame", httpContent)).Result;

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var data = JsonConvert.DeserializeObject<GameData>(responseBody);
            TestContext.Out.WriteLine(responseBody);

            for (int i = 0; i < 10; i++)
            {
                Move move = new Move {Type = MoveType.Set, X = 0, Y = i};
                stringPayload = Task.Run(() => JsonConvert.SerializeObject(move)).Result;
                httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
                //Act
                response = Task.Run(() => _client.PostAsync("/api/player/DoMove/" + data.GameId, httpContent)).Result;
            }

            responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);

            Assert.AreEqual(false, result.turn);
        }

        [TestCase(0, 0,'e', GameStatus.Lost)]
        [TestCase(0, 1,'e', GameStatus.Lost)]
        [TestCase(1, 0,'2')]
        [TestCase(0, 9,'e', GameStatus.Lost)]
        [TestCase(9, 9,'0', GameStatus.Won)]
        [TestCase(9, 0,'0',GameStatus.Won)]
        public void RevealCellTest(int X, int Y, char expected, GameStatus expectedStatus = GameStatus.Ongoing)
        {
            //Arrange
            PlayerData player1 = new PlayerData{userName = "user1",password = "#aAaA12345"};
            PlayerData player2 = new PlayerData{userName = "user2",password = "#aAaA12345"};

            SetClientToken(player1);
            var gameData1 = StartGame();
            SetClientToken(player2);
            var gameData2 = StartGame();
            SetClientToken(player1);



            for (int i = 0; i < 10; i++)
            {
                Move move1 = new Move {Type = MoveType.Set, X = 0, Y = i};
                var stringPayload1 = Task.Run(() => JsonConvert.SerializeObject(move1)).Result;
                var httpContent1 = new StringContent(stringPayload1, Encoding.UTF8, mediaType);
                Task.Run(() => _client.PostAsync("/api/player/DoMove/" + gameData1.GameId, httpContent1)).Wait();
            }
            SetClientToken(player2);

            Move move = new Move {Type = MoveType.Reveal, X = X, Y = Y};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(move)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            //Act
            var response = Task.Run(() => _client.PostAsync("/api/player/DoMove/" + gameData2.GameId, httpContent)).Result;
            var responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true,result.success);
            Assert.AreEqual(true,result.turn);
            Assert.AreEqual(expectedStatus,result.status);
            Assert.AreEqual(expected, result.map[X,Y]);
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 9)]
        [TestCase(9, 9)]
        [TestCase(9, 0)]
        public void MarkCellTest(int X, int Y)
        {
            //Arrange
            PlayerData player1 = new PlayerData{userName = "user1",password = "#aAaA12345"};
            PlayerData player2 = new PlayerData{userName = "user2",password = "#aAaA12345"};

            SetClientToken(player1);
            var gameData1 = StartGame();
            SetClientToken(player2);
            var gameData2 = StartGame();
            SetClientToken(player1);



            for (int i = 0; i < 10; i++)
            {
                Move move1 = new Move {Type = MoveType.Set, X = 0, Y = i};
                var stringPayload1 = Task.Run(() => JsonConvert.SerializeObject(move1)).Result;
                var httpContent1 = new StringContent(stringPayload1, Encoding.UTF8, mediaType);
                Task.Run(() => _client.PostAsync("/api/player/DoMove/" + gameData1.GameId, httpContent1)).Wait();
            }
            SetClientToken(player2);

            Move move = new Move {Type = MoveType.Mark, X = X, Y = Y};
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(move)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            //Act
            var response = Task.Run(() => _client.PostAsync("/api/player/DoMove/" + gameData2.GameId, httpContent)).Result;
            var responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true,result.success);
            Assert.AreEqual(true,result.turn);
            Assert.AreEqual(GameStatus.Ongoing,result.status);
            Assert.AreEqual('m', result.map[X,Y]);
        }

        [TestCase("user1", "#aAaA12345")]
        [TestCase("user2", "#aAaA12345")]
        [TestCase("user3", "#aAaA12345",HttpStatusCode.Unauthorized)]
        public void SurrenderTest(string userName, string password
            , HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            //Arrange
            PlayerData player1 = new PlayerData{userName = "user1",password = "#aAaA12345"};
            PlayerData player2 = new PlayerData{userName = "user2",password = "#aAaA12345"};

            SetClientToken(player1);
            var gameData1 = StartGame();
            SetClientToken(player2);
            var gameData2 = StartGame();
            SetClientToken(new PlayerData{userName = userName, password = password});
            var httpContent = new StringContent("", Encoding.UTF8, mediaType);
            var response = Task.Run(()=>
                _client.PutAsync("api/player/Surrender/" + gameData1.GameId, httpContent)).Result;
            var responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert

            Assert.AreEqual(expectedStatusCode, response.StatusCode);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                Assert.True(result.success);
                Assert.AreEqual(GameStatus.Lost,result.status);
            }

        }

        [Test]
        public void UpdateTest()
        {
            //Arrange
            PlayerData player1 = new PlayerData{userName = "user1",password = "#aAaA12345"};
            PlayerData player2 = new PlayerData{userName = "user2",password = "#aAaA12345"};

            SetClientToken(player1);
            var gameData1 = StartGame();
            SetClientToken(player2);
            var gameData2 = StartGame();
            SetClientToken(player1);



            for (int i = 0; i < 10; i++)
            {
                Move move1 = new Move {Type = MoveType.Set, X = 0, Y = i};
                var stringPayload1 = Task.Run(() => JsonConvert.SerializeObject(move1)).Result;
                var httpContent1 = new StringContent(stringPayload1, Encoding.UTF8, mediaType);
                Task.Run(() => _client.PostAsync("/api/player/DoMove/" + gameData1.GameId, httpContent1)).Wait();
            }
            SetClientToken(player2);

            //Act
            var response = Task.Run(() => _client.GetAsync("/api/player/Update/" + gameData2.GameId)).Result;
            var responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            Result result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(true, result.turn);
            Assert.AreEqual(true, result.success);
            Assert.AreEqual(GameStatus.Ongoing, result.status);
            Assert.IsNotEmpty(result.map);
        }


        /// <summary>
        /// Get token from server, then set it to client
        /// </summary>
        /// <param name="player"></param>
        private void SetClientToken(PlayerData player)
        {
            var stringPayload = Task.Run(() => JsonConvert.SerializeObject(player)).Result;
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            var response = Task.Run( () => _client.PutAsync(requestUri, httpContent)).Result;
            //Get Response body
            var responseBody = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
            //Deserialize token
            string token = JsonConvert.DeserializeObject<string>(responseBody);
            TestContext.Out.WriteLine("Token: " + token);
            //Set authentication token in client
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Start game when token is already set
        /// </summary>
        private GameData StartGame()
        {
            var httpContent = new StringContent("", Encoding.UTF8, mediaType);
            var response = Task.Run(()=>_client.PostAsync("api/player/StartGame", httpContent)).Result;
            var responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var data = JsonConvert.DeserializeObject<GameData>(responseBody);
            return data;
        }
        
        [TestCase("user1", "#aAaA12345")]
        [TestCase("user2", "#aAaA12345")]
        public void GetPlayersTest(string userName, string password
            , HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            //Arrange
            PlayerData player1 = new PlayerData{userName = "user1",password = "#aAaA12345"};
            PlayerData player2 = new PlayerData{userName = "user2",password = "#aAaA12345"};

            SetClientToken(player1);
            var gameData1 = StartGame();
            SetClientToken(player2);
            var gameData2 = StartGame();
            SetClientToken(new PlayerData{userName = userName, password = password});
            var httpContent = new StringContent("", Encoding.UTF8, mediaType);
            var response = Task.Run(() =>
                _client.PutAsync("api/player/Surrender/" + gameData1.GameId, httpContent)).Result;
            var responseBody = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;
            var result = JsonConvert.DeserializeObject<Result>(responseBody);

            //Assert

            Assert.AreEqual(expectedStatusCode, response.StatusCode);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                Assert.True(result.success);
                Assert.AreEqual(GameStatus.Lost,result.status);
            }
            
            response = Task.Run(() =>
                _client.GetAsync("api/player/GetPlayers/")).Result;
            responseBody = Task.Run(() => response.Content.ReadAsStringAsync()).Result;
            Assert.IsNotNull(responseBody);
            Assert.IsNotEmpty(responseBody);

        }





    }
}
