using NUnit.Framework;
using Isminuotojai.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Isminuotojai.Tests;
using Isminuotojai.Resources;
using System.Net;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture()]
    public class ApiHandlerTests
    {
        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.NotFound, false)]
        public async Task CreatePlayerAsyncTest(HttpStatusCode status, bool expected = true)
        {
            MockHttpHandler handler = new MockHttpHandler();
            var player = new PlayerData { userName = "user", password = "pass" };
            handler.SetReturnObj("");
            handler.SetHttpStatus(status);
            ApiHandler.Instance.SetHandler(handler);
            bool result = await ApiHandler.Instance.CreatePlayerAsync(player);
            Assert.AreEqual(expected, result);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.NotFound, false, char.MinValue)]
        public async Task DoMoveAsyncTest(HttpStatusCode status, bool success = true, char expected = 't')
        {
            MockHttpHandler handler = new MockHttpHandler();
            handler.SetHttpStatus(status);
            var move = new Move { Type = MoveType.Set, X = 0, Y = 0 };
            char[,] map = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = 'u';
                }
            }
            map[0, 0] = 't';
            var result = new MineResult { map = map, status = GameStatus.Ongoing, success = true, turn = true };
            handler.SetReturnObj(result);
            ApiHandler.Instance.SetHandler(handler);
            result = await ApiHandler.Instance.DoMoveAsync(move);
            Assert.AreEqual(success, result.success);
            Assert.AreEqual(expected, result.map[0, 0]);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.NotFound, false, char.MinValue)]
        public async Task UpdateAsyncTest(HttpStatusCode status, bool success = true, char expected = 't')
        {
            MockHttpHandler handler = new MockHttpHandler();
            handler.SetHttpStatus(status);
            char[,] map = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = 'u';
                }
            }
            map[0, 0] = 't';
            var result = new MineResult { map = map, status = GameStatus.Ongoing, success = true, turn = true };
            handler.SetReturnObj(result);
            ApiHandler.Instance.SetHandler(handler);
            result = await ApiHandler.Instance.UpdateAsync();
            Assert.AreEqual(success, result.success);
            Assert.AreEqual(expected, result.map[0, 0]);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.NotFound, false, char.MinValue, GameStatus.Ongoing)]
        public async Task SurrenderAsyncTest(HttpStatusCode status, bool success = true, char expected = 'e', GameStatus gameStatus = GameStatus.Lost)
        {
            MockHttpHandler handler = new MockHttpHandler();
            handler.SetHttpStatus(status);
            char[,] map = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = 'u';
                }
            }
            map[0, 0] = 'e';
            var result = new MineResult { map = map, status = GameStatus.Lost, success = true, turn = true };
            handler.SetReturnObj(result);
            ApiHandler.Instance.SetHandler(handler);
            result = await ApiHandler.Instance.SurrenderAsync();
            Assert.AreEqual(success, result.success);
            Assert.AreEqual(expected, result.map[0, 0]);
            Assert.AreEqual(gameStatus, result.status);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.NotFound, false)]
        public async Task GetTokenAsyncTest(HttpStatusCode status, bool expected = true)
        {
            MockHttpHandler handler = new MockHttpHandler();
            handler.SetHttpStatus(status);
            var player = new PlayerData { userName = "user", password = "pass" };
            handler.SetReturnObj("aaaaaaaaa");
            ApiHandler.Instance.SetHandler(handler);
            bool result = await ApiHandler.Instance.GetTokenAsync(player);
            Assert.AreEqual(expected, result);
        }
        
        [TestCase(HttpStatusCode.OK, MoveSet.MineSetter)]
        [TestCase(HttpStatusCode.NotFound)]
        public async Task StartGameAsyncTest(HttpStatusCode status, MoveSet expected = MoveSet.MineSetter)
        {
            MockHttpHandler handler = new MockHttpHandler();
            handler.SetHttpStatus(status);
            var gamedata = new GameData{GameId = "1", Role = MoveSet.MineSetter};
            handler.SetReturnObj(gamedata);
            ApiHandler.Instance.SetHandler(handler);
            var result = await ApiHandler.Instance.StartGameAsync();
            Assert.AreEqual(expected, result);
        }
    }
}