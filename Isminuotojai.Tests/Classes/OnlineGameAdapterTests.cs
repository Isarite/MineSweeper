using NUnit.Framework;
using Isminuotojai.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Isminuotojai.Tests;
using Isminuotojai.Resources;
using System.Windows.Threading;
using System.Threading;
using System.Windows;

namespace Isminuotojai.Classes.Tests
{
    [TestFixture()]
    public class OnlineGameAdapterTests:Window
    {
        protected PlayerData pd;
        protected MoveSet role;

        protected Label label_turn, label_role;
        protected StackPanel left_menu_not_in_game, left_menu_game_started;

        protected Grid mineGrid;

        protected Dispatcher dispatcher;

        protected bool yourTurn = true;
        protected bool started = false;

        [SetUp]
        public void SetUp()
        {
            dispatcher  = this.Dispatcher;
            dispatcher.Invoke(setWPF);

        }

        public void setWPF()
        {
            label_turn = new Label();
            label_role = new Label();
            left_menu_game_started = new StackPanel();
            left_menu_not_in_game = new StackPanel();
            mineGrid = new Grid();
        }

        [Test()]
        public void StartGameTest()
        {
            var mockAPI = new MockAPI(new MineResult(), new Move(), new PlayerData(), new MoveSet());
            OnlineGameAdapter adapter = 
                new OnlineGameAdapter(label_turn, label_role, left_menu_not_in_game, 
                left_menu_game_started, dispatcher, mineGrid, mockAPI);
            Assert.DoesNotThrow(()=>adapter.StartGame());
        }

        [Test()]
        public void OnClickTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void LogoutTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SurrenderTest()
        {
            Assert.Fail();
        }
    }
}