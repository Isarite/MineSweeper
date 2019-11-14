using NUnit.Framework;
using MineServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MineServer.Resources;

namespace MineServer.Models.Tests
{
    [TestFixture()]
    public class GameTests
    {

        Game game;
        [SetUp]
        public void Setup()
        {
            game = new Game();
        }

        [TearDown]
        public void TearDown()
        {
            game = null;
        }


        [TestCase("user","user2")]
        [TestCase("user", "user")]
        [TestCase(null, null)]
        public void SetPlayersTest(string username, string username2)
        {
            game.SetPlayers(
                new Player { UserName = username }
                , new Player { UserName = username2 });
            Assert.IsNotNull(game.players[0]);
            Assert.IsNotNull(game.players[1]);
            Assert.AreEqual(username, game.players[0].UserName);
            Assert.AreEqual(username2, game.players[1].UserName);
        }

        [TestCase("user", "user2")]
        [TestCase("user", "user")]
        [TestCase(null, null)]
        public void AddPlayerTest(string username, string username2)
        {
            game.AddPlayer(
                new Player {UserName = username});
            game.AddPlayer(
                new Player { UserName = username2 });
            Assert.IsNotNull(game.players[0]);
            Assert.IsNotNull(game.players[1]);
            Assert.AreEqual(username, game.players[0].UserName);
            Assert.AreEqual(username2, game.players[1].UserName);
        }

        [TestCase("1", true)]
        [TestCase("2", true)]
        [TestCase("0", false)]
        public void AuthorizeTest(string id, bool expected)
        {
            game.players.Add(
                new Player { Id = "1" });
            game.players.Add(
                new Player { Id = "2" });
            Assert.AreEqual(expected, game.Authorize(id));
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void AddTurnsTest(string id)
        {
            game.players.Add(
                new Player { Id = "1" });
            game.players.Add(
                new Player { Id = "2" });
            game.AddTurns(id);
            var player = game.players.Find(p => id.Equals(p.Id));
            if (player != null)
                Assert.AreEqual(player.TurnsLeft,game.Turns());
            else
                Assert.Pass();
        }

        [TestCase("1", true)]
        [TestCase("2", true)]
        [TestCase("3", false)]
        public void UpdateTest(string id, bool expectedSuccess)
        {
            game.players.Add(
                new Player { Id = "1" });
            game.players.Add(
                new Player { Id = "2" });
            var result = game.Update(id);

            Assert.AreEqual(expectedSuccess, result.success);
            if(expectedSuccess)
                Assert.AreEqual(GameStatus.Ongoing, result.status);
        }

        [TestCase(0,false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void FindPlayerTest(int timesToAdd, bool expected)
        {
            for(int i = 0; i < timesToAdd; i++)
                game.players.Add(
                    new Player { Id = i.ToString() });
            var result = game.FindPlayer(0.ToString());
            Assert.AreEqual(expected, result != null);
            if (expected)
                Assert.AreEqual(0.ToString(), result.Id);
        }
    }
}