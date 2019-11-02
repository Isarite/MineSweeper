using Isminuotojai.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Classes
{
    public class ApiHandler
    {
        private const string Site = "localhost:8080";
        private string _token;//Token assigned on login
        private int _gameId;//Game Id assigned on starting game
        static HttpClient client = new HttpClient();
        static string requestUri = "api/player/";
        static string mediaType = "application/json";


        public Result DoMove(Move move)
        {
            return new Result();//TODO DoMove
        }
        
        /// <summary>
        /// Creates Player
        /// </summary>
        /// <param name="player">Player username and password</param>
        /// <returns>If player got created</returns>
        static async Task<bool> CreatePlayerAsync(PlayerData player)
        {
            var stringPayload = await Task.Run(() => Newtonsoft.Json.JsonConvert.SerializeObject(player));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(
                requestUri, httpContent);
            return  response.StatusCode.Equals(HttpStatusCode.OK);
        }

        public void Surrender()
        {
            //TODO Surrender
        }

        public void CreateAccount()
        {
            //TODO Create Account
        }

        public string GetToken()
        {
            return "token";//TODO Get Token
        }

        public void StartGame()
        {
            //TODO Assign game id
            //TODO Start game
            //TODO Recognize role
        }
        
    }
}