using Isminuotojai.Resources.Result;
using System;
using System.Net.Http;

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
            Result result = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
            return new Result();//TODO DoMove
        }
        
        static async Task<Uri> CreatePlayerAsync(PlayerData player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            Player player2 = await response.Content.ReadAsAsync<Player>();
            if(player2 != null){
                ShowProduct(player2);
            } 

            // return URI of the created resource.
            return response.Headers.Location;
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