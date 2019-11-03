using Isminuotojai.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Isminuotojai.Classes
{
    public class ApiHandler
    {
        //private const string Site = "https://mineserver20191008030835.azurewebsites.net";
        private const string Site = "https://localhost:44397";
        private string _token;//Token assigned on login
        private int _gameId;//Game Id assigned on starting game
        static WinHttpHandler handler = new WinHttpHandler();
        static HttpClient client = new HttpClient(handler);
        static string apipath = "/api/player/";
        static string mediaType = "application/json";
        private static string requestUri = Site + apipath;




        /// <summary>
        /// Creates Player
        /// </summary>
        /// <param name="player">Player username and password</param>
        /// <returns>If player got created</returns>
        public  async Task<bool> CreatePlayerAsync(PlayerData player)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(player));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.PostAsync(
                requestUri, httpContent);
            }
            catch
            {
                return false;
            }

            return response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<MineResult> DoMove(Move move)
        {
            MineResult result = new MineResult();
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(move));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);

            HttpResponseMessage response = await client.PostAsync(
                requestUri + "DoMove/" + _gameId, httpContent);
            try
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MineResult>(responseBody);
            }
            catch
            {
                //Catching
            }

            return result;
        }


        public async Task<MineResult> Update()
        {
            MineResult result = new MineResult();
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(""));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);

            HttpResponseMessage response = await client.GetAsync(
                requestUri + "Update/" + _gameId);
            try
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<MineResult>(responseBody);
            }
            catch
            {
                //Catching
            }

            return result;
        }

        public void Surrender()
        {
            //TODO Surrender
        }


        public async Task<bool> GetToken(PlayerData player)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(player));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            //var httpContent = new StringContent(stringPayload, Encoding.UTF8, mediaType);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
                Content = new StringContent(stringPayload, Encoding.UTF8, mediaType),
            };

            HttpResponseMessage response;
            try
            {
                 response = await client.SendAsync(
                request);
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                return false;
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            _token = JsonConvert.DeserializeObject<string>(responseBody);
            //_token = _token.Substring(1, _token.Length - 2);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            return response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public async Task<MoveSet> StartGame()
        {
            //TODO Assign game id
            //TODO Start game
            //TODO Recognize role
            HttpResponseMessage response = new HttpResponseMessage();
            var httpContent = new StringContent("", Encoding.UTF8, mediaType);
            try
            {
                response = await client.PostAsync(requestUri + "/StartGame", httpContent);
            }
            catch
            {
                return MoveSet.MineSetter;
            }
            //JSON deserialization
            GameData data = new GameData();
            string responseBody = await response.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<GameData>(responseBody);
            //Assignment of game id
            _gameId = data.GameId;

            return data.Role;
        }

    }
}