using Isminuotojai.Resources.Result;
using System;
namespace Isminuotojai.Classes
{
    public class ApiHandler
    {
        private const string Site = "localhost:8080";
        private string _token;//Token assigned on login
        private int _gameId;//Game Id assigned on starting game

        public Result DoMove(Move move)
        {
            return new Result();//TODO DoMove
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