using Isminuotojai.Classes;
using Isminuotojai.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Tests
{
    class MockAPI : IAPI
    {

        MineResult result;
        Move move;
        PlayerData player;
        MoveSet role;

        public MockAPI(MineResult result, Move move, PlayerData player, MoveSet role)
        {
            this.result = result;
            this.move = move;
            this.player = player;
            this.role = role;
        }
        public async Task<bool> CreatePlayerAsync(PlayerData player)
        {
            this.player = player;
            return true;
        }

        public async Task<MineResult> DoMoveAsync(Move move)
        {
            return result;
        }

        public async Task<bool> GetTokenAsync(PlayerData player)
        {
            return true;
        }

        public async Task<MoveSet> StartGameAsync()
        {
            return role;
        }

        public async Task<MineResult> SurrenderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MineResult> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
